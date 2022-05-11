namespace EcoMart.InterfaceLayer.CommonControls
{
    partial class PSSaleViewControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSSaleViewControl));
            this.dgSubGrid = new System.Windows.Forms.DataGridView();
            this.dgMainGrid = new EcoMart.InterfaceLayer.CommonControls.PSMainGridViewForSale();
            ((System.ComponentModel.ISupportInitialize)(this.dgSubGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMainGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dgSubGrid
            // 
            this.dgSubGrid.AllowUserToAddRows = false;
            this.dgSubGrid.AllowUserToDeleteRows = false;
            this.dgSubGrid.AllowUserToOrderColumns = true;
            this.dgSubGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSubGrid.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgSubGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSubGrid.Location = new System.Drawing.Point(13, 66);
            this.dgSubGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgSubGrid.MultiSelect = false;
            this.dgSubGrid.Name = "dgSubGrid";
            this.dgSubGrid.RowHeadersWidth = 26;
            this.dgSubGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSubGrid.Size = new System.Drawing.Size(486, 94);
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
            this.dgMainGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgMainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMainGrid.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgMainGrid.DateColumnNames")));
            this.dgMainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMainGrid.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgMainGrid.DoubleColumnNames")));
            this.dgMainGrid.IsAllowDelete = true;
            this.dgMainGrid.IsAllowNewRow = true;
            this.dgMainGrid.Location = new System.Drawing.Point(0, 0);
            this.dgMainGrid.MultiSelect = false;
            this.dgMainGrid.Name = "dgMainGrid";
            this.dgMainGrid.NextRowColumn = 0;
            this.dgMainGrid.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgMainGrid.NumericColumnNames")));
            this.dgMainGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgMainGrid.Size = new System.Drawing.Size(520, 200);
            this.dgMainGrid.TabIndex = 2;
            this.dgMainGrid.OnEnterKeyPressed += new System.EventHandler(this.dgMainGrid_EnterKeyPressed);
            this.dgMainGrid.OnCellTextChanged += new EcoMart.InterfaceLayer.CommonControls.PSMainGridViewForSale.CellTextChanged(this.dgMainGrid_OnCellTextChanged);
            this.dgMainGrid.OnCellValueChangeCommited += new EcoMart.InterfaceLayer.CommonControls.PSMainGridViewForSale.CellValueChangeCommited(this.dgMainGrid_OnCellValueChangeCommited);
            this.dgMainGrid.OnArrowUpDownPressed += new EcoMart.InterfaceLayer.CommonControls.PSMainGridViewForSale.ArrowUpDownPressed(this.dgMainGrid_OnArrowUpDownPressed);
            this.dgMainGrid.OnRowAdded += new System.EventHandler(this.dgMainGrid_OnRowAdded);
            this.dgMainGrid.OnRowDeleted += new System.EventHandler(this.dgMainGrid_OnRowDeleted);
            this.dgMainGrid.OnTABKeyPressed += new System.EventHandler(this.dgMainGrid_OnTABKeyPressed);
            this.dgMainGrid.OnShiftTABKeyPressed += new System.EventHandler(this.dgMainGrid_OnShiftTABKeyPressed);
            this.dgMainGrid.OnEscapeKeyPressed += new System.EventHandler(this.dgMainGrid_OnEscapeKeyPressed);
            this.dgMainGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgMainGrid_CellBeginEdit);
            this.dgMainGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellDoubleClick);
            this.dgMainGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellEndEdit);
            this.dgMainGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellEnter);
            this.dgMainGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgMainGrid_CellFormatting);
            this.dgMainGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellLeave);
            // 
            // PSSaleViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.dgSubGrid);
            this.Controls.Add(this.dgMainGrid);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(520, 200);
            this.Name = "PSSaleViewControl";
            this.Size = new System.Drawing.Size(520, 200);
            ((System.ComponentModel.ISupportInitialize)(this.dgSubGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMainGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgSubGrid;
        private PSMainGridViewForSale dgMainGrid;
    }
}
