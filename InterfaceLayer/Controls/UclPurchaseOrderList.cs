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
using EcoMart.InterfaceLayer;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclPurchaseOrderList : BaseControl
    {
        #region Declaration     
        private PurchaseOrder _PurchaseOrder;
        private DailyPurchaseOrder _DailyPurchaseOrder;
        private DataTable _BindingSource;
        private List<DataGridViewRow> rowCollection;
        private List<DataGridViewRow> rowCollectionmain = null;
        #endregion

        #region Constructor
        public UclPurchaseOrderList()
        {
            InitializeComponent();
            _PurchaseOrder = new PurchaseOrder();
            _BindingSource = new DataTable();
            _DailyPurchaseOrder = new DailyPurchaseOrder();
            SearchControl = new UclPurchaseOrderSearch();
        }

        #endregion Constructor

        #region IDetail Control
        public override void SetFocus()
        {
            FromDateShortList.Focus();

        }
        public override bool ClearData()
        {
            try
            {
                _DailyPurchaseOrder.Initialise();
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
                lblFooterMessage.Text = "Press TAB To See PurchaseDetails and Press Enter to Select Party or Press Escape";
                headerLabel1.Text = "Purchase Order List";
              //  pnlDate.Enabled = true;
                pnlSummary.SendToBack();
                pnlSummary.Visible = false;
                mpMainSubViewControl1.ClearSelection();
                tsBtnSave.Enabled = true;
                tsBtnSavenPrint.Visible = false;
                tsBtnPrint.Visible = true;

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
            return retValue;

        }

        public override bool Delete()
        {
            return true;
        }

        public override bool Exit()
        {
            ClearData();
            this.Visible = false;
            tsBtnSavenPrint.Visible = true;
            return base.Exit();
        }
        public override bool ProcessDelete()
        {
            return true;
        }


        public override bool View()
        {
            return true;
        }

        public override bool Print()
        {
            bool retValue = true;
            PrintData();
            ClearData();
            return retValue;
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
                if (keyPressed == Keys.F && modifier == Keys.Alt)
                {
                    FromDateShortList.Select();
                    FromDateShortList.Focus();
                    retValue = true;
                }
               if (keyPressed == Keys.V && modifier == Keys.Alt)
                {
                    btnGoClick();
                    retValue = true;
                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                    ToDateShortList.Select();
                    ToDateShortList.Focus();
                    retValue = true;
                }
                //if (keyPressed == Keys.U && modifier == Keys.Alt)
                //{
                //    BtnUploadClick();
                //    retValue = true;
                //}
                //if (keyPressed == Keys.O && modifier == Keys.Alt)
                //{
                //    btnCreateOrder.Focus();
                //    retValue = true;
                //}
                //if (keyPressed == Keys.N && modifier == Keys.Alt)
                //{
                //    uclPurchaseNewProduct2.Focus();
                //    retValue = true;
                //}
                if (keyPressed == Keys.Escape)
                {
                    if (dgvBatchGrid.Visible == true)
                    {
                        dgvBatchGrid.Visible = false;
                        mpMainSubViewControl1.Focus();
                        retValue = true;
                    }
                    else
                        retValue = Exit();
                }
                if (retValue == false)
                {
                    retValue = base.HandleShortcutAction(keyPressed, modifier);
                }
                //{
                //    if (dgvBatchGrid.Visible == true)
                //    {
                //        dgvBatchGrid.Visible = false;
                //        mpMainSubViewControl1.Focus();
                //        retValue = true;
                //    }
                //    else if (pnlSummary.Visible==true)
                //    {
                //        pnlSummary.Visible = false;
                //        mpMainSubViewControl1.Focus();
                //        retValue = true;
                //    }
                //    else
                //        retValue = Exit();
                //}
                //if (retValue == false)
                //{
                //    retValue = base.HandleShortcutAction(keyPressed, modifier);
                //}
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override string GetShortcutKeys()
        {
            string lbl = base.GetShortcutKeys();
            lbl = "Alt+V View,Alt+O Create Orders";
            return lbl;
        }
        private void ClearControls()
        {
            try
            {
                lblFooterMessage.Text = "Press TAB To See PurchaseDetails and Press Enter to Select Party or Press Escape";
                dgvBatchGrid.Visible = false;
                FromDateShortList.ResetText();
                ToDateShortList.ResetText();
                FromDateSaleToday.ResetText();
                ToDateSaleToday.ResetText();
                mpMainSubViewControl1.Dock = DockStyle.Fill;
                ConstructMainColumns();
                mpMainSubViewControl1.NumericColumnNames.Add("Col_ProdClosingStock");
                DataTable dtable = new DataTable();
                DataTable dt = new DataTable();
                mpMainSubViewControl1.DataSourceMain = dtable;
                mpMainSubViewControl1.DataSource = dt;
                mpMainSubViewControl1.Bind();
                tsBtnSavenPrint.Visible = true;
                pnlSummary.Visible = false;
                txtAmount.Text = "";
                txtNoofOrders.Text = "";
                datePickerBillDate.Value = DateTime.Now;
                FromdateNextVisit.Value = DateTime.Now;
                ToDateNextVisit.Value = DateTime.Now;
                cbSaleToday.Checked = false;
                cbShortList.Checked = false;
                chkNextVisit.Checked = false;
                rbtLastOrderAllProducts.Checked = true;
                txtSaleDays.Text = "";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        #region contruct grid

        private void ConstructMainColumns()
        {
            mpMainSubViewControl1.ColumnsMain.Clear();
            DataGridViewTextBoxColumn column;
            DataGridViewComboBoxColumn combocolumn;
            DataGridViewCheckBoxColumn checkcolumn;

            //column = new DataGridViewTextBoxColumn();
            //column.Name = "Col_SrNo";
            //column.DataPropertyName = "SrNo";
            //column.HeaderText = "Sr No";
            //column.Visible = false;
            //column.Width = 40;
            //column.ReadOnly = true;
            //mpMainSubViewControl1.ColumnsMain.Add(column);

           

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ACCID";
            column.DataPropertyName = "AccountID";
            column.HeaderText = "ACCID";
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName";
            column.DataPropertyName = "AccName";
            column.HeaderText = "Account Name";
            column.Width = 220;
            //column.ReadOnly = true;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProductName";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "Product Name";
            column.Width = 220;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.ReadOnly = true;
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            mpMainSubViewControl1.ColumnsMain.Add(column);
                      
                     

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ShortListID";
            //    column.DataPropertyName = "DSLID";
            column.HeaderText = "DSLID";
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdID";
            column.DataPropertyName = "ProductID";
            column.HeaderText = "ID";
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UOM";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 70;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Pack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 70;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdCompShortName";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 70;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_BoxQty";
            column.DataPropertyName = "ProdBoxQuantity";
            column.HeaderText = "Box Qty";
            column.Width = 70;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingStock";
            column.DataPropertyName = "ProdClosingStock";
            column.HeaderText = "Cl. Stock";
            column.Width = 80;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.ReadOnly = true;
            column.Visible = false;
            column.ValueType = typeof(int);
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_SaleStock";
            column.DataPropertyName = "Quantity";
            column.HeaderText = "Sale Qty";
            column.Width = 80;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID1";
            //  column.DataPropertyName = "AccountID1";
            column.HeaderText = "ID1";
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName1";
            //   column.DataPropertyName = "AccName1";
            column.HeaderText = "AccountName1";
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID2";
            //   column.DataPropertyName = "AccountID2";
            column.HeaderText = "ID2";
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            DataGridViewCheckBoxColumn columnCheck1 = new DataGridViewCheckBoxColumn();
            columnCheck1.Name = "Col_Check1";
            columnCheck1.HeaderText = "Check1";
            columnCheck1.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(columnCheck1);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName2";
            //   column.DataPropertyName = "AccName2";
            column.HeaderText = "AccountName2";
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_IfSave";
            // column.DataPropertyName = "IfSave";
            column.HeaderText = "IfSave";
            column.Width = 20;
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_PurchaseRate";
            column.DataPropertyName = "ProdLastPurchaseRate";
            column.Width = 80;
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OrderNumber";
            column.DataPropertyName = "OrderNumber";
            column.HeaderText = "OrderNo";
            column.Width = 20;
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Quantity1";
            //   column.DataPropertyName = "OrderQuantity1";
            column.HeaderText = "Orderqty";
            column.Width = 20;
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccAddress1";
            column.Visible = false;
            column.DataPropertyName = "AccAddress1";
            mpMainSubViewControl1.ColumnsMain.Add(column);
            column = new DataGridViewTextBoxColumn();

            column.Name = "Col_AccAddress2";
            column.Visible = false;
            column.DataPropertyName = "AccAddress2";
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Quantity";
            column.DataPropertyName = "OrderQuantity";
            column.HeaderText = "Order Qty";
            column.Width = 80;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_SchemeQuantity";
            column.DataPropertyName = "SchemeQuantity";
            column.HeaderText = "Scm Qty";
            column.Width = 70;
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);
                     
            combocolumn = new DataGridViewComboBoxColumn();
            combocolumn.Name = "Col_NetRate";
            combocolumn.DataPropertyName = "NetRate";
            combocolumn.HeaderText = "Net Rate";
            combocolumn.Width = 70;
            combocolumn.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(combocolumn);

            checkcolumn = new DataGridViewCheckBoxColumn();
            checkcolumn.Name = "Col_CheckBox";
            checkcolumn.DataPropertyName = "Check";
            checkcolumn.HeaderText = "Check";
            checkcolumn.Width = 70;
            checkcolumn.TrueValue = "true";
            mpMainSubViewControl1.ColumnsMain.Add(checkcolumn);

            ((DataGridViewComboBoxColumn)mpMainSubViewControl1.dgMainGrid.Columns["Col_NetRate"]).Items.Add("Y");
            ((DataGridViewComboBoxColumn)mpMainSubViewControl1.dgMainGrid.Columns["Col_NetRate"]).Items.Add("N");
            //((DataGridViewCheckBoxColumn)mpMainSubViewControl1.dgMainGrid.Columns["Col_CheckBox"]).TrueValue = "True";
           
        }

        private void ConstructSubColumns()
        {

            mpMainSubViewControl1.ColumnsSub.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "AccountID";
            column.HeaderText = "ID";
            column.Visible = false;
            mpMainSubViewControl1.ColumnsSub.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName";
            column.DataPropertyName = "AccName";
            column.HeaderText = "AccountName";
            column.Width = 400;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsSub.Add(column);

        }

        private void ConstructOrderSummary()
        {
            mdgOrderSummary.Columns.Clear();
            DataGridViewTextBoxColumn column;

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OrderNumber";
            column.DataPropertyName = "OrderNumber";
            column.HeaderText = "OrderNumber";
            column.Width = 100;
            mdgOrderSummary.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "AccountID";
            column.HeaderText = "ID";
            column.Visible = false;
            mdgOrderSummary.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName";
            column.DataPropertyName = "AccName";
            column.HeaderText = "CreditorName";
            column.Width = 390;
            mdgOrderSummary.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.DataPropertyName = "Amount";
            column.HeaderText = "Amount";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 100;
            mdgOrderSummary.Columns.Add(column);
        }

        private void ConstructOrderDetails()
        {
            mdgOrderDetail.Columns.Clear();
            DataGridViewTextBoxColumn column;

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "ProductID";
            column.HeaderText = "ID";
            column.Visible = false;
            mdgOrderDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdName";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "Product";
          //  column.Width = 200;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            mdgOrderDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_LoosePack";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            //column.Width = 70;
            column.ReadOnly = true;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mdgOrderDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Pack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            //column.Width = 70;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mdgOrderDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_CompShortName";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            //column.Width = 70;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mdgOrderDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OrderQuantity";
            column.DataPropertyName = "OrderQuantity";
            column.HeaderText = "Quantity";
            //column.Width = 100;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mdgOrderDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OrderNumber";
            column.DataPropertyName = "OrderNumber";
            column.HeaderText = "OrderNumber";
            //column.Width = 120;
            column.Visible = false;
            mdgOrderSummary.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.DataPropertyName = "Amount";
            column.HeaderText = "Amount";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //column.Width = 100;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            mdgOrderDetail.Columns.Add(column);
        }


        #endregion

        # region Other Methods

        private void InitializeMainSubViewControl()
        {
            try
            {
                //ConstructMainColumns();
                //mpMainSubViewControl1.NumericColumnNames.Add("Col_ProdClosingStock");
                ConstructSubColumns();
                DataTable dtable = new DataTable();
                mpMainSubViewControl1.NextRowColumn = 0;
                mpMainSubViewControl1.DataSourceMain = dtable;
                mpMainSubViewControl1.NumericColumnNames.Add("Col_Quantity");
                mpMainSubViewControl1.DoubleColumnNames.Add("Col_Amount");
                Account acc = new Account();
                DataTable dt = acc.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
                mpMainSubViewControl1.DataSource = dt;
                //mpMainSubViewControl1.Bind();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void daydiffrence()
        {
            DateTime start= FromDateSaleToday.Value;
            DateTime end= ToDateSaleToday.Value;

            TimeSpan  result = start -end;

            txtSaleDays.Text  = result.ToString();
            return;
        }
        private void SelectFirstParty()
        {
            try
            {
                foreach (DataGridViewRow dsr in mpMainSubViewControl1.Rows)
                {
                    if (dsr.Cells["Col_IfSave"].Value.ToString().Trim() != "Y")
                    {
                        dsr.Cells["Col_ACCID"].Value = dsr.Cells["Col_ID1"].Value;
                        dsr.Cells["Col_AccName"].Value = dsr.Cells["Col_AccName1"].Value;
                        if (dsr.Cells["Col_Quantity"].Value != null)
                        {
                            if (Convert.ToInt32(dsr.Cells["Col_Quantity"].Value.ToString()) == 0 && dsr.Cells["Col_Quantity1"].Value != null)
                                dsr.Cells["Col_Quantity"].Value = dsr.Cells["Col_Quantity1"].Value;
                        }
                    }
                }
                mpMainSubViewControl1.Refresh();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }

        }

        //private bool saveorders()
        //{
        //    {
        //        bool returnVal = false;
        //        string mdslid = null;
        //        string maccid = null;
        //        try
        //        {
        //            foreach (DataGridViewRow dsr in mpMainSubViewControl1.Rows)
        //            {
        //                int mqty = 0;
        //                int.TryParse(dsr.Cells["Col_Quantity"].Value.ToString(), out mqty);
        //                if (dsr.Cells["Col_ACCID"].Value != null && mqty > 0)
        //                {
        //                    //mdslid = dsr.Cells["Col_ShortListID"].Value.ToString();
        //                    //_DailyPurchaseOrder.DSLID = mdslid;
        //                    mdslid = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //                    maccid = dsr.Cells["Col_ACCID"].Value.ToString();
        //                    _DailyPurchaseOrder.DSLAccountID = maccid;
        //                    _DailyPurchaseOrder.DSLQty = mqty;
        //                    _DailyPurchaseOrder.DSLIFSave = "Y";
        //                    returnVal = _DailyPurchaseOrder.SaveOrder();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.WriteError(ex.ToString());
        //            returnVal = false;
        //        }
        //        return returnVal;
        //    }
        //}

        private void PrintData()
        {
            PrintRow row;
            try
            {
                FromDateSaleToday.Focus();
                PrintFactory.SendReverseLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.ReverseLineFeed);
                PrintBill.Rows.Clear();
                Font fnt = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = 0;
                PrintPageNumber = 0;
                int rowcount = 0;
                double totpages = 0;
                int totalpages = 0;
                int preordernumber = 0;
                PrintRowPixel = 0;

                List<DataGridViewRow> rowCollection = new List<DataGridViewRow>();
                foreach (DataGridViewRow prodrow in mpMainSubViewControl1.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null && Convert.ToBoolean(prodrow.Cells["Col_CheckBox"].Value) == true)
                        rowCollection.Add(prodrow);
                }
                totalrows = 0;
                PrintPageNumber = 0;
                rowcount = 0;
                totpages = 0;
                totalpages = 0;
                preordernumber = 0;
                PrintRowPixel = 0;
                totalrows = rowCollection.Count();
                totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                totalpages = Convert.ToInt32(totpages);
                preordernumber = _DailyPurchaseOrder.DSLFirstOrderNumber;
                PrintHeader(totalpages, rowcount, fnt);
                foreach (DataGridViewRow prodrow in rowCollection)
                {
                    if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                    {
                        PrintRowPixel = 325;
                        row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
                        PrintBill.Rows.Add(row);
                        PrintBill.Print_Bill(600, 400);
                        PrintBill.Rows.Clear();
                        PrintRowPixel = 0;
                        PrintHeader(totalpages, rowcount, fnt);
                        rowcount = 0;
                    }
                    PrintRowPixel += 17;
                    rowcount += 1;

                    row = new PrintRow(prodrow.Cells["Col_AccName"].Value.ToString(), PrintRowPixel, 10, fnt);
                    PrintBill.Rows.Add(row);

                    row = new PrintRow(prodrow.Cells["Col_ProductName"].Value.ToString(), PrintRowPixel, 180, fnt);
                    PrintBill.Rows.Add(row);

                    row = new PrintRow(prodrow.Cells["Col_UOM"].Value.ToString(), PrintRowPixel, 320, fnt);
                    PrintBill.Rows.Add(row);

                    row = new PrintRow(prodrow.Cells["Col_Pack"].Value.ToString(), PrintRowPixel, 350, fnt);
                    PrintBill.Rows.Add(row);

                    row = new PrintRow(prodrow.Cells["Col_ProdCompShortName"].Value.ToString(), PrintRowPixel, 390, fnt);
                    PrintBill.Rows.Add(row);

                    row = new PrintRow(prodrow.Cells["Col_BoxQty"].Value.ToString(), PrintRowPixel, 430, fnt);
                    PrintBill.Rows.Add(row);

                    row = new PrintRow(prodrow.Cells["Col_SaleStock"].Value.ToString(), PrintRowPixel, 460, fnt);
                    PrintBill.Rows.Add(row);

                    int myqty = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                    row = new PrintRow(myqty.ToString("#0"), PrintRowPixel, 490, fnt);
                    PrintBill.Rows.Add(row);
                }
                PrintFooter(fnt);
                _DailyPurchaseOrder.DSLFirstOrderNumber += 1;
                PrintBill.Print_Bill(600, 400);
                rowCollection = new List<DataGridViewRow>();
                PrintFactory.SendLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.LineFeed);
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

                Font mfnt = new Font("Arial", 8, FontStyle.Bold);
                row = new PrintRow(General.ShopDetail.ShopName.ToUpper(), PrintRowPixel, 10, mfnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Purchase Order List", PrintRowPixel, 340, mfnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(General.ShopDetail.ShopAddress1.Trim(), PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Time :" + DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(General.ShopDetail.ShopAddress2.Trim(), PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                string voudate = General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd"));
                row = new PrintRow("Date :" + voudate, PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow("Page :" + page, PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 14;
                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 14;
                row = new PrintRow("Account                                      Product                          UOM  PACK    COMP  BQty SQty OQty", PrintRowPixel, 15, mfnt);
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
            return Rowcount;
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

        //private void SelectSecondParty()
        //{
        //    try
        //    {
        //        foreach (DataGridViewRow dsr in mpMainSubViewControl1.Rows)
        //        {
        //            if (rbtSecond.Checked == true && dsr.Cells["Col_IfSave"].Value.ToString().Trim() != "Y")
        //            {
        //                dsr.Cells["Col_ACCID"].Value = dsr.Cells["Col_ID2"].Value;
        //                dsr.Cells["Col_AccName"].Value = dsr.Cells["Col_AccName2"].Value;
        //                if (dsr.Cells["Col_Quantity"].Value != null)
        //                {
        //                    if (Convert.ToInt32(dsr.Cells["Col_Quantity"].Value.ToString()) == 0)
        //                        dsr.Cells["Col_Quantity"].Value = dsr.Cells["Col_Quantity1"].Value;
        //                }
        //            }
        //        }
        //        mpMainSubViewControl1.Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteError(ex.ToString());
        //    }

        //}
        #endregion Other Methods

        #region Events


        private void btnCreateOrder_KeyDown(object sender, KeyEventArgs e)
        {
            btnCreateOrderClick();
        }
        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            btnCreateOrderClick();
        }

        private void btnCreateOrderClick()
        {
            //bool retValue = false;
            //tsBtnSave.Enabled = false;
            //string preaccountid = "";
            //int mqty = 0;
            //int mmordno = 0;
            //int mscmqty = 0;
            try
            {

                //    mpMainSubViewControl1.Sort(mpMainSubViewControl1.ColumnsMain[2], ListSortDirection.Ascending);
                //    mpMainSubViewControl1.Refresh();
                //    rowCollectionmain = new List<DataGridViewRow>();
                //    foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                //    {
                //        preaccountid = dr.Cells["Col_ACCID"].Value.ToString().Trim();
                //        int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out mqty);
                //        int.TryParse(dr.Cells["Col_SchemeQuantity"].Value.ToString(), out mscmqty);

                //        if (preaccountid != "" && mqty > 0 && mmordno == 0)
                //        {
                //            rowCollectionmain.Add(dr);
                //        }

                //    }
                //    if (rowCollectionmain.Count > 0)
                //        retValue = CreateOrders();
                //    if (retValue == true)
                //    {

                //        ConstructOrderSummary();
                //        ConstructOrderDetails();
                //        FillSummaryGrid();
                //        //pnlDate.Enabled = false;
                //        pnlSummary.BringToFront();
                //        pnlSummary.Visible = true;
                //    }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        //private void btnCreateOrderClick()
        //{
        //    bool retValue = false;
        //    tsBtnSave.Enabled = false;
        //    string preaccountid = "";
        //    int mqty = 0;
        //    int mmordno = 0;
        //    try
        //    {
        //        mpMainSubViewControl1.Sort(mpMainSubViewControl1.ColumnsMain[1], ListSortDirection.Ascending);
        //        mpMainSubViewControl1.Refresh();
        //        rowCollectionmain = new List<DataGridViewRow>();
        //        foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
        //        {
        //            preaccountid = dr.Cells["Col_ACCID"].Value.ToString().Trim();
        //            int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out mqty);
        //            //int.TryParse(dr.Cells["Col_OrderNumber"].Value.ToString(), out mmordno);
        //            if (preaccountid != "" && mqty > 0 && mmordno == 0)
        //            {
        //                rowCollectionmain.Add(dr);
        //            }

        //        }
        //        if (rowCollectionmain.Count > 0)
        //            retValue = CreateOrders();
        //        if (retValue == true)
        //        {

        //            ConstructOrderSummary();
        //            ConstructOrderDetails();
        //            FillSummaryGrid();
        //            //pnlDate.Enabled = false;
        //            //rbtFirst.Enabled = false;
        //            //rbtSecond.Enabled = false;
        //            pnlSummary.BringToFront();
        //            pnlSummary.Visible = true;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }

        //}
        //private void btnCreateOrderClick()
        //{
        //    bool retValue = false;
        //    tsBtnSave.Enabled = false;
        //    string preaccountid = "";
        //    int mqty = 0;
        //    int mmordno = 0;
        //    try
        //    {

        //        mpMainSubViewControl1.Sort(mpMainSubViewControl1.ColumnsMain[1], ListSortDirection.Ascending);
        //        mpMainSubViewControl1.Refresh();

        //        //rowCollectionmain = new List<DataGridViewRow>();

        //        foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
        //        {
        //            preaccountid = dr.Cells["Col_ACCID"].Value.ToString().Trim();
        //            int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out mqty);

        //               //int.TryParse(dr.Cells["Col_OrderNumber"].Value.ToString(), out mmordno);

        //            if (preaccountid != "" && mqty > 0 && mmordno == 0)
        //            {
        //                //rowCollectionmain.Add(dr);
        //            }

        //        }

        //          // if (rowCollectionmain.Count > 0)
        //          //retValue = CreateOrders();


        //        if (retValue == true)
        //        {

        //            ConstructOrderSummary();
        //            ConstructOrderDetails();
        //            FillSummaryGrid();
        //           //pnlDate.Enabled = false;
        //            pnlSummary.BringToFront();
        //            pnlSummary.Visible = true;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }

        //}
        public void FixsizeOrdersummery()
        {
            mdgOrderDetail.Columns[0].Width =2;
            mdgOrderDetail.Columns[1].Width =200;
            mdgOrderDetail.Columns[2].Width =70;
            mdgOrderDetail.Columns[3].Width = 70;
            mdgOrderDetail.Columns[4].Width = 70;
            mdgOrderDetail.Columns[5].Width = 70;
            //mdgOrderDetail.Columns[7].Width = 100;
        }

        private void FillSummaryGrid()
        {
            FillSummaryData();
            mdgOrderSummary.DataSource = _BindingSource;
            mdgOrderSummary.Bind();
            //FixsizeOrdersummery();
            double totsummaryamt = 0;
            int mord = 0;
            double mamt = 0;
            foreach (DataGridViewRow dr in mdgOrderSummary.Rows)
            {
                if (dr.Cells["Col_Amount"].Value != null)
                    mamt = Convert.ToDouble(dr.Cells["Col_amount"].Value.ToString());
                totsummaryamt = totsummaryamt + mamt;
                mord += 1;
            }
            txtAmount.Text = totsummaryamt.ToString("0.00");
            txtNoofOrders.Text = mord.ToString("0");
        }

        private void FillSummaryData()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _DailyPurchaseOrder.GetSummaryData();
                _BindingSource = dtable;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillDetailGrid()
        {
            try
            {
                FillDetailData();
                mdgOrderDetail.DataSource = _BindingSource;
                mdgOrderDetail.Bind();
                //FixsizeOrdersummery();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillDetailData()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _DailyPurchaseOrder.GetDetailData(_DailyPurchaseOrder.CurrentOrderNumber);
                _BindingSource = dtable;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }




        private bool CreateOrders()
        {

            bool returnVal = false;
            _DailyPurchaseOrder.DSLFirstOrderNumber = 0;
            _DailyPurchaseOrder.DSLLastOrderNumber = 0;
            DBGetVouNumbers getno = new DBGetVouNumbers();
            try
            {
                for (int index = 0; index < rowCollectionmain.Count; index++)
                {
                    rowCollection = new List<DataGridViewRow>();

                    int mordno = 0;
                    int mmqty = 0;

                    string mmaccid = "";
                    string mmordid = "";
                    string preaccountid = "";
                    double mmpurrate = 0;
                    double mmamt = 0;
                    string mmprodID = "";
                    string netrate = "";


                    preaccountid = rowCollectionmain[index].Cells["Col_ACCID"].Value.ToString().Trim();
                    mordno = getno.GetPurchaseOrder(General.ShopDetail.ShopVoucherSeries);
                    if (_DailyPurchaseOrder.DSLFirstOrderNumber == 0)
                        _DailyPurchaseOrder.DSLFirstOrderNumber = mordno;
                    _DailyPurchaseOrder.DSLLastOrderNumber = mordno;
                    mmqty = 0;
                    mmpurrate = 0;
                    mmamt = 0;
                    mmaccid = "";
                    mmordid = "";
                    mmprodID = "";

                    _DailyPurchaseOrder.DSLMasterID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _DailyPurchaseOrder.DSLAmount = 0;


                    preaccountid = rowCollectionmain[index].Cells["Col_ACCID"].Value.ToString().Trim();
                    foreach (DataGridViewRow dr in rowCollectionmain)
                    {
                        mmaccid = dr.Cells["Col_ACCID"].Value.ToString().Trim();

                        if (preaccountid == mmaccid)
                        {
                            index++;
                            rowCollection.Add(dr);
                        }



                    }
                    foreach (DataGridViewRow ddsr in rowCollection)
                    {

                        int.TryParse(ddsr.Cells["Col_Quantity"].Value.ToString(), out mmqty);
                        mmaccid = ddsr.Cells["Col_ACCID"].Value.ToString();
                        mmprodID = ddsr.Cells["Col_ProdID"].Value.ToString();
                        netrate = ddsr.Cells["Col_NetRate"].Value.ToString();
                        mmordid = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        if (ddsr.Cells["Col_PurchaseRate"].Value != null)
                            double.TryParse(ddsr.Cells["Col_PurchaseRate"].Value.ToString(), out mmpurrate);
                        mmamt = mmqty * mmpurrate;


                        _DailyPurchaseOrder.DSLID = mmordid;
                        _DailyPurchaseOrder.DSLOrderNumber = mordno;
                        _DailyPurchaseOrder.DSLAccountID = mmaccid;
                        _DailyPurchaseOrder.DSLProductID = mmprodID;
                        _DailyPurchaseOrder.DSLQty = mmqty;
                        _DailyPurchaseOrder.DSLAmount += mmamt;
                        _DailyPurchaseOrder.DSLIFSave = "Y";
                        _DailyPurchaseOrder.DSLDailyShortList = "T";
                        _DailyPurchaseOrder.DSLPurchaseRate = mmpurrate;
                        _DailyPurchaseOrder.CreatedBy = General.CurrentUser.Id;
                        _DailyPurchaseOrder.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _DailyPurchaseOrder.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        _DailyPurchaseOrder.netrate = netrate;
                        returnVal = _DailyPurchaseOrder.CreateOrderForToday();


                    }

                    if (rowCollection.Count > 0)
                    {
                        returnVal = _DailyPurchaseOrder.AddDetails();
                        index--;
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




        private void mdgOrderSummary_SelectedRowChanged(object sender, EventArgs e)
        {

        }



        private void datetimepickerFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    ToDateShortList.Focus();
                else
                    mpMainSubViewControl1.ClearSelection();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void datetimepickerTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    btnGoClick();
                else
                    mpMainSubViewControl1.ClearSelection();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void sizefix()
        {
            mpMainSubViewControl1.dgMainGrid.Columns[0].Width = 70;
            mpMainSubViewControl1.dgMainGrid.Columns[1].Width = 270;
            mpMainSubViewControl1.dgMainGrid.Columns[2].Width = 250;
            mpMainSubViewControl1.dgMainGrid.Columns[6].Width = 90;
            mpMainSubViewControl1.dgMainGrid.Columns[7].Width = 90;
            mpMainSubViewControl1.dgMainGrid.Columns[8].Width = 90;
            mpMainSubViewControl1.dgMainGrid.Columns[9].Width = 90;
            mpMainSubViewControl1.dgMainGrid.Columns[10].Width = 90;
            mpMainSubViewControl1.dgMainGrid.Columns[11].Width = 90;
            mpMainSubViewControl1.dgMainGrid.Columns[12].Width = 70;
            //mpMainSubViewControl1.dgMainGrid.Columns[0].Width = 40;
            //mpMainSubViewControl1.dgMainGrid.Columns[10].Width = 80;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            btnGoClick();
            InitializeMainSubViewControl();
            sortcolumn();
            sizefix();

        }
        
        public void sortcolumn()
        {
            mpMainSubViewControl1.dgMainGrid.Columns["Col_ProductName"].SortMode = DataGridViewColumnSortMode.Automatic;
            //mpMainSubViewControl1.dgMainGrid.Columns["Col_SrNo"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpMainSubViewControl1.dgMainGrid.Columns["Col_AccName"].SortMode = DataGridViewColumnSortMode.Automatic;
            //mpMainSubViewControl1.dgMainGrid.Columns["Col_UOM"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpMainSubViewControl1.dgMainGrid.Columns["Col_ProdCompShortName"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpMainSubViewControl1.dgMainGrid.Columns["Col_Pack"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpMainSubViewControl1.dgMainGrid.Columns["Col_BoxQty"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpMainSubViewControl1.dgMainGrid.Columns["Col_ClosingStock"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpMainSubViewControl1.dgMainGrid.Columns["Col_Quantity"].SortMode = DataGridViewColumnSortMode.Automatic;

        }

        private void btnGo_KeyDown(object sender, KeyEventArgs e)
        {
            btnGoClick();
        }

        private void btnGoClick()
        {
            try
            {
                mpMainSubViewControl1.Rows.Clear();
                _DailyPurchaseOrder.FromDay = FromDateShortList.Value.Date.ToString("yyyyMMdd");
                _DailyPurchaseOrder.EndDay = ToDateShortList.Value.Date.ToString("yyyyMMdd");
                _DailyPurchaseOrder.FromDaySaleToday = FromDateSaleToday.Value.Date.ToString("yyyyMMdd");
                _DailyPurchaseOrder.EndDaySaleToday = ToDateSaleToday.Value.Date.ToString("yyyyMMdd");

                _DailyPurchaseOrder.FromDayNextVisit = FromdateNextVisit.Value.Date.ToString("yyyyMMdd");  //Amar
                _DailyPurchaseOrder.EndDayNextVisit = ToDateNextVisit.Value.Date.ToString("yyyyMMdd");

                double days = (ToDateShortList.Value - FromDateShortList.Value).TotalDays;
                days += 1;
                int mselection = 0;
                DataTable dtable = new DataTable();
                if (cbShortList.Checked == true)
                {
                    mselection = 1;
                    dtable = _DailyPurchaseOrder.ReadShotListByDateALLTypes();
                    if (mpMainSubViewControl.Rows.Count > 0)
                        mpMainSubViewControl.Rows.Clear();
                    BindmpMainSubViewControl(dtable, mselection);
                }
                if (cbSaleToday.Checked == true)
                {
                    mselection = 2;
                    _DailyPurchaseOrder.DSLAccountID = string.Empty;
                    dtable = _DailyPurchaseOrder.ReadListForTodayALLTypes();
                    if (mpMainSubViewControl.Rows.Count > 0)
                        mpMainSubViewControl.Rows.Clear();
                    BindmpMainSubViewControl(dtable, mselection);
                }
                if (rbtLastOrderAllProducts.Checked)
                {
                    RemoveBlankRow();
                    mselection = 3;
                    _DailyPurchaseOrder.DSLAccountID = string.Empty;
                    dtable = _DailyPurchaseOrder.ReadLastOrderAllProducts(_DailyPurchaseOrder.DSLAccountID);
                    DataRow firstdr = null;
                    int lastordernumber = 0;
                    if (dtable.Rows.Count > 0)
                    {
                        firstdr = dtable.Rows[0];
                        lastordernumber = Convert.ToInt32(firstdr["OrderNumber"].ToString());
                    }
                    BindmpMainSubViewControl(dtable, mselection);
                }
                if (chkNextVisit.Checked == true)         //Amar
                {
                    mselection = 4;
                    dtable = _DailyPurchaseOrder.ReadShotListByDateNextVisit();
                    if (mpMainSubViewControl.Rows.Count > 0)
                        mpMainSubViewControl.Rows.Clear();
                    BindmpMainSubViewControl(dtable, mselection);

                    //GetNextVisitDataPurchase();
                }

                else if (rbtLastOrderRemainingProducts.Checked)
                {
                    RemoveBlankRow();
                    mselection = 4;
                    _DailyPurchaseOrder.DSLAccountID = string.Empty;
                    dtable = _DailyPurchaseOrder.ReadLastOrderRemainingProductsAllTypes();
                    DataRow firstdr = null;
                    int lastordernumber = 0;
                    if (dtable != null && dtable.Rows.Count > 0)
                    {
                        firstdr = dtable.Rows[0];
                        lastordernumber = Convert.ToInt32(firstdr["OrderNumber"].ToString());
                        BindmpMainSubViewControl(dtable, mselection);
                    }
                }

                mpMainSubViewControl1.Sort(mpMainSubViewControl1.ColumnsMain[2], ListSortDirection.Ascending);
                mpMainSubViewControl1.Refresh();
                GetLastSale();
                CalculateAmount();

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        //private int LastSoldStock(string mprod)
        //{
        //    int lastsolddays = 0;
        //    string lastdate = "";
        //    int lastsoldqty = 0;
        //    if (txtSaleDays.Text != null && txtSaleDays.Text.ToString() != string.Empty && txtSaleDays.Text.ToString() != "0")
        //    {
        //        lastsolddays = Convert.ToInt32(txtSaleDays.Text.ToString());
        //        DateTime today = DateTime.Now;
        //        DateTime lastday = today.AddDays(lastsolddays * -1);
        //        lastdate = lastday.Date.ToString("yyyyMMdd");
        //    }
        //    lastsoldqty = _PurchaseOrder.GetSaleDataForLastSoldDays(mprod, lastdate);
        //    return lastsoldqty;
        //}
        private void RemoveBlankRow()
        {

            //foreach (DataGridViewRow drr in mpMainSubViewControl1.Rows)
            //{
            //    if (mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ID"].Value == null)
            //    {
            //        mpMainSubViewControl1.Rows.Remove(mpMainSubViewControl.MainDataGridCurrentRow);
            //    }

            //}
            //  mpMainSubViewControl.Refresh();
        }
        private bool BindmpMainSubViewControl(DataTable dt, int mselection)
        {
            //mpMainSubViewControl1.Rows.Clear();
            bool retValue = true;
          //  ConstructMainColumns();
            int rowindex = 0;
            int tempSrNo = 1;
            double mamt = 0;
            double mprate = 0;
            double mqty = 0;
            decimal orderqty = 0;
            decimal unit = 0;
            string productid = "";
            string drrproductid = "";
            bool found = false;
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
                        foreach (DataGridViewRow drr in mpMainSubViewControl1.Rows)
                        {
                            drrproductid = "";
                            try
                            {
                                if (drr.Cells["Col_ProdID"].Value != null)
                                {
                                    drrproductid = drr.Cells["Col_ProdID"].Value.ToString();

                                    if (drrproductid == productid)
                                    {
                                        found = true;
                                        //  drr.Cells["Col_ProdLastPurchaseRate"].Value = dr["ProdLastPurchaseRate"].ToString();
                                        //  drr.Cells["Col_Quantity"].Value = dr["OrderQuantity"].ToString();
                                        //  drr.Cells["Col_SchemeQuantity"].Value = dr["SchemeQuantity"].ToString();
                                        ////  drr.Cells["Col_ProdClosingStock"].Value = Convert.ToInt32(dr["ProdClosingStock"].ToString());
                                        //  drr.Cells["Col_Sale"].Value = dr["Quantity"].ToString();
                                        //  //if (dr["ProdLastPurchaseRate"] != DBNull.Value)
                                        //  //{
                                        //  //    mprate = Convert.ToDouble(dr["ProdLastPurchaseRate"].ToString());
                                        //  //}
                                        //  if (dr["OrderQuantity"] != DBNull.Value)
                                        //      mqty = Convert.ToDouble(dr["OrderQuantity"].ToString());
                                        //  mamt = mprate * mqty;
                                        //  drr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                                        //  if (mselection == 2)
                                        //      drr.DefaultCellStyle.BackColor = cbSaleToday.BackColor;
                                        //  else if (mselection == 3)
                                        //      drr.DefaultCellStyle.BackColor = rbtLastOrderAllProducts.BackColor;
                                        //  else if (mselection == 4)
                                        //      drr.DefaultCellStyle.BackColor = rbtLastOrderRemainingProducts.BackColor;
                                        break;
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                Log.WriteException(Ex);
                            }

                        }
                        if (found == false)
                        {
                            try
                            {
                                unit = 0;
                                rowindex = mpMainSubViewControl1.Rows.Add();
                                //mpMainSubViewControl1.Rows[rowindex].Cells["Col_SrNo"].Value = tempSrNo;
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_ACCID"].Value = dr["AccountID"].ToString();
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_AccName"].Value = dr["AccName"].ToString();
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_ProdID"].Value = dr["ProductID"].ToString();
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_UOM"].Value = dr["ProdLoosePack"].ToString();
                                unit = Convert.ToDecimal(dr["ProdLoosePack"].ToString());
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_Pack"].Value = dr["ProdPack"].ToString();
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_ProdCompShortName"].Value = dr["ProdCompShortName"].ToString();
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_BoxQty"].Value = dr["ProdBoxQuantity"].ToString();
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_ClosingStock"].Value = Convert.ToInt32(dr["ProdClosingStock"].ToString());
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_PurchaseRate"].Value = dr["ProdLastPurchaseRate"].ToString();
                                orderqty = Convert.ToDecimal(dr["Quantity"].ToString());
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_Quantity"].Value = Math.Round(orderqty / unit); //dr["Quantity"].ToString();
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_SchemeQuantity"].Value = 0;
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_NetRate"].Value = "N";
                                mpMainSubViewControl1.Rows[rowindex].Cells["Col_CheckBox"].Value ="True";
                                if (dr["ProdLastPurchaseRate"] != DBNull.Value)
                                {
                                    mprate = Convert.ToDouble(dr["ProdLastPurchaseRate"].ToString());
                                }
                                if (dr["Quantity"] != DBNull.Value)
                                    mqty = Convert.ToDouble(dr["Quantity"].ToString());
                                mamt = mprate * mqty;
                                //       mpMainSubViewControl1.Rows[rowindex].Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                                if (mselection == 1)
                                    mpMainSubViewControl1.Rows[rowindex].DefaultCellStyle.BackColor = cbShortList.BackColor;
                                else if (mselection == 2)
                                    mpMainSubViewControl1.Rows[rowindex].DefaultCellStyle.BackColor = cbSaleToday.BackColor;
                                else if (mselection == 3)
                                    mpMainSubViewControl1.Rows[rowindex].DefaultCellStyle.BackColor = rbtLastOrderAllProducts.BackColor;
                                else if (mselection == 4)
                                    mpMainSubViewControl1.Rows[rowindex].DefaultCellStyle.BackColor = rbtLastOrderRemainingProducts.BackColor;
                                tempSrNo += 1;
                                                              
                               
                              }
                            catch (Exception Ex)
                            {
                                Log.WriteException(Ex);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
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
                                        drr.Cells["Col_ProdClosingStock"].Value = dr["ProdClosingStock"].ToString();
                                        drr.Cells["Col_Sale"].Value = dr["Quantity"].ToString();
                                        if (dr["ProdLastPurchaseRate"] != DBNull.Value)
                                        {
                                            mprate = Convert.ToDouble(dr["ProdLastPurchaseRate"].ToString());
                                        }
                                        if (dr["OrderQuantity"] != DBNull.Value)
                                            mqty = Convert.ToDouble(dr["OrderQuantity"].ToString());
                                        mamt = mprate * mqty;
                                    //    drr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
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
                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {
                    mprod = string.Empty;
                    lastsoldstock = 0;
                    if (dr.Cells["Col_ProdID"].Value != null)
                        mprod = dr.Cells["Col_ProdID"].Value.ToString();
                    if (mprod != string.Empty)
                        lastsoldstock = LastSoldStock(mprod);
                    dr.Cells["Col_SaleStock"].Value = lastsoldstock.ToString("#0");

                }

            }
        }
        private void CalculateAmount()
        {
            double TotalAmount = 0;
            int itemCount = 0;
            double mtot = 0;
            try
            {
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
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
        #endregion Events


        public void ConstructBatchGrid()
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
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvBatchGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                column.Visible = false;
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
        private void mpMainSubViewControl1_OnTABKeyPressed(object sender, EventArgs e)
        {
            dgvBatchGrid.Visible = true;
            FillBatchGrid();

        }
        private void FillBatchGrid()
        {
            DataTable dt = new DataTable();
            SsStock invss = new SsStock();
            int rowindex = 0;

            string prodid = "";
            if (mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ProdID"].Value != null)
                prodid = mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ProdID"].Value.ToString();


            //  int expdt = 0;           
            if (dgvBatchGrid.Rows.Count > 0)
            {
                dgvBatchGrid.Rows.Clear();
                foreach (DataGridViewRow dr in dgvBatchGrid.Rows)
                {
                    dgvBatchGrid.Rows.Remove(dr);
                }
            }
            if (dgvBatchGrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow dr in dgvBatchGrid.Rows)
                {
                    dgvBatchGrid.Rows.Remove(dr);
                }
            }
            try
            {
                dt = invss.GetPurchaseDetailsForPurchaseOrder(prodid.ToString());
                //  dgvBatchGrid.DataSource = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ConstructBatchGrid();
                        //rowindex = mpMainSubViewControl.Rows.Add();
                        rowindex = dgvBatchGrid.Rows.Add();
                        dgvBatchGrid.Rows[rowindex].Cells["Col_Batchno"].Value = dr["Batchnumber"].ToString();
                        dgvBatchGrid.Rows[rowindex].Cells["Col_Expiry"].Value = dr["Expiry"].ToString();
                        dgvBatchGrid.Rows[rowindex].Cells["Col_TradeRate"].Value = dr["TradeRate"].ToString();
                        dgvBatchGrid.Rows[rowindex].Cells["Col_MRP"].Value = dr["MRP"].ToString();
                        dgvBatchGrid.Rows[rowindex].Cells["Col_SaleRate"].Value = dr["SaleRate"].ToString();
                        dgvBatchGrid.Rows[rowindex].Cells["Col_AccountName"].Value = dr["AccName"].ToString();
                        //   dgvBatchGrid.Rows[rowindex].Cells["Col_UOM"].Value = dr["ProdLoosePack"].ToString();
                        //   dgvBatchGrid.Rows[rowindex].Cells["Col_ExpiryDate"].Value = dr["ExpiryDate"].ToString();
                        dgvBatchGrid.Rows[rowindex].Cells["Col_PurchaseRate"].Value = dr["PurchaseRate"].ToString();
                        //    dgvBatchGrid.Rows[rowindex].Cells["Col_PurchaseVATPer"].Value = dr["PurchaseVATPercent"].ToString();
                        //   dgvBatchGrid.Rows[rowindex].Cells["Col_ProdCSTPer"].Value = dr["ProdCSTPercent"].ToString();
                        //    dgvBatchGrid.Rows[rowindex].Cells["Col_ScanCode"].Value = dr["ScanCode"].ToString();
                        dgvBatchGrid.Rows[rowindex].Cells["Col_ProductID"].Value = dr["ProductID"].ToString();
                        dgvBatchGrid.Rows[rowindex].Cells["Col_AccountID"].Value = dr["AccountID"].ToString();
                        //   dgvBatchGrid.Rows[rowindex].Cells["Col_StockID"].Value = dr["StockID"].ToString();
                        //}
                        //}

                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            dgvBatchGrid.Focus();
        }

        public void dgvBatchGrid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        public void btnUpLoad_Click(object sender, EventArgs e)
        {
            BtnUploadClick();

        }

        public void BtnUploadClick()
        {
            ////Orders orders = new Orders();
            //Emilan emilan = new Emilan();

            //DataTable dtOrders = emilan.GetPurchaseOrdersForOrderForToday(_DailyPurchaseOrder.DSLFirstOrderNumber, _DailyPurchaseOrder.DSLLastOrderNumber);
            //FormUploadEmilan uem = new FormUploadEmilan();

            //uem.emilandt = dtOrders;
            //uem.FillDetails();
            //uem.ShowDialog();
            //MessageBox.Show("Upload successful", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////  orders.OrdersFromUserBulk(General.DeveloperId, General.UserId, General.Password, dtOrders);
        }

        private void btnCreateOrder_Click_1(object sender, EventArgs e)
        {
            btnCreateOrderClick();
        }

        private void btnCreateOrder_KeyDown_1(object sender, KeyEventArgs e)
        {
            btnCreateOrderClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               for (int i = 0; i < mpMainSubViewControl1.Rows.Count; i++)
                {
                    mpMainSubViewControl1.dgMainGrid.Rows[i].Cells["Col_Quantity"].Value = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString ());
            }
        }

        private void FromDateShortList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ToDateShortList.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }

           
            }

        private void ToDateShortList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cbShortList.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbShortList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FromDateSaleToday.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FromDateSaleToday_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ToDateSaleToday.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ToDateSaleToday_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cbSaleToday.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbSaleToday_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        rbtLastOrderAllProducts.Focus();
                        break;
                    case Keys.Down:
                        rbtLastOrderRemainingProducts.Focus();
                        break;
                    case Keys.Up:
                        psRadioButton2.Focus();
                        break;
                   
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void rbtLastOrderAllProducts_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        rbtMinMax.Focus();
                        break;
                    case Keys.Down:
                        rbtExpiry.Focus();
                        break;
                    case Keys.Up:
                        psRadioButton1.Focus();
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void rbtLastOrderRemainingProducts_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        rbtMinMax.Focus();
                        break;
                    case Keys.Down:
                        rbtExpiry.Focus();
                        break;
                    case Keys.Up:
                        psRadioButton1.Focus();
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void psRadioButton2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        rbtMinMax .Focus();
                        break;
                    case Keys.Down:
                        rbtExpiry .Focus();
                        break;
                    case Keys.Up:
                        psRadioButton1 .Focus();
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void rbtMinMax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        FromdateNextVisit.Focus();
                        break;
                    case Keys.Down:
                        rbtExpiry.Focus();
                        break;
                    case Keys.Up:
                        psRadioButton1.Focus();
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void rbtExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        FromdateNextVisit.Focus();
                        break;
                    case Keys.Down:
                        rbtExpiry.Focus();
                        break;
                    case Keys.Up:
                        psRadioButton1.Focus();
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void psRadioButton1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        FromdateNextVisit.Focus();
                        break;
                    case Keys.Down:
                        rbtExpiry.Focus();
                        break;
                    case Keys.Up:
                        psRadioButton1.Focus();
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FromdateNextVisit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ToDateNextVisit.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ToDateNextVisit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    chkNextVisit.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chkNextVisit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtSaleDays.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtSaleDays_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
               switch (e.KeyCode)
                {
                    case Keys.Enter:
                        //GetLastSale();
                        btncloneOK.Focus();
                        break;
                    case Keys.Up:
                        chkNextVisit.Focus();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnRecoverQty_Click(object sender, EventArgs e)
        {
            try
            {
                GetRecoverQty();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void GetRecoverQty()
        {
            string mprod = string.Empty;
            int lastsoldstock = 0;
            //  mpMainSubViewControl.ColumnsMain["Col_Sale"].ReadOnly = false;
            //if (txtSaleDays.Text != null && txtSaleDays.Text.ToString() != string.Empty)
            //{
                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {
                    mprod = string.Empty;
                //string fromdate = "";
                //string Todate = "";
                //fromdate = FromDateSaleToday.Date.ToString("yyyyMMdd");
                //Todate = ToDateSaleToday.Date.ToString("yyyyMMdd");
                lastsoldstock = 0;
                    if (dr.Cells["Col_ProdID"].Value != null)
                        mprod = dr.Cells["Col_ProdID"].Value.ToString();
                if (mprod != string.Empty)
                    lastsoldstock = _PurchaseOrder.GetRecoverQuantity(mprod, FromDateSaleToday.Value .ToString ("yyyyMMdd"), ToDateSaleToday.Value .ToString ("yyyyMMdd"));
                //lastsoldqty = _PurchaseOrder.GetSaleDataForLastSoldDays(mprod, lastdate);
                dr.Cells["Col_Quantity"].Value = lastsoldstock.ToString("#0");

                }

            //}
        }
        private void txtNoOfRows_TextChanged(object sender, EventArgs e)
        {

        }
                   
        private void mdgOrderSummary_SelectedRowChanged_1(object sender, EventArgs e)
        {
            try
            {
                _DailyPurchaseOrder.CurrentOrderNumber = Convert.ToInt32(mdgOrderSummary.SelectedRow.Cells[0].Value.ToString().Trim());
                FillDetailGrid();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpMainSubViewControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (mpMainSubViewControl1.dgMainGrid.CurrentCell.ColumnIndex == 25)
            //{
            //    if ((e.KeyChar == 78) || e.KeyChar == 110)
            //    {
            //        e.Handled = false;
            //    }

            //}
        }

        private void dgvBatchGrid_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ACCID"].Value = dgvBatchGrid.CurrentRow.Cells["Col_AccountID"].Value.ToString();
                mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_AccName"].Value = dgvBatchGrid.CurrentRow.Cells["Col_AccountName"].Value.ToString();
                dgvBatchGrid.Visible = false;
                mpMainSubViewControl1.Focus();
            }
        }

        private void mpMainSubViewControl1_OnTABKeyPressed_1(object sender, EventArgs e)
        {
            dgvBatchGrid.Visible = true;
            dgvBatchGrid.BringToFront();
            FillBatchGrid();
        }

        private void GetNextVisitDataPurchase()
        {
            try
            {
                SaleList _SaleList = new BusinessLayer.SaleList();
                _BindingSource = new DataTable();
                _BindingSource = _SaleList.GetNextVisitDays(DateTime.Now.Date.ToString("yyyyMMdd"), DateTime.Now.Date.AddDays(2).ToString("yyyyMMdd"));
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void cbSaleToday_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cbSaleToday.Checked == true)
            //    {
            //        daydiffrence();
            //    } 
                               
            //}
            //catch (Exception ex)
            //{
            //    Log.WriteException(ex);
            //}
        }

        private void uclPurchaseNewProduct2_Load(object sender, EventArgs e)
        {

        }

        //mpMainSubViewControl1.dgMainGrid.CurrentRow.Cells["Col_CheckBox"].Value = "True";
    }
}
