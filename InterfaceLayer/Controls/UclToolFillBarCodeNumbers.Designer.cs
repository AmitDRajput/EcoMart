namespace EcoMart.InterfaceLayer
{
    partial class UclToolFillBarCodeNumbers
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
            this.components = new System.ComponentModel.Container();
            this.txtpasswd = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.psLableWithBorderMiddleLeft1 = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psButton1 = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(969, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 464);
            this.MMBottomPanel.Size = new System.Drawing.Size(971, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.txtpasswd);
            this.MMMainPanel.Controls.Add(this.psLableWithBorderMiddleLeft1);
            this.MMMainPanel.Controls.Add(this.psButton1);
            this.MMMainPanel.Size = new System.Drawing.Size(971, 412);
            this.MMMainPanel.Controls.SetChildIndex(this.psButton1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.psLableWithBorderMiddleLeft1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtpasswd, 0);
            // 
            // txtpasswd
            // 
            this.txtpasswd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpasswd.Location = new System.Drawing.Point(397, 168);
            this.txtpasswd.Name = "txtpasswd";
            this.txtpasswd.PasswordChar = '*';
            this.txtpasswd.Size = new System.Drawing.Size(181, 22);
            this.txtpasswd.TabIndex = 3;
            this.txtpasswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpasswd_KeyDown);
            // 
            // psLableWithBorderMiddleLeft1
            // 
            this.psLableWithBorderMiddleLeft1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.psLableWithBorderMiddleLeft1.Location = new System.Drawing.Point(233, 92);
            this.psLableWithBorderMiddleLeft1.Name = "psLableWithBorderMiddleLeft1";
            this.psLableWithBorderMiddleLeft1.Size = new System.Drawing.Size(523, 49);
            this.psLableWithBorderMiddleLeft1.TabIndex = 5;
            this.psLableWithBorderMiddleLeft1.Text = "This Procedure is to Give Product Numbers and Generate ScanCode For Each Batch\r\n";
            this.psLableWithBorderMiddleLeft1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psButton1
            // 
            this.psButton1.Location = new System.Drawing.Point(441, 201);
            this.psButton1.Name = "psButton1";
            this.psButton1.Size = new System.Drawing.Size(93, 45);
            this.psButton1.TabIndex = 4;
            this.psButton1.Text = "Start";
            this.psButton1.UseVisualStyleBackColor = true;
            this.psButton1.Click += new System.EventHandler(this.psButton1_Click);
            this.psButton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.psButton1_KeyDown);
            // 
            // UclToolFillBarCodeNumbers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclToolFillBarCodeNumbers";
            this.Size = new System.Drawing.Size(971, 487);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CommonControls.PSTextBox txtpasswd;
        private CommonControls.PSLableWithBorderMiddleLeft psLableWithBorderMiddleLeft1;
        private PharmaSYSPlus.CommonLibrary.PSButton psButton1;
    }
}
