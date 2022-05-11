using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.InterfaceLayer;
using System.Collections;


namespace PharmaSYSRetailPlus.BusinessLayer
{
    class IFShowYearEnd
    {

        public string GetIfShowYearEnd()
        {
            string IfYearEnd = "N";
            DBIfShowYearEnd dbs = new DBIfShowYearEnd();
            DataTable dt = dbs.GetTblAccountingYearData();
            string biggervoucherseries = General.ShopDetail.ShopVoucherSeries.ToString();
            string smallervoucherseries = General.ShopDetail.ShopVoucherSeries.ToString();
            foreach (DataRow dtrow in dt.Rows)
            {
                if ( Convert.ToInt32(dtrow["VoucherSeries"].ToString()) > Convert.ToInt32(General.ShopDetail.ShopVoucherSeries))
                {
                   biggervoucherseries = dtrow["VoucherSeries"].ToString();
                    
                }
                if (Convert.ToInt32(dtrow["VoucherSeries"].ToString()) < Convert.ToInt32(General.ShopDetail.ShopVoucherSeries))
                {
                    smallervoucherseries = dtrow["VoucherSeries"].ToString();
                   
                }
            }
            //string cdate = DateTime.Now.Date.ToString("yyyyMMdd");
            //if (Convert.ToInt32(cdate) >= Convert.ToInt32(General.ShopDetail.Shopsy)  && Convert.ToInt32(cdate) <= Convert.ToInt32(General.ShopDetail.Shopey))
            //{
            //    IfYearEnd = "N";
            //}
            if (biggervoucherseries != General.ShopDetail.ShopVoucherSeries && smallervoucherseries == General.ShopDetail.ShopVoucherSeries)
            {
                IfYearEnd = "Y";
            }
                return IfYearEnd;
                
        }
    }
}
