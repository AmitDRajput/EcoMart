using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using EcoMart.Common;
using System.Data;

namespace EcoMart
{
    public class LockTableRows
    {
        public static bool IsLocked(string LockName)
        {
            bool retValue = false;
            try
            {
                DataTable dtable = new DataTable();
                string strSQL = string.Format("SELECT IS_FREE_LOCK({0})", LockName);
                dtable = DBInterface.SelectDataTable(strSQL);
                if (dtable != null && dtable.Rows.Count > 0)
                {
                    retValue = Convert.ToBoolean(dtable.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
            return retValue;
        }

        public static bool LockRows(string LockName)
        {
            bool retValue = false;
            try
            {
                DataTable dtable = new DataTable();
                string strSQL = string.Format("SELECT GET_LOCK({0},10)", LockName);
                dtable = DBInterface.SelectDataTable(strSQL);
                if (dtable != null && dtable.Rows.Count > 0)
                {
                    retValue = Convert.ToBoolean(dtable.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
            return retValue;
        }

        public static void UnLockRows(string LockName)
        {
            try
            {
                string strSQL = string.Format("SELECT RELEASE_LOCK('{0}') ", LockName);
                DBInterface.ExecuteQuery(strSQL);                
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
    }
}
