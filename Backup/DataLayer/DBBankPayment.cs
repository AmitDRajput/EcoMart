using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBBankPayment
    {
        public DBBankPayment()
        {
        }

        public DataTable GetOverviewData(string VouType)
        {
            DataTable dtable = new DataTable();
            try
            {
                string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                                "a.AccountID,a.ChequeNumber, a.ChequeDate,a.ChequeDepositedBankID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankpayment a, masteraccount b " +
                                "where a.AccountId = b.AccountId and a.VoucherType = " + "'" + VouType + "'" + "  order by a.vouchernumber ";

                dtable = DBInterface.SelectDataTable(strSql);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }

            return dtable;
        }

        public DataTable GetOverviewData(string bankID, string VouType,string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            try
            {
                string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                                "a.AccountID,a.ChequeNumber, a.ChequeDate,a.ChequeDepositedBankID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankpayment a, masteraccount b " +
                                "where a.AccountId = b.AccountId && a.VoucherType = '" + VouType + "' &&  a.VoucherDate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "' && ChequeDepositedBankID = '"+ bankID +"' order by a.vouchernumber ";

                dtable = DBInterface.SelectDataTable(strSql);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }

            return dtable;
        }
        public DataTable GetDataForChequePaidButNotCleared(string VouType, string fromDate, string toDate, string bankID)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                          "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankpayment a, masteraccount b " +
                          "where a.AccountId = b.AccountId && a.VoucherType = '" + VouType + "' && a.VoucherDate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "' && ChequeDepositedBankID = '" + bankID + "' && (ClearedDate is null || ClearedDate = '' || ClearedDate <= '" + toDate + "')  order by a.vouchernumber "; 

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForTodaysCheques(string CBType, string CBType2, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            try
            {
                string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                                "a.AccountID,a.ChequeNumber, a.ChequeDate,a.ChequeDepositedBankID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2,c.AccName as BankName from vouchercashbankpayment a inner join masteraccount b on a.AccountID = b.accountID  inner join masteraccount c on a.ChequeDepositedBankID = c.accountID" +
                                " where (a.VoucherType = '" + CBType  + "' || a.VoucherType = '"+ CBType2 +"') && a.ChequeDate >= '" + fromDate + "' && a.ChequeDate <= '" + toDate + "'  order by a.vouchernumber ";

                dtable = DBInterface.SelectDataTable(strSql);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }

            return dtable;
        }



        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            try
            {
                if (Id != "")
                {
                    string strSql = "Select * from vouchercashbankpayment where CBID='{0}' ";
                    strSql = string.Format(strSql, Id);
                    dRow = DBInterface.SelectFirstRow(strSql);
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return dRow;
        }
        public DataRow ReadDetailsByIDForChanged(string Id)
        {
            DataRow dRow = null;
            try
            {
                if (Id != "")
                {
                    string strSql = "Select * from changedvouchercashbankpayment where changedID='{0}' ";
                    strSql = string.Format(strSql, Id);
                    dRow = DBInterface.SelectFirstRow(strSql);
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return dRow;
        }
        public DataRow ReadDetailsByIDForDeleted(string Id)
        {
            DataRow dRow = null;
            try
            {
                if (Id != "")
                {
                    string strSql = "Select * from deletedvouchercashbankpayment where CBID='{0}' ";
                    strSql = string.Format(strSql, Id);
                    dRow = DBInterface.SelectFirstRow(strSql);
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return dRow;
        }
        public DataTable GetPurchaseDetailsByID(string Id)
        {
            DataTable dtable = new DataTable();
            //string strSql = "Select PurchaseID,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,VoucherSubType,PurchaseBillNumber,AmountNet,Amountclear,AmountBalance," +
            //                "AccountID, null as MasterID from voucherpurchase where  AccountId =  '" + Id + "'  && AmountBalance > 0 && VoucherType = '" + FixAccounts.VoucherTypeForCreditPurchase + "' && statementnumber = 0 " +
            //                " union select ID as PurchaseID, VoucherSeries, VoucherType,VoucherNumber,ToDate as VoucherDate,null as VoucherSubtype, null as PurchaseBillNumber,AmountNet,AmountClear,AmountBalance," +
            //                    "AccountID,null as MasterID from voucherstatement where AccountID = '" + Id + "' && AmountBalance > 0 && VoucherType = '" + FixAccounts.VoucherTypeForStatementPurchase + "' order by voucherDate";


            //dtable = DBInterface.SelectDataTable(strSql);

            //return dtable;
            string strSql = "Select PurchaseID,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,VoucherSubType,PurchaseBillNumber,AmountNet,Amountclear,AmountBalance," +
                         "AccountID, null as MasterID from voucherpurchase where  AccountId =  '" + Id + "'  && AmountBalance > 0 && VoucherType = '" + FixAccounts.VoucherTypeForCreditPurchase + "' && statementnumber = 0 ";
                         


            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetStatementDetailsByID(string acId)
        {
            DataTable dtable = new DataTable();
            string strSql = "select a.ID , a.VoucherSeries, a.VoucherType,a.VoucherNumber,a.ToDate as VoucherDate,'' as VoucherSubType,  a.AmountNet,a.AmountClear,a.AmountBalance," +
                          "a.AccountID,null as MasterID,null as MasterSaleID,b.AccName as PatientshortName from voucherstatement a inner join masteraccount b on a.AccountID = b.AccountID where a.AccountID = '" + acId + "'  &&  a.AmountBalance > 0 && a.VoucherType = '" + FixAccounts.VoucherTypeForStatementPurchase + "' order by a.vouchertype ,a.vouchernumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataRow ReadDetailsByVoucherNumber(int vouno, string voucherType)
        {
            DataRow dRow = null;
            try
            {
                if (vouno != 0)
                {
                    string strSql = "Select * from vouchercashbankpayment where VoucherNumber='{0}' && voucherType = '{1}'";
                    strSql = string.Format(strSql, vouno,voucherType);
                    dRow = DBInterface.SelectFirstRow(strSql);
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return dRow;
        }

        public DataTable GetPurchaseDetailsByIDforModify(string acId, string id)
        {
            DataTable dtable = new DataTable();
            try
            {
                //string strSql = "Select PurchaseID,VoucherSeries,VoucherType,VoucherNumber,VoucherSubType,VoucherDate,PurchaseBillNumber,AmountNet,Amountclear,AmountBalance," +
                //           "AccountID, null as MasterID from voucherpurchase where  AccountId =  '" + acId + "'  && AmountBalance > 0 && VoucherType = '" + FixAccounts.VoucherTypeForCreditPurchase + "' && statementnumber = 0 " +
                //           " union select ID as PurchaseID, VoucherSeries, VoucherType,VoucherNumber,'' as VoucherSubType,VoucherDate, null as PurchaseBillNumber,AmountNet,AmountClear,AmountBalance," +
                //               "AccountID,null as MasterID from voucherstatement where AccountID = '" + acId + "' && AmountBalance > 0 && VoucherType = '" + FixAccounts.VoucherTypeForStatementPurchase + "' order by voucherDate";
                ////string strSql = "Select distinct a.purchaseID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.PurchaseBillNumber,a.AmountNet, a.AmountClear, a.AmountBalance ," +
                ////        "a.AccountID,null as MasterID  from voucherpurchase a  left outer join detailcashbankpayment b on a.purchaseID = b.MasterPurchaseID where  a.AccountId =  " + "'" + acId + "' order by VoucherNumber";
                //dtable = DBInterface.SelectDataTable(strSql);
                string strSql = "Select distinct a.purchaseID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.PurchaseBillNumber,a.VoucherSubType,a.AmountNet, a.AmountClear, a.AmountBalance ," +
                     "a.AccountID,null as MasterID  from voucherpurchase a  inner join  detailcashbankpayment b on a.purchaseID = b.MasterPurchaseID && b.MasterID = '" + id + "' order by VoucherType,VoucherNumber";

                dtable = DBInterface.SelectDataTable(strSql);
                return dtable;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return dtable;
        }
        public DataTable GetStatementDetailsByIDforModify(string acId, string id)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.ID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.ToDate as VoucherDate, '' as VoucherSubType,a.AmountNet,a.Amountclear,a.AmountBalance,"
                + "a.AccountID,c.AccName as PatientShortName,b.MasterID,b.MasterSaleID  from voucherstatement a  inner join detailcashbankreceipt b on a.ID = b.MastersaleID inner join masteraccount c on a.AccountID = c.AccountId  &&  b.MasterID = '" + id + "'  order by  VoucherType,VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
      
        public DataTable GetPurchaseDetailsByBKPID(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            try
            {
                //string strSql = "Select a.MasterId , a.MasterPurchaseID as PurchaseID,c.AmountBalance+a.ClearAmount as AmountBalance," +
                //                       "a.ClearAmount as AmountClear,b.AccountID,c.PurchaseID,c.VoucherSeries,c.VoucherType,c.VoucherNumber,c.VoucherDate,c.PurchaseBillNumber,c.AmountNet " +
                //                " from detailcashbankpayment a inner join vouchercashbankpayment b  on a.MasterId = b.CBId inner join voucherpurchase c on a.MasterPurchaseID = c.PurchaseID where  a.MasterID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerailNumber";
              //  string strSql = " Select a.MasterId, a.MasterPurchaseID as PurchaseID, a.BalanceAmount as AmountBalance," +
              //       "a.ClearAmount as AmountClear,b.AccountID , '' as PurchaseID, '' as VoucherSeries,  a.BillType as VoucherType, '1' as VoucherNumber, a.BillDate as voucherDate, '1' as PurchaseBillNumber,a.BillAmount as AmountNet " +
              //" from detailcashbankpayment a inner join vouchercashbankpayment b  on a.MasterId = b.CBId where a.MasterID =  '" + Id + "' &&  b.AccountID = '" + accid + "'  order by a.SerialNumber ";

                string strSql = " Select a.MasterId , a.MasterPurchaseID as PurchaseID, a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,b.AccountID , c.PurchaseBillNumber, BillSeries as VoucherSeries,c.VoucherSubType,  a.BillType as VoucherType,  BillNumber as VoucherNumber, a.BillDate as voucherDate, a.BillAmount as AmountNet " +
                            " from detailcashbankpayment a inner join vouchercashbankpayment b  on a.MasterId = b.CBId  left outer join VoucherPurchase c on a.MasterPurchaseId = c.PurchaseID where a.MasterID =  '" + Id + "' &&  b.AccountID = '" + accid + "'  order by a.SerialNumber ";
                dtable = DBInterface.SelectDataTable(strSql);

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return dtable;
        }
        public DataTable GetPurchaseDetailsByBKPIDForChanged(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            try
            {
                string strSql = " Select a.MasterId,a.changedMasterID as ID, a.MasterPurchaseID as PurchaseID, a.BalanceAmount as AmountBalance," +
                      "a.ClearAmount as AmountClear,b.AccountID ,BillNumber as VoucherNumber,c.PurchaseBillNumber, BillSeries as VoucherSeries,  a.BillType as VoucherType,  a.BillDate as voucherDate, a.BillAmount as AmountNet " +
               " from changeddetailcashbankpayment a inner join changedvouchercashbankpayment b  on a.changedMasterID = b.ChangedId left outer join voucherpurchase c on a.MasterPurchaseId = c.PurchaseID where a.changedMasterID =  '" + Id + "' &&  b.AccountID = '" + accid + "'  order by a.SerialNumber ";

               dtable = DBInterface.SelectDataTable(strSql);

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return dtable;
        }
        public DataTable GetPurchaseDetailsByBKPIDForDeleted(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            try
            {
                string strSql = " Select a.MasterId as Id, a.MasterID, a.MasterPurchaseID as PurchaseID, a.BalanceAmount as AmountBalance," +
                "a.ClearAmount as AmountClear,b.AccountID ,BillNumber as VoucherNumber,c.PurchaseBillNumber, BillSeries as VoucherSeries,  a.BillType as VoucherType,  a.BillDate as voucherDate, a.BillAmount as AmountNet " +
         " from deleteddetailcashbankpayment a inner join deletedvouchercashbankpayment b  on a.MasterID = b.CBId left outer join voucherpurchase c on a.MasterPurchaseId = c.PurchaseID where a.MasterID =  '" + Id + "' &&  b.AccountID = '" + accid + "'  order by a.SerialNumber ";


                dtable = DBInterface.SelectDataTable(strSql);

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return dtable;
        }

        public bool AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            try
            {
                string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo,
                    VouDate, Amt, bankaccount, bank, branch, chqno, chqdate, createdby, createddate, createdtime);

                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return bRetValue;
        }
        public bool AddChangedDetails(string Id, string changedID, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            try
            {
                string strSql = GetInsertQueryChanged(Id, changedID, CreditorId, Narration, VouType, VouNo,
                    VouDate, Amt, bankaccount, bank, branch, chqno, chqdate, createdby, createddate, createdtime);

                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return bRetValue;
        }
        public bool AddDeletedDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            try
            {
                string strSql = GetInsertQueryDeleted(Id, CreditorId, Narration, VouType, VouNo,
                    VouDate, Amt, bankaccount, bank, branch, chqno, chqdate, createdby, createddate, createdtime);

                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return bRetValue;
        }
        public bool AddDetailsParticulars(string Id, string detailId, string SaleId, string BSeries, string BType, int BNumber, string BDate,
              string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, int serialNumber)
        {
            bool bRetValue = false;
            try
            {
                string strSql = GetInsertQueryP(Id, detailId, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                    BClearedAmount, BDiscountAmount, serialNumber);

                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
                return bRetValue;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return bRetValue;
        }
        public bool AddDetailsParticularsChanged(string Id, string changedID, string detailId, string SaleId, string BSeries, string BType, int BNumber, string BDate,
                     string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, int serialNumber)
        {
            bool bRetValue = false;
            try
            {
                string strSql = GetInsertQueryPChanged(Id, changedID, detailId, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                    BClearedAmount, BDiscountAmount, serialNumber);

                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
                return bRetValue;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return bRetValue;
        }
        public bool AddDetailsParticularsDeleted(string Id, string detailId, string SaleId, string BSeries, string BType, int BNumber, string BDate,
             string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, int serialNumber)
        {
            bool bRetValue = false;
            try
            {
                string strSql = GetInsertQueryPDeleted(Id, detailId, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                    BClearedAmount, BDiscountAmount, serialNumber);

                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
                return bRetValue;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return bRetValue;
        }

        public bool UpdateDetailsPurchaseBill(string Id, string purchaseid, string BSeries, string BType, int BNumber, string BDate,
              string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount)
        {
            bool bRetValue = false;
            double BBalanceAmt = BBalanceAmount - BClearedAmount;
            try
            {
                string strSql = "Update voucherpurchase set AmountClear =  AmountClear + " + BClearedAmount + ", AmountBalance = " +
                   "AmountNet - AmountClear where purchaseID = '" + purchaseid + "'";
                DBInterface.ExecuteQuery(strSql);
                bRetValue = true;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return bRetValue;
        }
        public bool UpdateDetailsPurchaseStatement(string Id, string purchaseid, string BSeries, string BType, int BNumber, string BDate,
         string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount)
        {
            double BBalanceAmt = BBalanceAmount - BClearedAmount;
            string strSql = "Update voucherstatement set AmountClear =  AmountClear + " + BClearedAmount + ", AmountBalance = " +
               "AmountNet - AmountClear where ID = '" + purchaseid + "'";
            DBInterface.ExecuteQuery(strSql);
            return true;
        }

        public bool UpdateOpeningBalanceReducePrevious(string accID, double PreopeningCleared)
        {
            bool bRetValue = false;
            double mclearedamt = 0;
            string strSql = "select AccClearedAmount  from masteraccount Where AccountID = '" + accID + "'";
            DataRow dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null && dr["AccClearedAmount"] != DBNull.Value)
                mclearedamt = Convert.ToDouble(dr["AccClearedAmount"].ToString());
            mclearedamt = mclearedamt - PreopeningCleared;
            strSql = "update masteraccount  set AccClearedAmount = " + mclearedamt + " Where AccountID = '" + accID + "'";
            bRetValue = DBInterface.ExecuteQuery(strSql) > 0;
            return bRetValue;
        }
        public bool UpdateOpeningBalanceAddNew(string accID, double Cleared)
        {
            bool bRetValue = false;
            double mclearedamt = 0;
            string strSql = "select AccClearedAmount  from masteraccount Where AccountID = '" + accID + "'";
            DataRow dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null && dr["AccClearedAmount"] != DBNull.Value)
                mclearedamt = Convert.ToDouble(dr["AccClearedAmount"].ToString());
            mclearedamt = mclearedamt + Cleared;
            strSql = "update masteraccount  set AccClearedAmount = " + mclearedamt + " Where AccountID = '" + accID + "'";
            bRetValue = DBInterface.ExecuteQuery(strSql) > 0;
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            try
            {
                string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo,
                    VouDate, Amt, bankaccount, bank, branch, chqno, chqdate, modifiedby, modifieddate, modifiedtime);

                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return bRetValue;
        }


        public bool UpdateDetailsForFifth(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            try
            {
                string strSql = GetUpdateQueryForFifth(Id, CreditorId, Narration, VouType, VouNo,
                    VouDate, Amt, bankaccount, bank, branch, chqno, chqdate, modifiedby, modifieddate, modifiedtime);

                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return bRetValue;
        }

        public bool RevertPreviousPurchaseBalanceBill(string nSaleID, double nClearedAmount)
        {
            bool bRetValue = false;
            string strSql = "Update voucherpurchase set AmountClear =  AmountClear - " + nClearedAmount + ", AmountBalance = " +
                         "AmountNet - AmountClear where PurchaseID = " + "'" + nSaleID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        public bool RevertPreviousPurchaseBalanceStatement(string nSaleID, double nClearedAmount)
        {
            bool bRetValue = false;
            string strSql = "Update voucherstatement set AmountClear =  AmountClear - " + nClearedAmount + ", AmountBalance = " +
                         "AmountNet - AmountClear where ID = " + "'" + nSaleID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }
        public bool DeleteDetails(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQuery(Id);
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return bRetValue;
        }

        public bool DeletePreviosRowsByID(string Id)
        {
            bool bRetValue = false;
            try
            {
                string strSql = "Delete from detailcashbankpayment where MasterID = " + "'" + Id + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return bRetValue;
        }

        public bool IsNameUnique(string Name, string Id)
        {
            string strSql = GetDataForUnique(Name, Id);
            bool bRetValue = false;
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
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
            try
            {
                sQuery.AppendFormat("Select CompId from MasterCompany where CompName='{0}'", Name);
                if (Id != "")
                {
                    sQuery.AppendFormat(" AND CompId not in ('{0}')", Id);
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return sQuery.ToString();
        }
        private string GetInsertQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            try
            {
                objQuery.Table = "vouchercashbankpayment";
                objQuery.AddToQuery("CBID", Id);
                objQuery.AddToQuery("AccountId", CreditorId);
                objQuery.AddToQuery("Narration", Narration);
                objQuery.AddToQuery("VoucherType", VouType);
                objQuery.AddToQuery("VoucherNumber", VouNo);
                objQuery.AddToQuery("VoucherDate", VouDate);
                objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
                objQuery.AddToQuery("AmountNet", Amt);
                objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
                objQuery.AddToQuery("ChequeNumber", chqno);
                objQuery.AddToQuery("ChequeDate", chqdate);
                objQuery.AddToQuery("CreatedUserID", createdby);
                objQuery.AddToQuery("CreatedDate", createddate);
                objQuery.AddToQuery("CreatedTime", createdtime);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryChanged(string Id, string changedID, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            try
            {
                objQuery.Table = "changedvouchercashbankpayment";
                objQuery.AddToQuery("CBID", Id);
                objQuery.AddToQuery("ChangedID", changedID);
                objQuery.AddToQuery("AccountId", CreditorId);
                objQuery.AddToQuery("Narration", Narration);
                objQuery.AddToQuery("VoucherType", VouType);
                objQuery.AddToQuery("VoucherNumber", VouNo);
                objQuery.AddToQuery("VoucherDate", VouDate);
                objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
                objQuery.AddToQuery("AmountNet", Amt);
                objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
                objQuery.AddToQuery("ChequeNumber", chqno);
                objQuery.AddToQuery("ChequeDate", chqdate);
                objQuery.AddToQuery("ModifiedUserID", createdby);
                objQuery.AddToQuery("modifiedDate", createddate);
                objQuery.AddToQuery("ModifiedTime", createdtime);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryDeleted(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            try
            {
                objQuery.Table = "deletedvouchercashbankpayment";
                objQuery.AddToQuery("CBID", Id);
                objQuery.AddToQuery("AccountId", CreditorId);
                objQuery.AddToQuery("Narration", Narration);
                objQuery.AddToQuery("VoucherType", VouType);
                objQuery.AddToQuery("VoucherNumber", VouNo);
                objQuery.AddToQuery("VoucherDate", VouDate);
                objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
                objQuery.AddToQuery("AmountNet", Amt);
                objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
                objQuery.AddToQuery("ChequeNumber", chqno);
                objQuery.AddToQuery("ChequeDate", chqdate);
                objQuery.AddToQuery("ModifiedUserID", createdby);
                objQuery.AddToQuery("modifiedDate", createddate);
                objQuery.AddToQuery("ModifiedTime", createdtime);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryP(string Id, string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
              string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, int serialNumber)
        {
            Query objQuery = new Query();
            try
            {
                objQuery.Table = "detailcashbankpayment";
                objQuery.AddToQuery("DetailCashBankPaymentID", detailID);
                objQuery.AddToQuery("MasterID", Id);
                objQuery.AddToQuery("MasterPurchaseID", SaleId);
                objQuery.AddToQuery("BillSeries", RSeries);
                objQuery.AddToQuery("BillType", RType);
                objQuery.AddToQuery("BillNumber", RNumber);
                objQuery.AddToQuery("BillDate", RDate);
                objQuery.AddToQuery("BillAmount", RBillAmount);
                objQuery.AddToQuery("ClearAmount", RClearedAmount);
                objQuery.AddToQuery("BalanceAmount", RBalanceAmount);
                objQuery.AddToQuery("SerialNumber", serialNumber);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryPChanged(string Id, string changedID, string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
             string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, int serialNumber)
        {
            Query objQuery = new Query();
            try
            {
                objQuery.Table = "changeddetailcashbankpayment";
                objQuery.AddToQuery("DetailCashBankPaymentID", detailID);
                objQuery.AddToQuery("ChangedMasterID", changedID);
                objQuery.AddToQuery("MasterID", Id);
                objQuery.AddToQuery("MasterPurchaseID", SaleId);
                objQuery.AddToQuery("BillSeries", RSeries);
                objQuery.AddToQuery("BillType", RType);
                objQuery.AddToQuery("BillNumber", RNumber);
                objQuery.AddToQuery("BillDate", RDate);
                objQuery.AddToQuery("BillAmount", RBillAmount);
                objQuery.AddToQuery("ClearAmount", RClearedAmount);
                objQuery.AddToQuery("BalanceAmount", RBalanceAmount);
                objQuery.AddToQuery("SerialNumber", serialNumber);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryPDeleted(string Id, string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
             string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, int serialNumber)
        {
            Query objQuery = new Query();
            try
            {
                objQuery.Table = "deleteddetailcashbankpayment";
                objQuery.AddToQuery("DetailCashBankPaymentID", detailID);
                objQuery.AddToQuery("MasterID", Id);
                objQuery.AddToQuery("MasterPurchaseID", SaleId);
                objQuery.AddToQuery("BillSeries", RSeries);
                objQuery.AddToQuery("BillType", RType);
                objQuery.AddToQuery("BillNumber", RNumber);
                objQuery.AddToQuery("BillDate", RDate);
                objQuery.AddToQuery("BillAmount", RBillAmount);
                objQuery.AddToQuery("ClearAmount", RClearedAmount);
                objQuery.AddToQuery("BalanceAmount", RBalanceAmount);
                objQuery.AddToQuery("SerialNumber", serialNumber);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            try
            {
                objQuery.Table = "vouchercashbankpayment";
                objQuery.AddToQuery("CBID", Id, true);
                objQuery.AddToQuery("AccountId", CreditorId);
                objQuery.AddToQuery("Narration", Narration);
                objQuery.AddToQuery("VoucherType", VouType);
                objQuery.AddToQuery("VoucherNumber", VouNo);
                objQuery.AddToQuery("VoucherDate", VouDate);
                objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
                objQuery.AddToQuery("AmountNet", Amt);
                objQuery.AddToQuery("ChequeDepositedBankID", bankaccount);
                objQuery.AddToQuery("ChequeNumber", chqno);
                objQuery.AddToQuery("ChequeDate", chqdate);
                objQuery.AddToQuery("ModifiedUserID", modifiedby);
                objQuery.AddToQuery("ModifiedDate", modifieddate);
                objQuery.AddToQuery("ModifiedTime", modifiedtime);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return objQuery.UpdateQuery();
        }

        private string GetUpdateQueryForFifth(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string bankaccount, string bank, string branch, string chqno, string chqdate, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            try
            {
                objQuery.Table = "vouchercashbankpayment";
                objQuery.AddToQuery("CBID", Id, true);             
                objQuery.AddToQuery("Narration", Narration);
                objQuery.AddToQuery("VoucherDate", VouDate);
                objQuery.AddToQuery("ChequeNumber", chqno);
                objQuery.AddToQuery("ChequeDate", chqdate);
                objQuery.AddToQuery("ModifiedUserID", modifiedby);
                objQuery.AddToQuery("ModifiedDate", modifieddate);
                objQuery.AddToQuery("ModifiedTime", modifiedtime);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return objQuery.UpdateQuery();
        }


        private string GetDeleteQuery(string Id)
        {
            string strSql = "";
            try
            {
                Query objQuery = new Query();
                objQuery.Table = "vouchercashbankpayment";
                objQuery.AddToQuery("CBID", Id, true);
                strSql = objQuery.DeleteQuery();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return strSql;
        }
        #endregion


       
    }
}