namespace EcoMart.Reporting.Controls
{
    partial class UclChallanProductList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclChallanProductList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.rbtnPending = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnAll = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.mPlbl5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.mcbParty = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.psLabel4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewAmount = new System.Windows.Forms.TextBox();
            this.lbltxtProduct = new System.Windows.Forms.Label();
            this.txtViewtext = new System.Windows.Forms.TextBox();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.DataGridViewContent = new PharmaSYSPlus.CommonLibrary.MReportGridViewBase(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.dgvReportList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewContent)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(917, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 377);
            this.MMBottomPanel.Size = new System.Drawing.Size(919, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Size = new System.Drawing.Size(919, 325);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlTop, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.rbtnPending);
            this.pnlMultiSelection1.Controls.Add(this.rbtnAll);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl5);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl3);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.txtAmount);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.mcbParty);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(300, 139);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(480, 144);
            this.pnlMultiSelection1.TabIndex = 1045;
            // 
            // rbtnPending
            // 
            this.rbtnPending.AutoSize = true;
            this.rbtnPending.BackColor = System.Drawing.Color.White;
            this.rbtnPending.Location = new System.Drawing.Point(150, 79);
            this.rbtnPending.Name = "rbtnPending";
            this.rbtnPending.Size = new System.Drawing.Size(77, 21);
            this.rbtnPending.TabIndex = 12;
            this.rbtnPending.Text = "Pending";
            this.rbtnPending.UseVisualStyleBackColor = false;
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.BackColor = System.Drawing.Color.White;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new System.Drawing.Point(90, 79);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(43, 21);
            this.rbtnAll.TabIndex = 11;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "All";
            this.rbtnAll.UseVisualStyleBackColor = false;
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(226, 13);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(22, 16);
            this.mPlbl5.TabIndex = 2;
            this.mPlbl5.Text = "To";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(17, 115);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(57, 16);
            this.mPlbl3.TabIndex = 6;
            this.mPlbl3.Text = "Amount";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(17, 48);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(41, 16);
            this.mPlbl2.TabIndex = 4;
            this.mPlbl2.Text = "Party";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(17, 13);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(41, 16);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "From";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(90, 112);
            this.txtAmount.MaxLength = 15;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(86, 23);
            this.txtAmount.TabIndex = 7;
            this.txtAmount.Tag = "0.00";
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(259, 9);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 24);
            this.toDate1.TabIndex = 3;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(90, 9);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 1;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(410, 3);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 10;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // mcbParty
            // 
            this.mcbParty.ColumnWidth = null;
            this.mcbParty.DataSource = null;
            this.mcbParty.DisplayColumnNo = 1;
            this.mcbParty.DropDownHeight = 200;
            this.mcbParty.Location = new System.Drawing.Point(90, 45);
            this.mcbParty.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbParty.Name = "mcbParty";
            this.mcbParty.SelectedID = null;
            this.mcbParty.ShowNew = false;
            this.mcbParty.Size = new System.Drawing.Size(299, 22);
            this.mcbParty.SourceDataString = null;
            this.mcbParty.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbParty.TabIndex = 5;
            this.mcbParty.UserControlToShow = null;
            this.mcbParty.ValueColumnNo = 0;
            this.mcbParty.EnterKeyPressed += new System.EventHandler(this.mcbParty_EnterKeyPressed);
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.psLabel4);
            this.pnlTop.Controls.Add(this.psLabel3);
            this.pnlTop.Controls.Add(this.ViewToDate);
            this.pnlTop.Controls.Add(this.ViewFromDate);
            this.pnlTop.Controls.Add(this.psLabel2);
            this.pnlTop.Controls.Add(this.txtViewAmount);
            this.pnlTop.Controls.Add(this.lbltxtProduct);
            this.pnlTop.Controls.Add(this.txtViewtext);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(917, 33);
            this.pnlTop.TabIndex = 1046;
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(514, 9);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(41, 16);
            this.psLabel4.TabIndex = 1084;
            this.psLabel4.Text = "From";
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(325, 9);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(57, 16);
            this.psLabel3.TabIndex = 1083;
            this.psLabel3.Text = "Amount";
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(716, 3);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1082;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(568, 3);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1081;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(681, 9);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(22, 16);
            this.psLabel2.TabIndex = 1080;
            this.psLabel2.Text = "To";
            // 
            // txtViewAmount
            // 
            this.txtViewAmount.Enabled = false;
            this.txtViewAmount.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewAmount.Location = new System.Drawing.Point(395, 4);
            this.txtViewAmount.Name = "txtViewAmount";
            this.txtViewAmount.Size = new System.Drawing.Size(106, 22);
            this.txtViewAmount.TabIndex = 1049;
            // 
            // lbltxtProduct
            // 
            this.lbltxtProduct.AutoSize = true;
            this.lbltxtProduct.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtProduct.Location = new System.Drawing.Point(9, 8);
            this.lbltxtProduct.Name = "lbltxtProduct";
            this.lbltxtProduct.Size = new System.Drawing.Size(40, 15);
            this.lbltxtProduct.TabIndex = 1045;
            this.lbltxtProduct.Text = "Party";
            // 
            // txtViewtext
            // 
            this.txtViewtext.Enabled = false;
            this.txtViewtext.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewtext.Location = new System.Drawing.Point(62, 4);
            this.txtViewtext.Name = "txtViewtext";
            this.txtViewtext.Size = new System.Drawing.Size(250, 22);
            this.txtViewtext.TabIndex = 1044;
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = false;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.Controls.Add(this.DataGridViewContent);
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.FreezeLastRow = true;
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(917, 290);
            this.dgvReportList.TabIndex = 1047;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // DataGridViewContent
            // 
            this.DataGridViewContent.AllowUserToAddRows = false;
            this.DataGridViewContent.AllowUserToDeleteRows = false;
            this.DataGridViewContent.AllowUserToOrderColumns = true;
            this.DataGridViewContent.AllowUserToResizeRows = false;
            this.DataGridViewContent.BackgroundColor = System.Drawing.Color.RosyBrown;
            this.DataGridViewContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewContent.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridViewContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridViewContent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DataGridViewContent.FreezeLastRow = true;
            this.DataGridViewContent.Location = new System.Drawing.Point(0, 0);
            this.DataGridViewContent.MultiSelect = false;
            this.DataGridViewContent.Name = "DataGridViewContent";
            this.DataGridViewContent.RowHeadersVisible = false;
            this.DataGridViewContent.RowHeadersWidth = 25;
            this.DataGridViewContent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.DataGridViewContent.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewContent.Size = new System.Drawing.Size(915, 288);
            this.DataGridViewContent.TabIndex = 5;
            // 
            // UclChallanProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclChallanProductList";
            this.Size = new System.Drawing.Size(919, 400);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.dgvReportList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmount;
        private InterfaceLayer.CommonControls.ToDate toDate1;
        private InterfaceLayer.CommonControls.FromDate fromDate1;
        private InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private InterfaceLayer.CommonControls.PSComboBoxNew mcbParty;
        private System.Windows.Forms.Panel pnlTop;
        private InterfaceLayer.CommonControls.PSLabel psLabel4;
        private InterfaceLayer.CommonControls.PSLabel psLabel3;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private InterfaceLayer.CommonControls.PSLabel psLabel2;
        private System.Windows.Forms.TextBox txtViewAmount;
        private System.Windows.Forms.Label lbltxtProduct;
        private System.Windows.Forms.TextBox txtViewtext;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private PharmaSYSPlus.CommonLibrary.MReportGridViewBase DataGridViewContent;
        private InterfaceLayer.CommonControls.PSRadioButton rbtnPending;
        private InterfaceLayer.CommonControls.PSRadioButton rbtnAll;
    }
}
