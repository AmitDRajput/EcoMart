namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclSaleListProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclSaleListProduct));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ViewToDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.cbScheduleH1 = new System.Windows.Forms.CheckBox();
            this.psLabel6 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel7 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mcbProduct = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtNoofSearches = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.btnViewList = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton();
            this.psLabel5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtSearch = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.psLabel4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate(this.components);
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.dgvMultiSelection = new System.Windows.Forms.DataGridView();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(973, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 515);
            this.MMBottomPanel.Size = new System.Drawing.Size(975, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Size = new System.Drawing.Size(975, 463);
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.ViewToDate);
            this.pnlTop.Controls.Add(this.ViewFromDate);
            this.pnlTop.Controls.Add(this.psLabel2);
            this.pnlTop.Controls.Add(this.psLabel1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(973, 33);
            this.pnlTop.TabIndex = 1042;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(856, 5);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1053;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(707, 5);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1052;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(822, 5);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(28, 19);
            this.psLabel2.TabIndex = 1051;
            this.psLabel2.Text = "To";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(644, 5);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(48, 19);
            this.psLabel1.TabIndex = 1050;
            this.psLabel1.Text = "From";
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
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
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(973, 428);
            this.dgvReportList.TabIndex = 1045;
            this.ttToolTip.SetToolTip(this.dgvReportList, "Press Home Key ");
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.cbScheduleH1);
            this.pnlMultiSelection1.Controls.Add(this.psLabel6);
            this.pnlMultiSelection1.Controls.Add(this.psLabel7);
            this.pnlMultiSelection1.Controls.Add(this.mcbProduct);
            this.pnlMultiSelection1.Controls.Add(this.txtNoofSearches);
            this.pnlMultiSelection1.Controls.Add(this.btnViewList);
            this.pnlMultiSelection1.Controls.Add(this.psLabel5);
            this.pnlMultiSelection1.Controls.Add(this.txtSearch);
            this.pnlMultiSelection1.Controls.Add(this.psLabel4);
            this.pnlMultiSelection1.Controls.Add(this.psLabel3);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.dgvSelected);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.dgvMultiSelection);
            this.pnlMultiSelection1.Controls.Add(this.cbSelectAll);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(237, 73);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(665, 373);
            this.pnlMultiSelection1.TabIndex = 1046;
            // 
            // cbScheduleH1
            // 
            this.cbScheduleH1.AutoSize = true;
            this.cbScheduleH1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbScheduleH1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbScheduleH1.Location = new System.Drawing.Point(463, 12);
            this.cbScheduleH1.Name = "cbScheduleH1";
            this.cbScheduleH1.Size = new System.Drawing.Size(113, 21);
            this.cbScheduleH1.TabIndex = 1076;
            this.cbScheduleH1.Text = "Schedule H1";
            this.cbScheduleH1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbScheduleH1.UseVisualStyleBackColor = true;
            this.cbScheduleH1.CheckedChanged += new System.EventHandler(this.cbScheduleH1_CheckedChanged);
            // 
            // psLabel6
            // 
            this.psLabel6.AutoSize = true;
            this.psLabel6.Location = new System.Drawing.Point(90, 341);
            this.psLabel6.Name = "psLabel6";
            this.psLabel6.Size = new System.Drawing.Size(83, 19);
            this.psLabel6.TabIndex = 1075;
            this.psLabel6.Text = "Selections";
            // 
            // psLabel7
            // 
            this.psLabel7.AutoSize = true;
            this.psLabel7.Location = new System.Drawing.Point(71, 39);
            this.psLabel7.Name = "psLabel7";
            this.psLabel7.Size = new System.Drawing.Size(69, 19);
            this.psLabel7.TabIndex = 1073;
            this.psLabel7.Text = "Product";
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(148, 38);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = null;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(347, 26);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 1074;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            this.mcbProduct.EnterKeyPressed += new System.EventHandler(this.mcbProduct_EnterKeyPressed);
            // 
            // txtNoofSearches
            // 
            this.txtNoofSearches.BackColor = System.Drawing.Color.Snow;
            this.txtNoofSearches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoofSearches.Location = new System.Drawing.Point(179, 339);
            this.txtNoofSearches.MaxLength = 50;
            this.txtNoofSearches.Name = "txtNoofSearches";
            this.txtNoofSearches.Size = new System.Drawing.Size(57, 24);
            this.txtNoofSearches.TabIndex = 1072;
            this.txtNoofSearches.TabStop = false;
            this.txtNoofSearches.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            // 
            // btnViewList
            // 
            this.btnViewList.Location = new System.Drawing.Point(247, 334);
            this.btnViewList.Name = "btnViewList";
            this.btnViewList.Size = new System.Drawing.Size(76, 35);
            this.btnViewList.TabIndex = 1071;
            this.btnViewList.Text = "&View";
            this.btnViewList.UseVisualStyleBackColor = true;
            this.btnViewList.Click += new System.EventHandler(this.btnViewList_Click);
            this.btnViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnViewList_KeyDown);
            // 
            // psLabel5
            // 
            this.psLabel5.AutoSize = true;
            this.psLabel5.Location = new System.Drawing.Point(17, 69);
            this.psLabel5.Name = "psLabel5";
            this.psLabel5.Size = new System.Drawing.Size(123, 19);
            this.psLabel5.TabIndex = 1069;
            this.psLabel5.Text = "Search &Product";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.Snow;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(148, 67);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(114, 24);
            this.txtSearch.TabIndex = 1068;
            this.txtSearch.TabStop = false;
            this.txtSearch.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(295, 12);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(28, 19);
            this.psLabel4.TabIndex = 1064;
            this.psLabel4.Text = "To";
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(92, 12);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(48, 19);
            this.psLabel3.TabIndex = 1063;
            this.psLabel3.Text = "From";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(332, 9);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 25);
            this.toDate1.TabIndex = 1062;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ToDate_KeyDown);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(148, 9);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 25);
            this.fromDate1.TabIndex = 1061;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FromDate_KeyDown);
            // 
            // dgvSelected
            // 
            this.dgvSelected.AllowUserToAddRows = false;
            this.dgvSelected.AllowUserToResizeColumns = false;
            this.dgvSelected.AllowUserToResizeRows = false;
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Location = new System.Drawing.Point(134, 154);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.Size = new System.Drawing.Size(449, 160);
            this.dgvSelected.TabIndex = 1060;
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
            // dgvMultiSelection
            // 
            this.dgvMultiSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMultiSelection.Location = new System.Drawing.Point(26, 127);
            this.dgvMultiSelection.Name = "dgvMultiSelection";
            this.dgvMultiSelection.Size = new System.Drawing.Size(628, 203);
            this.dgvMultiSelection.TabIndex = 1059;
            this.dgvMultiSelection.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellValueChanged);
            this.dgvMultiSelection.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellContentClick);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectAll.Location = new System.Drawing.Point(71, 98);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(91, 21);
            this.cbSelectAll.TabIndex = 36;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // UclSaleListProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclSaleListProduct";
            this.Size = new System.Drawing.Size(975, 538);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.ToolTip ttToolTip;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate toDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate fromDate1;
        private System.Windows.Forms.DataGridView dgvSelected;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private System.Windows.Forms.DataGridView dgvMultiSelection;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel5;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtSearch;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNoofSearches;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton btnViewList;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel7;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel6;
        private System.Windows.Forms.CheckBox cbScheduleH1;        
    }
}
