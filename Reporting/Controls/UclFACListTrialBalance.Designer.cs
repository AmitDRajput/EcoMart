namespace EcoMart.Reporting.Controls
{
    partial class UclFACListTrialBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclFACListTrialBalance));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlGo = new System.Windows.Forms.Panel();
            this.btnDifference = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.btnRemoveZero = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.btnFirstLevel = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.btnAlphabetical = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.btnFourthLevel = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.btnThirdLevel = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.btnSecondLevel = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.toDate1 = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.fromDate1 = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dgvReportListLevel1 = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.dgvDifference = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.dgvReportListLevel2 = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.dgvReportListLevel3 = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.dgvReportListLevel4 = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(985, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 531);
            this.MMBottomPanel.Size = new System.Drawing.Size(987, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel2);
            this.MMMainPanel.Size = new System.Drawing.Size(987, 479);
            this.MMMainPanel.Controls.SetChildIndex(this.panel2, 0);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dgvReportListLevel4);
            this.panel2.Controls.Add(this.dgvReportListLevel3);
            this.panel2.Controls.Add(this.dgvReportList);
            this.panel2.Controls.Add(this.dgvReportListLevel2);
            this.panel2.Controls.Add(this.dgvDifference);
            this.panel2.Controls.Add(this.dgvReportListLevel1);
            this.panel2.Controls.Add(this.pnlGo);
            this.panel2.Controls.Add(this.pnlMultiSelection1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(985, 477);
            this.panel2.TabIndex = 44;
            // 
            // pnlGo
            // 
            this.pnlGo.BackColor = System.Drawing.Color.Plum;
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.btnDifference);
            this.pnlGo.Controls.Add(this.btnRemoveZero);
            this.pnlGo.Controls.Add(this.btnFirstLevel);
            this.pnlGo.Controls.Add(this.btnAlphabetical);
            this.pnlGo.Controls.Add(this.btnFourthLevel);
            this.pnlGo.Controls.Add(this.btnThirdLevel);
            this.pnlGo.Controls.Add(this.btnSecondLevel);
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.psLabel2);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(983, 33);
            this.pnlGo.TabIndex = 1043;
            // 
            // btnDifference
            // 
            this.btnDifference.Location = new System.Drawing.Point(514, 1);
            this.btnDifference.Name = "btnDifference";
            this.btnDifference.Size = new System.Drawing.Size(84, 28);
            this.btnDifference.TabIndex = 1085;
            this.btnDifference.Text = "Diff";
            this.btnDifference.UseVisualStyleBackColor = true;
            this.btnDifference.Click += new System.EventHandler(this.btnDifference_Click);
            // 
            // btnRemoveZero
            // 
            this.btnRemoveZero.Location = new System.Drawing.Point(411, 1);
            this.btnRemoveZero.Name = "btnRemoveZero";
            this.btnRemoveZero.Size = new System.Drawing.Size(101, 28);
            this.btnRemoveZero.TabIndex = 1084;
            this.btnRemoveZero.Text = "Remove Zero";
            this.btnRemoveZero.UseVisualStyleBackColor = true;
            this.btnRemoveZero.Click += new System.EventHandler(this.btnRemoveZero_Click);
            // 
            // btnFirstLevel
            // 
            this.btnFirstLevel.Location = new System.Drawing.Point(4, 1);
            this.btnFirstLevel.Name = "btnFirstLevel";
            this.btnFirstLevel.Size = new System.Drawing.Size(76, 28);
            this.btnFirstLevel.TabIndex = 1083;
            this.btnFirstLevel.Text = "1st Level";
            this.btnFirstLevel.UseVisualStyleBackColor = true;
            this.btnFirstLevel.Click += new System.EventHandler(this.btnFirstLevel_Click);
            // 
            // btnAlphabetical
            // 
            this.btnAlphabetical.Location = new System.Drawing.Point(316, 1);
            this.btnAlphabetical.Name = "btnAlphabetical";
            this.btnAlphabetical.Size = new System.Drawing.Size(93, 28);
            this.btnAlphabetical.TabIndex = 1082;
            this.btnAlphabetical.Text = "Alphabetical";
            this.btnAlphabetical.UseVisualStyleBackColor = true;
            this.btnAlphabetical.Click += new System.EventHandler(this.btnAlphabetical_Click);
            // 
            // btnFourthLevel
            // 
            this.btnFourthLevel.Location = new System.Drawing.Point(238, 1);
            this.btnFourthLevel.Name = "btnFourthLevel";
            this.btnFourthLevel.Size = new System.Drawing.Size(76, 28);
            this.btnFourthLevel.TabIndex = 1081;
            this.btnFourthLevel.Text = "4th Level";
            this.btnFourthLevel.UseVisualStyleBackColor = true;
            this.btnFourthLevel.Click += new System.EventHandler(this.btnFourthLevel_Click);
            // 
            // btnThirdLevel
            // 
            this.btnThirdLevel.Location = new System.Drawing.Point(160, 1);
            this.btnThirdLevel.Name = "btnThirdLevel";
            this.btnThirdLevel.Size = new System.Drawing.Size(76, 28);
            this.btnThirdLevel.TabIndex = 1080;
            this.btnThirdLevel.Text = "3rd Level";
            this.btnThirdLevel.UseVisualStyleBackColor = true;
            this.btnThirdLevel.Click += new System.EventHandler(this.btnThirdLevel_Click);
            // 
            // btnSecondLevel
            // 
            this.btnSecondLevel.Location = new System.Drawing.Point(82, 1);
            this.btnSecondLevel.Name = "btnSecondLevel";
            this.btnSecondLevel.Size = new System.Drawing.Size(76, 28);
            this.btnSecondLevel.TabIndex = 1079;
            this.btnSecondLevel.Text = "2nd Level";
            this.btnSecondLevel.UseVisualStyleBackColor = true;
            this.btnSecondLevel.Click += new System.EventHandler(this.btnSecondLevel_Click);
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(866, 3);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1078;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(717, 3);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1077;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(832, 3);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(21, 14);
            this.psLabel2.TabIndex = 1076;
            this.psLabel2.Text = "To";
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.toDate1);
            this.pnlMultiSelection1.Controls.Add(this.fromDate1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(333, 145);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(291, 85);
            this.pnlMultiSelection1.TabIndex = 1048;
            // 
            // toDate1
            // 
            this.toDate1.CustomFormat = "dd/MM/yyyy";
            this.toDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate1.Location = new System.Drawing.Point(72, 45);
            this.toDate1.Name = "toDate1";
            this.toDate1.Size = new System.Drawing.Size(125, 24);
            this.toDate1.TabIndex = 1071;
            this.toDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toDate1_KeyDown);
            // 
            // fromDate1
            // 
            this.fromDate1.CustomFormat = "dd/MM/yyyy";
            this.fromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate1.Location = new System.Drawing.Point(72, 10);
            this.fromDate1.Name = "fromDate1";
            this.fromDate1.Size = new System.Drawing.Size(125, 24);
            this.fromDate1.TabIndex = 1070;
            this.fromDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromDate1_KeyDown);
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(13, 14);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(38, 14);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "From";
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(224, 2);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 3;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(33, 48);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(21, 14);
            this.mPlbl2.TabIndex = 2;
            this.mPlbl2.Text = "To";
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(654, 3);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(48, 19);
            this.psLabel1.TabIndex = 1075;
            this.psLabel1.Text = "From";
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // dgvReportListLevel1
            // 
            this.dgvReportListLevel1.ApplyAlternateRowStyle = false;
            this.dgvReportListLevel1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportListLevel1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvReportListLevel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportListLevel1.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel1.ConvertDatetoMonth")));
            this.dgvReportListLevel1.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel1.DateColumnNames")));
            this.dgvReportListLevel1.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel1.DoubleColumnNames")));
            this.dgvReportListLevel1.FreezeLastRow = false;
            this.dgvReportListLevel1.Location = new System.Drawing.Point(477, 224);
            this.dgvReportListLevel1.Name = "dgvReportListLevel1";
            this.dgvReportListLevel1.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel1.NumericColumnNames")));
            this.dgvReportListLevel1.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel1.OptionalColumnNames")));
            this.dgvReportListLevel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportListLevel1.Size = new System.Drawing.Size(28, 27);
            this.dgvReportListLevel1.TabIndex = 1058;
            // 
            // dgvDifference
            // 
            this.dgvDifference.ApplyAlternateRowStyle = false;
            this.dgvDifference.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvDifference.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvDifference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvDifference.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvDifference.ConvertDatetoMonth")));
            this.dgvDifference.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvDifference.DateColumnNames")));
            this.dgvDifference.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvDifference.DoubleColumnNames")));
            this.dgvDifference.FreezeLastRow = false;
            this.dgvDifference.Location = new System.Drawing.Point(503, 273);
            this.dgvDifference.Name = "dgvDifference";
            this.dgvDifference.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvDifference.NumericColumnNames")));
            this.dgvDifference.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvDifference.OptionalColumnNames")));
            this.dgvDifference.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvDifference.Size = new System.Drawing.Size(28, 27);
            this.dgvDifference.TabIndex = 1059;
            // 
            // dgvReportListLevel2
            // 
            this.dgvReportListLevel2.ApplyAlternateRowStyle = false;
            this.dgvReportListLevel2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportListLevel2.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvReportListLevel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportListLevel2.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel2.ConvertDatetoMonth")));
            this.dgvReportListLevel2.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel2.DateColumnNames")));
            this.dgvReportListLevel2.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel2.DoubleColumnNames")));
            this.dgvReportListLevel2.FreezeLastRow = false;
            this.dgvReportListLevel2.Location = new System.Drawing.Point(540, 304);
            this.dgvReportListLevel2.Name = "dgvReportListLevel2";
            this.dgvReportListLevel2.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel2.NumericColumnNames")));
            this.dgvReportListLevel2.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel2.OptionalColumnNames")));
            this.dgvReportListLevel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportListLevel2.Size = new System.Drawing.Size(28, 27);
            this.dgvReportListLevel2.TabIndex = 1060;
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = false;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.FreezeLastRow = false;
            this.dgvReportList.Location = new System.Drawing.Point(287, 273);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(28, 27);
            this.dgvReportList.TabIndex = 1061;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // dgvReportListLevel3
            // 
            this.dgvReportListLevel3.ApplyAlternateRowStyle = false;
            this.dgvReportListLevel3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportListLevel3.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvReportListLevel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportListLevel3.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel3.ConvertDatetoMonth")));
            this.dgvReportListLevel3.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel3.DateColumnNames")));
            this.dgvReportListLevel3.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel3.DoubleColumnNames")));
            this.dgvReportListLevel3.FreezeLastRow = false;
            this.dgvReportListLevel3.Location = new System.Drawing.Point(182, 224);
            this.dgvReportListLevel3.Name = "dgvReportListLevel3";
            this.dgvReportListLevel3.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel3.NumericColumnNames")));
            this.dgvReportListLevel3.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel3.OptionalColumnNames")));
            this.dgvReportListLevel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportListLevel3.Size = new System.Drawing.Size(28, 27);
            this.dgvReportListLevel3.TabIndex = 1062;
            this.dgvReportListLevel3.DoubleClicked += new System.EventHandler(this.dgvReportListLevel3_DoubleClicked);
            // 
            // dgvReportListLevel4
            // 
            this.dgvReportListLevel4.ApplyAlternateRowStyle = false;
            this.dgvReportListLevel4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportListLevel4.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvReportListLevel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportListLevel4.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel4.ConvertDatetoMonth")));
            this.dgvReportListLevel4.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel4.DateColumnNames")));
            this.dgvReportListLevel4.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel4.DoubleColumnNames")));
            this.dgvReportListLevel4.FreezeLastRow = false;
            this.dgvReportListLevel4.Location = new System.Drawing.Point(108, 304);
            this.dgvReportListLevel4.Name = "dgvReportListLevel4";
            this.dgvReportListLevel4.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel4.NumericColumnNames")));
            this.dgvReportListLevel4.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportListLevel4.OptionalColumnNames")));
            this.dgvReportListLevel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportListLevel4.Size = new System.Drawing.Size(28, 27);
            this.dgvReportListLevel4.TabIndex = 1063;
            this.dgvReportListLevel4.DoubleClicked += new System.EventHandler(this.dgvReportListLevel4_DoubleClicked);
            // 
            // UclFACListTrialBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclFACListTrialBalance";
            this.Size = new System.Drawing.Size(987, 554);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlGo;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.ToDate toDate1;
        private EcoMart.InterfaceLayer.CommonControls.FromDate fromDate1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private System.Windows.Forms.ToolTip ttToolTip;
        private PharmaSYSPlus.CommonLibrary.PSButton btnThirdLevel;
        private PharmaSYSPlus.CommonLibrary.PSButton btnSecondLevel;
        private PharmaSYSPlus.CommonLibrary.PSButton btnAlphabetical;
        private PharmaSYSPlus.CommonLibrary.PSButton btnFourthLevel;
        private PharmaSYSPlus.CommonLibrary.PSButton btnFirstLevel;
        private PharmaSYSPlus.CommonLibrary.PSButton btnRemoveZero;
        private PharmaSYSPlus.CommonLibrary.PSButton btnDifference;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportListLevel4;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportListLevel3;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportListLevel2;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvDifference;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportListLevel1;
                
    }
}
