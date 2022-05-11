namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclListProductBySelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclListProductBySelection));
            this.txtVAT = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnVAT = new System.Windows.Forms.RadioButton();
            this.rbtnShelf = new System.Windows.Forms.RadioButton();
            this.rbtnCompany = new System.Windows.Forms.RadioButton();
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.dgvMultiSelection = new System.Windows.Forms.DataGridView();
            this.btnViewList = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtNoofSearches = new System.Windows.Forms.TextBox();
            this.lblNoofRowsSelected = new System.Windows.Forms.Label();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.lblAccount = new System.Windows.Forms.Label();
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(967, 23);
            // 
            // MMBottomPanel
            //            
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 640);
            this.MMBottomPanel.Size = new System.Drawing.Size(969, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel2);
            this.MMMainPanel.Size = new System.Drawing.Size(969, 588);
            // 
            // txtVAT
            // 
            this.txtVAT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVAT.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVAT.Location = new System.Drawing.Point(329, 12);
            this.txtVAT.MaxLength = 15;
            this.txtVAT.Name = "txtVAT";
            this.txtVAT.Size = new System.Drawing.Size(54, 23);
            this.txtVAT.TabIndex = 52;
            this.txtVAT.Tag = "0.00";
            this.txtVAT.Text = "0.00";
            this.txtVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(276, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 51;
            this.label3.Text = "VAT %";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlMultiSelection1);
            this.panel2.Controls.Add(this.dgvReportList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(967, 586);
            this.panel2.TabIndex = 35;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.groupBox1);
            this.pnlMultiSelection1.Controls.Add(this.dgvSelected);
            this.pnlMultiSelection1.Controls.Add(this.dgvMultiSelection);
            this.pnlMultiSelection1.Controls.Add(this.btnViewList);
            this.pnlMultiSelection1.Controls.Add(this.txtSearch);
            this.pnlMultiSelection1.Controls.Add(this.txtNoofSearches);
            this.pnlMultiSelection1.Controls.Add(this.lblNoofRowsSelected);
            this.pnlMultiSelection1.Controls.Add(this.cbSelectAll);
            this.pnlMultiSelection1.Controls.Add(this.lblAccount);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.label3);
            this.pnlMultiSelection1.Controls.Add(this.txtVAT);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(254, 50);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(482, 319);
            this.pnlMultiSelection1.TabIndex = 1033;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnVAT);
            this.groupBox1.Controls.Add(this.rbtnShelf);
            this.groupBox1.Controls.Add(this.rbtnCompany);
            this.groupBox1.Location = new System.Drawing.Point(31, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 32);
            this.groupBox1.TabIndex = 1070;
            this.groupBox1.TabStop = false;
            // 
            // rbtnVAT
            // 
            this.rbtnVAT.AutoSize = true;
            this.rbtnVAT.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnVAT.Location = new System.Drawing.Point(172, 12);
            this.rbtnVAT.Name = "rbtnVAT";
            this.rbtnVAT.Size = new System.Drawing.Size(64, 17);
            this.rbtnVAT.TabIndex = 2;
            this.rbtnVAT.TabStop = true;
            this.rbtnVAT.Text = "VAT%";
            this.rbtnVAT.UseVisualStyleBackColor = true;
            // 
            // rbtnShelf
            // 
            this.rbtnShelf.AutoSize = true;
            this.rbtnShelf.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnShelf.Location = new System.Drawing.Point(105, 12);
            this.rbtnShelf.Name = "rbtnShelf";
            this.rbtnShelf.Size = new System.Drawing.Size(58, 17);
            this.rbtnShelf.TabIndex = 1;
            this.rbtnShelf.TabStop = true;
            this.rbtnShelf.Text = "Shelf";
            this.rbtnShelf.UseVisualStyleBackColor = true;
            // 
            // rbtnCompany
            // 
            this.rbtnCompany.AutoSize = true;
            this.rbtnCompany.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnCompany.Location = new System.Drawing.Point(11, 12);
            this.rbtnCompany.Name = "rbtnCompany";
            this.rbtnCompany.Size = new System.Drawing.Size(85, 17);
            this.rbtnCompany.TabIndex = 0;
            this.rbtnCompany.TabStop = true;
            this.rbtnCompany.Text = "Company";
            this.rbtnCompany.UseVisualStyleBackColor = true;
            this.rbtnCompany.CheckedChanged += new System.EventHandler(this.rbtnCompany_CheckedChanged);
            // 
            // dgvSelected
            // 
            this.dgvSelected.AllowUserToAddRows = false;
            this.dgvSelected.AllowUserToResizeColumns = false;
            this.dgvSelected.AllowUserToResizeRows = false;
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Location = new System.Drawing.Point(77, 127);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.Size = new System.Drawing.Size(271, 112);
            this.dgvSelected.TabIndex = 1069;
            // 
            // dgvMultiSelection
            // 
            this.dgvMultiSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMultiSelection.Location = new System.Drawing.Point(42, 82);
            this.dgvMultiSelection.Name = "dgvMultiSelection";
            this.dgvMultiSelection.Size = new System.Drawing.Size(367, 198);
            this.dgvMultiSelection.TabIndex = 1068;
            this.dgvMultiSelection.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellValueChanged);
            this.dgvMultiSelection.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultiSelection_CellContentClick);
            // 
            // btnViewList
            // 
            this.btnViewList.Location = new System.Drawing.Point(241, 286);
            this.btnViewList.Name = "btnViewList";
            this.btnViewList.Size = new System.Drawing.Size(69, 23);
            this.btnViewList.TabIndex = 1067;
            this.btnViewList.Text = "View";
            this.btnViewList.UseVisualStyleBackColor = true;
            this.btnViewList.Click += new System.EventHandler(this.btnViewList_Click);
            this.btnViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnViewList_KeyDown);
            // 
            // txtSearch
            // 
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearch.Location = new System.Drawing.Point(238, 54);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(145, 20);
            this.txtSearch.TabIndex = 1066;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // txtNoofSearches
            // 
            this.txtNoofSearches.Location = new System.Drawing.Point(178, 288);
            this.txtNoofSearches.Name = "txtNoofSearches";
            this.txtNoofSearches.Size = new System.Drawing.Size(57, 20);
            this.txtNoofSearches.TabIndex = 1065;
            // 
            // lblNoofRowsSelected
            // 
            this.lblNoofRowsSelected.AutoSize = true;
            this.lblNoofRowsSelected.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRowsSelected.Location = new System.Drawing.Point(24, 290);
            this.lblNoofRowsSelected.Name = "lblNoofRowsSelected";
            this.lblNoofRowsSelected.Size = new System.Drawing.Size(148, 15);
            this.lblNoofRowsSelected.TabIndex = 1064;
            this.lblNoofRowsSelected.Text = "No of Products Selected";
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectAll.Location = new System.Drawing.Point(31, 58);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(81, 19);
            this.cbSelectAll.TabIndex = 1062;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.Location = new System.Drawing.Point(136, 59);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(99, 15);
            this.lblAccount.TabIndex = 1063;
            this.lblAccount.Text = "Search &Product";
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(414, 3);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 53;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // dgvReportList
            // 
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(967, 586);
            this.dgvReportList.TabIndex = 1034;           
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // UclListProductBySelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclListProductBySelection";
            this.Size = new System.Drawing.Size(969, 663);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSelection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtVAT;
        private System.Windows.Forms.Label label3;        
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private System.Windows.Forms.DataGridView dgvSelected;
        private System.Windows.Forms.DataGridView dgvMultiSelection;
        private System.Windows.Forms.Button btnViewList;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtNoofSearches;
        private System.Windows.Forms.Label lblNoofRowsSelected;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.ToolTip ttToolTip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnShelf;
        private System.Windows.Forms.RadioButton rbtnCompany;
        private System.Windows.Forms.RadioButton rbtnVAT;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        //   private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.MComboBoxNew mcbShelfCode;
    }
}
