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
using PharmaSYSRetailPlus.Printing;


namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclPurchaseOrder : BaseControl
    {
        #region Declaration     
        private PurchaseOrder _PurchaseOrder;       
        #endregion

        #region Constructor
        public UclPurchaseOrder()
        {
            InitializeComponent();
            _PurchaseOrder = new PurchaseOrder();
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
            InitializeMainSubViewControl();
            mcbCreditor.Enabled = true;
            txtVouchernumber.Enabled = false;
            FillCreditorCombo();
            SetFocus();

            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();          
            headerLabel1.Text = "PURCHASE ORDER -> EDIT";
            InitializeMainSubViewControl();
            FillCreditorCombo();
            mcbCreditor.Enabled = false;
            txtVouchernumber.Enabled = true;
            txtVouchernumber.ReadOnly = false;
            SetFocus();
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
                            rowCollection.Add(prodrow); 
                    }
                    totalrows = 0;
                    PrintPageNumber = 0;
                    rowcount = 0;
                    totpages = 0;
                    totalpages = 0;                   
                    PrintRowPixel = 0;
                    totalrows = rowCollection.Count();
                    totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill ));
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
                        int myqty = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        //.Cells["Col_Quantity"].Value.ToString());
                        row = new PrintRow(myqty.ToString("#0"), PrintRowPixel, 15, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(prodrow.Cells["Col_ProductName"].Value.ToString(), PrintRowPixel, 85, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(prodrow.Cells["Col_UOM"].Value.ToString(), PrintRowPixel, 260, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(" X " + prodrow.Cells["Col_Pack"].Value.ToString(), PrintRowPixel, 280, fnt);
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
            _PurchaseOrder.Validate();
            if (_PurchaseOrder.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _PurchaseOrder.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _PurchaseOrder.VoucherNumber = getno.GetPurchaseOrder(General.ShopDetail.ShopVoucherSeries);
                    txtVouchernumber.Text = _PurchaseOrder.VoucherNumber.ToString();
                    _PurchaseOrder.CreatedBy = General.CurrentUser.Id;
                    _PurchaseOrder.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _PurchaseOrder.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    _PurchaseOrder.AddDetails();
                    retValue = saveproducts();

                    if (retValue == true)
                    {
                        MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _PurchaseOrder.Id;
                    }
                    else
                    {
                        MessageBox.Show("Unable to save Information ", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    ClearControls();              
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _PurchaseOrder.ModifiedBy = General.CurrentUser.Id;
                    _PurchaseOrder.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _PurchaseOrder.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    _PurchaseOrder.UpdateDetails();
                    _PurchaseOrder.DeletePreviousProducts();
                    retValue = saveproductsModified();

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
            return true;
        }
        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData()
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
                        _PurchaseOrder.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        if (prodrow.Cells["Col_ProdLastPurchaseRate"].Value != null)
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
                        if (prodrow.Cells["Col_ProdLastPurchaseRate"].Value != null)
                            _PurchaseOrder.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_ProdLastPurchaseRate"].Value.ToString());
                        if (prodrow.Cells["Col_ShortListDate"].Value != null)
                            _PurchaseOrder.ShortListDate = prodrow.Cells["Col_ShortListDate"].Value.ToString();
                        if (prodrow.Cells["Col_IfDailyShortList"].Value != null)
                            _PurchaseOrder.IfDalilyShortList = prodrow.Cells["Col_IfDailyShortList"].Value.ToString();
                        _PurchaseOrder.DSLCreatedBy = prodrow.Cells["Col_CreatedBy"].Value.ToString();
                        _PurchaseOrder.DSLCreatedDate = prodrow.Cells["Col_CreatedDate"].Value.ToString();
                        _PurchaseOrder.DSLCreatedTime = prodrow.Cells["Col_CreatedTime"].Value.ToString();
                        returnVal = _PurchaseOrder.AddProductDetailsModified();
                    }
                }
            }
            catch { returnVal = false; }
            return returnVal;
        }
        # endregion
                
        private void ConstructMainColumns()
        {
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
            mpMainSubViewControl.ColumnsMain.Add(column);
          
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UOM";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 75;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);
           
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Pack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 80;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);
           
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdCompShortName";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 75;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);
           
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdBoxQuantity";
            column.DataPropertyName = "ProdBoxQuantity";
            column.HeaderText = "Box Quantity";
            column.Width = 75;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);
           
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdClosingStock";
            column.DataPropertyName = "ProdClosingStock";
            column.HeaderText = "Closing Stock";
            column.Width = 80;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);
          
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdLastPurchaseRate";
            column.DataPropertyName = "ProdLastPurchaseRate";
            column.HeaderText = "Purchase Rate";
            column.Width = 80;            
            mpMainSubViewControl.ColumnsMain.Add(column);
           
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Sale";
            column.DataPropertyName = "Sale";
            column.HeaderText = "Sale Quantity";
            column.Width = 80;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsMain.Add(column);
                 
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Quantity";
            column.DataPropertyName = "Quantity";
            column.HeaderText = "Quantity";
            column.Width = 80;
            mpMainSubViewControl.ColumnsMain.Add(column);
            
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.DataPropertyName = "Amount";
            column.HeaderText = "Amount";
            column.Width = 95;
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
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UOM";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 50;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdPack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 52;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Comp";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 52;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_BoxQty";
            column.DataPropertyName = "ProdBoxQuantity";
            column.HeaderText = "BoxQty";
            column.Width = 55;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_BoxQty";
            column.DataPropertyName = "ProdClosingStock";
            column.HeaderText = "ClStock";
            column.Width = 55;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdLastPurchaseRate";
            column.DataPropertyName = "ProdLastPurchaseRate";
            column.HeaderText = "Pur.Rate";
            column.Width = 90;
            column.ReadOnly = true;
            mpMainSubViewControl.ColumnsSub.Add(column);
        }

        private void ClearControls()
        {
            txtVouchernumber.Clear();
            datePickerBillDate.ResetText();
            txtNarration.Clear();
            txtAddress.Clear();
            txtBillAmount.Clear();
            mcbCreditor.SelectedID = "";
            InitializeMainSubViewControl();
            txtVouType.Text = _PurchaseOrder.VoucherType;

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
        private void InitializeMainSubViewControl()
        {
            ConstructMainColumns();
            ConstructSubColumns();            
            DataTable dtable = new DataTable();
            dtable = _PurchaseOrder.ReadProductDetailsByID();
            mpMainSubViewControl.DataSourceMain = dtable;
            mpMainSubViewControl.NumericColumnNames.Add("Col_Quantity");
            mpMainSubViewControl.NextRowColumn = 9;
            mpMainSubViewControl.DoubleColumnNames.Add("Col_Amount");
            mpMainSubViewControl.DoubleColumnNames.Add("Col_ProdLastPurchaseRate");
            DataTable dt = General.ProductList;
            mpMainSubViewControl.DataSource = dt;
            mpMainSubViewControl.Bind();
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
            if (colIndex == 9)
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
                    mpMainSubViewControl.Rows.Remove(mpMainSubViewControl.MainDataGridCurrentRow);                   
                    mpMainSubViewControl.Rows.Add();
                }
                else
                {
                    lblMessage.Text = "";
                    mpMainSubViewControl.MainDataGridCurrentRow.Cells[10].ReadOnly = false;
                    mpMainSubViewControl.MainDataGridCurrentRow.Cells[10].Value = mamt.ToString("#0.00");
                    mpMainSubViewControl.MainDataGridCurrentRow.Cells[10].ReadOnly = true;
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
            CalculateAmount();
        }

        private void mpMainSubViewControl_OnRowDeleted(object sender, EventArgs e)
        {
            CalculateAmount();
        }

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
                    mpMainSubViewControl.SetFocus(1);
                    break;
                case Keys.Up:
                    txtNarration.Focus();
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
                        _PurchaseOrder.ReadDetailsByVoucherNumber(vouno);
                        FillSearchData(_PurchaseOrder.Id,"");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void rbtOnlyShortlist_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtOnlyShortlist.Checked == true)
            {
                if (mpMainSubViewControl.Rows.Count == 1)
                {
                    DataTable dtable = new DataTable();
                    dtable = _PurchaseOrder.ReadProductDetailsByAccountID(mcbCreditor.SelectedID);
                    mpMainSubViewControl.DataSourceMain = dtable;
                    mpMainSubViewControl.Bind();
                    mpMainSubViewControl.Rows.Add();
                    CalculateAmount();
                    mpMainSubViewControl.SetFocus(1);
                }
                else
                {
                    lblMessage.Text = "Can not Add Products";
                }
            }

        }
    }
}
