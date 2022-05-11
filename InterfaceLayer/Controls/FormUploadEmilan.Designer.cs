namespace EcoMart.InterfaceLayer
{
    partial class FormUploadEmilan
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlDebitCreditNote = new System.Windows.Forms.Panel();
            this.btnOK = new PharmaSYSPlus.CommonLibrary.PSButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgPurchaseOrder = new System.Windows.Forms.DataGridView();
            this.lblDebitCreditNote = new System.Windows.Forms.Label();
            this.btnPurchaseOrder = new System.Windows.Forms.Button();
            this.txtDNAmountSelected = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.txtCRAmountSelected = new EcoMart.InterfaceLayer.CommonControls.PSLableWithBorderMiddleRight(this.components);
            this.pnlDebitCreditNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPurchaseOrder)).BeginInit();
            this.SuspendLayout();
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
            this.pnlDebitCreditNote.Location = new System.Drawing.Point(69, 128);
            this.pnlDebitCreditNote.Name = "pnlDebitCreditNote";
            this.pnlDebitCreditNote.Size = new System.Drawing.Size(772, 334);
            this.pnlDebitCreditNote.TabIndex = 1037;
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPurchaseOrder.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPurchaseOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPurchaseOrder.Size = new System.Drawing.Size(743, 275);
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
            // btnPurchaseOrder
            // 
            this.btnPurchaseOrder.Location = new System.Drawing.Point(165, 57);
            this.btnPurchaseOrder.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPurchaseOrder.Name = "btnPurchaseOrder";
            this.btnPurchaseOrder.Size = new System.Drawing.Size(88, 22);
            this.btnPurchaseOrder.TabIndex = 1036;
            this.btnPurchaseOrder.Text = "Purchase Order";
            this.btnPurchaseOrder.UseVisualStyleBackColor = true;
            this.btnPurchaseOrder.Visible = false;
            this.btnPurchaseOrder.Click += new System.EventHandler(this.btnPurchaseOrder_Click);
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
            this.txtDNAmountSelected.Visible = false;
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
            // FormUploadEmilan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 516);
            this.Controls.Add(this.pnlDebitCreditNote);
            this.Controls.Add(this.btnPurchaseOrder);
            this.Name = "FormUploadEmilan";
            this.Text = "FormUploadEmilan";
            this.pnlDebitCreditNote.ResumeLayout(false);
            this.pnlDebitCreditNote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPurchaseOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDebitCreditNote;
        private CommonControls.PSLableWithBorderMiddleRight txtDNAmountSelected;
        private CommonControls.PSLableWithBorderMiddleRight txtCRAmountSelected;
        private PharmaSYSPlus.CommonLibrary.PSButton btnOK;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgPurchaseOrder;
        private System.Windows.Forms.Label lblDebitCreditNote;
        private System.Windows.Forms.Button btnPurchaseOrder;
    }
}