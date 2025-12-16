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
        private Button button1;
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
            button1 = new Button();
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
            // button1
            // 
            button1.Location = new Point(419, 11);
            button1.Name = "button1";
            button1.Size = new Size(82, 31);
            button1.TabIndex = 5;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // TcpListenerForm
            // 
            ClientSize = new Size(784, 450);
            Controls.Add(button1);
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
            AppendData($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TCP Listener stopped\n");
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

            // Only accept connections from 192.168.3.1
            if (clientIP != "192.168.3.1")
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


        private void button1_Click(object sender, EventArgs e)
        {

            //string ackResponse = "ACK;1;3";
            string ackResponse = "ACK\u00C71\u00C73";

            byte[] ackBytes = ScrambleAndEncodeMessage(ackResponse);

            string mike = "x";

        }
    }
}