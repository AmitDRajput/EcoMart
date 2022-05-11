using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintDataGrid;
using EcoMart.Common;
using System.Windows.Forms;

namespace EcoMart.Printing
{
    public class PrePrintedPaperPrinter
    {
        public string IFSaleBill = "";
        public int Addrow = 0;
        public PrePrintedPaperPrinter()
        {

        }

        public void Print(string BillType, string BillNo, string BillDate, string PatientName, string PatientAddress, string PatientTelephone, string DoctorName, string DoctorAddress, DataGridViewRowCollection Rows, string Narration, double NetAmount, string SaleSubType, double DiscountAmount, double CNAmount, double DNAmount, double GrossAmount, double BalanceAmount, string atow)
        {
            //System.Threading.Thread th = new System.Threading.Thread(() =>
            //PrintData(BillType, BillNo, BillDate, PatientName, PatientAddress, PatientTelephone, DoctorName, DoctorAddress, Rows, Narration, NetAmount, SaleSubType, DiscountAmount, CNAmount, DNAmount, GrossAmount, BalanceAmount, atow));
            //th.Start();
            PrintData(BillType, BillNo, BillDate, PatientName, PatientAddress, PatientTelephone, DoctorName, DoctorAddress, Rows, Narration, NetAmount, SaleSubType, DiscountAmount, CNAmount, DNAmount, GrossAmount, BalanceAmount, atow);
        }
        private void PrintData(string BillType, string BillNo, string BillDate, string PatientName, string PatientAddress, string PatientTelephone, string DoctorName, string DoctorAddress, DataGridViewRowCollection Rows, string Narration, double NetAmount, string SaleSubType, double DiscountAmount, double CNAmount, double DNAmount, double GrossAmount, double BalanceAmount, string atow)
        {
            Addrow = 0;
            int PrintRowPixel = 0;
            int rowcount = 0;

            for (int copies = 0; copies < General.CurrentSetting.MsetNumberOfBillsAtaTime; copies++)
            {
                PrintRow row;
                rowcount = 0;
                if (copies == 1)
                {
                    Addrow += 24;
                }
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
                    else if (BillType == FixAccounts.VoucherTypeForCashReceipt)
                        IFSaleBill = "R";
                    else if (BillType == FixAccounts.VoucherTypeForCashPayment)
                        IFSaleBill = "P";
                    else if (BillType == FixAccounts.VoucherTypeForBankReceipt)
                        IFSaleBill = "W";
                    else if (BillType == FixAccounts.VoucherTypeForBankPayment)
                        IFSaleBill = "B";
                    if (General.CurrentSetting.MsetNumberOfBillsAtaTime == 1)
                    {
                        if (IFSaleBill == "S")
                            PrintFactory.SendReverseLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.ReverseLineFeed);
                        else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I" || IFSaleBill == "O")
                            PrintFactory.SendReverseLineFeed(General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.GeneralSettings.ReverseLineFeed);
                        else
                            PrintFactory.SendReverseLineFeed(General.PrintSettings.CashBankPrintSettingsPrePrintedPaper.GeneralSettings.ReverseLineFeed);
                    }
                    if (copies == 0)
                        PrintBill.Rows.Clear();
                    if (IFSaleBill == "S")
                        generalPrintSettings = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings;
                    else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I" || IFSaleBill == "O")
                        generalPrintSettings = General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.GeneralSettings;
                    else
                        generalPrintSettings = General.PrintSettings.CashBankPrintSettingsPrePrintedPaper.GeneralSettings;

                    //PrintBill.Rows.Clear();
                    //if (IFSaleBill == "S")
                    //    generalPrintSettings = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings;
                    //else
                    //    generalPrintSettings = General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.GeneralSettings;

                    double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(Rows.Count) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                    int totalpages = Convert.ToInt32(totpages);



                    PrintHeader(CurrentPageNo, totalpages, BillType, BillNo, BillDate, PatientName, PatientAddress, PatientTelephone, DoctorName, DoctorAddress, SaleSubType, copies);


                    if (copies == 0)
                        PrintRowPixel = generalPrintSettings.ContentStartRow;
                    else
                        PrintRowPixel = generalPrintSettings.ContentStartRow + 400;
                    PageFooter PageFooter;
                    foreach (DataGridViewRow dr in Rows)
                    {

                        if (dr.Cells["Col_ProductID"].Value != null || dr.Cells["Col_Amount"].Value != null)
                        {
                            if (rowcount >= General.CurrentSetting.MsetNumberOfLinesSaleBill)
                            {
                                if (IFSaleBill == "S")
                                {
                                    PageFooter = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.PageFooter;
                                }
                                else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I" || IFSaleBill == "O")
                                {
                                    PageFooter = General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.PageFooter;
                                }
                                else
                                {
                                    PageFooter = General.PrintSettings.CashBankPrintSettingsPrePrintedPaper.PageFooter;
                                }

                                row = new PrintRow(PageFooter.ContinueItem.Text, PageFooter.ContinueItem.Row + Addrow, PageFooter.ContinueItem.Column, PageFooter.ContinueItem.Font);
                                PrintBill.Rows.Add(row);
                                PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight);
                                PrintBill.Rows.Clear();
                                PrintRowPixel = 0;
                                CurrentPageNo += 1;
                                PrintHeader(CurrentPageNo, totalpages, BillType, BillNo, BillDate, PatientName, PatientAddress, PatientTelephone, DoctorName, DoctorAddress, SaleSubType, copies);
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
                                PageContent = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.PageContent;
                            }
                            else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I" || IFSaleBill == "O")
                            {
                                PageContent = General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.PageContent;
                            }
                            else
                            {
                                PageContent = General.PrintSettings.CashBankPrintSettingsPrePrintedPaper.PageContent;
                            }
                            try
                            {
                                for (int i = 0; i < PageContent.ColumnCount; i++)
                                {
                                    EcoMart.Printing.PageColumn column;
                                    column = PageContent.Columns[i];
                                    if (column.Show)
                                    {
                                        if (dr.Cells[column.ColumnDataField].Value != null)
                                        {
                                            if (column.ColumnDataType == EcoMart.Printing.ColumnDataType.Integer)
                                            {
                                                if (dr.Cells[column.ColumnDataField].Value != null && Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString()) != 0)
                                                {
                                                    mqty = Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString());
                                                    mlen = (mqty.ToString("#0").Length);
                                                    colpix = Convert.ToInt32(column.Column + ((5 - Convert.ToInt32(mlen)) * 5.5));
                                                    string pack = "";
                                                    if (dr.Cells["Col_Pack"].Value != null && dr.Cells["Col_Pack"].Value.ToString() != string.Empty)
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
                                                    //row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, column.Font);
                                                    //PrintBill.Rows.Add(row);
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
                                                if (dr.Cells[column.ColumnDataField].Value != DBNull.Value && dr.Cells[column.ColumnDataField].Value.ToString() != "")
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

                                                        //if (mproductname.Length < 20)
                                                        //    mproductname = mproductname.PadRight(20);
                                                        //else
                                                        //    mproductname = mproductname.Substring(0, 20);
                                                        if (IFSaleBill == "S")
                                                        {
                                                            if (dr.Cells["Col_ProdScheduleDrugCode"].Value != null && dr.Cells["Col_ProdScheduleDrugCode"].Value.ToString() == "H1")
                                                                mproductname = mproductname + "(H1)";
                                                        }
                                                        colpix = column.Column;
                                                        //row = new PrintRow(mproductname, PrintRowPixel, colpix, column.Font);
                                                        //PrintBill.Rows.Add(row);
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
                                                        //row = new PrintRow(dr.Cells[column.ColumnDataField].Value.ToString(), PrintRowPixel, colpix, column.Font);
                                                        //PrintBill.Rows.Add(row);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                Log.WriteException(Ex);
                            }
                        }

                    }
                    // End of For each Rows

                    PrintFooter(Narration, NetAmount, DiscountAmount, CNAmount, DNAmount, GrossAmount, BalanceAmount, atow);

                    if (General.CurrentSetting.MsetNumberOfBillsAtaTime == 2)
                    {
                        if (copies == 1)
                        {
                            PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight);
                        }
                    }
                    else
                        PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight);

                    if (General.CurrentSetting.MsetNumberOfBillsAtaTime == 1)
                    {
                        if (IFSaleBill == "S")
                        {
                            PrintFactory.SendLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.LineFeed);
                        }
                        else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I" || IFSaleBill == "O")
                        {
                            PrintFactory.SendLineFeed(General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.GeneralSettings.LineFeed);
                        }
                        else
                        {
                            PrintFactory.SendLineFeed(General.PrintSettings.CashBankPrintSettingsPrePrintedPaper.GeneralSettings.LineFeed);
                        }
                    }
                }


                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
        }


        private void PrintHeader(int currentPageNo, int TotalPages, string BillType, string BillNo, string BillDate, string PatientName, string PatientAddress, string PatientTelephone, string DoctorName, string DoctorAddress, string SaleSubType, int copies)
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
                    PageHeader = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.PageHeader;
                }
                else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I" || IFSaleBill == "O")
                {
                    PageHeader = General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.PageHeader;
                }
                else
                {
                    PageHeader = General.PrintSettings.CashBankPrintSettingsPrePrintedPaper.PageHeader;

                }

                if (PageHeader.ShopName.Show)
                {
                    row = new PrintRow(General.ShopDetail.ShopName, PageHeader.ShopName.Row + Addrow, PageHeader.ShopName.Column, PageHeader.ShopName.Font);
                    PrintBill.Rows.Add(row);
                }

                if (copies == 1)
                    Addrow = 400;
                else if (copies == 0)
                    Addrow = 0;
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PageHeader.Time.Row + Addrow, PageHeader.Time.Column, PageHeader.Time.Font);
                PrintBill.Rows.Add(row);
                if (PageHeader.ShopAddress1.Show)
                {
                    row = new PrintRow(General.ShopDetail.ShopAddress1.Trim() + " " + General.ShopDetail.ShopAddress2.Trim(), PageHeader.ShopAddress1.Row + Addrow, PageHeader.ShopAddress1.Column, PageHeader.ShopAddress1.Font);
                    PrintBill.Rows.Add(row);
                }
                if (IFSaleBill == "S")
                {
                    if (BillType == FixAccounts.VoucherTypeForCashSale)
                        row = new PrintRow(PageHeader.VoucherTypeSCA.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), (PageHeader.VoucherTypeSCA.Row + Addrow), PageHeader.VoucherTypeSCA.Column, PageHeader.VoucherTypeSCA.Font);
                    else if (BillType == FixAccounts.VoucherTypeForCreditStatementSale)
                        row = new PrintRow(PageHeader.VoucherTypeSCS.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCS.Row + Addrow, PageHeader.VoucherTypeSCS.Column, PageHeader.VoucherTypeSCS.Font);
                    else
                        row = new PrintRow(PageHeader.VoucherTypeSCR.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCR.Row + Addrow, PageHeader.VoucherTypeSCR.Column, PageHeader.VoucherTypeSCR.Font);
                    PrintBill.Rows.Add(row);
                }
                else if (IFSaleBill == "C" || IFSaleBill == "D" || IFSaleBill == "I" || IFSaleBill == "O")
                {
                    if (IFSaleBill == "C")
                        row = new PrintRow(PageHeader.VoucherTypeCreditNote.Text + BillNo.Trim(), PageHeader.VoucherTypeCreditNote.Row + Addrow, PageHeader.VoucherTypeCreditNote.Column, PageHeader.VoucherTypeCreditNote.Font);
                    else if (IFSaleBill == "D")
                        row = new PrintRow(PageHeader.VoucherTypeDebitNote.Text + BillNo.Trim(), PageHeader.VoucherTypeDebitNote.Row + Addrow, PageHeader.VoucherTypeDebitNote.Column, PageHeader.VoucherTypeDebitNote.Font);
                    else if (IFSaleBill == "I")
                        row = new PrintRow(PageHeader.VoucherTypeStockIn.Text + BillNo.Trim(), PageHeader.VoucherTypeStockIn.Row + Addrow, PageHeader.VoucherTypeStockIn.Column, PageHeader.VoucherTypeStockIn.Font);
                    else if (IFSaleBill == "O")
                        row = new PrintRow(PageHeader.VoucherTypeStockOut.Text + BillNo.Trim(), PageHeader.VoucherTypeStockOut.Row + Addrow, PageHeader.VoucherTypeStockOut.Column, PageHeader.VoucherTypeStockOut.Font);
                    PrintBill.Rows.Add(row);
                }
                else
                {
                    if (IFSaleBill == "R")
                        row = new PrintRow(PageHeader.VoucherTypeCashReceipt.Text + BillNo.Trim(), PageHeader.VoucherTypeCashReceipt.Row + Addrow, PageHeader.VoucherTypeCashReceipt.Column, PageHeader.VoucherTypeCashReceipt.Font);
                    if (IFSaleBill == "P")
                        row = new PrintRow(PageHeader.VoucherTypeCashPayment.Text + BillNo.Trim(), PageHeader.VoucherTypeCashPayment.Row + Addrow, PageHeader.VoucherTypeCashPayment.Column, PageHeader.VoucherTypeCashPayment.Font);
                    if (IFSaleBill == "W")
                        row = new PrintRow(PageHeader.VoucherTypeBankReceipt.Text + BillNo.Trim(), PageHeader.VoucherTypeBankReceipt.Row + Addrow, PageHeader.VoucherTypeBankReceipt.Column, PageHeader.VoucherTypeBankReceipt.Font);
                    if (IFSaleBill == "B")
                        row = new PrintRow(PageHeader.VoucherTypeBankPayment.Text + BillNo.Trim(), PageHeader.VoucherTypeBankPayment.Row + Addrow, PageHeader.VoucherTypeBankPayment.Column, PageHeader.VoucherTypeBankPayment.Font);
                    PrintBill.Rows.Add(row);
                }
                //else
                //{
                //    if (IFSaleBill == "C")
                //        row = new PrintRow(PageHeader.VoucherTypeCreditNote.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeCreditNote.Row, PageHeader.VoucherTypeCreditNote.Column, PageHeader.VoucherTypeCreditNote.Font);
                //    else if (IFSaleBill == "D")
                //        row = new PrintRow(PageHeader.VoucherTypeDebitNote.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeDebitNote.Row, PageHeader.VoucherTypeDebitNote.Column, PageHeader.VoucherTypeDebitNote.Font);
                //}
                //PrintBill.Rows.Add(row);

                if (PageHeader.ShopTelephone.Show)
                {
                    row = new PrintRow(General.ShopDetail.ShopTelephone.Trim(), PageHeader.ShopTelephone.Row + Addrow, PageHeader.ShopTelephone.Column, PageHeader.ShopTelephone.Font);
                    PrintBill.Rows.Add(row);
                }

                row = new PrintRow(General.GetDateInShortDateFormat(BillDate), PageHeader.Date.Row + Addrow, PageHeader.Date.Column, PageHeader.Date.Font);
                PrintBill.Rows.Add(row);
                row = new PrintRow(PatientName, PageHeader.PatientName.Row + Addrow, PageHeader.PatientName.Column, PageHeader.PatientName.Font);
                PrintBill.Rows.Add(row);
                row = new PrintRow(PatientAddress, PageHeader.PatientAddress.Row + Addrow, PageHeader.PatientAddress.Column, PageHeader.PatientAddress.Font);
                PrintBill.Rows.Add(row);
                if (PageHeader.PatientTelephone.Show)
                {
                    row = new PrintRow(PatientTelephone, PageHeader.PatientTelephone.Row + Addrow, PageHeader.PatientTelephone.Column, PageHeader.PatientTelephone.Font);
                    PrintBill.Rows.Add(row);
                }
                row = new PrintRow(DoctorName.Trim(), PageHeader.DoctorName.Row + Addrow, PageHeader.DoctorName.Column, PageHeader.DoctorName.Font);
                PrintBill.Rows.Add(row);
                row = new PrintRow(DoctorAddress.Trim(), PageHeader.DoctorAddress.Row + Addrow, PageHeader.DoctorAddress.Column, PageHeader.DoctorAddress.Font);
                PrintBill.Rows.Add(row);
                //string page = currentPageNo.ToString().Trim() + "/" + TotalPages.ToString().Trim();
                //row = new PrintRow(page, PageHeader.PageNo.Row, PageHeader.PageNo.Column, PageHeader.PageNo.Font);
                //PrintBill.Rows.Add(row);

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
                    PageFooter = General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.PageFooter;
                }
                else
                {
                    PageFooter = General.PrintSettings.DebitNotePrintSettingsPrePrintedPaper.PageFooter;
                }
                if (PageFooter.GrossAmountItem.Show)
                {
                    row = new PrintRow(GrossAmount.ToString("#0.00"), PageFooter.GrossAmountItem.Row + Addrow, PageFooter.GrossAmountItem.Column, PageFooter.GrossAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.NarrationItem.Show)
                {
                    row = new PrintRow(Narration, PageFooter.NarrationItem.Row + Addrow, PageFooter.NarrationItem.Column, PageFooter.NarrationItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.DiscountItem.Show && DiscountAmount > 0)
                {
                    row = new PrintRow(PageFooter.DiscountItem.Text + DiscountAmount.ToString("#0.00"), PageFooter.DiscountItem.Row + Addrow, PageFooter.DiscountItem.Column, PageFooter.DiscountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.CreditNoteItem.Show && CNAmount > 0)
                {
                    row = new PrintRow(PageFooter.CreditNoteItem.Text + CNAmount.ToString("#0.00"), PageFooter.CreditNoteItem.Row + Addrow, PageFooter.CreditNoteItem.Column, PageFooter.CreditNoteItem.Font);
                    PrintBill.Rows.Add(row);
                }

                if (PageFooter.DebitNoteItem.Show && DNAmount > 0)
                {
                    row = new PrintRow(PageFooter.DebitNoteItem.Text + DNAmount.ToString("#0.00"), PageFooter.DebitNoteItem.Row + Addrow, PageFooter.DebitNoteItem.Column, PageFooter.DebitNoteItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.BalanceAmountItem.Show && BalanceAmount > 0)
                {
                    row = new PrintRow(PageFooter.BalanceAmountItem.Text + BalanceAmount.ToString("#0.00"), PageFooter.BalanceAmountItem.Row + Addrow, PageFooter.BalanceAmountItem.Column, PageFooter.BalanceAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.NetAmountItem.Show)
                {
                    row = new PrintRow(NetAmount.ToString("#0.00"), PageFooter.NetAmountItem.Row + Addrow, PageFooter.NetAmountItem.Column, PageFooter.NetAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                row = new PrintRow(atow, PageFooter.NetAmountItem.Row + Addrow + 13, 1, PageFooter.NetAmountItem.Font);
                PrintBill.Rows.Add(row);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }
}
