using PharmaSYSPlus.CommonLibrary;
namespace EcoMart.InterfaceLayer
{
    partial class UclDrugGroupingSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclDrugGroupingSearch));
            this.dgvSearchList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.SuspendLayout();
            // 
            // headerLabelForOverView1
            // 
            this.headerLabelForOverView1.Size = new System.Drawing.Size(895, 23);
            // 
            // dgvSearchList
            // 
            this.dgvSearchList.BackColor = System.Drawing.Color.Transparent;
            this.dgvSearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchList.Filter = null;
            this.dgvSearchList.Location = new System.Drawing.Point(0, 23);
            this.dgvSearchList.Name = "dgvSearchList";
            this.dgvSearchList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvSearchList.ShowGridFilter = false;
            this.dgvSearchList.Size = new System.Drawing.Size(895, 562);
            this.dgvSearchList.TabIndex = 27;
            this.dgvSearchList.DoubleClicked += new System.EventHandler(this.dgvSearchList_DoubleClicked);
            // 
            // UclDrugGroupingSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MMMainPanel.Controls.Add(this.dgvSearchList);
            this.Name = "UclDrugGroupingSearch";
            this.Size = new System.Drawing.Size(895, 585);
            this.ResumeLayout(false);

        }

        #endregion

        private MDataGridView dgvSearchList;


    }
}
