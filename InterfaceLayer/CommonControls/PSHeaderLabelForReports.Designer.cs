namespace EcoMart.InterfaceLayer.CommonControls
{
    partial class PSHeaderLabelForReports
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
            this.lblHeaderCaptionForOverView = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHeaderCaptionForOverView
            // 
            this.lblHeaderCaptionForOverView.AutoSize = true;
            this.lblHeaderCaptionForOverView.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaptionForOverView.ForeColor = System.Drawing.Color.DarkViolet;
            this.lblHeaderCaptionForOverView.Location = new System.Drawing.Point(4, 2);
            this.lblHeaderCaptionForOverView.Margin = new System.Windows.Forms.Padding(0);
            this.lblHeaderCaptionForOverView.Name = "lblHeaderCaptionForOverView";
            this.lblHeaderCaptionForOverView.Size = new System.Drawing.Size(0, 18);
            this.lblHeaderCaptionForOverView.TabIndex = 0;
            // 
            // PSHeaderLabelForReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblHeaderCaptionForOverView);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.BlueViolet;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PSHeaderLabelForReports";
            this.Size = new System.Drawing.Size(751, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeaderCaptionForOverView;
    }
}
