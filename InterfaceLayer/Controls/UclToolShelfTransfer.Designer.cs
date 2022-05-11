namespace EcoMart.InterfaceLayer
{
    partial class UclToolShelfTransfer
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
            this.lblFromShelf = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtFromShelf = new EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.lblToShelf = new EcoMart.InterfaceLayer.CommonControls.PSLabel();
            this.txtToShelf = new EcoMart.InterfaceLayer.CommonControls.PSAutoSuggestTextBox();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Size = new System.Drawing.Size(911, 23);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 480);
            this.MMBottomPanel.Size = new System.Drawing.Size(913, 63);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.lblToShelf);
            this.MMMainPanel.Controls.Add(this.txtToShelf);
            this.MMMainPanel.Controls.Add(this.lblFromShelf);
            this.MMMainPanel.Controls.Add(this.txtFromShelf);
            this.MMMainPanel.Size = new System.Drawing.Size(913, 428);
            this.MMMainPanel.Controls.SetChildIndex(this.txtFromShelf, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.lblFromShelf, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.txtToShelf, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.lblToShelf, 0);
            // 
            // lblFromShelf
            // 
            this.lblFromShelf.AutoSize = true;
            this.lblFromShelf.Location = new System.Drawing.Point(233, 141);
            this.lblFromShelf.Name = "lblFromShelf";
            this.lblFromShelf.Size = new System.Drawing.Size(56, 14);
            this.lblFromShelf.TabIndex = 90;
            this.lblFromShelf.Text = "Old Shelf";
            // 
            // txtFromShelf
            // 
            this.txtFromShelf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFromShelf.ColumnWidth = null;
            this.txtFromShelf.DataSource = null;
            this.txtFromShelf.DisplayColumnNo = 1;
            this.txtFromShelf.DropDownHeight = 200;
            this.txtFromShelf.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromShelf.Location = new System.Drawing.Point(303, 138);
            this.txtFromShelf.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtFromShelf.Name = "txtFromShelf";
            this.txtFromShelf.SelectedID = null;
            this.txtFromShelf.Size = new System.Drawing.Size(174, 22);
            this.txtFromShelf.SourceDataString = null;
            this.txtFromShelf.TabIndex = 89;
            this.txtFromShelf.UserControlToShow = null;
            this.txtFromShelf.ValueColumnNo = 0;
            this.txtFromShelf.EnterKeyPressed += new System.EventHandler(this.txtFromShelf_EnterKeyPressed);
            // 
            // lblToShelf
            // 
            this.lblToShelf.AutoSize = true;
            this.lblToShelf.Location = new System.Drawing.Point(228, 193);
            this.lblToShelf.Name = "lblToShelf";
            this.lblToShelf.Size = new System.Drawing.Size(61, 14);
            this.lblToShelf.TabIndex = 92;
            this.lblToShelf.Text = "New Shelf";
            // 
            // txtToShelf
            // 
            this.txtToShelf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtToShelf.ColumnWidth = null;
            this.txtToShelf.DataSource = null;
            this.txtToShelf.DisplayColumnNo = 1;
            this.txtToShelf.DropDownHeight = 200;
            this.txtToShelf.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToShelf.Location = new System.Drawing.Point(303, 190);
            this.txtToShelf.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtToShelf.Name = "txtToShelf";
            this.txtToShelf.SelectedID = null;
            this.txtToShelf.Size = new System.Drawing.Size(174, 22);
            this.txtToShelf.SourceDataString = null;
            this.txtToShelf.TabIndex = 91;
            this.txtToShelf.UserControlToShow = null;
            this.txtToShelf.ValueColumnNo = 0;
            // 
            // UclToolShelfTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UclToolShelfTransfer";
            this.Size = new System.Drawing.Size(913, 503);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CommonControls.PSLabel lblToShelf;
        private CommonControls.PSAutoSuggestTextBox txtToShelf;
        private CommonControls.PSLabel lblFromShelf;
        private CommonControls.PSAutoSuggestTextBox txtFromShelf;
    }
}
