using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;


namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBCashPayment
    {
        public DBCashPayment()
        {
        }

        public DataTable GetOverviewData(string DbntType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankpayment a, masteraccount b " +
                            "where a.AccountId = b.AccountId and a.VoucherType = " + "'" + DbntType + "'" + "  order by a.vouchernumber ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetOverviewData(string VouType, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                          "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankpayment a, masteraccount b " +
                          "where a.AccountId = b.AccountId && a.VoucherType = '" + VouType + "' && a.VoucherDate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "'  order by a.vouchernumber ";

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
        public DataTable GetStatementDetailsByIDforModify(string acId, string id)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.ID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.ToDate as VoucherDate, '' as VoucherSubType,a.AmountNet,a.Amountclear,a.AmountBalance,"
                + "a.AccountID,c.AccName as PatientShortName,b.MasterID,b.MasterSaleID  from voucherstatement a  inner join detailcashbankreceipt b on a.ID = b.MastersaleID inner join masteraccount c on a.AccountID = c.AccountId  &&  b.MasterID = '" + id + "'  order by  VoucherType,VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetStatementDetailsByCSRID(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID as ID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.MasterId ,a.FromDate,a.ToDate,b.AccountID,d.AccName as PatientShortName " +
                            " from detailcashbankreceipt a inner join vouchercashbankreceipt b  on a.MasterId = b.CBId  inner join voucherstatement c on a.MasterSaleID = c.ID  inner join masteraccount d on c.AccountID = d.AccountID where  a.MasterID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
       
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from vouchercashbankpayment where CBID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByIDForChanged(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from changedvouchercashbankpayment where changedID ='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow ReadDetailsByIDForDeleted(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from deletedvouchercashbankpayment where CBID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow ReadDetailsByVoucherNumber(int vouno)
        {
            DataRow dRow = null;
            try
            {
                if (vouno != 0)
                {
                    string strSql = "Select * from vouchercashbankpayment where VoucherNumber='{0}' ";
                    strSql = string.Format(strSql, vouno);
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
            //               "AccountID, null as MasterID from voucherpurchase where  AccountId =  '" + Id + "'  && AmountBalance > 0 && VoucherType = '" + FixAccounts.VoucherTypeForCreditPurchase + "' && statementnumber = 0 " +
            //               " union select ID as PurchaseID, VoucherSeries, VoucherType,VoucherNumber,ToDate as VoucherDate,null as VoucherSubtype, null as PurchaseBillNumber,AmountNet,AmountClear,AmountBalance," +
            //                   "AccountID,null as MasterID from voucherstatement where AccountID = '" + Id + "' && AmountBalance > 0 && VoucherType = '" + FixAccounts.VoucherTypeForStatementPurchase + "' order by voucherDate";


            string strSql = "Select PurchaseID,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,VoucherSubType,PurchaseBillNumber,VoucherSubType,AmountNet,Amountclear,AmountBalance," +
                            "AccountID, null as MasterID from voucherpurchase where  AccountId =  '" + Id + "'  && AmountBalance > 0 && VoucherType = '" + FixAccounts.VoucherTypeForCreditPurchase + "' && statementnumber = 0 ";
            //" union select ID as PurchaseID, VoucherSeries, VoucherType,VoucherNumber, ToDate as VoucherDate, null as PurchaseBillNumber,null as vouchersubType,AmountNet,AmountClear,AmountBalance,"+
            //    "AccountID,null as MasterID from voucherstatement where AccountID = '" + Id + "' && AmountBalance > 0 && VoucherType = '" + FixAccounts.VoucherTypeForStatementPurchase +"' order by voucherDate";

           
           dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetPurchaseDetailsByIDforModify(string acId, string id)
        {
            DataTable dtable = new DataTable();

            //string strSql = "Select PurchaseID,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,PurchaseBillNumber,AmountNet,Amountclear,AmountBalance," +
            //                "AccountID, null as MasterID from voucherpurchase where  AccountId =  '" + acId + "'  && AmountBalance > 0 && VoucherType = '" + FixAccounts.VoucherTypeForCreditPurchase + "' && statementnumber = 0 " +
                            //" union select ID as PurchaseID, VoucherSeries, VoucherType,VoucherNumber,ToDate as VoucherDate, null as PurchaseBillNumber,AmountNet,AmountClear,AmountBalance," +
                            //    "AccountID,null as MasterID from voucherstatement where AccountID = '" + acId + "' && AmountBalance > 0 && VoucherType = '" + FixAccounts.VoucherTypeForStatementPurchase + "' order by voucherDate";
                
                // string strSql = "Select a.ID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.VoucherSubType,a.AmountNet,a.Amountclear,a.AmountBalance,"
                //+ "a.AccountID,a.PatientShortName,b.MasterID,b.MasterSaleID  from vouchersale a  inner join detailcashbankreceipt b on a.ID = b.MastersaleID && b.MasterID = '" + id + "'  order by  VoucherType,VoucherNumber";
            string strSql = "Select distinct a.purchaseID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.PurchaseBillNumber,a.VoucherSubType,a.AmountNet, a.AmountClear, a.AmountBalance ," +
                        "a.AccountID,null as MasterID  from voucherpurchase a  inner join  detailcashbankpayment b on a.purchaseID = b.MasterPurchaseID && b.MasterID = '"+ id +"' order by VoucherType,VoucherNumber";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetPurchaseDetailsByCSPID(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            if (Id != "")
            {
                //string strSql = " Select a.MasterId, a.MasterPurchaseID as PurchaseID, a.BalanceAmount as AmountBalance," +
                //       "a.ClearAmount as AmountClear,b.AccountID , '' as PurchaseID, '' as VoucherSeries,  a.BillType as VoucherType, '1' as VoucherNumber, a.BillDate as voucherDate, '1' as PurchaseBillNumber,a.BillAmount as AmountNet " +
                //" from detailcashbankpayment a inner join vouchercashbankpayment b  on a.MasterId = b.CBId where a.MasterID =  " + "'" + Id + "' and  b.AccountID = '" + accid + "' && a.BillType = 'OPB' order by a.SerialNumber " +
                //"union Select a.MasterId, a.MasterPurchaseID as PurchaseID,c.AmountBalance+a.ClearAmount as AmountBalance," +
                //                       "a.ClearAmount as AmountClear,b.AccountID,c.PurchaseID,c.VoucherSeries,c.VoucherType,c.VoucherNumber,c.VoucherDate,c.PurchaseBillNumber,c.AmountNet " +
                //                " from detailcashbankpayment a inner join vouchercashbankpayment b  on a.MasterId = b.CBId inner join voucherpurchase c on a.MasterPurchaseID = c.PurchaseID where a.MasterID =  " + "'" + Id + "' and  b.AccountID = '" + accid + "' ";

                string strSql = " Select a.MasterId as ID,a.MasterID, a.MasterPurchaseID as PurchaseID, a.BalanceAmount as AmountBalance," +
                     "a.ClearAmount as AmountClear,b.AccountID , c.PurchaseBillNumber, c.VoucherSubType,BillSeries as VoucherSeries,  a.BillType as VoucherType,  BillNumber as VoucherNumber, a.BillDate as voucherDate, a.BillAmount as AmountNet " +
              " from detailcashbankpayment a inner join vouchercashbankpayment b  on a.MasterId = b.CBId  left outer join VoucherPurchase c on a.MasterPurchaseId = c.PurchaseID where a.MasterID =  '" + Id + "' &&  b.AccountID = '" + accid + "'  order by a.SerialNumber ";
              

                dtable = DBInterface.SelectDataTable(strSql);
            }

            return dtable;
        }


        public DataTable GetPurchaseDetailsByCSPIDForChanged(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            if (Id != "")
            {
                
                string strSql = " Select a.MasterId,a.changedMasterID as ID, a.MasterPurchaseID as PurchaseID, a.BalanceAmount as AmountBalance," +
                     "a.ClearAmount as AmountClear,b.AccountID ,BillNumber as VoucherNumber,c.PurchaseBillNumber,c.VoucherSubType, BillSeries as VoucherSeries,  a.BillType as VoucherType,  a.BillDate as voucherDate, a.BillAmount as AmountNet " +
              " from changeddetailcashbankpayment a inner join changedvouchercashbankpayment b  on a.changedMasterID = b.ChangedId left outer join voucherpurchase c on a.MasterPurchaseId = c.PurchaseID where a.changedMasterID =  '" + Id + "' &&  b.AccountID = '" + accid + "'  order by a.SerialNumber ";


                dtable = DBInterface.SelectDataTable(strSql);
            }

            return dtable;
        }

        public DataTable GetPurchaseDetailsByCSPIDForDeleted(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            if (Id != "")
            {
                string strSql = " Select a.MasterId as Id, a.MasterID, a.MasterPurchaseID as PurchaseID, a.BalanceAmount as AmountBalance," +
                   "a.ClearAmount as AmountClear,b.AccountID ,BillNumber as VoucherNumber,c.PurchaseBillNumber,c.VoucherSubType, BillSeries as VoucherSeries,  a.BillType as VoucherType,  a.BillDate as voucherDate, a.BillAmount as AmountNet " +
            " from deleteddetailcashbankpayment a inner join deletedvouchercashbankpayment b  on a.MasterID = b.CBId left outer join voucherpurchase c on a.MasterPurchaseId = c.PurchaseID where a.MasterID =  '" + Id + "' &&  b.AccountID = '" + accid + "'  order by a.SerialNumber ";


                dtable = DBInterface.SelectDataTable(strSql);
            }

            return dtable;
        }

        public bool AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt,createdby,createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddChangedDetails(string Id, string changedID, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double Amt, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryChanged(Id,changedID, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDeletedDetails(string Id,  string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDeleted(Id,  CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddDetailsParticulars(string Id, string detailID, string SaleId, string BSeries, string BType, int BNumber, string BDate,
              string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryP(Id,detailID, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                BClearedAmount, BDiscountAmount,serialNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsParticularsChanged(string Id,string changedID, string detailID, string SaleId, string BSeries, string BType, int BNumber, string BDate,
            string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryPChanged(Id,changedID, detailID, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                BClearedAmount, BDiscountAmount, serialNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddDetailsParticularsDeleted(string Id, string detailID, string SaleId, string BSeries, string BType, int BNumber, string BDate,
          string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryPDeleted(Id, detailID, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                BClearedAmount, BDiscountAmount, serialNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateDetailsPurchaseBill(string Id,string purchaseid, string BSeries, string BType, int BNumber, string BDate,
              string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount)
        {
            double BBalanceAmt = BBalanceAmount - BClearedAmount;
            string strSql = "Update voucherpurchase set AmountClear =  AmountClear + " + BClearedAmount + ", AmountBalance = " +
               "AmountNet - AmountClear where purchaseID = '" + purchaseid +  "'";               
            DBInterface.ExecuteQuery(strSql);
            return true;
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

        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, modifiedby,modifieddate,modifiedtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
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

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool DeletePreviosRowsByID(string Id)
        {
            bool bRetValue = false;
            string strSql = "Delete from detailcashbankpayment where MasterID = " + "'" + Id + "'";
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
             string VouDate, double Amt, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankpayment";
            objQuery.AddToQuery("CBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryChanged(string Id,string changedID ,string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "changedvouchercashbankpayment";
            objQuery.AddToQuery("changedID", changedID);
            objQuery.AddToQuery("CBID", Id);            
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryDeleted(string Id,  string CreditorId, string Narration, string VouType, int VouNo,
          string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "deletedvouchercashbankpayment";           
            objQuery.AddToQuery("CBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryP(string Id,string detailID,  string SaleId, string RSeries, string RType, int RNumber, string RDate,
              string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcashbankpayment";
            objQuery.AddToQuery("DetailCashBankPaymentID",detailID);
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
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryPChanged(string Id,string changedID, string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
             string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, int serialNumber)
        {
            Query objQuery = new Query();
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
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryPDeleted(string Id,string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
            string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, int serialNumber)
        {
            Query objQuery = new Query();
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
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankpayment";
            objQuery.AddToQuery("CBID", Id, true);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankpayment";
            objQuery.AddToQuery("CBID", Id, true);        
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 

    }
}