using System;
using System.Windows.Forms;
namespace EcoMart
{
    partial class Download_Updates
    {
        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        //Required by the Windows Form Designer

        private System.ComponentModel.IContainer components;
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.Btndownload = new System.Windows.Forms.Button();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.LblRelease = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Location = new System.Drawing.Point(27, 67);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(226, 23);
            this.ProgressBar1.TabIndex = 0;
            this.ProgressBar1.Visible = false;
            // 
            // Btndownload
            // 
            this.Btndownload.Location = new System.Drawing.Point(62, 27);
            this.Btndownload.Name = "Btndownload";
            this.Btndownload.Size = new System.Drawing.Size(146, 23);
            this.Btndownload.TabIndex = 1;
            this.Btndownload.Text = "Download";
            this.Btndownload.UseVisualStyleBackColor = true;
            this.Btndownload.Click += new System.EventHandler(this.Btndownload_Click);
            // 
            // LblRelease
            // 
            this.LblRelease.AutoSize = true;
            this.LblRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRelease.Location = new System.Drawing.Point(27, 97);
            this.LblRelease.Name = "LblRelease";
            this.LblRelease.Size = new System.Drawing.Size(0, 20);
            this.LblRelease.TabIndex = 2;
            this.LblRelease.Visible = false;
            // 
            // Download_Updates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.LblRelease);
            this.Controls.Add(this.Btndownload);
            this.Controls.Add(this.ProgressBar1);
            this.MaximizeBox = false;
            this.Name = "Download_Updates";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download Updates";
            this.Load += new System.EventHandler(this.Download_Updates_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal ProgressBar ProgressBar1;
        internal Button Btndownload;
        internal Timer Timer1;
        internal Label LblRelease;
    }
}