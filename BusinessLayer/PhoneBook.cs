using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;


namespace EcoMart.BusinessLayer
{
    public class PhoneBook : BaseObject
    {
        #region Declaration
        //private string _Address1;
        //private string _Address2;
        private string _bday;
        private string _bmonth;
        private string _byear;
    //    private string _Telephone;
        private string _MobileNumberForSMS;
        private string _EmailID;
        private string _Remark;
        #endregion

        #region Constructors, Destructors
        public PhoneBook()
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

        public string PMobileNumberForSMS
        {
            get { return _MobileNumberForSMS; }
            set { _MobileNumberForSMS = value; }
        }
        //public string Address1
        //{
        //    get { return _Address1; }
        //    set { _Address1 = value; }
        //}
        //public string Address2
        //{
        //    get { return _Address2; }
        //    set { _Address2 = value; }
        //} // 17/10
        public string Bday
        {
            get { return _bday; }
            set { _bday = value; }
        }
        public string Bmonth
        {
            get { return _bmonth; }
            set { _bmonth = value; }
        }
        public string Byear
        {
            get { return _byear; }
            set { _byear = value; }
        }
        //public string Telephone
        //{
        //    get { return _Telephone; }
        //    set { _Telephone = value; }
        //}

        public string EmailID
        {
            get { return _EmailID; }
            set { _EmailID = value; }
        }

        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
       
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                //_Address1 = "";
                //_Address2 = "";
                _EmailID = "";             
              //  _Telephone = "";
                _bday = "";
                _bmonth = "";
                _byear = "";
                _Remark = "";
               
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
                    ValidationMessages.Add("Please enter Name.");
                if (MobileNumberForSMS == "")
                    ValidationMessages.Add("Please enter Mobile Number.");
                DBPhoneBook dbval = new DBPhoneBook();
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
            bool bRetValue = true;
           

            //    if (_rowcount != 0)
            //        bRetValue = false;
            //    else
            //    {
            //        bRetValue = true;
            //        _rowcount = dbdelete.GetOverviewDataSelect("vouchersale", "DoctorID", Id);
            //        if (_rowcount != 0)
            //            bRetValue = false;
            //        else
            //        {
            //            bRetValue = false;
            //            _rowcount = dbdelete.GetOverviewDataSelect("masteraccount", "AccDoctorID", Id);
            //            if (_rowcount == 0)
            //                bRetValue = true;
            //            else
            //                bRetValue = false;
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteException(Ex);
            //}
            return bRetValue;
        }
        #endregion


        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBPhoneBook dbDoc = new DBPhoneBook();
            return dbDoc.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBPhoneBook dbph = new DBPhoneBook();
                drow = dbph.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["Id"].ToString();
                    Name = Convert.ToString(drow["Name"]);
                    Address1 = drow["Address1"].ToString();
                    Address2 = drow["Address2"].ToString();
                    Telephone = drow["Telephone"].ToString();
                    MobileNumberForSMS = drow["MobileNumberForSMS"].ToString();
                    EmailID = drow["EmailID"].ToString();
                    Bday = drow["BirthDay"].ToString();
                    Bmonth = drow["BirthMonth"].ToString();
                    Byear = drow["BirthYear"].ToString();
                    Remark = drow["Remark"].ToString();
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
            DBPhoneBook dbDoc = new DBPhoneBook();
            return dbDoc.AddDetails(Id, Name, Address1,Address2,Telephone,MobileNumberForSMS, EmailID,Bday,Bmonth,Byear,Remark, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBPhoneBook dbDoc = new DBPhoneBook();
            return dbDoc.UpdateDetails(Id, Name, Address1, Address2, Telephone, MobileNumberForSMS, EmailID, Bday, Bmonth, Byear, Remark, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBPhoneBook dbDoc = new DBPhoneBook();
            return dbDoc.DeleteDetails(Id);
        }

        public DataTable GetDoctorsList()
        {
            DBPhoneBook dbData = new DBPhoneBook();
            return dbData.GetDoctorsList();
        }

        public DataTable GetSSDoctorsList()
        {
            DBPhoneBook dbData = new DBPhoneBook();
            return dbData.GetSSDoctorsList();
        }
        #endregion
    }
}
