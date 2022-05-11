namespace EcoMart.InterfaceLayer
{
    partial class UclSubstitute
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
            this.uclSubstituteControl1 = new EcoMart.InterfaceLayer.Controls.UclSubstituteControl();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(941, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 486);
            this.MMBottomPanel.Size = new System.Drawing.Size(943, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.uclSubstituteControl1);
            this.MMMainPanel.Size = new System.Drawing.Size(943, 423);
            this.MMMainPanel.Controls.SetChildIndex(this.uclSubstituteControl1, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(396, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(545, 19);
            // 
            // uclSubstituteControl1
            // 
            this.uclSubstituteControl1.Location = new System.Drawing.Point(138, 24);
            this.uclSubstituteControl1.Name = "uclSubstituteControl1";
            this.uclSubstituteControl1.Size = new System.Drawing.Size(664, 394);
            this.uclSubstituteControl1.TabIndex = 1;
            // 
            // UclSubstitute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclSubstitute";
            this.Size = new System.Drawing.Size(943, 549);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EcoMart.InterfaceLayer.Controls.UclSubstituteControl uclSubstituteControl1;


    }
}
