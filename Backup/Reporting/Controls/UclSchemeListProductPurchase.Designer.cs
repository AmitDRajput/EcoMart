namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclSchemeListProductPurchase
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ViewToDate = new System.Windows.Forms.DateTimePicker();
            this.ViewFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblViewTo = new System.Windows.Forms.Label();
            this.lblViewFrom = new System.Windows.Forms.Label();
            this.pnlMultiSelection = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnViewList = new System.Windows.Forms.Button();
            this.btnOKMultiSelection = new System.Windows.Forms.Button();
            this.lblNoofRowsSelected = new System.Windows.Forms.Label();
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtNoofAccounts = new System.Windows.Forms.TextBox();
            this.dgvMultiSelection = new System.Windows.Forms.DataGridView();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.lblAccount = new System.Windows.Forms.Label();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dgvReportList = new System.Windows.Forms.DataGridView();
            this.ttSchemeProductPurchase = new System.Windows.Forms.ToolTip(this.components);
          
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlMultiSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportList)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(949, 23);
            // 
            // MMBottomPanel
            //           
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 527);
            this.MMBottomPanel.Size = new System.Drawing.Size(951, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Size = new System.Drawing.Size(951, 475);
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.ViewToDate);
            this.pnlTop.Controls.Add(this.ViewFromDate);
            this.pnlTop.Controls.Add(this.lblViewTo);
            this.pnlTop.Controls.Add(this.lblViewFrom);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(949, 33);
            this.pnlTop.TabIndex = 1044;
            // 
            // ViewToDate
            // 
            this.ViewToDate.CustomFormat = "dd/MM/yy";
            this.ViewToDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ViewToDate.Location = new System.Drawing.Point(803, 2);
            this.ViewToDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ViewToDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(122, 26);
            this.ViewToDate.TabIndex = 1044;
            this.ViewToDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.CalendarFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewFromDate.CustomFormat = "dd/MM/yy";
            this.ViewFromDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ViewFromDate.Location = new System.Drawing.Point(639, 2);
            this.ViewFromDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ViewFromDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(122, 26);
            this.ViewFromDate.TabIndex = 1043;
            this.ViewFromDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // lblViewTo
            // 
            this.lblViewTo.AutoSize = true;
            this.lblViewTo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewTo.Location = new System.Drawing.Point(777, 7);
            this.lblViewTo.Name = "lblViewTo";
            this.lblViewTo.Size = new System.Drawing.Size(22, 15);
            this.lblViewTo.TabIndex = 1041;
            this.lblViewTo.Text = "To";
            // 
            // lblViewFrom
            // 
            this.lblViewFrom.AutoSize = true;
            this.lblViewFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewFrom.Location = new System.Drawing.Point(596, 7);
            this.lblViewFrom.Name = "lblViewFrom";
            this.lblViewFrom.Size = new System.Drawing.Size(39, 15);
            this.lblViewFrom.TabIndex = 1040;
            this.lblViewFrom.Text = "From";
            // 
            // pnlMultiSelection
            // 
            this.pnlMultiSelection.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.pnlMultiSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection.Controls.Add(this.btnOK);
            this.pnlMultiSelection.Controls.Add(this.btnViewList);
            this.pnlMultiSelection.Controls.Add(this.btnOKMultiSelection);
            this.pnlMultiSelection.Controls.Add(this.lblNoofRowsSelected);
            this.pnlMultiSelection.Controls.Add(this.dgvSelected);
            this.pnlMultiSelection.Controls.Add(this.txtSearch);
            this.pnlMultiSelection.Controls.Add(this.txtNoofAccounts);
            this.pnlMultiSelection.Controls.Add(this.dgvMultiSelection);
            this.pnlMultiSelection.Controls.Add(this.cbSelectAll);
            this.pnlMultiSelection.Controls.Add(this.lblAccount);
            this.pnlMultiSelection.Controls.Add(this.ToDate);
            this.pnlMultiSelection.Controls.Add(this.lblTo);
            this.pnlMultiSelection.Controls.Add(this.FromDate);
            this.pnlMultiSelection.Controls.Add(this.lblFrom);
            this.pnlMultiSelection.Location = new System.Drawing.Point(202, 78);
            this.pnlMultiSelection.Name = "pnlMultiSelection";
            this.pnlMultiSelection.Size = new System.Drawing.Size(637, 370);
            this.pnlMultiSelection.TabIndex = 1045;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Goldenrod;
            this.btnOK.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(392, 335);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 30);
            this.btnOK.TabIndex = 1061;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnViewList
            // 
            this.btnViewList.Location = new System.Drawing.Point(233, 339);
            this.btnViewList.Name = "btnViewList";
            this.btnViewList.Size = new System.Drawing.Size(69, 23);
            this.btnViewList.TabIndex = 1060;
            this.btnViewList.Text = "View";
            this.btnViewList.UseVisualStyleBackColor = true;
            this.btnViewList.Click += new System.EventHandler(this.btnViewList_Click);
            // 
            // btnOKMultiSelection
            // 
            this.btnOKMultiSelection.BackColor = System.Drawing.Color.SpringGreen;
            this.btnOKMultiSelection.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOKMultiSelection.Location = new System.Drawing.Point(489, 335);
            this.btnOKMultiSelection.Name = "btnOKMultiSelection";
            this.btnOKMultiSelection.Size = new System.Drawing.Size(75, 30);
            this.btnOKMultiSelection.TabIndex = 1059;
            this.btnOKMultiSelection.Text = "GO";
            this.btnOKMultiSelection.UseVisualStyleBackColor = false;
            this.btnOKMultiSelection.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // lblNoofRowsSelected
            // 
            this.lblNoofRowsSelected.AutoSize = true;
            this.lblNoofRowsSelected.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRowsSelected.Location = new System.Drawing.Point(12, 343);
            this.lblNoofRowsSelected.Name = "lblNoofRowsSelected";
            this.lblNoofRowsSelected.Size = new System.Drawing.Size(148, 15);
            this.lblNoofRowsSelected.TabIndex = 1058;
            this.lblNoofRowsSelected.Text = "No of Products Selected";
            // 
            // dgvSelected
            // 
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Location = new System.Drawing.Point(104, 117);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.Size = new System.Drawing.Size(466, 175);
            this.dgvSelected.TabIndex = 1055;
            // 
            // txtSearch
            // 
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearch.Location = new System.Drawing.Point(229, 60);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(145, 20);
            this.txtSearch.TabIndex = 1054;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // txtNoofAccounts
            // 
            this.txtNoofAccounts.Location = new System.Drawing.Point(166, 341);
            this.txtNoofAccounts.Name = "txtNoofAccounts";
            this.txtNoofAccounts.Size = new System.Drawing.Size(57, 20);
            this.txtNoofAccounts.TabIndex = 1052;
            // 
            // dgvMultiSelection
            // 
            this.dgvMultiSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMultiSelection.Location = new System.Drawing.Point(3, 86);
            this.dgvMultiSelection.Name = "dgvMultiSelection";
            this.dgvMultiSelection.Size = new System.Drawing.Size(629, 247);
            this.dgvMultiSelection.TabIndex = 37;
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectAll.Location = new System.Drawing.Point(22, 64);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(81, 19);
            this.cbSelectAll.TabIndex = 36;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.TextChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.Location = new System.Drawing.Point(127, 63);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(99, 15);
            this.lblAccount.TabIndex = 1044;
            this.lblAccount.Text = "Search &Product";
            // 
            // ToDate
            // 
            this.ToDate.CustomFormat = "dd/MM/yy";
            this.ToDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(229, 3);
            this.ToDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ToDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(122, 26);
            this.ToDate.TabIndex = 1043;
            this.ToDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(201, 8);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(22, 15);
            this.lblTo.TabIndex = 1041;
            this.lblTo.Text = "To";
            // 
            // FromDate
            // 
            this.FromDate.CustomFormat = "dd/MM/yy";
            this.FromDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(54, 3);
            this.FromDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.FromDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(122, 26);
            this.FromDate.TabIndex = 1042;
            this.FromDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(12, 9);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(39, 15);
            this.lblFrom.TabIndex = 1040;
            this.lblFrom.Text = "From";
            // 
            // dgvReportList
            // 
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
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.RowHeadersVisible = false;
            this.dgvReportList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReportList.Size = new System.Drawing.Size(949, 440);
            this.dgvReportList.TabIndex = 1047;
            // 
            // ttSchemeProductPurchase
            // 
            this.ttSchemeProductPurchase.ShowAlways = true;            
            // 
            // UclSchemeListProductPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclSchemeListProductPurchase";
            this.Size = new System.Drawing.Size(951, 550);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMultiSelection.ResumeLayout(false);
            this.pnlMultiSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.DateTimePicker ViewToDate;
        private System.Windows.Forms.DateTimePicker ViewFromDate;
        private System.Windows.Forms.Label lblViewTo;
        private System.Windows.Forms.Label lblViewFrom;
        private System.Windows.Forms.Panel pnlMultiSelection;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnViewList;
        private System.Windows.Forms.Button btnOKMultiSelection;
        private System.Windows.Forms.Label lblNoofRowsSelected;
        private System.Windows.Forms.DataGridView dgvSelected;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtNoofAccounts;
        private System.Windows.Forms.DataGridView dgvMultiSelection;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DataGridView dgvReportList;
        private System.Windows.Forms.ToolTip ttSchemeProductPurchase;       
    }
}
