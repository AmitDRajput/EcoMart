namespace EcoMart.Reporting.Controls
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStockListOpeningStockProduct));
            this.lblProduct = new System.Windows.Forms.Label();
            this.mcbProduct = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.txtTotQuantity = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.txtReportTotalAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.pnlMultiSelection = new System.Windows.Forms.Panel();
            this.lblTo = new System.Windows.Forms.Label();
            this.btnOKMultiSelection = new System.Windows.Forms.Button();
            this.lblFrom = new System.Windows.Forms.Label();
            this.FromDate = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.ToDate = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
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
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotQuantity, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Size = new System.Drawing.Size(976, 482);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection, 0);
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
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = null;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(319, 22);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 1036;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
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
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(974, 480);
            this.dgvReportList.TabIndex = 1032;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // txtTotQuantity
            // 
            this.txtTotQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotQuantity.Location = new System.Drawing.Point(472, 0);
            this.txtTotQuantity.Name = "txtTotQuantity";
            this.txtTotQuantity.Size = new System.Drawing.Size(86, 20);
            this.txtTotQuantity.TabIndex = 1048;
            this.txtTotQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BackColor = System.Drawing.Color.Linen;
            this.txtReportTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.pnlMultiSelection.Controls.Add(this.ToDate);
            this.pnlMultiSelection.Controls.Add(this.FromDate);
            this.pnlMultiSelection.Controls.Add(this.mcbProduct);
            this.pnlMultiSelection.Controls.Add(this.lblProduct);
            this.pnlMultiSelection.Controls.Add(this.lblTo);
            this.pnlMultiSelection.Controls.Add(this.btnOKMultiSelection);
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
            this.lblTo.Size = new System.Drawing.Size(21, 15);
            this.lblTo.TabIndex = 1041;
            this.lblTo.Text = "To";
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
            // FromDate
            // 
            this.FromDate.CustomFormat = "dd/MM/yyyy";
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(65, 12);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(125, 24);
            this.FromDate.TabIndex = 1066;
            this.FromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FromDate_KeyDown);
            // 
            // ToDate
            // 
            this.ToDate.CustomFormat = "dd/MM/yyyy";
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(65, 53);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(125, 24);
            this.ToDate.TabIndex = 1067;
            this.ToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ToDate_KeyDown);
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
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
       
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtTotQuantity;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtReportTotalAmount;
        private System.Windows.Forms.Panel pnlMultiSelection;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Button btnOKMultiSelection;
        private System.Windows.Forms.Label lblFrom;
        private InterfaceLayer.CommonControls.FromDate FromDate;
        private InterfaceLayer.CommonControls.ToDate ToDate;

    }
}
