using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;
using EcoMart.InterfaceLayer;
using System.Collections;

namespace EcoMart.BusinessLayer
{
    class UserRights : BaseObject
    {
        #region Delcaration
        private string _FormName;       
        private int _AddLevel;
        private int _DeleteLevel;
        private int _EditLevel;
        private int _ViewLevel;
        private int _PrintLevel;
        private DataTable _BandItem;
        private bool _IsFormName;  
        #endregion
       
        #region Constructor Destructor
        public UserRights()
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
        #endregion

        #region Public Properties
        public string FormName
        {
            get { return _FormName;}
            set { _FormName = value;}
        }
       
        public int AddLevel
        {
            get { return _AddLevel; }
            set { _AddLevel = value; }
        }
        public int DeleteLevel
        {
            get { return _DeleteLevel; }
            set { _DeleteLevel = value; }
        }
        public int EditLevel
        {
            get { return _EditLevel; }
            set { _EditLevel = value; }
        }
        public int ViewLevel
        {
            get { return _ViewLevel; }
            set { _ViewLevel = value; }
        }
        public int PrintLevel
        {
            get { return _PrintLevel; }
            set { _PrintLevel = value; }
        }

        public DataTable BandItem
        {
            get { return _BandItem; }
            set { _BandItem = value; }
        }

        public bool IsFormName
        {
            get { return _IsFormName; }
            set { _IsFormName = value; }
        }

        #endregion
        
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _FormName = "";
                _AddLevel = 0;
                _DeleteLevel = 0;
                _EditLevel = 0;
                _ViewLevel = 0;
                _PrintLevel = 0;
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
                if (FormName == "")
                {
                    if (IsFormName)
                        ValidationMessages.Add("Please enter the Form Name.");
                    else
                        ValidationMessages.Add("Please enter the Report Name.");
                }
                if (IsFormName)
                {
                    if (AddLevel == -1)
                        ValidationMessages.Add("Please select The Add Right.");
                    if (EditLevel == -1)
                        ValidationMessages.Add("Please select The Edit Right.");
                    if (DeleteLevel == -1)
                        ValidationMessages.Add("Please select The Delete Right.");
                }
                if (PrintLevel == -1)
                    ValidationMessages.Add("Please select The Print Right.");
                if (ViewLevel == -1)
                    ValidationMessages.Add("Please select The View Right.");
                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #region public Methods
        public bool AddDetails(string ID)
        {
            DBUserRights dbRight = new DBUserRights();
            return dbRight.AddDetails(ID, FormName, AddLevel, EditLevel, DeleteLevel, ViewLevel, PrintLevel,CreatedBy,CreatedDate,CreatedTime);
        }
        public bool UpdateDetails()
        {
            DBUserRights dbRight = new DBUserRights();
            return dbRight.UpdateDetails(Id, FormName, AddLevel, EditLevel, DeleteLevel, ViewLevel, PrintLevel,ModifiedBy,ModifiedDate,ModifiedTime);
        }
        public DataTable GetLevel()
        {
            DBUserRights dbRight = new DBUserRights();
            return dbRight.GetLevel();
        }
        public DataTable GetLevel(string fname)
        {
            DBUserRights dbRight = new DBUserRights();
            return dbRight.GetLevel(fname);
        }
        public DataTable GetOverviewData()
        {
            DBUserRights dbRight = new DBUserRights();
            return dbRight.GetOverviewData();
        }
        public DataTable GetLevelData()
        {
            DBUserRights dbRight = new DBUserRights();
            return dbRight.GetLevelData();
        }

        public DataTable GetComboLevelData()
        {
            DBUserRights dbRight = new DBUserRights();
            return dbRight.GetComboLevelData();
        }
       
        public bool ReadDetailsByID()
        {
            DataRow dtrow = null;        
            bool retvalue = false;
            try
            {
                DBUserRights dbRight = new DBUserRights();
                dtrow = dbRight.ReadDetailsByID(FormName);

                if (dtrow != null)
                {
                    Id = dtrow["ID"].ToString();
                    FormName = dtrow["FormName"].ToString();
                    AddLevel = Convert.ToInt32(dtrow["AddModule"].ToString());
                    EditLevel = Convert.ToInt32(dtrow["EditModule"].ToString());
                    DeleteLevel = Convert.ToInt32(dtrow["DeleteModule"].ToString());
                    ViewLevel = Convert.ToInt32(dtrow["ViewModule"].ToString());
                    PrintLevel = Convert.ToInt32(dtrow["PrintModule"].ToString());

                    retvalue = true;

                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retvalue;
        }
        public bool DeleteDetails()
        {          
                DBUserRights dbRight = new DBUserRights();
                return dbRight.DeleteDetails(Id);            
        }

        public List<UserRights> GetRightList()
        {
            List<UserRights> retValue = new List<UserRights>();
            try
            {
                UserRights rights = new UserRights();
                DBUserRights dbRight = new DBUserRights();
                DataTable dTable = dbRight.ReadDetails();
                foreach (DataRow dtrow in dTable.Rows)
                {
                    rights = new UserRights();
                    rights.Id = dtrow["ID"].ToString();
                    rights.FormName = dtrow["FormName"].ToString();
                    rights.AddLevel = Convert.ToInt32(dtrow["AddModule"].ToString());
                    rights.EditLevel = Convert.ToInt32(dtrow["EditModule"].ToString());
                    rights.DeleteLevel = Convert.ToInt32(dtrow["DeleteModule"].ToString());
                    rights.ViewLevel = Convert.ToInt32(dtrow["ViewModule"].ToString());
                    rights.PrintLevel = Convert.ToInt32(dtrow["PrintModule"].ToString());
                    retValue.Add(rights);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        #endregion


    }
}
