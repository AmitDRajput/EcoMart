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
using System.IO;
using PharmaSYSDistributorPlus.InterfaceLayer.Classes;
using System.Globalization;
using PharmaSYSDistributorPlus.InterfaceLayer.Controls;
using System.Diagnostics;
using EDE2;
using PaperlessPharmaRetail.Common.Classes;
using PharmaSYSDistributorPlus.DataLayer;
using PharmaSYSDistributorPlus.InterfaceLayer;

namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclChallanWithProduct : BaseControl
    {
        #region Declaration

        private Purchase _Purchase;
        private ChallanPurchase _ChallanPurchase;
        //private DataTable _BindingSource;
        //private DataTable _PaymentDetailsBindingSource;
        //private string IfEditPreviousRow = "N";
        private string _LastStockID;
        //private string purchaseType;
        //private string deletedproductname = "";

        //private Form frmView;
        //private string _preID = "";


        //   private bool pnltempoff = false;
        //  private bool IFpnlTempwasvisible = false;

        private ImportBill _ImportBill;
        //  Form frmOpen;


        #endregion

        #region contructor

        public UclChallanWithProduct()
        {
            InitializeComponent();
            _Purchase = new Purchase();
            _ChallanPurchase = new ChallanPurchase();
            _LastStockID = string.Empty;
            _ImportBill = null;
        }

        #endregion

        #region ImportBill

        public ImportBill ImportBillData
        {
            get
            {
                return _ImportBill;
            }
            set
            {
                _ImportBill = value;
            }
        }

        #endregion importBill

        #region IDetail Control

        public override void SetFocus()
        {
            if (_Mode == OperationMode.Add) { }
        }
        public override bool ClearData()
        {
            _Purchase.Initialise();
            ClearControls();
            dgChallanPurchase.Rows.Clear();
            mpMSVC.Rows.Clear();
            return true;
        }
        public override string GetShortcutKeys()
        {
            string keyCollection = "";
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {
                keyCollection = "TAB = Summary     Ctrl+X = Close    Ctrl+S = Save   Ctrl+T = ShortCutPanel  ";
            }
            else
            {
                keyCollection = " Ctrl+H = Search   Ctrl+X = Close    Ctrl+S = Save   Ctrl+T = ShortCutPanel";
            }
            return keyCollection;
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            FillTransactionType();
            FillCreditorCombo();
            FillChallanPurchaseDetail();
            InitializeMainSubViewControl("");
            headerLabel1.Text = "PURCHASE -> NEW";
            return retValue;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            InitializeMainSubViewControl("");
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            ClearData();

            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {

            }
            return retValue;
        }

        public override bool Exit()
        {
            bool retValue = false;
            retValue = base.Exit();
            System.IO.File.Delete(General.GetPurchaseTempFile());
            _ImportBill = null;
            return retValue;
        }
        public override bool IsDetailChanged()
        {
            return true;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            headerLabel1.Text = "Challan Wuth Purchase Bill -> DELETE";
            ClearData();
            return true;
        }

        public override bool ProcessDelete()
        {
            if (_Purchase.AmountClearS != 0)
                MessageBox.Show("Payment Done", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

                if (MessageBox.Show("Are you sure you want to delete Purchase information?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                }
            }
            ClearData();
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            try
            {
                ClearData();
                headerLabel1.Text = "Challan With Purchase Bill -> VIEW";
                tsBtnFifth.Text = "TypeChange";
                tsBtnFifth.Visible = true;

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
                //     GetLastRecord();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;

            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;

            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;

            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;

            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
            {
                if (txtBillAmount.Text != null && Convert.ToDouble(txtBillAmount.Text.ToString()) > 0)
                {

                    _Purchase.TransactionText = cbTransactionType.Text;
                    if (_Purchase.TransactionText != string.Empty)
                    {
                        FixVoucherType();
                        IfAdd();
                    }
                    if (_Mode == OperationMode.Edit)
                        _Purchase.IFEdit = "Y";
                    _Purchase.VoucherSubType = "1";
                    _Purchase.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    _Purchase.Validate();

                    //bool ifTempOK = true;
                    //if (_Purchase.IsValid)
                    //    ifTempOK = CheckforTempPuchaseProducts();
                    //if (ifTempOK == false)
                    //    lblFooterMessage.Text = "Please DO NOT Delete Temporary Purchase Selected Products";
                    //else
                    {

                        if (_Purchase.IsValid)
                        {
                            try
                            {
                                LockTable.LockTablesForPurchase();
                                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                                {
                                    General.BeginTransaction();

                                    _Purchase.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _Purchase.VoucherNumber = _Purchase.GetAndUpdatePurchaseNumber(_Purchase.VoucherType);
                                    _Purchase.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                                    txtVouchernumber.Text = _Purchase.VoucherNumber.ToString();
                                    _Purchase.CreatedBy = General.CurrentUser.Id;
                                    _Purchase.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                    _Purchase.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                                 //   retValue = _Purchase.AddDetails();
                                    _SavedID = _Purchase.Id;
                                    if (retValue)
                                        retValue = UpdatePurchaseID();

                                    if (retValue)
                                    {
                                        if (_Purchase.AmountDebitNoteS > 0 || _Purchase.AmountCreditNoteS > 0)
                                            SaveAndUpdateDebitCreditNote();
                                    }
                                    if (retValue)
                                    {
                                        _Purchase.AddAccountDetails();
                                    }

                                    if (retValue)
                                    {
                                        if (_Purchase.IfCashPaid == "Y")
                                        {
                                            _Purchase.CBId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                            CashPayment _csp = new CashPayment();
                                            _Purchase.CBVouNo = _csp.GetAndUpdateCSPNumber(General.ShopDetail.ShopVoucherSeries);
                                            _Purchase.CBVouType = FixAccounts.VoucherTypeForCashPayment;
                                            retValue = _Purchase.AddCashEntry();
                                        }
                                        else if (_Purchase.ChequeNumber != "" && _Purchase.BankID != "")
                                        {
                                            _Purchase.CBId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                            BankPayment _bkp = new BankPayment();
                                            _Purchase.CBVouNo = _bkp.GetAndUpdateBKPNumber(General.ShopDetail.ShopVoucherSeries);
                                            _Purchase.CBVouType = FixAccounts.VoucherTypeForBankPayment;
                                            retValue = _Purchase.AddBankEntry();
                                        }
                                    }
                                    //if (retValue)
                                    //    RemoveRecordsFromTempPurchase();
                                    if (retValue)
                                        General.CommitTransaction();
                                    else
                                        General.RollbackTransaction();
                                    LockTable.UnLockTables();
                                    if (retValue)
                                    {
                                        // UpdateClosingStockinCache();
                                        System.IO.File.Delete(General.GetPurchaseTempFile());
                                        string msgLine2 = _Purchase.VoucherType + "  " + _Purchase.VoucherNumber.ToString("#0");
                                        PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                        _ImportBill = null;
                                        retValue = true;
                                    }
                                    else
                                    {
                                        PSDialogResult result = PSMessageBox.Show("Could not Save Voucher...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                        retValue = false;
                                    }
                                }
                               
                            }

                            catch (Exception ex)
                            {
                                Log.WriteError(ex.ToString());
                            }
                        }
                        else
                        {
                            StringBuilder _errorMessage = new System.Text.StringBuilder();
                            _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                            foreach (string _message in _Purchase.ValidationMessages)
                            {
                                _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                            }
                            MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            LockTable.UnLockTables();
            CacheObject.Clear("cacheCounterSale");
            return retValue;
        }

        private bool UpdatePurchaseID()
        {
            bool retVal = false;

            _ChallanPurchase.ModifiedBy = General.CurrentUser.Id;
            _ChallanPurchase.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
            _ChallanPurchase.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
            //foreach (DataGridViewRow dr in dgChallanPurchase.Rows)
            //{
            //    if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_ChallanID"].Value)) == false
            //                  && string.Equals(Convert.ToString(dr.Cells["Col_Check"].Value), ((char)0x221A).ToString()) == true)
            //    {
            //        _ChallanPurchase.Id = Convert.ToString(dr.Cells["Col_ChallanID"].Value);
            //        retVal = _ChallanPurchase.UpdatePurchaseDetails(_Purchase.Id,_Purchase.PurchaseBillNumber);
            //    }
            //}
            foreach (DataGridViewRow item in mpMSVC.Rows)
            {
                string DetailID = string.IsNullOrEmpty(Convert.ToString(item.Cells["Col_DetailPurchaseID"].Value)) == true ? string.Empty : Convert.ToString(item.Cells["Col_DetailPurchaseID"].Value);
                if (string.IsNullOrEmpty(DetailID) == false)
                {
                    _Purchase.DetailId = DetailID;
                 //   retVal = _Purchase.UpdatePurchaseIDIDetails();
                    
                }
            }
            return retVal;
        }
        private bool SaveParticularsProductwise()
        {
            bool returnVal = false;
            //    bool IfRecordFound = false;
            _Purchase.SerialNumber = 0;
            int mqty = 0;
            int mrepl = 0;
            int mscm = 0;
            int oldTempStock = 0;
            int CurrentClosingStock = 0;
            string ThisStockID = "";
            //   DataRow dr;
            try
            {
                foreach (DataGridViewRow prodrow in mpMSVC.Rows)
                {
                    mqty = 0;
                    mrepl = 0;
                    mscm = 0;
                    if (prodrow.Cells["Col_Quantity"].Value != null)
                        mqty = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                    if (prodrow.Cells["Col_Replacement"].Value != null)
                        mrepl = Convert.ToInt32(prodrow.Cells["Col_Replacement"].Value.ToString());
                    if (prodrow.Cells["Col_Scheme"].Value != null)
                        mscm = Convert.ToInt32(prodrow.Cells["Col_Scheme"].Value.ToString());
                    if (prodrow.Cells["Col_ProductName"].Value != null && (mqty + mrepl + mscm) > 0)
                    {
                        _Purchase.SerialNumber += 1;
                        _Purchase.ProductID = "";
                        _Purchase.Batchno = "";
                        _Purchase.ProdLoosePack = 0;
                        _Purchase.MRP = 0;
                        _Purchase.Expiry = "";
                        _Purchase.ExpiryDate = "";
                        _Purchase.TradeRate = 0;
                        _Purchase.PurchaseRate = 0;
                        _Purchase.SaleRate = 0;
                        _Purchase.SchemeQuanity = 0;
                        _Purchase.ReplacementQuantity = 0;
                        _Purchase.Quantity = 0;
                        _Purchase.PurchaseVATPercent = 0;
                        _Purchase.ProductVATPercent = 0;
                        _Purchase.ItemDiscountPercent = 0;
                        _Purchase.AmountItemDiscount = 0;
                        _Purchase.AmountSchemeDiscount = 0;
                        _Purchase.AmountCST = 0;
                        _Purchase.SplDiscountPercent = 0;
                        _Purchase.AmountSplDiscountPerUnit = 0;
                        _Purchase.AmountPurchaseVAT = 0;
                        _Purchase.AmountProductVAT = 0;
                        _Purchase.AmountZeroVAT = 0;
                        _Purchase.AmountCashDiscountPerUnit = 0;
                        _Purchase.StockID = "";
                        _Purchase.ShelfID = "";
                        _Purchase.ProductMargin = 0;
                        _Purchase.ProductMargin2 = 0;
                        _Purchase.PurScanCode = string.Empty;

                        //_Purchase.DistributorSaleRate = 0;
                        //_Purchase.DistributorSaleRatePercent = 0;

                        _Purchase.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _Purchase.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                        _Purchase.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        if (prodrow.Cells["Col_UnitOfMeasure"].Value != null)
                            _Purchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Col_UnitOfMeasure"].Value.ToString());
                        if (prodrow.Cells["Col_MRP"].Value != null)
                            _Purchase.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString());
                        if (prodrow.Cells["Col_Pack"].Value != null)
                            _Purchase.Pack = prodrow.Cells["Col_Pack"].Value.ToString();

                        if (prodrow.Cells["Col_Expiry"].Value != null)
                            _Purchase.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();

                        if (_Purchase.Expiry != "00/00")
                        {
                            _Purchase.ExpiryDate = General.GetValidExpiryDate(prodrow.Cells["Col_Expiry"].Value.ToString());
                        }
                        if (prodrow.Cells["Col_TradeRate"].Value != null)
                            _Purchase.TradeRate = Convert.ToDouble(prodrow.Cells["Col_TradeRate"].Value.ToString());
                        if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                            _Purchase.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurchaseRate"].Value.ToString());
                        if (prodrow.Cells["Col_SaleRate"].Value != null)
                            _Purchase.SaleRate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());
                        if (prodrow.Cells["Col_Scheme"].Value != null)
                            _Purchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Col_Scheme"].Value.ToString());
                        if (prodrow.Cells["Col_Replacement"].Value != null)
                            _Purchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Col_Replacement"].Value.ToString());
                        if (prodrow.Cells["Col_Quantity"].Value != null)
                            _Purchase.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        if (prodrow.Cells["Col_VAT"].Value != null && prodrow.Cells["Col_VAT"].Value.ToString() != "")
                            _Purchase.PurchaseVATPercent = Convert.ToDouble(prodrow.Cells["Col_VAT"].Value.ToString());
                        if (prodrow.Cells["Col_ProdVATPer"].Value != null)
                            _Purchase.ProductVATPercent = Convert.ToDouble(prodrow.Cells["Col_ProdVATPer"].Value.ToString());
                        if (prodrow.Cells["Col_ItemDiscountPer"].Value != null)
                            _Purchase.ItemDiscountPercent = Convert.ToDouble(prodrow.Cells["Col_ItemDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Col_ItemDiscountAmount"].Value != null)
                            _Purchase.AmountItemDiscount = Convert.ToDouble(prodrow.Cells["Col_ItemDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_ItemSCMDiscountAmount"].Value != null)
                            _Purchase.AmountSchemeDiscount = Convert.ToDouble(prodrow.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_CSTAmount"].Value != null)
                            _Purchase.AmountCST = Convert.ToDouble(prodrow.Cells["Col_CSTAmount"].Value.ToString());
                        if (prodrow.Cells["Col_SplDiscountPer"].Value != null)
                            _Purchase.SplDiscountPercent = Convert.ToDouble(prodrow.Cells["Col_SplDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Col_SplDiscountAmount"].Value != null)
                            _Purchase.AmountSplDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Col_SplDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_ShelfID"].Value != null)
                            _Purchase.ShelfID = prodrow.Cells["Col_ShelfID"].Value.ToString();
                        if (prodrow.Cells["Col_VATAmountPurchase"].Value != null)
                            _Purchase.AmountPurchaseVAT = Convert.ToDouble(prodrow.Cells["Col_VATAmountPurchase"].Value.ToString());

                        if (prodrow.Cells["Col_VATAmountSale"].Value != null)
                            _Purchase.AmountProductVAT = Convert.ToDouble(prodrow.Cells["Col_VATAmountSale"].Value.ToString());
                        if (prodrow.Cells["Col_CashDiscountAmount"].Value != null)
                            _Purchase.AmountCashDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Col_CashDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_Margin"].Value != null)
                            _Purchase.ProductMargin = Convert.ToDouble(prodrow.Cells["Col_Margin"].Value.ToString());

                        if (prodrow.Cells["Col_Margin2"].Value != null)
                            _Purchase.ProductMargin2 = Convert.ToDouble(prodrow.Cells["Col_Margin2"].Value.ToString());
                        if (_Purchase.ProductMargin < 0)
                            _Purchase.ProductMargin = 0;
                        if (_Purchase.ProductMargin2 < 0)
                            _Purchase.ProductMargin2 = 0;
                        if (prodrow.Cells["Col_StockID"].Value != null && prodrow.Cells["Col_StockID"].Value.ToString() != "")
                            _Purchase.StockID = prodrow.Cells["Col_StockID"].Value.ToString();

                        if (prodrow.Cells["Col_ScanCode"].Value != null && prodrow.Cells["Col_ScanCode"].Value.ToString() != "")
                            _Purchase.PurScanCode = prodrow.Cells["Col_ScanCode"].Value.ToString();
                        _Purchase.Name = prodrow.Cells["Col_ProductName"].Value.ToString();

                        //if (prodrow.Cells["Col_DistributorSaleRate"].Value != null && prodrow.Cells["Col_DistributorSaleRate"].Value.ToString() != string.Empty)
                        //    _Purchase.DistributorSaleRate = Convert.ToDouble(prodrow.Cells["Col_DistributorSaleRate"].Value.ToString());
                        //if (prodrow.Cells["Col_DistributorSaleRatePer"].Value != null && prodrow.Cells["Col_DistributorSaleRatePer"].Value.ToString() != string.Empty)
                        //    _Purchase.DistributorSaleRatePercent = Convert.ToDouble(prodrow.Cells["Col_DistributorSaleRatePer"].Value.ToString());

                        string expdt = "";
                        expdt = _Purchase.ExpiryDate;
                        if (expdt != "")
                        {
                            _Purchase.ExpiryDate = General.GetExpiryInyyyymmddForm(expdt);
                        }

                        ThisStockID = string.Empty;

                    //ss18-10    ThisStockID = _Purchase.CheckForBatchMRPStockIDInStockTable();

                        if (ThisStockID == string.Empty)
                        {
                            ThisStockID = _Purchase.CheckForBatchMRPInStockTable();
                        }

                        if (ThisStockID == string.Empty)
                        {
                            if (prodrow.Cells["Col_IFTempPurchase"].Value != null && prodrow.Cells["Col_IFTempPurchase"].Value.ToString() == "Y")
                                _Purchase.StockID = prodrow.Cells["Col_StockID"].Value.ToString();

                            _Purchase.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            ThisStockID = _Purchase.StockID;
                            _Purchase.PurScanCode = _Purchase.GetScanGodeForCurrentBatch(_Purchase.ProductID);
                            //Create new scancode
                     // ss18-10       returnVal = _Purchase.AddProductDetailsInStockTable();
                        }
                        else
                        {

                            _Purchase.StockID = ThisStockID;
                      //ss18-10      CurrentClosingStock = _Purchase.GetCurrentClosingStock(ThisStockID);
                            oldTempStock = 0;
                            //if (_Mode == OperationMode.Edit)
                            //{
                            //    oldTempStock = GetOldStockFromTempGrid(ThisStockID);
                            //}
                            if (((CurrentClosingStock - (oldTempStock * _Purchase.ProdLoosePack) + ((_Purchase.Quantity + _Purchase.SchemeQuanity + _Purchase.ReplacementQuantity) * _Purchase.ProdLoosePack)) >= 0) || ((CurrentClosingStock - (oldTempStock * _Purchase.ProdLoosePack) + ((_Purchase.Quantity + _Purchase.SchemeQuanity + _Purchase.ReplacementQuantity) * _Purchase.ProdLoosePack)) <= 0 && General.CurrentSetting.MsetSaleAllowNegativeStock == "Y"))
                                returnVal = _Purchase.UpdatePurchaseIntblStock();
                            else
                            {
                                returnVal = false;
                                break;
                            }
                        }

                        if (returnVal)
                        {
                            returnVal = _Purchase.UpdatePurchaseOrder();
                            returnVal = _Purchase.UpdatePurchaseStockInMasterProduct();
                        }

                        else
                            break;
                        if (returnVal)
                        {
                            _Purchase.UpdateLastPurhcaseDataInMasterProduct();
                            _Purchase.RemoveFromShortList(_Purchase.ProductID);
                            _Purchase.GetFirstAndSecondCreditor(_Purchase.ProductID);
                            if (_Purchase.FirstCreditor != _Purchase.AccountID && _Purchase.SecondCreditor != _Purchase.AccountID)
                            {
                                if (_Purchase.FirstCreditor == string.Empty)
                                    _Purchase.FillFirstCreditorInMasterProduct();
                                else if (_Purchase.SecondCreditor == string.Empty)
                                    _Purchase.FillSecondCreditorInMasterProduct();
                            }

                        }
                        else
                            break;


                        if (returnVal)
                            returnVal = _Purchase.AddProductDetailsSS();
                        else
                            break;
                    }
                }
            }
            catch { returnVal = false; }
            return returnVal;
        }
     
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {

                }
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        private void InitializeMainSubViewControl(string vmode)
        {

            try
            {
                ConstructMainColumns();
                //ConstructSubColumns();
                //ConstructBatchGrid();
                //ConstructLastPurchaseColumns();
                //ConstructBarCodeColumns();

                DataTable dtable = new DataTable();
                if (vmode == "C")
                {
                    dtable = _Purchase.ReadProductDetailsByIDForChanged();
                    headerLabel1.Text = "Debtor Sale => Changed Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else if (vmode == "D")
                {
                    dtable = _Purchase.ReadProductDetailsByIDForDeleted();
                    headerLabel1.Text = "Debtor Sale => Deleted Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else
                    dtable = _Purchase.ReadProductDetailsByID();

                if (dtable != null)
                    _Purchase.NoofRows = dtable.Rows.Count;

                psLableWithBorder1.Text = _Purchase.NoofRows.ToString();
                mpMSVC.DataSourceMain = dtable;

                string tempFileName = General.GetPurchaseTempFile();

                if (_Mode == OperationMode.Add && File.Exists(tempFileName))
                {
                    mpMSVC.DataSourceMain = null;
                    mpMSVC.Rows.Clear();

                    DataSet ds = new DataSet();
                    ds.ReadXml(tempFileName);
                    mpMSVC.DataSourceMain = ds.Tables[0];

                }

                mpMSVC.NumericColumnNames.Add("Col_Quantity");
                mpMSVC.DoubleColumnNames.Add("Col_MRP");
                mpMSVC.DoubleColumnNames.Add("Col_VATPer");
                mpMSVC.DoubleColumnNames.Add("Col_VAT");
                mpMSVC.DoubleColumnNames.Add("Col_PurchaseRate");
                mpMSVC.DoubleColumnNames.Add("Col_Amount");
                mpMSVC.DoubleColumnNames.Add("Col_SaleRate");
                mpMSVC.DoubleColumnNames.Add("Col_TradeRate");
                mpMSVC.DoubleColumnNames.Add("Col_Amount");
                //DataTable dt = PharmaSYSDistributorPlusCache.GetProductData();
                Product prod = new Product();
                DataTable dt = prod.GetOverviewData();
                //  DataTable dt = General.ProductList;
                mpMSVC.DataSource = dt;
                mpMSVC.Bind();
                if (_Mode == OperationMode.Add && File.Exists(tempFileName))
                {
                    CalculateTotals();
                    mpMSVC.Rows.Add();
                    mcbCreditor.Focus();
                }
                mpMSVC.ClearSelection();
                // GotoLastRow();
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructMainColumns()
        {
            mpMSVC.ColumnsMain.Clear();
            try
            {
                DataGridViewTextBoxColumn column;

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DetailPurchaseID";
                column.DataPropertyName = "DetailPurchaseID";
                column.HeaderText = "DetailPurchaseID";
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 192;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UnitOfMeasure";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 45;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Company";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Co.";
                column.Width = 35;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Box1";
                column.HeaderText = "Box";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdVATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT %";
                column.Width = 85;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFBarCodeRequired";
                column.DataPropertyName = "ProdIfBarCodeRequired";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber1";
                column.HeaderText = "Batch";
                column.Width = 105;
                column.ReadOnly = true;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 90;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Exp";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "Trd.Rate";
                column.Width = 74;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 74;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT";
                column.DataPropertyName = "PurchaseVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 55;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "Scm";
                column.Width = 45;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Replacement";
                column.DataPropertyName = "ReplacementQuantity";
                column.HeaderText = "Repl";
                column.Width = 40;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountPer";
                column.DataPropertyName = "ItemDiscountPercent";
                column.HeaderText = "Disc";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 93;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountAmount";
                column.DataPropertyName = "AmountItemDiscount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SplDiscountPer";
                column.DataPropertyName = "SpecialDiscountPercent";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SplDiscountAmount";
                column.DataPropertyName = "AmountSpecialDiscount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountPurchase";
                column.DataPropertyName = "AmountPurchaseVAT";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CSTAmount";
                column.DataPropertyName = "AmountCST";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CSTPer";
                column.DataPropertyName = "CSTPercent";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmount";
                column.DataPropertyName = "AmountSchemeDiscount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmountPerUnit";
                column.DataPropertyName = "SchemeDiscountPercent";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfOctroi";
                column.DataPropertyName = "ProdIfOctroi";
                column.Width = 40;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreditNoteAmount";
                column.DataPropertyName = "AmountCreditNote";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CashDiscountAmount";
                column.DataPropertyName = "AmountCashDiscount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompID";
                column.DataPropertyName = "ProdCompID";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountSale";
                column.DataPropertyName = "AmountProdVAT";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorSaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorSaleRatePer";
                column.DataPropertyName = "DistributorSaleRatePer";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfCode";
                column.DataPropertyName = "ShelfCode";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfID";
                column.DataPropertyName = "ProdShelfID";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin";
                column.DataPropertyName = "Margin";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin2";
                column.DataPropertyName = "MarginAfterDiscount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScanCode";
                column.DataPropertyName = "ScanCode";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFTempPurchase";
                //column.DataPropertyName = "ScanCode";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorProductID";
                //   column.DataPropertyName = "DistributorProductID";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion Idetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
        {

            try
            {
                FillTransactionType();
                FillCreditorCombo();
                FillChallanPurchaseDetail();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool RefreshProductList()
        {
            DataTable dtable = new DataTable();
            Product prod = new Product();
            dtable = prod.GetOverviewData();
            //mpMSVC.DataSource = dtable;
            //mpMSVC.BindGridSub();
            return true;
        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;

            try
            {
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                }
                if (keyPressed == Keys.B && modifier == Keys.Alt)
                {
                    txtBillNumber.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    this.mcbCreditor.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                }
                if (keyPressed == Keys.E && modifier == Keys.Alt)
                { }
                if (keyPressed == Keys.I && modifier == Keys.Alt)
                {
                }
                if (keyPressed == Keys.L && modifier == Keys.Alt)
                {
                }
                if (keyPressed == Keys.M && modifier == Keys.Alt)
                {
                }
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                { }
                if (keyPressed == Keys.O && modifier == Keys.Alt)
                {
                    if (pnlDebitCreditNote.Visible == true)
                    {
                        btnCRDBOKClick();
                        retValue = true;
                    }
                }
                if (keyPressed == Keys.P && modifier == Keys.Alt)
                {
                    txtSplDiscountS.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.Q && modifier == Keys.Alt)
                { }
                if (keyPressed == Keys.R && modifier == Keys.Alt)
                { }

                if (keyPressed == Keys.S && modifier == Keys.Alt)
                {
                    btnSummary.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                }

                if (keyPressed == Keys.Z && modifier == Keys.Alt)
                { }
                if (keyPressed == Keys.Escape)
                {
                    if (pnlDebitCreditNote.Visible)
                    {
                        btnCRDBOKClick();
                        retValue = true;
                    }
                    else if (pnlSummary.Visible)
                    {
                        btnCancelSClick();
                        dgChallanPurchase.Focus();
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

        #endregion

        #region FillData

        private void FillCreditorCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[6] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccDiscountOffered", "AccAddress2" };
                mcbCreditor.ColumnWidth = new string[6] { "0", "20", "200", "150", "50", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
                mcbCreditor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private DataTable FillChallanPurchaseDetail()
        {
            bool retValue = false;
            DataTable dt = new DataTable();
            try
            {
                ConstructChallanPurchaseColumns();
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    ChallanPurchase dbChlnpur = new ChallanPurchase();

                    dt = dbChlnpur.GetOverviewDataForProductwiseBills(mcbCreditor.SelectedID);
                    if (dt != null)
                        retValue = BindChallanPurchaseDetail(dt);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.FillChallanPurchaseDetails>>" + Ex.Message);
            }
            return dt;
        }
        private void FillTransactionType()
        {
            cbTransactionType.Items.Clear();
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
            if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
        }
        private bool BindChallanPurchaseDetail(DataTable dt)
        {
            bool retValue = true;
            try
            {

                if (dgChallanPurchase != null)
                    dgChallanPurchase.Rows.Clear();
                int _RowIndex = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    _RowIndex = dgChallanPurchase.Rows.Add();
                    DataGridViewRow currentdr = dgChallanPurchase.Rows[_RowIndex];
                    currentdr.Cells["Col_ChallanID"].Value = dr["ChallanID"].ToString();
                   // currentdr.Cells["Col_Check"].Value = string.Empty;
                    currentdr.Cells["Col_ChallanNumber"].Value = dr["ChallanNumber"].ToString();
                    currentdr.Cells["Col_DetailPurchaseID"].Value = setdata(dr["DetailPurchaseID"]);
                    currentdr.Cells["Col_ProductID"].Value = setdata(dr["ProductID"]);
                    currentdr.Cells["Col_ProductName"].Value = setdata(dr["ProdName"]);
                    currentdr.Cells["Col_Company"].Value = setdata(dr["ProdCompID"]);

                    currentdr.Cells["Col_UnitOfMeasure"].Value = setdata(dr["Prodloosepack"]);
                    currentdr.Cells["Col_Pack"].Value = setdata(dr["prodpack"]);

                    currentdr.Cells["Col_Quantity"].Value = setdata(dr["Quantity"]);
                    currentdr.Cells["Col_BatchNumber"].Value = setdata(dr["BatchNumber"]);
                    currentdr.Cells["Col_Expiry"].Value = setdata(dr["Expiry"]);
                    currentdr.Cells["Col_ExpiryDate"].Value = setdata(dr["ExpiryDate"]);
                    currentdr.Cells["Col_TradeRate"].Value = setdata(dr["TradeRate"]);
                    currentdr.Cells["Col_MRP"].Value = setdata(dr["MRP"]);
                    currentdr.Cells["Col_PurchaseRate"].Value = setdata(dr["PurchaseRate"]);
                    currentdr.Cells["Col_SaleRate"].Value = setdata(dr["SaleRate"]);
                    currentdr.Cells["Col_VAT"].Value = setdata(dr["PurchaseVATPercent"]);
                    currentdr.Cells["Col_VATAmountPurchase"].Value = setdata(dr["AmountPurchaseVAT"]);
                    currentdr.Cells["Col_Scheme"].Value = setdata(dr["SchemeQuantity"]);
                    currentdr.Cells["Col_Replacement"].Value = setdata(dr["ReplacementQuantity"]);
                    currentdr.Cells["Col_ItemDiscountPer"].Value = setdata(dr["ItemDiscountPercent"]);
                    currentdr.Cells["Col_Amount"].Value = setdata(dr["Amount"]);
                    currentdr.Cells["Col_ItemDiscountAmount"].Value = setdata(dr["AmountItemDiscount"]);
                    currentdr.Cells["Col_ItemSCMDiscountAmount"].Value = setdata(dr["AmountSchemeDiscount"]);
                    currentdr.Cells["Col_CSTAmount"].Value = setdata(dr["AmountCST"]);
                    currentdr.Cells["Col_ProdVATPer"].Value = setdata(dr["ProdVATPercent"]);
                    currentdr.Cells["Col_VATAmountSale"].Value = setdata(dr["AmountProdVAT"]);
                    currentdr.Cells["Col_SpldiscountPer"].Value = setdata(dr["SchemeDiscountPercent"]);
                    currentdr.Cells["Col_SplDiscountAmount"].Value = setdata(dr["AmountSpecialDiscount"]);
                    currentdr.Cells["Col_CashDiscountAmount"].Value = setdata(dr["AmountCashDiscount"]);
                    currentdr.Cells["Col_Margin"].Value = setdata(dr["Margin"]);
                    currentdr.Cells["Col_Margin2"].Value = setdata(dr["MarginAfterDiscount"]);

                    currentdr.Cells["Col_DistributorSaleRate"].Value = setdata(dr["DistributorSaleRate"]);
                    currentdr.Cells["Col_DistributorSaleRatePer"].Value = setdata(dr["DistributorSaleRatePer"]);
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;
            }
            return retValue;
        }

        private void ConstructChallanPurchaseColumns()
        {
            dgChallanPurchase.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ChallanID";
                column.DataPropertyName = "ID";
                column.HeaderText = "ChallanID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = "Check";
                column.Width = 50;
                column.Visible = true;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DetailPurchaseID";
                column.DataPropertyName = "DetailPurchaseID";
                column.HeaderText = "DetailPurchaseID";
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ChallanNumber";
                column.DataPropertyName = "ChallanNumber";
                column.HeaderText = "ChallanNumber";
                column.Width = 150;
                column.Visible = true;
                column.ReadOnly = true;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 300;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UnitOfMeasure";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 45;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Company";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Co.";
                column.Width = 35;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Box1";
                column.HeaderText = "Box";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdVATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT %";
                column.Width = 85;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFBarCodeRequired";
                column.DataPropertyName = "ProdIfBarCodeRequired";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber1";
                column.HeaderText = "Batch";
                column.Width = 105;
                column.ReadOnly = true;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 90;
                column.ReadOnly = true;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Exp";
                column.Width = 60;
                column.ReadOnly = true;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "Trd.Rate";
                column.Width = 74;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 74;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT";
                column.DataPropertyName = "PurchaseVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 55;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "Scm";
                column.Width = 45;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Replacement";
                column.DataPropertyName = "ReplacementQuantity";
                column.HeaderText = "Repl";
                column.Width = 40;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountPer";
                column.DataPropertyName = "ItemDiscountPercent";
                column.HeaderText = "Disc";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 93;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountAmount";
                column.DataPropertyName = "AmountItemDiscount";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SplDiscountPer";
                column.DataPropertyName = "SpecialDiscountPercent";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SplDiscountAmount";
                column.DataPropertyName = "AmountSpecialDiscount";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountPurchase";
                column.DataPropertyName = "AmountPurchaseVAT";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CSTAmount";
                column.DataPropertyName = "AmountCST";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CSTPer";
                column.DataPropertyName = "CSTPercent";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmount";
                column.DataPropertyName = "AmountSchemeDiscount";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmountPerUnit";
                column.DataPropertyName = "SchemeDiscountPercent";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfOctroi";
                column.DataPropertyName = "ProdIfOctroi";
                column.Width = 40;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreditNoteAmount";
                column.DataPropertyName = "AmountCreditNote";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CashDiscountAmount";
                column.DataPropertyName = "AmountCashDiscount";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompID";
                column.DataPropertyName = "ProdCompID";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountSale";
                column.DataPropertyName = "AmountProdVAT";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorSaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorSaleRatePer";
                column.DataPropertyName = "DistributorSaleRatePer";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);
                
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin";
                column.DataPropertyName = "Margin";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin2";
                column.DataPropertyName = "MarginAfterDiscount";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);
                
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFTempPurchase";
                //column.DataPropertyName = "ScanCode";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorProductID";
                //   column.DataPropertyName = "DistributorProductID";
                column.Width = 80;
                column.Visible = false;
                dgChallanPurchase.Columns.Add(column);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        #region Summary&calc

        private void btnSummary_Click(object sender, EventArgs e)
        {
            BtnSummaryClicked();
        }
        private void BtnSummaryClicked()
        {
            DataTable dt = new DataTable();
            try
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                {
                    //  txtDBAmountS.Text = "0.00";
                    //  txtCRAmountS.Text = "0.00";
                    dt = FillCreditDebitNote();
                    pnlBillDetails.Enabled = false;
                    dgChallanPurchase.Enabled = false;
                    pnlSummary.Location = GetpnlSummaryLocation();
                    pnlSummary.BringToFront();
                    pnlSummary.Visible = true;
                    pnlDebitCreditNote.BringToFront();
                    dgCreditNote.Visible = true;
                    if (_Mode == OperationMode.Edit || cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                        pnlBank.Visible = false;
                    if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _Purchase.StatementNumber > 0)
                        pnlSummary.Enabled = false;
                    else
                        pnlSummary.Enabled = true;

                    //FillPurchaseDetailData();
                    CalculateGetSummaryData();
                    txtCRAmountS.Focus();
                    btnSummary.Enabled = false;

                    if (pnlDebitCreditNote.Visible == false)
                    {
                        tsBtnSave.Enabled = true;

                    }
                    CalculateFinalSummary();
                    if (_Purchase.StatementNumber > 0)
                        tsBtnSave.Enabled = false;
                }
                else
                {
                    pnlBillDetails.Enabled = false;
                    dgChallanPurchase.Enabled = false;
                    GetpnlSummaryLocation();
                    pnlSummary.BringToFront();
                    pnlSummary.Enabled = false;
                    //FillPurchaseDetailData();
                    CalculateGetSummaryData();
                    CalculateFinalSummary();
                    txtCRAmountS.Focus();
                    btnSummary.Enabled = false;
                    pnlSummary.Visible = true;
                }
                if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _Mode == OperationMode.Delete)
                    btnCancelS.Visible = false;
                else
                    btnCancelS.Visible = true;
            }

            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.btnSummary_Click>>" + Ex.Message);
            }
        }

        private void FillPurchaseDetailData()
        {
            try
            {
                DataGridViewRow dr = dgChallanPurchase.CurrentRow;
                DataTable dt = new DataTable();
                if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_ChallanID"].Value)) == false
                    && string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_DetailPurchaseID"].Value)) == false)
                {
                    if (string.Equals(Convert.ToString(dr.Cells["Col_Check"].Value), ((char)0x221A).ToString()) == true)
                    {
                        _ChallanPurchase.Id = Convert.ToString(dr.Cells["Col_ChallanID"].Value);
                        _ChallanPurchase.DetailId = Convert.ToString(dr.Cells["Col_DetailPurchaseID"].Value);
                        dt = _ChallanPurchase.ReadProductDetailsByIDPurchaseAndDetailID();
                        FillDataPurDeatil(dt);
                    }
                    else
                        foreach (DataGridViewRow item in mpMSVC.Rows)
                        {
                            if (string.Equals(Convert.ToString(dr.Cells["Col_DetailPurchaseID"].Value), Convert.ToString(item.Cells["Col_DetailPurchaseID"].Value)) == true)
                            {
                                mpMSVC.Rows.Remove(item);
                                break;
                            }
                        }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.btnSummary_Click>>" + Ex.Message);
            }
        }
        private void FillDataPurDeatil(DataTable dt)
        {
            foreach (DataRow item in dt.Rows)
            {
                int rowindex = mpMSVC.Rows.Add();
                DataGridViewRow currentdr = mpMSVC.Rows[rowindex];
                currentdr.Cells["Col_DetailPurchaseID"].Value = setdata(item["DetailPurchaseID"]);
                currentdr.Cells["Col_ProductID"].Value = setdata(item["ProductID"]);
                currentdr.Cells["Col_ProductName"].Value = setdata(item["ProdName"]);
                currentdr.Cells["Col_Company"].Value = setdata(item["ProdCompID"]);

                currentdr.Cells["Col_UnitOfMeasure"].Value = setdata(item["Prodloosepack"]);
                currentdr.Cells["Col_Pack"].Value = setdata(item["prodpack"]);

                currentdr.Cells["Col_Quantity"].Value = setdata(item["Quantity"]);
                currentdr.Cells["Col_BatchNumber"].Value = setdata(item["BatchNumber"]);
                currentdr.Cells["Col_Expiry"].Value = setdata(item["Expiry"]);
                currentdr.Cells["Col_ExpiryDate"].Value = setdata(item["ExpiryDate"]);
                currentdr.Cells["Col_TradeRate"].Value = setdata(item["TradeRate"]);
                currentdr.Cells["Col_MRP"].Value = setdata(item["MRP"]);
                currentdr.Cells["Col_PurchaseRate"].Value = setdata(item["PurchaseRate"]);
                currentdr.Cells["Col_SaleRate"].Value = setdata(item["SaleRate"]);
                currentdr.Cells["Col_VAT"].Value = setdata(item["PurchaseVATPercent"]);
                currentdr.Cells["Col_VATAmountPurchase"].Value = setdata(item["AmountPurchaseVAT"]);
                currentdr.Cells["Col_Scheme"].Value = setdata(item["SchemeQuantity"]);
                currentdr.Cells["Col_Replacement"].Value = setdata(item["ReplacementQuantity"]);
                currentdr.Cells["Col_ItemDiscountPer"].Value = setdata(item["ItemDiscountPercent"]);
                currentdr.Cells["Col_Amount"].Value = setdata(item["Amount"]);
                currentdr.Cells["Col_ItemDiscountAmount"].Value = setdata(item["AmountItemDiscount"]);
                currentdr.Cells["Col_ItemSCMDiscountAmount"].Value = setdata(item["AmountSchemeDiscount"]);
                currentdr.Cells["Col_CSTAmount"].Value = setdata(item["AmountCST"]);
                currentdr.Cells["Col_ProdVATPer"].Value = setdata(item["ProdVATPercent"]);
                currentdr.Cells["Col_VATAmountSale"].Value = setdata(item["AmountProdVAT"]);
                currentdr.Cells["Col_SpldiscountPer"].Value = setdata(item["SchemeDiscountPercent"]);
                currentdr.Cells["Col_SplDiscountAmount"].Value = setdata(item["AmountSpecialDiscount"]);
                currentdr.Cells["Col_CashDiscountAmount"].Value = setdata(item["AmountCashDiscount"]);
                currentdr.Cells["Col_Margin"].Value = setdata(item["Margin"]);
                currentdr.Cells["Col_Margin2"].Value = setdata(item["MarginAfterDiscount"]);
                // currentdr.Cells["Col_SplDiscountAmount"].Value = txtSplDiscountPerUnit.Text.ToString();
                currentdr.Cells["Col_StockID"].Value = setdata(item["StockID"]);
                currentdr.Cells["Col_ScanCode"].Value = setdata(item["ScanCode"]);

                currentdr.Cells["Col_ShelfID"].Value = setdata(item["ShelfCode"]);
                currentdr.Cells["Col_DistributorSaleRate"].Value = setdata(item["DistributorSaleRate"]);
                currentdr.Cells["Col_DistributorSaleRatePer"].Value = setdata(item["DistributorSaleRatePer"]);

            }
        }
        private object setdata(object Data)
        {
            if (Data == null)
            {
                Data = string.Empty;
            }
            return Data;
        }

        private DataTable FillCreditDebitNote()
        {
            bool retValue = false;
            DataTable dt = new DataTable();
            try
            {
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    ConstructCreditNoteColumns();
                    //   dgCreditNote.DoubleColumnNames.Add("Col_AmountNet");
                    Purchase crdb = new Purchase();

                    dt = crdb.GetOverviewDataForPurchase(mcbCreditor.SelectedID, _Purchase.Id);
                    if (dt != null)
                        retValue = BindCreditNoteDebitNote(dt);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.FillCreditDebitNote>>" + Ex.Message);
            }
            return dt;
        }
        private bool BindCreditNoteDebitNote(DataTable dt)
        {
            bool retValue = true;
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
                    currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    {
                        voudt = dr["VoucherDate"].ToString();
                        voudt = General.GetDateInShortDateFormat(voudt);
                    }
                    currentdr.Cells["Col_VoucherDate"].Value = voudt;
                    currentdr.Cells["Col_AmountNet"].Value = dr["AmountNet"].ToString();
                    currentdr.Cells["Col_Narr"].Value = dr["Narration"].ToString();
                    if (dr["AmountClear"] != DBNull.Value && dr["AmountClear"].ToString() != "")
                        double.TryParse(dr["AmountClear"].ToString(), out amtclear);
                    currentdr.Cells["Col_AmountClear"].Value = dr["AmountClear"].ToString();
                    //if (_Mode == OperationMode.Delete)
                    //    currentdr.Cells["Col_Check"].Value = false;
                    //else if (amtclear != 0)
                    //    currentdr.Cells["Col_Check"].Value = true;

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
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = "Check";
                column.Width = 15;
                column.Visible = true;
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
        private Point GetpnlSummaryLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = dgChallanPurchase.Location.X + 305;
                pt.Y = dgChallanPurchase.Location.Y - 28;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        private void CalculateTotals()
        {
            // check for inpurstring not in correct format???

            double mtotamt = 0;
            double mamt = 0;
            int itemCount = 0;
            double mmargin = 0;
            double mmargin2 = 0;
            double mpurrate = 1;
            double msalerate = 1;
            double mvatamt = 0;
            double mtraterate = 0;
            //if (txtPurchaseRate.Text != null && txtPurchaseRate.Text.ToString() != string.Empty)
            //    double.TryParse(txtPurchaseRate.Text.ToString(), out mpurrate);
            //if (txtSaleRate.Text != null && txtSaleRate.Text.ToString() != string.Empty)
            //    double.TryParse(txtSaleRate.Text.ToString(), out msalerate);
            //if (txtPurchaseVATAmt.Text != null && txtPurchaseVATAmt.Text.ToString() != string.Empty)
            //    double.TryParse(txtPurchaseVATAmt.Text.ToString(), out mvatamt);
            //if (txtTradeRate.Text != null && txtTradeRate.Text.ToString() != string.Empty)
            //    double.TryParse(txtTradeRate.Text.ToString(), out mtraterate);
            //if (txtMRP.Text != null && txtMRP.Text != string.Empty)
            //    double.TryParse(txtMRP.Text.ToString(), out mmrp);
            try
            {
                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    if (dr.Cells["Col_MRP"].Value != null && dr.Cells["Col_MRP"].Value.ToString().Trim() != "0.00" && dr.Cells["Col_MRP"].Value.ToString() != "")
                    {
                        itemCount += 1;
                        if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != string.Empty)
                        {
                            double.TryParse(dr.Cells["Col_Amount"].Value.ToString(), out mamt);
                            mtotamt += mamt;
                        }
                    }
                    txtGridAmountTot.Text = mtotamt.ToString("#0.00");
                    psLableWithBorder1.Text = itemCount.ToString().Trim();
                }
                if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                {
                    if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                    {
                        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / (mtraterate + mvatamt), 2);
                        mmargin2 = Math.Round((msalerate - mpurrate) / mpurrate, 2);
                    }
                    else
                    {
                        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / msalerate, 2);
                        mmargin2 = Math.Round((msalerate - mpurrate) / msalerate, 2);
                    }
                }
                else
                {
                    if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                    {
                        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / (mtraterate + mvatamt), 2);
                        mmargin2 = Math.Round((msalerate - mpurrate) / mpurrate, 2);
                    }
                    else
                    {
                        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / msalerate, 2);
                        mmargin2 = Math.Round((msalerate - mpurrate) / msalerate, 2);
                    }
                }
                mmargin = Math.Round(mmargin * 100, 2);
                mmargin2 = Math.Round(mmargin2 * 100, 2);
                //txtMargin.Text = mmargin.ToString("#0.00");
                //txtMargin2.Text = mmargin2.ToString("#0.00");
                if (mtotamt > 0)
                    btnSummary.Enabled = true;
                else
                    btnSummary.Enabled = false;

                CalculateGetSummaryData();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.CalculateTotals>>" + Ex.Message);
            }
        }
        private void CalculateGetSummaryData()
        {

            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {
                double mtotamt = 0;
                double mtotscm = 0;
                double mtotitem = 0;
                double mtotvat5 = 0;
                double mtotvat12point5 = 0;
                double mvatamount = 0;
                double mamt = 0;
                double mamts = 0;
                double mvatper = 0;
                double mqty = 0;
                double mtotvatzeroamt = 0;
                double moctroiamt = 0;
                _Purchase.AmountCashDiscountS = 0;
                double mtotspldisc = 0;
                double mpuramount0 = 0;
                double mpuramount5 = 0;
                double mpuramount12point5 = 0;
                double mtotamtbymrp = 0;
                double mtotamtbypurrate = 0;
                double mmrp = 0;
                double mprate = 0;
                int muom = 1;
                double puramt = 0;

                try
                {
                    foreach (DataGridViewRow dr in mpMSVC.Rows)
                    {
                        if (dr.Cells["Col_MRP"].Value != null && dr.Cells["Col_MRP"].Value.ToString().Trim() != "")
                        {
                            mprate = 0;
                            muom = 1;//Col_UnitOfMeasure
                            puramt = 0;

                            double.TryParse(dr.Cells["Col_MRP"].Value.ToString(), out mmrp);
                            if (dr.Cells["Col_PurchaseRate"].Value != null && dr.Cells["Col_PurchaseRate"].Value.ToString().Trim() != "")
                                double.TryParse(dr.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                            if (dr.Cells["Col_UnitOfMeasure"].Value != null && dr.Cells["Col_UnitOfMeasure"].Value.ToString().Trim() != "")
                                int.TryParse(dr.Cells["Col_UnitOfMeasure"].Value.ToString(), out muom);
                            if (dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != string.Empty)
                                mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                            mtotamtbymrp += Math.Round(mqty * (mmrp / muom), 2);
                            mtotamtbypurrate += Math.Round(mqty * (mprate / muom), 2);
                            if (dr.Cells["Col_VAT"].Value != null && dr.Cells["Col_VAT"].Value.ToString() != string.Empty)
                            {
                                double.TryParse(dr.Cells["Col_VAT"].Value.ToString(), out mvatper);
                            }
                            if (dr.Cells["Col_VATAmountPurchase"].Value != null && dr.Cells["Col_VATAmountPurchase"].Value.ToString() != string.Empty)
                            {
                                double.TryParse(dr.Cells["Col_VATAmountPurchase"].Value.ToString(), out mvatamount);
                            }
                            mvatamount = Math.Round(mvatamount * mqty, 2);
                            if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != string.Empty)
                            {
                                double.TryParse(dr.Cells["Col_Amount"].Value.ToString(), out mamts);
                                mtotamt += mamts;
                                puramt = mamts;

                            }
                            mamt = 0;
                            // Col_ItemSCMDiscountAmount

                            if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null && dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString() != "")
                            {
                                double.TryParse(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString(), out mamt);
                                mtotscm += mamt;
                                puramt -= Math.Round(mamt, 2);
                            }
                            mamt = 0;
                            if (dr.Cells["Col_ItemDiscountAmount"].Value != null && dr.Cells["Col_ItemDiscountAmount"].Value.ToString() != "")
                            {
                                double.TryParse(dr.Cells["Col_ItemDiscountAmount"].Value.ToString(), out mamt);
                                if (mamt > 0)
                                    mtotitem += Math.Round(mamt * mqty, 2);
                                puramt -= Math.Round(mamt * mqty, 2);
                            }
                            mamt = 0;
                            if (dr.Cells["Col_CashDiscountAmount"].Value != null && dr.Cells["Col_CashDiscountAmount"].Value.ToString() != "")
                            {
                                double.TryParse(dr.Cells["Col_CashDiscountAmount"].Value.ToString(), out mamt);
                                _Purchase.AmountCashDiscountS += mamt;
                                puramt -= Math.Round(mamt, 2);
                            }

                            mamt = 0;
                            if (dr.Cells["Col_SplDiscountAmount"].Value != null && dr.Cells["Col_SplDiscountAmount"].Value.ToString() != "")
                            {
                                double.TryParse(dr.Cells["Col_SplDiscountAmount"].Value.ToString(), out mamt);
                                mtotspldisc += mamt;
                                puramt -= Math.Round(mamt * mqty, 2);
                            }
                            mamt = 0;
                            //if (General.CurrentSetting.MsetPurchaseIfProductWithOctroi == "Y")
                            //{
                            //    if (dr.Cells["Col_IfOctroi"].Value != null && dr.Cells["Col_IfOctroi"].Value.ToString() == "Y")
                            //        moctroiamt += mamt;
                            //}
                            //else
                            //{
                            //    if (General.CurrentSetting.MsetPurchaseOctroionZeroVAT == "Y")
                            //    {
                            //        if (dr.Cells["Col_VAT"].Value != null && dr.Cells["Col_VAT"].Value.ToString() != "" && Convert.ToDouble(dr.Cells["Col_VAT"].Value.ToString()) == 0)
                            //            moctroiamt += mamt;
                            //    }
                            //    else
                                    moctroiamt += mamt;
                            }



                            if (mvatper == 0)
                            {
                                mtotvatzeroamt += puramt;
                                mpuramount0 += puramt;
                            }
                            else if (mvatper == 13.5 || mvatper == 12.5)
                            {
                                mtotvat12point5 += mvatamount;
                                mpuramount12point5 += puramt;
                            }
                            else
                            {
                                mtotvat5 += mvatamount;
                                mpuramount5 += puramt;
                            }

                        }
                    }
                    //if (Convert.ToDouble(txtGridAmountTot.Text.ToString()) != _Purchase.AmountBillS)
                    //{
                    _Purchase.TotalAmountForOctroiS = moctroiamt;
                    txtBillAmountS.Text = mtotamt.ToString("#0.00");
                    txtBillAmount.Text = mtotamt.ToString("#0.00");
                    txtItemDiscountS.Text = mtotitem.ToString("#0.00");
                    txtSplDiscPerS.Text = _Purchase.SpecialDiscountPercentS.ToString("#0.00");

                    txtCashDiscountAmountS.Text = _Purchase.AmountCashDiscountS.ToString("0.00");
                    txtPreCashDiscountAmountS.Text = _Purchase.AmountCashDiscountS.ToString("#0.00");
                    txtSchemeDiscountS.Text = mtotscm.ToString("#0.00");
                    txtVAT5AmountS.Text = mtotvat5.ToString("#0.00");
                    txtVAT12Point5AmountS.Text = mtotvat12point5.ToString("#0.00");
                    txtViewVat5per.Text = mtotvat5.ToString("#0.00");
                    txtViewVat12point5per.Text = mtotvat12point5.ToString("#0.00");
                    txtPurchaseAmountVAT12point5S.Text = mpuramount12point5.ToString("#0.00");
                    txtPurchaseAmountVAT5S.Text = mpuramount5.ToString("#0.00");
                    txtPurchaseAmountVATZeroS.Text = mpuramount0.ToString("#0.00");
                    double mtotprofit = 0;
                    if (mtotamtbypurrate > 0)
                        mtotprofit = Math.Round(((mtotamtbymrp - mtotamtbypurrate) / mtotamtbypurrate) * 100, 2);
                    txtProfitPerS.Text = mtotprofit.ToString("#0.00");
                    CalculateFinalVAT();
                    CalculateTotalVATAmount();
                    //}
                }

                catch (Exception Ex)
                {
                    Log.WriteError("UclChallanWithBillNumber.CalculateGetSummaryData>>" + Ex.Message);
                }
            }
        }
        public void CalculateTotalVATAmount()
        {
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {


                double mtotvat5 = 0;
                double mtotvat12point5 = 0;
                double mtotvat = 0;
                try
                {
                    if (txtVAT5AmountS.Text != null && txtVAT5AmountS.Text.ToString() != "")
                        double.TryParse(txtVAT5AmountS.Text.ToString(), out mtotvat5);
                    if (txtVAT12Point5AmountS.Text != null && txtVAT12Point5AmountS.Text.ToString() != "")
                        double.TryParse(txtVAT12Point5AmountS.Text.ToString(), out mtotvat12point5);
                    mtotvat = Math.Round(mtotvat5, 2) + Math.Round(mtotvat12point5, 2);
                    txtTotalVATAmountS.Text = (mtotvat).ToString("0.00");
                }
                catch (Exception Ex)
                {
                    Log.WriteError("UclChallanWithBillNumber.CalculateTotalVATAmount>>" + Ex.Message);
                }
            }
        }
        private void CalculateFinalSummary()
        {
            _Purchase.AmountBillS = Convert.ToDouble(txtBillAmountS.Text.ToString());
            try
            {

                if (_Purchase.AmountBillS > 0)
                {
                    if (txtSchemeDiscountS.Text.ToString().Trim() != "")
                        _Purchase.AmountSchemeDiscountS = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
                    if (txtItemDiscountS.Text.ToString().Trim() != "")
                        _Purchase.AmountItemDiscountS = Convert.ToDouble(txtItemDiscountS.Text.ToString());
                    if (txtSplDiscountS.Text.ToString().Trim() != "")
                        _Purchase.AmountSpecialDiscountS = Convert.ToDouble(txtSplDiscountS.Text.ToString());

                    _Purchase.SpecialDiscountPercentS = Math.Round((100 * _Purchase.AmountSpecialDiscountS) / (_Purchase.AmountBillS - _Purchase.AmountItemDiscountS - _Purchase.AmountSchemeDiscountS - _Purchase.AmountOctroiS), 6);

                    if (txtAddOnS.Text.ToString().Trim() != "")
                        _Purchase.AmountAddOnFreightS = Convert.ToDouble(txtAddOnS.Text.ToString());
                    if (txtLessS.Text.ToString().Trim() != "")
                        _Purchase.AmountLessS = Convert.ToDouble(txtLessS.Text.ToString());
                    if (txtCRAmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountCreditNoteS = Convert.ToDouble(txtCRAmountS.Text.ToString());
                    if (txtDBAmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountDebitNoteS = Convert.ToDouble(txtDBAmountS.Text.ToString());
                    if (txtCashDiscountPerS.Text.ToString().Trim() != "")
                        _Purchase.CashDiscountPercentageS = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
                    if (txtCashDiscountAmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountCashDiscountS = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
                    if (_Purchase.AmountBillS - _Purchase.AmountSpecialDiscountS - _Purchase.AmountSchemeDiscountS - _Purchase.AmountItemDiscountS + _Purchase.AmountCreditNoteS <= _Purchase.AmountDebitNoteS)
                    {
                        lblFooterMessage.Text = "Invalid Debit Note Amount";
                        _Purchase.AmountDebitNoteS = 0;
                        txtDBAmountS.Text = "0.00";
                        ClearDebitCreditNoteWhenAmountIsLess();
                    }
                    if (_Purchase.AmountCashDiscountS > (_Purchase.AmountBillS - _Purchase.AmountSpecialDiscountS - _Purchase.AmountSchemeDiscountS - _Purchase.AmountItemDiscountS + _Purchase.AmountCreditNoteS - _Purchase.AmountDebitNoteS))
                    {
                        lblFooterMessage.Text = "Invalid Cash Discount";
                        _Purchase.CashDiscountPercentageS = 0;
                        _Purchase.AmountCashDiscountS = 0;
                        txtCashDiscountAmountS.Text = "0.00";
                        txtPreCashDiscountAmountS.Text = "0.00";
                    }
                    txtCashDiscountAmountS.Text = _Purchase.AmountCashDiscountS.ToString("#0.00");
                    txtPreCashDiscountAmountS.Text = _Purchase.AmountCashDiscountS.ToString("#0.00");
                    if (txtCashDiscountAmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountCashDiscountS = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
                    if (txtVAT5AmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountVAT5PercentS = Convert.ToDouble(txtVAT5AmountS.Text.ToString());
                    if (txtVAT12Point5AmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountVAT12point5PercentS = Convert.ToDouble(txtVAT12Point5AmountS.Text.ToString());

                    if (txtOCTPerS.Text.ToString().Trim() != "")
                        _Purchase.OctroiPercentageS = Convert.ToDouble(txtOCTPerS.Text.ToString());
                    if (_Purchase.OctroiPercentageS > 0)
                        _Purchase.AmountOctroiS = Math.Round(_Purchase.TotalAmountForOctroiS * _Purchase.OctroiPercentageS / 100, 2);
                    _Purchase.AmountS = Math.Round(_Purchase.AmountBillS - _Purchase.AmountSchemeDiscountS - _Purchase.AmountItemDiscountS
                        - _Purchase.AmountSpecialDiscountS + _Purchase.AmountAddOnFreightS - _Purchase.AmountLessS + _Purchase.AmountCreditNoteS
                        - _Purchase.AmountDebitNoteS - _Purchase.AmountCashDiscountS + _Purchase.AmountVAT5PercentS
                        + _Purchase.AmountVAT12point5PercentS + _Purchase.AmountOctroiS, 2);
                    CalculateRoundup();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.CalculateFinalSummary>>" + Ex.Message);
            }

        }
        private void CalculateRoundup()
        {
            try
            {
                txtTotalS.Text = _Purchase.AmountS.ToString("#0.00");
                if (cbRound.Checked == true)
                    _Purchase.RoundUpAmountS = Math.Round(_Purchase.AmountS, 0) - _Purchase.AmountS;
                else
                    _Purchase.RoundUpAmountS = 0;
                _Purchase.AmountNetS = _Purchase.AmountS + _Purchase.RoundUpAmountS;
                txtNetAmountS.Text = _Purchase.AmountNetS.ToString("#0.00");
                txtBillAmount.Text = _Purchase.AmountNetS.ToString("#0.00");
                txtRoundUPS.Text = _Purchase.RoundUpAmountS.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.CalculateRoundup>>" + Ex.Message);
            }
        }
        private void CalculateFinalVAT()
        {
            double mtotdisczero = 0;
            double mtotdisc5 = 0;
            double mtotdisc12point5 = 0;
            double mtotdiscother = 0;
            double mtotcashdiscount = 0;
            double mmstamt5 = 0;
            double mmstamt12point5 = 0;
            double mmstamtother = 0;
            double mtotmstzero = 0;
            double mtotmst5 = 0;
            double mtotmst12point5 = 0;
            double mtotmstother = 0;
            int mqty = 0;
            double mskl = 0;
            double mscmdisc = 0;
            double mitm = 0;
            double msplddx = 0;
            double mcrddx = 0;
            double mddx = 0;
            double mtt1 = 0;
            double mtt1S = 0;
            double mmstperpur = 0;

            double mpuramountzero = 0;
            //  double mpuramount0 = 0;
            double mpuramount5 = 0;
            double mpuramount12point5 = 0;
            double mamt = 0;
            double mtotalvat = 0;
            if (txtCashDiscountPerS.Text != "")
                _Purchase.CashDiscountPercentageS = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
            try
            {

                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != string.Empty && Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString()) > 0)
                    {
                        int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out mqty);
                        mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null && dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString() != string.Empty)
                            mscmdisc = Convert.ToDouble(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());
                        if (dr.Cells["Col_VAT"].Value != null && dr.Cells["Col_VAT"].Value.ToString() != "")
                            mmstperpur = Convert.ToDouble(dr.Cells["Col_VAT"].Value.ToString());
                        mskl = Math.Round(mamt - mscmdisc, 2);
                        mitm = Math.Round((mskl * Convert.ToDouble(dr.Cells["Col_ItemDiscountPer"].Value.ToString())) / 100, 2); //4
                        msplddx = Math.Round(((mskl - mitm) * _Purchase.SpecialDiscountPercentS) / 100, 2); //4
                        mcrddx = Math.Round(((mskl - mitm) * _Purchase.CreditNoteDiscountPercentS) / 100, 2); //4
                        mddx = Math.Round(Math.Round((mskl - msplddx - mitm) * _Purchase.CashDiscountPercentageS, 2) / 100, 2); //4
                        mtt1 = Math.Round((mamt - mddx - msplddx - mcrddx - mscmdisc - mitm) * (mmstperpur / 100), 2); //4
                        mtt1S = mtt1;
                        mtt1 = Math.Round(mtt1 / mqty, 2); //4
                        mtotalvat += mtt1;
                        dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();
                        dr.Cells["Col_CreditNoteAmount"].Value = mcrddx.ToString();
                        dr.Cells["Col_CashDiscountAmount"].Value = mddx.ToString();
                        mtotcashdiscount += mddx;
                        dr.Cells["Col_VATAmountPurchase"].Value = mtt1.ToString();
                        dr.Cells["Col_SplDiscountPer"].Value = _Purchase.SpecialDiscountPercentS.ToString();
                        //   dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();
                        if (mmstperpur == 0)
                        {
                            mpuramountzero += mamt;
                            mtotmstzero += mtt1S;
                            mtotdisczero += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }
                        //vat 5.5
                        else if (mmstperpur == 6 || mmstperpur == 5.5)
                        {
                            mmstamt5 += Math.Round(mamt - mddx - msplddx - mcrddx - mscmdisc - mitm, 2);
                            mpuramount5 += Math.Round(mamt - mddx - msplddx - mcrddx - mscmdisc - mitm, 2);
                            mtotmst5 += mtt1S;
                            mtotdisc5 += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }
                        else if (mmstperpur == 13.5 || mmstperpur == 12.5)
                        {
                            mmstamt12point5 += Math.Round(mamt - mddx - msplddx - mcrddx - mscmdisc - mitm, 2);
                            mpuramount12point5 += Math.Round(mamt - mddx - msplddx - mcrddx - mscmdisc - mitm, 2);
                            mtotmst12point5 += mtt1S;
                            mtotdisc12point5 += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }
                        else
                        {
                            mmstamtother += Math.Round(mamt - mddx - msplddx - mcrddx - msplddx - mitm, 2);
                            mtotmstother += mtt1S;
                            mtotdiscother += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }

                    }

                    mtotalvat = mtotmst5 + mtotmst12point5 + mtotmstother;

                }
                txtVAT5AmountS.Text = mtotmst5.ToString("0.00");
                txtVAT12Point5AmountS.Text = mtotmst12point5.ToString("#0.00");
                txtViewVat5per.Text = mtotmst5.ToString("0.00");
                txtViewVat12point5per.Text = mtotmst12point5.ToString("#0.00");
                txtTotalVATAmountS.Text = (mtotmst5 + mtotmst12point5).ToString("#0.00");
                //  txtPurchaseAmountVAT5S.Text = mmstamt5.ToString("0.00");
                //  txtPurchaseAmountVAT12point5S.Text = mmstamt12point5.ToString("0.00");
                // txtPurchaseAmountVATZeroS.Text = mpuramountzero.ToString("#0.00");
                txtCashDiscountAmountS.Text = mtotcashdiscount.ToString("#0.00");
                txtPreCashDiscountAmountS.Text = mtotcashdiscount.ToString("#0.00");
                //txtpuramount0.Text = mpuramount0.ToString("0.00");
                // txtpuramount5.Text = mpuramount5.ToString("0.00");
                // txtpuramount12point5.Text = mpuramount12point5.ToString("#0.00");


            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.CalculateFinalVAT>>" + Ex.Message);
            }
        }
        private void ClearDebitCreditNoteWhenAmountIsLess()
        {
            string mvoutype = "";
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
                        if (mvoutype == FixAccounts.VoucherTypeForDebitNoteStock || mvoutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                            crdbdr.Cells["Col_Check"].Value = string.Empty;
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
                //txtCRAmountSelected.Text = mcrnoteamt.ToString("#0.00");
                //txtDNAmountSelected.Text = mdbnoteamt.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
     
        private void CalculatePurRateSaleRateAmountforFullGrid()
        {
            double mqty = 0;
            try
            {
                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    mqty = 0;
                    if (dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "")
                        mqty = Convert.ToInt16(dr.Cells["Col_Quantity"].Value.ToString());
                    if (mqty > 0)
                    {

                        double mprate = 0;
                        double mtraderate = 0;
                        double mpurvatamt = 0;
                        double mcstamt = 0;
                        double mmstamtbySale = 0;
                        double mscmqty = 0;
                        double mscmdiscper = 0;
                        double mscmamt = 0;
                        double mitemdiscper = 0;
                        double mitemdiscamt = 0;
                        double mtraderateafterscm = 0;
                        double mcashdiscper = 0;
                        _Purchase.AmountCashDiscountPerUnit = 0;
                        _Purchase.AmountSplDiscountPerUnit = 0;
                        _Purchase.SchemeDiscountPercent = 0;
                        _Purchase.AmountScmDiscountPerUnit = 0;
                        _Purchase.AmountSchemeDiscount = 0;
                        double mspldiscper = 0;
                        double mspldiscamt = 0;
                        double moctamt = 0;
                        double msalerate = 0;
                        double mpurvatper = 0;
                        double msalevatper = 0;
                        double msalevatamt = 0;
                        double mamt = 0;
                        double mamtzerovat = 0;
                        double mskl = 0;
                        double mmrp = 0;
                        double mmargin = 0;
                        double mmargin2 = 0;
                        double mpurrate = 0;

                        double.TryParse(dr.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                        double.TryParse(dr.Cells["Col_MRP"].Value.ToString(), out mmrp);
                        double.TryParse(dr.Cells["Col_ItemDiscountPer"].Value.ToString(), out mitemdiscper);
                        if (dr.Cells["Col_ItemSCMDiscountAmountPerUnit"].Value != null)
                            double.TryParse(dr.Cells["Col_ItemSCMDiscountAmountPerUnit"].Value.ToString(), out mscmdiscper);
                        if (txtSplDiscPerS.Text != null && txtSplDiscountS.Text.ToString() != string.Empty)
                            double.TryParse(txtSplDiscPerS.Text.ToString(), out mspldiscper);
                        double.TryParse(dr.Cells["Col_VAT"].Value.ToString(), out mpurvatper);
                        double.TryParse(dr.Cells["Col_ProdVATPer"].Value.ToString(), out msalevatper);
                        double.TryParse(txtCashDiscountPerS.Text.ToString(), out mcashdiscper);
                        if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null && dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString() != string.Empty)
                            double.TryParse(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString(), out mscmamt);
                        double.TryParse(txtSplDiscountS.Text.ToString(), out mspldiscamt);
                        double.TryParse(dr.Cells["Col_PurchaseRate"].Value.ToString(), out mpurrate);
                        mamt = Math.Round(mqty * mtraderate, 2); //4
                        mskl = Math.Round(mamt - mscmamt, 2); //4
                        _Purchase.AmountSchemeDiscount = mscmamt;
                        _Purchase.SchemeDiscountPercent = mscmdiscper;

                        if (mqty > 0)
                        {
                            mitemdiscamt = Math.Round((((mskl) * mitemdiscper / 100) / mqty), 2); //4
                            mspldiscper = Math.Round((100 * mspldiscamt) / (mamt - mitemdiscamt - mscmamt - moctamt), 2); //4
                            _Purchase.AmountScmDiscountPerUnit = Math.Round(_Purchase.AmountSchemeDiscount / mqty, 2); //4


                            _Purchase.AmountSplDiscountPerUnit = Math.Round((((mskl - mitemdiscamt) * mspldiscper) / 100) / mqty, 2); //4
                            _Purchase.AmountCashDiscountPerUnit = Math.Round((((mskl - _Purchase.AmountSplDiscountPerUnit - mitemdiscamt) * mcashdiscper) / 100) / mqty, 2); //4
                        }
                        double.TryParse(dr.Cells["Col_CSTAmount"].Value.ToString(), out mcstamt);
                        double.TryParse(dr.Cells["Col_Scheme"].Value.ToString(), out mscmqty);
                        if (mqty > 0)
                            mpurvatamt = Math.Round(((mamt - moctamt - _Purchase.AmountCashDiscountPerUnit - _Purchase.AmountSplDiscountPerUnit - mscmamt - mitemdiscamt) / mqty) * mpurvatper / 100, 2); //4

                        msalevatamt = Math.Round((mmrp * msalevatper) / 100, 2); //4
                        if ((mqty + mscmqty) > 0)
                            mtraderateafterscm = Math.Round((mtraderate * mqty) / (mqty + mscmqty), 2); //4
                        if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                            mprate = mtraderateafterscm + mpurvatamt + mcstamt - _Purchase.AmountScmDiscountPerUnit - mitemdiscamt - _Purchase.AmountCashDiscountPerUnit - _Purchase.AmountSplDiscountPerUnit;
                        else
                            mprate = mtraderateafterscm + mcstamt - _Purchase.AmountScmDiscountPerUnit - mitemdiscamt - _Purchase.AmountCashDiscountPerUnit - _Purchase.AmountSplDiscountPerUnit;
                        if (General.CurrentSetting.MsetPurchaseAddVATInSaleRate == "Y")
                            msalerate = mmrp + mmstamtbySale + mcstamt;
                        else
                            msalerate = mmrp;
                        mamt = Math.Round(mqty * mtraderate, 2);
                        if (mpurvatper == 0)
                            mamtzerovat = mamt - mitemdiscamt - mscmamt;
                        else
                            mamtzerovat = 0;

                        if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                        {
                            if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                            {
                                mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / (mtraderate + mpurvatamt), 2);
                                mmargin2 = Math.Round((msalerate - mprate) / mprate, 2);
                            }
                            else
                            {
                                mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / msalerate, 2);
                                mmargin2 = Math.Round((msalerate - mprate) / msalerate, 2);
                            }
                        }
                        else
                        {
                            if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                            {
                                mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / (mtraderate + mpurvatamt), 2);
                                mmargin2 = Math.Round((msalerate - mprate) / mprate, 2);
                            }
                            else
                            {
                                mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / msalerate, 2);
                                mmargin2 = Math.Round((msalerate - mprate) / msalerate, 2);
                            }
                        }
                        mmargin = Math.Round(mmargin * 100, 2);
                        mmargin2 = Math.Round(mmargin2 * 100, 2);


                        dr.Cells["Col_ItemDiscountAmount"].Value = mitemdiscamt.ToString("#0.0000");
                        dr.Cells["Col_ItemSCMDiscountAmount"].Value = mscmamt.ToString("#0.00");
                        dr.Cells["Col_VATAmountPurchase"].Value = mpurvatamt.ToString("#0.0000");
                        dr.Cells["Col_VATAmountSale"].Value = msalevatamt.ToString("#0.0000");
                        dr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                        dr.Cells["Col_SaleRate"].Value = msalerate.ToString("#0.00");
                        dr.Cells["Col_SplDiscountPer"].Value = _Purchase.AmountSplDiscountPerUnit.ToString("0.00");
                        dr.Cells["Col_CashDiscountAmount"].Value = _Purchase.AmountCashDiscountPerUnit.ToString("0.00");
                        dr.Cells["Col_PurchaseRate"].Value = mprate.ToString("#0.00");
                        dr.Cells["Col_Margin"].Value = mmargin.ToString("#0.00");
                        dr.Cells["Col_Margin2"].Value = mmargin2.ToString("#0.00");
                    }
                }
                CalculateTotals();

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.CalculatePurRateSaleRateAmountforFullGrid>>" + Ex.Message);
            }
        }
        #endregion

        #region UIEvents

        private void cbTransactionType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbCreditor.Focus();
        }
        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtBillNumber.Text.ToString().Trim() != "")
                    {
                        bool retValue = true;
                        Purchase purbill = new Purchase();
                        _Purchase.PurchaseBillNumber = txtBillNumber.Text.ToString().Trim();
                        if (_Mode == OperationMode.Add)
                            retValue = purbill.CheckForUniqueBillNumberforNew(_Purchase.PurchaseBillNumber, _Purchase.AccountID);
                        else
                            retValue = purbill.CheckForUniqueBillNumberforEdit(_Purchase.Id, _Purchase.PurchaseBillNumber, _Purchase.AccountID);
                        if (retValue == false)
                        {
                            lblFooterMessage.Text = "Purchase Number Already Entered";
                            txtBillNumber.Focus();
                        }
                        else
                        {
                            lblFooterMessage.Text = "";
                            txtNarration.Enabled = true;
                            datePickerBillDate.Focus();

                        }
                    }
                }
                else if (e.KeyCode == Keys.Up)
                    mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtNarration_KeyDown>>" + Ex.Message);
            }
        }
        private void txtNarration_Enter(object sender, EventArgs e)
        {
            if (txtBillNumber.Text == null || txtBillNumber.Text.ToString() == "")
                txtBillNumber.Focus();
        }
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtCashDiscountPerS.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtBillNumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtNarration_KeyDown>>" + Ex.Message);
            }
        }
        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            FillChallanPurchaseDetail();
            txtBillNumber.Focus();
        }

        private void dgChallanPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string ifchecked = string.Empty;
                if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (dgChallanPurchase.Rows.Count > 0 && string.IsNullOrEmpty(Convert.ToString(dgChallanPurchase.CurrentRow.Cells["Col_Check"].Value)) == false)
                            ifchecked = dgChallanPurchase.CurrentRow.Cells["Col_Check"].Value.ToString();
                        if (ifchecked != string.Empty)
                            dgChallanPurchase.CurrentRow.Cells["Col_Check"].Value = string.Empty;
                        else if (string.IsNullOrEmpty(Convert.ToString(dgChallanPurchase.CurrentRow.Cells[0].Value)) == false)
                            dgChallanPurchase.CurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();
                        else
                            btnSummary.Focus();
                        FillPurchaseDetailData();
                        CalculateTotals();
                        //CalculateGetSummaryData();
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void txtCRAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtVAT5AmountS.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtCRAmountS.Focus();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtDBAmountS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtVAT5AmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtVAT12Point5AmountS.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtDBAmountS.Focus();
                CalculateTotalVATAmount();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtVAT5AmountS_KeyDown>>" + Ex.Message);
            }
        }
        
        private void txtVAT12Point5AmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtAddOnS.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtVAT5AmountS.Focus();
                CalculateTotalVATAmount();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtVAT12Point5AmountS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtAddOnS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    CalculateFinalSummary();
                    txtLessS.Focus();
                }
                else if (e.KeyCode == Keys.Up)
                    txtVAT12Point5AmountS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtAddOnS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtLessS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    CalculateFinalSummary();
                    txtCashDiscountAmountS.Focus();
                }
                else if (e.KeyCode == Keys.Up)
                    txtAddOnS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtAddOnS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtCashDiscountAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    txtOCTPerS.Focus();
                    double billamt = Convert.ToDouble(txtBillAmountS.Text.ToString());
                    double scmamt = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
                    double itemamt = Convert.ToDouble(txtItemDiscountS.Text.ToString());
                    double discamt = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
                    double actualdiscamountper = 0;
                    if (txtCashDiscountPerS.Text.ToString() != string.Empty)
                        actualdiscamountper = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
                    double entereddiscper = Math.Round((discamt * 100) / (billamt - scmamt - itemamt), 2);
                    if (((entereddiscper) > (actualdiscamountper + 0.20)) || ((entereddiscper) < (actualdiscamountper - 0.20)))
                    {
                        txtCashDiscountPerS.Text = entereddiscper.ToString("#0.00");
                        lblFooterMessage.Text = "Press Enter..";
                    }
                    CalculateFinalSummary();
                    CalculateFinalVAT(); // [ansuman]
                }
                else if (e.KeyCode == Keys.Up)
                {
                    CalculateFinalSummary();
                    txtLessS.Focus();
                }

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtOCTPerS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtOCTAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    btnCancelS.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtOCTPerS.Focus();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtOCTAmountS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtOCTPerS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtOCTAmountS.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtAddOnS.Focus();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtOCTPerS_KeyDown>>" + Ex.Message);
            }
        }

        private void btnCancelS_Click(object sender, EventArgs e)
        {
            btnCancelSClick();

        }
        private void btnCancelSClick()
        {
            try
            {
                pnlSummary.Visible = false;
                pnlSummary.SendToBack();
                btnSummary.Enabled = true;
                dgChallanPurchase.BringToFront();
                dgChallanPurchase.Visible = true;
                if (_Purchase.IfTypeChange == "N")
                {
                    pnlBillDetails.Enabled = true;
                    dgChallanPurchase.Enabled = true;
                    //tsBtnSave.Enabled = false;
                }
                if (txtGridAmountTot.Text != null && txtGridAmountTot.Text != "")
                    btnSummary.Enabled = true;
                //dgChallanPurchase.SetFocus(1);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.btnCancelS_Click>>" + Ex.Message);
            }
        }
        private void dgCreditNote_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    pnlDebitCreditNote.Visible = false;
                }

                string ifchecked = string.Empty;
                if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (dgCreditNote.CurrentRow.Cells["Col_Check"].Value != null && dgCreditNote.CurrentRow.Cells["Col_Check"].Value.ToString() != string.Empty)
                            ifchecked = dgCreditNote.CurrentRow.Cells["Col_Check"].Value.ToString();
                        if (ifchecked != string.Empty)
                            dgCreditNote.CurrentRow.Cells["Col_Check"].Value = string.Empty;
                        else
                            dgCreditNote.CurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();

                        CalculateCRDBSelectedAmount();
                    }
                }
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
                        else
                            mdbnoteamt += mamt;
                    }
                }
                txtCRAmountS.Text = mcrnoteamt.ToString("#0.00");
                txtDBAmountS.Text = mdbnoteamt.ToString("#0.00");
                pnlDebitCreditNote.Visible = false;
                CalculateFinalSummary();
                tsBtnSave.Enabled = true;
                pnlSummary.BringToFront();
                pnlSummary.Visible = true;
                pnlSummary.Focus();
                txtCRAmountS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.btnCRDBOK_Click>>" + Ex.Message);
            }
        }
        private void cbTransactionType_Enter(object sender, EventArgs e)
        {
            pnlType.BackColor = Color.Blue;
        }

        private void cbTransactionType_Leave(object sender, EventArgs e)
        {
            pnlType.BackColor = Color.White;
        }
        private void mcbCreditor_UpArrowPressed(object sender, EventArgs e)
        {
            cbTransactionType.Focus();
        }
        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbCreditor.SelectedID;
            FillCreditorCombo();
            mcbCreditor.SelectedID = selectedId;
            txtBillNumber.Focus();
        }

        private void UclChallanWithBillNumber_Load(object sender, EventArgs e)
        {
            if (_ImportBill == null)
            {
                if (_Mode != OperationMode.ReportView)
                {
                    FillTransactionType();
                    datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                }
            }
            else if (_Mode == OperationMode.Add && _ImportBill.TotalAmount == string.Empty)
            {
                FillTransactionType();
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
            }
        }
        private void btnCRDBNote_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDebitCreditNote.BringToFront();
                pnlDebitCreditNote.Visible = true;
                dgCreditNote.Visible = true;
                dgCreditNote.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.btnCRDBNote_Click>>" + Ex.Message);
            }
        }

        private void datePickerBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            bool retValue = false;
            if (e.KeyCode == Keys.Enter)
            {
                string billDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(billDate, billDate);
                if (retValue)
                {
                    lblFooterMessage.Text = "";
                    txtNarration.Focus();
                }
                else
                {
                    lblFooterMessage.Text = "Check Date";
                    datePickerBillDate.Focus();
                }
            }
        }
        private void txtCashDiscountPerS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                CalculatePurRateSaleRateAmountforFullGrid();
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
                {
                    lblFooterMessage.Text = string.Empty;
                    txtSplDiscountS.Focus();
                }
                else if (e.KeyCode == Keys.Up)
                    txtNarration.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtCashDiscountPerS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtPreCashDiscountAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
                {
                    lblFooterMessage.Text = string.Empty;
                    CalculatePurRateSaleRateAmountforFullGrid();
                    txtSplDiscountS.Focus();
                }
                else if (e.KeyCode == Keys.Up)
                    txtCashDiscountPerS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtCashDiscountPerS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtSplDiscountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    CalculatePurRateSaleRateAmountforFullGrid();
                    if (dgChallanPurchase.Rows.Count > 0)
                    {
                        dgChallanPurchase.Focus();
                        dgChallanPurchase.CurrentRow.Selected = true;
                    }
                }
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                    txtCashDiscountPerS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclChallanWithBillNumber.txtSpecialDiscountS_KeyDown>>" + Ex.Message);
            }
        }

        #endregion UIEvents

        #region Function 

        public void FixVoucherType()
        {
            _Purchase.EntryDate = Convert.ToString(DateTime.Now);

            FixVoucherTypeBycbTransactionType();

            if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCashPurchase)
                _Purchase.PurchaseAccount = FixAccounts.AccountCashPurchase;
            else if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCreditStatementPurchase)
                _Purchase.PurchaseAccount = FixAccounts.AccountCreditPurchase;
            else if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCreditPurchase)
                _Purchase.PurchaseAccount = FixAccounts.AccountCashCreditPurchase;
            txtVouType.Text = _Purchase.VoucherType;
            if (mcbCreditor.SelectedID != null)
                _Purchase.AccountID = this.mcbCreditor.SelectedID;
            _Purchase.PurchaseBillNumber = txtBillNumber.Text;
        }
        private void FixVoucherTypeBycbTransactionType()
        {
            _Purchase.VoucherType = "";
            if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
            else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
                _Purchase.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
            else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
            txtVouType.Text = _Purchase.VoucherType;
        }
        public void IfAdd()
        {
            _Purchase.AmountItemDiscountS = Convert.ToDouble(txtItemDiscountS.Text.ToString());
            if (txtSplDiscountS.Text.ToString() != "")
                _Purchase.AmountSpecialDiscountS = Convert.ToDouble(txtSplDiscountS.Text.ToString());
            _Purchase.AmountSchemeDiscountS = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
            if (txtCashDiscountPerS.Text != string.Empty)
                _Purchase.CashDiscountPercentageS = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
            _Purchase.AmountCashDiscountS = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
            if (txtAddOnS.Text.ToString() != "")
                _Purchase.AmountAddOnFreightS = Convert.ToDouble(txtAddOnS.Text.ToString());
            if (txtLessS.Text.ToString() != "")
                _Purchase.AmountLessS = Convert.ToDouble(txtLessS.Text.ToString());
            if (txtCRAmountS.Text.ToString() != "")
                _Purchase.AmountCreditNoteS = Convert.ToDouble(txtCRAmountS.Text.ToString());
            if(string.IsNullOrEmpty(txtDBAmountS.Text.Trim()) == false)
                _Purchase.AmountDebitNoteS = Convert.ToDouble(txtDBAmountS.Text.ToString());
            if (txtOCTPerS.Text != "")
                _Purchase.OctroiPercentageS = Convert.ToDouble(txtOCTPerS.Text.ToString());
            if (txtOCTAmountS.Text != "")
                _Purchase.AmountOctroiS = Convert.ToDouble(txtOCTAmountS.Text.ToString());
            _Purchase.Narration = txtNarration.Text.ToString();
            _Purchase.RoundUpAmountS = Convert.ToDouble(txtRoundUPS.Text.ToString());
            _Purchase.AmountVAT5PercentS = Convert.ToDouble(txtVAT5AmountS.Text.ToString());
            _Purchase.AmountVAT12point5PercentS = Convert.ToDouble(txtVAT12Point5AmountS.Text.ToString());
            if (txtPurchaseAmountVATZeroS.Text != null && txtPurchaseAmountVATZeroS.Text != "")
                _Purchase.PurchaseAmountZeroVATS = Convert.ToDouble(txtPurchaseAmountVATZeroS.Text.ToString());
            //if (txtpuramount0.Text.ToString() != "")
            //    _Purchase.PurchaseAmount0PercentVATS = Convert.ToDouble(txtPurchaseAmountVATZeroS.Text.ToString());
            if (txtPurchaseAmountVAT5S.Text.ToString() != "")
                _Purchase.PurchaseAmount5PercentVATS = Convert.ToDouble(txtPurchaseAmountVAT5S.Text.ToString());
            if (txtPurchaseAmountVAT12point5S.Text.ToString() != "")
                _Purchase.PurchaseAmount12point5PercentVATS = Convert.ToDouble(txtPurchaseAmountVAT12point5S.Text.ToString());

            if (_Mode == OperationMode.Add)
            {
                _Purchase.CBVouType = "";
                _Purchase.IfCashPaid = "N";
                if (txtChequeNumber.Text != null && txtChequeNumber.Text != "")
                {
                    _Purchase.ChequeNumber = txtChequeNumber.Text.ToString();

                    if (mcbBank.SelectedID != null)
                    {
                        _Purchase.BankID = mcbBank.SelectedID;
                    }
                    _Purchase.ChequeDate = datePickerChqDate.Value.Date.ToString("yyyyMMdd");
                }


                if (_Purchase.IfCashPaid == "Y" || (_Purchase.ChequeNumber != "" && _Purchase.BankID != ""))
                {
                    _Purchase.AmountClearS = _Purchase.AmountNetS;
                }

            }

        }
        private bool SaveAndUpdateDebitCreditNote()
        {
            {
                bool returnVal = true;
                try
                {
                    foreach (DataGridViewRow prodrow in dgCreditNote.Rows)
                    {
                        if ((prodrow.Cells["Col_CrdbID"].Value) != null && (Convert.ToBoolean(prodrow.Cells["Col_Check"].Value.ToString() != string.Empty)))
                        {
                            _Purchase.CreditDebitNoteID = prodrow.Cells["Col_CrdbID"].Value.ToString();
                            _Purchase.Amount = Convert.ToDouble(prodrow.Cells["Col_AmountNet"].Value.ToString());
                            returnVal = _Purchase.UpdateCreditDebitNoteAdjustedDetails(_Purchase.CreditDebitNoteID, _Purchase.Amount, _Purchase.VoucherType, _Purchase.VoucherNumber, _Purchase.VoucherDate, _Purchase.PurchaseBillNumber, _Purchase.Id, _Purchase.VoucherSeries);
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
        }
        private void ClearControls()
        {
            tsBtnPrint.Visible = false;
            try
            {
                pnlSummary.Visible = false;
                lblPurchaseBillFormat.Text = string.Empty;
                btnSummary.BackColor = Color.Linen;
                txtVouchernumber.Clear();
                tsBtnSavenPrint.Enabled = false;
                txtBillNumber.Clear();
                datePickerChqDate.ResetText();
                txtChequeNumber.Clear();
                txtNarration.Text = "";
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtBillAmountS.Text = "0.00";
                txtSchemeDiscountS.Text = "0.00";
                txtItemDiscountS.Text = "0.00";
                txtSplDiscountS.Text = "0.00";
                txtSplDiscPerS.Text = "";
                txtAddOnS.Text = "0.00";
                txtCRAmountS.Text = "0.00";
                txtDBAmountS.Text = "0.00";
                txtCashDiscountPerS.Text = "0.00";
                txtCashDiscountAmountS.Text = "0.00";
                txtPreCashDiscountAmountS.Text = "0.00";
                txtVAT5AmountS.Text = "0.00";
                txtVAT12Point5AmountS.Text = "0.00";
                txtViewVat5per.Text = "0.00";
                txtViewVat12point5per.Text = "0.00";
                txtTotalVATAmountS.Text = "0.00";
                txtOCTPerS.Text = "0.00";
                txtOCTAmountS.Text = "0.00";
                txtRoundUPS.Text = "0.00";
                txtBillAmount.Text = "0.00";
                txtpuramount12point5.Text = "0.00";
                txtpuramount5.Text = "0.00";
                txtpuramount0.Text = "0.00";
                txtOCTPerS.Text = "0.00";
                mcbCreditor.SelectedID = "";
                txtChequeNumber.Text = "";
                mcbBank.SelectedID = "";
                txtGridAmountTot.Text = "0.00";
                pnlBillDetails.Enabled = true;
                pnlVou.Enabled = true;
                mpMSVC.Rows.Clear();
                psLableWithBorder1.Text = "";
                mpMSVC.Enabled = true;
                lblFooterMessage.Text = "";
                FixVoucherTypeBycbTransactionType();
                cbTransactionType.Focus();
                DataTable dtp = new DataTable(); 
                _Purchase.VoucherSubType = "1";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion
    }
}