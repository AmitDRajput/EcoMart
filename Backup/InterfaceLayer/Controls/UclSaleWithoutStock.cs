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
using System.IO;
using PharmaSYSRetailPlus.InterfaceLayer.Classes;


namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSaleWithoutStock : BaseControl
    {
        #region Declaration
      
        private SSSale _SSSale;
      //  private BaseControl ViewControl;
       // private Form frmView;
       
        #endregion

        #region Constructor
        public UclSaleWithoutStock()
        {
            try
            {
            InitializeComponent();
       
              _SSSale = new SSSale();
              SearchControl = new UclSaleWithOutStockSearch();
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
                _SSSale.Initialise();
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
                txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
                InitializeScreen();
                headerLabel1.Text = "CASH SALE -> NEW";                
                _SSSale.ReadBillPrintSettings();               
                InitialisempPVC1("");
                AddToolTip();
                FillDoctorCombo();              
                mcbDoctor.SelectedID = "";
                FillPartyCombo();
                InitializeCheckBoxes();              
             
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
                headerLabel1.Text = "CASH SALE -> ADD/REMOVE PRESCRIPTION";
                InitializeCheckBoxes();               
                FillPartyCombo();               
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

        public override bool Exit()
        {
            bool retValue = base.Exit();            
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "CASH SALE -> DELETE";
                InitializeCheckBoxes();
                ClearData();
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
                if ((_SSSale.CrdbAmountBalance != _SSSale.CrdbAmountNet) && _SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditSale)
                    MessageBox.Show("Payment Done Can not Delete", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {

                    if (MessageBox.Show("Are you sure you want to delete Sale Information?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                    
                        LockTable.LockTablesForSale();
                        General.BeginTransaction();
                        _SSSale.ModifiedBy = General.CurrentUser.Id;
                        _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _SSSale.DeleteDetails();
                        if (retValue)
                            retValue = _SSSale.DeletePreviousRecords();                       
                        
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {                           
                            _SSSale.ModifiedBy = General.CurrentUser.Id;
                            _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            _SSSale.AddDetailsInDeleteMaster();                        
                            retValue = true;
                            MessageBox.Show("Successfully Deleted", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            PSDialogResult result = PSMessageBox.Show("Could not Delete...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                            retValue = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            ClearData();
            return retValue;
        }

        public override bool View()
        {
            bool retValue = base.View();
            try
            {
                ClearData();
                headerLabel1.Text = "CASH SALE -> VIEW";
                InitializeCheckBoxes();               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool Print()
        {
            bool retValue = true;
            if (_SSSale.CrdbAmountNet > 0)
            {
                if (General.CurrentSetting.MsetPrintSaleBill == "Y")
                    PrintSaleBillPrePrintedPaper();
                else
                    PrintSaleBillPlainPaper();
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
           
            double maddon = 0;
           
            double mvat5per = 0;
            double mvat12point5per = 0;
            double mamtforzerovat = 0;
            double mbillamount = 0;
         
            double mround = 0;
            double mamountvat5per = 0;
            double mamountvat12point5per = 0;           

            if (txtNetAmount.Text != null && Convert.ToDouble(txtNetAmount.Text.ToString()) > 0)
            {
                try
                {
                    System.Text.StringBuilder _errorMessage;
                    if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                        _SSSale.PatientID = mcbCreditor.SelectedID;
                    _SSSale.AccountID = "";

                    if (_SSSale.IfTypeChange == "Y" && _SSSale.OldVoucherType == FixAccounts.VoucherTypeForCashSale)
                        txtVouType.Text = FixAccounts.VoucherTypeForCreditSale;
                    else
                        _SSSale.IfTypeChange = "N";


                    _SSSale.CrdbVouType = txtVouType.Text.Trim();
                    _SSSale.SaleSubType = FixAccounts.SubTypeForSaleWithoutStock;

                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _SSSale.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                    _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    double.TryParse(txtVatInput5per.Text, out mvat5per);
                    _SSSale.CrdbVat5 = mvat5per;
                    double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                    _SSSale.CrdbVat12point5 = mvat12point5per;
                    double.TryParse(txtAmountforZeroVAT.Text.ToString(), out mamtforzerovat);
                    _SSSale.CrdbAmtForZeroVAT = mamtforzerovat;
                   
                    double.TryParse(txtAmountVAT12Point5Per.Text, out mamountvat12point5per);
                    _SSSale.CrdbAmountVat12point5 = mamountvat12point5per;
                    double.TryParse(txtAmountVAT5Per.Text, out mamountvat5per);
                    _SSSale.CrdbAmountVat5 = mamountvat5per;
                    double.TryParse(txtBillAmount.Text, out mbillamount);
                    _SSSale.CrdbAmountNet = mbillamount;
                    _SSSale.CrdbAmount = _SSSale.CrdbAmountNet;
                    double.TryParse(txtRoundAmount.Text, out mround);
                    _SSSale.CrdbRoundAmount = mround;
                  
                    _SSSale.CrdbAddOn = maddon;                 
                    _SSSale.CrdbNarration = txtNarration.Text.ToString().Trim();
                    _SSSale.CrdbName = mcbCreditor.Text.ToString();
                    _SSSale.ShortName = txtPatient.Text.ToString();
                    if (_SSSale.ShortName.Length > 50)
                        _SSSale.ShortName = _SSSale.ShortName.Substring(0, 50);
                    if (txtAddress1.Text != null && txtAddress1.Text != "")
                        _SSSale.PatientAddress1 = txtAddress1.Text;
                    if (txtAddress2.Text != null && txtAddress2.Text != "")
                        _SSSale.PatientAddress2 = txtAddress2.Text;
                    if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "")
                    {
                        _SSSale.DocID = mcbDoctor.SelectedID.Trim();
                        _SSSale.DoctorName = mcbDoctor.SeletedItem.ItemData[1];
                        if (mcbDoctor.SeletedItem.ItemData[2] != null && mcbDoctor.SeletedItem.ItemData[2].ToString() != string.Empty)
                            _SSSale.DoctorAddress = mcbDoctor.SeletedItem.ItemData[2];
                    }
                    _SSSale.OperatorID = "";
                    _SSSale.OperatorPassword = txtOperator.Text.ToString();

                    if (txtMobileNumber.Text != null && txtMobileNumber.Text != string.Empty)
                        _SSSale.Telephone = txtMobileNumber.Text.ToString();                                 
                    if (_Mode == OperationMode.Edit || _Mode == OperationMode.Fifth)
                        _SSSale.IFEdit = "Y";
                    _SSSale.Validate();

                    if (_SSSale.IsValid)
                    {
                        LockTable.LockTablesForSale();                       
                           
                                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                                {
                                    General.BeginTransaction();

                                    _SSSale.CreatedBy = General.CurrentUser.Id;
                                    _SSSale.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                    _SSSale.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                                    _SSSale.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _SSSale.CrdbAmountBalance = _SSSale.CrdbAmountNet - _SSSale.CrdbAmountClear;

                                    if (_SSSale.CrdbAmountBalance > 0)
                                    {
                                        _SSSale.AccountID = FixAccounts.AccountPendingCashBills;
                                        _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditSale;
                                        txtVouType.Text = _SSSale.CrdbVouType;
                                        _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                                        txtVouchernumber.Text = Convert.ToString(_SSSale.CrdbVouNo);


                                        CashReceipt _cashrcpt = new CashReceipt();

                                        if (_SSSale.CrdbAmountClear > 0)
                                        {
                                            string cashrcptID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                            int CashrcptVouNo = _cashrcpt.GetAndUpdateCSRNumber(General.ShopDetail.ShopVoucherSeries);
                                            retValue = _cashrcpt.AddDetailsFromPatientSale(cashrcptID, FixAccounts.AccountPendingCashBills, _SSSale.ShortName, FixAccounts.VoucherTypeForCashReceipt, CashrcptVouNo, _SSSale.CrdbVouDate, _SSSale.CrdbAmountClear, _SSSale.CreatedBy, _SSSale.CreatedDate, _SSSale.CreatedTime);
                                            if (retValue)
                                            {
                                                _cashrcpt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                                retValue = _cashrcpt.AddParticularsDetailsByPatientSale(cashrcptID, _cashrcpt.DetailId, _SSSale.Id, General.ShopDetail.ShopVoucherSeries, _SSSale.CrdbVouType, _SSSale.CrdbVouNo,
                                                   _SSSale.CrdbVouDate, _SSSale.SaleSubType, _SSSale.CrdbAmountNet, _SSSale.CrdbAmountNet, _SSSale.CrdbAmountClear, 0, 1);
                                            }
                                            if (retValue)
                                            {
                                                _cashrcpt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                                retValue = _cashrcpt.AddAccountDetailsIntbltrnacDebitFromPatientSale(cashrcptID, _cashrcpt.DetailId, FixAccounts.AccountCash, _SSSale.CrdbAmountClear, 0, _SSSale.AccountID, FixAccounts.VoucherTypeForCashReceipt, CashrcptVouNo, _SSSale.CrdbVouDate, _SSSale.ShortName, _SSSale.CreatedBy, _SSSale.CreatedDate, _SSSale.CreatedTime);
                                            }
                                            if (retValue)
                                            {
                                                _cashrcpt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                                retValue = _cashrcpt.AddAccountDetailsIntbltrnacCreditFromPatientSale(cashrcptID, _cashrcpt.DetailId, _SSSale.AccountID, 0, _SSSale.CrdbAmountClear, FixAccounts.AccountCash, FixAccounts.VoucherTypeForCashReceipt, CashrcptVouNo, _SSSale.CrdbVouDate, _SSSale.ShortName, _SSSale.CreatedBy, _SSSale.CreatedDate, _SSSale.CreatedTime);
                                            }
                                        }
                                        else
                                            retValue = true;
                                    }
                                    else
                                    {

                                        _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
                                        txtVouType.Text = _SSSale.CrdbVouType;
                                        _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                                        txtVouchernumber.Text = Convert.ToString(_SSSale.CrdbVouNo);
                                        _SSSale.AccountID = FixAccounts.AccountCash;
                                        retValue = true;
                                    }
                                    if (retValue)
                                    {
                                        retValue = _SSSale.AddDetails();                                        
                                    }

                                    _SavedID = _SSSale.Id; 
                                   
                                    if (retValue)
                                        retValue = SaveIntblTrnac();

                                    if (retValue)
                                        General.CommitTransaction();
                                    else
                                        General.RollbackTransaction();
                                    LockTable.UnLockTables();
                                    System.IO.File.Delete(General.GetPatientSaleTempFile());
                                    if (retValue)
                                    {                                      
                                        string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
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
                                        retValue = true;
                                    }
                                    else
                                    {
                                        PSDialogResult result = PSMessageBox.Show("Could not Add...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                        retValue = false;
                                    }
                                }
                                else
                                {
                                    General.BeginTransaction();
                                    _SSSale.CreatedBy = General.CurrentUser.Id;
                                    _SSSale.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                    _SSSale.CreatedTime = DateTime.Now.ToString("HH:mm:ss");


                                    _SSSale.ModifiedBy = General.CurrentUser.Id;
                                    _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                    _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");

                                    if (_SSSale.IfTypeChange == "Y" && _SSSale.OldVoucherType == FixAccounts.VoucherTypeForCashSale)
                                    {
                                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditSale)
                                        {
                                            _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(_SSSale.CrdbVouType, General.ShopDetail.ShopVoucherSeries);
                                            txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString();
                                            retValue = _SSSale.UpdateDetailsForTypeChange();
                                            if (retValue)
                                            {
                                                retValue = _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
                                            }
                                            if (retValue)
                                            {
                                                retValue = SaveIntblTrnac();
                                            }
                                            if (retValue)
                                                _SSSale.UpdateCreditDebitNoteforTypeChange(_SSSale.CreditDebitNoteID, _SSSale.CrdbAmount, _SSSale.CrdbVouType, _SSSale.CrdbVouNo, _SSSale.CrdbVouDate, null, _SSSale.Id);

                                            if (retValue)
                                                General.CommitTransaction();
                                            else
                                                General.RollbackTransaction();
                                            LockTable.UnLockTables();
                                            if (retValue)
                                            {                                               
                                                string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
                                                PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                                retValue = true;
                                            }
                                            else
                                            {
                                                PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                                retValue = false;
                                            }
                                        }
                                        ClearData();
                                    }

                                    else
                                    {

                                        if (_SSSale.CrdbAmountBalance > 0)
                                        {
                                            _SSSale.AccountID = FixAccounts.AccountPendingCashBills;
                                        }
                                        else
                                        {
                                            _SSSale.AccountID = FixAccounts.AccountCash;
                                        }
                                        retValue = _SSSale.UpdateDetails();
                                        if (_SSSale.PrescriptionFileName != null && _SSSale.PrescriptionFileName != string.Empty)
                                        {
                                            ScanPrescription sp = new ScanPrescription();
                                          
                                        }
                                        if (retValue)
                                            retValue = DeletePreviousRows();
                                       
                                        //if (retValue)
                                        //    General.CommitTransaction();
                                        //else
                                        //{
                                        //    General.RollbackTransaction();
                                        //    PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                        //    retValue = false;
                                        //}
                                        if (retValue)
                                        {
                                            //General.BeginTransaction(); 
                                           
                                            if (retValue)
                                                retValue = SaveIntblTrnac();
                                            if (retValue)
                                                General.CommitTransaction();
                                            else
                                            {
                                                General.RollbackTransaction();
                                                //retValue = _SSSale.ReverseUpdateDetails();
                                               
                                                PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                                retValue = false;
                                            }
                                        }
                                        LockTable.UnLockTables();
                                        if (retValue)
                                        {
                                            _SSSale.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                            _SSSale.AddDetailsInChangedMaster();
                                        
                                            string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
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
                                            _SavedID = _SSSale.Id;
                                            retValue = true;
                                        }

                                    }
                                }
                            }
                       
                   
                    else
                    {
                        LockTable.UnLockTables();
                        _errorMessage = new System.Text.StringBuilder();
                        _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                        foreach (string _message in _SSSale.ValidationMessages)
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
            }
            LockTable.UnLockTables();
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            if (ID != null && ID != "")
            {
                try
                {
                    _SSSale.Id = ID;
                    if (Vmode == "C")
                        _SSSale.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _SSSale.ReadDetailsByIDForDeleted();
                    else
                        _SSSale.ReadDetailsByID();
                    FillDoctorCombo();
                    mcbDoctor.SelectedID = _SSSale.DocID;
                    txtVouType.Text = _SSSale.CrdbVouType;
                    FillPartyCombo();                               
                    InitialisempPVC1(Vmode);
                    mcbCreditor.Text = _SSSale.CrdbName;
                    txtAddress1.Text = _SSSale.PatientAddress1;
                    txtAddress2.Text = _SSSale.PatientAddress2;
                    mcbCreditor.SelectedID = _SSSale.PatientID;
                    txtPatient.Text = _SSSale.ShortName;
                    txtNarration.Text = _SSSale.CrdbNarration.ToString();
                    txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString().Trim();
                    DateTime mydate = new DateTime(Convert.ToInt32(_SSSale.CrdbVouDate.Substring(0, 4)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(4, 2)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    mcbDoctor.SelectedID = _SSSale.DocID;
                    if (_SSSale.DocID != string.Empty)
                    {
                        _SSSale.DoctorName = mcbDoctor.SeletedItem.ItemData[1];
                        if (mcbDoctor.SeletedItem.ItemData[2] != null && mcbDoctor.SeletedItem.ItemData[2].ToString() != string.Empty)
                            _SSSale.DoctorAddress = mcbDoctor.SeletedItem.ItemData[2];
                    }
                    mcbDoctor.Text = _SSSale.DoctorName;                 
                    txtMobileNumber.Text = _SSSale.Telephone;
                    if (_SSSale.CrdbVat5 >= 0)
                        txtVatInput5per.Text = _SSSale.CrdbVat5.ToString("#0.00");
                    if (_SSSale.CrdbVat12point5 >= 0)
                        txtVatInput12point5per.Text = _SSSale.CrdbVat12point5.ToString("#0.00");
                    if (_SSSale.CrdbAmountVat5 >= 0)
                        txtAmountVAT5Per.Text = _SSSale.CrdbAmountVat5.ToString("#0.00");
                    if (_SSSale.CrdbAmountVat12point5 >= 0)
                        txtAmountVAT12Point5Per.Text = _SSSale.CrdbAmountVat12point5.ToString("#0.00");
                   
                    txtRoundAmount.Text = _SSSale.CrdbRoundAmount.ToString("#0.00");
                    txtBillAmount.Text = _SSSale.CrdbBillAmount.ToString("#0.00");                  
                    txtNetAmount.Text = txtBillAmount.Text;
                    txtTotalAmount.Text = _SSSale.CrdbTotalAmount.ToString("#0.00");
                    if (_SSSale.CrdbRoundAmount != 0)
                        cbRound.Checked = true;
                     
                   
                    txtAmountforZeroVAT.Text = _SSSale.CrdbAmtForZeroVAT.ToString("#0.00");
                    rbtCash.Checked = true;
                    if (_SSSale.CrdbRoundAmount != 0)
                        cbRound.Checked = true;
                    else
                        cbRound.Checked = false;                
                    if (_Mode == OperationMode.View || _Mode == OperationMode.Fifth)
                    {                      
                        mcbCreditor.Enabled = false;
                        txtAddress1.Enabled = false;
                        txtAddress2.Enabled = false;
                        txtPatient.Enabled = false;
                        mcbDoctor.Enabled = false;
                    }
                    else
                    {
                                             
                        mcbCreditor.Enabled = true;                       
                        mcbDoctor.Enabled = true;
                        mcbCreditor.Focus();
                    }                   
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
            return true;
        }

        #endregion IDetail Control

        #region IDetail Members
        public override void ReFillData()
        {
            try
            {
                FillPartyCombo();             
                ShowHideAsperSetting();              
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void ShowHideAsperSetting()
        {
            if (General.CurrentSetting.MsetAskOperatorVoucherSale == "Y")
            {
                txtOperator.Visible = true;
                lblOperator.Visible = true;
            }
            else
            {
                txtOperator.Visible = false;
                lblOperator.Visible = false;
            }
            if (General.CurrentSetting.MsetSaleAskRoundinginSale == "Y")
            {
                cbRound.Visible = true;
                txtRoundAmount.Visible = true;
            }
            else
            {
                cbRound.Visible = false;
                txtRoundAmount.Visible = false;
            }

            if (General.CurrentSetting.MsetSaleRoundOff == "Y")
                cbRound.Checked = true;
            else
                cbRound.Checked = false;
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    txtAddress1.Focus();
                    retValue = true;
                }              
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    mcbDoctor.Focus();
                    retValue = true;
                }
              
                if (keyPressed == Keys.H && modifier == Keys.Alt)
                {
                    txtPatient.Focus();
                    retValue = true;
                }              
                     
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    txtNarration.Focus();
                    retValue = true;
                }              

                if (keyPressed == Keys.P && modifier == Keys.Alt)
                {
                    mcbCreditor.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.R && modifier == Keys.Alt)
                {
                    mcbDoctor.Focus();
                    retValue = true;
                }               

                if (keyPressed == Keys.U && modifier == Keys.Alt)
                {
                    cbRound.Focus();
                    retValue = true;
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



        private bool DeletePreviousRows()
        {
            bool returnVal = false;
            try
            {
                returnVal = _SSSale.DeletePreviousRecords();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }
       

      
        

        

      


       

       
        private bool SaveIntblTrnac()
        {
            bool retValue = false;
            try
            {
                double mdebit = 0;
             
                double maddon = 0;
                double mdiscamount = 0;
                double mvat5per = 0;
                double mvat12point5per = 0;
                double mamtforzerovat = 0;
                double mbillamount = 0;
                double mround = 0;
                double mcreditnoteamt = 0;
                double mdebitnoteamt = 0;
                if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                    _SSSale.ShortNameForNarration = _SSSale.ShortName;
                else
                    _SSSale.ShortNameForNarration = ""; 
                double.TryParse(txtVatInput5per.Text, out mvat5per);
                _SSSale.CrdbVat5 = Math.Round(mvat5per, 2);
                double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                _SSSale.CrdbVat12point5 = Math.Round(mvat12point5per, 2);
                double.TryParse(txtAmountforZeroVAT.Text.ToString(), out mamtforzerovat);
                _SSSale.CrdbAmtForZeroVAT = mamtforzerovat;
               
                double.TryParse(txtBillAmount.Text, out mbillamount);
                _SSSale.CrdbAmountNet = mbillamount;
                double.TryParse(txtRoundAmount.Text, out mround);
                _SSSale.CrdbRoundAmount = mround;
             

                mdebit = Math.Round(mbillamount - Math.Round(mvat5per, 2) - Math.Round(mvat12point5per, 2) + mdiscamount - maddon - mround - mamtforzerovat + mcreditnoteamt - mdebitnoteamt, 2);

                if (mamtforzerovat > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVATZeroSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mamtforzerovat;
                    retValue = _SSSale.AddVoucherIntblTrnac();

                }

                if (Math.Round(mvat5per, 2) > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput5Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(mvat5per, 2);
                    retValue = _SSSale.AddVoucherIntblTrnac();

                }
                if (Math.Round(mvat12point5per, 2) > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput12point5Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(mvat12point5per, 2);
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (maddon > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountAddonSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = maddon;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (mround < 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = (mround * -1);
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (mround > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mround;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }

                if (mdiscamount > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountCashDiscountSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mdiscamount;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }

                if (mcreditnoteamt > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountSalesReturn;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mcreditnoteamt;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (mdebitnoteamt > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountDebitNoteSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mdebitnoteamt;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (mdebit > 0)
                {

                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.DebitAccount = FixAccounts.AccountCashSale;
                    else
                        _SSSale.DebitAccount = FixAccounts.AccountCashCreditSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mdebit;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (mbillamount > 0)
                {
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.DebitAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.DebitAccount = FixAccounts.AccountPendingCashBills;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCashSale;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountCashCreditSale;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mbillamount;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void Filldailyshortlist()
        {
            try
            {
                _SSSale.ShortListID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _SSSale.AddToShortList();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
      

       




       



        #endregion

        # region Internal methods
        private void InitialisempPVC1(string vmode)
        {
            try
            {     

                DataTable dtable = new DataTable();
                if (vmode == "C")
                {
                    dtable = _SSSale.ReadProductDetailsByIDForChanged();
                    headerLabel1.Text = "Patient Sale => Changed Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else if (vmode == "D")
                {
                    dtable = _SSSale.ReadProductDetailsByIDForDeleted();
                    headerLabel1.Text = "Patient Sale => Deleted Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else
                    dtable = _SSSale.ReadProductDetailsByID();
              

                Product prod = new Product();
          
                string tempFileName = General.GetPatientSaleTempFile();
               

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

     

      

        private void ClearControls()
        {
            try
            {
               
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                mcbDoctor.Text = "";
                txtMobileNumber.Text = "";
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
                _SSSale.CrdbVouType = txtVouType.Text.ToString();
                _SSSale.SaleSubType = FixAccounts.SubTypeForSaleWithoutStock;
                datePickerBillDate.ResetText();
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtTotalAmount.Text = "0.00";
                txtAmountVAT12Point5Per.Text = "0.00";
                txtAmountVAT5Per.Text = "0.00";             
              
                txtVatInput12point5per.Text = "0.00";
                txtVatInput5per.Text = "0.00";
                txtAmountforZeroVAT.Text = "0.00";
              
                txtRoundAmount.Text = "0.00";
              
                mcbCreditor.Text = "";
                mcbCreditor.SelectedID = "";
                mcbDoctor.SelectedID = "";              
                mcbCreditor.Focus();
                lblFooterMessage.Text = "";
                if (_Mode != OperationMode.Add)
                {
                    mcbCreditor.Enabled = false;                  
                    txtVouchernumber.ReadOnly = false;
                    txtVouchernumber.Enabled = true;
                    txtVouchernumber.Focus();
                }
                else
                {
                    mcbCreditor.Enabled = true;
                    txtAddress1.Enabled = true;
                    txtAddress2.Enabled = true;
                    txtPatient.Enabled = true;
                    mcbDoctor.Enabled = true;                
                    txtVouchernumber.ReadOnly = true;
                    txtVouchernumber.Enabled = false;                  
                    mcbCreditor.Focus();
                }
                if (General.CurrentSetting.MsetAskOperatorVoucherSale == "Y")
                {
                    txtOperator.Visible = true;
                    lblOperator.Visible = true;
                }
                else
                {
                    txtOperator.Visible = false;
                    lblOperator.Visible = false;
                }
                if (General.CurrentSetting.MsetSaleAskRoundinginSale == "Y")
                {
                    cbRound.Visible = true;
                    txtRoundAmount.Visible = true;
                }
                else
                {
                    cbRound.Visible = false;
                    txtRoundAmount.Visible = false;
                }

                if (General.CurrentSetting.MsetSaleRoundOff == "Y")
                    cbRound.Checked = true;
                else
                    cbRound.Checked = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void InitializeScreen()
        {
            try
            {
               
                mcbCreditor.Enabled = true;
                if (_Mode == OperationMode.Edit)
                {
                    txtVouchernumber.ReadOnly = false;
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
        private void FillPartyCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[9] { "PatientID", "PatientName", "PatientAddress1", "PatientAddress2", "ShortNameAddress", "TokenNumber", "DoctorID", "AccCode", "DiscountOffered" };
                mcbCreditor.ColumnWidth = new string[9] { "0", "200", "200", "200", "0", "40", "0", "0", "0" };
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclPatient();
                Patient _Party = new Patient();
                DataTable dtable = _Party.GetOverviewData();
                mcbCreditor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

       
       

        private void FillDoctorCombo()
        {
            try
            {
                mcbDoctor.SelectedID = null;
                mcbDoctor.SourceDataString = new string[4] { "DocID", "DocName", "DocAddress", "DocShortNameAddress" };
                mcbDoctor.ColumnWidth = new string[4] { "0", "200", "300", "0" };
                mcbCreditor.ValueColumnNo = 0;
                mcbDoctor.UserControlToShow = new UclDoctor();
                Doctor _Doctor = new Doctor();
                DataTable dtabled = _Doctor.GetSSDoctorsList();
                mcbDoctor.FillData(dtabled);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }       
              
        #endregion

        #region Other private methods

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

  

        private void CalculateAllAmounts()
        {        
           
            double mtotamt = 0;
            double mamtrcvd = 0;
            double mamtbalance = 0;
            double mvat5 = 0;
            double mvat12point5 = 0;
            double mamount5 = 0;
            double mamount12point5 = 0;
            double mamountzero = 0;
           
           
            double.TryParse(txtAmountforZeroVAT.Text.ToString(), out mamountzero);
            double.TryParse(txtAmountVAT5Per.Text.ToString(), out mamount5);
            double.TryParse(txtAmountVAT12Point5Per.Text.ToString(), out mamount12point5);
            try
            {
               

                mtotamt = mamountzero + mamount5 + mamount12point5;






                txtTotalAmount.Text = mtotamt.ToString("#0.00");

                mvat5 = Math.Round((mamount5 * 5 / 100), 2);
                mvat12point5 = Math.Round((mamount12point5 * 12.5 / 100), 2);
                txtVatInput5per.Text = mvat5.ToString("#0.00");
                txtVatInput12point5per.Text = mvat12point5.ToString("#0.00");

                if (cbRound.Checked == true)
                {
                    txtRoundAmount.Text = Math.Round(Math.Round(Convert.ToDouble(txtTotalAmount.Text.ToString()), 0) - Math.Round(Convert.ToDouble(txtTotalAmount.Text.ToString()), 2), 2).ToString("#0.00");
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text;
                                       
                }
                else
                {
                    txtRoundAmount.Text = "0.00";
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text;
                    if (_Mode == OperationMode.Add)
                    {
                        mamtrcvd = Convert.ToDouble(txtNetAmount.Text.ToString());
                        mamtbalance = 0;
                    }
                    else
                    {
                       
                        mamtbalance = Convert.ToDouble(txtNetAmount.Text.ToString());
                        mamtbalance = mamtbalance - mamtrcvd;
                    }
                   
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
      
      
      
       
        private void ClearSummarySection()
        {
            try
            {
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                         
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void PrintSaleBillPlainPaper()
        {
            PharmaSYSRetailPlus.Printing.PlainPaperPrinter printer = new PharmaSYSRetailPlus.Printing.PlainPaperPrinter();
          //  printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "", _SSSale.Telephone, _SSSale.DoctorNameAddress, "", "", _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount);

        }
        private void PrintSaleBillPrePrintedPaper()
        {
            PharmaSYSRetailPlus.Printing.PrePrintedPaperPrinter printer = new PharmaSYSRetailPlus.Printing.PrePrintedPaperPrinter();
        //    printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "", _SSSale.DoctorNameAddress, "", "", _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount);

        }

    

        private void InitializeCheckBoxes()
        {
            
        }

        #endregion
     

        #region Events

        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = ((PSComboBoxNew)sender).SelectedID;
                mcbCreditor.SelectedID = selectedId;
                FillPartyCombo();
                mcbCreditor.SelectedID = selectedId;
                if (mcbCreditor.SeletedItem != null)
                {
                    _SSSale.TokenNumber = 0;
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[2];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtPatient.Text = mcbCreditor.SeletedItem.ItemData[4];
                    if (mcbCreditor.SeletedItem.ItemData[5] != null && mcbCreditor.SeletedItem.ItemData[5].ToString() != "")
                        _SSSale.TokenNumber = Convert.ToInt32(mcbCreditor.SeletedItem.ItemData[5].ToString());
                    _SSSale.DocID = mcbCreditor.SeletedItem.ItemData[6];
                    mcbDoctor.SelectedID = _SSSale.DocID;                  

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
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                    _SSSale.PatientID = mcbCreditor.SelectedID;
                if (mcbCreditor.SeletedItem == null && mcbCreditor.Text == "")
                {
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtPatient.Text = "";                 
                   
                }
                else if (mcbCreditor.SeletedItem != null)
                {                 
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[2];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtPatient.Text = mcbCreditor.SeletedItem.ItemData[4];
                    double mdis = 0;
                  
                    if (mcbCreditor.SeletedItem.ItemData[8] != null && mcbCreditor.SeletedItem.ItemData[8].ToString() != "")
                        mdis = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[8].ToString());
                 
                    if (_Mode == OperationMode.Add)
                    {
                        if (mcbCreditor.SeletedItem.ItemData[6] != null)
                        {

                            _SSSale.DocID = mcbCreditor.SeletedItem.ItemData[6];
                            mcbDoctor.SelectedID = _SSSale.DocID;
                        }
                    }
                    if (mcbCreditor.SeletedItem.ItemData[5] != null && mcbCreditor.SeletedItem.ItemData[5].ToString() != "")
                        _SSSale.TokenNumber = Convert.ToInt32(mcbCreditor.SeletedItem.ItemData[5].ToString());
                   
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

       
        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
    
        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbDoctor.SelectedID;
                FillDoctorCombo();
                mcbDoctor.SelectedID = selectedId;
                if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "" && mcbDoctor.SeletedItem.ItemData[2] != null)
                {                  
                    txtDoctorShortName.Text = mcbDoctor.SeletedItem.ItemData[3].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                txtPatient.Text = mcbCreditor.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
               
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "" && (mcbCreditor.SeletedItem.ItemData[1].ToString() != mcbCreditor.Text.ToString()))
                {
                    mcbCreditor.SeletedItem.ItemData[0] = "";
                    mcbCreditor.SelectedID = "";
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtPatient.Text = "";
                }
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "" && (mcbCreditor.SeletedItem.ItemData[1].ToString() == mcbCreditor.Text.ToString()))
                {                    
                        mcbDoctor.Focus();
                }              
                txtAddress1.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "" && mcbDoctor.SeletedItem.ItemData[2] != null)
            {               
                txtDoctorShortName.Text = mcbDoctor.SeletedItem.ItemData[3].ToString();
                txtAmountforZeroVAT.Focus();
            }
           
        }
        private void mcbDoctor_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "" && mcbDoctor.SeletedItem.ItemData[2] != null)
            {
                txtDoctorShortName.Text = mcbDoctor.SeletedItem.ItemData[3].ToString();
                txtAmountforZeroVAT.Focus();
            }
        }

        private void datePickerBillDate_KeyDown(object sender, KeyEventArgs e)
        {

        }
      

      
        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow dr = null;
                int vouno = 0;
                string voutype = FixAccounts.VoucherTypeForCashSale;
                try
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (txtVouchernumber.Text != "")
                        {
                            int.TryParse(txtVouchernumber.Text.ToString(), out vouno);
                            _SSSale.CrdbVouType = voutype;
                            _SSSale.CrdbVouNo = vouno;
                            dr = _SSSale.ReadDetailsByVouNumber();
                            if (dr == null)
                                Cancel();
                            else
                                FillSearchData(_SSSale.Id, "");
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtAddress2.Focus();
                    break;
                case Keys.Up:
                    mcbCreditor.Focus();
                    break;
                case Keys.Down:
                    txtAddress2.Focus();
                    break;
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtMobileNumber.Focus();
                    break;
                case Keys.Up:
                    txtAddress1.Focus();
                    break;
                case Keys.Down:
                    txtMobileNumber.Focus();
                    break;
            }

        }
        private void txtMobileNumber_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtPatient.Focus();
                    break;
                case Keys.Up:
                    txtAddress2.Focus();
                    break;
                case Keys.Down:
                    txtPatient.Focus();
                    break;
            }

        }

        private void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            txtPatient.Text = mcbCreditor.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
        }

        private void txtPatient_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _SSSale.CrdbName = txtPatient.Text.ToString();
                    mcbDoctor.Focus();
                    break;
                case Keys.Up:
                    txtMobileNumber.Focus();
                    break;
            }
        }
      
        private void txtAmountforZeroVAT_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    CalculateAllAmounts();
                    txtAmountVAT5Per.Focus();         
                    break;
                case Keys.Up:
                    mcbDoctor.Focus();
                    break;
            }
        }
        private void txtAmountVAT5Per_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    CalculateAllAmounts();
                    txtAmountVAT12Point5Per.Focus();
                    break;
                case Keys.Up:
                    txtAmountforZeroVAT.Focus();
                    break;
            }
        }
        private void txtAmountVAT12Point5Per_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    CalculateAllAmounts();
                    txtNarration.Focus();
                    break;
                case Keys.Up:
                    txtAmountVAT5Per.Focus();
                    break;
            }
        }

     

       
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtOperator.Focus();
                    break;
                case Keys.Up:
                    txtAmountVAT12Point5Per.Focus();
                    break;
            }
        }
       

      

      
      
    
        private void datePickerBillDate_Validating(object sender, CancelEventArgs e)
        {
            bool retValue = false;
            retValue = General.CheckBillDateForAccountingYear(datePickerBillDate.Text.ToString());
            if (retValue)
            {
                lblFooterMessage.Text = "";

            }
            else
            {
                datePickerBillDate.Focus();
                lblFooterMessage.Text = "Check Bill Date";
            }



        }
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {

        }
        #endregion

      
       

     

      

     
    }
}
