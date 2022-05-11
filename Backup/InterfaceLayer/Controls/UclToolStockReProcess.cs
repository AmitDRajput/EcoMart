using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSRetailPlus.Common;


namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclStockReProcess : BaseControl
    {
        public UclStockReProcess()
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
           
          
            LockTable.LockTablesForStockReProcess();
           
            this.Cursor = Cursors.WaitCursor;
            StockReProcess stk = new StockReProcess();
            retValue =  stk.RemoveNegetiveStockFromtblStock();
            retValue = stk.InitializeMasterProduct();
            
             retValue =  stk.UpdateMasterProduct();
            this.Cursor = Cursors.Default;
            if (retValue)
            {               
                MessageBox.Show("Successfully Processed", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Could Not Process", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            psButton1.Visible = false;
            txtpasswd.Text = "";
            txtpasswd.Focus();
            lblFooterMessage.Text = "";
            LockTable.UnLockTables();
            General.NotifyProductListRefill();            
        }
     

        private void txtpasswd_KeyDown(object sender, KeyEventArgs e)
        {
            string mpassword = "";
            if (e.KeyCode == Keys.Enter)
            {
                if (txtpasswd.Text != null)
                    mpassword = txtpasswd.Text.ToString().Trim().ToUpper();

                if (mpassword != "UPDATE")
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
