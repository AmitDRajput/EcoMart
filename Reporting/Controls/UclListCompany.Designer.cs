namespace EcoMart.Reporting.Controls
{
    partial class UclListCompany
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclListCompany));
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(666, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 322);
            this.MMBottomPanel.Size = new System.Drawing.Size(668, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Size = new System.Drawing.Size(668, 270);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvReportList, 0);
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = true;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.Transparent;
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.FreezeLastRow = false;
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.NumericColumnNames")));
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvReportList.Size = new System.Drawing.Size(666, 268);
            this.dgvReportList.TabIndex = 24;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
            // 
            // UclListCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclListCompany";
            this.Size = new System.Drawing.Size(668, 345);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
       
    }
}
