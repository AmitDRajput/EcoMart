using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    class Schedule : BaseObject
    {
        #region Declaration
        private string _SDescription;
        #endregion

        #region Constructors, Destructors
        public Schedule()
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
        public string SDescription
        {
            get { return _SDescription; }
            set { _SDescription = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            _SDescription = "";
        }

        public override void DoValidate()
        {
            try
            {
                if (SDescription == "")
                    ValidationMessages.Add("Please enter the Schedule Name.");

                DBSchedule dbSchedule = new DBSchedule();

                if (dbSchedule.IsNameUnique(SDescription, Id))
                {
                    ValidationMessages.Add("Schedule Name Already Exist.");
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }
        public override bool CanBeDeleted()
        {
            return false;
        }

        #endregion

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBSchedule dbschedule = new DBSchedule();
            return dbschedule.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBSchedule dbShelf = new DBSchedule();
                drow = dbShelf.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["ScheduleID"].ToString();
                    SDescription = Convert.ToString(drow["ScheduleName"]);
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
            return true;
        }

        public bool UpdateDetails()
        { 
            return true;
        }

        public bool DeleteDetails()
        {
            DBSchedule dbShelf = new DBSchedule();
            return dbShelf.DeleteDetails(Id);
        }


        #endregion

    }
}
