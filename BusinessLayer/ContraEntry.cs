using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class ContraEntry : BaseObject
    {
         #region Declaration       
        private string _CBAccountId;
        private string _CBAccountIDTobeCredited;
        private string _CBName;
        private string _CBAddress1;
        private string _CBAddress2;
        private string _CBNarration;
        private string _CBVouType;
        private int _CBVouNo;
        private string _CBVouSeries;
        private string _CBVouDate;     
        private double _CBAmount;
        private bool _DuplicateAccount;       
        # endregion

        #region Constructors
        public ContraEntry()
        {
            Initialise();
        }
        #endregion

        # region properties

      
       
       
        public bool DuplicateAccount
        {
            get { return _DuplicateAccount; }
            set { _DuplicateAccount = value; }
        }

        public string CBAccountID
        {
            get { return _CBAccountId; }
            set { _CBAccountId = value; }
        }

        public string CBAccountIDTobeCredited
        {
            get { return _CBAccountIDTobeCredited; }
            set { _CBAccountIDTobeCredited = value; }
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

        public string CBVouSeries
        {
            get { return _CBVouSeries; }
            set { _CBVouSeries = value; }
        }

        public double CBAmount
        {
            get { return _CBAmount; }
            set { _CBAmount = value; }
        }

        #endregion

        #region Internal Methods

        public override void Initialise()
        {
            base.Initialise();      
            _CBName = "";
            _CBAccountId = "";
            _CBAccountIDTobeCredited = "";
            _CBAddress1 = "";
            _CBAddress2 = "";
            _CBNarration = "";
            _CBVouType = FixAccounts.VoucherTypeForContraEntry;
            _CBVouNo = 0;
            _CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            _CBAmount = 0;
            _CBVouDate = "";

            _DuplicateAccount = false;
           
                  
        }
        public override void DoValidate()
        {
            try
            {
                if (CBAccountID == "")
                    ValidationMessages.Add("Please enter the Account Name.");
                if (CBAccountIDTobeCredited == "")
                    ValidationMessages.Add("Please enter Account Tobe Credited");
                if (CBAccountID == CBAccountIDTobeCredited)
                    ValidationMessages.Add("Transaction In Same Account Not Allowed");
                if (CBAmount <= 0)
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
        public DataTable GetOverviewData(string CEType)
        {
            DBContraEntry CBtran = new DBContraEntry();      
            return CBtran.GetOverviewData(CEType);
        }
        public DataTable GetOverviewDataForReport(string CEType , string fromDate, string toDate)
        {
            DBContraEntry CBtran = new DBContraEntry();
            return CBtran.GetOverviewDataForReport(CEType, fromDate, toDate);
        }
        public int GetAndUpdateCPENumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetCashExpenses(voucherseries);
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
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return vouno;
        }
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBContraEntry CBtran = new DBContraEntry();
                drow = CBtran.ReadDetailsByID(Id);

                if (drow != null)
                {
                    CBAccountID = drow["AccountId"].ToString();
                    CBName = drow["AccName"].ToString();
                    CBAddress1 = drow["AccAddress1"].ToString();
                    CBAddress2 = drow["AccAddress2"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CBVouDate = drow["VoucherDate"].ToString();
                    CBAccountIDTobeCredited = drow["ChequeDepositedBankID"].ToString();
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public DataRow ReadDetailsByVouNumber()
        {
           
            DataRow drow = null;
            try
            {
                DBContraEntry CBtran = new DBContraEntry();
                drow = CBtran.ReadDetailsByVouNumber(CBVouNo);

                if (drow != null)
                {
                    CBAccountID = drow["AccountId"].ToString();
                    CBName = drow["AccName"].ToString();
                    CBAddress1 = drow["AccAddress1"].ToString();
                    CBAddress2 = drow["AccAddress2"].ToString();
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CBVouDate = drow["VoucherDate"].ToString();

                   
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return drow;
        }


        public DataTable ReadBillDetailsByID(string ID)
        {
            DBContraEntry CBtran = new DBContraEntry();
            return CBtran.GetExpensesDetailsByID(ID);
        }



        public bool AddDetails()
        {
            DBContraEntry CBtran = new DBContraEntry();
            return CBtran.AddDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount,CBAccountIDTobeCredited, CreatedBy, CreatedDate, CreatedTime);
        }
         
        public bool AddVoucherIntblTrnac()
        {
            DBContraEntry CBtran = new DBContraEntry();
            return CBtran.AddVoucherIntblTrnac(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount,DetailId,CBAccountIDTobeCredited, CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddVoucherIntblTrnacReverse()
        {
            DBContraEntry CBtran = new DBContraEntry();
            return CBtran.AddVoucherIntblTrnacReverse(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount, DetailId,CBAccountIDTobeCredited, CreatedBy, CreatedDate, CreatedTime);
        }
      
        public bool UpdateDetails()
        {
            DBContraEntry CBtran = new DBContraEntry();
            return CBtran.UpdateDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate,
                CBAmount,CBAccountIDTobeCredited, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBContraEntry CBtran = new DBContraEntry();
            return CBtran.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBContraEntry CBtran = new DBContraEntry();
            return CBtran.DeletePreviosRowsByID(Id);
        }


        #endregion

        public void GetLastRecord()
        {
            DataRow dr;
            try
            {
                DBContraEntry dbs = new DBContraEntry();
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
                DBContraEntry dbs = new DBContraEntry();
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
                DBContraEntry dbs = new DBContraEntry();
                dr = dbs.GetFirstRecord(CBVouType, CBVouSeries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }

    }
}
