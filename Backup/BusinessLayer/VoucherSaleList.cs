using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
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
