namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclListTodaysCheques
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclListTodaysCheques));
            this.pnlMultiSelection = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.psLabel5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ViewToDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.txtReportTotalAmount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.psLabel3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlMultiSelection.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(961, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.psLabel3);
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmount);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 492);
            this.MMBottomPanel.Size = new System.Drawing.Size(963, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel3, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Size = new System.Drawing.Size(963, 440);
            // 
            // pnlMultiSelection
            // 
            this.pnlMultiSelection.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection.Controls.Add(this.FromDate);
            this.pnlMultiSelection.Controls.Add(this.psLabel5);
            this.pnlMultiSelection.Controls.Add(this.psLabel4);
            this.pnlMultiSelection.Controls.Add(this.ToDate);
            this.pnlMultiSelection.Location = new System.Drawing.Point(286, 91);
            this.pnlMultiSelection.Name = "pnlMultiSelection";
            this.pnlMultiSelection.Size = new System.Drawing.Size(481, 75);
            this.pnlMultiSelection.TabIndex = 1048;
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(411, 2);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 1085;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // FromDate
            // 
            this.FromDate.CustomFormat = "";
            this.FromDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(96, 22);
            this.FromDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.FromDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(120, 26);
            this.FromDate.TabIndex = 134;
            this.FromDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.FromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FromDate_KeyDown);
            // 
            // psLabel5
            // 
            this.psLabel5.AutoSize = true;
            this.psLabel5.Location = new System.Drawing.Point(240, 24);
            this.psLabel5.Name = "psLabel5";
            this.psLabel5.Size = new System.Drawing.Size(28, 19);
            this.psLabel5.TabIndex = 1082;
            this.psLabel5.Text = "To";
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(36, 24);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(48, 19);
            this.psLabel4.TabIndex = 1081;
            this.psLabel4.Text = "From";
            // 
            // ToDate
            // 
            this.ToDate.CustomFormat = "";
            this.ToDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(277, 22);
            this.ToDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ToDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(120, 26);
            this.ToDate.TabIndex = 135;
            this.ToDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.ToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ToDate_KeyDown);
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
            this.pnlTop.Size = new System.Drawing.Size(961, 33);
            this.pnlTop.TabIndex = 1049;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(841, 5);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1082;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(692, 5);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1081;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(807, 5);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(28, 19);
            this.psLabel2.TabIndex = 1080;
            this.psLabel2.Text = "To";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(629, 5);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(48, 19);
            this.psLabel1.TabIndex = 1079;
            this.psLabel1.Text = "From";
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = false;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(961, 405);
            this.dgvReportList.TabIndex = 1050;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmount.CausesValidation = false;
            this.txtReportTotalAmount.Location = new System.Drawing.Point(755, 0);
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.Size = new System.Drawing.Size(144, 23);
            this.txtReportTotalAmount.TabIndex = 1;
            this.txtReportTotalAmount.Text = "label";
            this.txtReportTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(681, 1);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(48, 19);
            this.psLabel3.TabIndex = 1080;
            this.psLabel3.Text = "Total";
            // 
            // UclListTodaysCheques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclListTodaysCheques";
            this.Size = new System.Drawing.Size(963, 515);
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

        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private System.Windows.Forms.DateTimePicker FromDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel5;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel4;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.Panel pnlTop;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtReportTotalAmount;
    }
}
