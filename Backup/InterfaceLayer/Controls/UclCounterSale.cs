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
    public partial class UclCounterSale : BaseControl
    {
        #region Declaration
        private SSSale _SSSale;
        private TempStock _TempStock;
        string _lastCustIdSelected = "1";
        List<DataGridViewRow> rowCollection;
        string _preID = "";
        public PSProductViewControl ActiveDataGrid;
        public string _IfNewDoctor = "N";
        #endregion

        #region Constructor
        public UclCounterSale()
        {
            InitializeComponent();
            _SSSale = new SSSale();
            _TempStock = new TempStock();
            SearchControl = new UclCounterSaleSearch();
        }
        #endregion

        # region IDetail Control
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }
        public override void SetFocus()
        {
            mpPVC1.SetFocus(1);
        }

        public override bool ClearData()
        {
            EnableAllGrids();
            DeleteRecordsForSelectedNumber();
            ClearTotals();
            return true;
        }

        private void EnableAllGrids()
        {
            ActiveDataGrid.Enabled = true;
            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
        }
        private void UclCounterSale_Load(object sender, EventArgs e)
        {
            ClearAllTotals();
            headerLabel1.Text = "COUNTER SALE -> NEW";
            pnlCustomerNumber.Enabled = true;
            pnlPatientDrDetails.Enabled = false;
            pnlBillAmount.Enabled = false;
            pnlFinal.Enabled = true;
            txtsavecustno.Enabled = true;
            btnPrint.Visible = false;
            InitialisempPVC1();
            lblMessage.Text = "";
            SetGridRowColour();
            ActiveDataGrid = mpPVC1;
            txtsavecustno.Text = "1";
            ActiveDataGrid.BringToFront();
            ChangeBackColour(ActiveDataGrid);
        }

        private void SetGridRowColour()
        {
            if (mpPVC1.Rows.Count > 0)
                mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn1.BackColor;

            if (mpPVC2.Rows.Count > 0)
                mpPVC2.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn2.BackColor;

            if (mpPVC3.Rows.Count > 0)
                mpPVC3.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn3.BackColor;

            if (mpPVC4.Rows.Count > 0)
                mpPVC4.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn4.BackColor;

            if (mpPVC5.Rows.Count > 0)
                mpPVC5.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn5.BackColor;

            if (mpPVC6.Rows.Count > 0)
                mpPVC6.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn6.BackColor;

        }
        public override bool Add()
        {
            bool retValue = true;
            ClearControls();
            lblMessage.Text = "";
            mpPVC1.ModuleNumber = ModuleNumber.CounterSale;
            mpPVC1.OperationMode = OperationMode.Add;
            pnlFinal.Visible = false;
            try
            {

                retValue = base.Add();


                _SSSale.Initialise();
                if (General.CurrentSetting.MsetSaleAskDiscountinCounterSale == "Y")
                {
                    txtDiscPercent.Visible = true;
                    txtDiscAmount.Visible = true;
                    lblDiscPer.Visible = true;
                }
                else
                {
                    txtDiscPercent.Visible = false;
                    txtDiscAmount.Visible = false;
                    lblDiscPer.Visible = false;
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
                FillDoctorCombo();
                FillTransactionType();
                mcbDoctor.SelectedID = "";
                FillTxtPatientName();
                FillTxtAddress();
                pnlFinal.Enabled = false;
                SetGridRowColour();
                ActiveDataGrid = mpPVC1;
                if (ActiveDataGrid != null)
                {
                    ActiveDataGrid.BringToFront();
                    ChangeBackColour(ActiveDataGrid);
                    NoofRows();
                    ActiveDataGrid.SetFocus(1);
                    txtsavecustno.Enabled = true;
                    txtsavecustno.Text = "1";
                    txtsavecustno.Enabled = false;
                    _SSSale.CustNumber = 0;
                    _lastCustIdSelected = "1";
                }
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

            return retValue;
        }

        private void FillTransactionType()
        {
            cbTransactionType.Items.Clear();
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForVoucher);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
            if (General.CurrentSetting.MsetSaleOnlyCashSaleInCounterSale != "Y")
            {
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditCard);
            }
            
            if (General.CurrentSetting.MsetSaleCreditSale == "Y")
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            cbTransactionType.Text = FixAccounts.TransactionTypeForVoucher;
            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForVoucher);
        }

        private void FillTransactionTypeForTab()
        {
            cbTransactionType.Items.Clear();
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForVoucher);
            if (General.CurrentSetting.MsetSaleOnlyCashSaleInCounterSale != "Y")
            {
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditCard);
            }
            if (General.CurrentSetting.MsetSaleCreditSale == "Y")
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                _SSSale.Initialise();
                headerLabel1.Text = "COUNTER SALE -> EDIT";
                pnlCustomerNumber.Enabled = true;
                pnlPatientDrDetails.Enabled = false;
                pnlBillAmount.Enabled = false;
                txtsavecustno.Enabled = true;
                btnPrint.Visible = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();

            pnlFinal.Enabled = true;
            txtPatientName.Enabled = true;
            txtPatientName.SelectedID = "";
            txtPatientName.Text = "";
            mcbDoctor.Enabled = true;
            mcbDoctor.SelectedID = "";
            txtAddress.Enabled = true;
            txtAddress.Text = "";
            //txtPatientNameAddress.Text = "";
            txtNetAmount.Text = "0.00";
            txtBillAmount.Text = "0.00";
            txtBillAmount2.Text = "0.00";
            txtDiscAmount.Text = "0.00";
            txtDiscPercent.Text = "0.00";
            pnlFinal.Enabled = false;
            ActiveDataGrid.SetFocus(1);
            //   mpPVC1.SetFocus(1);

            return retValue;
        }

        public override bool Exit()
        {
            bool retValue = false;

            if (AllBillGridsAreEmpty())
            {

                try
                {
                    retValue = base.Exit();
                    if (retValue)
                    {
                        UpdateClosingStockinCache();
                        mpPVC1.Rows.Clear();
                        mpPVC2.Rows.Clear();
                        mpPVC3.Rows.Clear();
                        mpPVC4.Rows.Clear();
                        mpPVC5.Rows.Clear();
                        mpPVC6.Rows.Clear();

                        ClearAllTotals();

                        mpPVC1.Rows.Add();
                        mpPVC2.Rows.Add();
                        mpPVC3.Rows.Add();
                        mpPVC4.Rows.Add();
                        mpPVC5.Rows.Add();
                        mpPVC6.Rows.Add();
                        SetGridRowColour();

                        EnableAllGrids();



                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
            return retValue;
        }

        public bool AllBillGridsAreEmpty()
        {
            bool retValue = false;
            if ((Convert.ToDouble(txtamount1.Text.ToString()) + Convert.ToDouble(txtamount2.Text.ToString()) + Convert.ToDouble(txtamount3.Text.ToString()) + Convert.ToDouble(txtamount4.Text.ToString()) + Convert.ToDouble(txtamount5.Text.ToString()) + Convert.ToDouble(txtamount6.Text.ToString())) == 0)
                retValue = true;
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "COUNTER SALE -> DELETE";
                _SSSale.Initialise();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }

        public override bool ProcessDelete()
        {
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            try
            {
                _SSSale.Initialise();
                headerLabel1.Text = "COUNTER SALE -> VIEW";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }


        public override bool Print()
        {
            bool retValue = true;
            try
            {
                ConstructPrintGridColumns();
                PrintGrid.Rows.Clear();
                FillPrintGrid();
                if (General.CurrentSetting.MsetPrintSaleBill == "Y")
                    PrintSaleBillPrePrintedPaper();
                else
                    PrintSaleBillPlainPaper();

                ClearData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }


        private void PrintSaleBillPlainPaper()
        {
            PharmaSYSRetailPlus.Printing.PlainPaperPrinter printer = new PharmaSYSRetailPlus.Printing.PlainPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.CrdbName, _SSSale.PatientAddress1, _SSSale.Telephone, _SSSale.DoctorName, _SSSale.DoctorAddress, PrintGrid.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount);

        }

        private void PrintSaleBillPrePrintedPaper()
        {

            PharmaSYSRetailPlus.Printing.PrePrintedPaperPrinter printer = new PharmaSYSRetailPlus.Printing.PrePrintedPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.CrdbName, _SSSale.PatientAddress1, _SSSale.Telephone, _SSSale.DoctorName, _SSSale.DoctorAddress, PrintGrid.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount);

        }

        private void FillPrintGrid()
        {
            foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
            {
                if (dr.Cells[0].Value != null && dr.Cells["Col_Quantity"].Value != null)
                {
                    int printgridindex = PrintGrid.Rows.Add();
                    for (int i = 0; i < 20; i++)
                    {
                        if (dr.Cells[i].Value != null)
                            PrintGrid.Rows[printgridindex].Cells[i].Value = dr.Cells[i].Value;

                    }
                }
            }
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
            double mdiscper = 0;
            double maddon = 0;
            double mdiscamount = 0;
            double mvat5per = 0;
            double mvat12point5per = 0;
            double mamountforzerovat = 0;
            double mamountfor5vat = 0;
            double mamountfor12vat = 0;
            double mbillamount = 0;
            double mamount = 0;
            double mround = 0;
            lblMessage.Text = "";
            _SSSale.IfNewPatient = "N";

            if (txtsavecustno.Text == null || txtsavecustno.Text == string.Empty)
            {
                if (ActiveDataGrid == mpPVC1)
                    txtsavecustno.Text = "1";
                else if (ActiveDataGrid == mpPVC2)
                    txtsavecustno.Text = "2";
                else if (ActiveDataGrid == mpPVC3)
                    txtsavecustno.Text = "3";
                else if (ActiveDataGrid == mpPVC4)
                    txtsavecustno.Text = "4";
                else if (ActiveDataGrid == mpPVC5)
                    txtsavecustno.Text = "5";
                else
                    txtsavecustno.Text = "6";
            }


            if (txtsavecustno.Text == "1")
                ActiveDataGrid = mpPVC1;
            else if (txtsavecustno.Text == "2")
                ActiveDataGrid = mpPVC2;
            else if (txtsavecustno.Text == "3")
                ActiveDataGrid = mpPVC3;
            else if (txtsavecustno.Text == "4")
                ActiveDataGrid = mpPVC4;
            else if (txtsavecustno.Text == "5")
                ActiveDataGrid = mpPVC5;
            else
                ActiveDataGrid = mpPVC6;
            ActiveDataGrid.BringToFront();

            CalculateAmount(-1);

            bool retVal = CheckForScheduleDrug();
            if (retVal)
            {
                if (cbTransactionType.Text == FixAccounts.TransactionTypeForVoucher)
                {
                    cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                    cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                }
            }

            _SSSale.DocID = string.Empty;
            if (txtNarration.Text != null)
                _SSSale.CrdbNarration = txtNarration.Text.ToString();
            if (txtAddress.Text != null && txtAddress.Text != "")
            {
                _SSSale.PatientAddress1 = txtAddress.Text.ToString();
                _SSSale.PatientShortAddress = txtAddress.Text.ToString();
            }
            if (txtPatientName.Text != null && txtPatientName.Text.ToString() != "")
            {
                _SSSale.CrdbName = txtPatientName.Text.ToString();
                _SSSale.ShortName = txtPatientName.Text.ToString();
            }

            if (_SSSale.ShortName.Length > 50)
                _SSSale.ShortName = _SSSale.ShortName.Substring(0, 50);

            if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != string.Empty)
            {
                _SSSale.DocID = mcbDoctor.SelectedID;
                _SSSale.DoctorName = mcbDoctor.SeletedItem.ItemData[1];
                _SSSale.DoctorAddress = mcbDoctor.SeletedItem.ItemData[2];
            }
            else
            {

                _SSSale.DoctorName = mcbDoctor.Text;
                _SSSale.DoctorAddress = txtDoctorAddress.Text.ToString();
                _SSSale.DocID = string.Empty;
            }


            if (_SSSale.DoctorName == string.Empty)
            {
                _SSSale.DoctorName = mcbDoctor.Text;
                _SSSale.DoctorAddress = txtDoctorAddress.Text.ToString();
            }

            if (txtsavecustno.Text != null && Convert.ToInt32(txtsavecustno.Text.ToString()) > 0)
            {
                try
                {


                    _SSSale.SaleSubType = "";
                    if (txtPatientName.SelectedID != null && txtPatientName.SelectedID != "")
                    {
                        if (_SSSale.AccCode == FixAccounts.AccCodeForDebtor)
                        {
                            _SSSale.AccountID = txtPatientName.SelectedID;
                            _SSSale.PatientID = "";
                            _SSSale.SaleSubType = FixAccounts.SubTypeForDebtorSale;
                        }

                        else if (_SSSale.AccCode == FixAccounts.AccCodeForPatient)
                        {
                            _SSSale.PatientID = txtPatientName.SelectedID;
                            _SSSale.AccountID = "";
                            if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                                cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                            _SSSale.SaleSubType = FixAccounts.SubTypeForPatientSale;
                        }
                        else
                        {
                            _SSSale.PatientID = "";
                            _SSSale.AccountID = "";
                            _SSSale.SaleSubType = "";
                        }
                    }
                    else
                    {
                        _SSSale.IfNewPatient = "Y";
                        if (_SSSale.IfNewPatient == "Y" && General.CurrentSetting.MsetSaleSaveCustomerInMaster == "Y" && _SSSale.CrdbName != string.Empty)
                        {
                            _SSSale.PatientID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _IfNewDoctor = "N";
                            if (_SSSale.DocID == string.Empty)
                            {
                                _IfNewDoctor = "Y";
                                _SSSale.DocID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            }
                            cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                            _SSSale.SaleSubType = FixAccounts.SubTypeForPatientSale;
                        }
                        if (cbTransactionType.Text == FixAccounts.TransactionTypeForVoucher)
                            _SSSale.SaleSubType = FixAccounts.SubTypeForVoucherSale;
                        else
                        {
                            _SSSale.SaleSubType = FixAccounts.SubTypeForPatientSale;
                            if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                                cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                        }
                    }



                    if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
                        txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
                    else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                        txtVouType.Text = FixAccounts.VoucherTypeForCreditSale;
                    else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                        txtVouType.Text = FixAccounts.VoucherTypeForCreditStatementSale;
                    else if (cbTransactionType.Text == FixAccounts.TransactionTypeForVoucher)
                        txtVouType.Text = FixAccounts.VoucherTypeForVoucherSale;
                    else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditCard)
                    {
                        txtVouType.Text = FixAccounts.VoucherTypeForCreditSale;
                        _SSSale.SaleSubType = FixAccounts.SubTypeForCreditCardSale;
                    }
                    if (txtVouType.Text == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        if (_SSSale.SaleSubType != "V")
                            txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
                        else
                        {
                            txtVouType.Text = FixAccounts.VoucherTypeForVoucherSale;
                            _SSSale.SaleSubType = FixAccounts.SubTypeForVoucherSale;
                            _SSSale.CrdbAmountNet = SavingCustomersTotalSale();
                          //  TxtSaveCustNoTextChanged();
                            txtBillAmount.Text = _SSSale.CrdbAmountNet.ToString("#0.00");
                        }
                    }

                    System.Text.StringBuilder _errorMessage;

                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _SSSale.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                    _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    _SSSale.CrdbVouType = txtVouType.Text.ToString();
                    double.TryParse(txtVatInput5.Text, out mvat5per);
                    _SSSale.CrdbVat5 = mvat5per;
                    double.TryParse(txtVatInput12point5.Text, out mvat12point5per);
                    _SSSale.CrdbVat12point5 = mvat12point5per;
                    double.TryParse(txtAmountforZeroVAT.Text, out mamountforzerovat);
                    _SSSale.CrdbAmtForZeroVAT = mamountforzerovat;
                    double.TryParse(txtAmountfor5VAT.Text, out mamountfor5vat);
                    _SSSale.CrdbAmountVat5 = mamountfor5vat;
                    double.TryParse(txtAmountfor12VAT.Text, out mamountfor12vat);
                    _SSSale.CrdbAmountVat12point5 = mamountfor12vat;

                    double.TryParse(txtDiscPercent.Text, out mdiscper);
                    _SSSale.CrdbDiscPer = mdiscper;
                    double.TryParse(txtDiscAmount.Text, out mdiscamount);
                    _SSSale.CrdbDiscAmt = mdiscamount;
                    _SSSale.TotalDiscount5 = Convert.ToDouble(txtdiscountAmount5.Text.ToString());
                    _SSSale.TotalDiscount12point5 = Convert.ToDouble(txtDiscountAmount12point5.Text.ToString());
                    _SSSale.MySpecialDiscountPer = Convert.ToDouble(txtMyDiscountPercent.Text.ToString());                    
                    _SSSale.MySpecialDiscountAmount = Convert.ToDouble(txtMyDiscountAmountTotal.Text.ToString());
                    _SSSale.MyTotalSpecialDiscountPer12point5 = Convert.ToDouble(txtMyDiscountAmount12point5.Text.ToString());
                    _SSSale.MyTotalSpecialDiscountPer5 = Convert.ToDouble(txtMyDiscountAmount5.Text.ToString());
                   
                    double.TryParse(txtBillAmount.Text, out mbillamount);
                    _SSSale.CrdbAmountNet = mbillamount-_SSSale.MySpecialDiscountAmount;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditSale || _SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditStatementSale)
                    {
                        _SSSale.CrdbAmountBalance = _SSSale.CrdbAmountNet;
                        _SSSale.CrdbAmountClear = 0;
                        if (_SSSale.SaleSubType == FixAccounts.SubTypeForPatientSale && (_SSSale.AccountID == null || _SSSale.AccountID == string.Empty))
                            _SSSale.AccountID = FixAccounts.AccountPendingCashBills;

                    }
                    else
                    {
                        _SSSale.CrdbAmountBalance = 0;
                        _SSSale.CrdbAmountClear = _SSSale.CrdbAmountNet;
                    }
                    double.TryParse(txtNetAmount.Text, out mamount);
                    _SSSale.CrdbAmount = mamount- _SSSale.MySpecialDiscountAmount;

                    _SSSale.OperatorID = "";
                    _SSSale.OperatorPassword = txtOperator.Text.ToString();
                    if (txtMobileNumber.Text != null && txtMobileNumber.Text != string.Empty)
                        _SSSale.Telephone = txtMobileNumber.Text.ToString();
                    if (txtRoundAmount.Text != null)
                        mround = Convert.ToDouble(txtRoundAmount.Text.ToString());
                    _SSSale.CrdbRoundAmount = mround;
                    _SSSale.CrdbAddOn = maddon;
                    CalculateProfitPercent();
                    _SSSale.Validate();

                    if (retVal)
                    {
                        _SSSale.ValidationMessages.Add("Shedule H1 Product");
                    }
                    if (_SSSale.IsValid)
                    {

                        LockTable.LockTablesForSale();
                        bool ifstockavailable = CheckForStockintblStock();
                        if (ifstockavailable)
                        {
                            // Begin Transactions

                            General.BeginTransaction();

                            _SSSale.CreatedBy = General.CurrentUser.Id;
                            _SSSale.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _SSSale.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            _SSSale.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                            DateTime dd = Convert.ToDateTime(General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy));
                            DateTime tt = datePickerBillDate.Value;
                            System.TimeSpan ts;
                            ts = (tt - dd);
                            int tsdays = ts.Days + 1;

                            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                            {
                                _SSSale.CrdbCountersaleNumber = tsdays;
                            }
                            else
                            {
                                _SSSale.CrdbCountersaleNumber = 0;
                            }
                            txtVouchernumber.Text = Convert.ToString(_SSSale.CrdbVouNo);
                            if (_SSSale.IfNewPatient == "Y" && General.CurrentSetting.MsetSaleSaveCustomerInMaster == "Y" && _SSSale.CrdbName != string.Empty)
                            {
                                if (_IfNewDoctor == "Y")
                                    _SSSale.SaveNewDoctor();

                                _SSSale.SaveNewPatient();
                            }
                            retValue = _SSSale.AddDetails();
                            _SavedID = _SSSale.Id;
                            if (retValue)
                                retValue = SaveParticularsProductwise();
                            if (retValue)
                                retValue = ReduceStockIntblStock();
                            if (retValue)
                                retValue = SaveIntblTrnac();

                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {
                                UpdateClosingStockinCache();
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
                                PSDialogResult result = PSMessageBox.Show("Could not Save Voucher...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                retValue = false;
                            }

                        }
                        else
                        {
                            LockTable.UnLockTables();
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
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
            LockTable.UnLockTables();
            return retValue;
        }

        private bool CheckForScheduleDrug()
        {
            bool retValue = false;

            try
            {
                foreach (DataGridViewRow prodrow in ActiveDataGrid.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        if (prodrow.Cells["Col_ProdScheduleDrugCode"].Value != null && prodrow.Cells["Col_ProdScheduleDrugCode"].Value.ToString() != string.Empty)
                        {
                            if (prodrow.Cells["Col_ProdScheduleDrugCode"].Value.ToString() == "H1" || prodrow.Cells["Col_ProdScheduleDrugCode"].Value.ToString() == "H" || prodrow.Cells["Col_ProdScheduleDrugCode"].Value.ToString() == "S")
                                retValue = true;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            if (ID != null && ID != "")
            {

            }
            return true;
        }
        #endregion IDetail Control

        #region IDetail Members
        public override void ReFillData()
        {
            try
            {
                FillTxtAddress();
                FillDoctorCombo();
                FillTxtPatientName();
                //General.CurrentSetting = new Settings();
                General.CurrentSetting.FillSettings();
                mpPVC1.DataSourceProductList = General.ProductList;
                mpPVC2.DataSourceProductList = General.ProductList;
                mpPVC3.DataSourceProductList = General.ProductList;
                mpPVC4.DataSourceProductList = General.ProductList;
                mpPVC5.DataSourceProductList = General.ProductList;
                mpPVC6.DataSourceProductList = General.ProductList;
                mpPVC1.BindGridProductList();
                mpPVC2.BindGridProductList();
                mpPVC3.BindGridProductList();
                mpPVC4.BindGridProductList();
                mpPVC5.BindGridProductList();
                mpPVC6.BindGridProductList();
                ActiveDataGrid.DataSourceProductList = General.ProductList;
                ActiveDataGrid.BindGridProductList();
                ActiveDataGrid.SetFocus(1);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;

            try
            {

                if (keyPressed == Keys.F1 && pnlSpecialDiscount.Visible == true)
                {
                    rbtnSpecialDiscount1.Checked = true;
                  //  CalculateAmount(-1);
                  //  CalculateAllAmounts();
                    retValue = true;
                }
                if (keyPressed == Keys.F2 && pnlSpecialDiscount.Visible == true)
                {
                    rbtnSpecialDiscount2.Checked = true;
                  //  CalculateAmount(-1);
                  //  CalculateAllAmounts();
                    retValue = true;
                }
                if (keyPressed == Keys.F3 && pnlSpecialDiscount.Visible == true)
                {
                    rbtnSpecialDiscount3.Checked = true;
                   // CalculateAmount(-1);
                  //  CalculateAllAmounts();
                    retValue = true;
                }
                if (keyPressed == Keys.F4 && pnlSpecialDiscount.Visible == true)
                {
                    rbtnSpecialDiscount4.Checked = true;
                   // CalculateAmount(-1);
                   // CalculateAllAmounts();
                    retValue = true;
                }
                if (keyPressed == Keys.NumPad1 && modifier == Keys.Alt)
                {
                    btn1.Focus();

                    retValue = true;
                }

                if (keyPressed == Keys.NumPad2 && modifier == Keys.Alt)
                {
                    btn2.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.NumPad3 && modifier == Keys.Alt)
                {
                    btn3.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.NumPad4 && modifier == Keys.Alt)
                {
                    btn4.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.NumPad5 && modifier == Keys.Alt)
                {
                    btn5.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.NumPad6 && modifier == Keys.Alt)
                {
                    btn6.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.NumPad1 && modifier == Keys.Control)
                {
                    rbtnSpecialDiscount1.Checked = true;
                    retValue = true;
                }
                if (keyPressed == Keys.NumPad4 && modifier == Keys.Control)
                {
                    rbtnSpecialDiscount4.Checked = true;
                    retValue = true;
                }
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    txtPatientName.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    txtAddress.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    uclSubstituteControl1.Visible = false;
                }

                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    mcbDoctor.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.Y && modifier == Keys.Alt)
                {
                    cbTransactionType.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.Escape)
                {
                    if (uclSubstituteControl1.Visible == true)
                    {
                        uclSubstituteControl1.Visible = false;
                        retValue = true;
                    }
                    if (pnlFinal.Visible == true)
                    {
                        pnlFinal.Visible = false;
                        EnableAllGrids();

                        cbTransactionType.Text = FixAccounts.TransactionTypeForVoucher;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForVoucher);

                        ActiveDataGrid.SetFocus(1);

                        retValue = true;
                    }
                }
                if (retValue == false)
                {
                    retValue = base.HandleShortcutAction(keyPressed, modifier);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }


        #endregion

        # region Internal methods
        private void InitialisempPVC1()
        {
            try
            {
                for (int i = 1; i <= 6; i++)
                {
                    if (i == 1)
                        ActiveDataGrid = mpPVC1;
                    else if (i == 2)
                        ActiveDataGrid = mpPVC2;
                    else if (i == 3)
                        ActiveDataGrid = mpPVC3;
                    else if (i == 4)
                        ActiveDataGrid = mpPVC4;
                    else if (i == 5)
                        ActiveDataGrid = mpPVC5;
                    else
                        ActiveDataGrid = mpPVC6;
                    ActiveDataGrid.ColumnsMain.Clear();
                    ConstructMainColumns(ActiveDataGrid);
                    ConstructProductSelectionListGridColumns(ActiveDataGrid);
                    ConstructBatchGridColumns(ActiveDataGrid);
                    if (General.CurrentSetting.MsetSaleShowOnlyMRPInCounterSale == "Y")
                    {
                        ActiveDataGrid.ColumnsBatchList["Col_PurchaseRate"].Visible = false;
                        ActiveDataGrid.ColumnsBatchList["Col_SaleRate"].Visible = false;
                    }
                    else
                    {
                        ActiveDataGrid.ColumnsBatchList["Col_PurchaseRate"].Visible = true;
                        ActiveDataGrid.ColumnsBatchList["Col_SaleRate"].Visible = true;
                    }
                }
                ConstructTempGridColumns();


                DataTable dtable = new DataTable();
                dtable = _SSSale.ReadProductDetailsByID();
                Product prod = new Product();
                mpPVC1.DataSourceProductList = General.ProductList;
                mpPVC2.DataSourceProductList = General.ProductList;
                mpPVC3.DataSourceProductList = General.ProductList;
                mpPVC4.DataSourceProductList = General.ProductList;
                mpPVC5.DataSourceProductList = General.ProductList;
                mpPVC6.DataSourceProductList = General.ProductList;
                FormatGrids();
                ActiveDataGrid = mpPVC1;
                mpPVC1.Bind();
                mpPVC2.Bind();
                mpPVC3.Bind();
                mpPVC4.Bind();
                mpPVC5.Bind();
                mpPVC6.Bind();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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

            mpPVC2.BatchGridShowColumnName = "Col_UOM";
            mpPVC2.NewRowColumnName = "Col_Quantity";
            mpPVC2.DoubleColumnNames.Add("Col_MRP");
            mpPVC2.NumericColumnNames.Add("Col_Quantity");
            mpPVC2.DoubleColumnNames.Add("Col_VATPer");
            mpPVC2.DoubleColumnNames.Add("Col_PurchaseRate");
            mpPVC2.DoubleColumnNames.Add("Col_Amount");
            mpPVC2.DoubleColumnNames.Add("Col_SaleRate");
            mpPVC2.ProductGridClosingStockColumnName = "Col_ClosingStock";
            mpPVC2.MainGridSoldQuantityColumnName = "Col_Quantity";
            mpPVC2.ClearSelection();

            mpPVC3.BatchGridShowColumnName = "Col_UOM";
            mpPVC3.NewRowColumnName = "Col_Quantity";
            mpPVC3.DoubleColumnNames.Add("Col_MRP");
            mpPVC3.NumericColumnNames.Add("Col_Quantity");
            mpPVC3.DoubleColumnNames.Add("Col_VATPer");
            mpPVC3.DoubleColumnNames.Add("Col_PurchaseRate");
            mpPVC3.DoubleColumnNames.Add("Col_Amount");
            mpPVC3.DoubleColumnNames.Add("Col_SaleRate");
            mpPVC3.ProductGridClosingStockColumnName = "Col_ClosingStock";
            mpPVC3.MainGridSoldQuantityColumnName = "Col_Quantity";
            mpPVC3.ClearSelection();

            mpPVC4.BatchGridShowColumnName = "Col_UOM";
            mpPVC4.NewRowColumnName = "Col_Quantity";
            mpPVC4.DoubleColumnNames.Add("Col_MRP");
            mpPVC4.NumericColumnNames.Add("Col_Quantity");
            mpPVC4.DoubleColumnNames.Add("Col_VATPer");
            mpPVC4.DoubleColumnNames.Add("Col_PurchaseRate");
            mpPVC4.DoubleColumnNames.Add("Col_Amount");
            mpPVC4.DoubleColumnNames.Add("Col_SaleRate");
            mpPVC4.ProductGridClosingStockColumnName = "Col_ClosingStock";
            mpPVC4.MainGridSoldQuantityColumnName = "Col_Quantity";
            mpPVC4.ClearSelection();

            mpPVC5.BatchGridShowColumnName = "Col_UOM";
            mpPVC5.NewRowColumnName = "Col_Quantity";
            mpPVC5.DoubleColumnNames.Add("Col_MRP");
            mpPVC5.NumericColumnNames.Add("Col_Quantity");
            mpPVC5.DoubleColumnNames.Add("Col_VATPer");
            mpPVC5.DoubleColumnNames.Add("Col_PurchaseRate");
            mpPVC5.DoubleColumnNames.Add("Col_Amount");
            mpPVC5.DoubleColumnNames.Add("Col_SaleRate");
            mpPVC5.ProductGridClosingStockColumnName = "Col_ClosingStock";
            mpPVC5.MainGridSoldQuantityColumnName = "Col_Quantity";
            mpPVC5.ClearSelection();

            mpPVC6.BatchGridShowColumnName = "Col_UOM";
            mpPVC6.NewRowColumnName = "Col_Quantity";
            mpPVC6.DoubleColumnNames.Add("Col_MRP");
            mpPVC6.NumericColumnNames.Add("Col_Quantity");
            mpPVC6.DoubleColumnNames.Add("Col_VATPer");
            mpPVC6.DoubleColumnNames.Add("Col_PurchaseRate");
            mpPVC6.DoubleColumnNames.Add("Col_Amount");
            mpPVC6.DoubleColumnNames.Add("Col_SaleRate");
            mpPVC6.ProductGridClosingStockColumnName = "Col_ClosingStock";
            mpPVC6.MainGridSoldQuantityColumnName = "Col_Quantity";
            mpPVC6.ClearSelection();
        }

        private void DeleteRecordsForSelectedNumber()
        {
            try
            {
                List<DataGridViewRow> rowCollection = new List<DataGridViewRow>();
                foreach (DataGridViewRow prodrow in ActiveDataGrid.Rows)
                {
                    if (prodrow.Cells["Col_ProductID"].Value != null)
                    {
                        rowCollection.Add(prodrow);
                    }
                }

                foreach (DataGridViewRow prodrow in rowCollection)
                {
                    ActiveDataGrid.Rows.Remove(prodrow);
                }

                rowCollection = new List<DataGridViewRow>();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
                foreach (DataGridViewRow prodrow in ActiveDataGrid.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
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
                                tempstock = Convert.ToInt32(temprow.Cells["Col_Quantity"].Value.ToString());
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
                foreach (DataGridViewRow prodrow in ActiveDataGrid.Rows)
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
                        _SSSale.PurchaseRate = mpurrate;
                        mrate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());
                       
                        if (prodrow.Cells["Col_TradeRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                        if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                        _SSSale.TradeRate = mtraderate;
                        double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                        if (prodrow.Cells["Col_VATPer"].Value != null)
                            double.TryParse(prodrow.Cells["Col_VATPer"].Value.ToString(), out mvatper);
                        mvatamt = Math.Round((mtraderate * mvatper) / 100, 2);
                        mamt = Math.Round(Math.Round(mrate / mpakn, 2) * mqty, 2);
                        double mdiscamt = 0;
                        if (prodrow.Cells["Col_DiscountAmount"].Value != null)
                            mdiscamt = Convert.ToDouble(prodrow.Cells["Col_DiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_MySpecialDiscountAmount"].Value != null && prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString() != string.Empty)
                            mdiscamt += Convert.ToDouble(prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString());
                        _SSSale.SaleRate = msalerate;
                        double newmdiscper = 0;
                        double newmydiscper = 0;
                        double.TryParse(txtDiscPercent.Text, out newmdiscper);
                        double.TryParse(txtMyDiscountPercent.Text, out newmydiscper);
                        double newsalerate = msalerate - Math.Round(((msalerate - Math.Round((msalerate * mvatper/100),2)) * (newmdiscper + newmydiscper)/100), 2);
                        double vatontrrate = Math.Round((mtraderate * mvatper) / 100, 2);
                       
                        totalvat += vatontrrate;
                        totalsale += newsalerate;
                        totalpur += mpurrate;

                        _SSSale.ProfitPercentBySaleRate = Math.Round(((msalerate - mdiscamt) - (mpurrate + mvatamt)) / (msalerate-mdiscamt), 4);
                        _SSSale.ProfitPercentByPurchaseRate = Math.Round(((msalerate-mdiscamt) - (mpurrate + mvatamt)) / (mpurrate + mvatamt), 4);
                        //_SSSale.TotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
                        //_SSSale.TotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
                        _SSSale.ProfitInRupees = Math.Round((((msalerate-mdiscamt) - (mpurrate + mvatamt)) / mpakn) * mqty, 2);
                        _SSSale.TotalProfitInRupees += _SSSale.ProfitInRupees;
                        prodrow.Cells["Col_ProfitPercentBySaleRate"].Value = _SSSale.ProfitPercentBySaleRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _SSSale.ProfitPercentByPurchaseRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitInRupees"].Value = _SSSale.ProfitInRupees.ToString("#0.00");
                    }
                   
                }
                _SSSale.TotalProfitPercentBySaleRate = Math.Round(((totalsale) - (totalpur + totalvat)) / (totalsale), 4);
                _SSSale.TotalProfitPercentByPurchaseRate = Math.Round(((totalsale) - (totalpur + totalvat)) / (totalpur + totalvat), 4);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private bool SaveParticularsProductwise()
        {
            bool returnVal = false;
            _SSSale.SerialNumber = 0;
            try
            {


                foreach (DataGridViewRow prodrow in ActiveDataGrid.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        _SSSale.SerialNumber += 1;
                        int mqty = 0;
                        double mpurrate = 0;
                        double mtraterate = 0;
                        double mmrp = 0;
                        double msalerate = 0;
                        double mvatper = 0;
                        double mamt = 0;
                        double mvatamt = 0;
                        string mlastsaleid = "";

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
                        if (prodrow.Cells["Col_TradeRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_TradeRate"].Value.ToString(), out mtraterate);
                        _SSSale.TradeRate = mtraterate;
                        if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                        _SSSale.PurchaseRate = mpurrate;
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
                        mlastsaleid = (prodrow.Cells["Col_StockID"].Value.ToString());
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

        private bool SaveIntblTrnac()
        {

            bool retValue = true;
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
                double.TryParse(txtVatInput5.Text, out mvat5per);
                _SSSale.CrdbVat5 = mvat5per;
                double.TryParse(txtVatInput12point5.Text, out mvat12point5per);
                _SSSale.CrdbVat12point5 = mvat12point5per;
                double.TryParse(txtAmountforZeroVAT.Text.ToString(), out mamtforzerovat);
                _SSSale.CrdbAmtForZeroVAT = mamtforzerovat;
                double.TryParse(txtDiscPercent.Text, out mdiscper);
                _SSSale.CrdbDiscPer = mdiscper;
                double.TryParse(txtDiscAmount.Text, out mdiscamount);
                _SSSale.CrdbDiscAmt = mdiscamount;
                double.TryParse(txtBillAmount.Text, out mbillamount);
                _SSSale.CrdbAmountNet = mbillamount;
                mround = _SSSale.CrdbRoundAmount;

                mdebit = Math.Round(mbillamount - Math.Round(mvat5per, 2) - Math.Round(mvat12point5per, 2) + mdiscamount - maddon - mround - mamtforzerovat + mcreditnoteamt - mdebitnoteamt, 2);

                if (mamtforzerovat > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVATZeroSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mamtforzerovat;
                    retValue = _SSSale.AddVoucherIntblTrnac();

                }

                if (retValue == true && mvat5per > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput5Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        _SSSale.CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCash;
                        else
                            _SSSale.CreditAccount = _SSSale.AccountID;
                    }
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(mvat5per, 2);
                    retValue = _SSSale.AddVoucherIntblTrnac();

                }
                if (retValue == true && mvat12point5per > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput12point5Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        _SSSale.CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCash;
                        else
                            _SSSale.CreditAccount = _SSSale.AccountID;
                    }
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(mvat12point5per, 2);
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (retValue == true && maddon > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountAddonSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        _SSSale.CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCash;
                        else
                            _SSSale.CreditAccount = _SSSale.AccountID;
                    }
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = maddon;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (retValue == true && mround > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        _SSSale.CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCash;
                        else
                            _SSSale.CreditAccount = _SSSale.AccountID;
                    }
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = (mround);
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (retValue == true && mround < 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        _SSSale.CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCash;
                        else
                            _SSSale.CreditAccount = _SSSale.AccountID;
                    }
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mround * (-1);
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }

                if (retValue == true && mdiscamount > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountCashDiscountSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        _SSSale.CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale || _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCash;
                        else
                            _SSSale.CreditAccount = _SSSale.AccountID;
                    }
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mdiscamount;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }

                if (retValue == true && mcreditnoteamt > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountSalesReturn;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        _SSSale.CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale || _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCash;
                        else
                            _SSSale.CreditAccount = _SSSale.AccountID;
                    }
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mcreditnoteamt;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (retValue == true && mdebitnoteamt > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountDebitNoteSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        _SSSale.CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale || _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCash;
                        else
                            _SSSale.CreditAccount = _SSSale.AccountID;
                    }
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mdebitnoteamt;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (retValue == true && mdebit > 0)
                {

                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.DebitAccount = FixAccounts.AccountCashSale;
                    else if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        _SSSale.DebitAccount = FixAccounts.AccountVoucherSale;
                    else
                        _SSSale.DebitAccount = FixAccounts.AccountCashCreditSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale || _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mdebit;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (retValue == true && mbillamount > 0)
                {
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale || _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        _SSSale.DebitAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.DebitAccount = _SSSale.AccountID;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        _SSSale.CreditAccount = FixAccounts.AccountVoucherSale;
                    else
                    {
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCashSale;
                        else
                            _SSSale.CreditAccount = FixAccounts.AccountCashCreditSale;
                    }
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

        private void FillDailyShortList()
        {
            try
            {
                _SSSale.ShortListID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _SSSale.AddToShortList();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private bool ReduceStockIntblStock()
        {
            bool returnVal = false;
            string mlastsaleid;
            try
            {
                foreach (DataGridViewRow prodrow in ActiveDataGrid.Rows)
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
                                    FillDailyShortList();
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
            bool returnVal = false;
            try
            {
                General.UpdateProductListCacheTest(ActiveDataGrid.Rows, "Col_ProductID", ActiveDataGrid.Rows, "Col_ProductID");
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }
        private void FillDoctorCombo()
        {
            try
            {
                mcbDoctor.SelectedID = null;
                mcbDoctor.SourceDataString = new string[4] { "DocID", "DocName", "DocAddress", "DocShortNameAddress" };
                mcbDoctor.ColumnWidth = new string[4] { "0", "200", "300", "0" };
                mcbDoctor.ValueColumnNo = 0;
                mcbDoctor.UserControlToShow = new UclDoctor();
                Doctor _Doctor = new Doctor();
                DataTable dtabled = _Doctor.GetSSDoctorsList();
                mcbDoctor.FillData(dtabled);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        private void FillTxtPatientName()
        {
            try
            {
                txtPatientName.SelectedID = null;
                txtPatientName.SourceDataString = new string[8] { "PatientID", "AccCode", "PatientName", "PatientAddress1", "PatientAddress2", "ShortNameAddress", "DoctorID", "AccTransactionType" };
                txtPatientName.ColumnWidth = new string[8] { "0", "50", "200", "200", "200", "0", "0", "0" };
                txtPatientName.DisplayColumnNo = 2;
                txtPatientName.ValueColumnNo = 0;
                txtPatientName.UserControlToShow = new UclPatient();
                Patient _Party = new Patient();
                DataTable dtable = new DataTable();
                if (General.CurrentSetting.MsetSaleOnlyCashSaleInCounterSale == "Y")
                   dtable = _Party.GetOverviewDataForCounterSaleForOnlyCashSale();
                else
                   dtable = _Party.GetOverviewDataForCounterSale();
                txtPatientName.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillTxtAddress()
        {
            try
            {
                txtAddress.SelectedID = null;
                txtAddress.SourceDataString = new string[2] { "AreaID", "AreaName" };
                txtAddress.ColumnWidth = new string[2] { "0", "200" };
                txtAddress.ValueColumnNo = 0;
                txtAddress.UserControlToShow = new UclArea();
                Area _Area = new Area();
                DataTable dtable = _Area.GetOverviewData();
                txtAddress.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillSpecialDiscount()
        {
            if (General.CurrentSetting.MsetSaleAllowSpecialDiscount == "Y")
            {
                pnlSpecialDiscount.Visible = true;
                rbtnSpecialDiscount1.Visible = true;
                rbtnSpecialDiscount2.Visible = true;
                rbtnSpecialDiscount3.Visible = true;
                rbtnSpecialDiscount4.Visible = true;
                rbtnSpecialDiscount1.Text = (General.CurrentSetting.MsetSpecialDiscount1.ToString());
                rbtnSpecialDiscount2.Text = (General.CurrentSetting.MsetSpecialDiscount2.ToString());
                rbtnSpecialDiscount3.Text = (General.CurrentSetting.MsetSpecialDiscount3.ToString());
            }
            else
            {
                pnlSpecialDiscount.Visible = false;
            }
        }
        private void mpPVC1_OnCellValueChangeCommited(int colIndex)
        {
            ActiveDataGridCellValueChangeCommited(colIndex);
        }
        private void mpPVC2_OnCellValueChangeCommited(int colIndex)
        {
            ActiveDataGridCellValueChangeCommited(colIndex);
        }
        private void mpPVC3_OnCellValueChangeCommited(int colIndex)
        {
            ActiveDataGridCellValueChangeCommited(colIndex);
        }
        private void mpPVC4_OnCellValueChangeCommited(int colIndex)
        {
            ActiveDataGridCellValueChangeCommited(colIndex);
        }
        private void mpPVC5_OnCellValueChangeCommited(int colIndex)
        {
            ActiveDataGridCellValueChangeCommited(colIndex);
        }
        private void mpPVC6_OnCellValueChangeCommited(int colIndex)
        {
            ActiveDataGridCellValueChangeCommited(colIndex);
        }

        private void ActiveDataGridCellValueChangeCommited(int colIndex)
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
                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                        _preID = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductName"].Value != null)
                        prodname = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString();
                    if (prodname != "" && _preID != "")
                    {
                        prodname = General.GetProductName(_preID);
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = prodname;
                    }
                }
                if (colIndex == 11)  // Quantity
                {
                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value == null || Convert.ToInt32(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString()) == 0)
                    {
                        ActiveDataGrid.IsAllowNewRow = false;
                    }
                    else
                    {
                        if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                            mbtno = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim();
                        if (mbtno != string.Empty)
                        {
                            string mdt = DateTime.Today.Date.ToString("yyyyMMdd");
                            if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value != null)
                                mexpirydate = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();

                            if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                            {
                                lblMessage.Text = "Expired Product";
                                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = 0;
                                ActiveDataGrid.IsAllowNewRow = false;
                                ActiveDataGrid.SetFocus(11);
                            }
                            else
                            {
                                requiredQty = Convert.ToInt32(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                                if (requiredQty <= 0 || _SSSale.CurrentBatchStock == 0)
                                {
                                    if (requiredQty <= 0)
                                    {
                                        lblMessage.Text = "Enter Quantity";
                                        ActiveDataGrid.SetFocus(11);
                                        ActiveDataGrid.IsAllowNewRow = false;
                                    }
                                    else
                                    {
                                        lblMessage.Text = "Batch Stock Zero";
                                        ActiveDataGrid.SetFocus(11);
                                        ActiveDataGrid.IsAllowNewRow = false;
                                    }
                                }
                                else if (requiredQty > _SSSale.CurrentBatchStock && _Mode == OperationMode.Edit)
                                {
                                    lblMessage.Text = "Enter Correct Quantity";
                                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = _SSSale.CurrentBatchStock;
                                    ActiveDataGrid.SetFocus(11);
                                    ActiveDataGrid.IsAllowNewRow = false;
                                    CalculateAmount(-1);
                                }
                                else
                                {
                                    int mbatchstock = 0;
                                    int oldQuantity = 0;
                                    //   int gridstock = 0;
                                    string mstockid = "";
                                    //    int gridprodstock = 0;
                                    int custno = 0;
                                    mprodno = "";
                                    mqty = 0;
                                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                                        mprodno = (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString());
                                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value != null)
                                        int.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value.ToString().Trim(), out mbatchstock);
                                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Old_Quantity"].Value != null)
                                        int.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString().Trim(), out oldQuantity);
                                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                                        mstockid = (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString());
                                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_CustID"].Value != null)
                                        int.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_CustID"].Value.ToString(), out custno);

                                    lblMessage.Text = "";

                                    if (requiredQty <= _SSSale.CurrentBatchStock)
                                    {
                                        FillBatchStock(ref mmrp, ref mrate, ref mpakn, ref mbtno, ref mprodno, ref mcurrentindex, ref oldmqty, ref mqty, ref custno);
                                        ActiveDataGrid.IsAllowNewRow = true;
                                        ////////if (_Mode == OperationMode.Add)
                                        ////////{
                                        ////////    WriteToXML();
                                        ////////}
                                        ActiveDataGrid.ColumnsMain["Col_Quantity"].ReadOnly = true;
                                    }
                                    else
                                    {


                                        if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                                            mprodno = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();

                                        FillMainGridwithMultipleBatch(requiredQty, mprodno);
                                        CalculateAmount(-1);
                                        ActiveDataGrid.ColumnsMain["Col_Quantity"].ReadOnly = true;

                                        //}
                                        if (_Mode == OperationMode.Add)
                                        {
                                            WriteToXML();
                                        }
                                        ActiveDataGrid.ColumnsMain["Col_Quantity"].ReadOnly = true;
                                    }


                                }

                            }
                        }
                        else
                        {

                            ActiveDataGridOnRowDeleted(ActiveDataGrid.MainDataGridCurrentRow);
                            ActiveDataGrid.Rows.Remove(ActiveDataGrid.MainDataGridCurrentRow);

                        }
                    }
                }

                if (colIndex == 10)  // sale rate
                {
                    if (Convert.ToDouble(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value) <= 0)
                    {
                        lblMessage.Text = "Enter SaleRate";
                        ActiveDataGrid.SetFocus(10);
                        ActiveDataGrid.IsAllowNewRow = false;
                    }
                    else if (Convert.ToInt32(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) > 0)
                    {
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Amount"].Value = (Convert.ToDouble(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value) * Convert.ToDouble(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value)).ToString();
                        CalculateAmount(-1);
                        ActiveDataGrid.IsAllowNewRow = true;
                    }
                }


                if (colIndex == 7)  // Expiry
                {
                    string newexpiry = "";
                    string newexpirydate = "";
                    int explength = 0;
                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null)
                        explength = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim().Length;
                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null && ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim() != "" && explength > 0)
                    {
                        newexpiry = General.GetValidExpiry(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString());
                        if (newexpiry != "")
                        {
                            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry.ToString();
                            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                            lblMessage.Text = "";
                        }
                        else
                        {
                            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                            lblMessage.Text = " No Expiry ";
                        }

                    }
                    else
                    {
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                        lblMessage.Text = " No Expiry ";
                    }
                }
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void WriteToXML()
        {

        }

        private void FillMainGridwithMultipleBatch(int requiredqty, string productID)
        {
            int mmaingridrowIndex = 0;
            DataTable stkdt = new DataTable();
            Stock prodstk = new Stock();
            int mycolindex = 0;
            int msalestk = requiredqty;
            int mactualclosingstock = 0;

            if (ActiveDataGrid.Rows.Count > 0)
                mmaingridrowIndex = ActiveDataGrid.MainDataGridCurrentRow.Index;

            stkdt = prodstk.GetStockByProductIDForSale(productID);

            foreach (DataRow dtrow in stkdt.Rows)
            {
                if (dtrow["ClosingStock"] != DBNull.Value)
                    mactualclosingstock += Convert.ToInt32(dtrow["ClosingStock"].ToString());
            }
            try
            {

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

                        ActiveDataGrid.Rows[mycolindex].Cells["Col_ProductID"].Value = dtrow["ProductID"].ToString();
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_ProductName"].Value = dtrow["ProdName"].ToString();
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_UOM"].Value = dtrow["ProdLoosePack"].ToString();
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_Pack"].Value = dtrow["ProdPack"].ToString();
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_ProdCompShortName"].Value = dtrow["ProdCompShortName"].ToString();
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_Shelf"].Value = dtrow["ShelfCode"].ToString();
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_BatchNumber"].Value = dtrow["BatchNumber"].ToString();
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_Expiry"].Value = dtrow["Expiry"].ToString();
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_MRP"].Value = Convert.ToDouble(dtrow["MRP"].ToString()).ToString("#0.00");
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_SaleRate"].Value = Convert.ToDouble(dtrow["SaleRate"].ToString()).ToString("#0.00");
                        double.TryParse(dtrow["SaleRate"].ToString(), out msalerate);
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_Quantity"].Value = Math.Min(mactualsalestock, mbatchstock);
                        double mamt = 0;
                        mamt = Math.Round(msalerate * mactualsalestock, 2);
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_Amount"].Value = Convert.ToDouble(mamt.ToString()).ToString("#0.00");
                        ActiveDataGrid.Rows[mycolindex].Cells["Old_Quantity"].Value = ActiveDataGrid.Rows[mycolindex].Cells["Col_Quantity"].Value;
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_ClosingStock"].Value = dtrow["ProdClosingStock"].ToString();
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_PurchaseRate"].Value = dtrow["PurchaseRate"].ToString();
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_ExpiryDate"].Value = dtrow["ExpiryDate"].ToString();

                        ActiveDataGrid.Rows[mycolindex].Cells["Col_VATPer"].Value = Convert.ToDouble(dtrow["ProdVATPercent"].ToString()).ToString("#0.00");
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_TradeRate"].Value = dtrow["TradeRate"].ToString();
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_StockID"].Value = dtrow["StockID"].ToString();
                        int mclstkdr1 = 0;
                        int.TryParse(dtrow["ClosingStock"].ToString(), out mclstkdr1);
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;
                        msalestk = msalestk - mactualsalestock;
                        mactualclosingstock -= mactualsalestock;
                        CalculateAmount(-1);
                        if (msalestk > 0 && mactualclosingstock > 0)
                        {
                            int curind = ActiveDataGrid.Rows.Add();
                            if (ActiveDataGrid == mpPVC1)
                                ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn1.BackColor;
                            else if (ActiveDataGrid == mpPVC2)
                                ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn2.BackColor;
                            else if (ActiveDataGrid == mpPVC3)
                                ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn3.BackColor;
                            else if (ActiveDataGrid == mpPVC4)
                                ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn4.BackColor;
                            else if (ActiveDataGrid == mpPVC5)
                                ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn5.BackColor;
                            else
                                ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn6.BackColor;

                            ActiveDataGrid.SetFocus(ActiveDataGrid.Rows.Count - 1, 11);
                            mmaingridrowIndex = mmaingridrowIndex + 1;
                        }
                    }
                }
                ActiveDataGrid.IsAllowNewRow = true;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillBatchStock(ref double mmrp, ref double mrate, ref int mpakn, ref string mbtno, ref string mprodno, ref int mcurrentindex, ref int oldmqty, ref int mqty, ref int custno)
        {
            mcurrentindex = ActiveDataGrid.MainDataGridCurrentRow.Index;
            if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                mprodno = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();
            if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                mbtno = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim();
            if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                double.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
            if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                mqty = Convert.ToInt32(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
            foreach (DataGridViewRow drp in ActiveDataGrid.Rows)
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
                        ActiveDataGrid.Rows.Remove(ActiveDataGrid.MainDataGridCurrentRow);
                        break;
                    }
                }
            }

            if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_UOM"].Value != null)
                mpakn = Convert.ToInt32(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
            if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value != null)
                double.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value.ToString(), out mrate);
            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Amount"].Value = ((mrate / mpakn) * mqty).ToString("#0.00");
            CalculateAmount(-1);
        }
        private void mpPVC1_OnProductSelected(DataGridViewRow productRow)
        {
            ActiveDataGridOnProductSelected(productRow);
        }
        private void mpPVC2_OnProductSelected(DataGridViewRow productRow)
        {
            ActiveDataGridOnProductSelected(productRow);
        }
        private void mpPVC3_OnProductSelected(DataGridViewRow productRow)
        {
            ActiveDataGridOnProductSelected(productRow);
        }
        private void mpPVC4_OnProductSelected(DataGridViewRow productRow)
        {
            ActiveDataGridOnProductSelected(productRow);
        }
        private void mpPVC5_OnProductSelected(DataGridViewRow productRow)
        {
            ActiveDataGridOnProductSelected(productRow);
        }
        private void mpPVC6_OnProductSelected(DataGridViewRow productRow)
        {
            ActiveDataGridOnProductSelected(productRow);
        }
        private void ActiveDataGridOnProductSelected(DataGridViewRow productRow)
        {
            double mprate = 0;
            int mclstk = 0;
            string mifshortlisted = "";
            string mifsalediscount = "Y";
            int mqty = 0;
            string mlastsalestockid = "";
            string mprodno = "";
            try
            {
                ActiveDataGrid.MainDataGridCurrentRow.Cells[0].Value = productRow.Cells[0].Value;
                _SSSale.ProductID = productRow.Cells[0].Value.ToString();
                mprodno = _SSSale.ProductID;
                double.TryParse(productRow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                _SSSale.PurchaseRate = mprate;
                int.TryParse(productRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);
                _SSSale.Closingstock = mclstk;
                if (mclstk >= 0)
                {
                    mifshortlisted = productRow.Cells["Col_IfShortListed"].Value.ToString().Trim();
                    if (productRow.Cells["Col_IfSaleDisc"].Value != null && productRow.Cells["Col_IfSaleDisc"].Value.ToString() != "")
                        mifsalediscount = productRow.Cells["Col_IfSaleDisc"].Value.ToString();
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = productRow.Cells["Col_ProductName"].Value;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_UOM"].Value = productRow.Cells["Col_UOM"].Value;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Pack"].Value = productRow.Cells["Col_Pack"].Value;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = productRow.Cells["Col_ProdCompShortName"].Value;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Shelf"].Value = productRow.Cells["Col_Shelf"].Value;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = Convert.ToDouble(productRow.Cells["Col_VATPer"].Value.ToString()).ToString("#0.00");
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_LastStockID"].Value = productRow.Cells["Col_LastStockID"].Value;
                    if (productRow.Cells["Col_ProdScheduleDrugCode"].Value != null)
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value = productRow.Cells["Col_ProdScheduleDrugCode"].Value;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_IfSaleDisc"].Value = mifsalediscount.ToString();
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value = productRow.Cells["Col_ProdScheduleDrugCode"].Value;
                    if (productRow.Cells["Col_LastStockID"].Value != null)
                        mlastsalestockid = productRow.Cells["Col_LastStockID"].Value.ToString();
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value = productRow.Cells["Col_ClosingStock"].Value;

                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = true;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = true;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = true;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Amount"].ReadOnly = true;
                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                        int.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);

                    int currentrow = ActiveDataGrid.MainDataGridCurrentRow.Index;
                    int totproductsale = 0;
                    int saleqty = 0;

                    foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                    {
                        if (dr.Index != currentrow && dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                        {
                            if (dr.Cells["Col_Quantity"].Value != null)
                                int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out saleqty);
                            totproductsale += saleqty;

                        }
                    }
                    // 20/4
                    mclstk = mclstk + mqty - totproductsale;
                    if (mclstk == 0 && mifshortlisted != "N" && mqty == 0)
                    {
                        lblMessage.Text = "No Stock";
                        FillDailyShortList();
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = null;
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_UOM"].Value = null;
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Pack"].Value = null;
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = null;
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Shelf"].Value = null;
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = null;
                        ActiveDataGrid.MainDataGridCurrentRow.Cells[0].Value = null;
                        ActiveDataGrid.RefreshMe();
                        ActiveDataGrid.SetFocus(1);
                    }
                    else
                    {
                        lblMessage.Text = "Product Stock :" + mclstk.ToString() + " : ";
                        try
                        {
                            if (mprodno != "")
                                FillLastSaleDataFromMasterProduct();
                        }
                        catch (Exception ex) { Log.WriteError(ex.ToString()); }
                        ActiveDataGrid.ColumnsMain["Col_Quantity"].ReadOnly = false;
                        ActiveDataGrid.SetFocus(11);
                    }
                }
                if (General.CurrentSetting.MsetSaleShowOnlyMRPInCounterSale == "Y")
                {
                    ActiveDataGrid.ColumnsBatchList["Col_PurchaseRate"].Visible = false;
                    ActiveDataGrid.ColumnsBatchList["Col_SaleRate"].Visible = false;
                }
                else
                {
                    ActiveDataGrid.ColumnsBatchList["Col_PurchaseRate"].Visible = true;
                    ActiveDataGrid.ColumnsBatchList["Col_SaleRate"].Visible = true;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }


        private void mpPVC1_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            ActiveDataGridOnCellEntered(e);
        }
        private void mpPVC2_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            ActiveDataGridOnCellEntered(e);
        }
        private void mpPVC3_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            ActiveDataGridOnCellEntered(e);
        }
        private void mpPVC4_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            ActiveDataGridOnCellEntered(e);
        }
        private void mpPVC5_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            ActiveDataGridOnCellEntered(e);
        }
        private void mpPVC6_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            ActiveDataGridOnCellEntered(e);
        }
        private void ActiveDataGridOnCellEntered(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (ActiveDataGrid == mpPVC1)
                        ActiveDataGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn1.BackColor;
                    else if (ActiveDataGrid == mpPVC2)
                        ActiveDataGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn2.BackColor;
                    else if (ActiveDataGrid == mpPVC3)
                        ActiveDataGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn3.BackColor;
                    else if (ActiveDataGrid == mpPVC4)
                        ActiveDataGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn4.BackColor;
                    else if (ActiveDataGrid == mpPVC5)
                        ActiveDataGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn5.BackColor;
                    else if (ActiveDataGrid == mpPVC6)
                        ActiveDataGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn6.BackColor;
                }

                if (e.ColumnIndex == 11) // Quantity
                {


                    if (e.RowIndex >= 0)
                    {
                        int mbatchstock = 0;
                        string mprodno = "";
                        string mbtno = "";
                        string mrp = "";
                        if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                            mprodno = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                        if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                            mbtno = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString();
                        if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                            mrp = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString();
                        int currentrow = e.RowIndex;
                        int totbatchsale = 0;
                        int totproductsale = 0;
                        int saleqty = 0;

                        if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value != null)
                            int.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value.ToString().Trim(), out mbatchstock);

                        foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                        {
                            if (dr.Cells["Col_ProductID"].Value != null && dr.Index != currentrow && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                            {
                                int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out saleqty);
                                totproductsale += saleqty;
                                if (dr.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno && dr.Cells["Col_MRP"].Value.ToString().Trim() == mrp)
                                {
                                    totbatchsale += saleqty;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        private void mpPVC1_OnBatchSelected(DataGridViewRow batchRow)
        {
            ActiveDataGridOnBatchSelected(batchRow);
        }
        private void mpPVC2_OnBatchSelected(DataGridViewRow batchRow)
        {
            ActiveDataGridOnBatchSelected(batchRow);
        }
        private void mpPVC3_OnBatchSelected(DataGridViewRow batchRow)
        {
            ActiveDataGridOnBatchSelected(batchRow);
        }
        private void mpPVC4_OnBatchSelected(DataGridViewRow batchRow)
        {
            ActiveDataGridOnBatchSelected(batchRow);
        }
        private void mpPVC5_OnBatchSelected(DataGridViewRow batchRow)
        {
            ActiveDataGridOnBatchSelected(batchRow);
        }
        private void mpPVC6_OnBatchSelected(DataGridViewRow batchRow)
        {
            ActiveDataGridOnBatchSelected(batchRow);
        }
        private void ActiveDataGridOnBatchSelected(DataGridViewRow batchRow)
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
                mprodno = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                mexpiry = batchRow.Cells["Col_Expiry"].Value.ToString().Trim();
                mexpirydate = batchRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                double.TryParse(batchRow.Cells["Col_MRP"].Value.ToString().Trim(), out mmrpn);
                double.TryParse(batchRow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                double.TryParse(batchRow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                double.TryParse(batchRow.Cells["Col_TradeRate"].Value.ToString().Trim(), out mtraderate);
                int.TryParse(batchRow.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclosingstock);
                mlastsalestockid = batchRow.Cells["Col_StockID"].Value.ToString();
                if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    int.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);

                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = batchRow.Cells["Col_Batchno"].Value;
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = mexpiry;
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_MRP"].Value = mmrpn.ToString("#0.00");
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = msalerate.ToString("#0.00");
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_StockID"].Value = mlastsalestockid;
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_PurchaseRate"].Value = mpurrate.ToString("#0.00");
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value = mtraderate.ToString("#0.00");

                if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value != null)
                    int.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = batchRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value = batchRow.Cells["Col_ClosingStock"].Value.ToString().Trim();
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Amount"].ReadOnly = true;
                string mdt = DateTime.Today.Date.ToString("yyyyMMdd");

                if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                {
                    lblMessage.Text = "Expired Product";
                    ActiveDataGrid.Rows.Remove(ActiveDataGrid.MainDataGridCurrentRow);
                    bool ifblank = false;
                    int currentindex = 0;
                    foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                    {
                        currentindex = dr.Index;
                        if (dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == "")
                            ifblank = true;

                    }
                    if (ifblank == false)
                    {
                        int mindex = ActiveDataGrid.Rows.Add();
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_CustID"].Value = _lastCustIdSelected;
                        ActiveDataGrid.SetFocus(mindex, 1);
                    }
                    else
                        ActiveDataGrid.SetFocus(currentindex, 1);
                }
                else
                {
                    lblMessage.Text = "";
                    int currentrow = ActiveDataGrid.MainDataGridCurrentRow.Index;
                    int totbatchsale = 0;
                    int totproductsale = 0;
                    int saleqty = 0;

                    foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
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
                    // 20/4
                    mclstk = mclstk + mqty - totbatchsale;

                    mclosingstock = mclosingstock - totbatchsale;
                    lblMessage.Text = "Product Stock :" + mclstk.ToString().Trim() + " : Batch Stock :" + mclosingstock.ToString().Trim();
                    _SSSale.CurrentProductStock = mclstk;
                    _SSSale.CurrentBatchStock = mclosingstock;

                    if (_SSSale.CurrentBatchStock <= 0)
                    {
                        lblMessage.Text = "Batch Stock Zero";
                        ActiveDataGrid.SetFocus(1);
                    }
                    else
                    {
                        ActiveDataGrid.SetFocus(11);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        private void mpPVC1_OnRowDeleted(object sender, EventArgs e)
        {
            ActiveDataGridOnRowDeleted(sender);
        }
        private void mpPVC2_OnRowDeleted(object sender, EventArgs e)
        {
            ActiveDataGridOnRowDeleted(sender);
        }
        private void mpPVC3_OnRowDeleted(object sender, EventArgs e)
        {
            ActiveDataGridOnRowDeleted(sender);
        }
        private void mpPVC4_OnRowDeleted(object sender, EventArgs e)
        {
            ActiveDataGridOnRowDeleted(sender);
        }
        private void mpPVC5_OnRowDeleted(object sender, EventArgs e)
        {
            ActiveDataGridOnRowDeleted(sender);
        }
        private void mpPVC6_OnRowDeleted(object sender, EventArgs e)
        {
            ActiveDataGridOnRowDeleted(sender);
        }
        private void ActiveDataGridOnRowDeleted(object sender)
        {
            try
            {
                DataGridViewRow deletedrow = (DataGridViewRow)sender;
                int deletedrowindex = deletedrow.Index;
                CalculateAmount(deletedrowindex);
                lblMessage.Text = "";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            ActiveDataGridTabKeyPressed();
        }
        private void mpPVC2_OnTABKeyPressed(object sender, EventArgs e)
        {
            ActiveDataGridTabKeyPressed();
        }
        private void mpPVC3_OnTABKeyPressed(object sender, EventArgs e)
        {
            ActiveDataGridTabKeyPressed();
        }
        private void mpPVC4_OnTABKeyPressed(object sender, EventArgs e)
        {
            ActiveDataGridTabKeyPressed();
        }
        private void mpPVC5_OnTABKeyPressed(object sender, EventArgs e)
        {
            ActiveDataGridTabKeyPressed();
        }
        private void mpPVC6_OnTABKeyPressed(object sender, EventArgs e)
        {
            ActiveDataGridTabKeyPressed();
        }
        private void ActiveDataGridTabKeyPressed()
        {
            double savingcusttotal = SavingCustomersTotalSale();

            if (savingcusttotal > 0 && (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value == null && ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Amount"].Value == null))
            {
                try
                {
                    DisableAllGrids();
                    pnlFinal.Visible = true;
                    pnlFinal.Enabled = true;
                    FillSpecialDiscount();
                    FillTransactionTypeForTab();
                    pnlPatientDrDetails.Enabled = true;
                    pnlBillAmount.Enabled = true;
                    btnDelete.Enabled = true;
                    txtsavecustno.Text = _lastCustIdSelected.ToString();
                    TxtSaveCustNoTextChanged();
                    cbTransactionType.Focus();
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
        }
        private double SavingCustomersTotalSale()
        {
            double savingcusttot = 0;
            if (ActiveDataGrid == mpPVC1)
                savingcusttot = Convert.ToDouble(txtamount1.Text.ToString());
            else if (ActiveDataGrid == mpPVC2)
                savingcusttot = Convert.ToDouble(txtamount2.Text.ToString());
            else if (ActiveDataGrid == mpPVC3)
                savingcusttot = Convert.ToDouble(txtamount3.Text.ToString());
            else if (ActiveDataGrid == mpPVC4)
                savingcusttot = Convert.ToDouble(txtamount4.Text.ToString());
            else if (ActiveDataGrid == mpPVC5)
                savingcusttot = Convert.ToDouble(txtamount5.Text.ToString());
            else
                savingcusttot = Convert.ToDouble(txtamount6.Text.ToString());
            return savingcusttot;
        }
        private void DisableAllGrids()
        {
            ActiveDataGrid.Enabled = false;
            btn1.Enabled = false;
            btn2.Enabled = false;
            btn3.Enabled = false;
            btn4.Enabled = false;
            btn5.Enabled = false;
            btn6.Enabled = false;
        }
     
        private void CalculateAmount(int deletedrowindex)
        {
            lblMessage.Text = "";
            double mTotalAmount = 0;
           // double mTotalAmountforDiscount = 0;
            double mTotalAmountVAT5 = 0;
            double mTotalAmountVAT12 = 0;

            double mvatper = 0;
            double mvatamount5 = 0;
            double mvatamount12point5 = 0;
            double mtotamtvat0 = 0;

            double mTvatamount5 = 0;
            double mTvatamount12point5 = 0;
            double mTtotamtvat0 = 0;



            double mrate = 0;
            double mamt = 0;
            double mpakn = 0;
            double mqty = 0;
            int itemCount = 0;
            // 9/12/2014   calculate discount after vat and calculate vat after subtracting vat from amt;
            double mmyspecialDiscountper = 0;
            double mmyspecialdiscountamt5 = 0;
            double mmyspecialdiscountamt12point5 = 0;
            double mmyspecialdiscountamtzero = 0;
            double mdiscamt5 = 0;
            double mdiscamt12point5 = 0;
            double mdiscamtzero = 0;
            double mdiscper = 0;
            double mnewamt = 0;
            double mnewamtwithoutmydiscount = 0;
            double mtotalafterdiscountwithoutmydiscount = 0;
            double mtotaldiscountamount5 = 0;
            double mtotaldiscountamount12point5 = 0;
            double mtotaldiscountamountzero = 0;
            double mtotalmyspecialdiscamt5 = 0;
            double mtotalmyspecialdiscamt12point5 = 0;
            double mtotalmyspecialdiscamtzero = 0;
            double mtotalafterdiscount = 0;           
            string ifdiscount = "Y";

            if ( txtPatientName.SelectedID != null && txtPatientName.SelectedID != string.Empty && txtPatientName.SeletedItem.ItemData[1] != null)
                _SSSale.SaleSubType = txtPatientName.SeletedItem.ItemData[1];

            if (txtDiscPercent.Text != null && txtDiscPercent.Text != string.Empty)
                mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());

            if (pnlSpecialDiscount.Visible == true)
            {
                if (rbtnSpecialDiscount4.Checked == true)
                    mmyspecialDiscountper = 0;
                else if (rbtnSpecialDiscount1.Checked == true)
                    mmyspecialDiscountper = Convert.ToDouble(rbtnSpecialDiscount1.Text.ToString());
                else if (rbtnSpecialDiscount2.Checked == true)
                    mmyspecialDiscountper = Convert.ToDouble(rbtnSpecialDiscount2.Text.ToString());
                else if (rbtnSpecialDiscount3.Checked == true)
                    mmyspecialDiscountper = Convert.ToDouble(rbtnSpecialDiscount3.Text.ToString());
                txtMyDiscountPercent.Text = mmyspecialDiscountper.ToString("#0.00");
            }
            else
                txtMyDiscountPercent.Text = "0.00";
            if (_SSSale.SaleSubType == FixAccounts.SubTypeForDebtorSale)
            {
                mmyspecialDiscountper = 0;
                txtMyDiscountPercent.Text = "0.00";
            }


            try
            {
                if (ActiveDataGrid != null)
                {
                    foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                    {
                        mvatamount5 = 0;
                        mvatamount12point5 = 0;
                        mtotamtvat0 = 0;
                        mdiscamt5 = 0;
                        mdiscamt12point5 = 0;
                        mdiscamtzero = 0;
                        mmyspecialdiscountamt5 = 0;
                        mmyspecialdiscountamt12point5 = 0;
                        mmyspecialdiscountamtzero = 0;
                        mnewamtwithoutmydiscount = 0;
                        mnewamt = 0;

                        if (dr.Index != deletedrowindex)
                        {
                            if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "0" && dr.Cells["Col_Quantity"].Value.ToString() != "")
                            {
                                ifdiscount = "Y";
                                mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                                mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                                mpakn = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());
                                if (dr.Cells["Col_IfSaleDisc"].Value != null && dr.Cells["Col_IfSaleDisc"].Value.ToString() != "")
                                    ifdiscount = dr.Cells["Col_IFSaleDisc"].Value.ToString().ToUpper();

                                if (Math.Truncate(mqty / mpakn) == (mqty / mpakn))
                                    mamt = Math.Round((mqty / mpakn) * mrate, 2);
                                else
                                {
                                    mamt = Math.Round(Math.Round(mrate / mpakn, 2) * mqty, 2);
                                    if (Math.Round(mamt, 1) - mamt < 0.05)
                                        mamt = Math.Round(mamt, 1);
                                }

                                dr.Cells["Col_Amount"].Value = (mamt).ToString("#0.00");
                                if (mamt > 0)
                                {
                                    mvatamount12point5 = 0;
                                    mvatamount5 = 0;
                                    mvatper = Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString());
                                    if (mvatper == 5)
                                    {
                                        mvatamount5 = Math.Round((mamt * mvatper) / (100 + mvatper), 2);
                                        mmyspecialdiscountamt5 = Math.Round((mamt - mvatamount5) * mmyspecialDiscountper / 100, 2);
                                        if (ifdiscount != "N")
                                            mdiscamt5 = Math.Round((mamt - mvatamount5) * mdiscper / 100, 2);
                                        else
                                            mdiscamt5 = 0;
                                    }
                                    else if (mvatper == 12.5)
                                    {
                                        mvatamount12point5 = Math.Round((mamt * mvatper) / (100 + mvatper), 4);
                                        mmyspecialdiscountamt12point5 = Math.Round((mamt - mvatamount5) * mmyspecialDiscountper / 100, 4);
                                        if (ifdiscount != "N")
                                            mdiscamt12point5 = Math.Round((mamt - mvatamount12point5) * mdiscper / 100, 4);
                                        else
                                            mdiscamt12point5 = 0;
                                    }
                                    else
                                    {

                                        mmyspecialdiscountamtzero = Math.Round((mamt) * mmyspecialDiscountper / 100, 2);
                                        if (ifdiscount != "N")
                                            mdiscamtzero = Math.Round(mamt * mdiscper / 100, 2);
                                        else
                                            mdiscamtzero = 0;
                                        mtotamtvat0 += mamt - mmyspecialdiscountamtzero - mdiscamtzero;

                                    }
                                    mtotaldiscountamount5 += mdiscamt5;
                                    mtotaldiscountamount12point5 += mdiscamt12point5;
                                    mtotaldiscountamountzero += mdiscamtzero;
                                    mtotalmyspecialdiscamt5 += mmyspecialdiscountamt5;
                                    mtotalmyspecialdiscamt12point5 += mmyspecialdiscountamt12point5;
                                    mtotalmyspecialdiscamtzero += mmyspecialdiscountamtzero;
                                    mnewamt = (mamt - mdiscamt5 - mdiscamt12point5 - mdiscamtzero - mmyspecialdiscountamt5 - mmyspecialdiscountamt12point5 - mmyspecialdiscountamtzero);
                                    mnewamtwithoutmydiscount = (mamt - mdiscamt5 - mdiscamt12point5 - mdiscamtzero);
                                    if (mvatper == 5)
                                    {
                                        mvatamount5 = Math.Round((mnewamt * mvatper) / (100 + mvatper), 2);
                                    }
                                    else if (mvatper == 12.5)
                                    {
                                        mvatamount12point5 = Math.Round((mnewamt * mvatper) / (100 + mvatper), 2);
                                    }

                                    dr.Cells["Col_VATAmount"].Value = (mvatamount12point5 + mvatamount5).ToString("#0.00");
                                    dr.Cells["Col_DiscountAmount"].Value = (mdiscamt5 + mdiscamt12point5 + mdiscamtzero).ToString("#0.00");
                                    dr.Cells["Col_MySpecialDiscountAmount"].Value = mmyspecialdiscountamt5 + mmyspecialdiscountamt12point5 + mmyspecialdiscountamtzero;
                                    mTotalAmount += mamt;
                                    mtotalafterdiscount += mnewamt;
                                    // mtotalafterdiscountwithoutmydiscount += 
                                    itemCount += 1;
                                    //if (ifdiscount != "N")
                                    //    mTotalAmountforDiscount += mamt;
                                    mTvatamount5 += mvatamount5;
                                    mTvatamount12point5 += mvatamount12point5;
                                    mTtotamtvat0 += mtotamtvat0;
                                    if (mvatper == 5)
                                        mTotalAmountVAT5 += (mnewamt - mvatamount5);
                                    else if (mvatper == 12.5)
                                        mTotalAmountVAT12 += (mnewamt - mvatamount12point5);
                                }
                            }
                        }
                    }
                    NoofRows();
                    txtdiscountAmount5.Text = mtotaldiscountamount5.ToString("#0.00");
                    txtDiscountAmount12point5.Text = mtotaldiscountamount12point5.ToString("#0.00");
                    txtMyDiscountAmount5.Text = mtotalmyspecialdiscamt5.ToString("#0.00");
                    txtMyDiscountAmount12point5.Text = mtotalmyspecialdiscamt12point5.ToString("#0.00");
                    txtVatInput5.Text = mTvatamount5.ToString("#0.00");
                    txtVatInput12point5.Text = mTvatamount12point5.ToString("#0.00");
                    txtAmountfor12VAT.Text = mTotalAmountVAT12.ToString("#0.00");
                    txtAmountfor5VAT.Text = mTotalAmountVAT5.ToString("#0.00");
                    txtAmountforZeroVAT.Text = mTtotamtvat0.ToString("#0.00");

                    double mdblDiscAmount = mtotaldiscountamount5 + mtotaldiscountamount12point5 + mtotaldiscountamountzero;
                    double mdblMyDiscAmount = mtotalmyspecialdiscamt12point5 + mtotalmyspecialdiscamt5 + mtotalmyspecialdiscamtzero;
                    txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                    mtotalafterdiscountwithoutmydiscount = mTotalAmount - mdblDiscAmount - mdblMyDiscAmount;
                    txtTotalafterdiscount.Text = mtotalafterdiscountwithoutmydiscount.ToString("#0.00");
                    txtMyDiscountAmountTotal.Text = mdblMyDiscAmount.ToString("#0.00");
                    if (cbRound.Checked == true)
                    {
                        txtRoundAmount.Text = Math.Round(Math.Round(mtotalafterdiscount, 0) - Math.Round(mtotalafterdiscount, 2), 2).ToString("#0.00");
                        txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString()) + mtotalmyspecialdiscamt12point5 + mtotalmyspecialdiscamt5), 2).ToString("#0.00");
                        txtBillAmount2.Text = txtBillAmount.Text;
                    }
                    else
                    {
                        txtRoundAmount.Text = "0.00";
                        txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString()) + mdblMyDiscAmount), 2).ToString("#0.00");
                        txtBillAmount2.Text = txtBillAmount.Text;
                    }

                    if (ActiveDataGrid == mpPVC1)
                    {
                        txtamount1.Text = mTotalAmount.ToString("#0.00");
                        txtitems1.Text = itemCount.ToString();
                        //txtVatInput12point51.Text = mTvatamount12point5.ToString("#0.00");
                        //txtVatInput51.Text = mTvatamount5.ToString("0.00");
                        //txtSaleAmountForDiscount1.Text = mTotalAmountforDiscount.ToString("0.00");
                        //txtAmountforZeroVAT1.Text = mtotamtvat0.ToString("0.00");
                        //txtVATAmount51.Text = mTotalAmountVAT5.ToString("#0.00");
                        //txtVATAmount12point51.Text = mTotalAmountVAT12.ToString("#0.00");
                    }
                    else if (ActiveDataGrid == mpPVC2)
                    {

                        txtamount2.Text = mTotalAmount.ToString("#0.00");
                        txtitems2.Text = itemCount.ToString();
                        //txtVatInput12point52.Text = mTvatamount12point5.ToString("#0.00");
                        //txtVatInput52.Text = mTvatamount5.ToString("0.00");
                        //txtSaleAmountForDiscount2.Text = mTotalAmountforDiscount.ToString("0.00");
                        //txtAmountforZeroVAT2.Text = mtotamtvat0.ToString("0.00");
                        //txtVATAmount52.Text = mTotalAmountVAT5.ToString("#0.00");
                        //txtVATAmount12point52.Text = mTotalAmountVAT12.ToString("#0.00");
                    }
                    else if (ActiveDataGrid == mpPVC3)
                    {

                        txtamount3.Text = mTotalAmount.ToString("#0.00");
                        txtitems3.Text = itemCount.ToString();
                        //txtVatInput12point53.Text = mTvatamount12point5.ToString("#0.00");
                        //txtVatInput53.Text = mTvatamount5.ToString("0.00");
                        //txtSaleAmountForDiscount3.Text = mTotalAmountforDiscount.ToString("0.00");
                        //txtAmountforZeroVAT3.Text = mtotamtvat0.ToString("0.00");
                        //txtVATAmount53.Text = mTotalAmountVAT5.ToString("#0.00");
                        //txtVATAmount12point53.Text = mTotalAmountVAT12.ToString("#0.00");
                    }
                    else if (ActiveDataGrid == mpPVC4)
                    {
                        txtamount4.Text = mTotalAmount.ToString("#0.00");
                        txtitems4.Text = itemCount.ToString();
                        //txtVatInput12point54.Text = mTvatamount12point5.ToString("#0.00");
                        //txtVatInput54.Text = mTvatamount5.ToString("0.00");
                        //txtSaleAmountForDiscount4.Text = mTotalAmountforDiscount.ToString("0.00");
                        //txtAmountforZeroVAT4.Text = mtotamtvat0.ToString("0.00");
                        //txtVATAmount54.Text = mTotalAmountVAT5.ToString("#0.00");
                        //txtVATAmount12point54.Text = mTotalAmountVAT12.ToString("#0.00");
                    }
                    else if (ActiveDataGrid == mpPVC5)
                    {
                        txtamount5.Text = mTotalAmount.ToString("#0.00");
                        txtitems5.Text = itemCount.ToString();
                        //txtVatInput12point55.Text = mTvatamount12point5.ToString("#0.00");
                        //txtVatInput55.Text = mTvatamount5.ToString("0.00");
                        //txtSaleAmountForDiscount5.Text = mTotalAmountforDiscount.ToString("0.00");
                        //txtAmountforZeroVAT5.Text = mtotamtvat0.ToString("0.00");
                        //txtVATAmount55.Text = mTotalAmountVAT5.ToString("#0.00");
                        //txtVATAmount12point55.Text = mTotalAmountVAT12.ToString("#0.00");
                    }
                    else
                    {
                        txtamount6.Text = mTotalAmount.ToString("#0.00");
                        txtitems6.Text = itemCount.ToString();
                        //txtVatInput12point56.Text = mTvatamount12point5.ToString("#0.00");
                        //txtVatInput56.Text = mTvatamount5.ToString("0.00");
                        //txtSaleAmountForDiscount6.Text = mTotalAmountforDiscount.ToString("0.00");
                        //txtAmountforZeroVAT6.Text = mtotamtvat0.ToString("0.00");
                        //txtVATAmount56.Text = mTotalAmountVAT5.ToString("#0.00");
                        //txtVATAmount12point56.Text = mTotalAmountVAT12.ToString("#0.00");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }
        //private void CalculateAllAmounts()
        //{
        //    lblMessage.Text = "";
        //    double mdblAmount;
        //    double maddon = 0;
        //    double mtotalafterdiscount = 0;
        //    double.TryParse(txtNetAmount.Text.ToString(), out mdblAmount);
        //    double mdblAmountforDiscount;
        //    double.TryParse(txtSaleAmountForDiscount1.Text.ToString(), out mdblAmountforDiscount);
        //    double mdblDiscPer;
        //    double.TryParse(txtDiscPercent.Text.ToString(), out mdblDiscPer);
        //    double mdblDiscAmount;
        //    double.TryParse(txtDiscAmount.Text.ToString(), out mdblDiscAmount);
        //    try
        //    {               
        //        mdblDiscAmount = Math.Round(((mdblAmountforDiscount) * mdblDiscPer / 100), 2);
        //        txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
        //        mtotalafterdiscount = Math.Round(mdblAmount - mdblDiscAmount + maddon, 2);
        //        txtTotalafterdiscount.Text = mtotalafterdiscount.ToString("#0.00");

        //        if (cbRound.Checked == true)
        //        {
        //            txtRoundAmount.Text = Math.Round(Math.Round(mtotalafterdiscount, 0) - Math.Round(mtotalafterdiscount, 2), 2).ToString("#0.00");
        //            txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
        //            txtBillAmount2.Text = txtBillAmount.Text;
        //        }
        //        else
        //        {
        //            txtRoundAmount.Text = "0.00";
        //            txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
        //            txtBillAmount2.Text = txtBillAmount.Text;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}
        private void NoofRows()
        {
            int itemCount = 0;
            bool ifH1 = false;
            try
            {
                if (ActiveDataGrid != null && ActiveDataGrid.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                    {
                        if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                        {
                            itemCount += 1;
                        }
                        if (dr.Cells["Col_ProdScheduleDrugCode"].Value != null && dr.Cells["Col_ProdScheduleDrugCode"].Value.ToString() != string.Empty)
                        {
                            string hh = dr.Cells["Col_ProdScheduleDrugCode"].Value.ToString();
                            if (hh == "H1" || hh == "H2" || hh == "H3" || hh == "H4" || hh == "H5")
                            {
                                ifH1 = true;
                            }
                        }
                    }
                    txtNoOfRows.Text = itemCount.ToString().Trim();
                    if (ifH1)
                        lblMessage.Text = "Bill Contains H1 Product";
                    else
                        lblMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private bool FillLastSaleDataFromMasterProduct()
        {
            DataRow dr = null;
            DataRow invdr = null;
            string mprodno = "";
            string mshelf = "";
            int mprodclosingstock = 0;

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
                dr = drprod.ReadLastSaleByID(ActiveDataGrid.MainDataGridCurrentRow.Cells[0].Value.ToString());
                mprodno = ActiveDataGrid.MainDataGridCurrentRow.Cells[0].Value.ToString();
                if (dr["ProdLastSaleStockID"] != null && dr["ProdLastSaleStockID"].ToString() != "")
                    mlastsalestockid = dr["ProdLastSaleStockID"].ToString();
                mprodclosingstock = Convert.ToInt32(dr["ProdClosingStock"].ToString().Trim());
                mshelf = dr["ShelfCode"].ToString().Trim();

                if (mlastsalestockid != "")
                {
                    SsStock invss = new SsStock();
                    invdr = invss.GetStockByStockID(mlastsalestockid);
                }

                if (invdr != null)
                {
                    int.TryParse(invdr["ClosingStock"].ToString().Trim(), out mclosingstock);

                    if (mclosingstock > 0)
                    {

                        mexpiry = invdr["Expiry"].ToString().Trim();
                        mexpirydate = invdr["ExpiryDate"].ToString().Trim();
                        double.TryParse(invdr["MRP"].ToString().Trim(), out mmrpn);
                        double.TryParse(invdr["SaleRate"].ToString().Trim(), out msalerate);
                        double.TryParse(invdr["PurchaseRate"].ToString().Trim(), out mpurrate);
                        double.TryParse(invdr["TradeRate"].ToString().Trim(), out mtraderate);
                        mbatchno = invdr["BatchNumber"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }

        #endregion

        #region "Public Methods"

        public void Initialize()
        {

        }

        private void ClearControls()
        {
            ClearTotals();
        }

        private void ClearAllTotals()
        {
            try
            {
                txtamount1.Text = "0.00";
                txtamount2.Text = "0.00";
                txtamount3.Text = "0.00";
                txtamount4.Text = "0.00";
                txtamount5.Text = "0.00";
                txtamount6.Text = "0.00";

                txtitems1.Text = "0";
                txtitems2.Text = "0";
                txtitems3.Text = "0";
                txtitems4.Text = "0";
                txtitems5.Text = "0";
                txtitems6.Text = "0";

                txtAmountforZeroVAT.Text = "0";
                //txtAmountforZeroVAT1.Text = "0";
                //txtAmountforZeroVAT2.Text = "0";
                //txtAmountforZeroVAT3.Text = "0";
                //txtAmountforZeroVAT4.Text = "0";
                //txtAmountforZeroVAT5.Text = "0";
                //txtAmountforZeroVAT6.Text = "0";

                txtAmountfor5VAT.Text = "0";
                ////txtVATAmount51.Text = "0";
                //txtVATAmount52.Text = "0";
                //txtVATAmount53.Text = "0";
                //txtVATAmount54.Text = "0";
                //txtVATAmount55.Text = "0";
                //txtVATAmount56.Text = "0";

                txtAmountfor12VAT.Text = "0";
                //txtVATAmount12point51.Text = "0";
                //txtVATAmount12point52.Text = "0";
                //txtVATAmount12point53.Text = "0";
                //txtVATAmount12point54.Text = "0";
                //txtVATAmount12point55.Text = "0";
                //txtVATAmount12point56.Text = "0";

                txtVatInput12point5.Text = "0";
                //txtVatInput12point51.Text = "0";
                //txtVatInput12point52.Text = "0";
                //txtVatInput12point53.Text = "0";
                //txtVatInput12point54.Text = "0";
                //txtVatInput12point55.Text = "0";
                //txtVatInput12point56.Text = "0";

                txtVatInput5.Text = "0";
                //txtVatInput51.Text = "0";
                //txtVatInput52.Text = "0";
                //txtVatInput53.Text = "0";
                //txtVatInput54.Text = "0";
                //txtVatInput55.Text = "0";
                //txtVatInput56.Text = "0";

                txtSaleAmountForDiscount.Text = "0";
                //txtSaleAmountForDiscount1.Text = "0";
                //txtSaleAmountForDiscount2.Text = "0";
                //txtSaleAmountForDiscount3.Text = "0";
                //txtSaleAmountForDiscount4.Text = "0";
                //txtSaleAmountForDiscount5.Text = "0";
                //txtSaleAmountForDiscount6.Text = "0";

                txtDiscPercent.Text = "0";
                _lastCustIdSelected = "1";
                txtVouchernumber.Clear();
                txtVouType.Text = "   ";
                datePickerBillDate.ResetText();
                txtBillAmount.Text = "0.00";
                txtBillAmount2.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                mcbDoctor.DataSource = null;
                mcbDoctor.Text = "";
                txtDoctorAddress.Text = "";
                txtOperator.Text = "";
                txtNoOfRows.Text = "";
                txtMobileNumber.Text = "";
                _SSSale.IfNewPatient = "N";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }



        private void ClearTotals()
        {
            try
            {
                txtOperator.Text = "";
                if (_SSSale.CustNumber == 1)
                {
                    txtamount1.Text = "0.00";
                    txtitems1.Text = "0";
                    //txtAmountforZeroVAT1.Text = "0";
                    //txtVatInput12point51.Text = "0";
                    //txtVatInput51.Text = "0";
                    //txtSaleAmountForDiscount1.Text = "0";
                    //txtVATAmount51.Text = "0";
                    //txtVATAmount12point51.Text = "0";
                }

                if (_SSSale.CustNumber == 2)
                {
                    txtamount2.Text = "0.00";
                    txtitems2.Text = "0";
                    //txtAmountforZeroVAT2.Text = "0";
                    //txtVatInput12point52.Text = "0";
                    //txtVatInput52.Text = "0";
                    //txtSaleAmountForDiscount2.Text = "0";
                    //txtVATAmount52.Text = "0";
                    //txtVATAmount12point52.Text = "0";
                }
                if (_SSSale.CustNumber == 3)
                {
                    txtamount3.Text = "0.00";
                    txtitems3.Text = "0";
                    //txtAmountforZeroVAT3.Text = "0";
                    //txtVatInput12point53.Text = "0";
                    //txtVatInput53.Text = "0";
                    //txtSaleAmountForDiscount3.Text = "0";
                    //txtVATAmount53.Text = "0";
                    //txtVATAmount12point53.Text = "0";
                }

                if (_SSSale.CustNumber == 4)
                {
                    txtamount4.Text = "0.00";
                    txtitems4.Text = "0";
                    //txtAmountforZeroVAT4.Text = "0";
                    //txtVatInput12point54.Text = "0";
                    //txtVatInput54.Text = "0";
                    //txtSaleAmountForDiscount4.Text = "0";
                    //txtVATAmount54.Text = "0";
                    //txtVATAmount12point54.Text = "0";
                }
                if (_SSSale.CustNumber == 5)
                {
                    txtamount5.Text = "0.00";
                    txtitems5.Text = "0";
                    //txtAmountforZeroVAT5.Text = "0";
                    //txtVatInput12point55.Text = "0";
                    //txtVatInput55.Text = "0";
                    //txtSaleAmountForDiscount5.Text = "0";
                    //txtVATAmount55.Text = "0";
                    //txtVATAmount12point55.Text = "0";
                }

                if (_SSSale.CustNumber == 6)
                {
                    txtamount6.Text = "0.00";
                    txtitems6.Text = "0";
                    //txtAmountforZeroVAT6.Text = "0";
                    //txtVatInput12point56.Text = "0";
                    //txtVatInput56.Text = "0";
                    //txtSaleAmountForDiscount6.Text = "0";
                    //txtVATAmount56.Text = "0";
                    //txtVATAmount12point56.Text = "0";
                }
                txtVouchernumber.Clear();
                txtVouType.Text = "   ";
                datePickerBillDate.ResetText();
                txtBillAmount.Text = "0.00";
                txtBillAmount2.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtdiscountAmount5.Text = "0.00";
                txtDiscountAmount12point5.Text = "0.00";
                txtTotalafterdiscount.Text = "0.00";
                txtMyDiscountPercent.Text = "0.00";
                txtMyDiscountAmount12point5.Text = "0.00";
                txtMyDiscountAmount5.Text = "0.00";
                txtMyDiscountAmountTotal.Text = "0.00";
                txtAmountforZeroVAT.Text = "0";
                txtVatInput12point5.Text = "0";
                txtVatInput5.Text = "0";
                txtAmountfor5VAT.Text = "0";
                txtAmountfor12VAT.Text = "0";
                txtRoundAmount.Text = "0.00";
                txtSaleAmountForDiscount.Text = "0";
                mcbDoctor.SelectedID = "";
                txtDoctorAddress.Text = "";
                txtPatientName.SelectedID = "";
                txtMobileNumber.Text = "";
                _SSSale.PatientID = "";
                _SSSale.AccountID = "";
                _SSSale.CrdbVouType = "";
                _SSSale.SaleSubType = "";
                txtAddress.SelectedID = "";
                txtPatientName.Text = "";
                txtAddress.Text = "";
                mcbDoctor.Text = "";
                if (ActiveDataGrid != null)
                {
                    ActiveDataGrid.SetFocus(1);
                    ActiveDataGrid.DataSourceProductList = General.ProductList;
                    ActiveDataGrid.BindGridProductList();
                }
                NoofRows();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private Color GetBackColorByCustID(string custID)
        {
            Color retValue = btn1.BackColor;
            try
            {
                switch (custID)
                {
                    case "1":
                        retValue = btn1.BackColor;
                        break;
                    case "2":
                        retValue = btn2.BackColor;
                        break;
                    case "3":
                        retValue = btn3.BackColor;
                        break;
                    case "4":
                        retValue = btn4.BackColor;
                        break;
                    case "5":
                        retValue = btn5.BackColor;
                        break;
                    case "6":
                        retValue = btn6.BackColor;
                        break;

                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        private void ChangeBackColour(PSProductViewControl activeDataGrid)
        {

            if (activeDataGrid == mpPVC1)
            {
                activeDataGrid.BringToFront();
                txtamount1.BackColor = btn1.BackColor;
                txtitems1.BackColor = btn1.BackColor;
                txtamount2.BackColor = Color.White;
                txtitems2.BackColor = Color.White;
                txtamount3.BackColor = Color.White;
                txtitems3.BackColor = Color.White;
                txtamount4.BackColor = Color.White;
                txtitems4.BackColor = Color.White;
                txtamount5.BackColor = Color.White;
                txtitems5.BackColor = Color.White;
                txtamount6.BackColor = Color.White;
                txtitems6.BackColor = Color.White;
            }
            else if (activeDataGrid == mpPVC2)
            {
                activeDataGrid.BringToFront();
                txtamount2.BackColor = btn2.BackColor;
                txtitems2.BackColor = btn2.BackColor;
                txtamount1.BackColor = Color.White;
                txtitems1.BackColor = Color.White;
                txtamount3.BackColor = Color.White;
                txtitems3.BackColor = Color.White;
                txtamount4.BackColor = Color.White;
                txtitems4.BackColor = Color.White;
                txtamount5.BackColor = Color.White;
                txtitems5.BackColor = Color.White;
                txtamount6.BackColor = Color.White;
                txtitems6.BackColor = Color.White;
            }


            else if (activeDataGrid == mpPVC3)
            {
                activeDataGrid.BringToFront();
                txtamount3.BackColor = btn3.BackColor;
                txtitems3.BackColor = btn3.BackColor;
                txtamount1.BackColor = Color.White;
                txtitems1.BackColor = Color.White;
                txtamount2.BackColor = Color.White;
                txtitems2.BackColor = Color.White;
                txtamount4.BackColor = Color.White;
                txtitems4.BackColor = Color.White;
                txtamount5.BackColor = Color.White;
                txtitems5.BackColor = Color.White;
                txtamount6.BackColor = Color.White;
                txtitems6.BackColor = Color.White;

            }
            else if (activeDataGrid == mpPVC4)
            {
                activeDataGrid.BringToFront();
                txtamount4.BackColor = btn4.BackColor;
                txtitems4.BackColor = btn4.BackColor;
                txtamount1.BackColor = Color.White;
                txtitems1.BackColor = Color.White;
                txtamount2.BackColor = Color.White;
                txtitems2.BackColor = Color.White;
                txtamount3.BackColor = Color.White;
                txtitems3.BackColor = Color.White;
                txtamount5.BackColor = Color.White;
                txtitems5.BackColor = Color.White;
                txtamount6.BackColor = Color.White;
                txtitems6.BackColor = Color.White;

            }
            else if (activeDataGrid == mpPVC5)
            {
                activeDataGrid.BringToFront();
                txtamount5.BackColor = btn5.BackColor;
                txtitems5.BackColor = btn5.BackColor;
                txtamount1.BackColor = Color.White;
                txtitems1.BackColor = Color.White;
                txtamount2.BackColor = Color.White;
                txtitems2.BackColor = Color.White;
                txtamount3.BackColor = Color.White;
                txtitems3.BackColor = Color.White;
                txtamount4.BackColor = Color.White;
                txtitems4.BackColor = Color.White;
                txtamount6.BackColor = Color.White;
                txtitems6.BackColor = Color.White;

            }
            else if (activeDataGrid == mpPVC6)
            {
                activeDataGrid.BringToFront();
                txtamount6.BackColor = btn6.BackColor;
                txtitems6.BackColor = btn6.BackColor;
                txtamount1.BackColor = Color.White;
                txtitems1.BackColor = Color.White;
                txtamount2.BackColor = Color.White;
                txtitems2.BackColor = Color.White;
                txtamount3.BackColor = Color.White;
                txtitems3.BackColor = Color.White;
                txtamount4.BackColor = Color.White;
                txtitems4.BackColor = Color.White;
                txtamount5.BackColor = Color.White;
                txtitems5.BackColor = Color.White;
            }
            ActiveDataGrid.DataSourceProductList = General.ProductList;
            ActiveDataGrid.BindGridProductList();

            activeDataGrid.SetFocus(1);
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

        #region "Events"

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dResult;
            txtNetAmount.Text = "0.00";
            txtBillAmount.Text = "0.00";
            txtBillAmount2.Text = "0.00";
            int lastrowindex = 0;
            try
            {
                dResult = MessageBox.Show("Remove all record for this customer?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dResult == DialogResult.Yes)
                {
                    rowCollection = new List<DataGridViewRow>();
                    foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                    {
                        rowCollection.Add(dr);

                    }
                    foreach (DataGridViewRow prodrow in rowCollection)
                    {
                        ActiveDataGrid.Rows.Remove(prodrow);
                    }

                    rowCollection = new List<DataGridViewRow>();

                }
                txtsavecustno.Text = "";
                CalculateAmount(-1);
                EnableAllGrids();
                ActiveDataGrid.Rows.Add();
                ChangeBackColour(ActiveDataGrid);
                SetGridRowColour();
                pnlFinal.Visible = false;
                ActiveDataGrid.SetFocus(lastrowindex, 1);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        //private void btnPrint_Click(object sender, EventArgs e)
        //{
        //    //          PrintBill();
        //}

        private void txtPatientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                cbTransactionType.Focus();
        }

        private void btn1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn1Click();
            }
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            btn1Click();
        }
        private void btn1Click()
        {
            ActiveDataGrid = mpPVC1;
            ChangeBackColour(ActiveDataGrid);
            _lastCustIdSelected = "1";
            txtsavecustno.Text = "1";
            NoofRows();
        }



        private void btn2_KeyDown(object sender, KeyEventArgs e)
        {
            btn2Click();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            btn2Click();
        }
        private void btn2Click()
        {
            ActiveDataGrid = mpPVC2;
            ChangeBackColour(ActiveDataGrid);
            _lastCustIdSelected = "2";
            txtsavecustno.Text = "2";
            NoofRows();
        }
        private void btn3_KeyDown(object sender, KeyEventArgs e)
        {
            btn3Click();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            btn3Click();
        }
        private void btn3Click()
        {
            ActiveDataGrid = mpPVC3;
            ChangeBackColour(ActiveDataGrid);
            _lastCustIdSelected = "3";
            txtsavecustno.Text = "3";
            NoofRows();
        }


        private void btn4_KeyDown(object sender, KeyEventArgs e)
        {
            btn4Click();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            btn4Click();
        }
        private void btn4Click()
        {
            ActiveDataGrid = mpPVC4;
            ChangeBackColour(ActiveDataGrid);
            _lastCustIdSelected = "4";
            txtsavecustno.Text = "4";
            NoofRows();
        }

        private void btn5_KeyDown(object sender, KeyEventArgs e)
        {
            btn5Click();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            btn5Click();
        }
        private void btn5Click()
        {
            ActiveDataGrid = mpPVC5;
            ChangeBackColour(ActiveDataGrid);
            _lastCustIdSelected = "5";
            txtsavecustno.Text = "5";
            NoofRows();
        }

        private void btn6_KeyDown(object sender, KeyEventArgs e)
        {
            btn6Click();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            btn6Click();
        }
        private void btn6Click()
        {
            ActiveDataGrid = mpPVC6;
            ChangeBackColour(ActiveDataGrid);
            _lastCustIdSelected = "6";
            txtsavecustno.Text = "6";
            NoofRows();
        }

        private void txtsavecustno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtSaveCustNoTextChanged();
            }
        }

        private void txtsavecustno_TextChanged(object sender, EventArgs e)
        {
            TxtSaveCustNoTextChanged();
        }
        private void TxtSaveCustNoTextChanged()
        {
            lblMessage.Text = "";
            int msavenumber = 0;
            double mamt1 = 0;
            double mamt2 = 0;
            double mamt3 = 0;
            double mamt4 = 0;
            double mamt5 = 0;
            double mamt6 = 0;


            try
            {
                if (txtsavecustno.Text != null && txtsavecustno.Text.ToString() != "")
                    int.TryParse(txtsavecustno.Text.ToString(), out msavenumber);
                if (msavenumber != 0)
                    _SSSale.CustNumber = msavenumber;
                if (msavenumber == 0)
                {
                    tsBtnSave.Enabled = false;
                    tsBtnSavenPrint.Enabled = false;
                }
                else
                {
                    tsBtnSave.Enabled = true;
                    tsBtnSavenPrint.Enabled = true;
                }

                if (txtamount1.Text != null)
                    double.TryParse(txtamount1.Text.ToString(), out mamt1);
                if (txtamount2.Text != null)
                    double.TryParse(txtamount2.Text.ToString(), out mamt2);
                if (txtamount3.Text != null)
                    double.TryParse(txtamount3.Text.ToString(), out mamt3);
                if (txtamount4.Text != null)
                    double.TryParse(txtamount4.Text.ToString(), out mamt4);
                if (txtamount5.Text != null)
                    double.TryParse(txtamount5.Text.ToString(), out mamt5);
                if (txtamount6.Text != null)
                    double.TryParse(txtamount6.Text.ToString(), out mamt6);

                if (msavenumber == 1 && mamt1 > 0)
                {
                    txtNetAmount.Text = mamt1.ToString("#0.00");
                    //txtVatInput5.Text = txtVatInput51.Text.ToString();
                    //txtVatInput12point5.Text = txtVatInput12point51.Text.ToString();
                    //txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount1.Text.ToString();
                    //txtAmountforZeroVAT.Text = txtAmountforZeroVAT1.Text.ToString();
                    //txtAmountfor5VAT.Text = txtVATAmount51.Text.ToString();
                    //txtAmountfor12VAT.Text = txtVATAmount12point51.Text.ToString();
                    CalculateAmount(-1);
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    cbTransactionType.Focus();

                }
                else if (msavenumber == 2 && mamt2 > 0)
                {
                    txtNetAmount.Text = mamt2.ToString("#0.00");
                    //txtVatInput5.Text = txtVatInput52.Text.ToString();
                    //txtVatInput12point5.Text = txtVatInput12point52.Text.ToString();
                    //txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount2.Text.ToString();
                    //txtAmountforZeroVAT.Text = txtAmountforZeroVAT2.Text.ToString();
                    //txtAmountfor5VAT.Text = txtVATAmount52.Text.ToString();
                    //txtAmountfor12VAT.Text = txtVATAmount12point52.Text.ToString();
                    CalculateAmount(-1);
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 3 && mamt3 > 0)
                {
                    txtNetAmount.Text = mamt3.ToString("#0.00");
                    //txtVatInput5.Text = txtVatInput53.Text.ToString();
                    //txtVatInput12point5.Text = txtVatInput12point53.Text.ToString();
                    //txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount3.Text.ToString();
                    //txtAmountforZeroVAT.Text = txtAmountforZeroVAT3.Text.ToString();
                    //txtAmountfor5VAT.Text = txtVATAmount53.Text.ToString();
                    //txtAmountfor12VAT.Text = txtVATAmount12point53.Text.ToString();
                    CalculateAmount(-1);
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 4 && mamt4 > 0)
                {
                    txtNetAmount.Text = mamt4.ToString("#0.00");
                    //txtVatInput5.Text = txtVatInput54.Text.ToString();
                    //txtVatInput12point5.Text = txtVatInput12point54.Text.ToString();
                    //txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount4.Text.ToString();
                    //txtAmountforZeroVAT.Text = txtAmountforZeroVAT4.Text.ToString();
                    //txtAmountfor5VAT.Text = txtVATAmount54.Text.ToString();
                    //txtAmountfor12VAT.Text = txtVATAmount12point54.Text.ToString();
                 //   CalculateAllAmounts();
                    CalculateAmount(-1);
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 5 && mamt5 > 0)
                {
                    txtNetAmount.Text = mamt5.ToString("#0.00");
                    //txtVatInput5.Text = txtVatInput55.Text.ToString();
                    //txtVatInput12point5.Text = txtVatInput12point55.Text.ToString();
                    //txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount5.Text.ToString();
                    //txtAmountforZeroVAT.Text = txtAmountforZeroVAT5.Text.ToString();
                    //txtAmountfor5VAT.Text = txtVATAmount55.Text.ToString();
                    //txtAmountfor12VAT.Text = txtVATAmount12point55.Text.ToString();
                 //   CalculateAllAmounts();
                    CalculateAmount(-1);
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 6 && mamt6 > 0)
                {
                    txtNetAmount.Text = mamt6.ToString("#0.00");

                    //txtVatInput5.Text = txtVatInput56.Text.ToString();
                    //txtVatInput12point5.Text = txtVatInput12point56.Text.ToString();
                    //txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount6.Text.ToString();
                    //txtAmountforZeroVAT.Text = txtAmountforZeroVAT6.Text.ToString();
                    //txtAmountfor5VAT.Text = txtVATAmount56.Text.ToString();
                    //txtAmountfor12VAT.Text = txtVATAmount12point56.Text.ToString();
                  //  CalculateAllAmounts();
                    CalculateAmount(-1);
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    cbTransactionType.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID.ToString() != "")
            {
                txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2];
                if (txtDiscPercent.Visible)
                    txtNarration.Focus();
                else
                    if (txtOperator.Visible)
                        txtOperator.Focus();
                    else
                        tsBtnSave.Select();
            }
            else
            {
                txtDoctorAddress.Focus();
            }

        }
        private void txtDoctorAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDiscPercent.Visible)
                    txtNarration.Focus();
                else
                    if (txtOperator.Visible)
                        txtOperator.Focus();
                    else
                        tsBtnSave.Select();
            }
            else if (e.KeyCode == Keys.Up)
                mcbDoctor.Focus();
        }
        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                CalculateAmount(-1);
            //    CalculateAllAmounts();
                txtOperator.Focus();
            }
        }

        private void cbSavePatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbSavePatient.Checked == true)
                    cbSavePatient.Checked = false;
                else
                    cbSavePatient.Checked = true;
            }
        }
        private void txtOperator_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtPatientName.SelectedID == null || txtPatientName.SelectedID == "")
                    {
                        cbSavePatient.Visible = true;
                        cbSavePatient.Focus();
                    }
                    else
                    {
                        cbSavePatient.Visible = false;
                        cbSavePatient.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void txtPatientName_EnterKeyPressed(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            try
            {

                if (txtPatientName.SelectedID != null && txtPatientName.SelectedID != "" && txtPatientName.Text.ToString() != txtPatientName.SeletedItem.ItemData[2])
                {
                    txtPatientName.SelectedID = "";

                    txtAddress.Focus();
                }
                else
                {
                    if (txtPatientName.SelectedID == null || txtPatientName.SelectedID == "")
                    {
                        txtAddress.Enabled = true;
                        txtAddress.Focus();
                    }
                    else
                    {
                        txtAddress.Text = txtPatientName.SeletedItem.ItemData[3];

                        mcbDoctor.SelectedID = txtPatientName.SeletedItem.ItemData[6];
                        _SSSale.AccCode = txtPatientName.SeletedItem.ItemData[1];

                        txtPatientName.Enabled = false;
                        txtAddress.Enabled = false;

                        mcbDoctor.Enabled = true;
                        if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "" && mcbDoctor.SeletedItem.ItemData[2] != null)
                            txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2];

                        if (General.CurrentSetting.MsetAskOperatorOtherThanVoucherSale == "Y")
                        {
                            if (txtOperator.Visible == false)
                            {
                                txtOperator.Visible = true;
                                lblOperator.Visible = true;
                            }
                        }
                        if (mcbDoctor.SelectedID == null || mcbDoctor.SelectedID == string.Empty)
                            mcbDoctor.Focus();
                        else
                            txtNarration.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void txtAddress_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                txtMobileNumber.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void uclSubstituteControl1_OnProductSelected(string productID)
        {
            ActiveDataGrid.LoadProduct(productID);
            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = productID;
        }

        private void btnSubstitute_Click(object sender, EventArgs e)
        {
            uclSubstituteControl1.Initialize();
            uclSubstituteControl1.Visible = true;
            uclSubstituteControl1.BringToFront();
        }

        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbDoctor.SelectedID;
                FillDoctorCombo();
                mcbDoctor.SelectedID = selectedId;
                //txtDoctorNameAddress.Text = mcbDoctor.SeletedItem.ItemData[3];
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbDoctor_SeletectIndexChanged(object sender, EventArgs e)
        {
            //if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID.ToString() != "")
            //{
            //    txtDoctorNameAddress.Text = mcbDoctor.SeletedItem.ItemData[3];
            //}
        }
        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {
            CalculateAmount(-1);
        }
        private void txtMobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbDoctor.Focus();
            else if (e.KeyCode == Keys.Up)
                txtAddress.Focus();
        }

        private void cbTransactionType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbTransactionType.Text == null || cbTransactionType.Text == string.Empty)
                {
                    cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                }
                txtPatientName.Focus();
            }

        }

        #endregion

        #region Construct columns



        private void ConstructMainColumns(PSProductViewControl activeDataGrid)
        {
            DataGridViewTextBoxColumn column;
            activeDataGrid.ColumnsMain.Clear();

            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 220;
                column.ReadOnly = false;
                activeDataGrid.ColumnsMain.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                activeDataGrid.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 50;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                activeDataGrid.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //8          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 70;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                activeDataGrid.ColumnsMain.Add(column);
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
                activeDataGrid.ColumnsMain.Add(column);
                //13            // temp storage columns 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //14         
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //20
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "IfSaleDisc";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CustID";
                //  column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "LastStockID";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";
                column.DataPropertyName = "ProfitPercentBySaleRate";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";
                column.DataPropertyName = "ProfitPercentByPurchaseRate";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRupees";
                column.DataPropertyName = "ProfitInRupees";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MySpecialDiscountAmount";
                column.DataPropertyName = "MySpecialDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }

        private void ConstructProductSelectionListGridColumns(PSProductViewControl activeDataGrid)
        {
            DataGridViewTextBoxColumn column;
            activeDataGrid.ColumnsProductList.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 290;
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsProductList.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 80;
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Com";
                column.Width = 50;
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);

                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorSaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.HeaderText = "Com";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.Visible = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Cl.Stk";
                column.Width = 60;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStockPack";
                column.DataPropertyName = "ProdClosingStockPack";
                column.HeaderText = "Cl.Stk";
                column.Width = 60;
                column.ReadOnly = true;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsProductList.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "Disc";
                column.Width = 40;
                column.Visible = false;
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.HeaderText = "IfH1";
                column.Width = 40;
                column.Visible = false;
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "ProdLastPurchaseRate";
                column.HeaderText = "PurRate";
                column.Width = 30;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfShortListed";
                column.DataPropertyName = "ProdIfShortListed";
                column.HeaderText = "Short";
                column.Width = 30;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MaxLevel";
                column.DataPropertyName = "ProdMaxLevel";
                column.HeaderText = "MaxLevel";
                column.Width = 30;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "ProdLastSaleStockID";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "ProdLastSaleStockID";
                column.HeaderText = "laststockid";
                column.Width = 30;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GenericCategoryName";
                column.DataPropertyName = "GenericCategoryName";
                column.HeaderText = "GenericCategoryName";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void ConstructBatchGridColumns(PSProductViewControl activeDataGrid)
        {
            DataGridViewTextBoxColumn column;
            activeDataGrid.ColumnsBatchList.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batchno";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batchno";
                column.Width = 130;
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 70;
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPercent";
                column.DataPropertyName = "ProductVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsBatchList.Add(column);

                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                activeDataGrid.ColumnsBatchList.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistSaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 100;
                column.Visible = false;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "PurRate";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                activeDataGrid.ColumnsBatchList.Add(column);

                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Cl.STK";
                column.Width = 65;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStockpack";
                column.DataPropertyName = "ClosingStockPack";
                column.HeaderText = "Cl.STK";
                column.Visible = false;
                column.Width = 65;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyShortName";
                column.DataPropertyName = "AccShortName";
                column.HeaderText = "";
                column.Width = 45;
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                activeDataGrid.ColumnsBatchList.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                activeDataGrid.ColumnsBatchList.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Visible = false;
                activeDataGrid.ColumnsBatchList.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        // Construct TempGrid to check stock availability. please do not erase.
        private void ConstructTempGridColumns()
        {
            DataGridViewTextBoxColumn column;
            dgtemp.Columns.Clear();

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
            column.HeaderText = "Batch No.";
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
            column.Name = "Temp_VATPer";
            column.DataPropertyName = "VATPer";
            column.HeaderText = "VAT%";
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
            column.Name = "Temp_Quantity";
            column.DataPropertyName = "Quantity";
            column.HeaderText = "Qty";
            column.Width = 50;
            column.ReadOnly = true;
            dgtemp.Columns.Add(column);



            column = new DataGridViewTextBoxColumn();
            column.Name = "Temp_Amount";
            column.DataPropertyName = "Amount";
            column.HeaderText = "Amount";
            column.Width = 95;
            column.ReadOnly = true;
            dgtemp.Columns.Add(column);

            // temp storage columns 
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

        private void ConstructPrintGridColumns()
        {
            DataGridViewTextBoxColumn column;
            PrintGrid.ColumnsMain.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 220;
                column.ReadOnly = false;
                PrintGrid.ColumnsMain.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                PrintGrid.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 50;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                PrintGrid.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //8          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 70;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                PrintGrid.ColumnsMain.Add(column);
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
                PrintGrid.ColumnsMain.Add(column);
                //13            // temp storage columns 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //14         
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //20
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "IfSaleDisc";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CustID";
                //  column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "LastStockID";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";
                column.DataPropertyName = "ProfitPercentBySaleRate";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";
                column.DataPropertyName = "ProfitPercentByPurchaseRate";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRupees";
                column.DataPropertyName = "ProfitInRupees";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }

        #endregion

        private void txtAddress_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtPatientName.Focus();
        }

        private void mpPVC_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            Exit();
        }

        private void mcbDoctor_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtMobileNumber.Focus();
        }

        private void txtPatientNameAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                txtMobileNumber.Focus();
            else if (e.KeyCode == Keys.Down)
                mcbDoctor.Focus();
            else if (e.KeyCode == Keys.Enter)
                mcbDoctor.Focus();
        }

        private void datePickerBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    ActiveDataGrid.SetFocus(1);
        }

        private void btnClearDoctor_Click(object sender, EventArgs e)
        {
            mcbDoctor.SelectedID = "";
            mcbDoctor.Text = "";
            txtDoctorAddress.Text = "";
            mcbDoctor.Focus();

        }

        private void btnClearPatient_Click(object sender, EventArgs e)
        {
            txtPatientName.Text = "";
            txtPatientName.SelectedID = "";
            txtAddress.Text = "";
            txtMobileNumber.Text = "";
            txtPatientName.Enabled = true;
            this.txtPatientName.Focus();
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                txtPatientName.Focus();
        }

        private void mcbDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                txtMobileNumber.Focus();
        }

        private void datePickerBillDate_Validating(object sender, CancelEventArgs e)
        {
            bool retValue = false;
            string _MDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
            string _CDate = DateTime.Now.Date.ToString("yyyyMMdd");
            if (_MDate == _CDate)
            {
                retValue = General.CheckDates(_MDate, _MDate);
            }
            if (retValue)
            {
                ActiveDataGrid.SetFocus(1);
                lblFooterMessage.Text = "";
            }
            else
            {
                datePickerBillDate.Focus();
                lblFooterMessage.Text = "Please Check Date...";
            }
        }

        private void txtDoctorAddress_UpArrowKeyPressed(object sender, EventArgs e)
        {
            mcbDoctor.Focus();
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDiscPercent.Focus();
            else if (e.KeyCode == Keys.Enter)
                txtDoctorAddress.Focus();
        }

    }
}