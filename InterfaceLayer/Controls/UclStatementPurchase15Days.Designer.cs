namespace EcoMart.InterfaceLayer
{
    partial class UclStatementPurchase15Days
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStatementPurchase15Days));
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.gbFirstSecond = new System.Windows.Forms.GroupBox();
            this.rbtSecond = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.rbtFirst = new EcoMart.InterfaceLayer.CommonControls.PSRadioButton();
            this.txtToDate = new System.Windows.Forms.TextBox();
            this.txtFromDate = new System.Windows.Forms.TextBox();
            this.txtCreatedDate = new System.Windows.Forms.TextBox();
            this.txtYear = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.txtMonth = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.pnlMultiSelection1 = new EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.mPlbl5 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.dgvStatements = new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.dgvbills = new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.gbFirstSecond.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(978, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 662);
            this.MMBottomPanel.Size = new System.Drawing.Size(980, 63);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvStatements);
            this.MMMainPanel.Controls.Add(this.dgvbills);
            this.MMMainPanel.Size = new System.Drawing.Size(980, 610);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvbills, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvStatements, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlMultiSelection1, 0);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 3);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(2, 16);
            this.lblMessage.TabIndex = 1010;
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
            // txtYear
            // 
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Location = new System.Drawing.Point(116, 50);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(77, 26);
            this.txtYear.TabIndex = 1043;
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
            this.pnlMultiSelection1.Location = new System.Drawing.Point(336, 157);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(345, 251);
            this.pnlMultiSelection1.TabIndex = 1042;
            // 
            // mPlbl5
            // 
            this.mPlbl5.AutoSize = true;
            this.mPlbl5.Location = new System.Drawing.Point(12, 205);
            this.mPlbl5.Name = "mPlbl5";
            this.mPlbl5.Size = new System.Drawing.Size(76, 14);
            this.mPlbl5.TabIndex = 1046;
            this.mPlbl5.Text = "Created Date";
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(80, 166);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(21, 14);
            this.mPlbl4.TabIndex = 1045;
            this.mPlbl4.Text = "To";
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(62, 138);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(38, 14);
            this.mPlbl3.TabIndex = 1044;
            this.mPlbl3.Text = "From";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(67, 52);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(31, 14);
            this.mPlbl2.TabIndex = 1043;
            this.mPlbl2.Text = "Year";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(54, 22);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(42, 14);
            this.mPlbl1.TabIndex = 0;
            this.mPlbl1.Text = "Mont&h";
            // 
            // dgvStatements
            // 
            this.dgvStatements.AutoScroll = true;
            this.dgvStatements.DataSource = null;
            this.dgvStatements.DataSourceMain = null;
            this.dgvStatements.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvStatements.DateColumnNames")));
            this.dgvStatements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStatements.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvStatements.DoubleColumnNames")));
            this.dgvStatements.Filter = null;
            this.dgvStatements.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvStatements.IsAllowDelete = true;
            this.dgvStatements.IsAllowNewRow = true;
            this.dgvStatements.Location = new System.Drawing.Point(0, 0);
            this.dgvStatements.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvStatements.MinimumSize = new System.Drawing.Size(520, 323);
            this.dgvStatements.Name = "dgvStatements";
            this.dgvStatements.NextRowColumn = 0;
            this.dgvStatements.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvStatements.NumericColumnNames")));
            this.dgvStatements.Size = new System.Drawing.Size(978, 608);
            this.dgvStatements.SubGridWidth = 380;
            this.dgvStatements.TabIndex = 1043;
            this.dgvStatements.ViewControl = null;
            this.dgvStatements.OnShowViewForm += new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl.ShowViewForm(this.dgvStatements_OnShowViewForm);
            // 
            // dgvbills
            // 
            this.dgvbills.AutoScroll = true;
            this.dgvbills.DataSource = null;
            this.dgvbills.DataSourceMain = null;
            this.dgvbills.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvbills.DateColumnNames")));
            this.dgvbills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvbills.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvbills.DoubleColumnNames")));
            this.dgvbills.Filter = null;
            this.dgvbills.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvbills.IsAllowDelete = true;
            this.dgvbills.IsAllowNewRow = true;
            this.dgvbills.Location = new System.Drawing.Point(0, 0);
            this.dgvbills.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvbills.MinimumSize = new System.Drawing.Size(520, 323);
            this.dgvbills.Name = "dgvbills";
            this.dgvbills.NextRowColumn = 0;
            this.dgvbills.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvbills.NumericColumnNames")));
            this.dgvbills.Size = new System.Drawing.Size(978, 608);
            this.dgvbills.SubGridWidth = 380;
            this.dgvbills.TabIndex = 1044;
            this.dgvbills.ViewControl = null;
            // 
            // UclStatementPurchase15Days
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclStatementPurchase15Days";
            this.Size = new System.Drawing.Size(980, 685);
            this.Load += new System.EventHandler(this.UclStatementPurchase15Days_Load);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.gbFirstSecond.ResumeLayout(false);
            this.gbFirstSecond.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtCreatedDate;
        private System.Windows.Forms.TextBox txtToDate;
        private System.Windows.Forms.TextBox txtFromDate;
        private System.Windows.Forms.GroupBox gbFirstSecond;
        private System.Windows.Forms.Button btnGo;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtYear;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtMonth;
        private EcoMart.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl5;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl4;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtSecond;
        private EcoMart.InterfaceLayer.CommonControls.PSRadioButton rbtFirst;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl dgvStatements;
        private EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl dgvbills;

    }
}
