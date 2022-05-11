namespace EcoMart.InterfaceLayer
{
    partial class UclSettingsEmail
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblEmailID = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblEmailPassword = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblEmailType = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtEmailID = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.txtEmailPassword = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.cbEmailType = new System.Windows.Forms.ComboBox();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lnkLessSecureApp = new System.Windows.Forms.LinkLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(831, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 511);
            this.MMBottomPanel.Size = new System.Drawing.Size(833, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.lnkLessSecureApp);
            this.MMMainPanel.Controls.Add(this.psLabel1);
            this.MMMainPanel.Controls.Add(this.cbEmailType);
            this.MMMainPanel.Controls.Add(this.txtEmailPassword);
            this.MMMainPanel.Controls.Add(this.txtEmailID);
            this.MMMainPanel.Controls.Add(this.lblEmailType);
            this.MMMainPanel.Controls.Add(this.lblEmailPassword);
            this.MMMainPanel.Controls.Add(this.lblEmailID);
            this.MMMainPanel.Size = new System.Drawing.Size(833, 459);
            this.MMMainPanel.Controls.SetChildIndex(this.lblEmailID, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.lblEmailPassword, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.lblEmailType, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtEmailID, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtEmailPassword, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.cbEmailType, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.psLabel1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.lnkLessSecureApp, 0);
            // 
            // lblEmailID
            // 
            this.lblEmailID.AutoSize = true;
            this.lblEmailID.Location = new System.Drawing.Point(243, 128);
            this.lblEmailID.Name = "lblEmailID";
            this.lblEmailID.Size = new System.Drawing.Size(54, 14);
            this.lblEmailID.TabIndex = 1;
            this.lblEmailID.Text = "Email ID";
            // 
            // lblEmailPassword
            // 
            this.lblEmailPassword.AutoSize = true;
            this.lblEmailPassword.Location = new System.Drawing.Point(234, 184);
            this.lblEmailPassword.Name = "lblEmailPassword";
            this.lblEmailPassword.Size = new System.Drawing.Size(62, 14);
            this.lblEmailPassword.TabIndex = 2;
            this.lblEmailPassword.Text = "Password";
            // 
            // lblEmailType
            // 
            this.lblEmailType.AutoSize = true;
            this.lblEmailType.Location = new System.Drawing.Point(224, 251);
            this.lblEmailType.Name = "lblEmailType";
            this.lblEmailType.Size = new System.Drawing.Size(69, 14);
            this.lblEmailType.TabIndex = 3;
            this.lblEmailType.Text = "Email Type";
            // 
            // txtEmailID
            // 
            this.txtEmailID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailID.Location = new System.Drawing.Point(335, 128);
            this.txtEmailID.Name = "txtEmailID";
            this.txtEmailID.Size = new System.Drawing.Size(453, 22);
            this.txtEmailID.TabIndex = 4;
            // 
            // txtEmailPassword
            // 
            this.txtEmailPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailPassword.Location = new System.Drawing.Point(335, 181);
            this.txtEmailPassword.Name = "txtEmailPassword";
            this.txtEmailPassword.Size = new System.Drawing.Size(453, 22);
            this.txtEmailPassword.TabIndex = 5;
            // 
            // cbEmailType
            // 
            this.cbEmailType.AllowDrop = true;
            this.cbEmailType.DisplayMember = "0";
            this.cbEmailType.Enabled = false;
            this.cbEmailType.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEmailType.FormattingEnabled = true;
            this.cbEmailType.Location = new System.Drawing.Point(335, 248);
            this.cbEmailType.Name = "cbEmailType";
            this.cbEmailType.Size = new System.Drawing.Size(190, 26);
            this.cbEmailType.TabIndex = 1093;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(333, 278);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(309, 14);
            this.psLabel1.TabIndex = 1094;
            this.psLabel1.Text = "Warning: Setting \'Allow Less Secure App\' should be ON.";
            // 
            // lnkLessSecureApp
            // 
            this.lnkLessSecureApp.AutoSize = true;
            this.lnkLessSecureApp.Location = new System.Drawing.Point(335, 296);
            this.lnkLessSecureApp.Name = "lnkLessSecureApp";
            this.lnkLessSecureApp.Size = new System.Drawing.Size(285, 13);
            this.lnkLessSecureApp.TabIndex = 1095;
            this.lnkLessSecureApp.TabStop = true;
            this.lnkLessSecureApp.Text = "https://www.google.com/settings/security/lesssecureapps";
            this.lnkLessSecureApp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLessSecureApp_LinkClicked);
            // 
            // UclSettingsEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclSettingsEmail";
            this.Size = new System.Drawing.Size(833, 534);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblEmailID;
        private EcoMart.InterfaceLayer.CommonControls.PSTextBox txtEmailPassword;
        private EcoMart.InterfaceLayer.CommonControls.PSTextBox txtEmailID;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblEmailType;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblEmailPassword;
        private System.Windows.Forms.ComboBox cbEmailType;
        private CommonControls.PSLabel psLabel1;
        private System.Windows.Forms.LinkLabel lnkLessSecureApp;
    }
}
