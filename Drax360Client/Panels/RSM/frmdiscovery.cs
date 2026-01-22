using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net;
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

namespace Drax360Client.Panels.RSM
{
    // Converted VB-style enum block into C# enums.
    public enum optSetGet
    {
        setgetModuleNumber = 1,
        setgetDHCPName,
        setgetIPAddress,
        setgetSubnetMask,
        setgetGateway,
        setgetReport1,
        setgetReport2,
        setgetReport3,
        setgetReport4,
        setgetName1,
        setgetName2,
        setgetName3,
        setgetName4,
        setgetPort,
        setgetPortlistening,
        setgetPortUDP,
        setgetConnectionTimeout,
        setgetLicense,           // must meet certain requirements
        setgetPanelNumbers,      // string of 8 values for panels 1-8
        setgetEngineerTimeout,
        setgetDiscoveryTimeout,
        setgetAMXPollInterval,
        setgetSerialNumber,      // Must work in a certain way to set
        setgetRSMOptions,
        setGetRSMActivated,      // non-zero once the module has been online; activation logic described in original comment
        setgetSoftwareVersion,
        setgetReverseInputs,     // set is done with a CMD, but response is a GAK

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

        private const int kudpport = 1471; // Port for discovery
        private const char sepCHAR = (char)199;
        private const char stxCHAR = (char)2;
        private const char etxCHAR = (char)3;

        // Message id counter used by MakeUDPMessage
        private static int _messageCounter;

        // listener task/cancellation
        private CancellationTokenSource _cts;
        private Task? _listenerTask;

        public frmdiscovery()
        {
            InitializeComponent();

            // Start listener using async ReceiveAsync loop on a background task
            _cts = new CancellationTokenSource();
            _listenerTask = Task.Run(() => StartListenerAsync(_cts.Token));
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

        /// <summary>
        /// Sends a UDP broadcast discovery request.
        /// </summary>
        private static void SendViaUDP(string msg)
        {
            try
            {
                using (UdpClient udpClient = new UdpClient())
                {
                    udpClient.EnableBroadcast = true;
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, kudpport);

                    byte[] data = Encoding.ASCII.GetBytes(msg);
                    udpClient.Send(data, data.Length, endPoint);

                    Console.WriteLine($"[Client] Discovery request sent to {endPoint}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Client] Error sending discovery request: {ex.Message}");
            }

        }

        private static string makeudpmessage(string messageType, string messageData)
        {
            // Build the plain payload (fields separated by sepCHAR)
            // Original fields: MessageType | MessageID | "0" | SerialNumber | "" | MessageData
            string messageId = GetMessageID().ToString();
            string serialNumber = GetSerialNumber(); // placeholder - replace with actual retrieval if available

            // Use string.Concat to avoid culture-specific formatting
            string plain = string.Concat(
                messageType,
                sepCHAR,
                messageId,
                sepCHAR,
                "0",
                sepCHAR,
                serialNumber,
                sepCHAR,
                string.Empty,
                sepCHAR,
                messageData
            );

            string scrambled = scramblestring(plain);

            // Wrap with STX and ETX characters
            return string.Concat(stxCHAR, scrambled, etxCHAR);
        }

        // Thread-safe message id generator (matches original GetMessageID semantics)
        private static int GetMessageID()
        {
            return Interlocked.Increment(ref _messageCounter);
        }

        // Placeholder for serial number retrieval used in original VB code.
        // The original referenced: vsfDiscover.Cell(flexcpText, disRow.SerialNumber, 1)
        // Replace this method's implementation to read the actual serial where appropriate.
        private static string GetSerialNumber()
        {
            return string.Empty;
        }

        // Async listener using ReceiveAsync (non-blocking, cancellable)
        private static async Task StartListenerAsync(CancellationToken ct)
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
                                Console.WriteLine($"[Server] Descrambled: '{descr}'");
                                if (descr.Length > 0)
                                {
                                    parseresult(descr);



                                    // Try ASCII first (matches SendViaUDP), fallback to UTF8 if different
                                    string asciiMessage = Encoding.ASCII.GetString(receivedData);
                                    Console.WriteLine($"[Server] ASCII decode: '{asciiMessage}'");

                                    // If message is wrapped with STX/ETX try to descramble and display inner payload
                                    /*if (!string.IsNullOrEmpty(asciiMessage) && asciiMessage.Length >= 2 && asciiMessage[0] == stxCHAR && asciiMessage[asciiMessage.Length - 1] == etxCHAR)
                                    {
                                        string inner = asciiMessage.Substring(1, asciiMessage.Length - 2);
                                        Console.WriteLine($"[Server] Detected STX/ETX. Inner (scrambled) RAW: {BitConverter.ToString(Encoding.ASCII.GetBytes(inner))}");
                                        string descr = descramblestring(inner);
                                        Console.WriteLine($"[Server] Descrambled: '{descr}'");
                                        if (!string.IsNullOrEmpty(descr)) {
                                            parseresult(descr);
                                        }

                                    }
                                    else
                                    {
                                        // Also try UTF8 decode to be safe
                                        string utf8Message = Encoding.UTF8.GetString(receivedData);
                                        if (utf8Message != asciiMessage)
                                        {
                                            Console.WriteLine($"[Server] UTF8 decode: '{utf8Message}'");
                                        }
                                    }*/
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

        private static void parseresult(byte[] descr)
        {
           string[] mparts = SplitByteArrayToStrings(descr, (byte)199, Encoding.UTF8).ToArray();

    

            switch (mparts[0])
            {
                case "DIS":

                    break;

                default:
                    string xxx = mparts[0];

                    break;

            }

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
            foreach(byte enc in bytesreceived)
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
                throw new Exception("Checksum mismatch");
                return new byte[0];
            }
            return  result;


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
                    byte[] bytes = Encoding.ASCII.GetBytes(probeMessage);

                    // send probe
                    await udpClient.SendAsync(bytes, bytes.Length, ep);

                    // Listen for response on ephemeral port using ReceiveAsync with timeout
                    var receiveTask = udpClient.ReceiveAsync();
                    if (await Task.WhenAny(receiveTask, Task.Delay(timeoutMs)) == receiveTask)
                    {
                        var res = receiveTask.Result;
                        string asciiMessage = Encoding.ASCII.GetString(res.Buffer);
                        string decoded = asciiMessage;
                        if (decoded.Length >= 2 && decoded[0] == stxCHAR && decoded[decoded.Length - 1] == etxCHAR)
                        {
                            // MIKE TODO
                           // decoded = descramblestring(decoded.Substring(1, decoded.Length - 2));
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

        private void bttesttop_Click(object sender, EventArgs e)
        {
            // send a proper UDP message and await a response using the new async helper
            string msg = makeudpmessage("SET", optSetGet.setgetRESTART + sepCHAR + "554");
            SendViaUDP(msg);
            // fire-and-forget the send (already wrapped inside the helper)

            // can use the below to await response in UI
            //_ = SendProbeAndAwaitResponseAsync(msg);
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
