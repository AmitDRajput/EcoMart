using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;
using EcoMart.InterfaceLayer;
using System.Collections;


namespace EcoMart.BusinessLayer
{
    class IFShowYearEnd
    {

        public string GetIfShowYearEnd()
        {
            string ShowYearEnd = "N";
            DBIfShowYearEnd dbs = new DBIfShowYearEnd();
            DataTable dt = dbs.GetTblAccountingYearData();
            string biggervoucherseries = General.ShopDetail.ShopVoucherSeries.ToString();
            string smallervoucherseries = General.ShopDetail.ShopVoucherSeries.ToString();
           DataRow dtcurrentyear = dbs.GetTblAccountingYearData(General.ShopDetail.ShopVoucherSeries.ToString());
            if (dtcurrentyear["YearEndOver"] != DBNull.Value && dtcurrentyear["YearEndOver"].ToString() == "Y")
                ShowYearEnd = "N";
            if (ShowYearEnd != "Y")
            {
                foreach (DataRow dtrow in dt.Rows)
                {
                    if (Convert.ToInt32(dtrow["VoucherSeries"].ToString()) > Convert.ToInt32(General.ShopDetail.ShopVoucherSeries))
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
                    ShowYearEnd = "Y";
                }
            }
            return ShowYearEnd;
                
        }
    }
}
