namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclStockIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStockIn));
            this.dgtemp = new System.Windows.Forms.DataGridView();
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.ttStkIn = new System.Windows.Forms.ToolTip(this.components);
            this.lblNoofRows = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlAmounts = new System.Windows.Forms.Panel();
            this.mPlbl6 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtNetAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.mPlbl13 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl11 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl7 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtDiscPercent = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtTotalAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtRoundAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.cbRound = new System.Windows.Forms.CheckBox();
            this.txtVatInput12point5per = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtVatInput5per = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtDiscAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
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
            this.mpPVC1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgtemp)).BeginInit();
            this.pnlAmounts.SuspendLayout();
            this.pnlVouTypeNo.SuspendLayout();
            this.pnlVou.SuspendLayout();
            this.pnlAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(968, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Controls.Add(this.lblNoofRows);
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 667);
            this.MMBottomPanel.Size = new System.Drawing.Size(970, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblNoofRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.mpPVC1);
            this.MMMainPanel.Controls.Add(this.pnlVouTypeNo);
            this.MMMainPanel.Controls.Add(this.dgtemp);
            this.MMMainPanel.Controls.Add(this.pnlAmounts);
            this.MMMainPanel.Size = new System.Drawing.Size(970, 615);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlAmounts, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgtemp, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlVouTypeNo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpPVC1, 0);
            // 
            // dgtemp
            // 
            this.dgtemp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgtemp.Location = new System.Drawing.Point(15, 399);
            this.dgtemp.Name = "dgtemp";
            this.dgtemp.Size = new System.Drawing.Size(57, 28);
            this.dgtemp.TabIndex = 7;
            this.dgtemp.Visible = false;
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.Snow;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(580, -1);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(61, 26);
            this.txtNoOfRows.TabIndex = 27;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNoofRows
            // 
            this.lblNoofRows.AutoSize = true;
            this.lblNoofRows.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRows.Location = new System.Drawing.Point(500, 2);
            this.lblNoofRows.Name = "lblNoofRows";
            this.lblNoofRows.Size = new System.Drawing.Size(74, 15);
            this.lblNoofRows.TabIndex = 1010;
            this.lblNoofRows.Text = "No Of Rows";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(19, 4);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(2, 16);
            this.lblMessage.TabIndex = 1009;
            // 
            // pnlAmounts
            // 
            this.pnlAmounts.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlAmounts.Controls.Add(this.mPlbl6);
            this.pnlAmounts.Controls.Add(this.txtNetAmount);
            this.pnlAmounts.Controls.Add(this.mPlbl13);
            this.pnlAmounts.Controls.Add(this.mPlbl11);
            this.pnlAmounts.Controls.Add(this.mPlbl7);
            this.pnlAmounts.Controls.Add(this.mPlbl5);
            this.pnlAmounts.Controls.Add(this.mPlbl4);
            this.pnlAmounts.Controls.Add(this.txtDiscPercent);
            this.pnlAmounts.Controls.Add(this.txtTotalAmount);
            this.pnlAmounts.Controls.Add(this.txtRoundAmount);
            this.pnlAmounts.Controls.Add(this.cbRound);
            this.pnlAmounts.Controls.Add(this.txtVatInput12point5per);
            this.pnlAmounts.Controls.Add(this.txtVatInput5per);
            this.pnlAmounts.Controls.Add(this.txtDiscAmount);
            this.pnlAmounts.Controls.Add(this.txtAmount);
            this.pnlAmounts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAmounts.Location = new System.Drawing.Point(0, 490);
            this.pnlAmounts.Name = "pnlAmounts";
            this.pnlAmounts.Size = new System.Drawing.Size(968, 123);
            this.pnlAmounts.TabIndex = 1016;
            // 
            // mPlbl6
            // 
            this.mPlbl6.AutoSize = true;
            this.mPlbl6.Location = new System.Drawing.Point(713, 95);
            this.mPlbl6.Name = "mPlbl6";
            this.mPlbl6.Size = new System.Drawing.Size(97, 19);
            this.mPlbl6.TabIndex = 40;
            this.mPlbl6.Text = "Net Amount";
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BackColor = System.Drawing.Color.White;
            this.txtNetAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount.ForeColor = System.Drawing.Color.DeepPink;
            this.txtNetAmount.Location = new System.Drawing.Point(808, 91);
            this.txtNetAmount.MaxLength = 15;
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(146, 26);
            this.txtNetAmount.TabIndex = 39;
            this.txtNetAmount.TabStop = false;
            this.txtNetAmount.Tag = "0.00";
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNetAmount.Visible = false;
            // 
            // mPlbl13
            // 
            this.mPlbl13.AutoSize = true;
            this.mPlbl13.Location = new System.Drawing.Point(757, 7);
            this.mPlbl13.Name = "mPlbl13";
            this.mPlbl13.Size = new System.Drawing.Size(48, 19);
            this.mPlbl13.TabIndex = 37;
            this.mPlbl13.Text = "Total";
            // 
            // mPlbl11
            // 
            this.mPlbl11.AutoSize = true;
            this.mPlbl11.Location = new System.Drawing.Point(739, 65);
            this.mPlbl11.Name = "mPlbl11";
            this.mPlbl11.Size = new System.Drawing.Size(68, 19);
            this.mPlbl11.TabIndex = 36;
            this.mPlbl11.Text = "Amount";
            // 
            // mPlbl7
            // 
            this.mPlbl7.AutoSize = true;
            this.mPlbl7.Location = new System.Drawing.Point(669, 36);
            this.mPlbl7.Name = "mPlbl7";
            this.mPlbl7.Size = new System.Drawing.Size(60, 19);
            this.mPlbl7.TabIndex = 35;
            this.mPlbl7.Text = "Less %";
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(10, 37);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(146, 19);
            this.mPlbl5.TabIndex = 33;
            this.mPlbl5.Text = "VAT &INPUT 12.5 %";
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(34, 9);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(120, 19);
            this.mPlbl4.TabIndex = 32;
            this.mPlbl4.Text = "&VAT INPUT 5%";
            // 
            // txtDiscPercent
            // 
            this.txtDiscPercent.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscPercent.Location = new System.Drawing.Point(731, 32);
            this.txtDiscPercent.MaxLength = 15;
            this.txtDiscPercent.Name = "txtDiscPercent";
            this.txtDiscPercent.Size = new System.Drawing.Size(74, 26);
            this.txtDiscPercent.TabIndex = 31;
            this.txtDiscPercent.Tag = "0.00";
            this.txtDiscPercent.Text = "0.00";
            this.txtDiscPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscPercent.Visible = false;
            this.txtDiscPercent.TextChanged += new System.EventHandler(this.txtDiscPercent_TextChanged);
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.Color.White;
            this.txtTotalAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(808, 61);
            this.txtTotalAmount.MaxLength = 15;
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(146, 26);
            this.txtTotalAmount.TabIndex = 29;
            this.txtTotalAmount.Tag = "0.00";
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalAmount.Visible = false;
            // 
            // txtRoundAmount
            // 
            this.txtRoundAmount.BackColor = System.Drawing.Color.Snow;
            this.txtRoundAmount.Enabled = false;
            this.txtRoundAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoundAmount.Location = new System.Drawing.Point(586, 66);
            this.txtRoundAmount.MaxLength = 6;
            this.txtRoundAmount.Name = "txtRoundAmount";
            this.txtRoundAmount.Size = new System.Drawing.Size(73, 26);
            this.txtRoundAmount.TabIndex = 28;
            this.txtRoundAmount.TabStop = false;
            this.txtRoundAmount.Tag = "0.00";
            this.txtRoundAmount.Text = "0.00";
            this.txtRoundAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRoundAmount.Visible = false;
            // 
            // cbRound
            // 
            this.cbRound.AutoSize = true;
            this.cbRound.BackColor = System.Drawing.Color.PapayaWhip;
            this.cbRound.Checked = true;
            this.cbRound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRound.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRound.Location = new System.Drawing.Point(516, 69);
            this.cbRound.Name = "cbRound";
            this.cbRound.Size = new System.Drawing.Size(66, 19);
            this.cbRound.TabIndex = 23;
            this.cbRound.Text = "Round";
            this.cbRound.UseVisualStyleBackColor = false;
            this.cbRound.Visible = false;
            this.cbRound.CheckedChanged += new System.EventHandler(this.cbRound_CheckedChanged);
            // 
            // txtVatInput12point5per
            // 
            this.txtVatInput12point5per.BackColor = System.Drawing.Color.White;
            this.txtVatInput12point5per.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVatInput12point5per.Location = new System.Drawing.Point(152, 33);
            this.txtVatInput12point5per.MaxLength = 15;
            this.txtVatInput12point5per.Name = "txtVatInput12point5per";
            this.txtVatInput12point5per.Size = new System.Drawing.Size(146, 26);
            this.txtVatInput12point5per.TabIndex = 1;
            this.txtVatInput12point5per.TabStop = false;
            this.txtVatInput12point5per.Tag = "0.00";
            this.txtVatInput12point5per.Text = "0.00";
            this.txtVatInput12point5per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVatInput12point5per.Visible = false;
            this.txtVatInput12point5per.TextChanged += new System.EventHandler(this.txtVatInput12point5per_TextChanged);
            this.txtVatInput12point5per.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVatInput12point5per_KeyDown);
            // 
            // txtVatInput5per
            // 
            this.txtVatInput5per.BackColor = System.Drawing.Color.White;
            this.txtVatInput5per.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVatInput5per.Location = new System.Drawing.Point(152, 4);
            this.txtVatInput5per.MaxLength = 15;
            this.txtVatInput5per.Name = "txtVatInput5per";
            this.txtVatInput5per.Size = new System.Drawing.Size(146, 26);
            this.txtVatInput5per.TabIndex = 0;
            this.txtVatInput5per.TabStop = false;
            this.txtVatInput5per.Tag = "0.00";
            this.txtVatInput5per.Text = "0.00";
            this.txtVatInput5per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVatInput5per.Visible = false;
            this.txtVatInput5per.TextChanged += new System.EventHandler(this.txtVatInput5per_TextChanged);
            this.txtVatInput5per.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVatInput5per_KeyDown);
            // 
            // txtDiscAmount
            // 
            this.txtDiscAmount.BackColor = System.Drawing.Color.White;
            this.txtDiscAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscAmount.Location = new System.Drawing.Point(808, 32);
            this.txtDiscAmount.MaxLength = 15;
            this.txtDiscAmount.Name = "txtDiscAmount";
            this.txtDiscAmount.Size = new System.Drawing.Size(146, 26);
            this.txtDiscAmount.TabIndex = 3;
            this.txtDiscAmount.Tag = "0.00";
            this.txtDiscAmount.Text = "0.00";
            this.txtDiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscAmount.Visible = false;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.White;
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(808, 3);
            this.txtAmount.MaxLength = 15;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(146, 26);
            this.txtAmount.TabIndex = 10;
            this.txtAmount.TabStop = false;
            this.txtAmount.Tag = "0.00";
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlVouTypeNo
            // 
            this.pnlVouTypeNo.BackColor = System.Drawing.Color.Gainsboro;
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
            this.pnlVouTypeNo.Size = new System.Drawing.Size(968, 133);
            this.pnlVouTypeNo.TabIndex = 1017;
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
            this.pnlVou.Location = new System.Drawing.Point(749, 3);
            this.pnlVou.Name = "pnlVou";
            this.pnlVou.Size = new System.Drawing.Size(216, 88);
            this.pnlVou.TabIndex = 1093;
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
            // mPlbl12
            // 
            this.mPlbl12.AutoSize = true;
            this.mPlbl12.Location = new System.Drawing.Point(602, 40);
            this.mPlbl12.Name = "mPlbl12";
            this.mPlbl12.Size = new System.Drawing.Size(110, 19);
            this.mPlbl12.TabIndex = 1092;
            this.mPlbl12.Text = "Final Amount";
            // 
            // txtBillAmount
            // 
            this.txtBillAmount.BackColor = System.Drawing.Color.Snow;
            this.txtBillAmount.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillAmount.ForeColor = System.Drawing.Color.DeepPink;
            this.txtBillAmount.Location = new System.Drawing.Point(525, 63);
            this.txtBillAmount.MaxLength = 15;
            this.txtBillAmount.Name = "txtBillAmount";
            this.txtBillAmount.ReadOnly = true;
            this.txtBillAmount.Size = new System.Drawing.Size(187, 40);
            this.txtBillAmount.TabIndex = 1091;
            this.txtBillAmount.TabStop = false;
            this.txtBillAmount.Tag = "0.00";
            this.txtBillAmount.Text = "0.00";
            this.txtBillAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(65, 83);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(82, 19);
            this.mPlbl3.TabIndex = 1090;
            this.mPlbl3.Text = "&Narration";
            // 
            // pnlAddress
            // 
            this.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddress.Controls.Add(this.txtAddress1);
            this.pnlAddress.Controls.Add(this.txtAddress2);
            this.pnlAddress.Enabled = false;
            this.pnlAddress.Location = new System.Drawing.Point(162, 38);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(323, 36);
            this.pnlAddress.TabIndex = 1089;
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
            this.mPlbl2.Location = new System.Drawing.Point(79, 43);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(68, 19);
            this.mPlbl2.TabIndex = 138;
            this.mPlbl2.Text = "Address";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(31, 12);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(116, 19);
            this.mPlbl1.TabIndex = 137;
            this.mPlbl1.Text = "&Account Name";
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(162, 10);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = false;
            this.mcbCreditor.Size = new System.Drawing.Size(360, 26);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 135;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.ItemAddedEdited += new System.EventHandler(this.mcbCreditor_ItemAddedEdited);
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            this.mcbCreditor.SeletectIndexChanged += new System.EventHandler(this.mcbCreditor_SeletectIndexChanged);
            this.mcbCreditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcbCreditor_KeyDown);
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.Location = new System.Drawing.Point(162, 81);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(323, 24);
            this.txtNarration.TabIndex = 2;
            this.txtNarration.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // mpPVC1
            // 
            this.mpPVC1.AllowNewBatch = true;
            this.mpPVC1.AutoScroll = true;
            this.mpPVC1.BatchColumnName = "Col_BatchNumber";
            this.mpPVC1.BatchGridShowColumnName = null;
            this.mpPVC1.BatchListGridWidth = 710;
            this.mpPVC1.DataSourceBatchList = null;
            this.mpPVC1.DataSourceMain = null;
            this.mpPVC1.DataSourceProductList = null;
            this.mpPVC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpPVC1.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpPVC1.DoubleColumnNames")));
            this.mpPVC1.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpPVC1.IsAllowDelete = true;
            this.mpPVC1.IsAllowNewRow = false;
            this.mpPVC1.Location = new System.Drawing.Point(0, 133);
            this.mpPVC1.MainGridSoldQuantityColumnName = "";
            this.mpPVC1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpPVC1.MinimumSize = new System.Drawing.Size(390, 260);
            this.mpPVC1.ModuleNumber = PharmaSYSRetailPlus.Common.ModuleNumber.None;
            this.mpPVC1.Name = "mpPVC1";
            this.mpPVC1.NewRowColumnName = null;
            this.mpPVC1.NextRowColumn = 0;
            this.mpPVC1.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpPVC1.NumericColumnNames")));
            this.mpPVC1.OperationMode = PharmaSYSRetailPlus.Common.OperationMode.None;
            this.mpPVC1.ProductGridClosingStockColumnName = "";
            this.mpPVC1.ProductListFilter = null;
            this.mpPVC1.ProductListGridWidth = 624;
            this.mpPVC1.Size = new System.Drawing.Size(968, 357);
            this.mpPVC1.TabIndex = 1018;
            this.mpPVC1.OnBatchSelected += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl.BatchSelected(this.mpPVC1_OnBatchSelected);
            this.mpPVC1.OnCellValueChangeCommited += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl.CellValueChangeCommited(this.mpPVC1_OnCellValueChangeCommited);
            this.mpPVC1.OnNewBatchClicked += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl.NewBatchClicked(this.mpPVC1_OnNewBatchClicked);
            this.mpPVC1.OnTABKeyPressed += new System.EventHandler(this.mpPVC1_OnTABKeyPressed);
            this.mpPVC1.OnProductSelected += new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl.ProductSelected(this.mpPVC1_OnProductSelected);
            this.mpPVC1.OnRowDeleted += new System.EventHandler(this.mpPVC1_OnRowDeleted);
            // 
            // UclStockIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "UclStockIn";
            this.Size = new System.Drawing.Size(970, 690);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgtemp)).EndInit();
            this.pnlAmounts.ResumeLayout(false);
            this.pnlAmounts.PerformLayout();
            this.pnlVouTypeNo.ResumeLayout(false);
            this.pnlVouTypeNo.PerformLayout();
            this.pnlVou.ResumeLayout(false);
            this.pnlVou.PerformLayout();
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.DataGridView dgtemp;
        private System.Windows.Forms.ToolTip ttStkIn;
        private System.Windows.Forms.Label lblNoofRows;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlAmounts;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtDiscPercent;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotalAmount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtRoundAmount;
        private System.Windows.Forms.CheckBox cbRound;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtVatInput12point5per;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtVatInput5per;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtDiscAmount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmount;
        private System.Windows.Forms.Panel pnlVouTypeNo;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNarration;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl mpPVC1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtAddress2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl12;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtBillAmount;
        private System.Windows.Forms.Panel pnlVou;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl10;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl9;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl8;
        private System.Windows.Forms.TextBox txtVouType;
        private System.Windows.Forms.DateTimePicker datePickerBillDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouchernumber;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl11;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl7;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl13;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl6;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtNetAmount;
    }
}
