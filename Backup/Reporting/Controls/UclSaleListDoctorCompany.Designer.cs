namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclSaleListDoctorCompany
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclSaleListDoctorCompany));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.txtViewText = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.ViewToDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.lblToDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.ViewFromDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.lblFromDate = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMultiSelection = new System.Windows.Forms.Panel();
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.psLabel6 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.mcbDoctor = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.txtReportTotalAmount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(997, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmount);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 568);
            this.MMBottomPanel.Size = new System.Drawing.Size(999, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(999, 516);
            // 
            // pnlGo
            // 
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.txtViewText);
            this.pnlGo.Controls.Add(this.psLabel3);
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.lblToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.lblFromDate);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(997, 31);
            this.pnlGo.TabIndex = 24;
            // 
            // txtViewText
            // 
            this.txtViewText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewText.Location = new System.Drawing.Point(96, 4);
            this.txtViewText.Name = "txtViewText";
            this.txtViewText.Size = new System.Drawing.Size(384, 23);
            this.txtViewText.TabIndex = 1075;
            this.txtViewText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(28, 6);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(59, 19);
            this.psLabel3.TabIndex = 1074;
            this.psLabel3.Text = "Doctor";
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.CausesValidation = false;
            this.ViewToDate.Location = new System.Drawing.Point(853, 3);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1072;
            this.ViewToDate.Text = "label";
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(813, 5);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(28, 19);
            this.lblToDate.TabIndex = 1071;
            this.lblToDate.Text = "To";
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.CausesValidation = false;
            this.ViewFromDate.Location = new System.Drawing.Point(707, 3);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1070;
            this.ViewFromDate.Text = "label";
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(651, 5);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(48, 19);
            this.lblFromDate.TabIndex = 1069;
            this.lblFromDate.Text = "From";
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
            this.dgvReportList.Location = new System.Drawing.Point(0, 31);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(997, 483);
            this.dgvReportList.TabIndex = 1031;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // pnlMultiSelection
            // 
            this.pnlMultiSelection.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.pnlMultiSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection.Controls.Add(this.psLabel6);
            this.pnlMultiSelection.Controls.Add(this.psLabel5);
            this.pnlMultiSelection.Controls.Add(this.psLabel4);
            this.pnlMultiSelection.Controls.Add(this.ToDate);
            this.pnlMultiSelection.Controls.Add(this.mcbDoctor);
            this.pnlMultiSelection.Controls.Add(this.FromDate);
            this.pnlMultiSelection.Location = new System.Drawing.Point(235, 140);
            this.pnlMultiSelection.Name = "pnlMultiSelection";
            this.pnlMultiSelection.Size = new System.Drawing.Size(590, 96);
            this.pnlMultiSelection.TabIndex = 1044;
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(514, 12);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 1074;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            // 
            // psLabel6
            // 
            this.psLabel6.AutoSize = true;
            this.psLabel6.Location = new System.Drawing.Point(61, 54);
            this.psLabel6.Name = "psLabel6";
            this.psLabel6.Size = new System.Drawing.Size(59, 19);
            this.psLabel6.TabIndex = 1073;
            this.psLabel6.Text = "Doctor";
            // 
            // psLabel5
            // 
            this.psLabel5.AutoSize = true;
            this.psLabel5.Location = new System.Drawing.Point(272, 17);
            this.psLabel5.Name = "psLabel5";
            this.psLabel5.Size = new System.Drawing.Size(28, 19);
            this.psLabel5.TabIndex = 1069;
            this.psLabel5.Text = "To";
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(72, 16);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(48, 19);
            this.psLabel4.TabIndex = 1068;
            this.psLabel4.Text = "From";
            // 
            // ToDate
            // 
            this.ToDate.CustomFormat = "";
            this.ToDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(306, 15);
            this.ToDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.ToDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(122, 26);
            this.ToDate.TabIndex = 1043;
            this.ToDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.ToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ToDate_KeyDown);
            // 
            // mcbDoctor
            // 
            this.mcbDoctor.ColumnWidth = null;
            this.mcbDoctor.DataSource = null;
            this.mcbDoctor.DisplayColumnNo = 1;
            this.mcbDoctor.DropDownHeight = 200;
            this.mcbDoctor.Location = new System.Drawing.Point(127, 52);
            this.mcbDoctor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbDoctor.Name = "mcbDoctor";
            this.mcbDoctor.SelectedID = null;
            this.mcbDoctor.ShowNew = false;
            this.mcbDoctor.Size = new System.Drawing.Size(333, 26);
            this.mcbDoctor.SourceDataString = null;
            this.mcbDoctor.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbDoctor.TabIndex = 1044;
            this.mcbDoctor.UserControlToShow = null;
            this.mcbDoctor.ValueColumnNo = 0;
            this.mcbDoctor.EnterKeyPressed += new System.EventHandler(this.mcbDoctor_EnterKeyPressed);
            // 
            // FromDate
            // 
            this.FromDate.CustomFormat = "";
            this.FromDate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(131, 16);
            this.FromDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.FromDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(122, 26);
            this.FromDate.TabIndex = 1042;
            this.FromDate.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.FromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FromDate_KeyDown);
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmount.CausesValidation = false;
            this.txtReportTotalAmount.Location = new System.Drawing.Point(436, 0);
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.Size = new System.Drawing.Size(181, 23);
            this.txtReportTotalAmount.TabIndex = 1;
            this.txtReportTotalAmount.Text = "label";
            this.txtReportTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UclSaleListDoctorCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclSaleListDoctorCompany";
            this.Size = new System.Drawing.Size(999, 591);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.pnlMultiSelection.ResumeLayout(false);
            this.pnlMultiSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGo;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.ToolTip ttToolTip;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight ViewToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight ViewFromDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblFromDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft txtViewText;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private System.Windows.Forms.Panel pnlMultiSelection;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel6;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel5;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel4;
        private System.Windows.Forms.DateTimePicker ToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbDoctor;
        private System.Windows.Forms.DateTimePicker FromDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtReportTotalAmount;
       
    }
}
