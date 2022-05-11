namespace EcoMart.Reporting.Controls
{
    partial class UclDebitNoteList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclDebitNoteList));
            this.txtAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.cbOnlyPending = new EcoMart.InterfaceLayer.CommonControls.PSCheckBox();
            this.mcbType = new System.Windows.Forms.ComboBox();
            this.mPlbl5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
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
            this.txtViewType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbltxtProduct = new System.Windows.Forms.Label();
            this.txtViewtext = new System.Windows.Forms.TextBox();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(977, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 554);
            this.MMBottomPanel.Size = new System.Drawing.Size(979, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Size = new System.Drawing.Size(979, 502);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlTop, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(77, 74);
            this.txtAmount.MaxLength = 15;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(86, 23);
            this.txtAmount.TabIndex = 7;
            this.txtAmount.Tag = "0.00";
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.cbOnlyPending);
            this.pnlMultiSelection1.Controls.Add(this.mcbType);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl5);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl4);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl3);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.txtAmount);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.mcbParty);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(300, 100);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(480, 144);
            this.pnlMultiSelection1.TabIndex = 0;
            // 
            // cbOnlyPending
            // 
            this.cbOnlyPending.AutoSize = true;
            this.cbOnlyPending.Location = new System.Drawing.Point(381, 77);
            this.cbOnlyPending.Name = "cbOnlyPending";
            this.cbOnlyPending.Size = new System.Drawing.Size(89, 17);
            this.cbOnlyPending.TabIndex = 11;
            this.cbOnlyPending.Text = "Only Pending";
            this.cbOnlyPending.UseVisualStyleBackColor = true;
            // 
            // mcbType
            // 
            this.mcbType.FormattingEnabled = true;
            this.mcbType.Items.AddRange(new object[] {
            "DNS",
            "DNA"});
            this.mcbType.Location = new System.Drawing.Point(76, 108);
            this.mcbType.Name = "mcbType";
            this.mcbType.Size = new System.Drawing.Size(113, 21);
            this.mcbType.TabIndex = 9;
            this.mcbType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcbType_KeyDown);
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(217, 11);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(22, 17);
            this.mPlbl5.TabIndex = 2;
            this.mPlbl5.Text = "To";
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(27, 108);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(38, 17);
            this.mPlbl4.TabIndex = 8;
            this.mPlbl4.Text = "Type";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(4, 73);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(57, 17);
            this.mPlbl3.TabIndex = 6;
            this.mPlbl3.Text = "Amount";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(23, 42);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(41, 17);
            this.mPlbl2.TabIndex = 4;
            this.mPlbl2.Text = "Party";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(24, 11);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(41, 17);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "From";
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
            this.fromDate1.Location = new System.Drawing.Point(76, 9);
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
            this.mcbParty.Location = new System.Drawing.Point(78, 42);
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
            this.pnlTop.Controls.Add(this.txtViewType);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.lbltxtProduct);
            this.pnlTop.Controls.Add(this.txtViewtext);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(977, 33);
            this.pnlTop.TabIndex = 1044;
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(656, 10);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(41, 17);
            this.psLabel4.TabIndex = 1084;
            this.psLabel4.Text = "From";
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(441, 10);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(57, 17);
            this.psLabel3.TabIndex = 1083;
            this.psLabel3.Text = "Amount";
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(852, 4);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1082;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(703, 4);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1081;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(818, 10);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(22, 17);
            this.psLabel2.TabIndex = 1080;
            this.psLabel2.Text = "To";
            // 
            // txtViewAmount
            // 
            this.txtViewAmount.Enabled = false;
            this.txtViewAmount.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewAmount.Location = new System.Drawing.Point(516, 5);
            this.txtViewAmount.Name = "txtViewAmount";
            this.txtViewAmount.Size = new System.Drawing.Size(106, 22);
            this.txtViewAmount.TabIndex = 1049;
            // 
            // txtViewType
            // 
            this.txtViewType.Enabled = false;
            this.txtViewType.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewType.Location = new System.Drawing.Point(373, 5);
            this.txtViewType.Name = "txtViewType";
            this.txtViewType.Size = new System.Drawing.Size(51, 22);
            this.txtViewType.TabIndex = 1047;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(329, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 1046;
            this.label1.Text = "Type";
            // 
            // lbltxtProduct
            // 
            this.lbltxtProduct.AutoSize = true;
            this.lbltxtProduct.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtProduct.Location = new System.Drawing.Point(13, 9);
            this.lbltxtProduct.Name = "lbltxtProduct";
            this.lbltxtProduct.Size = new System.Drawing.Size(41, 16);
            this.lbltxtProduct.TabIndex = 1045;
            this.lbltxtProduct.Text = "Party";
            // 
            // txtViewtext
            // 
            this.txtViewtext.Enabled = false;
            this.txtViewtext.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewtext.Location = new System.Drawing.Point(58, 5);
            this.txtViewtext.Name = "txtViewtext";
            this.txtViewtext.Size = new System.Drawing.Size(250, 22);
            this.txtViewtext.TabIndex = 1044;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(640, 3);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(48, 19);
            this.psLabel1.TabIndex = 1079;
            this.psLabel1.Text = "From";
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = false;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.FreezeLastRow = true;
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(977, 467);
            this.dgvReportList.TabIndex = 1045;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            this.dgvReportList.TabKeyPressed += new System.EventHandler(this.dgvReportList_TabKeyPressed);
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // UclDebitNoteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Name = "UclDebitNoteList";
            this.Size = new System.Drawing.Size(979, 577);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

       
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmount;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.ToDate toDate1;
        private EcoMart.InterfaceLayer.CommonControls.FromDate fromDate1;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbParty;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private System.Windows.Forms.ComboBox mcbType;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TextBox txtViewType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbltxtProduct;
        private System.Windows.Forms.TextBox txtViewtext;
        private System.Windows.Forms.TextBox txtViewAmount;
        private System.Windows.Forms.ToolTip ttToolTip;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private InterfaceLayer.CommonControls.PSCheckBox cbOnlyPending;
        private InterfaceLayer.CommonControls.PSLabel psLabel4;
    }
}
