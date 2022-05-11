namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclSaleListCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclSaleListCategory));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.ViewToDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.lblToDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.ViewFromDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.lblFromDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.ttSaleCategory = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMultiSelection = new System.Windows.Forms.Panel();
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.psLabel4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate(this.components);
            this.psLabel3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.fromDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate(this.components);
            this.txtReportTotalAmount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(970, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmount);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 546);
            this.MMBottomPanel.Size = new System.Drawing.Size(972, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(972, 494);
            // 
            // pnlGo
            // 
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.lblToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.lblFromDate);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(970, 33);
            this.pnlGo.TabIndex = 19;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.CausesValidation = false;
            this.ViewToDate.Location = new System.Drawing.Point(781, 3);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1053;
            this.ViewToDate.Text = "label";
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(741, 5);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(28, 19);
            this.lblToDate.TabIndex = 1052;
            this.lblToDate.Text = "To";
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.CausesValidation = false;
            this.ViewFromDate.Location = new System.Drawing.Point(635, 3);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1051;
            this.ViewFromDate.Text = "label";
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(579, 5);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(48, 19);
            this.lblFromDate.TabIndex = 1050;
            this.lblFromDate.Text = "From";
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
            this.dgvReportList.Size = new System.Drawing.Size(970, 459);
            this.dgvReportList.TabIndex = 1031;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // ttSaleCategory
            // 
            this.ttSaleCategory.ShowAlways = true;
            // 
            // pnlMultiSelection
            // 
            this.pnlMultiSelection.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.pnlMultiSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection.Controls.Add(this.psLabel4);
            this.pnlMultiSelection.Controls.Add(this.toDate1);
            this.pnlMultiSelection.Controls.Add(this.psLabel3);
            this.pnlMultiSelection.Controls.Add(this.fromDate1);
            this.pnlMultiSelection.Location = new System.Drawing.Point(266, 125);
            this.pnlMultiSelection.Name = "pnlMultiSelection";
            this.pnlMultiSelection.Size = new System.Drawing.Size(453, 72);
            this.pnlMultiSelection.TabIndex = 1041;
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(385, 3);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 1068;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(217, 25);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(28, 19);
            this.psLabel4.TabIndex = 1067;
            this.psLabel4.Text = "To";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(254, 22);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 25);
            this.toDate1.TabIndex = 1066;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(25, 25);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(48, 19);
            this.psLabel3.TabIndex = 1065;
            this.psLabel3.Text = "From";
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(79, 22);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 25);
            this.fromDate1.TabIndex = 1064;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmount.CausesValidation = false;
            this.txtReportTotalAmount.Location = new System.Drawing.Point(528, 0);
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.Size = new System.Drawing.Size(156, 23);
            this.txtReportTotalAmount.TabIndex = 1;
            this.txtReportTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UclSaleListCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclSaleListCategory";
            this.Size = new System.Drawing.Size(972, 569);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.pnlMultiSelection.ResumeLayout(false);
            this.pnlMultiSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGo;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.ToolTip ttSaleCategory;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight ViewToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight ViewFromDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblFromDate;
        private System.Windows.Forms.Panel pnlMultiSelection;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate toDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate fromDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtReportTotalAmount;
        
    }
}
