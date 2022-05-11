using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class Shelf : BaseObject
    {
        #region Declaration
        private string _Code;   
        #endregion

        #region Constructors, Destructors
        public Shelf()
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
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            _Code = "";    
        }

        public override void DoValidate()
        {
            try
            {
                if (Code == "")
                    ValidationMessages.Add("Please enter the Shelf Code.");          

                DBShelf dbval = new DBShelf();
                if (IFEdit == "Y")
                {
                    if (dbval.IsNameUniqueForEdit(Name, Id))
                    {
                        ValidationMessages.Add("Shelf Already Exists.");
                    }
                }
                else
                {
                    if (dbval.IsNameUniqueForAdd(Name, Id))
                    {
                        ValidationMessages.Add("Shelf Already Exists.");
                    }
                }
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
            DBShelf dbShelf = new DBShelf();
            return dbShelf.GetOverviewData();
        }

        public DataTable GetOverviewDataForMultiSelection()
        {
            DBShelf dbShelf = new DBShelf();
            return dbShelf.GetOverviewDataForMultiSelection();
        }
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            DBShelf dbShelf = new DBShelf();
            try
            {
                drow = dbShelf.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["ShelfId"].ToString();
                    Code = Convert.ToString(drow["ShelfCode"]);
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

        //public bool ReadProductDetailsByID()
        //{
        //    bool retValue = false;
        //    DataTable dt = new DataTable();
        //    DBShelf dbShelf = new DBShelf();
        //    try
        //    {
        //        drow = dbShelf.ReadProductDetailsByID(Id);

        //        if (drow != null)
        //        {
        //            Id = drow["ShelfId"].ToString();
        //            Code = Convert.ToString(drow["ShelfCode"]);
        //            Description = Convert.ToString(drow["ShelfDescription"]);
        //            retValue = true;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //    return retValue;
        //}

        public bool AddDetails()
        {
            DBShelf dbShelf = new DBShelf();
            return dbShelf.AddDetails(Id, Code, Description,CreatedBy,CreatedDate,CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBShelf dbShelf = new DBShelf();
            return dbShelf.UpdateDetails(Id, Code, Description,ModifiedBy,ModifiedDate,ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBShelf dbShelf = new DBShelf();
            return dbShelf.DeleteDetails(Id);
        }

        
        #endregion      

    }
}
