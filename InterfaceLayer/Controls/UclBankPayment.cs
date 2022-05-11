using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using EcoMart.InterfaceLayer.CommonControls;
using PrintDataGrid;
using EcoMart.InterfaceLayer.Classes;
using System.Data;
using EcoMartLicenseLib;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclBankPayment : BaseControl
    {
        #region Declaration
        private BankPayment _BankPayment;
        string DefaultBankID = "";
        DataTable _saledtable = new DataTable();
        DataTable _statementdtable = new DataTable();
        bool IfOpeningAdded = false;
        private BaseControl ViewControl;
        private Form frmView;
        #endregion

        # region Constructor
        public UclBankPayment()
        {
            try
            {
                InitializeComponent();
                _BankPayment = new BankPayment();
                SearchControl = new UclBankPaymentSearch();
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
                    mcbBankAccount.Focus();
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
                _BankPayment.Initialise();
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
                headerLabel1.Text = "BANK PAYMENT -> NEW";
                FillGrids();
                //GetDefaultBank();               
                EnableDisableAdd();
                mcbBankAccount.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void FillGrids()
        {
            FillmpPVC1GridSale();
            FillPartyCombo();
            string oldBankID = "";
            if (mcbBankAccount.SelectedID != null)
                oldBankID = mcbBankAccount.SelectedID;
            FillBankAccountCombo();
            _BankPayment.CBBankAccountID = oldBankID;
            mcbBankAccount.SelectedID = oldBankID;
            if (_Mode != OperationMode.Add)
                FillmpMSVCGrid("");
        }

        private void EnableDisableAdd()
        {
            btnModify.Visible = false;
            mpMSCSale.Visible = true;         
            mpMSVC.Visible = false;           
            mcbCreditor.Enabled = true;
            txtAmountReceived.Enabled = true;
            pnlVouTypeNo.Enabled = true;
            pnlVou.Enabled = true;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                ClearData();
                if (_Mode == OperationMode.Edit)
                    headerLabel1.Text = "BANK PAYMENT -> EDIT";
                else
                    headerLabel1.Text = "BANK PAYMENT -> VOUCHER DATE CHANGE";
                FillGrids();
                FillOpeningBalanceGrid("");
                datePickerFromDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                datePickerToDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                //   datePickerBillDate.Value = DateTime.Now;
                EnableDisableForEdit();
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
            ClearData();
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {                
                headerLabel1.Text = "BANK PAYMENT -> DELETE";
                ClearData();
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                //datePickerBillDate.Value = DateTime.Now;
                FillGrids();
                EnableDisable();
                txtVouchernumber.Focus();
               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        private void EnableDisable()
        {
           
            btnModify.Visible = false;
            mpMSCSale.Visible = true;       
            mpMSVC.Visible = false;       
            mcbCreditor.Enabled = false;
            txtAmountReceived.Enabled = false;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;           
            txtVouchernumber.Focus();
            if (_Mode == OperationMode.Add)
                mcbCreditor.Enabled = true;
            else
                mcbCreditor.Enabled = false;

        }
        private void EnableDisableForEdit()
        {

            btnModify.Visible = false;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = false;
            pnlAddress.Enabled = true;
            txtAmountReceived.Enabled = false;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;          
            txtVouchernumber.Focus();
            if (_Mode == OperationMode.Add)
                mcbCreditor.Enabled = true;
            else
                mcbCreditor.Enabled = false;

        }
        public override bool ProcessDelete()
        {
            bool retValue = false;
            try
            {
                if (_BankPayment.Id != null && _BankPayment.Id != "")
                {
                    LockTable.LockTablesForCashBankPayment();
                    if (_BankPayment.CanBeDeleted())
                    {
                        General.BeginTransaction();
                        retValue = _BankPayment.DeleteDetails();
                        if (retValue)
                        {
                            _BankPayment.DeletePreviousRecords();
                            RevertPreviousPurchaseBalance();
                            _BankPayment.DeleteJV();
                        }
                        retValue = _BankPayment.DeleteAccountDetails();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            MessageBox.Show("Deleted Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _BankPayment.ModifiedBy = General.CurrentUser.Id;
                            _BankPayment.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _BankPayment.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            _BankPayment.AddDeletedDetails();
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
            }
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            try
            {          
                headerLabel1.Text = "BANK PAYMENT -> VIEW";
                ClearData();
              //  datePickerBillDate.Value = DateTime.Now;
                FillGrids();
                FillBankAccountCombo();
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
                    _BankPayment.CBVouType = FixAccounts.VoucherTypeForBankPayment;
                }
                _BankPayment.GetLastRecord();
                FillSearchData(_BankPayment.Id, "");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                if (General.EcoMartLicense.LicenseType == LicenseTypes.Full)
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
                    if (dr.Cells[0].Value != null && (dr.Cells["Col_GetClearedAmount"].Value != null || dr.Cells["Col_ClearedAmount"].Value != null) &&  Convert.ToDouble(dr.Cells["Col_GetClearedAmount"].Value.ToString()) > 0)
                    {
                        int printgridindex = PrintGrid.Rows.Add();
                        if (dr.Cells["Col_BillSeries"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_Quantity"].Value = dr.Cells["Col_BillSeries"].Value.ToString();
                        if (dr.Cells["Col_BillType"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_ProductName"].Value = dr.Cells["Col_BillType"].Value.ToString();
                        if (dr.Cells["Col_BillNumber"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_Shelf"].Value = dr.Cells["Col_BillNumber"].Value.ToString();
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
            string atow = General.AmountToWord(_BankPayment.CBAmount);
            printer.Print(_BankPayment.CBVouType, _BankPayment.CBVouNo.ToString(), _BankPayment.CBVouDate, _BankPayment.CBName, _BankPayment.CBAddress1, _BankPayment.CBAddress2, "", "", PrintGrid.Rows, _BankPayment.CBNarration.ToString()+" Chq No:"+_BankPayment.CBChequeNumber.ToString()+" Dt:"+General.GetDateInShortDateFormat(_BankPayment.CBChequeDate), _BankPayment.CBAmount, "", _BankPayment.CBTotalDiscount, 0, 0, 0, 0, atow);

        }

        private void PrintCashBankVoucherPlainPaper()
        {
            EcoMart.Printing.PlainPaperPrinter printer = new EcoMart.Printing.PlainPaperPrinter();
            string atow = General.AmountToWord(_BankPayment.CBAmount);
            printer.Print(_BankPayment.CBVouType, _BankPayment.CBVouNo.ToString(), _BankPayment.CBVouDate, _BankPayment.CBName, _BankPayment.CBAddress1, _BankPayment.CBAddress2, "", "", PrintGrid.Rows, _BankPayment.CBNarration.ToString() + " Chq No:" + _BankPayment.CBChequeNumber.ToString() + " Dt:" + General.GetDateInShortDateFormat(_BankPayment.CBChequeDate), _BankPayment.CBAmount, "", _BankPayment.CBTotalDiscount, 0, 0, 0, 0, atow);

        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _BankPayment.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _BankPayment.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _BankPayment.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _BankPayment.GetFirstRecord();
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _BankPayment.Id = dr["CBID"].ToString();
                FillSearchData(_BankPayment.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _BankPayment.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _BankPayment.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _BankPayment.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _BankPayment.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _BankPayment.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _BankPayment.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _BankPayment.CBVouNo = i;
                dr = _BankPayment.ReadDetailsByVoucherNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _BankPayment.Id = dr["CBID"].ToString();
                FillSearchData(_BankPayment.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _BankPayment.GetLastVoucherNumber(FixAccounts.VoucherTypeForBankPayment, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _BankPayment.CBVouType = txtVouType.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _BankPayment.CBVouNo = i;
                dr = _BankPayment.ReadDetailsByVoucherNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _BankPayment.Id = dr["CBID"].ToString();
                FillSearchData(_BankPayment.Id, "");
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
                    _BankPayment.CBTotalDiscount = GetTotalDiscount();
                    _BankPayment.CBAccountID = mcbCreditor.SelectedID;
                    _BankPayment.CBAmount = Convert.ToDouble(txtAmountReceived.Text.ToString());
                    _BankPayment.CBNarration = txtNarration.Text.ToString().Trim();
                    _BankPayment.CBBankAccountID = mcbBankAccount.SelectedID;
                    _BankPayment.CBName = mcbCreditor.SeletedItem.ItemData[2];
                    _BankPayment.CBBankName = mcbBankAccount.SeletedItem.ItemData[1];
                    _BankPayment.MobileNumberForSMS = mcbCreditor.SeletedItem.ItemData[7]; //Amar
                    _BankPayment.CBChequeNumber = txtChequeNumber.Text.ToString();
                    _BankPayment.CBChequeDate = datePickerChequeDate.Value.Date.ToString("yyyyMMdd");
                    if (txtAmtNotAdjusted.Text != null)
                    {
                        _BankPayment.CBOnAccountAmount = Convert.ToDouble(txtAmtNotAdjusted.Text.ToString());
                    }
                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _BankPayment.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString().Trim());
                    _BankPayment.CBVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        LockTable.LocktblVoucherNo();
                        _BankPayment.CBVouNo = _BankPayment.GetAndUpdateBKPNumber(General.ShopDetail.ShopVoucherSeries);
                    }
                    _BankPayment.Validate();
                    if (_BankPayment.IsValid)
                    {
                        LockTable.LockTablesForCashBankPayment();
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            General.BeginTransaction();
                            //_BankPayment.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _BankPayment.CreatedBy = General.CurrentUser.Id;
                            _BankPayment.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _BankPayment.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            //_BankPayment.CBVouNo = _BankPayment.GetAndUpdateBKPNumber(General.ShopDetail.ShopVoucherSeries);
                            txtVouchernumber.Text = _BankPayment.CBVouNo.ToString();
                            //if (_BankPayment.CBTotalDiscount > 0)
                            //{
                            //    _BankPayment.CBJVNo = _BankPayment.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                            //    _BankPayment.CBJVID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            //}
                            if (_BankPayment.CBTotalDiscount > 0)
                            {
                                _BankPayment.CBJVNo = _BankPayment.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                            }
                            else
                            {
                                _BankPayment.CBJVNo = 0;
                                _BankPayment.CBJVIDpay = 0;
                            }
                            //retValue = _BankPayment.AddDetails();
                            _BankPayment.IntID = 0;
                            _BankPayment.IntID = _BankPayment.AddDetails();
                            if (_BankPayment.IntID > 0)
                                retValue = true;
                            else
                                retValue = false;
                            _SavedID = _BankPayment.Id;
                            if (retValue)
                                retValue = saveDetails();
                            if (retValue)
                                retValue = _BankPayment.SaveIntblTrnac();
                            if (retValue && _BankPayment.CBTotalDiscount > 0)
                            {
                                //retValue = _BankPayment.AddToMasterJV();
                                _BankPayment.CBJVIDpay = _BankPayment.AddToMasterJV();
                                retValue = _BankPayment.UpdateJVIDInVoucherBankPayment();// update jvid with update jv number
                                _BankPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _BankPayment.AddJVTotblTrnacDebit();
                                _BankPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _BankPayment.AddJVTotblTrnacCredit();
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
                                string msgLine2 = _BankPayment.CBVouType + "  " + _BankPayment.CBVouNo.ToString("#0");
                                PSDialogResult result;
                                if (printData)
                                {
                                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                    Print();
                                    //Amar For Sms
                                    if (General.CurrentSetting.SmsSetBankPaymentSale=="Y")
                                    {
                                        SendSMS mSMS = new SendSMS();
                                        string Msg = "Dear Customer Your Bill No.:" + _BankPayment.CBVouNo + " Of Amount :" + _BankPayment.CBAmount + " Thank You For Dealing With Us.";
                                        Msg += mSMS.GetShopDetailsFromData();
                                        if (string.IsNullOrEmpty(_BankPayment.MobileNumberForSMS) == false)
                                        {
                                            mSMS.SendSMSData(_BankPayment.MobileNumberForSMS, Msg);
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
                                    if (General.CurrentSetting.SmsSetBankPaymentSale == "Y")
                                    {
                                        SendSMS mSMS = new SendSMS();
                                        string Msg = "Dear Customer Your Bill No.:" + _BankPayment.CBVouNo + " Of Amount :" + _BankPayment.CBAmount + " Thank You For Dealing With Us.";
                                        Msg += mSMS.GetShopDetailsFromData();
                                        if (string.IsNullOrEmpty(_BankPayment.MobileNumberForSMS) == false)
                                        {
                                            mSMS.SendSMSData(_BankPayment.MobileNumberForSMS, Msg);
                                        }
                                        else
                                            MessageBox.Show("Please Update Mobile Number For Sending SMS", "EcoMart", MessageBoxButtons.OK);
                                    }
                                }
                            _SavedID = _BankPayment.Id;                    
                            retValue = true;
                            }

                            else
                            {
                                MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                retValue = false;
                            }
                        }
                        else if (_Mode == OperationMode.Fifth)
                        {
                            General.BeginTransaction();
                            _BankPayment.CBAccountID = mcbCreditor.SelectedID;
                            _BankPayment.ModifiedBy = General.CurrentUser.Id;
                            _BankPayment.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _BankPayment.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            retValue = _BankPayment.UpdateDetailsForFifth();
                            retValue = _BankPayment.SaveIntblTrnacForFifth();
                            General.CommitTransaction();                           
                            LockTable.UnLockTables();
                            
                                _BankPayment.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                _BankPayment.AddChangedDetails();
                                AddPreviousRowsInChangedDetail();

                                string msgLine2 = _BankPayment.CBVouType + "  " + _BankPayment.CBVouNo.ToString("#0");
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
                                _SavedID = _BankPayment.Id;


                                retValue = true;
                            
                        }
                            
                        else   if (_Mode == OperationMode.Edit)
                        {
                            if (_BankPayment.ModifyEdit == "Y")
                            {
                                General.BeginTransaction();
                                _BankPayment.CBAccountID = mcbCreditor.SelectedID;
                                _BankPayment.ModifiedBy = General.CurrentUser.Id;
                                _BankPayment.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _BankPayment.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                                if (_BankPayment.CBTotalDiscount > 0)
                                {
                                    _BankPayment.CBJVNo = _BankPayment.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                                    //_CashPayment.CBJVID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                }
                                else
                                {
                                    _BankPayment.CBJVIDpay = 0;
                                    _BankPayment.CBJVNo = 0;
                                }
                                retValue = _BankPayment.UpdateDetails();
                                if (retValue)
                                {
                                    _BankPayment.DeletePreviousRecords();
                                    RevertPreviousPurchaseBalance();
                                    saveDetails();
                                }
                                if (retValue)
                                {
                                    retValue = _BankPayment.DeleteJV();
                                    retValue = _BankPayment.DeleteAccountDetails();
                                }
                                if (retValue)
                                    retValue = _BankPayment.SaveIntblTrnac();
                                if (retValue && _BankPayment.CBTotalDiscount > 0)
                                {                                  
                                    _BankPayment.CBJVIDpay = _BankPayment.AddToMasterJV();
                                    retValue = _BankPayment.UpdateJVIDInVoucherBankPayment();// update jvid with update jv number
                                    _BankPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _BankPayment.AddJVTotblTrnacDebit();
                                    _BankPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _BankPayment.AddJVTotblTrnacCredit();
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
                                  
                                    _BankPayment.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _BankPayment.AddChangedDetails();
                                    AddPreviousRowsInChangedDetail();

                                    string msgLine2 = _BankPayment.CBVouType + "  " + _BankPayment.CBVouNo.ToString("#0");
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
                                    _SavedID = _BankPayment.Id;

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
                        foreach (string _message in _BankPayment.ValidationMessages)
                        {
                            _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                        }
                        MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception Ex)
            {
                LockTable.UnLockTables();
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
                    _BankPayment.Id = ID;
                    if (Vmode == "C")
                        _BankPayment.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _BankPayment.ReadDetailsByIDForDeleted();
                    else
                    _BankPayment.ReadDetailsByID();
                        
                    mcbBankAccount.SelectedID = _BankPayment.CBBankAccountID;                  
                    _BankPayment.ActualAccountID = _BankPayment.CBAccountID;
                    _BankPayment.CBBankName = mcbBankAccount.SeletedItem.ItemData[2];                  
                    mpMSCSale.Visible = false;               
                    mpMSVC.Visible = true;
              
                    FillmpMSVCGrid(Vmode);

                    FillmpPVCTempGrid();
                    DateTime mydate = new DateTime(Convert.ToInt32(_BankPayment.CBVouDate.Substring(0, 4)), Convert.ToInt32(_BankPayment.CBVouDate.Substring(4, 2)), Convert.ToInt32(_BankPayment.CBVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    mcbCreditor.SelectedID = _BankPayment.CBAccountID;
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                    _BankPayment.CBAddress1 = txtAddress1.Text.ToString();
                    _BankPayment.CBAddress2 = txtAddress2.Text.ToString();
                    txtNarration.Text = _BankPayment.CBNarration;
                    _BankPayment.CBName = mcbCreditor.SeletedItem.ItemData[2];
                    txtVouchernumber.Text = _BankPayment.CBVouNo.ToString();
                    txtChequeNumber.Text = _BankPayment.CBChequeNumber.ToString();
                    datePickerChequeDate.Value = General.ConvertStringToDateyyyyMMdd(_BankPayment.CBChequeDate);
                    txtVouType.Text = FixAccounts.VoucherTypeForBankPayment;
                    txtAmountReceived.Text = _BankPayment.CBAmount.ToString("#0.00");
                    txtAmtNotAdjusted.Text = _BankPayment.CBOnAccountAmount.ToString("#0.00");
              
                    pnlVou.Enabled = false;
                    if (_Mode == OperationMode.Edit)
                    {
                        mpMSCSale.ClearSelection();
                        mpMSVC.ClearSelection();
                        btnModify.Visible = true;
                        btnModify.Enabled = true;
                        btnModify.Focus();
                        btnModify.BackColor = General.ControlFocusColor;
                    }
                    if (_Mode == OperationMode.Fifth)
                    {
                        pnlVou.Enabled = true;
                        datePickerBillDate.Enabled = true;
                        mpMSVC.Enabled = false;
                        datePickerBillDate.Focus();

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
            try
            {
                if (closedControl is UclAccount)
                {
                    FillPartyCombo();
                    //Account Acc = new Account();
                    //DataTable dt = Acc.GetOverviewData();
                    //mpMSVC.DataSource = dt;
                    //mpMSVC.ReBindSubGrid();
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
                
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    mcbCreditor.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    txtAmountReceived.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                    txtNarration.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    datePickerBillDate.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    txtChequeNumber.Focus();
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
                _BankPayment.ModifyEdit = "Y";  
                                    
                retValue = FillmpPVC1GridPurchaseforModify();
                FillOpeningBalanceGrid("");
             //   retValue = RevertPreviousEntry();
             //   FillDiscountFromTempGrid();
                mpMSCSale.Refresh();
                headerLabel1.Text = "BANK PAYMENT -> MODIFY";
             
                EnableDisableForModify();
                txtAmtNotAdjusted.Text = _BankPayment.CBAmount.ToString("#0.00");
                txtAmountReceived.Enabled = true;
                txtAmountReceived.Focus();
               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
            txtChequeNumber.Enabled = true;
            datePickerChequeDate.Enabled = true;
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
            //////bool returnVal = true;
            //////try
            //////{
            //////    foreach (DataGridViewRow drowMSCSale in mpMSCSale.Rows)
            //////    {

            //////        double mClearedAmount = 0;
            //////        double mdiscountamt = 0;


            //////        if (drowMSCSale.Cells["Col_ID"].Value != null && drowMSCSale.Cells["Col_Type"].Value.ToString() != "OPB")
            //////        {

            //////            double mbalaceamount = 0;

            //////            if (drowMSCSale.Cells["Col_BalanceAmount"].Value != null)
            //////                double.TryParse(drowMSCSale.Cells["Col_BalanceAmount"].Value.ToString(), out mbalaceamount);
            //////            if (drowMSCSale.Cells["Col_ClearedAmount"].Value != null)
            //////                double.TryParse(drowMSCSale.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
            //////            if (drowMSCSale.Cells["Col_DiscountAmount"].Value != null && drowMSCSale.Cells["Col_DiscountAmount"].Value.ToString() != "")
            //////                double.TryParse(drowMSCSale.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);
            //////            //}
            //////            drowMSCSale.Cells["Col_BalanceAmount"].ReadOnly = false;
            //////            drowMSCSale.Cells["Col_BalanceAmount"].Value = mbalaceamount + mClearedAmount + mdiscountamt;
            //////            drowMSCSale.Cells["Col_GetClearedAmount"].Value = "";
            //////            drowMSCSale.Cells["Col_BalanceAmount"].ReadOnly = true;

            //////            //}
            //////        }

            //////    }

            //////}
            //////catch (Exception ex)
            //////{
            //////    Log.WriteError(ex.ToString());
            //////    returnVal = false;
            //////}
            //////return returnVal;

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
                    if (drowPVCTemp.Cells["Col_MasterPurchaseID"].Value != null)
                        mSaleID = drowPVCTemp.Cells["Col_MasterPurchaseID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_MasterID"].Value != null)
                        mcbid = drowPVCTemp.Cells["Col_MasterID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_ClearedAmount"].Value != null)
                        double.TryParse(drowPVCTemp.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                    if (drowPVCTemp.Cells["Col_DiscountAmount"].Value != null && drowPVCTemp.Cells["Col_DiscountAmount"].Value.ToString() != "")
                        double.TryParse(drowPVCTemp.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscamount);
                    if (drowPVCTemp.Cells["Col_Type"].Value != null)
                        mvoutype = drowPVCTemp.Cells["Col_Type"].Value.ToString();
                    if (mSaleID != null && mClearedAmount != 0 && mvoutype != "")
                    {
                        if (mvoutype == FixAccounts.VoucherTypeForOpeningBalance)
                            _BankPayment.UpdateOpeningBalanceReducePrevious(_BankPayment.preAccountID, _BankPayment.OpeningClearedInVoucher + _BankPayment.DiscountInOpeningBalance);
                        else if (mvoutype == FixAccounts.VoucherTypeForCreditPurchase)
                            retValue = _BankPayment.RevertPreviousPurchaseBalanceBill(mSaleID, mClearedAmount+mdiscamount);
                        else if (mvoutype == FixAccounts.VoucherTypeForStatementPurchase)
                            retValue = _BankPayment.RevertPreviousPurchaseBalanceStatement(mSaleID, mClearedAmount+mdiscamount);
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

     
        #endregion

        #region Other Private Methods

        private bool DeletePreviousEntry()
        {
            bool returnVal = false;
            try
            {
                returnVal = _BankPayment.DeletePreviousRecords();
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
                _BankPayment.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow drow in mpMSCSale.Rows)
                    {
                        if (drow.Cells["Col_GetClearedAmount"].Value != null && drow.Cells["Col_GetClearedAmount"].Value.ToString() != string.Empty &&
                           Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value) > 0)
                        {
                            _BankPayment.SerialNumber += 1;
                            _BankPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            if (drow.Cells["Col_MasterID"].Value != null)
                                _BankPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _BankPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            if (_BankPayment.DSaleId == "OPB")
                            {
                                _BankPayment.DSaleId = "0";
                            }
                            else
                            {
                                _BankPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            }
                            _BankPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                            _BankPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                            if (drow.Cells["Col_Number"].Value != null && drow.Cells["Col_Number"].Value.ToString() != "")
                                _BankPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString()); ;                      
                            _BankPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _BankPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _BankPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _BankPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value.ToString());
                            _BankPayment.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                            returnVal = _BankPayment.AddParticularsDetails();
                            if (returnVal)
                            {
                                if (_BankPayment.DVoucherType == FixAccounts.VoucherTypeForOpeningBalance)                               
                                        returnVal = _BankPayment.UpdateOpeningBalanceAddNew();                                
                                else if (_BankPayment.DVoucherType == FixAccounts.VoucherTypeForStatementPurchase)
                                    returnVal = _BankPayment.UpdatePurchaseStatement();
                                else if (_BankPayment.DVoucherType == FixAccounts.VoucherTypeForCreditPurchase)
                                    returnVal = _BankPayment.UpdatePurchaseBill();
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
                _BankPayment.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow drow in mpPVCTemp.Rows)
                    {
                        if (drow.Cells["Col_ID"].Value != null && drow.Cells["Col_ID"].Value.ToString() != "")
                        {
                            _BankPayment.SerialNumber += 1;
                            _BankPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _BankPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _BankPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _BankPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                            _BankPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                            _BankPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString());
                            _BankPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _BankPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _BankPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _BankPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_ClearedAmount"].Value.ToString());
                            returnVal = _BankPayment.AddParticularsDetailsChanged();
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
                _BankPayment.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow drow in mpMSVC.Rows)
                    {
                        if (drow.Cells["Col_ID"].Value != null && drow.Cells["Col_ID"].Value.ToString() != "")
                        {
                            _BankPayment.SerialNumber += 1;
                            _BankPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _BankPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _BankPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _BankPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                            _BankPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                            _BankPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString());
                            _BankPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _BankPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _BankPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _BankPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_ClearedAmount"].Value.ToString());
                            returnVal = _BankPayment.AddParticularsDetailsDeleted();
                            //if (returnVal)
                            //{
                            //    if (_BankPayment.DVoucherType == FixAccounts.VoucherTypeForStatementPurchase)
                            //        returnVal = _BankPayment.UpdatePurchaseStatement();
                            //    else
                            //        returnVal = _BankPayment.UpdatePurchaseBill();
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
                mpMSVC.Rows.Clear();
                mpMSCSale.Rows.Clear();
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtNoOfRows.Text = "0";
                txtTotalBalance.Text = "0.00";
                txtVouType.Text = FixAccounts.VoucherTypeForBankPayment;
              
                datePickerChequeDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
            
                txtAmountReceived.Text = "0.00";
                txtAmtNotAdjusted.Text = "0.00";               
                txtChequeNumber.Text = ""; 
                mpMSVC.Rows.Clear();
                mpMSCSale.Rows.Clear();                           
                mcbCreditor.SelectedID = "";
               // mcbBankAccount.SelectedID = "";
                if (_Mode == OperationMode.Add)
                {
                    mcbCreditor.Enabled = true;
                    mcbBankAccount.Enabled = true;   
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
                    mcbBankAccount.Enabled = false;
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
        private void FillPartyCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[8] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2", "AccOpeningCredit", "AccClearedAmount", "MobileNumberForSMS" };
                mcbCreditor.ColumnWidth = new string[8] { "0", "20", "200", "200", "150", "0", "0", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetDebtorCreditorList();
                mcbCreditor.FillData(dtable);
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
        public void GetDefaultBank()
        {
            DefaultBankID = General.GetDefaultBank();
            if (DefaultBankID != null)
            {
                mcbBankAccount.SelectedID = DefaultBankID;
                _BankPayment.CBBankAccountID = mcbBankAccount.SelectedID;
            }

        }
        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                if (mcbCreditor.SeletedItem != null)
                { 
                        txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                        txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                        if (mcbCreditor.SeletedItem.ItemData[5] != null)
                            _BankPayment.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
                        if (mcbCreditor.SeletedItem.ItemData[6] != null)
                            _BankPayment.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[6].ToString());
                         _BankPayment.CBAccountID = mcbCreditor.SelectedID;
                    //if (_Mode != OperationMode.ReportView)
                    //    FillmpMSVCGrid("");
                    //    if (_BankPayment.ModifyEdit != "Y" || (_BankPayment.ModifyEdit == "Y" && _BankPayment.ActualAccountID != _BankPayment.CBAccountID))
                    //        FillmpPVC1GridSale();
                    
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
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

     private void GetData()   //Amar
        {
            _BankPayment.CBFromDate = datePickerFromDate.Value.Date.ToString("yyyyMMdd");
            _BankPayment.CBToDate = datePickerToDate.Value.Date.ToString("yyyyMMdd");
            if (mcbCreditor.SeletedItem != null)
            {
                if (mcbCreditor.SeletedItem.ItemData[3] != null)
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                if (mcbCreditor.SeletedItem.ItemData[4] != null)
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                if (mcbCreditor.SeletedItem.ItemData[5] != null && mcbCreditor.SeletedItem.ItemData[5].ToString() != string.Empty)
                    _BankPayment.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
                if (mcbCreditor.SeletedItem.ItemData[6] != null && mcbCreditor.SeletedItem.ItemData[6].ToString() != string.Empty)
                    _BankPayment.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[6].ToString());
                    _BankPayment.CBAccountID = mcbCreditor.SelectedID;
                FillmpMSVCGrid("");
                FillOpeningBalanceGrid("");
                mpMSCSale.ClearSelection();
            }
        }
        //private void GetData()
        //{
        //    _BankPayment.CBFromDate = datePickerFromDate.Value.Date.ToString("yyyyMMdd");
        //    _BankPayment.CBToDate = datePickerToDate.Value.Date.ToString("yyyyMMdd");
        //    if (mcbCreditor.SeletedItem != null)
        //    {
        //        if (mcbCreditor.SeletedItem.ItemData[3] != null)
        //            txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
        //        if (mcbCreditor.SeletedItem.ItemData[4] != null && mcbCreditor.SeletedItem.ItemData[4] != string.Empty)
        //            txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
        //        if (mcbCreditor.SeletedItem.ItemData[5] != null && mcbCreditor.SeletedItem.ItemData[5] != string.Empty)
        //            _BankPayment.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
        //        if (mcbCreditor.SeletedItem.ItemData[6] != null && mcbCreditor.SeletedItem.ItemData[6] != string.Empty)
        //            _BankPayment.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[6].ToString());
        //        _BankPayment.CBAccountID = mcbCreditor.SelectedID;
        //        if (_Mode != OperationMode.ReportView)
        //            FillmpMSVCGrid("");
        //        FillOpeningBalanceGrid("");
        //        mpMSCSale.ClearSelection();
        //        //if (_BankPayment.ModifyEdit != "Y" || (_BankPayment.ModifyEdit == "Y" && _BankPayment.ActualAccountID != _BankPayment.CBAccountID))
        //        //    FillmpPVC1GridSale();
        //       // txtAmountReceived.Focus();
        //    }
        //}

        private bool FillmpPVC1GridSale()
        {
            bool retValue = false;
            try
            {
                ConstructSaleColumns();
                FormatSaleGrid();
                DataTable dtable = new DataTable();
                if (_BankPayment.CBAccountID != null && _BankPayment.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_BankPayment.ActualAccountID != _BankPayment.CBAccountID && _BankPayment.ModifyEdit == "Y"))
                    {
                        dtable = _BankPayment.ReadBillDetailsByID();
                        _statementdtable = _BankPayment.ReadStatementDetailsByID();
                    }
                    else
                        dtable = _BankPayment.ReadBillDetailsByBKPID();
                }
                //mpMSCSale.DataSourceMain = dtable;
                //mpMSCSale.Bind();
                BindmpMSVCGrid(dtable, _statementdtable);
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
            mpMSVC.DateColumnNames.Add("Col_BillFromDate");
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
                dtable = _BankPayment.ReadBillDetailsByIDforModify();
                _statementdtable = _BankPayment.ReadStatementDetailsByIDforModify();
                mpMSCSale.Rows.Clear();               
                BindmpMSCSaleGrid(dtable, _statementdtable);
                retValue = RevertPreviousEntry();
                IfOpeningAdded = true;
                _saledtable = _BankPayment.ReadBillDetailsByID();
                _statementdtable = _BankPayment.ReadStatementDetailsByID();
                BindmpMSCSaleGrid(_saledtable, _statementdtable);
                NoofRows();
                FillOpeningBalanceGrid("");
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
        //    foreach (DataGridViewRow tempdr in mpPVCTemp.Rows)
        //    {
        //        if (tempdr.Cells["Col_MasterSaleID"].Value != null)
        //            mtempvouid = tempdr.Cells["Col_MasterSaleID"].Value.ToString();
        //        if (tempdr.Cells["Col_DiscountAmount"].Value != null)
        //            mtempdiscamt = Convert.ToDouble(tempdr.Cells["Col_DiscountAmount"].Value.ToString());
        //        if (mtempvouid != FixAccounts.VoucherTypeForOpeningBalance && mtempdiscamt > 0)
        //        {
        //            foreach (DataGridViewRow dr in mpMSCSale.Rows)
        //            {
        //                if (dr.Cells["Col_ID"].Value != null)
        //                    msalevouid = dr.Cells["Col_ID"].Value.ToString();
        //                if (mtempvouid == msalevouid)
        //                {
        //                    if (dr.Cells["Col_BalanceAmount"].Value != null)
        //                        balamount = Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
        //                    balamount += mtempdiscamt;
        //                    dr.Cells["Col_BalanceAmount"].Value = balamount.ToString("#0.00");
        //                }
        //            }
        //        }
        //    }
        //}
        private void BindmpMSCSaleGrid(DataTable saletable, DataTable statementtable)
        {

            bool ifIDFound = false;
            int _rowIndex = 0;
            string iD = "";
            try
            {
                if (IfOpeningAdded != true && (_Mode != OperationMode.Edit) && (_Mode == OperationMode.Add || (((_BankPayment.preAccountID != null && _BankPayment.preAccountID != "") || _BankPayment.preAccountID != "") && _BankPayment.CBAccountID != _BankPayment.preAccountID) || _BankPayment.ModifyEdit == "Y"))
                {
                    if ((_BankPayment.OpeningClearedInVoucher > 0 || (_BankPayment.OpeningBalance - _BankPayment.OpeningCleared + _BankPayment.OpeningClearedInVoucher + _BankPayment.DiscountInOpeningBalance) > 0) && ((_BankPayment.preAccountID == null || _BankPayment.preAccountID == "") || _BankPayment.CBAccountID == _BankPayment.preAccountID))
                    {
                        _rowIndex = mpMSCSale.Rows.Add();
                        DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "OPB";
                        currentdr.Cells["Col_Series"].Value = "";
                        currentdr.Cells["Col_Type"].Value = "OPB";
                        currentdr.Cells["Col_Number"].Value = "1";
                        currentdr.Cells["Col_Check"].Value = " ";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = General.ShopDetail.Shopsy.ToString();
                        currentdr.Cells["Col_BillNumber"].Value = "1";
                        currentdr.Cells["Col_BillAmount"].Value = _BankPayment.OpeningBalance;
                        if ((_BankPayment.preAccountID == null || _BankPayment.preAccountID == "") || ( _BankPayment.CBAccountID == _BankPayment.preAccountID))
                            currentdr.Cells["Col_BalanceAmount"].Value = (_BankPayment.OpeningBalance - _BankPayment.OpeningCleared + _BankPayment.OpeningClearedInVoucher + _BankPayment.DiscountInOpeningBalance).ToString();
                        else
                            currentdr.Cells["Col_BalanceAmount"].Value = _BankPayment.OpeningBalance;
                        currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                        currentdr.Cells["Col_ClearedAmount"].Value = "0.00";
                        currentdr.Cells["Col_MasterID"].Value = "";


                        mpMSCSale.Sort(mpMSCSale.ColumnsMain["Col_Type"], ListSortDirection.Ascending);
                        IfOpeningAdded = true;

                    }
                }
                else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
                {
                    if (_BankPayment.OpeningClearedInVoucher >= 0)
                    {
                        if ((_BankPayment.OpeningBalance > 0) && (_BankPayment.OpeningClearedInVoucher > 0 || (_BankPayment.OpeningBalance - _BankPayment.OpeningCleared + _BankPayment.OpeningClearedInVoucher + _BankPayment.DiscountInOpeningBalance) > 0) && (_BankPayment.CBAccountID == _BankPayment.preAccountID))
                     
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = "OPB";
                            currentdr.Cells["Col_Series"].Value = General.ShopDetail.ShopVoucherSeries;
                            currentdr.Cells["Col_Type"].Value = "OPB";
                            currentdr.Cells["Col_Number"].Value = "";
                            currentdr.Cells["Col_BillSubType"].Value = "";
                            currentdr.Cells["Col_Check"].Value = " ";
                            currentdr.Cells["Col_BillFromDate"].Value = "";
                            currentdr.Cells["Col_BillAmount"].Value = _BankPayment.OpeningBalance.ToString();
                           // currentdr.Cells["Col_PatientShortName"].Value = "";
                            if (_BankPayment.CBAccountID == _BankPayment.preAccountID)
                                currentdr.Cells["Col_BalanceAmount"].Value = (_BankPayment.OpeningBalance - _BankPayment.OpeningCleared + _BankPayment.OpeningClearedInVoucher).ToString();
                            else
                                currentdr.Cells["Col_BalanceAmount"].Value = _BankPayment.OpeningBalance;
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
                            currentdr.Cells["Col_Check"].Value = " ";
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
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
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            currentdr.Cells["Col_Check"].Value = " ";
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
            mpMSVC.Rows.Clear();          
            int _rowIndex = 0;
            IfOpeningAdded = false;
            try
            {
                if (_Mode == OperationMode.Add || (_BankPayment.CBAccountID != _BankPayment.preAccountID))
                {
                    if ((_BankPayment.OpeningBalance - _BankPayment.OpeningCleared) > 0)
                    {
                        _rowIndex =  mpMSVC.Rows.Add();
                        DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "OPB";
                        currentdr.Cells["Col_Series"].Value = "";
                        currentdr.Cells["Col_Type"].Value = "OPB";
                        currentdr.Cells["Col_Number"].Value = "1";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = General.ShopDetail.Shopsy.ToString();
                        currentdr.Cells["Col_BillNumber"].Value = "1";
                        currentdr.Cells["Col_BillAmount"].Value = _BankPayment.OpeningBalance;
                        currentdr.Cells["Col_BalanceAmount"].Value = _BankPayment.OpeningBalance - _BankPayment.OpeningCleared;
                        currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                        currentdr.Cells["Col_ClearedAmount"].Value = "0.00";
                        currentdr.Cells["Col_MasterID"].Value = "";
                        IfOpeningAdded = true;

                    }
                }
                if (saletable != null && saletable.Rows.Count > 0)
                {

                    foreach (DataRow dr in saletable.Rows)
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
                        currentdr.Cells["Col_GetClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        // currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                    }

                }

                
                if (statementtable != null && statementtable.Rows.Count > 0)
                {

                    foreach (DataRow dr in statementtable.Rows)
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
                            currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString(); 
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            // currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        

                    }


                }
             //  mpMSVC.Sort(mpMSVC.ColumnsMain["Col_Type"], ListSortDirection.Ascending);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        //private void BindTempGrid(DataTable saletable, DataTable statementtable)
        //{

        //    int _rowIndex = 0;
        //    string iD = "";

        //    if (saletable != null && saletable.Rows.Count > 0)
        //    {
        //        try
        //        {
        //            foreach (DataRow dr in saletable.Rows)
        //            {
        //                if (dr["ID"] != DBNull.Value)
        //                    iD = dr["ID"].ToString();
        //                //ifIDFound = SearchforIDInSaleGrid(iD);
        //                //if (ifIDFound == false)
        //                //{
        //                _rowIndex = mpPVCTemp.Rows.Add();
        //                DataGridViewRow currentdr = mpPVCTemp.Rows[_rowIndex];
        //                currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
        //                currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
        //                currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
        //                currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
        //                currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
        //                //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
        //                currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
        //                currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
        //                currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
        //                currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
        //                currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
        //                currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
        //                currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
        //                currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
        //                //}

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.WriteError(ex.ToString());


        //        }

        //    }
        //    if (statementtable != null && statementtable.Rows.Count > 0)
        //    {

        //        foreach (DataRow dr in statementtable.Rows)
        //        {
        //            try
        //            {
        //                if (dr["ID"] != DBNull.Value)
        //                    iD = dr["ID"].ToString();
        //                _rowIndex = mpPVCTemp.Rows.Add();
        //                DataGridViewRow currentdr = mpPVCTemp.Rows[_rowIndex];
        //                currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
        //                currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
        //                currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
        //                currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
        //                currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
        //                //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
        //                currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
        //                currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
        //                currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
        //                currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
        //                currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
        //                currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
        //                currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
        //                currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
        //                //}
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.WriteError(ex.ToString());

        //            }
        //        }

        //    }

        //}
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

                DataTable dtable = new DataTable();
                if (_BankPayment.CBAccountID != null && _BankPayment.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_BankPayment.ActualAccountID != _BankPayment.CBAccountID && _BankPayment.ModifyEdit == "Y"))
                    {
                        dtable = _BankPayment.ReadBillDetailsByID();
                        _statementdtable = _BankPayment.ReadStatementDetailsByID();

                        mpMSCSale.Rows.Clear();
                        mpMSVC.Rows.Clear();
                        BindmpMSVCGrid(dtable, _statementdtable);
                        IfOpeningAdded = false;
                        BindmpMSCSaleGrid(dtable, _statementdtable);
                        NoofRows();
                        mpMSCSale.Sort(mpMSCSale.ColumnsMain["Col_BillFromDate"], ListSortDirection.Ascending);
                        mpMSVC.Sort(mpMSVC.ColumnsMain["Col_BillFromDate"], ListSortDirection.Ascending);
                        //mpMSVC.Refresh();
                        //mpMSCSale.Refresh();


                    }
                    else
                    {
                        mpMSVC.Rows.Clear();
                        _statementdtable = null;
                        if (vmode == "C")
                            dtable = _BankPayment.ReadBillDetailsByBKPIDForChanged();
                        else if (vmode == "D")
                            dtable = _BankPayment.ReadBillDetailsByBKPIDForDeleted();
                        else
                            dtable = _BankPayment.ReadBillDetailsByBKPID();



                        BindmpMSVCGrid(dtable, _statementdtable);
                        NoofRowsFormpMSVCGrid();
                        foreach (DataRow dr in dtable.Rows)
                        {
                            if (dr["VoucherType"] != DBNull.Value && dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForOpeningBalance)
                            {
                                _BankPayment.OpeningClearedInVoucher = Convert.ToDouble(dr["AmountClear"].ToString());
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
        private bool FillmpPVCTempGrid()
        {           
            bool retValue = false;
            try
            {
                ConstructTempColumns();

                //mpPVCTemp.DoubleColumnNames.Add("Col_BillAmount");
                //mpPVCTemp.DoubleColumnNames.Add("Col_BalanceAmount");
                //mpPVCTemp.DoubleColumnNames.Add("Col_ClearedAmount");
                //        mpPVCTemp.DoubleColumnNames.Add("Col_AmountDiscount");
                mpMSVC.DoubleColumnNames.Add("Col_GetClearedAmount");


                DataTable dtable = new DataTable();
                dtable = _BankPayment.ReadBillDetailsByBKPID();
             //   mpPVCTemp.DataSource = dtable;
               // mpPVCTemp.Bind();
                _statementdtable = _BankPayment.ReadStatementDetailsByID();
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
                        if (dr["MasterID"] != DBNull.Value)
                            iD = dr["MasterID"].ToString();
                        //ifIDFound = SearchforIDInSaleGrid(iD);
                        //if (ifIDFound == false)
                        //{
                        _rowIndex = mpPVCTemp.Rows.Add();
                        DataGridViewRow currentdr = mpPVCTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_Series"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_Type"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_Number"].Value = dr["VoucherNumber"].ToString();
                       // currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                     //   currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
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
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
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
            //DataGridViewTextBoxColumn column;

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
                column.HeaderText = "YEAR.";
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
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.Width = 110;
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
            //DataGridViewTextBoxColumn column;

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
                //7
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
                //DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                //columnCheck.Name = "Col_Check";
                //columnCheck.HeaderText = "Check";
                //columnCheck.Width = 50;
                //if (_Mode == OperationMode.Add)
                //    columnCheck.Visible = true;
                //else
                //    columnCheck.Visible = false;
                //mpMSCSale.ColumnsMain.Add(columnCheck);

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
                column.DefaultCellStyle.Format = "n2";
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
            //DataGridViewTextBoxColumn column;

            DataGridViewTextBoxColumn column;

            mpPVCTemp.Columns.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PurchaseID";
                column.HeaderText = "MasterID";
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
                //4
                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_BillSubType";
                //column.DataPropertyName = "VoucherSubType";
                //column.HeaderText = "SubType";
                //column.Width = 45;
                //column.ReadOnly = true;
                //mpPVCTemp.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "FromDate";
                column.Width = 80;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_MasterSaleID";
                //column.DataPropertyName = "MasterSaleID";
                //column.HeaderText = "MasterSaleID";
                //column.Width = 110;
                //column.Visible = false;
                //mpPVCTemp.ColumnsMain.Add(column);

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
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
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

        //private void mpMSCSale_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {

        //        double getclearedamt = 0;
        //        if (e.ColumnIndex == 9) // 
        //        {                    
        //            double mbalanceamount = 0;
        //            double mamtnotadj = 0;
        //            double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);
        //            //done
        //            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString().Trim() != "")
        //                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
        //            _BankPayment.CellOldValueAmount = getclearedamt;
        //            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString().Trim() != "")
        //                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceamount);
        //            if (getclearedamt == 0 && mamtnotadj != 0)
        //            {
        //                double clearedamt = 0;
        //                clearedamt = Math.Min(mamtnotadj, mbalanceamount);
        //                mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
        //            }
        //        }
               
        //    }            
        //    catch (Exception ex) { Log.WriteError(ex.ToString()); }
        //}

        //private void mpMSCSale_OnCellValueChangeCommited(int colIndex)
        //{
        //  //  txtAmountReceived.Enabled = false;
        //    double totalreceived = 0;
        //    double mgetclearedamount = 0;
        //    double mbalamount = 0;   
        //    double mamtnotadj = 0;
        //    double mbillamt = 0;
        //    double clearedamt = 0;
        //    double mdiscountamt = 0;
        //    double.TryParse(txtAmountReceived.Text, out mbillamt);
        //    double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);
        //    //done
        //     double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out mgetclearedamount);
        //     if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null)
        //         double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);
        //    double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalamount);

        //     clearedamt = Math.Min(mamtnotadj, mbalamount);
        //    try
        //    {
        //        if (colIndex == 9)
        //        {
        //            if (mgetclearedamount == 0)
        //            {
        //               _BankPayment.CellOldValueAmount = 0;
                       
        //            }
        //            if (mbalamount < mgetclearedamount)
        //            {                                              
        //                mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
        //                _BankPayment.CellOldValueAmount = clearedamt;
        //            }
        //            else
        //            {
        //               // if (mamtnotadj == 0 && mgetclearedamount > 0)
        //                    //done
        //                if (mamtnotadj == 0)
        //                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankPayment.CellOldValueAmount;

        //                if (clearedamt <= Math.Min(mamtnotadj, mbalamount) || (clearedamt <= _BankPayment.CellOldValueAmount))
        //                {
        //                    _BankPayment.CellOldValueAmount = mgetclearedamount;
        //                    foreach (DataGridViewRow dr in mpMSCSale.Rows)
        //                    {
        //                        double mcleared = 0;
        //                        double mdiscount = 0;
        //                        if (dr.Cells["Col_GetClearedAmount"].Value != null && dr.Cells["Col_GetClearedAmount"].Value.ToString() != "")
        //                            double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
        //                        if (dr.Cells["Col_DiscountAmount"].Value != null)
        //                            double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
        //                        if (mcleared > 0)
        //                            totalreceived += mcleared;
        //                    }


        //                    double d = (mbillamt - totalreceived);
        //                    txtAmtNotAdjusted.Text = d.ToString("#0.00");

        //                    mdiscountamt = mbalamount - mgetclearedamount;
        //                    if (mdiscountamt > 20)
        //                        mdiscountamt = 0;
        //                    if (mgetclearedamount == 0)
        //                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
        //                    else
        //                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt.ToString("#0.00");
        //                //int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
        //                //    if (mpMSCSale.Rows.Count > rowindex+1)
        //                //        mpMSCSale.SetFocus(rowindex+1, 9);

        //                }
        //            }
        //        }
        //        else if (colIndex == 13)
        //        {
        //            double mdiscgiven = 0;
        //            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null)
        //                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
        //            mdiscountamt = Math.Round(mbalamount - mgetclearedamount, 2);
        //            if (mdiscgiven != 0 && mdiscgiven != mdiscountamt)
        //                mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}

        //private void mpMSCSale_OnCellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.ColumnIndex == 9) // ss 14/3/2012
        //            mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankPayment.CellOldValueAmount.ToString("#0.00");
        //    }
        //    catch (Exception ex) { Log.WriteError(ex.ToString()); }           
        //}

        private void mpMSCSale_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double getclearedamt = 0;
                if (e.ColumnIndex == 12 && ( _BankPayment.PaymentSubType == 3 || _BankPayment.PaymentSubType == 0))// old 11
                {
                    double mbalanceamount = 0;
                    double mamtnotadj = 0;
                    double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString().Trim() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
                    _BankPayment.CellOldValueAmount = getclearedamt;
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
               
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

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
                    if (_BankPayment.PaymentSubType == 3 || _BankPayment.PaymentSubType == 0)// old 11
                    {
                        if (getclearedamt == 0)
                        {
                            _BankPayment.CellOldValueAmount = 0;
                        }

                        if (mbalanceAmount < getclearedamt)
                        {
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
                            _BankPayment.CellOldValueAmount = clearedamt;
                        }
                        else
                        {
                            if (mamtnotadj == 0)
                                mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankPayment.CellOldValueAmount;


                            //if (clearedamt <= Math.Min(mamtnotadj, mbalanceAmount) || (clearedamt <= _CashReceipt.CellOldValueAmount))
                            //{

                            _BankPayment.CellOldValueAmount = getclearedamt;
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
                                _BankPayment.CellOldValueAmount = 0;
                                _BankPayment.CellOldValueDiscount = 0;
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
                                // totalreceived += mcleared + mdiscount;
                            }

                            mamtnotadj = (mbillamt - totalreceived);
                            txtAmtNotAdjusted.Text = mamtnotadj.ToString("#0.00");
                            if (mamtnotadj >= 0 && getclearedamt > 0)
                            {
                                mdiscountamt = mbalanceAmount - getclearedamt;
                                if (General.CurrentSetting.MsetCashBankShowDiscount != "Y")   //Amar
                                {
                                    if (mdiscountamt > 10)
                                        mdiscountamt = 0;
                                }
                            }
                            else
                                mdiscountamt = 0;
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
                        //totalreceived += (mcleared+mdiscount);
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

                if (e.ColumnIndex == 12 && (_BankPayment.PaymentSubType == 3 || _BankPayment.PaymentSubType == 0))// old 11
                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankPayment.CellOldValueAmount.ToString("#0.00");

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }
        private void mpMSCSale_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            ClearGrid();

            mcbCreditor.Focus();
        }
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_Mode == OperationMode.Add)
                {
                    if (_BankPayment.PaymentSubType == 2)
                        mpMSCSale.SetFocus(0, 7); // old 11
                    else if (_BankPayment.PaymentSubType == 3 || (_BankPayment.PaymentSubType == 0))
                        mpMSCSale.SetFocus(0, 12);
                    else
                        txtNarration.Focus();
                }
                else
                    mpMSCSale.SetFocus(0, 12);
            }
            else if (e.KeyCode == Keys.Up)
                txtChequeNumber.Focus();

        }
       

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (datePickerFromDate.Visible == true)
                datePickerFromDate.Focus();
            else
                txtAmountReceived.Focus();
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
                txtChequeNumber.Focus();
                mpMSCSale.ClearSelection();
                _BankPayment.PaymentSubType = 0;

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
                        _BankPayment.PaymentSubType = 1;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;
                        FillGridForDates();
                    }
                    else if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == 0)
                    {
                        _BankPayment.PaymentSubType = 2;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = false;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;
                    }
                    else
                    {
                        _BankPayment.PaymentSubType = 3;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = false;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = false;
                    }
                }
                txtChequeNumber.Focus();

            }
            else if (e.KeyCode == Keys.Up)
                mcbCreditor.Focus();
        }
        public void ClearGrid()
        {
            try
            {
                txtAmtNotAdjusted.Text = txtAmountReceived.Text;
                _BankPayment.CellOldValueAmount = 0;
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
            if (_BankPayment.PaymentSubType == 1)
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
        private void txtChequeNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                datePickerChequeDate.Focus();
        }
        private void txtChequeNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void datePickerChequeDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNarration.Focus();
        }

        private void txtAmountReceived_TextChanged(object sender, EventArgs e)
        {
            if (mpMSCSale.ColumnsMain["Col_Check"].ReadOnly == true)
            ClearGrid();
        }       

        private void btnModify_Click(object sender, EventArgs e)
        {
            ModifyEdit();
        }

        private void mcbBankAccount_EnterKeyPressed(object sender, EventArgs e)
        {
           
            if (mcbBankAccount.SelectedID != null)
                _BankPayment.CBBankAccountID = mcbBankAccount.SelectedID;
            mcbCreditor.Focus();
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion           

        private void UclBankPayment_Load(object sender, EventArgs e)
        {
            datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
            datePickerFromDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
            datePickerToDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
            dgClearOpeningBalance.Visible = false;
            GetDefaultBank();
        }
    
        private void btnClearOpeningBalance_Click(object sender, EventArgs e)
        {
            btnClearOpeningBalanceClick();
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
                    dgClearOpeningBalance.SetFocus(0, 11); // old 11
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
            try
            {
                double getclearedamt = 0;
                if (e.ColumnIndex == 11) // old 11
                {
                    double mbalanceamount = 0;
                    double mamtnotadj = 0;
                    // txtOpeningBalanceAmount.Text = mpMSCSale.MainDataGridCurrentRow.Cells[12].ToString();
                    double.TryParse(txtOpeningBalanceAmount.Text, out mamtnotadj);
                    if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString().Trim() != "")
                        double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
                    _BankPayment.CellOpeningBalanceOldValueAmount = getclearedamt;
                    if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString().Trim() != "")
                        double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceamount);
                    if (getclearedamt == 0 && mamtnotadj != 0)
                    {
                        double clearedamt = 0;
                        clearedamt = Math.Min(mamtnotadj, mbalanceamount);
                        dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;

                    }
                  //  dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].ReadOnly = false;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgClearOpeningBalance_OnCellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == 11) // old 11
                    dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankPayment.CellOpeningBalanceOldValueAmount.ToString("#0.00");

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgClearOpeningBalance_OnCellValueChangeCommited(int colIndex)
        {
            txtAmountReceived.Enabled = false;
            txtOpeningBalanceAmount.Enabled = false;
            double totalreceived = 0;
            double getclearedamt = 0;
            double mbalanceAmount = 0;
            double mamtnotadj = 0;
            double clearedamt = 0;
            double mbillamt = 0;
            double mdiscountamt = 0;
            mbillamt = Convert.ToDouble(mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value.ToString());
            double.TryParse(txtOpeningBalanceAmount.Text, out mamtnotadj);

            if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null)
                double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
            if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);
            double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceAmount);

            clearedamt = Math.Min(mamtnotadj, mbalanceAmount);
            try
            {

                if (colIndex == 11) // old 11
                {

                    if (getclearedamt == 0)
                    {
                        _BankPayment.CellOpeningBalanceOldValueAmount = 0;
                    }

                    if (mbalanceAmount < getclearedamt)
                    {
                        dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
                        _BankPayment.CellOpeningBalanceOldValueAmount = clearedamt;
                    }
                    else
                    {
                        if (mamtnotadj == 0)
                            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankPayment.CellOpeningBalanceOldValueAmount;


                        //if (clearedamt <= Math.Min(mamtnotadj, mbalanceAmount) || (clearedamt <= _CashReceipt.CellOldValueAmount))
                        //{

                        _BankPayment.CellOpeningBalanceOldValueAmount = getclearedamt;
                        foreach (DataGridViewRow dr in dgClearOpeningBalance.Rows)
                        {
                            double mcleared = 0;
                            double mdiscount = 0;
                            if (dr.Cells["Col_GetClearedAmount"].Value != null)
                                double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                            if (dr.Index == dgClearOpeningBalance.MainDataGridCurrentRow.Index)
                                mcleared = getclearedamt;
                            if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
                            if (mcleared > 0)
                                totalreceived += mcleared;
                        }

                        mamtnotadj = (mbillamt - totalreceived);

                        if (mamtnotadj < 0)
                        {
                            mamtnotadj = 0;
                            _BankPayment.CellOpeningBalanceOldValueAmount = 0;
                            _BankPayment.CellOpeningBalanceOldValueDiscount = 0;
                            getclearedamt = 0;
                        }
                        txtOpeningBalanceAmount.Text = mamtnotadj.ToString("#0.00");
                        if (mamtnotadj >= 0 && getclearedamt > 0)
                        {
                            mdiscountamt = mbalanceAmount - getclearedamt;
                            if (mdiscountamt > 10)
                                mdiscountamt = 0;
                        }
                        else
                            mdiscountamt = 0;


                        if (getclearedamt == 0)
                            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                        else
                            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt.ToString("#0.00");
                        dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
                        //int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
                        //if (mpMSCSale.Rows.Count > rowindex + 1)
                        //    mpMSCSale.SetFocus(rowindex + 1, 9);
                        //}
                    }
                }
                else if (colIndex == 12)
                {
                    double mdiscgiven = 0;
                    if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                        double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
                    mdiscountamt = Math.Round(mbalanceAmount - getclearedamt, 2);
                    if (mdiscgiven != 0)
                    {
                        if (mdiscgiven != mdiscountamt || getclearedamt == 0)
                            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                    }
                    //if (mpMSCSale.MainDataGridCurrentRow.Index < mpMSCSale.Rows.Count-1)

                    //    mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 12);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                if (_BankPayment.CBAccountID != null && _BankPayment.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_BankPayment.ActualAccountID != _BankPayment.CBAccountID && _BankPayment.ModifyEdit == "Y"))
                    {
                        _saledtable = _BankPayment.ReadOldBillDetailsByID();
                        _statementdtable = _BankPayment.ReadOldStatementDetailsByID();
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
                            _saledtable = _BankPayment.ReadOldBillDetailsByCSPIDForChanged();
                        else if (vmode == "D")
                            _saledtable = _BankPayment.ReadOldBillDetailsByCSPIDForDeleted();
                        else
                        {
                            /////////  _saledtable = _CashReceipt.ReadOldBillDetailsByCSRID();
                            //  _statementdtable = _CashReceipt.ReadStatementDetailsByCSRID();

                            _saledtable = _BankPayment.ReadOldBillDetailsByID();
                            _statementdtable = _BankPayment.ReadOldStatementDetailsByID();
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
            dgClearOpeningBalanceTemp.Rows.Clear();
            int _rowIndex = 0;
            IfOpeningAdded = false;
            if (_Mode == OperationMode.Add)
            {
                if ((_BankPayment.OpeningBalance - _BankPayment.OpeningCleared) > 0)
                {
                    _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                    DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = "OPB";
                    currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                    currentdr.Cells["Col_BillType"].Value = "OPB";
                    currentdr.Cells["Col_BillNumber"].Value = "";
                    currentdr.Cells["Col_BillSubType"].Value = "";
                    currentdr.Cells["Col_BillFromDate"].Value = "";
                    currentdr.Cells["Col_BillAmount"].Value = _BankPayment.OpeningBalance.ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = "";
                    currentdr.Cells["Col_BalanceAmount"].Value = (_BankPayment.OpeningBalance - _BankPayment.OpeningCleared).ToString();
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
                    currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
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
                    currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
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
                if (IfOpeningAdded != true && (_Mode != OperationMode.Edit) && (_Mode == OperationMode.Add || ((_BankPayment.preAccountID != null && _BankPayment.preAccountID != "") && _BankPayment.CBAccountID != _BankPayment.preAccountID) || _BankPayment.ModifyEdit == "Y"))
                {
                    if ((_BankPayment.OpeningClearedInVoucher > 0 || (_BankPayment.OpeningBalance - _BankPayment.OpeningCleared + _BankPayment.OpeningClearedInVoucher + _BankPayment.DiscountInOpeningBalance) > 0) && (_BankPayment.preAccountID == null || _BankPayment.CBAccountID == _BankPayment.preAccountID))
                    {
                        _rowIndex = dgClearOpeningBalance.Rows.Add();
                        DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "OPB";
                        currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                        currentdr.Cells["Col_BillType"].Value = "OPB";
                        currentdr.Cells["Col_BillNumber"].Value = "";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = "";
                        currentdr.Cells["Col_BillAmount"].Value = _BankPayment.OpeningBalance.ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = "";
                        if ((_BankPayment.preAccountID == null || _BankPayment.preAccountID == "") || ((_BankPayment.preAccountID != null || _BankPayment.preAccountID != "") && _BankPayment.CBAccountID == _BankPayment.preAccountID))
                            currentdr.Cells["Col_BalanceAmount"].Value = (_BankPayment.OpeningBalance - _BankPayment.OpeningCleared + _BankPayment.OpeningClearedInVoucher + _BankPayment.DiscountInOpeningBalance).ToString();
                        else
                            currentdr.Cells["Col_BalanceAmount"].Value = _BankPayment.OpeningBalance;
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                        IfOpeningAdded = true;
                    }
                }
                else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
                {
                    if (_BankPayment.OpeningClearedInVoucher >= 0)
                    {
                        if (_BankPayment.OpeningBalance > 0 && (_BankPayment.OpeningBalance - _BankPayment.OpeningCleared + _BankPayment.OpeningClearedInVoucher + _BankPayment.DiscountInOpeningBalance) > 0)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = "OPB";
                            currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                            currentdr.Cells["Col_BillType"].Value = "OPB";
                            currentdr.Cells["Col_BillNumber"].Value = "";
                            currentdr.Cells["Col_BillSubType"].Value = "";
                            currentdr.Cells["Col_BillFromDate"].Value = "";
                            currentdr.Cells["Col_BillAmount"].Value = _BankPayment.OpeningBalance.ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = "";
                            currentdr.Cells["Col_BalanceAmount"].Value = (_BankPayment.OpeningBalance - _BankPayment.OpeningCleared + _BankPayment.OpeningClearedInVoucher + _BankPayment.DiscountInOpeningBalance).ToString();
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
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
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
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
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
            _BankPayment.SerialNumber = 0;
            try
            {
                foreach (DataGridViewRow drow in dgClearOpeningBalance.Rows)
                {
                    if (drow.Cells["Col_GetClearedAmount"].Value != null && drow.Cells["Col_GetClearedAmount"].Value.ToString() != "" &&
                       Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value) > 0)
                    {
                        _BankPayment.SerialNumber += 1;
                        //   _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        if (drow.Cells["Col_MasterID"].Value != null)
                            _BankPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                        _BankPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                        _BankPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                        _BankPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                        if (drow.Cells["Col_Number"].Value.ToString() != string.Empty)
                            _BankPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString());
                        if (drow.Cells["Col_BillSubType"].Value.ToString() != string.Empty)
                            _BankPayment.DSubType = drow.Cells["Col_BillSubType"].Value.ToString();
                        if (drow.Cells["Col_BillFromDate"].Value.ToString() != string.Empty)
                            _BankPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                        _BankPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                        _BankPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                        _BankPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value.ToString());
                        _BankPayment.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                        //  returnVal = _CashReceipt.AddParticularsDetailsOldSale();                        

                        if (_BankPayment.DVoucherType == FixAccounts.VoucherTypeForStatementPurchase)
                            returnVal = _BankPayment.UpdatePurchaseStatementOld();
                        else if (_BankPayment.DVoucherType == FixAccounts.VoucherTypeForCreditPurchase)
                            returnVal = _BankPayment.UpdatePurchaseBillOld();


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
                dtable = _BankPayment.ReadBillDetailsByCSPIDFromtblOld();
               
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
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
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
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
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

     

        //private void btnOKSelectedTotal_Click(object sender, EventArgs e)
        //{
        //    BtnOKSelectedTotalClick();
        //}

        //private void BtnOKSelectedTotalClick()
        //{
        //    //if (colIndex == 7)
        //    //{
        //    double mselectedtotal = 0;
        //    double mamt = 0;
        //    bool ch = false;
        //    {
        //        foreach (DataGridViewRow dr in mpMSCSale.Rows)
        //        {
        //            mamt = 0;
        //            ch = false;
        //            if (dr.Cells["Col_Check"].Value != null)
        //                ch = Convert.ToBoolean(dr.Cells["Col_Check"].Value.ToString());
        //            if (ch == true)
        //            {
        //                mamt = Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
        //                dr.Cells["Col_GetClearedAmount"].Value = mamt.ToString("#0.00");
        //            }
        //            mselectedtotal += mamt;
        //        }
        //        mpMSCSale.Refresh();              
        //        txtAmountReceived.Text = mselectedtotal.ToString("#0.00");
        //        txtAmtNotAdjusted.Text = "0.00";
        //        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
        //        txtAmountReceived.Focus();
        //    }
        //    //}
        //}



        #region tooltip

        #endregion

       
       

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
           // mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
            txtAmountReceived.Text = totamount.ToString("#0.00");
          //  mpMSCSale.Refresh();     
            mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 7);
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

        
    }
}
