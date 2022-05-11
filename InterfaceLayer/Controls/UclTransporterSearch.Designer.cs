namespace EcoMart.InterfaceLayer
{
    partial class UclTransporterSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclTransporterSearch));
            this.dgvSearchList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.MMDatePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabelForOverView1
            // 
            this.headerLabelForOverView1.Size = new System.Drawing.Size(763, 23);
            // 
            // MMDatePanel
            // 
            this.MMDatePanel.Size = new System.Drawing.Size(763, 30);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Size = new System.Drawing.Size(763, 433);
            // 
            // dgvSearchList
            // 
            this.dgvSearchList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvSearchList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSearchList.DateColumnNames")));
            this.dgvSearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSearchList.DoubleColumnNames")));
            this.dgvSearchList.Filter = null;
            this.dgvSearchList.Location = new System.Drawing.Point(0, 55);
            this.dgvSearchList.Name = "dgvSearchList";
            this.dgvSearchList.ShowGridFilter = false;
            this.dgvSearchList.Size = new System.Drawing.Size(763, 433);
            this.dgvSearchList.TabIndex = 27;
            this.dgvSearchList.DoubleClicked += new System.EventHandler(this.dgvSearchList_DoubleClicked_1);
            // 
            // UclTransporterSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvSearchList);
            this.Name = "UclTransporterSearch";
            this.Size = new System.Drawing.Size(763, 488);
            this.Controls.SetChildIndex(this.MMDatePanel, 0);
            this.Controls.SetChildIndex(this.MMMainPanel, 0);
            this.Controls.SetChildIndex(this.dgvSearchList, 0);
            this.MMDatePanel.ResumeLayout(false);
            this.MMDatePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvSearchList;
    }
}
