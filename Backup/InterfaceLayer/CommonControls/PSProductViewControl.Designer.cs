namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    partial class PSProductViewControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSProductViewControl));
            this.dgProductListGrid = new System.Windows.Forms.DataGridView();
            this.dgBatchListGrid = new System.Windows.Forms.DataGridView();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnlBatchGrid = new System.Windows.Forms.Panel();
            this.pnlProductContent = new System.Windows.Forms.Panel();
            this.lblGenericCategoryName = new System.Windows.Forms.Label();
            this.dgMainGrid = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductMainGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgProductListGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBatchListGrid)).BeginInit();
            this.pnlBatchGrid.SuspendLayout();
            this.pnlProductContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMainGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dgProductListGrid
            // 
            this.dgProductListGrid.AllowUserToAddRows = false;
            this.dgProductListGrid.AllowUserToDeleteRows = false;
            this.dgProductListGrid.AllowUserToOrderColumns = true;
            this.dgProductListGrid.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgProductListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProductListGrid.Location = new System.Drawing.Point(0, 144);
            this.dgProductListGrid.Margin = new System.Windows.Forms.Padding(4);
            this.dgProductListGrid.MinimumSize = new System.Drawing.Size(624, 194);
            this.dgProductListGrid.MultiSelect = false;
            this.dgProductListGrid.Name = "dgProductListGrid";
            this.dgProductListGrid.ReadOnly = true;
            this.dgProductListGrid.RowHeadersWidth = 26;
            this.dgProductListGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgProductListGrid.Size = new System.Drawing.Size(891, 202);
            this.dgProductListGrid.TabIndex = 1;
            this.dgProductListGrid.Visible = false;
            this.dgProductListGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgProductListGrid_RowEnter);
            this.dgProductListGrid.DoubleClick += new System.EventHandler(this.dgProductListGrid_DoubleClick);
            this.dgProductListGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgProductListGrid_KeyDown);
            this.dgProductListGrid.VisibleChanged += new System.EventHandler(this.dgProductListGrid_VisibleChanged);
            // 
            // dgBatchListGrid
            // 
            this.dgBatchListGrid.AllowUserToAddRows = false;
            this.dgBatchListGrid.AllowUserToDeleteRows = false;
            this.dgBatchListGrid.AllowUserToOrderColumns = true;
            this.dgBatchListGrid.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgBatchListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBatchListGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgBatchListGrid.Location = new System.Drawing.Point(0, 0);
            this.dgBatchListGrid.Margin = new System.Windows.Forms.Padding(4);
            this.dgBatchListGrid.MaximumSize = new System.Drawing.Size(843, 194);
            this.dgBatchListGrid.MinimumSize = new System.Drawing.Size(500, 190);
            this.dgBatchListGrid.MultiSelect = false;
            this.dgBatchListGrid.Name = "dgBatchListGrid";
            this.dgBatchListGrid.ReadOnly = true;
            this.dgBatchListGrid.RowHeadersWidth = 26;
            this.dgBatchListGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBatchListGrid.Size = new System.Drawing.Size(843, 190);
            this.dgBatchListGrid.TabIndex = 2;
            this.dgBatchListGrid.DoubleClick += new System.EventHandler(this.dgBatchListGrid_DoubleClick);
            this.dgBatchListGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBatchListGrid_KeyDown);
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(4, 191);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(124, 32);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // pnlBatchGrid
            // 
            this.pnlBatchGrid.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlBatchGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBatchGrid.Controls.Add(this.btnNew);
            this.pnlBatchGrid.Controls.Add(this.dgBatchListGrid);
            this.pnlBatchGrid.Location = new System.Drawing.Point(10, 68);
            this.pnlBatchGrid.Margin = new System.Windows.Forms.Padding(4);
            this.pnlBatchGrid.Name = "pnlBatchGrid";
            this.pnlBatchGrid.Size = new System.Drawing.Size(881, 228);
            this.pnlBatchGrid.TabIndex = 3;
            this.pnlBatchGrid.Visible = false;
            // 
            // pnlProductContent
            // 
            this.pnlProductContent.BackColor = System.Drawing.SystemColors.Control;
            this.pnlProductContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProductContent.Controls.Add(this.lblGenericCategoryName);
            this.pnlProductContent.Location = new System.Drawing.Point(3, 345);
            this.pnlProductContent.Name = "pnlProductContent";
            this.pnlProductContent.Size = new System.Drawing.Size(888, 35);
            this.pnlProductContent.TabIndex = 4;
            this.pnlProductContent.Visible = false;
            // 
            // lblGenericCategoryName
            // 
            this.lblGenericCategoryName.AutoSize = true;
            this.lblGenericCategoryName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenericCategoryName.Location = new System.Drawing.Point(29, 9);
            this.lblGenericCategoryName.Name = "lblGenericCategoryName";
            this.lblGenericCategoryName.Size = new System.Drawing.Size(47, 13);
            this.lblGenericCategoryName.TabIndex = 0;
            this.lblGenericCategoryName.Text = "label1";
            // 
            // dgMainGrid
            // 
            this.dgMainGrid.AllowUserToAddRows = false;
            this.dgMainGrid.AllowUserToOrderColumns = true;
            this.dgMainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMainGrid.CurrentQuantity = 0;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMainGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgMainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMainGrid.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgMainGrid.DoubleColumnNames")));
            this.dgMainGrid.IsAllowDelete = true;
            this.dgMainGrid.IsAllowNewRow = false;
            this.dgMainGrid.Location = new System.Drawing.Point(0, 0);
            this.dgMainGrid.Margin = new System.Windows.Forms.Padding(4);
            this.dgMainGrid.MultiSelect = false;
            this.dgMainGrid.Name = "dgMainGrid";
            this.dgMainGrid.NewRowColumnName = null;
            this.dgMainGrid.NextRowColumn = 0;
            this.dgMainGrid.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgMainGrid.NumericColumnNames")));
            this.dgMainGrid.RowHeadersWidth = 26;
            this.dgMainGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgMainGrid.Size = new System.Drawing.Size(909, 415);
            this.dgMainGrid.TabIndex = 0;
            this.dgMainGrid.OnEscapeKeyPressed += new System.EventHandler(this.dgMainGrid_OnEscapeKeyPressed);
            this.dgMainGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgMainGrid_CellBeginEdit);
            this.dgMainGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellEndEdit);
            this.dgMainGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellLeave);
            this.dgMainGrid.OnRowAdded += new System.EventHandler(this.dgMainGrid_OnRowAdded);
            this.dgMainGrid.OnCellValueChangeCommited += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductMainGridView.CellValueChangeCommited(this.dgMainGrid_OnCellValueChangeCommited);
            this.dgMainGrid.OnEnterKeyPressed += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductMainGridView.EnterKeyPressed(this.dgMainGrid_EnterKeyPressed);
            this.dgMainGrid.OnArrowUpDownPressed += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductMainGridView.ArrowUpDownPressed(this.dgMainGrid_OnArrowUpDownPressed);
            this.dgMainGrid.OnEnterKeyPressed_Processed += new System.EventHandler(this.dgMainGrid_OnEnterKeyPressed_Processed);
            this.dgMainGrid.OnShiftTABKeyPressed += new System.EventHandler(this.dgMainGrid_OnShiftTABKeyPressed);
            this.dgMainGrid.OnRowDeleted += new System.EventHandler(this.dgMainGrid_OnRowDeleted);
            this.dgMainGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellEnter);
            this.dgMainGrid.OnCellTextChanged += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductMainGridView.CellTextChanged(this.dgMainGrid_OnCellTextChanged);
            this.dgMainGrid.OnTABKeyPressed += new System.EventHandler(this.dgMainGrid_OnTABKeyPressed);
            this.dgMainGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellContentClick);
            // 
            // PSProductViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.pnlProductContent);
            this.Controls.Add(this.pnlBatchGrid);
            this.Controls.Add(this.dgProductListGrid);
            this.Controls.Add(this.dgMainGrid);
            this.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(700, 415);
            this.Name = "PSProductViewControl";
            this.Size = new System.Drawing.Size(909, 415);
            ((System.ComponentModel.ISupportInitialize)(this.dgProductListGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBatchListGrid)).EndInit();
            this.pnlBatchGrid.ResumeLayout(false);
            this.pnlProductContent.ResumeLayout(false);
            this.pnlProductContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMainGrid)).EndInit();
            this.ResumeLayout(false);

        } 

        #endregion

        private PSProductMainGridView dgMainGrid;
        private System.Windows.Forms.DataGridView dgProductListGrid;
        private System.Windows.Forms.DataGridView dgBatchListGrid;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel pnlBatchGrid;
        private System.Windows.Forms.Panel pnlProductContent;
        private System.Windows.Forms.Label lblGenericCategoryName;
    }
}
