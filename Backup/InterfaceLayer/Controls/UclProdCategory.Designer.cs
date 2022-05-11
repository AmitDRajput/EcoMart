namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclProdCategory
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
            this.ttProductCategory = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.mPlbl14 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtSaleDiscount = new System.Windows.Forms.TextBox();
            this.lblName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.psLabel1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(868, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 452);
            this.MMBottomPanel.Size = new System.Drawing.Size(870, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel1);
            this.MMMainPanel.Size = new System.Drawing.Size(870, 400);
            this.MMMainPanel.TabIndex = 0;
            this.MMMainPanel.Controls.SetChildIndex(this.panel1, 0);
            // 
            // ttProductCategory
            // 
            this.ttProductCategory.AutomaticDelay = 20;
            this.ttProductCategory.AutoPopDelay = 5000;
            this.ttProductCategory.InitialDelay = 20;
            this.ttProductCategory.ReshowDelay = 4;
            this.ttProductCategory.ShowAlways = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.psLabel1);
            this.panel1.Controls.Add(this.mPlbl14);
            this.panel1.Controls.Add(this.txtSaleDiscount);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Location = new System.Drawing.Point(115, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 180);
            this.panel1.TabIndex = 1025;
            // 
            // mPlbl14
            // 
            this.mPlbl14.AutoSize = true;
            this.mPlbl14.Location = new System.Drawing.Point(24, 110);
            this.mPlbl14.Name = "mPlbl14";
            this.mPlbl14.Size = new System.Drawing.Size(109, 19);
            this.mPlbl14.TabIndex = 40;
            this.mPlbl14.Text = "Sale Discount";
            // 
            // txtSaleDiscount
            // 
            this.txtSaleDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSaleDiscount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaleDiscount.Location = new System.Drawing.Point(136, 108);
            this.txtSaleDiscount.Name = "txtSaleDiscount";
            this.txtSaleDiscount.Size = new System.Drawing.Size(28, 21);
            this.txtSaleDiscount.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(56, 61);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(73, 19);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Category";
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.ColumnWidth = null;
            this.txtName.DataSource = null;
            this.txtName.DisplayColumnNo = 1;
            this.txtName.DropDownHeight = 200;
            this.txtName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(132, 61);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtName.Name = "txtName";
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(367, 26);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 0;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            this.txtName.EnterKeyPressed += new System.EventHandler(this.txtName_EnterKeyPressed);
            this.txtName.SeletectIndexChanged += new System.EventHandler(this.txtName_SeletectIndexChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(15, 2);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(2, 16);
            this.lblMessage.TabIndex = 1010;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(170, 111);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(38, 19);
            this.psLabel1.TabIndex = 41;
            this.psLabel1.Text = "Y/N";
            // 
            // UclProdCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Name = "UclProdCategory";
            this.Size = new System.Drawing.Size(870, 475);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttProductCategory;
        private System.Windows.Forms.Panel panel1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtName;
        private System.Windows.Forms.Label lblMessage;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblName;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl14;
        private System.Windows.Forms.TextBox txtSaleDiscount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel1;
    }
}
