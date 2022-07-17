namespace EcoMart.InterfaceLayer
{
    partial class FormImportSaleBill
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImportSaleBill));
            this.btnReadData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblProductName = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblRemainingProducts = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblTotalProducts = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblProductsRemainingToMatch = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblTotalProductsToMatch = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblProduct = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.dgImpotProducts = new System.Windows.Forms.DataGridView();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.pnlProductMatch = new System.Windows.Forms.Panel();
            this.dgProduct = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnNewProduct = new System.Windows.Forms.Button();
            this.btnProductMatch = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.psCombofile = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mcbCreditor = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.lblParty = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.mcbProduct = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.lblSelectProduct = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgImpotProducts)).BeginInit();
            this.pnlProductMatch.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReadData
            // 
            this.btnReadData.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadData.Location = new System.Drawing.Point(565, 75);
            this.btnReadData.Name = "btnReadData";
            this.btnReadData.Size = new System.Drawing.Size(83, 25);
            this.btnReadData.TabIndex = 3;
            this.btnReadData.Text = "Read Data";
            this.btnReadData.UseVisualStyleBackColor = true;
            this.btnReadData.Click += new System.EventHandler(this.btnReadData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblProductName);
            this.groupBox1.Controls.Add(this.lblRemainingProducts);
            this.groupBox1.Controls.Add(this.lblTotalProducts);
            this.groupBox1.Controls.Add(this.lblProductsRemainingToMatch);
            this.groupBox1.Controls.Add(this.lblTotalProductsToMatch);
            this.groupBox1.Controls.Add(this.lblProduct);
            this.groupBox1.Location = new System.Drawing.Point(680, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 85);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(102, 65);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(95, 16);
            this.lblProductName.TabIndex = 13;
            this.lblProductName.Text = "Product Name";
            this.lblProductName.Visible = false;
            // 
            // lblRemainingProducts
            // 
            this.lblRemainingProducts.AutoSize = true;
            this.lblRemainingProducts.Location = new System.Drawing.Point(260, 36);
            this.lblRemainingProducts.Name = "lblRemainingProducts";
            this.lblRemainingProducts.Size = new System.Drawing.Size(16, 16);
            this.lblRemainingProducts.TabIndex = 12;
            this.lblRemainingProducts.Text = "0";
            this.lblRemainingProducts.Visible = false;
            // 
            // lblTotalProducts
            // 
            this.lblTotalProducts.AutoSize = true;
            this.lblTotalProducts.Location = new System.Drawing.Point(260, 17);
            this.lblTotalProducts.Name = "lblTotalProducts";
            this.lblTotalProducts.Size = new System.Drawing.Size(16, 16);
            this.lblTotalProducts.TabIndex = 11;
            this.lblTotalProducts.Text = "0";
            this.lblTotalProducts.Visible = false;
            // 
            // lblProductsRemainingToMatch
            // 
            this.lblProductsRemainingToMatch.AutoSize = true;
            this.lblProductsRemainingToMatch.Location = new System.Drawing.Point(20, 36);
            this.lblProductsRemainingToMatch.Name = "lblProductsRemainingToMatch";
            this.lblProductsRemainingToMatch.Size = new System.Drawing.Size(190, 16);
            this.lblProductsRemainingToMatch.TabIndex = 10;
            this.lblProductsRemainingToMatch.Text = "Products Remaining to Match:";
            this.lblProductsRemainingToMatch.Visible = false;
            // 
            // lblTotalProductsToMatch
            // 
            this.lblTotalProductsToMatch.AutoSize = true;
            this.lblTotalProductsToMatch.Location = new System.Drawing.Point(20, 17);
            this.lblTotalProductsToMatch.Name = "lblTotalProductsToMatch";
            this.lblTotalProductsToMatch.Size = new System.Drawing.Size(154, 16);
            this.lblTotalProductsToMatch.TabIndex = 9;
            this.lblTotalProductsToMatch.Text = "Total Products to Match:";
            this.lblTotalProductsToMatch.Visible = false;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(20, 65);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(56, 16);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product";
            this.lblProduct.Visible = false;
            // 
            // dgImpotProducts
            // 
            this.dgImpotProducts.AllowUserToAddRows = false;
            this.dgImpotProducts.AllowUserToDeleteRows = false;
            this.dgImpotProducts.AllowUserToOrderColumns = true;
            this.dgImpotProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgImpotProducts.Location = new System.Drawing.Point(6, 15);
            this.dgImpotProducts.Name = "dgImpotProducts";
            this.dgImpotProducts.Size = new System.Drawing.Size(652, 358);
            this.dgImpotProducts.TabIndex = 7;
            // 
            // btnFinish
            // 
            this.btnFinish.Enabled = false;
            this.btnFinish.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold);
            this.btnFinish.Location = new System.Drawing.Point(548, 514);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(83, 31);
            this.btnFinish.TabIndex = 17;
            this.btnFinish.Text = "&Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(646, 514);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 31);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbFormat
            // 
            this.cmbFormat.AllowDrop = true;
            this.cmbFormat.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Items.AddRange(new object[] {
            "EcoMart",
            "CIPLA",
            "DR REDDY"});
            this.cmbFormat.Location = new System.Drawing.Point(96, 74);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(454, 25);
            this.cmbFormat.TabIndex = 7;
            this.cmbFormat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFormat_KeyDown);
            this.cmbFormat.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbFormat_MouseClick);
            this.cmbFormat.MouseHover += new System.EventHandler(this.cmbFormat_MouseHover);
            // 
            // pnlProductMatch
            // 
            this.pnlProductMatch.Controls.Add(this.dgProduct);
            this.pnlProductMatch.Location = new System.Drawing.Point(680, 130);
            this.pnlProductMatch.Name = "pnlProductMatch";
            this.pnlProductMatch.Size = new System.Drawing.Size(426, 369);
            this.pnlProductMatch.TabIndex = 12;
            this.pnlProductMatch.Visible = false;
            // 
            // dgProduct
            // 
            this.dgProduct.ApplyAlternateRowStyle = false;
            this.dgProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgProduct.BackColor = System.Drawing.Color.Transparent;
            this.dgProduct.ConvertDatetoMonth = null;
            this.dgProduct.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgProduct.DateColumnNames")));
            this.dgProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgProduct.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgProduct.DoubleColumnNames")));
            this.dgProduct.FreezeLastRow = false;
            this.dgProduct.Location = new System.Drawing.Point(0, 0);
            this.dgProduct.Name = "dgProduct";
            this.dgProduct.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgProduct.NumericColumnNames")));
            this.dgProduct.OptionalColumnNames = null;
            this.dgProduct.Size = new System.Drawing.Size(426, 369);
            this.dgProduct.TabIndex = 18;
            this.dgProduct.DoubleClicked += new System.EventHandler(this.dgProduct_DoubleClicked);
            // 
            // btnSkip
            // 
            this.btnSkip.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold);
            this.btnSkip.Location = new System.Drawing.Point(449, 514);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(83, 31);
            this.btnSkip.TabIndex = 13;
            this.btnSkip.Text = "&Skip";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // btnNewProduct
            // 
            this.btnNewProduct.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewProduct.Location = new System.Drawing.Point(253, 514);
            this.btnNewProduct.Name = "btnNewProduct";
            this.btnNewProduct.Size = new System.Drawing.Size(83, 31);
            this.btnNewProduct.TabIndex = 14;
            this.btnNewProduct.Text = "&New";
            this.btnNewProduct.UseVisualStyleBackColor = true;
            this.btnNewProduct.Click += new System.EventHandler(this.btnNewProduct_Click);
            // 
            // btnProductMatch
            // 
            this.btnProductMatch.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold);
            this.btnProductMatch.Location = new System.Drawing.Point(351, 514);
            this.btnProductMatch.Name = "btnProductMatch";
            this.btnProductMatch.Size = new System.Drawing.Size(83, 31);
            this.btnProductMatch.TabIndex = 15;
            this.btnProductMatch.Text = "&Match";
            this.btnProductMatch.UseVisualStyleBackColor = true;
            this.btnProductMatch.Click += new System.EventHandler(this.btnProductMatch_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.psCombofile);
            this.groupBox3.Controls.Add(this.cmbFormat);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.mcbCreditor);
            this.groupBox3.Controls.Add(this.lblParty);
            this.groupBox3.Controls.Add(this.btnReadData);
            this.groupBox3.Location = new System.Drawing.Point(10, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(664, 108);
            this.groupBox3.TabIndex = 1092;
            this.groupBox3.TabStop = false;
            // 
            // psCombofile
            // 
            this.psCombofile.ColumnWidth = null;
            this.psCombofile.DataSource = null;
            this.psCombofile.DisplayColumnNo = 1;
            this.psCombofile.DropDownHeight = 200;
            this.psCombofile.Location = new System.Drawing.Point(96, 18);
            this.psCombofile.Margin = new System.Windows.Forms.Padding(4);
            this.psCombofile.Name = "psCombofile";
            this.psCombofile.SelectedID = "";
            this.psCombofile.SelectedIDtest = 0;
            this.psCombofile.SelectedIntID = 0;
            this.psCombofile.ShowNew = false;
            this.psCombofile.Size = new System.Drawing.Size(552, 22);
            this.psCombofile.SourceDataString = null;
            this.psCombofile.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.psCombofile.TabIndex = 1099;
            this.psCombofile.UserControlToShow = null;
            this.psCombofile.ValueColumnNo = 0;
            this.psCombofile.EnterKeyPressed += new System.EventHandler(this.psCombofile_EnterKeyPressed);
            this.psCombofile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.psCombofile_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 1098;
            this.label5.Text = "File Format";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 1097;
            this.label4.Text = "Select File";
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(96, 45);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = "";
            this.mcbCreditor.SelectedIDtest = 0;
            this.mcbCreditor.SelectedIntID = 0;
            this.mcbCreditor.ShowNew = false;
            this.mcbCreditor.Size = new System.Drawing.Size(552, 22);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 1089;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.SeletectIndexChanged += new System.EventHandler(this.mcbCreditor_SeletectIndexChanged);
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            this.mcbCreditor.UpArrowPressed += new System.EventHandler(this.mcbCreditor_UpArrowPressed);
            this.mcbCreditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcbCreditor_KeyDown);
            // 
            // lblParty
            // 
            this.lblParty.AutoSize = true;
            this.lblParty.Location = new System.Drawing.Point(39, 47);
            this.lblParty.Name = "lblParty";
            this.lblParty.Size = new System.Drawing.Size(41, 16);
            this.lblParty.TabIndex = 1;
            this.lblParty.Text = "Party";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgImpotProducts);
            this.groupBox4.Location = new System.Drawing.Point(10, 120);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(664, 379);
            this.groupBox4.TabIndex = 1093;
            this.groupBox4.TabStop = false;
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(783, 98);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = "";
            this.mcbProduct.SelectedIDtest = 0;
            this.mcbProduct.SelectedIntID = 0;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(324, 22);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 1091;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            this.mcbProduct.EnterKeyPressed += new System.EventHandler(this.mcbProduct_EnterKeyPressed);
            // 
            // lblSelectProduct
            // 
            this.lblSelectProduct.AutoSize = true;
            this.lblSelectProduct.Location = new System.Drawing.Point(683, 101);
            this.lblSelectProduct.Name = "lblSelectProduct";
            this.lblSelectProduct.Size = new System.Drawing.Size(95, 16);
            this.lblSelectProduct.TabIndex = 1090;
            this.lblSelectProduct.Text = "Select Product";
            // 
            // FormImportSaleBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 554);
            this.ControlBox = false;
            this.Controls.Add(this.btnProductMatch);
            this.Controls.Add(this.btnNewProduct);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.mcbProduct);
            this.Controls.Add(this.lblSelectProduct);
            this.Controls.Add(this.pnlProductMatch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Cambria", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormImportSaleBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import -> Import Sale Bills";
            this.Load += new System.EventHandler(this.FormImportSaleBill_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgImpotProducts)).EndInit();
            this.pnlProductMatch.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnReadData;
        private System.Windows.Forms.GroupBox groupBox1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblParty;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblProduct;
        private System.Windows.Forms.DataGridView dgImpotProducts;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblRemainingProducts;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblTotalProducts;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblProductsRemainingToMatch;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblTotalProductsToMatch;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblProductName;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnCancel;
        private CommonControls.PSComboBoxNew mcbCreditor;
        private System.Windows.Forms.Panel pnlProductMatch;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnNewProduct;
        private System.Windows.Forms.Button btnProductMatch;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgProduct;
        private CommonControls.PSComboBoxNew mcbProduct;
        private CommonControls.PSLabel lblSelectProduct;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private CommonControls.PSComboBoxNew psCombofile;
    }
}