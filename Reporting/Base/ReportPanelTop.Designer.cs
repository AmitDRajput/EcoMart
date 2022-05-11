namespace EcoMart.Reporting.Base
{
    partial class ReportPanelTop
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
            this.lblViewFrom = new System.Windows.Forms.Label();
            this.ViewToDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.ViewFromDate = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft(this.components);
            this.psLabel2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewtext = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataField1 = new EcoMart.InterfaceLayer.CommonControls.PSTextBox();
            this.lblDataField1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblViewFrom
            // 
            this.lblViewFrom.AutoSize = true;
            this.lblViewFrom.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewFrom.Location = new System.Drawing.Point(438, 12);
            this.lblViewFrom.Name = "lblViewFrom";
            this.lblViewFrom.Size = new System.Drawing.Size(39, 15);
            this.lblViewFrom.TabIndex = 1085;
            this.lblViewFrom.Text = "From";
            // 
            // ViewToDate
            // 
            this.ViewToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewToDate.Location = new System.Drawing.Point(637, 8);
            this.ViewToDate.Name = "ViewToDate";
            this.ViewToDate.Size = new System.Drawing.Size(100, 23);
            this.ViewToDate.TabIndex = 1084;
            this.ViewToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewFromDate
            // 
            this.ViewFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewFromDate.Location = new System.Drawing.Point(488, 8);
            this.ViewFromDate.Name = "ViewFromDate";
            this.ViewFromDate.Size = new System.Drawing.Size(100, 23);
            this.ViewFromDate.TabIndex = 1083;
            this.ViewFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // psLabel2
            // 
            this.psLabel2.AutoSize = true;
            this.psLabel2.Location = new System.Drawing.Point(603, 8);
            this.psLabel2.Name = "psLabel2";
            this.psLabel2.Size = new System.Drawing.Size(21, 14);
            this.psLabel2.TabIndex = 1082;
            this.psLabel2.Text = "To";
            // 
            // txtViewtext
            // 
            this.txtViewtext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewtext.Location = new System.Drawing.Point(293, 8);
            this.txtViewtext.Name = "txtViewtext";
            this.txtViewtext.Size = new System.Drawing.Size(101, 22);
            this.txtViewtext.TabIndex = 1081;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(238, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1080;
            this.label1.Text = "Type";
            // 
            // txtDataField1
            // 
            this.txtDataField1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDataField1.Location = new System.Drawing.Point(77, 5);
            this.txtDataField1.Name = "txtDataField1";
            this.txtDataField1.Size = new System.Drawing.Size(142, 22);
            this.txtDataField1.TabIndex = 1087;
            // 
            // lblDataField1
            // 
            this.lblDataField1.AutoSize = true;
            this.lblDataField1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataField1.Location = new System.Drawing.Point(8, 9);
            this.lblDataField1.Name = "lblDataField1";
            this.lblDataField1.Size = new System.Drawing.Size(63, 15);
            this.lblDataField1.TabIndex = 1086;
            this.lblDataField1.Text = "Company";
            // 
            // ReportPanelTop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDataField1);
            this.Controls.Add(this.lblDataField1);
            this.Controls.Add(this.lblViewFrom);
            this.Controls.Add(this.ViewToDate);
            this.Controls.Add(this.ViewFromDate);
            this.Controls.Add(this.psLabel2);
            this.Controls.Add(this.txtViewtext);
            this.Controls.Add(this.label1);
            this.Name = "ReportPanelTop";
            this.Size = new System.Drawing.Size(751, 38);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblViewFrom;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewToDate;
        private InterfaceLayer.CommonControls.PSLableWithBorderMiddleLeft ViewFromDate;
        private InterfaceLayer.CommonControls.PSLabel psLabel2;
        private InterfaceLayer.CommonControls.PSTextBox txtViewtext;
        private System.Windows.Forms.Label label1;
        private InterfaceLayer.CommonControls.PSTextBox txtDataField1;
        private System.Windows.Forms.Label lblDataField1;
    }
}
