namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclCounterSaleEdit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclCounterSaleEdit));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.GivenDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.pnlFinal = new System.Windows.Forms.Panel();
            this.lblSaveCustNo = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.btnDelete = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton();
            this.txtAmountfor12VAT = new System.Windows.Forms.TextBox();
            this.rbtCash = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.txtAmountfor5VAT = new System.Windows.Forms.TextBox();
            this.pnlPatientDrDetails = new System.Windows.Forms.Panel();
            this.lblDocAddress = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtDoctorAddress = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.mcbDoctor = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.txtMobileNumber = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSTextBox();
            this.lblMobileNumber = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtAddress = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.txtPatientName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.txtSaleAmountForDiscount = new System.Windows.Forms.TextBox();
            this.pnlBillAmount = new System.Windows.Forms.Panel();
            this.txtBillAmount2 = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.mPlbl12 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblOperator = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblDiscPer = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtDiscAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtDiscPercent = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtOperator = new System.Windows.Forms.TextBox();
            this.cbSavePatient = new System.Windows.Forms.CheckBox();
            this.txtNetAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtAmountforZeroVAT = new System.Windows.Forms.TextBox();
            this.txtVatInput12point5 = new System.Windows.Forms.TextBox();
            this.txtVatInput5 = new System.Windows.Forms.TextBox();
            this.txtsavecustno = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.gbsaletype = new System.Windows.Forms.GroupBox();
            this.rbtCreditCard = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtCreditStatement = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtCashCredit = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.dgvReportList = new System.Windows.Forms.DataGridView();
            this.PrintGrid = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl();
            this.btnClearPatient = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton();
            this.btnClearDoctor = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlFinal.SuspendLayout();
            this.pnlPatientDrDetails.SuspendLayout();
            this.pnlBillAmount.SuspendLayout();
            this.gbsaletype.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportList)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(964, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 520);
            this.MMBottomPanel.Size = new System.Drawing.Size(966, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlFinal);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Controls.Add(this.PrintGrid);
            this.MMMainPanel.Size = new System.Drawing.Size(966, 468);
            this.MMMainPanel.Controls.SetChildIndex(this.PrintGrid, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlGo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlFinal, 0);
            // 
            // pnlGo
            // 
            this.pnlGo.BackColor = System.Drawing.Color.AliceBlue;
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.GivenDate);
            this.pnlGo.Controls.Add(this.lblDate);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(964, 33);
            this.pnlGo.TabIndex = 1048;
            // 
            // GivenDate
            // 
            this.GivenDate.CalendarFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GivenDate.CustomFormat = "";
            this.GivenDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GivenDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.GivenDate.Location = new System.Drawing.Point(95, 2);
            this.GivenDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.GivenDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.GivenDate.Name = "GivenDate";
            this.GivenDate.Size = new System.Drawing.Size(122, 26);
            this.GivenDate.TabIndex = 1059;
            this.GivenDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.GivenDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GivenDate_KeyDown);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(52, 7);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(35, 15);
            this.lblDate.TabIndex = 1057;
            this.lblDate.Text = "Date";
            // 
            // pnlFinal
            // 
            this.pnlFinal.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlFinal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFinal.Controls.Add(this.lblSaveCustNo);
            this.pnlFinal.Controls.Add(this.btnDelete);
            this.pnlFinal.Controls.Add(this.txtAmountfor12VAT);
            this.pnlFinal.Controls.Add(this.rbtCash);
            this.pnlFinal.Controls.Add(this.txtAmountfor5VAT);
            this.pnlFinal.Controls.Add(this.pnlPatientDrDetails);
            this.pnlFinal.Controls.Add(this.txtSaleAmountForDiscount);
            this.pnlFinal.Controls.Add(this.pnlBillAmount);
            this.pnlFinal.Controls.Add(this.txtAmountforZeroVAT);
            this.pnlFinal.Controls.Add(this.txtVatInput12point5);
            this.pnlFinal.Controls.Add(this.txtVatInput5);
            this.pnlFinal.Controls.Add(this.txtsavecustno);
            this.pnlFinal.Controls.Add(this.gbsaletype);
            this.pnlFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlFinal.ForeColor = System.Drawing.Color.Black;
            this.pnlFinal.Location = new System.Drawing.Point(0, 294);
            this.pnlFinal.Name = "pnlFinal";
            this.pnlFinal.Size = new System.Drawing.Size(964, 184);
            this.pnlFinal.TabIndex = 1049;
            // 
            // lblSaveCustNo
            // 
            this.lblSaveCustNo.AutoSize = true;
            this.lblSaveCustNo.Location = new System.Drawing.Point(14, 9);
            this.lblSaveCustNo.Name = "lblSaveCustNo";
            this.lblSaveCustNo.Size = new System.Drawing.Size(95, 19);
            this.lblSaveCustNo.TabIndex = 147;
            this.lblSaveCustNo.Text = "Voucher No";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(798, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(78, 28);
            this.btnDelete.TabIndex = 147;
            this.btnDelete.Text = "Del&ete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            // 
            // txtAmountfor12VAT
            // 
            this.txtAmountfor12VAT.Location = new System.Drawing.Point(932, 8);
            this.txtAmountfor12VAT.Name = "txtAmountfor12VAT";
            this.txtAmountfor12VAT.Size = new System.Drawing.Size(5, 20);
            this.txtAmountfor12VAT.TabIndex = 1040;
            this.txtAmountfor12VAT.UseSystemPasswordChar = true;
            this.txtAmountfor12VAT.Visible = false;
            // 
            // rbtCash
            // 
            this.rbtCash.AutoSize = true;
            this.rbtCash.BackColor = System.Drawing.Color.PapayaWhip;
            this.rbtCash.Checked = true;
            this.rbtCash.Location = new System.Drawing.Point(235, 9);
            this.rbtCash.Name = "rbtCash";
            this.rbtCash.Size = new System.Drawing.Size(59, 21);
            this.rbtCash.TabIndex = 0;
            this.rbtCash.TabStop = true;
            this.rbtCash.Text = "&Cash";
            this.rbtCash.UseVisualStyleBackColor = true;
            this.rbtCash.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtCash_KeyDown);
            // 
            // txtAmountfor5VAT
            // 
            this.txtAmountfor5VAT.Location = new System.Drawing.Point(921, 7);
            this.txtAmountfor5VAT.Name = "txtAmountfor5VAT";
            this.txtAmountfor5VAT.Size = new System.Drawing.Size(5, 20);
            this.txtAmountfor5VAT.TabIndex = 1044;
            this.txtAmountfor5VAT.UseSystemPasswordChar = true;
            this.txtAmountfor5VAT.Visible = false;
            // 
            // pnlPatientDrDetails
            // 
            this.pnlPatientDrDetails.Controls.Add(this.btnClearPatient);
            this.pnlPatientDrDetails.Controls.Add(this.btnClearDoctor);
            this.pnlPatientDrDetails.Controls.Add(this.lblDocAddress);
            this.pnlPatientDrDetails.Controls.Add(this.txtDoctorAddress);
            this.pnlPatientDrDetails.Controls.Add(this.mcbDoctor);
            this.pnlPatientDrDetails.Controls.Add(this.txtMobileNumber);
            this.pnlPatientDrDetails.Controls.Add(this.lblMobileNumber);
            this.pnlPatientDrDetails.Controls.Add(this.mPlbl4);
            this.pnlPatientDrDetails.Controls.Add(this.mPlbl2);
            this.pnlPatientDrDetails.Controls.Add(this.mPlbl1);
            this.pnlPatientDrDetails.Controls.Add(this.txtAddress);
            this.pnlPatientDrDetails.Controls.Add(this.txtPatientName);
            this.pnlPatientDrDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPatientDrDetails.Location = new System.Drawing.Point(1, 34);
            this.pnlPatientDrDetails.Name = "pnlPatientDrDetails";
            this.pnlPatientDrDetails.Size = new System.Drawing.Size(614, 138);
            this.pnlPatientDrDetails.TabIndex = 1043;
            // 
            // lblDocAddress
            // 
            this.lblDocAddress.AutoSize = true;
            this.lblDocAddress.Location = new System.Drawing.Point(5, 109);
            this.lblDocAddress.Name = "lblDocAddress";
            this.lblDocAddress.Size = new System.Drawing.Size(122, 19);
            this.lblDocAddress.TabIndex = 1095;
            this.lblDocAddress.Text = "Doctor Address";
            // 
            // txtDoctorAddress
            // 
            this.txtDoctorAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDoctorAddress.ColumnWidth = null;
            this.txtDoctorAddress.DataSource = null;
            this.txtDoctorAddress.DisplayColumnNo = 1;
            this.txtDoctorAddress.DropDownHeight = 200;
            this.txtDoctorAddress.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoctorAddress.Location = new System.Drawing.Point(135, 105);
            this.txtDoctorAddress.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtDoctorAddress.Name = "txtDoctorAddress";
            this.txtDoctorAddress.SelectedID = null;
            this.txtDoctorAddress.Size = new System.Drawing.Size(318, 26);
            this.txtDoctorAddress.SourceDataString = null;
            this.txtDoctorAddress.TabIndex = 1094;
            this.txtDoctorAddress.UserControlToShow = null;
            this.txtDoctorAddress.ValueColumnNo = 0;
            this.txtDoctorAddress.EnterKeyPressed += new System.EventHandler(this.txtDoctorAddress_EnterKeyPressed);
            this.txtDoctorAddress.UpArrowKeyPressed += new System.EventHandler(this.txtDoctorAddress_UpArrowKeyPressed);
            // 
            // mcbDoctor
            // 
            this.mcbDoctor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.mcbDoctor.ColumnWidth = null;
            this.mcbDoctor.DataSource = null;
            this.mcbDoctor.DisplayColumnNo = 1;
            this.mcbDoctor.DropDownHeight = 200;
            this.mcbDoctor.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcbDoctor.Location = new System.Drawing.Point(135, 79);
            this.mcbDoctor.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbDoctor.Name = "mcbDoctor";
            this.mcbDoctor.SelectedID = null;
            this.mcbDoctor.Size = new System.Drawing.Size(318, 26);
            this.mcbDoctor.SourceDataString = null;
            this.mcbDoctor.TabIndex = 1093;
            this.mcbDoctor.UserControlToShow = null;
            this.mcbDoctor.ValueColumnNo = 0;
            this.mcbDoctor.EnterKeyPressed += new System.EventHandler(this.mcbDoctor_EnterKeyPressed);
            this.mcbDoctor.UpArrowKeyPressed += new System.EventHandler(this.mcbDoctor_UpArrowKeyPressed);
            // 
            // txtMobileNumber
            // 
            this.txtMobileNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobileNumber.Location = new System.Drawing.Point(135, 56);
            this.txtMobileNumber.Name = "txtMobileNumber";
            this.txtMobileNumber.Size = new System.Drawing.Size(312, 23);
            this.txtMobileNumber.TabIndex = 10;
            this.txtMobileNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMobileNumber_KeyDown);
            // 
            // lblMobileNumber
            // 
            this.lblMobileNumber.AutoSize = true;
            this.lblMobileNumber.Location = new System.Drawing.Point(59, 63);
            this.lblMobileNumber.Name = "lblMobileNumber";
            this.lblMobileNumber.Size = new System.Drawing.Size(68, 19);
            this.lblMobileNumber.TabIndex = 9;
            this.lblMobileNumber.Text = "MOB No";
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(68, 90);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(59, 19);
            this.mPlbl4.TabIndex = 6;
            this.mPlbl4.Text = "Doctor";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(59, 37);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(68, 19);
            this.mPlbl2.TabIndex = 2;
            this.mPlbl2.Text = "Address";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(18, 9);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(109, 19);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "Patient Name";
            // 
            // txtAddress
            // 
            this.txtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress.ColumnWidth = null;
            this.txtAddress.DataSource = null;
            this.txtAddress.DisplayColumnNo = 1;
            this.txtAddress.DropDownHeight = 200;
            this.txtAddress.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(135, 30);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.SelectedID = null;
            this.txtAddress.Size = new System.Drawing.Size(318, 26);
            this.txtAddress.SourceDataString = null;
            this.txtAddress.TabIndex = 3;
            this.txtAddress.UserControlToShow = null;
            this.txtAddress.ValueColumnNo = 0;
            this.txtAddress.EnterKeyPressed += new System.EventHandler(this.txtAddress_EnterKeyPressed);
            this.txtAddress.UpArrowKeyPressed += new System.EventHandler(this.txtAddress_UpArrowKeyPressed);
            // 
            // txtPatientName
            // 
            this.txtPatientName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPatientName.ColumnWidth = null;
            this.txtPatientName.DataSource = null;
            this.txtPatientName.DisplayColumnNo = 1;
            this.txtPatientName.DropDownHeight = 200;
            this.txtPatientName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.Location = new System.Drawing.Point(135, 4);
            this.txtPatientName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.SelectedID = null;
            this.txtPatientName.Size = new System.Drawing.Size(318, 26);
            this.txtPatientName.SourceDataString = null;
            this.txtPatientName.TabIndex = 1;
            this.txtPatientName.UserControlToShow = null;
            this.txtPatientName.ValueColumnNo = 0;
            this.txtPatientName.EnterKeyPressed += new System.EventHandler(this.txtPatientName_EnterKeyPressed);
            // 
            // txtSaleAmountForDiscount
            // 
            this.txtSaleAmountForDiscount.Location = new System.Drawing.Point(910, 7);
            this.txtSaleAmountForDiscount.Name = "txtSaleAmountForDiscount";
            this.txtSaleAmountForDiscount.Size = new System.Drawing.Size(5, 20);
            this.txtSaleAmountForDiscount.TabIndex = 1042;
            this.txtSaleAmountForDiscount.UseSystemPasswordChar = true;
            this.txtSaleAmountForDiscount.Visible = false;
            // 
            // pnlBillAmount
            // 
            this.pnlBillAmount.Controls.Add(this.txtBillAmount2);
            this.pnlBillAmount.Controls.Add(this.mPlbl12);
            this.pnlBillAmount.Controls.Add(this.lblOperator);
            this.pnlBillAmount.Controls.Add(this.lblDiscPer);
            this.pnlBillAmount.Controls.Add(this.mPlbl5);
            this.pnlBillAmount.Controls.Add(this.txtDiscAmount);
            this.pnlBillAmount.Controls.Add(this.txtDiscPercent);
            this.pnlBillAmount.Controls.Add(this.txtOperator);
            this.pnlBillAmount.Controls.Add(this.cbSavePatient);
            this.pnlBillAmount.Controls.Add(this.txtNetAmount);
            this.pnlBillAmount.Location = new System.Drawing.Point(621, 33);
            this.pnlBillAmount.Name = "pnlBillAmount";
            this.pnlBillAmount.Size = new System.Drawing.Size(339, 139);
            this.pnlBillAmount.TabIndex = 3;
            // 
            // txtBillAmount2
            // 
            this.txtBillAmount2.BackColor = System.Drawing.Color.Snow;
            this.txtBillAmount2.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillAmount2.ForeColor = System.Drawing.Color.DeepPink;
            this.txtBillAmount2.Location = new System.Drawing.Point(142, 48);
            this.txtBillAmount2.MaxLength = 15;
            this.txtBillAmount2.Name = "txtBillAmount2";
            this.txtBillAmount2.ReadOnly = true;
            this.txtBillAmount2.Size = new System.Drawing.Size(146, 37);
            this.txtBillAmount2.TabIndex = 17;
            this.txtBillAmount2.TabStop = false;
            this.txtBillAmount2.Tag = "0.00";
            this.txtBillAmount2.Text = "0.00";
            this.txtBillAmount2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mPlbl12
            // 
            this.mPlbl12.AutoSize = true;
            this.mPlbl12.Location = new System.Drawing.Point(36, 59);
            this.mPlbl12.Name = "mPlbl12";
            this.mPlbl12.Size = new System.Drawing.Size(97, 19);
            this.mPlbl12.TabIndex = 16;
            this.mPlbl12.Text = "Net Amount";
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(127, 91);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(76, 19);
            this.lblOperator.TabIndex = 15;
            this.lblOperator.Text = "Operator";
            this.lblOperator.Visible = false;
            // 
            // lblDiscPer
            // 
            this.lblDiscPer.AutoSize = true;
            this.lblDiscPer.Location = new System.Drawing.Point(8, 27);
            this.lblDiscPer.Name = "lblDiscPer";
            this.lblDiscPer.Size = new System.Drawing.Size(56, 19);
            this.lblDiscPer.TabIndex = 0;
            this.lblDiscPer.Text = "Disc%";
            this.lblDiscPer.Visible = false;
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(65, 4);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(68, 19);
            this.mPlbl5.TabIndex = 12;
            this.mPlbl5.Text = "Amount";
            this.mPlbl5.Visible = false;
            // 
            // txtDiscAmount
            // 
            this.txtDiscAmount.BackColor = System.Drawing.Color.White;
            this.txtDiscAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscAmount.Location = new System.Drawing.Point(142, 25);
            this.txtDiscAmount.MaxLength = 15;
            this.txtDiscAmount.Name = "txtDiscAmount";
            this.txtDiscAmount.Size = new System.Drawing.Size(146, 22);
            this.txtDiscAmount.TabIndex = 1;
            this.txtDiscAmount.Tag = "0.00";
            this.txtDiscAmount.Text = "0.00";
            this.txtDiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscAmount.Visible = false;
            // 
            // txtDiscPercent
            // 
            this.txtDiscPercent.BackColor = System.Drawing.Color.White;
            this.txtDiscPercent.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscPercent.Location = new System.Drawing.Point(75, 25);
            this.txtDiscPercent.MaxLength = 15;
            this.txtDiscPercent.Name = "txtDiscPercent";
            this.txtDiscPercent.Size = new System.Drawing.Size(61, 22);
            this.txtDiscPercent.TabIndex = 1;
            this.txtDiscPercent.Tag = "0.00";
            this.txtDiscPercent.Text = "0.00";
            this.txtDiscPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscPercent.Visible = false;
            // 
            // txtOperator
            // 
            this.txtOperator.Location = new System.Drawing.Point(203, 89);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.PasswordChar = '*';
            this.txtOperator.Size = new System.Drawing.Size(83, 20);
            this.txtOperator.TabIndex = 5;
            this.txtOperator.UseSystemPasswordChar = true;
            this.txtOperator.Visible = false;
            // 
            // cbSavePatient
            // 
            this.cbSavePatient.AutoSize = true;
            this.cbSavePatient.BackColor = System.Drawing.Color.Linen;
            this.cbSavePatient.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSavePatient.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSavePatient.Location = new System.Drawing.Point(7, 90);
            this.cbSavePatient.Name = "cbSavePatient";
            this.cbSavePatient.Size = new System.Drawing.Size(111, 21);
            this.cbSavePatient.TabIndex = 4;
            this.cbSavePatient.Text = "Save Patient";
            this.cbSavePatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSavePatient.UseVisualStyleBackColor = false;
            this.cbSavePatient.Visible = false;
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BackColor = System.Drawing.Color.White;
            this.txtNetAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount.Location = new System.Drawing.Point(142, 2);
            this.txtNetAmount.MaxLength = 15;
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(146, 22);
            this.txtNetAmount.TabIndex = 3;
            this.txtNetAmount.TabStop = false;
            this.txtNetAmount.Tag = "0.00";
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNetAmount.Visible = false;
            // 
            // txtAmountforZeroVAT
            // 
            this.txtAmountforZeroVAT.Location = new System.Drawing.Point(902, 7);
            this.txtAmountforZeroVAT.Name = "txtAmountforZeroVAT";
            this.txtAmountforZeroVAT.Size = new System.Drawing.Size(5, 20);
            this.txtAmountforZeroVAT.TabIndex = 1041;
            this.txtAmountforZeroVAT.UseSystemPasswordChar = true;
            this.txtAmountforZeroVAT.Visible = false;
            // 
            // txtVatInput12point5
            // 
            this.txtVatInput12point5.Location = new System.Drawing.Point(894, 7);
            this.txtVatInput12point5.Name = "txtVatInput12point5";
            this.txtVatInput12point5.Size = new System.Drawing.Size(5, 20);
            this.txtVatInput12point5.TabIndex = 1040;
            this.txtVatInput12point5.UseSystemPasswordChar = true;
            this.txtVatInput12point5.Visible = false;
            // 
            // txtVatInput5
            // 
            this.txtVatInput5.Location = new System.Drawing.Point(886, 7);
            this.txtVatInput5.Name = "txtVatInput5";
            this.txtVatInput5.Size = new System.Drawing.Size(5, 20);
            this.txtVatInput5.TabIndex = 1039;
            this.txtVatInput5.UseSystemPasswordChar = true;
            this.txtVatInput5.Visible = false;
            // 
            // txtsavecustno
            // 
            this.txtsavecustno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsavecustno.Location = new System.Drawing.Point(120, 6);
            this.txtsavecustno.MaxLength = 1;
            this.txtsavecustno.Name = "txtsavecustno";
            this.txtsavecustno.Size = new System.Drawing.Size(87, 24);
            this.txtsavecustno.TabIndex = 0;
            this.txtsavecustno.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtsavecustno.TextChanged += new System.EventHandler(this.txtsavecustno_TextChanged);
            // 
            // gbsaletype
            // 
            this.gbsaletype.Controls.Add(this.rbtCreditCard);
            this.gbsaletype.Controls.Add(this.rbtCreditStatement);
            this.gbsaletype.Controls.Add(this.rbtCashCredit);
            this.gbsaletype.Enabled = false;
            this.gbsaletype.Location = new System.Drawing.Point(306, -2);
            this.gbsaletype.Name = "gbsaletype";
            this.gbsaletype.Size = new System.Drawing.Size(374, 32);
            this.gbsaletype.TabIndex = 1;
            this.gbsaletype.TabStop = false;
            this.gbsaletype.Visible = false;
            // 
            // rbtCreditCard
            // 
            this.rbtCreditCard.AutoSize = true;
            this.rbtCreditCard.BackColor = System.Drawing.Color.PapayaWhip;
            this.rbtCreditCard.Location = new System.Drawing.Point(264, 9);
            this.rbtCreditCard.Name = "rbtCreditCard";
            this.rbtCreditCard.Size = new System.Drawing.Size(102, 21);
            this.rbtCreditCard.TabIndex = 4;
            this.rbtCreditCard.TabStop = true;
            this.rbtCreditCard.Text = "CreditCard";
            this.rbtCreditCard.UseVisualStyleBackColor = true;
            this.rbtCreditCard.Visible = false;
            this.rbtCreditCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtCreditCard_KeyDown);
            // 
            // rbtCreditStatement
            // 
            this.rbtCreditStatement.AutoSize = true;
            this.rbtCreditStatement.BackColor = System.Drawing.Color.PapayaWhip;
            this.rbtCreditStatement.Location = new System.Drawing.Point(145, 9);
            this.rbtCreditStatement.Name = "rbtCreditStatement";
            this.rbtCreditStatement.Size = new System.Drawing.Size(113, 21);
            this.rbtCreditStatement.TabIndex = 3;
            this.rbtCreditStatement.TabStop = true;
            this.rbtCreditStatement.Text = "Credit STMT";
            this.rbtCreditStatement.UseVisualStyleBackColor = true;
            this.rbtCreditStatement.Visible = false;
            this.rbtCreditStatement.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtCreditStatement_KeyDown);
            // 
            // rbtCashCredit
            // 
            this.rbtCashCredit.AutoSize = true;
            this.rbtCashCredit.BackColor = System.Drawing.Color.PapayaWhip;
            this.rbtCashCredit.Location = new System.Drawing.Point(70, 9);
            this.rbtCashCredit.Name = "rbtCashCredit";
            this.rbtCashCredit.Size = new System.Drawing.Size(69, 21);
            this.rbtCashCredit.TabIndex = 2;
            this.rbtCashCredit.TabStop = true;
            this.rbtCashCredit.Text = "C&redit";
            this.rbtCashCredit.UseVisualStyleBackColor = true;
            this.rbtCashCredit.Visible = false;
            this.rbtCashCredit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtCashCredit_KeyDown);
            // 
            // dgvReportList
            // 
            this.dgvReportList.AllowUserToAddRows = false;
            this.dgvReportList.AllowUserToDeleteRows = false;
            this.dgvReportList.AllowUserToResizeColumns = false;
            this.dgvReportList.AllowUserToResizeRows = false;
            this.dgvReportList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReportList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReportList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvReportList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReportList.Size = new System.Drawing.Size(964, 433);
            this.dgvReportList.TabIndex = 1050;
            this.dgvReportList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReportList_CellContentClick);
            // 
            // PrintGrid
            // 
            this.PrintGrid.AllowNewBatch = false;
            this.PrintGrid.AutoScroll = true;
            this.PrintGrid.BackColor = System.Drawing.Color.Azure;
            this.PrintGrid.BatchColumnName = "Col_BatchNumber";
            this.PrintGrid.BatchGridShowColumnName = null;
            this.PrintGrid.BatchListGridWidth = 610;
            this.PrintGrid.DataSourceBatchList = null;
            this.PrintGrid.DataSourceMain = null;
            this.PrintGrid.DataSourceProductList = null;
            this.PrintGrid.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("PrintGrid.DoubleColumnNames")));
            this.PrintGrid.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintGrid.IsAllowDelete = true;
            this.PrintGrid.IsAllowNewRow = false;
            this.PrintGrid.Location = new System.Drawing.Point(15, 40);
            this.PrintGrid.MainGridSoldQuantityColumnName = "";
            this.PrintGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PrintGrid.MinimumSize = new System.Drawing.Size(390, 260);
            this.PrintGrid.ModuleNumber = PharmaSYSRetailPlus.Common.ModuleNumber.None;
            this.PrintGrid.Name = "PrintGrid";
            this.PrintGrid.NewRowColumnName = null;
            this.PrintGrid.NextRowColumn = 0;
            this.PrintGrid.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("PrintGrid.NumericColumnNames")));
            this.PrintGrid.OperationMode = PharmaSYSRetailPlus.Common.OperationMode.None;
            this.PrintGrid.ProductGridClosingStockColumnName = "";
            this.PrintGrid.ProductListFilter = null;
            this.PrintGrid.ProductListGridWidth = 700;
            this.PrintGrid.Size = new System.Drawing.Size(390, 260);
            this.PrintGrid.TabIndex = 1051;
            this.PrintGrid.Visible = false;
            // 
            // btnClearPatient
            // 
            this.btnClearPatient.Location = new System.Drawing.Point(450, 9);
            this.btnClearPatient.Name = "btnClearPatient";
            this.btnClearPatient.Size = new System.Drawing.Size(63, 40);
            this.btnClearPatient.TabIndex = 1097;
            this.btnClearPatient.Text = "Clear Patient";
            this.btnClearPatient.UseVisualStyleBackColor = true;
            this.btnClearPatient.Click += new System.EventHandler(this.btnClearPatient_Click);
            // 
            // btnClearDoctor
            // 
            this.btnClearDoctor.Location = new System.Drawing.Point(451, 84);
            this.btnClearDoctor.Name = "btnClearDoctor";
            this.btnClearDoctor.Size = new System.Drawing.Size(63, 40);
            this.btnClearDoctor.TabIndex = 1096;
            this.btnClearDoctor.Text = "Clear Doctor";
            this.btnClearDoctor.UseVisualStyleBackColor = true;
            this.btnClearDoctor.Click += new System.EventHandler(this.btnClearDoctor_Click);
            // 
            // UclCounterSaleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclCounterSaleEdit";
            this.Size = new System.Drawing.Size(966, 543);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.pnlFinal.ResumeLayout(false);
            this.pnlFinal.PerformLayout();
            this.pnlPatientDrDetails.ResumeLayout(false);
            this.pnlPatientDrDetails.PerformLayout();
            this.pnlBillAmount.ResumeLayout(false);
            this.pnlBillAmount.PerformLayout();
            this.gbsaletype.ResumeLayout(false);
            this.gbsaletype.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGo;
        private System.Windows.Forms.DateTimePicker GivenDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Panel pnlFinal;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblSaveCustNo;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton btnDelete;
        private System.Windows.Forms.TextBox txtAmountfor12VAT;
        private System.Windows.Forms.TextBox txtAmountfor5VAT;
        private System.Windows.Forms.Panel pnlPatientDrDetails;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtAddress;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtPatientName;
        private System.Windows.Forms.TextBox txtSaleAmountForDiscount;
        private System.Windows.Forms.Panel pnlBillAmount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtBillAmount2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl12;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblOperator;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblDiscPer;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtDiscAmount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtDiscPercent;
        private System.Windows.Forms.TextBox txtOperator;
        private System.Windows.Forms.CheckBox cbSavePatient;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtNetAmount;
        private System.Windows.Forms.TextBox txtAmountforZeroVAT;
        private System.Windows.Forms.TextBox txtVatInput12point5;
        private System.Windows.Forms.TextBox txtVatInput5;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtsavecustno;
        private System.Windows.Forms.GroupBox gbsaletype;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton rbtCash;
        private System.Windows.Forms.DataGridView dgvReportList;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton rbtCreditCard;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton rbtCreditStatement;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton rbtCashCredit;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSProductViewControl PrintGrid;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSTextBox txtMobileNumber;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblMobileNumber;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtDoctorAddress;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox mcbDoctor;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblDocAddress;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton btnClearPatient;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton btnClearDoctor;
    }
}
