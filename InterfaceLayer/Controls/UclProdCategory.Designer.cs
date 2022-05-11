namespace EcoMart.InterfaceLayer
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
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.psLabel4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtDoctorRequired = new System.Windows.Forms.TextBox();
            this.txtLBTPercent = new PharmaSYSPlus.CommonLibrary.DecimalTextBox();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl14 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtSaleDiscount = new System.Windows.Forms.TextBox();
            this.lblName = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtName = new EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(868, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 412);
            this.MMBottomPanel.Size = new System.Drawing.Size(870, 63);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblRightSideFooterMsg, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel1);
            this.MMMainPanel.Size = new System.Drawing.Size(870, 349);
            this.MMMainPanel.TabIndex = 0;
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.panel1, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(402, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 20);
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
            this.panel1.Controls.Add(this.psLabel3);
            this.panel1.Controls.Add(this.psLabel4);
            this.panel1.Controls.Add(this.txtDoctorRequired);
            this.panel1.Controls.Add(this.txtLBTPercent);
            this.panel1.Controls.Add(this.psLabel1);
            this.panel1.Controls.Add(this.mPlbl14);
            this.panel1.Controls.Add(this.txtSaleDiscount);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Location = new System.Drawing.Point(115, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 207);
            this.panel1.TabIndex = 1025;
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(170, 161);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(33, 16);
            this.psLabel3.TabIndex = 46;
            this.psLabel3.Text = "Y/N";
            this.psLabel3.Visible = false;
            // 
            // psLabel4
            // 
            this.psLabel4.AutoSize = true;
            this.psLabel4.Location = new System.Drawing.Point(7, 161);
            this.psLabel4.Name = "psLabel4";
            this.psLabel4.Size = new System.Drawing.Size(108, 16);
            this.psLabel4.TabIndex = 45;
            this.psLabel4.Text = "Doctor Required";
            this.psLabel4.Visible = false;
            // 
            // txtDoctorRequired
            // 
            this.txtDoctorRequired.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDoctorRequired.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoctorRequired.Location = new System.Drawing.Point(136, 159);
            this.txtDoctorRequired.Name = "txtDoctorRequired";
            this.txtDoctorRequired.Size = new System.Drawing.Size(28, 21);
            this.txtDoctorRequired.TabIndex = 44;
            this.txtDoctorRequired.Visible = false;
            this.txtDoctorRequired.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDoctorRequired_KeyDown);
            // 
            // txtLBTPercent
            // 
            this.txtLBTPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLBTPercent.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLBTPercent.Location = new System.Drawing.Point(136, 112);
            this.txtLBTPercent.MaxLength = 15;
            this.txtLBTPercent.Name = "txtLBTPercent";
            this.txtLBTPercent.Size = new System.Drawing.Size(86, 23);
            this.txtLBTPercent.TabIndex = 43;
            this.txtLBTPercent.Tag = "0.00";
            this.txtLBTPercent.Text = "0.00";
            this.txtLBTPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLBTPercent.Visible = false;
            this.txtLBTPercent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLBTPercent_KeyDown);
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(170, 72);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(33, 16);
            this.psLabel1.TabIndex = 41;
            this.psLabel1.Text = "Y/N";
            this.psLabel1.Visible = false;
            // 
            // mPlbl14
            // 
            this.mPlbl14.AutoSize = true;
            this.mPlbl14.Location = new System.Drawing.Point(26, 72);
            this.mPlbl14.Name = "mPlbl14";
            this.mPlbl14.Size = new System.Drawing.Size(89, 16);
            this.mPlbl14.TabIndex = 40;
            this.mPlbl14.Text = "Sale Discount";
            this.mPlbl14.Visible = false;
            // 
            // txtSaleDiscount
            // 
            this.txtSaleDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSaleDiscount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaleDiscount.Location = new System.Drawing.Point(136, 70);
            this.txtSaleDiscount.Name = "txtSaleDiscount";
            this.txtSaleDiscount.Size = new System.Drawing.Size(28, 21);
            this.txtSaleDiscount.TabIndex = 1;
            this.txtSaleDiscount.Text = "N";
            this.txtSaleDiscount.Visible = false;
            this.txtSaleDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSaleDiscount_KeyDown);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(53, 30);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(62, 16);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Category";
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
            this.txtName.Location = new System.Drawing.Point(136, 27);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = false;
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(367, 22);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 0;
            this.txtName.TextMaxLenght = 32767;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            this.txtName.SeletectIndexChanged += new System.EventHandler(this.txtName_SeletectIndexChanged);
            this.txtName.EnterKeyPressed += new System.EventHandler(this.txtName_EnterKeyPressed);
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
            this.MMMainPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttProductCategory;
        private System.Windows.Forms.Panel panel1;
        private EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtName;
        private System.Windows.Forms.Label lblMessage;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblName;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl14;
        private System.Windows.Forms.TextBox txtSaleDiscount;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSPlus.CommonLibrary.DecimalTextBox txtLBTPercent;
        private CommonControls.PSLabel psLabel3;
        private CommonControls.PSLabel psLabel4;
        private System.Windows.Forms.TextBox txtDoctorRequired;
    }
}
