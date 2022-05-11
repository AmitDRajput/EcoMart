using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{

    public class EmailID : BaseObject
    {
        #region Declaration
        private string _Details;
        # endregion
        #region Constructors, Destructors

        public EmailID()
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


        #region Internal Methods

        #region Properties

        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }

        #endregion

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
                if (Name == null || Name == "")
                    ValidationMessages.Add("Please enter EmailID");

                DBEmailID dbval = new DBEmailID();
                if (IFEdit == "Y")
                {
                    if (dbval.IsNameUniqueForEdit(Name, Id))
                    {
                        ValidationMessages.Add("EmailID Already Exists.");
                    }
                }
                else
                {
                    if (dbval.IsNameUniqueForAdd(Name, Id))
                    {
                        ValidationMessages.Add("EmailID Already Exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
           
            return bRetValue;
        }
        #endregion Internal Methods

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBEmailID dbe = new DBEmailID();
            return dbe.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBEmailID dbEmail = new DBEmailID();
                drow = dbEmail.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["EmailID"].ToString();
                    Name = Convert.ToString(drow["EmailName"]);
                    Details = Convert.ToString(drow["Description"]);

                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
            
        }

        public int AddDetails()
        {
            DBEmailID dbe = new DBEmailID();
            return dbe.AddDetails(IntID , Name, Details,CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBEmailID dbe = new DBEmailID();
            return dbe.UpdateDetails(Id, Name, Details);
        }

        public bool DeleteDetails()
        {
            DBEmailID dbe = new DBEmailID();
            return dbe.DeleteDetails(Id);
        }

        public DataTable GetAreaList()
        {
            DBEmailID dbe = new DBEmailID();
            return dbe.GetOverviewData();
        }

        #endregion Public Methods
    }
}
