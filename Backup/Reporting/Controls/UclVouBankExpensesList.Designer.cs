namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclVouBankExpensesList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclVouBankExpensesList));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.psLabel11 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtViewText2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel9 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel6 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewText = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewToDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.psLabel3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel8 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mcbAccount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.psLabel7 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.fromDate1 = new System.Windows.Forms.DateTimePicker();
            this.psLabel5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.psLabel4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.toDate1 = new System.Windows.Forms.DateTimePicker();
            this.mcbBank = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.psLabel10 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtTotalAmount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(972, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.psLabel10);
            this.MMBottomPanel.Controls.Add(this.txtTotalAmount);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 501);
            this.MMBottomPanel.Size = new System.Drawing.Size(974, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel10, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Size = new System.Drawing.Size(974, 449);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.PaleTurquoise;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.psLabel11);
            this.pnlTop.Controls.Add(this.txtViewAmount);
            this.pnlTop.Controls.Add(this.txtViewText2);
            this.pnlTop.Controls.Add(this.psLabel9);
            this.pnlTop.Controls.Add(this.psLabel6);
            this.pnlTop.Controls.Add(this.txtViewText);
            this.pnlTop.Controls.Add(this.ViewToDate);
            this.pnlTop.Controls.Add(this.ViewFromDate);
            this.pnlTop.Controls.Add(this.psLabel2);
            this.pnlTop.Controls.Add(this.psLabel1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(972, 33);
            this.pnlTop.TabIndex = 1046;
            // 
            // psLabel11
            // 
            this.psLabel11.AutoSize = true;
            this.psLabel11.Location = new System.Drawing.Point(507, 8);
            this.psLabel11.Name = "psLabel11";
            this.psLabel11.Size = new System.Drawing.Size(68, 19);
            this.psLabel11.TabIndex = 1089;
            this.psLabel11.Text = "Amount";
            // 
            // txtViewAmount
            // 
            this.txtViewAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewAmount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewAmount.Location = new System.Drawing.Point(581, 5);
            this.txtViewAmount.MaxLength = 15;
            this.txtViewAmount.Name = "txtViewAmount";
            this.txtViewAmount.Size = new System.Drawing.Size(86, 23);
            this.txtViewAmount.TabIndex = 1088;
            this.txtViewAmount.Tag = "0.00";
            this.txtViewAmount.Text = "0.00";
            this.txtViewAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtViewText2
            // 
            this.txtViewText2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewText2.Location = new System.Drawing.Point(328, 4);
            this.txtViewText2.Name = "txtViewText2";
            this.txtViewText2.Size = new System.Drawing.Size(172, 23);
            this.txtViewText2.TabIndex = 1087;
            this.txtViewText2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel9
            // 
            this.psLabel9.AutoSize = true;
            this.psLabel9.Location = new System.Drawing.Point(273, 6);
            this.psLabel9.Name = "psLabel9";
            this.psLabel9.Size = new System.Drawing.Size(49, 19);
            this.psLabel9.TabIndex = 1086;
            this.psLabel9.Text = "Party";
            // 
            // psLabel6
            // 
            this.psLabel6.AutoSize = true;
            this.psLabel6.Location = new System.Drawing.Point(8, 5);
            this.psLabel6.Name = "psLabel6";
            this.psLabel6.Size = new System.Drawing.Size(47, 19);
            this.psLabel6.TabIndex = 1085;
            this.psLabel6.Text = "Bank";
            // 
            // txtViewText
            // 
            this.txtViewText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewText.Location = new System.Drawing.Point(64, 3);
            this.txtViewText.Name = "txtViewText";
            this.txtViewText.Size = new System.Drawing.Size(199, 23);
            this.txtViewText.TabIndex = 1084;
            this.txtViewText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(863, 5);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1082;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(724, 5);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1081;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(830, 5);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(28, 19);
            this.psLabel2.TabIndex = 1080;
            this.psLabel2.Text = "To";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(672, 5);
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
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(972, 414);
            this.dgvReportList.TabIndex = 1047;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.psLabel3);
            this.pnlMultiSelection1.Controls.Add(this.psLabel8);
            this.pnlMultiSelection1.Controls.Add(this.mcbAccount);
            this.pnlMultiSelection1.Controls.Add(this.psLabel7);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.psLabel5);
            this.pnlMultiSelection1.Controls.Add(this.txtAmount);
            this.pnlMultiSelection1.Controls.Add(this.psLabel4);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.mcbBank);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(278, 108);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(548, 188);
            this.pnlMultiSelection1.TabIndex = 1048;
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(480, 2);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 1085;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(37, 17);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(47, 19);
            this.psLabel3.TabIndex = 1080;
            this.psLabel3.Text = "Bank";
            // 
            // psLabel8
            // 
            this.psLabel8.AutoSize = true;
            this.psLabel8.Location = new System.Drawing.Point(35, 144);
            this.psLabel8.Name = "psLabel8";
            this.psLabel8.Size = new System.Drawing.Size(49, 19);
            this.psLabel8.TabIndex = 1084;
            this.psLabel8.Text = "Party";
            // 
            // mcbAccount
            // 
            this.mcbAccount.ColumnWidth = null;
            this.mcbAccount.DataSource = null;
            this.mcbAccount.DisplayColumnNo = 1;
            this.mcbAccount.DropDownHeight = 200;
            this.mcbAccount.Location = new System.Drawing.Point(96, 144);
            this.mcbAccount.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbAccount.Name = "mcbAccount";
            this.mcbAccount.SelectedID = null;
            this.mcbAccount.ShowNew = false;
            this.mcbAccount.Size = new System.Drawing.Size(382, 26);
            this.mcbAccount.SourceDataString = null;
            this.mcbAccount.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbAccount.TabIndex = 40;
            this.mcbAccount.UserControlToShow = null;
            this.mcbAccount.ValueColumnNo = 0;
            this.mcbAccount.EnterKeyPressed += new System.EventHandler(this.mcbAccount_EnterKeyPressed);
            // 
            // psLabel7
            // 
            this.psLabel7.AutoSize = true;
            this.psLabel7.Location = new System.Drawing.Point(16, 114);
            this.psLabel7.Name = "psLabel7";
            this.psLabel7.Size = new System.Drawing.Size(68, 19);
            this.psLabel7.TabIndex = 1083;
            this.psLabel7.Text = "Amount";
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "";
            this.fromDate1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(96, 49);
            this.fromDate1.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.fromDate1.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(120, 26);
            this.fromDate1.TabIndex = 134;
            this.fromDate1.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // psLabel5
            // 
            this.psLabel5.AutoSize = true;
            this.psLabel5.Location = new System.Drawing.Point(62, 85);
            this.psLabel5.Name = "psLabel5";
            this.psLabel5.Size = new System.Drawing.Size(28, 19);
            this.psLabel5.TabIndex = 1082;
            this.psLabel5.Text = "To";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(96, 113);
            this.txtAmount.MaxLength = 15;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(120, 26);
            this.txtAmount.TabIndex = 46;
            this.txtAmount.Tag = "0.00";
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(36, 53);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(48, 19);
            this.psLabel4.TabIndex = 1081;
            this.psLabel4.Text = "From";
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "";
            this.toDate1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(96, 81);
            this.toDate1.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.toDate1.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(120, 26);
            this.toDate1.TabIndex = 135;
            this.toDate1.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // mcbBank
            // 
            this.mcbBank.ColumnWidth = null;
            this.mcbBank.DataSource = null;
            this.mcbBank.DisplayColumnNo = 1;
            this.mcbBank.DropDownHeight = 200;
            this.mcbBank.Location = new System.Drawing.Point(96, 14);
            this.mcbBank.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbBank.Name = "mcbBank";
            this.mcbBank.SelectedID = null;
            this.mcbBank.ShowNew = false;
            this.mcbBank.Size = new System.Drawing.Size(382, 26);
            this.mcbBank.SourceDataString = null;
            this.mcbBank.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbBank.TabIndex = 137;
            this.mcbBank.UserControlToShow = null;
            this.mcbBank.ValueColumnNo = 0;
            this.mcbBank.EnterKeyPressed += new System.EventHandler(this.mcbBank_EnterKeyPressed);
            // 
            // psLabel10
            // 
            this.psLabel10.AutoSize = true;
            this.psLabel10.Location = new System.Drawing.Point(656, -1);
            this.psLabel10.Name = "psLabel10";
            this.psLabel10.Size = new System.Drawing.Size(111, 19);
            this.psLabel10.TabIndex = 1086;
            this.psLabel10.Text = "Total Amount";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalAmount.CausesValidation = false;
            this.txtTotalAmount.Location = new System.Drawing.Point(783, -2);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(142, 23);
            this.txtTotalAmount.TabIndex = 1085;
            this.txtTotalAmount.Text = "label";
            this.txtTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UclVouBankExpensesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclVouBankExpensesList";
            this.Size = new System.Drawing.Size(974, 524);
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

        private System.Windows.Forms.Panel pnlTop;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel11;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtViewAmount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft txtViewText2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel9;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel6;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft txtViewText;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel8;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbAccount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel7;
        private System.Windows.Forms.DateTimePicker fromDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel5;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel4;
        private System.Windows.Forms.DateTimePicker toDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbBank;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel10;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtTotalAmount;
        private System.Windows.Forms.ToolTip ttToolTip;
    }
}
