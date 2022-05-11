namespace EcoMart.InterfaceLayer
{
    partial class UclStockReProcess
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
            this.psButton1 = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.psLableWithBorderMiddleLeft1 = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.txtpasswd = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
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
            // psButton1
            // 
            this.psButton1.Location = new System.Drawing.Point(383, 238);
            this.psButton1.Name = "psButton1";
            this.psButton1.Size = new System.Drawing.Size(93, 45);
            this.psButton1.TabIndex = 1;
            this.psButton1.Text = "Start";
            this.psButton1.UseVisualStyleBackColor = true;
            this.psButton1.Click += new System.EventHandler(this.psButton1_Click);
            this.psButton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.psButton1_KeyDown);
            // 
            // psLableWithBorderMiddleLeft1
            // 
            this.psLableWithBorderMiddleLeft1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.psLableWithBorderMiddleLeft1.Location = new System.Drawing.Point(175, 129);
            this.psLableWithBorderMiddleLeft1.Name = "psLableWithBorderMiddleLeft1";
            this.psLableWithBorderMiddleLeft1.Size = new System.Drawing.Size(508, 49);
            this.psLableWithBorderMiddleLeft1.TabIndex = 2;
            this.psLableWithBorderMiddleLeft1.Text = "This Procedure is to Update stock in Master as per Batch";
            this.psLableWithBorderMiddleLeft1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtpasswd
            // 
            this.txtpasswd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpasswd.Location = new System.Drawing.Point(339, 205);
            this.txtpasswd.Name = "txtpasswd";
            this.txtpasswd.PasswordChar = '*';
            this.txtpasswd.Size = new System.Drawing.Size(181, 23);
            this.txtpasswd.TabIndex = 0;
            this.txtpasswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpasswd_KeyDown);
            // 
            // UclStockReProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclStockReProcess";
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSPlus.CommonLibrary.PSButton psButton1;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft psLableWithBorderMiddleLeft1;
        private EcoMart.InterfaceLayer.CommonControls.PSTextBox txtpasswd;
    }
}
