using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
   public  class Doctor : BaseObject 
    {
        #region Declaration      
        private string _DocAddress;
        private string _DocTelephone;
        private string _DocEmailID;
        private string _DocShortNameAddress;
        private string _DocRegistrationNumber;
        private string _DocDegree;
        #endregion

        #region Constructors, Destructors
        public Doctor()
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

       
        public string DocAddress
        {
            get { return _DocAddress; }
            set { _DocAddress = value; }
        }

        public string DocTelephone
        {
            get { return _DocTelephone; }
            set { _DocTelephone = value; }
        }

        public string DocEmailID
        {
            get { return _DocEmailID; }
            set { _DocEmailID = value; }
        }

        public string DocShortNameAddress
        {
            get { return _DocShortNameAddress; }
            set { _DocShortNameAddress = value; }
        }
        public string DocRegistrationNumber
        {
            get { return _DocRegistrationNumber; }
            set { _DocRegistrationNumber = value; }
        }
        public string DocDegree
        {
            get { return _DocDegree; }
            set { _DocDegree = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _DocAddress = "";
                _DocEmailID = "";
                _DocShortNameAddress = "";
                _DocTelephone = "";
                _DocDegree = "";
                _DocRegistrationNumber = "";
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
                    ValidationMessages.Add("Please enter the Doctor Name.");
                if (DocAddress == "")
                    ValidationMessages.Add("Please enter the Doctor Address.");
                DBDoctor dbval = new DBDoctor();
                if (IFEdit == "Y")
                {
                    if (dbval.IsNameUniqueForEdit(Name, Id))
                    {
                        ValidationMessages.Add("Doctor Already Exists.");
                    }
                }
                else
                {
                    if (dbval.IsNameUniqueForAdd(Name, Id))
                    {
                        ValidationMessages.Add("Doctor Already Exists.");
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
                _rowcount = dbdelete.GetOverviewDataSelect("masterpatient", "DoctorID", Id);

                if (_rowcount != 0)
                    bRetValue = false;
                else
                {
                    bRetValue = true;
                    _rowcount = dbdelete.GetOverviewDataSelect("vouchersale", "DoctorID", Id);
                    if (_rowcount != 0)
                        bRetValue = false;
                    else
                    {
                        bRetValue = false;
                        _rowcount = dbdelete.GetOverviewDataSelect("masteraccount", "AccDoctorID", Id);
                        if (_rowcount == 0)
                            bRetValue = true;
                        else
                            bRetValue = false;
                    }
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
            DBDoctor dbDoc = new DBDoctor();
            return dbDoc.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBDoctor dbDoc = new DBDoctor();
                drow = dbDoc.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["DocId"].ToString();
                    Name = Convert.ToString(drow["DocName"]);
                    DocAddress = Convert.ToString(drow["DocAddress"]);
                    DocTelephone = Convert.ToString(drow["DocTelephone"]);
                    DocEmailID = Convert.ToString(drow["DocEmailID"]);
                    DocShortNameAddress = Convert.ToString(drow["DocShortNameAddress"]);
                    DocRegistrationNumber = Convert.ToString(drow["DocRegistrationNumber"]);
                    DocDegree = Convert.ToString(drow["DocDegree"]);
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
            DBDoctor dbDoc = new DBDoctor();
            return dbDoc.AddDetails(Id, Name , DocAddress,DocTelephone,DocEmailID,DocShortNameAddress,DocRegistrationNumber,DocDegree, CreatedBy, CreatedDate,CreatedTime );
        }

        public bool UpdateDetails()
        {
            DBDoctor dbDoc = new DBDoctor();
            return dbDoc.UpdateDetails(Id, Name, DocAddress, DocTelephone, DocEmailID, DocShortNameAddress,DocRegistrationNumber,DocDegree, ModifiedBy,ModifiedDate,ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBDoctor dbDoc = new DBDoctor();
            return dbDoc.DeleteDetails(Id);
        }

        #endregion      

        public DataTable GetDoctorsList()
        {
            DBDoctor dbData = new DBDoctor();
            return dbData.GetDoctorsList();
        }

        public DataTable GetSSDoctorsList()
        {
            DBDoctor dbData = new DBDoctor();
            return dbData.GetSSDoctorsList();
        }
    }
}
