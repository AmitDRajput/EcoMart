namespace EcoMart.InterfaceLayer
{
    partial class UclFACListEntryofScheduleNumber
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlEnterScheduleNumber = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.txtScheduleNumber = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.btnRemoveClearedDate = new System.Windows.Forms.Button();
            this.psLabel3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mpMSVCGroup = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlEnterScheduleNumber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mpMSVCGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(953, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 559);
            this.MMBottomPanel.Size = new System.Drawing.Size(955, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.panel2);
            this.MMMainPanel.Controls.Add(this.panel1);
            this.MMMainPanel.Size = new System.Drawing.Size(955, 507);
            this.MMMainPanel.Controls.SetChildIndex(this.panel1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.panel2, 0);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pnlEnterScheduleNumber);
            this.panel2.Controls.Add(this.mpMSVCGroup);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(953, 481);
            this.panel2.TabIndex = 46;
            // 
            // pnlEnterScheduleNumber
            // 
            this.pnlEnterScheduleNumber.BackColor = System.Drawing.Color.White;
            this.pnlEnterScheduleNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEnterScheduleNumber.Controls.Add(this.txtScheduleNumber);
            this.pnlEnterScheduleNumber.Controls.Add(this.btnRemoveClearedDate);
            this.pnlEnterScheduleNumber.Controls.Add(this.psLabel3);
            this.pnlEnterScheduleNumber.Location = new System.Drawing.Point(326, 202);
            this.pnlEnterScheduleNumber.Name = "pnlEnterScheduleNumber";
            this.pnlEnterScheduleNumber.Size = new System.Drawing.Size(299, 75);
            this.pnlEnterScheduleNumber.TabIndex = 1069;
            this.pnlEnterScheduleNumber.Click += new System.EventHandler(this.pnlEnterScheduleNumber_Click);
            // 
            // txtScheduleNumber
            // 
            this.txtScheduleNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScheduleNumber.Location = new System.Drawing.Point(163, 4);
            this.txtScheduleNumber.MaxLength = 2;
            this.txtScheduleNumber.Name = "txtScheduleNumber";
            this.txtScheduleNumber.Size = new System.Drawing.Size(50, 26);
            this.txtScheduleNumber.TabIndex = 12;
            this.txtScheduleNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScheduleNumber_KeyDown);
            // 
            // btnRemoveClearedDate
            // 
            this.btnRemoveClearedDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveClearedDate.Location = new System.Drawing.Point(25, 42);
            this.btnRemoveClearedDate.Name = "btnRemoveClearedDate";
            this.btnRemoveClearedDate.Size = new System.Drawing.Size(235, 23);
            this.btnRemoveClearedDate.TabIndex = 3;
            this.btnRemoveClearedDate.Text = "Remove Schedule Number";
            this.btnRemoveClearedDate.UseVisualStyleBackColor = true;
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(19, 9);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(137, 14);
            this.psLabel3.TabIndex = 2;
            this.psLabel3.Text = "Enter Schedule Number";
            // 
            // mpMSVCGroup
            // 
            this.mpMSVCGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mpMSVCGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMSVCGroup.Location = new System.Drawing.Point(0, 0);
            this.mpMSVCGroup.Name = "mpMSVCGroup";
            this.mpMSVCGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mpMSVCGroup.Size = new System.Drawing.Size(951, 479);
            this.mpMSVCGroup.TabIndex = 1070;
            this.mpMSVCGroup.DoubleClick += new System.EventHandler(this.mpMSVCGroup_DoubleClick);
            this.mpMSVCGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mpMSVCGroup_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(953, 24);
            this.panel1.TabIndex = 45;
            // 
            // UclFACListEntryofScheduleNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclFACListEntryofScheduleNumber";
            this.Size = new System.Drawing.Size(955, 582);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlEnterScheduleNumber.ResumeLayout(false);
            this.pnlEnterScheduleNumber.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mpMSVCGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private InterfaceLayer.CommonControls.PSpnlMultiSelection pnlEnterScheduleNumber;
        private System.Windows.Forms.Button btnRemoveClearedDate;
        private InterfaceLayer.CommonControls.PSLabel psLabel3;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtScheduleNumber;
        private System.Windows.Forms.DataGridView mpMSVCGroup;              
    }
}
