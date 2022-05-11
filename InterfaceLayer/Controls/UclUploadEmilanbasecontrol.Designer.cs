namespace PharmaSYSRetailPlus.InterfaceLayer
{
    partial class UclUploadEmilan
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
            this.btnPurchaseOrder = new System.Windows.Forms.Button();
            this.pnlDebitCreditNote = new System.Windows.Forms.Panel();
            this.txtDNAmountSelected = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtCRAmountSelected = new PharmaSYSRetailPlus.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.btnOK = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgPurchaseOrder = new System.Windows.Forms.DataGridView();
            this.lblDebitCreditNote = new System.Windows.Forms.Label();
            this.MMBottomPanel.SuspendLayout();
            this.MMMainPanel.SuspendLayout();
            this.pnlDebitCreditNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPurchaseOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // headerLabel1
            // 
            this.headerLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.headerLabel1.Size = new System.Drawing.Size(904, 24);
            // 
            // MMBottomPanel
            // 
            this.MMBottomPanel.Location = new System.Drawing.Point(0, 356);
            this.MMBottomPanel.Margin = new System.Windows.Forms.Padding(2);
            this.MMBottomPanel.Size = new System.Drawing.Size(906, 65);
            // 
            // MMMainPanel
            // 
            this.MMMainPanel.Controls.Add(this.pnlDebitCreditNote);
            this.MMMainPanel.Controls.Add(this.btnPurchaseOrder);
            this.MMMainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.MMMainPanel.Size = new System.Drawing.Size(906, 293);
            this.MMMainPanel.Controls.SetChildIndex(this.btnPurchaseOrder, 0);
            this.MMMainPanel.Controls.SetChildIndex(this.pnlDebitCreditNote, 0);
            // 
            // btnPurchaseOrder
            // 
            this.btnPurchaseOrder.Location = new System.Drawing.Point(126, 16);
            this.btnPurchaseOrder.Margin = new System.Windows.Forms.Padding(2);
            this.btnPurchaseOrder.Name = "btnPurchaseOrder";
            this.btnPurchaseOrder.Size = new System.Drawing.Size(88, 22);
            this.btnPurchaseOrder.TabIndex = 2;
            this.btnPurchaseOrder.Text = "Purchase Order";
            this.btnPurchaseOrder.UseVisualStyleBackColor = true;
            this.btnPurchaseOrder.Click += new System.EventHandler(this.btnPurchaseOrder_Click);
            // 
            // pnlDebitCreditNote
            // 
            this.pnlDebitCreditNote.BackColor = System.Drawing.Color.PaleGreen;
            this.pnlDebitCreditNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDebitCreditNote.Controls.Add(this.txtDNAmountSelected);
            this.pnlDebitCreditNote.Controls.Add(this.txtCRAmountSelected);
            this.pnlDebitCreditNote.Controls.Add(this.btnOK);
            this.pnlDebitCreditNote.Controls.Add(this.label8);
            this.pnlDebitCreditNote.Controls.Add(this.label7);
            this.pnlDebitCreditNote.Controls.Add(this.dgPurchaseOrder);
            this.pnlDebitCreditNote.Controls.Add(this.lblDebitCreditNote);
            this.pnlDebitCreditNote.Location = new System.Drawing.Point(40, 55);
            this.pnlDebitCreditNote.Name = "pnlDebitCreditNote";
            this.pnlDebitCreditNote.Size = new System.Drawing.Size(762, 234);
            this.pnlDebitCreditNote.TabIndex = 1035;
            // 
            // txtDNAmountSelected
            // 
            this.txtDNAmountSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDNAmountSelected.CausesValidation = false;
            this.txtDNAmountSelected.Location = new System.Drawing.Point(139, 28);
            this.txtDNAmountSelected.Name = "txtDNAmountSelected";
            this.txtDNAmountSelected.Size = new System.Drawing.Size(70, 23);
            this.txtDNAmountSelected.TabIndex = 1028;
            this.txtDNAmountSelected.Text = "psLableWithBorderMiddleRight1";
            this.txtDNAmountSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCRAmountSelected
            // 
            this.txtCRAmountSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCRAmountSelected.CausesValidation = false;
            this.txtCRAmountSelected.Location = new System.Drawing.Point(139, 4);
            this.txtCRAmountSelected.Name = "txtCRAmountSelected";
            this.txtCRAmountSelected.Size = new System.Drawing.Size(70, 23);
            this.txtCRAmountSelected.TabIndex = 1027;
            this.txtCRAmountSelected.Text = "psLableWithBorderMiddleRight1";
            this.txtCRAmountSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtCRAmountSelected.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(522, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 29);
            this.btnOK.TabIndex = 1026;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(17, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 15);
            this.label8.TabIndex = 1025;
            this.label8.Text = "Selected DN Amount";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 15);
            this.label7.TabIndex = 1024;
            this.label7.Text = "Selected CN Amount";
            this.label7.Visible = false;
            // 
            // dgPurchaseOrder
            // 
            this.dgPurchaseOrder.AllowUserToDeleteRows = false;
            this.dgPurchaseOrder.AllowUserToResizeColumns = false;
            this.dgPurchaseOrder.AllowUserToResizeRows = false;
            this.dgPurchaseOrder.BackgroundColor = System.Drawing.Color.DarkSeaGreen;
            this.dgPurchaseOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPurchaseOrder.Location = new System.Drawing.Point(7, 54);
            this.dgPurchaseOrder.Name = "dgPurchaseOrder";
            this.dgPurchaseOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPurchaseOrder.Size = new System.Drawing.Size(745, 174);
            this.dgPurchaseOrder.TabIndex = 1022;
            this.dgPurchaseOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgPurchaseOrder_KeyDown);
            // 
            // lblDebitCreditNote
            // 
            this.lblDebitCreditNote.AutoSize = true;
            this.lblDebitCreditNote.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebitCreditNote.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblDebitCreditNote.Location = new System.Drawing.Point(339, 7);
            this.lblDebitCreditNote.Name = "lblDebitCreditNote";
            this.lblDebitCreditNote.Size = new System.Drawing.Size(170, 19);
            this.lblDebitCreditNote.TabIndex = 1012;
            this.lblDebitCreditNote.Text = "Debit/Credit Note List";
            this.lblDebitCreditNote.Visible = false;
            // 
            // UclUploadEmilan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UclUploadEmilan";
            this.Size = new System.Drawing.Size(906, 421);
            this.MMBottomPanel.ResumeLayout(false);
            this.MMBottomPanel.PerformLayout();
            this.MMMainPanel.ResumeLayout(false);
            this.MMMainPanel.PerformLayout();
            this.pnlDebitCreditNote.ResumeLayout(false);
            this.pnlDebitCreditNote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPurchaseOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPurchaseOrder;
        private System.Windows.Forms.Panel pnlDebitCreditNote;
        private CommonControls.PSLableWithBorderMiddleRight txtDNAmountSelected;
        private CommonControls.PSLableWithBorderMiddleRight txtCRAmountSelected;
        private PharmaSYSPlus.CommonLibrary.PSButton btnOK;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgPurchaseOrder;
        private System.Windows.Forms.Label lblDebitCreditNote;
    }
}
