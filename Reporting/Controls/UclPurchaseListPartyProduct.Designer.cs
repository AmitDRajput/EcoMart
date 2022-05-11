namespace EcoMart.Reporting.Controls
{
    partial class UclPurchaseListPartyProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclPurchaseListPartyProduct));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.ViewToDate = new System.Windows.Forms.DateTimePicker();
            this.ViewFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblViewTo = new System.Windows.Forms.Label();
            this.lblViewFrom = new System.Windows.Forms.Label();
            this.lblViewParty = new System.Windows.Forms.Label();
            this.txtViewParty = new System.Windows.Forms.TextBox();
            this.lblParty = new System.Windows.Forms.Label();
            this.mcbCreditor = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.ttPurchasePartyProduct = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.btnViewList = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblAccount = new System.Windows.Forms.Label();
            this.txtNoofSearches = new System.Windows.Forms.TextBox();
            this.lblNoofRowsSelected = new System.Windows.Forms.Label();
            this.dgvMultiSelection = new System.Windows.Forms.DataGridView();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(998, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 525);
            this.MMBottomPanel.Size = new System.Drawing.Size(1000, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(1000, 473);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlGo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // pnlGo
            // 
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.lblViewTo);
            this.pnlGo.Controls.Add(this.lblViewFrom);
            this.pnlGo.Controls.Add(this.lblViewParty);
            this.pnlGo.Controls.Add(this.txtViewParty);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(998, 33);
            this.pnlGo.TabIndex = 1032;
            // 
            // ViewToDate
            // 
            this.ViewToDate.CustomFormat = "dd/MM/yy";
            this.ViewToDate.Enabled = false;
            this.ViewToDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ViewToDate.Location = new System.Drawing.Point(825, 2);
            this.ViewToDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ViewToDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(122, 26);
            this.ViewToDate.TabIndex = 1060;
            this.ViewToDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.CalendarFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewFromDate.CustomFormat = "dd/MM/yy";
            this.ViewFromDate.Enabled = false;
            this.ViewFromDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ViewFromDate.Location = new System.Drawing.Point(661, 2);
            this.ViewFromDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ViewFromDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(122, 26);
            this.ViewFromDate.TabIndex = 1059;
            this.ViewFromDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // lblViewTo
            // 
            this.lblViewTo.AutoSize = true;
            this.lblViewTo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewTo.Location = new System.Drawing.Point(799, 7);
            this.lblViewTo.Name = "lblViewTo";
            this.lblViewTo.Size = new System.Drawing.Size(22, 16);
            this.lblViewTo.TabIndex = 1058;
            this.lblViewTo.Text = "To";
            // 
            // lblViewFrom
            // 
            this.lblViewFrom.AutoSize = true;
            this.lblViewFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewFrom.Location = new System.Drawing.Point(618, 7);
            this.lblViewFrom.Name = "lblViewFrom";
            this.lblViewFrom.Size = new System.Drawing.Size(40, 16);
            this.lblViewFrom.TabIndex = 1057;
            this.lblViewFrom.Text = "From";
            // 
            // lblViewParty
            // 
            this.lblViewParty.AutoSize = true;
            this.lblViewParty.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewParty.Location = new System.Drawing.Point(37, 5);
            this.lblViewParty.Name = "lblViewParty";
            this.lblViewParty.Size = new System.Drawing.Size(41, 16);
            this.lblViewParty.TabIndex = 1056;
            this.lblViewParty.Text = "Party";
            // 
            // txtViewParty
            // 
            this.txtViewParty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtViewParty.Enabled = false;
            this.txtViewParty.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewParty.Location = new System.Drawing.Point(83, 3);
            this.txtViewParty.Name = "txtViewParty";
            this.txtViewParty.Size = new System.Drawing.Size(347, 22);
            this.txtViewParty.TabIndex = 1055;
            // 
            // lblParty
            // 
            this.lblParty.AutoSize = true;
            this.lblParty.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParty.Location = new System.Drawing.Point(27, 42);
            this.lblParty.Name = "lblParty";
            this.lblParty.Size = new System.Drawing.Size(41, 16);
            this.lblParty.TabIndex = 1054;
            this.lblParty.Text = "Party";
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(67, 37);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = false;
            this.mcbCreditor.Size = new System.Drawing.Size(359, 22);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 1053;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            // 
            // ttPurchasePartyProduct
            // 
            this.ttPurchasePartyProduct.ShowAlways = true;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.dgvSelected);
            this.pnlMultiSelection1.Controls.Add(this.lblParty);
            this.pnlMultiSelection1.Controls.Add(this.mcbCreditor);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.lblFrom);
            this.pnlMultiSelection1.Controls.Add(this.lblTo);
            this.pnlMultiSelection1.Controls.Add(this.cbSelectAll);
            this.pnlMultiSelection1.Controls.Add(this.btnViewList);
            this.pnlMultiSelection1.Controls.Add(this.txtSearch);
            this.pnlMultiSelection1.Controls.Add(this.lblAccount);
            this.pnlMultiSelection1.Controls.Add(this.txtNoofSearches);
            this.pnlMultiSelection1.Controls.Add(this.lblNoofRowsSelected);
            this.pnlMultiSelection1.Controls.Add(this.dgvMultiSelection);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(167, 56);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(665, 395);
            this.pnlMultiSelection1.TabIndex = 1046;
            // 
            // dgvSelected
            // 
            this.dgvSelected.AllowUserToAddRows = false;
            this.dgvSelected.AllowUserToResizeColumns = false;
            this.dgvSelected.AllowUserToResizeRows = false;
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Location = new System.Drawing.Point(120, 166);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.Size = new System.Drawing.Size(449, 160);
            this.dgvSelected.TabIndex = 1060;
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(218, 7);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 24);
            this.toDate1.TabIndex = 1062;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ToDate_KeyDown);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(72, 7);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 1061;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FromDate_KeyDown);
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(591, 3);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 0;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(28, 11);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(40, 16);
            this.lblFrom.TabIndex = 1040;
            this.lblFrom.Text = "From";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(190, 11);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(22, 16);
            this.lblTo.TabIndex = 1041;
            this.lblTo.Text = "To";
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectAll.Location = new System.Drawing.Point(45, 87);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(82, 20);
            this.cbSelectAll.TabIndex = 36;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // btnViewList
            // 
            this.btnViewList.Location = new System.Drawing.Point(252, 362);
            this.btnViewList.Name = "btnViewList";
            this.btnViewList.Size = new System.Drawing.Size(69, 23);
            this.btnViewList.TabIndex = 1056;
            this.btnViewList.Text = "View";
            this.btnViewList.UseVisualStyleBackColor = true;
            this.btnViewList.Click += new System.EventHandler(this.btnViewList_Click);
            this.btnViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnViewList_KeyDown);
            // 
            // txtSearch
            // 
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearch.Location = new System.Drawing.Point(252, 83);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(145, 20);
            this.txtSearch.TabIndex = 1054;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.Location = new System.Drawing.Point(150, 86);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(100, 16);
            this.lblAccount.TabIndex = 1044;
            this.lblAccount.Text = "Search &Product";
            // 
            // txtNoofSearches
            // 
            this.txtNoofSearches.Location = new System.Drawing.Point(189, 364);
            this.txtNoofSearches.Name = "txtNoofSearches";
            this.txtNoofSearches.Size = new System.Drawing.Size(57, 20);
            this.txtNoofSearches.TabIndex = 1052;
            // 
            // lblNoofRowsSelected
            // 
            this.lblNoofRowsSelected.AutoSize = true;
            this.lblNoofRowsSelected.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRowsSelected.Location = new System.Drawing.Point(35, 366);
            this.lblNoofRowsSelected.Name = "lblNoofRowsSelected";
            this.lblNoofRowsSelected.Size = new System.Drawing.Size(149, 16);
            this.lblNoofRowsSelected.TabIndex = 1051;
            this.lblNoofRowsSelected.Text = "No of Products Selected";
            // 
            // dgvMultiSelection
            // 
            this.dgvMultiSelection.AllowUserToAddRows = false;
            this.dgvMultiSelection.AllowUserToResizeColumns = false;
            this.dgvMultiSelection.AllowUserToResizeRows = false;
            this.dgvMultiSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMultiSelection.Location = new System.Drawing.Point(30, 133);
            this.dgvMultiSelection.Name = "dgvMultiSelection";
            this.dgvMultiSelection.Size = new System.Drawing.Size(604, 223);
            this.dgvMultiSelection.TabIndex = 1063;
            this.dgvMultiSelection.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellContentClick);
            this.dgvMultiSelection.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellValueChanged);
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
            this.dgvReportList.FreezeLastRow = false;
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(998, 438);
            this.dgvReportList.TabIndex = 1054;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // UclPurchaseListPartyProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclPurchaseListPartyProduct";
            this.Size = new System.Drawing.Size(1000, 548);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

       
        private System.Windows.Forms.Panel pnlGo;
        private System.Windows.Forms.DateTimePicker ViewToDate;
        private System.Windows.Forms.DateTimePicker ViewFromDate;
        private System.Windows.Forms.Label lblViewTo;
        private System.Windows.Forms.Label lblViewFrom;
        private System.Windows.Forms.Label lblViewParty;
        private System.Windows.Forms.TextBox txtViewParty;
        private System.Windows.Forms.Label lblParty;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private System.Windows.Forms.ToolTip ttPurchasePartyProduct;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.ToDate toDate1;
        private EcoMart.InterfaceLayer.CommonControls.FromDate fromDate1;
        private System.Windows.Forms.DataGridView dgvSelected;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.Button btnViewList;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.TextBox txtNoofSearches;
        private System.Windows.Forms.Label lblNoofRowsSelected;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.DataGridView dgvMultiSelection;        
    }
}
