namespace EcoMart.InterfaceLayer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclDrugGrouping));
            this.ttDrugGrouping = new System.Windows.Forms.ToolTip(this.components);
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblNoofRows = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlDrugGrouping = new System.Windows.Forms.Panel();
            this.pnlLinkDrugGrping = new System.Windows.Forms.Panel();
            this.mcbDrug = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblProduct = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblDrug = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbProduct = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.dgLinkProductsToAdd = new System.Windows.Forms.DataGridView();
            this.pnlList = new System.Windows.Forms.Panel();
            this.dgvUpperListY = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.btnReverse = new System.Windows.Forms.Button();
            this.dgvLowerListY = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.dgvLowerList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.dgvUpperList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlDrugGrouping.SuspendLayout();
            this.pnlLinkDrugGrping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLinkProductsToAdd)).BeginInit();
            this.pnlList.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(862, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Controls.Add(this.lblNoofRows);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 585);
            this.MMBottomPanel.Size = new System.Drawing.Size(864, 63);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblRightSideFooterMsg, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblNoofRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlDrugGrouping);
            this.MMMainPanel.Controls.Add(this.pnlList);
            this.MMMainPanel.Size = new System.Drawing.Size(864, 522);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlDrugGrouping, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(396, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 20);
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
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 22);
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
            this.pnlDrugGrouping.Controls.Add(this.pnlLinkDrugGrping);
            this.pnlDrugGrouping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDrugGrouping.Location = new System.Drawing.Point(0, 0);
            this.pnlDrugGrouping.Name = "pnlDrugGrouping";
            this.pnlDrugGrouping.Size = new System.Drawing.Size(862, 520);
            this.pnlDrugGrouping.TabIndex = 0;
            // 
            // pnlLinkDrugGrping
            // 
            this.pnlLinkDrugGrping.Controls.Add(this.mcbDrug);
            this.pnlLinkDrugGrping.Controls.Add(this.btnViewAll);
            this.pnlLinkDrugGrping.Controls.Add(this.btnAdd);
            this.pnlLinkDrugGrping.Controls.Add(this.lblProduct);
            this.pnlLinkDrugGrping.Controls.Add(this.lblDrug);
            this.pnlLinkDrugGrping.Controls.Add(this.mcbProduct);
            this.pnlLinkDrugGrping.Controls.Add(this.dgLinkProductsToAdd);
            this.pnlLinkDrugGrping.Controls.Add(this.dgvProduct);
            this.pnlLinkDrugGrping.Location = new System.Drawing.Point(66, 26);
            this.pnlLinkDrugGrping.Name = "pnlLinkDrugGrping";
            this.pnlLinkDrugGrping.Size = new System.Drawing.Size(730, 465);
            this.pnlLinkDrugGrping.TabIndex = 55;
            // 
            // mcbDrug
            // 
            this.mcbDrug.ColumnWidth = null;
            this.mcbDrug.DataSource = null;
            this.mcbDrug.DisplayColumnNo = 1;
            this.mcbDrug.DropDownHeight = 200;
            this.mcbDrug.Location = new System.Drawing.Point(136, 23);
            this.mcbDrug.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbDrug.Name = "mcbDrug";
            this.mcbDrug.SelectedID = "";
            this.mcbDrug.ShowNew = false;
            this.mcbDrug.Size = new System.Drawing.Size(434, 22);
            this.mcbDrug.SourceDataString = null;
            this.mcbDrug.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbDrug.TabIndex = 0;
            this.mcbDrug.UserControlToShow = null;
            this.mcbDrug.ValueColumnNo = 0;
            this.mcbDrug.SeletectIndexChanged += new System.EventHandler(this.mcbDrug_SeletectIndexChanged);
            this.mcbDrug.EnterKeyPressed += new System.EventHandler(this.mcbDrug_EnterKeyPressed);
            // 
            // btnViewAll
            // 
            this.btnViewAll.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewAll.Location = new System.Drawing.Point(588, 19);
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
            this.btnAdd.Location = new System.Drawing.Point(588, 52);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(79, 31);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "&Add ";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(63, 59);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(56, 16);
            this.lblProduct.TabIndex = 54;
            this.lblProduct.Text = "&Product";
            // 
            // lblDrug
            // 
            this.lblDrug.AutoSize = true;
            this.lblDrug.Location = new System.Drawing.Point(63, 26);
            this.lblDrug.Name = "lblDrug";
            this.lblDrug.Size = new System.Drawing.Size(38, 16);
            this.lblDrug.TabIndex = 53;
            this.lblDrug.Text = "&Drug";
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(136, 56);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = "";
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(434, 22);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 1;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            this.mcbProduct.EnterKeyPressed += new System.EventHandler(this.mcbProduct_EnterKeyPressed);
            this.mcbProduct.OnTextValueChanged += new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew.TextValueChanged(this.mcbProduct_OnTextValueChanged);
            this.mcbProduct.TabKeyPressed += new System.EventHandler(this.mcbProduct_TabKeyPressed);
            this.mcbProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcbProduct_KeyDown);
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProduct.CausesValidation = false;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProduct.DefaultCellStyle = dataGridViewCellStyle28;
            this.dgvProduct.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvProduct.Enabled = false;
            this.dgvProduct.Location = new System.Drawing.Point(63, 86);
            this.dgvProduct.MultiSelect = false;
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(604, 361);
            this.dgvProduct.TabIndex = 4;
            // 
            // dgLinkProductsToAdd
            // 
            this.dgLinkProductsToAdd.AllowUserToDeleteRows = false;
            this.dgLinkProductsToAdd.AllowUserToResizeColumns = false;
            this.dgLinkProductsToAdd.AllowUserToResizeRows = false;
            this.dgLinkProductsToAdd.BackgroundColor = System.Drawing.Color.DarkSeaGreen;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgLinkProductsToAdd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.dgLinkProductsToAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgLinkProductsToAdd.DefaultCellStyle = dataGridViewCellStyle26;
            this.dgLinkProductsToAdd.Location = new System.Drawing.Point(63, 86);
            this.dgLinkProductsToAdd.Name = "dgLinkProductsToAdd";
            this.dgLinkProductsToAdd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLinkProductsToAdd.Size = new System.Drawing.Size(604, 361);
            this.dgLinkProductsToAdd.TabIndex = 2;
            this.dgLinkProductsToAdd.Visible = false;
            this.dgLinkProductsToAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgLinkProductsToAdd_KeyDown);
            this.dgLinkProductsToAdd.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dgLinkProductsToAdd_PreviewKeyDown);
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
            this.Load += new System.EventHandler(this.UclDrugGrouping_Load);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlDrugGrouping.ResumeLayout(false);
            this.pnlLinkDrugGrping.ResumeLayout(false);
            this.pnlLinkDrugGrping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLinkProductsToAdd)).EndInit();
            this.pnlList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttDrugGrouping;
        private System.Windows.Forms.Label lblMessage;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Label lblNoofRows;
        private System.Windows.Forms.Panel pnlDrugGrouping;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbDrug;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlList;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvUpperList;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvLowerList;
        private System.Windows.Forms.Button btnReverse;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvUpperListY;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvLowerListY;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblProduct;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblDrug;
        private System.Windows.Forms.Panel pnlLinkDrugGrping;
        private System.Windows.Forms.DataGridView dgLinkProductsToAdd;
    }
}
