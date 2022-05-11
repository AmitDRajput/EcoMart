namespace EcoMart.InterfaceLayer.CommonControls
{
    partial class PSNameControl
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
            this.lblstar = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlSub = new System.Windows.Forms.Panel();
            this.pnlSub.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblstar
            // 
            this.lblstar.AutoSize = true;
            this.lblstar.BackColor = System.Drawing.Color.Transparent;
            this.lblstar.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstar.ForeColor = System.Drawing.Color.Red;
            this.lblstar.Location = new System.Drawing.Point(108, 53);
            this.lblstar.Name = "lblstar";
            this.lblstar.Size = new System.Drawing.Size(19, 18);
            this.lblstar.TabIndex = 49;
            this.lblstar.Text = "*";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(65, 54);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 17);
            this.lblName.TabIndex = 50;
            this.lblName.Text = "Name";
            // 
            // pnlSub
            // 
            this.pnlSub.BackColor = System.Drawing.Color.Silver;
            this.pnlSub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSub.Controls.Add(this.lblName);
            this.pnlSub.Controls.Add(this.lblstar);
            this.pnlSub.Location = new System.Drawing.Point(90, 92);
            this.pnlSub.Name = "pnlSub";
            this.pnlSub.Size = new System.Drawing.Size(578, 140);
            this.pnlSub.TabIndex = 49;
            // 
            // MPNameControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlSub);
            this.Name = "MPNameControl";
            this.Size = new System.Drawing.Size(764, 382);
            this.pnlSub.ResumeLayout(false);
            this.pnlSub.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblstar;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlSub;


    }
}
