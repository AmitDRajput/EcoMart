using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSDistributorPlus.DataLayer;
using PharmaSYSDistributorPlus.Common;

namespace PharmaSYSDistributorPlus.BusinessLayer
{
    class Patient : BaseObject
    {
        #region Declaration
        private string _Address1;
        private string _Address2;
        private string _Telephone;
        private string _Email;
        private int _Bday;
        private int _Bmonth;
        private int _Byear;
        private string _Remark1;
        private string _Remark2;
        private string _Remark3;
        private int _Visit1;
        private int _Visit2;
        private int _Visit3;
        private string _ShortNameAddress;
        private string _DoctorName;
        private string _DoctorAddress;
        private string _DoctorTelephone;
        private string _DoctorEmail;
        private string _DoctorID;
        private string _ProductID;
        private int _Quantity;
        private bool _DuplicateProduct;
        private string _Gender;
        private double _DiscountOffered;
        private string _PutInBlackList;

        private int _AccTokenNumber;
        private int _CurrentTokenNumber;

        private string _MobileNumberForSMS;
        #endregion

        #region Constructors, Destructors
        public Patient()
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

        public string PutInBlackList
        {
            get { return _PutInBlackList; }
            set { _PutInBlackList = value; }
        }

        public string PMobileNumberForSMS
        {
            get { return _MobileNumberForSMS; }
            set { _MobileNumberForSMS = value; }
        }
        public double DiscountOffered
        {
            get { return _DiscountOffered; }
            set { _DiscountOffered = value; }
        }
        public bool DuplicateProduct
        {
            get { return _DuplicateProduct; }
            set { _DuplicateProduct = value; }
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
        public int Visit1
        {
            get { return _Visit1; }
            set { _Visit1 = value; }
        }
        public int Visit2
        {
            get { return _Visit2; }
            set { _Visit2 = value; }
        }

        public int Visit3
        {
            get { return _Visit3; }
            set { _Visit3 = value; }
        }

        public string ShortNameAddress
        {
            get { return _ShortNameAddress; }
            set { _ShortNameAddress = value; }
        }
        public string DoctorName
        {
            get { return _DoctorName; }
            set { _DoctorName = value; }
        }
        public string DoctorAddress
        {
            get { return _DoctorAddress; }
            set { _DoctorAddress = value; }
        }

        public string DoctorTelephone
        {
            get { return _DoctorTelephone; }
            set { _DoctorTelephone = value; }
        }

        public string DoctorEmail
        {
            get { return _DoctorEmail; }
            set { _DoctorEmail = value; }
        }
        public string DoctorID
        {
            get { return _DoctorID; }
            set { _DoctorID = value; }
        }
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }

        }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public int AccTokenNumber
        {
            get { return _AccTokenNumber; }
            set { _AccTokenNumber = value; }
        }
        public int CurrentTokenNumber
        {
            get { return _CurrentTokenNumber; }
            set { _CurrentTokenNumber = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        #endregion

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBPatient dbPatient = new DBPatient();
            return dbPatient.GetOverviewData();
        }
        public DataTable GettxtAddress1()
        {
            DBPatient dbPatient = new DBPatient();
            return dbPatient.GettxtAddress1();
        }
        public DataTable GettxtAddress2()
        {
            DBPatient dbPatient = new DBPatient();
            return dbPatient.GettxtAddress2();
        }
        public DataTable GetOverviewDataForCounterSale()
        {
            DBPatient dbPatient = new DBPatient();
            return dbPatient.GetOverviewDataForCounterSale();
        }
        public DataTable GetOverviewDataForCounterSaleForOnlyCashSale()
        {
            DBPatient dbPatient = new DBPatient();
            return dbPatient.GetOverviewDataForCounterSaleForOnlyCashSale();
        }
        public DataTable GetTodaysBirthdayPatient(DateTime Date)
        {
            DBPatient dbPatient = new DBPatient();
            return dbPatient.GetTodaysBirthdayPatient(Date);
        }
  
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
                _Visit1 = 0;
                _Visit2 = 0;
                _Visit3 = 0;
                _ShortNameAddress = "";
                _DoctorName = "";
                _DoctorAddress = "";
                _DoctorTelephone = "";
                _DoctorEmail = "";
                _DoctorID = "";
                _Quantity = 0;
                _ProductID = "";
                _DuplicateProduct = false;
                _AccTokenNumber = 0;
                _CurrentTokenNumber = 0;
                _Gender = "M";
                _DiscountOffered = 0;
                _MobileNumberForSMS = "";
                _PutInBlackList = "N";
            }
            catch (Exception Ex)
            {
                Log.WriteError("Patient.Initialise>>" + Ex.Message);
            }

        }

        public override void DoValidate()
        {
            DBPatient dbPatient = new DBPatient();
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
            if (AccTokenNumber > 0)
            {
                if (IFEdit == "Y")
                {
                    if (dbPatient.IsTokenNumberUniqueForEdit(AccTokenNumber, Id))
                    {
                        ValidationMessages.Add("Token Number Already Exists.");
                    }
                }
                else
                {
                    if (dbPatient.IsTokenNumberUniqueForAdd(AccTokenNumber, Id))
                    {
                        ValidationMessages.Add("Token Number Already Exists.");
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

        public bool ReadDoctorDetailsByID()
        {
            bool retValue = false;
            DataRow docdrow = null;
            DBPatient dbdoctor = new DBPatient();
            docdrow = dbdoctor.ReadDoctorDetailsByID(DoctorID);
            try
            {
                if (docdrow != null)
                {
                    if (docdrow["DocName"] != DBNull.Value)
                        DoctorName = Convert.ToString(docdrow["DocName"]);
                    if (docdrow["DocAddress"] != DBNull.Value)
                        DoctorAddress = Convert.ToString(docdrow["DocAddress"]);
                    if (docdrow["DocTelephone"] != DBNull.Value)
                        DoctorTelephone = Convert.ToString(docdrow["DocTelephone"]);
                    if (docdrow["DocEmailID"] != DBNull.Value)
                        DoctorEmail = Convert.ToString(docdrow["DocEmailID"]);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("Patient.ReadDoctorDetailsByID>>" + Ex.Message);
            }
            return retValue;
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            DBPatient dbPatient = new DBPatient();
            drow = dbPatient.ReadDetailsByID(Id);

            try
            {
                if (drow != null)
                {
                    if (drow["PatientID"] != DBNull.Value)
                        Id = drow["PatientId"].ToString();
                    if (drow["PatientName"] != DBNull.Value)
                        Name = Convert.ToString(drow["PatientName"]);
                    if (drow["PatientAddress1"] != DBNull.Value)
                        Address1 = Convert.ToString(drow["PatientAddress1"]);
                    if (drow["PatientAddress2"] != DBNull.Value)
                        Address2 = Convert.ToString(drow["PatientAddress2"]);
                    if (drow["TelephoneNumber"] != DBNull.Value)
                        Telephone = Convert.ToString(drow["TelephoneNumber"]);
                    if (drow["Email"] != DBNull.Value)
                        Email = Convert.ToString(drow["Email"]);
                    if (drow["BirthDay"] != DBNull.Value)
                        Bday = Convert.ToInt32(drow["BirthDay"]);
                    if (drow["BirthMonth"] != DBNull.Value)
                        Bmonth = Convert.ToInt32(drow["BirthMonth"]);
                    if (drow["BirthYear"] != DBNull.Value)
                        Byear = Convert.ToInt32(drow["BirthYear"]);
                    if (drow["VisitDay1"] != DBNull.Value)
                        Visit1 = Convert.ToInt32(drow["VisitDay1"]);
                    if (drow["VisitDay2"] != DBNull.Value)
                        Visit2 = Convert.ToInt32(drow["VisitDay2"]);
                    if (drow["VisitDay3"] != DBNull.Value)
                        Visit3 = Convert.ToInt32(drow["VisitDay3"]);
                    if (drow["ShortNameAddress"] != DBNull.Value)
                        ShortNameAddress = Convert.ToString(drow["ShortNameAddress"]);
                    if (drow["DoctorID"] != DBNull.Value)
                        DoctorID = Convert.ToString(drow["DoctorID"]);
                    if (drow["Remark1"] != DBNull.Value)
                        Remark1 = Convert.ToString(drow["Remark1"]);
                    if (drow["Remark2"] != DBNull.Value)
                        Remark2 = Convert.ToString(drow["Remark2"]);
                    if (drow["Remark3"] != DBNull.Value)
                        Remark3 = Convert.ToString(drow["Remark3"]);
                    if (drow["DiscountOffered"] != DBNull.Value)
                        DiscountOffered = Convert.ToDouble(drow["DiscountOffered"].ToString());
                    if (drow["Gender"] != DBNull.Value && drow["Gender"].ToString() != string.Empty)
                        Gender = Convert.ToString(drow["Gender"]);
                    if (drow["MobileNumberForSMS"] != DBNull.Value)
                        MobileNumberForSMS = drow["MobileNumberForSMS"].ToString();
                    if (drow["PutInBlackList"] != DBNull.Value)
                        PutInBlackList = drow["PutInBlackList"].ToString();
                    else
                        PutInBlackList = "N";
                    if (drow["TokenNumber"] != DBNull.Value)
                    {
                        AccTokenNumber = Convert.ToInt32(drow["TokenNumber"].ToString());
                        CurrentTokenNumber = AccTokenNumber;
                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("Patient.ReadDetailsByID>>" + Ex.Message);
            }
            return retValue;
        }

        public DataTable ReadProductDetailsById(string Id)
        {

            DataTable dt = null;
            try
            {
                DBPatient _dbPatient = new DBPatient();
                dt = _dbPatient.ReadProductDetailsById(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteError("Patient.ReadProductDetailsById>>" + Ex.Message);
            }
            return dt;


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
            DBPatient dbPatient = new DBPatient();
            return dbPatient.AddDetails(Id, Name, Address1, Address2, Telephone, Email,
                Bday, Bmonth, Byear, Visit1, Visit2, Visit3, ShortNameAddress, DoctorID, Remark1, Remark2, Remark3, AccTokenNumber, Gender, DiscountOffered, MobileNumberForSMS, PutInBlackList, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool AddProductDetails()
        {
            DBPatient dbPatientProduct = new DBPatient();
            return dbPatientProduct.AddProductDetails(Id, DetailId, ProductID, Quantity);
        }

        public bool UpdateDetails()
        {
            DBPatient dbPatient = new DBPatient();
            return dbPatient.UpdateDetails(Id, Name, Address1, Address2, Telephone, Email,
                 Bday, Bmonth, Byear, Visit1, Visit2, Visit3, ShortNameAddress, DoctorID, Remark1, Remark2, Remark3, AccTokenNumber, Gender, DiscountOffered, MobileNumberForSMS, PutInBlackList, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBPatient dbPatient = new DBPatient();
            return dbPatient.DeleteDetails(Id);
        }

        public bool DeleteProductsById()
        {
            DBPatient dbpatient = new DBPatient();
            return dbpatient.DeleteProductsByID(Id);
        }

        public int GetCurrentTokenNumber()
        {
            DataRow dr = null;
            DBAccount dbdata = new DBAccount();
            try
            {
                dr = dbdata.GetTokenNumber();
                if (dr != null)
                {
                    if (dr["Tokennumber"] != DBNull.Value)
                    {
                        CurrentTokenNumber = Convert.ToInt32(dr["TokenNumber"].ToString());
                    }
                    CurrentTokenNumber += 1;
                    if (IFEdit != "Y")
                        AccTokenNumber = CurrentTokenNumber;

                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("Patient.GetAccTokenNumber>>" + Ex.Message);
            }
            return CurrentTokenNumber;
        }

        public bool UpdateTokenNumber()
        {
            bool retValue = false;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                dbno.UpdateTokenNumber(AccTokenNumber);
                retValue = true;
            }
            catch (Exception Ex)
            {
                Log.WriteError("Patient.GetAndUpdateTokenNumber>>" + Ex.Message);
            }
            return retValue;
        }
        public DataTable GetOverviewDataForCounterSaleForOnlyCashSaleCheck(string patientID) //Amar
        {
            DBPatient dbPatient = new DBPatient();

            return dbPatient.GetOverviewDataForCounterSaleForOnlyCashSaleCheck(patientID);
        }

        public bool UpdateMobileNumberInMasterPatient(string MobNo, string PatientID)
        {
            DBPatient pt = new DBPatient();
            return pt.UpdateMobileNumberInMasterPatient(MobNo, PatientID);
        }

        #endregion
    }
}
