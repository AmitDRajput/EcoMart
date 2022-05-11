namespace PharmaSYSRetailPlus.InterfaceLayer.Validation
{
    partial class UclTrialLicense
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
            this.pnlTrialLicense = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTrialDays = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlTrialLicense.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTrialLicense
            // 
            this.pnlTrialLicense.Controls.Add(this.label6);
            this.pnlTrialLicense.Controls.Add(this.lblTrialDays);
            this.pnlTrialLicense.Controls.Add(this.label5);
            this.pnlTrialLicense.Location = new System.Drawing.Point(5, 14);
            this.pnlTrialLicense.Name = "pnlTrialLicense";
            this.pnlTrialLicense.Size = new System.Drawing.Size(366, 128);
            this.pnlTrialLicense.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(355, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Click \'Next\' to import full license or \'Continue\' to run in evaluation period ..." +
                ".";
            // 
            // lblTrialDays
            // 
            this.lblTrialDays.AutoSize = true;
            this.lblTrialDays.Location = new System.Drawing.Point(7, 36);
            this.lblTrialDays.Name = "lblTrialDays";
            this.lblTrialDays.Size = new System.Drawing.Size(92, 13);
            this.lblTrialDays.TabIndex = 1;
            this.lblTrialDays.Text = "Day\'s Remaining: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "You are running Trial License.";
            // 
            // UclTrialLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTrialLicense);
            this.Name = "UclTrialLicense";
            this.Size = new System.Drawing.Size(376, 152);
            this.pnlTrialLicense.ResumeLayout(false);
            this.pnlTrialLicense.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTrialLicense;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTrialDays;
        private System.Windows.Forms.Label label5;
    }
}
