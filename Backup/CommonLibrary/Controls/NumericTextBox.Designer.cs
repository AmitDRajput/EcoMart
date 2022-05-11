namespace PharmaSYSPlus.CommonLibrary
{
    partial class NumericTextBox
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
            // NumericTextBox
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NumericTextBox_KeyDown);
            this.Leave += new System.EventHandler(this.NumericTextBox_Leave);
            this.Enter += new System.EventHandler(this.NumericTextBox_Enter);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
