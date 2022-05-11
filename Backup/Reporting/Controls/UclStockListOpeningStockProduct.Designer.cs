namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclStockListOpeningStockProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStockListOpeningStockProduct));
            this.lblProduct = new System.Windows.Forms.Label();
            this.mcbProduct = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
          
            this.txtTotQuantity = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.txtReportTotalAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.pnlMultiSelection = new System.Windows.Forms.Panel();
            this.lblTo = new System.Windows.Forms.Label();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.btnOKMultiSelection = new System.Windows.Forms.Button();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlMultiSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(974, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtTotQuantity);
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmount);
           
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 534);
            this.MMBottomPanel.Size = new System.Drawing.Size(976, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Size = new System.Drawing.Size(976, 482);
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(0, 99);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(55, 15);
            this.lblProduct.TabIndex = 1037;
            this.lblProduct.Text = "Product";
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(60, 99);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = null;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(319, 22);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 1036;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            // 
            // dgvReportList
            // 
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.Khaki;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(974, 480);
            this.dgvReportList.TabIndex = 1032;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
           
            // 
            // txtTotQuantity
            // 
            this.txtTotQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotQuantity.Location = new System.Drawing.Point(472, 0);
            this.txtTotQuantity.Name = "txtTotQuantity";
            this.txtTotQuantity.Size = new System.Drawing.Size(86, 20);
            this.txtTotQuantity.TabIndex = 1048;
            this.txtTotQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BackColor = System.Drawing.Color.Linen;
            this.txtReportTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportTotalAmount.Location = new System.Drawing.Point(846, 0);
            this.txtReportTotalAmount.MaxLength = 15;
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.ReadOnly = true;
            this.txtReportTotalAmount.Size = new System.Drawing.Size(108, 20);
            this.txtReportTotalAmount.TabIndex = 1047;
            this.txtReportTotalAmount.TabStop = false;
            this.txtReportTotalAmount.Tag = "0.00";
            this.txtReportTotalAmount.Text = "0.00";
            this.txtReportTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlMultiSelection
            // 
            this.pnlMultiSelection.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.pnlMultiSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection.Controls.Add(this.mcbProduct);
            this.pnlMultiSelection.Controls.Add(this.lblProduct);
            this.pnlMultiSelection.Controls.Add(this.lblTo);
            this.pnlMultiSelection.Controls.Add(this.ToDate);
            this.pnlMultiSelection.Controls.Add(this.btnOKMultiSelection);
            this.pnlMultiSelection.Controls.Add(this.FromDate);
            this.pnlMultiSelection.Controls.Add(this.lblFrom);
            this.pnlMultiSelection.Location = new System.Drawing.Point(305, 195);
            this.pnlMultiSelection.Name = "pnlMultiSelection";
            this.pnlMultiSelection.Size = new System.Drawing.Size(442, 143);
            this.pnlMultiSelection.TabIndex = 1043;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(33, 53);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(22, 15);
            this.lblTo.TabIndex = 1041;
            this.lblTo.Text = "To";
            // 
            // ToDate
            // 
            this.ToDate.CustomFormat = "dd/MM/yy";
            this.ToDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(60, 46);
            this.ToDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ToDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(122, 26);
            this.ToDate.TabIndex = 1043;
            this.ToDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // btnOKMultiSelection
            // 
            this.btnOKMultiSelection.BackColor = System.Drawing.Color.Lime;
            this.btnOKMultiSelection.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOKMultiSelection.Location = new System.Drawing.Point(395, 95);
            this.btnOKMultiSelection.Name = "btnOKMultiSelection";
            this.btnOKMultiSelection.Size = new System.Drawing.Size(35, 30);
            this.btnOKMultiSelection.TabIndex = 1035;
            this.btnOKMultiSelection.Text = "GO";
            this.btnOKMultiSelection.UseVisualStyleBackColor = false;
            this.btnOKMultiSelection.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FromDate
            // 
            this.FromDate.CustomFormat = "dd/MM/yy";
            this.FromDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(60, 8);
            this.FromDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.FromDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(122, 26);
            this.FromDate.TabIndex = 1042;
            this.FromDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(16, 15);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(39, 15);
            this.lblFrom.TabIndex = 1040;
            this.lblFrom.Text = "From";
            // 
            // UclStockListOpeningStockProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclStockListOpeningStockProduct";
            this.Size = new System.Drawing.Size(976, 557);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlMultiSelection.ResumeLayout(false);
            this.pnlMultiSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblProduct;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
       
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtTotQuantity;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtReportTotalAmount;
        private System.Windows.Forms.Panel pnlMultiSelection;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.Button btnOKMultiSelection;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lblFrom;

    }
}
