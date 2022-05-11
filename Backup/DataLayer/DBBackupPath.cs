using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBBackupPath
    {
        public DBBackupPath()
        {
        }

        public DataRow ReadDetails(string compvs)
        {
            DataRow dRow = null;

            string strSql = "Select BackupPath1,BackupPath2,BackupPath3 from tblsettings where ID = '" + compvs + "'";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        public bool UpdateDetails(string BackupPath1, string BackupPath2, string BackupPath3, string compvs)
        {
            bool retValue = false;
            string strSql = "update tblsettings set BackupPath1 = '" + BackupPath1 + "', BackupPath2 = '" + BackupPath2 + "', BackupPath3 = '" + BackupPath3 + "' where ID = '" + compvs + "'";
            strSql = strSql.Replace(@"\", @"\\");
            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            return retValue;
        }
    }
}
