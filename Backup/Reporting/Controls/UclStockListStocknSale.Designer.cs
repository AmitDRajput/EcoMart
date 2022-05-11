namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclStockListStocknSale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStockListStocknSale));
            this.cbZero = new System.Windows.Forms.CheckBox();
            this.mcbCompany = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMultiSelection = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.psLabel4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.mPlbl5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate(this.components);
            this.pnlTop = new System.Windows.Forms.Panel();
            this.psLabel3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewText = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewToDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlMultiSelection.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(968, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 531);
            this.MMBottomPanel.Size = new System.Drawing.Size(970, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Size = new System.Drawing.Size(970, 479);
            // 
            // cbZero
            // 
            this.cbZero.AutoSize = true;
            this.cbZero.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbZero.Location = new System.Drawing.Point(404, 75);
            this.cbZero.Name = "cbZero";
            this.cbZero.Size = new System.Drawing.Size(95, 21);
            this.cbZero.TabIndex = 1049;
            this.cbZero.Text = "With Zero";
            this.cbZero.UseVisualStyleBackColor = true;
            // 
            // mcbCompany
            // 
            this.mcbCompany.ColumnWidth = null;
            this.mcbCompany.DataSource = null;
            this.mcbCompany.DisplayColumnNo = 1;
            this.mcbCompany.DropDownHeight = 200;
            this.mcbCompany.Location = new System.Drawing.Point(107, 45);
            this.mcbCompany.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbCompany.Name = "mcbCompany";
            this.mcbCompany.SelectedID = null;
            this.mcbCompany.ShowNew = false;
            this.mcbCompany.Size = new System.Drawing.Size(293, 26);
            this.mcbCompany.SourceDataString = null;
            this.mcbCompany.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCompany.TabIndex = 1040;
            this.mcbCompany.UserControlToShow = null;
            this.mcbCompany.ValueColumnNo = 0;
            this.mcbCompany.EnterKeyPressed += new System.EventHandler(this.mcbCompany_EnterKeyPressed);
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = false;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.Khaki;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(968, 444);
            this.dgvReportList.TabIndex = 1052;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // pnlMultiSelection
            // 
            this.pnlMultiSelection.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection.Controls.Add(this.psLabel4);
            this.pnlMultiSelection.Controls.Add(this.mcbCompany);
            this.pnlMultiSelection.Controls.Add(this.cbZero);
            this.pnlMultiSelection.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection.Controls.Add(this.mPlbl5);
            this.pnlMultiSelection.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection.Controls.Add(this.toDate1);
            this.pnlMultiSelection.Controls.Add(this.fromDate1);
            this.pnlMultiSelection.Location = new System.Drawing.Point(323, 102);
            this.pnlMultiSelection.Name = "pnlMultiSelection";
            this.pnlMultiSelection.Size = new System.Drawing.Size(509, 105);
            this.pnlMultiSelection.TabIndex = 1053;
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(9, 47);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(78, 19);
            this.psLabel4.TabIndex = 1081;
            this.psLabel4.Text = "Company";
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(441, 3);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 1066;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(245, 16);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(28, 19);
            this.mPlbl5.TabIndex = 1064;
            this.mPlbl5.Text = "To";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(39, 13);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(48, 19);
            this.mPlbl1.TabIndex = 1063;
            this.mPlbl1.Text = "From";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(285, 13);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 25);
            this.toDate1.TabIndex = 1062;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.todate1_KeyDown);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(107, 13);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 25);
            this.fromDate1.TabIndex = 1061;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.psLabel3);
            this.pnlTop.Controls.Add(this.txtViewText);
            this.pnlTop.Controls.Add(this.ViewToDate);
            this.pnlTop.Controls.Add(this.ViewFromDate);
            this.pnlTop.Controls.Add(this.psLabel2);
            this.pnlTop.Controls.Add(this.psLabel1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(968, 33);
            this.pnlTop.TabIndex = 1054;
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(35, 6);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(78, 19);
            this.psLabel3.TabIndex = 1080;
            this.psLabel3.Text = "Company";
            // 
            // txtViewText
            // 
            this.txtViewText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewText.Location = new System.Drawing.Point(120, 4);
            this.txtViewText.Name = "txtViewText";
            this.txtViewText.Size = new System.Drawing.Size(335, 23);
            this.txtViewText.TabIndex = 1079;
            this.txtViewText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(837, 4);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1077;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(688, 4);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1076;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(803, 4);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(28, 19);
            this.psLabel2.TabIndex = 1075;
            this.psLabel2.Text = "To";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(625, 4);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(48, 19);
            this.psLabel1.TabIndex = 1074;
            this.psLabel1.Text = "From";
            // 
            // UclStockListStocknSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclStockListStocknSale";
            this.Size = new System.Drawing.Size(970, 554);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlMultiSelection.ResumeLayout(false);
            this.pnlMultiSelection.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbCompany;

        private System.Windows.Forms.CheckBox cbZero;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.ToolTip ttToolTip;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate toDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate fromDate1;
        private System.Windows.Forms.Panel pnlTop;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft txtViewText;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel4;
    }
}
