﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
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
                            "where VoucherSubType = '" + vouSubType + "' order by voucherdate desc , vouchernumber desc";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow GetSaleIDforClone(int vouno, string voutype)
        {
            DataRow drow;
            string strSql = "Select distinct ID,VoucherType,VoucherNumber from vouchersale " +
                            "where VoucherType = " + "'" + voutype + "' && VoucherNumber = " + vouno;
            drow = DBInterface.SelectFirstRow(strSql);
            return drow;
        }
        public DataTable GetOverviewDataForLastSale(string accID, string productID)
        {
            DataTable dt = null;
            {
                string strSql = "select a.DetailSaleID,a.MasterSaleID,a.ProductID,a.StockID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.Expiry,a.ExpiryDate,a.Quantity,a.Vatper,a.VATAmount,a.Amount,b.ID,b.PatientShortName,b.PatientShortAddress, b.VoucherType,b.AccountID, " +
                                "b.VoucherNumber,b.VoucherDate,c.ProductID,c.ProdName,c.ProdLoosePack,c.ProdPack from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID inner join masterproduct c on a.ProductId = c.ProductID where  b.AccountID = '" + accID + "' && a.ProductID = '" + productID + "' order by b.VoucherNumber DESC";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable GetOverviewDataForPartywiseBillsForStatements(string partyid, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherType,a.VoucherSubType, a.VoucherDate ,a.AmountNet , a.VAT5Per,a.VAT12point5Per " +
                 "from vouchersale a inner join masteraccount c on a.Accountid = c.AccountID where a.AccountID = '" + partyid + "' && a.voucherdate >= '" + fromDate + "' &&  a.voucherdate <= '" + toDate + "' && a.StatementNumber = 0  && AmountClear = 0 order by VoucherType, VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForHospitalStatement(string inwardID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherType,a.VoucherSubType, a.VoucherDate ,a.AmountNet , a.VAT5Per,a.VAT12point5Per " +
                 "from vouchersale a  where a.AccountID = '" + inwardID + "' && a.StatementNumber = 0  && a.AmountClear = 0 order by a.VoucherType, a.VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }


        public DataTable GetOverviewDataForPartywiseStatementsView(int statementNumber, string voucherSeries)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherType,a.VoucherSubType,a.VoucherDate ,a.AmountNet , a.VAT5Per,a.VAT12point5Per " +
                 "from vouchersale a  where a.statementnumber = " + statementNumber + " && voucherseries = '" + voucherSeries + "' order by VoucherType, VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetVoucherSaleDataData(string voudate)
        {
            DataTable dt = null;
            {
                string strSql = "select a.DetailSaleID,a.MasterSaleID,a.ProductID,a.StockID,a.BatchNumber,a.MRP,a.PurchaseRate,a.SaleRate, " +
                                "a.TradeRate,a.MySpecialDiscountAmount,a.Expiry,a.ExpiryDate,a.Quantity,a.Vatper,a.VATAmount,a.Amount,b.ID,b.PatientShortName,b.PatientShortAddress,b.VoucherType, " +
                                "b.VoucherNumber,b.VoucherDate,c.ProductID,c.ProdName,c.ProdLoosePack,c.ProdPack from detailsale a  inner join vouchersale b " +
                                "on A.MasterSaleID = B.ID inner join masterproduct c on a.ProductId = c.ProductID where b.VoucherDate = '" + voudate + "' && b.VoucherType =  '" + FixAccounts.VoucherTypeForVoucherSale + "' order by b.VoucherNumber";

                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataTable GetPreviousSale(string accountid)
        {
            DataTable dt = null;
            string strSql = "Select voucherdate, sum(AmountNet) as AmountNet from vouchersale  where (AccountID = '" + accountid + "' || PatientID = '" + accountid + "') group by  substring(voucherDate,4,2)";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataRow ReadDetailsByVouNumber(string voutype, string subtype, int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT12Point5Per,a.AmountVAT5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,b.AccountID,b.AccTokenNumber,a.OrderNumber,a.OrderDate from vouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.VoucherNumber ={0} && a.VoucherType = '{1}' && a.VoucherSubType = '{2}'";
                strSql = string.Format(strSql, vouno, voutype, subtype);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByVouNumberCounterSale(string voutype, string subtype, int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress, a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT12Point5Per,a.AmountVAT5Per," +
                "a.RoundingAmount,a.DiscountAmountCB,a.OrderNumber,a.OrderDate from vouchersale  a  where  a.VoucherNumber ={0} && a.VoucherType = '{1}' && a.VoucherSubType = '{2}'";
                strSql = string.Format(strSql, vouno, voutype, subtype);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByID(string Id, string subtype)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.ID, a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate, " +
                "a.VoucherSubType,a.AccountID,a.AmountNet,a.AmountClear,a.AmountBalance,a.AmountGross, " +
                "a.CashDiscountPercent,a.AmountCashDiscount,a.AddOnFreight,a.AmountCreditNote,a.AmountDebitNote, " +
                "a.OctroiPercentage,a.AmountOctroi,a.Narration,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT5Per,a.AmountVAT12point5Per,a.ScanPrescriptionFileName, " +
                "a.RoundingAmount,a.DiscountAmountCB,a.ProfitInRupees,a.ProfitPercentBySaleRate,a.ProfitPercentByPurchaseRate,a.AmountPMTDiscount,a.AmountItemDiscount,a.DoctorShortName,a.DoctorAddress,a.Telephone, b.AccountID,b.AccTokenNumber,a.OrderNumber, " +
                "a.OrderDate,a.MySpecialDiscountPercent,a.MySpecialDiscountAmount,a.MySpecialDiscountAmount12point5,a.MySpecialDiscountAmount5,a.AmountCashDiscount5,a.AmountCashDiscount12point5 from vouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.ID='{0}'  && a.VoucherSubType = '{1}' ";
                strSql = string.Format(strSql, Id, subtype);
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
                "a.OctroiPercentage,a.AmountOctroi,a.Narration,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT5Per,a.AmountVAT12point5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,a.ProfitInRupees,a.ProfitPercentBySaleRate,a.ProfitPercentByPurchaseRate,a.AmountPMTDiscount,a.AmountItemDiscount, b.AccountID,b.AccTokenNumber,a.OrderNumber,a.OrderDate from changedvouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.ChangedID='{0}'  && a.VoucherSubType = '{1}' ";
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
                "a.OctroiPercentage,a.AmountOctroi,a.Narration,a.StatementNumber,a.DoctorID,a.DoctorShortName,a.DoctorAddress,a.PatientID, " +
                "a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2,a.PatientShortName,a.PatientShortAddress,a.AmountForZeroVAT,a.IPDOPDCode,a.VAT5Per,a.VAT12Point5Per,a.AmountVAT5Per,a.AmountVAT12point5Per, " +
                "a.RoundingAmount,a.DiscountAmountCB,a.ProfitInRupees,a.ProfitPercentBySaleRate,a.ProfitPercentByPurchaseRate,a.AmountPMTDiscount,a.AmountItemDiscount, b.AccountID,b.AccTokenNumber,a.OrderNumber,a.OrderDate from deletedvouchersale  a  left outer join masteraccount b on a.AccountID = b.AccountID  where  a.ID='{0}'  && a.VoucherSubType = '{1}' ";
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

        public DataRow CheckProductInShortList(string Productid)
        {
            string strSql = string.Format("Select *  from tbldailyshortlist where ProductID = '{0}' &&  OrderNumber =  0", Productid);
            return DBInterface.SelectFirstRow(strSql);
        }

        public bool AddToShortList(string Productid, string Voudate, string shortlistid, double purchaserate)
        {
            string sql = GetInsertQueryforShortList(Productid, Voudate, shortlistid, purchaserate);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(sql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetInsertQueryforShortList(string Productid, string Voudate, string shortlistid, double purchaserate)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbldailyshortlist";
            objQuery.AddToQuery("DSLID", shortlistid);
            objQuery.AddToQuery("ProductId", Productid);
            objQuery.AddToQuery("ShortListDate", Voudate);
            objQuery.AddToQuery("PurchaseRate", purchaserate);
            objQuery.AddToQuery("IFSave", "N");
            objQuery.AddToQuery("MasterID", " ");
            objQuery.AddToQuery("OrderNumber", 0);
            objQuery.AddToQuery("OrderDate", " ");
            objQuery.AddToQuery("OrderQuantity", 0);
            objQuery.AddToQuery("AccountId", " ");
            objQuery.AddToQuery("ShortListTime", " ");
            return objQuery.InsertQuery();
        }

        public DataTable ReadProductDetailsByIDDetailSale(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdClosingStock/a.ProdLoosePack as ProdClosingStockPack,a.ProdVATPercent,a.ProdCompShortName,a.ProdLastSaleStockID as LastStockID,a.ProdScheduleDrugCode,b.IfProductDiscount as ProdIfSaleDisc,b.ProductID, " +
                "b.StockID,b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.Quantity as OldQuantity,b.VATAmount,b.Amount,b.PMTDiscount,b.PMTAmount,b.ItemDiscountPer,b.ItemDiscountAmount,b.ProfitPercentBySaleRate,b.ProfitInRupees,b.ProfitPercentByPurchaseRate,b.CashDiscountAmount,b.MySpecialDiscountAmount,c.ShelfID,c.ShelfCode," +
                "d.ClosingStock from masterproduct A inner join  detailsale B  on A.ProductId = B.ProductID  left outer join mastershelf C on A.ProdShelfID = C.ShelfID   left outer join tblstock D on B.stockID = d.stockID where B.MasterSaleID = '{0}' order by b.SerialNumber";
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
                string strsql = "select a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdVATPercent,a.ProdCompShortName,a.ProdLastSaleStockID as LastStockID,b.IfProductDiscount as ProdIfSaleDisc,b.ProductID, " +
                "b.StockID,b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.Quantity as OldQuantity,b.VATAmount,b.Amount,b.PMTDiscount,b.PMTAmount,b.ItemDiscountPer,b.ItemDiscountAmount,b.ChangedMasterID,b.ProfitPercentBySaleRate,b.ProfitInRupees,b.ProfitPercentByPurchaseRate,c.ShelfID,c.ShelfCode," +
                "d.ClosingStock from masterproduct A inner join  changeddetailsale B  on A.ProductId = B.ProductID  left outer join mastershelf C on A.ProdShelfID = C.ShelfID   left outer join tblstock D on B.stockID = d.stockID where B.ChangedMasterID = '{0}' order by b.SerialNumber";
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
                "d.ClosingStock from masterproduct A inner join  deleteddetailsale B  on A.ProductId = B.ProductID  left outer join mastershelf C on A.ProdShelfID = C.ShelfID   left outer join tblstock D on B.stockID = d.stockID where B.MasterSaleID = '{0}' order by b.SerialNumber";
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
                "d.ClosingStock from masterproduct A inner join  detailsale B  on A.ProductId = B.ProductID  left outer join mastershelf C on A.ProdShelfID = C.ShelfID   left outer join tblstock D on B.stockID = d.stockID where B.MasterSaleID = '{0}' order by b.SerialNumber";
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
            string ed = DateTime.Now.Date.ToString("yyyyMMdd").Substring(0, 8);
            string strsql = "select * from masterscheme where ProductID = '{0}' &&  ClosingDate >= '" + ed + "' && (IFSchemeClosed is null || IfSchemeClosed != 'N')";
            strsql = string.Format(strsql, mprod);
            dr = DBInterface.SelectFirstRow(strsql);

            return dr;
        }
        #endregion

        #region write Data

        public bool AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12,
            double rnd, string docId, string docNameAddress, string docAddress, double addon, string vousubtype,
            double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber,
            double cramount, double dbamount, string patientname, string patientaddress1, string patientaddress2,
            string patientshortname, string patientShortAddress, double amountforzerovat, string operatorid, string patientid, double AmountVat12,
            double AmountVat5, string IPDOPD, string OrderNumber, string OrderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate,
            double totalProfitPercentBySaleRate, double pmtTotalDiscount, double itemDiscount, string prescriptionFileName, string telephone,
            double mySpecialDiscountAmount, double mySpecialDiscountPer, double MyTotalSpecialDiscountPer12point5,
            double MyTotalSpecialDiscountPer5, double TotalDiscount12point5, double TotalDiscount5, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuerySale(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, docId, docNameAddress, docAddress, addon, vousubtype,
                amtbal, amtclear, octper, octamt, countersalenum, statementnumber, cramount, dbamount, patientname, patientaddress1,
                patientaddress2, patientshortname, patientShortAddress, amountforzerovat, operatorid, patientid, AmountVat12, AmountVat5, IPDOPD, OrderNumber,
                OrderDate, totalProfitInRupees, totalProfitPercentbyPurchaseRate, totalProfitPercentBySaleRate, pmtTotalDiscount, itemDiscount,
                prescriptionFileName, telephone, mySpecialDiscountAmount, mySpecialDiscountPer, MyTotalSpecialDiscountPer12point5, MyTotalSpecialDiscountPer5, TotalDiscount12point5, TotalDiscount5, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool SaveNewDoctor(string DocID, string doctorNameAddress, string doctorAddress, string createdby, string createddate, string createdtime)
        {
            bool retValue = false;

            string strSql = GetInsertQueryNewDoctor(DocID, doctorNameAddress, doctorAddress, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            return retValue;

        }
        public void SaveNewPatient(string PatientID, string Name, string PatientAddress1, string PatientAddress2, string ShortName, string Telephone, string DocID, string doctorNameAddress, string doctorAddress, string createdby, string createddate, string createdtime)
        {

            string strSql = GetInsertQueryNewPatient(PatientID, Name, PatientAddress1, PatientAddress2, ShortName, Telephone, DocID, doctorNameAddress, doctorAddress, createdby, createddate, createdtime);
            DBInterface.ExecuteQuery(strSql);

        }


        public bool AddDetailsSpecialSale(string Id, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12,
            double rnd, string docId, string docshortname, string docAddress, double addon, string vousubtype,
            double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber,
            double cramount, double dbamount, string patientname, string patientaddress1, string patientaddress2,
            string patientshortname, string patientShortAddress, double amountforzerovat, string operatorid, string patientid, double AmountVat12, double AmountVat5, string IPDOPD, string OrderNumber, string OrderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, double itemDiscount, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuerySaleSpecialSale(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, docId, docshortname, docAddress, addon, vousubtype,
                amtbal, amtclear, octper, octamt, countersalenum, statementnumber, cramount, dbamount, patientname, patientaddress1,
                patientaddress2, patientshortname, patientShortAddress, amountforzerovat, operatorid, patientid, AmountVat12, AmountVat5, IPDOPD, OrderNumber, OrderDate, totalProfitInRupees, totalProfitPercentbyPurchaseRate, totalProfitPercentBySaleRate, pmtTotalDiscount, itemDiscount, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }


        public bool AddDetailsProducts(string Id, string ProductId, string Batchno, int quantity, int SchemeQuantity,
              double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount, string VouType, int VouNo, string VouDate)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryProducts(Id, ProductId, Batchno, quantity, SchemeQuantity, PurchaseRate, MRP, SaleRate, Expiry, ExpiryDate, reasoncode, VatPer, Amount, VouType, VouNo, VouDate);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsProductsSS(string Id, string ProductId, string Batchno, int quantity,
                double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
                string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid,
                int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate,
                double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt, double cashDiscountAmount, double mySpecialDiscountamount)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryProductsSS(Id, ProductId, Batchno, quantity, PurchaseRate, MRP,
                      SaleRate, TradeRate, Expiry, VatPer, Amount, ExpiryDate, accId, CompId, VatAmt, ifproddisc,
                      stockid, mydetailsaleid, serialNumber, ProfitInRupees, ProfitByPurchaseRate, ProfitBySaleRate,
                      pmtDiscountPer, pmtDiscountAmt, itemDiscountPer, itemDiscountAmt, cashDiscountAmount, mySpecialDiscountamount);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsProductsSSSpecialSale(string Id, string ProductId, string Batchno, int quantity,
               double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
               string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryProductsSSSpecialSale(Id, ProductId, Batchno, quantity,
                PurchaseRate, MRP, SaleRate, TradeRate, Expiry, VatPer, Amount,
                 ExpiryDate, accId, CompId, VatAmt, ifproddisc, stockid, mydetailsaleid, serialNumber, ProfitInRupees, ProfitByPurchaseRate, ProfitBySaleRate, pmtDiscountPer, pmtDiscountAmt, itemDiscountPer, itemDiscountAmt);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDeletedDetailsProductsSS(string Id, string ProductId, string Batchno, int quantity,
              double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
              string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDeletedProductsSS(Id, ProductId, Batchno, quantity,
                PurchaseRate, MRP, SaleRate, TradeRate, Expiry, VatPer, Amount,
                 ExpiryDate, accId, CompId, VatAmt, ifproddisc, stockid, mydetailsaleid, serialNumber, ProfitInRupees, ProfitByPurchaseRate, ProfitBySaleRate, pmtDiscountPer, pmtDiscountAmt, itemDiscountPer, itemDiscountAmt);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddChangedDetailsProductsSS(string Id, string ChangedID, string ProductId, string Batchno, int quantity,
             double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
             string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryChangedProductsSS(Id, ChangedID, ProductId, Batchno, quantity,
                PurchaseRate, MRP, SaleRate, TradeRate, Expiry, VatPer, Amount,
                 ExpiryDate, accId, CompId, VatAmt, ifproddisc, stockid, mydetailsaleid, serialNumber, ProfitInRupees, ProfitByPurchaseRate, ProfitBySaleRate, pmtDiscountPer, pmtDiscountAmt, itemDiscountPer, itemDiscountAmt);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12,
             double rnd, double balamt, string docId, string docNameAddress, string docAddress, double addon, string vousubtype,
             double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber, double cramount,
            double dbamount, string patientname, string patientAddress1, string patientAddress2, string ShortName, string patientShortAddress, double amountforzerovat,
            string operatorid, double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees,
            double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount,
            double itemTotalDiscount, string prescriptionFileName, string telephone, double mySpecialDiscountAmount,
            double MySpecialDiscountPercent, double MyTotalSpecialDiscountPer12point5, double MyTotalSpecialDiscountPer5,
            double TotalDiscount12point5, double TotalDiscount5, string modifiedby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuerySale(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, balamt, docId, docNameAddress, docAddress, addon, vousubtype,
                amtbal, amtclear, octper, octamt, countersalenum, statementnumber, cramount, dbamount, patientname,
                patientAddress1, patientAddress2, ShortName, patientShortAddress, amountforzerovat, operatorid, AmountVat12,
                AmountVat5, IPDOPD, orderNumber, orderDate, totalProfitInRupees, totalProfitPercentbyPurchaseRate, totalProfitPercentBySaleRate,
                pmtTotalDiscount, itemTotalDiscount, prescriptionFileName, telephone, mySpecialDiscountAmount, MySpecialDiscountPercent,
                MyTotalSpecialDiscountPer12point5, MyTotalSpecialDiscountPer5, TotalDiscount12point5, TotalDiscount5, modifiedby, modifydate, modifytime);
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

        public bool AddDetailsInDeleteMaster(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12,
             double rnd, double balamt, string docId, string docshortname, string docAddress, double addon, string vousubtype,
             double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber, double cramount,
            double dbamount, string patientname, string patientAddress1, string patientAddress2, string ShortName, string patientShortAddress, double amountforzerovat,
            string operatorid, double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, string telephone, string modifiedby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetAddDetailsInDeleteMaster(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, balamt, docId, docshortname, docAddress, addon, vousubtype,
                amtbal, amtclear, octper, octamt, countersalenum, statementnumber, cramount, dbamount, patientname,
                patientAddress1, patientAddress2, ShortName, patientShortAddress, amountforzerovat, operatorid, AmountVat12, AmountVat5, IPDOPD, orderNumber, orderDate, totalProfitInRupees, totalProfitPercentbyPurchaseRate, totalProfitPercentBySaleRate, pmtTotalDiscount, telephone, modifiedby, modifydate, modifytime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsInChangedMaster(string Id, string ChangedID, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12,
            double rnd, double balamt, string docId, string docshortname, string docAddress, double addon, string vousubtype,
            double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber, double cramount,
           double dbamount, string patientname, string patientAddress1, string patientAddress2, string ShortName, string patientShortAddress, double amountforzerovat,
           string operatorid, double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees, double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, string telephone, string modifiedby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetAddDetailsInChangedMaster(Id, ChangedID, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, balamt, docId, docshortname, docAddress, addon, vousubtype,
                amtbal, amtclear, octper, octamt, countersalenum, statementnumber, cramount, dbamount, patientname,
                patientAddress1, patientAddress2, ShortName, patientShortAddress, amountforzerovat, operatorid, AmountVat12, AmountVat5, IPDOPD, orderNumber, orderDate, totalProfitInRupees, totalProfitPercentbyPurchaseRate, totalProfitPercentBySaleRate, pmtTotalDiscount, telephone, modifiedby, modifydate, modifytime);
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


            string strSql = GetDeleteDebtorSaleQuery(Id);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool DeleteDetailsFromtblTrnac(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteDebtorSaleQueryFromtblTrnac(Id);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
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
        internal void SaveDiscPercentInPatientMaster(string accountID, double discoutPercent)
        {

            string strSql = "Update masterpatient set DiscountOffered = " + discoutPercent + " where PatientID = '" + accountID + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
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
        public bool UpdateVoucherSaleUpdateMasterProduct(string mproductID, int mqty)
        {
            bool returnVal = false;
            string strSql = "Update masterproduct set prodclosingstock = Prodclosingstock +  " + mqty + "  where ProductID = '" + mproductID + "'";
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

        public bool UpdateDetailsForTypeChange(string purchaseID, string VoucherType, int VoucherNumber, double amountBalance, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryForTypeChange(purchaseID, VoucherType, VoucherNumber, amountBalance, modifiedby, modifieddate, modifiedtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public DataRow GetProductNameFromScanCode(string scanCode)
        {
            DataRow dr = null;
            string strsql = string.Format("Select B.ProdName, A.StockID, B.ProdLoosePack from tblStock A Inner join masterproduct B on A.ProductID=B.ProductID where ScanCode='{0}'", scanCode);
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


        private string GetUpdateQueryForTypeChange(string purchaseID, string VoucherType, int VoucherNumber, double amountBalance, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchersale";
            objQuery.AddToQuery("ID", purchaseID, true);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("AccountID", FixAccounts.AccountPendingCashBills);
            objQuery.AddToQuery("AmountClear", 0);
            objQuery.AddToQuery("AmountBalance", amountBalance);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }


        private string GetInsertQuerySale(string Id, string AccountId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd,
            string docId, string docNameAddress, string docAddress, double addon, string vousubtype, double amtbal, double amtclear, double octper, double octamt,
            int countersalenum, int statementnumber, double cramount, double dbamount, string patientname, string patientaddress1,
            string patientaddress2, string patientshortname, string patientShortAddress, double amountforzerovat, string operatorid, string patientid,
            double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees,
            double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, double itemTotalDiscount,
            string prescriptionFileName, string telephone, double mySpecialDiscountAmount, double mySpecialDiscountPer,
            double MyTotalSpecialDiscountPer12point5, double MyTotalSpecialDiscountPer5, double TotalDiscount12point5,
            double TotalDiscount5, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchersale";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("AccountID", AccountId);
            objQuery.AddToQuery("PatientID", patientid);
            objQuery.AddToQuery("Narration", Narration);
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
            objQuery.AddToQuery("DoctorShortName", docNameAddress);
            objQuery.AddToQuery("DoctorAddress", docAddress);
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
            objQuery.AddToQuery("ScanPrescriptionFileName", prescriptionFileName);
            objQuery.AddToQuery("MySpecialDiscountAmount", mySpecialDiscountAmount);
            objQuery.AddToQuery("MySpecialDiscountPercent", mySpecialDiscountPer);
            objQuery.AddToQuery("MySpecialDiscountAmount12point5", MyTotalSpecialDiscountPer12point5);
            objQuery.AddToQuery("MySpecialDiscountAmount5", MyTotalSpecialDiscountPer5);
            objQuery.AddToQuery("AmountCashDiscount5", TotalDiscount5);
            objQuery.AddToQuery("AmountCashDiscount12point5", TotalDiscount12point5);
            //  objQuery.AddToQuery("AmountByPurchaseRate", amountByPurchaseRate);            
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQuerySaleSpecialSale(string Id, string AccountId, string Narration, string VouType, int VouNo,
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
            objQuery.AddToQuery("Narration", Narration);
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

        private string GetInsertQueryProducts(string Id, string ProductId, string Batchno, int quantity, int SchemeQuantity,
              double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount, string VouType, int VouNo, string VouDate)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailsale";
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("ProductID", ProductId);
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

        private string GetInsertQueryProductsSS(string Id, string ProductId, string Batchno, int quantity,
                double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
                string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid,
                string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate,
                double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer,
                double itemDiscountAmt, double cashDiscountAmount, double mySpecialDiscountamount)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailsale";
            objQuery.AddToQuery("MasterSaleID", Id);
            objQuery.AddToQuery("ProductID", ProductId);
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
            objQuery.AddToQuery("CashDiscountAmount", cashDiscountAmount);
            objQuery.AddToQuery("MySpecialDiscountAmount", mySpecialDiscountamount);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryProductsSSSpecialSale(string Id, string ProductId, string Batchno, int quantity,
              double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
              string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            Query objQuery = new Query();
            objQuery.Table = "specialdetailsale";
            objQuery.AddToQuery("MasterSaleID", Id);
            objQuery.AddToQuery("ProductID", ProductId);
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

        private string GetInsertQueryDeletedProductsSS(string Id, string ProductId, string Batchno, int quantity,
                double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
                string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            Query objQuery = new Query();
            objQuery.Table = "Deleteddetailsale";
            objQuery.AddToQuery("MasterSaleID", Id);
            objQuery.AddToQuery("ProductID", ProductId);
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

        private string GetInsertQueryChangedProductsSS(string Id, string ChangedID, string ProductId, string Batchno, int quantity,
              double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, double VatPer, double Amount,
              string ExpiryDate, string accId, string CompId, double VatAmt, string ifproddisc, string stockid, string mydetailsaleid, int serialNumber, double ProfitInRupees, double ProfitByPurchaseRate, double ProfitBySaleRate, double pmtDiscountPer, double pmtDiscountAmt, double itemDiscountPer, double itemDiscountAmt)
        {
            Query objQuery = new Query();
            objQuery.Table = "Changeddetailsale";
            objQuery.AddToQuery("MasterSaleID", Id);
            objQuery.AddToQuery("ChangedMasterID", ChangedID);
            objQuery.AddToQuery("ProductID", ProductId);
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

        private string GetUpdateQuerySale(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd,
            double balamt, string docId, string docNameAddress, string docAddress, double addon, string vousubtype,
            double amtbal, double amtclear, double octper, double octamt, int countersalenum, int statementnumber, double cramount,
            double dbamount, string patientname, string patientaddress1, string patientaddress2, string patientshortname, string patientShortAddress, double amountforzerovat,
            string operatorid, double AmountVat12, double AmountVat5, string IPDOPD, string orderNumber, string orderDate, double totalProfitInRupees,
            double totalProfitPercentbyPurchaseRate, double totalProfitPercentBySaleRate, double pmtTotalDiscount, double itemTotalDiscount,
            string prescriptionFileName, string telephone, double mySpecialDiscountAmount, double MySpecialDiscountPercent, double MyTotalSpecialDiscountPer12point5, double MyTotalSpecialDiscountPer5,
            double TotalDiscount12point5, double TotalDiscount5, string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchersale";
            objQuery.AddToQuery("ID", Id, true);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
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
            objQuery.AddToQuery("DoctorShortName", docNameAddress);
            objQuery.AddToQuery("DoctorAddress", docAddress);
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
            objQuery.AddToQuery("ScanPrescriptionFileName", prescriptionFileName);
            objQuery.AddToQuery("MySpecialDiscountAmount", mySpecialDiscountAmount);
            objQuery.AddToQuery("MySpecialDiscountPercent", MySpecialDiscountPercent);
            objQuery.AddToQuery("MySpecialDiscountAmount12point5", MyTotalSpecialDiscountPer12point5);
            objQuery.AddToQuery("MySpecialDiscountAmount5", MyTotalSpecialDiscountPer5);
            objQuery.AddToQuery("AmountCashDiscount5", TotalDiscount5);
            objQuery.AddToQuery("AmountCashDiscount12point5", TotalDiscount12point5);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }

        private string GetAddDetailsInDeleteMaster(string Id, string CreditorId, string Narration, string VouType, int VouNo,
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
            objQuery.AddToQuery("Narration", Narration);
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

        private string GetAddDetailsInChangedMaster(string Id, string ChangedID, string CreditorId, string Narration, string VouType, int VouNo,
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
            objQuery.AddToQuery("Narration", Narration);
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
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.InsertQuery();

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

        public bool AddVoucherIntblTrnac(string Id, string debitAccount, string creditAccount, string Narration, string VouType, int VouNo,
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



        private string GetInsertQueryTrnacForVoucher(string Id, string debitaccount, string creditaccount, string Narration, string VouType, int VouNo,
          string VouDate, double debitamount, double creditamount, string DetailId, string ShortName, string SaleSubType, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("tblTrnacID", DetailId);
            objQuery.AddToQuery("VoucherID", Id);
            objQuery.AddToQuery("AccountId", debitaccount);
            objQuery.AddToQuery("Debit", debitamount);
            objQuery.AddToQuery("Credit", creditamount);
            objQuery.AddToQuery("AccAccountID", creditaccount);
            objQuery.AddToQuery("TransactionDate", VouDate);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("ReferenceVoucherId", "");
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("ShortName", ShortName);
            objQuery.AddToQuery("VoucherSubType", SaleSubType);
            objQuery.AddToQuery("CreatedUserID", createdby);
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


        private string GetInsertQueryNewPatient(string PatientID, string Name, string PatientAddress1, string PatientAddress2, string shortName, string Telephone, string DocID, string doctorNameAddress, string docAddress, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterpatient";
            objQuery.AddToQuery("PatientID", PatientID);
            objQuery.AddToQuery("PatientName", Name);
            objQuery.AddToQuery("AccCode", "P");
            objQuery.AddToQuery("PatientAddress1", PatientAddress1);
            objQuery.AddToQuery("PatientAddress2", PatientAddress2);
            objQuery.AddToQuery("ShortNameAddress", shortName);
            objQuery.AddToQuery("TelephoneNumber", Telephone);
            objQuery.AddToQuery("DoctorID", DocID);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
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
    }
}
