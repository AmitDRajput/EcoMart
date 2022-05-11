using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintDataGrid;
using EcoMart.Common;
using System.Windows.Forms;



namespace EcoMart.Printing
{
    public class PlainPaperPrinterForDistributor
    {
        public string IFSaleBill = "";
        GeneralSettingsForDistributor generalPrintSettings;
        public PlainPaperPrinterForDistributor()
        {

        }
        public void Print(string BillType, string BillNo, string BillDate, string PatientName, string PatientAddress, string PatientAddress2, string PatientTelephone, string PatientVATTIN,  string PatientDLN,string PatientLBT, string DoctorName, string DoctorAddress, DataGridViewRowCollection Rows, double GrossAmount, double DiscountAmount,double VAT12Point5Amount, double VAT5Amount, double ADDAmount, double LESSAmount,string Narration1,string Narration2, double NetAmount, string SaleSubType, double CNAmount, double DNAmount, double BalanceAmount, string ShopDLN, string ShopLBT, double TotalItemDiscount, double TotalSchemeDiscount, double RoundUP)
        {
            PrintRow row;
            int rowcount = 1;
            int PrintRowPixel = 0;
            int CurrentPageNo = 1;
            //    GeneralSettings generalPrintSettings;
            IFSaleBill = "W";
            try
            {
                //if (BillType == FixAccounts.VoucherTypeForDistributorSaleCash || BillType == FixAccounts.VoucherTypeForCreditSale || BillType == FixAccounts.VoucherTypeForCreditStatementSale || BillType == FixAccounts.VoucherTypeForVoucherSale)
                //    IFSaleBill = "S";
                //else if (BillType == FixAccounts.VoucherTypeForCreditNoteStock)
                //    IFSaleBill = "C";
                //else if (BillType == FixAccounts.VoucherTypeForDebitNoteStock)
                //    IFSaleBill = "D";
                //else if (BillType == FixAccounts.VoucherTypeForStockIN)
                //    IFSaleBill = "I";
                //else if (BillType == FixAccounts.VoucherTypeForStockOut)
                 

                    if (IFSaleBill == "W")
                        PrintFactory.SendReverseLineFeed(General.PrintSettingsForDistributor.SaleBillPrintSettingsPlainPaperForDistributor.GeneralSettingsForDistributor.ReverseLineFeed);
                    else
                        PrintFactory.SendReverseLineFeed(General.PrintSettings.DebitNotePrintSettingsPlainPaper.GeneralSettings.ReverseLineFeed);

                PrintBill.Rows.Clear();
                if (IFSaleBill == "W")
                     generalPrintSettings = General.PrintSettingsForDistributor.SaleBillPrintSettingsPlainPaperForDistributor.GeneralSettingsForDistributor;
                else
                    generalPrintSettings = General.PrintSettingsForDistributor.SaleBillPrintSettingsPlainPaperForDistributor.GeneralSettingsForDistributor;
                double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(Rows.Count) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                int totalpages = Convert.ToInt32(totpages);
             
                PrintHeader(CurrentPageNo, totalpages, BillType, BillNo, BillDate, PatientName, PatientAddress, PatientAddress2, PatientTelephone, PatientVATTIN,PatientDLN,PatientLBT, DoctorName, DoctorAddress, SaleSubType);



                PrintRowPixel = generalPrintSettings.ContentStartRow;
                PageFooter PageFooter;
                foreach (DataGridViewRow dr in Rows)
                {

                    if (dr.Cells["Col_ProductID"].Value != null)
                    {
                        if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                        {
                            if (IFSaleBill == "W")
                            {
                                PageFooter = General.PrintSettings.SaleBillPrintSettingsPlainPaper.PageFooter;
                            }
                            else
                            {
                                PageFooter = General.PrintSettings.DebitNotePrintSettingsPlainPaper.PageFooter;
                            }

                            int PrintPix = PageFooter.ContinueItem.Row ;
                            if (generalPrintSettings.PageWidth > 700)
                                row = new PrintRow(FixAccounts.DashLine80Normal, PrintPix, 1, General.FontRegular);
                            else
                                row = new PrintRow(FixAccounts.DashLine60Normal, PrintPix, 1, General.FontRegular);
                            PrintBill.Rows.Add(row);

                            row = new PrintRow(PageFooter.ContinueItem.Text, PageFooter.ContinueItem.Row, PageFooter.ContinueItem.Column, PageFooter.ContinueItem.Font);
                            PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight);
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            CurrentPageNo += 1;
                            PrintHeader(CurrentPageNo, totalpages, BillType, BillNo, BillDate, PatientName, PatientAddress, PatientAddress2, PatientTelephone, PatientVATTIN, PatientDLN,PatientLBT , DoctorName, DoctorAddress, SaleSubType);
                            rowcount = 0;
                            PrintRowPixel = generalPrintSettings.ContentStartRow;
                        }
                        PrintRowPixel += 13;
                        rowcount += 1;
                        int colpix = 1;
                       // int mqty = 0;
                        int mlen = 0;
                        double mamt = 0;
                        string mproductname = "";
                        int uom = 0;
                        try
                        {
                            EcoMart.Printing.PageContentForDistributor PageContent;

                            PageContent = General.PrintSettingsForDistributor.SaleBillPrintSettingsPlainPaperForDistributor.PageContentForDistributor;

                            //else
                            //{
                            //    PageContent = General.PrintSettings.DebitNotePrintSettingsPlainPaper.PageContent;
                            //}
                            for (int i = 0; i < PageContent.ColumnCount; i++)
                            {
                                EcoMart.Printing.PageColumnForDistributor column;
                                column = PageContent.Columns[i];
                                if (column.Show == true)
                                {
                                    if (column.ColumnDataType == EcoMart.Printing.ColumnDataType.Integer)
                                    {

                                        if (dr.Cells[column.ColumnDataField].Value != null && Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString()) != 0)
                                        {
                                            string ss = (column.ColumnDataField).ToString();
                                            if (ss != "Col_UnitOfMeasure")
                                            {
                                                
                                                uom = Convert.ToInt32(dr.Cells[column.ColumnDataField].Value.ToString());


                                                mlen = uom.ToString("#0").Length;

                                                colpix = Convert.ToInt32(column.Column + ((5 - Convert.ToInt32(mlen)) * 5.5));
                                               // string pack = dr.Cells["Col_Pack"].Value.ToString().PadRight(3).Substring(0, 3);
                                                row = new PrintRow(uom.ToString("#0"), PrintRowPixel, colpix, column.Font);
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
                                                row = new PrintRow(mproductname, PrintRowPixel, colpix, column.Font);
                                                PrintBill.Rows.Add(row);
                                            }
                                            else if (ss == "Col_Pack")
                                            {
                                                string mpak = dr.Cells["Col_Pack"].Value.ToString();
                                                if (mpak.Length < 4)
                                                    mpak = mpak.PadRight(4);
                                                else
                                                    mpak = mpak.Substring(0, 4);                                               
                                                colpix = column.Column;
                                                row = new PrintRow(mpak, PrintRowPixel, colpix, column.Font);
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
                        catch (Exception Ex)
                        {
                            Log.WriteException(Ex);
                        }
                    }//End of For each Rows
                }
           


                PrintFooter(Narration1,Narration2, GrossAmount,DiscountAmount, VAT12Point5Amount,VAT5Amount,ADDAmount,LESSAmount, NetAmount,  CNAmount, DNAmount,  BalanceAmount,ShopDLN, ShopLBT,TotalItemDiscount,TotalSchemeDiscount,RoundUP);


                PrintBill.Print_Bill(generalPrintSettings.PageWidth, generalPrintSettings.PageHeight);
                if (IFSaleBill == "S")
                {
                    PrintFactory.SendLineFeed(General.PrintSettings.SaleBillPrintSettingsPlainPaper.GeneralSettings.LineFeed);
                }
                else
                {
                    PrintFactory.SendLineFeed(General.PrintSettings.DebitNotePrintSettingsPlainPaper.GeneralSettings.LineFeed);
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        private void PrintHeader(int currentPageNo, int TotalPages, string BillType, string BillNo, string BillDate, string PatientName, string PatientAddress, string PatientAddress2, string PatientTelephone, string PatientVATTINV, string PatientDLN, string PatientLBT, string DoctorName, string DoctorAddress, string SaleSubType)
        {
            PrintRow row;
            EcoMart.Printing.PageHeaderForDistributor PageHeader;
            try
            {
                if (SaleSubType == string.Empty)
                    SaleSubType = " ";
                //PrintFactory.SendReverseLineFeed(6);
               
                    PageHeader = General.PrintSettingsForDistributor.SaleBillPrintSettingsPlainPaperForDistributor.PageHeaderForDistributor;
                //}
                //else
                //{
                //    PageHeader = General.PrintSettings.DebitNotePrintSettingsPlainPaper.PageHeader;
                //}

                if (PageHeader.ShopName.Show)
                {
                    row = new PrintRow(General.ShopDetail.ShopName, PageHeader.ShopName.Row, PageHeader.ShopName.Column, PageHeader.ShopName.Font);
                    PrintBill.Rows.Add(row);
                }
                row = new PrintRow("TAX INVOICE", PageHeader.ShopName.Row, PageHeader.ShopName.Column+400, PageHeader.ShopName.Font);
                PrintBill.Rows.Add(row);
               
                //if (IFSaleBill == "W")
                //{
                //    if (BillType == FixAccounts.VoucherTypeForDistributorSaleCash)
                //        row = new PrintRow("WholeSale "+PageHeader.VoucherTypeSCA.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCA.Row, PageHeader.VoucherTypeSCA.Column, PageHeader.VoucherTypeSCA.Font);
                //    else if (BillType == FixAccounts.VoucherTypeForDistributorSaleCreditStatement)
                //        row = new PrintRow("WholeSale " + PageHeader.VoucherTypeSCS.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCS.Row, PageHeader.VoucherTypeSCS.Column, PageHeader.VoucherTypeSCS.Font);
                //    else
                //        row = new PrintRow("WholeSale " + PageHeader.VoucherTypeSCR.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeSCR.Row, PageHeader.VoucherTypeSCR.Column, PageHeader.VoucherTypeSCR.Font);
                //}
                //else
                //{
                    if (IFSaleBill == "C")
                        row = new PrintRow(PageHeader.VoucherTypeCreditNote.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeCreditNote.Row, PageHeader.VoucherTypeCreditNote.Column, PageHeader.VoucherTypeCreditNote.Font);
                    else if (IFSaleBill == "D")
                        row = new PrintRow(PageHeader.VoucherTypeDebitNote.Text + BillNo.Trim() + "-" + SaleSubType.ToString().Trim(), PageHeader.VoucherTypeDebitNote.Row, PageHeader.VoucherTypeDebitNote.Column, PageHeader.VoucherTypeDebitNote.Font);
                //}
                PrintBill.Rows.Add(row);

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
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PageHeader.Time.Row, PageHeader.Time.Column, PageHeader.Time.Font);
                PrintBill.Rows.Add(row);
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

                row = new PrintRow(PatientName, PageHeader.PatientName.Row, PageHeader.PatientName.Column, PageHeader.PatientName.Font);
                PrintBill.Rows.Add(row);
                row = new PrintRow(PatientAddress, PageHeader.PatientAddress1.Row, PageHeader.PatientAddress1.Column, PageHeader.PatientAddress1.Font);
                PrintBill.Rows.Add(row);
                row = new PrintRow(PatientAddress2, PageHeader.PatientAddress2.Row, PageHeader.PatientAddress2.Column, PageHeader.PatientAddress2.Font);
                PrintBill.Rows.Add(row);
                if (PageHeader.PatientTelephone.Show)
                {
                    row = new PrintRow(PageHeader.PatientTelephone.Text.ToString() + " " + PatientTelephone, PageHeader.PatientTelephone.Row, PageHeader.PatientTelephone.Column, PageHeader.PatientTelephone.Font);
                    PrintBill.Rows.Add(row);
                }
                row = new PrintRow(PageHeader.PatientVATTINV.Text.ToString() + " " + PatientVATTINV , PageHeader.PatientVATTINV.Row, PageHeader.PatientVATTINV.Column, PageHeader.PatientVATTINV.Font);
                PrintBill.Rows.Add(row);               
                row = new PrintRow(PageHeader.PartyDLN.Text.ToString() + " " + PatientDLN, PageHeader.PartyDLN.Row, PageHeader.PartyDLN.Column, PageHeader.PartyDLN.Font);
                PrintBill.Rows.Add(row);              
                row = new PrintRow(PageHeader.PartyLBT.Text.ToString() + " " + PatientLBT, PageHeader.PartyLBT.Row, PageHeader.PartyLBT.Column, PageHeader.PartyLBT.Font);
                PrintBill.Rows.Add(row);
                string page = currentPageNo.ToString().Trim() + "/" + TotalPages.ToString().Trim();
                row = new PrintRow(PageHeader.PageNo.Text.ToString() + " " + page, PageHeader.PageNo.Row, PageHeader.PageNo.Column, PageHeader.PageNo.Font);
                PrintBill.Rows.Add(row);

                if (PageHeader.DoctorName.Show)
                {
                    row = new PrintRow(PageHeader.DoctorName.Text.ToString() + " " + DoctorName.Trim(), PageHeader.DoctorName.Row, PageHeader.DoctorName.Column, PageHeader.DoctorName.Font);
                    PrintBill.Rows.Add(row);
                }

                if (PageHeader.DoctorAddress.Show)
                {
                    row = new PrintRow(PageHeader.DoctorAddress.Text.ToString() + " " + DoctorAddress.Trim(), PageHeader.DoctorAddress.Row, PageHeader.DoctorAddress.Column, PageHeader.DoctorAddress.Font);
                    PrintBill.Rows.Add(row);
                }

                int PrintRowPixel = PageHeader.PageNo.Row+20;

                if (generalPrintSettings.PageWidth > 700)
                    row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, General.FontRegular);
                else
                    row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 12;
                EcoMart.Printing.PageContentForDistributor PageContent;

                PageContent = General.PrintSettingsForDistributor.SaleBillPrintSettingsPlainPaperForDistributor.PageContentForDistributor;
          
                for (int i = 0; i < PageContent.ColumnCount; i++)
                {
                    if (PageContent.Columns[i].Show == true)
                    {
                        row = new PrintRow(PageContent.Columns[i].ColumnHeader, PrintRowPixel, PageContent.Columns[i].Column, PageContent.Columns[i].Font);
                        PrintBill.Rows.Add(row);
                    }
                }

                PrintRowPixel += 12;

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


        private void PrintFooter(string Narration1, string Narration2, double GrossAmount, double DiscountAmount, double Vat12point5Amount, double Vat5Amount, double AddAmount, double LessAmount, double NetAmount, double CNAmount, double DNAmount, double BalanceAmount, string ShopDLN, string ShopLBT, double itemDiscount, double schemeDiscount, double roundUP)
        {
            PrintRow row;
            PageFooterForDistributor PageFooter;
            try
            {
                PageFooter = General.PrintSettingsForDistributor.SaleBillPrintSettingsPlainPaperForDistributor.PageFooterForDistributor;
                if (PageFooter.NarrationItem.Show)
                {
                  //  string nn = Concat(Narration1.Trim(), " ", Narration2.Trim());
                    row = new PrintRow(Narration1.Trim()+ " "+Narration2.Trim(), PageFooter.NarrationItem.Row, PageFooter.NarrationItem.Column, PageFooter.NarrationItem.Font);
                    PrintBill.Rows.Add(row);
                }
             
                if (PageFooter.BalanceAmountItem.Show )
                {
                    row = new PrintRow(PageFooter.BalanceAmountItem.Text + BalanceAmount.ToString("#0.00"), PageFooter.BalanceAmountItem.Row, PageFooter.BalanceAmountItem.Column, PageFooter.BalanceAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                int PrintRowPixel = PageFooter.BalanceAmountItem.Row + 17;
                if (generalPrintSettings.PageWidth > 700)
                    row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, General.FontRegular);
                else
                    row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                PrintBill.Rows.Add(row);

                if (PageFooter.DLNItem.Show)
                {
                    row = new PrintRow(PageFooter.DLNItem.Text.ToString() + " " + ShopDLN, PageFooter.DLNItem.Row, PageFooter.DLNItem.Column, PageFooter.DLNItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.VATTINVItem.Show)
                {
                    row = new PrintRow(PageFooter.VATTINVItem.Text.ToString() + " " + General.ShopDetail.ShopVATTINV.ToString(), PageFooter.VATTINVItem.Row, PageFooter.VATTINVItem.Column, PageFooter.VATTINVItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.GrossAmountItem.Show)
                {
                    row = new PrintRow(PageFooter.GrossAmountItem.Text.ToString() + GrossAmount.ToString("#0.00"), PageFooter.GrossAmountItem.Row, PageFooter.GrossAmountItem.Column, PageFooter.GrossAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.SubjectItem.Show == true)
                {
                    row = new PrintRow(PageFooter.SubjectItem.Text.ToString() + " " + General.ShopDetail.ShopJurisdiction.ToString(), PageFooter.SubjectItem.Row, PageFooter.SubjectItem.Column, PageFooter.SubjectItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.VATTINCItem.Show == true)
                {
                    row = new PrintRow(PageFooter.VATTINCItem.Text.ToString() + " " + General.ShopDetail.ShopVATTINC.ToString(), PageFooter.VATTINCItem.Row, PageFooter.VATTINCItem.Column, PageFooter.VATTINCItem.Font);
                    PrintBill.Rows.Add(row);
                }                             
              
                if (PageFooter.DiscountItem.Show)
                {
                    row = new PrintRow(PageFooter.DiscountItem.Text.ToString()+ DiscountAmount.ToString("#0.00"), PageFooter.DiscountItem.Row, PageFooter.DiscountItem.Column, PageFooter.DiscountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.DeclarationItem1.Show)
                {
                    row = new PrintRow(PageFooter.DeclarationItem1.Text.ToString(), PageFooter.DeclarationItem1.Row, PageFooter.DeclarationItem1.Column, PageFooter.DeclarationItem1.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.LBTItem.Show)
                {
                    row = new PrintRow(PageFooter.LBTItem.Text.ToString() + " " + ShopLBT, PageFooter.LBTItem.Row, PageFooter.LBTItem.Column, PageFooter.LBTItem.Font);
                    PrintBill.Rows.Add(row);
                }

                if (PageFooter.Vat12point5Item.Show)
                {
                    row = new PrintRow( PageFooter.Vat12point5Item.Text.ToString()+ Vat12point5Amount.ToString("#0.00"), PageFooter.Vat12point5Item.Row, PageFooter.Vat12point5Item.Column, PageFooter.Vat12point5Item.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.DeclarationItem2.Show)
                {
                    row = new PrintRow(PageFooter.DeclarationItem2.Text.ToString(), PageFooter.DeclarationItem2.Row, PageFooter.DeclarationItem2.Column, PageFooter.DeclarationItem2.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.Vat5Item.Show)
                {
                    row = new PrintRow(PageFooter.Vat5Item.Text.ToString() + Vat5Amount.ToString("#0.00"), PageFooter.Vat5Item.Row, PageFooter.Vat5Item.Column, PageFooter.Vat5Item.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.DeclarationItem3.Show)
                {
                    row = new PrintRow(PageFooter.DeclarationItem3.Text.ToString(), PageFooter.DeclarationItem3.Row, PageFooter.DeclarationItem3.Column, PageFooter.DeclarationItem3.Font);
                    PrintBill.Rows.Add(row);
                }              
             
                if (PageFooter.SchemeDiscountItem.Show )
                {
                    row = new PrintRow(PageFooter.SchemeDiscountItem.Text +  schemeDiscount.ToString("#0.00"), PageFooter.SchemeDiscountItem.Row, PageFooter.SchemeDiscountItem.Column, PageFooter.SchemeDiscountItem.Font);
                    PrintBill.Rows.Add(row);
                }

                if (PageFooter.ItemDiscountItem.Show)
                {
                    row = new PrintRow(PageFooter.ItemDiscountItem.Text + itemDiscount.ToString("#0.00"), PageFooter.ItemDiscountItem.Row, PageFooter.ItemDiscountItem.Column, PageFooter.ItemDiscountItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.RoundupItem.Show)
                {
                    row = new PrintRow(PageFooter.RoundupItem.Text + roundUP.ToString("#0.00"), PageFooter.RoundupItem.Row, PageFooter.RoundupItem.Column, PageFooter.RoundupItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.DebitNoteItem.Show && DNAmount > 0)
                {
                    row = new PrintRow(PageFooter.DebitNoteItem.Text + DNAmount.ToString("#0.00"), PageFooter.DebitNoteItem.Row, PageFooter.DebitNoteItem.Column, PageFooter.DebitNoteItem.Font);
                    PrintBill.Rows.Add(row);
                }
                if (PageFooter.CreditNoteItem.Show && CNAmount > 0)
                {
                    row = new PrintRow(PageFooter.CreditNoteItem.Text + CNAmount.ToString("#0.00"), PageFooter.CreditNoteItem.Row, PageFooter.CreditNoteItem.Column, PageFooter.CreditNoteItem.Font);
                    PrintBill.Rows.Add(row);
                }
                //if (PageFooter.DiscountItem.Show && DNAmount > 0)
                //{
                //    row = new PrintRow(PageFooter.DebitNoteItem.Text + DNAmount.ToString("#0.00"), PageFooter.DebitNoteItem.Row, PageFooter.DebitNoteItem.Column, PageFooter.DebitNoteItem.Font);
                //    PrintBill.Rows.Add(row);
                //}
                if (PageFooter.NetAmountItem.Show)
                {
                    row = new PrintRow(PageFooter.NetAmountItem.Text + NetAmount.ToString("#0.00"), PageFooter.NetAmountItem.Row, PageFooter.NetAmountItem.Column, PageFooter.NetAmountItem.Font);
                    PrintBill.Rows.Add(row);
                }

                //PrintRowPixel = PageFooter.NetAmountItem.Row + 13;
                //if (generalPrintSettings.PageWidth > 700)
                //    row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, General.FontRegular);
                //else
                //    row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 1, General.FontRegular);
                //PrintBill.Rows.Add(row);

                row = new PrintRow(PageFooter.ShopNameItem.Text.ToString() + " " + General.ShopDetail.ShopName.ToString(), PageFooter.ShopNameItem.Row, PageFooter.ShopNameItem.Column, PageFooter.ShopNameItem.Font);
                PrintBill.Rows.Add(row);

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
