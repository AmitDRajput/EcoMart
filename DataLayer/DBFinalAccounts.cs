using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;


namespace EcoMart.DataLayer
{
    public class DBFinalAccounts
    {
        public DBFinalAccounts()
        {

        }
        public bool SaveTrialBalanceDates(string _MFromDate, string _MToDate)
        {
            string strSql = "Update tblSettings set setTrialBalanceDateFrom = '" + _MFromDate + "', setTrialBalanceDateTo = '" + _MToDate + "' where ID = '" + General.ShopDetail.ShopVoucherSeries + "'";
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public DataRow GetTrialBalanceDates()
        {
            DataRow dr;
            string strSql = "select setTrialBalanceDateFrom,setTrialBalanceDateTo from tblSettings where ID = '" + General.ShopDetail.ShopVoucherSeries + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;

        }

        public bool ClearProfitAndLossfrommasterGroup(double closingStock)
        {
            string strSql = "Update masterGroup set closingdebit = 0 , closingcredit = 0 where GroupCode = 'V'";
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "Update masterGroup set  closingcredit = "+ closingStock +" where  groupID = '" + FixAccounts.GroupCodeClosingStock +"'";
            bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public double GetProfit()
        {
            DataRow dt;
            double profit = 0;
            double debit = 0;
            double credit = 0;
            string strSql = "select sum(closingdebit - closingcredit) as db from mastergroup where closingdebit-closingcredit > 0 && Balancesheetcode = 'T'";
            dt = DBInterface.SelectFirstRow(strSql);
            if (dt != null)
            {
                if (dt["db"] != DBNull.Value)
                    debit = Convert.ToDouble(dt["db"].ToString());
            }
            strSql = "select sum(closingcredit - closingdebit) as db from mastergroup where closingdebit-closingcredit < 0 && Balancesheetcode = 'T'";
            dt = DBInterface.SelectFirstRow(strSql);
            if (dt != null)
            {
                if (dt["db"] != DBNull.Value)
                    credit = Convert.ToDouble(dt["db"].ToString());
            }
            profit = Math.Round(debit - credit, 2);
            return profit;
            
        }

        public bool UpdateLossAccounts(double _Profit)
        {
            bool bRetValue = false;
         
            string strSql = "Update masterGroup set closingdebit = "+ _Profit +" where  groupID = '"+ FixAccounts.GroupCodeForGrossProfitCD +"'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "Update masterGroup set closingcredit = " + _Profit + " where  groupID = '" + FixAccounts.GroupCodeForGrossProfitBD + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "Update masterGroup set closingcredit = 0  where  groupID = '" + FixAccounts.GroupCodeForLOSSCD + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            strSql = "Update masterGroup set closingdebit = 0  where  groupID = '" + FixAccounts.GroupCodeForLOSSBD + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue; 
        }

        public bool UpdateProfitAccounts(double _Profit)
        {
            bool bRetValue = false;

            string strSql = "Update masterGroup set closingdebit = 0 where  groupID = '" + FixAccounts.GroupCodeForGrossProfitCD + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "Update masterGroup set closingcredit = 0  where  groupID = '" + FixAccounts.GroupCodeForGrossProfitBD + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "Update masterGroup set closingcredit = "+ _Profit +"  where  groupID = '" + FixAccounts.GroupCodeForLOSSCD + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            strSql = "Update masterGroup set closingdebit = "+ _Profit +"  where  groupID = '" + FixAccounts.GroupCodeForLOSSBD + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue; 
        }

        public DataTable GetTradingDebitRows()
        {
            string strSql = "select groupID, groupname,(closingdebit - closingcredit) as db from mastergroup where closingdebit-closingcredit > 0 && Balancesheetcode = 'T' ";
            return DBInterface.SelectDataTable(strSql);
        }

        public DataTable GetTradingCreditRows()
        {
            string strSql = "select groupID,groupname,(closingcredit - closingdebit ) as db from mastergroup where closingdebit-closingcredit < 0 && Balancesheetcode = 'T' ";
            return DBInterface.SelectDataTable(strSql);
        }

        public double GetProfitForPnl()
        {
            DataRow dt;
            double profit = 0;
            double debit = 0;
            double credit = 0;
            string strSql = "select sum(closingdebit - closingcredit) as db from mastergroup where closingdebit-closingcredit > 0 && Balancesheetcode = 'P'";
            dt = DBInterface.SelectFirstRow(strSql);
            if (dt != null)
            {
                if (dt["db"] != DBNull.Value)
                    debit = Convert.ToDouble(dt["db"].ToString());
            }
            strSql = "select sum(closingcredit - closingdebit) as db from mastergroup where closingdebit-closingcredit < 0 && Balancesheetcode = 'P'";
            dt = DBInterface.SelectFirstRow(strSql);
            if (dt != null)
            {
                if (dt["db"] != DBNull.Value)
                    credit = Convert.ToDouble(dt["db"].ToString());
            }
            profit = Math.Round(debit - credit, 2);

            return profit;
        }

        public bool UpdateLossAccountsForPnL(double _Profit)
        {
            bool bRetValue = false;

            string strSql = "Update masterGroup set closingdebit = " + _Profit + " where  groupID = '" + FixAccounts.GroupCodeForNETProfit + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }           

            strSql = "Update masterGroup set closingcredit = 0  where  groupID = '" + FixAccounts.GroupCodeForNETLOSS + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
           
            return bRetValue; 
        }

        public bool UpdateProfitAccountsForPnL(double _Profit)
        {
            bool bRetValue = false;

            string strSql = "Update masterGroup set closingcredit = " + _Profit + " where  groupID = '" + FixAccounts.GroupCodeForNETLOSS + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "Update masterGroup set closingdebit = 0  where  groupID = '" + FixAccounts.GroupCodeForNETProfit + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue; 
        }

        public DataTable GetProfitAndLossLeftRows()
        {
            string strSql = "select groupID, groupname,(closingdebit - closingcredit) as db from mastergroup where closingdebit-closingcredit > 0 && Balancesheetcode = 'P' ";
            return DBInterface.SelectDataTable(strSql);
        }

        public DataTable GetProfitAndLossRightRows()
        {
            string strSql = "select groupID,groupname,(closingcredit - closingdebit ) as db from mastergroup where closingdebit-closingcredit < 0 && Balancesheetcode = 'P' ";
            return DBInterface.SelectDataTable(strSql);
        }
    }
}
