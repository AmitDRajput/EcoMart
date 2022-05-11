namespace EcoMart.InterfaceLayer.CommonControls
{
    partial class PSCashBankcontrol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSCashBankcontrol));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgMainGrid = new EcoMart.InterfaceLayer.CommonControls.PSMainGridViewForCashBank();
            ((System.ComponentModel.ISupportInitialize)(this.dgMainGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dgMainGrid
            // 
            this.dgMainGrid.AllowUserToAddRows = false;
            this.dgMainGrid.AllowUserToOrderColumns = true;
            this.dgMainGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.dgMainGrid.OnEnterKeyPressed += new System.EventHandler(this.dgMainGrid_EnterKeyPressed);
            this.dgMainGrid.OnCellTextChanged += new EcoMart.InterfaceLayer.CommonControls.PSMainGridViewForCashBank.CellTextChanged(this.dgMainGrid_OnCellTextChanged);
            this.dgMainGrid.OnCellValueChangeCommited += new EcoMart.InterfaceLayer.CommonControls.PSMainGridViewForCashBank.CellValueChangeCommited(this.dgMainGrid_OnCellValueChangeCommited);
            this.dgMainGrid.OnArrowUpDownPressed += new EcoMart.InterfaceLayer.CommonControls.PSMainGridViewForCashBank.ArrowUpDownPressed(this.dgMainGrid_OnArrowUpDownPressed);
            this.dgMainGrid.OnEscapeKeyPressed += new System.EventHandler(this.dgMainGrid_OnEscapeKeyPressed);
            this.dgMainGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellDoubleClick);
            this.dgMainGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellEnter);
            this.dgMainGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgMainGrid_CellFormatting);
            this.dgMainGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMainGrid_CellLeave);
            this.dgMainGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgMainGrid_KeyDown);
            // 
            // PSCashBankcontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.dgMainGrid);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(520, 323);
            this.Name = "PSCashBankcontrol";
            this.Size = new System.Drawing.Size(520, 323);
            ((System.ComponentModel.ISupportInitialize)(this.dgMainGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PSMainGridViewForCashBank dgMainGrid;
    }
}
