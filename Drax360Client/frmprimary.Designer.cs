namespace DraxClient
{
    partial class frmprimary
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmprimary));
            btevacuate = new Button();
            btReset = new Button();
            lblversion = new Label();
            lblstatus = new Label();
            btSilence = new Button();
            btDisableDevice = new Button();
            btEnableDevice = new Button();
            tbNode = new TextBox();
            tbLoop = new TextBox();
            tbIP = new TextBox();
            lbNode = new Label();
            label1 = new Label();
            label2 = new Label();
            tbZone = new TextBox();
            label3 = new Label();
            panel1 = new Panel();
            btDisableZone = new Button();
            btEnableZone = new Button();
            btAlert = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            quitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem1 = new ToolStripMenuItem();
            btnrestartservice = new Button();
            btAnalogue = new Button();
            btMute = new Button();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btevacuate
            // 
            btevacuate.Location = new Point(19, 40);
            btevacuate.Margin = new Padding(3, 2, 3, 2);
            btevacuate.Name = "btevacuate";
            btevacuate.Size = new Size(206, 74);
            btevacuate.TabIndex = 0;
            btevacuate.Text = "Evacuate";
            btevacuate.UseVisualStyleBackColor = true;
            btevacuate.Click += btevacuate_Click;
            // 
            // btReset
            // 
            btReset.Location = new Point(19, 148);
            btReset.Margin = new Padding(3, 2, 3, 2);
            btReset.Name = "btReset";
            btReset.Size = new Size(206, 74);
            btReset.TabIndex = 1;
            btReset.Text = "Reset";
            btReset.UseVisualStyleBackColor = true;
            btReset.Click += btReset_Click;
            // 
            // lblversion
            // 
            lblversion.AutoSize = true;
            lblversion.Location = new Point(950, 380);
            lblversion.Name = "lblversion";
            lblversion.Size = new Size(27, 15);
            lblversion.TabIndex = 2;
            lblversion.Text = "xxxx";
            lblversion.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblstatus
            // 
            lblstatus.AutoSize = true;
            lblstatus.Location = new Point(10, 380);
            lblstatus.Name = "lblstatus";
            lblstatus.Size = new Size(52, 15);
            lblstatus.TabIndex = 2;
            lblstatus.Text = "Running";
            // 
            // btSilence
            // 
            btSilence.Location = new Point(263, 40);
            btSilence.Margin = new Padding(3, 2, 3, 2);
            btSilence.Name = "btSilence";
            btSilence.Size = new Size(206, 74);
            btSilence.TabIndex = 3;
            btSilence.Text = "Silence";
            btSilence.UseVisualStyleBackColor = true;
            btSilence.Click += btSilence_Click;
            // 
            // btDisableDevice
            // 
            btDisableDevice.Location = new Point(523, 141);
            btDisableDevice.Margin = new Padding(3, 2, 3, 2);
            btDisableDevice.Name = "btDisableDevice";
            btDisableDevice.Size = new Size(206, 41);
            btDisableDevice.TabIndex = 4;
            btDisableDevice.Text = "Disable Device";
            btDisableDevice.UseVisualStyleBackColor = true;
            btDisableDevice.Click += btDisableDevice_Click;
            // 
            // btEnableDevice
            // 
            btEnableDevice.Location = new Point(523, 187);
            btEnableDevice.Margin = new Padding(3, 2, 3, 2);
            btEnableDevice.Name = "btEnableDevice";
            btEnableDevice.Size = new Size(206, 41);
            btEnableDevice.TabIndex = 5;
            btEnableDevice.Text = "Enable Device";
            btEnableDevice.UseVisualStyleBackColor = true;
            btEnableDevice.Click += btEnableDevice_Click;
            // 
            // tbNode
            // 
            tbNode.Location = new Point(60, 7);
            tbNode.Margin = new Padding(3, 2, 3, 2);
            tbNode.Name = "tbNode";
            tbNode.Size = new Size(43, 23);
            tbNode.TabIndex = 6;
            tbNode.Text = "1";
            // 
            // tbLoop
            // 
            tbLoop.Location = new Point(176, 7);
            tbLoop.Margin = new Padding(3, 2, 3, 2);
            tbLoop.Name = "tbLoop";
            tbLoop.Size = new Size(41, 23);
            tbLoop.TabIndex = 7;
            tbLoop.Text = "1";
            // 
            // tbIP
            // 
            tbIP.Location = new Point(176, 40);
            tbIP.Margin = new Padding(3, 2, 3, 2);
            tbIP.Name = "tbIP";
            tbIP.Size = new Size(41, 23);
            tbIP.TabIndex = 8;
            tbIP.Text = "1";
            // 
            // lbNode
            // 
            lbNode.AutoSize = true;
            lbNode.Location = new Point(13, 12);
            lbNode.Name = "lbNode";
            lbNode.Size = new Size(36, 15);
            lbNode.TabIndex = 9;
            lbNode.Text = "Node";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(115, 12);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 10;
            label1.Text = "Loop";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(115, 42);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 11;
            label2.Text = "Device";
            // 
            // tbZone
            // 
            tbZone.Location = new Point(60, 40);
            tbZone.Margin = new Padding(3, 2, 3, 2);
            tbZone.Name = "tbZone";
            tbZone.Size = new Size(43, 23);
            tbZone.TabIndex = 12;
            tbZone.Text = "1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 42);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 13;
            label3.Text = "Zone";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(tbZone);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbNode);
            panel1.Controls.Add(tbIP);
            panel1.Controls.Add(tbLoop);
            panel1.Controls.Add(tbNode);
            panel1.Location = new Point(263, 154);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(240, 68);
            panel1.TabIndex = 14;
            // 
            // btDisableZone
            // 
            btDisableZone.Location = new Point(523, 233);
            btDisableZone.Margin = new Padding(3, 2, 3, 2);
            btDisableZone.Name = "btDisableZone";
            btDisableZone.Size = new Size(206, 41);
            btDisableZone.TabIndex = 15;
            btDisableZone.Text = "Disable Zone";
            btDisableZone.UseVisualStyleBackColor = true;
            btDisableZone.Click += btDisableZone_Click;
            // 
            // btEnableZone
            // 
            btEnableZone.Location = new Point(523, 278);
            btEnableZone.Margin = new Padding(3, 2, 3, 2);
            btEnableZone.Name = "btEnableZone";
            btEnableZone.Size = new Size(206, 41);
            btEnableZone.TabIndex = 16;
            btEnableZone.Text = "Enable Zone";
            btEnableZone.UseVisualStyleBackColor = true;
            btEnableZone.Click += btEnableZone_Click;
            // 
            // btAlert
            // 
            btAlert.Location = new Point(523, 40);
            btAlert.Margin = new Padding(3, 2, 3, 2);
            btAlert.Name = "btAlert";
            btAlert.Size = new Size(206, 74);
            btAlert.TabIndex = 17;
            btAlert.Text = "Alert";
            btAlert.UseVisualStyleBackColor = true;
            btAlert.Click += btAlert_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(1032, 24);
            menuStrip1.TabIndex = 18;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripSeparator1, quitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(131, 6);
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            quitToolStripMenuItem.Size = new Size(134, 22);
            quitToolStripMenuItem.Text = "Exit";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem1 });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            aboutToolStripMenuItem1.Size = new Size(214, 22);
            aboutToolStripMenuItem1.Text = "About Network Manager...";
            aboutToolStripMenuItem1.Click += aboutToolStripMenuItem1_Click;
            // 
            // btnrestartservice
            // 
            btnrestartservice.Location = new Point(776, 40);
            btnrestartservice.Margin = new Padding(3, 2, 3, 2);
            btnrestartservice.Name = "btnrestartservice";
            btnrestartservice.Size = new Size(206, 74);
            btnrestartservice.TabIndex = 19;
            btnrestartservice.Text = "Restart Service";
            btnrestartservice.UseVisualStyleBackColor = true;
            btnrestartservice.Click += btnrestartservice_Click;
            // 
            // btAnalogue
            // 
            btAnalogue.Location = new Point(776, 154);
            btAnalogue.Margin = new Padding(3, 2, 3, 2);
            btAnalogue.Name = "btAnalogue";
            btAnalogue.Size = new Size(206, 68);
            btAnalogue.TabIndex = 20;
            btAnalogue.Text = "Get Analogue Values";
            btAnalogue.UseVisualStyleBackColor = true;
            btAnalogue.Click += btAnalogue_Click;
            // 
            // btMute
            // 
            btMute.Location = new Point(19, 245);
            btMute.Margin = new Padding(3, 2, 3, 2);
            btMute.Name = "btMute";
            btMute.Size = new Size(206, 74);
            btMute.TabIndex = 21;
            btMute.Text = "Mute";
            btMute.UseVisualStyleBackColor = true;
            btMute.Click += btMute_Click;
            // 
            // frmprimary
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1032, 402);
            Controls.Add(btMute);
            Controls.Add(btAnalogue);
            Controls.Add(btnrestartservice);
            Controls.Add(btAlert);
            Controls.Add(btEnableZone);
            Controls.Add(btDisableZone);
            Controls.Add(panel1);
            Controls.Add(btEnableDevice);
            Controls.Add(btDisableDevice);
            Controls.Add(btSilence);
            Controls.Add(lblstatus);
            Controls.Add(lblversion);
            Controls.Add(btReset);
            Controls.Add(btevacuate);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmprimary";
            Text = "Drax Technology";
            FormClosing += frmprimary_FormClosing;
            Load += frmprimary_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btevacuate;
        private Button btReset;
        private Label lblversion;
        private Label lblstatus;
        private Button btSilence;
        private Button btDisableDevice;
        private Button btEnableDevice;
        private TextBox tbNode;
        private TextBox tbLoop;
        private TextBox tbIP;
        private Label lbNode;
        private Label label1;
        private Label label2;
        private TextBox tbZone;
        private Label label3;
        private Panel panel1;
        private Button btDisableZone;
        private Button btEnableZone;
        private Button btAlert;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private Button btnrestartservice;
        private Button btAnalogue;
        private Button btMute;
    }
}
