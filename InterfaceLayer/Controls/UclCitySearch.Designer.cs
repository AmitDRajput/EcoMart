
namespace EcoMart.InterfaceLayer
{
    partial class UclCitySearch
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclCitySearch));
            this.dgvSearchList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.mDataGridView1 = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.MMDatePanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabelForOverView1
            // 
            this.headerLabelForOverView1.Size = new System.Drawing.Size(595, 23);
            // 
            // MMDatePanel
            // 
            this.MMDatePanel.Size = new System.Drawing.Size(595, 30);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.mDataGridView1);
            this.MMMainPanel.Size = new System.Drawing.Size(595, 395);
            // 
            // dgvSearchList
            // 
            this.dgvSearchList.BackColor = System.Drawing.Color.Transparent;
            this.dgvSearchList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSearchList.DateColumnNames")));
            this.dgvSearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSearchList.DoubleColumnNames")));
            this.dgvSearchList.Filter = null;
            this.dgvSearchList.Location = new System.Drawing.Point(0, 55);
            this.dgvSearchList.Name = "dgvSearchList";
            this.dgvSearchList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvSearchList.ShowGridFilter = false;
            this.dgvSearchList.Size = new System.Drawing.Size(595, 395);
            this.dgvSearchList.TabIndex = 24;
            this.dgvSearchList.DoubleClicked += new System.EventHandler(this.dgvSearchList_DoubleClicked);
            // 
            // mDataGridView1
            // 
            this.mDataGridView1.BackColor = System.Drawing.Color.Transparent;
            this.mDataGridView1.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mDataGridView1.DateColumnNames")));
            this.mDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mDataGridView1.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mDataGridView1.DoubleColumnNames")));
            this.mDataGridView1.Filter = null;
            this.mDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.mDataGridView1.Name = "mDataGridView1";
            this.mDataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mDataGridView1.ShowGridFilter = false;
            this.mDataGridView1.Size = new System.Drawing.Size(595, 395);
            this.mDataGridView1.TabIndex = 24;
            // 
            // UclCitySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvSearchList);
            this.Name = "UclCitySearch";
            this.Size = new System.Drawing.Size(595, 450);
            this.Controls.SetChildIndex(this.MMDatePanel, 0);
            this.Controls.SetChildIndex(this.MMMainPanel, 0);
            this.Controls.SetChildIndex(this.dgvSearchList, 0);
            this.MMDatePanel.ResumeLayout(false);
            this.MMDatePanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvSearchList;
        private PharmaSYSPlus.CommonLibrary.MDataGridView mDataGridView1;
    }
}