namespace EcoMart.Reporting.Controls
{
    partial class UclDistributorAreawiseOutstanding
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclDistributorAreawiseOutstanding));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewText = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.psLabel5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbArea = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.psLabel4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel6 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.psLabel8 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel7 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtOutStanding = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtReportTotal = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(915, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.psLabel8);
            this.MMBottomPanel.Controls.Add(this.psLabel7);
            this.MMBottomPanel.Controls.Add(this.txtOutStanding);
            this.MMBottomPanel.Controls.Add(this.txtReportTotal);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 503);
            this.MMBottomPanel.Size = new System.Drawing.Size(917, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotal, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtOutStanding, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel7, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel8, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Size = new System.Drawing.Size(917, 451);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlTop, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
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
            this.pnlTop.Size = new System.Drawing.Size(915, 33);
            this.pnlTop.TabIndex = 1055;
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(93, 5);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(33, 14);
            this.psLabel3.TabIndex = 1069;
            this.psLabel3.Text = "Area";
            // 
            // txtViewText
            // 
            this.txtViewText.BackColor = System.Drawing.Color.Snow;
            this.txtViewText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewText.Location = new System.Drawing.Point(148, 2);
            this.txtViewText.MaxLength = 50;
            this.txtViewText.Name = "txtViewText";
            this.txtViewText.Size = new System.Drawing.Size(310, 24);
            this.txtViewText.TabIndex = 1068;
            this.txtViewText.TabStop = false;
            this.txtViewText.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(839, 5);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1049;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(690, 5);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1048;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(805, 5);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(21, 14);
            this.psLabel2.TabIndex = 1047;
            this.psLabel2.Text = "To";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(627, 5);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(38, 14);
            this.psLabel1.TabIndex = 1046;
            this.psLabel1.Text = "From";
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
            this.dgvReportList.Size = new System.Drawing.Size(915, 416);
            this.dgvReportList.TabIndex = 1058;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.psLabel5);
            this.pnlMultiSelection1.Controls.Add(this.mcbArea);
            this.pnlMultiSelection1.Controls.Add(this.psLabel4);
            this.pnlMultiSelection1.Controls.Add(this.psLabel6);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(223, 174);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(468, 100);
            this.pnlMultiSelection1.TabIndex = 1059;
            // 
            // psLabel5
            // 
            this.psLabel5.AutoSize = true;
            this.psLabel5.Location = new System.Drawing.Point(17, 58);
            this.psLabel5.Name = "psLabel5";
            this.psLabel5.Size = new System.Drawing.Size(33, 14);
            this.psLabel5.TabIndex = 1076;
            this.psLabel5.Text = "Area";
            // 
            // mcbArea
            // 
            this.mcbArea.ColumnWidth = null;
            this.mcbArea.DataSource = null;
            this.mcbArea.DisplayColumnNo = 1;
            this.mcbArea.DropDownHeight = 200;
            this.mcbArea.Location = new System.Drawing.Point(72, 55);
            this.mcbArea.Margin = new System.Windows.Forms.Padding(4);
            this.mcbArea.Name = "mcbArea";
            this.mcbArea.SelectedID = null;
            this.mcbArea.ShowNew = false;
            this.mcbArea.Size = new System.Drawing.Size(317, 22);
            this.mcbArea.SourceDataString = null;
            this.mcbArea.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbArea.TabIndex = 1075;
            this.mcbArea.UserControlToShow = null;
            this.mcbArea.ValueColumnNo = 0;
            this.mcbArea.EnterKeyPressed += new System.EventHandler(this.mcbArea_EnterKeyPressed);
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(217, 18);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(21, 14);
            this.psLabel4.TabIndex = 1068;
            this.psLabel4.Text = "To";
            // 
            // psLabel6
            // 
            this.psLabel6.AutoSize = true;
            this.psLabel6.Location = new System.Drawing.Point(18, 18);
            this.psLabel6.Name = "psLabel6";
            this.psLabel6.Size = new System.Drawing.Size(38, 14);
            this.psLabel6.TabIndex = 1067;
            this.psLabel6.Text = "From";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(251, 15);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 24);
            this.toDate1.TabIndex = 1066;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(72, 15);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 1065;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(400, 3);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 6;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // psLabel8
            // 
            this.psLabel8.AutoSize = true;
            this.psLabel8.Location = new System.Drawing.Point(207, 0);
            this.psLabel8.Name = "psLabel8";
            this.psLabel8.Size = new System.Drawing.Size(29, 14);
            this.psLabel8.TabIndex = 13;
            this.psLabel8.Text = "Sale";
            // 
            // psLabel7
            // 
            this.psLabel7.AutoSize = true;
            this.psLabel7.Location = new System.Drawing.Point(439, 0);
            this.psLabel7.Name = "psLabel7";
            this.psLabel7.Size = new System.Drawing.Size(73, 14);
            this.psLabel7.TabIndex = 12;
            this.psLabel7.Text = "OutStanding";
            // 
            // txtOutStanding
            // 
            this.txtOutStanding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutStanding.CausesValidation = false;
            this.txtOutStanding.Location = new System.Drawing.Point(554, -2);
            this.txtOutStanding.Name = "txtOutStanding";
            this.txtOutStanding.Size = new System.Drawing.Size(153, 23);
            this.txtOutStanding.TabIndex = 11;
            this.txtOutStanding.Text = "label";
            this.txtOutStanding.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReportTotal
            // 
            this.txtReportTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotal.CausesValidation = false;
            this.txtReportTotal.Location = new System.Drawing.Point(257, -1);
            this.txtReportTotal.Name = "txtReportTotal";
            this.txtReportTotal.Size = new System.Drawing.Size(153, 23);
            this.txtReportTotal.TabIndex = 10;
            this.txtReportTotal.Text = "label";
            this.txtReportTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UclDistributorAreawiseOutstanding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclDistributorAreawiseOutstanding";
            this.Size = new System.Drawing.Size(917, 526);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private InterfaceLayer.CommonControls.PSLabel psLabel3;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtViewText;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private InterfaceLayer.CommonControls.PSLabel psLabel2;
        private InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private InterfaceLayer.CommonControls.PSLabel psLabel5;
        private InterfaceLayer.CommonControls.PSComboBoxNew mcbArea;
        private InterfaceLayer.CommonControls.PSLabel psLabel4;
        private InterfaceLayer.CommonControls.PSLabel psLabel6;
        private InterfaceLayer.CommonControls.ToDate toDate1;
        private InterfaceLayer.CommonControls.FromDate fromDate1;
        private InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private InterfaceLayer.CommonControls.PSLabel psLabel8;
        private InterfaceLayer.CommonControls.PSLabel psLabel7;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtOutStanding;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtReportTotal;
    }
}
