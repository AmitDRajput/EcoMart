using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
{
    public  class DBHospitalPatient
    {

        public DBHospitalPatient()
        {
        }

        #region GetRead
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ID,a.InwardNumber,a.PatientName,a.Address1,a.Address2," +
                            "a.Telephone,a.Email,a.RoomNumber,a.IDNumber,a.BirthDay,a.BirthMonth,a.BirthYear," +
                            "a.wardID,a.ShortNameAddress,a.DoctorID,a.Remark1,a.IDNumber,b.DocID,b.DocName from masterhospitalpatient a  left outer join masterdoctor b on a.DoctorID = b.DocID order by PatientName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }

        public DataTable GetOverviewDataForStatement()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ID,a.InwardNumber,a.PatientName,a.Address1,a.Address2," +
                            "a.Telephone,a.Email,a.RoomNumber,a.IDNumber,a.BirthDay,a.BirthMonth,a.BirthYear," +
                            "a.wardID,a.ShortNameAddress,a.DoctorID,a.Remark1,a.IDNumber from masterhospitalpatient a order by PatientName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }


        //public DataTable GetOverviewDataForCounterSale()
        //{
        //    DataTable dtable = new DataTable();
        //    string strsql = "Select PatientID,AccCode,PatientName,PatientAddress1,PatientAddress2," +
        //                    "ShortNameAddress,DoctorID,1 as AccTransactionType from masterhospitalpatient " +
        //                    "union select AccountID as PatientID,Acccode,AccName as PatientName,AccAddress1 as PatientAddress1,AccAddress2 as PatientAddress2,AccNameAddress as ShortNameAddress , AccDoctorID as DoctorID,AccTransactionType from MasterAccount where acccode = 'D' order by PatientName ";

        //    dtable = DBInterface.SelectDataTable(strsql);
        //    return dtable;
        //}

        public DataTable GetPatient()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select * from masterHospitalpatient order by InWardNumber";


            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strsql = "Select * from masterHospitalpatient  where ID= '{0}'";

                strsql = string.Format(strsql, Id);
                dRow = DBInterface.SelectFirstRow(strsql);
            }
            return dRow;
        }


        public DataRow ReadPatientDataByTokenNumber(string IDNumber)
        {
            string strSql = "";
            DataRow dRow = null;
            if (IDNumber != "")
            {
                strSql = "Select * from masterHospitalpatient where IDNumber = '" + IDNumber + "'";        
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
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

        #endregion GetRead

        #region AddUpdateDelete

        public bool AddDetails(string Id, string Inwardno, string PatientName, string Address1, string Address2, string Telephone, string Email,
             string ShortNameAddres, int BDay, int BMonth, int BYear,int Ageyears, int Agemonths, int Agedays,string Gender, string Roomnumber, string DoctorID,
             string Remark1,string Remark2, string Remark3,string IDNumber, string CompanyID,string WardID, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id,Inwardno, PatientName,Address1,Address2,Telephone,Email,
                    ShortNameAddres, BDay, BMonth, BYear,Ageyears,Agemonths,Agedays,Gender,Roomnumber,DoctorID,
                    Remark1,Remark2,Remark3,IDNumber,CompanyID,WardID, createdby,createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }


        public bool UpdateDetails(string Id, string Inwardno, string PatientName, string Address1, string Address2, string Telephone, string Email,
             string ShortNameAddres, int BDay, int BMonth, int BYear, int Ageyears, int Agemonths, int Agedays, string Gender, string Roomnumber, string DoctorID,
             string Remark1, string Remark2, string Remark3, string IDNumber, string CompanyID, string WardID, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Inwardno, PatientName, Address1, Address2, Telephone, Email,
                    ShortNameAddres, BDay, BMonth, BYear, Ageyears, Agemonths, Agedays, Gender, Roomnumber, DoctorID,
                    Remark1, Remark2, Remark3, IDNumber, CompanyID, WardID, modifiedby, modifieddate, modifiedtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQuery(string Id, string Inwardno, string PatientName, string Address1, string Address2, string Telephone, string Email,
             string ShortNameAddres, int BDay, int BMonth, int BYear, int Ageyears, int Agemonths, int Agedays, string Gender, string Roomnumber, string DoctorID,
             string Remark1, string Remark2, string Remark3, string IDNumber, string AccountID,string WardID, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterHospitalpatient";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("InwardNumber", Inwardno);
            objQuery.AddToQuery("PatientName", PatientName);
            objQuery.AddToQuery("Address1", Address1);
            objQuery.AddToQuery("Address2", Address2);
            objQuery.AddToQuery("Telephone", Telephone);
            objQuery.AddToQuery("Email", Email);
            objQuery.AddToQuery("ShortNameAddress", ShortNameAddres);
            objQuery.AddToQuery("BirthDay", BDay);
            objQuery.AddToQuery("BirthMonth", BMonth);
            objQuery.AddToQuery("BirthYear", BYear);
            objQuery.AddToQuery("AgeYears",Ageyears);
            objQuery.AddToQuery("AgeMonths",Agemonths);
            objQuery.AddToQuery("AgeDays", Agedays);
            objQuery.AddToQuery("Gender", Gender);
            objQuery.AddToQuery("RoomNumber", Roomnumber);
            objQuery.AddToQuery("WardID", WardID);
            objQuery.AddToQuery("DoctorID", DoctorID);
            objQuery.AddToQuery("AccountID", AccountID);
            objQuery.AddToQuery("IDNumber", IDNumber);
            objQuery.AddToQuery("Remark1", Remark1);
            objQuery.AddToQuery("Remark2", Remark2);
            objQuery.AddToQuery("Remark3", Remark3);   
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Inwardno, string PatientName, string Address1, string Address2, string Telephone, string Email,
             string ShortNameAddres, int BDay, int BMonth, int BYear, int Ageyears, int Agemonths, int Agedays, string Gender, string Roomnumber, string DoctorID,
             string Remark1, string Remark2, string Remark3, string IDNumber, string AccountID, string WardID, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();           
            objQuery.Table = "masterHospitalpatient";
            objQuery.AddToQuery("ID", Id,true);
            objQuery.AddToQuery("InwardNumber", Inwardno);
            objQuery.AddToQuery("PatientName", PatientName);
            objQuery.AddToQuery("Address1", Address1);
            objQuery.AddToQuery("Address2", Address2);
            objQuery.AddToQuery("Telephone", Telephone);
            objQuery.AddToQuery("Email", Email);
            objQuery.AddToQuery("ShortNameAddress", ShortNameAddres);
            objQuery.AddToQuery("BirthDay", BDay);
            objQuery.AddToQuery("BirthMonth", BMonth);
            objQuery.AddToQuery("BirthYear", BYear);
            objQuery.AddToQuery("AgeYears", Ageyears);
            objQuery.AddToQuery("AgeMonths", Agemonths);
            objQuery.AddToQuery("AgeDays", Agedays);
            objQuery.AddToQuery("Gender", Gender);
            objQuery.AddToQuery("RoomNumber", Roomnumber);
            objQuery.AddToQuery("WardID", WardID);
            objQuery.AddToQuery("DoctorID", DoctorID);
            objQuery.AddToQuery("AccountID", AccountID);
            objQuery.AddToQuery("IDNumber", IDNumber);
            objQuery.AddToQuery("Remark1", Remark1);
            objQuery.AddToQuery("Remark2", Remark2);
            objQuery.AddToQuery("Remark3", Remark3);   
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
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
        #endregion AddUpdateDelete

        #region Query Building Functions

        public bool IsNameUniqueForAdd(string Name, string Id)
        {
            string strSql = GetDataForUniqueForAdd(Name, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool IsNameUniqueForEdit(string Name, string Id)
        {
            string strSql = GetDataForUniqueForEdit(Name, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }   

        public bool IsIDNumberUnique(string IDNumber, string Id)
        {
            bool bRetValue = false;
            if (IDNumber!= "")
            {
                string strSql = GetQueryUniqueIDNumber(IDNumber, Id);
                DataRow drow = DBInterface.SelectFirstRow(strSql);
                if (drow != null)
                    bRetValue = true;
            }
            else
                bRetValue = true;

            return bRetValue;
        }
        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "masterHospitalpatient";
            objQuery.AddToQuery("ID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        private string GetDataForUniqueForAdd(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select Id from masterHospitalpatient where PatientName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND ID  in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select Id from masterHospitalpatient where PatientName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND ID not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }      
        private string GetQueryUniqueIDNumber(string IDNumber, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select IDNumber from masterHospitalpatient where IDNumber ='{0}'", IDNumber);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND ID not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        #endregion
    }
}
