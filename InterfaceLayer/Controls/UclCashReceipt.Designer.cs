namespace EcoMart.InterfaceLayer
{
    partial class UclCashReceipt
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
      //  private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclCashReceipt));
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlNameAddress = new System.Windows.Forms.Panel();
            this.datePickerToDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.datePickerFromDate = new System.Windows.Forms.DateTimePicker();
            this.txtOpeningBalanceAmount = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.dgClearOpeningBalanceTemp = new System.Windows.Forms.DataGridView();
            this.btnClearOpeningBalance = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.mpPVCTemp = new System.Windows.Forms.DataGridView();
            this.pnlVou = new System.Windows.Forms.Panel();
            this.txtVouType = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.txtVoucherSeries = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.psLabel8 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouDate = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouNumber = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouType = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.datePickerBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtVouchernumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.PrintGrid = new System.Windows.Forms.DataGridView();
            this.btnModify = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.txtAmtNotAdjusted = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.mPlbl5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbCreditor = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtAmountReceived = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtNarration = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.txtTotalBalance = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtNoOfRows = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mpMSCSale = new EcoMart.InterfaceLayer.CommonControls.PSCashBankcontrol();
            this.mpMSVC = new EcoMart.InterfaceLayer.CommonControls.PSCashBankcontrol();
            this.dgClearOpeningBalance = new EcoMart.InterfaceLayer.CommonControls.PSCashBankcontrol();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlNameAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgClearOpeningBalanceTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mpPVCTemp)).BeginInit();
            this.pnlVou.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrintGrid)).BeginInit();
            this.pnlAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(971, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.psLabel2);
            this.MMBottomPanel.Controls.Add(this.psLabel1);
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.txtTotalBalance);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 616);
            this.MMBottomPanel.Size = new System.Drawing.Size(973, 63);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblRightSideFooterMsg, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotalBalance, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel1, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel2, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.dgClearOpeningBalance);
            this.MMMainPanel.Controls.Add(this.mpMSVC);
            this.MMMainPanel.Controls.Add(this.mpMSCSale);
            this.MMMainPanel.Controls.Add(this.lblMessage);
            this.MMMainPanel.Controls.Add(this.pnlNameAddress);
            this.MMMainPanel.Size = new System.Drawing.Size(973, 553);
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlNameAddress, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSCSale, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSVC, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgClearOpeningBalance, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(505, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 20);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.Location = new System.Drawing.Point(5, 644);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 61;
            // 
            // pnlNameAddress
            // 
            this.pnlNameAddress.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlNameAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNameAddress.Controls.Add(this.datePickerToDate);
            this.pnlNameAddress.Controls.Add(this.lblToDate);
            this.pnlNameAddress.Controls.Add(this.lblFromDate);
            this.pnlNameAddress.Controls.Add(this.datePickerFromDate);
            this.pnlNameAddress.Controls.Add(this.txtOpeningBalanceAmount);
            this.pnlNameAddress.Controls.Add(this.dgClearOpeningBalanceTemp);
            this.pnlNameAddress.Controls.Add(this.btnClearOpeningBalance);
            this.pnlNameAddress.Controls.Add(this.mpPVCTemp);
            this.pnlNameAddress.Controls.Add(this.pnlVou);
            this.pnlNameAddress.Controls.Add(this.PrintGrid);
            this.pnlNameAddress.Controls.Add(this.btnModify);
            this.pnlNameAddress.Controls.Add(this.txtAmtNotAdjusted);
            this.pnlNameAddress.Controls.Add(this.mPlbl5);
            this.pnlNameAddress.Controls.Add(this.pnlAddress);
            this.pnlNameAddress.Controls.Add(this.mPlbl4);
            this.pnlNameAddress.Controls.Add(this.mPlbl3);
            this.pnlNameAddress.Controls.Add(this.mPlbl2);
            this.pnlNameAddress.Controls.Add(this.mPlbl1);
            this.pnlNameAddress.Controls.Add(this.mcbCreditor);
            this.pnlNameAddress.Controls.Add(this.txtAmountReceived);
            this.pnlNameAddress.Controls.Add(this.txtNarration);
            this.pnlNameAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNameAddress.Location = new System.Drawing.Point(0, 0);
            this.pnlNameAddress.Name = "pnlNameAddress";
            this.pnlNameAddress.Size = new System.Drawing.Size(971, 147);
            this.pnlNameAddress.TabIndex = 0;
            // 
            // datePickerToDate
            // 
            this.datePickerToDate.CustomFormat = "dd/MM/yyyy";
            this.datePickerToDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerToDate.Location = new System.Drawing.Point(330, 68);
            this.datePickerToDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerToDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerToDate.Name = "datePickerToDate";
            this.datePickerToDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.datePickerToDate.Size = new System.Drawing.Size(100, 22);
            this.datePickerToDate.TabIndex = 1111;
            this.datePickerToDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.datePickerToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datePickerToDate_KeyDown);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(271, 72);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(52, 16);
            this.lblToDate.TabIndex = 1110;
            this.lblToDate.Text = "To Date";
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(65, 72);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(71, 16);
            this.lblFromDate.TabIndex = 1109;
            this.lblFromDate.Text = "From Date";
            // 
            // datePickerFromDate
            // 
            this.datePickerFromDate.CustomFormat = "dd/MM/yyyy";
            this.datePickerFromDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerFromDate.Location = new System.Drawing.Point(155, 68);
            this.datePickerFromDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerFromDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerFromDate.Name = "datePickerFromDate";
            this.datePickerFromDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.datePickerFromDate.Size = new System.Drawing.Size(100, 22);
            this.datePickerFromDate.TabIndex = 1108;
            this.datePickerFromDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.datePickerFromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datePickerFromDate_KeyDown);
            // 
            // txtOpeningBalanceAmount
            // 
            this.txtOpeningBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpeningBalanceAmount.CausesValidation = false;
            this.txtOpeningBalanceAmount.Location = new System.Drawing.Point(542, 118);
            this.txtOpeningBalanceAmount.Name = "txtOpeningBalanceAmount";
            this.txtOpeningBalanceAmount.Size = new System.Drawing.Size(141, 25);
            this.txtOpeningBalanceAmount.TabIndex = 1107;
            this.txtOpeningBalanceAmount.Text = "0.00";
            this.txtOpeningBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgClearOpeningBalanceTemp
            // 
            this.dgClearOpeningBalanceTemp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgClearOpeningBalanceTemp.Location = new System.Drawing.Point(668, 77);
            this.dgClearOpeningBalanceTemp.Name = "dgClearOpeningBalanceTemp";
            this.dgClearOpeningBalanceTemp.Size = new System.Drawing.Size(15, 17);
            this.dgClearOpeningBalanceTemp.TabIndex = 1106;
            this.dgClearOpeningBalanceTemp.Visible = false;
            // 
            // btnClearOpeningBalance
            // 
            this.btnClearOpeningBalance.Location = new System.Drawing.Point(557, 87);
            this.btnClearOpeningBalance.Name = "btnClearOpeningBalance";
            this.btnClearOpeningBalance.Size = new System.Drawing.Size(93, 28);
            this.btnClearOpeningBalance.TabIndex = 1105;
            this.btnClearOpeningBalance.Text = "Clear OP Bal.";
            this.btnClearOpeningBalance.UseVisualStyleBackColor = false;
            this.btnClearOpeningBalance.Visible = false;
            this.btnClearOpeningBalance.Click += new System.EventHandler(this.btnClearOpeningBalance_Click);
            // 
            // mpPVCTemp
            // 
            this.mpPVCTemp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mpPVCTemp.Location = new System.Drawing.Point(668, 54);
            this.mpPVCTemp.Name = "mpPVCTemp";
            this.mpPVCTemp.Size = new System.Drawing.Size(15, 17);
            this.mpPVCTemp.TabIndex = 65;
            this.mpPVCTemp.Visible = false;
            // 
            // pnlVou
            // 
            this.pnlVou.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlVou.Controls.Add(this.txtVouType);
            this.pnlVou.Controls.Add(this.txtVoucherSeries);
            this.pnlVou.Controls.Add(this.psLabel8);
            this.pnlVou.Controls.Add(this.lblVouDate);
            this.pnlVou.Controls.Add(this.lblVouNumber);
            this.pnlVou.Controls.Add(this.lblVouType);
            this.pnlVou.Controls.Add(this.datePickerBillDate);
            this.pnlVou.Controls.Add(this.txtVouchernumber);
            this.pnlVou.Location = new System.Drawing.Point(738, 3);
            this.pnlVou.Name = "pnlVou";
            this.pnlVou.Size = new System.Drawing.Size(216, 99);
            this.pnlVou.TabIndex = 1104;
            // 
            // txtVouType
            // 
            this.txtVouType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouType.Location = new System.Drawing.Point(93, 2);
            this.txtVouType.Name = "txtVouType";
            this.txtVouType.Size = new System.Drawing.Size(100, 23);
            this.txtVouType.TabIndex = 1087;
            this.txtVouType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVoucherSeries
            // 
            this.txtVoucherSeries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVoucherSeries.Enabled = false;
            this.txtVoucherSeries.IsNumericDataSet = false;
            this.txtVoucherSeries.Location = new System.Drawing.Point(93, 75);
            this.txtVoucherSeries.Name = "txtVoucherSeries";
            this.txtVoucherSeries.Size = new System.Drawing.Size(100, 22);
            this.txtVoucherSeries.TabIndex = 1086;
            // 
            // psLabel8
            // 
            this.psLabel8.AutoSize = true;
            this.psLabel8.Location = new System.Drawing.Point(1, 74);
            this.psLabel8.Name = "psLabel8";
            this.psLabel8.Size = new System.Drawing.Size(69, 16);
            this.psLabel8.TabIndex = 1085;
            this.psLabel8.Text = "Vou Series";
            // 
            // lblVouDate
            // 
            this.lblVouDate.AutoSize = true;
            this.lblVouDate.Location = new System.Drawing.Point(10, 54);
            this.lblVouDate.Name = "lblVouDate";
            this.lblVouDate.Size = new System.Drawing.Size(60, 16);
            this.lblVouDate.TabIndex = 1083;
            this.lblVouDate.Text = "Vou &Date";
            // 
            // lblVouNumber
            // 
            this.lblVouNumber.AutoSize = true;
            this.lblVouNumber.Location = new System.Drawing.Point(24, 29);
            this.lblVouNumber.Name = "lblVouNumber";
            this.lblVouNumber.Size = new System.Drawing.Size(49, 16);
            this.lblVouNumber.TabIndex = 1083;
            this.lblVouNumber.Text = "Vou No";
            // 
            // lblVouType
            // 
            this.lblVouType.AutoSize = true;
            this.lblVouType.Location = new System.Drawing.Point(8, 4);
            this.lblVouType.Name = "lblVouType";
            this.lblVouType.Size = new System.Drawing.Size(62, 16);
            this.lblVouType.TabIndex = 1083;
            this.lblVouType.Text = "Vou Type";
            // 
            // datePickerBillDate
            // 
            this.datePickerBillDate.CustomFormat = "dd/MM/yyyy";
            this.datePickerBillDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerBillDate.Location = new System.Drawing.Point(93, 51);
            this.datePickerBillDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerBillDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerBillDate.Name = "datePickerBillDate";
            this.datePickerBillDate.Size = new System.Drawing.Size(100, 22);
            this.datePickerBillDate.TabIndex = 0;
            this.datePickerBillDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // txtVouchernumber
            // 
            this.txtVouchernumber.BackColor = System.Drawing.Color.Snow;
            this.txtVouchernumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouchernumber.Enabled = false;
            this.txtVouchernumber.Location = new System.Drawing.Point(93, 26);
            this.txtVouchernumber.MaxLength = 50;
            this.txtVouchernumber.Name = "txtVouchernumber";
            this.txtVouchernumber.ReadOnly = true;
            this.txtVouchernumber.Size = new System.Drawing.Size(100, 24);
            this.txtVouchernumber.TabIndex = 0;
            this.txtVouchernumber.TabStop = false;
            this.txtVouchernumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtVouchernumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVouchernumber_KeyDown);
            // 
            // PrintGrid
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PrintGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PrintGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PrintGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.PrintGrid.Location = new System.Drawing.Point(691, 18);
            this.PrintGrid.Name = "PrintGrid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PrintGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.PrintGrid.Size = new System.Drawing.Size(18, 10);
            this.PrintGrid.TabIndex = 1103;
            this.PrintGrid.Visible = false;
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(557, 41);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(93, 43);
            this.btnModify.TabIndex = 1102;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // txtAmtNotAdjusted
            // 
            this.txtAmtNotAdjusted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmtNotAdjusted.CausesValidation = false;
            this.txtAmtNotAdjusted.Location = new System.Drawing.Point(805, 108);
            this.txtAmtNotAdjusted.Name = "txtAmtNotAdjusted";
            this.txtAmtNotAdjusted.Size = new System.Drawing.Size(141, 36);
            this.txtAmtNotAdjusted.TabIndex = 1100;
            this.txtAmtNotAdjusted.Text = "0.00";
            this.txtAmtNotAdjusted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(720, 117);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(76, 16);
            this.mPlbl5.TabIndex = 1099;
            this.mPlbl5.Text = "On Account";
            // 
            // pnlAddress
            // 
            this.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddress.Controls.Add(this.txtAddress1);
            this.pnlAddress.Controls.Add(this.txtAddress2);
            this.pnlAddress.Enabled = false;
            this.pnlAddress.Location = new System.Drawing.Point(154, 31);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(323, 36);
            this.pnlAddress.TabIndex = 3;
            // 
            // txtAddress1
            // 
            this.txtAddress1.BackColor = System.Drawing.Color.White;
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.Location = new System.Drawing.Point(1, 2);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(317, 15);
            this.txtAddress1.TabIndex = 0;
            // 
            // txtAddress2
            // 
            this.txtAddress2.BackColor = System.Drawing.Color.White;
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.Location = new System.Drawing.Point(1, 17);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(317, 15);
            this.txtAddress2.TabIndex = 1;
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(71, 123);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(67, 16);
            this.mPlbl4.TabIndex = 6;
            this.mPlbl4.Text = "Narra&tion";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(81, 97);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(56, 16);
            this.mPlbl3.TabIndex = 4;
            this.mPlbl3.Text = "&Amount";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(79, 40);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(57, 16);
            this.mPlbl2.TabIndex = 2;
            this.mPlbl2.Text = "Address";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(46, 8);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(95, 16);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "Account &Name";
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(154, 8);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = "";
            this.mcbCreditor.SelectedIDtest = 0;
            this.mcbCreditor.SelectedIntID = 0;
            this.mcbCreditor.ShowNew = true;
            this.mcbCreditor.Size = new System.Drawing.Size(323, 22);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 1;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.SeletectIndexChanged += new System.EventHandler(this.mcbCreditor_SeletectIndexChanged);
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            this.mcbCreditor.ItemAddedEdited += new System.EventHandler(this.mcbCreditor_ItemAddedEdited);
            // 
            // txtAmountReceived
            // 
            this.txtAmountReceived.BackColor = System.Drawing.SystemColors.Window;
            this.txtAmountReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountReceived.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountReceived.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAmountReceived.Location = new System.Drawing.Point(153, 91);
            this.txtAmountReceived.MaxLength = 15;
            this.txtAmountReceived.Name = "txtAmountReceived";
            this.txtAmountReceived.Size = new System.Drawing.Size(162, 26);
            this.txtAmountReceived.TabIndex = 5;
            this.txtAmountReceived.TabStop = false;
            this.txtAmountReceived.Tag = "0.00";
            this.txtAmountReceived.Text = "0.00";
            this.txtAmountReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmountReceived.TextChanged += new System.EventHandler(this.txtAmountReceived_TextChanged);
            this.txtAmountReceived.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmountReceived_KeyDown);
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNarration.Location = new System.Drawing.Point(153, 118);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(383, 24);
            this.txtNarration.TabIndex = 7;
            this.txtNarration.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // txtTotalBalance
            // 
            this.txtTotalBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalBalance.CausesValidation = false;
            this.txtTotalBalance.Location = new System.Drawing.Point(520, 0);
            this.txtTotalBalance.Name = "txtTotalBalance";
            this.txtTotalBalance.Size = new System.Drawing.Size(100, 23);
            this.txtTotalBalance.TabIndex = 1018;
            this.txtTotalBalance.Text = "label";
            this.txtTotalBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.CausesValidation = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(748, 0);
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(56, 23);
            this.txtNoOfRows.TabIndex = 1019;
            this.txtNoOfRows.Text = "label";
            this.txtNoOfRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(383, 5);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(88, 16);
            this.psLabel1.TabIndex = 1102;
            this.psLabel1.Text = "Total Balance";
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(652, 5);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(74, 16);
            this.psLabel2.TabIndex = 1103;
            this.psLabel2.Text = "No of Rows";
            // 
            // mpMSCSale
            // 
            this.mpMSCSale.AutoScroll = true;
            this.mpMSCSale.DataSource = null;
            this.mpMSCSale.DataSourceMain = null;
            this.mpMSCSale.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSCSale.DateColumnNames")));
            this.mpMSCSale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMSCSale.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSCSale.DoubleColumnNames")));
            this.mpMSCSale.Filter = null;
            this.mpMSCSale.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMSCSale.IsAllowDelete = false;
            this.mpMSCSale.IsAllowNewRow = false;
            this.mpMSCSale.Location = new System.Drawing.Point(0, 147);
            this.mpMSCSale.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpMSCSale.MinimumSize = new System.Drawing.Size(520, 323);
            this.mpMSCSale.Name = "mpMSCSale";
            this.mpMSCSale.NextRowColumn = 0;
            this.mpMSCSale.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSCSale.NumericColumnNames")));
            this.mpMSCSale.Size = new System.Drawing.Size(971, 404);
            this.mpMSCSale.TabIndex = 65;
            this.mpMSCSale.ViewControl = null;
            this.mpMSCSale.OnCellValueChangeCommited += new EcoMart.InterfaceLayer.CommonControls.PSCashBankcontrol.CellValueChangeCommited(this.mpMSCSale_OnCellValueChangeCommited);
            this.mpMSCSale.OnCellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.mpMSCSale_OnCellEnter);
            this.mpMSCSale.OnCellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.mpMSCSale_OnCellLeave);
            this.mpMSCSale.OnEscapeKeyPressed += new System.EventHandler(this.mpMSCSale_OnEscapeKeyPressed);
            this.mpMSCSale.OnShowViewForm += new EcoMart.InterfaceLayer.CommonControls.PSCashBankcontrol.ShowViewForm(this.mpMSCSale_OnShowViewForm);
            // 
            // mpMSVC
            // 
            this.mpMSVC.AutoScroll = true;
            this.mpMSVC.DataSource = null;
            this.mpMSVC.DataSourceMain = null;
            this.mpMSVC.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC.DateColumnNames")));
            this.mpMSVC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMSVC.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC.DoubleColumnNames")));
            this.mpMSVC.Filter = null;
            this.mpMSVC.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMSVC.IsAllowDelete = true;
            this.mpMSVC.IsAllowNewRow = true;
            this.mpMSVC.Location = new System.Drawing.Point(0, 147);
            this.mpMSVC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpMSVC.MinimumSize = new System.Drawing.Size(520, 323);
            this.mpMSVC.Name = "mpMSVC";
            this.mpMSVC.NextRowColumn = 0;
            this.mpMSVC.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC.NumericColumnNames")));
            this.mpMSVC.Size = new System.Drawing.Size(971, 404);
            this.mpMSVC.TabIndex = 66;
            this.mpMSVC.ViewControl = null;
            this.mpMSVC.Visible = false;
            this.mpMSVC.OnShowViewForm += new EcoMart.InterfaceLayer.CommonControls.PSCashBankcontrol.ShowViewForm(this.mpMSCSale_OnShowViewForm);
            // 
            // dgClearOpeningBalance
            // 
            this.dgClearOpeningBalance.AutoScroll = true;
            this.dgClearOpeningBalance.BackColor = System.Drawing.Color.Plum;
            this.dgClearOpeningBalance.DataSource = null;
            this.dgClearOpeningBalance.DataSourceMain = null;
            this.dgClearOpeningBalance.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgClearOpeningBalance.DateColumnNames")));
            this.dgClearOpeningBalance.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgClearOpeningBalance.DoubleColumnNames")));
            this.dgClearOpeningBalance.Filter = null;
            this.dgClearOpeningBalance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgClearOpeningBalance.ForeColor = System.Drawing.Color.Maroon;
            this.dgClearOpeningBalance.IsAllowDelete = true;
            this.dgClearOpeningBalance.IsAllowNewRow = true;
            this.dgClearOpeningBalance.Location = new System.Drawing.Point(-1, 193);
            this.dgClearOpeningBalance.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgClearOpeningBalance.MinimumSize = new System.Drawing.Size(520, 323);
            this.dgClearOpeningBalance.Name = "dgClearOpeningBalance";
            this.dgClearOpeningBalance.NextRowColumn = 0;
            this.dgClearOpeningBalance.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgClearOpeningBalance.NumericColumnNames")));
            this.dgClearOpeningBalance.Size = new System.Drawing.Size(968, 323);
            this.dgClearOpeningBalance.TabIndex = 67;
            this.dgClearOpeningBalance.ViewControl = null;
            this.dgClearOpeningBalance.OnCellValueChangeCommited += new EcoMart.InterfaceLayer.CommonControls.PSCashBankcontrol.CellValueChangeCommited(this.dgClearOpeningBalance_OnCellValueChangeCommited);
            this.dgClearOpeningBalance.OnCellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgClearOpeningBalance_OnCellEnter);
            this.dgClearOpeningBalance.OnCellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgClearOpeningBalance_OnCellLeave);
            this.dgClearOpeningBalance.OnEscapeKeyPressed += new System.EventHandler(this.dgClearOpeningBalance_OnEscapeKeyPressed);
            // 
            // UclCashReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclCashReceipt";
            this.Size = new System.Drawing.Size(973, 679);
            this.Load += new System.EventHandler(this.UclCashReceipt_Load);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlNameAddress.ResumeLayout(false);
            this.pnlNameAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgClearOpeningBalanceTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mpPVCTemp)).EndInit();
            this.pnlVou.ResumeLayout(false);
            this.pnlVou.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrintGrid)).EndInit();
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlNameAddress;
        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtAddress2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmountReceived;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNarration;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtTotalBalance;
        private System.ComponentModel.IContainer components;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtNoOfRows;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtAmtNotAdjusted;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSPlus.CommonLibrary.PSButton btnModify;
        private System.Windows.Forms.DataGridView PrintGrid;
        private System.Windows.Forms.Panel pnlVou;
        private CommonControls.PSLableWithBorderMiddleLeft txtVouType;
        private CommonControls.PSTextBox txtVoucherSeries;
        private CommonControls.PSLabel psLabel8;
        private CommonControls.PSLabel lblVouDate;
        private CommonControls.PSLabel lblVouNumber;
        private CommonControls.PSLabel lblVouType;
        private System.Windows.Forms.DateTimePicker datePickerBillDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouchernumber;
        private System.Windows.Forms.DataGridView mpPVCTemp;
        private CommonControls.PSCashBankcontrol mpMSVC;
        private CommonControls.PSCashBankcontrol mpMSCSale;
        private PharmaSYSPlus.CommonLibrary.PSButton btnClearOpeningBalance;
        private CommonControls.PSCashBankcontrol dgClearOpeningBalance;
        private System.Windows.Forms.DataGridView dgClearOpeningBalanceTemp;
        private CommonControls.PSLableWithBorderMiddleRight txtOpeningBalanceAmount;
        private CommonControls.PSLabel lblToDate;
        private CommonControls.PSLabel lblFromDate;
        private System.Windows.Forms.DateTimePicker datePickerFromDate;
        private System.Windows.Forms.DateTimePicker datePickerToDate;
    }
}
