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
using System.Collections;

namespace EcoMart.InterfaceLayer.CommonControls
{
    public partial class PSProductViewControlCRDB : UserControl
    {

        private BindingSource _BindingSourceProductList;
        private BindingSource _BindingSourceBatchList;

        private DataTable _DataTableProductList;
        private DataTable _DataTableMain;
        private DataTable _DataTableBatchList;

        private string _sProductListFilter = null;

        public delegate void CellValueChangeCommited(int colIndex);
        public event CellValueChangeCommited OnCellValueChangeCommited;

        public delegate void ProductSelected(DataGridViewRow productRow);
        public event ProductSelected OnProductSelected;

        public delegate void BatchSelected(DataGridViewRow batchRow);
        public event BatchSelected OnBatchSelected;

        public delegate void NewBatchClicked();
        public event NewBatchClicked OnNewBatchClicked;

        public delegate void SelectedProductClosingStock(int closingStockValue, int ProductID);
        public event SelectedProductClosingStock OnSelectedProductClosingStock;

        public delegate DataRow OnProductBarCodeScan(string scanCode);
        public event OnProductBarCodeScan OnProductBarCodeScaned;

        public event DataGridViewCellEventHandler OnCellEnter;
        public event DataGridViewCellEventHandler OnCellLeave;
        public event DataGridViewCellEventHandler OnCellContentClick;

        public delegate bool CanRowDeleted(object sender, EventArgs e);
        public event CanRowDeleted OnCanRowDeleted;  

        public event EventHandler OnRowAdded;
        public event EventHandler OnRowDeleted;
        public event EventHandler OnTABKeyPressed;
        public event EventHandler OnShiftTABKeyPressed;
        public event EventHandler OnEscapeKeyPressed;


        private DataGridViewRow _DataGridViewSelectedRow;
        private bool _AllowNewBatch = true;
        // ss 12/6/2015
        private bool _ShowBatchWithZeroStock = false;
        // ss 12/6/2015
        private bool _ShowProductContent = true;
        private string _BatchGridShowColumnName;
        private string _BatchColumnName;
        private string _BatchGridIDColumnName;
        private string _ProductGridLastStockIDColumnName;
        private string _ProductGridClosingStockColumnName;
        private string _MainGridSoldQuantityColumnName;

        private ModuleNumber _ModuleNumber;
        private OperationMode _OperationMode;

        private Hashtable htDeletedStock;

        private Color CellDefaultColor = Color.White;      

        public PSProductViewControlCRDB()
        {
            InitializeComponent();
            dgMainGrid.AutoGenerateColumns = false;
            dgProductListGrid.AutoGenerateColumns = false;     
                   

            _DataTableProductList = null;
            _DataTableBatchList = null;



            _BindingSourceProductList = new BindingSource();
            _BindingSourceBatchList = new BindingSource();



            _DataGridViewSelectedRow = new DataGridViewRow();
            _BatchGridIDColumnName = "Col_StockID";
            _ProductGridLastStockIDColumnName = "Col_LastStockID";
            _ProductGridClosingStockColumnName = string.Empty;
            _MainGridSoldQuantityColumnName = string.Empty;
            _BatchColumnName = "Col_BatchNumber";

            htDeletedStock = new Hashtable();
        }

        public DataGridViewColumnCollection ColumnsMain
        {
            get { return dgMainGrid.Columns; }
        }

        public DataGridViewColumnCollection ColumnsProductList
        {
            get { return dgProductListGrid.Columns; }
        }

        public DataGridViewColumnCollection ColumnsBatchList
        {
            get { return dgBatchListGrid.Columns; }
        }

        public DataTable DataSourceBatchList
        {
            get
            {
                return _DataTableBatchList;
            }
            set
            {
                _DataTableBatchList = value;
            }
        }

        public DataTable DataSourceProductList
        {
            get
            {
                return _DataTableProductList;
            }
            set
            {
                _DataTableProductList = value;
            }
        }

        public DataTable DataSourceMain
        {
            get
            {
                return _DataTableMain;
            }
            set
            {
                _DataTableMain = value;
            }
        }

        public DataGridViewRowCollection Rows
        {
            get { return dgMainGrid.Rows; }
        }

        public DataGridViewRow SelectedRow
        {
            get { return _DataGridViewSelectedRow; }
        }

        public string ProductListFilter
        {
            get { return _sProductListFilter; }
            set { _sProductListFilter = value; }
        }

        public int ProductListGridWidth
        {
            get { return pnlProductListGrid.Width; }
            set { pnlProductListGrid.Width = value; }
        }

        public int BatchListGridWidth
        {
            get { return pnlBatchGrid.Width; }
            set { pnlBatchGrid.Width = value; }
        }
      
        public ArrayList NumericColumnNames
        {
            get { return dgMainGrid.NumericColumnNames; }
            set { dgMainGrid.NumericColumnNames = value; }
        }

        public ArrayList DoubleColumnNames
        {
            get { return dgMainGrid.DoubleColumnNames; }
            set { dgMainGrid.DoubleColumnNames = value; }
        }

        public int NextRowColumn
        {
            get { return dgMainGrid.NextRowColumn; }
            set { dgMainGrid.NextRowColumn = value; }
        }
        // ss 27/08/2013
        //public int CurrentQuantity
        //{
        //    get { return _CurrentQuantity; }
        //    set { _CurrentQuantity = value; }
        //}
        // ss 27/08/2013

        public bool AllowNewBatch
        {
            get { return _AllowNewBatch; }
            set
            {
                _AllowNewBatch = value;
                if (_AllowNewBatch)
                {
                    btnNew.Visible = true;
                    this.dgBatchListGrid.Dock = System.Windows.Forms.DockStyle.Top;
                }
                else
                {
                    btnNew.Visible = false;
                    this.dgBatchListGrid.Dock = System.Windows.Forms.DockStyle.Fill;

                }
            }
        }
        // ss 15/12/2014
        public bool ShowProductContent
        {
            get { return _ShowProductContent; }
            set
            {
                _ShowProductContent = value;
                lblGenericName.Visible = _ShowProductContent;
            }
        }

        //ss 12/6/2015
        public bool ShowBatchWithZeroStock
        {
            get { return _ShowBatchWithZeroStock; }
            set { _ShowBatchWithZeroStock = value;}
        }
        // ss  12/6/2015
        public bool IsAllowNewRow
        {
            get { return dgMainGrid.IsAllowNewRow; }

            set { dgMainGrid.IsAllowNewRow = value; }
        }

        public bool IsAllowDelete
        {
            get { return dgMainGrid.IsAllowDelete; }

            set { dgMainGrid.IsAllowDelete = value; }
        }

        public string NewRowColumnName
        {
            get { return dgMainGrid.NewRowColumnName; }
            set { dgMainGrid.NewRowColumnName = value; }
        }

        public string BatchGridShowColumnName
        {
            get { return _BatchGridShowColumnName; }
            set { _BatchGridShowColumnName = value; }
        }

        public string BatchColumnName
        {
            get { return _BatchColumnName; }
            set { _BatchColumnName = value; }
        }

        public string ProductGridClosingStockColumnName
        {
            get { return _ProductGridClosingStockColumnName; }
            set { _ProductGridClosingStockColumnName = value; }
        }

        public string MainGridSoldQuantityColumnName
        {
            get { return _MainGridSoldQuantityColumnName; }
            set { _MainGridSoldQuantityColumnName = value; }
        }
        public ModuleNumber ModuleNumber
        {
            get { return _ModuleNumber; }
            set { _ModuleNumber = value; }
        }

        public OperationMode OperationMode
        {
            get { return _OperationMode; }
            set
            {
                _OperationMode = value;
                htDeletedStock = new Hashtable();
            }
        }

        public string ComputerName
        {
            get { return Environment.MachineName; }
        }

        public void Bind()
        {
            try
            {
                BindGridMain();
                BindGridProductList();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public void ReBindProductListGrid()
        {
            BindGridProductList();
        }

        public void ClosePopupGrid()
        {
            pnlProductListGrid.Visible = false;
            pnlBatchGrid.Visible = false;
        }

        public bool LoadProduct(int ProductID)
        {
            bool retValue = false;
            if (dgMainGrid.IsFirstColumn())
            {
                DoProductListFilterForProductID(ProductID);
                if (dgProductListGrid.Rows.Count > 0)
                    retValue = FillProductDetails();
                else
                {
                    pnlProductListGrid.Visible = false;
                    dgMainGrid.CurrentCell.Value = "";
                    SetFocus(dgMainGrid.CurrentCell.ColumnIndex);
                    retValue = true;
                }
            }
            return retValue;
        }
        private void BindGridMain()
        {
            try
            {
                try
                {
                    //Disable Sorting                    
                    foreach (DataGridViewColumn column in dgMainGrid.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    if (_DataTableMain != null)
                    {
                        dgMainGrid.Rows.Clear();
                        for (int index = 0; index < _DataTableMain.Rows.Count; index++)
                        {
                            if (_DataTableMain.Rows[index][0] != null && _DataTableMain.Rows[index][0].ToString() != "") // ss 9/8/2013
                            {
                                int rowIndex = dgMainGrid.Rows.Add();
                                DataGridViewRow dr = dgMainGrid.Rows[rowIndex];
                                for (int colIndex = 0; colIndex < dgMainGrid.Columns.Count; colIndex++)
                                {
                                    DataGridViewColumn col = dgMainGrid.Columns[colIndex];
                                    if (col.DataPropertyName != null && col.DataPropertyName != "")
                                    {
                                        if (DoubleColumnNames != null &&  DoubleColumnNames.Contains(col.Name))
                                        {
                                            dr.Cells[col.Name].Value = Convert.ToDouble(_DataTableMain.Rows[index][col.DataPropertyName]).ToString("#0.00");
                                        }
                                        else
                                        {
                                            dr.Cells[col.Name].Value = _DataTableMain.Rows[index][col.DataPropertyName].ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    dgMainGrid.Initialize();
                }
                catch (Exception ex) { Log.WriteError(ex.ToString()); }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public void BindGridProductList()
        {
            try
            {
                if (DataSourceProductList != null)
                {
                    _BindingSourceProductList.Filter = ProductListFilter;
                    _BindingSourceProductList.DataSource = DataSourceProductList;
                    dgProductListGrid.DataSource = _BindingSourceProductList;
                    // ss 20/5/2014
                    if (dgProductListGrid.Columns.Count > 0)
                        dgProductListGrid.Columns[0].Visible = false;
                    pnlProductListGrid.Visible = false;                   
                    pnlBatchGrid.Visible = false;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void FillGridBatchList(int ProductID)
        {
            try
            {
                //  added ProductID in first DataSourceBatchList 13/2/2015 ss
                SsStock stock = new SsStock();
                DataSourceBatchList = stock.GetStockByProductIDForDBCRNote(0);             
                dgBatchListGrid.DataSource = DataSourceBatchList;
                // ss 12/6/2015
               // DataSourceBatchList = stock.GetStockByProductIDForDBCRNote(ProductID);
                DataSourceBatchList = stock.GetStockByProductIDForDBCRNote(ProductID);
                // ss 12/6/2015
                // ss 12/11 2015
                if (DataSourceBatchList != null && DataSourceBatchList.Rows.Count > 0)
                {
                    foreach (DataRow dr in DataSourceBatchList.Rows)
                    {
                        string pdate = "";
                        if (dr["LastPurchaseDate"] != DBNull.Value && dr["LastPurchaseDate"].ToString() != string.Empty)
                        {
                            pdate = dr["LastPurchaseDate"].ToString();
                            char firstchar = pdate.ToCharArray().ElementAt(0);
                            bool ifIsNumber = char.IsNumber(firstchar);
                            if (ifIsNumber)
                            {
                                pdate = General.GetDateInShortDateFormat(pdate);
                            }
                            dr["LastPurchaseDate"] = pdate;
                        }
                    }
                }
                if (DataSourceBatchList != null && DataSourceBatchList.Rows.Count > 0)
                {
                    // RecalculateBatchQuantity(DataSourceBatchList);
                    RecalculateBatchQuantityForCurrentRow(DataSourceBatchList);
                    //  RecalculateBatchQuantityForEditRow(DataSourceBatchList);
                    if (ShowBatchWithZeroStock == false)
                        DataSourceBatchList = RemoveNoStockBatchRows(DataSourceBatchList);
                    DataSourceBatchList = AddCurrentStockRow(DataSourceBatchList,ProductID);
                    _BindingSourceBatchList.DataSource = DataSourceBatchList;                   
                    dgBatchListGrid.DataSource = _BindingSourceBatchList;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private DataTable AddCurrentStockRow(DataTable DataSourceBatchList, int ProductID)
        {
            try
            {
                // ss 8 jun 2016
                if (MainDataGridCurrentRow.Cells[0].ToString() != "" && Convert.ToInt32(MainDataGridCurrentRow.Cells[0].ToString()) == ProductID)
                {
                    if (MainDataGridCurrentRow.Cells[_BatchGridIDColumnName].Value != null && MainDataGridCurrentRow.Cells[_BatchGridIDColumnName].Value.ToString() != string.Empty)
                    {
                        bool IsFound = false;
                        for (int index = 0; index < DataSourceBatchList.Rows.Count; index++)
                        {
                            DataRow drBatch = DataSourceBatchList.Rows[index];
                            if (drBatch["StockID"].ToString() == (MainDataGridCurrentRow.Cells[_BatchGridIDColumnName].Value.ToString()))
                            {
                                IsFound = true;
                                break;
                            }
                        }
                        if (IsFound == false)
                        {
                            SsStock stock = new SsStock();
                            DataTable dtStockList = stock.GetStockByStockIDForDBCRNote(MainDataGridCurrentRow.Cells[_BatchGridIDColumnName].Value.ToString());
                            if (dtStockList.Rows.Count > 0)
                            {
                                DataSourceBatchList.Rows.Add(dtStockList.Rows[0].ItemArray);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return DataSourceBatchList;
        }

        private DataTable RemoveNoStockBatchRows(DataTable DataSourceBatchList)
        {
            DataTable dtBatchList = DataSourceBatchList.Clone();
            try
            {
                for (int index = 0; index < DataSourceBatchList.Rows.Count; index++)
                {
                    DataRow drBatch = DataSourceBatchList.Rows[index];
                    // ss 8/5/2015
                    if (General.CurrentSetting.MsetSaleAllowNegativeStock != "Y")
                    {
                        if (Convert.ToInt32(drBatch["ClosingStock"].ToString()) > 0)
                        {
                            dtBatchList.ImportRow(DataSourceBatchList.Rows[index]);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(drBatch["ClosingStock"].ToString()) > 0 || Convert.ToInt32(drBatch["ClosingStock"].ToString()) < 0)
                        {
                            dtBatchList.ImportRow(DataSourceBatchList.Rows[index]);
                        }
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return dtBatchList;
        }

        private void RecalculateBatchQuantityForEditRow(DataTable dtBatch)
        {
            //try
            //{
            //    for (int index = 0; index < dtBatch.Rows.Count; index++)
            //    {
            //        DataRow drBatch = dtBatch.Rows[index];
            //        if (htDeletedStock.Contains(drBatch["StockID"].ToString()))
            //        {
            //            TempStock tStock = new TempStock();
            //            DataTable dtStock = tStock.GetStockByStockID(drBatch["StockID"].ToString(), ModuleNumber, ComputerName, OperationMode);
            //            if (dtStock != null && dtStock.Rows.Count == 0)
            //            {
            //                int soldStock = Convert.ToInt32(htDeletedStock[drBatch["StockID"].ToString()]);
            //                drBatch["ClosingStock"] = Convert.ToInt32(drBatch["ClosingStock"].ToString()) + soldStock;
            //            }                        
            //        }                 
            //    }
            //}
            //catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void RecalculateBatchQuantityForCurrentRow(DataTable dtBatchList)
        {
            // ss 24/10/2013 check for closing stock = dbnull.value

            //   int _closingStock = 0;
            try
            {
                //if (dtBatchList != null)
                //{
                //    if (dgMainGrid.CurrentRow.Cells["Col_Quantity"].Value != null)
                //    {
                //        for (int index = 0; index < dtBatchList.Rows.Count; index++)
                //        {
                //            DataRow drBatch = dtBatchList.Rows[index];
                //            if (dgMainGrid.CurrentRow.Cells["Col_StockID"].Value != null && dgMainGrid.CurrentRow.Cells["Col_StockID"].Value.ToString() == drBatch["StockID"].ToString())
                //            {
                //                int currentStock = Convert.ToInt32(dgMainGrid.CurrentRow.Cells["Col_Quantity"].Value);
                //                _closingStock = 0;
                //                if (drBatch["ClosingStock"] != DBNull.Value)
                //                    _closingStock = Convert.ToInt32(drBatch["ClosingStock"].ToString());
                //                drBatch["ClosingStock"] = _closingStock + currentStock;
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void RecalculateBatchQuantity(DataTable dtBatchList)
        {
            //try
            //{
            //    TempStock tStock = new TempStock();
            //    DataTable dtStock = tStock.GetAllTempStockRows();
            //    if (dtStock != null && dtStock.Rows.Count > 0)
            //    {
            //        for (int index = 0; index < dtStock.Rows.Count; index++)
            //        {
            //            DataRow drStock = dtStock.Rows[index];
            //            CorrectStockQuantity(drStock, dtBatchList);
            //        }
            //    }
            //}
            //catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void CorrectStockQuantity(DataRow drStock, DataTable dtBatch)
        {
            try
            {
                for (int index = 0; index < dtBatch.Rows.Count; index++)
                {
                    DataRow drBatch = dtBatch.Rows[index];
                    if (drStock["StockID"].ToString() == drBatch["StockID"].ToString())
                    {
                        int soldStock = Convert.ToInt32(drStock["SoldQuantity"].ToString());
                        drBatch["ClosingStock"] = Convert.ToInt32(drBatch["ClosingStock"].ToString()) - soldStock;
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_OnCellTextChanged(string cellValue, int columnIndex)
        {
            try
            {
                if (DataSourceProductList != null)
                {
                    if (cellValue == "")
                    {
                        pnlProductListGrid.Visible = false;                       
                    }
                    else if (dgMainGrid.IsFirstColumn())
                    {
                        DoProductListFilter(cellValue);
                        pnlProductListGrid.Location = GetProductGridLocation();
                        pnlProductListGrid.Visible = true;
                        pnlProductListGrid.BringToFront();
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }
        

        private Point GetProductGridLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = dgMainGrid.Location.X + dgMainGrid.RowHeadersWidth + dgMainGrid.Columns[1].Width;
                pt.Y = dgMainGrid.Location.Y + dgMainGrid.ColumnHeadersHeight;

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        private Point GetCurrentRowLocationforBatchGrid()
        {
            Point pt = new Point();
            try
            {
                pt.X = dgMainGrid.Location.X + dgMainGrid.RowHeadersWidth + GetColumnWidthForBatch();
                pt.Y = dgMainGrid.Location.Y + dgMainGrid.ColumnHeadersHeight;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        private int GetColumnWidthForBatch()
        {
            int retVaue = 0;
            try
            {
                for (int index = 0; index < dgMainGrid.Columns.Count; index++)
                {
                    if (dgMainGrid.Columns[index].Name.ToLower() != BatchGridShowColumnName.ToLower())
                    {
                        if (dgMainGrid.Columns[index].Visible)
                            retVaue += dgMainGrid.Columns[index].Width;
                    }
                    else
                    {
                        break;
                    }
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return retVaue;
        }

        public void AddRowsInStockTempTable()
        {
            TempStock tStock;
            try
            {
                if (dgMainGrid.CurrentRow.Index >= 0 && OperationMode != OperationMode.None && ModuleNumber != ModuleNumber.None)
                {
                    for (int index = 0; index < dgMainGrid.Rows.Count; index++)
                    {
                        DataGridViewRow dgRow = dgMainGrid.Rows[index];
                        tStock = new TempStock();
                        if (dgRow.Cells["Col_StockID"].Value != null && dgRow.Cells["Col_StockID"].Value.ToString() != string.Empty && dgRow.Cells[0].Value != null && dgRow.Cells[0].Value.ToString() != string.Empty)
                        {
                            tStock.StockId = dgRow.Cells["Col_StockID"].Value.ToString();
                            tStock.ProductID = Convert.ToInt32(dgRow.Cells[0].Value.ToString());
                            if (dgRow.Cells["Old_Quantity"].Value != null && dgRow.Cells["Old_Quantity"].Value.ToString() != string.Empty)
                            {
                                tStock.SoldQuantity = Convert.ToInt32(dgRow.Cells["Col_Quantity"].Value.ToString()) - Convert.ToInt32(dgRow.Cells["Old_Quantity"].Value.ToString());
                            }
                            else
                            {
                                tStock.SoldQuantity = Convert.ToInt32(dgRow.Cells["Col_Quantity"].Value.ToString());
                            }
                            if (htDeletedStock.Contains(tStock.StockId))
                            {
                                tStock.SoldQuantity = tStock.SoldQuantity - Convert.ToInt32(htDeletedStock[tStock.StockId]);
                            }

                            if (dgMainGrid.Columns.Contains("Col_CustId"))
                            {
                                if (dgRow.Cells["Col_CustID"].Value != null && dgRow.Cells["Col_CustID"].Value.ToString() != string.Empty)
                                    tStock.CustomerNumber = Convert.ToInt32(dgRow.Cells["Col_CustID"].Value.ToString());
                            }

                            tStock.ModuleNumber = ModuleNumber;
                            tStock.CompName = ComputerName;
                            tStock.Mode = OperationMode;                          
                        }
                    }                    
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private string removeAsteric(string str)
        {
            
            string retVal =str;
            if(str.Contains('*'))
            {
                
               int index=str.IndexOf('*');

               retVal = str.Insert(index , "\\"); 
            }
            

            return retVal;
        
        }
        public void DoProductListFilter(string filterValue)
        {
            try
            {
                ProductListFilter = GetFilterString(filterValue);
                ProductListFilter = removeAsteric(ProductListFilter);
                _BindingSourceProductList.Filter = ProductListFilter;
                if (dgProductListGrid.Rows.Count > 0)
                {
                    dgProductListGrid.Rows[0].Selected = true;
                    dgProductListGrid.CurrentCell = dgProductListGrid.Rows[0].Cells[1];
                }
                dgProductListGrid.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        private void DoProductListFilterForProductID(int ProductID)
        {
            try
            {
                ProductListFilter = GetFilterStringForProductID(ProductID);
                _BindingSourceProductList.Filter = ProductListFilter;
                if (dgProductListGrid.Rows.Count > 0)
                {
                    dgProductListGrid.Rows[0].Selected = true;
                    dgProductListGrid.CurrentCell = dgProductListGrid.Rows[0].Cells[1];
                }
                dgProductListGrid.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        private string GetFilterStringForProductID(int ProductID)
        {
            string strFilterString = "";
            string strFilterColumn = "";
            try
            {

                strFilterColumn = dgMainGrid.Columns[0].DataPropertyName;
                if (strFilterString != "")
                    strFilterString += " AND ";
                if (DataSourceProductList.Columns[dgMainGrid.Columns[0].DataPropertyName].DataType == typeof(int))
                {
                    strFilterString += strFilterColumn + " = ";
                    strFilterString += ProductID + " ";
                }
                else if (DataSourceProductList.Columns[dgMainGrid.Columns[0].DataPropertyName].DataType == typeof(string))
                {
                    strFilterString += strFilterColumn;
                    strFilterString += " like '" + ProductID + "%' ";
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return strFilterString;
        }

        private string GetFilterString(string filterValue)
        {
            string strFilterString = "";
            string strFilterColumn = "";
            try
            {

                strFilterColumn = dgMainGrid.Columns[1].DataPropertyName;
                if (strFilterString != "")
                    strFilterString += " AND ";
                if (DataSourceProductList.Columns[dgMainGrid.Columns[1].DataPropertyName].DataType == typeof(int))
                {
                    strFilterString += strFilterColumn + " = ";
                    strFilterString += filterValue + " ";
                }
                else if (DataSourceProductList.Columns[dgMainGrid.Columns[1].DataPropertyName].DataType == typeof(string))
                {
                    strFilterString += strFilterColumn;
                    strFilterString += " like '" + filterValue + "%' ";
                }
                if (strFilterColumn.Equals("ProdName") == true && DataSourceProductList.Columns.Contains("Barcode") == true)
                {
                    strFilterString += " OR Barcode";
                    strFilterString += " like '%" + filterValue + "%' ";
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return strFilterString;
        }

        private void dgProductListGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _DataGridViewSelectedRow = dgProductListGrid.Rows[e.RowIndex];
                FillProductContents();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgProductListGrid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FillProductDetails();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public void SetFocus(int columnIndex)
        {
            try
            {
                //ss 20/3/2013
                if (dgMainGrid.CurrentRow != null)
                    dgMainGrid.SetCurrentCell(dgMainGrid.CurrentRow.Index, columnIndex);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public void SetFocus(int rowIndex, int columnIndex)
        {
            try
            {
                dgMainGrid.SetCurrentCell(rowIndex, columnIndex);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public void Sort(DataGridViewColumn columnToSort, ListSortDirection direction)
        {
            dgMainGrid.Sort(columnToSort, direction);
        }

        public void ClearSelection()
        {
            dgMainGrid.EndEdit();
            dgMainGrid.ClearSelection();
        }

        public void RefreshMe()
        {
            try
            {
                dgMainGrid.Refresh();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public DataGridViewRow MainDataGridCurrentRow
        {

            get { return dgMainGrid.Rows[dgMainGrid.CurrentRow.Index]; }
        }

        private bool FillProductDetails()
        {
            bool retValue = true;
            try
            {
                bool IsClosingStockZero = true;
                int closingStockValue = 0;
                int mqty = 0;
                // ss 12/6/2013
                int ProductID = 0;
                // ss 12/6/2013
                // Check for closing stock
                if (!string.IsNullOrEmpty(ProductGridClosingStockColumnName))
                {
                    if (_DataGridViewSelectedRow.Cells[ProductGridClosingStockColumnName].Value != null && _DataGridViewSelectedRow.Cells[ProductGridClosingStockColumnName].Value.ToString() != "")
                    {
                        closingStockValue = Convert.ToInt32(_DataGridViewSelectedRow.Cells[ProductGridClosingStockColumnName].Value.ToString());
                        ProductID = Convert.ToInt32(_DataGridViewSelectedRow.Cells[0].Value.ToString());

                        //ss 2/10/2013
                        {
                            if (!string.IsNullOrEmpty(MainGridSoldQuantityColumnName))

                                if (dgMainGrid.CurrentRow.Cells[MainGridSoldQuantityColumnName].Value != null && dgMainGrid.CurrentRow.Cells[MainGridSoldQuantityColumnName].Value.ToString() != "")
                                    mqty = Convert.ToInt32(dgMainGrid.CurrentRow.Cells[MainGridSoldQuantityColumnName].Value.ToString());
                        }
                        //ss 2/10/2013

                        if (closingStockValue + mqty > 0)
                        {
                            IsClosingStockZero = false;
                        }
                    }
                    // ss 12/3/2015
                    if (IsClosingStockZero && General.CurrentSetting.MsetSaleAllowNegativeStock != "Y")
                    {
                        ProductID = Convert.ToInt32(_DataGridViewSelectedRow.Cells[0].Value.ToString());
                        pnlProductListGrid.Visible = false;
                        dgMainGrid.CurrentCell.Value = "";
                        SetFocus(1);
                        dgMainGrid.Refresh();
                        if (OnSelectedProductClosingStock != null)
                        {
                            OnSelectedProductClosingStock(closingStockValue, ProductID);
                        }
                        retValue = false;
                        return retValue;
                    }
                }

                if (DataSourceProductList != null)
                {
                    if (OnProductSelected != null)
                        OnProductSelected(_DataGridViewSelectedRow);
                }
                try
                {
                    pnlProductListGrid.Visible = false;                   
                    FillGridBatchList(Convert.ToInt32(_DataGridViewSelectedRow.Cells[0].Value.ToString()));
                    if (dgBatchListGrid.Rows.Count > 0)
                    {
                        //if (dgBatchListGrid.Rows.Count == 1)
                        //{
                        //    dgBatchListGrid.Rows[0].Selected = true;
                        //    dgBatchListGrid.CurrentCell = dgBatchListGrid.Rows[0].Cells[1];  
                        //}
                        //else
                        //{
                            pnlBatchGrid.Location = GetCurrentRowLocationforBatchGrid();
                            dgBatchListGrid.Location = GetCurrentRowLocationforBatchGrid();
                            pnlBatchGrid.Visible = true;
                            pnlBatchGrid.BringToFront();
                            dgBatchListGrid.Visible = true;
                        //}
                    }
                   // else
                    else if (General.CurrentSetting.MsetSaleAllowNegativeStock == "Y")
                    {
                        btnNewClick();
                    }

                    if (dgBatchListGrid.Rows.Count > 0) //1
                    {
                        if (MainDataGridCurrentRow.Cells[_BatchColumnName].Value != null &&
                                MainDataGridCurrentRow.Cells[_BatchColumnName].Value.ToString() != "")
                        {
                            foreach (DataGridViewRow row in dgBatchListGrid.Rows)
                            {
                                if (MainDataGridCurrentRow.Cells[_BatchGridIDColumnName].Value != null && (row.Cells[_BatchGridIDColumnName].Value.ToString() == MainDataGridCurrentRow.Cells[_BatchGridIDColumnName].Value.ToString())) // ss check for null
                                {
                                    row.Selected = true;

                                    dgMainGrid.SetCurrentCellForcefully(dgMainGrid.CurrentRow.Index, dgMainGrid.Columns[_BatchColumnName].Index);

                                    break;
                                }
                            }
                        }
                        else if (MainDataGridCurrentRow.Cells[_ProductGridLastStockIDColumnName].Value != null &&
                                MainDataGridCurrentRow.Cells[_ProductGridLastStockIDColumnName].Value.ToString() != "")
                        {
                            foreach (DataGridViewRow row in dgBatchListGrid.Rows)
                            {
                                if (row.Cells[_BatchGridIDColumnName].Value.ToString() == MainDataGridCurrentRow.Cells[_ProductGridLastStockIDColumnName].Value.ToString())
                                {
                                    row.Selected = true;
                                    dgBatchListGrid.CurrentCell = row.Cells[1];
                                    dgMainGrid.SetCurrentCellForcefully(dgMainGrid.CurrentRow.Index, dgMainGrid.Columns[_BatchColumnName].Index);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            dgBatchListGrid.Rows[0].Selected = true;
                            dgBatchListGrid.CurrentCell = dgBatchListGrid.Rows[0].Cells[1];
                        }
                        pnlBatchGrid.Visible = true;
                        pnlBatchGrid.BringToFront();
                        pnlBatchGrid.Focus();
                        dgBatchListGrid.Focus();
                    }

                    else if (General.CurrentSetting.MsetSaleAllowNegativeStock == "N" ) //&& dgBatchListGrid.Rows.Count > 1)
                    {
                        pnlBatchGrid.Visible = _AllowNewBatch;
                        if (_AllowNewBatch)
                        {
                            dgMainGrid.SetCurrentCellForcefully(dgMainGrid.CurrentRow.Index, dgMainGrid.Columns[_BatchColumnName].Index);
                            pnlBatchGrid.Focus();
                            btnNew.Visible = true;
                            btnNew.Focus();
                        }
                    }
                }

                catch (Exception ex) { Log.WriteError(ex.ToString()); }
           
                
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return retValue;
        }

        private void FillBatchDetails()
        {
            try
            {
                if (DataSourceBatchList != null)
                {
                    if (OnBatchSelected != null)
                        OnBatchSelected(dgBatchListGrid.SelectedRows[0]);
                }
                pnlBatchGrid.Visible = false;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public DataTable GetGridData()
        {
            DataTable dt = new DataTable(this.Name);
            try
            {
                foreach (DataGridViewColumn col in dgMainGrid.Columns)
                {
                    dt.Columns.Add(col.DataPropertyName);
                }

                foreach (DataGridViewRow row in dgMainGrid.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value == null)
                            dRow[cell.ColumnIndex] = "";
                        else
                            dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt.Rows.Add(dRow);
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return dt;
        }

        private bool dgMainGrid_EnterKeyPressed(object sender, EventArgs e)
        {
            bool retValue = true;
            try
            {
                if (dgMainGrid.IsFirstColumn())
                {
                    if (dgMainGrid.CurrentRow.Cells[0].Value != null && dgMainGrid.CurrentRow.Cells[0].Value.ToString() != "")
                    {
                        DoProductListFilter(dgMainGrid.CurrentCell.Value.ToString());
                    }
                    if (dgProductListGrid.Rows.Count > 0)
                        retValue = FillProductDetails();
                    else
                    {
                        pnlProductListGrid.Visible = false;
                        dgMainGrid.CurrentCell.Value = "";
                        SetFocus(dgMainGrid.CurrentCell.ColumnIndex);
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return retValue;
        }

        private void dgMainGrid_OnEnterKeyPressed_Processed(object sender, EventArgs e)
        {
            try
            {
                if (dgBatchListGrid.Visible)
                {
                    pnlBatchGrid.Focus();
                    dgBatchListGrid.Focus();
                }
                else if (dgProductListGrid.Visible)
                {
                    pnlProductListGrid.Focus();
                    dgProductListGrid.Focus();
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_OnCellValueChangeCommited(int colIndex)
        {
            try
            {
                if (OnCellValueChangeCommited != null)
                {
                    OnCellValueChangeCommited(colIndex);
                }
                // Bar code
                if (dgMainGrid.CurrentRow.Cells[0].Value == null)
                {
                    FillBarCodeProduct();
                }
                else if (dgMainGrid.CurrentRow.Cells[0].Value.ToString() == "")
                {
                    FillBarCodeProduct();
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void FillBarCodeProduct()
        {
            try
            {
                if (dgMainGrid.IsFirstColumn())
                {
                    DataRow drProduct = null;
                    if (OnProductBarCodeScaned != null)
                        drProduct = OnProductBarCodeScaned(dgMainGrid.CurrentCell.Value.ToString());
                    if (drProduct != null)
                    {
                        DoProductListFilter(drProduct[0].ToString());
                        if (dgProductListGrid.Rows.Count > 0)
                            FillProductDetails();

                        foreach (DataGridViewRow row in dgBatchListGrid.Rows)
                        {
                            if (row.Cells[_BatchGridIDColumnName].Value.ToString() == drProduct[1].ToString())
                            {
                                row.Selected = true;
                                break;
                            }
                        }
                        FillBatchDetails();

                        MainDataGridCurrentRow.Cells["Col_Quantity"].Value = drProduct[2].ToString();

                        if (OnCellValueChangeCommited != null)
                        {
                            OnCellValueChangeCommited(MainDataGridCurrentRow.Cells["Col_Quantity"].ColumnIndex);
                        }
                    }
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_OnArrowUpDownPressed(int keyValue)
        {
            try
            {
                if (pnlProductListGrid.Visible && dgProductListGrid.Rows.Count > 0)
                {
                    dgProductListGrid.Focus();
                    if (dgProductListGrid.Rows.Count > 1)
                    {
                        dgProductListGrid.Rows[0].Selected = false;
                        dgProductListGrid.Rows[1].Selected = true;
                        dgProductListGrid.CurrentCell = dgProductListGrid.Rows[1].Cells[1];
                    }
                }
                if (dgBatchListGrid.Visible && dgBatchListGrid.Rows.Count > 0)
                {
                    dgBatchListGrid.Focus();
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgProductListGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgMainGrid.IsFirstColumn())
                        FillProductDetails();
                    pnlProductListGrid.Visible = false;
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    pnlProductListGrid.Visible = false;
                    dgMainGrid.Focus();
                    dgMainGrid.CurrentCell.Value = "";
                    dgMainGrid.EndEdit();
                    dgMainGrid.BeginEdit(false);
                    e.Handled = true;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgBatchListGrid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FillBatchDetails();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgBatchListGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FillBatchDetails();
                    pnlBatchGrid.Visible = false;
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    pnlBatchGrid.Visible = false;
                    dgMainGrid.Focus();
                    dgMainGrid.EndEdit();
                    dgMainGrid.BeginEdit(false);
                    e.Handled = true;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnNewClick();
        }

        private void btnNewClick()
        {
            try
            {
                pnlBatchGrid.Visible = false;
                if (OnNewBatchClicked != null)
                    OnNewBatchClicked();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private bool dgMainGrid_OnCanRowDeleted(object sender, EventArgs e)
        {
            bool retValue = true;
            try
            {
                if (OnCanRowDeleted != null)
                {
                    retValue = OnCanRowDeleted(sender, e);
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return retValue;           
        }

        private void dgMainGrid_OnRowDeleted(object sender, EventArgs e)
        {
            try
            {
                if (pnlProductListGrid.Visible)
                {
                    pnlProductListGrid.Visible = false;                    
                }
                if (pnlBatchGrid.Visible)
                {
                    pnlBatchGrid.Visible = false;
                }
                if (OnRowDeleted != null)
                    OnRowDeleted(sender, e);               
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }      

        private void dgMainGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (OnCellEnter != null)
                    OnCellEnter(sender, e);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (OnCellLeave != null)
                {
                    dgMainGrid.EndEdit();
                    OnCellLeave(sender, e);
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_OnTABKeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (OnTABKeyPressed != null)
                    OnTABKeyPressed(sender, e);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_OnShiftTABKeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (OnShiftTABKeyPressed != null)
                    OnShiftTABKeyPressed(sender, e);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (pnlProductListGrid.Visible)
                {
                    pnlProductListGrid.Visible = false;
                    dgMainGrid.Focus();
                    dgMainGrid.EndEdit();
                    if (dgMainGrid.CurrentRow.Cells[0].Value == null)
                    {
                        dgMainGrid.CurrentCell.Value = "";
                    }
                    else if (dgMainGrid.CurrentRow.Cells[0].Value.ToString() == "")
                    {
                        dgMainGrid.CurrentCell.Value = "";
                    }
                    dgMainGrid.BeginEdit(false);
                }
                else
                {
                    if (OnEscapeKeyPressed != null)
                        OnEscapeKeyPressed(sender, e);
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgMainGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            CellDefaultColor = dgMainGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
            dgMainGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = General.ControlFocusColor;
        }

        private void dgMainGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgMainGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = CellDefaultColor;
        }

        private void dgMainGrid_OnRowAdded(object sender, EventArgs e)
        {
            try
            {
                if (OnRowAdded != null)
                {
                    OnRowAdded(sender, e);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void dgMainGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OnCellContentClick != null)
            {
                dgMainGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                OnCellContentClick(sender, e);
            }
        }
      

        internal void CommitEdit(DataGridViewDataErrorContexts dataGridViewDataErrorContexts)
        {

        }       

        private void FillProductContents()
        {
            try
            {

                if ( _ShowProductContent== true && _DataGridViewSelectedRow != null)
                {
                    if (dgProductListGrid.Columns.Contains("Col_GenericCategoryName"))
                    {
                        if (_DataGridViewSelectedRow.Cells["Col_GenericCategoryName"].Value != null)
                            lblGenericName.Text = _DataGridViewSelectedRow.Cells["Col_GenericCategoryName"].Value.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void dgMainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgMainGrid.Rows.Count > 1 && (dgMainGrid.CurrentRow.Cells[0].Value == null))
                {
                    dgMainGrid.ClearSelection();
                    dgMainGrid_OnTABKeyPressed(null, null);
                }
            }
        }
    }
}
