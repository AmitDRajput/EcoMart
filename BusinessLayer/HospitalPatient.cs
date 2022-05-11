using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSDistributorPlus.DataLayer;
using PharmaSYSDistributorPlus.Common;

namespace PharmaSYSDistributorPlus.BusinessLayer
{

    class HospitalPatient : BaseObject
    {
        #region Declaration
        private string _InwardNumber;
        private string _Address1;
        private string _Address2;
        private string _Telephone;
        private string _Email;
        private string _ShortNameAddress;
        private int _Bday;
        private int _Bmonth;
        private int _Byear;
        private int _Ageyears;
        private int _Agemonths;
        private int _Agedays;
        private string _RoomNumber;
        private string _IDNumber;
        private string _AccountID;
        private string _DoctorID;
        private string _WardID;
        private string _Remark1;
        private string _Remark2;
        private string _Remark3;
        private string _Gender;

        private int _StatementNumber;
        private string _StatementDate;
        private double _StatementAmount;

        #endregion

        #region Constructors, Destructors
        public HospitalPatient()
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
        #endregion Constructors, Destructors


        #region Properties

        public string InwardNumber
        {
            get { return _InwardNumber; }
            set { _InwardNumber = value; }
        }

        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string ShortNameAddress
        {
            get { return _ShortNameAddress; }
            set { _ShortNameAddress = value; }
        }

        public int Bday
        {
            get { return _Bday; }
            set { _Bday = value; }
        }

        public int Bmonth
        {
            get { return _Bmonth; }
            set { _Bmonth = value; }
        }

        public int Byear
        {
            get { return _Byear; }
            set { _Byear = value; }
        }

        public int Ageyears
        {
            get { return _Ageyears; }
            set { _Ageyears = value; }
        }
        public int Agemonths
        {
            get { return _Agemonths; }
            set { _Agemonths = value; }
        }
        public int Agedays
        {
            get { return _Agedays; }
            set { _Agedays = value; }
        }
        public string RoomNumber
        {
            get { return _RoomNumber; }
            set { _RoomNumber = value; }
        }
        public string IDNumber
        {
            get { return _IDNumber; }
            set { _IDNumber = value; }
        }

        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        public string DoctorID
        {
            get { return _DoctorID; }
            set { _DoctorID = value; }
        }

        public string Remark1
        {
            get { return _Remark1; }
            set { _Remark1 = value; }
        }

        public string Remark2
        {
            get { return _Remark2; }
            set { _Remark2 = value; }
        }

        public string Remark3
        {
            get { return _Remark3; }
            set { _Remark3 = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public string WardID
        {
            get { return _WardID; }
            set { _WardID = value; }
        }

        public int StatementNumber
        {
            get { return _StatementNumber; }
            set { _StatementNumber = value; }
        }
        public string StatementDate
        {
            get { return _StatementDate; }
            set { _StatementDate = value; }
        }
        public double StatementAmount
        {
            get { return _StatementAmount; }
            set { _StatementAmount = value; }

        }
        #endregion


        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBHospitalPatient dbPatient = new DBHospitalPatient();
            return dbPatient.GetOverviewData();
        }

        public DataTable GetOverviewDataForStatement()
        {
            DBHospitalPatient dbPatient = new DBHospitalPatient();
            return dbPatient.GetOverviewDataForStatement();
        }

        //public DataTable GetOverviewDataForCounterSale()
        //{
        //    DBHospitalPatient dbPatient = new DBHospitalPatient();
        //    return dbPatient.GetOverviewDataForCounterSale();
        //}

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _Address1 = "";
                _Address2 = "";
                _Telephone = "";
                _Email = "";
                _Bday = 0;
                _Bmonth = 0;
                _Byear = 0;
                _Remark1 = "";
                _Remark2 = "";
                _Remark3 = "";
                _ShortNameAddress = "";
                _DoctorID = "";
                _AccountID = "";
                _WardID = "";
                _Agedays = 0;
                _Agemonths = 0;
                _Ageyears = 0;
                _Gender = "";
                _IDNumber = "";
                _InwardNumber = "";
                _RoomNumber = "";
                _StatementNumber = 0;
                _StatementDate = "";
                _StatementAmount = 0;    
            }
            catch (Exception Ex)
            {
                Log.WriteError("Patient.Initialise>>" + Ex.Message);
            }

        }

        public override void DoValidate()
        {
            DBHospitalPatient dbPatient = new DBHospitalPatient();
            if (Name == "")
                ValidationMessages.Add("Please enter the Patient Name.");
            if (Address1 == "")
                ValidationMessages.Add("Please enter Patient Address.");

            if (Name != "")
            {

                if (IFEdit == "Y")
                {
                    if (dbPatient.IsNameUniqueForEdit(Name, Id))
                    {
                        ValidationMessages.Add("Patient Already Exists.");
                    }
                }
                else
                {
                    if (dbPatient.IsNameUniqueForAdd(Name, Id))
                    {
                        ValidationMessages.Add("Patient Already Exists.");
                    }
                }
            }
            
        }
        #endregion

        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            int countrec = 0;
            DBDelete patdele = new DBDelete();
            countrec = patdele.GetOverviewDataSelect("vouchersale", "PatientID", Id);
            if (countrec == 0)
                bRetValue = true;
            else
                bRetValue = false;
            return bRetValue;
        }



        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            DBHospitalPatient dbPatient = new DBHospitalPatient();
            drow = dbPatient.ReadDetailsByID(Id);

            try
            {
                if (drow != null)
                {
                    if (drow["ID"] != DBNull.Value)
                        Id = drow["Id"].ToString();
                    if (drow["InwardNumber"] != DBNull.Value)
                        InwardNumber = Convert.ToString(drow["InwardNumber"]);
                    if (drow["PatientName"] != DBNull.Value)
                        Name = Convert.ToString(drow["PatientName"]);
                    if (drow["Address1"] != DBNull.Value)
                        Address1 = Convert.ToString(drow["Address1"]);
                    if (drow["Address2"] != DBNull.Value)
                        Address2 = Convert.ToString(drow["Address2"]);
                    if (drow["ShortNameAddress"] != DBNull.Value)
                        ShortNameAddress = Convert.ToString(drow["ShortNameAddress"]);
                    if (drow["Telephone"] != DBNull.Value)
                        Telephone = Convert.ToString(drow["Telephone"]);
                    if (drow["Email"] != DBNull.Value)
                        Email = Convert.ToString(drow["Email"]);
                    if (drow["RoomNumber"] != DBNull.Value)
                        RoomNumber = Convert.ToString(drow["RoomNumber"]);
                    if (drow["IDNumber"] != DBNull.Value)
                        IDNumber = Convert.ToString(drow["IDNumber"]);
                    if (drow["BirthDay"] != DBNull.Value)
                        Bday = Convert.ToInt32(drow["BirthDay"]);
                    if (drow["BirthMonth"] != DBNull.Value)
                        Bmonth = Convert.ToInt32(drow["BirthMonth"]);
                    if (drow["BirthYear"] != DBNull.Value)
                        Byear = Convert.ToInt32(drow["BirthYear"]);
                    if (drow["AgeYears"] != DBNull.Value)
                        Ageyears = Convert.ToInt32(drow["AgeYears"]);
                    if (drow["Agemonths"] != DBNull.Value)
                        Agemonths = Convert.ToInt32(drow["Agemonths"]);
                    if (drow["Agedays"] != DBNull.Value)
                        Agedays = Convert.ToInt32(drow["Agedays"]);
                    if (drow["WardID"] != DBNull.Value)
                        WardID = Convert.ToString(drow["WardID"]);                   
                    if (drow["DoctorID"] != DBNull.Value)
                        DoctorID = Convert.ToString(drow["DoctorID"]);
                    if (drow["Remark1"] != DBNull.Value)
                        Remark1 = Convert.ToString(drow["Remark1"]);
                    if (drow["Remark2"] != DBNull.Value)
                        Remark2 = Convert.ToString(drow["Remark2"]);
                    if (drow["Remark3"] != DBNull.Value)
                        Remark3 = Convert.ToString(drow["Remark3"]);
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = Convert.ToString(drow["AccountID"]);
                    if (drow["Gender"] != DBNull.Value)
                        Gender = Convert.ToString(drow["Gender"]);

                    if (drow["StatementNumber"] != DBNull.Value)
                        StatementNumber = Convert.ToInt32(drow["StatementNumber"]);
                    if (drow["StatementDate"] != DBNull.Value)
                        StatementDate = Convert.ToString(drow["StatementDate"]);
                    if (drow["StatementAmount"] != DBNull.Value)
                        StatementAmount = Convert.ToDouble(drow["StatementAmount"]);

                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("Patient.ReadDetailsByID>>" + Ex.Message);
            }
            return retValue;
        }


        public DataTable ReadProdDetailsByIdForDebtorSale(string Id)
        {
            DataTable dt = new DataTable();
            dt = null;
            DBDebtorProduct dbdbprod = new DBDebtorProduct();
            dt = dbdbprod.ReadProdDetailsByIdForDebtorSale(Id);
            return dt;
        }


        public bool AddDetails()
        {
            DBHospitalPatient DBHospitalPatient = new DBHospitalPatient();
            return DBHospitalPatient.AddDetails(Id, InwardNumber, Name, Address1, Address2, Telephone, Email,
                 ShortNameAddress, Bday, Bmonth, Byear, Ageyears, Agemonths, Agedays, Gender, RoomNumber, DoctorID,
                Remark1, Remark2, Remark3, IDNumber, AccountID,WardID, CreatedBy, CreatedDate, CreatedTime);           
        }

        public bool UpdateDetails()
        {
            DBHospitalPatient dbPatient = new DBHospitalPatient();
            return dbPatient.UpdateDetails(Id, InwardNumber, Name, Address1, Address2, Telephone, Email,
                 ShortNameAddress, Bday, Bmonth, Byear, Ageyears, Agemonths, Agedays, Gender, RoomNumber, DoctorID,
                Remark1, Remark2, Remark3, IDNumber, AccountID, WardID, ModifiedBy, ModifiedDate, ModifiedTime);           
        }

        public bool DeleteDetails()
        {
            DBHospitalPatient dbPatient = new DBHospitalPatient();
            return dbPatient.DeleteDetails(Id);
        }

        public int GetAccTokenNumber()
        {
            int tokennumber = 0;
            DataRow dr = null;
            DBAccount dbdata = new DBAccount();
            try
            {
                dr = dbdata.GetTokenNumber();
                if (dr != null)
                {
                    if (dr["Tokennumber"] != DBNull.Value)
                        tokennumber = Convert.ToInt32(dr["TokenNumber"].ToString());
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("Patient.GetAccTokenNumber>>" + Ex.Message);
            }
            return tokennumber + 1;
        }

        public int GetAndUpdateTokenNumber()
        {
            int tokennumber = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                //       tokennumber = dbno.GetTokenNumber();
            }
            catch (Exception Ex)
            {
                Log.WriteError("Patient.GetAndUpdateTokenNumber>>" + Ex.Message);
            }
            return tokennumber;
        }
        public DataTable GetRoomNumber() // [ansuman]
        {
            DBHospitalPatient hp = new DBHospitalPatient();
            return hp.GetRoomNumber();
        }

        #endregion

    }
}
