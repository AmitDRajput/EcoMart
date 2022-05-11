using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;


namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class YearEnd : BaseObject
    {

        #region Constructors, Destructors
        public YearEnd()
        {
            try
            {
                Initialise();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Constructors, Destructors

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override void DoValidate()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public bool CreateNewBase(string currentdatabase, string newdatabase)
        {
            bool retValue = false;

            try
            {
                DBYearEnd dby = new DBYearEnd();
                retValue = dby.CreateNewBase(currentdatabase, newdatabase);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public bool CreateTable(string currentdatabase, string newdatabase, string tablename)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.CreateTable(currentdatabase, newdatabase, tablename);
            return retValue;
        }



        public bool RemovePreviousYear(string voucherseries)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.RemovePreviousYear(voucherseries);
            return retValue;
        }

        public bool AddRowForCurrentAccountingYear(string newvoucherseries, string newsyear, string neweyear)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.IfCurrentYearExists(newvoucherseries);
            if (!retValue)
                retValue = dby.AddRowForCurrentAccountingYear(newvoucherseries, newsyear, neweyear);
            return retValue;
        }

        public bool DefinePrimaryKeys(string tablename, string primarykey)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.DefinePrimaryKeys(tablename, primarykey);
            return retValue;
        }

        public bool DefineUniqueKeys(string tablename, string primarykey)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.DefineUniqueKeys(tablename, primarykey);
            return retValue;
        }
        #endregion

    }
}
