using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    public class DBImportBill
    {
        public DBImportBill()
        {
        }
        public DataRow ReadPartyDetailsByAlliedID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from MasterAccount where AlliedCode ='{0}' || AIOCDACode = '{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadPurchaseFor(string partyCode, string partyBillNumber, string voucherType)
        {
            
            DataRow dr = null;
            string strSql = "Select * from VoucherPurchase where AccountID ='{0}' && PurchaseBillNumber = '{1}' && VoucherType = '{2}'";
            strSql = string.Format(strSql, partyCode,partyBillNumber,voucherType);
            dr = DBInterface.SelectFirstRow(strSql);
            
            return dr;
            
        }

        //public DataRow GetCompanyIDFromAIOCDACode(string AIOCDACode)
        //{
        //    DataRow dr = null;
        //    string strSql = "Select AccountID from masterAccount where AIOCDACode ='{0}' ";
        //    strSql = string.Format(strSql, AIOCDACode);
        //    dr = DBInterface.SelectFirstRow(strSql);

        //    return dr;
        //}

        //public DataRow GetAIOCDACode(string DistCode)
        //{
        //    DataRow dr = null;
        //    string strSql = "Select AIOCDACode from masterAccount where AccountID ='{0}' ";
        //    strSql = string.Format(strSql, DistCode);
        //    dr = DBInterface.SelectFirstRow(strSql);
        //    return dr;
        //}

        public DataRow GetRetailerProductIDFromCompanyProductID(string CompanyID, string CompanyProductID)
        {
            DataRow dr = null;
            string strSql = "Select EcoMartProductID from tblbillimportlink where CompanyProductID ='{0}' and CompanyID = '{1}'";
            strSql = string.Format(strSql, CompanyProductID,CompanyID);
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }

        public DataRow CheckIFProductIDIsNOTLinked(string _CompanyID, string _DistProductID, string _ProductID)
        {
            DataRow dr = null;
            string strSql = "Select * from tblbillimportlink where CompanyID = '{0}'  and CompanyProductID = '{1}'";
            strSql = string.Format(strSql, _CompanyID, _DistProductID);
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr == null)
            {
                strSql = "Select * from tblbillimportlink where CompanyID = '{0}'  and EcoMartProductID = '{1}'";
                strSql = string.Format(strSql, _CompanyID, _ProductID);
                dr = DBInterface.SelectFirstRow(strSql);                
            }
            return dr;
        }

        public DataRow GetCompanyIDFromCompanyCode(string p)
        {
            DataRow dr = null;
            //string strSql = "Select AccountID from masterAccount where AlliedCode ='{0}' ";
            //strSql = string.Format(strSql,p);
            //dr = DBInterface.SelectFirstRow(strSql);

            return dr;
        }
    }
}
