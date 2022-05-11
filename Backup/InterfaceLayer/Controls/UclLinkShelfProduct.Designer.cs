namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclLinkShelfProduct
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
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlDrugGrouping = new System.Windows.Forms.Panel();
            this.mPlbl3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mcbShelf = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mcbProduct = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlDrugGrouping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(934, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.label6);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 581);
            this.MMBottomPanel.Size = new System.Drawing.Size(936, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.label6, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlDrugGrouping);
            this.MMMainPanel.Size = new System.Drawing.Size(936, 529);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlDrugGrouping, 0);
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(740, -1);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 22);
            this.txtNoOfRows.TabIndex = 1013;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(651, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 15);
            this.label6.TabIndex = 1012;
            this.label6.Text = "No Of Rows";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 5);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(2, 16);
            this.lblMessage.TabIndex = 1014;
            // 
            // pnlDrugGrouping
            // 
            this.pnlDrugGrouping.BackColor = System.Drawing.Color.LightGray;
            this.pnlDrugGrouping.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDrugGrouping.Controls.Add(this.mPlbl3);
            this.pnlDrugGrouping.Controls.Add(this.mcbShelf);
            this.pnlDrugGrouping.Controls.Add(this.mPlbl2);
            this.pnlDrugGrouping.Controls.Add(this.mcbProduct);
            this.pnlDrugGrouping.Controls.Add(this.dgvProduct);
            this.pnlDrugGrouping.Controls.Add(this.btnViewAll);
            this.pnlDrugGrouping.Controls.Add(this.btnAdd);
            this.pnlDrugGrouping.Location = new System.Drawing.Point(188, 123);
            this.pnlDrugGrouping.Name = "pnlDrugGrouping";
            this.pnlDrugGrouping.Size = new System.Drawing.Size(664, 384);
            this.pnlDrugGrouping.TabIndex = 150;
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(66, 19);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(45, 19);
            this.mPlbl3.TabIndex = 151;
            this.mPlbl3.Text = "&Shelf";
            // 
            // mcbShelf
            // 
            this.mcbShelf.ColumnWidth = null;
            this.mcbShelf.DataSource = null;
            this.mcbShelf.DisplayColumnNo = 1;
            this.mcbShelf.DropDownHeight = 200;
            this.mcbShelf.Location = new System.Drawing.Point(123, 16);
            this.mcbShelf.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbShelf.Name = "mcbShelf";
            this.mcbShelf.SelectedID = null;
            this.mcbShelf.ShowNew = false;
            this.mcbShelf.Size = new System.Drawing.Size(363, 26);
            this.mcbShelf.SourceDataString = null;
            this.mcbShelf.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbShelf.TabIndex = 150;
            this.mcbShelf.UserControlToShow = null;
            this.mcbShelf.ValueColumnNo = 0;
            this.mcbShelf.SeletectIndexChanged += new System.EventHandler(this.mcbShelf_SeletectIndexChanged);
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(54, 49);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(69, 19);
            this.mPlbl2.TabIndex = 54;
            this.mPlbl2.Text = "&Product";
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(123, 47);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = null;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(363, 26);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 1;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            this.mcbProduct.EnterKeyPressed += new System.EventHandler(this.mcbProduct_EnterKeyPressed);
            this.mcbProduct.SeletectIndexChanged += new System.EventHandler(this.mcbProduct_SeletectIndexChanged);
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProduct.CausesValidation = false;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvProduct.Enabled = false;
            this.dgvProduct.Location = new System.Drawing.Point(-1, 76);
            this.dgvProduct.MultiSelect = false;
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(662, 307);
            this.dgvProduct.TabIndex = 3;
            // 
            // btnViewAll
            // 
            this.btnViewAll.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewAll.Location = new System.Drawing.Point(496, 7);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(79, 31);
            this.btnViewAll.TabIndex = 52;
            this.btnViewAll.Text = "V&iewAll";
            this.btnViewAll.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(496, 40);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(79, 31);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "&Add ";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // UclLinkShelfProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclLinkShelfProduct";
            this.Size = new System.Drawing.Size(936, 604);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlDrugGrouping.ResumeLayout(false);
            this.pnlDrugGrouping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlDrugGrouping;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbShelf;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.Button btnAdd;
    }
}
