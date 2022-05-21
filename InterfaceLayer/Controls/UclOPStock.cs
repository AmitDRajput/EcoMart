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
using EcoMart.InterfaceLayer.Classes;

using PharmaSYSPlus.CommonLibrary;
using PaperlessPharmaRetail.Common.Classes;
using EcoMart.DataLayer;
using System.IO;

namespace EcoMart.InterfaceLayer
{
    public enum OpStockMode   // [ansuman]
    {
        Add = 0,
        Edit = 1,
    }
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclOPStock : BaseControl
    {

        #region Declaration

        private OPStock _OPStock;
        private string IfEditPreviousRow = "N";
        bool ExpDateValid = false; //Kiran
        public OpStockMode _OpStockMode;   // [ansuman]
        public OpStockMode OpStockMode
        {
            get { return _OpStockMode; }
            set { _OpStockMode = value; }
        }
        Product pobj = null;                // [ansuman]
        Company cobj = new Company();

        #endregion

        #region contructor
        public UclOPStock()
        {
            try
            {
                InitializeComponent();
                _OPStock = new OPStock();
                SearchControl = new UclOPStockSearch();
                //  GetPurchaseSettings();
                pobj = new Product();     // [ansuman]
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        # endregion Constructor

        # region IDetail Control
        public override void SetFocus()
        {
            try
            {
                if (_Mode == OperationMode.Add)
                    mcbShelf.Focus();
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
                _OPStock.Initialise();
                AddToolTip();
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
                string _lastproductEntered = GetLastProductEntered();
                lblFooterMessage.Text = string.Concat("Last Product Entered : ", _lastproductEntered);
                FillShelfCombo();
                FillAllData();
                headerLabel1.Text = "OPENING STOCK -> NEW";
                dgvMPMSVCMain.Enabled = true;
                pnlProductDetail.Enabled = true;
                MakePnlProductDetailsReadOnlyFalse();
                pnlProductDetail.SendToBack();
                dgvMPMSVCMain.BringToFront();
                dgvMPMSVCMain.Dock = DockStyle.Fill;
                dgvBatchGrid.Visible = false;
                InitializeMainSubViewControl();
                dgvMPMSVCMain.IsAllowDelete = true;
                dgvMPMSVCMain.IsAllowNewRow = true;
                tsBtnExit.Visible = true;
                mcbShelf.Focus();
                retValue = true;
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclOPStock.Add>> " + Ex.Message);
                retValue = false;
            }
            return retValue;
        }

        private void FillAllData()
        {
            FillCompanyCombo();
            FillGenericCategoryCombo();
            FillProdCategoryCombo(); // [14.11.2016]
            FillShelfComboList();
            FillScheduleDrugCombo();
            FillPack();
            FillPackType();
        }

        private string GetLastProductEntered()
        {
            string lastproductEntered = "";
            lastproductEntered = _OPStock.GetLastProductEntered();
            return lastproductEntered;
        }

        public override bool Edit()
        {
            return true;
        }
        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            try
            {
                if (retValue == true)
                {
                    ClearData();
                    ConstructMainColumns();
                    ConstructSubColumns();
                    ConstructBatchGrid();
                    InitializeMainSubViewControl();
                    dgvMPMSVCMain.Enabled = false;
                    txtVouchernumber.ReadOnly = false;
                    txtVouchernumber.Enabled = true;
                    txtVouchernumber.Focus();
                    _OPStock.Id = "";
                    tsBtnDelete.Visible = false;
                    tsBtnEdit.Visible = false;
                    tsBtnSavenPrint.Visible = false;
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclOPStock.Cancel>> " + Ex.Message);
                retValue = false;
            }
            return retValue;
        }
        public override bool Exit()
        {
            bool retValue = false;
            if (dgvMPMSVCMain.Rows.Count < 2)
            {
                retValue = base.Exit();
                System.IO.File.Delete(General.GetOpeningStockTempFile());     // sheela 9/11/2016       
            }
            else if (_Mode == OperationMode.Add)
            {
                PSDialogResult result;
                result = PSMessageBox.Show("Save Or Remove All Product..", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
            }
            else
            {
                retValue = base.Exit();
            }
            return retValue;
        }
        public override bool Delete()
        {
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
                ClearData();
                InitializeMainSubViewControl();
                dgvMPMSVCMain.IsAllowDelete = false;
                dgvMPMSVCMain.IsAllowNewRow = false;
                dgvMPMSVCMain.Dock = DockStyle.Fill;
                headerLabel1.Text = "OPENING STOCK -> VIEW";
                dgvMPMSVCMain.Enabled = true;
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                tsBtnEdit.Visible = false;
                tsBtnDelete.Visible = false;
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
                    _OPStock.VoucherType = FixAccounts.VoucherTypeForOpeningStock;
                }
                _OPStock.GetLastRecord();
                FillSearchData(_OPStock.Id, "");
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
            _OPStock.VoucherType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _OPStock.VoucherSeries = txtVoucherSeries.Text.ToString();
            else
                _OPStock.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _OPStock.GetFirstRecord();
            if (dr != null && dr["MasterID"] != DBNull.Value)
            {
                _OPStock.Id = dr["MasterID"].ToString();
                FillSearchData(_OPStock.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _OPStock.VoucherType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _OPStock.VoucherSeries = txtVoucherSeries.Text.ToString();
            else
                _OPStock.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _OPStock.VoucherType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _OPStock.VoucherSeries = txtVoucherSeries.Text.ToString();
            else
                _OPStock.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _OPStock.VoucherNumber = i;
                dr = _OPStock.ReadDetailsByVoucherNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["MasterID"] != DBNull.Value)
            {
                _OPStock.Id = dr["MasterID"].ToString();
                FillSearchData(_OPStock.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _OPStock.GetLastVoucherNumber(FixAccounts.VoucherTypeForOpeningStock, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _OPStock.VoucherType = txtVouType.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _OPStock.VoucherNumber = i;
                dr = _OPStock.ReadDetailsByVoucherNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["MasterID"] != DBNull.Value)
            {
                _OPStock.Id = dr["MasterID"].ToString();
                FillSearchData(_OPStock.Id, "");
            }
            return retValue;
        }
        public override bool Save()
        {
            _OPStock.EntryDate = Convert.ToString(DateTime.Now);
            _OPStock.Amount = Convert.ToDouble(txtGridAmountTot.Text.ToString());
            _OPStock.Validate();
            bool retValue = false;
            try
            {
                if (_OPStock.IsValid)
                {
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        LockTable.LockTablesForOpeningStock();
                        General.BeginTransaction();
                        _OPStock.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _OPStock.VoucherNumber = _OPStock.GetAndUpdateOpeningStockNumber(General.ShopDetail.ShopVoucherSeries);
                        _OPStock.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                        _OPStock.CreatedBy = General.CurrentUser.Id;
                        _OPStock.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _OPStock.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        txtVouchernumber.Text = _OPStock.VoucherNumber.ToString();
                        retValue = _OPStock.AddDetails();
                        _SavedID = _OPStock.Id;
                        if (retValue)
                            retValue = SaveParticularsProductwise();
                        if (retValue)
                        {
                            General.CommitTransaction();
                            //CacheObject.Clear("cacheCounterSale");
                            //DBProduct dbStock = new DBProduct();
                            //dbStock.GetOverviewData();
                        }
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            System.IO.File.Delete(General.GetOpeningStockTempFile());  // sheela 9/11
                            string msgLine2 = _OPStock.VoucherType + "  " + _OPStock.VoucherNumber.ToString("#0");
                            PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                            //PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                            if (result == PSDialogResult.Print)
                                Print();
                            retValue = true;
                            if (General.CurrentSetting.MsetScanBarCode == "Y")
                            {
                                result = PSMessageBox.Show("Print Labels", "", General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                            }
                        }

                        else
                        {
                            PSDialogResult result = PSMessageBox.Show("Could not Add...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                            retValue = false;
                        }

                    }
                }
                else // Show Validation Messages
                {
                    StringBuilder _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _OPStock.ValidationMessages)
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
            CacheObject.Clear("cacheCounterSale");
            return retValue;
        }
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    this.Cursor = Cursors.WaitCursor;
                    _OPStock.Id = ID;
                    _OPStock.ReadDetailsByID();
                    txtVouchernumber.Text = _OPStock.VoucherNumber.ToString();
                    datePickerBillDate.Text = General.GetDateInDateFormat(_OPStock.VoucherDate);
                    if (_Mode == OperationMode.View)
                    {
                        datePickerBillDate.Enabled = false;
                    }

                    InitializeMainSubViewControl();
                    NoofRows();
                    this.Cursor = Cursors.Default;
                    dgvMPMSVCMain.ColumnsMain["Col_ProductName"].ReadOnly = false;
                    dgvMPMSVCMain.SetFocus(1);

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        #endregion IDetail Control

        #region IDetail Members
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }
        public override void ReFillData(Control closedControl)
        {
            try
            {
                if (closedControl is UclShelf)
                    FillShelfCombo();
                else if (closedControl is UclProduct)
                {
                    Product prod = new Product();
                    DataTable proddt = prod.GetOverviewData();
                    dgvMPMSVCMain.DataSource = proddt;
                    dgvMPMSVCMain.Bind();
                }
                dgvMPMSVCMain.SetFocus(1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void NoofRows()
        {
            int itemCount = 0;
            double totamt = 0;
            double mamt = 0;
            try
            {
                foreach (DataGridViewRow dr in dgvMPMSVCMain.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                    {
                        double.TryParse(dr.Cells["Col_Amount"].Value.ToString(), out mamt);
                        totamt += mamt;
                        itemCount += 1;
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtGridAmountTot.Text = totamt.ToString("#0.00");
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
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    txtTradeRate.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.B && modifier == Keys.Alt)
                {
                    txtBatch.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    btnCancel.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.D && modifier == Keys.Alt)   // [ansuman] [04.11.2016]
                {
                    if(pnlEditProduct.Visible == false /* || pnlEditProduct.Enabled == false*/)
                    {
                        _OpStockMode = OpStockMode.Add;
                        pobj = new Product();
                        pnlEditProduct.Visible = true;
                        pnlEditProduct.BringToFront();
                        pnlEditProduct.Enabled = true;

                        if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProductID"].Value == null)  // [ansuman][11.1.2017]
                        {
                            _OpStockMode = OpStockMode.Add;
                        }

                        if (_OpStockMode != OpStockMode.Edit)
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
                        txtProdName.Text = Convert.ToString(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProductName"].Value);

                        retValue = true;
                    }
                   
                } // [ansuman]
                if (keyPressed == Keys.E && modifier == Keys.Alt)
                {
                   // txtExpiry.Focus();
                   // retValue = true;
                }
                if (keyPressed == Keys.F && modifier == Keys.Alt)
                {
                    mcbShelf.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.O && modifier == Keys.Alt)
                {
                    btnOK.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.P && modifier == Keys.Alt)
                {
                    txtPurchaseRate.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.Q && modifier == Keys.Alt)
                {
                    txtQuantity.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.R && modifier == Keys.Alt)
                {
                    txtMRP.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.S && modifier == Keys.Alt)
                {
                    txtScanCode.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                    txtCSTPer.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.Escape)
                {
                    bool flag = false;
                    if (pnlBatchGrid.Visible == true)
                    {
                        pnlBatchGrid.Visible = false;
                        pnlProductDetail.Enabled = true;
                        txtBatch.Focus();
                        txtBatch.SelectAll();
                        flag = true;
                        retValue = true;
                    }
                    else if (pnlProductDetail.Visible && dgvBatchGrid.Visible == false)
                    {
                        btnCancelClick();
                        retValue = true;
                    }
                    else if (dgvMPMSVCMain.VisibleProductGrid() == true) //kiran
                    {
                        retValue = true;
                        dgvMPMSVCMain.Focus();   // [ansuman][28.11.2016]
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = "";  // [ansuman][28.11.2016]
                    }
                    else
                        retValue = Exit();
                    _OpStockMode = OpStockMode.Add;  // [ansuman]
                    if (flag == false)
                        ClearStockData();
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
        private bool SaveParticularsProductwise()
        {
            {
                bool returnVal = false;
                bool IfRecordFound = false;
                _OPStock.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in dgvMPMSVCMain.Rows)
                    {
                        _OPStock.ProductID = "";
                        _OPStock.Batchno = "";
                        _OPStock.MRP = 0;
                        _OPStock.Expiry = "";
                        _OPStock.ExpiryDate = "";
                        _OPStock.TradeRate = 0;
                        _OPStock.PurchaseRate = 0;
                        _OPStock.SaleRate = 0;
                        _OPStock.Quantity = 0;
                        _OPStock.ProductVATPercent = 0;
                        _OPStock.ItemDiscountPercent = 0;
                        _OPStock.AmountItemDiscount = 0;
                        _OPStock.AmountSchemeDiscount = 0;
                        _OPStock.AmountPurchaseVAT = 0;
                        _OPStock.AmountCST = 0;
                        _OPStock.StockID = "";
                        _OPStock.ShelfID = "";
                        _OPStock.ShelfCode = "";
                        //_OPStock.ScanBarCode = "";
                        double loosepack = 0;

                        if (prodrow.Cells["Col_ProductName"].Value != null && prodrow.Cells["Col_ProductName"].Value.ToString() != string.Empty &&
                           Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) != 0)
                        {
                            _OPStock.SerialNumber += 1;
                            _OPStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");

                            _OPStock.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString(); // [ansuman] [28.11.2016]
                            loosepack = 1;
                            if (prodrow.Cells["Col_UnitOfMeasure"].Value != null && prodrow.Cells["Col_UnitOfMeasure"].Value.ToString() != string.Empty)
                                loosepack = Convert.ToDouble(prodrow.Cells["Col_UnitOfMeasure"].Value.ToString());
                            _OPStock.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();

                            if (prodrow.Cells["Col_MRP"].Value != null)
                                _OPStock.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString());
                            if (prodrow.Cells["Col_Expiry"].Value != null)
                                _OPStock.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                            if (prodrow.Cells["Col_ExpiryDate"].Value != null)
                            {
                                _OPStock.ExpiryDate = prodrow.Cells["Col_ExpiryDate"].Value.ToString();
                                _OPStock.ExpiryDate = General.GetExpiryInyyyymmddForm(_OPStock.ExpiryDate);
                            }
                            if (prodrow.Cells["Col_TradeRate"].Value != null)
                                _OPStock.TradeRate = Convert.ToDouble(prodrow.Cells["Col_TradeRate"].Value.ToString());
                            if (prodrow.Cells["Col_OPStockRate"].Value != null)
                                _OPStock.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_OPStockRate"].Value.ToString());
                            if (prodrow.Cells["Col_SaleRate"].Value != null)
                                _OPStock.SaleRate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());
                            if (prodrow.Cells["Col_Quantity"].Value != null)
                                _OPStock.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                            if (prodrow.Cells["Col_ProdVATPer"].Value != null)
                                _OPStock.ProductVATPercent = Convert.ToDouble(prodrow.Cells["Col_ProdVATPer"].Value.ToString());

                            _OPStock.AmountPurchaseVAT = Math.Round((_OPStock.TradeRate / loosepack) * (_OPStock.ProductVATPercent / 100), 2);

                            if (prodrow.Cells["Col_ShelfID"].Value != null && prodrow.Cells["Col_ShelfID"].Value.ToString() != string.Empty)
                                _OPStock.ShelfID = prodrow.Cells["Col_ShelfID"].Value.ToString();

                            _OPStock.PurchaseVATPercent = _OPStock.ProductVATPercent;
                            if (prodrow.Cells["Col_CSTAmount"].Value != null)
                                _OPStock.AmountCST = Convert.ToDouble(prodrow.Cells["Col_CSTAmount"].Value.ToString());
                            if (prodrow.Cells["Col_StockID"].Value != null && prodrow.Cells["Col_StockID"].Value.ToString() != "")
                                _OPStock.StockID = prodrow.Cells["Col_StockID"].Value.ToString();
                            if (prodrow.Cells["Col_ScanBarCode"].Value != null && prodrow.Cells["Col_ScanBarCode"].Value.ToString() != "")
                                _OPStock.ScanBarCode = prodrow.Cells["Col_ScanBarCode"].Value.ToString();
                            IfRecordFound = _OPStock.CheckForBatchMRPInStockTable();
                            if (IfRecordFound == true)
                                returnVal = _OPStock.UpdateOPStockIntblStock();
                            else
                            {
                                _OPStock.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                _OPStock.ScanBarCode = _OPStock.GetScanCodeForCurrentBatch(_OPStock.ProductID); // [10.02.2017]
                                returnVal = _OPStock.AddProductDetailsInStockTable();
                            }
                            if (returnVal)
                                returnVal = _OPStock.AddProductDetailsSS();

                            if (returnVal)  // [ansuman] [8.11.2016]
                            {
                                returnVal = _OPStock.UpdateOpeningStockInMasterProduct();
                            }
                            _OPStock.UpdateLastPurhcaseDataInMasterProduct();
                            if (returnVal == false)
                                break;
                        }
                    }

                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                    returnVal = false;
                }
                return returnVal;
            }
        }

        //Kiran
        private void txtExpiry_TextChanged(object sender, EventArgs e)
        {
            if (ExpDateValid == true)
                txtExpiry.Text = string.Empty;
            ExpDateValid = false;
        }

        private void txtExpiry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtExpiry.TextLength >= 0 && (e.KeyChar == (char)Keys.OemPeriod || e.KeyChar == (char)Keys.Oemcomma))
            {
                //tests 
            }
            else
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar))
                {
                    ExpDateValid = true;
                }
            }
        }
        // kiran

        //private bool UpdateClosingStockinCache()
        //{
        //    bool returnVal = false;
        //    try
        //    {
        //        foreach (DataGridViewRow prodrow in dgvMPMSVCMain.Rows)
        //        {
        //            if (prodrow.Cells["Col_ProductID"].Value != null && prodrow.Cells["Col_ProductID"].Value.ToString() != string.Empty)
        //            {
        //                _OPStock.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
        //                EcoMartCache.RefreshProductData(_OPStock.ProductID);
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
        //        General.UpdateProductListCacheTest(dgvMPMSVCMain.Rows, "Col_ProductID", dgvMPMSVCMain.Rows, "Col_ProductID");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        returnVal = false;
        //    }
        //    return returnVal;
        //}
        #endregion

        #region Other Private Methods
        private void dgvMPMSVCMain_OnDetailsFilled(DataGridViewRow selectedRow)
        {
            _OPStock.MRP = 0;
            double mmamt = 0;
            int mrowindex = dgvMPMSVCMain.SelectedRow.Index;

            try
            {

                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                    _OPStock.MRP = Convert.ToDouble(dgvMPMSVCMain.Rows[mrowindex].Cells["Col_MRP"].Value.ToString());
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null)
                    mmamt = Convert.ToDouble(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString());
                _OPStock.ProductID = dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                dgvMPMSVCMain.Enabled = false;
                pnlProductDetail.BringToFront();
                pnlProductDetail.Visible = true;
                pnlProductDetail.Enabled = true;
                _OpStockMode = OpStockMode.Edit;  // [ansuman] [8.11.2016]
                pnlEditProduct.BringToFront();
                pnlEditProduct.Visible = true;
                pnlEditProduct.Enabled = false;
                FillPnlEditProduct();
                FillProductAndCmpnyData(dgvMPMSVCMain.SelectedRow.Cells[0].Value.ToString()); // end [ansuman]
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
                //if (selectedRow.Cells["Col_ScanCode"].Value != null)
                //    txtScanCode.Text = selectedRow.Cells["Col_ScanCode"].Value.ToString();
                if (mcbShelf.SelectedID != null && mcbShelf.SelectedID != string.Empty && string.IsNullOrEmpty(txtShelfCode.Text) == true)
                    txtShelfCode.Text = mcbShelf.SeletedItem.ItemData[1].ToString();
                if (_Mode == OperationMode.View)
                    MakePnlProductDetailsReadOnly();
                else
                    txtQuantity.Focus();
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region Contruct Grid
        private void ConstructMainColumns()
        {
            dgvMPMSVCMain.ColumnsMain.Clear();
            DataGridViewTextBoxColumn column;
            //0
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProductID";
            column.DataPropertyName = "ProductID";
            column.HeaderText = "ProductID";
            column.Visible = false;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //1
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProductName";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "ProductName";
            column.Width = 200;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //2
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UnitOfMeasure";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 50;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //3
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Pack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 60;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            //column = new DataGridViewTextBoxColumn();  // [ansuman]  [14.11.2016]
            //         column.Name = "Col_ProdPackType";
            //         column.DataPropertyName = "ProdPackType";
            //         column.HeaderText = "PackType";
            //         column.Width = 65;
            //         column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //         column.ReadOnly = true;
            //         dgvMPMSVCMain.ColumnsMain.Add(column);

            //4
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Company";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Co.";
            column.Width = 40;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            // column.Visible = false;		
            dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Shelf";
            column.DataPropertyName = "ShelfCode";
            column.HeaderText = "Shelf";
            column.Width = 40;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Drug";
            column.DataPropertyName = "GenericCategoryName";
            column.HeaderText = "Content";
            column.Width = 70;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //5
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdVATPer";
            column.DataPropertyName = "ProdVATPercent";
            column.HeaderText = "VAT %";
            column.Width = 60;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingStk";
            //column.DataPropertyName = "ClosingStock";
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //6
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Quantity";
            column.DataPropertyName = "Quantity";
            column.HeaderText = "Qty";
            column.Width = 60;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //7
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_BatchNumber";
            column.DataPropertyName = "BatchNumber";
            column.HeaderText = "Batch";
            column.Width = 130;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //8
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Expiry";
            column.DataPropertyName = "Expiry";
            column.HeaderText = "Exp";
            column.Width = 50;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //9
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TradeRate";
            column.DataPropertyName = "TradeRate";//SaleRate?
            column.HeaderText = "Trade.Rate";
            column.Width = 80;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.DefaultCellStyle.Format = "N2";
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //10
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_MRP";
            column.DataPropertyName = "MRP";
            column.HeaderText = "MRP";
            column.Width = 80;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            column.DefaultCellStyle.Format = "N2";
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //11
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_VAT";
            column.DataPropertyName = "PurchaseVATPercent";
            column.HeaderText = "VAT%";
            column.Width = 50;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.DefaultCellStyle.Format = "N2";
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            ////12
            //column = new DataGridViewTextBoxColumn();
            //column.Name = "Col_ItemDiscountPer";
            //column.DataPropertyName = "ItemDiscountPercent";//?
            //column.HeaderText = "Disc";
            //column.Width = 50;
            //column.DefaultCellStyle.Format = "N2";
            //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //column.ReadOnly = true;
            //column.Visible = false;
            //dgvMPMSVCMain.ColumnsMain.Add(column);
            //13
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.DataPropertyName = "Amount";
            column.HeaderText = "Amount";
            column.Width = 100;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //13

            //17
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_VATAmountPurchase";
            column.DataPropertyName = "AmountPurchaseVAT";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //18
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_CSTAmount";
            column.DataPropertyName = "AmountCST";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //19
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_CSTPer";
            column.DataPropertyName = "CSTPercent";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            ////20
            //column = new DataGridViewTextBoxColumn();
            //column.Name = "Col_ItemSCMDiscountAmount";
            //column.DataPropertyName = "AmountSchemeDiscount";
            //column.Width = 80;
            //column.Visible = false;
            //dgvMPMSVCMain.ColumnsMain.Add(column);
            ////21
            //column = new DataGridViewTextBoxColumn();
            //column.Name = "Col_ItemSCMDiscountAmountPerUnit";
            //column.DataPropertyName = "SchemeDiscountPercent";
            //column.Width = 80;
            //column.Visible = false;
            //dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ExpiryDate";
            column.DataPropertyName = "ExpiryDate";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_IfOctroi";
            column.DataPropertyName = "ProdIfOctroi";
            column.Width = 40;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OPStockRate";
            column.DataPropertyName = "PurchaseRate";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_SaleRate";
            column.DataPropertyName = "SaleRate";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_CompID";
            column.DataPropertyName = "ProdCompID";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_VATAmountSale";
            column.DataPropertyName = "AmountProdVAT";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdClosingStock";
            column.DataPropertyName = "ProdClosingStock";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            //column = new DataGridViewTextBoxColumn();
            //column.Name = "Col_ShelfCode";
            //column.DataPropertyName = "ShelfCode";
            //column.Width = 80;
            //column.Visible = false;
            //dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ShelfID";
            column.DataPropertyName = "ProdShelfID";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_StockID";
            column.DataPropertyName = "StockID";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();  // [09.02.2017]
            column.Name = "Col_ScanBarCode";
            column.DataPropertyName = "ScanCode";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);
        }
        private void ConstructSubColumns()
        {
            //ProductID,ProdName,ProdPack, ProdCompShortName
            dgvMPMSVCMain.ColumnsSub.Clear();

            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "ProductID";
            column.HeaderText = "ID";
            column.Visible = false;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdName";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "ProdName";
            column.Width = 230;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdLoosePack";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 50;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdPack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 65;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Comp";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 50;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Shelf";
            column.DataPropertyName = "ShelfCode";
            column.HeaderText = "Shelf";
            column.Width = 50;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Drug";
            column.DataPropertyName = "GenericCategoryName";
            column.HeaderText = "Content";
            column.Width = 150;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);
            column = new DataGridViewTextBoxColumn();

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdVATPer";
            column.DataPropertyName = "ProdVATPercent";
            column.HeaderText = "VAT %";
            column.Width = 50;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingStock";
            column.DataPropertyName = "ProdClosingStock";
            column.HeaderText = "CL.Stock";
            column.Width = 70;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

        }
        private void ConstructBatchGrid()
        {
            DataGridViewTextBoxColumn column;
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
            column.DefaultCellStyle.Format = "N2";
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBatchGrid.Columns.Add(column);
            //3
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_MRP";
            column.DataPropertyName = "MRP";
            column.HeaderText = "MRP";
            column.Width = 70;
            column.DefaultCellStyle.Format = "N2";
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBatchGrid.Columns.Add(column);
            //4
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_SaleRate";
            column.DataPropertyName = "SaleRate";
            column.HeaderText = "SaleRate";
            column.Width = 70;
            column.DefaultCellStyle.Format = "N2";
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
            column.Width = 149;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBatchGrid.Columns.Add(column);
            //7
            //Additional columns needed

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ExpiryDate";
            column.DataPropertyName = "ExpiryDate";
            column.Visible = false;
            dgvBatchGrid.Columns.Add(column);
            //8
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OPStockRate";
            column.DataPropertyName = "PurchaseRate";
            column.Width = 70;
            column.Visible = false;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBatchGrid.Columns.Add(column);
            //8
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OPStockVATPer";
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

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccountID";
            column.DataPropertyName = "AccountID";
            column.Visible = false;
            dgvBatchGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_StockID";
            column.DataPropertyName = "StockID";
            column.Width = 70;
            column.Visible = false;
            dgvBatchGrid.Columns.Add(column);

        }
        private void InitializeMainSubViewControl()
        {
            try
            {
                ConstructMainColumns();
                ConstructSubColumns();
                ConstructBatchGrid();
                if (dgvMPMSVCMain.Rows.Count == 0)
                    dgvMPMSVCMain.Rows.Add();
                DataTable dtable = new DataTable();
                dtable = _OPStock.ReadProductDetailsByID();
                dgvMPMSVCMain.DataSourceMain = dtable;

                // sheela 9/11/2016
                FormatGrid(); // 
                Product prod = new Product();
                DataTable proddt = prod.GetOverviewData();
                //  DataTable dt = General.ProductList;
                dgvMPMSVCMain.DataSource = proddt;
                string tempFileName = General.GetOpeningStockTempFile(); // sheela 9/11/2016
                if (_Mode == OperationMode.Add && File.Exists(tempFileName))
                {
                    dgvMPMSVCMain.DataSourceMain = null;
                    dgvMPMSVCMain.Rows.Clear();

                    DataSet ds = new DataSet();
                    ds.ReadXml(tempFileName);
                    dgvMPMSVCMain.DataSourceMain = ds.Tables[0];
                    dgvMPMSVCMain.Bind();
                    dgvMPMSVCMain.IsAllowNewRow = true;
                    if (_OPStock.AddNewRowCheck(dgvMPMSVCMain))
                        dgvMPMSVCMain.Rows.Add(1);

                    //  dgvMPMSVCMain.AddRowsInStockTempTable();
                    //  CalculateAmount();
                    // CalculateAllAmounts();
                    CalculateTotals();
                }
                else
                {
                    //Product prod = new Product();
                    //DataTable proddt = prod.GetOverviewData();
                    ////  DataTable dt = General.ProductList;
                    //dgvMPMSVCMain.DataSource = proddt;

                    dgvMPMSVCMain.Bind();
                    BindMainProductGrid();      // [ansuman]
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void FormatGrid()
        {
            dgvMPMSVCMain.NumericColumnNames.Add("Col_Quantity");
            dgvMPMSVCMain.DoubleColumnNames.Add("Col_MRP");
            dgvMPMSVCMain.NumericColumnNames.Add("Col_Quantity");
            dgvMPMSVCMain.DoubleColumnNames.Add("Col_VATPer");
            dgvMPMSVCMain.DoubleColumnNames.Add("Col_OPStockRate");
            dgvMPMSVCMain.DoubleColumnNames.Add("Col_Amount");
            dgvMPMSVCMain.DoubleColumnNames.Add("Col_SaleRate");
            dgvMPMSVCMain.DoubleColumnNames.Add("Col_TradeRate");
            dgvMPMSVCMain.DoubleColumnNames.Add("Col_Amount");
            dgvMPMSVCMain.DoubleColumnNames.Add("Col_VAT");
        }
        // sheela 9/11/2016
        #endregion Construct Grid

        #region fill or clean
        private void ClearControls()
        {
            try
            {
                tsBtnFifth.Visible = false;
                txtVouchernumber.Clear();
                txtVouType.Text = _OPStock.VoucherType;
                txtVoucherSeries.Text = _OPStock.VoucherSeries;
                txtVouchernumber.BackColor = Color.White;
                datePickerBillDate.Enabled = true;
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                txtQuantity.Text = "0";
                txtPurchaseRate.Text = "0.00";
                txtPurchaseVATAmt.Text = "0.00";
                txtAmount.Text = "0.00";
                txtNoOfRows.Text = "";
                txtAmount.Text = "";
                txtGridAmountTot.Text = "0.00";
                dgvMPMSVCMain.Enabled = true;
                pnlProductDetail.SendToBack();
                pnlBatchGrid.Visible = false;
                dgvBatchGrid.Visible = false;
                dgvMPMSVCMain.BringToFront();
                lblMessage.Text = "";
                if (General.CurrentSetting.MsetPurchaseChangeSaleRate == "Y")
                {
                    txtSaleRate.Enabled = true;
                    txtSaleRate.ReadOnly = false;
                }
                else
                {
                    txtSaleRate.Enabled = false;
                    txtSaleRate.ReadOnly = true;
                }
                if (_Mode == OperationMode.View)
                    btnCancel.Visible = false;
                else
                    btnCancel.Visible = true;

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        //private DataTable FillProductData()
        //{
        //    DataTable dtable = new DataTable();
        //    try
        //    {

        //        dtable = General.ProductList;
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //    return dtable;
        //}
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
            bool retValue = false;
            DataRow dr = null;
            DataRow invdr = null;
            string mbatchno = "";
            string mprodno = "";
            string mmrp = "";
            string mshelfcode = "";
            string mshelfID = "";
            int mprodclosingstock = 0;

            //    int mclosingstock = 0;
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
            string mstockid = "";

            Product drprod = new Product();
            try
            {
                dr = drprod.ReadLastPurchaseByID(dgvMPMSVCMain.MainDataGridCurrentRow.Cells[0].Value.ToString());
                mprodno = dgvMPMSVCMain.MainDataGridCurrentRow.Cells[0].Value.ToString();
                if (dr["ProdLastPurchaseStockID"] != DBNull.Value)
                    mstockid = dr["ProdLastPurchaseStockID"].ToString();
                if (dr["ProdLastPurchaseBatchNumber"] != DBNull.Value)
                    mbatchno = dr["ProdLastPurchaseBatchNumber"].ToString().Trim();
                if (dr["ProdLastPurchaseMRP"] != DBNull.Value)
                {
                    double.TryParse(dr["ProdLastPurchaseMRP"].ToString(), out mmrpn);
                    mmrp = dr["ProdLastPurchaseMRP"].ToString();
                    _OPStock.MRP = mmrpn;
                }
                if (dr["ProdClosingStock"] != DBNull.Value)
                    mprodclosingstock = Convert.ToInt32(dr["ProdClosingStock"].ToString().Trim());
                if (dr["ShelfCode"] != DBNull.Value)
                    mshelfcode = (dr["ShelfCode"].ToString().Trim());
                if (dr["ProdShelfID"] != DBNull.Value)
                    mshelfID = dr["ProdShelfID"].ToString().Trim();
                if (dr["ProdLastPurchaseExpiry"] != DBNull.Value)
                    mexpiry = dr["ProdLastPurchaseExpiry"].ToString().Trim();
                // if (dr["ProdLastPurchaseExpiryDate"] != DBNull.Value)
                // mexpirydate = dr["ProdLastPurchaseExpiryDate"].ToString().Trim();
                if (string.IsNullOrEmpty(mexpiry) == false && mexpiry != "00/00")
                    mexpiry = General.GetValidExpiry(mexpiry);
                if (string.IsNullOrEmpty(mexpiry) == false && mexpiry != "00/00")
                    mexpirydate = General.GetValidExpiryDate(mexpiry);
                else
                    mexpirydate = "";
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
                //if (dr["ProdVATPercent"] != DBNull.Value)
                //    double.TryParse(dr["ProdLastPurchaseCST"].ToString(), out mprodvatper);
                //mpurvatper = mprodvatper;
                //if (dr["ProdLastPurchaseVATPer"] != DBNull.Value)
                //    double.TryParse(dr["ProdLastPurchaseVATPer"].ToString(), out mpurvatper);
                //if (dr["ProdVATPercent"] != DBNull.Value)
                //    double.TryParse(dr["ProdVATPercent"].ToString(), out mprodvatper);
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value != null)
                    double.TryParse(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value.ToString(), out mprodvatper);
                mpurvatper = mprodvatper;
                mpurvatamt = Math.Round((mtraderate * mpurvatper) / 100, 2);
                mprodvatamt = Math.Round((mmrpn * mprodvatper) / 100, 2);
                //if (mpurrate == 0)
                mpurrate = mtraderate + mprodvatamt;
                if (dr["ProdLastPurchaseItemDiscPer"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseItemDiscPer"].ToString(), out mitemdiscper);
                if (mitemdiscper > 0)
                    mitemdiscamt = Math.Round((mtraderate * mitemdiscper) / 100, 2);


                txtBatch.Text = mbatchno;
                txtExpiry.Text = mexpiry;
                txtExpiryDate.Text = mexpirydate;
                txtTradeRate.Text = mtraderate.ToString("#0.00");
                txtPurchaseRate.ReadOnly = false;
                txtPurchaseRate.Enabled = true;
                txtPurchaseRate.Text = mpurrate.ToString("#0.00");
                txtPurchaseRate.ReadOnly = true;
                txtPurchaseRate.Enabled = false;
                txtStockID.Text = mstockid.ToString();
                //   txtDiscountPer.Text = Convert.ToString(mitemdiscper.ToString("#0.00")).Trim();
                //    txtDiscountAmt.Text = Convert.ToString(mitemdiscamt.ToString("#0.00")).Trim();
                txtPurchaseVATPer.Text = Convert.ToString(mpurvatper.ToString("#0.00"));
                txtPurchaseVATAmt.ReadOnly = false;
                txtPurchaseVATAmt.Enabled = true;
                txtPurchaseVATAmt.Text = Convert.ToString(mpurvatamt.ToString("#0.00"));
                txtPurchaseVATAmt.Enabled = false;
                txtPurchaseVATAmt.ReadOnly = true;
                //txtPurchaseRate.Text = Convert.ToString(mpurrate.ToString("#0.00")).Trim();
                txtMRP.Text = Convert.ToString(mmrpn.ToString("#0.00")).Trim();
                txtSaleRate.Text = Convert.ToString(msalerate.ToString("#0.00")).Trim();
                txtMasterVATPer.Text = mprodvatper.ToString("#0.00");
                txtMasterVATAmt.Text = mprodvatamt.ToString("#0.0000");
                //  mcbShelf.SelectedID = mshelfID;
                SsStock invss = new SsStock();
                invdr = invss.GetStockByProductIDAndBatchNumberAndMRP(mprodno, mbatchno, mmrp);

                if (invdr != null)
                {
                    ////////////int.TryParse(invdr["ClosingStock"].ToString().Trim(), out mclosingstock);
                    mexpiry = invdr["Expiry"].ToString().Trim();
                    mexpirydate = invdr["ExpiryDate"].ToString().Trim();
                    double.TryParse(invdr["MRP"].ToString().Trim(), out mmrpn);
                    double.TryParse(invdr["SaleRate"].ToString().Trim(), out msalerate);
                    double.TryParse(invdr["PurchaseRate"].ToString().Trim(), out mpurrate);
                    double.TryParse(invdr["TradeRate"].ToString().Trim(), out mtraderate);
                    mstockid = invdr["StockID"].ToString();

                    // ask Vijay 
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[5].Value = mbatchno;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[6].Value = mexpiry;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[7].Value = mmrp;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[8].Value = msalerate;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[12].Value = mpurrate;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[16].Value = mtraderate;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[13].Value = mexpirydate;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[17].Value = mclosingstock;

                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        #region FillEditProductPanel

        public void FillPnlEditProduct()  // [ansuman] [05.11.2016]
        {
            if (pnlProductDetail.Visible == true)
                lblEditProductTitle.Text = "Edit Product Details";

            txtProdName.Text = dgvMPMSVCMain.MainDataGridCurrentRow.Cells[1].Value.ToString();
            txtUOM.Text = dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_UnitOfMeasure"].Value.ToString();
            if (mcbShelfNoOpStock.SelectedID != null && mcbShelfNoOpStock.SelectedID != "")    // [14.11.2016]
                txtShelfCode.Text = mcbShelfNoOpStock.SeletedItem.ItemData[1].ToString();
            txtProdName.Focus();
        }

        #endregion

        private bool FillDataFromMPSVRow()
        {

            DataRow invdr = null;
            string mbatchno = "";
            string mprodno = "";
            string mmrp = "";
            string mshelfcode = "";
            string mshelfID = "";
            int mqty = 0;
            string mexpirydate = "";
            string mexpiry = "";
            double mmrpn = 0;
            double mpurrate = 0;
            double mtraderate = 0;
            double msalerate = 0;
            double mcstper = 0;
            double mcstamt = 0;
            double mpurvatper = 0;
            double mpurvatamt = 0;
            double mprodvatper = 0;
            double mprodvatamt = 0;
            double mitemdiscper = 0;
            double mitemdiscamt = 0;
            double mamt = 0;
            string mstockid = "";

            Product drprod = new Product();
            DataRow dr = null;
            try
            {
                dr = drprod.ReadLastPurchaseByID(dgvMPMSVCMain.MainDataGridCurrentRow.Cells[0].Value.ToString());
                //if (dr["ProdClosingStock"].ToString() != null)
                //    mprodclosingstock = Convert.ToInt32(dr["ProdClosingStock"].ToString().Trim());
                mprodno = dgvMPMSVCMain.MainDataGridCurrentRow.Cells[0].Value.ToString();
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Shelf"].Value != null)
                    mshelfcode = dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Shelf"].Value.ToString().Trim();
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value != null)
                    mshelfID = dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value.ToString().Trim();
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    mqty = Convert.ToInt32(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                    mbatchno = dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString();
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                {
                    mmrpn = Convert.ToDouble(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
                    mmrp = dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString();
                }
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null)
                    mexpiry = dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString();
                mexpiry = General.GetValidExpiry(mexpiry);
                mexpirydate = General.GetValidExpiryDate(mexpiry);
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_OPStockRate"].Value != null)
                    mpurrate = Convert.ToDouble(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_OPStockRate"].Value.ToString());
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value != null)
                    mtraderate = Convert.ToDouble(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value.ToString());
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value != null)
                    msalerate = Convert.ToDouble(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value.ToString());
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value != null)
                    mcstamt = Convert.ToDouble(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value.ToString());
                if (string.IsNullOrEmpty(Convert.ToString(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value)) == false)
                    mcstper = Convert.ToDouble(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value.ToString());
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_VAT"].Value != null && dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_VAT"].Value.ToString() != "")
                    mpurvatper = Convert.ToDouble(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_VAT"].Value.ToString());
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value != null)
                    mprodvatper = Convert.ToDouble(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value.ToString());
                mpurvatamt = Math.Round((mtraderate * mpurvatper) / 100, 2);
                mprodvatamt = Math.Round((mmrpn * mprodvatper) / 100, 2);
                mitemdiscamt = Math.Round((mtraderate * mitemdiscper) / 100, 2);
                mamt = Math.Round((mtraderate * mqty), 2);

                txtQuantity.Text = mqty.ToString("#0");
                txtAmount.Text = mamt.ToString("#0.00");
                txtBatch.Text = mbatchno;
                txtExpiry.Text = mexpiry;
                txtExpiryDate.Text = mexpirydate;
                txtTradeRate.Text = mtraderate.ToString("#0.00");
                mcbShelf.SelectedID = mshelfID;
                txtPurchaseVATPer.Text = mpurvatper.ToString("#0.00");
                txtPurchaseVATAmt.Text = mpurvatamt.ToString("#0.00");
                txtPurchaseRate.Text = mpurrate.ToString("#0.00");
                txtMRP.Text = mmrpn.ToString("#0.00");
                txtSaleRate.Text = msalerate.ToString("#0.00");
                txtMasterVATPer.Text = mprodvatper.ToString("#0.00");
                txtMasterVATAmt.Text = mprodvatamt.ToString("#0.00");
                SsStock invss = new SsStock();
                invdr = invss.GetStockByProductIDAndBatchNumberAndMRP(mprodno, mbatchno, mmrp);

                if (invdr != null)
                {
                    ////////////int.TryParse(invdr["ClosingStock"].ToString().Trim(), out mclosingstock);
                    mexpiry = invdr["Expiry"].ToString().Trim();
                    mexpirydate = invdr["ExpiryDate"].ToString().Trim();
                    double.TryParse(invdr["MRP"].ToString().Trim(), out mmrpn);
                    double.TryParse(invdr["SaleRate"].ToString().Trim(), out msalerate);
                    double.TryParse(invdr["PurchaseRate"].ToString().Trim(), out mpurrate);
                    double.TryParse(invdr["TradeRate"].ToString().Trim(), out mtraderate);
                    mstockid = invdr["StockID"].ToString();

                    // ask Vijay 
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[5].Value = mbatchno;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[6].Value = mexpiry;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[7].Value = mmrp;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[8].Value = msalerate;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[12].Value = mpurrate;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[16].Value = mtraderate;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[13].Value = mexpirydate;
                    ////////////mpPVC1.MainDataGridCurrentRow.Cells[17].Value = mclosingstock;

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        private void FillBatchGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                SsStock invss = new SsStock();
                dt = invss.GetStockByProductIDForPurchase(dgvMPMSVCMain.MainDataGridCurrentRow.Cells[0].Value.ToString(), 0);
                dgvBatchGrid.DataSource = dt;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ClearpnlProductDetail()
        {
            txtQuantity.Text = "0";
            txtBatch.Text = "";
            txtExpiry.Text = "";
            txtMRP.Text = "0.00";
            txtTradeRate.Text = "0.00";
            txtPurchaseVATAmt.Text = "0.00";
            txtPurchaseVATPer.Text = "0.00";
            txtShelfCode.Text = "";
            txtExpiryDate.Text = "";
            txtCSTAmount.Text = "0.00";
            txtCSTPer.Text = "0.00";
            txtPurchaseRate.Text = "0.00";
            txtSaleRate.Text = "0.00";
            txtAmount.Text = "0.00";
            txtScanCode.Text = "";

            txtMasterVATAmt.Text = "0.00";
            txtMasterVATPer.Text = "0.00";
        }
        private void MakePnlProductDetailsReadOnly()
        {
            txtQuantity.Enabled = false;
            txtBatch.Enabled = false;
            txtExpiry.Enabled = false;
            txtTradeRate.Enabled = false;
            txtPurchaseRate.Enabled = false;
            txtMasterVATPer.Enabled = false;
            txtMRP.Enabled = false;
            txtCSTAmount.Enabled = false;
            txtCSTPer.Enabled = false;
            txtPurchaseVATPer.Enabled = false;
            mcbShelf.Enabled = false;
            txtScanCode.Enabled = false;
            btnOK.Focus();
        }

        private void MakePnlProductDetailsReadOnlyFalse()
        {
            txtQuantity.Enabled = true;
            txtBatch.Enabled = true;
            txtExpiry.Enabled = true;
            txtTradeRate.Enabled = true;
            txtPurchaseRate.Enabled = true;
            txtMasterVATPer.Enabled = true;
            txtMRP.Enabled = true;
            txtCSTAmount.Enabled = true;
            txtCSTPer.Enabled = true;
            txtPurchaseVATPer.Enabled = true;
            mcbShelf.Enabled = true;
            txtScanCode.Enabled = true;
        }
        #endregion

        #region keydown-Click-DoubleClick

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int mqty = 0;
                try
                {
                    if (txtQuantity.Text != null)
                        int.TryParse(txtQuantity.Text.ToString(), out mqty);
                    int mrowcount = 0;
                    mrowcount = dgvBatchGrid.RowCount;
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (mqty != 0)
                        {
                            if (mrowcount > 0)
                            {
                                pnlBatchGrid.BringToFront();
                                pnlBatchGrid.Visible = true;
                                pnlBatchGrid.Enabled = true;
                                dgvBatchGrid.Visible = true;
                                dgvBatchGrid.Enabled = true;
                                pnlProductDetail.Enabled = false;
                                CalculatePurRateSaleRateAndAmount();
                                dgvBatchGrid.Focus();
                            }
                            else
                            {
                                CalculatePurRateSaleRateAndAmount();
                                lblMessage.Text = "No Batch Data ";
                                txtBatch.Focus();
                                txtBatch.SelectAll();
                            }

                            // CalculatePurRateSaleRateAndAmount();
                        }
                        //else if (e.KeyCode == Keys.Escape)
                        //    btnCancelClick();
                    }
                    else
                        if (e.KeyCode == Keys.Escape)
                        btnCancelClick();
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
            else if (e.KeyCode == Keys.Menu)
            {
                DOProductEdit();
                _OpStockMode = OpStockMode.Edit;
            }


        }
        private void DOProductEdit()
        {
            pnlEditProduct.Visible = true;
            pnlEditProduct.BringToFront();
            pnlEditProduct.Enabled = true;
            //FillCompanyCombo();
            //FillGenericCategoryCombo();
            //FillProdCategoryCombo(); // [14.11.2016]
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
        private void dgvBatchGrid_KeyDown(object sender, KeyEventArgs e)
        {

            double mqty = 0;
            try
            {
                double.TryParse(txtQuantity.Text.ToString(), out mqty);
                if (e.KeyCode == Keys.Escape)
                {
                    pnlBatchGrid.Visible = false;
                    pnlBatchGrid.SendToBack();
                    pnlProductDetail.BringToFront();
                    pnlProductDetail.Enabled = true;
                    dgvMPMSVCMain.Enabled = true;
                    txtBatch.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    BatchGridDone();
                    // btnOK.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void dgvBatchGrid_DoubleClick(object sender, EventArgs e)
        {
            BatchGridDone();
            txtQuantity.Focus();
        }
        private void BatchGridDone()
        {
            double mtraderate = 0;
            double mpurvatper = 0;
            double mcstper = 0;
            double mmstper = 0;
            double mqty = 0;
            double mpurrate = 0;
            double mmrp = 0;
            double.TryParse(txtQuantity.Text.ToString(), out mqty);
            pnlBatchGrid.Visible = false;
            dgvBatchGrid.Visible = false;
            dgvBatchGrid.SendToBack();
            pnlProductDetail.BringToFront();
            pnlProductDetail.Enabled = true;
            dgvMPMSVCMain.Enabled = true;

            try
            {
                if (dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value != null)
                    txtBatch.Text = dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value.ToString();
                if (txtBatch.Text.ToString() == "NEW")
                {
                    txtBatch.Text = "";
                    txtBatch.Focus();
                    txtStockID.Text = string.Empty;
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
                        txtMRP.Text = dgvBatchGrid.CurrentRow.Cells["Col_MRP"].Value.ToString();
                        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
                    }
                    if (dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value != null)
                    {
                        txtTradeRate.Text = dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value.ToString();
                        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                    }
                    if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value != null)
                    {
                        txtMasterVATPer.Text = dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value.ToString();
                        double.TryParse(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value.ToString(), out mmstper);
                        txtMasterVATAmt.Text = Math.Round(mmrp * mmstper / 100, 2).ToString();
                    }
                    else
                        if (dgvBatchGrid.CurrentRow.Cells["Col_OPStockVATPer"].Value != null)
                    {
                        txtPurchaseVATPer.Text = dgvBatchGrid.CurrentRow.Cells["Col_OPStockVATPer"].Value.ToString();
                        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_OPStockVATPer"].Value.ToString(), out mmstper);
                    }
                    mpurvatper = mmstper;
                    txtPurchaseVATPer.Text = mpurvatper.ToString("#0.00");
                    txtPurchaseVATAmt.Text = Math.Round(mtraderate * mpurvatper / 100, 2).ToString();
                    //}
                    if (dgvBatchGrid.CurrentRow.Cells["Col_OPStockRate"].Value != null)
                        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_OPStockRate"].Value.ToString(), out mpurrate);
                    if (mpurrate == 0)
                        mpurrate = mtraderate;
                    txtPurchaseRate.Text = mpurrate.ToString();
                    if (dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value != null)
                        txtSaleRate.Text = dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value.ToString();
                    if (dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value != null)
                    {
                        txtCSTPer.Text = dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value.ToString();
                        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value.ToString(), out mcstper);
                        txtCSTAmount.Text = Math.Round(mtraderate * mcstper / 100, 2).ToString();
                    }
                    if (dgvBatchGrid.CurrentRow.Cells["Col_ScanCode"].Value != null)
                        txtScanCode.Text = dgvBatchGrid.CurrentRow.Cells["Col_ScanCode"].Value.ToString();
                    dgvBatchGrid.Visible = false;

                    txtAmount.Text = Math.Round(mtraderate * mqty).ToString();
                    btnOK.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancelClick();
            ClearStockData();  // [anusman] [08.11.2016]
            _OpStockMode = OpStockMode.Add;
            //    FillCompanyCombo();
            mcbCompany1.SelectedID = "";
            //    FillGenericCategoryCombo();
            mcbGenCatOpStock.SelectedID = "";
            //     FillProdCategoryCombo();    // [14.11.2016]
            mcbProductCategory1.SelectedID = ""; // [14.11.2016]
                                                 //    FillShelfComboList();
            mcbShelfNoOpStock.SelectedID = "";
            //     FillScheduleDrugCombo();
            mcbSchedule1.SelectedItem = "";
        }
        private void btnCancel_KeyDown(object sender, KeyEventArgs e)
        {
            btnCancelClick();
        }

        private void btnCancelClick()
        {
            double mamt = 0;
            dgvMPMSVCMain.Enabled = true;
            try
            {
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null)
                    double.TryParse(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString(), out mamt);
                pnlProductDetail.SendToBack();
                pnlProductDetail.Visible = false;
                pnlEditProduct.SendToBack();        // [ansuman]
                pnlEditProduct.Visible = false;     // [ansuman]
                ClearpnlProductDetail();
                //kiran
                int index = dgvMPMSVCMain.MainDataGridCurrentRow.Index;
                if (mamt == 0)
                {
                    dgvMPMSVCMain.Rows.RemoveAt(0);
                    index = dgvMPMSVCMain.Rows.Add();
                }
                dgvMPMSVCMain.Focus();
                dgvMPMSVCMain.SetFocus(index, 1);
                CalculateTotals();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            bool retvalue = ButtonOKClick();
            if (retvalue)
            {
                ClearStockData();  // [ansuman] [08.11.2016]

                if (dgvMPMSVCMain.DataSourceMain != null)
                    dgvMPMSVCMain.DataSourceMain = null;

                BindMainProductGrid();
            }   // [ansuman]
        }
        private bool ButtonOKClick()
        {
            bool retValue = DoValidation();
            if (retValue)
            {
                //  lblExpired.Text = "";
                string ScannedBarcode = txtScanCode.Text;
                lblMessage.Text = "";
                pnlProductDetail.SendToBack();
                pnlProductDetail.Visible = false;
                pnlProductDetail.Enabled = true;
                pnlEditProduct.SendToBack();
                pnlEditProduct.Visible = false;
                pnlEditProduct.Enabled = true;
                dgvBatchGrid.Enabled = true;
                try
                {
                    if (_Mode == OperationMode.Add)
                    {
                        CalculatePurRateSaleRateAndAmount();
                        int mqty = 0;
                        int.TryParse(txtQuantity.Text.ToString(), out mqty);
                        // [ansuman]
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = txtProdName.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProductName"].ReadOnly = true;
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Company"].Value = txtCompShortName1.Text;  // [14.11.2016]

                        if (mcbGenCatOpStock.SeletedItem != null)
                            dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Drug"].Value = mcbGenCatOpStock.SeletedItem.ItemData[1].ToString();
                        else
                            dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Drug"].Value = "";

                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_UnitOfMeasure"].Value = txtUOM.Text.ToString();
                        //dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProdPackType"].Value = txtPackType1.Text.ToString(); // [14.11.2016]
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Pack"].Value = txtPack1.Text.ToString();
                        if (mcbShelfNoOpStock.SeletedItem != null)      // [14.11.2016]
                            dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Shelf"].Value = mcbShelfNoOpStock.SeletedItem.ItemData[1].ToString();
                        else
                            dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Shelf"].Value = "";
                        // [end]
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = txtQuantity.Text.ToString();
                        pobj.ProdClosingStock = Convert.ToInt32(txtQuantity.Text);  // [ansuman] [14.11.2016]
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = txtBatch.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = txtExpiry.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = txtExpiryDate.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value = txtTradeRate.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_MRP"].Value = txtMRP.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_OPStockRate"].Value = txtPurchaseRate.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = txtSaleRate.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_VAT"].Value = txtPurchaseVATPer.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_VATAmountPurchase"].Value = txtPurchaseVATAmt.Text.ToString();

                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Amount"].Value = txtAmount.Text.ToString();

                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value = txtCSTAmount.Text.ToString();
                        //dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value = txtMasterVATPer.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value = txtPurchaseVATPer.Text.ToString(); //Sheela madam
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_VATAmountSale"].Value = txtMasterVATAmt.Text.ToString();
                        if (mcbShelf.SelectedID != null && mcbShelf.SelectedID != string.Empty)
                            dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value = mcbShelf.SeletedItem.ItemData[0].ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_StockID"].Value = txtStockID.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ScanBarCode"].Value = txtScanCode.Text.ToString();  // [09.02.2017]
                        ClearpnlProductDetail();
                        dgvMPMSVCMain.Enabled = true;
                        if (mqty == 0)
                            dgvMPMSVCMain.Rows.Remove(dgvMPMSVCMain.MainDataGridCurrentRow);


                        if (IfEditPreviousRow == "N")
                        {
                            int rowID = dgvMPMSVCMain.Rows.Add();
                            dgvMPMSVCMain.SetFocus(rowID, 1);
                        }
                        else
                        {
                            int rowcnt = dgvMPMSVCMain.Rows.Count;
                            int rowID = dgvMPMSVCMain.MainDataGridCurrentRow.Index + 1;
                            if (rowID >= rowcnt && dgvMPMSVCMain.Rows[rowcnt - 1].Cells[0].Value != null)
                                rowID = dgvMPMSVCMain.Rows.Add();
                            dgvMPMSVCMain.SetFocus(rowID, 1);
                        }

                        DataTable dt = dgvMPMSVCMain.GetGridData();
                        if (dt.Rows.Count > 0)
                            dt.WriteXml(General.GetOpeningStockTempFile()); // sheela 9/11/2016

                        CalculateTotals();
                    }
                    else
                    {
                        dgvMPMSVCMain.ColumnsMain["col_ProductName"].ReadOnly = false;
                        dgvMPMSVCMain.Enabled = true;
                        dgvMPMSVCMain.SetFocus(dgvMPMSVCMain.MainDataGridCurrentRow.Index, 1);
                        dgvMPMSVCMain.ColumnsMain["Col_ProductName"].ReadOnly = true;
                    }
                    // Update Self Number in ProductMaster
                    //SS
                    if (pobj.ProdShelfID == 0  &&  mcbShelf.SeletedItem != null && Convert.ToInt32 (mcbShelf.SeletedItem.ItemData[0].ToString()) != 0)
                    {
                        pobj.ProdShelfID = Convert.ToInt32(mcbShelf.SeletedItem.ItemData[0].ToString());
                        bool SvaeFlag = pobj.UpdateProdSelfID();
                        if (SvaeFlag)
                            CacheObject.Clear("cacheCounterSale");
                    }

                    ////Update Barcode InProductMaster
                    //if (string.IsNullOrEmpty(pobj.ScannedBarcode) == true && string.IsNullOrEmpty(ScannedBarcode) == false)
                    //{
                    //    pobj.ScannedBarcode = ScannedBarcode;
                    //    bool SaveFlag = pobj.UpdateProductScanCode();
                    //    if (SaveFlag)
                    //        CacheObject.Clear("cacheCounterSale");
                    //}

                    dgvBatchGrid.Visible = false; // [ansuman]
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
            return retValue;
        }
        private bool DoValidation()
        {
            try
            {
                bool retValue = true;
                string err = string.Empty;
                retValue = CheckForNegativeStock(out err);
                double purvat = Convert.ToDouble(txtPurchaseVATPer.Text.ToString());
                if (purvat != 0 && purvat != 6 && purvat != 13.5)
                {
                    retValue = false;
                    err = "Check VAT %";
                    txtPurchaseVATPer.Focus();
                }

                if (txtBatch.Text == null || txtBatch.Text.ToString() == string.Empty)
                {
                    retValue = false;
                    err = "Check Batch :";
                    txtBatch.Focus();
                }
                if (txtExpiry.Text == null || txtExpiry.Text.ToString() == string.Empty)
                {
                    retValue = false;
                    err = err + "Check Expiry :";
                    txtExpiry.Focus();
                }
                if (Convert.ToDouble(txtMRP.Text.ToString()) <= Convert.ToDouble(txtTradeRate.Text.ToString()))
                {
                    err = err + "Check MRP and Trade Rate :";
                    retValue = false;
                    txtMRP.Focus();
                }
                lblMessage.Text = err;
                return retValue;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
                return false;
            }
        }

        private bool CheckForNegativeStock(out string err)
        {
            int mQuant = 0;
            err = string.Empty;
            bool retValue = true;

            if (string.IsNullOrEmpty(txtQuantity.Text) == false)
                mQuant = Convert.ToInt32(txtQuantity.Text);
            if (mQuant <= 0)
            {
                if (mQuant == 0)
                {
                    retValue = false;
                    err = "Please Enter Quantity.";
                    txtQuantity.Focus();
                }
                else
                {
                    int ClosingQuant = 0;
                    DataTable dtTable = ((DataTable)(dgvBatchGrid.DataSource));
                    var results = from myRow in dtTable.AsEnumerable()
                                  where myRow.Field<string>("StockID") == txtStockID.Text.ToString()
                                  select myRow;
                    if (results != null)
                    {
                        DataView dr = results.AsDataView();
                        ClosingQuant = (string.IsNullOrEmpty(Convert.ToString(dr[0]["ClosingStock"])) == false) ? Convert.ToInt32(dr[0]["ClosingStock"].ToString()) : 0;
                    }
                    int TotalQuant = ClosingQuant + mQuant;
                    if (TotalQuant < 0)
                    {
                        retValue = false;
                        PSMessageBox.Show("Please Enter proper Quantity", "EcoMart", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                        err = "Please Enter proper Quantity.";
                        txtQuantity.Focus();
                    }
                }
            }
            return retValue;
        }

        private void btnOK_KeyDown(object sender, KeyEventArgs e) // [ansuman] [14.11.2016]
        {
            if (e.KeyCode == Keys.Enter)
            {
                ButtonOKClick();
            }
            else if (e.KeyCode == Keys.Up)
                txtMasterVATPer.Focus();
        }
        private void txtBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtExpiry.Focus();
                txtExpiry.SelectAll();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtQuantity.SelectAll();
                txtQuantity.Focus();
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
                txtBatch.SelectAll();
            }
        }

        private void CheckValidExpiry()
        {
            string exp = "";
            string expdate = "";
            try
            {

                if (txtExpiry.Text.ToString() == "0000")
                    txtExpiry.Text = "00/00";
                if (txtExpiry.Text.ToString() != "00/00")
                {
                    exp = General.GetValidExpiry(txtExpiry.Text.ToString().Trim());
                    txtExpiry.Text = exp;
                    if (exp == "")
                    {
                        lblMessage.Text = "Please Check Expiry";
                        txtExpiry.Focus();
                    }
                    else
                    {

                        expdate = General.GetValidExpiryDate(exp);
                        txtExpiryDate.Text = expdate;
                        string mexpdate = General.GetExpiryInyyyymmddForm(expdate);
                        DateTime dd = General.ConvertStringToDateyyyyMMdd(mexpdate);
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
                                    //  lblExpired.Text = "Expired";
                                    //  lblExpired.BackColor = Color.IndianRed;
                                    if (General.CurrentSetting.MsetPurchaseAcceptExpriedItems != "Y")
                                    {
                                        //    cbAcceptNrExpired.Checked = false;
                                        //     cbAcceptNrExpired.Enabled = false;
                                        //  CBAcceptExpiryCheckedChange();
                                        PSMessageBox.Show("Product Expired", "Invalid expiry date", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                                        txtExpiry.Focus();
                                        txtExpiry.SelectAll();
                                    }

                                }
                                else if (days < 30)
                                {
                                    PSMessageBox.Show("Near Expiry Product", "Close To Expiry ", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                                    txtMRP.Focus();
                                }
                                else if (days < 60)
                                {
                                    PSMessageBox.Show("Near Expiry Product", "Close To Expiry ", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                                    txtMRP.Focus();
                                }
                                else if (days < 90)
                                {
                                    PSMessageBox.Show("Near Expiry Product", "Close To Expiry ", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                                    txtMRP.Focus();
                                }
                            }
                            else
                            {
                                txtMRP.Focus();
                            }
                        }


                    }

                }
                else
                {
                    if (General.CurrentSetting.MsetGeneralExpiryDateReuired != "Y")
                    {
                        //  cbAcceptNrExpired.Checked = true;
                        txtExpiryDate.Text = "";
                        txtMRP.Focus();
                        btnOK.Enabled = true;
                    }
                    else
                    {
                        lblMessage.Text = "Please Check Expiry";
                        txtExpiry.Focus();
                        btnOK.Enabled = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void txtMRP_KeyDown(object sender, KeyEventArgs e)
        {
            double mtrate = 0;
            try
            {

                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                {
                    if (Convert.ToDouble(txtMRP.Text.ToString()) == 0)
                        txtMRP.Focus();
                    else
                    {
                        mtrate = Convert.ToDouble(txtTradeRate.Text.ToString());
                        //if (mtrate == 0)
                        //{
                        mtrate = Math.Round(Convert.ToDouble(txtMRP.Text.ToString()) * (100 - General.CurrentSetting.MsetOpeningStockGetPercent) / 100, 2);
                        txtTradeRate.Text = mtrate.ToString("#0.00");
                        //}
                        txtTradeRate.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Right)
                    txtCSTPer.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtExpiry.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void txtCSTPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
                txtMasterVATPer.Focus();
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                txtScanCode.Focus();
        }
        private void txtCSTAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                txtTradeRate.Focus();
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                txtCSTPer.Focus();
        }
        private void txtTradeRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                CalculatePurRateSaleRateAndAmount();
                txtPurchaseVATPer.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                CalculatePurRateSaleRateAndAmount();
                txtMRP.Focus();
            }
        }
        private void txtDiscountPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                txtPurchaseVATPer.Focus();
            else if (e.KeyCode == Keys.Up)
                txtTradeRate.Focus();
        }
        private void txtPurchaseVATPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (txtSaleRate.Enabled == true)
                    txtSaleRate.Focus();
                else
                    txtShelfCode.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtTradeRate.Focus();
        }
        private void txtBatch_Validating(object sender, CancelEventArgs e)
        {
            if ((txtBatch.Text.ToString() == null || txtBatch.Text.ToString() == ""))
                txtBatch.Focus();
        }
        private void dgvMPMSVCMain_OnRowDeleted(object sender, EventArgs e)
        {
            CalculateTotals();
            //if(dgvMPMSVCMain.Rows.Count > 0)
            //    dgvMPMSVCMain.SetFocus(dgvMPMSVCMain.MainDataGridCurrentRow.Index, 1);
        }
        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtVouchernumber.Text != "")
                {
                    _OPStock.VoucherNumber = Convert.ToInt32(txtVouchernumber.Text.ToString());
                    _OPStock.ReadDetailsByVoucherNumber();
                    FillSearchData(_OPStock.Id, "");
                }
            }
        }
        #endregion

        #region Calculate Amounts Rates
        private void CalculatePurRateSaleRateAndAmount()
        {
            double mmrp = 0;
            double.TryParse(txtMRP.Text.ToString(), out mmrp);
            if (mmrp > 0)
            {
                double mprate = 0;
                double mtraderate = 0;
                double mpurvatamt = 0;
                double mcstamt = 0;
           //     double mmstamtbySale = 0;
                double mqty = 0;
                double mscmqty = 0;
                double mscmdiscper = 0;
                double mscmamt = 0;
                double mscmamtperunit = 0;
                double mdiscper = 0;
                double mdiscamt = 0;
                double mtraderateafterscm = 0;
                double mpakn = 1;

                double msalerate = 0;
                double mpurvatper = 0;
                double msalevatper = 0;
                double msalevatamt = 0;
                double mamt = 0;
                double mamtzerovat = 0;
                try
                {
                    if (_OpStockMode == OpStockMode.Edit)
                        double.TryParse(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_UnitOfMeasure"].Value.ToString(), out mpakn);
                    else
                        double.TryParse(txtUOM.Text, out mpakn);
                    if (mpakn == 0)
                        mpakn = 1;
                    double.TryParse(txtQuantity.Text.ToString(), out mqty);

                    double.TryParse(txtTradeRate.Text.ToString(), out mtraderate);

                    double.TryParse(txtPurchaseVATPer.Text.ToString(), out mpurvatper);
                    double.TryParse(txtMasterVATPer.Text.ToString(), out msalevatper);
                    if (mscmdiscper > 0)
                    {
                        mscmamt = Math.Round((mtraderate * (mqty / mpakn)) * mscmdiscper / 100, 4);

                    }

                    mscmamtperunit = Math.Round(mscmamt / mqty, 4);
                    mscmamtperunit = 0;
                    mdiscamt = Math.Round(((mtraderate - mscmamtperunit) * mdiscper / 100) * (mqty / mpakn), 4);
                    mdiscamt = 0;
                    mpurvatamt = Math.Round(((mtraderate / mpakn) - mscmamtperunit) * mpurvatper / 100, 4);
                    msalevatamt = Math.Round(((mmrp / mpakn) * msalevatper) / 100, 4);
                    //  double.TryParse(txtPurchaseVATAmt.Text.ToString(), out mpurvatamt);
                    double.TryParse(txtCSTAmount.Text.ToString(), out mcstamt);
                    //  double.TryParse(txtPurchaseVATAmt.Text.ToString(), out mmstamtbypur);
                    // double.TryParse(txtMasterVATAmt.Text.ToString(), out mmstamtbySale);
                    if (mqty != 0)
                    {
                        mtraderateafterscm = Math.Round((mtraderate * mqty) / (mqty + mscmqty), 4);
                        //if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                        //    mprate = mtraderateafterscm + mpurvatamt + mcstamt - mscmamt - mdiscamt;
                        //else
                            mprate = mtraderateafterscm + mcstamt - mscmamt - mdiscamt;
                    }
                    //if (General.CurrentSetting.MsetPurchaseAddVATInSaleRate == "Y")
                    //    msalerate = mmrp + mmstamtbySale + mcstamt;
                    //else
                        msalerate = mmrp;
                    if (mqty != 0)
                    {
                        mamt = Math.Round(mqty * (mtraderate / mpakn), 2);
                        if (mpurvatper == 0)
                            mamtzerovat = mamt;
                        else
                            mamtzerovat = 0;
                    }


                    txtPurchaseVATAmt.Text = mpurvatamt.ToString("#0.0000");
                    txtMasterVATAmt.Text = msalevatamt.ToString("#0.0000");
                    txtAmount.Text = mamt.ToString("#0.00");
                    txtSaleRate.Text = msalerate.ToString("#0.00");
                    if (mprate > 0)
                    {
                        txtPurchaseRate.Enabled = true;
                        txtPurchaseRate.ReadOnly = false;
                        txtPurchaseRate.Text = mprate.ToString("#0.00");
                        txtPurchaseRate.Enabled = false;
                        txtPurchaseRate.ReadOnly = true;
                    }

                    //       CalculateTotals();
                }

                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }

        }
        private void CalculateTotals()
        {
            double mtotamt = 0;
            double mamt = 0;
            int itemCount = 0;
            try
            {
                foreach (DataGridViewRow dr in dgvMPMSVCMain.Rows)
                {
                    if (dr.Cells["Col_MRP"].Value != null && dr.Cells["Col_MRP"].Value.ToString().Trim() != "0.00" && dr.Cells["Col_MRP"].Value.ToString() != "")
                    {
                        itemCount += 1;
                        if (dr.Cells["Col_Amount"].Value != null)
                        {
                            double.TryParse(dr.Cells["Col_Amount"].Value.ToString(), out mamt);
                            mtotamt += mamt;
                        }
                    }
                    txtGridAmountTot.Text = mtotamt.ToString("#0.00");

                    txtNoOfRows.Text = itemCount.ToString().Trim();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void CalculateFinalVAT()
        {
            double mtotdisczero = 0;
            double mtotdisc5 = 0;
            double mtotdisc12point5 = 0;
            double mtotdiscother = 0;
            double mmstamtzero = 0;
            double mmstamt5 = 0;
            double mmstamt12point5 = 0;
            double mmstamtother = 0;
            double mtotmstzero = 0;
            double mtotmst5 = 0;
            double mtotmst12point5 = 0;
            double mtotmstother = 0;

            double mskl = 0;
            double mscmdisc = 0;
            double mitm = 0;
            double msplddx = 0;
            double mcrddx = 0;
            double mddx = 0;
            double mtt1 = 0;
            double mmstperpur = 0;
            //double mmstpersale = 0;            

            //  double mtotamt = 0;
            double mamt = 0;
            double mtotalvat = 0;
            try
            {
                foreach (DataGridViewRow dr in dgvMPMSVCMain.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null)
                    {
                        mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null)
                            mscmdisc = Convert.ToDouble(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());
                        mmstperpur = Convert.ToDouble(dr.Cells["Col_VAT"].Value.ToString());
                        mskl = Math.Round(mamt - mscmdisc, 2);
                        mitm = Math.Round((mskl * Convert.ToDouble(dr.Cells["Col_ItemDiscountPer"].Value.ToString())) / 100, 4);
                        mtt1 = Math.Round((mamt - mddx - msplddx - mcrddx - mscmdisc - mitm) * (mmstperpur / 100), 4);
                        mtotalvat += mtt1;
                        dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();
                        dr.Cells["Col_CreditNoteAmount"].Value = mcrddx.ToString();
                        dr.Cells["Col_ItemDiscountAmount"].Value = mitm.ToString();
                        dr.Cells["Col_CashDiscountAmount"].Value = mddx.ToString();
                        dr.Cells["Col_VATAmountPurchase"].Value = mtt1.ToString();
                        //dr.Cells["Col_SplDiscountPer"].Value = _OPStock.SpecialDiscountPercentS.ToString();
                        dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();
                        if (mmstperpur == 0)
                        {
                            mmstamtzero += (mamt - mddx - msplddx - mcrddx - msplddx - mitm);
                            mtotmstzero += mtt1;
                            mtotdisczero += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }
                        // vat 5.5
                        else if (mmstperpur == 6)
                        {
                            mmstamt5 += (mamt - mddx - msplddx - mcrddx - msplddx - mitm);
                            mtotmst5 += mtt1;
                            mtotdisc5 += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }
                        else if (mmstperpur == 13.5)
                        {
                            mmstamt12point5 += (mamt - mddx - msplddx - mcrddx - msplddx - mitm);
                            mtotmst12point5 += mtt1;
                            mtotdisc12point5 += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }
                        else
                        {
                            mmstamtother += (mamt - mddx - msplddx - mcrddx - msplddx - mitm);
                            mtotmstother += mtt1;
                            mtotdiscother += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }

                    }

                    mtotalvat = mtotmst5 + mtotmst12point5 + mtotmstother;

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region Button Click
        private void btnOKS_Click(object sender, EventArgs e)
        {
            MainToolStrip.Select();
            tsBtnSave.Select();
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
                Log.WriteException(Ex);
            }
        }
        # endregion

        #region tooltip
        private void AddToolTip()
        {
            ttOpeningStock.SetToolTip(txtQuantity, "Loose eg. Tablets,Capsules if UOM > 1");
            ttOpeningStock.SetToolTip(txtBatch, "Batch Can not be Blank Type . or * or #");
            ttOpeningStock.SetToolTip(txtExpiry, "mm/yy Format");
            ttOpeningStock.SetToolTip(btnOK, "Save Product Details");
            ttOpeningStock.SetToolTip(btnCancel, "Do Not Save Product Details");
        }
        #endregion tooltip

        #region UIEvents

        //private void cbAcceptNrExpired_CheckedChanged(object sender, EventArgs e)
        //{
        //    CBAcceptExpiryCheckedChange();
        //}

        //private void CBAcceptExpiryCheckedChange()
        //{
        //    if (cbAcceptNrExpired.Checked == true)
        //    {
        //        txtMRP.Focus();
        //        btnOK.Enabled = true;
        //    }
        //    else
        //    {
        //        btnCancel.Focus();
        //        btnOK.Enabled = false;
        //    }
        //}

        private void cbAcceptNrExpired_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMRP.Focus();
            if (e.KeyCode == Keys.Up)
                txtExpiry.Focus();
        }

        private void txtTradeRateValidating()
        {
            double mtrate = 0;
            double mmrp = 0;
            mmrp = Convert.ToDouble(txtMRP.Text.ToString());
            mtrate = Convert.ToDouble(txtTradeRate.Text.ToString());
            if (mtrate >= mmrp)
            {
                lblMessage.Text = "Trade Rate should be < MRP";
                btnOK.Enabled = false;
                txtTradeRate.Focus();
            }
            else
            {
                btnOK.Enabled = true;
                CalculatePurRateSaleRateAndAmount();
                lblMessage.Text = "";
            }
        }

        private void txtTradeRate_Validating(object sender, CancelEventArgs e)
        {
            txtTradeRateValidating();
        }

        private void txtSaleRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // [ansuman] [14.11.2016]
                txtShelfCode.Focus();
            else if (e.KeyCode == Keys.Up)
                txtPurchaseVATPer.Focus();
        }

        private void mcbShelf_EnterKeyPressed(object sender, EventArgs e)
        {
            dgvMPMSVCMain.SetFocus(1);
        }

        private void mcbShelf_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbShelf.SelectedID;
            FillShelfCombo();
            mcbShelf.SelectedID = selectedId;
        }

        private void txtShelfCode_KeyDown(object sender, KeyEventArgs e)  // [14.11.2016]
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtScanCode.Enabled = true;
                txtScanCode.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtSaleRate.Focus();
        }

        private void txtScanCode_KeyDown(object sender, KeyEventArgs e)  // [14.11.2016]
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (_OpStockMode == OpStockMode.Add)
                {
                    DataRow dr = _OPStock.GetDuplicateBarcode(txtScanCode.Text);
                    if (dr != null && String.IsNullOrEmpty(txtScanCode.Text) == false)
                    {
                        PSMessageBox.Show("Invalid Barcode Entered", "Duplicate Barcode", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                        txtScanCode.Focus();
                        return;
                    }
                    else
                        btnOK.Focus();
                }
                else if (_OpStockMode == OpStockMode.Edit)
                {
                    txtScanCode.Enabled = false;
                }
                else
                    btnOK.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtShelfCode.Focus();
        }

        private void txtBatch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBatch.Text.Trim()) == true && pnlProductDetail.Visible)
            {
                MessageBox.Show("Please Enter Batch.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBatch.Focus();
            }
            //else
            //{

            //    txtExpiry.Focus();
            //    txtExpiry.SelectAll();
            //}
        }

        private void dgvMPMSVCMain_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            Exit();
        }
        private void txtProdName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.ActiveControl = mcbCompany1;
                mcbCompany1.Focus();
            }
        }
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
        private void FillProdCategoryCombo()  // [14.11.2016]
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
            if (_OpStockMode == OpStockMode.Edit)
            {
                pobj.Id = ID;
                pobj.Name = txtProdName.Text;
                pobj.ProdCompID = 0;
                pobj.ProdPack = "";
                pobj.ProdPackType = "";
                pobj.ProdPackTypeID = 0;
                pobj.ProdLoosePack = Convert.ToInt32(txtUOM.Text);
                pobj.ProdShelfID = 0;
                try
                {
                    DataRow drowprod = null;
                    DataRow drowcmpny = null;
                    DataRow drowGenCat = null;
                    DataRow drowProdCat = null;
                    DataRow drowShelf = null;
                    //DataRow cmpnydetails = null;
                    DBProduct dbProd = new DBProduct();
                    DBCompany dbComp = new DBCompany();
                    drowprod = dbProd.ReadDetailsByID(ID);

                    if (drowprod != null)
                    {
                        if (drowprod["ProdCompID"] != DBNull.Value)
                        {
                            pobj.ProdCompID = Convert.ToInt32(drowprod["ProdCompID"]);
                            drowcmpny = dbComp.ReadDetailsByID(pobj.ProdCompID);
                            if (string.IsNullOrEmpty(Convert.ToString(drowcmpny["CompName"])) == false)
                            {
                                //DataTable dt = new DataTable();
                                //cobj.CName = drowcmpny["CompName"].ToString();
                                //dt.Columns.Add(new DataColumn("CompID", typeof(string)));
                                //dt.Columns.Add(new DataColumn("CompName", typeof(string)));
                                //dt.Columns.Add(new DataColumn("CompShortName", typeof(string)));
                                //cmpnydetails = dt.NewRow();
                                //cmpnydetails["CompID"] = drowcmpny["CompID"];
                                //cmpnydetails["CompName"] = drowcmpny["CompName"];
                                //cmpnydetails["CompShortName"] = drowcmpny["CompShortName"];
                                //dt.Rows.Add(cmpnydetails);
                                //mcbCompany1.Refresh();
                                mcbCompany1.SelectedID = drowcmpny["CompID"].ToString();
                                //mcbCompany1.SelectedID = cmpnydetails["CompID"].ToString();
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
                                    pobj.ProdGenericID = Convert.ToInt32(mcbGenCatOpStock.SeletedItem.ItemData[0].ToString());
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
                                    mcbShelfNoOpStock.SelectedID = drowShelf["ShelfID"].ToString();
                                    txtShelfCode.Text = mcbShelfNoOpStock.SeletedItem.ItemData[1].ToString();
                                    pobj.ProdShelfID = Convert.ToInt32(mcbShelfNoOpStock.SeletedItem.ItemData[0].ToString());
                                }
                                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Shelf"].Value.ToString() == "" && _OpStockMode == OpStockMode.Edit) // [ansuman][18.11.2016]
                                {
                                    FillShelfComboList();
                                    if (mcbShelf.SelectedID != "")
                                    {
                                        mcbShelfNoOpStock.SelectedID = mcbShelf.SelectedID;
                                        txtShelfCode.Text = mcbShelfNoOpStock.SeletedItem.ItemData[1].ToString();
                                        //pobj.ProdShelfID = mcbShelfNoOpStock.SeletedItem.ItemData[0].ToString();
                                    }
                                    else
                                    {
                                        mcbShelfNoOpStock.SelectedID = "";
                                        mcbShelfNoOpStock.SelectedIntID = 0;
                                        pobj.ProdShelfID =0;
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
                            //txtPackType1.SelectedID = 
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
        private void ClearStockData()
        {
            //txtProdName.Text = "";
            //  FillCompanyCombo();
            mcbCompany1.SelectedID = "";
            //   FillGenericCategoryCombo();
            mcbGenCatOpStock.SelectedID = "";
            //    FillProdCategoryCombo();        // [14.11.2016]
            mcbProductCategory1.SelectedID = ""; // [14.11.2016]
                                                 //     FillShelfComboList();
            mcbShelfNoOpStock.SelectedID = "";
            //     FillScheduleDrugCombo();
            mcbSchedule1.SelectedItem = "";
            txtCompShortName1.Text = "";
            txtUOM.Text = "";
            // sheela 15-11-2016
            txtProdName.Text = "";
            // sheela 15-11-2016
            txtPackType1.Text = "";
            txtPack1.Text = "";
            txtIsDataOK.Text = "Y";
        }

        private void mcbGenCatOpStock_KeyDown(object sender, KeyEventArgs e)
        {

        }

        public Product GetDetailsForNewProduct()
        {
            Product _prod = new Product();
            _prod.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");

            _prod.Name = txtProdName.Text.Trim();
            _prod.Name = (_prod.Name.Replace("*", "X"));
            _prod.Name = (_prod.Name.Replace("%", "Per"));
            if (mcbCompany1.SelectedID != null)
                _prod.ProdCompID = Convert.ToInt32(mcbCompany1.SelectedID.Trim());
            if (mcbGenCatOpStock.SelectedID != null)
                _prod.ProdGenericID = Convert.ToInt32(mcbGenCatOpStock.SelectedID.Trim());
            if (txtUOM.Text.Trim() != "")
                _prod.ProdLoosePack = Convert.ToInt32(txtUOM.Text.Trim());
            _prod.ProdPack = txtPack1.Text.Trim();
            _prod.ProdPackType = txtPackType1.Text.Trim();
            if (mcbShelfNoOpStock.SelectedID != null)
                _prod.ProdShelfID = Convert.ToInt32(mcbShelfNoOpStock.SelectedID.Trim());

            if (_prod.ProdPack != null && _prod.ProdPack != string.Empty)
            {
                _prod.ProdPackID = Convert.ToInt32(_prod.SearchForProdPack(_prod.ProdPack));
                if (_prod.ProdPackID.ToString() == null || _prod.ProdPackID == 0)
                {
                    _prod.IfNewPack = "Y";
                    //SS
                    //_prod.ProdPackID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                }
            }
            if (_prod.ProdPackType != null && _prod.ProdPackType != string.Empty)
            {
                _prod.ProdPackTypeID = _prod.SearchForProdPackType(_prod.ProdPackType);
                if (_prod.ProdPackTypeID.ToString () == null || _prod.ProdPackTypeID == 0)
                {
                    _prod.IfNewPackType = "Y";
                    //SS
                    //_prod.ProdPackTypeID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                }
            }
            _prod.CreatedBy = General.CurrentUser.Id;
            _prod.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
            _prod.CreatedTime = DateTime.Now.ToString("HH:mm:ss");

            return _prod;
        }

        private void mcbGenCatOpStock_EnterKeyPressed(object sender, EventArgs e)
        {
            this.ActiveControl = mcbProductCategory1;  // [14.11.2016]
            mcbProductCategory1.Focus();
        }

        private void dgvMPMSVCMain_OnCellTextChanged(string cellValue)
        {
            if (cellValue != "")
            {
                if (_OpStockMode == OpStockMode.Add)
                    txtProdName.Text = cellValue;
            }
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

        public void BindMainProductGrid()
        {
            Product prod = new Product();
            DataTable proddt = prod.GetOverviewData();
            //  DataTable dt = General.ProductList;
            dgvMPMSVCMain.DataSource = proddt;

            dgvMPMSVCMain.Bind();
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

        private void txtPackType1_EnterKeyPressed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPackType1.Text) == false)
            {
            //    FillShelfComboList();  // [ansuman] [11.1.2017]
                this.ActiveControl = mcbShelfNoOpStock;
                mcbShelfNoOpStock.Focus();
            }
            else
            {
                this.ActiveControl = txtPackType1;
                txtPackType1.Focus();
            }
        }

        private void mcbShelfNoOpStock_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbShelfNoOpStock.SeletedItem != null) // [14.11.2016]
            {
                if (mcbShelfNoOpStock.SeletedItem.ItemData[0].ToString() != "")
                    txtShelfCode.Text = mcbShelfNoOpStock.SeletedItem.ItemData[1].ToString();
            }
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
            try
            {
                if (e.KeyCode == Keys.Enter && txtIsDataOK.Text == "Y")
                {
                    pobj.Name = txtProdName.Text.Trim();
                    pobj.Name = (pobj.Name.Replace("*", "X"));
                    pobj.Name = (pobj.Name.Replace("%", "Per"));
                    pobj.ProdCompID = Convert.ToInt32( mcbCompany1.SeletedItem.ItemData[0].ToString());
                    pobj.ProdCompShortName = mcbCompany1.SeletedItem.ItemData[2];
                    if (mcbGenCatOpStock.SeletedItem != null)
                        pobj.ProdGenericID = Convert.ToInt32(mcbGenCatOpStock.SeletedItem.ItemData[0].ToString());
                    if (mcbProductCategory1.SeletedItem != null)
                        pobj.ProdProductCategoryID = Convert.ToInt32(mcbProductCategory1.SeletedItem.ItemData[0].ToString()); // [14.11.2016]
                    pobj.ProdLoosePack = Convert.ToInt32(txtUOM.Text);
                    pobj.ProdPackType = txtPackType1.Text;
                    pobj.ProdPack = txtPack1.Text;
                    if (mcbShelfNoOpStock.SeletedItem != null)
                    {
                        if (mcbShelfNoOpStock.SeletedItem.ItemData[0].ToString() != "")
                            pobj.ProdShelfID = Convert.ToInt32 ( mcbShelfNoOpStock.SeletedItem.ItemData[0].ToString());
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
                        //_OpStockMode = OpStockMode.Edit;
                        pnlEditProduct.Enabled = false;
                        txtQuantity.Focus();
                    }

                }
                else if (e.KeyCode == Keys.Up) //kiran
                {
                    mcbSchedule1.Focus();
                }
            }
            catch (Exception Ex)
            { Log.WriteException(Ex); }
        }


        private bool SaveProductDetails()
        {
            bool IsProductCreateOrUpdate = false;
            System.Text.StringBuilder _errorMessage;
            try
            {
                if (_OpStockMode == OpStockMode.Add)
                {
                    pobj.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = pobj.Id;
                    pobj.CreatedBy = General.CurrentUser.Id;
                    pobj.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    pobj.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    pobj.IFEdit = "N";
                    pobj.Validate();
                    //if (pobj.IsValid)
                    //    IsProductCreateOrUpdate = pobj.AddDetails();
                    //else // Show Validation Messages
                    //{
                    //    _errorMessage = new System.Text.StringBuilder();
                    //    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    //    foreach (string _message in pobj.ValidationMessages)
                    //    {
                    //        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    //    }
                    //    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    IsProductCreateOrUpdate = false;
                    //}
                    if (IsProductCreateOrUpdate)
                        FillBatchGrid();
                }
                else
                {
                    pobj.Id = _OPStock.ProductID;
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
                    if (dgvMPMSVCMain.DataSourceMain != null)
                        dgvMPMSVCMain.DataSourceMain = null;
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
        private void mcbProductCategory1_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbProductCategory1.SeletedItem != null && mcbProductCategory1.SeletedItem.ItemData[0].ToString() != "")  // [14.11.2016]
            {
                this.ActiveControl = txtPackType1;
                txtPackType1.Focus();
            }
            else
            {
                this.ActiveControl = mcbProductCategory1;
                mcbProductCategory1.Focus();
            }
        }

        private void txtCSTPer_KeyDown_1(object sender, KeyEventArgs e) // [ansuman] [14.11.2016]
        {

        }

        private void txtMasterVATPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // [ansuman] [14.11.2016]
            {
                btnOK.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtCSTPer.Focus();
        }
        //private void btnOK_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Up)  // [ansuman] [14.11.2016]
        //    {
        //        txtScanCode.Focus();
        //    }
        //}

        //private void btnOK_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Up)  // [ansuman] [14.11.2016]
        //    {
        //       // txtScanCode.Enabled = true;
        //        this.ActiveControl = txtScanCode;
        //        txtScanCode.Select();
        //        txtScanCode.Focus();
        //    }
        //}

        private void mcbGenCatOpStock_UpArrowPressed(object sender, EventArgs e)
        {
            this.ActiveControl = txtPack1;
            txtPack1.Focus();
        }
        private void mcbProductCategory1_UpArrowPressed(object sender, EventArgs e)
        {
            this.ActiveControl = mcbGenCatOpStock;
            mcbGenCatOpStock.Focus();
        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    return true;
        //}
        // [ansuman]

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
        private void mcbCompany1_SeletectIndexChanged(object sender, EventArgs e)
        {

        }
        private void dgvMPMSVCMain_OnTABKeyPressed(object sender, EventArgs e)
        {
            MainToolStrip.Select();
            tsBtnSave.Select();
        }

        #endregion UIEvents
    }
}