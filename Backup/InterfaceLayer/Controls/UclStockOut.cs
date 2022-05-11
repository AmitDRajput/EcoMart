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
    public partial class UclStockOut : BaseControl
    {
        #region Declaration       
        private DataTable _BindingSource;
        private StockOut _StockOut;      
        #endregion

        #region constructor
        public UclStockOut()
        {
            try
            {
                InitializeComponent();
                _StockOut = new StockOut();
                SearchControl = new UclStockOutSearch();
            }
            catch(Exception Ex)
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
            _StockOut.Initialise();
            ClearControls();
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
                headerLabel1.Text = "STOCK OUT -> NEW";
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
                headerLabel1.Text = "STOCK OUT -> EDIT";
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
              
                ClearData();
                pnlVouTypeNo.Enabled = true;
                InitialisempPVC1();
                FillPartyCombo();
                mpPVC1.SetFocus(1);
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
                headerLabel1.Text = "STOCK OUT -> DELETE";
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
                if (_StockOut.Id != null && _StockOut.Id != "")
                {
                    bool canbedeleted = true;
                    if (canbedeleted)
                    {
                        BindTempGrid();
                        LockTable.LockTablesForCreditDebitNoteStock();
                        General.BeginTransaction();
                        retValue = AddPreviousStock();
                        if (retValue)
                            retValue = DeletePreviousProductRows();
                        if (retValue)
                            retValue = _StockOut.DeleteDetails();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        UpdateClosingStockinCache();

                        if (retValue)
                        {
                            UpdateClosingStockinCache();
                            retValue = true;
                            MessageBox.Show("Successfully Deleted", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            return retValue;
        }
        public override bool View()
        {
            bool retValue = base.View();
            try
            {
                ClearData();
                headerLabel1.Text = "STOCK OUT -> VIEW";
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
            PrintData();
            ClearData();
            return retValue;
        }

        private void PrintData()
        {
            PrintRow row;
            try
            {
                PrintBill.Rows.Clear();
                Font fnt = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = mpPVC1.Rows.Count;
                PrintPageNumber = 0;
                int rowcount = 0;
                PrintRowPixel = 0;
                double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                int totalpages = Convert.ToInt32(totpages);
                PrintHeader(totalpages, rowcount, fnt);
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {

                    if (dr.Cells["Col_ProductID"].Value != null)
                    {
                        if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                        {
                            PrintRowPixel = 325;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
                            PrintBill.Rows.Add(row);
                            //////////_StockOut.PrintRowPixel = 418;
                            //////////row = new PrintRow("End--------------", _StockOut.PrintRowPixel, 15, fnt);
                            //////////PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;                           
                            PrintHeader(totalpages, rowcount, fnt);

                            rowcount = 0;
                        }
                        PrintRowPixel += 17;
                        rowcount += 1;
                        row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString(), PrintRowPixel, 15, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString(), PrintRowPixel, 85, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_UOM"].Value.ToString(), PrintRowPixel, 260, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(" X " + dr.Cells["Col_Pack"].Value.ToString(), PrintRowPixel, 280, fnt);
                        PrintBill.Rows.Add(row);
                        //row = new PrintRow(dr.Cells["Col_Shelf"].Value.ToString(), PrintRowPixel, 350, fnt);
                        //PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_ProdCompShortName"].Value.ToString(), PrintRowPixel, 435, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_BatchNumber"].Value.ToString(), PrintRowPixel, 480, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_Expiry"].Value.ToString(), PrintRowPixel, 600, fnt);
                        PrintBill.Rows.Add(row);
                        double mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 700, fnt);
                        PrintBill.Rows.Add(row);

                    }
                }
                PrintRowPixel = 325;
                row = new PrintRow(_StockOut.CrdbNarration, PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(_StockOut.CrdbAmountNet.ToString("#0.00"), PrintRowPixel, 700, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel = 418;
                row = new PrintRow("---", PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);


                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private int PrintHeader(int TotalPages, int Rowcount, Font fnt)
        {
            PrintRow row;
            try
            {
                string billtype = "Stock Out";
               PrintRowPixel = PrintRowPixel + 37;
                row = new PrintRow(billtype, PrintRowPixel, 250, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_StockOut.CrdbVouNo.ToString(), PrintRowPixel, 400, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 36;

                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 520, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_StockOut.CrdbVouDate,PrintRowPixel, 680, fnt);
                PrintBill.Rows.Add(row);
               PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow(page, PrintRowPixel, 750, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 34;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            Rowcount = 1;
            return Rowcount;
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
                    _StockOut.CrdbId = mcbCreditor.SelectedID.Trim();
                _StockOut.CrdbNarration = txtNarration.Text.Trim(); ;
                if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                    _StockOut.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                _StockOut.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                double.TryParse(txtVatInput5per.Text, out mvat5per);
                _StockOut.CrdbVat5 = mvat5per;
                double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                _StockOut.CrdbVat12point5 = mvat12point5per;
                double.TryParse(txtDiscPercent.Text, out mdiscper);
                _StockOut.CrdbDiscPer = mdiscper;
                double.TryParse(txtDiscAmount.Text, out mdiscamount);
                _StockOut.CrdbDiscAmt = mdiscamount;
                double.TryParse(txtBillAmount.Text, out mbillamount);
                _StockOut.CrdbAmountNet = mbillamount;
                double.TryParse(txtAmount.Text, out mamount);
                _StockOut.CrdbAmount = mamount;
                double.TryParse(txtRoundAmount.Text, out mround);
                _StockOut.CrdbRoundAmount = mround;
                if (_Mode == OperationMode.Edit)
                    _StockOut.IFEdit = "Y";
                _StockOut.Validate();

                if (_StockOut.IsValid)
                {
                    LockTable.LockTablesForCreditDebitNoteStock();

                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        General.BeginTransaction();
                        _StockOut.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _StockOut.CrdbVouNo = _StockOut.GetAndUpdateStockOutNumber(General.ShopDetail.ShopVoucherSeries);
                        _StockOut.CreatedBy = General.CurrentUser.Id;
                        _StockOut.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _StockOut.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        txtVouchernumber.Text = Convert.ToString(_StockOut.CrdbVouNo);
                        retValue = _StockOut.AddDetails();                    
                        _SavedID = _StockOut.Id;
                        if (retValue)
                            retValue = SaveParticularsProductwise();
                        if (retValue)
                            retValue = ReduceStockIntblStock();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            retValue = UpdateClosingStockinCache();
                            string msgLine2 = _StockOut.CrdbVouType + "  " + _StockOut.CrdbVouNo.ToString("#0");
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
                        if (_Mode == OperationMode.Edit)
                        {
                            General.BeginTransaction();
                            _StockOut.ModifiedBy = General.CurrentUser.Id;
                            _StockOut.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _StockOut.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            retValue = _StockOut.UpdateDetails();
                            if (retValue)
                                retValue = DeletePreviousProductRows();
                            if (retValue)
                                retValue = SaveParticularsProductwise();
                            if (retValue)
                                retValue = AddPreviousStock();
                            if (retValue)
                                retValue = ReduceStockIntblStock();
                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {
                                UpdateClosingStockinCache();
                                string msgLine2 = _StockOut.CrdbVouType + "  " + _StockOut.CrdbVouNo.ToString("#0");
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
                                _SavedID = _StockOut.Id;
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
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _StockOut.ValidationMessages)
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
            return retValue;
        }


        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _StockOut.Id = ID;
                    _StockOut.ReadDetailsByID();
                    mcbCreditor.SelectedID = _StockOut.CrdbId;

                    BindTempGrid();
                    InitialisempPVC1();
                    NumberofRows();                 
                    mcbCreditor.Focus();
                    txtNarration.Text = _StockOut.CrdbNarration.ToString();
                    txtVouType.Text = _StockOut.CrdbVouType;
                    txtVouchernumber.Text = _StockOut.CrdbVouNo.ToString().Trim();
                    if (_StockOut.CrdbVat5 >= 0)
                        txtVatInput5per.Text = _StockOut.CrdbVat5.ToString("#0.00");
                    if (_StockOut.CrdbVat12point5 >= 0)
                        txtVatInput12point5per.Text = _StockOut.CrdbVat12point5.ToString("#0.00");
                    if (_StockOut.CrdbDiscPer >= 0)
                        txtDiscPercent.Text = _StockOut.CrdbDiscPer.ToString("#0.00");
                    if (_StockOut.CrdbDiscAmt > 0)
                        txtDiscAmount.Text = _StockOut.CrdbDiscAmt.ToString("#0.00");

                    txtBillAmount.Text = _StockOut.CrdbAmountNet.ToString("#0.00");
                    txtAmount.Text = _StockOut.CrdbAmount.ToString("#0.00");
                    if (_StockOut.CrdbRoundAmount != 0)
                        txtRoundAmount.Text = _StockOut.CrdbRoundAmount.ToString("#0.00");
                    txtTotalAmount.Text = _StockOut.CrdbTotalAmount.ToString("#0.00");
                    if (_StockOut.CrdbRoundAmount != 0)
                        cbRound.Checked = false;
                    DateTime mydate = new DateTime(Convert.ToInt32(_StockOut.CrdbVouDate.Substring(0, 4)), Convert.ToInt32(_StockOut.CrdbVouDate.Substring(4, 2)), Convert.ToInt32(_StockOut.CrdbVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
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
         public override void ReFillData()
        {
            try
            {
                FillPartyCombo();
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
                    txtNarration.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    mcbCreditor.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.L && modifier == Keys.Alt)
                {
                    txtDiscPercent.Focus();
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
       

    

        private bool DeletePreviousProductRows()
        {
            bool returnVal = false;
            try
            {
                returnVal = _StockOut.DeletePreviousRecords();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }

        private bool SaveParticularsProductwise()
        {
            {
                bool returnVal = false;
                _StockOut.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                    {
                        if (prodrow.Cells["Col_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                        {
                            _StockOut.SerialNumber += 1;
                            _StockOut.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _StockOut.StockID = prodrow.Cells["Col_StockID"].Value.ToString();
                            _StockOut.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                            _StockOut.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            _StockOut.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                            if (prodrow.Cells["Col_ScmQuantity"].Value != null && prodrow.Cells["Col_ScmQuantity"].Value.ToString().Trim() != "")
                            _StockOut.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Col_ScmQuantity"].Value);
                            _StockOut.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurRate"].Value);
                            _StockOut.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                            if (prodrow.Cells["Col_SaleRate"].Value != null)
                            _StockOut.SaleRate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value);
                            if (prodrow.Cells["Col_TradeRate"].Value != null)
                                _StockOut.TradeRate = Convert.ToDouble(prodrow.Cells["Col_TradeRate"].Value.ToString());
                            
                            _StockOut.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                            _StockOut.ExpiryDate = prodrow.Cells["Col_ExpiryDate"].Value.ToString();
                            if (_StockOut.ExpiryDate != "")
                                _StockOut.ExpiryDate = General.GetExpiryInyyyymmddForm(_StockOut.ExpiryDate);
                            _StockOut.VATPer = Convert.ToDouble(prodrow.Cells["Col_VATPer"].Value);
                            _StockOut.ReasonCode = prodrow.Cells["Col_Code"].Value.ToString();

                            _StockOut.Amount = Convert.ToDouble(prodrow.Cells["Col_Amount"].Value);
                            returnVal = _StockOut.AddProductDetails();
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
        }

        private bool ReduceStockIntblStock()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        _StockOut.StockID = prodrow.Cells["Col_StockID"].Value.ToString();
                        _StockOut.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                        _StockOut.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        _StockOut.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                        _StockOut.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                        if (prodrow.Cells["Col_ScmQuantity"].Value != null && prodrow.Cells["Col_ScmQuantity"].Value.ToString() != "")
                            _StockOut.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Col_ScmQuantity"].Value.ToString());

                        string ifRecordFound = "";
                        ifRecordFound = _StockOut.CheckForStockIDInStockTable();
                        if (ifRecordFound == "Y")
                        {
                            returnVal = _StockOut.UpdateIntblStock();
                            if (returnVal)
                            {
                                returnVal = _StockOut.UpdateStockOutInMasterProduct();
                                if (returnVal == false)
                                    break;
                            }
                            else
                                break;
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

        private bool AddPreviousStock()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0 && prodrow.Cells["Temp_Code"].Value.ToString().Trim() != "")
                    {
                        _StockOut.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        _StockOut.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        _StockOut.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _StockOut.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _StockOut.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _StockOut.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_ScmQuantity"].Value);

                        _StockOut.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurRate"].Value);
                        _StockOut.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _StockOut.ReasonCode = prodrow.Cells["Temp_Code"].Value.ToString();
                        _StockOut.ProdLoosePack =  Convert.ToInt32(prodrow.Cells["Temp_UOM"].Value.ToString());
                        string ifRecordFound = "";
                        ifRecordFound = _StockOut.CheckForStockIDInStockTable();
                        if (ifRecordFound == "Y")
                            returnVal = _StockOut.UpdateIntblStockAdd();
                        returnVal = _StockOut.UpdateStockOutInMasterProductAddFromTemp();                       
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
        //private bool UpdateClosingStockinCache()
        //{
        //    bool returnVal = false;
        //    try
        //    {
        //        foreach (DataGridViewRow prodrow in mpPVC1.Rows)
        //        {
        //            if (prodrow.Cells["Col_ProductID"].Value != null && prodrow.Cells["Col_ProductID"].Value.ToString() != string.Empty)
        //            {
        //                _StockOut.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
        //                PharmaSysRetailPlusCache.RefreshProductData(_StockOut.ProductID);
        //            }
        //        }
        //        foreach (DataGridViewRow prodrow in dgtemp.Rows)
        //        {
        //            if (prodrow.Cells["Temp_ProductID"].Value != null && prodrow.Cells["Temp_ProductID"].Value.ToString() != string.Empty)
        //            {
        //                _StockOut.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();                       
        //                PharmaSysRetailPlusCache.RefreshProductData(_StockOut.ProductID);
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
        private bool UpdateClosingStockinCache()
        {
            bool returnVal = false;
            try
            {
                General.UpdateProductListCacheTest(mpPVC1.Rows, "Col_ProductID", dgtemp.Rows, "Temp_ProductID");
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
            //bool retValue = false;
            //try
            //{
            //    double mbillAmount = 0;
            //    if (_StockOut.CrdbId != mcbCreditor.SelectedID)
            //        retValue = true;
            //    if (_StockOut.CrdbNarration != txtNarration.Text.Trim())
            //        retValue = true;
            //    double.TryParse(txtBillAmount.Text, out mbillAmount);

            //    if (_StockOut.CrdbAmountNet != mbillAmount)
            //        retValue = true;
            //    if (_StockOut.CrdbVouDate != datePickerBillDate.Value.Date.ToString("yyyyMMdd"))
            //        retValue = true;
            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteException(Ex);
            //}
            //return retValue;
        }

      
      
        #endregion
              

        #region Construct columns

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
                column.Width = 200;
                column.ToolTipText = "Press TAB Key To Exit Product Grid";
                column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 60;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "VATPer";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 80;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur.Rate";
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
                column.Width = 60;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScmQuantity";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "Scm";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = true;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Code";
                column.DataPropertyName = "ReasonCode";
                column.HeaderText = "CD";
                column.Width = 35;
                column.ReadOnly = true;
                column.ToolTipText = "E=EXPIRY,B=BREAKAGE,U=UNKNOWN,G=GOODS RETURN";
                mpPVC1.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.ToolTipText = "N=Qty*Pur.Rate,S=Qty*MRP";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.Width = 85;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                //13           
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //14          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
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


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "StockID";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
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
                column.Name = "Temp_ScmQuantity";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "Scm";
                column.Width = 50;
                column.Visible = false;
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
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdLoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfID";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "ShelfID";
                column.Width = 60;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT %";
                column.DefaultCellStyle.Format = "n2";
                column.Width = 60;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Cl Stk";
                column.Width = 50;
                column.ReadOnly = true;
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
                column.HeaderText = "VAT(%)";
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
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "TradeRate";
                column.Visible = false;
                column.DefaultCellStyle.Format = "n2";
                column.Width = 65;
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

        private void InitialisempPVC1()
        {
            try
            {
                ConstructMainColumns();
                ConstructProductSelectionListGridColumns();
                ConstructBatchGridColumns();
                DataTable dtable = new DataTable();
                dtable = _StockOut.ReadProductDetailsByID();
                mpPVC1.DataSourceMain = dtable;

                mpPVC1.NewRowColumnName = "Col_Code";
                mpPVC1.BatchGridShowColumnName = "Col_UOM";
                mpPVC1.NumericColumnNames.Add("Col_Quantity");
                mpPVC1.DoubleColumnNames.Add("Col_VATPer");
                mpPVC1.DoubleColumnNames.Add("Col_MRP");
                mpPVC1.DoubleColumnNames.Add("Col_PurRate");
                mpPVC1.DoubleColumnNames.Add("Col_Amount");

                //Product prod = new Product();
               // mpPVC1.DataSourceProductList = prod.GetOverviewDataForClosingStockNotZero();
               // mpPVC1.DataSourceProductList = PharmaSysRetailPlusCache.GetProductData();

                mpPVC1.DataSourceProductList = General.ProductList;
                mpPVC1.Bind();
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
                if (_Mode == OperationMode.Edit)
                    mpPVC1.Rows.Add();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void BindTempGrid()
        {
            try
            {
                ConstructTempGridColumns();
                DataTable tmptable = new DataTable();
                tmptable = _StockOut.ReadProductDetailsByID();
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
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtVouType.Text = _StockOut.CrdbVouType;
                datePickerBillDate.ResetText();
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtVatInput12point5per.Text = "0.00";
                txtVatInput5per.Text = "0.00";
                txtTotalAmount.Text = "";
                mcbCreditor.SelectedID = "";
                txtRoundAmount.Text = "";
                txtNoOfRows.Text = "";
                this.mcbCreditor.Focus();
                lblMessage.Text = "";
                mpPVC1.ColumnsMain.Clear();
                if (_Mode == OperationMode.Add)
                {
                    mcbCreditor.Enabled = true;
                    txtNarration.Enabled = true;
                    pnlAmounts.Enabled = true;
                    pnlVouTypeNo.Enabled = true;
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
                mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "200","0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetStockInOutList();
                mcbCreditor.FillData(dtable);
                mcbCreditor.SelectedID = FixAccounts.AccountStockInOut;
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

        private void mpPVC1_OnProductSelected(DataGridViewRow productRow)
        {
            double mvatper = 0;
            try
            {
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = productRow.Cells["Col_ProductID"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = productRow.Cells["Col_ProductName"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value = productRow.Cells["Col_ProdLoosePack"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Pack"].Value = productRow.Cells["Col_ProdPack"].Value;
                if (productRow.Cells["Col_VATPer"].Value != null)
                    double.TryParse(productRow.Cells["Col_VATPer"].Value.ToString(), out mvatper);
                mpPVC1.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = mvatper.ToString("#0.00");
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = productRow.Cells["Col_ProdCompShortName"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = true;
                mpPVC1.RefreshMe();
                mpPVC1.SetFocus(4);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }      
        }

        private void mpPVC1_OnBatchSelected(DataGridViewRow batchRow)
        {
            string newexpirydate = "";
            string newexpiry = "";
            double mmrp = 0;
            double mprate = 0;
            double mtraderate = 0;
            try
            {
                mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = batchRow.Cells["Col_Batchno"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = batchRow.Cells["Col_Expiry"].Value;
                if (batchRow.Cells["Col_MRP"].Value != null)
                    double.TryParse(batchRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
                mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value = mmrp.ToString("#0.00");
                if (batchRow.Cells["Col_PurchaseRate"].Value != null)
                    double.TryParse(batchRow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].Value = mprate.ToString("#0.00");

                if (batchRow.Cells["Col_TradeRate"].Value != null)
                    double.TryParse(batchRow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                mpPVC1.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value = mtraderate.ToString("#0.00");
                
                mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchClosingStock"].Value = batchRow.Cells["Col_ClosingStock"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value = batchRow.Cells["Col_StockID"].Value;
                newexpiry = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString();

                newexpirydate = General.GetValidExpiryDate(newexpiry);
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = false;
                lblMessage.Text = "Enter Loose Quantity";
                mpPVC1.SetFocus(9); 
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
                CalculateAmount();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            txtDiscPercent.Focus();
        }
        
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mpPVC1.SetFocus(1);
            }
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }
        private void mpPVC1_OnCellValueChangeCommited(int colIndex)
        {
            try
            {
                if (colIndex == 9)
                {
                    lblMessage.Text = "Enter Loose Quantity";
                    if (Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells[9].Value) <= 0)
                    {
                        lblMessage.Text = "Enter Loose Quantity";
                        mpPVC1.IsAllowNewRow = false;
                        mpPVC1.SetFocus(9);
                    }
                    else
                    {
                        int oldqty = 0;
                        int batchqty = 0;
                        int totalqty = 0;
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString() != "")
                            int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString(), out oldqty);

                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchClosingStock"].Value != null)
                            int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchClosingStock"].Value.ToString(), out batchqty);

                        totalqty = GetTotalQuantityForStockID();


                        if (totalqty <= (oldqty + batchqty))
                        {

                            if (mpPVC1.MainDataGridCurrentRow.Cells[10].Value == null || mpPVC1.MainDataGridCurrentRow.Cells[10].Value.ToString() == "")
                                mpPVC1.MainDataGridCurrentRow.Cells[10].Value = "0";

                            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString().Trim() != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString().Trim() != "" && Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) > 0)
                            {
                                double mamt = 0;
                                double mqty = 0;
                                double mmrp = 0;
                                double mprate = 0;
                                double mpakn = 0;
                                double mdiscountper = 0;
                                double mdiscountamt = 0;

                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() != null) 
                                {
                                    if (mpPVC1.MainDataGridCurrentRow.Cells[9].Value != null)
                                        mqty = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[9].Value.ToString());
                                    if (mpPVC1.MainDataGridCurrentRow.Cells[8].Value != null)
                                        mprate = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[8].Value.ToString());
                                    if (mpPVC1.MainDataGridCurrentRow.Cells[7].Value != null)
                                        mmrp = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[7].Value.ToString());
                                    if (mpPVC1.MainDataGridCurrentRow.Cells[2].Value != null)
                                        mpakn = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[2].Value.ToString());
                                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Discount"].Value != null)
                                        mdiscountper = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_Discount"].Value.ToString());

                                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == FixAccounts.SubTypeForBreakage || mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == FixAccounts.SubTypeForExpiry)
                                        mamt = mqty * (mmrp / mpakn);
                                    else
                                        mamt = mqty * (mprate / mpakn);
                                    mdiscountamt = Math.Round(mamt * (mdiscountper / 100), 2);
                                    mamt = mamt - mdiscountamt;
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt;
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value = Math.Round(mamt, 2).ToString("#0.00");                                   
                                    CalculateAmount();                                   
                                    mpPVC1.IsAllowNewRow = false;
                                }
                            }
                            mpPVC1.MainDataGridCurrentRow.Cells[10].ReadOnly = false;
                            mpPVC1.MainDataGridCurrentRow.Cells[11].ReadOnly = false;
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = false;
                        }
                        else
                        {
                            lblMessage.Text = "Not Enough Stock ";
                            mpPVC1.IsAllowNewRow = false;
                            mpPVC1.MainDataGridCurrentRow.Cells[10].ReadOnly = true;
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = true;
                            mpPVC1.SetFocus(9);
                        }
                    }
                }
                if (colIndex == 10)
                    lblMessage.Text = "[B] Breakage  [E] Expiry [G] Goods Return/Saleable ";

                if (colIndex == 11)
                {

                    if (mpPVC1.MainDataGridCurrentRow.Cells[11].Value.ToString().Trim() == FixAccounts.SubTypeForExpiry || mpPVC1.MainDataGridCurrentRow.Cells[11].Value.ToString().Trim() == FixAccounts.SubTypeForBreakage || mpPVC1.MainDataGridCurrentRow.Cells[11].Value.ToString().Trim() == FixAccounts.SubTypeForGoodsReturn)
                    {
                        if (Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells[9].Value) == 0)
                        {
                            lblMessage.Text = "Enter Quantity";
                            mpPVC1.SetFocus(9);

                        }
                        else if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[8].Value) == 0.00)
                        {
                            lblMessage.Text = "Enter Purchase Rate";
                            mpPVC1.SetFocus(8);

                        }
                        else if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[7].Value) == 0.00)
                        {
                            lblMessage.Text = "Enter MRP";
                            mpPVC1.SetFocus(7);

                        }
                        else if (mpPVC1.MainDataGridCurrentRow.Cells[5].Value == null || mpPVC1.MainDataGridCurrentRow.Cells[4].Value.ToString().Trim() == "")
                        {
                            lblMessage.Text = "Enter Batch Number";
                            mpPVC1.SetFocus(5);

                        }
                        else
                        {
                            bool retValue = false;
                            retValue = CheckForDuplicateProduct();
                            if (retValue == true)
                            {
                                lblMessage.Text = "Product Already Entered";
                                mpPVC1.IsAllowNewRow = false;
                                mpPVC1.SetFocus(11);
                            }
                            else
                            {

                                double mamt = 0;
                                double mqty = 0;
                                double mmrp = 0;
                                double mprate = 0;
                                double mpakn = 0; 
                                if (mpPVC1.MainDataGridCurrentRow.Cells[9].Value != null)
                                    mqty = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[9].Value.ToString());
                                if (mpPVC1.MainDataGridCurrentRow.Cells[8].Value != null)
                                    mprate = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[8].Value.ToString());
                                if (mpPVC1.MainDataGridCurrentRow.Cells[7].Value != null)
                                    mmrp = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[7].Value.ToString());
                                if (mpPVC1.MainDataGridCurrentRow.Cells[2].Value != null)
                                    mpakn = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[2].Value.ToString());
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == FixAccounts.SubTypeForBreakage || mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == FixAccounts.SubTypeForExpiry)
                                    mamt = mqty * (mmrp / mpakn);
                                else
                                    mamt = mqty * (mprate / mpakn);
                                mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value = Math.Round(mamt, 2).ToString("#0.00");
                                CalculateAmount();
                                mpPVC1.IsAllowNewRow = true;
                                lblMessage.Text = "";
                            }

                        }
                    }
                    else
                    {
                        lblMessage.Text = "[B] Breakage  [E] Expiry [G] Goods Return/Saleable ";
                        mpPVC1.IsAllowNewRow = false;
                        mpPVC1.SetFocus(10);
                    }
                }

                if (colIndex == 6)  
                {
                    string newexpiry = "";
                    string newexpirydate = "";

                    int explength = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim().Length;
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim() != "" && explength > 0)
                    {
                        newexpiry = General.GetValidExpiry(mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString());
                        if (newexpiry != "")
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry.ToString();
                            newexpirydate = General.GetValidExpiryDate(newexpiry);
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
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public bool CheckForDuplicateProduct()
        {
            bool retValue = false;
            double mmrp = 0;        
            string mbtno = "";
            string mprodno = "";
            string mcode = "";        
            int mcurrentindex = 0;     
            try
            {
                mcurrentindex = mpPVC1.MainDataGridCurrentRow.Index;
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                    mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                    mbtno = mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim();
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                    double.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value != null)
                    mcode = mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString();                
                foreach (DataGridViewRow drp in mpPVC1.Rows)
                {
                    if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno &&
                          drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno &&
                             drp.Cells["Col_MRP"].Value.ToString().Trim() == mmrp.ToString("#0.00") &&
                              drp.Cells["Col_Code"].Value.ToString() == mcode && drp.Index != mcurrentindex)
                    {
                        retValue = true;
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

        public int GetTotalQuantityForStockID()
        {
            int totalQty = 0;
            double mmrp = 0;      
            int mqty = 0;     
            string mbtno = "";
            string mprodno = "";        
            int mcurrentindex = 0;
            try
            {
                mcurrentindex = mpPVC1.MainDataGridCurrentRow.Index;
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                    mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                    mbtno = mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim();
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                    double.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
                foreach (DataGridViewRow drp in mpPVC1.Rows)
                {
                    if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno &&
                          drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno &&
                            Convert.ToDouble(drp.Cells["Col_MRP"].Value.ToString()) == mmrp)
                    {
                        int.TryParse(drp.Cells["Col_Quantity"].Value.ToString(), out mqty);
                        totalQty += mqty;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return totalQty;
        }

        private void NumberofRows()
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
        private void CalculateAmount()
        {
            double TotalAmount = 0;
            double VatAmount5 = 0;
            double VatAmount12Point5 = 0;
            int itemCount = 0;
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                    {
                        if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 5.00)
                            VatAmount5 += Math.Round((double.Parse(dr.Cells["Col_Amount"].Value.ToString())) * (double.Parse(dr.Cells["Col_VATPer"].Value.ToString())) / 100, 4);
                        else
                        {
                            if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 12.50)
                                VatAmount12Point5 += Math.Round((double.Parse(dr.Cells["Col_Amount"].Value.ToString())) * (double.Parse(dr.Cells["Col_VATPer"].Value.ToString())) / 100, 4);
                        }
                        TotalAmount += double.Parse(dr.Cells["Col_Amount"].Value.ToString());
                        itemCount += 1;
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtVatInput12point5per.Text = Math.Round(VatAmount12Point5, 2).ToString();
                txtVatInput5per.Text = Math.Round(VatAmount5, 2).ToString();
                txtAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");

                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void CalculateDiscount()
        {

            double mdblAmount;
            double.TryParse(txtAmount.Text, out mdblAmount);
            double mdblDiscPer;
            double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
            double mdblDiscAmount;
            double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);

            try
            {

                if (mdblDiscPer > 0)
                {
                    mdblDiscAmount = Math.Round(((mdblAmount) * mdblDiscPer / 100), 2);
                    txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                }

                if (mdblAmount < mdblDiscAmount)
                {
                    txtDiscAmount.Text = "0.00";
                    txtDiscPercent.Text = "0.00";
                    double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);
                    double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
                }
                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void CalculateAllAmounts()
        {

            try
            {
                txtTotalAmount.Text = Math.Round((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtDiscAmount.Text)), 2).ToString("#0.00");
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
                    CalculateAllAmounts();
                    txtDiscAmount.Focus();
                    break;
                case Keys.Down:
                    CalculateAllAmounts();
                    txtDiscAmount.Focus();
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
                        _StockOut.ReadDetailsByVouNumber(vouno);
                        FillSearchData(_StockOut.Id,"");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion             

        private void pnlVouTypeNo_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}