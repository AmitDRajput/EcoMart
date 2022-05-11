using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MitraPlus.DataLayer;
using System.Data;


namespace MitraPlus.BusinessLayer
{
    public class convertstringtodate
    {
        public DateTime ConvertStringToDate(string strdate)
        {
            DateTime dt;

            int theyear = System.Convert.ToInt32(strdate.Substring(0, 4));
            int themonth = System.Convert.ToInt32(strdate.Substring(4, 2));
            int theday = System.Convert.ToInt32(strdate.Substring(6, 2));
            dt = new DateTime(theyear, themonth, theday);

            return dt;
        }
    }
}
