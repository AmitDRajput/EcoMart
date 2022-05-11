using PharmaSYSPlus.CommonLibrary;
namespace EcoMart.InterfaceLayer
{
    partial class UclPartyCompany
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclPartyCompany));
            this.ttPartyCompany = new System.Windows.Forms.ToolTip(this.components);
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.lblNoofRows = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlPartyCompany = new System.Windows.Forms.Panel();
            this.panelComapanyName = new System.Windows.Forms.Panel();
            this.mcbCompany = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.btnAdd = new System.Windows.Forms.Button();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.panelSearchType = new System.Windows.Forms.Panel();
            this.psLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.rdbPartyWise = new System.Windows.Forms.RadioButton();
            this.rdbCompanyWise = new System.Windows.Forms.RadioButton();
            this.panelPartyName = new System.Windows.Forms.Panel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mcbParty = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.dgvCompany = new System.Windows.Forms.DataGridView();
            this.dgvUpperList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.dgvLowerList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.btnReverse = new System.Windows.Forms.Button();
            this.dgvUpperListY = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.dgvLowerListY = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.pnlList = new System.Windows.Forms.Panel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlPartyCompany.SuspendLayout();
            this.panelComapanyName.SuspendLayout();
            this.panelSearchType.SuspendLayout();
            this.panelPartyName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompany)).BeginInit();
            this.pnlList.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(862, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Controls.Add(this.lblNoofRows);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 570);
            this.MMBottomPanel.Size = new System.Drawing.Size(864, 63);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblRightSideFooterMsg, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblNoofRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlPartyCompany);
            this.MMMainPanel.Controls.Add(this.pnlList);
            this.MMMainPanel.Size = new System.Drawing.Size(864, 507);
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlList, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlPartyCompany, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(396, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 20);
            // 
            // ttPartyCompany
            // 
            this.ttPartyCompany.AutomaticDelay = 200;
            this.ttPartyCompany.AutoPopDelay = 5000;
            this.ttPartyCompany.InitialDelay = 10;
            this.ttPartyCompany.ReshowDelay = 10;
            this.ttPartyCompany.ShowAlways = true;
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(693, 0);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 22);
            this.txtNoOfRows.TabIndex = 1013;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNoofRows
            // 
            this.lblNoofRows.AutoSize = true;
            this.lblNoofRows.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofRows.Location = new System.Drawing.Point(602, 3);
            this.lblNoofRows.Name = "lblNoofRows";
            this.lblNoofRows.Size = new System.Drawing.Size(74, 15);
            this.lblNoofRows.TabIndex = 1012;
            this.lblNoofRows.Text = "No Of Rows";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 2);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(2, 16);
            this.lblMessage.TabIndex = 1009;
            // 
            // pnlPartyCompany
            // 
            this.pnlPartyCompany.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlPartyCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPartyCompany.Controls.Add(this.panelComapanyName);
            this.pnlPartyCompany.Controls.Add(this.panelSearchType);
            this.pnlPartyCompany.Controls.Add(this.panelPartyName);
            this.pnlPartyCompany.Controls.Add(this.dgvCompany);
            this.pnlPartyCompany.Location = new System.Drawing.Point(79, 10);
            this.pnlPartyCompany.Name = "pnlPartyCompany";
            this.pnlPartyCompany.Size = new System.Drawing.Size(704, 474);
            this.pnlPartyCompany.TabIndex = 0;
            // 
            // panelComapanyName
            // 
            this.panelComapanyName.Controls.Add(this.mcbCompany);
            this.panelComapanyName.Controls.Add(this.btnAdd);
            this.panelComapanyName.Controls.Add(this.mPlbl2);
            this.panelComapanyName.Location = new System.Drawing.Point(74, 82);
            this.panelComapanyName.Name = "panelComapanyName";
            this.panelComapanyName.Size = new System.Drawing.Size(564, 32);
            this.panelComapanyName.TabIndex = 146;
            // 
            // mcbCompany
            // 
            this.mcbCompany.ColumnWidth = null;
            this.mcbCompany.DataSource = null;
            this.mcbCompany.DisplayColumnNo = 1;
            this.mcbCompany.DropDownHeight = 200;
            this.mcbCompany.Location = new System.Drawing.Point(92, 5);
            this.mcbCompany.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbCompany.Name = "mcbCompany";
            this.mcbCompany.SelectedID = "";
            this.mcbCompany.ShowNew = false;
            this.mcbCompany.Size = new System.Drawing.Size(375, 22);
            this.mcbCompany.SourceDataString = null;
            this.mcbCompany.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCompany.TabIndex = 1;
            this.mcbCompany.UserControlToShow = null;
            this.mcbCompany.ValueColumnNo = 0;
            this.mcbCompany.EnterKeyPressed += new System.EventHandler(this.mcbCompany_EnterKeyPressed);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(482, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 31);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "&Add ";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(5, 8);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(65, 16);
            this.mPlbl2.TabIndex = 139;
            this.mPlbl2.Text = "&Company";
            // 
            // panelSearchType
            // 
            this.panelSearchType.Controls.Add(this.psLabel1);
            this.panelSearchType.Controls.Add(this.rdbPartyWise);
            this.panelSearchType.Controls.Add(this.rdbCompanyWise);
            this.panelSearchType.Location = new System.Drawing.Point(74, 20);
            this.panelSearchType.Name = "panelSearchType";
            this.panelSearchType.Size = new System.Drawing.Size(564, 28);
            this.panelSearchType.TabIndex = 145;
            this.panelSearchType.Visible = false;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(5, 6);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(82, 16);
            this.psLabel1.TabIndex = 140;
            this.psLabel1.Text = "Search &Type";
            // 
            // rdbPartyWise
            // 
            this.rdbPartyWise.AutoSize = true;
            this.rdbPartyWise.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbPartyWise.Location = new System.Drawing.Point(107, 4);
            this.rdbPartyWise.Name = "rdbPartyWise";
            this.rdbPartyWise.Size = new System.Drawing.Size(91, 20);
            this.rdbPartyWise.TabIndex = 141;
            this.rdbPartyWise.TabStop = true;
            this.rdbPartyWise.Text = "Party Wise";
            this.rdbPartyWise.UseVisualStyleBackColor = true;
            this.rdbPartyWise.CheckedChanged += new System.EventHandler(this.rdbPartyWise_CheckedChanged);
            // 
            // rdbCompanyWise
            // 
            this.rdbCompanyWise.AutoSize = true;
            this.rdbCompanyWise.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbCompanyWise.Location = new System.Drawing.Point(239, 4);
            this.rdbCompanyWise.Name = "rdbCompanyWise";
            this.rdbCompanyWise.Size = new System.Drawing.Size(118, 20);
            this.rdbCompanyWise.TabIndex = 142;
            this.rdbCompanyWise.TabStop = true;
            this.rdbCompanyWise.Text = "Company Wise";
            this.rdbCompanyWise.UseVisualStyleBackColor = true;
            this.rdbCompanyWise.CheckedChanged += new System.EventHandler(this.rdbCompanyWise_CheckedChanged);
            // 
            // panelPartyName
            // 
            this.panelPartyName.Controls.Add(this.mPlbl1);
            this.panelPartyName.Controls.Add(this.mcbParty);
            this.panelPartyName.Controls.Add(this.btnViewAll);
            this.panelPartyName.Location = new System.Drawing.Point(74, 48);
            this.panelPartyName.Name = "panelPartyName";
            this.panelPartyName.Size = new System.Drawing.Size(564, 34);
            this.panelPartyName.TabIndex = 144;
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(5, 9);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(80, 16);
            this.mPlbl1.TabIndex = 138;
            this.mPlbl1.Text = "&Party Name";
            // 
            // mcbParty
            // 
            this.mcbParty.ColumnWidth = null;
            this.mcbParty.DataSource = null;
            this.mcbParty.DisplayColumnNo = 1;
            this.mcbParty.DropDownHeight = 200;
            this.mcbParty.Location = new System.Drawing.Point(92, 6);
            this.mcbParty.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbParty.Name = "mcbParty";
            this.mcbParty.SelectedID = "";
            this.mcbParty.ShowNew = false;
            this.mcbParty.Size = new System.Drawing.Size(375, 22);
            this.mcbParty.SourceDataString = null;
            this.mcbParty.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbParty.TabIndex = 0;
            this.mcbParty.UserControlToShow = null;
            this.mcbParty.ValueColumnNo = 0;
            this.mcbParty.SeletectIndexChanged += new System.EventHandler(this.mcbParty_SeletectIndexChanged);
            this.mcbParty.EnterKeyPressed += new System.EventHandler(this.mcbParty_EnterKeyPressed);
            // 
            // btnViewAll
            // 
            this.btnViewAll.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewAll.Location = new System.Drawing.Point(482, 2);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(71, 31);
            this.btnViewAll.TabIndex = 137;
            this.btnViewAll.Text = "V&iewAll";
            this.btnViewAll.UseVisualStyleBackColor = true;
            this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);
            // 
            // dgvCompany
            // 
            this.dgvCompany.AllowUserToAddRows = false;
            this.dgvCompany.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCompany.CausesValidation = false;
            this.dgvCompany.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompany.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvCompany.Enabled = false;
            this.dgvCompany.Location = new System.Drawing.Point(42, 120);
            this.dgvCompany.MultiSelect = false;
            this.dgvCompany.Name = "dgvCompany";
            this.dgvCompany.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompany.Size = new System.Drawing.Size(619, 337);
            this.dgvCompany.TabIndex = 3;
            // 
            // dgvUpperList
            // 
            this.dgvUpperList.BackColor = System.Drawing.Color.Transparent;
            this.dgvUpperList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvUpperList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperList.DateColumnNames")));
            this.dgvUpperList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperList.DoubleColumnNames")));
            this.dgvUpperList.Filter = null;
            this.dgvUpperList.Location = new System.Drawing.Point(15, 0);
            this.dgvUpperList.Name = "dgvUpperList";
            this.dgvUpperList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvUpperList.ShowGridFilter = false;
            this.dgvUpperList.Size = new System.Drawing.Size(649, 219);
            this.dgvUpperList.TabIndex = 27;
            this.dgvUpperList.SelectedRowChanged += new System.EventHandler(this.dgvUpperList_SelectedRowChanged);
            // 
            // dgvLowerList
            // 
            this.dgvLowerList.BackColor = System.Drawing.Color.Transparent;
            this.dgvLowerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvLowerList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerList.DateColumnNames")));
            this.dgvLowerList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerList.DoubleColumnNames")));
            this.dgvLowerList.Filter = null;
            this.dgvLowerList.Location = new System.Drawing.Point(15, 236);
            this.dgvLowerList.Name = "dgvLowerList";
            this.dgvLowerList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvLowerList.ShowGridFilter = false;
            this.dgvLowerList.Size = new System.Drawing.Size(649, 287);
            this.dgvLowerList.TabIndex = 29;
            // 
            // btnReverse
            // 
            this.btnReverse.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReverse.Location = new System.Drawing.Point(673, 3);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(22, 116);
            this.btnReverse.TabIndex = 52;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // dgvUpperListY
            // 
            this.dgvUpperListY.BackColor = System.Drawing.Color.Transparent;
            this.dgvUpperListY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvUpperListY.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperListY.DateColumnNames")));
            this.dgvUpperListY.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperListY.DoubleColumnNames")));
            this.dgvUpperListY.Filter = null;
            this.dgvUpperListY.Location = new System.Drawing.Point(16, 1);
            this.dgvUpperListY.Name = "dgvUpperListY";
            this.dgvUpperListY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvUpperListY.ShowGridFilter = false;
            this.dgvUpperListY.Size = new System.Drawing.Size(649, 219);
            this.dgvUpperListY.TabIndex = 53;
            this.dgvUpperListY.SelectedRowChanged += new System.EventHandler(this.dgvUpperListY_SelectedRowChanged);
            // 
            // dgvLowerListY
            // 
            this.dgvLowerListY.BackColor = System.Drawing.Color.Transparent;
            this.dgvLowerListY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvLowerListY.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerListY.DateColumnNames")));
            this.dgvLowerListY.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerListY.DoubleColumnNames")));
            this.dgvLowerListY.Filter = null;
            this.dgvLowerListY.Location = new System.Drawing.Point(15, 236);
            this.dgvLowerListY.Name = "dgvLowerListY";
            this.dgvLowerListY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvLowerListY.ShowGridFilter = false;
            this.dgvLowerListY.Size = new System.Drawing.Size(649, 287);
            this.dgvLowerListY.TabIndex = 54;
            // 
            // pnlList
            // 
            this.pnlList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlList.Controls.Add(this.dgvLowerListY);
            this.pnlList.Controls.Add(this.dgvUpperListY);
            this.pnlList.Controls.Add(this.btnReverse);
            this.pnlList.Controls.Add(this.dgvLowerList);
            this.pnlList.Controls.Add(this.dgvUpperList);
            this.pnlList.Location = new System.Drawing.Point(79, 21);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(705, 526);
            this.pnlList.TabIndex = 1016;
            // 
            // UclPartyCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Name = "UclPartyCompany";
            this.Size = new System.Drawing.Size(864, 633);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlPartyCompany.ResumeLayout(false);
            this.panelComapanyName.ResumeLayout(false);
            this.panelComapanyName.PerformLayout();
            this.panelSearchType.ResumeLayout(false);
            this.panelSearchType.PerformLayout();
            this.panelPartyName.ResumeLayout(false);
            this.panelPartyName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompany)).EndInit();
            this.pnlList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttPartyCompany;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlPartyCompany;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbCompany;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbParty;
        private System.Windows.Forms.DataGridView dgvCompany;
        private System.Windows.Forms.Button btnAdd;
        private NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Label lblNoofRows;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.Panel pnlList;
        private MDataGridView dgvLowerListY;
        private MDataGridView dgvUpperListY;
        private System.Windows.Forms.Button btnReverse;
        private MDataGridView dgvLowerList;
        private MDataGridView dgvUpperList;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private System.Windows.Forms.RadioButton rdbCompanyWise;
        private System.Windows.Forms.RadioButton rdbPartyWise;
        private CommonControls.PSLabel psLabel1;
        private System.Windows.Forms.Panel panelComapanyName;
        private System.Windows.Forms.Panel panelSearchType;
        private System.Windows.Forms.Panel panelPartyName;
    }
}
