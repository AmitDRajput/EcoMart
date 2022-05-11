using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
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

        public DataTable GetOverviewDataProfitPercentDay(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select VoucherDate, sum(AmountNet) as AmountNet,sum(AmountNet - ProfitInRupees) as AmountByPurchaseRate,sum(ProfitInRupees) as ProfitInRupees,sum(ProfitPercentBySaleRate) as ProfitPercentBySaleRate,sum(ProfitPercentByPurchaseRate) as ProfitPercentByPurchaseRate from vouchersale " +
                               " where voucherdate >= '" + fromdate + "' && voucherdate <= '" + todate + "' group by voucherdate ";

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
                    "b.AccName,b.AccAddress1 from vouchersale  a inner join masteraccount b  on a.AccountID = b.AccountID where a.voucherdate >= '" + fromdate + "' && a.voucherdate <= '" + todate + "' && PatientID = '' && a.AccountID != '' group by a.AccountID ";
               

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }


        public DataTable ReadSaleDetailsForVATRegister(string  fromdate , string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select b.ID,b.VoucherType, b.VoucherSubType," +
                                    "b.VoucherNumber,b.VoucherDate,b.AmountForZeroVAT, AmountVAT5Per,b.VAT5Per,AmountVAT12point5Per,b.VAT12Point5Per,(b.AmountCashDiscount+b.AmountCreditNote) as TotalLess, b.AmountCashDiscount,b.AmountCreditNote,(b.AddOnFreight+b.AmountDebitNote) as TotalAdd, b.AddOnFreight,b.AmountDebitNote,b.RoundingAmount,b.AmountNet,b.PatientName as AccName,b.PatientAddress1 as AccAddress1   from  vouchersale b " +
                                    " where voucherdate >= '" + fromdate + "' && voucherdate <= '" +  todate + "' ";                                 

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
                                    " where voucherdate >= '" + fromdate + "' && voucherdate <= '" + todate + "' group by vouchertype,VoucherDate order by VoucherDate ";
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
                                    " where voucherdate >= '" + fromdate + "' && voucherdate <= '" + todate + "' group by VoucherDate order by VoucherDate ";
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
                                    " where voucherdate >= '" + fromdate + "' && voucherdate <= '" + todate + "' group by vouchertype, substring(VoucherDate,5,2) order by VoucherDate ";
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
                                    " where voucherdate >= '" + fromdate + "' && voucherdate <= '" + todate + "' group by substr(b.VoucherDate,5,2) order by b.VoucherDate ";
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable ReadSaleDetailsForVATRegisterTIN(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select b.ID,b.VoucherDate,sum(AmountVAT5Per + AmountVAT12point5Per) as TotalAmount,sum(b.VAT5Per+ b.VAT12Point5Per) as TotalVAT,b.PatientName as AccName, b.PatientAddress1 as AccAddress1   from  vouchersale b " +
                                    " where voucherdate >= '" + fromdate + "' && voucherdate <= '" + todate + "' group by b.AccountID  order by TotalVAT desc";
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable GetSaleDataForGivenProduct(string productid)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.Expiry,a.ExpiryDate,a.Quantity,a.VATAmount,a.Amount,b.ID,b.AccountID,b.VoucherType,b.VoucherSubType, " +
                                "b.VoucherNumber,b.VoucherDate,b.PatientName,b.PatientAddress1,c.AccCode from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID   where a.ProductID = '"+ productid+"' order by b.VoucherDate,b.VoucherType,b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable GetSaleDataForH1(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.Expiry,a.ExpiryDate,a.Quantity,a.VATAmount,a.Amount,b.ID,b.PatientShortName,b.DoctorShortName,b.VoucherType,b.VoucherSubType, " +
                                "b.VoucherNumber,b.VoucherDate,c.ProdName,c.ProdLoosePack,c.ProdPack from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID inner join masterproduct c on a.productId = c.ProductId  where c.ProdScheduleDrugCode = 'H1' && voucherdate >= '" + fromdate + "' && voucherdate <= '" + todate + "' order by b.VoucherDate,b.VoucherType,b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable GetSaleDataForGivenProductBatchMrp(string productid, string batch, double mrp, string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.Expiry,a.ExpiryDate,a.Quantity,a.VATAmount,a.Amount,b.ID,b.AccountID,b.VoucherType,b.VoucherSubType, " +
                                "b.VoucherNumber,b.VoucherDate,b.PatientName,b.PatientAddress1,c.AccCode from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID   where a.ProductID = '" + productid + "' && a.BatchNumber = '"+batch +"' && a.MRP = "+mrp +" && b.VoucherDate >= '"+ fromdate +"' && b.VoucherDate <= '"+ todate +"' order by b.VoucherDate, b.Vouchertype, b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable GetSaleDataForScheduledProducts(string fromdate, string todate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.MasterSaleID,a.ProductID,a.BatchNumber,a.SaleRate, " +
                                "a.Expiry,a.Quantity,a.Amount,b.ID,b.AccountID,b.VoucherType, " +
                                "b.VoucherNumber,b.VoucherDate,b.PatientName,b.PatientAddress1,c.AccCode,d.ProductID, " + 
                                "d.ProdName,d.ProdLoosePack,d.ProdPack,d.ProdCompShortName from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where d.ProdIfSchedule = 'Y' && b.VoucherDate >= '"+ fromdate + "' && b.Voucherdate <= '"+ todate +"' order by d.ProdName";

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
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where  b.PatientName like  '%"+ patient +"%' && b.VoucherDate >= '" + fromdate + "' && b.Voucherdate <= '" + todate + "' order by b.Vouchertype,b.VoucherNumber";

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
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where  b.DoctorID = '" +  doctorid + "' && b.VoucherDate >= '" + fromdate + "' && b.Voucherdate <= '" + todate + "' order by b.Vouchertype,b.VoucherNumber";

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
                                "on A.MasterSaleID = B.ID  left outer join masteraccount c on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where  b.DoctorID = '" + doctorid + "' &&  d.ProdCompID = '"+ companyid +"' &&b.VoucherDate >= '" + fromdate + "' && b.Voucherdate <= '" + todate + "' order by b.Vouchertype,b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        //public DataTable GetOverviewDataForAllPartySummary(string fromdate, string todate)
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = "Select voucherdate,vouchertype, AccountID ,PatientID, sum(AmountNet) as AmountNet,PatientName,PatientAddress1 " +
        //                     "from vouchersale where  voucherdate >= '" + fromdate + "' &&  voucherdate <= '" + todate + "'  group by PatientName,PatientAddress1,vouchertype order by PatientName ";
        //                     //" Union Select voucherdate,vouchertype,PatientID as AccountID, sum(AmountNet) as AmountNet,PatientName,PatientAddress1 " +
        //                     //"from vouchersale where PatientID != ''&&  voucherdate >= '" + fromdate + "' &&  voucherdate <= '" + todate + "'  group by PatientName,PatientAddress1,vouchertype  order by PatientName ";
        //    dtable = DBInterface.SelectDataTable(strSql);
        //    return dtable;
        //}

        public DataTable GetOverviewDataForAllPartySummary(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.voucherdate,a.vouchertype, a.AccountID , sum(a.AmountNet) as AmountNet,a.PatientName,a.PatientAddress1,b.AccName,b.AccAddress1 " +
                             "from vouchersale a inner join masteraccount b on a.AccountID = b.AccountId where a.AccountID != '"+FixAccounts.AccountCash +"' && a.AccountID != '"+ FixAccounts.AccountPendingCashBills + "' && voucherdate >= '" + fromdate + "' &&  voucherdate <= '" + todate + "'  group by AccountID ,VoucherType  "+
            "union Select a.voucherdate,a.vouchertype, a.PatientID as AccountId, sum(a.AmountNet) as AmountNet,a.PatientName,a.PatientAddress1,b.PatientName as AccName,b.PatientAddress1  as AddAddress1" +
                             " from vouchersale a inner join masterpatient b on a.PatientID = b.PatientID where a.PatientID != '' && voucherdate >= '" + fromdate + "' &&  voucherdate <= '" + todate + "'  group by AccountID ,VoucherType order by AccName ";
            //" Union Select voucherdate,vouchertype,PatientID as AccountID, sum(AmountNet) as AmountNet,PatientName,PatientAddress1 " +
            //"from vouchersale where PatientID != ''&&  voucherdate >= '" + fromdate + "' &&  voucherdate <= '" + todate + "'  group by PatientName,PatientAddress1,vouchertype  order by PatientName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForDailySaleReport(string fromdate)
        {
            DataTable dt = null;
            try
            {
                {
                    //string strSql = "select b.ID,b.VoucherType, " +
                    //                "b.VoucherNumber,b.VoucherDate, c.AccName,c.AccAddress1,b.AmountNet as Amount, c.AccAddress2  from  vouchersale b " +
                    //                "inner join masteraccount c on b.AccountID = c.AccountID  where b.VoucherType != '" + FixAccounts.VoucherTypeForVoucherSale + "' &&  b.AccountID != '' &&  b.voucherdate = '" + fromdate + "' " +
                    //                " union  select b.ID  , b.VoucherType, " +
                    //                "b.VoucherNumber,b.VoucherDate,c.PatientName as AccName,c.PatientAddress1 as AccAddress1,b.AmountNet as Amount, c.PatientAddress2 as AccAddress2  from  vouchersale b " +
                    //                "inner join masterpatient c on b.PatientID = c.PatientID  where b.VoucherType != '" + FixAccounts.VoucherTypeForVoucherSale + "' &&  b.PatientID != '' &&  b.voucherdate = '" + fromdate + "' " +
                    //                " union select ID , VoucherType, " +
                    //                "VoucherNumber,VoucherDate,PatientName as AccName ,PatientAddress1 as AccAddress1,AmountNet as Amount,  PatientAddress2 as AccAddress2  from  vouchersale  " +
                    //                " where  VoucherType != '" + FixAccounts.VoucherTypeForVoucherSale + "' &&  voucherdate = '" + fromdate + "'  && AccountID = '' && PatientID = ''" +
                    //                " union select ID,VoucherType,countersalenumber as VoucherNumber,VoucherDate, PatientName as AccName, PatientAddress1 as AccAddress1, sum(AmountNet) as Amount, PatientAddress2 as AccAddress2 from vouchersale " +
                    //                " where VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "' && voucherdate = '" + fromdate + "' Group by countersalenumber";

                    string strSql = "select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.PatientName as AccName,b.PatientAddress1 as AccAddress1,b.AmountNet as Amount, b.PatientAddress2 as AccAddress2  from  vouchersale b " +                                    
                                    "where b.VoucherType != '" + FixAccounts.VoucherTypeForVoucherSale + "'  &&  b.voucherdate = '" + fromdate + "' " +
                                    " union select ID,VoucherType,countersalenumber as VoucherNumber,VoucherSubType, VoucherDate,PatientName as AccName, PatientAddress1 as AccAddress1, sum(AmountNet) as Amount, PatientAddress2 as AccAddress2 from vouchersale " +
                                    " where VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "' && voucherdate = '" + fromdate + "' Group by countersalenumber";

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
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.PatientName as AccName,b.PatientAddress1 as AccAddress1,b.AmountNet as Amount, b.PatientAddress2 as AccAddress2  from  vouchersale b " +
                                    "where (b.AccountID = '" + accID + "' || b.PatientID = '" + accID + "') &&  b.voucherdate >= '" + fromdate + "'  && b.VoucherDate <= '" + todate + "' order by b.VoucherDate, b.VoucherNumber";
                                    

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
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.PatientName as AccName,b.PatientAddress1 as AccAddress1,b.AmountNet as Amount, b.PatientAddress2 as AccAddress2  from  vouchersale b " +
                                    "where (b.AccountID = '" + accID + "' || b.PatientID = '" + accID + "') &&  b.voucherdate >= '" + fromdate + "'  && b.VoucherDate <= '" + todate + "' order by b.VoucherDate,b.VoucherNumber";


                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }

        public DataTable GetOverviewDataForPartyProductListDebtor(string partyid, string productid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterSaleID,a.ProductID,a.BatchNumber,a.Quantity ,a.SaleRate, a.Amount, " +
                 "b.ID,b.VoucherNumber,b.VoucherType,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailsale a  inner join vouchersale b on a.MasterSaleID = b.ID inner join masteraccount c on b.Accountid = c.AccountID where c.AccountID = '" + partyid + "' && a.ProductId = '" + productid + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPartyProductListPatient(string partyid, string productid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterSaleID,a.ProductID,a.BatchNumber,a.Quantity ,a.SaleRate, a.Amount, " +
                 "b.ID,b.VoucherNumber,b.VoucherType,b.VoucherDate,b.PatientID,b.PatientName,b.PatientAddress1 " +
                 "from detailsale a  inner join vouchersale b on a.MasterSaleID = b.ID where b.PatientID = '" + partyid + "' && a.ProductId = '" + productid + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForRegularPartyProductSale(string partyid, string productid, string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterSaleID,a.ProductID,a.BatchNumber,a.Quantity ,a.SaleRate, a.Amount, " +
                 "b.ID,b.VoucherNumber,b.VoucherType,b.VoucherDate,c.AccountID,c.AccName,c.AccAddress1 " +
                 "from detailsale a  inner join vouchersale b on a.MasterSaleID = b.ID inner join masteraccount c on b.AccountID = c.AccountID where b.AccountID = '" + partyid + "' && a.ProductId = '" + productid + "' &&  b.voucherdate >= '" + fromdate + "'  && b.VoucherDate <= '" + todate + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForRegularPartyProductSalePatient(string partyid, string productid, string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterSaleID,a.ProductID,a.BatchNumber,a.Quantity ,a.SaleRate, a.Amount, " +
                 "b.ID,b.VoucherNumber,b.VoucherType,b.VoucherDate,c.PatientID,c.PatientName,c.PatientAddress1 " +
                 "from detailsale a  inner join vouchersale b on a.MasterSaleID = b.ID inner join masterpatient c on  b.PatientID = c.PatientID  where b.PatientID = '" + partyid + "' && a.ProductId = '" + productid + "' &&  b.voucherdate >= '" + fromdate + "'  && b.VoucherDate <= '" + todate + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadSalesRegisterData(string mfromdate, string mtodate, string mtype)
        {
            DataTable dt = null;
            string strSql = "select a.ID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate,a.VoucherSubType,a.AccountID,a.AmountNet,a.DoctorID,a.PatientID,a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2 from vouchersale a  where a.voucherdate >= '" + mfromdate + "' && a.voucherdate <= '" + mtodate + "' && a.voucherType = '" + mtype + "' order by a.voucherdate,a.vouchernumber";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable ReadSalesRegisterData(string mfromdate, string mtodate)
        {
            DataTable dt = null;
            string strSql = "select a.ID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate,a.VoucherSubType,a.AccountID,a.AmountNet,a.DoctorID,a.PatientID,a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2 from vouchersale a where a.voucherdate >= '" + mfromdate + "' && a.voucherdate <= '" + mtodate + "' order by  a.voucherdate,a.vouchernumber";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable GetSaleDataCategory(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select sum(a.Amount) as AmountNet,a.ProductId,a.MasterSaleID, b.ProductID,b.ProdCategoryID,c.ProductCategoryID,c.ProductCategoryName,d.voucherdate from detailsale a inner join masterproduct b on a.ProductID = b.ProductID inner join masterproductcategory c on b.ProdCategoryID = c.ProductCategoryID inner join vouchersale d on a.MasterSaleID = d.ID  where d.voucherdate >= '" + mfromdate + "' && d.voucherdate <= '" + mtodate + "' group by c.ProductCategoryID order by c.ProductCategoryName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetSaleDataDoctorCompanySummary(string doctorid, string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select sum(a.Amount) as Amount,b.ProdCompID,c.CompName,d.voucherdate,d.DoctorID from detailsale a inner join masterproduct b on a.ProductID = b.ProductID  inner join mastercompany c on b.ProdcompID = c.CompID  inner join vouchersale d on a.MasterSaleID = d.ID  where d.DoctorID = '"+ doctorid +"' && d.voucherdate >= '" + mfromdate + "' && d.voucherdate <= '" + mtodate + "' group by d.DoctorID,b.ProdCompID order by c.CompName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetSaleDataProductSummary(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select sum(a.Quantity) as Quantity, sum(a.Amount) as Amount,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName,d.voucherdate,d.DoctorID from detailsale a inner join masterproduct b on a.ProductID = b.ProductID  inner join vouchersale d on a.MasterSaleID = d.ID  where d.voucherdate >= '" + mfromdate + "' && d.voucherdate <= '" + mtodate + "' group by a.ProductID order by b.ProdName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }      
        
    }
}
