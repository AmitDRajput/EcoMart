namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    partial class FromDate
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
            // FromDate
            // 
            this.CustomFormat = "";
            this.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Size = new System.Drawing.Size(125, 26);
            this.Leave += new System.EventHandler(this.FromDate_Leave);
            this.Enter += new System.EventHandler(this.FromDate_Enter);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
