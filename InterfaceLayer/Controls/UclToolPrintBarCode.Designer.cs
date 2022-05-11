namespace EcoMart.InterfaceLayer
{
    partial class UclToolPrintBarCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclToolPrintBarCode));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnShelfWise = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnGivenProduct = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnPurchase = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.pnlPurchase = new System.Windows.Forms.Panel();
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.cbTransactionType = new System.Windows.Forms.ComboBox();
            this.txtVoucherNumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.lblVouNo = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouType = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnPrintLabels = new System.Windows.Forms.Button();
            this.pnlGivenProduct = new System.Windows.Forms.Panel();
            this.btnPrintLablesProduct = new System.Windows.Forms.Button();
            this.dgvBatchGrid = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.psLabel7 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbProduct = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.pnlShelf = new System.Windows.Forms.Panel();
            this.btnShelfwiseGo = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.lblShelf = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtName = new EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.pnlDebtorProduct = new System.Windows.Forms.Panel();
            this.mpMSVCFill = new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.pnlDebtorProductBottom = new System.Windows.Forms.Panel();
            this.txtStockInProducts = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblStockinProducts = new System.Windows.Forms.Label();
            this.txtNoOfProducts = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblNoofProducts = new System.Windows.Forms.Label();
            this.btnOKFill = new System.Windows.Forms.Button();
            this.btnCanelFill = new System.Windows.Forms.Button();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlPurchase.SuspendLayout();
            this.pnlGivenProduct.SuspendLayout();
            this.pnlShelf.SuspendLayout();
            this.pnlDebtorProduct.SuspendLayout();
            this.pnlDebtorProductBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(938, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 469);
            this.MMBottomPanel.Size = new System.Drawing.Size(940, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlDebtorProduct);
            this.MMMainPanel.Controls.Add(this.pnlShelf);
            this.MMMainPanel.Controls.Add(this.pnlGivenProduct);
            this.MMMainPanel.Controls.Add(this.pnlPurchase);
            this.MMMainPanel.Controls.Add(this.groupBox1);
            this.MMMainPanel.Size = new System.Drawing.Size(940, 406);
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.groupBox1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlPurchase, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlGivenProduct, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlShelf, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlDebtorProduct, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(472, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 20);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnShelfWise);
            this.groupBox1.Controls.Add(this.rbtnGivenProduct);
            this.groupBox1.Controls.Add(this.rbtnPurchase);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 125);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rbtnShelfWise
            // 
            this.rbtnShelfWise.AutoSize = true;
            this.rbtnShelfWise.BackColor = System.Drawing.Color.White;
            this.rbtnShelfWise.Location = new System.Drawing.Point(15, 93);
            this.rbtnShelfWise.Name = "rbtnShelfWise";
            this.rbtnShelfWise.Size = new System.Drawing.Size(88, 21);
            this.rbtnShelfWise.TabIndex = 2;
            this.rbtnShelfWise.TabStop = true;
            this.rbtnShelfWise.Text = "ShelfWise";
            this.rbtnShelfWise.UseVisualStyleBackColor = false;
            this.rbtnShelfWise.CheckedChanged += new System.EventHandler(this.rbtnShelfWise_CheckedChanged);
            // 
            // rbtnGivenProduct
            // 
            this.rbtnGivenProduct.AutoSize = true;
            this.rbtnGivenProduct.BackColor = System.Drawing.Color.White;
            this.rbtnGivenProduct.Location = new System.Drawing.Point(15, 58);
            this.rbtnGivenProduct.Name = "rbtnGivenProduct";
            this.rbtnGivenProduct.Size = new System.Drawing.Size(113, 21);
            this.rbtnGivenProduct.TabIndex = 1;
            this.rbtnGivenProduct.TabStop = true;
            this.rbtnGivenProduct.Text = "GivenProduct";
            this.rbtnGivenProduct.UseVisualStyleBackColor = false;
            this.rbtnGivenProduct.CheckedChanged += new System.EventHandler(this.rbtnGivenProduct_CheckedChanged);
            // 
            // rbtnPurchase
            // 
            this.rbtnPurchase.AutoSize = true;
            this.rbtnPurchase.BackColor = System.Drawing.Color.White;
            this.rbtnPurchase.Location = new System.Drawing.Point(15, 23);
            this.rbtnPurchase.Name = "rbtnPurchase";
            this.rbtnPurchase.Size = new System.Drawing.Size(84, 21);
            this.rbtnPurchase.TabIndex = 0;
            this.rbtnPurchase.TabStop = true;
            this.rbtnPurchase.Text = "Purchase";
            this.rbtnPurchase.UseVisualStyleBackColor = false;
            this.rbtnPurchase.CheckedChanged += new System.EventHandler(this.rbtnPurchase_CheckedChanged);
            // 
            // pnlPurchase
            // 
            this.pnlPurchase.BackColor = System.Drawing.Color.Cornsilk;
            this.pnlPurchase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPurchase.Controls.Add(this.btnOKMultiSelection1);
            this.pnlPurchase.Controls.Add(this.cbTransactionType);
            this.pnlPurchase.Controls.Add(this.txtVoucherNumber);
            this.pnlPurchase.Controls.Add(this.lblVouNo);
            this.pnlPurchase.Controls.Add(this.lblVouType);
            this.pnlPurchase.Location = new System.Drawing.Point(282, 5);
            this.pnlPurchase.Name = "pnlPurchase";
            this.pnlPurchase.Size = new System.Drawing.Size(360, 85);
            this.pnlPurchase.TabIndex = 2;
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(307, 1);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(49, 47);
            this.btnOKMultiSelection1.TabIndex = 1099;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection1_Click);
            // 
            // cbTransactionType
            // 
            this.cbTransactionType.AllowDrop = true;
            this.cbTransactionType.BackColor = System.Drawing.Color.White;
            this.cbTransactionType.DisplayMember = "0";
            this.cbTransactionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTransactionType.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTransactionType.FormattingEnabled = true;
            this.cbTransactionType.Location = new System.Drawing.Point(89, 9);
            this.cbTransactionType.Name = "cbTransactionType";
            this.cbTransactionType.Size = new System.Drawing.Size(190, 26);
            this.cbTransactionType.TabIndex = 1098;
            // 
            // txtVoucherNumber
            // 
            this.txtVoucherNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVoucherNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVoucherNumber.Location = new System.Drawing.Point(89, 51);
            this.txtVoucherNumber.MaxLength = 50;
            this.txtVoucherNumber.Name = "txtVoucherNumber";
            this.txtVoucherNumber.Size = new System.Drawing.Size(75, 24);
            this.txtVoucherNumber.TabIndex = 1093;
            this.txtVoucherNumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtVoucherNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVoucherNumber_KeyDown);
            // 
            // lblVouNo
            // 
            this.lblVouNo.AutoSize = true;
            this.lblVouNo.Location = new System.Drawing.Point(30, 55);
            this.lblVouNo.Name = "lblVouNo";
            this.lblVouNo.Size = new System.Drawing.Size(50, 16);
            this.lblVouNo.TabIndex = 1091;
            this.lblVouNo.Text = "Vou No";
            // 
            // lblVouType
            // 
            this.lblVouType.AutoSize = true;
            this.lblVouType.Location = new System.Drawing.Point(18, 16);
            this.lblVouType.Name = "lblVouType";
            this.lblVouType.Size = new System.Drawing.Size(63, 16);
            this.lblVouType.TabIndex = 1090;
            this.lblVouType.Text = "Vou Type";
            // 
            // btnPrintLabels
            // 
            this.btnPrintLabels.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintLabels.Location = new System.Drawing.Point(536, 2);
            this.btnPrintLabels.Name = "btnPrintLabels";
            this.btnPrintLabels.Size = new System.Drawing.Size(109, 30);
            this.btnPrintLabels.TabIndex = 1092;
            this.btnPrintLabels.Text = "Print Labels";
            this.btnPrintLabels.UseVisualStyleBackColor = true;
            this.btnPrintLabels.Click += new System.EventHandler(this.btnPrintLabels_Click);
            // 
            // pnlGivenProduct
            // 
            this.pnlGivenProduct.BackColor = System.Drawing.Color.PeachPuff;
            this.pnlGivenProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGivenProduct.Controls.Add(this.btnPrintLablesProduct);
            this.pnlGivenProduct.Controls.Add(this.dgvBatchGrid);
            this.pnlGivenProduct.Controls.Add(this.psLabel7);
            this.pnlGivenProduct.Controls.Add(this.mcbProduct);
            this.pnlGivenProduct.Location = new System.Drawing.Point(282, 2);
            this.pnlGivenProduct.Name = "pnlGivenProduct";
            this.pnlGivenProduct.Size = new System.Drawing.Size(576, 176);
            this.pnlGivenProduct.TabIndex = 3;
            // 
            // btnPrintLablesProduct
            // 
            this.btnPrintLablesProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintLablesProduct.Location = new System.Drawing.Point(445, 8);
            this.btnPrintLablesProduct.Name = "btnPrintLablesProduct";
            this.btnPrintLablesProduct.Size = new System.Drawing.Size(109, 30);
            this.btnPrintLablesProduct.TabIndex = 1126;
            this.btnPrintLablesProduct.Text = "Print Labels";
            this.btnPrintLablesProduct.UseVisualStyleBackColor = true;
            this.btnPrintLablesProduct.Visible = false;
            // 
            // dgvBatchGrid
            // 
            this.dgvBatchGrid.BackColor = System.Drawing.Color.Khaki;
            this.dgvBatchGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvBatchGrid.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvBatchGrid.DateColumnNames")));
            this.dgvBatchGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvBatchGrid.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvBatchGrid.DoubleColumnNames")));
            this.dgvBatchGrid.Filter = null;
            this.dgvBatchGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBatchGrid.Location = new System.Drawing.Point(0, 41);
            this.dgvBatchGrid.Name = "dgvBatchGrid";
            this.dgvBatchGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvBatchGrid.ShowGridFilter = false;
            this.dgvBatchGrid.Size = new System.Drawing.Size(574, 133);
            this.dgvBatchGrid.TabIndex = 1077;
            this.dgvBatchGrid.DoubleClicked += new System.EventHandler(this.dgvBatchGrid_DoubleClicked);
            this.dgvBatchGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvBatchGrid_KeyDown);
            // 
            // psLabel7
            // 
            this.psLabel7.AutoSize = true;
            this.psLabel7.Location = new System.Drawing.Point(18, 16);
            this.psLabel7.Name = "psLabel7";
            this.psLabel7.Size = new System.Drawing.Size(56, 16);
            this.psLabel7.TabIndex = 1075;
            this.psLabel7.Text = "Product";
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(95, 15);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = "";
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(347, 22);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 1076;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            this.mcbProduct.EnterKeyPressed += new System.EventHandler(this.mcbProduct_EnterKeyPressed);
            // 
            // pnlShelf
            // 
            this.pnlShelf.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.pnlShelf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlShelf.Controls.Add(this.btnShelfwiseGo);
            this.pnlShelf.Controls.Add(this.lblShelf);
            this.pnlShelf.Controls.Add(this.txtName);
            this.pnlShelf.Location = new System.Drawing.Point(282, 6);
            this.pnlShelf.Name = "pnlShelf";
            this.pnlShelf.Size = new System.Drawing.Size(291, 39);
            this.pnlShelf.TabIndex = 4;
            // 
            // btnShelfwiseGo
            // 
            this.btnShelfwiseGo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShelfwiseGo.BackgroundImage")));
            this.btnShelfwiseGo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShelfwiseGo.ForeColor = System.Drawing.Color.White;
            this.btnShelfwiseGo.Image = ((System.Drawing.Image)(resources.GetObject("btnShelfwiseGo.Image")));
            this.btnShelfwiseGo.Location = new System.Drawing.Point(230, 1);
            this.btnShelfwiseGo.Name = "btnShelfwiseGo";
            this.btnShelfwiseGo.Size = new System.Drawing.Size(49, 35);
            this.btnShelfwiseGo.TabIndex = 1100;
            this.btnShelfwiseGo.Text = "Go";
            this.btnShelfwiseGo.UseVisualStyleBackColor = true;
            this.btnShelfwiseGo.Click += new System.EventHandler(this.btnShelfwiseGo_Click);
            // 
            // lblShelf
            // 
            this.lblShelf.AutoSize = true;
            this.lblShelf.Location = new System.Drawing.Point(18, 12);
            this.lblShelf.Name = "lblShelf";
            this.lblShelf.Size = new System.Drawing.Size(38, 16);
            this.lblShelf.TabIndex = 1076;
            this.lblShelf.Text = "Shelf";
            // 
            // txtName
            // 
            this.txtName.AlphabeticalList = false;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.ColumnWidth = null;
            this.txtName.DataSource = null;
            this.txtName.DisplayColumnNo = 1;
            this.txtName.DropDownHeight = 200;
            this.txtName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(68, 8);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = false;
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(125, 22);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 1;
            this.txtName.TextMaxLenght = 32767;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            // 
            // pnlDebtorProduct
            // 
            this.pnlDebtorProduct.BackColor = System.Drawing.Color.Thistle;
            this.pnlDebtorProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDebtorProduct.Controls.Add(this.mpMSVCFill);
            this.pnlDebtorProduct.Controls.Add(this.pnlDebtorProductBottom);
            this.pnlDebtorProduct.Location = new System.Drawing.Point(282, 138);
            this.pnlDebtorProduct.Name = "pnlDebtorProduct";
            this.pnlDebtorProduct.Size = new System.Drawing.Size(651, 265);
            this.pnlDebtorProduct.TabIndex = 1125;
            // 
            // mpMSVCFill
            // 
            this.mpMSVCFill.AutoScroll = true;
            this.mpMSVCFill.BackColor = System.Drawing.Color.Thistle;
            this.mpMSVCFill.DataSource = null;
            this.mpMSVCFill.DataSourceMain = null;
            this.mpMSVCFill.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVCFill.DateColumnNames")));
            this.mpMSVCFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMSVCFill.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVCFill.DoubleColumnNames")));
            this.mpMSVCFill.EditedTempDataList = null;
            this.mpMSVCFill.Filter = null;
            this.mpMSVCFill.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMSVCFill.IsAllowDelete = true;
            this.mpMSVCFill.IsAllowNewRow = false;
            this.mpMSVCFill.Location = new System.Drawing.Point(0, 0);
            this.mpMSVCFill.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpMSVCFill.MinimumSize = new System.Drawing.Size(390, 200);
            this.mpMSVCFill.Name = "mpMSVCFill";
            this.mpMSVCFill.NextRowColumn = 0;
            this.mpMSVCFill.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVCFill.NumericColumnNames")));
            this.mpMSVCFill.Size = new System.Drawing.Size(649, 226);
            this.mpMSVCFill.SubGridWidth = 380;
            this.mpMSVCFill.TabIndex = 156;
            this.mpMSVCFill.ViewControl = null;
            // 
            // pnlDebtorProductBottom
            // 
            this.pnlDebtorProductBottom.BackColor = System.Drawing.Color.Plum;
            this.pnlDebtorProductBottom.Controls.Add(this.txtStockInProducts);
            this.pnlDebtorProductBottom.Controls.Add(this.lblStockinProducts);
            this.pnlDebtorProductBottom.Controls.Add(this.txtNoOfProducts);
            this.pnlDebtorProductBottom.Controls.Add(this.btnPrintLabels);
            this.pnlDebtorProductBottom.Controls.Add(this.lblNoofProducts);
            this.pnlDebtorProductBottom.Controls.Add(this.btnOKFill);
            this.pnlDebtorProductBottom.Controls.Add(this.btnCanelFill);
            this.pnlDebtorProductBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDebtorProductBottom.Location = new System.Drawing.Point(0, 226);
            this.pnlDebtorProductBottom.Name = "pnlDebtorProductBottom";
            this.pnlDebtorProductBottom.Size = new System.Drawing.Size(649, 37);
            this.pnlDebtorProductBottom.TabIndex = 155;
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
            // lblStockinProducts
            // 
            this.lblStockinProducts.AutoSize = true;
            this.lblStockinProducts.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockinProducts.Location = new System.Drawing.Point(179, 12);
            this.lblStockinProducts.Name = "lblStockinProducts";
            this.lblStockinProducts.Size = new System.Drawing.Size(122, 13);
            this.lblStockinProducts.TabIndex = 1013;
            this.lblStockinProducts.Text = "Number Of Labels";
            this.lblStockinProducts.Visible = false;
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
            this.lblNoofProducts.Location = new System.Drawing.Point(6, 12);
            this.lblNoofProducts.Name = "lblNoofProducts";
            this.lblNoofProducts.Size = new System.Drawing.Size(103, 13);
            this.lblNoofProducts.TabIndex = 1011;
            this.lblNoofProducts.Text = "No Of Products";
            this.lblNoofProducts.Visible = false;
            // 
            // btnOKFill
            // 
            this.btnOKFill.Location = new System.Drawing.Point(375, 9);
            this.btnOKFill.Name = "btnOKFill";
            this.btnOKFill.Size = new System.Drawing.Size(63, 21);
            this.btnOKFill.TabIndex = 151;
            this.btnOKFill.Text = "&OK";
            this.btnOKFill.UseVisualStyleBackColor = true;
            this.btnOKFill.Visible = false;
            // 
            // btnCanelFill
            // 
            this.btnCanelFill.Location = new System.Drawing.Point(444, 8);
            this.btnCanelFill.Name = "btnCanelFill";
            this.btnCanelFill.Size = new System.Drawing.Size(63, 21);
            this.btnCanelFill.TabIndex = 152;
            this.btnCanelFill.Text = "&Cancel";
            this.btnCanelFill.UseVisualStyleBackColor = true;
            this.btnCanelFill.Visible = false;
            // 
            // UclToolPrintBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclToolPrintBarCode";
            this.Size = new System.Drawing.Size(940, 532);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlPurchase.ResumeLayout(false);
            this.pnlPurchase.PerformLayout();
            this.pnlGivenProduct.ResumeLayout(false);
            this.pnlGivenProduct.PerformLayout();
            this.pnlShelf.ResumeLayout(false);
            this.pnlShelf.PerformLayout();
            this.pnlDebtorProduct.ResumeLayout(false);
            this.pnlDebtorProductBottom.ResumeLayout(false);
            this.pnlDebtorProductBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private CommonControls.PSRadioButton rbtnShelfWise;
        private CommonControls.PSRadioButton rbtnGivenProduct;
        private CommonControls.PSRadioButton rbtnPurchase;
        private System.Windows.Forms.Panel pnlPurchase;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVoucherNumber;
        private System.Windows.Forms.Button btnPrintLabels;
        private CommonControls.PSLabel lblVouNo;
        private CommonControls.PSLabel lblVouType;
        private System.Windows.Forms.Panel pnlGivenProduct;
        private System.Windows.Forms.Panel pnlShelf;
        private System.Windows.Forms.ComboBox cbTransactionType;
        private CommonControls.PSLabel psLabel7;
        private CommonControls.PSComboBoxNew mcbProduct;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvBatchGrid;
        private System.Windows.Forms.Panel pnlDebtorProduct;
        private CommonControls.PSMainSubViewControl mpMSVCFill;
        private System.Windows.Forms.Panel pnlDebtorProductBottom;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtStockInProducts;
        private System.Windows.Forms.Label lblStockinProducts;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfProducts;
        private System.Windows.Forms.Label lblNoofProducts;
        private System.Windows.Forms.Button btnOKFill;
        private System.Windows.Forms.Button btnCanelFill;
        private CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private CommonControls.PSLabel lblShelf;
        private CommonControls.PSAutoSuggestTextBox txtName;
        private System.Windows.Forms.Button btnPrintLablesProduct;
        private CommonControls.PSbtnOKMultiSelection btnShelfwiseGo;
    }
}
