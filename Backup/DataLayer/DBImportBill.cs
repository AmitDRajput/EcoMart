using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
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
                string strSql = "Select * from MasterAccount where AlliedCode ='{0}' || MSCDACode = '{0}' ";
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
    }
}
