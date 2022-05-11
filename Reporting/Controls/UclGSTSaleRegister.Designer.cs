namespace  EcoMart.Reporting.Controls
{
    partial class UclGSTSaleRegister
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclGSTSaleRegister));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.DataGridViewContent = new PharmaSYSPlus.CommonLibrary.MReportGridViewBase(this.components);
            this.mReportGridViewBase1 = new PharmaSYSPlus.CommonLibrary.MReportGridViewBase(this.components);
            this.mReportGridViewBase2 = new PharmaSYSPlus.CommonLibrary.MReportGridViewBase(this.components);
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnHSNNumberWise = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.btnVoucherNumberWise = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtYear = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtMonth = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.dgvReportList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mReportGridViewBase1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mReportGridViewBase2)).BeginInit();
            this.pnlMultiSelection1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(1027, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 473);
            this.MMBottomPanel.Size = new System.Drawing.Size(1029, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(1029, 421);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlGo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // pnlGo
            // 
            this.pnlGo.BackColor = System.Drawing.Color.DarkKhaki;
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(1027, 33);
            this.pnlGo.TabIndex = 50;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(837, 4);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1082;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(688, 4);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1081;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = false;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.Khaki;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.Controls.Add(this.DataGridViewContent);
            this.dgvReportList.Controls.Add(this.mReportGridViewBase1);
            this.dgvReportList.Controls.Add(this.mReportGridViewBase2);
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.FreezeLastRow = false;
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(1027, 386);
            this.dgvReportList.TabIndex = 1048;
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
            this.DataGridViewContent.FreezeLastRow = false;
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
            this.DataGridViewContent.Size = new System.Drawing.Size(1025, 384);
            this.DataGridViewContent.TabIndex = 5;
            // 
            // mReportGridViewBase1
            // 
            this.mReportGridViewBase1.AllowUserToAddRows = false;
            this.mReportGridViewBase1.AllowUserToDeleteRows = false;
            this.mReportGridViewBase1.AllowUserToOrderColumns = true;
            this.mReportGridViewBase1.AllowUserToResizeRows = false;
            this.mReportGridViewBase1.BackgroundColor = System.Drawing.Color.RosyBrown;
            this.mReportGridViewBase1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mReportGridViewBase1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.mReportGridViewBase1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mReportGridViewBase1.DefaultCellStyle = dataGridViewCellStyle5;
            this.mReportGridViewBase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mReportGridViewBase1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.mReportGridViewBase1.FreezeLastRow = false;
            this.mReportGridViewBase1.Location = new System.Drawing.Point(0, 0);
            this.mReportGridViewBase1.MultiSelect = false;
            this.mReportGridViewBase1.Name = "mReportGridViewBase1";
            this.mReportGridViewBase1.RowHeadersVisible = false;
            this.mReportGridViewBase1.RowHeadersWidth = 25;
            this.mReportGridViewBase1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.mReportGridViewBase1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.mReportGridViewBase1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mReportGridViewBase1.Size = new System.Drawing.Size(1025, 384);
            this.mReportGridViewBase1.TabIndex = 5;
            // 
            // mReportGridViewBase2
            // 
            this.mReportGridViewBase2.AllowUserToAddRows = false;
            this.mReportGridViewBase2.AllowUserToDeleteRows = false;
            this.mReportGridViewBase2.AllowUserToOrderColumns = true;
            this.mReportGridViewBase2.AllowUserToResizeRows = false;
            this.mReportGridViewBase2.BackgroundColor = System.Drawing.Color.RosyBrown;
            this.mReportGridViewBase2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mReportGridViewBase2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.mReportGridViewBase2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mReportGridViewBase2.DefaultCellStyle = dataGridViewCellStyle8;
            this.mReportGridViewBase2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mReportGridViewBase2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.mReportGridViewBase2.FreezeLastRow = false;
            this.mReportGridViewBase2.Location = new System.Drawing.Point(0, 0);
            this.mReportGridViewBase2.MultiSelect = false;
            this.mReportGridViewBase2.Name = "mReportGridViewBase2";
            this.mReportGridViewBase2.RowHeadersVisible = false;
            this.mReportGridViewBase2.RowHeadersWidth = 25;
            this.mReportGridViewBase2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            this.mReportGridViewBase2.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.mReportGridViewBase2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mReportGridViewBase2.Size = new System.Drawing.Size(1025, 384);
            this.mReportGridViewBase2.TabIndex = 5;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.groupBox1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl4);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl3);
            this.pnlMultiSelection1.Controls.Add(this.txtYear);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.txtMonth);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(341, 128);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(345, 163);
            this.pnlMultiSelection1.TabIndex = 1049;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnHSNNumberWise);
            this.groupBox1.Controls.Add(this.btnVoucherNumberWise);
            this.groupBox1.Location = new System.Drawing.Point(67, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 70);
            this.groupBox1.TabIndex = 1060;
            this.groupBox1.TabStop = false;
            // 
            // btnHSNNumberWise
            // 
            this.btnHSNNumberWise.AutoSize = true;
            this.btnHSNNumberWise.BackColor = System.Drawing.Color.White;
            this.btnHSNNumberWise.Location = new System.Drawing.Point(0, 38);
            this.btnHSNNumberWise.Name = "btnHSNNumberWise";
            this.btnHSNNumberWise.Size = new System.Drawing.Size(138, 21);
            this.btnHSNNumberWise.TabIndex = 1;
            this.btnHSNNumberWise.TabStop = true;
            this.btnHSNNumberWise.Text = "HSN NumberWise";
            this.btnHSNNumberWise.UseVisualStyleBackColor = false;
            this.btnHSNNumberWise.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnHSNNumberWise_KeyDown);
            // 
            // btnVoucherNumberWise
            // 
            this.btnVoucherNumberWise.AutoSize = true;
            this.btnVoucherNumberWise.BackColor = System.Drawing.Color.White;
            this.btnVoucherNumberWise.Location = new System.Drawing.Point(0, 8);
            this.btnVoucherNumberWise.Name = "btnVoucherNumberWise";
            this.btnVoucherNumberWise.Size = new System.Drawing.Size(163, 21);
            this.btnVoucherNumberWise.TabIndex = 0;
            this.btnVoucherNumberWise.TabStop = true;
            this.btnVoucherNumberWise.Text = "Voucher NumberWise";
            this.btnVoucherNumberWise.UseVisualStyleBackColor = false;
            this.btnVoucherNumberWise.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnVoucherNumberWise_KeyDown);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(248, 5);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(23, 24);
            this.fromDate1.TabIndex = 1057;
            this.fromDate1.Visible = false;
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(248, 34);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(23, 24);
            this.toDate1.TabIndex = 1058;
            this.toDate1.Visible = false;
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(277, 2);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 1056;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(222, 33);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(22, 16);
            this.mPlbl4.TabIndex = 1045;
            this.mPlbl4.Text = "To";
            this.mPlbl4.Visible = false;
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(203, 5);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(41, 16);
            this.mPlbl3.TabIndex = 1044;
            this.mPlbl3.Text = "From";
            this.mPlbl3.Visible = false;
            // 
            // txtYear
            // 
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Location = new System.Drawing.Point(116, 50);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(77, 22);
            this.txtYear.TabIndex = 1043;
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(64, 52);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(35, 16);
            this.mPlbl2.TabIndex = 1043;
            this.mPlbl2.Text = "Year";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(52, 22);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(47, 16);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "Mont&h";
            // 
            // txtMonth
            // 
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonth.Location = new System.Drawing.Point(116, 20);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(39, 22);
            this.txtMonth.TabIndex = 1059;
            this.txtMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMonth_KeyDown);
            // 
            // UclGSTSaleRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclGSTSaleRegister";
            this.Size = new System.Drawing.Size(1029, 496);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.dgvReportList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mReportGridViewBase1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mReportGridViewBase2)).EndInit();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGo;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private PharmaSYSPlus.CommonLibrary.MReportGridViewBase DataGridViewContent;
        private PharmaSYSPlus.CommonLibrary.MReportGridViewBase mReportGridViewBase1;
        private PharmaSYSPlus.CommonLibrary.MReportGridViewBase mReportGridViewBase2;
        private InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private System.Windows.Forms.GroupBox groupBox1;
        private InterfaceLayer.CommonControls.PSRadioButton btnHSNNumberWise;
        private InterfaceLayer.CommonControls.PSRadioButton btnVoucherNumberWise;
        private InterfaceLayer.CommonControls.FromDate fromDate1;
        private InterfaceLayer.CommonControls.ToDate toDate1;
        private InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtYear;
        private InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtMonth;
    }
}
