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
using EcoMart.InterfaceLayer;
using EcoMart.Printing;
using PrintDataGrid;

namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSaleListDoctorCompany : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;      
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        #endregion

        # region Constructor
        public UclSaleListDoctorCompany()
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
                headerLabel1.Text = "SALE DOCTOR/COMPANY - SUMMARY";              
                FillDoctorCombo();
                ClearControls();               
                AddToolTip();               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void SetFocus()
        {
            base.SetFocus();
            FromDate.Focus();
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
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead(_MFromDate,_MToDate);
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    double mamt = 0;
                    if (dr.Cells["Col_CompID"].Value != null)
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
                            PrintHead(_MFromDate,_MToDate);
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        if (dr.Cells["Col_CompName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_SrNo"].Value.ToString().PadRight(30), PrintRowPixel, 10, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_CompName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_CompName"].Value.ToString().PadRight(30), PrintRowPixel, 100, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                       
                        if (dr.Cells["Col_Amount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(550.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix+40, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                }
                    
                }
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                mlen = (_MTotalAmount.ToString("#0.00").Length);
                colpix = Convert.ToInt32(550.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_MTotalAmount.ToString("#0.00"), PrintRowPixel, colpix+40, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintFooter(PrintRowPixel);
                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private int PrintHead(string _MFromDate, string _MToDate)
        {
            PrintRow row;
            try
            {
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(_MFromDate), General.GetDateInDateFormat(_MToDate));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow("Sr No", PrintRowPixel, 10, PrintFont);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Company", PrintRowPixel, 100, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 620, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;

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
        private int PrintFooter(int PrintRowPixel) // [07.02.2017]
        {
            try
            {
                PrintRowPixel = GeneralReport.PrintFooter(PrintTotalPages, PrintFont, PrintRowPixel, PrintPageNumber);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 0;
            return PrintRowCount;
        }

        #endregion

        #region Other Private methods

        public void ClearControls()
        {
            try
            {
                FromDate.Value = DateTime.Now;
                ToDate.Value = DateTime.Now;
                mcbDoctor.SelectedID = "";
                lblFooterMessage.Text = "";
                txtReportTotalAmount.Text = "";
                InitializeReportGrid();
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
                column.DataPropertyName = "DoctorID";
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SrNo";
                column.HeaderText = "Sr No";
                column.Visible = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompID";
                column.DataPropertyName = "ProdCompID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompName";
                column.DataPropertyName = "CompName";
                column.HeaderText = "Company";
                column.Width = 400;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 400;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
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

        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            dgvReportList.InitializeColumnContextMenu();
        }
        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
        }
        public void HidepnlGO()
        { 
            
            pnlMultiSelection.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Text = "";
            ViewToDate.Text = "";         
            FromDate.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }       
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            dgvReportList.Focus();
        }
        private void FillReportGrid()
        {
            try
            {
                int x = 0;
                FillReportData();
                dgvReportList.DataSource = _BindingSource;               
                dgvReportList.DateColumnNames.Add("Col_VoucherDate");
                dgvReportList.DoubleColumnNames.Add("Col_Amount");
                dgvReportList.Bind();
                _MTotalAmount = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                }
                for (int i = 0; i < _BindingSource.Rows.Count; i++)
                {
                    x++;
                    dgvReportList.Rows[i].Cells["Col_SrNo"].Value = x;
                }
                txtReportTotalAmount.Text = _MTotalAmount.ToString("#0.00");
              

                NoofRows();
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
               _MFromDate = FromDate.Value.Date.ToString("yyyyMMdd");
               _MToDate =  ToDate.Value.Date.ToString("yyyyMMdd");
                if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "")
                    dtable = _SaleList.GetSaleDataDoctorCompanySummary(mcbDoctor.SelectedID, _MFromDate, _MToDate);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillDoctorCombo()
        {
            try
            {
                mcbDoctor.SelectedID = null;
                mcbDoctor.SourceDataString = new string[4] { "DocID", "DocName", "DocAddress", "DocShortNameAddress" };
                mcbDoctor.ColumnWidth = new string[4] { "0", "200", "300", "0" };
                mcbDoctor.ValueColumnNo = 0;
                mcbDoctor.UserControlToShow = new UclDoctor();
                Doctor _Doctor = new Doctor();
                DataTable dtabled = _Doctor.GetSSDoctorsList();
                mcbDoctor.FillData(dtabled);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
       

       

        #endregion

        # region Events     
        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            try
            {
                InitializeReportGrid();               
                _MFromDate = FromDate.Value.Date.ToString("yyyyMMdd");
                _MToDate = ToDate.Value.Date.ToString("yyyyMMdd"); 
                 retValue = General.CheckDates(_MFromDate, _MToDate);
                 if (retValue)
                 {
                     this.Cursor = Cursors.WaitCursor;
                     txtViewText.Text = mcbDoctor.SeletedItem.ItemData[1].ToString();                    
                     FillReportGrid();
                     ShowpnlGO();
                    PrintReportHead = "Sale Doctor Company Summary";  // From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                     PrintReportHead2 = "[" + mcbDoctor.SeletedItem.ItemData[1].ToString() + "]";
                     this.Cursor = Cursors.Default;
                     dgvReportList.Focus();
                 }
                 else
                     lblFooterMessage.Text = "Please Check Date...";

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ToDate.Focus();
        }

        private void ToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbDoctor.Focus();
            if (e.KeyCode == Keys.Up)
                FromDate.Focus();
        }

        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                string selectedID = dgvReportList.SelectedRow.Cells[1].Value.ToString();
                ReportControl = new UclSaleListDoctor();
                this.Cursor = Cursors.Default;
                ShowReportForm ( mcbDoctor.SelectedID, selectedID, _MFromDate, _MToDate);
                
            }

        }   
        #endregion

        #region ToolTip
        private void AddToolTip()
        {
            try
            {             
                ttToolTip.SetToolTip(pnlMultiSelection, "F12 to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion ToolTip

       

    }
}
