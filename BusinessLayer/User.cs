using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;
using EcoMart.DataLayer;
namespace EcoMart.BusinessLayer
{
    public class User:BaseObject
    {
        #region Declaration
        private string _UserName;
        private string _Password;
        private string _RetypePassword;
        private int _Level;
        private string _Details;
        private int _IfInUse;
        private string _MakeItDefault;
       
        #endregion

        #region Constructor Destructor
        public User()
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

        public string MakeItDefault
        {
            get { return _MakeItDefault; }
            set { _MakeItDefault = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string RetypePassword
        {
            get { return _RetypePassword; }
            set { _RetypePassword = value; }
        }
        
        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
       
        public int IfInUse
        {
            get { return _IfInUse; }
            set { _IfInUse = value; }
        }
        
        

        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _UserName = null;
                _Password = null;
                _RetypePassword = null;
                _Level = -1;
                _Details = null;
                _IfInUse = 0;
                _MakeItDefault = "N";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }
        #endregion

        #region  Validations

        public override bool CanBeDeleted()
        {
            bool bRetValue = false;
            try
            {
                int _rowcount = 0;
                DBDelete dbdelete = new DBDelete();
                _rowcount = dbdelete.GetOverviewDataSelect("vouchersale", "CreatedUserID", Id);
                if (_rowcount == 0)
                {
                    _rowcount = dbdelete.GetOverviewDataSelect("vouchersale", "ModifiedUserID", Id);
                    if (_rowcount == 0)
                    {
                        _rowcount = dbdelete.GetOverviewDataSelect("voucherpurchase", "CreatedUserID", Id);
                        if (_rowcount == 0)
                        {
                            _rowcount = dbdelete.GetOverviewDataSelect("voucherpurchase", "ModifiedUserID", Id);
                            if (_rowcount == 0)
                            {
                                _rowcount = dbdelete.GetOverviewDataSelect("tbltrnac", "CreatedUserID", Id);
                                if (_rowcount == 0)
                                {
                                    _rowcount = dbdelete.GetOverviewDataSelect("vouchercreditdebitnote", "CreatedUserID", Id);
                                    if (_rowcount == 0)
                                        bRetValue = true;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return bRetValue;
        }



        public override void DoValidate()
        {
            try
            {
                if (_UserName == "")
                    ValidationMessages.Add("Please enter the  User Name.");
                if (_Password == "")
                    ValidationMessages.Add("Please enter the  Password.");
                if (_Level == -1)
                    ValidationMessages.Add("Please Enter the Level");
                if (_Details == "")
                    ValidationMessages.Add("Enter the Details");
                if (Name != "")
                {
                    DBUser dbuser = new DBUser();
                    if (IFEdit == "Y")
                    {
                        if (dbuser.IsNameUniqueForEdit(Name, Id))
                        {
                            ValidationMessages.Add("User Already Exists.");
                        }
                    }
                    else
                    {
                        if (dbuser.IsNameUniqueForAdd(Name, Id))
                        {
                            ValidationMessages.Add("User Already Exists.");
                        }
                    }      
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }


        #endregion

        #region Public Method
        public bool AddDetails()
        {
            DBUser dbAddUser = new DBUser();
            return dbAddUser.AddDetails(Id, Name,Password,IfInUse,MakeItDefault, Level,Description,CreatedBy,CreatedDate,CreatedTime);
                       
        }
        public bool UpdateDetails()
        {
            DBUser dbadduser = new DBUser();
            return dbadduser.UpdateDetails(Id, Name, Password, Level, IfInUse,MakeItDefault, Description, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public void MakeAllMakeItDefaultNo()
        {
            DBUser dbadduser = new DBUser();
            dbadduser.MakeAllMakeItDefaultNo();
        }
        public DataTable GetOverviewData()
        {
            DBUser dbAddUser = new DBUser();
            DataTable dbs = null;
            dbs = dbAddUser.GetOverviewData();
           // return dbAddUser.GetOverviewData();
            return dbs;
        }
        public DataTable GetLevelData()
        {
        DBUser dbuser=new DBUser();
        return dbuser.GetLevelData();
        }

        public DataRow GetDefaultUser()
        {
            DBUser dbuser = new DBUser();
            return dbuser.GetDefaultUser();
        }
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBUser dbAddUser = new DBUser();
                drow = dbAddUser.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["UserID"].ToString();
                    Name = Convert.ToString(drow["UserName"]);
                    Password = Convert.ToString(drow["Password"]);
                    RetypePassword = Convert.ToString(drow["Password"]);
                    IfInUse = Convert.ToInt16(drow["IfInUse"]);
                    MakeItDefault = Convert.ToString(drow["MakeItDefault"]);
                    Level = Convert.ToInt32(drow["Level"]);
                    Description = Convert.ToString(drow["UserDetails"]);

                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }
        public bool DeleteDetails()
        {
            DBUser dbAddUser = new DBUser();
            return dbAddUser.DeleteDetails(Id,IfInUse);
        }

        public User GetUserByUserNameAndPassword(string userName, string password)
        {
            User retValue = null;
            try
            {
                DBUser dbUser = new DBUser();
                DataTable dTable = dbUser.GetUserByUserNameAndPassword(userName, password);
                if (dTable != null && dTable.Rows.Count > 0)
                {
                    retValue = new User();
                    DataRow drow = dTable.Rows[0];
                    retValue.Id = drow["UserID"].ToString();
                    retValue.Name = Convert.ToString(drow["UserName"]);
                    retValue.Password = Convert.ToString(drow["Password"]);
                    retValue.IfInUse = Convert.ToInt16(drow["IfInUse"]);
                    retValue.MakeItDefault = Convert.ToString(drow["MakeItDefault"]);
                    retValue.Level = Convert.ToInt32(drow["Level"]);
                    retValue.Description = Convert.ToString(drow["UserDetails"]);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }

        #endregion




    
    }
}
