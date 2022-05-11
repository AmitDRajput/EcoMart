namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    partial class PSMessageBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSMessageBoxForm));
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.PnlMessage = new System.Windows.Forms.Panel();
            this.lblMessageLine2 = new System.Windows.Forms.Label();
            this.lblMessageLine1 = new System.Windows.Forms.Label();
            this.PnlButton = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.PnlMessage.SuspendLayout();
            this.PnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // picIcon
            // 
            this.picIcon.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picIcon.ErrorImage")));
            this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
            this.picIcon.InitialImage = ((System.Drawing.Image)(resources.GetObject("picIcon.InitialImage")));
            this.picIcon.Location = new System.Drawing.Point(12, 12);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(36, 36);
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            // 
            // PnlMessage
            // 
            this.PnlMessage.AutoScroll = true;
            this.PnlMessage.Controls.Add(this.lblMessageLine2);
            this.PnlMessage.Controls.Add(this.lblMessageLine1);
            this.PnlMessage.Location = new System.Drawing.Point(49, 12);
            this.PnlMessage.Name = "PnlMessage";
            this.PnlMessage.Size = new System.Drawing.Size(278, 50);
            this.PnlMessage.TabIndex = 1;
            // 
            // lblMessageLine2
            // 
            this.lblMessageLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessageLine2.AutoSize = true;
            this.lblMessageLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageLine2.Location = new System.Drawing.Point(4, 25);
            this.lblMessageLine2.Name = "lblMessageLine2";
            this.lblMessageLine2.Size = new System.Drawing.Size(60, 24);
            this.lblMessageLine2.TabIndex = 1;
            this.lblMessageLine2.Text = "label2";
            this.lblMessageLine2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMessageLine1
            // 
            this.lblMessageLine1.AutoSize = true;
            this.lblMessageLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageLine1.Location = new System.Drawing.Point(4, 4);
            this.lblMessageLine1.Name = "lblMessageLine1";
            this.lblMessageLine1.Size = new System.Drawing.Size(46, 17);
            this.lblMessageLine1.TabIndex = 0;
            this.lblMessageLine1.Text = "label1";
            // 
            // PnlButton
            // 
            this.PnlButton.Controls.Add(this.btnPrint);
            this.PnlButton.Controls.Add(this.btnOk);
            this.PnlButton.Location = new System.Drawing.Point(49, 64);
            this.PnlButton.Name = "PnlButton";
            this.PnlButton.Size = new System.Drawing.Size(278, 40);
            this.PnlButton.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(68, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 29);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(149, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // PSMessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 107);
            this.Controls.Add(this.PnlButton);
            this.Controls.Add(this.PnlMessage);
            this.Controls.Add(this.picIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PSMessageBoxForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PharmaSYS Retail Plus";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PSMessageBoxForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.PnlMessage.ResumeLayout(false);
            this.PnlMessage.PerformLayout();
            this.PnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Panel PnlMessage;
        private System.Windows.Forms.Panel PnlButton;
        private System.Windows.Forms.Label lblMessageLine1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblMessageLine2;
    }
}