namespace EcoMart.InterfaceLayer
{
    partial class UclContraEntry
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
            this.components = new System.ComponentModel.Container();
            this.pnlNameAddress = new System.Windows.Forms.Panel();
            this.txtAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbAccountTobeCredited = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbCreditor = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtNarration = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.pnlVou = new System.Windows.Forms.Panel();
            this.txtVoucherSeries = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.psLabel6 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtVouType = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.lblVouDate = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouNumber = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouType = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.datePickerBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtVouchernumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlNameAddress.SuspendLayout();
            this.pnlVou.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(973, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 568);
            this.MMBottomPanel.Size = new System.Drawing.Size(975, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlNameAddress);
            this.MMMainPanel.Size = new System.Drawing.Size(975, 516);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlNameAddress, 0);
            // 
            // pnlNameAddress
            // 
            this.pnlNameAddress.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlNameAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNameAddress.Controls.Add(this.pnlVou);
            this.pnlNameAddress.Controls.Add(this.txtAmount);
            this.pnlNameAddress.Controls.Add(this.psLabel2);
            this.pnlNameAddress.Controls.Add(this.psLabel1);
            this.pnlNameAddress.Controls.Add(this.mcbAccountTobeCredited);
            this.pnlNameAddress.Controls.Add(this.mPlbl4);
            this.pnlNameAddress.Controls.Add(this.mPlbl1);
            this.pnlNameAddress.Controls.Add(this.mcbCreditor);
            this.pnlNameAddress.Controls.Add(this.txtNarration);
            this.pnlNameAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNameAddress.Location = new System.Drawing.Point(0, 0);
            this.pnlNameAddress.Name = "pnlNameAddress";
            this.pnlNameAddress.Size = new System.Drawing.Size(973, 514);
            this.pnlNameAddress.TabIndex = 62;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAmount.Location = new System.Drawing.Point(207, 254);
            this.txtAmount.MaxLength = 15;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(162, 26);
            this.txtAmount.TabIndex = 1091;
            this.txtAmount.TabStop = false;
            this.txtAmount.Tag = "0.00";
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(136, 256);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(51, 14);
            this.psLabel2.TabIndex = 1090;
            this.psLabel2.Text = "Amount";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(51, 153);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(136, 14);
            this.psLabel1.TabIndex = 1089;
            this.psLabel1.Text = "Account To Be Credited";
            // 
            // mcbAccountTobeCredited
            // 
            this.mcbAccountTobeCredited.ColumnWidth = null;
            this.mcbAccountTobeCredited.DataSource = null;
            this.mcbAccountTobeCredited.DisplayColumnNo = 1;
            this.mcbAccountTobeCredited.DropDownHeight = 200;
            this.mcbAccountTobeCredited.Location = new System.Drawing.Point(207, 153);
            this.mcbAccountTobeCredited.Margin = new System.Windows.Forms.Padding(4);
            this.mcbAccountTobeCredited.Name = "mcbAccountTobeCredited";
            this.mcbAccountTobeCredited.SelectedID = null;
            this.mcbAccountTobeCredited.ShowNew = false;
            this.mcbAccountTobeCredited.Size = new System.Drawing.Size(421, 22);
            this.mcbAccountTobeCredited.SourceDataString = null;
            this.mcbAccountTobeCredited.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbAccountTobeCredited.TabIndex = 1088;
            this.mcbAccountTobeCredited.UserControlToShow = null;
            this.mcbAccountTobeCredited.ValueColumnNo = 0;
            this.mcbAccountTobeCredited.EnterKeyPressed += new System.EventHandler(this.mcbAccountTobeCredited_EnterKeyPressed);
            this.mcbAccountTobeCredited.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcbAccountTobeCredited_KeyDown);
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(126, 95);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(61, 14);
            this.mPlbl4.TabIndex = 1087;
            this.mPlbl4.Text = "Narra&tion";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(56, 66);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(131, 14);
            this.mPlbl1.TabIndex = 1084;
            this.mPlbl1.Text = "Account To Be Debited";
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(207, 64);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = false;
            this.mcbCreditor.Size = new System.Drawing.Size(421, 22);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 135;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.Location = new System.Drawing.Point(206, 88);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(380, 24);
            this.txtNarration.TabIndex = 3;
            this.txtNarration.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // pnlVou
            // 
            this.pnlVou.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlVou.Controls.Add(this.txtVoucherSeries);
            this.pnlVou.Controls.Add(this.psLabel6);
            this.pnlVou.Controls.Add(this.txtVouType);
            this.pnlVou.Controls.Add(this.lblVouDate);
            this.pnlVou.Controls.Add(this.lblVouNumber);
            this.pnlVou.Controls.Add(this.lblVouType);
            this.pnlVou.Controls.Add(this.datePickerBillDate);
            this.pnlVou.Controls.Add(this.txtVouchernumber);
            this.pnlVou.Location = new System.Drawing.Point(732, 6);
            this.pnlVou.Name = "pnlVou";
            this.pnlVou.Size = new System.Drawing.Size(216, 103);
            this.pnlVou.TabIndex = 1104;
            // 
            // txtVoucherSeries
            // 
            this.txtVoucherSeries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVoucherSeries.Location = new System.Drawing.Point(93, 76);
            this.txtVoucherSeries.Name = "txtVoucherSeries";
            this.txtVoucherSeries.Size = new System.Drawing.Size(98, 22);
            this.txtVoucherSeries.TabIndex = 1086;
            // 
            // psLabel6
            // 
            this.psLabel6.AutoSize = true;
            this.psLabel6.Location = new System.Drawing.Point(5, 78);
            this.psLabel6.Name = "psLabel6";
            this.psLabel6.Size = new System.Drawing.Size(65, 14);
            this.psLabel6.TabIndex = 1085;
            this.psLabel6.Text = "Vou Series";
            // 
            // txtVouType
            // 
            this.txtVouType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouType.CausesValidation = false;
            this.txtVouType.Location = new System.Drawing.Point(93, 3);
            this.txtVouType.Name = "txtVouType";
            this.txtVouType.Size = new System.Drawing.Size(98, 23);
            this.txtVouType.TabIndex = 1084;
            this.txtVouType.Text = "label";
            this.txtVouType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVouDate
            // 
            this.lblVouDate.AutoSize = true;
            this.lblVouDate.Location = new System.Drawing.Point(15, 55);
            this.lblVouDate.Name = "lblVouDate";
            this.lblVouDate.Size = new System.Drawing.Size(55, 14);
            this.lblVouDate.TabIndex = 1083;
            this.lblVouDate.Text = "Vou &Date";
            // 
            // lblVouNumber
            // 
            this.lblVouNumber.AutoSize = true;
            this.lblVouNumber.Location = new System.Drawing.Point(24, 30);
            this.lblVouNumber.Name = "lblVouNumber";
            this.lblVouNumber.Size = new System.Drawing.Size(46, 14);
            this.lblVouNumber.TabIndex = 1083;
            this.lblVouNumber.Text = "Vou No";
            // 
            // lblVouType
            // 
            this.lblVouType.AutoSize = true;
            this.lblVouType.Location = new System.Drawing.Point(12, 5);
            this.lblVouType.Name = "lblVouType";
            this.lblVouType.Size = new System.Drawing.Size(58, 14);
            this.lblVouType.TabIndex = 1083;
            this.lblVouType.Text = "Vou Type";
            // 
            // datePickerBillDate
            // 
            this.datePickerBillDate.CustomFormat = "dd/MM/yy";
            this.datePickerBillDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerBillDate.Location = new System.Drawing.Point(93, 52);
            this.datePickerBillDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerBillDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerBillDate.Name = "datePickerBillDate";
            this.datePickerBillDate.Size = new System.Drawing.Size(98, 22);
            this.datePickerBillDate.TabIndex = 0;
            this.datePickerBillDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // txtVouchernumber
            // 
            this.txtVouchernumber.BackColor = System.Drawing.Color.Snow;
            this.txtVouchernumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouchernumber.Enabled = false;
            this.txtVouchernumber.Location = new System.Drawing.Point(93, 28);
            this.txtVouchernumber.MaxLength = 50;
            this.txtVouchernumber.Name = "txtVouchernumber";
            this.txtVouchernumber.ReadOnly = true;
            this.txtVouchernumber.Size = new System.Drawing.Size(98, 24);
            this.txtVouchernumber.TabIndex = 0;
            this.txtVouchernumber.TabStop = false;
            this.txtVouchernumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            // 
            // UclContraEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclContraEntry";
            this.Size = new System.Drawing.Size(975, 591);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlNameAddress.ResumeLayout(false);
            this.pnlNameAddress.PerformLayout();
            this.pnlVou.ResumeLayout(false);
            this.pnlVou.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlNameAddress;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNarration;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbAccountTobeCredited;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmount;
        private System.Windows.Forms.Panel pnlVou;
        private CommonControls.PSTextBox txtVoucherSeries;
        private CommonControls.PSLabel psLabel6;
        private CommonControls.PSLableWithBorderMiddleRight txtVouType;
        private CommonControls.PSLabel lblVouDate;
        private CommonControls.PSLabel lblVouNumber;
        private CommonControls.PSLabel lblVouType;
        private System.Windows.Forms.DateTimePicker datePickerBillDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouchernumber;
    }
}
