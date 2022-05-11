namespace PharmaSYSDistributorPlus.Reporting.Controls
{
    partial class UclVATListCreditNote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclVATListCreditNote));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.ViewToDate = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewtext = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.psLabel1 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMultiSelection1 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.fromDate1 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.FromDate(this.components);
            this.toDate1 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.ToDate(this.components);
            this.btnOKMultiSelection1 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.mPlbl2 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(958, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 535);
            this.MMBottomPanel.Size = new System.Drawing.Size(960, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(960, 483);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlGo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // pnlGo
            // 
            this.pnlGo.BackColor = System.Drawing.Color.DarkKhaki;
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.psLabel2);
            this.pnlGo.Controls.Add(this.txtViewtext);
            this.pnlGo.Controls.Add(this.label1);
            this.pnlGo.Controls.Add(this.txtType);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(958, 33);
            this.pnlGo.TabIndex = 49;
            this.pnlGo.Controls.SetChildIndex(this.txtType, 0);
            this.pnlGo.Controls.SetChildIndex(this.label1, 0);
            this.pnlGo.Controls.SetChildIndex(this.txtViewtext, 0);
            this.pnlGo.Controls.SetChildIndex(this.psLabel2, 0);
            this.pnlGo.Controls.SetChildIndex(this.ViewFromDate, 0);
            this.pnlGo.Controls.SetChildIndex(this.ViewToDate, 0);
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(853, 4);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1082;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(704, 4);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1081;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(819, 4);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(21, 14);
            this.psLabel2.TabIndex = 1080;
            this.psLabel2.Text = "To";
            // 
            // txtViewtext
            // 
            this.txtViewtext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewtext.Location = new System.Drawing.Point(90, 4);
            this.txtViewtext.Name = "txtViewtext";
            this.txtViewtext.Size = new System.Drawing.Size(157, 22);
            this.txtViewtext.TabIndex = 1067;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1066;
            this.label1.Text = "Type";
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.Location = new System.Drawing.Point(304, 4);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(57, 22);
            this.txtType.TabIndex = 1065;
            this.txtType.Visible = false;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(641, 4);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(48, 19);
            this.psLabel1.TabIndex = 1079;
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
            this.dgvReportList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(958, 448);
            this.dgvReportList.TabIndex = 1032;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(327, 153);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(341, 79);
            this.pnlMultiSelection1.TabIndex = 1037;
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(93, 10);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 1;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(93, 43);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 24);
            this.toDate1.TabIndex = 3;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(275, 5);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(59, 61);
            this.btnOKMultiSelection1.TabIndex = 4;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection1_Click);
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(40, 43);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(21, 14);
            this.mPlbl2.TabIndex = 2;
            this.mPlbl2.Text = "To";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(20, 15);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(38, 14);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "From";
            // 
            // UclVATListCreditNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclVATListCreditNote";
            this.Size = new System.Drawing.Size(960, 558);
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
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSTextBox txtViewtext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtType;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        //private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private System.Windows.Forms.ToolTip ttToolTip;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.FromDate fromDate1;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.ToDate toDate1;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSDistributorPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
    }
}
