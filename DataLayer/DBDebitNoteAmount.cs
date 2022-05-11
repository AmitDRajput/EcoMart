using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    class DBDebitNoteAmount
    {
        public DataTable GetOverviewData(string DbntType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.ID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a, masteraccount b " +
                            "where a.AccountId = b.AccountId and a.VoucherType = " + "'" + DbntType + "' && a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' && a.VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by a.voucherdate desc ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from vouchercreditdebitnote where ID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataTable ReadParticularById(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "Select ID,Particulars, Amount from detailcreditdebitnoteamount where ID = '{0}' order by SerialNumber";

                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);


            }
            return dt;
        }

        public DataRow ReadDetailsByVouNumber(int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select * from vouchercreditdebitnote where VoucherNumber='{0}' &&  VoucherType = '"+ FixAccounts.VoucherTypeForDebitNoteAmount +"'";
                strSql = string.Format(strSql, vouno);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }


        public int AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo, 
            string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, string createdby, string createddate, string createdtime)
        {
          //bool bRetValue = false;
            string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo, 
                VouDate, AmountNet, DiscPer, DiscAmt , Amt, Vat5, Vat12 , rnd,createdby,createddate,createdtime);
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

        public bool AddParticularsDetails(int Id, string Particulars, double Amount, string MyDbAmtID,int serialNumber)
        {

            bool bRetValue = false;
            string strSql = GetInsertParticularsQuery(Id, Particulars, Amount,MyDbAmtID, serialNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertParticularsQuery(int Id, string Particulars, double Amount, string MyDBAmtID, int serialNumber)
        {
           
           Query objQuery = new Query();
           objQuery.Table = "detailcreditDebitNoteAmount";
           objQuery.AddToQuery("ID", Id);
           objQuery.AddToQuery("particulars", Particulars);
           objQuery.AddToQuery("Amount", Amount);
           //objQuery.AddToQuery("DetailCreditDebitNoteAmountID", MyDBAmtID);
           objQuery.AddToQuery("SerialNumber", serialNumber);
           return objQuery.InsertQuery();
       
        }

        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, modifiedby, modifieddate, modifiedtime);


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
        public bool DeleteParticulars(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteParticularQuery(Id);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }



        #region Query Building Functions

        
        private string GetInsertQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd,string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            //objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("DiscountPer", DiscPer);
            objQuery.AddToQuery("DiscountAmount", DiscAmt);
            objQuery.AddToQuery("Amount", Amt);
            objQuery.AddToQuery("VAT5", Vat5);
            objQuery.AddToQuery("VAT12point5", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountClear", 0);
            objQuery.AddToQuery("ClearedInVoucherNumber", 0);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
            string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("ID", Id,true);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("DiscountPer", DiscPer);
            objQuery.AddToQuery("DiscountAmount", DiscAmt);
            objQuery.AddToQuery("Amount", Amt);
            objQuery.AddToQuery("VAT5", Vat5);
            objQuery.AddToQuery("VAT12point5", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("ID", Id, true);       
            strSql = objQuery.DeleteQuery();
            return strSql;
        }
        #endregion 
       

        private string GetDeleteParticularQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "detailcreditdebitnoteamount";
            objQuery.AddToQuery("ID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        public DataRow GetLastRecord(string CrdbVouType, string CrdbVouSeries)
        {
            DataRow dRow = null;

            string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' && voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber desc";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        public DataRow GetLastVoucherNumber(string vouType, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select Vouchernumber from vouchercreditdebitnote where  VoucherType =  '" + vouType + "'  &&  VoucherSeries = '" + vouSeries + "' order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetFirstRecord(string CrdbVouType, string CrdbVouSeries)
        {
            DataRow dRow = null;

            string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' && voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber ";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }
       
    }
}
