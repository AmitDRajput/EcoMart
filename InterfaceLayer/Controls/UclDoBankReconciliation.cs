using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using EcoMart.InterfaceLayer.CommonControls;
using PrintDataGrid;
using EcoMart.InterfaceLayer.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclDoBankReconciliation : BaseControl
    {

        #region Declaration
        private BankReConciliation _BankReConciliation;
        private MPReports _MPReports;
        private DataTable _BindingSource;
        DataTable _saledtable = new DataTable();
        DataTable _statementdtable = new DataTable();
        string _MFromDate;
        string _MToDate;
        string _BankID;
        double _DebitAmount;
        double _CreditAmount;
        string _IfNew;
        string _ClearedDate;
        string _VoucherID;
        string _VoucherType;

        private double _MOpeningDebit = 0;
        private double _MOpeningCredit = 0;
     //   private double _MTrDebit = 0;
    //    private double _MTrCredit = 0;
        private double _MClosingDebit = 0;
        private double _MClosingCredit = 0;
     //   private double _MDebit = 0;
    //    private double _MCredit = 0;
     //   private string _MAccountName = "";
        #endregion

        # region Constructor
        public UclDoBankReconciliation()
        {
            try
            {
                InitializeComponent();
                _BankReConciliation = new BankReConciliation();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region IDetail Control
        public override void SetFocus()
        {
            try
            {
                fromDate1.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool ClearData()
        {
            try
            {
                ClearControls();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                _BindingSource = new DataTable();
                _MPReports = new MPReports();
                ClearData();
                headerLabel1.Text = "DO BANK RECONCILIATION -> EDIT";
                FillBankAccountCombo();
                InitializeReportGrid();
                FormatReportGrid();
                pnlMultiSelection1.Visible = true;
                GetDefaultBank();
                fromDate1.Focus();
                tsBtnCancel.Visible = false;
                tsBtnExit.Visible = true;
                tsBtnSavenPrint.Visible = false;
                tsBtnSearch.Visible = false;

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public void GetDefaultBank()
        {
            string DefaultBankID = General.GetDefaultBank();
            if (DefaultBankID != null)
                mcbBankAccount.SelectedID = DefaultBankID;
        }
        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            ClearData();
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
        //    string voucherID = "";
            _ClearedDate = "";

            try
            {
                foreach (DataGridViewRow dr in mpMSVCBank.Rows)
                {
                    if (dr.Cells["Col_VoucherID"].Value != null && dr.Cells["Col_VoucherID"].Value.ToString() != string.Empty && dr.Cells["Col_ClearedDate"].Value != null  && dr.Cells["Col_VoucherType"].Value != null && dr.Cells["Col_VoucherType"].Value.ToString() != string.Empty)
                    {
                        _VoucherID = dr.Cells["Col_VoucherID"].Value.ToString();
                        _VoucherType = dr.Cells["Col_VoucherType"].Value.ToString();
                        _ClearedDate = dr.Cells["Col_ClearedDate"].Value.ToString();
                        if (_ClearedDate != "")
                            _ClearedDate = General.GetExpiryInyyyymmddForm(_ClearedDate);
                        retValue = _BankReConciliation.UpdatetblTrnac(_VoucherID, _ClearedDate);
                        if (_VoucherType == FixAccounts.VoucherTypeForBankPayment)
                            retValue = _BankReConciliation.UpdatetblForPayment(_VoucherID, _ClearedDate);
                        else if (_VoucherType == FixAccounts.VoucherTypeForBankReceipt)
                            retValue = _BankReConciliation.UpdatetblForReceipt(_VoucherID, _ClearedDate);
                        else if (_VoucherType == FixAccounts.VoucherTypeForBankExpenses || _VoucherType == FixAccounts.VoucherTypeForContraEntry)
                            retValue = _BankReConciliation.UpdatetblForExpenses(_VoucherID, _ClearedDate);

                    }
                    
                }
                retValue = true;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void ClearControls()
        {
            try
            {
                tsBtnFifth.Visible = false;
                pnlClearedDate.Visible = false;                
                mcbBankAccount.SelectedID = "";
                txtDebitAmount.Text = "0.00";
                txtCreditamount.Text = "0.00";
                cbOnlyNewEntries.Checked = false;
                DataTable dt = new DataTable();
                BindGrid(dt);
                pnlClearedDate.Visible = false;

               // mpMSVCBank.DataSource = dt;
               // mpMSVCBank.Bind();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillBankAccountCombo()
        {
            try
            {
                mcbBankAccount.SelectedID = null;
                mcbBankAccount.SourceDataString = new string[6] { "AccountID", "AccCode", "AccName", "AccAddress1","AccOpeningDebit", "AccOpeningCredit" };
                mcbBankAccount.ColumnWidth = new string[6] { "0", "20", "200", "200","0","0" };
                mcbBankAccount.DisplayColumnNo = 2;
                mcbBankAccount.ValueColumnNo = 0;
                mcbBankAccount.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetBankAccountList();
                mcbBankAccount.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion IDetail Control

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
             bool  retValue = false;

             if (keyPressed == Keys.Escape)
             {
                 if (pnlClearedDate.Visible == true)
                 {
                     pnlClearedDate.Visible = false;
                     mpMSVCBank.Focus();
                     retValue = true;
                 }
             }

             return retValue;
        }

        #region Construct columns
        private void ConstructReportColumns()
        {
            try
            {
                mpMSVCBank.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.Width = 100;              
                column.Visible = false;
                mpMSVCBank.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_AccID";
                //column.Visible = false;
                //column.Width = 80;
                //mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherID";
                column.Visible = false;
                column.Width = 100;
                column.ReadOnly = true;               
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "Date";
                column.Width = 80;
                column.ReadOnly = true;               
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.ReadOnly = true;
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.HeaderText = "Address";
                column.Width = 100;
                column.ReadOnly = true;
                mpMSVCBank.Columns.Add(column);              

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.ReadOnly = true;
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 70;
                column.ReadOnly = true;
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ChequeNumber";
                column.HeaderText = "Chq Number";
                column.Width = 100;
                column.DataPropertyName = "ChrqueNumber";
                mpMSVCBank.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ChequeDate";
                column.HeaderText = "Chq Date";
                column.Width = 90;
                column.ReadOnly = true;
                mpMSVCBank.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedDate";
                column.HeaderText = "Cl.Date";
                column.Width = 90;
                column.ReadOnly = false;
                column.DataPropertyName = "ClearedDate";
                mpMSVCBank.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Debit";
                column.HeaderText = "Debit";
                column.Width = 90;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Credit";
                column.HeaderText = "Credit";
                column.Width = 90;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVCBank.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Construct columns

        #region Events
        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            BtnOKMultiSelection1Click();
        }

        private void BtnOKMultiSelection1Click()
        {
            pnlMultiSelection1.Visible = false;
            InitializeReportGrid();
            FormatReportGrid();
            _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
            _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");


            DataTable dtable = new DataTable();
            dtable = _MPReports.GetGeneralLedgerByClearedDate(_MFromDate, _MToDate, mcbBankAccount.SelectedID);
            _BindingSource = dtable;
            if (mcbBankAccount.SelectedID != null && mcbBankAccount.SelectedID != "")
            {
                if (mcbBankAccount.SeletedItem.ItemData[4] != null && mcbBankAccount.SeletedItem.ItemData[4].ToString() != string.Empty)
                    _MOpeningDebit = Convert.ToDouble(mcbBankAccount.SeletedItem.ItemData[4].ToString());
                if (mcbBankAccount.SeletedItem.ItemData[5] != null && mcbBankAccount.SeletedItem.ItemData[5].ToString() != string.Empty)
                    _MOpeningCredit = Convert.ToDouble(mcbBankAccount.SeletedItem.ItemData[5].ToString());
                GetActualOpeningBalance(mcbBankAccount.SelectedID, _MFromDate);
            }
            if (_MOpeningDebit > 0)
            {
                txtOpeningBalance.Text = _MOpeningDebit.ToString("#0.00");
                txtOPTag.Text = "DB";
            }
            else if (_MOpeningCredit > 0)
            {
                txtOpeningBalance.Text = _MOpeningCredit.ToString("#0.00");
                txtOPTag.Text = "CR";
            }
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            _BankID = "";
            _DebitAmount = 0;
            _CreditAmount = 0;
            if (mcbBankAccount.SelectedID != null && mcbBankAccount.SelectedID != "")
                _BankID = mcbBankAccount.SelectedID;

            if (txtDebitAmount.Text != null && txtDebitAmount.Text.ToString() != "")
                _DebitAmount = Convert.ToDouble(txtDebitAmount.Text.ToString());
            if (txtCreditamount.Text != null && txtCreditamount.Text.ToString() != "")
                _CreditAmount = Convert.ToDouble(txtCreditamount.Text.ToString());
            if (cbOnlyNewEntries.Checked == true)
                _IfNew = "Y";
            else
                _IfNew = "N";
            GetBankReceiptData();
            mpMSVCBank.Select();

        }
        private void GetActualOpeningBalance(string accID, string fromDate)
        {
            double totdebit = 0;
            double totcredit = 0;
            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    double dd = Convert.ToDouble(dr["Debit"].ToString());
                    dd = Convert.ToDouble(dr["Credit"].ToString());
                    string ss = dr["AccountID"].ToString();
                    ss = dr["ClearedDate"].ToString();
                    if (ss != string.Empty)
                    {
                        if (dr["AccountID"].ToString() == accID && Convert.ToInt32(dr["ClearedDate"].ToString()) < Convert.ToInt32(fromDate))
                        {
                            if (dr["Debit"] != null && dr["Debit"].ToString() != "")
                                totdebit += Convert.ToDouble(dr["Debit"].ToString());
                            if (dr["Credit"] != null && dr["Credit"].ToString() != "")
                                totcredit += Convert.ToDouble(dr["Credit"].ToString());
                        }
                    }

                }
                _MOpeningDebit += totdebit;
                _MOpeningCredit += totcredit;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FormatReportGrid()
        {
            //mpMSVCBank.DateColumnNames.Add("Col_VoucherDate");
            //mpMSVCBank.DateColumnNames.Add("Col_ClearedDate");
            //mpMSVCBank.DateColumnNames.Add("Col_ChequeDate");
            //mpMSVCBank.DoubleColumnNames.Add("Col_Debit");
            //mpMSVCBank.DoubleColumnNames.Add("Col_Credit");
        }
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            mpMSVCBank.Columns["Col_ID"].Visible = false;
           // mpMSVCBank.InitializeColumnContextMenu();
        }

        private void GetBankReceiptData()
        {
            DataTable dtable = new DataTable();
            mpMSVCBank.Columns["Col_ClearedDate"].ReadOnly = false;
            if (_DebitAmount > 0)
            {
                if (_IfNew == "Y")
                {
                    dtable = _BankReConciliation.ReadDetailsDBNew(_MFromDate, _MToDate, _BankID, _DebitAmount);
                }
                else
                {
                    dtable = _BankReConciliation.ReadDetailsDBAll(_MFromDate, _MToDate, _BankID, _DebitAmount);
                }
            }
            else if (_CreditAmount > 0)
            {
                if (_IfNew == "Y")
                {
                    dtable = _BankReConciliation.ReadDetailsCRNew(_MFromDate, _MToDate, _BankID, _CreditAmount);
                }
                else
                {
                    dtable = _BankReConciliation.ReadDetailsCRAll(_MFromDate, _MToDate, _BankID, _CreditAmount);
                }
            }
            else if (_IfNew == "Y")
            {
                dtable = _BankReConciliation.ReadDetailsNew(_MFromDate, _MToDate, _BankID);
            }
            else
            {
                dtable = _BankReConciliation.ReadDetailsAll(_MFromDate, _MToDate, _BankID);
            }

            BindGrid(dtable);
          //  mpMSVCBank.Bind();
            GetClosingBalance();

        }

        private void BindGrid(DataTable dtable)
        {
            if (mpMSVCBank.Rows.Count > 0)
                mpMSVCBank.Rows.Clear();
            double amt = 0;
            try
            {
                if (dtable != null && dtable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtable.Rows)
                    {
                        if (dr["tblTrnacID"] != DBNull.Value)
                        {
                            int rowindex = mpMSVCBank.Rows.Add();

                            mpMSVCBank.Rows[rowindex].Cells["Col_ID"].Value = dr["tblTrnacID"].ToString();
                            mpMSVCBank.Rows[rowindex].Cells["Col_VoucherID"].Value = dr["VoucherID"].ToString();
                            mpMSVCBank.Rows[rowindex].Cells["Col_VoucherDate"].Value = General.GetDateInDateFormat(dr["TransactionDate"].ToString());
                            mpMSVCBank.Rows[rowindex].Cells["Col_AccName"].Value = dr["AccName"].ToString();
                            mpMSVCBank.Rows[rowindex].Cells["Col_Address"].Value = dr["AccAddress1"].ToString();
                            mpMSVCBank.Rows[rowindex].Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                            mpMSVCBank.Rows[rowindex].Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                            mpMSVCBank.Rows[rowindex].Cells["Col_ChequeNumber"].Value = dr["ChequeNumber"].ToString();
                            mpMSVCBank.Rows[rowindex].Cells["Col_ChequeDate"].Value = General.GetDateInDateFormat(dr["ChequeDate"].ToString());
                            mpMSVCBank.Rows[rowindex].Cells["Col_ClearedDate"].Value = General.GetDateInDateFormat(dr["ClearedDate"].ToString());
                            amt = 0;
                            if (dr["debit"] != DBNull.Value && dr["debit"].ToString() != "")
                                amt = Convert.ToDouble(dr["debit"].ToString());
                            mpMSVCBank.Rows[rowindex].Cells["Col_Debit"].Value = amt.ToString("#0.00");
                            amt = 0;
                            if (dr["credit"] != DBNull.Value && dr["credit"].ToString() != "")
                                amt = Convert.ToDouble(dr["credit"].ToString());
                            mpMSVCBank.Rows[rowindex].Cells["Col_Credit"].Value = amt.ToString("#0.00");





                            //mpMSVCBank["Col_ID", rowindex].Value = dr["tblTrnacID"].ToString();
                            //mpMSVCBank["Col_VoucherID", rowindex].Value = dr["VoucherID"].ToString();
                            //mpMSVCBank["Col_VoucherDate", rowindex].Value = General.GetDateInDateFormat(dr["TransactionDate"].ToString());
                            //mpMSVCBank["Col_AccName", rowindex].Value = dr["AccName"].ToString();
                            //mpMSVCBank["Col_Address", rowindex].Value = dr["AccAddress1"].ToString();
                            //mpMSVCBank["Col_VoucherType", rowindex].Value = dr["VoucherType"].ToString();
                            //mpMSVCBank["Col_VoucherNumber", rowindex].Value = dr["VoucherNumber"].ToString();
                            //mpMSVCBank["Col_ChequeNumber", rowindex].Value = dr["ChequeNumber"].ToString();
                            //mpMSVCBank["Col_ChequeDate", rowindex].Value = General.GetDateInDateFormat(dr["ChequeDate"].ToString());
                            //mpMSVCBank["Col_ClearedDate", rowindex].Value = General.GetDateInDateFormat(dr["ClearedDate"].ToString());
                            //amt = 0;
                            //if (dr["debit"] != DBNull.Value && dr["debit"].ToString() != "")
                            //    amt = Convert.ToDouble(dr["debit"].ToString());
                            //mpMSVCBank["Col_Debit", rowindex].Value = amt.ToString("#0.00");
                            //amt = 0;
                            //if (dr["credit"] != DBNull.Value && dr["credit"].ToString() != "")
                            //    amt = Convert.ToDouble(dr["credit"].ToString());
                            //mpMSVCBank["Col_Credit", rowindex].Value = amt.ToString("#0.00");
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void GetClosingBalance()
        {
            double mdebit = 0;
            double mcredit = 0;
            double mtotaldebit = 0;
            double mtotalcredit = 0;
            

            foreach (DataGridViewRow dr in mpMSVCBank.Rows)
            {
                mdebit = 0;
                mcredit = 0;
               
                if (dr.Cells["Col_ClearedDate"].Value != null && dr.Cells["Col_ClearedDate"].Value.ToString().Trim() != "")
                {
                    
                    if (dr.Cells["Col_Debit"] != null && dr.Cells["Col_Debit"].ToString() != "")
                        mdebit = Convert.ToDouble(dr.Cells["Col_Debit"].Value.ToString());
                    if (dr.Cells["Col_Credit"] != null && dr.Cells["Col_Credit"].ToString() != "")
                        mcredit = Convert.ToDouble(dr.Cells["Col_Credit"].Value.ToString());    
                }

                mtotaldebit += mdebit;
                mtotalcredit += mcredit;
            }

            _MClosingDebit = _MOpeningDebit - _MOpeningCredit + mtotaldebit - mtotalcredit;
            if (_MClosingDebit < 0)
            {
                _MClosingCredit = _MClosingDebit * (-1);
                _MClosingDebit = 0;
            }

            if (_MClosingDebit > 0)
            {
                txtClosingBalance.Text = _MClosingDebit.ToString("#0.00");
                txtCLTag.Text = "DB";
            }
            else
            {
                txtClosingBalance.Text = _MClosingCredit.ToString("#0.00");
                txtCLTag.Text = "CR";
            }
        }


        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }


        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbBankAccount.Focus();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
        }      

        private void mcbBankAccountDoubledClicked()
        {
           
            pnlClearedDate.Visible = true;

            if (mpMSVCBank.Rows[mpMSVCBank.CurrentRow.Index].Cells["Col_ClearedDate"].Value != null && mpMSVCBank.Rows[mpMSVCBank.CurrentRow.Index].Cells["Col_ClearedDate"].Value.ToString() != string.Empty)
                _ClearedDate = mpMSVCBank.Rows[mpMSVCBank.CurrentRow.Index].Cells["Col_ClearedDate"].Value.ToString();
            else
                _ClearedDate = mpMSVCBank.Rows[mpMSVCBank.CurrentRow.Index].Cells["Col_ChequeDate"].Value.ToString();



            //if (mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value != null && mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value.ToString() != string.Empty)
            //    _ClearedDate = mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value.ToString();
            //else
            //    _ClearedDate = mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ChequeDate"].Value.ToString();
            _ClearedDate = General.GetExpiryInyyyymmddForm(_ClearedDate);
            clearedDate.Value = General.ConvertStringToDateyyyyMMdd(_ClearedDate);
           // clearedDate.Value = Convert.ToDateTime(_ClearedDate);
          //  clearedDate.Value = new DateTime(Convert.ToInt32(_ClearedDate.Substring(0, 4)), Convert.ToInt32(_ClearedDate.Substring(4, 2)), Convert.ToInt32(_ClearedDate.Substring(6, 2)));
            clearedDate.Focus();
        }

        private void mcbBankAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDebitAmount.Focus();
            else if (e.KeyCode == Keys.Up)
                toDate1.Focus();
        }
        #endregion Events

        private void mpMSVCBank_DoubleClicked(object sender, EventArgs e)
        {   
            mcbBankAccountDoubledClicked();
        }

        private void clearedDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _ClearedDate = clearedDate.Value.ToString("yyyyMMdd");
                mpMSVCBank.Rows[mpMSVCBank.CurrentRow.Index].Cells["Col_ClearedDate"].Value = General.GetDateInDateFormat(_ClearedDate);
                //mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value = General.GetDateInDateFormat(_ClearedDate);
                pnlClearedDate.Visible = false;
                GetClosingBalance();
                mpMSVCBank.Select();
            }
        }

        private void txtDebitAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            txtCreditamount.Focus();
        }

        private void mcbBankAccount_EnterKeyPressed(object sender, EventArgs e)
        {
            txtDebitAmount.Focus();
        }

        private void pnlClearedDate_Click(object sender, EventArgs e)
        {
            pnlClearedDateClicked();
        }

        private void pnlClearedDateClicked()
        {
            _ClearedDate = clearedDate.Value.ToString("yyyyMMdd");
            mpMSVCBank.Rows[mpMSVCBank.CurrentRow.Index].Cells["Col_ClearedDate"].Value = General.GetDateInDateFormat(_ClearedDate);
            //mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value = General.GetDateInDateFormat(_ClearedDate);
            pnlClearedDate.Visible = false;
            mpMSVCBank.Focus();
        }

        private void btnRemoveClearedDate_Click(object sender, EventArgs e)
        {
            
            _ClearedDate = "";
            mpMSVCBank.Rows[mpMSVCBank.CurrentRow.Index].Cells["Col_ClearedDate"].Value = _ClearedDate;
            //mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value = _ClearedDate;
            GetClosingBalance();
            pnlClearedDate.Visible = false;
            mpMSVCBank.Focus();
        }

        private void mpMSVCBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                mcbBankAccountDoubledClicked();
            }
        }

        private void txtCreditamount_KeyDown(object sender, KeyEventArgs e)
        {
            btnOKMultiSelection1.Focus();
        }       

        private void clearedDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                pnlClearedDateClicked();

        } 
      
      
    }
}
