namespace EcoMart.InterfaceLayer
{
    partial class UclCustomer
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
            this.ttCustomer = new System.Windows.Forms.ToolTip(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.txtName = new EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.lblName = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(903, 23);
            // 
            // MMBottomPanel
            //            
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 428);
            this.MMBottomPanel.Size = new System.Drawing.Size(905, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlCenter);
            this.MMMainPanel.Size = new System.Drawing.Size(905, 376);
            // 
            // ttCustomer
            // 
            this.ttCustomer.AutomaticDelay = 20;
            this.ttCustomer.AutoPopDelay = 5000;
            this.ttCustomer.InitialDelay = 20;
            this.ttCustomer.ReshowDelay = 4;
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
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.LightGray;
            this.pnlCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCenter.Controls.Add(this.lblName);
            this.pnlCenter.Controls.Add(this.txtName);
            this.pnlCenter.Location = new System.Drawing.Point(202, 148);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(529, 148);
            this.pnlCenter.TabIndex = 1022;
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.ColumnWidth = null;
            this.txtName.DataSource = null;
            this.txtName.DisplayColumnNo = 1;
            this.txtName.DropDownHeight = 200;
            this.txtName.Location = new System.Drawing.Point(100, 60);
            this.txtName.Name = "txtName";
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(400, 22);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 0;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            this.txtName.SeletectIndexChanged += new System.EventHandler(this.txtName_SeletectIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(19, 62);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(75, 17);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Customer";
            // 
            // UclCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Name = "UclCustomer";
            this.Size = new System.Drawing.Size(905, 451);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            this.pnlCenter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttCustomer;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlCenter;
        private EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtName;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblName;

    }
}
