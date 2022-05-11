namespace EcoMart.Reporting.Controls
{
    partial class UclStockListCurrentStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStockListCurrentStock));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtOpeningSTock = new System.Windows.Forms.RadioButton();
            this.rbtClosingStock = new System.Windows.Forms.RadioButton();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.cbZero = new System.Windows.Forms.CheckBox();
            this.mcbCompany = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtNoofSearches = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.btnViewList = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.psLabel6 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtSearch = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.dgvMultiSelection = new System.Windows.Forms.DataGridView();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(989, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 525);
            this.MMBottomPanel.Size = new System.Drawing.Size(991, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Size = new System.Drawing.Size(991, 473);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtOpeningSTock);
            this.groupBox4.Controls.Add(this.rbtClosingStock);
            this.groupBox4.Location = new System.Drawing.Point(253, 40);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(138, 59);
            this.groupBox4.TabIndex = 38;
            this.groupBox4.TabStop = false;
            // 
            // rbtOpeningSTock
            // 
            this.rbtOpeningSTock.AutoSize = true;
            this.rbtOpeningSTock.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtOpeningSTock.Location = new System.Drawing.Point(11, 35);
            this.rbtOpeningSTock.Name = "rbtOpeningSTock";
            this.rbtOpeningSTock.Size = new System.Drawing.Size(125, 21);
            this.rbtOpeningSTock.TabIndex = 1;
            this.rbtOpeningSTock.TabStop = true;
            this.rbtOpeningSTock.Text = "Opening Stock";
            this.rbtOpeningSTock.UseVisualStyleBackColor = true;
            // 
            // rbtClosingStock
            // 
            this.rbtClosingStock.AutoSize = true;
            this.rbtClosingStock.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtClosingStock.Location = new System.Drawing.Point(11, 10);
            this.rbtClosingStock.Name = "rbtClosingStock";
            this.rbtClosingStock.Size = new System.Drawing.Size(119, 21);
            this.rbtClosingStock.TabIndex = 0;
            this.rbtClosingStock.TabStop = true;
            this.rbtClosingStock.Text = "Closing Stock";
            this.rbtClosingStock.UseVisualStyleBackColor = true;
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
            this.dgvReportList.FreezeLastRow = false;
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(989, 471);
            this.dgvReportList.TabIndex = 1048;
            this.ttToolTip.SetToolTip(this.dgvReportList, "Press Home Key ");
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.psLabel1);
            this.pnlMultiSelection1.Controls.Add(this.cbZero);
            this.pnlMultiSelection1.Controls.Add(this.mcbCompany);
            this.pnlMultiSelection1.Controls.Add(this.groupBox4);
            this.pnlMultiSelection1.Controls.Add(this.txtNoofSearches);
            this.pnlMultiSelection1.Controls.Add(this.btnViewList);
            this.pnlMultiSelection1.Controls.Add(this.psLabel6);
            this.pnlMultiSelection1.Controls.Add(this.psLabel5);
            this.pnlMultiSelection1.Controls.Add(this.txtSearch);
            this.pnlMultiSelection1.Controls.Add(this.dgvSelected);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.dgvMultiSelection);
            this.pnlMultiSelection1.Controls.Add(this.cbSelectAll);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(283, 52);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(642, 373);
            this.pnlMultiSelection1.TabIndex = 1047;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(17, 18);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(65, 16);
            this.psLabel1.TabIndex = 1080;
            this.psLabel1.Text = "Company";
            // 
            // cbZero
            // 
            this.cbZero.AutoSize = true;
            this.cbZero.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbZero.Location = new System.Drawing.Point(437, 78);
            this.cbZero.Name = "cbZero";
            this.cbZero.Size = new System.Drawing.Size(59, 19);
            this.cbZero.TabIndex = 52;
            this.cbZero.Text = "ZERO";
            this.cbZero.UseVisualStyleBackColor = true;
            // 
            // mcbCompany
            // 
            this.mcbCompany.ColumnWidth = null;
            this.mcbCompany.DataSource = null;
            this.mcbCompany.DisplayColumnNo = 1;
            this.mcbCompany.DropDownHeight = 200;
            this.mcbCompany.Location = new System.Drawing.Point(103, 12);
            this.mcbCompany.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbCompany.Name = "mcbCompany";
            this.mcbCompany.SelectedID = "";
            this.mcbCompany.ShowNew = false;
            this.mcbCompany.Size = new System.Drawing.Size(326, 22);
            this.mcbCompany.SourceDataString = null;
            this.mcbCompany.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCompany.TabIndex = 1079;
            this.mcbCompany.UserControlToShow = null;
            this.mcbCompany.ValueColumnNo = 0;
            this.mcbCompany.EnterKeyPressed += new System.EventHandler(this.mcbCompany_EnterKeyPressed);
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
            this.btnViewList.Location = new System.Drawing.Point(257, 334);
            this.btnViewList.Name = "btnViewList";
            this.btnViewList.Size = new System.Drawing.Size(76, 35);
            this.btnViewList.TabIndex = 1071;
            this.btnViewList.Text = "&View";
            this.btnViewList.UseVisualStyleBackColor = true;
            this.btnViewList.Click += new System.EventHandler(this.btnViewList_Click);
            this.btnViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnViewList_KeyDown);
            // 
            // psLabel6
            // 
            this.psLabel6.AutoSize = true;
            this.psLabel6.Location = new System.Drawing.Point(90, 341);
            this.psLabel6.Name = "psLabel6";
            this.psLabel6.Size = new System.Drawing.Size(69, 16);
            this.psLabel6.TabIndex = 1070;
            this.psLabel6.Text = "Selections";
            // 
            // psLabel5
            // 
            this.psLabel5.AutoSize = true;
            this.psLabel5.Location = new System.Drawing.Point(36, 50);
            this.psLabel5.Name = "psLabel5";
            this.psLabel5.Size = new System.Drawing.Size(49, 16);
            this.psLabel5.TabIndex = 1069;
            this.psLabel5.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.Snow;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(102, 48);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(114, 24);
            this.txtSearch.TabIndex = 1068;
            this.txtSearch.TabStop = false;
            this.txtSearch.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // dgvSelected
            // 
            this.dgvSelected.AllowUserToAddRows = false;
            this.dgvSelected.AllowUserToResizeColumns = false;
            this.dgvSelected.AllowUserToResizeRows = false;
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Location = new System.Drawing.Point(53, 123);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.Size = new System.Drawing.Size(531, 188);
            this.dgvSelected.TabIndex = 1060;
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(574, 3);
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
            this.dgvMultiSelection.Location = new System.Drawing.Point(28, 103);
            this.dgvMultiSelection.Name = "dgvMultiSelection";
            this.dgvMultiSelection.Size = new System.Drawing.Size(583, 224);
            this.dgvMultiSelection.TabIndex = 1059;
            this.dgvMultiSelection.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellContentClick);
            this.dgvMultiSelection.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellValueChanged);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectAll.Location = new System.Drawing.Point(28, 77);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(91, 21);
            this.cbSelectAll.TabIndex = 36;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // UclStockListCurrentStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclStockListCurrentStock";
            this.Size = new System.Drawing.Size(991, 548);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtOpeningSTock;
        private System.Windows.Forms.RadioButton rbtClosingStock;
        private System.Windows.Forms.ToolTip ttToolTip;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNoofSearches;
        private PharmaSYSPlus.CommonLibrary.PSButton btnViewList;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel6;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel5;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvSelected;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private System.Windows.Forms.DataGridView dgvMultiSelection;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbCompany;
        private System.Windows.Forms.CheckBox cbZero;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
    }
}
