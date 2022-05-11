namespace EcoMart.Reporting.Controls
{
    partial class UclMISListSummaryFromToBillNumber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclMISListSummaryFromToBillNumber));
            this.pnlGo = new System.Windows.Forms.Panel();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewtext = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.txtToNumber = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.txtFromNumber = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnOKMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.dgvChangedBills = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.btndgvChangedBills = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(954, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 516);
            this.MMBottomPanel.Size = new System.Drawing.Size(956, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.btndgvChangedBills);
            this.MMMainPanel.Controls.Add(this.dgvChangedBills);
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(956, 464);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlGo, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvChangedBills, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.btndgvChangedBills, 0);
            // 
            // pnlGo
            // 
            this.pnlGo.BackColor = System.Drawing.Color.Plum;
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.psLabel2);
            this.pnlGo.Controls.Add(this.txtViewtext);
            this.pnlGo.Controls.Add(this.txtType);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(954, 33);
            this.pnlGo.TabIndex = 1051;
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(816, 3);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1078;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(688, 3);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1077;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(792, 8);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(19, 17);
            this.psLabel2.TabIndex = 1076;
            this.psLabel2.Text = " - ";
            // 
            // txtViewtext
            // 
            this.txtViewtext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewtext.Location = new System.Drawing.Point(83, 3);
            this.txtViewtext.Name = "txtViewtext";
            this.txtViewtext.Size = new System.Drawing.Size(157, 22);
            this.txtViewtext.TabIndex = 1064;
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.Location = new System.Drawing.Point(297, 3);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(57, 22);
            this.txtType.TabIndex = 1062;
            this.txtType.Visible = false;
            this.txtType.WordWrap = false;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.txtToNumber);
            this.pnlMultiSelection1.Controls.Add(this.txtFromNumber);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(374, 126);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(291, 70);
            this.pnlMultiSelection1.TabIndex = 1054;
            // 
            // txtToNumber
            // 
            this.txtToNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToNumber.Location = new System.Drawing.Point(72, 39);
            this.txtToNumber.Name = "txtToNumber";
            this.txtToNumber.Size = new System.Drawing.Size(100, 22);
            this.txtToNumber.TabIndex = 5;
            this.txtToNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToNumber_KeyDown);
            // 
            // txtFromNumber
            // 
            this.txtFromNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromNumber.Location = new System.Drawing.Point(72, 9);
            this.txtFromNumber.Name = "txtFromNumber";
            this.txtFromNumber.Size = new System.Drawing.Size(100, 22);
            this.txtFromNumber.TabIndex = 4;
            this.txtFromNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFromNumber_KeyDown);
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(13, 11);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(41, 17);
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
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(33, 42);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(22, 17);
            this.mPlbl2.TabIndex = 2;
            this.mPlbl2.Text = "To";
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
            this.dgvReportList.FreezeLastRow = false;
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(954, 429);
            this.dgvReportList.TabIndex = 1053;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            this.dgvReportList.TabKeyPressed += new System.EventHandler(this.dgvReportList_TabKeyPressed);
            // 
            // dgvChangedBills
            // 
            this.dgvChangedBills.ApplyAlternateRowStyle = false;
            this.dgvChangedBills.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvChangedBills.BackColor = System.Drawing.Color.Khaki;
            this.dgvChangedBills.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvChangedBills.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvChangedBills.ConvertDatetoMonth")));
            this.dgvChangedBills.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvChangedBills.DateColumnNames")));
            this.dgvChangedBills.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvChangedBills.DoubleColumnNames")));
            this.dgvChangedBills.FreezeLastRow = false;
            this.dgvChangedBills.Location = new System.Drawing.Point(671, 52);
            this.dgvChangedBills.Name = "dgvChangedBills";
            this.dgvChangedBills.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvChangedBills.NumericColumnNames")));
            this.dgvChangedBills.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvChangedBills.OptionalColumnNames")));
            this.dgvChangedBills.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvChangedBills.Size = new System.Drawing.Size(268, 200);
            this.dgvChangedBills.TabIndex = 1055;
            this.dgvChangedBills.DoubleClicked += new System.EventHandler(this.dgvChangedBills_DoubleClicked);
            // 
            // btndgvChangedBills
            // 
            this.btndgvChangedBills.Location = new System.Drawing.Point(775, 258);
            this.btndgvChangedBills.Name = "btndgvChangedBills";
            this.btndgvChangedBills.Size = new System.Drawing.Size(93, 28);
            this.btndgvChangedBills.TabIndex = 1056;
            this.btndgvChangedBills.Text = "Close";
            this.btndgvChangedBills.UseVisualStyleBackColor = false;
            this.btndgvChangedBills.Click += new System.EventHandler(this.btndgvChangedBills_Click);
            // 
            // UclMISListSummaryFromToBillNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclMISListSummaryFromToBillNumber";
            this.Size = new System.Drawing.Size(956, 539);
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
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private InterfaceLayer.CommonControls.PSLabel psLabel2;
        private System.Windows.Forms.TextBox txtType;
        private InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
        private InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private InterfaceLayer.CommonControls.PSTextBox txtViewtext;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtToNumber;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtFromNumber;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvChangedBills;
        private PharmaSYSPlus.CommonLibrary.PSButton btndgvChangedBills;
    }
}
