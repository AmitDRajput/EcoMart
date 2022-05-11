namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclStatementPartywiseSale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStatementPartywiseSale));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.txtStmtNumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ViewToDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.ViewFromDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtViewText = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.lblViewTo = new System.Windows.Forms.Label();
            this.lblViewFrom = new System.Windows.Forms.Label();
            this.lblViewText = new System.Windows.Forms.Label();
            this.pnlAmounts = new System.Windows.Forms.Panel();
            this.txtCashAmount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtCreditAmount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtCreditStatementAmount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtVat12point5per = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtVat5per = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtNumberOfBills = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtReportTotalAmount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.psLabel10 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel9 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel8 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel7 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel6 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.pnlMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.mcbCreditor = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.lblParty = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblToDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblFromDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate(this.components);
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.dgvReportList = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlAmounts.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(942, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 641);
            this.MMBottomPanel.Size = new System.Drawing.Size(944, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlAmounts);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(944, 589);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlGo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlAmounts, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // pnlGo
            // 
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.txtStmtNumber);
            this.pnlGo.Controls.Add(this.label11);
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.txtViewText);
            this.pnlGo.Controls.Add(this.lblViewTo);
            this.pnlGo.Controls.Add(this.lblViewFrom);
            this.pnlGo.Controls.Add(this.lblViewText);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(942, 33);
            this.pnlGo.TabIndex = 1045;
            // 
            // txtStmtNumber
            // 
            this.txtStmtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStmtNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStmtNumber.Location = new System.Drawing.Point(566, 4);
            this.txtStmtNumber.MaxLength = 50;
            this.txtStmtNumber.Name = "txtStmtNumber";
            this.txtStmtNumber.Size = new System.Drawing.Size(61, 26);
            this.txtStmtNumber.TabIndex = 1082;
            this.txtStmtNumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtStmtNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStmtNumber_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(499, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 15);
            this.label11.TabIndex = 1081;
            this.label11.Text = "Stmt No.";
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.CausesValidation = false;
            this.ViewToDate.Location = new System.Drawing.Point(855, 4);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(95, 23);
            this.ViewToDate.TabIndex = 1080;
            this.ViewToDate.Text = "label";
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.CausesValidation = false;
            this.ViewFromDate.Location = new System.Drawing.Point(720, 4);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(95, 23);
            this.ViewFromDate.TabIndex = 1079;
            this.ViewFromDate.Text = "label";
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtViewText
            // 
            this.txtViewText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtViewText.Location = new System.Drawing.Point(101, 4);
            this.txtViewText.MaxLength = 50;
            this.txtViewText.Name = "txtViewText";
            this.txtViewText.Size = new System.Drawing.Size(286, 26);
            this.txtViewText.TabIndex = 1065;
            this.txtViewText.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            // 
            // lblViewTo
            // 
            this.lblViewTo.AutoSize = true;
            this.lblViewTo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewTo.Location = new System.Drawing.Point(827, 7);
            this.lblViewTo.Name = "lblViewTo";
            this.lblViewTo.Size = new System.Drawing.Size(22, 15);
            this.lblViewTo.TabIndex = 1062;
            this.lblViewTo.Text = "To";
            // 
            // lblViewFrom
            // 
            this.lblViewFrom.AutoSize = true;
            this.lblViewFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewFrom.Location = new System.Drawing.Point(677, 7);
            this.lblViewFrom.Name = "lblViewFrom";
            this.lblViewFrom.Size = new System.Drawing.Size(39, 15);
            this.lblViewFrom.TabIndex = 1061;
            this.lblViewFrom.Text = "From";
            // 
            // lblViewText
            // 
            this.lblViewText.AutoSize = true;
            this.lblViewText.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewText.Location = new System.Drawing.Point(55, 6);
            this.lblViewText.Name = "lblViewText";
            this.lblViewText.Size = new System.Drawing.Size(40, 15);
            this.lblViewText.TabIndex = 1045;
            this.lblViewText.Text = "Party";
            // 
            // pnlAmounts
            // 
            this.pnlAmounts.BackColor = System.Drawing.Color.SeaShell;
            this.pnlAmounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAmounts.Controls.Add(this.txtCashAmount);
            this.pnlAmounts.Controls.Add(this.txtCreditAmount);
            this.pnlAmounts.Controls.Add(this.txtCreditStatementAmount);
            this.pnlAmounts.Controls.Add(this.txtVat12point5per);
            this.pnlAmounts.Controls.Add(this.txtVat5per);
            this.pnlAmounts.Controls.Add(this.txtNumberOfBills);
            this.pnlAmounts.Controls.Add(this.txtReportTotalAmount);
            this.pnlAmounts.Controls.Add(this.psLabel10);
            this.pnlAmounts.Controls.Add(this.psLabel9);
            this.pnlAmounts.Controls.Add(this.psLabel8);
            this.pnlAmounts.Controls.Add(this.psLabel7);
            this.pnlAmounts.Controls.Add(this.psLabel6);
            this.pnlAmounts.Controls.Add(this.psLabel5);
            this.pnlAmounts.Controls.Add(this.psLabel4);
            this.pnlAmounts.Location = new System.Drawing.Point(595, 71);
            this.pnlAmounts.Name = "pnlAmounts";
            this.pnlAmounts.Size = new System.Drawing.Size(322, 365);
            this.pnlAmounts.TabIndex = 1066;
            // 
            // txtCashAmount
            // 
            this.txtCashAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCashAmount.CausesValidation = false;
            this.txtCashAmount.Location = new System.Drawing.Point(174, 288);
            this.txtCashAmount.Name = "txtCashAmount";
            this.txtCashAmount.Size = new System.Drawing.Size(130, 23);
            this.txtCashAmount.TabIndex = 1084;
            this.txtCashAmount.Text = "0.00";
            this.txtCashAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreditAmount
            // 
            this.txtCreditAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditAmount.CausesValidation = false;
            this.txtCreditAmount.Location = new System.Drawing.Point(174, 258);
            this.txtCreditAmount.Name = "txtCreditAmount";
            this.txtCreditAmount.Size = new System.Drawing.Size(130, 23);
            this.txtCreditAmount.TabIndex = 1083;
            this.txtCreditAmount.Text = "0.00";
            this.txtCreditAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreditStatementAmount
            // 
            this.txtCreditStatementAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditStatementAmount.CausesValidation = false;
            this.txtCreditStatementAmount.Location = new System.Drawing.Point(174, 229);
            this.txtCreditStatementAmount.Name = "txtCreditStatementAmount";
            this.txtCreditStatementAmount.Size = new System.Drawing.Size(130, 23);
            this.txtCreditStatementAmount.TabIndex = 1082;
            this.txtCreditStatementAmount.Text = "0.00";
            this.txtCreditStatementAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVat12point5per
            // 
            this.txtVat12point5per.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVat12point5per.CausesValidation = false;
            this.txtVat12point5per.Location = new System.Drawing.Point(174, 161);
            this.txtVat12point5per.Name = "txtVat12point5per";
            this.txtVat12point5per.Size = new System.Drawing.Size(130, 23);
            this.txtVat12point5per.TabIndex = 1081;
            this.txtVat12point5per.Text = "0.00";
            this.txtVat12point5per.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVat5per
            // 
            this.txtVat5per.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVat5per.CausesValidation = false;
            this.txtVat5per.Location = new System.Drawing.Point(174, 131);
            this.txtVat5per.Name = "txtVat5per";
            this.txtVat5per.Size = new System.Drawing.Size(130, 23);
            this.txtVat5per.TabIndex = 1080;
            this.txtVat5per.Text = "0.00";
            this.txtVat5per.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumberOfBills
            // 
            this.txtNumberOfBills.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumberOfBills.CausesValidation = false;
            this.txtNumberOfBills.Location = new System.Drawing.Point(174, 75);
            this.txtNumberOfBills.Name = "txtNumberOfBills";
            this.txtNumberOfBills.Size = new System.Drawing.Size(130, 23);
            this.txtNumberOfBills.TabIndex = 1079;
            this.txtNumberOfBills.Text = "0.00";
            this.txtNumberOfBills.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmount.CausesValidation = false;
            this.txtReportTotalAmount.Location = new System.Drawing.Point(173, 43);
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.Size = new System.Drawing.Size(130, 23);
            this.txtReportTotalAmount.TabIndex = 1078;
            this.txtReportTotalAmount.Text = "0.00";
            this.txtReportTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // psLabel10
            // 
            this.psLabel10.AutoSize = true;
            this.psLabel10.Location = new System.Drawing.Point(74, 161);
            this.psLabel10.Name = "psLabel10";
            this.psLabel10.Size = new System.Drawing.Size(85, 19);
            this.psLabel10.TabIndex = 1075;
            this.psLabel10.Text = "Vat 12.5%";
            // 
            // psLabel9
            // 
            this.psLabel9.AutoSize = true;
            this.psLabel9.Location = new System.Drawing.Point(96, 131);
            this.psLabel9.Name = "psLabel9";
            this.psLabel9.Size = new System.Drawing.Size(63, 19);
            this.psLabel9.TabIndex = 1073;
            this.psLabel9.Text = "Vat 5%";
            // 
            // psLabel8
            // 
            this.psLabel8.AutoSize = true;
            this.psLabel8.Location = new System.Drawing.Point(76, 74);
            this.psLabel8.Name = "psLabel8";
            this.psLabel8.Size = new System.Drawing.Size(83, 19);
            this.psLabel8.TabIndex = 1071;
            this.psLabel8.Text = "No of Bills";
            // 
            // psLabel7
            // 
            this.psLabel7.AutoSize = true;
            this.psLabel7.Location = new System.Drawing.Point(48, 288);
            this.psLabel7.Name = "psLabel7";
            this.psLabel7.Size = new System.Drawing.Size(111, 19);
            this.psLabel7.TabIndex = 1067;
            this.psLabel7.Text = "Cash Amount ";
            // 
            // psLabel6
            // 
            this.psLabel6.AutoSize = true;
            this.psLabel6.Location = new System.Drawing.Point(67, 259);
            this.psLabel6.Name = "psLabel6";
            this.psLabel6.Size = new System.Drawing.Size(92, 19);
            this.psLabel6.TabIndex = 1066;
            this.psLabel6.Text = "CR Amount";
            // 
            // psLabel5
            // 
            this.psLabel5.AutoSize = true;
            this.psLabel5.Location = new System.Drawing.Point(25, 230);
            this.psLabel5.Name = "psLabel5";
            this.psLabel5.Size = new System.Drawing.Size(134, 19);
            this.psLabel5.TabIndex = 1065;
            this.psLabel5.Text = "Cr STMT Amount";
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(12, 42);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(147, 19);
            this.psLabel4.TabIndex = 1064;
            this.psLabel4.Text = "Statement Amount";
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.mcbCreditor);
            this.pnlMultiSelection1.Controls.Add(this.lblParty);
            this.pnlMultiSelection1.Controls.Add(this.lblToDate);
            this.pnlMultiSelection1.Controls.Add(this.lblFromDate);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(290, 126);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(456, 76);
            this.pnlMultiSelection1.TabIndex = 1067;
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(72, 39);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = false;
            this.mcbCreditor.Size = new System.Drawing.Size(299, 26);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 5;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            // 
            // lblParty
            // 
            this.lblParty.AutoSize = true;
            this.lblParty.Location = new System.Drawing.Point(16, 40);
            this.lblParty.Name = "lblParty";
            this.lblParty.Size = new System.Drawing.Size(49, 19);
            this.lblParty.TabIndex = 11;
            this.lblParty.Text = "Party";
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(200, 12);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(28, 19);
            this.lblToDate.TabIndex = 10;
            this.lblToDate.Text = "To";
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(15, 10);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(48, 19);
            this.lblFromDate.TabIndex = 9;
            this.lblFromDate.Text = "From";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(235, 9);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 25);
            this.toDate1.TabIndex = 8;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(72, 7);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 25);
            this.fromDate1.TabIndex = 7;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(388, 3);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 6;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(4, 4);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(2, 15);
            this.lblMessage.TabIndex = 3;
            // 
            // dgvReportList
            // 
            this.dgvReportList.AutoScroll = true;
            this.dgvReportList.DataSource = null;
            this.dgvReportList.DataSourceMain = null;
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.Filter = null;
            this.dgvReportList.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReportList.IsAllowDelete = true;
            this.dgvReportList.IsAllowNewRow = true;
            this.dgvReportList.Location = new System.Drawing.Point(8, 48);
            this.dgvReportList.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dgvReportList.MinimumSize = new System.Drawing.Size(500, 415);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NextRowColumn = 0;
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(550, 540);
            this.dgvReportList.SubGridWidth = 380;
            this.dgvReportList.TabIndex = 1068;
            this.dgvReportList.ViewControl = null;
            this.dgvReportList.OnShowViewForm += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl.ShowViewForm(this.dgvReportList_OnShowViewForm);
            // 
            // UclStatementPartywiseSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclStatementPartywiseSale";
            this.Size = new System.Drawing.Size(944, 664);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.pnlAmounts.ResumeLayout(false);
            this.pnlAmounts.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGo;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtStmtNumber;
        private System.Windows.Forms.Label label11;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight ViewToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight ViewFromDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtViewText;
        private System.Windows.Forms.Label lblViewTo;
        private System.Windows.Forms.Label lblViewFrom;
        private System.Windows.Forms.Label lblViewText;
        private System.Windows.Forms.Panel pnlAmounts;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtCashAmount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtCreditAmount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtCreditStatementAmount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtVat12point5per;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtVat5per;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtNumberOfBills;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtReportTotalAmount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel10;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel9;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel8;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel7;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel6;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel5;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblParty;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblFromDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate toDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate fromDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private System.Windows.Forms.ToolTip ttToolTip;
        private System.Windows.Forms.Label lblMessage;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl dgvReportList;
    }
}
