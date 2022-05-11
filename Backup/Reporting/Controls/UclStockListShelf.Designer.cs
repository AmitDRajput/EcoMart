namespace PharmaSYSRetailPlus.Reporting.Controls
{
    partial class UclStockListShelf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclStockListShelf));
            this.mcbShelf = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlGo = new System.Windows.Forms.Panel();
            this.psLabel3 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.txtViewText = new PharmaSYSPlus.CommonLibrary.GeneralTextBox();
            this.dgvReportList = new PharmaSYSPlus.CommonLibrary.MReportGridView();
            this.pnlMultiSelection1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection(this.components);
            this.psLabel1 = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel();
            this.btnOKMultiSelection = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.txtReportTotalAmount = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlMultiSelection1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(979, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtReportTotalAmount);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 529);
            this.MMBottomPanel.Size = new System.Drawing.Size(981, 23);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtReportTotalAmount, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlMultiSelection1);
            this.MMMainPanel.Controls.Add(this.dgvReportList);
            this.MMMainPanel.Controls.Add(this.pnlGo);
            this.MMMainPanel.Size = new System.Drawing.Size(981, 477);
            // 
            // mcbShelf
            // 
            this.mcbShelf.ColumnWidth = null;
            this.mcbShelf.DataSource = null;
            this.mcbShelf.DisplayColumnNo = 1;
            this.mcbShelf.DropDownHeight = 200;
            this.mcbShelf.Location = new System.Drawing.Point(105, 24);
            this.mcbShelf.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mcbShelf.Name = "mcbShelf";
            this.mcbShelf.SelectedID = null;
            this.mcbShelf.ShowNew = false;
            this.mcbShelf.Size = new System.Drawing.Size(282, 26);
            this.mcbShelf.SourceDataString = null;
            this.mcbShelf.Style = PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.mcbShelf.TabIndex = 1038;
            this.ttToolTip.SetToolTip(this.mcbShelf, "Select Shelf Code ");
            this.mcbShelf.UserControlToShow = null;
            this.mcbShelf.ValueColumnNo = 0;
            this.mcbShelf.EnterKeyPressed += new System.EventHandler(this.mcbShelf_EnterKeyPressed);
            // 
            // ttToolTip
            // 
            this.ttToolTip.ShowAlways = true;
            // 
            // pnlGo
            // 
            this.pnlGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGo.Controls.Add(this.psLabel3);
            this.pnlGo.Controls.Add(this.txtViewText);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGo.Location = new System.Drawing.Point(0, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(979, 33);
            this.pnlGo.TabIndex = 1052;
            // 
            // psLabel3
            // 
            this.psLabel3.AutoSize = true;
            this.psLabel3.Location = new System.Drawing.Point(72, 6);
            this.psLabel3.Name = "psLabel3";
            this.psLabel3.Size = new System.Drawing.Size(45, 19);
            this.psLabel3.TabIndex = 1071;
            this.psLabel3.Text = "Shelf";
            // 
            // txtViewText
            // 
            this.txtViewText.BackColor = System.Drawing.Color.Snow;
            this.txtViewText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViewText.Location = new System.Drawing.Point(124, 3);
            this.txtViewText.MaxLength = 50;
            this.txtViewText.Name = "txtViewText";
            this.txtViewText.Size = new System.Drawing.Size(310, 26);
            this.txtViewText.TabIndex = 1070;
            this.txtViewText.TabStop = false;
            this.txtViewText.TextTransform = PharmaSYSPlus.CommonLibrary.GeneralTextBox.TextTransformation.Default;
            // 
            // dgvReportList
            // 
            this.dgvReportList.ApplyAlternateRowStyle = false;
            this.dgvReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvReportList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvReportList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvReportList.ConvertDatetoMonth = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.ConvertDatetoMonth")));
            this.dgvReportList.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DateColumnNames")));
            this.dgvReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportList.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.DoubleColumnNames")));
            this.dgvReportList.Location = new System.Drawing.Point(0, 33);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.OptionalColumnNames = ((System.Collections.ArrayList)(resources.GetObject("dgvReportList.OptionalColumnNames")));
            this.dgvReportList.Size = new System.Drawing.Size(979, 442);
            this.dgvReportList.TabIndex = 1053;
            // 
            // pnlMultiSelection1
            // 
            this.pnlMultiSelection1.BackColor = System.Drawing.Color.White;
            this.pnlMultiSelection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMultiSelection1.Controls.Add(this.psLabel1);
            this.pnlMultiSelection1.Controls.Add(this.btnOKMultiSelection);
            this.pnlMultiSelection1.Controls.Add(this.mcbShelf);
            this.pnlMultiSelection1.Location = new System.Drawing.Point(287, 153);
            this.pnlMultiSelection1.Name = "pnlMultiSelection1";
            this.pnlMultiSelection1.Size = new System.Drawing.Size(477, 76);
            this.pnlMultiSelection1.TabIndex = 1054;
            // 
            // psLabel1
            // 
            this.psLabel1.AutoSize = true;
            this.psLabel1.Location = new System.Drawing.Point(46, 27);
            this.psLabel1.Name = "psLabel1";
            this.psLabel1.Size = new System.Drawing.Size(45, 19);
            this.psLabel1.TabIndex = 1072;
            this.psLabel1.Text = "Shelf";
            // 
            // btnOKMultiSelection
            // 
            this.btnOKMultiSelection.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection.BackgroundImage")));
            this.btnOKMultiSelection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelection.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelection.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelection.Image")));
            this.btnOKMultiSelection.Location = new System.Drawing.Point(409, 3);
            this.btnOKMultiSelection.Name = "btnOKMultiSelection";
            this.btnOKMultiSelection.Size = new System.Drawing.Size(63, 63);
            this.btnOKMultiSelection.TabIndex = 1040;
            this.btnOKMultiSelection.Text = "Go";
            this.btnOKMultiSelection.UseVisualStyleBackColor = true;
            this.btnOKMultiSelection.Click += new System.EventHandler(this.btnOKMultiSelection_Click);
            // 
            // txtReportTotalAmount
            // 
            this.txtReportTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportTotalAmount.CausesValidation = false;
            this.txtReportTotalAmount.Location = new System.Drawing.Point(799, -1);
            this.txtReportTotalAmount.Name = "txtReportTotalAmount";
            this.txtReportTotalAmount.Size = new System.Drawing.Size(149, 23);
            this.txtReportTotalAmount.TabIndex = 1;
            this.txtReportTotalAmount.Text = "label";
            this.txtReportTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UclStockListShelf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclStockListShelf";
            this.Size = new System.Drawing.Size(981, 552);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.pnlGo.PerformLayout();
            this.pnlMultiSelection1.ResumeLayout(false);
            this.pnlMultiSelection1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSComboBoxNew mcbShelf;

        private System.Windows.Forms.ToolTip ttToolTip;
        private PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList;
        private System.Windows.Forms.Panel pnlGo;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSpnlMultiSelection pnlMultiSelection1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelection;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel1;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLabel psLabel3;
        private PharmaSYSPlus.CommonLibrary.GeneralTextBox txtViewText;
        private PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtReportTotalAmount;
    }
}
