namespace Drax360Client
{
    partial class frmamxtest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <sum
        /// mary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnfakeisolation = new Button();
            btntestmessage = new Button();
            bttestmessageclean = new Button();
            btcleanisoloate = new Button();
            btcleanalarm = new Button();
            btcleanalarmoff = new Button();
            SuspendLayout();
            // 
            // btnfakeisolation
            // 
            btnfakeisolation.Location = new Point(81, 93);
            btnfakeisolation.Name = "btnfakeisolation";
            btnfakeisolation.Size = new Size(143, 74);
            btnfakeisolation.TabIndex = 0;
            btnfakeisolation.Text = "Fake Isolate";
            btnfakeisolation.UseVisualStyleBackColor = true;
            btnfakeisolation.Click += btnfakeisolation_Click;
            // 
            // btntestmessage
            // 
            btntestmessage.Location = new Point(283, 92);
            btntestmessage.Name = "btntestmessage";
            btntestmessage.Size = new Size(131, 77);
            btntestmessage.TabIndex = 1;
            btntestmessage.Text = "Test Message";
            btntestmessage.UseVisualStyleBackColor = true;
            btntestmessage.Click += btntestmessage_Click;
            // 
            // bttestmessageclean
            // 
            bttestmessageclean.Location = new Point(283, 191);
            bttestmessageclean.Name = "bttestmessageclean";
            bttestmessageclean.Size = new Size(131, 77);
            bttestmessageclean.TabIndex = 2;
            bttestmessageclean.Text = "Clean Test Message";
            bttestmessageclean.UseVisualStyleBackColor = true;
            bttestmessageclean.Click += bttestmessageclean_Click;
            // 
            // btcleanisoloate
            // 
            btcleanisoloate.Location = new Point(81, 191);
            btcleanisoloate.Name = "btcleanisoloate";
            btcleanisoloate.Size = new Size(143, 74);
            btcleanisoloate.TabIndex = 3;
            btcleanisoloate.Text = "Clean Fake Isolate";
            btcleanisoloate.UseVisualStyleBackColor = true;
            btcleanisoloate.Click += btcleanisoloate_Click;
            // 
            // btcleanalarm
            // 
            btcleanalarm.Location = new Point(81, 286);
            btcleanalarm.Name = "btcleanalarm";
            btcleanalarm.Size = new Size(131, 77);
            btcleanalarm.TabIndex = 4;
            btcleanalarm.Text = "Clean Alarm On";
            btcleanalarm.UseVisualStyleBackColor = true;
            btcleanalarm.Click += btcleanalarm_Click;
            // 
            // btcleanalarmoff
            // 
            btcleanalarmoff.Location = new Point(283, 286);
            btcleanalarmoff.Name = "btcleanalarmoff";
            btcleanalarmoff.Size = new Size(131, 77);
            btcleanalarmoff.TabIndex = 4;
            btcleanalarmoff.Text = "Clean Alarm Off";
            btcleanalarmoff.UseVisualStyleBackColor = true;
            btcleanalarmoff.Click += btcleanalarmoff_Click;
            // 
            // frmamxtest
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(499, 450);
            Controls.Add(btcleanalarmoff);
            Controls.Add(btcleanalarm);
            Controls.Add(btcleanisoloate);
            Controls.Add(bttestmessageclean);
            Controls.Add(btntestmessage);
            Controls.Add(btnfakeisolation);
            Name = "frmamxtest";
            Text = "frmamxtest";
            ResumeLayout(false);
        }

        #endregion

        private Button btnfakeisolation;
        private Button btntestmessage;
        private Button bttestmessageclean;
        private Button btcleanisoloate;
        private Button btcleanalarm;
        private Button btcleanalarmoff;
    }
}