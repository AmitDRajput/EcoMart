using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;



namespace  EcoMart.DataLayer
{
    public class DBLicense
    {
        public string mShopName = "";
        public DBLicense()
        {
        }
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.CustomerID,a.ShopOwnersName,a.ShopName,a.Address1,b.LicenseDataID,b.Address2,b.Telephone,b.Mobile,b.Email,b.DrugLicenseNumber,b.DrugLicenseNumberDistributor,b.Jurisdiction,b.VATTINV,b.VATTINC,b.LBT,b.DistributorSale, b.NumberOfUsers,b.ChangeCounterSaleType,b.DebitNoteInLooseQuantity,b.BarCode,b.TransferToTally,b.CreatedDate,b.CreatedTime,b.LicenseType from tblCustomer a inner join tbllicensedata b on a.CustomerID = b.CustomerID order by ShopName,b.Createddate,b.CreatedTime";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow GetOverviewDataDetail(string customerID)
        {
            DataRow dr;
            string strSql = "Select * from tbllicensedata where CustomerID = '" + customerID + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            //if (Id != "")
            //{
            //    string strSql = "Select * from MasterArea where AreaId='{0}' for update ";
            //    strSql = string.Format(strSql, Id);
            //    dRow = DBInterface.SelectFirstRow(strSql);
            //}
            return dRow;
        }
        public bool AddDetailsCustomer(string Id, string OwnersName, string ShopName, string Address1, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string _Id = Id;
            string strSql = "Insert into tblcustomer set CustomerID = '" + Id + "', shopownersname = '" + OwnersName + "', ShopName = '" + ShopName + "', Address1 = '" + Address1 + "', CreatedDate = '" + CreatedDate + "', CreatedTime = '" + CreatedTime + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsLicense(string LicDataID, string Id, string Address2, string Telephone, string Mobile, string Email, string DLN, string Juri, string VATTINV, string VATTINC, string LBT, string DistributorSale, int NoofUsers, string LicData, int LicType, string changeCounterSaleType, string DebitNoteInLooseQuantity, string dlnDist, string barCode, string transferToTally, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = "Insert into tbllicensedata set LicenseDataID = '" + LicDataID + "',CustomerID = '" + Id + "', Address2 = '" + Address2 + "',Telephone = '" + Telephone + "', Mobile = '" + Mobile + "', Email = '" + Email + "', DrugLicenseNumber = '" + DLN + "',Jurisdiction = '" + Juri + "', VATTINV = '" + VATTINV + "',VATTINC = '" + VATTINC + "', LBT = '" + LBT + "', DistributorSale = '" + DistributorSale + "',NumberOfUsers = " + NoofUsers + ", LicenseData = '" + LicData + "', ChangeCounterSaleType = '" + changeCounterSaleType + "', DebitNoteInLooseQuantity = '" + DebitNoteInLooseQuantity + "' , LicenseType = " + LicType + ", DrugLicenseNumberDistributor = '" + dlnDist + "', BarCode = '" + barCode + "', TransferToTally = '" + transferToTally + "', CreatedDate = '" + CreatedDate + "', CreatedTime = '" + CreatedTime + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetailsCustomer(string Id, string OwnersName, string ShopName, string Address1, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string _Id = Id;
            string strSql = "Update tblcustomer set shopownersname = '" + OwnersName + "', ShopName = '" + ShopName + "', Address1 = '" + Address1 + "', CreatedDate = '" + CreatedDate + "', CreatedTime = '" + CreatedTime + "' where customerId = '" + Id + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetailsLicense(string LicDataID, string Id, string Address2, string Telephone, string Mobile, string Email, string DLN, string Juri, string VATTINV, string VATTINC, string LBT, int NoofUsers, string LicData, int LicType, string LicDLNDist, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = "Update tbllicensedata set CustomerID = '" + Id + "', Address2 = '" + Address2 + "',Telephone = '" + Telephone + "', Mobile = '" + Mobile + "', Email = '" + Email + "', DrugLicenseNumber = '" + DLN + "',Jurisdiction = '" + Juri + "', VATTINV = '" + VATTINV + "',VATTINC = '" + VATTINC + "', LBT = '" + LBT + "',  NumberOfUsers = " + NoofUsers + ", LicenseData = '" + LicData + "', LicenseType = " + LicType + ", DrugLicenseNumberDistributor = '" + LicDLNDist + "', CreatedDate = '" + CreatedDate + "', CreatedTime = '" + CreatedTime + "' where LicenseDataID = '" + LicDataID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool DeleteDetails(string Id)
        {
            bool bRetValue = false;

            return bRetValue;
        }





        #region Query Building Functions

        private string GetDataForUniqueForAdd(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select AreaId from masterarea where AreaName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND AreaId in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select AreaId from masterarea where AreaName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND AreaId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }

        //private string GetInsertQuery(string Id, string Name, string createdby, string createddate, string createdtime)
        //{
        //    Query objQuery = new Query();
        //    objQuery.Table = "MasterArea";
        //    objQuery.AddToQuery("AreaId", Id);
        //    objQuery.AddToQuery("AreaName", Name);
        //    objQuery.AddToQuery("CreatedUserID", createdby);
        //    objQuery.AddToQuery("CreatedDate", createddate);
        //    objQuery.AddToQuery("CreatedTime", createdtime);
        //    return objQuery.InsertQuery();
        //}

        //private string GetUpdateQuery(string Id, string Name, string modifiedby, string modifydate, string modifytime)
        //{
        //    Query objQuery = new Query();
        //    objQuery.Table = "MasterArea";
        //    objQuery.AddToQuery("AreaId", Id, true);
        //    objQuery.AddToQuery("AreaName", Name);
        //    objQuery.AddToQuery("ModifiedUserID", modifiedby);
        //    objQuery.AddToQuery("ModifiedDate", modifydate);
        //    objQuery.AddToQuery("ModifiedTime", modifytime);
        //    return objQuery.UpdateQuery();
        //}

        //private string GetDeleteQuery(string Id)
        //{
        //    string strSql = "";

        //    Query objQuery = new Query();
        //    objQuery.Table = "MasterArea";
        //    objQuery.AddToQuery("AreaId", Id, true);
        //    strSql = objQuery.DeleteQuery();

        //    return strSql;
        //}
        #endregion private methods
    }
}
