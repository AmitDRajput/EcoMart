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

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclStockOutExpiredProducts : BaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private StockOut _StockOut;
        private DebitNoteExpiry _DNExpiry;
        private int _Month;
        private int _Year;
        # endregion

        #region constructor
        public UclStockOutExpiredProducts()
        {
            try
            {
                InitializeComponent();
                _DNExpiry = new DebitNoteExpiry();
                _StockOut = new StockOut();
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
            txtMonth.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _DNExpiry.Initialise();
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
                tsBtnExit.Visible = true;
                tsBtnCancel.Visible = false;
                ClearData();
                _Month = 0;
                _Year = Convert.ToInt32(DateTime.Now.Year.ToString());
                txtYear.Text = _Year.ToString();
                InitializeMainSubViewControl();
                pnlMonthYear.Visible = true;
                txtMonth.Focus();
                tsBtnSave.Enabled = false;
                tsBtnSavenPrint.Enabled = false;

                headerLabel1.Text = "Stock Out Expired Products -> NEW";
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
                headerLabel1.Text = "Stock Out Expired Products -> EDIT";
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
            ClearData();
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
            return true;
        }

        public override bool Save()
        {
            bool retValue = true;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                lblFooterMessage.Text = "Please Wait...";
                // mpMainSubViewControl.Sort(mpMainSubViewControl.ColumnsMain[1], ListSortDirection.Ascending);


                _StockOut.CrdbVouType = txtVouType.Text.Trim();
                _StockOut.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                int mmonth = 0;
                int myear = 0;
                int.TryParse(txtMonth.Text, out mmonth);
                int.TryParse(txtYear.Text, out myear);             
                _DNExpiry.CrdbMonth = mmonth;
                _DNExpiry.CrdbYear = myear;
                _DNExpiry.CrdbVouDate = _StockOut.CrdbVouDate;
                _DNExpiry.Validate();
                if (_DNExpiry.IsValid)
                {
                    LockTable.LockTablesForCreditDebitNoteStock();

                    General.BeginTransaction();

                  
                    double totalamount = 0;                              
                    int jindex = 0;
                    _StockOut.CrdbVouNo = _StockOut.GetAndUpdateStockOutNumber(General.ShopDetail.ShopVoucherSeries);
                    _StockOut.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                  

                        //int mqty = Convert.ToInt32(mpMainSubViewControl.Rows[index].Cells["Col_Quantity"].Value.ToString());
                        //if (mqty > 0)
                        //{
                            totalamount = 0;
                           
                           
                           
                            for (jindex = 0; jindex < mpMainSubViewControl.Rows.Count; jindex++)
                            {
                                double mamount = 0;
                                if (mpMainSubViewControl.Rows[jindex].Cells["Col_Amount"].Value != null)
                                    double.TryParse(mpMainSubViewControl.Rows[jindex].Cells["Col_Amount"].Value.ToString(), out mamount);
                                totalamount += mamount;
                                _StockOut.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                _StockOut.ProductID = Convert.ToInt32(mpMainSubViewControl.Rows[jindex].Cells["Col_ProductID"].Value.ToString());
                                int muom = 1;
                                Int32.TryParse(mpMainSubViewControl.Rows[jindex].Cells["Col_UOM"].Value.ToString(), out  muom);
                              //  _StockOut.UOM = muom;
                                _StockOut.Batchno = mpMainSubViewControl.Rows[jindex].Cells["Col_BatchNumber"].Value.ToString();
                                _StockOut.Quantity = Convert.ToInt32(mpMainSubViewControl.Rows[jindex].Cells["Col_Quantity"].Value.ToString());
                                if (_StockOut.Quantity > 0)
                                {
                                    _StockOut.PurchaseRate = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_PurRate"].Value.ToString());
                                    _StockOut.MRP = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_MRP"].Value.ToString());
                                    if (mpMainSubViewControl.Rows[jindex].Cells["Col_SaleRate"].Value != null)
                                        _DNExpiry.SaleRate = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_SaleRate"].Value.ToString());
                                    _StockOut.Expiry = mpMainSubViewControl.Rows[jindex].Cells["Col_Expiry"].Value.ToString();
                                    _StockOut.ExpiryDate = mpMainSubViewControl.Rows[jindex].Cells["Col_ExpiryDate"].Value.ToString();
                                    _StockOut.VATPer = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_VATPer"].Value.ToString());
                                    _StockOut.ReasonCode = FixAccounts.SubTypeForExpiry;
                                    _StockOut.TradeRate = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_TradeRate"].Value.ToString());
                                    _StockOut.StockID = mpMainSubViewControl.Rows[jindex].Cells["Col_StockID"].Value.ToString();
                                    // _DNExpiry.DiscountPercentProduct = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_Discount"].Value.ToString());
                                    //  _DNExpiry.DiscountAmountProduct = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_DiscountAmount"].Value.ToString());
                                    //  totaldiscount += _DNExpiry.DiscountAmountProduct;
                                    _StockOut.Amount = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_Amount"].Value.ToString());
                                    retValue = _StockOut.AddProductDetails();
                                    if (retValue)
                                        retValue = ReduceStockIntblStock();
                                    if (retValue == false)
                                        break;
                                }

                                //}
                       

                    }
                    if (retValue)
                    {

                        
                       _StockOut.CrdbAmountNet = totalamount ;
                       _StockOut.CrdbTotalAmount = totalamount;
                        _StockOut.AccountID = FixAccounts.AccountStockInOut.ToString();
                       retValue = _StockOut.AddDetails();


                    }
                }
                if (retValue)
                    General.CommitTransaction();
                else
                    General.RollbackTransaction();
                LockTable.UnLockTables();
                if (retValue)
                {
                    //  UpdateClosingStockinCache();
                    MessageBox.Show("Data Saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    retValue = true;
                }
                else
                {
                    MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    retValue = false;
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            this.Cursor = Cursors.Default;
            lblFooterMessage.Text = "";
            LockTable.UnLockTables();
            ClearControls();
            return retValue;
        }

        private bool ReduceStockIntblStock()
        {
            bool returnVal = false;
           

            try
            {

                string ifRecordFound = "";
                _DNExpiry.StockID = _StockOut.StockID;
                ifRecordFound = _DNExpiry.CheckForBatchMRPInStockTable();
                if (ifRecordFound == "Y")
                {
                    returnVal = _StockOut.UpdateIntblStock();
                    if (returnVal)
                    {
                        returnVal = _StockOut.UpdateStockOutInMasterProduct();
                       // returnVal = _DNExpiry.UpdateDebitNoteStockInmasterProduct(curclstk);
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

        public override bool FillSearchData(string ID, string Vmode)
        {
            return true;
        }

        #endregion IDetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
        {
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {
                if (keyPressed == Keys.O && modifier == Keys.Alt)
                {
                    txtMonth.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.Y && modifier == Keys.Alt)
                {
                    txtYear.Focus();
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




        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

        #endregion

        #region Internal Methods

        private void ClearControls()
        {
            try
            {
                txtVouType.Text = FixAccounts.VoucherTypeForStockOut;
                txtMonth.Text = "0";
                txtYear.Text = "0";
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        #region Events
        private void InitializeMainSubViewControl()
        {
            try
            {
                ConstructMainColumns();
                ConstructSubColumns();
                mpMainSubViewControl.DoubleColumnNames.Add("Col_MRP");
                mpMainSubViewControl.DoubleColumnNames.Add("Col_Amount");
                BindExpiryData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void BindExpiryData()
        {

            int noofrows = 0;
            string cmonth = "";
            string mdate = "";
            int.TryParse(txtMonth.Text, out _Month);
            int.TryParse(txtYear.Text, out _Year);
            if (_Month > 0 && _Year > 2000)
            {
                try
                {
                    cmonth = "00" + Convert.ToString(_Month).Trim();
                    int mlen = 0;
                    mlen = cmonth.Length;
                    if (mlen == 3)
                        cmonth = cmonth.Substring(1, 2);
                    else
                        cmonth = cmonth.Substring(2, 2);
                    mdate = Convert.ToString(_Year).Trim() + cmonth + "01";
                    _BindingSource = _StockOut.ReadExpiredStockForStockOutExpiredProducts(mdate);
                    mpMainSubViewControl.DataSourceMain = _BindingSource;
                    Account expac = new Account();
                    DataTable dt = expac.GetSSAccountHoldersListforDebitNoteExpiry(FixAccounts.AccCodeForCreditor);
                    mpMainSubViewControl.DataSource = dt;
                    mpMainSubViewControl.Bind();                   
                    noofrows = mpMainSubViewControl.Rows.Count;
                    txtNoOfRows.Text = noofrows.ToString();
                    CalculateAmount();
                    tsBtnSave.Enabled = true;
                    tsBtnSavenPrint.Enabled = false;
                    mpMainSubViewControl.SetFocus(1, 1);
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
            else
            {
                lblFooterMessage.Text = "Please Check Month and year";
                pnlMonthYear.Visible = true;
            }
        }

        private void CalculateAmount()
        {
            
            double mamt = 0;           
            double mtotalAmount = 0;           
            try
            {
                foreach (DataGridViewRow dr in mpMainSubViewControl.Rows)
                {
                    
                    mamt = 0;
                   
                    if (dr.Cells["Col_Amount"].Value != null && Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString()) > 0)
                    {
                       
                       
                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());                         
                            mtotalAmount += mamt;
                       

                    }
                }
                txtReportTotalAmount.Text = mtotalAmount.ToString("#0.00");

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }      
        #endregion

        # region Other Private Methods
        private void ConstructMainColumns()
        {
            mpMainSubViewControl.ColumnsMain.Clear();
            DataGridViewTextBoxColumn column;

            try
            {  
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 100;
                column.Visible = false;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product Name";
                column.Width = 180;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ToolTipText = "Press TAB Key To Exit Product Grid";
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.Visible = false;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);
                //6          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.ToolTipText = "Expiry dd/mm";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 70;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur.Rate";
                column.Width = 80;
                column.ReadOnly = true;
                column.Visible = false;
                mpMainSubViewControl.ColumnsMain.Add(column);

                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "ClosingStock";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.HeaderText = "Qty";
                column.Width = 50;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);
                
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScmQuantity";
                column.HeaderText = "Scm";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = false;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);
                //11

                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Code";
                column.HeaderText = "CD";
                column.Width = 35;
                column.Visible = false;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);
                //13
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFAddVATInTradeRate";
                column.HeaderText = "Y/N";
                column.Width = 35;
                column.ReadOnly = true;
                column.Visible = false;
                column.ToolTipText = "Y=Add Vat In Trade Rate";
                mpMainSubViewControl.ColumnsMain.Add(column);
              
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.DataPropertyName = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 90;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Visible = false;
                mpMainSubViewControl.ColumnsMain.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.Width = 50;
                column.Visible = false;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.HeaderText = "VAT Amount";
                column.Visible = false;
                column.Width = 75;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Visible = false;
                mpMainSubViewControl.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructSubColumns()
        {
           
            mpMainSubViewControl.ColumnsSub.Clear();
            DataGridViewTextBoxColumn column;
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "ID";
                column.Visible = false;
                mpMainSubViewControl.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "AccName";
                column.Width = 200;
                mpMainSubViewControl.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "AccAddress1";
                column.Width = 200;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsSub.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        private void txtMonth_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtYear.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpMainSubViewControl_OnDetailsFilled(DataGridViewRow selectedRow)
        {
            try
            {              
                mpMainSubViewControl.SetFocus(0, 1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMonth.Text != "")
            {
                try
                {
                    datePickerBillDate.Focus();
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
            else if (e.KeyCode == Keys.Left)
                txtMonth.Focus();

            lblFooterMessage.Text = "";

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            btnOKClick();
        }

        private void btnOKClick()
        {
            pnlMonthYear.Visible = false;
            BindExpiryData();

        }

        private void btnOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKClick();
        }

        private void datePickerBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK.Focus();
        }

        private void txtMonth_Validating(object sender, CancelEventArgs e)
        {
            int mm = 0;
            if (txtMonth.Text != null && txtMonth.Text.ToString() != string.Empty)
                mm = Convert.ToInt32(txtMonth.Text.ToString());
            if (mm < 1 || mm > 12)
            {
                lblFooterMessage.Text = "Invalid Month";
                txtMonth.Focus();
                btnOK.Enabled = false;
            }
            else
            {
                btnOK.Enabled = true;
                lblFooterMessage.Text = "";
                txtYear.Focus();
            }
        }
    }
}
