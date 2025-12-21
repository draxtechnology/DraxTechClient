using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpListenerApp
{
    public partial class TcpListenerForm : Form
    {
        private TcpListener tcpListener;
        private CancellationTokenSource cancellationTokenSource;
        private bool isListening = false;
        private string logFilePath = @"c:\temp\tcp_listener_log.txt";
        private object logLock = new object();

        public TcpListenerForm()
        {
            InitializeComponent();

            // Register encoding provider for Windows-1252
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            EnsureLogDirectory();
        }

        private void EnsureLogDirectory()
        {
            try
            {
                string directory = Path.GetDirectoryName(logFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating log directory: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InitializeComponent()
        {
            btnStart = new Button();
            btnStop = new Button();
            btnClear = new Button();
            txtData = new RichTextBox();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 12);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(100, 30);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start Listener";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += BtnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(118, 12);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(100, 30);
            btnStop.TabIndex = 1;
            btnStop.Text = "Stop Listener";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += BtnStop_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(224, 12);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 30);
            btnClear.TabIndex = 2;
            btnClear.Text = "Clear Data";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += BtnClear_Click;
            // 
            // txtData
            // 
            txtData.Font = new Font("Consolas", 9F);
            txtData.Location = new Point(12, 75);
            txtData.Name = "txtData";
            txtData.ReadOnly = true;
            txtData.Size = new Size(760, 363);
            txtData.TabIndex = 4;
            txtData.Text = "";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 50);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(113, 15);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Status: Not listening";
            // 
            // TcpListenerForm
            // 
            ClientSize = new Size(784, 450);
            Controls.Add(txtData);
            Controls.Add(lblStatus);
            Controls.Add(btnClear);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Name = "TcpListenerForm";
            Text = "TCP Listener - Port 1471";
            FormClosing += TcpListenerForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox txtData;
        private System.Windows.Forms.Label lblStatus;

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 1471);
                tcpListener.Start();

                cancellationTokenSource = new CancellationTokenSource();
                isListening = true;

                btnStart.Enabled = false;
                btnStop.Enabled = true;
                UpdateStatus("Listening on port 1471...");
                AppendData($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TCP Listener started on port 1471\n");

                await Task.Run(() => ListenForConnections(cancellationTokenSource.Token));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting listener: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StopListener();
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            StopListener();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtData.Clear();
        }

        private void StopListener()
        {
            isListening = false;
            cancellationTokenSource?.Cancel();
            tcpListener?.Stop();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            UpdateStatus("Not listening");
            // AppendData($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TCP Listener stopped\n");
        }

        private async void ListenForConnections(CancellationToken token)
        {
            while (isListening && !token.IsCancellationRequested)
            {
                try
                {
                    TcpClient client = await tcpListener.AcceptTcpClientAsync();
                    _ = Task.Run(() => HandleClient(client, token), token);
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    if (isListening)
                    {
                        AppendData($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Error accepting connection: {ex.Message}\n");
                    }
                }
            }
        }

        private async void HandleClient(TcpClient client, CancellationToken token)
        {
            string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

            // Only accept connections from given IPs
            if (clientIP != "192.168.3.1"    && clientIP != "192.168.3.199")
            {
                AppendData($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Rejected connection from {clientIP} (only accepting from 192.168.3.1)\n");
                client.Close();
                return;
            }

            AppendData($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Client connected from {clientIP}\n");

            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while (!token.IsCancellationRequested && (bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
                    {
                        // Create a copy of the received data
                        byte[] receivedData = new byte[bytesRead];
                        Array.Copy(buffer, receivedData, bytesRead);

                        // Format as hex
                        string hexData = BitConverter.ToString(receivedData).Replace("-", " ");

                        // Decode the data (XOR decryption)
                        string decodedData = DecodeData(receivedData);

                        // Try to decode as ASCII (replace non-printable with '.')
                        StringBuilder asciiData = new StringBuilder();
                        foreach (byte b in receivedData)
                        {
                            asciiData.Append(b >= 32 && b <= 126 ? (char)b : '.');
                        }

                        AppendData($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Received ({bytesRead} bytes)");
                        AppendData($"  DECODED: {decodedData}\n\n");

                        // Send ACK response
                        string ackResponse = GenerateAckResponse(decodedData);
                        if (!string.IsNullOrEmpty(ackResponse))
                        {
                            byte[] ackBytes = ScrambleAndEncodeMessage(ackResponse);

                            // --- Log the byte values to a file ---
                            string logPath = @"C:\Temp\CSharp_ACK_Bytes.txt";
                            using (StreamWriter writer = new StreamWriter(logPath, append: true))
                            {
                                writer.WriteLine($"Sending ACK at {DateTime.Now}");
                                for (int i = 0; i < ackBytes.Length; i++)
                                {
                                    writer.WriteLine($"Byte {i}: {ackBytes[i]}");
                                }
                                writer.WriteLine("-------------------------");
                            }
                            // --- End logging ---


                            await stream.WriteAsync(ackBytes, 0, ackBytes.Length, token);
                            await stream.FlushAsync(token); // <-- important
                            AppendData($"  SENT ACK: {ackResponse}\n");
                        }

                        AppendData("\n");
                    }
                }
            }
            catch (Exception ex)
            {
                // Only log errors that aren't the expected "connection closed by remote host"
                if (!token.IsCancellationRequested && !ex.Message.Contains("forcibly closed"))
                {
                    AppendData($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Error handling client: {ex.Message}\n");
                }
            }
            finally
            {
                client.Close();
                AppendData($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Client disconnected from {clientIP}\n");
            }
        }

        private void UpdateStatus(string status)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action<string>(UpdateStatus), status);
            }
            else
            {
                lblStatus.Text = $"Status: {status}";
            }
        }

        private void AppendData(string data)
        {
            if (txtData.InvokeRequired)
            {
                txtData.Invoke(new Action<string>(AppendData), data);
            }
            else
            {
                txtData.AppendText(data);
                txtData.ScrollToCaret();

                // Write to log file
                WriteToLog(data);
            }
        }

        private void WriteToLog(string message)
        {
            try
            {
                lock (logLock)
                {
                    File.AppendAllText(logFilePath, message);
                }
            }
            catch (Exception ex)
            {
                // Don't show message box here to avoid blocking, just log to console if needed
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        private string DecodeData(byte[] data)
        {
            if (data.Length < 3)
                return Encoding.ASCII.GetString(data);

            // VB6 strips STX (0x02) and ETX before descrambling
            // Find and remove STX (0x02) and ETX (0x03) bytes
            List<byte> cleanedData = new List<byte>();
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != 0x02 && data[i] != 0x03)
                {
                    cleanedData.Add(data[i]);
                }
            }

            if (cleanedData.Count < 2)
                return "";

            // Now the data is just the scrambled content + checksum (last byte)
            int dataLength = cleanedData.Count - 1; // Exclude checksum
            int checksumByte = cleanedData[cleanedData.Count - 1];

            // Descramble the string
            StringBuilder descrambled = new StringBuilder();
            for (int n = 1; n <= dataLength; n++)
            {
                int byteValue = cleanedData[n - 1]; // C# is 0-based, VB6 loop is 1-based
                int decoded = byteValue - 3 - (n % 9) - ((n % 5) * 7);

                // Handle wrap-around for negative values (VB6 byte range is 0-255)
                while (decoded < 0)
                    decoded += 256;
                while (decoded > 255)
                    decoded -= 256;

                descrambled.Append((char)decoded);
            }

            // Reverse the string
            char[] chars = descrambled.ToString().ToCharArray();
            Array.Reverse(chars);
            string reversed = new string(chars);

            // Calculate and confirm checksum
            int calculatedChecksum = 0;
            for (int n = 0; n < reversed.Length; n++)
            {
                calculatedChecksum += (int)reversed[n];
            }
            calculatedChecksum = (calculatedChecksum % 200) + 33;

            // Replace semicolons with Ç to match VB6 output
            string result = reversed.Replace(";", "Ç");

            // Checksum validation (optional display)
            if (calculatedChecksum != checksumByte)
            {
                result += $" [CHECKSUM ERROR: Expected {checksumByte}, Got {calculatedChecksum}]";
            }

            return result;
        }

        private void TcpListenerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListener();
        }

        private string GenerateAckResponse(string decodedMessage)
        {
            // Parse the decoded message to extract: MessageType, ModuleNumber, MessageID
            // Format: EVTÇ3159Ç1ÇB19810252D...
            // or: POLÇxxxx...

            // Remove checksum error text if present
            if (decodedMessage.Contains("[CHECKSUM ERROR"))
            {
                decodedMessage = decodedMessage.Substring(0, decodedMessage.IndexOf("[CHECKSUM ERROR")).Trim();
            }

            string[] parts = decodedMessage.Split('Ç');

            if (parts.Length < 3)
                return ""; // Not enough parts to generate ACK

            string messageType = parts[0];
            string messageID = parts[1];
            string moduleNumber = parts[2];

            // Generate ACK based on message type
            switch (messageType)
            {
                case "EVT":
                case "ZTX":
                case "ANA":
                case "SPX":
                    // ACK format: ACKÇModuleNumberÇMessageID
                    return $"ACK\u00C7{moduleNumber}\u00C7{messageID}";

                case "POL":
                    // PAK format: PAKÇModuleNumberÇMessageIDÇLicenseStatus
                    // For now, return license status 0 (valid)
                    return $"PAK\u00C7{moduleNumber}\u00C7{messageID}\u00C70";

                default:
                    // For unknown messages, send generic ACK
                    //return $"ACK;{moduleNumber};{messageID}";
                    return $"ACK\u00C7{moduleNumber}\u00C7{messageID}";
            }
        }


        private byte[] ScrambleAndEncodeMessage(string message)
        {
            // Replace semicolons back from our display format
            message = message.Replace(";", new string((char)0x3B, 1));

            // Calculate checksum
            int checksum = 0;
            foreach (char c in message)
            {
                checksum += (int)c;
            }
            checksum = (checksum % 200) + 33;

            // Scramble the message (reverse of descramble)
            // First reverse the string
            char[] chars = message.ToCharArray();
            Array.Reverse(chars);
            string reversed = new string(chars);

            // Then apply the scramble formula
            List<byte> scrambled = new List<byte>();
            for (int n = 1; n <= reversed.Length; n++)
            {
                int charValue = (int)reversed[n - 1];
                int encoded = charValue + 3 + (n % 9) + ((n % 5) * 7);

                // Handle wrap-around
                while (encoded > 255)
                    encoded -= 256;

                scrambled.Add((byte)encoded);
            }

            // Add STX header, scrambled data, checksum, and ETX
            List<byte> fullMessage = new List<byte>();
            fullMessage.Add(0x02); // STX
            fullMessage.AddRange(scrambled);
            fullMessage.Add((byte)checksum);
            fullMessage.Add(0x03); // ETX

            return fullMessage.ToArray();
        }

        public class RSMMessageParser
        {
            private const char SEPARATOR_CHAR = (char)199; // Ç character
            private const int AMX1_OFFSET = 0; // Set your actual offset

            // Field indices (from VB6 mField enum)
            private enum MessageField
            {
                MessageType = 0,
                MessageID = 1,
                ModuleNumber = 2,
                ModuleType = 3,
                SerialNumber = 4,
                LoopNum = 5,
                Address = 6,
                SubAddress = 7,
                OnOff = 8,
                EventType = 9,
                Extension = 10,
                Extension2 = 11,
                DeviceType = 12,
                Text = 13
            }

            public string ParseRSMMessages(string message, string ipAddress, out string moduleNumberReturn, long portNumber)
            {
                try
                {
                    // Split message by separator character (Ç)
                    string[] parts = message.Split(SEPARATOR_CHAR);

                    if (parts.Length < 3)
                    {
                        moduleNumberReturn = "";
                        // RSMStats.IncreaseStat(RSMst.UnknownAcksTX);
                        return "NAK";
                    }

                    // Parse common header fields
                    string messageType = parts[(int)MessageField.MessageType];
                    long messageID = ParseLong(parts[(int)MessageField.MessageID]);
                    long moduleNumber = ParseLong(parts[(int)MessageField.ModuleNumber]);
                    string moduleType = parts[(int)MessageField.ModuleType];
                    string serialNumber = parts[(int)MessageField.SerialNumber];

                    moduleNumberReturn = moduleNumber.ToString();

                    LogMessage($"RX: {messageType} from {moduleNumber}");

                    // Update RX tracking
                    UpdateRXTracking(moduleNumber, ipAddress, portNumber);

                    System.Diagnostics.Debug.WriteLine($"Received {messageType}");

                    // Route to appropriate message handler
                    switch (messageType)
                    {
                        case "EVT":
                            return HandleEventMessage(parts, moduleNumber, moduleType, messageID);

                        case "POL":
                            return HandlePollMessage(parts, moduleNumber, moduleType, serialNumber, messageID, ipAddress);

                        case "CAK":
                            return HandleCommandAck(moduleNumber, messageID);

                        case "SAK":
                            return HandleSetOptionAck(parts, moduleNumber, messageID);

                        case "GAK":
                            return HandleGetOptionAck(parts, moduleNumber, messageID);

                        case "ZTX":
                            return HandleZoneText(parts, moduleNumber, messageID);

                        case "ANA":
                            return HandleAnalogValue(parts, moduleNumber, moduleType, messageID);

                        case "SPX":
                            return HandleSpecialMessage(parts, moduleNumber, messageID);

                        case "ACK":
                            return HandleAck(moduleNumber, messageID);

                        case "NAK":
                            return HandleNak(moduleNumber);

                        default:
                            return HandleUnknownMessage(moduleNumber, messageID, message);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error in ParseRSMMessages: {ex.Message}");
                    System.Media.SystemSounds.Beep.Play();
                    moduleNumberReturn = "";
                    return "NAK";
                }
            }

            private string HandleEventMessage(string[] parts, long moduleNumber, string moduleType, long messageID)
            {
                try
                {
                    // Parse event-specific fields
                    int loopNum = ParseInt(parts[(int)MessageField.LoopNum]);
                    int address = ParseInt(parts[(int)MessageField.Address]);
                    int subAddress = ParseInt(parts[(int)MessageField.SubAddress]);
                    int onOff = ParseInt(parts[(int)MessageField.OnOff]);
                    int inputType = ParseInt(parts[(int)MessageField.EventType]);
                    string deviceText = parts[(int)MessageField.Text].Trim();
                    int extension = ParseInt(parts[(int)MessageField.Extension]);
                    int extension2 = ParseInt(parts[(int)MessageField.Extension2]);

                    LogMessage($"EVENT RX: Address: {address} Loop: {loopNum} OnOff: {onOff} Text: {deviceText}");

                    // Truncate device text based on module type
                    if (moduleType == "AD")
                    {
                        deviceText = deviceText.Substring(0, Math.Min(26, deviceText.Length));
                    }
                    else
                    {
                        deviceText = deviceText.Substring(0, Math.Min(40, deviceText.Length));
                    }

                    deviceText = MakeStringSafe(deviceText);

                    // Calculate zone
                    int zone;
                    if (moduleType == "ZI" || moduleType == "MZ")
                    {
                        zone = extension;
                    }
                    else
                    {
                        zone = extension + (256 * extension2);
                    }

                    // Get device type
                    string deviceType = GetDeviceType(parts[(int)MessageField.DeviceType], moduleType, inputType, extension2);

                    // Check for license overrides
                    CheckLicenseOverrides(ref loopNum, ref address, ref subAddress, ref onOff, ref inputType, ref deviceText, moduleNumber);

                    // Build event number
                    bool oneShot = false;
                    long eventNumber = MakeInputNumber(moduleNumber + AMX1_OFFSET, loopNum, address, inputType);

                    if (onOff == 1)
                    {
                        eventNumber += 0x80000000;
                        System.Diagnostics.Debug.WriteLine($"EVENT: {GetReferenceString(eventNumber)} ON");
                    }
                    else if (onOff == 2)
                    {
                        eventNumber += 0x80000000;
                        oneShot = true;
                        System.Diagnostics.Debug.WriteLine($"EVENT: {GetReferenceString(eventNumber)} ON+oneshot");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"EVENT: {GetReferenceString(eventNumber)} OFF");
                    }

                    // Add status input descriptive text
                    AddStatusText(ref deviceText, ref deviceType, loopNum, address, inputType, moduleType, onOff, moduleNumber);

                    // Get zone text
                    string zoneText = GetZoneText(deviceText, moduleType, moduleNumber, zone, extension, extension2, inputType, address);

                    // Normalize OnOff to boolean
                    bool isOn = (onOff != 0);

                    // Write to AMX
                    string transferFile = GetCurrentTransferFile();
                    LogMessage("WriteNWMData");
                    WriteNWMData(transferFile, 1, eventNumber, 0, deviceText, MakeStringSafe(deviceType), zoneText, isOn);

                    // Handle one-shot
                    if (oneShot)
                    {
                        NwmForceEvmAttribute(transferFile, eventNumber, 13, 1);
                    }

                    // Check for isolation list copy
                    // if (giIsolationsList != 0 && inputType == 4 && loopNum != 0)
                    // {
                    //     WriteNWMData(transferFile, 2, eventNumber, 0, deviceText, MakeStringSafe(deviceType), zoneText, isOn);
                    // }

                    // Flush to AMX if needed
                    if (GetQueuedData() < 5)
                    {
                        FlushAMX1Messages();
                    }

                    string ackResponse = $"ACK{SEPARATOR_CHAR}{moduleNumber}{SEPARATOR_CHAR}{messageID}";
                    System.Diagnostics.Debug.WriteLine(ackResponse);

                    // Send next command if available
                    // CmdQ.SendNextCommandDummy(moduleNumber);

                    return ackResponse;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error handling event: {ex.Message}");
                    return $"NAK{SEPARATOR_CHAR}{moduleNumber}{SEPARATOR_CHAR}{messageID}";
                }
            }

            private string HandlePollMessage(string[] parts, long moduleNumber, string moduleType, string serialNumber, long messageID, string ipAddress)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Received POL");
                    // RSMStats.IncreaseStat(RSMst.PollsRX);

                    // Parse poll-specific fields (adjust indices based on your protocol)
                    long expiryDateDays = parts.Length > 5 ? ParseLong(parts[5]) : 0;
                    int numberOfPanels = parts.Length > 6 ? ParseInt(parts[6]) : 0;
                    string moduleOptions = parts.Length > 7 ? parts[7] : "";

                    // Update RSM data
                    // RSM.ExpiryDate[moduleNumber] = expiryDateDays;
                    // RSM.PanelsAllowed[moduleNumber] = numberOfPanels;
                    // RSM.ModuleOptions[moduleNumber] = moduleOptions;
                    // RSM.ModuleType[moduleNumber] = moduleType;

                    int licenseStatus = 0; // UpdateLicenseInfo(moduleNumber, serialNumber, expiryDateDays, numberOfPanels, moduleOptions);

                    string pakResponse = $"PAK{SEPARATOR_CHAR}{moduleNumber}{SEPARATOR_CHAR}{messageID}{SEPARATOR_CHAR}{licenseStatus}";

                    // RSM.WaitingAck[moduleNumber] = false;
                    // CmdQ.SendNextCommand(moduleNumber);

                    System.Diagnostics.Debug.WriteLine($"Poll Received from {ipAddress}");

                    return pakResponse;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error handling poll: {ex.Message}");
                    return $"NAK{SEPARATOR_CHAR}{moduleNumber}{SEPARATOR_CHAR}{messageID}";
                }
            }

            private string HandleCommandAck(long moduleNumber, long messageID)
            {
                // RSMStats.IncreaseStat(RSMst.CommandAcks);
                System.Diagnostics.Debug.WriteLine($"Received CAK of {messageID}");

                // CmdQ.Remove(messageID);
                // RSM.WaitingAck[moduleNumber] = false;
                // CmdQ.SendNextCommandDummy(moduleNumber);

                return "";
            }

            private string HandleSetOptionAck(string[] parts, long moduleNumber, long messageID)
            {
                // RSMStats.IncreaseStat(RSMst.OptionSetAcks);
                System.Diagnostics.Debug.WriteLine("Received SAK");

                // CmdQ.Remove(messageID);
                // CmdQ.SendNextCommandDummy(moduleNumber);
                // RSM.WaitingAck[moduleNumber] = false;

                return "";
            }

            private string HandleGetOptionAck(string[] parts, long moduleNumber, long messageID)
            {
                System.Diagnostics.Debug.WriteLine("Received GAK");
                // RSMStats.IncreaseStat(RSMst.OptionGetAcks);

                // Parse and update options based on parts
                // (Implementation depends on your option structure)

                // CmdQ.Remove(messageID);
                // RSM.WaitingAck[moduleNumber] = false;
                // CmdQ.SendNextCommandDummy(moduleNumber);

                return "";
            }

            private string HandleZoneText(string[] parts, long moduleNumber, long messageID)
            {
                // RSMStats.IncreaseStat(RSMst.ZoneTexts);

                int extension = ParseInt(parts[(int)MessageField.Extension]);
                int extension2 = ParseInt(parts[(int)MessageField.Extension2]);
                int zone = extension2 + (256 * extension);
                string text = MakeStringSafe(parts[(int)MessageField.Text]);

                // WriteToIniFile("ZoneText", zone.ToString(), text, $"\\Temp\\RsmZtext\\{moduleNumber}");

                string ackResponse = $"ACK{SEPARATOR_CHAR}{moduleNumber}{SEPARATOR_CHAR}{messageID}";
                System.Diagnostics.Debug.WriteLine(ackResponse);

                return ackResponse;
            }

            private string HandleAnalogValue(string[] parts, long moduleNumber, string moduleType, long messageID)
            {
                // RSMStats.IncreaseStat(RSMst.AnalogValues);

                int loopNum = ParseInt(parts[(int)MessageField.LoopNum]);
                int address = ParseInt(parts[(int)MessageField.Address]);
                int subAddress = ParseInt(parts[(int)MessageField.SubAddress]);
                string analogValue = parts[(int)MessageField.Text].Trim();
                string extension = parts[(int)MessageField.Extension];

                string deviceType = GetDeviceType(parts[(int)MessageField.DeviceType], moduleType, 0, 0);

                // SendAnalogValueToAMX(moduleNumber + AMX1_OFFSET, loopNum, address, subAddress, analogValue, extension, deviceType);

                LogMessage($"=== Device Analogue Data Response Sent to AMX From: {moduleNumber + AMX1_OFFSET} {loopNum} {address} : Analogue Value : {analogValue} Mode : {extension}");

                return $"ACK{SEPARATOR_CHAR}{moduleNumber}{SEPARATOR_CHAR}{messageID}";
            }

            private string HandleSpecialMessage(string[] parts, long moduleNumber, long messageID)
            {
                int loopNum = ParseInt(parts[(int)MessageField.LoopNum]);
                int address = ParseInt(parts[(int)MessageField.Address]);
                int subAddress = ParseInt(parts[(int)MessageField.SubAddress]);
                int onOff = ParseInt(parts[(int)MessageField.OnOff]);
                int inputType = ParseInt(parts[(int)MessageField.EventType]);
                string deviceText = parts[(int)MessageField.Text].Trim();
                int extension = ParseInt(parts[(int)MessageField.Extension]);

                string transferFile = GetCurrentTransferFile();

                // Handle different special message types based on extension
                switch (extension)
                {
                    case 1: // evxRST1TO14
                        for (int n = 1; n <= 14; n++)
                        {
                            long eventNumber = MakeInputNumber(moduleNumber + AMX1_OFFSET, loopNum, address, n);
                            WriteNWMData(transferFile, 1, eventNumber, 0, deviceText, "", "", false);
                        }
                        break;

                    case 2: // evxRST0TO14
                        for (int n = 0; n <= 14; n++)
                        {
                            long eventNumber = MakeInputNumber(moduleNumber + AMX1_OFFSET, loopNum, address, n);
                            WriteNWMData(transferFile, 1, eventNumber, 0, deviceText, "", "", false);
                        }
                        break;

                    case 3: // evxRST0TO14NOT4
                        for (int n = 0; n <= 14; n++)
                        {
                            if (n != 4)
                            {
                                long eventNumber = MakeInputNumber(moduleNumber + AMX1_OFFSET, loopNum, address, n);
                                WriteNWMData(transferFile, 1, eventNumber, 0, deviceText, "", "", false);
                            }
                        }
                        break;

                    case 4: // evxRST0TO15
                        for (int n = 0; n <= 15; n++)
                        {
                            long eventNumber = MakeInputNumber(moduleNumber + AMX1_OFFSET, loopNum, address, n);
                            WriteNWMData(transferFile, 1, eventNumber, 0, deviceText, "", "", false);
                        }
                        break;
                }

                if (GetQueuedData() < 5)
                {
                    FlushAMX1Messages();
                }

                return $"ACK{SEPARATOR_CHAR}{moduleNumber}{SEPARATOR_CHAR}{messageID}";
            }

            private string HandleAck(long moduleNumber, long messageID)
            {
                System.Diagnostics.Debug.WriteLine("Received ACK");
                // RSMStats.IncreaseStat(RSMst.Acks);

                // CmdQ.Remove(messageID);
                // RSM.WaitingAck[moduleNumber] = false;
                // CmdQ.SendNextCommandDummy(moduleNumber);

                return "";
            }

            private string HandleNak(long moduleNumber)
            {
                System.Diagnostics.Debug.WriteLine("Received NAK");

                // RSM.WaitingAck[moduleNumber] = false;
                // CmdQ.SendNextCommandDummy(moduleNumber);

                return "";
            }

            private string HandleUnknownMessage(long moduleNumber, long messageID, string message)
            {
                System.Diagnostics.Debug.WriteLine("Received ???");
                // RSMStats.IncreaseStat(RSMst.UnknownAcksTX);

                System.Diagnostics.Debug.WriteLine($"Unknown message: {message.Substring(0, Math.Min(12, message.Length))}");

                // RSM.WaitingAck[moduleNumber] = false;
                // CmdQ.SendNextCommandDummy(moduleNumber);

                return $"ACK{SEPARATOR_CHAR}{messageID:D5}";
            }

            // Helper methods - These need to be implemented based on your system

            private void UpdateRXTracking(long moduleNumber, string ipAddress, long portNumber)
            {
                // RSM.LastRXTime[moduleNumber] = DateTime.Now;
                // RSM.ReportedAddress[moduleNumber] = ipAddress;
                // RSM.LastKnownIP[moduleNumber] = ipAddress;
                // RSM.RXmessagesIncrement(moduleNumber);
                // RSM.RequestPort[moduleNumber] = portNumber;
            }

            private void CheckLicenseOverrides(ref int loopNum, ref int address, ref int subAddress, ref int onOff, ref int inputType, ref string deviceText, long moduleNumber)
            {
                // Implement license checking logic
                // Check RSM.CurrentLicenseStatus and RSM.NodeInUse
            }

            private void AddStatusText(ref string deviceText, ref string deviceType, int loopNum, int address, int inputType, string moduleType, int onOff, long moduleNumber)
            {
                if (loopNum == 0 && inputType == 15)
                {
                    if (moduleType == "AD")
                    {
                        if (address == 253)
                        {
                            deviceType = deviceText;
                            deviceText = "RSM Module Startup";
                            System.Diagnostics.Debug.WriteLine($"{DateTime.Now:HH:mm:ss}  ============================ MODULE RESTART =================================");
                            LogMessage($"Module Restart!! - {moduleNumber}");
                        }
                        else if (address == 252)
                        {
                            deviceText = onOff != 0 ? "Engineer Present" : "Engineer No Longer Present";
                        }
                    }
                    else
                    {
                        switch (address)
                        {
                            case 1: deviceText = "Internal Buzzer Muted"; break;
                            case 2: deviceText = "Alarms Silenced"; break;
                            case 3: deviceText = "General Disablement"; break;
                            case 4: deviceText = "Panel in Fire"; break;
                            case 5: deviceText = "Panel in Fault"; break;
                            case 6: deviceText = "Panel in Pre-alarm"; break;
                            case 7: deviceText = "Panel in Test Mode"; break;
                            case 8: deviceText = "Panel in Delay Mode Period"; break;
                            case 9: deviceText = "Master Panel RS232 Comms Lost"; break;
                            case 10:
                                deviceType = deviceText;
                                deviceText = "RSM Module Startup";
                                System.Diagnostics.Debug.WriteLine($"{DateTime.Now:HH:mm:ss}  ============================ MODULE RESTART =================================");
                                LogMessage($"Module Restart!! - {moduleNumber}");
                                break;
                            case 252:
                                deviceText = onOff != 0 ? "Engineer Present" : "Engineer No Longer Present";
                                break;
                        }
                    }
                }
            }

            private string GetZoneText(string deviceText, string moduleType, long moduleNumber, int zone, int extension, int extension2, int inputType, int address)
            {
                string zoneText = "";

                if (moduleType == "DX" || moduleType == "PE")
                {
                    if (deviceText.Length > 20 && extension == 255 && extension2 == 100)
                    {
                        zoneText = MakeStringSafe(deviceText.Substring(20));
                        deviceText = MakeStringSafe(deviceText.Substring(0, 20).Trim());
                    }

                    if (inputType == 15 && address == 200)
                    {
                        zoneText = "Contact Your Fire Alarm Maintainer";
                    }
                }
                else if (moduleType == "AD")
                {
                    zoneText = GetZoneTextFromDatabase(moduleNumber, zone).Substring(0, Math.Min(40, zoneText.Length));
                    zoneText = MakeStringSafe(zoneText);
                    if (zoneText == "099 - ")
                    {
                        zoneText = "Zone-99";
                    }
                }
                else
                {
                    zoneText = GetZoneTextFromDatabase(moduleNumber, zone).Substring(0, Math.Min(40, zoneText.Length));
                    zoneText = MakeStringSafe(zoneText);
                }

                return zoneText;
            }

            private string GetDeviceType(string deviceTypeField, string moduleType, int inputType, int extension2)
            {
                if (deviceTypeField.StartsWith("$"))
                {
                    return deviceTypeField.Substring(1);
                }

                // Implement device type lookup based on module type
                return "Unknown";
            }

            private string MakeStringSafe(string input)
            {
                // Remove or replace unsafe characters
                return input?.Replace("\0", "").Trim() ?? "";
            }

            private long MakeInputNumber(long node, int loop, int address, int inputType)
            {
                // Combine node, loop, address, and type into a single event number
                return ((node & 0xFF) << 24) | ((loop & 0xFF) << 16) | ((address & 0xFF) << 8) | (inputType & 0xFF);
            }

            private string GetReferenceString(long eventNumber)
            {
                // Format event number as reference string
                return $"EVT#{eventNumber:X8}";
            }

            private string GetZoneTextFromDatabase(long moduleNumber, int zone)
            {
                // Retrieve zone text from database/file
                return "";
            }

            private string GetCurrentTransferFile()
            {
                return "transfer.dat";
            }

            private void WriteNWMData(string file, int listType, long eventNumber, long param, string deviceText, string deviceType, string zoneText, bool isOn)
            {
                // Write event data to file/database
            }

            private void NwmForceEvmAttribute(string file, long eventNumber, int attribute, int value)
            {
                // Force event attribute
            }

            private int GetQueuedData()
            {
                return 0;
            }

            private void FlushAMX1Messages()
            {
                // Flush queued messages to AMX
            }

            private void LogMessage(string message)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            private int ParseInt(string value)
            {
                return int.TryParse(value, out int result) ? result : 0;
            }

            private long ParseLong(string value)
            {
                return long.TryParse(value, out long result) ? result : 0;
            }
        }
    }
}