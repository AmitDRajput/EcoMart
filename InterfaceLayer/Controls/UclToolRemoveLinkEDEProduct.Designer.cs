namespace EcoMart.InterfaceLayer
{
    partial class UclToolRemoveLinkEDEProduct
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
            this.txtpasswd = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.btnStart = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.psLabel9 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbProduct = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mcbCreditor = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.psLabel7 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLableWithBorderMiddleLeft1 = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(968, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 507);
            this.MMBottomPanel.Size = new System.Drawing.Size(970, 65);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.psLableWithBorderMiddleLeft1);
            this.MMMainPanel.Controls.Add(this.psLabel9);
            this.MMMainPanel.Controls.Add(this.mcbProduct);
            this.MMMainPanel.Controls.Add(this.mcbCreditor);
            this.MMMainPanel.Controls.Add(this.psLabel7);
            this.MMMainPanel.Controls.Add(this.txtpasswd);
            this.MMMainPanel.Controls.Add(this.btnStart);
            this.MMMainPanel.Size = new System.Drawing.Size(970, 444);
            this.MMMainPanel.Controls.SetChildIndex(this.btnStart, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtpasswd, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.psLabel7, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mcbCreditor, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mcbProduct, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.psLabel9, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.psLableWithBorderMiddleLeft1, 0);
            // 
            // txtpasswd
            // 
            this.txtpasswd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpasswd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtpasswd.IsNumericDataSet = false;
            this.txtpasswd.Location = new System.Drawing.Point(374, 215);
            this.txtpasswd.Name = "txtpasswd";
            this.txtpasswd.PasswordChar = '*';
            this.txtpasswd.Size = new System.Drawing.Size(181, 22);
            this.txtpasswd.TabIndex = 10;
            this.txtpasswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpasswd_KeyDown);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(419, 257);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(93, 45);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "Remove";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            this.btnStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnStart_KeyDown);
            // 
            // psLabel9
            // 
            this.psLabel9.AutoSize = true;
            this.psLabel9.Location = new System.Drawing.Point(193, 171);
            this.psLabel9.Name = "psLabel9";
            this.psLabel9.Size = new System.Drawing.Size(56, 16);
            this.psLabel9.TabIndex = 1086;
            this.psLabel9.Text = "Product";
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(271, 167);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = null;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(432, 22);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 1087;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            this.mcbProduct.EnterKeyPressed += new System.EventHandler(this.mcbProduct_EnterKeyPressed);
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(271, 138);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = null;
            this.mcbCreditor.ShowNew = false;
            this.mcbCreditor.Size = new System.Drawing.Size(432, 22);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 1084;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            // 
            // psLabel7
            // 
            this.psLabel7.AutoSize = true;
            this.psLabel7.Location = new System.Drawing.Point(213, 140);
            this.psLabel7.Name = "psLabel7";
            this.psLabel7.Size = new System.Drawing.Size(41, 16);
            this.psLabel7.TabIndex = 1085;
            this.psLabel7.Text = "Party";
            // 
            // psLableWithBorderMiddleLeft1
            // 
            this.psLableWithBorderMiddleLeft1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.psLableWithBorderMiddleLeft1.Location = new System.Drawing.Point(260, 75);
            this.psLableWithBorderMiddleLeft1.Name = "psLableWithBorderMiddleLeft1";
            this.psLableWithBorderMiddleLeft1.Size = new System.Drawing.Size(413, 49);
            this.psLableWithBorderMiddleLeft1.TabIndex = 1088;
            this.psLableWithBorderMiddleLeft1.Text = "This Procedure is to Remove Link  of the Product From NET Purchase\r\n";
            this.psLableWithBorderMiddleLeft1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UclToolRemoveLinkEDEProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclToolRemoveLinkEDEProduct";
            this.Size = new System.Drawing.Size(970, 572);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CommonControls.PSTextBox txtpasswd;
        private PharmaSYSPlus.CommonLibrary.PSButton btnStart;
        private CommonControls.PSLabel psLabel9;
        private CommonControls.PSComboBoxNew mcbProduct;
        private CommonControls.PSComboBoxNew mcbCreditor;
        private CommonControls.PSLabel psLabel7;
        private CommonControls.PSLableWithBorderMiddleLeft psLableWithBorderMiddleLeft1;
    }
}
