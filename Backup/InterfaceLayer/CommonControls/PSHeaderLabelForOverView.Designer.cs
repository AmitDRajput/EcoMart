namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    partial class PSHeaderLabelForOverView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSHeaderLabelForOverView));
            this.lblHeaderCaptionForOverView = new System.Windows.Forms.Label();
            this.btnclosed = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHeaderCaptionForOverView
            // 
            this.lblHeaderCaptionForOverView.AutoSize = true;
            this.lblHeaderCaptionForOverView.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaptionForOverView.Location = new System.Drawing.Point(4, 3);
            this.lblHeaderCaptionForOverView.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeaderCaptionForOverView.Name = "lblHeaderCaptionForOverView";
            this.lblHeaderCaptionForOverView.Size = new System.Drawing.Size(0, 18);
            this.lblHeaderCaptionForOverView.TabIndex = 0;
            // 
            // btnclosed
            // 
            this.btnclosed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclosed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnclosed.BackgroundImage")));
            this.btnclosed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnclosed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclosed.Location = new System.Drawing.Point(721, -1);
            this.btnclosed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnclosed.Name = "btnclosed";
            this.btnclosed.Size = new System.Drawing.Size(31, 25);
            this.btnclosed.TabIndex = 48;
            this.btnclosed.UseVisualStyleBackColor = true;
            this.btnclosed.Click += new System.EventHandler(this.btnclosed_Click);
            // 
            // PSHeaderLabelForOverView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumVioletRed;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnclosed);
            this.Controls.Add(this.lblHeaderCaptionForOverView);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PSHeaderLabelForOverView";
            this.Size = new System.Drawing.Size(751, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeaderCaptionForOverView;
        private System.Windows.Forms.Button btnclosed;
    }
}
