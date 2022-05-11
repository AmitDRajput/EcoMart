using PharmaSYSPlus.CommonLibrary;
namespace EcoMart.InterfaceLayer
{
    partial class UclBulkProduct
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BulkProductGrid = new System.Windows.Forms.DataGridView();
            this.PanelBulkProdDetail2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.RowCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PanelBulkProdDetail1 = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.labeltype = new System.Windows.Forms.Label();
            this.cmbCharacterWise = new System.Windows.Forms.ComboBox();
            this.lblCharacterWise = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.cbCompany = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.cbGenericCat = new PaperlessPharmaRetail.InterfaceLayer.CommonControls.PSGenericCategoryComboBoxNew();
            this.ttproduct = new System.Windows.Forms.ToolTip(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BulkProductGrid)).BeginInit();
            this.PanelBulkProdDetail2.SuspendLayout();
            this.PanelBulkProdDetail1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Margin = new System.Windows.Forms.Padding(5);
            this.headerLabel1.Size = new System.Drawing.Size(1247, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 583);
            this.MMBottomPanel.Margin = new System.Windows.Forms.Padding(4);
            this.MMBottomPanel.Size = new System.Drawing.Size(1249, 20);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel2);
            this.MMMainPanel.Controls.Add(this.label1);
            this.MMMainPanel.Margin = new System.Windows.Forms.Padding(4);
            this.MMMainPanel.Size = new System.Drawing.Size(1249, 520);
            this.MMMainPanel.Controls.SetChildIndex(this.label1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.panel2, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 578);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 38;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.BulkProductGrid);
            this.panel2.Controls.Add(this.PanelBulkProdDetail2);
            this.panel2.Controls.Add(this.PanelBulkProdDetail1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1247, 518);
            this.panel2.TabIndex = 0;
            // 
            // BulkProductGrid
            // 
            this.BulkProductGrid.AllowUserToAddRows = false;
            this.BulkProductGrid.AllowUserToDeleteRows = false;
            this.BulkProductGrid.AllowUserToOrderColumns = true;
            this.BulkProductGrid.AllowUserToResizeRows = false;
            this.BulkProductGrid.BackgroundColor = System.Drawing.Color.DarkGray;
            this.BulkProductGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.BulkProductGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.BulkProductGrid.ColumnHeadersHeight = 28;
            this.BulkProductGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.BulkProductGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.BulkProductGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BulkProductGrid.Location = new System.Drawing.Point(0, 109);
            this.BulkProductGrid.Margin = new System.Windows.Forms.Padding(4);
            this.BulkProductGrid.MinimumSize = new System.Drawing.Size(580, 194);
            this.BulkProductGrid.MultiSelect = false;
            this.BulkProductGrid.Name = "BulkProductGrid";
            this.BulkProductGrid.RowHeadersWidth = 26;
            this.BulkProductGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BulkProductGrid.Size = new System.Drawing.Size(1245, 349);
            this.BulkProductGrid.TabIndex = 1049;
            this.BulkProductGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.BulkProductGrid_CellEndEdit);
            this.BulkProductGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.BulkProductGrid_CellFormatting);
            this.BulkProductGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.BulkProductGrid_CellValidating);
            this.BulkProductGrid.CurrentCellDirtyStateChanged += new System.EventHandler(this.BulkProductGrid_CurrentCellDirtyStateChanged);
            this.BulkProductGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.BulkProductGrid_DataError);
            this.BulkProductGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.BulkProductGrid_EditingControlShowing);
            this.BulkProductGrid.Scroll += new System.Windows.Forms.ScrollEventHandler(this.BulkProductGrid_Scroll);
            this.BulkProductGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BulkProductGrid_KeyDown);
            // 
            // PanelBulkProdDetail2
            // 
            this.PanelBulkProdDetail2.Controls.Add(this.label4);
            this.PanelBulkProdDetail2.Controls.Add(this.RowCount);
            this.PanelBulkProdDetail2.Controls.Add(this.label6);
            this.PanelBulkProdDetail2.Controls.Add(this.panel1);
            this.PanelBulkProdDetail2.Controls.Add(this.label3);
            this.PanelBulkProdDetail2.Controls.Add(this.label5);
            this.PanelBulkProdDetail2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelBulkProdDetail2.Location = new System.Drawing.Point(0, 458);
            this.PanelBulkProdDetail2.Name = "PanelBulkProdDetail2";
            this.PanelBulkProdDetail2.Size = new System.Drawing.Size(1245, 58);
            this.PanelBulkProdDetail2.TabIndex = 1048;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1003, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 1045;
            this.label4.Text = "Save : Ctrl + S";
            // 
            // RowCount
            // 
            this.RowCount.AutoSize = true;
            this.RowCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RowCount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.RowCount.Location = new System.Drawing.Point(124, 10);
            this.RowCount.Name = "RowCount";
            this.RowCount.Size = new System.Drawing.Size(14, 13);
            this.RowCount.TabIndex = 1044;
            this.RowCount.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkRed;
            this.label6.Location = new System.Drawing.Point(491, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(220, 13);
            this.label6.TabIndex = 1043;
            this.label6.Text = "Be Careful While Doing These Things";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Violet;
            this.panel1.Location = new System.Drawing.Point(27, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(35, 18);
            this.panel1.TabIndex = 1040;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(67, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 13);
            this.label3.TabIndex = 1041;
            this.label3.Text = "Product Is In Transaction";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label5.Location = new System.Drawing.Point(24, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 1042;
            this.label5.Text = "Records Found : ";
            // 
            // PanelBulkProdDetail1
            // 
            this.PanelBulkProdDetail1.Controls.Add(this.radioButton3);
            this.PanelBulkProdDetail1.Controls.Add(this.radioButton2);
            this.PanelBulkProdDetail1.Controls.Add(this.radioButton1);
            this.PanelBulkProdDetail1.Controls.Add(this.label7);
            this.PanelBulkProdDetail1.Controls.Add(this.labeltype);
            this.PanelBulkProdDetail1.Controls.Add(this.cmbCharacterWise);
            this.PanelBulkProdDetail1.Controls.Add(this.lblCharacterWise);
            this.PanelBulkProdDetail1.Controls.Add(this.txtType);
            this.PanelBulkProdDetail1.Controls.Add(this.cbCompany);
            this.PanelBulkProdDetail1.Controls.Add(this.cbGenericCat);
            this.PanelBulkProdDetail1.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelBulkProdDetail1.Location = new System.Drawing.Point(0, 0);
            this.PanelBulkProdDetail1.Name = "PanelBulkProdDetail1";
            this.PanelBulkProdDetail1.Size = new System.Drawing.Size(1245, 109);
            this.PanelBulkProdDetail1.TabIndex = 1047;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Location = new System.Drawing.Point(489, 16);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(190, 19);
            this.radioButton3.TabIndex = 1059;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Non Transaction Products";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioButton3_KeyDown);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(301, 16);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(160, 19);
            this.radioButton2.TabIndex = 1058;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Transaction Products";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioButton2_KeyDown);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(137, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(101, 19);
            this.radioButton1.TabIndex = 1057;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "All Products";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioButton1_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(49, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 15);
            this.label7.TabIndex = 1053;
            this.label7.Text = "Search In";
            // 
            // labeltype
            // 
            this.labeltype.AutoSize = true;
            this.labeltype.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeltype.Location = new System.Drawing.Point(26, 81);
            this.labeltype.Name = "labeltype";
            this.labeltype.Size = new System.Drawing.Size(37, 15);
            this.labeltype.TabIndex = 1048;
            this.labeltype.Text = "Type";
            this.labeltype.Visible = false;
            // 
            // cmbCharacterWise
            // 
            this.cmbCharacterWise.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCharacterWise.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCharacterWise.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCharacterWise.FormattingEnabled = true;
            this.cmbCharacterWise.Items.AddRange(new object[] {
            "PRODUCT WISE",
            "COMPANY WISE",
            "CONTENT WISE"});
            this.cmbCharacterWise.Location = new System.Drawing.Point(136, 46);
            this.cmbCharacterWise.Name = "cmbCharacterWise";
            this.cmbCharacterWise.Size = new System.Drawing.Size(183, 24);
            this.cmbCharacterWise.TabIndex = 1039;
            this.cmbCharacterWise.SelectedIndexChanged += new System.EventHandler(this.cmbCharacterWise_SelectedIndexChanged);
            this.cmbCharacterWise.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCharacterWise_KeyDown);
            // 
            // lblCharacterWise
            // 
            this.lblCharacterWise.AutoSize = true;
            this.lblCharacterWise.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharacterWise.Location = new System.Drawing.Point(80, 49);
            this.lblCharacterWise.Name = "lblCharacterWise";
            this.lblCharacterWise.Size = new System.Drawing.Size(37, 15);
            this.lblCharacterWise.TabIndex = 1033;
            this.lblCharacterWise.Text = "Type";
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.Location = new System.Drawing.Point(136, 79);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(406, 21);
            this.txtType.TabIndex = 1050;
            this.txtType.Visible = false;
            this.txtType.TextChanged += new System.EventHandler(this.txtType_TextChanged);
            this.txtType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtType_KeyDown);
            // 
            // cbCompany
            // 
            this.cbCompany.ColumnWidth = null;
            this.cbCompany.DataSource = null;
            this.cbCompany.DisplayColumnNo = 1;
            this.cbCompany.DropDownHeight = 200;
            this.cbCompany.Location = new System.Drawing.Point(137, 79);
            this.cbCompany.Margin = new System.Windows.Forms.Padding(4);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.SelectedID = null;
            this.cbCompany.ShowNew = true;
            this.cbCompany.Size = new System.Drawing.Size(459, 22);
            this.cbCompany.SourceDataString = null;
            this.cbCompany.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.cbCompany.TabIndex = 1051;
            this.cbCompany.UserControlToShow = null;
            this.cbCompany.ValueColumnNo = 0;
            this.cbCompany.Visible = false;
            this.cbCompany.EnterKeyPressed += new System.EventHandler(this.cbCompany_EnterKeyPressed);
            this.cbCompany.UpArrowPressed += new System.EventHandler(this.cbCompany_UpArrowPressed);
            // 
            // cbGenericCat
            // 
            this.cbGenericCat.ColumnWidth = null;
            this.cbGenericCat.DataSource = null;
            this.cbGenericCat.DisplayColumnNo = 1;
            this.cbGenericCat.DropDownHeight = 200;
            this.cbGenericCat.Location = new System.Drawing.Point(135, 78);
            this.cbGenericCat.Margin = new System.Windows.Forms.Padding(2);
            this.cbGenericCat.Name = "cbGenericCat";
            this.cbGenericCat.SelectedID = null;
            this.cbGenericCat.ShowNew = true;
            this.cbGenericCat.Size = new System.Drawing.Size(576, 23);
            this.cbGenericCat.SourceDataString = null;
            this.cbGenericCat.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.cbGenericCat.TabIndex = 1052;
            this.cbGenericCat.UserControlToShow = null;
            this.cbGenericCat.ValueColumnNo = 0;
            this.cbGenericCat.Visible = false;
            this.cbGenericCat.EnterKeyPressed += new System.EventHandler(this.cbGenericCat_EnterKeyPressed);
            this.cbGenericCat.UpArrowPressed += new System.EventHandler(this.cbGenericCat_UpArrowPressed);
            // 
            // ttproduct
            // 
            this.ttproduct.AutomaticDelay = 20;
            this.ttproduct.AutoPopDelay = 5000;
            this.ttproduct.InitialDelay = 20;
            this.ttproduct.ReshowDelay = 4;
            this.ttproduct.ShowAlways = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(20, 2);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(2, 16);
            this.lblMessage.TabIndex = 1011;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(241, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 14);
            this.label2.TabIndex = 50;
            this.label2.Text = "*";
            // 
            // UclBulkProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UclBulkProduct";
            this.Size = new System.Drawing.Size(1249, 603);
            this.Load += new System.EventHandler(this.UclBulkProduct_Load);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BulkProductGrid)).EndInit();
            this.PanelBulkProdDetail2.ResumeLayout(false);
            this.PanelBulkProdDetail2.PerformLayout();
            this.PanelBulkProdDetail1.ResumeLayout(false);
            this.PanelBulkProdDetail1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip ttproduct;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCharacterWise;
        private System.Windows.Forms.ComboBox cmbCharacterWise;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel PanelBulkProdDetail1;
        private System.Windows.Forms.Panel PanelBulkProdDetail2;
        private System.Windows.Forms.DataGridView BulkProductGrid;
        private System.Windows.Forms.Label labeltype;
        private System.Windows.Forms.TextBox txtType;
        private CommonControls.PSComboBoxNew cbCompany;
        private PaperlessPharmaRetail.InterfaceLayer.CommonControls.PSGenericCategoryComboBoxNew cbGenericCat;
        private System.Windows.Forms.Label RowCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}
