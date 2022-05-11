using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    class Pack : BaseObject
    {
        #region Declaration
        private string _Code;
        private string _Pack;
        #endregion

        #region Constructors, Destructors
        public Pack()
        {
            try
            {
                Initialise();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }
        #endregion

        #region Properties
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public string PPack
        {
            get { return _Pack; }
            set { _Pack = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _Code = "";
                _Pack = "";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }

        public override void DoValidate()
        {
            try
            {
                if (PPack == "")
                    ValidationMessages.Add("Please enter the Pack");             
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
          
        }
        public override bool CanBeDeleted()
        {
            bool bRetValue = false;
            try
            {
                int _rowcount = 0;
                DBDelete dbdelete = new DBDelete();
                _rowcount = dbdelete.GetOverviewDataSelect("masterproduct", "ProdShelfID", Id);
                if (_rowcount == 0)
                    bRetValue = true;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return bRetValue;
        }

        #endregion

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBPack dbpack = new DBPack();
            return dbpack.GetOverviewData();
        }
        public DataTable GetOverviewDataForPackType()
        {
            DBPack dbpack = new DBPack();
            return dbpack.GetOverviewDataForPackType();
        }
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBShelf dbShelf = new DBShelf();
                drow = dbShelf.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["ShelfId"].ToString();                
                    Description = Convert.ToString(drow["ShelfDescription"]);
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }

        public bool AddDetails()
        {
            DBPack dbPack = new DBPack();
            return dbPack.AddDetails(Id, PPack);
        }

        public bool UpdateDetails()
        {
            DBPack dbPack = new DBPack();
            return dbPack.UpdateDetails(Id, PPack);
        }

        public bool DeleteDetails()
        {
            DBPack dbPack = new DBPack();
            return dbPack.DeleteDetails(Id);
        }


        #endregion


      
    }
}
