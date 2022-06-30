namespace EcoMart.InterfaceLayer.CommonControls
{
    partial class UclPurchaseNewProduct
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
            this.btnAddNewProduct = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.mcbProduct = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtQuantity = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.mcbCreditor = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtSchemeQuantity = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblProduct = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.lblParty = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.SuspendLayout();
            // 
            // btnAddNewProduct
            // 
            this.btnAddNewProduct.BackColor = System.Drawing.Color.LawnGreen;
            this.btnAddNewProduct.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddNewProduct.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddNewProduct.Location = new System.Drawing.Point(762, 0);
            this.btnAddNewProduct.Name = "btnAddNewProduct";
            this.btnAddNewProduct.Size = new System.Drawing.Size(60, 25);
            this.btnAddNewProduct.TabIndex = 5;
            this.btnAddNewProduct.Text = "ADD";
            this.btnAddNewProduct.UseVisualStyleBackColor = false;
            this.btnAddNewProduct.Click += new System.EventHandler(this.btnAddNewProduct_Click);
            this.btnAddNewProduct.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnAddNewProduct_KeyUp);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(600, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 25);
            this.button1.TabIndex = 5;
            this.button1.Text = "Qty";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.Dock = System.Windows.Forms.DockStyle.Left;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(56, 0);
            this.mcbProduct.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = "";
            this.mcbProduct.SelectedIDtest = 0;
            this.mcbProduct.SelectedIntID = 0;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(469, 22);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbProduct.TabIndex = 1;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            this.mcbProduct.EnterKeyPressed += new System.EventHandler(this.mcbProduct_EnterKeyPressed);
            this.mcbProduct.UpArrowPressed += new System.EventHandler(this.mcbProduct_UpArrowPressed);
            // 
            // txtQuantity
            // 
            this.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtQuantity.Location = new System.Drawing.Point(636, 0);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(66, 22);
            this.txtQuantity.TabIndex = 3;
            this.txtQuantity.Text = "0";
            this.txtQuantity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyUp);
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(566, 0);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = "";
            this.mcbCreditor.SelectedIDtest = 0;
            this.mcbCreditor.SelectedIntID = 0;
            this.mcbCreditor.ShowNew = false;
            this.mcbCreditor.Size = new System.Drawing.Size(34, 22);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 2;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.Visible = false;
            this.mcbCreditor.EnterKeyPressed += new System.EventHandler(this.mcbCreditor_EnterKeyPressed);
            this.mcbCreditor.UpArrowPressed += new System.EventHandler(this.mcbCreditor_UpArrowPressed);
            // 
            // txtSchemeQuantity
            // 
            this.txtSchemeQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSchemeQuantity.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtSchemeQuantity.Location = new System.Drawing.Point(702, 0);
            this.txtSchemeQuantity.Name = "txtSchemeQuantity";
            this.txtSchemeQuantity.Size = new System.Drawing.Size(60, 22);
            this.txtSchemeQuantity.TabIndex = 4;
            this.txtSchemeQuantity.Text = "0";
            this.txtSchemeQuantity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSchemeQuantity_KeyUp);
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblProduct.Location = new System.Drawing.Point(0, 0);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(56, 16);
            this.lblProduct.TabIndex = 6;
            this.lblProduct.Text = "Product";
            // 
            // lblParty
            // 
            this.lblParty.AutoSize = true;
            this.lblParty.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblParty.Location = new System.Drawing.Point(525, 0);
            this.lblParty.Name = "lblParty";
            this.lblParty.Size = new System.Drawing.Size(41, 16);
            this.lblParty.TabIndex = 7;
            this.lblParty.Text = "Party";
            this.lblParty.Visible = false;
            // 
            // UclPurchaseNewProduct
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.mcbCreditor);
            this.Controls.Add(this.lblParty);
            this.Controls.Add(this.mcbProduct);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txtSchemeQuantity);
            this.Controls.Add(this.btnAddNewProduct);
            this.Font = new System.Drawing.Font("Cambria", 8.25F);
            this.Name = "UclPurchaseNewProduct";
            this.Size = new System.Drawing.Size(822, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddNewProduct;
        private CommonControls.PSComboBoxNew mcbCreditor;
        private CommonControls.PSComboBoxNew mcbProduct;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtSchemeQuantity;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtQuantity;
        private System.Windows.Forms.Button button1;
        private PSLabel lblProduct;
        private PSLabel lblParty;
    }
}
