using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintDataGrid;
using EcoMart.Common;
using System.Windows.Forms;

namespace EcoMart.Printing
{
    public class PlainPaperPrinter
    {
        public string IFSaleBill = "";
        GeneralSettings generalPrintSettings;
        public PlainPaperPrinter()
        {

        }
        public void Print(string BillType, string BillNo, string BillDate, string PatientName, string PatientAddress, string PatientTelephone, string DoctorName, string DoctorAddress, DataGridViewRowCollection Rows, string Narration, double NetAmount, string SaleSubType, double DiscountAmount, double CNAmount, double DNAmount, double GrossAmount, double BalanceAmount, string atow)
        {
            //System.Threading.Thread th = new System.Threading.Thread(() => PrintData(BillType, BillNo, BillDate, PatientName, PatientAddress, PatientTelephone, DoctorName, DoctorAddress, Rows, Narration, NetAmount, SaleSubType, DiscountAmount, CNAmount, DNAmount, GrossAmount, BalanceAmount, atow));
            //th.Start();
            PrintData(BillType, BillNo, BillDate, PatientName, PatientAddress, PatientTelephone, DoctorName, DoctorAddress, Rows, Narration, NetAmount, SaleSubType, DiscountAmount, CNAmount, DNAmount, GrossAmount, BalanceAmount, atow);
        }
        private void PrintData(string BillType, string BillNo, string BillDate, string PatientName, string PatientAddress, string PatientTelephone, string DoctorName, string DoctorAddress, DataGridViewRowCollection Rows, string Narration, double NetAmount, string SaleSubType, double DiscountAmount, double CNAmount, double DNAmount, double GrossAmount, double BalanceAmount, string atow)
        {
            for (int copies = 0; copies < General.CurrentSetting.MsetNumberOfBillsAtaTime; copies++)
            {
                PrintRow row;
                int rowcount = 1;
                int PrintRowPixel = 0;
                int CurrentPageNo = 1;

                //    GeneralSettings generalPrintSettings;
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
                    else if (BillType == FixAccounts.VoucherTypeForCashReceipt)
                        IFSaleBill = "R";
                    else if (BillType == FixAccounts.VoucherTypeForCashPayment)
                        IFSaleBill = "P";
                    else if (BillType == FixAccounts.VoucherTypeForBankReceipt)
                        IFSaleBill = "W";
                    else if (BillType == FixAccounts.VoucherTypeForBankPayment)
                        IFSaleBill = "B";

                    if (IFSaleBill == "S")
                        PrintFactory.SendReverseLineFeed(General.PrintSettings.SaleBillPrintSettingsPlainPaper.GeneralSettings.ReverseLineFeed);
                    else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I")
                        PrintFactory.SendReverseLineFeed(General.PrintSettings.DebitNotePrintSettingsPlainPaper.GeneralSettings.ReverseLineFeed);
                    else if (IFSaleBill == "O")
                        PrintFactory.SendReverseLineFeed(General.PrintSettings.StockOutPrintSettingsPlainPaper.GeneralSettings.ReverseLineFeed);
                    else
                        PrintFactory.SendReverseLineFeed(General.PrintSettings.CashBankPrintSettingsPlainPaper.GeneralSettings.ReverseLineFeed);
                    PrintBill.Rows.Clear();
                    if (IFSaleBill == "S")
                        generalPrintSettings = General.PrintSettings.SaleBillPrintSettingsPlainPaper.GeneralSettings;
                    else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I")
                        generalPrintSettings = General.PrintSettings.DebitNotePrintSettingsPlainPaper.GeneralSettings;
                    else if (IFSaleBill == "O")
                        generalPrintSettings = General.PrintSettings.StockOutPrintSettingsPlainPaper.GeneralSettings;
                    else
                        generalPrintSettings = General.PrintSettings.CashBankPrintSettingsPlainPaper.GeneralSettings;

                    double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(Rows.Count) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                    Int32 totalpages = Convert.ToInt32(totpages);


                    //for (int copies = 0; copies < General.CurrentSetting.MsetNumberOfBillsAtaTime; copies++)
                    //{
                    //    _copiesPrinted = copies;

                    PrintHeader(CurrentPageNo, totalpages, BillType, BillNo, BillDate, PatientName, PatientAddress, PatientTelephone, DoctorName, DoctorAddress, SaleSubType);


                    //if (General.CurrentSetting.MsetNumberOfBillsAtaTime == 2)
                    //{
                    //    if (_copiesPrinted == 0)
                    //        PrintRowPixel = generalPrintSettings.ContentStartRow;
                    //    else
                    //        PrintRowPixel = generalPrintSettings.ContentStartRow + 400;
                    //}
                    //else
                    PrintRowPixel = generalPrintSettings.ContentStartRow;
                    PageFooter PageFooter;
                    foreach (DataGridViewRow dr in Rows)
                    {

                        if (dr.Cells["Col_ProductID"].Value != null || dr.Cells["Col_Amount"].Value != null)
                        {
                            if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                            {
                                if (IFSaleBill == "S")
                                {
                                    PageFooter = General.PrintSettings.SaleBillPrintSettingsPlainPaper.PageFooter;
                                }
                                else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I")
                                {
                                    PageFooter = General.PrintSettings.DebitNotePrintSettingsPlainPaper.PageFooter;
                                }
                                else if (IFSaleBill == "O")
                                {
                                    PageFooter = General.PrintSettings.StockOutPrintSettingsPlainPaper.PageFooter;
                                }
                                else
                                {
                                    PageFooter = General.PrintSettings.CashBankPrintSettingsPlainPaper.PageFooter;
                                }
                                int PrintPix = PageFooter.ContinueItem.Row - 13;
                                PrintRowPixel += 15;
                                if (generalPrintSettings.PageWidth > 700)
                                    row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, General.FontRegular);
                                else
                                    row = new PrintRow(FixAccounts.DashLine60Normal, PrintPix, 1, General.FontRegular);
                                PrintBill.Rows.Add(row);

                                row = new PrintRow(PageFooter.ContinueItem.Text, PageFooter.ContinueItem.Row, PageFooter.ContinueItem.Column, PageFooter.ContinueItem.Font);
                                PrintBill.Rows.Add(row);
                                PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight);
                                PrintBill.Rows.Clear();
                                PrintRowPixel = 0;
                                CurrentPageNo += 1;
                                PrintHeader(CurrentPageNo, totalpages, BillType, BillNo, BillDate, PatientName, PatientAddress, PatientTelephone, DoctorName, DoctorAddress, SaleSubType);
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
                            EcoMart.Printing.PageContent PageContent;
                            if (IFSaleBill == "S")
                            {
                                PageContent = General.PrintSettings.SaleBillPrintSettingsPlainPaper.PageContent;
                            }
                            else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I")
                            {
                                PageContent = General.PrintSettings.DebitNotePrintSettingsPlainPaper.PageContent;
                            }
                            else if (IFSaleBill == "O")
                            {
                                PageContent = General.PrintSettings.StockOutPrintSettingsPlainPaper.PageContent;
                            }
                            else
                            {
                                PageContent = General.PrintSettings.CashBankPrintSettingsPlainPaper.PageContent;
                            }
                            for (int i = 0; i < PageContent.ColumnCount; i++)
                            {
                                EcoMart.Printing.PageColumn column;
                                column = PageContent.Columns[i];
                                if (column.Show == true)
                                {
                                    string ssss = column.ColumnDataField.ToString();
                                    if (column.ColumnDataType == EcoMart.Printing.ColumnDataType.Integer)
                                    {
                                        if (dr.Cells[column.ColumnDataField].Value != null && Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString()) != 0)
                                        {
                                            mqty = Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString());
                                            mlen = (mqty.ToString("#0").Length);
                                            colpix = Convert.ToInt32(column.Column + ((5 - Convert.ToInt32(mlen)) * 5.5));
                                            string pack = "";
                                            if (IFSaleBill == "S" || IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I" || IFSaleBill == "O")
                                                pack = dr.Cells["Col_Pack"].Value.ToString().PadRight(3).Substring(0, 3);

                                            if (IFSaleBill == "S" && dr.Cells["Col_IfMultipleMRP"].Value != null && dr.Cells["Col_IfMultipleMRP"].Value.ToString() != string.Empty && dr.Cells["Col_IfMultipleMRP"].Value.ToString() == "Y")
                                            {
                                                //TODO:Set Font for underline
                                                System.Drawing.Font newfont = new System.Drawing.Font(column.Font, column.Font.Style | System.Drawing.FontStyle.Underline);
                                                row = new PrintRow(mqty.ToString("#0") + " X " + pack, PrintRowPixel, colpix, newfont);
                                                PrintBill.Rows.Add(row);
                                                //TODO
                                            }
                                            else
                                            {
                                                row = new PrintRow(mqty.ToString("#0") + " X " + pack, PrintRowPixel, colpix, column.Font);
                                                PrintBill.Rows.Add(row);
                                            }
                                        }
                                    }
                                    else if (column.ColumnDataType == EcoMart.Printing.ColumnDataType.Decimal)
                                    {
                                        if (dr.Cells[column.ColumnDataField].Value != null && Convert.ToDouble(dr.Cells[column.ColumnDataField].Value.ToString()) != 0)
                                        {
                                            mamt = Convert.ToDouble(dr.Cells[column.ColumnDataField].Value.ToString());
                                            mlen = (mamt.ToString("#0.00").Length);
                                            colpix = Convert.ToInt32(column.Column - 30 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                            if (IFSaleBill == "S" && dr.Cells["Col_IfMultipleMRP"].Value != null && dr.Cells["Col_IfMultipleMRP"].Value.ToString() != string.Empty && dr.Cells["Col_IfMultipleMRP"].Value.ToString() == "Y")
                                            {
                                                //TODO:Set Font for underline
                                                System.Drawing.Font newfont = new System.Drawing.Font(column.Font, column.Font.Style | System.Drawing.FontStyle.Underline);
                                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, newfont);
                                                PrintBill.Rows.Add(row);
                                                //TODO
                                            }
                                            else
                                            {
                                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, column.Font);
                                                PrintBill.Rows.Add(row);
                                            }
                                            
                                        }
                                    }
                                    else
                                    {
                                        if (dr.Cells[column.ColumnDataField].Value != null && dr.Cells[column.ColumnDataField].Value.ToString() != "")
                                        {
                                            string ss = (column.ColumnDataField).ToString();
                                            if (ss == "Col_ProductName")
                                            {
                                                mproductname = dr.Cells["Col_ProductName"].Value.ToString();
                                                int ProductNameWidth = General.CurrentSetting.MsetProductNameWidthInSaleInvoice;
                                                if (mproductname.Length < ProductNameWidth)
                                                    mproductname = mproductname.PadRight(ProductNameWidth);
                                                else
                                                    mproductname = mproductname.Substring(0, ProductNameWidth);
                                                //if (mproductname.Length < 30)
                                                //    mproductname = mproductname.PadRight(30);
                                                //else
                                                //    mproductname = mproductname.Substring(0, 30);
                                                if (IFSaleBill == "S")
                                                {
                                                    if (dr.Cells["Col_ProdScheduleDrugCode"].Value != null && dr.Cells["Col_ProdScheduleDrugCode"].Value.ToString() == "H1")
                                                        mproductname = mproductname + "(H1)";
                                                }
                                                colpix = column.Column;
                                                if (IFSaleBill == "S" && dr.Cells["Col_IfMultipleMRP"].Value != null && dr.Cells["Col_IfMultipleMRP"].Value.ToString() != string.Empty && dr.Cells["Col_IfMultipleMRP"].Value.ToString() == "Y")
                                                {
                                                    //TODO:Set Font for underline
                                                    System.Drawing.Font newfont = new System.Drawing.Font(column.Font, column.Font.Style | System.Drawing.FontStyle.Underline);
                                                    row = new PrintRow(mproductname, PrintRowPixel, colpix, newfont);
                                                    PrintBill.Rows.Add(row);
                                                    //TODO
                                                }
                                                else
                                                {
                                                    row = new PrintRow(mproductname, PrintRowPixel, colpix, column.Font);
                                                    PrintBill.Rows.Add(row);
                                                }
                                            }
                                            else
                                            {
                                                colpix = column.Column;
                                                if (IFSaleBill == "S" && dr.Cells["Col_IfMultipleMRP"].Value != null && dr.Cells["Col_IfMultipleMRP"].Value.ToString() != string.Empty && dr.Cells["Col_IfMultipleMRP"].Value.ToString() == "Y")
                                                {
                                                    //TODO:Set Font for underline
                                                    System.Drawing.Font newfont = new System.Drawing.Font(column.Font, column.Font.Style | System.Drawing.FontStyle.Underline);
                                                    row = new PrintRow(dr.Cells[column.ColumnDataField].Value.ToString(), PrintRowPixel, colpix, newfont);
                                                    PrintBill.Rows.Add(row);
                                                    //TODO
                                                }
                                                else
                                                {
                                                   
                                                        row = new PrintRow(dr.Cells[column.ColumnDataField].Value.ToString(), PrintRowPixel, colpix, column.Font);
                                                        PrintBill.Rows.Add(row);
                                                    
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                    //}//End of For each Rows


                    PrintFooter(Narration, NetAmount, DiscountAmount, CNAmount, DNAmount, GrossAmount, BalanceAmount, atow);

                    //if (General.CurrentSetting.MsetNumberOfBillsAtaTime == 2)
                    //{
                    //    if (_copiesPrinted == 1)
                    //        PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight * 2);
                    //}
                    //else
                    //{
                    PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight);

                    if (General.CurrentSetting.MsetNumberOfBillsAtaTime == 1)
                    {
                        if (IFSaleBill == "S")
                        {
                            PrintFactory.SendLineFeed(General.PrintSettings.SaleBillPrintSettingsPlainPaper.GeneralSettings.LineFeed);
                        }
                        else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I")
                        {
                            PrintFactory.SendLineFeed(General.PrintSettings.DebitNotePrintSettingsPlainPaper.GeneralSettings.LineFeed);
                        }
                        else if (IFSaleBill == "O")
                        {
                            PrintFactory.SendLineFeed(General.PrintSettings.StockOutPrintSettingsPlainPaper.GeneralSettings.LineFeed);
                        }
                        else
                        {
                            PrintFactory.SendLineFeed(General.PrintSettings.CashBankPrintSettingsPlainPaper.GeneralSettings.LineFeed);
                        }
                    }


                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
        }


        private void PrintHeader(int currentPageNo, int TotalPages, string BillType, string BillNo, string BillDate, string PatientName, string PatientAddress, string PatientTelephone, string DoctorName, string DoctorAddress, string SaleSubType)
        {
            PrintRow row;
            EcoMart.Printing.PageHeader PageHeader;
            try
            {
                if (SaleSubType == string.Empty)
                    SaleSubType = " ";
                //PrintFactory.SendReverseLineFeed(6);
                if (IFSaleBill == "S")
                {
                    PageHeader = General.PrintSettings.SaleBillPrintSettingsPlainPaper.PageHeader;
                }
                else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I")
                {
                    PageHeader = General.PrintSettings.DebitNotePrintSettingsPlainPaper.PageHeader;
                }
                else if (IFSaleBill == "O")
                {
                    PageHeader = General.PrintSettings.StockOutPrintSettingsPlainPaper.PageHeader;
                }
                else
                {
                    PageHeader = General.PrintSettings.CashBankPrintSettingsPlainPaper.PageHeader;

                }
                if (PageHeader.ShopName.Show)
                {
                    row = new PrintRow(General.ShopDetail.ShopName, PageHeader.ShopName.Row, PageHeader.ShopName.Column, PageHeader.ShopName.Font);
                    PrintBill.Rows.Add(row);
                }
                //if (PageHeader.Time.Show)
                //{
                //    row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PageHeader.Time.Row, PageHeader.Time.Column, PageHeader.Time.Font);
                //    PrintBill.Rows.Add(row);
                //}
                row = new PrintRow(PageHeader.VoucherTypeSCA.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCA.Row, PageHeader.VoucherTypeSCA.Column, PageHeader.VoucherTypeSCA.Font);
                if (IFSaleBill == "S")
                {
                    if (BillType == FixAccounts.VoucherTypeForCashSale)
                        row = new PrintRow(PageHeader.VoucherTypeSCA.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCA.Row, PageHeader.VoucherTypeSCA.Column, PageHeader.VoucherTypeSCA.Font);
                    else if (BillType == FixAccounts.VoucherTypeForCreditStatementSale)
                        row = new PrintRow(PageHeader.VoucherTypeSCS.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCS.Row, PageHeader.VoucherTypeSCS.Column, PageHeader.VoucherTypeSCS.Font);
                    else
                        row = new PrintRow(PageHeader.VoucherTypeSCR.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCR.Row, PageHeader.VoucherTypeSCR.Column, PageHeader.VoucherTypeSCR.Font);
                    PrintBill.Rows.Add(row);
                }
                else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I" || IFSaleBill == "O")
                {
                    if (IFSaleBill == "C")
                        row = new PrintRow(PageHeader.VoucherTypeCreditNote.Text + BillNo.Trim(), PageHeader.VoucherTypeCreditNote.Row, PageHeader.VoucherTypeCreditNote.Column, PageHeader.VoucherTypeCreditNote.Font);
                    else if (IFSaleBill == "D")
                        row = new PrintRow(PageHeader.VoucherTypeDebitNote.Text + BillNo.Trim(), PageHeader.VoucherTypeDebitNote.Row, PageHeader.VoucherTypeDebitNote.Column, PageHeader.VoucherTypeDebitNote.Font);
                    else if (IFSaleBill == "I")
                        row = new PrintRow(PageHeader.VoucherTypeStockIn.Text + BillNo.Trim(), PageHeader.VoucherTypeStockIn.Row, PageHeader.VoucherTypeStockIn.Column, PageHeader.VoucherTypeStockIn.Font);
                    else if (IFSaleBill == "O")
                        row = new PrintRow(PageHeader.VoucherTypeStockOut.Text + BillNo.Trim(), PageHeader.VoucherTypeStockOut.Row, PageHeader.VoucherTypeStockOut.Column, PageHeader.VoucherTypeStockOut.Font);
                    PrintBill.Rows.Add(row);
                }
                else
                {
                    if (IFSaleBill == "R")
                        row = new PrintRow(PageHeader.VoucherTypeCashReceipt.Text + BillNo.Trim(), PageHeader.VoucherTypeCashReceipt.Row, PageHeader.VoucherTypeCashReceipt.Column, PageHeader.VoucherTypeCashReceipt.Font);
                    if (IFSaleBill == "P")
                        row = new PrintRow(PageHeader.VoucherTypeCashPayment.Text + BillNo.Trim(), PageHeader.VoucherTypeCashPayment.Row, PageHeader.VoucherTypeCashPayment.Column, PageHeader.VoucherTypeCashPayment.Font);
                    if (IFSaleBill == "W")
                        row = new PrintRow(PageHeader.VoucherTypeBankReceipt.Text + BillNo.Trim(), PageHeader.VoucherTypeBankReceipt.Row, PageHeader.VoucherTypeBankReceipt.Column, PageHeader.VoucherTypeBankReceipt.Font);
                    if (IFSaleBill == "B")
                        row = new PrintRow(PageHeader.VoucherTypeBankPayment.Text + BillNo.Trim(), PageHeader.VoucherTypeBankPayment.Row, PageHeader.VoucherTypeBankPayment.Column, PageHeader.VoucherTypeBankPayment.Font);
                    PrintBill.Rows.Add(row);
                }


                if (PageHeader.ShopAddress1.Show)
                {
                    row = new PrintRow(General.ShopDetail.ShopAddress1.Trim(), PageHeader.ShopAddress1.Row, PageHeader.ShopAddress1.Column, PageHeader.ShopAddress1.Font);
                    PrintBill.Rows.Add(row);
                }
                row = new PrintRow(PageHeader.Date.Text.ToString() + " " + General.GetDateInShortDateFormat(BillDate), PageHeader.Date.Row, PageHeader.Date.Column, PageHeader.Date.Font);
                PrintBill.Rows.Add(row);
                if (PageHeader.ShopAddress2.Show)
                {
                    row = new PrintRow(General.ShopDetail.ShopAddress2.Trim(), PageHeader.ShopAddress2.Row, PageHeader.ShopAddress2.Column, PageHeader.ShopAddress2.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageHeader.ShopTelephone.Show)
                {
                    row = new PrintRow(PageHeader.ShopTelephone.Text.ToString() + " " + General.ShopDetail.ShopTelephone.Trim(), PageHeader.ShopTelephone.Row, PageHeader.ShopTelephone.Column, PageHeader.ShopTelephone.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageHeader.Time.Show)
                {

                    row = new PrintRow(PageHeader.Time.Text.ToString() + " " + DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PageHeader.Time.Row, PageHeader.Time.Column, PageHeader.Time.Font);
                    PrintBill.Rows.Add(row);
                }

                row = new PrintRow(PageHeader.PatientName.Text.ToString() + " " + PatientName, PageHeader.PatientName.Row, PageHeader.PatientName.Column, PageHeader.PatientName.Font);
                PrintBill.Rows.Add(row);

                row = new PrintRow(PageHeader.PatientAddress.Text.ToString() + " " + PatientAddress, PageHeader.PatientAddress.Row, PageHeader.PatientAddress.Column, PageHeader.PatientAddress.Font);
                PrintBill.Rows.Add(row);

                if (PageHeader.PatientTelephone.Show)
                {
                    row = new PrintRow(PatientTelephone, PageHeader.PatientTelephone.Row, PageHeader.PatientTelephone.Column, PageHeader.PatientTelephone.Font);
                    PrintBill.Rows.Add(row);
                }

                string page = currentPageNo.ToString().Trim() + "/" + TotalPages.ToString().Trim();
                row = new PrintRow(PageHeader.PageNo.Text.ToString() + " " + page, PageHeader.PageNo.Row, PageHeader.PageNo.Column, PageHeader.PageNo.Font);
                PrintBill.Rows.Add(row);

                int PrintRowPixel = 0;
                if (IFSaleBill == "S")
                {
                    row = new PrintRow(PageHeader.DoctorName.Text.ToString() + " " + DoctorName.Trim(), PageHeader.DoctorName.Row, PageHeader.DoctorName.Column, PageHeader.DoctorName.Font);
                    PrintBill.Rows.Add(row);

                    row = new PrintRow(PageHeader.DoctorAddress.Text.ToString() + " " + DoctorAddress.Trim(), PageHeader.DoctorAddress.Row, PageHeader.DoctorAddress.Column, PageHeader.DoctorAddress.Font);
                    PrintBill.Rows.Add(row);

                    PrintRowPixel = PageHeader.DoctorName.Row + 11;
                }
                else
                    PrintRowPixel = PageHeader.PatientAddress.Row + 11;

                if (generalPrintSettings.PageWidth > 700)
                    row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, General.FontRegular);
                else
                    row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 11;
                EcoMart.Printing.PageContent PageContent;
                if (IFSaleBill == "S")
                {
                    PageContent = General.PrintSettings.SaleBillPrintSettingsPlainPaper.PageContent;
                }
                else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I")
                {
                    PageContent = General.PrintSettings.DebitNotePrintSettingsPlainPaper.PageContent;
                }
                else if (IFSaleBill == "O")
                {
                    PageContent = General.PrintSettings.StockOutPrintSettingsPlainPaper.PageContent;
                }
                else
                {
                    PageContent = General.PrintSettings.CashBankPrintSettingsPlainPaper.PageContent;
                }
                //HEADERS
                for (int i = 0; i < PageContent.ColumnCount; i++)
                {
                    if (PageContent.Columns[i].Show == true)
                    {
                        
                            row = new PrintRow(PageContent.Columns[i].ColumnHeader, PrintRowPixel, PageContent.Columns[i].Column, PageContent.Columns[i].Font);
                            PrintBill.Rows.Add(row);
                        
                    }
                }

                PrintRowPixel += 11;

                if (generalPrintSettings.PageWidth > 700)
                    row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, General.FontRegular);
                else
                    row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                PrintBill.Rows.Add(row);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        private void PrintFooter(string Narration, double NetAmount, double DiscountAmount, double CNAmount, double DNAmount, double GrossAmount, double BalanceAmount, string atow)
        {
            PrintRow row;
            PageFooter PageFooter;
            try
            {
                if (IFSaleBill == "S")
                {
                    PageFooter = General.PrintSettings.SaleBillPrintSettingsPlainPaper.PageFooter;
                }
                else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I")
                {
                    PageFooter = General.PrintSettings.DebitNotePrintSettingsPlainPaper.PageFooter;
                }
                else if (IFSaleBill == "O")
                {
                    PageFooter = General.PrintSettings.StockOutPrintSettingsPlainPaper.PageFooter;
                }
                else
                {
                    PageFooter = General.PrintSettings.CashBankPrintSettingsPlainPaper.PageFooter;
                }

                int PrintRowPixel = PageFooter.NetAmountItem.Row - 13;
                if (PageFooter.ATOWItem.Show)
                {
                    row = new PrintRow(PageFooter.ATOWItem.Text + " " + atow, PageFooter.ATOWItem.Row, PageFooter.ATOWItem.Column, PageFooter.ATOWItem.Font);
                    PrintBill.Rows.Add(row);
                }


                if (generalPrintSettings.PageWidth > 700)
                    row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, General.FontRegular);
                else
                    row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                PrintBill.Rows.Add(row);

                if (PageFooter.GrossAmountItem.Show)
                {
                    row = new PrintRow(PageFooter.GrossAmountItem.Text + " " + GrossAmount.ToString("#0.00"), PageFooter.GrossAmountItem.Row, PageFooter.GrossAmountItem.Column, PageFooter.GrossAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.NarrationItem.Show)
                {
                    row = new PrintRow(Narration, PageFooter.NarrationItem.Row, PageFooter.NarrationItem.Column, PageFooter.NarrationItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.BalanceAmountItem.Show && BalanceAmount > 0)
                {
                    row = new PrintRow(PageFooter.BalanceAmountItem.Text + BalanceAmount.ToString("#0.00"), PageFooter.BalanceAmountItem.Row, PageFooter.BalanceAmountItem.Column, PageFooter.BalanceAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }

                if (PageFooter.DiscountItem.Show && DiscountAmount > 0)
                {
                    row = new PrintRow(PageFooter.DiscountItem.Text + DiscountAmount.ToString("#0.00"), PageFooter.DiscountItem.Row, PageFooter.DiscountItem.Column, PageFooter.DiscountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.CreditNoteItem.Show && CNAmount > 0)
                {
                    row = new PrintRow(PageFooter.CreditNoteItem.Text + CNAmount.ToString("#0.00"), PageFooter.CreditNoteItem.Row, PageFooter.CreditNoteItem.Column, PageFooter.CreditNoteItem.Font);
                    PrintBill.Rows.Add(row);
                }

                if (PageFooter.DebitNoteItem.Show && DNAmount > 0)
                {
                    row = new PrintRow(PageFooter.DebitNoteItem.Text + DNAmount.ToString("#0.00"), PageFooter.DebitNoteItem.Row, PageFooter.DebitNoteItem.Column, PageFooter.DebitNoteItem.Font);
                    PrintBill.Rows.Add(row);
                }
                //if (PageFooter.DiscountItem.Show && DNAmount > 0)
                //{
                //    row = new PrintRow(PageFooter.DebitNoteItem.Text + DNAmount.ToString("#0.00"), PageFooter.DebitNoteItem.Row, PageFooter.DebitNoteItem.Column, PageFooter.DebitNoteItem.Font);
                //    PrintBill.Rows.Add(row);
                //}
                if (PageFooter.NetAmountItem.Show)
                {
                    row = new PrintRow(PageFooter.NetAmountItem.Text + " " + NetAmount.ToString("#0.00"), PageFooter.NetAmountItem.Row, PageFooter.NetAmountItem.Column, PageFooter.NetAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }

                PrintRowPixel = PageFooter.NetAmountItem.Row + 13;
                if (generalPrintSettings.PageWidth > 700)
                    row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, General.FontRegular);
                else
                    row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                PrintBill.Rows.Add(row);



                if (PageFooter.SubjectItem.Show == true)
                {
                    row = new PrintRow(PageFooter.SubjectItem.Text.ToString() + " " + General.ShopDetail.ShopJurisdiction.ToString(), PageFooter.SubjectItem.Row, PageFooter.SubjectItem.Column, PageFooter.SubjectItem.Font);
                    PrintBill.Rows.Add(row);
                }

                row = new PrintRow(PageFooter.DLNItem.Text.ToString() + " " + General.ShopDetail.ShopDLN.ToString(), PageFooter.DLNItem.Row, PageFooter.DLNItem.Column, PageFooter.DLNItem.Font);
                PrintBill.Rows.Add(row);

                row = new PrintRow(PageFooter.VATTINItem.Text.ToString() + " " + General.ShopDetail.ShopVATTINV.ToString() + "," + General.ShopDetail.ShopVATTINC.ToString(), PageFooter.VATTINItem.Row, PageFooter.VATTINItem.Column, PageFooter.VATTINItem.Font);
                PrintBill.Rows.Add(row);
                if (PageFooter.DeclarationItem1.Show)
                {
                    row = new PrintRow(PageFooter.DeclarationItem1.Text.ToString(), PageFooter.DeclarationItem1.Row, PageFooter.DeclarationItem1.Column, PageFooter.DeclarationItem1.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.DeclarationItem2.Show)
                {
                    row = new PrintRow(PageFooter.DeclarationItem2.Text.ToString(), PageFooter.DeclarationItem2.Row, PageFooter.DeclarationItem2.Column, PageFooter.DeclarationItem2.Font);
                    PrintBill.Rows.Add(row);
                }
                row = new PrintRow(PageFooter.ShopNameItem.Text.ToString() + " " + General.ShopDetail.ShopName.ToString(), PageFooter.ShopNameItem.Row, PageFooter.ShopNameItem.Column, PageFooter.ShopNameItem.Font);
                PrintBill.Rows.Add(row);
                if (PageFooter.DeclarationItem3.Show)
                {
                    row = new PrintRow(PageFooter.DeclarationItem3.Text.ToString(), PageFooter.DeclarationItem3.Row, PageFooter.DeclarationItem3.Column, PageFooter.DeclarationItem3.Font);
                    PrintBill.Rows.Add(row);
                }
                row = new PrintRow(PageFooter.SignatureItem.Text.ToString(), PageFooter.SignatureItem.Row, PageFooter.SignatureItem.Column, PageFooter.SignatureItem.Font);
                PrintBill.Rows.Add(row);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }
}
