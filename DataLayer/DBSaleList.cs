using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public  class DBSaleList
    {
        #region Constructor
        public DBSaleList()
        {
        }
        #endregion

        public DataTable ReadProductDetailsByProductID()
        {
            DataTable dt = null;         
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.Expiry,a.ExpiryDate,a.Quantity,a.VATAmount,a.Amount,b.ID,b.AccountID,b.VoucherType, "+
                                "b.VoucherNumber,b.VoucherDate,c.AccountID,c.AccName,c.AccAddress1, c.AccAddress2 from detailsale a  inner join vouchersale b "+
                                "on A.MasterSaleID = B.ID inner join masteraccount c on b.AccountID = c.AccountID";
            
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
      
        public DataTable GetCreditNoteDataProfitPercentDay (string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select c.VoucherDate, sum(a.Quantity * (a.ReturnRate/b.ProdLoosePack)) as AmountNet,sum(a.Quantity * ((a.PurchaseRate+(a.TradeRate*b.ProdVatPercent/100))/b.ProdLoosePack)) as AmountByPurchaseRate,sum(a.Quantity * ((a.ReturnRate - (a.PurchaseRate+(a.TradeRate*b.ProdVatPercent/100)))/b.ProdLoosePack)) as ProfitInRupees,sum((a.Quantity * ((a.SaleRate - (a.PurchaseRate+(a.TradeRate*b.ProdVatPercent/100)))/b.ProdLoosePack))/(a.Quantity * (a.salerate/b.ProdLoosePack))) as ProfitPercentBySaleRate,sum((a.Quantity * ((a.SaleRate - (a.PurchaseRate+(a.TradeRate*b.ProdVatPercent/100)))/b.ProdLoosePack))/(a.Quantity * ((a.purchaserate+(a.TradeRate*b.ProdVatPercent/100))/b.ProdLoosePack))) as ProfitPercentByPurchaseRate from detailcreditdebitnotestock a " +
                               " inner join vouchercreditdebitnote c on a.MasterID = c.CRDBID inner join masterproduct b on a.ProductID = b.ProductID  where c.voucherdate >= '" + fromdate + "' AND c.voucherdate <= '" + todate + "' AND c.voucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "'   group by c.VoucherDate ";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable GetOverviewDataProfitPercentDay(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select VoucherDate, sum(AmountNet) as AmountNet,sum(AmountNet - ProfitInRupees) as AmountByPurchaseRate,sum(ProfitInRupees) as ProfitInRupees,sum(ProfitPercentBySaleRate) as ProfitPercentBySaleRate,sum(ProfitPercentByPurchaseRate) as ProfitPercentByPurchaseRate from vouchersale " +
                               " where voucherdate >= '" + fromdate + "' AND voucherdate <= '" + todate + "' group by voucherdate ";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;

        }
        public DataTable GetOverviewDataProfitPercentParty(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.accountId,a.patientID,a.VoucherDate, sum(a.AmountNet) as AmountNet,sum(a.AmountNet - a.ProfitInRupees) as AmountByPurchaseRate, " +
                    " sum(a.ProfitInRupees) as ProfitInRupees,sum(a.ProfitPercentBySaleRate) as ProfitPercentBySaleRate,sum(a.ProfitPercentByPurchaseRate) as ProfitPercentByPurchaseRate , " +
                    "b.AccName,b.AccAddress1 from vouchersale  a inner join masteraccount b  on a.AccountID = b.AccountID where a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "' AND PatientID = '' AND a.AccountID != '' group by a.AccountID ";
               

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable GetOverviewDataForGSTReportHSN(object fromdate, object todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "";
            strSql = "Select a.ID,a.VoucherNumber,a.VoucherType,a.VoucherSubType,a.VoucherDate,c.ProductID,c.ProdName,c.HSNNumber,b.ProductID,b.VATPer,b.CashDiscountAmount,b.GSTAmountZero,b.GSTSAmount,b.GSTCAmount,b.GSTIAmount,b.GSTS,b.GSTC,b.GSTI from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID inner join masterproduct c on b.ProductID = c.ProductID where a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForGSTReport(object fromdate, object todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "";
            strSql = "select ID,VoucherNumber,VoucherType,VoucherSubType,VoucherDate,AmountNet,(AmountCreditNote) as TotalLess,(AmountDebitNote) as TotalAdd,AmountCashDiscount,AmountGST0,AmountGSTS5,GSTS5,AmountGSTS12,GSTS12,AmountGSTS18,GSTS18,AmountGSTS28,GSTS28,AmountGSTC5,GSTC5,AmountGSTC12,GSTC12,AmountGSTC18,GSTC18,AmountGSTC28,GSTC28, AmountCreditNote,AmountDebitNote,roundingAmount from vouchersale a where a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable ReadSaleDetailsForVATRegister(string  fromdate , string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select b.ID,b.VoucherType, b.VoucherSubType," +
                                    "b.VoucherNumber,b.VoucherDate,b.AmountForZeroVAT, AmountVAT5Per,b.VAT5Per,AmountVAT12point5Per,b.VAT12Point5Per,(b.AmountCashDiscount+b.AmountCreditNote) as TotalLess, b.AmountCashDiscount,b.AmountCreditNote,(b.AddOnFreight+b.AmountDebitNote) as TotalAdd, b.AddOnFreight,b.AmountDebitNote,b.RoundingAmount,b.AmountNet,b.PatientName as AccName,b.PatientAddress1 as AccAddress1   from  vouchersale b " +
                                    " where voucherdate >= '" + fromdate + "' AND voucherdate <= '" + todate + "' order by b.VoucherDate,b.vouchertype,b.vouchernumber ";                                 

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable ReadSaleDetailsForVATRegister(string fromdate, string todate, string accountID)
        {
            DataTable dt = null;
            {
                string strSql = "select b.ID,b.VoucherType, b.VoucherSubType," +
                                    "b.VoucherNumber,b.VoucherDate,b.AmountForZeroVAT, AmountGross,AmountVAT5Per,b.VAT5Per,AmountVAT12point5Per,b.VAT12Point5Per,(b.AmountCashDiscount+b.AmountCreditNote) as TotalLess, b.AmountCashDiscount,b.AmountCreditNote,(b.AddOnFreight+b.AmountDebitNote) as TotalAdd, b.AddOnFreight,b.AmountDebitNote,b.RoundingAmount,b.AmountNet,b.PatientName as AccName,b.PatientAddress1 as AccAddress1   from  vouchersale b " +
                                    " where voucherdate >= '" + fromdate + "' AND voucherdate <= '" + todate + "' AND AccountID = '"+ accountID +"'";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable ReadSaleDetailsForVATRegisterDATE(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select b.ID,b.VoucherType, " +
                                    "b.VoucherDate,sum(b.AmountForZeroVAT) as AmountForZeroVAT,sum(AmountVAT5Per) as AmountVAT5Per,sum(b.VAT5Per) as VAT5PEr,sum(AmountVAT12point5Per) as AmountVAT12point5per,sum(b.VAT12Point5Per) as VAT12Point5Per,sum(b.AmountCashDiscount+b.AmountCreditNote) as TotalLess, sum(b.AddOnFreight+b.AmountDebitNote) as TotalAdd, sum(b.AddOnFreight) as AddOnFreight,sum(b.AmountDebitNote) as AmountDebitNote,sum(b.RoundingAmount) as RoundingAmount,sum(b.AmountNet) as AmountNet,b.PatientName as AccName,b.PatientAddress1 as AccAddress1   from  vouchersale b " +
                                    " where voucherdate >= '" + fromdate + "' AND voucherdate <= '" + todate + "' group by vouchertype,VoucherDate order by VoucherDate ";
                     dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable ReadSaleDetailsForVATRegisterDATEALL(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select b.ID,b.VoucherType, " +
                                    "b.VoucherDate,sum(b.AmountForZeroVAT) as AmountForZeroVAT,sum(AmountVAT5Per) as AmountVAT5Per,sum(b.VAT5Per) as VAT5PEr,sum(AmountVAT12point5Per) as AmountVAT12point5per,sum(b.VAT12Point5Per) as VAT12Point5Per,sum(b.AmountCashDiscount+b.AmountCreditNote) as TotalLess, sum(b.AddOnFreight+b.AmountDebitNote) as TotalAdd, sum(b.AddOnFreight) as AddOnFreight,sum(b.AmountDebitNote) as AmountDebitNote,sum(b.RoundingAmount) as RoundingAmount,sum(b.AmountNet) as AmountNet,b.PatientName as AccName,b.PatientAddress1 as AccAddress1   from  vouchersale b " +
                                    " where voucherdate >= '" + fromdate + "' AND voucherdate <= '" + todate + "' group by VoucherDate order by VoucherDate ";
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable ReadSaleDetailsForVATRegisterMONTH(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select b.ID,b.VoucherType, " +
                                    "b.VoucherDate,sum(b.AmountForZeroVAT) as AmountForZeroVAT,sum(AmountVAT5Per) as AmountVAT5Per,sum(b.VAT5Per) as VAT5PEr,sum(AmountVAT12point5Per) as AmountVAT12point5per,sum(b.VAT12Point5Per) as VAT12Point5Per,sum(b.AmountCashDiscount+b.AmountCreditNote) as TotalLess, sum(b.AddOnFreight+b.AmountDebitNote) as TotalAdd, sum(b.AddOnFreight) as AddOnFreight,sum(b.AmountDebitNote) as AmountDebitNote,sum(b.RoundingAmount) as RoundingAmount,sum(b.AmountNet) as AmountNet,b.PatientName as AccName,b.PatientAddress1 as AccAddress1   from  vouchersale b " +
                                    " where voucherdate >= '" + fromdate + "' AND voucherdate <= '" + todate + "' group by vouchertype, substring(VoucherDate,5,2) order by VoucherDate ";
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable ReadSaleDetailsForVATRegisterMONTHALL(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select b.ID,b.VoucherType, " +
                                    "b.VoucherDate,sum(b.AmountForZeroVAT) as AmountForZeroVAT,sum(AmountVAT5Per) as AmountVAT5Per,sum(b.VAT5Per) as VAT5PEr,sum(AmountVAT12point5Per) as AmountVAT12point5per,sum(b.VAT12Point5Per) as VAT12Point5Per,sum(b.AmountCashDiscount+b.AmountCreditNote) as TotalLess, sum(b.AddOnFreight+b.AmountDebitNote) as TotalAdd, sum(b.AddOnFreight) as AddOnFreight,sum(b.AmountDebitNote) as AmountDebitNote,sum(b.RoundingAmount) as RoundingAmount,sum(b.AmountNet) as AmountNet,b.PatientName as AccName,b.PatientAddress1 as AccAddress1   from  vouchersale b " +
                                    " where voucherdate >= '" + fromdate + "' AND voucherdate <= '" + todate + "' group by substr(b.VoucherDate,5,2) order by b.VoucherDate ";
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable ReadSaleDetailsForVATRegisterTIN(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select b.ID,b.VoucherDate,sum(AmountVAT5Per + AmountVAT12point5Per) as TotalAmount,sum(b.VAT5Per+ b.VAT12Point5Per) as TotalVAT,sum(b.VAT5Per) as VAT5Per,sum(b.VAT12Point5Per) as VAT12Point5Per,b.PatientName as AccName, b.PatientAddress1 as AccAddress1 ,c.AccVATTIN   from  vouchersale b  left outer join masteraccount c on  b.AccountID = c.AccountID " +
                                    " where voucherdate >= '" + fromdate + "' AND voucherdate <= '" + todate + "' group by b.AccountID  order by TotalVAT desc";
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable ReadSaleDetailsForVATRegisterTINDetail(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select b.ID,b.VoucherType,b.VoucherNumber,b.VoucherDate,(AmountVAT5Per + AmountVAT12point5Per) as TotalAmount,(b.VAT5Per+ b.VAT12Point5Per) as TotalVAT,(b.VAT5Per) as VAT5Per,(b.VAT12Point5Per) as VAT12Point5Per,b.AmountForZeroVAT,b.PatientName as AccName, b.PatientAddress1 as AccAddress1 ,c.AccVATTIN   from  vouchersale b  left outer join masteraccount c on  b.AccountID = c.AccountID " +
                                    " where voucherdate >= '" + fromdate + "' AND voucherdate <= '" + todate + "' order by VoucherNumber";
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable GetSaleDataForGivenProduct(int ProductID)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.Expiry,a.ExpiryDate,a.Quantity,a.VATAmount,a.Amount,b.ID,b.AccountID,b.VoucherType,b.VoucherSubType, " +
                                "b.VoucherNumber,b.VoucherDate,b.PatientName,b.PatientAddress1 as Address,c.AccCode from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID   where a.ProductID = '"+ ProductID+"' order by b.VoucherDate,b.VoucherType,b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        //start by snehal 
        public DataTable GetCNFSaleDataForGivenProduct(int ProductID)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.Expiry,a.ExpiryDate,a.Quantity,a.VATAmount,a.Amount,b.ID,b.AccountID,b.VoucherType,b.VoucherSubType, " +
                                "b.VoucherNumber,b.VoucherDate,b.PatientName,b.PatientAddress1 as Address,c.AccCode from detailsale_CS a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  where a.CNFID = '" + ProductID + "' order by b.VoucherDate,b.VoucherType,b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        //end by snehal
        public DataTable GetSaleDataForH1(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.Expiry,a.ExpiryDate,a.Quantity,a.VATAmount,a.Amount,b.ID,b.PatientName as PatientShortName,b.DoctorShortName,b.VoucherType,b.VoucherSubType, " +
                                "b.VoucherNumber,b.VoucherDate,b.MobileNumberForSMS,c.ProdName,c.ProdLoosePack,c.ProdPack from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID inner join masterproduct c on a.ProductID = c.ProductID  where c.ProdScheduleDrugCode = 'H1' AND b.voucherdate >= '" + fromdate + "' AND b.voucherdate <= '" + todate + "' order by b.VoucherDate,b.VoucherType,b.VoucherNumber";



                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable GetSaleDataForGivenProductBatchMrp(int ProductID, string batch, double mrp, string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.Expiry,a.ExpiryDate,a.Quantity,a.VATAmount,a.Amount,b.ID,b.AccountID,b.VoucherType,b.VoucherSubType, " +
                                "b.VoucherNumber,b.VoucherDate,b.PatientName,b.PatientAddress1 as Address,c.AccCode from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID   where a.ProductID = '" + ProductID + "' AND a.BatchNumber = '"+batch +"' AND a.MRP = "+mrp +" AND b.VoucherDate >= '"+ fromdate +"' AND b.VoucherDate <= '"+ todate +"' order by b.VoucherDate, b.Vouchertype, b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        // start by snehal 
        public DataTable GetCNFSaleDataForGivenProductBatchMrp(int ProductID)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.Expiry,a.ExpiryDate,a.Quantity,a.VATAmount,a.Amount,b.ID,b.AccountID,b.VoucherType,b.VoucherSubType, " +
                                "b.VoucherNumber,b.VoucherDate,b.PatientName,b.PatientAddress1 as Address,c.AccCode from detailsale_CS a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID    where a.ProductID = '" + ProductID + "' order by b.VoucherDate, b.Vouchertype, b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        // end by snehal
        public DataTable GetSaleDataForScheduledProducts(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.SaleRate, " +
                                "a.Expiry,a.Quantity,a.Amount,b.ID,b.AccountID,b.VoucherType, " +
                                "b.VoucherNumber,b.VoucherDate,b.PatientName,b.PatientAddress1,c.AccCode,d.ProductID, " + 
                                "d.ProdName,d.ProdLoosePack,d.ProdPack,d.ProdCompShortName from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where d.ProdIfSchedule = 'Y' AND b.VoucherDate >= '"+ fromdate + "' AND b.Voucherdate <= '"+ todate +"' order by d.ProdName";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable GetSaleDataForDailyProducts(string fromdate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.SaleRate, " +
                                "a.Expiry,a.Quantity,a.Amount,a.StockID,b.ID,b.VoucherType,b.CreatedUserID, " +
                                "b.VoucherNumber,b.VoucherDate,b.VoucherSubType,b.PatientShortName,c.UserName,d.ProductID, " +
                                "d.ProdName,d.ProdLoosePack,d.ProdPack,d.ProdCompShortName,d.ProdClosingStock as ClosingStock from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join tbluser c on b.CreatedUserID = c.UserID  inner join masterproduct d on a.ProductID = d.ProductID inner join tblstock e on a.StockID = e.StockID  where b.VoucherDate = '" + fromdate + "' order by b.VoucherType , b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable GetSaleDataForPatient(string patient, string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.SaleRate, " +
                                "a.Expiry,a.Quantity,a.Amount,b.ID,b.AccountID,b.VoucherType, " +
                                "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.PatientName,b.PatientAddress1,c.AccCode,d.ProductID, " +
                                "d.ProdName,d.ProdLoosePack,d.ProdPack,d.ProdCompShortName from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where  b.PatientName like  '%"+ patient +"%' AND b.VoucherDate >= '" + fromdate + "' AND b.Voucherdate <= '" + todate + "' order by b.Vouchertype,b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable GetSaleDataForPatient(string patient, string fromdate, string todate, int ProductID)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.SaleRate, " +
                                "a.Expiry,a.Quantity,a.Amount,b.ID,b.AccountID,b.VoucherType, " +
                                "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.PatientName,b.PatientAddress1,c.AccCode,d.ProductID, " +
                                "d.ProdName,d.ProdLoosePack,d.ProdPack,d.ProdCompShortName from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where a.ProductID = '"+ ProductID +"' AND b.PatientName like  '%" + patient + "%' AND b.VoucherDate >= '" + fromdate + "' AND b.Voucherdate <= '" + todate + "' order by b.Vouchertype,b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable GetSaleDataForDoctor(string doctorid, string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.SaleRate, " +
                                "a.Expiry,a.Quantity,a.Amount,b.ID,b.AccountID,b.VoucherType, " +
                                "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.PatientName,b.PatientAddress1,c.AccCode,d.ProductID, " +
                                "d.ProdName,d.ProdLoosePack,d.ProdPack,d.ProdCompShortName from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where  b.DoctorID = '" +  doctorid + "' AND b.VoucherDate >= '" + fromdate + "' AND b.Voucherdate <= '" + todate + "' order by b.Vouchertype,b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable GetSaleDataForDoctorShowReport(string doctorid, string compID, string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.SaleRate, " +
                                "a.Expiry,a.Quantity,a.Amount,b.ID,b.AccountID,b.VoucherType, " +
                                "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.PatientName,b.PatientAddress1,c.AccCode,d.ProductID, " +
                                "d.ProdName,d.ProdLoosePack,d.ProdPack,d.ProdCompShortName,d.ProdCompID from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where  b.DoctorID = '" + doctorid + "' AND b.VoucherDate >= '" + fromdate + "' AND b.Voucherdate <= '" + todate + "' AND d.ProdCompID = '"+ compID +"' order by b.Vouchertype,b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable GetSaleDataForDoctor(string doctorid, string compid, string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.SaleRate, " +
                                "a.Expiry,a.Quantity,a.Amount,b.ID,b.AccountID,b.VoucherType, " +
                                "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.PatientName,b.PatientAddress1,c.AccCode,d.ProductID, " +
                                "d.ProdName,d.ProdLoosePack,d.ProdPack,d.ProdCompShortName,d.ProdCompID from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where  b.DoctorID = '" + doctorid + "' AND d.ProdCompID = '"+ compid +"' AND b.VoucherDate >= '" + fromdate + "' AND b.Voucherdate <= '" + todate + "' order by b.Vouchertype,b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable GetSaleDataForDoctorCompanyDetail(string doctorid,string companyid,  string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.SaleRate, " +
                                "a.Expiry,a.Quantity,a.Amount,b.ID,b.AccountID,b.VoucherType, " +
                                "b.VoucherNumber,b.VoucherDate,b.PatientName,b.PatientAddress1,c.AccCode,d.ProductID, " +
                                "d.ProdName,d.ProdLoosePack,d.ProdPack,d.ProdCompShortName from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where  b.DoctorID = '" + doctorid + "' AND  d.ProdCompID = '"+ companyid +"' ANDb.VoucherDate >= '" + fromdate + "' AND b.Voucherdate <= '" + todate + "' order by b.Vouchertype,b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
     

        public DataTable GetOverviewDataForAllPartySummary(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.vouchertype, a.AccountID , sum(a.AmountNet) as AmountNet, a.PatientName, a.PatientAddress1 " +
                             "from vouchersale a inner join masteraccount b on a.AccountID = b.AccountId where a.AccountID != '" + FixAccounts.AccountCash + "' AND a.AccountID != '" + FixAccounts.AccountPendingCashBills + "' AND voucherdate >= '" + fromdate + "' AND  voucherdate <= '" + todate + "'  group by a.AccountID ,a.VoucherType, a.PatientName, a.PatientAddress1 order by a.patientname ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForDailySaleReport(string fromdate)
        {
            DataTable dt = null;
            try
            {
                {                   
                    string strSql = "select VoucherType, countersalenumber as VoucherNumber, sum(AmountNet) as Amount from vouchersale  " +
                                    " where VoucherSubType != '"+ FixAccounts.SubTypeForRegularSale + "' AND VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "' AND voucherdate = '" + fromdate + "' Group by countersalenumber, VoucherType order by VoucherType, VoucherNumber";

                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);
              
            }
       
            return dt;
        }
        public DataTable GetDetailsaleTotalForDayWiseVoucherSaleReport()
        {
            DataTable dt = null;
            try
            {
                {

                    string strSql = "select  VoucherDate, sum(Amount) as Amount from detailsale where vouchertype = '"+ FixAccounts.VoucherTypeForVoucherSale +"' group by VoucherDate";

                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);

            }

            return dt;
        }
        public DataTable GetOverviewDataForDailySaleReportDistributor(string fromdate)
        {
            DataTable dt = null;
            try
            {
                {

                    string strSql = "select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,a.AccName,a.AccAddress1,b.AmountNet as Amount, a.AccAddress2  from  vouchersale b  inner join masteraccount a on b.AccountID = a.AccountID " +
                                    "where b.VoucherSubType = '" + FixAccounts.SubTypeForRegularSale + "'  AND  b.voucherdate = '" + fromdate + "' order by b.VoucherNumber ";
                                   

                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }


        public DataTable GetOverviewDataForPartywiseSaleReport(string accID, string fromdate , string todate)
        {
            DataTable dt = null;
            try
            {
                {


                    string strSql = "select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.Narration,b.VoucherDate,b.PatientName as AccName,b.PatientAddress1 as AccAddress1,b.AmountNet as Amount, b.PatientAddress2 as AccAddress2  from  vouchersale b " +
                                    "where (b.AccountID = '" + accID + "' OR b.PatientID = '" + accID + "') AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' order by b.VoucherDate, b.VoucherNumber";
                                    

                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }

        public DataTable GetOverviewDataForPartywiseOutstandingSaleReport(string accID, string fromdate, string todate, string saleSubType)
        {
            DataTable dt = null;
            try
            {
                {
                    string strSql = "select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.AccountID,b.PatientName as AccName,b.PatientAddress1 as AccAddress1,b.AmountNet as Amount, b.AmountBalance, b.PatientAddress2 as AccAddress2,a.accopeningdebit,a.accopeningCredit,a.accclearedamount, 0 as onaccountAmount  from  vouchersale b " +
                                    "left outer join masteraccount a on b.AccountID = a.AccountID where (b.AccountID = '" + accID + "' OR b.PatientID = '" + accID + "') AND b.VoucherSubType = '" + saleSubType + "' AND b.AmountBalance > 0   AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' " +

                                    " union select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.AccountID,b.PatientName as AccName,b.PatientAddress1 as AccAddress1,b.AmountNet as Amount, b.AmountBalance, b.PatientAddress2 as AccAddress2,a.accopeningdebit,a.accopeningCredit,a.accclearedamount, 0 as onaccountAmount  from  tbloldvouchersale b " +
                                    "left outer join masteraccount a on b.AccountID = a.AccountID where (b.AccountID = '" + accID + "' OR b.PatientID = '" + accID + "') AND b.VoucherSubType = '" + saleSubType + "' AND b.AmountBalance > 0   AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' " +

                                    " union select  b.cbID  as ID , b.VoucherType, " +
                                   "b.VoucherNumber,'',b.VoucherDate,b.AccountID,a.AccName,a.AccAddress1,0,0,'',0,0,0,b.onaccountAmount  from  vouchercashbankreceipt b " +
                                   "left outer join masteraccount a on b.AccountID = a.AccountID where b.AccountID = '" + accID + "' AND  b.onAccountAmount > 0   AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' order by AccName, accAddress1,VoucherDate";
                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }
        public DataTable GetOverviewDataForPartywiseOutstandingSaleReport(string fromdate, string todate, string saleSubType)
        {
            DataTable dt = null;
            try
            {
                {
                    string strSql = "select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.AccountID,a.AccName,a.AccAddress1,b.AmountNet as Amount, b.AmountBalance, a.AccAddress2,a.accopeningdebit,a.accopeningCredit,a.accclearedamount, 0 as onaccountAmount from vouchersale b " +
                                    "inner join masteraccount a on b.AccountID = a.AccountID where b.VoucherSubType = '" + saleSubType + "' AND (b.AmountBalance > 0 OR a.AccOpeningDebit > 0 OR a.AccOpeningCredit > 0)  AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "'" +

                                    " union select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.AccountID,a.AccName,a.AccAddress1,b.AmountNet as Amount, b.AmountBalance, a.AccAddress2,a.accopeningdebit,a.accopeningCredit,a.accclearedamount, 0 as onaccountAmount  from  tbloldvouchersale b " +
                                    "inner join masteraccount a on b.AccountID = a.AccountID where b.VoucherSubType = '" + saleSubType + "' AND (b.AmountBalance > 0 OR a.AccOpeningDebit > 0 OR a.AccOpeningCredit > 0)   AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' " +

                                     " union select  b.cbID  as ID , b.VoucherType, " +
                                    "b.VoucherNumber,'',b.VoucherDate,b.AccountID,a.AccName,a.AccAddress1,0,0,'',0,0,0,b.onaccountAmount  from  vouchercashbankreceipt b " +
                                    "inner join masteraccount a on b.AccountID = a.AccountID where b.onAccountAmount > 0   AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' order by AccName, accAddress1,VoucherDate";
                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }
            return dt;
        }
        public DataTable GetOverviewDataForAreawiseOutstandingSaleReport(string accID, string fromdate, string todate, string saleSubType)
        {
            DataTable dt = null;
            try
            {
                {
                    string strSql = "select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,d.AccountID,d.AccName,d.AccAddress1, d.AccAddress2,d.AccAreaID,b.AmountNet as Amount, b.AmountBalance  from  vouchersale b " +
                                    "inner join masteraccount d on b.AccountID = d.AccountID  where (d.AccAreaID = '" + accID + "' ) AND b.VoucherSubType = '" + saleSubType + "' AND b.AmountBalance > 0   AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' " +


                                    " union select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,d.AccountID,d.AccName,d.AccAddress1, d.AccAddress2,d.AccAreaID,b.AmountNet as Amount, b.AmountBalance  from  tbloldvouchersale b " +
                                    "inner join masteraccount d on b.AccountID = d.AccountID  where (d.AccAreaID = '" + accID + "' ) AND b.VoucherSubType = '" + saleSubType + "' AND b.AmountBalance > 0   AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' order by AccName,AccAddress1";


                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }
            return dt;
        }
        public DataTable GetOverviewDataForAreawiseOutstandingSaleReport(string fromdate, string todate, string saleSubType)
        {
            DataTable dt = null;
            try
            {
                {
                    string strSql = "select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.AccountID,a.AccName,a.AccAddress1,b.AmountNet as Amount, b.AmountBalance, a.AccAddress2,a.accopeningdebit,a.accopeningCredit,a.accclearedamount  from  vouchersale b " +
                                    "inner join masteraccount a on b.AccountID = a.AccountID where b.VoucherSubType = '" + saleSubType + "' AND b.AmountBalance > 0   AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' order by b.PatientName, b.PatientAddress1,b.VoucherDate";
                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }
            return dt;
        }





        public DataTable GetOverviewDataForPartywiseSaleReportforPatient(string accID, string fromdate, string todate)
        {
            DataTable dt = null;
            try
            {
                {


                    string strSql = "select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.Narration,b.VoucherDate,b.PatientName as AccName,b.PatientAddress1 as AccAddress1,b.AmountNet as Amount, b.PatientAddress2 as AccAddress2  from  vouchersale b " +
                                    "where (b.AccountID = '" + accID + "' OR b.PatientID = '" + accID + "') AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' order by b.VoucherDate,b.VoucherNumber";


                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }

        public DataTable GetOverviewDataForPartywiseOutstandingSaleReportforPatient(string accID, string fromdate, string todate)
        {
            DataTable dt = null;
            try
            {
                {
                    string strSql = "select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.PatientName as AccName,b.PatientAddress1 as AccAddress1,b.AmountNet as Amount, b.PatientAddress2 as AccAddress2  from  vouchersale b " +
                                    "where (b.AccountID = '" + accID + "' OR b.PatientID = '" + accID + "') AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' "+

                                    " union select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.PatientName as AccName,b.PatientAddress1 as AccAddress1,b.AmountNet as Amount, b.PatientAddress2 as AccAddress2  from  tbloldvouchersale b " +
                                    "where (b.AccountID = '" + accID + "' OR b.PatientID = '" + accID + "') AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' order by VoucherDate,VoucherNumber";

                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }

        public DataTable GetOverviewDataForPartyProductListDebtor(string partyid, int ProductID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterSaleID,a.ProductID,a.BatchNumber,a.Quantity ,a.SaleRate, a.Amount, " +
                 "b.ID,b.VoucherNumber,b.VoucherType,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailsale a  inner join vouchersale b on a.MasterSaleID = b.ID inner join masteraccount c on b.Accountid = c.AccountID where c.AccountID = '" + partyid + "' AND a.ProductID = '" + ProductID + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPartyProductListPatient(string partyid, int ProductID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterSaleID,a.ProductID,a.BatchNumber,a.Quantity ,a.SaleRate, a.Amount, " +
                 "b.ID,b.VoucherNumber,b.VoucherType,b.VoucherDate,b.PatientID,b.PatientName,b.PatientAddress1 " +
                 "from detailsale a  inner join vouchersale b on a.MasterSaleID = b.ID where b.PatientID = '" + partyid + "' AND a.ProductID = '" + ProductID + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForRegularPartyProductSale(string partyid, int ProductID, string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterSaleID,a.ProductID,a.BatchNumber,a.Quantity ,a.SaleRate, a.Amount, " +
                 "b.ID,b.VoucherNumber,b.VoucherType,b.VoucherDate,c.AccountID,c.AccName,c.AccAddress1 " +
                 "from detailsale a  inner join vouchersale b on a.MasterSaleID = b.ID inner join masteraccount c on b.AccountID = c.AccountID where b.AccountID = '" + partyid + "' AND a.ProductID = '" + ProductID + "' AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForRegularPartyProductSalePatient(string partyid, int ProductID, string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterSaleID,a.ProductID,a.BatchNumber,a.Quantity ,a.SaleRate, a.Amount, " +
                 "b.ID,b.VoucherNumber,b.VoucherType,b.VoucherDate, " +
                 "from detailsale a  inner join vouchersale b on a.MasterSaleID = b.ID  order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadSalesRegisterData(string mfromdate, string mtodate, string mtype)
        {
            DataTable dt = null;
            string strSql = "select a.ID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate,a.VoucherSubType,a.AccountID,a.AmountNet,a.DoctorID,a.PatientID,a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2 from vouchersale a  where a.voucherdate >= '" + mfromdate + "' AND a.voucherdate <= '" + mtodate + "' AND a.voucherType = '" + mtype + "' order by a.voucherdate,a.vouchernumber";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable ReadSalesRegisterData(string mfromdate, string mtodate)
        {
            DataTable dt = null;
            string strSql = "select a.ID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate,a.VoucherSubType,a.AccountID,a.AmountNet,a.DoctorID,a.PatientID,a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2 from vouchersale a where a.voucherdate >= '" + mfromdate + "' AND a.voucherdate <= '" + mtodate + "' order by  a.voucherdate,a.vouchernumber";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable GetSaleDataCategory(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select sum(a.Amount) as AmountNet,a.ProductID,a.MasterSaleID, b.ProductID,b.ProdCategoryID,c.ProductCategoryID,c.ProductCategoryName,d.voucherdate from detailsale a inner join masterproduct b on a.ProductID = b.ProductID inner join masterproductcategory c on b.ProdCategoryID = c.ProductCategoryID inner join vouchersale d on a.MasterSaleID = d.ID  where d.voucherdate >= '" + mfromdate + "' AND d.voucherdate <= '" + mtodate + "' group by c.ProductCategoryID order by c.ProductCategoryName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetSaleDataDoctorCompanySummary(string doctorid, string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select sum(a.Amount) as Amount,b.ProdCompID,c.CompName,d.voucherdate,d.DoctorID from detailsale a inner join masterproduct b on a.ProductID = b.ProductID  inner join mastercompany c on b.ProdcompID = c.CompID  inner join vouchersale d on a.MasterSaleID = d.ID  where d.DoctorID = '"+ doctorid +"' AND d.voucherdate >= '" + mfromdate + "' AND d.voucherdate <= '" + mtodate + "' group by d.DoctorID,b.ProdCompID order by c.CompName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetSaleDataProductSummary(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select sum(a.Quantity) as Quantity, sum(a.Amount) as Amount,a.ProductID,MAX(b.ProdName) AS ProdName from detailsale a inner join masterproduct b on a.ProductID = b.ProductID  inner join vouchersale d on a.MasterSaleID = d.ID  where d.voucherdate >= '" + mfromdate + "' AND d.voucherdate <= '" + mtodate + "' group by a.ProductID order by ProdName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }




        public DataTable GetSaleDataFromToNumber(int mFromNumber, int mToNumber)
        {
            DataTable dt = null;
            string strSql = "select a.ID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate,a.VoucherSubType,a.AccountID,a.AmountNet,a.DoctorID,a.PatientID,a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.createdDate,a.CreatedTime from vouchersale a where a.vouchernumber >= " + mFromNumber + " AND a.vouchernumber <= " + mToNumber + " AND  a.vouchertype = '"+ FixAccounts.VoucherTypeForCashSale +"' order by  a.voucherdate,a.vouchernumber";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetSaleDataFromToNumber(string selectedID)
        {
            DataTable dt = null;
            string strSql = "select a.ID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate,a.VoucherSubType,a.AccountID,a.AmountNet,a.DoctorID,a.PatientID,a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.createdDate,a.CreatedTime,a.modifiedDate,a.ModifiedTime,changedID from changedvouchersale a where a.ID = '" + selectedID + "' order by  a.Modifieddate,a.modifiedTime";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetDataForCashCounter(string toDate)
        {
            DataTable dt = null;
            string strSql = "select a.ID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate,a.VoucherSubType,a.AccountID,a.AmountNet,a.DoctorID,a.PatientID,a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.createdDate,a.CreatedTime, COALESCE(a.CashCounter) from vouchersale a where a.VoucherDate = '" + toDate + "' AND a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' AND (a.cashCounter is null OR a.cashCounter != 'Y') order by  a.VoucherType, a.VoucherNumber";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public bool UpdateCashCounter(string saleID)
        {
            bool retValue = false;
            string strSql = "Update vouchersale set cashcounter = 'Y' where ID = '" + saleID + "'";
            retValue = (DBInterface.ExecuteQuery(strSql) > 0);
            return retValue;
        }

        public DataTable GetNextVisitDays(string _MFromDate, string _MToDate)
        {
            DataTable dt = null;
            try
            {
                string strSql = "select  b.ID  , b.VoucherType, " +
                           "b.VoucherNumber,b.VoucherSubType,Min(b.NextVisitDate) As NextVisitDate, B.MobileNumberForSMS,b.PatientName as AccName,b.PatientAddress1 as AccAddress1  from  vouchersale b " +
                           "where  b.NextVisitDate >= '" + _MFromDate + "'  AND b.NextVisitDate <= '" + _MToDate + "' group by AccName order by NextVisitDate, b.PatientName";
                dt = DBInterface.SelectDataTable(strSql);
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }

        public DataTable GetStockForNextVisitDays(string _MFromDate, string _MToDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select sum(a.Quantity) as RequiredQuantity,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName,b.ProdClosingStock,d.NextVisitDate,b.ProdClosingstock - sum(a.quantity) as Difference,c.AccName from detailsale a inner join masterproduct b on a.ProductID = b.ProductID  inner join vouchersale d on a.MasterSaleID = d.ID inner join masteraccount c on b.prodlastpurchasePartyID = c.AccountID where d.NextVisitDate >= '" + _MFromDate + "' AND d.NextVisitDate <= '" + _MToDate + "' group by a.ProductID order by b.ProdName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}
