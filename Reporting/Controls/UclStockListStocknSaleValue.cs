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
    public partial class UclStockListStocknSaleValue : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private SsStock _SsStock;
        private string _MFromDate;
        private string _MToDate;
        private int _MTotalRows;
        # endregion

        # region Constructor
        public UclStockListStocknSaleValue()
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
        # endregion

        #region IOverview Members

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _SsStock = new SsStock();
                headerLabel1.Text = "STOCK-STOCK AND SALE";
                ClearControls();
                FillCompanyCombo();
                HidepnlGO();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
                tsbtnPrint.Enabled = false;
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
                int totalrows = _MTotalRows;
                totalrows = CalculateTotalRows(totalrows);
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                PrintIfFirstRow = "Y";
                double mamt = 0;
                int mcolpix = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
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
                        PrintIfFirstRow = "Y";
                    }

                    if (dr.Visible)
                    {
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        mamt = 0;
                        if (dr.Cells["Col_ProductName"].Value != null)
                        {
                            {
                                row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_ProductLoosePack"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_ProductLoosePack"].Value.ToString().PadRight(15), PrintRowPixel, 200, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_ProductPack"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_ProductPack"].Value.ToString().PadLeft(6), PrintRowPixel, 250, PrintFont);
                                PrintBill.Rows.Add(row);
                            }

                            mamt = 0;
                            mcolpix = 310;

                            if (dr.Cells["Col_OpeningStock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_OpeningStock"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(mcolpix + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            mcolpix += 65;

                            if (dr.Cells["Col_purstock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_purstock"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(mcolpix + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            mcolpix += 65;

                            if (dr.Cells["Col_cnstistock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_cnstistock"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(mcolpix + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            mcolpix += 65;

                            if (dr.Cells["Col_totstock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_totstock"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(mcolpix + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            mcolpix += 65;

                            if (dr.Cells["Col_salestock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_salestock"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(mcolpix + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            mcolpix += 65;

                            if (dr.Cells["Col_dnstostock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_dnstostock"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(mcolpix + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            mcolpix += 65;


                            if (dr.Cells["Col_ClosingStock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_ClosingStock"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(mcolpix + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);

                        }
                    }
                }
                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public int CalculateTotalRows(int totrows)
        {
            return totrows;
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

                PrintRowPixel += 13;

                row = new PrintRow("Product", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("UOM", PrintRowPixel, 200, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pack", PrintRowPixel, 250, PrintFont);
                PrintBill.Rows.Add(row);
                int colpix = 330;

                row = new PrintRow("OP Stock", PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                colpix = colpix + 64;


                row = new PrintRow("PUR Stock", PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                colpix = colpix + 64;


                row = new PrintRow("CN/IN ", PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                colpix = colpix + 64;


                row = new PrintRow("Tot IN", PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                colpix = colpix + 64;



                row = new PrintRow("Sale Stock", PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                colpix = colpix + 64;

                row = new PrintRow("DN/OUT ", PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                colpix = colpix + 64;


                row = new PrintRow("Cl Stock", PrintRowPixel, colpix, PrintFont);
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

        #endregion IReportMember

        # region Other Private Methods
        public void ClearControls()
        {
            try
            {
                InitializeReportGrid();
                InitializeDates();
                lblFooterMessage.Text = "";
                mcbCompany.SelectedID = "";
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
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            FormatReportGrid();
            dgvReportList.InitializeColumnContextMenu();
        }

        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Debit");
            dgvReportList.DoubleColumnNames.Add("Col_Credit");
            dgvReportList.OptionalColumnNames.Add("Col_Debit");
            dgvReportList.OptionalColumnNames.Add("Col_Credit");
        }
        public void HidepnlGO()
        {
            pnlMultiSelection.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Visible = false;
            ViewToDate.Visible = false;
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
            ViewFromDate.Visible = true;
            ViewToDate.Visible = true;
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
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OpeningStock";
                column.Visible = false;
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_opstock";
                column.Width = 50;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Totopstock";
                column.HeaderText = "OPStk";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OpstockValue";
                column.HeaderText = "OPStk Value";
                column.Width = 85;
                column.DefaultCellStyle.BackColor = Color.AliceBlue;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_purstock";
                column.HeaderText = "PUR Stock";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_cnstistock";
                column.HeaderText = "CN/STI ";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurStkValue";
                column.HeaderText = "PStk Value";
                column.Width = 85;
                column.DefaultCellStyle.BackColor = Color.AliceBlue;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_salestock";
                column.HeaderText = "SALE Stock";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_dnstostock";
                column.HeaderText = "DN/STO ";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_salestockValue";
                column.HeaderText = "SStk Value";
                column.Width = 85;
                column.DefaultCellStyle.BackColor = Color.AliceBlue;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.HeaderText = "CL Stock";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStockValue";
                column.HeaderText = "CLStk Value";
                column.Width = 85;
                column.DefaultCellStyle.BackColor = Color.AliceBlue;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.Visible = false;
                column.Width = 85;
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
              //  CheckFilter();
                NoofRows();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
            int dropeningstock = 0;
            double dropstkvalue = 0;
            double drpurstkvalue = 0;
            double drslstkvalue = 0;           
            string mvoudate = "";
            double opstkvalue = 0;
            double mpurstkvalue = 0;
            double mslstkvalue = 0; 
            try
            {

                dtable = _SsStock.GetOverViewStocknSale(mcbCompany.SelectedID);
                _BindingSource = dtable;
                
                BindReportGrid();
                // opening stock
                try
                {
                    dtable = _SsStock.GetOpeningStockForStocknSale(mcbCompany.SelectedID);
                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        dtrow = dtable.Rows[i];
                        curproduct = dtrow["ProductID"].ToString();
                        mqty = 0;
                        dropstkvalue = 0;
                        drpurstkvalue = 0;
                        drslstkvalue = 0;                       
                        opstkvalue = 0;
                        mpurstkvalue = 0;
                        mslstkvalue = 0;                       
                        if (dtrow["OpeningStock"] != DBNull.Value && dtrow["OpeningStock"].ToString() != string.Empty)
                            mqty = Convert.ToInt16(dtrow["OpeningStock"].ToString());
                        if (dtrow["OpstkValue"] != DBNull.Value && dtrow["OpstkValue"].ToString() != string.Empty)
                            opstkvalue = Convert.ToDouble(dtrow["OpstkValue"].ToString());
                        foreach (DataGridViewRow dr in dgvReportList.Rows)
                        {
                            if (dr.Cells["Col_ID"] != null && dr.Cells["Col_ID"].Value.ToString() == curproduct)
                            {
                                dr.Cells["Col_OpeningStock"].Value = mqty.ToString("#0");
                                dr.Cells["Col_OpstockValue"].Value = opstkvalue.ToString("#0.00");
                                // dr.Cells["Col_OpeningStockValue"].Value = 
                                break;
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

                // Purchase stock
                dtable = _SsStock.GetPurchaseStockForStocknSale(_MToDate, mcbCompany.SelectedID);

                try
                {
                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        dtrow = dtable.Rows[i];
                        mqty = 0;
                        mscm = 0;
                        mrepl = 0;                       
                        mpurstkvalue = 0; 
                        curproduct = dtrow["ProductID"].ToString();
                        if (dtrow["Quantity"] != DBNull.Value && dtrow["Quantity"].ToString() != string.Empty)
                            mqty = Convert.ToInt16(dtrow["Quantity"].ToString());
                        if (dtrow["SchemeQuantity"] != DBNull.Value && dtrow["SchemeQuantity"].ToString() != string.Empty)
                            mscm = Convert.ToInt16(dtrow["SchemeQuantity"].ToString());
                        if (dtrow["ReplacementQuantity"] != DBNull.Value && dtrow["ReplacementQuantity"].ToString() != string.Empty)
                            mrepl = Convert.ToInt16(dtrow["ReplacementQuantity"].ToString());
                        if (dtrow["VoucherDate"] != DBNull.Value)
                            mvoudate = dtrow["VoucherDate"].ToString();
                        if (dtrow["PurstkValue"] != DBNull.Value && dtrow["PurstkValue"].ToString() != string.Empty)
                            mpurstkvalue = Convert.ToDouble(dtrow["PurstkValue"].ToString());
                        foreach (DataGridViewRow dr in dgvReportList.Rows)
                        {
                            dropstk = 0;
                            drpurstk = 0;
                            dropstkvalue = 0;
                            drpurstkvalue = 0;                           
                            if (dr.Cells["Col_ID"] != null && dr.Cells["Col_ID"].Value.ToString() == curproduct)
                            {
                                if (dr.Cells["Col_opstock"].Value != null && dr.Cells["Col_opstock"].Value.ToString() != string.Empty)
                                    dropstk = Convert.ToInt16(dr.Cells["Col_opstock"].Value.ToString());
                                if (dr.Cells["Col_purstock"].Value != null && dr.Cells["Col_purstock"].Value.ToString() != string.Empty)
                                    drpurstk = Convert.ToInt16(dr.Cells["Col_purstock"].Value.ToString());
                                if (dr.Cells["Col_OpstockValue"].Value != null && dr.Cells["Col_OpstockValue"].Value.ToString() != string.Empty)
                                    dropstkvalue = Convert.ToDouble(dr.Cells["Col_OpstockValue"].Value.ToString());
                                if (dr.Cells["Col_purstkvalue"].Value != null && dr.Cells["Col_purstkvalue"].Value.ToString() != string.Empty)
                                    drpurstkvalue = Convert.ToDouble(dr.Cells["Col_purstkvalue"].Value.ToString());

                                if (Convert.ToInt32(mvoudate) < Convert.ToInt32(_MFromDate))
                                {
                                    dr.Cells["Col_opstock"].Value = (dropstk + mqty + mscm + mrepl).ToString();
                                    dr.Cells["Col_OpstockValue"].Value = (dropstkvalue + mpurstkvalue).ToString("#0.00");
                                }
                                else
                                {
                                    dr.Cells["Col_purstock"].Value = (drpurstk + mqty + mscm + mrepl).ToString("#0");
                                    dr.Cells["Col_purstkvalue"].Value = (drpurstkvalue + mpurstkvalue).ToString("#0.00");
                                }
                                break;
                            }

                        }
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }


                dtable = _SsStock.GetSaleStockForStocknSale(_MToDate, mcbCompany.SelectedID);

                try
                {

                    if (dtable != null)
                    {
                        for (int i = 0; i < dtable.Rows.Count; i++)
                        {
                            mqty = 0;
                            mscm = 0;
                            mrepl = 0;
                            mslstkvalue = 0;
                            dtrow = dtable.Rows[i];
                            curproduct = dtrow["ProductID"].ToString();
                            if (dtrow["Quantity"] != DBNull.Value)
                                mqty = Convert.ToInt16(dtrow["Quantity"].ToString());
                            if (dtrow["SchemeQuantity"] != DBNull.Value)
                                mscm = Convert.ToInt16(dtrow["SchemeQuantity"].ToString());
                            if (dtrow["VoucherDate"] != DBNull.Value)
                                mvoudate = dtrow["VoucherDate"].ToString();
                            if (dtrow["Amount"] != DBNull.Value && dtrow["Amount"].ToString() != string.Empty)
                                mslstkvalue = Convert.ToDouble(dtrow["Amount"].ToString());
                            foreach (DataGridViewRow dr in dgvReportList.Rows)
                            {
                                if (dr.Cells["Col_ID"] != null && dr.Cells["Col_ID"].Value.ToString() == curproduct)
                                {
                                    dropstk = 0;
                                    drsalestk = 0;
                                    dropstkvalue = 0;
                                    drslstkvalue = 0;
                                    if (dr.Cells["Col_opstock"].Value != null)
                                        dropstk = Convert.ToInt16(dr.Cells["Col_opstock"].Value.ToString());
                                    if (dr.Cells["Col_salestock"].Value != null)
                                        drsalestk = Convert.ToInt16(dr.Cells["Col_salestock"].Value.ToString());
                                    if (dr.Cells["Col_OpstockValue"].Value != null && dr.Cells["Col_OpstockValue"].Value.ToString() != string.Empty)
                                        dropstkvalue = Convert.ToDouble(dr.Cells["Col_OpstockValue"].Value.ToString());
                                    if (dr.Cells["Col_salestockValue"].Value != null && dr.Cells["Col_salestockValue"].Value.ToString() != string.Empty)
                                        drslstkvalue = Convert.ToDouble(dr.Cells["Col_salestockValue"].Value.ToString());


                                    if (Convert.ToInt32(mvoudate) < Convert.ToInt32(_MFromDate))
                                    {
                                        dr.Cells["Col_opstock"].Value = (dropstk - mqty - mscm).ToString();
                                        dr.Cells["Col_OpstockValue"].Value = (dropstkvalue - mslstkvalue).ToString("#0.00");
                                    }
                                    else
                                    {
                                        dr.Cells["Col_salestock"].Value = (drsalestk + mqty + mscm).ToString();
                                        dr.Cells["Col_salestockValue"].Value = (drslstkvalue + mslstkvalue).ToString("#0.00");

                                    }

                                    break;
                                }

                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
                dtable = _SsStock.GetCRSTIStockForStocknSale(_MToDate, mcbCompany.SelectedID);

                try
                {
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
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
                dtable = _SsStock.GetDBSTOStockForStocknSale(_MToDate, mcbCompany.SelectedID);
                try
                {
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
                                drdbstk = 0;
                                if (dr.Cells["Col_ID"] != null && dr.Cells["Col_ID"].Value.ToString() == curproduct)
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
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
                try
                {
                    double mmrp = 0;
                    double muom = 0;
                    foreach (DataGridViewRow dr in dgvReportList.Rows)
                    {
                        dropeningstock = 0;
                        dropstk = 0;
                        drpurstk = 0;
                        drsalestk = 0;
                        drcrstk = 0;
                        drdbstk = 0;
                        dropstkvalue = 0;
                        drpurstkvalue = 0;
                        drslstkvalue = 0;
                        mmrp = 0;
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
                     
                        if (dr.Cells["Col_MRP"].Value != null && dr.Cells["Col_MRP"].Value.ToString() != "")
                            mmrp = Convert.ToDouble(dr.Cells["Col_MRP"].Value.ToString());
                        if (dr.Cells["Col_ProductLoosePack"].Value != null && dr.Cells["Col_ProductLoosePack"].Value.ToString() != "")
                            muom = Convert.ToDouble(dr.Cells["Col_ProductLoosePack"].Value.ToString());

                        if (dr.Cells["Col_OpstockValue"].Value != null && dr.Cells["Col_OpstockValue"].Value.ToString() != "")
                            dropstkvalue = Convert.ToDouble(dr.Cells["Col_OpstockValue"].Value.ToString());
                        if (dr.Cells["Col_PurStkValue"].Value != null && dr.Cells["Col_PurStkValue"].Value.ToString() != "")
                            drpurstkvalue = Convert.ToDouble(dr.Cells["Col_PurStkValue"].Value.ToString());
                        if (dr.Cells["Col_salestockValue"].Value != null && dr.Cells["Col_salestockValue"].Value.ToString() != "")
                            drslstkvalue = Convert.ToDouble(dr.Cells["Col_salestockValue"].Value.ToString());

                       // dr.Cells["Col_TotStock"].Value = (dropeningstock + dropstk + drpurstk + drcrstk).ToString("#0.00");
                        dr.Cells["Col_ClosingStock"].Value = (dropeningstock + dropstk + drpurstk + drcrstk - drsalestk - drdbstk).ToString("#0");
                        dr.Cells["Col_ClosingStockValue"].Value = ((dropeningstock + dropstk + drpurstk + drcrstk - drsalestk - drdbstk) * (mmrp/muom)).ToString("#0.00");
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
            double mmrp = 0;
            foreach (DataRow dr in _BindingSource.Rows)
            {
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.Cells["Col_ID"].Value = dr["ProductID"].ToString();
                currentdr.Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                currentdr.Cells["Col_ProductLoosePack"].Value = dr["ProdLoosePack"].ToString();
                currentdr.Cells["Col_ProductPack"].Value = dr["ProdPack"].ToString();
                if (dr["MRP"] != DBNull.Value && dr["MRP"].ToString() != string.Empty)
                    mmrp = Convert.ToDouble(dr["MRP"].ToString());
                currentdr.Cells["Col_MRP"].Value = mmrp.ToString("#0.00");
                //     currentdr.Cells["Col_OpeningStock"].Value = dr["ProdOpeningStock"].ToString();
            }

        }

        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            try
            {
                InitializeReportGrid();
                ShowpnlGO();
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FillReportGrid();
                    ShowpnlGO();
                    PrintReportHead = "Stock & Sale Value  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = "[" + mcbCompany.SeletedItem.ItemData[1].ToString() + "]";
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
            string strmessage = General.NoofRows(_MTotalRows);
            lblFooterMessage.Text = strmessage;
        }

        #endregion Other Private Methods

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
        private void mcbCompany_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbCompany.SelectedID != null && mcbCompany.SelectedID != "")
                btnOKMultiSelectionClick();
        }

        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                ReportControl = new UclStockListProductLedger();
                ShowReportForm(selectedID, "", _MFromDate, _MToDate);
                this.Cursor = Cursors.Default;
            }
        }
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = F10");
                ttToolTip.SetToolTip(pnlMultiSelection, "F12 to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion
    }
}
