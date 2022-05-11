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
    public partial class UclToolCompanyShortName : BaseControl
    {
        public UclToolCompanyShortName()
        {
            InitializeComponent();
         
            tsBtnCancel.Enabled = false;
            tsBtnDelete.Enabled = false;
            tsBtnPrint.Enabled = false;
            tsBtnSavenPrint.Enabled = false;
            tsBtnSave.Enabled = false;
            tsBtnSearch.Enabled = false;
        }
        private void psButton1_Click(object sender, EventArgs e)
        {
            DoReprocess();
        }

        private void psButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DoReprocess();
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

            LockTable.LockTablesForCompanyShortName();

           
            this.Cursor = Cursors.WaitCursor;
            StockReProcess stk = new StockReProcess();
           DataTable compdt = stk.ReadCompanyData();
           string compID = string.Empty;
           string compshortname = string.Empty;

           foreach (DataRow dr in compdt.Rows)
           {
               try
               {
                   compID = string.Empty;
                   compshortname = string.Empty;
                   if (dr["CompID"] != DBNull.Value)
                       compID = dr["CompID"].ToString();
                   if (dr["CompShortName"] != DBNull.Value)
                       compshortname = dr["CompShortName"].ToString();
                   stk.UpdateProductMaster(compID, compshortname);
                   retValue = true;
               }
               catch(Exception Ex)
               {
                   Log.WriteException(Ex);
               }
           }

            this.Cursor = Cursors.Default;
            if (retValue)
            {

             //   EcoMartCache.GetProductData();
                MessageBox.Show("Successfully Processed", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Could Not Process", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LockTable.UnLockTables();
           
        }
    }
}
