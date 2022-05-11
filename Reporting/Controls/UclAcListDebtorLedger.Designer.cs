namespace EcoMart.Reporting.Controls
{
    partial class UclAcListDebtorLedger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclAcListDebtorLedger));
            this.lblAccount = new System.Windows.Forms.Label();
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cbWithZeroTransactions = new System.Windows.Forms.CheckBox();
            this.txtNoofSearches = new System.Windows.Forms.TextBox();
            this.lblNoofRowsSelected = new System.Windows.Forms.Label();
            this.dgvMultiSelection = new System.Windows.Forms.DataGridView();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlGo = new System.Windows.Forms.Panel();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewtext = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnViewList = new System.Windows.Forms.Button();
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbvoutype = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.psLabel5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbCreditor = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.pnlForMultiSelection = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblViewFrom = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).BeginInit();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.pnlForMultiSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(974, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 534);
            this.MMBottomPanel.Size = new System.Drawing.Size(976, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(976, 482);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlGo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.BackColor = System.Drawing.Color.Transparent;
            this.lblAccount.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.Location = new System.Drawing.Point(142, 10);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(114, 17);
            this.lblAccount.TabIndex = 1044;
            this.lblAccount.Text = "Search &Account";
            // 
            // dgvSelected
            // 
            this.dgvSelected.AllowUserToAddRows = false;
            this.dgvSelected.AllowUserToDeleteRows = false;
            this.dgvSelected.BackgroundColor = System.Drawing.Color.BlanchedAlmond;
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Location = new System.Drawing.Point(56, 56);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.Size = new System.Drawing.Size(449, 131);
            this.dgvSelected.TabIndex = 1055;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearch.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(271, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(138, 26);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // cbWithZeroTransactions
            // 
            this.cbWithZeroTransactions.AutoSize = true;
            this.cbWithZeroTransactions.BackColor = System.Drawing.Color.Transparent;
            this.cbWithZeroTransactions.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWithZeroTransactions.Location = new System.Drawing.Point(348, 210);
            this.cbWithZeroTransactions.Name = "cbWithZeroTransactions";
            this.cbWithZeroTransactions.Size = new System.Drawing.Size(160, 19);
            this.cbWithZeroTransactions.TabIndex = 5;
            this.cbWithZeroTransactions.Text = "WithZeroTransactions";
            this.cbWithZeroTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbWithZeroTransactions.UseVisualStyleBackColor = false;
            // 
            // txtNoofSearches
            // 
            this.txtNoofSearches.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoofSearches.Location = new System.Drawing.Point(196, 209);
            this.txtNoofSearches.Name = "txtNoofSearches";
            this.txtNoofSearches.Size = new System.Drawing.Size(57, 22);
            this.txtNoofSearches.TabIndex = 1052;
            // 
            // lblNoofRowsSelected
            // 
            this.lblNoofRowsSelected.AutoSize = true;
            this.lblNoofRowsSelected.BackColor = System.Drawing.Color.Transparent;
            this.lblNoofRowsSelected.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRowsSelected.Location = new System.Drawing.Point(49, 209);
            this.lblNoofRowsSelected.Name = "lblNoofRowsSelected";
            this.lblNoofRowsSelected.Size = new System.Drawing.Size(146, 17);
            this.lblNoofRowsSelected.TabIndex = 1051;
            this.lblNoofRowsSelected.Text = "No of Rows Selected";
            // 
            // dgvMultiSelection
            // 
            this.dgvMultiSelection.AllowUserToAddRows = false;
            this.dgvMultiSelection.AllowUserToDeleteRows = false;
            this.dgvMultiSelection.BackgroundColor = System.Drawing.Color.Moccasin;
            this.dgvMultiSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMultiSelection.Location = new System.Drawing.Point(15, 40);
            this.dgvMultiSelection.Name = "dgvMultiSelection";
            this.dgvMultiSelection.Size = new System.Drawing.Size(513, 163);
            this.dgvMultiSelection.TabIndex = 3;
            this.dgvMultiSelection.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellContentClick);
            this.dgvMultiSelection.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellValueChanged);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.cbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectAll.Location = new System.Drawing.Point(15, 8);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(91, 21);
            this.cbSelectAll.TabIndex = 36;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.UseVisualStyleBackColor = false;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            this.cbSelectAll.Click += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = false;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.Khaki;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(974, 447);
            this.dgvReportList.TabIndex = 1043;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // pnlGo
            // 
            this.pnlGo.BackColor = System.Drawing.Color.Plum;
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.lblViewFrom);
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.psLabel2);
            this.pnlGo.Controls.Add(this.txtViewtext);
            this.pnlGo.Controls.Add(this.label2);
            this.pnlGo.Controls.Add(this.txtType);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(974, 33);
            this.pnlGo.TabIndex = 1042;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(834, 3);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1078;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(685, 3);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1077;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(800, 3);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(21, 14);
            this.psLabel2.TabIndex = 1076;
            this.psLabel2.Text = "To";
            // 
            // txtViewtext
            // 
            this.txtViewtext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewtext.Location = new System.Drawing.Point(83, 3);
            this.txtViewtext.Name = "txtViewtext";
            this.txtViewtext.Size = new System.Drawing.Size(157, 22);
            this.txtViewtext.TabIndex = 1064;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 1063;
            this.label2.Text = "Type";
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.Location = new System.Drawing.Point(297, 3);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(57, 22);
            this.txtType.TabIndex = 1062;
            this.txtType.Visible = false;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(622, 3);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(48, 19);
            this.psLabel1.TabIndex = 1075;
            this.psLabel1.Text = "From";
            // 
            // btnViewList
            // 
            this.btnViewList.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewList.Location = new System.Drawing.Point(259, 206);
            this.btnViewList.Name = "btnViewList";
            this.btnViewList.Size = new System.Drawing.Size(69, 27);
            this.btnViewList.TabIndex = 4;
            this.btnViewList.Text = "&View";
            this.btnViewList.UseVisualStyleBackColor = true;
            this.btnViewList.Click += new System.EventHandler(this.btnViewList_Click);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(90, 18);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 0;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(51, 52);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(21, 14);
            this.mPlbl2.TabIndex = 1071;
            this.mPlbl2.Text = "To";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(90, 48);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 24);
            this.toDate1.TabIndex = 1;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(31, 23);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(38, 14);
            this.mPlbl1.TabIndex = 1069;
            this.mPlbl1.Text = "From";
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(485, 7);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 7;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(264, 26);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(34, 14);
            this.mPlbl4.TabIndex = 1046;
            this.mPlbl4.Text = "Type";
            // 
            // mcbvoutype
            // 
            this.mcbvoutype.BackColor = System.Drawing.Color.Transparent;
            this.mcbvoutype.ColumnWidth = null;
            this.mcbvoutype.DataSource = null;
            this.mcbvoutype.DisplayColumnNo = 1;
            this.mcbvoutype.DropDownHeight = 200;
            this.mcbvoutype.Location = new System.Drawing.Point(323, 23);
            this.mcbvoutype.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbvoutype.Name = "mcbvoutype";
            this.mcbvoutype.SelectedID = null;
            this.mcbvoutype.ShowNew = false;
            this.mcbvoutype.Size = new System.Drawing.Size(127, 22);
            this.mcbvoutype.SourceDataString = null;
            this.mcbvoutype.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbvoutype.TabIndex = 6;
            this.mcbvoutype.UserControlToShow = null;
            this.mcbvoutype.ValueColumnNo = 0;
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.psLabel5);
            this.pnlMultiSelection1.Controls.Add(this.mcbCreditor);
            this.pnlMultiSelection1.Controls.Add(this.pnlForMultiSelection);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.mcbvoutype);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl4);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(273, 69);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(557, 372);
            this.pnlMultiSelection1.TabIndex = 1048;
            // 
            // psLabel5
            // 
            this.psLabel5.AutoSize = true;
            this.psLabel5.Location = new System.Drawing.Point(9, 83);
            this.psLabel5.Name = "psLabel5";
            this.psLabel5.Size = new System.Drawing.Size(52, 14);
            this.psLabel5.TabIndex = 1078;
            this.psLabel5.Text = "Account";
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(91, 80);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = false;
            this.mcbCreditor.Size = new System.Drawing.Size(360, 22);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 1077;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            // 
            // pnlForMultiSelection
            // 
            this.pnlForMultiSelection.BackColor = System.Drawing.Color.Tan;
            this.pnlForMultiSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForMultiSelection.Controls.Add(this.btnClear);
            this.pnlForMultiSelection.Controls.Add(this.dgvSelected);
            this.pnlForMultiSelection.Controls.Add(this.dgvMultiSelection);
            this.pnlForMultiSelection.Controls.Add(this.btnViewList);
            this.pnlForMultiSelection.Controls.Add(this.cbWithZeroTransactions);
            this.pnlForMultiSelection.Controls.Add(this.txtNoofSearches);
            this.pnlForMultiSelection.Controls.Add(this.lblNoofRowsSelected);
            this.pnlForMultiSelection.Controls.Add(this.txtSearch);
            this.pnlForMultiSelection.Controls.Add(this.lblAccount);
            this.pnlForMultiSelection.Controls.Add(this.cbSelectAll);
            this.pnlForMultiSelection.Location = new System.Drawing.Point(5, 127);
            this.pnlForMultiSelection.Name = "pnlForMultiSelection";
            this.pnlForMultiSelection.Size = new System.Drawing.Size(545, 237);
            this.pnlForMultiSelection.TabIndex = 1072;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(459, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(69, 27);
            this.btnClear.TabIndex = 1056;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblViewFrom
            // 
            this.lblViewFrom.AutoSize = true;
            this.lblViewFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewFrom.Location = new System.Drawing.Point(640, 6);
            this.lblViewFrom.Name = "lblViewFrom";
            this.lblViewFrom.Size = new System.Drawing.Size(39, 15);
            this.lblViewFrom.TabIndex = 1079;
            this.lblViewFrom.Text = "From";
            // 
            // UclAcListDebtorLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclAcListDebtorLedger";
            this.Size = new System.Drawing.Size(976, 557);
            this.Load += new System.EventHandler(this.UclAcListDebtorLedger_Load);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).EndInit();
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.pnlForMultiSelection.ResumeLayout(false);
            this.pnlForMultiSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAccount;       
        private System.Windows.Forms.DataGridView dgvMultiSelection;
        private System.Windows.Forms.TextBox txtNoofSearches;
        private System.Windows.Forms.Label lblNoofRowsSelected;
        private System.Windows.Forms.CheckBox cbWithZeroTransactions;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.DataGridView dgvSelected;
        private System.Windows.Forms.TextBox txtSearch;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.Panel pnlGo;
        private EcoMart.InterfaceLayer.CommonControls.PSTextBox txtViewtext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtType;
        private EcoMart.InterfaceLayer.CommonControls.FromDate fromDate1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.ToDate toDate1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
       private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbvoutype;
        private System.Windows.Forms.ToolTip ttToolTip;
        private System.Windows.Forms.Button btnViewList;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private System.Windows.Forms.Panel pnlForMultiSelection;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel5;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblViewFrom;      

    }
}
