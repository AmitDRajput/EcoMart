namespace EcoMart.InterfaceLayer.CommonControls
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTenderAmount = new System.Windows.Forms.Panel();
            this.txtTenderAmt = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtReceivedAmt = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.txtTotalAmt = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBillAmount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.PnlMessage.SuspendLayout();
            this.PnlButton.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlTenderAmount.SuspendLayout();
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
            this.PnlButton.Location = new System.Drawing.Point(3, 109);
            this.PnlButton.Name = "PnlButton";
            this.PnlButton.Size = new System.Drawing.Size(275, 36);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlTenderAmount, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.PnlButton, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(49, 68);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(299, 200);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pnlTenderAmount
            // 
            this.pnlTenderAmount.AutoScroll = true;
            this.pnlTenderAmount.Controls.Add(this.txtTenderAmt);
            this.pnlTenderAmount.Controls.Add(this.txtReceivedAmt);
            this.pnlTenderAmount.Controls.Add(this.txtTotalAmt);
            this.pnlTenderAmount.Controls.Add(this.label2);
            this.pnlTenderAmount.Controls.Add(this.label1);
            this.pnlTenderAmount.Controls.Add(this.lblBillAmount);
            this.pnlTenderAmount.Location = new System.Drawing.Point(3, 3);
            this.pnlTenderAmount.Name = "pnlTenderAmount";
            this.pnlTenderAmount.Size = new System.Drawing.Size(278, 100);
            this.pnlTenderAmount.TabIndex = 2;
            this.pnlTenderAmount.Visible = false;
            // 
            // txtTenderAmt
            // 
            this.txtTenderAmt.BackColor = System.Drawing.Color.Snow;
            this.txtTenderAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenderAmt.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenderAmt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTenderAmt.Location = new System.Drawing.Point(131, 70);
            this.txtTenderAmt.MaxLength = 15;
            this.txtTenderAmt.Name = "txtTenderAmt";
            this.txtTenderAmt.ReadOnly = true;
            this.txtTenderAmt.Size = new System.Drawing.Size(128, 26);
            this.txtTenderAmt.TabIndex = 7;
            this.txtTenderAmt.TabStop = false;
            this.txtTenderAmt.Tag = "0.00";
            this.txtTenderAmt.Text = "0.00";
            this.txtTenderAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtReceivedAmt
            // 
            this.txtReceivedAmt.BackColor = System.Drawing.Color.Snow;
            this.txtReceivedAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReceivedAmt.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceivedAmt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtReceivedAmt.Location = new System.Drawing.Point(131, 39);
            this.txtReceivedAmt.MaxLength = 15;
            this.txtReceivedAmt.Name = "txtReceivedAmt";
            this.txtReceivedAmt.Size = new System.Drawing.Size(128, 26);
            this.txtReceivedAmt.TabIndex = 6;
            this.txtReceivedAmt.TabStop = false;
            this.txtReceivedAmt.Tag = "0.00";
            this.txtReceivedAmt.Text = "0.00";
            this.txtReceivedAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtReceivedAmt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmountRcvd_KeyDown);
            this.txtReceivedAmt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAmountRcvd_KeyUp);
            // 
            // txtTotalAmt
            // 
            this.txtTotalAmt.BackColor = System.Drawing.Color.Snow;
            this.txtTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalAmt.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTotalAmt.Location = new System.Drawing.Point(131, 8);
            this.txtTotalAmt.MaxLength = 15;
            this.txtTotalAmt.Name = "txtTotalAmt";
            this.txtTotalAmt.ReadOnly = true;
            this.txtTotalAmt.Size = new System.Drawing.Size(128, 26);
            this.txtTotalAmt.TabIndex = 5;
            this.txtTotalAmt.TabStop = false;
            this.txtTotalAmt.Tag = "0.00";
            this.txtTotalAmt.Text = "0.00";
            this.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tender Amount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Received Amount";
            // 
            // lblBillAmount
            // 
            this.lblBillAmount.AutoSize = true;
            this.lblBillAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillAmount.Location = new System.Drawing.Point(3, 12);
            this.lblBillAmount.Name = "lblBillAmount";
            this.lblBillAmount.Size = new System.Drawing.Size(82, 17);
            this.lblBillAmount.TabIndex = 0;
            this.lblBillAmount.Text = "Net Amount";
            // 
            // PSMessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 113);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.PnlMessage);
            this.Controls.Add(this.picIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PSMessageBoxForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EcoMart";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PSMessageBoxForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.PnlMessage.ResumeLayout(false);
            this.PnlMessage.PerformLayout();
            this.PnlButton.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlTenderAmount.ResumeLayout(false);
            this.pnlTenderAmount.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlTenderAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBillAmount;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTenderAmt;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtReceivedAmt;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtTotalAmt;
    }
}