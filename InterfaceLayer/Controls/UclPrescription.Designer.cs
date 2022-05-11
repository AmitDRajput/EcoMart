using PharmaSYSPlus.CommonLibrary;
namespace EcoMart.InterfaceLayer
{
    partial class UclPrescription
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UclPrescription));
            this.label6 = new System.Windows.Forms.Label();
            this.ttPrescription = new System.Windows.Forms.ToolTip(this.components);
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.lblName = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtName = new EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.mpMainSubViewControl1 = new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtNoOfRows = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(941, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Controls.Add(this.txtNoOfRows);
            this.MMBottomPanel.Controls.Add(this.lblMessage);
            this.MMBottomPanel.Controls.Add(this.label6);
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 523);
            this.MMBottomPanel.Size = new System.Drawing.Size(943, 63);
            this.MMBottomPanel.Controls.SetChildIndex(this.label6, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.txtNoOfRows, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblFooterMessage, 0);
            this.MMBottomPanel.Controls.SetChildIndex(this.lblRightSideFooterMsg, 0);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlCenter);
            this.MMMainPanel.Size = new System.Drawing.Size(943, 460);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlCenter, 0);
            // 
            // lblRightSideFooterMsg
            // 
            this.lblRightSideFooterMsg.Location = new System.Drawing.Point(475, 0);
            this.lblRightSideFooterMsg.Size = new System.Drawing.Size(466, 20);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(522, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 17);
            this.label6.TabIndex = 1011;
            this.label6.Text = "No Of Rows";
            // 
            // ttPrescription
            // 
            this.ttPrescription.AutomaticDelay = 200;
            this.ttPrescription.AutoPopDelay = 2000;
            this.ttPrescription.InitialDelay = 10;
            this.ttPrescription.ReshowDelay = 10;
            // 
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCenter.Controls.Add(this.lblName);
            this.pnlCenter.Controls.Add(this.txtName);
            this.pnlCenter.Controls.Add(this.mpMainSubViewControl1);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 0);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(941, 458);
            this.pnlCenter.TabIndex = 51;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(62, 64);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 16);
            this.lblName.TabIndex = 146;
            this.lblName.Text = "&Name";
            // 
            // txtName
            // 
            this.txtName.AlphabeticalList = false;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.ColumnWidth = null;
            this.txtName.DataSource = null;
            this.txtName.DisplayColumnNo = 1;
            this.txtName.DropDownHeight = 200;
            this.txtName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(122, 64);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = false;
            this.txtName.SelectedID = null;
            this.txtName.Size = new System.Drawing.Size(385, 22);
            this.txtName.SourceDataString = null;
            this.txtName.TabIndex = 145;
            this.txtName.UserControlToShow = null;
            this.txtName.ValueColumnNo = 0;
            this.txtName.EnterKeyPressed += new System.EventHandler(this.txtName_EnterKeyPressed);
            // 
            // mpMainSubViewControl1
            // 
            this.mpMainSubViewControl1.AutoScroll = true;
            this.mpMainSubViewControl1.DataSource = null;
            this.mpMainSubViewControl1.DataSourceMain = null;
            this.mpMainSubViewControl1.DateColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.DateColumnNames")));
            this.mpMainSubViewControl1.DoubleColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.DoubleColumnNames")));
            this.mpMainSubViewControl1.EditedTempDataList = null;
            this.mpMainSubViewControl1.Filter = null;
            this.mpMainSubViewControl1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpMainSubViewControl1.IsAllowDelete = true;
            this.mpMainSubViewControl1.IsAllowNewRow = true;
            this.mpMainSubViewControl1.Location = new System.Drawing.Point(72, 98);
            this.mpMainSubViewControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mpMainSubViewControl1.MinimumSize = new System.Drawing.Size(390, 289);
            this.mpMainSubViewControl1.Name = "mpMainSubViewControl1";
            this.mpMainSubViewControl1.NextRowColumn = 0;
            this.mpMainSubViewControl1.NumericColumnNames = ((System.Collections.ArrayList)(resources.GetObject("mpMainSubViewControl1.NumericColumnNames")));
            this.mpMainSubViewControl1.Size = new System.Drawing.Size(842, 289);
            this.mpMainSubViewControl1.SubGridWidth = 540;
            this.mpMainSubViewControl1.TabIndex = 45;
            this.mpMainSubViewControl1.ViewControl = null;
            this.mpMainSubViewControl1.OnCellValueChangeCommited += new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl.CellValueChangeCommited(this.mpMainSubViewControl1_OnCellValueChangeCommited);
            this.mpMainSubViewControl1.OnDetailsFilled += new EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl.DetailsFilled(this.mpMainSubViewControl1_OnDetailsFilled);
            this.mpMainSubViewControl1.OnRowDeleted += new System.EventHandler(this.mpMainSubViewControl1_OnRowDeleted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(106, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 18);
            this.label2.TabIndex = 47;
            this.label2.Text = "*";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Yellow;
            this.lblMessage.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(15, 2);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 15);
            this.lblMessage.TabIndex = 1013;
            // 
            // txtNoOfRows
            // 
            this.txtNoOfRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoOfRows.CausesValidation = false;
            this.txtNoOfRows.Location = new System.Drawing.Point(616, 0);
            this.txtNoOfRows.Name = "txtNoOfRows";
            this.txtNoOfRows.Size = new System.Drawing.Size(72, 23);
            this.txtNoOfRows.TabIndex = 1014;
            this.txtNoOfRows.Text = "label";
            this.txtNoOfRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UclPrescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.Name = "UclPrescription";
            this.Size = new System.Drawing.Size(943, 586);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            this.pnlCenter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip ttPrescription;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Label label2;
        private EcoMart.InterfaceLayer.CommonControls.PSMainSubViewControl mpMainSubViewControl1;
        private EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtName;
        private System.Windows.Forms.Label lblMessage;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblName;
        private EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight txtNoOfRows;
    }
}
