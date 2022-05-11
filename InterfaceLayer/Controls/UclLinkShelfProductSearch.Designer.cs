namespace EcoMart.InterfaceLayer
{
    partial class UclLinkShelfProductSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclLinkShelfProductSearch));
            this.dgvSearchList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.SuspendLayout();
            // 
            // headerLabelForOverView1
            // 
            this.headerLabelForOverView1.Size = new System.Drawing.Size(688, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Size = new System.Drawing.Size(688, 448);
            // 
            // dgvSearchList
            // 
            this.dgvSearchList.BackColor = System.Drawing.Color.Transparent;
            this.dgvSearchList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSearchList.DateColumnNames")));
            this.dgvSearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSearchList.DoubleColumnNames")));
            this.dgvSearchList.Filter = null;
            this.dgvSearchList.Location = new System.Drawing.Point(0, 25);
            this.dgvSearchList.Name = "dgvSearchList";
            this.dgvSearchList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvSearchList.ShowGridFilter = false;
            this.dgvSearchList.Size = new System.Drawing.Size(688, 448);
            this.dgvSearchList.TabIndex = 30;
            // 
            // UclLinkShelfProductSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvSearchList);
            this.Name = "UclLinkShelfProductSearch";
            this.Size = new System.Drawing.Size(688, 473);
            this.Controls.SetChildIndex(this.MMMainPanel, 0);
            this.Controls.SetChildIndex(this.dgvSearchList, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvSearchList;
    }
}
