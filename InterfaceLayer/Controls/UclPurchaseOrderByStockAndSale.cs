using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.BusinessLayer;
using EcoMart.Common;
using PrintDataGrid;
using EcoMart.Printing;
using EcoMart.InterfaceLayer.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclPurchaseOrderByStockAndSale : BaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DailyPurchaseOrder _DailyPurchaseOrder;
        private PurchaseOrder _PurchaseOrder;
        private SsStock _SsStock;
        private string _MFromDate;
        private string _MToDate;
        private int _MTotalRows;
        #endregion

        #region Constructor
        public override bool Add()
        {
            bool retValue = true;
            retValue = base.Add();
            pnlMultiSelection.BringToFront();
            pnlMultiSelection.Visible = true;
            pnlMultiSelection.Focus();
            FillCreditorCombo();
            FillCompanyCombo();
            cbZero.Checked = true;
            datePickerBillDate.Value = DateTime.Now;
            ConstructReportColumns();
            fromDate1.Focus();
            return retValue;
        }

        private void GetNoofRows()
        {
            int noofrows = 0;
            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {
                if (dr.Cells["Col_OrderQuantity"].Value != null && dr.Cells["Col_OrderQuantity"].Value.ToString() != "")
                {
                    noofrows += 1;
                }
            }
            _MTotalRows = noofrows;
        }

        public override bool Print()
        {
            bool retValue = true;
            PrintData();
            ClearData();
            return retValue;
        }
        private void PrintData()
        {
            PrintRow row;
            try
            {
                PrintFactory.SendReverseLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.ReverseLineFeed);
                PrintBill.Rows.Clear();
                Font fnt = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = 0;
                PrintPageNumber = 0;
                int rowcount = 0;
                double totpages = 0;
                int totalpages = 0;
                PrintRowPixel = 0;
                DataTable dtable = new DataTable();
                List<DataGridViewRow> rowCollection = new List<DataGridViewRow>();
                foreach (DataGridViewRow prodrow in dgvReportList.Rows)
                {
                    if (prodrow.Cells["Col_ID"].Value != null && (prodrow.Cells["Col_OrderQuantity"].Value) != null &&
                       Convert.ToInt32(prodrow.Cells["Col_OrderQuantity"].Value.ToString()) > 0)
                        rowCollection.Add(prodrow);
                }
                totalrows = 0;
                PrintPageNumber = 0;
                rowcount = 0;
                totpages = 0;
                totalpages = 0;
                PrintRowPixel = 0;
                totalrows = rowCollection.Count();
                totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                totalpages = Convert.ToInt32(totpages);
                _PurchaseOrder.Name = mcbCreditor.SeletedItem.ItemData[2];
                _PurchaseOrder.Address1 = mcbCreditor.SeletedItem.ItemData[3];
                //  _PurchaseOrder.Address2 = mcbCreditor.SeletedItem.ItemData[3];
                PrintHeader(totalpages, rowcount, fnt);
                foreach (DataGridViewRow prodrow in rowCollection)
                {
                    if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                    {
                        PrintRowPixel = 325;
                        row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
                        //PrintBill.Rows.Add(row);
                        PrintBill.Print_Bill(600, 400);
                        PrintBill.Rows.Clear();
                        PrintRowPixel = 0;
                        PrintHeader(totalpages, rowcount, fnt);

                        rowcount = 0;
                    }
                    PrintRowPixel += 17;
                    rowcount += 1;
                    int myqty = Convert.ToInt32(prodrow.Cells["Col_OrderQuantity"].Value.ToString());
                    //.Cells["Col_Quantity"].Value.ToString());
                    row = new PrintRow(myqty.ToString("#0"), PrintRowPixel, 15, fnt);
                    PrintBill.Rows.Add(row);
                    row = new PrintRow(prodrow.Cells["Col_ProductName"].Value.ToString(), PrintRowPixel, 85, fnt);
                    PrintBill.Rows.Add(row);
                    row = new PrintRow(prodrow.Cells["Col_ProductLoosePack"].Value.ToString(), PrintRowPixel, 260, fnt);
                    PrintBill.Rows.Add(row);
                    row = new PrintRow(" X " + prodrow.Cells["Col_ProductPack"].Value.ToString(), PrintRowPixel, 280, fnt);
                    PrintBill.Rows.Add(row);
                    row = new PrintRow(prodrow.Cells["Col_ProdCompShortName"].Value.ToString(), PrintRowPixel, 360, fnt);
                    PrintBill.Rows.Add(row);
                    //double mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                    //row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 700, fnt);
                    //PrintBill.Rows.Add(row);                        

                }

                PrintBill.Print_Bill(600, 400);
                PrintFactory.SendLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.LineFeed);
                rowCollection = new List<DataGridViewRow>();
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
                PrintRowPixel = PrintRowPixel + 37;

                row = new PrintRow(General.ShopDetail.ShopName, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Purchase Order :" + _PurchaseOrder.VoucherNumber.ToString().Trim(), PrintRowPixel, 250, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(General.ShopDetail.ShopAddress1.Trim(), PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow("          Time :" + DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 250, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(_PurchaseOrder.Name, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);
                string voudate = DateTime.Now.ToShortDateString();
                row = new PrintRow("          Date :" + voudate, PrintRowPixel, 250, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                string myadd = _PurchaseOrder.Address1.Trim();
                row = new PrintRow(myadd, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);
                PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow("          Page :" + page, PrintRowPixel, 250, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 14;
                row = new PrintRow("----------------------------------------------------------------------------------------------------", PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 14;
                row = new PrintRow("Quantity                    Product", PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 14;
                row = new PrintRow("----------------------------------------------------------------------------------------------------", PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);
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
            System.Text.StringBuilder _errorMessage;
            if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
            {
                //LockTable.LocktblVoucherNo();
                DBGetVouNumbers getno = new DBGetVouNumbers();
                _PurchaseOrder.VoucherNumber = getno.GetPurchaseOrder(General.ShopDetail.ShopVoucherSeries);
            }
            if (mcbCreditor.SelectedID != null)
                _PurchaseOrder.AccountID = mcbCreditor.SelectedID.Trim();
            GetNoofRows();
            _PurchaseOrder.NoofRows = _MTotalRows;
            //if (txtNoOfRows.Text != null)
            //    _PurchaseOrder.NoofRows = Convert.ToInt32(txtNoOfRows.Text.ToString());
            //if (txtNarration.Text != null)
            //    _PurchaseOrder.Narration = txtNarration.Text.ToString().Trim();
            //if (txtAmount.Text != null)
            //    _PurchaseOrder.Amount = Convert.ToDouble(txtAmount.Text.ToString());
            _PurchaseOrder.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
            _PurchaseOrder.Narration = "";
            _PurchaseOrder.Validate();
            if (_PurchaseOrder.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _PurchaseOrder.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    //_PurchaseOrder.VoucherNumber = getno.GetPurchaseOrder(General.ShopDetail.ShopVoucherSeries);
                    //   txtVouchernumber.Text = _PurchaseOrder.VoucherNumber.ToString();
                    _PurchaseOrder.CreatedBy = General.CurrentUser.Id;
                    _PurchaseOrder.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _PurchaseOrder.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    _PurchaseOrder.AddDetails();
                    retValue = saveproducts();

                    if (retValue == true)
                    {
                        string msgLine2 = _PurchaseOrder.VoucherType + "  " + _PurchaseOrder.VoucherNumber.ToString("#0");
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
                        MessageBox.Show("Unable to save Information ", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //  ClearControls();
                    retValue = true;
                }

            }
            else
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _PurchaseOrder.ValidationMessages)
                {
                    _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                }
                MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //LockTable.UnLockTables();
            return retValue;
        }

        public bool saveproducts()
        {
            bool returnVal = false;
            try
            {
                foreach (DataGridViewRow prodrow in dgvReportList.Rows)
                {
                    _PurchaseOrder.ShortListID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    if (prodrow.Cells["Col_ID"].Value != null && prodrow.Cells["Col_OrderQuantity"].Value != null &&
                       Convert.ToInt32(prodrow.Cells["Col_OrderQuantity"].Value.ToString()) > 0)
                    {
                        _PurchaseOrder.ProductID = prodrow.Cells["Col_ID"].Value.ToString();
                        _PurchaseOrder.Quantity = Convert.ToInt32(prodrow.Cells["Col_OrderQuantity"].Value.ToString());
                        //  if (prodrow.Cells["Col_ProdLastPurchaseRate"].Value != null)
                        //      _PurchaseOrder.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_ProdLastPurchaseRate"].Value.ToString());
                        _PurchaseOrder.PurchaseRate = 0;
                        returnVal = _PurchaseOrder.AddProductDetails();
                    }
                }
            }
            catch { returnVal = false; }
            return returnVal;
        }

        public UclPurchaseOrderByStockAndSale()
        {
            InitializeComponent();
            _DailyPurchaseOrder = new DailyPurchaseOrder();
            _SsStock = new SsStock();
            _PurchaseOrder = new PurchaseOrder();
        }
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            FormatReportGrid();

        }

        private void FormatReportGrid()
        {
            //dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            //dgvReportList.DoubleColumnNames.Add("Col_Debit");
            //dgvReportList.DoubleColumnNames.Add("Col_Credit");
            //dgvReportList.OptionalColumnNames.Add("Col_Debit");
            //dgvReportList.OptionalColumnNames.Add("Col_Credit");
        }
        #endregion

        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            try
            {
                InitializeReportGrid();
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FillReportGrid();
                    this.Cursor = Cursors.Default;
                    dgvReportList.Focus();

                }
                else
                    lblFooterMessage.Text = "Check Date";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.HeaderText = "Product";
                column.Width = 200;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductLoosePack";
                column.HeaderText = "UOM";
                column.Width = 60;
               column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductPack";
                column.HeaderText = "Pack";
                column.Width = 60;
              column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.HeaderText = "COMP";
                column.Width = 60;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OpeningStock";
                column.Visible = false;
                column.Width = 60;
               column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_opstock";
                column.Width = 60;
                column.Visible = false;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Totopstock";
                column.HeaderText = "OP Stock";
                column.Width = 60;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_purstock";
                column.HeaderText = "PUR Stock";
                column.Width = 60;
               column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_cnstistock";
                column.HeaderText = "CN/STI ";
                column.Width = 60;
              column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_totstock";
                column.HeaderText = "TOT IN ";
                column.Width = 60;
               column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_salestock";
                column.HeaderText = "SALE Stock";
                column.Width = 60;
               column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_dnstostock";
                column.HeaderText = "DN/STO ";
                column.Width = 60;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.HeaderText = "CL Stock";
                column.Width = 60;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ActualClosingStock";
                column.HeaderText = "A CL Stock";
                column.Width = 60;
                column.ReadOnly = true;
             //   column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OrderQuantity";
                column.HeaderText = "Order Qty";
                column.Width = 60;
                column.ReadOnly = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillReportGrid()
        {
            try
            {
                FillReportData();
                FillClosingStock();
                pnlMultiSelection.Visible = false;
                CheckFilter();
                NoofRows();
                txtNoOfRows.Text = _MTotalRows.ToString("#0");
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillClosingStock()
        {

            int dropeningstock = 0;
            int dropstk = 0;
            int drpurstk = 0;
            int drsalestk = 0;
            int drcrstk = 0;
            int drdbstk = 0;
            try
            {
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    dropeningstock = 0;
                    dropstk = 0;
                    drpurstk = 0;
                    drsalestk = 0;
                    drcrstk = 0;
                    drdbstk = 0;

                    if (dr.Cells["Col_OpeningStock"].Value != null && dr.Cells["Col_openingStock"].Value.ToString() != "")
                        dropeningstock = Convert.ToInt32(dr.Cells["Col_OpeningStock"].Value.ToString());
                    if (dr.Cells["Col_opstock"].Value != null && dr.Cells["Col_opstock"].Value.ToString() != "")
                        dropstk = Convert.ToInt32(dr.Cells["Col_opstock"].Value.ToString());
                    dr.Cells["Col_Totopstock"].Value = (dropeningstock + dropstk).ToString();
                    if (dr.Cells["Col_purstock"].Value != null && dr.Cells["Col_purstock"].Value.ToString() != "")
                        drpurstk = Convert.ToInt16(dr.Cells["Col_purstock"].Value.ToString());
                    if (dr.Cells["Col_salestock"].Value != null && dr.Cells["Col_salestock"].Value.ToString() != "")
                        drsalestk = Convert.ToInt32(dr.Cells["Col_salestock"].Value.ToString());
                    if (dr.Cells["Col_cnstistock"].Value != null && dr.Cells["Col_cnstistock"].Value.ToString() != "")
                        drcrstk = Convert.ToInt32(dr.Cells["Col_cnstistock"].Value.ToString());
                    if (dr.Cells["Col_dnstostock"].Value != null && dr.Cells["Col_dnstostock"].Value.ToString() != "")
                        drdbstk = Convert.ToInt32(dr.Cells["Col_dnstostock"].Value.ToString());

                    dr.Cells["Col_TotStock"].Value = (dropeningstock + dropstk + drpurstk + drcrstk).ToString();
                    dr.Cells["Col_ActualClosingStock"].Value = (dropeningstock + dropstk + drpurstk + drcrstk - drsalestk - drdbstk).ToString();
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillReportData()
        {
            DataTable dtable = new DataTable();
            string _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
            string _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
            DataRow dtrow = null;
            string curproduct = "";
            int mqty = 0;
            int mscm = 0;
            int mrepl = 0;
            int dropstk = 0;
            int drpurstk = 0;
            int drsalestk = 0;
            int drcrstk = 0;
            int drdbstk = 0;           
            string mvoudate = "";


            try
            {

                dtable = _SsStock.GetOverViewStocknSale(mcbCompany.SelectedID);
                _BindingSource = dtable;
                BindReportGrid();
                // opening stock
                dtable = _SsStock.GetOpeningStockForStocknSale(mcbCompany.SelectedID);
                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    dtrow = dtable.Rows[i];
                    curproduct = dtrow["ProductID"].ToString();
                    mqty = 0;
                    int mpurqty = 0;
                    if (dtrow["OpeningStock"] != DBNull.Value)
                        mqty = Convert.ToInt16(dtrow["OpeningStock"].ToString());
                    if (dtrow["ClosingStock"] != DBNull.Value)
                        mpurqty = Convert.ToInt16(dtrow["ClosingStock"].ToString());
                    foreach (DataGridViewRow dr in dgvReportList.Rows)
                    {
                        dropstk = 0;
                        int drclstk = 0;
                       
                        if (dr.Cells["Col_ID"] != null && dr.Cells["Col_ID"].Value.ToString() == curproduct)
                        {
                            //if (curproduct == "6E93D14BC3D3419B869566E9265B8B16")
                            //    _IfShortCutOpen = true;
                            if (dr.Cells["Col_OpeningStock"].Value != null)
                                dropstk = Convert.ToInt16(dr.Cells["Col_OpeningStock"].Value.ToString());

                            dr.Cells["Col_OpeningStock"].Value = (dropstk + mqty).ToString();

                            if (dr.Cells["Col_ClosingStock"].Value != null)
                                drclstk = Convert.ToInt16(dr.Cells["Col_ClosingStock"].Value.ToString());

                            dr.Cells["Col_ClosingStock"].Value = (drclstk+mpurqty).ToString();

                            break;
                        }
                    }
                }

                // Purchase stock
                dtable = _SsStock.GetPurchaseStockForStocknSale(_MToDate, mcbCompany.SelectedID);

                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    mqty = 0;
                    mscm = 0;
                    mrepl = 0;
                    dtrow = dtable.Rows[i];
                    curproduct = dtrow["ProductID"].ToString();
                    if (dtrow["Quantity"] != DBNull.Value)
                        mqty = Convert.ToInt16(dtrow["Quantity"].ToString());
                    if (dtrow["SchemeQuantity"] != DBNull.Value)
                        mscm = Convert.ToInt16(dtrow["SchemeQuantity"].ToString());
                    if (dtrow["ReplacementQuantity"] != DBNull.Value)
                        mrepl = Convert.ToInt16(dtrow["ReplacementQuantity"].ToString());
                    if (dtrow["VoucherDate"] != DBNull.Value)
                        mvoudate = dtrow["VoucherDate"].ToString();
                    foreach (DataGridViewRow dr in dgvReportList.Rows)
                    {
                        dropstk = 0;
                        drpurstk = 0;
                        if (dr.Cells["Col_ID"] != null && dr.Cells["Col_ID"].Value.ToString() == curproduct)
                        {
                            
                            if (dr.Cells["Col_opstock"].Value != null)
                                dropstk = Convert.ToInt16(dr.Cells["Col_opstock"].Value.ToString());
                            if (dr.Cells["Col_purstock"].Value != null)
                                drpurstk = Convert.ToInt16(dr.Cells["Col_purstock"].Value.ToString());

                            if (Convert.ToInt32(mvoudate) < Convert.ToInt32(_MFromDate))
                            {
                                dr.Cells["Col_opstock"].Value = (dropstk + mqty + mscm + mrepl).ToString();
                            }
                            else
                            {
                                dr.Cells["Col_purstock"].Value = (drpurstk + mqty + mscm + mrepl).ToString();
                            }
                            break;
                        }

                    }
                }


                dtable = _SsStock.GetSaleStockForStocknSale(_MToDate, mcbCompany.SelectedID);

                if (dtable != null)
                {
                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        mqty = 0;
                        mscm = 0;
                        dtrow = dtable.Rows[i];
                        curproduct = dtrow["ProductID"].ToString();
                        if (dtrow["Quantity"] != DBNull.Value)
                            mqty = Convert.ToInt16(dtrow["Quantity"].ToString());
                        if (dtrow["SchemeQuantity"] != DBNull.Value)
                            mscm = Convert.ToInt16(dtrow["SchemeQuantity"].ToString());
                        if (dtrow["VoucherDate"] != DBNull.Value)
                            mvoudate = dtrow["VoucherDate"].ToString();
                        foreach (DataGridViewRow dr in dgvReportList.Rows)
                        {
                            dropstk = 0;
                            drsalestk = 0;

                            if (dr.Cells["Col_ID"] != null && dr.Cells["Col_ID"].Value.ToString() == curproduct)
                            {
                                if (dr.Cells["Col_opstock"].Value != null)
                                    dropstk = Convert.ToInt16(dr.Cells["Col_opstock"].Value.ToString());
                                if (dr.Cells["Col_salestock"].Value != null)
                                    drsalestk = Convert.ToInt16(dr.Cells["Col_salestock"].Value.ToString());

                                if (Convert.ToInt32(mvoudate) < Convert.ToInt32(_MFromDate))
                                {
                                    dr.Cells["Col_opstock"].Value = (dropstk - mqty - mscm).ToString();
                                }
                                else
                                {
                                    dr.Cells["Col_salestock"].Value = (drsalestk + mqty + mscm).ToString();
                                }

                                break;
                            }

                        }
                    }
                }

                dtable = _SsStock.GetCRSTIStockForStocknSale(_MToDate, mcbCompany.SelectedID);

                if (dtable != null)
                {
                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        mqty = 0;
                        mscm = 0;
                        dtrow = dtable.Rows[i];
                        curproduct = dtrow["ProductID"].ToString();
                        if (dtrow["Quantity"] != DBNull.Value)
                            mqty = Convert.ToInt16(dtrow["Quantity"].ToString());
                        if (dtrow["SchemeQuantity"] != DBNull.Value)
                            mscm = Convert.ToInt16(dtrow["SchemeQuantity"].ToString());
                        if (dtrow["VoucherDate"] != DBNull.Value)
                            mvoudate = dtrow["VoucherDate"].ToString();
                        foreach (DataGridViewRow dr in dgvReportList.Rows)
                        {
                            dropstk = 0;
                            drcrstk = 0;
                            if (dr.Cells["Col_ID"] != null && dr.Cells["Col_ID"].Value.ToString() == curproduct)
                            {
                                if (dr.Cells["Col_opstock"].Value != null)
                                    dropstk = Convert.ToInt16(dr.Cells["Col_opstock"].Value.ToString());
                                if (dr.Cells["Col_cnstistock"].Value != null)
                                    drcrstk = Convert.ToInt16(dr.Cells["Col_cnstistock"].Value.ToString());

                                if (Convert.ToInt32(mvoudate) < Convert.ToInt32(_MFromDate))
                                {
                                    dr.Cells["Col_opstock"].Value = (dropstk + mqty + mscm).ToString();
                                }
                                else
                                {
                                    dr.Cells["Col_cnstistock"].Value = (drcrstk + mqty + mscm).ToString();
                                }

                                break;
                            }

                        }
                    }
                }

                dtable = _SsStock.GetDBSTOStockForStocknSale(_MToDate, mcbCompany.SelectedID);

                try
                {
                    if (dtable != null)
                    {
                        string drproductid = "";
                        for (int i = 0; i < dtable.Rows.Count; i++)
                        {

                            mqty = 0;
                            mscm = 0;
                            dtrow = dtable.Rows[i];
                            curproduct = dtrow["ProductID"].ToString();
                            if (dtrow["Quantity"] != DBNull.Value)
                                mqty = Convert.ToInt16(dtrow["Quantity"].ToString());
                            if (dtrow["SchemeQuantity"] != DBNull.Value)
                                mscm = Convert.ToInt16(dtrow["SchemeQuantity"].ToString());
                            if (dtrow["VoucherDate"] != DBNull.Value)
                                mvoudate = dtrow["VoucherDate"].ToString();
                            try
                            {
                                foreach (DataGridViewRow dr in dgvReportList.Rows)
                                {
                                    dropstk = 0;
                                    drdbstk = 0;
                                    if (dr.Cells["Col_ID"].Value != null)
                                        drproductid = dr.Cells["Col_ID"].Value.ToString();
                                    if (drproductid == curproduct)
                                    {
                                        if (dr.Cells["Col_opstock"].Value != null)
                                            dropstk = Convert.ToInt16(dr.Cells["Col_opstock"].Value.ToString());
                                        if (dr.Cells["Col_dnstostock"].Value != null)
                                            drdbstk = Convert.ToInt16(dr.Cells["Col_dnstostock"].Value.ToString());

                                        if (Convert.ToInt32(mvoudate) < Convert.ToInt32(_MFromDate))
                                        {
                                            dr.Cells["Col_opstock"].Value = (dropstk - mqty - mscm).ToString();
                                        }
                                        else
                                        {
                                            dr.Cells["Col_dnstostock"].Value = (drdbstk + mqty + mscm).ToString();
                                        }

                                        break;
                                    }

                                }
                            }
                            catch (Exception Ex)
                            {
                                Log.WriteException(Ex);
                            }

                        }
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillCreditorCombo()
        {
            mcbCreditor.SelectedID = null;
            mcbCreditor.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccDiscountOffered" };
            mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "150", "50" };
            mcbCreditor.DisplayColumnNo = 2;
            mcbCreditor.ValueColumnNo = 0;
            mcbCreditor.UserControlToShow = new UclAccount();
            Account _Party = new Account();
            DataTable dtable = _Party.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
            mcbCreditor.FillData(dtable);
        }
        private void FillCompanyCombo()
        {
            try
            {
                mcbCompany.SelectedID = null;
                mcbCompany.SourceDataString = new string[2] { "CompID", "CompName" };
                mcbCompany.ColumnWidth = new string[2] { "0", "250" };
                mcbCompany.ValueColumnNo = 0;
                mcbCompany.UserControlToShow = new UclCompany();
                Company _Company = new Company();
                DataTable dtable = _Company.GetOverviewData();
                mcbCompany.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void BindReportGrid()
        {
            int _RowIndex;
            DataGridViewRow currentdr;
            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    _RowIndex = dgvReportList.Rows.Add();
                    currentdr = dgvReportList.Rows[_RowIndex];
                    currentdr.Cells["Col_ID"].Value = dr["ProductID"].ToString();
                    currentdr.Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                    currentdr.Cells["Col_ProductLoosePack"].Value = dr["ProdLoosePack"].ToString();
                    currentdr.Cells["Col_ProductPack"].Value = dr["ProdPack"].ToString();
                    currentdr.Cells["Col_ProdCompShortName"].Value = dr["ProdCompShortName"].ToString();
                    //     currentdr.Cells["Col_OpeningStock"].Value = dr["ProdOpeningStock"].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        private void CheckFilter()
        {
            int dropstk = 0;
            int drpurstk = 0;
            int drsalestk = 0;
            int drcrstk = 0;
            int drdbstk = 0;
            int dropeningstock = 0;
            _MTotalRows = 0;
            int i = 0;
            try
            {
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    dropeningstock = 0;
                    dropstk = 0;
                    drpurstk = 0;
                    drsalestk = 0;
                    drcrstk = 0;
                    drdbstk = 0;
                    if (dr.Cells["Col_OpeningStock"].Value != null && dr.Cells["Col_OpeningStock"].Value.ToString() != "")
                        dropeningstock = Convert.ToInt32(dr.Cells["Col_OpeningStock"].Value.ToString());
                    if (dr.Cells["Col_opstock"].Value != null)
                        dropstk = Convert.ToInt32(dr.Cells["Col_opstock"].Value.ToString());
                    if (dr.Cells["Col_purstock"].Value != null)
                        drpurstk = Convert.ToInt16(dr.Cells["Col_purstock"].Value.ToString());
                    if (dr.Cells["Col_salestock"].Value != null)
                        drsalestk = Convert.ToInt32(dr.Cells["Col_salestock"].Value.ToString());
                    if (dr.Cells["Col_cnstistock"].Value != null)
                        drcrstk = Convert.ToInt32(dr.Cells["Col_cnstistock"].Value.ToString());
                    if (dr.Cells["Col_dnstostock"].Value != null)
                        drdbstk = Convert.ToInt32(dr.Cells["Col_dnstostock"].Value.ToString());
                    if (cbZero.Checked == true)
                    {
                        _MTotalRows += 1;
                        dr.Visible = true;
                        i += 1;
                    }
                    else
                    {
                        if (dropeningstock + dropstk + drpurstk + drsalestk + drcrstk + drdbstk == 0)
                        {

                            if (i >= 0)
                                dr.Visible = false;
                            i += 1;
                        }
                        else
                        {
                            _MTotalRows += 1;
                            dr.Visible = true;
                            i += 1;
                        }
                    }

                }
                _BindingSource.DefaultView.RowFilter = "ProdCompID = '" + mcbCompany.SelectedID + "'";

            }
            catch (Exception ex)
            {
                Log.WriteError("UclStockListStocknSale:NoofRows>> " + ex.Message);
            }
        }

        private void NoofRows()
        {
            _MTotalRows = dgvReportList.Rows.Count;
            string strmessage = General.NoofRows(_MTotalRows);
            lblFooterMessage.Text = strmessage;
        }

        # region events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void todate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbCompany.Focus();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
            else if (e.KeyCode == Keys.Left)
                fromDate1.Focus();
        }
        #endregion Events

        private void mcbCompany_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbCreditor.Focus();
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            datePickerBillDate.Focus();
        }

        private void datePickerBillDate_KeyDown(object sender, KeyEventArgs e)
        {
           // btnOKMultiSelectionClick();
        }
    }
}
