namespace PharmaSYSRetailPlus.Reporting.Base
{
    partial class DateVoucherTypeControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateVoucherTypeControl));
            this.pnlMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.gboxVoucherType = new System.Windows.Forms.GroupBox();
            this.rbtnCreditStatement = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnVoucher = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnCredit = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnCash = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnAll = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.btnGo = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.toDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate(this.components);
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.pnlMultiSelection1.SuspendLayout();
            this.gboxVoucherType.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.gboxVoucherType);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.btnGo);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(1, 0);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(342, 154);
            this.pnlMultiSelection1.TabIndex = 1;
            // 
            // gboxVoucherType
            // 
            this.gboxVoucherType.BackColor = System.Drawing.Color.Transparent;
            this.gboxVoucherType.Controls.Add(this.rbtnCreditStatement);
            this.gboxVoucherType.Controls.Add(this.rbtnVoucher);
            this.gboxVoucherType.Controls.Add(this.rbtnCredit);
            this.gboxVoucherType.Controls.Add(this.rbtnCash);
            this.gboxVoucherType.Controls.Add(this.rbtnAll);
            this.gboxVoucherType.Location = new System.Drawing.Point(11, 78);
            this.gboxVoucherType.Name = "gboxVoucherType";
            this.gboxVoucherType.Size = new System.Drawing.Size(310, 60);
            this.gboxVoucherType.TabIndex = 2;
            this.gboxVoucherType.TabStop = false;
            // 
            // rbtnCreditStatement
            // 
            this.rbtnCreditStatement.AutoSize = true;
            this.rbtnCreditStatement.BackColor = System.Drawing.Color.White;
            this.rbtnCreditStatement.Location = new System.Drawing.Point(11, 35);
            this.rbtnCreditStatement.Name = "rbtnCreditStatement";
            this.rbtnCreditStatement.Size = new System.Drawing.Size(141, 21);
            this.rbtnCreditStatement.TabIndex = 9;
            this.rbtnCreditStatement.TabStop = true;
            this.rbtnCreditStatement.Text = "Credit Statement";
            this.rbtnCreditStatement.UseVisualStyleBackColor = true;
            // 
            // rbtnVoucher
            // 
            this.rbtnVoucher.AutoSize = true;
            this.rbtnVoucher.BackColor = System.Drawing.Color.White;
            this.rbtnVoucher.Location = new System.Drawing.Point(203, 11);
            this.rbtnVoucher.Name = "rbtnVoucher";
            this.rbtnVoucher.Size = new System.Drawing.Size(85, 21);
            this.rbtnVoucher.TabIndex = 8;
            this.rbtnVoucher.TabStop = true;
            this.rbtnVoucher.Text = "Voucher";
            this.rbtnVoucher.UseVisualStyleBackColor = true;
            // 
            // rbtnCredit
            // 
            this.rbtnCredit.AutoSize = true;
            this.rbtnCredit.BackColor = System.Drawing.Color.White;
            this.rbtnCredit.Location = new System.Drawing.Point(128, 11);
            this.rbtnCredit.Name = "rbtnCredit";
            this.rbtnCredit.Size = new System.Drawing.Size(69, 21);
            this.rbtnCredit.TabIndex = 7;
            this.rbtnCredit.TabStop = true;
            this.rbtnCredit.Text = "Credit";
            this.rbtnCredit.UseVisualStyleBackColor = true;
            // 
            // rbtnCash
            // 
            this.rbtnCash.AutoSize = true;
            this.rbtnCash.BackColor = System.Drawing.Color.White;
            this.rbtnCash.Location = new System.Drawing.Point(63, 11);
            this.rbtnCash.Name = "rbtnCash";
            this.rbtnCash.Size = new System.Drawing.Size(59, 21);
            this.rbtnCash.TabIndex = 6;
            this.rbtnCash.TabStop = true;
            this.rbtnCash.Text = "Cash";
            this.rbtnCash.UseVisualStyleBackColor = true;
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.BackColor = System.Drawing.Color.White;
            this.rbtnAll.Location = new System.Drawing.Point(11, 11);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(46, 21);
            this.rbtnAll.TabIndex = 5;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "All";
            this.rbtnAll.UseVisualStyleBackColor = true;
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(23, 19);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(48, 19);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "From";
            // 
            // btnGo
            // 
            this.btnGo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGo.BackgroundImage")));
            this.btnGo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.ForeColor = System.Drawing.Color.White;
            this.btnGo.Location = new System.Drawing.Point(254, 4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(63, 63);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(72, 43);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 25);
            this.toDate1.TabIndex = 1;
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(72, 15);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 25);
            this.fromDate1.TabIndex = 0;
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(39, 44);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(28, 19);
            this.mPlbl2.TabIndex = 2;
            this.mPlbl2.Text = "To";
            // 
            // DateVoucherTypeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMultiSelection1);
            this.Name = "DateVoucherTypeControl";
            this.Size = new System.Drawing.Size(345, 154);
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.gboxVoucherType.ResumeLayout(false);
            this.gboxVoucherType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private System.Windows.Forms.GroupBox gboxVoucherType;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnGo;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.ToDate toDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.FromDate fromDate1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton rbtnCash;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton rbtnAll;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton rbtnCreditStatement;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton rbtnVoucher;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton rbtnCredit;
    }
}
