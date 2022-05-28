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
    public partial class UclDebitNoteExpiry : BaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private DebitNoteExpiry _DNExpiry;
        private int _FromMonth;
        private int _FromYear;
        private int _ToMonth;
        private int _ToYear;
        # endregion

        #region constructor
        public UclDebitNoteExpiry()
        {
            try
            {
                InitializeComponent();
                _DNExpiry = new DebitNoteExpiry();
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
            txtFromMonth.Focus();
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
                _FromMonth = 0;
                _ToMonth = 0;
                _ToYear = Convert.ToInt32(DateTime.Now.Year.ToString());
                txtFromYear.Text = _ToYear.ToString();
                txtToYear.Text = _ToYear.ToString();
                InitializeMainSubViewControl();
                pnlMonthYear.Visible = true;
                txtFromMonth.Focus();
                tsBtnSave.Enabled = false;
                tsBtnSavenPrint.Enabled = false;
               
                headerLabel1.Text = "DEBIT NOTE EXPIRY -> NEW";
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
                headerLabel1.Text = "DEBIT NOTE EXPIRY -> EDIT";
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
                mpMainSubViewControl.Sort(mpMainSubViewControl.ColumnsMain[1], ListSortDirection.Ascending);


                _DNExpiry.CrdbVouType = txtVouType.Text.Trim();
                _DNExpiry.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                int mmonth = 0;
                int myear = 0;
                int.TryParse(txtToMonth.Text, out mmonth);
                int.TryParse(txtToYear.Text, out myear);
                _DNExpiry.CrdbMonth = mmonth;
                _DNExpiry.CrdbYear = myear;
                _DNExpiry.Validate();            
                if (_DNExpiry.IsValid)
                {
                    LockTable.LockTablesForCreditDebitNoteStock();

                    General.BeginTransaction();

                    string lastaccid = "";
                    double totalamount = 0;
                    double totaldiscount = 0;
                    string currentaccid = "";
                    int jindex = 0;

                    for (int index = 0; index < mpMainSubViewControl.Rows.Count; index++)
                    {
                        lastaccid = "";
                        string accname = "";
                        lastaccid = mpMainSubViewControl.Rows[index].Cells["Col_AccountID"].Value.ToString();
                        if (mpMainSubViewControl.Rows[index].Cells["Col_AccountName"].Value != null)
                            accname = mpMainSubViewControl.Rows[index].Cells["Col_AccountName"].Value.ToString();
                        int mqty = Convert.ToInt32(mpMainSubViewControl.Rows[index].Cells["Col_Quantity"].Value.ToString());
                        if (lastaccid != "" && accname != "" && mqty > 0)
                        {
                            totalamount = 0;
                            totaldiscount = 0;
                            _DNExpiry.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _DNExpiry.CrdbVouNo = _DNExpiry.GetAndUpdateDNNumber(General.ShopDetail.ShopVoucherSeries);
                            for (jindex = index; jindex < mpMainSubViewControl.Rows.Count; jindex++)
                            {
                                currentaccid = mpMainSubViewControl.Rows[jindex].Cells["Col_AccountID"].Value.ToString();
                                if (lastaccid != currentaccid)
                                {
                                    break;
                                }
                                else
                                {
                                    double mamount = 0;
                                    if (mpMainSubViewControl.Rows[jindex].Cells["Col_Amount"].Value != null)
                                        double.TryParse(mpMainSubViewControl.Rows[jindex].Cells["Col_Amount"].Value.ToString(), out mamount);
                                    totalamount += mamount;
                                    _DNExpiry.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _DNExpiry.ProductID = Convert.ToInt32(mpMainSubViewControl.Rows[jindex].Cells["Col_ProductID"].Value.ToString());                                  
                                    int muom = 1;
                                     Int32.TryParse(mpMainSubViewControl.Rows[jindex].Cells["Col_UOM"].Value.ToString(), out  muom);
                                     _DNExpiry.UOM = muom;
                                    _DNExpiry.Batchno = mpMainSubViewControl.Rows[jindex].Cells["Col_BatchNumber"].Value.ToString();
                                    _DNExpiry.Quantity = Convert.ToInt32(mpMainSubViewControl.Rows[jindex].Cells["Col_Quantity"].Value.ToString());
                                    _DNExpiry.PurchaseRate = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_PurRate"].Value.ToString());
                                    _DNExpiry.MRP = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_MRP"].Value.ToString());
                                    if (mpMainSubViewControl.Rows[jindex].Cells["Col_SaleRate"].Value != null)
                                        _DNExpiry.SaleRate = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_SaleRate"].Value.ToString());
                                    _DNExpiry.Expiry = mpMainSubViewControl.Rows[jindex].Cells["Col_Expiry"].Value.ToString();
                                    _DNExpiry.ExpiryDate = mpMainSubViewControl.Rows[jindex].Cells["Col_ExpiryDate"].Value.ToString();
                                    _DNExpiry.VATPer = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_VATPer"].Value.ToString());
                                    _DNExpiry.ReasonCode = FixAccounts.SubTypeForExpiry;
                                    _DNExpiry.TradeRate = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_TradeRate"].Value.ToString());
                                    _DNExpiry.StockID = mpMainSubViewControl.Rows[jindex].Cells["Col_StockID"].Value.ToString();
                                    _DNExpiry.DiscountPercentProduct = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_Discount"].Value.ToString());
                                    _DNExpiry.DiscountAmountProduct = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_DiscountAmount"].Value.ToString());
                                    totaldiscount += _DNExpiry.DiscountAmountProduct;
                                    _DNExpiry.Amount = Convert.ToDouble(mpMainSubViewControl.Rows[jindex].Cells["Col_Amount"].Value.ToString());
                                    retValue =  _DNExpiry.AddProductDetails();
                                    if (retValue)
                                        retValue = ReduceStockIntblStock();
                                    if (retValue == false)
                                        break;

                                }
                            }

                        }
                        if (retValue)
                        {
                            if (totalamount > 0)
                            {
                                _DNExpiry.CrdbDiscAmt = totaldiscount;
                                _DNExpiry.CrdbAmountNet = totalamount - totaldiscount;
                                _DNExpiry.CrdbTotalAmount = totalamount;
                                _DNExpiry.AccountID = lastaccid;
                                retValue = _DNExpiry.AddDetails();
                                if (retValue == false)
                                    break;
                                if (jindex < mpMainSubViewControl.Rows.Count)
                                    index = jindex - 1;
                                else
                                    index = jindex;
                            }
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
                this.Cursor = Cursors.Default;
                lblFooterMessage.Text = "";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            LockTable.UnLockTables();
            ClearControls();
            return retValue;
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
                    txtToMonth.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.Y && modifier == Keys.Alt)
                {
                    txtToYear.Focus();
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
                txtVouType.Text = FixAccounts.VoucherTypeForDebitNoteStock;
                txtFromMonth.Text = "0";
                txtFromYear.Text = "0";
                txtToMonth.Text = "0";
                txtToYear.Text = "0";
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
            string Frommonth = "";
            string Fromdate = "";
            string Tomonth = "";
            string Todate = "";
            int.TryParse(txtFromMonth.Text, out _FromMonth);
            int.TryParse(txtFromYear.Text, out _FromYear);
            int.TryParse(txtToMonth.Text, out _ToMonth);
            int.TryParse(txtToYear.Text, out _ToYear);
            if (_FromMonth > 0 && _FromYear > 2000 && _ToMonth > 0 && _ToYear > 2000)
            {
                try
                {
                    Frommonth = "00" + Convert.ToString(_FromMonth).Trim();
                    int len = 0;
                    len = Frommonth.Length;
                    if (len == 3)
                        Frommonth = Frommonth.Substring(1, 2);
                    else
                        Frommonth = Frommonth.Substring(2, 2);
                    Fromdate = Convert.ToString(_FromYear).Trim() + Frommonth + "01";

                    Tomonth = "00" + Convert.ToString(_ToMonth).Trim();
                    int mlen = 0;
                    mlen = Tomonth.Length;
                    if (mlen == 3)
                        Tomonth = Tomonth.Substring(1, 2);
                    else
                        Tomonth = Tomonth.Substring(2, 2);
                    Todate = Convert.ToString(_ToYear).Trim() + Tomonth + "01";

                    _BindingSource = _DNExpiry.ReadExpiredStockbtnDate(Fromdate, Todate);

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
            int mqty = 0;
            double mmrp = 0;
            double mamt = 0;
            int mpakn = 0;
            double mtotalAmount = 0;
            double actqty = 0;
            double mdiscper = 0;
            double mdiscamt = 0;
            try
            {
                foreach (DataGridViewRow dr in mpMainSubViewControl.Rows)
                {
                    mqty = 0;
                    mmrp = 0;
                    mamt = 0;
                    mpakn = 1;
                    mdiscper = 25.00;
                    if (dr.Cells["Col_Quantity"].Value != null && Convert.ToInt32(dr.Cells["Col_Quantity"].Value.ToString()) > 0)
                    {
                        mqty = Convert.ToInt32(dr.Cells["Col_Quantity"].Value.ToString());
                        if (dr.Cells["Col_MRP"].Value != null && dr.Cells["Col_MRP"].Value.ToString() != "")
                            mmrp = Convert.ToDouble(dr.Cells["Col_MRP"].Value.ToString());
                        if (dr.Cells["Col_UOM"].Value != null && dr.Cells["Col_UOM"].Value.ToString() != "")
                            mpakn = Convert.ToInt32(dr.Cells["Col_UOM"].Value.ToString());
                        if (dr.Cells["Col_Discount"].Value != null && dr.Cells["Col_Discount"].Value.ToString() != "")
                            mdiscper = Convert.ToDouble(dr.Cells["Col_Discount"].Value.ToString());
                        actqty = Math.Round((Convert.ToDouble(mqty) / Convert.ToDouble(mpakn)), 0);
                        if (actqty == 0)
                            mpMainSubViewControl.Rows.Remove(dr);
                        else
                        {
                            mdiscamt = Math.Round(((actqty * mmrp) * (mdiscper / 100)), 2);
                            mamt = Math.Round(((actqty * mmrp) - mdiscamt), 2);
                            dr.Cells["Col_Discount"].Value = mdiscper.ToString("#0.00");
                            dr.Cells["Col_DiscountAmount"].Value = mdiscamt.ToString("#0.00");
                            dr.Cells["Col_Quantity"].Value = Math.Round(actqty, 0).ToString("#0");
                            dr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                            mtotalAmount += mamt;
                        }

                    }
                }
                lblRightSideFooterMsg.Text = string.Format("Total Amount: {0}", mtotalAmount.ToString("#0.00"));
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }



        private bool ReduceStockIntblStock()
        {
            bool returnVal = false;
            int curclstk = 0;

            try
            {

                string ifRecordFound = "";
                ifRecordFound = _DNExpiry.CheckForBatchMRPInStockTable();
                if (ifRecordFound == "Y")
                {
                    returnVal = _DNExpiry.UpdateIntblStock();
                    if (returnVal)
                    {
                        curclstk = _DNExpiry.GetCurrentClosingStockFromMaster(_DNExpiry.ProductID);
                        returnVal = _DNExpiry.UpdateDebitNoteStockInmasterProduct(curclstk);                        
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
        //        foreach (DataGridViewRow prodrow in mpMainSubViewControl.Rows)
        //        {
        //            if (prodrow.Cells["Col_ProductID"].Value != null && prodrow.Cells["Col_ProductID"].Value.ToString() != string.Empty)
        //            {
        //               _DNExpiry.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
        //               EcoMartCache.RefreshProductData(_DNExpiry.ProductID);
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
        //        General.UpdateProductListCacheTest(mpMainSubViewControl.Rows, "Col_ProductID", mpMainSubViewControl.Rows, "Col_ProductID");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        returnVal = false;
        //    }
        //    return returnVal;
        //}
        #endregion

        # region Other Private Methods
        private void ConstructMainColumns()
        {
            mpMainSubViewControl.ColumnsMain.Clear();
            DataGridViewTextBoxColumn column;

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "AccountID";
                column.Visible = false;
                column.Width = 75;
                mpMainSubViewControl.ColumnsMain.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Creditor Name";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = false;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "AccAddress1";
                column.Width = 100;
                column.Visible = false;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);

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
              //  column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);
                //6          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.ToolTipText = "Expiry dd/mm";
                column.Width = 50;
              //  column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 70;
              //  column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMainSubViewControl.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur.Rate";
                column.Width = 80;
              //  column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
             //   column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_Discount";
                //column.HeaderText = "Less%";
                //column.Width = 70;
                ////column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //column.DataPropertyName = "AccLessPercentInDebitNote";
                //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;               
                //column.ReadOnly = true;
                //mpMainSubViewControl.ColumnsMain.Add(column);
                // 10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;              
                column.Width = 90;
             //   column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
            //ProductID,ProdName,ProdPack, ProdCompShortName


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

        #region UIEvents

        private void txtMonth_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {                  
                    txtToYear.Focus();
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
                mpMainSubViewControl.Sort(mpMainSubViewControl.ColumnsMain[1], ListSortDirection.Ascending);
                mpMainSubViewControl.SetFocus(0, 1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtToMonth.Text != "")
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
                txtToMonth.Focus();

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
            if (txtToMonth.Text != null && txtToMonth.Text.ToString() != string.Empty)
                mm = Convert.ToInt32(txtToMonth.Text.ToString());
            if (mm < 1 || mm > 12)
            {
                lblFooterMessage.Text = "Invalid Month";
                txtToMonth.Focus();
                btnOK.Enabled = false;
            }
            else
            {
                btnOK.Enabled = true;
                lblFooterMessage.Text = "";
                txtToYear.Focus();
            }
        }

        private void txtFromMonth_Validating(object sender, CancelEventArgs e)
        {
            int mm = 0;
            if (txtFromMonth.Text != null && txtFromMonth.Text.ToString() != string.Empty)
                mm = Convert.ToInt32(txtFromMonth.Text.ToString());
            if (mm < 1 || mm > 12)
            {
                lblFooterMessage.Text = "Invalid Month";
                txtFromMonth.Focus();
                btnOK.Enabled = false;
            }
            else
            {
                btnOK.Enabled = true;
                lblFooterMessage.Text = "";
                txtFromYear.Focus();
            }
        }

        private void txtFromMonth_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtFromYear.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtFromYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtToMonth.Text != "")
            {
                try
                {
                    txtToMonth.Focus();
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
            else if (e.KeyCode == Keys.Left)
                txtFromMonth.Focus();

            lblFooterMessage.Text = "";
        }
        
        #endregion UIEvents
    }
}
