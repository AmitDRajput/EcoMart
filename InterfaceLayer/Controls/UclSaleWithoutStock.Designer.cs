namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    partial class UclSaleWithoutStock
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.psLabel2 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtMobileNumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.mcbDoctor = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.txtPatient = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.txtDoctorShortName = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.lblDoctor = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblShortName = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblAddress = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblPatient = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblFinalBillAmount = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.rbtCash = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.txtAddress1 = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.txtAddress2 = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.mcbCreditor = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.txtBillAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtOperator = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.lblOperator = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.cbRound = new System.Windows.Forms.CheckBox();
            this.panelAmounts = new System.Windows.Forms.Panel();
            this.txtAmountforZeroVAT = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.lblNarration = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtAmountVAT12Point5Per = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtAmountVAT5Per = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtNarration = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.txtVatInput12point5per = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.lblVAT12point5Per = new System.Windows.Forms.Label();
            this.txtVatInput5per = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.lblVAT5Per = new System.Windows.Forms.Label();
            this.lblAmount = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblNetAmount = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtNetAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtTotalAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtRoundAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlVou = new System.Windows.Forms.Panel();
            this.txtVouType = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.txtVoucherSeries = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSTextBox();
            this.psLabel8 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouDate = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouNumber = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouType = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.datePickerBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtVouchernumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlAddress.SuspendLayout();
            this.panelAmounts.SuspendLayout();
            this.pnlVou.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(968, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 579);
            this.MMBottomPanel.Size = new System.Drawing.Size(970, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlTop);
            this.MMMainPanel.Controls.Add(this.panelAmounts);
            this.MMMainPanel.Size = new System.Drawing.Size(970, 527);
            this.MMMainPanel.Controls.SetChildIndex(this.panelAmounts, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlTop, 0);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.pnlVou);
            this.pnlTop.Controls.Add(this.psLabel2);
            this.pnlTop.Controls.Add(this.txtMobileNumber);
            this.pnlTop.Controls.Add(this.mcbDoctor);
            this.pnlTop.Controls.Add(this.txtPatient);
            this.pnlTop.Controls.Add(this.txtDoctorShortName);
            this.pnlTop.Controls.Add(this.lblDoctor);
            this.pnlTop.Controls.Add(this.lblShortName);
            this.pnlTop.Controls.Add(this.lblAddress);
            this.pnlTop.Controls.Add(this.lblPatient);
            this.pnlTop.Controls.Add(this.lblFinalBillAmount);
            this.pnlTop.Controls.Add(this.rbtCash);
            this.pnlTop.Controls.Add(this.pnlAddress);
            this.pnlTop.Controls.Add(this.mcbCreditor);
            this.pnlTop.Controls.Add(this.txtBillAmount);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(968, 177);
            this.pnlTop.TabIndex = 1;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(53, 87);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(33, 14);
            this.psLabel2.TabIndex = 1091;
            this.psLabel2.Text = "MOB";
            // 
            // txtMobileNumber
            // 
            this.txtMobileNumber.BackColor = System.Drawing.Color.Snow;
            this.txtMobileNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobileNumber.Location = new System.Drawing.Point(110, 86);
            this.txtMobileNumber.MaxLength = 50;
            this.txtMobileNumber.Name = "txtMobileNumber";
            this.txtMobileNumber.Size = new System.Drawing.Size(304, 24);
            this.txtMobileNumber.TabIndex = 1089;
            this.txtMobileNumber.TabStop = false;
            this.txtMobileNumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtMobileNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMobileNumber_KeyDown);
            // 
            // mcbDoctor
            // 
            this.mcbDoctor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.mcbDoctor.ColumnWidth = null;
            this.mcbDoctor.DataSource = null;
            this.mcbDoctor.DisplayColumnNo = 1;
            this.mcbDoctor.DropDownHeight = 200;
            this.mcbDoctor.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcbDoctor.Location = new System.Drawing.Point(110, 134);
            this.mcbDoctor.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbDoctor.Name = "mcbDoctor";
            this.mcbDoctor.SelectedID = null;
            this.mcbDoctor.Size = new System.Drawing.Size(412, 22);
            this.mcbDoctor.SourceDataString = null;
            this.mcbDoctor.TabIndex = 1088;
            this.mcbDoctor.UserControlToShow = null;
            this.mcbDoctor.ValueColumnNo = 0;
            this.mcbDoctor.SeletectIndexChanged += new System.EventHandler(this.mcbDoctor_SeletectIndexChanged);
            this.mcbDoctor.EnterKeyPressed += new System.EventHandler(this.mcbDoctor_EnterKeyPressed);
            // 
            // txtPatient
            // 
            this.txtPatient.BackColor = System.Drawing.Color.Snow;
            this.txtPatient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPatient.Location = new System.Drawing.Point(110, 110);
            this.txtPatient.MaxLength = 50;
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.Size = new System.Drawing.Size(407, 24);
            this.txtPatient.TabIndex = 6;
            this.txtPatient.TabStop = false;
            this.txtPatient.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtPatient.WordWrap = false;
            this.txtPatient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // txtDoctorShortName
            // 
            this.txtDoctorShortName.BackColor = System.Drawing.Color.Snow;
            this.txtDoctorShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDoctorShortName.Enabled = false;
            this.txtDoctorShortName.Location = new System.Drawing.Point(13, 144);
            this.txtDoctorShortName.MaxLength = 50;
            this.txtDoctorShortName.Name = "txtDoctorShortName";
            this.txtDoctorShortName.ReadOnly = true;
            this.txtDoctorShortName.Size = new System.Drawing.Size(15, 24);
            this.txtDoctorShortName.TabIndex = 1085;
            this.txtDoctorShortName.TabStop = false;
            this.txtDoctorShortName.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtDoctorShortName.Visible = false;
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Location = new System.Drawing.Point(42, 136);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(45, 14);
            this.lblDoctor.TabIndex = 7;
            this.lblDoctor.Text = "Docto&r";
            // 
            // lblShortName
            // 
            this.lblShortName.AutoSize = true;
            this.lblShortName.Location = new System.Drawing.Point(6, 110);
            this.lblShortName.Name = "lblShortName";
            this.lblShortName.Size = new System.Drawing.Size(71, 14);
            this.lblShortName.TabIndex = 5;
            this.lblShortName.Text = "S&hort Name";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(33, 41);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(53, 14);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "&Address";
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Location = new System.Drawing.Point(38, 16);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(45, 14);
            this.lblPatient.TabIndex = 2;
            this.lblPatient.Text = "&Patient";
            // 
            // lblFinalBillAmount
            // 
            this.lblFinalBillAmount.AutoSize = true;
            this.lblFinalBillAmount.Location = new System.Drawing.Point(828, 103);
            this.lblFinalBillAmount.Name = "lblFinalBillAmount";
            this.lblFinalBillAmount.Size = new System.Drawing.Size(72, 14);
            this.lblFinalBillAmount.TabIndex = 1050;
            this.lblFinalBillAmount.Text = "Net Amount";
            // 
            // rbtCash
            // 
            this.rbtCash.AutoSize = true;
            this.rbtCash.BackColor = System.Drawing.Color.Transparent;
            this.rbtCash.Checked = true;
            this.rbtCash.Enabled = false;
            this.rbtCash.Location = new System.Drawing.Point(517, 53);
            this.rbtCash.Name = "rbtCash";
            this.rbtCash.Size = new System.Drawing.Size(59, 21);
            this.rbtCash.TabIndex = 1049;
            this.rbtCash.TabStop = true;
            this.rbtCash.Text = "Cash";
            this.rbtCash.UseVisualStyleBackColor = false;
            // 
            // pnlAddress
            // 
            this.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddress.Controls.Add(this.txtAddress1);
            this.pnlAddress.Controls.Add(this.txtAddress2);
            this.pnlAddress.Location = new System.Drawing.Point(110, 37);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(304, 51);
            this.pnlAddress.TabIndex = 138;
            // 
            // txtAddress1
            // 
            this.txtAddress1.BackColor = System.Drawing.Color.Snow;
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress1.Location = new System.Drawing.Point(-1, 0);
            this.txtAddress1.MaxLength = 50;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(304, 24);
            this.txtAddress1.TabIndex = 0;
            this.txtAddress1.TabStop = false;
            this.txtAddress1.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress1_KeyDown);
            // 
            // txtAddress2
            // 
            this.txtAddress2.BackColor = System.Drawing.Color.Snow;
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress2.Location = new System.Drawing.Point(-1, 24);
            this.txtAddress2.MaxLength = 50;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(304, 24);
            this.txtAddress2.TabIndex = 1;
            this.txtAddress2.TabStop = false;
            this.txtAddress2.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress2_KeyDown);
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcbCreditor.Location = new System.Drawing.Point(110, 13);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.Size = new System.Drawing.Size(310, 22);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.TabIndex = 3;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.SeletectIndexChanged += new System.EventHandler(this.mcbCreditor_SeletectIndexChanged);
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            // 
            // txtBillAmount
            // 
            this.txtBillAmount.BackColor = System.Drawing.Color.Snow;
            this.txtBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBillAmount.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillAmount.ForeColor = System.Drawing.Color.DeepPink;
            this.txtBillAmount.Location = new System.Drawing.Point(767, 127);
            this.txtBillAmount.MaxLength = 15;
            this.txtBillAmount.Name = "txtBillAmount";
            this.txtBillAmount.ReadOnly = true;
            this.txtBillAmount.Size = new System.Drawing.Size(187, 40);
            this.txtBillAmount.TabIndex = 133;
            this.txtBillAmount.TabStop = false;
            this.txtBillAmount.Tag = "0.00";
            this.txtBillAmount.Text = "0.00";
            this.txtBillAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOperator
            // 
            this.txtOperator.BackColor = System.Drawing.Color.Snow;
            this.txtOperator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOperator.Location = new System.Drawing.Point(238, 223);
            this.txtOperator.MaxLength = 50;
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.PasswordChar = '*';
            this.txtOperator.Size = new System.Drawing.Size(60, 24);
            this.txtOperator.TabIndex = 1085;
            this.txtOperator.TabStop = false;
            this.txtOperator.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(151, 223);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(57, 14);
            this.lblOperator.TabIndex = 59;
            this.lblOperator.Text = "Operator";
            // 
            // cbRound
            // 
            this.cbRound.AutoSize = true;
            this.cbRound.BackColor = System.Drawing.Color.PapayaWhip;
            this.cbRound.Checked = true;
            this.cbRound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRound.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRound.Location = new System.Drawing.Point(490, 122);
            this.cbRound.Name = "cbRound";
            this.cbRound.Size = new System.Drawing.Size(73, 21);
            this.cbRound.TabIndex = 31;
            this.cbRound.Text = "Ro&und";
            this.cbRound.UseVisualStyleBackColor = false;
            // 
            // panelAmounts
            // 
            this.panelAmounts.BackColor = System.Drawing.Color.Gainsboro;
            this.panelAmounts.Controls.Add(this.txtAmountforZeroVAT);
            this.panelAmounts.Controls.Add(this.lblNarration);
            this.panelAmounts.Controls.Add(this.txtAmountVAT12Point5Per);
            this.panelAmounts.Controls.Add(this.txtAmountVAT5Per);
            this.panelAmounts.Controls.Add(this.txtNarration);
            this.panelAmounts.Controls.Add(this.txtVatInput12point5per);
            this.panelAmounts.Controls.Add(this.lblVAT12point5Per);
            this.panelAmounts.Controls.Add(this.txtVatInput5per);
            this.panelAmounts.Controls.Add(this.lblVAT5Per);
            this.panelAmounts.Controls.Add(this.lblOperator);
            this.panelAmounts.Controls.Add(this.txtOperator);
            this.panelAmounts.Controls.Add(this.lblAmount);
            this.panelAmounts.Controls.Add(this.lblNetAmount);
            this.panelAmounts.Controls.Add(this.txtNetAmount);
            this.panelAmounts.Controls.Add(this.txtTotalAmount);
            this.panelAmounts.Controls.Add(this.cbRound);
            this.panelAmounts.Controls.Add(this.txtRoundAmount);
            this.panelAmounts.Location = new System.Drawing.Point(152, 187);
            this.panelAmounts.Name = "panelAmounts";
            this.panelAmounts.Size = new System.Drawing.Size(753, 262);
            this.panelAmounts.TabIndex = 49;
            this.panelAmounts.Controls.SetChildIndex(this.txtRoundAmount, 0);
            this.panelAmounts.Controls.SetChildIndex(this.cbRound, 0);
            this.panelAmounts.Controls.SetChildIndex(this.txtTotalAmount, 0);
            this.panelAmounts.Controls.SetChildIndex(this.txtNetAmount, 0);
            this.panelAmounts.Controls.SetChildIndex(this.lblNetAmount, 0);
            this.panelAmounts.Controls.SetChildIndex(this.lblAmount, 0);
            this.panelAmounts.Controls.SetChildIndex(this.txtOperator, 0);
            this.panelAmounts.Controls.SetChildIndex(this.lblOperator, 0);
            this.panelAmounts.Controls.SetChildIndex(this.lblVAT5Per, 0);
            this.panelAmounts.Controls.SetChildIndex(this.txtVatInput5per, 0);
            this.panelAmounts.Controls.SetChildIndex(this.lblVAT12point5Per, 0);
            this.panelAmounts.Controls.SetChildIndex(this.txtVatInput12point5per, 0);
            this.panelAmounts.Controls.SetChildIndex(this.txtNarration, 0);
            this.panelAmounts.Controls.SetChildIndex(this.txtAmountVAT5Per, 0);
            this.panelAmounts.Controls.SetChildIndex(this.txtAmountVAT12Point5Per, 0);
            this.panelAmounts.Controls.SetChildIndex(this.lblNarration, 0);
            this.panelAmounts.Controls.SetChildIndex(this.txtAmountforZeroVAT, 0);
            // 
            // txtAmountforZeroVAT
            // 
            this.txtAmountforZeroVAT.BackColor = System.Drawing.Color.Snow;
            this.txtAmountforZeroVAT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountforZeroVAT.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountforZeroVAT.Location = new System.Drawing.Point(238, 15);
            this.txtAmountforZeroVAT.MaxLength = 15;
            this.txtAmountforZeroVAT.Name = "txtAmountforZeroVAT";
            this.txtAmountforZeroVAT.Size = new System.Drawing.Size(145, 26);
            this.txtAmountforZeroVAT.TabIndex = 1099;
            this.txtAmountforZeroVAT.TabStop = false;
            this.txtAmountforZeroVAT.Tag = "0.00";
            this.txtAmountforZeroVAT.Text = "0.00";
            this.txtAmountforZeroVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmountforZeroVAT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmountforZeroVAT_KeyDown);
            // 
            // lblNarration
            // 
            this.lblNarration.AutoSize = true;
            this.lblNarration.Location = new System.Drawing.Point(145, 187);
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.Size = new System.Drawing.Size(61, 14);
            this.lblNarration.TabIndex = 1098;
            this.lblNarration.Text = "&Narration";
            // 
            // txtAmountVAT12Point5Per
            // 
            this.txtAmountVAT12Point5Per.BackColor = System.Drawing.Color.Snow;
            this.txtAmountVAT12Point5Per.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountVAT12Point5Per.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountVAT12Point5Per.Location = new System.Drawing.Point(238, 79);
            this.txtAmountVAT12Point5Per.MaxLength = 15;
            this.txtAmountVAT12Point5Per.Name = "txtAmountVAT12Point5Per";
            this.txtAmountVAT12Point5Per.Size = new System.Drawing.Size(145, 26);
            this.txtAmountVAT12Point5Per.TabIndex = 1097;
            this.txtAmountVAT12Point5Per.TabStop = false;
            this.txtAmountVAT12Point5Per.Tag = "0.00";
            this.txtAmountVAT12Point5Per.Text = "0.00";
            this.txtAmountVAT12Point5Per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmountVAT12Point5Per.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmountVAT12Point5Per_KeyDown);
            // 
            // txtAmountVAT5Per
            // 
            this.txtAmountVAT5Per.BackColor = System.Drawing.Color.Snow;
            this.txtAmountVAT5Per.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountVAT5Per.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountVAT5Per.Location = new System.Drawing.Point(238, 47);
            this.txtAmountVAT5Per.MaxLength = 15;
            this.txtAmountVAT5Per.Name = "txtAmountVAT5Per";
            this.txtAmountVAT5Per.Size = new System.Drawing.Size(145, 26);
            this.txtAmountVAT5Per.TabIndex = 1096;
            this.txtAmountVAT5Per.TabStop = false;
            this.txtAmountVAT5Per.Tag = "0.00";
            this.txtAmountVAT5Per.Text = "0.00";
            this.txtAmountVAT5Per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmountVAT5Per.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmountVAT5Per_KeyDown);
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNarration.Location = new System.Drawing.Point(238, 182);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(373, 24);
            this.txtNarration.TabIndex = 1086;
            this.txtNarration.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // txtVatInput12point5per
            // 
            this.txtVatInput12point5per.BackColor = System.Drawing.Color.Snow;
            this.txtVatInput12point5per.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVatInput12point5per.Enabled = false;
            this.txtVatInput12point5per.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVatInput12point5per.Location = new System.Drawing.Point(490, 76);
            this.txtVatInput12point5per.MaxLength = 15;
            this.txtVatInput12point5per.Name = "txtVatInput12point5per";
            this.txtVatInput12point5per.ReadOnly = true;
            this.txtVatInput12point5per.Size = new System.Drawing.Size(128, 26);
            this.txtVatInput12point5per.TabIndex = 1090;
            this.txtVatInput12point5per.TabStop = false;
            this.txtVatInput12point5per.Tag = "0.00";
            this.txtVatInput12point5per.Text = "0.00";
            this.txtVatInput12point5per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblVAT12point5Per
            // 
            this.lblVAT12point5Per.AutoSize = true;
            this.lblVAT12point5Per.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVAT12point5Per.Location = new System.Drawing.Point(11, 81);
            this.lblVAT12point5Per.Name = "lblVAT12point5Per";
            this.lblVAT12point5Per.Size = new System.Drawing.Size(213, 19);
            this.lblVAT12point5Per.TabIndex = 1092;
            this.lblVAT12point5Per.Text = "Sale Amount For 13.5% VAT";
            // 
            // txtVatInput5per
            // 
            this.txtVatInput5per.BackColor = System.Drawing.Color.Snow;
            this.txtVatInput5per.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVatInput5per.Enabled = false;
            this.txtVatInput5per.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVatInput5per.Location = new System.Drawing.Point(490, 47);
            this.txtVatInput5per.MaxLength = 15;
            this.txtVatInput5per.Name = "txtVatInput5per";
            this.txtVatInput5per.ReadOnly = true;
            this.txtVatInput5per.Size = new System.Drawing.Size(128, 26);
            this.txtVatInput5per.TabIndex = 1089;
            this.txtVatInput5per.TabStop = false;
            this.txtVatInput5per.Tag = "0.00";
            this.txtVatInput5per.Text = "0.00";
            this.txtVatInput5per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblVAT5Per
            // 
            this.lblVAT5Per.AutoSize = true;
            this.lblVAT5Per.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVAT5Per.Location = new System.Drawing.Point(33, 51);
            this.lblVAT5Per.Name = "lblVAT5Per";
            this.lblVAT5Per.Size = new System.Drawing.Size(191, 19);
            this.lblVAT5Per.TabIndex = 1091;
            this.lblVAT5Per.Text = "Sale Amount For 6 % VAT";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(159, 121);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(51, 14);
            this.lblAmount.TabIndex = 1020;
            this.lblAmount.Text = "Amount";
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Location = new System.Drawing.Point(130, 155);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(72, 14);
            this.lblNetAmount.TabIndex = 1;
            this.lblNetAmount.Text = "Net Amount";
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BackColor = System.Drawing.Color.Snow;
            this.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNetAmount.Enabled = false;
            this.txtNetAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount.ForeColor = System.Drawing.Color.DeepPink;
            this.txtNetAmount.Location = new System.Drawing.Point(238, 143);
            this.txtNetAmount.MaxLength = 15;
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(145, 26);
            this.txtNetAmount.TabIndex = 2;
            this.txtNetAmount.TabStop = false;
            this.txtNetAmount.Tag = "0.00";
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.Color.Snow;
            this.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalAmount.Enabled = false;
            this.txtTotalAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTotalAmount.Location = new System.Drawing.Point(238, 113);
            this.txtTotalAmount.MaxLength = 15;
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(145, 26);
            this.txtTotalAmount.TabIndex = 1;
            this.txtTotalAmount.TabStop = false;
            this.txtTotalAmount.Tag = "0.00";
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRoundAmount
            // 
            this.txtRoundAmount.BackColor = System.Drawing.Color.Snow;
            this.txtRoundAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoundAmount.Enabled = false;
            this.txtRoundAmount.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoundAmount.Location = new System.Drawing.Point(406, 119);
            this.txtRoundAmount.MaxLength = 15;
            this.txtRoundAmount.Name = "txtRoundAmount";
            this.txtRoundAmount.Size = new System.Drawing.Size(62, 26);
            this.txtRoundAmount.TabIndex = 36;
            this.txtRoundAmount.TabStop = false;
            this.txtRoundAmount.Tag = "0.00";
            this.txtRoundAmount.Text = "0.00";
            this.txtRoundAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 19);
            this.label2.TabIndex = 1100;
            this.label2.Text = "Sale Amount For Zero VAT";
            // 
            // pnlVou
            // 
            this.pnlVou.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlVou.Controls.Add(this.txtVouType);
            this.pnlVou.Controls.Add(this.txtVoucherSeries);
            this.pnlVou.Controls.Add(this.psLabel8);
            this.pnlVou.Controls.Add(this.lblVouDate);
            this.pnlVou.Controls.Add(this.lblVouNumber);
            this.pnlVou.Controls.Add(this.lblVouType);
            this.pnlVou.Controls.Add(this.datePickerBillDate);
            this.pnlVou.Controls.Add(this.txtVouchernumber);
            this.pnlVou.Location = new System.Drawing.Point(738, 1);
            this.pnlVou.Name = "pnlVou";
            this.pnlVou.Size = new System.Drawing.Size(216, 99);
            this.pnlVou.TabIndex = 1102;
            // 
            // txtVouType
            // 
            this.txtVouType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouType.Location = new System.Drawing.Point(93, 2);
            this.txtVouType.Name = "txtVouType";
            this.txtVouType.Size = new System.Drawing.Size(100, 23);
            this.txtVouType.TabIndex = 1087;
            this.txtVouType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVoucherSeries
            // 
            this.txtVoucherSeries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVoucherSeries.Location = new System.Drawing.Point(93, 75);
            this.txtVoucherSeries.Name = "txtVoucherSeries";
            this.txtVoucherSeries.Size = new System.Drawing.Size(100, 22);
            this.txtVoucherSeries.TabIndex = 1086;
            // 
            // psLabel8
            // 
            this.psLabel8.AutoSize = true;
            this.psLabel8.Location = new System.Drawing.Point(1, 74);
            this.psLabel8.Name = "psLabel8";
            this.psLabel8.Size = new System.Drawing.Size(65, 14);
            this.psLabel8.TabIndex = 1085;
            this.psLabel8.Text = "Vou Series";
            // 
            // lblVouDate
            // 
            this.lblVouDate.AutoSize = true;
            this.lblVouDate.Location = new System.Drawing.Point(10, 54);
            this.lblVouDate.Name = "lblVouDate";
            this.lblVouDate.Size = new System.Drawing.Size(55, 14);
            this.lblVouDate.TabIndex = 1083;
            this.lblVouDate.Text = "Vou &Date";
            // 
            // lblVouNumber
            // 
            this.lblVouNumber.AutoSize = true;
            this.lblVouNumber.Location = new System.Drawing.Point(24, 29);
            this.lblVouNumber.Name = "lblVouNumber";
            this.lblVouNumber.Size = new System.Drawing.Size(46, 14);
            this.lblVouNumber.TabIndex = 1083;
            this.lblVouNumber.Text = "Vou No";
            // 
            // lblVouType
            // 
            this.lblVouType.AutoSize = true;
            this.lblVouType.Location = new System.Drawing.Point(8, 4);
            this.lblVouType.Name = "lblVouType";
            this.lblVouType.Size = new System.Drawing.Size(58, 14);
            this.lblVouType.TabIndex = 1083;
            this.lblVouType.Text = "Vou Type";
            // 
            // datePickerBillDate
            // 
            this.datePickerBillDate.CustomFormat = "dd/MM/yyyy";
            this.datePickerBillDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerBillDate.Location = new System.Drawing.Point(93, 51);
            this.datePickerBillDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerBillDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerBillDate.Name = "datePickerBillDate";
            this.datePickerBillDate.Size = new System.Drawing.Size(100, 22);
            this.datePickerBillDate.TabIndex = 0;
            this.datePickerBillDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // txtVouchernumber
            // 
            this.txtVouchernumber.BackColor = System.Drawing.Color.Snow;
            this.txtVouchernumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouchernumber.Enabled = false;
            this.txtVouchernumber.Location = new System.Drawing.Point(93, 26);
            this.txtVouchernumber.MaxLength = 50;
            this.txtVouchernumber.Name = "txtVouchernumber";
            this.txtVouchernumber.ReadOnly = true;
            this.txtVouchernumber.Size = new System.Drawing.Size(100, 24);
            this.txtVouchernumber.TabIndex = 0;
            this.txtVouchernumber.TabStop = false;
            this.txtVouchernumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtVouchernumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVouchernumber_KeyDown);
            // 
            // UclSaleWithoutStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclSaleWithoutStock";
            this.Size = new System.Drawing.Size(970, 602);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            this.panelAmounts.ResumeLayout(false);
            this.panelAmounts.PerformLayout();
            this.pnlVou.ResumeLayout(false);
            this.pnlVou.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtMobileNumber;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox mcbDoctor;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtPatient;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtDoctorShortName;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel lblDoctor;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel lblShortName;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel lblAddress;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel lblPatient;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel lblFinalBillAmount;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSRadioButton rbtCash;
        private System.Windows.Forms.Panel pnlAddress;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtAddress1;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtAddress2;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox mcbCreditor;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtBillAmount;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtOperator;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel lblOperator;
        private System.Windows.Forms.CheckBox cbRound;
        private System.Windows.Forms.Panel panelAmounts;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel lblNarration;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmountVAT12Point5Per;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmountVAT5Per;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNarration;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtVatInput12point5per;
        private System.Windows.Forms.Label lblVAT12point5Per;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtVatInput5per;
        private System.Windows.Forms.Label lblVAT5Per;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel lblNetAmount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtNetAmount;
        private System.Windows.Forms.Label label2;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtAmountforZeroVAT;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel lblAmount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotalAmount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtRoundAmount;
        private System.Windows.Forms.Panel pnlVou;
        private CommonControls.PSLableWithBorderMiddleLeft txtVouType;
        private CommonControls.PSTextBox txtVoucherSeries;
        private CommonControls.PSLabel psLabel8;
        private CommonControls.PSLabel lblVouDate;
        private CommonControls.PSLabel lblVouNumber;
        private CommonControls.PSLabel lblVouType;
        private System.Windows.Forms.DateTimePicker datePickerBillDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouchernumber;
    }
}
