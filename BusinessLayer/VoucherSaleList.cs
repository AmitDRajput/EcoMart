using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class VoucherSaleList : BaseObject
    {

        #region Constructors, Destructors
        public VoucherSaleList()
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
        public DataTable GetOverviewDataForVoucherSaleReport(string fromdate)
        {
            DBVoucherSaleList dbsalelist = new DBVoucherSaleList();
            return dbsalelist.GetOverviewDataForVoucherSaleReport(fromdate);
        }
    }
}
