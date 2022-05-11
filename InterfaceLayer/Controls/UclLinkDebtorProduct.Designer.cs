namespace EcoMart.InterfaceLayer
{
    partial class UclDebtorProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclDebtorProduct));
            this.mcbCreditor = new EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.txtNoOfRows = new PharmaSYSPlus.CommonLibrary.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.mpMSVC1 = new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.ttDebtorProduct = new System.Windows.Forms.ToolTip(this.components);
            this.mPlbl1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.pnlList = new System.Windows.Forms.Panel();
            this.dgvLowerListY = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.dgvUpperListY = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.btnReverse = new System.Windows.Forms.Button();
            this.dgvLowerList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.dgvUpperList = new PharmaSYSPlus.CommonLibrary.MDataGridView();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlList.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(894, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Controls.Add(this.label6);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 596);
            this.MMBottomPanel.Size = new System.Drawing.Size(896, 63);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblRightSideFooterMsg, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.label6, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlList);
            this.MMMainPanel.Controls.Add(this.btnViewAll);
            this.MMMainPanel.Controls.Add(this.mPlbl1);
            this.MMMainPanel.Controls.Add(this.mpMSVC1);
            this.MMMainPanel.Controls.Add(this.mcbCreditor);
            this.MMMainPanel.Size = new System.Drawing.Size(896, 533);
            this.MMMainPanel.Controls.SetChildIndex(this.mcbCreditor, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mpMSVC1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.mPlbl1, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.btnViewAll, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.ctrlUclSaleSummaryControl, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlList, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(428, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 20);
            // 
            // ctrlUclSaleSummaryControl
            // 
            this.ctrlUclSaleSummaryControl.Location = new System.Drawing.Point(-1, -1);
            // 
            // mcbCreditor
            // 
            this.mcbCreditor.ColumnWidth = null;
            this.mcbCreditor.DataSource = null;
            this.mcbCreditor.DisplayColumnNo = 1;
            this.mcbCreditor.DropDownHeight = 200;
            this.mcbCreditor.Location = new System.Drawing.Point(239, 33);
            this.mcbCreditor.Margin = new System.Windows.Forms.Padding(4);
            this.mcbCreditor.Name = "mcbCreditor";
            this.mcbCreditor.SelectedID = "";
            this.mcbCreditor.ShowNew = false;
            this.mcbCreditor.Size = new System.Drawing.Size(363, 22);
            this.mcbCreditor.SourceDataString = null;
            this.mcbCreditor.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbCreditor.TabIndex = 145;
            this.mcbCreditor.UserControlToShow = null;
            this.mcbCreditor.ValueColumnNo = 0;
            this.mcbCreditor.SeletectIndexChanged += new System.EventHandler(this.mcbCreditor_SeletectIndexChanged);
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.Enabled = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(695, -1);
            this.txtNoOfRows.MaxLength = 5;
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(53, 22);
            this.txtNoOfRows.TabIndex = 1011;
            this.txtNoOfRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(606, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 15);
            this.label6.TabIndex = 1010;
            this.label6.Text = "No Of Rows";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 2);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(2, 16);
            this.lblMessage.TabIndex = 1009;
            // 
            // mpMSVC1
            // 
            this.mpMSVC1.AutoScroll = true;
            this.mpMSVC1.BackColor = System.Drawing.Color.Linen;
            this.mpMSVC1.DataSource = null;
            this.mpMSVC1.DataSourceMain = null;
            this.mpMSVC1.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC1.DateColumnNames")));
            this.mpMSVC1.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC1.DoubleColumnNames")));
            this.mpMSVC1.EditedTempDataList = null;
            this.mpMSVC1.Filter = null;
            this.mpMSVC1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMSVC1.IsAllowDelete = true;
            this.mpMSVC1.IsAllowNewRow = true;
            this.mpMSVC1.Location = new System.Drawing.Point(139, 76);
            this.mpMSVC1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpMSVC1.MinimumSize = new System.Drawing.Size(390, 250);
            this.mpMSVC1.Name = "mpMSVC1";
            this.mpMSVC1.NextRowColumn = 0;
            this.mpMSVC1.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMSVC1.NumericColumnNames")));
            this.mpMSVC1.Size = new System.Drawing.Size(640, 359);
            this.mpMSVC1.SubGridWidth = 400;
            this.mpMSVC1.TabIndex = 143;
            this.mpMSVC1.ViewControl = null;
            this.mpMSVC1.OnCellValueChangeCommited += new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl.CellValueChangeCommited(this.mpmsvc1_OnCellValueChangeCommited);
            this.mpMSVC1.OnDetailsFilled += new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl.DetailsFilled(this.mpmsvc1_OnDetailsFilled);
            this.mpMSVC1.OnRowDeleted += new System.EventHandler(this.mpmsvc1_OnRowDeleted);
            // 
            // ttDebtorProduct
            // 
            this.ttDebtorProduct.AutomaticDelay = 20;
            this.ttDebtorProduct.AutoPopDelay = 200;
            this.ttDebtorProduct.InitialDelay = 0;
            this.ttDebtorProduct.ReshowDelay = 0;
            // 
            // mPlbl1
            // 
            this.mPlbl1.AutoSize = true;
            this.mPlbl1.Location = new System.Drawing.Point(128, 35);
            this.mPlbl1.Name = "mPlbl1";
            this.mPlbl1.Size = new System.Drawing.Size(89, 16);
            this.mPlbl1.TabIndex = 146;
            this.mPlbl1.Text = "&Debtor Name";
            // 
            // btnViewAll
            // 
            this.btnViewAll.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewAll.Location = new System.Drawing.Point(750, 30);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(71, 31);
            this.btnViewAll.TabIndex = 147;
            this.btnViewAll.Text = "V&iewAll";
            this.btnViewAll.UseVisualStyleBackColor = true;
            this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);
            // 
            // pnlList
            // 
            this.pnlList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlList.Controls.Add(this.dgvUpperList);
            this.pnlList.Controls.Add(this.dgvLowerListY);
            this.pnlList.Controls.Add(this.btnReverse);
            this.pnlList.Controls.Add(this.dgvLowerList);
            this.pnlList.Controls.Add(this.dgvUpperListY);
            this.pnlList.Location = new System.Drawing.Point(116, 30);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(705, 526);
            this.pnlList.TabIndex = 1017;
            this.pnlList.Visible = false;
            // 
            // dgvLowerListY
            // 
            this.dgvLowerListY.BackColor = System.Drawing.Color.Transparent;
            this.dgvLowerListY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvLowerListY.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerListY.DateColumnNames")));
            this.dgvLowerListY.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerListY.DoubleColumnNames")));
            this.dgvLowerListY.Filter = null;
            this.dgvLowerListY.Location = new System.Drawing.Point(15, 236);
            this.dgvLowerListY.Name = "dgvLowerListY";
            this.dgvLowerListY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvLowerListY.ShowGridFilter = false;
            this.dgvLowerListY.Size = new System.Drawing.Size(649, 287);
            this.dgvLowerListY.TabIndex = 54;
            // 
            // dgvUpperListY
            // 
            this.dgvUpperListY.BackColor = System.Drawing.Color.Transparent;
            this.dgvUpperListY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvUpperListY.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperListY.DateColumnNames")));
            this.dgvUpperListY.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperListY.DoubleColumnNames")));
            this.dgvUpperListY.Filter = null;
            this.dgvUpperListY.Location = new System.Drawing.Point(16, 1);
            this.dgvUpperListY.Name = "dgvUpperListY";
            this.dgvUpperListY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvUpperListY.ShowGridFilter = false;
            this.dgvUpperListY.Size = new System.Drawing.Size(649, 219);
            this.dgvUpperListY.TabIndex = 53;
            this.dgvUpperListY.SelectedRowChanged += new System.EventHandler(this.dgvUpperListY_SelectedRowChanged);
            // 
            // btnReverse
            // 
            this.btnReverse.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReverse.Location = new System.Drawing.Point(673, 3);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(22, 116);
            this.btnReverse.TabIndex = 52;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // dgvLowerList
            // 
            this.dgvLowerList.BackColor = System.Drawing.Color.Transparent;
            this.dgvLowerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvLowerList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerList.DateColumnNames")));
            this.dgvLowerList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvLowerList.DoubleColumnNames")));
            this.dgvLowerList.Filter = null;
            this.dgvLowerList.Location = new System.Drawing.Point(15, 236);
            this.dgvLowerList.Name = "dgvLowerList";
            this.dgvLowerList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvLowerList.ShowGridFilter = false;
            this.dgvLowerList.Size = new System.Drawing.Size(649, 287);
            this.dgvLowerList.TabIndex = 29;
            // 
            // dgvUpperList
            // 
            this.dgvUpperList.BackColor = System.Drawing.Color.Transparent;
            this.dgvUpperList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvUpperList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperList.DateColumnNames")));
            this.dgvUpperList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvUpperList.DoubleColumnNames")));
            this.dgvUpperList.Filter = null;
            this.dgvUpperList.Location = new System.Drawing.Point(15, 0);
            this.dgvUpperList.Name = "dgvUpperList";
            this.dgvUpperList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvUpperList.ShowGridFilter = false;
            this.dgvUpperList.Size = new System.Drawing.Size(649, 219);
            this.dgvUpperList.TabIndex = 27;
            this.dgvUpperList.SelectedRowChanged += new System.EventHandler(this.dgvUpperList_SelectedRowChanged);
            // 
            // UclDebtorProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Name = "UclDebtorProduct";
            this.Size = new System.Drawing.Size(896, 659);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PharmaSYSPlus.CommonLibrary.NumericTextBox txtNoOfRows;
        private System.Windows.Forms.Label label6;
        private EcoMart.InterfaceLayer.CommonControls.PSComboBoxNew mcbCreditor;
        private System.Windows.Forms.Label lblMessage;
        private EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl mpMSVC1;
        private System.Windows.Forms.ToolTip ttDebtorProduct;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel mPlbl1;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.Panel pnlList;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvLowerListY;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvUpperListY;
        private System.Windows.Forms.Button btnReverse;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvLowerList;
        private PharmaSYSPlus.CommonLibrary.MDataGridView dgvUpperList;
    }
}
