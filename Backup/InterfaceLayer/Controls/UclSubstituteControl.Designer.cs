namespace PharmaSYSRetailPlus.InterfaceLayer.Controls
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
            this.btnCancel = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton();
            this.dgvSubstitute = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mcbProduct = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.pnlPartyCompany.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPartyCompany
            // 
            this.pnlPartyCompany.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlPartyCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPartyCompany.Controls.Add(this.btnCancel);
            this.pnlPartyCompany.Controls.Add(this.dgvSubstitute);
            this.pnlPartyCompany.Controls.Add(this.mPlbl1);
            this.pnlPartyCompany.Controls.Add(this.mcbProduct);
            this.pnlPartyCompany.Location = new System.Drawing.Point(0, 0);
            this.pnlPartyCompany.Name = "pnlPartyCompany";
            this.pnlPartyCompany.Size = new System.Drawing.Size(664, 394);
            this.pnlPartyCompany.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(539, 348);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 39);
            this.btnCancel.TabIndex = 1049;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCancel_KeyDown);
            // 
            // dgvSubstitute
            // 
            this.dgvSubstitute.ApplyAlternateRowStyle = false;
            this.dgvSubstitute.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvSubstitute.BackColor = System.Drawing.Color.Khaki;
            this.dgvSubstitute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvSubstitute.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvSubstitute.ConvertDatetoMonth")));
            this.dgvSubstitute.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSubstitute.DateColumnNames")));
            this.dgvSubstitute.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSubstitute.DoubleColumnNames")));
            this.dgvSubstitute.Location = new System.Drawing.Point(21, 106);
            this.dgvSubstitute.Name = "dgvSubstitute";
            this.dgvSubstitute.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSubstitute.OptionalColumnNames")));
            this.dgvSubstitute.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvSubstitute.Size = new System.Drawing.Size(612, 237);
            this.dgvSubstitute.TabIndex = 1048;
            this.dgvSubstitute.DoubleClicked += new System.EventHandler(this.dgvSubstitute_DoubleClicked);
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(30, 39);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(69, 19);
            this.mPlbl1.TabIndex = 138;
            this.mPlbl1.Text = "Product";
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(123, 37);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = null;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(363, 23);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 0;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            this.mcbProduct.SeletectIndexChanged += new System.EventHandler(this.mcbProduct_SeletectIndexChanged);
            // 
            // UclSubstituteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPartyCompany);
            this.Name = "UclSubstituteControl";
            this.Size = new System.Drawing.Size(664, 394);
            this.pnlPartyCompany.ResumeLayout(false);
            this.pnlPartyCompany.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPartyCompany;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvSubstitute;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton btnCancel;
    }
}
