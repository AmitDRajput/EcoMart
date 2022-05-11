namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclDrugGrouping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclDrugGrouping));
            this.ttDrugGrouping = new System.Windows.Forms.ToolTip(this.components);
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblNoofRows = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlDrugGrouping = new System.Windows.Forms.Panel();
            this.lblProduct = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblDrug = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mcbProduct = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mcbDrug = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlList = new System.Windows.Forms.Panel();
            this.dgvUpperListY = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.btnReverse = new System.Windows.Forms.Button();
            this.dgvLowerListY = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.dgvLowerList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.dgvUpperList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlDrugGrouping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.pnlList.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(862, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Controls.Add(this.lblNoofRows);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 625);
            this.MMBottomPanel.Size = new System.Drawing.Size(864, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblNoofRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlDrugGrouping);
            this.MMMainPanel.Controls.Add(this.pnlList);
            this.MMMainPanel.Size = new System.Drawing.Size(864, 573);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlDrugGrouping, 0);
            // 
            // ttDrugGrouping
            // 
            this.ttDrugGrouping.AutomaticDelay = 200;
            this.ttDrugGrouping.AutoPopDelay = 5000;
            this.ttDrugGrouping.InitialDelay = 10;
            this.ttDrugGrouping.ReshowDelay = 10;
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(638, 0);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 26);
            this.txtNoOfRows.TabIndex = 1013;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNoofRows
            // 
            this.lblNoofRows.AutoSize = true;
            this.lblNoofRows.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRows.Location = new System.Drawing.Point(547, 3);
            this.lblNoofRows.Name = "lblNoofRows";
            this.lblNoofRows.Size = new System.Drawing.Size(74, 15);
            this.lblNoofRows.TabIndex = 1012;
            this.lblNoofRows.Text = "No Of Rows";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 2);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(2, 16);
            this.lblMessage.TabIndex = 1009;
            // 
            // pnlDrugGrouping
            // 
            this.pnlDrugGrouping.BackColor = System.Drawing.Color.LightGray;
            this.pnlDrugGrouping.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDrugGrouping.Controls.Add(this.lblProduct);
            this.pnlDrugGrouping.Controls.Add(this.lblDrug);
            this.pnlDrugGrouping.Controls.Add(this.mcbProduct);
            this.pnlDrugGrouping.Controls.Add(this.mcbDrug);
            this.pnlDrugGrouping.Controls.Add(this.dgvProduct);
            this.pnlDrugGrouping.Controls.Add(this.btnViewAll);
            this.pnlDrugGrouping.Controls.Add(this.btnAdd);
            this.pnlDrugGrouping.Location = new System.Drawing.Point(115, 100);
            this.pnlDrugGrouping.Name = "pnlDrugGrouping";
            this.pnlDrugGrouping.Size = new System.Drawing.Size(664, 384);
            this.pnlDrugGrouping.TabIndex = 0;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(54, 49);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(69, 19);
            this.lblProduct.TabIndex = 54;
            this.lblProduct.Text = "&Product";
            // 
            // lblDrug
            // 
            this.lblDrug.AutoSize = true;
            this.lblDrug.Location = new System.Drawing.Point(74, 19);
            this.lblDrug.Name = "lblDrug";
            this.lblDrug.Size = new System.Drawing.Size(45, 19);
            this.lblDrug.TabIndex = 53;
            this.lblDrug.Text = "&Drug";
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
            // 
            // mcbDrug
            // 
            this.mcbDrug.ColumnWidth = null;
            this.mcbDrug.DataSource = null;
            this.mcbDrug.DisplayColumnNo = 1;
            this.mcbDrug.DropDownHeight = 200;
            this.mcbDrug.Location = new System.Drawing.Point(123, 18);
            this.mcbDrug.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbDrug.Name = "mcbDrug";
            this.mcbDrug.SelectedID = null;
            this.mcbDrug.ShowNew = false;
            this.mcbDrug.Size = new System.Drawing.Size(363, 26);
            this.mcbDrug.SourceDataString = null;
            this.mcbDrug.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbDrug.TabIndex = 0;
            this.mcbDrug.UserControlToShow = null;
            this.mcbDrug.ValueColumnNo = 0;
            this.mcbDrug.EnterKeyPressed += new System.EventHandler(this.mcbDrug_EnterKeyPressed);
            this.mcbDrug.SeletectIndexChanged += new System.EventHandler(this.mcbDrug_SeletectIndexChanged);
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
            this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);
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
            // pnlList
            // 
            this.pnlList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlList.Controls.Add(this.dgvUpperListY);
            this.pnlList.Controls.Add(this.btnReverse);
            this.pnlList.Controls.Add(this.dgvLowerListY);
            this.pnlList.Controls.Add(this.dgvLowerList);
            this.pnlList.Controls.Add(this.dgvUpperList);
            this.pnlList.Location = new System.Drawing.Point(92, 21);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(705, 526);
            this.pnlList.TabIndex = 1015;
            // 
            // dgvUpperListY
            // 
            this.dgvUpperListY.BackColor = System.Drawing.Color.Transparent;
            this.dgvUpperListY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvUpperListY.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperListY.DateColumnNames")));
            this.dgvUpperListY.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperListY.DoubleColumnNames")));
            this.dgvUpperListY.Filter = null;
            this.dgvUpperListY.Location = new System.Drawing.Point(16, 1);
            this.dgvUpperListY.Name = "dgvUpperListY";
            this.dgvUpperListY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvUpperListY.ShowGridFilter = false;
            this.dgvUpperListY.Size = new System.Drawing.Size(649, 219);
            this.dgvUpperListY.TabIndex = 53;
            this.dgvUpperListY.SelectedRowChanged += new System.EventHandler(this.dgvUpperListY_SelectedRowChanged);
            // 
            // btnReverse
            // 
            this.btnReverse.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReverse.Location = new System.Drawing.Point(673, 3);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(22, 116);
            this.btnReverse.TabIndex = 52;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // dgvLowerListY
            // 
            this.dgvLowerListY.BackColor = System.Drawing.Color.Transparent;
            this.dgvLowerListY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvLowerListY.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerListY.DateColumnNames")));
            this.dgvLowerListY.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerListY.DoubleColumnNames")));
            this.dgvLowerListY.Filter = null;
            this.dgvLowerListY.Location = new System.Drawing.Point(15, 237);
            this.dgvLowerListY.Name = "dgvLowerListY";
            this.dgvLowerListY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvLowerListY.ShowGridFilter = false;
            this.dgvLowerListY.Size = new System.Drawing.Size(649, 287);
            this.dgvLowerListY.TabIndex = 54;
            // 
            // dgvLowerList
            // 
            this.dgvLowerList.BackColor = System.Drawing.Color.Transparent;
            this.dgvLowerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvLowerList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerList.DateColumnNames")));
            this.dgvLowerList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerList.DoubleColumnNames")));
            this.dgvLowerList.Filter = null;
            this.dgvLowerList.Location = new System.Drawing.Point(15, 236);
            this.dgvLowerList.Name = "dgvLowerList";
            this.dgvLowerList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvLowerList.ShowGridFilter = false;
            this.dgvLowerList.Size = new System.Drawing.Size(649, 287);
            this.dgvLowerList.TabIndex = 29;
            // 
            // dgvUpperList
            // 
            this.dgvUpperList.BackColor = System.Drawing.Color.Transparent;
            this.dgvUpperList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvUpperList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperList.DateColumnNames")));
            this.dgvUpperList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperList.DoubleColumnNames")));
            this.dgvUpperList.Filter = null;
            this.dgvUpperList.Location = new System.Drawing.Point(15, 0);
            this.dgvUpperList.Name = "dgvUpperList";
            this.dgvUpperList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvUpperList.ShowGridFilter = false;
            this.dgvUpperList.Size = new System.Drawing.Size(649, 219);
            this.dgvUpperList.TabIndex = 27;
            this.dgvUpperList.SelectedRowChanged += new System.EventHandler(this.dgvUpperList_SelectedRowChanged);
            // 
            // UclDrugGrouping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "UclDrugGrouping";
            this.Size = new System.Drawing.Size(864, 648);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlDrugGrouping.ResumeLayout(false);
            this.pnlDrugGrouping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.pnlList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttDrugGrouping;
        private System.Windows.Forms.Label lblMessage;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Label lblNoofRows;
        private System.Windows.Forms.Panel pnlDrugGrouping;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbDrug;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlList;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvUpperList;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvLowerList;
        private System.Windows.Forms.Button btnReverse;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvUpperListY;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvLowerListY;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblProduct;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblDrug;

    }
}
