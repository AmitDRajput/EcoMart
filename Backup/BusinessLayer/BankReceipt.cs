using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    class BankReceipt : BaseObject
    {
        #region Declaration  
     
        private double _CellOldValueAmount;
        private double _CellOldValueDiscount;
        private string _ModifyEdit;
        private string _ActualAccountID;

        private string _CBAccountId;
        private string _CBName;
        private string _CBAddress1;
        private string _CBAddress2;
        private string _CBNarration;
        private string _CBVouType;
        private int _CBVouNo;
        private string _CBVouDate;
        private int _CBNoOfRows;
        private double _CBAmount;
        private string _CBBankAccountId;
        private string _CBBankId;
        private string _CBBranchId;
        private string _CBBankName;
        private string _CBBranchName;
        private string _CBChequeNumber;
        private string _CBChequeDate;   
        private double _CBOnAccountAmount;
        private double _OpeningBalance;
        private double _OpeningCleared;
        private double _OpeningClearedInVoucher;


        private string _DMasterId;
        private string _DSaleId;
        private string _DVoucherSeries;
        private string _DVoucherType;
        private int    _DvoucherNumber;
        private string _DVoucherDate;
        private string _DSubType;
        private double _DBillAmount;
        private double _DBalanceAmount;
        private double _DClearedAmount;
        private double _DDiscountAmount;

        private string _preAccountId;
        private string _preName;        
        private string _preNarration;
        private string _preVouType;
        private int _preVouNo;
        private string _preVouDate;
        private int _preNoOfRows;
        private double _preAmount;
        private string _preBankAccountId;
        private string _preBankId;
        private string _preBranchId;
        private string _preBankName;
        private string _preBranchName;
        private string _preChequeNumber;
        private string _preChequeDate;
        private double _preOnAccountAmount;

        private int _ChequeReturnVouNo;
        private string _ChequeReturnVouDate;
        private string _ChequeReturnVouType;
        private double _ChequeReturnCharges;
        private string _ChequeReturnID;
        private string _IfChequeReturn;

        # endregion

        #region Constructors
        public BankReceipt()
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

        # region properties
        public string ActualAccountID
        {
            get { return _ActualAccountID; }
            set { _ActualAccountID = value; }
        }

        public string ModifyEdit
        {
            get { return _ModifyEdit; }
            set { _ModifyEdit = value; }
        }
        public double CellOldValueAmount
        {
            get { return _CellOldValueAmount; }
            set { _CellOldValueAmount = value; }
        }

        public double CellOldValueDiscount
        {
            get { return _CellOldValueDiscount; }
            set { _CellOldValueDiscount = value; }
        }
        public string CBChequeDate
        {
            get { return _CBChequeDate; }
            set { _CBChequeDate = value; }
        }

        public string CBChequeNumber
        {
            get { return _CBChequeNumber; }
            set { _CBChequeNumber = value; }
        }
        public string CBBankAccountID
        {
            get { return _CBBankAccountId; }
            set { _CBBankAccountId = value; }
        }
        public string CBBankID
        {
            get { return _CBBankId; }
            set { _CBBankId = value; }
        }
        public string CBBranchID
        {
            get { return _CBBranchId; }
            set { _CBBranchId = value; }
        }
        public string CBBankName
        {
            get { return _CBBankName; }
            set { _CBBankName = value; }
        }
        public string CBBranchName
        {
            get { return _CBBranchName; }
            set { _CBBranchName = value; }
        }
        public string CBAccountID
        {
            get { return _CBAccountId; }
            set { _CBAccountId = value; }
        }
        public string CBVouDate
        {
            get { return _CBVouDate; }
            set { _CBVouDate = value; }
        }
        public string DSaleId      
        {
            get { return _DSaleId; }
            set { _DSaleId = value; }
        }
        public string CBName
        {
            get { return _CBName; }
            set { _CBName = value; }
        }
        public string CBAddress1
        {
            get { return _CBAddress1; }
            set { _CBAddress1 = value; }
        }
        public string CBAddress2
        {
            get { return _CBAddress2; }
            set { _CBAddress2 = value; }
        }
        public string CBNarration
        {
            get { return _CBNarration; }
            set { _CBNarration = value; }
        }
        public string CBVouType
        {
            get { return _CBVouType; }
            set { _CBVouType = value; }
        }
        public int CBVouNo
        {
            get { return _CBVouNo; }
            set { _CBVouNo = value; }
        }
        public int CBNoOFRows
        {
            get { return _CBNoOfRows; }
            set { _CBNoOfRows = value; }
        }
        public double CBAmount
        {
            get { return _CBAmount; }
            set { _CBAmount = value; }
        }       
        public double CBOnAccountAmount
        {
            get { return _CBOnAccountAmount; }
            set { _CBOnAccountAmount = value; }
        }
        
        public string DMasterID
        {
            get { return _DMasterId; }
            set { _DMasterId = value; }
        }
        public string DVoucherSeries
        {
            get { return _DVoucherSeries; }
            set { _DVoucherSeries = value; }
        }
        public string DVoucherType
        {
            get { return _DVoucherType; }
            set { _DVoucherType = value; }
        }
        public int DVoucherNumber
        {
            get { return _DvoucherNumber; }
            set { _DvoucherNumber = value; }
        }
        public string DVoucherDate
        {
            get { return _DVoucherDate; }
            set { _DVoucherDate = value; }
        }
        public string DSubType
        {
            get { return _DSubType; }
            set { _DSubType = value; }
        }
        public double DBillAmount
        {
            get { return _DBillAmount; }
            set { _DBillAmount = value; }
        }
        public double DBalanceAmount
        {
            get { return _DBalanceAmount; }
            set { _DBalanceAmount = value; }
        }
        public double DClearedAmount
        {
            get { return _DClearedAmount; }
            set { _DClearedAmount = value; }
        }
        public double DDiscountAmount
        {
            get {return _DDiscountAmount;}
            set { _DDiscountAmount = value; }
        }
        public double OpeningBalance
        {
            get { return _OpeningBalance; }
            set { _OpeningBalance = value; }
        }

        public double OpeningCleared
        {
            get { return _OpeningCleared; }
            set { _OpeningCleared = value; }
        }
        public double OpeningClearedInVoucher
        {
            get { return _OpeningClearedInVoucher; }
            set { _OpeningClearedInVoucher = value; }
        }
        public string preChequeDate
        {
            get { return _preChequeDate; }
            set { _preChequeDate = value; }
        }

        public string preChequeNumber
        {
            get { return _preChequeNumber; }
            set { _preChequeNumber = value; }
        }
        public string preBankAccountID
        {
            get { return _preBankAccountId; }
            set { _preBankAccountId = value; }
        }
        public string preBankID
        {
            get { return _preBankId; }
            set { _preBankId = value; }
        }
        public string preBranchID
        {
            get { return _preBranchId; }
            set { _preBranchId = value; }
        }
        public string preBankName
        {
            get { return _preBankName; }
            set { _preBankName = value; }
        }
        public string preBranchName
        {
            get { return _preBranchName; }
            set { _preBranchName = value; }
        }
        public string preAccountID
        {
            get { return _preAccountId; }
            set { _preAccountId = value; }
        }
        public string preVouDate
        {
            get { return _preVouDate; }
            set { _preVouDate = value; }
        }
       
        public string preName
        {
            get { return _preName; }
            set { _preName = value; }
        }
      
        public string preNarration
        {
            get { return _preNarration; }
            set { _preNarration = value; }
        }
        public string preVouType
        {
            get { return _preVouType; }
            set { _preVouType = value; }
        }
        public int preVouNo
        {
            get { return _preVouNo; }
            set { _preVouNo = value; }
        }
        public int preNoOFRows
        {
            get { return _preNoOfRows; }
            set { _preNoOfRows = value; }
        }
        public double preAmount
        {
            get { return _preAmount; }
            set { _preAmount = value; }
        }
        public double preOnAccountAmount
        {
            get { return _preOnAccountAmount; }
            set { _preOnAccountAmount = value; }
        }




        public string ChequeReturnVouDate
        {
            get { return _ChequeReturnVouDate; }
            set { _ChequeReturnVouDate = value; }
        }
        public string ChequeReturnVouType
        {
            get { return _ChequeReturnVouType; }
            set { _ChequeReturnVouType = value; }
        }
        public int ChequeReturnVouNo
        {
            get { return _ChequeReturnVouNo; }
            set { _ChequeReturnVouNo = value; }
        }

        public double ChequeReturnCharges
        {
            get { return _ChequeReturnCharges; }
            set { _ChequeReturnCharges = value; }
        }
        public string ChequeReturnID
        {
            get { return _ChequeReturnID; }
            set { _ChequeReturnID = value; }
        }
        public string IfChequeReturn
        {
            get { return _IfChequeReturn; }
            set { _IfChequeReturn = value; }
        }
        #endregion

        #region Internal Methods

        public override void Initialise()
        {
            base.Initialise();

            _CellOldValueAmount = 0;
            _CellOldValueDiscount = 0;
            _ModifyEdit = "N";
            _ActualAccountID = null;         
            _CBName = "";
            _CBAccountId = "";
            _CBBankAccountId = "";
            _CBBankId = "";
            _CBBranchId = "";
            _CBAddress1 = "";
            _CBAddress2 = "";
            _CBNarration = "";
            _CBVouType = FixAccounts.VoucherTypeForBankReceipt;
            _CBVouNo = 0;
            _CBNoOfRows = 0;
            _CBAmount = 0;
            _CBVouDate = "";
            _CBChequeNumber = "";
            _CBOnAccountAmount = 0;
            _CBBankName = "";
            _CBBranchName = "";
            _OpeningBalance = 0;
            _OpeningCleared = 0;
            _OpeningClearedInVoucher = 0;

          
            _DSaleId = "";
            _DBalanceAmount = 0;
            _DBillAmount = 0;
            _DClearedAmount = 0;
            _DDiscountAmount = 0;
            _DMasterId = "";
            _DSubType = "";
            _DVoucherDate = "";
            _DvoucherNumber = 0;
            _DVoucherSeries = "";
            _DVoucherType = "";

            _ChequeReturnCharges = 0;
            _ChequeReturnVouDate = "";
            _ChequeReturnVouNo = 0;
            _ChequeReturnVouType = FixAccounts.VoucherTypeForChequeReturn;
            _ChequeReturnID = "";
            _IfChequeReturn = "";
        }
        public override void DoValidate()
        {
            try
            {
                if (CBAccountID == "" || CBAccountID == null)
                    ValidationMessages.Add("Please Select Account");
                if (CBAmount == 0)
                    ValidationMessages.Add("Invalid Amount");
                if (CBBankAccountID == "" || CBBankAccountID == null)
                    ValidationMessages.Add("Please Select Bank Account");
                if (CBBankID == "" || CBBankID == null)
                    ValidationMessages.Add("Please Select Bank");
                if (CBBranchID == "" || CBBranchID == null)
                    ValidationMessages.Add("Please Select Branch");
                if (CBChequeNumber == "")
                    ValidationMessages.Add("Please Type Cheque Number");
                if (CBChequeDate == null )
                    ValidationMessages.Add("Please Type Cheque Date");
                

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
        #endregion

        #region Public Methods
        public DataTable GetOverviewData(string CBType)
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.GetOverviewData(CBType);
        }
        public DataTable GetOverviewDataForChequeReturn(string CBType)
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.GetOverviewDataForChequeReturn(CBType);
        }
        public DataTable GetOverviewDataForReport(string bankID, string CBType,string fromDate, string toDate)
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.GetOverviewDataForReport(bankID, CBType,fromDate,toDate);
        }
        public DataTable GetDataForReportChequesReceivedButNotCleared(string bankID, string CBType, string fromDate, string toDate)
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.GetDataForReportChequesReceivedButNotCleared(bankID, CBType, fromDate, toDate);
        }
        public int GetAndUpdateBKRNumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetBankReceipt(voucherseries);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return vouno;
        }
        public int GetAndUpdateChequreReturn(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetChequeReturn(voucherseries);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return vouno;
        }
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBBankReceipt CBtran = new DBBankReceipt();
                drow = CBtran.ReadDetailsByID(Id);

                if (drow != null)
                {
                    CBAccountID = drow["AccountId"].ToString();
                    CBName = drow["AccName"].ToString();
                    CBAddress1 = drow["AccAddress1"].ToString();
                    CBAddress2 = drow["AccAddress2"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = Convert.ToString(drow["VoucherDate"]);
                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CBChequeNumber = Convert.ToString(drow["ChequeNumber"]);
                    CBChequeDate = Convert.ToString(drow["ChequeDate"]);
                    CBBankID = Convert.ToString(drow["CustomerBankID"]);
                    CBBranchID = Convert.ToString(drow["CustomerBranchID"]);
                    CBBankAccountID = drow["ChequeDepositedBankID"].ToString();
                    IfChequeReturn = drow["IfChequeReturn"].ToString();
                    if (drow["OnAccountAmount"] != DBNull.Value)
                        CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);

                    preAccountID = CBAccountID;
                    preName = CBName;
                    preVouNo = CBVouNo;
                    preVouDate = CBVouDate;
                    preNarration = CBNarration;
                    preAmount = CBAmount;
                    preChequeNumber = CBChequeNumber;
                    preChequeDate = CBChequeDate;
                    preBankID = CBBankID;
                    preBranchID = CBBranchID;
                    preOnAccountAmount = CBOnAccountAmount;
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public bool ReadDetailsByIDForChanged()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBBankReceipt CBtran = new DBBankReceipt();
                drow = CBtran.ReadDetailsByIDForChanged(Id);

                if (drow != null)
                {
                    CBAccountID = drow["AccountId"].ToString();
                    CBName = drow["AccName"].ToString();
                    CBAddress1 = drow["AccAddress1"].ToString();
                    CBAddress2 = drow["AccAddress2"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = Convert.ToString(drow["VoucherDate"]);
                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CBChequeNumber = Convert.ToString(drow["ChequeNumber"]);
                    CBChequeDate = Convert.ToString(drow["ChequeDate"]);
                    CBBankID = Convert.ToString(drow["CustomerBankID"]);
                    CBBranchID = Convert.ToString(drow["CustomerBranchID"]);
                    CBBankAccountID = drow["ChequeDepositedBankID"].ToString();
                    if (drow["OnAccountAmount"] != DBNull.Value)
                        CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public bool ReadDetailsByIDForDeleted()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBBankReceipt CBtran = new DBBankReceipt();
                drow = CBtran.ReadDetailsByIDForDeleted(Id);

                if (drow != null)
                {
                    CBAccountID = drow["AccountId"].ToString();
                    CBName = drow["AccName"].ToString();
                    CBAddress1 = drow["AccAddress1"].ToString();
                    CBAddress2 = drow["AccAddress2"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = Convert.ToString(drow["VoucherDate"]);
                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CBChequeNumber = Convert.ToString(drow["ChequeNumber"]);
                    CBChequeDate = Convert.ToString(drow["ChequeDate"]);
                    CBBankID = Convert.ToString(drow["CustomerBankID"]);
                    CBBranchID = Convert.ToString(drow["CustomerBranchID"]);
                    CBBankAccountID = drow["ChequeDepositedBankID"].ToString();
                    if (drow["OnAccountAmount"] != DBNull.Value)
                        CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public bool ReadDetailsByIDVoucherNumber()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBBankReceipt CBtran = new DBBankReceipt();
                drow = CBtran.ReadDetailsByVouNumber(CBVouNo);

                if (drow != null)
                {
                    Id = drow["CBID"].ToString();
                    CBAccountID = drow["AccountId"].ToString();
                    CBName = drow["AccName"].ToString();
                    CBAddress1 = drow["AccAddress1"].ToString();
                    CBAddress2 = drow["AccAddress2"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = Convert.ToString(drow["VoucherDate"]);
                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CBChequeNumber = Convert.ToString(drow["ChequeNumber"]);
                    CBChequeDate = Convert.ToString(drow["ChequeDate"]);
                    CBBankID = Convert.ToString(drow["CustomerBankID"]);
                    CBBranchID = Convert.ToString(drow["CustomerBranchID"]);
                    CBBankAccountID = drow["ChequeDepositedBankID"].ToString();
                    if (drow["OnAccountAmount"] != DBNull.Value)
                        CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public DataTable ReadBillDetailsByID()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.GetSaleDetailsByID(CBAccountID);
        }

     

        public DataTable ReadBillDetailsByIDforModify()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.GetSaleDetailsByIDforModify(CBAccountID, Id);
        }
        public DataTable ReadStatementDetailsByIDforModify()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.GetStatementDetailsByIDforModify(CBAccountID, Id);
        }
        public DataTable ReadBillDetailsByBKRID()
        {
            DBBankReceipt CBtrancsr = new DBBankReceipt();
            return CBtrancsr.GetSaleDetailsByBKRID(Id, CBAccountID);
            
        }

        public DataTable ReadBillDetailsByBKRIDForChanged()
        {
            DBBankReceipt CBtrancsr = new DBBankReceipt();
            return CBtrancsr.GetSaleDetailsByBKRIDForChanged(Id, CBAccountID);
        }
        public DataTable ReadBillDetailsByBKRIDForDeleted()
        {
            DBBankReceipt CBtrancsr = new DBBankReceipt();
            return CBtrancsr.GetSaleDetailsByBKRIDForDeleted(Id, CBAccountID);
        }

        public DataTable ReadStatementDetailsByBKRID()
        {
            DBBankReceipt CBtrancsr = new DBBankReceipt();
            return CBtrancsr.GetStatementDetailsByBKRID(Id, CBAccountID);
        }
        public DataTable ReadStatementDetailsByID()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.GetStatementDetailsByID(CBAccountID);
        } 
        public bool RevertPreviousSalesBalance(string nSaleID, double nClearedAmount)
        {
            DBBankReceipt CR = new DBBankReceipt();
            return CR.RevertPreviousSaleBalance(nSaleID, nClearedAmount);
        }
        public bool RevertPreviousStatementBalance(string nSaleID, double nClearedAmount)
        {
            DBBankReceipt CR = new DBBankReceipt();
            return CR.RevertPreviousStatementBalance(nSaleID, nClearedAmount);
        }
        public bool AddDetailsChequeReturn()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.AddDetailsChequeReturn(ChequeReturnID,ChequeReturnVouType,ChequeReturnVouNo,ChequeReturnVouDate,ChequeReturnCharges,  Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount, CBBankAccountID, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, CBOnAccountAmount, CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddDetails()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.AddDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount, CBBankAccountID, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate,CBOnAccountAmount, CreatedBy,CreatedDate,CreatedTime);
        }

        public bool AddChangedDetails()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.AddChangedDetails(Id, ChangedID, preAccountID, preNarration, preVouType, preVouNo, preVouDate, preAmount, preBankAccountID, preBankID, preBranchID, preChequeNumber, preChequeDate, preOnAccountAmount, ModifiedBy, ModifiedDate, ModifiedTime);
        }


        public bool AddDeletedDetails()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.AddDeletedDetails(Id, preAccountID, preNarration, preVouType, preVouNo, preVouDate, preAmount, preBankAccountID, preBankID, preBranchID, preChequeNumber, preChequeDate, preOnAccountAmount, ModifiedBy, ModifiedDate, ModifiedTime);
        }

      
        public bool AddAccountDetailsIntbltrnacDebit()
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            if (CBAmount > 0)
            {
                bRetValue = dbpur.AddDetailsForAccountsBankReceipt(Id, DetailId, CBBankAccountID, CBAmount, 0, CBAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, CreatedBy, CreatedDate, CreatedTime);
            }
            return bRetValue;
        }
        public bool AddAccountDetailsIntbltrnacCredit()
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            if (CBAmount > 0)
            {
                bRetValue = dbpur.AddDetailsForAccountsBankReceipt(Id, DetailId, CBAccountID, 0, CBAmount, CBBankAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, CreatedBy, CreatedDate, CreatedTime);
            }
            return bRetValue;
        }

        public bool UpdateAccountDetailsIntbltrnacForFifth()
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            if (CBAmount > 0)
            {
                bRetValue = dbpur.UpdateAccountDetailsIntbltrnacForFifth(Id, DetailId, CBBankAccountID, CBAmount, 0, CBAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, CreatedBy, CreatedDate, CreatedTime);
            }
            return bRetValue;
        }

        public bool DeleteAccountDetails()
        {
            bool bRetValue = false;
            try
            {
                DBAccountDetails dbpur = new DBAccountDetails();
                bRetValue = dbpur.DeleteAccountDetailsFromtbltrnac(Id);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return bRetValue;

        }
        public bool UpdateBankReceiptVoucherForChequeReturn()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.UpdateBankReceiptVoucherForChequeReturn(Id, "Y");
        }
        public bool AddParticularsDetailsChequeReturn()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.AddDetailsParticularsChequeReturn(ChequeReturnID ,Id, DetailId, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount, DetailId, SerialNumber);
        }
        public bool AddParticularsDetails()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.AddDetailsParticulars(Id,DetailId, DSaleId , DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount,DetailId,SerialNumber);
        }
        public bool AddParticularsDetailsChanged()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.AddDetailsParticularsChanged(Id,ChangedID, DetailId, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount, DetailId, SerialNumber);
        }
        public bool AddParticularsDetailsDeleted()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.AddDetailsParticularsDeleted(Id, DetailId, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount, DetailId, SerialNumber);
        }
        public bool UpdateSCCBill()
        {
            DBBankReceipt CBupd = new DBBankReceipt();
            return CBupd.UpdateDetailsSCCBill(Id,DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount);
        }
        public bool UpdateSaleStatement()
        {
            DBBankReceipt CBupd = new DBBankReceipt();
            return CBupd.UpdateSaleStatement(Id, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount);
        }
        //public bool UpdateOpeningBalance()
        //{
        //    DBBankReceipt CBupd = new DBBankReceipt();
        //    return CBupd.UpdateOpeningBalance(CBAccountID, DClearedAmount, OpeningCleared);
        //}
        public bool UpdateOpeningBalanceReducePrevious(string accountID, double preCleared)
        {
            DBBankReceipt CBupd = new DBBankReceipt();
            return CBupd.UpdateOpeningBalanceReducePrevious(accountID, preCleared);
        }
        public bool UpdateOpeningBalanceAddNew()
        {
            DBBankReceipt CBupd = new DBBankReceipt();
            return CBupd.UpdateOpeningBalanceAddNew(CBAccountID, DClearedAmount);
        }
        public bool UpdateDetails()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.UpdateDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount, CBBankAccountID, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate,CBOnAccountAmount, ModifiedBy,ModifiedDate,ModifiedTime);
        }
        public bool UpdateDetailsForFifth()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.UpdateDetailsForFifth(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount, CBBankAccountID, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, CBOnAccountAmount, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool DeleteDetails()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.DeletePreviosRowsByID(Id);
        }

        #endregion  
    
        
    
      
    }
}
