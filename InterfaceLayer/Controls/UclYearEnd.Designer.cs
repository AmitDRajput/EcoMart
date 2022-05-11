namespace EcoMart.InterfaceLayer
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
            this.txtpasswd = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.psLableWithBorderMiddleLeft1 = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.btnStart = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.pnlYearEndProgress = new System.Windows.Forms.Panel();
            this.lblYearEndMsgLine2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblYearEndMsgLine1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlYearEndProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlYearEndProgress);
            this.MMMainPanel.Controls.Add(this.txtpasswd);
            this.MMMainPanel.Controls.Add(this.psLableWithBorderMiddleLeft1);
            this.MMMainPanel.Controls.Add(this.btnStart);
            this.MMMainPanel.Controls.SetChildIndex(this.btnStart, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.psLableWithBorderMiddleLeft1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtpasswd, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlYearEndProgress, 0);
            // 
            // txtpasswd
            // 
            this.txtpasswd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpasswd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtpasswd.Location = new System.Drawing.Point(301, 107);
            this.txtpasswd.Name = "txtpasswd";
            this.txtpasswd.PasswordChar = '*';
            this.txtpasswd.Size = new System.Drawing.Size(181, 22);
            this.txtpasswd.TabIndex = 3;
            this.txtpasswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpasswd_KeyDown);
            // 
            // psLableWithBorderMiddleLeft1
            // 
            this.psLableWithBorderMiddleLeft1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.psLableWithBorderMiddleLeft1.Location = new System.Drawing.Point(260, 67);
            this.psLableWithBorderMiddleLeft1.Name = "psLableWithBorderMiddleLeft1";
            this.psLableWithBorderMiddleLeft1.Size = new System.Drawing.Size(262, 23);
            this.psLableWithBorderMiddleLeft1.TabIndex = 5;
            this.psLableWithBorderMiddleLeft1.Text = "This is a YearEnd Procedure\r\n";
            this.psLableWithBorderMiddleLeft1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(346, 149);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(93, 45);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            this.btnStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnStart_KeyDown);
            // 
            // pnlYearEndProgress
            // 
            this.pnlYearEndProgress.BackColor = System.Drawing.Color.Coral;
            this.pnlYearEndProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlYearEndProgress.Controls.Add(this.lblYearEndMsgLine1);
            this.pnlYearEndProgress.Controls.Add(this.lblYearEndMsgLine2);
            this.pnlYearEndProgress.Location = new System.Drawing.Point(232, 212);
            this.pnlYearEndProgress.Name = "pnlYearEndProgress";
            this.pnlYearEndProgress.Size = new System.Drawing.Size(307, 88);
            this.pnlYearEndProgress.TabIndex = 6;
            this.pnlYearEndProgress.Visible = false;
            // 
            // lblYearEndMsgLine2
            // 
            this.lblYearEndMsgLine2.AutoSize = true;
            this.lblYearEndMsgLine2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblYearEndMsgLine2.Location = new System.Drawing.Point(18, 47);
            this.lblYearEndMsgLine2.Name = "lblYearEndMsgLine2";
            this.lblYearEndMsgLine2.Size = new System.Drawing.Size(50, 13);
            this.lblYearEndMsgLine2.TabIndex = 0;
            this.lblYearEndMsgLine2.Text = "Message";
            // 
            // lblYearEndMsgLine1
            // 
            this.lblYearEndMsgLine1.AutoSize = true;
            this.lblYearEndMsgLine1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblYearEndMsgLine1.Location = new System.Drawing.Point(15, 24);
            this.lblYearEndMsgLine1.Name = "lblYearEndMsgLine1";
            this.lblYearEndMsgLine1.Size = new System.Drawing.Size(215, 14);
            this.lblYearEndMsgLine1.TabIndex = 1;
            this.lblYearEndMsgLine1.Text = "Year End is in Progress. Please wait ...";
            // 
            // UclYearEnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclYearEnd";
            this.Load += new System.EventHandler(this.UclYearEnd_Load);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlYearEndProgress.ResumeLayout(false);
            this.pnlYearEndProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private EcoMart.InterfaceLayer.CommonControls.PSTextBox txtpasswd;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft psLableWithBorderMiddleLeft1;
        private PharmaSYSPlus.CommonLibrary.PSButton btnStart;
        private System.Windows.Forms.Panel pnlYearEndProgress;
        private CommonControls.PSLabel lblYearEndMsgLine2;
        private CommonControls.PSLabel lblYearEndMsgLine1;
    }
}
