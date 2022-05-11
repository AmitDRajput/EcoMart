using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public abstract class BaseObject : IValidation
    {
        #region Declaration
        private string _Id;
        private string _DetailID;
        private string _ChangedID;
        private string _Name;
        private string _Description;
        private string _IFEdit;
        private string _DebitAccount;
        private string _CreditAccount;
        private double _DebitAmount;
        private double _CreditAmount;
        private int _SerialNumber;

        private string _CreatedBy; //User ID
        private string _CreatedTime;
        private string _CreatedDate; //ISO Date
        private string _ModifiedBy; //User ID
        private string _ModifiedTime;
        private string _ModifiedDate; //ISO Date       
        private bool _isValid = false;
        private ArrayList _validationMessages;

        #endregion

        #region Constructors, Destructors
        public BaseObject()
        {
            Initialise();
        }
        #endregion

        #region Properties

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

        public string DetailId
        {
            get { return _DetailID; }
            set { _DetailID = value; }
        }
        public string ChangedID
        {
            get { return _ChangedID; }
            set { _ChangedID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
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
        public string DebitAccount
        {
            get { return _DebitAccount; }
            set { _DebitAccount = value; }
        }
        public string CreditAccount
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
        #endregion

        #region Virtual Methods
        public virtual void Initialise()
        {
            _Id = "";
            _DetailID = "";
            _ChangedID = "";
            _Name = "";
            _IFEdit = "";
            _Description = "";
            _CreatedBy = "";
            _CreatedDate = "";
            _ModifiedBy = "";
            _ModifiedDate = "";
            _CreatedTime = "";
            _ModifiedTime = "";
            _DebitAccount = "";
            _CreditAccount = "";
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
