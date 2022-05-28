using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    class DBYearEnd
    {
        public DBYearEnd()
        {
        }

        public bool CreateNewBase(string currentdatabase, string newdatabase)
        {
            bool bRetValue = false;
           
                //SELECT IF('db3' IN(SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA), 1, 0) AS found
                string strSql = "CREATE DATABASE IF NOT EXISTS " + newdatabase + " DEFAULT CHARACTER SET latin1 COLLATE latin1_german1_ci";// +newdatabase;
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            
            return bRetValue;
        }
        public bool CreateTable(string currentdatabase, string newdatabase, string tablename)
        {
            bool bRetValue = false;
            //mysql> CREATE TABLE new_table LIKE old_table;

            //and then copying the data in:

            //mysql> INSERT INTO new_table SELECT * FROM old_table;
            string strSql = "CREATE TABLE " + newdatabase + "." + tablename + " LIKE " + currentdatabase + "." + tablename;
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            DBInterface.ExecuteQuery(strSql);
            strSql = "INSERT INTO " + newdatabase + "." + tablename + " SELECT * FROM " + currentdatabase + "." + tablename;
            //  if (tablename == "detailsale" || tablename == "tbltrnac" || tablename == "tblstock" || tablename == "vouchersale")
            DBInterface.ExecuteQuery(strSql);
            // else
            //     DBInterface.ExecuteQuery(strSql);
            bRetValue = true;
            //}

            // string strSql = "Create TABLE " + newdatabase + "." + tablename + " Select * from " + currentdatabase + "." + tablename;
            // //CREATE TABLE EcoMart1516.changeddetailcashbankexpenses SELECT * FROM EcoMart1415.changeddetailcashbankexpenses;
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}

            return bRetValue;
        }

        public bool RemovePreviousYeartblaccouningYear(string voucherseries)
        {
            bool bRetValue = false;

            string strSql = "Delete from tblaccountingyear where voucherseries = '"+voucherseries +"'";
            // //CREATE TABLE EcoMart1516.changeddetailcashbankexpenses SELECT * FROM EcoMart1415.changeddetailcashbankexpenses;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool AddRowForCurrentAccountingYear(string newvoucherseries, string newsyear, string neweyear)
        {
            bool bRetValue = false;

            string strSql = "Insert into tblaccountingyear set voucherseries = '" + newvoucherseries + "' , FromDate = '" + newsyear + "' , Todate = '" + neweyear + "' , YearendOver = 'N', CurrentYear = 'Y' ";
            // //CREATE TABLE EcoMart1516.changeddetailcashbankexpenses SELECT * FROM EcoMart1415.changeddetailcashbankexpenses;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool RemovePreviousYeartblVoucherNumbers(string voucherseries, string mnewdatabase)
        {
            bool bRetValue = false;

            string strSql = "use " + mnewdatabase;
            DBInterface.ExecuteQuery(strSql);
            strSql = "Delete from tblVoucherNumbers where ID = '" + voucherseries + "'";
            // //CREATE TABLE EcoMart1516.changeddetailcashbankexpenses SELECT * FROM EcoMart1415.changeddetailcashbankexpenses;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool RemovePreviousYeartblSettings(string voucherseries, string mnewdatabase, string newVoucherSeries)
        {
            bool bRetValue = false;
            DataRow dr = null;
            string strSql = "use " + mnewdatabase;
            DBInterface.ExecuteQuery(strSql);
            strSql = "select * from tblSettings where ID = '" + newVoucherSeries + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null)
                strSql = "Delete from tblSettings where ID = '" + voucherseries + "'";
            else
            {
                strSql = "Update tblSettings set ID = '" + newVoucherSeries + "' where ID = '" + voucherseries + "'";
            }
            DBInterface.ExecuteQuery(strSql);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddRowForCurrentAccountingYeartblVoucherNumbers(string newvoucherseries, string newsyear, string neweyear, string currentDataBase, string voucherseries)
        {
            bool bRetValue = false;
            DataRow dr = null;
            string strSql = "use " + currentDataBase;
            DBInterface.ExecuteQuery(strSql);
            strSql = "Delete from tblVoucherNumbers where ID = '" + voucherseries + "'";
            DBInterface.ExecuteQuery(strSql);
            strSql = "select * from tblVoucherNumbers where ID = '" + newvoucherseries + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr == null)
            {
                strSql = "Insert into tblVoucherNumbers set ID = '" + newvoucherseries + "'";
                // //CREATE TABLE EcoMart1516.changeddetailcashbankexpenses SELECT * FROM EcoMart1415.changeddetailcashbankexpenses;
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            }

            return bRetValue;
        }


        public bool IfCurrentYearExists(string newvoucherseries)
        {
            bool bRetValue = false;
            DataRow dt;
            string strSql = "Select * from tblaccountingyear where voucherseries = '" + newvoucherseries + "'";
            // //CREATE TABLE EcoMart1516.changeddetailcashbankexpenses SELECT * FROM EcoMart1415.changeddetailcashbankexpenses;
            dt = DBInterface.SelectFirstRow(strSql);
            if (dt != null)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool DefinePrimaryKeys(string tablename, string primarykey)
        {
            bool bRetValue = false;

            string strSql = "alter table " + tablename + " ADD PRIMARY KEY (" + primarykey + ")";
                
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }
        public bool DefineUniqueKeys(string tablename, string primarykey)
        {
            bool bRetValue = false;

            string strSql = "alter table " + tablename + " ADD UNIQUE KEY (" + primarykey + ")";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public void DeletePreviousYearFromtblAccountingYearandtblvouchernumbers(string accountingyear)
        {
          
            string strSql = "Delete from tblaccountingyear where voucherseries < '"+ accountingyear +"'";
            DBInterface.ExecuteQuery(strSql);
            strSql = "Delete from tblvouchernumbers where ID < '" + accountingyear + "'";
            DBInterface.ExecuteQuery(strSql);  
        }
      
      
        public bool ClearStockIntblStockAndMasterProductForYearEnd()
        {
            bool returnVal = false;
            string strSql = "Update masterproduct set ProdOpeningStock =  0";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            strSql = "Update tblstock set beginningStock = 0, OpeningStock =  0, PurchaseStock = 0, TransferInStock = 0,CreditNoteStock = 0, SaleStock = 0,TransferOutStock = 0, DebitNoteStock = 0,PurchaseSchemeStock = 0,SaleSchemeStock = 0,IfRateCorrection = ''";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }

        public DataTable GetOpeningStockForCurrentYear(string _MToDate)
        {
            DataTable dtable = null;
            string strSql = "";

            strSql = "Select b.ProductID, b.StockID, a.voucherDate,  b.Quantity , b.SchemeQuantity,d.Prodname,d.Prodpack,d.Prodloosepack  from detailopStock b inner join voucheropStock a on b.MasterID = a.MasterID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherDate > '" + _MToDate + "'";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public bool UpdateOpeningStock(string mstockid, int mqtyin, int mscmqtyin)
        {
            bool returnVal = false;
            string strSql = "Update tblstock set BeginningStock = beginningstock +"+(mqtyin+mscmqtyin) +" where stockID = '"+ mstockid +"'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public DataTable GetSaleStockForCurrentYearForYearEnd(string _MToDate)
        {
            DataTable dtable = null;
            string strSql = "";

            strSql = "Select b.ProductID, b.StockID, a.voucherDate, sum(b.Quantity) as Quantity , sum(b.SchemeQuantity) as SchemeQuantity,d.Prodname,d.Prodpack,d.Prodloosepack  from detailSale b inner join voucherSale a on b.MasterSaleID = a.ID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherDate > '" + _MToDate + "' group by b.StockID";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public bool UpdateSaleStock(string mstockid, int mqtyin, int mscmqtyin)
        {
            bool returnVal = false;
            string strSql = "Update tblstock set saleStock = salestock +" + (mqtyin + mscmqtyin) + "  where stockID = '" + mstockid + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public DataTable GetPurchaseStockForCurrentYearForYearEnd(string _MToDate)
        {
            DataTable dtable = null;
            string strSql = "";

            strSql = "Select b.ProductID, b.StockID, a.voucherDate,  sum(b.Quantity*d.ProdLoosePack) as Quantity , sum(b.SchemeQuantity*d.ProdLoosePack) as schemequantity,d.Prodname,d.Prodpack,d.Prodloosepack  from detailpurchase b inner join voucherpurchase a on b.PurchaseID = a.PurchaseID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherDate > '" + _MToDate + "' group by b.StockID";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }        

        public bool UpdatePurchaseStock(string mstockid, int mqtyin, int mscmqtyin)
        {
            bool returnVal = false;
            string strSql = "Update tblstock set purchaseStock = purchaseStock +" + (mqtyin + mscmqtyin) + "  where stockID = '" + mstockid + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public DataTable GetCreditNoteStockINStockForCurrentYearForYearEnd(string _MToDate)
        {
            DataTable dtable = null;
            string strSql = "";

            strSql = "Select b.ProductID, b.StockID, a.voucherDate,  sum(b.Quantity) as Quantity , sum(b.SchemeQuantity) as schemequantity,d.Prodname,d.Prodpack,d.Prodloosepack  from detailcreditdebitnotestock b inner join vouchercreditdebitnote a on b.MasterID = a.CRDBID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherDate > '" + _MToDate + "' && (a.voucherType = '"+ FixAccounts.VoucherTypeForCreditNoteStock +"' || a.voucherType = '"+ FixAccounts.VoucherTypeForStockIN +"') group by b.StockID";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public bool UpdateCreditNoteStockINStock(string mstockid, int mqtyin, int mscmqtyin)
        {
            bool returnVal = false;
            string strSql = "Update tblstock set creditnoteStock = creditnoteStock +" + (mqtyin + mscmqtyin) + "  where stockID = '" + mstockid + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }

        public DataTable GetDebitNoteStockOUTStockForCurrentYearForYearEnd(string _MToDate)
        {
            DataTable dtable = null;
            string strSql = "";

            strSql = "Select b.ProductID, b.StockID, a.voucherDate,  sum(b.Quantity) as Quantity , sum(b.SchemeQuantity) as schemequantity,d.Prodname,d.Prodpack,d.Prodloosepack  from detailcreditdebitnotestock b inner join vouchercreditdebitnote a on b.MasterID = a.CrdbID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherDate > '" + _MToDate + "' && (a.voucherType = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' || a.voucherType = '" + FixAccounts.VoucherTypeForStockOut + "') group by b.StockID";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public bool UpdateDebitNoteStockOUTStock(string mstockid, int mqtyin, int mscmqtyin)
        {
            bool returnVal = false;
            string strSql = "Update tblstock set debitnoteStock = debitnoteStock +" + (mqtyin + mscmqtyin) + "  where stockID = '" + mstockid + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public DataTable GetCorrectionInRateStockForCurrentYearForYearEnd(string _MToDate)
        {
            DataTable dtable = null;
            string strSql = "";

            strSql = "Select b.ProductID, b.oldStockID,b.OldQuantity,b.NewStockID,b.NewQuantity, b.voucherDate, d.Prodname,d.Prodpack,d.Prodloosepack  from vouchercorrectioninRate b  inner join masterproduct d on b.ProductID = d.ProductID where b.voucherDate > '" + _MToDate + "'";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public bool UpdateCorrectionInRateStockStock(string olstockid, int oldqty, string newstockID, int newqty)
        {
            bool returnVal = false;
            string strSql = "Update tblstock set Transferoutstock = Transferoutstock +" + oldqty
                + "  where stockID = '" + olstockid + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            strSql = "Update tblstock set TransferInstock = TransferInstock +" + newqty
              + "  where stockID = '" + newstockID + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
       // update tblstock set openingstock = closingstock - (beginningstock+purchasestock+creditnotestock+transferinstock) + (salestock+debitnotestock+transferoutstock)
        //update tblstock set beginningstock = 0
       // update tblstock set beginningstock = openingStock where openingstock < 0
      //  update tblstock set openingStock = 0  where openingstock < 0

        public bool CalculateOpeningStock(string toDate)
        {
            bool returnVal = false;
            string strSql = string.Empty;
            int noofrows = 0;

            strSql = "delete from tbltemppurchase where CreatedDate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            strSql = "select count(ID) from tbltemppurchase";
            noofrows = DBInterface.ExecuteQuery(strSql);
            if (noofrows <= 0)
            {
                strSql = "update tblstock set  closingstock = 0 where closingstock < 0";

                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;


                strSql = "update tblstock set openingstock = closingstock - (beginningstock+purchasestock+creditnotestock+transferinstock) + (salestock+debitnotestock+transferoutstock)";

                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            else
            {
                strSql = "update tblstock set openingstock = closingstock - (beginningstock+purchasestock+creditnotestock+transferinstock) + (salestock+debitnotestock+transferoutstock)";

                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
            }
            strSql = "update tblstock set beginningstock = 0";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            strSql = "update tblstock set beginningstock = openingStock where openingstock < 0";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            strSql = "update tblstock set openingStock = 0  where openingstock < 0";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            return returnVal;
        }
        public bool DeleteFromVouchers(string toDate)
        {
            bool returnVal = false;

            //changed vouchers

            string strSql = "delete from changedvouchercashbankexpenses where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from changedvouchercashbankpayment where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from changedvouchercashbankreceipt where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from changedvouchercreditdebitnote where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from changedvoucherjv where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from changedvoucherpurchase where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from changedvouchersale where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from changedspecialvouchersale where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            //deleted vouchers

            strSql = "delete from deletedvouchercashbankexpenses where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from deletedvouchercashbankpayment where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from deletedvouchercashbankreceipt where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from deletedvouchercreditdebitnote where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from deletedvoucherjv where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from deletedvoucherpurchase where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from deletedvouchersale where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from deletedspecialvouchersale where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            // vouchers 
            
           strSql = "delete from vouchercashbankexpenses where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
           
            strSql = "delete from vouchercashbankpayment where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from vouchercashbankreceipt where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from voucherchequereturn where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from vouchercorrectioninrate where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from vouchercreditdebitnote where voucherdate <= '" + toDate + "' && (AmountClear > 0 && vouchertype != '"+ FixAccounts.VoucherTypeForStockIN +"' && vouchertype != '"+ FixAccounts.VoucherTypeForStockOut +"')";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from voucherjv where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from voucheropstock where voucherdate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;            

            return returnVal;
        }
        public bool DeleteFromChangedDeletedDetails()
        {
            bool returnVal = false;
            string strSql = "DELETE FROM changeddetailcashbankexpenses WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchercashbankexpenses f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM changeddetailcashbankpayment WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchercashbankpayment f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM changeddetailcashbankreceipt WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchercashbankreceipt f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM changeddetailcreditdebitnoteamount WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchercreditdebitnote f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM changeddetailcreditdebitnotestock WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchercreditdebitnote f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM changeddetailpurchase WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvoucherpurchase f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM changeddetailsale WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchersale f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM changedspecialdetailsale WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedspecialvouchersale f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;


            //deleted

            strSql = "DELETE FROM deleteddetailcashbankexpenses WHERE masterID NOT IN (SELECT f.cbid FROM  deletedvouchercashbankexpenses f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM deleteddetailcashbankpayment WHERE masterID NOT IN (SELECT f.cbid FROM  deletedvouchercashbankpayment f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM deleteddetailcashbankreceipt WHERE masterID NOT IN (SELECT f.cbid FROM  deletedvouchercashbankreceipt f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM deleteddetailcreditdebitnoteamount WHERE crdbID NOT IN (SELECT f.crdbid FROM  deletedvouchercreditdebitnote f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM deleteddetailcreditdebitnotestock WHERE masterID NOT IN (SELECT f.crdbID FROM  deletedvouchercreditdebitnote f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM deleteddetailpurchase WHERE purchaseID NOT IN (SELECT f.purchaseID FROM  deletedvoucherpurchase f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM deleteddetailsale WHERE mastersaleID NOT IN (SELECT f.ID FROM  deletedvouchersale f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM deletedspecialdetailsale WHERE mastersaleID NOT IN (SELECT f.ID FROM  deletedspecialvouchersale f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

          
            return returnVal;
        }
        
        //other tables


        public bool DeleteFromtblTrnactbldailyshortlist(string toDate)
        {
            bool returnVal = false;
            string strSql = "delete from tbltrnac where Transactiondate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from tbldailyshortlist where ShortListDate <= '" + toDate + "' || CreatedDate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "delete from masterorder where voucherDate <= '" + toDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

           
        
            return returnVal;
        }
        // detail
        public bool DeleteFromDetails(string _MToDate)
        {           
            bool returnVal = false;
            string strSql = "DELETE FROM detailcashbankexpenses WHERE masterID NOT IN (SELECT f.cbid FROM  vouchercashbankexpenses f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM detailcashbankpayment WHERE masterID NOT IN (SELECT f.cbid FROM  vouchercashbankpayment f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM detailcashbankreceipt WHERE masterID NOT IN (SELECT f.cbid FROM  vouchercashbankreceipt f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM detailchequereturn WHERE masterID NOT IN (SELECT f.chequereturnid FROM  voucherchequereturn f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM detailcreditdebitnoteamount WHERE crdbid NOT IN (SELECT f.crdbid FROM  vouchercreditdebitnote f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;

            strSql = "DELETE FROM detailcreditdebitnotestock WHERE masterID NOT IN (SELECT f.crdbID FROM  vouchercreditdebitnote f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;


            strSql = "DELETE FROM specialdetailsale WHERE mastersaleID NOT IN (SELECT f.ID FROM  specialvouchersale f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            return returnVal;
        }

        public bool SelectFromSale(string _MToDate)
        {
            bool returnVal = false;          
            string strSql = "INSERT INTO tbloldvoucherSale Select *  FROM voucherSale WHERE  (vouchertype = '" + FixAccounts.VoucherTypeForCreditSale + "' && voucherDate <= '" + _MToDate + "' &&   ID IN (SELECT f.mastersaleID FROM  detailcashbankreceipt f inner join vouchercashbankreceipt b on f.masterID = b.cbid  where b.VoucherDate > '" + _MToDate + "' )) || (vouchertype = '" + FixAccounts.VoucherTypeForCreditSale + "' && voucherDate <= '" + _MToDate + "' && amountbalance > 0)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
               returnVal = true;
            strSql = "INSERT INTO tbloldDetailSale  select * from detailsale WHERE  mastersaleID in (select f.ID From tbloldvouchersale f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            else
                returnVal = false;

            return returnVal;
           

        }

        public bool SelectFromPurchase(string _MToDate)
        {
            bool returnVal = false;
            DataTable dt = new DataTable();
            string strSql = "INSERT INTO tbloldvoucherpurchase Select *  FROM voucherpurchase WHERE  (vouchertype = '" + FixAccounts.VoucherTypeForCreditPurchase + "' && voucherDate <= '" + _MToDate + "' &&   PurchaseID IN (SELECT f.masterpurchaseID FROM  detailcashbankpayment f inner join vouchercashbankpayment b on f.masterID = b.cbid  where b.VoucherDate > '" + _MToDate + "' )) || (vouchertype = '" + FixAccounts.VoucherTypeForCreditPurchase + "' && voucherDate <= '" + _MToDate + "' && amountbalance > 0)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            strSql = "INSERT INTO tbloldDetailpurchase  select * from detailpurchase WHERE  purchaseID in (select f.purchaseID From tbloldvoucherpurchase f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            return returnVal;
            
        }

        public bool SelectFromStatement(string _MToDate)
        {
            bool returnVal = false;
            string strSql = "INSERT INTO tbloldVoucherStatement  select * from voucherstatement WHERE (voucherDate <= '" + _MToDate +"' && amountbalance > 0)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            else
                returnVal = false;
            return returnVal;
        }

        public bool  DeleteForSalePurchaseAndStatement(string _MToDate)
        {
            bool returnVal = false;
            string strSql = "Delete from vouchersale   WHERE voucherDate <= '" + _MToDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            else
                returnVal = false;

            strSql = "Delete from voucherpurchase   WHERE voucherDate <= '" + _MToDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            else
                returnVal = false;

            strSql = "Delete from voucherstatement   WHERE voucherDate <= '" + _MToDate + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            else
                returnVal = false;

            strSql = "DELETE FROM detailsale WHERE mastersaleID NOT IN (SELECT f.ID FROM  vouchersale f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            else
                returnVal = false;

            strSql = "DELETE FROM detailpurchase WHERE purchaseID NOT IN (SELECT f.purchaseID FROM  voucherpurchase f)";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            else
                returnVal = false;

            return returnVal;
           
        }

        public DataTable GetClearedAmountinMasterAccount(string _MToDate)
        {
           
            DataTable dt = new DataTable();
            string strSql = "select b.AccountID, sum(a.clearAmount) as clearAmount from detailcashbankreceipt a inner join vouchercashbankreceipt b on a.MasterID = b.cbid where a.BillDate <= '" + _MToDate + "' group by b.AccountID";
            dt = DBInterface.SelectDataTable(strSql);
             return dt;
        }

        public bool UpdateMasterClearedAmount(string maccountID, double mclearamt)
        {
            bool returnVal = false;
            string strSql = "Update masteraccount set accClearedAmount = "+ mclearamt + " Where AccountID = '"+ maccountID +"'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            else
                returnVal = false;

            return returnVal;
        }

        public bool DeleteFromNewDataBase(string toDate, string newvoucherSeries)
        {
            bool returnVal = false;
            try
            {
                string strSql = "delete from changedvouchercashbankexpenses where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from changedvouchercashbankpayment where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from changedvouchercashbankreceipt where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from changedvouchercreditdebitnote where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from changedvoucherjv where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from changedvoucherpurchase where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from changedvouchersale where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from changedspecialvouchersale where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                //deleted vouchers

                strSql = "delete from deletedvouchercashbankexpenses where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from deletedvouchercashbankpayment where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from deletedvouchercashbankreceipt where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from deletedvouchercreditdebitnote where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from deletedvoucherjv where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from deletedvoucherpurchase where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from deletedvouchersale where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from deletedspecialvouchersale where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                // vouchers 

                strSql = "delete from vouchercashbankexpenses where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from vouchercashbankpayment where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from vouchercashbankreceipt where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from voucherchequereturn where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from vouchercorrectioninrate where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from vouchercreditdebitnote where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from voucherjv where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from voucheropstock where voucherdate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM changeddetailcashbankexpenses WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchercashbankexpenses f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM changeddetailcashbankpayment WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchercashbankpayment f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM changeddetailcashbankreceipt WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchercashbankreceipt f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM changeddetailcreditdebitnoteamount WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchercreditdebitnote f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM changeddetailcreditdebitnotestock WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchercreditdebitnote f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM changeddetailpurchase WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvoucherpurchase f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM changeddetailsale WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedvouchersale f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM changedspecialdetailsale WHERE changedmasterID NOT IN (SELECT f.changedID FROM  changedspecialvouchersale f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;


                //deleted

                strSql = "DELETE FROM deleteddetailcashbankexpenses WHERE masterID NOT IN (SELECT f.cbid FROM  deletedvouchercashbankexpenses f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM deleteddetailcashbankpayment WHERE masterID NOT IN (SELECT f.cbid FROM  deletedvouchercashbankpayment f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM deleteddetailcashbankreceipt WHERE masterID NOT IN (SELECT f.cbid FROM  deletedvouchercashbankreceipt f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM deleteddetailcreditdebitnoteamount WHERE crdbID NOT IN (SELECT f.crdbid FROM  deletedvouchercreditdebitnote f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM deleteddetailcreditdebitnotestock WHERE masterID NOT IN (SELECT f.crdbID FROM  deletedvouchercreditdebitnote f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM deleteddetailpurchase WHERE purchaseID NOT IN (SELECT f.purchaseID FROM  deletedvoucherpurchase f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM deleteddetailsale WHERE mastersaleID NOT IN (SELECT f.ID FROM  deletedvouchersale f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM deletedspecialdetailsale WHERE mastersaleID NOT IN (SELECT f.ID FROM  deletedspecialvouchersale f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "Delete from vouchersale   WHERE voucherDate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;

                strSql = "Delete from voucherpurchase   WHERE voucherDate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;

                strSql = "Delete from voucherstatement   WHERE voucherDate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;

                strSql = "DELETE FROM detailsale WHERE mastersaleID NOT IN (SELECT f.ID FROM  vouchersale f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;

                strSql = "DELETE FROM detailpurchase WHERE purchaseID NOT IN (SELECT f.purchaseID FROM  voucherpurchase f)";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;

                strSql = "delete from tbltrnac where Transactiondate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from tbldailyshortlist where ShortListDate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "delete from masterorder where voucherDate > '" + toDate + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;

                strSql = "DELETE FROM tblvoucherNumbers WHERE ID != '" + newvoucherSeries + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;

                strSql = "DELETE FROM tblsettings WHERE ID != '" + newvoucherSeries + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;
                strSql = "DELETE FROM tblaccountingyear WHERE voucherseries != '" + newvoucherSeries + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;

                strSql = "update tblaccountingyear set YearEndOver = 'Y' WHERE voucherseries = '" + newvoucherSeries + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return returnVal;
        }
    }
}
