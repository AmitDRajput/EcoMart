namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclCompany
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
            this.ttCompany = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.mPlbl11 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl10 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mcbSecondCreditor = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mcbFirstCreditor = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.mPlbl6 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtShortName = new System.Windows.Forms.TextBox();
            this.txtName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.txtMailId = new System.Windows.Forms.TextBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(886, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 498);
            this.MMBottomPanel.Size = new System.Drawing.Size(888, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel1);
            this.MMMainPanel.Size = new System.Drawing.Size(888, 446);
            this.MMMainPanel.TabIndex = 0;
            this.MMMainPanel.Controls.SetChildIndex(this.panel1, 0);
            // 
            // ttCompany
            // 
            this.ttCompany.AutomaticDelay = 200;
            this.ttCompany.AutoPopDelay = 5000;
            this.ttCompany.InitialDelay = 10;
            this.ttCompany.ReshowDelay = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mPlbl11);
            this.panel1.Controls.Add(this.mPlbl10);
            this.panel1.Controls.Add(this.mcbSecondCreditor);
            this.panel1.Controls.Add(this.mcbFirstCreditor);
            this.panel1.Controls.Add(this.mPlbl6);
            this.panel1.Controls.Add(this.mPlbl5);
            this.panel1.Controls.Add(this.mPlbl4);
            this.panel1.Controls.Add(this.mPlbl3);
            this.panel1.Controls.Add(this.mPlbl2);
            this.panel1.Controls.Add(this.mPlbl1);
            this.panel1.Controls.Add(this.txtShortName);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.txtContactPerson);
            this.panel1.Controls.Add(this.txtMailId);
            this.panel1.Controls.Add(this.txtTelephone);
            this.panel1.Controls.Add(this.txtAddress);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(65, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(651, 348);
            this.panel1.TabIndex = 0;
            // 
            // mPlbl11
            // 
            this.mPlbl11.AutoSize = true;
            this.mPlbl11.Location = new System.Drawing.Point(46, 268);
            this.mPlbl11.Name = "mPlbl11";
            this.mPlbl11.Size = new System.Drawing.Size(127, 19);
            this.mPlbl11.TabIndex = 52;
            this.mPlbl11.Text = "S&econd Creditor";
            // 
            // mPlbl10
            // 
            this.mPlbl10.AutoSize = true;
            this.mPlbl10.Location = new System.Drawing.Point(65, 237);
            this.mPlbl10.Name = "mPlbl10";
            this.mPlbl10.Size = new System.Drawing.Size(108, 19);
            this.mPlbl10.TabIndex = 50;
            this.mPlbl10.Text = "Fi&rst Creditor";
            // 
            // mcbSecondCreditor
            // 
            this.mcbSecondCreditor.ColumnWidth = null;
            this.mcbSecondCreditor.DataSource = null;
            this.mcbSecondCreditor.DisplayColumnNo = 1;
            this.mcbSecondCreditor.DropDownHeight = 200;
            this.mcbSecondCreditor.Location = new System.Drawing.Point(188, 266);
            this.mcbSecondCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbSecondCreditor.Name = "mcbSecondCreditor";
            this.mcbSecondCreditor.SelectedID = null;
            this.mcbSecondCreditor.ShowNew = true;
            this.mcbSecondCreditor.Size = new System.Drawing.Size(363, 26);
            this.mcbSecondCreditor.SourceDataString = null;
            this.mcbSecondCreditor.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbSecondCreditor.TabIndex = 7;
            this.mcbSecondCreditor.UserControlToShow = null;
            this.mcbSecondCreditor.ValueColumnNo = 0;
            // 
            // mcbFirstCreditor
            // 
            this.mcbFirstCreditor.ColumnWidth = null;
            this.mcbFirstCreditor.DataSource = null;
            this.mcbFirstCreditor.DisplayColumnNo = 1;
            this.mcbFirstCreditor.DropDownHeight = 200;
            this.mcbFirstCreditor.Location = new System.Drawing.Point(188, 235);
            this.mcbFirstCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbFirstCreditor.Name = "mcbFirstCreditor";
            this.mcbFirstCreditor.SelectedID = null;
            this.mcbFirstCreditor.ShowNew = true;
            this.mcbFirstCreditor.Size = new System.Drawing.Size(363, 26);
            this.mcbFirstCreditor.SourceDataString = null;
            this.mcbFirstCreditor.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbFirstCreditor.TabIndex = 6;
            this.mcbFirstCreditor.UserControlToShow = null;
            this.mcbFirstCreditor.ValueColumnNo = 0;
            this.mcbFirstCreditor.EnterKeyPressed += new System.EventHandler(this.mcbFirstCreditor_EnterKeyPressed);
            // 
            // mPlbl6
            // 
            this.mPlbl6.AutoSize = true;
            this.mPlbl6.Location = new System.Drawing.Point(59, 209);
            this.mPlbl6.Name = "mPlbl6";
            this.mPlbl6.Size = new System.Drawing.Size(121, 19);
            this.mPlbl6.TabIndex = 49;
            this.mPlbl6.Text = "&Contact Person";
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(115, 182);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(62, 19);
            this.mPlbl5.TabIndex = 48;
            this.mPlbl5.Text = "Mail &Id";
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(88, 152);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(87, 19);
            this.mPlbl4.TabIndex = 47;
            this.mPlbl4.Text = "&Telephone";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(106, 110);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(68, 19);
            this.mPlbl3.TabIndex = 46;
            this.mPlbl3.Text = "&Address";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(83, 78);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(95, 19);
            this.mPlbl2.TabIndex = 45;
            this.mPlbl2.Text = "&Short Name";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(124, 45);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(51, 19);
            this.mPlbl1.TabIndex = 44;
            this.mPlbl1.Text = "&Name";
            // 
            // txtShortName
            // 
            this.txtShortName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShortName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShortName.Location = new System.Drawing.Point(186, 77);
            this.txtShortName.MaxLength = 3;
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(59, 22);
            this.txtShortName.TabIndex = 1;
            this.txtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShortName_KeyDown);
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.ColumnWidth = null;
            this.txtName.DataSource = null;
            this.txtName.DisplayColumnNo = 1;
            this.txtName.DropDownHeight = 200;
            this.txtName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(186, 45);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtName.Name = "txtName";
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(438, 26);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 0;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            this.txtName.EnterKeyPressed += new System.EventHandler(this.txtName_EnterKeyPressed);
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactPerson.Location = new System.Drawing.Point(186, 207);
            this.txtContactPerson.MaxLength = 50;
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(435, 22);
            this.txtContactPerson.TabIndex = 5;
            this.txtContactPerson.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContactPerson_KeyDown);
            // 
            // txtMailId
            // 
            this.txtMailId.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMailId.Location = new System.Drawing.Point(186, 179);
            this.txtMailId.MaxLength = 40;
            this.txtMailId.Name = "txtMailId";
            this.txtMailId.Size = new System.Drawing.Size(435, 22);
            this.txtMailId.TabIndex = 4;
            this.txtMailId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMailId_KeyDown);
            // 
            // txtTelephone
            // 
            this.txtTelephone.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelephone.Location = new System.Drawing.Point(186, 151);
            this.txtTelephone.MaxLength = 50;
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(435, 22);
            this.txtTelephone.TabIndex = 3;
            this.txtTelephone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelephone_KeyDown);
            // 
            // txtAddress
            // 
            this.txtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(186, 105);
            this.txtAddress.MaxLength = 50;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(300, 40);
            this.txtAddress.TabIndex = 2;
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(170, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 18);
            this.label2.TabIndex = 30;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(170, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 18);
            this.label1.TabIndex = 29;
            this.label1.Text = "*";
            // 
            // UclCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.Name = "UclCompany";
            this.Size = new System.Drawing.Size(888, 521);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttCompany;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.TextBox txtMailId;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtShortName;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtName;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl6;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl11;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl10;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbSecondCreditor;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbFirstCreditor;
    }
}
