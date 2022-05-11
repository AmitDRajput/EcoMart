using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;


namespace EcoMart.BusinessLayer
{
    class BankReConciliation : BaseObject
    {


        #region Constructors
        public BankReConciliation()
        {
            try
            {
                Initialise();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        public DataTable ReadDetails(string FromDate, string ToDate, string BankID)
        {
            DBBankReconciliation CBtran = new DBBankReconciliation();
            return CBtran.GetOverviewData(FromDate, ToDate, BankID);
        }
        public DataTable ReadDetailsDBNew(string fromDate, string toDate, string bankID,double debitAmount)
        {
            DBBankReconciliation CBtran = new DBBankReconciliation();
            return CBtran.GetOverviewDataDBNew(fromDate, toDate, bankID, debitAmount);
        }
        public DataTable ReadDetailsDBAll(string fromDate, string toDate, string bankID, double debitAmount)
        {
            DBBankReconciliation CBtran = new DBBankReconciliation();
            return CBtran.GetOverviewDataDBAll(fromDate, toDate, bankID, debitAmount);
        }
        public DataTable ReadDetailsCRNew(string fromDate, string toDate, string bankID, double debitAmount)
        {
            DBBankReconciliation CBtran = new DBBankReconciliation();
            return CBtran.GetOverviewDataCRNew(fromDate, toDate, bankID, debitAmount);
        }
        public DataTable ReadDetailsCRAll(string fromDate, string toDate, string bankID, double debitAmount)
        {
            DBBankReconciliation CBtran = new DBBankReconciliation();
            return CBtran.GetOverviewDataCRAll(fromDate, toDate, bankID, debitAmount);
        }
        public DataTable ReadDetailsNew(string fromDate, string toDate, string bankID)
        {
            DBBankReconciliation CBtran = new DBBankReconciliation();
            return CBtran.GetOverviewDataNew(fromDate, toDate, bankID);
        }
        public DataTable ReadDetailsAll(string fromDate, string toDate, string bankID)
        {
            DBBankReconciliation CBtran = new DBBankReconciliation();
            return CBtran.GetOverviewDataAll(fromDate, toDate, bankID);
        }

        public bool UpdatetblTrnac(string voucherID, string clearedDate)
        {
            DBBankReconciliation dbbkr = new DBBankReconciliation();
            return dbbkr.UpdatetblTrnac(voucherID, clearedDate);
        }
        public bool UpdatetblForPayment(string voucherID, string clearedDate)
        {
            DBBankReconciliation dbbkr = new DBBankReconciliation();
            return dbbkr.UpdatetblForPayment(voucherID, clearedDate);
        }
        public bool UpdatetblForReceipt(string voucherID, string clearedDate)
        {
            DBBankReconciliation dbbkr = new DBBankReconciliation();
            return dbbkr.UpdatetblForReceipt(voucherID, clearedDate);
        }

        public bool UpdatetblForExpenses(string voucherID, string clearedDate)
        {
            DBBankReconciliation dbbkr = new DBBankReconciliation();
            return dbbkr.UpdatetblForExpenses(voucherID, clearedDate);
        }
    }
}
