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
    public partial class UclToolRewrite : BaseControl
    {
        public UclToolRewrite()
        {
            InitializeComponent();
            
            
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
            return base.Exit();
        }

        private void DoReprocess()
        {
           // bool retValue = false;
            lblFooterMessage.Text = "Please Wait...";
            lblFooterMessage.Refresh();


            // LockTable.LockTablesForStockReProcess();
            if (rbtnCheckFixAccounts.Checked == true)
            {
                this.Cursor = Cursors.WaitCursor;
                ToolRewrite trw = new ToolRewrite();
                DataTable dt;
                dt  = trw.ReadFixAccounts();
                string maccid = "";
                string macccode = "";
                string maccname = "";
                string mgroupid = "";
                foreach (DataRow dr in dt.Rows)
                {
                    maccid = "";
                    macccode = "";
                    maccname = "";
                    mgroupid = "";

                    if (dr["AccountID"] != DBNull.Value)
                        maccid = dr["AccountID"].ToString();
                    if (dr["AccCode"] != DBNull.Value)
                        macccode = dr["AccCode"].ToString();
                    if (dr["AccName"] != DBNull.Value)
                        maccname = dr["AccName"].ToString();
                    if (dr["AccGroupID"] != DBNull.Value)
                        mgroupid = dr["AccGroupID"].ToString();
                    bool ifexists = false;
                    if (maccid != "")
                    {
                        ifexists = trw.CheckmasterAccountForID(maccid);
                        if (!ifexists)
                        {
                            trw.UpdatemasterAccount(maccid, macccode, maccname, mgroupid);
                        }
                    }
                }

                this.Cursor = Cursors.Default;
               
                MessageBox.Show("Successfully Processed", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                psButton1.Visible = false;
                txtpasswd.Text = "";
                txtpasswd.Focus();
                lblFooterMessage.Text = "";
                LockTable.UnLockTables();
            }
            else if (rbtnFillTransactionDateForSale.Checked == true)
            {
                this.Cursor = Cursors.WaitCursor;
                ToolRewrite trw = new ToolRewrite();
                DataTable dt;
                DataRow   drs;
                dt = trw.ReadDataFromtblTrnacForBlankTransactionDate();
                string mvoucherID = "";
                string mdate = "";                
                foreach (DataRow dr in dt.Rows)
                {
                    mvoucherID = "";
                    mdate = "";

                    if (dr["VoucherID"] != DBNull.Value)
                        mvoucherID = dr["VoucherID"].ToString();
                   
                   
                    if (mvoucherID != "")
                    {
                        drs = null;
                        drs = trw.ReadDateFromvouchersale(mvoucherID);
                        if (drs != null )
                        {
                            if (drs["VoucherDate"] != DBNull.Value && drs["VoucherDate"].ToString() != string.Empty)
                            {
                                mdate = drs["VoucherDate"].ToString();
                                bool retVal = trw.UpdateDateIntbltrnac(mvoucherID, mdate);
                            }
                            
                        }
                    }
                }

                this.Cursor = Cursors.Default;

                MessageBox.Show("Successfully Processed", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                psButton1.Visible = false;
                txtpasswd.Text = "";
                txtpasswd.Focus();
                lblFooterMessage.Text = "";
                LockTable.UnLockTables();
            }
            else if (rbtnRefillPurchase.Checked == true)
            {
                this.Cursor = Cursors.WaitCursor;
                ToolRewrite trw = new ToolRewrite();
                DataTable dt;
              //  DataRow   drs;
                bool retValue = trw.DeletePurchaseRecordsFromtblTrnac();
                dt = trw.ReadPurchaseVouchers();
                Purchase _purchase = new Purchase();
                string mvoucherID = "";
               // string mdate = "";                
                foreach (DataRow drow in dt.Rows)
                {                   

                    if (drow["PurchaseID"] != DBNull.Value)
                        mvoucherID = drow["PurchaseID"].ToString();
                   
                  
                    if (mvoucherID != "")
                    {
                         _purchase = new Purchase();
                         _purchase.Id = mvoucherID;
                         _purchase.ReadDetailsByID();
                         _purchase.AddAccountDetails(); 
                    }
                }

                this.Cursor = Cursors.Default;

                MessageBox.Show("Successfully Processed", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                psButton1.Visible = false;
                txtpasswd.Text = "";
                txtpasswd.Focus();
                lblFooterMessage.Text = "";
                LockTable.UnLockTables();
            }
            
                      
        }


        private void txtpasswd_KeyDown(object sender, KeyEventArgs e)
        {
            string mpassword = "";
            if (e.KeyCode == Keys.Enter)
            {
                if (txtpasswd.Text != null)
                    mpassword = txtpasswd.Text.ToString().Trim().ToUpper();

                if (rbtnFillTransactionDateForSale.Checked == true)
                {

                    if (mpassword != "TRANSACTIONDATE")
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
                if (rbtnRefillPurchase.Checked == true)
                {

                    if (mpassword != "REFILLPURCHASE")
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
                if (rbtnCheckFixAccounts.Checked == true)
                {

                    if (mpassword != "FIXACCOUNTS")
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

        private void UclToolRewrite_Leave(object sender, EventArgs e)
        {
            tsBtnAdd.Enabled = false;
            tsBtnFifth.Enabled = false;
            tsBtnEdit.Enabled = false;
            tsBtnCancel.Enabled = false;
            tsBtnDelete.Enabled = false;
            tsBtnPrint.Enabled = false;
            tsBtnSavenPrint.Enabled = false;
            tsBtnSave.Enabled = false;
            tsBtnSearch.Enabled = false;
            psButton1.Visible = false;
            txtpasswd.Text = "";
            rbtnFillTransactionDateForSale.Checked = true;
            rbtnFillTransactionDateForSale.Focus();
        }
    }
}
