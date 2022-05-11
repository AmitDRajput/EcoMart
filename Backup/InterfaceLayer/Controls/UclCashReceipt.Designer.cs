namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclCashReceipt
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
      //  private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclCashReceipt));
            this.lblMessage = new System.Windows.Forms.Label();
            this.mpMSCSale = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.pnlNameAddress = new System.Windows.Forms.Panel();
            this.txtAmtNotAdjusted = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.mPlbl5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.pnlVou = new System.Windows.Forms.Panel();
            this.mPlbl10 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl9 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl8 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtVouType = new System.Windows.Forms.TextBox();
            this.datePickerBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtVouchernumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.mPlbl4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mcbCreditor = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtAmountReceived = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtNarration = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.mpPVCTemp = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl();
            this.txtTotalBalance = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtNoOfRows = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.mpMSVC = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.btnModify = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton();
            this.psLabel1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlNameAddress.SuspendLayout();
            this.pnlVou.SuspendLayout();
            this.pnlAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(971, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.psLabel2);
            this.MMBottomPanel.Controls.Add(this.psLabel1);
            this.MMBottomPanel.Controls.Add(this.btnModify);
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.txtTotalBalance);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 654);
            this.MMBottomPanel.Size = new System.Drawing.Size(973, 25);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotalBalance, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.btnModify, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel1, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel2, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.mpMSVC);
            this.MMMainPanel.Controls.Add(this.mpMSCSale);
            this.MMMainPanel.Controls.Add(this.mpPVCTemp);
            this.MMMainPanel.Controls.Add(this.lblMessage);
            this.MMMainPanel.Controls.Add(this.pnlNameAddress);
            this.MMMainPanel.Size = new System.Drawing.Size(973, 602);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlNameAddress, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpPVCTemp, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSCSale, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSVC, 0);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.Location = new System.Drawing.Point(5, 644);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 61;
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
            this.mpMSCSale.Location = new System.Drawing.Point(0, 144);
            this.mpMSCSale.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mpMSCSale.MinimumSize = new System.Drawing.Size(488, 386);
            this.mpMSCSale.Name = "mpMSCSale";
            this.mpMSCSale.NextRowColumn = 8;
            this.mpMSCSale.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSCSale.NumericColumnNames")));
            this.mpMSCSale.Size = new System.Drawing.Size(971, 456);
            this.mpMSCSale.SubGridWidth = 450;
            this.mpMSCSale.TabIndex = 1;
            this.mpMSCSale.ViewControl = null;
            this.mpMSCSale.OnCellValueChangeCommited += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl.CellValueChangeCommited(this.mpMSCSale_OnCellValueChangeCommited);
            this.mpMSCSale.OnCellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.mpMSCSale_OnCellEnter);
            this.mpMSCSale.OnCellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.mpMSCSale_OnCellLeave);
            // 
            // pnlNameAddress
            // 
            this.pnlNameAddress.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlNameAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNameAddress.Controls.Add(this.txtAmtNotAdjusted);
            this.pnlNameAddress.Controls.Add(this.mPlbl5);
            this.pnlNameAddress.Controls.Add(this.pnlVou);
            this.pnlNameAddress.Controls.Add(this.pnlAddress);
            this.pnlNameAddress.Controls.Add(this.mPlbl4);
            this.pnlNameAddress.Controls.Add(this.mPlbl3);
            this.pnlNameAddress.Controls.Add(this.mPlbl2);
            this.pnlNameAddress.Controls.Add(this.mPlbl1);
            this.pnlNameAddress.Controls.Add(this.mcbCreditor);
            this.pnlNameAddress.Controls.Add(this.txtAmountReceived);
            this.pnlNameAddress.Controls.Add(this.txtNarration);
            this.pnlNameAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNameAddress.Location = new System.Drawing.Point(0, 0);
            this.pnlNameAddress.Name = "pnlNameAddress";
            this.pnlNameAddress.Size = new System.Drawing.Size(971, 144);
            this.pnlNameAddress.TabIndex = 0;
            // 
            // txtAmtNotAdjusted
            // 
            this.txtAmtNotAdjusted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmtNotAdjusted.CausesValidation = false;
            this.txtAmtNotAdjusted.Location = new System.Drawing.Point(805, 101);
            this.txtAmtNotAdjusted.Name = "txtAmtNotAdjusted";
            this.txtAmtNotAdjusted.Size = new System.Drawing.Size(141, 36);
            this.txtAmtNotAdjusted.TabIndex = 1100;
            this.txtAmtNotAdjusted.Text = "0.00";
            this.txtAmtNotAdjusted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(695, 111);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(95, 19);
            this.mPlbl5.TabIndex = 1099;
            this.mPlbl5.Text = "On Account";
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
            this.pnlVou.Location = new System.Drawing.Point(750, 4);
            this.pnlVou.Name = "pnlVou";
            this.pnlVou.Size = new System.Drawing.Size(216, 88);
            this.pnlVou.TabIndex = 1098;
            // 
            // mPlbl10
            // 
            this.mPlbl10.AutoSize = true;
            this.mPlbl10.Location = new System.Drawing.Point(11, 61);
            this.mPlbl10.Name = "mPlbl10";
            this.mPlbl10.Size = new System.Drawing.Size(76, 19);
            this.mPlbl10.TabIndex = 1083;
            this.mPlbl10.Text = "Vou &Date";
            // 
            // mPlbl9
            // 
            this.mPlbl9.AutoSize = true;
            this.mPlbl9.Location = new System.Drawing.Point(25, 34);
            this.mPlbl9.Name = "mPlbl9";
            this.mPlbl9.Size = new System.Drawing.Size(62, 19);
            this.mPlbl9.TabIndex = 1083;
            this.mPlbl9.Text = "Vou No";
            // 
            // mPlbl8
            // 
            this.mPlbl8.AutoSize = true;
            this.mPlbl8.Location = new System.Drawing.Point(9, 8);
            this.mPlbl8.Name = "mPlbl8";
            this.mPlbl8.Size = new System.Drawing.Size(78, 19);
            this.mPlbl8.TabIndex = 1083;
            this.mPlbl8.Text = "Vou Type";
            // 
            // txtVouType
            // 
            this.txtVouType.BackColor = System.Drawing.Color.Snow;
            this.txtVouType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            // pnlAddress
            // 
            this.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddress.Controls.Add(this.txtAddress1);
            this.pnlAddress.Controls.Add(this.txtAddress2);
            this.pnlAddress.Enabled = false;
            this.pnlAddress.Location = new System.Drawing.Point(154, 40);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(323, 36);
            this.pnlAddress.TabIndex = 3;
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
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(59, 110);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(82, 19);
            this.mPlbl4.TabIndex = 6;
            this.mPlbl4.Text = "Narra&tion";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(73, 82);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(68, 19);
            this.mPlbl3.TabIndex = 4;
            this.mPlbl3.Text = "&Amount";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(73, 47);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(68, 19);
            this.mPlbl2.TabIndex = 2;
            this.mPlbl2.Text = "Address";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(25, 15);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(116, 19);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "Account &Name";
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(154, 15);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = true;
            this.mcbCreditor.Size = new System.Drawing.Size(323, 26);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 1;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.ItemAddedEdited += new System.EventHandler(this.mcbCreditor_ItemAddedEdited);
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            this.mcbCreditor.SeletectIndexChanged += new System.EventHandler(this.mcbCreditor_SeletectIndexChanged);
            // 
            // txtAmountReceived
            // 
            this.txtAmountReceived.BackColor = System.Drawing.SystemColors.Window;
            this.txtAmountReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountReceived.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountReceived.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAmountReceived.Location = new System.Drawing.Point(153, 79);
            this.txtAmountReceived.MaxLength = 15;
            this.txtAmountReceived.Name = "txtAmountReceived";
            this.txtAmountReceived.Size = new System.Drawing.Size(162, 26);
            this.txtAmountReceived.TabIndex = 5;
            this.txtAmountReceived.TabStop = false;
            this.txtAmountReceived.Tag = "0.00";
            this.txtAmountReceived.Text = "0.00";
            this.txtAmountReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmountReceived.TextChanged += new System.EventHandler(this.txtAmountReceived_TextChanged);
            this.txtAmountReceived.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmountReceived_KeyDown);
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNarration.Location = new System.Drawing.Point(153, 108);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(324, 24);
            this.txtNarration.TabIndex = 7;
            this.txtNarration.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
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
            this.mpPVCTemp.Location = new System.Drawing.Point(0, 184);
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
            this.mpPVCTemp.Size = new System.Drawing.Size(569, 300);
            this.mpPVCTemp.TabIndex = 46;
            this.mpPVCTemp.Visible = false;
            // 
            // txtTotalBalance
            // 
            this.txtTotalBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalBalance.CausesValidation = false;
            this.txtTotalBalance.Location = new System.Drawing.Point(520, -1);
            this.txtTotalBalance.Name = "txtTotalBalance";
            this.txtTotalBalance.Size = new System.Drawing.Size(100, 23);
            this.txtTotalBalance.TabIndex = 1018;
            this.txtTotalBalance.Text = "label";
            this.txtTotalBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.CausesValidation = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(748, 0);
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(56, 23);
            this.txtNoOfRows.TabIndex = 1019;
            this.txtNoOfRows.Text = "label";
            this.txtNoOfRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.mpMSVC.Location = new System.Drawing.Point(0, 144);
            this.mpMSVC.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mpMSVC.MinimumSize = new System.Drawing.Size(488, 386);
            this.mpMSVC.Name = "mpMSVC";
            this.mpMSVC.NextRowColumn = 0;
            this.mpMSVC.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC.NumericColumnNames")));
            this.mpMSVC.Size = new System.Drawing.Size(971, 456);
            this.mpMSVC.SubGridWidth = 450;
            this.mpMSVC.TabIndex = 62;
            this.mpMSVC.ViewControl = null;
            this.mpMSVC.Visible = false;  
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(829, -1);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(93, 25);
            this.btnModify.TabIndex = 1101;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(383, 1);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(111, 19);
            this.psLabel1.TabIndex = 1102;
            this.psLabel1.Text = "Total Balance";
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(652, 2);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(91, 19);
            this.psLabel2.TabIndex = 1103;
            this.psLabel2.Text = "No of Rows";
            // 
            // UclCashReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclCashReceipt";
            this.Size = new System.Drawing.Size(973, 679);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlNameAddress.ResumeLayout(false);
            this.pnlNameAddress.PerformLayout();
            this.pnlVou.ResumeLayout(false);
            this.pnlVou.PerformLayout();
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlNameAddress;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl mpMSCSale;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl mpPVCTemp;
        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtAddress2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmountReceived;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNarration;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private System.Windows.Forms.Panel pnlVou;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl10;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl9;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl8;
        private System.Windows.Forms.TextBox txtVouType;
        private System.Windows.Forms.DateTimePicker datePickerBillDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouchernumber;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtTotalBalance;
        private System.ComponentModel.IContainer components;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtNoOfRows;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl mpMSVC;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton btnModify;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtAmtNotAdjusted;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel1;
    }
}
