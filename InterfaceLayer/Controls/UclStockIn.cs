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
using PaperlessPharmaRetail.Common.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclStockIn : BaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private StockIn _StockIn;
        DataTable dtTempStockIn;
        #endregion

        #region constructor
        public UclStockIn()
        {
            try
            {
                InitializeComponent();
                _StockIn = new StockIn();
                SearchControl = new UclStockInSearch();
                CreateStockInDt();
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
                    mpPVC1.SetFocus(1);
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
                _StockIn.Initialise();
                ClearControls();
                //UpdateTempStockInDt();
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
                pnlVouTypeNo.Enabled = true;
                InitialisempPVC1();
                AddToolTip();
                headerLabel1.Text = "STOCK IN -> NEW";
                FillPartyCombo();
                mpPVC1.SetFocus(1);

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
                headerLabel1.Text = "STOCK IN -> EDIT";
                FillPartyCombo();
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
                pnlVouTypeNo.Enabled = true;
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
                headerLabel1.Text = "STOCK IN -> DELETE";
                FillPartyCombo();
                ClearData();
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
                if (_StockIn.Id != null && _StockIn.Id != "")
                {
                    bool canbedeleted = CheckStockForDelete();
                    if (canbedeleted)
                    {
                        LockTable.LockTablesForCreditDebitNoteStock();
                        General.BeginTransaction();
                        retValue = DeletePreviousRows();
                        if (retValue)
                            retValue = _StockIn.DeleteDetails();
                        if (retValue)
                            retValue = ReducePreviousStock();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                           // UpdateClosingStockinCache();
                            _SavedID = _StockIn.Id;
                            ClearControls();
                            MessageBox.Show("Voucher Deleted Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            retValue = true;
                        }
                        else
                        {
                            PSDialogResult result = PSMessageBox.Show("Could not Delete...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                            retValue = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                pnlVouTypeNo.Enabled = true;
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
                ClearData();
                headerLabel1.Text = "STOCK IN -> VIEW";
                FillPartyCombo();
                txtVouchernumber.Focus();
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
            if (txtBillAmount.Text != null && Convert.ToDouble(txtBillAmount.Text.ToString()) > 0)
            {
                if (General.CurrentSetting.MsetPrintSaleBill == "Y")
                    PrintCreditnNotePrePrintedPaper();
                else
                    PrintCreditnNotePlainPaper();
            }
            ClearData();
            return retValue;
        }
        private void PrintCreditnNotePrePrintedPaper()
        {
            EcoMart.Printing.PrePrintedPaperPrinter printer = new EcoMart.Printing.PrePrintedPaperPrinter();
            printer.Print(_StockIn.CrdbVouType, _StockIn.CrdbVouNo.ToString(), _StockIn.CrdbVouDate, "", _StockIn.CrdbAddress, "", "", "", mpPVC1.Rows, _StockIn.CrdbNarration, _StockIn.CrdbAmountNet, "", _StockIn.CrdbDiscAmt, 0, 0, _StockIn.CrdbAmount, 0 + _StockIn.CrdbAmountNet, "");

        }

        private void PrintCreditnNotePlainPaper()
        {
            EcoMart.Printing.PlainPaperPrinter printer = new EcoMart.Printing.PlainPaperPrinter();
            printer.Print(_StockIn.CrdbVouType, _StockIn.CrdbVouNo.ToString(), _StockIn.CrdbVouDate, "", _StockIn.CrdbAddress, "", "", "", mpPVC1.Rows, _StockIn.CrdbNarration, _StockIn.CrdbAmountNet, "", _StockIn.CrdbDiscAmt, 0, 0, _StockIn.CrdbAmount, 0 + _StockIn.CrdbAmountNet, "");

        }


        //public override bool Print()
        //{
        //    bool retValue = true;
        //    if (txtBillAmount.Text != null && Convert.ToDouble(txtBillAmount.Text.ToString()) > 0)
        //    PrintData();
        //    ClearData();
        //    return retValue;
        //}

        //private void PrintData()
        //{
        //    PrintRow row;
        //    try
        //    {
        //        PrintBill.Rows.Clear();
        //        Font fnt = new Font("Arial", 8, FontStyle.Regular);
        //        int totalrows = mpPVC1.Rows.Count;
        //        PrintPageNumber = 0;
        //        int rowcount = 0;
        //        PrintRowPixel = 0;
        //        double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
        //        int totalpages = Convert.ToInt32(totpages);
        //        PrintHeader(totalpages, rowcount, fnt);
        //        foreach (DataGridViewRow dr in mpPVC1.Rows)
        //        {

        //            if (dr.Cells["Col_ProductID"].Value != null)
        //            {
        //                if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
        //                {
        //                    PrintRowPixel = 325;
        //                    row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
        //                    PrintBill.Rows.Add(row);
        //                    PrintBill.Print_Bill();
        //                    PrintBill.Rows.Clear();
        //                    PrintRowPixel = 0;
        //                    PrintHeader(totalpages, rowcount, fnt);

        //                    rowcount = 0;
        //                }
        //                PrintRowPixel += 17;
        //                rowcount += 1;
        //                row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString(), PrintRowPixel, 15, fnt);
        //                PrintBill.Rows.Add(row);
        //                row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString(), PrintRowPixel, 85, fnt);
        //                PrintBill.Rows.Add(row);
        //                row = new PrintRow(dr.Cells["Col_UOM"].Value.ToString(), PrintRowPixel, 260, fnt);
        //                PrintBill.Rows.Add(row);
        //                row = new PrintRow(" X " + dr.Cells["Col_Pack"].Value.ToString(), PrintRowPixel, 280, fnt);
        //                PrintBill.Rows.Add(row);
        //                //row = new PrintRow(dr.Cells["Col_Shelf"].Value.ToString(), PrintRowPixel, 350, fnt);
        //                //PrintBill.Rows.Add(row);
        //                row = new PrintRow(dr.Cells["Col_ProdCompShortName"].Value.ToString(), PrintRowPixel, 435, fnt);
        //                PrintBill.Rows.Add(row);
        //                row = new PrintRow(dr.Cells["Col_BatchNumber"].Value.ToString(), PrintRowPixel, 480, fnt);
        //                PrintBill.Rows.Add(row);
        //                row = new PrintRow(dr.Cells["Col_Expiry"].Value.ToString(), PrintRowPixel, 600, fnt);
        //                PrintBill.Rows.Add(row);
        //                double mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
        //                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 700, fnt);
        //                PrintBill.Rows.Add(row);

        //            }
        //        }
        //        PrintRowPixel = 325;
        //        row = new PrintRow(_StockIn.CrdbNarration, PrintRowPixel, 15, fnt);
        //        PrintBill.Rows.Add(row);
        //        row = new PrintRow(_StockIn.CrdbAmountNet.ToString("#0.00"), PrintRowPixel, 700, fnt);
        //        PrintBill.Rows.Add(row);
        //        PrintRowPixel = 418;
        //        row = new PrintRow("---", PrintRowPixel, 15, fnt);
        //        PrintBill.Rows.Add(row);


        //        PrintBill.Print_Bill();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}

        //private int PrintHeader(int TotalPages, int Rowcount, Font fnt)
        //{
        //    PrintRow row;
        //    try
        //    {
        //        string billtype = "Stock In";
        //        PrintRowPixel = PrintRowPixel + 37;
        //        row = new PrintRow(billtype, PrintRowPixel, 250, fnt);
        //        PrintBill.Rows.Add(row);

        //        row = new PrintRow(_StockIn.CrdbVouNo.ToString(), PrintRowPixel, 400, fnt);
        //        PrintBill.Rows.Add(row);

        //        PrintRowPixel = PrintRowPixel + 36;

        //        row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 520, fnt);
        //        PrintBill.Rows.Add(row);

        //        row = new PrintRow(_StockIn.CrdbVouDate, PrintRowPixel, 680, fnt);
        //        PrintBill.Rows.Add(row);
        //        PrintPageNumber += 1;
        //        string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
        //        row = new PrintRow(page, PrintRowPixel, 750, fnt);
        //        PrintBill.Rows.Add(row);

        //        PrintRowPixel += 34;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //    Rowcount = 1;
        //    return Rowcount;
        //}
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
            double mdiscamount = 0;
            double mvat5per = 0;
            double mvat12point5per = 0;
            double mdiscper = 0;
            double mbillamount = 0;
            double mamount = 0;
            double mround = 0;
            try
            {
                System.Text.StringBuilder _errorMessage;
                if (mcbCreditor.SelectedID != null)
                    _StockIn.CrdbId = mcbCreditor.SelectedID.Trim();
                _StockIn.CrdbNarration = txtNarration.Text.Trim();
                _StockIn.CrdbVouType = _StockIn.CrdbVouType;
                if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                    _StockIn.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                _StockIn.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                double.TryParse(txtVatInput5per.Text, out mvat5per);
                _StockIn.CrdbVat5 = mvat5per;
                double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                _StockIn.CrdbVat12point5 = mvat12point5per;
                double.TryParse(txtDiscPercent.Text, out mdiscper);
                _StockIn.CrdbDiscPer = mdiscper;
                double.TryParse(txtDiscAmount.Text, out mdiscamount);
                _StockIn.CrdbDiscAmt = mdiscamount;
                double.TryParse(txtBillAmount.Text, out mbillamount);
                _StockIn.CrdbAmountNet = mbillamount;
                double.TryParse(txtAmount.Text, out mamount);
                _StockIn.CrdbAmount = mamount;
                double.TryParse(txtRoundAmount.Text, out mround);
                _StockIn.CrdbRoundAmount = mround;
                if (_Mode == OperationMode.Edit)
                    _StockIn.IFEdit = "Y";
                _StockIn.Validate();

                if (_StockIn.IsValid)
                {
                    LockTable.LockTablesForCreditDebitNoteStock();
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        General.BeginTransaction();
                        _StockIn.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _StockIn.CrdbVouNo = _StockIn.GetAndUpdateStockInNumber(General.ShopDetail.ShopVoucherSeries);
                        txtVouchernumber.Text = Convert.ToString(_StockIn.CrdbVouNo);
                        _StockIn.CreatedBy = General.CurrentUser.Id;
                        _StockIn.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _StockIn.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _StockIn.AddDetails();
                        if (retValue)
                            retValue = SaveParticularsProductwise();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                           // UpdateClosingStockinCache();
                            string msgLine2 = _StockIn.CrdbVouType + "  " + _StockIn.CrdbVouNo.ToString("#0");
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
                            _SavedID = _StockIn.Id;
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
                        if (_Mode == OperationMode.Edit)
                        {
                            General.BeginTransaction();
                            _StockIn.ModifiedBy = General.CurrentUser.Id;
                            _StockIn.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _StockIn.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            retValue = _StockIn.UpdateDetails();
                            if (retValue)
                                retValue = DeletePreviousRows();
                            if (retValue)
                                retValue = SaveParticularsProductwise();
                            if (retValue)
                                retValue = ReducePreviousStock();
                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {
                              //  UpdateClosingStockinCache();
                                string msgLine2 = _StockIn.CrdbVouType + "  " + _StockIn.CrdbVouNo.ToString("#0");
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
                                _SavedID = _StockIn.Id;
                                retValue = true;
                            }
                            else
                            {
                                PSDialogResult result = PSMessageBox.Show("Could not Save Voucher...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
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
                    foreach (string _message in _StockIn.ValidationMessages)
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
            CacheObject.Clear("TempCounterSale");
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _StockIn.Id = ID;
                    _StockIn.ReadDetailsByID();
                    mcbCreditor.SelectedID = FixAccounts.AccountStockInOut.ToString();
                    BindTempGrid();
                    InitialisempPVC1();
                    NumberofRows();
                    mcbCreditor.SelectedID = _StockIn.CrdbId;
                    txtNarration.Text = _StockIn.CrdbNarration.ToString();
                    txtVouType.Text = _StockIn.CrdbVouType;
                    txtVouchernumber.Text = _StockIn.CrdbVouNo.ToString().Trim();
                    if (_StockIn.CrdbVat5 >= 0)
                        txtVatInput5per.Text = _StockIn.CrdbVat5.ToString("#0.00");
                    if (_StockIn.CrdbVat12point5 >= 0)
                        txtVatInput12point5per.Text = _StockIn.CrdbVat12point5.ToString("#0.00");
                    if (_StockIn.CrdbDiscPer >= 0)
                        txtDiscPercent.Text = _StockIn.CrdbDiscPer.ToString("#0.00");
                    if (_StockIn.CrdbDiscAmt > 0)
                        txtDiscAmount.Text = _StockIn.CrdbDiscAmt.ToString("#0.00");

                    txtBillAmount.Text = _StockIn.CrdbAmountNet.ToString("#0.00");
                    txtAmount.Text = _StockIn.CrdbAmount.ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                    if (_StockIn.CrdbRoundAmount != 0)
                        txtRoundAmount.Text = _StockIn.CrdbRoundAmount.ToString("#0.00");
                    txtTotalAmount.Text = _StockIn.CrdbTotalAmount.ToString("#0.00");                  
                    DateTime mydate = new DateTime(Convert.ToInt32(_StockIn.CrdbVouDate.Substring(0, 4)), Convert.ToInt32(_StockIn.CrdbVouDate.Substring(4, 2)), Convert.ToInt32(_StockIn.CrdbVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    if (_Mode == OperationMode.View || _Mode == OperationMode.Delete || _Mode == OperationMode.ReportView)
                    {
                        mpPVC1.ColumnsMain[1].ReadOnly = true;
                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
                    }
                }
                else
                {
                    ClearControls();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData(Control closedControl)
        {
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Escape)
            {
                if (mpPVC1.VisibleProductGrid() == true) //kiran
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
            return retValue;
        }

        private bool DeletePreviousRows()
        {
            bool returnVal = false;
            try
            {
                returnVal = _StockIn.DeletePreviousRecords();
            }
            catch { returnVal = false; }
            return returnVal;
        }


        private bool SaveParticularsProductwise()
        {
            {
                bool returnVal = false;
                _StockIn.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                    {
                        if (prodrow.Cells["Col_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Col_Amount"].Value) > 0)
                        {
                            _StockIn.SerialNumber += 1;
                            _StockIn.StockID = "";
                            _StockIn.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _StockIn.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
                            _StockIn.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            _StockIn.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                            _StockIn.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurRate"].Value);
                            _StockIn.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                            _StockIn.SaleRate = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                            _StockIn.TradeRate = Convert.ToDouble(prodrow.Cells["Col_TradeRate"].Value);
                            _StockIn.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                            _StockIn.ExpiryDate = General.GetValidExpiryDate(prodrow.Cells["Col_Expiry"].Value.ToString());
                            //  _StockIn.ExpiryDate = prodrow.Cells["Col_ExpiryDate"].Value.ToString();
                            _StockIn.ExpiryDate = General.GetExpiryInyyyymmddForm(_StockIn.ExpiryDate);
                            _StockIn.VATPer = Convert.ToDouble(prodrow.Cells["Col_VATPer"].Value);
                            _StockIn.ReasonCode = prodrow.Cells["Col_Code"].Value.ToString();
                            if (prodrow.Cells["Col_StockID"].Value != null)
                                _StockIn.StockID = prodrow.Cells["Col_StockID"].Value.ToString();
                            _StockIn.Amount = Convert.ToDouble(prodrow.Cells["Col_Amount"].Value);

                            string ifRecordFound = "";
                            ifRecordFound = _StockIn.CheckForBatchMRPInStockTable();
                            if (ifRecordFound == "Y")
                            {
                                returnVal = _StockIn.UpdateIntblStock();
                            }
                            else
                            {
                                _StockIn.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                returnVal = _StockIn.InsertNewBatchIntblStock();
                            }
                            returnVal = _StockIn.AddProductDetails();
                            returnVal = _StockIn.UpdateCreditNoteStockInMasterProduct();
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
        }

        private bool AddStockIntblStock()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Amount"].Value) > 0 && prodrow.Cells["Col_Code"].Value.ToString().Trim() == "S")
                    {
                        _StockIn.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
                        _StockIn.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        _StockIn.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                        _StockIn.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                        _StockIn.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurRate"].Value);
                        _StockIn.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                        _StockIn.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                        _StockIn.SaleRate = _StockIn.MRP;
                        _StockIn.VATPer = Convert.ToDouble(prodrow.Cells["Col_VATPer"].Value);
                        _StockIn.ReasonCode = prodrow.Cells["Col_Code"].Value.ToString();

                        string ifRecordFound = "";
                        ifRecordFound = _StockIn.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "Y")
                        {
                            returnVal = _StockIn.UpdateIntblStock();
                            returnVal = _StockIn.UpdateCreditNoteStockInMasterProduct();
                        }
                        else
                        {
                            returnVal = _StockIn.InsertNewBatchIntblStock();
                            returnVal = _StockIn.UpdateCreditNoteStockInMasterProduct();
                        }
                    }
                }
            }
            catch { returnVal = false; }
            return returnVal;

        }

        private bool ReducePreviousStock()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0 && prodrow.Cells["Temp_Code"].Value.ToString().Trim() != "")
                    {
                        _StockIn.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        _StockIn.ProductID = Convert.ToInt32(prodrow.Cells["Temp_ProductID"].Value.ToString());
                        _StockIn.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _StockIn.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _StockIn.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _StockIn.ReasonCode = prodrow.Cells["Temp_Code"].Value.ToString();

                        string ifRecordFound = "";
                        ifRecordFound = _StockIn.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "Y")
                            returnVal = _StockIn.UpdateIntblStockReduce();
                        returnVal = _StockIn.UpdateCreditNoteStockInMasterProductReduce();
                    }
                }
            }
            catch { returnVal = false; }
            return returnVal;
        }
        //private bool UpdateClosingStockinCache()
        //{
        //    bool returnVal = false;
        //    try
        //    {
        //        foreach (DataGridViewRow prodrow in mpPVC1.Rows)
        //        {
        //            if (prodrow.Cells["Col_ProductID"].Value != null && prodrow.Cells["Col_ProductID"].Value.ToString() != string.Empty)
        //            {
        //                _StockIn.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
        //                EcoMartCache.RefreshProductData(_StockIn.ProductID);
        //            }
        //        }
        //        foreach (DataGridViewRow prodrow in dgtemp.Rows)
        //        {
        //            if (prodrow.Cells["Temp_ProductID"].Value != null && prodrow.Cells["Temp_ProductID"].Value.ToString() != string.Empty)
        //            {
        //                _StockIn.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();                       
        //                EcoMartCache.RefreshProductData(_StockIn.ProductID);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        returnVal = false;
        //    }
        //    return returnVal;
        //}
        //private bool UpdateClosingStockinCache()
        //{
        //    bool returnVal = false;
        //    try
        //    {             
        //        General.UpdateProductListCacheTest(mpPVC1.Rows, "Col_ProductID", dgtemp.Rows, "Temp_ProductID");        
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        returnVal = false;
        //    }
        //    return returnVal;
        //}
        #endregion

        #region Construct columns

        private void ConstructMainColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC1.ColumnsMain.Clear();

            try
            {
                //0
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
                column.Width = 215;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ToolTipText = "Press TAB Key To Exit Product Grid";
                column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                column.Name = "Col_VATPer";
                column.DataPropertyName = "VATPer";
                column.HeaderText = "VAT%";
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //6          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.ToolTipText = "Expiry dd/mm";
                column.Width = 50;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 80;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur.Rate";
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 80;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.HeaderText = "Qty";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Code";
                column.DataPropertyName = "ReasonCode";
                column.HeaderText = "CD";
                column.Width = 35;
                column.ReadOnly = true;
                column.ToolTipText = "N=Non Saleable, S=Add to Stock";
                mpPVC1.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";                
                column.ToolTipText = "N=Qty*Pur.Rate,S=Qty*MRP";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.Width = 115;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //12           
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //13          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Width = 70;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //14
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                column.Width = 60;
                mpPVC1.ColumnsMain.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Width = 70;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "StockID";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch No.";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATPer";
                column.DataPropertyName = "VATPer";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                column.Name = "Temp_PurRate";
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
                column.Name = "Temp_Code";
                column.DataPropertyName = "ReasonCode";
                column.HeaderText = "CD";
                column.Width = 40;
                column.ReadOnly = true;
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
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdLoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "COM";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfID";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT %";
                column.DefaultCellStyle.Format = "n2";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Cl Stk";
                column.Width = 60;
                column.ReadOnly = true;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
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
                column.DefaultCellStyle.Format = "n2";
                column.Visible = true;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.DefaultCellStyle.Format = "n2";
                column.Width = 100;
                mpPVC1.ColumnsBatchList.Add(column);



                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur Rate";
                column.DefaultCellStyle.Format = "n2";
                column.Width = 100;
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
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Closing Stock";
                column.Width = 65;
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

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.DefaultCellStyle.Format = "n2";
                column.Width = 100;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "TradeRate";
                column.Width = 80;
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);

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

        #endregion

        # region Internal methods
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

        private void InitialisempPVC1()
        {
            try
            {
                ConstructMainColumns();
                ConstructProductSelectionListGridColumns();
                ConstructBatchGridColumns();

                DataTable dtable = new DataTable();
                dtable = _StockIn.ReadProductDetailsByID();
                mpPVC1.DataSourceMain = dtable;
              //  mpPVC1.DataSourceProductList = EcoMartCache.GetProductData();
                 dtable = new DataTable();
                //dtable = General.ProductList;
                Product prod = new Product();
                dtable = prod.GetOverviewData();
                mpPVC1.DataSourceProductList = dtable;
                //mpPVC1.DataSourceProductList = General.ProductList;
                mpPVC1.NewRowColumnName = "Col_Code";
                mpPVC1.BatchGridShowColumnName = "Col_UOM";
                mpPVC1.NumericColumnNames.Add("Col_Quantity");
                mpPVC1.DoubleColumnNames.Add("Col_VATPer");
                mpPVC1.DoubleColumnNames.Add("Col_MRP");
                mpPVC1.DoubleColumnNames.Add("Col_PurRate");
                mpPVC1.DoubleColumnNames.Add("Col_Amount");
                mpPVC1.Bind();
                if (_Mode == OperationMode.Edit)
                    mpPVC1.Rows.Add();
                if (_Mode == OperationMode.View || _Mode == OperationMode.Delete || _Mode == OperationMode.ReportView)
                {
                    mpPVC1.ColumnsMain[1].ReadOnly = true;
                    mpPVC1.ColumnsMain[2].ReadOnly = true;
                    mpPVC1.ColumnsMain[3].ReadOnly = true;
                    mpPVC1.ColumnsMain[4].ReadOnly = true;
                    mpPVC1.ColumnsMain[5].ReadOnly = true;
                    mpPVC1.ColumnsMain[6].ReadOnly = true;
                    mpPVC1.ColumnsMain[7].ReadOnly = true;
                    mpPVC1.ColumnsMain[8].ReadOnly = true;
                    mpPVC1.ColumnsMain[9].ReadOnly = true;
                    mpPVC1.ColumnsMain[10].ReadOnly = true;
                    mpPVC1.ColumnsMain[11].ReadOnly = true;
                    mpPVC1.ColumnsMain[12].ReadOnly = true;
                    mpPVC1.ColumnsMain[13].ReadOnly = true;
                    mpPVC1.ColumnsMain[14].ReadOnly = true;
                    mpPVC1.ColumnsMain[15].ReadOnly = true;

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private bool BindmpPVC1Grid()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _StockIn.ReadProductDetailsByID();

                if (dtable.Rows.Count == 0)
                    return false;

                foreach (DataRow dr in dtable.Rows)
                {
                    int mindex = mpPVC1.Rows.Add();
                    mpPVC1.Rows[mindex].Cells["Col_ProductID"].Value = dr["ProductID"];
                    mpPVC1.Rows[mindex].Cells["Col_ProductName"].Value = dr["ProdName"];
                    mpPVC1.Rows[mindex].Cells["Col_UOM"].Value = dr["Prodloosepack"];
                    mpPVC1.Rows[mindex].Cells["Col_Pack"].Value = dr["prodpack"];
                    mpPVC1.Rows[mindex].Cells["Col_VATPer"].Value = Convert.ToDouble(dr["VATPer"]).ToString("#0.00");
                    mpPVC1.Rows[mindex].Cells["Col_BatchNumber"].Value = dr["BatchNumber"];
                    mpPVC1.Rows[mindex].Cells["Col_Expiry"].Value = dr["Expiry"];
                    mpPVC1.Rows[mindex].Cells["Col_MRP"].Value = Convert.ToDouble(dr["MRP"]).ToString("#0.00");
                    mpPVC1.Rows[mindex].Cells["Col_PurRate"].Value = Convert.ToDouble(dr["PurchaseRate"]).ToString("#0.00");
                    mpPVC1.Rows[mindex].Cells["Col_Quantity"].Value = Convert.ToInt32(dr["Quantity"]).ToString("#0");
                    mpPVC1.Rows[mindex].Cells["Col_Code"].Value = dr["ReasonCode"];
                    mpPVC1.Rows[mindex].Cells["Col_Amount"].Value = Convert.ToDouble(dr["Amount"]).ToString("#0.00");
                    mpPVC1.Rows[mindex].Cells["Col_ExpiryDate"].Value = dr["ExpiryDate"];
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void BindTempGrid()
        {
            try
            {
                ConstructTempGridColumns();
                DataTable tmptable = new DataTable();
                tmptable = _StockIn.ReadProductDetailsByID();
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
                cbRound.Checked = true;                    
                mpPVC1.ShowBatchWithZeroStock = true;
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtNoOfRows.Text = "";
                txtVouType.Text = _StockIn.CrdbVouType;
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtVatInput12point5per.Text = "0.00";
                txtVatInput5per.Text = "0.00";
                mpPVC1.ColumnsMain.Clear();
                mcbCreditor.SelectedID = "";
                this.mcbCreditor.Focus();
                mpPVC1.ColumnsMain.Clear();
                ConstructMainColumns();
                mpPVC1.ProductListGridWidth = 700;
                mpPVC1.BatchListGridWidth = 690;
                if (_Mode == OperationMode.Add)
                {
                    mcbCreditor.Enabled = true;
                    txtNarration.Enabled = true;
                    pnlAmounts.Enabled = true;
                    pnlVouTypeNo.Enabled = false;
                }
                else
                {
                    mcbCreditor.Enabled = false;
                    txtNarration.Enabled = false;
                    pnlAmounts.Enabled = false;
                    pnlVouTypeNo.Enabled = true;
                    txtVouchernumber.Enabled = true;
                    txtVouchernumber.ReadOnly = false;

                    txtVouchernumber.Focus();
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
                mcbCreditor.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2" };
                mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "200", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetStockInOutList();
                mcbCreditor.FillData(dtable);
                mcbCreditor.SelectedID = FixAccounts.AccountStockInOut.ToString();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        #region Events

        private void mpPVC1_OnProductSelected(DataGridViewRow productRow)
        {
            try
            {
                if (_Mode == OperationMode.View || _Mode == OperationMode.Delete || _Mode == OperationMode.ReportView)
                {
                    mpPVC1.ColumnsMain[1].ReadOnly = true;
                    mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
                }
                else
                {
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = productRow.Cells["Col_ProductID"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = productRow.Cells["Col_ProductName"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value = productRow.Cells["Col_ProdLoosePack"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Pack"].Value = productRow.Cells["Col_ProdPack"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = productRow.Cells["Col_VATPer"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = productRow.Cells["Col_ProdCompShortName"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = true;
                    mpPVC1.RefreshMe();
                    lblFooterMessage.Text = "Enter Loose Quantity";
                    mpPVC1.SetFocus(4);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpPVC1_OnBatchSelected(DataGridViewRow batchRow)
        {
            try
            {
                string newexpirydate = "";
                string newexpiry = "";
                mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = batchRow.Cells["Col_Batchno"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = batchRow.Cells["Col_Expiry"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value = batchRow.Cells["Col_MRP"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].Value = batchRow.Cells["Col_PurchaseRate"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value = batchRow.Cells["Col_StockID"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = batchRow.Cells["Col_SaleRate"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value = batchRow.Cells["Col_TradeRate"].Value;
                newexpiry = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString();

                newexpirydate = General.GetValidExpiryDate(newexpiry);
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = false;

                mpPVC1.SetFocus(9);


            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpPVC1_OnNewBatchClicked()
        {
            try
            {
                mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = false;
                mpPVC1.SetFocus(4);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private bool CheckStockForDelete()
        {
            bool retValue = true;

            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0 && prodrow.Cells["Temp_Code"].Value.ToString().Trim() == "S")
                    {
                        _StockIn.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        _StockIn.ProductID = Convert.ToInt32(prodrow.Cells["Temp_ProductID"].Value.ToString());
                        _StockIn.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _StockIn.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _StockIn.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _StockIn.ReasonCode = prodrow.Cells["Temp_Code"].Value.ToString();

                        string ifRecordFound = "";
                        ifRecordFound = _StockIn.CheckStockForBatchMRPInStockTable();
                        if (ifRecordFound != "Y")
                        {
                            retValue = false;
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;
            }
            return retValue;

        }


        private void mpPVC1_OnCellValueChangeCommited(int colIndex)
        {
            lblFooterMessage.Text = "";
            try
            {
                if (colIndex == 9)
                {
                    lblFooterMessage.Text = "Enter Quantity By Pack";
                    if (Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells[9].Value) <= 0)
                    {
                        lblFooterMessage.Text = "Enter Quantity By Pack";
                        mpPVC1.IsAllowNewRow = false;
                        mpPVC1.SetFocus(9);
                    }
                    else
                    {
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString() != "")
                        {
                            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value != null)
                            {

                                if ((Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value.ToString()) + Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString())) >= Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells[15].Value.ToString()))
                                {
                                    lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";
                                    mpPVC1.SetFocus(10);
                                }
                                else
                                {
                                    lblFooterMessage.Text = "Not Enough Stock for EDIT...";
                                    mpPVC1.IsAllowNewRow = false;
                                    mpPVC1.SetFocus(9);
                                }

                            }
                            else
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value == null || mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == "")
                                {
                                    mpPVC1.MainDataGridCurrentRow.Cells[10].Value = "S";
                                    lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";
                                }
                        }
                        else
                            if (mpPVC1.MainDataGridCurrentRow.Cells[10].Value == null || mpPVC1.MainDataGridCurrentRow.Cells[10].Value.ToString().Trim() == "")
                            {
                                mpPVC1.MainDataGridCurrentRow.Cells[10].Value = "S";
                                lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";
                            }
                    }

                }
                if (colIndex == 9)
                {
                    if (mpPVC1.MainDataGridCurrentRow.Cells[9].Value.ToString().Trim() != null && mpPVC1.MainDataGridCurrentRow.Cells[9].Value.ToString().Trim() != "" && Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells[9].Value) > 0)
                    {
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() != "")
                        {
                            if (mpPVC1.MainDataGridCurrentRow.Cells[10].Value.ToString().Trim() == "N")
                                mpPVC1.MainDataGridCurrentRow.Cells[11].Value = Math.Round(Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[9].Value) * Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[8].Value), 2).ToString();
                            else
                                mpPVC1.MainDataGridCurrentRow.Cells[11].Value = Math.Round(Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[9].Value) * Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[7].Value), 2).ToString();
                            CalculateAmount();
                            lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";
                            if (mpPVC1.MainDataGridCurrentRow.Cells[10].Value == null || mpPVC1.MainDataGridCurrentRow.Cells[10].Value.ToString().Trim() == "")
                                mpPVC1.MainDataGridCurrentRow.Cells[10].Value = "S";

                            mpPVC1.IsAllowNewRow = true;
                        }
                        else
                            lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";
                    }
                    else
                        lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";

                    mpPVC1.IsAllowNewRow = false;
                    //UpdateTempStockInDt();
                }
                if (colIndex == 10)
                {

                    if (mpPVC1.MainDataGridCurrentRow.Cells[10].Value.ToString().Trim() == "N" || mpPVC1.MainDataGridCurrentRow.Cells[10].Value.ToString().Trim() == "S")
                    {
                        if (Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells[9].Value) == 0)
                        {
                            lblFooterMessage.Text = "Enter Quantity";
                            mpPVC1.SetFocus(9);

                        }
                        else if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[8].Value) == 0.00)
                        {
                            lblFooterMessage.Text = "Enter Purchase Rate";
                            mpPVC1.SetFocus(8);

                        }
                        else if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[7].Value) == 0.00)
                        {
                            lblFooterMessage.Text = "Enter MRP";
                            mpPVC1.SetFocus(7);

                        }
                        else if (mpPVC1.MainDataGridCurrentRow.Cells[5].Value == null || mpPVC1.MainDataGridCurrentRow.Cells[4].Value.ToString().Trim() == "")
                        {
                            lblFooterMessage.Text = "Enter Batch Number";
                            mpPVC1.SetFocus(5);

                        }
                        else
                        {
                            string ifproductexists = "N";
                            int currowindex = 0;
                            string curProductID = mpPVC1.MainDataGridCurrentRow.Cells[0].Value.ToString();
                            string curbatch = mpPVC1.MainDataGridCurrentRow.Cells[5].Value.ToString();
                            double curmrp = 0;
                            double.TryParse(mpPVC1.MainDataGridCurrentRow.Cells[8].Value.ToString(), out curmrp);
                            string curcode = mpPVC1.MainDataGridCurrentRow.Cells[10].Value.ToString();

                            foreach (DataGridViewRow dr in mpPVC1.Rows)
                            {
                                if (dr.Index != mpPVC1.MainDataGridCurrentRow.Index)
                                {
                                    string drProductID = dr.Cells[0].Value.ToString();
                                    string drbatch = dr.Cells[5].Value.ToString();
                                    double drmrp = 0;
                                    double.TryParse(dr.Cells[8].Value.ToString(), out drmrp);
                                    string drcode = dr.Cells[10].Value.ToString();

                                    if (curProductID == drProductID && curbatch == drbatch && curmrp == drmrp && curcode == drcode)
                                    {
                                        currowindex = dr.Index;
                                        ifproductexists = "Y";
                                        break;
                                    }
                                }
                            }

                            if (ifproductexists == "Y")
                            {
                                lblFooterMessage.Text = "Batch Already Exist";
                            }
                            else
                            {

                                double mamount = 0;
                                double mpakn = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
                                if (mpakn == 0)
                                    mpakn = 1;
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == "N")
                                    mamount = Math.Round(Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) * (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].Value)/mpakn), 2);
                                else
                                    mamount = Math.Round(Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) * (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value)/mpakn), 2);

                                mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value = mamount.ToString("#0.00");
                                CalculateAmount();
                                mpPVC1.IsAllowNewRow = true;
                                lblFooterMessage.Text = "";
                            }

                        }
                    }
                    else
                    {
                        mpPVC1.MainDataGridCurrentRow.Cells[10].Value = "S";
                    }
                }

                if (colIndex == 6)
                {
                    string newexpiry = "";
                    string newexpirydate = "";
                    int explength = mpPVC1.MainDataGridCurrentRow.Cells[6].Value.ToString().Trim().Length;
                    if (mpPVC1.MainDataGridCurrentRow.Cells[6].Value != null && mpPVC1.MainDataGridCurrentRow.Cells[6].Value.ToString().Trim() != "" && explength > 0)
                    {
                        newexpiry = General.GetValidExpiry(mpPVC1.MainDataGridCurrentRow.Cells[6].Value.ToString());
                        if (newexpiry != "")
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells[6].Value = newexpiry.ToString();
                            newexpirydate = General.GetValidExpiryDate(newexpiry);
                            mpPVC1.MainDataGridCurrentRow.Cells[14].Value = newexpirydate.ToString();
                            lblFooterMessage.Text = "";
                        }
                        else
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells[6].Value = newexpiry;
                            lblFooterMessage.Text = " No Expiry ";
                        }

                    }
                    else
                    {
                        mpPVC1.MainDataGridCurrentRow.Cells[6].Value = newexpiry;
                        lblFooterMessage.Text = " No Expiry ";
                    }
                }
                CalculateAmount();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void NumberofRows()
        {
            try
            {
                int itemCount = 0;

                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                    {
                        itemCount += 1;
                    }

                }

                txtNoOfRows.Text = itemCount.ToString().Trim();
                //UpdateTempStockInDt();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void CalculateAmount()
        {
            try
            {
                double TotalAmount = 0;
                double VatAmount5 = 0;
                double VatAmount12Point5 = 0;
                int itemCount = 0;

                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                    {
                        // vat 5.5
                        if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 6)
                            VatAmount5 += Math.Round((double.Parse(dr.Cells["Col_Amount"].Value.ToString())) * (double.Parse(dr.Cells["Col_VATPer"].Value.ToString())) / 100, 4);
                        else
                        {
                            if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 13.50)
                                VatAmount12Point5 += Math.Round((double.Parse(dr.Cells["Col_Amount"].Value.ToString())) * (double.Parse(dr.Cells["Col_VATPer"].Value.ToString())) / 100, 4);
                        }
                        TotalAmount += double.Parse(dr.Cells["Col_Amount"].Value.ToString());
                        itemCount += 1;
                    }

                }

                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtVatInput12point5per.Text = Math.Round(VatAmount12Point5, 2).ToString("#0.00");
                txtVatInput5per.Text = Math.Round(VatAmount5, 2).ToString("#0.00");
                txtAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");

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
            double.TryParse(txtAmount.Text, out mdblAmount);
            double mdblVatInput12point5per;
            double.TryParse(txtVatInput12point5per.Text, out mdblVatInput12point5per);
            double mdblVatInput5per;
            double.TryParse(txtVatInput5per.Text, out mdblVatInput5per);
            double mdblDiscPer;
            double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
            double mdblDiscAmount;
            double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);
            try
            {
                if (mdblDiscPer > 0)
                {
                    mdblDiscAmount = Math.Round(((mdblAmount + mdblVatInput12point5per + mdblVatInput5per) * mdblDiscPer / 100), 2);
                    txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                }

                if (mdblAmount + mdblVatInput12point5per + mdblVatInput5per < mdblDiscAmount)
                {
                    txtDiscAmount.Text = "0.00";
                    double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);
                }

                txtTotalAmount.Text = Math.Round(mdblAmount + mdblVatInput12point5per
                              + mdblVatInput5per - mdblDiscAmount, 2).ToString("#0.00");



                if (cbRound.Checked == true)
                {
                    txtRoundAmount.Text = Math.Round(Math.Round(Convert.ToDouble(txtTotalAmount.Text), 0) - Math.Round(Convert.ToDouble(txtTotalAmount.Text), 2), 2).ToString("#0.00");
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text) + Convert.ToDouble(txtRoundAmount.Text)), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                }
                else
                {
                    txtRoundAmount.Text = "0.00";
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text) + Convert.ToDouble(txtRoundAmount.Text)), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtVatInput5per_TextChanged(object sender, EventArgs e)
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

        private void txtVatInput12point5per_TextChanged(object sender, EventArgs e)
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

        private void txtDiscPercent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double mdblAmount;
                double.TryParse(txtAmount.Text, out mdblAmount);
                double mdblVatInput12point5per;
                double.TryParse(txtVatInput12point5per.Text, out mdblVatInput12point5per);
                double mdblVatInput5per;
                double.TryParse(txtVatInput5per.Text, out mdblVatInput5per);
                double mdblDiscPer;
                double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
                double mdblDiscAmount;
                double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);
                mdblDiscAmount = Math.Round(((mdblAmount + mdblVatInput12point5per + mdblVatInput5per) * mdblDiscPer / 100), 2);
                txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                CalculateAllAmounts();
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

        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtDiscAmount.Focus();
                    break;
                case Keys.Down:
                    txtDiscAmount.Focus();
                    break;
            }
        }

        private void txtVatInput5per_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtVatInput12point5per.Focus();
                        break;
                    case Keys.Down:
                        txtVatInput12point5per.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void txtVatInput12point5per_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtDiscPercent.Focus();
                        break;
                    case Keys.Down:
                        txtDiscPercent.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }



        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mpPVC1.SetFocus(1);
            }
        }
        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = ((PSComboBoxNew)sender).SelectedID;
            try
            {
                FillPartyCombo();
                mcbCreditor.SelectedID = selectedId;
                if (mcbCreditor.SeletedItem != null)
                {
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
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
                if (mcbCreditor.SeletedItem == null)
                {
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                }
                else
                {
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mpPVC1_OnRowDeleted(object sender, EventArgs e)
        {
            try
            {
                CalculateAmount();
                //UpdateTempStockInDt();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            txtVatInput5per.Focus();
        }

        private void mcbCreditor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtNarration.Focus();
                    break;

            }
        }

        private void txtDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    cbRound.Focus();
                    break;
            }
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            int vouno = 0;
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtVouchernumber.Text != null)
                    {
                        int.TryParse(txtVouchernumber.Text.ToString(), out vouno);
                        _StockIn.ReadDetailsByVouNumber(vouno);
                        FillSearchData(_StockIn.Id,"");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            ttStkIn.SetToolTip(txtNarration, "Enter Narration");
            ttStkIn.SetToolTip(txtAmount, "Total Amount of All Rows");
            ttStkIn.SetToolTip(txtTotalAmount, "Amount+Vat Amounts - Discount");

        }
        #endregion

        #region StockUpdate
        private DataTable CreateStockInDt()
        {
            dtTempStockIn = CacheObject.Get<DataTable>("TempCounterSale");
            List<DataRow> rowsToDelete = new List<DataRow>();

            if (dtTempStockIn == null)
            {
                dtTempStockIn = new DataTable();
                dtTempStockIn.Columns.Add("ProductID", typeof(int));
                dtTempStockIn.Columns.Add("BatchID", typeof(string));
                dtTempStockIn.Columns.Add("QTY", typeof(int));
                dtTempStockIn.Columns.Add("SRate", typeof(decimal));
                dtTempStockIn.Columns.Add("FormName", typeof(string));
                dtTempStockIn.Columns.Add("StockID", typeof(int));
                //ProductID, batch,mrp
                CacheObject.Add(dtTempStockIn, "TempCounterSale");
                //DataRow[] drFormRows = dtTempPatientSale.Select("FormName");

            }
            foreach (DataRow item in dtTempStockIn.Rows)
            {
                if (string.Equals(item["FormName"], this.Name))
                {
                    rowsToDelete.Add(item);
                }
            }
            foreach (DataRow row in rowsToDelete)
            {
                dtTempStockIn.Rows.Remove(row);
            }
            return dtTempStockIn;

        }
        //private void UpdateTempStockInDt()
        //{
        //    try
        //    {
        //        DataTable dtTempCounterSale = CreateStockInDt();
        //        //dtTempCounterSale.Clear();

        //        foreach (DataGridViewRow dr in mpPVC1.Rows)
        //        {
        //            if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_SaleRate"].Value != null)
        //            {
        //                if (dtTempCounterSale.Rows.Count > 0)
        //                {
        //                    //  DataRow[] TempCounterSale = dtTempCounterSale.Select("ProductID=" + dr.Cells["Col_ProductID"].Value + " And BatchID=" + dr.Cells["Col_BatchNumber"].Value + " And SRate=" + dr.Cells["Col_SaleRate"].Value);
        //                    DataRow[] TempCounterSale = dtTempCounterSale.Select("ProductID='" + dr.Cells["Col_ProductID"].Value + "' And BatchID='" + dr.Cells["Col_BatchNumber"].Value + "' And SRate='" + dr.Cells["Col_SaleRate"].Value + "' And FormName='" + this.Name + "'");
        //                    if (TempCounterSale.Length > 0)
        //                    {
        //                        if (!string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_Quantity"].Value)))
        //                        {
        //                            if (!string.IsNullOrEmpty(Convert.ToString(TempCounterSale[0]["QTY"])))
        //                                TempCounterSale[0]["QTY"] = Convert.ToInt32(TempCounterSale[0]["QTY"]) + Convert.ToInt32(dr.Cells["Col_Quantity"].Value);
        //                            else
        //                                TempCounterSale[0]["QTY"] = dr.Cells["Col_Quantity"].Value;
        //                        }

        //                    }
        //                    else
        //                    {
        //                        DataRow drTempCounterSale = dtTempCounterSale.NewRow();

        //                        drTempCounterSale["StockID"] = dr.Cells["Col_StockID"].Value;
        //                        drTempCounterSale["ProductID"] = dr.Cells["Col_ProductID"].Value;
        //                        drTempCounterSale["BatchID"] = dr.Cells["Col_BatchNumber"].Value;
        //                        drTempCounterSale["QTY"] = dr.Cells["Col_Quantity"].Value;
        //                        drTempCounterSale["FormName"] = this.Name;

        //                        if (dr.Cells["Col_SaleRate"].Value != null)
        //                            drTempCounterSale["SRate"] = dr.Cells["Col_SaleRate"].Value;
        //                        else
        //                            drTempCounterSale["SRate"] = DBNull.Value;

        //                        dtTempCounterSale.Rows.Add(drTempCounterSale);

        //                    }
        //                }
        //                else
        //                {
        //                    DataRow drTempCounterSale = dtTempCounterSale.NewRow();
        //                    drTempCounterSale["StockID"] = dr.Cells["Col_StockID"].Value;
        //                    drTempCounterSale["ProductID"] = dr.Cells["Col_ProductID"].Value;
        //                    drTempCounterSale["BatchID"] = dr.Cells["Col_BatchNumber"].Value;
        //                    drTempCounterSale["QTY"] = dr.Cells["Col_Quantity"].Value;
        //                    drTempCounterSale["FormName"] = this.Name;

        //                    if (dr.Cells["Col_SaleRate"].Value != null)
        //                        drTempCounterSale["SRate"] = dr.Cells["Col_SaleRate"].Value;
        //                    else
        //                        drTempCounterSale["SRate"] = DBNull.Value;

        //                    dtTempCounterSale.Rows.Add(drTempCounterSale);
        //                }
        //            }
        //        }
        //        CacheObject.Add(dtTempCounterSale, "TempCounterSale");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteError(ex.Message);
        //    }
        //}

        #endregion StockUpdate
    }
}

