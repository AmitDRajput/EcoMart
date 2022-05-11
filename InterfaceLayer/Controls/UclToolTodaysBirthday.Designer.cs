namespace EcoMart.InterfaceLayer
{
    partial class UclToolTodaysBirthday
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBirthdayDetails = new System.Windows.Forms.Panel();
            this.dgvTodaysbirthday = new System.Windows.Forms.DataGridView();
            this.Col_PatientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_PatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_MobileNumberForSMS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlSMSDetails = new System.Windows.Forms.Panel();
            this.btnSendSMS = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlBirthdayDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodaysbirthday)).BeginInit();
            this.PnlSMSDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(903, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 382);
            this.MMBottomPanel.Size = new System.Drawing.Size(905, 65);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlBirthdayDetails);
            this.MMMainPanel.Size = new System.Drawing.Size(905, 319);
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlBirthdayDetails, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(437, 0);
            // 
            // pnlBirthdayDetails
            // 
            this.pnlBirthdayDetails.Controls.Add(this.dgvTodaysbirthday);
            this.pnlBirthdayDetails.Controls.Add(this.PnlSMSDetails);
            this.pnlBirthdayDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlBirthdayDetails.Name = "pnlBirthdayDetails";
            this.pnlBirthdayDetails.Size = new System.Drawing.Size(866, 317);
            this.pnlBirthdayDetails.TabIndex = 1109;
            // 
            // dgvTodaysbirthday
            // 
            this.dgvTodaysbirthday.AllowUserToAddRows = false;
            this.dgvTodaysbirthday.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTodaysbirthday.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTodaysbirthday.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTodaysbirthday.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_PatientID,
            this.Col_PatientName,
            this.Col_MobileNumberForSMS});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTodaysbirthday.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTodaysbirthday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTodaysbirthday.Location = new System.Drawing.Point(0, 41);
            this.dgvTodaysbirthday.Name = "dgvTodaysbirthday";
            this.dgvTodaysbirthday.ReadOnly = true;
            this.dgvTodaysbirthday.RowHeadersWidth = 15;
            this.dgvTodaysbirthday.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTodaysbirthday.Size = new System.Drawing.Size(866, 276);
            this.dgvTodaysbirthday.TabIndex = 0;
            // 
            // Col_PatientID
            // 
            this.Col_PatientID.HeaderText = "PatientID";
            this.Col_PatientID.Name = "Col_PatientID";
            this.Col_PatientID.ReadOnly = true;
            this.Col_PatientID.Visible = false;
            // 
            // Col_PatientName
            // 
            this.Col_PatientName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col_PatientName.HeaderText = "Patient Name";
            this.Col_PatientName.Name = "Col_PatientName";
            this.Col_PatientName.ReadOnly = true;
            // 
            // Col_MobileNumberForSMS
            // 
            this.Col_MobileNumberForSMS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Col_MobileNumberForSMS.HeaderText = "Mobile Number";
            this.Col_MobileNumberForSMS.Name = "Col_MobileNumberForSMS";
            this.Col_MobileNumberForSMS.ReadOnly = true;
            this.Col_MobileNumberForSMS.Width = 119;
            // 
            // PnlSMSDetails
            // 
            this.PnlSMSDetails.Controls.Add(this.btnSendSMS);
            this.PnlSMSDetails.Controls.Add(this.txtMsg);
            this.PnlSMSDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlSMSDetails.Location = new System.Drawing.Point(0, 0);
            this.PnlSMSDetails.Name = "PnlSMSDetails";
            this.PnlSMSDetails.Size = new System.Drawing.Size(866, 41);
            this.PnlSMSDetails.TabIndex = 1;
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.Location = new System.Drawing.Point(773, 4);
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Size = new System.Drawing.Size(75, 33);
            this.btnSendSMS.TabIndex = 1;
            this.btnSendSMS.Text = "&Send SMS";
            this.btnSendSMS.UseVisualStyleBackColor = true;
            this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMsg.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsg.Location = new System.Drawing.Point(303, 4);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(462, 33);
            this.txtMsg.TabIndex = 0;
            // 
            // UclToolTodaysBirthday
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclToolTodaysBirthday";
            this.Size = new System.Drawing.Size(905, 447);
            this.Load += new System.EventHandler(this.UclToolTodaysBirthday_Load);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlBirthdayDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodaysbirthday)).EndInit();
            this.PnlSMSDetails.ResumeLayout(false);
            this.PnlSMSDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBirthdayDetails;
        private System.Windows.Forms.Panel PnlSMSDetails;
        private System.Windows.Forms.Button btnSendSMS;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.DataGridView dgvTodaysbirthday;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_PatientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_PatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_MobileNumberForSMS;
    }
}
