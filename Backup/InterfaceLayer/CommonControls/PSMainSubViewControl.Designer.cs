namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    partial class PSMainSubViewControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSMainSubViewControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgSubGrid = new System.Windows.Forms.DataGridView();
            this.dgMainGrid = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgSubGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMainGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dgSubGrid
            // 
            this.dgSubGrid.AllowUserToAddRows = false;
            this.dgSubGrid.AllowUserToDeleteRows = false;
            this.dgSubGrid.AllowUserToOrderColumns = true;
            this.dgSubGrid.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgSubGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSubGrid.Location = new System.Drawing.Point(0, 141);
            this.dgSubGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgSubGrid.MultiSelect = false;
            this.dgSubGrid.Name = "dgSubGrid";
            this.dgSubGrid.RowHeadersWidth = 26;
            this.dgSubGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSubGrid.Size = new System.Drawing.Size(507, 151);
            this.dgSubGrid.TabIndex = 1;
            this.dgSubGrid.Visible = false;
            this.dgSubGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSubGrid_RowEnter);
            this.dgSubGrid.DoubleClick += new System.EventHandler(this.dgSubGrid_DoubleClick);
            this.dgSubGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgSubGrid_KeyDown);
            // 
            // dgMainGrid
            // 
            this.dgMainGrid.AllowUserToAddRows = false;
            this.dgMainGrid.AllowUserToOrderColumns = true;
            this.dgMainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMainGrid.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgMainGrid.DateColumnNames")));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMainGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgMainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMainGrid.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgMainGrid.DoubleColumnNames")));
            this.dgMainGrid.IsAllowDelete = true;
            this.dgMainGrid.IsAllowNewRow = true;
            this.dgMainGrid.Location = new System.Drawing.Point(0, 0);
            this.dgMainGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgMainGrid.MultiSelect = false;
            this.dgMainGrid.Name = "dgMainGrid";
            this.dgMainGrid.NextRowColumn = 0;
            this.dgMainGrid.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgMainGrid.NumericColumnNames")));
            this.dgMainGrid.RowHeadersWidth = 26;
            this.dgMainGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgMainGrid.Size = new System.Drawing.Size(520, 323);
            this.dgMainGrid.TabIndex = 0;
            this.dgMainGrid.OnEscapeKeyPressed += new System.EventHandler(this.dgMainGrid_OnEscapeKeyPressed);
            this.dgMainGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgMainGrid_CellBeginEdit);
            this.dgMainGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellEndEdit);
            this.dgMainGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellLeave);
            this.dgMainGrid.OnRowAdded += new System.EventHandler(this.dgMainGrid_OnRowAdded);
            this.dgMainGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellDoubleClick);
            this.dgMainGrid.OnCellValueChangeCommited += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainGridView.CellValueChangeCommited(this.dgMainGrid_OnCellValueChangeCommited);
            this.dgMainGrid.OnEnterKeyPressed += new System.EventHandler(this.dgMainGrid_EnterKeyPressed);
            this.dgMainGrid.OnArrowUpDownPressed += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainGridView.ArrowUpDownPressed(this.dgMainGrid_OnArrowUpDownPressed);
            this.dgMainGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgMainGrid_CellFormatting);
            this.dgMainGrid.OnShiftTABKeyPressed += new System.EventHandler(this.dgMainGrid_OnShiftTABKeyPressed);
            this.dgMainGrid.OnRowDeleted += new System.EventHandler(this.dgMainGrid_OnRowDeleted);
            this.dgMainGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgMainGrid_KeyDown);
            this.dgMainGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellEnter);
            this.dgMainGrid.OnCellTextChanged += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainGridView.CellTextChanged(this.dgMainGrid_OnCellTextChanged);
            this.dgMainGrid.OnTABKeyPressed += new System.EventHandler(this.dgMainGrid_OnTABKeyPressed);
            // 
            // PSMainSubViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.dgSubGrid);
            this.Controls.Add(this.dgMainGrid);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(520, 323);
            this.Name = "PSMainSubViewControl";
            this.Size = new System.Drawing.Size(520, 323);
            ((System.ComponentModel.ISupportInitialize)(this.dgSubGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMainGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PSMainGridView dgMainGrid;
        private System.Windows.Forms.DataGridView dgSubGrid;
    }
}
