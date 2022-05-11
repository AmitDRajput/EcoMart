namespace EcoMart.Reporting.Base
{
    partial class DateVoucherTypeControlOneDateSale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateVoucherTypeControlOneDateSale));
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.gboxVoucherType = new System.Windows.Forms.GroupBox();
            this.rbtnCreditStatement = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnVoucher = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnCredit = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnCash = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtnAll = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnGo = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
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
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(1, 0);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(342, 154);
            this.pnlMultiSelection1.TabIndex = 0;
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
            this.rbtnCreditStatement.TabIndex = 4;
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
            this.rbtnVoucher.TabIndex = 3;
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
            this.rbtnCredit.TabIndex = 2;
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
            this.rbtnCash.TabIndex = 1;
            this.rbtnCash.Text = "Cash";
            this.rbtnCash.UseVisualStyleBackColor = true;
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.BackColor = System.Drawing.Color.White;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new System.Drawing.Point(11, 11);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(46, 21);
            this.rbtnAll.TabIndex = 0;
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
            this.btnGo.Image = ((System.Drawing.Image)(resources.GetObject("btnGo.Image")));
            this.btnGo.Location = new System.Drawing.Point(254, 4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(63, 63);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(72, 15);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 25);
            this.fromDate1.TabIndex = 1;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // DateVoucherTypeControlOneDateSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMultiSelection1);
            this.Name = "DateVoucherTypeControlOneDateSale";
            this.Size = new System.Drawing.Size(345, 154);
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.gboxVoucherType.ResumeLayout(false);
            this.gboxVoucherType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private System.Windows.Forms.GroupBox gboxVoucherType;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnGo;
        private EcoMart.InterfaceLayer.CommonControls.FromDate fromDate1;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtnCash;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtnAll;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtnCreditStatement;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtnVoucher;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtnCredit;
    }
}
