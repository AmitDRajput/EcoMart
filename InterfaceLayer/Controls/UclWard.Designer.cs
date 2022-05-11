namespace EcoMart.InterfaceLayer
{
    partial class UclWard
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
            this.components = new System.ComponentModel.Container();
            this.ttWard = new System.Windows.Forms.ToolTip(this.components);
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtName = new EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.lblName = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.MMMainPanel.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(904, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 477);
            this.MMBottomPanel.Size = new System.Drawing.Size(906, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlCenter);
            this.MMMainPanel.Size = new System.Drawing.Size(906, 425);
            // 
            // ttWard
            // 
            this.ttWard.AutomaticDelay = 20;
            this.ttWard.AutoPopDelay = 5000;
            this.ttWard.InitialDelay = 20;
            this.ttWard.ReshowDelay = 4;
            this.ttWard.ShowAlways = true;
            // 
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCenter.Controls.Add(this.panel1);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 0);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(904, 423);
            this.pnlCenter.TabIndex = 1011;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Location = new System.Drawing.Point(200, 138);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 148);
            this.panel1.TabIndex = 1026;
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.ColumnWidth = null;
            this.txtName.DataSource = null;
            this.txtName.DisplayColumnNo = 1;
            this.txtName.DropDownHeight = 200;
            this.txtName.Location = new System.Drawing.Point(100, 60);
            this.txtName.Name = "txtName";
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(400, 22);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 0;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            this.txtName.SeletectIndexChanged += new System.EventHandler(this.txtName_SeletectIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(48, 62);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(46, 17);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Ward";
            // 
            // UclWard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Name = "UclWard";
            this.Size = new System.Drawing.Size(906, 500);
            this.MMMainPanel.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttWard;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Panel panel1;
        private EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtName;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblName;
    }
}
