namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclSaleListPartySaleSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclSaleListPartySaleSummary));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.ViewToDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.pnlMultiSelection = new System.Windows.Forms.Panel();
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.psLabel4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate(this.components);
            this.psLabel3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.fromDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate(this.components);
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.psLabel5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel6 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel7 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtCashTotal = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtCreditTotal = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtCrStatmentTotal = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(975, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtCrStatmentTotal);
            this.MMBottomPanel.Controls.Add(this.txtCreditTotal);
            this.MMBottomPanel.Controls.Add(this.txtCashTotal);
            this.MMBottomPanel.Controls.Add(this.psLabel7);
            this.MMBottomPanel.Controls.Add(this.psLabel6);
            this.MMBottomPanel.Controls.Add(this.psLabel5);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 501);
            this.MMBottomPanel.Size = new System.Drawing.Size(977, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel5, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel6, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel7, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtCashTotal, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtCreditTotal, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtCrStatmentTotal, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(977, 449);
            // 
            // pnlGo
            // 
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.psLabel2);
            this.pnlGo.Controls.Add(this.psLabel1);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(975, 33);
            this.pnlGo.TabIndex = 9;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(861, 3);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1057;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(712, 3);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1056;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(827, 3);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(28, 19);
            this.psLabel2.TabIndex = 1055;
            this.psLabel2.Text = "To";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(649, 3);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(48, 19);
            this.psLabel1.TabIndex = 1054;
            this.psLabel1.Text = "From";
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
            this.pnlMultiSelection.Location = new System.Drawing.Point(217, 133);
            this.pnlMultiSelection.Name = "pnlMultiSelection";
            this.pnlMultiSelection.Size = new System.Drawing.Size(453, 72);
            this.pnlMultiSelection.TabIndex = 1040;
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
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection1_Click);
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
            this.dgvReportList.Size = new System.Drawing.Size(975, 414);
            this.dgvReportList.TabIndex = 1046;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // psLabel5
            // 
            this.psLabel5.AutoSize = true;
            this.psLabel5.Location = new System.Drawing.Point(213, 1);
            this.psLabel5.Name = "psLabel5";
            this.psLabel5.Size = new System.Drawing.Size(78, 19);
            this.psLabel5.TabIndex = 1055;
            this.psLabel5.Text = "Cash Sale";
            // 
            // psLabel6
            // 
            this.psLabel6.AutoSize = true;
            this.psLabel6.Location = new System.Drawing.Point(447, 1);
            this.psLabel6.Name = "psLabel6";
            this.psLabel6.Size = new System.Drawing.Size(88, 19);
            this.psLabel6.TabIndex = 1056;
            this.psLabel6.Text = "Credit Sale";
            // 
            // psLabel7
            // 
            this.psLabel7.AutoSize = true;
            this.psLabel7.Location = new System.Drawing.Point(693, 0);
            this.psLabel7.Name = "psLabel7";
            this.psLabel7.Size = new System.Drawing.Size(109, 19);
            this.psLabel7.TabIndex = 1057;
            this.psLabel7.Text = "Cr/STMT Sale";
            // 
            // txtCashTotal
            // 
            this.txtCashTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCashTotal.CausesValidation = false;
            this.txtCashTotal.Location = new System.Drawing.Point(298, -1);
            this.txtCashTotal.Name = "txtCashTotal";
            this.txtCashTotal.Size = new System.Drawing.Size(134, 23);
            this.txtCashTotal.TabIndex = 1058;
            this.txtCashTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreditTotal
            // 
            this.txtCreditTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditTotal.CausesValidation = false;
            this.txtCreditTotal.Location = new System.Drawing.Point(536, -1);
            this.txtCreditTotal.Name = "txtCreditTotal";
            this.txtCreditTotal.Size = new System.Drawing.Size(134, 23);
            this.txtCreditTotal.TabIndex = 1059;
            this.txtCreditTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCrStatmentTotal
            // 
            this.txtCrStatmentTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCrStatmentTotal.CausesValidation = false;
            this.txtCrStatmentTotal.Location = new System.Drawing.Point(808, 0);
            this.txtCrStatmentTotal.Name = "txtCrStatmentTotal";
            this.txtCrStatmentTotal.Size = new System.Drawing.Size(134, 23);
            this.txtCrStatmentTotal.TabIndex = 1060;
            this.txtCrStatmentTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UclSaleListPartySaleSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclSaleListPartySaleSummary";
            this.Size = new System.Drawing.Size(977, 524);
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
        private System.Windows.Forms.Panel pnlMultiSelection;
        private System.Windows.Forms.ToolTip ttToolTip;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate fromDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate toDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtCrStatmentTotal;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtCreditTotal;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtCashTotal;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel7;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel6;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel5;
    }
}
