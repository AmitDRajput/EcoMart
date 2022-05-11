namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclBankReceipt
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclBankReceipt));
            this.pnlVouTypeNo = new System.Windows.Forms.Panel();
            this.mPlbl13 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl12 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl11 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl7 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl6 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.pnlVou = new System.Windows.Forms.Panel();
            this.mPlbl10 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl9 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl8 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtVouType = new System.Windows.Forms.TextBox();
            this.datePickerBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtVouchernumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.mcbBranch = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mcbBankAccount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.datePickerChequeDate = new System.Windows.Forms.DateTimePicker();
            this.txtChequeNumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.mcbBank = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mcbCreditor = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtAmtNotAdjusted = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtAmountReceived = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtNarration = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.btnModify = new System.Windows.Forms.Button();
            this.mpPVCTemp = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl();
            this.txtTotalBalance = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.lblTotalBalance = new System.Windows.Forms.Label();
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblNoofRows = new System.Windows.Forms.Label();
            this.ttBankReceipt = new System.Windows.Forms.ToolTip(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.mpMSVC = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.mpMSCSale = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlVouTypeNo.SuspendLayout();
            this.pnlVou.SuspendLayout();
            this.pnlAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(965, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Controls.Add(this.btnModify);
            this.MMBottomPanel.Controls.Add(this.txtTotalBalance);
            this.MMBottomPanel.Controls.Add(this.lblTotalBalance);
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.lblNoofRows);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 656);
            this.MMBottomPanel.Size = new System.Drawing.Size(967, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblNoofRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblTotalBalance, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotalBalance, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.btnModify, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.mpMSCSale);
            this.MMMainPanel.Controls.Add(this.mpMSVC);
            this.MMMainPanel.Controls.Add(this.pnlVouTypeNo);
            this.MMMainPanel.Controls.Add(this.mpPVCTemp);
            this.MMMainPanel.Size = new System.Drawing.Size(967, 604);
            this.MMMainPanel.Controls.SetChildIndex(this.mpPVCTemp, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlVouTypeNo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSVC, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSCSale, 0);
            // 
            // pnlVouTypeNo
            // 
            this.pnlVouTypeNo.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlVouTypeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.pnlVouTypeNo.Controls.Add(this.pnlVou);
            this.pnlVouTypeNo.Controls.Add(this.pnlAddress);
            this.pnlVouTypeNo.Controls.Add(this.mcbBranch);
            this.pnlVouTypeNo.Controls.Add(this.mcbBankAccount);
            this.pnlVouTypeNo.Controls.Add(this.datePickerChequeDate);
            this.pnlVouTypeNo.Controls.Add(this.txtChequeNumber);
            this.pnlVouTypeNo.Controls.Add(this.mcbBank);
            this.pnlVouTypeNo.Controls.Add(this.mcbCreditor);
            this.pnlVouTypeNo.Controls.Add(this.txtAmtNotAdjusted);
            this.pnlVouTypeNo.Controls.Add(this.txtAmountReceived);
            this.pnlVouTypeNo.Controls.Add(this.txtNarration);
            this.pnlVouTypeNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVouTypeNo.Location = new System.Drawing.Point(0, 0);
            this.pnlVouTypeNo.Name = "pnlVouTypeNo";
            this.pnlVouTypeNo.Size = new System.Drawing.Size(965, 245);
            this.pnlVouTypeNo.TabIndex = 0;
            // 
            // mPlbl13
            // 
            this.mPlbl13.AutoSize = true;
            this.mPlbl13.Location = new System.Drawing.Point(41, 208);
            this.mPlbl13.Name = "mPlbl13";
            this.mPlbl13.Size = new System.Drawing.Size(82, 19);
            this.mPlbl13.TabIndex = 17;
            this.mPlbl13.Text = "Narra&tion";
            // 
            // mPlbl12
            // 
            this.mPlbl12.AutoSize = true;
            this.mPlbl12.Location = new System.Drawing.Point(76, 129);
            this.mPlbl12.Name = "mPlbl12";
            this.mPlbl12.Size = new System.Drawing.Size(47, 19);
            this.mPlbl12.TabIndex = 9;
            this.mPlbl12.Text = "Ban&k";
            // 
            // mPlbl11
            // 
            this.mPlbl11.AutoSize = true;
            this.mPlbl11.Location = new System.Drawing.Point(680, 148);
            this.mPlbl11.Name = "mPlbl11";
            this.mPlbl11.Size = new System.Drawing.Size(95, 19);
            this.mPlbl11.TabIndex = 1057;
            this.mPlbl11.Text = "On Account";
            // 
            // mPlbl7
            // 
            this.mPlbl7.AutoSize = true;
            this.mPlbl7.Location = new System.Drawing.Point(60, 157);
            this.mPlbl7.Name = "mPlbl7";
            this.mPlbl7.Size = new System.Drawing.Size(63, 19);
            this.mPlbl7.TabIndex = 11;
            this.mPlbl7.Text = "Branc&h";
            // 
            // mPlbl6
            // 
            this.mPlbl6.AutoSize = true;
            this.mPlbl6.Location = new System.Drawing.Point(309, 184);
            this.mPlbl6.Name = "mPlbl6";
            this.mPlbl6.Size = new System.Drawing.Size(43, 19);
            this.mPlbl6.TabIndex = 15;
            this.mPlbl6.Text = "Date";
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(62, 184);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(61, 19);
            this.mPlbl5.TabIndex = 13;
            this.mPlbl5.Text = "&Chq No";
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(55, 103);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(68, 19);
            this.mPlbl4.TabIndex = 7;
            this.mPlbl4.Text = "&Amount";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(55, 68);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(68, 19);
            this.mPlbl3.TabIndex = 5;
            this.mPlbl3.Text = "Address";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(7, 43);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(116, 19);
            this.mPlbl2.TabIndex = 3;
            this.mPlbl2.Text = "Account &Name";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(11, 11);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(112, 19);
            this.mPlbl1.TabIndex = 1;
            this.mPlbl1.Text = "&Bank Account";
            // 
            // pnlVou
            // 
            this.pnlVou.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlVou.Controls.Add(this.mPlbl10);
            this.pnlVou.Controls.Add(this.mPlbl9);
            this.pnlVou.Controls.Add(this.mPlbl8);
            this.pnlVou.Controls.Add(this.txtVouType);
            this.pnlVou.Controls.Add(this.datePickerBillDate);
            this.pnlVou.Controls.Add(this.txtVouchernumber);
            this.pnlVou.Location = new System.Drawing.Point(744, 1);
            this.pnlVou.Name = "pnlVou";
            this.pnlVou.Size = new System.Drawing.Size(216, 86);
            this.pnlVou.TabIndex = 1049;
            // 
            // mPlbl10
            // 
            this.mPlbl10.AutoSize = true;
            this.mPlbl10.Location = new System.Drawing.Point(13, 61);
            this.mPlbl10.Name = "mPlbl10";
            this.mPlbl10.Size = new System.Drawing.Size(76, 19);
            this.mPlbl10.TabIndex = 0;
            this.mPlbl10.Text = "Vou &Date";
            // 
            // mPlbl9
            // 
            this.mPlbl9.AutoSize = true;
            this.mPlbl9.Location = new System.Drawing.Point(26, 33);
            this.mPlbl9.Name = "mPlbl9";
            this.mPlbl9.Size = new System.Drawing.Size(62, 19);
            this.mPlbl9.TabIndex = 1083;
            this.mPlbl9.Text = "Vou No";
            // 
            // mPlbl8
            // 
            this.mPlbl8.AutoSize = true;
            this.mPlbl8.Location = new System.Drawing.Point(10, 7);
            this.mPlbl8.Name = "mPlbl8";
            this.mPlbl8.Size = new System.Drawing.Size(78, 19);
            this.mPlbl8.TabIndex = 1083;
            this.mPlbl8.Text = "Vou Type";
            // 
            // txtVouType
            // 
            this.txtVouType.BackColor = System.Drawing.Color.Snow;
            this.txtVouType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouType.Enabled = false;
            this.txtVouType.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVouType.ForeColor = System.Drawing.Color.Navy;
            this.txtVouType.Location = new System.Drawing.Point(93, 3);
            this.txtVouType.Name = "txtVouType";
            this.txtVouType.ReadOnly = true;
            this.txtVouType.Size = new System.Drawing.Size(113, 26);
            this.txtVouType.TabIndex = 132;
            // 
            // datePickerBillDate
            // 
            this.datePickerBillDate.CustomFormat = "dd/MM/yy";
            this.datePickerBillDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerBillDate.Location = new System.Drawing.Point(93, 57);
            this.datePickerBillDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerBillDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerBillDate.Name = "datePickerBillDate";
            this.datePickerBillDate.Size = new System.Drawing.Size(113, 26);
            this.datePickerBillDate.TabIndex = 1;
            this.datePickerBillDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // txtVouchernumber
            // 
            this.txtVouchernumber.BackColor = System.Drawing.Color.Snow;
            this.txtVouchernumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouchernumber.Enabled = false;
            this.txtVouchernumber.Location = new System.Drawing.Point(93, 31);
            this.txtVouchernumber.MaxLength = 50;
            this.txtVouchernumber.Name = "txtVouchernumber";
            this.txtVouchernumber.ReadOnly = true;
            this.txtVouchernumber.Size = new System.Drawing.Size(113, 24);
            this.txtVouchernumber.TabIndex = 0;
            this.txtVouchernumber.TabStop = false;
            this.txtVouchernumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtVouchernumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVouchernumber_KeyDown);
            // 
            // pnlAddress
            // 
            this.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddress.Controls.Add(this.txtAddress1);
            this.pnlAddress.Controls.Add(this.txtAddress2);
            this.pnlAddress.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddress.Location = new System.Drawing.Point(139, 63);
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
            this.mcbBranch.Location = new System.Drawing.Point(139, 155);
            this.mcbBranch.Margin = new System.Windows.Forms.Padding(4);
            this.mcbBranch.Name = "mcbBranch";
            this.mcbBranch.SelectedID = null;
            this.mcbBranch.ShowNew = true;
            this.mcbBranch.Size = new System.Drawing.Size(394, 26);
            this.mcbBranch.SourceDataString = null;
            this.mcbBranch.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbBranch.TabIndex = 12;
            this.mcbBranch.UserControlToShow = null;
            this.mcbBranch.ValueColumnNo = 0;
            this.mcbBranch.ItemAddedEdited += new System.EventHandler(this.mcbBranch_ItemAddedEdited);
            this.mcbBranch.EnterKeyPressed += new System.EventHandler(this.mcbBranch_EnterKeyPressed);
            this.mcbBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcbBranch_KeyDown);
            // 
            // mcbBankAccount
            // 
            this.mcbBankAccount.ColumnWidth = null;
            this.mcbBankAccount.DataSource = null;
            this.mcbBankAccount.DisplayColumnNo = 1;
            this.mcbBankAccount.DropDownHeight = 200;
            this.mcbBankAccount.Location = new System.Drawing.Point(139, 9);
            this.mcbBankAccount.Margin = new System.Windows.Forms.Padding(4);
            this.mcbBankAccount.Name = "mcbBankAccount";
            this.mcbBankAccount.SelectedID = null;
            this.mcbBankAccount.ShowNew = false;
            this.mcbBankAccount.Size = new System.Drawing.Size(393, 26);
            this.mcbBankAccount.SourceDataString = null;
            this.mcbBankAccount.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbBankAccount.TabIndex = 2;
            this.mcbBankAccount.UserControlToShow = null;
            this.mcbBankAccount.ValueColumnNo = 0;
            this.mcbBankAccount.EnterKeyPressed += new System.EventHandler(this.mcbBankAccount_EnterKeyPressed);
            // 
            // datePickerChequeDate
            // 
            this.datePickerChequeDate.CustomFormat = "";
            this.datePickerChequeDate.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerChequeDate.Location = new System.Drawing.Point(357, 181);
            this.datePickerChequeDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerChequeDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerChequeDate.Name = "datePickerChequeDate";
            this.datePickerChequeDate.Size = new System.Drawing.Size(137, 27);
            this.datePickerChequeDate.TabIndex = 16;
            this.datePickerChequeDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.datePickerChequeDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datePickerChequeDate_KeyDown);
            // 
            // txtChequeNumber
            // 
            this.txtChequeNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChequeNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtChequeNumber.Location = new System.Drawing.Point(139, 183);
            this.txtChequeNumber.MaxLength = 50;
            this.txtChequeNumber.Name = "txtChequeNumber";
            this.txtChequeNumber.Size = new System.Drawing.Size(156, 24);
            this.txtChequeNumber.TabIndex = 14;
            this.txtChequeNumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtChequeNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChequeNumber_KeyDown);
            // 
            // mcbBank
            // 
            this.mcbBank.ColumnWidth = null;
            this.mcbBank.DataSource = null;
            this.mcbBank.DisplayColumnNo = 1;
            this.mcbBank.DropDownHeight = 200;
            this.mcbBank.Location = new System.Drawing.Point(139, 128);
            this.mcbBank.Margin = new System.Windows.Forms.Padding(4);
            this.mcbBank.Name = "mcbBank";
            this.mcbBank.SelectedID = null;
            this.mcbBank.ShowNew = true;
            this.mcbBank.Size = new System.Drawing.Size(394, 26);
            this.mcbBank.SourceDataString = null;
            this.mcbBank.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbBank.TabIndex = 10;
            this.mcbBank.UserControlToShow = null;
            this.mcbBank.ValueColumnNo = 0;
            this.mcbBank.ItemAddedEdited += new System.EventHandler(this.mcbBank_ItemAddedEdited);
            this.mcbBank.EnterKeyPressed += new System.EventHandler(this.mcbBank_EnterKeyPressed);
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(139, 36);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = false;
            this.mcbCreditor.Size = new System.Drawing.Size(394, 26);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 4;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.ItemAddedEdited += new System.EventHandler(this.mcbCreditor_ItemAddedEdited);
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            this.mcbCreditor.SeletectIndexChanged += new System.EventHandler(this.mcbCreditor_SeletectIndexChanged);
            // 
            // txtAmtNotAdjusted
            // 
            this.txtAmtNotAdjusted.BackColor = System.Drawing.SystemColors.Window;
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
            this.txtAmountReceived.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountReceived.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAmountReceived.Location = new System.Drawing.Point(139, 101);
            this.txtAmountReceived.MaxLength = 15;
            this.txtAmountReceived.Name = "txtAmountReceived";
            this.txtAmountReceived.Size = new System.Drawing.Size(156, 26);
            this.txtAmountReceived.TabIndex = 8;
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
            this.txtNarration.Location = new System.Drawing.Point(139, 209);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(357, 24);
            this.txtNarration.TabIndex = 18;
            this.txtNarration.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(863, -1);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(74, 22);
            this.btnModify.TabIndex = 139;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // mpPVCTemp
            // 
            this.mpPVCTemp.AllowNewBatch = true;
            this.mpPVCTemp.AutoScroll = true;
            this.mpPVCTemp.BatchColumnName = "Col_BatchNumber";
            this.mpPVCTemp.BatchGridShowColumnName = null;
            this.mpPVCTemp.BatchListGridWidth = 16;
            this.mpPVCTemp.DataSourceBatchList = null;
            this.mpPVCTemp.DataSourceMain = null;
            this.mpPVCTemp.DataSourceProductList = null;
            this.mpPVCTemp.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpPVCTemp.DoubleColumnNames")));
            this.mpPVCTemp.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpPVCTemp.IsAllowDelete = true;
            this.mpPVCTemp.IsAllowNewRow = false;
            this.mpPVCTemp.Location = new System.Drawing.Point(4, 279);
            this.mpPVCTemp.MainGridSoldQuantityColumnName = "";
            this.mpPVCTemp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpPVCTemp.MinimumSize = new System.Drawing.Size(420, 300);
            this.mpPVCTemp.ModuleNumber = PharmaSYSRetailPlus.Common.ModuleNumber.None;
            this.mpPVCTemp.Name = "mpPVCTemp";
            this.mpPVCTemp.NewRowColumnName = null;
            this.mpPVCTemp.NextRowColumn = 0;
            this.mpPVCTemp.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpPVCTemp.NumericColumnNames")));
            this.mpPVCTemp.OperationMode = PharmaSYSRetailPlus.Common.OperationMode.None;
            this.mpPVCTemp.ProductGridClosingStockColumnName = "";
            this.mpPVCTemp.ProductListFilter = null;
            this.mpPVCTemp.ProductListGridWidth = 600;
            this.mpPVCTemp.Size = new System.Drawing.Size(420, 300);
            this.mpPVCTemp.TabIndex = 1;
            this.mpPVCTemp.Visible = false;
            // 
            // txtTotalBalance
            // 
            this.txtTotalBalance.BackColor = System.Drawing.SystemColors.Window;
            this.txtTotalBalance.Enabled = false;
            this.txtTotalBalance.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBalance.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTotalBalance.Location = new System.Drawing.Point(552, 0);
            this.txtTotalBalance.MaxLength = 15;
            this.txtTotalBalance.Name = "txtTotalBalance";
            this.txtTotalBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalBalance.Size = new System.Drawing.Size(132, 22);
            this.txtTotalBalance.TabIndex = 1011;
            this.txtTotalBalance.TabStop = false;
            this.txtTotalBalance.Tag = "0.00";
            this.txtTotalBalance.Text = "0.00";
            this.txtTotalBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalBalance
            // 
            this.lblTotalBalance.AutoSize = true;
            this.lblTotalBalance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBalance.Location = new System.Drawing.Point(445, 2);
            this.lblTotalBalance.Name = "lblTotalBalance";
            this.lblTotalBalance.Size = new System.Drawing.Size(88, 15);
            this.lblTotalBalance.TabIndex = 1010;
            this.lblTotalBalance.Text = "Total Balance";
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.Snow;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(798, 0);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 26);
            this.txtNoOfRows.TabIndex = 1006;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNoofRows
            // 
            this.lblNoofRows.AutoSize = true;
            this.lblNoofRows.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRows.Location = new System.Drawing.Point(718, 2);
            this.lblNoofRows.Name = "lblNoofRows";
            this.lblNoofRows.Size = new System.Drawing.Size(74, 15);
            this.lblNoofRows.TabIndex = 1005;
            this.lblNoofRows.Text = "No Of Rows";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 3);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(2, 16);
            this.lblMessage.TabIndex = 1012;
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
            this.mpMSVC.Location = new System.Drawing.Point(0, 245);
            this.mpMSVC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpMSVC.MinimumSize = new System.Drawing.Size(488, 386);
            this.mpMSVC.Name = "mpMSVC";
            this.mpMSVC.NextRowColumn = 0;
            this.mpMSVC.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC.NumericColumnNames")));
            this.mpMSVC.Size = new System.Drawing.Size(965, 386);
            this.mpMSVC.SubGridWidth = 450;
            this.mpMSVC.TabIndex = 1059;
            this.mpMSVC.ViewControl = null;
            this.mpMSVC.Visible = false;
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
            this.mpMSCSale.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMSCSale.IsAllowDelete = false;
            this.mpMSCSale.IsAllowNewRow = false;
            this.mpMSCSale.Location = new System.Drawing.Point(0, 245);
            this.mpMSCSale.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpMSCSale.MinimumSize = new System.Drawing.Size(488, 300);
            this.mpMSCSale.Name = "mpMSCSale";
            this.mpMSCSale.NextRowColumn = 0;
            this.mpMSCSale.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSCSale.NumericColumnNames")));
            this.mpMSCSale.Size = new System.Drawing.Size(965, 357);
            this.mpMSCSale.SubGridWidth = 450;
            this.mpMSCSale.TabIndex = 1061;
            this.mpMSCSale.ViewControl = null;
            this.mpMSCSale.Visible = false;
            this.mpMSCSale.OnCellValueChangeCommited += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl.CellValueChangeCommited(this.mpMSCSale_OnCellValueChangeCommited);
            this.mpMSCSale.OnCellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.mpMSCSale_OnCellEnter);
            this.mpMSCSale.OnCellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.mpMSCSale_OnCellLeave);
            // 
            // UclBankReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclBankReceipt";
            this.Size = new System.Drawing.Size(967, 679);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlVouTypeNo.ResumeLayout(false);
            this.pnlVouTypeNo.PerformLayout();
            this.pnlVou.ResumeLayout(false);
            this.pnlVou.PerformLayout();
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlVouTypeNo;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmtNotAdjusted;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmountReceived;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNarration;
        private System.Windows.Forms.Button btnModify;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbBankAccount;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtChequeNumber;
        private System.Windows.Forms.DateTimePicker datePickerChequeDate;
        private System.Windows.Forms.ToolTip ttBankReceipt;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Label lblNoofRows;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotalBalance;
        private System.Windows.Forms.Label lblTotalBalance;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl mpPVCTemp;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbBranch;
        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlVou;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl10;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl9;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl8;
        private System.Windows.Forms.TextBox txtVouType;
        private System.Windows.Forms.DateTimePicker datePickerBillDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouchernumber;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl6;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl11;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl7;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl12;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl13;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl mpMSVC;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl mpMSCSale;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbBank;
    }
}
