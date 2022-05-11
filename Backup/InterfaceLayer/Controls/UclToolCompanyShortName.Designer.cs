namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclToolCompanyShortName
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
            this.psLableWithBorderMiddleLeft1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psButton1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.psButton1);
            this.MMMainPanel.Controls.Add(this.psLableWithBorderMiddleLeft1);
            this.MMMainPanel.Controls.SetChildIndex(this.psLableWithBorderMiddleLeft1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.psButton1, 0);
            // 
            // psLableWithBorderMiddleLeft1
            // 
            this.psLableWithBorderMiddleLeft1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.psLableWithBorderMiddleLeft1.Location = new System.Drawing.Point(106, 141);
            this.psLableWithBorderMiddleLeft1.Name = "psLableWithBorderMiddleLeft1";
            this.psLableWithBorderMiddleLeft1.Size = new System.Drawing.Size(618, 46);
            this.psLableWithBorderMiddleLeft1.TabIndex = 3;
            this.psLableWithBorderMiddleLeft1.Text = "This Procedure is to Update Company shortName in ProductMaster\r\n";
            this.psLableWithBorderMiddleLeft1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psButton1
            // 
            this.psButton1.Location = new System.Drawing.Point(339, 221);
            this.psButton1.Name = "psButton1";
            this.psButton1.Size = new System.Drawing.Size(93, 45);
            this.psButton1.TabIndex = 4;
            this.psButton1.Text = "Start";
            this.psButton1.UseVisualStyleBackColor = true;
            this.psButton1.Click += new System.EventHandler(this.psButton1_Click);
            this.psButton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.psButton1_KeyDown);
            // 
            // UclToolCompanyShortName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclToolCompanyShortName";
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft psLableWithBorderMiddleLeft1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSButton psButton1;
    }
}
