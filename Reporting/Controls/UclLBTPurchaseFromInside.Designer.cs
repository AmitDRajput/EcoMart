namespace PharmaSYSDistributorPlus.Reporting.Controls
{
    partial class UclLBTPurchaseFromInside
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclLBTPurchaseFromInside));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.ViewToDate = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlMultiSelection1 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.psLabel4 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel3 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.FromDate(this.components);
            this.btnOKMultiSelection1 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.txtLBTTotalAmount = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtPurchaseTotalAmount = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.psLabel7 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(966, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.psLabel7);
            this.MMBottomPanel.Controls.Add(this.txtPurchaseTotalAmount);
            this.MMBottomPanel.Controls.Add(this.txtLBTTotalAmount);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 482);
            this.MMBottomPanel.Size = new System.Drawing.Size(968, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtLBTTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtPurchaseTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel7, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(968, 430);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlGo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
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
            this.pnlGo.Size = new System.Drawing.Size(966, 33);
            this.pnlGo.TabIndex = 1045;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(851, 5);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1069;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(702, 5);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1068;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(817, 5);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(27, 19);
            this.psLabel2.TabIndex = 1067;
            this.psLabel2.Text = "To";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(639, 5);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(48, 19);
            this.psLabel1.TabIndex = 1066;
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
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(966, 395);
            this.dgvReportList.TabIndex = 1046;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
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
            this.pnlMultiSelection1.Location = new System.Drawing.Point(255, 164);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(456, 71);
            this.pnlMultiSelection1.TabIndex = 1047;
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(216, 24);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(27, 19);
            this.psLabel4.TabIndex = 1068;
            this.psLabel4.Text = "To";
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(15, 24);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(48, 19);
            this.psLabel3.TabIndex = 1067;
            this.psLabel3.Text = "From";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(251, 21);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 25);
            this.toDate1.TabIndex = 1066;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(72, 21);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 25);
            this.fromDate1.TabIndex = 1065;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(388, 3);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 6;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection1_Click);
            // 
            // txtLBTTotalAmount
            // 
            this.txtLBTTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLBTTotalAmount.CausesValidation = false;
            this.txtLBTTotalAmount.Location = new System.Drawing.Point(775, 0);
            this.txtLBTTotalAmount.Name = "txtLBTTotalAmount";
            this.txtLBTTotalAmount.Size = new System.Drawing.Size(157, 23);
            this.txtLBTTotalAmount.TabIndex = 4;
            this.txtLBTTotalAmount.Text = "label";
            this.txtLBTTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPurchaseTotalAmount
            // 
            this.txtPurchaseTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurchaseTotalAmount.CausesValidation = false;
            this.txtPurchaseTotalAmount.Location = new System.Drawing.Point(562, -1);
            this.txtPurchaseTotalAmount.Name = "txtPurchaseTotalAmount";
            this.txtPurchaseTotalAmount.Size = new System.Drawing.Size(157, 23);
            this.txtPurchaseTotalAmount.TabIndex = 5;
            this.txtPurchaseTotalAmount.Text = "label";
            this.txtPurchaseTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // psLabel7
            // 
            this.psLabel7.AutoSize = true;
            this.psLabel7.Location = new System.Drawing.Point(413, 1);
            this.psLabel7.Name = "psLabel7";
            this.psLabel7.Size = new System.Drawing.Size(141, 19);
            this.psLabel7.TabIndex = 1069;
            this.psLabel7.Text = "Purchase Amount";
            // 
            // UclLBTPurchaseFromInside
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclLBTPurchaseFromInside";
            this.Size = new System.Drawing.Size(968, 505);
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
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private InterfaceLayer.CommonControls.PSLabel psLabel2;
        private InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private InterfaceLayer.CommonControls.PSLabel psLabel4;
        private InterfaceLayer.CommonControls.PSLabel psLabel3;
        private InterfaceLayer.CommonControls.ToDate toDate1;
        private InterfaceLayer.CommonControls.FromDate fromDate1;
        private InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtPurchaseTotalAmount;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtLBTTotalAmount;
        private InterfaceLayer.CommonControls.PSLabel psLabel7;
    }
}
