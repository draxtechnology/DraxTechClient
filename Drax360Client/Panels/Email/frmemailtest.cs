using CryptoModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraxClient.Panels.Email
{
    public partial class frmemailtest : Form
    {
        const string kpipenamesend = "DraxTechnologyPipeSend";
        const char kpipedelim = '|';

        private void LoadFormData()
        {
            this.textBox1.Text = sendcmd("SETTINGSGET|EMAIL,TESTMESSAGE");
        }
        private void btsend_Click(object sender, EventArgs e)
        {
            string smtpserver = sendcmd("SETTINGSGET|EMAIL,SMTPSERVER");
            string smtpuser = sendcmd("SETTINGSGET|EMAIL,LOGINNAME");
            string smtppassword = sendcmd("SETTINGSGET|EMAIL,PASSWORD");
            string decryptedpassword = AesDecryptor.DecryptOpenSSLCtr(smtppassword, "");
            string result = sendcmd("SETTINGSGET|EMAIL,SMTPAUTHORISATION");
            bool cbauthorisation = result == "1" || result == "True";

            //string smtppassword = "Y(531132842940ax";
            int smtpport = Convert.ToInt32(sendcmd("SETTINGSGET|EMAIL,SMTPPORT"));

            try
            {
                SmtpClient mySmtpClient = new SmtpClient(smtpserver, smtpport);
                mySmtpClient.EnableSsl = true;

                // Optional but often needed:
                mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mySmtpClient.UseDefaultCredentials = false;

                // set smtp-client with basicAuthentication
                mySmtpClient.UseDefaultCredentials = false;
                if (cbauthorisation)
                {
                    System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(smtpuser, decryptedpassword);
                    mySmtpClient.Credentials = basicAuthenticationInfo;
                }

                // add from,to mailaddresses
                MailAddress from = new MailAddress(smtpuser, "Email Test");
                MailAddress to = new MailAddress("mike.holmes@draxtechnology.com", "Mike Holmes");
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                // add ReplyTo
                MailAddress replyTo = new MailAddress("mike.holmes@draxtechnology.com");
                myMail.ReplyToList.Add(replyTo);

                // set subject and encoding
                myMail.Subject = this.textBox1.Text;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = "<b><p style=\"font-size:36px;width:20px;height:20px;background-color:#00FFFF;\">" + this.textBox1.Text + "</b></p><br>LOCATION: <br>NODE:<BR>REFERENCE: <br>DATE/TIME: " + DateTime.Now.ToShortTimeString() + " on " + DateTime.Now.ToShortDateString();
                myMail.Body += "<br><span style='color: blue;'>________________________________________________________________________<br>";
                myMail.Body += "<span style='color: blue;font-size: 8pt;'>This is an event email generated automatically by AMX1. Replies to this email will not be read.</span><br>";
                myMail.Body += "<span style='color: blue;font-size: 8pt;'>Please contact the AMX1 administrator if you wish to be removed from this email list.<br>";
                myMail.Body += "<span style='color: blue;font-size: 8pt;'>" + sendcmd("SETTINGSGET|EMAIL,FOOTER");
                myMail.BodyEncoding = System.Text.Encoding.UTF8;

                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
                sendcmd($"SETTINGSSET|EMAIL,TESTMESSAGE,{this.textBox1.Text.Replace(","," ")}");
                this.Close();
            }

            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string sendcmd(string cmd, string parameters = "")
        {
            string strcmd = cmd;
            if (!string.IsNullOrEmpty(parameters))
                strcmd += kpipedelim + parameters;
            try { return Task.Run(() => sendserver(strcmd)).Result; }
            catch (Exception ex) { return "Error: " + ex; }
        }

        private async Task<string> sendserver(string message)
        {
            using (NamedPipeClientStream pipe = new NamedPipeClientStream(".", kpipenamesend, PipeDirection.InOut))
            {
                pipe.Connect(5000);
                pipe.ReadMode = PipeTransmissionMode.Message;
                byte[] ba = Encoding.Default.GetBytes(message);
                pipe.Write(ba, 0, ba.Length);
                var result = await Task.Run(() => readmessage(pipe));
                string strresponse = Encoding.Default.GetString(result);
                Console.WriteLine("Response received from Send server: " + strresponse);
                return strresponse;
            }
        }

        private static byte[] readmessage(PipeStream pipe)
        {
            byte[] buffer = new byte[1024];
            using (var ms = new MemoryStream())
            {
                do { var rb = pipe.Read(buffer, 0, buffer.Length); ms.Write(buffer, 0, rb); }
                while (!pipe.IsMessageComplete);
                return ms.ToArray();
            }
        }

        private void btcancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
