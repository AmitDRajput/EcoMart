namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclSaleListPartyProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclSaleListPartyProduct));
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlGo = new System.Windows.Forms.Panel();
           
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(978, 23);
            // 
            // MMBottomPanel
            // 
        
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 542);
            this.MMBottomPanel.Size = new System.Drawing.Size(980, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(980, 490);
            // 
            // ToDate
            // 
            this.ToDate.CustomFormat = "dd/MM/yy";
            this.ToDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(802, 17);
            this.ToDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ToDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(122, 26);
            this.ToDate.TabIndex = 1043;
            this.ToDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // FromDate
            // 
            this.FromDate.CustomFormat = "dd/MM/yy";
            this.FromDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(641, 17);
            this.FromDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.FromDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(122, 26);
            this.FromDate.TabIndex = 1042;
            this.FromDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(775, 22);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(22, 15);
            this.lblTo.TabIndex = 1041;
            this.lblTo.Text = "To";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(594, 22);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(39, 15);
            this.lblFrom.TabIndex = 1040;
            this.lblFrom.Text = "From";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Lime;
            this.btnOK.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(931, 15);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(35, 30);
            this.btnOK.TabIndex = 1035;
            this.btnOK.Text = "GO";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dgvReportList
            // 
            this.dgvReportList.BackColor = System.Drawing.Color.Khaki;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.Location = new System.Drawing.Point(0, 54);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(978, 434);
            this.dgvReportList.TabIndex = 1031;
            // 
            // pnlGo
            // 
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.ToDate);
            this.pnlGo.Controls.Add(this.FromDate);
            this.pnlGo.Controls.Add(this.lblTo);
            this.pnlGo.Controls.Add(this.lblFrom);
            this.pnlGo.Controls.Add(this.btnOK);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(978, 54);
            this.pnlGo.TabIndex = 22;
            
            // 
            // UclSaleListPartyProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclSaleListPartyProduct";
            this.Size = new System.Drawing.Size(980, 565);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lblTo;        
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Button btnOK;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.Panel pnlGo;
          
    }
}
