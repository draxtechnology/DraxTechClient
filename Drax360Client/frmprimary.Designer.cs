namespace Drax360Client
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
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            quitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem1 = new ToolStripMenuItem();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btevacuate
            // 
            btevacuate.Location = new Point(22, 54);
            btevacuate.Name = "btevacuate";
            btevacuate.Size = new Size(236, 98);
            btevacuate.TabIndex = 0;
            btevacuate.Text = "Evacuate";
            btevacuate.UseVisualStyleBackColor = true;
            btevacuate.Click += btevacuate_Click;
            // 
            // btReset
            // 
            btReset.Location = new Point(22, 197);
            btReset.Name = "btReset";
            btReset.Size = new Size(236, 98);
            btReset.TabIndex = 1;
            btReset.Text = "Reset";
            btReset.UseVisualStyleBackColor = true;
            btReset.Click += btReset_Click;
            // 
            // lblversion
            // 
            lblversion.AutoSize = true;
            lblversion.Location = new Point(856, 430);
            lblversion.Name = "lblversion";
            lblversion.Size = new Size(37, 20);
            lblversion.TabIndex = 2;
            lblversion.Text = "xxxx";
            lblversion.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblstatus
            // 
            lblstatus.AutoSize = true;
            lblstatus.Location = new Point(12, 421);
            lblstatus.Name = "lblstatus";
            lblstatus.Size = new Size(63, 20);
            lblstatus.TabIndex = 2;
            lblstatus.Text = "Running";
            // 
            // btSilence
            // 
            btSilence.Location = new Point(301, 54);
            btSilence.Name = "btSilence";
            btSilence.Size = new Size(236, 98);
            btSilence.TabIndex = 3;
            btSilence.Text = "Silence";
            btSilence.UseVisualStyleBackColor = true;
            btSilence.Click += btSilence_Click;
            // 
            // btDisableDevice
            // 
            btDisableDevice.Location = new Point(598, 188);
            btDisableDevice.Name = "btDisableDevice";
            btDisableDevice.Size = new Size(236, 55);
            btDisableDevice.TabIndex = 4;
            btDisableDevice.Text = "Disable Device";
            btDisableDevice.UseVisualStyleBackColor = true;
            btDisableDevice.Click += btDisableDevice_Click;
            // 
            // btEnableDevice
            // 
            btEnableDevice.Location = new Point(598, 249);
            btEnableDevice.Name = "btEnableDevice";
            btEnableDevice.Size = new Size(236, 55);
            btEnableDevice.TabIndex = 5;
            btEnableDevice.Text = "Enable Device";
            btEnableDevice.UseVisualStyleBackColor = true;
            btEnableDevice.Click += btEnableDevice_Click;
            // 
            // tbNode
            // 
            tbNode.Location = new Point(68, 9);
            tbNode.Name = "tbNode";
            tbNode.Size = new Size(49, 27);
            tbNode.TabIndex = 6;
            tbNode.Text = "1";
            // 
            // tbLoop
            // 
            tbLoop.Location = new Point(201, 9);
            tbLoop.Name = "tbLoop";
            tbLoop.Size = new Size(46, 27);
            tbLoop.TabIndex = 7;
            tbLoop.Text = "1";
            // 
            // tbIP
            // 
            tbIP.Location = new Point(201, 53);
            tbIP.Name = "tbIP";
            tbIP.Size = new Size(46, 27);
            tbIP.TabIndex = 8;
            tbIP.Text = "1";
            // 
            // lbNode
            // 
            lbNode.AutoSize = true;
            lbNode.Location = new Point(15, 16);
            lbNode.Name = "lbNode";
            lbNode.Size = new Size(46, 20);
            lbNode.TabIndex = 9;
            lbNode.Text = "Node";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(131, 16);
            label1.Name = "label1";
            label1.Size = new Size(43, 20);
            label1.TabIndex = 10;
            label1.Text = "Loop";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(131, 56);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 11;
            label2.Text = "Device";
            // 
            // tbZone
            // 
            tbZone.Location = new Point(68, 53);
            tbZone.Name = "tbZone";
            tbZone.Size = new Size(49, 27);
            tbZone.TabIndex = 12;
            tbZone.Text = "1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 56);
            label3.Name = "label3";
            label3.Size = new Size(43, 20);
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
            panel1.Location = new Point(301, 205);
            panel1.Name = "panel1";
            panel1.Size = new Size(274, 90);
            panel1.TabIndex = 14;
            // 
            // btDisableZone
            // 
            btDisableZone.Location = new Point(598, 310);
            btDisableZone.Name = "btDisableZone";
            btDisableZone.Size = new Size(236, 55);
            btDisableZone.TabIndex = 15;
            btDisableZone.Text = "Disable Zone";
            btDisableZone.UseVisualStyleBackColor = true;
            btDisableZone.Click += btDisableZone_Click;
            // 
            // btEnableZone
            // 
            btEnableZone.Location = new Point(598, 371);
            btEnableZone.Name = "btEnableZone";
            btEnableZone.Size = new Size(236, 55);
            btEnableZone.TabIndex = 16;
            btEnableZone.Text = "Enable Zone";
            btEnableZone.UseVisualStyleBackColor = true;
            btEnableZone.Click += btEnableZone_Click;
            // 
            // btAlert
            // 
            btAlert.Location = new Point(587, 63);
            btAlert.Name = "btAlert";
            btAlert.Size = new Size(236, 98);
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
            menuStrip1.Size = new Size(933, 28);
            menuStrip1.TabIndex = 18;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripSeparator1, quitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(169, 26);
            toolStripMenuItem1.Text = "Test AMX";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(166, 6);
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            quitToolStripMenuItem.Size = new Size(169, 26);
            quitToolStripMenuItem.Text = "Exit";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem1 });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(55, 24);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            aboutToolStripMenuItem1.Size = new Size(265, 26);
            aboutToolStripMenuItem1.Text = "About Network Manager...";
            aboutToolStripMenuItem1.Click += aboutToolStripMenuItem1_Click;
            // 
            // frmprimary
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 472);
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
            Name = "frmprimary";
            Text = "Drax Technology";
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
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
    }
}
