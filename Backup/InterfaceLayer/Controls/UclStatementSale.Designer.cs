namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclStatementSale
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvStatements = new System.Windows.Forms.DataGridView();
            this.pnlMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.mPlbl5 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.btnGo = new System.Windows.Forms.Button();
            this.mPlbl4 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtCreatedDate = new System.Windows.Forms.TextBox();
            this.mPlbl3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.gbFirstSecond = new System.Windows.Forms.GroupBox();
            this.rbtSecond = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtFirst = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton();
            this.txtYear = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtToDate = new System.Windows.Forms.TextBox();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtMonth = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.txtFromDate = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dgvbills = new System.Windows.Forms.DataGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatements)).BeginInit();
            this.pnlMultiSelection1.SuspendLayout();
            this.gbFirstSecond.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbills)).BeginInit();
            this.SuspendLayout();
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvStatements);
            this.MMMainPanel.Controls.Add(this.dgvbills);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvbills, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvStatements, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // dgvStatements
            // 
            this.dgvStatements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStatements.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStatements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStatements.Location = new System.Drawing.Point(0, 0);
            this.dgvStatements.Name = "dgvStatements";
            this.dgvStatements.Size = new System.Drawing.Size(748, 358);
            this.dgvStatements.TabIndex = 1037;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.mPlbl5);
            this.pnlMultiSelection1.Controls.Add(this.btnGo);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl4);
            this.pnlMultiSelection1.Controls.Add(this.txtCreatedDate);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl3);
            this.pnlMultiSelection1.Controls.Add(this.gbFirstSecond);
            this.pnlMultiSelection1.Controls.Add(this.txtYear);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl2);
            this.pnlMultiSelection1.Controls.Add(this.txtToDate);
            this.pnlMultiSelection1.Controls.Add(this.mPlbl1);
            this.pnlMultiSelection1.Controls.Add(this.txtMonth);
            this.pnlMultiSelection1.Controls.Add(this.txtFromDate);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(309, 93);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(345, 251);
            this.pnlMultiSelection1.TabIndex = 1043;
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(12, 205);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(104, 19);
            this.mPlbl5.TabIndex = 1046;
            this.mPlbl5.Text = "Created Date";
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.Lime;
            this.btnGo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(258, 136);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(55, 50);
            this.btnGo.TabIndex = 52;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(80, 166);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(28, 19);
            this.mPlbl4.TabIndex = 1045;
            this.mPlbl4.Text = "To";
            // 
            // txtCreatedDate
            // 
            this.txtCreatedDate.BackColor = System.Drawing.Color.Snow;
            this.txtCreatedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreatedDate.Location = new System.Drawing.Point(116, 203);
            this.txtCreatedDate.Name = "txtCreatedDate";
            this.txtCreatedDate.ReadOnly = true;
            this.txtCreatedDate.Size = new System.Drawing.Size(100, 22);
            this.txtCreatedDate.TabIndex = 46;
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(62, 138);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(48, 19);
            this.mPlbl3.TabIndex = 1044;
            this.mPlbl3.Text = "From";
            // 
            // gbFirstSecond
            // 
            this.gbFirstSecond.Controls.Add(this.rbtSecond);
            this.gbFirstSecond.Controls.Add(this.rbtFirst);
            this.gbFirstSecond.Location = new System.Drawing.Point(44, 78);
            this.gbFirstSecond.Name = "gbFirstSecond";
            this.gbFirstSecond.Size = new System.Drawing.Size(227, 41);
            this.gbFirstSecond.TabIndex = 51;
            this.gbFirstSecond.TabStop = false;
            // 
            // rbtSecond
            // 
            this.rbtSecond.AutoSize = true;
            this.rbtSecond.BackColor = System.Drawing.Color.PapayaWhip;
            this.rbtSecond.Location = new System.Drawing.Point(112, 12);
            this.rbtSecond.Name = "rbtSecond";
            this.rbtSecond.Size = new System.Drawing.Size(108, 21);
            this.rbtSecond.TabIndex = 3;
            this.rbtSecond.TabStop = true;
            this.rbtSecond.Text = "Second Half";
            this.rbtSecond.UseVisualStyleBackColor = true;
            // 
            // rbtFirst
            // 
            this.rbtFirst.AutoSize = true;
            this.rbtFirst.BackColor = System.Drawing.Color.PapayaWhip;
            this.rbtFirst.Location = new System.Drawing.Point(6, 12);
            this.rbtFirst.Name = "rbtFirst";
            this.rbtFirst.Size = new System.Drawing.Size(90, 21);
            this.rbtFirst.TabIndex = 2;
            this.rbtFirst.TabStop = true;
            this.rbtFirst.Text = "First Half";
            this.rbtFirst.UseVisualStyleBackColor = true;
            this.rbtFirst.CheckedChanged += new System.EventHandler(this.rbtFirst_CheckedChanged);
            this.rbtFirst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtFirst_KeyDown);
            // 
            // txtYear
            // 
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Location = new System.Drawing.Point(116, 50);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(77, 26);
            this.txtYear.TabIndex = 1043;
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(67, 52);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(43, 19);
            this.mPlbl2.TabIndex = 1043;
            this.mPlbl2.Text = "Year";
            // 
            // txtToDate
            // 
            this.txtToDate.BackColor = System.Drawing.Color.Snow;
            this.txtToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToDate.Location = new System.Drawing.Point(116, 164);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.ReadOnly = true;
            this.txtToDate.Size = new System.Drawing.Size(100, 22);
            this.txtToDate.TabIndex = 50;
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(54, 22);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(58, 19);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "Mont&h";
            // 
            // txtMonth
            // 
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonth.Location = new System.Drawing.Point(116, 19);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(41, 26);
            this.txtMonth.TabIndex = 1042;
            this.txtMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMonth_KeyDown);
            this.txtMonth.Validating += new System.ComponentModel.CancelEventHandler(this.txtMonth_Validating);
            // 
            // txtFromDate
            // 
            this.txtFromDate.BackColor = System.Drawing.Color.Snow;
            this.txtFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromDate.Location = new System.Drawing.Point(116, 136);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.ReadOnly = true;
            this.txtFromDate.Size = new System.Drawing.Size(100, 22);
            this.txtFromDate.TabIndex = 48;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 4);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(2, 16);
            this.lblMessage.TabIndex = 1011;
            // 
            // dgvbills
            // 
            this.dgvbills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvbills.Location = new System.Drawing.Point(79, 30);
            this.dgvbills.Name = "dgvbills";
            this.dgvbills.Size = new System.Drawing.Size(50, 50);
            this.dgvbills.TabIndex = 1044;
            // 
            // UclStatementSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclStatementSale";
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatements)).EndInit();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.gbFirstSecond.ResumeLayout(false);
            this.gbFirstSecond.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbills)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStatements;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private System.Windows.Forms.Button btnGo;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private System.Windows.Forms.TextBox txtCreatedDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private System.Windows.Forms.GroupBox gbFirstSecond;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton rbtSecond;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSRadioButton rbtFirst;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtYear;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private System.Windows.Forms.TextBox txtToDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtMonth;
        private System.Windows.Forms.TextBox txtFromDate;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DataGridView dgvbills;
    }
}
