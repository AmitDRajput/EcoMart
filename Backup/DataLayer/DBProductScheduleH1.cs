using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
{

    public class DBProductScheduleH1
    {
        # region Constructor
        public DBProductScheduleH1()
        {
        }
        # endregion

        # region Other Private Methods

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.AccName, a.AccountID,a.AccAddress1,a.AccAddress2 from Masteraccount  a  INNER Join linkdebtorproduct b where a.AccountID = b.AccountID order by a.AccName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        #endregion


        public void RemoveH1TagInProductMaster()
        {
           
            string strSql = "Update masterproduct set ProdScheduleDrugCode = '' where ProdScheduleDrugCode = 'H1'";
            DBInterface.ExecuteQuery(strSql);
               

        }

        public void SetProdScheduleCode(string productID)
        {
            
            string strSql = "Update masterproduct set ProdScheduleDrugCode = 'H1' where productID = '"+productID+"'";
            DBInterface.ExecuteQuery(strSql);
              
        }
    }
}
