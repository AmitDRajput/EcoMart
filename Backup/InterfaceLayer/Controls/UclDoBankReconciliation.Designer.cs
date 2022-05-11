namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclDoBankReconciliation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclDoBankReconciliation));
            this.pnlMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.txtCreditamount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtDebitAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.psLabel2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.cbOnlyNewEntries = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSCheckBox();
            this.mcbBankAccount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mPlbl3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.toDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate(this.components);
            this.mPlbl4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.fromDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate(this.components);
            this.mpMSVCBank = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlClearedDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.psLabel3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.clearedDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate(this.components);
            this.btnRemoveClearedDate = new System.Windows.Forms.Button();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.pnlClearedDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(965, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 517);
            this.MMBottomPanel.Size = new System.Drawing.Size(967, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlClearedDate);
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.mpMSVCBank);
            this.MMMainPanel.Size = new System.Drawing.Size(967, 465);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSVCBank, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlClearedDate, 0);
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.txtCreditamount);
            this.pnlMultiSelection1.Controls.Add(this.txtDebitAmount);
            this.pnlMultiSelection1.Controls.Add(this.psLabel2);
            this.pnlMultiSelection1.Controls.Add(this.psLabel1);
            this.pnlMultiSelection1.Controls.Add(this.cbOnlyNewEntries);
            this.pnlMultiSelection1.Controls.Add(this.mcbBankAccount);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl3);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl4);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(308, 141);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(534, 195);
            this.pnlMultiSelection1.TabIndex = 1063;
            // 
            // txtCreditamount
            // 
            this.txtCreditamount.BackColor = System.Drawing.Color.White;
            this.txtCreditamount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreditamount.Location = new System.Drawing.Point(137, 136);
            this.txtCreditamount.MaxLength = 15;
            this.txtCreditamount.Name = "txtCreditamount";
            this.txtCreditamount.Size = new System.Drawing.Size(130, 23);
            this.txtCreditamount.TabIndex = 1074;
            this.txtCreditamount.TabStop = false;
            this.txtCreditamount.Tag = "0.00";
            this.txtCreditamount.Text = "0.00";
            this.txtCreditamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDebitAmount
            // 
            this.txtDebitAmount.BackColor = System.Drawing.Color.White;
            this.txtDebitAmount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDebitAmount.Location = new System.Drawing.Point(137, 110);
            this.txtDebitAmount.MaxLength = 15;
            this.txtDebitAmount.Name = "txtDebitAmount";
            this.txtDebitAmount.Size = new System.Drawing.Size(130, 23);
            this.txtDebitAmount.TabIndex = 1073;
            this.txtDebitAmount.TabStop = false;
            this.txtDebitAmount.Tag = "0.00";
            this.txtDebitAmount.Text = "0.00";
            this.txtDebitAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDebitAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDebitAmount_KeyDown);
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(5, 135);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(117, 19);
            this.psLabel2.TabIndex = 1072;
            this.psLabel2.Text = "Credit Amount";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(11, 109);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(111, 19);
            this.psLabel1.TabIndex = 1071;
            this.psLabel1.Text = "Debit Amount";
            // 
            // cbOnlyNewEntries
            // 
            this.cbOnlyNewEntries.AutoSize = true;
            this.cbOnlyNewEntries.Location = new System.Drawing.Point(137, 165);
            this.cbOnlyNewEntries.Name = "cbOnlyNewEntries";
            this.cbOnlyNewEntries.Size = new System.Drawing.Size(107, 17);
            this.cbOnlyNewEntries.TabIndex = 1070;
            this.cbOnlyNewEntries.Text = "Only New Entries";
            this.cbOnlyNewEntries.UseVisualStyleBackColor = true;
            // 
            // mcbBankAccount
            // 
            this.mcbBankAccount.BackColor = System.Drawing.Color.Transparent;
            this.mcbBankAccount.ColumnWidth = null;
            this.mcbBankAccount.DataSource = null;
            this.mcbBankAccount.DisplayColumnNo = 1;
            this.mcbBankAccount.DropDownHeight = 200;
            this.mcbBankAccount.Location = new System.Drawing.Point(137, 78);
            this.mcbBankAccount.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbBankAccount.Name = "mcbBankAccount";
            this.mcbBankAccount.SelectedID = null;
            this.mcbBankAccount.ShowNew = false;
            this.mcbBankAccount.Size = new System.Drawing.Size(391, 26);
            this.mcbBankAccount.SourceDataString = null;
            this.mcbBankAccount.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbBankAccount.TabIndex = 2;
            this.mcbBankAccount.UserControlToShow = null;
            this.mcbBankAccount.ValueColumnNo = 0;
            this.mcbBankAccount.EnterKeyPressed += new System.EventHandler(this.mcbBankAccount_EnterKeyPressed);
            this.mcbBankAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcbBankAccount_KeyDown);
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(75, 81);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(47, 19);
            this.mPlbl3.TabIndex = 4;
            this.mPlbl3.Text = "Bank";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(74, 22);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(48, 19);
            this.mPlbl2.TabIndex = 0;
            this.mPlbl2.Text = "From";
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(465, 2);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 4;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection1_Click);
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(137, 45);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(129, 25);
            this.toDate1.TabIndex = 1;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(94, 51);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(28, 19);
            this.mPlbl4.TabIndex = 2;
            this.mPlbl4.Text = "To";
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(137, 18);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(129, 25);
            this.fromDate1.TabIndex = 0;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // mpMSVCBank
            // 
            this.mpMSVCBank.ApplyAlternateRowStyle = false;
            this.mpMSVCBank.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.mpMSVCBank.BackColor = System.Drawing.Color.Khaki;
            this.mpMSVCBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mpMSVCBank.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("mpMSVCBank.ConvertDatetoMonth")));
            this.mpMSVCBank.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVCBank.DateColumnNames")));
            this.mpMSVCBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMSVCBank.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVCBank.DoubleColumnNames")));
            this.mpMSVCBank.Location = new System.Drawing.Point(0, 0);
            this.mpMSVCBank.Name = "mpMSVCBank";
            this.mpMSVCBank.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVCBank.OptionalColumnNames")));
            this.mpMSVCBank.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mpMSVCBank.Size = new System.Drawing.Size(965, 463);
            this.mpMSVCBank.TabIndex = 1064;
            this.mpMSVCBank.DoubleClicked += new System.EventHandler(this.mpMSVCBank_DoubleClicked);
            this.mpMSVCBank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcbBankAccount_KeyDown);
            // 
            // pnlClearedDate
            // 
            this.pnlClearedDate.BackColor = System.Drawing.Color.White;
            this.pnlClearedDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClearedDate.Controls.Add(this.btnRemoveClearedDate);
            this.pnlClearedDate.Controls.Add(this.psLabel3);
            this.pnlClearedDate.Controls.Add(this.clearedDate);
            this.pnlClearedDate.Location = new System.Drawing.Point(292, 214);
            this.pnlClearedDate.Name = "pnlClearedDate";
            this.pnlClearedDate.Size = new System.Drawing.Size(299, 75);
            this.pnlClearedDate.TabIndex = 1065;
            this.pnlClearedDate.Click += new System.EventHandler(this.pnlClearedDate_Click);
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(19, 9);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(103, 19);
            this.psLabel3.TabIndex = 2;
            this.psLabel3.Text = "Cleared Date";
            // 
            // clearedDate
            // 
            this.clearedDate.CustomFormat = "dd/MM/yyyy";
            this.clearedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.clearedDate.Location = new System.Drawing.Point(135, 5);
            this.clearedDate.Name = "clearedDate";
            this.clearedDate.Size = new System.Drawing.Size(129, 25);
            this.clearedDate.TabIndex = 1;
            this.clearedDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.clearedDate_KeyDown);
            // 
            // btnRemoveClearedDate
            // 
            this.btnRemoveClearedDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveClearedDate.Location = new System.Drawing.Point(25, 42);
            this.btnRemoveClearedDate.Name = "btnRemoveClearedDate";
            this.btnRemoveClearedDate.Size = new System.Drawing.Size(235, 23);
            this.btnRemoveClearedDate.TabIndex = 3;
            this.btnRemoveClearedDate.Text = "Remove Cleared Date";
            this.btnRemoveClearedDate.UseVisualStyleBackColor = true;
            this.btnRemoveClearedDate.Click += new System.EventHandler(this.btnRemoveClearedDate_Click);
            // 
            // UclDoBankReconciliation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclDoBankReconciliation";
            this.Size = new System.Drawing.Size(967, 540);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.pnlClearedDate.ResumeLayout(false);
            this.pnlClearedDate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbBankAccount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate toDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate fromDate1;
        private PharmaSYSPlus.CommonLibrary.MReportGridView mpMSVCBank;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSCheckBox cbOnlyNewEntries;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtCreditamount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtDebitAmount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlClearedDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate clearedDate;
        private System.Windows.Forms.Button btnRemoveClearedDate;
    }
}
