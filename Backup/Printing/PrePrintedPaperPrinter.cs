using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintDataGrid;
using PharmaSYSRetailPlus.Common;
using System.Windows.Forms;

namespace PharmaSYSRetailPlus.Printing
{
    public class PrePrintedPaperPrinter
    {
        public string IFSaleBill = "";
        public PrePrintedPaperPrinter()
        {

        }

        public void Print(string BillType, string BillNo, string BillDate, string PatientName, string PatientAddress,string PatientTelephone, string DoctorName, string DoctorAddress, DataGridViewRowCollection Rows, string Narration, double NetAmount, string SaleSubType, double DiscountAmount, double CNAmount, double DNAmount, double GrossAmount, double BalanceAmount)
        {
            PrintRow row;
            int rowcount = 0;
            int PrintRowPixel = 0;
            int CurrentPageNo = 1;
            GeneralSettings generalPrintSettings;
            IFSaleBill = "S";
            try
            {
                if (BillType == FixAccounts.VoucherTypeForCashSale || BillType == FixAccounts.VoucherTypeForCreditSale || BillType == FixAccounts.VoucherTypeForCreditStatementSale || BillType == FixAccounts.VoucherTypeForVoucherSale)
                    IFSaleBill = "S";
                else if (BillType == FixAccounts.VoucherTypeForCreditNoteStock)
                    IFSaleBill = "C";
                else if (BillType == FixAccounts.VoucherTypeForDebitNoteStock)
                    IFSaleBill = "D";
                else if (BillType == FixAccounts.VoucherTypeForStockIN)
                    IFSaleBill = "I";
                else if (BillType == FixAccounts.VoucherTypeForStockOut)
                    IFSaleBill = "O";

                if (IFSaleBill == "S")
                    PrintFactory.SendReverseLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.ReverseLineFeed);
                else
                    PrintFactory.SendReverseLineFeed(General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.GeneralSettings.ReverseLineFeed);
               
                PrintBill.Rows.Clear();
                if (IFSaleBill == "S")
                    generalPrintSettings = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings;
                else
                    generalPrintSettings = General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.GeneralSettings;
                double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(Rows.Count) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                int totalpages = Convert.ToInt32(totpages);

                PrintHeader(CurrentPageNo, totalpages, BillType, BillNo, BillDate, PatientName, PatientAddress, DoctorName, DoctorAddress, SaleSubType);

              

                PrintRowPixel = generalPrintSettings.ContentStartRow;
                PageFooter PageFooter;
                foreach (DataGridViewRow dr in Rows)
                {

                    if (dr.Cells["Col_ProductID"].Value != null)
                    {
                        if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                        {
                            if (IFSaleBill == "S")
                            {
                                PageFooter = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.PageFooter;
                            }
                            else
                            {
                                PageFooter = General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.PageFooter;
                            }

                            row = new PrintRow(PageFooter.ContinueItem.Text, PageFooter.ContinueItem.Row, PageFooter.ContinueItem.Column, PageFooter.ContinueItem.Font);
                            PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight);
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            CurrentPageNo += 1;
                            PrintHeader(CurrentPageNo, totalpages, BillType, BillNo, BillDate, PatientName, PatientAddress, DoctorName, DoctorAddress, SaleSubType);
                            rowcount = 0;
                            PrintRowPixel = generalPrintSettings.ContentStartRow;
                        }
                        PrintRowPixel += 17;
                        rowcount += 1;
                        int colpix = 1;
                        int mqty = 0;
                        int mlen = 0;
                        double mamt = 0;
                        string mproductname = "";
                        PharmaSYSRetailPlus.Printing.PageContent PageContent;
                        if (IFSaleBill == "S")
                        {
                           PageContent = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.PageContent;
                        }
                        else
                        {
                            PageContent = General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.PageContent;
                        }
                        for (int i = 0; i < PageContent.ColumnCount; i++)
                        {
                            PharmaSYSRetailPlus.Printing.PageColumn column;
                            column = PageContent.Columns[i];
                            if (column.Show)
                            {
                                if (column.ColumnDataType == PharmaSYSRetailPlus.Printing.ColumnDataType.Integer)
                                {
                                    if (dr.Cells[column.ColumnDataField].Value != null && Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString()) != 0)
                                    {
                                        mqty = Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString());
                                        mlen = (mqty.ToString("#0").Length);
                                        colpix = Convert.ToInt32(column.Column + ((5 - Convert.ToInt32(mlen)) * 5.5));
                                        string pack = dr.Cells["Col_Pack"].Value.ToString().PadRight(3).Substring(0, 3);
                                        row = new PrintRow(mqty.ToString("#0") + " X " + pack, PrintRowPixel, colpix, column.Font);
                                        PrintBill.Rows.Add(row);
                                    }
                                }
                                else if (column.ColumnDataType == PharmaSYSRetailPlus.Printing.ColumnDataType.Decimal)
                                {
                                    if (dr.Cells[column.ColumnDataField].Value != null && Convert.ToDouble(dr.Cells[column.ColumnDataField].Value.ToString()) != 0)
                                    {
                                        mamt = Convert.ToDouble(dr.Cells[column.ColumnDataField].Value.ToString());
                                        mlen = (mamt.ToString("#0.00").Length);
                                        colpix = Convert.ToInt32(column.Column - 30 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, column.Font);
                                        PrintBill.Rows.Add(row);
                                    }
                                }
                                else
                                {
                                    if (dr.Cells[column.ColumnDataField].Value != DBNull.Value && dr.Cells[column.ColumnDataField].Value.ToString() != "")
                                    {
                                        string ss = (column.ColumnDataField).ToString();
                                        if (ss == "Col_ProductName")
                                        {
                                            mproductname = dr.Cells["Col_ProductName"].Value.ToString();
                                            if (mproductname.Length < 20)
                                                mproductname = mproductname.PadRight(20);
                                            else
                                                mproductname = mproductname.Substring(0, 20);
                                            if (IFSaleBill == "S")
                                            {
                                                if (dr.Cells["Col_ProdScheduleDrugCode"].Value != null && dr.Cells["Col_ProdScheduleDrugCode"].Value.ToString() == "H1")
                                                    mproductname = mproductname + "(H1)";
                                            }
                                            colpix = column.Column;
                                            row = new PrintRow(mproductname, PrintRowPixel, colpix, column.Font);
                                            PrintBill.Rows.Add(row);
                                        }
                                        else
                                        {
                                            colpix = column.Column;
                                            row = new PrintRow(dr.Cells[column.ColumnDataField].Value.ToString(), PrintRowPixel, colpix, column.Font);
                                            PrintBill.Rows.Add(row);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }//End of For each Rows

                PrintFooter(Narration, NetAmount, DiscountAmount, CNAmount, DNAmount, GrossAmount, BalanceAmount);
                              

                PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight);
                if (IFSaleBill == "S")
                {
                    PrintFactory.SendLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.LineFeed);
                }
                else
                {
                    PrintFactory.SendLineFeed(General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.GeneralSettings.LineFeed);
                }
              
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        private void PrintHeader(int currentPageNo, int TotalPages, string BillType, string BillNo, string BillDate, string PatientName, string PatientAddress, string DoctorName, string DoctorAddress, string SaleSubType)
        {
            PrintRow row;
            PharmaSYSRetailPlus.Printing.PageHeader PageHeader;
            try
            {  
               if (SaleSubType == string.Empty)
                    SaleSubType = " ";
                //PrintFactory.SendReverseLineFeed(6);
               if (IFSaleBill == "S")
               {
                   PageHeader = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.PageHeader;
               }
               else
               {
                   PageHeader = General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.PageHeader;
               }

                if (PageHeader.ShopName.Show)
                {
                    row = new PrintRow(General.ShopDetail.ShopName, PageHeader.ShopName.Row, PageHeader.ShopName.Column, PageHeader.ShopName.Font);
                    PrintBill.Rows.Add(row);
                }
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PageHeader.Time.Row, PageHeader.Time.Column, PageHeader.Time.Font);
                PrintBill.Rows.Add(row);
                if (PageHeader.ShopAddress1.Show)
                {
                    row = new PrintRow(General.ShopDetail.ShopAddress1.Trim() + " " + General.ShopDetail.ShopAddress2.Trim(), PageHeader.ShopAddress1.Row, PageHeader.ShopAddress1.Column, PageHeader.ShopAddress1.Font);
                    PrintBill.Rows.Add(row);
                }
                if (IFSaleBill == "S")
                {
                    if (BillType == FixAccounts.VoucherTypeForCashSale)
                        row = new PrintRow(PageHeader.VoucherTypeSCA.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCA.Row, PageHeader.VoucherTypeSCA.Column, PageHeader.VoucherTypeSCA.Font);
                    else if (BillType == FixAccounts.VoucherTypeForCreditStatementSale)
                        row = new PrintRow(PageHeader.VoucherTypeSCS.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCS.Row, PageHeader.VoucherTypeSCS.Column, PageHeader.VoucherTypeSCS.Font);
                    else
                        row = new PrintRow(PageHeader.VoucherTypeSCR.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCR.Row, PageHeader.VoucherTypeSCR.Column, PageHeader.VoucherTypeSCR.Font);
                }
                else
                {
                    if (IFSaleBill == "C")
                        row = new PrintRow(PageHeader.VoucherTypeCreditNote.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeCreditNote.Row, PageHeader.VoucherTypeCreditNote.Column, PageHeader.VoucherTypeCreditNote.Font);
                    else if (IFSaleBill == "D")
                        row = new PrintRow(PageHeader.VoucherTypeDebitNote.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeDebitNote.Row, PageHeader.VoucherTypeDebitNote.Column, PageHeader.VoucherTypeDebitNote.Font);
                }
                PrintBill.Rows.Add(row);

                if (PageHeader.ShopTelephone.Show)
                {
                    row = new PrintRow(General.ShopDetail.ShopTelephone.Trim(), PageHeader.ShopTelephone.Row, PageHeader.ShopTelephone.Column, PageHeader.ShopTelephone.Font);
                    PrintBill.Rows.Add(row);
                }
                row = new PrintRow(General.GetDateInShortDateFormat(BillDate), PageHeader.Date.Row, PageHeader.Date.Column, PageHeader.Date.Font);
                PrintBill.Rows.Add(row);
                row = new PrintRow(PatientName, PageHeader.PatientName.Row, PageHeader.PatientName.Column, PageHeader.PatientName.Font);
                PrintBill.Rows.Add(row);
                row = new PrintRow(PatientAddress, PageHeader.PatientAddress.Row, PageHeader.PatientAddress.Column, PageHeader.PatientAddress.Font);
                PrintBill.Rows.Add(row);
                row = new PrintRow(DoctorName.Trim(), PageHeader.DoctorName.Row, PageHeader.DoctorName.Column, PageHeader.DoctorName.Font);
                PrintBill.Rows.Add(row);
                row = new PrintRow(DoctorAddress.Trim(), PageHeader.DoctorAddress.Row, PageHeader.DoctorAddress.Column, PageHeader.DoctorAddress.Font);
                PrintBill.Rows.Add(row);
                string page = currentPageNo.ToString().Trim() + "/" + TotalPages.ToString().Trim();
                row = new PrintRow(page, PageHeader.PageNo.Row, PageHeader.PageNo.Column, PageHeader.PageNo.Font);
                PrintBill.Rows.Add(row);               

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }            
        }


        private void PrintFooter(string Narration, double NetAmount, double DiscountAmount, double CNAmount, double DNAmount, double GrossAmount, double BalanceAmount)
        {
            PrintRow row;
            PageFooter PageFooter;
            try
            {
                if (IFSaleBill == "S")
                {
                    PageFooter = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.PageFooter;
                }
                else
                {
                    PageFooter = General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.PageFooter;
                }
                if (PageFooter.GrossAmountItem.Show)
                {
                    row = new PrintRow(GrossAmount.ToString("#0.00"),PageFooter.GrossAmountItem.Row,PageFooter.GrossAmountItem.Column,PageFooter.GrossAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.NarrationItem.Show)
                {
                    row = new PrintRow(Narration, PageFooter.NarrationItem.Row, PageFooter.NarrationItem.Column, PageFooter.NarrationItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.DiscountItem.Show && DiscountAmount > 0)
                {
                    row = new PrintRow(PageFooter.DiscountItem.Text+ DiscountAmount.ToString("#0.00"), PageFooter.DiscountItem.Row, PageFooter.DiscountItem.Column, PageFooter.DiscountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.CreditNoteItem.Show && CNAmount > 0)
                {
                    row = new PrintRow(PageFooter.CreditNoteItem.Text+ CNAmount.ToString("#0.00"), PageFooter.CreditNoteItem.Row, PageFooter.CreditNoteItem.Column, PageFooter.CreditNoteItem.Font);
                    PrintBill.Rows.Add(row);
                }

                if (PageFooter.DebitNoteItem.Show && DNAmount > 0)
                {
                    row = new PrintRow(PageFooter.DebitNoteItem.Text+ DNAmount.ToString("#0.00"), PageFooter.DebitNoteItem.Row, PageFooter.DebitNoteItem.Column, PageFooter.DebitNoteItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.BalanceAmountItem.Show && BalanceAmount > 0)
                {
                    row = new PrintRow(PageFooter.BalanceAmountItem.Text + BalanceAmount.ToString("#0.00"), PageFooter.BalanceAmountItem.Row, PageFooter.BalanceAmountItem.Column, PageFooter.BalanceAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.NetAmountItem.Show)
                {
                    row = new PrintRow(NetAmount.ToString("#0.00"), PageFooter.NetAmountItem.Row, PageFooter.NetAmountItem.Column, PageFooter.NetAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }  
        }

        

    }
}
