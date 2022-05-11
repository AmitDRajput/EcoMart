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


namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclPurchaseOrderForToday : BaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DailyPurchaseOrder _DailyPurchaseOrder;
        private List<DataGridViewRow> rowCollection;
        private List<DataGridViewRow> rowCollectionmain;
        #endregion

        #region Constructor

        public UclPurchaseOrderForToday()
        {
            InitializeComponent();
            _BindingSource = new DataTable();
            _DailyPurchaseOrder = new DailyPurchaseOrder();
        }
        #endregion

        # region IDetail Control
        public override void SetFocus()
        {
            datetimepickerFrom.Focus();
            
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
                headerLabel1.Text = "DAILY PURCHASE ORDER -> NEW";
                pnlDate.Enabled = true;                
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

        public override bool Save()
        {

            bool retValue = false;

            try
            {
                if (_Mode == OperationMode.Add)
                {
                    _SavedID = _DailyPurchaseOrder.Id;
                    retValue = saveorders();
                    if (retValue == true)
                    {
                        MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Visible = false;
                    }
                    this.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

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
                    datetimepickerFrom.Select();
                    datetimepickerFrom.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.O && modifier == Keys.Alt)
                {
                    btnCreateOrder.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.V && modifier == Keys.Alt)
                {
                    btnGoClick();
                    retValue = true;
                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                    datetimepickerTo.Select();
                    datetimepickerTo.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.U && modifier == Keys.Alt)
                {
                    BtnUploadClick();
                    retValue = true;
                }
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
                datetimepickerFrom.ResetText();
                datetimepickerTo.ResetText();
                mpMainSubViewControl1.Dock = DockStyle.Fill;
                ConstructMainColumns();
                DataTable dtable = new DataTable();
                DataTable dt = new DataTable();
                mpMainSubViewControl1.DataSourceMain = dtable;
                mpMainSubViewControl1.DataSource = dt;
                mpMainSubViewControl1.Bind();
                tsBtnSavenPrint.Visible = true;
                pnlSummary.Visible = false;
                txtAmount.Text = "";
                txtNoofOrders.Text = "";
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

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ACCID";
            column.DataPropertyName = "AccountID";
            column.HeaderText = "ACCID";
            column.Visible = false;          
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName";
            column.DataPropertyName = "AccName";
            column.HeaderText = "AccountName";
            column.Width = 300;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
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
            column.Name = "Col_ProductName";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "ProdName";
            column.Width = 220;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UOM";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 60;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Pack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 60;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProdCompShortName";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 60;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_BoxQty";
            column.DataPropertyName = "ProdBoxQuantity";
            column.HeaderText = "BoxQty";
            column.Width = 80;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingStock";
            column.DataPropertyName = "ProdClosingStock";
            column.HeaderText = "Cl.Stock";
            column.Width = 80;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.ReadOnly = true;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            //column = new DataGridViewTextBoxColumn(); [Commented By Ansuman]
            //column.Name = "Col_SaleStock";
            //column.DataPropertyName = "Quantity";
            //column.HeaderText = "Sale Qty";
            //column.Width = 80;
            //column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //column.ReadOnly = true;
            //mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID1";
          //  column.DataPropertyName = "AccountID1";
            column.HeaderText = "ID1";
            column.Visible = false;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName1";
           column.DataPropertyName = "AccName1";
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
            column.Width = 100;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            mpMainSubViewControl1.ColumnsMain.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_SchemeQuantity";
            column.DataPropertyName = "SchemeQuantity";
            column.HeaderText = "Scm Qty";
            column.Width = 100;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            mpMainSubViewControl1.ColumnsMain.Add(column);


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
            column.Width = 120;
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
            column.Width = 200;
            mdgOrderDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_LoosePack";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 70;
            column.ReadOnly = true;
            mdgOrderDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Pack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 70;
            mdgOrderDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_CompShortName";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 70;
            mdgOrderDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OrderQuantity";
            column.DataPropertyName = "OrderQuantity";
            column.HeaderText = "Quantity";
            column.Width = 100;
            mdgOrderDetail.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_OrderNumber";
            column.DataPropertyName = "OrderNumber";
            column.HeaderText = "OrderNumber";
            column.Width = 150;
            column.Visible = false;
            mdgOrderSummary.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.DataPropertyName = "Amount";
            column.HeaderText = "Amount";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 100;
            mdgOrderDetail.Columns.Add(column);
        }


        #endregion

        # region Other Methods

        private void InitializeMainSubViewControl()
        {
            try
            {
                ConstructMainColumns();
                ConstructSubColumns();
                DataTable dtable = new DataTable();
                mpMainSubViewControl1.NextRowColumn = 0;
                dtable = _DailyPurchaseOrder.ReadShortListByDateForToday();
                if (dtable != null && dtable.Rows.Count == 0)
                    lblFooterMessage.Text = "No Records Found";
                mpMainSubViewControl1.DataSourceMain = dtable;
                mpMainSubViewControl1.NumericColumnNames.Add("Col_Quantity");
                Account acc = new Account();
                DataTable dt = acc.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
                mpMainSubViewControl1.DataSource = dt;

                mpMainSubViewControl1.Bind();
               
                    SelectFirstParty();
                
                //else
                //    SelectSecondParty();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void SelectFirstParty()
        {
            try
            {
                foreach (DataGridViewRow dsr in mpMainSubViewControl1.Rows)
                {
                    if (dsr.Cells["Col_IfSave"].Value != null && dsr.Cells["Col_IfSave"].Value.ToString().Trim() != "Y") // sheela 4/1/17
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
              //  mpMainSubViewControl1.Refresh();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }

        }

        private bool saveorders()
        {
            {
                bool returnVal = false;
                string mdslid = null;
                string maccid = null;
                try
                {
                    foreach (DataGridViewRow dsr in mpMainSubViewControl1.Rows)
                    {
                        int mqty = 0;
                        int.TryParse(dsr.Cells["Col_Quantity"].Value.ToString(), out mqty);
                        if (dsr.Cells["Col_ACCID"].Value != null &&  mqty > 0)
                        {
                            //mdslid = dsr.Cells["Col_ShortListID"].Value.ToString();
                            //_DailyPurchaseOrder.DSLID = mdslid;
                            mdslid = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            maccid = dsr.Cells["Col_ACCID"].Value.ToString();
                            _DailyPurchaseOrder.DSLAccountID = maccid;
                            _DailyPurchaseOrder.DSLQty = mqty;
                            _DailyPurchaseOrder.DSLIFSave = "Y";
                            returnVal = _DailyPurchaseOrder.SaveOrder();
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
        }

        private void PrintData()
        {
            try
            {
                PrintFactory.SendReverseLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.ReverseLineFeed);
                //PrintBill.Rows.Clear();
                Font fnt = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = 0;
                PrintPageNumber = 0;
                int rowcount = 0;
                double totpages = 0;
                int totalpages = 0;
                int preordernumber = 0;
                PrintRowPixel = 0;
                DataTable dtable = new DataTable();
                dtable = _DailyPurchaseOrder.ReadOrderByNumber();
                mpMainSubViewControl1.DataSourceMain = dtable;
                mpMainSubViewControl1.Bind();

                for (int ordno = _DailyPurchaseOrder.DSLFirstOrderNumber; ordno <= _DailyPurchaseOrder.DSLLastOrderNumber; ordno++)
                {
                    PrintRow row;
                    PrintBill.Rows.Clear();
                    List<DataGridViewRow> rowCollection = new List<DataGridViewRow>();
                    foreach (DataGridViewRow prodrow in mpMainSubViewControl1.Rows)
                    {

                        if (prodrow.Cells["Col_OrderNumber"].Value != null)
                            preordernumber = Convert.ToInt32(prodrow.Cells["Col_OrderNumber"].Value.ToString());
                        if (preordernumber == _DailyPurchaseOrder.DSLFirstOrderNumber)
                        {
                            rowCollection.Add(prodrow);
                            _DailyPurchaseOrder.DSLAccountName = prodrow.Cells["Col_AccName1"].Value.ToString();
                            _DailyPurchaseOrder.DSLAddress1 = prodrow.Cells["Col_AccAddress1"].Value.ToString();
                            _DailyPurchaseOrder.DSLAddress2 = prodrow.Cells["Col_AccAddress2"].Value.ToString();
                        }
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
                    List<int> PrintRowPixelData = PrintHeader(totalpages, rowcount, fnt);
                    bool NextRowFlag = false;
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


                            //row = new PrintRow(prodrow.Cells["Col_ProdCompShortName"].Value.ToString(), PrintRowPixel, 260, fnt);
                            //PrintBill.Rows.Add(row);
                            //double mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            //row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 700, fnt);
                            //PrintBill.Rows.Add(row);
                        }
                        //PrintRowPixel += 17;
                        //rowcount += 1;
                        //int myqty = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        ////.Cells["Col_Quantity"].Value.ToString());
                        //row = new PrintRow(myqty.ToString("#0"), PrintRowPixel, 15, fnt);
                        //PrintBill.Rows.Add(row);
                        //row = new PrintRow(prodrow.Cells["Col_ProductName"].Value.ToString(), PrintRowPixel, 85, fnt);
                        //PrintBill.Rows.Add(row);
                        //row = new PrintRow(prodrow.Cells["Col_UOM"].Value.ToString(), PrintRowPixel, 260, fnt);
                        //PrintBill.Rows.Add(row);
                        //row = new PrintRow(" X " + prodrow.Cells["Col_Pack"].Value.ToString(), PrintRowPixel, 280, fnt);
                        //PrintBill.Rows.Add(row);
                        //row = new PrintRow(prodrow.Cells["Col_ProdCompShortName"].Value.ToString(), PrintRowPixel, 360, fnt);
                        //PrintBill.Rows.Add(row);
                        //double mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        //row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 700, fnt);
                        //PrintBill.Rows.Add(row);                        

                    }
                    PrintFooter(fnt);

                    //PrintRowPixel = 418;
                    //row = new PrintRow("---", PrintRowPixel, 15, fnt);
                    //PrintBill.Rows.Add(row);

                    _DailyPurchaseOrder.DSLFirstOrderNumber += 1;


                    PrintBill.Print_Bill(600, 400);

                    //   PrintBill.Rows.Clear();
                    rowCollection = new List<DataGridViewRow>();

                }
                PrintFactory.SendLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.LineFeed);
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

                row = new PrintRow("Purchase Order :" + (_DailyPurchaseOrder.DSLFirstOrderNumber.ToString().Trim()), PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(General.ShopDetail.ShopAddress1.Trim(), PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Time :" + DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(General.ShopDetail.ShopAddress2.Trim(), PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                string voudate = DateTime.Now.ToShortDateString();
                row = new PrintRow("Date :" + voudate, PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(_DailyPurchaseOrder.DSLAccountName, PrintRowPixel, 10, mfnt);
                PrintBill.Rows.Add(row);

                PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow("Page :" + page, PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                string myadd = _DailyPurchaseOrder.DSLAddress1.Trim();
                row = new PrintRow(myadd + ",", PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                string myadd2 = _DailyPurchaseOrder.DSLAddress2.Trim();
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
            bool retValue = false;
            tsBtnSave.Enabled = false;
            string preaccountid = "";
            int mqty = 0;
            int mmordno = 0;
            int mscmqty = 0;
            try
            {

                mpMainSubViewControl1.Sort(mpMainSubViewControl1.ColumnsMain[1], ListSortDirection.Ascending);
                mpMainSubViewControl1.Refresh();
                rowCollectionmain = new List<DataGridViewRow>();
                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {
                    preaccountid = dr.Cells["Col_ACCID"].Value.ToString().Trim();
                    int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out mqty);
                    int.TryParse(dr.Cells["Col_SchemeQuantity"].Value.ToString(), out mscmqty);

                   //   int.TryParse(dr.Cells["Col_OrderNumber"].Value.ToString(), out mmordno);
                    if (preaccountid != "" && mqty > 0 && mmordno == 0)
                    {
                        rowCollectionmain.Add(dr);
                    }

                }
                if (rowCollectionmain.Count > 0)
                    retValue = CreateOrders();
                if (retValue == true)
                {

                    ConstructOrderSummary();
                    ConstructOrderDetails();
                    FillSummaryGrid();
                    pnlDate.Enabled = false;                   
                    pnlSummary.BringToFront();
                    pnlSummary.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        private void FillSummaryGrid()
        {
            FillSummaryData();
            mdgOrderSummary.DataSource = _BindingSource;
            mdgOrderSummary.Bind();
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
                    int mmscmqty = 0;
                    string mmaccid = "";
                    string mmordid = "";
                    string preaccountid = "";
                    double mmpurrate = 0;
                    double mmamt = 0;
                    string mmprodID = "";


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
                    mmscmqty = 0;

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
                        int.TryParse(ddsr.Cells["Col_SchemeQuantity"].Value.ToString(), out mmscmqty);
                        mmaccid = ddsr.Cells["Col_ACCID"].Value.ToString();
                        mmprodID = ddsr.Cells["Col_ProdID"].Value.ToString();
                        mmordid = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        if (ddsr.Cells["Col_PurchaseRate"].Value != null)
                            double.TryParse(ddsr.Cells["Col_PurchaseRate"].Value.ToString(), out mmpurrate);
                        mmamt = mmqty * mmpurrate;


                        _DailyPurchaseOrder.DSLID = mmordid;
                        _DailyPurchaseOrder.DSLOrderNumber = mordno;
                        _DailyPurchaseOrder.DSLAccountID = mmaccid;
                        _DailyPurchaseOrder.DSLProductID = mmprodID;
                        _DailyPurchaseOrder.DSLQty = mmqty;
                        _DailyPurchaseOrder.DSLSchemeQuantity = mmscmqty;
                        _DailyPurchaseOrder.DSLAmount += mmamt;
                        _DailyPurchaseOrder.DSLIFSave = "Y";
                        _DailyPurchaseOrder.DSLDailyShortList = "T";
                        _DailyPurchaseOrder.DSLPurchaseRate = mmpurrate;
                        _DailyPurchaseOrder.CreatedBy = General.CurrentUser.Id;
                        _DailyPurchaseOrder.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _DailyPurchaseOrder.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
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



        private void datetimepickerFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    datetimepickerTo.Focus();
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

        private void btnGo_Click(object sender, EventArgs e)
        {
            btnGoClick();
        }

        private void btnGo_KeyDown(object sender, KeyEventArgs e)
        {
            btnGoClick();
        }

        private void btnGoClick()
        {
            try
            {
                ConstructBatchGrid();
                _DailyPurchaseOrder.FromDay = datetimepickerFrom.Value.Date.ToString("yyyyMMdd");
                _DailyPurchaseOrder.EndDay = datetimepickerTo.Value.Date.ToString("yyyyMMdd");
                _DailyPurchaseOrder.DayofWeek = 0;
                _DailyPurchaseOrder.DayofWeek = (int)(DateTime.Today.DayOfWeek);
                InitializeMainSubViewControl();
                mpMainSubViewControl1.SetFocus(1);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                        //mclstk = 0;
                        //mexpirydate = string.Empty;
                        //if (dr["StockID"] != DBNull.Value && dr["StockID"].ToString() != string.Empty)
                        //{
                        //    mstockid = dr["StockID"].ToString();
                        //    if (dr["ClosingStock"] != DBNull.Value && dr["ClosingStock"].ToString() != string.Empty)
                        //        mclstk = Convert.ToInt32(dr["ClosingStock"].ToString());
                        //    if (dr["ExpiryDate"] != DBNull.Value)
                        //    {
                        //        if (dr["ExpiryDate"].ToString() != string.Empty)
                        //            mexpirydate = dr["ExpiryDate"].ToString();
                        //    }
                            //if (mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null && mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString() != string.Empty)
                            //{
                            //    prodsaleqty = Convert.ToInt32(mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                            //}
                            //if (mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_Scheme"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value.ToString() != string.Empty)
                            //{
                            //    prodscmqty = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value.ToString());
                            //}
                            //if (mclstk > 0 || ((mexpirydate == string.Empty || Convert.ToInt32(mexpirydate) >= expdt)) && (mstockid == prodstockid && _Mode == OperationMode.Edit))
                            //{
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
            if (e.KeyCode == Keys.Enter)
            {
                mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ACCID"].Value = dgvBatchGrid.CurrentRow.Cells["Col_AccountID"].Value.ToString();
                mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_AccName"].Value = dgvBatchGrid.CurrentRow.Cells["Col_AccountName"].Value.ToString();
                dgvBatchGrid.Visible = false;
                mpMainSubViewControl1.Focus();
            }
        }

        public void btnUpLoad_Click(object sender, EventArgs e)
        {
            BtnUploadClick();
            
        }

        public void BtnUploadClick()
        {
            //Orders orders = new Orders();
            Emilan emilan = new Emilan();
           
            DataTable dtOrders = emilan.GetPurchaseOrdersForOrderForToday(_DailyPurchaseOrder.DSLFirstOrderNumber,_DailyPurchaseOrder.DSLLastOrderNumber);
            FormUploadEmilan uem = new FormUploadEmilan();

            uem.emilandt = dtOrders;
            uem.FillDetails();
            uem.ShowDialog();
            MessageBox.Show("Upload successful", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
          //  orders.OrdersFromUserBulk(General.DeveloperId, General.UserId, General.Password, dtOrders);
        }

         
       
    }
}
