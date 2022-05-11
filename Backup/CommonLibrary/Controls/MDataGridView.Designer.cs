namespace PharmaSYSPlus.CommonLibrary
{
    partial class MDataGridView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DataGridViewContent = new System.Windows.Forms.DataGridView();
            this.DataGridViewHeader = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridViewContent
            // 
            this.DataGridViewContent.AllowUserToAddRows = false;
            this.DataGridViewContent.AllowUserToDeleteRows = false;
            this.DataGridViewContent.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Bisque;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridViewContent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewContent.BackgroundColor = System.Drawing.Color.LightGray;
            this.DataGridViewContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridViewContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridViewContent.ColumnHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewContent.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewContent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DataGridViewContent.Location = new System.Drawing.Point(0, 37);
            this.DataGridViewContent.MultiSelect = false;
            this.DataGridViewContent.Name = "DataGridViewContent";
            this.DataGridViewContent.RowHeadersVisible = false;
            this.DataGridViewContent.RowHeadersWidth = 25;
            this.DataGridViewContent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.DataGridViewContent.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridViewContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewContent.Size = new System.Drawing.Size(250, 116);
            this.DataGridViewContent.TabIndex = 5;
            this.DataGridViewContent.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DataGridViewContent_Scroll);
            this.DataGridViewContent.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewContent_RowEnter);
            this.DataGridViewContent.DoubleClick += new System.EventHandler(this.DataGridViewContent_DoubleClick);
            this.DataGridViewContent.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DataGridViewContent_ColumnDisplayIndexChanged);
            this.DataGridViewContent.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewContent_CellFormatting);
            this.DataGridViewContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridViewContent_KeyDown);
            this.DataGridViewContent.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DataGridViewContent_ColumnWidthChanged);
            // 
            // DataGridViewHeader
            // 
            this.DataGridViewHeader.AllowUserToDeleteRows = false;
            this.DataGridViewHeader.AllowUserToResizeColumns = false;
            this.DataGridViewHeader.AllowUserToResizeRows = false;
            this.DataGridViewHeader.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridViewHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewHeader.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.DataGridViewHeader.ColumnHeadersHeight = 30;
            this.DataGridViewHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewHeader.DefaultCellStyle = dataGridViewCellStyle6;
            this.DataGridViewHeader.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DataGridViewHeader.EnableHeadersVisualStyles = false;
            this.DataGridViewHeader.Location = new System.Drawing.Point(0, 0);
            this.DataGridViewHeader.MultiSelect = false;
            this.DataGridViewHeader.Name = "DataGridViewHeader";
            this.DataGridViewHeader.RowHeadersVisible = false;
            this.DataGridViewHeader.RowHeadersWidth = 25;
            this.DataGridViewHeader.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DataGridViewHeader.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DataGridViewHeader.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewHeader.Size = new System.Drawing.Size(250, 38);
            this.DataGridViewHeader.TabIndex = 4;
            this.DataGridViewHeader.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewHeader_CellClick);
            this.DataGridViewHeader.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DataGridViewHeader_KeyUp);
            // 
            // MDataGridView
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.DataGridViewContent);
            this.Controls.Add(this.DataGridViewHeader);
            this.Name = "MDataGridView";
            this.Size = new System.Drawing.Size(400, 216);
            this.Resize += new System.EventHandler(this.MDataGridView_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridViewContent;
        private System.Windows.Forms.DataGridView DataGridViewHeader;
    }
}
