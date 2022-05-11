namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class FrmLogin
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnlogin = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.txtpasswd = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSTextBox();
            this.txtusrname = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSTextBox();
            this.lblAccountingYear = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblPassword = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.lblUserName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mcbAccountingYear = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.grpMain.SuspendLayout();
            this.grpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnlogin
            // 
            this.btnlogin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnlogin.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogin.Location = new System.Drawing.Point(100, 13);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(75, 30);
            this.btnlogin.TabIndex = 0;
            this.btnlogin.Text = "Login";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // btncancel
            // 
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btncancel.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Location = new System.Drawing.Point(181, 13);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 30);
            this.btncancel.TabIndex = 1;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.txtpasswd);
            this.grpMain.Controls.Add(this.txtusrname);
            this.grpMain.Controls.Add(this.lblAccountingYear);
            this.grpMain.Controls.Add(this.lblPassword);
            this.grpMain.Controls.Add(this.lblUserName);
            this.grpMain.Controls.Add(this.mcbAccountingYear);
            this.grpMain.Location = new System.Drawing.Point(12, 1);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(304, 103);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            // 
            // grpButtons
            // 
            this.grpButtons.Controls.Add(this.btnlogin);
            this.grpButtons.Controls.Add(this.btncancel);
            this.grpButtons.Location = new System.Drawing.Point(12, 107);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(304, 49);
            this.grpButtons.TabIndex = 1;
            this.grpButtons.TabStop = false;
            // 
            // txtpasswd
            // 
            this.txtpasswd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpasswd.Location = new System.Drawing.Point(98, 44);
            this.txtpasswd.Name = "txtpasswd";
            this.txtpasswd.PasswordChar = '*';
            this.txtpasswd.Size = new System.Drawing.Size(181, 23);
            this.txtpasswd.TabIndex = 3;
            this.txtpasswd.Text = "test";
            this.txtpasswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpasswd_KeyDown);
            // 
            // txtusrname
            // 
            this.txtusrname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtusrname.Location = new System.Drawing.Point(98, 15);
            this.txtusrname.Name = "txtusrname";
            this.txtusrname.Size = new System.Drawing.Size(181, 23);
            this.txtusrname.TabIndex = 1;
            this.txtusrname.Text = "admin";
            this.txtusrname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtusrname_KeyDown);
            // 
            // lblAccountingYear
            // 
            this.lblAccountingYear.AutoSize = true;
            this.lblAccountingYear.Location = new System.Drawing.Point(6, 71);
            this.lblAccountingYear.Name = "lblAccountingYear";
            this.lblAccountingYear.Size = new System.Drawing.Size(73, 19);
            this.lblAccountingYear.TabIndex = 4;
            this.lblAccountingYear.Text = "A/c Year";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(6, 44);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(81, 19);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(6, 16);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(88, 19);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "User Name";
            // 
            // mcbAccountingYear
            // 
            this.mcbAccountingYear.ColumnWidth = null;
            this.mcbAccountingYear.DataSource = null;
            this.mcbAccountingYear.DisplayColumnNo = 1;
            this.mcbAccountingYear.DropDownHeight = 200;
            this.mcbAccountingYear.Location = new System.Drawing.Point(98, 71);
            this.mcbAccountingYear.Margin = new System.Windows.Forms.Padding(4);
            this.mcbAccountingYear.Name = "mcbAccountingYear";
            this.mcbAccountingYear.SelectedID = null;
            this.mcbAccountingYear.ShowNew = false;
            this.mcbAccountingYear.Size = new System.Drawing.Size(126, 26);
            this.mcbAccountingYear.SourceDataString = null;
            this.mcbAccountingYear.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbAccountingYear.TabIndex = 5;
            this.mcbAccountingYear.UserControlToShow = null;
            this.mcbAccountingYear.ValueColumnNo = 0;
            this.mcbAccountingYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcbAccountingYear_KeyDown);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(328, 162);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.grpButtons);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.Location = new System.Drawing.Point(100, 300);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PharmaSYS Retail Plus";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.grpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.GroupBox grpButtons;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbAccountingYear;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblUserName;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblAccountingYear;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblPassword;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSTextBox txtusrname;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSTextBox txtpasswd;
        
    }
}