namespace PharmaSYSRetailPlus.InterfaceLayer.Validation
{
    partial class UclNoLicense
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
            this.pnlNoLicense = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlNoLicense.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNoLicense
            // 
            this.pnlNoLicense.Controls.Add(this.label4);
            this.pnlNoLicense.Controls.Add(this.label3);
            this.pnlNoLicense.Location = new System.Drawing.Point(6, 24);
            this.pnlNoLicense.Name = "pnlNoLicense";
            this.pnlNoLicense.Size = new System.Drawing.Size(315, 55);
            this.pnlNoLicense.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "You don\'t have valid license to run this Application.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(302, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Click on \'Next\' to import license or \'Cancel\' to close the Wizard.";
            // 
            // UclNoLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlNoLicense);
            this.Name = "UclNoLicense";
            this.Size = new System.Drawing.Size(330, 107);
            this.pnlNoLicense.ResumeLayout(false);
            this.pnlNoLicense.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlNoLicense;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}
