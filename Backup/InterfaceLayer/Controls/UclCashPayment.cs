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
    public partial class UclCashPayment : BaseControl
    {
        #region Declaration
        private CashPayment _CashPayment;
        DataTable _saledtable = new DataTable();
        DataTable _statementdtable = new DataTable();
        bool IfOpeningAdded = false;
        #endregion

        # region Constructor

        public UclCashPayment()
        {
            try
            {
                InitializeComponent();
                _CashPayment = new CashPayment();
                SearchControl = new UclCashPaymentSearch();
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
                _CashPayment.Initialise();
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
                FillmpPVC1GridSale();
                headerLabel1.Text = "CASH PAYMENT -> NEW";
                FillPartyCombo();
           //     datePickerBillDate.Value = DateTime.Now;
                EnableDisableForModify();
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
                headerLabel1.Text = "CASH PAYMENT -> EDIT";
                FillPartyCombo();
                datePickerBillDate.Value = DateTime.Now;
                EnableDisable();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private void EnableDisableForModify()
        {

            if (_Mode == OperationMode.Add)
                btnModify.Visible = false;
            else
            {
                btnModify.Enabled = false;
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                txtVouchernumber.Focus();
            }
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = true;
            txtAmountReceived.Enabled = true;
            pnlVouTypeNo.Enabled = true;
            pnlVou.Enabled = true;           
        }
        private void EnableDisable()
        {
            btnModify.Visible = true;
            mpMSCSale.Visible = true;        
            mpMSVC.Visible = false;        
            mcbCreditor.Enabled = false;
            txtAmountReceived.Enabled = false;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            txtVouchernumber.Focus();          
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            ClearData();
            return retValue;
        }

        public override bool Delete()
        {
            try
            {
                bool retValue = base.Delete();
                headerLabel1.Text = "CASH PAYMENT -> DELETE";
                ClearData();
                FillPartyCombo();
                datePickerBillDate.Value = DateTime.Now;
                EnableDisable();
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
                if (_CashPayment.Id != null && _CashPayment.Id != "")
                {
                    LockTable.LockTablesForCashBankPayment();
                    if (_CashPayment.CanBeDeleted())
                    {
                        General.BeginTransaction();
                        retValue = _CashPayment.DeleteDetails();
                        if (retValue)
                        {
                            DeletePreviousEntry();
                            RevertPreviousPurchaseBalance();
                        }
                        retValue = _CashPayment.DeleteAccountDetails();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            MessageBox.Show("Deleted Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _CashPayment.ModifiedBy = General.CurrentUser.Id;
                            _CashPayment.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _CashPayment.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            _CashPayment.AddDeletedDetails();
                            AddPreviousRowsInDeletedDetail();
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
                LockTable.UnLockTables();
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
                ClearData();
                headerLabel1.Text = "CASH PAYMENT -> VIEW";
                datePickerBillDate.Value = DateTime.Now;
                FillmpPVC1GridSale();
                FillPartyCombo();               
                EnableDisable();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool Save()
        {
            return SaveData(false);
        }

        public override bool SaveAndPrint()
        {
            return SaveData(true);
        }

        private bool SaveData(bool printData)
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                if (txtAmountReceived.Text != null && txtAmountReceived.Text != "")
                {
                    _CashPayment.CBAccountID = mcbCreditor.SelectedID;
                    _CashPayment.CBAmount = Convert.ToDouble(txtAmountReceived.Text.ToString());
                    _CashPayment.CBNarration = txtNarration.Text.ToString().Trim();

                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _CashPayment.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString().Trim());
                    _CashPayment.CBVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    if (_Mode == OperationMode.Edit)
                        _CashPayment.IFEdit = "Y";
                    _CashPayment.Validate();

                    if (_CashPayment.IsValid)
                    {
                        LockTable.LockTablesForCashBankPayment();
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            General.BeginTransaction();
                            _CashPayment.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _CashPayment.CreatedBy = General.CurrentUser.Id;
                            _CashPayment.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _CashPayment.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            _CashPayment.CBVouNo = _CashPayment.GetAndUpdateCSPNumber(General.ShopDetail.ShopVoucherSeries);
                            txtVouchernumber.Text = _CashPayment.CBVouNo.ToString();
                            retValue = _CashPayment.AddDetails();
                            if (retValue)
                                retValue = saveDetails();
                            if (retValue)
                                retValue = _CashPayment.AddAccountDetails();
                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {
                                string msgLine2 = _CashPayment.CBVouType + "  " + _CashPayment.CBVouNo.ToString("#0");
                                PSDialogResult result;
                                if (printData)
                                {
                                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                    Print();
                                }
                                else
                                {
                                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                    if (result == PSDialogResult.Print)
                                        Print();
                                }
                                _SavedID = _CashPayment.Id;
                                retValue = true;
                            }

                            else
                            {
                                PSDialogResult result = PSMessageBox.Show("Could not Add...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                retValue = false;
                            }
                        }

                        else if (_Mode == OperationMode.Edit)
                        {
                            if (_CashPayment.ModifyEdit == "Y")
                            {
                                General.BeginTransaction();
                                _CashPayment.CBAccountID = mcbCreditor.SelectedID;
                                _CashPayment.ModifiedBy = General.CurrentUser.Id;
                                _CashPayment.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _CashPayment.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                                retValue = _CashPayment.UpdateDetails();
                                if (retValue)
                                {
                                    retValue = DeletePreviousEntry();
                                    retValue = RevertPreviousPurchaseBalance();
                                    retValue = saveDetails();
                                }
                                if (retValue)
                                    retValue = _CashPayment.DeleteAccountDetails();
                                if (retValue)
                                    retValue = _CashPayment.AddAccountDetails();
                                if (retValue)
                                    General.CommitTransaction();
                                else
                                    General.RollbackTransaction();
                                LockTable.UnLockTables();
                                if (retValue)
                                {
                                    _CashPayment.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _CashPayment.AddChangedDetails();
                                    AddPreviousRowsInChangedDetail();
                                    string msgLine2 = _CashPayment.CBVouType + "  " + _CashPayment.CBVouNo.ToString("#0");
                                    PSDialogResult result;
                                    if (printData)
                                    {
                                        result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                        Print();
                                    }
                                    else
                                    {
                                        result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                        if (result == PSDialogResult.Print)
                                            Print();
                                    }
                                    _SavedID = _CashPayment.Id;
                                    retValue = true;
                                }
                                else
                                {
                                    MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    retValue = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        LockTable.UnLockTables();
                        _errorMessage = new System.Text.StringBuilder();
                        _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                        foreach (string _message in _CashPayment.ValidationMessages)
                        {
                            _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                        }
                        MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
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
                    _CashPayment.Id = ID;
                    if (Vmode == "C")
                        _CashPayment.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _CashPayment.ReadDetailsByIDForDeleted();
                    else
                    _CashPayment.ReadDetailsByID();
                    mcbCreditor.SelectedID = _CashPayment.CBAccountID;
                    _CashPayment.ActualAccountID = _CashPayment.CBAccountID;
                  
                    mpMSCSale.Visible = false;                
                    mpMSVC.Visible = true; 
                    FillmpMSVCGrid(Vmode);
                    FillmpPVCTempGrid();
                    DateTime mydate = new DateTime(Convert.ToInt32(_CashPayment.CBVouDate.Substring(0, 4)), Convert.ToInt32(_CashPayment.CBVouDate.Substring(4, 2)), Convert.ToInt32(_CashPayment.CBVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    txtNarration.Text = _CashPayment.CBNarration;
                    txtVouchernumber.Text = _CashPayment.CBVouNo.ToString();
                    txtVouType.Text = FixAccounts.VoucherTypeForCashPayment;
                    txtAmountReceived.Text = _CashPayment.CBAmount.ToString("#0.00");
                    txtVouchernumber.Enabled = false;

                    if (_Mode == OperationMode.Add)
                        pnlVou.Enabled = false;
                    else
                    {
                        mcbCreditor.Enabled = false;
                        txtAmountReceived.Enabled = false;
                        pnlVouTypeNo.Enabled = true;
                        pnlVou.Enabled = true;
                        txtVouchernumber.ReadOnly = false;
                        txtVouchernumber.Enabled = true;
                    }
                    if (_CashPayment.IFEdit == "Y")
                    {
                        btnModify.Visible = true;
                        btnModify.Enabled = true;
                        btnModify.Focus();
                    }
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
        public override void ReFillData()
        {
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {
              
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    txtAmountReceived.Focus();
                    retValue = true;
                }
             
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    datePickerBillDate.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    mcbCreditor.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                    txtNarration.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.V && modifier == Keys.Alt)
                {
                    txtVouchernumber.Focus();
                    retValue = true;
                }
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

        public void ModifyEdit()
        {
            bool retValue = false;
            try
            {
                _Mode = OperationMode.Edit;
                _CashPayment.ModifyEdit = "Y";              
                retValue = FillmpPVC1GridPurchaseforModify();     
              //  retValue = FillmpMSVCGrid("");     
                retValue = RevertPreviousEntry();
             //   retValue = RemoveZeroAmountAfterRevertPreviousEntry();
                mpMSCSale.Refresh();
                headerLabel1.Text = "CASH PAYMENT -> MODIFY";
             //   FillPartyCombo();
             //   mcbCreditor.SelectedID = _CashPayment.CBAccountID;
                EnableDisableForModify();            
                           
                txtAmountReceived.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public bool RevertPreviousEntry()
        {
            bool returnVal = true;
            try
            {
                foreach (DataGridViewRow drowMSVC in mpMSVC.Rows)
                {
                    string mSaleID = "";
                    string mcbid = "";
                    string mvoutype = "";
                    double mClearedAmount = 0;
                    if (drowMSVC.Cells["Col_ID"].Value != null)
                        mSaleID = drowMSVC.Cells["Col_ID"].Value.ToString();
                    if (drowMSVC.Cells["Col_MasterID"].Value != null)
                        mcbid = drowMSVC.Cells["Col_MasterID"].Value.ToString();
                    if (drowMSVC.Cells["Col_Type"].Value != null)
                        mvoutype = drowMSVC.Cells["Col_Type"].Value.ToString();
                    if (drowMSVC.Cells["Col_ClearedAmount"].Value != null)
                        double.TryParse(drowMSVC.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                    foreach (DataGridViewRow drowMSCSale in mpMSCSale.Rows)
                    {
                        if (drowMSCSale.Cells["Col_ID"].Value != null || mvoutype == "OPB")
                        {
                            if (drowMSCSale.Cells["Col_ID"].Value.ToString() == mSaleID || mvoutype == "OPB")
                            {
                                double mbalaceamount = 0;
                                if (mvoutype == "OPB")
                                {
                                    mbalaceamount = _CashPayment.OpeningBalance - _CashPayment.OpeningCleared;
                                }
                                else
                                {
                                    if (drowMSCSale.Cells["Col_BalanceAmount"].Value != null)
                                        double.TryParse(drowMSCSale.Cells["Col_BalanceAmount"].Value.ToString(), out mbalaceamount);
                                }
                                drowMSCSale.Cells["Col_BalanceAmount"].Value = mbalaceamount + mClearedAmount;
                                drowMSCSale.Cells["Col_GetClearedAmount"].Value = "";
                                break;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;

        }

        public bool RemoveZeroAmountAfterRevertPreviousEntry()
        {
            bool returnVal = true;
            try
            {
                foreach (DataGridViewRow drowMSCSale in mpMSCSale.Rows)
                {
                    if (drowMSCSale.Cells["Col_ID"].Value != null && drowMSCSale.Cells["Col_BalanceAmount"].Value != null)
                    {
                        if (Convert.ToDouble(drowMSCSale.Cells["Col_BalanceAmount"].Value) == 0)
                        {
                            mpMSCSale.Rows.Remove(drowMSCSale);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;

        }

        public bool RevertPreviousPurchaseBalance()
        {
            bool retValue = false;

            try
            {
                foreach (DataGridViewRow drowPVCTemp in mpPVCTemp.Rows)
                {
                    string mSaleID = "";
                    string mcbid = "";
                    double mClearedAmount = 0;
                    string mvoutype = "";
                    if (drowPVCTemp.Cells["Col_ID"].Value != null)
                        mSaleID = drowPVCTemp.Cells["Col_ID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_MasterID"].Value != null)
                        mcbid = drowPVCTemp.Cells["Col_MasterID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_ClearedAmount"].Value != null)
                        double.TryParse(drowPVCTemp.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                    if (drowPVCTemp.Cells["Col_Type"].Value != null)
                        mvoutype = drowPVCTemp.Cells["Col_Type"].Value.ToString();
                    if (mSaleID != null && mClearedAmount != 0 && mvoutype != "")
                    {
                        if (mvoutype == FixAccounts.VoucherTypeForOpeningBalance)
                            _CashPayment.UpdateOpeningBalanceReducePrevious(_CashPayment.CBAccountID, mClearedAmount);
                        else if (mvoutype == FixAccounts.VoucherTypeForCreditPurchase)
                            retValue = _CashPayment.RevertPreviousPurchaseBalanceBill(mSaleID, mClearedAmount);
                        else if (mvoutype == FixAccounts.VoucherTypeForStatementPurchase)
                            retValue = _CashPayment.RevertPreviousPurchaseBalanceStatement(mSaleID, mClearedAmount);
                    }
                    if (retValue == false)
                        break;
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;
            }
            return retValue;

        }

        private bool DeletePreviousEntry()
        {
            bool returnVal = false;
            try
            {
                returnVal = _CashPayment.DeletePreviousRecords();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return returnVal;
        }

        private bool saveDetails()
        {
            {
                bool returnVal = true;
                _CashPayment.SerialNumber = 0;               
                try
                {
                    foreach (DataGridViewRow drow in mpMSCSale.Rows)
                    {
                        if (drow.Cells["Col_GetClearedAmount"].Value != null &&
                           Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value) > 0)
                        {
                            _CashPayment.SerialNumber += 1;
                            _CashPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            if (drow.Cells["Col_MasterID"].Value != null)
                            _CashPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _CashPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _CashPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                            _CashPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                            if (drow.Cells["Col_Number"].Value != null && drow.Cells["Col_Number"].Value.ToString() != "")
                                _CashPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString());
                            _CashPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _CashPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _CashPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _CashPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value.ToString());
                            returnVal = _CashPayment.AddParticularsDetails();
                            if (returnVal)
                            {
                                if (_CashPayment.DVoucherType == FixAccounts.VoucherTypeForOpeningBalance)
                                {
                                    //if (_Mode == OperationMode.Add)
                                    //{
                                    _CashPayment.OpeningCleared = 0;
                                    if (_CashPayment.PreAccountID != null && _CashPayment.PreAccountID != "")
                                        returnVal = _CashPayment.UpdateOpeningBalanceReducePrevious(_CashPayment.PreAccountID, _CashPayment.OpeningCleared);
                                    returnVal = _CashPayment.UpdateOpeningBalanceAddNew();

                                    //}
                                }
                                if (_CashPayment.DVoucherType == FixAccounts.VoucherTypeForStatementPurchase)
                                    returnVal = _CashPayment.UpdatePurchaseStatement();
                                else
                                    returnVal = _CashPayment.UpdatePurchaseBill();
                                if (returnVal == false)
                                    break;
                            }
                            else
                                break;
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
        }

        private bool AddPreviousRowsInChangedDetail()
        {
            {
                bool returnVal = true;
                _CashPayment.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow drow in mpPVCTemp.Rows)
                    {
                        if (drow.Cells["Col_GetClearedAmount"].Value != null &&
                           Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value) > 0)
                        {
                            _CashPayment.SerialNumber += 1;
                            _CashPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _CashPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _CashPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _CashPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                            _CashPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                            _CashPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString());
                            _CashPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _CashPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _CashPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _CashPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value.ToString());
                            returnVal = _CashPayment.AddParticularsDetailsChanged();
                            //if (returnVal)
                            //{
                            //    if (_CashPayment.DVoucherType == FixAccounts.VoucherTypeForStatementPurchase)
                            //        returnVal = _CashPayment.UpdatePurchaseStatement();
                            //    else
                            //        returnVal = _CashPayment.UpdatePurchaseBill();
                            //    if (returnVal == false)
                            //        break;
                            //}
                            //else
                            //    break;
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
        }

        private bool AddPreviousRowsInDeletedDetail()
        {
            {
                bool returnVal = true;
                _CashPayment.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow drow in mpMSVC.Rows)
                    {
                        if (drow.Cells["Col_ID"].Value != null && drow.Cells["Col_ID"].Value.ToString() != "" )
                        {
                            _CashPayment.SerialNumber += 1;
                            _CashPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _CashPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _CashPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _CashPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                            _CashPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                            _CashPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString());
                            _CashPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _CashPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _CashPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _CashPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_ClearedAmount"].Value.ToString());
                            returnVal = _CashPayment.AddParticularsDetailsDeleted();
                            //if (returnVal)
                            //{
                            //    if (_CashPayment.DVoucherType == FixAccounts.VoucherTypeForStatementPurchase)
                            //        returnVal = _CashPayment.UpdatePurchaseStatement();
                            //    else
                            //        returnVal = _CashPayment.UpdatePurchaseBill();
                            //    if (returnVal == false)
                            //        break;
                            //}
                            //else
                            //    break;
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
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
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtNoOfRows.Text = "0";
                txtTotalBalance.Text = "0.00";
                txtVouType.Text = FixAccounts.VoucherTypeForCashPayment;
                datePickerBillDate.ResetText();
                txtAmountReceived.Text = "0.00";
                txtAmtNotAdjusted.Text = "0.00";
                mpMSVC.Rows.Clear();
                mpMSCSale.Rows.Clear();
                mcbCreditor.SelectedID = "";
                if (_Mode == OperationMode.Add)
                {
                    mcbCreditor.Enabled = true;
                    this.mcbCreditor.Focus();
                    txtVouchernumber.Enabled = false;
                }
                else
                {
                    txtVouchernumber.Enabled = true;
                    txtVouchernumber.Focus();
                    mcbCreditor.Enabled = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void NoofRows()
        {
            int itemCount = 0;
            double totamt = 0;

            try
            {
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {
                    if (dr.Cells["Col_BillAmount"].Value != null && dr.Cells["Col_BillAmount"].Value.ToString() != "")
                    {
                        itemCount += 1;
                        totamt = totamt + Convert.ToDouble(dr.Cells["Col_BillAmount"].Value.ToString());
                    }

                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtTotalBalance.Text = totamt.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void NoofRowsFormpMSVCGrid()
        {
            int itemCount = 0;
            double totamt = 0;

            try
            {
                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    if (dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != "")
                    {
                        itemCount += 1;
                        totamt = totamt + Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtTotalBalance.Text = totamt.ToString("#0.00");
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
                mcbCreditor.SourceDataString = new string[7] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccOpeningCredit", "AccClearedAmount", "AccAddress2" };
                mcbCreditor.ColumnWidth = new string[7] { "0", "20", "200", "200", "0", "0", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetCreditorListForPayment();
                mcbCreditor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                if (mcbCreditor.SeletedItem != null)
                {
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[6];
                    _CashPayment.CBAccountID = mcbCreditor.SelectedID;
                    if (_Mode != OperationMode.ReportView)
                    FillmpMSVCGrid("");
                    if (_CashPayment.ModifyEdit != "Y" || (_CashPayment.ModifyEdit == "Y" && _CashPayment.ActualAccountID != _CashPayment.CBAccountID))
                        FillmpPVC1GridSale();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbCreditor_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mcbCreditor.SeletedItem != null)
                {
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[6];
                    _CashPayment.OpeningBalance = 0;
                    _CashPayment.OpeningCleared = 0;
                    if (mcbCreditor.SeletedItem.ItemData[4] != null && mcbCreditor.SeletedItem.ItemData[4].ToString() != "")
                        _CashPayment.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[4].ToString());
                    if (mcbCreditor.SeletedItem.ItemData[5] != null && mcbCreditor.SeletedItem.ItemData[5].ToString() != "")
                        _CashPayment.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
                    _CashPayment.CBAccountID = mcbCreditor.SelectedID;
                    if (_Mode != OperationMode.ReportView)
                    FillmpMSVCGrid("");
                    //if ((_CashPayment.ModifyEdit != "Y" || (_CashPayment.ModifyEdit == "Y" && _CashPayment.ActualAccountID != _CashPayment.CBAccountID)) && _Mode != OperationMode.ReportView)
                    //    FillmpPVC1GridSale();
                    //mpMSCSale.ClearSelection();
                    txtAmountReceived.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool FillmpPVC1GridSale()
        {
            bool retValue = false;
            try
            {
               
                ConstructSaleColumns();
                FormatSaleGrid();
                DataTable dtable = new DataTable();
                if (_CashPayment.CBAccountID != null && _CashPayment.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_CashPayment.ActualAccountID != _CashPayment.CBAccountID && _CashPayment.ModifyEdit == "Y"))
                    {
                        dtable = _CashPayment.ReadBillDetailsByID();
                        if (_CashPayment.CBAccountID != null && _CashPayment.CBAccountID != "" && (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared) > 0)
                        {

                            int rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow dr = mpMSCSale.Rows[rowIndex];
                            dr.Cells["Col_ID"].Value = "";
                            dr.Cells["Col_Series"].Value = "";
                            dr.Cells["Col_Type"].Value = "OPB";
                            dr.Cells["Col_Number"].Value = "1";
                            dr.Cells["Col_BillFromDate"].Value = General.ShopDetail.Shopsy.ToString();
                            dr.Cells["Col_BillNumber"].Value = "1";
                            dr.Cells["Col_BillAmount"].Value = _CashPayment.OpeningBalance;
                            dr.Cells["Col_BalanceAmount"].Value = _CashPayment.OpeningBalance - _CashPayment.OpeningCleared;
                            dr.Cells["Col_GetClearedAmount"].Value = "";
                            dr.Cells["Col_ClearedAmount"].Value = "";
                            dr.Cells["Col_MasterID"].Value = "";


                            mpMSCSale.Sort(mpMSCSale.ColumnsMain["Col_Type"], ListSortDirection.Ascending);

                        }
                        if (dtable.Rows.Count > 0)
                        {
                            foreach (DataRow dt in dtable.Rows)
                            {
                                int rowIndex = mpMSCSale.Rows.Add();
                                DataGridViewRow dr = mpMSCSale.Rows[rowIndex];
                                if (dt["PurchaseID"] != DBNull.Value)
                                    dr.Cells["Col_ID"].Value = dt["PurchaseID"].ToString();
                                if (dt["VoucherSeries"] != DBNull.Value)
                                    dr.Cells["Col_Series"].Value = dt["VoucherSeries"].ToString();
                                if (dt["VoucherType"] != DBNull.Value)
                                    dr.Cells["Col_Type"].Value = dt["VoucherType"].ToString();
                                if (dt["VoucherNumber"] != DBNull.Value)
                                    dr.Cells["Col_Number"].Value = Convert.ToInt32(dt["VoucherNumber"].ToString());
                                if (dt["VoucherSubType"] != DBNull.Value)
                                    dr.Cells["Col_BillSubType"].Value = dt["VoucherSubType"].ToString();
                                if (dt["VoucherDate"] != DBNull.Value)
                                    dr.Cells["Col_BillFromDate"].Value = Convert.ToInt32(dt["VoucherDate"].ToString());
                                if (dt["PurchaseBillNumber"] != DBNull.Value)
                                    dr.Cells["Col_BillNumber"].Value = dt["PurchaseBillNumber"].ToString();
                                if (dt["AmountNet"] != DBNull.Value)
                                    dr.Cells["Col_BillAmount"].Value = Convert.ToInt32(dt["AmountNet"].ToString());
                                if (dt["AmountBalance"] != DBNull.Value)
                                    dr.Cells["Col_BalanceAmount"].Value = Convert.ToInt32(dt["AmountBalance"].ToString());
                                dr.Cells["Col_GetClearedAmount"].Value = 0;
                                dr.Cells["Col_ClearedAmount"].Value = 0;
                                dr.Cells["Col_MasterID"].Value = "";
                            }
                        }

                    }
                    else
                    {
                        dtable = _CashPayment.ReadBillDetailsByCSPID();

                        mpMSCSale.DataSourceMain = dtable;
                        if (dtable.Rows.Count > 0)
                        {
                            foreach (DataRow dt in dtable.Rows)
                            {
                                int rowIndex = mpMSCSale.Rows.Add();
                                DataGridViewRow dr = mpMSCSale.Rows[rowIndex];
                                if (dt["PurchaseID"] != DBNull.Value)
                                    dr.Cells["Col_ID"].Value = dt["PurchaseID"].ToString();
                                if (dt["VoucherSeries"] != DBNull.Value)
                                    dr.Cells["Col_Series"].Value = dt["VoucherSeries"].ToString();
                                if (dt["VoucherType"] != DBNull.Value)
                                    dr.Cells["Col_Type"].Value = dt["VoucherType"].ToString();
                                if (dt["VoucherNumber"] != DBNull.Value)
                                    dr.Cells["Col_Number"].Value = Convert.ToInt32(dt["VoucherNumber"].ToString());
                                dr.Cells["Col_BillSubType"].Value = string.Empty;
                                if (dt["VoucherType"] != DBNull.Value && dt["VoucherType"].ToString() == "OPB")
                                {
                                    dr.Cells["VoucherDate"].Value = General.ShopDetail.Shopsy;
                                    dr.Cells["Col_BillNumber"].Value = "1";
                                }
                                else
                                {
                                    if (dt["VoucherDate"] != DBNull.Value && dt["VoucherDate"].ToString() != "")
                                        dr.Cells["Col_BillFromDate"].Value = Convert.ToInt32(dt["VoucherDate"].ToString());

                                    if (dt["PurchaseBillNumber"] != DBNull.Value)
                                        dr.Cells["Col_BillNumber"].Value = dt["PurchaseBillNumber"].ToString();
                                }
                                if (dt["AmountNet"] != DBNull.Value)
                                    dr.Cells["Col_BillAmount"].Value = Convert.ToInt32(dt["AmountNet"].ToString());
                                if (dt["AmountBalance"] != DBNull.Value)
                                    dr.Cells["Col_BalanceAmount"].Value = Convert.ToInt32(dt["AmountBalance"].ToString());
                                dr.Cells["Col_GetClearedAmount"].Value = 0;
                                dr.Cells["Col_ClearedAmount"].Value = 0;
                                dr.Cells["Col_MasterID"].Value = "";
                            }
                        }
                    }
                }

                NoofRows();
                retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }

        private void FormatSaleGrid()
        {
            mpMSCSale.DoubleColumnNames.Add("Col_GetClearedAmount");
            mpMSCSale.DoubleColumnNames.Add("Col_BillAmount");
            mpMSCSale.DoubleColumnNames.Add("Col_BalanceAmount");
            mpMSCSale.DateColumnNames.Add("Col_BillFromDate");
            mpMSCSale.OnShowViewForm -= new PSMainSubViewControl.ShowViewForm(mpMSCSale_OnShowViewForm);
            mpMSCSale.OnShowViewForm += new PSMainSubViewControl.ShowViewForm(mpMSCSale_OnShowViewForm);
        }

        private void mpMSCSale_OnShowViewForm(DataGridViewRow selectedRow)
        {
            try
            {
                //TODO: Create form as per voucher type
                mpMSCSale.ViewControl = new UclPurchase();
                mpMSCSale.ProcessViewForm(selectedRow.Cells[0].Value.ToString(), this.Size, this.Location);
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }


        private bool FillmpPVC1GridPurchaseforModify()
        {
            bool retValue = false;
            try
            {
                ConstructSaleColumns();
                FormatSaleGrid();
                DataTable dtable = new DataTable();
              

                IfOpeningAdded = false;
                dtable = _CashPayment.ReadBillDetailsByIDforModify();
                _statementdtable = _CashPayment.ReadStatementDetailsByIDforModify();
                mpMSCSale.Rows.Clear();
                BindmpMSCSaleGrid(dtable, _statementdtable);
                IfOpeningAdded = true;
                _saledtable = _CashPayment.ReadBillDetailsByID();
                _statementdtable = _CashPayment.ReadStatementDetailsByID();
                BindmpMSCSaleGrid(_saledtable, _statementdtable);
                NoofRows();
                retValue = true;

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }
        private void BindmpMSCSaleGrid(DataTable saletable, DataTable statementtable)
        {

            bool ifIDFound = false;
            int _rowIndex = 0;
            string iD = "";
            try
            {
                if (_Mode == OperationMode.Add)
                {
                    if ((_CashPayment.OpeningBalance - _CashPayment.OpeningCleared) > 0)
                    {
                        _rowIndex = mpMSCSale.Rows.Add();
                        DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "";
                        currentdr.Cells["Col_Series"].Value = "";
                        currentdr.Cells["Col_Type"].Value = "OPB";
                        currentdr.Cells["Col_Number"].Value = "1";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = General.ShopDetail.Shopsy.ToString();
                        currentdr.Cells["Col_BillNumber"].Value = "1";
                        currentdr.Cells["Col_BillAmount"].Value = _CashPayment.OpeningBalance;
                        currentdr.Cells["Col_BalanceAmount"].Value = _CashPayment.OpeningBalance - _CashPayment.OpeningCleared;
                        currentdr.Cells["Col_GetClearedAmount"].Value = "";
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                        currentdr.Cells["Col_MasterID"].Value = "";


                        mpMSCSale.Sort(mpMSCSale.ColumnsMain["Col_Type"], ListSortDirection.Ascending);


                    }
                }
                else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
                {
                    if (_CashPayment.OpeningClearedInVoucher >= 0)
                    {
                        if ((_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher) > 0)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = "OPB";
                            currentdr.Cells["Col_Series"].Value = General.ShopDetail.ShopVoucherSeries;
                            currentdr.Cells["Col_Type"].Value = "OPB";
                            currentdr.Cells["Col_Number"].Value = "";
                            currentdr.Cells["Col_BillSubType"].Value = "";
                            currentdr.Cells["Col_BillFromDate"].Value = "";
                            currentdr.Cells["Col_BillAmount"].Value = _CashPayment.OpeningBalance.ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = "";
                            currentdr.Cells["Col_BalanceAmount"].Value = (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher).ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = "";
                        }
                    }
                }
                if (saletable != null && saletable.Rows.Count > 0)
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["PurchaseID"] != DBNull.Value)
                            iD = dr["PurchaseID"].ToString();
                        ifIDFound = SearchforIDInSaleGrid(iD);
                        if (ifIDFound == false)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                            currentdr.Cells["Col_Series"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_Type"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_Number"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            if (dr["PurchaseBillNumber"] != DBNull.Value)
                                currentdr.Cells["Col_BillNumber"].Value = dr["PurchaseBillNumber"].ToString();
                            //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                          //  currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                           // currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }

                }
                if (statementtable != null && statementtable.Rows.Count > 0)
                {

                    foreach (DataRow dr in statementtable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        ifIDFound = SearchforIDInSaleGrid(iD);
                        if (ifIDFound == false)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                            currentdr.Cells["Col_Series"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_Type"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_Number"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                           // currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                           // currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        private void BindmpMSVCGrid(DataTable saletable, DataTable statementtable)
        {

            bool ifIDFound = false;
            int _rowIndex = 0;
            string iD = "";
            try
            {
                if (_Mode == OperationMode.Add)
                {
                    if ((_CashPayment.OpeningBalance - _CashPayment.OpeningCleared) > 0)
                    {
                        _rowIndex = mpMSVC.Rows.Add();
                        DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "";
                        currentdr.Cells["Col_Series"].Value = "";
                        currentdr.Cells["Col_Type"].Value = "OPB";
                        currentdr.Cells["Col_Number"].Value = "1";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = General.ShopDetail.Shopsy.ToString();
                        currentdr.Cells["Col_BillNumber"].Value = "1";
                        currentdr.Cells["Col_BillAmount"].Value = _CashPayment.OpeningBalance;
                        currentdr.Cells["Col_BalanceAmount"].Value = _CashPayment.OpeningBalance - _CashPayment.OpeningCleared;
                        currentdr.Cells["Col_GetClearedAmount"].Value = "";
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                        currentdr.Cells["Col_MasterID"].Value = "";


                    }
                }
                else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
                {
                    if (_CashPayment.OpeningClearedInVoucher >= 0)
                    {
                        if ((_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher) > 0)
                        {
                            _rowIndex = mpMSVC.Rows.Add();
                            DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = "OPB";
                            currentdr.Cells["Col_Series"].Value = General.ShopDetail.ShopVoucherSeries;
                            currentdr.Cells["Col_Type"].Value = "OPB";
                            currentdr.Cells["Col_Number"].Value = "";
                            currentdr.Cells["Col_BillSubType"].Value = "";
                            currentdr.Cells["Col_BillFromDate"].Value = "";
                            currentdr.Cells["Col_BillAmount"].Value = _CashPayment.OpeningBalance.ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = "";
                            currentdr.Cells["Col_BalanceAmount"].Value = (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher).ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = "";
                        }
                    }
                }
                if (saletable != null && saletable.Rows.Count > 0)
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["PurchaseID"] != DBNull.Value)
                            iD = dr["PurchaseID"].ToString();
                        ifIDFound = SearchforIDInmpMSVCGrid(iD);
                        if (ifIDFound == false)
                        {
                            _rowIndex = mpMSVC.Rows.Add();
                            DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                            currentdr.Cells["Col_Series"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_Type"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_Number"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            if (dr["PurchaseBillNumber"] != DBNull.Value)
                                currentdr.Cells["Col_BillNumber"].Value = dr["PurchaseBillNumber"].ToString();
                            //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            //  currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            // currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }

                }
                if (statementtable != null && statementtable.Rows.Count > 0)
                {

                    foreach (DataRow dr in statementtable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        ifIDFound = SearchforIDInmpMSVCGrid(iD);
                        if (ifIDFound == false)
                        {
                            _rowIndex = mpMSVC.Rows.Add();
                            DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                            currentdr.Cells["Col_Series"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_Type"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_Number"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            // currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            // currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }


                }
                //  mpMSVC.Sort(mpMSVC.ColumnsMain["Col_Type"], ListSortDirection.Ascending);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private bool SearchforIDInSaleGrid(string ID)
        {
            bool retValue = false;
            string _GridID = "";
            foreach (DataGridViewRow dr in mpMSCSale.Rows)
            {
                if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    _GridID = dr.Cells["Col_ID"].Value.ToString();
                if (_GridID == ID)
                {
                    retValue = true;
                    break;
                }
            }

            return retValue;
        }
        private bool SearchforIDInmpMSVCGrid(string ID)
        {
            bool retValue = false;
            string _GridID = "";
            foreach (DataGridViewRow dr in mpMSVC.Rows)
            {
                if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    _GridID = dr.Cells["Col_ID"].Value.ToString();
                if (_GridID == ID)
                {
                    retValue = true;
                    break;
                }
            }

            return retValue;
        }
        private bool FillmpMSVCGrid(string vmode)
        {
            bool retValue = false;
            try
            {

                ConstructMainColumns();

                mpMSVC.DoubleColumnNames.Add("Col_BillAmount");
                mpMSVC.DoubleColumnNames.Add("Col_BalanceAmount");
                mpMSVC.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSVC.DoubleColumnNames.Add("Col_GetClearedAmount");
                mpMSVC.DateColumnNames.Add("Col_VoucherDate");

                ConstructSaleColumns();

                mpMSCSale.DoubleColumnNames.Add("Col_BillAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_BalanceAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_GetClearedAmount");
                mpMSCSale.DateColumnNames.Add("Col_VoucherDate");
             
                DataTable dtable = new DataTable();
                if (_CashPayment.CBAccountID != null && _CashPayment.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_CashPayment.ActualAccountID != _CashPayment.CBAccountID && _CashPayment.ModifyEdit == "Y"))
                    {
                        dtable = _CashPayment.ReadBillDetailsByID();
                        _statementdtable = _CashPayment.ReadStatementDetailsByID();

                        mpMSCSale.Rows.Clear();
                        mpMSVC.Rows.Clear();
                        BindmpMSVCGrid(dtable, _statementdtable);
                        BindmpMSCSaleGrid(dtable, _statementdtable);
                        mpMSCSale.Sort(mpMSCSale.ColumnsMain["Col_BillFromDate"], ListSortDirection.Ascending);
                        mpMSVC.Sort(mpMSVC.ColumnsMain["Col_BillFromDate"], ListSortDirection.Ascending);
                        NoofRows();
                    }

                    else
                    {
                        mpMSVC.Rows.Clear();
                      _statementdtable = null;
                        if (vmode == "C")
                            dtable = _CashPayment.ReadBillDetailsByCSPIDForChanged();
                        else if (vmode == "D")
                            dtable = _CashPayment.ReadBillDetailsByCSPIDForDeleted();
                        else
                            dtable = _CashPayment.ReadBillDetailsByCSPID();



                        BindmpMSVCGrid(dtable, _statementdtable);
                        NoofRowsFormpMSVCGrid();
                        foreach (DataRow dr in dtable.Rows)
                        {
                            if (dr["VoucherType"] != DBNull.Value && dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForOpeningBalance)
                            {
                                _CashPayment.OpeningClearedInVoucher = Convert.ToDouble(dr["AmountClear"].ToString());
                                break;
                            }
                        }
                        retValue = true;
                    }
                }
                    retValue = true;
                
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }

        private bool FillmpPVCTempGrid()
        {
            bool retValue = false;
            try
            {
                ConstructTempColumns();

                mpPVCTemp.DoubleColumnNames.Add("Col_BillAmount");
                mpPVCTemp.DoubleColumnNames.Add("Col_BalanceAmount");
                mpPVCTemp.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSVC.DoubleColumnNames.Add("Col_GetClearedAmount");


                DataTable dtable = new DataTable();
                dtable = _CashPayment.ReadBillDetailsByCSPID();
                mpPVCTemp.DataSourceMain = dtable;
                mpPVCTemp.Bind();
                retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }


        #endregion

        #region Construct columns

        private void ConstructMainColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMSVC.ColumnsMain.Clear();
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "purchaseID";
                column.HeaderText = "ID";
                column.Width = 0;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Series";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "AC-YEAR";
                column.Width = 100;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Type";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.ReadOnly = true;
                column.Width = 75;
                mpMSVC.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Number";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 95;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SUB";
                column.Width = 35;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "PurchaseBillNumber";
                column.HeaderText = "BillNumber";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 140;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                mpMSVC.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.Width = 140;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.ReadOnly = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void ConstructSaleColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMSCSale.ColumnsMain.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "purchaseID";
                column.HeaderText = "ID";
                column.Width = 0;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Series";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "YEAR";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Type";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.ReadOnly = true;
                column.Width = 65;
                mpMSCSale.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Number";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 95;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SUB";
                column.Width = 45;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "PurchaseBillNumber";
                column.HeaderText = "BillNumber";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 140;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                mpMSCSale.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.Width = 140;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.HeaderText = "ClearedAmount";
                column.Width = 140;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.ReadOnly = false;
                mpMSCSale.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructTempColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVCTemp.ColumnsMain.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "purchaseID";
                column.HeaderText = "ID";
                column.Width = 0;
                column.Visible = false;
                mpPVCTemp.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Series";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "VoucherSeriesT";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Type";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "VoucherType";
                column.ReadOnly = true;
                column.Width = 80;
                mpPVCTemp.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Number";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "VoucherNumber";
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "FromDate";
                column.Width = 80;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.Width = 110;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //10

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.ColumnsMain.Add(column);
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region EVENTS

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mpMSCSale.SetFocus(0, 9);
            }
        }

        private void mpMSCSale_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double mclearedamount = 0;
                if (e.ColumnIndex == 9)
                {
                    double mbalanceamount = 0;
                    double mamtnotadj = 0;
                    double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString().Trim() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out mclearedamount);
                    _CashPayment.CellOldValueAmount = mclearedamount;
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString().Trim() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceamount);
                    if (mclearedamount == 0 && mamtnotadj != 0)
                    {
                        double clearedamt = 0;
                        clearedamt = Math.Min(mamtnotadj, mbalanceamount);
                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void mpMSCSale_OnCellValueChangeCommited(int colIndex)
        {
          //  txtAmountReceived.Enabled = false;
            double totalreceived = 0;
            double mgetclearedamount = 0;
            double mbalamount = 0;
            double mamtnotadj = 0;
            double mbillamt = 0;
            double clearedamt = 0;
            double.TryParse(txtAmountReceived.Text, out mbillamt);
            double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);
            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null)
                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out mgetclearedamount);
            double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalamount);
        
            clearedamt = Math.Min(mamtnotadj, mbalamount);
            try
            {
                if (colIndex == 9)
                {
                    if (mgetclearedamount == 0)
                    {                       
                        _CashPayment.CellOldValueAmount = 0;
                    }

                    if (mbalamount < mgetclearedamount)
                    {  
                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
                        _CashPayment.CellOldValueAmount = clearedamt;
                    }
                    else
                    {
                        if (mamtnotadj == 0 )
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashPayment.CellOldValueAmount;
                       

                            if (clearedamt <= Math.Min(mamtnotadj, mbalamount) || (clearedamt <= _CashPayment.CellOldValueAmount))
                        {
                           
                        _CashPayment.CellOldValueAmount = mgetclearedamount;
                            foreach (DataGridViewRow dr in mpMSCSale.Rows)
                            {
                                double mcleared = 0;
                                if (dr.Cells["Col_GetClearedAmount"].Value != null)
                                    double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                                if (mcleared > 0)
                                    totalreceived += mcleared;
                            }
                            double d = (mbillamt - totalreceived);
                            txtAmtNotAdjusted.Text = d.ToString("#0.00");

                            int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
                            if (mpMSCSale.Rows.Count > rowindex + 1)
                                mpMSCSale.SetFocus(rowindex + 1, 9);
                           
                        }          
                    }
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpMSCSale_OnCellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == 9)
                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashPayment.CellOldValueAmount.ToString("#0.00");
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void txtAmountReceived_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    ClearGrid();
                    txtNarration.Focus();
                    break;
                case Keys.Down:
                    txtNarration.Focus();
                    break;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ModifyEdit();
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtAmountReceived.Focus();
        }

        private void txtAmountReceived_TextChanged(object sender, EventArgs e)
        {
            ClearGrid();
        }

        public void ClearGrid()
        {
            try
            {
                txtAmtNotAdjusted.Text = txtAmountReceived.Text;
                _CashPayment.CellOldValueAmount = 0;
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {
                    dr.Cells["Col_GetClearedAmount"].Value = 0;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtVouchernumber.Text != "")
                    {
                        _CashPayment.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _CashPayment.ReadDetailsByVoucherNumber();
                        FillSearchData(_CashPayment.Id,"");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

       


        #region tooltip

        #endregion



    }
}
