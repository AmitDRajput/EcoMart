namespace EcoMart.InterfaceLayer
{
    partial class UclBankExpenses
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclBankExpenses));
            this.pnlNameAddress = new System.Windows.Forms.Panel();
            this.PrintGrid = new System.Windows.Forms.DataGridView();
            this.pnlVou = new System.Windows.Forms.Panel();
            this.txtVouType = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.txtVoucherSeries = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.psLabel8 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouDate = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouNumber = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouType = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.datePickerBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtVouchernumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.mPlbl6 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.datePickerChequeDate = new System.Windows.Forms.DateTimePicker();
            this.txtChequeNumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbBankAccount = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbCreditor = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtNarration = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.mpMainSubViewControl1 = new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.txtTotalCredit = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblTotalDebit = new System.Windows.Forms.Label();
            this.lblNoofRows = new System.Windows.Forms.Label();
            this.lblTotalCredit = new System.Windows.Forms.Label();
            this.txtTotalDebit = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlNameAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrintGrid)).BeginInit();
            this.pnlVou.SuspendLayout();
            this.pnlAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(961, 24);
            this.headerLabel1.TabIndex = 1;
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtTotalCredit);
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.lblTotalDebit);
            this.MMBottomPanel.Controls.Add(this.lblNoofRows);
            this.MMBottomPanel.Controls.Add(this.lblTotalCredit);
            this.MMBottomPanel.Controls.Add(this.txtTotalDebit);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 553);
            this.MMBottomPanel.Size = new System.Drawing.Size(963, 63);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblRightSideFooterMsg, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotalDebit, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblTotalCredit, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblNoofRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblTotalDebit, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotalCredit, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.mpMainSubViewControl1);
            this.MMMainPanel.Controls.Add(this.pnlNameAddress);
            this.MMMainPanel.Size = new System.Drawing.Size(963, 490);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlNameAddress, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMainSubViewControl1, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(416, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(545, 19);
            // 
            // pnlNameAddress
            // 
            this.pnlNameAddress.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlNameAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNameAddress.Controls.Add(this.PrintGrid);
            this.pnlNameAddress.Controls.Add(this.pnlVou);
            this.pnlNameAddress.Controls.Add(this.mPlbl6);
            this.pnlNameAddress.Controls.Add(this.mPlbl5);
            this.pnlNameAddress.Controls.Add(this.datePickerChequeDate);
            this.pnlNameAddress.Controls.Add(this.txtChequeNumber);
            this.pnlNameAddress.Controls.Add(this.psLabel1);
            this.pnlNameAddress.Controls.Add(this.mcbBankAccount);
            this.pnlNameAddress.Controls.Add(this.pnlAddress);
            this.pnlNameAddress.Controls.Add(this.mPlbl4);
            this.pnlNameAddress.Controls.Add(this.mPlbl3);
            this.pnlNameAddress.Controls.Add(this.mPlbl2);
            this.pnlNameAddress.Controls.Add(this.mPlbl1);
            this.pnlNameAddress.Controls.Add(this.mcbCreditor);
            this.pnlNameAddress.Controls.Add(this.txtAmount);
            this.pnlNameAddress.Controls.Add(this.txtNarration);
            this.pnlNameAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNameAddress.Location = new System.Drawing.Point(0, 0);
            this.pnlNameAddress.Name = "pnlNameAddress";
            this.pnlNameAddress.Size = new System.Drawing.Size(961, 203);
            this.pnlNameAddress.TabIndex = 1;
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
            this.PrintGrid.Location = new System.Drawing.Point(604, 32);
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
            this.PrintGrid.TabIndex = 1110;
            this.PrintGrid.Visible = false;
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
            this.psLabel8.Size = new System.Drawing.Size(70, 16);
            this.psLabel8.TabIndex = 1085;
            this.psLabel8.Text = "Vou Series";
            // 
            // lblVouDate
            // 
            this.lblVouDate.AutoSize = true;
            this.lblVouDate.Location = new System.Drawing.Point(10, 54);
            this.lblVouDate.Name = "lblVouDate";
            this.lblVouDate.Size = new System.Drawing.Size(61, 16);
            this.lblVouDate.TabIndex = 1083;
            this.lblVouDate.Text = "Vou &Date";
            // 
            // lblVouNumber
            // 
            this.lblVouNumber.AutoSize = true;
            this.lblVouNumber.Location = new System.Drawing.Point(24, 29);
            this.lblVouNumber.Name = "lblVouNumber";
            this.lblVouNumber.Size = new System.Drawing.Size(50, 16);
            this.lblVouNumber.TabIndex = 1083;
            this.lblVouNumber.Text = "Vou No";
            // 
            // lblVouType
            // 
            this.lblVouType.AutoSize = true;
            this.lblVouType.Location = new System.Drawing.Point(8, 4);
            this.lblVouType.Name = "lblVouType";
            this.lblVouType.Size = new System.Drawing.Size(63, 16);
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
            this.datePickerBillDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datePickerBillDate_KeyDown);
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
            // mPlbl6
            // 
            this.mPlbl6.AutoSize = true;
            this.mPlbl6.Location = new System.Drawing.Point(318, 138);
            this.mPlbl6.Name = "mPlbl6";
            this.mPlbl6.Size = new System.Drawing.Size(39, 16);
            this.mPlbl6.TabIndex = 1094;
            this.mPlbl6.Text = " Date";
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(66, 138);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(52, 16);
            this.mPlbl5.TabIndex = 1092;
            this.mPlbl5.Text = "&Chq No";
            // 
            // datePickerChequeDate
            // 
            this.datePickerChequeDate.CustomFormat = "dd/MM/yyyy";
            this.datePickerChequeDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerChequeDate.Location = new System.Drawing.Point(377, 134);
            this.datePickerChequeDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerChequeDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerChequeDate.Name = "datePickerChequeDate";
            this.datePickerChequeDate.Size = new System.Drawing.Size(122, 26);
            this.datePickerChequeDate.TabIndex = 4;
            this.datePickerChequeDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.datePickerChequeDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datePickerChequeDate_KeyDown);
            // 
            // txtChequeNumber
            // 
            this.txtChequeNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChequeNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtChequeNumber.Location = new System.Drawing.Point(145, 137);
            this.txtChequeNumber.MaxLength = 50;
            this.txtChequeNumber.Name = "txtChequeNumber";
            this.txtChequeNumber.Size = new System.Drawing.Size(162, 24);
            this.txtChequeNumber.TabIndex = 3;
            this.txtChequeNumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtChequeNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChequeNumber_KeyDown);
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(35, 13);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(78, 16);
            this.psLabel1.TabIndex = 1090;
            this.psLabel1.Text = "&Bank Name";
            // 
            // mcbBankAccount
            // 
            this.mcbBankAccount.ColumnWidth = null;
            this.mcbBankAccount.DataSource = null;
            this.mcbBankAccount.DisplayColumnNo = 1;
            this.mcbBankAccount.DropDownHeight = 200;
            this.mcbBankAccount.Location = new System.Drawing.Point(145, 11);
            this.mcbBankAccount.Margin = new System.Windows.Forms.Padding(4);
            this.mcbBankAccount.Name = "mcbBankAccount";
            this.mcbBankAccount.SelectedID = null;
            this.mcbBankAccount.ShowNew = false;
            this.mcbBankAccount.Size = new System.Drawing.Size(354, 22);
            this.mcbBankAccount.SourceDataString = null;
            this.mcbBankAccount.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbBankAccount.TabIndex = 0;
            this.mcbBankAccount.UserControlToShow = null;
            this.mcbBankAccount.ValueColumnNo = 0;
            this.mcbBankAccount.EnterKeyPressed += new System.EventHandler(this.mcbBankAccount_EnterKeyPressed);
            // 
            // pnlAddress
            // 
            this.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddress.Controls.Add(this.txtAddress1);
            this.pnlAddress.Controls.Add(this.txtAddress2);
            this.pnlAddress.Enabled = false;
            this.pnlAddress.Location = new System.Drawing.Point(145, 69);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(323, 36);
            this.pnlAddress.TabIndex = 1089;
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
            this.mPlbl4.Location = new System.Drawing.Point(51, 167);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(68, 16);
            this.mPlbl4.TabIndex = 1087;
            this.mPlbl4.Text = "Narra&tion";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(65, 112);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(57, 16);
            this.mPlbl3.TabIndex = 1086;
            this.mPlbl3.Text = "&Amount";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(65, 77);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(58, 16);
            this.mPlbl2.TabIndex = 1085;
            this.mPlbl2.Text = "Address";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(17, 48);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(96, 16);
            this.mPlbl1.TabIndex = 1084;
            this.mPlbl1.Text = "Account &Name";
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(145, 40);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = true;
            this.mcbCreditor.Size = new System.Drawing.Size(354, 22);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 1;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.SeletectIndexChanged += new System.EventHandler(this.mcbCreditor_SeletectIndexChanged);
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            this.mcbCreditor.ItemAddedEdited += new System.EventHandler(this.mcbCreditor_ItemAddedEdited);
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.ForeColor = System.Drawing.Color.Black;
            this.txtAmount.Location = new System.Drawing.Point(145, 108);
            this.txtAmount.MaxLength = 15;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(162, 26);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.TabStop = false;
            this.txtAmount.Tag = "0.00";
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.Location = new System.Drawing.Point(145, 165);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(323, 24);
            this.txtNarration.TabIndex = 5;
            this.txtNarration.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // mpMainSubViewControl1
            // 
            this.mpMainSubViewControl1.AutoScroll = true;
            this.mpMainSubViewControl1.BackColor = System.Drawing.Color.Linen;
            this.mpMainSubViewControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mpMainSubViewControl1.DataSource = null;
            this.mpMainSubViewControl1.DataSourceMain = null;
            this.mpMainSubViewControl1.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.DateColumnNames")));
            this.mpMainSubViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMainSubViewControl1.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.DoubleColumnNames")));
            this.mpMainSubViewControl1.Filter = null;
            this.mpMainSubViewControl1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMainSubViewControl1.IsAllowDelete = true;
            this.mpMainSubViewControl1.IsAllowNewRow = true;
            this.mpMainSubViewControl1.Location = new System.Drawing.Point(0, 203);
            this.mpMainSubViewControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mpMainSubViewControl1.MinimumSize = new System.Drawing.Size(487, 321);
            this.mpMainSubViewControl1.Name = "mpMainSubViewControl1";
            this.mpMainSubViewControl1.NextRowColumn = 4;
            this.mpMainSubViewControl1.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.NumericColumnNames")));
            this.mpMainSubViewControl1.Size = new System.Drawing.Size(961, 321);
            this.mpMainSubViewControl1.SubGridWidth = 400;
            this.mpMainSubViewControl1.TabIndex = 144;
            this.mpMainSubViewControl1.ViewControl = null;
            this.mpMainSubViewControl1.OnCellValueChangeCommited += new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl.CellValueChangeCommited(this.mpMainSubViewControl1_OnCellValueChangeCommited);
            this.mpMainSubViewControl1.OnRowDeleted += new System.EventHandler(this.mpMainSubViewControl_OnRowDeleted);
            // 
            // txtTotalCredit
            // 
            this.txtTotalCredit.BackColor = System.Drawing.SystemColors.Window;
            this.txtTotalCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalCredit.Enabled = false;
            this.txtTotalCredit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCredit.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTotalCredit.Location = new System.Drawing.Point(465, -1);
            this.txtTotalCredit.MaxLength = 15;
            this.txtTotalCredit.Name = "txtTotalCredit";
            this.txtTotalCredit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalCredit.Size = new System.Drawing.Size(120, 22);
            this.txtTotalCredit.TabIndex = 1019;
            this.txtTotalCredit.TabStop = false;
            this.txtTotalCredit.Tag = "0.00";
            this.txtTotalCredit.Text = "0.00";
            this.txtTotalCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.Snow;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(671, -1);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 22);
            this.txtNoOfRows.TabIndex = 1015;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalDebit
            // 
            this.lblTotalDebit.AutoSize = true;
            this.lblTotalDebit.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDebit.Location = new System.Drawing.Point(176, 1);
            this.lblTotalDebit.Name = "lblTotalDebit";
            this.lblTotalDebit.Size = new System.Drawing.Size(73, 15);
            this.lblTotalDebit.TabIndex = 1016;
            this.lblTotalDebit.Text = "Total Debit";
            // 
            // lblNoofRows
            // 
            this.lblNoofRows.AutoSize = true;
            this.lblNoofRows.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRows.Location = new System.Drawing.Point(591, 2);
            this.lblNoofRows.Name = "lblNoofRows";
            this.lblNoofRows.Size = new System.Drawing.Size(74, 15);
            this.lblNoofRows.TabIndex = 1014;
            this.lblNoofRows.Text = "No Of Rows";
            // 
            // lblTotalCredit
            // 
            this.lblTotalCredit.AutoSize = true;
            this.lblTotalCredit.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCredit.Location = new System.Drawing.Point(382, 1);
            this.lblTotalCredit.Name = "lblTotalCredit";
            this.lblTotalCredit.Size = new System.Drawing.Size(77, 15);
            this.lblTotalCredit.TabIndex = 1018;
            this.lblTotalCredit.Text = "Total Credit";
            // 
            // txtTotalDebit
            // 
            this.txtTotalDebit.BackColor = System.Drawing.SystemColors.Window;
            this.txtTotalDebit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalDebit.Enabled = false;
            this.txtTotalDebit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDebit.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTotalDebit.Location = new System.Drawing.Point(256, -2);
            this.txtTotalDebit.MaxLength = 15;
            this.txtTotalDebit.Name = "txtTotalDebit";
            this.txtTotalDebit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalDebit.Size = new System.Drawing.Size(120, 22);
            this.txtTotalDebit.TabIndex = 1017;
            this.txtTotalDebit.TabStop = false;
            this.txtTotalDebit.Tag = "0.00";
            this.txtTotalDebit.Text = "0.00";
            this.txtTotalDebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // UclBankExpenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclBankExpenses";
            this.Size = new System.Drawing.Size(963, 616);
            this.Load += new System.EventHandler(this.UclBankExpenses_Load);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlNameAddress.ResumeLayout(false);
            this.pnlNameAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrintGrid)).EndInit();
            this.pnlVou.ResumeLayout(false);
            this.pnlVou.PerformLayout();
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlNameAddress;
        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtAddress2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmount;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNarration;
        private EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl mpMainSubViewControl1;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotalCredit;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Label lblTotalDebit;
        private System.Windows.Forms.Label lblNoofRows;
        private System.Windows.Forms.Label lblTotalCredit;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotalDebit;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbBankAccount;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl6;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private System.Windows.Forms.DateTimePicker datePickerChequeDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtChequeNumber;
        private System.Windows.Forms.Panel pnlVou;
        private CommonControls.PSLableWithBorderMiddleLeft txtVouType;
        private CommonControls.PSTextBox txtVoucherSeries;
        private CommonControls.PSLabel psLabel8;
        private CommonControls.PSLabel lblVouDate;
        private CommonControls.PSLabel lblVouNumber;
        private CommonControls.PSLabel lblVouType;
        private System.Windows.Forms.DateTimePicker datePickerBillDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouchernumber;
        private System.Windows.Forms.DataGridView PrintGrid;
    }
}
