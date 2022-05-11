namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclYearEnd
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
            this.txtpasswd = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSTextBox();
            this.psLableWithBorderMiddleLeft1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psButton1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.txtpasswd);
            this.MMMainPanel.Controls.Add(this.psLableWithBorderMiddleLeft1);
            this.MMMainPanel.Controls.Add(this.psButton1);
            this.MMMainPanel.Controls.SetChildIndex(this.psButton1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.psLableWithBorderMiddleLeft1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtpasswd, 0);
            // 
            // txtpasswd
            // 
            this.txtpasswd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpasswd.Location = new System.Drawing.Point(284, 178);
            this.txtpasswd.Name = "txtpasswd";
            this.txtpasswd.PasswordChar = '*';
            this.txtpasswd.Size = new System.Drawing.Size(181, 23);
            this.txtpasswd.TabIndex = 3;
            // 
            // psLableWithBorderMiddleLeft1
            // 
            this.psLableWithBorderMiddleLeft1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.psLableWithBorderMiddleLeft1.Location = new System.Drawing.Point(234, 113);
            this.psLableWithBorderMiddleLeft1.Name = "psLableWithBorderMiddleLeft1";
            this.psLableWithBorderMiddleLeft1.Size = new System.Drawing.Size(262, 23);
            this.psLableWithBorderMiddleLeft1.TabIndex = 5;
            this.psLableWithBorderMiddleLeft1.Text = "This is a YearEnd Procedure\r\n";
            this.psLableWithBorderMiddleLeft1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psButton1
            // 
            this.psButton1.Location = new System.Drawing.Point(328, 211);
            this.psButton1.Name = "psButton1";
            this.psButton1.Size = new System.Drawing.Size(93, 45);
            this.psButton1.TabIndex = 4;
            this.psButton1.Text = "Start";
            this.psButton1.UseVisualStyleBackColor = true;
            this.psButton1.Click += new System.EventHandler(this.psButton1_Click);
            // 
            // UclYearEnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclYearEnd";
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSTextBox txtpasswd;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft psLableWithBorderMiddleLeft1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton psButton1;
    }
}
