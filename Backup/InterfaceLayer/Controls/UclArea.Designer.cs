namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclArea
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ttArea = new System.Windows.Forms.ToolTip(this.components);
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.lblName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtName = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(970, 23);
            this.headerLabel1.TabIndex = 1;
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 505);
            this.MMBottomPanel.Size = new System.Drawing.Size(972, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlCenter);
            this.MMMainPanel.Size = new System.Drawing.Size(972, 453);
            this.MMMainPanel.TabIndex = 0;
            this.MMMainPanel.Controls.SetChildIndex(this.pnlCenter, 0);
            // 
            // ttArea
            // 
            this.ttArea.AutomaticDelay = 20;
            this.ttArea.AutoPopDelay = 5000;
            this.ttArea.InitialDelay = 0;
            this.ttArea.ReshowDelay = 0;
            this.ttArea.ShowAlways = true;
            // 
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.LightGray;
            this.pnlCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCenter.Controls.Add(this.lblName);
            this.pnlCenter.Controls.Add(this.txtName);
            this.pnlCenter.Location = new System.Drawing.Point(115, 100);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(530, 150);
            this.pnlCenter.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(55, 62);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(43, 19);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Area";
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.ColumnWidth = null;
            this.txtName.DataSource = null;
            this.txtName.DisplayColumnNo = 1;
            this.txtName.DropDownHeight = 200;
            this.txtName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(100, 60);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtName.Name = "txtName";
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(400, 26);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 0;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            this.txtName.SeletectIndexChanged += new System.EventHandler(this.txtName_SeletectIndexChanged);
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
            this.lblMessage.TabIndex = 1010;
            // 
            // UclArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Name = "UclArea";
            this.Size = new System.Drawing.Size(972, 528);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            this.pnlCenter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttArea;
        private System.Windows.Forms.Panel pnlCenter;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtName;
        private System.Windows.Forms.Label lblMessage;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel lblName;
    }
}
