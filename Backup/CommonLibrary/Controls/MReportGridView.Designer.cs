namespace PharmaSYSPlus.CommonLibrary
{
    partial class MReportGridView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DataGridViewContent = new System.Windows.Forms.DataGridView();
            this.menuColumns = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuColumn1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewContent)).BeginInit();
            this.menuColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridViewContent
            // 
            this.DataGridViewContent.AllowUserToAddRows = false;
            this.DataGridViewContent.AllowUserToDeleteRows = false;
            this.DataGridViewContent.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Brown;
            this.DataGridViewContent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewContent.BackgroundColor = System.Drawing.Color.RosyBrown;
            this.DataGridViewContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridViewContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridViewContent.ColumnHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewContent.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridViewContent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DataGridViewContent.Location = new System.Drawing.Point(0, 0);
            this.DataGridViewContent.MultiSelect = false;
            this.DataGridViewContent.Name = "DataGridViewContent";
            this.DataGridViewContent.RowHeadersVisible = false;
            this.DataGridViewContent.RowHeadersWidth = 25;
            this.DataGridViewContent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.DataGridViewContent.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridViewContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewContent.Size = new System.Drawing.Size(253, 216);
            this.DataGridViewContent.TabIndex = 5;
            this.DataGridViewContent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridViewContent_MouseDown);
            this.DataGridViewContent.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewContent_RowEnter);
            this.DataGridViewContent.DoubleClick += new System.EventHandler(this.DataGridViewContent_DoubleClick);
            this.DataGridViewContent.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewContent_CellFormatting);
            this.DataGridViewContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridViewContent_KeyDown);
            // 
            // menuColumns
            // 
            this.menuColumns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuColumn1});
            this.menuColumns.Name = "menuColumns";
            this.menuColumns.Size = new System.Drawing.Size(139, 26);
            // 
            // mnuColumn1
            // 
            this.mnuColumn1.Checked = true;
            this.mnuColumn1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuColumn1.Name = "mnuColumn1";
            this.mnuColumn1.Size = new System.Drawing.Size(138, 22);
            this.mnuColumn1.Text = "Description";
            // 
            // MReportGridView
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.DataGridViewContent);
            this.Name = "MReportGridView";
            this.Size = new System.Drawing.Size(253, 216);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewContent)).EndInit();
            this.menuColumns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridViewContent;
        private System.Windows.Forms.ContextMenuStrip menuColumns;
        private System.Windows.Forms.ToolStripMenuItem mnuColumn1;
        private System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1;
        private System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2;
    }
}
