namespace EcoMart.InterfaceLayer
{
    partial class UclSaleSummaryControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclSaleSummaryControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTotalSummary = new System.Windows.Forms.Panel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.DataGridViewContent = new PharmaSYSPlus.CommonLibrary.MReportGridViewBase(this.components);
            this.mReportGridViewBase1 = new PharmaSYSPlus.CommonLibrary.MReportGridViewBase(this.components);
            this.pnlGo = new System.Windows.Forms.Panel();
            this.lblViewFrom = new System.Windows.Forms.Label();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewtext = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.pnlTotalSummary.SuspendLayout();
            this.dgvReportList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mReportGridViewBase1)).BeginInit();
            this.pnlGo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTotalSummary
            // 
            this.pnlTotalSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotalSummary.Controls.Add(this.dgvReportList);
            this.pnlTotalSummary.Controls.Add(this.pnlGo);
            this.pnlTotalSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotalSummary.Location = new System.Drawing.Point(0, 0);
            this.pnlTotalSummary.Name = "pnlTotalSummary";
            this.pnlTotalSummary.Size = new System.Drawing.Size(764, 285);
            this.pnlTotalSummary.TabIndex = 1110;
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = false;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.Khaki;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.Controls.Add(this.DataGridViewContent);
            this.dgvReportList.Controls.Add(this.mReportGridViewBase1);
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
            this.dgvReportList.Size = new System.Drawing.Size(762, 250);
            this.dgvReportList.TabIndex = 1051;
            // 
            // DataGridViewContent
            // 
            this.DataGridViewContent.AllowUserToAddRows = false;
            this.DataGridViewContent.AllowUserToDeleteRows = false;
            this.DataGridViewContent.AllowUserToOrderColumns = true;
            this.DataGridViewContent.AllowUserToResizeRows = false;
            this.DataGridViewContent.BackgroundColor = System.Drawing.Color.RosyBrown;
            this.DataGridViewContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewContent.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridViewContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridViewContent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DataGridViewContent.FreezeLastRow = false;
            this.DataGridViewContent.Location = new System.Drawing.Point(0, 0);
            this.DataGridViewContent.MultiSelect = false;
            this.DataGridViewContent.Name = "DataGridViewContent";
            this.DataGridViewContent.RowHeadersVisible = false;
            this.DataGridViewContent.RowHeadersWidth = 25;
            this.DataGridViewContent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.DataGridViewContent.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewContent.Size = new System.Drawing.Size(760, 248);
            this.DataGridViewContent.TabIndex = 5;
            // 
            // mReportGridViewBase1
            // 
            this.mReportGridViewBase1.AllowUserToAddRows = false;
            this.mReportGridViewBase1.AllowUserToDeleteRows = false;
            this.mReportGridViewBase1.AllowUserToOrderColumns = true;
            this.mReportGridViewBase1.AllowUserToResizeRows = false;
            this.mReportGridViewBase1.BackgroundColor = System.Drawing.Color.RosyBrown;
            this.mReportGridViewBase1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mReportGridViewBase1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.mReportGridViewBase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mReportGridViewBase1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.mReportGridViewBase1.FreezeLastRow = false;
            this.mReportGridViewBase1.Location = new System.Drawing.Point(0, 0);
            this.mReportGridViewBase1.MultiSelect = false;
            this.mReportGridViewBase1.Name = "mReportGridViewBase1";
            this.mReportGridViewBase1.RowHeadersVisible = false;
            this.mReportGridViewBase1.RowHeadersWidth = 25;
            this.mReportGridViewBase1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.mReportGridViewBase1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.mReportGridViewBase1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mReportGridViewBase1.Size = new System.Drawing.Size(760, 248);
            this.mReportGridViewBase1.TabIndex = 5;
            // 
            // pnlGo
            // 
            this.pnlGo.BackColor = System.Drawing.Color.Plum;
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.lblViewFrom);
            this.pnlGo.Controls.Add(this.ViewToDate);
            this.pnlGo.Controls.Add(this.ViewFromDate);
            this.pnlGo.Controls.Add(this.psLabel3);
            this.pnlGo.Controls.Add(this.txtViewtext);
            this.pnlGo.Controls.Add(this.label3);
            this.pnlGo.Controls.Add(this.txtType);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(762, 33);
            this.pnlGo.TabIndex = 1050;
            // 
            // lblViewFrom
            // 
            this.lblViewFrom.AutoSize = true;
            this.lblViewFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewFrom.Location = new System.Drawing.Point(364, 7);
            this.lblViewFrom.Name = "lblViewFrom";
            this.lblViewFrom.Size = new System.Drawing.Size(39, 15);
            this.lblViewFrom.TabIndex = 1079;
            this.lblViewFrom.Text = "From";
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(563, 3);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1078;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(414, 3);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1077;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(529, 3);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(22, 16);
            this.psLabel3.TabIndex = 1076;
            this.psLabel3.Text = "To";
            // 
            // txtViewtext
            // 
            this.txtViewtext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewtext.IsNumericDataSet = false;
            this.txtViewtext.Location = new System.Drawing.Point(83, 3);
            this.txtViewtext.Name = "txtViewtext";
            this.txtViewtext.Size = new System.Drawing.Size(157, 22);
            this.txtViewtext.TabIndex = 1064;
            this.txtViewtext.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 1063;
            this.label3.Text = "Type";
            this.label3.Visible = false;
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.Location = new System.Drawing.Point(254, 3);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(57, 22);
            this.txtType.TabIndex = 1062;
            this.txtType.Visible = false;
            // 
            // UclSaleSummaryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTotalSummary);
            this.Name = "UclSaleSummaryControl";
            this.Size = new System.Drawing.Size(764, 285);
            this.pnlTotalSummary.ResumeLayout(false);
            this.dgvReportList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mReportGridViewBase1)).EndInit();
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTotalSummary;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private PharmaSYSPlus.CommonLibrary.MReportGridViewBase DataGridViewContent;
        private PharmaSYSPlus.CommonLibrary.MReportGridViewBase mReportGridViewBase1;
        private System.Windows.Forms.Panel pnlGo;
        private System.Windows.Forms.Label lblViewFrom;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private EcoMart.InterfaceLayer.CommonControls.PSTextBox txtViewtext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtType;
    }
}
