namespace EcoMart.InterfaceLayer
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
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtCreditamount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtDebitAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.cbOnlyNewEntries = new EcoMart.InterfaceLayer.CommonControls.PSCheckBox();
            this.mcbBankAccount = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mPlbl3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.pnlClearedDate = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.btnRemoveClearedDate = new System.Windows.Forms.Button();
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.clearedDate = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.pnlGo = new System.Windows.Forms.Panel();
            this.txtOPTag = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblViewFrom = new System.Windows.Forms.Label();
            this.txtOpeningBalance = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewtext = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtClosingBalance = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.txtCLTag = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mpMSVCBank = new System.Windows.Forms.DataGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.pnlClearedDate.SuspendLayout();
            this.pnlGo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mpMSVCBank)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(965, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtCLTag);
            this.MMBottomPanel.Controls.Add(this.txtClosingBalance);
            this.MMBottomPanel.Controls.Add(this.label5);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 477);
            this.MMBottomPanel.Size = new System.Drawing.Size(967, 63);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.label5, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtClosingBalance, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtCLTag, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlClearedDate);
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.mpMSVCBank);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(967, 414);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlGo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSVCBank, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlClearedDate, 0);
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.psLabel1);
            this.pnlMultiSelection1.Controls.Add(this.txtCreditamount);
            this.pnlMultiSelection1.Controls.Add(this.txtDebitAmount);
            this.pnlMultiSelection1.Controls.Add(this.psLabel2);
            this.pnlMultiSelection1.Controls.Add(this.cbOnlyNewEntries);
            this.pnlMultiSelection1.Controls.Add(this.mcbBankAccount);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl3);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl4);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(296, 126);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(534, 195);
            this.pnlMultiSelection1.TabIndex = 1063;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(29, 109);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(93, 17);
            this.psLabel1.TabIndex = 1071;
            this.psLabel1.Text = "Debit Amount";
            // 
            // txtCreditamount
            // 
            this.txtCreditamount.BackColor = System.Drawing.Color.White;
            this.txtCreditamount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.txtCreditamount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCreditamount_KeyDown);
            // 
            // txtDebitAmount
            // 
            this.txtDebitAmount.BackColor = System.Drawing.Color.White;
            this.txtDebitAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.psLabel2.Location = new System.Drawing.Point(24, 135);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(98, 17);
            this.psLabel2.TabIndex = 1072;
            this.psLabel2.Text = "Credit Amount";
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
            this.mcbBankAccount.Size = new System.Drawing.Size(391, 22);
            this.mcbBankAccount.SourceDataString = null;
            this.mcbBankAccount.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbBankAccount.TabIndex = 2;
            this.mcbBankAccount.UserControlToShow = null;
            this.mcbBankAccount.ValueColumnNo = 0;
            this.mcbBankAccount.EnterKeyPressed += new System.EventHandler(this.mcbBankAccount_EnterKeyPressed);
            this.mcbBankAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcbBankAccount_KeyDown);
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(77, 81);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(39, 17);
            this.mPlbl3.TabIndex = 4;
            this.mPlbl3.Text = "Bank";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(74, 22);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(41, 17);
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
            this.toDate1.Size = new System.Drawing.Size(115, 24);
            this.toDate1.TabIndex = 1;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(91, 51);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(22, 17);
            this.mPlbl4.TabIndex = 2;
            this.mPlbl4.Text = "To";
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(137, 18);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(115, 24);
            this.fromDate1.TabIndex = 0;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // pnlClearedDate
            // 
            this.pnlClearedDate.BackColor = System.Drawing.Color.White;
            this.pnlClearedDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClearedDate.Controls.Add(this.btnRemoveClearedDate);
            this.pnlClearedDate.Controls.Add(this.psLabel3);
            this.pnlClearedDate.Controls.Add(this.clearedDate);
            this.pnlClearedDate.Location = new System.Drawing.Point(258, 176);
            this.pnlClearedDate.Name = "pnlClearedDate";
            this.pnlClearedDate.Size = new System.Drawing.Size(299, 75);
            this.pnlClearedDate.TabIndex = 1065;
            this.pnlClearedDate.Click += new System.EventHandler(this.pnlClearedDate_Click);
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
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(19, 9);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(86, 17);
            this.psLabel3.TabIndex = 2;
            this.psLabel3.Text = "Cleared Date";
            // 
            // clearedDate
            // 
            this.clearedDate.CustomFormat = "dd/MM/yyyy";
            this.clearedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.clearedDate.Location = new System.Drawing.Point(135, 5);
            this.clearedDate.Name = "clearedDate";
            this.clearedDate.Size = new System.Drawing.Size(115, 24);
            this.clearedDate.TabIndex = 1;
            this.clearedDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.clearedDate_KeyDown);
            this.clearedDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.clearedDate_KeyPress);
            // 
            // pnlGo
            // 
            this.pnlGo.BackColor = System.Drawing.Color.Plum;
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.txtOPTag);
            this.pnlGo.Controls.Add(this.lblViewFrom);
            this.pnlGo.Controls.Add(this.txtOpeningBalance);
            this.pnlGo.Controls.Add(this.label4);
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.psLabel4);
            this.pnlGo.Controls.Add(this.txtViewtext);
            this.pnlGo.Controls.Add(this.label3);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(965, 33);
            this.pnlGo.TabIndex = 1066;
            // 
            // txtOPTag
            // 
            this.txtOPTag.AutoSize = true;
            this.txtOPTag.Location = new System.Drawing.Point(637, 7);
            this.txtOPTag.Name = "txtOPTag";
            this.txtOPTag.Size = new System.Drawing.Size(22, 17);
            this.txtOPTag.TabIndex = 1078;
            this.txtOPTag.Text = "To";
            // 
            // lblViewFrom
            // 
            this.lblViewFrom.AutoSize = true;
            this.lblViewFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewFrom.Location = new System.Drawing.Point(664, 7);
            this.lblViewFrom.Name = "lblViewFrom";
            this.lblViewFrom.Size = new System.Drawing.Size(40, 16);
            this.lblViewFrom.TabIndex = 1077;
            this.lblViewFrom.Text = "From";
            // 
            // txtOpeningBalance
            // 
            this.txtOpeningBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpeningBalance.CausesValidation = false;
            this.txtOpeningBalance.Location = new System.Drawing.Point(469, 3);
            this.txtOpeningBalance.Name = "txtOpeningBalance";
            this.txtOpeningBalance.Size = new System.Drawing.Size(163, 23);
            this.txtOpeningBalance.TabIndex = 1076;
            this.txtOpeningBalance.Text = "label";
            this.txtOpeningBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(416, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 1075;
            this.label4.Text = "OP.Bal.";
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(858, 3);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1074;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(709, 3);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1073;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(824, 3);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(22, 17);
            this.psLabel4.TabIndex = 1072;
            this.psLabel4.Text = "To";
            // 
            // txtViewtext
            // 
            this.txtViewtext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewtext.Location = new System.Drawing.Point(52, 3);
            this.txtViewtext.Name = "txtViewtext";
            this.txtViewtext.Size = new System.Drawing.Size(349, 22);
            this.txtViewtext.TabIndex = 1064;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 16);
            this.label3.TabIndex = 1063;
            this.label3.Text = "Bank";
            // 
            // txtClosingBalance
            // 
            this.txtClosingBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClosingBalance.CausesValidation = false;
            this.txtClosingBalance.Location = new System.Drawing.Point(692, -2);
            this.txtClosingBalance.Name = "txtClosingBalance";
            this.txtClosingBalance.Size = new System.Drawing.Size(176, 23);
            this.txtClosingBalance.TabIndex = 1078;
            this.txtClosingBalance.Text = "label";
            this.txtClosingBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(582, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 16);
            this.label5.TabIndex = 1077;
            this.label5.Text = "Closing Balance";
            // 
            // txtCLTag
            // 
            this.txtCLTag.AutoSize = true;
            this.txtCLTag.Location = new System.Drawing.Point(874, 3);
            this.txtCLTag.Name = "txtCLTag";
            this.txtCLTag.Size = new System.Drawing.Size(22, 17);
            this.txtCLTag.TabIndex = 1079;
            this.txtCLTag.Text = "To";
            // 
            // mpMSVCBank
            // 
            this.mpMSVCBank.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mpMSVCBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mpMSVCBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMSVCBank.Location = new System.Drawing.Point(0, 33);
            this.mpMSVCBank.Name = "mpMSVCBank";
            this.mpMSVCBank.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mpMSVCBank.Size = new System.Drawing.Size(965, 379);
            this.mpMSVCBank.TabIndex = 1067;
            this.mpMSVCBank.DoubleClick += new System.EventHandler(this.mpMSVCBank_DoubleClicked);
            this.mpMSVCBank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mpMSVCBank_KeyDown);
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
            this.MMMainPanel.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.pnlClearedDate.ResumeLayout(false);
            this.pnlClearedDate.PerformLayout();
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mpMSVCBank)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbBankAccount;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.ToDate toDate1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private EcoMart.InterfaceLayer.CommonControls.FromDate fromDate1;
        private EcoMart.InterfaceLayer.CommonControls.PSCheckBox cbOnlyNewEntries;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtCreditamount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtDebitAmount;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlClearedDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private EcoMart.InterfaceLayer.CommonControls.FromDate clearedDate;
        private System.Windows.Forms.Button btnRemoveClearedDate;
        private System.Windows.Forms.Panel pnlGo;
        private CommonControls.PSLableWithBorderMiddleRight txtOpeningBalance;
        private System.Windows.Forms.Label label4;
        private CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private CommonControls.PSLabel psLabel4;
        private CommonControls.PSTextBox txtViewtext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblViewFrom;
        private CommonControls.PSLableWithBorderMiddleRight txtClosingBalance;
        private System.Windows.Forms.Label label5;
        private CommonControls.PSLabel txtCLTag;
        private CommonControls.PSLabel txtOPTag;
        private System.Windows.Forms.DataGridView mpMSVCBank;
    }
}
