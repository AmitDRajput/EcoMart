namespace EcoMart.InterfaceLayer
{
    partial class UclToolDistributorPercentageChange
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
            this.btnStart = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.txtDistRatePer = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.mPlbl17 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(929, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 423);
            this.MMBottomPanel.Size = new System.Drawing.Size(931, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.psLabel1);
            this.MMMainPanel.Controls.Add(this.mPlbl17);
            this.MMMainPanel.Controls.Add(this.txtDistRatePer);
            this.MMMainPanel.Controls.Add(this.txtpasswd);
            this.MMMainPanel.Controls.Add(this.btnStart);
            this.MMMainPanel.Size = new System.Drawing.Size(931, 360);
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.btnStart, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtpasswd, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtDistRatePer, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mPlbl17, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.psLabel1, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(463, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 20);
            // 
            // txtpasswd
            // 
            this.txtpasswd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpasswd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtpasswd.IsNumericDataSet = false;
            this.txtpasswd.Location = new System.Drawing.Point(437, 128);
            this.txtpasswd.Name = "txtpasswd";
            this.txtpasswd.PasswordChar = '*';
            this.txtpasswd.Size = new System.Drawing.Size(181, 22);
            this.txtpasswd.TabIndex = 8;
            this.txtpasswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpasswd_KeyDown);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(437, 175);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(83, 38);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            this.btnStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnStart_KeyDown);
            // 
            // txtDistRatePer
            // 
            this.txtDistRatePer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDistRatePer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDistRatePer.Location = new System.Drawing.Point(437, 85);
            this.txtDistRatePer.Name = "txtDistRatePer";
            this.txtDistRatePer.Size = new System.Drawing.Size(86, 22);
            this.txtDistRatePer.TabIndex = 54;
            this.txtDistRatePer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaxLevel_KeyDown);
            // 
            // mPlbl17
            // 
            this.mPlbl17.AutoSize = true;
            this.mPlbl17.Location = new System.Drawing.Point(198, 89);
            this.mPlbl17.Name = "mPlbl17";
            this.mPlbl17.Size = new System.Drawing.Size(193, 16);
            this.mPlbl17.TabIndex = 56;
            this.mPlbl17.Text = "&Distributor Rate Increase In %";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(341, 133);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(67, 16);
            this.psLabel1.TabIndex = 57;
            this.psLabel1.Text = "Password";
            // 
            // UclToolDistributorPercentageChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclToolDistributorPercentageChange";
            this.Size = new System.Drawing.Size(931, 486);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CommonControls.PSTextBox txtpasswd;
        private PharmaSYSPlus.CommonLibrary.PSButton btnStart;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtDistRatePer;
        private CommonControls.PSLabel mPlbl17;
        private CommonControls.PSLabel psLabel1;
        private System.Windows.Forms.Timer timer1;
    }
}
