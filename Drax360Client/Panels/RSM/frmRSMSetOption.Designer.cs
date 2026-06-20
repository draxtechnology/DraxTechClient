namespace DraxClient.Panels.RSM
{
    partial class frmRSMSetOption
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblOptionCaption  = new Label();
            lblOption         = new Label();
            lblCurrentCaption = new Label();
            lblCurrent        = new Label();
            lblNewCaption     = new Label();
            tbNew             = new TextBox();
            btOK              = new Button();
            btCancel          = new Button();
            SuspendLayout();

            lblOptionCaption.Text     = "Option:";
            lblOptionCaption.Location = new Point(12, 15);
            lblOptionCaption.Size     = new Size(80, 20);

            lblOption.Location = new Point(98, 15);
            lblOption.Size     = new Size(274, 20);
            lblOption.Font     = new Font(Font, FontStyle.Bold);

            lblCurrentCaption.Text     = "Current:";
            lblCurrentCaption.Location = new Point(12, 45);
            lblCurrentCaption.Size     = new Size(80, 20);

            lblCurrent.Location = new Point(98, 45);
            lblCurrent.Size     = new Size(274, 20);

            lblNewCaption.Text     = "New value:";
            lblNewCaption.Location = new Point(12, 78);
            lblNewCaption.Size     = new Size(80, 20);

            tbNew.Location = new Point(98, 75);
            tbNew.Size     = new Size(274, 23);

            btOK.Text     = "Set";
            btOK.Location = new Point(200, 115);
            btOK.Size     = new Size(80, 28);
            btOK.Click   += btOK_Click;

            btCancel.Text         = "Cancel";
            btCancel.Location     = new Point(292, 115);
            btCancel.Size         = new Size(80, 28);
            btCancel.DialogResult = DialogResult.Cancel;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(388, 156);
            FormBorderStyle     = FormBorderStyle.FixedDialog;
            MaximizeBox         = false;
            MinimizeBox         = false;
            StartPosition       = FormStartPosition.CenterParent;
            Text                = "Set a Node Option";
            AcceptButton        = btOK;
            CancelButton        = btCancel;
            Controls.AddRange(new Control[] {
                lblOptionCaption, lblOption, lblCurrentCaption, lblCurrent,
                lblNewCaption, tbNew, btOK, btCancel
            });
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblOptionCaption, lblOption, lblCurrentCaption, lblCurrent, lblNewCaption;
        private TextBox tbNew;
        private Button btOK, btCancel;
    }
}
