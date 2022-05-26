using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class Account : BaseObject
    {
        #region Declaration
        private string _AccCode;
        private string _AccName;
        private string _AccGroupID;
        private string _AccGroupName;
        private double _AccOpeningDebit;
        private double _AccOpeningCredit;
        private string _AccAddress1;
        private string _AccAddress2;
        private string _AccEmailID;
        private string _AccTransactionType;
        private string _AccBankId;
        private string _AccBranchID;
        private string _AccDoctorID;
        private string _AccAreaID;
        private string _AccTelephone;
        private string _AccMobileNumberForSMS;
        private string _AccContactPerson;
        private string _AccRemark1;
        private string _AccRemark2;
        private int _AccBirthDay;
        private int _AccBirthMonth;
        private int _AccBirthYear;
        private string _AccCrVisitDays;
        private string _AccDLN;
        private string _AccShortName;
        private int _AccDbVisitDay1;
        private int _AccDbVisitDay2;
        private int _AccDbVisitDay3;
        private double _AccDiscountOffered;
        private double _AccLessPercentInDebitNote;
        private string _AccVATTin;
        private string _AccBankAccountNumber;
        private string _AccPAN;
        private string _AccNameAddress;
        private int _AccTokenNumber;
        private string _AccStatement15Days;
        private int _CurrentTokenNumber;
        private string _SetAsDefault;
        private string _IfLBt;
        private string _AccLBT;
        private string _AccAIOCDACode;
        private string _AccSCORGCode;
        private string _IfOMS;
   
        #endregion

        #region Constructors, Destructors
        public Account()
        {
           Initialise();
        }
        #endregion

        #region Properties
        public string IFOMS
        {
            get { return _IfOMS; }
            set { _IfOMS = value; }
        }
        public string AccAIOCDACode
        {
            get { return _AccAIOCDACode; }
            set { _AccAIOCDACode = value; }
        }
        public string AccSCORGCode
        {
            get { return _AccSCORGCode; }
            set { _AccSCORGCode = value; }
        }
        public string AccCode
        {
            get { return _AccCode; }
            set { _AccCode = value; }
        }
        public string AccName
        {
            get { return _AccName; }
            set { _AccName = value; }
        }
        public string AccGroupID
        {
            get { return _AccGroupID; }
            set { _AccGroupID = value; }
        }
        public string AccGroupName
        {
            get { return _AccGroupName; }
            set { _AccGroupName = value; }
        }
        public double AccOpeningDebit
        {
            get { return _AccOpeningDebit; }
            set { _AccOpeningDebit = value; }
        }
        public double AccOpeningCredit
        {
            get { return _AccOpeningCredit; }
            set { _AccOpeningCredit = value; }
        }
        public string AccAddress1
        {
            get { return _AccAddress1; }
            set { _AccAddress1 = value; }
        }

        public string AccAddress2
        {
            get { return _AccAddress2; }
            set { _AccAddress2 = value; }
        }

        public string AccEmailID
        {
            get { return _AccEmailID; }
            set { _AccEmailID = value; }
        }
        public string AccTransactionType
        {
            get { return _AccTransactionType; }
            set { _AccTransactionType = value; }
        }
        public string AccBankId
        {
            get { return _AccBankId; }
            set { _AccBankId = value; }
        }
        public string AccBranchID
        {
            get { return _AccBranchID; }
            set { _AccBranchID = value; }
        }
        public string AccDoctorID
        {
            get { return _AccDoctorID; }
            set { _AccDoctorID = value; }
        }
        public string AccAreaID
        {
            get { return _AccAreaID; }
            set { _AccAreaID = value; }
        }
        public string AccTelephone
        {
            get { return _AccTelephone; }
            set { _AccTelephone = value; }
        }
        public string AccMobileNumberForSMS
        {
            get { return _AccMobileNumberForSMS; }
            set { _AccMobileNumberForSMS = value; }
        }
        public string AccContactPerson
        {
            get { return _AccContactPerson; }
            set { _AccContactPerson = value; }
        }
        public string AccRemark1
        {
            get { return _AccRemark1; }
            set { _AccRemark1 = value; }
        }
        public string AccRemark2
        {
            get { return _AccRemark2; }
            set { _AccRemark2 = value; }
        }
        public int AccBirthDay
        {
            get { return _AccBirthDay; }
            set { _AccBirthDay = value; }
        }
        public int AccBirthMonth
        {
            get { return _AccBirthMonth; }
            set { _AccBirthMonth = value; }
        }
        public int AccBirthYear
        {
            get { return _AccBirthYear; }
            set { _AccBirthYear = value; }
        }
        public string AccCrVisitDays
        {
            get { return _AccCrVisitDays; }
            set { _AccCrVisitDays = value; }
        }

        public string AccDLN
        {
            get { return _AccDLN; }
            set { _AccDLN = value; }
        }

        public string AccShortName
        {
            get { return _AccShortName; }
            set { _AccShortName = value; }
        }
        public int AccDbVisitDay1
        {
            get { return _AccDbVisitDay1; }
            set { _AccDbVisitDay1 = value; }
        }
        public int AccDbVisitDay2
        {
            get { return _AccDbVisitDay2; }
            set { _AccDbVisitDay2 = value; }
        }
        public int AccDbVisitDay3
        {
            get { return _AccDbVisitDay3; }
            set { _AccDbVisitDay3 = value; }
        }
        public double AccDiscountOffered
        {
            get { return _AccDiscountOffered; }
            set { _AccDiscountOffered = value; }
        }
        public double AccLessPercentInDebitNote
        {
            get { return _AccLessPercentInDebitNote; }
            set { _AccLessPercentInDebitNote = value; }
        }
        public string AccVATTin
        {
            get { return _AccVATTin; }
            set { _AccVATTin = value; }
        }
        public string AccBankAccountNumber
        {
            get { return _AccBankAccountNumber; }
            set { _AccBankAccountNumber = value; }
        }

        public string AccPAN
        {
            get { return _AccPAN; }
            set { _AccPAN = value; }
        }

        public string AccNameAddress
        {
            get { return _AccNameAddress; }
            set { _AccNameAddress = value; }
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

        public string AccStatement15Days
        {
            get { return _AccStatement15Days; }
            set { _AccStatement15Days = value; }
        }
        public string SetAsDefault
        {
            get { return _SetAsDefault; }
            set { _SetAsDefault = value; }
        }

        public string IfLBt
        {
            get { return _IfLBt; }
            set { _IfLBt = value; }
        }
        public string AccLBT
        {
            get { return _AccLBT; }
            set { _AccLBT = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            _AccCode = "";
            _AccName = "";
            _AccGroupID = "";
            _AccGroupName = "";
            _AccOpeningCredit = 0.00;
            _AccOpeningDebit = 0.00;
            _AccAddress1 = "";
            _AccAddress2 = "";
            _AccEmailID = "";
            _AccTransactionType = "";
            _AccBankId = "";
            _AccBranchID = "";
            _AccDoctorID = "";
            _AccAreaID = "";
            _AccTelephone = "";
            _AccMobileNumberForSMS = "";
            _AccContactPerson = "";
            _AccRemark1 = "";
            _AccRemark2 = "";
            _AccBirthDay = 0;
            _AccBirthMonth = 0;
            _AccBirthYear = 0;
            _AccCrVisitDays = "";
            _AccShortName = "";
            _AccDbVisitDay1 = 0;
            _AccDbVisitDay2 = 0;
            _AccDbVisitDay3 = 0;
            _AccDiscountOffered = 0.00;
            _AccLessPercentInDebitNote = 0.00;
            _AccVATTin = "";
            _AccBankAccountNumber = "";
            _AccPAN = "";
            _AccTokenNumber = 0;
            _CurrentTokenNumber = 0;
            _AccNameAddress = "";
            _AccStatement15Days = "Y";
            _SetAsDefault = "N";
            _IfLBt = "N";
            _AccLBT = "";
            _AccSCORGCode = "";
            _AccAIOCDACode = "";
            _IfOMS = "N";
        }

        public override void DoValidate()
        {

            try
            {
                if (AccName == null || AccName == "")
                    ValidationMessages.Add("Please enter the Account Name.");
                if (AccCode == null || AccCode == "")
                    ValidationMessages.Add("Select Group");
                if (AccGroupID == null || AccGroupID == "")
                    ValidationMessages.Add("Please enter the Group Name.");
                if (AccCode.Trim() == FixAccounts.AccCodeForBank)
                {
                    if (AccBankAccountNumber == null)
                        ValidationMessages.Add("Please enter the Bank Account Number.");
                }

                DBAccount dbAccount = new DBAccount();

                if (Name != "")
                {

                    if (IFEdit == "Y")
                    {
                        if (dbAccount.IsNameUniqueForEdit(AccName, AccAddress1, Id))
                        {
                            ValidationMessages.Add("Account Already Exists.");
                        }
                    }
                    else
                    {
                        if (dbAccount.IsNameUniqueForAdd(AccName, AccAddress1, Id))
                        {
                            ValidationMessages.Add("Account Already Exists.");
                        }
                    }
                }
                if (AccCode == FixAccounts.AccCodeForDebtor)
                {
                   
                    if (IFEdit == "Y")
                    {
                        if (dbAccount.IsTokenNumberUniqueForEdit(AccTokenNumber, Id))
                        {
                            if (AccTokenNumber > 0)
                                ValidationMessages.Add("Token Number Already Exists");
                        }
                        //DBPatient dbPatient = new DBPatient();
                        //if (dbPatient.IsTokenNumberUniqueForEdit(AccTokenNumber, Id))
                        //{
                        //    if (AccTokenNumber > 0)
                        //        ValidationMessages.Add("Token Number Already Exists");
                        //}
                    }
                    else
                    {
                        if (dbAccount.IsTokenNumberUniqueForAdd(AccTokenNumber, Id))
                        {
                            if (AccTokenNumber > 0)
                                ValidationMessages.Add("Token Number Already Exists");
                        }
                        //DBPatient dbPatient = new DBPatient();
                        //if (dbPatient.IsTokenNumberUniqueForAdd(AccTokenNumber, Id))
                        //{
                        //    if (AccTokenNumber > 0)
                        //        ValidationMessages.Add("Token Number Already Exists");
                        //}
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
            bool bRetValue = false;
            int _rowcount = 0;
            try
            {
                DBDelete dbdelete = new DBDelete();
                _rowcount = dbdelete.GetOverviewDataSelect("masterproduct", "ProdPartyId_1", Id);
                if (_rowcount == 0)
                {
                    _rowcount = dbdelete.GetOverviewDataSelect("masterproduct", "ProdPartyId_2", Id);
                    if (_rowcount == 0)
                    {
                        _rowcount = dbdelete.GetOverviewDataSelect("linkpartycompany", "AccountID", Id);
                        if (_rowcount == 0)
                        {
                            _rowcount = dbdelete.GetOverviewDataSelect("tbltrnac", "AccountID", Id);
                            if (_rowcount == 0)
                            {
                                _rowcount = dbdelete.GetOverviewDataSelect("tbltrnac", "AccAccountID", Id);
                                if (_rowcount == 0)
                                {
                                    _rowcount = dbdelete.GetOverviewDataSelect("vouchercreditdebitnote", "AccountID", Id);
                                    if (_rowcount == 0)
                                        bRetValue = true;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return bRetValue;
        }
        #endregion

        #region Public Methods
        # region read

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBAccount dbAccount = new DBAccount();
                drow = dbAccount.ReadDetailsByID(Id);
                if (drow != null)
                {
                    if (drow["AccountId"] != DBNull.Value)
                        Id = drow["AccountId"].ToString();
                    if (drow["AccCode"] != DBNull.Value)
                        AccCode = Convert.ToString(drow["AccCode"]);
                    if (drow["AccName"] != DBNull.Value)
                        AccName = Convert.ToString(drow["AccName"]);
                    if (drow["AccGroupID"] != DBNull.Value)
                        AccGroupID = Convert.ToString(drow["AccGroupID"]);
                    if (drow["AccOpeningCredit"] != DBNull.Value)
                        AccOpeningCredit = Convert.ToDouble(drow["AccOpeningCredit"]);
                    if (drow["AccOpeningDebit"] != DBNull.Value)
                        AccOpeningDebit = Convert.ToDouble(drow["AccOpeningDebit"]);
                    if (drow["AccAddress1"] != DBNull.Value)
                        AccAddress1 = Convert.ToString(drow["AccAddress1"]);
                    if (drow["AccAddress2"] != DBNull.Value)
                        AccAddress2 = Convert.ToString(drow["AccAddress2"]);
                    if (drow["AccEmailID"] != DBNull.Value)
                        AccEmailID = Convert.ToString(drow["AccEmailID"]);
                    if (drow["AccBankId"] != DBNull.Value)
                        AccBankId = Convert.ToString(drow["AccBankId"]);
                    if (drow["AccBranchID"] != DBNull.Value)
                        AccBranchID = Convert.ToString(drow["AccBranchID"]);
                    if (drow["AccDoctorID"] != DBNull.Value)
                        AccDoctorID = Convert.ToString(drow["AccDoctorID"]);
                    if (drow["AccAreaID"] != DBNull.Value)
                        AccAreaID = Convert.ToString(drow["AccAreaID"]);
                    if (drow["AccTelephone"] != DBNull.Value)
                        AccTelephone = Convert.ToString(drow["AccTelephone"]);
                    if (drow["MobileNumberForSMS"] != DBNull.Value)
                        AccMobileNumberForSMS = Convert.ToString(drow["MobileNumberForSMS"]);
                    if (drow["AccTransactionType"] != DBNull.Value)
                        AccTransactionType = Convert.ToString(drow["AccTransactionType"]);
                    if (drow["AccContactPerson"] != DBNull.Value)
                        AccContactPerson = Convert.ToString(drow["AccContactPerson"]);
                    if (drow["AccRemark1"] != DBNull.Value)
                        AccRemark1 = Convert.ToString(drow["AccRemark1"]);
                    if (drow["AccRemark2"] != DBNull.Value)
                        AccRemark2 = Convert.ToString(drow["AccRemark2"]);
                    if (drow["AccBirthDay"] != DBNull.Value)
                        AccBirthDay = Convert.ToInt32(drow["AccBirthDay"]);
                    if (drow["AccBirthMonth"] != DBNull.Value)
                        AccBirthMonth = Convert.ToInt32(drow["AccBirthMonth"]);
                    if (drow["AccBirthYear"] != DBNull.Value)
                        AccBirthYear = Convert.ToInt32(drow["AccBirthYear"]);
                    //if (drow["AccCrVisitDays"] != DBNull.Value)
                    //    AccCrVisitDays = Convert.ToString(drow["AccCrVisitDays"]);
                    //if (drow["AccShortName"] != DBNull.Value)
                    //    AccShortName = Convert.ToString(drow["AccShortName"]);
                    //if (drow["AccDbVisitDay1"] != DBNull.Value)
                    //    AccDbVisitDay1 = Convert.ToInt32(drow["AccDbVisitDay1"]);
                    //if (drow["AccDbVisitDay2"] != DBNull.Value)
                    //    AccDbVisitDay2 = Convert.ToInt32(drow["AccDbVisitDay2"]);
                    //if (drow["AccDbVisitDay3"] != DBNull.Value)
                    //    AccDbVisitDay3 = Convert.ToInt32(drow["AccDbVisitDay3"]);
                    if (drow["AccDiscountOffered"] != DBNull.Value)
                        AccDiscountOffered = Convert.ToDouble(drow["AccDiscountOffered"]);
                    //if (drow["AccLessPercentInDebitNote"] != DBNull.Value)
                    //    AccLessPercentInDebitNote = Convert.ToDouble(drow["AccLessPercentInDebitNote"]);
                    if (drow["AccVATTin"] != DBNull.Value)
                        AccVATTin = Convert.ToString(drow["AccVATTin"]);
                    if (drow["AccBankAccountNumber"] != DBNull.Value)
                        AccBankAccountNumber = Convert.ToString(drow["AccBankAccountNumber"]);
                    if (drow["AccPAN"] != DBNull.Value)
                        AccPAN = Convert.ToString(drow["AccPAN"]);
                    if (drow["AccDLN"] != DBNull.Value)
                        AccDLN = Convert.ToString(drow["AccDLN"]);
                    if (drow["AccTokenNumber"] != DBNull.Value)
                        AccTokenNumber = Convert.ToInt32(drow["AccTokenNumber"]);
                    if (drow["AccIFOctroi"] != DBNull.Value)
                    {
                        if (AccCode == FixAccounts.AccCodeForBank)
                            SetAsDefault = Convert.ToString(drow["AccIFOctroi"]);
                    }
                    if (drow["AccLBT"] != DBNull.Value)
                        AccLBT = drow["AccLBT"].ToString();
                    if (drow["IFLBT"] != DBNull.Value)
                        IfLBt = drow["IFLBT"].ToString();
                    //if (drow["ACCIFOMS"] != DBNull.Value)
                    //    IFOMS = Convert.ToString(drow["ACCIFOMS"]);
                    AccStatement15Days = "Y";

                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        //public DataRow ReadDetailsByIDSS(string accId)
        //{

        //    DataRow drow = null;
        //    try
        //    {
        //        DBAccount dbAccount = new DBAccount();
        //        drow = dbAccount.ReadDetailsByID(accId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //    return drow;
        //}
        # endregion

        #region For Debtors
        public bool AddDebtorDetails()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.AddDetailsDr(IntID, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2, AccEmailID, AccTransactionType, AccBankId, AccBranchID, AccDoctorID,AccAreaID, 
                AccTelephone, AccMobileNumberForSMS, AccContactPerson, AccRemark1,AccRemark2, AccBirthDay, AccBirthMonth, AccBirthYear,
                AccDbVisitDay1, AccDbVisitDay2, AccDbVisitDay3, AccVATTin, AccDLN, AccTokenNumber,AccDiscountOffered,AccLBT,IfLBt,AccAIOCDACode,AccSCORGCode, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDebtorDetails()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.UpdateDetailsDr(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2, AccEmailID, AccTransactionType, AccBankId, AccBranchID, AccDoctorID,AccAreaID,
                AccTelephone, AccMobileNumberForSMS, AccContactPerson, AccRemark1,AccRemark2, AccBirthDay, AccBirthMonth, AccBirthYear,
                AccDbVisitDay1, AccDbVisitDay2, AccDbVisitDay3, AccVATTin, AccDLN, AccTokenNumber,AccDiscountOffered,AccLBT,IfLBt,AccAIOCDACode,AccSCORGCode, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        #endregion

        #region For Creditors
        public bool AddCreditorDetails()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.AddDetailsCreditor(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2, AccEmailID, AccTelephone, AccMobileNumberForSMS, AccContactPerson,
                AccRemark1, AccRemark2, AccCrVisitDays, AccShortName, AccDiscountOffered, AccVATTin, AccDLN, AccStatement15Days, AccLessPercentInDebitNote, AccLBT, IfLBt, AccAIOCDACode, AccSCORGCode, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateCreditorDetails()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.UpdateDetailsCreditor(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1,AccAddress2, AccEmailID, AccTelephone, AccMobileNumberForSMS, AccContactPerson,
                AccRemark1, AccRemark2, AccCrVisitDays, AccShortName, AccDiscountOffered, AccVATTin, AccDLN, AccStatement15Days, AccLessPercentInDebitNote, AccLBT, IfLBt, AccAIOCDACode, AccSCORGCode, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        #endregion

        #region For Bank
        public bool AddBankDetails()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.AddDetailsBank(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1,AccAddress2, AccEmailID, AccTelephone, AccMobileNumberForSMS, AccContactPerson, AccRemark1,AccRemark2,
                AccBankAccountNumber,SetAsDefault, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateBankDetails()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.UpdateDetailsBank(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2, AccEmailID, AccTelephone,AccMobileNumberForSMS, AccContactPerson, AccRemark1,AccRemark2,
                AccBankAccountNumber,SetAsDefault , ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public void ClearAllSetDefault()
        {
            DBAccount dbAccount = new DBAccount();
            dbAccount.ClearAllSetDefault();
        }
        public void SetThisAsDefault(string accountID)
        {
            DBAccount dbAccount = new DBAccount();
            dbAccount.SetThisAsDefault(accountID);
        }
        #endregion

        # region For General
        public bool AddDetailsGnrl()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.AddDetailsGnrl(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2, AccEmailID, AccTelephone,AccMobileNumberForSMS, AccContactPerson,
                AccRemark1,AccRemark2, AccVATTin, AccPAN, CreatedDate, CreatedTime);
        }

        public bool UpdateDetailsGnrl()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.UpdateDetailsGnrl(Id, AccCode, AccName, AccGroupID, AccOpeningDebit,
                AccOpeningCredit, AccAddress1, AccAddress2, AccEmailID, AccTelephone, AccMobileNumberForSMS, AccContactPerson,
                AccRemark1,AccRemark2, AccVATTin, AccPAN, ModifiedDate, ModifiedTime);
        }
        #endregion

        # region Delete

        public bool DeleteDetails()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.DeleteDetails(Id);
        }
        # endregion

        #region GET
        public DataTable GetOverviewData()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.GetOverviewData();
        }
        public DataTable GetOverviewAddress1()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.GetOverviewAddress1();
        }
        public DataTable GetOverviewAddress2()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.GetOverviewAddress2();
        }
        public DataTable GetOverviewDataForContraEntry()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.GetOverviewDataForContraEntry();
        }
        public DataTable GetOverviewDataForContraEntry(string accountID)
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.GetOverviewDataForContraEntry(accountID);
        }
        public DataTable GetOverviewDataForList()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.GetOverviewDataForList();
        }
     
        public DataTable GetOverviewData(string AccCode)
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.GetOverviewData(AccCode);
        }

        public DataRow GetSSNameForGivenAccount(string AccountID)
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.GetSSNameForGivenAccount(AccountID);
        }
        public DataTable GetSSAccountHoldersList(string AccCode)
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetSSAccountHoldersList(AccCode);
        }
       
        public DataTable GetSSAccountHoldersListForMultiSelection(string AccCode)
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetSSAccountHoldersListForMultiSelection(AccCode);
        }
        public DataTable GetSSAccountHoldersListForGeneralLedger()
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetSSAccountHoldersListForGeneralLedger();
        }
        //public DataTable GetSSAccountHoldersListForMultiSelection()
        //{
        //    DBAccount dbData = new DBAccount();
        //    return dbData.GetSSAccountHoldersListForMultiSelection();
        //}
        public DataTable GetSSAccountHoldersListforDebitNoteExpiry(string AccCode)
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetSSAccountHoldersListforDebitNoteExpiry(AccCode);
        }
        public DataTable GetAccountsOtherThanDebtorCreditor()
        {
            DBAccount dbAccount = new DBAccount();
            return dbAccount.GetAccountsOtherThanDebtorCreditor();
        }
       
        public DataTable GetDebtorCreditorList()
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetDebtorCreditorList();
        }
        //public DataTable GetDebtorCreditorPatientList()
        //{
        //    DBAccount dbData = new DBAccount();
        //    return dbData.GetDebtorCreditorPatientList();
        //}
        public DataTable GetDebtorCreditorListForCashBankReceipt()
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetDebtorCreditorListForCashBankReceipt();
        }
        //public DataTable GetDebtorCreditorListForBankReceipt()
        //{
        //    DBAccount dbData = new DBAccount();
        //    return dbData.GetDebtorCreditorListForBankReceipt();
        //}
        public DataTable GetDebtorCreditorPatientList()
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetDebtorCreditorPatientList();
        }
        public DataTable GetDebtorPatientList()
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetDebtorPatientList();
        }
        public DataTable GetStockInOutList()
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetStockInOutList(FixAccounts.AccountStockInOut.ToString());
        }
        public DataTable GetCreditorListForPayment()
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetCreditorListForPayment();
        }

        public DataTable GetAccountHoldersListForAlliedImport(string AccCode)
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetAccountHoldersListForAlliedImport(AccCode);
        }

        public DataTable GetBankAccountList()
        {
            DBAccount dbData = new DBAccount();
            return dbData.GetBankAccountList();
        }
        public string GetDefaultBank()
        {
            DataRow dr = null;
            DBAccount dbdata = new DBAccount();
            string accountID = string.Empty;
            try
            {
                dr = dbdata.GetDefaultBank();
                if (dr != null)
                {
                   accountID = dr["AccountID"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return accountID;

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

        internal bool UpdateMobilenoInAccountDetail()
        {
            bool retValue = false;
            try
            {
                DBAccount dbAcc= new DBAccount();
                dbAcc.UpdateMobilenoInAccountDetail(Id,AccMobileNumberForSMS);
                retValue = true;
            }
            catch (Exception Ex)
            {
                Log.WriteError("Patient.GetAndUpdateTokenNumber>>" + Ex.Message);
            }
            return retValue;
        }

        public Int32 GetIntID()
        {
            int maxid;
            maxid = 1;
            DBAccount dbaccount = new DBAccount();
            DataRow idrow = dbaccount.GetMaxID();
            if (idrow != null)
            {
                if (idrow["maxid"] != null && idrow["maxid"].ToString() != string.Empty)
                {
                    maxid = Convert.ToInt32(idrow["maxid"]) + 1;
                }
            }
            return maxid;

        }
        #endregion


        #endregion


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

        public DataTable GetVoucherTypes()
        {
            DataTable dt = new DataTable();
            try
            {
                DBAccount dbac = new DBAccount();
                dt = dbac.GetVoucherTypes();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }

        //public DataTable GetVoucherTypes(string voutype1, string voutype2, string voutype3,string voutype4)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        DBAccount dbac = new DBAccount();
        //        dt = dbac.GetVoucherTypes();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //    return dt;
        //}

      
    }
}
