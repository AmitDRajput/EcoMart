using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;
using PrintDataGrid;
using PharmaSYSRetailPlus.InterfaceLayer.Classes;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclDoBankReconciliation : BaseControl
    {

        #region Declaration
        private BankReConciliation _BankReConciliation;
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
                ClearData();
                headerLabel1.Text = "DO BANK RECONCILIATION -> EDIT";
                FillBankAccountCombo();
                InitializeReportGrid();
                FormatReportGrid();
                pnlMultiSelection1.Visible = true;
                GetDefaultBank();
                fromDate1.Focus();

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
                        retValue = _BankReConciliation.UpdatetblTrnac(_VoucherID, _ClearedDate);
                        if (_VoucherType == FixAccounts.VoucherTypeForBankPayment || _VoucherType == FixAccounts.VoucherTypeForBankExpenses)
                            retValue = _BankReConciliation.UpdatetblForPayment(_VoucherID, _ClearedDate);
                        else if (_VoucherType == FixAccounts.VoucherTypeForBankReceipt)
                            retValue = _BankReConciliation.UpdatetblForReceipt(_VoucherID, _ClearedDate);

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
                pnlClearedDate.Visible = false;
                mcbBankAccount.SelectedID = "";
                txtDebitAmount.Text = "0.00";
                txtCreditamount.Text = "0.00";
                cbOnlyNewEntries.Checked = false;
                DataTable dt = new DataTable();
                mpMSVCBank.DataSource = dt;
                mpMSVCBank.Bind();
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
                mcbBankAccount.SourceDataString = new string[4] { "AccountID", "AccCode", "AccName", "AccAddress1" };
                mcbBankAccount.ColumnWidth = new string[4] { "0", "20", "200", "200" };
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
                column.DataPropertyName = "tblTrnacID";
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
                column.DataPropertyName = "VoucherID";
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "Date";
                column.Width = 90;
                column.DataPropertyName = "TransactionDate";
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.DataPropertyName = "AccName";
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.HeaderText = "Address";
                column.Width = 100;
                column.DataPropertyName = "AccAddress1";
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.DataPropertyName = "VoucherType";
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 70;
                column.DataPropertyName = "VoucherNumber";
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
                column.DataPropertyName = "ChequeDate";
                mpMSVCBank.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedDate";
                column.HeaderText = "Cl.Date";
                column.Width = 90;
                column.DataPropertyName = "ClearedDate";
                mpMSVCBank.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Debit";
                column.HeaderText = "Debit";
                column.Width = 90;
                column.DataPropertyName = "Debit";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVCBank.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Credit";
                column.HeaderText = "Credit";
                column.Width = 90;
                column.DataPropertyName = "Credit";
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
            //   BindReportGrid();

        }

        private void FormatReportGrid()
        {
            mpMSVCBank.DateColumnNames.Add("Col_VoucherDate");
            mpMSVCBank.DateColumnNames.Add("Col_ClearedDate");
            mpMSVCBank.DateColumnNames.Add("Col_ChequeDate");
            mpMSVCBank.DoubleColumnNames.Add("Col_Debit");
            mpMSVCBank.DoubleColumnNames.Add("Col_Credit");
        }
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            mpMSVCBank.Columns["Col_ID"].Visible = false;
            mpMSVCBank.InitializeColumnContextMenu();
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
           
            mpMSVCBank.DataSource = dtable;
            mpMSVCBank.Bind();

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
            if (mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value != null && mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value.ToString() != string.Empty)
            _ClearedDate = mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value.ToString();
            else                           
                _ClearedDate = mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ChequeDate"].Value.ToString();


            clearedDate.Value = new DateTime(Convert.ToInt32(_ClearedDate.Substring(0, 4)), Convert.ToInt32(_ClearedDate.Substring(4, 2)), Convert.ToInt32(_ClearedDate.Substring(6, 2)));
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
                mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value = _ClearedDate;
                pnlClearedDate.Visible = false;
               // mpMSVCBank.SetFocus();
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
            _ClearedDate = clearedDate.Value.ToString("yyyyMMdd");
            mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value = _ClearedDate;
            pnlClearedDate.Visible = false;
        }

        private void btnRemoveClearedDate_Click(object sender, EventArgs e)
        {
            _ClearedDate = "";
            mpMSVCBank.Rows[mpMSVCBank.SelectedRow.Index].Cells["Col_ClearedDate"].Value = _ClearedDate;
            pnlClearedDate.Visible = false;
        }
    }
}
