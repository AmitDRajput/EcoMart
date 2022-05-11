using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBPatient
    {
        public DBPatient()
        {
        }

        #region GetRead
        //public DataTable GetOverviewData()
        //{
        //    DataTable dtable = new DataTable();
        //    string strsql = "Select a.PatientID,a.PatientName,a.PatientAddress1,a.PatientAddress2," +
        //                    "a.TelephoneNumber,a.Email,a.BirthDay,a.BirthMonth,a.MobileNumberForSMS,a.BirthYear," +
        //                    "a.VisitDay1,a.VisitDay2,a.VisitDay3,a.ShortNameAddress,a.DoctorID,a.Remark1,a.Remark2,a.Remark3,a.TokenNumber,a.Acccode,a.DiscountOffered,a.MobileNumberForSMS,b.DocID,b.DocName from masterpatient a  left outer join masterdoctor b on a.DoctorID = b.DocID where PatientName != '' order by PatientName";

        //    dtable = DBInterface.SelectDataTable(strsql);
        //    return dtable;
        //}
        //public DataTable GettxtAddress1()
        //{
        //    DataTable dtable = new DataTable();
        //    string strsql = "select distinct PatientAddress1 from masterpatient where PatientAddress1 IS NOT NULL order by PatientAddress1";

        //    dtable = DBInterface.SelectDataTable(strsql);
        //    return dtable;
        //}
        //public DataTable GettxtAddress2()
        //{
        //    DataTable dtable = new DataTable();
        //    string strsql = "select distinct PatientAddress2 from masterpatient where PatientAddress2 IS NOT NULL order by PatientAddress2";

        //    dtable = DBInterface.SelectDataTable(strsql);
        //    return dtable;
        //}
        //public DataTable GetOverviewDataForCounterSale()
        //{
        //    DataTable dtable = new DataTable();
        //    string strsql = "Select PatientID,AccCode,PatientName,PatientAddress1,PatientAddress2," +
        //                    "ShortNameAddress,DoctorID,1 as AccTransactionType, DiscountOffered,MobileNumberForSMS from masterpatient " +
        //                    "union select AccountID as PatientID,Acccode,AccName as PatientName,AccAddress1 as PatientAddress1,AccAddress2 as PatientAddress2, AccDoctorID as DoctorID,AccTransactionType, accdiscountoffered as DiscountOffered,MobileNumberForSMS from MasterAccount where acccode = 'D' order by PatientName ";

        //    dtable = DBInterface.SelectDataTable(strsql);
        //    return dtable;
        //}
        //public DataTable GetOverviewDataForCounterSaleForOnlyCashSale()
        //{
        //    DataTable dtable = new DataTable();
        //    string strsql = "Select PatientID,AccCode,PatientName,PatientAddress1,PatientAddress2," +
        //                    "ShortNameAddress,DoctorID,1 as AccTransactionType, DiscountOffered,MobileNumberForSMS from masterpatient " +
        //                    "order by PatientName ";

        //    dtable = DBInterface.SelectDataTable(strsql);
        //    return dtable;
        //}
        //public DataTable GetPatient()
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = "Select PatientID,PatientName,PatientAddress1,PatientAddress2," +
        //                    "TelephoneNumber,Email,BirthDay,BirthMonth,BirthYear," +
        //                    "VisitDay1,VisitDay2,VisitDay3,ShortNameAddress,DoctorID,Remark1,Remark2,Remark3 from masterpatient order by PatientName";


        //    dtable = DBInterface.SelectDataTable(strSql);
        //    return dtable;
        //}

        //public DataRow ReadDetailsByID(string Id)
        //{
        //    DataRow dRow = null;
        //    if (Id != "")
        //    {
        //        string strsql = "Select PatientID,PatientName,PatientAddress1,PatientAddress2," +
        //                    "TelephoneNumber,Email,BirthDay,BirthMonth,BirthYear,TokenNumber,Gender," +
        //                    "VisitDay1,VisitDay2,VisitDay3,ShortNameAddress,DoctorID,Remark1,Remark2,Remark3,DiscountOffered,MobileNumberForSMS,PutInBlackList from masterpatient  where PatientID= '{0}'";

        //        strsql = string.Format(strsql, Id);
        //        dRow = DBInterface.SelectFirstRow(strsql);
        //    }
        //    return dRow;
        //}

        public bool DeleteProductsByID(string Id)
        {
            bool bRetValue = false;

            if (Id != "")
            {

                string strsql = "Delete from linkpatientproduct where PatientID= '{0}'";

                strsql = string.Format(strsql, Id);
                if (DBInterface.ExecuteQuery(strsql) > 0)
                {
                    bRetValue = true;
                }


            }
            return bRetValue;

        }

        //public DataRow ReadPatientDataByTokenNumber(int TokenNumber)
        //{
        //    string strSql = "";
        //    DataRow dRow = null;
        //    if (TokenNumber != 0)
        //    {
        //        strSql = "Select * from masterpatient where TokenNumber = " + TokenNumber;
        //        dRow = DBInterface.SelectFirstRow(strSql);
        //    }
        //    return dRow;
        //}
        public DataTable ReadProductDetailsById(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "Select a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdCompShortName,a.ProdClosingStock,b.quantity,b.patientID,b.productID " +
                                "from linkpatientproduct  b ,masterproduct a  where b.productID = a.ProductId  &&  " +
                                  " b.patientID = '{0}'";

                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);


            }
            return dt;
        }

        public DataRow ReadDoctorDetailsByID(string DoctorId)
        {
            DataRow docRow = null;
            if (DoctorId != "")
            {
                string strsql = "Select DocID,DocName,DocAddress," +
                            "DocTelephone,DocEmailID from masterdoctor  where DocID= '{0}'";

                strsql = string.Format(strsql, DoctorId);
                docRow = DBInterface.SelectFirstRow(strsql);
            }
            return docRow;
        }
        //public DataTable GetTodaysBirthdayPatient(DateTime date)
        //{
        //    DataTable dt = null;
        //    if (date != null)
        //    {
        //        int Day = date.Day;
        //        int Month = date.Month;               
        //        string strsql = "Select * From masterpatient where BirthMonth = {0} And BirthDay = {1}";

        //        strsql = string.Format(strsql,Month, Day);
        //        dt = DBInterface.SelectDataTable(strsql);
        //    }
        //    return dt;
        //}
        #endregion GetRead

        #region AddUpdateDelete

        //public bool AddDetails(string Id, string PatientName,
        //     string Address1, string Address2, string Telephone,
        //    string Email, int BDay, int BMonth, int BYear, int Visit1, int Visit2, int Visit3,
        //    string ShortNameAddres, string DoctorID, string Remark1, string Remark2, string Remark3, int tokannumber, string Gender, double discountOffered, string MobileNumberForSMS, string putInBlackList, string createdby, string createddate, string createdtime)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetInsertQuery(Id, PatientName,
        //       Address1, Address2, Telephone, Email, BDay, BMonth, BYear,
        //       Visit1, Visit2, Visit3, ShortNameAddres, DoctorID, Remark1, Remark2, Remark3, tokannumber, Gender, discountOffered, MobileNumberForSMS, putInBlackList, createdby, createddate, createdtime);

        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}
              

        public bool AddProductDetails(string Id, string detailID, string ProductID, int Quantity)
        {

            bool bRetValue = false;
            string strSql = GetInsertProductQuery(Id, detailID, ProductID, Quantity);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }


        //public bool UpdateDetails(string Id, string PatientName,
        //     string Address1, string Address2, string Telephone,
        //    string Email, int BDay, int BMonth, int BYear, int Visit1, int Visit2, int Visit3,
        //    string ShortNameAddres, string DoctorID, string Remark1, string Remark2, string Remark3, int tokannumber, string Gender, double discountOffered, string MobileNumberForSMS, string putInBlackList, string modifiedby, string modifieddate, string modifiedtime)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetUpdateQuery(Id, PatientName,
        //       Address1, Address2, Telephone, Email, BDay, BMonth, BYear,
        //       Visit1, Visit2, Visit3, ShortNameAddres, DoctorID, Remark1, Remark2, Remark3, tokannumber, Gender, discountOffered, MobileNumberForSMS, putInBlackList, modifiedby, modifieddate, modifiedtime);
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}

        //private string GetInsertQuery(string Id, string PatientName,
        //     string PatientAddress1, string PatientAddress2, string TelephoneNumber,
        //    string Email, int BirthDay, int BirthMonth, int BirthYear, int VisitDay1, int VisitDay2, int VisitDay3,
        //    string ShortNameAddres, string DoctorID, string Remark1, string Remark2, string Remark3, int tokennumber, string Gender, double discountOffered, string MobileNumberForSMS, string putInBlackList, string createdby, string createddate, string createdtime)
        //{
        //    Query objQuery = new Query();
        //    objQuery.Table = "masterpatient";
        //    objQuery.AddToQuery("PatientID", Id);
        //    objQuery.AddToQuery("AccCode", FixAccounts.AccCodeForPatient);
        //    objQuery.AddToQuery("PatientName", PatientName);
        //    objQuery.AddToQuery("PatientAddress1", PatientAddress1);
        //    objQuery.AddToQuery("PatientAddress2", PatientAddress2);
        //    objQuery.AddToQuery("TelephoneNumber", TelephoneNumber);
        //    objQuery.AddToQuery("Email", Email);
        //    objQuery.AddToQuery("BirthDay", BirthDay);
        //    objQuery.AddToQuery("BirthMonth", BirthMonth);
        //    objQuery.AddToQuery("BirthYear", BirthYear);
        //    objQuery.AddToQuery("VisitDay1", VisitDay1);
        //    objQuery.AddToQuery("VisitDay2", VisitDay2);
        //    objQuery.AddToQuery("VisitDay3", VisitDay3);
        //    objQuery.AddToQuery("ShortNameAddress", ShortNameAddres);
        //    objQuery.AddToQuery("DoctorID", DoctorID);
        //    objQuery.AddToQuery("Remark1", Remark1);
        //    objQuery.AddToQuery("Remark2", Remark2);
        //    objQuery.AddToQuery("Remark3", Remark3);
        //    objQuery.AddToQuery("TokenNumber", tokennumber);
        //    objQuery.AddToQuery("Gender", Gender);
        //    objQuery.AddToQuery("DiscountOffered", discountOffered);
        //    objQuery.AddToQuery("MobileNumberForSMS", MobileNumberForSMS);
        //    objQuery.AddToQuery("PutInBlackList", putInBlackList);
        //    objQuery.AddToQuery("CreatedUserID", createdby);
        //    objQuery.AddToQuery("CreatedDate", createddate);
        //    objQuery.AddToQuery("CreatedTime", createdtime);
        //    return objQuery.InsertQuery();
        //}

        private string GetInsertProductQuery(string Id, string detailID, string ProductID, int Quantity)
        {
            Query objQuery = new Query();
            objQuery.Table = "linkpatientproduct";
            objQuery.AddToQuery("LinkPatientProductID", detailID);
            objQuery.AddToQuery("patientID", Id);
            objQuery.AddToQuery("productID", ProductID);
            objQuery.AddToQuery("quantity", Quantity);
            return objQuery.InsertQuery();
        }


        //private string GetUpdateQuery(string Id, string PatientName,
        //     string PatientAddress1, string PatientAddress2, string TelephoneNumber,
        //    string Email, int BirthDay, int BirthMonth, int BirthYear, int VisitDay1, int VisitDay2, int VisitDay3,
        //    string ShortNameAddres, string DoctorID, string Remark1, string Remark2, string Remark3, int tokennumber, string Gender, double discountOffered, string MobileNumberForSMS, string putInBlackList, string modifiedby, string modifieddate, string modifiedtime)
        //{
        //    Query objQuery = new Query();
        //    objQuery.Table = "masterpatient";
        //    objQuery.AddToQuery("PatientID", Id, true);
        //    objQuery.AddToQuery("AccCode", FixAccounts.AccCodeForPatient);
        //    objQuery.AddToQuery("PatientName", PatientName);
        //    objQuery.AddToQuery("PatientAddress1", PatientAddress1);
        //    objQuery.AddToQuery("PatientAddress2", PatientAddress2);
        //    objQuery.AddToQuery("TelephoneNumber", TelephoneNumber);
        //    objQuery.AddToQuery("Email", Email);
        //    objQuery.AddToQuery("BirthDay", BirthDay);
        //    objQuery.AddToQuery("BirthMonth", BirthMonth);
        //    objQuery.AddToQuery("BirthYear", BirthYear);
        //    objQuery.AddToQuery("VisitDay1", VisitDay1);
        //    objQuery.AddToQuery("VisitDay2", VisitDay2);
        //    objQuery.AddToQuery("VisitDay3", VisitDay3);
        //    objQuery.AddToQuery("ShortNameAddress", ShortNameAddres);
        //    objQuery.AddToQuery("DoctorID", DoctorID);
        //    objQuery.AddToQuery("Remark1", Remark1);
        //    objQuery.AddToQuery("Remark2", Remark2);
        //    objQuery.AddToQuery("Remark3", Remark3);
        //    objQuery.AddToQuery("TokenNumber", tokennumber);
        //    objQuery.AddToQuery("Gender", Gender);
        //    objQuery.AddToQuery("DiscountOffered", discountOffered);
        //    objQuery.AddToQuery("MobileNumberForSMS", MobileNumberForSMS);
        //    objQuery.AddToQuery("PutInBlackList", putInBlackList);
        //    objQuery.AddToQuery("ModifiedUserID", modifiedby);
        //    objQuery.AddToQuery("ModifiedDate", modifieddate);
        //    objQuery.AddToQuery("ModifiedTime", modifiedtime);
        //    return objQuery.UpdateQuery();
        //}



        //public bool DeleteDetails(string Id)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetDeleteQuery(Id);

        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}
        #endregion AddUpdateDelete

        #region Query Building Functions

        //public bool IsNameUniqueForAdd(string Name, string Id)
        //{
        //    string strSql = GetDataForUniqueForAdd(Name, Id);
        //    bool bRetValue = false;
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}

        //public bool IsNameUniqueForEdit(string Name, string Id)
        //{
        //    string strSql = GetDataForUniqueForEdit(Name, Id);
        //    bool bRetValue = false;
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}
        //public bool IsTokenNumberUniqueForAdd(int Tokennumber, string Id)
        //{
        //    bool bRetValue = false;
        //    if (Tokennumber > 0)
        //    {
        //        string strSql = GetQueryUniqueTokenNumberForAdd(Tokennumber, Id);
        //        DataRow drow = DBInterface.SelectFirstRow(strSql);
        //        if (drow != null)
        //            bRetValue = true;
        //    }
        //    else
        //        bRetValue = true;

        //    return bRetValue;
        //}
        //public bool IsTokenNumberUniqueForEdit(int Tokennumber, string Id)
        //{
        //    bool bRetValue = false;
        //    if (Tokennumber > 0)
        //    {
        //        string strSql = GetQueryUniqueTokenNumberForEdit(Tokennumber, Id);
        //        DataRow drow = DBInterface.SelectFirstRow(strSql);
        //        if (drow != null)
        //            bRetValue = true;
        //    }
        //    else
        //        bRetValue = true;

        //    return bRetValue;
        //}
        //private string GetDeleteQuery(string Id)
        //{
        //    string strSql = "";

        //    Query objQuery = new Query();
        //    objQuery.Table = "MasterPatient";
        //    objQuery.AddToQuery("PatientID", Id, true);
        //    strSql = objQuery.DeleteQuery();

        //    return strSql;
        //}
        //private string GetDataForUniqueForAdd(string Name, string Id)
        //{
        //    StringBuilder sQuery = new StringBuilder();
        //    sQuery.AppendFormat("Select PatientId from Masterpatient where PatientName='{0}'", Name);
        //    if (Id != "")
        //    {
        //        sQuery.AppendFormat(" AND PatientID  in ('{0}')", Id);
        //    }
        //    return sQuery.ToString();
        //}
        //private string GetDataForUniqueForEdit(string Name, string Id)
        //{
        //    StringBuilder sQuery = new StringBuilder();
        //    sQuery.AppendFormat("Select PatientId from Masterpatient where PatientName='{0}'", Name);
        //    if (Id != "")
        //    {
        //        sQuery.AppendFormat(" AND PatientID not in ('{0}')", Id);
        //    }
        //    return sQuery.ToString();
        //}
        //private string GetQueryUniqueTokenNumberForAdd(int TokenNumber, string Id)
        //{
        //    StringBuilder sQuery = new StringBuilder();
        //    sQuery.AppendFormat("Select PatientId from Masterpatient where TokenNumber ='{0}'", TokenNumber);
        //    if (Id != "")
        //    {
        //        sQuery.AppendFormat(" AND PatientID in ('{0}')", Id);
        //    }
        //    return sQuery.ToString();
        //}

        //private string GetQueryUniqueTokenNumberForEdit(int TokenNumber, string Id)
        //{
        //    StringBuilder sQuery = new StringBuilder();
        //    sQuery.AppendFormat("Select PatientId from Masterpatient where TokenNumber ='{0}'", TokenNumber);
        //    if (Id != "")
        //    {
        //        sQuery.AppendFormat(" AND PatientID not in ('{0}')", Id);
        //    }
        //    return sQuery.ToString();
        //}
        //public DataTable GetOverviewDataForCounterSaleForOnlyCashSaleCheck(string patientID)  //Amar
        //{
        //    DataTable dtable = new DataTable();
        //    string strsql = "select PatientID from masterpatient where  patientId= '" + patientID + "' ";

        //    dtable = DBInterface.SelectDataTable(strsql);
        //    return dtable;
        //}
        //public bool UpdateMobileNumberInMasterPatient(string Mobno, string PatientID)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetQueryForMobileNoInMasterPatient(Mobno, PatientID);
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}

        //public string GetQueryForMobileNoInMasterPatient(string Mobno, string PatientID)
        //{
        //    Query objquery = new Query();
        //    objquery.Table = "masterpatient";
        //    objquery.AddToQuery("PatientID", PatientID, true);
        //    objquery.AddToQuery("MobileNumberForSMS", Mobno);
        //    return objquery.UpdateQuery();
        //}
        #endregion

    }
}
