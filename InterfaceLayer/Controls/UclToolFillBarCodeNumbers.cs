using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.BusinessLayer;
using EcoMart.Common;

namespace EcoMart.InterfaceLayer
{
     [System.ComponentModel.ToolboxItem(false)]
    public partial class UclToolFillBarCodeNumbers : BaseControl
    {
        public UclToolFillBarCodeNumbers()
        {
            InitializeComponent();
            tsBtnCancel.Enabled = false;
            tsBtnDelete.Enabled = false;
            tsBtnPrint.Enabled = false;
            tsBtnSavenPrint.Enabled = false;
            tsBtnSave.Enabled = false;
            tsBtnSearch.Enabled = false;
            psButton1.Visible = false;
            txtpasswd.Text = "";
            txtpasswd.Focus();
        }
        private void psButton1_Click(object sender, EventArgs e)
        {
            DoReprocess();
        }

        private void psButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoReprocess();
            }
        }

        public override bool Exit()
        {
            tsBtnCancel.Enabled = true;
            tsBtnDelete.Enabled = true;
            tsBtnPrint.Enabled = true;
            tsBtnSavenPrint.Enabled = true;
            tsBtnSave.Enabled = true;
            tsBtnSearch.Enabled = true;
            return base.Exit();

        }
        private void DoReprocess()
        {
            bool retValue = false;
            lblFooterMessage.Text = "Please Wait...";
            lblFooterMessage.Refresh();
             
            int prodno = 100000;
           

            BarCode _BarCode = new BarCode();
            _BarCode.GetProductAndBatchNumber();
            // LockTable.LockTablesForStockReProcess();

            if (_BarCode.LastProductNumberForBarCode > 0)
                prodno = _BarCode.LastProductNumberForBarCode;

            prodno = prodno + 1;
            this.Cursor = Cursors.WaitCursor;
            BarCode barCode = new BarCode();
            retValue = barCode.CheckIfProcedureIsAlreadyDone();
            if (!retValue)
            {
                barCode.FillBarCodeNumbers(prodno);
               // barCode.FillBarcodeIntblStock();
            }
        

          //  retValue = barCode.UpdateMasterProduct();
            this.Cursor = Cursors.Default;
            //if (retValue)
            //{
                MessageBox.Show("Successfully Processed", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Could Not Process", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            psButton1.Visible = false;
            txtpasswd.Text = "";
            txtpasswd.Focus();
            lblFooterMessage.Text = "";
            LockTable.UnLockTables();
            // General.NotifyProductListRefill();            
        }


        private void txtpasswd_KeyDown(object sender, KeyEventArgs e)
        {
            string mpassword = "";
            if (e.KeyCode == Keys.Enter)
            {
                if (txtpasswd.Text != null)
                    mpassword = txtpasswd.Text.ToString().Trim().ToUpper();

                if (mpassword != "GENERATE")
                {
                    txtpasswd.Text = "";
                    txtpasswd.Focus();
                }
                else
                {
                    psButton1.Visible = true;
                    psButton1.Focus();
                }
            }
        }
    }
}
