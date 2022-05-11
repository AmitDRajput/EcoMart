using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    class DBChequeReturn
    {
        public DBChequeReturn()
        {
        }

        public DataTable GetOverviewData(string DbntType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ChequeReturnID, a.CBID,a.ChequeReturnVoucherSeries as VoucherSeries,a.ChequeReturnVoucherType as VoucherType,a.ChequeReturnVoucherNumber as VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,a.ChequeNumber,a.ChequeDate,a.ChequeDepositedBankID,a.CustomerBankID,a.CustomerBranchID,a.BankSlipNumber,a.BankSlipDate,b.AccName,b.AccAddress1,b.AccAddress2 from voucherchequereturn a inner join masteraccount b on a.AccountID = b.AccountID  where  a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' && a.VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by a.voucherdate desc ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }


        public DataTable GetOverviewData(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.chequereturnVoucherType,chequereturnVouchernumber,chequereturnvoucherdate, a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from voucherchequereturn a, masteraccount b " +
                            "where a.AccountId = b.AccountId && a.VoucherDate >= '"+ fromDate +"' && a.VoucherDate <= '"+ toDate +"'  order by a.vouchernumber ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetOverviewDataForReport(string vouType, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from vouchercashbankreceipt a, masteraccount b " +
                            "where a.AccountId = b.AccountId and a.VoucherType = " + "'" + vouType + "' && a.VoucherDate >= '" + fromDate + "'  && a.VoucherDate <= '" + toDate + "'  order by a.vouchernumber ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        //public DataRow ReadDetailsByID(string Id)
        //{
        //    DataRow dRow = null;
        //    if (Id != "")
        //    {
        //        string strSql = "Select a.ChequeReturnID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,a.ChequeNumber,a.ChequeDate,a.ChequeDepositedBankID,a.CustomerBankID,a.CustomerBranchID,a.BankSlipNumber,a.BankSlipDate,b.AccName,b.AccAddress1,b.AccAddress2 from voucherchequereturn a inner join masteraccount b on a.AccountID = b.AccountID where ChequeReturnID='{0}' ";
        //        strSql = string.Format(strSql, Id);
        //        dRow = DBInterface.SelectFirstRow(strSql);
        //    }
        //    return dRow;
        //}
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select distinct a.CBID,a.VoucherSeries,a.VoucherType,a.IfChequereturn,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,a.ChequeNumber,a.ChequeDate,a.ChequeDepositedBankID,a.CustomerBankID,a.CustomerBranchID,a.BankSlipNumber,a.BankSlipDate,b.AccName,b.AccAddress1,b.AccAddress2,c.ChequeReturnID,d.TransactionDate from vouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID left outer join voucherChequereturn c on  a.CBID = c.CBID inner join tbltrnac d on a.CBID = d.VoucherID where a.CBID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow ReadDetailsByIDInVoucherChequeReturn(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.ChequeReturnID, a.CBID,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,a.ChequeNumber,a.ChequeDate,a.ChequeDepositedBankID,a.CustomerBankID,a.CustomerBranchID,a.BankSlipNumber,a.BankSlipDate,b.AccName,b.AccAddress1,b.AccAddress2 from voucherchequereturn a inner join masteraccount b on a.AccountID = b.AccountID where ChequeReturnID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow ReadDetailsByVouNumber(int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where VoucherNumber= " + vouno + " && VoucherType = '" + FixAccounts.VoucherTypeForBankReceipt + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataTable GetSaleDetailsByID(string acId)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ID,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,VoucherSubType,AmountNet,AmountClear,AmountBalance,"
                + "AccountID,ID as MasterID,null as MasterSaleID  from vouchersale where  AccountId =  " + "'" + acId + "'" + " &&  VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' && AmountBalance > 0 order by voucherNumber ";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetSaleDetailsByIDforModify(string acId, string id)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select ID ,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,VoucherSubType,AmountNet,Amountclear,AmountBalance,"
                + "AccountID, null as MasterID, null as MasterSaleID  from vouchersale where  AccountId =  " + "'" + acId + "'" + " &&  VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' && Amountclear = 0 "
                + "union Select a.ID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.VoucherSubType,a.AmountNet,a.Amountclear,AmountBalance,"
                + "a.AccountID,b.MasterID,b.MasterSaleID  from vouchersale a  inner join detailcashbankreceipt b on a.ID = b.MastersaleID and b.MasterID = '" + id + "'  order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetSaleDetailsByBKRID(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID as ID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.MasterId ,a.FromDate,a.ToDate,b.AccountID" +
                            " from detailcashbankreceipt a inner join vouchercashbankreceipt b  on a.MasterId = b.CBId where  a.MasterID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }


        public int AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount,string bkrID,string bkrDate, int bkrno, string bkrseries, string createdby, string createddate, string createdtime)
        {
         //   bool bRetValue = false;
            string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, bankaccount, bank, branch, chqno, chqdate, onaccountamount,bkrID,bkrDate,bkrno,bkrseries, createdby, createddate, createdtime);
            bool ii = (DBInterface.ExecuteQuery(strSql) > 0);
            strSql = "select last_insert_ID()";
            int iid = DBInterface.ExecuteScalar(strSql);
            return iid;
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}
            //return bRetValue;
        }
        public bool UpdateReturnDate(string Id, string VouDate)
        {
            bool bRetValue = false;
            string strSql = "Update voucherchequereturn set ChequeReturnVoucherDate = '"+ VouDate +"' where ChequeReturnID = '"+ Id +"'"; 

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "Update tbltrnac set transactiondate = '" + VouDate + "' where voucherID = '" + Id + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddDetailsParticulars(int Id, string detailID, string SaleId, string BSeries, string BType, int BNumber, string BDate,
              string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, string DetailID, int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetails(Id, detailID, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                BClearedAmount, BDiscountAmount, DetailID, serialNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
      
        public bool UpdateDetailsSCCBill(string Id, string saleid, string BSeries, string BType, int BNumber, string BDate,
              string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount)
        {
            bool bRetValue = false;
            double BBalanceAmt = BBalanceAmount - BClearedAmount;
            string strSql = "Update vouchersale set AmountClear =  AmountClear + " + BClearedAmount + ",  AmountBalance = " +
               "AmountNet - AmountClear where  ID = '" + saleid + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, bankaccount, bank, branch, chqno, chqdate, onaccountamount, modifiedby, modifieddate, modifiedtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool RevertPreviousSaleBalance(string nSaleID, double nClearedAmount)
        {
            bool bRetValue = false;
            string strSql = "Update vouchersale set AmountClear =  AmountClear - " + nClearedAmount + ", AmountBalance = " +
                         "AmountNet - AmountClear where ID = " + "'" + nSaleID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }
        public bool UpdateBankReceiptVoucherForChequeReturn(string BKRID)
        {
            bool bRetValue = false;
            string strSql = "Update vouchercashbankreceipt set IfChequeReturn = 'Y'  where CBID = " + "'" + BKRID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;

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

        public bool DeletePreviosRowsByID(string Id)
        {
            bool bRetValue = false;
            string strSql = "Delete from detailcashbankreceipt where MasterID = " + "'" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool IsNameUnique(string Name, string Id)
        {
            string strSql = GetDataForUnique(Name, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool CheckStock()
        {
            return true;

        }

       
        #region Query Building Functions

        private string GetDataForUnique(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select CompId from MasterCompany where CompName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND CompId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetInsertQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount,string bkrID,string bkrDate, int bkrno, string bkrseries, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherchequereturn";
            objQuery.AddToQuery("ChequeReturnID", Id);
            objQuery.AddToQuery("ChequeReturnVoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("ChequeReturnVoucherNumber", VouNo);
            objQuery.AddToQuery("ChequeReturnVoucherType", VouType);
            objQuery.AddToQuery("ChequeReturnVoucherDate", VouDate);
            objQuery.AddToQuery("ChequeReturnCharges", 0);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", FixAccounts.VoucherTypeForBankReceipt);
            objQuery.AddToQuery("CBID", bkrID);
            objQuery.AddToQuery("VoucherNumber", bkrno);
            objQuery.AddToQuery("VoucherDate", bkrDate);
            objQuery.AddToQuery("VoucherSeries", bkrseries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
            objQuery.AddToQuery("CustomerBankID", bank);
            objQuery.AddToQuery("CustomerBranchID", branch);
            objQuery.AddToQuery("ChequeNumber", chqno);
            objQuery.AddToQuery("ChequeDate", chqdate);
            objQuery.AddToQuery("OnAccountAmount", onaccountamount);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryDetails(int Id, string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
              string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, string DetailID, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailchequereturn";
            //objQuery.AddToQuery("DetailChequereturnID", detailID);
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("MasterSaleID", SaleId);
            objQuery.AddToQuery("BillSeries", RSeries);
            objQuery.AddToQuery("BillType", RType);
            objQuery.AddToQuery("BillNumber", RNumber);
            objQuery.AddToQuery("BillDate", RDate);
            objQuery.AddToQuery("BillSubType", RSubType);
            objQuery.AddToQuery("BillAmount", RBillAmount);
            objQuery.AddToQuery("ClearAmount", RClearedAmount);
            objQuery.AddToQuery("BalanceAmount", RBalanceAmount);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            return objQuery.InsertQuery();
        }
        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, double onaccountamount, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankreceipt";
            objQuery.AddToQuery("CBID", Id, true);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
            objQuery.AddToQuery("CustomerBankID", bank);
            objQuery.AddToQuery("CustomerBranchID", branch);
            objQuery.AddToQuery("ChequeNumber", chqno);
            objQuery.AddToQuery("ChequeDate", chqdate);
            objQuery.AddToQuery("OnAccountAmount", onaccountamount);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankreceipt";
            objQuery.AddToQuery("CBID", Id, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }
        #endregion private methods

      
    }
}
