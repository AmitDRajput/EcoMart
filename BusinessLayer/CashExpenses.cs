using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class CashExpenses : BaseObject
    {

        #region Declaration       
        private string _CBAccountId;
        private string _CBName;
        private string _CBAddress1;
        private string _CBAddress2;
        private string _CBNarration;
        private string _CBVouType;
        private string _CBVouSeries;
        private int _CBVouNo;
        private string _CBVouDate;
        private int _CBNoOfRows;
        private double _CBAmount;
        private bool _DuplicateAccount;
        private string _RAccountID;
        private double _RDebit;
        private double _RCredit;
        private string _JVID;
        private int _JVNo;
        private int _CBJVIDpay;
        #endregion

        #region Constructors
        public CashExpenses()
        {
            Initialise();
        }
        #endregion

        # region properties

        public int JVNo
        {
            get { return _JVNo; }
            set { _JVNo = value; }
        }
        public int CBJVIDpay
        {
            get { return _CBJVIDpay;}
            set { _CBJVIDpay = value; }
        }

        public string JVID
        {
            get { return _JVID; }
            set { _JVID = value; }
        }
        public string RAccountID
        {
            get { return _RAccountID; }
            set { _RAccountID = value; }
        }
        public double Rdebit
        {
            get { return _RDebit; }
            set { _RDebit = value; }
        }
        public double Rcredit
        {
            get { return _RCredit; }
            set { _RCredit = value; }
        }
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

        #endregion

        #region Internal Methods

        public override void Initialise()
        {
            base.Initialise();      
            _CBName = "";
            _CBAccountId = "";
            _CBAddress1 = "";
            _CBAddress2 = "";
            _CBNarration = "";
            _CBVouType = FixAccounts.VoucherTypeForCashExpenses;
            _CBVouNo = 0;
            _CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            _CBNoOfRows = 0;
            _CBAmount = 0;
            _CBVouDate = "";

            _DuplicateAccount = false;
            _RAccountID = null;
            _RCredit = 0;
            _RDebit = 0;
            _JVID = "";
            _JVNo = 0;
        }
        public override void DoValidate()
        {
            try
            {
                if (CBAccountID == "")
                    ValidationMessages.Add("Please enter the Account Name.");
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
            DBCashExpenses CBtran = new DBCashExpenses();        
            return CBtran.GetOverviewData(CEType);
        }
        public DataTable GetOverviewDataForReport(string CEType , string fromDate, string toDate)
        {
            DBCashExpenses CBtran = new DBCashExpenses();
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
                DBCashExpenses CBtran = new DBCashExpenses();
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
                DBCashExpenses CBtran = new DBCashExpenses();
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

                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }


        public DataTable ReadBillDetailsByID(string ID)
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.GetExpensesDetailsByID(ID);
        }



        public int AddDetails()
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.AddDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount, CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddParticularsDetails(int id,int acid,double mdebit,double mcredit, string detailid)
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.AddDetailsP(id, acid, mdebit, mcredit,detailid);
        }
        public int AddDetailsInmaterJV(string detailId, string accountID, string VouType,int VouNo, string VouSeries, string VouDate,double mdebit, double mcredit, string Refno,string Narration, string CreatedBy,string CreatedDate,string CreatedTime)
        
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.AddDetailsInmaterJV(detailId,accountID, VouType, VouNo,VouSeries, VouDate, mdebit, mcredit, Refno, Narration, CreatedBy, CreatedDate, CreatedTime);
        }
    
        public bool AddVoucherIntblTrnac()
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.AddVoucherIntblTrnac(IntID, CBAccountIDINT, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount,DetailId, CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddVoucherIntblTrnacReverse()
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.AddVoucherIntblTrnacReverse(IntID, CBAccountIDINT, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount, DetailId,CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddJVIntblTrnac(string id, string acid, double mdebit, double mcredit, string detailid,string AccountID, string VouDate, string VouType,string Refno,string Narration, int VouNo, string CreatedBy,string CreatedDate,string CreatedTime)
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.AddJVIntblTrnac(id, acid, mdebit, mcredit, detailid,AccountID,VouDate,VouType,Refno,Narration,VouNo,CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddJVIntblTrnacReverse(string id, string acid, double mdebit, double mcredit, string detailid, string AccountID, string VouDate, string VouType, string Refno, string Narration, int VouNo, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.AddJVIntblTrnacReverse(id, acid, mdebit, mcredit, detailid, AccountID, VouDate, VouType, Refno,Narration,VouNo,CreatedBy, CreatedDate, CreatedTime);
        }
        public bool UpdateDetails()
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.UpdateDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate,
                CBAmount, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.DeletePreviosRowsByID(Id);
        }


        #endregion

        public void GetLastRecord()
        {
            DataRow dr;
            try
            {
                DBCashReceipt dbs = new DBCashReceipt();
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
                DBCashReceipt dbs = new DBCashReceipt();
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
                DBCashReceipt dbs = new DBCashReceipt();
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
