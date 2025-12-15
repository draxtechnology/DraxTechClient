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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtData = new System.Windows.Forms.RichTextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // btnStart
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 30);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Listener";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);

            // btnStop
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(118, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 30);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop Listener";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);

            // btnClear
            this.btnClear.Location = new System.Drawing.Point(224, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear Data";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 50);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(150, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status: Not listening";

            // txtData
            this.txtData.Location = new System.Drawing.Point(12, 75);
            this.txtData.Name = "txtData";
            this.txtData.ReadOnly = true;
            this.txtData.Size = new System.Drawing.Size(760, 363);
            this.txtData.TabIndex = 4;
            this.txtData.Text = "";
            this.txtData.Font = new System.Drawing.Font("Consolas", 9F);

            // TcpListenerForm
            this.ClientSize = new System.Drawing.Size(784, 450);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "TcpListenerForm";
            this.Text = "TCP Listener - Port 1471";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TcpListenerForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
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
                        string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        AppendData($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Received ({bytesRead} bytes): {data}\n");
                    }
                }
            }
            catch (Exception ex)
            {
                if (!token.IsCancellationRequested)
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

        private void TcpListenerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListener();
        }
    }
}