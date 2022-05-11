namespace EcoMart.InterfaceLayer
{
    partial class UclJournalVoucher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclJournalVoucher));
            this.pnlNameAddress = new System.Windows.Forms.Panel();
            this.btnModify = new System.Windows.Forms.Button();
            this.pnlVou = new System.Windows.Forms.Panel();
            this.txtVouType = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.txtVoucherSeries = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.psLabel8 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouDate = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouNumber = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblVouType = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.datePickerBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtVouchernumber = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtNarration = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.mpMainSubViewControl1 = new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.txtNoOfRows = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtTotalCredit = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtTotalDebit = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlNameAddress.SuspendLayout();
            this.pnlVou.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(973, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.txtTotalCredit);
            this.MMBottomPanel.Controls.Add(this.txtTotalDebit);
            this.MMBottomPanel.Controls.Add(this.psLabel3);
            this.MMBottomPanel.Controls.Add(this.psLabel2);
            this.MMBottomPanel.Controls.Add(this.psLabel1);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 587);
            this.MMBottomPanel.Size = new System.Drawing.Size(975, 65);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblRightSideFooterMsg, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel1, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel2, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel3, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotalDebit, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtTotalCredit, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.mpMainSubViewControl1);
            this.MMMainPanel.Controls.Add(this.pnlNameAddress);
            this.MMMainPanel.Size = new System.Drawing.Size(975, 524);
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlNameAddress, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMainSubViewControl1, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(507, 0);
            // 
            // pnlNameAddress
            // 
            this.pnlNameAddress.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlNameAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNameAddress.Controls.Add(this.btnModify);
            this.pnlNameAddress.Controls.Add(this.pnlVou);
            this.pnlNameAddress.Controls.Add(this.mPlbl4);
            this.pnlNameAddress.Controls.Add(this.txtNarration);
            this.pnlNameAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNameAddress.Location = new System.Drawing.Point(0, 0);
            this.pnlNameAddress.Name = "pnlNameAddress";
            this.pnlNameAddress.Size = new System.Drawing.Size(973, 85);
            this.pnlNameAddress.TabIndex = 62;
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(460, 15);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(93, 44);
            this.btnModify.TabIndex = 1103;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Visible = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
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
            this.pnlVou.Location = new System.Drawing.Point(563, 7);
            this.pnlVou.Name = "pnlVou";
            this.pnlVou.Size = new System.Drawing.Size(405, 66);
            this.pnlVou.TabIndex = 1102;
            // 
            // txtVouType
            // 
            this.txtVouType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouType.Location = new System.Drawing.Point(93, 8);
            this.txtVouType.Name = "txtVouType";
            this.txtVouType.Size = new System.Drawing.Size(100, 23);
            this.txtVouType.TabIndex = 1087;
            this.txtVouType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVoucherSeries
            // 
            this.txtVoucherSeries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVoucherSeries.IsNumericDataSet = false;
            this.txtVoucherSeries.Location = new System.Drawing.Point(295, 33);
            this.txtVoucherSeries.Name = "txtVoucherSeries";
            this.txtVoucherSeries.Size = new System.Drawing.Size(100, 22);
            this.txtVoucherSeries.TabIndex = 1086;
            // 
            // psLabel8
            // 
            this.psLabel8.AutoSize = true;
            this.psLabel8.Location = new System.Drawing.Point(203, 36);
            this.psLabel8.Name = "psLabel8";
            this.psLabel8.Size = new System.Drawing.Size(70, 16);
            this.psLabel8.TabIndex = 1085;
            this.psLabel8.Text = "Vou Series";
            // 
            // lblVouDate
            // 
            this.lblVouDate.AutoSize = true;
            this.lblVouDate.Location = new System.Drawing.Point(212, 11);
            this.lblVouDate.Name = "lblVouDate";
            this.lblVouDate.Size = new System.Drawing.Size(61, 16);
            this.lblVouDate.TabIndex = 1083;
            this.lblVouDate.Text = "Vou &Date";
            // 
            // lblVouNumber
            // 
            this.lblVouNumber.AutoSize = true;
            this.lblVouNumber.Location = new System.Drawing.Point(21, 36);
            this.lblVouNumber.Name = "lblVouNumber";
            this.lblVouNumber.Size = new System.Drawing.Size(50, 16);
            this.lblVouNumber.TabIndex = 1083;
            this.lblVouNumber.Text = "Vou No";
            // 
            // lblVouType
            // 
            this.lblVouType.AutoSize = true;
            this.lblVouType.Location = new System.Drawing.Point(8, 11);
            this.lblVouType.Name = "lblVouType";
            this.lblVouType.Size = new System.Drawing.Size(63, 16);
            this.lblVouType.TabIndex = 1083;
            this.lblVouType.Text = "Vou Type";
            // 
            // datePickerBillDate
            // 
            this.datePickerBillDate.CustomFormat = "dd/MM/yyyy";
            this.datePickerBillDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerBillDate.Location = new System.Drawing.Point(295, 8);
            this.datePickerBillDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerBillDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerBillDate.Name = "datePickerBillDate";
            this.datePickerBillDate.Size = new System.Drawing.Size(100, 22);
            this.datePickerBillDate.TabIndex = 0;
            this.datePickerBillDate.Value = new System.DateTime(2017, 4, 3, 0, 0, 0, 0);
            // 
            // txtVouchernumber
            // 
            this.txtVouchernumber.BackColor = System.Drawing.Color.Snow;
            this.txtVouchernumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVouchernumber.Enabled = false;
            this.txtVouchernumber.Location = new System.Drawing.Point(93, 32);
            this.txtVouchernumber.MaxLength = 50;
            this.txtVouchernumber.Name = "txtVouchernumber";
            this.txtVouchernumber.ReadOnly = true;
            this.txtVouchernumber.Size = new System.Drawing.Size(100, 24);
            this.txtVouchernumber.TabIndex = 0;
            this.txtVouchernumber.TabStop = false;
            this.txtVouchernumber.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtVouchernumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVouchernumber_KeyDown);
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(24, 17);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(68, 16);
            this.mPlbl4.TabIndex = 1087;
            this.mPlbl4.Text = "Narra&tion";
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.Location = new System.Drawing.Point(98, 17);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(345, 24);
            this.txtNarration.TabIndex = 3;
            this.txtNarration.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // mpMainSubViewControl1
            // 
            this.mpMainSubViewControl1.AutoScroll = true;
            this.mpMainSubViewControl1.BackColor = System.Drawing.Color.Linen;
            this.mpMainSubViewControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mpMainSubViewControl1.DataSource = null;
            this.mpMainSubViewControl1.DataSourceMain = null;
            this.mpMainSubViewControl1.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.DateColumnNames")));
            this.mpMainSubViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMainSubViewControl1.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.DoubleColumnNames")));
            this.mpMainSubViewControl1.EditedTempDataList = null;
            this.mpMainSubViewControl1.Filter = null;
            this.mpMainSubViewControl1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMainSubViewControl1.IsAllowDelete = true;
            this.mpMainSubViewControl1.IsAllowNewRow = true;
            this.mpMainSubViewControl1.Location = new System.Drawing.Point(0, 85);
            this.mpMainSubViewControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mpMainSubViewControl1.MinimumSize = new System.Drawing.Size(487, 321);
            this.mpMainSubViewControl1.Name = "mpMainSubViewControl1";
            this.mpMainSubViewControl1.NextRowColumn = 4;
            this.mpMainSubViewControl1.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.NumericColumnNames")));
            this.mpMainSubViewControl1.Size = new System.Drawing.Size(973, 437);
            this.mpMainSubViewControl1.SubGridWidth = 400;
            this.mpMainSubViewControl1.TabIndex = 144;
            this.mpMainSubViewControl1.ViewControl = null;
            this.mpMainSubViewControl1.OnCellValueChangeCommited += new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl.CellValueChangeCommited(this.mpMainSubViewControl1_OnCellValueChangeCommited);
            this.mpMainSubViewControl1.OnDetailsFilled += new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl.DetailsFilled(this.mpMainSubViewControl1_OnDetailsFilled);
            this.mpMainSubViewControl1.OnRowDeleted += new System.EventHandler(this.mpMainSubViewControl1_OnRowDeleted);
            this.mpMainSubViewControl1.OnTABKeyPressed += new System.EventHandler(this.mpMainSubViewControl1_OnTABKeyPressed);
            this.mpMainSubViewControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mpMainSubViewControl1_KeyDown);
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.CausesValidation = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(891, -1);
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(56, 23);
            this.txtNoOfRows.TabIndex = 1025;
            this.txtNoOfRows.Text = "0";
            this.txtNoOfRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalCredit
            // 
            this.txtTotalCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalCredit.CausesValidation = false;
            this.txtTotalCredit.Location = new System.Drawing.Point(724, -1);
            this.txtTotalCredit.Name = "txtTotalCredit";
            this.txtTotalCredit.Size = new System.Drawing.Size(115, 23);
            this.txtTotalCredit.TabIndex = 1024;
            this.txtTotalCredit.Text = "0.00";
            this.txtTotalCredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalDebit
            // 
            this.txtTotalDebit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalDebit.CausesValidation = false;
            this.txtTotalDebit.Location = new System.Drawing.Point(552, -1);
            this.txtTotalDebit.Name = "txtTotalDebit";
            this.txtTotalDebit.Size = new System.Drawing.Size(115, 23);
            this.txtTotalDebit.TabIndex = 1023;
            this.txtTotalDebit.Text = "0.00";
            this.txtTotalDebit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(849, 3);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(41, 16);
            this.psLabel3.TabIndex = 1022;
            this.psLabel3.Text = "Rows";
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(679, 3);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(46, 16);
            this.psLabel2.TabIndex = 1021;
            this.psLabel2.Text = "Credit";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(480, 3);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(74, 16);
            this.psLabel1.TabIndex = 1020;
            this.psLabel1.Text = "Total Debit";
            // 
            // UclJournalVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclJournalVoucher";
            this.Size = new System.Drawing.Size(975, 652);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlNameAddress.ResumeLayout(false);
            this.pnlNameAddress.PerformLayout();
            this.pnlVou.ResumeLayout(false);
            this.pnlVou.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlNameAddress;
        private System.Windows.Forms.Panel pnlVou;
        private CommonControls.PSLableWithBorderMiddleLeft txtVouType;
        private CommonControls.PSTextBox txtVoucherSeries;
        private CommonControls.PSLabel psLabel8;
        private CommonControls.PSLabel lblVouDate;
        private CommonControls.PSLabel lblVouNumber;
        private CommonControls.PSLabel lblVouType;
        private System.Windows.Forms.DateTimePicker datePickerBillDate;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtVouchernumber;
        private CommonControls.PSLabel mPlbl4;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtNarration;
        private CommonControls.PSMainSubViewControl mpMainSubViewControl1;
        private CommonControls.PSLableWithBorderMiddleRight txtNoOfRows;
        private CommonControls.PSLableWithBorderMiddleRight txtTotalCredit;
        private CommonControls.PSLableWithBorderMiddleRight txtTotalDebit;
        private CommonControls.PSLabel psLabel3;
        private CommonControls.PSLabel psLabel2;
        private CommonControls.PSLabel psLabel1;
        private System.Windows.Forms.Button btnModify;
    }
}
