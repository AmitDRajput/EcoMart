using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    class DeletedORChangedVouchers : BaseObject
    {
  #region Declaration  

        #endregion

        #region Constructors, Destructors
        public DeletedORChangedVouchers()
        {
            try
            {
                Initialise();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
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

        public DataTable GetDeletedDataMaster(string fromdate, string todate, string mtype)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadDeletedData(fromdate, todate);
        }
        public DataTable GetDeletedDataMaster(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadDeletedData(fromdate, todate);
        }
        public DataTable GetDeletedDataMasterPurchase(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadDeletedDataPurchase(fromdate, todate);
        }
        public DataTable GetDeletedDataMasterCashReceipt(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadDeletedDataCashReceipt(fromdate, todate);
        }
        public DataTable GetDeletedDataMasterBankReceipt(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadDeletedDataBankReceipt(fromdate, todate);
        }
        public DataTable GetDeletedDataMasterCashPayment(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadDeletedDataCashPayment(fromdate, todate);
        }
        public DataTable GetDeletedDataMasterBankPayment(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadDeletedDataBankPayment(fromdate, todate);
        }
        //public DataTable GetChangedDataMasterSale(string fromdate, string todate, string mtype)
        //{
        //    DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
        //    return dclist.ReadChangedDataSale(fromdate, todate);
        //}
        public DataTable GetChangedDataMasterSale(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadChangedDataSale(fromdate, todate);
        }
        public DataTable GetChangedDataMasterPurchase(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadChangedDataPurchase(fromdate, todate);
        }
        public DataTable GetChangedDataMasterCashReceipt(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadChangedDataCashReceipt(fromdate, todate);
        }
        public DataTable GetChangedDataMasterBankReceipt(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadChangedDataBankReceipt(fromdate, todate);
        }
        public DataTable GetChangedDataMasterCashPayment(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadChangedDataCashPayment(fromdate, todate);
        }

        internal DataTable GetChangedDataMasterBankPayment(string fromdate, string todate)
        {
            DBDeletedORChangedVouchers dclist = new DBDeletedORChangedVouchers();
            return dclist.ReadChangedDataBankPayment(fromdate, todate);
        }
        #endregion Public Methods

    }
}
