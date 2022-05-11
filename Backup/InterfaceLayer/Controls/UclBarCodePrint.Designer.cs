namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclBarCodePrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclBarCodePrint));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnProduct = new System.Windows.Forms.Button();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.pnlProduct = new System.Windows.Forms.Panel();
            this.btnPrintLableProduct = new System.Windows.Forms.Button();
            this.dgvBatchGrid = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.txtcompany = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.txtpack = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.txtloosepack = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.mPlbl4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mcbProduct = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.pnlOld = new System.Windows.Forms.Panel();
            this.txtQuantity = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtSaleRate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtPurchaseRate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtMrp = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtexpiry = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.txtbatchno = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.mPlbl20 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl18 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl16 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl14 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl12 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl7 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.btnBarCodeNumber = new System.Windows.Forms.Button();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlProduct.SuspendLayout();
            this.pnlOld.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(958, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 543);
            this.MMBottomPanel.Size = new System.Drawing.Size(960, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlProduct);
            this.MMMainPanel.Controls.Add(this.panel1);
            this.MMMainPanel.Size = new System.Drawing.Size(960, 491);
            this.MMMainPanel.Controls.SetChildIndex(this.panel1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlProduct, 0);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnProduct);
            this.panel1.Controls.Add(this.btnPurchase);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 138);
            this.panel1.TabIndex = 1;
            // 
            // btnProduct
            // 
            this.btnProduct.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProduct.Location = new System.Drawing.Point(6, 89);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(155, 33);
            this.btnProduct.TabIndex = 1;
            this.btnProduct.Text = "Product";
            this.btnProduct.UseVisualStyleBackColor = true;
            // 
            // btnPurchase
            // 
            this.btnPurchase.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPurchase.Location = new System.Drawing.Point(6, 30);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(155, 33);
            this.btnPurchase.TabIndex = 0;
            this.btnPurchase.Text = "Purchase";
            this.btnPurchase.UseVisualStyleBackColor = true;
            // 
            // pnlProduct
            // 
            this.pnlProduct.Controls.Add(this.btnBarCodeNumber);
            this.pnlProduct.Controls.Add(this.btnPrintLableProduct);
            this.pnlProduct.Controls.Add(this.dgvBatchGrid);
            this.pnlProduct.Controls.Add(this.txtcompany);
            this.pnlProduct.Controls.Add(this.txtpack);
            this.pnlProduct.Controls.Add(this.txtloosepack);
            this.pnlProduct.Controls.Add(this.mPlbl4);
            this.pnlProduct.Controls.Add(this.mPlbl3);
            this.pnlProduct.Controls.Add(this.mPlbl2);
            this.pnlProduct.Controls.Add(this.mPlbl1);
            this.pnlProduct.Controls.Add(this.mcbProduct);
            this.pnlProduct.Controls.Add(this.pnlOld);
            this.pnlProduct.Location = new System.Drawing.Point(188, 6);
            this.pnlProduct.Name = "pnlProduct";
            this.pnlProduct.Size = new System.Drawing.Size(740, 480);
            this.pnlProduct.TabIndex = 1113;
            // 
            // btnPrintLableProduct
            // 
            this.btnPrintLableProduct.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintLableProduct.Location = new System.Drawing.Point(531, 334);
            this.btnPrintLableProduct.Name = "btnPrintLableProduct";
            this.btnPrintLableProduct.Size = new System.Drawing.Size(151, 61);
            this.btnPrintLableProduct.TabIndex = 1122;
            this.btnPrintLableProduct.Text = "Print";
            this.btnPrintLableProduct.UseVisualStyleBackColor = true;
            // 
            // dgvBatchGrid
            // 
            this.dgvBatchGrid.BackColor = System.Drawing.Color.Khaki;
            this.dgvBatchGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvBatchGrid.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvBatchGrid.DateColumnNames")));
            this.dgvBatchGrid.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvBatchGrid.DoubleColumnNames")));
            this.dgvBatchGrid.Filter = null;
            this.dgvBatchGrid.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBatchGrid.Location = new System.Drawing.Point(73, 109);
            this.dgvBatchGrid.Name = "dgvBatchGrid";
            this.dgvBatchGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvBatchGrid.ShowGridFilter = false;
            this.dgvBatchGrid.Size = new System.Drawing.Size(526, 87);
            this.dgvBatchGrid.TabIndex = 1114;
            this.dgvBatchGrid.DoubleClicked += new System.EventHandler(this.dgvBatchGrid_DoubleClicked);
            this.dgvBatchGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvBatchGrid_KeyDown);
            // 
            // txtcompany
            // 
            this.txtcompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcompany.Location = new System.Drawing.Point(387, 80);
            this.txtcompany.Name = "txtcompany";
            this.txtcompany.Size = new System.Drawing.Size(96, 23);
            this.txtcompany.TabIndex = 1121;
            this.txtcompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtpack
            // 
            this.txtpack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpack.Location = new System.Drawing.Point(277, 80);
            this.txtpack.Name = "txtpack";
            this.txtpack.Size = new System.Drawing.Size(96, 23);
            this.txtpack.TabIndex = 1120;
            this.txtpack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtloosepack
            // 
            this.txtloosepack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtloosepack.Location = new System.Drawing.Point(188, 80);
            this.txtloosepack.Name = "txtloosepack";
            this.txtloosepack.Size = new System.Drawing.Size(76, 23);
            this.txtloosepack.TabIndex = 1119;
            this.txtloosepack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(393, 59);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(78, 19);
            this.mPlbl4.TabIndex = 1118;
            this.mPlbl4.Text = "Company";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(294, 59);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(45, 19);
            this.mPlbl3.TabIndex = 1117;
            this.mPlbl3.Text = "Pack";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(199, 59);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(45, 19);
            this.mPlbl2.TabIndex = 1116;
            this.mPlbl2.Text = "UOM";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(64, 34);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(115, 19);
            this.mPlbl1.TabIndex = 1115;
            this.mPlbl1.Text = "Product Name";
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(188, 33);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = null;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(411, 26);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 1113;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            this.mcbProduct.SeletectIndexChanged += new System.EventHandler(this.mcbProduct_SeletectIndexChanged);
            // 
            // pnlOld
            // 
            this.pnlOld.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOld.Controls.Add(this.txtQuantity);
            this.pnlOld.Controls.Add(this.txtSaleRate);
            this.pnlOld.Controls.Add(this.txtPurchaseRate);
            this.pnlOld.Controls.Add(this.txtMrp);
            this.pnlOld.Controls.Add(this.txtexpiry);
            this.pnlOld.Controls.Add(this.txtbatchno);
            this.pnlOld.Controls.Add(this.mPlbl20);
            this.pnlOld.Controls.Add(this.mPlbl18);
            this.pnlOld.Controls.Add(this.mPlbl16);
            this.pnlOld.Controls.Add(this.mPlbl14);
            this.pnlOld.Controls.Add(this.mPlbl12);
            this.pnlOld.Controls.Add(this.mPlbl7);
            this.pnlOld.Controls.Add(this.mPlbl5);
            this.pnlOld.Location = new System.Drawing.Point(73, 209);
            this.pnlOld.Name = "pnlOld";
            this.pnlOld.Size = new System.Drawing.Size(381, 241);
            this.pnlOld.TabIndex = 1106;
            // 
            // txtQuantity
            // 
            this.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuantity.CausesValidation = false;
            this.txtQuantity.Location = new System.Drawing.Point(252, 199);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(100, 30);
            this.txtQuantity.TabIndex = 125;
            this.txtQuantity.Text = "0";
            this.txtQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSaleRate
            // 
            this.txtSaleRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaleRate.CausesValidation = false;
            this.txtSaleRate.Location = new System.Drawing.Point(152, 165);
            this.txtSaleRate.Name = "txtSaleRate";
            this.txtSaleRate.Size = new System.Drawing.Size(100, 26);
            this.txtSaleRate.TabIndex = 124;
            this.txtSaleRate.Text = "0.00";
            this.txtSaleRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPurchaseRate
            // 
            this.txtPurchaseRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurchaseRate.CausesValidation = false;
            this.txtPurchaseRate.Location = new System.Drawing.Point(152, 138);
            this.txtPurchaseRate.Name = "txtPurchaseRate";
            this.txtPurchaseRate.Size = new System.Drawing.Size(100, 26);
            this.txtPurchaseRate.TabIndex = 123;
            this.txtPurchaseRate.Text = "0.00";
            this.txtPurchaseRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMrp
            // 
            this.txtMrp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMrp.CausesValidation = false;
            this.txtMrp.Location = new System.Drawing.Point(152, 111);
            this.txtMrp.Name = "txtMrp";
            this.txtMrp.Size = new System.Drawing.Size(100, 26);
            this.txtMrp.TabIndex = 122;
            this.txtMrp.Text = "0.00";
            this.txtMrp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtexpiry
            // 
            this.txtexpiry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtexpiry.Location = new System.Drawing.Point(152, 84);
            this.txtexpiry.Name = "txtexpiry";
            this.txtexpiry.Size = new System.Drawing.Size(100, 26);
            this.txtexpiry.TabIndex = 121;
            this.txtexpiry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbatchno
            // 
            this.txtbatchno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbatchno.Location = new System.Drawing.Point(152, 60);
            this.txtbatchno.Name = "txtbatchno";
            this.txtbatchno.Size = new System.Drawing.Size(169, 23);
            this.txtbatchno.TabIndex = 120;
            this.txtbatchno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mPlbl20
            // 
            this.mPlbl20.AutoSize = true;
            this.mPlbl20.Location = new System.Drawing.Point(114, 12);
            this.mPlbl20.Name = "mPlbl20";
            this.mPlbl20.Size = new System.Drawing.Size(122, 19);
            this.mPlbl20.TabIndex = 119;
            this.mPlbl20.Text = "Existing Details";
            // 
            // mPlbl18
            // 
            this.mPlbl18.AutoSize = true;
            this.mPlbl18.Location = new System.Drawing.Point(99, 210);
            this.mPlbl18.Name = "mPlbl18";
            this.mPlbl18.Size = new System.Drawing.Size(137, 19);
            this.mPlbl18.TabIndex = 118;
            this.mPlbl18.Text = "Number of Labels";
            // 
            // mPlbl16
            // 
            this.mPlbl16.AutoSize = true;
            this.mPlbl16.Location = new System.Drawing.Point(41, 166);
            this.mPlbl16.Name = "mPlbl16";
            this.mPlbl16.Size = new System.Drawing.Size(96, 19);
            this.mPlbl16.TabIndex = 117;
            this.mPlbl16.Text = "Selling Rate";
            // 
            // mPlbl14
            // 
            this.mPlbl14.AutoSize = true;
            this.mPlbl14.Location = new System.Drawing.Point(21, 138);
            this.mPlbl14.Name = "mPlbl14";
            this.mPlbl14.Size = new System.Drawing.Size(116, 19);
            this.mPlbl14.TabIndex = 116;
            this.mPlbl14.Text = "Purchase Rate";
            // 
            // mPlbl12
            // 
            this.mPlbl12.AutoSize = true;
            this.mPlbl12.Location = new System.Drawing.Point(93, 111);
            this.mPlbl12.Name = "mPlbl12";
            this.mPlbl12.Size = new System.Drawing.Size(44, 19);
            this.mPlbl12.TabIndex = 115;
            this.mPlbl12.Text = "MRP";
            // 
            // mPlbl7
            // 
            this.mPlbl7.AutoSize = true;
            this.mPlbl7.Location = new System.Drawing.Point(81, 85);
            this.mPlbl7.Name = "mPlbl7";
            this.mPlbl7.Size = new System.Drawing.Size(56, 19);
            this.mPlbl7.TabIndex = 114;
            this.mPlbl7.Text = "Expiry";
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(22, 59);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(115, 19);
            this.mPlbl5.TabIndex = 113;
            this.mPlbl5.Text = "Batch Number";
            // 
            // btnBarCodeNumber
            // 
            this.btnBarCodeNumber.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarCodeNumber.Location = new System.Drawing.Point(479, 250);
            this.btnBarCodeNumber.Name = "btnBarCodeNumber";
            this.btnBarCodeNumber.Size = new System.Drawing.Size(241, 33);
            this.btnBarCodeNumber.TabIndex = 1123;
            this.btnBarCodeNumber.Text = "BarCodeNumber";
            this.btnBarCodeNumber.UseVisualStyleBackColor = true;
            // 
            // UclBarCodePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclBarCodePrint";
            this.Size = new System.Drawing.Size(960, 566);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlProduct.ResumeLayout(false);
            this.pnlProduct.PerformLayout();
            this.pnlOld.ResumeLayout(false);
            this.pnlOld.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Panel pnlProduct;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvBatchGrid;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft txtcompany;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft txtpack;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft txtloosepack;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
        private System.Windows.Forms.Panel pnlOld;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtQuantity;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtSaleRate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtPurchaseRate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtMrp;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft txtexpiry;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft txtbatchno;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl20;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl18;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl16;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl14;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl12;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl7;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private System.Windows.Forms.Button btnPrintLableProduct;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.Button btnBarCodeNumber;
    }
}
