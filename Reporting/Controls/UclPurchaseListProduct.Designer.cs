namespace EcoMart.Reporting.Controls
{
    partial class UclPurchaseListProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclPurchaseListProduct));
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.cbScheduleH1 = new System.Windows.Forms.CheckBox();
            this.btnViewList = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.txtNoofSearches = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtSearch = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.psLabel6 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.mcbProduct = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.gbRate = new System.Windows.Forms.GroupBox();
            this.rbtPurchaseRate = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtTradeRate = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.dgvMultiSelection = new System.Windows.Forms.DataGridView();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel7 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.gbRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(983, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 519);
            this.MMBottomPanel.Size = new System.Drawing.Size(985, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Size = new System.Drawing.Size(985, 467);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlTop, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.cbScheduleH1);
            this.pnlMultiSelection1.Controls.Add(this.btnViewList);
            this.pnlMultiSelection1.Controls.Add(this.txtNoofSearches);
            this.pnlMultiSelection1.Controls.Add(this.psLabel2);
            this.pnlMultiSelection1.Controls.Add(this.txtSearch);
            this.pnlMultiSelection1.Controls.Add(this.psLabel6);
            this.pnlMultiSelection1.Controls.Add(this.psLabel5);
            this.pnlMultiSelection1.Controls.Add(this.psLabel4);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.mcbProduct);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.gbRate);
            this.pnlMultiSelection1.Controls.Add(this.dgvSelected);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.dgvMultiSelection);
            this.pnlMultiSelection1.Controls.Add(this.cbSelectAll);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(200, 100);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(665, 359);
            this.pnlMultiSelection1.TabIndex = 0;
            // 
            // cbScheduleH1
            // 
            this.cbScheduleH1.AutoSize = true;
            this.cbScheduleH1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbScheduleH1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbScheduleH1.Location = new System.Drawing.Point(438, 76);
            this.cbScheduleH1.Name = "cbScheduleH1";
            this.cbScheduleH1.Size = new System.Drawing.Size(113, 22);
            this.cbScheduleH1.TabIndex = 1077;
            this.cbScheduleH1.Text = "Schedule H1";
            this.cbScheduleH1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbScheduleH1.UseVisualStyleBackColor = true;
            this.cbScheduleH1.CheckedChanged += new System.EventHandler(this.cbScheduleH1_CheckedChanged);
            // 
            // btnViewList
            // 
            this.btnViewList.Location = new System.Drawing.Point(269, 326);
            this.btnViewList.Name = "btnViewList";
            this.btnViewList.Size = new System.Drawing.Size(83, 28);
            this.btnViewList.TabIndex = 24;
            this.btnViewList.Text = "View";
            this.btnViewList.UseVisualStyleBackColor = true;
            this.btnViewList.Click += new System.EventHandler(this.btnViewList_Click);
            this.btnViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnViewList_KeyDown);
            // 
            // txtNoofSearches
            // 
            this.txtNoofSearches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoofSearches.CausesValidation = false;
            this.txtNoofSearches.Location = new System.Drawing.Point(194, 328);
            this.txtNoofSearches.Name = "txtNoofSearches";
            this.txtNoofSearches.Size = new System.Drawing.Size(59, 23);
            this.txtNoofSearches.TabIndex = 23;
            this.txtNoofSearches.Text = "label";
            this.txtNoofSearches.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(22, 330);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(139, 17);
            this.psLabel2.TabIndex = 22;
            this.psLabel2.Text = "Number Of Selections";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(259, 76);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(135, 22);
            this.txtSearch.TabIndex = 21;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // psLabel6
            // 
            this.psLabel6.AutoSize = true;
            this.psLabel6.Location = new System.Drawing.Point(20, 42);
            this.psLabel6.Name = "psLabel6";
            this.psLabel6.Size = new System.Drawing.Size(56, 17);
            this.psLabel6.TabIndex = 18;
            this.psLabel6.Text = "Product";
            // 
            // psLabel5
            // 
            this.psLabel5.AutoSize = true;
            this.psLabel5.Location = new System.Drawing.Point(235, 12);
            this.psLabel5.Name = "psLabel5";
            this.psLabel5.Size = new System.Drawing.Size(22, 17);
            this.psLabel5.TabIndex = 16;
            this.psLabel5.Text = "To";
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(41, 12);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(41, 17);
            this.psLabel4.TabIndex = 14;
            this.psLabel4.Text = "From";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(269, 10);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 24);
            this.toDate1.TabIndex = 17;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(101, 42);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = null;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(331, 22);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 19;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            this.mcbProduct.EnterKeyPressed += new System.EventHandler(this.mcbProduct_EnterKeyPressed);
            this.mcbProduct.Load += new System.EventHandler(this.mcbProduct_Load);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(101, 10);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 15;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // gbRate
            // 
            this.gbRate.Controls.Add(this.rbtPurchaseRate);
            this.gbRate.Controls.Add(this.rbtTradeRate);
            this.gbRate.Location = new System.Drawing.Point(437, 4);
            this.gbRate.Name = "gbRate";
            this.gbRate.Size = new System.Drawing.Size(141, 62);
            this.gbRate.TabIndex = 13;
            this.gbRate.TabStop = false;
            // 
            // rbtPurchaseRate
            // 
            this.rbtPurchaseRate.AutoSize = true;
            this.rbtPurchaseRate.BackColor = System.Drawing.Color.White;
            this.rbtPurchaseRate.Location = new System.Drawing.Point(12, 35);
            this.rbtPurchaseRate.Name = "rbtPurchaseRate";
            this.rbtPurchaseRate.Size = new System.Drawing.Size(115, 22);
            this.rbtPurchaseRate.TabIndex = 3;
            this.rbtPurchaseRate.TabStop = true;
            this.rbtPurchaseRate.Text = "Purchase Rate";
            this.rbtPurchaseRate.UseVisualStyleBackColor = true;
            // 
            // rbtTradeRate
            // 
            this.rbtTradeRate.AutoSize = true;
            this.rbtTradeRate.BackColor = System.Drawing.Color.White;
            this.rbtTradeRate.Location = new System.Drawing.Point(12, 10);
            this.rbtTradeRate.Name = "rbtTradeRate";
            this.rbtTradeRate.Size = new System.Drawing.Size(93, 22);
            this.rbtTradeRate.TabIndex = 2;
            this.rbtTradeRate.TabStop = true;
            this.rbtTradeRate.Text = "Trade Rate";
            this.rbtTradeRate.UseVisualStyleBackColor = true;
            // 
            // dgvSelected
            // 
            this.dgvSelected.AllowUserToAddRows = false;
            this.dgvSelected.AllowUserToResizeColumns = false;
            this.dgvSelected.AllowUserToResizeRows = false;
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Location = new System.Drawing.Point(134, 132);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.Size = new System.Drawing.Size(449, 160);
            this.dgvSelected.TabIndex = 12;
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
            this.btnOKMultiSelection1.TabIndex = 8;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // dgvMultiSelection
            // 
            this.dgvMultiSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMultiSelection.Location = new System.Drawing.Point(26, 105);
            this.dgvMultiSelection.Name = "dgvMultiSelection";
            this.dgvMultiSelection.Size = new System.Drawing.Size(628, 216);
            this.dgvMultiSelection.TabIndex = 7;
            this.dgvMultiSelection.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellContentClick);
            this.dgvMultiSelection.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellValueChanged);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectAll.Location = new System.Drawing.Point(26, 80);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(91, 22);
            this.cbSelectAll.TabIndex = 6;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            this.cbSelectAll.Click += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(130, 80);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(123, 19);
            this.psLabel1.TabIndex = 20;
            this.psLabel1.Text = "Search &Product";
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.ViewToDate);
            this.pnlTop.Controls.Add(this.ViewFromDate);
            this.pnlTop.Controls.Add(this.psLabel3);
            this.pnlTop.Controls.Add(this.psLabel7);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(983, 33);
            this.pnlTop.TabIndex = 1051;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(820, 4);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(80, 18);
            this.ViewToDate.TabIndex = 1057;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(671, 4);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(80, 18);
            this.ViewFromDate.TabIndex = 1056;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(786, 4);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(22, 17);
            this.psLabel3.TabIndex = 1055;
            this.psLabel3.Text = "To";
            // 
            // psLabel7
            // 
            this.psLabel7.AutoSize = true;
            this.psLabel7.Location = new System.Drawing.Point(608, 4);
            this.psLabel7.Name = "psLabel7";
            this.psLabel7.Size = new System.Drawing.Size(41, 17);
            this.psLabel7.TabIndex = 1054;
            this.psLabel7.Text = "From";
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
            this.dgvReportList.Size = new System.Drawing.Size(983, 432);
            this.dgvReportList.TabIndex = 1053;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // UclPurchaseListProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclPurchaseListProduct";
            this.Size = new System.Drawing.Size(985, 542);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.gbRate.ResumeLayout(false);
            this.gbRate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

       
        private System.Windows.Forms.ToolTip ttToolTip;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private System.Windows.Forms.DataGridView dgvSelected;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private System.Windows.Forms.DataGridView dgvMultiSelection;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.GroupBox gbRate;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel6;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel5;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel4;
        private EcoMart.InterfaceLayer.CommonControls.ToDate toDate1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
        private EcoMart.InterfaceLayer.CommonControls.FromDate fromDate1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private EcoMart.InterfaceLayer.CommonControls.PSTextBox txtSearch;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtPurchaseRate;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtTradeRate;
        private PharmaSYSPlus.CommonLibrary.PSButton btnViewList;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtNoofSearches;
        private System.Windows.Forms.CheckBox cbScheduleH1;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private InterfaceLayer.CommonControls.PSLabel psLabel3;
        private InterfaceLayer.CommonControls.PSLabel psLabel7;
    }
}
