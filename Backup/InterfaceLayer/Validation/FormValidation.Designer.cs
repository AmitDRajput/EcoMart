namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class FormValidation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormValidation));
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblConfiguration = new System.Windows.Forms.Label();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.pnlLeft.SuspendLayout();
            this.grpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(303, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLeft.BackgroundImage")));
            this.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlLeft.Controls.Add(this.label1);
            this.pnlLeft.Controls.Add(this.lblConfiguration);
            this.pnlLeft.Location = new System.Drawing.Point(-2, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(153, 314);
            this.pnlLeft.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Version: v1.0.0";
            // 
            // lblConfiguration
            // 
            this.lblConfiguration.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfiguration.Location = new System.Drawing.Point(14, 9);
            this.lblConfiguration.Name = "lblConfiguration";
            this.lblConfiguration.Size = new System.Drawing.Size(123, 68);
            this.lblConfiguration.TabIndex = 0;
            this.lblConfiguration.Text = "PharmaSYS     Retail Plus Configuration";
            // 
            // grpButtons
            // 
            this.grpButtons.Controls.Add(this.btnFinish);
            this.grpButtons.Controls.Add(this.btnContinue);
            this.grpButtons.Controls.Add(this.btnNext);
            this.grpButtons.Controls.Add(this.btnBack);
            this.grpButtons.Controls.Add(this.btnCancel);
            this.grpButtons.Location = new System.Drawing.Point(157, 266);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(384, 42);
            this.grpButtons.TabIndex = 6;
            this.grpButtons.TabStop = false;
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(222, 14);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 5;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(303, 14);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 1;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(222, 14);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(6, 14);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Visible = false;
            // 
            // grpMain
            // 
            this.grpMain.Location = new System.Drawing.Point(157, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(384, 260);
            this.grpMain.TabIndex = 7;
            this.grpMain.TabStop = false;
            // 
            // FormValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 314);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.grpButtons);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormValidation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.grpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
       
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox grpButtons;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Label lblConfiguration;
        private System.Windows.Forms.Label label1;
    }
}