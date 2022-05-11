using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSDistributorPlus.Common;
using PharmaSYSDistributorPlus.BusinessLayer;
using PharmaSYSDistributorPlus.InterfaceLayer;
using PharmaSYSDistributorPlus.Printing;
using PrintDataGrid;

namespace PharmaSYSDistributorPlus.Reporting.Controls
{
     [System.ComponentModel.ToolboxItem(false)]
    public partial class UclLBTSummary : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private Purchase _Purchase;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        private double _MTotalPurchase;
        private double _MTotalLocalPurchase;       
        //private double _MTotalNetPurchase;
        //private double _MTotalOUTSale;
        private double _MTotalLBTPurchase;
        //private double _MTotalLBTSale;


        #endregion

        # region Constructor
        public UclLBTSummary()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclPurchase();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        #region IOverview Members
        public override void ShowOverview()
        {
            try
            {

                _BindingSource = new DataTable();
                _Purchase = new Purchase();
                headerLabel1.Text = "PURCHASE-PARTYWISE BILLS";
                ClearControls();              
                AddToolTip();
                HidepnlGO();
                fromDate1.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void ShowReport(string ID, string blank, string FromDate, string ToDate)
        {
            base.ShowReport(ID,blank, FromDate, ToDate);
            if (ID != null && ID != "")
            {
                _MFromDate = FromDate;
                _MToDate = ToDate;              
                ShowReportGrid();

            }
        }

        public override void SetFocus()
        {
            base.SetFocus();
            fromDate1.Focus();
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Home)
            {
                HidepnlGO();
                retValue = true;
            }
            if (keyPressed == Keys.End)
            {
                btnOKMultiSelectionClick();
                retValue = true;
            }

            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }
        #endregion

        # region IReport Members
        public override void Export(string ExportFileName)
        {
            base.Export(ExportFileName);
            GeneralReport.ExportFile(PrintReportHead, PrintReportHead2, dgvReportList, ExportFileName);
        }

        public override void EMail(string EmailID)
        {
            base.EMail(EmailID);
            GeneralReport.SendEmails(PrintReportHead, PrintReportHead2, dgvReportList, EmailID);
        }
        public override void Print()
        {
            try
            {
                PrintData();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void PrintData()
        {
            PrintRow row;
            try
            {               
                PrintBill.Rows.Clear();
                PrintFont = new Font(General.CurrentSetting.MsetPrintFontName, Convert.ToInt32(General.CurrentSetting.MsetPrintFontSize));
                int totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    double mamt = 0;
                    if (dr.Cells["Col_VoucherType"].Value != null)
                    {
                        if (PrintRowCount >= FixAccounts.NumberOfRowsPerReport)
                        {
                            PrintRowPixel += 17;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintPageNumber += 1;
                            PrintHead();
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        if (dr.Cells["Col_VoucherType"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadRight(30), PrintRowPixel, 40, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherDate"].Value != null)
                        {
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()).PadRight(30), PrintRowPixel, 90, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_BillNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_BillNumber"].Value.ToString().PadRight(30), PrintRowPixel, 140, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Party"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Party"].Value.ToString().PadRight(30), PrintRowPixel, 200, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Product"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Product"].Value.ToString().PadRight(30), PrintRowPixel, 350, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Pack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Pack"].Value.ToString().PadRight(30), PrintRowPixel, 500, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Category"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Category"].Value.ToString().PadRight(30), PrintRowPixel, 540, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_LBT"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_LBT"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                    }
                }
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                mlen = (_MTotalAmount.ToString("#0.00").Length);
                colpix = Convert.ToInt32(320.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow("Purchase Amount:" + _MTotalPurchase.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                mlen = (_MTotalAmount.ToString("#0.00").Length);
                colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_MTotalAmount.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private int PrintHead()
        {
            PrintRow row;
            try
            {
                PrintFont = new Font(General.CurrentSetting.MsetPrintFontName, Convert.ToInt32(General.CurrentSetting.MsetPrintFontSize));
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));
                PrintFont = new Font(General.CurrentSetting.MsetPrintFontName, Convert.ToInt32(General.CurrentSetting.MsetPrintFontSize));
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 40, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 90, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Bill No", PrintRowPixel, 140, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 200, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Product", PrintRowPixel, 350, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pack", PrintRowPixel, 500, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Category", PrintRowPixel, 540, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("LBT", PrintRowPixel, 700, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 0;
            return PrintRowCount;
        }
        #endregion IReport Memebers

        # region Other Private methods

        public void ClearControls()
        {
            try
            {
                InitializeReportGrid();
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();
                InitializeDates();
                lblFooterMessage.Text = "";               
                txtViewText.Text = "";              
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void InitializeDates()
        {
            _MFromDate = General.ShopDetail.Shopsy;
            _MToDate = General.ShopDetail.Shopey;
            fromDate1.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
            toDate1.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);

        }

        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PurchaseID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Text";              
                column.HeaderText = "Particulars";
                column.Width = 400;
                dgvReportList.Columns.Add(column);              

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LBT";
                column.DataPropertyName = "LBTAmount";
                column.HeaderText = "LBT";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.DateColumnNames.Add("Col_VoucherDate");
                dgvReportList.DoubleColumnNames.Add("Col_Amount");
                dgvReportList.Bind();
                dgvReportList.Columns["Col_ID"].Visible = false;
                _MTotalAmount = 0;
                _MTotalPurchase = 0;
                int prevouno = 0;
                int vouno = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    vouno += Convert.ToInt32(dr.Cells["Col_VoucherNumber"].Value.ToString());
                    if (prevouno != vouno)
                    {
                        prevouno = vouno;
                        _MTotalPurchase += Convert.ToDouble(dr.Cells["Col_Purchase"].Value.ToString());
                    }
                    _MTotalAmount += Convert.ToDouble(dr.Cells["Col_LBT"].Value.ToString());
                }               
                NoofRows();
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }

        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
                //   if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                //     dtable = _Purchase.GetOverviewDataForLBTPurchaseOUT(mcbCreditor.SelectedID, _MFromDate, _MToDate);
                // _MTotalPurchase = _Purchase.GetOverviewDataForLBTSummaryTotalPurchase(_MFromDate, _MToDate);
                //_MTotalLBTPurchase = _Purchase.GetOverviewDataForLBTPurchaseOUTSummary(_MFromDate, _MToDate);
                //_MTotalLocalPurchase = _Purchase.GetOverviewDataForLBTPurchaseINSummary(_MFromDate, _MToDate);
                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }     

        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Text = "";
            ViewToDate.Text = "";         
            txtViewText.Text = "";
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            if (General.PharmaSYSDistributorPlusLicense.LicenseType == LicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);          
            dgvReportList.Focus();
        }
        #endregion

        # region Events

        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            try
            {
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ShowReportGrid();
                    PrintReportHead = "LBT Purchase From Outside  From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                    PrintReportHead2 = "[" + txtViewText.Text.ToString() + "]";
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

        private void ShowReportGrid()
        {
            InitializeReportGrid();
            ShowpnlGO();
            FillReportGrid();
        }
        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    ShowViewForm(selectedID, ViewMode.Current);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
            else if (e.KeyCode == Keys.Left)
                fromDate1.Focus();
        }
        #endregion Events

        #region ToolTip
        private void AddToolTip()
        {
            try
            {
                //ttPurchasePartyBills.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                //ttPurchasePartyBills.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion ToolTip
    }
}
