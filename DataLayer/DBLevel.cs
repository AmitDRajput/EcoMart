using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    class DBLevel
    {
        public DBLevel()
        { 
        }
        public DataTable GetLevel(string fname)
        {
            DataTable dtable = new DataTable();
            string strsql = string.Format("select FormName,AddModule,EditModule,DeleteModule,PrintModule,ViewModule from ablauthority where FormName={0}",fname);
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
    }
}
