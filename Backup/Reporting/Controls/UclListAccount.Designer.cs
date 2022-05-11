namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclListAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclListAccount));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.gbAccountType = new System.Windows.Forms.GroupBox();
            this.cbOtherDebtor = new System.Windows.Forms.CheckBox();
            this.cbOtherCreditor = new System.Windows.Forms.CheckBox();
            this.cbPurchase = new System.Windows.Forms.CheckBox();
            this.cbSale = new System.Windows.Forms.CheckBox();
            this.cbGeneral = new System.Windows.Forms.CheckBox();
            this.cbBank = new System.Windows.Forms.CheckBox();
            this.cbDebtor = new System.Windows.Forms.CheckBox();
            this.cbCreditor = new System.Windows.Forms.CheckBox();
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.gbAccountType.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(968, 23);
            // 
            // MMBottomPanel
            //            
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 559);
            this.MMBottomPanel.Size = new System.Drawing.Size(970, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel2);
            this.MMMainPanel.Size = new System.Drawing.Size(970, 507);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pnlMultiSelection1);
            this.panel2.Controls.Add(this.dgvReportList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(968, 505);
            this.panel2.TabIndex = 1050;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.gbAccountType);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(361, 50);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(248, 220);
            this.pnlMultiSelection1.TabIndex = 28;
            // 
            // gbAccountType
            // 
            this.gbAccountType.Controls.Add(this.cbOtherDebtor);
            this.gbAccountType.Controls.Add(this.cbOtherCreditor);
            this.gbAccountType.Controls.Add(this.cbPurchase);
            this.gbAccountType.Controls.Add(this.cbSale);
            this.gbAccountType.Controls.Add(this.cbGeneral);
            this.gbAccountType.Controls.Add(this.cbBank);
            this.gbAccountType.Controls.Add(this.cbDebtor);
            this.gbAccountType.Controls.Add(this.cbCreditor);
            this.gbAccountType.Location = new System.Drawing.Point(12, 3);
            this.gbAccountType.Name = "gbAccountType";
            this.gbAccountType.Size = new System.Drawing.Size(150, 209);
            this.gbAccountType.TabIndex = 5;
            this.gbAccountType.TabStop = false;
            // 
            // cbOtherDebtor
            // 
            this.cbOtherDebtor.AutoSize = true;
            this.cbOtherDebtor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOtherDebtor.Location = new System.Drawing.Point(25, 178);
            this.cbOtherDebtor.Name = "cbOtherDebtor";
            this.cbOtherDebtor.Size = new System.Drawing.Size(104, 19);
            this.cbOtherDebtor.TabIndex = 7;
            this.cbOtherDebtor.Text = "OtherDebtor";
            this.cbOtherDebtor.UseVisualStyleBackColor = true;
            // 
            // cbOtherCreditor
            // 
            this.cbOtherCreditor.AutoSize = true;
            this.cbOtherCreditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOtherCreditor.Location = new System.Drawing.Point(25, 155);
            this.cbOtherCreditor.Name = "cbOtherCreditor";
            this.cbOtherCreditor.Size = new System.Drawing.Size(112, 19);
            this.cbOtherCreditor.TabIndex = 6;
            this.cbOtherCreditor.Text = "OtherCreditor";
            this.cbOtherCreditor.UseVisualStyleBackColor = true;
            // 
            // cbPurchase
            // 
            this.cbPurchase.AutoSize = true;
            this.cbPurchase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPurchase.Location = new System.Drawing.Point(25, 132);
            this.cbPurchase.Name = "cbPurchase";
            this.cbPurchase.Size = new System.Drawing.Size(86, 19);
            this.cbPurchase.TabIndex = 5;
            this.cbPurchase.Text = "Purchase";
            this.cbPurchase.UseVisualStyleBackColor = true;
            // 
            // cbSale
            // 
            this.cbSale.AutoSize = true;
            this.cbSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSale.Location = new System.Drawing.Point(25, 109);
            this.cbSale.Name = "cbSale";
            this.cbSale.Size = new System.Drawing.Size(55, 19);
            this.cbSale.TabIndex = 4;
            this.cbSale.Text = "Sale";
            this.cbSale.UseVisualStyleBackColor = true;
            // 
            // cbGeneral
            // 
            this.cbGeneral.AutoSize = true;
            this.cbGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGeneral.Location = new System.Drawing.Point(25, 86);
            this.cbGeneral.Name = "cbGeneral";
            this.cbGeneral.Size = new System.Drawing.Size(77, 19);
            this.cbGeneral.TabIndex = 3;
            this.cbGeneral.Text = "General";
            this.cbGeneral.UseVisualStyleBackColor = true;
            // 
            // cbBank
            // 
            this.cbBank.AutoSize = true;
            this.cbBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBank.Location = new System.Drawing.Point(25, 63);
            this.cbBank.Name = "cbBank";
            this.cbBank.Size = new System.Drawing.Size(58, 19);
            this.cbBank.TabIndex = 2;
            this.cbBank.Text = "Bank";
            this.cbBank.UseVisualStyleBackColor = true;
            // 
            // cbDebtor
            // 
            this.cbDebtor.AutoSize = true;
            this.cbDebtor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDebtor.Location = new System.Drawing.Point(25, 40);
            this.cbDebtor.Name = "cbDebtor";
            this.cbDebtor.Size = new System.Drawing.Size(69, 19);
            this.cbDebtor.TabIndex = 1;
            this.cbDebtor.Text = "Debtor";
            this.cbDebtor.UseVisualStyleBackColor = true;
            // 
            // cbCreditor
            // 
            this.cbCreditor.AutoSize = true;
            this.cbCreditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCreditor.Location = new System.Drawing.Point(25, 17);
            this.cbCreditor.Name = "cbCreditor";
            this.cbCreditor.Size = new System.Drawing.Size(77, 19);
            this.cbCreditor.TabIndex = 0;
            this.cbCreditor.Text = "Creditor";
            this.cbCreditor.UseVisualStyleBackColor = true;
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(179, 3);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 4;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection1_Click);
            // 
            // dgvReportList
            // 
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(966, 503);
            this.dgvReportList.TabIndex = 27;            
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // UclListAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Name = "UclListAccount";
            this.Size = new System.Drawing.Size(970, 582);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlMultiSelection1.ResumeLayout(false);
            this.gbAccountType.ResumeLayout(false);
            this.gbAccountType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private System.Windows.Forms.GroupBox gbAccountType;
        private System.Windows.Forms.CheckBox cbCreditor;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private System.Windows.Forms.CheckBox cbOtherDebtor;
        private System.Windows.Forms.CheckBox cbOtherCreditor;
        private System.Windows.Forms.CheckBox cbPurchase;
        private System.Windows.Forms.CheckBox cbSale;
        private System.Windows.Forms.CheckBox cbGeneral;
        private System.Windows.Forms.CheckBox cbBank;
        private System.Windows.Forms.CheckBox cbDebtor;
        private System.Windows.Forms.ToolTip ttToolTip;
    }
}
