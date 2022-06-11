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
using System.IO;
using EcoMart.InterfaceLayer.Classes;
using System.Globalization;
using EcoMart.InterfaceLayer.Controls;
using System.Diagnostics;
using PaperlessPharmaRetail.Common.Classes;
using EcoMart.DataLayer;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclPurchase : BaseControl
    {
        public enum PurchaseMode
        {
            Add = 0,
            Edit = 1,
        }

        #region Declaration
        private Purchase _Purchase;
        public Product _UclProduct;
        private DataTable _BindingSource;
        private DataTable _PaymentDetailsBindingSource;
        private string IfEditPreviousRow = "N";
        private string _LastStockID;
        //private string purchaseType;
        private string deletedproductname = "";
        private BaseControl ViewControl;
        private Form frmView;
        private int _preID = 0;
        private string _tempdelpath = "";
        private string ifnewscheme = "N";
        private Scheme _Scheme;


        public PurchaseMode _purchaseMode;
        public PurchaseMode purchaseMode
        {
            get { return _purchaseMode; }
            set { _purchaseMode = value; }
        }
        public string tempdelpath //Amar
        {
            get { return _tempdelpath; }
            set { _tempdelpath = value; }
        }
        Company cobj = new Company();
        Product pobj = null;
        //   private bool pnltempoff = false;
        //  private bool IFpnlTempwasvisible = false;

        private ImportBill _ImportBill;
        Form frmOpen;
        FormImportSaleBill _formImportAlliedSaleBill;
        public static double MinTradeRate = 0;
        public static double MinMRP = 0;
        Timer timer;
        Timer DateTimer;
        #endregion

        #region contructor

        public UclPurchase()
        {
            InitializeComponent();
            _Purchase = new Purchase();
            SearchControl = new UclPurchaseSearch();
            _LastStockID = string.Empty;
            _Scheme = new Scheme();
            _ImportBill = null;
            pobj = new Product();
            timer = new Timer();
            DateTimer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            DateTimer.Tick += new EventHandler(DateTimer_Tick);
        }

        # endregion

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
            if (_Mode == OperationMode.Add)
            {
                if (string.IsNullOrEmpty(Convert.ToString(mcbCreditor.SelectedID)) == false && mpMSVC.Rows.Count > 1)
                {
                    mpMSVC.SetFocus(1);
                }
                else
                    mcbCreditor.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(Convert.ToString(mcbCreditor.SelectedID)) == false && mpMSVC.Rows.Count > 1)
                {
                    mpMSVC.SetFocus(mpMSVC.Rows.Count - 1, 1);
                }
                else
                    txtVouchernumber.Focus();
            }
        }
        public override bool ClearData()
        {
            _Purchase.Initialise();
            ClearControls();
            return true;
            // mpMSVC.ClearSelection();
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
            headerLabel1.Text = "PURCHASE -> NEW";
            InitializeScreen();
            mcbBank.SelectedID = null;
            mcbCreditor.SelectedID = null;
            InitializeMainSubViewControl("");
            //  IFpnlTempwasvisible = false;
            mpMSVC.Enabled = true;
            FillShelfCombo();
            FillBankCombo();
            FillAllData();
            mpMSVC.BringToFront();
            FillCreditorCombo();
            //  FillTempPurchase();
            btnPaymentHistory.Visible = false;
            btnSummary.Enabled = false;
            tsBtnSave.Enabled = false;
            pnlProductDetail.Enabled = true;
            dgvBatchGrid.Visible = false;
            pnlGST.Visible = false;
            pnlIGST.Visible = false;
            pnlSummary.Visible = false;
            if (General.CurrentSetting.MsetPurchaseRounding == "Y")
                cbRound.Checked = true;
            else
                cbRound.Checked = false;
            mcbCreditor.Enabled = true;
            cbTransactionType.Enabled = false;
            txtNarration.Enabled = true;
            txtBillNumber.Enabled = true;
            txtVouchernumber.Enabled = false;
            //if (dgTempPurchase.Rows.Count > 0)
            //    pnlTempPurchase.Visible = true;
            if (General.CurrentSetting.MsetPurchaseChangeSaleRate == "Y")
                txtSaleRate.Enabled = true;
            else
                txtSaleRate.Enabled = false;

            if (_ImportBill != null)
            {
                FillFormWithImportBillData();
            }
            FixVoucherTypeBycbTransactionType();
            mcbCreditor.Focus();
            //   cbTransactionType.Focus();

            return retValue;
        }

        private void FillAllData()
        {
            FillCompanyCombo();
            FillGenericCategoryCombo();
            FillProdCategoryCombo();
            FillShelfComboList();
            FillScheduleDrugCombo();
            FillPack();
            FillPackType();
        }

        public void FillFormWithImportBillData()
        {
            DateTime mydate;

            txtBillNumber.Text = _ImportBill.BillNumber;
            if (_ImportBill.BillDate != "")
            {
                int datelen = _ImportBill.BillDate.ToString().Length;
                if (datelen == 10)
                {
                    mydate = new DateTime(Convert.ToInt32(_ImportBill.BillDate.Substring(6, 4)), Convert.ToInt32(_ImportBill.BillDate.Substring(3, 2)), Convert.ToInt32(_ImportBill.BillDate.Substring(0, 2)));
                    datePickerBillDate.Value = mydate;
                }
                else
                {
                    mydate = new DateTime(Convert.ToInt32(_ImportBill.BillDate.Substring(4, 4)), Convert.ToInt32(_ImportBill.BillDate.Substring(2, 2)), Convert.ToInt32(_ImportBill.BillDate.Substring(0, 2)));
                    datePickerBillDate.Value = mydate;
                }
            }

            // datePickerBillDate.Value= Convert.ToDateTime();
            txtBillNumber.Text = _ImportBill.BillNumber;
            txtVouType.Text = _ImportBill.VoucherType;
            cbTransactionType.Items.Clear();
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
            //cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
            //if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
            //    cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            if (_ImportBill.VoucherType == FixAccounts.VoucherTypeForCreditStatementPurchase)
            {
                cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
                cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCreditStatement);
            }
            else if (_ImportBill.VoucherType == FixAccounts.VoucherTypeForCashPurchase)
            {
                cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
            }
            else
            {
                cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
            }
            cbTransactionType.SelectedItem = FixAccounts.TransactionTypeForCredit;
            lblPurchaseBillFormat.Text = _ImportBill.PurchaseBillFormat;
            txtVouType.Text = _ImportBill.VoucherType;
            mcbCreditor.SelectedID = _ImportBill.DistributorID;
            txtCashDiscountPerS.Text = _ImportBill.CashDiscountPercent;
            txtRoundUPS.Text = _ImportBill.RoundOFF;
            txtGridAmountTot.Text = _ImportBill.TotalAmount;
            txtTotalS.Text = _ImportBill.TotalAmount;
            txtBillAmountS.Text = _ImportBill.BillNetAmount;
            txtBillAmount.Text = _ImportBill.BillNetAmount;
            txtDBAmountS.Text = _ImportBill.DebitAmount.ToString("#0.00");
            if (mpMSVC.Rows.Count > 0)
                mpMSVC.Rows.Clear();

            _Purchase.SavePartyAIOCDACodeInMasterAccount(_ImportBill.DistributorID, _ImportBill.AIOCDACode, _ImportBill.DistributorCode);
            //foreach (DataGridViewRow dr in _ImportBill.SaleBillData.Rows)
            //{
            //    if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_DistributorsProductID"].Value != null)
            //        _Purchase.SaveProductCodeInMasterProduct(dr.Cells["Col_ProductID"].Value.ToString(), dr.Cells["Col_DistributorsProductID"].Value.ToString());
            //}
            //tsBtnSave.Enabled = false;
            //btnSummary.Enabled = true;

            try
            {
                foreach (DataGridViewRow dr in _ImportBill.SaleBillData.Rows)
                {
                    _Purchase.ProductID = Convert.ToInt32(dr.Cells["Col_ProductID"].Value.ToString());
                    if (_Purchase.ProductID <= 0)
                    {
                        DataRow proddr;
                        proddr = _Purchase.GetDetailsForProduct(_Purchase.ProductID);
                        int currow = mpMSVC.Rows.Add();
                        mpMSVC.Rows[currow].Cells["Col_ProductID"].Value = _Purchase.ProductID;
                        mpMSVC.Rows[currow].Cells["Col_ProductName"].Value = proddr["ProdName"].ToString();
                        mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].ReadOnly = true;
                        mpMSVC.Rows[currow].Cells["Col_UnitOfMeasure"].Value = proddr["ProdLoosePack"].ToString();
                        mpMSVC.Rows[currow].Cells["Col_Pack"].Value = proddr["ProdPack"].ToString();
                        mpMSVC.Rows[currow].Cells["Col_Company"].Value = proddr["ProdCompShortName"].ToString();
                        mpMSVC.Rows[currow].Cells["Col_Box1"].Value = proddr["ProdBoxQuantity"].ToString();
                        mpMSVC.Rows[currow].Cells["Col_ProdClosingStock"].Value = proddr["ProdClosingStock"].ToString();
                        mpMSVC.Rows[currow].Cells["Col_ProdVATPer"].Value = proddr["ProdVATPercent"].ToString();
                        mpMSVC.Rows[currow].Cells["Col_DistributorProductID"].Value = dr.Cells["Col_DistributorsProductID"].Value.ToString();
                        mpMSVC.Rows[currow].Cells["Col_Quantity"].Value = dr.Cells["Col_Quantity"].Value.ToString();
                        mpMSVC.Rows[currow].Cells["Col_BatchNumber"].Value = dr.Cells["Col_BatchNumber"].Value.ToString();
                        if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null)
                            mpMSVC.Rows[currow].Cells["Col_ItemSCMDiscountAmount"].Value = dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString();
                        //string mexp = dr.Cells["Col_ExpiryDate"].Value.ToString();
                        //string mexpl = "";
                        //string mexpr = "";
                        //if (mexp.Length == 10)
                        //{
                        //    mexpl = mexp.Substring(3, 2);
                        //    mexpr = mexp.Substring(8, 2);
                        //    mexp = mexpl + "/" + mexpr;
                        //}
                        //else if (mexp.Length == 8)
                        //{
                        //    mexpl = mexp.Substring(2, 2);
                        //    mexpr = mexp.Substring(6, 2);
                        //    mexp = mexpl + "/" + mexpr;
                        //}

                        mpMSVC.Rows[currow].Cells["Col_Expiry"].Value = dr.Cells["Col_Expiry"].Value.ToString();
                        mpMSVC.Rows[currow].Cells["Col_ExpiryDate"].Value = dr.Cells["Col_ExpiryDate"].Value.ToString();
                        mpMSVC.Rows[currow].Cells["Col_TradeRate"].Value = dr.Cells["Col_TradeRate"].Value.ToString();
                        mpMSVC.Rows[currow].Cells["Col_MRP"].Value = dr.Cells["Col_MRP"].Value.ToString();
                        mpMSVC.Rows[currow].Cells["Col_VAT"].Value = dr.Cells["Col_VAT"].Value.ToString();
                        mpMSVC.Rows[currow].Cells["Col_Scheme"].Value = dr.Cells["Col_Scheme"].Value.ToString();
                        mpMSVC.Rows[currow].Cells["Col_Replacement"].Value = "0";
                        if (dr.Cells["Col_ItemDiscountPer"].Value != null)
                            mpMSVC.Rows[currow].Cells["Col_ItemDiscountPer"].Value = dr.Cells["Col_ItemDiscountPer"].Value.ToString();
                        else
                            mpMSVC.Rows[currow].Cells["Col_ItemDiscountPer"].Value = "0.00";
                        double mamount = 0;
                        double mqty = Convert.ToDouble(mpMSVC.Rows[currow].Cells["Col_Quantity"].Value.ToString());
                        double mtraderate = Convert.ToDouble(mpMSVC.Rows[currow].Cells["Col_TradeRate"].Value.ToString());
                        mamount = mqty * mtraderate;
                        mpMSVC.Rows[currow].Cells["Col_Amount"].Value = mamount.ToString("#0.00");
                        if (dr.Cells["Col_ItemDiscountAmount"].Value != null)
                            mpMSVC.Rows[currow].Cells["Col_ItemDiscountAmount"].Value = dr.Cells["Col_ItemDiscountAmount"].Value.ToString();
                        else
                            mpMSVC.Rows[currow].Cells["Col_ItemDiscountAmount"].Value = "0.00";
                        // mpMSVC.Rows[currow].Cells["Col_SplDiscountPer"].Value = "0.00";
                        // mpMSVC.Rows[currow].Cells["Col_SplDiscountAmount"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_VATAmountPurchase"].Value = "0.00";
                        //mpMSVC.Rows[currow].Cells["Col_CSTAmount"].Value = "0.00";
                        //mpMSVC.Rows[currow].Cells["Col_CSTPer"].Value = "0.00";
                        //mpMSVC.Rows[currow].Cells["Col_ItemSCMDiscountAmount"].Value = "0.00";
                        //mpMSVC.Rows[currow].Cells["Col_ItemSCMDiscountAmountPerUnit"].Value = "0.00";
                        // mpMSVC.Rows[currow].Cells["Col_CSTPer"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_IfOctroi"].Value = proddr["ProdIfOctroi"].ToString();
                        if (dr.Cells["Col_CreditNoteAmount"].Value != null)
                            mpMSVC.Rows[currow].Cells["Col_CreditNoteAmount"].Value = dr.Cells["Col_CreditNoteAmount"].Value.ToString();
                        else
                            mpMSVC.Rows[currow].Cells["Col_CreditNoteAmount"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_CashDiscountAmount"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_PurchaseRate"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_SaleRate"].Value = dr.Cells["Col_MRP"].Value.ToString();
                        mpMSVC.Rows[currow].Cells["Col_CompID"].Value = proddr["CompID"].ToString();
                        mpMSVC.Rows[currow].Cells["Col_VATAmountSale"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_ShelfCode"].Value = proddr["ShelfCode"].ToString();
                        mpMSVC.Rows[currow].Cells["Col_ShelfID"].Value = proddr["ShelfID"].ToString();
                        mpMSVC.Rows[currow].Cells["Col_Margin"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_Margin2"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_ScanCode"].Value = "";
                        mpMSVC.Rows[currow].Cells["Col_StockID"].Value = "";
                        mpMSVC.Rows[currow].Cells["Col_DistributorSaleRate"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_ProductName"].ReadOnly = true;  //  mpMSVC.Rows[currow].Cells["Col_DistributorSale"].Value = "0.00";
                    }
                }
                SaveProductintblbillimportlink();
                if (lblPurchaseBillFormat.Text.ToString() != string.Empty)
                    _Purchase.PurchaseBillFormat = lblPurchaseBillFormat.Text.ToString();
                else
                    _Purchase.PurchaseBillFormat = string.Empty;
                if (lblPurchaseBillFormat.Text != string.Empty)
                    _Purchase.SavePurchaseBillformat();
                CalculatePurRateSaleRateAmountforFullGrid();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void SaveProductintblbillimportlink()
        {
            string guid = string.Empty;
            string distributorProductID = string.Empty;
            string retailerProductID = string.Empty;
            foreach (DataGridViewRow dr in mpMSVC.Rows)
            {
                if (dr.Cells["Col_ProductID"].Value != null)
                    retailerProductID = dr.Cells["Col_ProductID"].Value.ToString();
                if (dr.Cells["Col_DistributorProductID"].Value.ToString() != string.Empty)
                    distributorProductID = dr.Cells["Col_DistributorProductID"].Value.ToString().Trim();
                if (distributorProductID != null && distributorProductID != string.Empty)
                {
                    guid = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _Purchase.SaveProductsintblbillimportlink(guid, _ImportBill.DistributorID, distributorProductID, retailerProductID);
                }
            }
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            InitializeScreen();

            headerLabel1.Text = "PURCHASE -> EDIT";
            InitializeMainSubViewControl("");
            FillShelfCombo();
            FillCreditorCombo();
            FillBankCombo();
            FillAllData();
            //  FillTransactionType();
            FixVoucherTypeBycbTransactionType();
            EnableDisable();
            SetFocus();
            return retValue;
        }

        private void EnableDisable()
        {
            tsBtnSave.Enabled = false;
            btnPaymentHistory.Visible = true;
            mcbCreditor.Enabled = false;
            pnlProductDetail.Enabled = true;
            txtNarration.Enabled = false;
            txtBillNumber.Enabled = false;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            cbTransactionType.Enabled = true;

            if (General.CurrentSetting.MsetPurchaseChangeSaleRate == "Y")
                txtSaleRate.Enabled = true;
            else
                txtSaleRate.Enabled = false;
        }

        private void FixVoucherTypeBycbTransactionType()
        {
            //_Purchase.VoucherType = "";
            //if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
            _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
            //else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
            //    _Purchase.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
            //else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
            //    _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
            txtVouType.Text = FixAccounts.VoucherTypeForCreditPurchase;
            _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            System.IO.File.Delete(General.GetPurchaseTempFile());
            pnlEditProduct.Visible = false;
            pnlProductDetail.Visible = false;
            pnlSummary.Visible = false;
            pnlGST.Visible = false;
            pnlIGST.Visible = false;
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {
                mpMSVC.Rows.Add();
            }
            cbTransactionType.Enabled = true;
            return retValue;
        }

        public override bool Exit()
        {
            bool retValue = false;
            if (mpMSVC.Rows.Count > 0)
            {
                retValue = base.Exit();
                System.IO.File.Delete(General.GetPurchaseTempFile());
                _ImportBill = null;
            }
            else if (_Mode == OperationMode.Add)
            {
                PSDialogResult result;
                result = PSMessageBox.Show("Save Or Remove All Invoices..", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
            }
            else
                retValue = base.Exit();
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            InitializeScreen();
            headerLabel1.Text = "PURCHASE -> DELETE";
            ClearData();
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            txtVouchernumber.Focus();
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_Purchase.AmountClearS != 0)
                MessageBox.Show("Payment Done", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

                if (MessageBox.Show("Are you sure you want to delete Purchase information?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BindTempGrid();
                    bool canbedeleted = CheckStockForDelete();
                    if (canbedeleted)
                    {
                        LockTable.LockTablesForPurchase();
                        retValue = _Purchase.DeleteDetails();
                        retValue = _Purchase.DeletePreviousRecords();
                        retValue = _Purchase.DeleteAccountDetails();
                        ReducePreviousStock();
                        clearPreviousdebitcreditnotes();
                        LockTable.UnLockTables();
                        //  UpdateClosingStockinCache();
                        retValue = true;
                        MessageBox.Show("Successfully Deleted", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _Purchase.AddDeletedDetails();
                        AddPreviousRowsInDeleteDetail();
                    }
                    else
                        MessageBox.Show("Can not Update " + deletedproductname, General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                InitializeScreen();
                headerLabel1.Text = "PURCHASE -> VIEW";
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                txtVouchernumber.Focus();
                mcbCreditor.Enabled = false;
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
        private void GetLastRecord()
        {
            try
            {
                _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                _Purchase.VoucherSubType = "1";
                _Purchase.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                _Purchase.GetLastRecordForPurchase(_Purchase.VoucherType, _Purchase.VoucherSubType, _Purchase.VoucherSeries);
                FillSearchData(_Purchase.Id, "");
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
            _Purchase.VoucherType = txtVouType.Text.ToString();
            _Purchase.VoucherSubType = "1";
            dr = _Purchase.GetFirstRecord();
            if (dr != null && dr["PurchaseID"] != DBNull.Value)
            {
                _Purchase.Id = dr["PurchaseID"].ToString();
                FillSearchData(_Purchase.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            //   DataRow dr = null;
            _Purchase.VoucherType = txtVouType.Text.ToString();
            _Purchase.VoucherSubType = "1";
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _Purchase.VoucherType = txtVouType.Text.ToString();
            _Purchase.VoucherSubType = "1";
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _Purchase.VoucherSeries = txtVoucherSeries.Text.ToString();
            else
                _Purchase.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _Purchase.VoucherNumber = i;
                dr = _Purchase.ReadDetailsByVouNumber(i, _Purchase.VoucherType, _Purchase.VoucherSeries, _Purchase.VoucherSubType);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["PurchaseID"] != DBNull.Value)
            {
                _Purchase.Id = dr["PurchaseID"].ToString();
                FillSearchData(_Purchase.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _Purchase.GetLastVoucherNumber(_Purchase.VoucherType, _Purchase.VoucherSubType, _Purchase.VoucherSeries);
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _Purchase.VoucherType = txtVouType.Text.ToString();
            _Purchase.VoucherSubType = "1";
            if (txtVoucherSeries.Text == null || txtVoucherSeries.Text == string.Empty)
                _Purchase.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            else
                _Purchase.VoucherSeries = txtVoucherSeries.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _Purchase.VoucherNumber = i;
                dr = _Purchase.ReadDetailsByVouNumber(_Purchase.VoucherNumber, _Purchase.VoucherType, _Purchase.VoucherSeries, _Purchase.VoucherSubType);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["PurchaseID"] != DBNull.Value)
            {
                _Purchase.Id = dr["PurchaseID"].ToString();
                FillSearchData(_Purchase.Id, "");
            }
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
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {

                        LockTable.LockTablesForPurchase();// LockTable.LocktblVoucherNo();
                        _Purchase.VoucherNumber = _Purchase.GetAndUpdatePurchaseNumber(_Purchase.VoucherType);
                        // LockTable.UnLockTables();
                    }

                    _Purchase.Validate();



                    if (_Purchase.IsValid)
                    {
                        try
                        {
                            LockTable.LockTablesForPurchase();
                            if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                            {
                                General.BeginTransaction();

                                //    _Purchase.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                //_Purchase.VoucherNumber = _Purchase.GetAndUpdatePurchaseNumber(_Purchase.VoucherType);
                                _Purchase.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                                txtVouchernumber.Text = _Purchase.VoucherNumber.ToString();
                                _Purchase.CreatedBy = General.CurrentUser.Id;
                                _Purchase.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _Purchase.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                                //  retValue = _Purchase.AddDetails();
                                _Purchase.IntID = 0;
                                _Purchase.IntID = _Purchase.AddDetails();
                                if (_Purchase.IntID > 0)
                                    retValue = true;
                                else
                                    retValue = false;
                                _SavedID = _Purchase.Id;
                                if (retValue)
                                    retValue = SaveParticularsProductwise();

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
                                    // ss 18-10
                                    ////if (_Purchase.IfCashPaid == "Y")
                                    ////{
                                    ////    _Purchase.CBId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    ////    CashPayment _csp = new CashPayment();
                                    ////    _Purchase.CBVouNo = _csp.GetAndUpdateCSPNumber(General.ShopDetail.ShopVoucherSeries);
                                    ////    _Purchase.CBVouType = FixAccounts.VoucherTypeForCashPayment;
                                    ////    retValue = _Purchase.AddCashEntry();
                                    ////}
                                    ////else if (_Purchase.ChequeNumber != "" && _Purchase.BankID != "")
                                    ////{
                                    ////    _Purchase.CBId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    ////    BankPayment _bkp = new BankPayment();
                                    ////    _Purchase.CBVouNo = _bkp.GetAndUpdateBKPNumber(General.ShopDetail.ShopVoucherSeries);
                                    ////    _Purchase.CBVouType = FixAccounts.VoucherTypeForBankPayment;
                                    ////    retValue = _Purchase.AddBankEntry();
                                    ////}
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
                                    //if (General.CurrentSetting.MsetScanBarCode == "Y")
                                    //{
                                    //    DialogResult dResult;
                                    //    dResult = MessageBox.Show("Print Labels", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                    //    if (dResult == DialogResult.Yes)
                                    //    {
                                    //        PrintBarCodeLables();
                                    //    }
                                    //}

                                    if (System.IO.File.Exists(tempdelpath))  //Amar For delete the already purchased online bills
                                    {

                                        try
                                        {
                                            System.IO.File.Delete(tempdelpath);
                                        }
                                        catch (Exception Ex)
                                        {
                                            Log.WriteException(Ex);
                                        }
                                    }

                                }
                                else
                                {
                                    PSDialogResult result = PSMessageBox.Show("Could not Save Voucher...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                    retValue = false;
                                }
                            }
                            //else if (_Mode == OperationMode.Fifth)
                            //{
                            //    DataTable stocktbl = new DataTable();
                            //    _Purchase.VoucherNumber = int.Parse(txtVouchernumber.Text);
                            //    if (cbTransactionType.Text == cbNewTransactionType.Text)
                            //        _Purchase.IfTypeChange = "N";
                            //    else
                            //        _Purchase.IfTypeChange = "Y";
                            //    if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCash)
                            //        _Purchase.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
                            //    else if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                            //        _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                            //    else
                            //        _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;

                            //    General.BeginTransaction();

                            //    if (_Purchase.IfTypeChange == "Y")
                            //    {
                            //        if (_Purchase.OldVoucherType != _Purchase.VoucherType)
                            //        {
                            //            _Purchase.VoucherNumber = _Purchase.GetAndUpdatePurchaseNumber(_Purchase.VoucherType);
                            //            txtVouchernumber.Text = _Purchase.VoucherNumber.ToString();
                            //            txtVouType.Text = _Purchase.VoucherType;
                            //            retValue = _Purchase.UpdateDetailsForTypeChange();
                            //            if (retValue)
                            //            {
                            //                retValue = _Purchase.DeleteAccountDetails();
                            //            }
                            //            if (retValue)
                            //            {
                            //                retValue = _Purchase.AddAccountDetails();
                            //            }
                            //            if (retValue)
                            //                _Purchase.UpdateCreditDebitNoteforTypeChange(_Purchase.CreditDebitNoteID, _Purchase.Amount, _Purchase.VoucherType, _Purchase.VoucherNumber, _Purchase.VoucherDate, _Purchase.PurchaseBillNumber, _Purchase.Id);


                            //            if (retValue)
                            //                General.CommitTransaction();
                            //            else
                            //                General.RollbackTransaction();
                            //            LockTable.UnLockTables();
                            //            if (retValue)
                            //            {
                            //                //  UpdateClosingStockinCache();
                            //                string msgLine2 = _Purchase.VoucherType + "  " + _Purchase.VoucherNumber.ToString("#0");
                            //                PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                            //                //     MessageBox.Show("Information has been saved successfully.'", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //                retValue = true;
                            //            }
                            //            else
                            //            {
                            //                PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                            //                retValue = false;
                            //            }
                            //        }
                            //        ClearData();
                            //    }
                            //}
                            else if (_Mode == OperationMode.Edit)
                            {
                                DataTable stocktbl = new DataTable();
                                _Purchase.VoucherNumber = int.Parse(txtVouchernumber.Text);
                                General.BeginTransaction();
                                _Purchase.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                                _Purchase.ModifiedBy = General.CurrentUser.Id;
                                _Purchase.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _Purchase.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");

                                retValue = CheckStockForDeletedRows();
                                if (retValue)
                                {
                                    retValue = DeletePreviousRows();
                                    if (retValue)
                                        retValue = SaveParticularsProductwise();
                                    if (retValue)
                                        retValue = ReducePreviousStock();

                                    if (retValue)
                                        retValue = _Purchase.UpdateDetails();
                                    _SavedID = _Purchase.Id;

                                    if (retValue)
                                    {
                                        //retValue = DeletePreviousRows();
                                        //if (retValue)
                                        //    retValue = SaveParticularsProductwise();

                                        clearPreviousdebitcreditnotes();
                                        if (retValue && (_Purchase.AmountCreditNoteS > 0 || _Purchase.AmountDebitNoteS > 0))
                                            retValue = SaveAndUpdateDebitCreditNote();

                                        if (retValue)
                                        {

                                            retValue = _Purchase.DeleteAccountDetails();
                                            _Purchase.CreatedBy = _Purchase.ModifiedBy;
                                            _Purchase.CreatedDate = _Purchase.ModifiedDate;
                                            _Purchase.CreatedTime = _Purchase.ModifiedTime;
                                            retValue = _Purchase.AddAccountDetails();

                                        }
                                        if (retValue)
                                            General.CommitTransaction();
                                        else
                                            General.RollbackTransaction();
                                        LockTable.UnLockTables();
                                        if (retValue)
                                        {
                                            //  UpdateClosingStockinCache();
                                            try
                                            {
                                                //SS
                                                //_/*Purchase.ChangedID = _Purchase.GetChangedIntID(); /* Guid.NewGuid().ToString().ToUpper().Replace("-", "");*/*                                                
                                                _Purchase.ChangedIntID = _Purchase.AddChangedDetails();
                                                AddPreviousRowsInChangedDetail();
                                                MessageBox.Show("Information has been Updated successfully.'", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                datePickerBillDate.Value = DateTime.Now.Date;
                                            }
                                            catch (Exception Ex)
                                            {
                                                Log.WriteException(Ex);
                                            }
                                            retValue = true;
                                        }
                                        else
                                        {
                                            PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                            retValue = false;
                                        }
                                    }
                                    else
                                    {
                                        string mm = _Purchase.Name + " " + _Purchase.ProdLoosePack.ToString() + _Purchase.Pack + " - " + _Purchase.Batchno + " - " + _Purchase.MRP.ToString("0.00");
                                        MessageBox.Show(mm + " Can not Update Stock < 0", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        General.RollbackTransaction();
                                        LockTable.UnLockTables();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Can not Update " + deletedproductname, General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    General.RollbackTransaction();
                                    deletedproductname = "";
                                    LockTable.UnLockTables();
                                }

                            }
                            //else
                            //{
                            //    StringBuilder _errorMessage = new System.Text.StringBuilder();
                            //    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                            //    foreach (string _message in _Purchase.ValidationMessages)
                            //    {
                            //        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                            //    }
                            //    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //}
                        }

                        catch (Exception ex)
                        {
                            Log.WriteError(ex.ToString());
                            StringBuilder _errorMessage = new System.Text.StringBuilder();
                            _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                            foreach (string _message in _Purchase.ValidationMessages)
                            {
                                _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                            }
                            MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            LockTable.UnLockTables();
            //CacheObject.Clear("cacheCounterSale");
            return retValue;
        }

        private void PrintBarCodeLables()
        {
            OpenBarCodeForm("Purchase", _Purchase.VoucherType, _Purchase.VoucherNumber.ToString(), new DataTable());
        }

        private void OpenBarCodeForm(string PrintType, string VoucherType, string VoucherNumber, DataTable data)
        {
            UclToolPrintBarCode UserControlToShow = new UclToolPrintBarCode();
            frmOpen = new Form();
            frmOpen.FormBorderStyle = FormBorderStyle.FixedSingle;
            frmOpen.ControlBox = false;
            frmOpen.Height = UserControlToShow.Height;
            frmOpen.Width = UserControlToShow.Width;
            frmOpen.StartPosition = FormStartPosition.CenterScreen;
            UserControlToShow.Mode = OperationMode.OpenAsChild;
            UserControlToShow.Visible = true;
            UserControlToShow.Add();
            UserControlToShow.SetData(PrintType, VoucherType, VoucherNumber, data);
            UserControlToShow.ExitClicked += new EventHandler(UserControlToShow_ExitClicked);
            frmOpen.Controls.Add(UserControlToShow);
            frmOpen.KeyPreview = true;
            if (frmOpen.Controls.Count > 0)
                frmOpen.Controls[0].Focus();
            frmOpen.ShowDialog();
        }

        private void UserControlToShow_ExitClicked(object sender, EventArgs e)
        {
            if (frmOpen != null)
            {
                frmOpen.Close();
            }
        }





        public void FixVoucherType()
        {
            _Purchase.EntryDate = Convert.ToString(DateTime.Now);

            FixVoucherTypeBycbTransactionType();

            if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCashPurchase)
                _Purchase.PurchaseAccount = FixAccounts.AccountCashPurchase.ToString();
            else if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCreditStatementPurchase)
                _Purchase.PurchaseAccount = FixAccounts.AccountCreditPurchase.ToString();
            else if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCreditPurchase)
                _Purchase.PurchaseAccount = FixAccounts.AccountCashCreditPurchase.ToString();
            txtVouType.Text = _Purchase.VoucherType;
            if (mcbCreditor.SelectedID != null)
                _Purchase.AccountID = this.mcbCreditor.SelectedID;
            _Purchase.PurchaseBillNumber = txtBillNumber.Text;
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
            _Purchase.AmountDebitNoteS = Convert.ToDouble(txtDBAmountS.Text.ToString());
            //if (txtOCTPerS.Text != "")
            //    _Purchase.OctroiPercentageS = Convert.ToDouble(txtOCTPerS.Text.ToString());

            _Purchase.Narration = txtNarration.Text.ToString();
            _Purchase.RoundUpAmountS = Convert.ToDouble(txtRoundUPS.Text.ToString());

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
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    ClearData();
                    _Purchase.Id = ID;
                    if (Vmode == "C")
                        _Purchase.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _Purchase.ReadDetailsByIDForDeleted();
                    else
                        _Purchase.ReadDetailsByID();

                    BindTempGrid();

                    BindPaymentDetails();
                    FillGSTpnl();
                    InitializeMainSubViewControl(Vmode);
                    if (_Mode == OperationMode.ReportView)
                    {
                        string vout = _Purchase.VoucherType;
                        FillTransactionType();
                        _Purchase.VoucherType = vout;
                    }

                    //if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
                    //{
                    //    int currentrow =  mpMSVC.Rows.Add();
                    //    mpMSVC.SetFocus(currentrow, 1);
                    //}
                    _Purchase.OldVoucherType = _Purchase.VoucherType;
                    _Purchase.OldVoucherNumber = _Purchase.VoucherNumber;
                    if (_Purchase.StatementNumber.ToString() != "" && _Purchase.StatementNumber > 0)
                        lblFooterMessage.Text = "Statement Number : " + _Purchase.StatementNumber.ToString();
                    else
                        lblFooterMessage.Text = "";
                    txtVoucherSeries.Text = _Purchase.VoucherSeries;
                    if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCashPurchase)
                    {

                        cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                    }
                    else if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCreditStatementPurchase)
                    {

                        cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCreditStatement);
                    }
                    else
                    {

                        cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                    }

                    txtVouType.Text = _Purchase.VoucherType.ToString();
                    txtVouchernumber.Text = _Purchase.VoucherNumber.ToString();
                    txtBillNumber.Text = _Purchase.PurchaseBillNumber;
                    txtNarration.Text = _Purchase.Narration;
                    txtSplDiscPerS.Text = _Purchase.SpecialDiscountPercentS.ToString("#0.00");
                    txtCashDiscountPerS.Text = _Purchase.CashDiscountPercentageS.ToString("#0.00");
                    txtCashDiscountAmountS.Text = _Purchase.AmountCashDiscountS.ToString("#0.00");
                    txtPreCashDiscountAmountS.Text = _Purchase.AmountCashDiscountS.ToString("#0.00");
                    txtSplDiscountS.Text = _Purchase.AmountSpecialDiscountS.ToString("#0.00");
                    txtItemDiscountS.Text = _Purchase.AmountItemDiscountS.ToString("#0.00");
                    txtSchemeDiscountS.Text = _Purchase.AmountSchemeDiscountS.ToString("#0.00");
                    txtAddOnS.Text = _Purchase.AmountAddOnFreightS.ToString("#0.00");
                    txtLessS.Text = _Purchase.AmountLessS.ToString("#0.00");
                    txtCRAmountS.Text = _Purchase.AmountCreditNoteS.ToString("#0.00");
                    txtDBAmountS.Text = _Purchase.AmountDebitNoteS.ToString("#0.00");
                    txtViewVat5per.Text = _Purchase.AmountVAT5PercentS.ToString("#0.00");
                    txtViewVat12point5per.Text = _Purchase.AmountVAT12point5PercentS.ToString("#0.00");
                    txtPurchaseAmountVATZeroS.Text = _Purchase.PurchaseAmountZeroVATS.ToString("#0.00");
                    txtPurchaseAmountVAT5S.Text = _Purchase.PurchaseAmount5PercentVATS.ToString("#0.00");
                    txtPurchaseAmountVAT12point5S.Text = _Purchase.PurchaseAmount12point5PercentVATS.ToString("#0.00");
                    if (DateTime.TryParse(_Purchase.VoucherDate, out DateTime mydate))
                        datePickerBillDate.Value = mydate;
                    txtRoundUPS.Text = _Purchase.RoundUpAmountS.ToString("#0.00");
                    txtGridAmountTot.Text = _Purchase.AmountS.ToString("#0.00");
                    txtBillAmountS.Text = _Purchase.AmountS.ToString("#0.00");
                    txtBillAmount.ReadOnly = false;
                    txtBillAmount.Enabled = true;
                    txtBillAmount.Text = _Purchase.AmountNetS.ToString("#0.00");
                    txtNetAmountS.Text = _Purchase.AmountNetS.ToString("#0.00");
                    txtBillAmount.ReadOnly = true;
                    txtBillAmount.Enabled = false;

                    FillShelfCombo();
                    FillBankCombo();
                    string ssa = _Purchase.AccountID;
                    FillCreditorCombo();
                    _Purchase.AccountID = ssa;
                    btnSummary.Enabled = true;
                    dgvBatchGrid.Visible = false;
                    pnlSummary.Visible = false;
                    pnlGST.Visible = false;
                    pnlIGST.Visible = false;


                    mcbCreditor.SelectedID = _Purchase.AccountID;
                    if (_Mode == OperationMode.ReportView)
                    {
                        tsBtnFifth.Visible = false;
                    }
                    //if (_Mode == OperationMode.Fifth && _Purchase.StatementNumber == 0)
                    //{
                    //    btnTypeChange.Visible = true;
                    //    cbNewTransactionType.Visible = true;
                    //    cbNewTransactionType.Enabled = false;
                    //    btnTypeChange.Enabled = true;
                    //    btnTypeChange.Focus();
                    //}
                    if (_Purchase.StatementNumber > 0 || _Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                    {
                        pnlPaymentDetails.Enabled = false;
                        mpMSVC.IsAllowDelete = false;
                        mcbCreditor.Enabled = false;
                    }
                    else
                    {
                        mpMSVC.IsAllowDelete = true;
                        pnlPaymentDetails.Enabled = true;
                        pnlBillDetails.Enabled = true;
                        mcbCreditor.Enabled = true;
                        txtBillNumber.Enabled = true;
                        //if (_Mode !=OperationMode.Add)
                        //    mcbCreditor.Focus();

                    }

                    txtVouchernumber.Enabled = false;
                    cbTransactionType.Enabled = false;
                    txtVouchernumber.Enabled = true;
                    if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
                    {
                        int currentrow = mpMSVC.Rows.Add();
                        //    mpMSVC.SetFocus(currentrow, 1);
                    }
                }
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        # endregion Idetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
        {

            try
            {
                General.CurrentSetting.FillSettings();

                if (closedControl is UclProduct)
                {
                    DataTable dtable = new DataTable();
                    Product prod = new Product();
                    dtable = prod.GetOverviewData();
                    mpMSVC.DataSource = dtable;
                    mpMSVC.BindGridSub();
                }
                else if (closedControl is UclAccount)
                {
                    string creditorID = mcbCreditor.SelectedID;
                    FillCreditorCombo();
                    mcbCreditor.SelectedID = creditorID;
                    mcbCreditor.Focus();
                }
                else if (closedControl is UclCreditNoteAmount || closedControl is UclCreditNoteStock || closedControl is UclDebitNoteAmount || closedControl is UclDebitNotestock)
                    FillCreditDebitNote();
                string oldtrans = cbTransactionType.Text;
                Int32 oldtransindex = cbTransactionType.SelectedIndex;
                FillTransactionType();
                cbTransactionType.Text = oldtrans;
                cbTransactionType.SelectedIndex = oldtransindex;
                if (General.CurrentSetting.MsetScanBarCode == "Y")
                    btnPrintBarCode.Visible = true;
                else
                    btnPrintBarCode.Visible = false;
                //if (General.CurrentSetting.MsetPurchaseGetPendingScheme == "Y")
                //{
                //    lblpendingscheme.Visible = true;
                //    txtPendingScheme.Visible = true;
                //}
                //else
                //{
                lblpendingscheme.Visible = false;
                txtPendingScheme.Visible = false;
                //}
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool RefreshProductList()
        {
            //Product prod = new Product();
            //DataTable proddt = prod.GetOverviewData();
            //mpMSVC.DataSource = proddt;
            DataTable dtable = new DataTable();
            //dtable = General.ProductList;
            Product prod = new Product();
            dtable = prod.GetOverviewData();
            mpMSVC.DataSource = dtable;
            //  mpMSVC.DataSource = General.ProductList;
            mpMSVC.BindGridSub();
            return true;
        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;

            try
            {
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtScanCode.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.B && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        pnlProductDetail.Focus();
                        txtBatch.Focus();
                        this.ActiveControl = txtBatch;
                        retValue = true;
                    }
                    else
                    {

                        txtBillNumber.Focus();
                        retValue = true;

                    }

                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        btnCancel.Focus();
                        btnCancel.BackColor = General.ControlFocusColor;
                        retValue = true;
                    }
                    else
                    {
                        this.mcbCreditor.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    if (pnlEditProduct.Visible == false /* || pnlEditProduct.Enabled == false*/)
                    {
                        _purchaseMode = PurchaseMode.Add;
                        pobj = new Product();
                        pnlEditProduct.Visible = true;
                        pnlEditProduct.BringToFront();
                        pnlEditProduct.Enabled = true;


                        if (_purchaseMode != PurchaseMode.Edit)
                        {
                            _purchaseMode = PurchaseMode.Add;
                            lblEditProductTitle.Text = "Add Product Details";
                            mcbCompany1.SelectedID = "";
                            mcbGenCatOpStock.SelectedID = "";
                            mcbProductCategory1.SelectedID = "";
                        }
                        if (pnlProductDetail.Visible == false)
                        {
                            pnlProductDetail.Visible = true;
                            pnlProductDetail.BringToFront();
                            pnlProductDetail.Enabled = true;
                        }
                        pnlEditProduct.Select();
                        pnlEditProduct.Focus();
                        this.ActiveControl = txtProdName;
                        txtProdName.Select();
                        txtProdName.Focus();
                        txtProdName.Text = Convert.ToString(mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value);
                        retValue = true;
                    }
                }
                if (keyPressed == Keys.E && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtExpiry.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.F && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        mcbShelf.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.H && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtSchemeAmount.Focus();
                        retValue = true;
                    }
                    else
                    {
                        txtCashDiscountPerS.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.I && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtItemDiscountPer.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.L && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        dgvLastPurchase.Visible = true;
                        dgvLastPurchase.Location = GetdgvLastPurchaseLocation();
                        dgvLastPurchase.BringToFront();
                    }
                    else
                    {
                        txtSaleRate.Focus();
                    }
                    retValue = true;
                }
                if (keyPressed == Keys.M && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtMRP.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == false)
                    {
                        txtNarration.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.O && modifier == Keys.Alt)
                {
                    if (pnlDebitCreditNote.Visible == true)
                    {
                        btnCRDBOKClick();
                        retValue = true;
                    }
                    else if (pnlPurchaseOrder.Visible == true)
                    {
                        btnPurchaseOrderOKClick();
                        retValue = true;
                    }
                    else
                    {
                        btnOK.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.P && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtPurchaseVATPer.Focus();
                        retValue = true;
                    }
                    else
                    {
                        txtSplDiscountS.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.Q && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtQuantity.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.R && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtReplacement.Focus();
                        retValue = true;
                    }
                    else if (pnlSummary.Visible == true)
                    {
                        btnCRDBNote_Click(btnCRDBNote, null);
                    }

                }

                if (keyPressed == Keys.S && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtSchemeQuantity.Focus();
                        retValue = true;
                    }
                    else
                    {
                        btnSummary.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtTradeRate.Focus();
                        retValue = true;
                    }

                }

                if (keyPressed == Keys.Z && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtCSTAmount.Focus();
                        retValue = true;
                    }

                }

                if (keyPressed == Keys.Escape)
                {
                    bool flag = false;
                    if (pnlEnterScheme.Visible == true)
                    {
                        pnlEnterScheme.Visible = false;
                        txtSchemeAmount.Focus();
                        retValue = true;
                    }
                    else if (pnlPaymentDetails.Visible)
                    {
                        pnlPaymentDetails.Visible = false;
                        retValue = true;
                    }
                    else if (IfShortCutOpen == true)
                    {
                        tsBtnShortcuts.PerformClick();
                        retValue = true;
                    }
                    else if (dgvBatchGrid.Visible)
                    {
                        dgvBatchGrid.Visible = false;
                        pnlProductDetail.Enabled = true;
                        txtBatch.Focus();
                        flag = true;
                        retValue = true;
                    }
                    else if (dgvLastPurchase.Visible == true)
                    {
                        dgvLastPurchase.Visible = false;
                        dgvLastPurchase.SendToBack();
                        flag = true;
                        retValue = true;
                    }
                    else if (pnlProductDetail.Visible && dgvBatchGrid.Visible == false)
                    {
                        btnCancelClick();
                        retValue = true;
                    }
                    else if (pnlDebitCreditNote.Visible)
                    {
                        btnCRDBOKClick();
                        retValue = true;
                    }
                    else if (pnlSummary.Visible)
                    {
                        btnCancelSClick();
                        retValue = true;
                    }
                    else if (mpMSVC.VisibleProductGrid() == true) //kiran
                    {
                        mpMSVC.SetFocus(mpMSVC.Rows.Count - 1, 1);
                        retValue = true;
                    }
                    else
                        retValue = Exit();

                    _purchaseMode = PurchaseMode.Add;
                    if (flag == false)
                        ClearStockData();
                }
                //if (retValue && pnlEditProduct.Visible == false) // [ansuman][28.11.2016]
                //{
                //    mpMSVC.Focus();
                //    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = "";
                //}
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

        # region Stock update

        private bool CheckStockForUpdate(DataTable stocktable)
        {
            bool retValue = true;
            try
            {
                int mclosingstock = 0;
                int prodqty = 0;
                int prodscm = 0;
                int prodrepl = 0;
                bool ifbreak = false;
                foreach (DataGridViewRow temprow in dgtemp.Rows)
                {

                    if (temprow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(temprow.Cells["Temp_Quantity"].Value) > 0)
                    {
                        _Purchase.ProductID = Convert.ToInt32(temprow.Cells["Temp_ProductID"].Value.ToString());
                        _Purchase.Batchno = temprow.Cells["Temp_BatchNumber"].Value.ToString();
                        if (temprow.Cells["Temp_MRP"].Value != null)
                            _Purchase.MRP = Convert.ToDouble(temprow.Cells["Temp_MRP"].Value.ToString());
                        if (temprow.Cells["Temp_Scheme"].Value != null)
                            _Purchase.SchemeQuanity = Convert.ToInt32(temprow.Cells["Temp_Scheme"].Value.ToString());
                        if (temprow.Cells["Temp_Replacement"].Value != null)
                            _Purchase.ReplacementQuantity = Convert.ToInt32(temprow.Cells["Temp_Replacement"].Value.ToString());
                        if (temprow.Cells["Temp_Quantity"].Value != null)
                            _Purchase.Quantity = Convert.ToInt32(temprow.Cells["Temp_Quantity"].Value.ToString());
                        if (temprow.Cells["Temp_StockID"].Value != null)
                            _Purchase.StockID = temprow.Cells["Temp_StockID"].Value.ToString();
                        mclosingstock = 0;
                        foreach (DataRow dr in stocktable.Rows)
                        {
                            if (dr["StockID"].ToString() == _Purchase.StockID)
                            {
                                //  if (dr["ProductID"].ToString() == _Purchase.ProductID && dr["BatchNumber"].ToString() == _Purchase.Batchno && Convert.ToDouble(dr["MRP"].ToString()) == _Purchase.MRP)
                                mclosingstock = Convert.ToInt32(dr["ClosingStock"].ToString());
                                break;
                            }

                        }
                        mclosingstock = mclosingstock - _Purchase.Quantity - _Purchase.SchemeQuanity - _Purchase.ReplacementQuantity;
                        prodqty = 0;
                        prodrepl = 0;
                        prodscm = 0;
                        foreach (DataGridViewRow prodrow in mpMSVC.Rows)
                        {
                            if (prodrow.Cells["Col_ProductName"].Value != null)
                            {
                                if (prodrow.Cells["Col_StockID"].Value != null && prodrow.Cells["Col_StockID"].Value.ToString() != "")
                                {
                                    if (prodrow.Cells["Col_StockID"].Value.ToString() == _Purchase.StockID)
                                    {
                                        //if (prodrow.Cells["Col_ProductID"].Value.ToString() == _Purchase.ProductID &&
                                        //    prodrow.Cells["Col_BatchNumber"].Value.ToString() == _Purchase.Batchno &&
                                        //   Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString()) == _Purchase.MRP)
                                        {
                                            if (prodrow.Cells["Col_Scheme"].Value != null)
                                                prodscm = Convert.ToInt32(prodrow.Cells["Col_Scheme"].Value.ToString());
                                            if (prodrow.Cells["Col_Replacement"].Value != null)
                                                prodrepl = Convert.ToInt32(prodrow.Cells["Col_Replacement"].Value.ToString());
                                            if (prodrow.Cells["Col_Quantity"].Value != null)
                                                prodqty = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                                            mclosingstock = mclosingstock + prodqty + prodrepl + prodscm;
                                            if (mclosingstock < 0)
                                            {
                                                ifbreak = true;
                                                retValue = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (ifbreak == true)
                            break;
                    }
                    if (ifbreak == true)
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;

        }

        private bool CheckStockForDelete()
        {
            bool retValue = true;
            int CurrentClosingStock = 0;
            deletedproductname = "";
            //  DataRow dr;
            try
            {
                if (General.CurrentSetting.MsetSaleAllowNegativeStock != "Y")
                {
                    foreach (DataGridViewRow temprow in dgtemp.Rows)
                    {

                        if (temprow.Cells["Temp_ProductName"].Value != null &&
                           Convert.ToDouble(temprow.Cells["Temp_Quantity"].Value) > 0)
                        {
                            _Purchase.StockID = temprow.Cells["Temp_StockID"].Value.ToString();
                            _Purchase.ProductID = Convert.ToInt32(temprow.Cells["Temp_ProductID"].Value.ToString());
                            _Purchase.Batchno = temprow.Cells["Temp_BatchNumber"].Value.ToString();
                            if (temprow.Cells["Temp_MRP"].Value != null)
                                _Purchase.MRP = Convert.ToDouble(temprow.Cells["Temp_MRP"].Value.ToString());
                            if (temprow.Cells["Temp_Scheme"].Value != null)
                                _Purchase.SchemeQuanity = Convert.ToInt32(temprow.Cells["Temp_Scheme"].Value.ToString());
                            if (temprow.Cells["Temp_Replacement"].Value != null)
                                _Purchase.ReplacementQuantity = Convert.ToInt32(temprow.Cells["Temp_Replacement"].Value.ToString());
                            if (temprow.Cells["Temp_Quantity"].Value != null)
                                _Purchase.Quantity = Convert.ToInt32(temprow.Cells["Temp_Quantity"].Value.ToString());
                            if (temprow.Cells["Temp_UnitOfMeasure"].Value != null)
                                _Purchase.ProdLoosePack = Convert.ToInt16(temprow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                            CurrentClosingStock = _Purchase.GetCurrentClosingStock(_Purchase.StockID);
                            if (CurrentClosingStock < (_Purchase.Quantity + _Purchase.SchemeQuanity + _Purchase.ReplacementQuantity))
                            {
                                deletedproductname = temprow.Cells["Temp_ProductName"].Value.ToString().Trim() + " " + temprow.Cells["Temp_UnitOfMeasure"].Value.ToString().Trim() + " " + temprow.Cells["Temp_Pack"].Value.ToString().Trim();
                                retValue = false;
                                break;
                            }
                            //dr = _Purchase.IFRecordFoundInStockTable();
                            //if (dr == null)
                            //{
                            //    retValue = false;
                            //    break;
                            //}
                            //else
                            //{

                            //    ReducePreviousStock();
                            //    _Purchase.DeleteAccountDetails();
                            //    _Purchase.DeletePreviousRecords();
                            //    _Purchase.DeleteDetails();

                            //}
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
        private bool SaveParticularsProductwise()
        {
            bool returnVal = false;
            _Purchase.SerialNumber = 0;
            int mqty = 0;
            int mrepl = 0;
            int mscm = 0;
            int oldTempStock = 0;
            int CurrentClosingStock = 0;
            string ThisStockID = "";
            //  int IntStockID = 0;
            //string oldstockId = "";
            string oldAccountId = "";
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
                        _Purchase.ProductID = 0;
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
                        _Purchase.CSTPercent = 0;
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
                        _Purchase.GSTPurchaseAmountZero = 0;
                        _Purchase.GSTSAmount = 0;
                        _Purchase.GSTCAmount = 0;
                        _Purchase.GSTSPurchaseAmount = 0;
                        _Purchase.GSTCPurchaseAmount = 0;
                        _Purchase.ProfitPercent = 0;
                        _Purchase.PriceToRetailer = 0;
                        //  _Purchase.

                        //   _Purchase.PendingSchemeQuantity = 0;
                        //  _Purchase.DistributorSaleRate = 0;
                        //    _Purchase.DistributorSaleRatePercent = 0;
                        _Purchase.oldAccountId = "";
                        //_Purchase.AccountID = 0;
                        //_Purchase.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _Purchase.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
                        _Purchase.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        if (prodrow.Cells["Col_UnitOfMeasure"].Value != null && !String.IsNullOrEmpty(prodrow.Cells["Col_UnitOfMeasure"].Value.ToString()))
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

                        //if (string.IsNullOrEmpty(Convert.ToString(prodrow.Cells["Col_PendingSchemeQuantity"].Value)) == true)
                        //    _Purchase.PendingSchemeQuantity = 0;
                        //else
                        //    _Purchase.PendingSchemeQuantity = Convert.ToInt32(prodrow.Cells["Col_PendingSchemeQuantity"].Value.ToString());

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
                        //if (prodrow.Cells["Col_CSTAmount"].Value != null)
                        //    _Purchase.AmountCST = Convert.ToDouble(prodrow.Cells["Col_CSTAmount"].Value.ToString());
                        //if (prodrow.Cells["Col_CSTPer"].Value != null)
                        //    _Purchase.CSTPercent = Convert.ToDouble(prodrow.Cells["Col_CSTPer"].Value.ToString());
                        //if (prodrow.Cells["Col_SplDiscountPer"].Value != null)
                        //    _Purchase.SplDiscountPercent = Convert.ToDouble(prodrow.Cells["Col_SplDiscountPer"].Value.ToString());
                        //if (prodrow.Cells["Col_SplDiscountAmount"].Value != null)
                        //    _Purchase.AmountSplDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Col_SplDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_ShelfID"].Value != null)
                            _Purchase.ShelfID = prodrow.Cells["Col_ShelfID"].Value.ToString();
                        if (prodrow.Cells["Col_VATAmountPurchase"].Value != null)
                            _Purchase.AmountPurchaseVAT = Convert.ToDouble(prodrow.Cells["Col_VATAmountPurchase"].Value.ToString());

                        if (prodrow.Cells["Col_VATAmountSale"].Value != null)
                            _Purchase.AmountProductVAT = Convert.ToDouble(prodrow.Cells["Col_VATAmountSale"].Value.ToString());
                        if (prodrow.Cells["Col_CashDiscountAmount"].Value != null)
                            _Purchase.AmountCashDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Col_CashDiscountAmount"].Value.ToString());

                        if (prodrow.Cells["Col_PriceToRetailer"].Value != null)
                            _Purchase.PriceToRetailer = Convert.ToDouble(prodrow.Cells["Col_PriceToRetailer"].Value.ToString());

                        if (prodrow.Cells["Col_ProfitPercent"].Value != null)
                            _Purchase.ProfitPercent = Convert.ToDouble(prodrow.Cells["Col_ProfitPercent"].Value.ToString());


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
                        if (prodrow.Cells["Col_GSTAmountZero"].Value != null && prodrow.Cells["Col_GSTAmountZero"].Value.ToString() != string.Empty)
                            _Purchase.GSTPurchaseAmountZero = Convert.ToDouble(prodrow.Cells["Col_GSTAmountZero"].Value.ToString());
                        if (prodrow.Cells["Col_GSTSAmount"].Value != null && prodrow.Cells["Col_GSTSAmount"].Value.ToString() != string.Empty)
                            _Purchase.GSTSPurchaseAmount = Convert.ToDouble(prodrow.Cells["Col_GSTSAmount"].Value.ToString());
                        if (prodrow.Cells["Col_GSTCAmount"].Value != null && prodrow.Cells["Col_GSTCAmount"].Value.ToString() != string.Empty)
                            _Purchase.GSTCPurchaseAmount = Convert.ToDouble(prodrow.Cells["Col_GSTCAmount"].Value.ToString());
                        if (prodrow.Cells["Col_GSTS"].Value != null && prodrow.Cells["Col_GSTS"].Value.ToString() != string.Empty)
                            _Purchase.GSTSAmount = Convert.ToDouble(prodrow.Cells["Col_GSTS"].Value.ToString());
                        if (prodrow.Cells["Col_GSTC"].Value != null && prodrow.Cells["Col_GSTC"].Value.ToString() != string.Empty)
                            _Purchase.GSTCAmount = Convert.ToDouble(prodrow.Cells["Col_GSTC"].Value.ToString());
                        string expdt = "";
                        expdt = _Purchase.ExpiryDate;
                        if (expdt != "")
                        {
                            _Purchase.ExpiryDate = General.GetExpiryInyyyymmddForm(expdt);
                        }
                        //   IntStockID = 0;
                        ThisStockID = "";
                        oldTempStock = 0;
                        if (General.CurrentSetting.MsetPurchaseHold != "Y")
                        {
                            // ThisStockID = _Purchase.CheckForBatchMRPStockIDInStockTable_stock(); //ss18/10
                            ThisStockID = _Purchase.CheckForBatchMRPStockIDInStockTable();
                            oldAccountId = _Purchase.oldaccountIDFind();
                            _Purchase.oldAccountId = oldAccountId;

                            if (ThisStockID != "")
                            {
                                _Purchase.IntStockID = Convert.ToInt32(ThisStockID);
                                CurrentClosingStock = _Purchase.GetCurrentClosingStock(ThisStockID);

                                if (_Mode == OperationMode.Edit)
                                {
                                    oldTempStock = GetOldStockFromTempGrid(ThisStockID);
                                }
                                if (((CurrentClosingStock - (oldTempStock) + ((_Purchase.Quantity + _Purchase.SchemeQuanity + _Purchase.ReplacementQuantity))) >= 0) || ((CurrentClosingStock - (oldTempStock) + ((_Purchase.Quantity + _Purchase.SchemeQuanity + _Purchase.ReplacementQuantity))) <= 0 && General.CurrentSetting.MsetSaleAllowNegativeStock == "Y"))
                                    returnVal = _Purchase.UpdatePurchaseIntblStock();
                                else
                                {
                                    returnVal = false;
                                    break;
                                }
                            }
                            else
                            {
                                ThisStockID = "";
                            }

                            if (ThisStockID == "")
                            {

                                _Purchase.PurScanCode = _Purchase.GetScanGodeForCurrentBatch(_Purchase.ProductID);

                                ThisStockID = _Purchase.AddProductDetailsInStockTable();
                                if (ThisStockID != "")
                                    returnVal = true;

                            }


                            if (returnVal)
                            {
                                // returnVal = _Purchase.UpdatePurchaseOrder(); ss 18-10
                                returnVal = _Purchase.UpdatePurchaseStockInMasterProduct();
                            }

                            else
                                break;
                            if (returnVal)
                            {
                                _Purchase.UpdateLastPurhcaseDataInMasterProduct();
                                // ss 18-10
                                //  _Purchase.RemoveFromShortList(_Purchase.ProductID);
                                //   _Purchase.GetFirstAndSecondCreditor(_Purchase.ProductID);
                                //if (_Purchase.FirstCreditor != _Purchase.AccountID && _Purchase.SecondCreditor != _Purchase.AccountID)
                                //{
                                //    if (_Purchase.FirstCreditor == string.Empty)
                                //        _Purchase.FillFirstCreditorInMasterProduct();
                                //    else if (_Purchase.SecondCreditor == string.Empty)
                                //        _Purchase.FillSecondCreditorInMasterProduct();
                                //}
                            }
                            else
                                break;

                        }
                        else
                            returnVal = true;
                        if (returnVal)
                        {
                            _Purchase.IntStockID = Convert.ToInt32(ThisStockID);
                            returnVal = _Purchase.AddProductDetailsSS();
                        }
                        else
                            break;
                    }
                }
            }
            catch { returnVal = false; }
            return returnVal;

        }


        private int GetOldStockFromTempGrid(string stockID)
        {
            int closingstock = 0;
            string tempstockID = "";
            int qty = 0;
            int repl = 0;
            int scm = 0;
            foreach (DataGridViewRow dr in dgtemp.Rows)
            {
                tempstockID = "";
                if (dr.Cells["Temp_StockID"].Value != null && dr.Cells["Temp_StockID"].Value.ToString() != "")
                    tempstockID = dr.Cells["Temp_StockID"].Value.ToString();
                if (tempstockID == stockID)
                {
                    if (dr.Cells["Temp_Quantity"].Value != null && dr.Cells["Temp_Quantity"].Value.ToString() != "")
                        qty = Convert.ToInt32(dr.Cells["Temp_Quantity"].Value.ToString());
                    if (dr.Cells["Temp_Scheme"].Value != null && dr.Cells["Temp_Scheme"].Value.ToString() != "")
                        scm = Convert.ToInt32(dr.Cells["Temp_Scheme"].Value.ToString());
                    if (dr.Cells["Temp_Replacement"].Value != null && dr.Cells["Temp_Replacement"].Value.ToString() != "")
                        repl = Convert.ToInt32(dr.Cells["Temp_Replacement"].Value.ToString());
                    closingstock = qty + scm + repl;
                    break;
                }
            }
            return closingstock;
        }

        //private bool UpdateClosingStockinCache()
        //{
        //    bool returnVal = false;
        //    try
        //    {
        //        General.UpdateProductListCacheTest(mpMSVC.Rows, "Col_ProductID", dgtemp.Rows, "Temp_ProductID");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        returnVal = false;
        //    }
        //    return returnVal;
        //}

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

        private bool clearPreviousdebitcreditnotes()
        {
            bool retValue = true;
            retValue = _Purchase.clearPreviousdebitcreditnotes(_Purchase.Id);
            return retValue;
        }

        #endregion

        #region IChildDetail Members

        #endregion

        #region Other Private Methods
        public override bool IsDetailChanged()
        {
            return true;
        }
        void mcbCreditor_SeletectIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                Account _account = new Account();
                _account.Id = mcbCreditor.SelectedID;
                _Purchase.AccountID = mcbCreditor.SelectedID;
                _account.ReadDetailsByID();
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[5];

                    if (_Mode == OperationMode.Add)
                        txtCashDiscountPerS.Text = _account.AccDiscountOffered.ToString("0.00");
                    _Purchase.IFOMS = _account.IFOMS;
                    _Purchase.GetPendingAmount(mcbCreditor.SelectedID);
                    _Purchase.GetOpeningBalance(mcbCreditor.SelectedID);
                    _Purchase.PendingAmount = _Purchase.OpeningBalance + (_Purchase.TotalDebit - _Purchase.TotalCredit);
                    txtPendingBalance.Text = Math.Abs(_Purchase.PendingAmount).ToString("#0.00");
                    _Purchase.PendingAmount = 0;
                    _Purchase.PendingAmount = _Purchase.GetDNAmount(mcbCreditor.SelectedID);
                    txtPendingCN.Text = _Purchase.PendingAmount.ToString("#0.00");
                    if (General.CurrentSetting.MsetPurchaseCopyPurchaseOrder == "Y")
                    {

                        DataTable dt = FillPurchaseOrder();
                        //   pnlPurchaseOrder.Location = GetpnlPurchaseOrderLocation();
                        pnlPurchaseOrder.BringToFront();
                        pnlPurchaseOrder.Visible = true;
                        dgPurchaseOrder.Visible = true;
                        pnlPurchaseOrder.Select();
                        dgPurchaseOrder.Focus();
                    }
                    else
                        txtBillNumber.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void mpMSVC_OnDetailsFilled(DataGridViewRow selectedRow)
        {
            // bool isTempPurchaseVisible = false;
            //if (General.CurrentSetting.MsetSaleAllowNegativeStock == "Y")
            //{
            //    try
            //    {

            //        _Purchase.MRP = 0;
            //        double mmamt = 0;
            //        if (mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
            //            _Purchase.MRP = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
            //        if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null)
            //            mmamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString());
            //        _Purchase.ProductID = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
            //        //       int mmqty = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());

            //        //bool ifanyrow = false;
            //        //  pnltempoff = false;

            //        if (dgTempPurchase.Rows.Count > 0 && General.CurrentSetting.MsetSaleAllowNegativeStock == "Y")
            //        {
            //            foreach (DataGridViewRow dr in dgTempPurchase.Rows)
            //            {
            //                if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() == _Purchase.ProductID)
            //                {
            //                    //ifanyrow = true;
            //                    isTempPurchaseVisible = true;
            //                    dr.Visible = true;
            //                    break;
            //                }
            //                else
            //                    dr.Visible = false;
            //            }

            //            //if (ifanyrow)
            //            //{
            //            //    pnlTempPurchase.BringToFront();
            //            //    pnlTempPurchase.Visible = true;
            //            //    GetpnlTempPurchaseLocation();
            //            //    dgTempPurchase.Focus();
            //            //    isTempPurchaseVisible = true;
            //            //}
            //        }

            //        if (!isTempPurchaseVisible)
            //        {


            //            mpMSVC.Enabled = false;
            //            if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
            //            {
            //                //dgvLastPurchase.Visible = true;
            //                //dgvLastPurchase.Location = GetdgvLastPurchaseLocation();
            //                //dgvLastPurchase.BringToFront();
            //            }
            //            //FillLastPurchase();
            //            //pnlBillDetails.Enabled = false;
            //            //pnlProductDetail.BringToFront();
            //            //pnlProductDetail.Location = GetpnlProductDetailLocation();
            //            //pnlProductDetail.Visible = true;

            //            //_purchaseMode = PurchaseMode.Edit;
            //            //pnlEditProduct.BringToFront();
            //            //pnlEditProduct.Visible = true;
            //            //pnlEditProduct.Enabled = false;
            //            //FillPnlEditProduct();
            //            //FillProductAndCmpnyData(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString());
            //            //pnlEditProduct.Location = GetpnlEditProductLocation();

            //            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _Purchase.StatementNumber > 0)
            //                pnlProductDetail1.Enabled = false;
            //            else
            //                pnlProductDetail1.Enabled = true;

            //            _LastStockID = string.Empty;
            //            if (mmamt == 0)
            //            {
            //                IfEditPreviousRow = "N";
            //                FillLastPurchaseDataFromMasterProduct();
            //            }
            //            else
            //            {
            //                IfEditPreviousRow = "Y";
            //                FillDataFromMPSVRow();
            //            }
            //            FillBatchGrid();
            //            if (mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value == null || mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value.ToString() != "Y")
            //            {
            //                txtBatch.Enabled = true;
            //                txtExpiry.Enabled = true;
            //                txtMRP.Enabled = true;

            //            }
            //            else
            //            {
            //                txtBatch.Enabled = false;
            //                txtExpiry.Enabled = false;
            //                txtMRP.Enabled = false;
            //            }
            //        }
            //    }
            //    catch (Exception Ex)
            //    {
            //        Log.WriteException(Ex);
            //    }
            //}
            //else
            //{
            try
            {

                _Purchase.MRP = 0;
                double mmamt = 0;
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                    _Purchase.MRP = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null)
                    mmamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString());
                _Purchase.ProductID = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString());
                mpMSVC.Enabled = false;
                if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
                {
                    //dgvLastPurchase.Visible = true;
                    //dgvLastPurchase.Location = GetdgvLastPurchaseLocation();
                    //dgvLastPurchase.BringToFront();
                }
                //FillLastPurchase();
                pnlBillDetails.Enabled = false;

                //pnlProductDetail.BringToFront();
                //pnlProductDetail.Location = GetpnlProductDetailLocation();
                //pnlProductDetail.Visible = true;

                //_purchaseMode = PurchaseMode.Edit;
                //pnlEditProduct.BringToFront();
                //pnlEditProduct.Visible = true;
                //pnlEditProduct.Enabled = false;
                //FillPnlEditProduct();
                //FillProductAndCmpnyData(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString());
                //pnlEditProduct.Location = GetpnlEditProductLocation();

                if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _Purchase.StatementNumber > 0)
                    pnlProductDetail1.Enabled = false;
                else
                    pnlProductDetail1.Enabled = true;

                _LastStockID = string.Empty;
                if (mmamt == 0)
                {
                    IfEditPreviousRow = "N";
                    FillLastPurchaseDataFromMasterProduct();
                }
                else
                {
                    IfEditPreviousRow = "Y";
                    FillDataFromMPSVRow();
                }
                FillBatchGrid();
                //if (mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value == null || mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value.ToString() != "Y")
                //{
                txtBatch.Enabled = true;
                txtExpiry.Enabled = true;
                txtMRP.Enabled = true;
                //}
                //else
                //{
                //    txtBatch.Enabled = false;
                //    txtExpiry.Enabled = false;
                //    txtMRP.Enabled = false;
                //}
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            //}

            //if (isTempPurchaseVisible)
            //{
            //    pnlTempPurchase.BringToFront();
            //    pnlTempPurchase.Visible = true;
            //    GetpnlTempPurchaseLocation();
            //    dgTempPurchase.Focus();
            //}
            //else
            //{
            ShowProductDetailPanel();
            //}
        }

        private void ShowProductDetailPanel()
        {
            FillLastPurchase();
            pnlBillDetails.Enabled = false;
            pnlProductDetail.BringToFront();
            pnlProductDetail.Location = GetpnlProductDetailLocation();
            pnlProductDetail.Visible = true;

            _purchaseMode = PurchaseMode.Edit;
            pnlEditProduct.BringToFront();
            pnlEditProduct.Visible = true;
            pnlEditProduct.Enabled = false;
            FillPnlEditProduct();
            FillProductAndCmpnyData(Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString()));
            pnlEditProduct.Location = GetpnlEditProductLocation();
            txtQuantity.Focus();
        }

        private Point GetpnlProductDetailLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = mpMSVC.Location.X + 360;
                pt.Y = mpMSVC.Location.Y + -15;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private Point GetpnlEditProductLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlProductDetail.Location.X;
                pt.Y = pnlProductDetail.Location.Y - 150;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        //private Point GetpnlSummaryLocation()
        //{
        //    Point pt = new Point();
        //    try
        //    {
        //        pt.X = mpMSVC.Location.X + 305;
        //        pt.Y = mpMSVC.Location.Y + -5;
        //    }
        //    catch (Exception ex) { Log.WriteError(ex.ToString()); }
        //    return pt;
        //}
        private Point GetdgvLastPurchaseLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlBillDetails.Location.X + 400;
                pt.Y = pnlBillDetails.Location.Y + 23;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        private Point GetpnlSummaryLocation()
        {
            Point pt = new Point();
            int difference = pnlSummary.Size.Height - mpMSVC.Size.Height;

            try
            {
                pt.X = mpMSVC.Location.X + 565;
                pt.Y = mpMSVC.Location.Y - difference;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private Point GetpnlGSTLocation()
        {
            Point pt = new Point();
            int difference = pnlSummary.Size.Height - mpMSVC.Size.Height;

            try
            {
                pt.X = mpMSVC.Location.X + 50;
                pt.Y = mpMSVC.Location.Y - difference + 50;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        private Point GetpnlPurchaseOrderLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlBillDetails.Location.X + 200;
                pt.Y = pnlBillDetails.Location.Y + 200;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private Point GetdgvBatchGridLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = mpMSVC.Location.X + 335;
                pt.Y = mpMSVC.Location.Y + 125;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private bool DeletePreviousRows()
        {
            bool returnVal = false;
            try
            {
                returnVal = _Purchase.DeletePreviousRecords();
            }
            catch { returnVal = false; }
            return returnVal;
        }
        private bool AddPreviousRowsInDeleteDetail()
        {
            bool returnVal = false;
            _Purchase.SerialNumber = 0;
            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null)
                    {
                        _Purchase.SerialNumber += 1;
                        _Purchase.ProductID = 0;
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
                        _Purchase.ProductMargin = 0;
                        _Purchase.ProductMargin2 = 0;

                        _Purchase.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _Purchase.ProductID = Convert.ToInt32(prodrow.Cells["Temp_ProductID"].Value.ToString());
                        _Purchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        if (prodrow.Cells["Temp_UnitOfMeasure"].Value != null)
                            _Purchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                        if (prodrow.Cells["Temp_MRP"].Value != null)
                            _Purchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value.ToString());
                        if (prodrow.Cells["Temp_Expiry"].Value != null)
                            _Purchase.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
                        if (_Purchase.Expiry != "00/00")
                            _Purchase.ExpiryDate = General.GetValidExpiryDate(prodrow.Cells["Temp_Expiry"].Value.ToString());
                        if (prodrow.Cells["Temp_TradeRate"].Value != null)
                            _Purchase.TradeRate = Convert.ToDouble(prodrow.Cells["Temp_TradeRate"].Value.ToString());
                        if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
                            _Purchase.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value.ToString());
                        if (prodrow.Cells["Temp_SaleRate"].Value != null)
                            _Purchase.SaleRate = Convert.ToDouble(prodrow.Cells["Temp_SaleRate"].Value.ToString());
                        if (prodrow.Cells["Temp_Scheme"].Value != null)
                            _Purchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value.ToString());
                        if (prodrow.Cells["Temp_Replacement"].Value != null)
                            _Purchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
                        if (prodrow.Cells["Temp_Quantity"].Value != null)
                            _Purchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value.ToString());
                        if (prodrow.Cells["Temp_VAT"].Value != null && prodrow.Cells["Temp_VAT"].Value.ToString() != "")
                            _Purchase.PurchaseVATPercent = Convert.ToDouble(prodrow.Cells["Temp_VAT"].Value.ToString());
                        if (prodrow.Cells["Temp_ProdVATPer"].Value != null)
                            _Purchase.ProductVATPercent = Convert.ToDouble(prodrow.Cells["Temp_ProdVATPer"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemDiscountPer"].Value != null)
                            _Purchase.ItemDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemDiscountAmount"].Value != null)
                            _Purchase.AmountItemDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value != null)
                            _Purchase.AmountSchemeDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_CSTAmount"].Value != null)
                            _Purchase.AmountCST = Convert.ToDouble(prodrow.Cells["Temp_CSTAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_SplDiscountPer"].Value != null)
                            _Purchase.SplDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Temp_SplDiscountAmount"].Value != null)
                            _Purchase.AmountSplDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountAmount"].Value.ToString());

                        if (prodrow.Cells["Temp_VATAmountPurchase"].Value != null)
                            _Purchase.AmountPurchaseVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountPurchase"].Value.ToString());

                        if (prodrow.Cells["Temp_VATAmountSale"].Value != null)
                            _Purchase.AmountProductVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountSale"].Value.ToString());
                        if (prodrow.Cells["Temp_CashDiscountAmount"].Value != null)
                            _Purchase.AmountCashDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_CashDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_Margin"].Value != null)
                            _Purchase.ProductMargin = Convert.ToDouble(prodrow.Cells["Temp_Margin"].Value.ToString());

                        if (prodrow.Cells["Temp_Margin2"].Value != null)
                            _Purchase.ProductMargin2 = Convert.ToDouble(prodrow.Cells["Temp_Margin2"].Value.ToString());

                        if (prodrow.Cells["Temp_StockID"].Value != null)
                            _Purchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();

                        string expdt = "";
                        expdt = _Purchase.ExpiryDate;
                        if (expdt != "")
                        {
                            _Purchase.ExpiryDate = General.GetExpiryInyyyymmddForm(expdt);
                        }

                        returnVal = _Purchase.AddDeletedProductDetailsSS();

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
        private bool AddPreviousRowsInChangedDetail()
        {
            bool returnVal = false;
            _Purchase.SerialNumber = 0;
            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null)
                    {
                        _Purchase.SerialNumber += 1;
                        _Purchase.ProductID = 0;
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
                        _Purchase.ProductMargin = 0;
                        _Purchase.ProductMargin2 = 0;

                        _Purchase.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _Purchase.ProductID = Convert.ToInt32(prodrow.Cells["Temp_ProductID"].Value.ToString());
                        _Purchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        if (prodrow.Cells["Temp_UnitOfMeasure"].Value != null)
                            _Purchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                        if (prodrow.Cells["Temp_MRP"].Value != null)
                            _Purchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value.ToString());
                        if (prodrow.Cells["Temp_Expiry"].Value != null)
                            _Purchase.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
                        if (_Purchase.Expiry != "00/00")
                            _Purchase.ExpiryDate = General.GetValidExpiryDate(prodrow.Cells["Temp_Expiry"].Value.ToString());
                        if (prodrow.Cells["Temp_TradeRate"].Value != null)
                            _Purchase.TradeRate = Convert.ToDouble(prodrow.Cells["Temp_TradeRate"].Value.ToString());
                        if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
                            _Purchase.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value.ToString());
                        if (prodrow.Cells["Temp_SaleRate"].Value != null)
                            _Purchase.SaleRate = Convert.ToDouble(prodrow.Cells["Temp_SaleRate"].Value.ToString());
                        if (prodrow.Cells["Temp_Scheme"].Value != null)
                            _Purchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value.ToString());
                        if (prodrow.Cells["Temp_Replacement"].Value != null)
                            _Purchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
                        if (prodrow.Cells["Temp_Quantity"].Value != null)
                            _Purchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value.ToString());
                        if (prodrow.Cells["Temp_VAT"].Value != null && prodrow.Cells["Temp_VAT"].Value.ToString() != "")
                            _Purchase.PurchaseVATPercent = Convert.ToDouble(prodrow.Cells["Temp_VAT"].Value.ToString());
                        if (prodrow.Cells["Temp_ProdVATPer"].Value != null)
                            _Purchase.ProductVATPercent = Convert.ToDouble(prodrow.Cells["Temp_ProdVATPer"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemDiscountPer"].Value != null)
                            _Purchase.ItemDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemDiscountAmount"].Value != null)
                            _Purchase.AmountItemDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value != null)
                            _Purchase.AmountSchemeDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_CSTAmount"].Value != null)
                            _Purchase.AmountCST = Convert.ToDouble(prodrow.Cells["Temp_CSTAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_SplDiscountPer"].Value != null)
                            _Purchase.SplDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Temp_SplDiscountAmount"].Value != null)
                            _Purchase.AmountSplDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountAmount"].Value.ToString());

                        if (prodrow.Cells["Temp_VATAmountPurchase"].Value != null)
                            _Purchase.AmountPurchaseVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountPurchase"].Value.ToString());

                        if (prodrow.Cells["Temp_VATAmountSale"].Value != null)
                            _Purchase.AmountProductVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountSale"].Value.ToString());
                        if (prodrow.Cells["Temp_CashDiscountAmount"].Value != null)
                            _Purchase.AmountCashDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_CashDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_Margin"].Value != null)
                            _Purchase.ProductMargin = Convert.ToDouble(prodrow.Cells["Temp_Margin"].Value.ToString());

                        if (prodrow.Cells["Temp_Margin2"].Value != null)
                            _Purchase.ProductMargin2 = Convert.ToDouble(prodrow.Cells["Temp_Margin2"].Value.ToString());

                        if (prodrow.Cells["Temp_StockID"].Value != null)
                            _Purchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();

                        string expdt = "";
                        expdt = _Purchase.ExpiryDate;
                        if (expdt != "")
                        {
                            _Purchase.ExpiryDate = General.GetExpiryInyyyymmddForm(expdt);
                        }

                        returnVal = _Purchase.AddChangedProductDetailsSS();

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
        private bool ReducePreviousStock()
        {
            bool returnVal = false;
            //bool ifRecordFound = false;
            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                        (Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0
                         || Convert.ToDouble(prodrow.Cells["Temp_Scheme"].Value) > 0
                         || Convert.ToDouble(prodrow.Cells["Temp_Replacement"].Value) > 0))
                    {
                        _Purchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        _Purchase.ProductID = Convert.ToInt32(prodrow.Cells["Temp_ProductID"].Value.ToString());
                        _Purchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _Purchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _Purchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _Purchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value);
                        _Purchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
                        _Purchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                        _Purchase.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value.ToString());
                        DataRow dr = _Purchase.IfStockIDFoundInStockTable(_Purchase.StockID);
                        if (dr != null)
                            returnVal = _Purchase.UpdatePurchaseIntblStockReduceFromTemp();
                        returnVal = _Purchase.UpdatePurchaseStockInmasterProductReduceFromTemp();

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


        private bool CheckStockForDeletedRows()
        {
            bool returnVal = true;
            string gridstockid;
            int CurrentClosingStock = 0;
            deletedproductname = "";
            //bool ifRecordFound = false;
            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_StockID"].Value != null &&
                        (Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0
                         || Convert.ToDouble(prodrow.Cells["Temp_Scheme"].Value) > 0
                         || Convert.ToDouble(prodrow.Cells["Temp_Replacement"].Value) > 0))
                    {
                        _Purchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        deletedproductname = prodrow.Cells["Temp_ProductName"].Value.ToString().Trim() + " " + prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString().Trim() + " " + prodrow.Cells["Temp_Pack"].Value.ToString().Trim();
                        //_Purchase.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        //_Purchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        //_Purchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _Purchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _Purchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value);
                        _Purchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
                        _Purchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                        string ifmatchfound = "N";
                        foreach (DataGridViewRow gridrow in mpMSVC.Rows)
                        {
                            gridstockid = "";
                            if (gridrow.Cells["Col_StockID"].Value != null && gridrow.Cells["Col_StockID"].Value.ToString() != "")
                                gridstockid = gridrow.Cells["Col_StockID"].Value.ToString();
                            if (_Purchase.StockID == gridstockid)
                            {
                                deletedproductname = "";
                                ifmatchfound = "Y";
                                break;
                            }

                        }
                        if (ifmatchfound == "N")
                        {
                            CurrentClosingStock = _Purchase.GetCurrentClosingStock(_Purchase.StockID);
                            if (CurrentClosingStock < (_Purchase.Quantity + _Purchase.SchemeQuanity + _Purchase.ReplacementQuantity) * _Purchase.ProdLoosePack)
                            {
                                returnVal = false;
                                break;
                            }
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


        #endregion

        # region Contruct

        private void ConstructMainColumns()
        {
            try
            {
                // mpMSVC.dgMainGrid.Rows.Clear();
                mpMSVC.ColumnsMain.Clear();
                DataGridViewTextBoxColumn column;
                DataGridViewCheckBoxColumn column1; //Amar

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
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UnitOfMeasure";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.Visible = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Company";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Co.";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Box1";
                column.HeaderText = "Box";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdVATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT %";
                column.Width = 85;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
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
                column.Width = 130;
                column.ReadOnly = true;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 60;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Exp";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "Trd.Rate";
                column.Width = 80;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT";
                column.DataPropertyName = "PurchaseVATPercent";
                column.HeaderText = "GST%";
                column.Width = 50;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_CSTPer";
                //column.DataPropertyName = "CSTPercent";
                //column.HeaderText = "CST%";
                //column.Width = 50;
                //column.ReadOnly = true;
                //column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.Visible = false;
                //mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "Scm";
                column.Width = 50;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Replacement";
                column.DataPropertyName = "ReplacementQuantity";
                column.HeaderText = "Repl";
                column.Width = 50;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountPer";
                column.DataPropertyName = "ItemDiscountPercent";
                column.HeaderText = "Disc";
                column.Width = 50;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 100;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountAmount";
                column.DataPropertyName = "AmountItemDiscount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_SplDiscountPer";
                //column.DataPropertyName = "SpecialDiscountPercent";
                //column.Width = 80;
                //column.Visible = false;
                //mpMSVC.ColumnsMain.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_SplDiscountAmount";
                //column.DataPropertyName = "AmountSpecialDiscount";
                //column.Width = 80;
                //column.Visible = false;
                //mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountPurchase";
                column.DataPropertyName = "AmountPurchaseVAT";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_CSTAmount";
                //column.DataPropertyName = "AmountCST";
                //column.Width = 80;
                //column.Visible = false;
                //mpMSVC.ColumnsMain.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_CSTPer";
                //column.DataPropertyName = "CSTPercent";
                //column.Width = 80;
                //column.Visible = false;
                //mpMSVC.ColumnsMain.Add(column);

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

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_DistributorSaleRate";
                //column.DataPropertyName = "DistributorSaleRate";
                //column.Width = 80;
                //column.Visible = false;
                //mpMSVC.ColumnsMain.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_DistributorSaleRatePer";
                //column.DataPropertyName = "DistributorSaleRatePer";
                //column.Width = 80;
                //column.Visible = false;
                //mpMSVC.ColumnsMain.Add(column);

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

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_PendingSchemeQuantity";
                //column.DataPropertyName = "PendingSchemeQuantity";
                //column.Width = 80;
                //column.Visible = false;
                //mpMSVC.ColumnsMain.Add(column);

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

                column1 = new DataGridViewCheckBoxColumn();  //Amar
                column1.Name = "Col_CheckEdit";
                column1.HeaderText = "CheckEdit";
                column1.ReadOnly = true;
                column1.Visible = false;
                column1.Width = 30;
                mpMSVC.ColumnsMain.Add(column1);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTAmountZero";
                column.DataPropertyName = "GSTAmountZero";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTSAmount";
                column.DataPropertyName = "GSTSAmount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTCAmount";
                column.DataPropertyName = "GSTCAmount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTIAmount";
                column.DataPropertyName = "GSTIAmount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTS";
                column.DataPropertyName = "GSTS";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                column = new DataGridViewTextBoxColumn();

                column.Name = "Col_GSTC";
                column.DataPropertyName = "GSTC";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTI";
                column.DataPropertyName = "GSTI";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PriceToRetailer";
                column.DataPropertyName = "PriceToRetailer";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercent";
                column.DataPropertyName = "ProfitPercent";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructSubColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMSVC.ColumnsSub.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProdName";
                column.Width = 350;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.Visible = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BoxQty";
                column.DataPropertyName = "ProdBoxQuantity";
                column.HeaderText = "BoxQty";
                column.Width = 55;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Cl.Stk";
                column.Width = 55;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdVATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFBarCodeRequired";
                column.DataPropertyName = "ProdIfBarCodeRequired";
                column.HeaderText = "BarCode";
                column.Width = 40;
                mpMSVC.ColumnsSub.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructBatchGrid()
        {
            DataGridViewTextBoxColumn column;
            try
            {
                dgvBatchGrid.Columns.Clear();
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batchno";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batchno";
                column.Width = 90;
                dgvBatchGrid.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 50;
                dgvBatchGrid.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "TrateRate";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                dgvBatchGrid.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                dgvBatchGrid.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                dgvBatchGrid.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Closing Stock";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 140;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                dgvBatchGrid.Columns.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                dgvBatchGrid.Columns.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Width = 70;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseVATPer";
                column.DataPropertyName = "PurchaseVATPercent";
                column.Width = 70;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);
                //
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCSTPer";
                column.DataPropertyName = "ProdCSTPercent";
                column.Width = 70;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercent";
                column.DataPropertyName = "ProfitPercent";
                column.Width = 70;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PriceToRetailer";
                column.DataPropertyName = "PriceToRetailer";
                column.Width = 70;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScanCode";
                column.DataPropertyName = "ScanCode";
                column.Width = 120;
                column.Visible = false;
                dgvBatchGrid.Columns.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.Width = 70;
                column.Visible = false;
                dgvBatchGrid.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "PartyID";
                column.Width = 140;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 40;
                column.Visible = false;
                dgvBatchGrid.Columns.Add(column);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructTempGridColumns()
        {
            try
            {
                dgtemp.Columns.Clear();
                DataGridViewTextBoxColumn column;

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 180;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_UnitOfMeasure";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.Visible = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Company";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Co.";
                column.Width = 35;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 88;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Exp";
                column.Width = 45;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "Trade Rate";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VAT";
                column.DataPropertyName = "PurchaseVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 45;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Scheme";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "Scm";
                column.Width = 45;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Replacement";
                column.DataPropertyName = "ReplacementQuantity";
                column.HeaderText = "Repl";
                column.Width = 45;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ItemDiscountPer";
                column.DataPropertyName = "ItemDiscountPercent";
                column.HeaderText = "Disc";
                column.Width = 45;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 80;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ProdVATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT %";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ItemDiscountAmount";
                column.DataPropertyName = "AmountItemDiscount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_SplDiscountPer";
                column.DataPropertyName = "SpecialDiscountPercent";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_SplDiscountAmount";
                column.DataPropertyName = "AmountSpecialDiscount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATAmountPurchase";
                column.DataPropertyName = "ProdVATAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_CSTAmount";
                column.DataPropertyName = "AmountCST";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ItemSCMDiscountAmount";
                column.DataPropertyName = "ItemSCMDiscountAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ItemSCMDiscountAmountPerUnit";
                column.DataPropertyName = "ItemSCMDiscountAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_IfOctroi";
                column.DataPropertyName = "ProdIfOctroi";
                column.Width = 40;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_CreditNoteAmount";
                column.DataPropertyName = "CreditNoteAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_CashDiscountAmount";
                column.DataPropertyName = "CashDiscountAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_CompID";
                column.DataPropertyName = "ProdCompID";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATAmountSale";
                column.DataPropertyName = "ProdVATAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Margin";
                column.DataPropertyName = "Margin";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Margin2";
                column.DataPropertyName = "MarginAfterDiscount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructPurchaseOrder()
        {
            dgPurchaseOrder.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DSLID";
                column.HeaderText = "VouSeries";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPurchaseOrder.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = " ";
                column.Width = 15;
                dgPurchaseOrder.Columns.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSeries";
                column.HeaderText = "Type";
                column.Width = 50;
                column.ReadOnly = true;
                dgPurchaseOrder.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.ReadOnly = true;
                dgPurchaseOrder.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "Number";
                column.ReadOnly = true;
                column.Width = 50;
                dgPurchaseOrder.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "Date";
                column.Width = 80;
                column.ReadOnly = true;
                dgPurchaseOrder.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.Width = 120;
                column.ReadOnly = true;
                dgPurchaseOrder.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Acc";
                column.HeaderText = "a1";
                column.Width = 50;
                column.Visible = false;
                dgPurchaseOrder.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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


        private void ConstructPaymentDetailsColumns()
        {
            dgPaymentDetails.ColumnsMain.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CrdbID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "ID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);

                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurID";
                column.DataPropertyName = "MasterPurchaseID";
                column.HeaderText = "PurID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 80;
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.ReadOnly = true;
                column.Width = 80;
                dgPaymentDetails.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 90;
                column.DefaultCellStyle.Format = "d2";
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
                column.DataPropertyName = "ClearAmount";
                column.HeaderText = "Cleared Amount";
                column.Width = 90;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgPaymentDetails.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CbID";
                column.DataPropertyName = "CBID";
                column.HeaderText = "cID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CrdbpurID";
                column.DataPropertyName = "purchaseID";
                column.HeaderText = "pID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructLastPurchaseColumns()
        {
            dgvLastPurchase.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PurchaseID";
                column.HeaderText = "ID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 70;
                column.ReadOnly = true;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 80;
                column.ReadOnly = true;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 65;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur.Rate";
                column.Width = 65;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.HeaderText = "Dist.Rate";
                column.Width = 65;
                column.ReadOnly = true;
                if (General.ShopDetail.ShopDistributorSale == "Y")
                    column.Visible = true;
                else
                    column.Visible = false;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme";
                column.DataPropertyName = "Scheme";
                column.HeaderText = "SCM";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin";
                column.DataPropertyName = "MarginAfterDiscount";
                column.HeaderText = "Margin%";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Name of party";
                column.Width = 140;
                column.ReadOnly = true;
                dgvLastPurchase.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructBarCodeColumns()
        {
            dgvBarCode.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UnitOfMeasure";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Company";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);



                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfCode";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdClosingStock";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScanCode";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyID";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyName";
                column.Width = 100;
                dgvBarCode.Columns.Add(column);


            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void InitializeMainSubViewControl(string vmode)
        {

            try
            {
                ConstructMainColumns();
                ConstructSubColumns();
                ConstructBatchGrid();
                ConstructLastPurchaseColumns();
                ConstructBarCodeColumns();

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
                mpMSVC.DoubleColumnNames.Add("Col_CSTPer");
                mpMSVC.DoubleColumnNames.Add("Col_VAT");
                mpMSVC.DoubleColumnNames.Add("Col_PurchaseRate");
                mpMSVC.DoubleColumnNames.Add("Col_Amount");
                mpMSVC.DoubleColumnNames.Add("Col_SaleRate");
                mpMSVC.DoubleColumnNames.Add("Col_TradeRate");
                mpMSVC.DoubleColumnNames.Add("Col_Amount");
                //DataTable dt = EcoMartCache.GetProductData();
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

                if (_Mode == OperationMode.Edit) //Amar start
                {
                    foreach (DataGridViewRow row in mpMSVC.dgMainGrid.Rows)
                    {
                        row.Cells["Col_CheckEdit"].Value = "True";
                        row.Cells["Col_CheckEdit"].ReadOnly = true;
                    }
                } //end
                // GotoLastRow();
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void GotoLastRow()
        {
            if (mpMSVC.Rows.Count > 1)
            {
                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    if (dr.Cells[1].Value == null || dr.Cells[1].Value.ToString() == string.Empty)
                        mpMSVC.SetFocus(dr.Index, 1);

                }
            }

        }
        #endregion

        # region fill or clean

        private void BindTempGrid()
        {
            try
            {
                ConstructTempGridColumns();
                DataTable tmptable = new DataTable();
                tmptable = _Purchase.ReadProductDetailsByID();
                _BindingSource = tmptable;
                dgtemp.DataSource = _BindingSource;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void BindPaymentDetails()
        {
            try
            {
                ConstructPaymentDetailsColumns();
                DataTable tmptable = new DataTable();
                tmptable = _Purchase.ReadPaymentDetailsByID();
                _PaymentDetailsBindingSource = tmptable;
                BindPaymentDetails(_PaymentDetailsBindingSource);
                if (_PaymentDetailsBindingSource != null && _PaymentDetailsBindingSource.Rows.Count > 0)
                    btnPaymentHistory.Enabled = true;
                else
                    btnPaymentHistory.Enabled = false;
                //dgPaymentDetails.DataSource = _PaymentDetailsBindingSource;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ClearControls()
        {
            tsBtnPrint.Visible = false;
            dgBills.Visible = false;
            try
            {
                //if (General.CurrentSetting.MsetPurchaseGetPendingScheme == "Y")
                //{
                //    lblpendingscheme.Visible = true;
                //    txtPendingScheme.Visible = true;
                //}
                //else
                //{
                lblpendingscheme.Visible = false;
                txtPendingScheme.Visible = false;
                //}

                if (_Mode == OperationMode.Add)
                {
                    btnDownLoad.Visible = true;
                    btnImport.Visible = true;
                }
                else
                {
                    btnDownLoad.Visible = false;
                    btnImport.Visible = false;
                }
                if (General.CurrentSetting.MsetPurchaseChangeSaleRate == "Y")
                    txtSaleRate.Enabled = true;
                else
                    txtSaleRate.Enabled = false;
                if (General.CurrentSetting.MsetPurchaseIfPTR == "Y")
                {
                    txtPTR.Visible = true;
                    txtProfitPercentage.Visible = false;
                    lblProfitPercent.Visible = false;
                    lblPriceToRetailer.Visible = true;
                }
                else
                {
                    txtPTR.Visible = false;
                    txtProfitPercentage.Visible = true;
                    lblPriceToRetailer.Visible = false;
                    lblProfitPercent.Visible = true;
                }
                pnlSummary.Visible = false;
                pnlGST.Visible = false;
                pnlIGST.Visible = false;
                lblPurchaseBillFormat.Text = string.Empty;
                btnSummary.BackColor = Color.Linen;
                txtVouchernumber.Clear();
                tsBtnSavenPrint.Enabled = false;
                txtExpiredDays.Clear();
                txtExpiry.BackColor = Color.White;
                txtBillNumber.Clear();
                txtVoucherSeries.Text = General.ShopDetail.ShopVoucherSeries;
                datePickerChqDate.ResetText();
                txtChequeNumber.Clear();
                txtNarration.Text = "";
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtQuantity.Text = "0";
                txtPendingScheme.Text = "0";
                txtSchemeQuantity.Text = "0";
                txtReplacement.Text = "0";
                txtBillAmountS.Text = "0.00";
                txtSchemeDiscountS.Text = "0.00";
                txtItemDiscountS.Text = "0.00";
                txtSplDiscountS.Text = "0.00";
                txtSplDiscountPerUnit.Text = "";
                txtSplDiscPerS.Text = "";
                txtAddOnS.Text = "0.00";
                txtCRAmountS.Text = "0.00";
                txtDBAmountS.Text = "0.00";
                txtCashDiscountPerS.Text = "0.00";
                txtCashDiscountAmountS.Text = "0.00";
                txtPreCashDiscountAmountS.Text = "0.00";
                txtViewVat5per.Text = "0.00";
                txtViewVat12point5per.Text = "0.00";
                txtPurchaseRate.Text = "0.00";
                txtPurchaseVATAmt.Text = "0.00";
                txtItemDiscountPer.Text = "0.00";
                txtDiscountAmt.Text = "0.00";
                txtRoundUPS.Text = "0.00";
                txtMasterVATAmt.Text = "0.00";
                txtMasterVATPer.Text = "0.00";
                txtBillAmount.Text = "0.00";
                txtpuramount12point5.Text = "0.00";
                txtpuramount5.Text = "0.00";
                txtpuramount0.Text = "0.00";
                mcbCreditor.SelectedID = "";
                txtChequeNumber.Text = "";
                txtStockID.Text = "";
                mcbBank.SelectedID = "";
                txtPendingBalance.Text = "0.00";
                txtPendingCN.Text = "0.00";
                txtGridAmountTot.Text = "0.00";
                pnlBillDetails.Enabled = true;
                pnlEnterScheme.Visible = false;
                pnlVou.Enabled = true;
                mpMSVC.Rows.Clear();
                psLableWithBorder1.Text = "";
                mpMSVC.Enabled = true;
                dgvLastPurchase.Visible = false;
                lblFooterMessage.Text = "";
                //btnTypeChange.Visible = false;
                //cbNewTransactionType.Visible = false;
                FixVoucherTypeBycbTransactionType();
                cbTransactionType.Focus();
                DataTable dtp = new DataTable();
                if (dgPaymentDetails.Rows.Count > 0)
                {
                    dgPaymentDetails.DataSource = dtp;

                }
                if (General.CurrentSetting.MsetScanBarCode == "Y")
                {
                    if (_Mode == OperationMode.View)
                        btnPrintBarCode.Visible = true;
                    else
                        btnPrintBarCode.Visible = false;
                }
                else
                    btnPrintBarCode.Visible = false;
                _Purchase.VoucherSubType = "1";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillBankCombo()
        {
            mcbBank.SelectedID = null;
            mcbBank.SourceDataString = new string[2] { "AccountID", "AccName" };
            mcbBank.ColumnWidth = new string[2] { "0", "200" };
            mcbBank.ValueColumnNo = 0;
            mcbCreditor.UserControlToShow = new UclAccount();
            Account _Bank = new Account();
            DataTable dbanktable = _Bank.GetSSAccountHoldersList(FixAccounts.AccCodeForBank);
            mcbBank.FillData(dbanktable);
        }
        private void FillTransactionType()
        {
            cbTransactionType.Items.Clear();
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
            //cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
            //if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
            //    cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
        }
        private void InitializeScreen()
        {
            try
            {
                mpMSVC.BringToFront();
                mpMSVC.Visible = true;
                mpMSVC.Dock = DockStyle.Fill;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

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
        private void FillShelfCombo()
        {
            try
            {
                mcbShelf.SelectedID = null;
                mcbShelf.SourceDataString = new string[2] { "ShelfID", "ShelfCode" };
                mcbShelf.ColumnWidth = new string[2] { "0", "200" };
                mcbShelf.ValueColumnNo = 0;
                mcbShelf.UserControlToShow = new UclShelf();
                Shelf _Shelf = new Shelf();
                DataTable dtable = _Shelf.GetOverviewData();
                mcbShelf.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private bool FillLastPurchaseDataFromMasterProduct()
        {
            DataRow dr = null;
            DataRow invdr = null;
            string mbatchno = "";
            string mprodno = "";
            string mmrp = "";
            string mshelfcode = "";
            string mshelfID = "";
            int mprodclosingstock = 0;
            string mexpirydate = "";
            string mexpiry = "";
            double mmrpn = 0.00;
            double mpurrate = 0.00;
            double mtraderate = 0.00;
            double msalerate = 0.00;
            double mcstper = 0.00;
            double mcstamt = 0.00;
            double mscmamt = 0.00;
            double mscmper = 0.00;
            double mpurvatper = 0.00;
            double mpurvatamt = 0.00;
            double mprodvatper = 0.00;
            double mprodvatamt = 0.00;
            double mitemdiscper = 0.00;
            double mitemdiscamt = 0.00;
            string mlastpurchasestockid = "";
            double mdistrateper = 0;
            double mdistrateamt = 0;

            try
            {
                Product drprod = new Product();
                dr = drprod.ReadLastPurchaseByID(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString());
                mprodno = mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString();
                if (dr["ProdLastPurchaseStockID"] != DBNull.Value)
                    mlastpurchasestockid = dr["ProdLastPurchaseStockID"].ToString().Trim();
                if (dr["ProdLastPurchaseBatchNumber"] != DBNull.Value)
                    mbatchno = dr["ProdLastPurchaseBatchNumber"].ToString().Trim();
                if (dr["ProdLastPurchaseMRP"] != DBNull.Value)
                {
                    double.TryParse(dr["ProdLastPurchaseMRP"].ToString(), out mmrpn);
                    mmrp = dr["ProdLastPurchaseMRP"].ToString();
                    _Purchase.MRP = mmrpn;
                }
                if (dr["ProdClosingStock"] != DBNull.Value)
                    mprodclosingstock = Convert.ToInt32(dr["ProdClosingStock"].ToString().Trim());
                if (dr["ShelfCode"] != DBNull.Value)
                    mshelfcode = (dr["ShelfCode"].ToString().Trim());
                if (dr["ProdShelfID"] != DBNull.Value)
                    mshelfID = dr["ProdShelfID"].ToString().Trim();
                if (dr["ProdLastPurchaseExpiry"] != DBNull.Value)
                    mexpiry = dr["ProdLastPurchaseExpiry"].ToString().Trim();
                if (dr["ProdLastPurchaseExpiryDate"] != DBNull.Value)
                    mexpirydate = dr["ProdLastPurchaseExpiryDate"].ToString().Trim();
                if (dr["ProdLastPurchaseSaleRate"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseSaleRate"].ToString(), out msalerate);
                if (dr["ProdLastPurchaseTradeRate"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseTradeRate"].ToString(), out mtraderate);
                if (dr["ProdLastPurchaseRate"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseRate"].ToString(), out mpurrate);
                if (dr["ProdLastPurchaseCST"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseCST"].ToString(), out mcstamt);
                if (dr["ProdLastPurchaseCSTPer"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseCSTPer"].ToString(), out mcstper);
                if (dr["ProdLastPurchaseSCMPer"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseSCMPer"].ToString(), out mscmper);
                if (dr["ProdLastPurchaseSCM"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseSCM"].ToString(), out mscmamt);
                if (dr["ProdLastPurchaseVATPer"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseVATPer"].ToString(), out mpurvatper);
                if (dr["ProdVATPercent"] != DBNull.Value)
                    double.TryParse(dr["ProdVATPercent"].ToString(), out mprodvatper);

                //if (dr["ProdLastPurchaseDistributorSaleRatePer"] != DBNull.Value)
                //    double.TryParse(dr["ProdLastPurchaseDistributorSaleRatePer"].ToString(), out mdistrateper);
                //if (dr["ProdLastPurchaseDistributorSaleRate"] != DBNull.Value)
                //    double.TryParse(dr["ProdLastPurchaseDistributorSaleRate"].ToString(), out mdistrateamt);

                mpurvatper = mprodvatper;
                mpurvatamt = Math.Round((mtraderate * mpurvatper) / 100, 2);
                mprodvatamt = Math.Round((msalerate * mprodvatper) / 100, 2);

                double mdistrate = Math.Round((mtraderate * mdistrateper / 100), 2);
                if (mdistrateamt == 0)
                    mdistrateamt = Math.Round(mtraderate + mdistrate, 2);

                if (dr["ProdLastPurchaseItemDiscPer"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseItemDiscPer"].ToString(), out mitemdiscper);
                if (mitemdiscper > 0)
                    mitemdiscamt = Math.Round((mtraderate * mitemdiscper) / 100, 4);

                if (dr["ProdLastPurchaseStockID"] != DBNull.Value)
                {
                    _LastStockID = dr["ProdLastPurchaseStockID"].ToString();
                    txtStockID.Text = dr["ProdLastPurchaseStockID"].ToString();
                }

                if (mexpiry != "")
                    mexpiry = General.GetValidExpiry(mexpiry);
                else
                    mexpiry = "00/00";

                if (mexpiry != "O")
                    txtExpiry.Text = mexpiry;
                else
                {
                    mexpiry = "";
                    txtExpiry.Text = "";
                }

                if (mexpiry != string.Empty)
                {
                    mexpiry = General.GetValidExpiryDate(mexpiry);
                    txtExpiryDate.Text = General.GetDateInShortDateFormat(mexpirydate);
                }
                txtTradeRate.Text = mtraderate.ToString("#0.00");
                txtItemDiscountPer.Text = Convert.ToString(mitemdiscper.ToString("#0.00")).Trim();
                txtDiscountAmt.Text = Convert.ToString(mitemdiscamt.ToString("#0.0000")).Trim();
                txtPurchaseRate.Text = Convert.ToString(mpurrate.ToString("#0.00")).Trim();
                txtMRP.Text = Convert.ToString(mmrpn.ToString("#0.00")).Trim();
                txtSaleRate.Text = Convert.ToString(msalerate.ToString("#0.00")).Trim();
                txtMasterVATPer.Text = mprodvatper.ToString("#0.00");
                txtMasterVATAmt.Text = mprodvatamt.ToString("#0.0000");
                //txtDistSaleRate.Text = mdistrateamt.ToString("#0.00");
                //txtDistRatePercent.Text = mdistrateper.ToString("#0.00");
                if (mpurvatper == 0)
                {
                    mpurvatper = mprodvatper;
                    mpurvatamt = mprodvatamt;
                }
                txtPurchaseVATPer.Text = Convert.ToString(mpurvatper.ToString("#0.00")).Trim();
                txtPurchaseVATAmt.Text = Convert.ToString(mpurvatamt.ToString("#0.0000")).Trim();

                mcbShelf.SelectedID = mshelfID;
                SsStock invss = new SsStock();
                invdr = invss.GetStockByStockID(mlastpurchasestockid);
                if (invdr != null)
                {

                    mexpiry = invdr["Expiry"].ToString().Trim();
                    mexpirydate = invdr["ExpiryDate"].ToString().Trim();
                    double.TryParse(invdr["MRP"].ToString().Trim(), out mmrpn);
                    double.TryParse(invdr["SaleRate"].ToString().Trim(), out msalerate);
                    double.TryParse(invdr["PurchaseRate"].ToString().Trim(), out mpurrate);
                    double.TryParse(invdr["TradeRate"].ToString().Trim(), out mtraderate);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        private bool FillDataFromMPSVRow()
        {
            DataRow invdr = null;
            string mbatchno = "";
            string mmrp = "";
            string mshelfcode = "";
            string mshelfID = "";
            int mqty = 0;
            int mrepl = 0;
            int mscm = 0;
            string mexpirydate = "";
            string mexpiry = "";
            double mmrpn = 0;
            double mpurrate = 0;
            double mtraderate = 0;
            double msalerate = 0;
            double mcstper = 0;
            double mcstamt = 0;
            double mscmamt = 0;
            double mscmper = 0;
            double mpurvatper = 0;
            double mpurvatamt = 0;
            double mprodvatper = 0;
            double mprodvatamt = 0;
            double mitemdiscper = 0;
            double mitemdiscamt = 0;
            double mamt = 0;
            string mstockid = "";
            //   double mdiscdiscper = 0;
            //     double mdiscdiscamt = 0;
            double mpricetoretailer = 0;
            double mprofitpercent = 0;
            int mpendingschemequantity = 0;
            try
            {
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfCode"].Value != null)
                    mshelfcode = mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfCode"].Value.ToString().Trim();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value != null)
                    mshelfID = mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value.ToString().Trim();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    mqty = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                //if (mpMSVC.MainDataGridCurrentRow.Cells["Col_PendingSchemeQuantity"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_PendingSchemeQuantity"].Value.ToString() != "")
                //    mpendingschemequantity = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_PendingSchemeQuantity"].Value.ToString());
                //else
                mpendingschemequantity = 0;

                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value != null)
                    mscm = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Replacement"].Value != null)
                    mrepl = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Replacement"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                    mbatchno = mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                {
                    mmrpn = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
                    mmrp = mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString();
                }
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null)
                    mexpiry = mpMSVC.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value != null)
                    mexpirydate = mpMSVC.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value.ToString();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_PurchaseRate"].Value != null)
                    mpurrate = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_PurchaseRate"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value != null)
                    mtraderate = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value != null)
                    msalerate = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value.ToString());
                //if (mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value != null)
                //    mcstamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value.ToString());
                //if (mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value.ToString() != "")
                //    mcstper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmountPerUnit"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmountPerUnit"].Value.ToString() != "")
                    mscmper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmountPerUnit"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmount"].Value != null)
                    mscmamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_VAT"].Value != null)
                    mpurvatper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_VAT"].Value.ToString());



                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value != null)
                    mprodvatper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value != null)
                    mitemdiscper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_PriceToRetailer"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_PriceToRetailer"].Value.ToString() != string.Empty)
                    mpricetoretailer = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_PriceToRetailer"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProfitPercent"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_ProfitPercent"].Value.ToString() != string.Empty)
                    mprofitpercent = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ProfitPercent"].Value.ToString());
                //double mdistrate = mdiscdiscamt;
                //mdiscdiscamt  = Math.Round(mtraderate + mdistrate, 2);
                //  mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRate"].Value = mdiscdiscamt.ToString();

                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                {
                    mstockid = mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString();
                    _LastStockID = mstockid;
                }
                if (mcstper == 0)
                    mpurvatper = mprodvatper;
                mpurvatamt = Math.Round((mmrpn * mpurvatper) / 100, 2); //4
                mprodvatamt = Math.Round((mmrpn * mprodvatper) / 100, 2); //4
                mitemdiscamt = Math.Round((mtraderate * mitemdiscper) / 100, 4); //4
                mamt = Math.Round((mtraderate * mqty), 2);

                txtQuantity.Text = mqty.ToString("#0");
                txtPendingScheme.Text = mpendingschemequantity.ToString("#0");
                txtReplacement.Text = mrepl.ToString("#0");
                txtSchemeQuantity.Text = mscm.ToString("#0");
                txtAmount.Text = mamt.ToString("#0.00");
                txtBatch.Text = mbatchno;
                txtExpiry.Text = mexpiry;
                txtExpiryDate.Text = General.GetDateInShortDateFormat(mexpirydate);
                txtTradeRate.Text = mtraderate.ToString("#0.00");
                txtItemDiscountPer.Text = mitemdiscper.ToString("#0.00");
                txtDiscountAmt.Text = mitemdiscamt.ToString("#0.0000");
                txtPurchaseVATPer.Text = mpurvatper.ToString("#0.00");
                txtPurchaseVATAmt.Text = mpurvatamt.ToString("#0.0000");
                txtPurchaseRate.Text = mpurrate.ToString("#0.00");
                txtMRP.Text = mmrpn.ToString("#0.00");
                txtSaleRate.Text = msalerate.ToString("#0.00");
                txtMasterVATPer.Text = mprodvatper.ToString("#0.00");
                txtMasterVATAmt.Text = mprodvatamt.ToString("#0.00");
                txtCSTPer.Text = mcstper.ToString("#0.00");
                txtCSTAmount.Text = mcstamt.ToString("#0.00");
                mcbShelf.SelectedID = mshelfID;
                txtSchemeAmount.Text = mscmamt.ToString("#0.00");
                txtSchemePer.Text = mscmper.ToString("#0.00");
                txtStockID.Text = mstockid;
                txtProfitPercentage.Text = mprofitpercent.ToString("#0.00");
                txtPTR.Text = mpricetoretailer.ToString("#0.00");
                //txtDistRatePercent.Text = mdiscdiscper.ToString("#0.00");
                //txtDistSaleRate.Text = mdiscdiscamt.ToString("#0.00");

                SsStock invss = new SsStock();
                invdr = invss.GetStockByProductIDAndBatchNumberAndMRP(_Purchase.ProductID, mbatchno, mmrp);

                if (invdr != null)
                {
                    mexpiry = invdr["Expiry"].ToString().Trim();
                    mexpirydate = invdr["ExpiryDate"].ToString().Trim();
                    double.TryParse(invdr["MRP"].ToString().Trim(), out mmrpn);
                    double.TryParse(invdr["SaleRate"].ToString().Trim(), out msalerate);
                    double.TryParse(invdr["PurchaseRate"].ToString().Trim(), out mpurrate);
                    double.TryParse(invdr["TradeRate"].ToString().Trim(), out mtraderate);
                }
                IfEditPreviousRow = "N";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        private void FillBatchGrid()
        {
            DataTable dt = new DataTable();
            SsStock invss = new SsStock();
            try
            {
                //if (_Mode == OperationMode.Add)
                //    dt = invss.GetStockByProductIDForPurchase(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString(), 1);
                //else
                dt = invss.GetStockByProductIDForPurchase(Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString()), 0);
                dgvBatchGrid.DataSource = dt;
                if (dgvBatchGrid.Rows.Count > 0 && _LastStockID != string.Empty)
                {
                    foreach (DataGridViewRow row in dgvBatchGrid.Rows)
                    {
                        if (row.Cells["Col_StockID"].Value.ToString() == _LastStockID)
                        {
                            row.Selected = true;
                            dgvBatchGrid.CurrentCell = dgvBatchGrid.Rows[row.Index].Cells["Col_Batchno"];
                            break;
                        }
                    }
                }
                // ---- [ansuman 23.02.2017]
                DataRow[] minRow = dt.Select("TradeRate = MIN(TradeRate)");
                if (minRow.Length > 0)
                {
                    foreach (DataRow dr in minRow)
                    {
                        MinTradeRate = Convert.ToDouble(dr["TradeRate"]);
                        lblLowestRate.Text = dr["TradeRate"].ToString();
                        lblSupplierName.Text = dr["AccName"].ToString();
                        lblMRPVal.Text = dr["MRP"].ToString();
                        MinMRP = Convert.ToDouble(dr["MRP"]);
                    }
                }
                else
                {
                    lblLowestRate.Text = "----";
                    lblSupplierName.Text = "----";
                    lblMRPVal.Text = "----";
                    MinTradeRate = 0;
                    MinMRP = 0;
                }
                // ----
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.FillBatchGrid>>" + Ex.Message);
            }
        }
        private void ClearpnlProductDetail()
        {
            try
            {
                txtQuantity.Text = "0";
                txtSchemeQuantity.Text = "0";
                txtReplacement.Text = "0";
                txtBatch.Text = "";
                txtExpiry.Text = "";
                txtMRP.Text = "0.00";
                txtTradeRate.Text = "0.00";
                txtPurchaseVATAmt.Text = "0.00";
                txtPurchaseVATPer.Text = "0.00";
                txtDiscountAmt.Text = "0.00";
                txtItemDiscountPer.Text = "0.00";
                txtExpiryDate.Text = "";
                txtCSTAmount.Text = "0.00";
                txtCSTPer.Text = "0.00";
                txtPurchaseRate.Text = "0.00";
                txtSaleRate.Text = "0.00";
                txtAmount.Text = "0.00";
                txtScanCode.Text = "";
                mcbShelf.SelectedID = "";
                txtSchemeAmount.Text = "0.00";
                txtSchemePer.Text = "0.00";
                txtMasterVATAmt.Text = "0.00";
                txtMasterVATPer.Text = "0.00";
                txtCashDisountPerUnit.Text = "0.00";
                txtSplDiscountPerUnit.Text = "0.00";
                lblLowestRate.Text = "0.00";
                lblMRPVal.Text = "0.00";
                lblSupplierName.Text = "";
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.ClearpnlProductDetail>>" + Ex.Message);
            }
        }
        private DataTable FillPurchaseOrder()
        {
            bool retValue = false;
            DataTable dt = new DataTable();
            try
            {
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    ConstructPurchaseOrder();
                    PurchaseOrder puro = new PurchaseOrder();

                    dt = puro.GetListforPurchase(mcbCreditor.SelectedID);
                    if (dt != null)
                        retValue = BindPurchaseOrder(dt);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.FillCreditDebitNote>>" + Ex.Message);
            }
            return dt;

        }

        private bool BindPurchaseOrder(DataTable dt)
        {
            bool retValue = true;
            try
            {

                if (dgPurchaseOrder != null)
                    dgPurchaseOrder.Rows.Clear();
                int _RowIndex = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    _RowIndex = dgPurchaseOrder.Rows.Add();
                    string voudt = "";
                    DataGridViewRow currentdr = dgPurchaseOrder.Rows[_RowIndex];
                    currentdr.Cells["Col_DSLID"].Value = dr["ID"].ToString();
                    currentdr.Cells["Col_VoucherSeries"].Value = dr["VoucherSeries"].ToString();
                    currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    {
                        voudt = dr["VoucherDate"].ToString();
                        voudt = General.GetDateInShortDateFormat(voudt);
                    }
                    currentdr.Cells["Col_VoucherDate"].Value = voudt;
                    currentdr.Cells["Col_Amount"].Value = dr["Amount"].ToString();
                    currentdr.Cells["Col_Check"].Value = string.Empty; //currentdr.Cells["Col_Check"].Value = ((char)0x221A).ToString();
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
                Log.WriteError("UclPurchase.FillCreditDebitNote>>" + Ex.Message);
            }
            return dt;
        }

        //private DataTable FillTempPurchase()
        //{
        //    bool retValue = false;
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        ConstructTempPurchaseColumns();

        //        Purchase crdb = new Purchase();

        //        dt = crdb.GetOverviewDataForTempPurchase();
        //        if (dt != null)
        //        {

        //            retValue = BindTempPurchase(dt);
        //            pnlTempPurchase.BringToFront();
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("UclPurchase.FillCreditDebitNote>>" + Ex.Message);
        //    }
        //    return dt;
        //}

        private DataTable FillLastPurchase()
        {
            bool retValue = false;
            DataTable dt = new DataTable();
            try
            {
                ConstructLastPurchaseColumns();
                Purchase lastpur = new Purchase();
                dt = lastpur.GetOverviewDataForLastPurchase(_Purchase.ProductID);
                if (dt != null && dt.Rows.Count > 0)
                    retValue = BindLastPurchase(dt);

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.FillCreditDebitNote>>" + Ex.Message);
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
                    currentdr.Cells["Col_CrdbID"].Value = dr["ID"].ToString();
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



        private bool BindPaymentDetails(DataTable dt)
        {
            bool retValue = true;
            try
            {

                if (dgPaymentDetails != null)
                    dgPaymentDetails.Rows.Clear();
                int _RowIndex = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    _RowIndex = dgPaymentDetails.Rows.Add();
                    string voudt = "";
                    DataGridViewRow currentdr = dgPaymentDetails.Rows[_RowIndex];
                    currentdr.Cells["Col_CrdbID"].Value = dr["MasterID"].ToString();
                    currentdr.Cells["Col_PurID"].Value = dr["MasterPurchaseID"].ToString();
                    currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    {
                        voudt = dr["VoucherDate"].ToString();
                        voudt = General.GetDateInShortDateFormat(voudt);
                    }
                    currentdr.Cells["Col_VoucherDate"].Value = voudt;
                    double clamt = Convert.ToDouble(dr["ClearAmount"].ToString());
                    currentdr.Cells["Col_AmountNet"].Value = clamt.ToString("0.00");
                    currentdr.Cells["Col_CbID"].Value = dr["CBID"].ToString();
                    currentdr.Cells["Col_CrdbpurID"].Value = dr["purchaseID"].ToString();
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
        private bool BindLastPurchase(DataTable dt)
        {
            bool retValue = true;
            try
            {

                if (dgvLastPurchase != null)
                    dgvLastPurchase.Rows.Clear();
                int _RowIndex = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    _RowIndex = dgvLastPurchase.Rows.Add();
                    string voudt = "";
                    double amtclear = 0;
                    double mmargin = 0;
                    int mqty = 0;
                    int mscm = 0;
                    DataGridViewRow currentdr = dgvLastPurchase.Rows[_RowIndex];
                    currentdr.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                    if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    {
                        voudt = dr["VoucherDate"].ToString();
                        voudt = General.GetDateInShortDateFormat(voudt);
                    }
                    currentdr.Cells["Col_VoucherDate"].Value = voudt;

                    currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();
                    amtclear = 0;
                    if (dr["MRP"] != DBNull.Value)
                        double.TryParse(dr["MRP"].ToString(), out amtclear);
                    currentdr.Cells["Col_MRP"].Value = amtclear.ToString("#0.00");
                    amtclear = 0;
                    if (dr["PurchaseRate"] != DBNull.Value)
                        double.TryParse(dr["PurchaseRate"].ToString(), out amtclear);
                    currentdr.Cells["Col_PurchaseRate"].Value = amtclear.ToString("#0.00");

                    amtclear = 0;
                    //if (dr["DistributorSaleRate"] != DBNull.Value)
                    //    double.TryParse(dr["DistributorSaleRate"].ToString(), out amtclear);
                    currentdr.Cells["Col_DistributorRate"].Value = amtclear.ToString("#0.00");

                    mqty = 0;
                    mscm = 0;
                    if (dr["Quantity"] != DBNull.Value)
                        int.TryParse(dr["Quantity"].ToString(), out mqty);
                    if (dr["SchemeQuantity"] != DBNull.Value)
                        int.TryParse(dr["SchemeQuantity"].ToString(), out mscm);
                    string scm = mqty.ToString() + "+" + mscm.ToString();
                    currentdr.Cells["Col_Scheme"].Value = scm;
                    if (dr["MarginAfterDiscount"] != DBNull.Value)
                        double.TryParse(dr["MarginAfterDiscount"].ToString(), out mmargin);
                    currentdr.Cells["Col_Margin"].Value = mmargin.ToString("#0.00");
                    currentdr.Cells["Col_PartyName"].Value = dr["AccName"].ToString();
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



        #endregion

        #region keydown-Click-DoubleClick

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
                {
                    int mqty = 0;
                    int.TryParse(txtQuantity.Text.ToString(), out mqty);
                    txtSchemeQuantity.Focus();
                }
                else if (e.KeyCode == Keys.Menu)
                {
                    DOProductEdit();
                }

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtQuantity_KeyDown>>" + Ex.Message);
            }
        }

        private void dgvBatchGrid_KeyDown(object sender, KeyEventArgs e)
        {
            double mqty = 0;
            try
            {
                double.TryParse(txtQuantity.Text.ToString(), out mqty);
                if (e.KeyCode == Keys.Escape)
                {
                    dgvBatchGrid.Visible = false;
                    pnlProductDetail.BringToFront();
                    pnlProductDetail.Enabled = true;
                    pnlProductDetail.Visible = true;
                    mpMSVC.Enabled = true;
                    if (txtQuantity.Text == null || txtQuantity.Text.ToString() == "")
                        txtQuantity.Focus();
                    else
                        txtBatch.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    dgvBatchGridClick();
                    pnlProductDetail.Enabled = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.dgvBatchGrid_KeyDown>>" + Ex.Message);
            }

            lblFooterMessage.Text = "";

        }
        private void dgvBatchGrid_DoubleClick(object sender, EventArgs e)
        {
            dgvBatchGridClick();
        }

        private void dgvBatchGridClick()
        {
            double mtraderate = 0;
            double mpurvatper = 0;
            double mcstper = 0;
            double mmstper = 0;
            double mqty = 0;
            double mmrp = 0;
            double msalerate = 0;
            try
            {
                double.TryParse(txtQuantity.Text.ToString(), out mqty);
                dgvBatchGrid.Visible = false;
                dgvBatchGrid.SendToBack();
                pnlProductDetail.BringToFront();
                pnlProductDetail.Enabled = true;
                mpMSVC.Enabled = true;
                if (dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value != null)
                    txtBatch.Text = dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value.ToString();
                if (txtBatch.Text.ToString() == "NEW")
                {
                    txtBatch.Text = string.Empty;
                }
                else
                {
                    if (dgvBatchGrid.CurrentRow.Cells["Col_StockID"].Value != null)
                        txtStockID.Text = dgvBatchGrid.CurrentRow.Cells["Col_StockID"].Value.ToString();
                    if (dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value != null)
                        txtBatch.Text = dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value.ToString();
                    if (dgvBatchGrid.CurrentRow.Cells["Col_Expiry"].Value != null)
                        txtExpiry.Text = dgvBatchGrid.CurrentRow.Cells["Col_Expiry"].Value.ToString();
                    if (dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value != null)
                        txtSaleRate.Text = dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value.ToString();

                    if (dgvBatchGrid.CurrentRow.Cells["Col_MRP"].Value != null)
                    {
                        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
                    }
                    txtMRP.Text = mmrp.ToString("#0.00");
                    if (dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value != null)
                    {

                        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                        txtTradeRate.Text = mtraderate.ToString("#0.00");
                    }
                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value != null)
                    {

                        double.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value.ToString(), out mmstper);
                        txtMasterVATPer.Text = mmstper.ToString("#0.00");
                        txtMasterVATAmt.Text = Math.Round(msalerate * mmstper / 100, 2).ToString("#0.00");
                    }
                    else
                    {
                        txtMasterVATPer.Text = "0.00";
                        txtMasterVATAmt.Text = Math.Round(msalerate * mmstper / 100, 2).ToString("#0.00");
                    }
                    if (dgvBatchGrid.CurrentRow.Cells["Col_PurchaseVATPer"].Value != null)
                    {
                        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_PurchaseVATPer"].Value.ToString(), out mpurvatper);
                        if (mpurvatper == 0)
                            txtPurchaseVATPer.Text = mmstper.ToString("#0.00");
                        else
                            txtPurchaseVATPer.Text = mpurvatper.ToString("#0.00");
                        mpurvatper = Convert.ToDouble(txtPurchaseVATPer.Text.ToString());
                        txtPurchaseVATAmt.Text = Math.Round(mtraderate * mpurvatper / 100, 2).ToString("#0.0000"); //4
                    }
                    if (dgvBatchGrid.CurrentRow.Cells["Col_PurchaseRate"].Value != null)
                        txtPurchaseRate.Text = dgvBatchGrid.CurrentRow.Cells["Col_PurchaseRate"].Value.ToString();
                    if (dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value != null)
                        txtSaleRate.Text = dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value.ToString();
                    if (dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value != null)
                    {
                        txtCSTPer.Text = dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value.ToString();
                        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value.ToString(), out mcstper);
                        txtCSTAmount.Text = Math.Round(mtraderate * mcstper / 100, 2).ToString("#0.00");
                    }
                    if (dgvBatchGrid.CurrentRow.Cells["Col_ScanCode"].Value != null)
                        txtScanCode.Text = dgvBatchGrid.CurrentRow.Cells["Col_ScanCode"].Value.ToString();
                    pnlProductDetail.Enabled = true;
                    txtAmount.Text = Math.Round(mtraderate * mqty).ToString("#0.00");
                }
                txtBatch.Focus();
                CalculatePurRateSaleRateAndAmount();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.dgvBatchGridClick>>" + Ex.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancelClick();

            ClearStockData();
            _purchaseMode = PurchaseMode.Add;

            mcbCompany1.SelectedID = "";
            mcbGenCatOpStock.SelectedID = "";
            mcbProductCategory1.SelectedID = "";
            mcbShelfNoOpStock.SelectedID = "";
            mcbSchedule1.SelectedItem = "";
        }
        private void btnCancel_KeyDown(object sender, KeyEventArgs e)
        {
            btnCancelClick();
        }

        public void btnCancelClick()
        {
            lblExpired.Text = "";
            lblFooterMessage.Text = "";
            double mamt = 0;
            btnOK.Enabled = true;
            btnSummary.Enabled = true;
            btnCancel.BackColor = Color.White;
            txtMRP.BackColor = Color.White;
            try
            {
                ClearStockData();
                _purchaseMode = PurchaseMode.Add;

                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null)
                    double.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString(), out mamt);
                mpMSVC.Enabled = true;
                pnlProductDetail.Enabled = true;
                pnlProductDetail.SendToBack();
                pnlProductDetail.Visible = false;
                pnlEditProduct.SendToBack();
                pnlEditProduct.Visible = false;
                dgvBatchGrid.Visible = false;
                dgvLastPurchase.Visible = false;
                ClearpnlProductDetail();
                pnlBillDetails.Enabled = true;
                timer.Stop();
                SetColorToLowestRate();
                SetColorToLowestRate();
                if (mamt == 0)
                {
                    bool SchemeQuantFlag = false;

                    if (string.IsNullOrEmpty(Convert.ToString(mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value)) == false
                        && Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value) > 0)
                        SchemeQuantFlag = true;
                    if (SchemeQuantFlag == false)
                    {
                        mpMSVC.Rows.Remove(mpMSVC.MainDataGridCurrentRow);
                        int curro = mpMSVC.Rows.Add();
                        mpMSVC.Focus();
                        mpMSVC.SetFocus(curro, 1);
                    }
                    else
                    {
                        mpMSVC.Focus();
                        mpMSVC.SetFocus(1);
                    }
                }
                else
                {
                    mpMSVC.Focus();
                    mpMSVC.SetFocus(1);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCancel_KeyDown>>" + Ex.Message);
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            bool retvalue = ButtonOKClick();
            if (retvalue)
            {
                ClearStockData();  // [ansuman] [08.11.2016]
            }
        }
        private bool ValidationForOK()
        {
            bool retValue = false;
            lblFooterMessage.Text = "";
            double mamt = 0;
            int mqty = 0;
            int mscm = 0;
            int mrepl = 0;
            double mmrp = 0;
            double mtrate = 0;
            double mprate = 0;
            double msalerate = 0;
            double mvatamt = 0;
            string mbatch = "";
            string mexp = "";
            string mexpdate = "";
            double mdiscamt = 0;
            double mvatper = 0;
            double mcstper = 0;
            try
            {
                mamt = Convert.ToDouble(txtAmount.Text.ToString());
                mqty = Convert.ToInt32(txtQuantity.Text.ToString());
                mscm = Convert.ToInt32(txtSchemeQuantity.Text.ToString());
                mrepl = Convert.ToInt32(txtReplacement.Text.ToString());
                mmrp = Convert.ToDouble(txtMRP.Text.ToString());
                mtrate = Convert.ToDouble(txtTradeRate.Text.ToString());
                mdiscamt = Convert.ToDouble(txtDiscountAmt.Text.ToString());
                msalerate = Convert.ToDouble(txtSaleRate.Text.ToString());
                mcstper = Convert.ToDouble(txtCSTPer.Text.ToString());
                mvatper = Convert.ToDouble(txtPurchaseVATPer.Text.ToString());
                mvatamt = Convert.ToDouble(txtPurchaseVATAmt.Text.ToString());
                mbatch = txtBatch.Text.ToString();
                mexp = txtExpiry.Text;
                mexpdate = General.GetValidExpiryDate(mexp);
                mprate = Convert.ToDouble(txtPurchaseRate.Text.ToString());
                if (mcstper > 0)
                {
                    mvatper = 0;
                    mvatamt = 0;
                    txtPurchaseVATPer.Text = "0.00";
                    txtPurchaseVATAmt.Text = "0.00";
                }
                if ((mqty + mscm + mrepl) <= 0)
                {
                    retValue = false;
                    lblFooterMessage.Text = "Enter Quantity or Scheme Quantity";
                    txtQuantity.Focus();
                }
                else if (mprate == 0)
                {
                    retValue = false;
                    lblFooterMessage.Text = "PurchaseRate should be > 0";
                    txtPurchaseRate.Focus();
                }
                else if (mtrate > mmrp)
                {
                    retValue = false;
                    lblFooterMessage.Text = "Trade Rate should be < MRP";
                    txtTradeRate.Focus();
                }
                else
                {

                    if (mmrp > 0 && mtrate > 0)
                    {
                        if ((mqty > 0 && mamt > 0))
                        {
                            if (mamt > 0 || (mamt == 0 && (mscm > 0 || mrepl > 0)))
                            {
                                if (mmrp >= mtrate)
                                {
                                    if (msalerate >= (mtrate - mdiscamt))
                                    {
                                        retValue = CheckValidExpiry();
                                        if (retValue)
                                        {

                                            if (mbatch != "")
                                            {
                                                retValue = true;
                                                lblFooterMessage.Text = "";
                                            }
                                            else
                                            {
                                                retValue = false;
                                                lblFooterMessage.Text = "Batch Cannot be Blank";
                                                txtBatch.Focus();
                                            }
                                        }
                                        else
                                        {
                                            retValue = false;
                                            lblFooterMessage.Text = "Please Check Expiry Date";
                                            txtExpiry.Focus();
                                        }

                                    }
                                    else
                                    {
                                        retValue = false;
                                        lblFooterMessage.Text = "Sale Rate > Trade Rate";
                                        txtSaleRate.Focus();
                                    }
                                }
                                else
                                {
                                    retValue = false;
                                    lblFooterMessage.Text = "Mrp > Trade Rate";
                                    txtTradeRate.Focus();

                                }
                            }
                            else
                            {
                                retValue = false;
                                lblFooterMessage.Text = "Please Check Quantity,Scheme,Replacement";
                                txtQuantity.Focus();
                            }
                        }
                        else
                        {
                            if (mqty == 0 && mscm == 0)
                            {
                                retValue = false;
                                lblFooterMessage.Text = "Please Check Quantity";
                                txtQuantity.Focus();
                            }
                            else
                                retValue = true;
                        }

                    }
                    int mvd = Convert.ToInt32(datePickerBillDate.Value.ToString("yyyyMMdd"));
                    if (retValue)
                    {
                        if (mvatper != 0 && mvatper != 5 && mvatper != 12 && mvatper != 18 && mvatper != 28)
                        {
                            lblFooterMessage.Text = "Please Check GST Percent";
                            retValue = false;
                            tsBtnSave.Enabled = false;
                        }
                        else
                        {
                            lblFooterMessage.Text = "";
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.ValidationForOK>>" + Ex.Message);
            }
            return retValue;

        }

        private bool ButtonOKClick()
        {
            bool result = false;
            bool ifok = ValidationForOK();
            //  lblFooterMessage.Text = "";
            lblExpired.Text = "";
            pnlBillDetails.Enabled = true;
            txtMRP.BackColor = Color.White;
            string ScannedBarcode = txtScanCode.Text;
            timer.Stop();
            SetColorToLowestRate();
            try
            {
                if (ifok)
                {
                    CalculatePurRateSaleRateAndAmount();
                    pnlEditProduct.SendToBack();
                    pnlEditProduct.Visible = false;
                    pnlProductDetail.SendToBack();
                    pnlProductDetail.Visible = false;
                    dgvLastPurchase.Visible = false;
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = pobj.Id;
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = txtProdName.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].ReadOnly = true;
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Company"].Value = txtCompShortName1.Text;

                    //      mpMSVC.MainDataGridCurrentRow.Cells["Col_UnitOfMeasure"].Value = txtUOM.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Pack"].Value = txtPack1.Text.ToString();

                    if (string.IsNullOrEmpty(txtQuantity.Text.ToString()) == true)
                        txtQuantity.Text = "0";
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = txtQuantity.Text.ToString();
                    //mpMSVC.MainDataGridCurrentRow.Cells["Col_PendingSchemeQuantity"].Value = txtPendingScheme.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = txtBatch.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = txtExpiry.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = txtExpiryDate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value = txtTradeRate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value = txtMRP.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_PurchaseRate"].Value = txtPurchaseRate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = txtSaleRate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_VAT"].Value = txtPurchaseVATPer.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_VATAmountPurchase"].Value = txtPurchaseVATAmt.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value = txtSchemeQuantity.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Replacement"].Value = txtReplacement.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value = txtItemDiscountPer.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value = txtAmount.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountAmount"].Value = txtDiscountAmt.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmount"].Value = txtSchemeAmount.Text.ToString();
                    //mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value = txtCSTPer.Text.ToString();
                    //mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value = txtCSTAmount.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value = txtMasterVATPer.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_VATAmountSale"].Value = txtMasterVATAmt.Text.ToString();
                    //mpMSVC.MainDataGridCurrentRow.Cells["Col_SpldiscountPer"].Value = txtSplDiscPerS.Text.ToString();
                    //mpMSVC.MainDataGridCurrentRow.Cells["Col_SplDiscountAmount"].Value = txtSplDiscountPerUnit.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_CashDiscountAmount"].Value = txtCashDisountPerUnit.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_PriceToRetailer"].Value = txtPTR.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProfitPercent"].Value = txtProfitPercentage.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Margin"].Value = txtMargin.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Margin2"].Value = txtMargin2.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value = txtStockID.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ScanCode"].Value = txtScanCode.Text.ToString();
                    if (mcbShelf.SelectedID != null && mcbShelf.SelectedID != string.Empty)
                        mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value = mcbShelf.SelectedID;

                    ClearpnlProductDetail();
                    mpMSVC.Enabled = true;
                    int pp = mpMSVC.MainDataGridCurrentRow.Index;
                    if (IfEditPreviousRow == "Y")
                    {
                        if (mpMSVC.Rows.Count > mpMSVC.SelectedRow.Index + 1)
                            mpMSVC.SetFocus(mpMSVC.SelectedRow.Index + 1, 1);
                    }

                    if (IfEditPreviousRow == "N")
                    {
                        int rowcnt = mpMSVC.Rows.Count;
                        int rowID = mpMSVC.MainDataGridCurrentRow.Index + 1;
                        if (rowID >= rowcnt && mpMSVC.Rows[rowcnt - 1].Cells[0].Value != null)
                        {
                            rowID = mpMSVC.Rows.Add();
                        }
                        mpMSVC.SetFocus(rowID, 1);
                    }
                    else
                    {
                        int rowcnt = mpMSVC.Rows.Count;
                        int rowID = mpMSVC.MainDataGridCurrentRow.Index + 1;
                        if (rowID >= rowcnt && mpMSVC.Rows[rowcnt - 1].Cells[0].Value != null)
                        {
                            rowID = mpMSVC.Rows.Add();
                        }
                        mpMSVC.SetFocus(rowID, 1);
                    }
                    if (_Mode == OperationMode.Add)
                    {
                        DataTable dt = mpMSVC.GetGridData();
                        if (dt.Rows.Count > 0)
                            dt.WriteXml(General.GetPurchaseTempFile());
                    }
                    CalculateTotals();
                    btnOK.Enabled = true;
                    result = true;
                }
                //else
                //    btnOK.Enabled = false;
                //Update Barcode InProductMaster
                //if (string.IsNullOrEmpty(pobj.ScannedBarcode) == true && string.IsNullOrEmpty(ScannedBarcode) == false)
                //{
                //    pobj.ScannedBarcode = ScannedBarcode;
                //    bool SaveFlag = pobj.UpdateProductScanCode();
                //    if (SaveFlag)
                //        CacheObject.Clear("cacheCounterSale");
                //}
            }

            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.ButtonOKClick>>" + Ex.Message);
            }
            return result;
        }
        private void btnOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ButtonOKClick();
            }
            else if (e.KeyCode == Keys.Up)
                txtScanCode.Focus();
        }
        private void txtSchemeQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                {
                    //if (txtPendingScheme.Visible == true && _Mode == OperationMode.Add)
                    //    txtPendingScheme.Focus();
                    //int ss = 0;
                    //if (txtSchemeQuantity.Text != null && txtSchemeQuantity.Text.ToString() != string.Empty)
                    //    ss = Convert.ToInt32(txtSchemeQuantity.Text.ToString());
                    //if (ss > 0)
                    //{
                    //    ClearSchemePnl();
                    //    GetScheme();
                    //}
                    //else
                    txtSchemeAmount.Focus();
                }
                //else
                //    txtSchemeAmount.Focus();
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                    txtQuantity.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtSchemeQuantity_KeyDown>>" + Ex.Message);
            }
        }
        private void GetScheme()
        {

            pnlEnterScheme.Visible = true;
            pnlEnterScheme.BringToFront();
            _Scheme.ProductID = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString());
            bool retValue = _Scheme.ReadDetailsByProductID();
            if (retValue)
            {
                ifnewscheme = "N";
                FillSchemedata();
            }
            else
            {
                ifnewscheme = "Y";
                txtSchemeProduct.Text = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString();
                txtSchemePack.Text = mpMSVC.MainDataGridCurrentRow.Cells["Col_Pack"].Value.ToString();
                datePickerStartDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Now.Date.ToString("yyyyMMdd"));
                datePickerClosingDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
            }
            txtEnterScheme.Focus();
        }
        private void FillSchemedata()
        {
            txtSchemeProduct.Text = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString();
            txtSchemePack.Text = mpMSVC.MainDataGridCurrentRow.Cells["Col_Pack"].Value.ToString();
            datePickerStartDate.Value = General.ConvertStringToDateyyyyMMdd(_Scheme.StartDate);
            datePickerClosingDate.Value = General.ConvertStringToDateyyyyMMdd(_Scheme.ClosingDate);
            txtQuantity1.Text = _Scheme.Quantity1.ToString("#0");
            txtQuantity2.Text = _Scheme.Quantity2.ToString("#0");
            txtQuantity3.Text = _Scheme.Quantity3.ToString("#0");
            txtScheme1.Text = _Scheme.Scheme1.ToString("#0");
            txtScheme2.Text = _Scheme.Scheme2.ToString("#0");
            txtScheme3.Text = _Scheme.Scheme3.ToString("#0");
        }
        private void txtSchemeAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int mrowcount = 0;
                mrowcount = dgvBatchGrid.RowCount;
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value == null)
                {
                    txtBatch.Enabled = true;
                    txtMRP.Enabled = true;
                    txtExpiry.Enabled = true;
                }
                else if (mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value.ToString() == "Y")
                {
                    txtBatch.Enabled = false;
                    txtMRP.Enabled = false;
                    txtExpiry.Enabled = false;
                }
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value == null || mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value.ToString() != "Y")
                {

                    if (mrowcount > 0)
                    {
                        lblFooterMessage.Text = "Esc = Exit   Enter = Select Batch";
                        dgvBatchGrid.BringToFront();
                        dgvBatchGrid.Location = GetdgvBatchGridLocation();
                        dgvBatchGrid.Height = 237;
                        dgvBatchGrid.Width = pnlProductDetail.Width;
                        dgvBatchGrid.Visible = true;
                        dgvBatchGrid.Enabled = true;
                        dgvBatchGrid.Enabled = true;
                        pnlProductDetail.Enabled = false;
                        CalculatePurRateSaleRateAndAmount();
                        dgvBatchGrid.Focus();
                    }
                    else
                    {
                        CalculatePurRateSaleRateAndAmount();
                        lblFooterMessage.Text = "No Batch Data ";
                        txtBatch.Focus();
                    }
                }
                else
                {
                    txtTradeRate.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtSchemeQuantity.Focus();
            }
        }
        private void txtReplacement_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                    txtSchemeQuantity.Focus();
                else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                    txtQuantity.Focus();
                //else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.O)
                //{
                //    btnOK.Focus();
                //}
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtReplacement_KeyDown>>" + Ex.Message);
            }
        }
        private void txtBatch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                    txtExpiry.Focus();
                else if (e.KeyCode == Keys.Up)
                {
                    if (txtReplacement.Visible == true)
                        txtReplacement.Focus();
                    else
                        txtSchemeAmount.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtBatch_KeyDown>>" + Ex.Message);
            }
        }
        private void txtExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckValidExpiry();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtBatch.Focus();
            }
        }

        private bool CheckValidExpiry()
        {
            bool retValue = false;
            lblExpired.Visible = false;
            string exp = "";
            string expdate = "";
            try
            {
                if (txtExpiry.Text.ToString() == "0000")
                {
                    txtExpiry.Text = "00/00";
                }
                if (txtExpiry.Text.ToString() != "00/00")
                {
                    exp = General.GetValidExpiry(txtExpiry.Text.ToString().Trim());
                    txtExpiry.Text = exp;
                    if (exp == "")
                    {
                        lblFooterMessage.Text = "Please Check Expiry";
                        txtExpiry.Focus();
                    }
                    else
                    {

                        expdate = General.GetValidExpiryDate(exp);
                        txtExpiryDate.Text = expdate;
                        string mexpdate = General.GetExpiryInyyyymmddForm(expdate);
                        DateTime dd = General.ConvertStringToDateyyyyMMdd(mexpdate);


                        DateTime dt = DateTime.ParseExact(expdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        expdate = dt.ToString("dd/MM/yyyy");


                        TimeSpan tt = dd.Subtract(DateTime.Now.Date);
                        int days = tt.Days;
                        txtExpiredDays.Text = days.ToString("#0");
                        if (txtExpiry.Text == "00/00")
                            txtExpiry.BackColor = Color.Magenta;
                        else
                        {
                            btnOK.Enabled = true;
                            if (days < 90)
                            {
                                if (days < 0)
                                {
                                    PSMessageBox.Show("Product Expired", "Invalid expiry date", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                                    txtExpiry.Focus();
                                    txtExpiry.SelectAll();
                                    if (General.CurrentSetting.MsetPurchaseAcceptExpriedItems != "Y")
                                    {
                                        retValue = false;
                                    }
                                    else
                                        retValue = true;

                                }
                                else if (days < 30)
                                {
                                    PSMessageBox.Show("Near Expiry Product", "Close To Expiry ", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                                    txtMRP.Focus(); retValue = true;
                                }
                                else if (days < 60)
                                {
                                    PSMessageBox.Show("Near Expiry Product", "Close To Expiry ", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                                    txtMRP.Focus(); retValue = true;
                                }
                                else if (days < 90)
                                {
                                    PSMessageBox.Show("Near Expiry Product", "Close To Expiry ", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                                    txtMRP.Focus(); retValue = true;
                                }
                                else
                                {
                                    lblExpired.Text = "";
                                    lblExpired.BackColor = Color.White;
                                    retValue = false;
                                }

                                if ((General.CurrentSetting.MsetPurchaseAcceptExpriedItems == "Y" && days < 0))
                                {
                                    retValue = false;
                                }

                            }
                            else
                            {
                                lblExpired.Text = "";
                                retValue = true;
                                txtMRP.Focus();
                            }
                        }


                    }

                }
                else
                {
                    if (General.CurrentSetting.MsetGeneralExpiryDateReuired != "Y")
                    {
                        //cbAcceptNrExpired.Checked = true;
                        txtExpiryDate.Text = "";
                        txtMRP.Focus();
                        retValue = true;
                        btnOK.Enabled = true;
                    }
                    else
                    {
                        lblFooterMessage.Text = "Please Check Expiry";
                        txtExpiry.Focus();
                        btnOK.Enabled = false;
                        retValue = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private void txtMRP_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                {
                    double mmrp = Convert.ToDouble(txtMRP.Text.ToString());

                    CalculatePurRateSaleRateAndAmount();
                    txtTradeRate.Focus();
                    if (_Purchase.MRP != mmrp)
                        txtMRP.BackColor = Color.MediumVioletRed;
                }
                else if (e.KeyCode == Keys.Right)
                    txtCSTPer.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtExpiry.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtMRP_KeyDown>>" + Ex.Message);
            }
        }
        private void txtCSTPer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
                {
                    CalculatePurRateSaleRateAndAmount();
                    txtScanCode.Focus();
                }
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                    txtMRP.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtCSTPer_KeyDown>>" + Ex.Message);
            }
        }

        private void txtTradeRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtTradeRateValidating();
                    if (Convert.ToDouble(txtMRP.Text) == MinMRP && Convert.ToDouble(txtTradeRate.Text) > MinTradeRate) // [ansuman 23.02.2017]
                    {
                        timer.Interval = 500;
                        timer.Enabled = true;
                        timer.Start();
                    }
                    else
                    { timer.Stop(); SetColorToLowestRate(); }
                    if (Convert.ToDouble(txtPurchaseRate.Text) <= 0)
                    {
                        txtPurchaseRate.Text = txtTradeRate.Text;
                    }
                    //txtDiscountPer.Focus();
                }
                if (e.KeyCode == Keys.Right)
                    txtSchemeAmount.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtMRP.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {

            if (lblLowestRate.BackColor == Color.Thistle)
            {
                lblLowestRate.BackColor = Color.Red;
                lblSupplierName.BackColor = Color.Red;
                lblMRPVal.BackColor = Color.Red;
            }
            else
            {
                lblLowestRate.BackColor = Color.Thistle;
                lblSupplierName.BackColor = Color.Thistle;
                lblMRPVal.BackColor = Color.Thistle;
            }
        }
        private void SetColorToLowestRate()
        {
            lblLowestRate.BackColor = Color.Thistle;
            lblSupplierName.BackColor = Color.Thistle;
            lblMRPVal.BackColor = Color.Thistle;
        }
        private void txtTradeRateValidating()
        {
            double mtrate = 0;
            double mmrp = 0;
            if (txtMRP.Text != null && txtMRP.Text != string.Empty)
                mmrp = Convert.ToDouble(txtMRP.Text.ToString());
            if (txtTradeRate.Text != null && txtTradeRate.Text != string.Empty)
                mtrate = Convert.ToDouble(txtTradeRate.Text.ToString());
            if (mtrate > mmrp)
            {
                lblFooterMessage.Text = "Trade Rate should be < MRP";
                txtTradeRate.Focus();
            }
            else
            {
                CalculatePurRateSaleRateAndAmount();
                lblFooterMessage.Text = "";
                txtItemDiscountPer.Focus();
            }

        }
        private void txtDiscountPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtDiscountAmt.Text = "0.00";
                CalculatePurRateSaleRateAndAmount();
                if (General.CurrentSetting.MsetPurchaseIfPTR == "Y")
                    txtPTR.Focus();
                else
                    txtProfitPercentage.Focus();

            }
            else if (e.KeyCode == Keys.Up)
                txtTradeRate.Focus();
            else if (e.KeyCode == Keys.Right)
                txtDiscountAmt.Focus();

        }

        private void txtPurchaseVATPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                CalculatePurRateSaleRateAndAmount();


                btnOK.Focus();

            }
            else if (e.KeyCode == Keys.Up)
                txtItemDiscountPer.Focus();
        }

        private void txtSaleRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                btnOK.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtPurchaseVATPer.Focus();
        }
        private void mpMSVC_OnTABKeyPressed(object sender, EventArgs e)
        {
            btnSummary.BackColor = General.ControlFocusColor;
            btnSummary.Focus();
        }
        private void txtBatch_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBatch.Text.ToString()) == true && pnlProductDetail.Visible)
                txtBatch.Focus();
            else
                btnOK.Enabled = true;
        }
        //private void txtBatch_Validating(object sender, CancelEventArgs e)
        //{
        //    if ((txtBatch.Text.ToString() == null || txtBatch.Text.ToString() == ""))
        //        txtBatch.Focus();
        //    else
        //        btnOK.Enabled = true;
        //}
        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbCreditor.SeletedItem != null)
            {

                if (pnlPurchaseOrder.Visible == false)
                {
                    txtBillNumber.Enabled = true;
                    txtBillNumber.Focus();
                }
                else
                {
                    if (dgPurchaseOrder.Rows.Count > 0)
                    {
                        dgPurchaseOrder.Focus();
                    }
                    else
                        txtBillNumber.Focus();
                }
            }
            else
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
                Log.WriteError("UclPurchase.txtBillNumber_KeyDown>>" + Ex.Message);
            }
        }

        private void mpMSVC_OnRowDeleted(object sender, EventArgs e)
        {
            try
            {
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_IfTempPurchase"].Value == null || mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value.ToString() != "Y")
                    CalculateTotals();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.mpMSVC_OnRowDeleted>>" + Ex.Message);
            }
        }

        #endregion

        #region Calculate Amounts Rates
        private void CalculatePurRateSaleRateAndAmount()
        {


            double mmrp = 0;
            double mgridamttot = 0;
            try
            {
                double.TryParse(txtMRP.Text.ToString(), out mmrp);
                double.TryParse(txtGridAmountTot.Text.ToString(), out mgridamttot);
                if (mmrp > 0 || mgridamttot > 0)
                {
                    double mprate = 0;
                    double mtraderate = 0;
                    double mpurvatamt = 0;
                    double mqty = 0;
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
                    //double moctper = 0;
                    double moctamt = 0;
                    double msalerate = 0;
                    double mpurvatper = 0;
                    double msalevatper = 0;
                    double msalevatamt = 0;
                    double mamt = 0;
                    double mamtzerovat = 0;
                    double mskl = 0;
                    double mmargin = 0;
                    double mmargin2 = 0;
                    double mpurrate = 0;
                    double mvatamt = 0;

                    double.TryParse(txtQuantity.Text.ToString(), out mqty);
                    double.TryParse(txtTradeRate.Text.ToString(), out mtraderate);
                    double.TryParse(txtItemDiscountPer.Text.ToString(), out mitemdiscper);
                    double.TryParse(txtSchemePer.Text.ToString(), out mscmdiscper);
                    double.TryParse(txtSplDiscPerS.Text.ToString(), out mspldiscper);
                    double.TryParse(txtPurchaseVATPer.Text.ToString(), out mpurvatper);
                    double.TryParse(txtMasterVATPer.Text.ToString(), out msalevatper);
                    double.TryParse(txtCashDiscountPerS.Text.ToString(), out mcashdiscper);
                    double.TryParse(txtSchemeAmount.Text.ToString(), out mscmamt);
                    double.TryParse(txtSplDiscountS.Text.ToString(), out mspldiscamt);
                    double.TryParse(txtDiscountAmt.Text.ToString(), out mitemdiscamt);
                    double.TryParse(txtSaleRate.Text.ToString(), out msalerate);
                    if (txtPurchaseRate.Text != null && txtPurchaseRate.Text.ToString() != string.Empty)
                        double.TryParse(txtPurchaseRate.Text.ToString(), out mpurrate);
                    if (txtPurchaseVATAmt.Text != null && txtPurchaseVATAmt.Text.ToString() != string.Empty)
                        double.TryParse(txtPurchaseVATAmt.Text.ToString(), out mvatamt);
                    //double.TryParse(txtCSTPer.Text.ToString(), out mcstper); // sheela 31/12

                    mamt = Math.Round(mqty * mtraderate, 2); //4
                    mskl = Math.Round(mamt - mscmamt, 2); //4
                    _Purchase.AmountSchemeDiscount = mscmamt;
                    _Purchase.SchemeDiscountPercent = mscmdiscper;
                    if (mqty > 0)
                    {
                        //  if (mitemdiscamt > 0)
                        //     mitemdiscper = Math.Round((mitemdiscamt * 100 * mqty) / mskl, 2);
                        //   txtDiscountPer.Text = mitemdiscper.ToString("#0.00");

                        mitemdiscamt = Math.Round((((mskl) * mitemdiscper / 100) / mqty), 2); //4
                        mspldiscper = Math.Round((100 * mspldiscamt) / (mamt - mitemdiscamt - mscmamt - moctamt), 2); //4
                        _Purchase.AmountScmDiscountPerUnit = Math.Round(_Purchase.AmountSchemeDiscount / mqty, 2); //4
                        _Purchase.AmountSplDiscountPerUnit = Math.Round(Math.Round(((mskl - mitemdiscamt) * mspldiscper) / 100, 2) / mqty, 2); //4
                        _Purchase.AmountCashDiscountPerUnit = Math.Round((Math.Round((mskl - _Purchase.AmountSplDiscountPerUnit - mitemdiscamt) * mcashdiscper, 2) / 100) / mqty, 2); //4
                    }
                    //   double.TryParse(txtCSTAmount.Text.ToString(), out mcstamt);
                    double.TryParse(txtSchemeQuantity.Text.ToString(), out mscmqty);
                    if (mqty > 0)
                    {
                        double pamt = Math.Round(((mamt - moctamt - Math.Round(_Purchase.AmountCashDiscountPerUnit * mqty, 2) - Math.Round(_Purchase.AmountSplDiscountPerUnit * mqty, 2) - mscmamt - Math.Round(mitemdiscamt * mqty, 2)) / mqty), 2); //4
                        mpurvatamt = Math.Round(((mamt - moctamt - Math.Round(_Purchase.AmountCashDiscountPerUnit * mqty, 2) - Math.Round(_Purchase.AmountSplDiscountPerUnit * mqty, 2) - mscmamt - Math.Round(mitemdiscamt * mqty, 2)) / mqty) * mpurvatper / 100, 2); //4
                    }
                    //   mcstamt = 0; // Math.Round(mamt * mcstper / 100, 4); // sheela 31/12
                    //if (mqty > 0)// sheela 31/12
                    //    mcstamt = mcstamt / mqty;// sheela 31/12;
                    //  txtCSTAmount.Text = mcstamt.ToString("#0.000"); // sheela 31/12
                    msalevatamt = Math.Round((msalerate * msalevatper) / 100, 2); //4
                    if ((mqty + mscmqty) > 0)
                        mtraderateafterscm = Math.Round((mtraderate * mqty) / (mqty + mscmqty), 2); //4
                                                                                                    //if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                                                                                                    //    mprate = mtraderateafterscm + mpurvatamt - _Purchase.AmountScmDiscountPerUnit - mitemdiscamt - _Purchase.AmountCashDiscountPerUnit;
                                                                                                    //else
                    mprate = mtraderateafterscm - _Purchase.AmountScmDiscountPerUnit - mitemdiscamt - _Purchase.AmountCashDiscountPerUnit - _Purchase.AmountSplDiscountPerUnit;
                    //if (General.CurrentSetting.MsetPurchaseAddVATInSaleRate == "Y")
                    //    msalerate = mmrp + mmstamtbySale ;
                    //else
                    //    msalerate = mmrp;
                    mamt = Math.Round(mqty * mtraderate, 2);
                    if (mpurvatper == 0)
                        mamtzerovat = mamt - (mitemdiscamt * mqty) - mscmamt - (_Purchase.AmountSplDiscountPerUnit * mqty) - (_Purchase.AmountCashDiscountPerUnit * mqty);
                    else
                        mamtzerovat = 0;

                    txtDiscountAmt.Text = mitemdiscamt.ToString("#0.0000");
                    txtPurchaseVATAmt.Text = mpurvatamt.ToString("#0.0000");
                    txtMasterVATAmt.Text = msalevatamt.ToString("#0.0000");
                    txtAmount.Text = mamt.ToString("#0.00");
                    txtSaleRate.Text = msalerate.ToString("#0.00");
                    txtSplDiscountPerUnit.Text = _Purchase.AmountSplDiscountPerUnit.ToString("0.00");
                    txtCashDisountPerUnit.Text = _Purchase.AmountCashDiscountPerUnit.ToString("0.0000");
                    txtSplDiscPerS.Text = mspldiscper.ToString("#0.0000");
                    _Purchase.SpecialDiscountPercentS = mspldiscper;
                    if (mprate > 0)
                        txtPurchaseRate.Text = mprate.ToString("#0.00");
                    txtPurZeroVAT.Text = mamtzerovat.ToString("#0.00");
                    if (msalerate > 0)
                    {
                        mmargin = Math.Round(((msalerate + msalevatamt) - (mpurrate + mvatamt)) / (msalerate + msalevatamt), 4);
                        //mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / (mtraterate + mvatamt), 2);
                        mmargin2 = Math.Round(((msalerate + msalevatamt) - (mpurrate + mvatamt)) / (mpurrate + mvatamt), 4);
                    }
                    mmargin = Math.Round(mmargin * 100, 2);
                    mmargin2 = Math.Round(mmargin2 * 100, 2);
                    txtMargin.Text = mmargin.ToString("#0.00");
                    txtMargin2.Text = mmargin2.ToString("#0.00");
                    CalculateTotals();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculatePurRateSaleRateAndAmount>>" + Ex.Message);
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
                        //      double mcstamt = 0;
                        //    double mmstamtbySale = 0;
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
                        //double moctamt = 0;
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
                        //double.TryParse(dr.Cells["Col_ProdVATPer"].Value.ToString(), out msalevatper);
                        double.TryParse(txtCashDiscountPerS.Text.ToString(), out mcashdiscper);
                        if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null && dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString() != string.Empty)
                            double.TryParse(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString(), out mscmamt);
                        double.TryParse(txtSplDiscountS.Text.ToString(), out mspldiscamt);
                        //double.TryParse(dr.Cells["Col_PurchaseRate"].Value.ToString(), out mpurrate);
                        mamt = Math.Round(mqty * mtraderate, 2); //4
                        mskl = Math.Round(mamt - mscmamt, 2); //4
                        _Purchase.AmountSchemeDiscount = mscmamt;
                        _Purchase.SchemeDiscountPercent = mscmdiscper;

                        if (mqty > 0)
                        {
                            mitemdiscamt = Math.Round((((mskl) * mitemdiscper / 100) / mqty), 2); //4
                            mspldiscper = Math.Round((100 * mspldiscamt) / (mamt - mitemdiscamt - mscmamt), 2); //4
                            _Purchase.AmountScmDiscountPerUnit = Math.Round(_Purchase.AmountSchemeDiscount / mqty, 2); //4


                            _Purchase.AmountSplDiscountPerUnit = Math.Round((((mskl - mitemdiscamt) * mspldiscper) / 100) / mqty, 2); //4
                            _Purchase.AmountCashDiscountPerUnit = Math.Round((((mskl - _Purchase.AmountSplDiscountPerUnit - mitemdiscamt) * mcashdiscper) / 100) / mqty, 2); //4
                        }
                        //    double.TryParse(dr.Cells["Col_CSTAmount"].Value.ToString(), out mcstamt);
                        double.TryParse(dr.Cells["Col_Scheme"].Value.ToString(), out mscmqty);
                        if (mqty > 0)
                            mpurvatamt = Math.Round(((mamt - _Purchase.AmountCashDiscountPerUnit - _Purchase.AmountSplDiscountPerUnit - mscmamt - mitemdiscamt) / mqty) * mpurvatper / 100, 2); //4

                        msalevatamt = Math.Round((mmrp * msalevatper) / 100, 2); //4
                        if ((mqty + mscmqty) > 0)
                            mtraderateafterscm = Math.Round((mtraderate * mqty) / (mqty + mscmqty), 2); //4
                                                                                                        //if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                                                                                                        //    mprate = mtraderateafterscm + mpurvatamt  - _Purchase.AmountScmDiscountPerUnit - mitemdiscamt - _Purchase.AmountCashDiscountPerUnit - _Purchase.AmountSplDiscountPerUnit;
                                                                                                        //else
                        mprate = mtraderateafterscm - _Purchase.AmountScmDiscountPerUnit - mitemdiscamt - _Purchase.AmountCashDiscountPerUnit - _Purchase.AmountSplDiscountPerUnit;
                        ////if (General.CurrentSetting.MsetPurchaseAddVATInSaleRate == "Y")
                        ////    msalerate = mmrp + mmstamtbySale ;
                        ////else
                        msalerate = mmrp;
                        mamt = Math.Round(mqty * mtraderate, 2);
                        if (mpurvatper == 0)
                            mamtzerovat = mamt - mitemdiscamt - mscmamt;
                        else
                            mamtzerovat = 0;

                        //if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                        //{
                        //    if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                        //    {
                        //        mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / (mtraderate + mpurvatamt), 2);
                        //        mmargin2 = Math.Round((msalerate - mprate) / mprate, 2);
                        //    }
                        //    else
                        //    {
                        //        mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / msalerate, 2);
                        //        mmargin2 = Math.Round((msalerate - mprate) / msalerate, 2);
                        //    }
                        //}
                        //else
                        //{
                        //if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                        //{
                        mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / (mtraderate + mpurvatamt), 2);
                        mmargin2 = Math.Round((msalerate - (mprate + mpurvatamt)) / (mprate + mpurvatamt), 2);
                        //    }
                        //    else
                        //    {
                        //        mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / (msalerate), 2);
                        //        mmargin2 = Math.Round((msalerate - (mprate + mpurvatamt)) / (msalerate), 2);  // [Ansuman]
                        //    }
                        //}
                        mmargin = Math.Round(mmargin * 100, 2);
                        mmargin2 = Math.Round(mmargin2 * 100, 2);


                        dr.Cells["Col_ItemDiscountAmount"].Value = mitemdiscamt.ToString("#0.0000");
                        dr.Cells["Col_ItemSCMDiscountAmount"].Value = mscmamt.ToString("#0.00");
                        dr.Cells["Col_VATAmountPurchase"].Value = mpurvatamt.ToString("#0.0000");
                        dr.Cells["Col_VATAmountSale"].Value = msalevatamt.ToString("#0.0000");
                        dr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                        dr.Cells["Col_SaleRate"].Value = msalerate.ToString("#0.00");
                        // dr.Cells["Col_SplDiscountPer"].Value = _Purchase.AmountSplDiscountPerUnit.ToString("0.00");
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
                Log.WriteError("UclPurchase.CalculatePurRateSaleRateAmountforFullGrid>>" + Ex.Message);
            }
        }
        private void CalculateTotals()
        {
            // check for inpurstring not in correct format???

            double mtotamt = 0;
            double mamt = 0;
            int itemCount = 0;
            //   double mmargin = 0;
            //   double mmargin2 = 0;
            double mpurrate = 1;
            double msalerate = 1;
            double mvatamt = 0;
            double mtraterate = 0;
            double mmrp = 0;
            double msalevatamt = 0;
            if (txtPurchaseRate.Text != null && txtPurchaseRate.Text.ToString() != string.Empty)
                double.TryParse(txtPurchaseRate.Text.ToString(), out mpurrate);
            if (txtSaleRate.Text != null && txtSaleRate.Text.ToString() != string.Empty)
                double.TryParse(txtSaleRate.Text.ToString(), out msalerate);
            if (txtPurchaseVATAmt.Text != null && txtPurchaseVATAmt.Text.ToString() != string.Empty)
                double.TryParse(txtPurchaseVATAmt.Text.ToString(), out mvatamt);
            if (txtTradeRate.Text != null && txtTradeRate.Text.ToString() != string.Empty)
                double.TryParse(txtTradeRate.Text.ToString(), out mtraterate);
            if (txtMRP.Text != null && txtMRP.Text != string.Empty)
                double.TryParse(txtMRP.Text.ToString(), out mmrp);
            if (txtMasterVATAmt.Text != null && txtMasterVATAmt.Text.ToString() != string.Empty)
                double.TryParse(txtMasterVATAmt.Text.ToString(), out msalevatamt);
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
                //if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                //{
                //    if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                //    {
                //        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / (mtraterate + mvatamt), 2);
                //        mmargin2 = Math.Round((msalerate - mpurrate) / mpurrate, 2);
                //    }
                //    else
                //    {
                //        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / msalerate, 2);
                //        mmargin2 = Math.Round((msalerate - mpurrate) / msalerate, 2);
                //    }
                //}
                //else
                //{

                //    if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")             // [Ansuman] [Need To Confirm]
                //    {
                ////if (msalerate > 0)
                ////{
                ////    mmargin = Math.Round(((msalerate + msalevatamt) - (mpurrate + mvatamt)) / (msalerate + msalevatamt), 2);
                ////    //mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / (mtraterate + mvatamt), 2);
                ////    mmargin2 = Math.Round(((msalerate + msalevatamt) - (mpurrate + mvatamt)) / (mpurrate + mvatamt), 2);
                ////}
                //}
                //else
                //{
                //    mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / (mtraterate + mvatamt), 2);
                //    //mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / (msalerate), 2);
                //    mmargin2 = Math.Round((msalerate - (mpurrate + mvatamt)) / (msalerate), 2);

                //}
                ////}
                //mmargin = Math.Round(mmargin * 100, 2);
                //mmargin2 = Math.Round(mmargin2 * 100, 2);
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
                Log.WriteError("UclPurchase.CalculateTotals>>" + Ex.Message);
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

                    if (_Purchase.AmountBillS - _Purchase.AmountSpecialDiscountS - _Purchase.AmountSchemeDiscountS - _Purchase.AmountItemDiscountS + _Purchase.AmountCreditNoteS + _Purchase.VAT5Percent + _Purchase.VAT12point5Percent <= _Purchase.AmountDebitNoteS)
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

                    //.Text = _Purchase.GSTAmt0.ToString("#0.00");
                    FillGSTpnl();

                    //     if (_Purchase.OctroiPercentageS >= 0)
                    //         _Purchase.AmountOctroiS = Math.Round(_Purchase.TotalAmountForOctroiS * _Purchase.OctroiPercentageS / 100, 2);
                    //_Purchase.AmountS = Math.Round(_Purchase.AmountBillS + _Purchase.AmountCSTS - _Purchase.AmountSchemeDiscountS - _Purchase.AmountItemDiscountS
                    //    - _Purchase.AmountSpecialDiscountS + _Purchase.AmountAddOnFreightS - _Purchase.AmountLessS + _Purchase.AmountCreditNoteS
                    //    - _Purchase.AmountDebitNoteS - _Purchase.AmountCashDiscountS + _Purchase.AmountVAT5PercentS
                    //    + _Purchase.AmountVAT12point5PercentS + _Purchase.AmountOctroiS, 2);

                    double mtotgstamt = 0;
                    double mtotgst = 0;

                    mtotgstamt = _Purchase.GSTAmt0 + _Purchase.GSTAmtS5 + _Purchase.GSTAmtS12 + _Purchase.GSTAmtS18 + _Purchase.GSTAmtS28 +
                                 _Purchase.GSTAmtC5 + _Purchase.GSTAmtC12 + _Purchase.GSTAmtC18 + _Purchase.GSTAmtC28 + _Purchase.GSTAmtI5 +
                                 _Purchase.GSTAmtI12 + _Purchase.GSTAmtI18 + _Purchase.GSTAmtI18 + _Purchase.GSTI28;
                    mtotgst = _Purchase.GSTS5 + _Purchase.GSTS12 + _Purchase.GSTS18 + _Purchase.GSTS28 +
                                 _Purchase.GSTC5 + _Purchase.GSTC12 + _Purchase.GSTC18 + _Purchase.GSTC28 + _Purchase.GSTI5 + _Purchase.GSTI12 + _Purchase.GSTI18 + _Purchase.GSTI28;

                    _Purchase.AmountS = Math.Round(mtotgstamt + mtotgst + _Purchase.AmountAddOnFreightS - _Purchase.AmountLessS + _Purchase.AmountCreditNoteS
                       - _Purchase.AmountDebitNoteS, 2);
                    CalculateRoundup();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateFinalSummary>>" + Ex.Message);
            }

        }

        private void FillGSTpnl()
        {
            txtSPUR5.Text = _Purchase.GSTAmtS5.ToString("#0.00");
            txtSPUR12.Text = _Purchase.GSTAmtS12.ToString("#0.00");
            txtSPUR18.Text = _Purchase.GSTAmtS18.ToString("0.00");
            txtSPUR28.Text = _Purchase.GSTAmtS28.ToString("0.00");
            txtCPUR5.Text = _Purchase.GSTAmtC5.ToString("#0.00");
            txtCPUR12.Text = _Purchase.GSTAmtC12.ToString("#0.00");
            txtCPUR18.Text = _Purchase.GSTAmtC18.ToString("0.00");
            txtCPUR28.Text = _Purchase.GSTAmtC28.ToString("0.00");

            txtSGST5.Text = _Purchase.GSTS5.ToString("0.00");
            txtSGST12.Text = _Purchase.GSTS12.ToString("0.00");
            txtSGST18.Text = _Purchase.GSTS18.ToString("0.00");
            txtSGST28.Text = _Purchase.GSTS28.ToString("0.00");

            txtCGST5.Text = _Purchase.GSTC5.ToString("0.00");
            txtCGST12.Text = _Purchase.GSTC12.ToString("0.00");
            txtCGST18.Text = _Purchase.GSTC18.ToString("0.00");
            txtCGST28.Text = _Purchase.GSTC28.ToString("0.00");

            txtIGST5.Text = _Purchase.GSTI5.ToString("#0.00");
            txtIGST12.Text = _Purchase.GSTI12.ToString("#0.00");
            txtIGST18.Text = _Purchase.GSTI18.ToString("#0.00");
            txtIGST28.Text = _Purchase.GSTI28.ToString("#0.00");

            txtIPUR0.Text = _Purchase.GSTAmt0.ToString("#0.00");
            txtIPUR5.Text = _Purchase.GSTAmtI5.ToString("#0.00");
            txtIPUR12.Text = _Purchase.GSTAmtI12.ToString("#0.00");
            txtIPUR18.Text = _Purchase.GSTAmtI18.ToString("#0.00");
            txtIPUR28.Text = _Purchase.GSTAmtI28.ToString("#0.00");
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
                Log.WriteError("UclPurchase.CalculateRoundup>>" + Ex.Message);
            }
        }
        private void CalculateFinalVAT()
        {

            _Purchase.GSTAmt0 = 0;
            _Purchase.GSTAmtS5 = 0;
            _Purchase.GSTAmtC5 = 0;
            _Purchase.GSTAmtI5 = 0;
            _Purchase.GSTS5 = 0;
            _Purchase.GSTC5 = 0;
            _Purchase.GSTI5 = 0;
            _Purchase.GSTAmtS12 = 0;
            _Purchase.GSTAmtC12 = 0;
            _Purchase.GSTAmtI12 = 0;
            _Purchase.GSTS12 = 0;
            _Purchase.GSTC12 = 0;
            _Purchase.GSTI12 = 0;
            _Purchase.GSTAmtS18 = 0;
            _Purchase.GSTAmtC18 = 0;
            _Purchase.GSTAmtI18 = 0;
            _Purchase.GSTS18 = 0;
            _Purchase.GSTC18 = 0;
            _Purchase.GSTI18 = 0;
            _Purchase.GSTAmtS28 = 0;
            _Purchase.GSTAmtC28 = 0;
            _Purchase.GSTAmtI28 = 0;
            _Purchase.GSTS28 = 0;
            _Purchase.GSTC28 = 0;
            _Purchase.GSTI28 = 0;
            //   double  mgstamt = 0;
            //   double  mgst = 0;
            //   double mtotdisczero = 0;
            //     double mtotdisc5 = 0;
            //    double mtotdisc12point5 = 0;
            //     double mtotdiscother = 0;
            double mtotcashdiscount = 0;
            //     double mmstamt5 = 0;
            //     double mmstamt12point5 = 0;
            //      double mmstamtother = 0;
            //      double mtotmstzero = 0;
            //      double mtotmst5 = 0;
            //      double mtotmst12point5 = 0;
            //      double mtotmstother = 0;
            int mqty = 0;
            double mskl = 0;
            double mscmdisc = 0;
            double mitm = 0;
            double msplddx = 0;
            double mcrddx = 0;
            double mddx = 0;
            double mtt1 = 0;
            double mtt1S = 0;
            double mtta = 0;
            double mmstperpur = 0;
            double mgstamts = 0;
            double mgstamtc = 0;
            double mgsts = 0;
            double mgstc = 0;
            double mgstamt0 = 0;

            double mtotamt = 0;

            //  double mpuramountzero = 0;
            //  double mpuramount0 = 0;
            //   double mpuramount5 = 0;
            //    double mpuramount12point5 = 0;
            double mamt = 0;
            double mtotalvat = 0;
            if (txtCashDiscountPerS.Text != "")
                _Purchase.CashDiscountPercentageS = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
            try
            {

                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    mgstamts = 0;
                    mgstamtc = 0;
                    mgsts = 0;
                    mgstc = 0;
                    mgstamt0 = 0;
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
                        mcrddx = 0; // ss 19-10-2017
                        mddx = Math.Round(Math.Round((mskl - msplddx - mitm) * _Purchase.CashDiscountPercentageS, 2) / 100, 2); //4
                        mtta = Math.Round((mamt - mddx - msplddx - mcrddx - mscmdisc - mitm), 2);
                        mtt1 = Math.Round(mtta * (mmstperpur / 100), 2); //4
                        mtt1S = mtt1;
                        mtt1 = Math.Round(mtt1 / mqty, 2); //4
                        mtotalvat += mtt1;
                        //dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();
                        dr.Cells["Col_CreditNoteAmount"].Value = mcrddx.ToString();
                        dr.Cells["Col_CashDiscountAmount"].Value = mddx.ToString();
                        mtotcashdiscount += mddx;
                        dr.Cells["Col_VATAmountPurchase"].Value = mtt1.ToString();
                        //dr.Cells["Col_SplDiscountPer"].Value = _Purchase.SpecialDiscountPercentS.ToString();

                        if (mmstperpur == 0)
                        {
                            mgstamt0 = mtta;
                            _Purchase.GSTAmt0 += mtta;
                        }
                        else
                        {
                            mgstamts = Math.Round(mtta * (General.CurrentSetting.MsetGSTSPercent / 100), 2);
                            if (General.CurrentSetting.MsetGSTSPercent == 50)
                                mgstamtc = mgstamts;
                            else
                                mgstamtc = mtta - mgstamts;
                            mgsts = Math.Round(mtt1S * (General.CurrentSetting.MsetGSTSPercent / 100), 2);
                            if (General.CurrentSetting.MsetGSTSPercent == 50)
                                mgstc = mgsts;
                            else
                                mgstc = mtta - mgsts;
                        }
                        mtotamt += mgstamts + mgstamtc + mgsts + mgstc;
                        //vat 5.5
                        if (mmstperpur == 5)
                        {
                            if (_Purchase.IFOMS != "Y")
                            {
                                _Purchase.GSTAmtS5 += mgstamts;
                                _Purchase.GSTAmtC5 += mgstamtc;

                                _Purchase.GSTS5 += mgsts;
                                _Purchase.GSTC5 += mgstc;
                            }
                            else
                            {
                                _Purchase.GSTAmtI5 += (mgstamts + mgstamtc);
                                _Purchase.GSTI5 += (mgsts + mgstc);
                            }
                        }
                        else if (mmstperpur == 12.00)
                        {
                            if (_Purchase.IFOMS != "Y")
                            {
                                _Purchase.GSTAmtS12 += mgstamts;
                                _Purchase.GSTAmtC12 += mgstamtc;
                                _Purchase.GSTS12 += mgsts;
                                _Purchase.GSTC12 += mgstc;
                            }
                            else
                            {
                                _Purchase.GSTAmtI12 += (mgstamts + mgstamtc);
                                _Purchase.GSTI12 += (mgsts + mgstc);
                            }

                        }
                        else if (mmstperpur == 18.00)
                        {
                            if (_Purchase.IFOMS != "Y")
                            {
                                _Purchase.GSTAmtS18 += mgstamts;
                                _Purchase.GSTAmtC18 += mgstamtc;
                                _Purchase.GSTS18 += mgsts;
                                _Purchase.GSTC18 += mgstc;
                            }
                            else
                            {
                                _Purchase.GSTAmtI18 += (mgstamts + mgstamtc);
                                _Purchase.GSTI18 += (mgsts + mgstc);
                            }
                        }
                        else if (mmstperpur == 28.00)
                        {
                            if (_Purchase.IFOMS != "Y")
                            {
                                _Purchase.GSTAmtS28 += mgstamts;
                                _Purchase.GSTAmtC28 += mgstamtc;
                                _Purchase.GSTS28 += mgsts;
                                _Purchase.GSTC28 += mgstc;
                            }
                            else
                            {
                                _Purchase.GSTAmtI28 += (mgstamts + mgstamtc);
                                _Purchase.GSTI28 += (mgsts + mgstc);
                            }
                        }

                    }
                    dr.Cells["Col_GSTAmountZero"].Value = mgstamt0;
                    if (_Purchase.IFOMS != "Y")
                    {
                        dr.Cells["Col_GSTSAmount"].Value = mgstamts.ToString();
                        dr.Cells["Col_GSTCAmount"].Value = mgstamtc.ToString();
                        dr.Cells["Col_GSTS"].Value = mgsts.ToString();
                        dr.Cells["Col_GSTC"].Value = mgstc.ToString();
                    }
                    else
                    {
                        dr.Cells["Col_GSTIAmount"].Value = (mgstamts + mgstamtc).ToString();
                        dr.Cells["Col_GSTI"].Value = (mgsts + mgstc).ToString();
                    }

                }
                txtCashDiscountAmountS.Text = mtotcashdiscount.ToString("#0.00");
                txtPreCashDiscountAmountS.Text = mtotcashdiscount.ToString("#0.00");
                txtNetAmountS.Text = mtotamt.ToString("#0.00");
                FillGSTpnl();


            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateFinalVAT>>" + Ex.Message);
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
        #endregion

        # region Button Click
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
                    pnlEditProduct.Visible = false;
                    pnlProductDetail.Visible = false;
                    dgvLastPurchase.Visible = false;
                    pnlBillDetails.Enabled = false;
                    mpMSVC.Enabled = false;
                    pnlSummary.Location = GetpnlSummaryLocation();
                    pnlSummary.BringToFront();
                    pnlSummary.Visible = true;
                    if (_Purchase.IFOMS != "Y")
                    {
                        pnlGST.Location = GetpnlGSTLocation();
                        pnlGST.BringToFront();
                        pnlGST.Visible = true;
                        pnlIGST.Visible = false;
                    }
                    else
                    {
                        pnlIGST.Location = GetpnlGSTLocation();
                        pnlIGST.BringToFront();
                        pnlIGST.Visible = true;
                        pnlGST.Visible = false;
                    }

                    pnlDebitCreditNote.BringToFront();
                    dgCreditNote.Visible = true;
                    if (_Mode == OperationMode.Edit || cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                        pnlBank.Visible = false;
                    if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _Purchase.StatementNumber > 0)
                        pnlSummary.Enabled = false;
                    else
                        pnlSummary.Enabled = true;
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
                    pnlEditProduct.Visible = false;
                    pnlProductDetail.Visible = false;
                    dgvLastPurchase.Visible = false;
                    pnlBillDetails.Enabled = false;
                    mpMSVC.Enabled = false;
                    GetpnlSummaryLocation();
                    GetpnlGSTLocation();
                    pnlSummary.BringToFront();
                    pnlSummary.Enabled = false;
                    CalculateGetSummaryData();
                    CalculateFinalSummary();
                    txtCRAmountS.Focus();
                    btnSummary.Enabled = false;
                    pnlSummary.Visible = true;
                    pnlGST.BringToFront();
                    if (_Purchase.IFOMS != "Y")
                    {
                        pnlGST.BringToFront();
                        //     pnlGST.Enabled = false;
                        pnlGST.Visible = true;
                        pnlIGST.Visible = false;
                    }
                    else
                    {
                        pnlIGST.BringToFront();
                        pnlIGST.Visible = true;
                        pnlGST.Visible = false;
                    }
                }
                if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _Mode == OperationMode.Delete)
                    btnCancelS.Visible = false;
                else
                    btnCancelS.Visible = true;
            }

            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnSummary_Click>>" + Ex.Message);
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
                pnlGST.Visible = false;
                //   pnlGST.SendToBack();
                pnlSummary.Visible = false;
                //    pnlSummary.SendToBack();
                btnSummary.Enabled = true;
                mpMSVC.BringToFront();
                mpMSVC.Visible = true;
                if (_Purchase.IfTypeChange == "N")
                {
                    pnlBillDetails.Enabled = true;
                    mpMSVC.Enabled = true;
                    tsBtnSave.Enabled = false;
                }
                if (txtGridAmountTot.Text != null && txtGridAmountTot.Text != "")
                    btnSummary.Enabled = true;
                mpMSVC.SetFocus(1);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCancelS_Click>>" + Ex.Message);
            }
        }
        private void CalculateGetSummaryData()
        {

            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {
                double mtotamt = 0;
                double mtotscm = 0;
                double mtotitem = 0;
                //double mtotvat5 = 0;
                //double mtotvat12point5 = 0;
                //   double mcstamount = 0; //sheela 31/12
                double mvatamount = 0;
                double mamt = 0;
                double mamts = 0;
                double mvatper = 0;
                double mqty = 0;
                //     double mtotvatzeroamt = 0;
                //    double moctroiamt = 0;
                _Purchase.AmountCashDiscountS = 0;
                double mtotspldisc = 0;
                //   double mpuramount0 = 0;
                //double mpuramount5 = 0;
                //double mpuramount12point5 = 0;
                double mtotamtbysalerate = 0;
                double mtotamtbypurrate = 0;
                double msalerate = 0;
                double mprate = 0;
                //     int muom = 1;
                double puramt = 0;

                try
                {
                    foreach (DataGridViewRow dr in mpMSVC.Rows)
                    {
                        if (dr.Cells["Col_SaleRate"].Value != null && dr.Cells["Col_SaleRate"].Value.ToString().Trim() != "")
                        {
                            mprate = 0;
                            //    muom = 1;//Col_UnitOfMeasure
                            puramt = 0;

                            double.TryParse(dr.Cells["Col_SaleRate"].Value.ToString(), out msalerate);
                            if (dr.Cells["Col_PurchaseRate"].Value != null && dr.Cells["Col_PurchaseRate"].Value.ToString().Trim() != "")
                                double.TryParse(dr.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                            //if (dr.Cells["Col_UnitOfMeasure"].Value != null && dr.Cells["Col_UnitOfMeasure"].Value.ToString().Trim() != "")
                            //    int.TryParse(dr.Cells["Col_UnitOfMeasure"].Value.ToString(), out muom);
                            if (dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != string.Empty)
                                mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                            mtotamtbysalerate += Math.Round(mqty * (msalerate), 2);
                            mtotamtbypurrate += Math.Round(mqty * (mprate), 2);
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
                            // sheela 31/12
                            mamt = 0;

                            //if (dr.Cells["Col_CSTAmount"].Value != null && dr.Cells["Col_CSTAmount"].Value.ToString() != "")
                            //{
                            //    double.TryParse(dr.Cells["Col_CSTAmount"].Value.ToString(), out mamt);
                            //    mcstamount += mamt * mqty;
                            //    puramt += Math.Round(mamt, 2);
                            //}
                            // sheela 31/12
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

                            //mamt = 0;
                            //if (dr.Cells["Col_SplDiscountAmount"].Value != null && dr.Cells["Col_SplDiscountAmount"].Value.ToString() != "")
                            //{
                            //    double.TryParse(dr.Cells["Col_SplDiscountAmount"].Value.ToString(), out mamt);
                            //    mtotspldisc += mamt;
                            //    puramt -= Math.Round(mamt * mqty, 2);
                            //}
                            //mamt = 0;
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
                            //        moctroiamt += mamt;
                            //}



                            //if (mvatper == 0)
                            //{
                            //    mtotvatzeroamt += puramt;
                            //    mpuramount0 += puramt;
                            //}
                            //else if (mvatper == 13.5 || mvatper == 12.5)
                            //{
                            //    mtotvat12point5 += mvatamount;
                            //    mpuramount12point5 += puramt;
                            //}
                            //else
                            //{
                            //    mtotvat5 += mvatamount;
                            //    mpuramount5 += puramt;
                            //}

                        }
                    }
                    //if (Convert.ToDouble(txtGridAmountTot.Text.ToString()) != _Purchase.AmountBillS)
                    //{

                    //   _Purchase.TotalAmountForOctroiS = moctroiamt;
                    txtBillAmountS.Text = mtotamt.ToString("#0.00");
                    txtBillAmount.Text = mtotamt.ToString("#0.00");
                    txtItemDiscountS.Text = mtotitem.ToString("#0.00");
                    txtSplDiscPerS.Text = _Purchase.SpecialDiscountPercentS.ToString("#0.00");
                    txtCashDiscountAmountS.Text = _Purchase.AmountCashDiscountS.ToString("0.00");
                    txtPreCashDiscountAmountS.Text = _Purchase.AmountCashDiscountS.ToString("#0.00");
                    txtSchemeDiscountS.Text = mtotscm.ToString("#0.00");
                    //   txtViewVat5per.Text = mtotvat5.ToString("#0.00");
                    //   txtViewVat12point5per.Text = mtotvat12point5.ToString("#0.00");
                    //    txtPurchaseAmountVAT12point5S.Text = mpuramount12point5.ToString("#0.00");
                    //   txtPurchaseAmountVAT5S.Text = mpuramount5.ToString("#0.00");
                    //   txtPurchaseAmountVATZeroS.Text = mpuramount0.ToString("#0.00");
                    double mtotprofit = 0;
                    if (mtotamtbypurrate > 0)
                        mtotprofit = Math.Round(((mtotamtbysalerate - mtotamtbypurrate) / mtotamtbypurrate) * 100, 2);
                    txtProfitPerS.Text = mtotprofit.ToString("#0.00");
                    CalculateFinalVAT();

                    //}
                }

                catch (Exception Ex)
                {
                    Log.WriteError("UclPurchase.CalculateGetSummaryData>>" + Ex.Message);
                }
            }
        }

        #endregion

        #region summary keydown textchange
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
                    txtDBAmountS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtAddOnS_KeyDown>>" + Ex.Message);
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
                Log.WriteError("UclPurchase.txtAddOnS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtCRAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    CalculateFinalSummary();
                    txtDBAmountS.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtCRAmountS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtDBAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtAddOnS.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtCRAmountS.Focus();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtDBAmountS_KeyDown>>" + Ex.Message);
            }
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
                Log.WriteError("UclPurchase.txtNarration_KeyDown>>" + Ex.Message);
            }
        }
        private void txtCashDiscountPerS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
                {
                    lblFooterMessage.Text = string.Empty;
                    if (pnlProductDetail.Visible == true)
                    {
                        CalculatePurRateSaleRateAndAmount();
                    }
                    else
                    {
                        CalculatePurRateSaleRateAmountforFullGrid();
                    }
                    if (mpMSVC.Rows.Count > 0)
                        mpMSVC.SetFocus(mpMSVC.Rows.Count - 1, 1);
                }
                else if (e.KeyCode == Keys.Up)
                    txtNarration.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtCashDiscountPerS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtSpecialDiscountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        CalculatePurRateSaleRateAndAmount();
                    }
                    else
                    {
                        CalculatePurRateSaleRateAmountforFullGrid();
                    }
                    if (mpMSVC.Rows.Count > 0)
                        mpMSVC.SetFocus(mpMSVC.Rows.Count - 1, 1);
                }
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                    txtCashDiscountPerS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtSpecialDiscountS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtPreCashDiscountAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
                {
                    lblFooterMessage.Text = string.Empty;
                    if (pnlProductDetail.Visible == true)
                    {
                        CalculatePurRateSaleRateAndAmount();
                    }
                    else
                    {
                        CalculatePurRateSaleRateAmountforFullGrid();
                    }
                    if (mpMSVC.Rows.Count > 0)
                        mpMSVC.SetFocus(mpMSVC.Rows.Count - 1, 1);
                }
                else if (e.KeyCode == Keys.Up)
                    txtCashDiscountPerS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtCashDiscountPerS_KeyDown>>" + Ex.Message);
            }

        }

        private void txtCashDiscountAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    //  txtOCTPerS.Focus();
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
                        //    pnlSummary.Visible = false;
                        //    pnlBillDetails.Enabled = true;
                        txtCashDiscountPerS.Text = entereddiscper.ToString("#0.00");
                        lblFooterMessage.Text = "Press Enter..";
                        // txtCashDiscountPerS.Focus();
                        //    btnSummary.Enabled = true;
                        //  btnSummary.Focus();

                    }
                    //CalculateFinalSummary();
                    //CalculateFinalVAT(); // [ansuman]
                    CalculatePurRateSaleRateAndAmount();
                }
                else if (e.KeyCode == Keys.Up)
                {
                    CalculateFinalSummary();
                    txtLessS.Focus();
                }
                if (e.KeyCode == Keys.Enter)
                {
                    MainToolStrip.Select();
                    tsBtnSave.Select();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtOCTPerS_KeyDown>>" + Ex.Message);
            }
        }



        private void txtPurchaseVATPer_Validating(object sender, CancelEventArgs e)
        {
            double purvat = 0;
            try
            {
                purvat = Convert.ToDouble(txtPurchaseVATPer.Text.ToString());
                // vat 5.5
                if (purvat != 0 && purvat != 5.00 && purvat != 12.00 && purvat != 18.00 && purvat != 28.00)
                    txtPurchaseVATPer.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtPurchaseVATPer_Validating>>" + Ex.Message);
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
                pnlGST.BringToFront();
                pnlGST.Visible = true;
                //   pnlGST.Enabled           
                pnlSummary.BringToFront();
                pnlSummary.Visible = true;
                pnlSummary.Focus();
                txtCRAmountS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCRDBOK_Click>>" + Ex.Message);
            }
        }
        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            try
            {
                btnSummary.BackColor = Color.Bisque;
                btnSummary.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.mpPVC1_OnTABKeyPressed>>" + Ex.Message);
            }
        }

        private void mpPVC1_OnRowDeleted(object sender, EventArgs e)
        {
            try
            {
                CalculateTotals();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.mpPVC1_OnRowDeleted>>" + Ex.Message);
            }
        }

        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CalculatePurRateSaleRateAndAmount();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.cbRound_CheckedChanged>>" + Ex.Message);
            }
        }

        private void btnPaymentHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlPaymentDetails.Visible == false)
                {
                    mpMSVC.Enabled = false;
                    pnlPaymentDetails.BringToFront();
                    pnlPaymentDetails.Visible = true;
                    dgPaymentDetails.Visible = true;
                    btnSummary.Enabled = false;

                }
                else
                {
                    pnlPaymentDetails.SendToBack();
                    dgPaymentDetails.Visible = false;
                    mpMSVC.Enabled = true;
                    pnlPaymentDetails.Visible = false;
                    pnlBillDetails.Enabled = true;
                    btnSummary.Enabled = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnPaymentHistory_Click>>" + Ex.Message);
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
                Log.WriteError("UclPurchase.btnCRDBNote_Click>>" + Ex.Message);
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

        //private void btnTypeChange_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        _Purchase.IfTypeChange = "Y";
        //        //if (General.CurrentSetting.MsetPurchaseIfCreditPurchase == "Y")
        //        //{
        //        //    rbtCreditSTMT.Visible = true;
        //        //    rbtCreditSTMT.Enabled = true;
        //        //}
        //        //else
        //        //    rbtCreditSTMT.Visible = false;

        //        tsBtnSave.Enabled = true;
        //        mcbCreditor.Enabled = false;
        //        mpMSVC.Enabled = false;
        //        pnlProductDetail.Enabled = false;
        //       // pnlGST.Enabled = false;
        //        pnlSummary.Enabled = false;
        //        cbTransactionType.Enabled = false;
        //        cbNewTransactionType.Enabled = true;
        //        cbNewTransactionType.Items.Clear();
        //        if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
        //        {

        //            cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
        //            if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
        //                cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
        //            cbNewTransactionType.Text = FixAccounts.TransactionTypeForCredit;
        //            cbNewTransactionType.SelectedIndex = cbNewTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
        //        }
        //        else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
        //        {
        //            cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
        //            if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
        //                cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
        //            cbNewTransactionType.Text = FixAccounts.TransactionTypeForCash;
        //            cbNewTransactionType.SelectedIndex = cbNewTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);

        //        }
        //        else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
        //        {
        //            cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
        //            cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
        //            cbNewTransactionType.Text = FixAccounts.TransactionTypeForCredit;
        //            cbNewTransactionType.SelectedIndex = cbNewTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
        //        }
        //        btnTypeChange.Enabled = false;
        //        cbNewTransactionType.Focus();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("UclPurchase.btnTypeChange_Click>>" + Ex.Message);
        //    }
        //}



        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtVouchernumber.Text != "")
                    {

                        _Purchase.VoucherNumber = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _Purchase.VoucherSeries = txtVoucherSeries.Text.ToString();
                        _Purchase.ReadDetailsByVouNumber(_Purchase.VoucherNumber, _Purchase.VoucherType, _Purchase.VoucherSeries, _Purchase.VoucherSubType);
                        if (mpMSVC.Rows.Count > 1)
                        {
                            mpMSVC.SetFocus(mpMSVC.Rows.Count - 1, 1);
                            System.Threading.Thread.Sleep(10);
                        }
                        FillSearchData(_Purchase.Id, "");
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteError("UclPurchase.txtVouchernumber_KeyDown>>" + Ex.Message);
                }
            }
        }


        private void mcbCreditor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Up)
                    cbTransactionType.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.mcbCreditor_KeyDown>>" + Ex.Message);
            }
        }



        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbCreditor.SelectedID;
            FillCreditorCombo();
            mcbCreditor.SelectedID = selectedId;
            txtBillNumber.Focus();
        }

        private void cbAcceptNrExpired_CheckedChanged(object sender, EventArgs e)
        {
            //CBAcceptExpiryCheckedChange();
        }

        //private void CBAcceptExpiryCheckedChange()
        //{
        //    //if (cbAcceptNrExpired.Checked == true)
        //    {
        //        txtMRP.Focus();
        //        btnOK.Enabled = true;
        //    }
        //    else
        //    {
        //        btnCancel.Focus();
        //        btnCancel.BackColor = General.ControlFocusColor;
        //        btnOK.Enabled = false;
        //    }
        //}

        private void cbAcceptNrExpired_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMRP.Focus();
        }

        private void btnOK_Enter(object sender, EventArgs e)
        {
            btnOK.BackColor = General.ControlFocusColor;
        }

        private void btnOK_Leave(object sender, EventArgs e)
        {
            btnOK.BackColor = Color.White;
        }

        private void btnCancel_Leave(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.White;
        }

        private void btnCancel_Enter(object sender, EventArgs e)
        {
            btnCancel.BackColor = General.ControlFocusColor;
        }

        private void dgCreditNote_OnCellValueChangeCommited(int colIndex)
        {
            dgCreditNote.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgCreditNote_OnShowViewForm(DataGridViewRow selectedRow)
        {
            string voutype = "";
            try
            {
                if (selectedRow != null && dgCreditNote.Rows.Count > 0 && selectedRow.Index >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = selectedRow.Cells[0].Value.ToString();
                    voutype = selectedRow.Cells["Col_VoucherType"].Value.ToString();
                    if (voutype == FixAccounts.VoucherTypeForCreditNoteStock)
                        ViewControl = new UclCreditNoteStock();
                    else if (voutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                        ViewControl = new UclCreditNoteAmount();
                    else if (voutype == FixAccounts.VoucherTypeForDebitNoteStock)
                        ViewControl = new UclDebitNotestock();
                    else if (voutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                        ViewControl = new UclDebitNoteAmount();
                    ShowViewForm(selectedID);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        private void ViewControl_ExitClicked(object sender, EventArgs e)
        {

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

        private void txtNarration_Enter(object sender, EventArgs e)
        {
            if (txtBillNumber.Text == null || txtBillNumber.Text.ToString() == "")
                txtBillNumber.Focus();
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

        private void txtScanCode_TextChanged(object sender, EventArgs e)
        {
            lblFooterMessage.Text = txtScanCode.Text;
        }

        //private void cbTransactionType_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //        mcbCreditor.Focus();
        //}

        //private void cbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        private void UclPurchase_Load(object sender, EventArgs e)
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

        private void mpMSVC_OnCellValueChangeCommited(int colIndex)
        {
            if (colIndex == 1)
            {
                _preID = 0;
                string prodname = "";
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                    _preID = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value != null)
                    prodname = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString();
                if (prodname != "" && _preID < 0)
                {
                    prodname = General.GetProductName(_preID);
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = prodname;
                }
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

        private void btnPrintBarCode_Click(object sender, EventArgs e)
        {

            int currentbarcoderow = 0;
            //   retValue = _Purchase.DeletePreviousRecordsFromtblBarCode();
            if (dgvBarCode.Rows.Count > 0)
                dgvBarCode.Rows.Clear();
            foreach (DataGridViewRow dr in mpMSVC.Rows)
            {
                if (dr.Cells["Col_IFBarCodeRequired"].Value != null && dr.Cells["Col_IFBarCodeRequired"].Value.ToString() == "Y")
                {
                    currentbarcoderow = dgvBarCode.Rows.Add();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_ProductID"].Value = dr.Cells["Col_ProductID"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_ProductName"].Value = dr.Cells["Col_ProductName"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_UnitOfMeasure"].Value = dr.Cells["Col_UnitOfMeasure"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_Pack"].Value = dr.Cells["Col_Pack"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_Company"].Value = dr.Cells["Col_Company"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_BatchNumber"].Value = dr.Cells["Col_BatchNumber"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_MRP"].Value = dr.Cells["Col_MRP"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_Expiry"].Value = dr.Cells["Col_Expiry"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_Quantity"].Value = dr.Cells["Col_Quantity"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_ShelfCode"].Value = dr.Cells["Col_ShelfCode"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_VoucherType"].Value = _Purchase.VoucherType;
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_VoucherNumber"].Value = _Purchase.VoucherNumber.ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_ProdClosingStock"].Value = dr.Cells["Col_ProdClosingStock"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_ScanCode"].Value = dr.Cells["Col_ScanCode"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_StockID"].Value = dr.Cells["Col_StockID"].ToString();
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_PartyID"].Value = mcbCreditor.SelectedID;
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_PartyName"].Value = mcbCreditor.SeletedItem.ItemData[2].ToString();
                }

            }
        }

        private void txtDistRatePercent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDistPercentEnterkeyPressed();
                //txtScanCode.Focus();
                btnOK.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtPurchaseVATPer.Focus();

        }

        private void txtDistPercentEnterkeyPressed()
        {
            double mdistper = 0;
            double mdistrate = 0;
            double mtraderate = 0;
            try
            {
                //if (txtDistRatePercent.Text != null && txtDistRatePercent.Text.ToString() != string.Empty)
                //    mdistper = Convert.ToDouble(txtDistRatePercent.Text.ToString());
                if (txtTradeRate.Text != null && txtTradeRate.Text.ToString() != string.Empty)
                    mtraderate = Convert.ToDouble(txtTradeRate.Text.ToString());
                if (mdistper > 0)
                {
                    mdistrate = Math.Round((mtraderate * mdistper) / 100, 2);
                    mdistrate = Math.Round(mtraderate + mdistrate, 2);
                    ///*  txt*/DistSaleRate.Text = mdistrate.ToString("#0.00");
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void mpMSVC_OnShiftTABKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }

        private void cbTransactionType_Leave(object sender, EventArgs e)
        {
            cbTransactionType.BackColor = Color.White;
        }

        private void dgPaymentDetails_OnShowViewForm(DataGridViewRow selectedRow)
        {
            string voutype = "";
            try
            {
                if (selectedRow != null && dgPaymentDetails.Rows.Count > 0 && selectedRow.Index >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = selectedRow.Cells[6].Value.ToString();
                    voutype = selectedRow.Cells["Col_VoucherType"].Value.ToString();
                    if (voutype == FixAccounts.VoucherTypeForBankPayment)
                        ViewControl = new UclBankPayment();
                    else if (voutype == FixAccounts.VoucherTypeForCashPayment)
                        ViewControl = new UclCashPayment();
                    ShowViewForm(selectedID);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        private void dgCreditNote_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            btnCRDBOKClick();
        }


        private void dgPurchaseOrder_KeyDown(object sender, KeyEventArgs e)
        {
            string ifchecked = string.Empty;
            if (_Mode == OperationMode.Add)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value != null && dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value.ToString() != string.Empty)
                        ifchecked = dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value.ToString();
                    if (ifchecked != string.Empty)
                        dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value = string.Empty;
                    else
                        dgPurchaseOrder.CurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();

                    //  CopyPurchaseOrderProducts();
                }
                else if (e.KeyCode == Keys.Tab)
                {

                }
            }

        }
        private void dgPurchaseOrder_OnShowViewForm(DataGridViewRow selectedRow)
        {
            string voutype = "";
            try
            {
                if (selectedRow != null && dgPurchaseOrder.Rows.Count > 0 && selectedRow.Index >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = selectedRow.Cells[0].Value.ToString();
                    voutype = selectedRow.Cells["Col_VoucherType"].Value.ToString();
                    if (voutype == FixAccounts.VoucherTypeForCreditNoteStock)
                        ViewControl = new UclCreditNoteStock();
                    else if (voutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                        ViewControl = new UclCreditNoteAmount();
                    else if (voutype == FixAccounts.VoucherTypeForDebitNoteStock)
                        ViewControl = new UclDebitNotestock();
                    else if (voutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                        ViewControl = new UclDebitNoteAmount();
                    ShowViewForm(selectedID);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        private void CopyPurchaseOrderProducts()
        {
            throw new NotImplementedException();
        }

        private void btnPurchaseOrderOK_Click(object sender, EventArgs e)
        {
            btnPurchaseOrderOKClick();
        }
        private void btnPurchaseOrderOKClick()
        {
            lblFooterMessage.Text = "";
            pnlPurchaseOrder.Visible = false;
            dgPurchaseOrder.Visible = false;
            DataTable purchaseordertable;
            PurchaseOrder _purchaseorder = new PurchaseOrder();
            string poid = string.Empty;
            int _RowIndex = 0;
            mpMSVC.Rows.Clear();
            int ProductID = 0;
            try
            {
                foreach (DataGridViewRow crdbdr in dgPurchaseOrder.Rows)
                {
                    string ch = string.Empty;
                    if (crdbdr.Cells["Col_Check"].Value != null && crdbdr.Cells["Col_Check"].Value.ToString() != string.Empty)
                        ch = crdbdr.Cells["Col_Check"].Value.ToString();
                    if (ch != string.Empty)
                    {
                        poid = crdbdr.Cells["Col_DSLID"].Value.ToString();
                        purchaseordertable = _purchaseorder.ReadProductDetailsByIDForPurchase(poid);
                        foreach (DataRow dr in purchaseordertable.Rows)
                        {
                            _RowIndex = -1;
                            ProductID = 0;
                            if (dr["ProductID"] != DBNull.Value)
                                ProductID = Convert.ToInt32(dr["ProductID"].ToString());
                            _RowIndex = CheckForProductinMainGrid(ProductID);
                            if (_RowIndex == -1)
                            {
                                _RowIndex = mpMSVC.Rows.Add();
                                mpMSVC.Rows[_RowIndex].Cells["Col_ProductID"].Value = dr["ProductID"].ToString();
                                mpMSVC.Rows[_RowIndex].Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                                mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].ReadOnly = true;
                                mpMSVC.Rows[_RowIndex].Cells["Col_UnitOfMeasure"].Value = dr["ProdLoosePack"].ToString();
                                mpMSVC.Rows[_RowIndex].Cells["Col_Pack"].Value = dr["ProdPack"].ToString();
                                mpMSVC.Rows[_RowIndex].Cells["Col_Company"].Value = dr["ProdCompShortName"].ToString();
                                mpMSVC.Rows[_RowIndex].Cells["Col_Box1"].Value = "0";
                                mpMSVC.Rows[_RowIndex].Cells["Col_ProdClosingStock"].Value = dr["ProdClosingStock"].ToString();
                                mpMSVC.Rows[_RowIndex].Cells["Col_ProdVATPer"].Value = dr["ProdLastPurchaseVATPer"].ToString();
                                mpMSVC.Rows[_RowIndex].Cells["Col_IFBarCodeRequired"].Value = dr["ProdIfBarCodeRequired"].ToString();
                                mpMSVC.Rows[_RowIndex].Cells["Col_BatchNumber1"].Value = dr["ProdLastPurchaseBatchNumber"].ToString();
                                mpMSVC.Rows[_RowIndex].Cells["Col_Quantity"].Value = dr["OrderQuantity"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_BatchNumber"].Value = dr["ProdLastPurchaseBatchNumber"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_Expiry"].Value = dr["ProdLastPurchaseExpiry"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_TradeRate"].Value = dr["ProdLastPurchaseTradeRate"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_MRP"].Value = dr["ProdLastPurchaseMRP"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_VAT"].Value = dr["ProdLastPurchaseVATPer"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_Scheme"].Value = "0";
                                //mpMSVC.Rows[_RowIndex].Cells["Col_Replacement"].Value = "0";
                                //mpMSVC.Rows[_RowIndex].Cells["Col_ItemDiscountPer"].Value = "0";
                                //mpMSVC.Rows[_RowIndex].Cells["Col_Amount"].Value = "0";
                                //mpMSVC.Rows[_RowIndex].Cells["Col_ItemDiscountAmount"].Value = "0";
                                //mpMSVC.Rows[_RowIndex].Cells["Col_SplDiscountPer"].Value = "0";
                                //mpMSVC.Rows[_RowIndex].Cells["Col_SplDiscountAmount"].Value = "0";
                                //mpMSVC.Rows[_RowIndex].Cells["Col_VATAmountPurchase"].Value = "0";
                                //mpMSVC.Rows[_RowIndex].Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_ProductID"].Value = dr["ProductID"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                                //mpMSVC.Rows[_RowIndex].Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                            }

                        }
                    }


                }

                //   pnlPurchaseOrder.Visible = false;

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCRDBOK_Click>>" + Ex.Message);
            }
        }

        private int CheckForProductinMainGrid(int ProductID)
        {
            int index = -1;
            int prodID = 0;
            foreach (DataGridViewRow dr in mpMSVC.Rows)
            {
                if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() != string.Empty)
                    prodID = Convert.ToInt32(dr.Cells["Col_ProductID"].Value.ToString());
                if (prodID == ProductID)
                    index = dr.Index;
            }
            return index;
        }

        private void dgPurchaseOrder_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dgPurchaseOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // int vouno = 0;
            try
            {
                if (dgPurchaseOrder.CurrentRow != null && dgPurchaseOrder.Rows.Count > 0 && dgPurchaseOrder.CurrentRow.Index >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgPurchaseOrder.CurrentRow.Cells["Col_DSLID"].Value.ToString();
                    //voutype = dgPurchaseOrder.CurrentRow.Cells["Col_VoucherType"].Value.ToString();
                    //if (voutype == FixAccounts.VoucherTypeForCreditNoteStock)
                    //    ViewControl = new UclCreditNoteStock();
                    //else if (voutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                    //    ViewControl = new UclCreditNoteAmount();
                    //else if (voutype == FixAccounts.VoucherTypeForDebitNoteStock)
                    //    ViewControl = new UclDebitNotestock();
                    //else if (voutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                    //    ViewControl = new UclDebitNoteAmount();
                    ViewControl = new UclPurchaseOrder();
                    ShowViewForm(selectedID);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            BtnDownLoadClick();
        }

        private void BtnDownLoadClick()
        {
            //ConstructdgBillsColumns();
            //dgBills.BringToFront();
            //dgBills.Visible = true;
            //ShowBills();
            //Invoices invoices = new Invoices();
            //DataTable dtInvoices = new DataTable("InvoiceItems");
            ////invoices.InvoicesFromUserBulk(DeveloperId, UserId, Password, dtInvoices);

            ////Download fresh Invoices
            //DataTable invoicesReceived = invoices.InvoicesToUser(General.DeveloperId, General.UserId, General.Password);
            //Emilan _emilan = new Emilan();
            //bool retvalue = _emilan.CopyInvoicesReceived(invoicesReceived);

        }
        private void ShowBills()
        {
            DataTable dt = new DataTable();
            Emilan em = new Emilan();
            dt = em.GetPurchaseBillsForGrid();
            if (dt != null && dt.Rows.Count > 0)
                FillGridWithExistingRows(dt);
        }

        private void FillGridWithExistingRows(DataTable mdt)
        {
            DataTable dt = mdt;
            string minvno = "";
            int currow = 0;
            string mdate = "";
            string accnm = "";
            double mamt = 0;
            string mifbillingdone = "";
            int mvouno = 0;
            string mvendorID = "";
            string mbatchID = "";
            string mchallan = "";
            foreach (DataRow dr in mdt.Rows)
            {
                mdate = "";
                accnm = "";
                mamt = 0;
                mifbillingdone = "";
                mvouno = 0;
                mvendorID = "";
                mbatchID = "";
                mchallan = "";

                currow = dgBills.Rows.Add();
                if (dr["InvNo"] != DBNull.Value)
                    minvno = dr["InvNo"].ToString();
                if (dr["InvDate"] != DBNull.Value)
                    mdate = dr["InvDate"].ToString();
                DateTime dd = Convert.ToDateTime(mdate);
                mdate = dd.Date.ToString("yyyyMMdd");
                mdate = General.GetDateInShortDateFormat(mdate);
                if (dr["accname"] != DBNull.Value)
                    accnm = dr["AccName"].ToString();
                if (dr["InvAmt"] != DBNull.Value)
                    mamt = Convert.ToDouble(dr["InvAmt"].ToString());
                if (dr["IfBillingDone"] != DBNull.Value)
                    mifbillingdone = dr["IfBillingDone"].ToString();
                if (dr["VoucherNumber"] != DBNull.Value)
                    mvouno = Convert.ToInt32(dr["VoucherNumber"].ToString());
                if (dr["Vendor"] != DBNull.Value)
                    mvendorID = dr["Vendor"].ToString();
                if (dr["Vendor"] != DBNull.Value)
                    mvendorID = dr["Vendor"].ToString();
                if (dr["ChallanNumber"] != DBNull.Value)
                    mchallan = dr["ChallanNumber"].ToString();
                if (dr["BatchID"] != DBNull.Value)
                    mbatchID = dr["BatchID"].ToString();

                dgBills.Rows[currow].Cells["Col_InvoiceNumber"].Value = minvno;
                dgBills.Rows[currow].Cells["Col_InvoiceDate"].Value = mdate;
                dgBills.Rows[currow].Cells["Col_Party"].Value = accnm;
                dgBills.Rows[currow].Cells["Col_AmountNet"].Value = mamt.ToString("#0.00");
                dgBills.Rows[currow].Cells["Col_Narration"].Value = mifbillingdone;

                dgBills.Rows[currow].Cells["Col_VendorID"].Value = mvendorID;
                dgBills.Rows[currow].Cells["Col_ChallanNumber"].Value = mchallan;
                dgBills.Rows[currow].Cells["Col_BatchID"].Value = mbatchID;
                if (mvouno > 0)
                    dgBills.Rows[currow].ReadOnly = true;


            }
        }
        private void ConstructdgBillsColumns()
        {
            dgBills.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VendorID";
                //   column.DataPropertyName = "VendorID";
                column.HeaderText = "VouSeries";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = "Check";
                column.HeaderText = " ";
                column.Width = 15;
                dgBills.Columns.Add(column);

                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_InvoiceNumber";
                //  column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "Invoice Number";
                column.Width = 50;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_InvoiceDate";
                //  column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "Date";
                column.Width = 90;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Party";
                // column.DataPropertyName = "Narration";
                column.HeaderText = "Party";
                column.Width = 220;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
                //   column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 80;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgBills.Columns.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                //   column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                //    column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.ReadOnly = true;
                column.Width = 80;
                dgBills.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                //    column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 80;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Narration";
                // column.DataPropertyName = "Narration";
                column.HeaderText = "Remark";
                column.Width = 200;
                column.ReadOnly = false;
                dgBills.Columns.Add(column);
                //5

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ChallanNumber";
                //   column.DataPropertyName = "VendorID";
                column.HeaderText = "VouSeries";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgBills.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void dgBills_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                string invno = "";
                string challanno = "";
                string vendorID = "";
                string batchID = "";
                DataTable dt = new DataTable();
                Emilan em = new Emilan();
                //  dgBills.CurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();
                if (dgBills.CurrentRow.Cells["Col_VendorID"].Value != null)
                    vendorID = dgBills.CurrentRow.Cells["Col_VendorID"].Value.ToString();
                if (dgBills.CurrentRow.Cells["Col_BatchID"].Value != null)
                    batchID = dgBills.CurrentRow.Cells["Col_BatchID"].Value.ToString();
                if (dgBills.CurrentRow.Cells["Col_InvoiceNumber"].Value != null)
                    invno = dgBills.CurrentRow.Cells["Col_InvoiceNumber"].Value.ToString();
                if (dgBills.CurrentRow.Cells["Col_ChallanNumber"].Value != null)
                    challanno = dgBills.CurrentRow.Cells["Col_ChallanNumber"].Value.ToString();
                dt = em.GetPurchaseDetails(vendorID, batchID, invno, challanno);
                FillPurchaseGrid(dt);
            }

        }

        private void FillPurchaseGrid(DataTable dt)
        {
            throw new NotImplementedException();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            _formImportAlliedSaleBill = new FormImportSaleBill();
            CreateImportAlliedSaleBillForm();
            _formImportAlliedSaleBill.RefreshData();
            DialogResult result = _formImportAlliedSaleBill.ShowDialog();
            tempdelpath = string.Empty;
            if (result == DialogResult.OK)
            {
                _ImportBill = _formImportAlliedSaleBill.ImportBillData;
                if (_ImportBill != null)
                {
                    FillFormWithImportBillData();
                }
                tempdelpath = _formImportAlliedSaleBill.tempdelpath;
            }
        }
        private void CreateImportAlliedSaleBillForm()
        {
            _formImportAlliedSaleBill = new FormImportSaleBill();
        }

        #region Events
        #endregion

        #endregion

        #region ProductDetail

        private void FillPack()
        {
            txtPack1.SelectedID = null;
            txtPack1.SourceDataString = new string[2] { "PackID", "PackName" };
            txtPack1.ColumnWidth = new string[2] { "0", "100" };
            Pack _Pack = new Pack();
            DataTable dtable = _Pack.GetOverviewData();
            txtPack1.FillData(dtable);
        }
        private void FillPackType()
        {
            txtPackType1.SelectedID = null;
            txtPackType1.SourceDataString = new string[2] { "ID", "PackTypeName" };
            txtPackType1.ColumnWidth = new string[2] { "0", "100" };
            Pack _Pack = new Pack();
            DataTable dtable = _Pack.GetOverviewDataForPackType();
            txtPackType1.FillData(dtable);
        }
        private void FillCompanyCombo()
        {
            mcbCompany1.SelectedID = null;
            mcbCompany1.SourceDataString = new string[5] { "CompID", "CompName", "CompShortName", "PartyID_1", "PartyID_2" };
            mcbCompany1.ColumnWidth = new string[5] { "0", "250", "50", "0", "0" };
            mcbCompany1.ValueColumnNo = 0;
            mcbCompany1.UserControlToShow = new UclCompany();
            Company _Company = new Company();
            DataTable dtable = _Company.GetOverviewData();
            mcbCompany1.FillData(dtable);
        }
        private void FillGenericCategoryCombo()
        {
            mcbGenCatOpStock.SelectedID = null;
            mcbGenCatOpStock.SourceDataString = new string[2] { "GenericCategoryId", "GenericCategoryName" };
            mcbGenCatOpStock.ColumnWidth = new string[2] { "0", "600" };   // kiran
            mcbGenCatOpStock.ValueColumnNo = 0;
            mcbGenCatOpStock.UserControlToShow = new UclGenericCategory();
            GenericCategory _GenericCateory = new GenericCategory();
            DataTable dtable = _GenericCateory.GetOverviewData();
            mcbGenCatOpStock.FillData(dtable);
        }
        private void FillProdCategoryCombo()
        {
            mcbProductCategory1.SelectedID = null;
            mcbProductCategory1.SourceDataString = new string[3] { "ProductCategoryID", "ProductCategoryName", "SaleDiscount" };
            mcbProductCategory1.ColumnWidth = new string[3] { "0", "200", "20" };
            mcbProductCategory1.ValueColumnNo = 0;
            mcbProductCategory1.UserControlToShow = new UclProdCategory();
            ProductCategory _ProductCategory = new ProductCategory();
            DataTable dtable = _ProductCategory.GetOverviewData();
            mcbProductCategory1.FillData(dtable);
        }
        private void FillShelfComboList()
        {
            mcbShelfNoOpStock.SelectedID = null;
            mcbShelfNoOpStock.SourceDataString = new string[2] { "ShelfId", "ShelfCode" };
            mcbShelfNoOpStock.ColumnWidth = new string[2] { "0", "200" };
            mcbShelfNoOpStock.ValueColumnNo = 0;
            mcbShelfNoOpStock.UserControlToShow = new UclShelf();
            Shelf _Shelf = new Shelf();
            DataTable dtable = _Shelf.GetOverviewData();
            mcbShelfNoOpStock.FillData(dtable);
        }
        private void FillScheduleDrugCombo()
        {
            mcbSchedule1.Items.Clear();
            Schedule _Schedule = new Schedule();
            DataTable dtable = _Schedule.GetOverviewData();
            if (dtable != null)
            {
                foreach (DataRow dr in dtable.Rows)
                {
                    if (dr["ScheduleCode"] != DBNull.Value)
                    {
                        mcbSchedule1.Items.Add(dr["ScheduleCode"].ToString());
                    }
                }
                foreach (DataRow dr in dtable.Rows)
                {
                    mcbSchedule1.Text = dr["ScheduleCode"].ToString();
                    mcbSchedule1.SelectedIndex = mcbSchedule1.Text.IndexOf(dr["ScheduleCode"].ToString());
                    break;
                }
            }
        }
        private Product FillProductAndCmpnyData(int ID)
        {
            if (_purchaseMode == PurchaseMode.Edit)
            {
                OPStock _OPStock = new OPStock();
                pobj.Id = ID.ToString();
                pobj.Name = txtProdName.Text;
                pobj.ProdCompID = 0;
                pobj.ProdPack = "";
                pobj.ProdPackType = "";
                pobj.ProdPackTypeID = 0;
                //    pobj.ProdLoosePack = Convert.ToInt32(txtUOM.Text);
                try
                {
                    DataRow drowprod = null;
                    DataRow drowcmpny = null;
                    DataRow drowGenCat = null;
                    DataRow drowProdCat = null;
                    DataRow drowShelf = null;
                    DataRow cmpnydetails = null;
                    DBProduct dbProd = new DBProduct();
                    DBCompany dbComp = new DBCompany();
                    drowprod = dbProd.ReadDetailsByID(ID);

                    if (drowprod != null)
                    {
                        if (drowprod["ProdCompID"] != DBNull.Value)
                        {
                            pobj.ProdCompID = Convert.ToInt32(drowprod["ProdCompID"]);
                            drowcmpny = dbComp.ReadDetailsByID(pobj.ProdCompID);
                            if (drowcmpny["CompName"] != DBNull.Value)
                            {
                                DataTable dt = new DataTable();
                                cobj.CName = drowcmpny["CompName"].ToString();
                                dt.Columns.Add(new DataColumn("CompID", typeof(string)));
                                dt.Columns.Add(new DataColumn("CompName", typeof(string)));
                                dt.Columns.Add(new DataColumn("CompShortName", typeof(string)));
                                cmpnydetails = dt.NewRow();
                                cmpnydetails["CompID"] = drowcmpny["CompID"];
                                cmpnydetails["CompName"] = drowcmpny["CompName"];
                                cmpnydetails["CompShortName"] = drowcmpny["CompShortName"];
                                dt.Rows.Add(cmpnydetails);
                                //mcbCompany1.Refresh();
                                //mcbCompany1.FillData(dt);
                                mcbCompany1.SelectedID = cmpnydetails["CompID"].ToString();
                                txtCompShortName1.Text = mcbCompany1.SeletedItem.ItemData[2].ToString();
                                pobj.ProdCompShortName = mcbCompany1.SeletedItem.ItemData[2];
                            }
                            if (drowprod["ProdDrugID"] != DBNull.Value)
                            {
                                DataTable dt = new DataTable();
                                DataRow dr = null;
                                dr = _OPStock.GetGenCategoryID(drowprod["ProdDrugID"].ToString());
                                if (dr != null)
                                {
                                    dt.Columns.Add(new DataColumn("GenericCategoryID", typeof(string)));
                                    dt.Columns.Add(new DataColumn("GenericCategoryName", typeof(string)));
                                    drowGenCat = dt.NewRow();
                                    drowGenCat["GenericCategoryID"] = dr["GenericCategoryID"];
                                    drowGenCat["GenericCategoryName"] = dr["GenericCategoryName"];
                                    dt.Rows.Add(drowGenCat);
                                    //mcbGenCatOpStock.Refresh();
                                    //mcbGenCatOpStock.FillData(dt);
                                    mcbGenCatOpStock.SelectedID = drowGenCat["GenericCategoryID"].ToString();
                                    //SS
                                    //pobj.ProdGenericID = mcbGenCatOpStock.SeletedItem.ItemData[0].ToString();
                                }
                            }
                            if (drowprod["ProdCategoryID"] != DBNull.Value)  // [14.11.2016]
                            {
                                DataTable dt = new DataTable();
                                DataRow dr = null;
                                dr = _OPStock.GetProdCategoryID(drowprod["ProdCategoryID"].ToString());
                                if (dr != null)
                                {
                                    dt.Columns.Add(new DataColumn("ProductCategoryID", typeof(string)));
                                    dt.Columns.Add(new DataColumn("ProductCategoryName", typeof(string)));
                                    drowProdCat = dt.NewRow();
                                    drowProdCat["ProductCategoryID"] = dr["ProductCategoryID"];
                                    drowProdCat["ProductCategoryName"] = dr["ProductCategoryName"];
                                    dt.Rows.Add(drowProdCat);
                                    //mcbProductCategory1.Refresh();
                                    //mcbProductCategory1.FillData(dt);
                                    mcbProductCategory1.SelectedID = drowProdCat["ProductCategoryID"].ToString();
                                    pobj.ProdProductCategoryID = Convert.ToInt32(mcbProductCategory1.SeletedItem.ItemData[0].ToString());
                                }
                            }

                            if (drowprod["ProdShelfID"] != DBNull.Value)
                            {
                                DataTable dt = new DataTable();
                                DataRow dr = null;
                                dr = _OPStock.GetShelfID(drowprod["ProdShelfID"].ToString());
                                if (dr != null)
                                {
                                    dt.Columns.Add(new DataColumn("ShelfID", typeof(string)));
                                    dt.Columns.Add(new DataColumn("ShelfCode", typeof(string)));
                                    drowShelf = dt.NewRow();
                                    drowShelf["ShelfID"] = dr["ShelfID"];
                                    drowShelf["ShelfCode"] = dr["ShelfCode"];
                                    dt.Rows.Add(drowShelf);
                                    //mcbShelfNoOpStock.Refresh();
                                    //mcbShelfNoOpStock.FillData(dt);
                                    mcbShelfNoOpStock.SelectedID = drowShelf["ShelfID"].ToString();
                                }
                                //if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Shelf"].Value.ToString() == "" && _purchaseMode == PurchaseMode.Edit) // [ansuman][18.11.2016]
                                //{
                                //    FillShelfComboList();
                                //    if (mcbShelfNoOpStock.SelectedID != "")
                                //    {
                                //        mcbShelf.SelectedID = mcbShelfNoOpStock.SelectedID;
                                //        pobj.ProdShelfID = mcbShelfNoOpStock.SeletedItem.ItemData[0].ToString();
                                //    }
                                //    else
                                //    {
                                //        mcbShelfNoOpStock.SelectedID = "";
                                //        pobj.ProdShelfID = "";
                                //    }
                                //}
                            }
                            if (drowprod["ProdScheduleDrugCode"] != DBNull.Value)
                            {
                                mcbSchedule1.Items.Add(drowprod["ProdScheduleDrugCode"].ToString());
                                mcbSchedule1.Text = drowprod["ProdScheduleDrugCode"].ToString();
                                pobj.ProdScheduleDrugCode = mcbSchedule1.SelectedItem.ToString();
                            }
                        }
                        if (drowprod["ProdPack"] != DBNull.Value)
                        {
                            pobj.ProdPack = Convert.ToString(drowprod["ProdPack"]);
                            txtPack1.Text = pobj.ProdPack;
                        }
                        if (drowprod["ProdPackType"] != DBNull.Value)
                        {
                            pobj.ProdPackType = Convert.ToString(drowprod["ProdPackType"]);
                            txtPackType1.Text = pobj.ProdPackType;
                        }
                        if (drowprod["ProdLoosePack"] != DBNull.Value)
                        {
                            pobj.ProdLoosePack = Convert.ToInt32(drowprod["ProdLoosePack"]);
                            pobj.ProdPreLoosePack = pobj.ProdLoosePack;
                            //  txtUOM.Text = pobj.ProdLoosePack.ToString();
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
            return pobj;
        }
        public void FillPnlEditProduct()
        {
            if (pnlProductDetail.Visible == true)
                lblEditProductTitle.Text = "Edit Product Details";

            txtProdName.Text = mpMSVC.MainDataGridCurrentRow.Cells[1].Value.ToString();
            //   txtUOM.Text = mpMSVC.MainDataGridCurrentRow.Cells["Col_UnitOfMeasure"].Value.ToString();
            if (mcbShelfNoOpStock.SelectedID != null && mcbShelfNoOpStock.SelectedID != "")    // [14.11.2016]
                mpMSVC.Text = mcbShelfNoOpStock.SeletedItem.ItemData[1].ToString();
            txtProdName.Focus();
        }
        private void DOProductEdit()
        {
            pnlEditProduct.Visible = true;
            pnlEditProduct.BringToFront();
            pnlEditProduct.Enabled = true;
            //FillCompanyCombo();
            //FillGenericCategoryCombo();
            //FillProdCategoryCombo();
            //FillShelfComboList();
            //FillScheduleDrugCombo();
            //FillPack();
            //FillPackType();
            this.ActiveControl = pnlEditProduct;
            pnlEditProduct.Select();
            pnlEditProduct.Focus();
            this.ActiveControl = txtProdName;
            txtProdName.Select();
            txtProdName.Focus();
        }
        private void ClearStockData()
        {
            mcbCompany1.SelectedID = "";
            mcbGenCatOpStock.SelectedID = "";
            mcbProductCategory1.SelectedID = "";
            mcbShelfNoOpStock.SelectedID = "";
            mcbSchedule1.SelectedItem = "";
            txtCompShortName1.Text = "";
            //   txtUOM.Text = "";
            txtProdName.Text = "";
            txtPackType1.Text = "";
            txtPack1.Text = "";
            txtIsDataOK.Text = "Y";
        }
        private void txtProdName_EnterKeyPressed(object sender, EventArgs e)
        {
            if (txtProdName.Text != "")
            {
                this.ActiveControl = mcbCompany1;
                mcbCompany1.Focus();
            }
            else
            {
                txtProdName.Focus();
            }
        }
        private void mcbCompany1_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbCompany1.SeletedItem != null && string.IsNullOrEmpty(Convert.ToString(mcbCompany1.SelectedID)) == false)
            {
                txtCompShortName1.Text = mcbCompany1.SeletedItem.ItemData[2].ToString();
                this.ActiveControl = txtCompShortName1;
                txtCompShortName1.Focus();
            }
            else
                mcbCompany1.Focus();
        }
        private void mcbCompany1_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbCompany1.SelectedID;
            if (mcbCompany1.SelectedID != null && mcbCompany1.SelectedID != "")
            {
                FillCompanyCombo();
                mcbCompany1.SelectedID = selectedId;
                // _Product.ProdCompShortName = mcbCompany1.SeletedItem.ItemData[2];
                txtCompShortName1.Text = mcbCompany1.SeletedItem.ItemData[2];
                //if (mcbCompany1.SeletedItem.ItemData[3] != null && mcbCompany1.SeletedItem.ItemData[3].ToString() != string.Empty)
                //{
                //    _Product.ProdCreditor1ID = mcbCompany1.SeletedItem.ItemData[3].ToString();
                //    mcbFirstCreditor.SelectedID = _Product.ProdCreditor1ID;
                //}

                //if (mcbCompany1.SeletedItem.ItemData[4] != null && mcbCompany1.SeletedItem.ItemData[4].ToString() != string.Empty)
                //{
                //    _Product.ProdCreditor2ID = mcbCompany1.SeletedItem.ItemData[4].ToString();
                //    mcbSecondCreditor.SelectedID = _Product.ProdCreditor2ID;
                //}
                // txtCompShortName.Focus();
            }
        }

        private void txtCompShortName1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && txtCompShortName1.Text != "") // [14.11.2016]
            {
                mcbCompany1.Focus();
            }
            else if (e.KeyCode == Keys.Up)//kiran
            {
                mcbCompany1.Focus();
            }
            else
            {
                txtCompShortName1.Focus();
            }
        }

        //private void txtUOM_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)  // [14.11.2016]
        //    {
        //        if (txtUOM.Text != "")
        //        {
        //            // this.ActiveControl = mcbGenCatOpStock;
        //            // mcbGenCatOpStock.Focus();
        //            this.ActiveControl = txtPack1;
        //            txtPack1.Focus();
        //        }
        //        else
        //        {
        //            txtUOM.Focus();
        //        }
        //    }
        //    else if (e.KeyCode == Keys.Up) //kiran
        //    {
        //        mcbGenCatOpStock.Focus();
        //        mcbGenCatOpStock.Select();
        //    }
        //}
        private void txtPack1_EnterKeyPressed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtPack1.Text)) == false)
            {
                this.ActiveControl = mcbGenCatOpStock;
                mcbGenCatOpStock.Focus();
                //this.ActiveControl = txtPackType1;
                //txtPackType1.Focus();
            }
            else
            { txtPack1.Focus(); }
        }

        private void mcbGenCatOpStock_EnterKeyPressed(object sender, EventArgs e)
        {

            this.ActiveControl = mcbProductCategory1;
            mcbProductCategory1.Focus();
        }
        private void txtPackType1_EnterKeyPressed(object sender, EventArgs e)  // [ansuman]
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtPackType1.Text)) == false)
            {
                this.ActiveControl = mcbShelfNoOpStock;
                mcbShelfNoOpStock.Focus();
            }
            else
            {
                txtPackType1.Focus();
            }

        }

        private void mcbProductCategory1_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbProductCategory1.SeletedItem != null && mcbProductCategory1.SeletedItem.ItemData[0].ToString() != "")  // [14.11.2016]
            {
                this.ActiveControl = txtPackType1;
                txtPackType1.Focus();
            }
            else
            {
                mcbProductCategory1.Focus();
            }
        }
        private void mcbShelfNoOpStock_EnterKeyPressed(object sender, EventArgs e)
        {
            //  if (mcbShelfNoOpStock.SeletedItem != null && string.IsNullOrEmpty(Convert.ToString(mcbShelfNoOpStock.SelectedID)) == false)
            {
                this.ActiveControl = mcbSchedule1;
                mcbSchedule1.Focus();
            }
            //else
            //{
            //    this.ActiveControl = mcbShelfNoOpStock;
            //    mcbShelfNoOpStock.Focus();
            //}
        }

        private void mcbSchedule1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (mcbSchedule1.SelectedItem != null && string.IsNullOrEmpty(Convert.ToString(mcbSchedule1.SelectedItem)) == false)
                {
                    this.ActiveControl = txtIsDataOK;
                    txtIsDataOK.Focus();
                }
                else
                {
                    this.ActiveControl = mcbSchedule1;
                    mcbSchedule1.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up)//kiran
            {
                mcbShelfNoOpStock.Focus();
            }
        }
        private void txtIsDataOK_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && txtIsDataOK.Text == "Y")
                {
                    pobj.Name = txtProdName.Text.Trim();
                    pobj.Name = (pobj.Name.Replace("*", "X"));
                    pobj.Name = (pobj.Name.Replace("%", "Per"));
                    pobj.ProdCompID = Convert.ToInt32(mcbCompany1.SeletedItem.ItemData[0].ToString());
                    pobj.ProdCompShortName = mcbCompany1.SeletedItem.ItemData[2];
                    if (mcbGenCatOpStock.SeletedItem != null)
                        pobj.ProdGenericID = Convert.ToInt32(mcbGenCatOpStock.SeletedItem.ItemData[0].ToString());
                    if (mcbProductCategory1.SeletedItem != null)
                        pobj.ProdProductCategoryID = Convert.ToInt32(mcbProductCategory1.SeletedItem.ItemData[0].ToString()); // [14.11.2016]
                                                                                                                              //   pobj.ProdLoosePack = Convert.ToInt32(txtUOM.Text);
                    pobj.ProdPackType = txtPackType1.Text;
                    pobj.ProdPack = txtPack1.Text;
                    if (mcbShelfNoOpStock.SeletedItem != null)
                    {
                        if (mcbShelfNoOpStock.SeletedItem.ItemData[0].ToString() != "")
                            pobj.ProdShelfID = Convert.ToInt32(mcbShelfNoOpStock.SeletedItem.ItemData[0].ToString());
                    }
                    else
                        pobj.ProdShelfID = 0;

                    if (mcbSchedule1.SelectedItem != null || mcbSchedule1.SelectedItem.ToString() != "")
                        pobj.ProdScheduleDrugCode = mcbSchedule1.SelectedItem.ToString();
                    else
                        pobj.ProdScheduleDrugCode = string.Empty;
                    bool isDataUpdate = SaveProductDetails();

                    if (isDataUpdate == false)
                    {
                        MessageBox.Show("Error while saving, Please product save again..");
                        txtProdName.Focus();
                    }
                    else
                    {
                        _purchaseMode = PurchaseMode.Edit;
                        pnlEditProduct.Enabled = false;
                        txtQuantity.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Up) //kiran
                {
                    mcbSchedule1.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private bool SaveProductDetails()
        {
            bool IsProductCreateOrUpdate = false;
            System.Text.StringBuilder _errorMessage;
            try
            {
                if (_purchaseMode == PurchaseMode.Add)
                {
                    pobj.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = pobj.Id;
                    pobj.CreatedBy = General.CurrentUser.Id;
                    pobj.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    pobj.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    pobj.IFEdit = "N";
                    pobj.Validate();
                    if (pobj.IsValid)
                        IsProductCreateOrUpdate = pobj.AddDetails();
                    else // Show Validation Messages
                    {
                        _errorMessage = new System.Text.StringBuilder();
                        _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                        foreach (string _message in pobj.ValidationMessages)
                        {
                            _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                        }
                        MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        IsProductCreateOrUpdate = false;
                    }
                    if (IsProductCreateOrUpdate)
                        FillBatchGrid();
                }
                else
                {
                    pobj.Id = _Purchase.ProductID.ToString();
                    pobj.IFEdit = "Y";
                    pobj.Validate();
                    if (pobj.IsValid)
                        IsProductCreateOrUpdate = pobj.UpdateDetails();
                    else // Show Validation Messages
                    {
                        _errorMessage = new System.Text.StringBuilder();
                        _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                        foreach (string _message in pobj.ValidationMessages)
                        {
                            _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                        }
                        MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        IsProductCreateOrUpdate = false;
                    }
                }

                if (IsProductCreateOrUpdate)
                {
                    if (mpMSVC.DataSourceMain != null)
                        mpMSVC.DataSourceMain = null;
                    CacheObject.Clear("cacheCounterSale");
                    BindMainProductGrid();
                }
                return IsProductCreateOrUpdate;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
                return IsProductCreateOrUpdate;
            }
        }

        //if (_purchaseMode == PurchaseMode.Add)
        //{
        //    pobj.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //    pobj.CreatedBy = General.CurrentUser.Id;
        //    pobj.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
        //    pobj.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
        //    pobj.AddDetails();
        //}
        //else
        //    pobj.UpdateDetails();
        //CacheObject.Clear("cacheCounterSale");
        //BindMainProductGrid();
        //}
        public void BindMainProductGrid()
        {
            Product prod = new Product();
            DataTable proddt = prod.GetOverviewData();
            //  DataTable dt = General.ProductList;
            mpMSVC.DataSource = proddt;
            mpMSVC.Bind();
        }

        #endregion ProductDetail

        #region UIEvents
        private void txtProfitPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                double pp = 0;
                if (txtProfitPercentage.Text != null && txtProfitPercentage.Text.ToString() != string.Empty)
                    pp = Convert.ToDouble(txtProfitPercentage.Text.ToString());
                if (pp > 0)
                {
                    double mtrate = 0;
                    mtrate = Convert.ToDouble(txtTradeRate.Text.ToString());
                    double msalerate = Math.Round(mtrate * pp / 100, 2);
                    txtSaleRate.Text = (mtrate + msalerate).ToString("#0.00");
                }
                if (General.CurrentSetting.MsetPurchaseChangeSaleRate == "Y")
                    txtSaleRate.Focus();
                else
                    btnOK.Focus();
                CalculateTotals();

            }
            else if (e.KeyCode == Keys.Up)
            {
                txtItemDiscountPer.Focus();
            }

        }
        private void mpMSVC_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnChallan_Click(object sender, EventArgs e)
        {
            btnChallanClick();
        }

        private void btnChallan_KeyDown(object sender, KeyEventArgs e)
        {
            btnChallanClick();
        }
        private void btnChallanClick()
        {

        }

        private void txtScanCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (_purchaseMode == PurchaseMode.Add)
                {
                    Purchase _Purchase = new Purchase();
                    DataRow dr = _Purchase.GetDuplicateBarcode(txtScanCode.Text);
                    if (dr != null)
                    {
                        PSMessageBox.Show("Invalid Barcode Entered", "Duplicate Barcode", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                        txtScanCode.Focus();
                        return;
                    }
                    else
                        mcbShelf.Focus();
                }
                else if (_purchaseMode == PurchaseMode.Edit)
                {
                    txtScanCode.Enabled = false;
                }
                else
                    mcbShelf.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtCSTPer.Focus();
            }
        }

        private void mcbShelf_EnterKeyPressed(object sender, EventArgs e)
        {
            btnOK.Focus();
        }

        private void mcbShelf_UpArrowPressed(object sender, EventArgs e)
        {
            txtScanCode.Focus();
        }

        private void txtMRP_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtMRP.Text) == 0)
            {
                txtMRP.Focus();
            }
        }

        private void txtTradeRate_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtTradeRate.Text) == 0)
            {
                txtTradeRate.Focus();
            }
        }


        //private void cbTransactionType_Enter_1(object sender, EventArgs e)
        //{
        //    pnlType.BackColor = Color.Blue;
        //}
        //private void cbTransactionType_Leave_1(object sender, EventArgs e)
        //{
        //    pnlType.BackColor = Color.White;
        //}

        private void btnSummary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && mpMSVC.Rows.Count > 0)
            {
                mpMSVC.Focus();
                mpMSVC.SetFocus(1);
            }
        }

        private void mcbCreditor_UpArrowPressed(object sender, EventArgs e)
        {
            cbTransactionType.Focus();
        }
        private void txtSplDiscountS_TextChanged(object sender, EventArgs e)
        {
            txtSpecialDiscountAmountS.Text = txtSplDiscountS.Text;
        }

        private void txtPendingScheme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSchemeAmount.Focus();
            else if (e.KeyCode == Keys.Up)
                txtSchemeQuantity.Focus();
        }

        private void cbRound_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    MainToolStrip.Select();
                    tsBtnSave.Select();
                    //IsSetSaveInvoice = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion UIEvents

        #region TimerDate

        private void SetDateStatus()
        {
            DateTimer.Interval = 1000;

            DateTime _MDate = datePickerBillDate.Value.Date;
            DateTime _CDate = DateTime.Now.Date;
            int result = DateTime.Compare(_MDate, _CDate);
            if (result < 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You are working in Previous date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                DateTimer.Enabled = true;
                DateTimer.Start();
            }
            else if (result == 0)
            {
                lblmsg.Visible = false;
                lblmsg.Text = ""; // "You are working in Current date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                DateTimer.Enabled = false;
                DateTimer.Stop();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You are working in Next date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                DateTimer.Enabled = true;
                DateTimer.Start();
            }
        }
        void DateTimer_Tick(object sender, EventArgs e)
        {
            if (lblmsg.BackColor == Color.Gainsboro)
                lblmsg.BackColor = Color.Red;
            else
                lblmsg.BackColor = Color.Gainsboro;
        }
        private void datePickerBillDate_ValueChanged(object sender, EventArgs e)
        {
            SetDateStatus();
        }



        #endregion TimerDate    


        private void txtEnterScheme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string ifs = "N";
                if (txtEnterScheme.Text != null)
                    ifs = txtEnterScheme.Text.ToString();
                if (ifs.ToUpper() == "Y")
                    datePickerStartDate.Focus();
                else
                {
                    pnlEnterScheme.Visible = false;
                    txtSchemeAmount.Focus();
                }
            }
        }

        private void datePickerStartDate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    datePickerClosingDate.Focus();
                    break;
                case Keys.Up:
                    txtSchemeProduct.Focus();
                    break;
            }
        }
        private void datePickerClosingDate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtQuantity1.Focus();
                    break;
                case Keys.Up:
                    datePickerStartDate.Focus();
                    break;
            }
        }

        private void txtQuantity1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtScheme1.Focus();
                    break;
                case Keys.Up:
                    datePickerClosingDate.Focus();
                    break;
            }
        }

        private void txtScheme1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtQuantity2.Focus();
                    break;
                case Keys.Up:
                    txtQuantity1.Focus();
                    break;
            }
        }

        private void txtQuantity2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtScheme2.Focus();
                    break;
                case Keys.Up:
                    txtScheme1.Focus();
                    break;
            }

        }

        private void txtScheme2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtQuantity3.Focus();
                    break;
                case Keys.Up:
                    txtQuantity2.Focus();
                    break;
            }
        }

        private void txtQuantity3_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtScheme3.Focus();
                    break;
                case Keys.Up:
                    txtScheme2.Focus();
                    break;
            }
        }

        private void txtScheme3_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    txtQuantity3.Focus();
                    break;
                case Keys.Enter:
                    btnOKEnterScheme.Focus();
                    break;
            }
        }

        private void txtScheme1_Validating(object sender, CancelEventArgs e)
        {
            if (txtScheme1.Text != null && txtScheme1.Text != "" && Convert.ToInt32(txtScheme1.Text.ToString()) < 0)
            {
                txtScheme1.Text = "";
                txtScheme1.Focus();
            }

        }

        private void btnOKEnterScheme_Click(object sender, EventArgs e)
        {
            btnOKEnterSchemeClick();
        }
        private void btnOKEnterSchemeClick()
        {
            _Scheme.Quantity1 = 0;
            _Scheme.Quantity2 = 0;
            _Scheme.Quantity3 = 0;
            _Scheme.Scheme1 = 0;
            _Scheme.Scheme2 = 0;
            _Scheme.Scheme3 = 0;
            _Scheme.StartDate = "";
            _Scheme.ClosingDate = "";
            if (txtQuantity1.Text != null && txtQuantity1.Text.ToString() != string.Empty)
                _Scheme.Quantity1 = Convert.ToInt32(txtQuantity1.Text.ToString());
            if (txtQuantity2.Text != null && txtQuantity2.Text.ToString() != string.Empty)
                _Scheme.Quantity2 = Convert.ToInt32(txtQuantity2.Text.ToString());
            if (txtQuantity3.Text != null && txtQuantity3.Text.ToString() != string.Empty)
                _Scheme.Quantity3 = Convert.ToInt32(txtQuantity3.Text.ToString());
            if (txtScheme1.Text != null && txtScheme1.Text.ToString() != string.Empty)
                _Scheme.Scheme1 = Convert.ToInt32(txtScheme1.Text.ToString());
            if (txtScheme2.Text != null && txtScheme2.Text.ToString() != string.Empty)
                _Scheme.Scheme2 = Convert.ToInt32(txtScheme2.Text.ToString());
            if (txtScheme3.Text != null && txtScheme3.Text.ToString() != string.Empty)
                _Scheme.Scheme3 = Convert.ToInt32(txtScheme3.Text.ToString());
            _Scheme.StartDate = datePickerStartDate.Value.Date.ToString("yyyyMMdd");
            _Scheme.ClosingDate = datePickerClosingDate.Value.Date.ToString("yyyyMMdd");
            if (ifnewscheme == "Y")
                _Scheme.AddDetails();
            else
                _Scheme.UpdateDetails();

            ClearSchemePnl();
            pnlEnterScheme.Visible = false;
            txtSchemeAmount.Focus();
        }
        private void ClearSchemePnl()
        {
            txtSchemeProduct.Text = "";
            txtSchemePack.Text = "";
            txtQuantity1.Text = "0";
            txtQuantity2.Text = "0";
            txtQuantity3.Text = "0";
            txtScheme1.Text = "0";
            txtScheme2.Text = "0";
            txtScheme3.Text = "0";

        }

        private void txtPTR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double ptr = 0;
                if (txtPTR.Text != null && txtPTR.Text.ToString() != string.Empty)
                    ptr = Convert.ToDouble(txtPTR.Text.ToString());
                if (ptr > 0)
                    txtSaleRate.Text = ptr.ToString("#0.00");
                if (txtSaleRate.Enabled == true)
                    txtSaleRate.Focus();
                else
                    btnOK.Focus();
                CalculatePurRateSaleRateAndAmount();
            }
            else if (e.KeyCode == Keys.Up)
                txtItemDiscountPer.Focus();


        }

        private void txtPTR_TextChanged(object sender, EventArgs e)
        {
            double ptr = 0;
            if (txtPTR.Text != null && txtPTR.Text.ToString() != string.Empty)
                ptr = Convert.ToDouble(txtPTR.Text.ToString());
            if (ptr > 0)
                txtSaleRate.Text = ptr.ToString("#0.00");
        }

        private void txtProfitPercentage_TextChanged(object sender, EventArgs e)
        {
            CalculatePurRateSaleRateAndAmount();
        }

        private void txtSaleRate_TextChanged(object sender, EventArgs e)
        {
            CalculatePurRateSaleRateAndAmount();
        }
    }
}