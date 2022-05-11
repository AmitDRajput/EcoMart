namespace EcoMart.Reporting.Controls
{
    partial class UclFACListProfitandLoss
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclFACListProfitandLoss));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.txtClosingStock = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.pnlGo = new System.Windows.Forms.Panel();
            this.lblViewFrom = new System.Windows.Forms.Label();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.dgvTradingLeft = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.dgvTradingRight = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.dgvProfitAndLossLeft = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.dgvProfitAndLossRight = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(967, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 546);
            this.MMBottomPanel.Size = new System.Drawing.Size(969, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel2);
            this.MMMainPanel.Size = new System.Drawing.Size(969, 494);
            this.MMMainPanel.Controls.SetChildIndex(this.panel2, 0);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pnlMultiSelection1);
            this.panel2.Controls.Add(this.dgvProfitAndLossRight);
            this.panel2.Controls.Add(this.dgvProfitAndLossLeft);
            this.panel2.Controls.Add(this.dgvTradingRight);
            this.panel2.Controls.Add(this.pnlGo);
            this.panel2.Controls.Add(this.dgvTradingLeft);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(967, 492);
            this.panel2.TabIndex = 49;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.txtClosingStock);
            this.pnlMultiSelection1.Controls.Add(this.label1);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(342, 121);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(303, 128);
            this.pnlMultiSelection1.TabIndex = 1049;
            // 
            // txtClosingStock
            // 
            this.txtClosingStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClosingStock.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClosingStock.Location = new System.Drawing.Point(114, 90);
            this.txtClosingStock.MaxLength = 15;
            this.txtClosingStock.Name = "txtClosingStock";
            this.txtClosingStock.Size = new System.Drawing.Size(163, 23);
            this.txtClosingStock.TabIndex = 1082;
            this.txtClosingStock.Tag = "0.00";
            this.txtClosingStock.Text = "0.00";
            this.txtClosingStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtClosingStock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClosingStock_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 1081;
            this.label1.Text = "Closing Balance";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Enabled = false;
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(72, 45);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 24);
            this.toDate1.TabIndex = 1071;
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Enabled = false;
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(72, 10);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 1070;
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(13, 14);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(38, 14);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "From";
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(233, 2);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 3;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(33, 48);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(21, 14);
            this.mPlbl2.TabIndex = 2;
            this.mPlbl2.Text = "To";
            // 
            // pnlGo
            // 
            this.pnlGo.BackColor = System.Drawing.Color.Plum;
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.lblViewFrom);
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.psLabel2);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(965, 33);
            this.pnlGo.TabIndex = 1043;
            // 
            // lblViewFrom
            // 
            this.lblViewFrom.AutoSize = true;
            this.lblViewFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewFrom.Location = new System.Drawing.Point(638, 7);
            this.lblViewFrom.Name = "lblViewFrom";
            this.lblViewFrom.Size = new System.Drawing.Size(39, 15);
            this.lblViewFrom.TabIndex = 1079;
            this.lblViewFrom.Text = "From";
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(837, 3);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1078;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(688, 3);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1077;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(803, 3);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(21, 14);
            this.psLabel2.TabIndex = 1076;
            this.psLabel2.Text = "To";
            // 
            // dgvTradingLeft
            // 
            this.dgvTradingLeft.ApplyAlternateRowStyle = false;
            this.dgvTradingLeft.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvTradingLeft.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvTradingLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvTradingLeft.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvTradingLeft.ConvertDatetoMonth")));
            this.dgvTradingLeft.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvTradingLeft.DateColumnNames")));
            this.dgvTradingLeft.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvTradingLeft.DoubleColumnNames")));
            this.dgvTradingLeft.FreezeLastRow = false;
            this.dgvTradingLeft.Location = new System.Drawing.Point(1, 34);
            this.dgvTradingLeft.Name = "dgvTradingLeft";
            this.dgvTradingLeft.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvTradingLeft.NumericColumnNames")));
            this.dgvTradingLeft.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvTradingLeft.OptionalColumnNames")));
            this.dgvTradingLeft.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvTradingLeft.Size = new System.Drawing.Size(477, 222);
            this.dgvTradingLeft.TabIndex = 1047;
            // 
            // dgvTradingRight
            // 
            this.dgvTradingRight.ApplyAlternateRowStyle = false;
            this.dgvTradingRight.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvTradingRight.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvTradingRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvTradingRight.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvTradingRight.ConvertDatetoMonth")));
            this.dgvTradingRight.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvTradingRight.DateColumnNames")));
            this.dgvTradingRight.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvTradingRight.DoubleColumnNames")));
            this.dgvTradingRight.FreezeLastRow = false;
            this.dgvTradingRight.Location = new System.Drawing.Point(484, 34);
            this.dgvTradingRight.Name = "dgvTradingRight";
            this.dgvTradingRight.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvTradingRight.NumericColumnNames")));
            this.dgvTradingRight.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvTradingRight.OptionalColumnNames")));
            this.dgvTradingRight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvTradingRight.Size = new System.Drawing.Size(477, 222);
            this.dgvTradingRight.TabIndex = 1050;
            // 
            // dgvProfitAndLossLeft
            // 
            this.dgvProfitAndLossLeft.ApplyAlternateRowStyle = false;
            this.dgvProfitAndLossLeft.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvProfitAndLossLeft.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvProfitAndLossLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvProfitAndLossLeft.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvProfitAndLossLeft.ConvertDatetoMonth")));
            this.dgvProfitAndLossLeft.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvProfitAndLossLeft.DateColumnNames")));
            this.dgvProfitAndLossLeft.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvProfitAndLossLeft.DoubleColumnNames")));
            this.dgvProfitAndLossLeft.FreezeLastRow = false;
            this.dgvProfitAndLossLeft.Location = new System.Drawing.Point(2, 262);
            this.dgvProfitAndLossLeft.Name = "dgvProfitAndLossLeft";
            this.dgvProfitAndLossLeft.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvProfitAndLossLeft.NumericColumnNames")));
            this.dgvProfitAndLossLeft.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvProfitAndLossLeft.OptionalColumnNames")));
            this.dgvProfitAndLossLeft.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvProfitAndLossLeft.Size = new System.Drawing.Size(477, 222);
            this.dgvProfitAndLossLeft.TabIndex = 1051;
            // 
            // dgvProfitAndLossRight
            // 
            this.dgvProfitAndLossRight.ApplyAlternateRowStyle = false;
            this.dgvProfitAndLossRight.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvProfitAndLossRight.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvProfitAndLossRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvProfitAndLossRight.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvProfitAndLossRight.ConvertDatetoMonth")));
            this.dgvProfitAndLossRight.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvProfitAndLossRight.DateColumnNames")));
            this.dgvProfitAndLossRight.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvProfitAndLossRight.DoubleColumnNames")));
            this.dgvProfitAndLossRight.FreezeLastRow = false;
            this.dgvProfitAndLossRight.Location = new System.Drawing.Point(484, 261);
            this.dgvProfitAndLossRight.Name = "dgvProfitAndLossRight";
            this.dgvProfitAndLossRight.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvProfitAndLossRight.NumericColumnNames")));
            this.dgvProfitAndLossRight.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvProfitAndLossRight.OptionalColumnNames")));
            this.dgvProfitAndLossRight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvProfitAndLossRight.Size = new System.Drawing.Size(477, 222);
            this.dgvProfitAndLossRight.TabIndex = 1052;
            // 
            // UclFACListProfitandLoss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclFACListProfitandLoss";
            this.Size = new System.Drawing.Size(969, 569);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlGo;
        private System.Windows.Forms.Label lblViewFrom;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private InterfaceLayer.CommonControls.PSLabel psLabel2;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvTradingLeft;
        private InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtClosingStock;
        private System.Windows.Forms.Label label1;
        private InterfaceLayer.CommonControls.ToDate toDate1;
        private InterfaceLayer.CommonControls.FromDate fromDate1;
        private InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvProfitAndLossRight;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvProfitAndLossLeft;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvTradingRight;
               
    }
}
