using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public abstract class BaseObject : IValidation
    {
        #region Declaration
        
        private int _IntID;
        private int _IntStockID;
        private string _Id;
        private string _DetailID;
        private string _ChangedID;
        private Int32 _DetailIntID;
        private Int32 _ChangedIntID;
        private string _Name;
        private string _Adress1;
        private string _Adress2;
        private string _Telephone;
        private string _MailID;
        private int _CBAccountIDINT;

        private string _Description;
        private string _IFEdit;
        private int _DebitAccount;
        private int _CreditAccount;
        private double _DebitAmount;
        private double _CreditAmount;
        private int _SerialNumber;

        private string _CreatedBy; //User ID
        private string _CreatedTime;
        private string _netrate;
        private string _CreatedDate; //ISO Date
        private string _ModifiedBy; //User ID
        private string _ModifiedTime;
        private string _ModifiedDate; //ISO Date       
        private bool _isValid = false;
        private ArrayList _validationMessages;
        private string _MobileNumberForSMS;


        private string _AIODA;
        private string _GlobalNumber;
        private int _GalliNumber;

        #endregion

        #region Constructors, Destructors
        public BaseObject()
        {
            Initialise();
        }
        #endregion

        #region Properties
      
        public int IntStockID
        {
            get { return _IntStockID; }
            set { _IntStockID = value; }
        }

        public string AIODA
        {
            get { return _AIODA; }
            set { _AIODA = value; }
        }

        public string GlobalNumber
        {
            get { return _GlobalNumber; }
            set { _GlobalNumber = value; }
        }

        public int GalliNumber
        {
            get { return _GalliNumber; }
            set { _GalliNumber = value; }
        }

        public int SerialNumber
        {
            get { return _SerialNumber; }
            set { _SerialNumber = value; }
        }
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int IntID
        {
            get { return _IntID; }
            set { _IntID = value; }
        }       

        public int CBAccountIDINT
        {
            get { return _CBAccountIDINT; }
            set { _CBAccountIDINT = value; }
        }




        public string   DetailId
        {
            get { return _DetailID; }
            set { _DetailID = value; }
        }
        public string   ChangedID
        {
            get { return _ChangedID; }
            set { _ChangedID = value; }
        }
        public Int32 DetailIntID
        {
            get { return _DetailIntID; }
            set { _DetailIntID = value; }
        }
        public Int32  ChangedIntID
        {
            get { return _ChangedIntID; }
            set { _ChangedIntID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Address1
        {
            get { return _Adress1; }
            set { _Adress1 = value; }
        }


        public string Address2
        {
            get { return _Adress2; }
            set { _Adress2 = value; }
        }

        public string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }

        public string MailID
        {
            get { return _MailID; }
            set { _MailID = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public string ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }
        public string CreatedTime
        {
            get { return _CreatedTime; }
            set { _CreatedTime = value; }
        }
        public string netrate
        {
            get { return _netrate; }
            set { _netrate = value; }
        }
        public string ModifiedTime
        {
            get { return _ModifiedTime; }
            set { _ModifiedTime = value; }
        }
        public string IFEdit
        {
            get { return _IFEdit; }
            set { _IFEdit = value; }
        }
        public int DebitAccount
        {
            get { return _DebitAccount; }
            set { _DebitAccount = value; }
        }
        public int CreditAccount
        {
            get { return _CreditAccount; }
            set { _CreditAccount = value; }
        }
        public double DebitAmount
        {
            get { return _DebitAmount; }
            set { _DebitAmount = value; }
        }
        public double CreditAmount
        {
            get { return _CreditAmount; }
            set { _CreditAmount = value; }
        }
        public string MobileNumberForSMS
        {
            get { return _MobileNumberForSMS; }
            set { _MobileNumberForSMS = value; }
        }
        
        #endregion

        #region Virtual Methods
        public virtual void Initialise()
        {
            _IntID = 0;
            _Id = "";
            _DetailID = "";
            _ChangedID = "";
            _Name = "";
            _Adress1 = "";
            _Adress2 = "";
            _Telephone = "";
            _MailID = "";
            _IFEdit = "";
            _Description = "";
            _CreatedBy = "";
            _CreatedDate = "";
            _ModifiedBy = "";
            _ModifiedDate = "";
            _CreatedTime = "";
            _ModifiedTime = "";
            _DebitAccount = 0;
            _CreditAccount = 0;
            _DebitAmount = 0;
            _CreditAmount = 0;
            _SerialNumber = 0;
            
        }

        public virtual bool CanBeDeleted()
        {
            return true;
        }
        #endregion

        #region IValidation Members

        public bool IsValid
        {
            get { return _isValid; }
        }

        public ArrayList ValidationMessages
        {
            get
            {
                if (_validationMessages == null)
                    _validationMessages = new ArrayList();
                return _validationMessages;
            }
        }

        public void Validate()
        {
            _isValid = false;
            ValidationMessages.Clear();
            DoValidate();
            _isValid = (ValidationMessages.Count == 0);
        }
        /// <summary>
        /// Method to perform Validations in Child Classes
        /// </summary>
        public virtual void DoValidate()
        {

        }

        public void CompanyInfo()
        {

        }




        #endregion
    }
}
