namespace PharmaSYSRetailPlus
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
            this.tsmenusubImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenusubImportSaleBillAllied = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenusubExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuModules = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuFavourites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuYearEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMessageContainer = new System.Windows.Forms.Panel();
            this.uclMessageView1 = new PharmaSYSRetailPlus.InterfaceLayer.UclMessageView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MainOutlookBar = new ControlLib.OutlookControl();
            this.PnlClientArea = new System.Windows.Forms.Panel();
            this.PnlViewDetail = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.msMain.SuspendLayout();
            this.pnlMessageContainer.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.PnlClientArea.SuspendLayout();
            this.PnlViewDetail.SuspendLayout();
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
            resources.ApplyResources(this.msMain, "msMain");
            this.msMain.BackColor = System.Drawing.Color.GhostWhite;
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenuFile,
            this.tsmenuModules,
            this.tsmenuReports,
            this.tsmenuFavourites,
            this.tsMenuSettings,
            this.tsMenuTools,
            this.tsmenuWindow,
            this.tsmenuYearEnd});
            this.msMain.Name = "msMain";
            // 
            // tsmenuFile
            // 
            this.tsmenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.tsmenusubLicense,
            this.tsmenusubImport,
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
            this.tsmenusubLicAssociation});
            this.tsmenusubLicense.Name = "tsmenusubLicense";
            resources.ApplyResources(this.tsmenusubLicense, "tsmenusubLicense");
            // 
            // tsmenusubLicAssociation
            // 
            this.tsmenusubLicAssociation.Name = "tsmenusubLicAssociation";
            resources.ApplyResources(this.tsmenusubLicAssociation, "tsmenusubLicAssociation");
            this.tsmenusubLicAssociation.Click += new System.EventHandler(this.tsmenusubLicAssociation_Click);
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
            // pnlMessageContainer
            // 
            this.pnlMessageContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlMessageContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMessageContainer.Controls.Add(this.uclMessageView1);
            resources.ApplyResources(this.pnlMessageContainer, "pnlMessageContainer");
            this.pnlMessageContainer.Name = "pnlMessageContainer";
            // 
            // uclMessageView1
            // 
            this.uclMessageView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.uclMessageView1, "uclMessageView1");
            this.uclMessageView1.MsgList = null;
            this.uclMessageView1.Name = "uclMessageView1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.AliceBlue;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.splitContainer1.Panel1.Controls.Add(this.MainOutlookBar);
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
            this.PnlViewDetail.BackColor = System.Drawing.Color.Lavender;
            this.PnlViewDetail.Controls.Add(this.label2);
            this.PnlViewDetail.Controls.Add(this.label3);
            this.PnlViewDetail.Controls.Add(this.lblWelcome);
            this.PnlViewDetail.Name = "PnlViewDetail";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Name = "label3";
            // 
            // lblWelcome
            // 
            resources.ApplyResources(this.lblWelcome, "lblWelcome");
            this.lblWelcome.Name = "lblWelcome";
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
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.pnlMessageContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.PnlClientArea.ResumeLayout(false);
            this.PnlViewDetail.ResumeLayout(false);
            this.PnlViewDetail.PerformLayout();
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
       
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private PharmaSYSRetailPlus.InterfaceLayer.UclMessageView uclMessageView1;
        private System.Windows.Forms.ToolStripMenuItem tsmenusubLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmenusubLicAssociation;
        private System.Windows.Forms.ToolStripMenuItem tsmenuFavourites;
        private System.Windows.Forms.ToolStripMenuItem tsmenusubImport;
        private System.Windows.Forms.ToolStripMenuItem tsMenuTools;
        private System.Windows.Forms.ToolStripMenuItem tsmenusubImportSaleBillAllied;
        private System.Windows.Forms.ToolStripMenuItem tsmenuYearEnd;
        private System.Windows.Forms.ToolStripMenuItem tsMenuSettings;

    }
}

