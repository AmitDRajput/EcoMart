namespace EcoMart.InterfaceLayer.Controls
{
    partial class UclSubstituteControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclSubstituteControl));
            this.pnlPartyCompany = new System.Windows.Forms.Panel();
            this.lblReset = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblSelectcategory = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblSelectProduct = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbGenericCategory = new PaperlessPharmaRetail.InterfaceLayer.CommonControls.PSGenericCategoryComboBoxNew();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnCancel = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.dgvSubstitute = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbProduct = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.pnlPartyCompany.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPartyCompany
            // 
            this.pnlPartyCompany.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlPartyCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPartyCompany.Controls.Add(this.lblReset);
            this.pnlPartyCompany.Controls.Add(this.lblSelectcategory);
            this.pnlPartyCompany.Controls.Add(this.lblSelectProduct);
            this.pnlPartyCompany.Controls.Add(this.mcbGenericCategory);
            this.pnlPartyCompany.Controls.Add(this.psLabel1);
            this.pnlPartyCompany.Controls.Add(this.btnCancel);
            this.pnlPartyCompany.Controls.Add(this.dgvSubstitute);
            this.pnlPartyCompany.Controls.Add(this.mPlbl1);
            this.pnlPartyCompany.Controls.Add(this.mcbProduct);
            this.pnlPartyCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPartyCompany.Location = new System.Drawing.Point(0, 0);
            this.pnlPartyCompany.Name = "pnlPartyCompany";
            this.pnlPartyCompany.Size = new System.Drawing.Size(714, 394);
            this.pnlPartyCompany.TabIndex = 2;
            // 
            // lblReset
            // 
            this.lblReset.AutoSize = true;
            this.lblReset.Location = new System.Drawing.Point(419, 361);
            this.lblReset.Name = "lblReset";
            this.lblReset.Size = new System.Drawing.Size(127, 15);
            this.lblReset.TabIndex = 1055;
            this.lblReset.Text = "Ctrl + R : Reset Data";
            // 
            // lblSelectcategory
            // 
            this.lblSelectcategory.AutoSize = true;
            this.lblSelectcategory.Location = new System.Drawing.Point(213, 361);
            this.lblSelectcategory.Name = "lblSelectcategory";
            this.lblSelectcategory.Size = new System.Drawing.Size(135, 15);
            this.lblSelectcategory.TabIndex = 1054;
            this.lblSelectcategory.Text = "Ctrl + I :  Contentwise";
            // 
            // lblSelectProduct
            // 
            this.lblSelectProduct.AutoSize = true;
            this.lblSelectProduct.Location = new System.Drawing.Point(13, 361);
            this.lblSelectProduct.Name = "lblSelectProduct";
            this.lblSelectProduct.Size = new System.Drawing.Size(138, 15);
            this.lblSelectProduct.TabIndex = 1053;
            this.lblSelectProduct.Text = "Ctrl + P : ProductWise";
            // 
            // mcbGenericCategory
            // 
            this.mcbGenericCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mcbGenericCategory.ColumnWidth = null;
            this.mcbGenericCategory.DataSource = null;
            this.mcbGenericCategory.DisplayColumnNo = 1;
            this.mcbGenericCategory.DropDownHeight = 200;
            this.mcbGenericCategory.Location = new System.Drawing.Point(136, 65);
            this.mcbGenericCategory.Margin = new System.Windows.Forms.Padding(2);
            this.mcbGenericCategory.Name = "mcbGenericCategory";
            this.mcbGenericCategory.SelectedID = null;
            this.mcbGenericCategory.ShowNew = true;
            this.mcbGenericCategory.Size = new System.Drawing.Size(529, 22);
            this.mcbGenericCategory.SourceDataString = null;
            this.mcbGenericCategory.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbGenericCategory.TabIndex = 1052;
            this.mcbGenericCategory.UserControlToShow = null;
            this.mcbGenericCategory.ValueColumnNo = 0;
            this.mcbGenericCategory.EnterKeyPressed += new System.EventHandler(this.mcbGenericCategory_EnterKeyPressed);
            this.mcbGenericCategory.UpArrowPressed += new System.EventHandler(this.mcbGenericCategory_UpArrowPressed);
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(22, 67);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(112, 16);
            this.psLabel1.TabIndex = 1051;
            this.psLabel1.Text = "Generic Category";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(600, 351);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 39);
            this.btnCancel.TabIndex = 1049;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCancel_KeyDown);
            // 
            // dgvSubstitute
            // 
            this.dgvSubstitute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSubstitute.ApplyAlternateRowStyle = false;
            this.dgvSubstitute.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvSubstitute.BackColor = System.Drawing.Color.Khaki;
            this.dgvSubstitute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvSubstitute.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvSubstitute.ConvertDatetoMonth")));
            this.dgvSubstitute.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSubstitute.DateColumnNames")));
            this.dgvSubstitute.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSubstitute.DoubleColumnNames")));
            this.dgvSubstitute.FreezeLastRow = false;
            this.dgvSubstitute.Location = new System.Drawing.Point(13, 106);
            this.dgvSubstitute.Name = "dgvSubstitute";
            this.dgvSubstitute.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSubstitute.NumericColumnNames")));
            this.dgvSubstitute.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSubstitute.OptionalColumnNames")));
            this.dgvSubstitute.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvSubstitute.Size = new System.Drawing.Size(687, 237);
            this.dgvSubstitute.TabIndex = 1048;
            this.dgvSubstitute.DoubleClicked += new System.EventHandler(this.dgvSubstitute_DoubleClicked);
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(22, 39);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(56, 16);
            this.mPlbl1.TabIndex = 138;
            this.mPlbl1.Text = "Product";
            // 
            // mcbProduct
            // 
            this.mcbProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(136, 37);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = "";
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(494, 22);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 0;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            this.mcbProduct.EnterKeyPressed += new System.EventHandler(this.mcbProduct_EnterKeyPressed);
            // 
            // UclSubstituteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPartyCompany);
            this.Name = "UclSubstituteControl";
            this.Size = new System.Drawing.Size(714, 394);
            this.Load += new System.EventHandler(this.UclSubstituteControl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UclSubstituteControl_KeyDown);
            this.pnlPartyCompany.ResumeLayout(false);
            this.pnlPartyCompany.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPartyCompany;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvSubstitute;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
        private PharmaSYSPlus.CommonLibrary.PSButton btnCancel;
        private CommonControls.PSLabel psLabel1;
        private PaperlessPharmaRetail.InterfaceLayer.CommonControls.PSGenericCategoryComboBoxNew mcbGenericCategory;
        private CommonControls.PSLabel lblSelectcategory;
        private CommonControls.PSLabel lblSelectProduct;
        private CommonControls.PSLabel lblReset;
    }
}
