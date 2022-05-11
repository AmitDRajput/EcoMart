namespace EcoMart.InterfaceLayer
{
    partial class UclToolEmilanDownLoadPurchase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclToolEmilanDownLoadPurchase));
            this.button1 = new System.Windows.Forms.Button();
            this.dgBills = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.headerLabel1.Size = new System.Drawing.Size(1033, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 374);
            this.MMBottomPanel.Margin = new System.Windows.Forms.Padding(2);
            this.MMBottomPanel.Size = new System.Drawing.Size(1035, 65);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.button1);
            this.MMMainPanel.Controls.Add(this.dgBills);
            this.MMMainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.MMMainPanel.Size = new System.Drawing.Size(1035, 311);
            this.MMMainPanel.Controls.SetChildIndex(this.dgBills, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.button1, 0);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(403, 16);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 37);
            this.button1.TabIndex = 4;
            this.button1.Text = "DownLoad";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgBills
            // 
            this.dgBills.ApplyAlternateRowStyle = false;
            this.dgBills.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgBills.BackColor = System.Drawing.Color.Transparent;
            this.dgBills.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgBills.ConvertDatetoMonth")));
            this.dgBills.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgBills.DateColumnNames")));
            this.dgBills.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgBills.DoubleColumnNames")));
            this.dgBills.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgBills.FreezeLastRow = false;
            this.dgBills.Location = new System.Drawing.Point(27, 75);
            this.dgBills.Margin = new System.Windows.Forms.Padding(2);
            this.dgBills.Name = "dgBills";
            this.dgBills.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgBills.NumericColumnNames")));
            this.dgBills.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgBills.OptionalColumnNames")));
            this.dgBills.Size = new System.Drawing.Size(972, 188);
            this.dgBills.TabIndex = 3;
            this.dgBills.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBills_KeyDown);
            // 
            // UclToolEmilanDownLoadPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UclToolEmilanDownLoadPurchase";
            this.Size = new System.Drawing.Size(1035, 439);
            this.Load += new System.EventHandler(this.UclToolEmilanDownLoadPurchase_Load);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgBills;
    }
}
