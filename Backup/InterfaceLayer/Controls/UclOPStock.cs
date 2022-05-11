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
using PharmaSYSRetailPlus.InterfaceLayer.Classes;

using PharmaSYSPlus.CommonLibrary;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclOPStock : BaseControl
    {

        #region Declaration
        private OPStock _OPStock;
        private string IfEditPreviousRow = "N";
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
                    dgvMPMSVCMain.SetFocus(1);
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
                FillShelfCombo();
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
                retValue = true;
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclOPStock.Add>> " + Ex.Message);
                retValue = false;
            }
            return retValue;
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
                retValue = true;
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclOPStock.Cancel>> " + Ex.Message);
                retValue = false;
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

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            UpdateClosingStockinCache();
                            string msgLine2 = _OPStock.VoucherType + "  " + _OPStock.VoucherNumber.ToString("#0");
                            PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                            if (result == PSDialogResult.Print)
                                Print();
                            retValue = true;
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
        public override void ReFillData()
        {
            try
            {
                FillShelfCombo();
                dgvMPMSVCMain.DataSource = General.ProductList;
                dgvMPMSVCMain.Bind();
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
                if (keyPressed == Keys.E && modifier == Keys.Alt)
                {
                    txtExpiry.Focus();
                    retValue = true;
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
                    if (pnlBatchGrid.Visible == true)
                    {
                        pnlBatchGrid.Visible = false;
                        pnlProductDetail.Enabled = true;
                        txtBatch.Focus();
                        retValue = true;
                    }
                    else if (pnlProductDetail.Visible && dgvBatchGrid.Visible == false)
                    {
                        btnCancelClick();
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
                        double loosepack = 0;

                        if (prodrow.Cells["Col_ProductName"].Value != null && prodrow.Cells["Col_ProductName"].Value.ToString() != string.Empty && 
                           Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                        {
                            _OPStock.SerialNumber += 1;
                            _OPStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _OPStock.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                            loosepack = 1;
                            if (prodrow.Cells["Col_UnitOfMeasure"].Value != null && prodrow.Cells["Col_UnitOfMeasure"].Value.ToString() != string.Empty)
                                loosepack = Convert.ToDouble(prodrow.Cells["Col_UnitOfMeasure"].Value.ToString());
                            _OPStock.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            if (prodrow.Cells["Col_MRP"].Value != null)
                                _OPStock.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString());
                            if (prodrow.Cells["Col_Expiry"].Value != null)
                                _OPStock.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                            if (prodrow.Cells["Col_ExpiryDate"].Value != null)
                                _OPStock.ExpiryDate = prodrow.Cells["Col_ExpiryDate"].Value.ToString();
                            _OPStock.ExpiryDate = General.GetExpiryInyyyymmddForm(_OPStock.ExpiryDate);
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
                            IfRecordFound = _OPStock.CheckForBatchMRPInStockTable();
                            if (IfRecordFound == true)
                                returnVal = _OPStock.UpdateOPStockIntblStock();
                            else
                            {
                                _OPStock.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                returnVal = _OPStock.AddProductDetailsInStockTable();
                            }
                            if (returnVal)
                                returnVal = _OPStock.AddProductDetailsSS();  
                            if (returnVal)
                                returnVal = _OPStock.UpdateOpeningStockInMasterProduct();
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
        //                PharmaSysRetailPlusCache.RefreshProductData(_OPStock.ProductID);
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
                General.UpdateProductListCacheTest(dgvMPMSVCMain.Rows, "Col_ProductID", dgvMPMSVCMain.Rows, "Col_ProductID");
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }
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
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //1
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProductName";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "ProductName";
            column.Width = 200;
            //if (_Mode == OperationMode.Add)
            //    column.ReadOnly = false;
            //else
            //    column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //2
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UnitOfMeasure";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 50;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //3
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Pack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 60;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //4
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Company";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Co.";
            column.Width = 60;
            column.ReadOnly = true;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //5
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdVATPer";
            column.DataPropertyName = "ProdVATPercent";
            column.HeaderText = "VAT %";
            column.Width = 80;
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
            column.Width = 90;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //7
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_BatchNumber";
            column.DataPropertyName = "BatchNumber";
            column.HeaderText = "Batch";
            column.Width = 120;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //8
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Expiry";
            column.DataPropertyName = "Expiry";
            column.HeaderText = "Exp";
            column.Width = 70;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //9
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_TradeRate";
            column.DataPropertyName = "TradeRate";//SaleRate?
            column.HeaderText = "Trade.Rate";
            column.Width = 80;
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
            column.ReadOnly = true;
            column.DefaultCellStyle.Format = "N2";
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMPMSVCMain.ColumnsMain.Add(column);
            //11
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_VAT";
            column.DataPropertyName = "PurchaseVATPercent";
            column.HeaderText = "VAT%";
            column.Width = 60;
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
            column.Width = 120;
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

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ShelfCode";
            column.DataPropertyName = "ShelfCode";
            column.Width = 80;
            column.Visible = false;
            dgvMPMSVCMain.ColumnsMain.Add(column);

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
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdLoosePack";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 50;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdPack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 65;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Comp";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 50;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdVATPer";
            column.DataPropertyName = "ProdVATPercent";
            column.HeaderText = "VAT %";
            column.Width = 50;
            column.ReadOnly = true;
            dgvMPMSVCMain.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingStock";
            column.DataPropertyName = "ProdClosingStock";
            column.HeaderText = "CL.Stock";
            column.Width = 70;
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

                DataTable dt = General.ProductList;
                dgvMPMSVCMain.DataSource = dt;

                dgvMPMSVCMain.Bind();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        #endregion Construct Grid

        # region fill or clean
        private void ClearControls()
        {
            try
            {
                txtVouchernumber.Clear();
                txtVouType.Text = _OPStock.VoucherType;
                datePickerBillDate.Enabled = true;
                datePickerBillDate.ResetText();
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
                lblExpired.Text = "";
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
        private DataTable FillProductData()
        {
            DataTable dtable = new DataTable();
            try
            {
                dtable = General.ProductList;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dtable;
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
                if (mexpiry != "00/00")
                    mexpiry = General.GetValidExpiry(mexpiry);
                if (mexpiry != "00/00")
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
                mcbShelf.SelectedID = mshelfID;
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
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ShelfCode"].Value != null)
                    mshelfcode = dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ShelfCode"].Value.ToString().Trim();
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
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value != null)
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
                mcbShelf.SelectedID = mshelfID;
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
                dt = invss.GetStockByProductIDForPurchase(dgvMPMSVCMain.MainDataGridCurrentRow.Cells[0].Value.ToString());
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

            txtExpiryDate.Text = "";
            txtCSTAmount.Text = "0.00";
            txtCSTPer.Text = "0.00";
            txtPurchaseRate.Text = "0.00";
            txtSaleRate.Text = "0.00";
            txtAmount.Text = "0.00";
            txtScanCode.Text = "";
            mcbShelf.SelectedID = "";

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

            int mqty = 0;
            try
            {
                if (txtQuantity.Text != null)
                    int.TryParse(txtQuantity.Text.ToString(), out mqty);
                int mrowcount = 0;
                mrowcount = dgvBatchGrid.RowCount;
                if (e.KeyCode == Keys.Enter)
                {
                    if (mqty > 0)
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
                        }

                        // CalculatePurRateSaleRateAndAmount();
                    }
                    else if (e.KeyCode == Keys.Escape)
                        btnCancelClick();
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
                    btnOK.Focus();
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
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancelClick();
        }
        private void btnCancel_KeyDown(object sender, KeyEventArgs e)
        {
            btnCancelClick();
        }
        private void btnCancelClick()
        {
            double mamt = 0;
            dgvMPMSVCMain.Enabled = true;
            lblExpired.Text = "";
            try
            {
                if (dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null)
                    double.TryParse(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString(), out mamt);
                pnlProductDetail.SendToBack();
                pnlProductDetail.Visible = false;
                ClearpnlProductDetail();
                if (mamt == 0)
                {
                    dgvMPMSVCMain.Rows.Remove(dgvMPMSVCMain.MainDataGridCurrentRow);
                    dgvMPMSVCMain.Rows.Add();
                }
                dgvMPMSVCMain.SetFocus(1);
                CalculateTotals();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            ButtonOKClick();
        }
        private void ButtonOKClick()
        {
            bool retValue = DoValidation();
            if (retValue)
            {
                lblExpired.Text = "";
                lblMessage.Text = "";
                pnlProductDetail.SendToBack();
                pnlProductDetail.Visible = false;
                pnlProductDetail.Enabled = true;
                dgvBatchGrid.Enabled = true;
                try
                {
                    if (_Mode == OperationMode.Add)
                    {
                        CalculatePurRateSaleRateAndAmount();
                        int mqty = 0;
                        int.TryParse(txtQuantity.Text.ToString(), out mqty);
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = txtQuantity.Text.ToString();
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
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value = txtMasterVATPer.Text.ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_VATAmountSale"].Value = txtMasterVATAmt.Text.ToString();
                        if (mcbShelf.SelectedID != null && mcbShelf.SelectedID != string.Empty)
                            dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value = mcbShelf.SeletedItem.ItemData[0].ToString();
                        dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_StockID"].Value = txtStockID.Text.ToString();
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
                        CalculateTotals();
                    }
                    else
                    {
                        dgvMPMSVCMain.ColumnsMain["col_ProductName"].ReadOnly = false;
                        dgvMPMSVCMain.Enabled = true;
                        dgvMPMSVCMain.SetFocus(dgvMPMSVCMain.MainDataGridCurrentRow.Index, 1);
                        dgvMPMSVCMain.ColumnsMain["Col_ProductName"].ReadOnly = true;
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
        }
        private bool DoValidation()
        {
            bool retValue = true;
            string err = string.Empty;
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
        private void btnOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ButtonOKClick();

            }
        }
        private void txtBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                txtExpiry.Focus();
            else if (e.KeyCode == Keys.Up)
                txtQuantity.Focus();
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
                                    lblExpired.Text = "Expired";
                                    lblExpired.BackColor = Color.IndianRed;
                                    if (General.CurrentSetting.MsetPurchaseAcceptExpriedItems != "Y")
                                    {
                                        cbAcceptNrExpired.Checked = false;
                                        cbAcceptNrExpired.Enabled = false;
                                        CBAcceptExpiryCheckedChange();
                                    }

                                }
                                else if (days < 30)
                                {
                                    lblExpired.Text = "Expiry 1 Mn";
                                    lblExpired.BackColor = Color.Orange;
                                }
                                else if (days < 60)
                                {
                                    lblExpired.Text = "Expiry 2 Mns";
                                    lblExpired.BackColor = Color.Yellow;
                                }
                                else if (days < 90)
                                {
                                    lblExpired.Text = "Expiry 3 Mns";
                                    lblExpired.BackColor = Color.LightGreen;
                                }
                                else
                                {
                                    lblExpired.Text = "";
                                    lblExpired.BackColor = Color.White;
                                }

                                if ((General.CurrentSetting.MsetPurchaseAcceptExpriedItems == "Y" && days < 0) || days > 0)
                                {
                                    cbAcceptNrExpired.Enabled = true;
                                    cbAcceptNrExpired.Checked = true;
                                    cbAcceptNrExpired.Focus();
                                }

                            }
                            else
                            {
                                lblExpired.Text = "";
                                cbAcceptNrExpired.Checked = true;
                                cbAcceptNrExpired.Enabled = false;
                                txtMRP.Focus();
                            }
                        }


                    }

                }
                else
                {
                    if (General.CurrentSetting.MsetGeneralExpiryDateReuired != "Y")
                    {
                        cbAcceptNrExpired.Checked = true;
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
                        if (mtrate == 0)
                        {
                            mtrate = Math.Round(Convert.ToDouble(txtMRP.Text.ToString()) * 83 / 100, 2);
                            txtTradeRate.Text = mtrate.ToString("#0.00");
                        }
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
                txtCSTAmount.Focus();
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                txtMRP.Focus();
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
                    mcbShelf.Focus();
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
                double mmstamtbySale = 0;
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
                    double.TryParse(dgvMPMSVCMain.MainDataGridCurrentRow.Cells["Col_UnitOfMeasure"].Value.ToString(), out mpakn);
                    if (mpakn == 0)
                        mpakn = 1;
                    double.TryParse(txtQuantity.Text.ToString(), out mqty);

                    double.TryParse(txtTradeRate.Text.ToString(), out mtraderate);

                    double.TryParse(txtPurchaseVATPer.Text.ToString(), out mpurvatper);
                    double.TryParse(txtMasterVATPer.Text.ToString(), out msalevatper);
                    if (mscmdiscper > 0)
                    {
                        mscmamt = Math.Round((mtraderate * (mqty/mpakn)) * mscmdiscper / 100, 4);

                    }

                    mscmamtperunit = Math.Round(mscmamt / mqty, 4);
                    mscmamtperunit = 0;
                    mdiscamt = Math.Round(((mtraderate - mscmamtperunit) * mdiscper / 100) * (mqty/mpakn), 4);
                    mdiscamt = 0;
                    mpurvatamt = Math.Round(((mtraderate/mpakn) - mscmamtperunit) * mpurvatper / 100, 4);
                    msalevatamt = Math.Round(((mmrp/mpakn) * msalevatper) / 100, 4);
                    //  double.TryParse(txtPurchaseVATAmt.Text.ToString(), out mpurvatamt);
                    double.TryParse(txtCSTAmount.Text.ToString(), out mcstamt);
                    //  double.TryParse(txtPurchaseVATAmt.Text.ToString(), out mmstamtbypur);
                    // double.TryParse(txtMasterVATAmt.Text.ToString(), out mmstamtbySale);
                    if (mqty > 0)
                    {
                        mtraderateafterscm = Math.Round((mtraderate * mqty) / (mqty + mscmqty), 4);
                        if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                            mprate = mtraderateafterscm + mpurvatamt + mcstamt - mscmamt - mdiscamt;
                        else
                            mprate = mtraderateafterscm + mcstamt - mscmamt - mdiscamt;
                    }
                    if (General.CurrentSetting.MsetPurchaseAddVATInSaleRate == "Y")
                        msalerate = mmrp + mmstamtbySale + mcstamt;
                    else
                        msalerate = mmrp;
                    if (mqty > 0)
                    {
                        mamt = Math.Round(mqty * (mtraderate/mpakn), 2);
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
                        else if (mmstperpur == 5)
                        {
                            mmstamt5 += (mamt - mddx - msplddx - mcrddx - msplddx - mitm);
                            mtotmst5 += mtt1;
                            mtotdisc5 += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }
                        else if (mmstperpur == 12.5)
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
            tsBtnSave.Select();
        }
        private void txtPurchaseVATPer_Validating(object sender, CancelEventArgs e)
        {
            double purvat = 0;
            try
            {
                purvat = Convert.ToDouble(txtPurchaseVATPer.Text.ToString());
                if (purvat != 0 && purvat != 5 && purvat != 12.5)
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

        private void cbAcceptNrExpired_CheckedChanged(object sender, EventArgs e)
        {
            CBAcceptExpiryCheckedChange();
        }

        private void CBAcceptExpiryCheckedChange()
        {
            if (cbAcceptNrExpired.Checked == true)
            {
                txtMRP.Focus();
                btnOK.Enabled = true;
            }
            else
            {
                btnCancel.Focus();
                btnOK.Enabled = false;
            }
        }

        private void cbAcceptNrExpired_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMRP.Focus();
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
            if (e.KeyCode == Keys.Enter)
                btnOK.Focus();
            else if (e.KeyCode == Keys.Up)
                txtPurchaseVATPer.Focus();

        }

        private void mcbShelf_EnterKeyPressed(object sender, EventArgs e)
        {
            btnOK.Focus();
        }

        private void mcbShelf_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbShelf.SelectedID;
            FillShelfCombo();
            mcbShelf.SelectedID = selectedId;       
        }
    }
}
