using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSDistributorPlus.Common;
using PharmaSYSDistributorPlus.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using PharmaSYSDistributorPlus.InterfaceLayer.CommonControls;
using PrintDataGrid;
using System.IO;
using PharmaSYSDistributorPlus.InterfaceLayer.Classes;

namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSpecialSale : BaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SSSale _SSSale;
        string _preID = "";
        bool IsClearData = true;
        #endregion

        #region Constructor
        public UclSpecialSale()
        {
            try
            {
                InitializeComponent();
                _SSSale = new SSSale();
                SearchControl = new UclSpecialSaleSearch();
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
                if (IsClearData == false)
                    return false;
                _SSSale.Initialise();
                ClearControls();
                ConstructMainColumns();
                FormatGrids();
                if (_Mode == OperationMode.Add)
                {

                    if (AddNewRowCheck())
                        mpPVC1.Rows.Add();
                }
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
                if (IsClearData == false)
                {
                    IsClearData = true;
                    return false;
                }
                ClearData();
                txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
                InitializeScreen();
                headerLabel1.Text = "Bill Reprint -> NEW";

                if (General.CurrentUser.Level > 1)
                    cbEditRate.Visible = false;
                _SSSale.ReadBillPrintSettings();
                mpPVC1.ModuleNumber = ModuleNumber.PatientSale;
                mpPVC1.OperationMode = OperationMode.Add;
                InitialisempPVC1("");
                AddToolTip();
                FillDoctorCombo();
                FillPrescription();
                mcbDoctor.SelectedID = null;
                FillPartyCombo();
                FillTxtAddress();
                InitializeCheckBoxes();
                cbclonevoutype.Items.Add(FixAccounts.VoucherTypeForCashSale);
                cbclonevoutype.Items.Add(FixAccounts.VoucherTypeForCreditSale);
                cbclonevoutype.Items.Add(FixAccounts.VoucherTypeForCreditStatementSale);
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                //txtVouchernumber.Focus();
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
                if (IsClearData == false)
                {
                    IsClearData = true;
                    return false;
                }
                ClearData();
                FillPrescription();
                headerLabel1.Text = "Bill Reprint -> EDIT";
                InitializeCheckBoxes();
                if (General.CurrentUser.Level > 1)
                    cbEditRate.Visible = false;
                FillPartyCombo();
                FillTxtAddress();
                mpPVC1.ClosePopupGrid();
                mpPVC1.ModuleNumber = ModuleNumber.PatientSale;
                mpPVC1.OperationMode = OperationMode.Edit;
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

            if (AddNewRowCheck())
                mpPVC1.Rows.Add();

            return retValue;
        }

        public override bool Exit()
        {
            bool retValue = base.Exit();
            if (retValue)
            {
                //  System.IO.File.Delete(General.GetPatientSaleTempFile());
                //  General.DeleteTempStockByModuleNumber(ModuleNumber.PatientSale);
                // UpdateClosingStockinCache();
            }
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "Bill Reprint -> DELETE";
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
                        BindTempGrid();
                        LockTable.LockTablesForSale();
                        General.BeginTransaction();
                        _SSSale.ModifiedBy = General.CurrentUser.Id;
                        _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _SSSale.DeleteDetails();
                        if (retValue)
                            retValue = _SSSale.DeletePreviousRecords();
                        if (retValue)
                            retValue = AddPreviousStock();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            //  UpdateClosingStockinCache();
                            _SSSale.ModifiedBy = General.CurrentUser.Id;
                            _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            _SSSale.AddDetailsInDeleteMaster();
                            AddPreviousRowsInDeleteDetail();
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
                headerLabel1.Text = "Bill Reprint -> VIEW";
                InitializeCheckBoxes();
                mpPVC1.ClosePopupGrid();
                //   GetLastRecord();
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
                    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
                }
                //  _SSSale.SaleSubType = FixAccounts.SubTypeForPatientSale;
                _SSSale.GetLastRecordForSaleSpecialSale();
                FillSearchData(_SSSale.Id, "");
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
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _SSSale.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _SSSale.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _SSSale.GetFirstRecordSpecialSale();
            if (dr != null && dr["ID"] != DBNull.Value)
            {
                _SSSale.Id = dr["ID"].ToString();
                FillSearchData(_SSSale.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _SSSale.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _SSSale.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _SSSale.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _SSSale.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _SSSale.CrdbVouNo = i;
                dr = _SSSale.ReadDetailsByVouNumberSpecialSale();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["ID"] != DBNull.Value)
            {
                _SSSale.Id = dr["ID"].ToString();
                FillSearchData(_SSSale.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            txtVoucherSeries.Text = General.ShopDetail.ShopVoucherSeries;
            int lastvouno = _SSSale.GetLastVoucherNumberSpecialSale(FixAccounts.VoucherTypeForCashSale, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            _SSSale.SaleSubType = FixAccounts.SubTypeForPatientSale;
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _SSSale.CrdbVouNo = i;
                dr = _SSSale.ReadDetailsByVouNumberSpecialSale();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["ID"] != DBNull.Value)
            {
                _SSSale.Id = dr["ID"].ToString();
                FillSearchData(_SSSale.Id, "");
            }
            return retValue;
        }
        public override bool Save()
        {
            return SaveData(false);
        }

        public override bool SaveAndPrint()
        {
            // if (Mode == OperationMode.Add)
            return SaveData(true);
            ///else 
              // return Print();
        }

        private bool SaveData(bool printData)
        {
            bool retValue = false;
            if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
            {
                double mdiscper = 0;
                double maddon = 0;
                double mdiscamount = 0;
                double mvat5per = 0;
                double mvat12point5per = 0;
                double mamtforzerovat = 0;
                double mbillamount = 0;
                double mamount = 0;
                double mround = 0;
                double mamountvat5per = 0;
                double mamountvat12point5per = 0;
                double mdebitnoteamount = 0;

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
                        _SSSale.SaleSubType = FixAccounts.SubTypeForPatientSale;

                        if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                            _SSSale.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                        _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                        double.TryParse(txtVatInput5per.Text, out mvat5per);
                        _SSSale.CrdbVat5 = mvat5per;
                        double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                        _SSSale.CrdbVat12point5 = mvat12point5per;
                        double.TryParse(txtAmountforZeroVAT.Text.ToString(), out mamtforzerovat);
                        _SSSale.CrdbAmtForZeroVAT = mamtforzerovat;
                        double.TryParse(txtDiscPercent.Text, out mdiscper);
                        _SSSale.CrdbDiscPer = mdiscper;
                        double.TryParse(txtDiscAmount.Text, out mdiscamount);
                        _SSSale.CrdbDiscAmt = mdiscamount;
                        _SSSale.TotalDiscount5 = Convert.ToDouble(txtdiscountAmount5.Text.ToString());
                        _SSSale.TotalDiscount12point5 = Convert.ToDouble(txtDiscountAmount12point5.Text.ToString());
                        double.TryParse(txtAmountVAT12Point5Per.Text, out mamountvat12point5per);
                        _SSSale.CrdbAmountVat12point5 = mamountvat12point5per;
                        double.TryParse(txtAmountVAT5Per.Text, out mamountvat5per);
                        _SSSale.CrdbAmountVat5 = mamountvat5per;
                        double.TryParse(txtBillAmount.Text, out mbillamount);
                        _SSSale.CrdbAmountNet = mbillamount;
                        _SSSale.CrdbAmountClear = Convert.ToDouble(txtAmountRcvd.Text.ToString());
                        _SSSale.CrdbAmountBalance = Convert.ToDouble(txtAmountBalance.Text.ToString());
                        double.TryParse(txtAmount.Text, out mamount);
                        _SSSale.CrdbAmount = mamount;
                        double.TryParse(txtRoundAmount.Text, out mround);
                        _SSSale.CrdbRoundAmount = mround;
                        double.TryParse(txtAddOn.Text, out maddon);
                        _SSSale.CrdbAddOn = maddon;
                        _SSSale.DbNoteAmount = mdebitnoteamount; ;
                        _SSSale.CrdbNarration = txtNarration.Text.ToString().Trim();
                        _SSSale.CrdbName = mcbCreditor.Text.ToString();
                        _SSSale.ShortName = mcbCreditor.Text.ToString();
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
                        CalculateProfitPercent();
                        if (_Mode == OperationMode.Edit)
                            _SSSale.IFEdit = "Y";
                        if (_SSSale.CrdbVouNo == 0)
                        {
                            _errorMessage = new System.Text.StringBuilder();
                            _errorMessage.Append("Please enter Voucher Number" + Environment.NewLine);
                            foreach (string _message in _SSSale.ValidationMessages)
                            {
                                _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                            }
                            MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            retValue = false;
                        }
                        else retValue = true;
                        if(retValue)
                            _SSSale.Validate();

                        if (_SSSale.IsValid && retValue)
                        {
                            LockTable.LockTablesForSpecialSale();
                            bool ifstockavailable = true;
                            //    if (_SSSale.IfTypeChange == "N")
                            //       ifstockavailable = CheckForStockintblStock();
                            if (ifstockavailable)
                            {
                                if ((_Mode == OperationMode.Add || _Mode == OperationMode.Edit) || _Mode == OperationMode.OpenAsChild)
                                {
                                    General.BeginTransaction();

                                    _SSSale.CreatedBy = General.CurrentUser.Id;
                                    _SSSale.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                    _SSSale.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                                    _SSSale.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");



                                    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
                                    txtVouType.Text = _SSSale.CrdbVouType;
                                    //  _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                                    //   txtVouchernumber.Text = Convert.ToString(_SSSale.CrdbVouNo);

                                    _SSSale.AccountID = FixAccounts.AccountCash.ToString();
                                    retValue = true;
                                    //}
                                    if (retValue)
                                        retValue = _SSSale.AddDetailsSpecialSale();
                                    _SavedID = _SSSale.Id;
                                    if (retValue)
                                        retValue = SaveparticularsProductwise();
                                    //  if (retValue)
                                    //      retValue = ReduceStockIntblStock();
                                    //     if (retValue)
                                    //    {
                                    //        if (_SSSale.PatientID != null && _SSSale.PatientID != "")
                                    //            _SSSale.SaveDiscPercentInPatientMaster(_SSSale.PatientID, _SSSale.CrdbDiscPer);

                                    //    }

                                    if (retValue)
                                        General.CommitTransaction();
                                    else
                                        General.RollbackTransaction();
                                    LockTable.UnLockTables();
                                    // System.IO.File.Delete(General.GetPatientSaleTempFile());
                                    if (retValue)
                                    {
                                        //   UpdateClosingStockinCache();
                                        //  General.DeleteTempStockByModuleNumber(ModuleNumber.PatientSale);
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



                                    _SSSale.AccountID = FixAccounts.AccountCash;

                                    retValue = _SSSale.UpdateDetails();
                                    if (retValue)
                                        retValue = DeletePreviousRows();
                                    if (retValue)
                                        retValue = AddPreviousStock();
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
                                        retValue = SaveparticularsProductwise();

                                        //if (retValue)
                                        //    retValue = ReduceStockIntblStock();
                                        if (retValue)
                                        {
                                            if (_SSSale.AccountID != null && _SSSale.AccountID != "")
                                                _SSSale.SaveDiscPercentInPatientMaster(_SSSale.AccountID, _SSSale.CrdbDiscPer);

                                        }
                                        if (retValue)
                                            General.CommitTransaction();
                                        else
                                        {
                                            General.RollbackTransaction();
                                            //retValue = _SSSale.ReverseUpdateDetails();
                                            //retValue = AddPreviousRows();
                                            //retValue = ReducePreviousStock();
                                            PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                            retValue = false;
                                        }
                                    }
                                    LockTable.UnLockTables();
                                    if (retValue)
                                    {
                                        //   UpdateClosingStockinCache();
                                        _SSSale.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                        _SSSale.AddDetailsInChangedMaster();
                                        AddPreviousRowsInChangedDetail();
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

                        else if(retValue)
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
                    _SSSale.SaleSubType = FixAccounts.SubTypeForPatientSale;
                    if (Vmode == "C")
                        _SSSale.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _SSSale.ReadDetailsByIDForDeleted();
                    else
                        _SSSale.ReadDetailsByID(); //_SSSale.ReadDetailsByIDSpecialSale();
                  //  FillDoctorCombo();
                    mcbDoctor.SelectedID = _SSSale.DocID;
                    txtVouType.Text = _SSSale.CrdbVouType;
                  //  FillPartyCombo();
                 //   FillTxtAddress();
                //    BindTempGrid();

                //    InitialisempPVC1(Vmode);
                    mcbCreditor.Text = _SSSale.CrdbName;
                    txtAddress1.Text = _SSSale.PatientAddress1;
                    txtAddress2.Text = _SSSale.PatientAddress2;
                    mcbCreditor.SelectedID = _SSSale.PatientID;
                    //txtPatient.Text = _SSSale.ShortName;
                    txtNarration.Text = _SSSale.CrdbNarration.ToString();
                    txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString().Trim();
                    DateTime mydate = new DateTime(Convert.ToInt32(_SSSale.CrdbVouDate.Substring(0, 4)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(4, 2)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    mcbDoctor.SelectedID = _SSSale.DocID;
                    _SSSale.DoctorName = mcbDoctor.SeletedItem.ItemData[1];
                    if (mcbDoctor.SeletedItem.ItemData[2] != null && mcbDoctor.SeletedItem.ItemData[2].ToString() != string.Empty)
                        _SSSale.DoctorAddress = mcbDoctor.SeletedItem.ItemData[2];
                    if (_SSSale.CrdbVat5 >= 0)
                        txtVatInput5per.Text = _SSSale.CrdbVat5.ToString("#0.00");
                    if (_SSSale.CrdbVat12point5 >= 0)
                        txtVatInput12point5per.Text = _SSSale.CrdbVat12point5.ToString("#0.00");
                    if (_SSSale.CrdbAmountVat5 >= 0)
                        txtAmountVAT5Per.Text = _SSSale.CrdbAmountVat5.ToString("#0.00");
                    if (_SSSale.CrdbAmountVat12point5 >= 0)
                        txtAmountVAT12Point5Per.Text = _SSSale.CrdbAmountVat12point5.ToString("#0.00");
                    txtAmount.Text = _SSSale.CrdbAmount.ToString("#0.00");
                    txtDiscPercent.Text = _SSSale.CrdbDiscPer.ToString("#0.00");
                    txtRoundAmount.Text = _SSSale.CrdbRoundAmount.ToString("#0.00");
                    txtBillAmount.Text = _SSSale.CrdbBillAmount.ToString("#0.00");
                    txtAmountBalance.Text = _SSSale.CrdbAmountBalance.ToString("#0.00");
                    txtAmountRcvd.Text = _SSSale.CrdbAmountClear.ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text;
                    txtTotalAmount.Text = _SSSale.CrdbTotalAmount.ToString("#0.00");
                    txtDiscAmount.Text = _SSSale.CrdbDiscAmt.ToString("#0.00");
                    txtAddOn.Text = _SSSale.CrdbAddOn.ToString("#0.00");

                    txtAmountforZeroVAT.Text = _SSSale.CrdbAmtForZeroVAT.ToString("#0.00");
                    rbtCash.Checked = true;
                //    NoofRows();
                    if (_Mode == OperationMode.View)
                    {
                        mpPVC1.ColumnsMain["Col_ProductName"].ReadOnly = true;
                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
                        mpPVC1.IsAllowDelete = false;
                        mcbCreditor.Enabled = false;
                        txtAddress1.Enabled = false;
                        txtAddress2.Enabled = false;
                        // txtPatient.Enabled = false;
                        mcbDoctor.Enabled = false;
                    }
                    else
                    {
                        if (_Mode == OperationMode.Edit)
                        {
                            txtAmountRcvd.Enabled = false;
                            txtAmountBalance.Enabled = false;
                        }


                        mpPVC1.ColumnsMain["Col_ProductName"].ReadOnly = false;
                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = false;
                        mpPVC1.IsAllowDelete = true;
                        mcbCreditor.Enabled = true;
                        if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                        {
                            //txtAddress1.Enabled = false;
                            //txtAddress2.Enabled = false;
                            //txtPatient.Enabled = false;
                        }
                        else
                        {
                            //txtAddress1.Enabled = true;
                            //txtAddress2.Enabled = true;
                            //txtPatient.Enabled = true;
                        }
                        mcbDoctor.Enabled = true;
                        mpPVC1.SetFocus(1);
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
        public override void ReFillData(Control closedControl)
        {
            try
            {
                if (closedControl is UclAccount)
                {
                    string preselectedID = "";
                    if (mcbCreditor.SelectedID != null)
                        preselectedID = mcbCreditor.SelectedID;
                 //   FillPartyCombo();
                //    FillTxtAddress();
                    mcbCreditor.SelectedID = preselectedID;
                }
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
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    if (btnClone.Enabled)
                        btnClone.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    mcbDoctor.Focus();
                    retValue = true;
                }
                //if (keyPressed == Keys.D && modifier == Keys.Alt)
                //{
                //    datePickerBillDate.Focus();
                //    retValue = true;
                //}
                if (keyPressed == Keys.E && modifier == Keys.Alt)
                {
                    if (cbEditRate.Visible == true)
                    {
                        cbEditRate.Focus();
                    }
                    retValue = true;
                }

                //if (keyPressed == Keys.H && modifier == Keys.Alt)
                //{
                //    txtPatient.Focus();
                //    retValue = true;
                //}
                if (keyPressed == Keys.L && modifier == Keys.Alt)
                {
                    if (cbFill.Checked != true)
                    {
                        cbFill.Checked = true;
                    }
                    retValue = true;
                }
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    txtNarration.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.O && modifier == Keys.Alt)
                {
                    if (pnlClone.Visible)
                    {
                        btnOKCloneClick();
                    }
                    else
                        txtAddOn.Focus();
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
                if (keyPressed == Keys.S && modifier == Keys.Alt)
                {
                    if (txtDiscPercent.Visible)
                    {
                        txtDiscPercent.Focus();
                    }
                    retValue = true;
                }

                if (keyPressed == Keys.U && modifier == Keys.Alt)
                {
                    cbRound.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.P && modifier == Keys.Control)
                {
                    IsClearData = false;
                }
                if (keyPressed == Keys.Escape /*&& pnlCenter.Enabled == true*/)
                {
                    if (mpPVC1.VisibleProductGrid() == true) //kiran 09112016
                    {
                        retValue = true;
                    }
                    else
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



        private bool DeletePreviousRows()
        {
            bool returnVal = false;
            try
            {
                returnVal = _SSSale.DeletePreviousRecords();
                // returnVal = _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }
        private bool CheckForStockintblStock()
        {
            bool retValue = true;
            string mlastsaleid;
            string mproductname;
            string mpack;
            string muom;
            try
            {
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null && prodrow.Cells["Col_Quantity"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        mproductname = prodrow.Cells["Col_ProductName"].Value.ToString();
                        mpack = prodrow.Cells["Col_Pack"].Value.ToString();
                        muom = prodrow.Cells["Col_UOM"].Value.ToString();
                        mlastsaleid = "";
                        _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                        _SSSale.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                        _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                        if (prodrow.Cells["Col_StockID"].Value != null)
                            mlastsaleid = prodrow.Cells["Col_StockID"].Value.ToString();
                        _SSSale.LastStockID = mlastsaleid;
                        int stockavailable = 0;
                        stockavailable = _SSSale.GetStockByStockID();
                        int tempstock = 0;

                        foreach (DataGridViewRow temprow in dgtemp.Rows)
                        {
                            string mtempid = "";
                            if (temprow.Cells["Temp_StockID"].Value != null)
                                mtempid = temprow.Cells["Temp_StockID"].Value.ToString();
                            if (mtempid == mlastsaleid && mtempid != "")
                            {
                                tempstock = Convert.ToInt32(temprow.Cells["Temp_Quantity"].Value.ToString());
                                break;
                            }
                        }

                        if (stockavailable + tempstock < _SSSale.Quantity)
                        {
                            if (stockavailable == 0)
                                MessageBox.Show("Stock Not Available For " + mproductname + " " + muom + " " + mpack);
                            else
                                MessageBox.Show("Stock Available : " + stockavailable + " : For " + mproductname + " " + muom + " " + mpack);
                            retValue = false;
                            LockTable.UnLockTables();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                retValue = false;
            }
            LockTable.UnLockTables();
            return retValue;
        }
        private void CalculateProfitPercent()
        {
            _SSSale.ProfitPercentByPurchaseRate = 0;
            _SSSale.ProfitPercentBySaleRate = 0;
            _SSSale.TotalProfitPercentByPurchaseRate = 0;
            _SSSale.TotalProfitPercentBySaleRate = 0;
            _SSSale.TotalProfitInRupees = 0;

            double mqty = 0;
            double mpurrate = 0;
            double mtraderate = 0;
            double msalerate = 0;
            double mpakn = 0;
            double mprate = 0;
            double mvatper = 0;
            double mvatamt = 0;
            double mamt = 0;
            double mrate = 0;

            double totalsale = 0;
            double totalpur = 0;
            double totalvat = 0;
            // double totaldisc = 0;

            try
            {
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    mqty = 0;
                    mpurrate = 0;
                    mtraderate = 0;
                    msalerate = 0;
                    mpakn = 0;
                    mvatper = 0;
                    mvatamt = 0;
                    mprate = 0;
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        if (prodrow.Cells["Col_UOM"].Value != null)
                            double.TryParse(prodrow.Cells["Col_UOM"].Value.ToString(), out mpakn);
                        if (prodrow.Cells["Col_Quantity"].Value != null)
                            double.TryParse(prodrow.Cells["Col_Quantity"].Value.ToString().Trim(), out mqty);
                        if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                        mrate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());
                        if (prodrow.Cells["Col_MRP"].Value == null)
                            prodrow.Cells["Col_MRP"].Value = mrate.ToString("#0.00");
                        if (mpurrate == 0)
                            mpurrate = mrate - (mrate * 18 / 100);
                        _SSSale.PurchaseRate = mpurrate;
                        if (prodrow.Cells["Col_TradeRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                        if (mtraderate == 0)
                            mtraderate = mpurrate;
                        if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                        if (mprate == 0)
                            mprate = mpurrate;

                        _SSSale.TradeRate = mtraderate;
                        double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                        if (prodrow.Cells["Col_VATPer"].Value != null)
                            double.TryParse(prodrow.Cells["Col_VATPer"].Value.ToString(), out mvatper);
                        mvatamt = Math.Round((mtraderate * mvatper) / 100, 2);
                        mamt = Math.Round(Math.Round(mrate / mpakn, 4) * mqty, 2);
                        double mdiscamt = 0;
                        if (prodrow.Cells["Col_DiscountAmount"].Value != null)
                            mdiscamt = Convert.ToDouble(prodrow.Cells["Col_DiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_MySpecialDiscountAmount"].Value != null && prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString() != string.Empty)
                            mdiscamt += Convert.ToDouble(prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString());
                        _SSSale.SaleRate = msalerate;
                        double newmdiscper = 0;
                        double newmydiscper = 0;
                        double.TryParse(txtDiscPercent.Text, out newmdiscper);
                        //double.TryParse(txtMyDiscountPercent.Text, out newmydiscper);
                        double newsalerate = msalerate - Math.Round(((msalerate - Math.Round((msalerate * mvatper / 100), 2)) * (newmdiscper + newmydiscper) / 100), 2);
                        double vatontrrate = Math.Round((mtraderate * mvatper) / 100, 2);

                        totalvat += vatontrrate;
                        totalsale += newsalerate;
                        totalpur += mpurrate;

                        _SSSale.ProfitPercentBySaleRate = Math.Round(((msalerate - mdiscamt) - (mpurrate)) / (msalerate - mdiscamt), 4);
                        _SSSale.ProfitPercentByPurchaseRate = Math.Round(((msalerate - mdiscamt) - (mpurrate)) / (mpurrate), 4);
                        //_SSSale.TotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
                        //_SSSale.TotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
                        _SSSale.ProfitInRupees = Math.Round((((msalerate - mdiscamt) - (mpurrate)) / mpakn) * mqty, 2);
                        _SSSale.TotalProfitInRupees += _SSSale.ProfitInRupees;
                        prodrow.Cells["Col_ProfitPercentBySaleRate"].Value = _SSSale.ProfitPercentBySaleRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _SSSale.ProfitPercentByPurchaseRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitInRupees"].Value = _SSSale.ProfitInRupees.ToString("#0.00");
                    }

                }
                _SSSale.TotalProfitPercentBySaleRate = Math.Round(((totalsale) - (totalpur)) / (totalsale), 4);
                _SSSale.TotalProfitPercentByPurchaseRate = Math.Round(((totalsale) - (totalpur)) / (totalpur), 4);
                //if (pnlFinal.Visible && General.CurrentSetting.MsetSaleShowProfitInSaleBill == "Y")
                //{
                //    txtProfit.Text = _SSSale.TotalProfitInRupees.ToString("#0.00");
                //}
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        //private void CalculateProfitPercent()
        //{
        //    _SSSale.ProfitPercentByPurchaseRate = 0;
        //    _SSSale.ProfitPercentBySaleRate = 0;
        //    _SSSale.TotalProfitPercentByPurchaseRate = 0;
        //    _SSSale.TotalProfitPercentBySaleRate = 0;
        //    _SSSale.TotalProfitInRupees = 0;

        //    double mqty = 0;
        //    double mpurrate = 0;
        //    double mtraderate = 0;
        //    double msalerate = 0;
        //    double mpakn = 0;
        //    double mvatper = 0;
        //    double mvatamt = 0;
        //    try
        //    {
        //        foreach (DataGridViewRow prodrow in mpPVC1.Rows)
        //        {
        //            mqty = 0;
        //            mpurrate = 0;
        //            mtraderate = 0;
        //            msalerate = 0;
        //            mpakn = 0;
        //            mvatper = 0;
        //            mvatamt = 0;

        //            if (prodrow.Cells["Col_ProductName"].Value != null &&
        //               Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
        //            {
        //                if (prodrow.Cells["Col_UOM"].Value != null)
        //                    double.TryParse(prodrow.Cells["Col_UOM"].Value.ToString(), out mpakn);
        //                if (prodrow.Cells["Col_Quantity"].Value != null)
        //                    double.TryParse(prodrow.Cells["Col_Quantity"].Value.ToString().Trim(), out mqty);
        //                if (prodrow.Cells["Col_PurchaseRate"].Value != null)
        //                    double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
        //                _SSSale.PurchaseRate = mpurrate;
        //                if (prodrow.Cells["Col_TradeRate"].Value != null)
        //                    double.TryParse(prodrow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
        //                _SSSale.TradeRate = mtraderate;
        //                if (prodrow.Cells["Col_VATPer"].Value != null)
        //                    double.TryParse(prodrow.Cells["Col_VATPer"].Value.ToString(), out mvatper);
        //                mvatamt = Math.Round((mtraderate * mvatper) / 100, 2);
        //                double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
        //                _SSSale.SaleRate = msalerate;
        //                _SSSale.ProfitPercentBySaleRate = Math.Round((msalerate - (mpurrate + mvatamt)) / msalerate, 4);
        //                _SSSale.ProfitPercentByPurchaseRate = Math.Round((msalerate - (mpurrate + mvatamt)) / (mpurrate + mvatamt), 4);
        //                _SSSale.TotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
        //                _SSSale.TotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
        //                _SSSale.ProfitInRupees = Math.Round(((msalerate - (mpurrate + mvatamt)) / mpakn) * mqty, 2);
        //                _SSSale.TotalProfitInRupees += _SSSale.ProfitInRupees;
        //                prodrow.Cells["Col_ProfitPercentBySaleRate"].Value = _SSSale.ProfitPercentBySaleRate.ToString("#0.00");
        //                prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _SSSale.ProfitPercentByPurchaseRate.ToString("#0.00");
        //                prodrow.Cells["Col_ProfitInRupees"].Value = _SSSale.ProfitInRupees.ToString("#0.00");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}
        private bool SaveparticularsProductwise()
        {
            {
                bool returnVal = false;
                _SSSale.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                    {
                        if (prodrow.Cells["Col_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                        {
                            _SSSale.SerialNumber += 1;
                            int mqty = 0;
                            double mpurrate = 0;
                            double mtraderate = 0;
                            double mmrp = 0;
                            double msalerate = 0;
                            double mvatper = 0;
                            double mamt = 0;
                            double mvatamt = 0;
                            string mlastsaleid = "";
                            double mpakn = 0;
                            _SSSale.ProfitPercentBySaleRate = 0;
                            _SSSale.ProfitPercentByPurchaseRate = 0;
                            _SSSale.ProfitInRupees = 0;

                            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                            _SSSale.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            _SSSale.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                            if (prodrow.Cells["Col_ExpiryDate"].Value != null)
                                _SSSale.ExpiryDate = prodrow.Cells["Col_ExpiryDate"].Value.ToString();
                            int.TryParse(prodrow.Cells["Col_Quantity"].Value.ToString().Trim(), out mqty);
                            _SSSale.Quantity = mqty;
                            if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                                double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            _SSSale.PurchaseRate = mpurrate;
                            if (prodrow.Cells["Col_TradeRate"].Value != null)
                                double.TryParse(prodrow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                            _SSSale.TradeRate = mtraderate;
                            double.TryParse(prodrow.Cells["Col_MRP"].Value.ToString().Trim(), out mmrp);
                            _SSSale.MRP = mmrp;
                            double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                            _SSSale.SaleRate = msalerate;
                            double.TryParse(prodrow.Cells["Col_VATPer"].Value.ToString().Trim(), out mvatper);
                            _SSSale.VATPer = mvatper;
                            double.TryParse(prodrow.Cells["Col_VATAmount"].Value.ToString().Trim(), out mvatamt);
                            _SSSale.VATAmount = mvatamt;
                            double.TryParse(prodrow.Cells["Col_Amount"].Value.ToString().Trim(), out mamt);
                            _SSSale.Amount = mamt;
                            if (prodrow.Cells["Col_StockID"].Value != null)
                                mlastsaleid = (prodrow.Cells["Col_StockID"].Value.ToString());
                            if (prodrow.Cells["Col_UOM"].Value != null)
                                double.TryParse(prodrow.Cells["Col_UOM"].Value.ToString(), out mpakn);
                            _SSSale.ProfitPercentBySaleRate = Math.Round((msalerate - mpurrate) / msalerate, 4);
                            _SSSale.ProfitPercentByPurchaseRate = Math.Round((msalerate - mpurrate) / mpurrate, 4);
                            _SSSale.ProfitInRupees = Math.Round(((msalerate - mpurrate) / mpakn) * mqty, 2);
                            if (prodrow.Cells["Col_ProfitPercentBySaleRate"].Value != null)
                                _SSSale.ProfitPercentBySaleRate = Convert.ToDouble(prodrow.Cells["Col_ProfitPercentBySaleRate"].Value.ToString());

                            if (prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value != null)
                                _SSSale.ProfitPercentByPurchaseRate = Convert.ToDouble(prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value.ToString());

                            if (prodrow.Cells["Col_ProfitInRupees"].Value != null)
                                _SSSale.ProfitInRupees = Convert.ToDouble(prodrow.Cells["Col_ProfitInRupees"].Value.ToString());
                            if (prodrow.Cells["Col_DiscountAmount"].Value != null)
                                _SSSale.CrdbDiscAmt = Convert.ToDouble(prodrow.Cells["Col_DiscountAmount"].Value.ToString());
                            if (prodrow.Cells["Col_MySpecialDiscountAmount"].Value != null && prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString() != string.Empty)
                                _SSSale.MySpecialDiscountAmount = Convert.ToDouble(prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString());
                            _SSSale.LastStockID = mlastsaleid;
                            returnVal = _SSSale.AddProductDetailsSSSpecialSale();
                            if (returnVal == false)
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                    returnVal = false;
                }
                return returnVal;
            }
        }

        private bool AddPreviousRows()
        {
            {
                bool returnVal = false;
                _SSSale.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in dgtemp.Rows)
                    {
                        if (prodrow.Cells["Temp_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0)
                        {
                            _SSSale.SerialNumber += 1;
                            int mqty = 0;
                            double mpurrate = 0;
                            double mtraderate = 0;
                            double mmrp = 0;
                            double msalerate = 0;
                            double mvatper = 0;
                            double mamt = 0;
                            double mvatamt = 0;
                            string mlastsaleid = "";
                            double mpakn = 0;

                            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                            _SSSale.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                            _SSSale.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
                            if (prodrow.Cells["Temp_ExpiryDate"].Value != null)
                                _SSSale.ExpiryDate = prodrow.Cells["Temp_ExpiryDate"].Value.ToString();
                            int.TryParse(prodrow.Cells["Temp_Quantity"].Value.ToString().Trim(), out mqty);
                            _SSSale.Quantity = mqty;
                            if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            _SSSale.PurchaseRate = mpurrate;
                            if (prodrow.Cells["Temp_TradeRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_TradeRate"].Value.ToString(), out mtraderate);
                            _SSSale.TradeRate = mtraderate;
                            double.TryParse(prodrow.Cells["Temp_MRP"].Value.ToString().Trim(), out mmrp);
                            _SSSale.MRP = mmrp;
                            double.TryParse(prodrow.Cells["Temp_SaleRate"].Value.ToString().Trim(), out msalerate);
                            _SSSale.SaleRate = msalerate;
                            double.TryParse(prodrow.Cells["Temp_VATPer"].Value.ToString().Trim(), out mvatper);
                            _SSSale.VATPer = mvatper;
                            double.TryParse(prodrow.Cells["Temp_VATAmount"].Value.ToString().Trim(), out mvatamt);
                            _SSSale.VATAmount = mvatamt;
                            double.TryParse(prodrow.Cells["Temp_Amount"].Value.ToString().Trim(), out mamt);
                            _SSSale.Amount = mamt;
                            if (prodrow.Cells["Temp_StockID"].Value != null)
                                mlastsaleid = (prodrow.Cells["Temp_StockID"].Value.ToString());
                            if (prodrow.Cells["Temp_UOM"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_UOM"].Value.ToString(), out mpakn);
                            _SSSale.ProfitPercentBySaleRate = Math.Round((msalerate - mpurrate) / msalerate, 4);
                            _SSSale.ProfitPercentByPurchaseRate = Math.Round((msalerate - mpurrate) / mpurrate, 4);
                            _SSSale.ProfitInRupees = Math.Round(((msalerate - mpurrate) / mpakn) * mqty, 2);
                            _SSSale.LastStockID = mlastsaleid;
                            returnVal = _SSSale.AddProductDetailsSS();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                    returnVal = false;
                }
                return returnVal;
            }
        }

        private bool AddPreviousRowsInDeleteDetail()
        {
            {
                bool returnVal = false;
                _SSSale.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in dgtemp.Rows)
                    {
                        if (prodrow.Cells["Temp_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0)
                        {
                            _SSSale.SerialNumber += 1;
                            int mqty = 0;
                            double mpurrate = 0;
                            double mtraderate = 0;
                            double mmrp = 0;
                            double msalerate = 0;
                            double mvatper = 0;
                            double mamt = 0;
                            double mvatamt = 0;
                            string mlastsaleid = "";
                            double mpakn = 0;

                            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                            _SSSale.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                            _SSSale.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
                            if (prodrow.Cells["Temp_ExpiryDate"].Value != null)
                                _SSSale.ExpiryDate = prodrow.Cells["Temp_ExpiryDate"].Value.ToString();
                            int.TryParse(prodrow.Cells["Temp_Quantity"].Value.ToString().Trim(), out mqty);
                            _SSSale.Quantity = mqty;
                            if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            _SSSale.PurchaseRate = mpurrate;
                            if (prodrow.Cells["Temp_TradeRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_TradeRate"].Value.ToString(), out mtraderate);
                            _SSSale.TradeRate = mtraderate;
                            double.TryParse(prodrow.Cells["Temp_MRP"].Value.ToString().Trim(), out mmrp);
                            _SSSale.MRP = mmrp;
                            double.TryParse(prodrow.Cells["Temp_SaleRate"].Value.ToString().Trim(), out msalerate);
                            _SSSale.SaleRate = msalerate;
                            double.TryParse(prodrow.Cells["Temp_VATPer"].Value.ToString().Trim(), out mvatper);
                            _SSSale.VATPer = mvatper;
                            double.TryParse(prodrow.Cells["Temp_VATAmount"].Value.ToString().Trim(), out mvatamt);
                            _SSSale.VATAmount = mvatamt;
                            double.TryParse(prodrow.Cells["Temp_Amount"].Value.ToString().Trim(), out mamt);
                            _SSSale.Amount = mamt;
                            if (prodrow.Cells["Temp_StockID"].Value != null)
                                mlastsaleid = (prodrow.Cells["Temp_StockID"].Value.ToString());
                            if (prodrow.Cells["Temp_UOM"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_UOM"].Value.ToString(), out mpakn);
                            _SSSale.ProfitPercentBySaleRate = Math.Round((msalerate - mpurrate) / msalerate, 4);
                            _SSSale.ProfitPercentByPurchaseRate = Math.Round((msalerate - mpurrate) / mpurrate, 4);
                            _SSSale.ProfitInRupees = Math.Round(((msalerate - mpurrate) / mpakn) * mqty, 2);
                            _SSSale.LastStockID = mlastsaleid;
                            returnVal = _SSSale.AddDeletedProductDetailsSS();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                    returnVal = false;
                }
                return returnVal;
            }
        }



        private bool AddPreviousRowsInChangedDetail()
        {
            {
                bool returnVal = false;
                _SSSale.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in dgtemp.Rows)
                    {
                        if (prodrow.Cells["Temp_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0)
                        {
                            _SSSale.SerialNumber += 1;
                            int mqty = 0;
                            double mpurrate = 0;
                            double mtraderate = 0;
                            double mmrp = 0;
                            double msalerate = 0;
                            double mvatper = 0;
                            double mamt = 0;
                            double mvatamt = 0;
                            string mlastsaleid = "";
                            double mpakn = 0;

                            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                            _SSSale.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                            _SSSale.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
                            if (prodrow.Cells["Temp_ExpiryDate"].Value != null)
                                _SSSale.ExpiryDate = prodrow.Cells["Temp_ExpiryDate"].Value.ToString();
                            int.TryParse(prodrow.Cells["Temp_Quantity"].Value.ToString().Trim(), out mqty);
                            _SSSale.Quantity = mqty;
                            if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            _SSSale.PurchaseRate = mpurrate;
                            if (prodrow.Cells["Temp_TradeRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_TradeRate"].Value.ToString(), out mtraderate);
                            _SSSale.TradeRate = mtraderate;
                            double.TryParse(prodrow.Cells["Temp_MRP"].Value.ToString().Trim(), out mmrp);
                            _SSSale.MRP = mmrp;
                            double.TryParse(prodrow.Cells["Temp_SaleRate"].Value.ToString().Trim(), out msalerate);
                            _SSSale.SaleRate = msalerate;
                            double.TryParse(prodrow.Cells["Temp_VATPer"].Value.ToString().Trim(), out mvatper);
                            _SSSale.VATPer = mvatper;
                            double.TryParse(prodrow.Cells["Temp_VATAmount"].Value.ToString().Trim(), out mvatamt);
                            _SSSale.VATAmount = mvatamt;
                            double.TryParse(prodrow.Cells["Temp_Amount"].Value.ToString().Trim(), out mamt);
                            _SSSale.Amount = mamt;
                            if (prodrow.Cells["Temp_StockID"].Value != null)
                                mlastsaleid = (prodrow.Cells["Temp_StockID"].Value.ToString());
                            if (prodrow.Cells["Temp_UOM"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_UOM"].Value.ToString(), out mpakn);
                            _SSSale.ProfitPercentBySaleRate = Math.Round((msalerate - mpurrate) / msalerate, 4);
                            _SSSale.ProfitPercentByPurchaseRate = Math.Round((msalerate - mpurrate) / mpurrate, 4);
                            _SSSale.ProfitInRupees = Math.Round(((msalerate - mpurrate) / mpakn) * mqty, 2);
                            _SSSale.LastStockID = mlastsaleid;
                            returnVal = _SSSale.AddChangedProductDetailsSS();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                    returnVal = false;
                }
                return returnVal;
            }
        }


        private bool ReducePreviousStock()
        {
            bool returnVal = false;
            string mlastsaleid;
            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0)
                    {
                        mlastsaleid = "";
                        _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        _SSSale.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        if (prodrow.Cells["Temp_StockID"].Value != null)
                            mlastsaleid = prodrow.Cells["Temp_StockID"].Value.ToString();
                        _SSSale.LastStockID = mlastsaleid;
                        string ifRecordFound = "";
                        ifRecordFound = _SSSale.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "Y")
                        {
                            returnVal = _SSSale.UpdateIntblStock();
                            if (returnVal)
                                returnVal = _SSSale.UpdateSaleStockInMasterProduct();
                            if (returnVal)
                            {
                                if (_SSSale.IfAddToShortList())
                                {
                                    Filldailyshortlist();
                                }
                            }
                            else
                                break;
                        }
                    }
                }
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
                double mdiscper = 0;
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
                double.TryParse(txtDiscPercent.Text, out mdiscper);
                _SSSale.CrdbDiscPer = mdiscper;
                double.TryParse(txtDiscAmount.Text, out mdiscamount);
                _SSSale.CrdbDiscAmt = mdiscamount;
                double.TryParse(txtBillAmount.Text, out mbillamount);
                _SSSale.CrdbAmountNet = mbillamount;
                double.TryParse(txtRoundAmount.Text, out mround);
                _SSSale.CrdbRoundAmount = mround;
                double.TryParse(txtAddOn.Text, out maddon);
                _SSSale.CrdbAddOn = maddon;

                mdebit = Math.Round(mbillamount - Math.Round(mvat5per, 2) - Math.Round(mvat12point5per, 2) + mdiscamount - maddon - mround - mamtforzerovat + mcreditnoteamt - mdebitnoteamt, 2);

                if (mamtforzerovat > 0)
                //{
                //    _SSSale.DebitAccount = FixAccounts.AccountVATZeroSale;
                //    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                //        _SSSale.CreditAccount = FixAccounts.AccountCash;
                //    else
                //        _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                //    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    _SSSale.DebitAmount = 0;
                //    _SSSale.CreditAmount = mamtforzerovat;
                //    retValue = _SSSale.AddVoucherIntblTrnac();

                }

                if (Math.Round(mvat5per, 2) > 0)
                {
                    //_SSSale.DebitAccount = FixAccounts.AccountVatOutput6Sale;
                    //if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                    //    _SSSale.CreditAccount = FixAccounts.AccountCash;
                    //else
                    //    _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    //_SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    //_SSSale.DebitAmount = 0;
                    //_SSSale.CreditAmount = Math.Round(mvat5per, 2);
                    //retValue = _SSSale.AddVoucherIntblTrnac();

                }
                if (Math.Round(mvat12point5per, 2) > 0)
                {
                    //_SSSale.DebitAccount = FixAccounts.AccountVatOutput13point5Sale;
                    //if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                    //    _SSSale.CreditAccount = FixAccounts.AccountCash;
                    //else
                    //    _SSSale.CreditAccount = FixAccounts.AccountPendingCashBills;
                    //_SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    //_SSSale.DebitAmount = 0;
                    //_SSSale.CreditAmount = Math.Round(mvat12point5per, 2);
                    //retValue = _SSSale.AddVoucherIntblTrnac();
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
                        _SSSale.DebitAccount = FixAccounts.AccountCreditSale;
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
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCashSale;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountCreditSale;
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
        private bool ReduceStockIntblStock()
        {
            bool returnVal = false;
            string mlastsaleid;
            try
            {
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        mlastsaleid = "";
                        _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                        _SSSale.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                        _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                        if (prodrow.Cells["Col_StockID"].Value != null)
                            mlastsaleid = prodrow.Cells["Col_StockID"].Value.ToString();
                        _SSSale.LastStockID = mlastsaleid;
                        string ifRecordFound = "";
                        ifRecordFound = _SSSale.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "Y")
                        {
                            returnVal = _SSSale.UpdateIntblStock();
                            if (returnVal)
                                returnVal = _SSSale.UpdateSaleStockInMasterProduct();
                            if (returnVal)
                            {
                                if (_SSSale.IfAddToShortList())
                                {
                                    Filldailyshortlist();
                                }
                            }
                            else
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;

        }



        private bool UpdateClosingStockinCache()
        {
            bool returnVal = true;
            //try
            //{
            //    foreach (DataGridViewRow prodrow in mpPVC1.Rows)
            //    {
            //        if (prodrow.Cells["Col_ProductID"].Value != null && prodrow.Cells["Col_ProductID"].Value.ToString() != string.Empty)
            //        {
            //            _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();                       
            //            PharmaSYSDistributorPlusCache.RefreshProductData(_SSSale.ProductID);
            //        }
            //    }

            //    foreach (DataGridViewRow prodrow in dgtemp.Rows)
            //    {
            //        if (prodrow.Cells["Temp_ProductID"].Value != null && prodrow.Cells["Temp_ProductID"].Value.ToString() != string.Empty)
            //        {
            //            _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();                       
            //            PharmaSYSDistributorPlusCache.RefreshProductData(_SSSale.ProductID);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.WriteException(ex);
            //    returnVal = false;
            //}
            return returnVal;
        }
        private bool AddPreviousStock()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0)
                    {
                        _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        _SSSale.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _SSSale.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value);
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _SSSale.LastStockID = prodrow.Cells["Temp_StockID"].Value.ToString();

                        string ifRecordFound = "";
                        ifRecordFound = _SSSale.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "Y")
                            returnVal = _SSSale.UpdateIntblStockAdd();
                        returnVal = _SSSale.UpdateDebtorSaleStockInMasterProductAddFromTemp();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }



        #endregion

        # region Internal methods
        private void InitialisempPVC1(string vmode)
        {
            try
            {
                ConstructMainColumns();
                ConstructProductSelectionListGridColumns();
                ConstructBatchGridColumns();
                ConstructmpMSVC1Columns();

                // FormatGrids();

                pnlDebtorProduct.Visible = false;

                DataTable dtable = new DataTable();
                if (vmode == "C")
                {
                    dtable = _SSSale.ReadProductDetailsByIDForChanged();
                    headerLabel1.Text = "Bill Reprint => Changed Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else if (vmode == "D")
                {
                    dtable = _SSSale.ReadProductDetailsByIDForDeleted();
                    headerLabel1.Text = "Bill Reprint => Deleted Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else
                    dtable = _SSSale.ReadProductDetailsByID();// dtable = _SSSale.ReadProductDetailsByIDSpecialSale();
                mpPVC1.DataSourceMain = dtable;


                if (_Mode != OperationMode.View)
                {
                    Product prod = new Product();
                    dtable = prod.GetOverviewData();
                    mpPVC1.DataSourceProductList = dtable;
                }
                // mpPVC1.DataSourceProductList = General.ProductList;
                //string tempFileName = General.get
                if (_Mode == OperationMode.Add)
                {
                    mpPVC1.DataSourceMain = null;
                    mpPVC1.Rows.Clear();
                    mpPVC1.Bind();
                    mpPVC1.IsAllowNewRow = true;
                    if (_SSSale.AddNewRowCheck(mpPVC1))
                        mpPVC1.Rows.Add(1);

                    mpPVC1.AddRowsInStockTempTable();
                    CalculateAmount(-1);
                    // CalculateAllAmounts();
                }
                else
                    mpPVC1.Bind();

                if (_Mode == OperationMode.Edit)
                {
                    if (AddNewRowCheck())
                        mpPVC1.Rows.Add();
                }

                mpPVC1.ClearSelection();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FormatGrids()
        {
            mpPVC1.BatchGridShowColumnName = "Col_UOM";
            mpPVC1.NewRowColumnName = "Col_Quantity";
            mpPVC1.DoubleColumnNames.Add("Col_MRP");
            mpPVC1.NumericColumnNames.Add("Col_Quantity");
            mpPVC1.DoubleColumnNames.Add("Col_VATPer");
            mpPVC1.DoubleColumnNames.Add("Col_PurchaseRate");
            mpPVC1.DoubleColumnNames.Add("Col_Amount");
            mpPVC1.DoubleColumnNames.Add("Col_SaleRate");
            mpPVC1.ProductGridClosingStockColumnName = "Col_ClosingStock";
            mpPVC1.MainGridSoldQuantityColumnName = "Col_Quantity";
            mpPVC1.ClearSelection();
        }

        private void BindTempGrid()
        {
            try
            {
                ConstructTempGridColumns();
                DataTable tmptable = new DataTable();
                tmptable = _SSSale.ReadProductDetailsByID();
                _BindingSource = tmptable;
                dgtemp.DataSource = _BindingSource;
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
                mpPVC1.BatchListGridWidth = 690;
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtNarration.Clear();
                txtVouchernumber.Clear();
                mpPVC1.ShowBatchWithZeroStock = true;
                txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
                _SSSale.CrdbVouType = txtVouType.Text.ToString();
                _SSSale.SaleSubType = FixAccounts.SubTypeForPatientSale;
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtTotalAmount.Text = "0.00";
                txtAmountRcvd.Text = "0.00";
                txtAmountBalance.Text = "0.00";
                txtAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtdiscountAmount5.Text = "0.00";
                txtDiscountAmount12point5.Text = "0.00";
                txtTotalafterdiscount.Text = "0.00";
                txtAddOn.Text = "0.00";
                txtVatInput12point5per.Text = "0.00";
                txtVatInput5per.Text = "0.00";
                txtAmountforZeroVAT.Text = "0.00";
                txtTotalAmountForDiscount.Text = "0.00";
                txtRoundAmount.Text = "0.00";
                txtNoOfRows.Text = "";

                mcbCreditor.Text = "";
                mcbCreditor.SelectedID = "";
                mcbDoctor.Text = "";
                txtDoctorAddress.Text = "";
                mcbDoctor.SelectedID = "";
                mcbPrescription.SelectedID = "";
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
                    //  txtPatient.Enabled = true;
                    mcbDoctor.Enabled = true;
                    txtVouchernumber.ReadOnly = true;
                    txtVouchernumber.Enabled = false;
                    txtAmountRcvd.Enabled = true;
                    txtAmountBalance.Enabled = true;
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

                pnlClone.Visible = false;
                txtclonevouno.Text = "";
                cbclonevoutype.Text = "";
                InitializeScreen();
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

                pnlCenter.BringToFront();
                pnlDebtorProduct.Visible = false;
                mpMSVCFill.Visible = false;
                pnlCenter.Dock = DockStyle.Fill;
                mpPVC1.Dock = DockStyle.Fill;
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
        private void FillTxtAddress()
        {
            try
            {
                SetAddressToControl(txtAddress1);
                SetAddressToControl(txtDoctorAddress);

                Area _Area = new Area();
                DataTable dtable = _Area.GetOverViewDataForAddress();

                txtAddress1.FillData(dtable);
                txtDoctorAddress.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void SetAddressToControl(PSAutoSuggestTextBox txtAddress)
        {
            try
            {
                txtAddress.SelectedID = null;
                txtAddress.SourceDataString = new string[2] { "AreaID", "AreaName" };
                txtAddress.ColumnWidth = new string[2] { "0", "300" };
                txtAddress.ValueColumnNo = 0;
                txtAddress.UserControlToShow = new UclArea();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
                //mcbCreditor.UserControlToShow = new UclPatient();
                //Patient _Party = new Patient();
                //DataTable dtable = _Party.GetOverviewData();
                //mcbCreditor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillMainGridwithMultipleBatch(int requiredqty, string productID)
        {
            // why here
            int mmaingridrowIndex = 0;
            DataTable stkdt = new DataTable();
            Stock prodstk = new Stock();
            int mycolindex = 0;
            int msalestk = requiredqty;
            int mactualclosingstock = 0;

            if (mpPVC1.Rows.Count > 0)
                mmaingridrowIndex = mpPVC1.MainDataGridCurrentRow.Index;

            stkdt = prodstk.GetStockByProductIDForSale(productID);

            foreach (DataRow dtrow in stkdt.Rows)
            {
                if (dtrow["ClosingStock"] != DBNull.Value)
                    mactualclosingstock += Convert.ToInt32(dtrow["ClosingStock"].ToString());
            }
            try
            {
                //ss here
                foreach (DataRow dtrow in stkdt.Rows)
                {
                    int mbatchstock = 0;
                    int mactualsalestock = 0;
                    double msalerate = 0;
                    int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock);
                    mactualsalestock = Math.Min(mbatchstock, msalestk);
                    if (mactualsalestock > 0 && msalestk > 0 && mactualclosingstock > 0)
                    {
                        string mbtno = "";
                        double mmrp = 0;
                        //   string ifbatchfoundindr1 = "";
                        mbtno = dtrow["BatchNumber"].ToString();
                        double.TryParse(dtrow["MRP"].ToString(), out mmrp);
                        mycolindex = 0;


                        mycolindex = mmaingridrowIndex;

                        mpPVC1.Rows[mycolindex].Cells["Col_ProductID"].Value = dtrow["ProductID"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].Value = dtrow["ProdName"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_UOM"].Value = dtrow["ProdLoosePack"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_Pack"].Value = dtrow["ProdPack"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_ProdCompShortName"].Value = dtrow["ProdCompShortName"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_Shelf"].Value = dtrow["ShelfCode"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_BatchNumber"].Value = dtrow["BatchNumber"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_Expiry"].Value = dtrow["Expiry"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_MRP"].Value = Convert.ToDouble(dtrow["MRP"].ToString()).ToString("#0.00");
                        mpPVC1.Rows[mycolindex].Cells["Col_SaleRate"].Value = Convert.ToDouble(dtrow["SaleRate"].ToString()).ToString("#0.00");
                        double.TryParse(dtrow["SaleRate"].ToString(), out msalerate);
                        mpPVC1.Rows[mycolindex].Cells["Col_Quantity"].Value = Math.Min(mactualsalestock, mbatchstock);
                        double mamt = 0;
                        mamt = Math.Round(msalerate * mactualsalestock, 2);
                        mpPVC1.Rows[mycolindex].Cells["Col_Amount"].Value = Convert.ToDouble(mamt.ToString()).ToString("#0.00");
                        mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = mpPVC1.Rows[mycolindex].Cells["Col_Quantity"].Value;
                        mpPVC1.Rows[mycolindex].Cells["Col_ClosingStock"].Value = dtrow["ProdClosingStock"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_PurchaseRate"].Value = dtrow["PurchaseRate"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_ExpiryDate"].Value = dtrow["ExpiryDate"].ToString();
                        // mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = 0;
                        mpPVC1.Rows[mycolindex].Cells["Col_VATPer"].Value = Convert.ToDouble(dtrow["ProdVATPercent"].ToString()).ToString("#0.00");
                        mpPVC1.Rows[mycolindex].Cells["Col_TradeRate"].Value = dtrow["TradeRate"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_StockID"].Value = dtrow["StockID"].ToString();
                        int mclstkdr1 = 0;
                        int.TryParse(dtrow["ClosingStock"].ToString(), out mclstkdr1);
                        mpPVC1.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;
                        msalestk = msalestk - mactualsalestock;
                        mactualclosingstock -= mactualsalestock;
                        CalculateAmount(-1);
                        if (msalestk > 0 && mactualclosingstock > 0)
                        {
                            mpPVC1.Rows.Add();
                            mpPVC1.SetFocus(mpPVC1.Rows.Count - 1, 11);
                            mmaingridrowIndex = mmaingridrowIndex + 1;
                        }
                    }
                }
                mpPVC1.IsAllowNewRow = true;
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

        private void FillPrescription()
        {
            try
            {
                mcbPrescription.SelectedID = null;
                mcbPrescription.SourceDataString = new string[2] { "prescriptionID", "PrescriptionName" };
                mcbPrescription.ColumnWidth = new string[2] { "0", "200" };
                mcbPrescription.ValueColumnNo = 0;
                mcbPrescription.UserControlToShow = new UclPrescription();
                Prescription _Prescription = new Prescription();
                DataTable dtabled = _Prescription.GetOverviewData();
                mcbPrescription.FillData(dtabled);
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

        private void CalculateAmount(int deletedrowindex)
        {
            double TotalAmount = 0;
            double TotalAmountforDiscount = 0;
            double mvatper = 0;
            double mvatamount5 = 0;
            double mvatamount12point5 = 0;
            double mtotvat5 = 0;
            double mtotvat12point5 = 0;
            double mtotamtvat0 = 0;
            double mtotamtvat5 = 0;
            double mtotamtvat12 = 0;
            double mrate = 0;
            double mamt = 0;
            double mpakn = 0;
            double mqty = 0;
            int itemCount = 0;
            string ifdiscount = "Y";
            //   _SSSale.AmountByPurchaseRate = 0;
            //  double mpuramt = 0;
            double mprate = 0;
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Index != deletedrowindex)
                    {
                        if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "0" && dr.Cells["Col_Quantity"].Value.ToString() != "")
                        {
                            ifdiscount = "Y";
                            mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                            mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                            mpakn = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());

                            mprate = 0;
                            if (dr.Cells["Col_PurchaseRate"].Value != null && (dr.Cells["Col_PurchaseRate"].Value.ToString() != ""))
                                mprate = Convert.ToDouble(dr.Cells["Col_PurchaseRate"].Value.ToString());


                            if (dr.Cells["Col_IfSaleDisc"].Value != null)
                                ifdiscount = dr.Cells["Col_IFSaleDisc"].Value.ToString();
                            if (Math.Truncate(mqty / mpakn) == (mqty / mpakn))
                                mamt = Math.Round((mqty / mpakn) * mrate, 2);
                            else
                            {
                                mamt = Math.Round(Math.Round(mrate / mpakn, 4) * mqty, 2);
                                //if (Math.Round(mamt, 1) - mamt < 0.05)
                                //    mamt = Math.Round(mamt, 1);
                            }
                            dr.Cells["Col_Amount"].Value = (mamt).ToString("#0.00");
                            if (mamt > 0)
                            {
                                mvatamount12point5 = 0;
                                mvatamount5 = 0;
                                mvatper = Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString());
                                // vat 5.5
                                if (mvatper == 6)
                                {
                                    mvatamount5 = Math.Round((mamt) / (100 / mvatper), 4);
                                    mtotamtvat5 += mamt;
                                }
                                else if (mvatper == 13.5)
                                {
                                    mvatamount12point5 = Math.Round((mamt) / (100 / mvatper), 4);
                                    mtotamtvat12 += mamt;
                                }
                                else
                                    mtotamtvat0 += mamt;
                                dr.Cells["Col_VATAmount"].Value = mvatamount12point5 + mvatamount5;

                                //   mpuramt = Math.Round(Math.Round(mprate / mpakn, 2) * mqty, 2) + mvatamount5 + mvatamount12point5;
                                //mpuramt = Math.Round(Math.Round(mprate / mpakn, 2) * mqty, 2);
                                //_SSSale.AmountByPurchaseRate += mpuramt;

                                TotalAmount += mamt;
                                if (ifdiscount != "N")
                                    TotalAmountforDiscount += mamt;
                                mtotvat12point5 += mvatamount12point5;
                                mtotvat5 += mvatamount5;
                                itemCount += 1;
                            }
                        }
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtVatInput12point5per.Text = Math.Round(mtotvat12point5, 4).ToString("#0.0000");
                txtVatInput5per.Text = Math.Round(mtotvat5, 4).ToString("#0.0000");
                txtAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");
                txtAmountforZeroVAT.Text = mtotamtvat0.ToString("#0.00");
                txtTotalAmountForDiscount.Text = Math.Round(TotalAmountforDiscount, 2).ToString("#0.00");
                txtAmountVAT5Per.Text = mtotamtvat5.ToString();
                txtAmountVAT12Point5Per.Text = mtotamtvat12.ToString();
                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void CalculateAmountForDiscount()
        {
            double TotalAmount = 0;
            double TotalAmountforDiscount = 0;
            double mvatper = 0;
            double mvatamount5 = 0;
            double mvatamount12point5 = 0;
            double mtotvat5 = 0;
            double mtotvat12point5 = 0;
            double mtotamtvat0 = 0;
            double mtotamtvat5 = 0;
            double mtotamtvat12 = 0;
            double mrate = 0;
            double mamt = 0;
            double mpakn = 0;
            double mqty = 0;
            int itemCount = 0;
            string ifdiscount = "Y";
            //   _SSSale.AmountByPurchaseRate = 0;
            //  double mpuramt = 0;
            double mprate = 0;
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "0" && dr.Cells["Col_Quantity"].Value.ToString() != "")
                    {
                        ifdiscount = "Y";
                        mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                        mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                        mpakn = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());

                        mprate = 0;
                        if (dr.Cells["Col_PurchaseRate"].Value != null && (dr.Cells["Col_PurchaseRate"].Value.ToString() != ""))
                            mprate = Convert.ToDouble(dr.Cells["Col_PurchaseRate"].Value.ToString());

                        if (dr.Cells["Col_IfSaleDisc"].Value != null)
                            ifdiscount = dr.Cells["Col_IFSaleDisc"].Value.ToString();
                        if (Math.Truncate(mqty / mpakn) == (mqty / mpakn))
                            mamt = Math.Round((mqty / mpakn) * mrate, 2);
                        else
                        {
                            mamt = Math.Round(Math.Round(mrate / mpakn, 4) * mqty, 2);
                            //if (Math.Round(mamt, 1) - mamt < 0.05)
                            //    mamt = Math.Round(mamt, 1);
                        }
                        dr.Cells["Col_Amount"].Value = (mamt).ToString("#0.00");
                        if (mamt > 0)
                        {
                            mvatamount12point5 = 0;
                            mvatamount5 = 0;
                            mvatper = Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString());
                            // vat 5.5
                            if (mvatper == 6)
                            {
                                mvatamount5 = Math.Round((mamt) / (100 / mvatper), 4);
                                mtotamtvat5 += mamt;
                            }
                            else if (mvatper == 13.5)
                            {
                                mvatamount12point5 = Math.Round((mamt) / (100 / mvatper), 4);
                                mtotamtvat12 += mamt;
                            }
                            else
                                mtotamtvat0 += mamt;
                            dr.Cells["Col_VATAmount"].Value = mvatamount12point5 + mvatamount5;

                            //   mpuramt = Math.Round(Math.Round(mprate / mpakn, 2) * mqty, 2) + mvatamount5 + mvatamount12point5;
                            //mpuramt = Math.Round(Math.Round(mprate / mpakn, 2) * mqty, 2);
                            //_SSSale.AmountByPurchaseRate += mpuramt;

                            TotalAmount += mamt;
                            if (ifdiscount != "N")
                                TotalAmountforDiscount += mamt;
                            mtotvat12point5 += mvatamount12point5;
                            mtotvat5 += mvatamount5;
                            itemCount += 1;
                        }
                    }
                }
                //txtNoOfRows.Text = itemCount.ToString().Trim();
                //txtVatInput12point5per.Text = Math.Round(mtotvat12point5, 4).ToString("#0.0000");
                //txtVatInput5per.Text = Math.Round(mtotvat5, 4).ToString("#0.0000");
                //txtAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");
                //txtAmountforZeroVAT.Text = mtotamtvat0.ToString("#0.00");
                txtTotalAmountForDiscount.Text = Math.Round(TotalAmountforDiscount, 2).ToString("#0.00");
                //txtAmountVAT5Per.Text = mtotamtvat5.ToString();
                //txtAmountVAT12Point5Per.Text = mtotamtvat12.ToString();
                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void CalculateAllAmounts()
        {
            double mdblAmount;
            double maddon = 0;
            double mdebitnote = 0;
            double mtotamt = 0;
            double mamtrcvd = 0;
            double mamtbalance = 0;
            double.TryParse(txtAmount.Text.ToString(), out mdblAmount);
            double mdblAmountforDiscount;
            double.TryParse(txtTotalAmountForDiscount.Text.ToString(), out mdblAmountforDiscount);
            double.TryParse(txtAddOn.Text.ToString(), out maddon);
            double mdblDiscPer;
            double.TryParse(txtDiscPercent.Text.ToString(), out mdblDiscPer);
            double mdblDiscAmount;
            double.TryParse(txtDiscAmount.Text.ToString(), out mdblDiscAmount);
            try
            {
                mdblDiscAmount = Math.Round(((mdblAmountforDiscount) * mdblDiscPer / 100), 2);
                txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                mtotamt = Math.Round(mdblAmount - mdblDiscAmount + maddon + mdebitnote, 2);

                txtTotalAmount.Text = mtotamt.ToString("#0.00");

                if (cbRound.Checked == true)
                {
                    txtRoundAmount.Text = Math.Round(Math.Round(Convert.ToDouble(txtTotalAmount.Text.ToString()), 0) - Math.Round(Convert.ToDouble(txtTotalAmount.Text.ToString()), 2), 2).ToString("#0.00");
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text;
                    if (_Mode == OperationMode.Add)
                    {
                        mamtrcvd = Convert.ToDouble(txtNetAmount.Text.ToString());
                        mamtbalance = 0;
                    }
                    else
                    {
                        mamtrcvd = Convert.ToDouble(txtAmountRcvd.Text.ToString());
                        mamtbalance = Convert.ToDouble(txtNetAmount.Text.ToString());
                        mamtbalance = Math.Round(mamtbalance - mamtrcvd, 2);
                    }
                    txtAmountRcvd.Text = mamtrcvd.ToString("#0.00");
                    txtAmountBalance.Text = mamtbalance.ToString("#0.00");

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
                        mamtrcvd = Convert.ToDouble(txtAmountRcvd.Text.ToString());
                        mamtbalance = Convert.ToDouble(txtNetAmount.Text.ToString());
                        mamtbalance = mamtbalance - mamtrcvd;
                    }
                    txtAmountRcvd.Text = mamtrcvd.ToString("#0.00");
                    txtAmountBalance.Text = mamtbalance.ToString("#0.00");
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
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                    {
                        itemCount += 1;
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillMainGridwithmpMSVC1()
        {
            int mmaingridrowIndex = 0;
            try
            {
                mmaingridrowIndex = mpPVC1.Rows.Count - 1;
                foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
                {
                    int mclstk = 0;
                    int mreqstk = 0;
                    int msalestk = 0;
                    if (dr2.Cells["Col_ProductID"].Value != null)
                    {
                        string mprodno = dr2.Cells["Col_ProductID"].Value.ToString().Trim();
                        if (dr2.Cells["Col_ClosingStock"].Value != null)
                            int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
                        if (dr2.Cells["Col_Quantity"].Value != null)
                            int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
                        if (dr2.Cells["Col_SQuantity"].Value != null)
                            int.TryParse(dr2.Cells["Col_SQuantity"].Value.ToString().Trim(), out msalestk);
                        if (msalestk > 0)
                        {
                            SsStock dbstk = new SsStock();
                            DataTable stkdt = new DataTable();
                            stkdt = dbstk.GetStockByProductIDForFill(mprodno);
                            foreach (DataRow dtrow in stkdt.Rows)
                            {
                                int mbatchstock = 0;
                                int mactualsalestock = 0;
                                double msalerate = 0;
                                int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock);
                                mactualsalestock = Math.Min(mbatchstock, msalestk);
                                if (mactualsalestock > 0 && msalestk > 0)
                                {
                                    string mbtno = "";
                                    double mmrp = 0;
                                    string mproddr1 = "";
                                    string mbatnodr1 = "";
                                    double mmrpdr1 = 0;
                                    int msaleQtydr1 = 0;
                                    int mbatchstkdr1 = 0;
                                    string mstockid = "";
                                    string ifbatchfoundindr1 = "";
                                    mbtno = dtrow["BatchNumber"].ToString();
                                    double.TryParse(dtrow["MRP"].ToString(), out mmrp);
                                    int mycolindex = 0;
                                    foreach (DataGridViewRow dr1 in mpPVC1.Rows)
                                    {
                                        if (dr1.Cells["Col_ProductID"].Value != null && dr1.Cells["Col_ProductID"].Value.ToString() != "")
                                        {
                                            mproddr1 = dr1.Cells["Col_ProductID"].Value.ToString();
                                            mbatnodr1 = dr1.Cells["Col_BatchNumber"].Value.ToString();
                                            double.TryParse(dr1.Cells["Col_MRP"].Value.ToString(), out mmrpdr1);
                                            int.TryParse(dr1.Cells["Col_Quantity"].Value.ToString(), out msaleQtydr1);
                                            int.TryParse(dr1.Cells["Col_BatchStock"].Value.ToString(), out mbatchstkdr1);
                                            if (dr1.Cells["Col_StockID"].Value != null)
                                                mstockid = dr1.Cells["Col_StockID"].Value.ToString();
                                            if (mprodno == mproddr1 && mbtno == mbatnodr1 && mmrp == mmrpdr1)
                                            {
                                                mycolindex = dr1.Index;
                                                ifbatchfoundindr1 = "Y";
                                                break;
                                            }
                                        }

                                    }
                                    if (ifbatchfoundindr1 == "Y")
                                    {
                                        mpPVC1.Rows[mycolindex].Cells["Col_ProductID"].Value = mprodno;
                                        mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].Value = dtrow["ProdName"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_UOM"].Value = dtrow["ProdLoosePack"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_Pack"].Value = dtrow["ProdPack"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_ProdCompShortName"].Value = dtrow["ProdCompShortName"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_IfSaleDisc"].Value = dtrow["ProdIfSaleDisc"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_Shelf"].Value = dtrow["ShelfCode"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_BatchNumber"].Value = dtrow["BatchNumber"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_Expiry"].Value = dtrow["Expiry"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_MRP"].Value = Convert.ToDouble(dtrow["MRP"].ToString()).ToString("#0.00");
                                        mpPVC1.Rows[mycolindex].Cells["Col_SaleRate"].Value = Convert.ToDouble(dtrow["SaleRate"].ToString()).ToString("#0.00");
                                        double.TryParse(dtrow["SaleRate"].ToString(), out msalerate);
                                        mpPVC1.Rows[mycolindex].Cells["Col_Quantity"].Value = Math.Min((mactualsalestock + msaleQtydr1), mbatchstkdr1);
                                        mpPVC1.Rows[mycolindex].Cells["Col_Amount"].Value = (msalerate * mactualsalestock).ToString("#0.00");
                                        mpPVC1.Rows[mycolindex].Cells["Col_ClosingStock"].Value = dtrow["ProdClosingStock"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_PurchaseRate"].Value = dtrow["PurchaseRate"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_ExpiryDate"].Value = dtrow["ExpiryDate"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = 0;
                                        mpPVC1.Rows[mycolindex].Cells["Col_VATPer"].Value = Convert.ToDouble(dtrow["ProdVATPercent"].ToString()).ToString("#0.00");
                                        mpPVC1.Rows[mycolindex].Cells["Col_TradeRate"].Value = dtrow["TradeRate"].ToString();

                                        int mclstkdr1 = 0;
                                        int.TryParse(dtrow["ClosingStock"].ToString(), out mclstkdr1);
                                        mpPVC1.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;
                                        mpPVC1.Rows[mycolindex].Cells["Col_StockID"].Value = mstockid;
                                        msalestk = msalestk - mactualsalestock;
                                        mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].ReadOnly = true;
                                    }
                                    else
                                    {
                                        mycolindex = mmaingridrowIndex;
                                        mpPVC1.Rows.Add();
                                        mmaingridrowIndex = mmaingridrowIndex + 1;
                                        mpPVC1.Rows[mycolindex].Cells["Col_ProductID"].Value = mprodno;
                                        mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].Value = dtrow["ProdName"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_UOM"].Value = dtrow["ProdLoosePack"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_Pack"].Value = dtrow["ProdPack"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_IfSaleDisc"].Value = dtrow["ProdIfSaleDisc"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_Shelf"].Value = dtrow["ShelfCode"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_BatchNumber"].Value = dtrow["BatchNumber"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_Expiry"].Value = dtrow["Expiry"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_MRP"].Value = Convert.ToDouble(dtrow["MRP"].ToString()).ToString("#0.00");
                                        mpPVC1.Rows[mycolindex].Cells["Col_SaleRate"].Value = Convert.ToDouble(dtrow["SaleRate"].ToString()).ToString("#0.00");
                                        double.TryParse(dtrow["SaleRate"].ToString(), out msalerate);
                                        mpPVC1.Rows[mycolindex].Cells["Col_Quantity"].Value = Math.Min(mactualsalestock, mbatchstock);
                                        double mamt = 0;
                                        mamt = Math.Round(msalerate * mactualsalestock, 2);
                                        mpPVC1.Rows[mycolindex].Cells["Col_Amount"].Value = Convert.ToDouble(mamt.ToString()).ToString("#0.00");
                                        mpPVC1.Rows[mycolindex].Cells["Col_ClosingStock"].Value = dtrow["ProdClosingStock"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_PurchaseRate"].Value = dtrow["PurchaseRate"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_ExpiryDate"].Value = dtrow["ExpiryDate"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = 0;
                                        mpPVC1.Rows[mycolindex].Cells["Col_VATPer"].Value = Convert.ToDouble(dtrow["ProdVATPercent"].ToString()).ToString("#0.00");
                                        mpPVC1.Rows[mycolindex].Cells["Col_TradeRate"].Value = dtrow["TradeRate"].ToString();
                                        mpPVC1.Rows[mycolindex].Cells["Col_StockID"].Value = dtrow["StockID"].ToString();
                                        int mclstkdr1 = 0;
                                        int.TryParse(dtrow["ClosingStock"].ToString(), out mclstkdr1);
                                        mpPVC1.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;
                                        msalestk = msalestk - mactualsalestock;
                                        mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].ReadOnly = true;
                                    }
                                }
                            }
                        }
                    }
                }
                //mpPVC1.AddRowsInStockTempTable();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        private bool FillLastSaleDataFromMasterProduct()
        {
            bool retValue = false;
            DataRow dr = null;
            DataRow invdr = null;
            string mprodno = "";
            int mclosingstock = 0;
            string mexpirydate = "";
            string mexpiry = "";
            double mmrpn = 0;
            double mpurrate = 0;
            double mtraderate = 0;
            double msalerate = 0;
            string mlastsalestockid = "";
            string mbatchno = "";

            try
            {
                Product drprod = new Product();
                dr = drprod.ReadLastSaleByID(mpPVC1.MainDataGridCurrentRow.Cells[0].Value.ToString());
                mprodno = mpPVC1.MainDataGridCurrentRow.Cells[0].Value.ToString();
                if (dr != null)
                {
                    if (dr["ProdLastSaleStockID"] != null && dr["ProdLastSaleStockID"].ToString() != "")
                    {
                        mlastsalestockid = dr["ProdLastSaleStockID"].ToString();
                        if (mlastsalestockid != "")
                        {
                            SsStock invss = new SsStock();
                            invdr = invss.GetStockByStockID(mlastsalestockid);
                        }

                        if (invdr != null)
                        {
                            int.TryParse(invdr["ClosingStock"].ToString().Trim(), out mclosingstock);

                            //if (mclosingstock > 0)
                            //{
                            mexpiry = invdr["Expiry"].ToString().Trim();
                            mexpirydate = invdr["ExpiryDate"].ToString().Trim();
                            double.TryParse(invdr["MRP"].ToString().Trim(), out mmrpn);
                            double.TryParse(invdr["SaleRate"].ToString().Trim(), out msalerate);
                            double.TryParse(invdr["PurchaseRate"].ToString().Trim(), out mpurrate);
                            double.TryParse(invdr["TradeRate"].ToString().Trim(), out mtraderate);
                            mbatchno = invdr["BatchNumber"].ToString().Trim();
                            //}
                        }
                        retValue = true;
                    }
                }
                else
                    retValue = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private void ClearSummarySection()
        {
            try
            {
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtAmountBalance.Text = "0.00";
                txtAmountRcvd.Text = "0.00";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public override bool Print()
        {
            bool retValue = true;
            if (_SSSale.CrdbAmountNet > 0)
            {
                if (General.PharmaSYSDistributorPlusLicense.LicenseType == DistributorLicenseLib.LicenseTypes.Full)
                {
                    if (General.CurrentSetting.MsetPrintSaleBill == "Y")
                        PrintSaleBillPrePrintedPaper();
                    else
                        PrintSaleBillPlainPaper();
                }
                else
                {
                    PSDialogResult result;
                    result = PSMessageBox.Show("Trial License", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                }
            }

            return retValue;
        }
        private void PrintSaleBillPlainPaper()
        {
            PharmaSYSDistributorPlus.Printing.PlainPaperPrinter printer = new PharmaSYSDistributorPlus.Printing.PlainPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "", _SSSale.MobileNumberForSMS, _SSSale.DoctorName, _SSSale.DoctorAddress, mpPVC1.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount, "");

        }
        private void PrintSaleBillPrePrintedPaper()
        {
            PharmaSYSDistributorPlus.Printing.PrePrintedPaperPrinter printer = new PharmaSYSDistributorPlus.Printing.PrePrintedPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "", _SSSale.MobileNumberForSMS, _SSSale.DoctorName, _SSSale.DoctorAddress, mpPVC1.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount, "");
        }

        private void InitializeCheckBoxes()
        {
            try
            {
                cbFill.Checked = false;
                cbFill.Enabled = true;
                cbEditRate.Checked = false;
                cbEditRate.Enabled = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        #region Construct columns

        // sheela 14/11/2016 start
        private void ConstructMainColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC1.ColumnsMain.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 250;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "BatchNo";
                column.Width = 100;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 70;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 55;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 114;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "OldQty";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "IfSaleDisc";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "LastStockID";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";
                column.DataPropertyName = "ProfitPercentBySaleRate";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";
                column.DataPropertyName = "ProfitPercentByPurchaseRate";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRupees";
                column.DataPropertyName = "ProfitInRupees";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MySpecialDiscountAmount";
                column.DataPropertyName = "MySpecialDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFTempSale";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_RatePerUnit";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfMultipleMRP";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        // sheela 14/11/2016  end

        private void ConstructTempGridColumns()
        {
            DataGridViewTextBoxColumn column;
            dgtemp.Columns.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 200;
                column.ReadOnly = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "BatchNo";
                column.Width = 90;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 50;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 70;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Purchase Rate";
                column.Width = 70;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 95;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                dgtemp.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_StockID";
                column.DataPropertyName = "StockID";
                column.Visible = false;
                dgtemp.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        // sheela 14/11/2016 start
        private void ConstructProductSelectionListGridColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC1.ColumnsProductList.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 250;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "ShelfID";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                column.ReadOnly = true;

                mpPVC1.ColumnsProductList.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Cl.Stk";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "Disc";
                column.Width = 40;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "ProdLastPurchaseRate";
                column.HeaderText = "PurRate";
                column.Width = 30;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfShortListed";
                column.DataPropertyName = "ProdIfShortListed";
                column.HeaderText = "Short";
                column.Width = 30;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MaxLevel";
                column.DataPropertyName = "ProdMaxLevel";
                column.HeaderText = "MaxLevel";
                column.Width = 30;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "ProdLastSaleStockID";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);


                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_StockID";
                ////    column.DataPropertyName = "ProdLastSaleStockID";
                //column.HeaderText = "laststockid";
                //column.Width = 30;
                //column.Visible = false;
                //mpPVC1.ColumnsProductList.Add(column);
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.HeaderText = "Sch";
                column.Width = 30;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Visible = true;
                mpPVC1.ColumnsProductList.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GenericCategoryName";
                column.DataPropertyName = "GenericCategoryName";
                column.HeaderText = "GenericCategoryName";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        private void ConstructBatchGridColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC1.ColumnsBatchList.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batchno";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batchno";
                column.Width = 130;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPercent";
                column.DataPropertyName = "ProductVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);

                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                mpPVC1.ColumnsBatchList.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistSaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 100;
                column.Visible = false;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "PurRate";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                mpPVC1.ColumnsBatchList.Add(column);

                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Cl.STK";
                column.Width = 65;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStockpack";
                column.DataPropertyName = "ClosingStockPack";
                column.HeaderText = "Cl.STK";
                column.Visible = false;
                column.Width = 65;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyShortName";
                column.DataPropertyName = "AccShortName";
                column.HeaderText = "";
                column.Width = 45;
                mpPVC1.ColumnsBatchList.Add(column);

                //7               
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        // sheela 14/11/2016 end
        private void ConstructmpMSVC1Columns()
        {
            DataGridViewTextBoxColumn column;
            mpMSVCFill.ColumnsMain.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                mpMSVCFill.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 180;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                mpMSVCFill.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Cl.tock";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Required.Qty";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SQuantity";
                column.HeaderText = "Sale.Qty";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVCFill.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                    //   txtPatient.Text = mcbCreditor.SeletedItem.ItemData[4];
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
                    // txtPatient.Text = "";
                    txtDiscPercent.Text = "";
                }
                else if (mcbCreditor.SeletedItem != null)
                {

                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[2];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[3];
                    //   txtPatient.Text = mcbCreditor.SeletedItem.ItemData[4];
                    double mdis = 0;
                    txtDiscPercent.Text = "";
                    if (mcbCreditor.SeletedItem.ItemData[8] != null && mcbCreditor.SeletedItem.ItemData[8].ToString() != "")
                        mdis = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[8].ToString());
                    txtDiscPercent.Text = mdis.ToString("#0.00");
                    if (_Mode == OperationMode.Add)
                    {
                        if (mcbCreditor.SeletedItem.ItemData[6] != null)
                        {

                            _SSSale.DocID = mcbCreditor.SeletedItem.ItemData[6];
                            mcbDoctor.SelectedID = _SSSale.DocID;
                            if (mcbDoctor.SeletedItem.ItemData[2] != null)
                                txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2].ToString();
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



        private void mpPVC1_OnSelectedProductClosingStock(int closingStockValue, string productID)
        {
            int mqty = 0;
            if (_Mode == OperationMode.Add)
            {
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);
                if (closingStockValue + mqty == 0)
                {
                    _SSSale.ProductID = productID;

                    if (_SSSale.IfAddToShortList())
                    {
                        Filldailyshortlist();
                    }
                    lblFooterMessage.Text = "No Stock";
                    mpPVC1.SetFocus(mpPVC1.MainDataGridCurrentRow.Index, 1);
                }
                else
                {
                    lblFooterMessage.Text = string.Format("Stock: {0}", closingStockValue + mqty);
                }
            }
        }

        private void mpPVC1_OnProductSelected(DataGridViewRow productRow)
        {
            _SSSale.IFMultipleMRP = "N";
            double mprate = 0;
            int mclstk = 0;
            string mifshortlisted = "";
            string mifsalediscount = "Y";
            int mqty = 0;
            string mlastsalestockid = "";
            string mprodno = "";
            try
            {
                mpPVC1.MainDataGridCurrentRow.Cells[0].Value = productRow.Cells[0].Value;
                _SSSale.ProductID = productRow.Cells[0].Value.ToString();
                mprodno = _SSSale.ProductID;
                double.TryParse(productRow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                _SSSale.PurchaseRate = mprate;
                int.TryParse(productRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);
                _SSSale.Closingstock = mclstk;
                mifshortlisted = productRow.Cells["Col_IfShortListed"].Value.ToString().Trim();
                if (productRow.Cells["Col_IfSaleDisc"].Value != null && productRow.Cells["Col_IfSaleDisc"].Value.ToString() != "")
                    mifsalediscount = productRow.Cells["Col_IfSaleDisc"].Value.ToString();
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = productRow.Cells["Col_ProductName"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value = productRow.Cells["Col_UOM"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Pack"].Value = productRow.Cells["Col_Pack"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = productRow.Cells["Col_ProdCompShortName"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Shelf"].Value = productRow.Cells["Col_Shelf"].Value.ToString();
                mpPVC1.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = Convert.ToDouble(productRow.Cells["Col_VATPer"].Value.ToString()).ToString("#0.00");
                mpPVC1.MainDataGridCurrentRow.Cells["Col_LastStockID"].Value = productRow.Cells["Col_LastStockID"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_IfSaleDisc"].Value = mifsalediscount.ToString();

                if (_Mode == OperationMode.Add)
                {
                    if (productRow.Cells["Col_LastStockID"].Value != null)
                        mlastsalestockid = productRow.Cells["Col_LastStockID"].Value.ToString();
                }
                else
                    mlastsalestockid = mprodno;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value = productRow.Cells["Col_ClosingStock"].Value;

                mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].ReadOnly = true;
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);
                else
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = 0;

                int currentrow = mpPVC1.MainDataGridCurrentRow.Index;
                int totproductsale = 0;
                int saleqty = 0;

                int tempstock = 0;

                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Index != currentrow && dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                    {
                        if (dr.Cells["Col_Quantity"].Value != null)
                            int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out saleqty);
                        totproductsale += saleqty;

                    }
                }
                if (_Mode == OperationMode.Edit)
                {
                    foreach (DataGridViewRow dr in dgtemp.Rows)
                    {
                        if (dr.Cells["Temp_ProductID"].Value != null && dr.Cells["Temp_ProductID"].Value.ToString() == mprodno)
                        {
                            if (dr.Cells["Temp_Quantity"].Value != null)
                                int.TryParse(dr.Cells["Temp_Quantity"].Value.ToString(), out saleqty);
                            tempstock += saleqty;

                        }
                    }
                }

                mclstk = mclstk + mqty;

                //TODO: Check if No batch then remove product

                //if (mclstk == 0 && mifshortlisted != "N" && mqty == 0)
                //{
                //    //lblFooterMessage.Text = "No Stock";
                //    //Filldailyshortlist();
                //    //mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = null;
                //    //mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value = null;
                //    //mpPVC1.MainDataGridCurrentRow.Cells["Col_Pack"].Value = null;
                //    //mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = null;
                //    //mpPVC1.MainDataGridCurrentRow.Cells["Col_Shelf"].Value = null;
                //    //mpPVC1.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = null;
                //    //mpPVC1.MainDataGridCurrentRow.Cells[0].Value = null;
                //    //mpPVC1.RefreshMe();
                //    //mpPVC1.SetFocus(1);
                //}
                //else
                //{
                lblFooterMessage.Text = "Product Stock :" + mclstk.ToString() + " : ";
                bool iffilllastsale = false;
                try
                {
                    if (mprodno != "")
                        iffilllastsale = FillLastSaleDataFromMasterProduct();
                }
                catch (Exception ex) { Log.WriteError(ex.ToString()); }
                if (General.CurrentSetting.MsetSaleAllowNegativeStock == "Y")
                {
                    mpPVC1.ColumnsMain["Col_BatchNumber"].ReadOnly = false;
                    mpPVC1.ColumnsMain["Col_Expiry"].ReadOnly = false;
                    mpPVC1.ColumnsMain["Col_SaleRate"].ReadOnly = false;
                    mpPVC1.SetFocus(6);
                }
                else
                {
                    if (iffilllastsale)
                    {
                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = false;
                        mpPVC1.SetFocus(11);
                    }
                    else
                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
                }
               

                //}
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }
        private void mpPVC1_OnBatchSelected(DataGridViewRow batchRow)
        {
            if (_Mode != OperationMode.View)
            {
                int mclosingstock = 0;
                string mexpirydate = "";
                string mexpiry = "";
                double mmrpn = 0;
                double mpurrate = 0;
                double mtraderate = 0;
                double msalerate = 0;
                int mclstk = 0;
                string mprodno = "";
                int mqty = 0;
                string mlastsalestockid = "";
                try
                {
                    mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                    mexpiry = batchRow.Cells["Col_Expiry"].Value.ToString().Trim();
                    mexpirydate = batchRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                    double.TryParse(batchRow.Cells["Col_MRP"].Value.ToString().Trim(), out mmrpn);
                    double.TryParse(batchRow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                    double.TryParse(batchRow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                    double.TryParse(batchRow.Cells["Col_TradeRate"].Value.ToString().Trim(), out mtraderate);
                    int.TryParse(batchRow.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclosingstock);
                    mlastsalestockid = batchRow.Cells["Col_StockID"].Value.ToString();

                    mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = batchRow.Cells["Col_Batchno"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = mexpiry;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value = mmrpn.ToString("#0.00");
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = msalerate.ToString("#0.00");
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value = mlastsalestockid;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_PurchaseRate"].Value = mpurrate.ToString("#0.00");
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value = mtraderate.ToString("#0.00");
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                        int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);

                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value != null)
                        int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = batchRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value = batchRow.Cells["Col_ClosingStock"].Value.ToString().Trim();
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].ReadOnly = true;
                    string mdt = DateTime.Today.Date.ToString("yyyyMMdd");

                    //TOCHECK
                    // checked ss

                    if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                    {
                        lblFooterMessage.Text = "Expired Product";
                        PSMessageBox.Show("Product Expired", "Invalid expiry date", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                        mpPVC1.Rows.Remove(mpPVC1.MainDataGridCurrentRow);
                        int currentindex = 0;

                        if (AddNewRowCheck())
                        {
                            int mindex = mpPVC1.Rows.Add();
                            mpPVC1.SetFocus(mindex, 1);
                        }
                        else
                            mpPVC1.SetFocus(currentindex, 1);
                    }
                    else
                    {
                        lblFooterMessage.Text = "";
                        int currentrow = mpPVC1.MainDataGridCurrentRow.Index;
                        int totbatchsale = 0;
                        int totproductsale = 0;
                        int saleqty = 0;
                        int tempproductstock = 0;
                        int tempbatchstock = 0;

                        foreach (DataGridViewRow dr in mpPVC1.Rows)
                        {
                            if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                            {
                                if (dr.Index != currentrow)
                                {
                                    if (dr.Cells["Col_Quantity"].Value != null)
                                        int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out saleqty);
                                    totproductsale += saleqty;
                                    if (dr.Cells["Col_StockID"].Value.ToString().Trim() == mlastsalestockid)
                                    {
                                        totbatchsale += saleqty;
                                    }
                                }
                            }
                        }
                        if (_Mode == OperationMode.Edit)
                        {

                            foreach (DataGridViewRow dr in dgtemp.Rows)
                            {
                                if (dr.Cells["Temp_ProductID"].Value != null && dr.Cells["Temp_ProductID"].Value.ToString() == mprodno)
                                {
                                    if (dr.Cells["Temp_Quantity"].Value != null)
                                        int.TryParse(dr.Cells["Temp_Quantity"].Value.ToString(), out saleqty);
                                    tempproductstock += saleqty;
                                    if (dr.Cells["Temp_StockID"].Value.ToString().Trim() == mlastsalestockid)
                                    {
                                        if (dr.Cells["Temp_Quantity"].Value != null)
                                            int.TryParse(dr.Cells["Temp_Quantity"].Value.ToString(), out saleqty);
                                        tempbatchstock += saleqty;
                                    }

                                }
                            }
                        }

                        mclstk = mclstk + mqty;

                        mclosingstock = mclosingstock - totbatchsale;



                        lblFooterMessage.Text = "Product Stock :" + mclstk.ToString().Trim() + " : Batch Stock :" + mclosingstock.ToString().Trim();
                        _SSSale.CurrentProductStock = mclstk;
                        _SSSale.CurrentBatchStock = mclosingstock;

                        //if (_SSSale.CurrentBatchStock <= 0)
                        //{
                        //    lblFooterMessage.Text = "Batch Stock Zero";
                        //    mpPVC1.SetFocus(1);
                        //}
                        //else
                        //{
                        if (cbEditRate.Checked == true)
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = false;
                            mpPVC1.SetFocus(10);
                        }
                        else
                            mpPVC1.SetFocus(11);
                        //}
                    }
                }

                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());
                }
            }
            else
                mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
        }

        public bool AddNewRowCheck()
        {
            bool retValue = true;

            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == string.Empty)
                    {
                        retValue = false;
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void mpPVC1_OnRowDeleted(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow deletedrow = (DataGridViewRow)sender;
                int deletedrowindex = deletedrow.Index;
                CalculateAmount(deletedrowindex);
                lblFooterMessage.Text = "";
                //string ifblankrow = "N";
                //foreach (DataGridViewRow dr in mpPVC1.Rows)
                //{
                //    if (dr.Cells["Col_ProductID"].Value == null || dr.Cells["Col_ProductID"].Value.ToString() == "")
                //    {
                //        ifblankrow = "Y";
                //        break;
                //    }
                //}
                //if (ifblankrow == "N")
                if (AddNewRowCheck())
                    mpPVC1.Rows.Add();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }
        //private void mpPVC1_OnCellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    int colIndex = e.ColumnIndex;
        //    mpPVC1CellValueChanged(colIndex);
        //}

        private void mpPVC1_OnCellValueChangeCommited(int colIndex)
        {
            mpPVC1CellValueChanged(colIndex);
        }

        private void mpPVC1CellValueChanged(int colIndex)
        {
            int requiredQty = 0;
            double mmrp = 0;
            double mrate = 0;
            int mqty = 0;
            int mpakn = 1;
            string mbtno = "";
            string mprodno = "";
            int mcurrentindex = 0;
            int oldmqty = 0;
            string prodname = "";
            string mexpirydate = "";
            try
            {
                if (colIndex == 1)
                {
                    _preID = "";
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                        _preID = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value != null)
                        prodname = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString();
                    if (prodname != "" && _preID != "")
                    {
                        prodname = General.GetProductName(_preID);
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = prodname;
                    }
                }
                if (colIndex == 8)  // Expiry
                {
                    string newexpiry = "";
                    string newexpirydate = "";
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null)
                    {
                        int explength = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim().Length;
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim() != "" && explength > 0)
                        {
                            newexpiry = General.GetValidExpiry(mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString());
                            if (newexpiry != "")
                            {
                                bool ifexp = CheckValidExpiry(newexpiry);

                                newexpirydate = General.GetValidExpiryDate(newexpiry.ToString());
                                newexpirydate = General.GetExpiryInyyyymmddForm(newexpirydate);
                                string mdt = DateTime.Today.Date.ToString("yyyyMMdd");

                                if (newexpirydate != "" && Convert.ToInt32(newexpirydate) < Convert.ToInt32(mdt))
                                {
                                    lblFooterMessage.Text = "Expired Product..";
                                    ifexp = false;
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = true;
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = true;
                                }
                                else
                                {
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = false;
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                                    if (ifexp)
                                    {
                                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry.ToString();
                                        newexpirydate = General.GetValidExpiryDate(newexpiry);
                                        newexpirydate = General.GetExpiryInyyyymmddForm(newexpirydate);
                                        mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                                        lblFooterMessage.Text = "";
                                        lblRightSideFooterMsg.Text = "";
                                    }
                                    else
                                    {
                                        lblFooterMessage.Text = "Check Expiry ";
                                        mpPVC1.SetFocus(5);
                                    }
                                }
                            }
                            else
                            {
                                mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                                lblFooterMessage.Text = " No Expiry ";
                                mpPVC1.SetFocus(5);
                            }

                        }
                        else
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                            lblFooterMessage.Text = " No Expiry ";
                            mpPVC1.SetFocus(5);
                        }
                    }
                }
                if (colIndex == 11)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString())) == true)
                        mpPVC1.IsAllowNewRow = false;
                    else
                    {
                        string mdt = DateTime.Today.Date.ToString("yyyyMMdd");
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value != null)
                            mexpirydate = mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                        if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                        {
                            lblFooterMessage.Text = "Expired Product";
                            PSMessageBox.Show("Product Expired", "Invalid expiry date", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = 0;
                            mpPVC1.IsAllowNewRow = false;
                            mpPVC1.SetFocus(11);
                        }
                        else
                        {
                            requiredQty = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                            //if (requiredQty <= 0 || _SSSale.CurrentBatchStock == 0)
                            //{
                            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString() == "0")
                            {
                                int minq = 0;
                                _SSSale.ProdPakn = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
                                minq = _SSSale.ProdPakn;
                                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = minq.ToString("#0");
                                requiredQty = minq;
                            }
                            if (Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) <= _SSSale.CurrentBatchStock)
                                mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.ForeColor = Color.Black;

                            if (requiredQty <= 0)
                            {
                                lblFooterMessage.Text = "Enter Quantity";
                                mpPVC1.SetFocus(11);
                                mpPVC1.IsAllowNewRow = false;
                            }
                            //else
                            //{
                            //    lblFooterMessage.Text = "Batch Stock Zero";
                            //    mpPVC1.SetFocus(11);
                            //    mpPVC1.IsAllowNewRow = false;
                            //}
                            //}
                            //else if (requiredQty > _SSSale.CurrentBatchStock && _Mode == OperationMode.Edit)
                            //{
                            //    lblFooterMessage.Text = "Enter Correct Quantity";
                            //    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = _SSSale.CurrentBatchStock;
                            //    mpPVC1.SetFocus(11);
                            //    mpPVC1.IsAllowNewRow = false;
                            //    CalculateAmount(-1);
                            //}
                            else
                            {
                                int mbatchstock = 0;
                                int oldQuantity = 0;
                                //   int gridstock = 0;
                                string mstockid = "";
                                //   int gridprodstock = 0;
                                mprodno = "";
                                mqty = 0;
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                                    mprodno = (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString());
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value != null)
                                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value.ToString().Trim(), out mbatchstock);
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value != null)
                                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString().Trim(), out oldQuantity);
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                                    mstockid = (mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString());

                                lblFooterMessage.Text = "";


                                //if (requiredQty <= _SSSale.CurrentBatchStock)
                                //{
                                FillBatchStock(ref mmrp, ref mrate, ref mpakn, ref mbtno, ref mprodno, ref mcurrentindex, ref oldmqty, ref mqty);
                                mpPVC1.IsAllowNewRow = true;
                                //if (_Mode == OperationMode.Add)
                                //{
                                //    WriteToXML();
                                //}
                                //}
                                //else if (_Mode == OperationMode.Add)
                                //{

                                //    if ((requiredQty > _SSSale.CurrentProductStock + oldQuantity - gridstock) || gridprodstock > 0)
                                //    {
                                //        lblFooterMessage.Text = "Enter Correct Quantity";
                                //        mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = _SSSale.CurrentBatchStock;
                                //        mpPVC1.IsAllowNewRow = false;
                                //        CalculateAmount(-1);
                                //    }
                                //    else
                                //    {

                                //        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                                //            mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();

                                //        FillMainGridwithMultipleBatch(requiredQty, mprodno);
                                //        CalculateAmount(-1);
                                //    }

                                //if (_Mode == OperationMode.Add)
                                //{
                                //    WriteToXML();
                                //}

                                //}
                                //else
                                //{
                                //    lblFooterMessage.Text = "Enter Correct Quantity";
                                //    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = _SSSale.CurrentBatchStock;
                                //    mpPVC1.IsAllowNewRow = false;
                                //    CalculateAmount(-1);
                                //}

                            }
                        }
                        
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value)) == false && Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) != 0)
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = true;
                }
                if (colIndex == 10)
                {
                    if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value) <= 0)
                    {
                        lblFooterMessage.Text = "Enter SaleRate";
                        mpPVC1.SetFocus(10);
                        mpPVC1.IsAllowNewRow = false;
                    }
                    else if (Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) > 0)
                    {
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value = (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value) * Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value)).ToString();
                        CalculateAmount(-1);
                        mpPVC1.IsAllowNewRow = true;
                    }
                }


                if (colIndex == 7)
                {
                    string newexpiry = "";
                    string newexpirydate = "";
                    int explength = 0;
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null)
                        explength = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim().Length;
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim() != "" && explength > 0)
                    {
                        newexpiry = General.GetValidExpiry(mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString());
                        if (newexpiry != "")
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry.ToString();
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                            lblFooterMessage.Text = "";
                        }
                        else
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                            lblFooterMessage.Text = " No Expiry ";
                        }

                    }
                    else
                    {
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                        lblFooterMessage.Text = " No Expiry ";
                    }
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool CheckValidExpiry(string newexp)
        {
            bool ifexp = false;
            string exp = "";
            //    string expdate = "";
            try
            {
                if (newexp == "0000")
                    newexp = "00/00";
                if (newexp != "00/00")
                {
                    exp = General.GetValidExpiry(newexp);

                    if (exp == "")
                    {
                        lblFooterMessage.Text = "Please Check Expiry";
                        ifexp = false;
                    }
                    else
                    {
                        ifexp = true;

                        //expdate = General.GetValidExpiryDate(exp);
                        //string mexpdate = General.GetExpiryInyyyymmddForm(expdate);
                        //DateTime dd = General.ConvertStringToDateyyyyMMdd(mexpdate);
                        //TimeSpan tt = dd.Subtract(DateTime.Now.Date);
                        //int days = tt.Days;
                    }

                }
                else
                {
                    if (General.CurrentSetting.MsetGeneralExpiryDateReuired != "Y")
                    {
                        ifexp = true;
                        lblFooterMessage.Text = "";

                    }
                    else
                    {
                        ifexp = false;
                        lblFooterMessage.Text = "Please Check Expiry";

                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return ifexp;
        }
        private void WriteToXML()
        {
            //DataTable dt = mpPVC1.GetGridData();
            //if (dt.Rows.Count > 0)
            //    dt.WriteXml(General.GetPatientSaleTempFile());
        }

        private void FillBatchStock(ref double mmrp, ref double mrate, ref int mpakn, ref string mbtno, ref string mprodno, ref int mcurrentindex, ref int oldmqty, ref int mqty)
        {
            mcurrentindex = mpPVC1.MainDataGridCurrentRow.Index;
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                mbtno = mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim();
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                double.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                mqty = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
            foreach (DataGridViewRow drp in mpPVC1.Rows)
            {
                if (drp.Cells["Col_ProductID"].Value != null &&
                      drp.Cells["Col_BatchNumber"].Value != null &&
                         drp.Cells["Col_MRP"].Value != null)
                {
                    if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno &&
                          drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno &&
                             drp.Cells["Col_MRP"].Value.ToString().Trim() == mmrp.ToString("#0.00") && drp.Index != mcurrentindex)
                    {
                        if (drp.Cells["Col_Quantity"].Value != null)
                            oldmqty = Convert.ToInt32(drp.Cells["Col_Quantity"].Value.ToString());
                        oldmqty += mqty;
                        drp.Cells["Col_Quantity"].Value = oldmqty;
                        mpPVC1.Rows.Remove(mpPVC1.MainDataGridCurrentRow);
                        break;
                    }
                }
            }

            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value != null)
                mpakn = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value != null)
                double.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value.ToString(), out mrate);
            mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value = ((mrate / mpakn) * mqty).ToString("#0.00");
            CalculateAmount(-1);
        }

        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtCash_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtCash.Checked == true)
                {
                    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
                    txtVouType.Text = _SSSale.CrdbVouType;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {

        }
        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                //  txtPatient.Text = mcbCreditor.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
                if (General.CurrentUser.Level <= 1)
                {
                    cbEditRate.Enabled = true;
                }
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "" && (mcbCreditor.SeletedItem.ItemData[1].ToString() != mcbCreditor.Text.ToString()))
                {
                    mcbCreditor.SeletedItem.ItemData[0] = "";
                    mcbCreditor.SelectedID = "";
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    //txtPatient.Text = "";
                }
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "" && (mcbCreditor.SeletedItem.ItemData[1].ToString() == mcbCreditor.Text.ToString()))
                {
                    cbFill.Enabled = true;
                    //txtPatient.ReadOnly = true;
                    //txtAddress1.ReadOnly = true;
                    //txtAddress2.ReadOnly = true;
                    if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "")
                        mpPVC1.SetFocus(1);
                    else
                        mcbDoctor.Focus();
                }
                else
                {
                    cbFill.Enabled = false;
                    //txtPatient.ReadOnly = false;
                    //txtAddress1.ReadOnly = false;
                    //txtAddress2.ReadOnly = false;                   
                }
                txtAddress1.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbDoctor_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtAddress1.Focus();
        }
        private void txtDoctorAddress_UpArrowKeyPressed(object sender, EventArgs e)
        {
            mcbDoctor.Focus();
        }
        private void txtAddOn_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    CalculateAmount(-1);
                    txtAmountRcvd.Focus();
                    break;
                case Keys.Up:
                    CalculateAmount(-1);
                    txtDiscAmount.Focus();
                    break;
            }
        }

        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        CalculateAmountForDiscount();
                        txtAddOn.Focus();
                        break;
                    case Keys.Down:
                        txtAddOn.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void cbFill_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbFill.Checked == true && mcbCreditor.SelectedID != "")
                {
                 //   Patient dbprod = new Patient();
                    DataTable dt = new DataTable();
                  //  dt = dbprod.ReadProductDetailsById(mcbCreditor.SelectedID);

                    int cnt = dt.Rows.Count;
                    if (cnt > 0)
                        txtNoOfProducts.Text = cnt.ToString();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cbFill.Enabled = false;
                        pnlCenter.SendToBack();
                        pnlCenter.Enabled = false;
                        pnlTotals.Enabled = false;
                        pnlDebtorProduct.Location = GetpnlDebtorProductLocation();
                        pnlDebtorProduct.Width = 614;
                        pnlDebtorProduct.Height = 325;
                        pnlDebtorProduct.Visible = true;

                        mpMSVCFill.Visible = true;
                        mpMSVCFill.Dock = DockStyle.Fill;
                        ConstructmpMSVC1Columns();

                        mpMSVCFill.DataSourceMain = dt;
                        Product prod = new Product();
                        DataTable dtable = prod.GetOverviewData();
                        mpPVC1.DataSourceProductList = dtable;
                        // mpPVC1.DataSourceProductList = General.ProductList;
                        mpMSVCFill.DataSource = dtable;

                        mpMSVCFill.NumericColumnNames.Add("Col_ClosingStock");
                        mpMSVCFill.NumericColumnNames.Add("Col_Quantity");
                        mpMSVCFill.NumericColumnNames.Add("Col_SQuantity");
                        mpMSVCFill.DoubleColumnNames.Add("Col_VATPer");

                        mpMSVCFill.Bind();

                        int cntstock = 0;
                        foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
                        {
                            int mclstk = 0;
                            int mreqstk = 0;
                            int msalestk = 0;
                            if (dr2.Cells["Col_ClosingStock"].Value != null)
                                int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
                            if (dr2.Cells["Col_Quantity"].Value != null)
                                int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
                            msalestk = Math.Min(mclstk, mreqstk);
                            if (msalestk > 0)
                                cntstock += 1;
                            if (dr2.Cells["Col_ProductID"].Value != null)
                            {
                                dr2.Cells["Col_SQuantity"].Value = msalestk;
                            }

                        }
                        txtStockInProducts.Text = cntstock.ToString();
                    }
                    else
                        lblFooterMessage.Text = "No Data Available for the Debtor.";
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private Point GetpnlDebtorProductLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlCenter.Location.X + 300;
                pt.Y = pnlCenter.Location.Y + 10;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private Point GetpnlDebitCreditNoteLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlCenter.Location.X + 80;
                pt.Y = pnlCenter.Location.Y + 80;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        private Point GetmpMSVC1Location()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlDebtorProduct.Location.X + 4;
                pt.Y = pnlDebtorProduct.Location.Y + 3;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDebtorProduct.Visible = false;
                mpMSVCFill.ColumnsMain.Clear();
                cbFill.Enabled = true;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ChangeRateinGrid()
        {
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells["Col_ProductID"].Value != null)
                    {
                        if (dr.Cells["Col_PurchaseRate"].Value != null)
                            dr.Cells["Col_SaleRate"].Value = dr.Cells["Col_PurchaseRate"].Value.ToString();
                    }
                }
                CalculateAmount(-1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }



        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (mcbDoctor.SeletedItem != null && string.IsNullOrEmpty(Convert.ToString(mcbDoctor.SelectedID)) == false && mcbDoctor.SeletedItem.ItemData[2] != null)
                {
                    txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2].ToString();
                    //   txtDoctorShortName.Text = mcbDoctor.SeletedItem.ItemData[3].ToString();
                }
                else txtDoctorAddress.Text = string.Empty;

                if (string.IsNullOrEmpty(txtDoctorAddress.Text) == true)
                    txtDoctorAddress.Focus();
                else
                    mpPVC1.SetFocus(1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void txtDoctorAddress_EnterKeyPressed(object sender, EventArgs e)
        {
            mpPVC1.SetFocus(1);
        }

        private void datePickerBillDate_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (txtDiscPercent.Visible)
                {
                    txtDiscPercent.Focus();
                }
                else
                {
                    MainToolStrip.Select();
                    tsBtnSave.Select();
                }
            }
        }
        private void btnOKFill_Click(object sender, EventArgs e)
        {
            btnOKFillClick();
        }

        private void btnCanelFill_Click(object sender, EventArgs e)
        {
            btnCancelFillClick();
        }

        private void btnOKFill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKFillClick();
        }

        private void btnCanelFill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnCancelFillClick();
        }

        private void btnOKFillClick()
        {

            try
            {
                foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
                {
                    int mclstk = 0;
                    int mreqstk = 0;
                    int msalestk = 0;
                    if (dr2.Cells["Col_ClosingStock"].Value != null)
                        int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
                    if (dr2.Cells["Col_Quantity"].Value != null)
                        int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
                    if (dr2.Cells["Col_SQuantity"].Value != null)
                        int.TryParse(dr2.Cells["Col_SQuantity"].Value.ToString().Trim(), out msalestk);
                    if (msalestk > mclstk)
                    {
                        msalestk = mclstk;
                        dr2.Cells["Col_SQuantity"].Value = msalestk;
                    }

                }

                FillMainGridwithmpMSVC1();
                pnlDebtorProduct.Visible = false;
                pnlCenter.BringToFront();
                pnlCenter.Enabled = true;
                pnlTotals.Enabled = true;
                CalculateAmount(-1);
                mpPVC1.SetFocus(1);
                txtNarration.Focus();
                mpPVC1.SetFocus(mpPVC1.Rows.Count - 1, 1);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void btnCancelFillClick()
        {
            try
            {
                pnlDebtorProduct.Visible = false;
                pnlCenter.BringToFront();
                pnlCenter.Enabled = true;
                cbFill.Enabled = true;
                cbFill.Checked = false;
                mcbPrescription.SelectedID = "";
                CalculateAmount(-1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbCreditor.Focus();
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
        private void txtAddress1_EnterKeyPressed(object sender, EventArgs e)
        {
            txtAddress2.Focus();
        }
        private void txtAddress2_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbDoctor.Focus();
        }
        private void txtAddress1_UpArrowKeyPressed(object sender, EventArgs e)
        {
            mcbCreditor.Focus();
        }
        private void txtAddress2_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtAddress1.Focus();
        }
        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    mcbDoctor.Focus();
                    break;
                case Keys.Up:
                    txtAddress1.Focus();
                    break;
                    //case Keys.Down:
                    //    txtPatient.Focus();
                    //    break;
            }

        }

        //private void txtAddress1_TextChanged(object sender, EventArgs e)
        //{
        //    txtPatient.Text = mcbCreditor.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
        //}

        private void txtPatient_KeyDown(object sender, KeyEventArgs e)
        {

        }



        private void mcbPrescription_SeletectIndexChanged(object sender, EventArgs e)
        {
            McbPrescriptionSelected();
        }

        private void mcbPrescription_EnterKeyPressed(object sender, EventArgs e)
        {

            McbPrescriptionSelected();
        }

        private void McbPrescriptionSelected()
        {
            try
            {
                if (mcbPrescription.SelectedID != null && mcbPrescription.SelectedID != "")
                {
                    Prescription dbpres = new Prescription();
                    DataTable dt = new DataTable();
                    dt = dbpres.ReadProductDetailsById(mcbPrescription.SelectedID);
                    int cnt = dt.Rows.Count;
                    if (cnt > 0)
                        txtNoOfProducts.Text = cnt.ToString();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        pnlCenter.SendToBack();
                        pnlCenter.Enabled = false;
                        pnlDebtorProduct.Location = GetpnlDebtorProductLocation();
                        pnlDebtorProduct.Visible = true;
                        pnlDebtorProduct.Width = 614;
                        pnlDebtorProduct.Height = 325;

                        mpMSVCFill.Visible = true;
                        mpMSVCFill.Dock = DockStyle.Fill;
                        ConstructmpMSVC1Columns();

                        mpMSVCFill.DataSourceMain = dt;

                        Product prod = new Product();
                        DataTable dtable = prod.GetOverviewData();

                        mpMSVCFill.DataSource = dtable;

                        mpMSVCFill.NumericColumnNames.Add("Col_ClosingStock");
                        mpMSVCFill.NumericColumnNames.Add("Col_Quantity");
                        mpMSVCFill.NumericColumnNames.Add("Col_SQuantity");
                        mpMSVCFill.DoubleColumnNames.Add("Col_VATPer");
                        mpMSVCFill.Bind();
                        int cntstock = 0;
                        foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
                        {
                            int mclstk = 0;
                            int mreqstk = 0;
                            int msalestk = 0;
                            if (dr2.Cells["Col_ClosingStock"].Value != null)
                                int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
                            if (dr2.Cells["Col_Quantity"].Value != null)
                                int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
                            msalestk = Math.Min(mclstk, mreqstk);
                            if (msalestk > 0)
                                cntstock += 1;
                            if (dr2.Cells["Col_ProductID"].Value != null)
                            {
                                dr2.Cells["Col_SQuantity"].Value = msalestk;
                            }

                        }
                        txtStockInProducts.Text = cntstock.ToString();
                    }
                    else
                        lblFooterMessage.Text = "No Data Available for the Debtor.";
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void btnSubstitute_Click(object sender, EventArgs e)
        {
            if (uclSubstituteControl1.Visible == false)
            {
                uclSubstituteControl1.Initialize();
                uclSubstituteControl1.Visible = true;
                uclSubstituteControl1.BringToFront();
            }
            else
            {
                uclSubstituteControl1.Visible = false;
                uclSubstituteControl1.SendToBack();
            }
        }
        private void uclSubstituteControl1_OnProductSelected(string productID)
        {
            mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = productID;

            //search for productID in datasourceproduct list and send that row  to mppvc1_onproductselected                      

        }
        private void mpMSVCFill_OnCellValueChangeCommited(int colIndex)
        {
            try
            {
                if (colIndex == 6)
                {
                    int mclstk = 0;
                    int mreqstk = 0;
                    int msalestk = 0;
                    if (mpMSVCFill.MainDataGridCurrentRow.Cells[4].Value != null)
                        int.TryParse(mpMSVCFill.MainDataGridCurrentRow.Cells[4].Value.ToString().Trim(), out mclstk);
                    if (mpMSVCFill.MainDataGridCurrentRow.Cells[5].Value != null)
                        int.TryParse(mpMSVCFill.MainDataGridCurrentRow.Cells[5].Value.ToString().Trim(), out mreqstk);
                    if (mpMSVCFill.MainDataGridCurrentRow.Cells[6].Value != null)
                        int.TryParse(mpMSVCFill.MainDataGridCurrentRow.Cells[6].Value.ToString().Trim(), out msalestk);
                    if (msalestk > mclstk)
                    {
                        msalestk = Math.Min(mclstk, mreqstk);
                        mpMSVCFill.MainDataGridCurrentRow.Cells[6].Value = msalestk;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                CalculateAllAmounts();
        }
        private void mpPVC1_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            General.ProdID = "";
        }

        private void mpPVC1_OnRowAdded(object sender, EventArgs e)
        {
            try
            {
                // RefreshProductGrid();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {

        }
        #endregion

        #region UIEvents

        private void txtclonevouno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbclonevoutype.Focus();
        }

        private void btncloneOK_Click(object sender, EventArgs e)
        {
            btnOKCloneClick();
        }
        private void btncloneOK_KeyDown(object sender, KeyEventArgs e)
        {
            btnOKCloneClick();
        }
        private void btnOKCloneClick()
        {
            pnlClone.Visible = false;
            Product dbprod = new Product();
            DataTable dt = new DataTable();
            DataRow dr;
            int clonevouno = 0;
            string clonevoutype = "";
            string cloneSaleID = "";
            if (txtclonevouno.Text != null && txtclonevouno.Text.ToString() != "")
                int.TryParse(txtclonevouno.Text.ToString(), out clonevouno);
            if (cbclonevoutype.Text != null)
                clonevoutype = cbclonevoutype.Text.ToString();
            try
            {
                dr = _SSSale.GetSaleIDforClone(clonevouno, clonevoutype);
                if (dr != null)
                {
                    if (dr["ID"] != DBNull.Value)
                        cloneSaleID = dr["ID"].ToString();
                    dt = _SSSale.ReadProductDetailsByCloneID(cloneSaleID);

                    int cnt = dt.Rows.Count;
                    if (cnt > 0)
                        txtNoOfProducts.Text = cnt.ToString();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cbFill.Enabled = false;
                        pnlCenter.SendToBack();
                        pnlCenter.Enabled = false;
                        pnlTotals.Enabled = false;
                        pnlDebtorProduct.Location = GetpnlDebtorProductLocation();
                        pnlDebtorProduct.Width = 614;
                        pnlDebtorProduct.Height = 325;
                        pnlDebtorProduct.Visible = true;

                        mpMSVCFill.Visible = true;
                        mpMSVCFill.Dock = DockStyle.Fill;
                        ConstructmpMSVC1Columns();

                        mpMSVCFill.DataSourceMain = dt;

                        Product prod = new Product();
                        DataTable dtable = prod.GetOverviewData();

                        mpMSVCFill.DataSource = dtable;

                        mpMSVCFill.NumericColumnNames.Add("Col_ClosingStock");
                        mpMSVCFill.NumericColumnNames.Add("Col_Quantity");
                        mpMSVCFill.NumericColumnNames.Add("Col_SQuantity");
                        mpMSVCFill.DoubleColumnNames.Add("Col_VATPer");

                        mpMSVCFill.Bind();
                        mpMSVCFill.SetFocus(7);

                        int cntstock = 0;
                        foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
                        {
                            int mclstk = 0;
                            int mreqstk = 0;
                            int msalestk = 0;
                            if (dr2.Cells["Col_ClosingStock"].Value != null)
                                int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
                            if (dr2.Cells["Col_Quantity"].Value != null)
                                int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
                            msalestk = Math.Min(mclstk, mreqstk);
                            if (msalestk > 0)
                                cntstock += 1;
                            if (dr2.Cells["Col_ProductID"].Value != null)
                            {
                                dr2.Cells["Col_SQuantity"].Value = msalestk;
                            }

                        }
                        txtStockInProducts.Text = cntstock.ToString();
                        mpMSVCFill.SetFocus(7);
                    }
                    else
                        lblFooterMessage.Text = "No Data Available for the Debtor.";
                }
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            pnlClone.Visible = true;
            txtclonevouno.Focus();
        }
        private void cbclonevoutype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKCloneClick();
        }

        private void txtAmountRcvd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _SSSale.CrdbAmountClear = 0;
                _SSSale.CrdbAmountNet = 0;
                if (txtNetAmount.Text != null && txtNetAmount.Text.ToString() != "")
                    _SSSale.CrdbAmountNet = Convert.ToDouble(txtNetAmount.Text.ToString());
                if (txtAmountRcvd.Text != null && txtAmountRcvd.Text.ToString() != "")
                    _SSSale.CrdbAmountClear = Convert.ToDouble(txtAmountRcvd.Text.ToString());
                txtAmountRcvd.Text = _SSSale.CrdbAmountClear.ToString("#0.00");
                _SSSale.CrdbAmountBalance = _SSSale.CrdbAmountNet - _SSSale.CrdbAmountClear;
                txtAmountBalance.Text = _SSSale.CrdbAmountBalance.ToString("#0.00");
                MainToolStrip.Select();
                tsBtnSave.Select();
            }
        }

        private void mpPVC1_OnShiftTABKeyPressed(object sender, EventArgs e)
        {
            mcbDoctor.Focus();

        }
        //private void mpPVC1_OnRowAdded(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        RefreshProductGrid();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}
        private DataRow mpPVC1_OnProductBarCodeScaned(string scanCode)
        {
            return GetProductNameFromScanCode(scanCode);
        }

        private DataRow GetProductNameFromScanCode(string scanCode)
        {
            DataRow dr = null;
            try
            {
                dr = _SSSale.GetProductNameFromScanCode(scanCode);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dr;
        }
        private void txtNarration_KeyDown_1(object sender, KeyEventArgs e)
        {
            txtDiscPercent.Focus();
        }

        #endregion UIEvents
    }
}