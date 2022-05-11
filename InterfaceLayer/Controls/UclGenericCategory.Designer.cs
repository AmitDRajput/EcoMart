namespace EcoMart.InterfaceLayer
{
    partial class UclGenericCategory
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
            this.ttGeneric = new System.Windows.Forms.ToolTip(this.components);
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.mPlbl12 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtIfScheduledDrug = new System.Windows.Forms.TextBox();
            this.lblMessageText = new EcoMart.InterfaceLayer.CommonControls.PSlblMessage();
            this.lblName = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtName = new EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(735, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 390);
            this.MMBottomPanel.Size = new System.Drawing.Size(737, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlCenter);
            this.MMMainPanel.Size = new System.Drawing.Size(737, 327);
            this.MMMainPanel.TabIndex = 0;
            this.MMMainPanel.Controls.SetChildIndex(this.pnlCenter, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(269, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 20);
            // 
            // ttGeneric
            // 
            this.ttGeneric.AutomaticDelay = 20;
            this.ttGeneric.AutoPopDelay = 5000;
            this.ttGeneric.InitialDelay = 20;
            this.ttGeneric.ReshowDelay = 4;
            this.ttGeneric.ShowAlways = true;
            // 
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.LightGray;
            this.pnlCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCenter.Controls.Add(this.mPlbl12);
            this.pnlCenter.Controls.Add(this.txtIfScheduledDrug);
            this.pnlCenter.Controls.Add(this.lblMessageText);
            this.pnlCenter.Controls.Add(this.lblName);
            this.pnlCenter.Controls.Add(this.txtName);
            this.pnlCenter.Location = new System.Drawing.Point(69, 100);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(620, 148);
            this.pnlCenter.TabIndex = 1023;
            // 
            // mPlbl12
            // 
            this.mPlbl12.AutoSize = true;
            this.mPlbl12.Location = new System.Drawing.Point(3, 83);
            this.mPlbl12.Name = "mPlbl12";
            this.mPlbl12.Size = new System.Drawing.Size(116, 16);
            this.mPlbl12.TabIndex = 34;
            this.mPlbl12.Text = "If Scheduled Drug";
            this.mPlbl12.Visible = false;
            // 
            // txtIfScheduledDrug
            // 
            this.txtIfScheduledDrug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIfScheduledDrug.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIfScheduledDrug.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIfScheduledDrug.Location = new System.Drawing.Point(120, 81);
            this.txtIfScheduledDrug.Name = "txtIfScheduledDrug";
            this.txtIfScheduledDrug.Size = new System.Drawing.Size(37, 23);
            this.txtIfScheduledDrug.TabIndex = 35;
            this.txtIfScheduledDrug.Visible = false;
            this.txtIfScheduledDrug.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIfScheduledDrug_KeyDown);
            // 
            // lblMessageText
            // 
            this.lblMessageText.AutoSize = true;
            this.lblMessageText.Location = new System.Drawing.Point(110, 99);
            this.lblMessageText.Name = "lblMessageText";
            this.lblMessageText.Size = new System.Drawing.Size(0, 16);
            this.lblMessageText.TabIndex = 4;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 47);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(112, 16);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Generic Categor&y";
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
            this.txtName.Location = new System.Drawing.Point(120, 44);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = false;
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(471, 22);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 0;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            this.txtName.SeletectIndexChanged += new System.EventHandler(this.txtName_SeletectIndexChanged);
            this.txtName.EnterKeyPressed += new System.EventHandler(this.txtName_EnterKeyPressed);
            // 
            // UclGenericCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Name = "UclGenericCategory";
            this.Size = new System.Drawing.Size(737, 453);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            this.pnlCenter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttGeneric;
        private System.Windows.Forms.Panel pnlCenter;
        private EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtName;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblName;
        private CommonControls.PSlblMessage lblMessageText;
        private CommonControls.PSLabel mPlbl12;
        private System.Windows.Forms.TextBox txtIfScheduledDrug;
    }
}
