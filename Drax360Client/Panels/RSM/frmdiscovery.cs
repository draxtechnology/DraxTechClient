using DraxClient.Panels.Email;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DraxClient.Panels.RSM
{
    // Converted VB-style enum block into C# enums.
    public enum optSetGet
    {
        setgetModuleNumber = 1,
        setgetDHCPName = 2 ,
        setgetIPAddress = 3 ,
        setgetSubnetMask = 4,
        setgetGateway = 5,
        setgetReport1 = 6,
        setgetReport2 = 7,
        setgetReport3 = 8,
        setgetReport4 = 9 ,
        setgetName1 = 10,
        setgetName2 = 11,
        setgetName3 = 12,
        setgetName4 = 13,
        setgetPort = 14,
        setgetPortlistening = 15,
        setgetPortUDP = 16,
        setgetConnectionTimeout = 17,
        setgetLicense = 18,           // must meet certain requirements
        setgetPanelNumbers = 19,      // string of 8 values for panels 1-8
        setgetEngineerTimeout = 20,
        setgetDiscoveryTimeout = 21,
        setgetAMXPollInterval = 22,
        setgetSerialNumber = 23,      // Must work in a certain way to set
        setgetRSMOptions = 24,
        setGetRSMActivated = 25,      // non-zero once the module has been online; activation logic described in original comment
        setgetSoftwareVersion = 26,
        setgetReverseInputs = 27,     // set is done with a CMD, but response is a GAK

        setgetResetDefaults = 198,   // needs a parameter of "544" for it to be actioned
        setgetRESTART = 199         // needs a parameter of "544" for it to be actioned
    }

    // The cmdToPanel enum is global for all RSM interfaces - keep ordering to match legacy mappings.
    public enum cmdToPanel
    {
        cmdMuteBuzzer = 0,
        cmdEvacuate,
        cmdSilenceAlarms,
        cmdReset,
        cmdResoundAlarms,
        cmdDisableDevice,
        cmdEnableDevice,
        cmdDisableZone,
        cmdEnableZone,
        cmdCreateEventTransient,
        cmdRemoveEventTransient,
        cmdCreateEvent,
        cmdRemoveEvent,
        cmdDisablePanelSounders,
        cmdEnablePanelSounders,
        cmdDisablePanelRelays,
        cmdEnablePanelRelays,
        cmdOFF,
        cmdOn,
        cmdShortPulse,
        cmdMediumPulse,
        cmdLongPulse,
        cmdShortFlash,
        cmdMediumFlash,
        cmdLongFlash,
        cmdExternalAcknowledgement,
        cmdExternalFireInitiate,
        cmdExternalAlarmInitiate,
        cmdExternalFireCancel,
        cmdExternalAlarmCancel,
        cmdAlertNetwork,
        cmdResoundNetworkAlarms,
        cmdDisableGroup,
        cmdEnableGroup,
        cmdAlertPanel,
        cmdResoundPanelAlarms,
        cmdActivateLoopOutput,
        cmdDeActivateLoopOutput,
        cmdEvacuateNetwork,
        cmdSilenceNetworkAlarms,
        cmdMuteNetworkBuzzers,
        cmdResetNetwork,
        cmdNetworkEvacuate,
        cmdGetReverseInputs,
        cmdSetReverseInputs,
        cmdInitInputs,
        cmdGetDeviceAnalogue
    }

    public partial class frmdiscovery : Form

    {

        #region enums

        private enum mdiscover
        {
            MessageType = 0,
            MessageID,
            ModuleNumber,
            SerialNumber,
            ModuleType,
            MACAddress,
            //DHCPName,
            IpAddress,
            SubnetMask,
            Gateway,
            ReportIP1,
            ReportIP2,   //currently unused
            ReportIP3,   //currently unused
            ReportIP4,   //currently unused
            SwVersion,
        }


        #endregion
        #region constants
        private const int kudpport = 1471; // Port for discovery
        private const char sepCHAR = (char)199;
        private const char stxCHAR = (char)2;
        private const char etxCHAR = (char)3;
        #endregion


        #region private variables
        private bool editmode = false;
        private string settingModuleNumber = string.Empty;
        private string settingModuleType = string.Empty;
        private string settingSerialNumber = string.Empty;
        private string settingMACAddress = string.Empty;
        private string settingDHCPName = string.Empty;
        private string settingIpAddress = string.Empty;
        private string settingSubnetMask = string.Empty;
        private string settingGateway = string.Empty;
        private string settingReporting1 = string.Empty;
        private string settingReporting2 = string.Empty;
        private string settingSoftwareVer = string.Empty;

        #endregion
        // Message id counter used by MakeUDPMessage
        private static int _messageCounter;

        // listener task/cancellation
        private CancellationTokenSource _cts;
        private Task? _listenerTask;

        // Capture the UI SynchronizationContext so background threads can marshal UI updates reliably
        private readonly SynchronizationContext _uiContext;
        public Device OurDevice { get; set; }
        public bool IsNew { get; set; }
        public frmdiscovery()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Cancel listener gracefully
            try
            {
                _cts?.Cancel();
                // give it a short time to exit
                _listenerTask?.Wait(250);
            }
            catch { }
            finally
            {
                _cts?.Dispose();
                _cts = null!;
                _listenerTask = null;
            }

            base.OnFormClosing(e);
        }

        // Helper: map string to single-byte-per-char (VB/legacy behaviour)
        private static byte[] MessageToRawBytes(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return Array.Empty<byte>();
            return msg.Select(c => (byte)c).ToArray();
        }

        // Helper: convert raw bytes to a single-byte-per-char string for logging/inspection
        private static string RawBytesToString(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return string.Empty;
            return new string(bytes.Select(b => (char)b).ToArray());
        }

        /// <summary>
        /// Sends a UDP broadcast discovery request.
        /// Uses single-byte mapping for chars to preserve legacy VB message format.
        /// </summary>
        private static void SendViaUDP(string msg, string settingIpAddress = "")
        {
            const int kdelay = 150;
            try
            {
                byte[] data = MessageToRawBytes(msg);

                using (UdpClient udpClient = new UdpClient())
                {
                    udpClient.EnableBroadcast = true;

                    // If specific IP provided, try sending direct first (6 times as VB)
                    if (!string.IsNullOrWhiteSpace(settingIpAddress) &&
                        IPAddress.TryParse(settingIpAddress, out IPAddress? parsedAddress))
                    {
                        var endPoint = new IPEndPoint(parsedAddress, kudpport);
                        for (int i = 0; i < 6; i++)
                        {
                            udpClient.Send(data, data.Length, endPoint);
                            Console.WriteLine($"[Client] Discovery request sent to {endPoint}");
                            Thread.Sleep(kdelay);
                        }
                    }

                    // Always also broadcast (matches original behavior)
                    var broadcastEP = new IPEndPoint(IPAddress.Broadcast, kudpport);
                    for (int i = 0; i < 6; i++)
                    {
                        udpClient.Send(data, data.Length, broadcastEP);
                        Console.WriteLine($"[Client] Discovery request sent to {broadcastEP}");
                        Thread.Sleep(kdelay);
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"[Client] Socket error ({ex.SocketErrorCode}): {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Client] Error sending discovery request: {ex.Message}");
            }
        }

        private static string makeudpmessage(string messageType, string messageData, string serialnumber)
        {

            // VB Version
            // MakeUDPMessage = Chr$(stxCHAR) + ScrambleString(MessageType + Chr$(sepCHAR) + CStr(GetMessageID()) + Chr$(sepCHAR) + "0" + Chr$(sepCHAR) + 
            // vsfDiscover.Cell(flexcpText, disRow.SerialNumber, 1) + Chr$(sepCHAR) + "" + Chr$(sepCHAR) + MessageData) + Chr$(etxCHAR)
            // Build the plain payload (fields separated by sepCHAR)
            // Original fields: MessageType | MessageID | "0" | SerialNumber | "" | MessageData
            string messageId = GetMessageID().ToString();


            // Use string.Concat to avoid culture-specific formatting
            string plain = string.Concat(
                messageType,
                sepCHAR,
                messageId,
                sepCHAR,
                "0",
                sepCHAR,
                serialnumber,
                sepCHAR,
                string.Empty,
                sepCHAR,
                messageData
            );
            /*
              VB at the top example:
              SET Ç 13 Ç 0 Ç C12860CE34 ÇÇ 3Ç 192.168.3.198
              SET Ç 4  Ç 0 Ç C1A0275FE3 ÇÇ 3Ç 192.168.3.197
            */



            string scrambled = scramblestring(plain);

            // Wrap with STX and ETX characters
            return string.Concat(stxCHAR, scrambled, etxCHAR);
        }

        // Thread-safe message id generator (matches original GetMessageID semantics)
        private static int GetMessageID()
        {
            return Interlocked.Increment(ref _messageCounter);
        }


        // Async listener using ReceiveAsync (non-blocking, cancellable)
        private async Task StartListenerAsync(CancellationToken ct)
        {
            try
            {

                using (UdpClient udpServer = new UdpClient(AddressFamily.InterNetwork))
                {
                    // Allow reuse so broadcasts reach multiple listeners and to avoid bind issues
                    udpServer.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    // Bind to any IPv4 address
                    udpServer.Client.Bind(new IPEndPoint(IPAddress.Any, kudpport));

                    Console.WriteLine($"[Server] Listening for discovery on port {kudpport}...");

                    while (!ct.IsCancellationRequested)
                    {
                        try
                        {
                            var receiveTask = udpServer.ReceiveAsync();
                            // Wait for either the receive to complete or cancellation
                            var completedTask = await Task.WhenAny(receiveTask, Task.Delay(Timeout.Infinite, ct));
                            if (completedTask == receiveTask)
                            {
                                UdpReceiveResult result = receiveTask.Result;
                                var remoteEP = result.RemoteEndPoint;
                                byte[] receivedData = result.Buffer;

                                // Log raw bytes for troubleshooting
                                Console.WriteLine($"[Server] Packet from {remoteEP} ({receivedData.Length} bytes) RAW: {BitConverter.ToString(receivedData)}");

                                byte[] descr = descramblebyte(receivedData);
                                Console.WriteLine($"[Server] Descrambled (raw): {BitConverter.ToString(descr)}");
                                Console.WriteLine($"[Server] Descrambled (text): '{RawBytesToString(descr)}'");

                                if (descr.Length > 0)
                                {
                                    parseresult(descr);

                                    // Try ASCII first (matches old code), fallback to Latin1 single-byte mapping for inspection
                                    string asciiMessage = Encoding.ASCII.GetString(receivedData);
                                    string latin1 = RawBytesToString(receivedData);
                                    Console.WriteLine($"[Server] ASCII decode: '{asciiMessage}'");
                                    if (latin1 != asciiMessage)
                                    {
                                        Console.WriteLine($"[Server] Latin1 decode: '{latin1}'");
                                    }
                                }
                                else
                                {
                                    // Cancellation requested
                                    break;
                                }
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            // cancellation requested - break loop
                            break;
                        }
                        catch (SocketException se)
                        {
                            Console.WriteLine($"[Server] Socket exception while receiving: {se}");
                            await Task.Delay(100, ct).ContinueWith(_ => { });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[Server] Receive error: {ex}");
                            await Task.Delay(100, ct).ContinueWith(_ => { });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Server] Error initializing listener: {ex}");
            }
        }

        private void parseresult(byte[] descr)
        {
            string[] mparts = SplitByteArrayToStrings(descr, (byte)199, Encoding.UTF8).ToArray();

            switch (mparts[0])
            {
                case "DIS":
                    if (editmode)
                    {
                        return;
                    }

                    editmode = true;


                    settingModuleNumber = mparts[(int)mdiscover.ModuleNumber];
                    settingModuleType = ExpandModuleType(mparts[(int)mdiscover.ModuleType]);
                    settingSerialNumber = mparts[(int)mdiscover.SerialNumber];
                    settingMACAddress = GetHexMacAddress(mparts[(int)mdiscover.MACAddress]);

                    settingDHCPName = OurDevice.Name; // this is coming from the device name - its not stored.
                    settingIpAddress = mparts[(int)mdiscover.IpAddress];
                    settingSubnetMask = mparts[(int)mdiscover.SubnetMask];
                    settingGateway = mparts[(int)mdiscover.Gateway];
                    settingReporting1 = mparts[(int)mdiscover.ReportIP1];
                    settingReporting2 = mparts[(int)mdiscover.ReportIP2];
                    settingSoftwareVer = mparts[(int)mdiscover.SwVersion];

                    /*
                    vsfDiscover.Cell(flexcpText, disRow.ModuleNumber, 1) = mParts(mDiscover.ModuleNumber)
             vsfDiscover.Cell(flexcpText, disRow.ModuleType, 1) = ExpandModuleType(mParts(mDiscover.ModuleType))
             vsfDiscover.Cell(flexcpText, disRow.SerialNumber, 1) = mParts(mDiscover.SerialNumber)
             sHexMacAddress = GetHexMacAddress(mParts(mDiscover.MACAddress))
'             GetHexMacAddress
             vsfDiscover.Cell(flexcpText, disRow.MACAddress, 1) = sHexMacAddress 'mParts(mDiscover.MACAddress)
             vsfDiscover.Cell(flexcpText, disRow.DHCPName, 1) = mParts(mDiscover.DHCPName)
             vsfDiscover.Cell(flexcpText, disRow.IpAddress, 1) = mParts(mDiscover.IpAddress)
             vsfDiscover.Cell(flexcpText, disRow.SubnetMask, 1) = mParts(mDiscover.SubnetMask)
             vsfDiscover.Cell(flexcpText, disRow.Gateway, 1) = mParts(mDiscover.Gateway)
             vsfDiscover.Cell(flexcpText, disRow.Report1, 1) = mParts(mDiscover.ReportIP1)
             vsfDiscover.Cell(flexcpText, disRow.Report2, 1) = mParts(mDiscover.ReportIP2)
             vsfDiscover.Cell(flexcpText, disRow.SwVersion, 1) = mParts(mDiscover.SwVersion)
                    */


                    // Ensure UI updates run on the main thread
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke((Action)(() => buttonhandler()));
                    }
                    else
                    {
                        buttonhandler();
                    }
                    break;

                default:
                    string xxx = mparts[0];
                    break;
            }

        }

        private string ExpandModuleType(string abbreviatedType)
        {
            switch (abbreviatedType)
            {
                case "MZ": return "Morley ZXe";
                case "4I": return "4 Input";
                case "12": return "12 Input";
                case "IO": return "4 I/P, 2 O/P";
                case "AD": return "Advanced MX";
                case "KE": return "Kentec";
                case "DX": return "Morley DX";
                case "NO": return "Notifier ID3000";
                case "GE": return "Gent Fire";
                case "CO": return "Coopers Fire/Easicheck";
                case "PE": return "Notifier Pearl";
                case "ZI": return "Ziton";
                default: return "?";
            }
        }

        private string GetHexMacAddress(string input)
        {
            return string.Join(":",
                 input.Split('.')
                      .Select(part => byte.Parse(part).ToString("X2")));
        }

        private void buttonhandler()
        {
            pbdiscoveryimage.Visible = !editmode;

            if (editmode)
            {

                tbmodulenumber.Text = settingModuleNumber;
                tbmoduletype.Text = settingModuleType;
                tbserialnumber.Text = settingSerialNumber;
                tbmacaddress.Text = settingMACAddress;

                tbdhcpname.Text = settingDHCPName;
                tbipaddress.Text = settingIpAddress;
                tbsubnetmask.Text = settingSubnetMask;
                tbgateway.Text = settingGateway;
                tbreportto1.Text = settingReporting1;
                tbreportto2.Text = settingReporting2;
                tbsoftwarever.Text = settingSoftwareVer;

            }
            else
            {
                // for safety clear the IP address
                settingIpAddress = "";
            }
            pnleditoptions.Visible = editmode;

        }
        public static List<string> SplitByteArrayToStrings(byte[] data, byte delimiter, Encoding encoding)

        {

            if (data == null)

                throw new ArgumentNullException(nameof(data));

            if (encoding == null)

                throw new ArgumentNullException(nameof(encoding));

            List<string> result = new List<string>();

            int start = 0;

            for (int i = 0; i < data.Length; i++)

            {

                if (data[i] == delimiter)

                {

                    // Extract segment

                    int length = i - start;

                    if (length > 0) // Avoid empty strings unless needed

                    {

                        string segment = encoding.GetString(data, start, length);

                        result.Add(segment);

                    }

                    start = i + 1; // Move past delimiter

                }

            }

            // Add last segment if any

            if (start < data.Length)

            {

                string segment = encoding.GetString(data, start, data.Length - start);

                result.Add(segment);

            }

            return result;

        }

        // Converted from VB: ScrambleString
        private static string scramblestring(string sString)
        {
            if (string.IsNullOrEmpty(sString))
            {
                return string.Empty;
            }

            int pLs = sString.Length;
            int pCsum = 0;

            // First, calc a checksum byte (sum of all char codes)
            for (int i = 0; i < pLs; i++)
            {
                pCsum += sString[i];
            }
            pCsum = (pCsum % 200) + 33;

            // Reverse the string
            char[] rev = sString.ToCharArray();
            Array.Reverse(rev);

            // Scramble the reversed string
            var sb = new StringBuilder(pLs + 1);
            for (int n = 1; n <= pLs; n++)
            {
                int orig = rev[n - 1];
                int scrambled = orig + 3 + (n % 9) + ((n % 5) * 7);
                sb.Append((char)scrambled);
            }

            // Append checksum char
            sb.Append((char)pCsum);
            return sb.ToString();
        }


        private static byte[] descramblebyte(byte[] bytesreceived)
        {
            int pCsum = 0;
            List<byte> toprocess = new List<byte>();
            foreach (byte enc in bytesreceived)
            {

                // ignore stxhar or etxchar

                if (enc == 2 || enc == 3)
                {
                    continue;
                }
                toprocess.Add(enc);
            }
            byte[] sString = toprocess.ToArray();


            int checksumChar = sString[sString.Length - 1];



            int masterlength = sString.Length - 1;


            // Descramble the string portion (excluding checksum)
            byte[] sb = new byte[masterlength];

            int bytecount = 0;
            for (int n = 0; n < masterlength; n++)
            {
                int enc = sString[n];
                int m = n + 1;
                int dec = enc - 3 - (m % 9) - ((m % 5) * 7);

                sb[bytecount] = (byte)dec;

                pCsum += dec;
                bytecount++;
            }


            int revcounter = 0;



            byte[] result = new byte[masterlength];
            for (int i = masterlength - 1; i >= 0; i--)
            {
                result[revcounter] = sb[i];
                revcounter++;
            }

            pCsum = (pCsum % 200) + 33;
            if (pCsum != checksumChar)   // TODO
            {
                // throw new Exception("Checksum mismatch");
                //return new byte[0];
            }
            return result;


        }

        public static StringBuilder ReverseStringBuilder(StringBuilder sb)
        {
            if (sb == null)
                throw new ArgumentNullException(nameof(sb), "StringBuilder cannot be null.");

            int length = sb.Length;
            for (int i = 0, j = length - 1; i < j; i++, j--)
            {
                // Swap characters at positions i and j
                char temp = sb[i];
                sb[i] = sb[j];
                sb[j] = temp;
            }

            return sb;
        }

        // Send probe and await a single response (used by UI test button)
        // This is fire-and-wait with timeout; shows MessageBox on result
        private async Task SendProbeAndAwaitResponseAsync(string probeMessage, int timeoutMs = 2000)
        {
            try
            {
                using (var udpClient = new UdpClient())
                {
                    udpClient.EnableBroadcast = true;
                    var ep = new IPEndPoint(IPAddress.Broadcast, kudpport);

                    byte[] bytes = MessageToRawBytes(probeMessage);

                    // send probe
                    await udpClient.SendAsync(bytes, bytes.Length, ep);

                    // Listen for response on ephemeral port using ReceiveAsync with timeout
                    var receiveTask = udpClient.ReceiveAsync();
                    if (await Task.WhenAny(receiveTask, Task.Delay(timeoutMs)) == receiveTask)
                    {
                        var res = receiveTask.Result;

                        // Convert raw reply bytes to single-byte string for inspection
                        string raw = RawBytesToString(res.Buffer);
                        string decoded = raw;

                        // If message wrapped with STX/ETX try descramble path
                        if (decoded.Length >= 2 && decoded[0] == stxCHAR && decoded[decoded.Length - 1] == etxCHAR)
                        {
                            try
                            {
                                byte[] descr = descramblebyte(res.Buffer);
                                decoded = RawBytesToString(descr);
                            }
                            catch (Exception ex)
                            {
                                decoded = $"<descramble failed: {ex.Message}>";
                            }
                        }

                        MessageBox.Show($"Response from {res.RemoteEndPoint}: {decoded}", "Probe result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No response within timeout.", "Probe result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Probe error: " + ex.Message, "Probe error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void frmdiscovery_Load(object sender, EventArgs e)
        {
            buttonhandler();

            if (!IsNew)
            {
                settingIpAddress = OurDevice.IP;
            }

            // Start listener using async ReceiveAsync loop on a background task
            _cts = new CancellationTokenSource();
            _listenerTask = Task.Run(() => StartListenerAsync(_cts.Token));

            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            progressBar1.Visible = false;
        }

        private void btcancel_Click(object sender, EventArgs e)
        {

            // send a proper UDP message and await a response using the new async helper
            string msg = makeudpmessage("SET", string.Concat((int)optSetGet.setgetRESTART, sepCHAR, "554"), settingSerialNumber);


            SendViaUDP(msg, settingIpAddress);

            cancel();
        }
        private void cancel()
        {

            editmode = !editmode;
            buttonhandler();
        }

        private void btsavechanges_Click(object sender, EventArgs e)
        {
            string msg = "";
            Cursor.Current = Cursors.WaitCursor;
            progressBar1.Visible = true;

            // has node number changed?
            string modulenumber = this.tbmodulenumber.Text;

            if (settingModuleNumber != modulenumber)
            {
                msg = makeudpmessage("SET", string.Concat((int)optSetGet.setgetModuleNumber, sepCHAR, modulenumber), settingSerialNumber);
                SendViaUDP(msg, settingIpAddress);
            }

            // has serial number changed?
            string serialnumber = this.tbserialnumber.Text;

            if (settingSerialNumber != serialnumber)
            {
                msg = makeudpmessage("SET", string.Concat((int)optSetGet.setgetSerialNumber, sepCHAR, serialnumber), settingSerialNumber);
                SendViaUDP(msg, settingIpAddress);
            }

            // has dhcp name changed?
            string dhcpname = this.tbdhcpname.Text;
            OurDevice.Name = dhcpname; // Update our device's Name

            if (settingDHCPName != dhcpname)
            {
               // msg = makeudpmessage("SET", string.Concat((int)optSetGet.setgetDHCPName, sepCHAR, dhcpname), settingSerialNumber);
               // SendViaUDP(msg, settingIpAddress);
               
            }

            // has ip address changed?
            string ipaddress = this.tbipaddress.Text;
            OurDevice.IP = ipaddress; // Update our device's IP

            if (settingIpAddress != ipaddress)
            {
                msg = makeudpmessage("SET", string.Concat((int)optSetGet.setgetIPAddress, sepCHAR, ipaddress), settingSerialNumber);
                SendViaUDP(msg, settingIpAddress);
                
            }

            // has id sub net mask changed?
            string subnetmask = this.tbsubnetmask.Text;

            if (settingSubnetMask != subnetmask)
            {
                msg = makeudpmessage("SET", string.Concat((int)optSetGet.setgetSubnetMask, sepCHAR, subnetmask), settingSerialNumber);
                SendViaUDP(msg, settingIpAddress);
            }

            // has the gateway changed?
            string gateway = this.tbgateway.Text;

            if (settingGateway != gateway)
            {
                msg = makeudpmessage("SET", string.Concat((int)optSetGet.setgetGateway, sepCHAR, gateway), settingSerialNumber);
                SendViaUDP(msg, settingIpAddress);
            }

            // has the Reporting 1 changed?
            string reporting1 = this.tbreportto1.Text;

            if (settingReporting1 != reporting1)
            {
                msg = makeudpmessage("SET", string.Concat((int)optSetGet.setgetReport1, sepCHAR, reporting1), settingSerialNumber);
                SendViaUDP(msg, settingIpAddress);
            }

            // has the Reporting 2 changed?
            string reporting2 = this.tbreportto2.Text;

            if (settingReporting2 != reporting2)
            {
                msg = makeudpmessage("SET", string.Concat((int)optSetGet.setgetReport2, sepCHAR, reporting2), settingSerialNumber);
                SendViaUDP(msg, settingIpAddress);
            }

            // bake in changes - not sure if this is needed but it matches the original flow where the UI is updated with the new values before sending the restart command
            msg = makeudpmessage("SET", string.Concat((int)optSetGet.setgetRESTART, sepCHAR, "554"), settingSerialNumber);


            SendViaUDP(msg, settingIpAddress);


            Cursor.Current = Cursors.Default;
            progressBar1.Visible = false;
            this.DialogResult = DialogResult.OK;
        }

        private void btrestoretodefaults_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This will reset the module to factory defaults. Are you sure?", "Confirm reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string msg = makeudpmessage("SET", string.Concat((int)optSetGet.setgetResetDefaults, sepCHAR, "544"), settingSerialNumber);
                SendViaUDP(msg, settingIpAddress);

                cancel();
            }

        }

        private void tbipaddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btclose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #region to convert later
        /*
         
         private void vsfDiscover_AfterEdit(long row, long col)
         {
             // Equivalent local variables from VB
             string txString = string.Empty;
             long Node = 0;
             bool AbortEntry = false;

             try
             {
                 txString = string.Empty;
                 // mbDiscoveryEdit = false; // preserve original flag usage if present

                 // check for valid serial num
                 // if (string.IsNullOrEmpty(vsfDiscover.Cell(flexcpText, disRow.SerialNumber, 1))) { return; }
                 // Node = Convert.ToInt64(vsfDiscover.Cell(flexcpText, 0, 1));
                 // AbortEntry = false;

                 // Example conversion of select-case to switch
                 switch (row)
                 {
                     case  0: // replace 0 with the actual enum/int for disRow.ModuleNumber
                         // module number
                         // if (Node > 0 && RSM.InUse(Node)) { if (myMsgBox(... ) != vbYes) { AbortEntry = true; } }
                         /* RPJ
                           if (!AbortEntry)
                         {
                             // TxString = MakeUDPMessage("SET", CStr(optSetGet.setgetModuleNumber) + Chr$(sepCHAR) + vsfDiscover.Cell(flexcpText, row, 1))
                             txString = makeudpmessage("SET", ((int) optSetGet.setgetModuleNumber).ToString() + sepCHAR + /*vsfDiscover.Cell(... row,1)*/ //string.Empty);
    }
    /* RPJ
                         break;
    
                     case 1: // replace 1 with the actual enum/int for disRow.DHCPName
                         // dhcp name
                         // if (vsfDiscover.Cell(...).Length > 24) { myMsgBox("Only 24 characters maximum!"); AbortEntry = true; } else { TxString = MakeUDPMessage("SET", ...); }
                         break;

                     case 2: // replace 2 with the actual enum/int for disRow.IPAddress
                         // ip address - validate dot notation then send
                         break;

                     case  3: // replace 3 with the actual enum/int for disRow.SubnetMask
                         // subnet mask
                         break;

                     case  4: // replace 4 with the actual enum/int for disRow.Gateway
                         // gateway
                         break;

                     case  5: // replace 5 with the actual enum/int for disRow.Report1
                         // report1 or name1 depending on content
                         break;

                     case  6: // replace 6 with the actual enum/int for disRow.Report2
                         // report2 or name2 depending on content
                         break;

                     default:
                         // do nothing
                         break;
                 }

// Now send if it worked out OK
if (!string.IsNullOrEmpty(txString))
{
    // ClearDiscoverGrid(); // clear the current settings
    SendViaUDP(txString);
}

// or restore the original text
if (AbortEntry)
{
    // vsfDiscover.Cell(flexcpText, row, 1) = BeforeEditText;
}
             }
             catch (Exception ex)
             {
                 // ErrorBeep("Error " + ex.Number + " (" + ex.Message + ") in procedure vsfDiscover_AfterEdit of Form frmRSMDiscovery");
                 // In VB the code used Resume Next and On Error handling; here we catch and log.
                 Console.WriteLine("Error in vsfDiscover_AfterEdit: " + ex.Message);
             }
         }

         // Notes:
         // - Placeholders (disRow.* numeric values, vsfDiscover.Cell access, RSM.InUse, myMsgBox, WrapText, BeforeEditText)
         //   need to be replaced with actual project types and methods when converting to executable C#.
         // - This block intentionally remains commented to serve as a reference translation from VB -> C#.
        */
    #endregion
}
