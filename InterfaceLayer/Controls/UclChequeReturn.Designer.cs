namespace EcoMart.InterfaceLayer
{
    partial class UclChequeReturn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclChequeReturn));
            this.pnlVouTypeNo = new System.Windows.Forms.Panel();
            this.datePickerChequeDate = new System.Windows.Forms.DateTimePicker();
            this.mPlbl13 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl12 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl11 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl7 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl6 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.mcbBranch = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mcbBankAccount = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtChequeNumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.mcbBank = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mcbCreditor = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtAmtNotAdjusted = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtAmountReceived = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtNarration = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.txtTotalBalance = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.lblTotalBalance = new System.Windows.Forms.Label();
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblNoofRows = new System.Windows.Forms.Label();
            this.pnlChequeReturn = new System.Windows.Forms.Panel();
            this.txtReturnCharges = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mpMSVC = new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.pnlVou = new System.Windows.Forms.Panel();
            this.txtVouType = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.txtVoucherSeries = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.psLabel8 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouDate = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouNumber = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouType = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.datePickerBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtVouchernumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlVouTypeNo.SuspendLayout();
            this.pnlAddress.SuspendLayout();
            this.pnlChequeReturn.SuspendLayout();
            this.pnlVou.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(964, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtTotalBalance);
            this.MMBottomPanel.Controls.Add(this.lblTotalBalance);
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.lblNoofRows);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 710);
            this.MMBottomPanel.Size = new System.Drawing.Size(966, 63);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblNoofRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblTotalBalance, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotalBalance, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.mpMSVC);
            this.MMMainPanel.Controls.Add(this.pnlChequeReturn);
            this.MMMainPanel.Controls.Add(this.pnlVouTypeNo);
            this.MMMainPanel.Size = new System.Drawing.Size(966, 658);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlVouTypeNo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlChequeReturn, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSVC, 0);
            // 
            // pnlVouTypeNo
            // 
            this.pnlVouTypeNo.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlVouTypeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVouTypeNo.Controls.Add(this.pnlVou);
            this.pnlVouTypeNo.Controls.Add(this.datePickerChequeDate);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl13);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl12);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl11);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl7);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl6);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl5);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl4);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl3);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl2);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl1);
            this.pnlVouTypeNo.Controls.Add(this.pnlAddress);
            this.pnlVouTypeNo.Controls.Add(this.mcbBranch);
            this.pnlVouTypeNo.Controls.Add(this.mcbBankAccount);
            this.pnlVouTypeNo.Controls.Add(this.txtChequeNumber);
            this.pnlVouTypeNo.Controls.Add(this.mcbBank);
            this.pnlVouTypeNo.Controls.Add(this.mcbCreditor);
            this.pnlVouTypeNo.Controls.Add(this.txtAmtNotAdjusted);
            this.pnlVouTypeNo.Controls.Add(this.txtAmountReceived);
            this.pnlVouTypeNo.Controls.Add(this.txtNarration);
            this.pnlVouTypeNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVouTypeNo.Location = new System.Drawing.Point(0, 0);
            this.pnlVouTypeNo.Name = "pnlVouTypeNo";
            this.pnlVouTypeNo.Size = new System.Drawing.Size(964, 218);
            this.pnlVouTypeNo.TabIndex = 1;
            // 
            // datePickerChequeDate
            // 
            this.datePickerChequeDate.CustomFormat = "";
            this.datePickerChequeDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerChequeDate.Location = new System.Drawing.Point(377, 165);
            this.datePickerChequeDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerChequeDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerChequeDate.Name = "datePickerChequeDate";
            this.datePickerChequeDate.Size = new System.Drawing.Size(118, 23);
            this.datePickerChequeDate.TabIndex = 1058;
            this.datePickerChequeDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // mPlbl13
            // 
            this.mPlbl13.AutoSize = true;
            this.mPlbl13.Location = new System.Drawing.Point(58, 196);
            this.mPlbl13.Name = "mPlbl13";
            this.mPlbl13.Size = new System.Drawing.Size(61, 14);
            this.mPlbl13.TabIndex = 17;
            this.mPlbl13.Text = "Narra&tion";
            // 
            // mPlbl12
            // 
            this.mPlbl12.AutoSize = true;
            this.mPlbl12.Location = new System.Drawing.Point(84, 123);
            this.mPlbl12.Name = "mPlbl12";
            this.mPlbl12.Size = new System.Drawing.Size(35, 14);
            this.mPlbl12.TabIndex = 9;
            this.mPlbl12.Text = "Ban&k";
            // 
            // mPlbl11
            // 
            this.mPlbl11.AutoSize = true;
            this.mPlbl11.Location = new System.Drawing.Point(680, 148);
            this.mPlbl11.Name = "mPlbl11";
            this.mPlbl11.Size = new System.Drawing.Size(70, 14);
            this.mPlbl11.TabIndex = 1057;
            this.mPlbl11.Text = "On Account";
            // 
            // mPlbl7
            // 
            this.mPlbl7.AutoSize = true;
            this.mPlbl7.Location = new System.Drawing.Point(72, 147);
            this.mPlbl7.Name = "mPlbl7";
            this.mPlbl7.Size = new System.Drawing.Size(47, 14);
            this.mPlbl7.TabIndex = 11;
            this.mPlbl7.Text = "Branc&h";
            // 
            // mPlbl6
            // 
            this.mPlbl6.AutoSize = true;
            this.mPlbl6.Location = new System.Drawing.Point(326, 170);
            this.mPlbl6.Name = "mPlbl6";
            this.mPlbl6.Size = new System.Drawing.Size(31, 14);
            this.mPlbl6.TabIndex = 15;
            this.mPlbl6.Text = "Date";
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(73, 169);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(46, 14);
            this.mPlbl5.TabIndex = 13;
            this.mPlbl5.Text = "&Chq No";
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(68, 98);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(51, 14);
            this.mPlbl4.TabIndex = 7;
            this.mPlbl4.Text = "&Amount";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(66, 62);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(53, 14);
            this.mPlbl3.TabIndex = 5;
            this.mPlbl3.Text = "Address";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(33, 34);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(86, 14);
            this.mPlbl2.TabIndex = 3;
            this.mPlbl2.Text = "Account &Name";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(36, 11);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(83, 14);
            this.mPlbl1.TabIndex = 1;
            this.mPlbl1.Text = "&Bank Account";
            // 
            // pnlAddress
            // 
            this.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddress.Controls.Add(this.txtAddress1);
            this.pnlAddress.Controls.Add(this.txtAddress2);
            this.pnlAddress.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddress.Location = new System.Drawing.Point(139, 55);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(357, 36);
            this.pnlAddress.TabIndex = 6;
            // 
            // txtAddress1
            // 
            this.txtAddress1.BackColor = System.Drawing.Color.White;
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress1.Enabled = false;
            this.txtAddress1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.Location = new System.Drawing.Point(1, 2);
            this.txtAddress1.MaxLength = 30;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(357, 15);
            this.txtAddress1.TabIndex = 0;
            // 
            // txtAddress2
            // 
            this.txtAddress2.BackColor = System.Drawing.Color.White;
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress2.Enabled = false;
            this.txtAddress2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.Location = new System.Drawing.Point(1, 17);
            this.txtAddress2.MaxLength = 30;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(357, 15);
            this.txtAddress2.TabIndex = 1;
            // 
            // mcbBranch
            // 
            this.mcbBranch.ColumnWidth = null;
            this.mcbBranch.DataSource = null;
            this.mcbBranch.DisplayColumnNo = 1;
            this.mcbBranch.DropDownHeight = 200;
            this.mcbBranch.Location = new System.Drawing.Point(139, 142);
            this.mcbBranch.Margin = new System.Windows.Forms.Padding(4);
            this.mcbBranch.Name = "mcbBranch";
            this.mcbBranch.SelectedID = null;
            this.mcbBranch.ShowNew = true;
            this.mcbBranch.Size = new System.Drawing.Size(394, 22);
            this.mcbBranch.SourceDataString = null;
            this.mcbBranch.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbBranch.TabIndex = 12;
            this.mcbBranch.UserControlToShow = null;
            this.mcbBranch.ValueColumnNo = 0;
            // 
            // mcbBankAccount
            // 
            this.mcbBankAccount.ColumnWidth = null;
            this.mcbBankAccount.DataSource = null;
            this.mcbBankAccount.DisplayColumnNo = 1;
            this.mcbBankAccount.DropDownHeight = 200;
            this.mcbBankAccount.Location = new System.Drawing.Point(139, 8);
            this.mcbBankAccount.Margin = new System.Windows.Forms.Padding(4);
            this.mcbBankAccount.Name = "mcbBankAccount";
            this.mcbBankAccount.SelectedID = null;
            this.mcbBankAccount.ShowNew = false;
            this.mcbBankAccount.Size = new System.Drawing.Size(394, 22);
            this.mcbBankAccount.SourceDataString = null;
            this.mcbBankAccount.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbBankAccount.TabIndex = 2;
            this.mcbBankAccount.UserControlToShow = null;
            this.mcbBankAccount.ValueColumnNo = 0;
            // 
            // txtChequeNumber
            // 
            this.txtChequeNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChequeNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtChequeNumber.Location = new System.Drawing.Point(139, 165);
            this.txtChequeNumber.MaxLength = 50;
            this.txtChequeNumber.Name = "txtChequeNumber";
            this.txtChequeNumber.Size = new System.Drawing.Size(156, 24);
            this.txtChequeNumber.TabIndex = 14;
            this.txtChequeNumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            // 
            // mcbBank
            // 
            this.mcbBank.ColumnWidth = null;
            this.mcbBank.DataSource = null;
            this.mcbBank.DisplayColumnNo = 1;
            this.mcbBank.DropDownHeight = 200;
            this.mcbBank.Location = new System.Drawing.Point(139, 119);
            this.mcbBank.Margin = new System.Windows.Forms.Padding(4);
            this.mcbBank.Name = "mcbBank";
            this.mcbBank.SelectedID = null;
            this.mcbBank.ShowNew = true;
            this.mcbBank.Size = new System.Drawing.Size(394, 22);
            this.mcbBank.SourceDataString = null;
            this.mcbBank.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbBank.TabIndex = 10;
            this.mcbBank.UserControlToShow = null;
            this.mcbBank.ValueColumnNo = 0;
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(139, 32);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = false;
            this.mcbCreditor.Size = new System.Drawing.Size(394, 22);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 4;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            // 
            // txtAmtNotAdjusted
            // 
            this.txtAmtNotAdjusted.BackColor = System.Drawing.SystemColors.Window;
            this.txtAmtNotAdjusted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmtNotAdjusted.Enabled = false;
            this.txtAmtNotAdjusted.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmtNotAdjusted.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAmtNotAdjusted.Location = new System.Drawing.Point(794, 145);
            this.txtAmtNotAdjusted.MaxLength = 15;
            this.txtAmtNotAdjusted.Name = "txtAmtNotAdjusted";
            this.txtAmtNotAdjusted.Size = new System.Drawing.Size(137, 31);
            this.txtAmtNotAdjusted.TabIndex = 127;
            this.txtAmtNotAdjusted.TabStop = false;
            this.txtAmtNotAdjusted.Tag = "0.00";
            this.txtAmtNotAdjusted.Text = "0.00";
            this.txtAmtNotAdjusted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAmountReceived
            // 
            this.txtAmountReceived.BackColor = System.Drawing.SystemColors.Window;
            this.txtAmountReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountReceived.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountReceived.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAmountReceived.Location = new System.Drawing.Point(139, 92);
            this.txtAmountReceived.MaxLength = 15;
            this.txtAmountReceived.Name = "txtAmountReceived";
            this.txtAmountReceived.Size = new System.Drawing.Size(156, 26);
            this.txtAmountReceived.TabIndex = 8;
            this.txtAmountReceived.TabStop = false;
            this.txtAmountReceived.Tag = "0.00";
            this.txtAmountReceived.Text = "0.00";
            this.txtAmountReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNarration.Location = new System.Drawing.Point(139, 190);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(357, 24);
            this.txtNarration.TabIndex = 18;
            this.txtNarration.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            // 
            // txtTotalBalance
            // 
            this.txtTotalBalance.BackColor = System.Drawing.SystemColors.Window;
            this.txtTotalBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalBalance.Enabled = false;
            this.txtTotalBalance.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBalance.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTotalBalance.Location = new System.Drawing.Point(392, -1);
            this.txtTotalBalance.MaxLength = 15;
            this.txtTotalBalance.Name = "txtTotalBalance";
            this.txtTotalBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalBalance.Size = new System.Drawing.Size(132, 22);
            this.txtTotalBalance.TabIndex = 1015;
            this.txtTotalBalance.TabStop = false;
            this.txtTotalBalance.Tag = "0.00";
            this.txtTotalBalance.Text = "0.00";
            this.txtTotalBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalBalance
            // 
            this.lblTotalBalance.AutoSize = true;
            this.lblTotalBalance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBalance.Location = new System.Drawing.Point(285, 1);
            this.lblTotalBalance.Name = "lblTotalBalance";
            this.lblTotalBalance.Size = new System.Drawing.Size(87, 15);
            this.lblTotalBalance.TabIndex = 1014;
            this.lblTotalBalance.Text = "Total Balance";
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.Snow;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(638, -1);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 26);
            this.txtNoOfRows.TabIndex = 1013;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNoofRows
            // 
            this.lblNoofRows.AutoSize = true;
            this.lblNoofRows.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRows.Location = new System.Drawing.Point(558, 1);
            this.lblNoofRows.Name = "lblNoofRows";
            this.lblNoofRows.Size = new System.Drawing.Size(74, 15);
            this.lblNoofRows.TabIndex = 1012;
            this.lblNoofRows.Text = "No Of Rows";
            // 
            // pnlChequeReturn
            // 
            this.pnlChequeReturn.Controls.Add(this.txtReturnCharges);
            this.pnlChequeReturn.Controls.Add(this.psLabel2);
            this.pnlChequeReturn.Controls.Add(this.dtpReturnDate);
            this.pnlChequeReturn.Controls.Add(this.psLabel1);
            this.pnlChequeReturn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChequeReturn.Location = new System.Drawing.Point(0, 586);
            this.pnlChequeReturn.Name = "pnlChequeReturn";
            this.pnlChequeReturn.Size = new System.Drawing.Size(964, 70);
            this.pnlChequeReturn.TabIndex = 48;
            // 
            // txtReturnCharges
            // 
            this.txtReturnCharges.BackColor = System.Drawing.SystemColors.Window;
            this.txtReturnCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReturnCharges.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnCharges.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtReturnCharges.Location = new System.Drawing.Point(504, 23);
            this.txtReturnCharges.MaxLength = 15;
            this.txtReturnCharges.Name = "txtReturnCharges";
            this.txtReturnCharges.Size = new System.Drawing.Size(128, 26);
            this.txtReturnCharges.TabIndex = 1062;
            this.txtReturnCharges.TabStop = false;
            this.txtReturnCharges.Tag = "0.00";
            this.txtReturnCharges.Text = "0.00";
            this.txtReturnCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtReturnCharges.Visible = false;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(361, 25);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(92, 14);
            this.psLabel2.TabIndex = 1061;
            this.psLabel2.Text = "Return Charges";
            this.psLabel2.Visible = false;
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.CustomFormat = "";
            this.dtpReturnDate.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReturnDate.Location = new System.Drawing.Point(179, 20);
            this.dtpReturnDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtpReturnDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(137, 27);
            this.dtpReturnDate.TabIndex = 1060;
            this.dtpReturnDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.dtpReturnDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpReturnDate_KeyDown);
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(63, 25);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(72, 14);
            this.psLabel1.TabIndex = 1059;
            this.psLabel1.Text = "Return Date";
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
            this.mpMSVC.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMSVC.IsAllowDelete = false;
            this.mpMSVC.IsAllowNewRow = false;
            this.mpMSVC.Location = new System.Drawing.Point(0, 218);
            this.mpMSVC.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mpMSVC.MinimumSize = new System.Drawing.Size(488, 200);
            this.mpMSVC.Name = "mpMSVC";
            this.mpMSVC.NextRowColumn = 0;
            this.mpMSVC.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC.NumericColumnNames")));
            this.mpMSVC.Size = new System.Drawing.Size(964, 368);
            this.mpMSVC.SubGridWidth = 450;
            this.mpMSVC.TabIndex = 49;
            this.mpMSVC.ViewControl = null;
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
            this.pnlVou.Location = new System.Drawing.Point(738, 5);
            this.pnlVou.Name = "pnlVou";
            this.pnlVou.Size = new System.Drawing.Size(216, 99);
            this.pnlVou.TabIndex = 1102;
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
            this.psLabel8.Size = new System.Drawing.Size(65, 14);
            this.psLabel8.TabIndex = 1085;
            this.psLabel8.Text = "Vou Series";
            // 
            // lblVouDate
            // 
            this.lblVouDate.AutoSize = true;
            this.lblVouDate.Location = new System.Drawing.Point(10, 54);
            this.lblVouDate.Name = "lblVouDate";
            this.lblVouDate.Size = new System.Drawing.Size(55, 14);
            this.lblVouDate.TabIndex = 1083;
            this.lblVouDate.Text = "Vou &Date";
            // 
            // lblVouNumber
            // 
            this.lblVouNumber.AutoSize = true;
            this.lblVouNumber.Location = new System.Drawing.Point(24, 29);
            this.lblVouNumber.Name = "lblVouNumber";
            this.lblVouNumber.Size = new System.Drawing.Size(46, 14);
            this.lblVouNumber.TabIndex = 1083;
            this.lblVouNumber.Text = "Vou No";
            // 
            // lblVouType
            // 
            this.lblVouType.AutoSize = true;
            this.lblVouType.Location = new System.Drawing.Point(8, 4);
            this.lblVouType.Name = "lblVouType";
            this.lblVouType.Size = new System.Drawing.Size(58, 14);
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
            // UclChequeReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclChequeReturn";
            this.Size = new System.Drawing.Size(966, 733);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlVouTypeNo.ResumeLayout(false);
            this.pnlVouTypeNo.PerformLayout();
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            this.pnlChequeReturn.ResumeLayout(false);
            this.pnlChequeReturn.PerformLayout();
            this.pnlVou.ResumeLayout(false);
            this.pnlVou.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlVouTypeNo;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl12;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl11;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl7;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtAddress2;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbBranch;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbBankAccount;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtChequeNumber;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbBank;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmtNotAdjusted;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmountReceived;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNarration;
        private System.Windows.Forms.DateTimePicker datePickerChequeDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl13;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl6;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotalBalance;
        private System.Windows.Forms.Label lblTotalBalance;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Label lblNoofRows;
        private EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl mpMSVC;
        private System.Windows.Forms.Panel pnlChequeReturn;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtReturnCharges;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private System.Windows.Forms.Panel pnlVou;
        private CommonControls.PSLableWithBorderMiddleLeft txtVouType;
        private CommonControls.PSTextBox txtVoucherSeries;
        private CommonControls.PSLabel psLabel8;
        private CommonControls.PSLabel lblVouDate;
        private CommonControls.PSLabel lblVouNumber;
        private CommonControls.PSLabel lblVouType;
        private System.Windows.Forms.DateTimePicker datePickerBillDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouchernumber;
    }
}
