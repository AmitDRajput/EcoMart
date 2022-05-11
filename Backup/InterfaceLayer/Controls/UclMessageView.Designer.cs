namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclMessageView
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
            this.tmScroller = new System.Windows.Forms.Timer(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.cmMessageView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemMessages = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExpiry = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemStop = new System.Windows.Forms.ToolStripMenuItem();
            this.expiryTwoMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expiryThreeMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmMessageView.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmScroller
            // 
            this.tmScroller.Tick += new System.EventHandler(this.tmScroller_Tick);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 4);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(96, 15);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Message Text";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblMessage_MouseDown);
            // 
            // cmMessageView
            // 
            this.cmMessageView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemMessages,
            this.menuItemExpiry,
            this.expiryTwoMonthToolStripMenuItem,
            this.expiryThreeMonthToolStripMenuItem,
            this.menuItemStop});
            this.cmMessageView.Name = "cmMessageView";
            this.cmMessageView.Size = new System.Drawing.Size(180, 136);
            // 
            // menuItemMessages
            // 
            this.menuItemMessages.Checked = true;
            this.menuItemMessages.CheckOnClick = true;
            this.menuItemMessages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemMessages.Name = "menuItemMessages";
            this.menuItemMessages.Size = new System.Drawing.Size(179, 22);
            this.menuItemMessages.Text = "Messages";
            this.menuItemMessages.Click += new System.EventHandler(this.menuItemMessages_Click);
            // 
            // menuItemExpiry
            // 
            this.menuItemExpiry.CheckOnClick = true;
            this.menuItemExpiry.Name = "menuItemExpiry";
            this.menuItemExpiry.Size = new System.Drawing.Size(179, 22);
            this.menuItemExpiry.Text = "Expiry One Month";
            this.menuItemExpiry.Click += new System.EventHandler(this.menuItemExpiry_Click);
            // 
            // menuItemStop
            // 
            this.menuItemStop.CheckOnClick = true;
            this.menuItemStop.Name = "menuItemStop";
            this.menuItemStop.Size = new System.Drawing.Size(179, 22);
            this.menuItemStop.Text = "Stop";
            this.menuItemStop.Click += new System.EventHandler(this.menuItemStop_Click);
            // 
            // expiryTwoMonthToolStripMenuItem
            // 
            this.expiryTwoMonthToolStripMenuItem.CheckOnClick = true;
            this.expiryTwoMonthToolStripMenuItem.Name = "expiryTwoMonthToolStripMenuItem";
            this.expiryTwoMonthToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.expiryTwoMonthToolStripMenuItem.Text = "Expiry Two Month";
            this.expiryTwoMonthToolStripMenuItem.Click += new System.EventHandler(this.expiryTwoMonthToolStripMenuItem_Click);
            // 
            // expiryThreeMonthToolStripMenuItem
            // 
            this.expiryThreeMonthToolStripMenuItem.CheckOnClick = true;
            this.expiryThreeMonthToolStripMenuItem.Name = "expiryThreeMonthToolStripMenuItem";
            this.expiryThreeMonthToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.expiryThreeMonthToolStripMenuItem.Text = "Expiry Three Month";
            this.expiryThreeMonthToolStripMenuItem.Click += new System.EventHandler(this.expiryThreeMonthToolStripMenuItem_Click);
            // 
            // UclMessageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.lblMessage);
            this.Name = "UclMessageView";
            this.Size = new System.Drawing.Size(449, 25);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UclMessageView_MouseDown);
            this.cmMessageView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmScroller;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ContextMenuStrip cmMessageView;
        private System.Windows.Forms.ToolStripMenuItem menuItemMessages;
        private System.Windows.Forms.ToolStripMenuItem menuItemExpiry;
        private System.Windows.Forms.ToolStripMenuItem menuItemStop;
        private System.Windows.Forms.ToolStripMenuItem expiryTwoMonthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expiryThreeMonthToolStripMenuItem;
    }
}
