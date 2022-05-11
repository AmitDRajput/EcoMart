namespace EcoMart.Reporting.Controls
{
    partial class UclCreditNoteList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclCreditNoteList));
           
            this.txtReportTotalAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.txtViewAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtViewType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbltxtProduct = new System.Windows.Forms.Label();
            this.txtViewtext = new System.Windows.Forms.TextBox();
            this.ViewToDate = new System.Windows.Forms.DateTimePicker();
            this.ViewFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblViewTo = new System.Windows.Forms.Label();
            this.lblViewFrom = new System.Windows.Forms.Label();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.mcbType = new System.Windows.Forms.ComboBox();
            this.mPlbl5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.mcbParty = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(975, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmount);
          
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 535);
            this.MMBottomPanel.Size = new System.Drawing.Size(977, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Size = new System.Drawing.Size(977, 483);
           
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BackColor = System.Drawing.Color.Linen;
            this.txtReportTotalAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportTotalAmount.Location = new System.Drawing.Point(831, 0);
            this.txtReportTotalAmount.MaxLength = 15;
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.ReadOnly = true;
            this.txtReportTotalAmount.Size = new System.Drawing.Size(109, 22);
            this.txtReportTotalAmount.TabIndex = 1011;
            this.txtReportTotalAmount.TabStop = false;
            this.txtReportTotalAmount.Tag = "0.00";
            this.txtReportTotalAmount.Text = "0.00";
            this.txtReportTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.txtViewAmount);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.txtViewType);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.lbltxtProduct);
            this.pnlTop.Controls.Add(this.txtViewtext);
            this.pnlTop.Controls.Add(this.ViewToDate);
            this.pnlTop.Controls.Add(this.ViewFromDate);
            this.pnlTop.Controls.Add(this.lblViewTo);
            this.pnlTop.Controls.Add(this.lblViewFrom);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(975, 33);
            this.pnlTop.TabIndex = 1045;
            // 
            // txtViewAmount
            // 
            this.txtViewAmount.Enabled = false;
            this.txtViewAmount.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewAmount.Location = new System.Drawing.Point(504, 5);
            this.txtViewAmount.Name = "txtViewAmount";
            this.txtViewAmount.Size = new System.Drawing.Size(106, 22);
            this.txtViewAmount.TabIndex = 1049;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(443, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 1048;
            this.label2.Text = "Amount";
            // 
            // txtViewType
            // 
            this.txtViewType.Enabled = false;
            this.txtViewType.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewType.Location = new System.Drawing.Point(373, 6);
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
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1046;
            this.label1.Text = "Type";
            // 
            // lbltxtProduct
            // 
            this.lbltxtProduct.AutoSize = true;
            this.lbltxtProduct.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtProduct.Location = new System.Drawing.Point(13, 9);
            this.lbltxtProduct.Name = "lbltxtProduct";
            this.lbltxtProduct.Size = new System.Drawing.Size(40, 15);
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
            // ViewToDate
            // 
            this.ViewToDate.CustomFormat = "dd/MM/yy";
            this.ViewToDate.Enabled = false;
            this.ViewToDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ViewToDate.Location = new System.Drawing.Point(811, 2);
            this.ViewToDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ViewToDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(113, 26);
            this.ViewToDate.TabIndex = 1043;
            this.ViewToDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.CustomFormat = "dd/MM/yy";
            this.ViewFromDate.Enabled = false;
            this.ViewFromDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ViewFromDate.Location = new System.Drawing.Point(661, 2);
            this.ViewFromDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ViewFromDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(118, 26);
            this.ViewFromDate.TabIndex = 1042;
            this.ViewFromDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // lblViewTo
            // 
            this.lblViewTo.AutoSize = true;
            this.lblViewTo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewTo.Location = new System.Drawing.Point(785, 7);
            this.lblViewTo.Name = "lblViewTo";
            this.lblViewTo.Size = new System.Drawing.Size(22, 15);
            this.lblViewTo.TabIndex = 1041;
            this.lblViewTo.Text = "To";
            // 
            // lblViewFrom
            // 
            this.lblViewFrom.AutoSize = true;
            this.lblViewFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewFrom.Location = new System.Drawing.Point(618, 7);
            this.lblViewFrom.Name = "lblViewFrom";
            this.lblViewFrom.Size = new System.Drawing.Size(39, 15);
            this.lblViewFrom.TabIndex = 1040;
            this.lblViewFrom.Text = "From";
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
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(975, 448);
            this.dgvReportList.TabIndex = 1046;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.pnlMultiSelection1.Size = new System.Drawing.Size(470, 144);
            this.pnlMultiSelection1.TabIndex = 1047;
            // 
            // mcbType
            // 
            this.mcbType.FormattingEnabled = true;
            this.mcbType.Items.AddRange(new object[] {
            "CNS",
            "CNA"});
            this.mcbType.Location = new System.Drawing.Point(97, 108);
            this.mcbType.Name = "mcbType";
            this.mcbType.Size = new System.Drawing.Size(113, 21);
            this.mcbType.TabIndex = 9;
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(219, 11);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(28, 19);
            this.mPlbl5.TabIndex = 2;
            this.mPlbl5.Text = "To";
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(36, 108);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(45, 19);
            this.mPlbl4.TabIndex = 8;
            this.mPlbl4.Text = "Type";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(13, 73);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(68, 19);
            this.mPlbl3.TabIndex = 6;
            this.mPlbl3.Text = "Amount";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(32, 42);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(49, 19);
            this.mPlbl2.TabIndex = 4;
            this.mPlbl2.Text = "Party";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(33, 11);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(48, 19);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "From";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(98, 74);
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
            this.toDate1.Location = new System.Drawing.Point(258, 9);
            this.toDate1.Name = "toDate1";         
            this.toDate1.TabIndex = 3;
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";           
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(97, 9);
            this.fromDate1.Name = "fromDate1";
                       this.fromDate1.TabIndex = 1;
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(402, 3);
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
            this.mcbParty.Location = new System.Drawing.Point(93, 42);
            this.mcbParty.Name = "mcbParty";
            this.mcbParty.SelectedID = null;
            this.mcbParty.ShowNew = false;
            this.mcbParty.Size = new System.Drawing.Size(299, 22);
            this.mcbParty.SourceDataString = null;
            this.mcbParty.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbParty.TabIndex = 5;
            this.mcbParty.UserControlToShow = null;
            this.mcbParty.ValueColumnNo = 0;
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // UclCreditNoteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Name = "UclCreditNoteList";
            this.Size = new System.Drawing.Size(977, 558);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

      
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtReportTotalAmount;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TextBox txtViewAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtViewType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbltxtProduct;
        private System.Windows.Forms.TextBox txtViewtext;
        private System.Windows.Forms.DateTimePicker ViewToDate;
        private System.Windows.Forms.DateTimePicker ViewFromDate;
        private System.Windows.Forms.Label lblViewTo;
        private System.Windows.Forms.Label lblViewFrom;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private System.Windows.Forms.ComboBox mcbType;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmount;
        private EcoMart.InterfaceLayer.CommonControls.ToDate toDate1;
        private EcoMart.InterfaceLayer.CommonControls.FromDate fromDate1;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbParty;
        private System.Windows.Forms.ToolTip ttToolTip;
    }
}
