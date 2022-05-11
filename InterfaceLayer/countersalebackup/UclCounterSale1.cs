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
        private string _lastProductID = "";
        string _lastCustIdSelected = "1";
        private VoucherType _VoucherType;
        List<DataGridViewRow> rowCollection;
        string _preID = "";
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
        public override void SetFocus()
        {
            mpPVC1.SetFocus(1);
        }

        public override bool ClearData()
        {

            DeleteRecordsForSelectedNumber();
            ClearTotals();
            mpPVC1.AddRowsInStockTempTable();
            return true;
        }

        public override bool Add()
        {
            bool retValue = true;
            lblMessage.Text = "";
            tsBtnCancel.Enabled = false;
            tsBtnSave.Enabled = false;
            tsBtnSavenPrint.Enabled = false;           
            mpPVC1.ModuleNumber = ModuleNumber.CounterSale;
            mpPVC1.OperationMode = OperationMode.Add;
            try
            {
                if (mpPVC1.Rows.Count < 1)
                {
                    retValue = base.Add();
                    ClearControls();
                    headerLabel1.Text = "COUNTER SALE -> NEW";
                    pnlCustomerNumber.Enabled = true;
                    pnlPatientDrDetails.Enabled = false;
                    pnlBillAmount.Enabled = false;
                    pnlFinal.Enabled = true;
                    txtsavecustno.Enabled = true;
                    btnPrint.Visible = false;
                    mpPVC1.ColumnsMain.Clear();
                    InitialisempPVC1();
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
                   // cbTransactionType.ValueMember[0];
                 //   FillTransactionType();
                 //   mcbTransactionType.SelectedID = "1";
                    mcbDoctor.SelectedID = "";
                    FillTxtPatientName();
                    FillTxtAddress();
                    pnlFinal.Enabled = false;
                }
                else
                {
                    pnlCustomerNumber.Enabled = true;
                    pnlPatientDrDetails.Enabled = false;
                    pnlBillAmount.Enabled = false;
                    txtsavecustno.Enabled = true;
                    btnPrint.Visible = false;
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
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditCard);
            if (General.CurrentSetting.MsetSaleCreditSale == "Y")
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForVoucher);
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
            txtPatientNameAddress.Text = "";
            txtNetAmount.Text = "0.00";
            txtBillAmount.Text = "0.00";
            txtBillAmount2.Text = "0.00";
            txtDiscAmount.Text = "0.00";
            txtDiscPercent.Text = "0.00";
            pnlFinal.Enabled = false;
            mpPVC1.SetFocus(1);

            return retValue;
        }

        public override bool Exit()
        {
            bool retValue = false;
            try
            {               
                retValue = base.Exit();
                if (retValue)
                {                  
                    System.IO.File.Delete(General.GetCounterSaleTempFile());
                    General.DeleteTempStockByModuleNumber(ModuleNumber.CounterSale);
                    UpdateClosingStockinCache();
                    mpPVC1.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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

        //////private void PrintSaleBillPlainPaper()
        //////{
        //////    PrintRow row;
        //////    try
        //////    {

        //////        PrintBill.Rows.Clear();
        //////        Font fnt = General.FontRegular;
        //////        int totalrows = mpPVC1.Rows.Count;
        //////        PrintPageNumber = 0;
        //////        int rowcount = 0;
        //////        PrintRowPixel = 0;
        //////        int mqty = 0;
        //////        int mlen = 0;
        //////        double mamt = 0;
        //////        int colpix = 1;
        //////        double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
        //////        int totalpages = Convert.ToInt32(totpages);
        //////        PrintHeaderPlainPaper(totalpages, rowcount, fnt);
        //////        foreach (DataGridViewRow dr in mpPVC1.Rows)
        //////        {


        //////            if (dr.Cells["Col_ProductID"].Value != null)
        //////            {

        //////                if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
        //////                {
        //////                    PrintRowPixel = 325;
        //////                    row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
        //////                    PrintBill.Rows.Add(row);
        //////                    PrintBill.Print_Bill();
        //////                    PrintBill.Rows.Clear();
        //////                    PrintRowPixel = 0;
        //////                    PrintHeaderPlainPaper(totalpages, rowcount, fnt);

        //////                    rowcount = 0;
        //////                }
        //////                PrintRowPixel += 17;
        //////                rowcount += 1;
        //////                colpix = 1;

        //////                PharmaSYSRetailPlus.Printing.PageContent PageContent = General.PrintSettings.SaleBillPrintSettingsPlainPaper.PageContent;
        //////                for (int i = 0; i < PageContent.ColumnCount; i++)
        //////                {
        //////                    PharmaSYSRetailPlus.Printing.PageColumn column;
        //////                    column = PageContent.Columns[i];
        //////                    if (column.ColumnDataType == PharmaSYSRetailPlus.Printing.ColumnDataType.Integer)
        //////                    {
        //////                        if (dr.Cells[column.ColumnDataField].Value != null && Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString()) != 0)
        //////                        {
        //////                            mqty = Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString());
        //////                            mlen = (mqty.ToString("#0").Length);
        //////                            colpix = Convert.ToInt32(1 + ((5 - Convert.ToInt32(mlen)) * 5.5));
        //////                            row = new PrintRow(mqty.ToString("#0") + " X " + dr.Cells["Col_Pack"].Value.ToString(), PrintRowPixel, column.Column, column.Font);
        //////                            PrintBill.Rows.Add(row);
        //////                        }
        //////                    }
        //////                    else if (column.ColumnDataType == PharmaSYSRetailPlus.Printing.ColumnDataType.Decimal)
        //////                    {
        //////                        if (dr.Cells[column.ColumnDataField].Value != null && Convert.ToDouble(dr.Cells[column.ColumnDataField].Value.ToString()) != 0)
        //////                        {
        //////                            mamt = Convert.ToDouble(dr.Cells[column.ColumnDataField].Value.ToString());
        //////                            mlen = (mamt.ToString("#0.00").Length);
        //////                            colpix = Convert.ToInt32(column.Column - 30 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
        //////                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, column.Font);
        //////                            PrintBill.Rows.Add(row);
        //////                        }
        //////                    }
        //////                    else
        //////                    {
        //////                        row = new PrintRow(dr.Cells[column.ColumnDataField].Value.ToString(), PrintRowPixel, column.Column, column.Font);
        //////                        PrintBill.Rows.Add(row);
        //////                    }
        //////                }
        //////            }
        //////        }
        //////        PrintRowPixel = 291;
        //////        if (_SSSale.CrdbDiscAmt != 0)
        //////        {
        //////            mamt = Convert.ToDouble(_SSSale.CrdbDiscAmt.ToString("#0.00"));
        //////            mlen = (mamt.ToString("#0.00").Length);
        //////            colpix = Convert.ToInt32(10 + ((10.00 - Convert.ToDouble(mlen)) * 5.5));
        //////            row = new PrintRow("Discount : " + mamt.ToString("#0.00"), PrintRowPixel, 1, fnt);
        //////            PrintBill.Rows.Add(row);
        //////        }
        //////        if (_SSSale.CrdbAmount != 0)
        //////        {
        //////            row = new PrintRow("Gross Amount : ", PrintRowPixel, 350, fnt);
        //////            PrintBill.Rows.Add(row);
        //////            mamt = Convert.ToDouble(_SSSale.CrdbAmount.ToString());
        //////            mlen = (mamt.ToString("#0.00").Length);
        //////            colpix = Convert.ToInt32(435 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
        //////            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, fnt);
        //////            PrintBill.Rows.Add(row);
        //////        }

        //////        PrintRowPixel += 17;
        //////        row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, fnt);
        //////        PrintBill.Rows.Add(row);
        //////        PrintRowPixel += 17;
        //////        row = new PrintRow(_SSSale.CrdbNarration, PrintRowPixel, 15, fnt);
        //////        PrintBill.Rows.Add(row);
        //////        if (_SSSale.CrdbAmountNet != 0)
        //////        {
        //////            row = new PrintRow("    Net Amount : ", PrintRowPixel, 350, fnt);
        //////            PrintBill.Rows.Add(row);
        //////            mamt = Convert.ToDouble(_SSSale.CrdbAmountNet.ToString());
        //////            mlen = (mamt.ToString("#0.00").Length);
        //////            colpix = Convert.ToInt32(435 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
        //////            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, fnt);
        //////            PrintBill.Rows.Add(row);
        //////        }
        //////        PrintRowPixel += 17;
        //////        row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, fnt);
        //////        PrintBill.Rows.Add(row);
        //////        string footer1 = "E & O E.Subject to " + General.ShopDetail.ShopJurisdiction.ToString().Trim() + "  Jurisdiction";
        //////        string footer2 = "DLN." + General.ShopDetail.ShopDLN.ToString().Trim();
        //////        string footer3 = "VAT TIN :" + General.ShopDetail.ShopVATTINV.ToString().Trim() + " , " + General.ShopDetail.ShopVATTINC.ToString().Trim();
        //////        string footersign = "Pharmasist Sign";
        //////        PrintRowPixel += 17;
        //////        row = new PrintRow(footer1, PrintRowPixel, 1, fnt);
        //////        PrintBill.Rows.Add(row);
        //////        mlen = General.ShopDetail.ShopName.Length;
        //////        colpix = 380;
        //////        colpix = colpix - mlen;
        //////        row = new PrintRow(General.ShopDetail.ShopName, PrintRowPixel, colpix, fnt);
        //////        PrintBill.Rows.Add(row);
        //////        PrintRowPixel += 17;
        //////        row = new PrintRow(footer2, PrintRowPixel, 1, fnt);
        //////        PrintBill.Rows.Add(row);
        //////        PrintRowPixel += 17;
        //////        row = new PrintRow(footer3, PrintRowPixel, 1, fnt);
        //////        PrintBill.Rows.Add(row);
        //////        mlen = footersign.Length;
        //////        colpix = 380;
        //////        colpix = colpix - mlen;
        //////        row = new PrintRow(footersign, PrintRowPixel, colpix, fnt);
        //////        PrintBill.Rows.Add(row);

        //////        PrintBill.Print_Bill();
        //////    }
        //////    catch (Exception Ex)
        //////    {
        //////        Log.WriteException(Ex);
        //////    }
        //////}

        //////private int PrintHeaderPlainPaper(int TotalPages, int Rowcount, Font fnt)
        //////{
        //////    PrintRow row;
        //////    try
        //////    {
        //////        PharmaSYSRetailPlus.Printing.PageHeader PageHeader = General.PrintSettings.SaleBillPrintSettingsPlainPaper.PageHeader;
              
        //////        if (PageHeader.ShopName.Show)
        //////        {
        //////            row = new PrintRow(General.ShopDetail.ShopName, PageHeader.ShopName.Row, PageHeader.ShopName.Column, PageHeader.ShopName.Font);
        //////            PrintBill.Rows.Add(row);
        //////        }

        //////        row = new PrintRow(PageHeader.Time.Text + DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PageHeader.Time.Row, PageHeader.Time.Column, PageHeader.Time.Font);
        //////        PrintBill.Rows.Add(row);

        //////        PrintRowPixel = PrintRowPixel + 17;
        //////        row = new PrintRow(General.ShopDetail.ShopAddress1.Trim() + " " + General.ShopDetail.ShopAddress2.Trim(), PageHeader.ShopAddress1.Row, PageHeader.ShopAddress1.Column, General.FontRegularBold);
        //////        PrintBill.Rows.Add(row);
        //////        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //////            row = new PrintRow(PageHeader.VoucherTypeSCA.Text + _SSSale.CrdbVouNo.ToString().Trim(), PageHeader.VoucherTypeSCA.Row, PageHeader.VoucherTypeSCA.Column, PageHeader.VoucherTypeSCA.Font);
        //////        else if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditStatementSale)
        //////            row = new PrintRow(PageHeader.VoucherTypeSCS.Text + _SSSale.CrdbVouNo.ToString().Trim(), PageHeader.VoucherTypeSCS.Row, PageHeader.VoucherTypeSCS.Column, PageHeader.VoucherTypeSCS.Font);
        //////        else
        //////            row = new PrintRow(PageHeader.VoucherTypeSCR.Text + _SSSale.CrdbVouNo.ToString().Trim(), PageHeader.VoucherTypeSCR.Row, PageHeader.VoucherTypeSCR.Column, PageHeader.VoucherTypeSCR.Font);

        //////        PrintBill.Rows.Add(row);               

        //////        PrintRowPixel = PrintRowPixel + 17;
        //////        row = new PrintRow(General.ShopDetail.ShopTelephone.Trim(), PageHeader.ShopTelephone.Row, PageHeader.ShopTelephone.Column, PageHeader.ShopTelephone.Font);
        //////        PrintBill.Rows.Add(row);
        //////        row = new PrintRow(PageHeader.Date.Text + General.GetDateInShortDateFormat(_SSSale.CrdbVouDate), PageHeader.Date.Row, PageHeader.Date.Column, PageHeader.Date.Font);
        //////        PrintBill.Rows.Add(row);

        //////        PrintRowPixel = PrintRowPixel + 17;
        //////        row = new PrintRow(PageHeader.PatientName.Text + " & " + PageHeader.PatientAddress.Text + _SSSale.ShortName, PageHeader.PatientName.Row, PageHeader.PatientName.Column, PageHeader.PatientName.Font);
        //////        PrintBill.Rows.Add(row);

        //////        PrintRowPixel = PrintRowPixel + 17;

        //////        row = new PrintRow(PageHeader.DoctorName.Text + _SSSale.DoctorNameAddress.Trim(), PageHeader.DoctorName.Row, PageHeader.DoctorName.Column, PageHeader.DoctorName.Font);
        //////        PrintBill.Rows.Add(row);

        //////        PrintPageNumber += 1;
        //////        string page = PrintPageNumber.ToString().Trim() + "/" + TotalPages.ToString().Trim();
        //////        row = new PrintRow(page, PageHeader.PageNo.Row, PageHeader.PageNo.Column, PageHeader.PageNo.Font);
        //////        PrintBill.Rows.Add(row);

        //////        PrintRowPixel += 17;
        //////        row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, fnt);
        //////        PrintBill.Rows.Add(row);

        //////        PrintRowPixel += 17;

        //////        PharmaSYSRetailPlus.Printing.PageContent PageContent = General.PrintSettings.SaleBillPrintSettingsPlainPaper.PageContent;
        //////        //HEADERS
        //////        for (int i = 0; i < PageContent.ColumnCount; i++)
        //////        {
        //////            row = new PrintRow(PageContent.Columns[i].ColumnHeader, PrintRowPixel, PageContent.Columns[i].Column, PageContent.Columns[i].Font);
        //////            PrintBill.Rows.Add(row);
        //////        }               
        //////        PrintRowPixel += 17;

        //////        row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, fnt);
        //////        PrintBill.Rows.Add(row);

        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        Log.WriteException(ex);
        //////    }
        //////    Rowcount = 1;
        //////    return Rowcount;

        //////}
        private void PrintSaleBillPlainPaper()
        {
            PharmaSYSRetailPlus.Printing.PlainPaperPrinter printer = new PharmaSYSRetailPlus.Printing.PlainPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "", _SSSale.DoctorNameAddress, "", PrintGrid.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount);

        }        

        private void PrintSaleBillPrePrintedPaper()
        {
           
            PharmaSYSRetailPlus.Printing.PrePrintedPaperPrinter printer = new PharmaSYSRetailPlus.Printing.PrePrintedPaperPrinter();
          //  DataGridViewRowCollection rows = new DataGridViewRowCollection(mpPVC1.datagri);
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "",  _SSSale.DoctorNameAddress, "", PrintGrid.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet,_SSSale.SaleSubType,_SSSale.CrdbDiscAmt,_SSSale.CrNoteAmount,_SSSale.DbNoteAmount, _SSSale.CrdbAmount);

        }

        private void FillPrintGrid()
        {
            foreach (DataGridViewRow dr in mpPVC1.Rows)
            {
                if (dr.Cells[0].Value != null && dr.Cells["Col_Quantity"].Value != null && Convert.ToInt32(dr.Cells["Col_CustID"].Value.ToString()) == _SSSale.CustNumber)
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
//        private void PrintSaleBillPrePrintedPaper()
//        {
//            PrintRow row;
//            try
//            {

//                PrintBill.Rows.Clear();
//                Font fnt = General.FontRegular;
//                int totalrows = mpPVC1.Rows.Count;
//                PrintPageNumber = 0;
//                int rowcount = 0;
//                PrintRowPixel = 0;
//                int mqty = 0;
//                int mlen = 0;
//                double mamt = 0;
//                int colpix = 1;
//                double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerSaleBill));
//                int totalpages = Convert.ToInt32(totpages);
//                PrintHeaderPrePrintedPaper(totalpages, rowcount, fnt);
//                foreach (DataGridViewRow dr in mpPVC1.Rows)
//                {


//                    if (dr.Cells["Col_ProductID"].Value != null)
//                    {

//                        if (rowcount > FixAccounts.NumberOfRowsPerSaleBill)
//                        {
//                            PrintRowPixel = 325;
//                            row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
//                            PrintBill.Rows.Add(row);
//                            PrintBill.Print_Bill();
//                            PrintBill.Rows.Clear();
//                            PrintRowPixel = 0;
//                            PrintHeaderPrePrintedPaper(totalpages, rowcount, fnt);

//                            rowcount = 0;
//                        }
//                        PrintRowPixel += 17;
//                        rowcount += 1;
//                        colpix = 1;

//                        PharmaSYSRetailPlus.Printing.PageContent PageContent = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.PageContent;
//                        for (int i = 0; i < PageContent.ColumnCount; i++)
//                        {
//                            PharmaSYSRetailPlus.Printing.PageColumn column;
//                            column = PageContent.Columns[i];
//                            if (column.ColumnDataType == PharmaSYSRetailPlus.Printing.ColumnDataType.Integer)
//                            {
//                                if (dr.Cells[column.ColumnDataField].Value != null && Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString()) != 0)
//                                {
//                                    mqty = Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString());
//                                    mlen = (mqty.ToString("#0").Length);
//                                    colpix = Convert.ToInt32(1 + ((5 - Convert.ToInt32(mlen)) * 5.5));
//                                    row = new PrintRow(mqty.ToString("#0") + " X " + dr.Cells["Col_Pack"].Value.ToString(), PrintRowPixel, column.Column, column.Font);
//                                    PrintBill.Rows.Add(row);
//                                }
//                            }
//                            else if (column.ColumnDataType == PharmaSYSRetailPlus.Printing.ColumnDataType.Decimal)
//                            {
//                                if (dr.Cells[column.ColumnDataField].Value != null && Convert.ToDouble(dr.Cells[column.ColumnDataField].Value.ToString()) != 0)
//                                {
//                                    mamt = Convert.ToDouble(dr.Cells[column.ColumnDataField].Value.ToString());
//                                    mlen = (mamt.ToString("#0.00").Length);
//                                    colpix = Convert.ToInt32(column.Column - 30 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
//                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, column.Font);
//                                    PrintBill.Rows.Add(row);
//                                }
//                            }
//                            else
//                            {
//                                row = new PrintRow(dr.Cells[column.ColumnDataField].Value.ToString(), PrintRowPixel, column.Column, column.Font);
//                                PrintBill.Rows.Add(row);
//                            }
//                        }

                       

//                    }
//                }
//                PrintRowPixel = 291;
//                if (_SSSale.CrdbDiscAmt != 0)
//                {
//                    mamt = Convert.ToDouble(_SSSale.CrdbDiscAmt.ToString("#0.00"));
//                    mlen = (mamt.ToString("#0.00").Length);
//                    colpix = Convert.ToInt32(10 + ((10.00 - Convert.ToDouble(mlen)) * 5.5));
//                    row = new PrintRow("Discount : " + mamt.ToString("#0.00"), PrintRowPixel, 1, fnt);
//                    PrintBill.Rows.Add(row);
//                }
//                if (_SSSale.CrdbAmount != 0)
//                {
//                    row = new PrintRow("Gross Amount : ", PrintRowPixel, 350, fnt);
//                    PrintBill.Rows.Add(row);
//                    mamt = Convert.ToDouble(_SSSale.CrdbAmount.ToString());
//                    mlen = (mamt.ToString("#0.00").Length);
//                    colpix = Convert.ToInt32(435 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
//                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, fnt);
//                    PrintBill.Rows.Add(row);
//                }

//                PrintRowPixel += 17;
//                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, fnt);
//                PrintBill.Rows.Add(row);
//                PrintRowPixel += 17;
//                row = new PrintRow(_SSSale.CrdbNarration, PrintRowPixel, 15, fnt);
//                PrintBill.Rows.Add(row);
//                if (_SSSale.CrdbAmountNet != 0)
//                {
//                    row = new PrintRow("    Net Amount : ", PrintRowPixel, 350, fnt);
//                    PrintBill.Rows.Add(row);
//                    mamt = Convert.ToDouble(_SSSale.CrdbAmountNet.ToString());
//                    mlen = (mamt.ToString("#0.00").Length);
//                    colpix = Convert.ToInt32(435 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
//                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, fnt);
//                    PrintBill.Rows.Add(row);
//                }
//                PrintRowPixel += 17;
//                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, fnt);
//                PrintBill.Rows.Add(row);
//                string footer1 = "E & O E.Subject to " + General.ShopDetail.ShopJurisdiction.ToString().Trim() + "  Jurisdiction";
//                string footer2 = "DLN." + General.ShopDetail.ShopDLN.ToString().Trim();
//                string footer3 = "VAT TIN :" + General.ShopDetail.ShopVATTINV.ToString().Trim() + " , " + General.ShopDetail.ShopVATTINC.ToString().Trim();
//                string footersign = "Pharmasist Sign";
//                PrintRowPixel += 17;
//                row = new PrintRow(footer1, PrintRowPixel, 1, fnt);
//                PrintBill.Rows.Add(row);
//                mlen = General.ShopDetail.ShopName.Length;
//                colpix = 380;
//                colpix = colpix - mlen;
//                row = new PrintRow(General.ShopDetail.ShopName, PrintRowPixel, colpix, fnt);
//                PrintBill.Rows.Add(row);
//                PrintRowPixel += 17;
//                row = new PrintRow(footer2, PrintRowPixel, 1, fnt);
//                PrintBill.Rows.Add(row);
//                PrintRowPixel += 17;
//                row = new PrintRow(footer3, PrintRowPixel, 1, fnt);
//                PrintBill.Rows.Add(row);
//                mlen = footersign.Length;
//                colpix = 380;
//                colpix = colpix - mlen;
//                row = new PrintRow(footersign, PrintRowPixel, colpix, fnt);
//                PrintBill.Rows.Add(row);

//                PrintBill.Print_Bill();
//            }
//            catch (Exception Ex)
//            {
//                Log.WriteException(Ex);
//            }
//        }

//        private int PrintHeaderPrePrintedPaper(int TotalPages, int Rowcount, Font fnt)
//        {
//            PrintRow row;
//            try
//            {
//                PharmaSYSRetailPlus.Printing.PageHeader PageHeader = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.PageHeader;
         
//                if (PageHeader.ShopName.Show)
//                {
//                    row = new PrintRow(General.ShopDetail.ShopName, PageHeader.ShopName.Row, PageHeader.ShopName.Column, PageHeader.ShopName.Font);
//                    PrintBill.Rows.Add(row);
//                }

//                if (PageHeader.Time.Show)
//                {
//                    row = new PrintRow(PageHeader.Time.Text + DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PageHeader.Time.Row, PageHeader.Time.Column, PageHeader.Time.Font);
//                    PrintBill.Rows.Add(row);
//                }

//              //  PrintRowPixel = PrintRowPixel + 17;
//                if (PageHeader.ShopAddress1.Show)
//                {
//                    row = new PrintRow(General.ShopDetail.ShopAddress1.Trim() + " " + General.ShopDetail.ShopAddress2.Trim(), PageHeader.ShopAddress1.Row, PageHeader.ShopAddress1.Column, General.FontRegularBold);
//                    PrintBill.Rows.Add(row);
//                }

//                if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
//                    row = new PrintRow(PageHeader.VoucherTypeSCA.Text + _SSSale.CrdbVouNo.ToString().Trim(), PageHeader.VoucherTypeSCA.Row, PageHeader.VoucherTypeSCA.Column, PageHeader.VoucherTypeSCA.Font);
//                else if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditStatementSale)
//                    row = new PrintRow(PageHeader.VoucherTypeSCS.Text + _SSSale.CrdbVouNo.ToString().Trim(), PageHeader.VoucherTypeSCS.Row, PageHeader.VoucherTypeSCS.Column, PageHeader.VoucherTypeSCS.Font);
//                else
//                    row = new PrintRow(PageHeader.VoucherTypeSCR.Text + _SSSale.CrdbVouNo.ToString().Trim(), PageHeader.VoucherTypeSCR.Row, PageHeader.VoucherTypeSCR.Column, PageHeader.VoucherTypeSCR.Font);

//                PrintBill.Rows.Add(row);              

//             //   PrintRowPixel = PrintRowPixel + 17;
//                if (PageHeader.ShopTelephone.Show)
//                {
//                    row = new PrintRow(General.ShopDetail.ShopTelephone.Trim(), PageHeader.ShopTelephone.Row, PageHeader.ShopTelephone.Column, PageHeader.ShopTelephone.Font);
//                    PrintBill.Rows.Add(row);
//                }
//                row = new PrintRow(PageHeader.Date.Text + General.GetDateInShortDateFormat(_SSSale.CrdbVouDate), PageHeader.Date.Row, PageHeader.Date.Column, PageHeader.Date.Font);
//                PrintBill.Rows.Add(row);

//             //   PrintRowPixel = PrintRowPixel + 17;
//                row = new PrintRow(PageHeader.PatientName.Text + " & " + PageHeader.PatientAddress.Text + _SSSale.ShortName, PageHeader.PatientName.Row, PageHeader.PatientName.Column, PageHeader.PatientName.Font);
//                PrintBill.Rows.Add(row);

////PrintRowPixel = PrintRowPixel + 17;

//                row = new PrintRow(PageHeader.DoctorName.Text + _SSSale.DoctorNameAddress.Trim(), PageHeader.DoctorName.Row, PageHeader.DoctorName.Column, PageHeader.DoctorName.Font);
//                PrintBill.Rows.Add(row);

//                PrintPageNumber += 1;
//                string page = PrintPageNumber.ToString().Trim() + "/" + TotalPages.ToString().Trim();
//                row = new PrintRow(page, PageHeader.PageNo.Row, PageHeader.PageNo.Column, PageHeader.PageNo.Font);
//                PrintBill.Rows.Add(row);                

//                PharmaSYSRetailPlus.Printing.PageContent PageContent = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.PageContent;             
          
//            }
//            catch (Exception ex)
//            {
//                Log.WriteException(ex);
//            }
//            Rowcount = 1;
//            return Rowcount;
//        }

        public override bool Save()
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
            _SSSale.DocID = string.Empty;
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
                            if (cbTransactionType.Text == "CreditStatement" || cbTransactionType.Text == "Credit")
                                cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
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
                        //if (rbtVoucherSale.Checked)
                        //    _SSSale.SaleSubType = "V";
                        //else
                        //    _SSSale.SaleSubType = "P";
                        if (cbTransactionType.Text == FixAccounts.TransactionTypeForVoucher)
                            _SSSale.SaleSubType = "V";
                        else
                        {
                            _SSSale.SaleSubType = "P";
                            if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit || cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                                cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                        }
                    }
                  
                    

                   

                 //   if (rbtCash.Checked == true)
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
                        }
                    }

                    System.Text.StringBuilder _errorMessage;

                    if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != string.Empty)
                    {
                        _SSSale.DocID = mcbDoctor.SelectedID;
                        _SSSale.DoctorNameAddress = mcbDoctor.SeletedItem.ItemData[3];
                    }
                    else
                    {
                        _SSSale.DoctorNameAddress = mcbDoctor.Text;
                        _SSSale.DocID = string.Empty;
                    }

                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _SSSale.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                    _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    _SSSale.CrdbVouType = txtVouType.Text.ToString();
                    if (txtsavecustno.Text == "")
                        txtsavecustno.Text = mpPVC1.Rows[1].Cells["Col_CustID"].Value.ToString();
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
                    double.TryParse(txtBillAmount.Text, out mbillamount);
                    _SSSale.CrdbAmountNet = mbillamount;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditSale || _SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditStatementSale)
                    {
                        _SSSale.CrdbAmountBalance = mbillamount;
                        _SSSale.CrdbAmountClear = 0;
                    }
                    else
                    {
                        _SSSale.CrdbAmountBalance = 0;
                        _SSSale.CrdbAmountClear = mbillamount;
                    }
                    double.TryParse(txtNetAmount.Text, out mamount);
                    _SSSale.CrdbAmount = mamount;
                    if (txtAddress.Text != null && txtAddress.Text != "")
                        _SSSale.PatientAddress1 = txtAddress.Text;
                    if (txtPatientName.Text != null && txtPatientName.Text.ToString() != "")
                        _SSSale.CrdbName = txtPatientName.Text;
                    if (txtPatientNameAddress.Text != null)
                        _SSSale.ShortName = txtPatientNameAddress.Text;
                    if (_SSSale.ShortName.Length > 50)
                        _SSSale.ShortName = _SSSale.ShortName.Substring(0, 50);
                    if (txtDoctorNameAddress.Text != null && txtDoctorNameAddress.Text != string.Empty)
                        _SSSale.DoctorNameAddress = txtDoctorNameAddress.Text;
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
                            DateTime tt = DateTime.Now.Date;
                            System.TimeSpan ts;
                            ts = (tt - dd);
                            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                            {
                                _SSSale.CrdbCountersaleNumber = ts.Days;
                            }
                            else
                            {
                                _SSSale.CrdbCountersaleNumber = 0;
                            }
                            txtVouchernumber.Text = Convert.ToString(_SSSale.CrdbVouNo);
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
                                
                                txtsavecustno.Enabled = false;                               
                                General.DeleteTempStockByModuleNumber(ModuleNumber.CounterSale);                               
                                string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
                                PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                if (result == PSDialogResult.Print)
                                    Print();
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
                if (keyPressed == Keys.S && modifier == Keys.Alt)
                {
                    txtsavecustno.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    mcbDoctor.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.E && modifier == Keys.Alt)
                {
                    btnDelete.Focus();
                    retValue = true;
                }               
                if (keyPressed == Keys.V && modifier == Keys.Alt)
                {
                    btnViewClick();
                    retValue = true;
                }
                if (keyPressed == Keys.Y && modifier == Keys.Alt)
                {
                    cbTransactionType.Focus();
                    retValue = true;
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
                ConstructMainColumns();
                ConstructProductSelectionListGridColumns();
                ConstructBatchGridColumns();
                ConstructTempGridColumns();              

                DataTable dtable = new DataTable();
                dtable = _SSSale.ReadProductDetailsByID();
                mpPVC1.DataSourceMain = dtable;
               
                Product prod = new Product();
                mpPVC1.DataSourceProductList = PharmaSysRetailPlusCache.GetProductData();
                FormatGrids();
                mpPVC1.Bind();
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
        }
        //private void GetSaleSettings()
        //{            
        //    SSSale setting = new SSSale();
        //    setting.GetSaleSettings();            
        //}

        private void DeleteRecordsForSelectedNumber()
        {
            try
            {
                List<DataGridViewRow> rowCollection = new List<DataGridViewRow>();
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_CustID"].Value.ToString() == txtsavecustno.Text.ToString() && prodrow.Cells["Col_ProductID"].Value != null)
                    {
                        rowCollection.Add(prodrow);
                    }
                }

                foreach (DataGridViewRow prodrow in rowCollection)
                {
                    mpPVC1.Rows.Remove(prodrow);
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
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0 &&
                            prodrow.Cells["Col_CustID"].Value.ToString() == txtsavecustno.Text.ToString())
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
          //  double mpuramt = 0;
            double mvatper = 0;
            double mvatamt = 0;
            double mamt = 0;
            double mrate = 0;
        //    _SSSale.AmountByPurchaseRate = 0;
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
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0 &&
                        prodrow.Cells["Col_CustID"].Value.ToString() == txtsavecustno.Text.ToString())                   
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
                        _SSSale.SaleRate = msalerate;
                        _SSSale.ProfitPercentBySaleRate = Math.Round((msalerate - (mpurrate + mvatamt)) / msalerate, 4);
                        _SSSale.ProfitPercentByPurchaseRate = Math.Round((msalerate - (mpurrate + mvatamt)) / (mpurrate + mvatamt), 4);
                        _SSSale.TotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
                        _SSSale.TotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
                        _SSSale.ProfitInRupees = Math.Round(((msalerate - (mpurrate + mvatamt)) / mpakn) * mqty, 2);
                        _SSSale.TotalProfitInRupees += _SSSale.ProfitInRupees;
                        prodrow.Cells["Col_ProfitPercentBySaleRate"].Value = _SSSale.ProfitPercentBySaleRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _SSSale.ProfitPercentByPurchaseRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitInRupees"].Value = _SSSale.ProfitInRupees.ToString("#0.00");
                    }
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
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0 &&
                        prodrow.Cells["Col_CustID"].Value.ToString() == txtsavecustno.Text.ToString())
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

                mdebit = Math.Round(mbillamount - mvat5per - mvat12point5per + mdiscamount - maddon - mround - mamtforzerovat + mcreditnoteamt - mdebitnoteamt, 2);

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
                    _SSSale.CreditAmount = mvat5per;
                    retValue = _SSSale.AddVoucherIntblTrnac();

                }
                if (retValue == true &&  mvat12point5per > 0)
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
                    _SSSale.CreditAmount = mvat12point5per;
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
                if (retValue == true &&  mround > 0)
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
                if (retValue == true &&  mround < 0)
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

                if (retValue == true &&  mdiscamount > 0)
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

                if (retValue == true &&  mcreditnoteamt > 0)
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
                if (retValue == true &&  mdebitnoteamt > 0)
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
                if (retValue == true &&  mbillamount > 0)
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
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0 &&
                            prodrow.Cells["Col_CustID"].Value.ToString() == txtsavecustno.Text.ToString())
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
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_ProductID"].Value != null && prodrow.Cells["Col_ProductID"].Value.ToString() != string.Empty)
                    {
                        _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();                       
                        PharmaSysRetailPlusCache.RefreshProductData(_SSSale.ProductID);
                    }
                }
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductID"].Value != null && prodrow.Cells["Temp_ProductID"].Value.ToString() != string.Empty)
                    {
                        _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();                      
                        PharmaSysRetailPlusCache.RefreshProductData(_SSSale.ProductID);
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
                DataTable dtable = _Party.GetOverviewDataForCounterSale();
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

                if (colIndex == 0)
                {
                    if (_lastCustIdSelected == "1")
                        mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn1.BackColor;
                    else if (_lastCustIdSelected == "2")
                        mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn2.BackColor;
                    else if (_lastCustIdSelected == "3")
                        mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn3.BackColor;
                    else if (_lastCustIdSelected == "4")
                        mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn4.BackColor;
                    else if (_lastCustIdSelected == "5")
                        mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn5.BackColor;
                    else
                        mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn6.BackColor;
                }
                if (colIndex == 1)
                {
                    _preID = "";
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                        _preID = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value != null)
                        prodname = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString();
                    if (prodname != "" && _preID != "")
                    {
                        prodname = _SSSale.GetProductName(_preID);
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = prodname;
                    }
                }
                if (colIndex == 11)  // Quantity
                {
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value == null || Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString()) == 0)
                        mpPVC1.IsAllowNewRow = false;
                    else
                    {
                        string mdt = DateTime.Today.Date.ToString("yyyyMMdd");
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value != null)
                            mexpirydate = mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                        //if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value != null)
                        //{                           
                        if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                        {
                            lblMessage.Text = "Expired Product";
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = 0;
                            mpPVC1.IsAllowNewRow = false;
                            mpPVC1.SetFocus(11);
                        }
                        else
                        {
                            requiredQty = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                            if (requiredQty <= 0 || _SSSale.CurrentBatchStock == 0)
                            {
                                if (requiredQty <= 0)
                                {
                                    lblMessage.Text = "Enter Quantity";
                                    mpPVC1.SetFocus(11);
                                    mpPVC1.IsAllowNewRow = false;
                                }
                                else
                                {
                                    lblMessage.Text = "Batch Stock Zero";
                                    mpPVC1.SetFocus(11);
                                    mpPVC1.IsAllowNewRow = false;
                                }
                            }
                            else if (requiredQty > _SSSale.CurrentBatchStock && _Mode == OperationMode.Edit)
                            {
                                lblMessage.Text = "Enter Correct Quantity";
                                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = _SSSale.CurrentBatchStock;
                                mpPVC1.SetFocus(11);
                                mpPVC1.IsAllowNewRow = false;
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
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                                    mprodno = (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString());
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value != null)
                                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value.ToString().Trim(), out mbatchstock);
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value != null)
                                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString().Trim(), out oldQuantity);
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                                    mstockid = (mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString());
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value != null)
                                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value.ToString(),out custno);

                                lblMessage.Text = "";

                                if (requiredQty <= _SSSale.CurrentBatchStock)
                                {
                                    FillBatchStock(ref mmrp, ref mrate, ref mpakn, ref mbtno, ref mprodno, ref mcurrentindex, ref oldmqty, ref mqty, ref custno);
                                    mpPVC1.IsAllowNewRow = true;
                                    if (_Mode == OperationMode.Add)
                                    {
                                        WriteToXML();
                                    }
                                    mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
                                }
                                else
                                {
                                    //if ((requiredQty > _SSSale.CurrentProductStock + oldQuantity - gridstock) || gridprodstock > 0)
                                    //{
                                    //    lblMessage.Text = "Enter Correct Quantity";
                                    //    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = _SSSale.CurrentBatchStock;
                                    //    mpPVC1.IsAllowNewRow = false;
                                    //    mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = false;
                                    //    CalculateAmount(-1);
                                    //}
                                    //else
                                    //{

                                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                                            mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();

                                        FillMainGridwithMultipleBatch(requiredQty, mprodno);
                                        CalculateAmount(-1);
                                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;

                                    //}
                                    if (_Mode == OperationMode.Add)
                                    {
                                        WriteToXML();
                                    }
                                    mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
                                }


                            }

                        }

                    }
                    //else
                    //{
                    //    mpPVC1.MainDataGridCurrentRow.Cells[0].Value = null;
                    //    mpPVC1.MainDataGridCurrentRow.Cells[11].Value = null;
                    //    mpPVC1.MainDataGridCurrentRow.Cells[1].Value = null;
                    //    mpPVC1.SetFocus(mpPVC1.MainDataGridCurrentRow.Index, 1);
                    //}

                   
                }

                if (colIndex == 10)  // sale rate
                {
                    if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value) <= 0)
                    {
                        lblMessage.Text = "Enter SaleRate";
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


                if (colIndex == 7)  // Expiry
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
                            lblMessage.Text = "";
                        }
                        else
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                            lblMessage.Text = " No Expiry ";
                        }

                    }
                    else
                    {
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
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
            DataTable dt = mpPVC1.GetGridData();
            if (dt.Rows.Count > 0)
                dt.WriteXml(General.GetCounterSaleTempFile());
        }
        //private void FillMainGridwithMultipleBatch(string prodID, int requiredqty)
        //{
        //    int mmaingridrowIndex = mpPVC1.MainDataGridCurrentRow.Index - 1;
           
        //    DataTable stkdt = new DataTable();
        //    Stock prodstk = new Stock();          
        //    string custID = "";
        //    int mycolindex = 0;
        //    string productID = prodID;
        //    int msalestk = requiredqty;
        //    stkdt = prodstk.GetStockByProductIDForSale(productID);    
        //    custID = _lastCustIdSelected;
        //    int icount = 0;
        //    foreach (DataGridViewRow dr in mpPVC1.Rows)
        //    {
        //        if (dr.Cells["Col_ProductID"].Value != null && (dr.Cells["Col_ProductID"].Value.ToString() == prodID))
        //        {                  
        //            break;
        //        }
        //    }           
                  
        //    try
        //    {

        //        foreach (DataRow dtrow in stkdt.Rows)
        //        {
        //            int mbatchstock = 0;
        //            int mactualsalestock = 0;
        //            double msalerate = 0;
        //            int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock);
        //            mactualsalestock = Math.Min(mbatchstock, msalestk);
        //            if (mactualsalestock > 0 && msalestk > 0)
        //            {
        //                string mbtno = "";
        //                double mmrp = 0;
        //                //string mproddr1 = "";
        //                //string mbatnodr1 = "";
        //                //double mmrpdr1 = 0;
        //                int msaleQtydr1 = 0;
        //                int mbatchstkdr1 = 0;
        //                string mstockid = "";
        //                string ifbatchfoundindr1 = "";
        //                mbtno = dtrow["BatchNumber"].ToString();
        //                double.TryParse(dtrow["MRP"].ToString(), out mmrp);
        //             //   mycolindex = 0;
        //                //foreach (DataGridViewRow dr1 in mpPVC1.Rows)
        //                //{
        //                //    if (dr1.Cells["Col_ProductID"].Value != null)
        //                //    {
        //                //        mproddr1 = dr1.Cells["Col_ProductID"].Value.ToString();
        //                //        mbatnodr1 = dr1.Cells["Col_BatchNumber"].Value.ToString();
        //                //        double.TryParse(dr1.Cells["Col_MRP"].Value.ToString(), out mmrpdr1);
        //                //        int.TryParse(dr1.Cells["Col_Quantity"].Value.ToString(), out msaleQtydr1);
        //                //        int.TryParse(dr1.Cells["Col_BatchStock"].Value.ToString(), out mbatchstkdr1);
        //                //        if (dr1.Cells["Col_StockID"].Value != null)
        //                //            mstockid = dr1.Cells["Col_StockID"].Value.ToString();
        //                //        //if (mprodno == mproddr1 && mbtno == mbatnodr1 && mmrp == mmrpdr1)
        //                //        //{
        //                //        //    mycolindex = dr1.Index;
        //                //        //    ifbatchfoundindr1 = "Y";
        //                //        //    break;
        //                //        //}
        //                //    }

        //                //}
        //                if (ifbatchfoundindr1 == "Y")
        //                {
        //                    //   mpPVC1.Rows[mycolindex].Cells["Col_ProductID"].Value = mprodno;
        //                    mpPVC1.Rows[mycolindex].Cells["Col_CustID"].Value = custID;
        //                    mpPVC1.Rows[mycolindex].DefaultCellStyle.BackColor = GetBackColorByCustID(custID);
        //                    mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].Value = dtrow["ProdName"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_UOM"].Value = dtrow["ProdLoosePack"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_Pack"].Value = dtrow["ProdPack"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_ProdCompShortName"].Value = dtrow["ProdCompShortName"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_Shelf"].Value = dtrow["ShelfCode"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_BatchNumber"].Value = dtrow["BatchNumber"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_Expiry"].Value = dtrow["Expiry"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_MRP"].Value = Convert.ToDouble(dtrow["MRP"].ToString()).ToString("#0.00");
        //                    mpPVC1.Rows[mycolindex].Cells["Col_SaleRate"].Value = Convert.ToDouble(dtrow["SaleRate"].ToString()).ToString("#0.00");
        //                    double.TryParse(dtrow["SaleRate"].ToString(), out msalerate);
        //                    mpPVC1.Rows[mycolindex].Cells["Col_Quantity"].Value = Math.Min((mactualsalestock + msaleQtydr1), mbatchstkdr1);
        //                    mpPVC1.Rows[mycolindex].Cells["Col_Amount"].Value = (msalerate * mactualsalestock).ToString("#0.00");
        //                    mpPVC1.Rows[mycolindex].Cells["Col_ClosingStock"].Value = dtrow["ProdClosingStock"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_PurchaseRate"].Value = dtrow["PurchaseRate"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_ExpiryDate"].Value = dtrow["ExpiryDate"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = 0;
        //                    mpPVC1.Rows[mycolindex].Cells["Col_VATPer"].Value = Convert.ToDouble(dtrow["ProdVATPercent"].ToString()).ToString("#0.00");
        //                    mpPVC1.Rows[mycolindex].Cells["Col_TradeRate"].Value = dtrow["TradeRate"].ToString();
        //                    int mclstkdr1 = 0;
        //                    int.TryParse(dtrow["ClosingStock"].ToString(), out mclstkdr1);
        //                    mpPVC1.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;
        //                    mpPVC1.Rows[mycolindex].Cells["Col_StockID"].Value = mstockid;
        //                    msalestk = msalestk - mactualsalestock;
        //                }
        //                else
        //                {
        //                    icount = icount + 1;
        //                    mycolindex = mmaingridrowIndex;
        //                    if (icount > 2)
        //                        mpPVC1.Rows.Add();
                            
        //                    //mmaingridrowIndex = mmaingridrowIndex + 1;
        //                    //mpPVC1.Rows[mycolindex].Cells["Col_CustID"].Value = custID;
        //                    //mpPVC1.Rows[mycolindex].DefaultCellStyle.BackColor = GetBackColorByCustID(custID);
        //                    mpPVC1.Rows[mycolindex].Cells["Col_ProductID"].Value = dtrow["ProductID"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].Value = dtrow["ProdName"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_UOM"].Value = dtrow["ProdLoosePack"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_Pack"].Value = dtrow["ProdPack"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_Shelf"].Value = dtrow["ShelfCode"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_BatchNumber"].Value = dtrow["BatchNumber"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_Expiry"].Value = dtrow["Expiry"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_MRP"].Value = Convert.ToDouble(dtrow["MRP"].ToString()).ToString("#0.00");
        //                    mpPVC1.Rows[mycolindex].Cells["Col_SaleRate"].Value = Convert.ToDouble(dtrow["SaleRate"].ToString()).ToString("#0.00");
        //                    double.TryParse(dtrow["SaleRate"].ToString(), out msalerate);
        //                    mpPVC1.Rows[mycolindex].Cells["Col_Quantity"].Value = Math.Min(mactualsalestock, mbatchstock);
        //                    double mamt = 0;
        //                    mamt = Math.Round(msalerate * mactualsalestock, 2);
        //                    mpPVC1.Rows[mycolindex].Cells["Col_Amount"].Value = Convert.ToDouble(mamt.ToString()).ToString("#0.00");
        //                    mpPVC1.Rows[mycolindex].Cells["Col_ClosingStock"].Value = dtrow["ProdClosingStock"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_PurchaseRate"].Value = dtrow["PurchaseRate"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_ExpiryDate"].Value = dtrow["ExpiryDate"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = 0;
        //                    mpPVC1.Rows[mycolindex].Cells["Col_VATPer"].Value = Convert.ToDouble(dtrow["ProdVATPercent"].ToString()).ToString("#0.00");
        //                    mpPVC1.Rows[mycolindex].Cells["Col_TradeRate"].Value = dtrow["TradeRate"].ToString();
        //                    mpPVC1.Rows[mycolindex].Cells["Col_StockID"].Value = dtrow["StockID"].ToString();
        //                    int mclstkdr1 = 0;
        //                    int.TryParse(dtrow["ClosingStock"].ToString(), out mclstkdr1);
        //                    mpPVC1.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;
        //                    msalestk = msalestk - mactualsalestock;
        //                    CalculateAmount();
        //                    mpPVC1.IsAllowNewRow = true;
        //                  //  mpPVC1.Rows.Add();
        //                    mmaingridrowIndex = mmaingridrowIndex + 1;
        //                    mpPVC1.SetFocus(mmaingridrowIndex + 1, 1);                         
        //                    mpPVC1.Rows[mycolindex].Cells["Col_CustID"].Value = custID;
        //                    mpPVC1.Rows[mycolindex].DefaultCellStyle.BackColor = GetBackColorByCustID(custID);
        //                 //   mpPVC1.Rows[mycolindex].Cells["Col_ProductID"].Value = "";
        //                    mpPVC1.SetFocus(mmaingridrowIndex, 1);
                            
        //                }
                       
        //            }
        //        }
        //       // mpPVC1.SetFocus(mycolindex+1, 1);


        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }

        //}

        private void FillMainGridwithMultipleBatch(int requiredqty, string productID)
        {
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

                            mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value = _lastCustIdSelected;
                            if (_lastCustIdSelected == "1")
                                mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn1.BackColor;
                            else if (_lastCustIdSelected == "2")
                                mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn2.BackColor;
                            else if (_lastCustIdSelected == "3")
                                mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn3.BackColor;
                            else if (_lastCustIdSelected == "4")
                                mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn4.BackColor;
                            else if (_lastCustIdSelected == "5")
                                mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn5.BackColor;
                            else
                                mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn6.BackColor;
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
        private void FillBatchStock(ref double mmrp, ref double mrate, ref int mpakn, ref string mbtno, ref string mprodno, ref int mcurrentindex, ref int oldmqty, ref int mqty ,ref int custno)
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
                         drp.Cells["Col_MRP"].Value != null && drp.Cells["Col_CustID"].Value != null)
                {
                    if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno &&
                          drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno &&
                             drp.Cells["Col_MRP"].Value.ToString().Trim() == mmrp.ToString("#0.00") && drp.Index != mcurrentindex && Convert.ToInt32(drp.Cells["Col_CustID"].Value.ToString()) == custno)
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
        private void mpPVC1_OnProductSelected(DataGridViewRow productRow)
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
                mpPVC1.MainDataGridCurrentRow.Cells[0].Value = productRow.Cells[0].Value;
                _SSSale.ProductID = productRow.Cells[0].Value.ToString();
                mprodno = _SSSale.ProductID;
                double.TryParse(productRow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                _SSSale.PurchaseRate = mprate;
                int.TryParse(productRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);
                _SSSale.Closingstock = mclstk;
                if (mclstk > 0)
                {
                    mifshortlisted = productRow.Cells["Col_IfShortListed"].Value.ToString().Trim();
                    if (productRow.Cells["Col_IfSaleDisc"].Value != null && productRow.Cells["Col_IfSaleDisc"].Value.ToString() != "")
                        mifsalediscount = productRow.Cells["Col_IfSaleDisc"].Value.ToString();
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = productRow.Cells["Col_ProductName"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value = productRow.Cells["Col_UOM"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Pack"].Value = productRow.Cells["Col_Pack"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = productRow.Cells["Col_ProdCompShortName"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Shelf"].Value = productRow.Cells["Col_Shelf"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = Convert.ToDouble(productRow.Cells["Col_VATPer"].Value.ToString()).ToString("#0.00");
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_LastStockID"].Value = productRow.Cells["Col_LastStockID"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_IfSaleDisc"].Value = mifsalediscount.ToString();

                    if (productRow.Cells["Col_LastStockID"].Value != null)
                        mlastsalestockid = productRow.Cells["Col_LastStockID"].Value.ToString();
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value = productRow.Cells["Col_ClosingStock"].Value;

                    mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].ReadOnly = true;
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                        int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);

                    int currentrow = mpPVC1.MainDataGridCurrentRow.Index;
                    int totproductsale = 0;
                    int saleqty = 0;

                  // int tempstock = 0;

                    foreach (DataGridViewRow dr in mpPVC1.Rows)
                    {
                        if (dr.Index != currentrow && dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                        {
                            if (dr.Cells["Col_Quantity"].Value != null)
                                int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out saleqty);
                            totproductsale += saleqty;

                        }
                    }
                 //   mclstk = mclstk + mqty;
                    if (mclstk == 0 && mifshortlisted != "N" && mqty == 0)
                    {
                        lblMessage.Text = "No Stock";
                        FillDailyShortList();
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = null;
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value = null;
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Pack"].Value = null;
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = null;
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Shelf"].Value = null;
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = null;
                        mpPVC1.MainDataGridCurrentRow.Cells[0].Value = null;
                        mpPVC1.RefreshMe();
                        mpPVC1.SetFocus(1);
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
                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = false;
                        mpPVC1.SetFocus(11);
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }


        private void mpPVC1_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                   
                    txtsavecustno.Text = "";
                    txtNetAmount.Text = "0.00";
                    txtBillAmount.Text = "0.00";
                    txtBillAmount2.Text = "0.00";
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value == null)
                    {
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value = _lastCustIdSelected;
                        if (_lastCustIdSelected == "1")
                            mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn1.BackColor;
                        else if (_lastCustIdSelected == "2")
                            mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn2.BackColor;
                        else if (_lastCustIdSelected == "3")
                            mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn3.BackColor;
                        else if (_lastCustIdSelected == "4")
                            mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn4.BackColor;
                        else if (_lastCustIdSelected == "5")
                            mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn5.BackColor;
                        else
                            mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn6.BackColor;
                    }
                }

                if (e.ColumnIndex == 11) // Quantity
                {

                    if (e.RowIndex >= 0)
                    {
                        int mbatchstock = 0;
                        string mprodno = "";
                        string mbtno = "";
                        string mrp = "";
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                            mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                            mbtno = mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString();
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                            mrp = mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString();
                        _lastProductID = mprodno;
                        int currentrow = e.RowIndex;
                        int totbatchsale = 0;
                        int totproductsale = 0;
                        int saleqty = 0;

                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value != null)
                            int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value.ToString().Trim(), out mbatchstock);

                        foreach (DataGridViewRow dr in mpPVC1.Rows)
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
                        //   lblMessage.Text = "BatchStock=" + Convert.ToString(mbatchstock - totbatchsale);
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
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);

                mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = batchRow.Cells["Col_Batchno"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = mexpiry;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value = mmrpn.ToString("#0.00");
                mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = msalerate.ToString("#0.00");
                mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value = mlastsalestockid;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_PurchaseRate"].Value = mpurrate.ToString("#0.00");
                mpPVC1.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value = mtraderate.ToString("#0.00");

                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value != null)
                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = batchRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value = batchRow.Cells["Col_ClosingStock"].Value.ToString().Trim();
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].ReadOnly = true;
                string mdt = DateTime.Today.Date.ToString("yyyyMMdd");
              
                if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                {
                    lblMessage.Text = "Expired Product";
                    mpPVC1.Rows.Remove(mpPVC1.MainDataGridCurrentRow);                
                    bool ifblank = false;
                    int currentindex = 0;
                    foreach (DataGridViewRow dr in mpPVC1.Rows)
                    {
                        currentindex = dr.Index;
                        if (dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == "")
                            ifblank = true;

                    }
                    if (ifblank == false)
                    {
                        int mindex = mpPVC1.Rows.Add();
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value = _lastCustIdSelected;
                        mpPVC1.SetFocus(mindex, 1);
                    }
                    else
                        mpPVC1.SetFocus(currentindex, 1);
                }
                else
                {
                    lblMessage.Text = "";
                    int currentrow = mpPVC1.MainDataGridCurrentRow.Index;
                    int totbatchsale = 0;
                    int totproductsale = 0;
                    int saleqty = 0;
                 //   int tempproductstock = 0;
               //     int tempbatchstock = 0;

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

                    mclstk = mclstk + mqty;

               //     mclosingstock = mclosingstock + tempbatchstock - totbatchsale;



                    lblMessage.Text = "Product Stock :" + mclstk.ToString().Trim() + " : Batch Stock :" + mclosingstock.ToString().Trim();
                    _SSSale.CurrentProductStock = mclstk;
                    _SSSale.CurrentBatchStock = mclosingstock;

                    if (_SSSale.CurrentBatchStock <= 0)
                    {
                        lblMessage.Text = "Batch Stock Zero";
                        mpPVC1.SetFocus(1);
                    }
                    else
                    {                       
                            mpPVC1.SetFocus(11);
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
            try
            {
                DataGridViewRow deletedrow = (DataGridViewRow)sender;
                int deletedrowindex = deletedrow.Index;
                CalculateAmount(deletedrowindex);
                lblMessage.Text = "";                            
                //foreach (DataGridViewRow dr in mpPVC1.Rows)
                //{
                //    if (dr.Cells["Col_ProductID"].Value == null || dr.Cells["Col_ProductID"].Value.ToString() == "")
                //    {                       
                //        break;
                //    }
                //}
                //if (ifblankrow == "N")
                //    mpPVC1.Rows.Add();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            if (mpPVC1.Rows[0].Cells["Col_ProductID"].Value != null)
            {
                try
                {
                    pnlFinal.Enabled = true;
                    pnlPatientDrDetails.Enabled = true;
                    pnlBillAmount.Enabled = true;
                    btnDelete.Enabled = true;
                    btnView.Enabled = true;
                    txtsavecustno.Text = _lastCustIdSelected.ToString();
                    txtsavecustno.Focus();
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
        }
        private void CalculateAmount(int deletedrowindex)
        {
            lblMessage.Text = "";
            double TotalAmount1 = 0;
            double TotalAmount2 = 0;
            double TotalAmount3 = 0;
            double TotalAmount4 = 0;
            double TotalAmount5 = 0;
            double TotalAmount6 = 0;
            double TotalAmountforDiscount1 = 0;
            double TotalAmountforDiscount2 = 0;
            double TotalAmountforDiscount3 = 0;
            double TotalAmountforDiscount4 = 0;
            double TotalAmountforDiscount5 = 0;
            double TotalAmountforDiscount6 = 0;
            double TotalAmountVAT51 = 0;
            double TotalAmountVAT52 = 0;
            double TotalAmountVAT53 = 0;
            double TotalAmountVAT54 = 0;
            double TotalAmountVAT55 = 0;
            double TotalAmountVAT56 = 0;
            double TotalAmountVAT121 = 0;
            double TotalAmountVAT122 = 0;
            double TotalAmountVAT123 = 0;
            double TotalAmountVAT124 = 0;
            double TotalAmountVAT125 = 0;
            double TotalAmountVAT126 = 0;

            double mvatper = 0;
            double mvatamount5 = 0;
            double mvatamount12point5 = 0;
            double mtotamtvat0 = 0;

            double mvatamount51 = 0;
            double mvatamount12point51 = 0;
            double mtotamtvat01 = 0;
            double mvatamount52 = 0;
            double mvatamount12point52 = 0;
            double mtotamtvat02 = 0;
            double mvatamount53 = 0;
            double mvatamount12point53 = 0;
            double mtotamtvat03 = 0;
            double mvatamount54 = 0;
            double mvatamount12point54 = 0;
            double mtotamtvat04 = 0;
            double mvatamount55 = 0;
            double mvatamount12point55 = 0;
            double mtotamtvat05 = 0;
            double mvatamount56 = 0;
            double mvatamount12point56 = 0;
            double mtotamtvat06 = 0;

            double mrate = 0;
            double mamt = 0;
            double mpakn = 0;
            double mqty = 0;
            int itemCount1 = 0;
            int itemCount2 = 0;
            int itemCount3 = 0;
            int itemCount4 = 0;
            int itemCount5 = 0;
            int itemCount6 = 0;
            string mcustid = "";
            string ifdiscount = "Y";
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    mvatamount5 = 0;
                    mvatamount12point5 = 0;
                    mtotamtvat0 = 0;

                     if (dr.Index != deletedrowindex)
                    {
                        if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "0" && dr.Cells["Col_Quantity"].Value.ToString() != "")
                        {
                            ifdiscount = "Y";
                            mcustid = "";
                            if (dr.Cells["Col_CustID"].Value != null)
                                mcustid = dr.Cells["Col_CustID"].Value.ToString();
                            mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                            mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                            mpakn = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());
                            if (dr.Cells["Col_IfSaleDisc"].Value != null && dr.Cells["Col_IfSaleDisc"].Value.ToString() != "")
                                ifdiscount = dr.Cells["Col_IFSaleDisc"].Value.ToString();
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
                                    mvatamount5 = Math.Round((mamt) / (100 / mvatper), 4);
                                else if (mvatper == 12.5)
                                {
                                    mvatamount12point5 = Math.Round((mamt) / (100 / mvatper), 4);
                                }
                                else
                                    mtotamtvat0 += mamt;
                                dr.Cells["Col_VATAmount"].Value = mvatamount12point5 + mvatamount5;
                                if (mcustid == "1")
                                {
                                    TotalAmount1 += mamt;
                                    itemCount1 += 1;
                                    if (ifdiscount != "N")
                                        TotalAmountforDiscount1 += mamt;
                                    mvatamount51 += mvatamount5;
                                    mvatamount12point51 += mvatamount12point5;
                                    mtotamtvat01 += mtotamtvat0;
                                    if (mvatper == 5)
                                        TotalAmountVAT51 += mamt;
                                    else if (mvatper == 12.5)
                                        TotalAmountVAT121 += mamt;
                                }
                                else if (mcustid == "2")
                                {
                                    TotalAmount2 += mamt;
                                    itemCount2 += 1;
                                    if (ifdiscount != "N")
                                        TotalAmountforDiscount2 += mamt;
                                    mvatamount52 += mvatamount5;
                                    mvatamount12point52 += mvatamount12point5;
                                    mtotamtvat02 += mtotamtvat0;
                                    if (mvatper == 5)
                                        TotalAmountVAT52 += mamt;
                                    else if (mvatper == 12.5)
                                        TotalAmountVAT122 += mamt;
                                }
                                else if (mcustid == "3")
                                {
                                    TotalAmount3 += mamt;
                                    itemCount3 += 1;
                                    if (ifdiscount != "N")
                                        TotalAmountforDiscount3 += mamt;
                                    mvatamount53 += mvatamount5;
                                    mvatamount12point53 += mvatamount12point5;
                                    mtotamtvat03 += mtotamtvat0;
                                    if (mvatper == 5)
                                        TotalAmountVAT53 += mamt;
                                    else if (mvatper == 12.5)
                                        TotalAmountVAT123 += mamt;
                                }
                                else if (mcustid == "4")
                                {
                                    TotalAmount4 += mamt;
                                    itemCount4 += 1;
                                    if (ifdiscount != "N")
                                        TotalAmountforDiscount4 += mamt;
                                    mvatamount54 += mvatamount5;
                                    mvatamount12point54 += mvatamount12point5;
                                    mtotamtvat04 += mtotamtvat0;
                                    if (mvatper == 5)
                                        TotalAmountVAT54 += mamt;
                                    else if (mvatper == 12.5)
                                        TotalAmountVAT124 += mamt;
                                }
                                else if (mcustid == "5")
                                {
                                    TotalAmount5 += mamt;
                                    itemCount5 += 1;
                                    if (ifdiscount != "N")
                                        TotalAmountforDiscount5 += mamt;
                                    mvatamount55 += mvatamount5;
                                    mvatamount12point55 += mvatamount12point5;
                                    mtotamtvat05 += mtotamtvat0;
                                    if (mvatper == 5)
                                        TotalAmountVAT55 += mamt;
                                    else if (mvatper == 12.5)
                                        TotalAmountVAT125 += mamt;
                                }
                                else if (mcustid == "6")
                                {
                                    TotalAmount6 += mamt;
                                    itemCount6 += 1;
                                    if (ifdiscount != "N")
                                        TotalAmountforDiscount6 += mamt;
                                    mvatamount56 += mvatamount5;
                                    mvatamount12point56 += mvatamount12point5;
                                    mtotamtvat06 += mtotamtvat0;
                                    if (mvatper == 5)
                                        TotalAmountVAT56 += mamt;
                                    else if (mvatper == 12.5)
                                        TotalAmountVAT126 += mamt;
                                }
                            }
                        }
                    }
                }
                NoofRows();
                txtamount1.Text = TotalAmount1.ToString("#0.00");
                txtitems1.Text = itemCount1.ToString();
                txtVatInput12point51.Text = mvatamount12point51.ToString("#0.00");
                txtVatInput51.Text = mvatamount51.ToString("0.00");
                txtSaleAmountForDiscount1.Text = TotalAmountforDiscount1.ToString("0.00");
                txtAmountforZeroVAT1.Text = mtotamtvat01.ToString("0.00");
                txtVATAmount51.Text = TotalAmountVAT51.ToString("#0.00");
                txtVATAmount12point51.Text = TotalAmountVAT121.ToString("#0.00");

                txtamount2.Text = TotalAmount2.ToString("#0.00");
                txtitems2.Text = itemCount2.ToString();
                txtVatInput12point52.Text = mvatamount12point52.ToString("#0.00");
                txtVatInput52.Text = mvatamount52.ToString("0.00");
                txtSaleAmountForDiscount2.Text = TotalAmountforDiscount2.ToString("0.00");
                txtAmountforZeroVAT2.Text = mtotamtvat02.ToString("0.00");
                txtVATAmount52.Text = TotalAmountVAT52.ToString("#0.00");
                txtVATAmount12point52.Text = TotalAmountVAT122.ToString("#0.00");

                txtamount3.Text = TotalAmount3.ToString("#0.00");
                txtitems3.Text = itemCount3.ToString();
                txtVatInput12point53.Text = mvatamount12point53.ToString("#0.00");
                txtVatInput53.Text = mvatamount53.ToString("0.00");
                txtSaleAmountForDiscount3.Text = TotalAmountforDiscount3.ToString("0.00");
                txtAmountforZeroVAT3.Text = mtotamtvat03.ToString("0.00");
                txtVATAmount53.Text = TotalAmountVAT53.ToString("#0.00");
                txtVATAmount12point53.Text = TotalAmountVAT123.ToString("#0.00");

                txtamount4.Text = TotalAmount4.ToString("#0.00");
                txtitems4.Text = itemCount4.ToString();
                txtVatInput12point54.Text = mvatamount12point54.ToString("#0.00");
                txtVatInput54.Text = mvatamount54.ToString("0.00");
                txtSaleAmountForDiscount4.Text = TotalAmountforDiscount4.ToString("0.00");
                txtAmountforZeroVAT4.Text = mtotamtvat04.ToString("0.00");
                txtVATAmount54.Text = TotalAmountVAT54.ToString("#0.00");
                txtVATAmount12point54.Text = TotalAmountVAT124.ToString("#0.00");

                txtamount5.Text = TotalAmount5.ToString("#0.00");
                txtitems5.Text = itemCount5.ToString();
                txtVatInput12point55.Text = mvatamount12point55.ToString("#0.00");
                txtVatInput55.Text = mvatamount55.ToString("0.00");
                txtSaleAmountForDiscount5.Text = TotalAmountforDiscount5.ToString("0.00");
                txtAmountforZeroVAT5.Text = mtotamtvat05.ToString("0.00");
                txtVATAmount55.Text = TotalAmountVAT55.ToString("#0.00");
                txtVATAmount12point55.Text = TotalAmountVAT125.ToString("#0.00");

                txtamount6.Text = TotalAmount6.ToString("#0.00");
                txtitems6.Text = itemCount6.ToString();
                txtVatInput12point56.Text = mvatamount12point56.ToString("#0.00");
                txtVatInput56.Text = mvatamount56.ToString("0.00");
                txtSaleAmountForDiscount6.Text = TotalAmountforDiscount6.ToString("0.00");
                txtAmountforZeroVAT6.Text = mtotamtvat06.ToString("0.00");
                txtVATAmount56.Text = TotalAmountVAT56.ToString("#0.00");
                txtVATAmount12point56.Text = TotalAmountVAT126.ToString("#0.00");
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }
        private void CalculateAllAmounts()
        {
            lblMessage.Text = "";
            double mdblAmount;
            double maddon = 0;
            double mtotalafterdiscount = 0;
            double.TryParse(txtNetAmount.Text.ToString(), out mdblAmount);
            double mdblAmountforDiscount;
            double.TryParse(txtSaleAmountForDiscount1.Text.ToString(), out mdblAmountforDiscount);
            double mdblDiscPer;
            double.TryParse(txtDiscPercent.Text.ToString(), out mdblDiscPer);
            double mdblDiscAmount;
            double.TryParse(txtDiscAmount.Text.ToString(), out mdblDiscAmount);
            try
            {
                mdblDiscAmount = Math.Round(((mdblAmountforDiscount) * mdblDiscPer / 100), 2);
                txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                mtotalafterdiscount = Math.Round(mdblAmount - mdblDiscAmount + maddon, 2);
                txtTotalafterdiscount.Text = mtotalafterdiscount.ToString("#0.00");
               
                if (cbRound.Checked == true)
                {
                    txtRoundAmount.Text = Math.Round(Math.Round(mtotalafterdiscount, 0) - Math.Round(mtotalafterdiscount, 2),2).ToString("#0.00");
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                    txtBillAmount2.Text = txtBillAmount.Text;
                }
                else
                {
                    txtRoundAmount.Text = "0.00";
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                    txtBillAmount2.Text = txtBillAmount.Text;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
                dr = drprod.ReadLastSaleByID(mpPVC1.MainDataGridCurrentRow.Cells[0].Value.ToString());
                mprodno = mpPVC1.MainDataGridCurrentRow.Cells[0].Value.ToString();
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
                txtAmountforZeroVAT1.Text = "0";
                txtAmountforZeroVAT2.Text = "0";
                txtAmountforZeroVAT3.Text = "0";
                txtAmountforZeroVAT4.Text = "0";
                txtAmountforZeroVAT5.Text = "0";
                txtAmountforZeroVAT6.Text = "0";

                txtAmountfor5VAT.Text = "0";
                txtVATAmount51.Text = "0";
                txtVATAmount52.Text = "0";
                txtVATAmount53.Text = "0";
                txtVATAmount54.Text = "0";
                txtVATAmount55.Text = "0";
                txtVATAmount56.Text = "0";

                txtAmountfor12VAT.Text = "0";
                txtVATAmount12point51.Text = "0";
                txtVATAmount12point52.Text = "0";
                txtVATAmount12point53.Text = "0";
                txtVATAmount12point54.Text = "0";
                txtVATAmount12point55.Text = "0";
                txtVATAmount12point56.Text = "0";

                txtVatInput12point5.Text = "0";
                txtVatInput12point51.Text = "0";
                txtVatInput12point52.Text = "0";
                txtVatInput12point53.Text = "0";
                txtVatInput12point54.Text = "0";
                txtVatInput12point55.Text = "0";
                txtVatInput12point56.Text = "0";

                txtVatInput5.Text = "0";
                txtVatInput51.Text = "0";
                txtVatInput52.Text = "0";
                txtVatInput53.Text = "0";
                txtVatInput54.Text = "0";
                txtVatInput55.Text = "0";
                txtVatInput56.Text = "0";

                txtSaleAmountForDiscount.Text = "0";
                txtSaleAmountForDiscount1.Text = "0";
                txtSaleAmountForDiscount2.Text = "0";
                txtSaleAmountForDiscount3.Text = "0";
                txtSaleAmountForDiscount4.Text = "0";
                txtSaleAmountForDiscount5.Text = "0";
                txtSaleAmountForDiscount6.Text = "0";

                txtDiscPercent.Text = "0";
                btnView.Text = "&View";
                _lastCustIdSelected = "1";
                btn1.ForeColor = System.Drawing.Color.Red;

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
                txtOperator.Text = "";
                txtNoOfRows.Text = "";
                txtPatientNameAddress.Text = "";
                txtMobileNumber.Text = "";
                //if (General.CurrentSetting.MsetSaleCreditSale == "Y")
                //    rbtCreditStatement.Visible = true;
                //else
                //    rbtCreditStatement.Visible = false;
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
                    txtAmountforZeroVAT1.Text = "0";
                    txtVatInput12point51.Text = "0";
                    txtVatInput51.Text = "0";
                    txtSaleAmountForDiscount1.Text = "0";
                    txtVATAmount51.Text = "0";
                    txtVATAmount12point51.Text = "0";
                }

                if (_SSSale.CustNumber == 2)
                {
                    txtamount2.Text = "0.00";
                    txtitems2.Text = "0";
                    txtAmountforZeroVAT2.Text = "0";
                    txtVatInput12point52.Text = "0";
                    txtVatInput52.Text = "0";
                    txtSaleAmountForDiscount2.Text = "0";
                    txtVATAmount52.Text = "0";
                    txtVATAmount12point52.Text = "0";
                }
                if (_SSSale.CustNumber == 3)
                {
                    txtamount3.Text = "0.00";
                    txtitems3.Text = "0";
                    txtAmountforZeroVAT3.Text = "0";
                    txtVatInput12point53.Text = "0";
                    txtVatInput53.Text = "0";
                    txtSaleAmountForDiscount3.Text = "0";
                    txtVATAmount53.Text = "0";
                    txtVATAmount12point53.Text = "0";
                }

                if (_SSSale.CustNumber == 4)
                {
                    txtamount4.Text = "0.00";
                    txtitems4.Text = "0";
                    txtAmountforZeroVAT4.Text = "0";
                    txtVatInput12point54.Text = "0";
                    txtVatInput54.Text = "0";
                    txtSaleAmountForDiscount4.Text = "0";
                    txtVATAmount54.Text = "0";
                    txtVATAmount12point54.Text = "0";
                }
                if (_SSSale.CustNumber == 5)
                {
                    txtamount5.Text = "0.00";
                    txtitems5.Text = "0";
                    txtAmountforZeroVAT5.Text = "0";
                    txtVatInput12point55.Text = "0";
                    txtVatInput55.Text = "0";
                    txtSaleAmountForDiscount5.Text = "0";
                    txtVATAmount55.Text = "0";
                    txtVATAmount12point55.Text = "0";
                }

                if (_SSSale.CustNumber == 6)
                {
                    txtamount6.Text = "0.00";
                    txtitems6.Text = "0";
                    txtAmountforZeroVAT6.Text = "0";
                    txtVatInput12point56.Text = "0";
                    txtVatInput56.Text = "0";
                    txtSaleAmountForDiscount6.Text = "0";
                    txtVATAmount56.Text = "0";
                    txtVATAmount12point56.Text = "0";
                }


                btnView.Text = "&View";
                txtVouchernumber.Clear();
                txtVouType.Text = "   ";
                datePickerBillDate.ResetText();
                txtBillAmount.Text = "0.00";
                txtBillAmount2.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtAmountforZeroVAT.Text = "0";
                txtVatInput12point5.Text = "0";
                txtVatInput5.Text = "0";
                txtAmountfor5VAT.Text = "0";
                txtAmountfor12VAT.Text = "0";
                txtRoundAmount.Text = "0.00";
                txtSaleAmountForDiscount.Text = "0";
                mcbDoctor.SelectedID = "";
                txtPatientName.SelectedID = "";
                _SSSale.CustNumber = 0;
                _SSSale.PatientID = "";
                _SSSale.AccountID = "";
                _SSSale.CrdbVouType = "";
                _SSSale.SaleSubType = "";
                txtAddress.SelectedID = "";
                txtPatientName.Text = "";
                txtAddress.Text = "";
                mcbDoctor.Text = "";
                txtPatientNameAddress.Text = "";
                _lastCustIdSelected = "1";
                // error 
                if (mpPVC1.Rows.Count > 1)
                {
                    _lastCustIdSelected = mpPVC1.Rows[mpPVC1.Rows.Count - 2].Cells["Col_CustID"].Value.ToString();

                }
                if (mpPVC1.Rows.Count >= 1)
                {

                    mpPVC1.Rows[mpPVC1.Rows.Count - 1].DefaultCellStyle.BackColor = GetBackColorByCustID(_lastCustIdSelected);
                    mpPVC1.Rows[mpPVC1.Rows.Count - 1].Cells["Col_CustID"].Value = _lastCustIdSelected;
                }

                mpPVC1.SetFocus(1);
                txtsavecustno.Text = "";
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

        #endregion

        #region "Events"

        private void btnView_Click(object sender, EventArgs e)
        {
            btnViewClick();
        }
        private void btnViewClick()
        {
            bool Visibility = true;

            txtNetAmount.Text = "0.00";
            txtBillAmount.Text = "0.00";
            txtBillAmount2.Text = "0.00";

            try
            {
                if (btnView.Text == "&View" || btnView.Text == "View")
                {
                    if (txtsavecustno.Text != null && txtsavecustno.Text.ToString() != "")
                    {
                        Visibility = false;
                        btnView.Text = "Sho&w All";
                        mpPVC1.IsAllowNewRow = false;
                        foreach (DataGridViewRow dr in mpPVC1.Rows)
                        {
                            if (dr.Cells["Col_CustID"].Value != null &&
                                    dr.Cells["Col_CustID"].Value.ToString() != txtsavecustno.Text)
                            {
                                dr.Visible = Visibility;

                            }
                        }
                    }
                }
                else if (btnView.Text.ToLower() == "sho&w all")
                {
                    Visibility = true;
                    btnView.Text = "&View";
                    mpPVC1.IsAllowNewRow = true;
                    foreach (DataGridViewRow dr in mpPVC1.Rows)
                    {
                        dr.Visible = true;
                    }
                    mpPVC1.SetFocus(1);
                }
                //////////////////  txtsavecustno.Text = "";
                if (btnView.Text == "&View" || btnView.Text == "View")
                {
                    btnView.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);

            }
        }

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
                    foreach (DataGridViewRow dr in mpPVC1.Rows)
                    {
                        if (dr.Cells["Col_CustID"].Value != null)
                        {
                            if (dr.Cells["Col_CustID"].Value.ToString() != txtsavecustno.Text.ToString())
                            {
                                dr.Visible = true;
                            }
                            else
                                rowCollection.Add(dr);
                        }
                    }
                    foreach (DataGridViewRow prodrow in rowCollection)
                    {
                        mpPVC1.Rows.Remove(prodrow);
                    }

                    rowCollection = new List<DataGridViewRow>();
                    //for (i = 0; i < mpPVC1.Rows.Count; i++)
                    //{
                    //    if (mpPVC1.Rows[i].Cells["Col_CustID"].Value != null)
                    //    {
                    //        if (mpPVC1.Rows[i].Cells["Col_CustID"].Value.ToString() == txtsavecustno.Text)
                    //        {
                    //            mpPVC1.Rows.Remove(mpPVC1.Rows[i]);
                    //            i = i - 1;
                    //        }
                    //        else
                    //        {
                    //            _lastCustIdSelected = mpPVC1.Rows[i].Cells["Col_CustID"].Value.ToString();
                    //            lastrowindex = mpPVC1.Rows[i].Index;
                    //        }

                    //    }
                    //}
                  
                    if (_lastCustIdSelected == "1")
                    {
                        btn1.ForeColor = System.Drawing.Color.Red;
                        btn2.ForeColor = System.Drawing.Color.Black;
                        btn3.ForeColor = System.Drawing.Color.Black;
                        btn4.ForeColor = System.Drawing.Color.Black;
                        btn5.ForeColor = System.Drawing.Color.Black;
                        btn6.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (_lastCustIdSelected == "2")
                    {
                        btn1.ForeColor = System.Drawing.Color.Black;
                        btn2.ForeColor = System.Drawing.Color.Red;
                        btn3.ForeColor = System.Drawing.Color.Black;
                        btn4.ForeColor = System.Drawing.Color.Black;
                        btn5.ForeColor = System.Drawing.Color.Black;
                        btn6.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (_lastCustIdSelected == "3")
                    {
                        btn1.ForeColor = System.Drawing.Color.Black;
                        btn2.ForeColor = System.Drawing.Color.Black;
                        btn3.ForeColor = System.Drawing.Color.Red;
                        btn4.ForeColor = System.Drawing.Color.Black;
                        btn5.ForeColor = System.Drawing.Color.Black;
                        btn6.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (_lastCustIdSelected == "4")
                    {
                        btn1.ForeColor = System.Drawing.Color.Black;
                        btn2.ForeColor = System.Drawing.Color.Black;
                        btn3.ForeColor = System.Drawing.Color.Black;
                        btn4.ForeColor = System.Drawing.Color.Red;
                        btn5.ForeColor = System.Drawing.Color.Black;
                        btn6.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (_lastCustIdSelected == "5")
                    {
                        btn1.ForeColor = System.Drawing.Color.Black;
                        btn2.ForeColor = System.Drawing.Color.Black;
                        btn3.ForeColor = System.Drawing.Color.Black;
                        btn4.ForeColor = System.Drawing.Color.Black;
                        btn5.ForeColor = System.Drawing.Color.Red;
                        btn6.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (_lastCustIdSelected == "6")
                    {
                        btn1.ForeColor = System.Drawing.Color.Black;
                        btn2.ForeColor = System.Drawing.Color.Black;
                        btn3.ForeColor = System.Drawing.Color.Black;
                        btn4.ForeColor = System.Drawing.Color.Black;
                        btn5.ForeColor = System.Drawing.Color.Black;
                        btn6.ForeColor = System.Drawing.Color.Red;
                    }
                }
                txtsavecustno.Text = "";
                CalculateAmount(-1);
                mpPVC1.SetFocus(lastrowindex, 1);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        //private void rbtCash_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            if (rbtCash.Checked)
        //            {
        //                txtAddress.Enabled = true;
        //                txtPatientName.Focus();
        //            }
        //        }
        //        if (e.KeyCode == Keys.Right)
        //            rbtVoucherSale.Focus();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        //private void rbtCashCredit_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            if (rbtCashCredit.Checked)
        //            {
        //                txtAddress.Enabled = true;
        //                txtPatientName.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}



        //private void rbtVoucherSale_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            if (rbtVoucherSale.Checked)
        //            {
        //                txtAddress.Enabled = true;
        //                txtPatientName.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        //private void rbtCreditCard_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            if (rbtCreditCard.Checked)
        //            {
        //                txtAddress.Enabled = true;
        //                txtPatientName.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}


        private void btnPrint_Click(object sender, EventArgs e)
        {
            //          PrintBill();
        }
        #endregion


        #region Construct columns

        private void ConstructMainColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC1.ColumnsMain.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 220;
                column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                mpPVC1.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 50;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                mpPVC1.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //8          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);
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
                mpPVC1.ColumnsMain.Add(column);
                //13            // temp storage columns 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //14         
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //20
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
                column.Name = "Col_CustID";
                //  column.DataPropertyName = "StockID";
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
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }

        private void ConstructProductSelectionListGridColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC1.ColumnsProductList.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 250;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsProductList.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 80;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "ShelfID";
                column.Width = 60;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
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
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsProductList.Add(column);
                //8
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

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "ProdLastSaleStockID";
                column.HeaderText = "laststockid";
                column.Width = 30;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void ConstructBatchGridColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC1.ColumnsBatchList.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batchno";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batchno";
                column.Width = 130;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 70;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPercent";
                column.DataPropertyName = "ProductVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);

                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                mpPVC1.ColumnsBatchList.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "Sale Rate";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur Rate";
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
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

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
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }

        #endregion

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
            _lastCustIdSelected = "1";
            try
            {
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value == null)
                {
                    btn1.ForeColor = System.Drawing.Color.Red;
                    btn2.ForeColor = System.Drawing.Color.Black;
                    btn3.ForeColor = System.Drawing.Color.Black;
                    btn4.ForeColor = System.Drawing.Color.Black;
                    btn5.ForeColor = System.Drawing.Color.Black;
                    btn6.ForeColor = System.Drawing.Color.Black;
                    mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn1.BackColor;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value = _lastCustIdSelected;
                }
                else
                    _lastCustIdSelected = mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value.ToString();
                mpPVC1.SetFocus(1);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
            _lastCustIdSelected = "2";
            try
            {
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value == null)
                {
                    btn1.ForeColor = System.Drawing.Color.Black;
                    btn2.ForeColor = System.Drawing.Color.Red;
                    btn3.ForeColor = System.Drawing.Color.Black;
                    btn4.ForeColor = System.Drawing.Color.Black;
                    btn5.ForeColor = System.Drawing.Color.Black;
                    btn6.ForeColor = System.Drawing.Color.Black;

                    mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn2.BackColor;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value = _lastCustIdSelected;
                }
                else
                    _lastCustIdSelected = mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value.ToString();
                mpPVC1.SetFocus(1);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
            _lastCustIdSelected = "3";
            try
            {
                btn1.ForeColor = System.Drawing.Color.Black;
                btn2.ForeColor = System.Drawing.Color.Black;
                btn3.ForeColor = System.Drawing.Color.Red;
                btn4.ForeColor = System.Drawing.Color.Black;
                btn5.ForeColor = System.Drawing.Color.Black;
                btn6.ForeColor = System.Drawing.Color.Black;
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value == null)
                {
                    mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn3.BackColor;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value = _lastCustIdSelected;
                }
                mpPVC1.SetFocus(1);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
            _lastCustIdSelected = "4";
            try
            {
                btn1.ForeColor = System.Drawing.Color.Black;
                btn2.ForeColor = System.Drawing.Color.Black;
                btn3.ForeColor = System.Drawing.Color.Black;
                btn4.ForeColor = System.Drawing.Color.Red;
                btn5.ForeColor = System.Drawing.Color.Black;
                btn6.ForeColor = System.Drawing.Color.Black;
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value == null)
                {
                    mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn4.BackColor;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value = _lastCustIdSelected;
                }
                mpPVC1.SetFocus(1);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
            _lastCustIdSelected = "5";
            try
            {
                btn1.ForeColor = System.Drawing.Color.Black;
                btn2.ForeColor = System.Drawing.Color.Black;
                btn3.ForeColor = System.Drawing.Color.Black;
                btn4.ForeColor = System.Drawing.Color.Black;
                btn5.ForeColor = System.Drawing.Color.Red;
                btn6.ForeColor = System.Drawing.Color.Black;
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value == null)
                {
                    mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn5.BackColor;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value = _lastCustIdSelected;
                }
                mpPVC1.SetFocus(1);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
            _lastCustIdSelected = "6";
            try
            {
                btn1.ForeColor = System.Drawing.Color.Black;
                btn2.ForeColor = System.Drawing.Color.Black;
                btn3.ForeColor = System.Drawing.Color.Black;
                btn4.ForeColor = System.Drawing.Color.Black;
                btn5.ForeColor = System.Drawing.Color.Black;
                btn6.ForeColor = System.Drawing.Color.Red;
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value == null)
                {
                    mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn6.BackColor;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value = _lastCustIdSelected;
                }
                mpPVC1.SetFocus(1);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void txtsavecustno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtSaveCustNoTextChanged();
            }
            else if (e.KeyCode == Keys.Escape)
                mpPVC1.SetFocus(1);
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
                if (txtsavecustno.Text != null &&  txtsavecustno.Text.ToString() != "")
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

                    txtVatInput5.Text = txtVatInput51.Text.ToString();
                    txtVatInput12point5.Text = txtVatInput12point51.Text.ToString();
                    txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount1.Text.ToString();
                    txtAmountforZeroVAT.Text = txtAmountforZeroVAT1.Text.ToString();
                    txtAmountfor5VAT.Text = txtVATAmount51.Text.ToString();
                    txtAmountfor12VAT.Text = txtVATAmount12point51.Text.ToString();
                    CalculateAllAmounts();

                 //   gbsaletype.Enabled = true;                   
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    txtPatientNameAddress.Enabled = true;
                    cbTransactionType.Focus();

                }
                else if (msavenumber == 2 && mamt2 > 0)
                {
                    txtNetAmount.Text = mamt2.ToString("#0.00");

                    txtVatInput5.Text = txtVatInput52.Text.ToString();
                    txtVatInput12point5.Text = txtVatInput12point52.Text.ToString();
                    txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount2.Text.ToString();
                    txtAmountforZeroVAT.Text = txtAmountforZeroVAT2.Text.ToString();
                    txtAmountfor5VAT.Text = txtVATAmount52.Text.ToString();
                    txtAmountfor12VAT.Text = txtVATAmount12point52.Text.ToString();
                    CalculateAllAmounts();
                  //  gbsaletype.Enabled = true;                 
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    txtPatientNameAddress.Enabled = true;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 3 && mamt3 > 0)
                {
                    txtNetAmount.Text = mamt3.ToString("#0.00");

                    txtVatInput5.Text = txtVatInput53.Text.ToString();
                    txtVatInput12point5.Text = txtVatInput12point53.Text.ToString();
                    txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount3.Text.ToString();
                    txtAmountforZeroVAT.Text = txtAmountforZeroVAT3.Text.ToString();
                    txtAmountfor5VAT.Text = txtVATAmount53.Text.ToString();
                    txtAmountfor12VAT.Text = txtVATAmount12point53.Text.ToString();
                    CalculateAllAmounts();
                  //  gbsaletype.Enabled = true;                   
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    txtPatientNameAddress.Enabled = true;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 4 && mamt4 > 0)
                {
                    txtNetAmount.Text = mamt4.ToString("#0.00");

                    txtVatInput5.Text = txtVatInput54.Text.ToString();
                    txtVatInput12point5.Text = txtVatInput12point54.Text.ToString();
                    txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount4.Text.ToString();
                    txtAmountforZeroVAT.Text = txtAmountforZeroVAT4.Text.ToString();
                    txtAmountfor5VAT.Text = txtVATAmount54.Text.ToString();
                    txtAmountfor12VAT.Text = txtVATAmount12point54.Text.ToString();
                    CalculateAllAmounts();
                  //  gbsaletype.Enabled = true;                   
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    txtPatientNameAddress.Enabled = true;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 5 && mamt5 > 0)
                {
                    txtNetAmount.Text = mamt5.ToString("#0.00");

                    txtVatInput5.Text = txtVatInput55.Text.ToString();
                    txtVatInput12point5.Text = txtVatInput12point55.Text.ToString();
                    txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount5.Text.ToString();
                    txtAmountforZeroVAT.Text = txtAmountforZeroVAT5.Text.ToString();
                    txtAmountfor5VAT.Text = txtVATAmount55.Text.ToString();
                    txtAmountfor12VAT.Text = txtVATAmount12point55.Text.ToString();
                    CalculateAllAmounts();
                 //   gbsaletype.Enabled = true;                   
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    txtPatientNameAddress.Enabled = true;
                    cbTransactionType.Focus();
                }
                else if (msavenumber == 6 && mamt6 > 0)
                {
                    txtNetAmount.Text = mamt6.ToString("#0.00");

                    txtVatInput5.Text = txtVatInput56.Text.ToString();
                    txtVatInput12point5.Text = txtVatInput12point56.Text.ToString();
                    txtSaleAmountForDiscount.Text = txtSaleAmountForDiscount6.Text.ToString();
                    txtAmountforZeroVAT.Text = txtAmountforZeroVAT6.Text.ToString();
                    txtAmountfor5VAT.Text = txtVATAmount56.Text.ToString();
                    txtAmountfor12VAT.Text = txtVATAmount12point56.Text.ToString();
                    CalculateAllAmounts();
                //    gbsaletype.Enabled = true;
                   
                    txtPatientName.Enabled = true;
                    txtAddress.Enabled = true;
                    mcbDoctor.Enabled = true;
                    txtPatientNameAddress.Enabled = true;
                    cbTransactionType.Focus();
                }
                else
                {
                    if (msavenumber  > 0)
                    lblMessage.Text = "Wrong Number";
                    mpPVC1.SetFocus(1);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        //private void mcbAddress_EnterKeyPressed(object sender, EventArgs e)
        //{
        //    mcbDoctor.Focus();
        //}

        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID.ToString() != "")
            {              
                txtDoctorNameAddress.Text = mcbDoctor.SeletedItem.ItemData[3];
            }
            if (txtDiscPercent.Visible)
                txtDiscPercent.Focus();
            else
                if (txtOperator.Visible)
                    txtOperator.Focus();
                else
                    tsBtnSave.Select();
        }

        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CalculateAllAmounts();
                txtOperator.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
                mpPVC1.SetFocus(1);
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
                    txtPatientNameAddress.Text = txtPatientName.Text.Trim() + " " + txtAddress.Text.Trim();
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
                        txtPatientNameAddress.Text = txtPatientName.SeletedItem.ItemData[5];
                        mcbDoctor.SelectedID = txtPatientName.SeletedItem.ItemData[6];
                        _SSSale.AccCode = txtPatientName.SeletedItem.ItemData[1];

                        txtPatientName.Enabled = false;
                        txtAddress.Enabled = false;
                        if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "")
                            mcbDoctor.Enabled = false;
                        txtPatientNameAddress.Enabled = false;
                        if (General.CurrentSetting.MsetAskOperatorOtherThanVoucherSale == "Y")
                        {
                            if (txtOperator.Visible == false)
                            {
                                txtOperator.Visible = true;
                                lblOperator.Visible = true;
                            }
                        }

                        txtDiscPercent.Focus();
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
                if (txtPatientNameAddress.Text == null || txtPatientNameAddress.Text == "")
                    txtPatientNameAddress.Text = txtPatientName.Text.Trim() + " " + txtAddress.Text.Trim();
                txtMobileNumber.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        //private void rbtCreditStatement_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            if (rbtCreditStatement.Checked)
        //            {
        //                txtAddress.Enabled = true;
        //                txtPatientName.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}      

      

        private void uclSubstituteControl1_OnProductSelected(string productID)
        {
            mpPVC1.LoadProduct(productID);
            mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = productID;
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
                txtDoctorNameAddress.Text = mcbDoctor.SeletedItem.ItemData[3];
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbDoctor_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID.ToString() != "")
            {               
                txtDoctorNameAddress.Text = mcbDoctor.SeletedItem.ItemData[3];
            }
        }
        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {
            CalculateAllAmounts();
        }

        private void mpPVC1_OnSelectedProductClosingStock(int closingStockValue, string productID)
        {
            int mqty = 0;
         //   string preprodname = "";
            if (_Mode == OperationMode.Add)
            {
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);
                //if (mpPVC1.MainDataGridCurrentRow.Cells[1].Value != null)
                //    preprodname = mpPVC1.MainDataGridCurrentRow.Cells[1].Value.ToString();
                if (closingStockValue+mqty == 0)
                {
                    _SSSale.ProductID = productID;

                    if (_SSSale.IfAddToShortList())
                    {
                        Filldailyshortlist();
                    }
                    lblMessage.Text = "No Stock";
                    mpPVC1.SetFocus(mpPVC1.MainDataGridCurrentRow.Index, 1);
                }
                else
                {
                    lblMessage.Text = string.Format("Stock: {0}", closingStockValue+mqty);
                }
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

        private void txtMobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbDoctor.Focus();
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

        private void txtPatientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                cbTransactionType.Focus();
        }

       
     
    }
}