using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBSSSale
    {
        #region Constructor
        public DBSSSale()
        {
        }
        #endregion

        #region Get Data
        public DataTable GetOverviewData(string vouSubType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct ID,VoucherType,VoucherNumber,VoucherDate,PatientName,PatientAddress1,AmountNet, " +
                            "PatientShortName,PatientShortAddress, Telephone from vouchersale " +
                            "where VoucherSubType = '" + vouSubType + "' AND VoucherDate >= '" + General.ShopDetail.Shopsy + "' AND VoucherDate <= '" + General.ShopDetail.Shopey + "' AND  VoucherDate >= '" + General.ShopDetail.Shopsy + "' AND VoucherDate <= '" + General.ShopDetail.Shopey + "' order by voucherdate desc , vouchernumber desc";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewData(string vouSubType, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct ID,VoucherType,VoucherNumber,VoucherDate,PatientName,PatientAddress1,AmountNet, " +
                            "PatientShortName,PatientShortAddress, MobileNumberForSMS, Telephone from vouchersale " +
                            "where VoucherSubType = '" + vouSubType + "' AND VoucherDate >= '" + General.ShopDetail.Shopsy + "' AND VoucherDate <= '" + General.ShopDetail.Shopey + "' AND  VoucherDate >= '" + fromDate + "' AND VoucherDate <= '" + toDate + "' order by voucherdate desc , vouchernumber desc";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataSpecialSale(string vouSubType, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct ID,VoucherType,VoucherNumber,VoucherDate,PatientName,PatientAddress1,AmountNet, " +
                            "PatientShortName,PatientShortAddress, Telephone from specialvouchersale " +
                            "where VoucherSubType = '" + vouSubType + "' AND VoucherDate >= '" + General.ShopDetail.Shopsy + "' AND VoucherDate <= '" + General.ShopDetail.Shopey + "' AND  VoucherDate >= '" + fromDate + "' AND VoucherDate <= '" + toDate + "' order by voucherdate desc , vouchernumber desc";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow GetSaleIDforClone(int vouno, string voutype)
        {
            DataRow drow;
            string strSql = "Select distinct ID,VoucherType,VoucherNumber from vouchersale " +
                            "where VoucherType = " + "'" + voutype + "' AND VoucherNumber = " + vouno;
            drow = DBInterface.SelectFirstRow(strSql);
            return drow;
        }
        public DataTable GetOverviewDataForLastSale(string accID, int ProductID)
        {
            DataTable dt = null;
            {
                string strSql = "select a.DetailSaleID,a.MasterSaleID,a.ProductID,a.StockID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.Expiry,a.ExpiryDate,a.Quantity,a.Vatper,a.VATAmount,a.Amount,b.ID,b.PatientShortName,b.PatientShortAddress, b.VoucherType,b.AccountID, " +
                                "b.VoucherNumber,b.VoucherDate,c.ProductID,c.ProdName,c.ProdLoosePack,c.ProdPack from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID inner join masterproduct c on a.ProductID = c.ProductID where  b.AccountID = '" + accID + "' AND a.ProductID = '" + ProductID + "' order by b.VoucherNumber DESC";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable GetOverviewDataForPartywiseBillsForStatements(string partyid, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherType,a.VoucherSubType, a.VoucherDate ,a.AmountNet , a.VAT5Per,a.VAT12point5Per " +
                 "from vouchersale a inner join masteraccount c on a.Accountid = c.AccountID where a.AccountID = '" + partyid + "' AND a.voucherdate >= '" + fromDate + "' AND  a.voucherdate <= '" + toDate + "' AND a.StatementNumber = 0  AND AmountClear = 0 order by VoucherType, VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForHospitalStatement(string inwardID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherType,a.VoucherSubType, a.VoucherDate ,a.AmountNet , a.VAT5Per,a.VAT12point5Per " +
                 "from vouchersale a  where a.AccountID = '" + inwardID + "' AND a.StatementNumber = 0  AND a.AmountClear = 0 order by a.VoucherType, a.VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }


        public DataTable GetOverviewDataForPartywiseStatementsView(int statementNumber, string voucherSeries)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherType,a.VoucherSubType,a.VoucherDate ,a.AmountNet , a.VAT5Per,a.VAT12point5Per " +
                 "from vouchersale a  where a.statementnumber = " + statementNumber + " AND voucherseries = '" + voucherSeries + "' order by VoucherType, VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPartywiseSaleReportforPatient(string accID)
        {
            DataTable dt = null;
            try
            {
                {


                    string strSql = "select  b.ID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.PatientName as AccName,b.PatientAddress1 as AccAddress1,b.AmountNet as Amount, b.PatientAddress2 as AccAddress2  from  vouchersale b " +
                                    "where (b.AccountID = '" + accID + "' OR b.PatientID = '" + accID + "') order by b.VoucherDate,b.VoucherNumber";


                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }
        public DataTable GetOverviewDataForLastPurchase(int ProductID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate,a.MRP,a.Margin ,a.MarginAfterDiscount,a.DistributorSaleRatePer,a.DistributorSaleRate," +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where a.ProductID = '" + ProductID + "' order by b.VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetVoucherSaleDataData(string voudate, string changeCounterSaleType) //Amar
        {
            DataTable dt = null;
            string strSql = "";
            if (changeCounterSaleType != "Y")
            {
                //strSql = "select a.DetailSaleID,a.MasterSaleID,a.ProductID,a.StockID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                //                "a.TradeRate,a.MySpecialDiscountAmount,a.Expiry,a.ExpiryDate,a.Quantity,a.Vatper,a.VATAmount,a.Amount, " +
                //                "b.ID,b.PatientShortName,b.PatientShortAddress,b.VoucherNumber,b.VoucherDate,b.VoucherType, " +
                //                "c.ProductID,c.ProdName,c.ProdLoosePack,c.ProdPack,c.ProdIfSaleDisc from detailsale a  inner join vouchersale b " +
                //                "on A.MasterSaleID = B.ID inner join masterproduct c on a.ProductID = c.ProductID where b.VoucherDate = '" + voudate + "' AND b.VoucherType =  '" + FixAccounts.VoucherTypeForVoucherSale + "' order by b.VoucherNumber desc";

                strSql = "select a.DetailSaleID,a.MasterSaleID,a.ProductID,a.StockID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, a.TradeRate,a.MySpecialDiscountAmount,a.Expiry,a.ExpiryDate,a.Quantity,a.Vatper,a.VATAmount,a.Amount, b.ID,b.PatientShortName,b.PatientShortAddress,b.VoucherNumber,b.VoucherDate,b.VoucherType, c.ProductID,c.ProdName,c.ProdLoosePack,c.ProdPack,c.ProdIfSaleDisc, c.ProdCompShortName, a.ExpiryDate, b.CreatedTime from detailsale a  inner join vouchersale b on A.MasterSaleID = B.ID inner join masterproduct c on a.ProductID = c.ProductID where b.VoucherDate = '" + voudate + "' AND b.VoucherType =  '" + FixAccounts.VoucherTypeForVoucherSale + "' order by b.VoucherNumber desc";
                dt = DBInterface.SelectDataTable(strSql);
            }
            else
            {
                strSql = "select a.DetailSaleID,a.MasterSaleID,a.ProductID,a.StockID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                               "a.TradeRate,a.MySpecialDiscountAmount,a.Expiry,a.ExpiryDate,a.Quantity,a.Vatper,a.VATAmount,a.Amount, " +
                               "a.VoucherDate,a.VoucherNumber,a.VoucherType," +
                               "c.ProductID,c.ProdName,c.ProdLoosePack,c.ProdPack,c.ProdIfSaleDisc, c.ProdCompShortName from detailsale a  " +
                               "inner join masterproduct c on a.ProductID = c.ProductID where a.VoucherDate = '" + voudate + "' AND a.VoucherType =  '" + FixAccounts.VoucherTypeForVoucherSale + "'";
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable GetPreviousSale(string accountid)
        {
            DataTable dt = null;
            string strSql = "Select voucherdate, sum(AmountNet) as AmountNet from vouchersale  where (AccountID = '" + accountid + "' OR PatientID = '" + accountid + "' OR debtorsPatientID = '"+accountid +"') group by  substring(voucherDate,5,2)";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable GetPreviousSaleBillWise(string accountid, int month)
        {
            string cmonth = "";
            if (month.ToString().Length == 1)
                cmonth = string.Concat("0", month.ToString().Trim());
            else
                cmonth = month.ToString().Trim();

           

            DataTable dt = null;
            string strSql = "Select ID, VoucherType,VoucherSubType,VoucherNumber,Voucherdate,AmountNet, PatientName from vouchersale  where (AccountID = '" + accountid + "' OR PatientID = '" + accountid + "') AND  substring(voucherDate,5,2) = " + cmonth;
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataRow ReadDetailsByVouNumber(string voutype, string subtype, int vouno, string vouSeries)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration1,a.Narration2,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT12Point5Per,a.AmountVAT5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,b.AccountID,b.AccTokenNumber,a.OrderNumber,a.OrderDate from vouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.VoucherNumber ={0} AND a.VoucherType = '{1}' AND a.VoucherSubType = '{2}' AND a.VoucherSeries = '{3}'";
                strSql = string.Format(strSql, vouno, voutype, subtype,vouSeries);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow ReadDetailsByVouNumberSpecialSale(string voutype, int vouno, string vouSeries)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration1,a.Narration2,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT12Point5Per,a.AmountVAT5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,b.AccountID,b.AccTokenNumber,a.OrderNumber,a.OrderDate from specialvouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.VoucherNumber ={0} AND a.VoucherType = '{1}'  AND a.VoucherSeries = '{2}'";
                strSql = string.Format(strSql, vouno, voutype, vouSeries);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        //public DataRow ReadDetailsByVouNumberCounterSale(string voutype, string subtype, int vouno)
        //{
        //    DataRow dRow = null;
        //    if (vouno != 0)
        //    {
        //        string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
        //        "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross,a.MobileNumberForSMS, " +
        //        "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
        //        "a.OctroiPercentage,a.AmountOctroi,a.Narration1,a.Narraion2,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
        //        "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress, a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT12Point5Per,a.AmountVAT5Per," +
        //        "a.RoundingAmount,a.DiscountAmountCB,a.OrderNumber,a.OrderDate from vouchersale  a  where  a.VoucherNumber ={0} AND a.VoucherType = '{1}' AND a.VoucherSubType = '{2}'";
        //        strSql = string.Format(strSql, vouno, voutype, subtype);
        //        dRow = DBInterface.SelectFirstRow(strSql);
        //    }
        //    return dRow;
        //}

        public DataRow ReadDetailsByID(string Id, string subtype)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                //string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                //"a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross,a.MobileNumberForSMS, " +
                //"a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                //"a.OctroiPercentage,a.AmountOctroi,a.Narration,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                //"a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT5Per,a.AmountVAT12point5Per,a.ScanPrescriptionFileName, " +
                //"a.RoundingAmount,a.DiscountAmountCB,a.ProfitInRupees,a.ProfitPercentBySaleRate,a.ProfitPercentByPurchaseRate,a.AmountPMTDiscount,a.AmountItemDiscount,a.DoctorShortName,a.DoctorAddress,a.Telephone, b.AccountID,b.AccTokenNumber,a.OrderNumber, " +
                //"a.OrderDate,a.MySpecialDiscountPercent,a.DebtorsPatientID, a.MySpecialDiscountAmount,a.MySpecialDiscountAmount12point5,a.MySpecialDiscountAmount5,a.AmountCashDiscount5,a.AmountCashDiscount12point5,a.AmountSchemeDiscount,a.IfFullPayment,a.NextVisitDate,a.CreditCardBankID from " + 
                //"vouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.ID='{0}'  AND a.VoucherSubType = '{1}' ";
                string strSql = "Select * from vouchersale where ID = '" + Id +"'"; 
                strSql = string.Format(strSql, Id, subtype);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByIDSpecialSale(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration1,a.Narration2,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT5Per,a.AmountVAT12point5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,a.ProfitInRupees,a.ProfitPercentBySaleRate,a.ProfitPercentByPurchaseRate,a.AmountPMTDiscount,a.AmountItemDiscount,a.DoctorShortName,a.DoctorAddress,a.Telephone, b.AccountID,b.AccTokenNumber,a.OrderNumber, " +
                "a.OrderDate from specialvouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.ID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            
            }
            return dRow;
        }


        public DataRow ReadDetailsByIDForChanged(string Id, string subtype)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.ID,a.ChangedID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration1,a.Narration2,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT5Per,a.AmountVAT12point5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,a.ProfitInRupees,a.ProfitPercentBySaleRate,a.ProfitPercentByPurchaseRate,a.AmountPMTDiscount,a.AmountItemDiscount, b.AccountID,b.AccTokenNumber,a.OrderNumber,a.OrderDate from changedvouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.ChangedID='{0}'  AND a.VoucherSubType = '{1}' ";
                strSql = string.Format(strSql, Id, subtype);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow ReadDetailsByIDForDeleted(string Id, string subtype)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.ID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration1,a.Narration2,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT5Per,a.AmountVAT12point5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,a.ProfitInRupees,a.ProfitPercentBySaleRate,a.ProfitPercentByPurchaseRate,a.AmountPMTDiscount,a.AmountItemDiscount, b.AccountID,b.AccTokenNumber,a.OrderNumber,a.OrderDate from deletedvouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.ID='{0}'  AND a.VoucherSubType = '{1}' ";
                strSql = string.Format(strSql, Id, subtype);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow GetSaleSettings()
        {
            DataRow dr = null;
            string strSql = "Select * from tblsettings";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }

        public DataRow CheckProductInShortList(int ProductID)
        {
            string strSql = string.Format("Select *  from tbldailyshortlist where ProductID = '{0}' AND  OrderNumber =  0 AND ShortListDate = '"+ DateTime.Today.Date.ToString("yyyyMMdd") +"' ", ProductID);
            return DBInterface.SelectFirstRow(strSql);
        }

        public bool AddToShortList(int ProductID, string Voudate, string shortlistid, double purchaserate, string accID)
        {
            string sql = GetInsertQueryforShortList(ProductID, Voudate, shortlistid, purchaserate,accID);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(sql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetInsertQueryforShortList(int ProductID, string Voudate, string shortlistid, double purchaserate, string accid)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbldailyshortlist";
            //objQuery.AddToQuery("DSLID", shortlistid);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("ShortListDate", Voudate);
            objQuery.AddToQuery("PurchaseRate", purchaserate);
            objQuery.AddToQuery("IFSave", "N");
            objQuery.AddToQuery("MasterID", " ");
            objQuery.AddToQuery("OrderNumber", 0);
            objQuery.AddToQuery("OrderDate", " ");
            objQuery.AddToQuery("OrderQuantity", 0);
            objQuery.AddToQuery("AccountId", accid);
            objQuery.AddToQuery("ShortListTime", " ");
            return objQuery.InsertQuery();
        }

        public DataTable ReadProductDetailsByIDDetailSale(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdClosingStock/a.ProdLoosePack as ProdClosingStockPack,a.ProdVATPercent,a.ProdCompShortName,a.ProdLastSaleStockID as LastStockID,a.ProdScheduleDrugCode,a.ProdBoxQuantity,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdMaxLevel,a.ProdCategoryID,ProdIfSaleDisc,b.ProductID, " +
                "b.StockID,b.StockID as ProdLastSaleStockID, b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.Quantity as OldQuantity,b.VATAmount,b.Amount,b.ItemDiscountPer,b.ItemDiscountAmount,b.ProfitPercentBySaleRate,b.ProfitInRupees,b.ProfitPercentByPurchaseRate,b.CashDiscountAmount,b.SchemeQuantity,SchemeDiscountAmount,c.ShelfID,c.ShelfCode," +
                "b.GSTAmountZero,b.GSTSAmount,b.GSTCAmount,b.GSTIAmount,b.GSTS,b.GSTC,b.GSTI,b.ActualBatchNumber,b.ActualMRP,b.ActualSaleRate,d.ClosingStock,e.GenericCategoryName from masterproduct A inner join  detailsale b  on A.ProductID = B.ProductID  left outer join mastershelf C on A.ProdShelfID = C.ShelfID   left outer join tblstock D on B.stockID = d.stockID left outer join mastergenericcategory e on a.ProdCategoryID = e.GenericCategoryID where B.MasterSaleID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public DataTable ReadProductDetailsByIDDetailSaleSpecialSale(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdClosingStock/a.ProdLoosePack as ProdClosingStockPack,a.ProdVATPercent,a.ProdCompShortName,a.ProdLastSaleStockID as LastStockID,a.ProdScheduleDrugCode,a.ProdBoxQuantity,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdMaxLevel,a.ProdCategoryID,b.IfProductDiscount as ProdIfSaleDisc,b.ProductID, " +
                "b.StockID,b.StockID as ProdLastSaleStockID, b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.Quantity as OldQuantity,b.VATAmount,b.Amount,b.PMTDiscount,b.PMTAmount,b.ItemDiscountPer,b.ItemDiscountAmount,b.ProfitPercentBySaleRate,b.ProfitInRupees,b.ProfitPercentByPurchaseRate,c.ShelfID,c.ShelfCode," +
                "d.ClosingStock,e.GenericCategoryName from masterproduct A inner join  specialdetailsale B  on A.ProductID = B.ProductID  left outer join mastershelf C on A.ProdShelfID = C.ShelfID   left outer join tblstock D on B.stockID = d.stockID left outer join mastergenericcategory e on a.ProdCategoryID = e.GenericCategoryID where B.MasterSaleID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public DataTable ReadProductDetailsByIDDetailSaleForChanged(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdVATPercent,a.ProdCompShortName,a.ProdScheduleDrugCode,a.ProdLastSaleStockID as LastStockID,b.IfProductDiscount as ProdIfSaleDisc,b.ProductID, " +
                "b.StockID,b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.Quantity as OldQuantity,b.VATAmount,b.Amount,b.PMTDiscount,b.PMTAmount,b.ItemDiscountPer,b.ItemDiscountAmount,b.ChangedMasterID,b.ProfitPercentBySaleRate,b.ProfitInRupees,b.ProfitPercentByPurchaseRate,b.MySpecialDiscountAmount,c.ShelfID,c.ShelfCode," +
                "d.ClosingStock,e.GenericCategoryName from masterproduct A inner join  changeddetailsale B  on A.ProductID = B.ProductID  left outer join mastershelf C on A.ProdShelfID = C.ShelfID   left outer join tblstock D on B.stockID = d.stockID left outer join mastergenericcategory e on a.ProdCategoryID = e.GenericCategoryID where B.ChangedMasterID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public DataTable ReadProductDetailsByIDDetailSaleForDeleted(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdVATPercent,a.ProdCompShortName,a.ProdLastSaleStockID as LastStockID,b.IfProductDiscount as ProdIfSaleDisc,b.ProductID, " +
                "b.StockID,b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.Quantity as OldQuantity,b.VATAmount,b.Amount,b.PMTDiscount,b.PMTAmount,b.ItemDiscountPer,b.ItemDiscountAmount,b.ProfitPercentBySaleRate,b.ProfitInRupees,b.ProfitPercentByPurchaseRate,c.ShelfID,c.ShelfCode," +
                "d.ClosingStock from masterproduct A inner join  deleteddetailsale B  on A.ProductID = B.ProductID  left outer join mastershelf C on A.ProdShelfID = C.ShelfID   left outer join tblstock D on B.stockID = d.stockID where B.MasterSaleID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public DataTable ReadDetailSaleByCloneID(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdVATPercent,a.ProdCompShortName,a.ProdLastSaleStockID as LastStockID,b.IfProductDiscount as ProdIfSaleDisc,b.ProductID, " +
                "b.StockID,b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.Quantity as OldQuantity,b.VATAmount,b.Amount,c.ShelfID,c.ShelfCode," +
                "d.ClosingStock from masterproduct A inner join  detailsale B  on A.ProductID = B.ProductID  left outer join mastershelf C on A.ProdShelfID = C.ShelfID   left outer join tblstock D on B.stockID = d.stockID where B.MasterSaleID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }

        public DataTable ReadPaymentDetailsByID(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select a.MasterID, a.MastersaleID,a.ClearAmount,b.VoucherType, " +
                "b.VoucherNumber,b.VoucherDate,b.CBID,b.Ifchequereturn," +
                "c.ID from detailcashbankreceipt a inner join  vouchercashbankreceipt b  on a.MasterID = b.CBID  inner join vouchersale c on a.MastersaleID = c.ID where a.MastersaleID = '{0}'";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        //
        public DataRow ReadBillPrintSettings()
        {
            DataRow dr;

            string strsql = "select setPrintSaleBillPrintedPaper from tblsettings";
            dr = DBInterface.SelectFirstRow(strsql);

            return dr;
        }
        public DataRow GetSchemeDetails(string mprod)
        {
            DataRow dr;
            int intprod = Convert.ToInt32(mprod);
            string ed = DateTime.Now.Date.ToString("yyyyMMdd").Substring(0, 8);
            string strsql = "select * from masterscheme where ProductID = {0} AND  ClosingDate >= '" + ed + "' AND (IFSchemeClosed is null OR IfSchemeClosed != 'N')";
            strsql = string.Format(strsql, intprod);
            dr = DBInterface.SelectFirstRow(strsql);

            return dr;
        }
        #endregion

        #region write Data

        public int AddDetails(string Id, string CreditorId, string Narration1,string Narration2, string VouType, int VouNo, string VouDate,
            double AmountNet, double DiscPer, double DiscAmt, double Amt, double rnd, string docId, double addon,
            string vousubtype,double amtbal, double amtclear, int statementnumber,double cramount,
            double dbamount, string patientname, string patientaddress1, string patientaddress2,string operatorid, int salesmanid, int transporterid,
            string OrderNumber, string OrderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate,double totalProfitPercentBySaleRate,
            double itemDiscount,double SchemeTotalDiscount,
             double gstAmt0, double gstAmtS5, double gstAmtS12, double gstAmtS18, double gstAmtS28,
            double gstAmtC5, double gstAmtC12, double gstAmtC18, double gstAmtC28, double gsts5, double gsts12, double gsts18, double gsts28,
            double gstc5, double gstc12, double gstc18, double gstc28, double gstAmtI5, double gstAmtI12, double gstAmtI18, double gstAmtI28, double gstI5, double gstI12, double gstI18, double gstI28, string createdby, string createddate, string createdtime)
        {
           
            string strSql = GetInsertQuerySale(Id, CreditorId, Narration1,Narration2, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, rnd, docId, addon, vousubtype,
                amtbal, amtclear, statementnumber, cramount, dbamount, patientname, patientaddress1,
                patientaddress2, operatorid, salesmanid,transporterid, OrderNumber,
                OrderDate, totalProfitInRupees, totalProfitPercentbyPurchaseRate, totalProfitPercentBySaleRate,  itemDiscount,
                SchemeTotalDiscount,
                 gstAmt0, gstAmtS5, gstAmtS12, gstAmtS18, gstAmtS28, gstAmtC5, gstAmtC12, gstAmtC18, gstAmtC28, gsts5,
                gsts12, gsts18, gsts28, gstc5, gstc12, gstc18, gstc28, gstAmtI5, gstAmtI12, gstAmtI18, gstAmtI28, gstI5, gstI12, gstI18, gstI28, createdby, createddate, createdtime);
            //strSql += ";select last_insert_ID()";
            int ii = Convert.ToInt32(DBInterface.ExecuteScalar(strSql));
            return ii;
        }

        public bool SaveNewDoctor(string DocID, string doctorNameAddress, string doctorAddress, string createdby, string createddate, string createdtime)
        {
            bool retValue = false;

            string strSql = GetInsertQueryNewDoctor(DocID, doctorNameAddress, doctorAddress, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            return retValue;

        }
        //public void SaveNewPatient(string PatientID, string Name, string PatientAddress1, string PatientAddress2, string ShortName, string Telephone, string mobileNumberForSMS, string DocID, string doctorNameAddress, string doctorAddress, string createdby, string createddate, string createdtime)
        //{                  
        //    string strSql = GetInsertQueryNewPatient(PatientID, Name, PatientAddress1, PatientAddress2, ShortName, Telephone, mobileNumberForSMS, DocID, doctorNameAddress, doctorAddress, createdby, createddate, createdtime);
        //    DBInterface.ExecuteQuery(strSql);
        //}


        public bool AddDetailsSpecialSale(string Id, string CreditorId, string Narration1,string Narration2, string VouType, int VouNo,
            string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12,
            double rnd, string docId, string docshortname, string docAddress, double addon, string vousubtype,
            double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber,
            double cramount, double dbamount, string patientname, string patientaddress1, string patientaddress2,
            string patientshortname, string patientShortAddress, double amountforzerovat, string operatorid, string patientid, double AmountVat12, double AmountVat5, string IPDOPD, string OrderNumber, string OrderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, double itemDiscount, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuerySaleSpecialSale(Id, CreditorId, Narration1,Narration2, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, docId, docshortname, docAddress, addon, vousubtype,
                amtbal, amtclear, octper, octamt, countersalenum, statementnumber, cramount, dbamount, patientname, patientaddress1,
                patientaddress2, patientshortname, patientShortAddress, amountforzerovat, operatorid, patientid, AmountVat12, AmountVat5, IPDOPD, OrderNumber, OrderDate, totalProfitInRupees, totalProfitPercentbyPurchaseRate, totalProfitPercentBySaleRate, pmtTotalDiscount, itemDiscount, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }


        //public bool AddDetailsProducts(string Id, int ProductID, string Batchno, int quantity, int SchemeQuantity,
        //      double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount, string VouType, int VouNo, string VouDate)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetInsertQueryProducts(Id, ProductID, Batchno, quantity, SchemeQuantity, PurchaseRate, MRP, SaleRate, Expiry, ExpiryDate, reasoncode, VatPer, Amount, VouType, VouNo, VouDate);
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}

        public bool AddDetailsProductsSS(int Id, int ProductID, string Batchno, int quantity,
                double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
                string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid,
                int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate,
                double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt,
                double cashDiscountAmount, double mySpecialDiscountamount,double schemeDiscountAmount, int schemeDiscountQuantity,string vouDate, 
                int vouNo, string vouType, double gstPurchaseAmountZero, double gstSPurchaseAmount, double gstCPurchaseAmount,
               double gstSAmount, double gstCAmount, string newbatchno, double newmrp,double newsalerate)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryProductsSS(Id, ProductID, Batchno, quantity, PurchaseRate, MRP,
                      SaleRate, TradeRate, Expiry, VatPer, Amount, ExpiryDate, accId, CompId, VatAmt, ifproddisc,
                      stockid, mydetailsaleid, serialNumber, ProfitInRupees, ProfitByPurchaseRate, ProfitBySaleRate,
                      pmtDiscountPer, pmtDiscountAmt, itemDiscountPer, itemDiscountAmt, cashDiscountAmount, mySpecialDiscountamount,
                      schemeDiscountAmount, schemeDiscountQuantity, vouDate, vouNo, vouType, gstPurchaseAmountZero, gstSPurchaseAmount,
                      gstCPurchaseAmount, gstSAmount, gstCAmount,newbatchno,newmrp,newsalerate);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsProductsSSSpecialSale(string Id, int ProductID, string Batchno, int quantity,
               double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
               string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryProductsSSSpecialSale(Id, ProductID, Batchno, quantity,
                PurchaseRate, MRP, SaleRate, TradeRate, Expiry, VatPer, Amount,
                 ExpiryDate, accId, CompId, VatAmt, ifproddisc, stockid, mydetailsaleid, serialNumber, ProfitInRupees, ProfitByPurchaseRate, ProfitBySaleRate, pmtDiscountPer, pmtDiscountAmt, itemDiscountPer, itemDiscountAmt);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDeletedDetailsProductsSS(string Id, int ProductID, string Batchno, int quantity,
              double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
              string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDeletedProductsSS(Id, ProductID, Batchno, quantity,
                PurchaseRate, MRP, SaleRate, TradeRate, Expiry, VatPer, Amount,
                 ExpiryDate, accId, CompId, VatAmt, ifproddisc, stockid, mydetailsaleid, serialNumber, ProfitInRupees, ProfitByPurchaseRate, ProfitBySaleRate, pmtDiscountPer, pmtDiscountAmt, itemDiscountPer, itemDiscountAmt);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddChangedDetailsProductsSS(string Id, string ChangedID, int ProductID, string Batchno, int quantity,
             double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
             string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryChangedProductsSS(Id, ChangedID, ProductID, Batchno, quantity,
                PurchaseRate, MRP, SaleRate, TradeRate, Expiry, VatPer, Amount,
                 ExpiryDate, accId, CompId, VatAmt, ifproddisc, stockid, mydetailsaleid, serialNumber, ProfitInRupees, ProfitByPurchaseRate, ProfitBySaleRate, pmtDiscountPer, pmtDiscountAmt, itemDiscountPer, itemDiscountAmt);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string CreditorId, string Narration1,string Narration2, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12,
             double rnd, double balamt, string docId, string docNameAddress, string docAddress, double addon, string vousubtype,
             double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber, double cramount,
            double dbamount, string patientname, string patientAddress1, string patientAddress2, string ShortName, string patientShortAddress, double amountforzerovat,
            string operatorid, string patientID, double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees,
            double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount,
            double itemTotalDiscount, string prescriptionFileName, string telephone, double mySpecialDiscountAmount,
            double MySpecialDiscountPercent, double MyTotalSpecialDiscountPer12point5, double MyTotalSpecialDiscountPer5,
            double TotalDiscount12point5, double TotalDiscount5,double SchemeTotalDiscount, string iffullpayment, string nextVisitDate, string debtorsPatientID,
             double gstAmt0, double gstAmtS5, double gstAmtS12, double gstAmtS18, double gstAmtS28,
            double gstAmtC5, double gstAmtC12, double gstAmtC18, double gstAmtC28, double gsts5, double gsts12, double gsts18, double gsts28,
            double gstc5, double gstc12, double gstc18, double gstc28, double gstAmtI5, double gstAmtI12, double gstAmtI18, double gstAmtI28, double gstI5, double gstI12, double gstI18, double gstI28, string modifiedby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuerySale(Id, CreditorId, Narration1,Narration2, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, balamt, docId, docNameAddress, docAddress, addon, vousubtype,
                amtbal, amtclear, octper, octamt, countersalenum, statementnumber, cramount, dbamount, patientname,
                patientAddress1, patientAddress2, ShortName, patientShortAddress, amountforzerovat, operatorid, patientID, AmountVat12,
                AmountVat5, IPDOPD, orderNumber, orderDate, totalProfitInRupees, totalProfitPercentbyPurchaseRate, totalProfitPercentBySaleRate,
                pmtTotalDiscount, itemTotalDiscount, prescriptionFileName, telephone, mySpecialDiscountAmount, MySpecialDiscountPercent,
                MyTotalSpecialDiscountPer12point5, MyTotalSpecialDiscountPer5, TotalDiscount12point5, TotalDiscount5, SchemeTotalDiscount, iffullpayment, nextVisitDate,debtorsPatientID,
                  gstAmt0, gstAmtS5, gstAmtS12, gstAmtS18, gstAmtS28, gstAmtC5, gstAmtC12, gstAmtC18, gstAmtC28, gsts5,
                gsts12, gsts18, gsts28, gstc5, gstc12, gstc18, gstc28, gstAmtI5, gstAmtI12, gstAmtI18, gstAmtI28, gstI5, gstI12, gstI18, gstI28, modifiedby, modifydate, modifytime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetailsPrescription(string Id, string prescriptionFileName)
        {
            bool bRetValue = false;
            string strSql = "Update vouchersale set scanPrescriptionFileName = '" + prescriptionFileName + "' where ID = '" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsInDeleteMaster(string Id, string CreditorId, string Narration1,string Narration2, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12,
             double rnd, double balamt, string docId, string docshortname, string docAddress, double addon, string vousubtype,
             double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber, double cramount,
            double dbamount, string patientname, string patientAddress1, string patientAddress2, string ShortName, string patientShortAddress, double amountforzerovat,
            string operatorid, double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, string telephone, string modifiedby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetAddDetailsInDeleteMaster(Id, CreditorId, Narration1, Narration2, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, balamt, docId, docshortname, docAddress, addon, vousubtype,
                amtbal, amtclear, octper, octamt, countersalenum, statementnumber, cramount, dbamount, patientname,
                patientAddress1, patientAddress2, ShortName, patientShortAddress, amountforzerovat, operatorid, AmountVat12, AmountVat5, IPDOPD, orderNumber, orderDate, totalProfitInRupees, totalProfitPercentbyPurchaseRate, totalProfitPercentBySaleRate, pmtTotalDiscount, telephone, modifiedby, modifydate, modifytime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsInChangedMaster(string Id, string ChangedID, string CreditorId, string Narration1,string Narration2, string VouType, int VouNo,
            string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12,
            double rnd, double balamt, string docId, string docshortname, string docAddress, double addon, string vousubtype,
            double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber, double cramount,
           double dbamount, string patientname, string patientAddress1, string patientAddress2, string ShortName, string patientShortAddress, double amountforzerovat,
           string operatorid, double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, string telephone, string modifiedby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetAddDetailsInChangedMaster(Id, ChangedID, CreditorId, Narration1,Narration2, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, balamt, docId, docshortname, docAddress, addon, vousubtype,
                amtbal, amtclear, octper, octamt, countersalenum, statementnumber, cramount, dbamount, patientname,
                patientAddress1, patientAddress2, ShortName, patientShortAddress, amountforzerovat, operatorid, AmountVat12, AmountVat5, IPDOPD, orderNumber, orderDate, totalProfitInRupees, totalProfitPercentbyPurchaseRate, totalProfitPercentBySaleRate, pmtTotalDiscount, telephone, modifiedby, modifydate, modifytime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        //public bool UpdateDetailsEditCounterSale(string Id, string CreditorId, string Narration, string VouType, int VouNo,
        //    string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12,
        //    double rnd, string docId, string docNameAddress, string docAddress, double addon, string vousubtype,
        //    double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber,
        //    double cramount, double dbamount, string patientname, string patientaddress1, string patientaddress2,
        //    string patientshortname, string patientShortAddress, double amountforzerovat, string operatorid, string patientid, double AmountVat12,
        //    double AmountVat5, string IPDOPD, string OrderNumber, string OrderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate,
        //    double totalProfitPercentBySaleRate, double pmtTotalDiscount, double itemDiscount, string prescriptionFileName, string telephone,
        //    double mySpecialDiscountAmount, double mySpecialDiscountPer, double MyTotalSpecialDiscountPer12point5,
        //    double MyTotalSpecialDiscountPer5, double TotalDiscount12point5, double TotalDiscount5, double SchemeTotalDiscount,
        //    string iffullpayment, string creditCardBankID,string MobileNumberForSMS, string createdby, string createddate, string createdtime)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetUpdateQueryEditCounterSale(Id, CreditorId, Narration, VouType, VouNo,
        //        VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, docId, docNameAddress, docAddress, addon, vousubtype,
        //        amtbal, amtclear, octper, octamt, countersalenum, statementnumber, cramount, dbamount, patientname, patientaddress1,
        //        patientaddress2, patientshortname, patientShortAddress, amountforzerovat, operatorid, patientid, AmountVat12, AmountVat5, IPDOPD, OrderNumber,
        //        OrderDate, totalProfitInRupees, totalProfitPercentbyPurchaseRate, totalProfitPercentBySaleRate, pmtTotalDiscount, itemDiscount,
        //        prescriptionFileName, telephone, mySpecialDiscountAmount, mySpecialDiscountPer, MyTotalSpecialDiscountPer12point5,
        //        MyTotalSpecialDiscountPer5, TotalDiscount12point5, TotalDiscount5, SchemeTotalDiscount, iffullpayment, creditCardBankID, MobileNumberForSMS, createdby, createddate, createdtime);
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
  
            //here 30/10/2015
        //    string Id, string CreditorId, string VouType, int VouNo,
        //    string VouDate, string docId, string docshortname, string docAddress, string vousubtype, int countersalenum, string patientname, string patientID,
        //    string patientaddress1, string patientaddress2, string shortname, string patientShortAddress, string operatorid, string telePhone,
        //    string modifiedby, string modifydate, string modifytime)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetUpdateQueryEditCounterSale(Id, CreditorId, VouType, VouNo,
        //        VouDate, docId, docshortname, docAddress, vousubtype, countersalenum, patientname, patientID,
        //        patientaddress1, patientaddress2, shortname, patientShortAddress, operatorid, telePhone,
        //        modifiedby, modifydate, modifytime);
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}
            //return bRetValue;
        //}
        public bool DeleteDetails(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQuery(Id);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool DeleteProductsByMasterID(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQueryProducts(Id);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool DeleteDebtorSaleByMasterID(string Id)
        {
            bool bRetValue = false;
            DataTable dt = null;
            string strSql = "Select * from detailsale where MasterSaleID = '"+ Id +"'";
            dt = DBInterface.SelectDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0)
                bRetValue = true;
            else
            {
                strSql = GetDeleteDebtorSaleQuery(Id);
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            }
            //strSql = "Delete from tbltrnac where voucherID = '"+ Id +"'";
            //DBInterface.ExecuteQuery(strSql);            
            return bRetValue;
        }
        public bool DeleteDetailsFromtblTrnac(string Id)
        {
            bool bRetValue = false;            
            DataTable dt = null;
            string strSql = "Select * from tbltrnac where voucherID = '"+ Id +"'";
            dt = DBInterface.SelectDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0)
                bRetValue = true;
            else
            {
                strSql = "Delete from tbltrnac where voucherID = '" + Id + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            }
            return bRetValue;
        }
        public bool UpdateVoucherSaleDeleteMaster(string mmasterSaleID)
        {
            bool bRetValue = false;
            string strSql = GetVoucherSaleDeleteMasterQuery(mmasterSaleID);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool DeleteDetailsVoucherSale(string mmasterSaleID)
        {
            bool bRetValue = false;
            string strSql = GetDeleteDetailsVoucherSaleQuery(mmasterSaleID);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateVoucherSaleDeleteDetails(string mdetailSaleID)
        {
            bool bRetValue = false;
            string strSql = GetUpdateVoucherSaleDeleteDetailsQuery(mdetailSaleID);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateVoucherSaleUpdateStock(string mstockID, int mqty)
        {
            bool returnVal = false;
            string strSql = "Update tblstock set closingstock = closingstock +  " + mqty + ", SaleStock = SaleStock - " + mqty + " where StockID = '" + mstockID + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;

        }
        //internal void SaveDiscPercentInPatientMaster(string accountID, double discoutPercent)
        //{

        //    string strSql = "Update masterpatient set DiscountOffered = " + discoutPercent + " where PatientID = '" + accountID + "'";
        //    try
        //    {
        //        DBInterface.ExecuteQuery(strSql);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteError(ex.ToString());
        //    }
        //}
        internal void SaveDiscPercentInAccountMaster(string accountID, double discoutPercent)
        {

            string strSql = "Update masteraccount set AccDiscountOffered = " + discoutPercent + " where AccountID = '" + accountID + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }

        }
        public bool UpdateVoucherSaleUpdateMasterProduct(string mProductID, int mqty)
        {
            bool returnVal = false;
            string strSql = "Update masterproduct set prodclosingstock = Prodclosingstock +  " + mqty + "  where ProductID = '" + mProductID + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool UpdateVoucherSaleUpdateMaster(string mmasterSaleID, double mamt, double mvatamt5, double mvatamt12point5, double mvatamtforZero, double mvat5, double mvat12point5, double mProfitInRupees, double mTotalProfitPercentBySaleRate, double mTotalProfitPercentByPurchaseRate)
        {
            bool bRetValue = false;
            string strSql = GetUpdateVoucherSaleUpdateMasterQuery(mmasterSaleID, mamt, mvatamt5, mvatamt12point5, mvatamtforZero, mvat5, mvat12point5, mProfitInRupees, mTotalProfitPercentBySaleRate, mTotalProfitPercentByPurchaseRate);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateDetailsEditCounterSale(string Id, string CreditorId, string VouType, int VouNo,
           string VouDate, string docId, string docshortname, string docAddress, string vousubtype, int countersalenum, string patientname, string patientID,
           string patientaddress1, string patientaddress2, string shortname, string patientShortAddress, string operatorid, string telePhone,
           string modifiedby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryEditCounterSale(Id, CreditorId, VouType, VouNo,
                VouDate, docId, docshortname, docAddress, vousubtype, countersalenum, patientname, patientID,
                patientaddress1, patientaddress2, shortname, patientShortAddress, operatorid, telePhone,
                modifiedby, modifydate, modifytime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetUpdateQueryEditCounterSale(string Id, string CreditorId, string VouType, int VouNo,
                   string VouDate, string docId, string docshortname, string docAddress, string vousubtype, int countersalenum, string patientname, string patientID,
                   string patientaddress1, string patientaddress2, string patientshortname, string patientShortAddress, string operatorid, string telePhone,
                   string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchersale";
            objQuery.AddToQuery("ID", Id, true);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("PatientID", patientID);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("DoctorID", docId);
            objQuery.AddToQuery("DoctorShortName", docshortname);
            objQuery.AddToQuery("DoctorAddress", docAddress);
            objQuery.AddToQuery("VoucherSubType", vousubtype);
            objQuery.AddToQuery("CounterSaleNumber", countersalenum);
            objQuery.AddToQuery("PatientName", patientname);
            objQuery.AddToQuery("PatientAddress1", patientaddress1);
            objQuery.AddToQuery("PatientAddress2", patientaddress2);
            objQuery.AddToQuery("PatientShortName", patientshortname);
            objQuery.AddToQuery("PatientShortAddress", patientShortAddress);
            objQuery.AddToQuery("Telephone", telePhone);
            objQuery.AddToQuery("ModifiedOperatorID", operatorid);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }
        public bool UpdateDetailsForTypeChange(string purchaseID, string VoucherType, string subType, int VoucherNumber, double amountBalance, double amountClear, string accountID, int CreditCardBankID, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryForTypeChange(purchaseID, VoucherType, subType, VoucherNumber, amountBalance, amountClear, accountID, CreditCardBankID, modifiedby, modifieddate, modifiedtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public DataRow GetProductNameFromScanCode(string scanCode) // [10.02.2017]
        {
            DataRow dr = null;
            string strsql = string.Format("Select B.ProdName, A.StockID, B.ProdLoosePack from tblStock A Inner join masterproduct B on A.ProductID=B.ProductID where (ScanCode='{0}' or ScannedBarcode = '{0}')", scanCode);
            //string strsql = string.Format("Select B.ProdName, A.StockID, B.ProdLoosePack from tblStock A Inner join masterproduct B on A.ProductID=B.ProductID where ScanCode='{0}'", scanCode);
            dr = DBInterface.SelectFirstRow(strsql);

            return dr;
        }

        #endregion

        #region validations

        public bool CheckStock()
        {
            return true;
        }
        #endregion

        #region Query Building Functions


        private string GetUpdateQueryForTypeChange(string purchaseID, string VoucherType, string subType, int VoucherNumber, double amountBalance, double amountClear, string accountID, int CreditCardBankID, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchersale";
            objQuery.AddToQuery("ID", purchaseID, true);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("VoucherSubType", subType);
            objQuery.AddToQuery("AccountID", accountID);
        //    objQuery.AddToQuery("CreditCardBankID", CreditCardBankID); 
            objQuery.AddToQuery("AmountClear", amountClear);
            objQuery.AddToQuery("AmountBalance", amountBalance);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }


        private string GetInsertQuerySale(/*string Id, string AccountId, string Narration1,string Narration2, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd,
            string docId, string docNameAddress, string docAddress, double addon, string vousubtype, double amtbal, double amtclear, double octper, double octamt,
            int countersalenum, int statementnumber, double cramount, double dbamount, string patientname, string patientaddress1,
            string patientaddress2, string patientshortname, string patientShortAddress, double amountforzerovat, string operatorid, int salesmanid,int transporterid, string patientid,
            double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees,
            double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, double itemTotalDiscount,
            string prescriptionFileName, string telephone, double mySpecialDiscountAmount, double mySpecialDiscountPer,
            double MyTotalSpecialDiscountPer12point5, double MyTotalSpecialDiscountPer5, double TotalDiscount12point5,
            double TotalDiscount5, double SchemeTotalDiscount, string iffullpayment, int creditCardBankID, string MobileNumberForSMS, string nextVisitDate,string debtorPatientID,*/

            string Id, string AccountId, string Narration1, string Narration2, string VouType, int VouNo, string VouDate,
            double AmountNet, double DiscPer, double DiscAmt, double Amt, double rnd, string docId, double addon,
            string vousubtype, double amtbal, double amtclear, int statementnumber, double cramount,
            double dbamount, string patientname, string patientaddress1, string patientaddress2, string operatorid, int salesmanid, int transporterid,
            string orderNumber, string orderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate,
            double itemTotalDiscount, double SchemeTotalDiscount,
            double gstAmt0, double gstAmtS5, double gstAmtS12, double gstAmtS18, double gstAmtS28,
            double gstAmtC5, double gstAmtC12, double gstAmtC18, double gstAmtC28, double gsts5, double gsts12, double gsts18, double gsts28,
            double gstc5, double gstc12, double gstc18, double gstc28, double gstAmtI5, double gstAmtI12, double gstAmtI18, double gstAmtI28, double gstI5, double gstI12, double gstI18, double gstI28, string createdby, string createddate, string createdtime)
        {
            int mmoo = 0;
            if (operatorid != string.Empty)
                mmoo = Convert.ToInt32(operatorid.ToString());
            Query objQuery = new Query();
            objQuery.Table = "vouchersale";
          //  objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("AccountID", AccountId);
         //   objQuery.AddToQuery("PatientID", patientid);
            objQuery.AddToQuery("Narration", Narration1);
            //objQuery.AddToQuery("Narration2", Narration2);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("CashDiscountPercent", DiscPer);
            objQuery.AddToQuery("AmountCashDiscount", DiscAmt);
            objQuery.AddToQuery("AmountGross", Amt);
           // objQuery.AddToQuery("VAT5Per", Vat5);
          //  objQuery.AddToQuery("VAT12Point5Per", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountBalance", amtbal);
            objQuery.AddToQuery("DoctorID", docId);
         //   objQuery.AddToQuery("DoctorShortName", docNameAddress);
         //   objQuery.AddToQuery("DoctorAddress", docAddress);
         //   objQuery.AddToQuery("Telephone", telephone);
            objQuery.AddToQuery("AddOnFreight", addon);
            objQuery.AddToQuery("VoucherSubType", vousubtype);
            objQuery.AddToQuery("AmountClear", amtclear);
         //   objQuery.AddToQuery("OctroiPercentage", octper);
         //   objQuery.AddToQuery("AmountOctroi", octamt);
         //   objQuery.AddToQuery("CounterSaleNumber", countersalenum);
            objQuery.AddToQuery("StatementNumber", statementnumber);
            objQuery.AddToQuery("AmountCreditNote", cramount);
            objQuery.AddToQuery("AmountDebitNote", dbamount);
         //   objQuery.AddToQuery("DebtorsPatientID", debtorPatientID);
            objQuery.AddToQuery("PatientName", patientname);
            objQuery.AddToQuery("PatientAddress1", patientaddress1);
            objQuery.AddToQuery("PatientAddress2", patientaddress2);
            //   objQuery.AddToQuery("PatientShortName", patientshortname);
            //   objQuery.AddToQuery("PatientShortAddress", patientShortAddress);
            //   objQuery.AddToQuery("AmountForZeroVAT", amountforzerovat);
               objQuery.AddToQuery("OperatorID", mmoo);
            //   objQuery.AddToQuery("AmountVAT12Point5Per", AmountVat12);
            //   objQuery.AddToQuery("AmountVAT5Per", AmountVat5);
            //   objQuery.AddToQuery("IPDOPDCode", IPDOPD);
            objQuery.AddToQuery("SalesmanID", salesmanid);
            objQuery.AddToQuery("TransporterID", transporterid);
            objQuery.AddToQuery("OrderNumber", orderNumber);
            objQuery.AddToQuery("OrderDate", orderDate);
            objQuery.AddToQuery("ProfitInRupees", totalProfitInRupees);
            objQuery.AddToQuery("ProfitPercentByPurchaseRate", totalProfitPercentbyPurchaseRate);
            objQuery.AddToQuery("ProfitPercentBySaleRate", totalProfitPercentBySaleRate);
      //      objQuery.AddToQuery("AmountPMTDiscount", pmtTotalDiscount);
            objQuery.AddToQuery("AmountItemDiscount", itemTotalDiscount);
        //    objQuery.AddToQuery("ScanPrescriptionFileName", prescriptionFileName);
            //objQuery.AddToQuery("MySpecialDiscountAmount", mySpecialDiscountAmount);
          //  objQuery.AddToQuery("MySpecialDiscountPercent", mySpecialDiscountPer);
         //   objQuery.AddToQuery("MySpecialDiscountAmount12point5", MyTotalSpecialDiscountPer12point5);
         //   objQuery.AddToQuery("MySpecialDiscountAmount5", MyTotalSpecialDiscountPer5);
        //    objQuery.AddToQuery("AmountCashDiscount5", TotalDiscount5);
          //  objQuery.AddToQuery("AmountCashDiscount12point5", TotalDiscount12point5);
            objQuery.AddToQuery("AmountSchemeDiscount", SchemeTotalDiscount);

            objQuery.AddToQuery("AmountGST0", gstAmt0);
            objQuery.AddToQuery("AmountGSTS5", gstAmtS5);
            objQuery.AddToQuery("AmountGSTS12", gstAmtS12);
            objQuery.AddToQuery("AmountGSTS18", gstAmtS18);
            objQuery.AddToQuery("AmountGSTS28", gstAmtS28);
            objQuery.AddToQuery("AmountGSTC5", gstAmtC5);
            objQuery.AddToQuery("AmountGSTC12", gstAmtC12);
            objQuery.AddToQuery("AmountGSTC18", gstAmtC18);
            objQuery.AddToQuery("AmountGSTC28", gstAmtC28);

            objQuery.AddToQuery("GSTS5", gsts5);
            objQuery.AddToQuery("GSTS12", gsts12);
            objQuery.AddToQuery("GSTS18", gsts18);
            objQuery.AddToQuery("GSTS28", gsts28);
            objQuery.AddToQuery("GSTC5", gstc5);
            objQuery.AddToQuery("GSTC12", gstc12);
            objQuery.AddToQuery("GSTC18", gstc18);
            objQuery.AddToQuery("GSTC28", gstc28);

            objQuery.AddToQuery("AmountGSTI5", gstAmtI5);
            objQuery.AddToQuery("AmountGSTI12", gstAmtI12);
            objQuery.AddToQuery("AmountGSTI18", gstAmtI18);
            objQuery.AddToQuery("AmountGSTI28", gstAmtI28);
            objQuery.AddToQuery("GSTI5", gstI5);
            objQuery.AddToQuery("GSTI12", gstI12);
            objQuery.AddToQuery("GSTI18", gstI18);
            objQuery.AddToQuery("GSTI28", gstI28);
            //  objQuery.AddToQuery("IfFullPayment", iffullpayment);
            //   objQuery.AddToQuery("CreditCardBankID", creditCardBankID);
            //   objQuery.AddToQuery("MobileNumberForSMS", MobileNumberForSMS);
            //   objQuery.AddToQuery("NextVisitDate", nextVisitDate);
            //  objQuery.AddToQuery("AmountByPurchaseRate", amountByPurchaseRate);            
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQuerySaleSpecialSale(string Id, string AccountId, string Narration1,string Narration2, string VouType, int VouNo,
           string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd,
          string docId, string docshortname, string docAddress, double addon, string vousubtype, double amtbal, double amtclear, double octper, double octamt,
          int countersalenum, int statementnumber, double cramount, double dbamount, string patientname, string patientaddress1,
          string patientaddress2, string patientshortname, string patientShortAddress, double amountforzerovat, string operatorid, string patientid,
          double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, double itemTotalDiscount, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "specialvouchersale";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("AccountID", AccountId);
            objQuery.AddToQuery("PatientID", patientid);
            objQuery.AddToQuery("Narration1", Narration1);
            objQuery.AddToQuery("Narration2", Narration2);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("CashDiscountPercent", DiscPer);
            objQuery.AddToQuery("AmountCashDiscount", DiscAmt);
            objQuery.AddToQuery("AmountGross", Amt);
            objQuery.AddToQuery("VAT5Per", Vat5);
            objQuery.AddToQuery("VAT12Point5Per", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountBalance", amtbal);
            objQuery.AddToQuery("DoctorID", docId);
            objQuery.AddToQuery("DoctorShortName", docshortname);
            objQuery.AddToQuery("DoctorAddress", docAddress);
            objQuery.AddToQuery("AddOnFreight", addon);
            objQuery.AddToQuery("VoucherSubType", vousubtype);
            objQuery.AddToQuery("AmountClear", amtclear);
            objQuery.AddToQuery("OctroiPercentage", octper);
            objQuery.AddToQuery("AmountOctroi", octamt);
            objQuery.AddToQuery("CounterSaleNumber", countersalenum);
            objQuery.AddToQuery("StatementNumber", statementnumber);
            objQuery.AddToQuery("AmountCreditNote", cramount);
            objQuery.AddToQuery("AmountDebitNote", dbamount);
            objQuery.AddToQuery("PatientName", patientname);
            objQuery.AddToQuery("PatientAddress1", patientaddress1);
            objQuery.AddToQuery("PatientAddress2", patientaddress2);
            objQuery.AddToQuery("PatientShortName", patientshortname);
            objQuery.AddToQuery("PatientShortAddress", patientShortAddress);
            objQuery.AddToQuery("AmountForZeroVAT", amountforzerovat);
            objQuery.AddToQuery("OperatorID", operatorid);
            objQuery.AddToQuery("AmountVAT12Point5Per", AmountVat12);
            objQuery.AddToQuery("AmountVAT5Per", AmountVat5);
            objQuery.AddToQuery("IPDOPDCode", IPDOPD);
            objQuery.AddToQuery("OrderNumber", orderNumber);
            objQuery.AddToQuery("OrderDate", orderDate);
            objQuery.AddToQuery("ProfitInRupees", totalProfitInRupees);
            objQuery.AddToQuery("ProfitPercentByPurchaseRate", totalProfitPercentbyPurchaseRate);
            objQuery.AddToQuery("ProfitPercentBySaleRate", totalProfitPercentBySaleRate);
            objQuery.AddToQuery("AmountPMTDiscount", pmtTotalDiscount);
            objQuery.AddToQuery("AmountItemDiscount", itemTotalDiscount);
            //  objQuery.AddToQuery("AmountByPurchaseRate", amountByPurchaseRate);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryProducts(string Id, int ProductID, string Batchno, int quantity, int SchemeQuantity,
              double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount, string VouType, int VouNo, string VouDate)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailsale";
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("Quantity", quantity);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("ReasonCode", reasoncode);
            objQuery.AddToQuery("VATPer", VatPer);
            objQuery.AddToQuery("Amount", Amount);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryProductsSS(int Id, int ProductID, string Batchno, int quantity,
                double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
                string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid,
                string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate,
                double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer,
                double itemDiscountAmt, double cashDiscountAmount, double mySpecialDiscountamount, double schemeDiscountAmount, 
                int schemeDiscountQuantity, string vouDate, int vouNo, string vouType, double gstPurchaseAmountZero, double gstSPurchaseAmount, double gstCPurchaseAmount,
               double gstSAmount, double gstCAmount, string newbatchno, double newmrp, double newsalerate)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailsale";
            objQuery.AddToQuery("MasterSaleID", Id);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("Quantity", quantity);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("VATPer", VatPer);
            objQuery.AddToQuery("Amount", Amount);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("VATAmount", VatAmt);
            objQuery.AddToQuery("IfProductDiscount", ifproddisc);
            objQuery.AddToQuery("StockID", stockid);
         //   objQuery.AddToQuery("DetailSaleID", mydetailsaleid);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            objQuery.AddToQuery("ProfitInRupees", ProfitInRupees);
            objQuery.AddToQuery("ProfitPercentBySaleRate", ProfitBySaleRate);
            objQuery.AddToQuery("ProfitPercentByPurchaseRate", ProfitByPurchaseRate);
            objQuery.AddToQuery("PMTDiscount", pmtDiscountPer);
            objQuery.AddToQuery("PMTAmount", pmtDiscountAmt);
            objQuery.AddToQuery("ItemDiscountPer", itemDiscountPer);
            objQuery.AddToQuery("ItemDiscountAmount", itemDiscountAmt);
            objQuery.AddToQuery("CashDiscountAmount", cashDiscountAmount);
          //  objQuery.AddToQuery("MySpecialDiscountAmount", mySpecialDiscountamount);
            objQuery.AddToQuery("SchemeDiscountAmount", schemeDiscountAmount);
            objQuery.AddToQuery("SchemeQuantity", schemeDiscountQuantity);
            objQuery.AddToQuery("VoucherDate", vouDate);

            objQuery.AddToQuery("GSTAmountZero", gstPurchaseAmountZero);
            objQuery.AddToQuery("GSTSAmount", gstSPurchaseAmount);
            objQuery.AddToQuery("GSTCAmount", gstCPurchaseAmount);
            objQuery.AddToQuery("GSTS", gstSAmount);
            objQuery.AddToQuery("GSTC", gstCAmount);

            objQuery.AddToQuery("ActualBatchNumber", newbatchno);
            objQuery.AddToQuery("ActualMRP", newmrp);
            objQuery.AddToQuery("ActualSaleRate", newsalerate);
            //   objQuery.AddToQuery("VoucherNumber", vouNo);
            //  objQuery.AddToQuery("VoucherType", vouType);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryProductsSSSpecialSale(string Id, int ProductID, string Batchno, int quantity,
              double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
              string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            Query objQuery = new Query();
            objQuery.Table = "specialdetailsale";
            objQuery.AddToQuery("MasterSaleID", Id);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("Quantity", quantity);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("VATPer", VatPer);
            objQuery.AddToQuery("Amount", Amount);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("VATAmount", VatAmt);
            objQuery.AddToQuery("IfProductDiscount", ifproddisc);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("DetailSaleID", mydetailsaleid);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            objQuery.AddToQuery("ProfitInRupees", ProfitInRupees);
            objQuery.AddToQuery("ProfitPercentBySaleRate", ProfitBySaleRate);
            objQuery.AddToQuery("ProfitPercentByPurchaseRate", ProfitByPurchaseRate);
            objQuery.AddToQuery("PMTDiscount", pmtDiscountPer);
            objQuery.AddToQuery("PMTAmount", pmtDiscountAmt);
            objQuery.AddToQuery("ItemDiscountPer", itemDiscountPer);
            objQuery.AddToQuery("ItemDiscountAmount", itemDiscountAmt);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryDeletedProductsSS(string Id, int ProductID, string Batchno, int quantity,
                double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
                string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            Query objQuery = new Query();
            objQuery.Table = "Deleteddetailsale";
            objQuery.AddToQuery("MasterSaleID", Id);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("Quantity", quantity);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("VATPer", VatPer);
            objQuery.AddToQuery("Amount", Amount);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("VATAmount", VatAmt);
            objQuery.AddToQuery("IfProductDiscount", ifproddisc);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("DetailSaleID", mydetailsaleid);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            objQuery.AddToQuery("ProfitInRupees", ProfitInRupees);
            objQuery.AddToQuery("ProfitPercentBySaleRate", ProfitBySaleRate);
            objQuery.AddToQuery("ProfitPercentByPurchaseRate", ProfitByPurchaseRate);
            objQuery.AddToQuery("PMTDiscount", pmtDiscountPer);
            objQuery.AddToQuery("PMTAmount", pmtDiscountAmt);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryChangedProductsSS(string Id, string ChangedID, int ProductID, string Batchno, int quantity,
              double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
              string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            Query objQuery = new Query();
            objQuery.Table = "Changeddetailsale";
            objQuery.AddToQuery("MasterSaleID", Id);
            objQuery.AddToQuery("ChangedMasterID", ChangedID);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("Quantity", quantity);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("VATPer", VatPer);
            objQuery.AddToQuery("Amount", Amount);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("VATAmount", VatAmt);
            objQuery.AddToQuery("IfProductDiscount", ifproddisc);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("DetailSaleID", mydetailsaleid);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            objQuery.AddToQuery("ProfitInRupees", ProfitInRupees);
            objQuery.AddToQuery("ProfitPercentBySaleRate", ProfitBySaleRate);
            objQuery.AddToQuery("ProfitPercentByPurchaseRate", ProfitByPurchaseRate);
            objQuery.AddToQuery("PMTDiscount", pmtDiscountPer);
            objQuery.AddToQuery("PMTAmount", pmtDiscountAmt);
            objQuery.AddToQuery("ItemDiscountPer", itemDiscountPer);
            objQuery.AddToQuery("ItemDiscountAmount", itemDiscountAmt);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuerySale(string Id, string CreditorId, string Narration1, string Narration2, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd,
            double balamt, string docId, string docNameAddress, string docAddress, double addon, string vousubtype,
            double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber, double cramount,
            double dbamount, string patientname, string patientaddress1, string patientaddress2, string patientshortname, string patientShortAddress, double amountforzerovat,
            string operatorid, string patientID, double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees,
            double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, double itemTotalDiscount,
            string prescriptionFileName, string telephone, double mySpecialDiscountAmount, double MySpecialDiscountPercent, double MyTotalSpecialDiscountPer12point5, double MyTotalSpecialDiscountPer5,
            double TotalDiscount12point5, double TotalDiscount5,double  SchemeTotalDiscount,string iffullpayment, string nextVisitDate, string debtorPatientID,
            double gstAmt0, double gstAmtS5, double gstAmtS12, double gstAmtS18, double gstAmtS28,
            double gstAmtC5, double gstAmtC12, double gstAmtC18, double gstAmtC28, double gsts5, double gsts12, double gsts18, double gsts28,
            double gstc5, double gstc12, double gstc18, double gstc28, double gstAmtI5, double gstAmtI12, double gstAmtI18, double gstAmtI28, double gstI5, double gstI12, double gstI18, double gstI28, string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchersale";
            objQuery.AddToQuery("ID", Id, true);            
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration1);
            //objQuery.AddToQuery("Narration2", Narration2);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("CashDiscountPercent", DiscPer);
            objQuery.AddToQuery("AmountCashDiscount", DiscAmt);
            objQuery.AddToQuery("AmountGross", Amt);
        //    objQuery.AddToQuery("VAT5Per", Vat5);
        //    objQuery.AddToQuery("VAT12Point5Per", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountBalance", amtbal);
            objQuery.AddToQuery("DoctorID", docId);
         //   objQuery.AddToQuery("DoctorShortName", docNameAddress);
         //   objQuery.AddToQuery("DoctorAddress", docAddress);
        //    objQuery.AddToQuery("Telephone", telephone);
            objQuery.AddToQuery("AddOnFreight", addon);
            objQuery.AddToQuery("VoucherSubType", vousubtype);
            objQuery.AddToQuery("AmountClear", amtclear);
       //     objQuery.AddToQuery("OctroiPercentage", octper);
        //    objQuery.AddToQuery("AmountOctroi", octamt);
        //    objQuery.AddToQuery("CounterSaleNumber", countersalenum);
            objQuery.AddToQuery("StatementNumber", statementnumber);
            objQuery.AddToQuery("AmountCreditNoteStock", cramount);
            objQuery.AddToQuery("AmountDebitNote", dbamount);
        //    objQuery.AddToQuery("PatientID", patientID);
         //   objQuery.AddToQuery("DebtorsPatientID", debtorPatientID);
         //   objQuery.AddToQuery("PatientName", patientname);
        //    objQuery.AddToQuery("PatientAddress1", patientaddress1);
        //    objQuery.AddToQuery("PatientAddress2", patientaddress2);
        //    objQuery.AddToQuery("PatientShortName", patientshortname);
       //     objQuery.AddToQuery("PatientShortAddress", patientShortAddress);
       //     objQuery.AddToQuery("AmountForZeroVAT", amountforzerovat);
       //     objQuery.AddToQuery("OperatorID", operatorid);
         //   objQuery.AddToQuery("AmountVAT12Point5Per", AmountVat12);
        //    objQuery.AddToQuery("AmountVAT5Per", AmountVat5);
        //    objQuery.AddToQuery("IPDOPDCode", IPDOPD);
            objQuery.AddToQuery("OrderNumber", orderNumber);
            objQuery.AddToQuery("OrderDate", orderDate);
            objQuery.AddToQuery("ProfitInRupees", totalProfitInRupees);
            objQuery.AddToQuery("ProfitPercentByPurchaseRate", totalProfitPercentbyPurchaseRate);
            objQuery.AddToQuery("ProfitPercentBySaleRate", totalProfitPercentBySaleRate);
        //    objQuery.AddToQuery("AmountPMTDiscount", pmtTotalDiscount);
            objQuery.AddToQuery("AmountItemDiscount", itemTotalDiscount);
        //    objQuery.AddToQuery("ScanPrescriptionFileName", prescriptionFileName);
          //  objQuery.AddToQuery("MySpecialDiscountAmount", mySpecialDiscountAmount);
         //   objQuery.AddToQuery("MySpecialDiscountPercent", MySpecialDiscountPercent);
         //   objQuery.AddToQuery("MySpecialDiscountAmount12point5", MyTotalSpecialDiscountPer12point5);
        //    objQuery.AddToQuery("MySpecialDiscountAmount5", MyTotalSpecialDiscountPer5);
         //   objQuery.AddToQuery("AmountCashDiscount5", TotalDiscount5);
       //     objQuery.AddToQuery("AmountCashDiscount12point5", TotalDiscount12point5);
            objQuery.AddToQuery("AmountSchemeDiscount", SchemeTotalDiscount);
            //   objQuery.AddToQuery("IfFullPayment", iffullpayment);
            //    objQuery.AddToQuery("NextVisitDate", nextVisitDate);

            objQuery.AddToQuery("AmountGST0", gstAmt0);
            objQuery.AddToQuery("AmountGSTS5", gstAmtS5);
            objQuery.AddToQuery("AmountGSTS12", gstAmtS12);
            objQuery.AddToQuery("AmountGSTS18", gstAmtS18);
            objQuery.AddToQuery("AmountGSTS28", gstAmtS28);
            objQuery.AddToQuery("AmountGSTC5", gstAmtC5);
            objQuery.AddToQuery("AmountGSTC12", gstAmtC12);
            objQuery.AddToQuery("AmountGSTC18", gstAmtC18);
            objQuery.AddToQuery("AmountGSTC28", gstAmtC28);

            objQuery.AddToQuery("GSTS5", gsts5);
            objQuery.AddToQuery("GSTS12", gsts12);
            objQuery.AddToQuery("GSTS18", gsts18);
            objQuery.AddToQuery("GSTS28", gsts28);
            objQuery.AddToQuery("GSTC5", gstc5);
            objQuery.AddToQuery("GSTC12", gstc12);
            objQuery.AddToQuery("GSTC18", gstc18);
            objQuery.AddToQuery("GSTC28", gstc28);

            objQuery.AddToQuery("AmountGSTI5", gstAmtI5);
            objQuery.AddToQuery("AmountGSTI12", gstAmtI12);
            objQuery.AddToQuery("AmountGSTI18", gstAmtI18);
            objQuery.AddToQuery("AmountGSTI28", gstAmtI28);
            objQuery.AddToQuery("GSTI5", gstI5);
            objQuery.AddToQuery("GSTI12", gstI12);
            objQuery.AddToQuery("GSTI18", gstI18);
            objQuery.AddToQuery("GSTI28", gstI28);

            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }

        private string GetAddDetailsInDeleteMaster(string Id, string CreditorId, string Narration1,string Narration2, string VouType, int VouNo,
            string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd,
           double balamt, string docId, string docshortname, string docAddress, double addon, string vousubtype,
           double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber, double cramount,
           double dbamount, string patientname, string patientaddress1, string patientaddress2, string patientshortname, string patientShortAddress, double amountforzerovat,
           string operatorid, double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, string telephone, string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "DeletedVouchersale";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration1);
            //objQuery.AddToQuery("Narration2", Narration2);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("CashDiscountPercent", DiscPer);
            objQuery.AddToQuery("AmountCashDiscount", DiscAmt);
            objQuery.AddToQuery("AmountGross", Amt);
            objQuery.AddToQuery("VAT5Per", Vat5);
            objQuery.AddToQuery("VAT12Point5Per", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountBalance", balamt);
            objQuery.AddToQuery("DoctorID", docId);
            objQuery.AddToQuery("DoctorShortName", docshortname);
            objQuery.AddToQuery("DoctorAddress", docAddress);
            objQuery.AddToQuery("AddOnFreight", addon);
            objQuery.AddToQuery("VoucherSubType", vousubtype);
            objQuery.AddToQuery("AmountClear", amtclear);
            objQuery.AddToQuery("OctroiPercentage", octper);
            objQuery.AddToQuery("AmountOctroi", octamt);
            objQuery.AddToQuery("CounterSaleNumber", countersalenum);
            objQuery.AddToQuery("StatementNumber", statementnumber);
            objQuery.AddToQuery("AmountCreditNote", cramount);
            objQuery.AddToQuery("AmountDebitNote", dbamount);
            objQuery.AddToQuery("PatientName", patientname);
            objQuery.AddToQuery("PatientAddress1", patientaddress1);
            objQuery.AddToQuery("PatientAddress2", patientaddress2);
            objQuery.AddToQuery("PatientShortName", patientshortname);
            objQuery.AddToQuery("PatientShortAddress", patientShortAddress);
            objQuery.AddToQuery("Telephone", telephone);
            objQuery.AddToQuery("AmountForZeroVAT", amountforzerovat);
            objQuery.AddToQuery("OperatorID", operatorid);
            objQuery.AddToQuery("AmountVAT12Point5Per", AmountVat12);
            objQuery.AddToQuery("AmountVAT5Per", AmountVat5);
            objQuery.AddToQuery("IPDOPDCode", IPDOPD);
            objQuery.AddToQuery("OrderNumber", orderNumber);
            objQuery.AddToQuery("OrderDate", orderDate);
            objQuery.AddToQuery("ProfitInRupees", totalProfitInRupees);
            objQuery.AddToQuery("ProfitPercentByPurchaseRate", totalProfitPercentbyPurchaseRate);
            objQuery.AddToQuery("ProfitPercentBySaleRate", totalProfitPercentBySaleRate);
            objQuery.AddToQuery("AmountPMTDiscount", pmtTotalDiscount);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.InsertQuery();

        }

        private string GetAddDetailsInChangedMaster(string Id, string ChangedID, string CreditorId, string Narration1,string Narration2, string VouType, int VouNo,
            string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd,
           double balamt, string docId, string docshortname, string docAddress, double addon, string vousubtype,
           double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber, double cramount,
           double dbamount, string patientname, string patientaddress1, string patientaddress2, string patientshortname, string patientShortAddress, double amountforzerovat,
           string operatorid, double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, string telephone, string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "ChangedVouchersale";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("ChangedID", ChangedID);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration1);
            //objQuery.AddToQuery("Narration2", Narration2);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("CashDiscountPercent", DiscPer);
            objQuery.AddToQuery("AmountCashDiscount", DiscAmt);
            objQuery.AddToQuery("AmountGross", Amt);
            objQuery.AddToQuery("VAT5Per", Vat5);
            objQuery.AddToQuery("VAT12Point5Per", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountBalance", balamt);
            objQuery.AddToQuery("DoctorID", docId);
            objQuery.AddToQuery("DoctorShortName", docshortname);
            //objQuery.AddToQuery("DoctorAddress", docAddress);
            objQuery.AddToQuery("Telephone", telephone);
            objQuery.AddToQuery("AddOnFreight", addon);
            objQuery.AddToQuery("VoucherSubType", vousubtype);
            objQuery.AddToQuery("AmountClear", amtclear);
            objQuery.AddToQuery("OctroiPercentage", octper);
            objQuery.AddToQuery("AmountOctroi", octamt);
            objQuery.AddToQuery("CounterSaleNumber", countersalenum);
            objQuery.AddToQuery("StatementNumber", statementnumber);
            objQuery.AddToQuery("AmountCreditNote", cramount);
            objQuery.AddToQuery("AmountDebitNote", dbamount);
            objQuery.AddToQuery("PatientName", patientname);
            objQuery.AddToQuery("PatientAddress1", patientaddress1);
            objQuery.AddToQuery("PatientAddress2", patientaddress2);
            objQuery.AddToQuery("PatientShortName", patientshortname);
            //objQuery.AddToQuery("PatientShortAddress", patientShortAddress);
            objQuery.AddToQuery("AmountForZeroVAT", amountforzerovat);
            objQuery.AddToQuery("OperatorID", operatorid);
            objQuery.AddToQuery("AmountVAT12Point5Per", AmountVat12);
            objQuery.AddToQuery("AmountVAT5Per", AmountVat5);
            objQuery.AddToQuery("IPDOPDCode", IPDOPD);
            objQuery.AddToQuery("OrderNumber", orderNumber);
            objQuery.AddToQuery("OrderDate", orderDate);
            objQuery.AddToQuery("ProfitInRupees", totalProfitInRupees);
            objQuery.AddToQuery("ProfitPercentByPurchaseRate", totalProfitPercentbyPurchaseRate);
            objQuery.AddToQuery("ProfitPercentBySaleRate", totalProfitPercentBySaleRate);
            objQuery.AddToQuery("AmountPMTDiscount", pmtTotalDiscount);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.InsertQuery();

        }

        //private string GetUpdateQueryEditCounterSale(string Id, string CreditorId, string VouType, int VouNo,
        //    string VouDate, string docId, string docshortname, string docAddress, string vousubtype, int countersalenum, string patientname, string patientID,
        //    string patientaddress1, string patientaddress2, string patientshortname, string patientShortAddress, string operatorid, string telePhone,
        //    string modifiedby, string modifydate, string modifytime)
        //{
        //    Query objQuery = new Query();
        //    objQuery.Table = "vouchersale";
        //    objQuery.AddToQuery("ID", Id, true);
        //    objQuery.AddToQuery("AccountId", CreditorId);
        //    objQuery.AddToQuery("PatientID", patientID);
        //    objQuery.AddToQuery("VoucherType", VouType);
        //    objQuery.AddToQuery("VoucherNumber", VouNo);
        //    objQuery.AddToQuery("VoucherDate", VouDate);
        //    objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
        //    objQuery.AddToQuery("DoctorID", docId);
        //    objQuery.AddToQuery("DoctorShortName", docshortname);
        //    objQuery.AddToQuery("DoctorAddress", docAddress);
        //    objQuery.AddToQuery("VoucherSubType", vousubtype);
        //    objQuery.AddToQuery("CounterSaleNumber", countersalenum);
        //    objQuery.AddToQuery("PatientName", patientname);
        //    objQuery.AddToQuery("PatientAddress1", patientaddress1);
        //    objQuery.AddToQuery("PatientAddress2", patientaddress2);
        //    objQuery.AddToQuery("PatientShortName", patientshortname);
        //    objQuery.AddToQuery("PatientShortAddress", patientShortAddress);
        //    objQuery.AddToQuery("Telephone", telePhone);
        //    objQuery.AddToQuery("ModifiedOperatorID", operatorid);
        //    objQuery.AddToQuery("ModifiedUserID", modifiedby);
        //    objQuery.AddToQuery("ModifiedDate", modifydate);
        //    objQuery.AddToQuery("ModifiedTime", modifytime);
        //    return objQuery.UpdateQuery();
        //}
        private string GetDeleteQuery(string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "vouchersale";
            objQuery.AddToQuery("ID", Id, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }
        private string GetDeleteQueryPrescription(string prescriptionID, string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "tblscanprescriptions";
            objQuery.AddToQuery("SaleBillID", Id, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }
        private string GetDeleteQueryProducts(string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "detailcreditdebitnotestock";
            objQuery.AddToQuery("MasterID", Id, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }

        private string GetDeleteDebtorSaleQuery(string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "detailsale";
            objQuery.AddToQuery("MasterSaleID", Id, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }
        private string GetDeleteDebtorSaleQueryFromtblTrnac(string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("VoucherID", Id, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }

        public bool AddVoucherIntblTrnac(int Id, int debitAccount, int creditAccount, string Narration, string VouType, int VouNo,
          string VouDate, double debitamount, double creditamount, string detailID, string ShortName, string SaleSubType, string createdby, string createddate, string createdtime)
        {
            bool retValue = false;
            string strSql = GetInsertQueryTrnacForVoucher(Id, debitAccount, creditAccount, Narration, VouType, VouNo,
                VouDate, debitamount, creditamount, detailID, ShortName, SaleSubType, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retValue = true;
            }
            return retValue;
        }


        private string GetInsertQueryTrnacForVoucher(int Id, int debitaccount, int creditaccount, string Narration, string VouType, int VouNo,
          string VouDate, double debitamount, double creditamount, string DetailId, string ShortName, string SaleSubType, string createdby, string createddate, string createdtime)
        {
            int crby = 0;
            if (createdby != string.Empty)
                crby = Convert.ToInt32(createdby);
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
          //  objQuery.AddToQuery("tblTrnacID", DetailId);
            objQuery.AddToQuery("VoucherID", Id);
            objQuery.AddToQuery("AccountId", debitaccount);
            objQuery.AddToQuery("Debit", debitamount);
            objQuery.AddToQuery("Credit", creditamount);
            objQuery.AddToQuery("AccAccountID", creditaccount);
            objQuery.AddToQuery("TransactionDate", VouDate);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("ReferenceVoucherId", 0);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("ShortName", ShortName);
            objQuery.AddToQuery("VoucherSubType", SaleSubType);
            objQuery.AddToQuery("CreatedUserID", crby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);

            return objQuery.InsertQuery();
        }

        private string GetVoucherSaleDeleteMasterQuery(string masterSaleID)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "vouchersale";
            objQuery.AddToQuery("ID", masterSaleID, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }

        private string GetDeleteDetailsVoucherSaleQuery(string mmasterSaleID)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "detailsale";
            objQuery.AddToQuery("MasterSaleID", mmasterSaleID, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }

        private string GetUpdateVoucherSaleDeleteDetailsQuery(string mdetailSaleID)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "detailsale";
            objQuery.AddToQuery("DetailSaleID", mdetailSaleID, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }
        private string GetUpdateVoucherSaleUpdateMasterQuery(string mmasterSaleID, double mamt, double mvatamt5, double mvatamt12point5, double mvatamtforZero, double mvat5, double mvat12point5, double mProfitInRupees, double mTotalProfitPercentBySaleRate, double mTotalProfitPercentByPurchaseRate)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchersale";
            objQuery.AddToQuery("ID", mmasterSaleID, true);
            objQuery.AddToQuery("AmountNet", mamt);
            objQuery.AddToQuery("AmountClear", mamt);
            objQuery.AddToQuery("AmountGross", mamt);
            objQuery.AddToQuery("Vat5Per", mvat5);
            objQuery.AddToQuery("Vat12point5Per", mvat12point5);
            objQuery.AddToQuery("AmountVat5Per", mvatamt5);
            objQuery.AddToQuery("AmountVat12point5Per", mvatamt12point5);
            objQuery.AddToQuery("AmountForZeroVat", mvatamtforZero);
            objQuery.AddToQuery("ProfitInRupees", mProfitInRupees);
            objQuery.AddToQuery("ProfitPercentBySaleRate", mTotalProfitPercentBySaleRate);
            objQuery.AddToQuery("ProfitPercentByPurchaseRate", mTotalProfitPercentByPurchaseRate);
            return objQuery.UpdateQuery();
        }


        //private string GetInsertQueryNewPatient(string PatientID, string Name, string PatientAddress1, string PatientAddress2, string shortName, string Telephone, string mobileNumberForSMS, string DocID, string doctorNameAddress, string docAddress, string createdby, string createddate, string createdtime)
        //{
        //    Query objQuery = new Query();
        //    objQuery.Table = "masterpatient";
        //    objQuery.AddToQuery("PatientID", PatientID);
        //    objQuery.AddToQuery("PatientName", Name);
        //    objQuery.AddToQuery("AccCode", "P");
        //    objQuery.AddToQuery("PatientAddress1", PatientAddress1);
        //    objQuery.AddToQuery("PatientAddress2", PatientAddress2);
        //    objQuery.AddToQuery("ShortNameAddress", shortName);
        //    objQuery.AddToQuery("TelephoneNumber", Telephone);
        //    objQuery.AddToQuery("MobileNumberForSMS", mobileNumberForSMS);
        //    objQuery.AddToQuery("DoctorID", DocID);
        //    objQuery.AddToQuery("CreatedUserID", createdby);
        //    objQuery.AddToQuery("CreatedDate", createddate);
        //    objQuery.AddToQuery("CreatedTime", createdtime);
        //    return objQuery.InsertQuery();
        //}
        private string GetInsertQueryNewDoctor(string DocID, string doctorNameAddress, string doctorAddress, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterDoctor";
            objQuery.AddToQuery("DocID", DocID);
            objQuery.AddToQuery("DocName", doctorNameAddress);
            objQuery.AddToQuery("DocShortNameAddress", doctorNameAddress);
            objQuery.AddToQuery("DocAddress", doctorAddress);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
        #endregion Private Methods

        public void UpdateDetailsInTempPurchase(string TempChallanID, int Quantity, string LastStockID, string CreatedBy, string CreatedDate, string CreatedTime)
        {
           // bool bRetValue = false;
            string strSql = "Update tblTempPurchase set Quantity = Quantity " + Quantity + " where StockID = '" + LastStockID + "'";
        }

        public void AddDetailsInTempPurchase(string TempChallanID, int ProductID, string Batchno, int Quantity, double PurchaseRate, double MRP,  double TradeRate, string Expiry, string LastStockID, string CreatedBy, string CreatedDate, string CreatedTime)
        {
           // bool bRetValue = false;
            string strSql = GetInsertQueryInTempPurchase(TempChallanID, ProductID, Batchno, Quantity, PurchaseRate,  MRP, TradeRate, Expiry,  LastStockID, CreatedBy,  CreatedDate,  CreatedTime);
            DBInterface.ExecuteQuery(strSql);           
        }
        private string GetInsertQueryInTempPurchase(string TempChallanID, int ProductID, string Batchno, int Quantity, double PurchaseRate, double MRP,double TradeRate, string Expiry, string LastStockID, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblTempPurchase";
            objQuery.AddToQuery("ID", TempChallanID);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber",Batchno);
            objQuery.AddToQuery("MRP",MRP);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("ClearedQuantity", 0);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
           // objQuery.AddToQuery("TradeRate",TradeRate);
            objQuery.AddToQuery("Expiry", Expiry);           
            objQuery.AddToQuery("StockID", LastStockID);
            objQuery.AddToQuery("CreatedUserID",CreatedBy);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            return objQuery.InsertQuery();
        }

        public void UpdateDetailSaleForNewVoucherTypeAndNumber(string Id, string vouID, string vouType, int vouNumber)
        {
           
            string strSql = "Update DetailSale set MasterSaleID = '" + Id +"', VoucherType = '"+ vouType +"', VoucherNumber = "+ vouNumber+" where DetailSaleID = '"+ vouID +"'";

            DBInterface.ExecuteQuery(strSql);  
        }

        public DataRow GetPartyOtherDetails(string partyID)
        {
            DataRow dr = null;
            string strSql = "Select * from masterAccount where accountID = '"+ partyID +"'";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }


        public DataRow GetLastRecordForSale(string voutype,string vousubtype, string vouSeries)
        {
            DataRow dRow = null;            
            {
                string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration1,a.Narration2,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT12Point5Per,a.AmountVAT5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,b.AccountID,b.AccTokenNumber,a.OrderNumber,a.OrderDate from vouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.VoucherType =  '" + voutype + "'  AND a.VoucherSubType = '" + vousubtype + "' AND a.VoucherSeries = '"+ vouSeries +"'  order by a.Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetLastVoucherNumber(string voutype, string vousubtype, string vouseries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select Vouchernumber from vouchersale where  VoucherType =  '" + voutype + "'  AND  VoucherSubType = '" + vousubtype + "' AND  VoucherSeries = '" + vouseries + "' order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow GetFirstVoucherNumber(string voutype, string vousubtype , string vouseries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select Vouchernumber from vouchersale where VoucherType =  '" + voutype + "'  AND  VoucherSubType = '" + vousubtype + "' AND  VoucherSeries = '" + vouseries + "'  order by Vouchernumber";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetFirstRecord(string CrdbVouType, string SaleSubType, string crdbVoucherSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration1,a.Narration2,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT12Point5Per,a.AmountVAT5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,b.AccountID,b.AccTokenNumber,a.OrderNumber,a.OrderDate from vouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.VoucherType =  '" + CrdbVouType + "'  AND a.VoucherSubType = '" + SaleSubType + "' AND a.VoucherSeries = '"+ crdbVoucherSeries+"'  order by a.Vouchernumber ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }


        public DataRow GetLastRecordForSaleSpecialSale(string voutype, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration1,a.Narrtion2,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT12Point5Per,a.AmountVAT5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,b.AccountID,b.AccTokenNumber,a.OrderNumber,a.OrderDate from specialvouchersale a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.VoucherType =  '" + voutype + "'   AND a.VoucherSeries = '" + vouSeries + "'  order by a.Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetLastVoucherNumberSpecialSale(string voutype, string vouseries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select Vouchernumber from specialvouchersale where  VoucherType =  '" + voutype + "'  AND  VoucherSeries = '" + vouseries + "' order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow GetFirstVoucherNumberSpecialSale(string voutype, string vouseries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select Vouchernumber from specialvouchersale where VoucherType =  '" + voutype + "'  AND  VoucherSeries = '" + vouseries + "'  order by Vouchernumber";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetFirstRecordSpecialSale(string CrdbVouType, string crdbVoucherSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration1,a.Narration2,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT12Point5Per,a.AmountVAT5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,b.AccountID,b.AccTokenNumber,a.OrderNumber,a.OrderDate from specialvouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.VoucherType =  '" + CrdbVouType + "'  AND a.VoucherSeries = '" + crdbVoucherSeries + "'  order by a.Vouchernumber ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow IfMultipleMrp(string mprodno, string mbatchno, double mmrpn)
        {
            DataRow dr = null;
            string strSql = "select * from tblstock where ProductID = '{0}' AND mrp != {1} AND closingStock > 0 ";
            strSql = string.Format(strSql, mprodno,  mmrpn);
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }

        public DataTable GetDataForSummary(string _MFromDate, string _MToDate)
        {
            DataTable dt;
            string strSql = "Select VoucherType, sum(AmountNet) as AmountNet from vouchersale where voucherdate >= '" + _MFromDate + "' AND voucherDate <= '" + _MToDate + "' group by vouchertype " +
                            " union Select VoucherType,sum(Amount) as AmountNet from detailSale where voucherdate >= '" + _MFromDate + "' AND voucherDate <= '" + _MToDate + "' AND VoucherType = '"+ FixAccounts.VoucherTypeForVoucherSale +"' group by vouchertype " +
                            " union Select VoucherType, sum(AmountNet) as AmountNet from voucherpurchase where voucherdate >= '" + _MFromDate + "' AND voucherDate <= '" + _MToDate + "' group by vouchertype " +
                            " union Select VoucherType, sum(AmountNet) as AmountNet from vouchercreditdebitnote where voucherdate >= '" + _MFromDate + "' AND voucherDate <= '" + _MToDate + "' group by vouchertype " +
                            " union Select VoucherType, sum(AmountNet) as AmountNet from vouchercashbankreceipt where voucherdate >= '" + _MFromDate + "' AND voucherDate <= '" + _MToDate + "' group by vouchertype " +
                            " union Select VoucherType, sum(AmountNet) as AmountNet from vouchercashbankpayment where voucherdate >= '" + _MFromDate + "' AND voucherDate <= '" + _MToDate + "' group by vouchertype " +
                            " union Select VoucherType, sum(AmountNet) as AmountNet from vouchercashbankExpenses where voucherdate >= '" + _MFromDate + "' AND voucherDate <= '" + _MToDate + "' group by vouchertype " +
                            " union Select ChequeReturnVoucherType as VoucherType, sum(AmountNet) as AmountNet from VoucherChequeReturn where  ChequeReturnvoucherdate >= '" + _MFromDate + "' AND ChequeReturnvoucherDate <= '" + _MToDate + "' group by vouchertype ";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable GetDataForSaleSummary(string _MFromDate, string _MToDate)
        {
            DataTable dt;
            string strSql = "Select VoucherType, sum(AmountNet) as AmountNet, sum(ProfitInRupees) as ProfitInRupees from vouchersale where voucherdate >= '" + _MFromDate + "' AND voucherDate <= '" + _MToDate + "' group by vouchertype " +
                            " union Select VoucherType,sum(Amount) as AmountNet, sum(ProfitInRupees) as ProfitInRupees from detailSale where voucherdate >= '" + _MFromDate + "' AND voucherDate <= '" + _MToDate + "' AND VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "' group by vouchertype ";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable GetDataForSummaryFromToNumber(string _MFromDate, string _MToDate, string _MFromTime, string _MToTime)
        {
            DataTable dt;
            string strSql = "Select ID, VoucherType, vouchernumber, Voucherdate,vouchersubtype, AmountNet,PatientName as AccName,createddate,createdtime,modifieddate from vouchersale where (createddate >= '" + _MFromDate + "' AND createddate <= '" + _MToDate + "' ) OR (modifieddate >= '" + _MFromDate + "' AND modifieddate <= '" + _MToDate + "') " +
                //" union Select VoucherType,sum(Amount) as AmountNet from detailSale where voucherdate >= '" + _MFromDate + "' AND voucherDate <= '" + _MToDate + "' AND VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "' group by vouchertype " +
            " union Select a.PurchaseID as ID, a.VoucherType, a.vouchernumber, a.Voucherdate,'' as vouchersubtype, a.AmountNet,b.AccName,a.createddate,a.createdtime,a.modifieddate from voucherpurchase a inner join masteraccount b on a.AccountID = b.AccountID where (a.createddate >= '" + _MFromDate + "' AND a.createddate <= '" + _MToDate + "' ) OR (a.modifieddate >= '" + _MFromDate + "' AND a.modifieddate <= '" + _MToDate + "') " +
            " union Select a.CRDBID as ID, a.VoucherType, a.vouchernumber, a.Voucherdate,'' as vouchersubtype, a.AmountNet,b.AccName,a.createddate,a.createdtime,a.modifieddate from vouchercreditdebitnote a inner join masteraccount b on a.AccountID = b.AccountID  where (a.createddate >= '" + _MFromDate + "' AND a.createddate <= '" + _MToDate + "' ) OR (a.modifieddate >= '" + _MFromDate + "' AND a.modifieddate <= '" + _MToDate + "') " +
            " union Select a.CBID as ID, a.VoucherType, a.vouchernumber, a.Voucherdate,'' as vouchersubtype, a.AmountNet,b.AccName,a.createddate,a.createdtime,a.modifieddate from vouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where (a.createddate >= '" + _MFromDate + "' AND a.createddate <= '" + _MToDate + "' ) OR (a.modifieddate >= '" + _MFromDate + "' AND a.modifieddate <= '" + _MToDate + "') " +
            " union Select a.CBID as ID, a.VoucherType, a.vouchernumber, a.Voucherdate,'' as vouchersubtype, a.AmountNet,b.AccName,a.createddate,a.createdtime,a.modifieddate from vouchercashbankpayment a inner join masteraccount b on a.AccountID = b.AccountID  where (a.createddate >= '" + _MFromDate + "' AND a.createddate <= '" + _MToDate + "' ) OR (a.modifieddate >= '" + _MFromDate + "' AND a.modifieddate <= '" + _MToDate + "') " +
            " union Select a.CBID as ID, a.VoucherType, a.vouchernumber, a.Voucherdate,'' as vouchersubtype, a.AmountNet,b.AccName,a.createddate,a.createdtime,a.modifieddate from vouchercashbankExpenses  a inner join masteraccount b on a.AccountID = b.AccountID where (a.createddate >= '" + _MFromDate + "' AND a.createddate <= '" + _MToDate + "' ) OR (a.modifieddate >= '" + _MFromDate + "' AND a.modifieddate <= '" + _MToDate + "') ";
            //                " union Select a.ChequeReturnID as ID, a.VoucherType, a.vouchernumber, a.Voucherdate,'' as vouchersubtype, a.AmountNet,b.AccName from VoucherChequeReturn a inner join masteraccount b on a.AccountID = b.AccountID   where (createddate >= '" + _MFromDate + "' AND createddate <= '" + _MToDate + "' ) OR (modifieddate >= '" + _MFromDate + "' AND modifieddate <= '" + _MToDate + "') " +
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public void UpdateDetailSaleForRateInCounterSale(string detailID, double mrate, string voucherType, int voucherNumber)
        {
          //  bool retValue = false;
            string strSql = "Update Detailsale set salerate = "+ mrate +", voucherType = '"+ voucherType +"', vouchernumber = "+ voucherNumber +"  where detailSaleID = '"+ detailID +"'";
            DBInterface.ExecuteQuery(strSql);
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //    retValue = true;
            //else
            //    retValue = false;
        }

        //public DataRow GetPutInBlackList(string selectedID)
        //{
        //    DataRow dr = null;
        //    string strSql = "select putinblacklist from masterpatient where PatientID = '{0}'";
        //    strSql = string.Format(strSql, selectedID);
        //    dr = DBInterface.SelectFirstRow(strSql);
        //    return dr;
        //}

        //public DataRow GetPatientTelephone(string selectedID)
        //{
        //    DataRow dr = null;
        //    string strSql = "select TelephoneNumber from masterpatient where PatientID = '{0}'";
        //    strSql = string.Format(strSql, selectedID);
        //    dr = DBInterface.SelectFirstRow(strSql);
        //    return dr;
        //}

        public DataRow GetAccountIDForShortList(int ProductID)
        {
            string strSql = "Select *  from masterproduct where ProductID = '" + ProductID + "'";
            return DBInterface.SelectFirstRow(strSql);
        }

        public void UpdateDoctor(string address, string docID)
        {
            string strSql = "Update masterdoctor set DocAddress = '" + address + "' where DocID = '" + docID + "'";

            DBInterface.ExecuteQuery(strSql);  
        }

        //public void UpdatePatient(string mobilenumber, string telephone, string patientID, string docID)
        //{
        //    string strSql = "Update masterpatient set  MobileNumberForSMS = '" + mobilenumber + "', TelephoneNumber = '" + telephone + "' , DoctorID = '" + docID + "' where PatientID = '" + patientID + "'";

        //    DBInterface.ExecuteQuery(strSql);
        //}

        public DataTable GetNegetiveStockRowsFromtblStock(string crdbVouDate)
        {
            DataTable dt = new DataTable();
            string strSql = "Select a.stockID,a.ProductID,a.BatchNumber,a.purchaseRate,a.MRP,a.SaleRate,a.Expiry,a.ExpiryDate,a.TradeRate,a.ProductVATPercent, sum(a.closingstock) as closingstock, b.voucherDate, c.ProdLoosePack from tblstock a inner join detailsale b on a.stockID = b.stockID inner join masterproduct c on a.ProductID = c.ProductID where closingstock < 0 AND b.Voucherdate = '" + crdbVouDate +"' group by a.stockID";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public void CreateStockInVoucher(string stockINId, int stockINVouNo, DataTable dt)
        {
            

        }
        public DataRow GetDetailsByPatientID(string PatientID)  // [ansuman]
        {
           //DataRow dr;
            string strsql = "select sum(AmountBalance) as PendingAmount from vouchersale where PatientID = '{0}'";
            strsql = string.Format(strsql, PatientID);
            return DBInterface.SelectFirstRow(strsql);
        }
    }
}
