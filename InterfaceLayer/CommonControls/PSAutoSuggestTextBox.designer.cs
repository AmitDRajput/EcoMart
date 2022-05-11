namespace EcoMart.InterfaceLayer.CommonControls
{
    partial class PSAutoSuggestTextBox
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
            this.multiColumComboBox1 = new EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox();
            this.SuspendLayout();
            // 
            // multiColumComboBox1
            // 
            this.multiColumComboBox1.AlphabeticalList = false;
            this.multiColumComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.multiColumComboBox1.ColumnWidth = null;
            this.multiColumComboBox1.DataSource = null;
            this.multiColumComboBox1.DisplayColumnNo = 1;
            this.multiColumComboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiColumComboBox1.DropDownHeight = 200;
            this.multiColumComboBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiColumComboBox1.GridLines = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.GridLineView.None;
            this.multiColumComboBox1.Location = new System.Drawing.Point(0, 0);
            this.multiColumComboBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.multiColumComboBox1.Name = "multiColumComboBox1";
            this.multiColumComboBox1.ReadOnly = false;
            this.multiColumComboBox1.SelectedItem = null;
            this.multiColumComboBox1.ShowHeader = false;
            this.multiColumComboBox1.Size = new System.Drawing.Size(337, 22);
            this.multiColumComboBox1.SourceDataHeader = null;
            this.multiColumComboBox1.SourceDataString = null;
            this.multiColumComboBox1.Style = EcoMart.InterfaceLayer.CommonControls.PSMultiColumComboBox.ComboStyle.DropDown;
            this.multiColumComboBox1.TabIndex = 1;
            this.multiColumComboBox1.ValueColumnNo = 0;
            this.multiColumComboBox1.SelectedIndexChanged += new System.EventHandler(this.multiColumComboBox1_SeletectIndexChanged);
            this.multiColumComboBox1.EnterKeyPressed += new System.EventHandler(this.multiColumComboBox1_EnterKeyPressed);
            this.multiColumComboBox1.UpArrowKeyPressed += new System.EventHandler(this.multiColumComboBox1_UpArrowKeyPressed);
            this.multiColumComboBox1.Enter += new System.EventHandler(this.multiColumComboBox1_Enter);
            this.multiColumComboBox1.Leave += new System.EventHandler(this.multiColumComboBox1_Leave);
            // 
            // PSAutoSuggestTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.multiColumComboBox1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "PSAutoSuggestTextBox";
            this.Size = new System.Drawing.Size(337, 22);
            this.Resize += new System.EventHandler(this.MComboBoxNew_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private PSMultiColumComboBox multiColumComboBox1;
    }
}
