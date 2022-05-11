namespace EcoMart.Reporting.Controls
{
    partial class UclListBank
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclListBank));
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(944, 23);
            // 
            // MMBottomPanel
            //             
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 510);
            this.MMBottomPanel.Size = new System.Drawing.Size(946, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Size = new System.Drawing.Size(946, 458);
            // 
            // dgvReportList
            // 
            this.dgvReportList.BackColor = System.Drawing.Color.Transparent;
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;           
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;         
            this.dgvReportList.Size = new System.Drawing.Size(944, 456);
            this.dgvReportList.TabIndex = 1054;            
            // 
            // UclListBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Name = "UclListBank";
            this.Size = new System.Drawing.Size(946, 533);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
       
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        
    }
}
