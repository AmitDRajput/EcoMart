namespace EcoMart.Reporting.Controls
{
    partial class UclSaleListDailySale
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
            if (disposing)
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
        private void InitializeComponent()       {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclSaleListDailySale));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.txtViewtype = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblDate = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.txtReportTotalAmount = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.rbtnCredit = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnCreditCard = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnVoucher = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnCreditStatement = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnCash = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnAll = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(976, 23);
            this.headerLabel1.TabIndex = 0;
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmount);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 526);
            this.MMBottomPanel.Size = new System.Drawing.Size(978, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Size = new System.Drawing.Size(978, 474);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlTop, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.txtViewtype);
            this.pnlTop.Controls.Add(this.ViewFromDate);
            this.pnlTop.Controls.Add(this.psLabel1);
            this.pnlTop.Controls.Add(this.lblDate);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(976, 33);
            this.pnlTop.TabIndex = 1;
            // 
            // txtViewtype
            // 
            this.txtViewtype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewtype.Location = new System.Drawing.Point(71, 3);
            this.txtViewtype.Name = "txtViewtype";
            this.txtViewtype.Size = new System.Drawing.Size(100, 23);
            this.txtViewtype.TabIndex = 4;
            this.txtViewtype.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.CausesValidation = false;
            this.ViewFromDate.Location = new System.Drawing.Point(790, 5);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 3;
            this.ViewFromDate.Text = "label";
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(20, 5);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(38, 17);
            this.psLabel1.TabIndex = 0;
            this.psLabel1.Text = "Type";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(741, 7);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(36, 17);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Date";
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
            this.dgvReportList.Size = new System.Drawing.Size(976, 439);
            this.dgvReportList.TabIndex = 2;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmount.CausesValidation = false;
            this.txtReportTotalAmount.Location = new System.Drawing.Point(804, 0);
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.Size = new System.Drawing.Size(138, 23);
            this.txtReportTotalAmount.TabIndex = 1;
            this.txtReportTotalAmount.Text = "label";
            this.txtReportTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbtnCredit
            // 
            this.rbtnCredit.AutoSize = true;
            this.rbtnCredit.BackColor = System.Drawing.Color.White;
            this.rbtnCredit.Location = new System.Drawing.Point(170, 9);
            this.rbtnCredit.Name = "rbtnCredit";
            this.rbtnCredit.Size = new System.Drawing.Size(64, 22);
            this.rbtnCredit.TabIndex = 7;
            this.rbtnCredit.TabStop = true;
            this.rbtnCredit.Text = "Credit";
            this.rbtnCredit.UseVisualStyleBackColor = true;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.groupBox1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(327, 159);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(323, 172);
            this.pnlMultiSelection1.TabIndex = 1046;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rbtnCreditCard);
            this.groupBox1.Controls.Add(this.rbtnVoucher);
            this.groupBox1.Controls.Add(this.rbtnCreditStatement);
            this.groupBox1.Controls.Add(this.rbtnCredit);
            this.groupBox1.Controls.Add(this.rbtnCash);
            this.groupBox1.Controls.Add(this.rbtnAll);
            this.groupBox1.Location = new System.Drawing.Point(11, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 89);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // rbtnCreditCard
            // 
            this.rbtnCreditCard.AutoSize = true;
            this.rbtnCreditCard.BackColor = System.Drawing.Color.White;
            this.rbtnCreditCard.Location = new System.Drawing.Point(18, 62);
            this.rbtnCreditCard.Name = "rbtnCreditCard";
            this.rbtnCreditCard.Size = new System.Drawing.Size(96, 22);
            this.rbtnCreditCard.TabIndex = 11;
            this.rbtnCreditCard.TabStop = true;
            this.rbtnCreditCard.Text = "Credit Card";
            this.rbtnCreditCard.UseVisualStyleBackColor = true;
            // 
            // rbtnVoucher
            // 
            this.rbtnVoucher.AutoSize = true;
            this.rbtnVoucher.BackColor = System.Drawing.Color.White;
            this.rbtnVoucher.Location = new System.Drawing.Point(170, 35);
            this.rbtnVoucher.Name = "rbtnVoucher";
            this.rbtnVoucher.Size = new System.Drawing.Size(78, 22);
            this.rbtnVoucher.TabIndex = 10;
            this.rbtnVoucher.TabStop = true;
            this.rbtnVoucher.Text = "Voucher";
            this.rbtnVoucher.UseVisualStyleBackColor = true;
            // 
            // rbtnCreditStatement
            // 
            this.rbtnCreditStatement.AutoSize = true;
            this.rbtnCreditStatement.BackColor = System.Drawing.Color.White;
            this.rbtnCreditStatement.Location = new System.Drawing.Point(18, 36);
            this.rbtnCreditStatement.Name = "rbtnCreditStatement";
            this.rbtnCreditStatement.Size = new System.Drawing.Size(127, 22);
            this.rbtnCreditStatement.TabIndex = 8;
            this.rbtnCreditStatement.TabStop = true;
            this.rbtnCreditStatement.Text = "CreditStatement";
            this.rbtnCreditStatement.UseVisualStyleBackColor = true;
            // 
            // rbtnCash
            // 
            this.rbtnCash.AutoSize = true;
            this.rbtnCash.BackColor = System.Drawing.Color.White;
            this.rbtnCash.Location = new System.Drawing.Point(91, 10);
            this.rbtnCash.Name = "rbtnCash";
            this.rbtnCash.Size = new System.Drawing.Size(55, 22);
            this.rbtnCash.TabIndex = 6;
            this.rbtnCash.TabStop = true;
            this.rbtnCash.Text = "Cash";
            this.rbtnCash.UseVisualStyleBackColor = true;
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.BackColor = System.Drawing.Color.White;
            this.rbtnAll.Location = new System.Drawing.Point(18, 10);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(43, 22);
            this.rbtnAll.TabIndex = 5;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "All";
            this.rbtnAll.UseVisualStyleBackColor = true;
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(15, 19);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(36, 17);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "Date";
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(254, 4);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 3;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection1_Click);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(70, 15);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 0;
            // 
            // UclSaleListDailySale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclSaleListDailySale";
            this.Size = new System.Drawing.Size(978, 549);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.ToolTip ttToolTip;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight ViewFromDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft txtViewtype;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtReportTotalAmount;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private System.Windows.Forms.GroupBox groupBox1;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtnCreditStatement;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtnCredit;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtnCash;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtnAll;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.FromDate fromDate1;
        private InterfaceLayer.CommonControls.PSRadioButton rbtnVoucher;
        private InterfaceLayer.CommonControls.PSRadioButton rbtnCreditCard;
       
    }
}
