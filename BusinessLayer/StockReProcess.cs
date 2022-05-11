using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public  class StockReProcess : BaseObject
    {
        #region Constructors, Destructors
        public StockReProcess()
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
        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Internal Methods


        #region Public Methods
        public bool InitializeMasterProduct()
        {            
            DBStockReProcess dbRe = new DBStockReProcess();
            return dbRe.InitializeMasterProduct();
        }

        public bool RemoveNegetiveStockFromtblStock()
        {
            DBStockReProcess dbRe = new DBStockReProcess();
            return dbRe.RemoveNegetiveStockFromtblStock();
        }
        //public bool UpdateMasterProduct()
        //{
        //    DBStockReProcess dbRe = new DBStockReProcess();
        //    return dbRe.UpdateMasterProduct();
        //}
        #endregion Public Methods

        public DataTable ReadCompanyData()
        {
            DataTable dt = new DataTable();
            DBStockReProcess dbRe = new DBStockReProcess();
            dt = dbRe.GetCompanyData();
            return dt;
        }

        public void UpdateProductMaster(string compID, string compshortname)
        {
            DBStockReProcess dbRe = new DBStockReProcess();
            dbRe.UpdateProductMaster(compID,compshortname);
        }

        public DataTable GetStockFromtblStock()
        {
            DataTable dt = new DataTable();
            DBStockReProcess dbRe = new DBStockReProcess();
            dt = dbRe.GetStockFromtblStock();
            return dt;
        }

        internal void UpdateStockInMasterProduct(string mprodID, int mopstk, int mclstk)
        {
            DBStockReProcess dbRe = new DBStockReProcess();
            bool retValue =  dbRe.UpdateStockInMasterProduct(mprodID, mopstk, mclstk);
        }
    }
}
