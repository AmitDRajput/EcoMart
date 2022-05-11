using PharmaSYSPlus.CommonLibrary;
namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclBranchSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclBranchSearch));
            this.mDataGridView1 = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.dgvSearchList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabelForOverView1
            // 
            this.headerLabelForOverView1.Size = new System.Drawing.Size(402, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.dgvSearchList);
            this.MMMainPanel.Size = new System.Drawing.Size(402, 329);
            // 
            // mDataGridView1
            // 
            this.mDataGridView1.BackColor = System.Drawing.Color.Transparent;
            this.mDataGridView1.Filter = null;
            this.mDataGridView1.Location = new System.Drawing.Point(66, 50);
            this.mDataGridView1.Name = "mDataGridView1";
            this.mDataGridView1.ShowGridFilter = false;
            this.mDataGridView1.Size = new System.Drawing.Size(8, 44);
            this.mDataGridView1.TabIndex = 24;
            // 
            // dgvSearchList
            // 
            this.dgvSearchList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvSearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchList.Filter = null;
            this.dgvSearchList.Location = new System.Drawing.Point(0, 0);
            this.dgvSearchList.Name = "dgvSearchList";
            this.dgvSearchList.ShowGridFilter = false;
            this.dgvSearchList.Size = new System.Drawing.Size(402, 329);
            this.dgvSearchList.TabIndex = 26;
            this.dgvSearchList.DoubleClicked += new System.EventHandler(this.dgvSearchList_DoubleClicked);
            // 
            // UclBranchSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mDataGridView1);
            this.Name = "UclBranchSearch";
            this.Size = new System.Drawing.Size(402, 354);
            this.Controls.SetChildIndex(this.mDataGridView1, 0);
            this.Controls.SetChildIndex(this.MMMainPanel, 0);
            this.MMMainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MDataGridView mDataGridView1;
        private MDataGridView dgvSearchList;
    }
}
