using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    public class DBChangeVAT5to55
    {
        public DBChangeVAT5to55()
        {

        }
        public bool UpdateProductMaster()
        {
            bool bRetValue = false;
            string strSql = "update masterproduct set ProdVATPercent = 6 where ProdVATPercent = 5.5";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            strSql = "update masterproduct set ProdVATPercent = 13.5 where ProdVATPercent = 12.5";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            strSql = "update masterproduct set ProdLastPurchaseVATPer = 6 where ProdLastPurchaseVATPer = 5.5";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "update masterproduct set ProdLastPurchaseVATPer = 13.5 where ProdLastPurchaseVATPer = 12.5";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdatetblStock()
        {
            bool bRetValue = false;
            string strSql = "update tblstock set ProductVATPercent = 6 where ProductVATPercent = 5.5";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "update tblstock set ProductVATPercent = 13.5 where ProductVATPercent = 12.5";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "update tblstock set PurchaseVATPercent = 6 where PurchaseVATPercent = 5.5";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "update tblstock set PurchaseVATPercent = 13.5 where PurchaseVATPercent = 12.5";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateMasterVATPercent()
        {
            bool bRetValue = false;
            string strSql = "update mastervatpercent set vatpercent = 6 where vatpercent = 5.5";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "update mastervatpercent set vatpercent = 13.5 where vatpercent = 12.5";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }        
            return bRetValue;
        }

        public bool UpdateAccNameInmasterAccount()
        {
            bool bRetValue = false;
            //string strSql = "";
           //string strSql = "update masteraccount set accname = 'VAT OUTPUT 5.5 PERCENT' where accountID = '30009' ";

            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}

         //   strSql = "update masteraccount set accname = 'VAT INPUT 5.5 PERCENT' where accountID = '40012' ";

            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}

            //DataRow dr = null;
            //strSql = "Select * from masteraccount where AccountID = '30025'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into masteraccount (AccountID,acccode,accname,accopeningdebit,accopeningcredit,accgroupID) values ('30025','S','VAT OUTPUT 6 PERCENT',0,0,'63')";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}
            //dr = null;
            //strSql = "Select * from masteraccount where AccountID = '30026'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into masteraccount (AccountID,acccode,accname,accopeningdebit,accopeningcredit,accgroupID) values ('30026','S','VAT OUTPUT 13.5 PERCENT',0,0,'63')";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}
            //dr = null;
            //strSql = "Select * from masteraccount where AccountID = '40024'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into masteraccount (AccountID,acccode,accname,accopeningdebit,accopeningcredit,accgroupID) values ('40024','P','VAT INPUT 6 PERCENT',0,0,'156')";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}
            //dr = null;
            //strSql = "Select * from masteraccount where AccountID = '40025'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into masteraccount (AccountID,acccode,accname,accopeningdebit,accopeningcredit,accgroupID) values ('40025','P','VAT INPUT 13.5 PERCENT',0,0,'155')";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}

            //dr = null;
            //strSql = "Select * from masteraccount where AccountID = '40022'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into masteraccount (AccountID,acccode,accname,accopeningdebit,accopeningcredit,accgroupID) values ('40022','P','PURCHASE 6 PERCENT',0,0,'156')";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}

            //dr = null;
            //strSql = "Select * from masteraccount where AccountID = '40023'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into masteraccount (AccountID,acccode,accname,accopeningdebit,accopeningcredit,accgroupID) values ('40023','P','PURCHASE 13.5 PERCENT',0,0,'155') ";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}

            //dr = null;
            //strSql = "Select * from masteraccount where AccountID = '30016'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into masteraccount (AccountID,acccode,accname,accopeningdebit,accopeningcredit,accgroupID) values ('30016','S','DISCOUNT IN CASH BANK',0,0,'63' )";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}

            //dr = null;
            //strSql = "Select * from masteraccount where AccountID = '30017'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into masteraccount (AccountID,acccode,accname,accopeningdebit,accopeningcredit,accgroupID) values ('30017','S','CREDIT CARD SALE',0,0,'63' )";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}

            // dr = null;
            //strSql = "Select * from masteraccount where AccountID = '30022'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into masteraccount (AccountID,acccode,accname,accopeningdebit,accopeningcredit,accgroupID) values ('30022','S','SALE 6 PER VAT',0,0,'63' )";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}

            // dr = null;
            //strSql = "Select * from masteraccount where AccountID = '30023'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into masteraccount (AccountID,acccode,accname,accopeningdebit,accopeningcredit,accgroupID) values ('30023','S','SALE 13.5 PER VAT',0,0,'63' )";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}

            //dr = null;
            //strSql = "Select * from mastergroup where GroupID = '155'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into mastergroup (GroupID,Groupname,GroupCode,UnderGroupId,undergroupIDparentID,IFFIX) values ('155','PURCHASE FOR 13.5 PER VAT','G','56','11','Y')";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}

            //   dr = null;
            //   strSql = "Select * from mastergroup where GroupID = '156'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into mastergroup (GroupID,Groupname,GroupCode,UnderGroupId,undergroupIDparentID,IFFIX) values ('156','PURCHASE FOR 6 PER VAT','G','56','11','Y')";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}

            //dr = null;
            //strSql = "Select * from mastergroup where GroupID = '02000'";
            //dr = DBInterface.SelectFirstRow(strSql);
            //if (dr == null)
            //{
            //    strSql = "insert into mastergroup (GroupID,Groupname,GroupCode,UnderGroupId,undergroupIDparentID,IFFIX) values ('02000','CLOSING STOCK','G','','','Y')";

            //    if (DBInterface.ExecuteQuery(strSql) > 0)
            //    {
            //        bRetValue = true;
            //    }
            //}


            
                //strSql = "update  tbltrnac inner join vouchersale on tbltrnac.VoucherID = vouchersale.id  set tbltrnac.transactiondate =  vouchersale.VoucherDate , tbltrnac.VoucherType = vouchersale.vouchertype , tbltrnac.VoucherSubType = vouchersale.VoucherSubType , tbltrnac.vouchernumber = vouchersale.VoucherNumber where  tbltrnac.transactiondate = ''";

                //if (DBInterface.ExecuteQuery(strSql) > 0)
                //{
                //    bRetValue = true;
                //}

            //strSql = "update tbltrnac  set AccaccountID = '26001'  where AccaccountID = ''";

            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}

            //strSql = "update tbltrnac  set accountID = '26001'  where accountID = ''";

            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}

            //strSql = "update tbltrnac set accaccountid = '30004' where accaccountID = '30003' and vouchertype = 'SVU'";

            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}

            //strSql = "update  tbltrnac set transactiondate = createddate where transactiondate < '20160331'";

            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}

            //strSql = "update  vouchersale set voucherdate = createddate where voucherdate < '20160331'";

            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}
            return bRetValue;
        }
    }
}
