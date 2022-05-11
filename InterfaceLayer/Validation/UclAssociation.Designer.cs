namespace EcoMart.InterfaceLayer.Validation
{
    partial class UclAssociation
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAssociation = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssociation)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "License Association Details:";
            // 
            // dgvAssociation
            // 
            this.dgvAssociation.AllowUserToAddRows = false;
            this.dgvAssociation.AllowUserToDeleteRows = false;
            this.dgvAssociation.AllowUserToResizeColumns = false;
            this.dgvAssociation.AllowUserToResizeRows = false;
            this.dgvAssociation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssociation.Location = new System.Drawing.Point(17, 49);
            this.dgvAssociation.MultiSelect = false;
            this.dgvAssociation.Name = "dgvAssociation";
            this.dgvAssociation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAssociation.Size = new System.Drawing.Size(373, 157);
            this.dgvAssociation.TabIndex = 1056;
            this.dgvAssociation.DoubleClick += new System.EventHandler(this.dgvAssociation_DoubleClick);            
            // 
            // UclAssociation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvAssociation);
            this.Controls.Add(this.label1);
            this.Name = "UclAssociation";
            this.Size = new System.Drawing.Size(407, 232);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssociation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAssociation;
    }
}
