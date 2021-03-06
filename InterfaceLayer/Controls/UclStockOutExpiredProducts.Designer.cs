namespace EcoMart.InterfaceLayer
{
    partial class UclStockOutExpiredProducts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStockOutExpiredProducts));
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtVouType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlMonthYear = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtYear = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.datePickerBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtMonth = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblBillDate = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.mpMainSubViewControl = new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReportTotalAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlMonthYear.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(938, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.label6);
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmount);
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.label4);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 490);
            this.MMBottomPanel.Size = new System.Drawing.Size(940, 63);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.label4, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.label6, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMonthYear);
            this.MMMainPanel.Controls.Add(this.mpMainSubViewControl);
            this.MMMainPanel.Controls.Add(this.panel2);
            this.MMMainPanel.Size = new System.Drawing.Size(940, 438);
            this.MMMainPanel.Controls.SetChildIndex(this.panel2, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMainSubViewControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMonthYear, 0);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtVouType);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(938, 71);
            this.panel2.TabIndex = 108;
            // 
            // txtVouType
            // 
            this.txtVouType.BackColor = System.Drawing.Color.Snow;
            this.txtVouType.Enabled = false;
            this.txtVouType.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVouType.ForeColor = System.Drawing.Color.Navy;
            this.txtVouType.Location = new System.Drawing.Point(827, 3);
            this.txtVouType.Name = "txtVouType";
            this.txtVouType.ReadOnly = true;
            this.txtVouType.Size = new System.Drawing.Size(91, 27);
            this.txtVouType.TabIndex = 145;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(760, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 144;
            this.label3.Text = "Vou Type";
            // 
            // pnlMonthYear
            // 
            this.pnlMonthYear.Controls.Add(this.btnOK);
            this.pnlMonthYear.Controls.Add(this.txtYear);
            this.pnlMonthYear.Controls.Add(this.label5);
            this.pnlMonthYear.Controls.Add(this.datePickerBillDate);
            this.pnlMonthYear.Controls.Add(this.txtMonth);
            this.pnlMonthYear.Controls.Add(this.lblBillDate);
            this.pnlMonthYear.Controls.Add(this.lblMonth);
            this.pnlMonthYear.Controls.Add(this.lblYear);
            this.pnlMonthYear.Location = new System.Drawing.Point(342, 138);
            this.pnlMonthYear.Name = "pnlMonthYear";
            this.pnlMonthYear.Size = new System.Drawing.Size(286, 144);
            this.pnlMonthYear.TabIndex = 110;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Lime;
            this.btnOK.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(223, 27);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(45, 52);
            this.btnOK.TabIndex = 1036;
            this.btnOK.Text = "GO";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnOK.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnOK_KeyDown);
            // 
            // txtYear
            // 
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Location = new System.Drawing.Point(108, 64);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(95, 26);
            this.txtYear.TabIndex = 147;
            this.txtYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtYear_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(49, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(219, 17);
            this.label5.TabIndex = 149;
            this.label5.Text = "Products Expired On or Before";
            // 
            // datePickerBillDate
            // 
            this.datePickerBillDate.CustomFormat = "dd/MM/yy";
            this.datePickerBillDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerBillDate.Location = new System.Drawing.Point(108, 90);
            this.datePickerBillDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datePickerBillDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datePickerBillDate.Name = "datePickerBillDate";
            this.datePickerBillDate.Size = new System.Drawing.Size(122, 26);
            this.datePickerBillDate.TabIndex = 143;
            this.datePickerBillDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.datePickerBillDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datePickerBillDate_KeyDown);
            // 
            // txtMonth
            // 
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonth.Location = new System.Drawing.Point(108, 38);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(59, 26);
            this.txtMonth.TabIndex = 146;
            this.txtMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMonth_KeyDown);
            // 
            // lblBillDate
            // 
            this.lblBillDate.AutoSize = true;
            this.lblBillDate.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillDate.Location = new System.Drawing.Point(41, 97);
            this.lblBillDate.Name = "lblBillDate";
            this.lblBillDate.Size = new System.Drawing.Size(60, 15);
            this.lblBillDate.TabIndex = 142;
            this.lblBillDate.Text = "Vou Date";
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.Location = new System.Drawing.Point(56, 42);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(46, 15);
            this.lblMonth.TabIndex = 0;
            this.lblMonth.Text = "M&onth";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.Location = new System.Drawing.Point(67, 68);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(34, 15);
            this.lblYear.TabIndex = 1;
            this.lblYear.Text = "&Year";
            // 
            // mpMainSubViewControl
            // 
            this.mpMainSubViewControl.AutoScroll = true;
            this.mpMainSubViewControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mpMainSubViewControl.DataSource = null;
            this.mpMainSubViewControl.DataSourceMain = null;
            this.mpMainSubViewControl.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl.DateColumnNames")));
            this.mpMainSubViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMainSubViewControl.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl.DoubleColumnNames")));
            this.mpMainSubViewControl.Filter = null;
            this.mpMainSubViewControl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMainSubViewControl.IsAllowDelete = false;
            this.mpMainSubViewControl.IsAllowNewRow = false;
            this.mpMainSubViewControl.Location = new System.Drawing.Point(0, 71);
            this.mpMainSubViewControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.mpMainSubViewControl.MinimumSize = new System.Drawing.Size(693, 348);
            this.mpMainSubViewControl.Name = "mpMainSubViewControl";
            this.mpMainSubViewControl.NextRowColumn = 0;
            this.mpMainSubViewControl.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl.NumericColumnNames")));
            this.mpMainSubViewControl.Size = new System.Drawing.Size(938, 365);
            this.mpMainSubViewControl.SubGridWidth = 450;
            this.mpMainSubViewControl.TabIndex = 109;
            this.mpMainSubViewControl.ViewControl = null;
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.Bisque;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(588, -2);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 26);
            this.txtNoOfRows.TabIndex = 29;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(496, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 15);
            this.label4.TabIndex = 28;
            this.label4.Text = "No Of Rows";
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BackColor = System.Drawing.Color.Linen;
            this.txtReportTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportTotalAmount.Location = new System.Drawing.Point(754, 0);
            this.txtReportTotalAmount.MaxLength = 15;
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.ReadOnly = true;
            this.txtReportTotalAmount.Size = new System.Drawing.Size(147, 22);
            this.txtReportTotalAmount.TabIndex = 1012;
            this.txtReportTotalAmount.TabStop = false;
            this.txtReportTotalAmount.Tag = "0.00";
            this.txtReportTotalAmount.Text = "0.00";
            this.txtReportTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(682, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 1013;
            this.label6.Text = "Amount";
            // 
            // UclStockOutExpiredProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclStockOutExpiredProducts";
            this.Size = new System.Drawing.Size(940, 513);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlMonthYear.ResumeLayout(false);
            this.pnlMonthYear.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtVouType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlMonthYear;
        private System.Windows.Forms.Button btnOK;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtYear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker datePickerBillDate;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtMonth;
        private System.Windows.Forms.Label lblBillDate;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblYear;
        private CommonControls.PSMainSubViewControl mpMainSubViewControl;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtReportTotalAmount;
    }
}
