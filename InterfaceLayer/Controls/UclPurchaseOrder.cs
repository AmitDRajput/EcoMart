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
using EcoMart.Printing;


namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclPurchaseOrder : BaseControl
    {
        #region Declaration     
        private PurchaseOrder _PurchaseOrder;
        private DailyPurchaseOrder _DailyPurchaseOrder;
        #endregion

        #region Constructor
        public UclPurchaseOrder()
        {
            InitializeComponent();
            _PurchaseOrder = new PurchaseOrder();
            _DailyPurchaseOrder = new DailyPurchaseOrder();
            SearchControl = new UclPurchaseOrderSearch();
        }
        #endregion Constructor

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
            _PurchaseOrder.Initialise();
            ClearControls();
            return true;
        }

        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            headerLabel1.Text = "PURCHASE ORDER -> NEW";
            //   InitializeMainSubViewControl();
            mcbCreditor.Enabled = true;
            txtVouchernumber.Enabled = false;
            FillCreditorCombo();
            FillmcbTransferProductCombo();
            //  SetFocus();

            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            headerLabel1.Text = "PURCHASE ORDER -> EDIT";
            InitializeMainSubViewControl();
            FillCreditorCombo();
            FillmcbTransferProductCombo();
            mcbCreditor.Enabled = false;
            txtVouchernumber.Enabled = true;
            txtVouchernumber.ReadOnly = false;
            rbtOnlyShortlist.Visible = false;
            //mpMainSubViewControl.SetFocus(1);
            txtVouchernumber.Focus();
            return retValue;

        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();

            return retValue;
        }


        public override bool Delete()
        {
            bool retValue = base.Delete();
            headerLabel1.Text = "PURCHASE ORDER -> DELETE";
            ClearData();
            InitializeMainSubViewControl();
            FillCreditorCombo();
            mcbCreditor.Enabled = false;
            txtVouchernumber.Enabled = true;
            txtVouchernumber.ReadOnly = false;
            SetFocus();

            return true;
        }


        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_PurchaseOrder.Id != null && _PurchaseOrder.Id != "")
            {

                retValue = _PurchaseOrder.DeleteDetails();
                MessageBox.Show("Scheme information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            ClearData();
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            headerLabel1.Text = "PURCHASE ORDER -> VIEW";
            ClearData();
            InitializeMainSubViewControl();
            FillCreditorCombo();
            mcbCreditor.Enabled = false;
            txtVouchernumber.Enabled = true;
            txtVouchernumber.ReadOnly = false;
            SetFocus();
            return retValue;
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
                foreach (DataGridViewRow prodrow in mpMainSubViewControl.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null)
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
                List<int> PrintRowPixelData = PrintHeader(totalpages, rowcount, fnt);
                bool NextRowFlag = false;
                //rowcount = 9;
                foreach (DataGridViewRow prodrow in rowCollection)
                {
                    if (rowcount > General.CurrentSetting.MsetNumberOfLinesPerPage)
                    {
                        PrintRowPixel = 325;
                        row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
                        PrintBill.Rows.Add(row);
                        PrintBill.Print_Bill(600, 400);
                        PrintBill.Rows.Clear();
                        PrintRowPixel = 0;

                        PrintRowPixelData = PrintHeader(totalpages, rowcount, fnt);

                        rowcount = 0;
                    }
                    else if (rowcount >= General.CurrentSetting.MsetNumberOfLinesSaleBill)
                    {
                        if (!NextRowFlag)
                        {
                            PrintRowPixel = PrintRowPixelData[1];
                            NextRowFlag = true;
                        }
                        PrintRowPixel += 17;
                        rowcount += 1;
                        int myqty = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        //.Cells["Col_Quantity"].Value.ToString());
                        row = new PrintRow(myqty.ToString("#0") + "X", PrintRowPixel, 280, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(prodrow.Cells["Col_UOM"].Value.ToString(), PrintRowPixel, 300, fnt);
                        PrintBill.Rows.Add(row);

                        row = new PrintRow(prodrow.Cells["Col_Pack"].Value.ToString(), PrintRowPixel, 315, fnt);
                        PrintBill.Rows.Add(row);

                        row = new PrintRow(prodrow.Cells["Col_ProductName"].Value.ToString(), PrintRowPixel, 370, fnt);
                        PrintBill.Rows.Add(row);
                    }
                    else
                    {
                        PrintRowPixel += 17;
                        rowcount += 1;
                        int myqty = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        //.Cells["Col_Quantity"].Value.ToString());
                        row = new PrintRow(myqty.ToString("#0") + "X", PrintRowPixel, 10, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(prodrow.Cells["Col_UOM"].Value.ToString(), PrintRowPixel, 30, fnt);
                        PrintBill.Rows.Add(row);

                        row = new PrintRow(prodrow.Cells["Col_Pack"].Value.ToString(), PrintRowPixel, 45, fnt);
                        PrintBill.Rows.Add(row);

                        row = new PrintRow(prodrow.Cells["Col_ProductName"].Value.ToString(), PrintRowPixel, 100, fnt);
                        PrintBill.Rows.Add(row);

                        //int myscmqty = 0;
                        //if (prodrow.Cells["Col_SchemeQuantity"].Value != null && prodrow.Cells["Col_SchemeQuantity"].Value.ToString() != string.Empty)
                        // myscmqty = Convert.ToInt32(prodrow.Cells["Col_SchemeQuantity"].Value.ToString());
                        ////.Cells["Col_Quantity"].Value.ToString());
                        //row = new PrintRow(myscmqty.ToString("#0"), PrintRowPixel, 55, fnt);

                        //double mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        //row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 700, fnt);
                        //PrintBill.Rows.Add(row);                        

                    }
                }
                PrintFooter(fnt);
                PrintBill.Print_Bill(600, 400);
                PrintFactory.SendLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.LineFeed);
                rowCollection = new List<DataGridViewRow>();
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private List<int> PrintHeader(int TotalPages, int Rowcount, Font fnt)
        {
            PrintRow row;
            List<int> Headerlist = new List<int>(2);
            try
            {
                PrintRowPixel = PrintRowPixel + 37;

                Font mfnt = new Font("Arial", 8, FontStyle.Bold);
                row = new PrintRow(General.ShopDetail.ShopName.ToUpper(), PrintRowPixel, 10, mfnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Purchase Order :" + _PurchaseOrder.VoucherNumber.ToString().Trim(), PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(General.ShopDetail.ShopAddress1.Trim(), PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Time :" + DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(General.ShopDetail.ShopAddress2.Trim(), PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                //string voudate = DateTime.Now.ToShortDateString();
                string voudate = General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd"));
                row = new PrintRow("Date :" + voudate, PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(_PurchaseOrder.Name, PrintRowPixel, 10, mfnt);
                PrintBill.Rows.Add(row);

                PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow("          Page :" + page, PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                string myadd = (_PurchaseOrder.Address1.Trim().PadRight(30).Substring(0,30));
                row = new PrintRow(myadd + ",", PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                string myadd2 = (_PurchaseOrder.Address2.Trim().PadRight(30).Substring(0, 30));
                row = new PrintRow(myadd2, PrintRowPixel, 160, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 14;
                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 14;
                row = new PrintRow("QTY                   PRODUCT/PACK                                QTY                   PRODUCT/PACK", PrintRowPixel, 15, mfnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 14;
                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            Rowcount = 1;
            Headerlist.Add(Rowcount);
            Headerlist.Add(PrintRowPixel);
            return Headerlist;
        }

        private int PrintFooter(Font fnt)
        {
            try
            {
                PrintRow row;
                PrintRowPixel = 325;
                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 21;

                row = new PrintRow("Drug Lic.No.: " + General.ShopDetail.ShopDLN, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("For: " + General.ShopDetail.ShopName, PrintRowPixel, 370, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 14;
                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

            return PrintRowPixel;
        }

        public override bool Save()
        {
            return SaveData(false);
        }
        public override bool SaveAndPrint()
        {
            return SaveData(true);
        }
        public bool SaveData(bool printData)
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            DBGetVouNumbers getno = new DBGetVouNumbers();
            if (mcbCreditor.SelectedID != null)
                _PurchaseOrder.AccountID = mcbCreditor.SelectedID.Trim();
            if (txtNoOfRows.Text != null)
                _PurchaseOrder.NoofRows = Convert.ToInt32(txtNoOfRows.Text.ToString());
            if (txtNarration.Text != null)
                _PurchaseOrder.Narration = txtNarration.Text.ToString().Trim();
            if (txtAmount.Text != null)
                _PurchaseOrder.Amount = Convert.ToDouble(txtAmount.Text.ToString());
            _PurchaseOrder.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
            if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
            {
                LockTable.LocktblVoucherNo();
                _PurchaseOrder.VoucherNumber = getno.GetPurchaseOrder(General.ShopDetail.ShopVoucherSeries);
            }
            _PurchaseOrder.Validate();
            if (_PurchaseOrder.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    LockTable.LockTablesForPurchaseOrder();
                    _PurchaseOrder.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    //_PurchaseOrder.VoucherNumber = getno.GetPurchaseOrder(General.ShopDetail.ShopVoucherSeries);
                    txtVouchernumber.Text = _PurchaseOrder.VoucherNumber.ToString();
                    _PurchaseOrder.CreatedBy = General.CurrentUser.Id;
                    _PurchaseOrder.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _PurchaseOrder.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _PurchaseOrder.AddDetails();
                    if (retValue)
                        retValue = saveproducts();

                    if (retValue == true)
                    {
                        if (printData)
                        {
                            MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SavedID = _PurchaseOrder.Id;
                            Print();
                        }
                        else
                        {
                            MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SavedID = _PurchaseOrder.Id;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to save Information ", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    ClearControls();
                    //retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _PurchaseOrder.ModifiedBy = General.CurrentUser.Id;
                    _PurchaseOrder.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _PurchaseOrder.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    _PurchaseOrder.UpdateDetails();
                    _PurchaseOrder.DeletePreviousProducts();
                    retValue = saveproductsModified();
                    if (retValue ==true)
                    {
                        MessageBox.Show("Information has been Updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                   
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
            LockTable.UnLockTables();
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            if (ID != null && ID != "")
            {
                _PurchaseOrder.Id = ID;
                _PurchaseOrder.ReadDetailsByID();
                mcbCreditor.SelectedID = _PurchaseOrder.AccountID;

                InitializeMainSubViewControl();
                CalculateAmount();
                mcbCreditor.Focus();
                txtNarration.Text = _PurchaseOrder.Narration.ToString();
                txtVouType.Text = _PurchaseOrder.VoucherType;
                txtVouchernumber.Text = _PurchaseOrder.VoucherNumber.ToString().Trim();
                txtBillAmount.Text = _PurchaseOrder.Amount.ToString("#0.00");
                txtAmount.Text = _PurchaseOrder.Amount.ToString("#0.00");
                DateTime mydate = new DateTime(Convert.ToInt32(_PurchaseOrder.VoucherDate.Substring(0, 4)), Convert.ToInt32(_PurchaseOrder.VoucherDate.Substring(4, 2)), Convert.ToInt32(_PurchaseOrder.VoucherDate.Substring(6, 2)));
                datePickerBillDate.Value = mydate;
            }
            SetFocus();
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
            try
            {
                if (keyPressed == Keys.S && modifier == Keys.Alt)
                {
                    if (rbtOnlyShortlist.Checked == false)
                        rbtOnlyShortlist.Checked = true;
                    retValue = true;
                }

                if (keyPressed == Keys.Escape)
                {
                    if (pnlTransferProduct.Visible == true)
                    {
                        pnlTransferProduct.Visible = false;
                        mpMainSubViewControl.SetFocus(1);
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

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

        public bool saveproducts()
        {
            bool returnVal = false;
            try
            {
                foreach (DataGridViewRow prodrow in mpMainSubViewControl.Rows)
                {
                    _PurchaseOrder.ShortListID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    if (prodrow.Cells["Col_ID"].Value != null &&
                       Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        _PurchaseOrder.ProductID = prodrow.Cells["Col_ID"].Value.ToString();
                        _PurchaseOrder.SchemeQuantity = 0;
                        _PurchaseOrder.PurchaseRate = 0;
                        _PurchaseOrder.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        if (prodrow.Cells["Col_SchemeQuantity"].Value != null && prodrow.Cells["Col_SchemeQuantity"].Value.ToString() != string.Empty)
                            _PurchaseOrder.SchemeQuantity = Convert.ToInt32(prodrow.Cells["Col_SchemeQuantity"].Value.ToString());
                        if (prodrow.Cells["Col_ProdLastPurchaseRate"].Value != null && prodrow.Cells["Col_ProdLastPurchaseRate"].Value.ToString() != string.Empty)
                            _PurchaseOrder.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_ProdLastPurchaseRate"].Value.ToString());
                        returnVal = _PurchaseOrder.AddProductDetails();
                    }
                }
            }
            catch { returnVal = false; }
            return returnVal;
        }

        public bool saveproductsModified()
        {
            bool returnVal = false;
            try
            {
                foreach (DataGridViewRow prodrow in mpMainSubViewControl.Rows)
                {
                    _PurchaseOrder.ShortListID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    if (prodrow.Cells["Col_ID"].Value != null &&
                       Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        _PurchaseOrder.ProductID = prodrow.Cells["Col_ID"].Value.ToString();
                        _PurchaseOrder.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        _PurchaseOrder.SchemeQuantity = 0.00;
                        //if (prodrow.Cells["Col_SchemeQuantity"].Value != null && prodrow.Cells["Col_SchemeQuantity"].Value.ToString() != string.Empty)
                        if (prodrow.Cells["Col_SchemeQuantity"].Value != null)
                            _PurchaseOrder.SchemeQuantity = Convert.ToDouble(prodrow.Cells["Col_SchemeQuantity"].Value.ToString());
                        if (prodrow.Cells["Col_ProdLastPurchaseRate"].Value != null)
                            _PurchaseOrder.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_ProdLastPurchaseRate"].Value.ToString());
                        if (prodrow.Cells["Col_ShortListDate"].Value != null)
                            _PurchaseOrder.ShortListDate = prodrow.Cells["Col_ShortListDate"].Value.ToString();
                        if (prodrow.Cells["Col_IfDailyShortList"].Value != null)
                            _PurchaseOrder.IfDalilyShortList = prodrow.Cells["Col_IfDailyShortList"].Value.ToString();
                        if (prodrow.Cells["Col_CreatedBy"].Value!=null)
                        {
                        _PurchaseOrder.DSLCreatedBy = prodrow.Cells["Col_CreatedBy"].Value.ToString();
                        }
                        if(prodrow.Cells["Col_CreatedDate"].Value!=null)
                        _PurchaseOrder.DSLCreatedDate = prodrow.Cells["Col_CreatedDate"].Value.ToString();
                        if(prodrow.Cells["Col_CreatedTime"].Value!=null)
                        _PurchaseOrder.DSLCreatedTime = prodrow.Cells["Col_CreatedTime"].Value.ToString();

                        returnVal = _PurchaseOrder.AddProductDetailsModified();
                    }
                }
            }
            catch { returnVal = false; }
            return returnVal;
        }
        #endregion

        #region Internal methods

        private void ConstructMainColumns()
        {
            if(mpMainSubViewControl.Rows.Count > 0)
            {
                mpMainSubViewControl.Rows[0].Selected = false;
                mpMainSubViewControl.dgMainGrid.EndEdit();
                mcbCreditor.Focus();
            }
            mpMainSubViewControl.ColumnsMain.Clear();
            DataGridViewTextBoxColumn column;

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "ProductID";
            column.HeaderText = "ID";
            column.Visible = false;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProductName";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "Product Name";
            column.Width = 200;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UOM";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 75;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;           
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Pack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 80;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdCompShortName";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 75;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdBoxQuantity";
            column.DataPropertyName = "ProdBoxQuantity";
            column.HeaderText = "BoxQty";
            column.Width = 75;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdClosingStock";
            column.DataPropertyName = "ProdClosingStock";
            column.HeaderText = "ClsStock";
            column.Width = 80;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdLastPurchaseRate";
            column.DataPropertyName = "ProdLastPurchaseRate";
            column.HeaderText = "PurRate";
            column.Width = 80;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Sale";
            column.DataPropertyName = "Sale";
            column.HeaderText = "SaleQty";
            column.Width = 80;
            column.ReadOnly = true;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Quantity";
            column.DataPropertyName = "Quantity";
            column.HeaderText = "Qty";
            column.Width = 80;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_SchemeQuantity";
            column.DataPropertyName = "SchemeQuantity";
            column.HeaderText = "Scheme Qty";
            column.Width = 80;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.DataPropertyName = "Amount";
            column.HeaderText = "Amount";
            column.Width = 120;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ShortListDate";
            column.DataPropertyName = "ShortListDate";
            column.Visible = false;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_IFDailyShortList";
            column.DataPropertyName = "IfDailyShortList";
            column.Visible = false;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_CreatedBy";
            column.DataPropertyName = "CreatedUserID";
            column.Visible = false;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_CreatedDate";
            column.DataPropertyName = "CreatedDate";
            column.Visible = false;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_CreatedTime";
            column.DataPropertyName = "CreatedTime";
            column.Visible = false;
            mpMainSubViewControl.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_DSLID";
            column.DataPropertyName = "DSLID";
            column.Visible = false;
            mpMainSubViewControl.ColumnsMain.Add(column);

        }

        private void ConstructSubColumns()
        {
            mpMainSubViewControl.ColumnsSub.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "ProductID";
            column.HeaderText = "ID";
            column.Visible = false;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdName";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "Product";
            column.Width = 200;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UOM";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 50;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdPack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 52;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Comp";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 52;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_BoxQty";
            column.DataPropertyName = "ProdBoxQuantity";
            column.HeaderText = "BoxQty";
            column.Width = 55;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_BoxQty";
            column.DataPropertyName = "ProdClosingStock";
            column.HeaderText = "ClStock";
            column.Width = 55;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdLastPurchaseRate";
            column.DataPropertyName = "ProdLastPurchaseRate";
            column.HeaderText = "Pur.Rate";
            column.Width = 90;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);
        }

        private void ClearControls()
        {
            btncloneOK.Enabled = true;
            txtVouchernumber.Clear();
            datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
            txtNarration.Clear();
            txtAddress.Clear();
            txtBillAmount.Clear();
            mcbCreditor.SelectedID = "";
            InitializeMainSubViewControl();
            txtVouType.Text = _PurchaseOrder.VoucherType;
            rbtShortListNone.Checked = true;
            rbtLastSaleNone.Checked = true;
            pnlTransferProduct.Visible = false;
            txtNoOfRows.Text = "";
            txtAmount.Text = "";

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

        private void FillmcbTransferProductCombo()
        {
            mcbTransferProduct.SelectedID = null;
            mcbTransferProduct.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccDiscountOffered" };
            mcbTransferProduct.ColumnWidth = new string[5] { "0", "20", "200", "150", "50" };
            mcbTransferProduct.DisplayColumnNo = 2;
            mcbTransferProduct.ValueColumnNo = 0;
            mcbTransferProduct.UserControlToShow = new UclAccount();
            Account _Party = new Account();
            DataTable dtable = _Party.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
            mcbTransferProduct.FillData(dtable);
        }
        private void InitializeMainSubViewControl()
        {
            ConstructMainColumns();
            ConstructSubColumns();
            DataTable dtable = new DataTable();
            dtable = _PurchaseOrder.ReadProductDetailsByID(_PurchaseOrder.Id);
            // mpMainSubViewControl.DataSourceMain = dtable;
            if (dtable != null)
                BindmpMainSubViewControl(dtable);
            mpMainSubViewControl.ClearSelection();
            mpMainSubViewControl.NumericColumnNames.Add("Col_Quantity");
            mpMainSubViewControl.NumericColumnNames.Add("Col_SchemeQuantity");
            mpMainSubViewControl.NextRowColumn = 10;
            mpMainSubViewControl.DoubleColumnNames.Add("Col_Amount");
            mpMainSubViewControl.DoubleColumnNames.Add("Col_ProdLastPurchaseRate");
            Product prod = new Product();
            DataTable proddt = prod.GetOverviewData();
            //  DataTable dt = General.ProductList;
            mpMainSubViewControl.DataSource = proddt;
            mpMainSubViewControl.ReBindSubGrid();
            if (mpMainSubViewControl.Rows.Count == 0)
                mpMainSubViewControl.Rows.Add();

        }
        private void CalculateAmount()
        {
            double TotalAmount = 0;
            int itemCount = 0;
            double mtot = 0;

            foreach (DataGridViewRow dr in mpMainSubViewControl.Rows)
            {
                if (dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "")
                {
                    mtot = 0;
                    double.TryParse(dr.Cells["Col_Amount"].Value.ToString(), out mtot);
                    TotalAmount += mtot;
                    itemCount += 1;
                }

            }

            txtNoOfRows.Text = itemCount.ToString().Trim();
            txtAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");
            txtBillAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");
        }
     

        private void mpMainSubViewControl_OnCellValueChangeCommited(int colIndex)
        {
            if (colIndex == 10 || colIndex == 9)
            {
                int mqty = 0;
                double mamt = 0;
                double mprate = 0;
                int.TryParse(mpMainSubViewControl.MainDataGridCurrentRow.Cells[9].Value.ToString(), out mqty);
                if (mpMainSubViewControl.MainDataGridCurrentRow.Cells[7].Value != null)
                    double.TryParse(mpMainSubViewControl.MainDataGridCurrentRow.Cells[7].Value.ToString(), out mprate);
                mamt = Math.Round(mqty * mprate, 2);
                if (mqty <= 0)
                {
                    //mpMainSubViewControl.Rows.Remove(mpMainSubViewControl.MainDataGridCurrentRow);                   
                    //mpMainSubViewControl.Rows.Add();
                    mpMainSubViewControl.SetFocus(7);
                }
                else
                {
                    lblMessage.Text = "";
                    mpMainSubViewControl.MainDataGridCurrentRow.Cells[11].ReadOnly = false; // 10
                    mpMainSubViewControl.MainDataGridCurrentRow.Cells[11].Value = mamt.ToString("#0.00"); //10
                    mpMainSubViewControl.MainDataGridCurrentRow.Cells[11].ReadOnly = true; //10
                }

                CalculateAmount();

            }
        }

        private void mpMainSubViewControl_OnDetailsFilled(DataGridViewRow selectedRow)
        {
            string mprodID = "";
            int mrowindex = 0;
            int mcindex = 0;
            _PurchaseOrder.DuplicateProduct = false;
            if (mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_ID"].Value != null)
            {
                mprodID = mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_ID"].Value.ToString();
                mrowindex = mpMainSubViewControl.MainDataGridCurrentRow.Index;
            }
            foreach (DataGridViewRow prodrow in mpMainSubViewControl.Rows)
            {
                if (prodrow.Cells["Col_ID"].Value != null)
                {
                    _PurchaseOrder.ProductID = prodrow.Cells["Col_ID"].Value.ToString();
                    mcindex = prodrow.Index;
                    if (_PurchaseOrder.ProductID == mprodID && mrowindex != mcindex)
                    {
                        _PurchaseOrder.DuplicateProduct = true;
                        mpMainSubViewControl.Rows.Remove(mpMainSubViewControl.Rows[mrowindex]);
                        break;
                    }
                }
            }
            ShowLastSoldQuantity();
            CalculateAmount();
        }

        private void ShowLastSoldQuantity()
        {
            string mprod = mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_ID"].Value.ToString();
            int lastsoldqty = LastSoldStock(mprod);
            mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_Sale"].Value = lastsoldqty.ToString("#0");
        }

        private int LastSoldStock(string mprod)
        {
            int lastsolddays = 0;
            string lastdate = "";
            int lastsoldqty = 0;
            if (txtSaleDays.Text != null && txtSaleDays.Text.ToString() != string.Empty && txtSaleDays.Text.ToString() != "0")
            {
                lastsolddays = Convert.ToInt32(txtSaleDays.Text.ToString());
                DateTime today = DateTime.Now;
                DateTime lastday = today.AddDays(lastsolddays * -1);
                lastdate = lastday.Date.ToString("yyyyMMdd");
            }
            lastsoldqty = _PurchaseOrder.GetSaleDataForLastSoldDays(mprod, lastdate);
            return lastsoldqty;
        }

        private void mpMainSubViewControl_OnRowDeleted(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        #endregion Internal methods

        #region Events

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtSaleDays.Focus();
                    break;
                case Keys.Up:
                    mcbCreditor.Focus();
                    break;
            }
        }

        private void txtSaleDays_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    GetLastSale();
                    mpMainSubViewControl.SetFocus(1);
                    break;
                case Keys.Up:
                    txtNarration.Focus();
                    break;
            }
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mpMainSubViewControl.dgMainGrid.Rows[0].Cells[7].Selected = true; //Amar


            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void btncloneOK_Click(object sender, EventArgs e)
        {
            BtnCloneOKClick();
        }

        private void BtnCloneOKClick()
        {
            btncloneOK.Enabled = false;
            _DailyPurchaseOrder.FromDay = datetimepickerFrom.Value.Date.ToString("yyyyMMdd");
            _DailyPurchaseOrder.EndDay = datetimepickerTo.Value.Date.ToString("yyyyMMdd");
            double days = (datetimepickerTo.Value - datetimepickerFrom.Value).TotalDays;
            days += 1;

            DataTable dtable = new DataTable();
            if (rbtOnlyShortlist.Checked == true)
            {
                dtable = _PurchaseOrder.ReadProductDetailsByAccountID(mcbCreditor.SelectedID, _DailyPurchaseOrder.FromDay, _DailyPurchaseOrder.EndDay);
                if (mpMainSubViewControl.Rows.Count > 0)
                    mpMainSubViewControl.Rows.Clear();
                BindmpMainSubViewControl(dtable);

            }
            else if (rbtTodaySale.Checked == true)
            {
                _DailyPurchaseOrder.DSLAccountID = string.Empty;
                if (mcbCreditor.SelectedID != null)
                    _DailyPurchaseOrder.DSLAccountID = mcbCreditor.SelectedID;

                dtable = _DailyPurchaseOrder.ReadShortListByDateForTodayByAccountID();
                if (mpMainSubViewControl.Rows.Count > 0)
                    mpMainSubViewControl.Rows.Clear();
                BindmpMainSubViewControl(dtable);
            }
            if (rbtLastOrderAllProducts.Checked)
            {
                RemoveBlankRow();
                _DailyPurchaseOrder.DSLAccountID = string.Empty;
                if (mcbCreditor.SelectedID != null)
                    _DailyPurchaseOrder.DSLAccountID = mcbCreditor.SelectedID;

                dtable = _DailyPurchaseOrder.ReadLastOrderAllProducts(_DailyPurchaseOrder.DSLAccountID);
                DataRow firstdr = null;
                int lastordernumber = 0;
                if (dtable.Rows.Count > 0)
                {
                    firstdr = dtable.Rows[0];
                    lastordernumber = Convert.ToInt32(firstdr["OrderNumber"].ToString());
                }

                BindmpMainSubViewControlLastOrder(dtable, lastordernumber);

            }
            else if (rbtLastOrderRemainingProducts.Checked)
            {
                RemoveBlankRow();
                _DailyPurchaseOrder.DSLAccountID = string.Empty;
                if (mcbCreditor.SelectedID != null)
                    _DailyPurchaseOrder.DSLAccountID = mcbCreditor.SelectedID;

                dtable = _DailyPurchaseOrder.ReadLastOrderRemainingProducts(_DailyPurchaseOrder.DSLAccountID);
                DataRow firstdr = null;
                int lastordernumber = 0;
                if (dtable.Rows.Count > 0)
                {
                    firstdr = dtable.Rows[0];
                    lastordernumber = Convert.ToInt32(firstdr["OrderNumber"].ToString());
                }

                BindmpMainSubViewControlLastOrder(dtable, lastordernumber);
            }
            GetLastSale();
            CalculateAmount();
            mpMainSubViewControl.Rows.Add();
            mpMainSubViewControl.SetFocus(1);

        }

        private void RemoveBlankRow()
        {

            foreach (DataGridViewRow drr in mpMainSubViewControl.Rows)
            {
                if (mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_ID"].Value == null)
                {
                    mpMainSubViewControl.Rows.Remove(mpMainSubViewControl.MainDataGridCurrentRow);
                }

            }
            //  mpMainSubViewControl.Refresh();
        }

        private bool BindmpMainSubViewControlLastOrder(DataTable dt, int lastordernumber)
        {
            bool retValue = true;
            int rowindex = 0;
            double mamt = 0;
            double mprate = 0;
            double mqty = 0;
            string drrproductid = "";
            string productid = "";
            bool found = false;
            int ordernumber = 0;
            try
            {

                foreach (DataRow dr in dt.Rows)
                {
                    mprate = 0;
                    mqty = 0;
                    found = false;
                    if (dr["ProductID"] != DBNull.Value)
                    {
                        productid = dr["ProductID"].ToString();
                        ordernumber = Convert.ToInt32(dr["OrderNumber"].ToString());
                        if (ordernumber == lastordernumber)
                        {
                            foreach (DataGridViewRow drr in mpMainSubViewControl.Rows)
                            {
                                drrproductid = "";

                                if (drr.Cells["Col_ID"].Value != null)
                                {
                                    drrproductid = drr.Cells["Col_ID"].Value.ToString();

                                    if (drrproductid == productid)
                                    {
                                        found = true;
                                        drr.Cells["Col_ProdLastPurchaseRate"].Value = dr["ProdLastPurchaseRate"].ToString();
                                        drr.Cells["Col_Quantity"].Value = dr["OrderQuantity"].ToString();
                                        drr.Cells["Col_SchemeQuantity"].Value = dr["SchemeQuantity"].ToString();
                                        drr.Cells["Col_ProdClosingStock"].Value = dr["ProdClosingStock"].ToString();
                                        drr.Cells["Col_Sale"].Value = dr["Quantity"].ToString();
                                        if (dr["ProdLastPurchaseRate"] != DBNull.Value)
                                        {
                                            mprate = Convert.ToDouble(dr["ProdLastPurchaseRate"].ToString());
                                        }
                                        if (dr["OrderQuantity"] != DBNull.Value)
                                            mqty = Convert.ToDouble(dr["OrderQuantity"].ToString());
                                        mamt = mprate * mqty;
                                        drr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                                        drr.DefaultCellStyle.BackColor = Color.LightBlue;
                                        break;
                                    }
                                }

                            }
                            if (found == false)
                            {
                                rowindex = mpMainSubViewControl.Rows.Add();
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_ID"].Value = dr["ProductID"].ToString();
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_UOM"].Value = dr["ProdLoosePack"].ToString();
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_Pack"].Value = dr["ProdPack"].ToString();
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_ProdCompShortName"].Value = dr["ProdCompShortName"].ToString();
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_ProdBoxQuantity"].Value = dr["ProdBoxQuantity"].ToString();
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_ProdClosingStock"].Value = dr["ProdClosingStock"].ToString();
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_ProdLastPurchaseRate"].Value = dr["ProdLastPurchaseRate"].ToString();
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_Quantity"].Value = dr["OrderQuantity"].ToString();
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_SchemeQuantity"].Value = dr["SchemeQuantity"].ToString();
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_Sale"].Value = dr["Quantity"].ToString();
                                if (dr["ProdLastPurchaseRate"] != DBNull.Value)
                                {
                                    mprate = Convert.ToDouble(dr["ProdLastPurchaseRate"].ToString());
                                }
                                if (dr["OrderQuantity"] != DBNull.Value)
                                    mqty = Convert.ToDouble(dr["OrderQuantity"].ToString());
                                mamt = mprate * mqty;
                                mpMainSubViewControl.Rows[rowindex].Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                                mpMainSubViewControl.Rows[rowindex].DefaultCellStyle.BackColor = Color.LightBlue;
                            }
                        }
                    }
                    else
                    {
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

        private void GetLastSale()
        {
            string mprod = string.Empty;
            int lastsoldstock = 0;
            //  mpMainSubViewControl.ColumnsMain["Col_Sale"].ReadOnly = false;
            if (txtSaleDays.Text != null && txtSaleDays.Text.ToString() != string.Empty)
            {
                foreach (DataGridViewRow dr in mpMainSubViewControl.Rows)
                {
                    mprod = string.Empty;
                    lastsoldstock = 0;
                    if (dr.Cells["Col_ID"].Value != null)
                        mprod = dr.Cells["Col_ID"].Value.ToString();
                    if (mprod != string.Empty)
                        lastsoldstock = LastSoldStock(mprod);
                    dr.Cells["Col_Sale"].Value = lastsoldstock.ToString("#0");

                }

            }
        }

        private bool BindmpMainSubViewControl(DataTable dt)
        {
            bool retValue = true;
            int rowindex = 0;
            double mamt = 0;
            double mprate = 0;
            double mqty = 0;
            try
            {

                foreach (DataRow dr in dt.Rows)
                {
                    mprate = 0;
                    mqty = 0;
                    if (dr["ProductID"] != DBNull.Value)
                    {

                        rowindex = mpMainSubViewControl.Rows.Add();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_ID"].Value = dr["ProductID"].ToString();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_ProductName"].ReadOnly = true;
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_UOM"].Value = dr["ProdLoosePack"].ToString();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_Pack"].Value = dr["ProdPack"].ToString();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_ProdCompShortName"].Value = dr["ProdCompShortName"].ToString();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_ProdBoxQuantity"].Value = dr["ProdBoxQuantity"].ToString();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_ProdClosingStock"].Value = dr["ProdClosingStock"].ToString();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_ProdLastPurchaseRate"].Value = dr["ProdLastPurchaseRate"].ToString();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_Quantity"].Value = dr["OrderQuantity"].ToString();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_SchemeQuantity"].Value = dr["SchemeQuantity"].ToString();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_Sale"].Value = dr["Quantity"].ToString();
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_DSLID"].Value = dr["DSLID"].ToString();
                        if (dr["ProdLastPurchaseRate"] != DBNull.Value)
                        {
                            mprate = Convert.ToDouble(dr["ProdLastPurchaseRate"].ToString());
                        }
                        if (dr["OrderQuantity"] != DBNull.Value)
                            mqty = Convert.ToDouble(dr["OrderQuantity"].ToString());
                        mamt = mprate * mqty;
                        mpMainSubViewControl.Rows[rowindex].Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void UclPurchaseOrder_Load(object sender, EventArgs e)
        {
            datetimepickerFrom.ResetText();
            datetimepickerTo.ResetText();
        }


        private void mpMainSubViewControl_OnTABKeyPressed(object sender, EventArgs e)
        {
            DataGridViewRow dataGridViewRow = mpMainSubViewControl.MainDataGridCurrentRow;
            pnlTransferProduct.Visible = true;
            mcbTransferProduct.Focus();

        }

        private void mcbTransferProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            string accountID = string.Empty;
            string productID = string.Empty;
            string dslID = string.Empty;
            int mqty = 0;
            int mschemeqty = 0;

            if (mcbTransferProduct.SelectedID != null && mcbTransferProduct.SelectedID != string.Empty)
                accountID = mcbTransferProduct.SelectedID;
            if (mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_ID"].Value != null && mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_ID"].Value.ToString() != string.Empty)
                productID = mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_ID"].Value.ToString();
            if (mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null && mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString() != string.Empty)
                mqty = Convert.ToInt32(mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
            if (mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_SchemeQuantity"].Value != null && mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_SchemeQuantity"].Value.ToString() != string.Empty)
                mschemeqty = Convert.ToInt32(mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_SchemeQuantity"].Value.ToString());
            if (mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_DSLID"].Value != null)
                dslID = mpMainSubViewControl.MainDataGridCurrentRow.Cells["Col_DSLID"].Value.ToString();

            if (accountID != string.Empty && productID != string.Empty)
            {
                if (_Mode == OperationMode.Add)
                {
                    if (rbtOnlyShortlist.Checked == true)
                        _PurchaseOrder.TransferProductShortList(accountID, dslID, mqty);
                    else if (rbtTodaySale.Checked == true)
                        _PurchaseOrder.TransferProductTodaySale(accountID, productID, mqty);
                }

                mpMainSubViewControl.Rows.Remove(mpMainSubViewControl.MainDataGridCurrentRow);
                mpMainSubViewControl.Refresh();
            }
            pnlTransferProduct.Visible = false;
            mcbTransferProduct.SelectedID = "";
            mpMainSubViewControl.SetFocus(1);
        }

        private void mcbTransferProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                pnlTransferProduct.Visible = false;
            }
        }

        #endregion Events

        private void mpMainSubViewControl_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
