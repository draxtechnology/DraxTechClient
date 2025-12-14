using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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

                                // Try ASCII first (matches SendViaUDP), fallback to UTF8 if different
                                string asciiMessage = Encoding.ASCII.GetString(receivedData);
                                Console.WriteLine($"[Server] ASCII decode: '{asciiMessage}'");

                                // If message is wrapped with STX/ETX try to descramble and display inner payload
                                if (!string.IsNullOrEmpty(asciiMessage) && asciiMessage.Length >= 2 && asciiMessage[0] == stxCHAR && asciiMessage[asciiMessage.Length - 1] == etxCHAR)
                                {
                                    string inner = asciiMessage.Substring(1, asciiMessage.Length - 2);
                                    Console.WriteLine($"[Server] Detected STX/ETX. Inner (scrambled) RAW: {BitConverter.ToString(Encoding.ASCII.GetBytes(inner))}");
                                    string descr = descramblestring(inner);
                                    Console.WriteLine($"[Server] Descrambled: '{descr}'");
                                }
                                else
                                {
                                    // Also try UTF8 decode to be safe
                                    string utf8Message = Encoding.UTF8.GetString(receivedData);
                                    if (utf8Message != asciiMessage)
                                    {
                                        Console.WriteLine($"[Server] UTF8 decode: '{utf8Message}'");
                                    }
                                }
                            }
                            else
                            {
                                // Cancellation requested
                                break;
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

        // Converted from VB: DescrambleString
        private static string descramblestring(string sString)
        {
            if (string.IsNullOrEmpty(sString) || sString.Length < 2)
            {
                return string.Empty;
            }

            int pLs = sString.Length - 1; // exclude checksum char
            if (pLs < 1)
            {
                return string.Empty;
            }

            // Save checksum (last char)
            int checksumChar = sString[sString.Length - 1];

            // Descramble the string portion (excluding checksum)
            var sb = new StringBuilder(pLs);
            for (int n = 1; n <= pLs; n++)
            {
                int enc = sString[n - 1];
                int dec = enc - 3 - (n % 9) - ((n % 5) * 7);
                sb.Append((char)dec);
            }

            // Reverse to original order
            char[] arr = sb.ToString().ToCharArray();
            Array.Reverse(arr);
            string result = new string(arr);

            // Finally, calc and confirm the checksum byte
            int pCsum = 0;
            for (int i = 0; i < result.Length; i++)
            {
                pCsum += result[i];
            }
            pCsum = (pCsum % 200) + 33;
            if (pCsum != checksumChar)
            {
                return string.Empty;
            }

            return result;
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
                            decoded = descramblestring(decoded.Substring(1, decoded.Length - 2));
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
            SendViaUDP(msg)
            // fire-and-forget the send (already wrapped inside the helper)

            // can use the below to await response in UI
            //_ = SendProbeAndAwaitResponseAsync(msg);
        }

        #region to convert later
        /*
         Private Sub vsfDiscover_AfterEdit(ByVal row As Long, ByVal Col As Long)
    Dim TxString As String
    Dim Node As Long
    Dim AbortEntry As Boolean
    
    On Error GoTo vsfDiscover_AfterEdit_ERROR

    TxString = ""
    mbDiscoveryEdit = False
    
    'check for valid serial num
    If vsfDiscover.Cell(flexcpText, disRow.SerialNumber) = "" Then
        Exit Sub
    End If
    
    Node = Val(vsfDiscover.Cell(flexcpText, 0, 1))
    AbortEntry = False
    Select Case row
        Case disRow.ModuleNumber
            'module number
            If Node > 0 And RSM.InUse(Node) Then
                If myMsgBox(WrapText("This module number is already in use! If you proceed the current programming for the node may be lost. Do you wish to continue?", 50), vbExclamation + vbYesNo, "WARNING") <> vbYes Then
                    AbortEntry = True
                End If
            End If
            If Not AbortEntry Then
                TxString = MakeUDPMessage("SET", CStr(optSetGet.setgetModuleNumber) + Chr$(sepCHAR) + vsfDiscover.Cell(flexcpText, row, 1))
            End If
        Case disRow.DHCPName
            'dhcp name
            If Len(vsfDiscover.Cell(flexcpText, row, 1)) > 24 Then
                 myMsgBox "Only 24 characters maximum!", vbCritical + vbOK, "ERROR"
                AbortEntry = True
            Else
                TxString = MakeUDPMessage("SET", CStr(optSetGet.setgetDHCPName) + Chr$(sepCHAR) + vsfDiscover.Cell(flexcpText, row, 1))
            End If
        Case disRow.IpAddress
            'ip address
            If Not IsItDotNotation(vsfDiscover.Cell(flexcpText, row, 1)) Then
                myMsgBox vsfDiscover.Cell(flexcpText, row, 1) + " is not a valid IP address!", vbCritical + vbOK, "ERROR"
                AbortEntry = True
            Else
                TxString = MakeUDPMessage("SET", CStr(optSetGet.setgetIPAddress) + Chr$(sepCHAR) + vsfDiscover.Cell(flexcpText, row, 1))
            End If
         Case disRow.SubnetMask
            If Not IsItDotNotation(vsfDiscover.Cell(flexcpText, row, 1)) Then
                myMsgBox vsfDiscover.Cell(flexcpText, row, 1) + " is not a valid subnet mask!", vbCritical + vbOK, "ERROR"
                AbortEntry = True
            Else
                TxString = MakeUDPMessage("SET", CStr(optSetGet.setgetSubnetMask) + Chr$(sepCHAR) + vsfDiscover.Cell(flexcpText, row, 1))
            End If
         Case disRow.Gateway
            If Not IsItDotNotation(vsfDiscover.Cell(flexcpText, row, 1)) Then
                myMsgBox vsfDiscover.Cell(flexcpText, row, 1) + " is not a valid gateway address!", vbCritical + vbOK, "ERROR"
                AbortEntry = True
            Else
                TxString = MakeUDPMessage("SET", CStr(optSetGet.setgetGateway) + Chr$(sepCHAR) + vsfDiscover.Cell(flexcpText, row, 1))
            End If
         Case disRow.Report1
            If Not IsItDotNotation(vsfDiscover.Cell(flexcpText, row, 1)) Then
                TxString = MakeUDPMessage("SET", CStr(optSetGet.setgetName1) + Chr$(sepCHAR) + vsfDiscover.Cell(flexcpText, row, 1))
            Else
                TxString = MakeUDPMessage("SET", CStr(optSetGet.setgetReport1) + Chr$(sepCHAR) + vsfDiscover.Cell(flexcpText, row, 1))
            End If
         Case disRow.Report2
            If Not IsItDotNotation(vsfDiscover.Cell(flexcpText, row, 1)) Then
                TxString = MakeUDPMessage("SET", CStr(optSetGet.setgetName2) + Chr$(sepCHAR) + vsfDiscover.Cell(flexcpText, row, 1))
            Else
                TxString = MakeUDPMessage("SET", CStr(optSetGet.setgetReport2) + Chr$(sepCHAR) + vsfDiscover.Cell(flexcpText, row, 1))
            End If
       Case Else
            'do nothing
    End Select

    'Now send if it worked out OK
    If TxString <> "" Then
        'clear the current settings
        ClearDiscoverGrid
        SendViaUDP TxString
    End If
    
    'or restore the original text
    If AbortEntry Then
        vsfDiscover.Cell(flexcpText, row, 1) = BeforeEditText
    End If
    
vsfDiscover_AfterEdit_EXIT:
    On Error GoTo 0
    Exit Sub

vsfDiscover_AfterEdit_ERROR:
    ErrorBeep "Error " & Err.Number & " (" & Err.Description & ") in procedure vsfDiscover_AfterEdit of Form frmRSMDiscovery"       'noslz
    Resume Next
    
End Sub
         
         */
        #endregion
    }
}