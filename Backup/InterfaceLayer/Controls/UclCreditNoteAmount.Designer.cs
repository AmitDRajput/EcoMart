namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclCreditNoteAmount
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
     //   private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclCreditNoteAmount));
            this.mpProductGrid = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.pnlVouTypeNo = new System.Windows.Forms.Panel();
            this.pnlVou = new System.Windows.Forms.Panel();
            this.mPlbl10 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl9 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl8 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtVouType = new System.Windows.Forms.TextBox();
            this.datePickerBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtVouchernumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.mPlbl12 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtBillAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.mPlbl3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mcbCreditor = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtNarration = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlAmounts = new System.Windows.Forms.Panel();
            this.mPlbl7 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl6 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtNetAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtTotalAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtDiscPercent = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtRoundAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.cbRound = new System.Windows.Forms.CheckBox();
            this.txtAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtVatInput12point5per = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtVatInput5per = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtDiscAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlVouTypeNo.SuspendLayout();
            this.pnlVou.SuspendLayout();
            this.pnlAddress.SuspendLayout();
            this.pnlAmounts.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(968, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.label21);
            this.MMBottomPanel.Controls.Add(this.label7);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 588);
            this.MMBottomPanel.Size = new System.Drawing.Size(970, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.label7, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.label21, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlAmounts);
            this.MMMainPanel.Controls.Add(this.mpProductGrid);
            this.MMMainPanel.Controls.Add(this.pnlVouTypeNo);
            this.MMMainPanel.Size = new System.Drawing.Size(970, 536);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlVouTypeNo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpProductGrid, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlAmounts, 0);
            // 
            // mpProductGrid
            // 
            this.mpProductGrid.AutoScroll = true;
            this.mpProductGrid.DataSource = null;
            this.mpProductGrid.DataSourceMain = null;
            this.mpProductGrid.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpProductGrid.DateColumnNames")));
            this.mpProductGrid.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpProductGrid.DoubleColumnNames")));
            this.mpProductGrid.Filter = null;
            this.mpProductGrid.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpProductGrid.IsAllowDelete = true;
            this.mpProductGrid.IsAllowNewRow = true;
            this.mpProductGrid.Location = new System.Drawing.Point(192, 116);
            this.mpProductGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpProductGrid.MinimumSize = new System.Drawing.Size(439, 297);
            this.mpProductGrid.Name = "mpProductGrid";
            this.mpProductGrid.NextRowColumn = 0;
            this.mpProductGrid.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpProductGrid.NumericColumnNames")));
            this.mpProductGrid.Size = new System.Drawing.Size(552, 297);
            this.mpProductGrid.SubGridWidth = 380;
            this.mpProductGrid.TabIndex = 0;
            this.mpProductGrid.ViewControl = null;
            this.mpProductGrid.OnCellValueChangeCommited += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl.CellValueChangeCommited(this.mpProductGrid_OnCellValueChangeCommited);
            this.mpProductGrid.OnTABKeyPressed += new System.EventHandler(this.mpProductGrid_OnTABKeyPressed);
            this.mpProductGrid.OnRowDeleted += new System.EventHandler(this.mpProductGrid_OnRowDeleted);
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.Snow;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(583, 0);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 26);
            this.txtNoOfRows.TabIndex = 27;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlVouTypeNo
            // 
            this.pnlVouTypeNo.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlVouTypeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVouTypeNo.Controls.Add(this.pnlVou);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl12);
            this.pnlVouTypeNo.Controls.Add(this.txtBillAmount);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl3);
            this.pnlVouTypeNo.Controls.Add(this.pnlAddress);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl2);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl1);
            this.pnlVouTypeNo.Controls.Add(this.mcbCreditor);
            this.pnlVouTypeNo.Controls.Add(this.txtNarration);
            this.pnlVouTypeNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVouTypeNo.Location = new System.Drawing.Point(0, 0);
            this.pnlVouTypeNo.Name = "pnlVouTypeNo";
            this.pnlVouTypeNo.Size = new System.Drawing.Size(968, 110);
            this.pnlVouTypeNo.TabIndex = 132;
            // 
            // pnlVou
            // 
            this.pnlVou.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlVou.Controls.Add(this.mPlbl10);
            this.pnlVou.Controls.Add(this.mPlbl9);
            this.pnlVou.Controls.Add(this.mPlbl8);
            this.pnlVou.Controls.Add(this.txtVouType);
            this.pnlVou.Controls.Add(this.datePickerBillDate);
            this.pnlVou.Controls.Add(this.txtVouchernumber);
            this.pnlVou.Location = new System.Drawing.Point(747, 4);
            this.pnlVou.Name = "pnlVou";
            this.pnlVou.Size = new System.Drawing.Size(216, 88);
            this.pnlVou.TabIndex = 1099;
            // 
            // mPlbl10
            // 
            this.mPlbl10.AutoSize = true;
            this.mPlbl10.Location = new System.Drawing.Point(9, 61);
            this.mPlbl10.Name = "mPlbl10";
            this.mPlbl10.Size = new System.Drawing.Size(76, 19);
            this.mPlbl10.TabIndex = 1083;
            this.mPlbl10.Text = "Vou &Date";
            // 
            // mPlbl9
            // 
            this.mPlbl9.AutoSize = true;
            this.mPlbl9.Location = new System.Drawing.Point(23, 34);
            this.mPlbl9.Name = "mPlbl9";
            this.mPlbl9.Size = new System.Drawing.Size(62, 19);
            this.mPlbl9.TabIndex = 1083;
            this.mPlbl9.Text = "Vou No";
            // 
            // mPlbl8
            // 
            this.mPlbl8.AutoSize = true;
            this.mPlbl8.Location = new System.Drawing.Point(7, 8);
            this.mPlbl8.Name = "mPlbl8";
            this.mPlbl8.Size = new System.Drawing.Size(78, 19);
            this.mPlbl8.TabIndex = 1083;
            this.mPlbl8.Text = "Vou Type";
            // 
            // txtVouType
            // 
            this.txtVouType.BackColor = System.Drawing.Color.Snow;
            this.txtVouType.Enabled = false;
            this.txtVouType.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVouType.ForeColor = System.Drawing.Color.Navy;
            this.txtVouType.Location = new System.Drawing.Point(93, 3);
            this.txtVouType.Name = "txtVouType";
            this.txtVouType.ReadOnly = true;
            this.txtVouType.Size = new System.Drawing.Size(113, 26);
            this.txtVouType.TabIndex = 132;
            // 
            // datePickerBillDate
            // 
            this.datePickerBillDate.CustomFormat = "dd/MM/yy";
            this.datePickerBillDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerBillDate.Location = new System.Drawing.Point(93, 57);
            this.datePickerBillDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerBillDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerBillDate.Name = "datePickerBillDate";
            this.datePickerBillDate.Size = new System.Drawing.Size(113, 26);
            this.datePickerBillDate.TabIndex = 0;
            this.datePickerBillDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // txtVouchernumber
            // 
            this.txtVouchernumber.BackColor = System.Drawing.Color.Snow;
            this.txtVouchernumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouchernumber.Enabled = false;
            this.txtVouchernumber.Location = new System.Drawing.Point(93, 30);
            this.txtVouchernumber.MaxLength = 50;
            this.txtVouchernumber.Name = "txtVouchernumber";
            this.txtVouchernumber.ReadOnly = true;
            this.txtVouchernumber.Size = new System.Drawing.Size(113, 24);
            this.txtVouchernumber.TabIndex = 0;
            this.txtVouchernumber.TabStop = false;
            this.txtVouchernumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtVouchernumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVouchernumber_KeyDown);
            // 
            // mPlbl12
            // 
            this.mPlbl12.AutoSize = true;
            this.mPlbl12.Location = new System.Drawing.Point(559, 44);
            this.mPlbl12.Name = "mPlbl12";
            this.mPlbl12.Size = new System.Drawing.Size(110, 19);
            this.mPlbl12.TabIndex = 1098;
            this.mPlbl12.Text = "Final Amount";
            // 
            // txtBillAmount
            // 
            this.txtBillAmount.BackColor = System.Drawing.Color.Snow;
            this.txtBillAmount.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillAmount.ForeColor = System.Drawing.Color.DeepPink;
            this.txtBillAmount.Location = new System.Drawing.Point(489, 64);
            this.txtBillAmount.MaxLength = 15;
            this.txtBillAmount.Name = "txtBillAmount";
            this.txtBillAmount.ReadOnly = true;
            this.txtBillAmount.Size = new System.Drawing.Size(222, 40);
            this.txtBillAmount.TabIndex = 1097;
            this.txtBillAmount.TabStop = false;
            this.txtBillAmount.Tag = "0.00";
            this.txtBillAmount.Text = "0.00";
            this.txtBillAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(50, 80);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(82, 19);
            this.mPlbl3.TabIndex = 1096;
            this.mPlbl3.Text = "&Narration";
            // 
            // pnlAddress
            // 
            this.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddress.Controls.Add(this.txtAddress1);
            this.pnlAddress.Controls.Add(this.txtAddress2);
            this.pnlAddress.Enabled = false;
            this.pnlAddress.Location = new System.Drawing.Point(142, 38);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(323, 36);
            this.pnlAddress.TabIndex = 1095;
            // 
            // txtAddress1
            // 
            this.txtAddress1.BackColor = System.Drawing.Color.White;
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.Location = new System.Drawing.Point(1, 2);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(317, 15);
            this.txtAddress1.TabIndex = 0;
            // 
            // txtAddress2
            // 
            this.txtAddress2.BackColor = System.Drawing.Color.White;
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.Location = new System.Drawing.Point(1, 17);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(317, 15);
            this.txtAddress2.TabIndex = 1;
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(64, 48);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(68, 19);
            this.mPlbl2.TabIndex = 1094;
            this.mPlbl2.Text = "Address";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(16, 12);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(116, 19);
            this.mPlbl1.TabIndex = 1093;
            this.mPlbl1.Text = "&Account Name";
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(142, 10);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = true;
            this.mcbCreditor.Size = new System.Drawing.Size(323, 26);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 1092;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.ItemAddedEdited += new System.EventHandler(this.mcbCreditor_ItemAddedEdited);
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            this.mcbCreditor.SeletectIndexChanged += new System.EventHandler(this.mcbCreditor_SeletectIndexChanged);
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.Location = new System.Drawing.Point(142, 80);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(324, 24);
            this.txtNarration.TabIndex = 1091;
            this.txtNarration.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(503, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(74, 15);
            this.label21.TabIndex = 1010;
            this.label21.Text = "No Of Rows";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Yellow;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(18, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(2, 16);
            this.label7.TabIndex = 1009;
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // pnlAmounts
            // 
            this.pnlAmounts.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlAmounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAmounts.Controls.Add(this.mPlbl7);
            this.pnlAmounts.Controls.Add(this.mPlbl6);
            this.pnlAmounts.Controls.Add(this.mPlbl5);
            this.pnlAmounts.Controls.Add(this.mPlbl4);
            this.pnlAmounts.Controls.Add(this.txtNetAmount);
            this.pnlAmounts.Controls.Add(this.txtTotalAmount);
            this.pnlAmounts.Controls.Add(this.txtDiscPercent);
            this.pnlAmounts.Controls.Add(this.txtRoundAmount);
            this.pnlAmounts.Controls.Add(this.cbRound);
            this.pnlAmounts.Controls.Add(this.txtAmount);
            this.pnlAmounts.Controls.Add(this.txtVatInput12point5per);
            this.pnlAmounts.Controls.Add(this.txtVatInput5per);
            this.pnlAmounts.Controls.Add(this.txtDiscAmount);
            this.pnlAmounts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAmounts.Location = new System.Drawing.Point(0, 411);
            this.pnlAmounts.Name = "pnlAmounts";
            this.pnlAmounts.Size = new System.Drawing.Size(968, 123);
            this.pnlAmounts.TabIndex = 134;
            // 
            // mPlbl7
            // 
            this.mPlbl7.AutoSize = true;
            this.mPlbl7.Location = new System.Drawing.Point(669, 96);
            this.mPlbl7.Name = "mPlbl7";
            this.mPlbl7.Size = new System.Drawing.Size(110, 19);
            this.mPlbl7.TabIndex = 1019;
            this.mPlbl7.Text = "Final Amount";
            // 
            // mPlbl6
            // 
            this.mPlbl6.AutoSize = true;
            this.mPlbl6.Location = new System.Drawing.Point(711, 66);
            this.mPlbl6.Name = "mPlbl6";
            this.mPlbl6.Size = new System.Drawing.Size(68, 19);
            this.mPlbl6.TabIndex = 1018;
            this.mPlbl6.Text = "Amount";
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(637, 39);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(56, 19);
            this.mPlbl5.TabIndex = 1017;
            this.mPlbl5.Text = "&Less%";
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(731, 8);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(48, 19);
            this.mPlbl4.TabIndex = 1016;
            this.mPlbl4.Text = "Total";
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BackColor = System.Drawing.Color.White;
            this.txtNetAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount.Location = new System.Drawing.Point(789, 91);
            this.txtNetAmount.MaxLength = 15;
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(164, 26);
            this.txtNetAmount.TabIndex = 1014;
            this.txtNetAmount.Tag = "0.00";
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.Color.White;
            this.txtTotalAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(789, 4);
            this.txtTotalAmount.MaxLength = 15;
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(164, 26);
            this.txtTotalAmount.TabIndex = 29;
            this.txtTotalAmount.Tag = "0.00";
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDiscPercent
            // 
            this.txtDiscPercent.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscPercent.Location = new System.Drawing.Point(708, 35);
            this.txtDiscPercent.MaxLength = 15;
            this.txtDiscPercent.Name = "txtDiscPercent";
            this.txtDiscPercent.Size = new System.Drawing.Size(74, 26);
            this.txtDiscPercent.TabIndex = 31;
            this.txtDiscPercent.Tag = "0.00";
            this.txtDiscPercent.Text = "0.00";
            this.txtDiscPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscPercent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscPercent_KeyDown);
            // 
            // txtRoundAmount
            // 
            this.txtRoundAmount.BackColor = System.Drawing.Color.Snow;
            this.txtRoundAmount.Enabled = false;
            this.txtRoundAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoundAmount.Location = new System.Drawing.Point(563, 91);
            this.txtRoundAmount.MaxLength = 6;
            this.txtRoundAmount.Name = "txtRoundAmount";
            this.txtRoundAmount.Size = new System.Drawing.Size(73, 26);
            this.txtRoundAmount.TabIndex = 28;
            this.txtRoundAmount.TabStop = false;
            this.txtRoundAmount.Tag = "0.00";
            this.txtRoundAmount.Text = "0.00";
            this.txtRoundAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbRound
            // 
            this.cbRound.AutoSize = true;
            this.cbRound.BackColor = System.Drawing.SystemColors.Control;
            this.cbRound.Checked = true;
            this.cbRound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRound.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRound.Location = new System.Drawing.Point(488, 94);
            this.cbRound.Name = "cbRound";
            this.cbRound.Size = new System.Drawing.Size(73, 21);
            this.cbRound.TabIndex = 23;
            this.cbRound.Text = "Round";
            this.cbRound.UseVisualStyleBackColor = false;
            this.cbRound.CheckedChanged += new System.EventHandler(this.cbRound_CheckedChanged);
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.White;
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(789, 62);
            this.txtAmount.MaxLength = 15;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(164, 26);
            this.txtAmount.TabIndex = 10;
            this.txtAmount.TabStop = false;
            this.txtAmount.Tag = "0.00";
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVatInput12point5per
            // 
            this.txtVatInput12point5per.BackColor = System.Drawing.Color.White;
            this.txtVatInput12point5per.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVatInput12point5per.Location = new System.Drawing.Point(142, 50);
            this.txtVatInput12point5per.MaxLength = 15;
            this.txtVatInput12point5per.Name = "txtVatInput12point5per";
            this.txtVatInput12point5per.Size = new System.Drawing.Size(146, 26);
            this.txtVatInput12point5per.TabIndex = 1;
            this.txtVatInput12point5per.TabStop = false;
            this.txtVatInput12point5per.Tag = "0.00";
            this.txtVatInput12point5per.Text = "0.00";
            this.txtVatInput12point5per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVatInput12point5per.Visible = false;
            this.txtVatInput12point5per.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVatInput12point5per_KeyDown);
            // 
            // txtVatInput5per
            // 
            this.txtVatInput5per.BackColor = System.Drawing.Color.White;
            this.txtVatInput5per.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVatInput5per.Location = new System.Drawing.Point(142, 22);
            this.txtVatInput5per.MaxLength = 15;
            this.txtVatInput5per.Name = "txtVatInput5per";
            this.txtVatInput5per.Size = new System.Drawing.Size(146, 26);
            this.txtVatInput5per.TabIndex = 0;
            this.txtVatInput5per.TabStop = false;
            this.txtVatInput5per.Tag = "0.00";
            this.txtVatInput5per.Text = "0.00";
            this.txtVatInput5per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVatInput5per.Visible = false;
            this.txtVatInput5per.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVatInput5per_KeyDown);
            // 
            // txtDiscAmount
            // 
            this.txtDiscAmount.BackColor = System.Drawing.Color.White;
            this.txtDiscAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscAmount.Location = new System.Drawing.Point(789, 33);
            this.txtDiscAmount.MaxLength = 15;
            this.txtDiscAmount.Name = "txtDiscAmount";
            this.txtDiscAmount.ReadOnly = true;
            this.txtDiscAmount.Size = new System.Drawing.Size(164, 26);
            this.txtDiscAmount.TabIndex = 3;
            this.txtDiscAmount.Tag = "0.00";
            this.txtDiscAmount.Text = "0.00";
            this.txtDiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscAmount_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 15);
            this.label4.TabIndex = 25;
            this.label4.Text = "VAT Input 12.5 %";
            this.label4.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "VAT Input 5%";
            this.label2.Visible = false;
            // 
            // UclCreditNoteAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "UclCreditNoteAmount";
            this.Size = new System.Drawing.Size(970, 611);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlVouTypeNo.ResumeLayout(false);
            this.pnlVouTypeNo.PerformLayout();
            this.pnlVou.ResumeLayout(false);
            this.pnlVou.PerformLayout();
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            this.pnlAmounts.ResumeLayout(false);
            this.pnlAmounts.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl mpProductGrid;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Panel pnlVouTypeNo;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip ttToolTip;
        private System.ComponentModel.IContainer components;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtAddress2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNarration;
        private System.Windows.Forms.Panel pnlVou;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl10;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl9;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl8;
        private System.Windows.Forms.TextBox txtVouType;
        private System.Windows.Forms.DateTimePicker datePickerBillDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouchernumber;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl12;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtBillAmount;
        private System.Windows.Forms.Panel pnlAmounts;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl7;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl6;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtNetAmount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotalAmount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtDiscPercent;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtRoundAmount;
        private System.Windows.Forms.CheckBox cbRound;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtVatInput12point5per;
        private System.Windows.Forms.Label label4;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtVatInput5per;
        private System.Windows.Forms.Label label2;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtDiscAmount;

    }
}
