using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    class CashReceipt : BaseObject
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
        private double _CBOnAccountAmount;
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
        private double _preOnAccountAmount;

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
      
        # endregion

        #region Constructors
        public CashReceipt()
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
            get { return  _OpeningCleared; }
            set { _OpeningCleared = value; }
        }
        public double OpeningClearedInVoucher
        {
            get { return _OpeningClearedInVoucher; }
            set { _OpeningClearedInVoucher = value; }
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
            _CBVouType = FixAccounts.VoucherTypeForCashReceipt;
            _CBVouNo = 0;
            _CBNoOfRows = 0;
            _CBAmount = 0;
            _CBVouDate = "";
            _CBOnAccountAmount = 0;
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
           
        }
        public override void DoValidate()
        {
            try
            {
                if (CBAccountID == "" || CBAccountID == null)
                    ValidationMessages.Add("Please enter the Account Name.");
                if (CBAmount == 0)
                    ValidationMessages.Add("Invalid Amount");
                if (IFEdit == "Y" && CBVouNo == 0)
                    ValidationMessages.Add("Invalid Voucher Number");
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
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.GetOverviewData(CBType);
        }
        public DataTable GetOverviewData(string CBType,string fromDate, string toDate)
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.GetOverviewData(CBType,fromDate,toDate);
        }
        public int GetAndUpdateCSRNumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetCashReceipt(voucherseries);
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
                DBCashReceipt CBtran = new DBCashReceipt();
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
                    if (drow["OnAccountAmount"] != DBNull.Value)
                        CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);


                    preAccountID = CBAccountID;
                    preName = CBName;                  
                    preVouNo = CBVouNo;
                    preVouDate = CBVouDate;
                    preNarration = CBNarration;
                    preAmount = CBAmount;
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
                DBCashReceipt CBtran = new DBCashReceipt();
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
                DBCashReceipt CBtran = new DBCashReceipt();
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
        public bool ReadDetailsByVouNumber()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBCashReceipt CBtran = new DBCashReceipt();
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
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.GetSaleDetailsByID(CBAccountID);
        }

        public DataTable ReadStatementDetailsByID()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.GetStatementDetailsByID(CBAccountID);
        } 
        public DataTable ReadBillDetailsByIDforModify()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.GetSaleDetailsByIDforModify(CBAccountID,Id);
        }
        public DataTable ReadStatementDetailsByIDforModify()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.GetStatementDetailsByIDforModify(CBAccountID, Id);
        }

        public DataTable ReadBillDetailsByCSRID()
        {
            DBCashReceipt CBtrancsr = new DBCashReceipt();
            return CBtrancsr.GetSaleDetailsByCSRID(Id,CBAccountID);
        }

        public DataTable ReadBillDetailsByCSRIDForChanged()
        {
            DBCashReceipt CBtrancsr = new DBCashReceipt();
            return CBtrancsr.GetSaleDetailsByCSRIDForChanged(Id, CBAccountID);
        }
        public DataTable ReadBillDetailsByCSRIDForDeleted()
        {
            DBCashReceipt CBtrancsr = new DBCashReceipt();
            return CBtrancsr.GetSaleDetailsByCSRIDForDeleted(Id, CBAccountID);
        }
        public DataTable ReadStatementDetailsByCSRID()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.GetStatementDetailsByCSRID(Id,CBAccountID);
        } 
        public bool RevertPreviousSalesBalance(string nSaleID, double nClearedAmount)
        {
            DBCashReceipt CR = new DBCashReceipt();
            return CR.RevertPreviousSaleBalance(nSaleID,nClearedAmount);
        }
        public bool RevertPreviousStatementBalance(string nSaleID, double nClearedAmount)
        {
            DBCashReceipt CR = new DBCashReceipt();
            return CR.RevertPreviousStatementBalance(nSaleID, nClearedAmount);
        }

        public bool AddDetails()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.AddDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate,CBAmount,CreatedBy,CreatedDate,CreatedTime);
        }

        public bool AddChangedDetails()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.AddChangedDetails(Id, ChangedID, preAccountID, preNarration, preVouType, preVouNo, preVouDate, preAmount, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool AddDeletedDetails()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.AddDeletedDetails(Id, preAccountID, preNarration, preVouType, preVouNo, preVouDate, preAmount, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool AddDetailsFromPatientSale(string iD,string accID,string naration,string vouType,int vouNo,string vouDate,double amount,string createdBy,string createdDate,string createdTime)
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.AddDetails(iD, accID, naration, vouType, vouNo, vouDate, amount, createdBy, createdDate,createdTime);
        }
        public bool AddAccountDetailsIntbltrnacDebit()
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            if (CBAmount > 0)
            {
                bRetValue = dbpur.AddDetailsForAccountsCashReceipt(Id,DetailId, FixAccounts.AccountCash, CBAmount, 0, CBAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration, CreatedBy, CreatedDate, CreatedTime);
            }
            return bRetValue;
        }
        public bool AddAccountDetailsIntbltrnacCredit()
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            if (CBAmount > 0)
            {               
                bRetValue = dbpur.AddDetailsForAccountsCashReceipt(Id,DetailId, CBAccountID, 0, CBAmount, FixAccounts.AccountCash, CBVouType, CBVouNo, CBVouDate, CBNarration, CreatedBy, CreatedDate, CreatedTime);
            }
            return bRetValue;
        }



        public bool AddAccountDetailsIntbltrnacDebitFromPatientSale(string id, string detailID, string accountID, double amountdebit, double amountcredit,string accAccountID, string vouType, int vouNo, string vouDate, string narration, string createdBy, string createdDate, string createdTime)
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            if (amountdebit > 0)
            {
                bRetValue = dbpur.AddDetailsForAccountsCashReceipt(id,detailID,accountID, amountdebit,amountcredit, accAccountID, vouType, vouNo, vouDate, narration,createdBy,createdDate,createdTime);
            }
            return bRetValue;
        }
        public bool AddAccountDetailsIntbltrnacCreditFromPatientSale(string id, string detailID, string accountID, double amountdebit, double amountcredit, string accAccountID, string vouType, int vouNo, string vouDate, string narration, string createdBy, string createdDate, string createdTime)
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            if (amountcredit > 0)
            {
                bRetValue = dbpur.AddDetailsForAccountsCashReceipt(id, detailID, accountID, amountdebit, amountcredit, accAccountID, vouType, vouNo, vouDate, narration, createdBy, createdDate, createdTime);
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
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.AddDetailsParticulars(Id,DetailId,  DSaleId , DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount,SerialNumber);
        }

        public bool AddParticularsDetailsChanged()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.AddDetailsParticularsChanged(Id,ChangedID, DetailId, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount, SerialNumber);
        }

        public bool AddParticularsDetailsDeleted()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.AddDetailsParticularsDeleted(Id, DetailId, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount, SerialNumber);
        }

        public bool AddParticularsDetailsByPatientSale(string iD,string detailID,string saleID,string vouSeries, string vouType, int vouNo,string vouDate, string subType, double billAmount,
                    double balanceAmount, double clearedAmount, double discountAmount,int serialNumber)
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.AddDetailsParticulars(iD,detailID,saleID,vouSeries, vouType, vouNo, vouDate,subType, billAmount,
              balanceAmount, clearedAmount, discountAmount, serialNumber);
        }

        public bool UpdateSCCBill()
        {
            DBCashReceipt CBupd = new DBCashReceipt();
            return CBupd.UpdateDetailsSCCBill(Id, DSaleId , DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount);
        }
        public bool UpdateSaleStatement()
        {
            DBCashReceipt CBupd = new DBCashReceipt();
            return CBupd.UpdateSaleStatement(Id, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount);
        }
        public bool UpdateOpeningBalanceReducePrevious(string accountID,double preCleared)
        {
            DBCashReceipt CBupd = new DBCashReceipt();
            return CBupd.UpdateOpeningBalanceReducePrevious(accountID, preCleared );
        }
        public bool UpdateOpeningBalanceAddNew()
        {
            DBCashReceipt CBupd = new DBCashReceipt();
            return CBupd.UpdateOpeningBalanceAddNew(CBAccountID,DClearedAmount);
        }
        public bool UpdateDetails()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.UpdateDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate,CBAmount,ModifiedBy,ModifiedDate,ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBCashReceipt CBtran = new DBCashReceipt();
            return CBtran.DeletePreviosRowsByID(Id);
        }    

        #endregion  
    }
}
