using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class Messages : BaseObject
    {      

        #region Constructors, Destructors
        public Messages()
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

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
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
                    ValidationMessages.Add("Please enter Message.");

                DBMessages dbval = new DBMessages();
                if (IFEdit == "Y")
                {
                    if (dbval.IsNameUniqueForEdit(Name, Id))
                    {
                        ValidationMessages.Add("Message Already Exists.");
                    }
                }
                else
                {
                    if (dbval.IsNameUniqueForAdd(Name, Id))
                    {
                        ValidationMessages.Add("Message Already Exists.");
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
            bool bRetValue = true;  
            return bRetValue;
        }
        #endregion

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBMessages dbMessages = new DBMessages();
            return dbMessages.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBMessages dbMessages = new DBMessages();
                drow = dbMessages.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["MessageId"].ToString();
                    Name = Convert.ToString(drow["Message"]);
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }

        public int AddDetails()
        {
            DBMessages dbMessages = new DBMessages();
            return dbMessages.AddDetails(IntID , Name, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBMessages dbMessages = new DBMessages();
            return dbMessages.UpdateDetails(Id, Name, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBMessages dbMessages = new DBMessages();
            return dbMessages.DeleteDetails(Id);
        }

        public DataTable GetAreaList()
        {
            DBMessages dbMessages = new DBMessages();
            return dbMessages.GetMessageList();
        }
       

       
        #endregion

    }
}
