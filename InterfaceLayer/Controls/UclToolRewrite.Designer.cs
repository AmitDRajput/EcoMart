namespace EcoMart.InterfaceLayer
{
    partial class UclToolRewrite
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnCheckFixAccounts = new System.Windows.Forms.RadioButton();
            this.rbtnRefillPurchase = new System.Windows.Forms.RadioButton();
            this.rbtnFillTransactionDateForSale = new System.Windows.Forms.RadioButton();
            this.txtpasswd = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.psButton1 = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(666, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 457);
            this.MMBottomPanel.Size = new System.Drawing.Size(668, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.txtpasswd);
            this.MMMainPanel.Controls.Add(this.psButton1);
            this.MMMainPanel.Controls.Add(this.groupBox1);
            this.MMMainPanel.Size = new System.Drawing.Size(668, 405);
            this.MMMainPanel.Controls.SetChildIndex(this.groupBox1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.psButton1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtpasswd, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnCheckFixAccounts);
            this.groupBox1.Controls.Add(this.rbtnRefillPurchase);
            this.groupBox1.Controls.Add(this.rbtnFillTransactionDateForSale);
            this.groupBox1.Location = new System.Drawing.Point(142, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 149);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rbtnCheckFixAccounts
            // 
            this.rbtnCheckFixAccounts.AutoSize = true;
            this.rbtnCheckFixAccounts.Location = new System.Drawing.Point(39, 87);
            this.rbtnCheckFixAccounts.Name = "rbtnCheckFixAccounts";
            this.rbtnCheckFixAccounts.Size = new System.Drawing.Size(131, 17);
            this.rbtnCheckFixAccounts.TabIndex = 2;
            this.rbtnCheckFixAccounts.TabStop = true;
            this.rbtnCheckFixAccounts.Text = "Check All FixAccounts";
            this.rbtnCheckFixAccounts.UseVisualStyleBackColor = true;
            // 
            // rbtnRefillPurchase
            // 
            this.rbtnRefillPurchase.AutoSize = true;
            this.rbtnRefillPurchase.Location = new System.Drawing.Point(39, 62);
            this.rbtnRefillPurchase.Name = "rbtnRefillPurchase";
            this.rbtnRefillPurchase.Size = new System.Drawing.Size(185, 17);
            this.rbtnRefillPurchase.TabIndex = 1;
            this.rbtnRefillPurchase.TabStop = true;
            this.rbtnRefillPurchase.Text = "Refill Purchase Entries In tblTrnac";
            this.rbtnRefillPurchase.UseVisualStyleBackColor = true;
            // 
            // rbtnFillTransactionDateForSale
            // 
            this.rbtnFillTransactionDateForSale.AutoSize = true;
            this.rbtnFillTransactionDateForSale.Location = new System.Drawing.Point(39, 37);
            this.rbtnFillTransactionDateForSale.Name = "rbtnFillTransactionDateForSale";
            this.rbtnFillTransactionDateForSale.Size = new System.Drawing.Size(218, 17);
            this.rbtnFillTransactionDateForSale.TabIndex = 0;
            this.rbtnFillTransactionDateForSale.TabStop = true;
            this.rbtnFillTransactionDateForSale.Text = "Fill Transaction Date For Sale In tblTrnac";
            this.rbtnFillTransactionDateForSale.UseVisualStyleBackColor = true;
            // 
            // txtpasswd
            // 
            this.txtpasswd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpasswd.Location = new System.Drawing.Point(235, 234);
            this.txtpasswd.Name = "txtpasswd";
            this.txtpasswd.PasswordChar = '*';
            this.txtpasswd.Size = new System.Drawing.Size(181, 22);
            this.txtpasswd.TabIndex = 2;
            this.txtpasswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpasswd_KeyDown);
            // 
            // psButton1
            // 
            this.psButton1.Location = new System.Drawing.Point(279, 267);
            this.psButton1.Name = "psButton1";
            this.psButton1.Size = new System.Drawing.Size(93, 45);
            this.psButton1.TabIndex = 3;
            this.psButton1.Text = "Start";
            this.psButton1.UseVisualStyleBackColor = true;
            this.psButton1.Click += new System.EventHandler(this.psButton1_Click);
            this.psButton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.psButton1_KeyDown);
            // 
            // UclToolRewrite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclToolRewrite";
            this.Size = new System.Drawing.Size(668, 503);
            this.Leave += new System.EventHandler(this.UclToolRewrite_Leave);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnCheckFixAccounts;
        private System.Windows.Forms.RadioButton rbtnRefillPurchase;
        private System.Windows.Forms.RadioButton rbtnFillTransactionDateForSale;
        private CommonControls.PSTextBox txtpasswd;
        private PharmaSYSPlus.CommonLibrary.PSButton psButton1;
    }
}
