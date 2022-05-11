namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class SearchControl
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
            this.MMTopPanel = new System.Windows.Forms.Panel();
            this.MMMainPanel = new System.Windows.Forms.Panel();
            this.headerLabelForOverView1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSHeaderLabelForOverView();
            this.MMTopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MMTopPanel
            // 
            this.MMTopPanel.Controls.Add(this.headerLabelForOverView1);
            this.MMTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MMTopPanel.Location = new System.Drawing.Point(0, 0);
            this.MMTopPanel.Name = "MMTopPanel";
            this.MMTopPanel.Size = new System.Drawing.Size(549, 25);
            this.MMTopPanel.TabIndex = 24;
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MMMainPanel.Location = new System.Drawing.Point(0, 25);
            this.MMMainPanel.Name = "MMMainPanel";
            this.MMMainPanel.Size = new System.Drawing.Size(549, 209);
            this.MMMainPanel.TabIndex = 0;
            // 
            // headerLabelForOverView1
            // 
            this.headerLabelForOverView1.BackColor = System.Drawing.Color.MediumVioletRed;
            this.headerLabelForOverView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.headerLabelForOverView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerLabelForOverView1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabelForOverView1.ForeColor = System.Drawing.Color.White;
            this.headerLabelForOverView1.Location = new System.Drawing.Point(0, 0);
            this.headerLabelForOverView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.headerLabelForOverView1.Name = "headerLabelForOverView1";
            this.headerLabelForOverView1.Size = new System.Drawing.Size(549, 23);
            this.headerLabelForOverView1.TabIndex = 0;
            this.headerLabelForOverView1.ExitClicked += new System.EventHandler(this.headerLabelForOverView1_ExitClicked);
            // 
            // SearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.MMMainPanel);
            this.Controls.Add(this.MMTopPanel);
            this.Name = "SearchControl";
            this.Size = new System.Drawing.Size(549, 234);
            this.MMTopPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSHeaderLabelForOverView headerLabelForOverView1;
        private System.Windows.Forms.Panel MMTopPanel;
        public System.Windows.Forms.Panel MMMainPanel;

    }
}
