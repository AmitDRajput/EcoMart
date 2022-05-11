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
using EcoMart.InterfaceLayer.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclContraEntry : BaseControl
    {
        #region Declaration
        private ContraEntry _ContraEntry;
        #endregion

        # region Constructor
        public UclContraEntry()
        {
             try
            {
            InitializeComponent();
            _ContraEntry = new ContraEntry();
            SearchControl = new UclContraEntrySearch();
            }
             catch (Exception Ex)
             {
                 Log.WriteException(Ex);
             }
        }
        #endregion

        #region IDetail Control
        public override void SetFocus()
        {
            try
            {
                if (_Mode == OperationMode.Add)
                    mcbCreditor.Focus();
                else
                    txtVouchernumber.Focus();
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
                _ContraEntry.Initialise();
                ClearControls();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        public override bool Add()
        {

            bool retValue = base.Add();
            try
            {
                ClearData();              
                headerLabel1.Text = "CONTRA ENTRY -> NEW";
                AddToolTip();
                FillPartyCombo();
                FillmcbAccountTobeCredited();
                mcbCreditor.Enabled = true;              
                pnlNameAddress.Enabled = true;
                pnlVou.Enabled = true;
                datePickerBillDate.Value = DateTime.Now;
                mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return retValue;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                ClearData();
                headerLabel1.Text = "CONTRA ENTRY -> EDIT";
                FillPartyCombo();
                FillmcbAccountTobeCredited();
                mcbCreditor.Enabled = false;              
                pnlNameAddress.Enabled = true;
                pnlVou.Enabled = true;
                AddToolTip();
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                txtVouchernumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            try
            {
                pnlNameAddress.Enabled = true;
                ClearData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "CONTRA ENTRY -> DELETE";
                ClearData();             
                FillPartyCombo();
                FillmcbAccountTobeCredited();
                mcbCreditor.Enabled = false;             
                pnlNameAddress.Enabled = true;
                pnlVou.Enabled = true;            
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                txtVouchernumber.Focus();

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;

            try
            {
                if (_ContraEntry.Id != null && _ContraEntry.Id != "")
                {
                    LockTable.LockTablesForCashBankExpenses();

                    if (_ContraEntry.CanBeDeleted())
                    {
                        General.BeginTransaction();
                        retValue = _ContraEntry.DeleteDetails();
                        if (retValue)
                            retValue = _ContraEntry.DeletePreviousRecords();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            MessageBox.Show("Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            retValue = true;
                        }
                        else
                        {
                            MessageBox.Show("Could not Delete...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            retValue = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                pnlNameAddress.Enabled = true;
                ClearData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
                retValue = false;
            }
            return retValue;
        }

        public override bool View()
        {
            bool retValue = base.View();
            try
            {
                headerLabel1.Text = "CONTRA ENTRY -> VIEW";
                ClearData();              
                FillPartyCombo();
                FillmcbAccountTobeCredited();
                mcbCreditor.Enabled = false;              
                pnlNameAddress.Enabled = true;             
                pnlVou.Enabled = true;
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                txtVouchernumber.Focus();
              //  GetLastRecord();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private void GetLastRecord()
        {
            try
            {
                if (txtVouType.Text == null || txtVouType.Text.ToString().Trim() == string.Empty)
                {
                    _ContraEntry.CBVouType = FixAccounts.VoucherTypeForContraEntry;
                }
                _ContraEntry.GetLastRecord();
                FillSearchData(_ContraEntry.Id, "");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _ContraEntry.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _ContraEntry.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _ContraEntry.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _ContraEntry.GetFirstRecord();
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _ContraEntry.Id = dr["CBID"].ToString();
                FillSearchData(_ContraEntry.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _ContraEntry.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _ContraEntry.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _ContraEntry.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _ContraEntry.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _ContraEntry.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _ContraEntry.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _ContraEntry.CBVouNo = i;
                dr = _ContraEntry.ReadDetailsByVouNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _ContraEntry.Id = dr["CBID"].ToString();
                FillSearchData(_ContraEntry.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _ContraEntry.GetLastVoucherNumber(FixAccounts.VoucherTypeForContraEntry, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _ContraEntry.CBVouType = txtVouType.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _ContraEntry.CBVouNo = i;
                dr = _ContraEntry.ReadDetailsByVouNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _ContraEntry.Id = dr["CBID"].ToString();
                FillSearchData(_ContraEntry.Id, "");
            }
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                _ContraEntry.CBAccountID = mcbCreditor.SelectedID;              
                _ContraEntry.CBVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                _ContraEntry.CBAccountIDTobeCredited = mcbAccountTobeCredited.SelectedID;
                _ContraEntry.CBAmount = Convert.ToDouble(txtAmount.Text.ToString());
                if (txtNarration.Text != null)
                    _ContraEntry.CBNarration = txtNarration.Text.ToString();
                _ContraEntry.Validate();
                if (_ContraEntry.IsValid)
                {
                    LockTable.LockTablesForCashBankExpenses();
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        General.BeginTransaction();
                        _ContraEntry.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _ContraEntry.CBVouNo = _ContraEntry.GetAndUpdateCPENumber(General.ShopDetail.ShopVoucherSeries);
                        _ContraEntry.CreatedBy = General.CurrentUser.Id;
                        _ContraEntry.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _ContraEntry.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _ContraEntry.AddDetails();                       
                        if (retValue)
                            retValue = SaveIntblTrnac();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            string msgLine2 = _ContraEntry.CBVouType + "  " + _ContraEntry.CBVouNo.ToString("#0");
                            PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                            // MessageBox.Show("Cash Expenses saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SavedID = _ContraEntry.Id;
                            retValue = true;
                        }
                        else
                        {
                            MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            retValue = false;
                        }
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        General.BeginTransaction();
                        _ContraEntry.ModifiedBy = General.CurrentUser.Id;
                        _ContraEntry.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _ContraEntry.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _ContraEntry.UpdateDetails();
                        if (retValue)
                            retValue = DeletePreviousEntry();                       
                        if (retValue)
                            retValue = SaveIntblTrnac();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            MessageBox.Show("Cash Expenses Updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SavedID = _ContraEntry.Id;
                            retValue = true;
                        }
                        else
                        {
                            MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            retValue = false;
                        }
                    }
                }
                else // Show Validation Messages
                {
                    LockTable.UnLockTables();
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _ContraEntry.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            LockTable.UnLockTables();
            return retValue;
        }


        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    FillPartyCombo();
                    FillmcbAccountTobeCredited();
                    _ContraEntry.Id = ID;
                    _ContraEntry.ReadDetailsByID();
                    mcbCreditor.SelectedID = _ContraEntry.CBAccountID;
                    mcbAccountTobeCredited.SelectedID = _ContraEntry.CBAccountIDTobeCredited;
                    txtNarration.Text = _ContraEntry.CBNarration;
                    txtVouchernumber.Text = _ContraEntry.CBVouNo.ToString();
                    txtAmount.Text = _ContraEntry.CBAmount.ToString("#0.00");
                    DateTime mydate = new DateTime(Convert.ToInt32(_ContraEntry.CBVouDate.Substring(0, 4)), Convert.ToInt32(_ContraEntry.CBVouDate.Substring(4, 2)), Convert.ToInt32(_ContraEntry.CBVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    if (_Mode == OperationMode.Edit)
                        mcbCreditor.Enabled = true;
                    mcbCreditor.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        #endregion IDetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
        {
            try
            {
                if (closedControl is UclAccount)
                {
                    FillPartyCombo();
                    FillmcbAccountTobeCredited();

                    Account Acc = new Account();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {
                if (keyPressed == Keys.Escape)
                {
                    retValue = Exit();
                }
                if (retValue == false)
                {
                    retValue = base.HandleShortcutAction(keyPressed, modifier);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        #endregion

        #region Other Private Methods
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }
        private void ClearControls()
        {
            try
            {
                tsBtnFifth.Visible = false;
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtVoucherSeries.Text = _ContraEntry.CBVouSeries;
                txtVouType.Text = FixAccounts.VoucherTypeForContraEntry;
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                mcbCreditor.SelectedID = "";
                mcbAccountTobeCredited.SelectedID = "";
                txtAmount.Text = "0.00";
                this.mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillPartyCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2" };
                mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "200", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetOverviewDataForContraEntry();
                mcbCreditor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillmcbAccountTobeCredited()
        {
            try
            {
                mcbAccountTobeCredited.SelectedID = null;
                mcbAccountTobeCredited.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2" };
                mcbAccountTobeCredited.ColumnWidth = new string[5] { "0", "20", "200", "200", "0" };
                mcbAccountTobeCredited.DisplayColumnNo = 2;
                mcbAccountTobeCredited.ValueColumnNo = 0;
                mcbAccountTobeCredited.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetOverviewDataForContraEntry();
                mcbAccountTobeCredited.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool DeletePreviousEntry()
        {
            bool returnVal = false;
            try
            {
                returnVal = _ContraEntry.DeletePreviousRecords();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return returnVal;
        }
      

      

      

       

        #endregion

        #region Events

        #region Construct columns

      

        #endregion



        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
          
            txtNarration.Focus();
        }
      

      
       

       
        private bool SaveIntblTrnac()
        {
           
            bool retValue = false;

            _ContraEntry.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            retValue = _ContraEntry.AddVoucherIntblTrnac();
            _ContraEntry.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            retValue = _ContraEntry.AddVoucherIntblTrnacReverse();
            //try
            //{
            //    foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
            //    {
            //        mdebit = 0;
            //        mcredit = 0;
            //        macno = "";
            //        if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
            //        {
            //            _ContraEntry.JVID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            //            _ContraEntry.JVNo = _ContraEntry.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
            //            macno = dr.Cells["Col_ID"].Value.ToString();
            //            if (dr.Cells["Col_Debit"].Value != null)
            //                double.TryParse(dr.Cells["Col_Debit"].Value.ToString(), out mdebit);
            //            if (dr.Cells["Col_Credit"].Value != null)
            //                double.TryParse(dr.Cells["Col_Credit"].Value.ToString(), out mcredit);

            //            retValue = _ContraEntry.AddDetailsInmaterJV(_ContraEntry.JVID, macno, FixAccounts.VoucherTypeForJournalEntry, _ContraEntry.JVNo, General.ShopDetail.ShopVoucherSeries, _ContraEntry.CBVouDate, mdebit, mcredit, _ContraEntry.Id, _ContraEntry.CBNarration, _ContraEntry.CreatedBy, _ContraEntry.CreatedDate, _ContraEntry.CreatedTime);
            //            _ContraEntry.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            //            retValue = _ContraEntry.AddJVIntblTrnac(_ContraEntry.JVID, macno, mdebit, mcredit, _ContraEntry.DetailId, _ContraEntry.CBAccountID, _ContraEntry.CBVouDate, FixAccounts.VoucherTypeForJournalEntry, _ContraEntry.Id, _ContraEntry.CBNarration, _ContraEntry.JVNo);
            //            _ContraEntry.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            //            retValue = _ContraEntry.AddJVIntblTrnacReverse(_ContraEntry.JVID, macno, mdebit, mcredit, _ContraEntry.DetailId, _ContraEntry.CBAccountID, _ContraEntry.CBVouDate, FixAccounts.VoucherTypeForJournalEntry, _ContraEntry.Id, _ContraEntry.CBNarration, _ContraEntry.JVNo);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.WriteError(ex.ToString());
            //    retValue = false;
            //}
            return retValue;
        }


        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = ((PSComboBoxNew)sender).SelectedID;
            try
            {
                FillPartyCombo();
                mcbCreditor.SelectedID = selectedId;               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    mcbAccountTobeCredited.Focus();
                else if (e.KeyCode == Keys.Up)
                    mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
              
                case Keys.Up:
                    mcbAccountTobeCredited.Focus();
                    break;
            }
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtVouchernumber.Text != "")
                    {
                        _ContraEntry.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _ContraEntry.ReadDetailsByVouNumber();
                        FillSearchData(_ContraEntry.Id, "");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbAccountTobeCredited_EnterKeyPressed(object sender, EventArgs e)
        {
            txtAmount.Focus();
        }

        private void mcbAccountTobeCredited_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                txtNarration.Focus();
            else if (e.KeyCode == Keys.Down)
                txtAmount.Focus();
        }
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            //ttToolTip.SetToolTip(txtAmount, "Write Full Amount and in the grid select VAT Account and VAT Amount");
        }
        #endregion      

     
           
    }
}
