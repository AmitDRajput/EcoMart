using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.Common;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class Favourite : BaseObject
    {
        #region Declaration      
        private string _ControlName;
        private int _OperationMode;
        private int _FavIndex;
        #endregion

        #region Constructors, Destructors
        public Favourite()
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


        public string ControlName
        {
            get { return _ControlName; }
            set { _ControlName = value; }
        }

        public int OperationMode
        {
            get { return _OperationMode; }
            set { _OperationMode = value; }
        }

        public int FavIndex
        {
            get { return _FavIndex; }
            set { _FavIndex = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _ControlName = "";
                _OperationMode = -1;
                _FavIndex = 0;
               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }     
     
        #endregion
        
        public List<Favourite> GetFavouriteList()
        {
            List<Favourite> favList = new List<Favourite>();
            try
            {
                DBFavourite dbData = new DBFavourite();
                DataTable dtList = dbData.GetFavouriteList();
                Favourite Fav = new Favourite();
                //Add items
                if (dtList != null && dtList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtList.Rows)
                    {
                        Fav = new Favourite();
                        Fav.Id = dr["FavouriteId"].ToString();
                        Fav.Name = dr["FavName"].ToString();
                        Fav.ControlName = dr["FavControlName"].ToString();
                        Fav.OperationMode = Convert.ToInt32(dr["FavOperationMode"].ToString());
                        Fav.FavIndex = Convert.ToInt32(dr["FavIndex"].ToString());
                        favList.Add(Fav);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return favList;
        }

        public int GetLastFavIndex()
        {
            int lastfavindex = 0;
            try
            {
                DBFavourite dbData = new DBFavourite();
                DataTable dtList = dbData.GetFavouriteList();
                if (dtList != null && dtList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtList.Rows)
                    {
                        lastfavindex = Convert.ToInt32(dr["FavIndex"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("Favourite.GetLastFavIndex:" + ex.Message);
            }
            return lastfavindex + 1;
        }

        public bool AddDetails()
        {
            DBFavourite dbData = new DBFavourite();            
            FavIndex = GetLastFavIndex();
            return dbData.AddDetails(Id, Name, ControlName, OperationMode, FavIndex);
        }

        public bool DeleteDetails()
        {
            DBFavourite dbData = new DBFavourite();
            return dbData.DeleteDetails(Id);
        }
    }
}
