namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    partial class CustomizePrintSetting
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
            this.btnaddcancel = new System.Windows.Forms.Button();
            this.btnadd = new System.Windows.Forms.Button();
            this.txtaddnewprintersetingvalue = new System.Windows.Forms.TextBox();
            this.cmbprintseting = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnview = new System.Windows.Forms.Button();
            this.dgvpageheader = new System.Windows.Forms.DataGridView();
            this.pnlgeneral = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btncancel = new System.Windows.Forms.Button();
            this.txtpagewidth = new System.Windows.Forms.TextBox();
            this.btnsavegridvalue = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtpageheight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtreverselinefeed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtlinefeed = new System.Windows.Forms.TextBox();
            this.txtcontentstartrow = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvpagefooter = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.dgprintsettingview = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpageheader)).BeginInit();
            this.pnlgeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpagefooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgprintsettingview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(946, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 499);
            this.MMBottomPanel.Size = new System.Drawing.Size(948, 57);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.splitContainer1);
            this.MMMainPanel.Size = new System.Drawing.Size(948, 436);
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.splitContainer1, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(480, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 16);
            // 
            // ctrlUclSaleSummaryControl
            // 
            this.ctrlUclSaleSummaryControl.Size = new System.Drawing.Size(848, 254);
            // 
            // btnaddcancel
            // 
            this.btnaddcancel.Location = new System.Drawing.Point(770, 1);
            this.btnaddcancel.Name = "btnaddcancel";
            this.btnaddcancel.Size = new System.Drawing.Size(85, 25);
            this.btnaddcancel.TabIndex = 14;
            this.btnaddcancel.Text = "Remove";
            this.btnaddcancel.UseVisualStyleBackColor = true;
            this.btnaddcancel.Click += new System.EventHandler(this.btnaddcancel_Click);
            // 
            // btnadd
            // 
            this.btnadd.Location = new System.Drawing.Point(663, 1);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(83, 28);
            this.btnadd.TabIndex = 13;
            this.btnadd.Text = "Add";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // txtaddnewprintersetingvalue
            // 
            this.txtaddnewprintersetingvalue.Location = new System.Drawing.Point(468, 3);
            this.txtaddnewprintersetingvalue.Name = "txtaddnewprintersetingvalue";
            this.txtaddnewprintersetingvalue.Size = new System.Drawing.Size(170, 23);
            this.txtaddnewprintersetingvalue.TabIndex = 5;
            // 
            // cmbprintseting
            // 
            this.cmbprintseting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbprintseting.FormattingEnabled = true;
            this.cmbprintseting.Items.AddRange(new object[] {
            "SaleBillPrintSettingsPlainPaper",
            "SaleBillPrintSettingsPrePrintedPaper",
            "DebitNotePrintSettingsPlainPaper",
            "DebitNotePrintSettingsPrePrintedPaper",
            "CashBankPrintSettingsPlainPaper",
            "CashBankPrintSettingsPrePrintedPaper",
            "StockOutPrintSettingsPlainPaper",
            "StockOutPrintSettingsPrePrintedPaper"});
            this.cmbprintseting.Location = new System.Drawing.Point(137, 4);
            this.cmbprintseting.Name = "cmbprintseting";
            this.cmbprintseting.Size = new System.Drawing.Size(303, 23);
            this.cmbprintseting.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(16, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Printer Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Print Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(195, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "General Setting";
            // 
            // btnview
            // 
            this.btnview.Location = new System.Drawing.Point(283, 87);
            this.btnview.Name = "btnview";
            this.btnview.Size = new System.Drawing.Size(73, 33);
            this.btnview.TabIndex = 27;
            this.btnview.Text = "View";
            this.btnview.UseVisualStyleBackColor = true;
            this.btnview.Click += new System.EventHandler(this.btnview_Click);
            // 
            // dgvpageheader
            // 
            this.dgvpageheader.AllowUserToAddRows = false;
            this.dgvpageheader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvpageheader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvpageheader.Location = new System.Drawing.Point(0, 0);
            this.dgvpageheader.Name = "dgvpageheader";
            this.dgvpageheader.Size = new System.Drawing.Size(386, 240);
            this.dgvpageheader.TabIndex = 13;
            this.dgvpageheader.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvpageheader_CellClick);
            this.dgvpageheader.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvpageheader_CellContentClick);
            this.dgvpageheader.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvpageheader_CellContentDoubleClick);
            this.dgvpageheader.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvpageheader_DataError);
            this.dgvpageheader.SelectionChanged += new System.EventHandler(this.dgvpageheader_SelectionChanged);
            // 
            // pnlgeneral
            // 
            this.pnlgeneral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlgeneral.Controls.Add(this.btnview);
            this.pnlgeneral.Controls.Add(this.label8);
            this.pnlgeneral.Controls.Add(this.label9);
            this.pnlgeneral.Controls.Add(this.btncancel);
            this.pnlgeneral.Controls.Add(this.txtpagewidth);
            this.pnlgeneral.Controls.Add(this.btnsavegridvalue);
            this.pnlgeneral.Controls.Add(this.label4);
            this.pnlgeneral.Controls.Add(this.txtpageheight);
            this.pnlgeneral.Controls.Add(this.label6);
            this.pnlgeneral.Controls.Add(this.txtreverselinefeed);
            this.pnlgeneral.Controls.Add(this.label5);
            this.pnlgeneral.Controls.Add(this.txtlinefeed);
            this.pnlgeneral.Controls.Add(this.txtcontentstartrow);
            this.pnlgeneral.Controls.Add(this.label7);
            this.pnlgeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlgeneral.Location = new System.Drawing.Point(0, 0);
            this.pnlgeneral.Name = "pnlgeneral";
            this.pnlgeneral.Size = new System.Drawing.Size(560, 125);
            this.pnlgeneral.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Gainsboro;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 18);
            this.label8.TabIndex = 24;
            this.label8.Text = "General Settings";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Gainsboro;
            this.label9.Location = new System.Drawing.Point(3, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 16);
            this.label9.TabIndex = 25;
            this.label9.Text = "Page Width";
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(186, 88);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 33);
            this.btncancel.TabIndex = 14;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // txtpagewidth
            // 
            this.txtpagewidth.Location = new System.Drawing.Point(87, 25);
            this.txtpagewidth.Name = "txtpagewidth";
            this.txtpagewidth.Size = new System.Drawing.Size(73, 23);
            this.txtpagewidth.TabIndex = 4;
            // 
            // btnsavegridvalue
            // 
            this.btnsavegridvalue.Location = new System.Drawing.Point(88, 87);
            this.btnsavegridvalue.Name = "btnsavegridvalue";
            this.btnsavegridvalue.Size = new System.Drawing.Size(73, 33);
            this.btnsavegridvalue.TabIndex = 13;
            this.btnsavegridvalue.Text = "Save";
            this.btnsavegridvalue.UseVisualStyleBackColor = true;
            this.btnsavegridvalue.Click += new System.EventHandler(this.btnsavegridvalue_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.Location = new System.Drawing.Point(3, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Page Height";
            // 
            // txtpageheight
            // 
            this.txtpageheight.Location = new System.Drawing.Point(87, 56);
            this.txtpageheight.Name = "txtpageheight";
            this.txtpageheight.Size = new System.Drawing.Size(73, 23);
            this.txtpageheight.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Gainsboro;
            this.label6.Location = new System.Drawing.Point(166, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "ReverseLineFeed";
            // 
            // txtreverselinefeed
            // 
            this.txtreverselinefeed.Location = new System.Drawing.Point(290, 25);
            this.txtreverselinefeed.Name = "txtreverselinefeed";
            this.txtreverselinefeed.Size = new System.Drawing.Size(73, 23);
            this.txtreverselinefeed.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.Location = new System.Drawing.Point(166, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Line Feed";
            // 
            // txtlinefeed
            // 
            this.txtlinefeed.Location = new System.Drawing.Point(290, 56);
            this.txtlinefeed.Name = "txtlinefeed";
            this.txtlinefeed.Size = new System.Drawing.Size(73, 23);
            this.txtlinefeed.TabIndex = 14;
            // 
            // txtcontentstartrow
            // 
            this.txtcontentstartrow.Location = new System.Drawing.Point(482, 25);
            this.txtcontentstartrow.Name = "txtcontentstartrow";
            this.txtcontentstartrow.Size = new System.Drawing.Size(55, 23);
            this.txtcontentstartrow.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.Location = new System.Drawing.Point(365, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "ContentStartRow";
            // 
            // dgvpagefooter
            // 
            this.dgvpagefooter.AllowUserToAddRows = false;
            this.dgvpagefooter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvpagefooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvpagefooter.Location = new System.Drawing.Point(0, 18);
            this.dgvpagefooter.Name = "dgvpagefooter";
            this.dgvpagefooter.Size = new System.Drawing.Size(386, 138);
            this.dgvpagefooter.TabIndex = 26;
            this.dgvpagefooter.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvpagefooter_CellContentClick);
            this.dgvpagefooter.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvpagefooter_CellContentDoubleClick);
            this.dgvpagefooter.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvpagefooter_DataError);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Gainsboro;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 18);
            this.label11.TabIndex = 24;
            this.label11.Text = "Page Header";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Gainsboro;
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.label27.Location = new System.Drawing.Point(0, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(81, 18);
            this.label27.TabIndex = 24;
            this.label27.Text = "Page Footer";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Gainsboro;
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Location = new System.Drawing.Point(0, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(90, 18);
            this.label32.TabIndex = 25;
            this.label32.Text = "Page Content";
            // 
            // dgprintsettingview
            // 
            this.dgprintsettingview.AllowUserToAddRows = false;
            this.dgprintsettingview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgprintsettingview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgprintsettingview.Location = new System.Drawing.Point(0, 0);
            this.dgprintsettingview.Name = "dgprintsettingview";
            this.dgprintsettingview.Size = new System.Drawing.Size(560, 271);
            this.dgprintsettingview.TabIndex = 10;
            this.dgprintsettingview.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgprintsettingview_CellContentClick);
            this.dgprintsettingview.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgprintsettingview_CellContentDoubleClick);
            this.dgprintsettingview.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgprintsettingview_DataError);
            this.dgprintsettingview.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgprintsettingview_EditingControlShowing);
            this.dgprintsettingview.SelectionChanged += new System.EventHandler(this.dgprintsettingview_SelectionChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnaddcancel);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btnadd);
            this.splitContainer1.Panel1.Controls.Add(this.cmbprintseting);
            this.splitContainer1.Panel1.Controls.Add(this.txtaddnewprintersetingvalue);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(946, 434);
            this.splitContainer1.SplitterDistance = 30;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label11);
            this.splitContainer2.Panel1.Controls.Add(this.dgvpageheader);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvpagefooter);
            this.splitContainer2.Panel2.Controls.Add(this.label27);
            this.splitContainer2.Size = new System.Drawing.Size(386, 400);
            this.splitContainer2.SplitterDistance = 240;
            this.splitContainer2.TabIndex = 5;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(386, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.label32);
            this.splitContainer3.Panel1.Controls.Add(this.dgprintsettingview);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.pnlgeneral);
            this.splitContainer3.Size = new System.Drawing.Size(560, 400);
            this.splitContainer3.SplitterDistance = 271;
            this.splitContainer3.TabIndex = 6;
            // 
            // CustomizePrintSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Cambria", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomizePrintSetting";
            this.Size = new System.Drawing.Size(948, 556);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpageheader)).EndInit();
            this.pnlgeneral.ResumeLayout(false);
            this.pnlgeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpagefooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgprintsettingview)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtcontentstartrow;
        private System.Windows.Forms.TextBox txtlinefeed;
        private System.Windows.Forms.TextBox txtreverselinefeed;
        private System.Windows.Forms.TextBox txtpageheight;
        private System.Windows.Forms.TextBox txtpagewidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgprintsettingview;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtaddnewprintersetingvalue;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.ComboBox cmbprintseting;
        private System.Windows.Forms.Button btnaddcancel;
        private System.Windows.Forms.Button btnsavegridvalue;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.DataGridView dgvpageheader;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.DataGridView dgvpagefooter;
        private System.Windows.Forms.Button btnview;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlgeneral;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
    }
}