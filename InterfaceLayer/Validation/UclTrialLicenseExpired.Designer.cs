namespace EcoMart.InterfaceLayer.Validation
{
    partial class UclTrialLicenseExpired
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
            this.pnlTrialExpire = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlTrialExpire.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTrialExpire
            // 
            this.pnlTrialExpire.Controls.Add(this.label8);
            this.pnlTrialExpire.Controls.Add(this.label7);
            this.pnlTrialExpire.Location = new System.Drawing.Point(17, 16);
            this.pnlTrialExpire.Name = "pnlTrialExpire";
            this.pnlTrialExpire.Size = new System.Drawing.Size(286, 91);
            this.pnlTrialExpire.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(256, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Click \'Next\' to import Full License or \'Cancel\' to close.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Your Trial License is expired.";
            // 
            // UclTrialLicenseExpired
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTrialExpire);
            this.Name = "UclTrialLicenseExpired";
            this.Size = new System.Drawing.Size(312, 116);
            this.pnlTrialExpire.ResumeLayout(false);
            this.pnlTrialExpire.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTrialExpire;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}
