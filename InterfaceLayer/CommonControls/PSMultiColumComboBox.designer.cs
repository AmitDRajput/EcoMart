namespace EcoMart.InterfaceLayer.CommonControls
{
    partial class PSMultiColumComboBox
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
            this.txtbox = new System.Windows.Forms.TextBox();
            this.buttonDropDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtbox
            // 
            this.txtbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbox.Location = new System.Drawing.Point(0, 0);
            this.txtbox.Name = "txtbox";
            this.txtbox.Size = new System.Drawing.Size(254, 22);
            this.txtbox.TabIndex = 0;
            this.txtbox.FontChanged += new System.EventHandler(this.txtbox_FontChanged);
            this.txtbox.TextChanged += new System.EventHandler(this.txtbox_TextChanged);
            this.txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbox_KeyDown);
            this.txtbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbox_KeyUp);
            this.txtbox.Leave += new System.EventHandler(this.txtbox_Leave);
            // 
            // buttonDropDown
            // 
            this.buttonDropDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDropDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDropDown.Font = new System.Drawing.Font("Marlett", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.buttonDropDown.Location = new System.Drawing.Point(254, 0);
            this.buttonDropDown.Name = "buttonDropDown";
            this.buttonDropDown.Size = new System.Drawing.Size(26, 23);
            this.buttonDropDown.TabIndex = 4;
            this.buttonDropDown.TabStop = false;
            this.buttonDropDown.Text = "u";
            this.buttonDropDown.UseVisualStyleBackColor = true;
            this.buttonDropDown.Click += new System.EventHandler(this.buttonDropDown_Click);
            // 
            // PSMultiColumComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtbox);
            this.Controls.Add(this.buttonDropDown);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PSMultiColumComboBox";
            this.Size = new System.Drawing.Size(280, 23);
            this.Leave += new System.EventHandler(this.MultiColumComboBox_Leave);
            this.Resize += new System.EventHandler(this.MultiColumComboBox_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }         

        #endregion

        private System.Windows.Forms.TextBox txtbox;
        private System.Windows.Forms.Button buttonDropDown;

    }
}
