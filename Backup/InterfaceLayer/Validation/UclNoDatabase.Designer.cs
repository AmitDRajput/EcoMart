namespace PharmaSYSRetailPlus.InterfaceLayer.Validation
{
    partial class UclNoDatabase
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnConnectDatabase = new System.Windows.Forms.RadioButton();
            this.rbtnCreateDatbase = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database connection not found.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnCreateDatbase);
            this.groupBox1.Controls.Add(this.rbtnConnectDatabase);
            this.groupBox1.Location = new System.Drawing.Point(20, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 90);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "How would you like to proceed further?";
            // 
            // rbtnConnectDatabase
            // 
            this.rbtnConnectDatabase.AutoSize = true;
            this.rbtnConnectDatabase.Checked = true;
            this.rbtnConnectDatabase.Location = new System.Drawing.Point(7, 24);
            this.rbtnConnectDatabase.Name = "rbtnConnectDatabase";
            this.rbtnConnectDatabase.Size = new System.Drawing.Size(177, 17);
            this.rbtnConnectDatabase.TabIndex = 0;
            this.rbtnConnectDatabase.TabStop = true;
            this.rbtnConnectDatabase.Text = "Connect to an existing database";
            this.rbtnConnectDatabase.UseVisualStyleBackColor = true;
            this.rbtnConnectDatabase.CheckedChanged += new System.EventHandler(this.rbtnConnectDatabase_CheckedChanged);
            // 
            // rbtnCreateDatbase
            // 
            this.rbtnCreateDatbase.AutoSize = true;
            this.rbtnCreateDatbase.Location = new System.Drawing.Point(7, 54);
            this.rbtnCreateDatbase.Name = "rbtnCreateDatbase";
            this.rbtnCreateDatbase.Size = new System.Drawing.Size(126, 17);
            this.rbtnCreateDatbase.TabIndex = 1;
            this.rbtnCreateDatbase.Text = "Create new database";
            this.rbtnCreateDatbase.UseVisualStyleBackColor = true;
            this.rbtnCreateDatbase.CheckedChanged += new System.EventHandler(this.rbtnCreateDatbase_CheckedChanged);
            // 
            // UclNoDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "UclNoDatabase";
            this.Size = new System.Drawing.Size(341, 159);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnCreateDatbase;
        private System.Windows.Forms.RadioButton rbtnConnectDatabase;
    }
}
