namespace EcoMart
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.IMLIcons16 = new System.Windows.Forms.ImageList(this.components);
            this.IMLIcons32 = new System.Windows.Forms.ImageList(this.components);
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmenusubLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenusubLicAssociation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmesnuShopDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenusubImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenusubImportSaleBillAllied = new System.Windows.Forms.ToolStripMenuItem();
            this.downLoadUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenusubSMS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenusubExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuModules = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuFavourites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuYearEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuExitMain = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMessageContainer = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MainOutlookBar = new ControlLib.OutlookControl();
            this.PnlClientArea = new System.Windows.Forms.Panel();
            this.PnlViewDetail = new System.Windows.Forms.Panel();
            this.pnlDBBackupProgress = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.tssHeader = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsbMinimize = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbClose = new System.Windows.Forms.ToolStripSplitButton();
            this.lblDBBackupMsgLine1 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.uclMessageView1 = new EcoMart.InterfaceLayer.UclMessageView();
            this.statusStrip1.SuspendLayout();
            this.msMain.SuspendLayout();
            this.pnlMessageContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.PnlClientArea.SuspendLayout();
            this.PnlViewDetail.SuspendLayout();
            this.pnlDBBackupProgress.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.MarqueeAnimationSpeed = 50;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // IMLIcons16
            // 
            this.IMLIcons16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IMLIcons16.ImageStream")));
            this.IMLIcons16.TransparentColor = System.Drawing.Color.Transparent;
            this.IMLIcons16.Images.SetKeyName(0, "images_medicine_10.jpg");
            this.IMLIcons16.Images.SetKeyName(1, "images_diseases_1.jpeg");
            this.IMLIcons16.Images.SetKeyName(2, "images_Users.jpeg");
            this.IMLIcons16.Images.SetKeyName(3, "patient.jpg");
            this.IMLIcons16.Images.SetKeyName(4, "doctor.jpg");
            this.IMLIcons16.Images.SetKeyName(5, "ProdCategory1.JPG");
            this.IMLIcons16.Images.SetKeyName(6, "ProdCategory.JPG");
            this.IMLIcons16.Images.SetKeyName(7, "Shelf.jpg");
            this.IMLIcons16.Images.SetKeyName(8, "Bank1.jpg");
            this.IMLIcons16.Images.SetKeyName(9, "company.jpg");
            this.IMLIcons16.Images.SetKeyName(10, "salesman11.JPG");
            this.IMLIcons16.Images.SetKeyName(11, "Customer.jpg");
            this.IMLIcons16.Images.SetKeyName(12, "ProductCategory.jpg");
            this.IMLIcons16.Images.SetKeyName(13, "ward.jpg");
            this.IMLIcons16.Images.SetKeyName(14, "account.jpg");
            // 
            // IMLIcons32
            // 
            this.IMLIcons32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IMLIcons32.ImageStream")));
            this.IMLIcons32.TransparentColor = System.Drawing.Color.Transparent;
            this.IMLIcons32.Images.SetKeyName(0, "images_medicine_10.jpg");
            this.IMLIcons32.Images.SetKeyName(1, "images_diseases_1.jpeg");
            this.IMLIcons32.Images.SetKeyName(2, "images_Users.jpeg");
            this.IMLIcons32.Images.SetKeyName(3, "patient.jpg");
            this.IMLIcons32.Images.SetKeyName(4, "doctor.jpg");
            this.IMLIcons32.Images.SetKeyName(5, "ProdCategory.JPG");
            this.IMLIcons32.Images.SetKeyName(6, "ProdCategory1.JPG");
            this.IMLIcons32.Images.SetKeyName(7, "Shelf.jpg");
            this.IMLIcons32.Images.SetKeyName(8, "Bank1.jpg");
            this.IMLIcons32.Images.SetKeyName(9, "company.jpg");
            this.IMLIcons32.Images.SetKeyName(10, "salesman11.JPG");
            this.IMLIcons32.Images.SetKeyName(11, "Customer.jpg");
            this.IMLIcons32.Images.SetKeyName(12, "ProductCategory.jpg");
            this.IMLIcons32.Images.SetKeyName(13, "ward.jpg");
            this.IMLIcons32.Images.SetKeyName(14, "account.jpg");
            // 
            // msMain
            // 
            this.msMain.BackColor = System.Drawing.Color.MediumTurquoise;
            resources.ApplyResources(this.msMain, "msMain");
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenuFile,
            this.tsmenuModules,
            this.tsmenuReports,
            this.tsmenuFavourites,
            this.tsMenuSettings,
            this.tsMenuTools,
            this.tsmenuWindow,
            this.tsmenuYearEnd,
            this.tsMenuExitMain});
            this.msMain.Name = "msMain";
            // 
            // tsmenuFile
            // 
            this.tsmenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.tsmenusubLicense,
            this.tsmenusubImport,
            this.downLoadUpdatesToolStripMenuItem,
            this.tsmenusubSMS,
            this.tsmenusubExit});
            this.tsmenuFile.Name = "tsmenuFile";
            resources.ApplyResources(this.tsmenuFile, "tsmenuFile");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsmenusubLicense
            // 
            this.tsmenusubLicense.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenusubLicAssociation,
            this.tsmesnuShopDetails});
            this.tsmenusubLicense.Name = "tsmenusubLicense";
            resources.ApplyResources(this.tsmenusubLicense, "tsmenusubLicense");
            // 
            // tsmenusubLicAssociation
            // 
            this.tsmenusubLicAssociation.Name = "tsmenusubLicAssociation";
            resources.ApplyResources(this.tsmenusubLicAssociation, "tsmenusubLicAssociation");
            this.tsmenusubLicAssociation.Click += new System.EventHandler(this.tsmenusubLicAssociation_Click);
            // 
            // tsmesnuShopDetails
            // 
            this.tsmesnuShopDetails.Name = "tsmesnuShopDetails";
            resources.ApplyResources(this.tsmesnuShopDetails, "tsmesnuShopDetails");
            this.tsmesnuShopDetails.Click += new System.EventHandler(this.tsmesnuShopDetails_Click);
            // 
            // tsmenusubImport
            // 
            this.tsmenusubImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenusubImportSaleBillAllied});
            this.tsmenusubImport.Name = "tsmenusubImport";
            resources.ApplyResources(this.tsmenusubImport, "tsmenusubImport");
            // 
            // tsmenusubImportSaleBillAllied
            // 
            this.tsmenusubImportSaleBillAllied.Name = "tsmenusubImportSaleBillAllied";
            resources.ApplyResources(this.tsmenusubImportSaleBillAllied, "tsmenusubImportSaleBillAllied");
            this.tsmenusubImportSaleBillAllied.Click += new System.EventHandler(this.tsmenusubImportSaleBillAllied_Click);
            // 
            // downLoadUpdatesToolStripMenuItem
            // 
            this.downLoadUpdatesToolStripMenuItem.Name = "downLoadUpdatesToolStripMenuItem";
            resources.ApplyResources(this.downLoadUpdatesToolStripMenuItem, "downLoadUpdatesToolStripMenuItem");
            this.downLoadUpdatesToolStripMenuItem.Click += new System.EventHandler(this.downLoadUpdatesToolStripMenuItem_Click);
            // 
            // tsmenusubSMS
            // 
            this.tsmenusubSMS.Name = "tsmenusubSMS";
            resources.ApplyResources(this.tsmenusubSMS, "tsmenusubSMS");
            this.tsmenusubSMS.Click += new System.EventHandler(this.tsmenusubSMS_Click);
            // 
            // tsmenusubExit
            // 
            this.tsmenusubExit.Name = "tsmenusubExit";
            resources.ApplyResources(this.tsmenusubExit, "tsmenusubExit");
            this.tsmenusubExit.Click += new System.EventHandler(this.tsmenusubExit_Click);
            // 
            // tsmenuModules
            // 
            this.tsmenuModules.Name = "tsmenuModules";
            resources.ApplyResources(this.tsmenuModules, "tsmenuModules");
            // 
            // tsmenuReports
            // 
            this.tsmenuReports.Name = "tsmenuReports";
            resources.ApplyResources(this.tsmenuReports, "tsmenuReports");
            // 
            // tsmenuFavourites
            // 
            this.tsmenuFavourites.Name = "tsmenuFavourites";
            resources.ApplyResources(this.tsmenuFavourites, "tsmenuFavourites");
            // 
            // tsMenuSettings
            // 
            this.tsMenuSettings.Name = "tsMenuSettings";
            resources.ApplyResources(this.tsMenuSettings, "tsMenuSettings");
            // 
            // tsMenuTools
            // 
            this.tsMenuTools.Name = "tsMenuTools";
            resources.ApplyResources(this.tsMenuTools, "tsMenuTools");
            // 
            // tsmenuWindow
            // 
            this.tsmenuWindow.Name = "tsmenuWindow";
            resources.ApplyResources(this.tsmenuWindow, "tsmenuWindow");
            // 
            // tsmenuYearEnd
            // 
            this.tsmenuYearEnd.Name = "tsmenuYearEnd";
            resources.ApplyResources(this.tsmenuYearEnd, "tsmenuYearEnd");
            // 
            // tsMenuExitMain
            // 
            this.tsMenuExitMain.Name = "tsMenuExitMain";
            resources.ApplyResources(this.tsMenuExitMain, "tsMenuExitMain");
            this.tsMenuExitMain.Click += new System.EventHandler(this.tsmenusubExit_Click);
            // 
            // pnlMessageContainer
            // 
            this.pnlMessageContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlMessageContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMessageContainer.Controls.Add(this.uclMessageView1);
            resources.ApplyResources(this.pnlMessageContainer, "pnlMessageContainer");
            this.pnlMessageContainer.Name = "pnlMessageContainer";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.AliceBlue;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.splitContainer1.Panel1.Controls.Add(this.MainOutlookBar);
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.PnlClientArea);
            // 
            // MainOutlookBar
            // 
            this.MainOutlookBar.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.MainOutlookBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.MainOutlookBar, "MainOutlookBar");
            this.MainOutlookBar.Name = "MainOutlookBar";
            this.MainOutlookBar.ItemClicked += new ControlLib.OutlookBarItemClickedHandler(this.MainOutlookBar_ItemClicked);
            // 
            // PnlClientArea
            // 
            this.PnlClientArea.BackColor = System.Drawing.Color.LightSteelBlue;
            this.PnlClientArea.Controls.Add(this.PnlViewDetail);
            resources.ApplyResources(this.PnlClientArea, "PnlClientArea");
            this.PnlClientArea.Name = "PnlClientArea";
            // 
            // PnlViewDetail
            // 
            resources.ApplyResources(this.PnlViewDetail, "PnlViewDetail");
            this.PnlViewDetail.BackColor = System.Drawing.Color.White;
            this.PnlViewDetail.BackgroundImage = global::EcoMart.Properties.Resources._10;
            this.PnlViewDetail.Controls.Add(this.pnlDBBackupProgress);
            this.PnlViewDetail.Controls.Add(this.label2);
            this.PnlViewDetail.Controls.Add(this.lblWelcome);
            this.PnlViewDetail.Controls.Add(this.label3);
            this.PnlViewDetail.Name = "PnlViewDetail";
            // 
            // pnlDBBackupProgress
            // 
            this.pnlDBBackupProgress.BackColor = System.Drawing.Color.Coral;
            this.pnlDBBackupProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDBBackupProgress.Controls.Add(this.lblDBBackupMsgLine1);
            resources.ApplyResources(this.pnlDBBackupProgress, "pnlDBBackupProgress");
            this.pnlDBBackupProgress.Name = "pnlDBBackupProgress";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblWelcome
            // 
            resources.ApplyResources(this.lblWelcome, "lblWelcome");
            this.lblWelcome.Name = "lblWelcome";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.ForestGreen;
            this.label3.Name = "label3";
            // 
            // statusStrip2
            // 
            resources.ApplyResources(this.statusStrip2, "statusStrip2");
            this.statusStrip2.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssHeader,
            this.tsbMinimize,
            this.tsbClose});
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.SizingGrip = false;
            // 
            // tssHeader
            // 
            this.tssHeader.ForeColor = System.Drawing.Color.White;
            this.tssHeader.Name = "tssHeader";
            resources.ApplyResources(this.tssHeader, "tssHeader");
            this.tssHeader.Spring = true;
            // 
            // tsbMinimize
            // 
            this.tsbMinimize.BackColor = System.Drawing.Color.MidnightBlue;
            this.tsbMinimize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMinimize.DropDownButtonWidth = 0;
            this.tsbMinimize.Image = global::EcoMart.Properties.Resources.Maximize_18;
            resources.ApplyResources(this.tsbMinimize, "tsbMinimize");
            this.tsbMinimize.Name = "tsbMinimize";
            this.tsbMinimize.ButtonClick += new System.EventHandler(this.tsbMinimize_ButtonClick);
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.DropDownButtonWidth = 0;
            this.tsbClose.Image = global::EcoMart.Properties.Resources.Close;
            resources.ApplyResources(this.tsbClose, "tsbClose");
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbClose.ButtonClick += new System.EventHandler(this.tsbClose_ButtonClick);
            // 
            // lblDBBackupMsgLine1
            // 
            resources.ApplyResources(this.lblDBBackupMsgLine1, "lblDBBackupMsgLine1");
            this.lblDBBackupMsgLine1.Name = "lblDBBackupMsgLine1";
            // 
            // uclMessageView1
            // 
            this.uclMessageView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.uclMessageView1, "uclMessageView1");
            this.uclMessageView1.MsgList = null;
            this.uclMessageView1.Name = "uclMessageView1";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlMessageContainer);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.statusStrip2);
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.pnlMessageContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.PnlClientArea.ResumeLayout(false);
            this.PnlViewDetail.ResumeLayout(false);
            this.PnlViewDetail.PerformLayout();
            this.pnlDBBackupProgress.ResumeLayout(false);
            this.pnlDBBackupProgress.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ImageList IMLIcons16;
        private System.Windows.Forms.ImageList IMLIcons32;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmenuFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmenusubExit;
        private System.Windows.Forms.ToolStripMenuItem tsmenuModules;
        private System.Windows.Forms.Panel pnlMessageContainer;
        private System.Windows.Forms.Panel PnlClientArea;
        public System.Windows.Forms.Panel PnlViewDetail;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ControlLib.OutlookControl MainOutlookBar;
        private System.Windows.Forms.ToolStripMenuItem tsmenuWindow;
        private System.Windows.Forms.ToolStripMenuItem tsmenuReports;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private EcoMart.InterfaceLayer.UclMessageView uclMessageView1;
        private System.Windows.Forms.ToolStripMenuItem tsmenusubLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmenusubLicAssociation;
        private System.Windows.Forms.ToolStripMenuItem tsmenuFavourites;
        private System.Windows.Forms.ToolStripMenuItem tsmenusubImport;
        private System.Windows.Forms.ToolStripMenuItem tsMenuTools;
        private System.Windows.Forms.ToolStripMenuItem tsmenusubImportSaleBillAllied;
        private System.Windows.Forms.ToolStripMenuItem tsmenuYearEnd;
        private System.Windows.Forms.ToolStripMenuItem tsMenuSettings;
        private System.Windows.Forms.Panel pnlDBBackupProgress;
        private InterfaceLayer.CommonControls.PSLabel lblDBBackupMsgLine1;
        private System.Windows.Forms.ToolStripMenuItem tsMenuExitMain;
        private System.Windows.Forms.ToolStripMenuItem tsmesnuShopDetails;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel tssHeader;
        private System.Windows.Forms.ToolStripSplitButton tsbMinimize;
        private System.Windows.Forms.ToolStripSplitButton tsbClose;
        private System.Windows.Forms.ToolStripMenuItem downLoadUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmenusubSMS;
        private System.Windows.Forms.Label label3;
    }
}

