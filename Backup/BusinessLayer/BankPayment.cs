using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    class BankPayment : BaseObject
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

        private string _DMasterId;
        private string _DSaleId;
        private string _DVoucherSeries;
        private string _DVoucherType;
        private int _DvoucherNumber;
        private string _DVoucherDate;
        private string _DSubType;
        private double _DBillAmount;
        private double _DBalanceAmount;
        private double _DClearedAmount;
        private double _DDiscountAmount;

        private double _OpeningBalance;
        private double _OpeningCleared;
        private double _OpeningClearedInVoucher;

        private string _preAccountId;
        private string _preName;       
        private string _preNarration;
        private string _preVouType;
        private int _preVouNo;
        private string _preVouDate;
        private int _preNoOfRows;
        private double _preAmount;
        private string _preBankAccountId;
        private string _preChequeNumber;
        private string _preChequeDate;
        private double _preOnAccountAmount;
        private string _preBankID;
        private string _preBranchID;

        # endregion

        #region Constructors
        public BankPayment()
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
        public string DSaleId
        {
            get { return _DSaleId; }
            set { _DSaleId = value; }
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
            get { return _DDiscountAmount; }
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
        public string preBankID
        {
            get { return _preBankID; }
            set { _preBankID = value; }
        }
        public string preBranchID
        {
            get { return _preBranchID; }
            set { _preBranchID = value; }
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
            _CBAddress1 = "";
            _CBAddress2 = "";
            _CBNarration = "";
            _CBVouType = FixAccounts.VoucherTypeForBankPayment;
            _CBVouNo = 0;
            _CBNoOfRows = 0;
            _CBAmount = 0;
            _CBVouDate = "";
            _CBBankAccountId = "";
            _CBBankId = "";
            _CBBranchId = "";
            _CBChequeNumber = "";
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

            _preName = "";
            _preAccountId = "";          
            _preNarration = "";
            _preVouType = FixAccounts.VoucherTypeForBankPayment;
            _preVouNo = 0;
            _preNoOfRows = 0;
            _preAmount = 0;
            _preVouDate = "";
            _preBankAccountId = "";           
            _preChequeNumber = "";
            _preBankID = "";
            _preBranchID = "";

        }
        public override void DoValidate()
        {
            try
            {
                if (CBBankAccountID == null || CBBankAccountID == "")
                    ValidationMessages.Add("Please Select Bank Account");
                if (CBAccountID == null || CBAccountID == "")
                    ValidationMessages.Add("Please Enter Account Name.");
                if (CBAmount == 0)
                    ValidationMessages.Add("Invalid Amount");
                if (CBChequeNumber == null || CBChequeNumber == "")
                    ValidationMessages.Add("Please Enter Cheque Number");
                if (Convert.ToInt32(CBChequeDate.ToString()) < Convert.ToInt32(General.ShopDetail.Shopsy.ToString()))
                    ValidationMessages.Add("Please Check Cheque Date");
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
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.GetOverviewData(CBType);
        }
        public DataTable GetOverviewData(string bankID, string CBType,string fromDate, string toDate)
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.GetOverviewData(bankID, CBType,fromDate,toDate);
        }
        public DataTable GetDataForChequePaidButNotCleared(string bankID,string CBType, string fromDate, string toDate )
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.GetDataForChequePaidButNotCleared(CBType, fromDate, toDate, bankID);
        }
        public DataTable GetOverviewDataForTodaysCheques(string CBType, string CBType2, string fromDate, string toDate)
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.GetOverviewDataForTodaysCheques(CBType,CBType2, fromDate,toDate);
        }
        public int GetAndUpdateBKPNumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetBankPayment(voucherseries);
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
                DBBankPayment CBtran = new DBBankPayment();
                drow = CBtran.ReadDetailsByID(Id);

                if (drow != null)
                {
                    CBAccountID = drow["AccountId"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = drow["VoucherDate"].ToString();
                    if (drow["Narration"] != null)
                        CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["OnAccountAmount"] != DBNull.Value)
                        CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);
                    if (drow["ChequeNumber"] != null)
                        CBChequeNumber = Convert.ToString(drow["ChequeNumber"].ToString());
                    if (drow["ChequeDepositedBankID"] != null)
                        CBBankAccountID = Convert.ToString(drow["ChequeDepositedBankID"].ToString());
                    CBChequeDate = drow["ChequeDate"].ToString();

                    preAccountID = CBAccountID;
                    preVouNo = CBVouNo;
                    preVouDate = CBVouDate;
                    preNarration = CBNarration;
                    preAmount = CBAmount;
                    preOnAccountAmount = CBOnAccountAmount;
                    preChequeNumber = CBChequeNumber;
                    preChequeDate = CBChequeDate;
                    preBankAccountID = CBBankAccountID;
                    
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
                DBBankPayment CBtran = new DBBankPayment();
                drow = CBtran.ReadDetailsByIDForChanged(Id);

                if (drow != null)
                {
                    CBAccountID = drow["AccountId"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = drow["VoucherDate"].ToString();
                    if (drow["Narration"] != null)
                        CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["OnAccountAmount"] != DBNull.Value)
                        CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);
                    if (drow["ChequeNumber"] != null)
                        CBChequeNumber = Convert.ToString(drow["ChequeNumber"].ToString());
                    if (drow["ChequeDepositedBankID"] != null)
                        CBBankAccountID = Convert.ToString(drow["ChequeDepositedBankID"].ToString());
                    CBChequeDate = drow["ChequeDate"].ToString();

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
                DBBankPayment CBtran = new DBBankPayment();
                drow = CBtran.ReadDetailsByIDForDeleted(Id);

                if (drow != null)
                {
                    CBAccountID = drow["AccountId"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = drow["VoucherDate"].ToString();
                    if (drow["Narration"] != null)
                        CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["OnAccountAmount"] != DBNull.Value)
                        CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);
                    if (drow["ChequeNumber"] != null)
                        CBChequeNumber = Convert.ToString(drow["ChequeNumber"].ToString());
                    if (drow["ChequeDepositedBankID"] != null)
                        CBBankAccountID = Convert.ToString(drow["ChequeDepositedBankID"].ToString());
                    CBChequeDate = drow["ChequeDate"].ToString();

                
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public bool ReadDetailsByVoucherNumber()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBBankPayment CBtran = new DBBankPayment();
                drow = CBtran.ReadDetailsByVoucherNumber(CBVouNo,CBVouType);

                if (drow != null)
                {
                    Id = drow["CBID"].ToString();
                    CBAccountID = drow["AccountId"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = drow["VoucherDate"].ToString();
                    if (drow["Narration"] != null)
                        CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["OnAccountAmount"] != DBNull.Value)
                        CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);
                    if (drow["ChequeNumber"] != null)
                        CBChequeNumber = Convert.ToString(drow["ChequeNumber"].ToString());
                    if (drow["ChequeDepositedBankID"] != null)
                        CBBankAccountID = Convert.ToString(drow["ChequeDepositedBankID"].ToString());
                    CBChequeDate = drow["ChequeDate"].ToString();                  
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
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.GetPurchaseDetailsByID(CBAccountID);
        }

      
        public DataTable ReadBillDetailsByIDforModify()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.GetPurchaseDetailsByIDforModify(CBAccountID, Id);
        }
        public DataTable ReadStatementDetailsByIDforModify()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.GetStatementDetailsByIDforModify(CBAccountID, Id);
        }
        public DataTable ReadStatementDetailsByID()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.GetStatementDetailsByID(CBAccountID);
        } 
        public DataTable ReadBillDetailsByBKPID()
        {
            DBBankPayment CBtrancsr = new DBBankPayment();
            return CBtrancsr.GetPurchaseDetailsByBKPID(Id, CBAccountID);
        }
        public DataTable ReadBillDetailsByBKPIDForChanged()
        {
            DBBankPayment CBtrancsr = new DBBankPayment();
            return CBtrancsr.GetPurchaseDetailsByBKPIDForChanged(Id, CBAccountID);
        }
        public DataTable ReadBillDetailsByBKPIDForDeleted()
        {
            DBBankPayment CBtrancsr = new DBBankPayment();
            return CBtrancsr.GetPurchaseDetailsByBKPIDForDeleted(Id, CBAccountID);
        }


        public bool RevertPreviousPurchaseBalanceBill(string nSaleID, double nClearedAmount)
        {
            DBBankPayment CR = new DBBankPayment();
            return CR.RevertPreviousPurchaseBalanceBill(nSaleID, nClearedAmount);
        }
        public bool RevertPreviousPurchaseBalanceStatement(string nSaleID, double nClearedAmount)
        {
            DBBankPayment CR = new DBBankPayment();
            return CR.RevertPreviousPurchaseBalanceStatement(nSaleID, nClearedAmount);
        }

        public bool SaveIntblTrnac()
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            try
            {
                if (CBAmount > 0)
                {
                    DetailId  = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    bRetValue = dbpur.AddDetailsForAccountsBankPayment(Id,DetailId, CBAccountID, CBAmount, 0, CBBankAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration,CBChequeNumber,CBChequeDate, CreatedBy, CreatedDate, CreatedTime);
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    bRetValue = dbpur.AddDetailsForAccountsBankPayment(Id,DetailId, CBBankAccountID, 0, CBAmount, CBAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration,CBChequeNumber,CBChequeDate, CreatedBy, CreatedDate, CreatedTime);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }  
            return bRetValue;
        }

        public bool SaveIntblTrnacForFifth()
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            try
            {
               
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    bRetValue = dbpur.UpdateDetailsForAccountsBankPaymentForFifth(Id, DetailId, CBAccountID, CBAmount, 0, CBBankAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration, CBChequeNumber, CBChequeDate, CreatedBy, CreatedDate, CreatedTime);
                  
               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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

        public bool AddDetails()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.AddDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount, CBBankAccountID, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool AddChangedDetails()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.AddChangedDetails(Id,ChangedID, preAccountID, preNarration, preVouType, preVouNo, preVouDate, preAmount, preBankAccountID, preBankID, preBranchID, preChequeNumber, preChequeDate, ModifiedBy , ModifiedDate, ModifiedTime);
        }

        public bool AddDeletedDetails()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.AddDeletedDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount, CBBankAccountID, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, ModifiedBy,ModifiedDate,ModifiedTime );
        }
        public bool AddParticularsDetails()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.AddDetailsParticulars(Id,DetailId, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount,SerialNumber);
        }
        public bool AddParticularsDetailsChanged()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.AddDetailsParticularsChanged(Id,ChangedID, DetailId, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount, SerialNumber);
        }
        public bool AddParticularsDetailsDeleted()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.AddDetailsParticularsDeleted(Id, DetailId, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount, SerialNumber);
        }
        public bool UpdatePurchaseBill()
        {
            DBBankPayment CBupd = new DBBankPayment();
            return CBupd.UpdateDetailsPurchaseBill(Id,DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount);
        }
        public bool UpdatePurchaseStatement()
        {
            DBBankPayment CBupd = new DBBankPayment();
            return CBupd.UpdateDetailsPurchaseStatement(Id, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount);
        }
        public bool UpdateDetails()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.UpdateDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate,
                CBAmount,CBBankAccountID, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate,ModifiedBy,ModifiedDate,ModifiedTime);
        
        }
        public bool UpdateDetailsForFifth()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.UpdateDetailsForFifth(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate,
                CBAmount, CBBankAccountID, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, ModifiedBy, ModifiedDate, ModifiedTime);

        }
        public bool UpdateOpeningBalanceReducePrevious(string accountID, double preCleared)
        {
            DBBankPayment CBupd = new DBBankPayment();
            return CBupd.UpdateOpeningBalanceReducePrevious(accountID, preCleared);
        }

        public bool UpdateOpeningBalanceAddNew()
        {
            DBBankPayment CBupd = new DBBankPayment();
            return CBupd.UpdateOpeningBalanceAddNew(CBAccountID, DClearedAmount);
        }
        public bool DeleteDetails()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBBankPayment CBtran = new DBBankPayment();
            return CBtran.DeletePreviosRowsByID(Id);
        }
        #endregion
    }
}


        
