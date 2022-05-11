namespace EcoMart.InterfaceLayer
{
    partial class UclToolCashCounter
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
            this.pnlDebtorProduct = new System.Windows.Forms.Panel();
            this.dgBillList = new System.Windows.Forms.DataGridView();
            this.pnlDebtorProductBottom = new System.Windows.Forms.Panel();
            this.btnRefresh = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.txtStockInProducts = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.txtNoOfProducts = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblNoofProducts = new System.Windows.Forms.Label();
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.cashCounterDate = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlDebtorProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillList)).BeginInit();
            this.pnlDebtorProductBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(946, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 517);
            this.MMBottomPanel.Size = new System.Drawing.Size(948, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.cashCounterDate);
            this.MMMainPanel.Controls.Add(this.psLabel2);
            this.MMMainPanel.Controls.Add(this.pnlDebtorProduct);
            this.MMMainPanel.Size = new System.Drawing.Size(948, 465);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlDebtorProduct, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.psLabel2, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.cashCounterDate, 0);
            // 
            // pnlDebtorProduct
            // 
            this.pnlDebtorProduct.BackColor = System.Drawing.Color.Thistle;
            this.pnlDebtorProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDebtorProduct.Controls.Add(this.dgBillList);
            this.pnlDebtorProduct.Controls.Add(this.pnlDebtorProductBottom);
            this.pnlDebtorProduct.Location = new System.Drawing.Point(-1, 68);
            this.pnlDebtorProduct.Name = "pnlDebtorProduct";
            this.pnlDebtorProduct.Size = new System.Drawing.Size(929, 326);
            this.pnlDebtorProduct.TabIndex = 1126;
            // 
            // dgBillList
            // 
            this.dgBillList.AllowUserToDeleteRows = false;
            this.dgBillList.AllowUserToResizeColumns = false;
            this.dgBillList.AllowUserToResizeRows = false;
            this.dgBillList.BackgroundColor = System.Drawing.Color.LightBlue;
            this.dgBillList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBillList.Location = new System.Drawing.Point(24, 3);
            this.dgBillList.Name = "dgBillList";
            this.dgBillList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBillList.Size = new System.Drawing.Size(861, 281);
            this.dgBillList.TabIndex = 1023;
            this.dgBillList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBillList_KeyDown);
            // 
            // pnlDebtorProductBottom
            // 
            this.pnlDebtorProductBottom.BackColor = System.Drawing.Color.Plum;
            this.pnlDebtorProductBottom.Controls.Add(this.btnRefresh);
            this.pnlDebtorProductBottom.Controls.Add(this.txtStockInProducts);
            this.pnlDebtorProductBottom.Controls.Add(this.txtNoOfProducts);
            this.pnlDebtorProductBottom.Controls.Add(this.lblNoofProducts);
            this.pnlDebtorProductBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDebtorProductBottom.Location = new System.Drawing.Point(0, 290);
            this.pnlDebtorProductBottom.Name = "pnlDebtorProductBottom";
            this.pnlDebtorProductBottom.Size = new System.Drawing.Size(927, 34);
            this.pnlDebtorProductBottom.TabIndex = 155;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(619, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(93, 28);
            this.btnRefresh.TabIndex = 1015;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtStockInProducts
            // 
            this.txtStockInProducts.BackColor = System.Drawing.Color.Snow;
            this.txtStockInProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStockInProducts.Enabled = false;
            this.txtStockInProducts.Location = new System.Drawing.Point(319, 7);
            this.txtStockInProducts.Name = "txtStockInProducts";
            this.txtStockInProducts.Size = new System.Drawing.Size(50, 22);
            this.txtStockInProducts.TabIndex = 1014;
            this.txtStockInProducts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStockInProducts.Visible = false;
            // 
            // txtNoOfProducts
            // 
            this.txtNoOfProducts.BackColor = System.Drawing.Color.Snow;
            this.txtNoOfProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfProducts.Enabled = false;
            this.txtNoOfProducts.Location = new System.Drawing.Point(114, 8);
            this.txtNoOfProducts.Name = "txtNoOfProducts";
            this.txtNoOfProducts.Size = new System.Drawing.Size(50, 22);
            this.txtNoOfProducts.TabIndex = 1012;
            this.txtNoOfProducts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfProducts.Visible = false;
            // 
            // lblNoofProducts
            // 
            this.lblNoofProducts.AutoSize = true;
            this.lblNoofProducts.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofProducts.Location = new System.Drawing.Point(21, 12);
            this.lblNoofProducts.Name = "lblNoofProducts";
            this.lblNoofProducts.Size = new System.Drawing.Size(79, 13);
            this.lblNoofProducts.TabIndex = 1011;
            this.lblNoofProducts.Text = "No Of Rows";
            this.lblNoofProducts.Visible = false;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(62, 37);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(36, 16);
            this.psLabel2.TabIndex = 1127;
            this.psLabel2.Text = "Date";
            // 
            // cashCounterDate
            // 
            this.cashCounterDate.CustomFormat = "dd/MM/yyyy";
            this.cashCounterDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cashCounterDate.Location = new System.Drawing.Point(104, 34);
            this.cashCounterDate.Name = "cashCounterDate";
            this.cashCounterDate.Size = new System.Drawing.Size(125, 24);
            this.cashCounterDate.TabIndex = 1128;
            this.cashCounterDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cashCounterDate_KeyDown);
            // 
            // timer1
            // 
            this.timer1.Interval = 6000;
            // 
            // UclToolCashCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclToolCashCounter";
            this.Size = new System.Drawing.Size(948, 563);
            this.Load += new System.EventHandler(this.UclToolCashCounter_Load);
            this.Enter += new System.EventHandler(this.UclToolCashCounter_Enter);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlDebtorProduct.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBillList)).EndInit();
            this.pnlDebtorProductBottom.ResumeLayout(false);
            this.pnlDebtorProductBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDebtorProduct;
        private System.Windows.Forms.Panel pnlDebtorProductBottom;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtStockInProducts;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfProducts;
        private System.Windows.Forms.Label lblNoofProducts;
        private CommonControls.PSLabel psLabel2;
        private System.Windows.Forms.DataGridView dgBillList;
        private CommonControls.FromDate cashCounterDate;
        private System.Windows.Forms.Timer timer1;
        private PharmaSYSPlus.CommonLibrary.PSButton btnRefresh;
    }
}
