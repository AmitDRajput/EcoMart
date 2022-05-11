namespace EcoMart.InterfaceLayer
{
    partial class UclEmilanDownloadPurchase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclEmilanDownloadPurchase));
            this.mReportGridView1 = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.btnDownLoad = new System.Windows.Forms.Button();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(2420, 41);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 907);
            this.MMBottomPanel.Size = new System.Drawing.Size(2422, 118);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.btnDownLoad);
            this.MMMainPanel.Controls.Add(this.mReportGridView1);
            this.MMMainPanel.Size = new System.Drawing.Size(2422, 827);
            this.MMMainPanel.Controls.SetChildIndex(this.mReportGridView1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.btnDownLoad, 0);
            // 
            // mReportGridView1
            // 
            this.mReportGridView1.ApplyAlternateRowStyle = false;
            this.mReportGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.mReportGridView1.BackColor = System.Drawing.Color.Transparent;
            this.mReportGridView1.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("mReportGridView1.ConvertDatetoMonth")));
            this.mReportGridView1.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mReportGridView1.DateColumnNames")));
            this.mReportGridView1.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mReportGridView1.DoubleColumnNames")));
            this.mReportGridView1.FreezeLastRow = false;
            this.mReportGridView1.Location = new System.Drawing.Point(169, 235);
            this.mReportGridView1.Name = "mReportGridView1";
            this.mReportGridView1.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mReportGridView1.NumericColumnNames")));
            this.mReportGridView1.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mReportGridView1.OptionalColumnNames")));
            this.mReportGridView1.Size = new System.Drawing.Size(1779, 347);
            this.mReportGridView1.TabIndex = 1;
            // 
            // btnDownLoad
            // 
            this.btnDownLoad.Location = new System.Drawing.Point(942, 116);
            this.btnDownLoad.Name = "btnDownLoad";
            this.btnDownLoad.Size = new System.Drawing.Size(182, 69);
            this.btnDownLoad.TabIndex = 2;
            this.btnDownLoad.Text = "button1";
            this.btnDownLoad.UseVisualStyleBackColor = true;
            this.btnDownLoad.Click += new System.EventHandler(this.btnDownLoad_Click);
            // 
            // UclEmilanDownloadPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclEmilanDownloadPurchase";
            this.Size = new System.Drawing.Size(2422, 1025);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PharmaSYSPlus.CommonLibrary.MReportGridView mReportGridView1;
        private System.Windows.Forms.Button btnDownLoad;
    }
}
