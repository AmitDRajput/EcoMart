namespace EcoMart.Reporting.Controls
{
    partial class UclStockListPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStockListPatient));
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlMultiSelection = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.btnOKMultiSelection = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.cbOnlyLessStock = new System.Windows.Forms.CheckBox();
            this.txtDays = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.txtReportTotalAmount = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlMultiSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(989, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.psLabel2);
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmount);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 509);
            this.MMBottomPanel.Size = new System.Drawing.Size(991, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.psLabel2, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Size = new System.Drawing.Size(991, 457);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection, 0);
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
            this.dgvReportList.Size = new System.Drawing.Size(989, 455);
            this.dgvReportList.TabIndex = 27;
            // 
            // pnlMultiSelection
            // 
            this.pnlMultiSelection.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection.Controls.Add(this.btnOKMultiSelection);
            this.pnlMultiSelection.Controls.Add(this.psLabel1);
            this.pnlMultiSelection.Controls.Add(this.psLabel3);
            this.pnlMultiSelection.Controls.Add(this.cbOnlyLessStock);
            this.pnlMultiSelection.Controls.Add(this.txtDays);
            this.pnlMultiSelection.Location = new System.Drawing.Point(276, 109);
            this.pnlMultiSelection.Name = "pnlMultiSelection";
            this.pnlMultiSelection.Size = new System.Drawing.Size(481, 73);
            this.pnlMultiSelection.TabIndex = 1043;
            // 
            // btnOKMultiSelection
            // 
            this.btnOKMultiSelection.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection.BackgroundImage")));
            this.btnOKMultiSelection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection.Image")));
            this.btnOKMultiSelection.Location = new System.Drawing.Point(412, 4);
            this.btnOKMultiSelection.Name = "btnOKMultiSelection";
            this.btnOKMultiSelection.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection.TabIndex = 1087;
            this.btnOKMultiSelection.Text = "Go";
            this.btnOKMultiSelection.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(228, 20);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(31, 16);
            this.psLabel1.TabIndex = 1086;
            this.psLabel1.Text = "Day";
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(13, 20);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(124, 16);
            this.psLabel3.TabIndex = 1085;
            this.psLabel3.Text = "Patients visiting on";
            // 
            // cbOnlyLessStock
            // 
            this.cbOnlyLessStock.AutoSize = true;
            this.cbOnlyLessStock.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOnlyLessStock.Location = new System.Drawing.Point(285, 24);
            this.cbOnlyLessStock.Name = "cbOnlyLessStock";
            this.cbOnlyLessStock.Size = new System.Drawing.Size(119, 19);
            this.cbOnlyLessStock.TabIndex = 1084;
            this.cbOnlyLessStock.Text = "Only Less Stock";
            this.cbOnlyLessStock.UseVisualStyleBackColor = true;
            // 
            // txtDays
            // 
            this.txtDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDays.Location = new System.Drawing.Point(171, 18);
            this.txtDays.Name = "txtDays";
            this.txtDays.Size = new System.Drawing.Size(48, 22);
            this.txtDays.TabIndex = 1083;
            this.txtDays.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDays_KeyDown);
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmount.CausesValidation = false;
            this.txtReportTotalAmount.Location = new System.Drawing.Point(802, 0);
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.Size = new System.Drawing.Size(127, 23);
            this.txtReportTotalAmount.TabIndex = 1;
            this.txtReportTotalAmount.Text = "label";
            this.txtReportTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(509, 4);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(214, 16);
            this.psLabel2.TabIndex = 2;
            this.psLabel2.Text = "Approx.Amount of Required Stock";
            // 
            // UclStockListPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclStockListPatient";
            this.Size = new System.Drawing.Size(991, 532);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlMultiSelection.ResumeLayout(false);
            this.pnlMultiSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private System.Windows.Forms.CheckBox cbOnlyLessStock;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtDays;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel2;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtReportTotalAmount;
    }
}
