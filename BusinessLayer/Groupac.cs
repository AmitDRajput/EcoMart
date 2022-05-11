using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class Groupac : BaseObject
    {
        # region Declaration       
        private string _UnderGroup;
        private string _UnderGroupId;
        private string _IfMainGroup;
        private string _ifFix;
        private int _GroupIDInteger;
        private int _UnderGroupIDParentID;

        # endregion

        #region Constructors, Destructors
        public Groupac()
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

     
        public string UnderGroup
        {
            get { return _UnderGroup; }
            set { _UnderGroup = value; }
        }
        public string UnderGroupId
        {
            get { return _UnderGroupId; }
            set { _UnderGroupId = value; }
        }
        public string IfMainGroup
        {
            get { return _IfMainGroup;}
            set { _IfMainGroup = value; }
        }
        public string IfFix
        {
            get { return _ifFix; }
            set { _ifFix = value; }
        }

        public int GroupIDInteger
        {
            get { return _GroupIDInteger; }
            set { _GroupIDInteger = value; }
        }
        public int UnderGroupIDParentID
        {
            get { return _UnderGroupIDParentID; }
            set { _UnderGroupIDParentID = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();

                _UnderGroup = null;
                _UnderGroupId = null;
                _IfMainGroup = " ";
                _ifFix = " ";
                _UnderGroupIDParentID = 0;
                _GroupIDInteger = 0;
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
                if (Name == "")
                    ValidationMessages.Add("Please enter the Group Name.");

                DBGroup dbval = new DBGroup();
                if (IFEdit == "Y")
                {
                    if (dbval.IsNameUniqueForEdit(Name, Id))
                    {
                        ValidationMessages.Add("Name Already Exists.");
                    }
                }
                else
                {
                    if (dbval.IsNameUniqueForAdd(Name, Id))
                    {
                        ValidationMessages.Add("Name Already Exists.");
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
                _rowcount = dbdelete.GetOverviewDataSelect("masteraccount", "AccGroupID", Id);
                if (_rowcount == 0)
                {
                    _rowcount = dbdelete.GetOverviewDataSelect("mastergroup", "UnderGroupID", Id);
                    if (_rowcount == 0)
                        bRetValue = true;
                }
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
            DBGroup dbGroup = new DBGroup();
            return dbGroup.GetOverviewData();
        }
     
        public DataTable GetOverviewDataALL()
        {
            DBGroup dbGroup = new DBGroup();
            return dbGroup.GetOverviewDataALL();
        }

        public DataTable GetOverviewDataForFixedCode(string code)
        {
            DBGroup dbGroup = new DBGroup();
            return dbGroup.GetOverviewDataForFixedCode(code);
        }

        public DataTable GetOverviewDataForGeneral()
        {
            DBGroup dbGroup = new DBGroup();
            return dbGroup.GetOverviewDataForGeneral();

        }
        public int GetGroupIDInteger()
        {
            int groupnumber = 0;
            int mgroupnumber = 0;
            DataTable dt;
            DBGroup dbGroup = new DBGroup();
            dt = dbGroup.GetGroupIDInteger();
            foreach (DataRow dr in dt.Rows)
            {
                mgroupnumber = 0;
                if (dr["GroupID"] != DBNull.Value && dr["GroupID"].ToString() != string.Empty)
                    mgroupnumber = Convert.ToInt32(dr["GroupID"].ToString());
                if (groupnumber < mgroupnumber)
                    groupnumber = mgroupnumber;

            }
            return mgroupnumber + 1;
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBGroup dbGroup = new DBGroup();
                drow = dbGroup.ReadDetailsByID(Id);

                if (drow != null)
                {
                    GroupIDInteger = Convert.ToInt32(drow["GroupId"].ToString());
                    Name = Convert.ToString(drow["GroupName"]);
                    if (drow["IfMainGroup"] != null)
                        IfMainGroup = Convert.ToString(drow["IfMainGroup"]);
                    UnderGroupId = drow["UnderGroupId"].ToString();
                    if (drow["IFFIX"] != null)
                        IfFix = drow["IFFIX"].ToString();
                    if (drow["UnderGroupIDParentID"] != null)
                        UnderGroupIDParentID = Convert.ToInt32(drow["UnderGroupIDParentID"].ToString());
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
            DBGroup dbGroup = new DBGroup();
            return dbGroup.AddDetails(GroupIDInteger, Name, UnderGroupId, IfFix, IfMainGroup,UnderGroupIDParentID, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBGroup dbGroup = new DBGroup();
            return dbGroup.UpdateDetails(GroupIDInteger, Name, UnderGroupId, IfFix, IfMainGroup, UnderGroupIDParentID, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBGroup dbGroup = new DBGroup();
            return dbGroup.DeleteDetails(GroupIDInteger.ToString());
        }

        #endregion


      
    }
}

    

