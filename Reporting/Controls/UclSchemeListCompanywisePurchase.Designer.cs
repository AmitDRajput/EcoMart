namespace EcoMart.Reporting.Controls
{
    partial class UclSchemeListCompanywisePurchase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclSchemeListCompanywisePurchase));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblProduct = new System.Windows.Forms.Label();
            this.mcbProduct = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.btnOK = new System.Windows.Forms.Button();
            this.mcbCompany = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
           
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(980, 23);
            // 
            // MMBottomPanel
            // 
           
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 530);
            this.MMBottomPanel.Size = new System.Drawing.Size(982, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel2);
            this.MMMainPanel.Controls.Add(this.panel1);
            this.MMMainPanel.Size = new System.Drawing.Size(982, 478);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblProduct);
            this.panel1.Controls.Add(this.mcbProduct);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.mcbCompany);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 54);
            this.panel1.TabIndex = 1;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(459, 29);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(55, 15);
            this.lblProduct.TabIndex = 1037;
            this.lblProduct.Text = "Product";
            // 
            // mcbProduct
            // 
            this.mcbProduct.ColumnWidth = null;
            this.mcbProduct.DataSource = null;
            this.mcbProduct.DisplayColumnNo = 1;
            this.mcbProduct.DropDownHeight = 200;
            this.mcbProduct.Location = new System.Drawing.Point(528, 26);
            this.mcbProduct.Name = "mcbProduct";
            this.mcbProduct.SelectedID = null;
            this.mcbProduct.ShowNew = false;
            this.mcbProduct.Size = new System.Drawing.Size(359, 22);
            this.mcbProduct.SourceDataString = null;
            this.mcbProduct.TabIndex = 1036;
            this.mcbProduct.UserControlToShow = null;
            this.mcbProduct.ValueColumnNo = 0;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Lime;
            this.btnOK.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(925, 23);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(35, 30);
            this.btnOK.TabIndex = 1035;
            this.btnOK.Text = "GO";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // mcbCompany
            // 
            this.mcbCompany.ColumnWidth = null;
            this.mcbCompany.DataSource = null;
            this.mcbCompany.DisplayColumnNo = 1;
            this.mcbCompany.DropDownHeight = 200;
            this.mcbCompany.Location = new System.Drawing.Point(77, 27);
            this.mcbCompany.Name = "mcbCompany";
            this.mcbCompany.SelectedID = null;
            this.mcbCompany.ShowNew = false;
            this.mcbCompany.Size = new System.Drawing.Size(359, 22);
            this.mcbCompany.SourceDataString = null;
            this.mcbCompany.TabIndex = 1033;
            this.mcbCompany.UserControlToShow = null;
            this.mcbCompany.ValueColumnNo = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 1032;
            this.label1.Text = "Company";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvReportList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(980, 422);
            this.panel2.TabIndex = 2;
            // 
            // dgvReportList
            // 
            this.dgvReportList.BackColor = System.Drawing.Color.Khaki;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;          
            this.dgvReportList.Location = new System.Drawing.Point(0, 0);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.RightToLeft = System.Windows.Forms.RightToLeft.No;           
            this.dgvReportList.Size = new System.Drawing.Size(980, 422);
            this.dgvReportList.TabIndex = 1031;
            this.dgvReportList.DoubleClicked += new System.EventHandler(this.dgvReportList_DoubleClicked);
           
            // 
            // UclSchemeListCompanywisePurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Name = "UclSchemeListCompanywisePurchase";
            this.Size = new System.Drawing.Size(982, 553);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbCompany;
        private System.Windows.Forms.Label label1;        
        private System.Windows.Forms.Panel panel2;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;        
        private System.Windows.Forms.Label lblProduct;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbProduct;
       
    }
}
