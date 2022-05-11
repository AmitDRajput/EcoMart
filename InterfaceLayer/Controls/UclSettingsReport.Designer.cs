namespace EcoMart.InterfaceLayer
{
    partial class UclSettingsReport
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
            this.label4 = new System.Windows.Forms.Label();
            this.cbSaleDailySaleDoNotShowTotal = new System.Windows.Forms.CheckBox();
            this.txtFont = new System.Windows.Forms.TextBox();
            this.cbFontSize = new System.Windows.Forms.ComboBox();
            this.lblFontSize = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.cbFontName = new System.Windows.Forms.ComboBox();
            this.lblFontName = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.cbSaleDailyProductsDoNotShowTotal = new System.Windows.Forms.CheckBox();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(966, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 498);
            this.MMBottomPanel.Size = new System.Drawing.Size(968, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.cbSaleDailyProductsDoNotShowTotal);
            this.MMMainPanel.Controls.Add(this.txtFont);
            this.MMMainPanel.Controls.Add(this.cbFontSize);
            this.MMMainPanel.Controls.Add(this.lblFontSize);
            this.MMMainPanel.Controls.Add(this.cbFontName);
            this.MMMainPanel.Controls.Add(this.lblFontName);
            this.MMMainPanel.Controls.Add(this.label4);
            this.MMMainPanel.Controls.Add(this.cbSaleDailySaleDoNotShowTotal);
            this.MMMainPanel.Size = new System.Drawing.Size(968, 446);
            this.MMMainPanel.Controls.SetChildIndex(this.cbSaleDailySaleDoNotShowTotal, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.label4, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.lblFontName, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.cbFontName, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.lblFontSize, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.cbFontSize, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtFont, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.cbSaleDailyProductsDoNotShowTotal, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(118, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 17);
            this.label4.TabIndex = 61;
            this.label4.Text = "Sale";
            // 
            // cbSaleDailySaleDoNotShowTotal
            // 
            this.cbSaleDailySaleDoNotShowTotal.AutoSize = true;
            this.cbSaleDailySaleDoNotShowTotal.BackColor = System.Drawing.Color.Transparent;
            this.cbSaleDailySaleDoNotShowTotal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSaleDailySaleDoNotShowTotal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSaleDailySaleDoNotShowTotal.Location = new System.Drawing.Point(87, 45);
            this.cbSaleDailySaleDoNotShowTotal.Name = "cbSaleDailySaleDoNotShowTotal";
            this.cbSaleDailySaleDoNotShowTotal.Size = new System.Drawing.Size(208, 18);
            this.cbSaleDailySaleDoNotShowTotal.TabIndex = 60;
            this.cbSaleDailySaleDoNotShowTotal.Text = "Daily Sale Do Not Show Total";
            this.cbSaleDailySaleDoNotShowTotal.UseVisualStyleBackColor = false;
            // 
            // txtFont
            // 
            this.txtFont.Location = new System.Drawing.Point(743, 60);
            this.txtFont.Name = "txtFont";
            this.txtFont.Size = new System.Drawing.Size(131, 20);
            this.txtFont.TabIndex = 1107;
            this.txtFont.Text = "ABCDEFGHIJ";
            // 
            // cbFontSize
            // 
            this.cbFontSize.AllowDrop = true;
            this.cbFontSize.BackColor = System.Drawing.Color.White;
            this.cbFontSize.DisplayMember = "0";
            this.cbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFontSize.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFontSize.FormattingEnabled = true;
            this.cbFontSize.Location = new System.Drawing.Point(504, 76);
            this.cbFontSize.Name = "cbFontSize";
            this.cbFontSize.Size = new System.Drawing.Size(190, 24);
            this.cbFontSize.TabIndex = 1106;
            this.cbFontSize.SelectedIndexChanged += new System.EventHandler(this.cbFontSize_SelectedIndexChanged);
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(432, 83);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(59, 14);
            this.lblFontSize.TabIndex = 1105;
            this.lblFontSize.Text = "Font Size:";
            // 
            // cbFontName
            // 
            this.cbFontName.AllowDrop = true;
            this.cbFontName.BackColor = System.Drawing.Color.White;
            this.cbFontName.DisplayMember = "0";
            this.cbFontName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFontName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFontName.FormattingEnabled = true;
            this.cbFontName.Location = new System.Drawing.Point(504, 42);
            this.cbFontName.Name = "cbFontName";
            this.cbFontName.Size = new System.Drawing.Size(190, 24);
            this.cbFontName.TabIndex = 1104;
            this.cbFontName.SelectedIndexChanged += new System.EventHandler(this.cbFontName_SelectedIndexChanged);
            // 
            // lblFontName
            // 
            this.lblFontName.AutoSize = true;
            this.lblFontName.Location = new System.Drawing.Point(423, 49);
            this.lblFontName.Name = "lblFontName";
            this.lblFontName.Size = new System.Drawing.Size(68, 14);
            this.lblFontName.TabIndex = 1103;
            this.lblFontName.Text = "Font Name:";
            // 
            // cbSaleDailyProductsDoNotShowTotal
            // 
            this.cbSaleDailyProductsDoNotShowTotal.AutoSize = true;
            this.cbSaleDailyProductsDoNotShowTotal.BackColor = System.Drawing.Color.Transparent;
            this.cbSaleDailyProductsDoNotShowTotal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSaleDailyProductsDoNotShowTotal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSaleDailyProductsDoNotShowTotal.Location = new System.Drawing.Point(59, 76);
            this.cbSaleDailyProductsDoNotShowTotal.Name = "cbSaleDailyProductsDoNotShowTotal";
            this.cbSaleDailyProductsDoNotShowTotal.Size = new System.Drawing.Size(236, 18);
            this.cbSaleDailyProductsDoNotShowTotal.TabIndex = 1108;
            this.cbSaleDailyProductsDoNotShowTotal.Text = "Daily Products Do Not Show Total";
            this.cbSaleDailyProductsDoNotShowTotal.UseVisualStyleBackColor = false;
            // 
            // UclSettingsReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclSettingsReport";
            this.Size = new System.Drawing.Size(968, 521);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbSaleDailySaleDoNotShowTotal;
        private System.Windows.Forms.TextBox txtFont;
        private System.Windows.Forms.ComboBox cbFontSize;
        private CommonControls.PSLabel lblFontSize;
        private System.Windows.Forms.ComboBox cbFontName;
        private CommonControls.PSLabel lblFontName;
        private System.Windows.Forms.CheckBox cbSaleDailyProductsDoNotShowTotal;
    }
}
