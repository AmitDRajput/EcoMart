namespace EcoMart.Reporting.Controls
{
    partial class UclPurchaseListDiscount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclPurchaseListDiscount));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.ViewToDate = new System.Windows.Forms.DateTimePicker();
            this.ViewFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblViewTo = new System.Windows.Forms.Label();
            this.lblViewFrom = new System.Windows.Forms.Label();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.txtReportTotalAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtTotScmDiscount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtTotCashDiscount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtTotSplDiscount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtTotItemDiscount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.ttPurchaseDiscount = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.psLabel4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(981, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtTotItemDiscount);
            this.MMBottomPanel.Controls.Add(this.txtTotSplDiscount);
            this.MMBottomPanel.Controls.Add(this.txtTotCashDiscount);
            this.MMBottomPanel.Controls.Add(this.txtTotScmDiscount);
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmount);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 542);
            this.MMBottomPanel.Size = new System.Drawing.Size(983, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotScmDiscount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotCashDiscount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotSplDiscount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotItemDiscount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(983, 490);
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
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(981, 33);
            this.pnlGo.TabIndex = 9;
            // 
            // ViewToDate
            // 
            this.ViewToDate.CustomFormat = "dd/MM/yy";
            this.ViewToDate.Enabled = false;
            this.ViewToDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ViewToDate.Location = new System.Drawing.Point(835, 3);
            this.ViewToDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ViewToDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(122, 26);
            this.ViewToDate.TabIndex = 1068;
            this.ViewToDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.CalendarFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewFromDate.CustomFormat = "dd/MM/yy";
            this.ViewFromDate.Enabled = false;
            this.ViewFromDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ViewFromDate.Location = new System.Drawing.Point(671, 3);
            this.ViewFromDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ViewFromDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(122, 26);
            this.ViewFromDate.TabIndex = 1067;
            this.ViewFromDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // lblViewTo
            // 
            this.lblViewTo.AutoSize = true;
            this.lblViewTo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewTo.Location = new System.Drawing.Point(809, 8);
            this.lblViewTo.Name = "lblViewTo";
            this.lblViewTo.Size = new System.Drawing.Size(22, 16);
            this.lblViewTo.TabIndex = 1066;
            this.lblViewTo.Text = "To";
            // 
            // lblViewFrom
            // 
            this.lblViewFrom.AutoSize = true;
            this.lblViewFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewFrom.Location = new System.Drawing.Point(628, 8);
            this.lblViewFrom.Name = "lblViewFrom";
            this.lblViewFrom.Size = new System.Drawing.Size(40, 16);
            this.lblViewFrom.TabIndex = 1065;
            this.lblViewFrom.Text = "From";
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
            this.dgvReportList.Size = new System.Drawing.Size(981, 455);
            this.dgvReportList.TabIndex = 1031;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BackColor = System.Drawing.Color.Linen;
            this.txtReportTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportTotalAmount.Location = new System.Drawing.Point(858, 0);
            this.txtReportTotalAmount.MaxLength = 15;
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.ReadOnly = true;
            this.txtReportTotalAmount.Size = new System.Drawing.Size(100, 20);
            this.txtReportTotalAmount.TabIndex = 1046;
            this.txtReportTotalAmount.TabStop = false;
            this.txtReportTotalAmount.Tag = "0.00";
            this.txtReportTotalAmount.Text = "0.00";
            this.txtReportTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotScmDiscount
            // 
            this.txtTotScmDiscount.BackColor = System.Drawing.Color.Linen;
            this.txtTotScmDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotScmDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotScmDiscount.Location = new System.Drawing.Point(698, 0);
            this.txtTotScmDiscount.MaxLength = 15;
            this.txtTotScmDiscount.Name = "txtTotScmDiscount";
            this.txtTotScmDiscount.ReadOnly = true;
            this.txtTotScmDiscount.Size = new System.Drawing.Size(80, 20);
            this.txtTotScmDiscount.TabIndex = 1047;
            this.txtTotScmDiscount.TabStop = false;
            this.txtTotScmDiscount.Tag = "0.00";
            this.txtTotScmDiscount.Text = "0.00";
            this.txtTotScmDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotCashDiscount
            // 
            this.txtTotCashDiscount.BackColor = System.Drawing.Color.Linen;
            this.txtTotCashDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotCashDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotCashDiscount.Location = new System.Drawing.Point(778, 0);
            this.txtTotCashDiscount.MaxLength = 15;
            this.txtTotCashDiscount.Name = "txtTotCashDiscount";
            this.txtTotCashDiscount.ReadOnly = true;
            this.txtTotCashDiscount.Size = new System.Drawing.Size(80, 20);
            this.txtTotCashDiscount.TabIndex = 1048;
            this.txtTotCashDiscount.TabStop = false;
            this.txtTotCashDiscount.Tag = "0.00";
            this.txtTotCashDiscount.Text = "0.00";
            this.txtTotCashDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotSplDiscount
            // 
            this.txtTotSplDiscount.BackColor = System.Drawing.Color.Linen;
            this.txtTotSplDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotSplDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotSplDiscount.Location = new System.Drawing.Point(618, 0);
            this.txtTotSplDiscount.MaxLength = 15;
            this.txtTotSplDiscount.Name = "txtTotSplDiscount";
            this.txtTotSplDiscount.ReadOnly = true;
            this.txtTotSplDiscount.Size = new System.Drawing.Size(80, 20);
            this.txtTotSplDiscount.TabIndex = 1049;
            this.txtTotSplDiscount.TabStop = false;
            this.txtTotSplDiscount.Tag = "0.00";
            this.txtTotSplDiscount.Text = "0.00";
            this.txtTotSplDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotItemDiscount
            // 
            this.txtTotItemDiscount.BackColor = System.Drawing.Color.Linen;
            this.txtTotItemDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotItemDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotItemDiscount.Location = new System.Drawing.Point(538, 0);
            this.txtTotItemDiscount.MaxLength = 15;
            this.txtTotItemDiscount.Name = "txtTotItemDiscount";
            this.txtTotItemDiscount.ReadOnly = true;
            this.txtTotItemDiscount.Size = new System.Drawing.Size(80, 20);
            this.txtTotItemDiscount.TabIndex = 1050;
            this.txtTotItemDiscount.TabStop = false;
            this.txtTotItemDiscount.Tag = "0.00";
            this.txtTotItemDiscount.Text = "0.00";
            this.txtTotItemDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ttPurchaseDiscount
            // 
            this.ttPurchaseDiscount.ShowAlways = true;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.psLabel4);
            this.pnlMultiSelection1.Controls.Add(this.psLabel3);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(356, 129);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(462, 69);
            this.pnlMultiSelection1.TabIndex = 0;
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(226, 23);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(22, 17);
            this.psLabel4.TabIndex = 1072;
            this.psLabel4.Text = "To";
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(27, 23);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(41, 17);
            this.psLabel3.TabIndex = 1071;
            this.psLabel3.Text = "From";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(260, 20);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 24);
            this.toDate1.TabIndex = 1070;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(81, 20);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 1069;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(394, 2);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 4;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // UclPurchaseListDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclPurchaseListDiscount";
            this.Size = new System.Drawing.Size(983, 565);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGo;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
       
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotItemDiscount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotSplDiscount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotCashDiscount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotScmDiscount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtReportTotalAmount;
        private System.Windows.Forms.DateTimePicker ViewToDate;
        private System.Windows.Forms.DateTimePicker ViewFromDate;
        private System.Windows.Forms.Label lblViewTo;
        private System.Windows.Forms.Label lblViewFrom;
        private System.Windows.Forms.ToolTip ttPurchaseDiscount;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel4;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private EcoMart.InterfaceLayer.CommonControls.ToDate toDate1;
        private EcoMart.InterfaceLayer.CommonControls.FromDate fromDate1;
       
    }
}
