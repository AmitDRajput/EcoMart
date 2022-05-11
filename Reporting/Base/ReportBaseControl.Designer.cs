namespace EcoMart.Reporting.Controls
{
    partial class ReportBaseControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportBaseControl));
            this.MMTopPanel = new System.Windows.Forms.Panel();
            this.headerLabel1 = new EcoMart.InterfaceLayer.CommonControls.PSHeaderLabelForReports();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnEMail = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnExit = new System.Windows.Forms.ToolStripButton();
            this.tsBtnfavourite = new System.Windows.Forms.ToolStripButton();
            this.MMBottomPanel = new System.Windows.Forms.Panel();
            this.lblFooterMessage = new EcoMart.InterfaceLayer.CommonControls.PSlblMessage();
            this.MMMainPanel = new System.Windows.Forms.Panel();
            this.pnlFileName = new System.Windows.Forms.Panel();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnOKMultiSelectionForPanel = new EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection();
            this.lblEmailID = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtEmailID = new EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.MMTopPanel.SuspendLayout();
            this.MainToolStrip.SuspendLayout();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlFileName.SuspendLayout();
            this.SuspendLayout();
            // 
            // MMTopPanel
            // 
            this.MMTopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MMTopPanel.Controls.Add(this.headerLabel1);
            this.MMTopPanel.Controls.Add(this.MainToolStrip);
            this.MMTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MMTopPanel.Location = new System.Drawing.Point(0, 0);
            this.MMTopPanel.Name = "MMTopPanel";
            this.MMTopPanel.Size = new System.Drawing.Size(674, 52);
            this.MMTopPanel.TabIndex = 54;
            // 
            // headerLabel1
            // 
            this.headerLabel1.BackColor = System.Drawing.Color.Gold;
            this.headerLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.headerLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.headerLabel1.ForeColor = System.Drawing.Color.DarkViolet;
            this.headerLabel1.Location = new System.Drawing.Point(0, 27);
            this.headerLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.headerLabel1.Name = "headerLabel1";
            this.headerLabel1.Size = new System.Drawing.Size(672, 23);
            this.headerLabel1.TabIndex = 52;
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.AutoSize = false;
            this.MainToolStrip.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MainToolStrip.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnPrint,
            this.toolStripSeparator1,
            this.tsBtnExport,
            this.toolStripSeparator2,
            this.tsBtnEMail,
            this.toolStripSeparator3,
            this.tsBtnExit,
            this.tsBtnfavourite});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(672, 27);
            this.MainToolStrip.TabIndex = 51;
            this.MainToolStrip.Text = "Button Panel";
            // 
            // tsbtnPrint
            // 
            this.tsbtnPrint.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F);
            this.tsbtnPrint.Name = "tsbtnPrint";
            this.tsbtnPrint.Size = new System.Drawing.Size(43, 24);
            this.tsbtnPrint.Text = "Print";
            this.tsbtnPrint.Click += new System.EventHandler(this.tsbtnPrint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsBtnExport
            // 
            this.tsBtnExport.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsBtnExport.Name = "tsBtnExport";
            this.tsBtnExport.Size = new System.Drawing.Size(54, 24);
            this.tsBtnExport.Text = "Export";
            this.tsBtnExport.Click += new System.EventHandler(this.tsBtnExport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tsBtnEMail
            // 
            this.tsBtnEMail.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F);
            this.tsBtnEMail.Name = "tsBtnEMail";
            this.tsBtnEMail.Size = new System.Drawing.Size(52, 24);
            this.tsBtnEMail.Text = "E-mail";
            this.tsBtnEMail.Click += new System.EventHandler(this.tsBtnEMail_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // tsBtnExit
            // 
            this.tsBtnExit.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F);
            this.tsBtnExit.Name = "tsBtnExit";
            this.tsBtnExit.Size = new System.Drawing.Size(36, 24);
            this.tsBtnExit.Text = "Exit";
            this.tsBtnExit.Click += new System.EventHandler(this.tsBtnExit_Click);
            // 
            // tsBtnfavourite
            // 
            this.tsBtnfavourite.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnfavourite.AutoSize = false;
            this.tsBtnfavourite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnfavourite.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnfavourite.Image")));
            this.tsBtnfavourite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnfavourite.Name = "tsBtnfavourite";
            this.tsBtnfavourite.Size = new System.Drawing.Size(28, 24);
            this.tsBtnfavourite.Text = "Add to favourites";
            this.tsBtnfavourite.Click += new System.EventHandler(this.tsBtnfavourite_Click);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.BackColor = System.Drawing.Color.Moccasin;
            this.MMBottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MMBottomPanel.Controls.Add(this.lblFooterMessage);
            this.MMBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 487);
            this.MMBottomPanel.Name = "MMBottomPanel";
            this.MMBottomPanel.Size = new System.Drawing.Size(674, 23);
            this.MMBottomPanel.TabIndex = 55;
            // 
            // lblFooterMessage
            // 
            this.lblFooterMessage.AutoSize = true;
            this.lblFooterMessage.Location = new System.Drawing.Point(5, 3);
            this.lblFooterMessage.Name = "lblFooterMessage";
            this.lblFooterMessage.Size = new System.Drawing.Size(0, 16);
            this.lblFooterMessage.TabIndex = 0;
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.MMMainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MMMainPanel.Controls.Add(this.pnlFileName);
            this.MMMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MMMainPanel.Location = new System.Drawing.Point(0, 52);
            this.MMMainPanel.Name = "MMMainPanel";
            this.MMMainPanel.Size = new System.Drawing.Size(674, 435);
            this.MMMainPanel.TabIndex = 56;
            // 
            // pnlFileName
            // 
            this.pnlFileName.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFileName.Controls.Add(this.txtFileName);
            this.pnlFileName.Controls.Add(this.btnOKMultiSelectionForPanel);
            this.pnlFileName.Controls.Add(this.lblEmailID);
            this.pnlFileName.Controls.Add(this.txtEmailID);
            this.pnlFileName.Location = new System.Drawing.Point(43, 1);
            this.pnlFileName.Name = "pnlFileName";
            this.pnlFileName.Size = new System.Drawing.Size(565, 55);
            this.pnlFileName.TabIndex = 0;
            this.pnlFileName.Visible = false;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(3, 2);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(13, 20);
            this.txtFileName.TabIndex = 12;
            this.txtFileName.Visible = false;
            // 
            // btnOKMultiSelectionForPanel
            // 
            this.btnOKMultiSelectionForPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelectionForPanel.BackgroundImage")));
            this.btnOKMultiSelectionForPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOKMultiSelectionForPanel.ForeColor = System.Drawing.Color.White;
            this.btnOKMultiSelectionForPanel.Image = ((System.Drawing.Image)(resources.GetObject("btnOKMultiSelectionForPanel.Image")));
            this.btnOKMultiSelectionForPanel.Location = new System.Drawing.Point(497, 2);
            this.btnOKMultiSelectionForPanel.Name = "btnOKMultiSelectionForPanel";
            this.btnOKMultiSelectionForPanel.Size = new System.Drawing.Size(63, 46);
            this.btnOKMultiSelectionForPanel.TabIndex = 11;
            this.btnOKMultiSelectionForPanel.Text = "Go";
            this.btnOKMultiSelectionForPanel.UseVisualStyleBackColor = true;
            this.btnOKMultiSelectionForPanel.Click += new System.EventHandler(this.btnOKMultiSelectionForPanel_Click);
            // 
            // lblEmailID
            // 
            this.lblEmailID.AutoSize = true;
            this.lblEmailID.Location = new System.Drawing.Point(30, 19);
            this.lblEmailID.Name = "lblEmailID";
            this.lblEmailID.Size = new System.Drawing.Size(70, 16);
            this.lblEmailID.TabIndex = 3;
            this.lblEmailID.Text = " Email ID :";
            // 
            // txtEmailID
            // 
            this.txtEmailID.AlphabeticalList = false;
            this.txtEmailID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEmailID.ColumnWidth = null;
            this.txtEmailID.DataSource = null;
            this.txtEmailID.DisplayColumnNo = 1;
            this.txtEmailID.DropDownHeight = 200;
            this.txtEmailID.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailID.Location = new System.Drawing.Point(110, 15);
            this.txtEmailID.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtEmailID.Name = "txtEmailID";
            this.txtEmailID.ReadOnly = false;
            this.txtEmailID.SelectedID = null;
            this.txtEmailID.Size = new System.Drawing.Size(379, 22);
            this.txtEmailID.SourceDataString = null;
            this.txtEmailID.TabIndex = 1;
            this.txtEmailID.UserControlToShow = null;
            this.txtEmailID.ValueColumnNo = 0;
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // ReportBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MMMainPanel);
            this.Controls.Add(this.MMBottomPanel);
            this.Controls.Add(this.MMTopPanel);
            this.Name = "ReportBaseControl";
            this.Size = new System.Drawing.Size(674, 510);
            this.MMTopPanel.ResumeLayout(false);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.pnlFileName.ResumeLayout(false);
            this.pnlFileName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MMTopPanel;
        public EcoMart.InterfaceLayer.CommonControls.PSHeaderLabelForReports headerLabel1;
        public System.Windows.Forms.ToolStrip MainToolStrip;
        public System.Windows.Forms.ToolStripButton tsbtnPrint;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton tsBtnExport;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripButton tsBtnEMail;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton tsBtnExit;
        public System.Windows.Forms.Panel MMBottomPanel;
        public System.Windows.Forms.Panel MMMainPanel;
        public System.Drawing.Printing.PrintDocument printDoc;
        public System.Windows.Forms.PrintDialog printDialog;
        public EcoMart.InterfaceLayer.CommonControls.PSlblMessage lblFooterMessage;
        private System.Windows.Forms.ToolStripButton tsBtnfavourite;
        private System.Windows.Forms.Panel pnlFileName;
        private EcoMart.InterfaceLayer.CommonControls.PSLabel lblEmailID;
        private EcoMart.InterfaceLayer.CommonControls.PSbtnOKMultiSelection btnOKMultiSelectionForPanel;
        private EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox txtEmailID;
        private System.Windows.Forms.TextBox txtFileName;
    }
}
