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
using PaperlessPharmaRetail.Common.Classes;
//using System.Threading;
namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclCounterSale : BaseControl
    {
        #region Declaration

        private SSSale _SSSale;
        private TempStock _TempStock;
        string _lastCustIdSelected = "1";
        List<DataGridViewRow> rowCollection;
        // string _preID = "";
        public PSProductViewControl ActiveDataGrid;
        public DataGridView psGrid;
        public string _IfNewDoctor = "N";
        public int _PreCurrentQuantity = 0;
        DataTable dtTempCounterSale;
        bool IsSetSaveInvoice = false;
        Timer timer;
        private DataTable _BindingSource;

        #endregion

        #region Constructor
        public UclCounterSale()
        {
            InitializeComponent();
            _SSSale = new SSSale();
            _TempStock = new TempStock();
            SearchControl = new UclCounterSaleSearch();
            CreateCounterSaleDt();
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
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
        { // sheela 14/11/2016 start

            foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
            {
                if (dr.Cells["Col_ProductID"].Value == null || dr.Cells["Col_ProductID"].Value.ToString() == string.Empty)
                {
                    ActiveDataGrid.Focus();
                    ActiveDataGrid.SetFocus(dr.Index, 1);
                    break;
                }
            }
            //  ActiveGrid.SetFocus()
            // ActiveGrid.SetFocus(1);
        }

        public override bool ClearData()
        {
            EnableAllGrids();
            lblTransactionType.BackColor = Color.White;
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
            DataTable dtable = new DataTable();
            Product prod = new Product();
            ContructGrids();
            FormatGrids();
            mpPVC1.Visible = false;
            mpPVC2.Visible = false;
            mpPVC3.Visible = false;
            mpPVC4.Visible = false;
            mpPVC5.Visible = false;
            mpPVC6.Visible = false;
            ActiveGrid.DataSourceProductList = prod.GetOverviewData();

            ActiveGrid.Bind();
            datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
            _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
            headerLabel1.Text = "COUNTER SALE -> NEW";
            pnlCustomerNumber.Enabled = true;
            pnlPatientDrDetails.Enabled = false;
            pnlBillAmount.Enabled = false;
            pnlFinal.Enabled = true;
            txtsavecustno.Enabled = true;
            btnPrint.Visible = false;
            lblFooterMessage.Text = "";
            lblRightSideFooterMsg.Text = "";
            SetGridRowColour();
            txtsavecustno.Text = "1";
            ActiveDataGrid.ProductListGridWidth = 695; // Math.Max(ActiveDataGrid.Width - 350,690);
            ActiveDataGrid.BatchListGridWidth = 695; // Math.Max(ActiveDataGrid.Width - 350, 690);
            NoofRows();
            ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn1.BackColor;
            ActiveDataGrid.SetFocus(1);
            txtsavecustno.Enabled = true;
            txtsavecustno.Text = "1";
            txtsavecustno.Enabled = false;
            _SSSale.CustNumber = 0;
            _lastCustIdSelected = "1";
            ChangeBackColour(ActiveDataGrid);
            if (General.CurrentSetting.MsetSaleSelectVATPercent == "Y")
            {
                lblVATPercentSelected.Visible = true;
                cbVATSelected.Visible = true;
                txtVATPercentSelected.Visible = true;
            }
            else
            {
                lblVATPercentSelected.Visible = false;
                cbVATSelected.Visible = false;
                txtVATPercentSelected.Visible = false;
            }
        }

        private void SetGridRowColour()
        {
            try
            {
                if (ActiveDataGrid != null && ActiveDataGrid.Rows.Count > 0 && ActiveDataGrid.MainDataGridCurrentRow.Cells[0].Value == null || ActiveDataGrid.MainDataGridCurrentRow.Cells[0].Value.ToString() == string.Empty)
                {
                    if (txtsavecustno.Text.ToString().Trim() == "1")
                        ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn1.BackColor;

                    if (txtsavecustno.Text.ToString().Trim() == "2")
                        ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn2.BackColor;

                    if (txtsavecustno.Text.ToString().Trim() == "3")
                        ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn3.BackColor;

                    if (txtsavecustno.Text.ToString().Trim() == "4")
                        ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn4.BackColor;

                    if (txtsavecustno.Text.ToString().Trim() == "5")
                        ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn5.BackColor;

                    if (txtsavecustno.Text.ToString().Trim() == "6")
                        ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn6.BackColor;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ResetGridColour()
        {
            if (ActiveDataGrid.MainDataGridCurrentRow.Cells[0].Value != null)
            {
                if (txtsavecustno.Text.ToString().Trim() == "1")
                    ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn1.BackColor;

                if (txtsavecustno.Text.ToString().Trim() == "2")
                    ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn2.BackColor;

                if (txtsavecustno.Text.ToString().Trim() == "3")
                    ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn3.BackColor;

                if (txtsavecustno.Text.ToString().Trim() == "4")
                    ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn4.BackColor;

                if (txtsavecustno.Text.ToString().Trim() == "5")
                    ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn5.BackColor;

                if (txtsavecustno.Text.ToString().Trim() == "6")
                    ActiveGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn6.BackColor;
            }
        }
        public override string GetShortcutKeys()
        {
            string keyCollection = "";
            keyCollection = "ALT+1 = 1 , ALT+2 = 2 ..     TAB = Enter Patient  ESC = To Go Back To Product     Ctrl+C = Close    Ctrl+P = Save&Print    Ctrl+S = Save   Ctrl+T = ShortCutPanel  ";
            return keyCollection;
        }
        public override string[] FillAllShortcutkeys()
        {
            string[] ShortKeys = new string[12];
            ShortKeys[0] = "Counter 1 ------->  Alt + 1";
            ShortKeys[1] = "Counter 2 ------->  Alt + 2";
            ShortKeys[2] = "Counter 3 ------->  Alt + 3";
            ShortKeys[3] = "Counter 4 ------->  Alt + 4";
            ShortKeys[4] = "Counter 5 ------->  Alt + 5";
            ShortKeys[5] = "Counter 6 ------->  Alt + 6";
            ShortKeys[6] = "Clear Doctor --->  Alt + D";
            ShortKeys[7] = "Edit Rate -------->  Alt + E";
            ShortKeys[8] = "Clear Patient --->  Alt + R";
            ShortKeys[9] = "Similar Product -> Alt + M";
            ShortKeys[10] = "NextVisit -------->  Alt + V";
            ShortKeys[11] = "";
            return ShortKeys;
        }
        public override bool Add()
        {

            bool retValue = true;
            ClearControls();
            //  GoToFirstGrid();
            pnlFinal.Visible = false;
            lblProfit.Visible = txtProfit.Visible = false;
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
                FillBankAccountCombo();
                FillTransactionType();
                mcbDoctor.SelectedID = "";
                FillTxtPatientName();
                FillTxtAddress();
                FillTxtDoctorAddress();
                pnlFinal.Enabled = false;
                //  ContructGrids();
                // ActiveDataGrid = new PSProductViewControl();
                ActiveDataGrid = ActiveGrid;
                ActiveDataGrid.OperationMode = OperationMode.Add;
                //  SetGridRowColour();
                if (ActiveDataGrid != null)
                {
                    //ActiveDataGrid.BatchListGridWidth = 690;
                    //ActiveDataGrid.BringToFront();
                    //ChangeBackColour(ActiveDataGrid);
                    //NoofRows();
                    //ActiveDataGrid.SetFocus(1);
                    //txtsavecustno.Enabled = true;
                    //txtsavecustno.Text = "1";
                    //txtsavecustno.Enabled = false;
                    //_SSSale.CustNumber = 0;
                    //_lastCustIdSelected = "1";
                }
                PanelRs.BackColor = btn1.BackColor;
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            tsBtnCancel.Visible = false;
            tsBtnExit.Visible = true;
            ActiveDataGrid.SetFocus(1);
            lblFooterMessage.Text = "";
            lblRightSideFooterMsg.Text = "";
            txtMinlevel.Text = "";
            txtMaxlevel.Text = "";
            return retValue;
        }

        private void FillTransactionType()
        {
            cbTransactionType.Items.Clear();
            if (txtPatientName.SelectedID == null || txtPatientName.SelectedID.ToString() == string.Empty)
            {
                if (string.IsNullOrEmpty(txtPatientName.Text) == false)
                    cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForVoucher);

            }
            if (string.IsNullOrEmpty(Convert.ToString(txtPatientName.SelectedID)) == false)
            {
                DataTable dt1 = new DataTable();
                Patient _Party = new Patient();
                dt1 = _Party.GetOverviewDataForCounterSaleForOnlyCashSaleCheck(txtPatientName.SelectedID);
                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Rows[0]["PatientID"].ToString() == txtPatientName.SelectedID)
                    {
                        cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
                    }

                }

            }

            if (General.CurrentSetting.MsetAllowPendingCashMemo != "N")
            {
                if (General.CurrentSetting.MsetSaleOnlyCashSaleInCounterSale != "Y")
                {
                    cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
                    cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditCard);
                }

                if (General.CurrentSetting.MsetSaleCreditSale == "Y")
                    cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);

            }
            if (string.IsNullOrEmpty(txtPatientName.Text) == false) //Amar
            {
                cbTransactionType.SelectedIndex = 0;//cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
            }
            else
            {
                cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForVoucher);
            }

        }

        private void FillTransactionTypeForTab()
        {
            cbTransactionType.Items.Clear();
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
            if (txtPatientName.SelectedID == null || txtPatientName.SelectedID.ToString() == string.Empty)
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForVoucher);
            if (General.CurrentSetting.MsetSaleOnlyCashSaleInCounterSale != "Y")
            {
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditCard);
            }
            if (General.CurrentSetting.MsetSaleCreditSale == "Y" && _SSSale.SaleSubType == FixAccounts.SubTypeForDebtorSale)
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
            cbTransactionType.Refresh();
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
            txtPatientName.ReadOnly = false;
            txtPatientName.SelectedID = "";
            txtPatientName.Text = "";
            mcbDoctor.ReadOnly = false;
            mcbDoctor.SelectedID = "";
            txtAddress.ReadOnly = false;
            txtAddress.Text = "";
            txtDoctorAddress.ReadOnly = false;
            txtDoctorAddress.Text = "";
            txtBillAmount.Text = "0.00";
            txtNetAmount.Text = "0.00";
            txtBillAmount2.Text = "0.00";
            txtDiscAmount.Text = "0.00";
            txtDiscPercent.Text = "0.00";
            pnlFinal.Enabled = false;
            ActiveDataGrid.SetFocus(1);
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
                        tsBtnCancel.Visible = true;
                        // UpdateClosingStockinCache();
                        mpPVC1.Rows.Clear();
                        mpPVC2.Rows.Clear();
                        mpPVC3.Rows.Clear();
                        mpPVC4.Rows.Clear();
                        mpPVC5.Rows.Clear();
                        mpPVC6.Rows.Clear();

                        ClearAllTotals();

                        //mpPVC1.Rows.Add();
                        //mpPVC2.Rows.Add();
                        //mpPVC3.Rows.Add();
                        //mpPVC4.Rows.Add();
                        //mpPVC5.Rows.Add();
                        //mpPVC6.Rows.Add();
                        //SetGridRowColour();

                        EnableAllGrids();



                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
            else
            {
                PSDialogResult result;
                result = PSMessageBox.Show("Save Or Remove All Invoices..", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
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
                if (General.PharmaSYSDistributorPlusLicense.LicenseType == LicenseLib.LicenseTypes.Full)
                {

                    ConstructPrintGridColumns();
                    PrintGrid.Rows.Clear();
                    FillPrintGrid();
                    if (General.CurrentSetting.MsetPrintSaleBill == "Y")
                    {
                        PrintSaleBillPrePrintedPaper();
                    }
                    else
                    {
                        PrintSaleBillPlainPaper();
                    }
                }
                else
                {
                    PSDialogResult result;
                    result = PSMessageBox.Show("Trial License", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                }

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
            PharmaSYSDistributorPlus.Printing.PlainPaperPrinter printer = new PharmaSYSDistributorPlus.Printing.PlainPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.CrdbName, _SSSale.PatientAddress1, string.Concat(_SSSale.Telephone.ToString(), _SSSale.MobileNumberForSMS), _SSSale.DoctorName, _SSSale.DoctorAddress, PrintGrid.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount, "");

        }

        private void PrintSaleBillPrePrintedPaper()
        {

            PharmaSYSDistributorPlus.Printing.PrePrintedPaperPrinter printer = new PharmaSYSDistributorPlus.Printing.PrePrintedPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.CrdbName, _SSSale.PatientAddress1, _SSSale.MobileNumberForSMS, _SSSale.DoctorName, _SSSale.DoctorAddress, PrintGrid.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount, "");

        }

        private void FillPrintGrid()
        {
            int colcount = ActiveDataGrid.ColumnsMain.Count;
            double srate = 0;
            double uom = 0;
            double rateperunit = 0;
            try
            {
                foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                {
                    if (dr.Cells[0].Value != null && dr.Cells["Col_Quantity"].Value != null)
                    {
                        int printgridindex = PrintGrid.Rows.Add();
                        for (int i = 0; i < colcount; i++)
                        {
                            if (dr.Cells[i].Value != null)
                                PrintGrid.Rows[printgridindex].Cells[i].Value = dr.Cells[i].Value;
                        }
                        srate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                        uom = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());
                        if (uom == 0)
                            uom = 1;
                        rateperunit = Math.Round(srate / uom, 2);
                        PrintGrid.Rows[printgridindex].Cells["Col_RatePerUnit"].Value = rateperunit.ToString("#0.00");
                    }
                }

                if (General.CurrentSetting.MsetSortOrder == FixAccounts.SortByShelf)
                    PrintGrid.Sort(PrintGrid.Columns["Col_Shelf"], ListSortDirection.Ascending);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool Save()
        {
            //pnlTransactCbBorder.BackColor = Color.White; // [ansuman] 11..11.2016]
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
            _SSSale.DbNoteAmount = 0;
            _SSSale.CrNoteAmount = 0;
            lblFooterMessage.Text = "";
            lblRightSideFooterMsg.Text = "";
            txtMinlevel.Text = "";
            txtMaxlevel.Text = "";
            _SSSale.IfNewPatient = "N";
            _SSSale.IfFullPayment = "Y";

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
            if (txtCreditNote.Text != null && txtCreditNote.Text.ToString() != string.Empty)
                _SSSale.CrNoteAmount = Convert.ToDouble(txtCreditNote.Text.ToString());
            if (txtDebitNote.Text != null && txtDebitNote.Text.ToString() != string.Empty)
                _SSSale.DbNoteAmount = Convert.ToDouble(txtDebitNote.Text.ToString());
            if (mcbBankAccount.SelectedID != null)
                _SSSale.CreditCardBankID = mcbBankAccount.SelectedID;
            _SSSale.DocID = string.Empty;
            if (txtNarration.Text != null)
                _SSSale.CrdbNarration = txtNarration.Text.ToString();
            if (txtAddress.Text != null && txtAddress.Text != "")
            {
                _SSSale.PatientAddress1 = txtAddress.Text.ToString();
                _SSSale.PatientShortAddress = txtAddress.Text.ToString();
            }
            if (_SSSale.PatientAddress1.Length > 50)
                _SSSale.PatientAddress1 = _SSSale.PatientAddress1.Substring(0, 49);
            if (txtPatientName.Text != null && txtPatientName.Text.ToString() != "")
            {
                _SSSale.CrdbName = txtPatientName.Text.ToString();
                _SSSale.ShortName = txtPatientName.Text.ToString();
            }
            if (txtMobileNumber.Text != null)
            {
                _SSSale.MobileNumberForSMS = txtMobileNumber.Text.ToString();
            }
            if (txtTelephoneNumber.Text != null)
            {
                _SSSale.Telephone = txtTelephoneNumber.Text.ToString();
            }
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
            if (_SSSale.DoctorAddress == string.Empty)
                _SSSale.DoctorAddress = txtDoctorAddress.Text.ToString();

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
                            _SSSale.NewPatientIDInDebtorSale = _SSSale.PatientID;
                            _IfNewDoctor = "N";
                            if (_SSSale.DocID == string.Empty)
                            {
                                _IfNewDoctor = "Y";
                                _SSSale.DocID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            }
                            if (cbTransactionType.SelectedItem == null)
                            {
                                cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                                cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                            }
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
                        //if (_SSSale.AccountID == null || _SSSale.AccountID == "")
                        //    _SSSale.AccountID = FixAccounts.AccountCashCreditSale;
                    }
                    if (txtVouType.Text == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        if (_SSSale.SaleSubType != FixAccounts.SubTypeForVoucherSale)
                            txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
                        else
                        {
                            txtVouType.Text = FixAccounts.VoucherTypeForVoucherSale;
                            _SSSale.SaleSubType = FixAccounts.SubTypeForVoucherSale;
                            _SSSale.CrdbAmountNet = Convert.ToDouble(txtNetAmount.Text.ToString()); // SavingCustomersTotalSale();
                                                                                                    //  txtBillAmount.Text = _SSSale.CrdbAmountNet.ToString("#0.00");
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

                    double.TryParse(txtNetAmount.Text, out mbillamount);
                    _SSSale.CrdbAmountNet = mbillamount - _SSSale.MySpecialDiscountAmount;
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
                    double.TryParse(txtBillAmount.Text, out mamount);
                    //  _SSSale.CrdbAmount = mamount - _SSSale.MySpecialDiscountAmount;
                    _SSSale.CrdbAmount = mamount;
                    _SSSale.OperatorID = "";
                    _SSSale.OperatorPassword = txtOperator.Text.ToString();
                    //if (txtMobileNumber.Text != null && txtMobileNumber.Text != string.Empty)
                    //    _SSSale.Telephone = txtMobileNumber.Text.ToString();
                    if (txtRoundAmount.Text != null)
                        mround = Convert.ToDouble(txtRoundAmount.Text.ToString());
                    _SSSale.CrdbRoundAmount = mround;
                    _SSSale.CrdbAddOn = maddon;
                    //CalculateProfitPercent();
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
                        {
                            LockTable.LocktblVoucherNo();
                            _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                        }
                        else if (_SSSale.CrdbVouType != FixAccounts.VoucherTypeForVoucherSale)
                        {
                            LockTable.LocktblVoucherNo();
                            _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                        }
                    }
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
                            if (_SSSale.CrdbVouType != FixAccounts.VoucherTypeForVoucherSale || (General.ShopDetail.ShopChangeCounterSaleType != "Y" && _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale))
                            {
                                _SSSale.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                // _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                            }
                            else
                                _SSSale.Id = "-";

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
                            else if (_SSSale.PatientID != null && _SSSale.PatientID != string.Empty)
                            {
                                _SSSale.UpdatePatient(_SSSale.MobileNumberForSMS, _SSSale.Telephone, _SSSale.PatientID, _SSSale.DocID);
                                if (_SSSale.DocID != null && _SSSale.DocID != string.Empty)
                                {
                                    _SSSale.UpdateDoctor(_SSSale.DoctorAddress, _SSSale.DocID);
                                }
                            }
                            if (_SSSale.CrdbVouType != FixAccounts.VoucherTypeForVoucherSale || (General.ShopDetail.ShopChangeCounterSaleType != "Y" && _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale))
                            {

                                if (_SSSale.SaleSubType != FixAccounts.SubTypeForCreditCardSale && _SSSale.SaleSubType != FixAccounts.SubTypeForDebtorSale && _SSSale.SaleSubType != FixAccounts.SubTypeForVoucherSale)
                                    _SSSale.SaleSubType = FixAccounts.SubTypeForPatientSale;
                                retValue = _SSSale.AddDetails();
                            }
                            else
                                retValue = true;

                            _SavedID = _SSSale.Id;
                            if (retValue)
                                retValue = SaveParticularsProductwise();
                            if (retValue)
                                retValue = ReduceStockIntblStock();

                            if (retValue)
                            {
                                if (_SSSale.CrdbVouType != FixAccounts.VoucherTypeForVoucherSale || (General.ShopDetail.ShopChangeCounterSaleType != "Y" && _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale))
                                    retValue = SaveIntblTrnac();
                            }
                            if (_SSSale.CrNoteAmount > 0 || _SSSale.DbNoteAmount > 0)
                                SaveAndUpdateDebitCreditNote();
                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            string msgLine2 = "";
                            PSDialogResult result;
                            LockTable.UnLockTables();
                            msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
                            if (retValue && General.ShopDetail.ShopChangeCounterSaleType != "Y" || (General.ShopDetail.ShopChangeCounterSaleType == "Y" && _SSSale.CrdbVouType != FixAccounts.VoucherTypeForVoucherSale))
                            {

                                if (_SSSale.CrdbVouType != FixAccounts.VoucherTypeForVoucherSale)
                                {
                                    // UpdateClosingStockinCache();
                                    if (printData)
                                    {
                                        result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK,  Convert.ToDouble(txtBillAmount2.Text.ToString()));
                                        Print();
                                    }
                                    else
                                    {
                                        if (_SSSale.CrdbVouType != FixAccounts.VoucherTypeForVoucherSale || (General.ShopDetail.ShopChangeCounterSaleType != "Y" && _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale))
                                        {
                                            HasShowSavedMessage(msgLine2);
                                        }
                                    }
                                }
                                else
                                    HasShowSavedMessage(msgLine2);

                                retValue = true;
                            }
                            else
                            {
                                if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                                {
                                    if (retValue && _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                                    {
                                        HasShowSavedMessage(msgLine2);
                                    }
                                    else
                                    {
                                        result = PSMessageBox.Show("Could not Save Voucher...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                        retValue = false;
                                    }
                                }
                            }

                        }
                        else
                        {
                            LockTable.UnLockTables();
                        }


                        btnRefreshClick();
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
            // CacheObject.Clear("cacheCounterSale");
            CacheObject.Clear("TempCounterSale");
            return retValue;
        }
        private void HasShowSavedMessage(string msgLine2)
        {
            int sendsms = 0;
            PSDialogResult result;
           if (General.CurrentSetting.MsetSaleTenderAmount == "Y")
            {
                if (General.CurrentSetting.MsetAllowPrintMessage =="Y")  //Amar
                {
                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print, Convert.ToDouble(txtBillAmount2.Text.ToString()));
                    if (result == PSDialogResult.Print)
                        Print();
                }
               
                //Amar For Sms
                if (General.CurrentSetting.SmsSetPatientSale == "Y" || General.CurrentSetting.SmsSetDebtorSale == "Y" || General.CurrentSetting.SmsSetCreditCardSale == "Y")
                {
                    if (General.CurrentSetting.SmsSetDebtorSale == "Y" && cbTransactionType.Text.ToString() == FixAccounts.TransactionTypeForCredit)
                    {
                        sendsms = 1;
                    }
                    else if (General.CurrentSetting.SmsSetPatientSale == "Y")
                    {
                        sendsms = 1;
                    }
                    else if (General.CurrentSetting.SmsSetCreditCardSale == "Y")
                    {
                        sendsms = 1;
                    }
                }
            }
            else
            {
                if (General.CurrentSetting.MsetAllowPrintMessage == "Y")  //Amar
                {
                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                    if (result == PSDialogResult.Print)
                        Print();
                }
                   
                //Amar For Sms
                if (General.CurrentSetting.SmsSetPatientSale == "Y" || General.CurrentSetting.SmsSetDebtorSale == "Y" || General.CurrentSetting.SmsSetCreditCardSale == "Y")
                {
                    if (General.CurrentSetting.SmsSetDebtorSale == "Y" && cbTransactionType.Text.ToString() == FixAccounts.TransactionTypeForCredit)
                    {
                        sendsms = 1;
                    }
                    else if (General.CurrentSetting.SmsSetPatientSale == "Y" && cbTransactionType.Text.ToString() == FixAccounts.TransactionTypeForCash)
                    {
                        sendsms = 1;
                    }
                    else if (General.CurrentSetting.SmsSetCreditCardSale == "Y" && cbTransactionType.Text.ToString() == FixAccounts.TransactionTypeForCredit)
                    {
                        sendsms = 1;
                    }
                }
            }
            if (sendsms == 1) //amar
            {
                SendSMS mSMS = new SendSMS();
                string Msg = "Dear Customer Your Bill No.:" + _SSSale.CrdbVouNo + " Of Amount :" + _SSSale.CrdbAmountNet + " Thank You For Dealing With Us.";
                Msg += mSMS.GetShopDetailsFromData();
                if (string.IsNullOrEmpty(_SSSale.MobileNumberForSMS) == false)
                {
                    mSMS.SendSMSData(_SSSale.MobileNumberForSMS, Msg);
                }
                else
                {
                    MessageBox.Show("Please Update Mobile Number For Sending SMS", "PharmaSYSDistributorPlus", MessageBoxButtons.OK);
                }
                sendsms = 0;
            }
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
                        if (prodrow.Cells["Col_ProdScheduleDrugCode"].Value != null && prodrow.Cells["Col_ProdScheduleDrugCode"].Value.ToString().Trim() != string.Empty)
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

        public override void ReFillData(Control closedControl)
        {
            try
            {
                if (closedControl is UclArea)
                {
                    FillTxtAddress();
                    FillTxtDoctorAddress();
                }
                else if (closedControl is UclDoctor)
                    FillDoctorCombo();
                else if (closedControl is UclPatient || closedControl is UclAccount)
                    FillTxtPatientName();
                //else if (closedControl is UclPurchase)
                //    BindActiveDataGrid();
                General.CurrentSetting.FillSettings();
                if (General.CurrentSetting.MsetSaleAllowNegativeStock == "Y")
                {
                    ActiveGrid.AllowNewBatch = true;
                }
                else
                {
                    ActiveGrid.AllowNewBatch = false;
                }

                CheckSettings();
                if (pnlPatientDrDetails.Visible == true)
                {
                    if (txtPatientName.Enabled == true)
                        txtPatientName.Focus();
                    else if (txtAddress.Enabled == true)
                        txtAddress.Focus();
                    else if (mcbDoctor.Enabled == true)
                        mcbDoctor.Focus();
                    else if (txtNarration.Enabled == true)
                        txtNarration.Focus();
                }
                else
                    ActiveDataGrid.SetFocus(1);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public void BindActiveDataGrid()
        {

            Product prod = new Product();
            if (ActiveDataGrid == null)
                ActiveDataGrid = ActiveGrid;

            ActiveDataGrid.DataSourceProductList = prod.GetOverviewData();

            ActiveDataGrid.BindGridProductList();

        }

        private void CheckSettings()
        {
            if (General.CurrentSetting.MsetSaleDoNotShowNegetiveStock == "Y")
                btnUpdateNegetiveStock.Visible = true;
            else
                btnUpdateNegetiveStock.Visible = false;
            if (General.CurrentSetting.MsetSaleEditRateInCounterSale == "Y")
                cbEditRate.Visible = true;
            else
                cbEditRate.Visible = false;
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
            if (General.CurrentSetting.MsetSaleSelectVATPercent == "Y")
            {
                lblVATPercentSelected.Visible = true;
                cbVATSelected.Visible = true;
                txtVATPercentSelected.Visible = true;
            }
            else
            {
                lblVATPercentSelected.Visible = false;
                cbVATSelected.Visible = false;
                txtVATPercentSelected.Visible = false;
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
                //if (keyPressed == Keys.N && modifier == Keys.Alt)
                //{
                //    txtPatientName.Focus();
                //    retValue = true;
                //}

                //if (keyPressed == Keys.A && modifier == Keys.Alt)
                //{
                //    txtAddress.Focus();
                //    retValue = true;
                //}
                if (uclSubstituteControl1.Visible)
                {
                    retValue = uclSubstituteControl1.HandleShortcutAction(keyPressed, modifier);
                }
                if (keyPressed == Keys.S && modifier == Keys.Alt)
                {
                    uclSubstituteControl1.Visible = false;
                }

                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    BtnClearDoctorClick();
                    mcbDoctor.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.V && modifier == Keys.Alt)
                {
                    btnNextVisit_Click();
                    retValue = true;
                }
                if (keyPressed == Keys.R && modifier == Keys.Alt)
                {
                    if (pnlPatientDrDetails.Visible == true)
                        BtnClearPatientClick();
                    else
                        ActiveDataGridTabKeyPressed();
                    retValue = true;
                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    BtnIfDebitCreditNoteClick();
                    retValue = true;
                }
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    if (txtOperator.Visible == true)
                        txtOperator.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                    if (pnlPatientDrDetails.Visible == true)
                        BtnClearPatientClick();
                    else
                        ActiveDataGridTabKeyPressed();
                    retValue = true;
                }
                if (keyPressed == Keys.Right || keyPressed == Keys.Left)
                {
                    retValue = true;
                }
                if (keyPressed == Keys.Escape)
                {

                    //if (pnlDebitCreditNote.Visible == true)
                    //{
                    //    pnlDebitCreditNote.Visible = false;
                    //    txtDiscPercent.Focus();
                    //    retValue = true;
                    //}else

                    if (uclSubstituteControl1.Visible == true)
                    {
                        uclSubstituteControl1.Visible = false;
                        retValue = true;
                    }
                    else if (pnlVisitDetails.Visible == true)
                    {
                        btnNextVisit_Click();
                        retValue = true;
                    }
                    else if (pnlFinal.Visible == true)
                    {
                        pnlFinal.Visible = false;
                        lblProfit.Visible = txtProfit.Visible = false;
                        EnableAllGrids();

                        cbTransactionType.Text = FixAccounts.TransactionTypeForVoucher;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForVoucher);

                        ActiveDataGrid.SetFocus(1);

                        retValue = true;
                    }
                    else if (ActiveGrid.VisibleProductGrid() == true)
                    {
                        lblFooterMessage.Text = "";
                        lblRightSideFooterMsg.Text = "";
                        txtMinlevel.Text = "";
                        txtMaxlevel.Text = "";
                    }
                    else if (ISSaleSummaryShow == true)
                    {
                        if (base.ctrlUclSaleSummaryControl.Visible)
                        {
                            ctrlUclSaleSummaryControl.SendToBack();
                            ctrlUclSaleSummaryControl.Visible = false;
                        }
                    }
                    else
                        Exit();
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

        private void ContructGrids()
        {
            for (int i = 1; i <= 6; i++)
            {

                if (i == 1)
                    psGrid = mpPVC1;
                else if (i == 2)
                    psGrid = mpPVC2;
                else if (i == 3)
                    psGrid = mpPVC3;
                else if (i == 4)
                    psGrid = mpPVC4;
                else if (i == 5)
                    psGrid = mpPVC5;
                else
                    psGrid = mpPVC6;

                ConstructMainColumnsmpPVC(psGrid);

            }

            ActiveDataGrid = ActiveGrid;
            if (ActiveDataGrid != null)
            {
                ActiveDataGrid.BatchColumnName = "Col_BatchNumber";
                ActiveDataGrid.ColumnsMain.Clear();
            }
            ActiveDataGrid.ColumnsMain.Clear();
            ConstructMainColumns(ActiveDataGrid);
            ActiveDataGrid.BatchColumnName = "Col_BatchNumber";
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
            ConstructTempGridColumns();
            ConstructNextVisitColumns();
        }
        private void FormatGrids()
        {
            try
            {

                ActiveGrid.ProductListGridWidth = 680;
                ActiveGrid.BatchListGridWidth = 690;
                ActiveGrid.BatchGridShowColumnName = "Col_UOM";
                ActiveGrid.NewRowColumnName = "Col_Quantity";
                //  ActiveGrid.DoubleColumnNames.Add("Col_MRP");
                //  ActiveGrid.NumericColumnNames.Add("Col_Quantity");
                //  ActiveGrid.DoubleColumnNames.Add("Col_VATPer");
                //   ActiveGrid.DoubleColumnNames.Add("Col_PurchaseRate");
                //    ActiveGrid.DoubleColumnNames.Add("Col_Amount");
                //    ActiveGrid.DoubleColumnNames.Add("Col_SaleRate");
                ActiveGrid.ProductGridClosingStockColumnName = "Col_ClosingStock";
                ActiveGrid.MainGridSoldQuantityColumnName = "Col_Quantity";
                ActiveGrid.ClearSelection();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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

                        if (stockavailable + tempstock < _SSSale.Quantity && General.CurrentSetting.MsetSaleAllowNegativeStock != "Y")
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
                        double.TryParse(txtMyDiscountPercent.Text, out newmydiscper);
                        double newsalerate = msalerate - Math.Round(((msalerate - Math.Round((msalerate * mvatper / 100), 2)) * (newmdiscper + newmydiscper) / 100), 2);
                        double vatontrrate = Math.Round((mtraderate * mvatper) / 100, 2);

                        double PerPackDiscount = mdiscamt / (mqty / mpakn);
                        totalvat += vatontrrate;
                        totalsale += newsalerate;
                        totalpur += mpurrate;

                        _SSSale.ProfitPercentBySaleRate = Math.Round(((msalerate - PerPackDiscount) - (mpurrate)) / (msalerate - PerPackDiscount), 4);
                        _SSSale.ProfitPercentByPurchaseRate = Math.Round(((msalerate - PerPackDiscount) - (mpurrate)) / (mpurrate), 4);
                        //_SSSale.TotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
                        //_SSSale.TotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
                        _SSSale.ProfitInRupees = Math.Round((((msalerate - PerPackDiscount) - (mpurrate)) / mpakn) * mqty, 2);
                        _SSSale.TotalProfitInRupees += _SSSale.ProfitInRupees;
                        prodrow.Cells["Col_ProfitPercentBySaleRate"].Value = _SSSale.ProfitPercentBySaleRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _SSSale.ProfitPercentByPurchaseRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitInRupees"].Value = _SSSale.ProfitInRupees.ToString("#0.00");
                    }

                }
                _SSSale.TotalProfitPercentBySaleRate = Math.Round(((totalsale) - (totalpur)) / (totalsale), 4);
                _SSSale.TotalProfitPercentByPurchaseRate = Math.Round(((totalsale) - (totalpur)) / (totalpur), 4);
                if (pnlFinal.Visible && General.CurrentSetting.MsetSaleShowProfitInSaleBill == "Y")
                {
                    txtProfit.Text = _SSSale.TotalProfitInRupees.ToString("#0.00");
                }
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
                        if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                        if (prodrow.Cells["Col_MRP"].Value != null)
                            double.TryParse(prodrow.Cells["Col_MRP"].Value.ToString().Trim(), out mmrp);
                        double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                        _SSSale.SaleRate = msalerate;
                        if (mmrp == 0)
                        {
                            mmrp = msalerate;
                            _SSSale.MRP = msalerate;
                        }
                        else
                            _SSSale.MRP = mmrp;
                        if (mpurrate == 0 || mpurrate > msalerate)
                            mpurrate = msalerate - (msalerate * 18 / 100);
                        _SSSale.PurchaseRate = mpurrate;
                        if (mtraterate == 0 || mtraterate > msalerate)
                            mtraterate = mpurrate;
                        _SSSale.TradeRate = mtraterate;
                        double.TryParse(prodrow.Cells["Col_VATPer"].Value.ToString().Trim(), out mvatper);
                        _SSSale.VATPer = mvatper;
                        double.TryParse(prodrow.Cells["Col_VATAmount"].Value.ToString().Trim(), out mvatamt);
                        _SSSale.VATAmount = mvatamt;
                        double.TryParse(prodrow.Cells["Col_Amount"].Value.ToString().Trim(), out mamt);
                        _SSSale.Amount = mamt;
                        if (prodrow.Cells["Col_StockID"].Value != null)
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

                        if (mlastsaleid == null || mlastsaleid == "")
                        {

                            mlastsaleid = _SSSale.CheckForProductBatchMRPInStocktable();
                            _SSSale.TempChallanID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            if (mlastsaleid == "")
                            {
                                mlastsaleid = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            }
                            _SSSale.LastStockID = mlastsaleid;
                            _SSSale.StockID = mlastsaleid;
                            prodrow.Cells["Col_StockID"].Value = _SSSale.StockID.ToString();
                            if (General.CurrentSetting.MsetSaleDoNotShowNegetiveStock != "Y")
                                _SSSale.AddDetailsInTempPurchase();

                        }
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
                if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale || _SSSale.SaleSubType == FixAccounts.SubTypeForCreditCardSale)
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
                double.TryParse(txtNetAmount.Text, out mbillamount);
                _SSSale.CrdbAmountNet = mbillamount;
                mround = _SSSale.CrdbRoundAmount;

                mdebit = Math.Round(mbillamount - Math.Round(mvat5per, 2) - Math.Round(mvat12point5per, 2) + mdiscamount - maddon - mround - mamtforzerovat + mcreditnoteamt - mdebitnoteamt, 2);

                if (mamtforzerovat > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVATZeroSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale || _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else if (_SSSale.SaleSubType == FixAccounts.SubTypeForCreditCardSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCreditCardSale;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mamtforzerovat;
                    retValue = _SSSale.AddVoucherIntblTrnac();

                }

                if (retValue == true && mvat5per > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput6Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        _SSSale.CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCash;
                        else if (_SSSale.SaleSubType == FixAccounts.SubTypeForCreditCardSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCashCreditSale;
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
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput13point5Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        _SSSale.CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCash;
                        else if (_SSSale.SaleSubType == FixAccounts.SubTypeForCreditCardSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCashCreditSale;
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
                        else if (_SSSale.SaleSubType == FixAccounts.SubTypeForCreditCardSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCashCreditSale;
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
                        else if (_SSSale.SaleSubType == FixAccounts.SubTypeForCreditCardSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCashCreditSale;
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
                        else if (_SSSale.SaleSubType == FixAccounts.SubTypeForCreditCardSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCashCreditSale;
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
                        else if (_SSSale.SaleSubType == FixAccounts.SubTypeForCreditCardSale)
                            _SSSale.CreditAccount = FixAccounts.AccountCashCreditSale;
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
                    else if (_SSSale.SaleSubType == FixAccounts.SubTypeForCreditCardSale)
                    {
                        _SSSale.CreditAccount = _SSSale.CreditCardBankID;
                        _SSSale.DebitAccount = FixAccounts.AccountCashCreditSale;
                    }
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mdebit;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (retValue == true && mbillamount > 0)
                {
                    if (_SSSale.SaleSubType == FixAccounts.SubTypeForCreditCardSale)
                    {
                        _SSSale.DebitAccount = _SSSale.CreditCardBankID;
                        _SSSale.CreditAccount = FixAccounts.AccountCashCreditSale;
                    }
                    else
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
                        _SSSale.ProdPakn = Convert.ToInt32(prodrow.Cells["Col_UOM"].Value.ToString());
                        _SSSale.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString());
                        _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        _SSSale.SaleRate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());
                        _SSSale.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                        _SSSale.ExpiryDate = prodrow.Cells["Col_ExpiryDate"].Value.ToString();
                        if (prodrow.Cells["Col_PurchaseRate"].Value != null && prodrow.Cells["Col_PurchaseRate"].Value.ToString() != "")
                            _SSSale.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurchaseRate"].Value.ToString());
                        else
                            _SSSale.PurchaseRate = Math.Round(_SSSale.MRP - (_SSSale.MRP * 20 / 100), 2);
                        if (prodrow.Cells["Col_TradeRate"].Value != null && prodrow.Cells["Col_TradeRate"].Value.ToString() != "")
                            _SSSale.TradeRate = Convert.ToDouble(prodrow.Cells["Col_TradeRate"].Value.ToString());
                        else
                            _SSSale.TradeRate = _SSSale.PurchaseRate;
                        _SSSale.VATPer = Convert.ToDouble(prodrow.Cells["Col_VATPer"].Value.ToString());
                        if (prodrow.Cells["Col_StockID"].Value != null)
                            mlastsaleid = prodrow.Cells["Col_StockID"].Value.ToString();
                        _SSSale.LastStockID = mlastsaleid;
                        _SSSale.StockID = mlastsaleid;
                        if (mlastsaleid != "")
                        {
                            string ifRecordFound = "";
                            ifRecordFound = _SSSale.CheckForBatchMRPInStockTable();
                            if (ifRecordFound == "Y")
                            {
                                returnVal = _SSSale.UpdateIntblStock();
                                if (returnVal)
                                    returnVal = _SSSale.UpdateSaleStockInMasterProduct();
                                if (returnVal)
                                {
                                    if (_SSSale.IfAddToShortList() && prodrow.Cells["Col_IFTempSale"].Value != null && prodrow.Cells["Col_IFTempSale"].Value.ToString() != "Y")
                                        FillDailyShortList();
                                }
                                else
                                {
                                    returnVal = false;
                                    break;
                                }
                            }
                            else
                            {
                                int negativestock = _SSSale.GetStockToCheckNegetive();
                                if (negativestock < 0)
                                {
                                    _SSSale.AddIntblStockForNegativeStock();

                                    returnVal = _SSSale.UpdateSaleStockInMasterProduct();
                                    returnVal = true;
                                    // returnVal = _SSSale.UpdateSaleStockInMasterProduct();
                                }
                            }

                        }
                        else
                            returnVal = true;
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

        //private bool UpdateClosingStockinCache()
        //{
        //    bool returnVal = false;
        //    try
        //    {
        //        General.UpdateProductListCacheTest(ActiveDataGrid.Rows, "Col_ProductID", ActiveDataGrid.Rows, "Col_ProductID");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        returnVal = false;
        //    }
        //    return returnVal;
        //}
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

        private void FillTxtPatientName()
        {
            try
            {
                txtPatientName.SelectedID = null;
                txtPatientName.SourceDataString = new string[11] { "PatientID", "AccCode", "PatientName", "PatientAddress1", "PatientAddress2", "ShortNameAddress", "DoctorID", "AccTransactionType", "DiscountOffered", "MobileNumberForSMS", "TelephoneNumber" };
                txtPatientName.ColumnWidth = new string[11] { "0", "50", "200", "200", "200", "0", "0", "0", "0", "0", "0" };
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
                txtAddress.ColumnWidth = new string[2] { "0", "500" };
                txtAddress.ValueColumnNo = 0;
                txtAddress.UserControlToShow = new UclArea();
                Area _Area = new Area();
                DataTable dtable = _Area.GetOverViewDataForAddress();
                txtAddress.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillTxtDoctorAddress()
        {
            try
            {
                txtDoctorAddress.SelectedID = null;
                txtDoctorAddress.SourceDataString = new string[2] { "AreaID", "AreaName" };
                txtDoctorAddress.ColumnWidth = new string[2] { "0", "500" };
                txtDoctorAddress.ValueColumnNo = 0;
                txtDoctorAddress.UserControlToShow = new UclArea();
                Area _Area = new Area();
                DataTable dtable = _Area.GetOverViewDataForAddress();
                txtDoctorAddress.FillData(dtable);
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
            //  string prodname = "";
            string mexpirydate = "";
            try
            {
                if (colIndex == 1)
                {
                    //_PreCurrentQuantity = 0;
                    //if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    //    _PreCurrentQuantity = Convert.ToInt32(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                    //_preID = "";
                    //if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                    //    _preID = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                    //if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductName"].Value != null)
                    //    prodname = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString();
                    //if (prodname != "" && _preID != "")
                    //{
                    //    prodname = General.GetProductName(_preID);
                    //    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = prodname;
                    //}
                }
                if (colIndex == 11)  // Quantity
                {
                    try
                    {

                        //kiran
                        int i;
                        //bool IsSplitinMultipleBatch = false;
                        if (!int.TryParse(Convert.ToString(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value), out i))
                        {
                            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = null;
                        }

                        SsStock stk = new SsStock();
                        DataTable dtCurrStk = new DataTable();
                        // sheela 30/11
                        if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                            dtCurrStk = stk.GetStockByStockIDForDBCRNote(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString());
                        foreach (DataRow drCurrStk in dtCurrStk.Rows)
                        {
                            if (drCurrStk["ClosingStock"] != null)
                                _SSSale.CurrentBatchStock = Convert.ToInt32(drCurrStk["ClosingStock"]);
                        }
                        // kiran
                        if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value == null || ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString() == string.Empty)
                            ActiveDataGrid.IsAllowNewRow = false;
                        else
                        {
                            if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString() == "0")
                            {
                                int minq = 0;
                                _SSSale.ProdPakn = Convert.ToInt32(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
                                minq = Math.Min(_SSSale.CurrentBatchStock, _SSSale.ProdPakn);
                                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = minq.ToString("#0");
                            }
                            if (Convert.ToInt32(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) <= _SSSale.CurrentBatchStock)
                                ActiveDataGrid.MainDataGridCurrentRow.DefaultCellStyle.ForeColor = Color.Black;

                            if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                                mbtno = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim();
                            if (mbtno != string.Empty)
                            {
                                string mdt = DateTime.Today.Date.ToString("yyyyMMdd");

                                if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value != null)
                                    mexpirydate = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                                if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                                {
                                    lblFooterMessage.Text = "Expired Product";
                                    PSMessageBox.Show("Product Expired", "Invalid expiry date", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = 0;
                                    ActiveDataGrid.IsAllowNewRow = false;
                                    ActiveDataGrid.SetFocus(11);
                                }
                                else
                                {
                                    // here
                                    int activegridbatchstock = 0;
                                    requiredQty = Convert.ToInt32(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                                    if (_PreCurrentQuantity == 0)
                                        _PreCurrentQuantity = requiredQty;
                                    string stkdtstockid = string.Empty;
                                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                                        stkdtstockid = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString();
                                    foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                                    {
                                        if (dr.Cells["Col_StockID"].Value != null)
                                        {
                                            string activegridrowstockid = dr.Cells["Col_StockID"].Value.ToString();
                                            if (stkdtstockid == activegridrowstockid && dr.Index != ActiveDataGrid.MainDataGridCurrentRow.Index)
                                            {
                                                activegridbatchstock += Convert.ToInt32(dr.Cells["Col_Quantity"].Value.ToString());
                                                break;
                                            }
                                        }
                                    }
                                    for (int Counter = 1; Counter <= 6; Counter++)
                                    {
                                        DataGridView grid = (DataGridView)this.Controls.Find(string.Format("mpPVC{0}", Counter), true).FirstOrDefault();
                                        if (string.Equals(txtsavecustno.Text, Counter.ToString()) == false)
                                        {
                                            foreach (DataGridViewRow dr in grid.Rows)
                                            {
                                                if (dr.Cells["Col_StockID"].Value != null)
                                                {
                                                    string activegridrowstockid = dr.Cells["Col_StockID"].Value.ToString();
                                                    if (stkdtstockid == activegridrowstockid)
                                                    {
                                                        _SSSale.CurrentBatchStock -= Convert.ToInt32(dr.Cells["Col_Quantity"].Value.ToString());
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if ((_SSSale.CurrentBatchStock - activegridbatchstock) >= 0 && requiredQty > _SSSale.CurrentProductStock && General.CurrentSetting.MsetSaleAllowNegativeStock == "N" && requiredQty == 0)
                                    {
                                        requiredQty = _SSSale.CurrentBatchStock - activegridbatchstock;
                                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = requiredQty.ToString("#0");
                                    }
                                    if (requiredQty <= 0 || _SSSale.CurrentBatchStock == 0)
                                    {
                                        if (requiredQty <= 0)
                                        {
                                            lblFooterMessage.Text = "Enter Quantity";
                                            ActiveDataGrid.SetFocus(11);
                                            ActiveDataGrid.IsAllowNewRow = false;
                                        }
                                        else if (General.CurrentSetting.MsetSaleAllowNegativeStock == "N")
                                        {
                                            lblFooterMessage.Text = "Batch Stock Zero";
                                            ActiveDataGrid.SetFocus(11);
                                            ActiveDataGrid.IsAllowNewRow = false;
                                        }
                                        else
                                        {
                                            int mbatchstock = 0;
                                            int oldQuantity = 0;
                                            string mstockid = "";
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

                                            lblFooterMessage.Text = "";
                                            lblRightSideFooterMsg.Text = "";
                                            txtMinlevel.Text = "";
                                            txtMaxlevel.Text = "";
                                            FillBatchStock(ref mmrp, ref mrate, ref mpakn, ref mbtno, ref mprodno, ref mcurrentindex, ref oldmqty, ref mqty, ref custno);
                                            ActiveDataGrid.IsAllowNewRow = true;
                                            ActiveDataGrid.ColumnsMain["Col_Quantity"].ReadOnly = true;
                                        }
                                    }
                                    else if (requiredQty > _SSSale.CurrentBatchStock && _Mode == OperationMode.Edit)
                                    {

                                        lblFooterMessage.Text = "Enter Correct Quantity";
                                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = _SSSale.CurrentBatchStock;
                                        ActiveDataGrid.SetFocus(11);
                                        ActiveDataGrid.IsAllowNewRow = false;
                                        CalculateAmount(-1);
                                    }
                                    else
                                    {
                                        int currindex = ActiveDataGrid.MainDataGridCurrentRow.Index;
                                        if (requiredQty <= _SSSale.CurrentBatchStock || (ActiveDataGrid.Rows.Count == currindex + 1) || (ActiveDataGrid.Rows.Count > currindex + 1 && (ActiveDataGrid.Rows[currindex + 1].Cells["Col_ProductID"].Value == null)))
                                        {
                                            _SSSale.CurrentProductStock = 0;
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
                                            if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value != null)
                                                mrate = Convert.ToDouble(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value);

                                            lblFooterMessage.Text = "";
                                            lblRightSideFooterMsg.Text = "";
                                            if ((requiredQty <= _SSSale.CurrentBatchStock) || (requiredQty > _SSSale.CurrentBatchStock && General.CurrentSetting.MsetSaleAllowNegativeStock == "Y"))
                                            {
                                                //FillBatchStockOptionalMultipleBatch(ref mmrp, ref mrate, ref mpakn, ref mbtno, ref mprodno, ref mcurrentindex, ref oldmqty, ref mqty, ref custno,);
                                                FillBatchStock(ref mmrp, ref mrate, ref mpakn, ref mbtno, ref mprodno, ref mcurrentindex, ref oldmqty, ref mqty, ref custno);
                                                int numberofLines = General.GetNumberofLinesInGrid(ActiveDataGrid);
                                                if (General.CurrentSetting.MsetPrintFixNumberOfLines == "Y")
                                                {
                                                    if (numberofLines < General.CurrentSetting.MsetNumberOfLinesSaleBill)
                                                    {
                                                        ActiveDataGrid.IsAllowNewRow = true;
                                                    }
                                                    else
                                                    {
                                                        ActiveDataGrid.IsAllowNewRow = false;
                                                    }
                                                }
                                                else
                                                {
                                                    ActiveDataGrid.IsAllowNewRow = true;
                                                }


                                                ActiveDataGrid.ColumnsMain["Col_Quantity"].ReadOnly = false;
                                            }
                                            else
                                            {
                                                if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                                                    mprodno = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();


                                                //FillMainGridwithMultipleBatch(requiredQty, mprodno);
                                                bool checkCounterDt = false;
                                                checkCounterDt = RemovePreviousProductInMainGrid(ref requiredQty, mprodno, mbtno, Convert.ToString(mrate), mprodno);


                                                //FillBatchStockOptionalMultipleBatch(requiredQty, _SSSale.CurrentBatchStock, mprodno, ref mmrp, ref mrate, ref mpakn, ref mbtno, ref mprodno, ref mcurrentindex, ref oldmqty, ref mqty, ref custno);
                                                FillMainGridwithMultipleBatch(requiredQty, mprodno, checkCounterDt, mbtno);
                                                CalculateAmount(-1);
                                                //ActiveDataGrid.ColumnsMain["Col_Quantity"].ReadOnly = true;

                                                //}
                                                //if (_Mode == OperationMode.Add)
                                                //{
                                                //    WriteToXML();
                                                //}

                                            }
                                        }
                                        else
                                        {
                                            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = _SSSale.CurrentBatchStock;// _PreCurrentQuantity.ToString("#0");
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
                        _PreCurrentQuantity = 0;


                        UpdateTempCounterSaleDt();
                        if (_Mode == OperationMode.Add)
                        {
                            WriteToXML();
                        }
                        if (ActiveDataGrid.Rows.Count > 0)
                        {
                            if (ActiveDataGrid.Rows[0].Cells["Col_ProductID"].Value == null)
                            {
                                ActiveDataGrid.Rows.RemoveAt(0);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex.ToString());
                    }
                }

                if (colIndex == 10)  // sale rate
                {
                    //kiran if else condition added
                    decimal i;
                    if (!Decimal.TryParse(Convert.ToString(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value), out i))
                    {
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = null;
                        ActiveDataGrid.IsFocusSameCell = true;
                    }
                    else
                    {
                        ActiveDataGrid.IsFocusSameCell = false;
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = String.Format("{0:0.00}", Convert.ToDouble(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value)).ToString();
                        string newexp = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString();
                        newexp = General.GetValidExpiry(newexp);
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexp.ToString();
                        string edate = General.GetValidExpiryDate(newexp);
                        edate = General.GetExpiryInyyyymmddForm(edate);
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = edate.ToString();
                        if (Convert.ToDouble(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value) <= 0)
                        {
                            lblFooterMessage.Text = "Enter SaleRate";
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
                }

                if (colIndex == 8)  // Expiry
                {
                    string newexpiry = "";
                    string newexpirydate = "";
                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null)
                    {
                        int explength = ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim().Length;
                        if (ActiveGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null && ActiveGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim() != "" && explength > 0)
                        {
                            newexpiry = General.GetValidExpiry(ActiveGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString());
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
                                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = true;
                                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = true;
                                }
                                else
                                {
                                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = false;
                                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                                    if (ifexp)
                                    {
                                        ActiveGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry.ToString();
                                        newexpirydate = General.GetValidExpiryDate(newexpiry);
                                        newexpirydate = General.GetExpiryInyyyymmddForm(newexpirydate);
                                        ActiveGrid.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                                        lblFooterMessage.Text = "";
                                        lblRightSideFooterMsg.Text = "";
                                        txtMinlevel.Text = "";
                                        txtMaxlevel.Text = "";
                                    }
                                    else
                                    {
                                        lblFooterMessage.Text = "Check Expiry ";
                                        ActiveGrid.SetFocus(5);
                                    }
                                }
                            }
                            else
                            {
                                ActiveGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                                lblFooterMessage.Text = " No Expiry ";
                                ActiveGrid.SetFocus(5);
                            }

                        }
                        else
                        {
                            ActiveGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                            lblFooterMessage.Text = " No Expiry ";
                            ActiveGrid.SetFocus(5);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private DataTable SortByBatch(DataTable dtStk, string batchId)
        {

            List<DataRow> stkLists = dtStk.AsEnumerable().ToList();
            var stkList = stkLists;
            var matchList = stkList.Where(m => m["BatchNumber"].ToString().StartsWith(batchId)).ToList();
            var FinalList = matchList.Concat(stkList.Except(matchList).ToList());
            dtStk = FinalList.CopyToDataTable();
            return dtStk;
        }

        private bool RemovePreviousProductInMainGrid(ref int reqQty, string productId, string batchId, string saleRate, string prodId)
        {
            bool checkCounterDt = false;
            //Check productStocck
            Stock prodstk = new Stock();
            DataTable stkdt = new DataTable();
            stkdt = prodstk.GetStockByProductIDForSale(prodId);



            int totalCounterProduct = 0;
            foreach (DataGridViewRow row in ActiveDataGrid.Rows)
            {
                if (Convert.ToString(row.Cells["Col_ProductID"].Value) == productId)
                {
                    totalCounterProduct += Convert.ToInt32(row.Cells["Col_Quantity"].Value);
                }
            }


            int totalStock = 0;
            foreach (DataRow dtrow in stkdt.Rows)
            {
                if (dtrow["ClosingStock"] != DBNull.Value)
                    totalStock += Convert.ToInt32(dtrow["ClosingStock"].ToString());
            }

            bool addedNewRow = false;
            List<int> rowIdx = new List<int>();

            int currentRowIdx = ActiveDataGrid.Rows.Count;
            //if reqired qty is > total stock remove all productin the batch (ignoring price)
            //if (reqQty > totalStock)
            if (totalCounterProduct >= totalStock)
            {
                reqQty = totalCounterProduct;
                checkCounterDt = false; // we'll NOT check items in the counter sale datatable while merging
                foreach (DataGridViewRow row in ActiveDataGrid.Rows)
                {
                    if ((Convert.ToString(row.Cells["Col_ProductID"].Value) == productId)) // && (Convert.ToString(row.Cells["Col_BatchNumber"].Value) == batchId))
                    {
                        rowIdx.Add(row.Index);
                    }
                }
            }
            else
            {
                checkCounterDt = true; // we'll check items in the counter sale while merging
                foreach (DataGridViewRow row in ActiveDataGrid.Rows)
                {
                    if ((Convert.ToString(row.Cells["Col_ProductID"].Value) == productId) && (Convert.ToString(row.Cells["Col_BatchNumber"].Value) == batchId) && (Convert.ToString(double.Parse(row.Cells["Col_SaleRate"].Value.ToString())) == saleRate))
                    {

                        rowIdx.Add(row.Index);
                    }
                }
            }
            int deletedRows = 0;
            foreach (int rowId in rowIdx)
            {
                if (!addedNewRow)
                    ActiveDataGrid.Rows.RemoveAt(rowId);
                else
                    ActiveDataGrid.Rows.RemoveAt(rowId - deletedRows);

                deletedRows++;

                UpdateTempCounterSaleDt();

                if (!addedNewRow)
                {

                    // ActiveDataGrid.Rows.Add();


                    ActiveDataGrid.addNewBlankRow();
                    ActiveDataGrid.IsAllowNewRow = true;
                    addedNewRow = true;
                    //for (int i = 0; i < rowIdx.Count; i++)
                    //{
                    //    rowIdx[i] = rowIdx[i] - 1;
                    //}
                    //ActiveDataGrid.MainDataGridCurrentRow.Selected = true;
                    //ActiveDataGrid.MainDataGridCurrentRow[rowId].sele

                    //if (currentRowIdx <= 0)
                    //    ActiveDataGrid.Rows[0].Selected = true;
                    //else
                    //    ActiveDataGrid.Rows[currentRowIdx - 1].Selected = true;
                }
            }

            return checkCounterDt;
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
            //DataTable dt = ActiveDataGrid.GetGridData();
            //if (dt.Rows.Count > 0)
            //    dt.WriteXml(General.GetCounterSaleTempFile());
        }


        private void FillBatchStockOptionalMultipleBatch(int requiredqty, int currentBatchStk, string productID, ref double mmrpPass, ref double mrate, ref int mpakn, ref string mbtnoPass, ref string mprodno2, ref int mcurrentindex, ref int oldmqty, ref int mqty, ref int custno)
        {
            //bool found = false;

            int mmaingridrowIndex = 0;
            DataTable stkdt = new DataTable();
            Stock prodstk = new Stock();
            int mycolindex = 0;
            int msalestk = requiredqty;
            int mactualclosingstock = 0;

            mcurrentindex = ActiveDataGrid.MainDataGridCurrentRow.Index;

            if (ActiveDataGrid.Rows.Count > 0)
                mmaingridrowIndex = ActiveDataGrid.MainDataGridCurrentRow.Index;

            stkdt = prodstk.GetStockByProductIDForSale(productID);

            //int counterProductStk = 0;
            //int totCounterProductStk = 0;

            DataTable dtTempCounterSale = CacheObject.Get<DataTable>("TempCounterSale");


            foreach (DataRow dtrow in stkdt.Rows)
            {
                if (dtrow["ClosingStock"] != DBNull.Value && dtrow["BatchNumber"].ToString() == mbtnoPass && Convert.ToDouble(dtrow["SaleRate"].ToString()) == mrate)
                {
                    mactualclosingstock += Convert.ToInt32(dtrow["ClosingStock"].ToString());

                    if (dtTempCounterSale != null)
                    {
                        if (dtTempCounterSale.Rows.Count > 0)
                        {
                            foreach (DataRow drTempCounter in dtTempCounterSale.Rows)
                            {
                                if (drTempCounter["ProductID"].ToString() == productID && drTempCounter["BatchID"].ToString() == dtrow["BatchNumber"].ToString() && drTempCounter["SRate"].ToString() == dtrow["SaleRate"].ToString())
                                {
                                    mactualclosingstock -= Convert.ToInt32(drTempCounter["QTY"]);

                                }
                            }
                        }
                    }
                }

            }

            //if (counterProductStk > mactualclosingstock)
            //    mactualclosingstock = counterProductStk - mactualclosingstock;
            //else
            //    mactualclosingstock -= counterProductStk;

            try
            {

                foreach (DataGridViewRow drp in ActiveDataGrid.Rows)
                {
                    if (drp.Cells["Col_ProductID"].Value != null &&
                          drp.Cells["Col_BatchNumber"].Value != null &&
                             drp.Cells["Col_MRP"].Value != null && drp.Index != mcurrentindex)
                    {
                        if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno2)
                            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_IfMultipleMRP"].Value = _SSSale.IFMultipleMRP;

                        if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno2 &&
                              drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtnoPass &&
                              double.Parse(drp.Cells["Col_SaleRate"].Value.ToString().Trim()).ToString() == Convert.ToString(mrate))
                        {
                            if (drp.Cells["Col_Quantity"].Value != null)
                                oldmqty = Convert.ToInt32(drp.Cells["Col_Quantity"].Value.ToString());


                            if (requiredqty < mactualclosingstock)
                                oldmqty += requiredqty;
                            else
                                oldmqty += mactualclosingstock;

                            drp.Cells["Col_Quantity"].Value = oldmqty;
                            ActiveDataGrid.Rows.Remove(ActiveDataGrid.MainDataGridCurrentRow);
                            //found = true;
                            break;
                        }
                    }
                }

                //if (found)
                //    return;


                foreach (DataRow dtrow in stkdt.Rows)
                {

                    // here

                    //int mbatchstock = 0;
                    //int mactualsalestock = 0;
                    //double msalerate = 0;
                    string stkdtstockid = dtrow["StockID"].ToString();
                    //int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock); //commented by Amit
                    if (dtTempCounterSale != null)
                    {
                        if (dtTempCounterSale.Rows.Count > 0)
                        {
                            foreach (DataRow drTempCounter in dtTempCounterSale.Rows)
                            {
                                if (drTempCounter["ProductID"].ToString() == productID && drTempCounter["BatchID"].ToString() == dtrow["BatchNumber"].ToString())
                                {
                                    dtrow["ClosingStock"] = Convert.ToInt32(dtrow["ClosingStock"]) - Convert.ToInt32(drTempCounter["QTY"]);

                                }
                            }
                        }
                    }

                }


                foreach (DataRow dtrow in stkdt.Rows)
                {

                    // here

                    int mbatchstock = 0;
                    int mactualsalestock = 0;
                    double msalerate = 0;
                    string stkdtstockid = dtrow["StockID"].ToString();
                    int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock); //commented by Amit

                    //int.TryParse(Convert.ToString(Convert.ToInt32(dtrow["ClosingStock"]) - counterProductStk), out mbatchstock);


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
                        if (mactualclosingstock < msalestk)
                            ActiveDataGrid.Rows[mycolindex].DefaultCellStyle.ForeColor = Color.DarkViolet; // kiran
                        msalestk = msalestk - mactualsalestock;
                        mactualclosingstock -= mactualsalestock;
                        CalculateAmount(-1);
                        if (msalestk > 0 && mactualclosingstock > 0)
                        {
                            int CurRowIndex = ActiveDataGrid.MainDataGridCurrentRow.Index + 1;
                            if (ActiveGrid.Rows.Count == CurRowIndex)
                            {
                                int curind = ActiveDataGrid.Rows.Add();
                                if (curind < ActiveDataGrid.Rows.Count)
                                    if (txtsavecustno.Text.ToString().Trim() == "1")
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn1.BackColor;
                                    else if (txtsavecustno.Text.ToString().Trim() == "2")
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn2.BackColor;
                                    else if (txtsavecustno.Text.ToString().Trim() == "3")
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn3.BackColor;
                                    else if (txtsavecustno.Text.ToString().Trim() == "4")
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn4.BackColor;
                                    else if (txtsavecustno.Text.ToString().Trim() == "5")
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn5.BackColor;
                                    else
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn6.BackColor;
                            }
                            ActiveDataGrid.SetFocus(ActiveDataGrid.Rows.Count - 1, 11);
                            mmaingridrowIndex = mmaingridrowIndex + 1;
                        }
                    }
                }
                ActiveDataGrid.IsAllowNewRow = true;
                UpdateTempCounterSaleDt();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        private void FillMainGridwithMultipleBatch(int requiredqty, string productID, bool checkCounterDt, string batchId)
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
            stkdt = SortByBatch(stkdt, batchId);

            int counterProductStk = 0;
            DataTable dtTempCounterSale = CacheObject.Get<DataTable>("TempCounterSale");
            if (dtTempCounterSale != null)
            {
                if (dtTempCounterSale.Rows.Count > 0)
                {
                    foreach (DataRow drTempCounter in dtTempCounterSale.Rows)
                    {
                        if (drTempCounter["ProductID"].ToString() == productID)
                        {
                            counterProductStk += Convert.ToInt32(drTempCounter["QTY"]);
                        }
                    }
                }
            }

            foreach (DataRow dtrow in stkdt.Rows)
            {
                if (dtrow["ClosingStock"] != DBNull.Value)
                    if (Convert.ToInt32(dtrow["ClosingStock"].ToString()) != 0)
                        mactualclosingstock += Convert.ToInt32(dtrow["ClosingStock"].ToString());
            }
            mactualclosingstock -= counterProductStk;
            try
            {

                foreach (DataRow dtrow in stkdt.Rows)
                {

                    // here

                    int mbatchstock = 0;
                    int mactualsalestock = 0;
                    double msalerate = 0;
                    string stkdtstockid = dtrow["StockID"].ToString();
                    //int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock); //commented by Amit

                    if (dtTempCounterSale != null)
                    {
                        if (dtTempCounterSale.Rows.Count > 0 && checkCounterDt)
                        {
                            foreach (DataRow drTempCounter in dtTempCounterSale.Rows)
                            {
                                if (drTempCounter["ProductID"].ToString() == productID && drTempCounter["BatchID"].ToString() == dtrow["BatchNumber"].ToString() && drTempCounter["SRate"].ToString() == dtrow["SaleRate"].ToString())
                                {
                                    //dtrow["ClosingStock"] = Convert.ToInt32(dtrow["ClosingStock"]) - Convert.ToInt32(drTempCounter["QTY"]);
                                    mbatchstock = Convert.ToInt32(dtrow["ClosingStock"]) - Convert.ToInt32(drTempCounter["QTY"]);
                                }
                                else
                                    mbatchstock = Convert.ToInt32(dtrow["ClosingStock"]);
                            }
                        }
                        else
                            int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock);
                    }
                    else
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
                        //ActiveDataGrid.Rows[mycolindex].Cells["Col_ProdScheduleDrugCode"].Value = dtrow["ProdScheduleDrugCode"].ToString(); // [ansuman][29.11.2016]
                        if (dtrow["ProdScheduleDrugCode"] != null)
                            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value = dtrow["ProdScheduleDrugCode"];
                        int mclstkdr1 = 0;
                        int.TryParse(dtrow["ClosingStock"].ToString(), out mclstkdr1);
                        ActiveDataGrid.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;
                        if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value != null && ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value.ToString() == "H1")
                            ActiveDataGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = Color.Tomato;
                        else if (mactualclosingstock < msalestk)
                            ActiveDataGrid.Rows[mycolindex].DefaultCellStyle.ForeColor = Color.DarkViolet; // kiran                       
                        msalestk = msalestk - mactualsalestock;
                        mactualclosingstock -= mactualsalestock;
                        CalculateAmount(-1);
                        if (msalestk > 0 && mactualclosingstock > 0)
                        {
                            int CurRowIndex = ActiveDataGrid.MainDataGridCurrentRow.Index + 1;
                            ActiveDataGrid.MainDataGridCurrentRow.ReadOnly = true;
                            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                            if (cbEditRate.Checked == true)
                                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = false;
                            if (ActiveGrid.Rows.Count == CurRowIndex)
                            {
                                int curind = ActiveDataGrid.Rows.Add();
                                if (curind < ActiveDataGrid.Rows.Count)
                                    if (txtsavecustno.Text.ToString().Trim() == "1")
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn1.BackColor;
                                    else if (txtsavecustno.Text.ToString().Trim() == "2")
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn2.BackColor;
                                    else if (txtsavecustno.Text.ToString().Trim() == "3")
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn3.BackColor;
                                    else if (txtsavecustno.Text.ToString().Trim() == "4")
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn4.BackColor;
                                    else if (txtsavecustno.Text.ToString().Trim() == "5")
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn5.BackColor;
                                    else
                                        ActiveDataGrid.Rows[curind].DefaultCellStyle.BackColor = btn6.BackColor;
                            }
                            ActiveDataGrid.SetFocus(ActiveDataGrid.Rows.Count - 1, 11);
                            mmaingridrowIndex = mmaingridrowIndex + 1;
                        }
                    }
                }

                RemoveBlankRowsInActiveGrid();

                MergeSameBatchesinActiveGrid();
                ActiveDataGrid.IsAllowNewRow = true;
                UpdateTempCounterSaleDt();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void MergeSameBatchesinActiveGrid()
        {
            //foreach (DataGridViewRow drp in ActiveDataGrid.Rows)
            //{
            //    if (drp.Cells["Col_ProductID"].Value != null &&
            //          drp.Cells["Col_BatchNumber"].Value != null &&
            //             drp.Cells["Col_MRP"].Value != null)
            //    {
            //        if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno)
            //            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_IfMultipleMRP"].Value = _SSSale.IFMultipleMRP;

            //        if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno &&
            //              drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno &&
            //                 drp.Cells["Col_MRP"].Value.ToString().Trim() == mmrp.ToString("#0.00") && drp.Index != mcurrentindex)
            //        {
            //            if (drp.Cells["Col_Quantity"].Value != null)
            //                oldmqty = Convert.ToInt32(drp.Cells["Col_Quantity"].Value.ToString());


            //            int currentBatchStk = 0;
            //            //Check stock from stock table
            //            DataTable dtCurrStk = stk.GetStockByStockIDForDBCRNote(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString());
            //            foreach (DataRow drCurrStk in dtCurrStk.Rows)
            //            {
            //                if (drCurrStk["ClosingStock"] != null)
            //                    currentBatchStk = Convert.ToInt32(drCurrStk["ClosingStock"]);
            //            }

            //            oldmqty += mqty;

            //            if (oldmqty > currentBatchStk)
            //                drp.Cells["Col_Quantity"].Value = currentBatchStk;
            //            else
            //                drp.Cells["Col_Quantity"].Value = oldmqty;

            //            ActiveDataGrid.Rows.Remove(ActiveDataGrid.MainDataGridCurrentRow);
            //            break;
            //        }
            //    }
            //}

        }

        private void RemoveBlankRowsInActiveGrid()
        {
            if (ActiveDataGrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in ActiveDataGrid.Rows)
                {

                    if (row.Cells["Col_ProductID"].Value == null)
                    {
                        ActiveDataGrid.Rows.RemoveAt(row.Index);
                    }
                }
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

            SsStock stk = new SsStock();

            foreach (DataGridViewRow drp in ActiveDataGrid.Rows)
            {
                if (drp.Cells["Col_ProductID"].Value != null &&
                      drp.Cells["Col_BatchNumber"].Value != null &&
                         drp.Cells["Col_MRP"].Value != null)
                {
                    if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno)
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_IfMultipleMRP"].Value = _SSSale.IFMultipleMRP;

                    if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno &&
                          drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno &&
                             drp.Cells["Col_MRP"].Value.ToString().Trim() == mmrp.ToString("#0.00") && drp.Index != mcurrentindex)
                    {
                        if (drp.Cells["Col_Quantity"].Value != null)
                            oldmqty = Convert.ToInt32(drp.Cells["Col_Quantity"].Value.ToString());


                        int currentBatchStk = 0;
                        //Check stock from stock table
                        DataTable dtCurrStk = stk.GetStockByStockIDForDBCRNote(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString());
                        foreach (DataRow drCurrStk in dtCurrStk.Rows)
                        {
                            if (drCurrStk["ClosingStock"] != null)
                                currentBatchStk = Convert.ToInt32(drCurrStk["ClosingStock"]);
                        }

                        oldmqty += mqty;

                        if (oldmqty > currentBatchStk)
                            drp.Cells["Col_Quantity"].Value = currentBatchStk;
                        else
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

        private void ActiveDataGridOnProductSelected(DataGridViewRow productRow)
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
                ActiveDataGrid.MainDataGridCurrentRow.Cells[0].Value = productRow.Cells[0].Value;
                _SSSale.ProductID = productRow.Cells[0].Value.ToString();
                mprodno = _SSSale.ProductID;
                double.TryParse(productRow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                _SSSale.PurchaseRate = mprate;
                int.TryParse(productRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);
                _SSSale.Closingstock = mclstk;

                txtMinlevel.Text = (string.IsNullOrEmpty(Convert.ToString(productRow.Cells["Col_MinLevel"].Value)) == false) ? productRow.Cells["Col_MinLevel"].Value.ToString() : "0";
                txtMaxlevel.Text = (string.IsNullOrEmpty(Convert.ToString(productRow.Cells["Col_MaxLevel"].Value)) == false) ? productRow.Cells["Col_MaxLevel"].Value.ToString() : "0";

                if (mclstk >= 0 || (General.CurrentSetting.MsetSaleAllowNegativeStock == "Y" && mclstk < 0))
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
                    // ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value = productRow.Cells["Col_ProdScheduleDrugCode"].Value;

                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value != null && ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value.ToString() == "H1")
                        ActiveDataGrid.MainDataGridCurrentRow.DefaultCellStyle.BackColor = Color.Tomato;
                    else
                        ResetGridColour();
                    //here
                    if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                        int.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);
                    else
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = 0;
                    if (productRow.Cells["Col_LastStockID"].Value != null)
                        mlastsalestockid = productRow.Cells["Col_LastStockID"].Value.ToString();
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value = productRow.Cells["Col_ClosingStock"].Value;
                    if (mclstk <= 0 && General.CurrentSetting.MsetSaleAllowNegativeStock == "Y")
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_IFTempSale"].Value = "Y";

                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = true;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = true;
                    if (cbEditRate.Checked == true)
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = true;
                    else
                        ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = false;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                    ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Amount"].ReadOnly = true;


                    int currentrow = ActiveDataGrid.MainDataGridCurrentRow.Index;
                    int totproductsale = 0;
                    int saleqty = 0;
                    int rowmqty = 0;
                    foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                    {
                        if (dr.Index != currentrow && dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                        {
                            if (dr.Cells["Col_Quantity"].Value != null)
                                int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out saleqty);
                            totproductsale += saleqty;
                            if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                                int.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out rowmqty);

                        }
                    }
                    // 20/4
                    // mclstk = mclstk + rowmqty - totproductsale; commented by Amit, as mclstk is calculated runtime
                    if ((mclstk == 0 && General.CurrentSetting.MsetSaleAllowNegativeStock != "Y") && mifshortlisted != "N" && mqty == 0)
                    {
                        lblFooterMessage.Text = "No Stock";
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
                        lblFooterMessage.Text = "Product Stock :" + mclstk.ToString() + " : ";
                        try
                        {
                            if (mprodno != "")
                                FillLastSaleDataFromMasterProduct();
                        }
                        catch (Exception ex) { Log.WriteError(ex.ToString()); }
                        if (General.CurrentSetting.MsetSaleAllowNegativeStock == "Y")
                        {
                            ActiveDataGrid.ColumnsMain["Col_BatchNumber"].ReadOnly = false;
                            ActiveDataGrid.ColumnsMain["Col_Expiry"].ReadOnly = false;
                            ActiveDataGrid.ColumnsMain["Col_SaleRate"].ReadOnly = false;
                            ActiveDataGrid.SetFocus(6);
                        }
                        else
                        {
                            ActiveDataGrid.ColumnsMain["Col_Quantity"].ReadOnly = false;
                            ActiveDataGrid.SetFocus(11);
                        }
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
        private DataRow ActiveGrid_OnProductBarCodeScaned(string scanCode)
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
        private void ActiveDataGridOnCellEntered(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    SetGridRowColour();
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

                        UpdateTempCounterSaleDt();
                    }
                }
                General.ProdID = ""; // [ansuman] [02/11/2016]
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        private void ActiveDataGridOnBatchSelected(DataGridViewRow batchRow)
        {
            int mclosingstock = 0;
            string mexpirydate = "";
            string mexpiry = "";
            double mmrpn = 0;
            string mbatchno = "";
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
                mbatchno = batchRow.Cells["Col_Batchno"].Value.ToString().Trim();


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
                    lblFooterMessage.Text = "Expired Product";
                    PSMessageBox.Show("Product Expired", "Invalid expiry date", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
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

                    _SSSale.IFMultipleMRP = _SSSale.IfmultipleMRP(mprodno, mbatchno, mmrpn);
                    lblFooterMessage.Text = "";
                    int currentrow = ActiveDataGrid.MainDataGridCurrentRow.Index;
                    int totbatchsale = 0;
                    int totproductsale = 0;
                    int saleqty = 0;
                    int rowmqty = 0;
                    foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                    {
                        if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                        {
                            if (dr.Index != currentrow)
                            {
                                if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                                    int.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out rowmqty);
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
                    // 16/10
                    // here
                    //mclstk = mclstk + rowmqty - totproductsale; Commented by Amit as stock is calculate runtime

                    //if (totbatchsale > mclosingstock)
                    //    mclosingstock = totbatchsale - mclosingstock;
                    //else
                    //    mclosingstock = mclosingstock - totbatchsale;

                    lblFooterMessage.Text = "Product Stock :" + mclstk.ToString().Trim() + " : Batch Stock :" + mclosingstock.ToString().Trim();

                    _SSSale.CurrentProductStock = mclstk;
                    _SSSale.CurrentBatchStock = mclosingstock;
                    // setRightFoooterMessage(batchRow);
                    if (General.CurrentSetting.MsetSaleAllowNegativeStock != "Y" && _SSSale.CurrentBatchStock <= 0)
                    {
                        lblFooterMessage.Text = "Batch Stock Zero";
                        ActiveDataGrid.SetFocus(1);
                    }
                    else
                    {
                        if (General.CurrentSetting.MsetSaleEditRateInCounterSale == "Y" && cbEditRate.Checked == true)
                        {
                            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = false;
                            ActiveDataGrid.SetFocus(10);
                            ActiveDataGrid.IsFocusSameCell = true;
                        }
                        else if (cbEditRate.Checked == true)
                        {
                            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = false;
                            ActiveDataGrid.SetFocus(10);
                        }
                        else
                        {
                            ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = true;
                            ActiveDataGrid.SetFocus(11);
                        }
                    }
                }
                setRightFoooterMessage(batchRow);
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        //private void setRightFoooterMessage(DataGridViewRow batchRow)
        //{
        //    try
        //    {
        //        string Message = string.Empty;
        //        Message += (string.IsNullOrEmpty(Convert.ToString(batchRow.Cells["BillNo"].Value)) == true) ? string.Empty
        //                 : "BillNo:" + batchRow.Cells["BillNo"].Value.ToString();
        //        Message += (string.IsNullOrEmpty(Convert.ToString(batchRow.Cells["Col_PurchaseDate"].Value)) == true) ? string.Empty
        //                : " BillDate:" + batchRow.Cells["Col_PurchaseDate"].Value.ToString();
        //        Message += (string.IsNullOrEmpty(Convert.ToString(batchRow.Cells["PartyName"].Value)) == true) ? string.Empty
        //            : " Party:" + batchRow.Cells["PartyName"].Value.ToString();

        //        lblRightSideFooterMsg.Text = Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteError(ex.ToString());
        //    }
        //}

        private void ActiveDataGridOnRowAdded(object sender)
        {
            try
            {
                //  RefreshProductGrid();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void RefreshProductGrid()
        {
            try
            {
                DataTable dtable = new DataTable();
                Product prod = new Product();
                dtable = prod.GetOverviewData();

                ActiveGrid.DataSourceProductList = dtable;
                ActiveGrid.BindGridProductList();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void ActiveDataGridOnRowDeleted(object sender)
        {
            try
            {
                DataGridViewRow deletedrow = (DataGridViewRow)sender;
                int deletedrowindex = deletedrow.Index;
                CalculateAmount(deletedrowindex);
                lblFooterMessage.Text = "";

                UpdateTempCounterSaleDt();

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private DataTable CreateCounterSaleDt()
        {
            try
            {
                dtTempCounterSale = CacheObject.Get<DataTable>("TempCounterSale");
                List<DataRow> rowsToDelete = new List<DataRow>();
                if (dtTempCounterSale == null)
                {
                    dtTempCounterSale = new DataTable();
                    dtTempCounterSale.Columns.Add("ProductID", typeof(string));
                    dtTempCounterSale.Columns.Add("BatchID", typeof(string));
                    dtTempCounterSale.Columns.Add("QTY", typeof(int));
                    dtTempCounterSale.Columns.Add("SRate", typeof(double));
                    dtTempCounterSale.Columns.Add("FormName", typeof(string));
                    dtTempCounterSale.Columns.Add("StockID", typeof(string));
                    //productid, batch,mrp
                    CacheObject.Add(dtTempCounterSale, "TempCounterSale");
                }

                foreach (DataRow item in dtTempCounterSale.Rows)
                {
                    if (string.Equals(item["FormName"], this.Name))
                    {
                        rowsToDelete.Add(item);
                    }
                }
                foreach (DataRow row in rowsToDelete)
                {
                    dtTempCounterSale.Rows.Remove(row);
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message);
            }
            return dtTempCounterSale;

        }

        private void UpdateTempCounterSaleDt()
        {
            try
            {
                DataTable dtTempCounterSale = CreateCounterSaleDt();
                //dtTempCounterSale.Clear();

                foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                {
                    if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_SaleRate"].Value != null)
                    {
                        if (dtTempCounterSale.Rows.Count > 0)
                        {
                            //  DataRow[] TempCounterSale = dtTempCounterSale.Select("ProductID=" + dr.Cells["Col_ProductID"].Value + " And BatchID=" + dr.Cells["Col_BatchNumber"].Value + " And SRate=" + dr.Cells["Col_SaleRate"].Value);
                            DataRow[] TempCounterSale = dtTempCounterSale.Select("ProductID='" + dr.Cells["Col_ProductID"].Value + "' And BatchID='" + dr.Cells["Col_BatchNumber"].Value + "' And SRate='" + dr.Cells["Col_SaleRate"].Value + "' And FormName='" + this.Name + "'");
                            if (TempCounterSale.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_Quantity"].Value)))
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(TempCounterSale[0]["QTY"])))
                                        TempCounterSale[0]["QTY"] = Convert.ToInt32(TempCounterSale[0]["QTY"]) + Convert.ToInt32(dr.Cells["Col_Quantity"].Value);
                                    else
                                        TempCounterSale[0]["QTY"] = dr.Cells["Col_Quantity"].Value;
                                }

                            }
                            else
                            {
                                DataRow drTempCounterSale = dtTempCounterSale.NewRow();

                                drTempCounterSale["StockID"] = dr.Cells["Col_StockID"].Value;
                                drTempCounterSale["ProductID"] = dr.Cells["Col_ProductID"].Value;
                                drTempCounterSale["BatchID"] = dr.Cells["Col_BatchNumber"].Value;
                                drTempCounterSale["QTY"] = dr.Cells["Col_Quantity"].Value;
                                drTempCounterSale["FormName"] = this.Name;

                                if (dr.Cells["Col_SaleRate"].Value != null)
                                    drTempCounterSale["SRate"] = dr.Cells["Col_SaleRate"].Value;
                                else
                                    drTempCounterSale["SRate"] = DBNull.Value;

                                dtTempCounterSale.Rows.Add(drTempCounterSale);

                            }
                        }
                        else
                        {
                            DataRow drTempCounterSale = dtTempCounterSale.NewRow();
                            drTempCounterSale["StockID"] = dr.Cells["Col_StockID"].Value;
                            drTempCounterSale["ProductID"] = dr.Cells["Col_ProductID"].Value;
                            drTempCounterSale["BatchID"] = dr.Cells["Col_BatchNumber"].Value;
                            drTempCounterSale["QTY"] = dr.Cells["Col_Quantity"].Value;
                            drTempCounterSale["FormName"] = this.Name;

                            if (dr.Cells["Col_SaleRate"].Value != null)
                                drTempCounterSale["SRate"] = dr.Cells["Col_SaleRate"].Value;
                            else
                                drTempCounterSale["SRate"] = DBNull.Value;

                            dtTempCounterSale.Rows.Add(drTempCounterSale);
                        }
                    }
                }

                for (int i = 1; i <= 6; i++)
                {
                    DataGridView grid = (DataGridView)this.Controls.Find(string.Format("mpPVC{0}", i), true).FirstOrDefault();
                    if (string.Equals(txtsavecustno.Text, i.ToString()) == false)
                    {

                        foreach (DataGridViewRow dr in grid.Rows)
                        {
                            if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_SaleRate"].Value != null)
                            {
                                if (dtTempCounterSale.Rows.Count > 0)
                                {
                                    DataRow[] TempCounterSale = dtTempCounterSale.Select("ProductID='" + dr.Cells["Col_ProductID"].Value + "' And BatchID='" + dr.Cells["Col_BatchNumber"].Value + "' And SRate='" + dr.Cells["Col_SaleRate"].Value + "' And FormName='" + this.Name + "'");
                                    if (TempCounterSale.Length > 0)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_Quantity"].Value)))
                                        {
                                            if (!string.IsNullOrEmpty(Convert.ToString(TempCounterSale[0]["QTY"])))
                                                TempCounterSale[0]["QTY"] = Convert.ToInt32(TempCounterSale[0]["QTY"]) + Convert.ToInt32(dr.Cells["Col_Quantity"].Value);
                                            else
                                                TempCounterSale[0]["QTY"] = dr.Cells["Col_Quantity"].Value;
                                        }

                                    }
                                    else
                                    {
                                        DataRow drTempCounterSale = dtTempCounterSale.NewRow();

                                        drTempCounterSale["StockID"] = dr.Cells["Col_StockID"].Value;
                                        drTempCounterSale["ProductID"] = dr.Cells["Col_ProductID"].Value;
                                        drTempCounterSale["BatchID"] = dr.Cells["Col_BatchNumber"].Value;
                                        drTempCounterSale["QTY"] = dr.Cells["Col_Quantity"].Value;
                                        drTempCounterSale["FormName"] = this.Name;
                                        if (dr.Cells["Col_SaleRate"].Value != null)
                                            drTempCounterSale["SRate"] = dr.Cells["Col_SaleRate"].Value;
                                        else
                                            drTempCounterSale["SRate"] = DBNull.Value;

                                        dtTempCounterSale.Rows.Add(drTempCounterSale);
                                    }
                                }
                                else
                                {
                                    DataRow drTempCounterSale = dtTempCounterSale.NewRow();

                                    drTempCounterSale["StockID"] = dr.Cells["Col_StockID"].Value;
                                    drTempCounterSale["ProductID"] = dr.Cells["Col_ProductID"].Value;
                                    drTempCounterSale["BatchID"] = dr.Cells["Col_BatchNumber"].Value;
                                    drTempCounterSale["QTY"] = dr.Cells["Col_Quantity"].Value;
                                    drTempCounterSale["FormName"] = this.Name;
                                    if (dr.Cells["Col_SaleRate"].Value != null)
                                        drTempCounterSale["SRate"] = dr.Cells["Col_SaleRate"].Value;
                                    else
                                        drTempCounterSale["SRate"] = DBNull.Value;

                                    dtTempCounterSale.Rows.Add(drTempCounterSale);
                                }
                            }

                        }
                    }
                }

                CacheObject.Add(dtTempCounterSale, "TempCounterSale");
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message);
            }
        }
        private void ActiveDataGridTabKeyPressed()
        {
            double savingcusttotal = SavingCustomersTotalSale();
            int numberofLines = General.GetNumberofLinesInGrid(ActiveDataGrid);
            if (savingcusttotal > 0 && ((string.IsNullOrEmpty(Convert.ToString(ActiveGrid.MainDataGridCurrentRow.Cells["Col_ProductName"].Value)) == true) && (ActiveGrid.MainDataGridCurrentRow.Cells["Col_ProductID"].Value == null && ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Amount"].Value == null) || (General.CurrentSetting.MsetPrintFixNumberOfLines == "Y")))
            {
                try
                {
                    //CheckforDoctorRequired();
                    btnIfDebitCredit.Visible = false;
                    lblBank.Visible = false;
                    mcbBankAccount.Visible = false;
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
                    // cbTransactionType.Focus();
                    txtPatientName.Focus();
                    if (_SSSale.PrescriptionFileName != string.Empty)
                        psBtnAttachPrescription.Text = "Show Prescription";
                    else
                    {
                        psBtnAttachPrescription.Text = "Attach Prescription";
                        psBtnRemovePrescription.Visible = false;
                    }
                    if (pnlFinal.Visible && General.CurrentSetting.MsetSaleShowProfitInSaleBill == "Y")
                    {
                        lblProfit.Visible = txtProfit.Visible = true;
                        txtProfit.Text = _SSSale.TotalProfitInRupees.ToString("#0.00");
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
            else if (string.IsNullOrEmpty(Convert.ToString(ActiveGrid.MainDataGridCurrentRow.Cells["Col_ProductName"].Value)) == false)
            {
                ActiveGrid.SetForwardCellIndex();
            }
        }

        #region OCT _Pravin
        Doctor odoc = new Doctor();
        private void CheckforDoctorRequired()
        {
            if (CheckOCTCategories() == false)
            {
                mcbDoctor.SelectedID = GetDoctor().Id;
                mcbDoctor_EnterKeyPressed(mcbDoctor, new EventArgs());
                // mcbDoctor.Visible = txtDoctorAddress.Visible = false;
            }
            // else
            //  mcbDoctor.Visible = txtDoctorAddress.Visible = true;
        }
        private Doctor GetDoctor()
        {
            DataLayer.DBDoctor odb = new DataLayer.DBDoctor();
            if (odoc.Id == null || string.IsNullOrEmpty(odoc.Id) == true)
            {
                DataRow dr = odb.ReadDetailsByName(".");
                if (dr != null)
                {
                    odoc.Id = Convert.ToString(dr["DocID"]);
                    odoc.Name = Convert.ToString(dr["DocName"]);
                    odoc.DocAddress = Convert.ToString(dr["DocAddress"]);
                    odoc.DocShortNameAddress = Convert.ToString(dr["DocShortNameAddress"]);
                }
                else
                {
                    odb.AddDetails(Guid.NewGuid().ToString().ToUpper().Replace("-", ""), ".", "", "", "", "", "", "", "", "", DateTime.Now.Date.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"));
                    GetDoctor();
                }
            }
            return odoc;
        }
        private bool CheckOCTCategories()
        {
            foreach (DataGridViewRow item in ActiveDataGrid.Rows)
            {
                if (item.Cells["Col_ProductID"].Value != null)
                {
                    (ActiveGrid.DataSourceProductList).DefaultView.RowFilter = string.Format("ProductID = '" + item.Cells["Col_ProductID"].Value + "'");
                    if ((ActiveGrid.DataSourceProductList).DefaultView.Count > 0)
                    {
                        string CatName = Convert.ToString((ActiveGrid.DataSourceProductList).DefaultView[0]["ProdCategoryID"]);
                        DataLayer.DBProductCategory odbCat = new DataLayer.DBProductCategory();
                        DataRow dr = odbCat.ReadDetailsByID(CatName);
                        if (dr != null)
                        {
                            if ("Y" == Convert.ToString(dr["IfDoctorRequired"]))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        private double SavingCustomersTotalSale()
        {
            double savingcusttot = 0;
            if (txtsavecustno.Text.ToString().Trim() == "1")
                savingcusttot = Convert.ToDouble(txtamount1.Text.ToString());
            else if (txtsavecustno.Text.ToString().Trim() == "2")
                savingcusttot = Convert.ToDouble(txtamount2.Text.ToString());
            else if (txtsavecustno.Text.ToString().Trim() == "3")
                savingcusttot = Convert.ToDouble(txtamount3.Text.ToString());
            else if (txtsavecustno.Text.ToString().Trim() == "4")
                savingcusttot = Convert.ToDouble(txtamount4.Text.ToString());
            else if (txtsavecustno.Text.ToString().Trim() == "5")
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
            lblFooterMessage.Text = "";
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

            double mcreditnote = 0;
            double mdebitnote = 0;

            string ifdiscount = "Y";

            if (txtCreditNote.Text != null && txtCreditNote.Text.ToString() != string.Empty)
                mcreditnote = Convert.ToDouble(txtCreditNote.Text.ToString());
            if (txtDebitNote.Text != null && txtDebitNote.Text.ToString() != string.Empty)
                mdebitnote = Convert.ToDouble(txtDebitNote.Text.ToString());

            if (txtPatientName.SelectedID != null && txtPatientName.SelectedID != string.Empty && txtPatientName.SeletedItem.ItemData[1] != null)
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
                                    //vat 5.5
                                    if (mvatper == 6)
                                    {
                                        mvatamount5 = Math.Round((mamt * mvatper) / (100 + mvatper), 2);
                                        mmyspecialdiscountamt5 = Math.Round((mamt - mvatamount5) * mmyspecialDiscountper / 100, 2);
                                        if (ifdiscount != "N")
                                            mdiscamt5 = Math.Round((mamt - mvatamount5) * mdiscper / 100, 2);
                                        else
                                            mdiscamt5 = 0;
                                    }
                                    else if (mvatper == 13.5)
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
                                    //vat 5.5
                                    if (mvatper == 6)
                                    {
                                        mvatamount5 = Math.Round((mnewamt * mvatper) / (100 + mvatper), 2);
                                    }
                                    else if (mvatper == 13.5)
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
                                    // vat 5.5
                                    if (mvatper == 6)
                                        mTotalAmountVAT5 += (mnewamt - mvatamount5);
                                    else if (mvatper == 13.5)
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
                    double mrndamt = 0;
                    if (mTotalAmount == 0)
                    {
                        btnPutInBlackList.Visible = false;
                        mcreditnote = 0;
                        mdebitnote = 0;
                        txtCreditNote.Text = "0.00";
                        txtDebitNote.Text = "0.00";
                        txtDiscPercent.Text = "0.00";
                        txtPatientName.Text = "";
                        txtPatientName.SelectedID = "";
                        txtMobileNumber.Text = "";
                        txtTelephoneNumber.Text = "";
                        txtNarration.Text = General.CurrentSetting.MsetFixedNarration.ToString();
                        txtAddress.Text = "";
                        txtAddress.SelectedID = "";
                        mcbDoctor.Text = "";
                        mcbDoctor.SelectedID = "";
                        txtDoctorAddress.Text = "";

                    }
                    // mTotalAmount = mTotalAmount - mcreditnote + mdebitnote;
                    if (mTotalAmount <= 0)
                    {
                        ClearCreditDebitNote();
                        mTotalAmount = mTotalAmount + mcreditnote - mdebitnote;
                        txtCreditNote.Text = "0.00";
                        lblFooterMessage.Text = "Credit Note Not Adjusted...";

                    }
                    else
                    {
                        lblFooterMessage.Text = "";
                        lblRightSideFooterMsg.Text = "";
                        mtotalafterdiscount = mtotalafterdiscount - mcreditnote + mdebitnote;
                    }

                    txtTotalafterdiscount.Text = mtotalafterdiscount.ToString("#0.00");
                    if (cbRound.Checked == true)
                    {
                        if (General.CurrentSetting.MsetSaleRoundingToPreviousRupee == "Y")
                        {
                            mrndamt = Math.Floor(Math.Round(mtotalafterdiscount, 2)) - Math.Round(mtotalafterdiscount, 2);
                            txtRoundAmount.Text = mrndamt.ToString("#0.00");
                        }
                        else
                            txtRoundAmount.Text = Math.Round(Math.Round(mtotalafterdiscount, 0) - Math.Round(mtotalafterdiscount, 2), 2).ToString("#0.00");

                        txtBillAmount.Text = mTotalAmount.ToString("#0.00");
                        txtBillAmount2.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString()) + mtotalmyspecialdiscamt12point5 + mtotalmyspecialdiscamt5), 2).ToString("#0.00");
                        txtNetAmount.Text = txtBillAmount2.Text;
                        //txtNetAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + mtotalmyspecialdiscamt12point5 + mtotalmyspecialdiscamt5), 2).ToString("#0.00");
                    }
                    else
                    {
                        txtRoundAmount.Text = "0.00";
                        txtBillAmount.Text = mTotalAmount.ToString("#0.00");
                        txtBillAmount2.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString()) + mtotalmyspecialdiscamt12point5 + mtotalmyspecialdiscamt5), 2).ToString("#0.00");
                        txtNetAmount.Text = txtBillAmount2.Text;
                    }

                    if (txtsavecustno.Text.ToString().Trim() == "1")
                    {
                        txtamount1.Text = mTotalAmount.ToString("#0.00");
                        txtitems1.Text = itemCount.ToString();
                        txtBillAmount.Text = txtamount1.Text;
                        //txtNetAmount.Text = txtamount1.Text;
                        //txtBillAmount.Text = txtNetAmount.Text;
                    }
                    else if (txtsavecustno.Text.ToString().Trim() == "2")
                    {

                        txtamount2.Text = mTotalAmount.ToString("#0.00");
                        txtitems2.Text = itemCount.ToString();
                        txtBillAmount.Text = txtamount2.Text;
                        //txtNetAmount.Text = txtamount2.Text;
                        //txtBillAmount.Text = txtNetAmount.Text;
                    }
                    else if (txtsavecustno.Text.ToString().Trim() == "3")
                    {

                        txtamount3.Text = mTotalAmount.ToString("#0.00");
                        txtitems3.Text = itemCount.ToString();
                        txtBillAmount.Text = txtamount3.Text;
                        //txtNetAmount.Text = txtamount3.Text;
                        //txtBillAmount.Text = txtNetAmount.Text;
                    }
                    else if (txtsavecustno.Text.ToString().Trim() == "4")
                    {
                        txtamount4.Text = mTotalAmount.ToString("#0.00");
                        txtitems4.Text = itemCount.ToString();
                        txtBillAmount.Text = txtamount4.Text;
                        //txtNetAmount.Text = txtamount4.Text;
                        //txtBillAmount.Text = txtNetAmount.Text;
                    }
                    else if (txtsavecustno.Text.ToString().Trim() == "5")
                    {
                        txtamount5.Text = mTotalAmount.ToString("#0.00");
                        txtitems5.Text = itemCount.ToString();
                        txtBillAmount.Text = txtamount6.Text;
                        //txtNetAmount.Text = txtamount5.Text;
                        //txtBillAmount.Text = txtNetAmount.Text;
                    }
                    else
                    {
                        txtamount6.Text = mTotalAmount.ToString("#0.00");
                        txtitems6.Text = itemCount.ToString();
                        txtBillAmount.Text = txtamount6.Text;
                        //txtNetAmount.Text = txtamount6.Text;
                        // txtBillAmount.Text = txtNetAmount.Text;
                    }
                }

                CalculateRoundAmount();
                CalculateProfitPercent();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }

        private void CalculateRoundAmount()
        {
            double mrndamt = 0;
            double mtotamt = Convert.ToDouble(txtBillAmount.Text.ToString());
            double totalamt = mtotamt;
            if (cbRound.Checked == true)
            {
                double mtotalafterdiscount = Convert.ToDouble(txtTotalafterdiscount.Text.ToString());
                //double mtotalafterdiscount = Convert.ToDouble(txtNetAmount.Text.ToString()); //
                if (General.CurrentSetting.MsetSaleRoundingToPreviousRupee == "Y")
                {
                    mrndamt = Math.Floor(Math.Round(mtotalafterdiscount, 2)) - Math.Round(mtotalafterdiscount, 2);
                    txtRoundAmount.Text = mrndamt.ToString("#0.00");
                }
                else
                    txtRoundAmount.Text = Math.Round(Math.Round(mtotalafterdiscount, 0) - Math.Round(mtotalafterdiscount, 2), 2).ToString("#0.00");
                _SSSale.CrdbRoundAmount = Convert.ToDouble(txtRoundAmount.Text.ToString());
                txtNetAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                txtBillAmount.Text = totalamt.ToString("#0.00");
                mtotamt = Convert.ToDouble(txtBillAmount.Text.ToString());
                if (_SSSale.CrdbAmountClear > 0 && _SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditSale)
                    _SSSale.IfFullPayment = "N";

                if (_SSSale.IfFullPayment != "Y")
                {
                    _SSSale.CrdbAmountBalance = (mtotamt - _SSSale.CrdbAmountClear);
                }
                else
                {
                    if (_Mode != OperationMode.Edit)
                    {

                        _SSSale.CrdbAmountClear = mtotamt;
                        _SSSale.CrdbAmountBalance = mtotamt - _SSSale.CrdbAmountClear;
                    }
                    else
                    {
                        if (General.CurrentSetting.MsetAllowPendingCashMemo != "Y")
                        {
                            _SSSale.CrdbAmountClear = mtotamt;
                            _SSSale.CrdbAmountBalance = 0;

                        }
                        else
                            _SSSale.CrdbAmountClear = mtotamt - _SSSale.CrdbAmountBalance;
                    }
                }
                //txtAmountRcvd.Text = _SSSale.CrdbAmountClear.ToString("#0.00");
                //txtAmountBalance.Text = _SSSale.CrdbAmountBalance.ToString("#0.00");
                //if (_SSSale.CrdbAmountBalance < 0)
                //    txtAmountBalance.BackColor = Color.Red;
                //else
                //    txtAmountBalance.BackColor = Color.Snow;

                //if (_Mode == OperationMode.Add)
                //    txtAmountRcvd.Text = txtBillAmount.Text;
                //else if (_Mode == OperationMode.Edit)
                //{
                //    _SSSale.CrdbAmountNet = Convert.ToDouble(txtBillAmount.Text.ToString());
                //    _SSSale.CrdbAmountBalance = _SSSale.CrdbAmountNet - _SSSale.CrdbAmountClear;
                //    txtAmountBalance.Text = _SSSale.CrdbAmountBalance.ToString("#0.00");
                //}

            }
            else
            {
                txtRoundAmount.Text = "0.00";
                txtNetAmount.Text = Math.Round((Convert.ToDouble(txtBillAmount2.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                //txtNetAmount.Text = txtBillAmount.Text;
                //if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                //txtAmountRcvd.Text = txtBillAmount.Text;
                mtotamt = Convert.ToDouble(txtBillAmount.Text.ToString());
                // here

                if (_SSSale.CrdbAmountClear == 0 && General.CurrentSetting.MsetAllowPendingCashMemo != "N" && _SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditSale)
                    _SSSale.CrdbAmountBalance = mtotamt;
                else
                {
                    //_SSSale.CrdbAmountClear = Convert.ToDouble(txtAmountRcvd.Text.ToString());
                    _SSSale.CrdbAmountBalance = mtotamt - _SSSale.CrdbAmountClear;
                }
                //txtAmountRcvd.Text = _SSSale.CrdbAmountClear.ToString("#0.00");
                //txtAmountBalance.Text = _SSSale.CrdbAmountBalance.ToString("#0.00");
            }
            double newpendingAmount = _SSSale.PendingAmount - _SSSale.PreAmountNet + Convert.ToDouble(txtNetAmount.Text.ToString());  // [amar]

            //txtPendingBalance.Text = newpendingAmount.ToString("#0.00");//Amar
        }

        private void ClearCreditDebitNote()
        {
            double mcrnoteamt = 0;
            double mdbnoteamt = 0;
            lblFooterMessage.Text = "";

            try
            {
                foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
                {
                    string ch = string.Empty;
                    if (crdbdr.Cells["Col_Check"].Value != null)
                        ch = crdbdr.Cells["Col_Check"].Value.ToString();
                    if (ch != string.Empty)
                    {
                        crdbdr.Cells["Col_Check"].Value = string.Empty;

                    }
                }
                txtCreditNote.Text = mcrnoteamt.ToString("#0.00");
                txtDebitNote.Text = mdbnoteamt.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCRDBOK_Click>>" + Ex.Message);
            }
        }

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
                        lblFooterMessage.Text = "Bill Contains H1 Product";
                    else
                        lblFooterMessage.Text = "";
                    UpdateTempCounterSaleDt();

                    txtMinlevel.Text = "";
                    txtMaxlevel.Text = "";
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
                if (dr["ProdClosingStock"] != DBNull.Value && dr["ProdClosingStock"].ToString() != string.Empty)
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

                    if (mclosingstock > 0 || (mclosingstock == 0 && General.CurrentSetting.MsetSaleAllowNegativeStock == "Y"))
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


                txtAmountfor5VAT.Text = "0";


                txtAmountfor12VAT.Text = "0";


                txtVatInput12point5.Text = "0";


                txtVatInput5.Text = "0";


                txtSaleAmountForDiscount.Text = "0";

                if (General.CurrentSetting.MsetSaleDoNotShowNegetiveStock == "Y")
                    btnUpdateNegetiveStock.Visible = true;
                else
                    btnUpdateNegetiveStock.Visible = false;

                if (General.CurrentSetting.MsetSaleEditRateInCounterSale == "Y")
                    cbEditRate.Visible = true;
                else
                    cbEditRate.Visible = false;

                txtDiscPercent.Text = "0";
                btnPutInBlackList.Visible = false;
                _lastCustIdSelected = "1";
                txtVouchernumber.Clear();
                txtVouType.Text = "   ";
                txtNetAmount.Text = "0.00";
                txtBillAmount2.Text = "0.00";
                txtBillAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                _PreCurrentQuantity = 0;
                mcbDoctor.DataSource = null;
                mcbDoctor.Text = "";
                txtDoctorAddress.Text = "";
                txtOperator.Text = "";
                txtNoOfRows.Text = "";
                txtMobileNumber.Text = "";
                txtTelephoneNumber.Text = "";
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


                if (txtsavecustno.Text != null && txtsavecustno.Text.ToString() != string.Empty)
                    _SSSale.CustNumber = Convert.ToInt32(txtsavecustno.Text.ToString());
                if (_SSSale.CustNumber == 1)
                {
                    txtamount1.Text = "0.00";
                    txtitems1.Text = "0";
                }

                if (_SSSale.CustNumber == 2)
                {
                    txtamount2.Text = "0.00";
                    txtitems2.Text = "0";
                }
                if (_SSSale.CustNumber == 3)
                {
                    txtamount3.Text = "0.00";
                    txtitems3.Text = "0";
                }

                if (_SSSale.CustNumber == 4)
                {
                    txtamount4.Text = "0.00";
                    txtitems4.Text = "0";
                }
                if (_SSSale.CustNumber == 5)
                {
                    txtamount5.Text = "0.00";
                    txtitems5.Text = "0";
                }

                if (_SSSale.CustNumber == 6)
                {
                    txtamount6.Text = "0.00";
                    txtitems6.Text = "0";
                }
                btnPutInBlackList.Visible = false;
                lblFooterMessage.Text = "";
                txtVouchernumber.Clear();
                txtVouType.Text = "   ";
                txtNetAmount.Text = "0.00";
                txtBillAmount2.Text = "0.00";
                txtBillAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                _PreCurrentQuantity = 0;
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
                txtPatientName.Text = "";
                txtCreditNote.Text = "0.00";
                txtDebitNote.Text = "0.00";
                txtOperator.Text = "";
                txtMobileNumber.Text = "";
                txtTelephoneNumber.Text = "";
                _SSSale.PatientID = "";
                _SSSale.AccountID = "";
                _SSSale.CrdbVouType = "";
                _SSSale.SaleSubType = "";
                txtAddress.SelectedID = "";
                txtPatientName.Text = "";
                txtAddress.Text = "";
                mcbDoctor.Text = "";
                txtDoctorAddress.Text = "";
                txtPendingBalance.Text = "0.00"; //Amar
                txtNarration.Text = General.CurrentSetting.MsetFixedNarration.ToString();

                if (General.CurrentSetting.MsetSaleAllowNegativeStock == "Y")
                {
                    ActiveGrid.AllowNewBatch = true;
                }
                else
                {
                    ActiveGrid.AllowNewBatch = false;
                }
                ////if (ActiveGrid.Rows.Count == 0)
                ////    ActiveGrid.Rows.Add();
                ResetPriceColor();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void GoToFirstGrid()
        {
            ActiveGrid.SetFocus(1);
            CopyGrid(txtsavecustno.Text.ToString(), "1");
            txtsavecustno.Text = "1";
            _lastCustIdSelected = "1";

            NoofRows();
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

            if (txtsavecustno.Text.ToString().Trim() == "1")
            {
                //  ActiveGrid.BackColor = btn1.BackColor;
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
            else if (txtsavecustno.Text.ToString().Trim() == "2")
            {
                //   ActiveGrid.BackColor = btn2.BackColor;
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


            else if (txtsavecustno.Text.ToString().Trim() == "3")
            {
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
            else if (txtsavecustno.Text.ToString().Trim() == "4")
            {
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
            else if (txtsavecustno.Text.ToString().Trim() == "5")
            {
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
            else if (txtsavecustno.Text.ToString().Trim() == "6")
            {
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
            txtBillAmount.Text = "0.00";
            txtNetAmount.Text = "0.00";
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
                lblProfit.Visible = txtProfit.Visible = false;
                ActiveDataGrid.SetFocus(lastrowindex, 1);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void txtPatientName_KeyDown(object sender, KeyEventArgs e)
        {

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
            ResetPriceColor();
        }
        private void ResetPriceColor()
        {

            if (txtsavecustno.Text.ToString().Trim() == "1")
            {
                PanelRs.BackColor = btn1.BackColor;
            }

            if (txtsavecustno.Text.ToString().Trim() == "2")
            {
                PanelRs.BackColor = btn2.BackColor;
            }
            if (txtsavecustno.Text.ToString().Trim() == "3")
            {
                PanelRs.BackColor = btn3.BackColor;
            }

            if (txtsavecustno.Text.ToString().Trim() == "4")
            {
                PanelRs.BackColor = btn4.BackColor;
            }

            if (txtsavecustno.Text.ToString().Trim() == "5")
            {
                PanelRs.BackColor = btn5.BackColor;
            }

            if (txtsavecustno.Text.ToString().Trim() == "6")
            {
                PanelRs.BackColor = btn6.BackColor;
            }
        }
        private void btn1Click()
        {
            string lastCustSelected = txtsavecustno.Text.ToString().Trim();
            _lastCustIdSelected = "1";
            txtsavecustno.Text = "1";
            CopyGrid(lastCustSelected, "1");
            ChangeBackColour(ActiveDataGrid);
            txtNetAmount.Text = txtamount1.Text;
            NoofRows();
        }

        private void btn2_KeyDown(object sender, KeyEventArgs e)
        {
            btn2Click();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            btn2Click();
            ResetPriceColor();
        }
        private void btn2Click()
        {
            string lastCustSelected = txtsavecustno.Text.ToString().Trim();
            _lastCustIdSelected = "2";
            txtsavecustno.Text = "2";
            CopyGrid(lastCustSelected, "2");
            ChangeBackColour(ActiveDataGrid);
            txtNetAmount.Text = txtamount2.Text;
            NoofRows();
        }

        private void CopyGrid(string ToGrid, string FromGrid)
        {
            DataGridView copytogrid = new DataGridView();
            DataGridView copyfromgrid = new DataGridView();
            if (ToGrid == "1")
                copytogrid = mpPVC1;
            else if (ToGrid == "2")
                copytogrid = mpPVC2;
            else if (ToGrid == "3")
                copytogrid = mpPVC3;
            else if (ToGrid == "4")
                copytogrid = mpPVC4;
            else if (ToGrid == "5")
                copytogrid = mpPVC5;
            else if (ToGrid == "6")
                copytogrid = mpPVC6;

            if (FromGrid == "1")
                copyfromgrid = mpPVC1;
            else if (FromGrid == "2")
                copyfromgrid = mpPVC2;
            else if (FromGrid == "3")
                copyfromgrid = mpPVC3;
            else if (FromGrid == "4")
                copyfromgrid = mpPVC4;
            else if (FromGrid == "5")
                copyfromgrid = mpPVC5;
            else if (FromGrid == "6")
                copyfromgrid = mpPVC6;

            int columncount = ActiveGrid.ColumnsMain.Count;
            if (copytogrid.Rows.Count > 0)
                copytogrid.Rows.Clear();

            foreach (DataGridViewRow dr in ActiveGrid.Rows)
            {
                if (dr.Cells["Col_Amount"].Value != null)
                {
                    int rowindex = copytogrid.Rows.Add();
                    copytogrid.Rows[rowindex].DefaultCellStyle.ForeColor = dr.DefaultCellStyle.ForeColor;
                    copytogrid.Rows[rowindex].DefaultCellStyle.BackColor = dr.DefaultCellStyle.BackColor;

                    for (int i = 0; i < columncount; i++)
                    {
                        copytogrid.Rows[rowindex].Cells[i].Value = dr.Cells[i].Value;
                    }

                }
            }
            if (ActiveGrid.Rows.Count > 0)
                ActiveGrid.Rows.Clear();
            if (copyfromgrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow dr in copyfromgrid.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null)
                    {
                        int rowindex = ActiveGrid.Rows.Add();
                        ActiveGrid.Rows[rowindex].ReadOnly = true; //kiran
                        ActiveGrid.Rows[rowindex].DefaultCellStyle.ForeColor = dr.DefaultCellStyle.ForeColor;
                        ActiveGrid.Rows[rowindex].DefaultCellStyle.BackColor = dr.DefaultCellStyle.BackColor;
                        for (int i = 0; i < columncount; i++)
                        {
                            ActiveGrid.Rows[rowindex].Cells[i].Value = dr.Cells[i].Value;

                        }
                        //if (FromGrid == "1")
                        //    ActiveGrid.Rows[rowindex].DefaultCellStyle.BackColor = btn1.BackColor;
                        //else if (FromGrid == "2")
                        //    ActiveGrid.Rows[rowindex].DefaultCellStyle.BackColor = btn2.BackColor;
                        //else if (FromGrid == "3")
                        //    ActiveGrid.Rows[rowindex].DefaultCellStyle.BackColor = btn3.BackColor;
                        //else if (FromGrid == "4")
                        //    ActiveGrid.Rows[rowindex].DefaultCellStyle.BackColor = btn4.BackColor;
                        //else if (FromGrid == "5")
                        //    ActiveGrid.Rows[rowindex].DefaultCellStyle.BackColor = btn5.BackColor;
                        //else if (FromGrid == "6")
                        //    ActiveGrid.Rows[rowindex].DefaultCellStyle.BackColor = btn6.BackColor;
                    }
                }
            }

            int cindex = ActiveGrid.Rows.Add();
            ActiveGrid.SetFocus(cindex, 1);
        }
        private void btn3_KeyDown(object sender, KeyEventArgs e)
        {
            btn3Click();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            btn3Click();
            ResetPriceColor();
        }
        private void btn3Click()
        {
            string lastCustSelected = txtsavecustno.Text.ToString().Trim();
            _lastCustIdSelected = "3";
            txtsavecustno.Text = "3";
            CopyGrid(lastCustSelected, "3");

            ChangeBackColour(ActiveDataGrid);
            txtNetAmount.Text = txtamount3.Text;
            NoofRows();
        }
        private void btn4_KeyDown(object sender, KeyEventArgs e)
        {
            btn4Click();
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            btn4Click();
            ResetPriceColor();
        }
        private void btn4Click()
        {

            string lastCustSelected = txtsavecustno.Text.ToString().Trim();
            _lastCustIdSelected = "4";
            txtsavecustno.Text = "4";
            CopyGrid(lastCustSelected, "4");
            ChangeBackColour(ActiveDataGrid);
            txtNetAmount.Text = txtamount4.Text;
            NoofRows();
        }
        private void btn5_KeyDown(object sender, KeyEventArgs e)
        {
            btn5Click();
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            btn5Click();
            ResetPriceColor();
        }
        private void btn5Click()
        {
            string lastCustSelected = txtsavecustno.Text.ToString().Trim();
            _lastCustIdSelected = "5";
            txtsavecustno.Text = "5";
            CopyGrid(lastCustSelected, "5");
            ChangeBackColour(ActiveDataGrid);

            txtNetAmount.Text = txtamount5.Text;
            NoofRows();
        }

        private void btn6_KeyDown(object sender, KeyEventArgs e)
        {
            btn6Click();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            btn6Click();
            ResetPriceColor();
        }
        private void btn6Click()
        {

            string lastCustSelected = txtsavecustno.Text.ToString().Trim();
            _lastCustIdSelected = "6";
            txtsavecustno.Text = "6";
            CopyGrid(lastCustSelected, "6");
            ChangeBackColour(ActiveDataGrid);
            txtNetAmount.Text = txtamount6.Text;
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
            lblFooterMessage.Text = "";
            lblRightSideFooterMsg.Text = "";
            txtMinlevel.Text = "";
            txtMaxlevel.Text = "";
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
                    txtBillAmount.Text = mamt1.ToString("#0.00");
                    CalculateAmount(-1);
                    txtPatientName.ReadOnly = false;
                    txtAddress.ReadOnly = false;
                    mcbDoctor.ReadOnly = false;
                    cbTransactionType.Focus();

                }
                else if (msavenumber == 2 && mamt2 > 0)
                {
                    txtBillAmount.Text = mamt2.ToString("#0.00");
                    CalculateAmount(-1);
                    txtPatientName.ReadOnly = false;
                    txtAddress.ReadOnly = false;
                    mcbDoctor.ReadOnly = false;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 3 && mamt3 > 0)
                {
                    txtBillAmount.Text = mamt3.ToString("#0.00");
                    CalculateAmount(-1);
                    txtPatientName.ReadOnly = false;
                    txtAddress.ReadOnly = false;
                    mcbDoctor.ReadOnly = false;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 4 && mamt4 > 0)
                {
                    txtBillAmount.Text = mamt4.ToString("#0.00");
                    CalculateAmount(-1);
                    txtPatientName.ReadOnly = false;
                    txtAddress.ReadOnly = false;
                    mcbDoctor.ReadOnly = false;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 5 && mamt5 > 0)
                {
                    txtBillAmount.Text = mamt5.ToString("#0.00");
                    CalculateAmount(-1);
                    txtPatientName.ReadOnly = false;
                    txtAddress.ReadOnly = false;
                    mcbDoctor.ReadOnly = false;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 6 && mamt6 > 0)
                {
                    txtBillAmount.Text = mamt6.ToString("#0.00");
                    CalculateAmount(-1);
                    txtPatientName.ReadOnly = false;
                    txtAddress.ReadOnly = false;
                    mcbDoctor.ReadOnly = false;
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
            if (mcbDoctor.SeletedItem != null && string.IsNullOrEmpty(Convert.ToString(mcbDoctor.SelectedID)) == false && mcbDoctor.SeletedItem.ItemData[2] != null)
            {
                mcbDoctor.Text = mcbDoctor.SeletedItem.ItemData[1];
                txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2];
                mcbDoctor.ReadOnly = true;
            }
            txtDoctorAddress.Focus();

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
                {
                    MainToolStrip.Select();
                    tsBtnSave.Select();
                }
            }
            else if (e.KeyCode == Keys.Up)
                mcbDoctor.Focus();
        }
        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            double mdiscper = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDiscPercent.Text != null && txtDiscPercent.Text.ToString() != string.Empty)
                    mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());
                if (mdiscper <= General.CurrentSetting.MsetSaleMaxDiscount)
                {
                    lblFooterMessage.Text = "";
                    CalculateAmount(-1);
                    txtNetAmount.Text = txtBillAmount2.Text;
                    lblTransactionType.BackColor = General.ControlFocusColor;
                    //cbTransactionType.Focus();
                    //txtOperator.Focus();
                    if (General.CurrentSetting.MsetSaleAskRoundinginSale == "Y")
                    {
                        cbRound.Focus(); // [ansuman][15.11.2016]
                        cbRound.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        cbTransactionType.Focus(); // [ansuman]11.11.2016
                        cbRound.BackColor = Color.PapayaWhip; // [ansuman]11.11.2016
                        //pnlTransactCbBorder.BackColor = Color.Green;
                    }
                }
                else
                {
                    lblFooterMessage.Text = string.Concat("Max Discount Percent is ", General.CurrentSetting.MsetSaleMaxDiscount.ToString());
                    txtDiscPercent.Text = "0.00";
                }
            }
        }
        private void txtDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            //double mdiscper = 0;
            //double mdiscperCalculated = 0;
            //txtDiscPercent.Text = "";

            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (!string.IsNullOrEmpty(txtDiscAmount.Text))
            //    {
            //        if (!string.IsNullOrEmpty(txtNetAmount.Text))
            //        {
            //            mdiscperCalculated = (Convert.ToDouble(txtDiscAmount.Text) * 100) / Convert.ToDouble(txtNetAmount.Text);
            //            txtDiscPercent.Text = Convert.ToString(mdiscperCalculated);
            //        }
            //    }

            //    if (txtDiscPercent.Text != null && txtDiscPercent.Text.ToString() != string.Empty)
            //        mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());
            //    if (mdiscper <= General.CurrentSetting.MsetSaleMaxDiscount)
            //    {
            //        lblFooterMessage.Text = "";
            //        CalculateDiscountAmtPer(-1);
            //        CalculateAmount(-1);
            //        lblTransactionType.BackColor = General.ControlFocusColor;
            //        cbTransactionType.Focus();
            //    }
            //    else
            //    {
            //        lblFooterMessage.Text = string.Concat("Max Discount Percent is ", General.CurrentSetting.MsetSaleMaxDiscount.ToString());
            //        txtDiscPercent.Text = "0.00";
            //        txtOperator.Focus();
            //    }
            //}
            double mdiscper = 0;
            if (e.KeyCode == Keys.Enter)
            {

                if (txtDiscPercent.Text != null && txtDiscPercent.Text.ToString() != string.Empty)
                    mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());
                if (mdiscper <= General.CurrentSetting.MsetSaleMaxDiscount)
                {
                    lblFooterMessage.Text = "";
                    CalculateDiscountAmtPer(-1);
                    CalculateAmount(-1);
                    lblTransactionType.BackColor = General.ControlFocusColor;
                    cbTransactionType.Focus();
                }
                else
                {
                    lblFooterMessage.Text = string.Concat("Max Discount Percent is ", General.CurrentSetting.MsetSaleMaxDiscount.ToString());
                    txtDiscPercent.Text = "0.00";
                    txtOperator.Focus();
                }
            }
        }
        private void CalculateDiscountAmtPer(int deletedrowindex)
        {
            double mrate = 0;
            double mamt = 0;
            double mpakn = 0;
            double mqty = 0;
            string ifdiscount = "Y";
            double mdiscamt = 0;
            double mdiscper = 0;
            double mvatper = 0;
            double totamtfordiscount = 0;
            double mvatamountfordiscount = 0;
            if (txtDiscAmount.Text != null && txtDiscAmount.Text != string.Empty)
                mdiscamt = Convert.ToDouble(txtDiscAmount.Text.ToString());
            if (mdiscamt > 0)
            {
                foreach (DataGridViewRow dr in ActiveDataGrid.Rows)
                {
                    if (dr.Index != deletedrowindex)
                    {
                        if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "0" && dr.Cells["Col_Quantity"].Value.ToString() != "")
                        {
                            ifdiscount = "Y";
                            mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                            mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                            mpakn = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());
                            mamt = 0;
                            if (dr.Cells["Col_IfSaleDisc"].Value != null && dr.Cells["Col_IfSaleDisc"].Value.ToString() != "")
                                ifdiscount = dr.Cells["Col_IFSaleDisc"].Value.ToString().ToUpper();
                            if (ifdiscount == "Y")
                            {
                                mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                                mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                                mpakn = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());
                                mvatper = Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString());

                                if (Math.Truncate(mqty / mpakn) == (mqty / mpakn))
                                    mamt = Math.Round((mqty / mpakn) * mrate, 2);
                                else
                                {
                                    mamt = Math.Round(Math.Round(mrate / mpakn, 4) * mqty, 2);
                                }
                                mvatamountfordiscount = Math.Round((mamt * mvatper) / (100 + mvatper), 2);
                                totamtfordiscount += (mamt - mvatamountfordiscount);
                            }
                        }
                    }
                }

                if (totamtfordiscount <= mdiscamt)
                {
                    mdiscamt = 0;
                }
                mdiscper = Math.Round((mdiscamt * 100) / totamtfordiscount, 2);
                txtDiscPercent.Text = mdiscper.ToString("#0.00");
            }
            else
            {
                txtDiscPercent.Text = "0.00";
                mdiscper = 0;
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
            lblFooterMessage.Text = "";
            try
            {
                _SSSale.PatientID = "";
                if (txtPatientName.SelectedID != null && txtPatientName.SelectedID != "")
                {
                    _SSSale.PatientID = txtPatientName.SelectedID.ToString();
                    //amar
                    _SSSale.PatientID = txtPatientName.SelectedID;
                    DataRow dr = _SSSale.GetDetailsByPatientID(txtPatientName.SelectedID);
                    if (string.IsNullOrEmpty(Convert.ToString(dr["PendingAmount"])) == false)
                    {
                        _SSSale.PendingAmount = Convert.ToDouble(dr["PendingAmount"]);
                        txtPendingBalance.Text = dr["PendingAmount"].ToString();
                    }
                    else
                    {
                        _SSSale.PendingAmount = 0;
                        txtPendingBalance.Text = "0.00";
                    }
                }
                else
                {
                    _SSSale.PendingAmount = 0;
                    txtPendingBalance.Text = "0.00";
                }
                   
              
                //end
                SetpatientDeatils();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void SetpatientDeatils()
        {
            ClearCreditDebitNote();
            CalculateAmount(-1);
            FillCreditDebitNote();
            FillTransactionTypeForTab();
            FillTransactionType(); //Amar
            if (txtPatientName.SelectedID != null && txtPatientName.SelectedID != "" && txtPatientName.Text.ToString() != txtPatientName.SeletedItem.ItemData[2])
            {
                txtPatientName.SelectedID = "";
                txtAddress.Focus();
            }
            else
            {
                if (txtPatientName.SelectedID == null || txtPatientName.SelectedID == "")
                {
                    txtAddress.ReadOnly = false;
                    txtAddress.Focus();
                }
                else
                {

                    txtAddress.Text = txtPatientName.SeletedItem.ItemData[3];
                    txtMobileNumber.Text = txtPatientName.SeletedItem.ItemData[9];
                    txtTelephoneNumber.Text = _SSSale.GetPatientTelephone(txtPatientName.SelectedID);
                    _SSSale.PutInBlackList = _SSSale.GetPutInBlackList(txtPatientName.SelectedID);
                    mcbDoctor.SelectedID = txtPatientName.SeletedItem.ItemData[6];
                    _SSSale.AccCode = txtPatientName.SeletedItem.ItemData[1];
                    if (_SSSale.AccCode == FixAccounts.SubTypeForPatientSale)
                    {
                        FillTransactionTypeForTab();
                        cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                    }
                    if (txtPatientName.SeletedItem.ItemData[8] != null && txtPatientName.SeletedItem.ItemData[8] != "")
                        _SSSale.CrdbDiscPer = Convert.ToDouble(txtPatientName.SeletedItem.ItemData[8]);
                    txtPatientName.ReadOnly = true;
                    txtAddress.ReadOnly = true;
                    txtDiscPercent.Text = _SSSale.CrdbDiscPer.ToString("#0.00");

                    txtPendingBalance.Text = Math.Abs(_SSSale.PendingAmount).ToString("#0.00"); //amar

                    mcbDoctor.ReadOnly = false;
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
                    if (_SSSale.PutInBlackList == "Y")
                        btnPutInBlackList.Visible = true;
                    else
                        btnPutInBlackList.Visible = false;
                    if (mcbDoctor.SelectedID == null || mcbDoctor.SelectedID == string.Empty)
                        mcbDoctor.Focus();
                    else
                        txtNarration.Focus();
                }
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
            uclSubstituteControl1.Select();
            uclSubstituteControl1.Focus();
        }

        #region NextVisit
        private void btnNextVisit_Click(object sender, EventArgs e)
        {
            btnNextVisit_Click();
        }

        private void btnNextVisit_Click()
        {
            if (pnlVisitDetails.Visible)
            {
                dgvNextVisitDetail.Rows.Clear();
                pnlVisitDetails.SendToBack();
                pnlVisitDetails.Visible = false;

                if (pnlFinal.Visible)
                    txtPatientName.Focus();
                else
                    ActiveGrid.SetFocus(1);
            }
            else
            {
                FillReportGrid();
                pnlVisitDetails.Visible = true;
                pnlVisitDetails.BringToFront();
                pnlVisitDetails.Focus();
                dgvNextVisitDetail.Select();
            }

        }

        private void FillReportGrid()
        {
            try
            {
                _BindingSource = new DataTable();
                GetNextVisitData();
                FillNextVisitData();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void GetNextVisitData()
        {
            try
            {
                SaleList _SaleList = new BusinessLayer.SaleList();
                _BindingSource = new DataTable();
                _BindingSource = _SaleList.GetNextVisitDays(DateTime.Now.Date.ToString("yyyyMMdd"), DateTime.Now.Date.AddDays(2).ToString("yyyyMMdd"));
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillNextVisitData()
        {
            try
            {
                dgvNextVisitDetail.Rows.Clear();
                int rowindex = 0;
                foreach (DataRow item in _BindingSource.Rows)
                {
                    rowindex = dgvNextVisitDetail.Rows.Add();
                    DataGridViewRow row = dgvNextVisitDetail.Rows[rowindex];
                    row.Cells["Col_ID"].Value = item["ID"];
                    row.Cells["Col_PatientName"].Value = item["AccName"];
                    row.Cells["Col_PatientAddress"].Value = item["AccAddress1"];
                    row.Cells["Col_MobileNumber"].Value = item["MobileNumberForSMS"];
                    row.Cells["Col_NextDate"].Value = General.GetDateInDateFormat(item["NextVisitDate"].ToString());
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion NextVisit

        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbDoctor.SelectedID;
                FillDoctorCombo();
                mcbDoctor.SelectedID = selectedId;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {
            CalculateAmount(-1);
            cbTransactionType.Focus(); // [ansuman]11.11.2016
            cbRound.BackColor = Color.PapayaWhip; // [ansuman]11.11.2016
            //pnlTransactCbBorder.BackColor = Color.Green;
        }

        private void txtMobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (!System.Text.RegularExpressions.Regex.IsMatch(txtMobileNumber.Text, "^[0-9]+$")) // [ansuman] [16.11.2016]
                //{
                //    txtMobileNumber.Focus();
                //    txtMobileNumber.ForeColor = Color.DarkRed;
                //    return;
                //}
                if (string.IsNullOrEmpty(txtPatientName.Text.ToString()) == true)
                {
                    if (txtMobileNumber.Text != null && txtMobileNumber.Text != string.Empty)
                        _SSSale.MobileNumberForSMS = txtMobileNumber.Text.ToString();
                    if (_SSSale.MobileNumberForSMS != string.Empty)
                    {
                        DataRow dr = null;
                        dr = _SSSale.GetPatientDataByMobileNumber();
                        if (dr != null)
                        {
                            string selectedId = dr["PatientID"].ToString();
                            txtPatientName.SelectedID = selectedId;
                            SetpatientDeatils();
                        }
                        else
                        {
                            txtPatientName.SelectedID = null;
                            txtAddress.Text = "";
                            txtPatientName.Text = "";
                            mcbDoctor.SelectedID = "";
                            txtDoctorAddress.Text = "";
                        }
                    }
                }
                else if (txtTelephoneNumber.Visible == true)
                    txtTelephoneNumber.Focus();
                else
                    mcbDoctor.Focus();
                txtMobileNumber.ForeColor = Color.Black;   // [ansuman] [16.11.2016]

            }
            else if (e.KeyCode == Keys.Up)
            {
                if (txtPatientName.SelectedID != null && txtPatientName.SelectedID.ToString() != string.Empty && txtPatientName.Enabled == true)
                    txtPatientName.Focus();
                else
                {
                    if (txtAddress.Enabled == true)
                        txtAddress.Focus();
                    else
                        btnClearPatient.Focus();
                }
            }
        }
        private void txtTelephoneNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (!System.Text.RegularExpressions.Regex.IsMatch(txtTelephoneNumber.Text, "^[0-9]+$")) // [ansuman] [16.11.2016]
                //{
                //    txtTelephoneNumber.Focus();
                //    txtTelephoneNumber.ForeColor = Color.DarkRed;
                //    return;
                //}
                mcbDoctor.Focus();
                txtTelephoneNumber.ForeColor = Color.Black;  // [ansuman] [16.11.2016]
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtMobileNumber.Focus();
            }
        }

        private void mcbBankAccount_EnterKeyPressed(object sender, EventArgs e)
        {
            MainToolStrip.Select();
            tsBtnSave.Select();
        }

        #endregion

        #region Construct columns

        private void ConstructMainColumnsmpPVC(DataGridView activeDataGrid)
        {
            DataGridViewTextBoxColumn column;
            activeDataGrid.Columns.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 220;
                column.ReadOnly = false;
                activeDataGrid.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                activeDataGrid.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                activeDataGrid.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 50;
                column.ReadOnly = true;
                activeDataGrid.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.ReadOnly = true;
                activeDataGrid.Columns.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                activeDataGrid.Columns.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.ReadOnly = true;
                activeDataGrid.Columns.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 70;
                column.ReadOnly = true;
                activeDataGrid.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                activeDataGrid.Columns.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                column.ValueType = typeof(int);
                activeDataGrid.Columns.Add(column);
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
                activeDataGrid.Columns.Add(column);
                //13
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //14
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //20
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "IfSaleDisc";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //21
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //22
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CustID";
                //  column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //23
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "LastStockID";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //24
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";
                column.DataPropertyName = "ProfitPercentBySaleRate";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //25
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";
                column.DataPropertyName = "ProfitPercentByPurchaseRate";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //26
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRupees";
                column.DataPropertyName = "ProfitInRupees";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //27
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //28
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //29
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MySpecialDiscountAmount";
                column.DataPropertyName = "MySpecialDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //30
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFTempSale";
                //  column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);
                //31
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFMultipleMRP";
                //  column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }

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
                column.Width = 250;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.ReadOnly = false;
                activeDataGrid.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                activeDataGrid.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                activeDataGrid.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 110;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 80;
                //  column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = false;
                activeDataGrid.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 150;
                //       column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.ReadOnly = true;
                activeDataGrid.ColumnsMain.Add(column);
                //13
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
                column.HeaderText = "TradeRate";
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
                ///21
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //22
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CustID";
                //  column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //23
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "LastStockID";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //24
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";
                column.DataPropertyName = "ProfitPercentBySaleRate";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //25
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";
                column.DataPropertyName = "ProfitPercentByPurchaseRate";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //26
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRupees";
                column.DataPropertyName = "ProfitInRupees";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //27
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //28
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //29
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MySpecialDiscountAmount";
                column.DataPropertyName = "MySpecialDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //30
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFTempSale";
                column.Width = 40;
                column.Visible = false;
                activeDataGrid.ColumnsMain.Add(column);
                //31
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFMultipleMRP";
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
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 40;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsProductList.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                activeDataGrid.ColumnsProductList.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PackType";
                column.DataPropertyName = "ProdPackType";
                column.HeaderText = "Type";
                column.Width = 60;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                activeDataGrid.ColumnsProductList.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Com";
                column.Width = 40;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
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
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.Visible = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Cl.Stk";
                column.Width = 55;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "ProdMRP";
                column.HeaderText = "MRP";
                column.Width = 70;
                column.Visible = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                column.DefaultCellStyle.Format = "N2";
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
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.HeaderText = "IfH1";
                column.Width = 40;
                column.Visible = false;
                column.ReadOnly = true;
                activeDataGrid.ColumnsProductList.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "ProdLastPurchaseRate";
                column.HeaderText = "PurRate";
                column.Width = 30;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfShortListed";
                column.DataPropertyName = "ProdIfShortListed";
                column.HeaderText = "Short";
                column.Width = 30;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MaxLevel";
                column.DataPropertyName = "ProdMaxLevel";
                column.HeaderText = "MaxLevel";
                column.Width = 30;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);
                //13
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MinLevel";
                column.DataPropertyName = "ProdMinLevel";
                column.HeaderText = "MinLevel";
                column.Width = 30;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);
                //14
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "ProdLastSaleStockID";
                column.Width = 60;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "ProdLastSaleStockID";
                column.HeaderText = "laststockid";
                column.Width = 30;
                column.Visible = false;
                activeDataGrid.ColumnsProductList.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.HeaderText = "Sch";
                column.Width = 30;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Visible = true;
                activeDataGrid.ColumnsProductList.Add(column);
                //17
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
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPercent";
                column.DataPropertyName = "ProductVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.Visible = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsBatchList.Add(column);

                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Cl.STK";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
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

                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 70;
                column.DefaultCellStyle.Format = "N2";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistSaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 70;
                column.Visible = false;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "PurRate";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                activeDataGrid.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseDate";
                column.DataPropertyName = "LastPurchaseDate";
                column.HeaderText = "LastPurOn";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 85;
                activeDataGrid.ColumnsBatchList.Add(column);
                //5

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyShortName";
                column.DataPropertyName = "AccShortName";
                column.HeaderText = "";
                column.Width = 45;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                column.HeaderText = "TradeRate";
                column.Width = 70;
                column.Visible = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
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

        private void ConstructNextVisitColumns()
        {
            DataGridViewTextBoxColumn column;
            dgvNextVisitDetail.Columns.Clear();

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "ID";
            column.HeaderText = "ID";
            column.Width = 0;
            column.Visible = false;
            dgvNextVisitDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_PatientName";
            column.DataPropertyName = "PatientName";
            column.HeaderText = "PatientName";
            column.ReadOnly = true;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvNextVisitDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_PatientAddress";
            column.DataPropertyName = "PatientAddress";
            column.HeaderText = "Address";
            column.ReadOnly = true;
            column.Width = 300;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvNextVisitDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_MobileNumber";
            column.DataPropertyName = "MobileNumber";
            column.HeaderText = "MobileNumber";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            dgvNextVisitDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_NextDate";
            column.DataPropertyName = "NextDate";
            column.HeaderText = "NextDate";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            dgvNextVisitDetail.Columns.Add(column);
        }

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
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CustID";
                //  column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "LastStockID";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";
                column.DataPropertyName = "ProfitPercentBySaleRate";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";
                column.DataPropertyName = "ProfitPercentByPurchaseRate";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRupees";
                column.DataPropertyName = "ProfitInRupees";
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
                column.Name = "Col_MySpecialDiscountAmount";
                column.DataPropertyName = "MySpecialDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFTempSale";
                //  column.DataPropertyName = "ItemDiscountAmount";
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

        private void ConstructCreditNoteColumns()
        {
            dgCreditNote.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CrdbID";
                column.DataPropertyName = "CRDBID";
                column.HeaderText = "VouSeries";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgCreditNote.Columns.Add(column);

                //DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                //columnCheck.Name = "Col_Check";
                //columnCheck.HeaderText = "Check";
                //columnCheck.Width = 50;
                //columnCheck.Visible = true;
                //dgCreditNote.ColumnsMain.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = " ";
                column.Width = 15;
                dgCreditNote.Columns.Add(column);

                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "Series";
                column.Width = 50;
                column.ReadOnly = true;
                dgCreditNote.Columns.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.ReadOnly = true;
                dgCreditNote.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.ReadOnly = true;
                column.Width = 50;
                dgCreditNote.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 80;
                column.ReadOnly = true;
                dgCreditNote.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 80;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCreditNote.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Narr";
                column.DataPropertyName = "Narration";
                column.HeaderText = "Narration";
                column.Width = 160;
                column.ReadOnly = true;
                dgCreditNote.Columns.Add(column);
                //8

                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountClear";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "AmountClear";
                column.Visible = false;
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCreditNote.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Acc";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "a1";
                column.Width = 50;
                column.Visible = false;
                dgCreditNote.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private bool BindCreditNoteDebitNote(DataTable dt)
        {
            bool retValue = true;
            double amt = 0;
            try
            {

                if (dgCreditNote != null)
                    dgCreditNote.Rows.Clear();
                int _RowIndex = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    _RowIndex = dgCreditNote.Rows.Add();
                    string voudt = "";
                    double amtclear = 0;
                    DataGridViewRow currentdr = dgCreditNote.Rows[_RowIndex];
                    currentdr.Cells["Col_CrdbID"].Value = dr["CRDBID"].ToString();
                    currentdr.Cells["Col_VoucherSeries"].Value = dr["VoucherSeries"].ToString();
                    currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    {
                        voudt = dr["VoucherDate"].ToString();
                        voudt = General.GetDateInShortDateFormat(voudt);
                    }
                    currentdr.Cells["Col_VoucherDate"].Value = voudt;
                    amt = Convert.ToDouble(dr["AmountNet"].ToString());
                    currentdr.Cells["Col_AmountNet"].Value = amt.ToString("#0.00");
                    currentdr.Cells["Col_Narr"].Value = dr["Narration"].ToString();
                    if (dr["AmountClear"] != DBNull.Value && dr["AmountClear"].ToString() != "")
                        double.TryParse(dr["AmountClear"].ToString(), out amtclear);
                    currentdr.Cells["Col_AmountClear"].Value = dr["AmountClear"].ToString();
                    if (_Mode == OperationMode.Delete)
                        currentdr.Cells["Col_Check"].Value = string.Empty;
                    else if (amtclear != 0)
                        currentdr.Cells["Col_Check"].Value = ((char)0x221A).ToString();
                    else
                        currentdr.Cells["Col_Check"].Value = string.Empty;

                    _RowIndex += 1;

                }

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;
            }
            return retValue;
        }
        private void dgCreditNote_KeyDown(object sender, KeyEventArgs e)
        {
            string ifchecked = string.Empty;
            if (Mode == OperationMode.Add || Mode == OperationMode.Edit)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(dgCreditNote.CurrentRow.Cells["Col_Check"].Value)) == false)
                        ifchecked = dgCreditNote.CurrentRow.Cells["Col_Check"].Value.ToString();
                    if (ifchecked != string.Empty)
                        dgCreditNote.CurrentRow.Cells["Col_Check"].Value = string.Empty;
                    else if (string.IsNullOrEmpty(Convert.ToString(dgCreditNote.CurrentRow.Cells[0].Value)) == false)
                        dgCreditNote.CurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();

                    //btnCRDBOKClick();
                    CalculateCRDBSelectedAmount();
                    if (string.IsNullOrEmpty(Convert.ToString(dgCreditNote.CurrentRow.Cells[0].Value)) == true)
                    {
                        btnCRDBOK.Focus();
                        btnCRDBOK.BackColor = Color.Aqua;
                    }
                }
            }
        }
        private void CalculateCRDBSelectedAmount()
        {
            string mvoutype = "";
            double mcrnoteamt = 0;
            double mdbnoteamt = 0;
            try
            {
                foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
                {
                    string ch = string.Empty;
                    double mamt = 0;
                    if (crdbdr.Cells["Col_Check"].Value != null && crdbdr.Cells["Col_Check"].Value.ToString() != string.Empty)
                        ch = crdbdr.Cells["Col_Check"].Value.ToString();
                    if (ch != string.Empty)
                    {
                        mvoutype = crdbdr.Cells["Col_VoucherType"].Value.ToString().Trim();
                        double.TryParse(crdbdr.Cells["Col_AmountNet"].Value.ToString().Trim(), out mamt);
                        if (mvoutype == FixAccounts.VoucherTypeForCreditNoteStock || mvoutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                            mcrnoteamt += mamt;
                        else if (mvoutype == FixAccounts.VoucherTypeForDebitNoteStock || mvoutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                            mdbnoteamt += mamt;
                    }
                }
                txtCRAmountSelected.Text = mcrnoteamt.ToString("#0.00");
                txtDNAmountSelected.Text = mdbnoteamt.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void btnCRDBOK_Click(object sender, EventArgs e)
        {
            btnCRDBOKClick();
        }
        private void btnCRDBOKClick()
        {
            double mcrnoteamt = 0;
            double mdbnoteamt = 0;
            string mvoutype = "";
            lblFooterMessage.Text = "";

            try
            {
                foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
                {
                    string ch = string.Empty;
                    double mamt = 0;
                    if (crdbdr.Cells["Col_Check"].Value != null && crdbdr.Cells["Col_Check"].Value.ToString() != string.Empty)
                        ch = crdbdr.Cells["Col_Check"].Value.ToString();
                    if (ch != string.Empty)
                    {
                        mvoutype = crdbdr.Cells["Col_VoucherType"].Value.ToString().Trim();
                        double.TryParse(crdbdr.Cells["Col_AmountNet"].Value.ToString().Trim(), out mamt);
                        if (mvoutype == FixAccounts.VoucherTypeForCreditNoteStock || mvoutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                            mcrnoteamt += mamt;
                        else if (mvoutype == FixAccounts.VoucherTypeForDebitNoteStock || mvoutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                            mdbnoteamt += mamt;
                    }
                }
                txtCreditNote.Text = mcrnoteamt.ToString("#0.00");
                txtDebitNote.Text = mdbnoteamt.ToString("#0.00");
                pnlDebitCreditNote.Visible = false;
                CalculateAmount(-1);
                // CalculateFinalSummary();
                tsBtnSave.Enabled = true;
                btnCRDBOK.BackColor = Color.Honeydew;
                txtDiscPercent.Focus();
                txtDiscPercent.SelectAll();
                //pnlSummary.BringToFront();
                //pnlSummary.Visible = true;
                //pnlSummary.Focus();
                //txtCRAmountS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCRDBOK_Click>>" + Ex.Message);
            }
        }
        //private void ConstructCreditNoteColumns()
        //{
        //    dgCreditNote.ColumnsMain.Clear();
        //    DataGridViewTextBoxColumn column;
        //    //0
        //    try
        //    {
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_CrdbID";
        //        column.DataPropertyName = "CRDBID";
        //        column.HeaderText = "VouSeries";
        //        column.Width = 50;
        //        column.Visible = false;
        //        column.ReadOnly = true;
        //        dgCreditNote.ColumnsMain.Add(column);

        //        DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
        //        columnCheck.Name = "Col_Check";
        //        columnCheck.HeaderText = "Check";
        //        columnCheck.Width = 50;
        //        columnCheck.Visible = true;
        //        dgCreditNote.ColumnsMain.Add(columnCheck);

        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Selected";
        //        column.HeaderText = " ";
        //        column.Width = 1;
        //        dgCreditNote.ColumnsMain.Add(column);

        //        //1
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherSeries";
        //        column.DataPropertyName = "VoucherSeries";
        //        column.HeaderText = "VouSeries";
        //        column.Width = 50;
        //        column.ReadOnly = true;
        //        dgCreditNote.ColumnsMain.Add(column);

        //        //2
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherType";
        //        column.DataPropertyName = "VoucherType";
        //        column.HeaderText = "VouType";
        //        column.Width = 50;
        //        column.ReadOnly = true;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //3
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherNumber";
        //        column.DataPropertyName = "VoucherNumber";
        //        column.HeaderText = "VouNumber";
        //        column.ReadOnly = true;
        //        column.Width = 50;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //4
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherDate";
        //        column.DataPropertyName = "VoucherDate";
        //        column.HeaderText = "VoucherDate";
        //        column.Width = 80;
        //        column.ReadOnly = true;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //5
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_AmountNet";
        //        column.DataPropertyName = "AmountNet";
        //        column.HeaderText = "AmountNet";
        //        column.Width = 80;
        //        column.ReadOnly = true;
        //        column.DefaultCellStyle.Format = "N2";
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //10
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Narr";
        //        column.DataPropertyName = "Narration";
        //        column.HeaderText = "Narration";
        //        column.Width = 160;
        //      //  column.ReadOnly = true;
        //        dgCreditNote.ColumnsMain.Add(column);

        //        //6
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_AmountClear";
        //        column.HeaderText = "AmountBalance";
        //        column.Visible = false;
        //        column.Width = 80;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //7
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_AmountClear";
        //        column.DataPropertyName = "AmountClear";
        //        column.HeaderText = "AmountClear";
        //        column.Visible = false;
        //        column.Width = 80;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //9
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Acc";
        //        column.DataPropertyName = "AccountID";
        //        column.HeaderText = "a1";
        //        column.Width = 50;
        //        column.Visible = false;
        //        dgCreditNote.ColumnsMain.Add(column);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        #endregion

        #region UIEvents

        private void txtAddress_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtPatientName.Focus();
        }

        private void mpPVC_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            Exit();
        }
        //private void dgCreditDebitNoteEscapeKeyPressed()
        //{
        //    txtCreditNote.Text = "0.00";
        //    txtDebitNote.Text = "0.00";
        //    pnlDebitCreditNote.Visible = false;
        //    CalculateAmount(-1);
        //    if (txtDiscPercent.Visible)
        //        txtDiscPercent.Focus();
        //}
        private void mcbDoctor_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtMobileNumber.Focus();
        }
        private void datePickerBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                datePickerBillDateValidating();
            }
        }
        private void datePickerBillDate_ValueChanged(object sender, EventArgs e)
        {
            SetDateStatus();
        }
        private void SetDateStatus()
        {
            timer.Interval = 1000;

            DateTime _MDate = datePickerBillDate.Value.Date;
            DateTime _CDate = DateTime.Now.Date;
            int result = DateTime.Compare(_MDate, _CDate);
            if (result < 0)
            {
                lblmsg.Text = "You are working in Previous date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                timer.Enabled = true;
                timer.Start();
            }
            else if (result == 0)
            {
                lblmsg.Text = ""; // "You are working in Current date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                timer.Enabled = false;
                timer.Stop();
            }
            else
            {
                lblmsg.Text = "You are working in Next date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                timer.Enabled = true;
                timer.Start();
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (lblmsg.BackColor == Color.Gainsboro)
                lblmsg.BackColor = Color.Red;
            else
                lblmsg.BackColor = Color.Gainsboro;
        }

        private void btnClearDoctor_Click(object sender, EventArgs e)
        {
            BtnClearDoctorClick();

        }

        private void BtnClearDoctorClick()
        {
            mcbDoctor.SelectedID = "";
            mcbDoctor.Text = "";
            txtDoctorAddress.Text = "";
            mcbDoctor.ReadOnly = false;
            mcbDoctor.Focus();
        }
        private void btnClearPatient_Click(object sender, EventArgs e)
        {
            BtnClearPatientClick();
        }

        private void BtnClearPatientClick()
        {
            txtPatientName.Text = "";
            txtPatientName.SelectedID = "";
            txtAddress.Text = "";
            txtMobileNumber.Text = "";
            txtTelephoneNumber.Text = "";
            txtPendingBalance.Text = "0.00";
            txtPatientName.ReadOnly = false;
            this.txtPatientName.Focus();
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMobileNumber.Focus();
            else if (e.KeyCode == Keys.Up)
                txtPatientName.Focus();
        }
        private bool datePickerBillDateValidating()
        {
            bool retValue = false;
            string _MDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
            string _CDate = DateTime.Now.Date.ToString("yyyyMMdd");
            if (General.CurrentSetting.MsetSaleAllowBackDate != "Y")
            {
                if (Convert.ToInt32(_MDate) >= Convert.ToInt32(_CDate))
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
            else
            {
                retValue = General.CheckDates(_MDate, _MDate);
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
            return retValue;
        }
        private void txtDoctorAddress_UpArrowKeyPressed(object sender, EventArgs e)
        {
            if (mcbDoctor.Enabled == true)
                mcbDoctor.Focus();
            else
                btnClearDoctor.Focus();
        }
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btnIfDebitCredit.Visible)
                {
                    btnIfDebitCredit.Focus();
                    btnIfDebitCredit.BackColor = Color.Aqua;
                }
                else if (txtDiscPercent.Visible == true)
                    txtDiscPercent.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (txtDoctorAddress.Enabled == true) txtDoctorAddress.Focus();
                else btnClearDoctor.Focus();
            }
        }
        private void txtDoctorAddress_EnterKeyPressed(object sender, EventArgs e)
        {
            if (txtDiscPercent.Visible)
                txtNarration.Focus();
            else if (txtOperator.Visible)
                txtOperator.Focus();
            else
            {
                MainToolStrip.Select();
                tsBtnSave.Select();
            }
        }
        private void cbTransactionType_Enter(object sender, EventArgs e)
        {
            cbTransactionType.BackColor = General.ControlFocusColor;
        }
        private void cbEditRate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEditRate.Checked)
                ActiveDataGrid.ColumnsMain["Col_SaleRate"].ReadOnly = false;
            else
                ActiveDataGrid.ColumnsMain["Col_SaleRate"].ReadOnly = true;
            ActiveDataGrid.SetFocus(ActiveDataGrid.Rows.Count - 1, 1);
        }

        private void ActiveDataGridOnNewBatchClicked()
        {
            try
            {
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = false;
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = false;
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_MRP"].ReadOnly = false;
                ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                ActiveDataGrid.SetFocus(6); //focus to Batch - 1
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ActiveGrid_OnBatchSelected(DataGridViewRow batchRow)
        {
            ActiveDataGridOnBatchSelected(batchRow);
        }

        private void ActiveGrid_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ActiveDataGridOnCellEntered(e);
                if (General.CurrentSetting.MsetSaleShowProfitInSaleBill == "Y" && ActiveDataGrid.Rows.Count > 0 && e.ColumnIndex == 1
                    && string.IsNullOrEmpty(Convert.ToString(ActiveDataGrid.Rows[e.RowIndex].Cells[0].Value)) == false)
                {
                    txtProfit.Visible = lblProfit.Visible = true;
                    txtProfit.Text = Convert.ToDouble(ActiveDataGrid.Rows[e.RowIndex].Cells["Col_ProfitInRupees"].Value).ToString("#0.00");
                }
                else
                    txtProfit.Visible = lblProfit.Visible = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ActiveGrid_OnCellValueChangeCommited(int colIndex)
        {
            ActiveDataGridCellValueChangeCommited(colIndex);
        }

        private void ActiveGrid_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            Exit();
        }

        private void ActiveGrid_OnNewBatchClicked()
        {
            ActiveDataGridOnNewBatchClicked();
        }

        private void ActiveGrid_OnProductSelected(DataGridViewRow productRow)
        {
            ActiveDataGridOnProductSelected(productRow);
        }

        private void ActiveGrid_OnRowAdded(object sender, EventArgs e)
        {
            ActiveDataGridOnRowAdded(e);
        }

        private bool ActiveGrid_OnCanRowDeleted(object sender, EventArgs e)
        {
            bool retValue = true;
            //DialogResult dResult = MessageBox.Show("Delete Row ?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //if (dResult == DialogResult.Yes)
            //{
            //    retValue = true;
            //}
            //else
            //{
            //    retValue = false;
            //}
            return retValue;
        }

        private void ActiveGrid_OnRowDeleted(object sender, EventArgs e)
        {
            ActiveDataGridOnRowDeleted(sender);
        }

        private void ActiveGrid_OnShiftTABKeyPressed(object sender, EventArgs e)
        {

        }

        private void ActiveGrid_OnTABKeyPressed(object sender, EventArgs e)
        {
            ActiveDataGrid.ClearSelection();
            ActiveDataGridTabKeyPressed();
        }
        private void ActiveGrid_OnSelectedProductClosingStock(int closingStockValue, string productID)
        {
            int mqty = 0;
            if (_Mode == OperationMode.Add)
            {
                if (ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    int.TryParse(ActiveDataGrid.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);
                if (closingStockValue + mqty == 0)
                {
                    _SSSale.ProductID = productID;

                    if (_SSSale.IfAddToShortList())
                    {
                        Filldailyshortlist();
                    }
                    lblFooterMessage.Text = "No Stock";
                    ActiveDataGrid.SetFocus(ActiveDataGrid.MainDataGridCurrentRow.Index, 1);
                }
                else
                {
                    lblFooterMessage.Text = string.Format("Stock: {0}", closingStockValue + mqty);
                }
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefreshClick();
        }

        private void btnRefreshClick()
        {
            DataTable dtable = new DataTable();
            Product prod = new Product();
            dtable = prod.GetOverviewData();

            ActiveGrid.DataSourceProductList = dtable;
            ActiveGrid.BindGridProductList();
        }

        private void FillCreditDebitNote()
        {
            bool retValue = false;
            DataTable dt = new DataTable();
            string patid = "";

            if (_SSSale.PatientID == "")
                patid = FixAccounts.AccountCounterSaleCreditNote;
            else
                patid = _SSSale.PatientID;

            try
            {
                ConstructCreditNoteColumns();
                //  dgCreditNote.DoubleColumnNames.Add("Col_AmountNet");
                CreditNoteStock crdb = new CreditNoteStock();
                dt = crdb.GetOverviewDataForDebtorSale(patid, _SSSale.Id);
                if (dt != null)
                    retValue = BindCreditNoteDebitNote(dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    btnIfDebitCredit.Visible = true;
                    lblCreditNote.Visible = true;
                    lblDebitNote.Visible = true;
                    txtCreditNote.Visible = true;
                    txtDebitNote.Visible = true;
                }
                else
                {
                    btnIfDebitCredit.Visible = false;
                    lblCreditNote.Visible = false;
                    lblDebitNote.Visible = false;
                    txtCreditNote.Visible = false;
                    txtDebitNote.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private Point GetpnlDebitCreditNoteLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = ActiveGrid.Location.X + 80;
                pt.Y = ActiveGrid.Location.Y + -100;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        //private bool BindCreditNoteDebitNote(DataTable dt)
        //{
        //    bool retValue = true;
        //    try
        //    {

        //        if (dgCreditNote != null && dgCreditNote.Rows.Count > 0)
        //            dgCreditNote.Rows.Clear();
        //        int _RowIndex = 0;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            _RowIndex = dgCreditNote.Rows.Add();
        //            string voudt = "";
        //            double amtclear = 0;
        //            DataGridViewRow currentdr = dgCreditNote.Rows[_RowIndex];
        //            currentdr.Cells["Col_Check"].Value = false;
        //            currentdr.Cells["Col_CrdbID"].Value = dr["CRDBID"].ToString();
        //            currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
        //            currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
        //            if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
        //            {
        //                voudt = dr["VoucherDate"].ToString();
        //                voudt = General.GetDateInShortDateFormat(voudt);
        //            }
        //            currentdr.Cells["Col_VoucherDate"].Value = voudt;
        //            currentdr.Cells["Col_AmountNet"].Value = dr["AmountNet"].ToString();
        //            currentdr.Cells["Col_Narr"].Value = dr["Narration"].ToString();
        //            if (dr["AmountClear"] != DBNull.Value && dr["AmountClear"].ToString() != "")
        //                double.TryParse(dr["AmountClear"].ToString(), out amtclear);
        //            currentdr.Cells["Col_AmountClear"].Value = dr["AmountClear"].ToString();
        //            //currentdr.Cells["Col_Check"].Value = false;

        //            _RowIndex += 1;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteError(ex.ToString());
        //        retValue = false;
        //    }
        //    return retValue;
        //}

        //private bool SaveAndUpdateDebitCreditNote()
        //{
        //    {
        //        bool returnVal = false;
        //        try
        //        {
        //            foreach (DataGridViewRow prodrow in dgCreditNote.Rows)
        //            {
        //                if ((prodrow.Cells["Col_CrdbID"].Value) != null && (Convert.ToBoolean(prodrow.Cells["Col_Check"].Value) == true))
        //                {
        //                    _SSSale.CreditDebitNoteID = prodrow.Cells["Col_CrdbID"].Value.ToString();
        //                    _SSSale.Amount = Convert.ToDouble(prodrow.Cells["Col_AmountNet"].Value.ToString());
        //                    returnVal = _SSSale.UpdateCreditDebitNoteAdjustedDetails(_SSSale.CreditDebitNoteID, _SSSale.Amount, _SSSale.CrdbVouType, _SSSale.CrdbVouNo, _SSSale.CrdbVouDate, _SSSale.CrdbVouType, _SSSale.Id, "");
        //                }
        //            }
        //        }
        //        catch { returnVal = false; }
        //        return returnVal;
        //    }
        //}
        //private bool clearPreviousdebitcreditnotes()
        //{
        //    bool retValue = true;
        //    retValue = _SSSale.clearPreviousdebitcreditnotes(_SSSale.Id);
        //    return retValue;
        //}
        private bool SaveAndUpdateDebitCreditNote()
        {
            {
                bool returnVal = false;
                try
                {
                    foreach (DataGridViewRow prodrow in dgCreditNote.Rows)
                    {
                        if ((prodrow.Cells["Col_CrdbID"].Value) != null && (prodrow.Cells["Col_Check"].Value.ToString() != string.Empty))
                        {
                            _SSSale.CreditDebitNoteID = prodrow.Cells["Col_CrdbID"].Value.ToString();
                            _SSSale.Amount = Convert.ToDouble(prodrow.Cells["Col_AmountNet"].Value.ToString());
                            returnVal = _SSSale.UpdateCreditDebitNoteAdjustedDetails(_SSSale.CreditDebitNoteID, _SSSale.Amount, _SSSale.CrdbVouType, _SSSale.CrdbVouNo, _SSSale.CrdbVouDate, _SSSale.CrdbVouType, _SSSale.Id, "");
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
        }
        private bool clearPreviousdebitcreditnotes()
        {
            bool retValue = false;
            retValue = _SSSale.clearPreviousdebitcreditnotes(_SSSale.Id);
            return retValue;
        }
        private void ClearDebitCreditNoteWhenAmountIsLess()
        {
            string mvoutype = "";
            try
            {
                foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
                {
                    string ch = string.Empty;
                    double mamt = 0;
                    if (crdbdr.Cells["Col_Check"].Value != null)
                    {
                        ch = crdbdr.Cells["Col_Check"].Value.ToString();
                        if (ch != string.Empty)
                        {
                            mvoutype = crdbdr.Cells["Col_VoucherType"].Value.ToString().Trim();
                            double.TryParse(crdbdr.Cells["Col_AmountNet"].Value.ToString().Trim(), out mamt);
                            if (mvoutype == FixAccounts.VoucherTypeForCreditNoteStock || mvoutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                                crdbdr.Cells["Col_Check"].Value = string.Empty;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void btnIfDebitCredit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnIfDebitCreditNoteClick();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void btnOKCreditDebitNote_Click(object sender, EventArgs e)
        {
            btnOKCreditDebitNoteClick();
        }
        private void btnOKCreditDebitNoteClick()
        {
            double mcrnoteamt = 0;
            double mdbnoteamt = 0;
            string mvoutype = "";
            lblFooterMessage.Text = "";

            try
            {
                foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
                {
                    bool ch = false;
                    double mamt = 0;
                    ch = Convert.ToBoolean(crdbdr.Cells["Col_Check"].Value);
                    if (ch == true)
                    {
                        mvoutype = crdbdr.Cells["Col_VoucherType"].Value.ToString().Trim();
                        double.TryParse(crdbdr.Cells["Col_AmountNet"].Value.ToString().Trim(), out mamt);
                        if (mvoutype == FixAccounts.VoucherTypeForCreditNoteStock || mvoutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                            mcrnoteamt += mamt;
                        else
                            mdbnoteamt += mamt;
                    }
                }
                txtCreditNote.Text = mcrnoteamt.ToString("#0.00");
                txtDebitNote.Text = mdbnoteamt.ToString("#0.00");
                pnlDebitCreditNote.Visible = false;
                CalculateAmount(-1);
                if (txtDiscPercent.Visible)
                    txtDiscPercent.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCRDBOK_Click>>" + Ex.Message);
            }
        }
        private void BtnIfDebitCreditNoteClick()
        {
            double amt = 0;
            try
            {
                double.TryParse(txtBillAmount2.Text.ToString(), out amt);
                if (amt > 0)
                {
                    //FillCreditDebitNote();
                    pnlDebitCreditNote.Location = GetpnlDebitCreditNoteLocation();
                    pnlDebitCreditNote.Visible = true;
                    dgCreditNote.Visible = true;
                    lblCreditNote.Visible = true;
                    lblDebitNote.Visible = true;
                    lblFooterMessage.Text = "Press Space Bar To Select Credit/Debit Note";
                    txtCreditNote.Visible = true;
                    txtDebitNote.Visible = true;
                    pnlDebitCreditNote.BringToFront();
                    //dgCreditNote.BringToFront();
                    pnlDebitCreditNote.Select();
                    dgCreditNote.Select();
                    dgCreditNote.Focus();
                    btnIfDebitCredit.BackColor = Color.Gainsboro;
                }
                else
                    lblFooterMessage.Text = "First Enter Sale";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void btnIfDebitCredit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnIfDebitCreditNoteClick();
            else
                txtDiscPercent.Focus();
        }

        //private void dgCreditNote_OnEscapeKeyPressed(object sender, EventArgs e)
        //{
        //    dgCreditDebitNoteEscapeKeyPressed();
        //}

        //private void dgCreditNote_OnCellValueChangeCommited(int colIndex)
        //{
        //    bool ifchecked = false;
        //    if (dgCreditNote.MainDataGridCurrentRow.Cells["Col_Check"].Value != null)
        //        ifchecked = Convert.ToBoolean(dgCreditNote.MainDataGridCurrentRow.Cells["Col_Check"].Value);
        //    if (ifchecked == true)
        //        dgCreditNote.MainDataGridCurrentRow.Cells["Col_Check"].Value = false;
        //    else
        //        dgCreditNote.MainDataGridCurrentRow.Cells["Col_Check"].Value = true;
        //}

        //private void dgCreditNote_OnTABKeyPressed(object sender, EventArgs e)
        //{
        //    bool ifchecked = false;
        //    if (dgCreditNote.MainDataGridCurrentRow.Cells["Col_Check"].Value != null)
        //        ifchecked = Convert.ToBoolean(dgCreditNote.MainDataGridCurrentRow.Cells["Col_Check"].Value);
        //    if (ifchecked == true)
        //        dgCreditNote.MainDataGridCurrentRow.Cells["Col_Check"].Value = false;
        //    else
        //        dgCreditNote.MainDataGridCurrentRow.Cells["Col_Check"].Value = true;
        //}

        //private void dgCreditNote_OnShowViewForm(DataGridViewRow selectedRow)
        //{

        //}

        private void cbVATSelected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbVATSelected.Checked == true)
            {
                BindActiveGridProductVATWise();
            }
            else
                BindActiveDataGrid();
        }

        private void BindActiveGridProductVATWise()
        {
            double vatPercent = 0;
            if (txtVATPercentSelected.Text != null && txtVATPercentSelected.Text.ToString() != string.Empty)
            {
                vatPercent = Convert.ToDouble(txtVATPercentSelected.Text.ToString());
            }
            Product prod = new Product();
            ActiveDataGrid.DataSourceProductList = prod.GetOverviewData(vatPercent);
            ActiveDataGrid.BindGridProductList();
            ActiveDataGrid.SetFocus(1);
        }

        private void txtVATPercentSelected_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && cbVATSelected.Checked == true)
            {
                BindActiveGridProductVATWise();
            }

        }

        private void txtPatientName_UpArrowKeyPressed(object sender, EventArgs e)
        {
            pnlFinal.Visible = false;
            lblProfit.Visible = txtProfit.Visible = false;
            EnableAllGrids();

            cbTransactionType.Text = FixAccounts.TransactionTypeForVoucher;
            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForVoucher);

            ActiveDataGrid.SetFocus(1);
        }

        private void btnClearPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                ActiveDataGrid.SetFocus(1);
            }
        }

        private void btnClearDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                btnClearPatient.Focus();
            }
        }

        private void cbTransactionType_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbTransactionType_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool IsSelectedBank = false;
                if (cbTransactionType.Text == null || cbTransactionType.Text == string.Empty)
                {
                    cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                }
                if (cbTransactionType.Text.ToString() == FixAccounts.TransactionTypeForCreditCard)
                {
                    lblBank.Visible = true;
                    mcbBankAccount.Visible = true;
                    mcbBankAccount.Focus();
                    IsSelectedBank = true;
                }
                else
                {
                    lblBank.Visible = false;
                    mcbBankAccount.Visible = false;
                }
                if (IsSetSaveInvoice == true && IsSelectedBank == false)
                {
                    MainToolStrip.Select();
                    tsBtnSave.Select();
                    IsSetSaveInvoice = false;
                    //tsbtnSaveClik();
                }
                else if(IsSelectedBank == false)
                {
                    IsSetSaveInvoice = true;
                    MainToolStrip.Select();
                    tsBtnSave.Select();
                    //pnlTransactCbBorder.BackColor = Color.White;
                }
            }
        }
        private void cbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbTransactionType.SelectedItem != null)
                {
                    if (cbTransactionType.Text.ToString() == FixAccounts.TransactionTypeForCreditCard)
                    {
                        lblBank.Visible = true;
                        mcbBankAccount.Visible = true;
                        mcbBankAccount.Focus();
                    }
                    else
                    {
                        lblBank.Visible = false;
                        mcbBankAccount.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void cbRound_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbTransactionType.Focus(); // [ansuman]11.11.2016
                cbRound.BackColor = Color.PapayaWhip; // [ansuman]11.11.2016
                //pnlTransactCbBorder.BackColor = Color.Green;
            }
        }

        private void UclCounterSale_Enter(object sender, EventArgs e)
        {
            //RefreshProductGrid();
        }

        private void psBtnAttachPrescription_Click(object sender, EventArgs e)
        {
            try
            {
                if (_SSSale.PrescriptionFileName != null && _SSSale.PrescriptionFileName != string.Empty)
                {
                    if (_SSSale.PrePrescriptionFileName.Contains('\\'))
                    {
                        _SSSale.PrescriptionFileName = _SSSale.PrescriptionFileName.Replace("\\", "-");
                    }
                    _SSSale.PrescriptionFileName = _SSSale.PrescriptionFileName.Replace("-", "\\");

                    System.Diagnostics.Process.Start(@_SSSale.PrescriptionFileName);
                    psBtnAttachPrescription.Text = "Show Prescription";
                    psBtnRemovePrescription.Visible = true;
                    psBtnRemovePrescription.Enabled = true;
                    psBtnRemovePrescription.Text = "Remove Prescription";
                }
                else
                {

                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.RestoreDirectory = true;
                    dialog.Title = "Please select a prescription file.";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (dialog.FileName != string.Empty)
                        {
                            try
                            {
                                _SSSale.PrescriptionFileName = dialog.FileName;
                                _SSSale.PrescriptionFileName = _SSSale.PrescriptionFileName.Replace("\\", "-");
                                psBtnAttachPrescription.Text = "Show Prescription";
                                psBtnRemovePrescription.Text = "Remove Prescription";
                                psBtnRemovePrescription.Enabled = true;
                                psBtnRemovePrescription.Visible = true;
                            }
                            catch (IOException ioex)
                            {
                                Log.WriteException(ioex);
                            }
                        }

                    }
                    else
                        psBtnAttachPrescription.Text = "Attach Prescription";
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                MessageBox.Show(ex.ToString(), General.ApplicationTitle);
            }
        }

        private void psBtnRemovePrescription_Click(object sender, EventArgs e)
        {
            try
            {
                if (_SSSale.PrescriptionFileName != null && _SSSale.PrescriptionFileName != string.Empty)
                {
                    _SSSale.PrescriptionFileName = "";
                    psBtnRemovePrescription.Text = "Removed";
                    psBtnRemovePrescription.Enabled = false;
                    psBtnAttachPrescription.Text = "Attach Prescription";
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                MessageBox.Show(ex.ToString(), General.ApplicationTitle);
            }
        }

        // sheela  26/11/2016
        private void btnUpdateNegetiveStock_Click(object sender, EventArgs e)
        {
            _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
            BtnUpdateNegetiveStock();
        }

        private void BtnUpdateNegetiveStock()
        {
            StockIn _StockIn = new StockIn();

            _StockIn.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _StockIn.CrdbVouNo = _StockIn.GetAndUpdateStockInNumber(General.ShopDetail.ShopVoucherSeries);
            DataTable dt = _SSSale.GetNegetiveStockRowsFromtblStock();
            double mtotalamt = 0;
            double VatAmount5 = 0;
            double VatAmount12Point5 = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                bool returnVal = false;
                _StockIn.SerialNumber = 0;
                try
                {
                    foreach (DataRow prodrow in dt.Rows)
                    {

                        _StockIn.SerialNumber += 1;
                        _StockIn.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _StockIn.ProductID = prodrow["ProductID"].ToString();
                        _StockIn.Batchno = prodrow["BatchNumber"].ToString();
                        _StockIn.Quantity = Convert.ToInt32(prodrow["ClosingStock"].ToString()) * -1;
                        _StockIn.PurchaseRate = Convert.ToDouble(prodrow["PurchaseRate"].ToString());
                        _StockIn.MRP = Convert.ToDouble(prodrow["MRP"].ToString());
                        _StockIn.SaleRate = Convert.ToDouble(prodrow["SaleRate"].ToString());
                        _StockIn.TradeRate = Convert.ToDouble(prodrow["TradeRate"].ToString());
                        _StockIn.Expiry = prodrow["Expiry"].ToString();
                        _StockIn.ExpiryDate = prodrow["ExpiryDate"].ToString();
                        //  _StockIn.ExpiryDate = prodrow.Cells["Col_ExpiryDate"].Value.ToString();
                        // _StockIn.ExpiryDate = General.GetExpiryInyyyymmddForm(_StockIn.ExpiryDate);
                        int mpack = Convert.ToInt32(prodrow["ProdLoosePack"].ToString());
                        _StockIn.VATPer = Convert.ToDouble(prodrow["ProductVATPercent"].ToString());
                        _StockIn.ReasonCode = "S";
                        _StockIn.StockID = prodrow["StockID"].ToString();
                        _StockIn.Amount = (_StockIn.Quantity * _StockIn.PurchaseRate) * mpack;
                        if (_StockIn.VATPer == 6)
                            VatAmount5 += Math.Round(((_StockIn.Amount) * (_StockIn.VATPer)) / 100, 4);
                        else
                        {
                            if (_StockIn.VATPer == 13.50)
                                VatAmount12Point5 += Math.Round(((_StockIn.Amount) * (_StockIn.VATPer)) / 100, 4);
                        }
                        mtotalamt += _StockIn.Amount;
                        string ifRecordFound = "";
                        ifRecordFound = _StockIn.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "Y")
                        {
                            returnVal = _StockIn.UpdateIntblStock();
                        }
                        returnVal = _StockIn.AddProductDetails();
                        returnVal = _StockIn.UpdateCreditNoteStockInMasterProduct();
                    }

                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

                _StockIn.CrdbId = FixAccounts.AccountStockInOut;
                _StockIn.CrdbVouType = FixAccounts.VoucherTypeForStockIN;
                _StockIn.CrdbVouDate = _SSSale.CrdbVouDate;
                _StockIn.CrdbDiscPer = 0;
                _StockIn.CrdbDiscAmt = 0;
                _StockIn.CrdbAmount = mtotalamt;
                _StockIn.CrdbRoundAmount = (mtotalamt) - Math.Round(mtotalamt, 2);
                _StockIn.CrdbAmountNet = Math.Round(mtotalamt, 2);
                _StockIn.Amount = mtotalamt;
                _StockIn.AddDetails();
            }
        }

        private void ActiveGrid_OnSetMultipleMRP()
        {
            lblFooterMessage.Text = "Multiple MRP";
        }
        private void cbTransactionType_Enter_1(object sender, EventArgs e)
        {
            pnlTransactCbBorder.BackColor = Color.Blue;
        }

        private void cbTransactionType_Leave(object sender, EventArgs e)
        {
            pnlTransactCbBorder.BackColor = Color.White;
        }
        private void dgvNextVisitDetail_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNextVisitDetail.Rows.Count > 0 && e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNextVisitDetail.Rows[e.RowIndex];
                if (string.IsNullOrEmpty(Convert.ToString(row.Cells["Col_MobileNumber"].Value)) == false)
                {
                    txtMsg.Text = "This is reminder for your regular medicines.Please Visit our Shop: " + row.Cells["Col_NextDate"].Value.ToString();
                }
            }
        }
        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            if (dgvNextVisitDetail.Rows.Count > 0 && dgvNextVisitDetail.SelectedRows.Count > 0)
            {
                string Msg = "Dear Customer, \n" + txtMsg.Text + "\n From,\n" + General.ShopDetail.ShopName + "\n";
                bool IsMobileNumber = false;
                if (string.IsNullOrEmpty(General.ShopDetail.ShopMobileNumber) == false)
                {
                    Msg += General.ShopDetail.ShopMobileNumber; IsMobileNumber = true;
                }
                if (string.IsNullOrEmpty(General.ShopDetail.ShopTelephone) == false)
                {
                    if (IsMobileNumber)
                        Msg += "/";
                    Msg += General.ShopDetail.ShopTelephone;
                }
                SendSMS mSMS = new SendSMS();
                DataGridViewRow row = dgvNextVisitDetail.SelectedRows[0];
                if (string.IsNullOrEmpty(Convert.ToString(row.Cells["Col_MobileNumber"].Value)) == false)
                {
                    mSMS.SendSMSData(row.Cells["Col_MobileNumber"].Value.ToString(), Msg);
                }
                else
                    MessageBox.Show("Please Update Mobile Number", "PharmaSYSDistributorPlus", MessageBoxButtons.OK);
            }
        }
        #endregion UIEvents
        
    }
}