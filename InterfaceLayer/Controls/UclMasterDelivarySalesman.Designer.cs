namespace EcoMart.InterfaceLayer
{
    partial class UclMasterDelivarySalesman
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMailId = new System.Windows.Forms.TextBox();
            this.txtDocAddress2 = new System.Windows.Forms.TextBox();
            this.txtDocAddress1 = new System.Windows.Forms.TextBox();
            this.txtName = new EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.txtMobileNumberForSMS = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.txtTelephone = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblEmailID = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblTelephone = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblName = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel1);
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.panel1, 0);
            // 
            // ctrlUclSaleSummaryControl
            // 
            this.ctrlUclSaleSummaryControl.Size = new System.Drawing.Size(848, 347);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtMailId);
            this.panel1.Controls.Add(this.txtDocAddress2);
            this.panel1.Controls.Add(this.txtDocAddress1);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.txtMobileNumberForSMS);
            this.panel1.Controls.Add(this.txtTelephone);
            this.panel1.Controls.Add(this.psLabel3);
            this.panel1.Controls.Add(this.lblEmailID);
            this.panel1.Controls.Add(this.lblTelephone);
            this.panel1.Controls.Add(this.psLabel2);
            this.panel1.Controls.Add(this.psLabel1);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(126, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 223);
            this.panel1.TabIndex = 1027;
            // 
            // txtMailId
            // 
            this.txtMailId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMailId.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMailId.Location = new System.Drawing.Point(172, 160);
            this.txtMailId.MaxLength = 50;
            this.txtMailId.Name = "txtMailId";
            this.txtMailId.Size = new System.Drawing.Size(435, 22);
            this.txtMailId.TabIndex = 1123;
            this.txtMailId.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMailId_KeyUp);
            // 
            // txtDocAddress2
            // 
            this.txtDocAddress2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDocAddress2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocAddress2.Location = new System.Drawing.Point(172, 70);
            this.txtDocAddress2.MaxLength = 50;
            this.txtDocAddress2.Name = "txtDocAddress2";
            this.txtDocAddress2.Size = new System.Drawing.Size(435, 22);
            this.txtDocAddress2.TabIndex = 1122;
            this.txtDocAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocAddress2_KeyDown);
            this.txtDocAddress2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDocAddress2_KeyUp);
            // 
            // txtDocAddress1
            // 
            this.txtDocAddress1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDocAddress1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocAddress1.Location = new System.Drawing.Point(172, 42);
            this.txtDocAddress1.MaxLength = 50;
            this.txtDocAddress1.Name = "txtDocAddress1";
            this.txtDocAddress1.Size = new System.Drawing.Size(435, 22);
            this.txtDocAddress1.TabIndex = 1121;
            this.txtDocAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocAddress1_KeyDown);
            this.txtDocAddress1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDocAddress1_KeyUp);
            // 
            // txtName
            // 
            this.txtName.AlphabeticalList = false;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.ColumnWidth = null;
            this.txtName.DataSource = null;
            this.txtName.DisplayColumnNo = 1;
            this.txtName.DropDownHeight = 200;
            this.txtName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(172, 9);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = false;
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(435, 22);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 1120;
            this.txtName.TextMaxLenght = 32767;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            this.txtName.EnterKeyPressed += new System.EventHandler(this.txtName_EnterKeyPressed);
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // txtMobileNumberForSMS
            // 
            this.txtMobileNumberForSMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobileNumberForSMS.IsNumericDataSet = true;
            this.txtMobileNumberForSMS.Location = new System.Drawing.Point(172, 130);
            this.txtMobileNumberForSMS.MaxLength = 30;
            this.txtMobileNumberForSMS.Name = "txtMobileNumberForSMS";
            this.txtMobileNumberForSMS.Size = new System.Drawing.Size(435, 22);
            this.txtMobileNumberForSMS.TabIndex = 1119;
            this.txtMobileNumberForSMS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMobileNumberForSMS_KeyDown);
            this.txtMobileNumberForSMS.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMobileNumberForSMS_KeyUp);
            // 
            // txtTelephone
            // 
            this.txtTelephone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelephone.IsNumericDataSet = true;
            this.txtTelephone.Location = new System.Drawing.Point(172, 102);
            this.txtTelephone.MaxLength = 30;
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(435, 22);
            this.txtTelephone.TabIndex = 1113;
            this.txtTelephone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelephone_KeyDown);
            this.txtTelephone.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTelephone_KeyUp);
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(7, 133);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(154, 16);
            this.psLabel3.TabIndex = 1112;
            this.psLabel3.Text = "Mobile Number For SMS";
            // 
            // lblEmailID
            // 
            this.lblEmailID.AutoSize = true;
            this.lblEmailID.Location = new System.Drawing.Point(92, 163);
            this.lblEmailID.Name = "lblEmailID";
            this.lblEmailID.Size = new System.Drawing.Size(58, 16);
            this.lblEmailID.TabIndex = 1111;
            this.lblEmailID.Text = "EMail &Id";
            // 
            // lblTelephone
            // 
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Location = new System.Drawing.Point(92, 105);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(71, 16);
            this.lblTelephone.TabIndex = 1110;
            this.lblTelephone.Text = "&Telephone";
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(92, 75);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(69, 16);
            this.psLabel2.TabIndex = 62;
            this.psLabel2.Text = "Address 2";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(92, 45);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(69, 16);
            this.psLabel1.TabIndex = 60;
            this.psLabel1.Text = "Address 1";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(42, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(119, 16);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Delivary SalesMan";
            // 
            // UclMasterDelivarySalesman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclMasterDelivarySalesman";
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMailId;
        private System.Windows.Forms.TextBox txtDocAddress2;
        private System.Windows.Forms.TextBox txtDocAddress1;
        private CommonControls.PSAutoSuggestTextBox txtName;
        private CommonControls.PSTextBox txtMobileNumberForSMS;
        private CommonControls.PSTextBox txtTelephone;
        private CommonControls.PSLabel psLabel3;
        private CommonControls.PSLabel lblEmailID;
        private CommonControls.PSLabel lblTelephone;
        private CommonControls.PSLabel psLabel2;
        private CommonControls.PSLabel psLabel1;
        private CommonControls.PSLabel lblName;
    }
}
