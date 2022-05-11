using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class ChequeReturn : BaseObject
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
        private string _CBChequeReturnDate;

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
        private string _CBIfChequeReturn;

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

        private string _BKRID;
        private string _BKRVoucherSeries;
        private int _BKRVoucherNumber;
        private string _BKRVoucherDate;
        # endregion

        #region Constructors
        public ChequeReturn()
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
        public string CBIfChequeReturn
        {
            get { return _CBIfChequeReturn; }
            set { _CBIfChequeReturn = value; }
        }
        public int BKRVoucherNumber
        {
            get { return _BKRVoucherNumber; }
            set { _BKRVoucherNumber = value; }
        }
        public string BKRVoucherDate
        {
            get { return _BKRVoucherDate; }
            set { _BKRVoucherDate = value; }
        }
        public string BKRVoucherSeries
        {
            get { return _BKRVoucherSeries; }
            set { _BKRVoucherSeries = value; }
        }
        public string BKRID
        {
            get { return _BKRID; }
            set { _BKRID = value; }
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

        public string CBChequeReturnDate
        {
            get { return _CBChequeReturnDate; }
            set { _CBChequeReturnDate = value; }
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
            get { return _DDiscountAmount; }
            set { _DDiscountAmount = value; }
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
            _CBChequeDate = "";
            _CBChequeNumber = "";
            _CBOnAccountAmount = 0;
            _CBBankName = "";
            _CBBranchName = "";

            _BKRID = "";
            _BKRVoucherDate = "";
            _BKRVoucherNumber = 0;
            _BKRVoucherSeries = "";
            _CBIfChequeReturn = "N";

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
                if (CBVouNo == 0)
                {
                    ValidationMessages.Add("Error while saving, Please save again...");
                }
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
            DBChequeReturn CBtran = new DBChequeReturn();
            return CBtran.GetOverviewData(CBType);
        }
        public DataTable GetOverviewData(string fromDate, string toDate)
        {
            DBChequeReturn CBtran = new DBChequeReturn();
            return CBtran.GetOverviewData(fromDate, toDate);
        }
        public DataTable GetOverviewDataForReport(string bankID, string CBType, string fromDate, string toDate)
        {
            DBBankReceipt CBtran = new DBBankReceipt();
            return CBtran.GetOverviewDataForReport(bankID, CBType, fromDate, toDate);
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
                DBChequeReturn CBtran = new DBChequeReturn();
                drow = CBtran.ReadDetailsByID(BKRID);
                CBIfChequeReturn = "";
                if (drow == null)
                {
                    drow = CBtran.ReadDetailsByIDInVoucherChequeReturn(BKRID);
                    CBIfChequeReturn = "Y";

                }

                if (drow != null)
                {
                    BKRID = drow["CBID"].ToString();
                    CBAccountID = drow["AccountId"].ToString();
                    CBName = drow["AccName"].ToString();
                    CBAddress1 = drow["AccAddress1"].ToString();
                    CBAddress2 = drow["AccAddress2"].ToString();
                    BKRVoucherSeries = drow["VoucherSeries"].ToString();
                    BKRVoucherNumber = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    BKRVoucherDate = Convert.ToString(drow["VoucherDate"]);

                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CBChequeNumber = Convert.ToString(drow["ChequeNumber"]);
                    CBChequeDate = Convert.ToString(drow["ChequeDate"]);
                    CBBankID = Convert.ToString(drow["CustomerBankID"]);
                    CBBranchID = Convert.ToString(drow["CustomerBranchID"]);
                    CBChequeReturnDate = Convert.ToString(drow["TransactionDate"]);
                    CBBankAccountID = drow["ChequeDepositedBankID"].ToString();
                    Id = drow["ChequeReturnID"].ToString();
                    if (CBIfChequeReturn != "Y")
                    {
                        if (drow["IfChequeReturn"] != DBNull.Value)
                            CBIfChequeReturn = drow["IfChequeReturn"].ToString();
                    }

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
                    //CBAccountID = drow["AccountId"].ToString();
                    //CBName = drow["AccName"].ToString();
                    //CBAddress1 = drow["AccAddress1"].ToString();
                    //CBAddress2 = drow["AccAddress2"].ToString();
                    //CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    //CBVouDate = Convert.ToString(drow["VoucherDate"]);
                    //CBNarration = Convert.ToString(drow["Narration"]);
                    //CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    //CBChequeNumber = Convert.ToString(drow["ChequeNumber"]);
                    //CBChequeDate = Convert.ToString(drow["ChequeDate"]);
                    //CBBankID = Convert.ToString(drow["CustomerBankID"]);
                    //CBBranchID = Convert.ToString(drow["CustomerBranchID"]);
                    //CBBankAccountID = drow["ChequeDepositedBankID"].ToString();
                    //if (drow["OnAccountAmount"] != DBNull.Value)
                    //    CBOnAccountAmount = Convert.ToDouble(drow["OnAccountAmount"]);
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


        public DataTable ReadBillDetailsByBKRID()
        {
            DBBankReceipt CBtrancsr = new DBBankReceipt();
            return CBtrancsr.GetSaleDetailsByBKRID(BKRID, CBAccountID);
        }
        public DataTable ReadBillDetailsByBKRIDInDetailChequeReturn()
        {
            DBBankReceipt CBtrancsr = new DBBankReceipt();
            return CBtrancsr.GetSaleDetailsByBKRID(BKRID, CBAccountID);
        }
        public bool RevertPreviousSalesBalance(string nSaleID, double nClearedAmount)
        {
            DBBankReceipt CBtrancsr = new DBBankReceipt();
            return CBtrancsr.RevertPreviousSaleBalance(nSaleID, nClearedAmount);
        }
        public bool RevertPreviousStatementBalance(string nSaleID, double nClearedAmount)
        {
            DBBankReceipt CR = new DBBankReceipt();
            return CR.RevertPreviousStatementBalance(nSaleID, nClearedAmount);
        }
        public int AddDetails()
        {
            DBChequeReturn CBtran = new DBChequeReturn();
            return CBtran.AddDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount, CBBankAccountID, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, CBOnAccountAmount, BKRID, BKRVoucherDate, BKRVoucherNumber, BKRVoucherSeries, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateReturnDate()
        {
            DBChequeReturn CBtran = new DBChequeReturn();
            return CBtran.UpdateReturnDate(Id,CBChequeReturnDate);
        }

        public bool AddAccountDetailsIntbltrnacDebitForChequeReturn()
        {
            bool bRetValue = false;
            try
            {
                DBAccountDetails dbpur = new DBAccountDetails();

                if (CBAmount > 0)
                {
                    bRetValue = dbpur.AddDetailsForAccountsBankReceipt(IntID, DetailId, CBAccountID, CBAmount, 0, CBBankAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, CreatedBy, CreatedDate, CreatedTime);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return bRetValue;
        }

        public bool AddAccountDetailsIntbltrnacCreditForChequeReturn()
        {
            bool bRetValue = false;
            try
            {
                DBAccountDetails dbpur = new DBAccountDetails();

                if (CBAmount > 0)
                {
                    bRetValue = dbpur.AddDetailsForAccountsBankReceipt(IntID, DetailId, CBBankAccountID, 0, CBAmount, CBAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, CreatedBy, CreatedDate, CreatedTime);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return bRetValue;
        }

        public bool UpdateBankReceiptVoucherForChequeReturn()
        {
            bool bRetValue = false;
            try
            {
                DBChequeReturn dbpur = new DBChequeReturn();

                if (CBAmount > 0)
                {
                    bRetValue = dbpur.UpdateBankReceiptVoucherForChequeReturn(BKRID);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return bRetValue;
        }

        //public bool AddAccountDetailsIntbltrnacDebit()
        //{
        //    bool bRetValue = false;
        //    try
        //    {
        //        DBAccountDetails dbpur = new DBAccountDetails();

        //        if (CBAmount > 0)
        //        {
        //            bRetValue = dbpur.a AddDetailsForAccountsBankReceipt(Id, DetailId, CBAccountID, CBAmount, 0, CBBankAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, CreatedBy, CreatedDate, CreatedTime);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //    return bRetValue;
        //}

        //public bool AddAccountDetailsIntbltrnacCredit()
        //{
        //    bool bRetValue = false;
        //    try
        //    {
        //        DBAccountDetails dbpur = new DBAccountDetails();

        //        if (CBAmount > 0)
        //        {
        //            bRetValue = dbpur.AddDetailsForAccountsBankReceipt(Id, DetailId, CBBankAccountID, 0, CBAmount, CBAccountID, CBVouType, CBVouNo, CBVouDate, CBNarration, CBBankID, CBBranchID, CBChequeNumber, CBChequeDate, CreatedBy, CreatedDate, CreatedTime);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //    return bRetValue;
        //}


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
            DBChequeReturn CBtran = new DBChequeReturn();
            return CBtran.AddDetailsParticulars(IntID, DetailId, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount, DetailId, SerialNumber);
        }
        public bool UpdateSCCBill()
        {
            DBChequeReturn CBtran = new DBChequeReturn();
            return CBtran.UpdateDetailsSCCBill(Id, DSaleId, DVoucherSeries, DVoucherType, DVoucherNumber, DVoucherDate, DSubType, DBillAmount,
                DBalanceAmount, DClearedAmount, DDiscountAmount);
        }


        public bool DeleteDetails()
        {
            DBChequeReturn CBtran = new DBChequeReturn();
            return CBtran.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBChequeReturn CBtran = new DBChequeReturn();
            return CBtran.DeletePreviosRowsByID(Id);
        }

        #endregion




    }
}
