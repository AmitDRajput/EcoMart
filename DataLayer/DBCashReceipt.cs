using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBCashReceipt
    {
        public DBCashReceipt()
        {
        }

        public DataTable GetOverviewData(string VouType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankreceipt a, masteraccount b " +
                            "where a.AccountId = b.AccountId AND a.VoucherType = '" + VouType + "' AND a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' AND a.VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by a.voucherdate desc ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetOverviewData(string VouType,string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankreceipt a, masteraccount b " +
                            "where a.AccountId = b.AccountId AND  a.VoucherType = '"+ VouType + "' AND a.VoucherDate >= '" + fromDate +"' AND  a.VoucherDate <= '"+ toDate +"'  order by a.vouchernumber ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,a.TotalDiscount,a.jvnumber,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where CBID='{0}' ";
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
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,b.AccName,b.AccAddress1,b.AccAddress2 from changedvouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where changedId = '{0}' ";
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
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,b.AccName,b.AccAddress1,b.AccAddress2 from deletedvouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where CBID='{0}' ";
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
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where VoucherNumber= " + vouno + " AND VoucherType = '" + FixAccounts.VoucherTypeForCashReceipt + "' ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataTable GetSaleDetailsByID(string acId, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable(); //Amar
            string strSql = "Select ID,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,VoucherSubType,AmountNet,AmountClear,AmountBalance, 0 as DiscountAmount,"
                + "AccountID,ID as MasterID ,null as MasterSaleID,PatientshortName from vouchersale where  voucherdate >= '" + fromDate + "' AND voucherdate <= '" + toDate + "' AND AccountId =  " + "'" + acId + "'" + " AND (VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' )  AND AmountBalance > 0 order by vouchertype, vouchernumber";
            //" union select ID , VoucherSeries, VoucherType,VoucherNumber,VoucherDate,' ' as VoucherSubType,  AmountNet,AmountClear,AmountBalance,"+
            //              "AccountID,null as MasterID,null as MasterSaleID,' ' as PatientshortName from voucherstatement where AccountID = '" + acId + "'  &&  AmountBalance > 0 && (VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementPurchase + "' || Vouchertype = '" + FixAccounts.VoucherTypeForStatementHospital + "' ) order by vouchertype vouchernumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

            //DataTable dtable = new DataTable();
            //string strSql = "Select ID,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,VoucherSubType,AmountNet,AmountClear,AmountBalance, 0 as DiscountAmount,"
            //    + "AccountID,ID as MasterID ,null as MasterSaleID,PatientshortName from vouchersale where  AccountId =  " + "'" + acId + "'" + " && (VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "')  && AmountBalance > 0 && VoucherSubType != '" + FixAccounts.SubTypeForHospitalSale + "' order by vouchertype, vouchernumber";
            ////" union select ID , VoucherSeries, VoucherType,VoucherNumber,VoucherDate,' ' as VoucherSubType,  AmountNet,AmountClear,AmountBalance,"+
            ////              "AccountID,null as MasterID,null as MasterSaleID,' ' as PatientshortName from voucherstatement where AccountID = '" + acId + "'  &&  AmountBalance > 0 && (VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementPurchase + "' || Vouchertype = '" + FixAccounts.VoucherTypeForStatementHospital + "' ) order by vouchertype vouchernumber";
            //dtable = DBInterface.SelectDataTable(strSql);
            //return dtable;
        }
       
        public DataTable GetStatementDetailsByID(string acId)
        {
            DataTable dtable = new DataTable();
            string strSql = "select a.ID , a.VoucherSeries, a.VoucherType,a.VoucherNumber,a.ToDate as VoucherDate,'' as VoucherSubType,  a.AmountNet,a.AmountClear,a.AmountBalance, 0 as DiscountAmount," +
                          "a.AccountID,null as MasterID,null as MasterSaleID,b.AccName as PatientshortName from voucherstatement a inner join masteraccount b on a.AccountID = b.AccountID where a.AccountID = '" + acId + "'  AND  a.AmountBalance > 0 AND (a.VoucherType = '" + FixAccounts.VoucherTypeForStatementSale + "' OR a.Vouchertype = '" + FixAccounts.VoucherTypeForStatementHospital + "' ) order by a.vouchertype ,a.vouchernumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetSaleDetailsByIDforModify(string acId, string id)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.ID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.VoucherSubType,a.AmountNet,a.Amountclear,a.AmountBalance,b.discountAmount,"
                + "a.AccountID,a.PatientShortName,b.MasterID,b.MasterSaleID  from vouchersale a  inner join detailcashbankreceipt b on a.ID = b.MastersaleID AND b.MasterID = '" + id + "'  order by  VoucherType,VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetStatementDetailsByIDforModify(string acId, string id)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.ID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.ToDate as VoucherDate, '' as VoucherSubType,a.AmountNet,a.Amountclear,a.AmountBalance,b.discountamount,"
                + "a.AccountID,c.AccName as PatientShortName,b.MasterID,b.MasterSaleID  from voucherstatement a  inner join detailcashbankreceipt b on a.ID = b.MastersaleID inner join masteraccount c on a.AccountID = c.AccountId  AND  b.MasterID = '" + id + "'  order by  VoucherType,VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        //public DataTable GetSaleDetailsByIDforModify(string acId, string id)
        //{
        //    DataTable dtable = new DataTable();

        //    string strSql = "Select ID ,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,VoucherSubType,AmountNet,Amountclear,AmountBalance,"
        //        + "AccountID,patientshortname, null as MasterID,null as MasterSaleID  from vouchersale where  AccountId =  " + "'" + acId + "'" + " &&  VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' && Amountclear = 0  order by  VoucherType,VoucherNumber"
        //        + "union Select a.ID ,a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.VoucherSubType,a.AmountNet,a.Amountclear,a.AmountBalance,"
        //        + "a.AccountID,a.PatientShortName,b.MasterID,b.MasterSaleID  from vouchersale a  inner join detailcashbankreceipt b on a.ID = b.MastersaleID and b.MasterID = '" + id + "'  order by  VoucherType,VoucherNumber";
        //    dtable = DBInterface.SelectDataTable(strSql);

        //    return dtable;
        //}

        public DataTable GetSaleDetailsByCSRID(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterSaleID as ID,a.MasterID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.DiscountAmount,a.FromDate,a.ToDate,b.AccountID,c.PatientShortName " +
                            " from detailcashbankreceipt a inner join vouchercashbankreceipt b  on a.MasterId = b.CBId  left outer join vouchersale c on a.MasterSaleID = c.ID where  a.MasterID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetSaleDetailsByCSRIDForChanged(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ChangedMasterID as ID,a.MasterID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.FromDate,a.ToDate,b.AccountID, ''  as PatientShortName " +
                            " from changeddetailcashbankreceipt a inner join changedvouchercashbankreceipt b  on a.ChangedMasterId = b.ChangedId   where  b.ChangedID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetSaleDetailsByCSRIDForDeleted(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID as ID,a.MasterID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.FromDate,a.ToDate,b.AccountID, ''  as PatientShortName " +
                            " from deleteddetailcashbankreceipt a inner join deletedvouchercashbankreceipt b  on a.MasterId = b.CBID   where  a.MasterId =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetStatementDetailsByCSRID(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID as ID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.DiscountAmount,a.MasterId ,a.FromDate,a.ToDate,b.AccountID,d.AccName as PatientShortName " +
                            " from detailcashbankreceipt a inner join vouchercashbankreceipt b  on a.MasterId = b.CBId  inner join voucherstatement c on a.MasterSaleID = c.ID  inner join masteraccount d on c.AccountID = d.AccountID where  a.MasterID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        //Comment the string identity//


        public int AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, double totalDiscount, int jvnumber, int jvID, double onAccountAmount, string createdby, string createddate, string createdtime)

        {
            // bool bRetValue = false;
            string strSql = GetInsertQuery(Id,CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, totalDiscount, jvnumber, jvID, onAccountAmount, createdby, createddate, createdtime);

            //bool ii = (DBInterface.ExecuteQuery(strSql) > 0);
            //strSql = "select last_insert_ID()";
            //int iid = DBInterface.ExecuteScalar(strSql);

            //return iid;
            int ii = Convert.ToInt32(DBInterface.ExecuteScalar(strSql));
            return ii;
        }

        //private string GetInsertQuery(string id, string creditorId, string narration, string vouType, int vouNo, string vouDate, double amt, double totalDiscount, int jvnumber, int jvID, double onAccountAmount, string createdby, string createddate, string createdtime)
        //{
        //    throw new NotImplementedException();
        //}

        public bool AddChangedDetails(string Id, string changedID, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryChanged(Id, changedID, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, modifiedby, modifieddate, modifiedtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDeletedDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDeleted(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, modifiedby, modifieddate, modifiedtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsParticulars(int Id, string detailID, string SaleId, string BSeries, string BType, int BNumber, string BDate,
              string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, int seriralNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetail(Id, detailID, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                BClearedAmount, BDiscountAmount, seriralNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }
        public bool AddDetailsParticularsChanged(string Id, string changedID, string detailID, string SaleId, string BSeries, string BType, int BNumber, string BDate,
            string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, int seriralNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetailChanged(Id, changedID, detailID, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                BClearedAmount, BDiscountAmount, seriralNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }
        public bool AddDetailsParticularsDeleted(string Id, string detailID, string SaleId, string BSeries, string BType, int BNumber, string BDate,
           string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount, int seriralNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetailDeleted(Id, detailID, SaleId, BSeries, BType, BNumber, BDate, BSubType, BBillAmount, BBalanceAmount,
                BClearedAmount, BDiscountAmount, seriralNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }
        public bool UpdateDetailsSCCBill(string Id, string saleid, string BSeries, string BType, int BNumber, string BDate,
              string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount)
        {
            bool bRetValue = false;
            double mamtclear = 0;
            double mamtbalance = 0;
            double mamtnet = 0;
            DataRow dr;
            string strSql = "select AmountClear,AmountBalance,AmountNet from vouchersale where  ID = '" + saleid + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null)
            {
                if (dr["AmountClear"] != DBNull.Value)
                    mamtclear = Convert.ToDouble(dr["AmountClear"].ToString());

                if (dr["AmountBalance"] != DBNull.Value)
                    mamtbalance = Convert.ToDouble(dr["AmountBalance"].ToString());
                if (dr["AmountNet"] != DBNull.Value)
                    mamtnet = Convert.ToDouble(dr["AmountNet"].ToString());
            }
            mamtclear += BClearedAmount + BDiscountAmount;
          //  mamtbalance += BBalanceAmount;
            mamtbalance = mamtnet - mamtclear;
            strSql = "Update vouchersale set AmountClear =   " + mamtclear + ",  AmountBalance = " +  mamtbalance + " where  ID = '" + saleid + "'";

               
            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        public bool UpdateSaleStatement(string Id, string saleid, string BSeries, string BType, int BNumber, string BDate,
             string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount)
        {
            bool bRetValue = false;
            double mamtclear = 0;
            double mamtbalance = 0;
            double mamtnet = 0;
            DataRow dr;
            string strSql = "select AmountClear,AmountBalance,AmountNet from voucherstatement where  ID = '" + saleid + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null)
            {
                if (dr["AmountClear"] != DBNull.Value)
                    mamtclear = Convert.ToDouble(dr["AmountClear"].ToString());

                if (dr["AmountBalance"] != DBNull.Value)
                    mamtbalance = Convert.ToDouble(dr["AmountBalance"].ToString());
                if (dr["AmountNet"] != DBNull.Value)
                    mamtnet = Convert.ToDouble(dr["AmountNet"].ToString());
            }
            mamtclear += BClearedAmount + BDiscountAmount;
            //  mamtbalance += BBalanceAmount;
            mamtbalance = mamtnet - mamtclear;
            strSql = "Update voucherstatement set AmountClear =   " + mamtclear + ",  AmountBalance = " + mamtbalance + " where  ID = '" + saleid + "'";


            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            //bool bRetValue = false;
            //double BBalanceAmt = BBalanceAmount - BClearedAmount;
            //string strSql = "Update voucherstatement set AmountClear =  AmountClear + " + BClearedAmount + ",  AmountBalance = " +
            //   "AmountNet - AmountClear where  ID = '" + saleid + "'";

            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //    bRetValue = true;
            return bRetValue;
        }
        public bool UpdateOpeningBalanceReducePrevious(string accID, double PreopeningCleared)
        {
            bool bRetValue = false;
            double mclearedamt = 0;
            string strSql = "select AccClearedAmount  from masteraccount Where AccountID = '"+accID +"'";
            DataRow dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null && dr["AccClearedAmount"] != DBNull.Value)
                mclearedamt = Convert.ToDouble(dr["AccClearedAmount"].ToString());
            mclearedamt = mclearedamt  - PreopeningCleared;
            strSql = "update masteraccount  set AccClearedAmount = "+ mclearedamt +" Where AccountID = '" + accID + "'";
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
        public bool UpdateDetails(int Id, int CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, double totalDiscount, int jvnumber, string jvID, double onAccountAmount, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt,totalDiscount, jvnumber, jvID, onAccountAmount, modifiedby, modifieddate, modifiedtime);

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
        public bool RevertPreviousStatementBalance(string nSaleID, double nClearedAmount)
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
        private string GetInsertQuery(string Id,string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, double totalDiscount, int jvnumber, int jvID, double onAccountAmount, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankreceipt";
            //objQuery.AddToQuery("CBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("JVNumber", jvnumber);
            objQuery.AddToQuery("JVID", jvID);
            objQuery.AddToQuery("TotalDiscount", totalDiscount);
            objQuery.AddToQuery("OnAccountAmount", onAccountAmount);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryChanged(string Id, string changedID, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "changedvouchercashbankreceipt";
            objQuery.AddToQuery("CBID", Id);
            objQuery.AddToQuery("ChangedID", changedID);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", FixAccounts.VoucherTypeForCashReceipt);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryDeleted(string Id, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "deletedvouchercashbankreceipt";
            objQuery.AddToQuery("CBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", FixAccounts.VoucherTypeForCashReceipt);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryDetail(int Id, string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
              string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcashbankreceipt";
            //objQuery.AddToQuery("DetailCashBankReceiptID", detailID);
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("MasterSaleID", SaleId);
            objQuery.AddToQuery("BillSeries", RSeries);
            objQuery.AddToQuery("BillType", RType);
            objQuery.AddToQuery("BillNumber", RNumber);
            //objQuery.AddToQuery("BillDate", RDate);
            objQuery.AddToQuery("BillSubType", RSubType);
            objQuery.AddToQuery("BillAmount", RBillAmount);
            objQuery.AddToQuery("ClearAmount", RClearedAmount);
            objQuery.AddToQuery("BalanceAmount", RBalanceAmount);
            objQuery.AddToQuery("DiscountAmount", RDiscountAmount);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryDetailChanged(string Id, string changedID, string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
             string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "changeddetailcashbankreceipt";
            objQuery.AddToQuery("DetailCashBankReceiptID", detailID);
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("ChangedMasterID", changedID);
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
        private string GetInsertQueryDetailDeleted(string Id, string detailID, string SaleId, string RSeries, string RType, int RNumber, string RDate,
             string RSubType, double RBillAmount, double RBalanceAmount, double RClearedAmount, double RDiscountAmount, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "deleteddetailcashbankreceipt";
            objQuery.AddToQuery("DetailCashBankReceiptID", detailID);
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
        private string GetUpdateQuery(int Id, int CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, double totalDiscount, int jvnumber, string jvID, double onAccountAmount, string modifiedby, string modifieddate, string modifiedtime)
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
            objQuery.AddToQuery("TotalDiscount", totalDiscount);
            objQuery.AddToQuery("JVNumber", jvnumber);
            objQuery.AddToQuery("JVID", jvID);
            objQuery.AddToQuery("OnAccountAmount", onAccountAmount);
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
        #endregion


        public DataRow GetLastRecord(string vouType, string vouSeries)
        {
            DataRow dRow = null;

            string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID  where voucherType = '" + vouType + "' AND voucherSeries = '" + vouSeries + "' order by vouchernumber desc";

            //string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' && voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber desc";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        public DataRow GetLastVoucherNumber(string vouType, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select VoucherNumber from vouchercashbankreceipt  where voucherType = '" + vouType + "' AND voucherSeries = '" + vouSeries + "' order by vouchernumber desc";

               // string strSql = "Select Vouchernumber from vouchercreditdebitnote where  VoucherType =  '" + vouType + "'  &&  VoucherSeries = '" + vouSeries + "' order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetFirstRecord(string vouType, string vouSeries)
        {
            DataRow dRow = null;
            string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where VoucherType= " + vouType + " AND VoucherSeries = '" + vouSeries + "' ";
           // string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' && voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber ";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }


        public bool AddToMasterJV(int Id, int CBJVIDpay, int CBVouNo, string CBAccountID, string CBNarration, string CBVouDate, double CBTotalDiscount, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryJV(Id, CBJVIDpay, CBVouNo, CBAccountID, CBNarration, CBVouDate, CBTotalDiscount, CreatedBy, CreatedDate, CreatedTime);                                                                     
                

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQueryJV(int Id, int CBJVIDpay, int CBVouNo, string CBAccountID, string CBNarration, string CBVouDate, double CBTotalDiscount, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherjv";
            //objQuery.AddToQuery("ID", CBJVID);
            objQuery.AddToQuery("AccountId", CBAccountID);
            objQuery.AddToQuery("Narration", CBNarration);
            objQuery.AddToQuery("VoucherType", "JV ");
            objQuery.AddToQuery("VoucherNumber", CBVouNo);
            objQuery.AddToQuery("VoucherDate", CBVouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("Debit", 0);
            objQuery.AddToQuery("Credit", CBTotalDiscount);
            objQuery.AddToQuery("ReferenceVoucherID", Id);
            objQuery.AddToQuery("CreatedUserID", CreatedBy);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);

            return objQuery.InsertQuery();
        }
        public bool AddJVIntblTrnac(int Id, int CBJVIDpay, int CBVouNo, string CBAccountID, string CBNarration, string CBVouDate, double CBTotalDiscount, string detailID, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForJVIntblTrnac(Id, CBJVIDpay, CBVouNo, CBAccountID, CBNarration, CBVouDate, CBTotalDiscount, detailID, CreatedBy, CreatedDate, CreatedTime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddJVIntblTrnacReverse(int Id, int CBJVIDpay, int CBVouNo, string CBAccountID, string CBNarration, string CBVouDate, double CBTotalDiscount, string detailID, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForJVIntblTrnacReverse(Id, CBJVIDpay, CBVouNo, CBAccountID, CBNarration, CBVouDate, CBTotalDiscount,  detailID, CreatedBy, CreatedDate, CreatedTime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetInsertQueryForJVIntblTrnac(int Id, int CBJVIDpay, int CBVouNo, string CBAccountID, string CBNarration, string CBVouDate, double CBTotalDiscount, string detailID, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            //objQuery.AddToQuery("tblTrnacID", detailID);
            objQuery.AddToQuery("VoucherID", CBJVIDpay);
            objQuery.AddToQuery("AccountId", FixAccounts.AccountDiscountInCashBankEntry);
            objQuery.AddToQuery("Debit", CBTotalDiscount);
            objQuery.AddToQuery("Credit", 0);
            objQuery.AddToQuery("AccAccountID", CBAccountID);
            objQuery.AddToQuery("TransactionDate", CBVouDate);
            objQuery.AddToQuery("ReferenceVoucherId", Id);
            objQuery.AddToQuery("VoucherType", "JV ");
            objQuery.AddToQuery("Narration", CBNarration);
            objQuery.AddToQuery("VoucherNumber", CBVouNo);
            objQuery.AddToQuery("CreatedUserID", CreatedBy);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryForJVIntblTrnacReverse(int Id, int CBJVIDpay, int CBVouNo, string CBAccountID, string CBNarration, string CBVouDate, double CBTotalDiscount, string detailID, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            //.AddToQuery("tblTrnacID", detailID);
            objQuery.AddToQuery("VoucherID", CBJVIDpay);
            objQuery.AddToQuery("AccountId",CBAccountID );
            objQuery.AddToQuery("Debit", 0);
            objQuery.AddToQuery("Credit", CBTotalDiscount);
            objQuery.AddToQuery("AccAccountID", FixAccounts.AccountDiscountInCashBankEntry);
            objQuery.AddToQuery("TransactionDate", CBVouDate);
            objQuery.AddToQuery("ReferenceVoucherId", Id);
            objQuery.AddToQuery("VoucherType", "JV ");
            objQuery.AddToQuery("Narration", CBNarration);
            objQuery.AddToQuery("VoucherNumber", CBVouNo);
            objQuery.AddToQuery("CreatedUserID", CreatedBy);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            return objQuery.InsertQuery();
        }

        public bool DeleteJV(string Id)
        {
            bool bRetValue = false;
            string strSql = "Delete from voucherjv where referencevoucherID = " + "'" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            strSql = "Delete from tbltrnac where referencevoucherID = " + "'" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public DataTable GetOldStatementDetailsByID(string acId)
        {
            DataTable dtable = new DataTable();
            //string strSql = "select a.ID , a.VoucherSeries, a.VoucherType,a.VoucherNumber,a.ToDate as VoucherDate,'' as VoucherSubType,  a.AmountNet,a.AmountClear,a.AmountBalance, 0 as DiscountAmount," +
            //              "a.AccountID,null as MasterID,null as MasterSaleID,b.AccName as PatientshortName from tblOldvoucherstatement a inner join masteraccount b on a.AccountID = b.AccountID where a.AccountID = '" + acId + "'  AND  a.AmountBalance > 0 AND (a.VoucherType = '" + FixAccounts.VoucherTypeForStatementSale + "' OR  a.Vouchertype = '" + FixAccounts.VoucherTypeForStatementHospital + "' ) order by a.vouchertype ,a.vouchernumber";

            string strSql = "select  b.AccountID,null as MasterID,null as MasterSaleID,b.AccName as PatientshortName From  masteraccount b   where b.AccountID = '" + acId + "'  order by b.AccName";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetOldSaleDetailsByID(string acId)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ID,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,VoucherSubType,AmountNet,AmountClear,AmountBalance, 0 as DiscountAmount,"
                + "AccountID,ID as MasterID ,null as MasterSaleID,PatientshortName from tblOldvouchersale where  AccountId =  " + "'" + acId + "'" + " AND (VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "')  AND AmountBalance > 0  order by vouchertype, vouchernumber";
            //" union select ID , VoucherSeries, VoucherType,VoucherNumber,VoucherDate,' ' as VoucherSubType,  AmountNet,AmountClear,AmountBalance,"+
            //              "AccountID,null as MasterID,null as MasterSaleID,' ' as PatientshortName from voucherstatement where AccountID = '" + acId + "'  &&  AmountBalance > 0 && (VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementPurchase + "' || Vouchertype = '" + FixAccounts.VoucherTypeForStatementHospital + "' ) order by vouchertype vouchernumber";
            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetOldSaleDetailsByCSRIDForChanged(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ChangedMasterID as ID,a.MasterID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.FromDate,a.ToDate,b.AccountID, ''  as PatientShortName " +
                            " from changeddetailcashbankreceipt a inner join changedvouchercashbankreceipt b  on a.ChangedMasterId = b.ChangedId   where  b.ChangedID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetOldSaleDetailsByCSRIDForDeleted(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID as ID,a.MasterID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
                                   "a.ClearAmount as AmountClear,a.FromDate,a.ToDate,b.AccountID, ''  as PatientShortName " +
                            " from deleteddetailcashbankreceipt a inner join deletedvouchercashbankreceipt b  on a.MasterId = b.CBID   where  a.MasterId =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetOldSaleDetailsByCSRID(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            //string strSql = "Select a.MasterID as ID,a.MasterID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
            //                       "a.ClearAmount as AmountClear,a.DiscountAmount,a.FromDate,a.ToDate,b.AccountID,c.PatientShortName " +
            //                " from detailcashbankreceipt a inner join vouchercashbankreceipt b  on a.MasterId = b.CBId  left outer join vouchersale c on a.MasterSaleID = c.ID where  a.MasterID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            //dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public bool UpdateSaleStatementOld(string Id, string saleid, string BSeries, string BType, int BNumber, string BDate,
             string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount)
        {
            bool bRetValue = false;
            double mamtclear = 0;
            double mamtbalance = 0;
            double mamtnet = 0;
            DataRow dr;
            string strSql = "select AmountClear,AmountBalance,AmountNet from tbloldvoucherstatement where  ID = '" + saleid + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null)
            {
                if (dr["AmountClear"] != DBNull.Value)
                    mamtclear = Convert.ToDouble(dr["AmountClear"].ToString());

                if (dr["AmountBalance"] != DBNull.Value)
                    mamtbalance = Convert.ToDouble(dr["AmountBalance"].ToString());
                if (dr["AmountNet"] != DBNull.Value)
                    mamtnet = Convert.ToDouble(dr["AmountNet"].ToString());
            }
            mamtclear += BClearedAmount + BDiscountAmount;
            //  mamtbalance += BBalanceAmount;
            mamtbalance = mamtnet - mamtclear;
            strSql = "Update tbloldvoucherstatement set AmountClear =   " + mamtclear + ",  AmountBalance = " + mamtbalance + " where  ID = '" + saleid + "'";


            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
           
            return bRetValue;
        }

        public bool UpdateDetailsSCCBillOld(string Id, string saleid, string BSeries, string BType, int BNumber, string BDate,
              string BSubType, double BBillAmount, double BBalanceAmount, double BClearedAmount, double BDiscountAmount)
        {
            bool bRetValue = false;
            double mamtclear = 0;
            double mamtbalance = 0;
            double mamtnet = 0;
            DataRow dr;
            string strSql = "select AmountClear,AmountBalance,AmountNet from tbloldvouchersale where  ID = '" + saleid + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null)
            {
                if (dr["AmountClear"] != DBNull.Value)
                    mamtclear = Convert.ToDouble(dr["AmountClear"].ToString());

                if (dr["AmountBalance"] != DBNull.Value)
                    mamtbalance = Convert.ToDouble(dr["AmountBalance"].ToString());
                if (dr["AmountNet"] != DBNull.Value)
                    mamtnet = Convert.ToDouble(dr["AmountNet"].ToString());
            }
            mamtclear += BClearedAmount + BDiscountAmount;
            //  mamtbalance += BBalanceAmount;
            mamtbalance = mamtnet - mamtclear;
            strSql = "Update tbloldvouchersale set AmountClear =   " + mamtclear + ",  AmountBalance = " + mamtbalance + " where  ID = '" + saleid + "'";


            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        public DataTable GetSaleDetailsByCSRIDFromtblOld(string Id, string accid)
        {
            DataTable dtable = new DataTable();
            //string strSql = "Select a.MasterID as ID,a.MasterID, a.MasterSaleID,a.BillSeries as VoucherSeries,a.BillType as VoucherType,a.BillNumber as VoucherNumber,a.BillDate as VoucherDate,a.BillSubType as VoucherSubType,a.BillAmount as AmountNet,a.BalanceAmount as AmountBalance," +
            //                       "a.ClearAmount as AmountClear,a.DiscountAmount,a.FromDate,a.ToDate,b.AccountID,c.PatientShortName " +
            //                " from detailcashbankreceipt a inner join vouchercashbankreceipt b  on a.MasterId = b.CBId  left outer join tbloldvouchersale c on a.MasterSaleID = c.ID where  a.MasterID =  " + "'" + Id + "' and b.AccountID = '" + accid + "' order by a.SerialNumber";

            //dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public bool AddDetailsParticularsChangedreceipt(int intID, int CBJVIDpay, string vouchertype)
        {
            bool bRetValue = false;
            string strSql = "update vouchercashbankreceipt  set JVID = " + CBJVIDpay + " Where  VoucherType= " + vouchertype + " AND ID = " + intID;

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

    }
}
