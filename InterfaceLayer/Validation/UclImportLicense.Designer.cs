namespace EcoMart.InterfaceLayer.Validation
{
    partial class UclImportLicense
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
            this.pnlImport = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnImportLicense = new System.Windows.Forms.Button();
            this.pnlImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImport
            // 
            this.pnlImport.Controls.Add(this.btnImportLicense);
            this.pnlImport.Controls.Add(this.label2);
            this.pnlImport.Controls.Add(this.textBox1);
            this.pnlImport.Controls.Add(this.label1);
            this.pnlImport.Controls.Add(this.btnSelectFile);
            this.pnlImport.Location = new System.Drawing.Point(5, 6);
            this.pnlImport.Name = "pnlImport";
            this.pnlImport.Size = new System.Drawing.Size(351, 147);
            this.pnlImport.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please select the license and click on \'Import\' to import the license ...";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(72, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(244, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "License File";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(322, 49);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(25, 23);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnImportLicense
            // 
            this.btnImportLicense.Location = new System.Drawing.Point(272, 78);
            this.btnImportLicense.Name = "btnImportLicense";
            this.btnImportLicense.Size = new System.Drawing.Size(75, 23);
            this.btnImportLicense.TabIndex = 6;
            this.btnImportLicense.Text = "Import";
            this.btnImportLicense.UseVisualStyleBackColor = true;
            this.btnImportLicense.Click += new System.EventHandler(this.btnImportLicense_Click);
            // 
            // UclImportLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlImport);
            this.Name = "UclImportLicense";
            this.Size = new System.Drawing.Size(361, 160);
            this.pnlImport.ResumeLayout(false);
            this.pnlImport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlImport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnImportLicense;
    }
}
