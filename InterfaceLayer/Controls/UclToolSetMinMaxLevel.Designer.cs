namespace EcoMart.InterfaceLayer
{
    partial class UclToolSetMinMaxLevel
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
            this.txtpasswd = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.btnStart = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.txtMinLevel = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.txtMaxLevel = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.mPlbl6 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl17 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(929, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 423);
            this.MMBottomPanel.Size = new System.Drawing.Size(931, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.mPlbl17);
            this.MMMainPanel.Controls.Add(this.mPlbl6);
            this.MMMainPanel.Controls.Add(this.txtMaxLevel);
            this.MMMainPanel.Controls.Add(this.txtMinLevel);
            this.MMMainPanel.Controls.Add(this.txtpasswd);
            this.MMMainPanel.Controls.Add(this.btnStart);
            this.MMMainPanel.Size = new System.Drawing.Size(931, 371);
            this.MMMainPanel.Controls.SetChildIndex(this.btnStart, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtpasswd, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtMinLevel, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtMaxLevel, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mPlbl6, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mPlbl17, 0);
            // 
            // txtpasswd
            // 
            this.txtpasswd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpasswd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtpasswd.Location = new System.Drawing.Point(374, 141);
            this.txtpasswd.Name = "txtpasswd";
            this.txtpasswd.PasswordChar = '*';
            this.txtpasswd.Size = new System.Drawing.Size(181, 22);
            this.txtpasswd.TabIndex = 8;
            this.txtpasswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpasswd_KeyDown);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(419, 183);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(93, 45);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtMinLevel
            // 
            this.txtMinLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMinLevel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMinLevel.Location = new System.Drawing.Point(397, 43);
            this.txtMinLevel.Name = "txtMinLevel";
            this.txtMinLevel.Size = new System.Drawing.Size(86, 22);
            this.txtMinLevel.TabIndex = 53;
            this.txtMinLevel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMinLevel_KeyDown);
            // 
            // txtMaxLevel
            // 
            this.txtMaxLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaxLevel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaxLevel.Location = new System.Drawing.Point(397, 82);
            this.txtMaxLevel.Name = "txtMaxLevel";
            this.txtMaxLevel.Size = new System.Drawing.Size(86, 22);
            this.txtMaxLevel.TabIndex = 54;
            this.txtMaxLevel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaxLevel_KeyDown);
            // 
            // mPlbl6
            // 
            this.mPlbl6.AutoSize = true;
            this.mPlbl6.Location = new System.Drawing.Point(288, 45);
            this.mPlbl6.Name = "mPlbl6";
            this.mPlbl6.Size = new System.Drawing.Size(103, 16);
            this.mPlbl6.TabIndex = 55;
            this.mPlbl6.Text = "M&inimum Level";
            // 
            // mPlbl17
            // 
            this.mPlbl17.AutoSize = true;
            this.mPlbl17.Location = new System.Drawing.Point(286, 84);
            this.mPlbl17.Name = "mPlbl17";
            this.mPlbl17.Size = new System.Drawing.Size(105, 16);
            this.mPlbl17.TabIndex = 56;
            this.mPlbl17.Text = "Ma&ximum Level";
            // 
            // UclToolSetMinMaxLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclToolSetMinMaxLevel";
            this.Size = new System.Drawing.Size(931, 486);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CommonControls.PSTextBox txtpasswd;
        private PharmaSYSPlus.CommonLibrary.PSButton btnStart;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtMaxLevel;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtMinLevel;
        private CommonControls.PSLabel mPlbl6;
        private CommonControls.PSLabel mPlbl17;
    }
}
