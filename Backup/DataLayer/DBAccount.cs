using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBAccount
    {
        public DBAccount()
        {
        }
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select AccountID,AccCode, AccName, AccOpeningDebit, AccOpeningCredit, " +
                "AccTransactionDebit, AccTransactionCredit, AccClosingDebit, AccClosingCredit, " +
                "AccAddress1,AccAddress2, AccTelephone, AccContactPerson, AccDiscountOffered, AccCrVisitDays, " +
                "AccTransactionType, AccIFOctroi, AccOctroiPer, AccBankId, AccBranchID, AccGroupID, " +
                "AccDoctorID, AccAreaID, AccClearedAmount, AccLastVoucherNumber, AccVoucherDate, " +
                "AccShortName, AccNameAddress, AccBirthDay, AccBirthMonth, AccBirthYear, AccHistory, " +
                "AccVATTinNumber, AccEmailID, IPartyID, AccNumber, AccRemark1,AccRemark2, AccBankAccountNumber " +
                ", AccCrVisitDays, AccShortName, AccDbVisitDay1, AccDbVisitDay2, AccDbVisitDay3 "+
                ", AccDiscountOffered, AccVATTinNumber, AccBankAccountNumber, AccDLN, AccTokenNumber from masteraccount order by AccName";

            dtable = DBInterface.SelectDataTable(strSql);            
            return dtable;
        }
        public DataTable GetOverviewDataForList()
        {
            DataTable dtable = new DataTable();
             string strSql = "Select a.AccountID,a.AccCode, a.AccName,a.AccAddress1,a.AccAddress2,a.AccAreaID,b.AreaID,b.AreaName from masteraccount a left outer join masterarea b on a.AccAreaID = b.AreaID  order by a.AccName";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForContraEntry()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select AccountID,AccCode, AccName, AccOpeningDebit, AccOpeningCredit, " +
                "AccTransactionDebit, AccTransactionCredit, AccClosingDebit, AccClosingCredit, " +
                "AccAddress1,AccAddress2, AccTelephone, AccContactPerson, AccDiscountOffered, AccCrVisitDays, " +
                "AccTransactionType, AccIFOctroi, AccOctroiPer, AccBankId, AccBranchID, AccGroupID, " +
                "AccDoctorID, AccAreaID, AccClearedAmount, AccLastVoucherNumber, AccVoucherDate, " +
                "AccShortName, AccNameAddress, AccBirthDay, AccBirthMonth, AccBirthYear, AccHistory, " +
                "AccVATTinNumber, AccEmailID, IPartyID, AccNumber, AccRemark1,AccRemark2, AccBankAccountNumber " +
                ", AccCrVisitDays, AccShortName, AccDbVisitDay1, AccDbVisitDay2, AccDbVisitDay3 " +
                ", AccDiscountOffered, AccVATTinNumber, AccBankAccountNumber, AccDLN, AccTokenNumber from masteraccount where AccountID = '"+ FixAccounts.AccountCash +"' || AccCode = '"+ FixAccounts.AccCodeForBank +"' order by AccName";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForContraEntry(string accountID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select AccountID,AccCode, AccName, AccOpeningDebit, AccOpeningCredit, " +
                "AccTransactionDebit, AccTransactionCredit, AccClosingDebit, AccClosingCredit, " +
                "AccAddress1,AccAddress2, AccTelephone, AccContactPerson, AccDiscountOffered, AccCrVisitDays, " +
                "AccTransactionType, AccIFOctroi, AccOctroiPer, AccBankId, AccBranchID, AccGroupID, " +
                "AccDoctorID, AccAreaID, AccClearedAmount, AccLastVoucherNumber, AccVoucherDate, " +
                "AccShortName, AccNameAddress, AccBirthDay, AccBirthMonth, AccBirthYear, AccHistory, " +
                "AccVATTinNumber, AccEmailID, IPartyID, AccNumber, AccRemark1,AccRemark2, AccBankAccountNumber " +
                ", AccCrVisitDays, AccShortName, AccDbVisitDay1, AccDbVisitDay2, AccDbVisitDay3 " +
                ", AccDiscountOffered, AccVATTinNumber, AccBankAccountNumber, AccDLN, AccTokenNumber from masteraccount where AccountID != '"+ accountID +"' && (AccountID = '" + FixAccounts.AccountCash + "' || AccCode = '" + FixAccounts.AccCodeForBank + "') order by AccName";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewData(string AccCode)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select AccountID,AccCode, AccName, AccOpeningDebit, AccOpeningCredit, " +
                "AccTransactionDebit, AccTransactionCredit, AccClosingDebit, AccClosingCredit, " +
                "AccAddress1,AccAddress2, AccTelephone, AccContactPerson, AccDiscountOffered, AccCrVisitDays, " +
                "AccTransactionType, AccIFOctroi, AccOctroiPer, AccBankId, AccBranchID, AccGroupID, " +
                "AccDoctorID, AccAreaID, AccClearedAmount, AccLastVoucherNumber, AccVoucherDate, " +
                "AccShortName, AccNameAddress, AccBirthDay, AccBirthMonth, AccBirthYear, AccHistory, " +
                "AccVATTinNumber, AccEmailID, IPartyID, AccNumber, AccRemark1,AccRemark2, AccBankAccountNumber " +
                ", AccCrVisitDays, AccShortName, AccDbVisitDay1, AccDbVisitDay2, AccDbVisitDay3 " +
                ", AccDiscountOffered, AccVATTinNumber, AccBankAccountNumber,AccTokenNumber from masteraccount where acccode = '{0}' order by AccName",AccCode);

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow  GetTokenNumber()
        {
            DataRow dr = null;
            string strSql = "select TokenNumber from tblvouchernumbers";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }
        public void ClearAllSetDefault()
        {
            string strSql = "";
            strSql = "update masteraccount set AccIFOctroi = 'N' where AccCode = 'B'";
            DBInterface.ExecuteQuery(strSql);
        }
        public void SavePartyMSCDACodeInMasterAccount(string ID, string Code, string AlliedCode)
        {
            string strSql = "";
            strSql = "update masteraccount set MSCDACode = '{1}', AlliedCode = '{2}' where AccountID = '{0}'";
            strSql = string.Format(strSql, ID, Code, AlliedCode);
            DBInterface.ExecuteQuery(strSql);
        }
        public void SetThisAsDefault(string accountID)
        {
            string strSql = "";
            strSql = "update masteraccount set AccIfOctroi = 'Y' where AccountID = '"+ accountID +"'";
            DBInterface.ExecuteQuery(strSql);
        }
        public DataRow ReadCreditorDataByTokenNumber(int TokenNumber)
        {
            string strSql = "";
            DataRow dRow = null;
            if (TokenNumber != 0)
            {
              strSql = "Select * from masteraccount where AccTokenNumber = " + TokenNumber ;
              dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadPatientDataByTokenNumber(int TokenNumber)
        {
            string strSql = "";
            DataRow dRow = null;
            if (TokenNumber != 0)
            {
                strSql = "Select * from masterpatient where TokenNumber = " + TokenNumber;  
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select AccountID,AccCode, AccName, AccOpeningDebit, AccOpeningCredit, " +
                "AccAddress1,AccAddress2, AccTelephone, AccContactPerson, AccDiscountOffered, AccCrVisitDays, " +
                "AccTransactionType, AccIFOctroi, AccOctroiPer, AccBankId, AccBranchID, AccGroupID, " +
                "AccDoctorID, AccAreaID, AccShortName, AccNameAddress, AccBirthDay, AccBirthMonth, AccBirthYear, AccHistory, " +
                "AccEmailID, AccRemark1,AccRemark2, AccBankAccountNumber " +
                ", AccCrVisitDays, AccDbVisitDay1, AccDbVisitDay2, AccDbVisitDay3 " +
                ", AccVATTinNumber, AccPAN, AccDLN,AccTokenNumber,AccDiscountOffered,AccStatement15Days,AccLessPercentInDebitNote from masteraccount where AccountID='{0}' ";

                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }      

        #region For Debtors

        public bool AddDetailsDr(string Id, string AccCode, string AccName, string AccGroupID, 
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1,string AccAddress2, string AccEmailID,
            string AccTransactionType, string AccBankId, string AccBranchID, string AccdoctorID, string AccTelephone,
            string AccContactPerson, string AccRemark1,string AccRemark2, int AccBirthDay, int AccBirthMonth, int AccBirthYear
            , int AccDbVisitDay1, int AccDbVisitDay2, int AccDbVisitDay3, string AccNameAddress,string AccVatTIN, string AccDLN, int AccTokenNumber,double accDiscountOffered, string createdby, string CreatedDate, string Createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDr(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2, AccEmailID, AccTransactionType, AccBankId, AccBranchID, AccdoctorID,
                AccTelephone, AccContactPerson, AccRemark1,AccRemark2, AccBirthDay, AccBirthMonth, AccBirthYear
                , AccDbVisitDay1, AccDbVisitDay2, AccDbVisitDay3, AccNameAddress, AccVatTIN, AccDLN, AccTokenNumber,accDiscountOffered, createdby, CreatedDate,Createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetailsDr(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTransactionType, string AccBankId, string AccBranchID, string AccDoctorID, string AccTelephone,
            string AccContactPerson, string AccRemark1, string AccRemark2, int AccBirthDay, int AccBirthMonth, int AccBirthYear
            , int AccDbVisitDay1, int AccDbVisitDay2, int AccDbVisitDay3, string AccNameAddress, string AccVatTIN, string AccDLN, int AccTokenNumber, double accDiscountOffered, string modifyby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryDr(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2, AccEmailID, AccTransactionType, AccBankId, AccBranchID, AccDoctorID,
                AccTelephone, AccContactPerson, AccRemark1, AccRemark2, AccBirthDay, AccBirthMonth, AccBirthYear
                , AccDbVisitDay1, AccDbVisitDay2, AccDbVisitDay3, AccNameAddress, AccVatTIN, AccDLN, AccTokenNumber,accDiscountOffered, modifyby, modifydate,modifytime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQueryDr(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTransactionType, string AccBankId, string AccBranchID, string AccDoctorID, string AccTelephone,
            string AccContactPerson, string AccRemark1,string AccRemark2, int AccBirthDay, int AccBirthMonth, int AccBirthYear
            , int AccDbVisitDay1, int AccDbVisitDay2, int AccDbVisitDay3, string AccNameAddress, string AccVATTIN, string AccDLN, int AccTokenNumber, double accDiscountOffered, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masteraccount";
            objQuery.AddToQuery("AccountID", Id, true);
            objQuery.AddToQuery("AccCode", AccCode);
            objQuery.AddToQuery("AccName", AccName);
            objQuery.AddToQuery("AccGroupID", AccGroupID);
            objQuery.AddToQuery("AccOpeningDebit", AccOpeningDebit);
            objQuery.AddToQuery("AccOpeningCredit", AccOpeningCredit);
            objQuery.AddToQuery("AccAddress1", AccAddress1);
            objQuery.AddToQuery("AccAddress2", AccAddress2);
            objQuery.AddToQuery("AccEmailID", AccEmailID);
            objQuery.AddToQuery("AccTransactionType", AccTransactionType);
            objQuery.AddToQuery("AccBankId", AccBankId);
            objQuery.AddToQuery("AccBranchID", AccBranchID);
            objQuery.AddToQuery("AccDoctorID", AccDoctorID);
            objQuery.AddToQuery("AccTelephone", AccTelephone);
            objQuery.AddToQuery("AccContactPerson", AccContactPerson);
            objQuery.AddToQuery("AccRemark1", AccRemark1);
            objQuery.AddToQuery("AccRemark2", AccRemark2);
            objQuery.AddToQuery("AccBirthDay", AccBirthDay);
            objQuery.AddToQuery("AccBirthMonth", AccBirthMonth);
            objQuery.AddToQuery("AccBirthYear", AccBirthYear);
            objQuery.AddToQuery("AccDbVisitDay1", AccDbVisitDay1);
            objQuery.AddToQuery("AccDbVisitDay2", AccDbVisitDay2);
            objQuery.AddToQuery("AccDbVisitDay3", AccDbVisitDay3);
            objQuery.AddToQuery("AccNameAddress", AccNameAddress);
            objQuery.AddToQuery("AccVATTinNumber", AccVATTIN);
            objQuery.AddToQuery("AccDLN", AccDLN);
            objQuery.AddToQuery("AccTokenNumber", AccTokenNumber);
            objQuery.AddToQuery("AccCrVisitDays", "");
            objQuery.AddToQuery("AccShortName", "");
            objQuery.AddToQuery("AccDiscountOffered", accDiscountOffered);
            objQuery.AddToQuery("IFFIX", "N");
            objQuery.AddToQuery("AccIfOctroi", "N");
            objQuery.AddToQuery("AccOctroiPer", 0);
            objQuery.AddToQuery("AccClearedAmount", 0);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQueryDr(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1,string AccAddress2, string AccEmailID,
            string AccTransactionType, string AccBankId, string AccBranchID, string AccDoctorID, string AccTelephone,
            string AccContactPerson, string AccRemark1,string AccRemark2, int AccBirthDay, int AccBirthMonth, int AccBirthYear
            , int AccDbVisitDay1, int AccDbVisitDay2, int AccDbVisitDay3, string AccNameAddress, string AccVATTIN, string AccDLN, int AccTokenNumber, double accDiscountOffered, string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masteraccount";
            objQuery.AddToQuery("AccountID", Id, true);
            objQuery.AddToQuery("AccCode", AccCode);
            objQuery.AddToQuery("AccName", AccName);
            objQuery.AddToQuery("AccGroupID", AccGroupID);
            objQuery.AddToQuery("AccOpeningDebit", AccOpeningDebit);
            objQuery.AddToQuery("AccOpeningCredit", AccOpeningCredit);
            objQuery.AddToQuery("AccAddress1", AccAddress1);
            objQuery.AddToQuery("AccAddress2", AccAddress2);
            objQuery.AddToQuery("AccEmailID", AccEmailID);
            objQuery.AddToQuery("AccTransactionType", AccTransactionType);
            objQuery.AddToQuery("AccBankId", AccBankId);
            objQuery.AddToQuery("AccBranchID", AccBranchID);
            objQuery.AddToQuery("AccDoctorID", AccDoctorID);
            objQuery.AddToQuery("AccTelephone", AccTelephone);
            objQuery.AddToQuery("AccContactPerson", AccContactPerson);
            objQuery.AddToQuery("AccRemark1", AccRemark1);
            objQuery.AddToQuery("AccRemark2", AccRemark2);
            objQuery.AddToQuery("AccBirthDay", AccBirthDay);
            objQuery.AddToQuery("AccBirthMonth", AccBirthMonth);
            objQuery.AddToQuery("AccBirthYear", AccBirthYear);
            objQuery.AddToQuery("AccDbVisitDay1", AccDbVisitDay1);
            objQuery.AddToQuery("AccDbVisitDay2", AccDbVisitDay2);
            objQuery.AddToQuery("AccDbVisitDay3", AccDbVisitDay3);
            objQuery.AddToQuery("AccNameAddress", AccNameAddress);
            objQuery.AddToQuery("AccVATTinNumber", AccVATTIN);
            objQuery.AddToQuery("AccDLN", AccDLN);
            objQuery.AddToQuery("AccTokenNumber", AccTokenNumber);
            objQuery.AddToQuery("AccCrVisitDays", "");
            objQuery.AddToQuery("AccShortName", "");
            objQuery.AddToQuery("AccDiscountOffered", accDiscountOffered);
            objQuery.AddToQuery("IFFIX", "N");
            objQuery.AddToQuery("AccIfOctroi", "N");
            objQuery.AddToQuery("AccOctroiPer", 0);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }
        #endregion

        #region For Creditors
        public bool AddDetailsCreditor(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1,string AccAddress2, string AccEmailID,
            string AccTelephone, string AccContactPerson, string AccRemark1, string AccRemark2,
            string AccCrVisitDays, string AccShortName, double AccDiscountOffered, string AccVATTinNumber, string AccDLN,string AccStatement15Days,double AccLessPercentInDebitNote, string createdby, string createddate,string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryCreditor(Id, AccCode, AccName, AccGroupID, AccOpeningDebit, 
                AccOpeningCredit, AccAddress1,AccAddress2, AccEmailID, AccTelephone, AccContactPerson, AccRemark1,AccRemark2,
                AccCrVisitDays, AccShortName, AccDiscountOffered, AccVATTinNumber, AccDLN,AccStatement15Days,AccLessPercentInDebitNote, createdby, createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        
        public bool UpdateDetailsCreditor(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTelephone, string AccContactPerson, string AccRemark1, string AccRemark2,
            string AccCrVisitDays, string AccShortName, double AccDiscountOffered, string AccVATTinNumber, string AccDLN, string AccStatement15Days, double AccLessPercentInDebitNote, string modifyby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryCreditor(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2,  AccEmailID, AccTelephone, AccContactPerson, AccRemark1,AccRemark2,
                AccCrVisitDays, AccShortName, AccDiscountOffered, AccVATTinNumber,AccDLN,AccStatement15Days,AccLessPercentInDebitNote, modifyby, modifydate,modifytime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQueryCreditor(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTelephone, string AccContactPerson, string AccRemark1, string AccRemark2,
            string AccCrVisitDays, string AccShortName, double AccDiscountOffered, string AccVATTinNumber, string AccDLN, string AccStatement15Days, double AccLessPercentInDebitNote, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masteraccount";
            objQuery.AddToQuery("AccountID", Id, true);
            objQuery.AddToQuery("AccCode", AccCode);
            objQuery.AddToQuery("AccName", AccName);
            objQuery.AddToQuery("AccGroupID", AccGroupID);
            objQuery.AddToQuery("AccOpeningDebit", AccOpeningDebit);
            objQuery.AddToQuery("AccOpeningCredit", AccOpeningCredit);
            objQuery.AddToQuery("AccAddress1", AccAddress1);
            objQuery.AddToQuery("AccAddress2", AccAddress2);
            objQuery.AddToQuery("AccEmailID", AccEmailID);
            objQuery.AddToQuery("AccTelephone", AccTelephone);
            objQuery.AddToQuery("AccContactPerson", AccContactPerson);
            objQuery.AddToQuery("AccRemark1", AccRemark1);
            objQuery.AddToQuery("AccRemark2", AccRemark2);
            objQuery.AddToQuery("AccCrVisitDays", AccCrVisitDays);
            objQuery.AddToQuery("AccShortName", AccShortName);
            objQuery.AddToQuery("AccDiscountOffered", AccDiscountOffered);
            objQuery.AddToQuery("IFFIX", "N");
            objQuery.AddToQuery("AccVATTinNumber", AccVATTinNumber);
            objQuery.AddToQuery("AccDLN", AccDLN);
            objQuery.AddToQuery("AccStatement15Days", "Y");
            objQuery.AddToQuery("AccLessPercentInDebitNote", AccLessPercentInDebitNote);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
       
        private string GetUpdateQueryCreditor(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTelephone, string AccContactPerson, string AccRemark1, string AccRemark2,
            string AccCrVisitDays, string AccShortName, double AccDiscountOffered, string AccVATTinNumber, string AccDLN, string AccStatement15Days, double AccLessPercentInDebitNote, string modifyby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masteraccount";
            objQuery.AddToQuery("AccountID", Id, true);
            objQuery.AddToQuery("AccCode", AccCode);
            objQuery.AddToQuery("AccName", AccName);
            objQuery.AddToQuery("AccGroupID", AccGroupID);
            objQuery.AddToQuery("AccOpeningDebit", AccOpeningDebit);
            objQuery.AddToQuery("AccOpeningCredit", AccOpeningCredit);
            objQuery.AddToQuery("AccAddress1", AccAddress1);
            objQuery.AddToQuery("AccAddress2", AccAddress2);
            objQuery.AddToQuery("AccEmailID", AccEmailID);
            objQuery.AddToQuery("AccTelephone", AccTelephone);
            objQuery.AddToQuery("AccContactPerson", AccContactPerson);
            objQuery.AddToQuery("AccRemark1", AccRemark1);
            objQuery.AddToQuery("AccRemark2", AccRemark2);
            objQuery.AddToQuery("AccCrVisitDays", AccCrVisitDays);
            objQuery.AddToQuery("AccShortName", AccShortName);
            objQuery.AddToQuery("AccDiscountOffered", AccDiscountOffered);
            objQuery.AddToQuery("AccVATTinNumber", AccVATTinNumber);
            objQuery.AddToQuery("AccStatement15Days", AccStatement15Days);
            objQuery.AddToQuery("AccLessPercentInDebitNote", AccLessPercentInDebitNote);
            objQuery.AddToQuery("ModifiedUserID", modifyby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }

        #endregion

        #region For General

        public bool AddDetailsGnrl(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTelephone,  string AccContactPerson, string AccRemark1,string AccRemark2, string AccVATTIN,string AccPAN,string createddate,string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryGnrl(Id, AccCode, AccName, AccGroupID, AccOpeningDebit, 
                AccOpeningCredit, AccAddress1, AccAddress2,  AccEmailID, AccTelephone, AccContactPerson, AccRemark1,AccRemark2,AccVATTIN,AccPAN,createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetailsGnrl(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTelephone, string AccContactPerson, string AccRemark1,string AccRemark2, string AccVATTIN,string AccPAN,string modifydate,string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryGnrl(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2, AccEmailID, AccTelephone, AccContactPerson, AccRemark1,AccRemark2, AccVATTIN,AccPAN,modifydate,modifytime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQueryGnrl(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTelephone, string AccContactPerson, string AccRemark1, string AccRemark2, string AccVATTIN, string AccPAN,string createddate,string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masteraccount";
            objQuery.AddToQuery("AccountID", Id, true);
            objQuery.AddToQuery("AccCode", AccCode);
            objQuery.AddToQuery("AccName", AccName);
            objQuery.AddToQuery("AccGroupID", AccGroupID);
            objQuery.AddToQuery("AccOpeningDebit", AccOpeningDebit);
            objQuery.AddToQuery("AccOpeningCredit", AccOpeningCredit);
            objQuery.AddToQuery("AccAddress1", AccAddress1);
            objQuery.AddToQuery("AccAddress2", AccAddress2);
            objQuery.AddToQuery("AccEmailID", AccEmailID);
            objQuery.AddToQuery("AccTelephone", AccTelephone);
            objQuery.AddToQuery("AccContactPerson", AccContactPerson);
            objQuery.AddToQuery("AccRemark1", AccRemark1);
            objQuery.AddToQuery("AccRemark2", AccRemark2);
            objQuery.AddToQuery("AccVATTinNumber", AccVATTIN);
            objQuery.AddToQuery("AccPAN", AccPAN);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQueryGnrl(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTelephone, string AccContactPerson, string AccRemark1, string AccRemark2, string AccVATTIN, string AccPAN,string modifydate,string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masteraccount";
            objQuery.AddToQuery("AccountID", Id, true);
            objQuery.AddToQuery("AccCode", AccCode);
            objQuery.AddToQuery("AccName", AccName);
            objQuery.AddToQuery("AccGroupID", AccGroupID);
            objQuery.AddToQuery("AccOpeningDebit", AccOpeningDebit);
            objQuery.AddToQuery("AccOpeningCredit", AccOpeningCredit);
            objQuery.AddToQuery("AccAddress1", AccAddress1);
            objQuery.AddToQuery("AccAddress2", AccAddress2);
            objQuery.AddToQuery("AccEmailID", AccEmailID);
            objQuery.AddToQuery("AccTelephone", AccTelephone);
            objQuery.AddToQuery("AccContactPerson", AccContactPerson);
            objQuery.AddToQuery("AccRemark1", AccRemark1);
            objQuery.AddToQuery("AccRemark2", AccRemark2);
            objQuery.AddToQuery("AccVATTinNumber", AccVATTIN);
            objQuery.AddToQuery("AccPAN", AccPAN);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }
        #endregion

        #region For Bank

        public bool AddDetailsBank(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTelephone, string AccContactPerson, string AccRemark1, string AccRemark2, string AccBankAccountNumber,string setasDefault, string createdby, string createddate,string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryBank(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2, AccEmailID, AccTelephone, AccContactPerson, AccRemark1,AccRemark2,
                AccBankAccountNumber,setasDefault, createdby, createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetailsBank(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTelephone, string AccContactPerson, string AccRemark1, string AccRemark2, string AccBankAccountNumber, string setasDefault, string modifyby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryBank(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2, AccEmailID, AccTelephone, AccContactPerson, AccRemark1,AccRemark2,
                AccBankAccountNumber,setasDefault, modifyby, modifydate,modifytime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQueryBank(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTelephone, string AccContactPerson, string AccRemark1, string AccRemark2, string AccBankAccountNumber, string setasDefault, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masteraccount";
            objQuery.AddToQuery("AccountID", Id, true);
            objQuery.AddToQuery("AccCode", AccCode);
            objQuery.AddToQuery("AccName", AccName);
            objQuery.AddToQuery("AccGroupID", AccGroupID);
            objQuery.AddToQuery("AccOpeningDebit", AccOpeningDebit);
            objQuery.AddToQuery("AccOpeningCredit", AccOpeningCredit);
            objQuery.AddToQuery("AccAddress1", AccAddress1);
            objQuery.AddToQuery("AccAddress2", AccAddress2);
            objQuery.AddToQuery("AccEmailID", AccEmailID);
            objQuery.AddToQuery("AccTelephone", AccTelephone);
            objQuery.AddToQuery("AccContactPerson", AccContactPerson);
            objQuery.AddToQuery("AccRemark1", AccRemark1);
            objQuery.AddToQuery("AccRemark2", AccRemark2);
            objQuery.AddToQuery("AccBankAccountNumber", AccBankAccountNumber);
            objQuery.AddToQuery("AccIfOctroi", setasDefault);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQueryBank(string Id, string AccCode, string AccName, string AccGroupID,
            double AccOpeningDebit, double AccOpeningCredit, string AccAddress1, string AccAddress2, string AccEmailID,
            string AccTelephone, string AccContactPerson, string AccRemark1, string AccRemark2, string AccBankAccountNumber, string setasDefault, string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masteraccount";
            objQuery.AddToQuery("AccountID", Id, true);
            objQuery.AddToQuery("AccCode", AccCode);
            objQuery.AddToQuery("AccName", AccName);
            objQuery.AddToQuery("AccGroupID", AccGroupID);
            objQuery.AddToQuery("AccOpeningDebit", AccOpeningDebit);
            objQuery.AddToQuery("AccOpeningCredit", AccOpeningCredit);
            objQuery.AddToQuery("AccAddress1", AccAddress1);
            objQuery.AddToQuery("AccAddress2", AccAddress2);
            objQuery.AddToQuery("AccEmailID", AccEmailID);
            objQuery.AddToQuery("AccTelephone", AccTelephone);
            objQuery.AddToQuery("AccContactPerson", AccContactPerson);
            objQuery.AddToQuery("AccRemark1", AccRemark1);
            objQuery.AddToQuery("AccRemark2", AccRemark2);
            objQuery.AddToQuery("AccBankAccountNumber", AccBankAccountNumber);
            objQuery.AddToQuery("AccIfOctroi", setasDefault);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }
        #endregion

        #region Other Comman Functions
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
        public bool IsNameUnique(string AccName,string AccAddress, string Id)
        {
            bool bRetValue = false;
            string strSql = GetQueryUnique(AccName,AccAddress, Id);
            DataRow drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool IsTokenNumberUniqueForAdd(int Tokennumber, string Id)
        {
            bool bRetValue = false;
            string strSql = GetQueryUniqueTokenNumberForAdd(Tokennumber , Id);
            DataRow drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool IsTokenNumberUniqueForEdit(int Tokennumber, string Id)
        {
            bool bRetValue = false;
            string strSql = GetQueryUniqueTokenNumberForEdit(Tokennumber, Id);
            DataRow drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool IsNameUniqueForAdd(string Name, string Address, string Id)
        {
            string strSql = GetDataForUniqueForAdd(Name,Address, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool IsNameUniqueForEdit(string Name, string Address, string Id)
        {
            string strSql = GetDataForUniqueForEdit(Name,Address, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetDataForUniqueForAdd(string Name, string Address, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select AccountID from Masteraccount where AccName='{0}' && AccAddress1 = '{1}'", Name,Address);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND AccountID  in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Address, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select AccountID from Masteraccount where AccName='{0}'&& AccAddress1 = '{1}'", Name, Address);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND AccountID not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }

        public DataTable GetSSAccountHoldersList(string AccCode)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccNameAddress,AccTransactionType,AccCode,AccDoctorID,AccDiscountOffered,AccTokenNumber,AccOpeningDebit,AccOpeningCredit FROM masterAccount WHERE AccCode = '{0}' order by AccName", AccCode);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }

        public DataTable GetSSAccountHoldersListForMultiSelection(string AccCode)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccountID,AccName,AccAddress1,AccOpeningDebit,AccOpeningCredit,Tag FROM masterAccount WHERE AccCode = '{0}' order by AccName", AccCode);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }
        public DataTable GetSSAccountHoldersListForGeneralLedger()
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccNameAddress,AccTransactionType,AccCode,AccDoctorID,AccDiscountOffered,AccTokenNumber,AccOpeningDebit,AccOpeningCredit,tag FROM masterAccount WHERE AccCode != 'C' && AccCode != 'D' && AccCode != 'H' && AccCode != 'B' order by AccName");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }

        public DataTable GetAccountHoldersListForAlliedImport(string AccCode)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccountID,AccName,AccAddress1,AlliedCode,MSCDACode,GlobalID FROM masterAccount WHERE AccCode = '{0}' order by AccName", AccCode);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }
        //public DataTable GetSSAccountHoldersListForMultiSelection()
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = string.Format("SELECT AccountID,AccName,AccAddress1,AccOpeningDebit,AccOpeningCredit,Tag FROM masterAccount WHERE AccCode  != 'C' && AccCode != 'D' order by AccName");
        //    dtable = DBInterface.SelectDataTable(strSql);
        //    return dtable;

        //}
        public DataTable GetAccountsOtherThanDebtorCreditor()
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccNameAddress,AccTransactionType,AccCode,AccDoctorID,AccDiscountOffered FROM masterAccount WHERE AccCode  != 'C' && AccCode != 'D' order by AccName");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetSSAccountHoldersListforDebitNoteExpiry(string AccCode)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccountID,AccName,AccAddress1,AccAddress2,AccLessPercentInDebitNote FROM masterAccount WHERE AccCode = '{0}' order by AccName", AccCode);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }
        public DataTable GetSSOneAccount(string AccId)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccNameAddress,AccTransactionType,AccShortName,AccCode,AreaID FROM masterAccount where AccountId = "+ "'"+AccId+ "'");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }
        public DataTable GetDebtorCreditorList()
        {
            DataTable dtable = new DataTable();         
            string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccTransactionType,AccCode,AccOpeningCredit,AccClearedAmount FROM masterAccount WHERE AccCode = 'C' || AccCode = 'D'  order by AccName");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }
        public DataTable GetDebtorCreditorListForCashBankReceipt()
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccTransactionType,AccCode,AccOpeningDebit,AccClearedAmount FROM masterAccount where accCode != 'B'  && accountID != '" + FixAccounts.AccountCash + "'" +
                       "union select inwardNumber as AccName,Id as accountid, PatientName as AccAddress1, address1 as AccAddress2, '3' as AccTransactionType, 'H' As AccCode,0,0 from masterHospitalPatient  order by AccName");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }

        //public DataTable GetDebtorCreditorListForBankReceipt()
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccTransactionType,AccCode FROM masterAccount " +
        //               "union select inwardNumber as AccName,Id as accountid, PatientName as AccAddress1, address1 as AccAddress2, '3' as AccTransactionType, 'H' As AccCode from masterHospitalPatient where accountID != '"+ FixAccounts.AccountCash +"' order by AccName");
        //    dtable = DBInterface.SelectDataTable(strSql);
        //    return dtable;

        //}
        public DataTable GetDebtorCreditorPatientList()
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccTransactionType,AccCode FROM masterAccount WHERE (AccCode = 'C' || AccCode = 'D' || AccountID = '"+ FixAccounts.AccountSalesReturn +"') union select patientname,patientID,PatientAddress1,PatientAddress2,'CS','P' from masterpatient order by AccName");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }

        public DataTable GetDebtorPatientList()
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccTransactionType,AccCode FROM masterAccount WHERE (AccCode = 'D') union select patientname,patientID,PatientAddress1,PatientAddress2,'CS','P' from masterpatient order by AccName");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }

        public DataTable GetStockInOutList(string stockinoutcode)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccTransactionType,AccCode FROM masterAccount WHERE AccountID = '" + stockinoutcode + "'" );
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }
        public DataTable GetCreditorListForPayment()
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccTransactionType,AccCode,AccOpeningCredit,AccClearedAmount FROM masterAccount WHERE AccCode = 'C' order by AccName");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }
        public DataTable GetBankAccountList()
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccTransactionType,AccCode,AccIfOctroi FROM masterAccount WHERE AccCode = 'B' order by AccName");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }
        public DataRow GetDefaultBank()
        {
            DataRow dr;
            string strSql = "Select AccountID from masteraccount where AccIfOctroi = 'Y' && AccCode = 'B'";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }
        public  DataRow GetSSNameForGivenAccount(string Id)
        {
            DataRow dRow = null;          
            if (Id != "")
            {
                string strSql = string.Format("SELECT AccName,AccountID,AccAddress1,AccAddress2,AccNameAddress,AccTransactionType,AccCode,AccOpeningDebit,AccOpeningCredit FROM masterAccount where AccountId like " + "'" + Id + "%'");
              //  strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            //if (dRow != null)
            //    nm = dRow["AccName"].ToString();
            //return nm;
            return dRow;
        }

        #region Query Building Functions
   
        private string GetQueryUnique(string AccName,string AccAddress1, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select AccountID from masteraccount where AccName='{0}' && AccAddress1 = '{1}'", AccName,AccAddress1);          
            if (Id != "")
            {
                sQuery.AppendFormat(" AND AccountID not in ('{0}')", Id);                
            }
            return sQuery.ToString();
        }
        private string GetQueryUniqueTokenNumberForEdit(int TokenNumber , string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select AccountID from masteraccount where AccTokenNumber ='{0}'", TokenNumber);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND AccountID not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetQueryUniqueTokenNumberForAdd(int TokenNumber, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select AccountID from masteraccount where AccTokenNumber ='{0}'", TokenNumber);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND AccountID in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "masteraccount";
            objQuery.AddToQuery("AccountID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 

        public DataTable GetVoucherTypes()
        {
            DataTable dtable = new DataTable();
            string strSql = "SELECT * FROM tblVoucherTypes order by VoucherType";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }

        //public DataTable GetVoucherTypes(string voutype1, string voutype2, string voutype3, string voutype4)
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = string.Format("SELECT * FROM tblVoucherTypes where  code in '" + voutype1 + "','" + voutype2 + "','" + voutype3 + "','" + voutype4 +"'" );
        //    dtable = DBInterface.SelectDataTable(strSql);
        //    return dtable;

        //}
        #endregion


       
    }
}
