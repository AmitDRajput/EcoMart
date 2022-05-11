using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public  class DBVoucherSaleList
    {
        #region Constructor
        public DBVoucherSaleList()
        {
        }
        #endregion
   
        public DataTable GetOverviewDataForVoucherSaleReport(string fromdate)
        {
            DataTable dt = null;
            try
            {
                {
                    string strSql = "select ID,VoucherType, " +
                                    "VoucherNumber,VoucherDate, PatientName as AccName ,PatientAddress1 as AccAddress1,AmountNet as Amount  from  vouchersale  " +
                                    " where VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "'  &&  voucherdate = '" + fromdate + "' ";                                

                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }

    }
}
