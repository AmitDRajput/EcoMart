namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclCashPayment
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclCashPayment));
            this.mpPVCTemp = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl();
            this.btnModify = new System.Windows.Forms.Button();
            this.txtTotalBalance = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.lblTotalBalance = new System.Windows.Forms.Label();
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblNoofRows = new System.Windows.Forms.Label();
            this.txtNarration = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.txtAmountReceived = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtAmtNotAdjusted = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.mcbCreditor = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.pnlVouTypeNo = new System.Windows.Forms.Panel();
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.mPlbl5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.pnlVou = new System.Windows.Forms.Panel();
            this.txtVouType = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.datePickerBillDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSDateVoucher(this.components);
            this.mPlbl10 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl9 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl8 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtVouchernumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.mpMSVC = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.mpMSCSale = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlVouTypeNo.SuspendLayout();
            this.pnlAddress.SuspendLayout();
            this.pnlVou.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(969, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.lblTotalBalance);
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.txtTotalBalance);
            this.MMBottomPanel.Controls.Add(this.lblNoofRows);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 613);
            this.MMBottomPanel.Size = new System.Drawing.Size(971, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblNoofRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotalBalance, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblTotalBalance, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.mpMSVC);
            this.MMMainPanel.Controls.Add(this.mpMSCSale);
            this.MMMainPanel.Controls.Add(this.mpPVCTemp);
            this.MMMainPanel.Location = new System.Drawing.Point(0, 195);
            this.MMMainPanel.Size = new System.Drawing.Size(971, 418);
            this.MMMainPanel.Controls.SetChildIndex(this.mpPVCTemp, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSCSale, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSVC, 0);
            // 
            // mpPVCTemp
            // 
            this.mpPVCTemp.AllowNewBatch = true;
            this.mpPVCTemp.AutoScroll = true;
            this.mpPVCTemp.BatchColumnName = "Col_BatchNumber";
            this.mpPVCTemp.BatchGridShowColumnName = null;
            this.mpPVCTemp.BatchListGridWidth = 140;
            this.mpPVCTemp.DataSourceBatchList = null;
            this.mpPVCTemp.DataSourceMain = null;
            this.mpPVCTemp.DataSourceProductList = null;
            this.mpPVCTemp.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpPVCTemp.DoubleColumnNames")));
            this.mpPVCTemp.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpPVCTemp.IsAllowDelete = true;
            this.mpPVCTemp.IsAllowNewRow = false;
            this.mpPVCTemp.Location = new System.Drawing.Point(35, 173);
            this.mpPVCTemp.MainGridSoldQuantityColumnName = "";
            this.mpPVCTemp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpPVCTemp.MinimumSize = new System.Drawing.Size(420, 300);
            this.mpPVCTemp.ModuleNumber = PharmaSYSRetailPlus.Common.ModuleNumber.None;
            this.mpPVCTemp.Name = "mpPVCTemp";
            this.mpPVCTemp.NewRowColumnName = null;
            this.mpPVCTemp.NextRowColumn = 0;
            this.mpPVCTemp.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpPVCTemp.NumericColumnNames")));
            this.mpPVCTemp.OperationMode = PharmaSYSRetailPlus.Common.OperationMode.None;
            this.mpPVCTemp.ProductGridClosingStockColumnName = "";
            this.mpPVCTemp.ProductListFilter = null;
            this.mpPVCTemp.ProductListGridWidth = 600;
            this.mpPVCTemp.Size = new System.Drawing.Size(545, 300);
            this.mpPVCTemp.TabIndex = 47;
            this.mpPVCTemp.Visible = false;
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(558, 39);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(74, 44);
            this.btnModify.TabIndex = 1017;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // txtTotalBalance
            // 
            this.txtTotalBalance.BackColor = System.Drawing.SystemColors.Window;
            this.txtTotalBalance.Enabled = false;
            this.txtTotalBalance.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBalance.ForeColor = System.Drawing.Color.Crimson;
            this.txtTotalBalance.Location = new System.Drawing.Point(668, 1);
            this.txtTotalBalance.MaxLength = 15;
            this.txtTotalBalance.Name = "txtTotalBalance";
            this.txtTotalBalance.ReadOnly = true;
            this.txtTotalBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalBalance.Size = new System.Drawing.Size(132, 22);
            this.txtTotalBalance.TabIndex = 1016;
            this.txtTotalBalance.TabStop = false;
            this.txtTotalBalance.Tag = "0.00";
            this.txtTotalBalance.Text = "0.00";
            this.txtTotalBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalBalance
            // 
            this.lblTotalBalance.AutoSize = true;
            this.lblTotalBalance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBalance.Location = new System.Drawing.Point(574, 3);
            this.lblTotalBalance.Name = "lblTotalBalance";
            this.lblTotalBalance.Size = new System.Drawing.Size(88, 15);
            this.lblTotalBalance.TabIndex = 1015;
            this.lblTotalBalance.Text = "Total Balance";
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(482, -1);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 26);
            this.txtNoOfRows.TabIndex = 1014;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNoofRows
            // 
            this.lblNoofRows.AutoSize = true;
            this.lblNoofRows.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRows.Location = new System.Drawing.Point(402, 3);
            this.lblNoofRows.Name = "lblNoofRows";
            this.lblNoofRows.Size = new System.Drawing.Size(74, 15);
            this.lblNoofRows.TabIndex = 1013;
            this.lblNoofRows.Text = "No Of Rows";
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNarration.Location = new System.Drawing.Point(151, 105);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(324, 24);
            this.txtNarration.TabIndex = 3;
            this.txtNarration.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // txtAmountReceived
            // 
            this.txtAmountReceived.BackColor = System.Drawing.SystemColors.Window;
            this.txtAmountReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountReceived.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountReceived.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAmountReceived.Location = new System.Drawing.Point(151, 75);
            this.txtAmountReceived.MaxLength = 15;
            this.txtAmountReceived.Name = "txtAmountReceived";
            this.txtAmountReceived.Size = new System.Drawing.Size(162, 26);
            this.txtAmountReceived.TabIndex = 2;
            this.txtAmountReceived.TabStop = false;
            this.txtAmountReceived.Tag = "0.00";
            this.txtAmountReceived.Text = "0.00";
            this.txtAmountReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmountReceived.TextChanged += new System.EventHandler(this.txtAmountReceived_TextChanged);
            this.txtAmountReceived.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmountReceived_KeyDown);
            // 
            // txtAmtNotAdjusted
            // 
            this.txtAmtNotAdjusted.BackColor = System.Drawing.SystemColors.Window;
            this.txtAmtNotAdjusted.Enabled = false;
            this.txtAmtNotAdjusted.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmtNotAdjusted.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAmtNotAdjusted.Location = new System.Drawing.Point(810, 102);
            this.txtAmtNotAdjusted.MaxLength = 15;
            this.txtAmtNotAdjusted.Name = "txtAmtNotAdjusted";
            this.txtAmtNotAdjusted.Size = new System.Drawing.Size(138, 31);
            this.txtAmtNotAdjusted.TabIndex = 127;
            this.txtAmtNotAdjusted.TabStop = false;
            this.txtAmtNotAdjusted.Tag = "0.00";
            this.txtAmtNotAdjusted.Text = "0.00";
            this.txtAmtNotAdjusted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(152, 10);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = true;
            this.mcbCreditor.Size = new System.Drawing.Size(388, 26);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 0;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.ItemAddedEdited += new System.EventHandler(this.mcbCreditor_ItemAddedEdited);
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            this.mcbCreditor.SeletectIndexChanged += new System.EventHandler(this.mcbCreditor_SeletectIndexChanged);
            // 
            // pnlVouTypeNo
            // 
            this.pnlVouTypeNo.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlVouTypeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVouTypeNo.Controls.Add(this.btnModify);
            this.pnlVouTypeNo.Controls.Add(this.pnlAddress);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl5);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl4);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl3);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl2);
            this.pnlVouTypeNo.Controls.Add(this.mPlbl1);
            this.pnlVouTypeNo.Controls.Add(this.pnlVou);
            this.pnlVouTypeNo.Controls.Add(this.mcbCreditor);
            this.pnlVouTypeNo.Controls.Add(this.txtAmtNotAdjusted);
            this.pnlVouTypeNo.Controls.Add(this.txtAmountReceived);
            this.pnlVouTypeNo.Controls.Add(this.txtNarration);
            this.pnlVouTypeNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVouTypeNo.Location = new System.Drawing.Point(0, 52);
            this.pnlVouTypeNo.Name = "pnlVouTypeNo";
            this.pnlVouTypeNo.Size = new System.Drawing.Size(971, 143);
            this.pnlVouTypeNo.TabIndex = 0;
            // 
            // pnlAddress
            // 
            this.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddress.Controls.Add(this.txtAddress1);
            this.pnlAddress.Controls.Add(this.txtAddress2);
            this.pnlAddress.Enabled = false;
            this.pnlAddress.Location = new System.Drawing.Point(152, 36);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(323, 36);
            this.pnlAddress.TabIndex = 1;
            // 
            // txtAddress1
            // 
            this.txtAddress1.BackColor = System.Drawing.Color.White;
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress1.Enabled = false;
            this.txtAddress1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.Location = new System.Drawing.Point(1, 2);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.ReadOnly = true;
            this.txtAddress1.Size = new System.Drawing.Size(317, 15);
            this.txtAddress1.TabIndex = 0;
            // 
            // txtAddress2
            // 
            this.txtAddress2.BackColor = System.Drawing.Color.White;
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress2.Enabled = false;
            this.txtAddress2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.Location = new System.Drawing.Point(1, 17);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.ReadOnly = true;
            this.txtAddress2.Size = new System.Drawing.Size(317, 15);
            this.txtAddress2.TabIndex = 1;
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(697, 110);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(95, 19);
            this.mPlbl5.TabIndex = 139;
            this.mPlbl5.Text = "On Account";
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(57, 108);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(82, 19);
            this.mPlbl4.TabIndex = 138;
            this.mPlbl4.Text = "Narra&tion";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(71, 81);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(68, 19);
            this.mPlbl3.TabIndex = 137;
            this.mPlbl3.Text = "&Amount";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(71, 42);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(68, 19);
            this.mPlbl2.TabIndex = 136;
            this.mPlbl2.Text = "Address";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(23, 10);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(116, 19);
            this.mPlbl1.TabIndex = 135;
            this.mPlbl1.Text = "Account &Name";
            // 
            // pnlVou
            // 
            this.pnlVou.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlVou.Controls.Add(this.txtVouType);
            this.pnlVou.Controls.Add(this.datePickerBillDate);
            this.pnlVou.Controls.Add(this.mPlbl10);
            this.pnlVou.Controls.Add(this.mPlbl9);
            this.pnlVou.Controls.Add(this.mPlbl8);
            this.pnlVou.Controls.Add(this.txtVouchernumber);
            this.pnlVou.Location = new System.Drawing.Point(750, 3);
            this.pnlVou.Name = "pnlVou";
            this.pnlVou.Size = new System.Drawing.Size(216, 88);
            this.pnlVou.TabIndex = 134;
            // 
            // txtVouType
            // 
            this.txtVouType.BackColor = System.Drawing.Color.Snow;
            this.txtVouType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouType.Enabled = false;
            this.txtVouType.Location = new System.Drawing.Point(88, 5);
            this.txtVouType.MaxLength = 50;
            this.txtVouType.Name = "txtVouType";
            this.txtVouType.ReadOnly = true;
            this.txtVouType.Size = new System.Drawing.Size(125, 24);
            this.txtVouType.TabIndex = 1085;
            this.txtVouType.TabStop = false;
            this.txtVouType.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            // 
            // datePickerBillDate
            // 
            this.datePickerBillDate.CustomFormat = "dd/MM/yyyy";
            this.datePickerBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerBillDate.Location = new System.Drawing.Point(88, 58);
            this.datePickerBillDate.Name = "datePickerBillDate";
            this.datePickerBillDate.Size = new System.Drawing.Size(125, 27);
            this.datePickerBillDate.TabIndex = 1084;
            // 
            // mPlbl10
            // 
            this.mPlbl10.AutoSize = true;
            this.mPlbl10.Location = new System.Drawing.Point(6, 61);
            this.mPlbl10.Name = "mPlbl10";
            this.mPlbl10.Size = new System.Drawing.Size(76, 19);
            this.mPlbl10.TabIndex = 1083;
            this.mPlbl10.Text = "Vou &Date";
            // 
            // mPlbl9
            // 
            this.mPlbl9.AutoSize = true;
            this.mPlbl9.Location = new System.Drawing.Point(20, 34);
            this.mPlbl9.Name = "mPlbl9";
            this.mPlbl9.Size = new System.Drawing.Size(62, 19);
            this.mPlbl9.TabIndex = 1083;
            this.mPlbl9.Text = "Vou No";
            // 
            // mPlbl8
            // 
            this.mPlbl8.AutoSize = true;
            this.mPlbl8.Location = new System.Drawing.Point(4, 8);
            this.mPlbl8.Name = "mPlbl8";
            this.mPlbl8.Size = new System.Drawing.Size(78, 19);
            this.mPlbl8.TabIndex = 1083;
            this.mPlbl8.Text = "Vou Type";
            // 
            // txtVouchernumber
            // 
            this.txtVouchernumber.BackColor = System.Drawing.Color.Snow;
            this.txtVouchernumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouchernumber.Enabled = false;
            this.txtVouchernumber.Location = new System.Drawing.Point(88, 32);
            this.txtVouchernumber.MaxLength = 50;
            this.txtVouchernumber.Name = "txtVouchernumber";
            this.txtVouchernumber.ReadOnly = true;
            this.txtVouchernumber.Size = new System.Drawing.Size(125, 24);
            this.txtVouchernumber.TabIndex = 0;
            this.txtVouchernumber.TabStop = false;
            this.txtVouchernumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            // 
            // mpMSVC
            // 
            this.mpMSVC.AutoScroll = true;
            this.mpMSVC.DataSource = null;
            this.mpMSVC.DataSourceMain = null;
            this.mpMSVC.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC.DateColumnNames")));
            this.mpMSVC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMSVC.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC.DoubleColumnNames")));
            this.mpMSVC.Filter = null;
            this.mpMSVC.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMSVC.IsAllowDelete = false;
            this.mpMSVC.IsAllowNewRow = false;
            this.mpMSVC.Location = new System.Drawing.Point(0, 0);
            this.mpMSVC.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mpMSVC.MinimumSize = new System.Drawing.Size(488, 386);
            this.mpMSVC.Name = "mpMSVC";
            this.mpMSVC.NextRowColumn = 0;
            this.mpMSVC.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC.NumericColumnNames")));
            this.mpMSVC.Size = new System.Drawing.Size(969, 416);
            this.mpMSVC.SubGridWidth = 450;
            this.mpMSVC.TabIndex = 0;
            this.mpMSVC.ViewControl = null;
            this.mpMSVC.Visible = false;
            // 
            // mpMSCSale
            // 
            this.mpMSCSale.AutoScroll = true;
            this.mpMSCSale.DataSource = null;
            this.mpMSCSale.DataSourceMain = null;
            this.mpMSCSale.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSCSale.DateColumnNames")));
            this.mpMSCSale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMSCSale.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSCSale.DoubleColumnNames")));
            this.mpMSCSale.Filter = null;
            this.mpMSCSale.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMSCSale.IsAllowDelete = false;
            this.mpMSCSale.IsAllowNewRow = false;
            this.mpMSCSale.Location = new System.Drawing.Point(0, 0);
            this.mpMSCSale.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mpMSCSale.MinimumSize = new System.Drawing.Size(488, 386);
            this.mpMSCSale.Name = "mpMSCSale";
            this.mpMSCSale.NextRowColumn = 0;
            this.mpMSCSale.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSCSale.NumericColumnNames")));
            this.mpMSCSale.Size = new System.Drawing.Size(969, 416);
            this.mpMSCSale.SubGridWidth = 450;
            this.mpMSCSale.TabIndex = 48;
            this.mpMSCSale.ViewControl = null;
            this.mpMSCSale.OnCellValueChangeCommited += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl.CellValueChangeCommited(this.mpMSCSale_OnCellValueChangeCommited);
            this.mpMSCSale.OnCellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.mpMSCSale_OnCellEnter);
            this.mpMSCSale.OnCellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.mpMSCSale_OnCellLeave);
            // 
            // UclCashPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlVouTypeNo);
            this.Name = "UclCashPayment";
            this.Size = new System.Drawing.Size(971, 636);
            this.Controls.SetChildIndex(this.pnlVouTypeNo, 0);
            this.Controls.SetChildIndex(this.MMBottomPanel, 0);
            this.Controls.SetChildIndex(this.MMMainPanel, 0);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlVouTypeNo.ResumeLayout(false);
            this.pnlVouTypeNo.PerformLayout();
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            this.pnlVou.ResumeLayout(false);
            this.pnlVou.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl mpPVCTemp;
        private System.Windows.Forms.Button btnModify;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotalBalance;
        private System.Windows.Forms.Label lblTotalBalance;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Label lblNoofRows;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNarration;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmountReceived;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmtNotAdjusted;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private System.Windows.Forms.Panel pnlVouTypeNo;
        private System.Windows.Forms.Panel pnlVou;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl10;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl9;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl8;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtAddress2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSDateVoucher datePickerBillDate;
        private System.ComponentModel.IContainer components;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouType;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouchernumber;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl mpMSVC;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl mpMSCSale;

    }
}
