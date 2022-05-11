namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclGroup
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
            this.ttGroup = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panmaster = new System.Windows.Forms.Panel();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.mcbUnderGroup = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panmaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(769, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 391);
            this.MMBottomPanel.Size = new System.Drawing.Size(771, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel1);
            this.MMMainPanel.Size = new System.Drawing.Size(771, 339);
            this.MMMainPanel.Controls.SetChildIndex(this.panel1, 0);
            // 
            // ttGroup
            // 
            this.ttGroup.AutomaticDelay = 20;
            this.ttGroup.AutoPopDelay = 5000;
            this.ttGroup.InitialDelay = 20;
            this.ttGroup.ReshowDelay = 4;
            this.ttGroup.ShowAlways = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panmaster);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 337);
            this.panel1.TabIndex = 0;
            // 
            // panmaster
            // 
            this.panmaster.BackColor = System.Drawing.Color.LightGray;
            this.panmaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panmaster.Controls.Add(this.mPlbl1);
            this.panmaster.Controls.Add(this.lblName);
            this.panmaster.Controls.Add(this.txtName);
            this.panmaster.Controls.Add(this.mcbUnderGroup);
            this.panmaster.Controls.Add(this.label4);
            this.panmaster.Controls.Add(this.label6);
            this.panmaster.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panmaster.Location = new System.Drawing.Point(115, 100);
            this.panmaster.Name = "panmaster";
            this.panmaster.Size = new System.Drawing.Size(558, 150);
            this.panmaster.TabIndex = 1;
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(13, 82);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(105, 19);
            this.mPlbl1.TabIndex = 48;
            this.mPlbl1.Text = "Under Group";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(64, 45);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(51, 19);
            this.lblName.TabIndex = 47;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.ColumnWidth = null;
            this.txtName.DataSource = null;
            this.txtName.DisplayColumnNo = 1;
            this.txtName.DropDownHeight = 200;
            this.txtName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(134, 42);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtName.Name = "txtName";
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(330, 26);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 0;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            this.txtName.EnterKeyPressed += new System.EventHandler(this.txtName_EnterKeyPressed);
            // 
            // mcbUnderGroup
            // 
            this.mcbUnderGroup.ColumnWidth = null;
            this.mcbUnderGroup.DataSource = null;
            this.mcbUnderGroup.DisplayColumnNo = 1;
            this.mcbUnderGroup.DropDownHeight = 200;
            this.mcbUnderGroup.Location = new System.Drawing.Point(134, 81);
            this.mcbUnderGroup.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbUnderGroup.Name = "mcbUnderGroup";
            this.mcbUnderGroup.SelectedID = null;
            this.mcbUnderGroup.ShowNew = false;
            this.mcbUnderGroup.Size = new System.Drawing.Size(389, 26);
            this.mcbUnderGroup.SourceDataString = null;
            this.mcbUnderGroup.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbUnderGroup.TabIndex = 1;
            this.mcbUnderGroup.UserControlToShow = null;
            this.mcbUnderGroup.ValueColumnNo = 0;
            this.mcbUnderGroup.EnterKeyPressed += new System.EventHandler(this.mcbUnderGroup_EnterKeyPressed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(113, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(111, 42);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "*";
            // 
            // UclGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "UclGroup";
            this.Size = new System.Drawing.Size(771, 414);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panmaster.ResumeLayout(false);
            this.panmaster.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttGroup;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panmaster;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbUnderGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtName;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblName;
    }
}
