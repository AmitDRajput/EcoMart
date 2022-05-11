namespace EcoMart.InterfaceLayer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.mPlbl3 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnGo = new System.Windows.Forms.Button();
            this.datetimepickerFrom = new System.Windows.Forms.DateTimePicker();
            this.datetimepickerTo = new System.Windows.Forms.DateTimePicker();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.btnUpLoad = new System.Windows.Forms.Button();
            this.txtAmount = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtNoofOrders = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.label21 = new System.Windows.Forms.Label();
            this.btnExitSummary = new System.Windows.Forms.Button();
            this.mdgOrderDetail = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.mdgOrderSummary = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.mpMainSubViewControl1 = new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.dgvBatchGrid = new System.Windows.Forms.DataGridView();
            this.uclPurchaseNewProduct1 = new EcoMart.InterfaceLayer.CommonControls.UclPurchaseNewProduct();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Margin = new System.Windows.Forms.Padding(7);
            this.headerLabel1.Size = new System.Drawing.Size(952, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 610);
            this.MMBottomPanel.Margin = new System.Windows.Forms.Padding(6);
            this.MMBottomPanel.Size = new System.Drawing.Size(954, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlSummary);
            this.MMMainPanel.Controls.Add(this.mpMainSubViewControl1);
            this.MMMainPanel.Controls.Add(this.uclPurchaseNewProduct1);
            this.MMMainPanel.Controls.Add(this.pnlDate);
            this.MMMainPanel.Controls.Add(this.dgvBatchGrid);
            this.MMMainPanel.Margin = new System.Windows.Forms.Padding(6);
            this.MMMainPanel.Size = new System.Drawing.Size(954, 547);
            this.MMMainPanel.Controls.SetChildIndex(this.dgvBatchGrid, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlDate, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.uclPurchaseNewProduct1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMainSubViewControl1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlSummary, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(486, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 20);
            // 
            // pnlDate
            // 
            this.pnlDate.BackColor = System.Drawing.Color.LightPink;
            this.pnlDate.Controls.Add(this.mPlbl3);
            this.pnlDate.Controls.Add(this.mPlbl2);
            this.pnlDate.Controls.Add(this.mPlbl1);
            this.pnlDate.Controls.Add(this.btnGo);
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
            this.mPlbl3.Location = new System.Drawing.Point(267, 11);
            this.mPlbl3.Name = "mPlbl3";
            this.mPlbl3.Size = new System.Drawing.Size(41, 16);
            this.mPlbl3.TabIndex = 1019;
            this.mPlbl3.Text = "F&rom";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(438, 11);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(22, 16);
            this.mPlbl2.TabIndex = 1018;
            this.mPlbl2.Text = "&To";
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(789, 11);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(39, 16);
            this.mPlbl1.TabIndex = 1017;
            this.mPlbl1.Text = "Date ";
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.LawnGreen;
            this.btnGo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(594, 2);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(57, 35);
            this.btnGo.TabIndex = 1016;
            this.btnGo.Text = "&View";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            this.btnGo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGo_KeyDown);
            // 
            // datetimepickerFrom
            // 
            this.datetimepickerFrom.CustomFormat = "dd/MM/yy";
            this.datetimepickerFrom.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datetimepickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetimepickerFrom.Location = new System.Drawing.Point(319, 6);
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
            this.datetimepickerTo.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datetimepickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetimepickerTo.Location = new System.Drawing.Point(470, 6);
            this.datetimepickerTo.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.datetimepickerTo.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.datetimepickerTo.Name = "datetimepickerTo";
            this.datetimepickerTo.Size = new System.Drawing.Size(113, 26);
            this.datetimepickerTo.TabIndex = 131;
            this.datetimepickerTo.Value = new System.DateTime(2010, 7, 4, 0, 0, 0, 0);
            this.datetimepickerTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datetimepickerTo_KeyDown);
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
            this.pnlSummary.Controls.Add(this.btnUpLoad);
            this.pnlSummary.Controls.Add(this.txtAmount);
            this.pnlSummary.Controls.Add(this.txtNoofOrders);
            this.pnlSummary.Controls.Add(this.label21);
            this.pnlSummary.Controls.Add(this.btnExitSummary);
            this.pnlSummary.Controls.Add(this.mdgOrderDetail);
            this.pnlSummary.Controls.Add(this.mdgOrderSummary);
            this.pnlSummary.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSummary.Location = new System.Drawing.Point(223, 92);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(646, 392);
            this.pnlSummary.TabIndex = 1024;
            // 
            // btnUpLoad
            // 
            this.btnUpLoad.Location = new System.Drawing.Point(437, 361);
            this.btnUpLoad.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpLoad.Name = "btnUpLoad";
            this.btnUpLoad.Size = new System.Drawing.Size(94, 24);
            this.btnUpLoad.TabIndex = 1021;
            this.btnUpLoad.Text = "UpLoad";
            this.btnUpLoad.UseVisualStyleBackColor = true;
            this.btnUpLoad.Visible = false;
            this.btnUpLoad.Click += new System.EventHandler(this.btnUpLoad_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.CausesValidation = false;
            this.txtAmount.Location = new System.Drawing.Point(286, 357);
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
            this.txtNoofOrders.Location = new System.Drawing.Point(197, 357);
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
            this.label21.Location = new System.Drawing.Point(92, 364);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(99, 17);
            this.label21.TabIndex = 1012;
            this.label21.Text = "No Of  Orders";
            // 
            // btnExitSummary
            // 
            this.btnExitSummary.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitSummary.Location = new System.Drawing.Point(544, 361);
            this.btnExitSummary.Name = "btnExitSummary";
            this.btnExitSummary.Size = new System.Drawing.Size(94, 24);
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
            this.mdgOrderDetail.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mdgOrderDetail.Location = new System.Drawing.Point(11, 165);
            this.mdgOrderDetail.Name = "mdgOrderDetail";
            this.mdgOrderDetail.ShowGridFilter = false;
            this.mdgOrderDetail.Size = new System.Drawing.Size(624, 184);
            this.mdgOrderDetail.TabIndex = 3;
            // 
            // mdgOrderSummary
            // 
            this.mdgOrderSummary.BackColor = System.Drawing.Color.Transparent;
            this.mdgOrderSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mdgOrderSummary.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mdgOrderSummary.DateColumnNames")));
            this.mdgOrderSummary.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mdgOrderSummary.DoubleColumnNames")));
            this.mdgOrderSummary.Filter = null;
            this.mdgOrderSummary.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mdgOrderSummary.Location = new System.Drawing.Point(10, 10);
            this.mdgOrderSummary.Name = "mdgOrderSummary";
            this.mdgOrderSummary.ShowGridFilter = false;
            this.mdgOrderSummary.Size = new System.Drawing.Size(625, 173);
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
            this.mpMainSubViewControl1.EditedTempDataList = null;
            this.mpMainSubViewControl1.Filter = null;
            this.mpMainSubViewControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMainSubViewControl1.IsAllowDelete = true;
            this.mpMainSubViewControl1.IsAllowNewRow = false;
            this.mpMainSubViewControl1.Location = new System.Drawing.Point(0, 39);
            this.mpMainSubViewControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpMainSubViewControl1.MinimumSize = new System.Drawing.Size(390, 250);
            this.mpMainSubViewControl1.Name = "mpMainSubViewControl1";
            this.mpMainSubViewControl1.NextRowColumn = 0;
            this.mpMainSubViewControl1.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.NumericColumnNames")));
            this.mpMainSubViewControl1.Size = new System.Drawing.Size(952, 481);
            this.mpMainSubViewControl1.SubGridWidth = 450;
            this.mpMainSubViewControl1.TabIndex = 1025;
            this.mpMainSubViewControl1.ViewControl = null;
            this.mpMainSubViewControl1.OnTABKeyPressed += new System.EventHandler(this.mpMainSubViewControl1_OnTABKeyPressed);
            // 
            // dgvBatchGrid
            // 
            this.dgvBatchGrid.AllowUserToAddRows = false;
            this.dgvBatchGrid.AllowUserToDeleteRows = false;
            this.dgvBatchGrid.AllowUserToResizeColumns = false;
            this.dgvBatchGrid.AllowUserToResizeRows = false;
            this.dgvBatchGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBatchGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBatchGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBatchGrid.Location = new System.Drawing.Point(233, 62);
            this.dgvBatchGrid.MultiSelect = false;
            this.dgvBatchGrid.Name = "dgvBatchGrid";
            this.dgvBatchGrid.ReadOnly = true;
            this.dgvBatchGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBatchGrid.Size = new System.Drawing.Size(633, 179);
            this.dgvBatchGrid.TabIndex = 1075;
            this.dgvBatchGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvBatchGrid_KeyDown);
            // 
            // uclPurchaseNewProduct1
            // 
            this.uclPurchaseNewProduct1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uclPurchaseNewProduct1.Font = new System.Drawing.Font("Cambria", 8.25F);
            this.uclPurchaseNewProduct1.Location = new System.Drawing.Point(0, 520);
            this.uclPurchaseNewProduct1.MainViewControl = this.mpMainSubViewControl1;
            this.uclPurchaseNewProduct1.Name = "uclPurchaseNewProduct1";
            this.uclPurchaseNewProduct1.Size = new System.Drawing.Size(952, 25);
            this.uclPurchaseNewProduct1.TabIndex = 1026;
            // 
            // UclPurchaseOrderForToday
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "UclPurchaseOrderForToday";
            this.Size = new System.Drawing.Size(954, 673);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlDate.ResumeLayout(false);
            this.pnlDate.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlDate;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl3;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl2;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.DateTimePicker datetimepickerFrom;
        private System.Windows.Forms.DateTimePicker datetimepickerTo;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Panel pnlSummary;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtAmount;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtNoofOrders;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnExitSummary;
        private PharmaSYSPlus.CommonLibrary.MDataGridView mdgOrderDetail;
        private PharmaSYSPlus.CommonLibrary.MDataGridView mdgOrderSummary;
        private EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl mpMainSubViewControl1;
        private System.Windows.Forms.DataGridView dgvBatchGrid;
        private System.Windows.Forms.Button btnUpLoad;
        private CommonControls.UclPurchaseNewProduct uclPurchaseNewProduct1;
    }
}
