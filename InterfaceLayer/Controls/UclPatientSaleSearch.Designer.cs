namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    partial class UclPatientSaleSearch
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
      //  private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclPatientSaleSearch));
            this.lblHeaderCaption = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dgvSearchList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.MMDatePanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabelForOverView1
            // 
            this.headerLabelForOverView1.Size = new System.Drawing.Size(755, 23);
            // 
            // MMDatePanel
            // 
            this.MMDatePanel.Size = new System.Drawing.Size(755, 30);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.dgvSearchList);
            this.MMMainPanel.Size = new System.Drawing.Size(755, 400);
            // 
            // lblHeaderCaption
            // 
            this.lblHeaderCaption.AutoSize = true;
            this.lblHeaderCaption.BackColor = System.Drawing.Color.MediumBlue;
            this.lblHeaderCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.White;
            this.lblHeaderCaption.Location = new System.Drawing.Point(3, 3);
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            this.lblHeaderCaption.Size = new System.Drawing.Size(167, 17);
            this.lblHeaderCaption.TabIndex = 29;
            this.lblHeaderCaption.Text = "Patient Sale Overview";
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.MediumBlue;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(679, 23);
            this.lblHeader.TabIndex = 27;
            this.lblHeader.Text = " ";
            // 
            // dgvSearchList
            // 
            this.dgvSearchList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvSearchList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSearchList.DateColumnNames")));
            this.dgvSearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvSearchList.DoubleColumnNames")));
            this.dgvSearchList.Filter = null;
            this.dgvSearchList.Location = new System.Drawing.Point(0, 0);
            this.dgvSearchList.Name = "dgvSearchList";
            this.dgvSearchList.ShowGridFilter = false;
            this.dgvSearchList.Size = new System.Drawing.Size(755, 400);
            this.dgvSearchList.TabIndex = 33;
            this.dgvSearchList.DoubleClicked += new System.EventHandler(this.dgvSearchList_DoubleClicked);
            // 
            // UclPatientSaleSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "UclPatientSaleSearch";
            this.Size = new System.Drawing.Size(755, 455);
            this.MMDatePanel.ResumeLayout(false);
            this.MMDatePanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeaderCaption;
        private System.Windows.Forms.Label lblHeader;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvSearchList;
    }
}
