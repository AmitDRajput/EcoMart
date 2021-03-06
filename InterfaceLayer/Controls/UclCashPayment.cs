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
    public partial class UclCashPayment : BaseControl
    {
        #region Declaration
        private CashPayment _CashPayment;
        DataTable _saledtable = new DataTable();
        DataTable _statementdtable = new DataTable();
        bool IfOpeningAdded = false;
        private Form frmView;
        private DateTime _preDate = DateTime.Now;
        private BaseControl ViewControl;
        #endregion

        #region Constructor

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
                //    ClearData();
                FillmpPVC1GridSale();
                datePickerBillDate.Value = _preDate;
                headerLabel1.Text = "CASH PAYMENT -> NEW";
                FillPartyCombo();
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
                //FillOpeningBalanceGrid("");
                datePickerFromDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                datePickerToDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
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
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                FillPartyCombo();
                //   datePickerBillDate.Value = DateTime.Now;
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
                            _CashPayment.DeleteJV();
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
                // ClearData();
                headerLabel1.Text = "CASH PAYMENT -> VIEW";
                //  datePickerBillDate.Value = DateTime.Now;
                FillmpPVC1GridSale();
                FillPartyCombo();
                EnableDisable();
                if (General.IfYearEndOverGlobal == "Y")
                {
                    if (General.CurrentUser.Level <= 1)
                    {
                        tsBtnAdd.Visible = true;
                        tsBtnDelete.Visible = true;
                        tsBtnFifth.Visible = true;
                        tsBtnEdit.Visible = true;
                    }
                    else
                    {
                        tsBtnAdd.Visible = false;
                        tsBtnDelete.Visible = false;
                        tsBtnFifth.Visible = false;
                        tsBtnEdit.Visible = false;
                    }
                }
                // GetLastRecord();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private void ViewControl_ExitClicked(object sender, EventArgs e)
        {
            if (frmView != null)
            {
                frmView.Close();
            }
        }
        public void ShowViewForm(string ID)
        {
            if (ViewControl != null)
            {
                frmView = new Form();
                frmView.FormBorderStyle = FormBorderStyle.None;
                frmView.Height = this.Height;
                frmView.Width = this.Width;
                frmView.StartPosition = FormStartPosition.Manual;
                frmView.Location = new Point(this.Location.X + 45, this.Location.Y + 60);
                //  frmView.Icon = EcoMart.Properties.Resources.Icon;
                ViewControl.Mode = OperationMode.ReportView;
                ((IDetailControl)ViewControl).View();
                ViewControl.FillSearchData(ID, "C");
                ViewControl.ExitClicked -= new EventHandler(ViewControl_ExitClicked);
                ViewControl.ExitClicked += new EventHandler(ViewControl_ExitClicked);
                ViewControl.Visible = true;
                ViewControl.Height = this.Height - 6;
                ViewControl.Width = this.Width - 6;
                ViewControl.BringToFront();
                ViewControl.Location = new Point(3, 3);
                Panel pnl = new Panel();
                pnl.BackColor = Color.Orange;
                pnl.Dock = DockStyle.Fill;
                pnl.Controls.Add(ViewControl);
                frmView.Controls.Add(pnl);
                frmView.ShowDialog();
            }
        }

        public override bool Print()
        {
            bool retValue = true;
            if (txtAmountReceived.Text != null && Convert.ToDouble(txtAmountReceived.Text.ToString()) > 0)
            {
                if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
                {

                    ConstructPrintGridColumns();
                    PrintGrid.Rows.Clear();
                    FillPrintGrid();
                    if (General.CurrentSetting.MsetPrintCashBankVoucher == "Y")
                        PrintCashBankVoucherPrePrintedPaper();
                    else
                        PrintCashBankVoucherPlainPaper();
                }
                else
                {
                    PSDialogResult result;
                    result = PSMessageBox.Show("Trial License", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                }
            }
            ClearData();
            return retValue;
        }

        private void FillPrintGrid()
        {
            int colcount = mpMSCSale.ColumnsMain.Count;
            PSCashBankcontrol GridForPrint = new PSCashBankcontrol();
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                GridForPrint = mpMSCSale;
            else
                GridForPrint = mpMSVC;

            try
            {

                foreach (DataGridViewRow dr in GridForPrint.Rows)
                {
                    if (dr.Cells[0].Value != null && (dr.Cells["Col_GetClearedAmount"].Value != null || dr.Cells["Col_ClearedAmount"].Value != null) && Convert.ToDouble(dr.Cells["Col_GetClearedAmount"].Value.ToString()) > 0)
                    {
                        int printgridindex = PrintGrid.Rows.Add();
                        //if (dr.Cells["Col_BillSeries"].Value != null)
                        //    PrintGrid.Rows[printgridindex].Cells["Col_Quantity"].Value = dr.Cells["Col_BillSeries"].Value.ToString();
                        //if (dr.Cells["Col_BillType"].Value != null)
                        //    PrintGrid.Rows[printgridindex].Cells["Col_ProductName"].Value = dr.Cells["Col_BillType"].Value.ToString();
                        if (dr.Cells["Col_BillNumber"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_ProductName"].Value = dr.Cells["Col_BillNumber"].Value.ToString();
                        if (dr.Cells["Col_BillSubType"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_ProdCompShortName"].Value = dr.Cells["Col_BillSubType"].Value.ToString();
                        if (dr.Cells["Col_BillFromDate"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_BatchNumber"].Value = General.GetDateInShortDateFormat(dr.Cells["Col_BillFromDate"].Value.ToString());
                        if (dr.Cells["Col_BillAmount"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_RatePerUnit"].Value = dr.Cells["Col_BillAmount"].Value.ToString();
                        double amt = 0;
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                            amt = Convert.ToDouble(dr.Cells["Col_GetClearedAmount"].Value.ToString());
                        else
                            amt = Convert.ToDouble(dr.Cells["Col_ClearedAmount"].Value.ToString());
                        if (dr.Cells["Col_ClearedAmount"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_Amount"].Value = amt.ToString();


                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void PrintCashBankVoucherPrePrintedPaper()
        {
            EcoMart.Printing.PrePrintedPaperPrinter printer = new EcoMart.Printing.PrePrintedPaperPrinter();
            string atow = General.AmountToWord(_CashPayment.CBAmount);
            printer.Print(_CashPayment.CBVouType, _CashPayment.CBVouNo.ToString(), _CashPayment.CBVouDate, _CashPayment.CBName, _CashPayment.CBAddress1, _CashPayment.CBAddress2, "", "", PrintGrid.Rows, _CashPayment.CBNarration, _CashPayment.CBAmount, "", _CashPayment.CBTotalDiscount, 0, 0, 0, 0, atow);

        }

        private void PrintCashBankVoucherPlainPaper()
        {
            EcoMart.Printing.PlainPaperPrinter printer = new EcoMart.Printing.PlainPaperPrinter();
            string atow = General.AmountToWord(_CashPayment.CBAmount);
            printer.Print(_CashPayment.CBVouType, _CashPayment.CBVouNo.ToString(), _CashPayment.CBVouDate, _CashPayment.CBName, _CashPayment.CBAddress1, _CashPayment.CBAddress2, "", "", PrintGrid.Rows, _CashPayment.CBNarration, _CashPayment.CBAmount, "", _CashPayment.CBTotalDiscount, 0, 0, 0, 0, atow);

        }
        private void GetLastRecord()
        {
            try
            {
                if (txtVouType.Text == null || txtVouType.Text.ToString().Trim() == string.Empty)
                {
                    _CashPayment.CBVouType = FixAccounts.VoucherTypeForCashPayment;
                }
                _CashPayment.GetLastRecord();
                FillSearchData(_CashPayment.Id, "");
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
            _CashPayment.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _CashPayment.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _CashPayment.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _CashPayment.GetFirstRecord();
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _CashPayment.Id = dr["CBID"].ToString();
                FillSearchData(_CashPayment.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _CashPayment.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _CashPayment.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _CashPayment.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _CashPayment.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _CashPayment.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _CashPayment.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _CashPayment.CBVouNo = i;
                dr = _CashPayment.ReadDetailsByVoucherNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _CashPayment.Id = dr["CBID"].ToString();
                FillSearchData(_CashPayment.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _CashPayment.GetLastVoucherNumber(FixAccounts.VoucherTypeForCashPayment, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _CashPayment.CBVouType = txtVouType.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _CashPayment.CBVouNo = i;
                dr = _CashPayment.ReadDetailsByVoucherNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _CashPayment.Id = dr["CBID"].ToString();
                FillSearchData(_CashPayment.Id, "");
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
                if (mcbCreditor.SelectedID != null)
                {
                    _CashPayment.MobileNumberForSMS = mcbCreditor.SeletedItem.ItemData[7]; //Amar
                }
                if (txtAmountReceived.Text != null && txtAmountReceived.Text != "")
                {
                    _CashPayment.CBTotalDiscount = GetTotalDiscount();
                    _CashPayment.CBAccountID = mcbCreditor.SelectedID;
                    _CashPayment.CBAmount = Convert.ToDouble(txtAmountReceived.Text.ToString());
                    _CashPayment.CBNarration = txtNarration.Text.ToString().Trim();
                    if (txtAmtNotAdjusted.Text != null)
                    {
                        _CashPayment.CBOnAccountAmount = Convert.ToDouble(txtAmtNotAdjusted.Text.ToString());
                    }
                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _CashPayment.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString().Trim());
                    _CashPayment.CBVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    if (_Mode == OperationMode.Add)
                        _preDate = datePickerBillDate.Value;
                    if (_Mode == OperationMode.Edit)
                        _CashPayment.IFEdit = "Y";
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        LockTable.LocktblVoucherNo();
                        _CashPayment.CBVouNo = _CashPayment.GetAndUpdateCSPNumber(General.ShopDetail.ShopVoucherSeries);
                    }
                    _CashPayment.Validate();

                    if (_CashPayment.IsValid)
                    {
                        LockTable.LockTablesForCashBankPayment();
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            General.BeginTransaction();
                            //_CashPayment.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                           
                            _CashPayment.CreatedBy = General.CurrentUser.Id;
                            _CashPayment.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _CashPayment.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            //_CashPayment.CBVouNo = _CashPayment.GetAndUpdateCSPNumber(General.ShopDetail.ShopVoucherSeries);
                            txtVouchernumber.Text = _CashPayment.CBVouNo.ToString();
                            if (_CashPayment.CBTotalDiscount > 0)
                            {
                                _CashPayment.CBJVNo = _CashPayment.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                            }
                            else
                            {
                                _CashPayment.CBJVNo = 0;
                                _CashPayment.CBJVIDpay = 0;
                            }


                            _CashPayment.IntID = 0;
                            _CashPayment.IntID = _CashPayment.AddDetails();
                            if (_CashPayment.IntID > 0)
                                retValue = true;
                            else
                                retValue = false;
                            _SavedID = _CashPayment.Id;
                            //int retv  = _CashPayment.AddDetails();
                          
                            //if (retv > 0)
                            //{
                            //    _CashPayment.IntID = retv;
                            //    retValue = true;
                            //}
                            if (retValue)
                                retValue = saveDetails();
                            if (retValue)
                                retValue = _CashPayment.AddAccountDetails();
                            if (retValue && _CashPayment.CBTotalDiscount > 0)
                            {
                                _CashPayment.CBJVIDpay = _CashPayment.AddToMasterJV();
                                retValue = _CashPayment.UpdateJVIDInVoucherCashBankPayment();// update jvid with update jv number
                                _CashPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _CashPayment.AddJVTotblTrnacDebit();
                                _CashPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _CashPayment.AddJVTotblTrnacCredit();

                            }
                            if (retValue)
                            {
                                //SaveOldDetails();
                                General.CommitTransaction();
                            }
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
                                    //Amar For Sms
                                    if (General .CurrentSetting.SmsSetCashPaymentSale=="Y")
                                    {
                                        SendSMS mSMS = new SendSMS();
                                        string Msg = "Dear Customer Your Bill No.:" + _CashPayment.CBVouNo + " Of Amount :" + _CashPayment.CBAmount + " Thank You For Dealing With Us.";
                                        Msg += mSMS.GetShopDetailsFromData();
                                        if (string.IsNullOrEmpty(_CashPayment.MobileNumberForSMS) == false)
                                        {
                                            mSMS.SendSMSData(_CashPayment.MobileNumberForSMS, Msg);
                                        }
                                        else
                                            MessageBox.Show("Please Update Mobile Number For Sending SMS", "EcoMart", MessageBoxButtons.OK);
                                    }
                                }
                                else
                                {
                                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                    if (result == PSDialogResult.Print)
                                        Print();
                                    //Amar For Sms
                                    if (General.CurrentSetting.SmsSetCashPaymentSale == "Y")
                                    {
                                        SendSMS mSMS = new SendSMS();
                                        string Msg = "Dear Customer Your Bill No.:" + _CashPayment.CBVouNo + " Of Amount :" + _CashPayment.CBAmount + " Thank You For Dealing With Us.";
                                        Msg += mSMS.GetShopDetailsFromData();
                                        if (string.IsNullOrEmpty(_CashPayment.MobileNumberForSMS) == false)
                                        {
                                            mSMS.SendSMSData(_CashPayment.MobileNumberForSMS, Msg);
                                        }
                                        else
                                            MessageBox.Show("Please Update Mobile Number For Sending SMS", "EcoMart", MessageBoxButtons.OK);
                                    }
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
                                if (_CashPayment.CBTotalDiscount > 0)
                                {
                                    _CashPayment.CBJVNo = _CashPayment.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                                    //_CashPayment.CBJVID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                }
                                else
                                {
                                    _CashPayment.CBJVIDpay = 0;

                                }
                                retValue = _CashPayment.UpdateDetails();
                                if (retValue)
                                {
                                    retValue = DeletePreviousEntry();
                                    retValue = RevertPreviousPurchaseBalance();
                                    retValue = saveDetails();
                                }
                                if (retValue)
                                {
                                    retValue = _CashPayment.DeleteJV();
                                    retValue = _CashPayment.DeleteAccountDetails();
                                }
                                if (retValue)
                                    retValue = _CashPayment.AddAccountDetails();
                                if (retValue && _CashPayment.CBTotalDiscount > 0)
                                {
                                    //_CashReceipt.CBJVNo = _CashReceipt.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                                    //_CashReceipt.CBJVID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    //retValue = _CashPayment.AddToMasterJV();
                                    _CashPayment.CBJVIDpay = _CashPayment.AddToMasterJV();
                                    retValue = _CashPayment.UpdateJVIDInVoucherCashBankPayment();// update jvid with update jv number
                                    _CashPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _CashPayment.AddJVTotblTrnacDebit();
                                    _CashPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _CashPayment.AddJVTotblTrnacCredit();
                                }
                                if (retValue)
                                {
                                    SaveOldDetails();
                                    General.CommitTransaction();
                                }
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
        private double GetTotalDiscount()
        {
            double totdisc = 0;
            foreach (DataGridViewRow dr in mpMSCSale.Rows)
            {
                if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != string.Empty)
                    totdisc += Convert.ToDouble(dr.Cells["Col_DiscountAmount"].Value.ToString());

            }
            return totdisc;
        }
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _CashPayment.Id = ID;
                    if (_Mode == OperationMode.ReportView)
                        _CashPayment.ReadDetailsByID();
                    if (Vmode == "C" && _Mode != OperationMode.ReportView)
                        _CashPayment.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _CashPayment.ReadDetailsByIDForDeleted();
                    else
                        _CashPayment.ReadDetailsByID();
                    // mcbCreditor.SelectedIDtest = _CashPayment.CBAccountIDINT;
                    mcbCreditor.Enabled = true;
                    FillPartyCombo();                   
                    mcbCreditor.SelectedID = _CashPayment.CBAccountID;
                    _CashPayment.ActualAccountID = _CashPayment.CBAccountIDINT.ToString();

                    mpMSCSale.Visible = false;
                    mpMSVC.Visible = true;
                    FillmpMSVCGrid(Vmode);
                    FillmpPVCTempGrid();
                    //DateTime mydate = new DateTime(Convert.ToInt32(_CashPayment.CBVouDate.Substring(0, 4)), Convert.ToInt32(_CashPayment.CBVouDate.Substring(4, 2)), Convert.ToInt32(_CashPayment.CBVouDate.Substring(6, 2)));
                    //datePickerBillDate.Value = mydate;
                    if (DateTime.TryParse(_CashPayment.CBVouDate, out DateTime mydate))
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
                       // string dd = mcbCreditor.SelectedID;
                        mcbCreditor.Enabled = false;
                        txtAmountReceived.Enabled = false;
                        pnlVouTypeNo.Enabled = true;
                        pnlVou.Enabled = true;
                        txtVouchernumber.ReadOnly = false;
                        txtVouchernumber.Enabled = true;
                        btnModify.Visible = false;
                    }
                    if (_CashPayment.IFEdit == "Y" || _Mode == OperationMode.Edit)
                    {
                        mpMSVC.ClearSelection();
                        mcbCreditor.BackColor = Color.White;
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
        public override void ReFillData(Control closedControl)
        {
            if (closedControl is UclAccount)
            {
                string preselectedID = "";
                if (mcbCreditor.SelectedID != null)
                    preselectedID = mcbCreditor.SelectedID;
                FillPartyCombo();
                mcbCreditor.SelectedID = preselectedID;
            }
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
                tsBtnSearch.Visible = false;
                btnModify.BackColor = Color.White;
                _Mode = OperationMode.Edit;
                _CashPayment.ModifyEdit = "Y";

                retValue = FillmpPVC1GridPurchaseforModify();
                //FillOpeningBalanceGrid("");
                // FillDiscountFromTempGrid();
                mpMSCSale.Refresh();
                headerLabel1.Text = "CASH PAYMENT -> MODIFY";
                txtAmtNotAdjusted.Text = _CashPayment.CBAmount.ToString("#0.00");
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
            string mpmsvcID = "";
            double pvcClearedAmount = 0;
            double pvcdiscountamt = 0;
            double pvcbalaceamount = 0;
            double mbalaceamount = 0;
            try
            {
                foreach (DataGridViewRow pvcdr in mpMSVC.Rows)
                {
                    mpmsvcID = "";
                    pvcClearedAmount = 0;
                    pvcdiscountamt = 0;
                    pvcbalaceamount = 0;
                    if (pvcdr.Cells["Col_ID"].Value != null && pvcdr.Cells["Col_ID"].Value.ToString() != string.Empty)
                    {
                        mpmsvcID = pvcdr.Cells["Col_ID"].Value.ToString();
                        if (pvcdr.Cells["Col_BalanceAmount"].Value != null)
                            double.TryParse(pvcdr.Cells["Col_BalanceAmount"].Value.ToString(), out pvcbalaceamount);
                        if (pvcdr.Cells["Col_ClearedAmount"].Value != null)
                            double.TryParse(pvcdr.Cells["Col_ClearedAmount"].Value.ToString(), out pvcClearedAmount);
                        if (pvcdr.Cells["Col_DiscountAmount"].Value != null)
                            double.TryParse(pvcdr.Cells["Col_DiscountAmount"].Value.ToString(), out pvcdiscountamt);
                    }
                    foreach (DataGridViewRow drowMSCSale in mpMSCSale.Rows)
                    {



                        if (drowMSCSale.Cells["Col_ID"].Value != null && drowMSCSale.Cells["Col_Type"].Value.ToString() != "OPB")
                        {
                            string drowMSCSaleID = drowMSCSale.Cells["Col_ID"].Value.ToString();
                            if (mpmsvcID == drowMSCSaleID)
                            {
                                mbalaceamount = 0;

                                if (drowMSCSale.Cells["Col_BalanceAmount"].Value != null)
                                    double.TryParse(drowMSCSale.Cells["Col_BalanceAmount"].Value.ToString(), out mbalaceamount);
                                //  if (drowMSCSale.Cells["Col_ClearedAmount"].Value != null)
                                //        double.TryParse(drowMSCSale.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                                //   if (drowMSCSale.Cells["Col_DiscountAmount"].Value != null)
                                //       double.TryParse(drowMSCSale.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);

                                drowMSCSale.Cells["Col_BalanceAmount"].Value = mbalaceamount + pvcClearedAmount + pvcdiscountamt;
                                drowMSCSale.Cells["Col_GetClearedAmount"].Value = "";
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
                    double mdiscamount = 0;
                    if (drowPVCTemp.Cells["Col_ID"].Value != null)
                        mcbid = drowPVCTemp.Cells["Col_ID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_MasterPurchaseID"].Value != null)
                        mSaleID = drowPVCTemp.Cells["Col_MasterPurchaseID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_ClearedAmount"].Value != null)
                        double.TryParse(drowPVCTemp.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                    if (drowPVCTemp.Cells["Col_DiscountAmount"].Value != null && drowPVCTemp.Cells["Col_DiscountAmount"].Value.ToString() != "")
                        double.TryParse(drowPVCTemp.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscamount);
                    if (drowPVCTemp.Cells["Col_Type"].Value != null)
                        mvoutype = drowPVCTemp.Cells["Col_Type"].Value.ToString();
                    if (mSaleID != null && mClearedAmount != 0 && mvoutype != "")
                    {
                        if (mvoutype == FixAccounts.VoucherTypeForOpeningBalance)
                            _CashPayment.UpdateOpeningBalanceReducePrevious(_CashPayment.preAccountId, _CashPayment.OpeningClearedInVoucher + _CashPayment.DiscountInOpeningBalance);
                        else if (mvoutype == FixAccounts.VoucherTypeForCreditPurchase)
                            retValue = _CashPayment.RevertPreviousPurchaseBalanceBill(mSaleID, mClearedAmount + mdiscamount);
                        else if (mvoutype == FixAccounts.VoucherTypeForStatementPurchase)
                            retValue = _CashPayment.RevertPreviousPurchaseBalanceStatement(mSaleID, mClearedAmount + mdiscamount);
                    }

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
                        if (drow.Cells["Col_GetClearedAmount"].Value != null && drow.Cells["Col_GetClearedAmount"].Value.ToString() != string.Empty &&
                           Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value) > 0)
                        {
                            _CashPayment.SerialNumber += 1;
                            _CashPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            if (drow.Cells["Col_MasterID"].Value != null)
                                _CashPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _CashPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            if (_CashPayment.DSaleId == "OPB")
                            {
                                _CashPayment.DSaleId = "0";
                            }
                            else
                            {
                                _CashPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            }
                          
                            _CashPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                            _CashPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                            if (drow.Cells["Col_Number"].Value != null && drow.Cells["Col_Number"].Value.ToString() != "")
                                _CashPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString());
                            _CashPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _CashPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _CashPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _CashPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value.ToString());
                            _CashPayment.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                            returnVal = _CashPayment.AddParticularsDetails();
                            if (returnVal)
                            {
                                if (_CashPayment.DVoucherType == FixAccounts.VoucherTypeForOpeningBalance)
                                    returnVal = _CashPayment.UpdateOpeningBalanceAddNew();
                                else if (_CashPayment.DVoucherType == FixAccounts.VoucherTypeForStatementPurchase)
                                    returnVal = _CashPayment.UpdatePurchaseStatement();
                                else if (_CashPayment.DVoucherType == FixAccounts.VoucherTypeForCreditPurchase)
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
                        if (drow.Cells["Col_ID"].Value != null && drow.Cells["Col_ID"].Value.ToString() != "")
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
                tsBtnFifth.Visible = false;
                dgClearOpeningBalance.Visible = false;
                txtOpeningBalanceAmount.Text = "0.00";
                mpMSVC.Rows.Clear();
                mpMSCSale.Rows.Clear();
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtVoucherSeries.Text = _CashPayment.CBVouSeries;
                txtNoOfRows.Text = "0";
                txtTotalBalance.Text = "0.00";
                txtVouType.Text = FixAccounts.VoucherTypeForCashPayment;

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
                    datePickerFromDate.Visible = true;
                    datePickerToDate.Visible = true;
                    lblFromDate.Visible = true;
                    lblToDate.Visible = true;
                }
                else
                {
                    txtVouchernumber.Enabled = true;
                    txtVouchernumber.Focus();
                    mcbCreditor.Enabled = false;
                    datePickerFromDate.Visible = false;
                    datePickerToDate.Visible = false;
                    lblFromDate.Visible = false;
                    lblToDate.Visible = false;
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
        private void FillPartyCombo() //amar
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[8] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccOpeningCredit", "AccClearedAmount", "AccAddress2", "MobileNumberForSMS" };
                mcbCreditor.ColumnWidth = new string[8] { "0", "20", "200", "200", "0", "0", "0", "0" };
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
                    //_CashPayment.MobileNumberForSMS = mcbCreditor.SeletedItem.ItemData[8]; //Amar
                    
                    if (mcbCreditor.SeletedItem.ItemData[5] != null)
                        _CashPayment.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
                    if (mcbCreditor.SeletedItem.ItemData[6] != null)
                        _CashPayment.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[6].ToString());
                    _CashPayment.CBAccountID = mcbCreditor.SelectedID;
                    //if (_Mode != OperationMode.ReportView)
                    //FillmpMSVCGrid("");
                    //if (_CashPayment.ModifyEdit != "Y" || (_CashPayment.ModifyEdit == "Y" && _CashPayment.ActualAccountID != _CashPayment.CBAccountID))
                    //    FillmpPVC1GridSale();
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
                GetData();
                //if (mcbCreditor.SeletedItem != null && mcbCreditor.SelectedID != string.Empty)
                //{
                //    if (mcbCreditor.SeletedItem.ItemData[3] != null)
                //        txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                //    if (mcbCreditor.SeletedItem.ItemData[3] != null && mcbCreditor.SeletedItem.ItemData[3] != string.Empty)
                //        txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[3];
                //    if (mcbCreditor.SeletedItem.ItemData[4] != null && mcbCreditor.SeletedItem.ItemData[4] != string.Empty)
                //        _CashPayment.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[4].ToString());
                //    if (mcbCreditor.SeletedItem.ItemData[5] != null && mcbCreditor.SeletedItem.ItemData[5] != string.Empty)
                //        _CashPayment.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
                //    _CashPayment.CBAccountID = mcbCreditor.SelectedID;
                //    if (_Mode != OperationMode.ReportView)
                //    {
                //        FillOpeningBalanceGrid("");
                //        FillmpMSVCGrid("");
                //    }
                //    mpMSCSale.ClearSelection();
                //    //if ((_CashPayment.ModifyEdit != "Y" || (_CashPayment.ModifyEdit == "Y" && _CashPayment.ActualAccountID != _CashPayment.CBAccountID)) && _Mode != OperationMode.ReportView)
                //    //    FillmpPVC1GridSale();
                //    //mpMSCSale.ClearSelection();
                //    txtAmountReceived.Focus();
                //}
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void GetData()
        {
            _CashPayment.CBFromDate = datePickerFromDate.Value.Date.ToString("yyyyMMdd");
            _CashPayment.CBToDate = datePickerToDate.Value.Date.ToString("yyyyMMdd");
            if (mcbCreditor.SeletedItem != null)
            {
                if (mcbCreditor.SeletedItem.ItemData[3] != null)
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                if (mcbCreditor.SeletedItem.ItemData[6] != null && mcbCreditor.SeletedItem.ItemData[6] != string.Empty)
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[6];
                if (mcbCreditor.SeletedItem.ItemData[4] != null && mcbCreditor.SeletedItem.ItemData[4] != string.Empty)
                    _CashPayment.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[4].ToString());
                if (mcbCreditor.SeletedItem.ItemData[5] != null && mcbCreditor.SeletedItem.ItemData[5] != string.Empty)
                    _CashPayment.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
             //   _CashPayment.CBAccountID = mcbCreditor.SelectedID;
                //_CashPayment.MobileNumberForSMS = mcbCreditor.SeletedItem.ItemData[7];  //Amar

                if (_Mode != OperationMode.ReportView)
                    FillmpMSVCGrid("");
                //FillOpeningBalanceGrid("");
                mpMSCSale.ClearSelection();
                //if (_BankPayment.ModifyEdit != "Y" || (_BankPayment.ModifyEdit == "Y" && _BankPayment.ActualAccountID != _BankPayment.CBAccountID))
                //    FillmpPVC1GridSale();
                // txtAmountReceived.Focus();
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
                                //if (dt["VoucherSubType"] != DBNull.Value)
                                //    dr.Cells["Col_BillSubType"].Value = dt["VoucherSubType"].ToString();
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
            //mpMSCSale.OnShowViewForm -= new PSMainSubViewControl.ShowViewForm(mpMSCSale_OnShowViewForm);
            //mpMSCSale.OnShowViewForm += new PSMainSubViewControl.ShowViewForm(mpMSCSale_OnShowViewForm);
        }

        private void mpMSCSale_OnShowViewForm(DataGridViewRow selectedRow)
        {

            string voutype = "";
            string vousubtype = "";
            try
            {
                mpMSCSale.ViewControl = new UclDistributorSale("R");
                //  mpMSCSale.ProcessViewForm(selectedRow.Cells[0].Value.ToString(), this.Size, this.Location);
                string selectedID = selectedRow.Cells[0].Value.ToString();
                voutype = selectedRow.Cells["Col_BillType"].Value.ToString();
                vousubtype = selectedRow.Cells["Col_BillSubType"].Value.ToString();
                if (voutype == FixAccounts.VoucherTypeForCashPurchase || voutype == FixAccounts.VoucherTypeForCreditPurchase || voutype == FixAccounts.VoucherTypeForCreditStatementPurchase)
                    ViewControl = new UclPurchase();
                ShowViewForm(selectedID);
                this.Cursor = Cursors.Default;
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
                retValue = RevertPreviousEntry();
                IfOpeningAdded = true;
                _saledtable = _CashPayment.ReadBillDetailsByID();
                _statementdtable = _CashPayment.ReadStatementDetailsByID();
                BindmpMSCSaleGrid(_saledtable, _statementdtable);
                NoofRows();
               // FillOpeningBalanceGrid("");
                retValue = true;

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }
        //private void FillDiscountFromTempGrid()
        //{
        //    string mtempvouid = "";
        //    string msalevouid = "";
        //    double mtempdiscamt = 0;
        //    double balamount = 0;
        //    try
        //    {
        //        foreach (DataGridViewRow tempdr in mpPVCTemp.Rows)
        //        {
        //            if (tempdr.Cells["Col_MasterpurchaseID"].Value != null)
        //                mtempvouid = tempdr.Cells["Col_MasterpurchaseID"].Value.ToString();
        //            if (tempdr.Cells["Col_DiscountAmount"].Value != null)
        //                mtempdiscamt = Convert.ToDouble(tempdr.Cells["Col_DiscountAmount"].Value.ToString());
        //            if (mtempvouid != FixAccounts.VoucherTypeForOpeningBalance && mtempdiscamt > 0)
        //            {
        //                foreach (DataGridViewRow dr in mpMSCSale.Rows)
        //                {
        //                    if (dr.Cells["Col_ID"].Value != null)
        //                        msalevouid = dr.Cells["Col_ID"].Value.ToString();
        //                    if (mtempvouid == msalevouid)
        //                    {
        //                        if (dr.Cells["Col_BalanceAmount"].Value != null)
        //                            balamount = Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
        //                        balamount += mtempdiscamt;
        //                        dr.Cells["Col_BalanceAmount"].Value = balamount.ToString("#0.00");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        private void BindmpMSCSaleGrid(DataTable saletable, DataTable statementtable)
        {

            bool ifIDFound = false;
            int _rowIndex = 0;
            string iD = "";
            try
            {
                if (IfOpeningAdded != true && (_Mode != OperationMode.Edit) && (_Mode == OperationMode.Add || (((_CashPayment.preAccountId != null && _CashPayment.preAccountId != "") || _CashPayment.preAccountId != "") && _CashPayment.CBAccountID != _CashPayment.preAccountId) || _CashPayment.ModifyEdit == "Y"))
                {
                    if ((_CashPayment.OpeningClearedInVoucher > 0 || (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher + _CashPayment.DiscountInOpeningBalance) > 0) && ((_CashPayment.preAccountId == null || _CashPayment.preAccountId == "") || _CashPayment.CBAccountID == _CashPayment.preAccountId))
                    {
                        _rowIndex = mpMSCSale.Rows.Add();
                        DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "OPB";
                        currentdr.Cells["Col_Series"].Value = "";
                        currentdr.Cells["Col_Type"].Value = "OPB";
                        currentdr.Cells["Col_Number"].Value = "1";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = General.ShopDetail.Shopsy.ToString();
                        currentdr.Cells["Col_BillNumber"].Value = "1";
                        currentdr.Cells["Col_BillAmount"].Value = _CashPayment.OpeningBalance;
                        if ((_CashPayment.preAccountId == null || _CashPayment.preAccountId == "") || ((_CashPayment.preAccountId != null || _CashPayment.preAccountId != "") && _CashPayment.CBAccountID == _CashPayment.preAccountId))
                            currentdr.Cells["Col_BalanceAmount"].Value = (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher + _CashPayment.DiscountInOpeningBalance).ToString();
                        else
                            currentdr.Cells["Col_BalanceAmount"].Value = _CashPayment.OpeningBalance;
                        currentdr.Cells["Col_GetClearedAmount"].Value = "";
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                        currentdr.Cells["Col_MasterID"].Value = "";


                        mpMSCSale.Sort(mpMSCSale.ColumnsMain["Col_Type"], ListSortDirection.Ascending);
                        IfOpeningAdded = true;

                    }
                }
                else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
                {
                    if (_CashPayment.OpeningClearedInVoucher >= 0)
                    {
                        if ((_CashPayment.OpeningBalance > 0) && (_CashPayment.OpeningClearedInVoucher > 0 || (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher + _CashPayment.DiscountInOpeningBalance) > 0) && (_CashPayment.CBAccountID == _CashPayment.preAccountId))
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
                            //  currentdr.Cells["Col_PatientShortName"].Value = "";
                            //  currentdr.Cells["Col_BalanceAmount"].Value = (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher).ToString();
                            if (_CashPayment.CBAccountID == _CashPayment.preAccountId)
                                currentdr.Cells["Col_BalanceAmount"].Value = (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher + _CashPayment.DiscountInOpeningBalance).ToString();
                            else
                                currentdr.Cells["Col_BalanceAmount"].Value = _CashPayment.OpeningBalance;
                            currentdr.Cells["Col_ClearedAmount"].Value = "";
                            IfOpeningAdded = true;
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
                            //currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            if (dr["PurchaseBillNumber"] != DBNull.Value)
                                currentdr.Cells["Col_BillNumber"].Value = dr["PurchaseBillNumber"].ToString();
                            //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            //  currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                            currentdr.Cells["Col_Discountamount"].Value = "0.00";
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
                            //currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            // currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                            currentdr.Cells["Col_Discountamount"].Value = "0.00";
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
            mpMSVC.Rows.Clear();
            int _rowIndex = 0;
            IfOpeningAdded = false;
            string iD = "";
            try
            {
                if (_Mode == OperationMode.Add || (_CashPayment.CBAccountID != _CashPayment.preAccountId))
                {
                    if ((_CashPayment.OpeningBalance - _CashPayment.OpeningCleared) > 0)
                    {
                        _rowIndex = mpMSVC.Rows.Add();
                        DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "OPB";
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
                        IfOpeningAdded = true;

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
                            //currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            if (dr["PurchaseBillNumber"] != DBNull.Value)
                                currentdr.Cells["Col_BillNumber"].Value = dr["PurchaseBillNumber"].ToString();
                            //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            //  currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_GetClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
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
                            //currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            // currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
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
        private void BindTempGrid(DataTable saletable, DataTable statementtable)
        {

            int _rowIndex = 0;
            string iD = "";

            if (saletable != null && saletable.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        //ifIDFound = SearchforIDInSaleGrid(iD);
                        //if (ifIDFound == false)
                        //{
                        _rowIndex = mpPVCTemp.Rows.Add();
                        DataGridViewRow currentdr = mpPVCTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_Series"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_Type"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_Number"].Value = dr["VoucherNumber"].ToString();
                        // currentdr.Cells["Col_SubType"].Value = dr["VoucherSubType"].ToString();
                        //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        //  currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterPurchaseID"].Value = dr["PurchaseID"].ToString();
                        //}

                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());


                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
            {

                foreach (DataRow dr in statementtable.Rows)
                {
                    try
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        _rowIndex = mpPVCTemp.Rows.Add();
                        DataGridViewRow currentdr = mpPVCTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        //currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        //}
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex.ToString());

                    }
                }

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
                mpMSVC.DoubleColumnNames.Add("Col_DiscountAmount");
                mpMSVC.DateColumnNames.Add("Col_VoucherDate");

                ConstructSaleColumns();

                mpMSCSale.DoubleColumnNames.Add("Col_BillAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_BalanceAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_GetClearedAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_DiscountAmount");
                mpMSCSale.DateColumnNames.Add("Col_VoucherDate");
                _CashPayment.CBAccountID = mcbCreditor.SelectedID;
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
                        IfOpeningAdded = false;
                        BindmpMSCSaleGrid(dtable, _statementdtable);
                        mpMSCSale.Sort(mpMSCSale.ColumnsMain["Col_BillFromDate"], ListSortDirection.Ascending);
                        mpMSVC.Sort(mpMSVC.ColumnsMain["Col_BillFromDate"], ListSortDirection.Ascending);
                        NoofRows();
                    }

                    else
                    {
                        mpMSVC.Rows.Clear();
                        _statementdtable = null;
                        if (vmode == "C" && _Mode != OperationMode.ReportView)
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

                //mpPVCTemp.DoubleColumnNames.Add("Col_BillAmount");
                //mpPVCTemp.DoubleColumnNames.Add("Col_BalanceAmount");
                //mpPVCTemp.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSVC.DoubleColumnNames.Add("Col_GetClearedAmount");


                DataTable dtable = new DataTable();
                dtable = _CashPayment.ReadBillDetailsByCSPID();
                //  mpPVCTemp.DataSource = dtable;
                //  mpPVCTemp.Bind();
                BindTempGrid(dtable, _statementdtable);
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
        private void ConstructPrintGridColumns()
        {
            DataGridViewTextBoxColumn column;
            PrintGrid.Columns.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 220;
                column.ReadOnly = false;
                PrintGrid.Columns.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                PrintGrid.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 50;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                PrintGrid.Columns.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //8          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 70;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                PrintGrid.Columns.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //13            // temp storage columns 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //14         
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //20
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "IfSaleDisc";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                ////// added new columns 29/3/2015

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_RatePerUnit";
                // column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFMultipleMRP";
                //  column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);



            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }
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
                column.Width = 70;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Type";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.ReadOnly = true;
                column.Width = 55;
                mpMSVC.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Number";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 75;
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
                column.Width = 110;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "PurchaseBillNumber";
                column.HeaderText = "BillNumber";
                column.Width = 80;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 110;
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
                column.Width = 110;
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

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "DiscountAmount";
                column.HeaderText = "Discount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.Visible = true;
                column.ReadOnly = false;
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
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.HeaderText = "ID";
                column.Width = 0;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Series";
                column.HeaderText = "YEAR";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Type";
                column.HeaderText = "Type";
                column.ReadOnly = true;
                column.Width = 55;
                mpMSCSale.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Number";
                column.HeaderText = "Number";
                column.Width = 75;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.HeaderText = "SUB";
                column.Width = 35;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.HeaderText = "Date";
                column.Width = 110;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.HeaderText = "BillNumber";
                column.Width = 80;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = "Check";
                column.Width = 30;
                if (_Mode == OperationMode.Add)
                {
                    column.Visible = true;
                    //column.ReadOnly = false;
                }
                else
                {
                    column.Visible = false;
                    //column.ReadOnly = true;
                }
                mpMSCSale.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSCSale.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.HeaderText = "BalanceAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);

                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);

                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.HeaderText = "ClearedAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.HeaderText = "Discount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.Visible = true;
                column.ReadOnly = true;
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
            mpPVCTemp.Columns.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "purchaseID";
                column.HeaderText = "ID";
                column.Width = 0;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Series";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "VoucherSeriesT";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Type";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "VoucherType";
                column.ReadOnly = true;
                column.Width = 80;
                mpPVCTemp.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Number";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "VoucherNumber";
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "FromDate";
                column.Width = 80;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterPurchaseID";
                column.DataPropertyName = "PurchaseID";
                column.HeaderText = "MasterPurchaseID";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
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
                mpPVCTemp.Columns.Add(column);
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
                mpPVCTemp.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "DiscountAmount";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);
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
                if (_Mode == OperationMode.Add)
                {
                    if (_CashPayment.PaymentSubType == 2)
                        mpMSCSale.SetFocus(0, 7); // old 11
                    else if (_CashPayment.PaymentSubType == 3 || (_CashPayment.PaymentSubType == 0))
                        mpMSCSale.SetFocus(0, 12);
                    else
                        txtNarration.Focus();
                }
                else
                    mpMSCSale.SetFocus(0, 12);
            }
            else if (e.KeyCode == Keys.Up)
                txtAmountReceived.Focus();
        }

        private void mpMSCSale_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double getclearedamt = 0;
                if (e.ColumnIndex == 12 && (_CashPayment.PaymentSubType == 3 || _CashPayment.PaymentSubType == 0))// old 11
                {
                    double mbalanceamount = 0;
                    double mamtnotadj = 0;
                    double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString().Trim() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
                    _CashPayment.CellOldValueAmount = getclearedamt;
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString().Trim() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceamount);
                    if (getclearedamt == 0 && mamtnotadj != 0)
                    {
                        double clearedamt = 0;
                        clearedamt = Math.Min(mamtnotadj, mbalanceamount);
                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;

                    }
                    //mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].ReadOnly = false;
                    //mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
                }
                //else
                //{
                //    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].ReadOnly = true;
                //    mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = true;
                //}
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        //private void mpMSCSale_OnCellValueChangeCommited(int colIndex)
        //{
        //    //  txtAmountReceived.Enabled = false;
        //    double totalreceived = 0;
        //    double getclearedamt = 0;
        //    double mbalanceAmount = 0;
        //    double mamtnotadj = 0;
        //    double clearedamt = 0;
        //    double mbillamt = 0;
        //    double mdiscountamt = 0;
        //    double.TryParse(txtAmountReceived.Text, out mbillamt);
        //    double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);

        //    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null)
        //        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
        //    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
        //        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);
        //    double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceAmount);

        //    clearedamt = Math.Min(mamtnotadj, mbalanceAmount);
        //    try
        //    {
        //        if (colIndex == 7)
        //        {
        //            ColCheckChecked();
        //        }

        //        if (colIndex == 12) // old 11
        //        {
        //            if ((txtAmountReceived.Text != txtTotalBalance.Text && mpMSCSale.ColumnsMain["Col_Check"].ReadOnly != false))// old 11
        //            {
        //                if (getclearedamt == 0)
        //                {
        //                    _CashPayment.CellOldValueAmount = 0;
        //                }

        //                if (mbalanceAmount < getclearedamt)
        //                {
        //                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
        //                    _CashPayment.CellOldValueAmount = clearedamt;
        //                }
        //                else
        //                {
        //                    if (mamtnotadj == 0)
        //                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashPayment.CellOldValueAmount;


        //                    //if (clearedamt <= Math.Min(mamtnotadj, mbalanceAmount) || (clearedamt <= _CashReceipt.CellOldValueAmount))
        //                    //{

        //                    _CashPayment.CellOldValueAmount = getclearedamt;
        //                    foreach (DataGridViewRow dr in mpMSCSale.Rows)
        //                    {
        //                        double mcleared = 0;
        //                        double mdiscount = 0;
        //                        if (dr.Cells["Col_GetClearedAmount"].Value != null)
        //                            double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
        //                        if (dr.Index == mpMSCSale.MainDataGridCurrentRow.Index)
        //                            mcleared = getclearedamt;
        //                        if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
        //                            double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
        //                        if (mcleared > 0)
        //                            totalreceived += mcleared + mdiscount;
        //                    }
        //                    mamtnotadj = (mbillamt - totalreceived);

        //                    if (mamtnotadj < 0)
        //                    {
        //                        mamtnotadj = 0;
        //                        _CashPayment.CellOldValueAmount = 0;
        //                        _CashPayment.CellOldValueDiscount = 0;
        //                        getclearedamt = 0;
        //                    }
        //                    txtAmtNotAdjusted.Text = mamtnotadj.ToString("#0.00");
        //                    if (mamtnotadj >= 0 && getclearedamt > 0)
        //                        mdiscountamt = mbalanceAmount - getclearedamt;
        //                    else
        //                        mdiscountamt = 0;
        //                    if (mdiscountamt > 10)
        //                        mdiscountamt = 0;

        //                    if (getclearedamt == 0)
        //                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
        //                    else
        //                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt.ToString("#0.00");

        //                    if (mdiscountamt == 0)
        //                    {
        //                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = true;
        //                        mpMSCSale.NextRowColumn = 12;
        //                    }
        //                    else
        //                    {
        //                        mpMSCSale.NextRowColumn = 13;
        //                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
        //                    }
        //                    //int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
        //                    //if (mpMSCSale.Rows.Count > rowindex + 1)
        //                    //    mpMSCSale.SetFocus(rowindex + 1, 9);
        //                    //}
        //                }
        //            }
        //            else
        //            {
        //                mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = mbalanceAmount.ToString("#0.00");
        //            }
        //        }
        //        else if (colIndex == 13) // SaveOldDetails 12
        //        {
        //            double mdiscgiven = 0;
        //            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
        //                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
        //            mdiscountamt = Math.Round(mbalanceAmount - getclearedamt, 2);
        //            if (mdiscgiven != 0)
        //            {
        //                if (mdiscgiven != mdiscountamt || getclearedamt == 0)
        //                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
        //            }
        //            totalreceived = 0;
        //            foreach (DataGridViewRow dr in mpMSCSale.Rows)
        //            {
        //                double mcleared = 0;
        //                double mdiscount = 0;
        //                if (dr.Cells["Col_GetClearedAmount"].Value != null)
        //                    double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);

        //                if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
        //                    double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);

        //                totalreceived += (mcleared + mdiscount);
        //            }
        //            mamtnotadj = (mbillamt - totalreceived);
        //            txtAmtNotAdjusted.Text = mamtnotadj.ToString("#0.00");
        //            //if (mpMSCSale.MainDataGridCurrentRow.Index < mpMSCSale.Rows.Count-1)

        //            //    mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 12);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        private void mpMSCSale_OnCellValueChangeCommited(int colIndex)
        {
            //  txtAmountReceived.Enabled = false;


            double totalreceived = 0;
            double getclearedamt = 0;
            double mbalanceAmount = 0;
            double mamtnotadj = 0;
            double clearedamt = 0;
            double mbillamt = 0;
            double mdiscountamt = 0;
            double.TryParse(txtAmountReceived.Text, out mbillamt);
            double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);

            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null)
                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);
            double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceAmount);

            clearedamt = Math.Min(mamtnotadj, mbalanceAmount);
            try
            {

                if (colIndex == 7)
                {
                    ColCheckChecked();
                }
                if (colIndex == 12) // old 11
                {
                    if (_CashPayment.PaymentSubType == 3 || _CashPayment.PaymentSubType == 0)// old 11
                    {
                        if (getclearedamt == 0)
                        {
                            _CashPayment.CellOldValueAmount = 0;
                        }

                        if (mbalanceAmount < getclearedamt)
                        {
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
                            _CashPayment.CellOldValueAmount = clearedamt;
                        }
                        else
                        {
                            if (mamtnotadj == 0)
                                mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashPayment.CellOldValueAmount;


                            //if (clearedamt <= Math.Min(mamtnotadj, mbalanceAmount) || (clearedamt <= _CashReceipt.CellOldValueAmount))
                            //{

                            _CashPayment.CellOldValueAmount = getclearedamt;
                            foreach (DataGridViewRow dr in mpMSCSale.Rows)
                            {
                                double mcleared = 0;
                                double mdiscount = 0;
                                if (dr.Cells["Col_GetClearedAmount"].Value != null)
                                    double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                                if (dr.Index == mpMSCSale.MainDataGridCurrentRow.Index)
                                    mcleared = getclearedamt;
                                if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                    double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
                                if (mcleared > 0)
                                    totalreceived += mcleared; 
                                //totalreceived += mcleared + mdiscount;
                            }

                            mamtnotadj = (mbillamt - totalreceived);


                            if (mamtnotadj < 0)
                            {
                                mamtnotadj = 0;
                                _CashPayment.CellOldValueAmount = 0;
                                _CashPayment.CellOldValueDiscount = 0;
                                getclearedamt = 0;
                            }
                            totalreceived = 0;
                            foreach (DataGridViewRow dr in mpMSCSale.Rows)
                            {
                                double mcleared = 0;
                                double mdiscount = 0;
                                if (dr.Cells["Col_GetClearedAmount"].Value != null)
                                    double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                                if (dr.Index == mpMSCSale.MainDataGridCurrentRow.Index)
                                    mcleared = getclearedamt;
                                if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                    double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
                                if (mcleared > 0)
                                    totalreceived += mcleared;
                                //totalreceived += mcleared + mdiscount;
                            }

                            mamtnotadj = (mbillamt - totalreceived);
                            txtAmtNotAdjusted.Text = mamtnotadj.ToString("#0.00");
                            if (mamtnotadj >= 0 && getclearedamt > 0)

                                mdiscountamt = mbalanceAmount - getclearedamt;
                            if (General.CurrentSetting.MsetCashBankShowDiscount != "Y")   //Amar
                            {
                                if (mdiscountamt > 10)
                                    mdiscountamt = 0;

                                else
                                    mdiscountamt = 0;
                            }
                            if (General.CurrentSetting.MsetCashBankShowDiscount != "Y")   //Amar
                            {
                                if (mdiscountamt > 10)
                                    mdiscountamt = 0;
                            }

                            if (getclearedamt == 0)
                                mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                            else
                                mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt.ToString("#0.00");
                            if (mdiscountamt == 0)
                            {
                                mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = true;
                                mpMSCSale.NextRowColumn = 12;
                            }
                            else
                            {
                                mpMSCSale.NextRowColumn = 13;
                                mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = false;
                                mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
                            }
                            //if (Convert.ToDouble(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString()) != Convert.ToDouble(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString()))
                            //    mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
                            //else
                            //    mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = true;
                            //int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
                            //if (mpMSCSale.Rows.Count > rowindex + 1)
                            //    mpMSCSale.SetFocus(rowindex + 1, 9);
                            //}
                        }
                    }
                    //else
                    //{
                    //    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = mbalanceAmount.ToString("#0.00");
                    //}
                }
                else if (colIndex == 13) // old 12
                {
                    double mdiscgiven = 0;
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
                    mdiscountamt = Math.Round(mbalanceAmount - getclearedamt, 2);
                    if (mdiscgiven != 0)
                    {
                        if (mdiscgiven != mdiscountamt || getclearedamt == 0)
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                    }
                    totalreceived = 0;
                    foreach (DataGridViewRow dr in mpMSCSale.Rows)
                    {
                        double mcleared = 0;
                        double mdiscount = 0;
                        if (dr.Cells["Col_GetClearedAmount"].Value != null)
                            double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);

                        if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                            double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);

                        totalreceived += mcleared; 
                        //totalreceived += (mcleared + mdiscount);
                    }
                    if (mbillamt - totalreceived > 0)
                        mamtnotadj = (mbillamt - totalreceived);
                    else
                        mamtnotadj = 0;
                    txtAmtNotAdjusted.Text = mamtnotadj.ToString("#0.00");
                    //if (mpMSCSale.MainDataGridCurrentRow.Index < mpMSCSale.Rows.Count-1)

                    //    mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 12);
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

                if (e.ColumnIndex == 12 && (_CashPayment.PaymentSubType == 3 || _CashPayment.PaymentSubType == 0))// old 11
                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashPayment.CellOldValueAmount.ToString("#0.00");

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            //try
            //{

            //    if (e.ColumnIndex == 12 && (txtAmountReceived.Text != txtTotalBalance.Text && mpMSCSale.ColumnsMain["Col_Check"].ReadOnly != false))// old 11
            //        mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashPayment.CellOldValueAmount.ToString("#0.00");

            //}
            //catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }


        private void txtAmountReceived_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    FillGridForDates();
            //    txtChequeNumber.Focus();
            //}
            //else if (e.KeyCode == Keys.Up)
            //    mcbCreditor.Focus();

            if (e.KeyCode == Keys.Enter)
            {
                ClearGrid();
                txtNarration.Focus();
                mpMSCSale.ClearSelection();
                _CashPayment.PaymentSubType = 0;

                if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == 0 && _Mode == OperationMode.Add)
                {
                    mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
                    mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;
                    mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = false;
                }
                else
                {

                    mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                    mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = false;
                    mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = false;
                }
                if (_Mode == OperationMode.Add)
                {

                    if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == Convert.ToDouble(txtTotalBalance.Text.ToString()))
                    {
                        _CashPayment.PaymentSubType = 1;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;
                        FillGridForDates();
                    }
                    else if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == 0)
                    {
                        _CashPayment.PaymentSubType = 2;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = false;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;
                    }
                    else
                    {
                        _CashPayment.PaymentSubType = 3;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = false;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = false;
                    }
                }
                txtNarration.Focus();

            }
            else if (e.KeyCode == Keys.Up)
                mcbCreditor.Focus();
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
                    dr.Cells["Col_DiscountAmount"].Value = 0;
                    dr.Cells["Col_Check"].Value = " ";
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillGridForDates()
        {
            //double clearedAmount = 0;
            //double balanceAmount = 0;
            if (_CashPayment.PaymentSubType == 1)
            {
                txtAmtNotAdjusted.Text = "0.00";
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {
                    if (dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != string.Empty)
                    {
                        dr.Cells["Col_GetClearedAmount"].Value = dr.Cells["Col_BalanceAmount"].Value;
                    }
                }
            }
        }


        //    //if (e.KeyCode == Keys.Enter)
        //    //{
        //    //    FillGridForDates();
        //    //    txtChequeNumber.Focus();
        //    //}
        //    //else if (e.KeyCode == Keys.Up)
        //    //    mcbCreditor.Focus();

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        ClearGrid();

        //        if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == 0 && _Mode == OperationMode.Add)
        //        {
        //            mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
        //            mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;
        //            mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = false;
        //        }
        //        else
        //        {

        //            mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
        //            mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = false;
        //            mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = false;
        //        }
        //        FillGridForDates();
        //        txtNarration.Focus();

        //    }
        //    else if (e.KeyCode == Keys.Up)
        //        mcbCreditor.Focus();
        //}
        //private void FillGridForDates()
        //{
        //    //double clearedAmount = 0;
        //    //double balanceAmount = 0;
        //    if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == Convert.ToDouble(txtTotalBalance.Text.ToString()))
        //    {
        //        txtAmtNotAdjusted.Text = "0.00";
        //        foreach (DataGridViewRow dr in mpMSCSale.Rows)
        //        {
        //            if (dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != string.Empty)
        //            {
        //                dr.Cells["Col_GetClearedAmount"].Value = dr.Cells["Col_BalanceAmount"].Value;
        //            }
        //        }
        //    }
        //}
        private void btnModify_Click(object sender, EventArgs e)
        {
            ModifyEdit();
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (datePickerFromDate.Visible == true)
                datePickerFromDate.Focus();
            else
                txtAmountReceived.Focus();
            GetData();
        }

        private void txtAmountReceived_TextChanged(object sender, EventArgs e)
        {
            if (mpMSCSale.ColumnsMain["Col_Check"].ReadOnly == true)
                ClearGrid();
        }



        #endregion

        #region UIEvents

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void UclCashPayment_Load(object sender, EventArgs e)
        {
            //  ClearData();
            dgClearOpeningBalance.Visible = false;
            datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
            datePickerFromDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
            datePickerToDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
        }
        private void mpMSCSale_OnEnterKeyPressed(object sender, EventArgs e)
        {
            ColCheckChecked();
        }

        private void ColCheckChecked()
        {
            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value == null || mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value.ToString().Trim() == string.Empty)
            {
                mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();
            }
            else
                mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value = " ";
            double totamount = 0;
            double amt = 0;
            mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = false;
            foreach (DataGridViewRow dr in mpMSCSale.Rows)
            {
                if (dr.Cells["Col_Check"].Value.ToString().Trim() != string.Empty && dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != string.Empty)
                {
                    amt = Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
                    dr.Cells["Col_GetClearedAmount"].Value = amt.ToString("#0.00");
                    totamount += amt;
                }
                else
                    dr.Cells["Col_GetClearedAmount"].Value = "0.00";
            }
            mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
            txtAmountReceived.Text = totamount.ToString("#0.00");
            mpMSCSale.Refresh();
            mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 7);
        }
        private void mpMSCSale_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            ClearGrid();

            mcbCreditor.Focus();
        }

        private void btnClearOpeningBalance_Click(object sender, EventArgs e)
        {
            //btnClearOpeningBalanceClick();
        }
        private void btnClearOpeningBalanceClick()
        {
            double opamt = 0;
            try
            {
                if (mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value != null && mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value.ToString() != string.Empty)
                    opamt = Convert.ToDouble(mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value.ToString());
                txtOpeningBalanceAmount.Text = opamt.ToString("#0.00");
                if (opamt > 0 && dgClearOpeningBalance.Rows.Count > 0)
                {
                    dgClearOpeningBalance.Visible = true;
                    dgClearOpeningBalance.Enabled = true;
                    dgClearOpeningBalance.SetFocus(0, 11);
                }
                else if (opamt > 0)

                    lblFooterMessage.Text = "No Previous Records...";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void dgClearOpeningBalance_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    double getclearedamt = 0;
            //    if (e.ColumnIndex == 11) // old 8
            //    {
            //        double mbalanceamount = 0;
            //        double mamtnotadj = 0;
            //        // txtOpeningBalanceAmount.Text = mpMSCSale.MainDataGridCurrentRow.Cells[12].ToString();
            //        double.TryParse(txtOpeningBalanceAmount.Text, out mamtnotadj);
            //        if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString().Trim() != "")
            //            double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
            //        _CashPayment.CellOpeningBalanceOldValueAmount = getclearedamt;
            //        if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString().Trim() != "")
            //            double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceamount);
            //        if (getclearedamt == 0 && mamtnotadj != 0)
            //        {
            //            double clearedamt = 0;
            //            clearedamt = Math.Min(mamtnotadj, mbalanceamount);
            //            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;

            //        }
            //        dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].ReadOnly = false;
            //    }
            //}
            //catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgClearOpeningBalance_OnCellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{

            //    if (e.ColumnIndex == 11) // old 8
            //        dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashPayment.CellOpeningBalanceOldValueAmount.ToString("#0.00");

            //}
            //catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgClearOpeningBalance_OnCellValueChangeCommited(int colIndex)
        {
            //txtAmountReceived.Enabled = false;
            //txtOpeningBalanceAmount.Enabled = false;
            //double totalreceived = 0;
            //double getclearedamt = 0;
            //double mbalanceAmount = 0;
            //double mamtnotadj = 0;
            //double clearedamt = 0;
            //double mbillamt = 0;
            //double mdiscountamt = 0;
            //mbillamt = Convert.ToDouble(mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value.ToString());
            //double.TryParse(txtOpeningBalanceAmount.Text, out mamtnotadj);

            //if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null)
            //    double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
            //if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
            //    double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);
            //double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceAmount);

            //clearedamt = Math.Min(mamtnotadj, mbalanceAmount);
            //try
            //{

            //    if (colIndex == 11) // old 8
            //    {

            //        if (getclearedamt == 0)
            //        {
            //            _CashPayment.CellOpeningBalanceOldValueAmount = 0;
            //        }

            //        if (mbalanceAmount < getclearedamt)
            //        {
            //            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
            //            _CashPayment.CellOpeningBalanceOldValueAmount = clearedamt;
            //        }
            //        else
            //        {
            //            if (mamtnotadj == 0)
            //                dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _CashPayment.CellOpeningBalanceOldValueAmount;


            //            //if (clearedamt <= Math.Min(mamtnotadj, mbalanceAmount) || (clearedamt <= _CashReceipt.CellOldValueAmount))
            //            //{

            //            _CashPayment.CellOpeningBalanceOldValueAmount = getclearedamt;
            //            foreach (DataGridViewRow dr in dgClearOpeningBalance.Rows)
            //            {
            //                double mcleared = 0;
            //                double mdiscount = 0;
            //                if (dr.Cells["Col_GetClearedAmount"].Value != null)
            //                    double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
            //                if (dr.Index == dgClearOpeningBalance.MainDataGridCurrentRow.Index)
            //                    mcleared = getclearedamt;
            //                if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
            //                    double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
            //                if (mcleared > 0)
            //                    totalreceived += mcleared;
            //            }

            //            mamtnotadj = (mbillamt - totalreceived);

            //            if (mamtnotadj < 0)
            //            {
            //                mamtnotadj = 0;
            //                _CashPayment.CellOpeningBalanceOldValueAmount = 0;
            //                _CashPayment.CellOpeningBalanceOldValueDiscount = 0;
            //                getclearedamt = 0;
            //            }
            //            txtOpeningBalanceAmount.Text = mamtnotadj.ToString("#0.00");
            //            if (mamtnotadj >= 0 && getclearedamt > 0)
            //            {
            //                mdiscountamt = mbalanceAmount - getclearedamt;
            //                if (mdiscountamt > 10)
            //                    mdiscountamt = 0;
            //            }
            //            else
            //                mdiscountamt = 0;


            //            if (getclearedamt == 0)
            //                dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
            //            else
            //                dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt.ToString("#0.00");
            //            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
            //            //int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
            //            //if (mpMSCSale.Rows.Count > rowindex + 1)
            //            //    mpMSCSale.SetFocus(rowindex + 1, 9);
            //            //}
            //        }
            //    }
            //    else if (colIndex == 12)
            //    {
            //        double mdiscgiven = 0;
            //        if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
            //            double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
            //        mdiscountamt = Math.Round(mbalanceAmount - getclearedamt, 2);
            //        if (mdiscgiven != 0)
            //        {
            //            if (mdiscgiven != mdiscountamt || getclearedamt == 0)
            //                dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
            //        }
            //        //if (mpMSCSale.MainDataGridCurrentRow.Index < mpMSCSale.Rows.Count-1)

            //        //    mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 12);
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteException(Ex);
            //}
        }


        private bool FillOpeningBalanceGrid(string vmode)
        {
            bool retValue = false;
            try
            {
                ConstructdgClearOpeningBalanceTempMainColumns();

                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_BillAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_BalanceAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_ClearedAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_GetClearedAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_DiscountAmount");
                //dgClearOpeningBalanceTemp.DateColumnNames.Add("Col_VoucherDate");
                //dgClearOpeningBalanceTemp.DateColumnNames.Add("Col_BillFromDate");



                ConstructdgClearOpeningBalanceColumns();

                dgClearOpeningBalance.DoubleColumnNames.Add("Col_GetClearedAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_BillAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_BalanceAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_ClearedAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_DiscountAmount");
                dgClearOpeningBalance.DateColumnNames.Add("Col_VoucherDate");
                dgClearOpeningBalance.DateColumnNames.Add("Col_BillFromDate");

                DataTable dtable = new DataTable();
                if (_CashPayment.CBAccountID != null && _CashPayment.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_CashPayment.ActualAccountID != _CashPayment.CBAccountID && _CashPayment.ModifyEdit == "Y"))
                    {
                        _saledtable = _CashPayment.ReadOldBillDetailsByID();
                        _statementdtable = _CashPayment.ReadOldStatementDetailsByID();
                        dgClearOpeningBalance.Rows.Clear();
                        BinddgClearOpeningBalanceGrid(_saledtable, _statementdtable);
                        //IfOpeningAdded = false;
                        //BinddgClearOpeningBalanceGrid(_saledtable, _statementdtable);
                        NoofRows();
                    }
                    else
                    {
                        dgClearOpeningBalanceTemp.Rows.Clear();
                        _statementdtable = null;
                        if (vmode == "C")
                            _saledtable = _CashPayment.ReadOldBillDetailsByCSPIDForChanged();
                        else if (vmode == "D")
                            _saledtable = _CashPayment.ReadOldBillDetailsByCSPIDForDeleted();
                        else
                        {
                            /////////  _saledtable = _CashReceipt.ReadOldBillDetailsByCSRID();
                            //  _statementdtable = _CashReceipt.ReadStatementDetailsByCSRID();

                            _saledtable = _CashPayment.ReadOldBillDetailsByID();
                            _statementdtable = _CashPayment.ReadOldStatementDetailsByID();
                            dgClearOpeningBalance.Rows.Clear();
                            BinddgClearOpeningBalanceGrid(_saledtable, _statementdtable);

                        }
                        BinddgClearOpeningBalanceTempGrid(_saledtable, _statementdtable);
                        // NoofRowsFormpMSVCGrid();
                        //foreach (DataRow dr in _saledtable.Rows)
                        //{
                        //    if (dr["VoucherType"] != DBNull.Value && dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForOpeningBalance)
                        //    {
                        //        _CashReceipt.OpeningClearedInVoucher = Convert.ToDouble(dr["AmountClear"].ToString());
                        //        _CashReceipt.DiscountInOpeningBalance = Convert.ToDouble(dr["DiscountAmount"].ToString());
                        //        break;
                        //    }
                        //}
                        retValue = true;
                        //txtVouchernumber.BackColor = Color.White;
                        //btnModify.BackColor = General.ControlFocusColor;
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

        private void BinddgClearOpeningBalanceTempGrid(DataTable saletable, DataTable statementtable)
        {
            // view
            mpMSVC.Rows.Clear();
            int _rowIndex = 0;
            IfOpeningAdded = false;
            if (_Mode == OperationMode.Add)
            {
                if ((_CashPayment.OpeningBalance - _CashPayment.OpeningCleared) > 0)
                {
                    _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                    DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = "OPB";
                    currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                    currentdr.Cells["Col_BillType"].Value = "OPB";
                    currentdr.Cells["Col_BillNumber"].Value = "";
                    currentdr.Cells["Col_BillSubType"].Value = "";
                    currentdr.Cells["Col_BillFromDate"].Value = "";
                    currentdr.Cells["Col_BillAmount"].Value = _CashPayment.OpeningBalance.ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = "";
                    currentdr.Cells["Col_BalanceAmount"].Value = (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared).ToString();
                    currentdr.Cells["Col_ClearedAmount"].Value = "";
                    IfOpeningAdded = true;
                }
            }
            if (saletable != null && saletable.Rows.Count > 0)
            {
                foreach (DataRow dr in saletable.Rows)
                {
                    _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                    DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                    currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                    currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                    //currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                    currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                    currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                    currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                    currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                    currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                    currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                    currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
            {

                foreach (DataRow dr in statementtable.Rows)
                {
                    _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                    DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                    currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                    currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                    //currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                    currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                    currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                    currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                    currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                    currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                    currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                    currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                }

            }
        }

        private void BinddgClearOpeningBalanceGrid(DataTable saletable, DataTable statementtable)
        {
            //modify
            bool ifIDFound = false;
            int _rowIndex = 0;
            string iD = "";
            IfOpeningAdded = true;
            try
            {
                if (IfOpeningAdded != true && (_Mode != OperationMode.Edit) && (_Mode == OperationMode.Add || ((_CashPayment.preAccountId != null && _CashPayment.preAccountId != "") && _CashPayment.CBAccountID != _CashPayment.preAccountId) || _CashPayment.ModifyEdit == "Y"))
                {
                    if ((_CashPayment.OpeningClearedInVoucher > 0 || (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher + _CashPayment.DiscountInOpeningBalance) > 0) && (_CashPayment.preAccountId == null || _CashPayment.CBAccountID == _CashPayment.preAccountId))
                    {
                        _rowIndex = dgClearOpeningBalance.Rows.Add();
                        DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "OPB";
                        currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                        currentdr.Cells["Col_BillType"].Value = "OPB";
                        currentdr.Cells["Col_BillNumber"].Value = "";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = "";
                        currentdr.Cells["Col_BillAmount"].Value = _CashPayment.OpeningBalance.ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = "";
                        if ((_CashPayment.preAccountId == null || _CashPayment.preAccountId == "") || ((_CashPayment.preAccountId != null || _CashPayment.preAccountId != "") && _CashPayment.CBAccountID == _CashPayment.preAccountId))
                            currentdr.Cells["Col_BalanceAmount"].Value = (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher + _CashPayment.DiscountInOpeningBalance).ToString();
                        else
                            currentdr.Cells["Col_BalanceAmount"].Value = _CashPayment.OpeningBalance;
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                        IfOpeningAdded = true;
                    }
                }
                else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
                {
                    if (_CashPayment.OpeningClearedInVoucher >= 0)
                    {
                        if (_CashPayment.OpeningBalance > 0 && (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher + _CashPayment.DiscountInOpeningBalance) > 0)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = "OPB";
                            currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                            currentdr.Cells["Col_BillType"].Value = "OPB";
                            currentdr.Cells["Col_BillNumber"].Value = "";
                            currentdr.Cells["Col_BillSubType"].Value = "";
                            currentdr.Cells["Col_BillFromDate"].Value = "";
                            currentdr.Cells["Col_BillAmount"].Value = _CashPayment.OpeningBalance.ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = "";
                            currentdr.Cells["Col_BalanceAmount"].Value = (_CashPayment.OpeningBalance - _CashPayment.OpeningCleared + _CashPayment.OpeningClearedInVoucher + _CashPayment.DiscountInOpeningBalance).ToString();
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
                            _rowIndex = dgClearOpeningBalance.Rows.Add();
                            DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                            currentdr.Cells["Col_Series"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_Type"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_Number"].Value = dr["VoucherNumber"].ToString();
                            //currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            //  currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                            currentdr.Cells["Col_Discountamount"].Value = "0.00";
                            //  currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            currentdr.Cells["Col_MasterID"].Value = dr["PurchaseID"].ToString();
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
                            _rowIndex = dgClearOpeningBalance.Rows.Add();
                            DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                            currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                            //currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                            currentdr.Cells["Col_Discountamount"].Value = "0.00";
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void ConstructdgClearOpeningBalanceColumns()
        {
            DataGridViewTextBoxColumn column;
            dgClearOpeningBalance.ColumnsMain.Clear();
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.HeaderText = "ID";
                column.Width = 0;
                column.Visible = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Series";
                column.HeaderText = "YEAR";
                column.Width = 60;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Type";
                column.HeaderText = "Type";
                column.ReadOnly = true;
                column.Width = 55;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Number";
                column.HeaderText = "Number";
                column.Width = 75;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.HeaderText = "SUB";
                column.Width = 35;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.HeaderText = "Date";
                column.Width = 110;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.HeaderText = "BillNumber";
                column.Width = 80;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.HeaderText = "BalanceAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);

                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                column.Visible = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);

                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.HeaderText = "ClearedAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.HeaderText = "Discount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.Visible = true;
                column.ReadOnly = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructdgClearOpeningBalanceTempMainColumns()
        {
            DataGridViewTextBoxColumn column;
            dgClearOpeningBalanceTemp.Columns.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "VoucherSeriesT";
                column.Width = 70;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "VoucherType";
                column.ReadOnly = true;
                column.Width = 80;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "VoucherNumber";
                column.Width = 100;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SubType";
                column.Width = 45;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "FromDate";
                column.Width = 80;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.DataPropertyName = "MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.DataPropertyName = "PatientShortName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);

                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "DiscountAmount";
                column.Width = 110;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool SaveOldDetails()
        {
            bool returnVal = true;
            _CashPayment.SerialNumber = 0;
            try
            {
                foreach (DataGridViewRow drow in dgClearOpeningBalance.Rows)
                {
                    if (drow.Cells["Col_GetClearedAmount"].Value != null && drow.Cells["Col_GetClearedAmount"].Value.ToString() != "" &&
                       Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value) > 0)
                    {
                        _CashPayment.SerialNumber += 1;
                        //   _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        if (drow.Cells["Col_MasterID"].Value != null)
                            _CashPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                        _CashPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                        _CashPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                        _CashPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                        if (drow.Cells["Col_Number"].Value.ToString() != string.Empty)
                            _CashPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString());
                        if (drow.Cells["Col_BillSubType"].Value.ToString() != string.Empty)
                            _CashPayment.DSubType = drow.Cells["Col_BillSubType"].Value.ToString();
                        if (drow.Cells["Col_BillFromDate"].Value.ToString() != string.Empty)
                            _CashPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                        _CashPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                        _CashPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                        _CashPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value.ToString());
                        _CashPayment.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                        //  returnVal = _CashReceipt.AddParticularsDetailsOldSale();                        

                        if (_CashPayment.DVoucherType == FixAccounts.VoucherTypeForStatementPurchase)
                            returnVal = _CashPayment.UpdatePurchaseStatementOld();
                        else if (_CashPayment.DVoucherType == FixAccounts.VoucherTypeForCreditPurchase)
                            returnVal = _CashPayment.UpdatePurchaseBillOld();


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

        private bool FillOpeningBalanceTempGrid()
        {
            bool retValue = false;
            try
            {
                ConstructdgClearOpeningBalanceTempMainColumns();
                //  FormatOpeningBalanceTempGrid();
                DataTable dtable = new DataTable();
                dtable = _CashPayment.ReadBillDetailsByCSPIDFromtblOld();
                //  _statementdtable = _CashPayment.ReadStatementDetailsByCSRIDFromtblOld();
                //  mpPVCTemp.DataSource .DataSourceMain = dtable;
                BindOpeningBalanceTempGrid(dtable, _statementdtable);
                retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }

        private void BindOpeningBalanceTempGrid(DataTable saletable, DataTable statementtable)
        {
            int _rowIndex = 0;
            string iD = "";

            if (saletable != null && saletable.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        //ifIDFound = SearchforIDInSaleGrid(iD);
                        //if (ifIDFound == false)
                        //{
                        _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                        DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        //currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        //}

                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());


                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
            {

                foreach (DataRow dr in statementtable.Rows)
                {
                    try
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                        DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        //currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        //}
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex.ToString());

                    }
                }

            }
        }

        private void btnOKSelectedTotal_Click(object sender, EventArgs e)
        {
            BtnOKSelectedTotalClick();
        }

        private void BtnOKSelectedTotalClick()
        {
            //if (colIndex == 7)
            //{
            double mselectedtotal = 0;
            double mamt = 0;
            bool ch = false;
            {
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {
                    mamt = 0;
                    ch = false;
                    if (dr.Cells["Col_Check"].Value != null)
                        ch = Convert.ToBoolean(dr.Cells["Col_Check"].Value.ToString());
                    if (ch == true)
                    {
                        mamt = Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
                        dr.Cells["Col_GetClearedAmount"].Value = mamt.ToString("#0.00");
                    }
                    mselectedtotal += mamt;
                }
                mpMSCSale.Refresh();
                txtSelectedTotal.Text = mselectedtotal.ToString("#0.00");
                txtAmountReceived.Text = mselectedtotal.ToString("#0.00");
                txtAmtNotAdjusted.Text = "0.00";
                mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                txtAmountReceived.Focus();
            }
            //}
        }
        private void datePickerFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime dd = datePickerFromDate.Value.AddMonths(1).AddDays(-1);
                datePickerToDate.Value = dd;
                datePickerToDate.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                mcbCreditor.Focus();
        }

        private void datePickerToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetData();
                txtAmountReceived.Text = txtTotalBalance.Text;
                txtAmountReceived.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                datePickerFromDate.Focus();
        }

        #endregion UIEvents

        #region tooltip

        #endregion
    }
}
