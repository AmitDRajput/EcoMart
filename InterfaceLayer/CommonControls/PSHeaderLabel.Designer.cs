namespace EcoMart.InterfaceLayer.CommonControls
{
    partial class PSHeaderLabel
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
            this.lblHeaderCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHeaderCaption
            // 
            this.lblHeaderCaption.AutoSize = true;
            this.lblHeaderCaption.BackColor = System.Drawing.Color.Teal;
            this.lblHeaderCaption.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.Gold;
            this.lblHeaderCaption.Location = new System.Drawing.Point(4, 2);
            this.lblHeaderCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            this.lblHeaderCaption.Size = new System.Drawing.Size(0, 22);
            this.lblHeaderCaption.TabIndex = 15;
            // 
            // PSHeaderLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.Controls.Add(this.lblHeaderCaption);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PSHeaderLabel";
            this.Size = new System.Drawing.Size(735, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeaderCaption;
    }
}
