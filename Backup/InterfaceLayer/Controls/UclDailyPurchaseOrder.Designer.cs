namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclDailyPurchaseOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclDailyPurchaseOrder));
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.btnExitSummary = new System.Windows.Forms.Button();
            this.mdgOrderDetail = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.mdgOrderSummary = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.mPlbl3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.btnGo = new System.Windows.Forms.Button();
            this.gbAccount = new System.Windows.Forms.GroupBox();
            this.rbtSecond = new System.Windows.Forms.RadioButton();
            this.rbtFirst = new System.Windows.Forms.RadioButton();
            this.datetimepickerFrom = new System.Windows.Forms.DateTimePicker();
            this.datetimepickerTo = new System.Windows.Forms.DateTimePicker();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.mpMainSubViewControl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.txtNoofOrders = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtAmount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.gbAccount.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(965, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Controls.Add(this.label6);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 631);
            this.MMBottomPanel.Size = new System.Drawing.Size(967, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.label6, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlSummary);
            this.MMMainPanel.Controls.Add(this.mpMainSubViewControl1);
            this.MMMainPanel.Controls.Add(this.pnlDate);
            this.MMMainPanel.Size = new System.Drawing.Size(967, 579);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlDate, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMainSubViewControl1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlSummary, 0);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(18, 2);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(606, 16);
            this.lblMessage.TabIndex = 1009;
            this.lblMessage.Text = "Save = Save Grid Details   Create Order =  Save Grid Details with Order Number an" +
                "d Date";
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(765, 0);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 26);
            this.txtNoOfRows.TabIndex = 1006;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(670, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 15);
            this.label6.TabIndex = 1005;
            this.label6.Text = "No Of Rows";
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.DarkOrchid;
            this.pnlSummary.Controls.Add(this.txtAmount);
            this.pnlSummary.Controls.Add(this.txtNoofOrders);
            this.pnlSummary.Controls.Add(this.label21);
            this.pnlSummary.Controls.Add(this.btnExitSummary);
            this.pnlSummary.Controls.Add(this.mdgOrderDetail);
            this.pnlSummary.Controls.Add(this.mdgOrderSummary);
            this.pnlSummary.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSummary.Location = new System.Drawing.Point(156, 100);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(646, 432);
            this.pnlSummary.TabIndex = 1018;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(92, 405);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(99, 17);
            this.label21.TabIndex = 1012;
            this.label21.Text = "No Of  Orders";
            // 
            // btnExitSummary
            // 
            this.btnExitSummary.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitSummary.Location = new System.Drawing.Point(544, 401);
            this.btnExitSummary.Name = "btnExitSummary";
            this.btnExitSummary.Size = new System.Drawing.Size(75, 23);
            this.btnExitSummary.TabIndex = 4;
            this.btnExitSummary.Text = "Exit";
            this.btnExitSummary.UseVisualStyleBackColor = true;
            this.btnExitSummary.Visible = false;
            // 
            // mdgOrderDetail
            // 
            this.mdgOrderDetail.BackColor = System.Drawing.Color.Transparent;
            this.mdgOrderDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mdgOrderDetail.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mdgOrderDetail.DateColumnNames")));
            this.mdgOrderDetail.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mdgOrderDetail.DoubleColumnNames")));
            this.mdgOrderDetail.Filter = null;
            this.mdgOrderDetail.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mdgOrderDetail.Location = new System.Drawing.Point(11, 186);
            this.mdgOrderDetail.Name = "mdgOrderDetail";
            this.mdgOrderDetail.ShowGridFilter = false;
            this.mdgOrderDetail.Size = new System.Drawing.Size(624, 210);
            this.mdgOrderDetail.TabIndex = 3;
            // 
            // mdgOrderSummary
            // 
            this.mdgOrderSummary.BackColor = System.Drawing.Color.Transparent;
            this.mdgOrderSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mdgOrderSummary.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mdgOrderSummary.DateColumnNames")));
            this.mdgOrderSummary.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mdgOrderSummary.DoubleColumnNames")));
            this.mdgOrderSummary.Filter = null;
            this.mdgOrderSummary.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mdgOrderSummary.Location = new System.Drawing.Point(10, 10);
            this.mdgOrderSummary.Name = "mdgOrderSummary";
            this.mdgOrderSummary.ShowGridFilter = false;
            this.mdgOrderSummary.Size = new System.Drawing.Size(625, 176);
            this.mdgOrderSummary.TabIndex = 2;
            this.mdgOrderSummary.SelectedRowChanged += new System.EventHandler(this.mdgOrderSummary_SelectedRowChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(275, 405);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Order Value";
            // 
            // pnlDate
            // 
            this.pnlDate.Controls.Add(this.mPlbl3);
            this.pnlDate.Controls.Add(this.mPlbl2);
            this.pnlDate.Controls.Add(this.mPlbl1);
            this.pnlDate.Controls.Add(this.btnGo);
            this.pnlDate.Controls.Add(this.gbAccount);
            this.pnlDate.Controls.Add(this.datetimepickerFrom);
            this.pnlDate.Controls.Add(this.datetimepickerTo);
            this.pnlDate.Controls.Add(this.btnCreateOrder);
            this.pnlDate.Controls.Add(this.txtDate);
            this.pnlDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDate.Location = new System.Drawing.Point(0, 0);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(965, 39);
            this.pnlDate.TabIndex = 1020;
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(256, 8);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(48, 19);
            this.mPlbl3.TabIndex = 1019;
            this.mPlbl3.Text = "F&rom";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(414, 9);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(28, 19);
            this.mPlbl2.TabIndex = 1018;
            this.mPlbl2.Text = "&To";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(787, 9);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(47, 19);
            this.mPlbl1.TabIndex = 1017;
            this.mPlbl1.Text = "Date ";
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.LawnGreen;
            this.btnGo.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(559, 4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(57, 29);
            this.btnGo.TabIndex = 1016;
            this.btnGo.Text = "View";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            this.btnGo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGo_KeyDown);
            // 
            // gbAccount
            // 
            this.gbAccount.Controls.Add(this.rbtSecond);
            this.gbAccount.Controls.Add(this.rbtFirst);
            this.gbAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbAccount.Location = new System.Drawing.Point(5, 2);
            this.gbAccount.Name = "gbAccount";
            this.gbAccount.Size = new System.Drawing.Size(242, 30);
            this.gbAccount.TabIndex = 1015;
            this.gbAccount.TabStop = false;
            // 
            // rbtSecond
            // 
            this.rbtSecond.AutoSize = true;
            this.rbtSecond.BackColor = System.Drawing.Color.PapayaWhip;
            this.rbtSecond.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSecond.Location = new System.Drawing.Point(118, 8);
            this.rbtSecond.Name = "rbtSecond";
            this.rbtSecond.Size = new System.Drawing.Size(121, 19);
            this.rbtSecond.TabIndex = 1;
            this.rbtSecond.TabStop = true;
            this.rbtSecond.Text = "Second Creditor";
            this.rbtSecond.UseVisualStyleBackColor = false;
            // 
            // rbtFirst
            // 
            this.rbtFirst.AutoSize = true;
            this.rbtFirst.BackColor = System.Drawing.Color.PapayaWhip;
            this.rbtFirst.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtFirst.Location = new System.Drawing.Point(6, 7);
            this.rbtFirst.Name = "rbtFirst";
            this.rbtFirst.Size = new System.Drawing.Size(106, 19);
            this.rbtFirst.TabIndex = 0;
            this.rbtFirst.TabStop = true;
            this.rbtFirst.Text = "First Creditor";
            this.rbtFirst.UseVisualStyleBackColor = false;
            this.rbtFirst.CheckedChanged += new System.EventHandler(this.rbtFirst_CheckedChanged);
            // 
            // datetimepickerFrom
            // 
            this.datetimepickerFrom.CustomFormat = "dd/MM/yy";
            this.datetimepickerFrom.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datetimepickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetimepickerFrom.Location = new System.Drawing.Point(307, 6);
            this.datetimepickerFrom.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datetimepickerFrom.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datetimepickerFrom.Name = "datetimepickerFrom";
            this.datetimepickerFrom.Size = new System.Drawing.Size(102, 23);
            this.datetimepickerFrom.TabIndex = 132;
            this.datetimepickerFrom.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.datetimepickerFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datetimepickerFrom_KeyDown);
            // 
            // datetimepickerTo
            // 
            this.datetimepickerTo.CustomFormat = "dd/MM/yy";
            this.datetimepickerTo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datetimepickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetimepickerTo.Location = new System.Drawing.Point(446, 6);
            this.datetimepickerTo.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datetimepickerTo.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datetimepickerTo.Name = "datetimepickerTo";
            this.datetimepickerTo.Size = new System.Drawing.Size(102, 23);
            this.datetimepickerTo.TabIndex = 131;
            this.datetimepickerTo.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.datetimepickerTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datetimepickerTo_KeyDown);
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnCreateOrder.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateOrder.Location = new System.Drawing.Point(654, 5);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(120, 29);
            this.btnCreateOrder.TabIndex = 2;
            this.btnCreateOrder.Text = "Create &Orders";
            this.btnCreateOrder.UseVisualStyleBackColor = false;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            this.btnCreateOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCreateOrder_KeyDown);
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.SeaShell;
            this.txtDate.Enabled = false;
            this.txtDate.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Location = new System.Drawing.Point(834, 6);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(112, 25);
            this.txtDate.TabIndex = 1;
            // 
            // mpMainSubViewControl1
            // 
            this.mpMainSubViewControl1.AutoScroll = true;
            this.mpMainSubViewControl1.BackColor = System.Drawing.Color.Linen;
            this.mpMainSubViewControl1.DataSource = null;
            this.mpMainSubViewControl1.DataSourceMain = null;
            this.mpMainSubViewControl1.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.DateColumnNames")));
            this.mpMainSubViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpMainSubViewControl1.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.DoubleColumnNames")));
            this.mpMainSubViewControl1.Filter = null;
            this.mpMainSubViewControl1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMainSubViewControl1.IsAllowDelete = true;
            this.mpMainSubViewControl1.IsAllowNewRow = false;
            this.mpMainSubViewControl1.Location = new System.Drawing.Point(0, 39);
            this.mpMainSubViewControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mpMainSubViewControl1.MinimumSize = new System.Drawing.Size(488, 321);
            this.mpMainSubViewControl1.Name = "mpMainSubViewControl1";
            this.mpMainSubViewControl1.NextRowColumn = 0;
            this.mpMainSubViewControl1.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.NumericColumnNames")));
            this.mpMainSubViewControl1.Size = new System.Drawing.Size(965, 538);
            this.mpMainSubViewControl1.SubGridWidth = 450;
            this.mpMainSubViewControl1.TabIndex = 1023;
            this.mpMainSubViewControl1.ViewControl = null;
            // 
            // txtNoofOrders
            // 
            this.txtNoofOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoofOrders.CausesValidation = false;
            this.txtNoofOrders.Location = new System.Drawing.Point(197, 398);
            this.txtNoofOrders.Name = "txtNoofOrders";
            this.txtNoofOrders.Size = new System.Drawing.Size(56, 28);
            this.txtNoofOrders.TabIndex = 1016;
            this.txtNoofOrders.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.CausesValidation = false;
            this.txtAmount.Location = new System.Drawing.Point(336, 398);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(132, 28);
            this.txtAmount.TabIndex = 1017;
            this.txtAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UclDailyPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclDailyPurchaseOrder";
            this.Size = new System.Drawing.Size(967, 654);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.pnlDate.ResumeLayout(false);
            this.pnlDate.PerformLayout();
            this.gbAccount.ResumeLayout(false);
            this.gbAccount.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlSummary;
        private PharmaSYSPlus.CommonLibrary.MDataGridView mdgOrderDetail;
        private PharmaSYSPlus.CommonLibrary.MDataGridView mdgOrderSummary;
        private System.Windows.Forms.Button btnExitSummary;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel pnlDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.GroupBox gbAccount;
        private System.Windows.Forms.RadioButton rbtSecond;
        private System.Windows.Forms.RadioButton rbtFirst;
        private System.Windows.Forms.DateTimePicker datetimepickerFrom;
        private System.Windows.Forms.DateTimePicker datetimepickerTo;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.TextBox txtDate;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl mpMainSubViewControl1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtAmount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtNoofOrders;
    }
}
