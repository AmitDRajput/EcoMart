using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
{
   public class DBDeletedORChangedVouchers
    {

       public DBDeletedORChangedVouchers()
        {
        }
       public DataTable ReadDeletedData(string mfromdate, string mtodate, string mtype)
       {
           DataTable dt = null;
           string strSql = "select a.ID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate,a.VoucherSubType,a.AccountID,a.AmountNet,a.DoctorID,a.PatientID,a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2 from deletedvouchersale a  where a.voucherdate >= '" + mfromdate + "' && a.voucherdate <= '" + mtodate + "' && a.voucherType = '" + mtype + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }
       public DataTable ReadDeletedData(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.ID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate,a.VoucherSubType,a.AccountID,a.AmountNet,a.DoctorID,a.PatientID,a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2 from deletedvouchersale a  where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }

       public DataTable ReadDeletedDataPurchase(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.PurchaseID,a.VoucherType,a.VoucherNumber,VoucherDate,a.AccountID,a.AmountNet,b.AccName from Deletedvoucherpurchase a inner join masteraccount b on a.AccountID = b.AccountID where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }
       public DataTable ReadDeletedDataCashReceipt(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.CBID,a.VoucherType,a.VoucherNumber,VoucherDate,a.AccountID,a.AmountNet,b.AccName from Deletedvouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' && a.voucherType = '" + FixAccounts.VoucherTypeForCashReceipt + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }

       public DataTable ReadDeletedDataBankReceipt(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.CBID,a.VoucherType,a.VoucherNumber,VoucherDate,a.AccountID,a.AmountNet,b.AccName from Deletedvouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' && a.voucherType = '" + FixAccounts.VoucherTypeForBankReceipt + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }


       public DataTable ReadDeletedDataCashPayment(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.CBID,a.VoucherType,a.VoucherNumber,VoucherDate,a.AccountID,a.AmountNet,b.AccName from DeletedvouchercashbankPayment a inner join masteraccount b on a.AccountID = b.AccountID where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' && a.voucherType = '" + FixAccounts.VoucherTypeForCashPayment + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }

       public DataTable ReadDeletedDataBankPayment(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.CBID,a.VoucherType,a.VoucherNumber,VoucherDate,a.AccountID,a.AmountNet,b.AccName from DeletedvouchercashbankPayment a inner join masteraccount b on a.AccountID = b.AccountID where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' && a.voucherType = '" + FixAccounts.VoucherTypeForBankPayment + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }


       public DataTable ReadChangedData(string mfromdate, string mtodate, string mtype)
       {
           DataTable dt = null;
           string strSql = "select a.ID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate,a.VoucherSubType,a.AccountID,a.AmountNet,a.DoctorID,a.PatientID,a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2 from changedvouchersale a  where a.voucherdate >= '" + mfromdate + "' && a.voucherdate <= '" + mtodate + "' && a.voucherType = '" + mtype + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }
       public DataTable ReadChangedDataSale(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.ID,a.ChangedID,a.VoucherType,a.VoucherNumber,a.CounterSaleNumber,a.VoucherDate,a.VoucherSubType,a.AccountID,a.AmountNet,a.DoctorID,a.PatientID,a.OperatorID,a.PatientName,a.PatientAddress1,a.PatientAddress2 from changedvouchersale a  where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }
       public DataTable ReadChangedDataPurchase(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.PurchaseID,a.ChangedID,a.VoucherType,a.VoucherNumber,VoucherDate,a.AccountID,a.AmountNet,b.AccName from changedvoucherpurchase a inner join masteraccount b on a.AccountID = b.AccountID where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }
       public DataTable ReadChangedDataCashReceipt(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.CBID,a.ChangedID,a.VoucherType,a.VoucherNumber,VoucherDate,a.AccountID,a.AmountNet,b.AccName from changedvouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' && a.voucherType = '"+ FixAccounts.VoucherTypeForCashReceipt +"' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }

       public DataTable ReadChangedDataBankReceipt(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.CBID,a.ChangedID,a.VoucherType,a.VoucherNumber,VoucherDate,a.AccountID,a.AmountNet,b.AccName from changedvouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' && a.voucherType = '" + FixAccounts.VoucherTypeForBankReceipt + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }
       public DataTable ReadChangedDataCashPayment(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.CBID,a.ChangedID,a.VoucherType,a.VoucherNumber,VoucherDate,a.AccountID,a.AmountNet,b.AccName from changedvouchercashbankpayment a inner join masteraccount b on a.AccountID = b.AccountID where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' && a.voucherType = '" + FixAccounts.VoucherTypeForCashPayment + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }
       public DataTable ReadChangedDataBankPayment(string mfromdate, string mtodate)
       {
           DataTable dt = null;
           string strSql = "select a.CBID,a.ChangedID,a.VoucherType,a.VoucherNumber,VoucherDate,a.AccountID,a.AmountNet,b.AccName from changedvouchercashbankpayment a inner join masteraccount b on a.AccountID = b.AccountID where a.modifieddate >= '" + mfromdate + "' && a.modifieddate <= '" + mtodate + "' && a.voucherType = '" + FixAccounts.VoucherTypeForBankPayment + "' order by a.voucherdate,a.vouchernumber";
           dt = DBInterface.SelectDataTable(strSql);
           return dt;
       }
    }
}
