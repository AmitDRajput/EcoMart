namespace EcoMart.Reporting.Controls
{
    partial class UclAcListBankBookByClearedDate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclAcListBankBookByClearedDate));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewtext = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.mcbBank = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mPlbl3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.mcbvoutype = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lblViewFrom = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(960, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Size = new System.Drawing.Size(962, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(962, 435);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlGo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // pnlGo
            // 
            this.pnlGo.BackColor = System.Drawing.Color.Plum;
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.lblViewFrom);
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.psLabel2);
            this.pnlGo.Controls.Add(this.txtViewtext);
            this.pnlGo.Controls.Add(this.label1);
            this.pnlGo.Controls.Add(this.txtType);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(960, 33);
            this.pnlGo.TabIndex = 1046;
            this.pnlGo.Controls.SetChildIndex(this.txtType, 0);
            this.pnlGo.Controls.SetChildIndex(this.label1, 0);
            this.pnlGo.Controls.SetChildIndex(this.txtViewtext, 0);
            this.pnlGo.Controls.SetChildIndex(this.psLabel2, 0);
            this.pnlGo.Controls.SetChildIndex(this.ViewFromDate, 0);
            this.pnlGo.Controls.SetChildIndex(this.ViewToDate, 0);
            this.pnlGo.Controls.SetChildIndex(this.lblViewFrom, 0);
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(858, 3);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1074;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(709, 3);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1073;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(824, 3);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(21, 14);
            this.psLabel2.TabIndex = 1072;
            this.psLabel2.Text = "To";
            // 
            // txtViewtext
            // 
            this.txtViewtext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewtext.Location = new System.Drawing.Point(83, 3);
            this.txtViewtext.Name = "txtViewtext";
            this.txtViewtext.Size = new System.Drawing.Size(157, 22);
            this.txtViewtext.TabIndex = 1064;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1063;
            this.label1.Text = "Type";
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.Location = new System.Drawing.Point(297, 3);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(57, 22);
            this.txtType.TabIndex = 1062;
            this.txtType.Visible = false;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(646, 3);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(48, 19);
            this.psLabel1.TabIndex = 1071;
            this.psLabel1.Text = "From";
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = false;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.Khaki;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(960, 400);
            this.dgvReportList.TabIndex = 1048;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.mcbBank);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl3);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.mcbvoutype);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl4);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(334, 167);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(484, 151);
            this.pnlMultiSelection1.TabIndex = 1049;
            // 
            // mcbBank
            // 
            this.mcbBank.BackColor = System.Drawing.Color.Transparent;
            this.mcbBank.ColumnWidth = null;
            this.mcbBank.DataSource = null;
            this.mcbBank.DisplayColumnNo = 1;
            this.mcbBank.DropDownHeight = 200;
            this.mcbBank.Location = new System.Drawing.Point(77, 78);
            this.mcbBank.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbBank.Name = "mcbBank";
            this.mcbBank.SelectedID = null;
            this.mcbBank.ShowNew = false;
            this.mcbBank.Size = new System.Drawing.Size(398, 22);
            this.mcbBank.SourceDataString = null;
            this.mcbBank.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbBank.TabIndex = 2;
            this.mcbBank.UserControlToShow = null;
            this.mcbBank.ValueColumnNo = 0;
            this.mcbBank.EnterKeyPressed += new System.EventHandler(this.mcbBank_EnterKeyPressed);
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(19, 81);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(35, 14);
            this.mPlbl3.TabIndex = 4;
            this.mPlbl3.Text = "Bank";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(24, 117);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(34, 14);
            this.mPlbl1.TabIndex = 1069;
            this.mPlbl1.Text = "Type";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(20, 22);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(38, 14);
            this.mPlbl2.TabIndex = 0;
            this.mPlbl2.Text = "From";
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(416, 1);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 4;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection1_Click);
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(80, 46);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 24);
            this.toDate1.TabIndex = 1;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // mcbvoutype
            // 
            this.mcbvoutype.BackColor = System.Drawing.Color.Transparent;
            this.mcbvoutype.ColumnWidth = null;
            this.mcbvoutype.DataSource = null;
            this.mcbvoutype.DisplayColumnNo = 1;
            this.mcbvoutype.DropDownHeight = 200;
            this.mcbvoutype.Location = new System.Drawing.Point(77, 113);
            this.mcbvoutype.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbvoutype.Name = "mcbvoutype";
            this.mcbvoutype.SelectedID = null;
            this.mcbvoutype.ShowNew = false;
            this.mcbvoutype.Size = new System.Drawing.Size(127, 22);
            this.mcbvoutype.SourceDataString = null;
            this.mcbvoutype.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbvoutype.TabIndex = 3;
            this.mcbvoutype.UserControlToShow = null;
            this.mcbvoutype.ValueColumnNo = 0;
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(36, 51);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(21, 14);
            this.mPlbl4.TabIndex = 2;
            this.mPlbl4.Text = "To";
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(80, 18);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 0;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // lblViewFrom
            // 
            this.lblViewFrom.AutoSize = true;
            this.lblViewFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewFrom.Location = new System.Drawing.Point(664, 6);
            this.lblViewFrom.Name = "lblViewFrom";
            this.lblViewFrom.Size = new System.Drawing.Size(39, 15);
            this.lblViewFrom.TabIndex = 1075;
            this.lblViewFrom.Text = "From";
            // 
            // UclAcListBankBookByClearedDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclAcListBankBookByClearedDate";
            this.Size = new System.Drawing.Size(962, 510);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGo;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private EcoMart.InterfaceLayer.CommonControls.PSTextBox txtViewtext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtType;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbBank;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.ToDate toDate1;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbvoutype;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private EcoMart.InterfaceLayer.CommonControls.FromDate fromDate1;
        private System.Windows.Forms.ToolTip ttToolTip;
        private System.Windows.Forms.Label lblViewFrom;
    }
}
