using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBDelete
    {
        public DBDelete()
        {
        }

        public int GetOverviewDataSelect(string ddtable, string dfield, string dfieldId)
        {
            DataTable dtable = new DataTable();
            int _count = 0;
            string strSql = string.Format("Select " + dfield + " from " + ddtable + " where " + dfield + " =  '{0}'", dfieldId);

            dtable = DBInterface.SelectDataTable(strSql);
            if (dtable != null)
                _count = dtable.Rows.Count;
            return _count;
        }

    }
}
