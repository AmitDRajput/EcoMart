namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    partial class GBType
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
            this.gbType1 = new System.Windows.Forms.GroupBox();
            this.gbTrnType = new System.Windows.Forms.GroupBox();
            this.rbtnCreditStatement = new System.Windows.Forms.RadioButton();
            this.rbtnCredit = new System.Windows.Forms.RadioButton();
            this.rbtnCash = new System.Windows.Forms.RadioButton();
            this.rbtnAll = new System.Windows.Forms.RadioButton();
            this.gbTrnType.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbType1
            // 
            this.gbType1.Location = new System.Drawing.Point(0, 0);
            this.gbType1.Name = "gbType1";
            this.gbType1.Size = new System.Drawing.Size(200, 100);
            this.gbType1.TabIndex = 0;
            this.gbType1.TabStop = false;
            this.gbType1.Text = "groupBox1";
            // 
            // gbTrnType
            // 
            this.gbTrnType.BackColor = System.Drawing.Color.Transparent;
            this.gbTrnType.Controls.Add(this.rbtnCreditStatement);
            this.gbTrnType.Controls.Add(this.rbtnCredit);
            this.gbTrnType.Controls.Add(this.rbtnCash);
            this.gbTrnType.Location = new System.Drawing.Point(3, 3);
            this.gbTrnType.Name = "gbTrnType";
            this.gbTrnType.Size = new System.Drawing.Size(287, 38);
            this.gbTrnType.TabIndex = 0;
            this.gbTrnType.TabStop = false;
            // 
            // rbtnCreditStatement
            // 
            this.rbtnCreditStatement.AutoSize = true;
            this.rbtnCreditStatement.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnCreditStatement.Location = new System.Drawing.Point(128, 15);
            this.rbtnCreditStatement.Name = "rbtnCreditStatement";
            this.rbtnCreditStatement.Size = new System.Drawing.Size(113, 18);
            this.rbtnCreditStatement.TabIndex = 2;
            this.rbtnCreditStatement.TabStop = true;
            this.rbtnCreditStatement.Text = "CreditStatement";
            this.rbtnCreditStatement.UseVisualStyleBackColor = true;
            // 
            // rbtnCredit
            // 
            this.rbtnCredit.AutoSize = true;
            this.rbtnCredit.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnCredit.Location = new System.Drawing.Point(63, 15);
            this.rbtnCredit.Name = "rbtnCredit";
            this.rbtnCredit.Size = new System.Drawing.Size(59, 18);
            this.rbtnCredit.TabIndex = 1;
            this.rbtnCredit.TabStop = true;
            this.rbtnCredit.Text = "Credit";
            this.rbtnCredit.UseVisualStyleBackColor = true;
            // 
            // rbtnCash
            // 
            this.rbtnCash.AutoSize = true;
            this.rbtnCash.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnCash.Location = new System.Drawing.Point(6, 15);
            this.rbtnCash.Name = "rbtnCash";
            this.rbtnCash.Size = new System.Drawing.Size(51, 18);
            this.rbtnCash.TabIndex = 0;
            this.rbtnCash.TabStop = true;
            this.rbtnCash.Text = "Cash";
            this.rbtnCash.UseVisualStyleBackColor = true;
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnAll.Location = new System.Drawing.Point(247, 18);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(41, 18);
            this.rbtnAll.TabIndex = 3;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "All";
            this.rbtnAll.UseVisualStyleBackColor = true;
            // 
            // GBType
            // 
            this.Controls.Add(this.rbtnAll);
            this.Controls.Add(this.gbTrnType);
            this.Name = "GBType";
            this.Size = new System.Drawing.Size(295, 46);
            this.gbTrnType.ResumeLayout(false);
            this.gbTrnType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbType1;
        private System.Windows.Forms.GroupBox gbTrnType;
        private System.Windows.Forms.RadioButton rbtnCreditStatement;
        private System.Windows.Forms.RadioButton rbtnCredit;
        private System.Windows.Forms.RadioButton rbtnCash;
        private System.Windows.Forms.RadioButton rbtnAll;
    }
}
