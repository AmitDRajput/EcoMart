namespace EcoMart.InterfaceLayer
{
    partial class FormSplash
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSplash));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pBar = new EcoMart.InterfaceLayer.CommonControls.PSContinuousProgressBar();
            this.lblProgressMsg = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlMain.BackgroundImage")));
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.lblProgressMsg);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.lblVersion);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(373, 183);
            this.pnlMain.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pBar);
            this.panel1.Location = new System.Drawing.Point(162, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 20);
            this.panel1.TabIndex = 6;
            // 
            // pBar
            // 
            this.pBar.BackColor = System.Drawing.Color.White;
            this.pBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBar.ForeColor = System.Drawing.Color.RoyalBlue;
            this.pBar.Location = new System.Drawing.Point(0, 0);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(183, 18);
            this.pBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pBar.TabIndex = 4;
            // 
            // lblProgressMsg
            // 
            this.lblProgressMsg.AutoSize = true;
            this.lblProgressMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblProgressMsg.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressMsg.Location = new System.Drawing.Point(3, 164);
            this.lblProgressMsg.Name = "lblProgressMsg";
            this.lblProgressMsg.Size = new System.Drawing.Size(34, 13);
            this.lblProgressMsg.TabIndex = 5;
            this.lblProgressMsg.Text = "MSG";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(94, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Loading, Please wait ...";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.Navy;
            this.lblVersion.Location = new System.Drawing.Point(239, 82);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 14);
            this.lblVersion.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(93, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "EcoMart";
            // 
            // FormSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(373, 183);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EcoMart";
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private EcoMart.InterfaceLayer.CommonControls.PSContinuousProgressBar pBar;
        private System.Windows.Forms.Label lblProgressMsg;
        private System.Windows.Forms.Panel panel1;
    }
}