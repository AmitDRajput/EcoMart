using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class CreditDebitNote : BaseObject
    {
        #region Declaration
   
        # endregion

        #region Constructors
        public CreditDebitNote()
        {
            Initialise();
        }
        #endregion

        # region properties  
 
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
        }

        #endregion

        #region Public Methods

      
        public DataTable GetOverviewData()
        {
            DBCreditDebitNote dbStock = new DBCreditDebitNote();
            return dbStock.GetOverviewData();
        }
        public DataTable GetOverviewDataForParty(string AccID)
        {
            DBCreditDebitNote dbStock = new DBCreditDebitNote();
            return dbStock.GetOverviewDataForParty(AccID);
        }
        public DataTable GetDebitCreditNoteListByParty(string AccID, string fromDate, string toDate)
        {
            DBCreditDebitNote dbStock = new DBCreditDebitNote();
            return dbStock.GetOverviewDataForParty(AccID, fromDate, toDate);
        }

        public DataTable GetDebitCreditListProduct(string productID, string fromDate, string toDate)
        {
            DBCreditDebitNote dbStock = new DBCreditDebitNote();
            return dbStock.GetDebitCreditListProduct(productID, fromDate, toDate);
        }
        public DataTable GetDebitCreditStockListProductByVouType(string voutype, string fromDate, string toDate)
        {
            DBCreditDebitNote dbStock = new DBCreditDebitNote();
            return dbStock.GetDebitCreditStockListProductByVouType(voutype,fromDate,toDate);
        }
        public DataTable GetDebitCreditStockListProductByVouType(string voutype, string fromDate, string toDate, string productID)
        {
            DBCreditDebitNote dbStock = new DBCreditDebitNote();
            return dbStock.GetDebitCreditStockListProductByVouType(voutype, fromDate, toDate, productID);
        }
        public DataTable GetCreditDebitNoteListProduct(string Productid, string fromDate, string toDate)
        {
            DBCreditDebitNote dbStock = new DBCreditDebitNote();
            return dbStock.GetDebitCreditListProduct(Productid,fromDate,toDate);
        }
        public DataTable GetStockoutListProduct(string Productid)
        {
            DBCreditDebitNote dbStock = new DBCreditDebitNote();
            return dbStock.GetStockoutListProduct(Productid);
        }
        public DataTable GetOverviewDataForVATReport(string voucherType , string fromDate, string toDate)
        {
            DBCreditDebitNote dbStock = new DBCreditDebitNote();
            return dbStock.GetOverviewDataForVATReport(voucherType,fromDate, toDate);
        }
        public DataTable GetOverviewDataForVATReportMonth(string voucherType, string fromDate, string toDate)
        {
            DBCreditDebitNote dbStock = new DBCreditDebitNote();
            return dbStock.GetOverviewDataForVATReportMonth(voucherType, fromDate, toDate);
        }
        #endregion


       
    }
}
