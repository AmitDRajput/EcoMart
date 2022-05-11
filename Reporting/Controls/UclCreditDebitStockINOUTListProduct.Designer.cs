namespace EcoMart.Reporting.Controls
{
    partial class UclCreditDebitStockINOUTListProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclCreditDebitStockINOUTListProduct));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.mPlbl5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.txtViewtext = new System.Windows.Forms.TextBox();
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbProduct = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(975, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 556);
            this.MMBottomPanel.Size = new System.Drawing.Size(977, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel1);
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Size = new System.Drawing.Size(977, 504);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.panel1, 0);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.psLabel3);
            this.panel1.Controls.Add(this.ViewToDate);
            this.panel1.Controls.Add(this.ViewFromDate);
            this.panel1.Controls.Add(this.psLabel2);
            this.panel1.Controls.Add(this.txtViewtext);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(975, 33);
            this.panel1.TabIndex = 1029;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 39;
            this.label1.Text = "Product";
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
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(975, 502);
            this.dgvReportList.TabIndex = 1031;
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Controls.Add(this.mcbProduct);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl5);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(318, 143);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(456, 74);
            this.pnlMultiSelection1.TabIndex = 1049;
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(211, 10);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(21, 14);
            this.mPlbl5.TabIndex = 2;
            this.mPlbl5.Text = "To";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(27, 10);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(38, 14);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "From";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(249, 8);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 24);
            this.toDate1.TabIndex = 3;
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(76, 8);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 1;
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
            this.btnOKMultiSelection1.TabIndex = 10;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            // 
            // txtViewtext
            // 
            this.txtViewtext.Enabled = false;
            this.txtViewtext.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewtext.Location = new System.Drawing.Point(83, 4);
            this.txtViewtext.Name = "txtViewtext";
            this.txtViewtext.Size = new System.Drawing.Size(250, 22);
            this.txtViewtext.TabIndex = 1045;
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(659, 6);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(38, 14);
            this.psLabel3.TabIndex = 1103;
            this.psLabel3.Text = "From";
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(839, 2);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1102;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(706, 2);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1101;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(812, 6);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(21, 14);
            this.psLabel2.TabIndex = 1100;
            this.psLabel2.Text = "To";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(14, 40);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(51, 14);
            this.mPlbl2.TabIndex = 11;
            this.mPlbl2.Text = "Product";
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(81, 39);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = null;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(299, 22);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 12;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            // 
            // UclCreditDebitStockINOUTListProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Name = "UclCreditDebitStockINOUTListProduct";
            this.Size = new System.Drawing.Size(977, 579);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.ToolTip ttToolTip;
        private InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private InterfaceLayer.CommonControls.ToDate toDate1;
        private InterfaceLayer.CommonControls.FromDate fromDate1;
        private InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private System.Windows.Forms.TextBox txtViewtext;
        private InterfaceLayer.CommonControls.PSLabel psLabel3;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private InterfaceLayer.CommonControls.PSLabel psLabel2;
        private InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
    }
}
