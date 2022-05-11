namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class FormImportAlliedSaleBill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImportAlliedSaleBill));
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnReadData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblProductName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblRemainingProducts = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblTotalProducts = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblProductsRemainingToMatch = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblTotalProductsToMatch = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblPartyName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblParty = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblProduct = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.dgAlliedProducts = new System.Windows.Forms.DataGridView();
            this.pnlProductMatch = new System.Windows.Forms.Panel();
            this.btnNewProduct = new System.Windows.Forms.Button();
            this.btnProductMatch = new System.Windows.Forms.Button();
            this.dgProduct = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlPartyMatch = new System.Windows.Forms.Panel();
            this.btnNewParty = new System.Windows.Forms.Button();
            this.dgParty = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.btnMatchParty = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbPharmaSYS = new System.Windows.Forms.RadioButton();
            this.rbAMD = new System.Windows.Forms.RadioButton();
            this.rbDAVA = new System.Windows.Forms.RadioButton();
            this.rbAllied = new System.Windows.Forms.RadioButton();
            this.lblPharmaSYSPartyName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblPharmaSysParty = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.btnSkip = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAlliedProducts)).BeginInit();
            this.pnlProductMatch.SuspendLayout();
            this.pnlPartyMatch.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select File";
            // 
            // txtFileName
            // 
            this.txtFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(100, 73);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(265, 26);
            this.txtFileName.TabIndex = 1;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(369, 74);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(27, 26);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnReadData
            // 
            this.btnReadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadData.Location = new System.Drawing.Point(402, 70);
            this.btnReadData.Name = "btnReadData";
            this.btnReadData.Size = new System.Drawing.Size(83, 35);
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
            this.groupBox1.Controls.Add(this.lblPartyName);
            this.groupBox1.Controls.Add(this.lblParty);
            this.groupBox1.Controls.Add(this.lblProduct);
            this.groupBox1.Controls.Add(this.dgAlliedProducts);
            this.groupBox1.Location = new System.Drawing.Point(12, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 274);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(102, 103);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(115, 19);
            this.lblProductName.TabIndex = 13;
            this.lblProductName.Text = "Product Name";
            this.lblProductName.Visible = false;
            // 
            // lblRemainingProducts
            // 
            this.lblRemainingProducts.AutoSize = true;
            this.lblRemainingProducts.Location = new System.Drawing.Point(260, 184);
            this.lblRemainingProducts.Name = "lblRemainingProducts";
            this.lblRemainingProducts.Size = new System.Drawing.Size(18, 19);
            this.lblRemainingProducts.TabIndex = 12;
            this.lblRemainingProducts.Text = "3";
            this.lblRemainingProducts.Visible = false;
            // 
            // lblTotalProducts
            // 
            this.lblTotalProducts.AutoSize = true;
            this.lblTotalProducts.Location = new System.Drawing.Point(260, 154);
            this.lblTotalProducts.Name = "lblTotalProducts";
            this.lblTotalProducts.Size = new System.Drawing.Size(27, 19);
            this.lblTotalProducts.TabIndex = 11;
            this.lblTotalProducts.Text = "10";
            this.lblTotalProducts.Visible = false;
            // 
            // lblProductsRemainingToMatch
            // 
            this.lblProductsRemainingToMatch.AutoSize = true;
            this.lblProductsRemainingToMatch.Location = new System.Drawing.Point(20, 184);
            this.lblProductsRemainingToMatch.Name = "lblProductsRemainingToMatch";
            this.lblProductsRemainingToMatch.Size = new System.Drawing.Size(234, 19);
            this.lblProductsRemainingToMatch.TabIndex = 10;
            this.lblProductsRemainingToMatch.Text = "Products Remaining to Match:";
            this.lblProductsRemainingToMatch.Visible = false;
            // 
            // lblTotalProductsToMatch
            // 
            this.lblTotalProductsToMatch.AutoSize = true;
            this.lblTotalProductsToMatch.Location = new System.Drawing.Point(20, 154);
            this.lblTotalProductsToMatch.Name = "lblTotalProductsToMatch";
            this.lblTotalProductsToMatch.Size = new System.Drawing.Size(193, 19);
            this.lblTotalProductsToMatch.TabIndex = 9;
            this.lblTotalProductsToMatch.Text = "Total Products to Match:";
            this.lblTotalProductsToMatch.Visible = false;
            // 
            // lblPartyName
            // 
            this.lblPartyName.AutoSize = true;
            this.lblPartyName.Location = new System.Drawing.Point(102, 30);
            this.lblPartyName.Name = "lblPartyName";
            this.lblPartyName.Size = new System.Drawing.Size(95, 19);
            this.lblPartyName.TabIndex = 8;
            this.lblPartyName.Text = "Party Name";
            this.lblPartyName.Visible = false;
            // 
            // lblParty
            // 
            this.lblParty.AutoSize = true;
            this.lblParty.Location = new System.Drawing.Point(20, 30);
            this.lblParty.Name = "lblParty";
            this.lblParty.Size = new System.Drawing.Size(49, 19);
            this.lblParty.TabIndex = 1;
            this.lblParty.Text = "Party";
            this.lblParty.Visible = false;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(20, 103);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(69, 19);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product";
            this.lblProduct.Visible = false;
            // 
            // dgAlliedProducts
            // 
            this.dgAlliedProducts.AllowUserToAddRows = false;
            this.dgAlliedProducts.AllowUserToDeleteRows = false;
            this.dgAlliedProducts.AllowUserToOrderColumns = true;
            this.dgAlliedProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAlliedProducts.Location = new System.Drawing.Point(6, 220);
            this.dgAlliedProducts.Name = "dgAlliedProducts";
            this.dgAlliedProducts.Size = new System.Drawing.Size(405, 42);
            this.dgAlliedProducts.TabIndex = 7;
            this.dgAlliedProducts.Visible = false;
            // 
            // pnlProductMatch
            // 
            this.pnlProductMatch.Controls.Add(this.btnSkip);
            this.pnlProductMatch.Controls.Add(this.btnNewProduct);
            this.pnlProductMatch.Controls.Add(this.btnProductMatch);
            this.pnlProductMatch.Controls.Add(this.dgProduct);
            this.pnlProductMatch.Location = new System.Drawing.Point(501, 102);
            this.pnlProductMatch.Name = "pnlProductMatch";
            this.pnlProductMatch.Size = new System.Drawing.Size(411, 373);
            this.pnlProductMatch.TabIndex = 12;
            this.pnlProductMatch.Visible = false;
            // 
            // btnNewProduct
            // 
            this.btnNewProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewProduct.Location = new System.Drawing.Point(227, 293);
            this.btnNewProduct.Name = "btnNewProduct";
            this.btnNewProduct.Size = new System.Drawing.Size(83, 48);
            this.btnNewProduct.TabIndex = 20;
            this.btnNewProduct.Text = "New";
            this.btnNewProduct.UseVisualStyleBackColor = true;
            this.btnNewProduct.Click += new System.EventHandler(this.btnNewProduct_Click);
            // 
            // btnProductMatch
            // 
            this.btnProductMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductMatch.Location = new System.Drawing.Point(316, 293);
            this.btnProductMatch.Name = "btnProductMatch";
            this.btnProductMatch.Size = new System.Drawing.Size(83, 48);
            this.btnProductMatch.TabIndex = 19;
            this.btnProductMatch.Text = "Match";
            this.btnProductMatch.UseVisualStyleBackColor = true;
            this.btnProductMatch.Click += new System.EventHandler(this.btnProductMatch_Click);
            // 
            // dgProduct
            // 
            this.dgProduct.ApplyAlternateRowStyle = false;
            this.dgProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgProduct.BackColor = System.Drawing.Color.Transparent;
            this.dgProduct.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgProduct.ConvertDatetoMonth")));
            this.dgProduct.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgProduct.DateColumnNames")));
            this.dgProduct.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgProduct.DoubleColumnNames")));
            this.dgProduct.Location = new System.Drawing.Point(3, 3);
            this.dgProduct.Name = "dgProduct";
            this.dgProduct.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgProduct.OptionalColumnNames")));
            this.dgProduct.Size = new System.Drawing.Size(396, 284);
            this.dgProduct.TabIndex = 18;
            this.dgProduct.DoubleClicked += new System.EventHandler(this.dgProduct_DoubleClicked);
            // 
            // pnlPartyMatch
            // 
            this.pnlPartyMatch.Controls.Add(this.btnNewParty);
            this.pnlPartyMatch.Controls.Add(this.dgParty);
            this.pnlPartyMatch.Controls.Add(this.btnMatchParty);
            this.pnlPartyMatch.Location = new System.Drawing.Point(501, 102);
            this.pnlPartyMatch.Name = "pnlPartyMatch";
            this.pnlPartyMatch.Size = new System.Drawing.Size(411, 368);
            this.pnlPartyMatch.TabIndex = 11;
            this.pnlPartyMatch.Visible = false;
            // 
            // btnNewParty
            // 
            this.btnNewParty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewParty.Location = new System.Drawing.Point(227, 293);
            this.btnNewParty.Name = "btnNewParty";
            this.btnNewParty.Size = new System.Drawing.Size(83, 48);
            this.btnNewParty.TabIndex = 18;
            this.btnNewParty.Text = "New";
            this.btnNewParty.UseVisualStyleBackColor = true;
            this.btnNewParty.Click += new System.EventHandler(this.btnNewParty_Click);
            // 
            // dgParty
            // 
            this.dgParty.ApplyAlternateRowStyle = false;
            this.dgParty.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgParty.BackColor = System.Drawing.Color.Transparent;
            this.dgParty.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgParty.ConvertDatetoMonth")));
            this.dgParty.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgParty.DateColumnNames")));
            this.dgParty.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgParty.DoubleColumnNames")));
            this.dgParty.Location = new System.Drawing.Point(3, 3);
            this.dgParty.Name = "dgParty";
            this.dgParty.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgParty.OptionalColumnNames")));
            this.dgParty.Size = new System.Drawing.Size(396, 284);
            this.dgParty.TabIndex = 17;
            this.dgParty.DoubleClicked += new System.EventHandler(this.dgParty_DoubleClicked);
            // 
            // btnMatchParty
            // 
            this.btnMatchParty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMatchParty.Location = new System.Drawing.Point(316, 293);
            this.btnMatchParty.Name = "btnMatchParty";
            this.btnMatchParty.Size = new System.Drawing.Size(83, 48);
            this.btnMatchParty.TabIndex = 16;
            this.btnMatchParty.Text = "Match";
            this.btnMatchParty.UseVisualStyleBackColor = true;
            this.btnMatchParty.Click += new System.EventHandler(this.btnMatchParty_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Enabled = false;
            this.btnFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinish.Location = new System.Drawing.Point(826, 499);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(83, 48);
            this.btnFinish.TabIndex = 13;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(737, 499);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 48);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbPharmaSYS);
            this.groupBox2.Controls.Add(this.rbAMD);
            this.groupBox2.Controls.Add(this.rbDAVA);
            this.groupBox2.Controls.Add(this.rbAllied);
            this.groupBox2.Location = new System.Drawing.Point(19, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 50);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select File Format";
            // 
            // rbPharmaSYS
            // 
            this.rbPharmaSYS.AutoSize = true;
            this.rbPharmaSYS.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPharmaSYS.Location = new System.Drawing.Point(273, 18);
            this.rbPharmaSYS.Name = "rbPharmaSYS";
            this.rbPharmaSYS.Size = new System.Drawing.Size(112, 23);
            this.rbPharmaSYS.TabIndex = 3;
            this.rbPharmaSYS.TabStop = true;
            this.rbPharmaSYS.Text = "PharmaSYS";
            this.rbPharmaSYS.UseVisualStyleBackColor = true;
            // 
            // rbAMD
            // 
            this.rbAMD.AutoSize = true;
            this.rbAMD.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAMD.Location = new System.Drawing.Point(192, 18);
            this.rbAMD.Name = "rbAMD";
            this.rbAMD.Size = new System.Drawing.Size(62, 23);
            this.rbAMD.TabIndex = 2;
            this.rbAMD.TabStop = true;
            this.rbAMD.Text = "AMD";
            this.rbAMD.UseVisualStyleBackColor = true;
            // 
            // rbDAVA
            // 
            this.rbDAVA.AutoSize = true;
            this.rbDAVA.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDAVA.Location = new System.Drawing.Point(105, 20);
            this.rbDAVA.Name = "rbDAVA";
            this.rbDAVA.Size = new System.Drawing.Size(68, 23);
            this.rbDAVA.TabIndex = 1;
            this.rbDAVA.TabStop = true;
            this.rbDAVA.Text = "DAVA";
            this.rbDAVA.UseVisualStyleBackColor = true;
            // 
            // rbAllied
            // 
            this.rbAllied.AutoSize = true;
            this.rbAllied.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAllied.Location = new System.Drawing.Point(16, 19);
            this.rbAllied.Name = "rbAllied";
            this.rbAllied.Size = new System.Drawing.Size(70, 23);
            this.rbAllied.TabIndex = 0;
            this.rbAllied.TabStop = true;
            this.rbAllied.Text = "Allied";
            this.rbAllied.UseVisualStyleBackColor = true;
            this.rbAllied.CheckedChanged += new System.EventHandler(this.rbAllied_CheckedChanged);
            // 
            // lblPharmaSYSPartyName
            // 
            this.lblPharmaSYSPartyName.AutoSize = true;
            this.lblPharmaSYSPartyName.Location = new System.Drawing.Point(552, 77);
            this.lblPharmaSYSPartyName.Name = "lblPharmaSYSPartyName";
            this.lblPharmaSYSPartyName.Size = new System.Drawing.Size(153, 19);
            this.lblPharmaSYSPartyName.TabIndex = 9;
            this.lblPharmaSYSPartyName.Text = "Select Party below -";
            this.lblPharmaSYSPartyName.Visible = false;
            // 
            // lblPharmaSysParty
            // 
            this.lblPharmaSysParty.AutoSize = true;
            this.lblPharmaSysParty.Location = new System.Drawing.Point(497, 77);
            this.lblPharmaSysParty.Name = "lblPharmaSysParty";
            this.lblPharmaSysParty.Size = new System.Drawing.Size(49, 19);
            this.lblPharmaSysParty.TabIndex = 9;
            this.lblPharmaSysParty.Text = "Party";
            this.lblPharmaSysParty.Visible = false;
            // 
            // btnSkip
            // 
            this.btnSkip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSkip.Location = new System.Drawing.Point(138, 293);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(83, 48);
            this.btnSkip.TabIndex = 21;
            this.btnSkip.Text = "Skip";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // FormImportAlliedSaleBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 558);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.lblPharmaSYSPartyName);
            this.Controls.Add(this.lblPharmaSysParty);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnReadData);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlProductMatch);
            this.Controls.Add(this.pnlPartyMatch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormImportAlliedSaleBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import -> Sale Bill Allied ";
            this.Enter += new System.EventHandler(this.FormImportAlliedSaleBill_Enter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAlliedProducts)).EndInit();
            this.pnlProductMatch.ResumeLayout(false);
            this.pnlPartyMatch.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnReadData;
        private System.Windows.Forms.GroupBox groupBox1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblParty;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblProduct;
        private System.Windows.Forms.DataGridView dgAlliedProducts;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblPharmaSysParty;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblPartyName;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblPharmaSYSPartyName;
        private System.Windows.Forms.Panel pnlPartyMatch;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblRemainingProducts;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblTotalProducts;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblProductsRemainingToMatch;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblTotalProductsToMatch;
        private System.Windows.Forms.Panel pnlProductMatch;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblProductName;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMatchParty;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgParty;
        private System.Windows.Forms.Button btnProductMatch;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgProduct;
        private System.Windows.Forms.Button btnNewProduct;
        private System.Windows.Forms.Button btnNewParty;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbDAVA;
        private System.Windows.Forms.RadioButton rbAllied;
        private System.Windows.Forms.RadioButton rbPharmaSYS;
        private System.Windows.Forms.RadioButton rbAMD;
        private System.Windows.Forms.Button btnSkip;
    }
}