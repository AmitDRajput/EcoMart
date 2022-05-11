namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclStockListAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStockListAll));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtAlphabetical = new System.Windows.Forms.RadioButton();
            this.rbtCompanyWise = new System.Windows.Forms.RadioButton();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.rbtMRP = new System.Windows.Forms.RadioButton();
            this.rbtPurhcaseRate = new System.Windows.Forms.RadioButton();
            this.rbtNone = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblOPStockValue = new System.Windows.Forms.Label();
            this.lblCLStockValue = new System.Windows.Forms.Label();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMultiSelection = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.btnOKMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.txtReportTotalAmountOP = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtReportTotalAmountCL = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pnlMultiSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(957, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmountCL);
            this.MMBottomPanel.Controls.Add(this.lblCLStockValue);
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmountOP);
            this.MMBottomPanel.Controls.Add(this.lblOPStockValue);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 565);
            this.MMBottomPanel.Size = new System.Drawing.Size(959, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblOPStockValue, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmountOP, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblCLStockValue, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmountCL, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Size = new System.Drawing.Size(959, 513);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtAlphabetical);
            this.groupBox1.Controls.Add(this.rbtCompanyWise);
            this.groupBox1.Location = new System.Drawing.Point(12, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 60);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            // 
            // rbtAlphabetical
            // 
            this.rbtAlphabetical.AutoSize = true;
            this.rbtAlphabetical.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtAlphabetical.Location = new System.Drawing.Point(6, 35);
            this.rbtAlphabetical.Name = "rbtAlphabetical";
            this.rbtAlphabetical.Size = new System.Drawing.Size(114, 21);
            this.rbtAlphabetical.TabIndex = 1;
            this.rbtAlphabetical.TabStop = true;
            this.rbtAlphabetical.Text = "Alphabetical";
            this.rbtAlphabetical.UseVisualStyleBackColor = true;
            // 
            // rbtCompanyWise
            // 
            this.rbtCompanyWise.AutoSize = true;
            this.rbtCompanyWise.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCompanyWise.Location = new System.Drawing.Point(6, 8);
            this.rbtCompanyWise.Name = "rbtCompanyWise";
            this.rbtCompanyWise.Size = new System.Drawing.Size(123, 21);
            this.rbtCompanyWise.TabIndex = 0;
            this.rbtCompanyWise.TabStop = true;
            this.rbtCompanyWise.Text = "Companywise";
            this.rbtCompanyWise.UseVisualStyleBackColor = true;
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
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(957, 511);
            this.dgvReportList.TabIndex = 27;
            // 
            // rbtMRP
            // 
            this.rbtMRP.AutoSize = true;
            this.rbtMRP.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtMRP.Location = new System.Drawing.Point(17, 21);
            this.rbtMRP.Name = "rbtMRP";
            this.rbtMRP.Size = new System.Drawing.Size(58, 21);
            this.rbtMRP.TabIndex = 3;
            this.rbtMRP.TabStop = true;
            this.rbtMRP.Text = "MRP";
            this.rbtMRP.UseVisualStyleBackColor = true;
            // 
            // rbtPurhcaseRate
            // 
            this.rbtPurhcaseRate.AutoSize = true;
            this.rbtPurhcaseRate.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtPurhcaseRate.Location = new System.Drawing.Point(75, 21);
            this.rbtPurhcaseRate.Name = "rbtPurhcaseRate";
            this.rbtPurhcaseRate.Size = new System.Drawing.Size(124, 21);
            this.rbtPurhcaseRate.TabIndex = 4;
            this.rbtPurhcaseRate.TabStop = true;
            this.rbtPurhcaseRate.Text = "Purchase Rate";
            this.rbtPurhcaseRate.UseVisualStyleBackColor = true;
            // 
            // rbtNone
            // 
            this.rbtNone.AutoSize = true;
            this.rbtNone.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtNone.Location = new System.Drawing.Point(199, 21);
            this.rbtNone.Name = "rbtNone";
            this.rbtNone.Size = new System.Drawing.Size(62, 21);
            this.rbtNone.TabIndex = 5;
            this.rbtNone.TabStop = true;
            this.rbtNone.Text = "None";
            this.rbtNone.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtNone);
            this.groupBox3.Controls.Add(this.rbtPurhcaseRate);
            this.groupBox3.Controls.Add(this.rbtMRP);
            this.groupBox3.Location = new System.Drawing.Point(12, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(293, 48);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Value By";
            // 
            // lblOPStockValue
            // 
            this.lblOPStockValue.AutoSize = true;
            this.lblOPStockValue.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOPStockValue.Location = new System.Drawing.Point(436, 3);
            this.lblOPStockValue.Name = "lblOPStockValue";
            this.lblOPStockValue.Size = new System.Drawing.Size(95, 17);
            this.lblOPStockValue.TabIndex = 1054;
            this.lblOPStockValue.Text = "OP Stk Value";
            // 
            // lblCLStockValue
            // 
            this.lblCLStockValue.AutoSize = true;
            this.lblCLStockValue.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCLStockValue.Location = new System.Drawing.Point(695, 2);
            this.lblCLStockValue.Name = "lblCLStockValue";
            this.lblCLStockValue.Size = new System.Drawing.Size(93, 17);
            this.lblCLStockValue.TabIndex = 1055;
            this.lblCLStockValue.Text = "CL Stk Value";
            // 
            // pnlMultiSelection
            // 
            this.pnlMultiSelection.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection.Controls.Add(this.btnOKMultiSelection1);
            this.pnlMultiSelection.Controls.Add(this.groupBox3);
            this.pnlMultiSelection.Controls.Add(this.groupBox1);
            this.pnlMultiSelection.Location = new System.Drawing.Point(363, 81);
            this.pnlMultiSelection.Name = "pnlMultiSelection";
            this.pnlMultiSelection.Size = new System.Drawing.Size(308, 147);
            this.pnlMultiSelection.TabIndex = 1046;
            // 
            // btnOKMultiSelection1
            // 
            this.btnOKMultiSelection1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.BackgroundImage")));
            this.btnOKMultiSelection1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection1.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection1.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection1.Image")));
            this.btnOKMultiSelection1.Location = new System.Drawing.Point(242, 3);
            this.btnOKMultiSelection1.Name = "btnOKMultiSelection1";
            this.btnOKMultiSelection1.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection1.TabIndex = 48;
            this.btnOKMultiSelection1.Text = "Go";
            this.btnOKMultiSelection1.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection1.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // txtReportTotalAmountOP
            // 
            this.txtReportTotalAmountOP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmountOP.CausesValidation = false;
            this.txtReportTotalAmountOP.Location = new System.Drawing.Point(534, -1);
            this.txtReportTotalAmountOP.Name = "txtReportTotalAmountOP";
            this.txtReportTotalAmountOP.Size = new System.Drawing.Size(155, 23);
            this.txtReportTotalAmountOP.TabIndex = 1056;
            this.txtReportTotalAmountOP.Text = "label";
            this.txtReportTotalAmountOP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReportTotalAmountCL
            // 
            this.txtReportTotalAmountCL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmountCL.CausesValidation = false;
            this.txtReportTotalAmountCL.Location = new System.Drawing.Point(789, -2);
            this.txtReportTotalAmountCL.Name = "txtReportTotalAmountCL";
            this.txtReportTotalAmountCL.Size = new System.Drawing.Size(155, 23);
            this.txtReportTotalAmountCL.TabIndex = 1057;
            this.txtReportTotalAmountCL.Text = "label";
            this.txtReportTotalAmountCL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UclStockListAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclStockListAll";
            this.Size = new System.Drawing.Size(959, 588);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.pnlMultiSelection.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtAlphabetical;
        private System.Windows.Forms.RadioButton rbtCompanyWise;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
      
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtNone;
        private System.Windows.Forms.RadioButton rbtPurhcaseRate;
        private System.Windows.Forms.RadioButton rbtMRP;
        private System.Windows.Forms.Label lblCLStockValue;
        private System.Windows.Forms.Label lblOPStockValue;
        private System.Windows.Forms.ToolTip ttToolTip;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtReportTotalAmountCL;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtReportTotalAmountOP;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection1;
    }
}
