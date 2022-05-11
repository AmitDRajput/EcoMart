namespace EcoMart.InterfaceLayer
{
    partial class SearchControl
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
            this.MMTopPanel = new System.Windows.Forms.Panel();
            this.MMDatePanel = new System.Windows.Forms.Panel();
            this.btnDateFromToGo = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.MMMainPanel = new System.Windows.Forms.Panel();
            this.dtTo = new EcoMart.InterfaceLayer.CommonControls.ToDate(this.components);
            this.mPlbl4 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.mPlbl2 = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.dtFrom = new EcoMart.InterfaceLayer.CommonControls.FromDate(this.components);
            this.headerLabelForOverView1 = new EcoMart.InterfaceLayer.CommonControls.PSHeaderLabelForOverView();
            this.MMTopPanel.SuspendLayout();
            this.MMDatePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MMTopPanel
            // 
            this.MMTopPanel.Controls.Add(this.headerLabelForOverView1);
            this.MMTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MMTopPanel.Location = new System.Drawing.Point(0, 0);
            this.MMTopPanel.Name = "MMTopPanel";
            this.MMTopPanel.Size = new System.Drawing.Size(549, 25);
            this.MMTopPanel.TabIndex = 24;
            // 
            // MMDatePanel
            // 
            this.MMDatePanel.Controls.Add(this.btnDateFromToGo);
            this.MMDatePanel.Controls.Add(this.dtTo);
            this.MMDatePanel.Controls.Add(this.mPlbl4);
            this.MMDatePanel.Controls.Add(this.mPlbl2);
            this.MMDatePanel.Controls.Add(this.dtFrom);
            this.MMDatePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MMDatePanel.Location = new System.Drawing.Point(0, 25);
            this.MMDatePanel.Name = "MMDatePanel";
            this.MMDatePanel.Size = new System.Drawing.Size(549, 30);
            this.MMDatePanel.TabIndex = 25;
            // 
            // btnDateFromToGo
            // 
            this.btnDateFromToGo.Location = new System.Drawing.Point(347, 0);
            this.btnDateFromToGo.Name = "btnDateFromToGo";
            this.btnDateFromToGo.Size = new System.Drawing.Size(59, 28);
            this.btnDateFromToGo.TabIndex = 5;
            this.btnDateFromToGo.Text = "Go";
            this.btnDateFromToGo.UseVisualStyleBackColor = false;
            this.btnDateFromToGo.Click += new System.EventHandler(this.btnDateFromToGo_Click);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MMMainPanel.Location = new System.Drawing.Point(0, 55);
            this.MMMainPanel.Name = "MMMainPanel";
            this.MMMainPanel.Size = new System.Drawing.Size(549, 179);
            this.MMMainPanel.TabIndex = 26;
            // 
            // dtTo
            // 
            this.dtTo.CustomFormat = "dd/MM/yyyy";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(216, 3);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(125, 24);
            this.dtTo.TabIndex = 3;
            this.dtTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtTo_KeyDown);
            // 
            // mPlbl4
            // 
            this.mPlbl4.AutoSize = true;
            this.mPlbl4.Location = new System.Drawing.Point(196, 8);
            this.mPlbl4.Name = "mPlbl4";
            this.mPlbl4.Size = new System.Drawing.Size(22, 16);
            this.mPlbl4.TabIndex = 4;
            this.mPlbl4.Text = "To";
            // 
            // mPlbl2
            // 
            this.mPlbl2.AutoSize = true;
            this.mPlbl2.Location = new System.Drawing.Point(4, 7);
            this.mPlbl2.Name = "mPlbl2";
            this.mPlbl2.Size = new System.Drawing.Size(41, 16);
            this.mPlbl2.TabIndex = 2;
            this.mPlbl2.Text = "From";
            // 
            // dtFrom
            // 
            this.dtFrom.CustomFormat = "dd/MM/yyyy";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(42, 3);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(125, 24);
            this.dtFrom.TabIndex = 1;
            this.dtFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtFrom_KeyDown);
            // 
            // headerLabelForOverView1
            // 
            this.headerLabelForOverView1.BackColor = System.Drawing.Color.MediumVioletRed;
            this.headerLabelForOverView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.headerLabelForOverView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerLabelForOverView1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabelForOverView1.ForeColor = System.Drawing.Color.White;
            this.headerLabelForOverView1.Location = new System.Drawing.Point(0, 0);
            this.headerLabelForOverView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.headerLabelForOverView1.Name = "headerLabelForOverView1";
            this.headerLabelForOverView1.Size = new System.Drawing.Size(549, 23);
            this.headerLabelForOverView1.TabIndex = 0;
            this.headerLabelForOverView1.ExitClicked += new System.EventHandler(this.headerLabelForOverView1_ExitClicked);
            // 
            // SearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.MMMainPanel);
            this.Controls.Add(this.MMDatePanel);
            this.Controls.Add(this.MMTopPanel);
            this.Name = "SearchControl";
            this.Size = new System.Drawing.Size(549, 234);
            this.MMTopPanel.ResumeLayout(false);
            this.MMDatePanel.ResumeLayout(false);
            this.MMDatePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public EcoMart.InterfaceLayer.CommonControls.PSHeaderLabelForOverView headerLabelForOverView1;
        private System.Windows.Forms.Panel MMTopPanel;
        public System.Windows.Forms.Panel MMDatePanel;
        public System.Windows.Forms.Panel MMMainPanel;
        private CommonControls.PSLabel mPlbl2;
        public CommonControls.FromDate dtFrom;
        public CommonControls.ToDate dtTo;
        private CommonControls.PSLabel mPlbl4;
        private PharmaSYSPlus.CommonLibrary.PSButton btnDateFromToGo;

    }
}
