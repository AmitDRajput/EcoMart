using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class CashPayment : BaseObject
    {

        #region Declaration

        private double _CellOldValueAmount;
        private double _CellOldValueDiscount;
        private string _ModifyEdit;
        private string _ActualAccountID;
        private string _CBAccountId;
     // private int _CBAccountIDINT;
        private string _CBName;
        private string _CBAddress;
        private string _CBAddress1;
        private string _CBAddress2;
        private string _CBNarration;
        private string _CBVouType;
        private string _CBVouSeries;
        private int _CBVouNo;
        private string _CBVouDate;
        private int _CBNoOfRows;
        private double _CBAmount;
        private double _CBOnAccountAmount;

        private double _CellOpeningBalanceOldValueDiscount;
        private double _CellOpeningBalanceOldValueAmount;
      
        private double _DiscountInOpeningBalance;
        private double _CBTotalDiscount;
        //private string _CBJVID;
        private string  _CBJVID; 
        private int _CBJVIDpay;// added by snehal
        private int _CBJVNo;
        private string _CBFromDate;
        private string _CBToDate;
        private int _PaymentSubType;

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
        private int _preAccountIdINT;
        private string _PreName;
        private string _PreAddress;
        private string _PreNarration;
        private string _PreVouType;
        private int _PreVouNo;
        private string _PreVouDate;
        private int _PreNoOfRows;
        private double _PreAmount;
        private double _PreOnAccountAmount;

        # endregion

        #region Constructors
        public CashPayment()
        {
            Initialise();
        }
        #endregion

        #region properties
        public int PaymentSubType
        {
            get { return _PaymentSubType; }
            set { _PaymentSubType = value; }
        }
        public string CBFromDate
        {
            get { return _CBFromDate; }
            set { _CBFromDate = value; }
        }
        public string CBToDate
        {
            get { return _CBToDate; }
            set { _CBToDate = value; }
        }
        public int CBJVNo
        {
            get { return _CBJVNo; }
            set { _CBJVNo = value; }
        }
        public string CBJVID
        {
            get { return _CBJVID; }
            set { _CBJVID = value; }
        }
        // added by snehal //
        public int CBJVIDpay
        {
            get { return _CBJVIDpay; }
            set { _CBJVIDpay = value; }
        }  

        public double CBTotalDiscount
        {
            get { return _CBTotalDiscount; }
            set { _CBTotalDiscount = value; }
        }
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
        public double CellOpeningBalanceOldValueAmount
        {
            get { return _CellOpeningBalanceOldValueAmount; }
            set { _CellOpeningBalanceOldValueAmount = value; }
        }
        public double CellOpeningBalanceOldValueDiscount
        {
            get { return _CellOpeningBalanceOldValueDiscount; }
            set { _CellOpeningBalanceOldValueDiscount = value; }
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
        public string CBAccountID
        {
            get { return _CBAccountId; }
            set { _CBAccountId = value; }
        }

        //public int CBAccountIDINT
        //{
        //    get { return _CBAccountIDINT; }
        //    set { _CBAccountIDINT = value; }
        //}

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
        public string CBAddress
        {
            get { return _CBAddress; }
            set { _CBAddress = value; }
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
        public string CBVouSeries
        {
            get { return _CBVouSeries; }
            set { _CBVouSeries = value; }
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
        public double DiscountInOpeningBalance
        {
            get { return _DiscountInOpeningBalance; }
            set { _DiscountInOpeningBalance = value; }
        }

        public string preAccountId
        {
            get { return _preAccountId;; }
            set { _preAccountId = value; }
        }

        public int preAccountIdINT
        {
            get { return _preAccountIdINT;  }
            set { _preAccountIdINT = value; }
        }


        public string PreVouDate
        {
            get { return _PreVouDate; }
            set { _PreVouDate = value; }
        }

        public string PreName
        {
            get { return _PreName; }
            set { _PreName = value; }
        }
        public string PreAddress
        {
            get { return _PreAddress; }
            set { _PreAddress = value; }
        }
        public string PreNarration
        {
            get { return _PreNarration; }
            set { _PreNarration = value; }
        }
        public string PreVouType
        {
            get { return _PreVouType; }
            set { _PreVouType = value; }
        }
        public int PreVouNo
        {
            get { return _PreVouNo; }
            set { _PreVouNo = value; }
        }
        public int PreNoOFRows
        {
            get { return _PreNoOfRows; }
            set { _PreNoOfRows = value; }
        }
        public double PreAmount
        {
            get { return _PreAmount; }
            set { _PreAmount = value; }
        }
        public double PreOnAccountAmount
        {
            get { return _PreOnAccountAmount; }
            set { _PreOnAccountAmount = value; }
        }










        #endregion

        #region Internal Methods

        public override void Initialise()
        {
            base.Initialise();
            _CBJVID = "";
            _CBJVIDpay = 0;
            _CBJVNo = 0;
            _CBTotalDiscount = 0;
            _CellOldValueAmount = 0;
            _CellOldValueDiscount = 0;
            _ModifyEdit = "N";
            _ActualAccountID = null;        
            _CBName = "";
            _CBAccountId = "";
            _CBAddress = "";
            _CBAddress1 = "";
            _CBAddress2 = "";
            _CBNarration = "";
            _CBVouType = FixAccounts.VoucherTypeForCashPayment;
            _CBVouNo = 0;
            _CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            _CBNoOfRows = 0;
            _CBAmount = 0;
            _CBVouDate = "";
            _CBOnAccountAmount = 0;
            _OpeningBalance = 0;
            _OpeningCleared = 0;
            _OpeningClearedInVoucher = 0;
            _DiscountInOpeningBalance = 0;
            _PaymentSubType = 3;
            _PreName = "";
            _preAccountId = "";
            _PreAddress = "";
            _PreNarration = "";
            _PreVouType = FixAccounts.VoucherTypeForCashPayment;
            _PreVouNo = 0;
            _PreNoOfRows = 0;
            _PreAmount = 0;
            _PreVouDate = "";
            _PreOnAccountAmount = 0;

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

            _CellOpeningBalanceOldValueDiscount = 0;
            _CellOpeningBalanceOldValueAmount = 0;
            _CBFromDate = "";
            _CBToDate = "";

        }
        public override void DoValidate()
        {
            try
            {
                if (CBVouNo == 0)
                {
                    ValidationMessages.Add("Error while saving, Please save again...");
                }
                if (CBAccountID == "" || CBAccountID == null)
                    ValidationMessages.Add("Please Enter Account Name.");
                if (CBAmount == 0)
                    ValidationMessages.Add("Invalid Amount");

                bool retValue = General.CheckDates(CBVouDate, CBVouDate);
                if (retValue == false)
                {
                    ValidationMessages.Add("Please Check Date...");
                }
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
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.GetOverviewData(CBType);
        }
        public DataTable GetOverviewData(string CBType, string fromDate, string toDate)
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.GetOverviewData(CBType, fromDate, toDate);
        }

        public int GetAndUpdateCSPNumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetCashPayment(voucherseries);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return vouno;
        }
        public int GetAndUpdateJVNumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetJV(voucherseries);
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
                DBCashPayment CBtran = new DBCashPayment();
                drow = CBtran.ReadDetailsByID(Id);

                if (drow != null)
                {
                    CBAccountID = (drow["AccountID"]).ToString();
                    CBAccountIDINT = Convert.ToInt32(drow["AccountID"]);
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = Convert.ToString(drow["VoucherDate"].ToString());
                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["OnAccountAmount"] != DBNull.Value)
                        CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);
                    if (drow["TotalDiscount"] != DBNull.Value)
                        CBTotalDiscount = Convert.ToDouble(drow["TotalDiscount"].ToString());
                    if (drow["JVNumber"] != DBNull.Value)
                        CBJVNo = Convert.ToInt32(drow["JVNumber"].ToString());

                    preAccountId = CBAccountID;
                    PreVouNo = CBVouNo;
                    PreVouDate = CBVouDate;
                    PreNarration = CBNarration;
                    PreAmount = CBAmount;
                    PreOnAccountAmount = CBOnAccountAmount;
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
                DBCashPayment CBtran = new DBCashPayment();
                drow = CBtran.ReadDetailsByIDForChanged(Id);

                if (drow != null)
                {
                    CBAccountID = drow["AccountId"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = Convert.ToString(drow["VoucherDate"].ToString());
                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
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
                DBCashPayment CBtran = new DBCashPayment();
                drow = CBtran.ReadDetailsByIDForDeleted(Id);

                if (drow != null)
                {
                    CBAccountID = drow["AccountId"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = Convert.ToString(drow["VoucherDate"].ToString());
                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
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
        public DataRow ReadDetailsByVoucherNumber()
        {
           
            DataRow drow = null;
            try
            {
                DBCashPayment CBtran = new DBCashPayment();
                drow = CBtran.ReadDetailsByVoucherNumber(CBVouNo);

                if (drow != null)
                {
                    Id = drow["CBID"].ToString();
                    CBAccountID = drow["AccountId"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBVouDate = Convert.ToString(drow["VoucherDate"].ToString());
                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["OnAccountAmount"] != DBNull.Value)
                        CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);
                   
                }
            }
                 catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return drow;
        }
        public DataTable ReadBillDetailsByID()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.GetPurchaseDetailsByID(CBAccountID,CBFromDate,CBToDate);
        }
        public DataTable ReadBillDetailsByIDforModify()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.GetPurchaseDetailsByIDforModify(CBAccountID, Id);
        }

        public DataTable ReadBillDetailsByCSPID()
        {
            DBCashPayment CBtrancsr = new DBCashPayment();
            return CBtrancsr.GetPurchaseDetailsByCSPID(Id, CBAccountID);
        }

        public DataTable ReadBillDetailsByCSPIDForChanged()
        {
            DBCashPayment CBtrancsr = new DBCashPayment();
            return CBtrancsr.GetPurchaseDetailsByCSPIDForChanged(Id, CBAccountID);
        }
        public DataTable ReadStatementDetailsByID()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.GetStatementDetailsByID(CBAccountID);
        } 
        public DataTable ReadStatementDetailsByIDforModify()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.GetStatementDetailsByIDforModify(CBAccountID, Id);
        }

        public DataTable ReadBillDetailsByCSPIDForDeleted()
        {
            DBCashPayment CBtrancsr = new DBCashPayment();
            return CBtrancsr.GetPurchaseDetailsByCSPIDForDeleted(Id, CBAccountID);
        }
        public bool RevertPreviousPurchaseBalanceBill(string nSaleID, double nClearedAmount)
        {
            DBCashPayment CR = new DBCashPayment();
            return CR.RevertPreviousPurchaseBalanceBill(nSaleID, nClearedAmount);
        }
        public bool RevertPreviousPurchaseBalanceStatement(string nSaleID, double nClearedAmount)
        {
            DBCashPayment CR = new DBCashPayment();
            return CR.RevertPreviousPurchaseBalanceStatement(nSaleID, nClearedAmount);
        }
        public DataTable GetOpeningBalance()
        {
            DBAccount  CBtran = new DBAccount();
            return CBtran.GetCreditorListForPayment();
        }
       
        public int AddDetails()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.AddDetails(Id,CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount,CBTotalDiscount,CBJVNo, CBJVIDpay, CBOnAccountAmount, CreatedBy,CreatedDate,CreatedTime);
        }
        public bool AddChangedDetails()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.AddChangedDetails(Id, ChangedID, preAccountId, PreNarration, PreVouType, PreVouNo, PreVouDate, PreAmount, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool AddDeletedDetails()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.AddDeletedDetails(Id, preAccountId, PreNarration, PreVouType, PreVouNo, PreVouDate, PreAmount, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool AddAccountDetails()
        {
            bool bRetValue = false;
            try
            {
                DBAccountDetails dbpur = new DBAccountDetails();              


                if (CBAmount > 0)
                {
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    bRetValue = dbpur.AddDetailsForAccountsCashPayment(IntID,DetailId,  CBAccountID, CBAmount, 0, FixAccounts.AccountCash.ToString(), CBVouType, CBVouNo, CBVouDate, CBNarration, CreatedBy, CreatedDate, CreatedTime);
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    bRetValue = dbpur.AddDetailsForAccountsCashPayment(IntID, DetailId, FixAccounts.AccountCash.ToString(), 0, CBAmount, CBAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration, CreatedBy, CreatedDate, CreatedTime);
                }

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

        public bool AddParticularsDetails()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.AddDetailsParticulars(IntID,DetailId, DSaleId , DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount,SerialNumber);
        }

        public bool AddParticularsDetailsChanged()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.AddDetailsParticularsChanged(Id,ChangedID, DetailId, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount, SerialNumber);
        }

        public bool UpdateJVIDInVoucherCashBankPayment()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.AddDetailsParticularsChanged(IntID, CBJVIDpay,FixAccounts.VoucherTypeForCashPayment);
        }

        public bool AddParticularsDetailsDeleted()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.AddDetailsParticularsDeleted(Id, DetailId, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount, SerialNumber);
        }
        public bool UpdatePurchaseBill()
        {
            DBCashPayment CBupd = new DBCashPayment();
            return CBupd.UpdateDetailsPurchaseBill(Id,DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount);
        }

        public bool UpdatePurchaseStatement()
        {
            DBCashPayment CBupd = new DBCashPayment();
            return CBupd.UpdateDetailsPurchaseStatement(Id, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount);
        }

        public bool UpdateDetails()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.UpdateDetails(IntID, CBAccountIDINT, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount, CBTotalDiscount, CBJVNo, CBJVIDpay, CBOnAccountAmount, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool UpdateOpeningBalanceReducePrevious(string accountID, double preCleared)
        {
            DBBankPayment CBupd = new DBBankPayment();
            return CBupd.UpdateOpeningBalanceReducePrevious(accountID, preCleared);
        }

        public bool UpdateOpeningBalanceAddNew()
        {
            DBBankPayment CBupd = new DBBankPayment();
            return CBupd.UpdateOpeningBalanceAddNew(CBAccountID, DClearedAmount+DDiscountAmount);
        }
        public bool DeleteDetails()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.DeletePreviosRowsByID(Id);
        }

        #endregion

        public void GetLastRecord()
        {
            DataRow dr;
            try
            {
                DBCashPayment dbs = new DBCashPayment();
                dr = dbs.GetLastRecord(CBVouType, CBVouSeries);
                if (dr != null && dr["CBID"] != null)
                {

                    Id = dr["CBID"].ToString();

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        public int GetLastVoucherNumber(string vouType, string vouSeries)
        {
            DataRow dr;
            int lastvouno = 0;
            try
            {
                DBCashPayment dbs = new DBCashPayment();
                dr = dbs.GetLastVoucherNumber(vouType, vouSeries);
                if (dr != null)
                {

                    lastvouno = Convert.ToInt32(dr["VoucherNumber"].ToString());

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return lastvouno;
        }

        public DataRow GetFirstRecord()
        {
            DataRow dr = null;
            try
            {
                DBCashPayment dbs = new DBCashPayment();
                dr = dbs.GetFirstRecord(CBVouType, CBVouSeries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }
        public int AddToMasterJV()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.AddToMasterJV(IntID, CBJVIDpay, CBJVNo, CBAccountID, CBNarration, CBVouDate, CBTotalDiscount, CreatedBy, CreatedDate, CreatedTime);
        }
        //public bool UpdateMasterJV()
        //{
        //    DBCashReceipt CBtran = new DBCashReceipt();
        //    return CBtran.UpdateMasterJV(Id, CBJVID, CBJVNo, CBAccountID, CBNarration, CBVouDate, CBTotalDiscount, CreatedBy, CreatedDate, CreatedTime);
        //}
        public bool AddJVTotblTrnacDebit()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.AddJVIntblTrnac(IntID, CBJVIDpay, CBJVNo, CBAccountID, CBNarration, CBVouDate, CBTotalDiscount, DetailId, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool AddJVTotblTrnacCredit()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.AddJVIntblTrnacReverse(IntID, CBJVIDpay, CBJVNo, CBAccountID, CBNarration, CBVouDate, CBTotalDiscount, DetailId, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool DeleteJV()
        {
            bool bRetValue = false;
            try
            {
                DBCashPayment dbpur = new DBCashPayment();
                bRetValue = dbpur.DeleteJV(Id);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return bRetValue;
        }

        public DataTable ReadBillDetailsByCSPIDFromtblOld()
        {
            DBCashPayment CBtrancsr = new DBCashPayment();
            return CBtrancsr.GetPurchaseDetailsByCSPIDFromtblOld(Id, CBAccountID);
        }



        public DataTable ReadOldBillDetailsByCSPIDForChanged()
        {
            throw new NotImplementedException();
        }

        public DataTable ReadOldBillDetailsByCSPIDForDeleted()
        {
            throw new NotImplementedException();
        }

        public DataTable ReadOldBillDetailsByID()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.GetPurchaseDetailsByIDOld(CBAccountID);
        }

        public DataTable ReadOldStatementDetailsByID()
        {
            DBCashPayment CBtran = new DBCashPayment();
            return CBtran.GetStatementDetailsByIDOld(CBAccountID);
        }

        public bool UpdatePurchaseStatementOld()
        {
            DBCashPayment CBupd = new DBCashPayment();
            return CBupd.UpdateDetailsPurchaseStatementOld(Id, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount);
        }

        public bool UpdatePurchaseBillOld()
        {
            DBCashPayment CBupd = new DBCashPayment();
            return CBupd.UpdateDetailsPurchaseBillOld(Id, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount);
        }

        // added by snehal // to get jvid from voucherjv table
        public int GetJVID()
        {
            int JVID = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                JVID = dbno.GetJVID();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return JVID;
        }



    }
}
