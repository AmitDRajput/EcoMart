namespace PharmaSYSPlus.CommonLibrary
{
    partial class DecimalTextBoxSmallFont
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
            this.SuspendLayout();
            // 
            // DecimalTextBox
            // 
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxLength = 15;
            this.Tag = "";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DecimalTextBox_KeyDown);
            this.Leave += new System.EventHandler(this.DecimalTextBox_Leave);
            this.Enter += new System.EventHandler(this.DecimalTextBox_Enter);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
