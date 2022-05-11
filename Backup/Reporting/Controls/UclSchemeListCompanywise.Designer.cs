namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclSchemeListCompanywise
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
            this.pnlMultiSelection = new System.Windows.Forms.Panel();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.btnViewList = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtNoofSearches = new System.Windows.Forms.TextBox();
            this.lblNoofRowsSelected = new System.Windows.Forms.Label();
            this.btnOKMultiSelection = new System.Windows.Forms.Button();
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.dgvMultiSelection = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();           
            this.ttSchemeCompany = new System.Windows.Forms.ToolTip(this.components);
            this.dgvReportList = new System.Windows.Forms.DataGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlMultiSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportList)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(969, 23);
            // 
            // MMBottomPanel
            //           
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 506);
            this.MMBottomPanel.Size = new System.Drawing.Size(971, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Size = new System.Drawing.Size(971, 454);
            // 
            // pnlMultiSelection
            // 
            this.pnlMultiSelection.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.pnlMultiSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection.Controls.Add(this.cbSelectAll);
            this.pnlMultiSelection.Controls.Add(this.btnViewList);
            this.pnlMultiSelection.Controls.Add(this.btnOK);
            this.pnlMultiSelection.Controls.Add(this.txtNoofSearches);
            this.pnlMultiSelection.Controls.Add(this.lblNoofRowsSelected);
            this.pnlMultiSelection.Controls.Add(this.btnOKMultiSelection);
            this.pnlMultiSelection.Controls.Add(this.dgvSelected);
            this.pnlMultiSelection.Controls.Add(this.dgvMultiSelection);
            this.pnlMultiSelection.Controls.Add(this.txtSearch);
            this.pnlMultiSelection.Controls.Add(this.lblSearch);
            this.pnlMultiSelection.Location = new System.Drawing.Point(233, 57);
            this.pnlMultiSelection.Name = "pnlMultiSelection";
            this.pnlMultiSelection.Size = new System.Drawing.Size(637, 349);
            this.pnlMultiSelection.TabIndex = 1039;
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectAll.Location = new System.Drawing.Point(21, 25);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(81, 19);
            this.cbSelectAll.TabIndex = 1065;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.Click += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // btnViewList
            // 
            this.btnViewList.Location = new System.Drawing.Point(243, 312);
            this.btnViewList.Name = "btnViewList";
            this.btnViewList.Size = new System.Drawing.Size(69, 23);
            this.btnViewList.TabIndex = 1064;
            this.btnViewList.Text = "View";
            this.btnViewList.UseVisualStyleBackColor = true;
            this.btnViewList.Click += new System.EventHandler(this.btnViewList_Click);
            this.btnViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnViewList_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Goldenrod;
            this.btnOK.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(342, 310);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 30);
            this.btnOK.TabIndex = 1063;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtNoofSearches
            // 
            this.txtNoofSearches.Location = new System.Drawing.Point(180, 314);
            this.txtNoofSearches.Name = "txtNoofSearches";
            this.txtNoofSearches.Size = new System.Drawing.Size(57, 20);
            this.txtNoofSearches.TabIndex = 1062;
            // 
            // lblNoofRowsSelected
            // 
            this.lblNoofRowsSelected.AutoSize = true;
            this.lblNoofRowsSelected.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRowsSelected.Location = new System.Drawing.Point(18, 316);
            this.lblNoofRowsSelected.Name = "lblNoofRowsSelected";
            this.lblNoofRowsSelected.Size = new System.Drawing.Size(160, 15);
            this.lblNoofRowsSelected.TabIndex = 1061;
            this.lblNoofRowsSelected.Text = "No of Companies Selected";
            // 
            // btnOKMultiSelection
            // 
            this.btnOKMultiSelection.BackColor = System.Drawing.Color.SpringGreen;
            this.btnOKMultiSelection.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOKMultiSelection.Location = new System.Drawing.Point(542, 310);
            this.btnOKMultiSelection.Name = "btnOKMultiSelection";
            this.btnOKMultiSelection.Size = new System.Drawing.Size(68, 30);
            this.btnOKMultiSelection.TabIndex = 1060;
            this.btnOKMultiSelection.Text = "GO";
            this.btnOKMultiSelection.UseVisualStyleBackColor = false;
            this.btnOKMultiSelection.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // dgvSelected
            // 
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Location = new System.Drawing.Point(84, 82);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.Size = new System.Drawing.Size(466, 175);
            this.dgvSelected.TabIndex = 1058;
            // 
            // dgvMultiSelection
            // 
            this.dgvMultiSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMultiSelection.Location = new System.Drawing.Point(3, 48);
            this.dgvMultiSelection.Name = "dgvMultiSelection";
            this.dgvMultiSelection.Size = new System.Drawing.Size(629, 258);
            this.dgvMultiSelection.TabIndex = 1057;
            // 
            // txtSearch
            // 
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearch.Location = new System.Drawing.Point(282, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(145, 20);
            this.txtSearch.TabIndex = 1056;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(173, 9);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(107, 15);
            this.lblSearch.TabIndex = 1055;
            this.lblSearch.Text = "Search &Company";           
            // 
            // ttSchemeCompany
            // 
            this.ttSchemeCompany.ShowAlways = true;
            // 
            // dgvReportList
            // 
            this.dgvReportList.AllowUserToDeleteRows = false;
            this.dgvReportList.AllowUserToResizeColumns = false;
            this.dgvReportList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReportList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.RowHeadersVisible = false;
            this.dgvReportList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReportList.Size = new System.Drawing.Size(969, 452);
            this.dgvReportList.TabIndex = 1044;
            // 
            // UclSchemeListCompanywise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Name = "UclSchemeListCompanywise";
            this.Size = new System.Drawing.Size(971, 529);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlMultiSelection.ResumeLayout(false);
            this.pnlMultiSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
       
        private System.Windows.Forms.Panel pnlMultiSelection;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvMultiSelection;
        private System.Windows.Forms.DataGridView dgvSelected;
        private System.Windows.Forms.Button btnViewList;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtNoofSearches;
        private System.Windows.Forms.Label lblNoofRowsSelected;
        private System.Windows.Forms.Button btnOKMultiSelection;
        private System.Windows.Forms.ToolTip ttSchemeCompany;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.DataGridView dgvReportList;
    }
}
