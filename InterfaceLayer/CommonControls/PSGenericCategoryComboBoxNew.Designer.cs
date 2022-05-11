namespace PaperlessPharmaRetail.InterfaceLayer.CommonControls
{
    partial class PSGenericCategoryComboBoxNew
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.multiColumComboBoxGeneric = new EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOpen.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(521, 0);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(35, 24);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.TabStop = false;
            this.btnOpen.Text = "...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // multiColumComboBoxGeneric
            // 
            this.multiColumComboBoxGeneric.AlphabeticalList = false;
            this.multiColumComboBoxGeneric.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.multiColumComboBoxGeneric.ColumnWidth = null;
            this.multiColumComboBoxGeneric.DataSource = null;
            this.multiColumComboBoxGeneric.DisplayColumnNo = 1;
            this.multiColumComboBoxGeneric.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiColumComboBoxGeneric.DropDownHeight = 200;
            this.multiColumComboBoxGeneric.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiColumComboBoxGeneric.GridLines = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.GridLineView.None;
            this.multiColumComboBoxGeneric.Location = new System.Drawing.Point(0, 0);
            this.multiColumComboBoxGeneric.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.multiColumComboBoxGeneric.Name = "multiColumComboBoxGeneric";
            this.multiColumComboBoxGeneric.ReadOnly = false;
            this.multiColumComboBoxGeneric.SelectedItem = null;
            this.multiColumComboBoxGeneric.ShowHeader = false;
            this.multiColumComboBoxGeneric.Size = new System.Drawing.Size(521, 22);
            this.multiColumComboBoxGeneric.SourceDataHeader = null;
            this.multiColumComboBoxGeneric.SourceDataString = null;
            this.multiColumComboBoxGeneric.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDownList;
            this.multiColumComboBoxGeneric.TabIndex = 2;
            this.multiColumComboBoxGeneric.ValueColumnNo = 0;
            this.multiColumComboBoxGeneric.EnterKeyPressed += new System.EventHandler(this.multiColumComboBoxGeneric_EnterKeyPressed);
            this.multiColumComboBoxGeneric.UpArrowKeyPressed += new System.EventHandler(this.multiColumComboBoxGeneric_UpArrowPressed);
            // 
            // PSGenericCategoryComboBoxNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.multiColumComboBoxGeneric);
            this.Controls.Add(this.btnOpen);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PSGenericCategoryComboBoxNew";
            this.Size = new System.Drawing.Size(556, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox multiColumComboBoxGeneric;
        private System.Windows.Forms.Button btnOpen;
    }
}
