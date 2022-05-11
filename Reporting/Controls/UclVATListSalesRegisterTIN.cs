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
using System.IO;

namespace PharmaSYSDistributorPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclVATListSalesRegisterTIN : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        private double _MTotalVAT;
        private double _MTotalVAT5;
        private double _MTotalVAT12;
        #endregion

        # region Constructor
        public UclVATListSalesRegisterTIN()
        {
            try
            {
                InitializeComponent();
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
                _SaleList = new SaleList();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "VAT - SALES REGISTER TINWISE SUMMARY";
                ConstructReportColumns();
                dgvReportList.Columns["Col_ID"].Visible = false;
                ClearControls();
                HidepnlGO();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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

        public override void SetFocus()
        {
            base.SetFocus();
            fromDate1.Focus();
        }

        #endregion IOverview Members

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
                double mamt = 0;
                // totalrows += 7; // for first page heading
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_AccName"].Value != null)
                    {
                        if (PrintRowCount >= FixAccounts.NumberOfRowsPerReport)
                        {
                            PrintRowPixel += 34;
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
                        mamt = 0;

                        if (dr.Cells["Col_AccName"].Value.ToString() == "Total" )
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            PrintRowCount += 1;
                        }
                            row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(25).Substring(0,25), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                           
                       
                        if (dr.Cells["Col_TotalAmount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_TotalAmount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(235.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_TotalVAT"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_TotalVAT"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(335.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_5perVAT"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_5perVAT"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(435.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_12point5perVAT"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_12point5perVAT"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(535.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_TIN"].Value != null)
                            row = new PrintRow(dr.Cells["Col_TIN"].Value.ToString(), PrintRowPixel, 635, PrintFont);
                        PrintBill.Rows.Add(row);

                    }
                }
                PrintRowPixel += 17;
                PrintRowCount += 1;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;
                row = new PrintRow("Party", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 265, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Total VAT", PrintRowPixel, 365, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("6% VAT", PrintRowPixel, 465, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("13.5% VAT", PrintRowPixel, 565, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("TIN", PrintRowPixel, 665, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 0;
            return PrintRowCount;
        }


        #endregion IReportMember

        #region OtherPrivateMethods

        public void ClearControls()
        {
            try
            {
                InitializeReportGrid();
                InitializeDates();           
                lblFooterMessage.Text = "";              
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
        private void InitializeDates()
        {
            _MFromDate = General.ShopDetail.Shopsy;
            _MToDate = General.ShopDetail.Shopey;
            fromDate1.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
            toDate1.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);
        }
        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Visible = false;
            ViewToDate.Visible = false;
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            if (General.PharmaSYSDistributorPlusLicense.LicenseType == LicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }       
            ViewFromDate.Visible = true;
            ViewToDate.Visible = true;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
        }

        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";             
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";              
                column.HeaderText = "Party";
                column.Width = 250;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TotalAmount";              
                column.HeaderText = "Amount";
                column.Width = 150;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TotalVAT";             
                column.HeaderText = "VAT";
                column.Width = 150;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_5perVAT";
                column.HeaderText = "6%VAT";
                column.Width = 150;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_12point5perVAT";
                column.HeaderText = "13.5%VAT";
                column.Width = 150;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TIN";
                column.HeaderText = "TIN";
                column.Width = 250;
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
                FormatReportGrid();
                BindReportGrid();
                CalculateFinalTotals();
                NoofRows();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }       

        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _SaleList.GetDataForVATRegisterTIN(_MFromDate, _MToDate);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FormatReportGrid()
        {
            dgvReportList.DoubleColumnNames.Add("Col_TotalAmount");
            dgvReportList.DoubleColumnNames.Add("Col_TotalVAT");
           // dgvReportList.OptionalColumnNames.Add("Col_AccName");
           // dgvReportList.OptionalColumnNames.Add("Col_Address");
        }

        private void BindReportGrid()
        {
            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {
                        int rowIndex = dgvReportList.Rows.Add();
                        DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                        dgvrow.Cells["Col_ID"].Value = dr["ID"].ToString();
                        if (dr["AccVATTinNumber"] != DBNull.Value)
                            dgvrow.Cells["Col_TIN"].Value = dr["AccVATTinNumber"].ToString();
                        dgvrow.Cells["Col_TotalAmount"].Value = dr["TotalAmount"].ToString();
                        dgvrow.Cells["Col_TotalVAT"].Value = dr["TotalVAT"].ToString();
                        dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                        dgvrow.Cells["Col_5perVAT"].Value = dr["VAT5Per"].ToString();
                        dgvrow.Cells["Col_12point5perVAT"].Value = dr["VAT12point5Per"].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                    lblFooterMessage.Text = "";
                    InitializeReportGrid();
                    ShowpnlGO();                   
                    PrintReportHead = "VAT Sale Report [Party] From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = "";
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

        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;       
        }

        private void CalculateFinalTotals()
        {
            _MTotalAmount = 0;
            _MTotalVAT = 0;
            _MTotalVAT5 = 0;
            _MTotalVAT12 = 0;
            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {
                _MTotalAmount += Convert.ToDouble(dr.Cells["Col_TotalAmount"].Value.ToString());
                _MTotalVAT += Convert.ToDouble(dr.Cells["Col_TotalVAT"].Value.ToString());
                _MTotalVAT5 += Convert.ToDouble(dr.Cells["Col_5PerVAT"].Value.ToString());
                _MTotalVAT12 += Convert.ToDouble(dr.Cells["Col_12point5PerVAT"].Value.ToString());
            }
            int rowIndex = dgvReportList.Rows.Add();
            DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
            dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
            dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
            dgvrow.Cells["Col_AccName"].Value = "Total";
            dgvrow.Cells["Col_TotalVAT"].Value = _MTotalVAT.ToString("#0.00");
            dgvrow.Cells["Col_TotalAmount"].Value = _MTotalAmount.ToString("#0.00");
            dgvrow.Cells["Col_5PerVAT"].Value = _MTotalVAT5.ToString("#0.00");
            dgvrow.Cells["Col_12point5PerVAT"].Value = _MTotalVAT12.ToString("#0.00");
        }       

        #endregion

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
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
        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion

       
    
    }
}
