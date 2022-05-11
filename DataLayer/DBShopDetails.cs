using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    public class DBShopDetails
    {
        public DBShopDetails()
        {
        }
        public DataRow GetOverviewData()
        {
            DataRow dr;
            string strSql = "Select ShopOwnerName,AIOCDACode,SCORGCode,Address2,Telephone,MobileNumber,EmailID from tblShopDetails";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }
        public bool SaveDeails(string fShopOwnersName, string fAIOCDACode, string fSCORGCode, string fShopAddress2, string fShopTelephone, string fShopMobileNumber, string fShopEmailID)
        {
            bool retValue = false;
            string strSql = "Select * from tblShopDetails";
            DataRow dr = DBInterface.SelectFirstRow(strSql);
            if (dr == null)
                strSql = "Insert into tblShopDetails set ID = '1', ShopOwnerName = '" + fShopOwnersName + "' ,AIOCDACode = '" + fAIOCDACode + "' , SCORGCode = '" + fSCORGCode + "', Address2 = '"+ fShopAddress2 +"', Telephone = '"+ fShopTelephone +"', MobileNumber = '"+ fShopMobileNumber +"', EmailID = '"+ fShopEmailID +"'";
            else
            {
                string fID = dr["ID"].ToString();
                strSql = "Update tblShopDetails set ID = '1', ShopOwnerName = '" + fShopOwnersName + "' ,AIOCDACode = '" + fAIOCDACode + "' , SCORGCode = '" + fSCORGCode + "', Address2 = '" + fShopAddress2 + "', Telephone = '" + fShopTelephone + "', MobileNumber = '" + fShopMobileNumber + "', EmailID = '" + fShopEmailID + "' where ID = '"+ fID +"'";
            }

            retValue = (DBInterface.ExecuteQuery(strSql) > 0);
            return retValue;
        }
        public DataRow ReadShopDetails()
        {
            string strSql = "Select SCORGCode from tblShopDetails";
            DataRow dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }
    }
}
