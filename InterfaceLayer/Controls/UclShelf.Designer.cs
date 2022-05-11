namespace EcoMart.InterfaceLayer
{
    partial class UclShelf
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
            this.ttShelf = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtShelfDescription = new System.Windows.Forms.TextBox();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtName = new EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(968, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 614);
            this.MMBottomPanel.Size = new System.Drawing.Size(970, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel1);
            this.MMMainPanel.Size = new System.Drawing.Size(970, 551);
            this.MMMainPanel.TabIndex = 0;
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.panel1, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(502, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 20);
            // 
            // ttShelf
            // 
            this.ttShelf.AutomaticDelay = 20;
            this.ttShelf.AutoPopDelay = 5000;
            this.ttShelf.InitialDelay = 20;
            this.ttShelf.ReshowDelay = 4;
            this.ttShelf.ShowAlways = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mPlbl2);
            this.panel1.Controls.Add(this.txtShelfDescription);
            this.panel1.Controls.Add(this.mPlbl1);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(191, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 150);
            this.panel1.TabIndex = 32;
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(19, 85);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(78, 16);
            this.mPlbl2.TabIndex = 89;
            this.mPlbl2.Text = "&Description";
            // 
            // txtShelfDescription
            // 
            this.txtShelfDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShelfDescription.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShelfDescription.Location = new System.Drawing.Point(127, 86);
            this.txtShelfDescription.MaxLength = 50;
            this.txtShelfDescription.Name = "txtShelfDescription";
            this.txtShelfDescription.Size = new System.Drawing.Size(378, 22);
            this.txtShelfDescription.TabIndex = 1;
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(69, 33);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(38, 16);
            this.mPlbl1.TabIndex = 88;
            this.mPlbl1.Text = "&Code";
            // 
            // txtName
            // 
            this.txtName.AlphabeticalList = false;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.ColumnWidth = null;
            this.txtName.DataSource = null;
            this.txtName.DisplayColumnNo = 1;
            this.txtName.DropDownHeight = 200;
            this.txtName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(127, 30);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = false;
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(125, 22);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 0;
            this.txtName.TextMaxLenght = 8;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            this.txtName.EnterKeyPressed += new System.EventHandler(this.txtName_EnterKeyPressed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(119, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 18);
            this.label1.TabIndex = 35;
            this.label1.Text = "*";
            // 
            // UclShelf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Name = "UclShelf";
            this.Size = new System.Drawing.Size(970, 677);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttShelf;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShelfDescription;
        private EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtName;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
    }
}
