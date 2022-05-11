namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclPurchaseOrderForToday
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclPurchaseOrderForToday));
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
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.txtAmount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtNoofOrders = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.label21 = new System.Windows.Forms.Label();
            this.btnExitSummary = new System.Windows.Forms.Button();
            this.mdgOrderDetail = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.mdgOrderSummary = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.mpMainSubViewControl1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.gbAccount.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(952, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 650);
            this.MMBottomPanel.Size = new System.Drawing.Size(954, 23);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlSummary);
            this.MMMainPanel.Controls.Add(this.mpMainSubViewControl1);
            this.MMMainPanel.Controls.Add(this.pnlDate);
            this.MMMainPanel.Size = new System.Drawing.Size(954, 598);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlDate, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMainSubViewControl1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlSummary, 0);
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
            this.pnlDate.Size = new System.Drawing.Size(952, 39);
            this.pnlDate.TabIndex = 1021;
            // 
            // mPlbl3
            // 
            this.mPlbl3.AutoSize = true;
            this.mPlbl3.Location = new System.Drawing.Point(267, 8);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(48, 19);
            this.mPlbl3.TabIndex = 1019;
            this.mPlbl3.Text = "F&rom";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(438, 9);
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
            this.btnGo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(587, 4);
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
            this.gbAccount.Controls.Add(this.rbtFirst);
            this.gbAccount.Controls.Add(this.rbtSecond);
            this.gbAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbAccount.Location = new System.Drawing.Point(5, 2);
            this.gbAccount.Name = "gbAccount";
            this.gbAccount.Size = new System.Drawing.Size(141, 30);
            this.gbAccount.TabIndex = 1015;
            this.gbAccount.TabStop = false;
            // 
            // rbtSecond
            // 
            this.rbtSecond.AutoSize = true;
            this.rbtSecond.BackColor = System.Drawing.Color.PapayaWhip;
            this.rbtSecond.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSecond.Location = new System.Drawing.Point(143, 6);
            this.rbtSecond.Name = "rbtSecond";
            this.rbtSecond.Size = new System.Drawing.Size(156, 22);
            this.rbtSecond.TabIndex = 1;
            this.rbtSecond.TabStop = true;
            this.rbtSecond.Text = "Second Creditor";
            this.rbtSecond.UseVisualStyleBackColor = false;
            this.rbtSecond.Visible = false;
            // 
            // rbtFirst
            // 
            this.rbtFirst.AutoSize = true;
            this.rbtFirst.BackColor = System.Drawing.Color.PapayaWhip;
            this.rbtFirst.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtFirst.Location = new System.Drawing.Point(3, 7);
            this.rbtFirst.Name = "rbtFirst";
            this.rbtFirst.Size = new System.Drawing.Size(134, 22);
            this.rbtFirst.TabIndex = 0;
            this.rbtFirst.TabStop = true;
            this.rbtFirst.Text = "First Creditor";
            this.rbtFirst.UseVisualStyleBackColor = false;
            this.rbtFirst.CheckedChanged += new System.EventHandler(this.rbtFirst_CheckedChanged);
            // 
            // datetimepickerFrom
            // 
            this.datetimepickerFrom.CustomFormat = "dd/MM/yy";
            this.datetimepickerFrom.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datetimepickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetimepickerFrom.Location = new System.Drawing.Point(321, 6);
            this.datetimepickerFrom.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datetimepickerFrom.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datetimepickerFrom.Name = "datetimepickerFrom";
            this.datetimepickerFrom.Size = new System.Drawing.Size(113, 26);
            this.datetimepickerFrom.TabIndex = 132;
            this.datetimepickerFrom.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.datetimepickerFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datetimepickerFrom_KeyDown);
            // 
            // datetimepickerTo
            // 
            this.datetimepickerTo.CustomFormat = "dd/MM/yy";
            this.datetimepickerTo.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datetimepickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetimepickerTo.Location = new System.Drawing.Point(470, 6);
            this.datetimepickerTo.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datetimepickerTo.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datetimepickerTo.Name = "datetimepickerTo";
            this.datetimepickerTo.Size = new System.Drawing.Size(113, 26);
            this.datetimepickerTo.TabIndex = 131;
            this.datetimepickerTo.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnCreateOrder.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateOrder.Location = new System.Drawing.Point(660, 5);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(120, 29);
            this.btnCreateOrder.TabIndex = 2;
            this.btnCreateOrder.Text = "Create &Orders";
            this.btnCreateOrder.UseVisualStyleBackColor = false;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
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
            this.pnlSummary.Location = new System.Drawing.Point(223, 157);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(646, 432);
            this.pnlSummary.TabIndex = 1024;
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.CausesValidation = false;
            this.txtAmount.Location = new System.Drawing.Point(336, 398);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(132, 28);
            this.txtAmount.TabIndex = 1017;
            this.txtAmount.Text = "label";
            this.txtAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNoofOrders
            // 
            this.txtNoofOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoofOrders.CausesValidation = false;
            this.txtNoofOrders.Location = new System.Drawing.Point(197, 398);
            this.txtNoofOrders.Name = "txtNoofOrders";
            this.txtNoofOrders.Size = new System.Drawing.Size(56, 28);
            this.txtNoofOrders.TabIndex = 1016;
            this.txtNoofOrders.Text = "label";
            this.txtNoofOrders.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.mpMainSubViewControl1.Size = new System.Drawing.Size(952, 557);
            this.mpMainSubViewControl1.SubGridWidth = 450;
            this.mpMainSubViewControl1.TabIndex = 1025;
            this.mpMainSubViewControl1.ViewControl = null;
            // 
            // UclPurchaseOrderForToday
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclPurchaseOrderForToday";
            this.Size = new System.Drawing.Size(954, 673);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlDate.ResumeLayout(false);
            this.pnlDate.PerformLayout();
            this.gbAccount.ResumeLayout(false);
            this.gbAccount.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.Panel pnlSummary;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtAmount;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtNoofOrders;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnExitSummary;
        private PharmaSYSPlus.CommonLibrary.MDataGridView mdgOrderDetail;
        private PharmaSYSPlus.CommonLibrary.MDataGridView mdgOrderSummary;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMainSubViewControl mpMainSubViewControl1;
    }
}
