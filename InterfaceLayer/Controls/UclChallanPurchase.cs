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
using PharmaSYSDistributorPlus.InterfaceLayer;
using PaperlessPharmaRetail.Common.Classes;
using PharmaSYSDistributorPlus.DataLayer;

namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclChallanPurchase : BaseControl
    {
        public enum ChallanPurchaseMode
        {
            Add = 0,
            Edit = 1,
        }

        #region Declaration

        private ChallanPurchase _ChallanPurchase;
        private DataTable _BindingSource;
        private string IfEditPreviousRow = "N";
        private string _LastStockID;
        //private string purchaseType;
        private string deletedproductname = "";
        private BaseControl ViewControl = null;
        private Form frmView;
        private string _preID = "";
        //   private bool pnltempoff = false;
        //  private bool IFpnlTempwasvisible = false;

        private ImportBill _ImportBill;
        Form frmOpen;
        FormImportSaleBill _formImportAlliedSaleBill;
        public ChallanPurchaseMode _purchaseMode;
        public ChallanPurchaseMode purchaseMode
        {
            get { return _purchaseMode; }
            set { _purchaseMode = value; }
        }
        Company cobj = new Company();
        Product pobj = null;
        #endregion

        #region contructor
        public UclChallanPurchase()
        {
            InitializeComponent();
            _ChallanPurchase = new ChallanPurchase();
            SearchControl = new UclChallanPurchaseSearch();
            _LastStockID = string.Empty;
            _ImportBill = null;
            pobj = new Product();
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

        # region IDetail Control
        public override void SetFocus()
        {
            if (_Mode == OperationMode.Add)
                mcbCreditor.Focus();
            else
                txtVouchernumber.Focus();
        }
        public override bool ClearData()
        {
            _ChallanPurchase.Initialise();
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
            headerLabel1.Text = "CHALLAN PURCHASE -> NEW";
            InitializeScreen();
            mcbCreditor.SelectedID = null;
            InitializeMainSubViewControl("");
            //  IFpnlTempwasvisible = false;
            mpMSVC.Enabled = true;
            FillShelfCombo();
            mpMSVC.BringToFront();
            FillCreditorCombo();
            btnSummary.Enabled = false;
            pnlProductDetail.Enabled = true;
            dgvBatchGrid.Visible = false;
            pnlSummary.Visible = false;
            if (General.CurrentSetting.MsetPurchaseRounding == "Y")
                cbRound.Checked = true;
            else
                cbRound.Checked = false;
            mcbCreditor.Enabled = true;
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
            mcbCreditor.Focus();
            return retValue;
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


            lblPurchaseBillFormat.Text = _ImportBill.PurchaseBillFormat;
            txtVouType.Text = _ImportBill.VoucherType;
            mcbCreditor.SelectedID = _ImportBill.DistributorID;

            txtVAT5AmountS.Text = _ImportBill.Vat5PerAmount;
            txtRoundUPS.Text = _ImportBill.RoundOFF;
            txtGridAmountTot.Text = _ImportBill.TotalAmount;
            txtTotalS.Text = _ImportBill.TotalAmount;
            txtBillAmountS.Text = _ImportBill.BillNetAmount;
            txtBillAmount.Text = _ImportBill.BillNetAmount;
            //    txtDBAmountS.Text = _ImportBill.DebitAmount.ToString("#0.00");
            if (mpMSVC.Rows.Count > 0)
                mpMSVC.Rows.Clear();

            _ChallanPurchase.SavePartyAIOCDACodeInMasterAccount(_ImportBill.DistributorID, _ImportBill.AIOCDACode, _ImportBill.DistributorCode);

            tsBtnSave.Enabled = false;
            btnSummary.Enabled = true;

            try
            {
                foreach (DataGridViewRow dr in _ImportBill.SaleBillData.Rows)
                {
                    _ChallanPurchase.ProductID = dr.Cells["Col_ProductID"].Value.ToString();
                    if (_ChallanPurchase.ProductID != null && _ChallanPurchase.ProductID != string.Empty)
                    {
                        DataRow proddr;
                        proddr = _ChallanPurchase.GetDetailsForProduct(_ChallanPurchase.ProductID);
                        int currow = mpMSVC.Rows.Add();
                        mpMSVC.Rows[currow].Cells["Col_ProductID"].Value = _ChallanPurchase.ProductID;
                        mpMSVC.Rows[currow].Cells["Col_ProductName"].Value = proddr["ProdName"].ToString();
                        mpMSVC.Rows[currow].Cells["Col_ProductName"].ReadOnly = true;
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
                        mpMSVC.Rows[currow].Cells["Col_SplDiscountPer"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_SplDiscountAmount"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_VATAmountPurchase"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_CSTAmount"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_CSTPer"].Value = "0.00";
                        //mpMSVC.Rows[currow].Cells["Col_ItemSCMDiscountAmount"].Value = "0.00";
                        //mpMSVC.Rows[currow].Cells["Col_ItemSCMDiscountAmountPerUnit"].Value = "0.00";
                        mpMSVC.Rows[currow].Cells["Col_CSTPer"].Value = "0.00";
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
                        //  mpMSVC.Rows[currow].Cells["Col_DistributorSale"].Value = "0.00";
                    }
                }
                SaveProductintblbillimportlink();
                if (lblPurchaseBillFormat.Text.ToString() != string.Empty)
                    _ChallanPurchase.PurchaseBillFormat = lblPurchaseBillFormat.Text.ToString();
                else
                    _ChallanPurchase.PurchaseBillFormat = string.Empty;
                if (lblPurchaseBillFormat.Text != string.Empty)
                    _ChallanPurchase.SavePurchaseBillformat();
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
            string distributorproductID = string.Empty;
            string retailerproductID = string.Empty;
            foreach (DataGridViewRow dr in mpMSVC.Rows)
            {
                if (dr.Cells["Col_ProductID"].Value != null)
                    retailerproductID = dr.Cells["Col_ProductID"].Value.ToString();
                if (dr.Cells["Col_DistributorProductID"].Value.ToString() != string.Empty)
                    distributorproductID = dr.Cells["Col_DistributorProductID"].Value.ToString().Trim();
                if (distributorproductID != null && distributorproductID != string.Empty)
                {
                    guid = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _ChallanPurchase.SaveProductsintblbillimportlink(guid, _ImportBill.DistributorID, distributorproductID, retailerproductID);
                }
            }
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            //  ClearData();
            InitializeScreen();

            headerLabel1.Text = "CHALLAN PURCHASE -> EDIT";
            InitializeMainSubViewControl("");
            FillShelfCombo();
            FillCreditorCombo();
            //EnableDisable();

            return retValue;
        }

        private void EnableDisable()
        {
            tsBtnSave.Enabled = false;
            mcbCreditor.Enabled = false;
            pnlProductDetail.Enabled = true;
            txtNarration.Enabled = false;
            txtBillNumber.Enabled = false;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;

            if (General.CurrentSetting.MsetPurchaseChangeSaleRate == "Y")
                txtSaleRate.Enabled = true;
            else
                txtSaleRate.Enabled = false;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            pnlProductDetail.Visible = false;
            pnlSummary.Visible = false;
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {
                mpMSVC.Rows.Add();
            }
            return retValue;
        }

        public override bool Exit()
        {
            bool retValue = false;
            //  if (((_Mode == OperationMode.Add || _Mode == OperationMode.Edit) && (mpMSVC.Rows.Count-1) >= 3) && (_Mode != OperationMode.Add || _Mode != OperationMode.Edit))
            if ((_Mode == OperationMode.Add ) && mpMSVC.Rows.Count > 1)
            {
                PSDialogResult result;
                result = PSMessageBox.Show("Save Or Remove All Invoices..", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
            }
            else
            {
                retValue = base.Exit();
                System.IO.File.Delete(General.GetPurchaseTempFile());
                _ImportBill = null;
            }
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            InitializeScreen();
            headerLabel1.Text = "CHALLAN PURCHASE -> DELETE";
            ClearData();
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            txtVouchernumber.Focus();
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_ChallanPurchase.AmountClearS != 0)
                MessageBox.Show("Payment Done", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (MessageBox.Show("Are you sure you want to delete Purchase information?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BindTempGrid();
                    bool canbedeleted = CheckStockForDelete();
                    if (canbedeleted)
                    {
                        LockTable.LockTablesForChallanPurchase();
                        retValue = _ChallanPurchase.DeleteDetails();
                        retValue = _ChallanPurchase.DeletePreviousRecords();
                        //  retValue = _ChallanPurchase.DeleteAccountDetails();
                        ReducePreviousStock();
                        //   clearPreviousdebitcreditnotes();
                        LockTable.UnLockTables();
                        //  UpdateClosingStockinCache();
                        retValue = true;
                        MessageBox.Show("Successfully Deleted", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //  _ChallanPurchase.AddDeletedDetails();
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
                headerLabel1.Text = "CHALLAN PURCHASE -> VIEW";
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
                _ChallanPurchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                _ChallanPurchase.VoucherSubType = "1";
                _ChallanPurchase.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                _ChallanPurchase.GetLastRecordForPurchase(_ChallanPurchase.VoucherType, _ChallanPurchase.VoucherSubType, _ChallanPurchase.VoucherSeries);
                FillSearchData(_ChallanPurchase.Id, "");
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
            _ChallanPurchase.VoucherType = txtVouType.Text.ToString();
            _ChallanPurchase.VoucherSubType = "1";
            dr = _ChallanPurchase.GetFirstRecord();
            if (dr != null && dr["PurchaseID"] != DBNull.Value)
            {
                _ChallanPurchase.Id = dr["PurchaseID"].ToString();
                FillSearchData(_ChallanPurchase.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            //   DataRow dr = null;
            _ChallanPurchase.VoucherType = txtVouType.Text.ToString();
            _ChallanPurchase.VoucherSubType = "1";
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _ChallanPurchase.VoucherType = txtVouType.Text.ToString();
            _ChallanPurchase.VoucherSubType = "1";
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _ChallanPurchase.VoucherSeries = txtVoucherSeries.Text.ToString();
            else
                _ChallanPurchase.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _ChallanPurchase.VoucherNumber = i;
                dr = _ChallanPurchase.ReadDetailsByVouNumber(i, _ChallanPurchase.VoucherType, _ChallanPurchase.VoucherSeries, _ChallanPurchase.VoucherSubType);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["PurchaseID"] != DBNull.Value)
            {
                _ChallanPurchase.Id = dr["PurchaseID"].ToString();
                FillSearchData(_ChallanPurchase.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _ChallanPurchase.GetLastVoucherNumber(_ChallanPurchase.VoucherType, _ChallanPurchase.VoucherSubType, _ChallanPurchase.VoucherSeries);
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _ChallanPurchase.VoucherType = txtVouType.Text.ToString();
            _ChallanPurchase.VoucherSubType = "1";
            if (txtVoucherSeries.Text == null || txtVoucherSeries.Text == string.Empty)
                _ChallanPurchase.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            else
                _ChallanPurchase.VoucherSeries = txtVoucherSeries.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _ChallanPurchase.VoucherNumber = i;
                dr = _ChallanPurchase.ReadDetailsByVouNumber(_ChallanPurchase.VoucherNumber, _ChallanPurchase.VoucherType, _ChallanPurchase.VoucherSeries, _ChallanPurchase.VoucherSubType);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["PurchaseID"] != DBNull.Value)
            {
                _ChallanPurchase.Id = dr["PurchaseID"].ToString();
                FillSearchData(_ChallanPurchase.Id, "");
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

                    txtVouType.Text = FixAccounts.VoucherTypeForChallanPurchase;
                    _ChallanPurchase.VoucherType = FixAccounts.VoucherTypeForChallanPurchase;
                    IfAdd();


                    if (_Mode == OperationMode.Edit)
                        _ChallanPurchase.IFEdit = "Y";
                    _ChallanPurchase.VoucherSubType = "1";
                    _ChallanPurchase.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        LockTable.LocktblVoucherNo();
                        _ChallanPurchase.VoucherNumber = _ChallanPurchase.GetAndUpdateChallanPurchaseNumber(_ChallanPurchase.VoucherType);
                    }
                    _ChallanPurchase.Validate();

                    if (_ChallanPurchase.IsValid)
                    {
                        try
                        {
                            LockTable.LockTablesForChallanPurchase();
                            if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                            {
                                General.BeginTransaction();

                                _ChallanPurchase.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                //_ChallanPurchase.VoucherNumber = _ChallanPurchase.GetAndUpdateChallanPurchaseNumber(_ChallanPurchase.VoucherType);
                                _ChallanPurchase.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                                txtVouchernumber.Text = _ChallanPurchase.VoucherNumber.ToString();
                                _ChallanPurchase.CreatedBy = General.CurrentUser.Id;
                                _ChallanPurchase.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _ChallanPurchase.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                                retValue = _ChallanPurchase.AddDetails();
                                _SavedID = _ChallanPurchase.Id;
                                if (retValue)
                                    retValue = SaveParticularsProductwise();
                                //if (retValue)
                                //{
                                //    _ChallanPurchase.AddAccountDetails();
                                //}

                                //if (retValue)
                                //{
                                //    if (_ChallanPurchase.IfCashPaid == "Y")
                                //    {
                                //        _ChallanPurchase.CBId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                //        CashPayment _csp = new CashPayment();
                                //        _ChallanPurchase.CBVouNo = _csp.GetAndUpdateCSPNumber(General.ShopDetail.ShopVoucherSeries);
                                //        _ChallanPurchase.CBVouType = FixAccounts.VoucherTypeForCashPayment;
                                //      //  retValue = _ChallanPurchase.AddCashEntry();
                                //    }
                                //    else if (_ChallanPurchase.ChequeNumber != "" && _ChallanPurchase.BankID != "")
                                //    {
                                //        _ChallanPurchase.CBId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                //        BankPayment _bkp = new BankPayment();
                                //        _ChallanPurchase.CBVouNo = _bkp.GetAndUpdateBKPNumber(General.ShopDetail.ShopVoucherSeries);
                                //        _ChallanPurchase.CBVouType = FixAccounts.VoucherTypeForBankPayment;
                                //     //   retValue = _ChallanPurchase.AddBankEntry();
                                //    }
                                //}
                                if (retValue)
                                    General.CommitTransaction();
                                else
                                    General.RollbackTransaction();
                                LockTable.UnLockTables();
                                if (retValue)
                                {
                                    // UpdateClosingStockinCache();
                                    System.IO.File.Delete(General.GetPurchaseTempFile());
                                    string msgLine2 = _ChallanPurchase.VoucherType + "  " + _ChallanPurchase.VoucherNumber.ToString("#0");
                                    PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                    _ImportBill = null;
                                    retValue = true;
                                    if (General.CurrentSetting.MsetScanBarCode == "Y")
                                    {
                                        DialogResult dResult;
                                        dResult = MessageBox.Show("Print Labels", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                        if (dResult == DialogResult.Yes)
                                        {
                                            PrintBarCodeLables();
                                        }
                                    }

                                }
                                else
                                {
                                    PSDialogResult result = PSMessageBox.Show("Could not Save Voucher...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                    retValue = false;
                                }
                            }
                            else if (_Mode == OperationMode.Edit)
                            {
                                DataTable stocktbl = new DataTable();
                                _ChallanPurchase.VoucherNumber = int.Parse(txtVouchernumber.Text);
                                General.BeginTransaction();
                                _ChallanPurchase.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                                _ChallanPurchase.ModifiedBy = General.CurrentUser.Id;
                                _ChallanPurchase.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _ChallanPurchase.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");

                                retValue = CheckStockForDeletedRows();
                                if (retValue)
                                {
                                    retValue = DeletePreviousRows();
                                    if (retValue)
                                        retValue = SaveParticularsProductwise();
                                    if (retValue)
                                        retValue = ReducePreviousStock();
                                    if (retValue)
                                        retValue = _ChallanPurchase.UpdateDetails();
                                    _SavedID = _ChallanPurchase.Id;

                                    if (retValue)
                                    {
                                        //retValue = DeletePreviousRows();
                                        //if (retValue)
                                        //    retValue = SaveParticularsProductwise();

                                        //    clearPreviousdebitcreditnotes();

                                        if (retValue)
                                        {

                                            //  retValue = _ChallanPurchase.DeleteAccountDetails();
                                            _ChallanPurchase.CreatedBy = _ChallanPurchase.ModifiedBy;
                                            _ChallanPurchase.CreatedDate = _ChallanPurchase.ModifiedDate;
                                            _ChallanPurchase.CreatedTime = _ChallanPurchase.ModifiedTime;
                                            //  retValue = _ChallanPurchase.AddAccountDetails();

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
                                                _ChallanPurchase.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                                //   _ChallanPurchase.AddChangedDetails();
                                                AddPreviousRowsInChangedDetail();
                                                MessageBox.Show("Information has been Updated successfully.'", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                        string mm = _ChallanPurchase.Name + " " + _ChallanPurchase.ProdLoosePack.ToString() + _ChallanPurchase.Pack + " - " + _ChallanPurchase.Batchno + " - " + _ChallanPurchase.MRP.ToString("0.00");
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
                        foreach (string _message in _ChallanPurchase.ValidationMessages)
                        {
                            _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                        }
                        MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            LockTable.UnLockTables();

            return retValue;
        }

        private void PrintBarCodeLables()
        {
            OpenBarCodeForm("Purchase", _ChallanPurchase.VoucherType, _ChallanPurchase.VoucherNumber.ToString(), new DataTable());
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

        public void IfAdd()
        {
            _ChallanPurchase.AmountItemDiscountS = Convert.ToDouble(txtItemDiscountS.Text.ToString());
            //if (txtSplDiscountS.Text.ToString() != "")
            //    _ChallanPurchase.AmountSpecialDiscountS = Convert.ToDouble(txtSplDiscountS.Text.ToString());
            //_ChallanPurchase.AmountSchemeDiscountS = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
            //if (txtCashDiscountPerS.Text != string.Empty)
            //    _ChallanPurchase.CashDiscountPercentageS = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
            //_ChallanPurchase.AmountCashDiscountS = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
            if (txtAddOnS.Text.ToString() != "")
                _ChallanPurchase.AmountAddOnFreightS = Convert.ToDouble(txtAddOnS.Text.ToString());
            if (txtLessS.Text.ToString() != "")
                _ChallanPurchase.AmountLessS = Convert.ToDouble(txtLessS.Text.ToString());
            //if (txtCRAmountS.Text.ToString() != "")
            //    _ChallanPurchase.AmountCreditNoteS = Convert.ToDouble(txtCRAmountS.Text.ToString());
            // _Challan*/Purchase.AmountDebitNoteS = Convert.ToDouble(txtDBAmountS.Text.ToString());
            if (txtOCTPerS.Text != "")
                _ChallanPurchase.OctroiPercentageS = Convert.ToDouble(txtOCTPerS.Text.ToString());
            if (txtOCTAmountS.Text != "")
                _ChallanPurchase.AmountOctroiS = Convert.ToDouble(txtOCTAmountS.Text.ToString());
            _ChallanPurchase.Narration = txtNarration.Text.ToString();
            _ChallanPurchase.RoundUpAmountS = Convert.ToDouble(txtRoundUPS.Text.ToString());
            _ChallanPurchase.AmountVAT5PercentS = Convert.ToDouble(txtVAT5AmountS.Text.ToString());
            _ChallanPurchase.AmountVAT12point5PercentS = Convert.ToDouble(txtVAT12Point5AmountS.Text.ToString());
            if (txtPurchaseAmountVATZeroS.Text != null && txtPurchaseAmountVATZeroS.Text != "")
                _ChallanPurchase.PurchaseAmountZeroVATS = Convert.ToDouble(txtPurchaseAmountVATZeroS.Text.ToString());
            //if (txtpuramount0.Text.ToString() != "")
            //    _ChallanPurchase.PurchaseAmount0PercentVATS = Convert.ToDouble(txtPurchaseAmountVATZeroS.Text.ToString());
            if (txtPurchaseAmountVAT5S.Text.ToString() != "")
                _ChallanPurchase.PurchaseAmount5PercentVATS = Convert.ToDouble(txtPurchaseAmountVAT5S.Text.ToString());
            if (txtPurchaseAmountVAT12point5S.Text.ToString() != "")
                _ChallanPurchase.PurchaseAmount12point5PercentVATS = Convert.ToDouble(txtPurchaseAmountVAT12point5S.Text.ToString());

        }
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _ChallanPurchase.Id = ID;
                    //if (Vmode == "C")
                    //    _ChallanPurchase.ReadDetailsByIDForChanged();
                    //else if (Vmode == "D")
                    //    _ChallanPurchase.ReadDetailsByIDForDeleted();
                    //else
                    _ChallanPurchase.ReadDetailsByID();
                    BindTempGrid();
                    InitializeMainSubViewControl(Vmode);

                    //if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
                    //{
                    //    int currentrow =  mpMSVC.Rows.Add();
                    //    mpMSVC.SetFocus(currentrow, 1);
                    //}
                    _ChallanPurchase.OldVoucherType = _ChallanPurchase.VoucherType;
                    _ChallanPurchase.OldVoucherNumber = _ChallanPurchase.VoucherNumber;
                    if (_ChallanPurchase.StatementNumber.ToString() != "" && _ChallanPurchase.StatementNumber > 0)
                        lblFooterMessage.Text = "Statement Number : " + _ChallanPurchase.StatementNumber.ToString();
                    else
                        lblFooterMessage.Text = "";
                    txtVoucherSeries.Text = _ChallanPurchase.VoucherSeries;
                    txtVouType.Text = _ChallanPurchase.VoucherType.ToString();
                    txtVouchernumber.Text = _ChallanPurchase.VoucherNumber.ToString();
                    txtBillNumber.Text = _ChallanPurchase.ChallanNumber;
                    txtNarration.Text = _ChallanPurchase.Narration;
                    txtCashDiscountAmountS.Text = _ChallanPurchase.AmountCashDiscountS.ToString("#0.00");
                    txtItemDiscountS.Text = _ChallanPurchase.AmountItemDiscountS.ToString("#0.00");
                    txtSchemeDiscountS.Text = _ChallanPurchase.AmountSchemeDiscountS.ToString("#0.00");
                    txtAddOnS.Text = _ChallanPurchase.AmountAddOnFreightS.ToString("#0.00");
                    txtLessS.Text = _ChallanPurchase.AmountLessS.ToString("#0.00");
                    //txtCRAmountS.Text = _ChallanPurchase.AmountCreditNoteS.ToString("#0.00");
                    //txtDBAmountS.Text = _ChallanPurchase.AmountDebitNoteS.ToString("#0.00");
                    txtOCTPerS.Text = _ChallanPurchase.OctroiPercentageS.ToString("#0.00");
                    txtOCTAmountS.Text = _ChallanPurchase.AmountOctroiS.ToString("#0.00");

                    txtVAT5AmountS.Text = _ChallanPurchase.AmountVAT5PercentS.ToString("#0.00");
                    txtVAT12Point5AmountS.Text = _ChallanPurchase.AmountVAT12point5PercentS.ToString("#0.00");
                    //txtViewVat5per.Text = _ChallanPurchase.AmountVAT5PercentS.ToString("#0.00");
                    //txtViewVat12point5per.Text = _ChallanPurchase.AmountVAT12point5PercentS.ToString("#0.00");
                    txtPurchaseAmountVATZeroS.Text = _ChallanPurchase.PurchaseAmountZeroVATS.ToString("#0.00");
                    txtPurchaseAmountVAT5S.Text = _ChallanPurchase.PurchaseAmount5PercentVATS.ToString("#0.00");
                    txtPurchaseAmountVAT12point5S.Text = _ChallanPurchase.PurchaseAmount12point5PercentVATS.ToString("#0.00");
                    DateTime mydate = new DateTime(Convert.ToInt32(_ChallanPurchase.VoucherDate.Substring(0, 4)), Convert.ToInt32(_ChallanPurchase.VoucherDate.Substring(4, 2)), Convert.ToInt32(_ChallanPurchase.VoucherDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    txtRoundUPS.Text = _ChallanPurchase.RoundUpAmountS.ToString("#0.00");
                    txtGridAmountTot.Text = _ChallanPurchase.AmountS.ToString("#0.00");
                    txtBillAmountS.Text = _ChallanPurchase.AmountS.ToString("#0.00");
                    txtBillAmount.ReadOnly = false;
                    txtBillAmount.Enabled = true;
                    txtBillAmount.Text = _ChallanPurchase.AmountNetS.ToString("#0.00");
                    txtNetAmountS.Text = _ChallanPurchase.AmountNetS.ToString("#0.00");
                    txtBillAmount.ReadOnly = true;
                    txtBillAmount.Enabled = false;

                    FillShelfCombo();
                    FillCreditorCombo();
                    btnSummary.Enabled = true;
                    dgvBatchGrid.Visible = false;
                    pnlSummary.Visible = false;

                    mcbCreditor.SelectedID = _ChallanPurchase.AccountID;
                    if (_Mode == OperationMode.ReportView)
                    {
                        tsBtnFifth.Visible = false;
                    }
                    if (_Mode == OperationMode.Fifth && _ChallanPurchase.StatementNumber == 0)
                    {
                        btnTypeChange.Visible = true;
                        cbNewTransactionType.Visible = true;
                        cbNewTransactionType.Enabled = false;
                        btnTypeChange.Enabled = true;
                        btnTypeChange.Focus();
                    }
                    if (_ChallanPurchase.StatementNumber > 0 || _Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                    {
                        //   pnlPaymentDetails.Enabled = false;
                        mpMSVC.IsAllowDelete = false;
                        mcbCreditor.Enabled = false;
                    }
                    else
                    {
                        mpMSVC.IsAllowDelete = true;
                        //   pnlPaymentDetails.Enabled = true;
                        pnlBillDetails.Enabled = true;
                        mcbCreditor.Enabled = true;
                        txtBillNumber.Enabled = true;
                        //if (_Mode !=OperationMode.Add)
                        //    mcbCreditor.Focus();

                    }

                    txtVouchernumber.Enabled = false;
                    txtVouchernumber.Enabled = true;
                    if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
                    {
                        int currentrow = mpMSVC.Rows.Add();
                        mpMSVC.SetFocus(currentrow, 1);
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
                if (General.CurrentSetting.MsetScanBarCode == "Y")
                    btnPrintBarCode.Visible = true;
                else
                    btnPrintBarCode.Visible = false;
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
                        _purchaseMode = ChallanPurchaseMode.Add;
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

                        if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value == null)  // [ansuman][11.1.2017]
                        {
                            _purchaseMode = ChallanPurchaseMode.Add;
                        }

                        if (_purchaseMode != ChallanPurchaseMode.Edit)
                        {
                            lblEditProductTitle.Text = "Add Product Details";
                            mcbCompany1.SelectedID = "";
                            mcbGenCatOpStock.SelectedID = "";
                            mcbProductCategory1.SelectedID = "";  // [14.11.2016] 
                            mcbShelfNoOpStock.SelectedID = mcbShelf.SelectedID;
                            //mcbShelfNoOpStock.SelectedID = "";
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
                        // datePickerBillDate.Focus();
                        //retValue = true;
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


                }
                if (keyPressed == Keys.I && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtDiscountPer.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.L && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtSaleRate.Focus();
                        retValue = true;
                    }

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
                    btnOK.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.P && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtPurchaseVATPer.Focus();
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
                    //if (pnlPaymentDetails.Visible)
                    //{
                    //    pnlPaymentDetails.Visible = false;
                    //    retValue = true;
                    //}
                    if (IfShortCutOpen == true)
                    {
                        tsBtnShortcuts.PerformClick();
                        retValue = true;
                    }
                    //else if (pnlTempPurchase.Visible)
                    //{
                    //    btnCancelTemPurchaseClick();
                    //    retValue = true;
                    //}
                    else if (dgvBatchGrid.Visible)
                    {
                        dgvBatchGrid.Visible = false;
                        pnlProductDetail.Enabled = true;
                        txtBatch.Focus();
                        retValue = true;
                    }
                    else if (pnlProductDetail.Visible && dgvBatchGrid.Visible == false)
                    {
                        btnCancelClick();
                        retValue = true;
                    }
                    else if (pnlSummary.Visible)
                    {
                        btnCancelSClick();
                        retValue = true;
                    }
                    else if (mpMSVC.VisibleProductGrid() == true) //kiran
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
                        _ChallanPurchase.ProductID = temprow.Cells["Temp_ProductID"].Value.ToString();
                        _ChallanPurchase.Batchno = temprow.Cells["Temp_BatchNumber"].Value.ToString();
                        if (temprow.Cells["Temp_MRP"].Value != null)
                            _ChallanPurchase.MRP = Convert.ToDouble(temprow.Cells["Temp_MRP"].Value.ToString());
                        if (temprow.Cells["Temp_Scheme"].Value != null)
                            _ChallanPurchase.SchemeQuanity = Convert.ToInt32(temprow.Cells["Temp_Scheme"].Value.ToString());
                        if (temprow.Cells["Temp_Replacement"].Value != null)
                            _ChallanPurchase.ReplacementQuantity = Convert.ToInt32(temprow.Cells["Temp_Replacement"].Value.ToString());
                        if (temprow.Cells["Temp_Quantity"].Value != null)
                            _ChallanPurchase.Quantity = Convert.ToInt32(temprow.Cells["Temp_Quantity"].Value.ToString());
                        if (temprow.Cells["Temp_StockID"].Value != null)
                            _ChallanPurchase.StockID = temprow.Cells["Temp_StockID"].Value.ToString();
                        mclosingstock = 0;
                        foreach (DataRow dr in stocktable.Rows)
                        {
                            if (dr["StockID"].ToString() == _ChallanPurchase.StockID)
                            {
                                //  if (dr["ProductID"].ToString() == _ChallanPurchase.ProductID && dr["BatchNumber"].ToString() == _ChallanPurchase.Batchno && Convert.ToDouble(dr["MRP"].ToString()) == _ChallanPurchase.MRP)
                                mclosingstock = Convert.ToInt32(dr["ClosingStock"].ToString());
                                break;
                            }

                        }
                        mclosingstock = mclosingstock - _ChallanPurchase.Quantity - _ChallanPurchase.SchemeQuanity - _ChallanPurchase.ReplacementQuantity;
                        prodqty = 0;
                        prodrepl = 0;
                        prodscm = 0;
                        foreach (DataGridViewRow prodrow in mpMSVC.Rows)
                        {
                            if (prodrow.Cells["Col_ProductName"].Value != null)
                            {
                                if (prodrow.Cells["Col_StockID"].Value != null && prodrow.Cells["Col_StockID"].Value.ToString() != "")
                                {
                                    if (prodrow.Cells["Col_StockID"].Value.ToString() == _ChallanPurchase.StockID)
                                    {
                                        //if (prodrow.Cells["Col_ProductID"].Value.ToString() == _ChallanPurchase.ProductID &&
                                        //    prodrow.Cells["Col_BatchNumber"].Value.ToString() == _ChallanPurchase.Batchno &&
                                        //   Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString()) == _ChallanPurchase.MRP)
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
                            _ChallanPurchase.StockID = temprow.Cells["Temp_StockID"].Value.ToString();
                            _ChallanPurchase.ProductID = temprow.Cells["Temp_ProductID"].Value.ToString();
                            _ChallanPurchase.Batchno = temprow.Cells["Temp_BatchNumber"].Value.ToString();
                            if (temprow.Cells["Temp_MRP"].Value != null)
                                _ChallanPurchase.MRP = Convert.ToDouble(temprow.Cells["Temp_MRP"].Value.ToString());
                            if (temprow.Cells["Temp_Scheme"].Value != null)
                                _ChallanPurchase.SchemeQuanity = Convert.ToInt32(temprow.Cells["Temp_Scheme"].Value.ToString());
                            if (temprow.Cells["Temp_Replacement"].Value != null)
                                _ChallanPurchase.ReplacementQuantity = Convert.ToInt32(temprow.Cells["Temp_Replacement"].Value.ToString());
                            if (temprow.Cells["Temp_Quantity"].Value != null)
                                _ChallanPurchase.Quantity = Convert.ToInt32(temprow.Cells["Temp_Quantity"].Value.ToString());
                            if (temprow.Cells["Temp_UnitOfMeasure"].Value != null)
                                _ChallanPurchase.ProdLoosePack = Convert.ToInt16(temprow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                            CurrentClosingStock = _ChallanPurchase.GetCurrentClosingStock(_ChallanPurchase.StockID);
                            if (CurrentClosingStock < (_ChallanPurchase.Quantity + _ChallanPurchase.SchemeQuanity + _ChallanPurchase.ReplacementQuantity))
                            {
                                deletedproductname = temprow.Cells["Temp_ProductName"].Value.ToString().Trim() + " " + temprow.Cells["Temp_UnitOfMeasure"].Value.ToString().Trim() + " " + temprow.Cells["Temp_Pack"].Value.ToString().Trim();
                                retValue = false;
                                break;
                            }
                            //dr = _ChallanPurchase.IFRecordFoundInStockTable();
                            //if (dr == null)
                            //{
                            //    retValue = false;
                            //    break;
                            //}
                            //else
                            //{

                            //    ReducePreviousStock();
                            //    _ChallanPurchase.DeleteAccountDetails();
                            //    _ChallanPurchase.DeletePreviousRecords();
                            //    _ChallanPurchase.DeleteDetails();

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
            //    bool IfRecordFound = false;
            _ChallanPurchase.SerialNumber = 0;
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
                        _ChallanPurchase.SerialNumber += 1;
                        _ChallanPurchase.ProductID = "";
                        _ChallanPurchase.Batchno = "";
                        _ChallanPurchase.ProdLoosePack = 0;
                        _ChallanPurchase.MRP = 0;
                        _ChallanPurchase.Expiry = "";
                        _ChallanPurchase.ExpiryDate = "";
                        _ChallanPurchase.TradeRate = 0;
                        _ChallanPurchase.PurchaseRate = 0;
                        _ChallanPurchase.SaleRate = 0;
                        _ChallanPurchase.SchemeQuanity = 0;
                        _ChallanPurchase.ReplacementQuantity = 0;
                        _ChallanPurchase.Quantity = 0;
                        _ChallanPurchase.PurchaseVATPercent = 0;
                        _ChallanPurchase.ProductVATPercent = 0;
                        _ChallanPurchase.ItemDiscountPercent = 0;
                        _ChallanPurchase.AmountItemDiscount = 0;
                        _ChallanPurchase.AmountSchemeDiscount = 0;
                        _ChallanPurchase.AmountCST = 0;
                        _ChallanPurchase.SplDiscountPercent = 0;
                        _ChallanPurchase.AmountSplDiscountPerUnit = 0;
                        _ChallanPurchase.AmountPurchaseVAT = 0;
                        _ChallanPurchase.AmountProductVAT = 0;
                        _ChallanPurchase.AmountZeroVAT = 0;
                        _ChallanPurchase.AmountCashDiscountPerUnit = 0;
                        _ChallanPurchase.StockID = "";
                        _ChallanPurchase.ShelfID = "";
                        _ChallanPurchase.ProductMargin = 0;
                        _ChallanPurchase.ProductMargin2 = 0;
                        _ChallanPurchase.PurScanCode = string.Empty;

                        _ChallanPurchase.DistributorSaleRate = 0;
                        _ChallanPurchase.DistributorSaleRatePercent = 0;

                        _ChallanPurchase.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _ChallanPurchase.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                        _ChallanPurchase.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        if (prodrow.Cells["Col_UnitOfMeasure"].Value != null)
                            _ChallanPurchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Col_UnitOfMeasure"].Value.ToString());
                        if (prodrow.Cells["Col_MRP"].Value != null)
                            _ChallanPurchase.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString());
                        if (prodrow.Cells["Col_Pack"].Value != null)
                            _ChallanPurchase.Pack = prodrow.Cells["Col_Pack"].Value.ToString();

                        if (prodrow.Cells["Col_Expiry"].Value != null)
                            _ChallanPurchase.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();

                        if (_ChallanPurchase.Expiry != "00/00")
                        {
                            _ChallanPurchase.ExpiryDate = General.GetValidExpiryDate(prodrow.Cells["Col_Expiry"].Value.ToString());
                        }
                        if (prodrow.Cells["Col_TradeRate"].Value != null)
                            _ChallanPurchase.TradeRate = Convert.ToDouble(prodrow.Cells["Col_TradeRate"].Value.ToString());
                        if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                            _ChallanPurchase.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurchaseRate"].Value.ToString());
                        if (prodrow.Cells["Col_SaleRate"].Value != null)
                            _ChallanPurchase.SaleRate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());
                        if (prodrow.Cells["Col_Scheme"].Value != null)
                            _ChallanPurchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Col_Scheme"].Value.ToString());
                        if (prodrow.Cells["Col_Replacement"].Value != null)
                            _ChallanPurchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Col_Replacement"].Value.ToString());
                        if (prodrow.Cells["Col_Quantity"].Value != null)
                            _ChallanPurchase.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        if (prodrow.Cells["Col_VAT"].Value != null && prodrow.Cells["Col_VAT"].Value.ToString() != "")
                            _ChallanPurchase.PurchaseVATPercent = Convert.ToDouble(prodrow.Cells["Col_VAT"].Value.ToString());
                        if (prodrow.Cells["Col_ProdVATPer"].Value != null)
                            _ChallanPurchase.ProductVATPercent = Convert.ToDouble(prodrow.Cells["Col_ProdVATPer"].Value.ToString());
                        if (prodrow.Cells["Col_ItemDiscountPer"].Value != null)
                            _ChallanPurchase.ItemDiscountPercent = Convert.ToDouble(prodrow.Cells["Col_ItemDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Col_ItemDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountItemDiscount = Convert.ToDouble(prodrow.Cells["Col_ItemDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_ItemSCMDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountSchemeDiscount = Convert.ToDouble(prodrow.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_CSTAmount"].Value != null)
                            _ChallanPurchase.AmountCST = Convert.ToDouble(prodrow.Cells["Col_CSTAmount"].Value.ToString());
                        if (prodrow.Cells["Col_SplDiscountPer"].Value != null)
                            _ChallanPurchase.SplDiscountPercent = Convert.ToDouble(prodrow.Cells["Col_SplDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Col_SplDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountSplDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Col_SplDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_ShelfID"].Value != null)
                            _ChallanPurchase.ShelfID = prodrow.Cells["Col_ShelfID"].Value.ToString();
                        if (prodrow.Cells["Col_VATAmountPurchase"].Value != null)
                            _ChallanPurchase.AmountPurchaseVAT = Convert.ToDouble(prodrow.Cells["Col_VATAmountPurchase"].Value.ToString());

                        if (prodrow.Cells["Col_VATAmountSale"].Value != null)
                            _ChallanPurchase.AmountProductVAT = Convert.ToDouble(prodrow.Cells["Col_VATAmountSale"].Value.ToString());
                        if (prodrow.Cells["Col_CashDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountCashDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Col_CashDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_Margin"].Value != null)
                            _ChallanPurchase.ProductMargin = Convert.ToDouble(prodrow.Cells["Col_Margin"].Value.ToString());

                        if (prodrow.Cells["Col_Margin2"].Value != null)
                            _ChallanPurchase.ProductMargin2 = Convert.ToDouble(prodrow.Cells["Col_Margin2"].Value.ToString());
                        if (_ChallanPurchase.ProductMargin < 0)
                            _ChallanPurchase.ProductMargin = 0;
                        if (_ChallanPurchase.ProductMargin2 < 0)
                            _ChallanPurchase.ProductMargin2 = 0;
                        if (prodrow.Cells["Col_StockID"].Value != null && prodrow.Cells["Col_StockID"].Value.ToString() != "")
                            _ChallanPurchase.StockID = prodrow.Cells["Col_StockID"].Value.ToString();

                        if (prodrow.Cells["Col_ScanCode"].Value != null && prodrow.Cells["Col_ScanCode"].Value.ToString() != "")
                            _ChallanPurchase.PurScanCode = prodrow.Cells["Col_ScanCode"].Value.ToString();
                        _ChallanPurchase.Name = prodrow.Cells["Col_ProductName"].Value.ToString();

                        if (prodrow.Cells["Col_DistributorSaleRate"].Value != null && prodrow.Cells["Col_DistributorSaleRate"].Value.ToString() != string.Empty)
                            _ChallanPurchase.DistributorSaleRate = Convert.ToDouble(prodrow.Cells["Col_DistributorSaleRate"].Value.ToString());
                        if (prodrow.Cells["Col_DistributorSaleRatePer"].Value != null && prodrow.Cells["Col_DistributorSaleRatePer"].Value.ToString() != string.Empty)
                            _ChallanPurchase.DistributorSaleRatePercent = Convert.ToDouble(prodrow.Cells["Col_DistributorSaleRatePer"].Value.ToString());

                        string expdt = "";
                        expdt = _ChallanPurchase.ExpiryDate;
                        if (expdt != "")
                        {
                            _ChallanPurchase.ExpiryDate = General.GetExpiryInyyyymmddForm(expdt);
                        }

                        ThisStockID = string.Empty;

                        ThisStockID = _ChallanPurchase.CheckForBatchMRPStockIDInStockTable();

                        if (ThisStockID == string.Empty)
                        {
                            ThisStockID = _ChallanPurchase.CheckForBatchMRPInStockTable();
                        }

                        if (ThisStockID == string.Empty)
                        {
                            if (prodrow.Cells["Col_IFTempPurchase"].Value != null && prodrow.Cells["Col_IFTempPurchase"].Value.ToString() == "Y")
                                _ChallanPurchase.StockID = prodrow.Cells["Col_StockID"].Value.ToString();

                            _ChallanPurchase.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            ThisStockID = _ChallanPurchase.StockID;
                            _ChallanPurchase.PurScanCode = _ChallanPurchase.GetScanGodeForCurrentBatch(_ChallanPurchase.ProductID);
                            //Create new scancode 
                            returnVal = _ChallanPurchase.AddProductDetailsInStockTable();
                        }
                        else
                        {

                            _ChallanPurchase.StockID = ThisStockID;
                            CurrentClosingStock = _ChallanPurchase.GetCurrentClosingStock(ThisStockID);
                            oldTempStock = 0;
                            if (_Mode == OperationMode.Edit)
                            {
                                oldTempStock = GetOldStockFromTempGrid(ThisStockID);
                            }
                            if (((CurrentClosingStock - (oldTempStock * _ChallanPurchase.ProdLoosePack) + ((_ChallanPurchase.Quantity + _ChallanPurchase.SchemeQuanity + _ChallanPurchase.ReplacementQuantity) * _ChallanPurchase.ProdLoosePack)) >= 0) || ((CurrentClosingStock - (oldTempStock * _ChallanPurchase.ProdLoosePack) + ((_ChallanPurchase.Quantity + _ChallanPurchase.SchemeQuanity + _ChallanPurchase.ReplacementQuantity) * _ChallanPurchase.ProdLoosePack)) <= 0 && General.CurrentSetting.MsetSaleAllowNegativeStock == "Y"))
                                returnVal = _ChallanPurchase.UpdatePurchaseIntblStock();
                            else
                            {
                                returnVal = false;
                                break;
                            }
                        }

                        if (returnVal)
                        {
                            returnVal = _ChallanPurchase.UpdatePurchaseOrder();
                            returnVal = _ChallanPurchase.UpdatePurchaseStockInMasterProduct();
                        }

                        else
                            break;
                        if (returnVal)
                        {
                            _ChallanPurchase.UpdateLastPurhcaseDataInMasterProduct();
                            _ChallanPurchase.RemoveFromShortList(_ChallanPurchase.ProductID);
                            _ChallanPurchase.GetFirstAndSecondCreditor(_ChallanPurchase.ProductID);
                            if (_ChallanPurchase.FirstCreditor != _ChallanPurchase.AccountID && _ChallanPurchase.SecondCreditor != _ChallanPurchase.AccountID)
                            {
                                if (_ChallanPurchase.FirstCreditor == string.Empty)
                                    _ChallanPurchase.FillFirstCreditorInMasterProduct();
                                else if (_ChallanPurchase.SecondCreditor == string.Empty)
                                    _ChallanPurchase.FillSecondCreditorInMasterProduct();
                            }

                        }
                        else
                            break;


                        if (returnVal)
                            returnVal = _ChallanPurchase.AddProductDetailsSS();
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

        //private bool clearPreviousdebitcreditnotes()
        //{
        //    bool retValue = true;
        //    retValue = _ChallanPurchase.clearPreviousdebitcreditnotes(_ChallanPurchase.Id);
        //    return retValue;
        //}

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
                _ChallanPurchase.AccountID = mcbCreditor.SelectedID;
                _account.ReadDetailsByID();
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[5];
                    //     _ChallanPurchase.GetPendingAmount(mcbCreditor.SelectedID);
                    _ChallanPurchase.GetOpeningBalance(mcbCreditor.SelectedID);
                    _ChallanPurchase.PendingAmount = _ChallanPurchase.OpeningBalance + (_ChallanPurchase.TotalDebit - _ChallanPurchase.TotalCredit);

                    _ChallanPurchase.PendingAmount = 0;
                    //  _ChallanPurchase.PendingAmount = _ChallanPurchase.GetDNAmount(mcbCreditor.SelectedID);


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

            if (General.CurrentSetting.MsetSaleAllowNegativeStock == "Y")
            {
                try
                {

                    _ChallanPurchase.MRP = 0;
                    double mmamt = 0;
                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                        _ChallanPurchase.MRP = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null)
                        mmamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString());
                    _ChallanPurchase.ProductID = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();

                    bool ifanyrow = false;


                    if (!ifanyrow)
                    {
                        mpMSVC.Enabled = false;
                        pnlBillDetails.Enabled = false;
                        //pnlProductDetail.BringToFront();
                        // pnlProductDetail.Location = GetpnlProductDetailLocation();
                        // pnlProductDetail.Visible = true;
                        if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _ChallanPurchase.StatementNumber > 0)
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
                        if (mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value == null || mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value.ToString() != "Y")
                        {
                            txtBatch.Enabled = true;
                            txtExpiry.Enabled = true;
                            txtMRP.Enabled = true;

                        }
                        else
                        {
                            txtBatch.Enabled = false;
                            txtExpiry.Enabled = false;
                            txtMRP.Enabled = false;
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
            else
            {
                try
                {

                    _ChallanPurchase.MRP = 0;
                    double mmamt = 0;
                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                        _ChallanPurchase.MRP = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null)
                        mmamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString());
                    _ChallanPurchase.ProductID = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                    mpMSVC.Enabled = false;
                    pnlBillDetails.Enabled = false;
                    pnlProductDetail.BringToFront();
                    pnlProductDetail.Location = GetpnlProductDetailLocation();
                    pnlProductDetail.Visible = true;
                    if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _ChallanPurchase.StatementNumber > 0)
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
                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value == null || mpMSVC.MainDataGridCurrentRow.Cells["Col_IFTempPurchase"].Value.ToString() != "Y")
                    {
                        txtBatch.Enabled = true;
                        txtExpiry.Enabled = true;
                        txtMRP.Enabled = true;
                    }
                    else
                    {
                        txtBatch.Enabled = false;
                        txtExpiry.Enabled = false;
                        txtMRP.Enabled = false;
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
            ShowProductDetailPanel();
        }

        private void ShowProductDetailPanel()
        {
            //FillLastPurchase();
            pnlBillDetails.Enabled = false;
            pnlProductDetail.BringToFront();
            pnlProductDetail.Location = GetpnlProductDetailLocation();
            pnlProductDetail.Visible = true;

            _purchaseMode = ChallanPurchaseMode.Edit;
            pnlEditProduct.BringToFront();
            pnlEditProduct.Visible = true;
            pnlEditProduct.Enabled = false;
            FillPnlEditProduct();
            FillProductAndCmpnyData(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString());
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
            try
            {
                pt.X = mpMSVC.Location.X + 305;
                pt.Y = mpMSVC.Location.Y - 28;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private Point GetpnlTempPurchaseLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlBillDetails.Location.X + 200;
                pt.Y = pnlBillDetails.Location.Y - 200;
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
                returnVal = _ChallanPurchase.DeletePreviousRecords();
            }
            catch { returnVal = false; }
            return returnVal;
        }
        private bool AddPreviousRowsInDeleteDetail()
        {
            bool returnVal = false;
            _ChallanPurchase.SerialNumber = 0;
            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null)
                    {
                        _ChallanPurchase.SerialNumber += 1;
                        _ChallanPurchase.ProductID = "";
                        _ChallanPurchase.Batchno = "";
                        _ChallanPurchase.ProdLoosePack = 0;
                        _ChallanPurchase.MRP = 0;
                        _ChallanPurchase.Expiry = "";
                        _ChallanPurchase.ExpiryDate = "";
                        _ChallanPurchase.TradeRate = 0;
                        _ChallanPurchase.PurchaseRate = 0;
                        _ChallanPurchase.SaleRate = 0;
                        _ChallanPurchase.SchemeQuanity = 0;
                        _ChallanPurchase.ReplacementQuantity = 0;
                        _ChallanPurchase.Quantity = 0;
                        _ChallanPurchase.PurchaseVATPercent = 0;
                        _ChallanPurchase.ProductVATPercent = 0;
                        _ChallanPurchase.ItemDiscountPercent = 0;
                        _ChallanPurchase.AmountItemDiscount = 0;
                        _ChallanPurchase.AmountSchemeDiscount = 0;
                        _ChallanPurchase.AmountCST = 0;
                        _ChallanPurchase.SplDiscountPercent = 0;
                        _ChallanPurchase.AmountSplDiscountPerUnit = 0;
                        _ChallanPurchase.AmountPurchaseVAT = 0;
                        _ChallanPurchase.AmountProductVAT = 0;
                        _ChallanPurchase.AmountZeroVAT = 0;
                        _ChallanPurchase.AmountCashDiscountPerUnit = 0;
                        _ChallanPurchase.StockID = "";
                        _ChallanPurchase.ProductMargin = 0;
                        _ChallanPurchase.ProductMargin2 = 0;

                        _ChallanPurchase.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _ChallanPurchase.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        _ChallanPurchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        if (prodrow.Cells["Temp_UnitOfMeasure"].Value != null)
                            _ChallanPurchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                        if (prodrow.Cells["Temp_MRP"].Value != null)
                            _ChallanPurchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value.ToString());
                        if (prodrow.Cells["Temp_Expiry"].Value != null)
                            _ChallanPurchase.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
                        if (_ChallanPurchase.Expiry != "00/00")
                            _ChallanPurchase.ExpiryDate = General.GetValidExpiryDate(prodrow.Cells["Temp_Expiry"].Value.ToString());
                        if (prodrow.Cells["Temp_TradeRate"].Value != null)
                            _ChallanPurchase.TradeRate = Convert.ToDouble(prodrow.Cells["Temp_TradeRate"].Value.ToString());
                        if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
                            _ChallanPurchase.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value.ToString());
                        if (prodrow.Cells["Temp_SaleRate"].Value != null)
                            _ChallanPurchase.SaleRate = Convert.ToDouble(prodrow.Cells["Temp_SaleRate"].Value.ToString());
                        if (prodrow.Cells["Temp_Scheme"].Value != null)
                            _ChallanPurchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value.ToString());
                        if (prodrow.Cells["Temp_Replacement"].Value != null)
                            _ChallanPurchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
                        if (prodrow.Cells["Temp_Quantity"].Value != null)
                            _ChallanPurchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value.ToString());
                        if (prodrow.Cells["Temp_VAT"].Value != null && prodrow.Cells["Temp_VAT"].Value.ToString() != "")
                            _ChallanPurchase.PurchaseVATPercent = Convert.ToDouble(prodrow.Cells["Temp_VAT"].Value.ToString());
                        if (prodrow.Cells["Temp_ProdVATPer"].Value != null)
                            _ChallanPurchase.ProductVATPercent = Convert.ToDouble(prodrow.Cells["Temp_ProdVATPer"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemDiscountPer"].Value != null)
                            _ChallanPurchase.ItemDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountItemDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountSchemeDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_CSTAmount"].Value != null)
                            _ChallanPurchase.AmountCST = Convert.ToDouble(prodrow.Cells["Temp_CSTAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_SplDiscountPer"].Value != null)
                            _ChallanPurchase.SplDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Temp_SplDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountSplDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountAmount"].Value.ToString());

                        if (prodrow.Cells["Temp_VATAmountPurchase"].Value != null)
                            _ChallanPurchase.AmountPurchaseVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountPurchase"].Value.ToString());

                        if (prodrow.Cells["Temp_VATAmountSale"].Value != null)
                            _ChallanPurchase.AmountProductVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountSale"].Value.ToString());
                        if (prodrow.Cells["Temp_CashDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountCashDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_CashDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_Margin"].Value != null)
                            _ChallanPurchase.ProductMargin = Convert.ToDouble(prodrow.Cells["Temp_Margin"].Value.ToString());

                        if (prodrow.Cells["Temp_Margin2"].Value != null)
                            _ChallanPurchase.ProductMargin2 = Convert.ToDouble(prodrow.Cells["Temp_Margin2"].Value.ToString());

                        if (prodrow.Cells["Temp_StockID"].Value != null)
                            _ChallanPurchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();

                        string expdt = "";
                        expdt = _ChallanPurchase.ExpiryDate;
                        if (expdt != "")
                        {
                            _ChallanPurchase.ExpiryDate = General.GetExpiryInyyyymmddForm(expdt);
                        }

                        //  returnVal = _ChallanPurchase.AddDeletedProductDetailsSS();

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
            _ChallanPurchase.SerialNumber = 0;
            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null)
                    {
                        _ChallanPurchase.SerialNumber += 1;
                        _ChallanPurchase.ProductID = "";
                        _ChallanPurchase.Batchno = "";
                        _ChallanPurchase.ProdLoosePack = 0;
                        _ChallanPurchase.MRP = 0;
                        _ChallanPurchase.Expiry = "";
                        _ChallanPurchase.ExpiryDate = "";
                        _ChallanPurchase.TradeRate = 0;
                        _ChallanPurchase.PurchaseRate = 0;
                        _ChallanPurchase.SaleRate = 0;
                        _ChallanPurchase.SchemeQuanity = 0;
                        _ChallanPurchase.ReplacementQuantity = 0;
                        _ChallanPurchase.Quantity = 0;
                        _ChallanPurchase.PurchaseVATPercent = 0;
                        _ChallanPurchase.ProductVATPercent = 0;
                        _ChallanPurchase.ItemDiscountPercent = 0;
                        _ChallanPurchase.AmountItemDiscount = 0;
                        _ChallanPurchase.AmountSchemeDiscount = 0;
                        _ChallanPurchase.AmountCST = 0;
                        _ChallanPurchase.SplDiscountPercent = 0;
                        _ChallanPurchase.AmountSplDiscountPerUnit = 0;
                        _ChallanPurchase.AmountPurchaseVAT = 0;
                        _ChallanPurchase.AmountProductVAT = 0;
                        _ChallanPurchase.AmountZeroVAT = 0;
                        _ChallanPurchase.AmountCashDiscountPerUnit = 0;
                        _ChallanPurchase.StockID = "";
                        _ChallanPurchase.ProductMargin = 0;
                        _ChallanPurchase.ProductMargin2 = 0;

                        _ChallanPurchase.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _ChallanPurchase.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        _ChallanPurchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        if (prodrow.Cells["Temp_UnitOfMeasure"].Value != null)
                            _ChallanPurchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                        if (prodrow.Cells["Temp_MRP"].Value != null)
                            _ChallanPurchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value.ToString());
                        if (prodrow.Cells["Temp_Expiry"].Value != null)
                            _ChallanPurchase.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
                        if (_ChallanPurchase.Expiry != "00/00")
                            _ChallanPurchase.ExpiryDate = General.GetValidExpiryDate(prodrow.Cells["Temp_Expiry"].Value.ToString());
                        if (prodrow.Cells["Temp_TradeRate"].Value != null)
                            _ChallanPurchase.TradeRate = Convert.ToDouble(prodrow.Cells["Temp_TradeRate"].Value.ToString());
                        if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
                            _ChallanPurchase.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value.ToString());
                        if (prodrow.Cells["Temp_SaleRate"].Value != null)
                            _ChallanPurchase.SaleRate = Convert.ToDouble(prodrow.Cells["Temp_SaleRate"].Value.ToString());
                        if (prodrow.Cells["Temp_Scheme"].Value != null)
                            _ChallanPurchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value.ToString());
                        if (prodrow.Cells["Temp_Replacement"].Value != null)
                            _ChallanPurchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
                        if (prodrow.Cells["Temp_Quantity"].Value != null)
                            _ChallanPurchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value.ToString());
                        if (prodrow.Cells["Temp_VAT"].Value != null && prodrow.Cells["Temp_VAT"].Value.ToString() != "")
                            _ChallanPurchase.PurchaseVATPercent = Convert.ToDouble(prodrow.Cells["Temp_VAT"].Value.ToString());
                        if (prodrow.Cells["Temp_ProdVATPer"].Value != null)
                            _ChallanPurchase.ProductVATPercent = Convert.ToDouble(prodrow.Cells["Temp_ProdVATPer"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemDiscountPer"].Value != null)
                            _ChallanPurchase.ItemDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountItemDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountSchemeDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_CSTAmount"].Value != null)
                            _ChallanPurchase.AmountCST = Convert.ToDouble(prodrow.Cells["Temp_CSTAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_SplDiscountPer"].Value != null)
                            _ChallanPurchase.SplDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Temp_SplDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountSplDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountAmount"].Value.ToString());

                        if (prodrow.Cells["Temp_VATAmountPurchase"].Value != null)
                            _ChallanPurchase.AmountPurchaseVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountPurchase"].Value.ToString());

                        if (prodrow.Cells["Temp_VATAmountSale"].Value != null)
                            _ChallanPurchase.AmountProductVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountSale"].Value.ToString());
                        if (prodrow.Cells["Temp_CashDiscountAmount"].Value != null)
                            _ChallanPurchase.AmountCashDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_CashDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Temp_Margin"].Value != null)
                            _ChallanPurchase.ProductMargin = Convert.ToDouble(prodrow.Cells["Temp_Margin"].Value.ToString());

                        if (prodrow.Cells["Temp_Margin2"].Value != null)
                            _ChallanPurchase.ProductMargin2 = Convert.ToDouble(prodrow.Cells["Temp_Margin2"].Value.ToString());

                        if (prodrow.Cells["Temp_StockID"].Value != null)
                            _ChallanPurchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();

                        string expdt = "";
                        expdt = _ChallanPurchase.ExpiryDate;
                        if (expdt != "")
                        {
                            _ChallanPurchase.ExpiryDate = General.GetExpiryInyyyymmddForm(expdt);
                        }

                        //   returnVal = _ChallanPurchase.AddChangedProductDetailsSS();

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
                        _ChallanPurchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        _ChallanPurchase.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        _ChallanPurchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _ChallanPurchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _ChallanPurchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _ChallanPurchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value);
                        _ChallanPurchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
                        _ChallanPurchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                        _ChallanPurchase.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value.ToString());
                        DataRow dr = _ChallanPurchase.IfStockIDFoundInStockTable(_ChallanPurchase.StockID);
                        if (dr != null)
                            returnVal = _ChallanPurchase.UpdatePurchaseIntblStockReduceFromTemp();
                        returnVal = _ChallanPurchase.UpdatePurchaseStockInmasterProductReduceFromTemp();

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
                        _ChallanPurchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        deletedproductname = prodrow.Cells["Temp_ProductName"].Value.ToString().Trim() + " " + prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString().Trim() + " " + prodrow.Cells["Temp_Pack"].Value.ToString().Trim();
                        //_ChallanPurchase.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        //_ChallanPurchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        //_ChallanPurchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _ChallanPurchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _ChallanPurchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value);
                        _ChallanPurchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
                        _ChallanPurchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                        string ifmatchfound = "N";
                        foreach (DataGridViewRow gridrow in mpMSVC.Rows)
                        {
                            gridstockid = "";
                            if (gridrow.Cells["Col_StockID"].Value != null && gridrow.Cells["Col_StockID"].Value.ToString() != "")
                                gridstockid = gridrow.Cells["Col_StockID"].Value.ToString();
                            if (_ChallanPurchase.StockID == gridstockid)
                            {
                                deletedproductname = "";
                                ifmatchfound = "Y";
                                break;
                            }

                        }
                        if (ifmatchfound == "N")
                        {
                            CurrentClosingStock = _ChallanPurchase.GetCurrentClosingStock(_ChallanPurchase.StockID);
                            if (CurrentClosingStock < (_ChallanPurchase.Quantity + _ChallanPurchase.SchemeQuanity + _ChallanPurchase.ReplacementQuantity) * _ChallanPurchase.ProdLoosePack)
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
            mpMSVC.ColumnsMain.Clear();
            try
            {
                DataGridViewTextBoxColumn column;

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

        //private void ConstructPurchaseOrder()
        //{
        //    dgPurchaseOrder.Columns.Clear();
        //    try
        //    {
        //        DataGridViewTextBoxColumn column;
        //        //0
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_DSLID";
        //        column.HeaderText = "VouSeries";
        //        column.Width = 50;
        //        column.Visible = false;
        //        column.ReadOnly = true;
        //        dgPurchaseOrder.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Check";
        //        column.HeaderText = " ";
        //        column.Width = 15;
        //        dgPurchaseOrder.Columns.Add(column);

        //        //2
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherSeries";
        //        column.HeaderText = "Type";
        //        column.Width = 50;
        //        column.ReadOnly = true;
        //        dgPurchaseOrder.Columns.Add(column);
        //        //2
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherType";
        //        column.DataPropertyName = "VoucherType";
        //        column.HeaderText = "Type";
        //        column.Width = 50;
        //        column.ReadOnly = true;
        //        dgPurchaseOrder.Columns.Add(column);
        //        //3 
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherNumber";
        //        column.HeaderText = "Number";
        //        column.ReadOnly = true;
        //        column.Width = 50;
        //        dgPurchaseOrder.Columns.Add(column);
        //        //4
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherDate";
        //        column.HeaderText = "Date";
        //        column.Width = 80;
        //        column.ReadOnly = true;
        //        dgPurchaseOrder.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Amount";
        //        column.HeaderText = "Amount";
        //        column.Width = 120;
        //        column.ReadOnly = true;
        //        dgPurchaseOrder.Columns.Add(column);
        //        //5               
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Acc";
        //        column.HeaderText = "a1";
        //        column.Width = 50;
        //        column.Visible = false;
        //        dgPurchaseOrder.Columns.Add(column);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
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
                ConstructBarCodeColumns();

                DataTable dtable = new DataTable();
                if (vmode == "C")
                {
                    dtable = _ChallanPurchase.ReadProductDetailsByIDForChanged();
                    headerLabel1.Text = "Debtor Sale => Changed Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else if (vmode == "D")
                {
                    dtable = _ChallanPurchase.ReadProductDetailsByIDForDeleted();
                    headerLabel1.Text = "Debtor Sale => Deleted Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else
                    dtable = _ChallanPurchase.ReadProductDetailsByID();

                if (dtable != null)
                    _ChallanPurchase.NoofRows = dtable.Rows.Count;

                psLableWithBorder1.Text = _ChallanPurchase.NoofRows.ToString();
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
                tmptable = _ChallanPurchase.ReadProductDetailsByID();
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
            tsBtnPrint.Visible = false;
            dgBills.Visible = false;
            try
            {

                if (General.ShopDetail.ShopDistributorSale == "Y")
                {
                    lblDistPercent.Visible = true;
                    txtDistRatePercent.Visible = true;
                    txtDistSaleRate.Visible = true;
                }
                else
                {
                    lblDistPercent.Visible = false;
                    txtDistRatePercent.Visible = false;
                    txtDistSaleRate.Visible = false;
                }
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
                pnlSummary.Visible = false;
                lblPurchaseBillFormat.Text = string.Empty;
                btnSummary.BackColor = Color.Linen;
                txtVouchernumber.Clear();
                tsBtnSavenPrint.Enabled = false;
                txtExpiredDays.Clear();
                txtExpiry.BackColor = Color.White;
                txtBillNumber.Clear();
                txtNarration.Text = "";
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtQuantity.Text = "0";
                txtSchemeQuantity.Text = "0";
                txtReplacement.Text = "0";
                txtBillAmountS.Text = "0.00";
                txtSchemeDiscountS.Text = "0.00";
                txtItemDiscountS.Text = "0.00";

                txtSplDiscountPerUnit.Text = "";

                txtAddOnS.Text = "0.00";
                //txtCRAmountS.Text = "0.00";
                //txtDBAmountS.Text = "0.00";

                txtCashDiscountAmountS.Text = "0.00";

                txtVAT5AmountS.Text = "0.00";
                txtVAT12Point5AmountS.Text = "0.00";


                txtTotalVATAmountS.Text = "0.00";
                txtOCTPerS.Text = "0.00";
                txtOCTAmountS.Text = "0.00";
                txtPurchaseRate.Text = "0.00";
                txtPurchaseVATAmt.Text = "0.00";
                txtDiscountPer.Text = "0.00";
                txtDiscountAmt.Text = "0.00";
                txtRoundUPS.Text = "0.00";
                txtMasterVATAmt.Text = "0.00";
                txtMasterVATPer.Text = "0.00";
                txtBillAmount.Text = "0.00";
                txtpuramount12point5.Text = "0.00";
                txtpuramount5.Text = "0.00";
                txtpuramount0.Text = "0.00";
                txtOCTPerS.Text = "0.00";
                mcbCreditor.SelectedID = "";
                txtStockID.Text = "";
                txtGridAmountTot.Text = "0.00";
                pnlBillDetails.Enabled = true;
                pnlVou.Enabled = true;
                mpMSVC.Rows.Clear();
                psLableWithBorder1.Text = "";
                mpMSVC.Enabled = true;
                //    dgvLastPurchase.Visible = false;
                lblFooterMessage.Text = "";
                btnTypeChange.Visible = false;
                cbNewTransactionType.Visible = false;

                DataTable dtp = new DataTable();

                if (General.CurrentSetting.MsetScanBarCode == "Y")
                {
                    if (_Mode == OperationMode.View)
                        btnPrintBarCode.Visible = true;
                    else
                        btnPrintBarCode.Visible = false;
                }
                else
                    btnPrintBarCode.Visible = false;
                _ChallanPurchase.VoucherSubType = "1";



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
                    _ChallanPurchase.MRP = mmrpn;
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

                if (dr["ProdLastPurchaseDistributorSaleRatePer"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseDistributorSaleRatePer"].ToString(), out mdistrateper);
                if (dr["ProdLastPurchaseDistributorSaleRate"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseDistributorSaleRate"].ToString(), out mdistrateamt);

                mpurvatper = mprodvatper;
                mpurvatamt = Math.Round((mtraderate * mpurvatper) / 100, 2);
                mprodvatamt = Math.Round((mmrpn * mprodvatper) / 100, 2);

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
                txtDiscountPer.Text = Convert.ToString(mitemdiscper.ToString("#0.00")).Trim();
                txtDiscountAmt.Text = Convert.ToString(mitemdiscamt.ToString("#0.0000")).Trim();
                txtPurchaseRate.Text = Convert.ToString(mpurrate.ToString("#0.00")).Trim();
                txtMRP.Text = Convert.ToString(mmrpn.ToString("#0.00")).Trim();
                txtSaleRate.Text = Convert.ToString(msalerate.ToString("#0.00")).Trim();
                txtMasterVATPer.Text = mprodvatper.ToString("#0.00");
                txtMasterVATAmt.Text = mprodvatamt.ToString("#0.0000");
                txtDistSaleRate.Text = mdistrateamt.ToString("#0.00");
                txtDistRatePercent.Text = mdistrateper.ToString("#0.00");
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
            double mdiscdiscper = 0;
            double mdiscdiscamt = 0;
            try
            {
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfCode"].Value != null)
                    mshelfcode = mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfCode"].Value.ToString().Trim();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value != null)
                    mshelfID = mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value.ToString().Trim();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    mqty = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
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
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value != null)
                    mcstamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value.ToString() != "")
                    mcstper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value.ToString());
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
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRatePer"].Value != null)
                    mdiscdiscper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRatePer"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRate"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRate"].Value.ToString() != string.Empty)
                    mdiscdiscamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRate"].Value.ToString());
                //double mdistrate = mdiscdiscamt;
                //mdiscdiscamt  = Math.Round(mtraderate + mdistrate, 2);
                //  mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRate"].Value = mdiscdiscamt.ToString();

                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                {
                    mstockid = mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString();
                    _LastStockID = mstockid;
                }
                mpurvatper = mprodvatper;
                mpurvatamt = Math.Round((mmrpn * mpurvatper) / 100, 2); //4
                mprodvatamt = Math.Round((mmrpn * mprodvatper) / 100, 2); //4
                mitemdiscamt = Math.Round((mtraderate * mitemdiscper) / 100, 4); //4
                mamt = Math.Round((mtraderate * mqty), 2);

                txtQuantity.Text = mqty.ToString("#0");
                txtReplacement.Text = mrepl.ToString("#0");
                txtSchemeQuantity.Text = mscm.ToString("#0");
                txtAmount.Text = mamt.ToString("#0.00");
                txtBatch.Text = mbatchno;
                txtExpiry.Text = mexpiry;
                txtExpiryDate.Text = General.GetDateInShortDateFormat(mexpirydate);
                txtTradeRate.Text = mtraderate.ToString("#0.00");
                txtDiscountPer.Text = mitemdiscper.ToString("#0.00");
                txtDiscountAmt.Text = mitemdiscamt.ToString("#0.0000");
                txtPurchaseVATPer.Text = mpurvatper.ToString("#0.00");
                txtPurchaseVATAmt.Text = mpurvatamt.ToString("#0.0000");
                txtPurchaseRate.Text = mpurrate.ToString("#0.00");
                txtMRP.Text = mmrpn.ToString("#0.00");
                txtSaleRate.Text = msalerate.ToString("#0.00");
                txtMasterVATPer.Text = mprodvatper.ToString("#0.00");
                txtMasterVATAmt.Text = mprodvatamt.ToString("#0.00");
                mcbShelf.SelectedID = mshelfID;
                txtSchemeAmount.Text = mscmamt.ToString("#0.00");
                txtSchemePer.Text = mscmper.ToString("#0.00");
                txtStockID.Text = mstockid;
                txtDistRatePercent.Text = mdiscdiscper.ToString("#0.00");
                txtDistSaleRate.Text = mdiscdiscamt.ToString("#0.00");

                SsStock invss = new SsStock();
                invdr = invss.GetStockByProductIDAndBatchNumberAndMRP(_ChallanPurchase.ProductID, mbatchno, mmrp);

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
                if (_Mode == OperationMode.Add)
                    dt = invss.GetStockByProductIDForPurchase(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString(), 1);
                else
                    dt = invss.GetStockByProductIDForPurchase(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString(), 0);
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
                txtDiscountPer.Text = "0.00";
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
                txtDistRatePercent.Text = "0.00";
                txtDistSaleRate.Text = "0.00";
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.ClearpnlProductDetail>>" + Ex.Message);
            }
        }
        #endregion

        #region keydown-Click-DoubleClick

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int mqty = 0;
                    int.TryParse(txtQuantity.Text.ToString(), out mqty);
                    if (mqty == 0)
                        txtQuantity.Focus();
                    else
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
                        txtMasterVATAmt.Text = Math.Round(mtraderate * mmstper / 100, 2).ToString("#0.00");
                    }
                    else
                    {
                        txtMasterVATPer.Text = "0.00";
                        txtMasterVATAmt.Text = Math.Round(mtraderate * mmstper / 100, 2).ToString("#0.00");
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
            _purchaseMode = ChallanPurchaseMode.Add;

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
                _purchaseMode = ChallanPurchaseMode.Add;

                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null)
                    double.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString(), out mamt);
                mpMSVC.Enabled = true;
                pnlProductDetail.Enabled = true;
                pnlProductDetail.SendToBack();
                pnlProductDetail.Visible = false;
                pnlEditProduct.SendToBack();
                pnlEditProduct.Visible = false;
                dgvBatchGrid.Visible = false;
                //    dgvLastPurchase.Visible = false;
                ClearpnlProductDetail();
                pnlBillDetails.Enabled = true;
                if (mamt == 0)
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
                mvatper = Convert.ToDouble(txtPurchaseVATPer.Text.ToString());
                mvatamt = Convert.ToDouble(txtPurchaseVATAmt.Text.ToString());
                mbatch = txtBatch.Text.ToString();
                mexp = txtExpiry.Text;
                mexpdate = General.GetValidExpiryDate(mexp);
                mprate = Convert.ToDouble(txtPurchaseRate.Text.ToString());
                if ((mqty + mscm + mrepl) <= 0)
                {
                    lblFooterMessage.Text = "Enter Quantity or Scheme Quantity";
                    txtQuantity.Focus();
                }
                else if (mprate == 0)
                {
                    lblFooterMessage.Text = "PurchaseRate should be > 0";
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
                                            lblFooterMessage.Text = "Please Check Expiry Date";
                                            txtExpiry.Focus();
                                        }

                                    }
                                    else
                                    {
                                        lblFooterMessage.Text = "Sale Rate > Trade Rate + Vat Amount";
                                        txtTradeRate.Focus();
                                    }
                                }
                                else
                                {
                                    lblFooterMessage.Text = "Mrp > Trade Rate";

                                    txtTradeRate.Focus();

                                }
                            }
                            else
                            {
                                lblFooterMessage.Text = "Please Check Quantity,Scheme,Replacement";
                                txtQuantity.Focus();
                            }
                        }
                        else
                        {
                            if (mqty == 0 && mscm == 0)
                            {
                                lblFooterMessage.Text = "Please Check Quantity";
                                txtQuantity.Focus();
                            }
                            else
                                retValue = true;
                        }

                    }
                    int mvd = Convert.ToInt32(datePickerBillDate.Value.ToString("yyyyMMdd"));
                    if ((mvatper == 12.5 && mvd > 20160917) || (mvatper == 13.5 && mvd < 20160917) || (mvatper == 5.5 && mvd > 20160917) || (mvatper == 6 && mvd < 20160917))
                    {
                        lblFooterMessage.Text = "Please Check VAT";
                        retValue = false;
                        tsBtnSave.Enabled = false;
                    }
                    else
                    {
                        retValue = true;
                        lblFooterMessage.Text = "";
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
            try
            {
                if (ifok)
                {
                    CalculatePurRateSaleRateAndAmount();
                    pnlEditProduct.SendToBack();
                    pnlEditProduct.Visible = false;
                    pnlProductDetail.SendToBack();
                    pnlProductDetail.Visible = false;
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = pobj.Id; // [ansuman][28.11.2016]
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = txtProdName.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].ReadOnly = true;
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Company"].Value = txtCompShortName1.Text;

                    mpMSVC.MainDataGridCurrentRow.Cells["Col_UnitOfMeasure"].Value = txtUOM.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Pack"].Value = txtPack1.Text.ToString();

                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = txtQuantity.Text.ToString();
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
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value = txtDiscountPer.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value = txtAmount.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountAmount"].Value = txtDiscountAmt.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmount"].Value = txtSchemeAmount.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value = txtCSTAmount.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value = txtMasterVATPer.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_VATAmountSale"].Value = txtMasterVATAmt.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_SplDiscountAmount"].Value = txtSplDiscountPerUnit.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_CashDiscountAmount"].Value = txtCashDisountPerUnit.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Margin"].Value = txtMargin.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Margin2"].Value = txtMargin2.Text.ToString();
                    // mpMSVC.MainDataGridCurrentRow.Cells["Col_SplDiscountAmount"].Value = txtSplDiscountPerUnit.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value = txtStockID.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ScanCode"].Value = txtScanCode.Text.ToString();
                    if (mcbShelf.SelectedID != null && mcbShelf.SelectedID != string.Empty)
                        mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value = mcbShelf.SelectedID;
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRate"].Value = txtDistSaleRate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRatePer"].Value = txtDistRatePercent.Text.ToString();

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

            }

            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.ButtonOKClick>>" + Ex.Message);
            }
            return result;
        }
        private void btnOK_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ButtonOKClick();

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnOK_KeyDown>>" + Ex.Message);
            }
        }
        private void txtSchemeQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                    txtQuantity.Focus();
                else
                {
                    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                    {
                        txtSchemeAmount.Focus();

                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtSchemeQuantity_KeyDown>>" + Ex.Message);
            }
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
                    txtBatch.Focus();
                else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                    txtSchemeQuantity.Focus();
                else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.O)
                {
                    btnOK.Focus();
                }
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
                    //if (txtExpiry.Text.ToString() == "00/00")
                    //{
                    //    txtExpiryDate.Focus();
                    //    txtExpiryDate.SelectAll();
                    //}
                    //else 
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

                    if (mmrp == 0)
                        txtMRP.Focus();
                    else
                        txtTradeRate.Focus();

                    if (_ChallanPurchase.MRP != mmrp)
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
                    txtSchemeAmount.Focus();
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
        private void txtTradeRateValidating()
        {
            double mtrate = 0;
            double mmrp = 0;
            if (txtMRP.Text != null && txtMRP.Text != string.Empty)
                mmrp = Convert.ToDouble(txtMRP.Text.ToString());
            if (txtTradeRate.Text != null && txtTradeRate.Text != string.Empty)
                mtrate = Convert.ToDouble(txtTradeRate.Text.ToString());
            if (mtrate == 0 || mtrate > mmrp)
            {
                lblFooterMessage.Text = "Trade Rate should be < MRP";
                txtTradeRate.Focus();
            }
            else
            {
                CalculatePurRateSaleRateAndAmount();
                lblFooterMessage.Text = "";
                txtDiscountPer.Focus();
            }
        }
        private void txtDiscountPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtDiscountAmt.Text = "0.00";
                CalculatePurRateSaleRateAndAmount();
                txtPurchaseVATPer.Focus();
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
                if (txtDistRatePercent.Visible)
                    txtDistRatePercent.Focus();
                else
                    txtScanCode.Focus();

            }
            else if (e.KeyCode == Keys.Up)
                txtDiscountPer.Focus();
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
            if ((txtBatch.Text.ToString() == null || txtBatch.Text.ToString() == ""))
                txtBatch.Focus();
            else
                btnOK.Enabled = true;
        }
        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbCreditor.SeletedItem != null)
            {
                txtBillNumber.Focus();
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
                        _ChallanPurchase.ChallanNumber = txtBillNumber.Text.ToString().Trim();
                        if (_Mode == OperationMode.Add)
                            retValue = purbill.CheckForUniqueBillNumberforNew(_ChallanPurchase.ChallanNumber, _ChallanPurchase.AccountID);
                        else
                            retValue = purbill.CheckForUniqueBillNumberforEdit(_ChallanPurchase.Id, _ChallanPurchase.ChallanNumber, _ChallanPurchase.AccountID);
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
                    double mcstamt = 0;
                    double mmstamtbySale = 0;
                    double mqty = 0;
                    double mscmqty = 0;
                    double mscmdiscper = 0;
                    double mscmamt = 0;
                    double mitemdiscper = 0;
                    double mitemdiscamt = 0;
                    double mtraderateafterscm = 0;
                    double mcashdiscper = 0;
                    _ChallanPurchase.AmountCashDiscountPerUnit = 0;
                    _ChallanPurchase.AmountSplDiscountPerUnit = 0;
                    _ChallanPurchase.SchemeDiscountPercent = 0;
                    _ChallanPurchase.AmountScmDiscountPerUnit = 0;
                    _ChallanPurchase.AmountSchemeDiscount = 0;
                    double mspldiscper = 0;
                    double mspldiscamt = 0;
                    double moctper = 0;
                    double moctamt = 0;
                    double msalerate = 0;
                    double mpurvatper = 0;
                    double msalevatper = 0;
                    double msalevatamt = 0;
                    double mamt = 0;
                    double mamtzerovat = 0;
                    double mskl = 0;

                    double.TryParse(txtQuantity.Text.ToString(), out mqty);
                    double.TryParse(txtTradeRate.Text.ToString(), out mtraderate);
                    double.TryParse(txtDiscountPer.Text.ToString(), out mitemdiscper);
                    double.TryParse(txtSchemePer.Text.ToString(), out mscmdiscper);
                    //   double.TryParse(txtSplDiscPerS.Text.ToString(), out mspldiscper);
                    double.TryParse(txtPurchaseVATPer.Text.ToString(), out mpurvatper);
                    double.TryParse(txtMasterVATPer.Text.ToString(), out msalevatper);
                    double.TryParse(txtOCTPerS.Text.ToString(), out moctper);
                    //  double.TryParse(txtCashDiscountPerS.Text.ToString(), out mcashdiscper);
                    double.TryParse(txtSchemeAmount.Text.ToString(), out mscmamt);
                    double.TryParse(txtOCTAmountS.Text.ToString(), out moctamt);
                    //  double.TryParse(txtSplDiscountS.Text.ToString(), out mspldiscamt);
                    double.TryParse(txtDiscountAmt.Text.ToString(), out mitemdiscamt);
                    mamt = Math.Round(mqty * mtraderate, 2); //4
                    mskl = Math.Round(mamt - mscmamt, 2); //4
                    _ChallanPurchase.AmountSchemeDiscount = mscmamt;
                    _ChallanPurchase.SchemeDiscountPercent = mscmdiscper;
                    if (mqty > 0)
                    {
                        //  if (mitemdiscamt > 0)
                        //     mitemdiscper = Math.Round((mitemdiscamt * 100 * mqty) / mskl, 2);
                        //   txtDiscountPer.Text = mitemdiscper.ToString("#0.00");

                        mitemdiscamt = Math.Round((((mskl) * mitemdiscper / 100) / mqty), 2); //4
                        mspldiscper = Math.Round((100 * mspldiscamt) / (mamt - mitemdiscamt - mscmamt - moctamt), 2); //4
                        _ChallanPurchase.AmountScmDiscountPerUnit = Math.Round(_ChallanPurchase.AmountSchemeDiscount / mqty, 2); //4
                        _ChallanPurchase.AmountSplDiscountPerUnit = Math.Round(Math.Round(((mskl - mitemdiscamt) * mspldiscper) / 100, 2) / mqty, 2); //4
                        _ChallanPurchase.AmountCashDiscountPerUnit = Math.Round((Math.Round((mskl - _ChallanPurchase.AmountSplDiscountPerUnit - mitemdiscamt) * mcashdiscper, 2) / 100) / mqty, 2); //4
                    }
                    double.TryParse(txtCSTAmount.Text.ToString(), out mcstamt);
                    double.TryParse(txtSchemeQuantity.Text.ToString(), out mscmqty);
                    if (mqty > 0)
                    {
                        double pamt = Math.Round(((mamt - moctamt - Math.Round(_ChallanPurchase.AmountCashDiscountPerUnit * mqty, 2) - Math.Round(_ChallanPurchase.AmountSplDiscountPerUnit * mqty, 2) - mscmamt - Math.Round(mitemdiscamt * mqty, 2)) / mqty), 2); //4
                        mpurvatamt = Math.Round(((mamt - moctamt - Math.Round(_ChallanPurchase.AmountCashDiscountPerUnit * mqty, 2) - Math.Round(_ChallanPurchase.AmountSplDiscountPerUnit * mqty, 2) - mscmamt - Math.Round(mitemdiscamt * mqty, 2)) / mqty) * mpurvatper / 100, 2); //4
                    }

                    msalevatamt = Math.Round((mmrp * msalevatper) / 100, 2); //4                    
                    if ((mqty + mscmqty) > 0)
                        mtraderateafterscm = Math.Round((mtraderate * mqty) / (mqty + mscmqty), 2); //4
                    if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                        mprate = mtraderateafterscm + mpurvatamt + mcstamt - _ChallanPurchase.AmountScmDiscountPerUnit - mitemdiscamt - _ChallanPurchase.AmountCashDiscountPerUnit;
                    else
                        mprate = mtraderateafterscm + mcstamt - _ChallanPurchase.AmountScmDiscountPerUnit - mitemdiscamt - _ChallanPurchase.AmountCashDiscountPerUnit - _ChallanPurchase.AmountSplDiscountPerUnit;
                    if (General.CurrentSetting.MsetPurchaseAddVATInSaleRate == "Y")
                        msalerate = mmrp + mmstamtbySale + mcstamt;
                    else
                        msalerate = mmrp;
                    mamt = Math.Round(mqty * mtraderate, 2);
                    if (mpurvatper == 0)
                        mamtzerovat = mamt - (mitemdiscamt * mqty) - mscmamt - (_ChallanPurchase.AmountSplDiscountPerUnit * mqty) - (_ChallanPurchase.AmountCashDiscountPerUnit * mqty);
                    else
                        mamtzerovat = 0;

                    txtDiscountAmt.Text = mitemdiscamt.ToString("#0.0000");
                    txtPurchaseVATAmt.Text = mpurvatamt.ToString("#0.0000");
                    txtMasterVATAmt.Text = msalevatamt.ToString("#0.0000");
                    txtAmount.Text = mamt.ToString("#0.00");
                    txtSaleRate.Text = msalerate.ToString("#0.00");
                    txtSplDiscountPerUnit.Text = _ChallanPurchase.AmountSplDiscountPerUnit.ToString("0.00");
                    txtCashDisountPerUnit.Text = _ChallanPurchase.AmountCashDiscountPerUnit.ToString("0.0000");
                    //  txtSplDiscPerS.Text = mspldiscper.ToString("#0.0000");
                    _ChallanPurchase.SpecialDiscountPercentS = mspldiscper;
                    if (mprate > 0)
                        txtPurchaseRate.Text = mprate.ToString("#0.00");
                    txtPurZeroVAT.Text = mamtzerovat.ToString("#0.00");
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
                        double mcstamt = 0;
                        double mmstamtbySale = 0;
                        double mscmqty = 0;
                        double mscmdiscper = 0;
                        double mscmamt = 0;
                        double mitemdiscper = 0;
                        double mitemdiscamt = 0;
                        double mtraderateafterscm = 0;
                        double mcashdiscper = 0;
                        _ChallanPurchase.AmountCashDiscountPerUnit = 0;
                        _ChallanPurchase.AmountSplDiscountPerUnit = 0;
                        _ChallanPurchase.SchemeDiscountPercent = 0;
                        _ChallanPurchase.AmountScmDiscountPerUnit = 0;
                        _ChallanPurchase.AmountSchemeDiscount = 0;
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

                        double.TryParse(dr.Cells["Col_VAT"].Value.ToString(), out mpurvatper);
                        double.TryParse(dr.Cells["Col_ProdVATPer"].Value.ToString(), out msalevatper);

                        if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null && dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString() != string.Empty)
                            double.TryParse(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString(), out mscmamt);

                        double.TryParse(dr.Cells["Col_PurchaseRate"].Value.ToString(), out mpurrate);
                        mamt = Math.Round(mqty * mtraderate, 2); //4
                        mskl = Math.Round(mamt - mscmamt, 2); //4
                        _ChallanPurchase.AmountSchemeDiscount = mscmamt;
                        _ChallanPurchase.SchemeDiscountPercent = mscmdiscper;

                        if (mqty > 0)
                        {
                            mitemdiscamt = Math.Round((((mskl) * mitemdiscper / 100) / mqty), 2); //4
                            mspldiscper = Math.Round((100 * mspldiscamt) / (mamt - mitemdiscamt - mscmamt - moctamt), 2); //4
                            _ChallanPurchase.AmountScmDiscountPerUnit = Math.Round(_ChallanPurchase.AmountSchemeDiscount / mqty, 2); //4


                            _ChallanPurchase.AmountSplDiscountPerUnit = Math.Round((((mskl - mitemdiscamt) * mspldiscper) / 100) / mqty, 2); //4
                            _ChallanPurchase.AmountCashDiscountPerUnit = Math.Round((((mskl - _ChallanPurchase.AmountSplDiscountPerUnit - mitemdiscamt) * mcashdiscper) / 100) / mqty, 2); //4
                        }
                        double.TryParse(dr.Cells["Col_CSTAmount"].Value.ToString(), out mcstamt);
                        double.TryParse(dr.Cells["Col_Scheme"].Value.ToString(), out mscmqty);
                        if (mqty > 0)
                            mpurvatamt = Math.Round(((mamt - moctamt - _ChallanPurchase.AmountCashDiscountPerUnit - _ChallanPurchase.AmountSplDiscountPerUnit - mscmamt - mitemdiscamt) / mqty) * mpurvatper / 100, 2); //4

                        msalevatamt = Math.Round((mmrp * msalevatper) / 100, 2); //4
                        if ((mqty + mscmqty) > 0)
                            mtraderateafterscm = Math.Round((mtraderate * mqty) / (mqty + mscmqty), 2); //4
                        if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                            mprate = mtraderateafterscm + mpurvatamt + mcstamt - _ChallanPurchase.AmountScmDiscountPerUnit - mitemdiscamt - _ChallanPurchase.AmountCashDiscountPerUnit - _ChallanPurchase.AmountSplDiscountPerUnit;
                        else
                            mprate = mtraderateafterscm + mcstamt - _ChallanPurchase.AmountScmDiscountPerUnit - mitemdiscamt - _ChallanPurchase.AmountCashDiscountPerUnit - _ChallanPurchase.AmountSplDiscountPerUnit;
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
                        dr.Cells["Col_SplDiscountPer"].Value = _ChallanPurchase.AmountSplDiscountPerUnit.ToString("0.00");
                        dr.Cells["Col_CashDiscountAmount"].Value = _ChallanPurchase.AmountCashDiscountPerUnit.ToString("0.00");
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
            double mmargin = 0;
            double mmargin2 = 0;
            double mpurrate = 1;
            double msalerate = 1;
            double mvatamt = 0;
            double mtraterate = 0;
            double mmrp = 0;
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
                //{
                //    if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                //    {
                //        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / (mtraterate + mvatamt), 2);
                //        mmargin2 = Math.Round((msalerate - mpurrate) / mpurrate, 2);
                //    }
                //    else
                //    {
                        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / msalerate, 2);
                        mmargin2 = Math.Round((msalerate - mpurrate) / msalerate, 2);
                //    }
                //}
                mmargin = Math.Round(mmargin * 100, 2);
                mmargin2 = Math.Round(mmargin2 * 100, 2);
                txtMargin.Text = mmargin.ToString("#0.00");
                txtMargin2.Text = mmargin2.ToString("#0.00");
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
            _ChallanPurchase.AmountBillS = Convert.ToDouble(txtBillAmountS.Text.ToString());
            try
            {

                if (_ChallanPurchase.AmountBillS > 0)
                {
                    if (txtSchemeDiscountS.Text.ToString().Trim() != "")
                        _ChallanPurchase.AmountSchemeDiscountS = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
                    if (txtItemDiscountS.Text.ToString().Trim() != "")
                        _ChallanPurchase.AmountItemDiscountS = Convert.ToDouble(txtItemDiscountS.Text.ToString());


                    _ChallanPurchase.SpecialDiscountPercentS = Math.Round((100 * _ChallanPurchase.AmountSpecialDiscountS) / (_ChallanPurchase.AmountBillS - _ChallanPurchase.AmountItemDiscountS - _ChallanPurchase.AmountSchemeDiscountS - _ChallanPurchase.AmountOctroiS), 6);

                    if (txtAddOnS.Text.ToString().Trim() != "")
                        _ChallanPurchase.AmountAddOnFreightS = Convert.ToDouble(txtAddOnS.Text.ToString());
                    if (txtLessS.Text.ToString().Trim() != "")
                        _ChallanPurchase.AmountLessS = Convert.ToDouble(txtLessS.Text.ToString());
                    //if (txtCRAmountS.Text.ToString().Trim() != "")
                    //    _ChallanPurchase.AmountCreditNoteS = Convert.ToDouble(txtCRAmountS.Text.ToString());
                    //if (txtDBAmountS.Text.ToString().Trim() != "")
                    //    _ChallanPurchase.AmountDebitNoteS = Convert.ToDouble(txtDBAmountS.Text.ToString());

                    if (txtCashDiscountAmountS.Text.ToString().Trim() != "")
                        _ChallanPurchase.AmountCashDiscountS = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
                    if (_ChallanPurchase.AmountBillS - _ChallanPurchase.AmountSpecialDiscountS - _ChallanPurchase.AmountSchemeDiscountS - _ChallanPurchase.AmountItemDiscountS + _ChallanPurchase.AmountCreditNoteS <= _ChallanPurchase.AmountDebitNoteS)
                    {
                        lblFooterMessage.Text = "Invalid Debit Note Amount";
                        _ChallanPurchase.AmountDebitNoteS = 0;
                        //txtDBAmountS.Text = "0.00";                       
                    }
                    if (_ChallanPurchase.AmountCashDiscountS > (_ChallanPurchase.AmountBillS - _ChallanPurchase.AmountSpecialDiscountS - _ChallanPurchase.AmountSchemeDiscountS - _ChallanPurchase.AmountItemDiscountS + _ChallanPurchase.AmountCreditNoteS - _ChallanPurchase.AmountDebitNoteS))
                    {
                        lblFooterMessage.Text = "Invalid Cash Discount";
                        _ChallanPurchase.CashDiscountPercentageS = 0;
                        _ChallanPurchase.AmountCashDiscountS = 0;
                        txtCashDiscountAmountS.Text = "0.00";

                    }
                    txtCashDiscountAmountS.Text = _ChallanPurchase.AmountCashDiscountS.ToString("#0.00");

                    if (txtCashDiscountAmountS.Text.ToString().Trim() != "")
                        _ChallanPurchase.AmountCashDiscountS = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
                    if (txtVAT5AmountS.Text.ToString().Trim() != "")
                        _ChallanPurchase.AmountVAT5PercentS = Convert.ToDouble(txtVAT5AmountS.Text.ToString());
                    if (txtVAT12Point5AmountS.Text.ToString().Trim() != "")
                        _ChallanPurchase.AmountVAT12point5PercentS = Convert.ToDouble(txtVAT12Point5AmountS.Text.ToString());

                    if (txtOCTPerS.Text.ToString().Trim() != "")
                        _ChallanPurchase.OctroiPercentageS = Convert.ToDouble(txtOCTPerS.Text.ToString());
                    if (_ChallanPurchase.OctroiPercentageS > 0)
                        _ChallanPurchase.AmountOctroiS = Math.Round(_ChallanPurchase.TotalAmountForOctroiS * _ChallanPurchase.OctroiPercentageS / 100, 2);
                    _ChallanPurchase.AmountS = Math.Round(_ChallanPurchase.AmountBillS - _ChallanPurchase.AmountSchemeDiscountS - _ChallanPurchase.AmountItemDiscountS
                        - _ChallanPurchase.AmountSpecialDiscountS + _ChallanPurchase.AmountAddOnFreightS - _ChallanPurchase.AmountLessS + _ChallanPurchase.AmountCreditNoteS
                        - _ChallanPurchase.AmountDebitNoteS - _ChallanPurchase.AmountCashDiscountS + _ChallanPurchase.AmountVAT5PercentS
                        + _ChallanPurchase.AmountVAT12point5PercentS + _ChallanPurchase.AmountOctroiS, 2);
                    CalculateRoundup();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateFinalSummary>>" + Ex.Message);
            }

        }
        private void CalculateRoundup()
        {
            try
            {
                txtTotalS.Text = _ChallanPurchase.AmountS.ToString("#0.00");
                if (cbRound.Checked == true)
                    _ChallanPurchase.RoundUpAmountS = Math.Round(_ChallanPurchase.AmountS, 0) - _ChallanPurchase.AmountS;
                else
                    _ChallanPurchase.RoundUpAmountS = 0;
                _ChallanPurchase.AmountNetS = _ChallanPurchase.AmountS + _ChallanPurchase.RoundUpAmountS;
                txtNetAmountS.Text = _ChallanPurchase.AmountNetS.ToString("#0.00");
                txtBillAmount.Text = _ChallanPurchase.AmountNetS.ToString("#0.00");
                txtRoundUPS.Text = _ChallanPurchase.RoundUpAmountS.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateRoundup>>" + Ex.Message);
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
                        msplddx = Math.Round(((mskl - mitm) * _ChallanPurchase.SpecialDiscountPercentS) / 100, 2); //4
                        mcrddx = Math.Round(((mskl - mitm) * _ChallanPurchase.CreditNoteDiscountPercentS) / 100, 2); //4
                        mddx = Math.Round(Math.Round((mskl - msplddx - mitm) * _ChallanPurchase.CashDiscountPercentageS, 2) / 100, 2); //4
                        mtt1 = Math.Round((mamt - mddx - msplddx - mcrddx - mscmdisc - mitm) * (mmstperpur / 100), 2); //4
                        mtt1S = mtt1;
                        mtt1 = Math.Round(mtt1 / mqty, 2); //4
                        mtotalvat += mtt1;
                        dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();
                        dr.Cells["Col_CreditNoteAmount"].Value = mcrddx.ToString();
                        dr.Cells["Col_CashDiscountAmount"].Value = mddx.ToString();
                        mtotcashdiscount += mddx;
                        dr.Cells["Col_VATAmountPurchase"].Value = mtt1.ToString();
                        dr.Cells["Col_SplDiscountPer"].Value = _ChallanPurchase.SpecialDiscountPercentS.ToString();
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
                //txtViewVat5per.Text = mtotmst5.ToString("0.00");
                //txtViewVat12point5per.Text = mtotmst12point5.ToString("#0.00");
                txtTotalVATAmountS.Text = (mtotmst5 + mtotmst12point5).ToString("#0.00");
                //  txtPurchaseAmountVAT5S.Text = mmstamt5.ToString("0.00");
                //  txtPurchaseAmountVAT12point5S.Text = mmstamt12point5.ToString("0.00");
                // txtPurchaseAmountVATZeroS.Text = mpuramountzero.ToString("#0.00");
                txtCashDiscountAmountS.Text = mtotcashdiscount.ToString("#0.00");
                //txtPreCashDiscountAmountS.Text = mtotcashdiscount.ToString("#0.00");
                //txtpuramount0.Text = mpuramount0.ToString("0.00");
                // txtpuramount5.Text = mpuramount5.ToString("0.00");
                // txtpuramount12point5.Text = mpuramount12point5.ToString("#0.00");


            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateFinalVAT>>" + Ex.Message);
            }
        }
        #endregion

        #region Button Click
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

                    pnlProductDetail.Visible = false;
                    //   dgvLastPurchase.Visible = false;
                    pnlBillDetails.Enabled = false;
                    mpMSVC.Enabled = false;
                    pnlSummary.Location = GetpnlSummaryLocation();
                    pnlSummary.BringToFront();
                    this.ActiveControl = pnlSummary;
                    pnlSummary.Visible = true;
                    if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _ChallanPurchase.StatementNumber > 0)
                        pnlSummary.Enabled = false;
                    else
                        pnlSummary.Enabled = true;
                    CalculateGetSummaryData();
                    //txtCRAmountS.Focus();
                    btnSummary.Enabled = false;
                    // 14/9/2016
                    //if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                    //{

                    //    if (dt != null && dt.Rows.Count > 0)
                    //    {
                    //        pnlDebitCreditNote.BringToFront();
                    //        pnlDebitCreditNote.Visible = true;
                    //        dgCreditNote.Visible = true;
                    //        lblFooterMessage.Text = "Press Space Bar to Select unSelect Credit Debit Note";
                    //        pnlDebitCreditNote.Select();
                    //        if (_Mode == OperationMode.View)
                    //            btnCRDBOK.Focus();
                    //        else
                    //            dgCreditNote.Focus();

                    //    }
                    //}



                    CalculateFinalSummary();
                    if (_ChallanPurchase.StatementNumber > 0)
                        tsBtnSave.Enabled = false;
                }
                else
                {
                    pnlProductDetail.Visible = false;
                    //    dgvLastPurchase.Visible = false;
                    pnlBillDetails.Enabled = false;
                    mpMSVC.Enabled = false;
                    GetpnlSummaryLocation();
                    pnlSummary.BringToFront();
                    pnlSummary.Enabled = false;
                    CalculateGetSummaryData();
                    CalculateFinalSummary();
                    //txtCRAmountS.Focus();
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
                pnlSummary.Visible = false;
                pnlSummary.SendToBack();
                btnSummary.Enabled = true;
                mpMSVC.BringToFront();
                mpMSVC.Visible = true;
                if (_ChallanPurchase.IfTypeChange == "N")
                {
                    pnlBillDetails.Enabled = true;
                    mpMSVC.Enabled = true;
                    //tsBtnSave.Enabled = false;
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
                double mtotvat5 = 0;
                double mtotvat12point5 = 0;
                double mvatamount = 0;
                double mamt = 0;
                double mamts = 0;
                double mvatper = 0;
                double mqty = 0;
                double mtotvatzeroamt = 0;
                double moctroiamt = 0;
                _ChallanPurchase.AmountCashDiscountS = 0;
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
                                _ChallanPurchase.AmountCashDiscountS += mamt;
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
                            //        moctroiamt += mamt;
                            //}



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
                    //if (Convert.ToDouble(txtGridAmountTot.Text.ToString()) != _ChallanPurchase.AmountBillS)
                    //{
                    _ChallanPurchase.TotalAmountForOctroiS = moctroiamt;
                    txtBillAmountS.Text = mtotamt.ToString("#0.00");
                    txtBillAmount.Text = mtotamt.ToString("#0.00");
                    txtItemDiscountS.Text = mtotitem.ToString("#0.00");
                    //   txtSplDiscPerS.Text = _ChallanPurchase.SpecialDiscountPercentS.ToString("#0.00");
                    txtCashDiscountAmountS.Text = _ChallanPurchase.AmountCashDiscountS.ToString("0.00");
                    //  txtPreCashDiscountAmountS.Text = _ChallanPurchase.AmountCashDiscountS.ToString("#0.00");
                    txtSchemeDiscountS.Text = mtotscm.ToString("#0.00");
                    txtVAT5AmountS.Text = mtotvat5.ToString("#0.00");
                    txtVAT12Point5AmountS.Text = mtotvat12point5.ToString("#0.00");
                    //   txtViewVat5per.Text = mtotvat5.ToString("#0.00");
                    //   txtViewVat12point5per.Text = mtotvat12point5.ToString("#0.00");
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
                    Log.WriteError("UclPurchase.CalculateGetSummaryData>>" + Ex.Message);
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
                    Log.WriteError("UclPurchase.CalculateTotalVATAmount>>" + Ex.Message);
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
                    txtVAT12Point5AmountS.Focus();
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
                    //txtDBAmountS.Focus();
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
                    txtVAT5AmountS.Focus();
                //else if (e.KeyCode == Keys.Up)
                //    txtCRAmountS.Focus();
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
                if (e.KeyCode == Keys.Enter)
                    mpMSVC.SetFocus(1);
                if (e.KeyCode == Keys.Up)
                    txtBillNumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtNarration_KeyDown>>" + Ex.Message);
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
                    mpMSVC.SetFocus(1);
                }

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtSpecialDiscountS_KeyDown>>" + Ex.Message);
            }
        }

        private void txtVAT5AmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtVAT12Point5AmountS.Focus();
                //else if (e.KeyCode == Keys.Up)
                //    txtDBAmountS.Focus();
                CalculateTotalVATAmount();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtVAT5AmountS_KeyDown>>" + Ex.Message);
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
                Log.WriteError("UclPurchase.txtVAT12Point5AmountS_KeyDown>>" + Ex.Message);
            }
        }

        //private void txtCashDiscountAmountS_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {

        //        if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
        //        {
        //            txtOCTPerS.Focus();
        //            double billamt = Convert.ToDouble(txtBillAmountS.Text.ToString());
        //            double scmamt = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
        //            double itemamt = Convert.ToDouble(txtItemDiscountS.Text.ToString());
        //            double discamt = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
        //            double actualdiscamountper = 0;

        //            double entereddiscper = Math.Round((discamt * 100) / (billamt - scmamt - itemamt), 2);
        //            if (((entereddiscper) > (actualdiscamountper + 0.20)) || ((entereddiscper) < (actualdiscamountper - 0.20)))
        //            {
        //                //    pnlSummary.Visible = false;
        //                //    pnlBillDetails.Enabled = true;
        //               // txtCashDiscountPerS.Text = entereddiscper.ToString("#0.00");
        //                lblFooterMessage.Text = "Press Enter..";
        //                // txtCashDiscountPerS.Focus();
        //                //    btnSummary.Enabled = true;
        //                //  btnSummary.Focus();

        //            }
        //            CalculateFinalSummary();
        //            CalculateFinalVAT(); // [ansuman]
        //        }
        //        else if (e.KeyCode == Keys.Up)
        //        {
        //            CalculateFinalSummary();
        //            txtLessS.Focus();
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("UclPurchase.txtOCTPerS_KeyDown>>" + Ex.Message);
        //    }
        //}

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
                Log.WriteError("UclPurchase.txtOCTPerS_KeyDown>>" + Ex.Message);
            }
        }

        private void txtOCTAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                //    btnOKS.Focus();
                //else
                if (e.KeyCode == Keys.Up)
                    txtOCTPerS.Focus();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtOCTAmountS_KeyDown>>" + Ex.Message);
            }
        }




        private void txtPurchaseVATPer_Validating(object sender, CancelEventArgs e)
        {
            double purvat = 0;
            try
            {
                purvat = Convert.ToDouble(txtPurchaseVATPer.Text.ToString());
                // vat 5.5
                if (purvat != 0 && purvat != 6 && purvat != 13.5)
                    txtPurchaseVATPer.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtPurchaseVATPer_Validating>>" + Ex.Message);
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

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtVouchernumber.Text != "")
                    {

                        _ChallanPurchase.VoucherNumber = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _ChallanPurchase.ReadDetailsByVouNumber(_ChallanPurchase.VoucherNumber, _ChallanPurchase.VoucherType, _ChallanPurchase.VoucherSeries, _ChallanPurchase.VoucherSubType);
                        FillSearchData(_ChallanPurchase.Id, "");
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteError("UclPurchase.txtVouchernumber_KeyDown>>" + Ex.Message);
                }
            }
        }
        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbCreditor.SelectedID;
            FillCreditorCombo();
            mcbCreditor.SelectedID = selectedId;
            txtBillNumber.Focus();
        }

        //private void cbAcceptNrExpired_CheckedChanged(object sender, EventArgs e)
        //{
        //    CBAcceptExpiryCheckedChange();
        //}

        //private void CBAcceptExpiryCheckedChange()
        //{
        //    //if (cbAcceptNrExpired.Checked == true)
        //    //{
        //    //    txtMRP.Focus();
        //    //    btnOK.Enabled = true;
        //    //}
        //    //else
        //    //{
        //        btnCancel.Focus();
        //        btnCancel.BackColor = General.ControlFocusColor;
        //        btnOK.Enabled = false;
        //    //}
        //}

        //private void cbAcceptNrExpired_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //        txtMRP.Focus();
        //}

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
                //  frmView.Icon = PharmaSYSDistributorPlus.Properties.Resources.Icon;
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

        private void cbTransactionType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbCreditor.Focus();
        }


        private void mpMSVC_OnCellValueChangeCommited(int colIndex)
        {
            if (colIndex == 1)
            {
                _preID = "";
                string prodname = "";
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                    _preID = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value != null)
                    prodname = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString();
                if (prodname != "" && _preID != "")
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
            //   retValue = _ChallanPurchase.DeletePreviousRecordsFromtblBarCode();
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
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_VoucherType"].Value = _ChallanPurchase.VoucherType;
                    dgvBarCode.Rows[currentbarcoderow].Cells["Col_VoucherNumber"].Value = _ChallanPurchase.VoucherNumber.ToString();
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
                txtScanCode.Focus();
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
                if (txtDistRatePercent.Text != null && txtDistRatePercent.Text.ToString() != string.Empty)
                    mdistper = Convert.ToDouble(txtDistRatePercent.Text.ToString());
                if (txtTradeRate.Text != null && txtTradeRate.Text.ToString() != string.Empty)
                    mtraderate = Convert.ToDouble(txtTradeRate.Text.ToString());
                if (mdistper > 0)
                {
                    mdistrate = Math.Round((mtraderate * mdistper) / 100, 2);
                    mdistrate = Math.Round(mtraderate + mdistrate, 2);
                    txtDistSaleRate.Text = mdistrate.ToString("#0.00");
                }


                txtDistSaleRate.Focus();
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

        private void txtDistSaleRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDistPercentEnterkeyPressed();
                txtScanCode.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtPurchaseVATPer.Focus();

        }

        private void CopyPurchaseOrderProducts()
        {
            throw new NotImplementedException();
        }


        private int CheckForProductinMainGrid(string productID)
        {
            int index = -1;
            string prodID = string.Empty;
            foreach (DataGridViewRow dr in mpMSVC.Rows)
            {
                if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() != string.Empty)
                    prodID = dr.Cells["Col_ProductID"].Value.ToString();
                if (prodID == productID)
                    index = dr.Index;
            }
            return index;
        }

        private void dgPurchaseOrder_DoubleClick(object sender, EventArgs e)
        {

        }



        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            BtnDownLoadClick();
        }

        private void BtnDownLoadClick()
        {
            ConstructdgBillsColumns();
            dgBills.BringToFront();
            dgBills.Visible = true;
            ShowBills();
            Invoices invoices = new Invoices();
            DataTable dtInvoices = new DataTable("InvoiceItems");
            //invoices.InvoicesFromUserBulk(DeveloperId, UserId, Password, dtInvoices);

            //Download fresh Invoices
            DataTable invoicesReceived = invoices.InvoicesToUser(General.DeveloperId, General.UserId, General.Password);
            Emilan _emilan = new Emilan();
            bool retvalue = _emilan.CopyInvoicesReceived(invoicesReceived);

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

            if (result == DialogResult.OK)
            {
                _ImportBill = _formImportAlliedSaleBill.ImportBillData;
                if (_ImportBill != null)
                {
                    FillFormWithImportBillData();
                }
            }
        }
        private void CreateImportAlliedSaleBillForm()
        {
            _formImportAlliedSaleBill = new FormImportSaleBill();
            //  _formImportAlliedSaleBill.OnNewParty += new EventHandler(ImportAlliedSaleBill_OnNewParty);
            //  _formImportAlliedSaleBill.OnNewProduct += new EventHandler(ImportAlliedSaleBill_OnNewProduct);
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
        private Product FillProductAndCmpnyData(string ID)
        {
            if (_purchaseMode == ChallanPurchaseMode.Edit)
            {
                OPStock _OPStock = new OPStock();
                pobj.Id = ID;
                pobj.Name = txtProdName.Text;
                pobj.ProdCompID = "";
                pobj.ProdPack = "";
                pobj.ProdPackType = "";
                pobj.ProdPackTypeID = "";
                pobj.ProdLoosePack = Convert.ToInt32(txtUOM.Text);
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
                            pobj.ProdCompID = Convert.ToString(drowprod["ProdCompID"]);
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
                                mcbCompany1.Refresh();
                                mcbCompany1.FillData(dt);
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
                                    mcbGenCatOpStock.Refresh();
                                    mcbGenCatOpStock.FillData(dt);
                                    mcbGenCatOpStock.SelectedID = drowGenCat["GenericCategoryID"].ToString();
                                    pobj.ProdGenericID = mcbGenCatOpStock.SeletedItem.ItemData[0].ToString();
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
                                    mcbProductCategory1.Refresh();
                                    mcbProductCategory1.FillData(dt);
                                    mcbProductCategory1.SelectedID = drowProdCat["ProductCategoryID"].ToString();
                                    pobj.ProdProductCategoryID = mcbProductCategory1.SeletedItem.ItemData[0].ToString();
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
                                    mcbShelfNoOpStock.Refresh();
                                    mcbShelfNoOpStock.FillData(dt);
                                    mcbShelfNoOpStock.SelectedID = drowShelf["ShelfID"].ToString();
                                }
                                if (_purchaseMode == ChallanPurchaseMode.Edit)
                                {
                                    FillShelfComboList();
                                    if (mcbShelfNoOpStock.SelectedID != "")
                                    {
                                        mcbShelf.SelectedID = mcbShelfNoOpStock.SelectedID;
                                        pobj.ProdShelfID = mcbShelfNoOpStock.SeletedItem.ItemData[0].ToString();
                                    }
                                    else
                                    {
                                        mcbShelfNoOpStock.SelectedID = "";
                                        pobj.ProdShelfID = "";
                                    }
                                }
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
                            txtUOM.Text = pobj.ProdLoosePack.ToString();
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
            txtUOM.Text = mpMSVC.MainDataGridCurrentRow.Cells["Col_UnitOfMeasure"].Value.ToString();
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
            txtUOM.Text = "";
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
            if (mcbCompany1.SeletedItem != null)
            {
                txtCompShortName1.Text = mcbCompany1.SeletedItem.ItemData[2].ToString();
                this.ActiveControl = txtCompShortName1;
                txtCompShortName1.Focus();
            }
            else
            {
                this.ActiveControl = mcbCompany1;
                mcbCompany1.Focus();
            }
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
                this.ActiveControl = txtUOM;
                txtUOM.Focus();
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

        private void txtUOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // [14.11.2016]
            {
                if (txtUOM.Text != "")
                {
                    // this.ActiveControl = mcbGenCatOpStock;
                    // mcbGenCatOpStock.Focus();
                    this.ActiveControl = txtPack1;
                    txtPack1.Focus();
                }
                else
                {
                    txtUOM.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up) //kiran
            {
                mcbGenCatOpStock.Focus();
                mcbGenCatOpStock.Select();
            }
        }
        private void txtPack1_EnterKeyPressed(object sender, EventArgs e)
        {
            if (txtPack1.Text.ToString() != string.Empty)
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
        private void txtPackType1_EnterKeyPressed(object sender, EventArgs e)
        {
            this.ActiveControl = mcbShelfNoOpStock;
            mcbShelfNoOpStock.Focus();
        }

        private void mcbProductCategory1_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbProductCategory1.SeletedItem != null && mcbProductCategory1.SeletedItem.ItemData[0].ToString() != "")  // [14.11.2016]
            {
                //this.ActiveControl = txtPack1;
                //txtPack1.Focus();
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
            this.ActiveControl = mcbSchedule1;
            mcbSchedule1.Focus();
        }

        private void mcbSchedule1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = txtIsDataOK;
                txtIsDataOK.Focus();
            }
            else if (e.KeyCode == Keys.Up)//kiran
            {
                mcbShelfNoOpStock.Focus();
            }
        }
        private void txtIsDataOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtIsDataOK.Text == "Y")
            {
                pobj.Name = txtProdName.Text.Trim();
                pobj.Name = (pobj.Name.Replace("*", "X"));
                pobj.Name = (pobj.Name.Replace("%", "Per"));
                pobj.ProdCompID = mcbCompany1.SeletedItem.ItemData[0].ToString();
                pobj.ProdCompShortName = mcbCompany1.SeletedItem.ItemData[2];
                if (mcbGenCatOpStock.SeletedItem != null)
                    pobj.ProdGenericID = mcbGenCatOpStock.SeletedItem.ItemData[0].ToString();
                if (mcbProductCategory1.SeletedItem != null)
                    pobj.ProdProductCategoryID = mcbProductCategory1.SeletedItem.ItemData[0].ToString(); // [14.11.2016]
                pobj.ProdLoosePack = Convert.ToInt32(txtUOM.Text);
                pobj.ProdPackType = txtPackType1.Text;
                pobj.ProdPack = txtPack1.Text;
                if (mcbShelfNoOpStock.SeletedItem != null)
                {
                    if (mcbShelfNoOpStock.SeletedItem.ItemData[0].ToString() != "")
                        pobj.ProdShelfID = mcbShelfNoOpStock.SeletedItem.ItemData[0].ToString();
                }
                else
                    pobj.ProdShelfID = "";

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
                    _purchaseMode = ChallanPurchaseMode.Edit;
                    pnlEditProduct.Enabled = false;
                    txtQuantity.Focus();
                }

            }
            else if (e.KeyCode == Keys.Up) //kiran
            {
                mcbSchedule1.Focus();
            }
        }

        private bool SaveProductDetails()
        {
            bool IsProductCreateOrUpdate = false;
            System.Text.StringBuilder _errorMessage;
            try
            {
                if (_purchaseMode == ChallanPurchaseMode.Add)
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
                    pobj.Id = _ChallanPurchase.ProductID;
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
        private void mpMSVC_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            Exit();
        }

        private void UclChallanPurchase_Load(object sender, EventArgs e)
        {
            if (_ImportBill == null)
            {
                if (_Mode != OperationMode.ReportView)
                {
                    datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                }
            }
            else if (_Mode == OperationMode.Add && _ImportBill.TotalAmount == string.Empty)
            {
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
            }
        }

        private void btnSummary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSummaryClicked();
            else if (e.KeyCode == Keys.Escape && mpMSVC.Rows.Count > 0)
            {
                mpMSVC.Focus();
                mpMSVC.SetFocus(1);
            }
        }

        private void txtScanCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                mcbShelf.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (txtDistRatePercent.Visible == true)
                    txtDistRatePercent.Focus();
                else txtPurchaseVATPer.Focus();
            }
        }
        private void mcbShelf_UpArrowPressed(object sender, EventArgs e)
        {
            txtScanCode.Focus();
        }

        private void mcbShelf_EnterKeyPressed(object sender, EventArgs e)
        {
            btnOK.Focus();
        }

        #endregion
    }
}