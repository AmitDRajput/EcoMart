namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclStockOutSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStockOutSearch));
            this.label1 = new System.Windows.Forms.Label();
            this.dgvSearchList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabelForOverView1
            // 
            this.headerLabelForOverView1.Size = new System.Drawing.Size(891, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.MMMainPanel.Controls.Add(this.dgvSearchList);
            this.MMMainPanel.Controls.Add(this.label1);
            this.MMMainPanel.Size = new System.Drawing.Size(891, 555);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.MediumBlue;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, -56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 18);
            this.label1.TabIndex = 34;
            this.label1.Text = "DebitNote Stock Overview";
            // 
            // dgvSearchList
            // 
            this.dgvSearchList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvSearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchList.Filter = null;
            this.dgvSearchList.Location = new System.Drawing.Point(0, 0);
            this.dgvSearchList.Name = "dgvSearchList";
            this.dgvSearchList.ShowGridFilter = false;
            this.dgvSearchList.Size = new System.Drawing.Size(891, 555);
            this.dgvSearchList.TabIndex = 44;
            this.dgvSearchList.DoubleClicked += new System.EventHandler(this.dgvSearchList_DoubleClicked);
            // 
            // UclStockOutSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Name = "UclStockOutSearch";
            this.Size = new System.Drawing.Size(891, 580);
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvSearchList;
    }
}
