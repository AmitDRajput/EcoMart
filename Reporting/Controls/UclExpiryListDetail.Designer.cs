namespace EcoMart.Reporting.Controls
{
    partial class UclExpiryListDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclExpiryListDetail));
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtReportTotalAmount = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.pnlMonthYear = new System.Windows.Forms.Panel();
            this.mcbShelfCode = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFromYear = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.txtFromMonth = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblFromMonth = new System.Windows.Forms.Label();
            this.lblFromYear = new System.Windows.Forms.Label();
            this.btnOKMultiSelection1 = new System.Windows.Forms.Button();
            this.txtToYear = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtToMonth = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblToMonth = new System.Windows.Forms.Label();
            this.lblToYear = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlMonthYear.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(957, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.psLabel1);
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmount);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 530);
            this.MMBottomPanel.Size = new System.Drawing.Size(959, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel1, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMonthYear);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Size = new System.Drawing.Size(959, 478);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMonthYear, 0);
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = false;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.FreezeLastRow = false;
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(957, 476);
            this.dgvReportList.TabIndex = 1030;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(363, 3);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(90, 16);
            this.psLabel1.TabIndex = 1052;
            this.psLabel1.Text = "Total Amount";
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BackColor = System.Drawing.Color.Linen;
            this.txtReportTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportTotalAmount.Location = new System.Drawing.Point(468, 0);
            this.txtReportTotalAmount.MaxLength = 15;
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.ReadOnly = true;
            this.txtReportTotalAmount.Size = new System.Drawing.Size(126, 20);
            this.txtReportTotalAmount.TabIndex = 1051;
            this.txtReportTotalAmount.TabStop = false;
            this.txtReportTotalAmount.Tag = "0.00";
            this.txtReportTotalAmount.Text = "0.00";
            this.txtReportTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlMonthYear
            // 
            this.pnlMonthYear.Controls.Add(this.mcbShelfCode);
            this.pnlMonthYear.Controls.Add(this.label2);
            this.pnlMonthYear.Controls.Add(this.txtFromYear);
            this.pnlMonthYear.Controls.Add(this.txtFromMonth);
            this.pnlMonthYear.Controls.Add(this.lblFromMonth);
            this.pnlMonthYear.Controls.Add(this.lblFromYear);
            this.pnlMonthYear.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMonthYear.Controls.Add(this.txtToYear);
            this.pnlMonthYear.Controls.Add(this.label1);
            this.pnlMonthYear.Controls.Add(this.txtToMonth);
            this.pnlMonthYear.Controls.Add(this.lblToMonth);
            this.pnlMonthYear.Controls.Add(this.lblToYear);
            this.pnlMonthYear.Location = new System.Drawing.Point(301, 126);
            this.pnlMonthYear.Name = "pnlMonthYear";
            this.pnlMonthYear.Size = new System.Drawing.Size(376, 150);
            this.pnlMonthYear.TabIndex = 1033;
            // 
            // mcbShelfCode
            // 
            this.mcbShelfCode.ColumnWidth = null;
            this.mcbShelfCode.DataSource = null;
            this.mcbShelfCode.DisplayColumnNo = 1;
            this.mcbShelfCode.DropDownHeight = 200;
            this.mcbShelfCode.Location = new System.Drawing.Point(141, 105);
            this.mcbShelfCode.Margin = new System.Windows.Forms.Padding(4);
            this.mcbShelfCode.Name = "mcbShelfCode";
            this.mcbShelfCode.SelectedID = "";
            this.mcbShelfCode.ShowNew = true;
            this.mcbShelfCode.Size = new System.Drawing.Size(175, 22);
            this.mcbShelfCode.SourceDataString = null;
            this.mcbShelfCode.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbShelfCode.TabIndex = 1042;
            this.mcbShelfCode.UserControlToShow = null;
            this.mcbShelfCode.ValueColumnNo = 0;
            this.mcbShelfCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcbShelfCode_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(66, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 1041;
            this.label2.Text = "Shelf Code";
            // 
            // txtFromYear
            // 
            this.txtFromYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromYear.Location = new System.Drawing.Point(96, 69);
            this.txtFromYear.Name = "txtFromYear";
            this.txtFromYear.Size = new System.Drawing.Size(59, 22);
            this.txtFromYear.TabIndex = 1040;
            this.txtFromYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFromYear_KeyDown);
            // 
            // txtFromMonth
            // 
            this.txtFromMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromMonth.Location = new System.Drawing.Point(96, 39);
            this.txtFromMonth.Name = "txtFromMonth";
            this.txtFromMonth.Size = new System.Drawing.Size(59, 22);
            this.txtFromMonth.TabIndex = 1039;
            this.txtFromMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFromMonth_KeyDown);
            // 
            // lblFromMonth
            // 
            this.lblFromMonth.AutoSize = true;
            this.lblFromMonth.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromMonth.Location = new System.Drawing.Point(8, 43);
            this.lblFromMonth.Name = "lblFromMonth";
            this.lblFromMonth.Size = new System.Drawing.Size(81, 15);
            this.lblFromMonth.TabIndex = 1037;
            this.lblFromMonth.Text = "From M&onth";
            // 
            // lblFromYear
            // 
            this.lblFromYear.AutoSize = true;
            this.lblFromYear.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromYear.Location = new System.Drawing.Point(20, 73);
            this.lblFromYear.Name = "lblFromYear";
            this.lblFromYear.Size = new System.Drawing.Size(69, 15);
            this.lblFromYear.TabIndex = 1038;
            this.lblFromYear.Text = "From Year";
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackColor = System.Drawing.Color.Lime;
            this.btnOKMultiSelection1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(312, 39);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(57, 52);
            this.btnOKMultiSelection1.TabIndex = 1036;
            this.btnOKMultiSelection1.Text = "GO";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = false;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // txtToYear
            // 
            this.txtToYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToYear.Location = new System.Drawing.Point(247, 69);
            this.txtToYear.Name = "txtToYear";
            this.txtToYear.Size = new System.Drawing.Size(59, 22);
            this.txtToYear.TabIndex = 147;
            this.txtToYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToYear_KeyDown);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 17);
            this.label1.TabIndex = 149;
            this.label1.Text = "Products Expired On or Before";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtToMonth
            // 
            this.txtToMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToMonth.Location = new System.Drawing.Point(247, 39);
            this.txtToMonth.Name = "txtToMonth";
            this.txtToMonth.Size = new System.Drawing.Size(59, 22);
            this.txtToMonth.TabIndex = 146;
            this.txtToMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToMonth_KeyDown);
            // 
            // lblToMonth
            // 
            this.lblToMonth.AutoSize = true;
            this.lblToMonth.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToMonth.Location = new System.Drawing.Point(175, 43);
            this.lblToMonth.Name = "lblToMonth";
            this.lblToMonth.Size = new System.Drawing.Size(63, 15);
            this.lblToMonth.TabIndex = 0;
            this.lblToMonth.Text = "To M&onth";
            // 
            // lblToYear
            // 
            this.lblToYear.AutoSize = true;
            this.lblToYear.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToYear.Location = new System.Drawing.Point(190, 73);
            this.lblToYear.Name = "lblToYear";
            this.lblToYear.Size = new System.Drawing.Size(48, 15);
            this.lblToYear.TabIndex = 1;
            this.lblToYear.Text = "ToYear";
            // 
            // UclExpiryListDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclExpiryListDetail";
            this.Size = new System.Drawing.Size(959, 553);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlMonthYear.ResumeLayout(false);
            this.pnlMonthYear.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.ToolTip ttToolTip;
        private InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtReportTotalAmount;
        private System.Windows.Forms.Panel pnlMonthYear;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtFromYear;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtFromMonth;
        private System.Windows.Forms.Label lblFromMonth;
        private System.Windows.Forms.Label lblFromYear;
        private System.Windows.Forms.Button btnOKMultiSelection1;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtToYear;
        private System.Windows.Forms.Label label1;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtToMonth;
        private System.Windows.Forms.Label lblToMonth;
        private System.Windows.Forms.Label lblToYear;
        private InterfaceLayer.CommonControls.PSComboBoxNew mcbShelfCode;
        private System.Windows.Forms.Label label2;
    }
}
