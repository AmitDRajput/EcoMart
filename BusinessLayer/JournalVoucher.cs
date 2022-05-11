using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;
using EcoMart.InterfaceLayer;
using EcoMart.InterfaceLayer.Classes;
using EcoMart.InterfaceLayer.CommonControls;


namespace EcoMart.BusinessLayer
{
    public class JournalVoucher : BaseObject
    {
        #region Declaration    
        private string _ID;
        private string _JVAccountID;
        private string _JVVoucherID;
        private string _JVName;
        private string _JVAddress;
        private double _JVCredit;
        private double _JVDebit;
        private string _JVNarration;
        private string _JVVouType;
        private string _JVVouDate;
        private int _JVVouNo;
        private string _JVVouSeries;
        private int _JVNoOfRows;
        private bool _DuplicateAccount;
        private string _RAccountID;
        private double _JVAmountClear;
        private double _JVAmountBalance;
        private string _JVReferenceVoucherID;
        private string _JVOperatorID;
        private string _CreatedUserID;
     //   private string _CreatedDate;
    //    private string _CreatedTime;
        private string _ModifiedUserID;
     //   private string _ModifiedDate;
    //    private string _ModifiedTime;
        private string _ModifiedOperatorID;
     //   private string _TransacID;
        private string _FirstDBAccID;
        private string _FirstCRAccID;
        private string _AccAccountID;
        private string _ModifyEdit;
        #endregion

        #region Properties
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string JVAccountID
        {
            get { return _JVAccountID; }
            set { _JVAccountID = value; }
        }

        public string JVVoucherID
        {
            get { return _JVVoucherID; }
            set { _JVVoucherID = value; }
        }

        public string JVVouType
        {
            get { return _JVVouType; }
            set { _JVVouType = value; }
        }
        public string JVVouSeries
        {
            get { return _JVVouSeries; }
            set { _JVVouSeries = value; }
        }
        public int JVVouNo
        {
            get { return _JVVouNo; }
            set { _JVVouNo = value; }
        }
        public string JVVouDate
        {
            get { return _JVVouDate; }
            set { _JVVouDate = value; }
        }


        public double JVDebit
        {
            get { return _JVDebit; }
            set { _JVDebit = value; }
        }
        public double JVCredit
        {
            get { return _JVCredit; }
            set { _JVCredit = value; }
        }
        public double JVAmountClear
        {
            get { return _JVAmountClear; }
            set { _JVAmountClear = value; }
        }
        public double JVAmountBalance
        {
            get { return _JVAmountBalance; }
            set { _JVAmountBalance = value; }
        }
        public string JVNarration
        {
            get { return _JVNarration; }
            set { _JVNarration = value; }
        }
        public string JVReferenceVoucherID
        {
            get { return _JVReferenceVoucherID; }
            set { _JVReferenceVoucherID = value; }
        }
        public string JVOperatorID
        {
            get { return _JVOperatorID; }
            set { _JVOperatorID = value; }
        }
        public string CreatedUserID
        {
            get { return _CreatedUserID; }
            set { _CreatedUserID = value; }
        }
        //public string CreatedDate
        //{
        //    get { return _CreatedDate; }
        //    set { _CreatedDate = value; }
        //}
        //public string CreatedTime
        //{
        //    get { return _CreatedTime; }
        //    set { _CreatedTime = value; }
        //}
        public string ModifiedUserID
        {
            get { return _ModifiedUserID; }
            set { _ModifiedUserID = value; }
        }
        //public string ModifiedDate
        //{
        //    get { return _ModifiedDate; }
        //    set { _ModifiedDate = value; }
        //}
        //public string ModifiedTime
        //{
        //    get { return _ModifiedTime; }
        //    set { _ModifiedTime = value; }
        //}
        public string ModifiedOperatorID
        {
            get { return _ModifiedOperatorID; }
            set { _ModifiedOperatorID = value; }
        }
        public string JVName
        {
            get { return _JVName; }
            set { _JVName = value; }
        }
        public string JVAddress
        {
            get { return _JVAddress; }
            set { _JVAddress = value; }
        }
        public int JVNoOfRows
        {
            get { return _JVNoOfRows; }
            set { _JVNoOfRows = value; }
        }

        public bool DuplicateAccount
        {
            get { return _DuplicateAccount; }
            set { _DuplicateAccount = value; }
        }

        public string RAccountID
        {
            get { return _RAccountID; }
            set { _RAccountID = value; }
        }

        public string TransacID
        {
            get { return _RAccountID; }
            set { _RAccountID = value; }
        }
        public string FirstDBAccID
        {
            get { return _FirstDBAccID; }
            set { _FirstDBAccID = value; }
        }
        public string FirstCRAccID
        {
            get { return _FirstCRAccID; }
            set { _FirstCRAccID = value; }
        }
        public string AccAccountID
        {
            get { return _AccAccountID; }
            set { _AccAccountID = value; }
        }
        public string ModifyEdit
        {
            get { return _ModifyEdit; }
            set { _ModifyEdit = value; }
        }
        #endregion

        #region Constructors, Destructors
        public JournalVoucher()
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

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _JVName = "";
                _JVAccountID = "";
                _JVVoucherID = "";
                _JVAddress = "";
                _JVNarration = "";
                _JVVouType = FixAccounts.VoucherTypeForJournalVoucher;
                _JVVouNo = 0;
                _JVVouSeries = General.ShopDetail.ShopVoucherSeries;
                _JVNoOfRows = 0;
                _JVVouDate = "";
                _DuplicateAccount = false;
                _RAccountID = null;
                _JVCredit = 0;
                _JVDebit = 0;
                _CreatedUserID = "";
              //  _CreatedTime = "";
              //  _CreatedDate = "";
           //     _ModifiedDate = "";
                _ModifiedOperatorID = "";
           //     _ModifiedTime = "";
                _ModifiedUserID = "";
                _FirstDBAccID = "";
                _FirstCRAccID = "";
                _AccAccountID = "";
                _ModifyEdit = "";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override void DoValidate()
        {
            try
            {
                //if (Name == null || Name == "")
                //    ValidationMessages.Add("Please enter the Area Name.");

                //DBArea dbval = new DBArea();
                //if (IFEdit == "Y")
                //{
                //    if (dbval.IsNameUniqueForEdit(Name, Id))
                //    {
                //        ValidationMessages.Add("Name Already Exists.");
                //    }
                //}
                //else
                //{
                //    if (dbval.IsNameUniqueForAdd(Name, Id))
                //    {
                //        ValidationMessages.Add("Name Already Exists.");
                //    }
                //}
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override bool CanBeDeleted()
        {
            bool bRetValue = false;
            try
            {
                int _rowcount = 0;
                DBDelete dbdelete = new DBDelete();
                _rowcount = dbdelete.GetOverviewDataSelect("masteraccount", "AccAreaID", Id);
                if (_rowcount == 0)
                {
                    bRetValue = true;
                    _rowcount = dbdelete.GetOverviewDataSelect("vouchersale", "AreaID", Id);
                    if (_rowcount == 0)
                        bRetValue = true;
                    else
                        bRetValue = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return bRetValue;
        }
        #endregion Internal Methods

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBJournalVoucher dbj = new DBJournalVoucher();
            return dbj.GetOverviewData();
        }

        public DataTable GetOverviewJVData(string JVType, string fromDate, string toDate)
        {
            DBJournalVoucher dbj = new DBJournalVoucher();
            return dbj.GetOverviewJVData(JVType, fromDate, toDate);
        }
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBArea dbArea = new DBArea();
                drow = dbArea.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["AreaId"].ToString();
                    Name = Convert.ToString(drow["AreaName"]);
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public bool ReadDetailsByJVID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBJournalVoucher dJV = new DBJournalVoucher();
                drow = dJV.ReadDetailsByJVID(ID);
                if (drow != null)
                {
                    JVVoucherID = drow["VoucherID"].ToString();
                    JVVouType = Convert.ToString(drow["VoucherType"]);
                    JVVouSeries = drow["VoucherSeries"].ToString();
                    JVVouNo = Convert.ToInt32(drow["VoucherNumber"]);
                    JVNarration = drow["Narration"].ToString();
                    JVVouDate = drow["VoucherDate"].ToString();
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public DataTable ReadDetailsByVoucherID()
        {
            DataTable dt = null;
            try
            {
                DBJournalVoucher dJV = new DBJournalVoucher();
                dt = dJV.ReadDetailsByVoucherID(JVVoucherID);
                return dt;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                return null;
            }
        }

        public bool AddDetails()
        {
            DBJournalVoucher DBJNV = new DBJournalVoucher();
            return DBJNV.AddDetails(ID, JVAccountID, JVVoucherID, JVVouType, JVVouSeries, JVVouNo, JVVouDate, SerialNumber,JVDebit, JVCredit, JVAmountClear, JVAmountBalance,
                JVNarration, JVReferenceVoucherID, JVOperatorID, CreatedUserID, CreatedDate, CreatedTime, ModifiedUserID, ModifiedDate, ModifiedTime, ModifiedOperatorID);
        }

        public bool AddJVDetailsIntblTrnac()
        {
            DBCashExpenses CBtran = new DBCashExpenses();
            return CBtran.AddJVDetailsIntblTrnac(TransacID, JVVouSeries, JVVoucherID, JVAccountID, JVDebit, JVCredit, AccAccountID, JVVouDate, string.Empty, JVVouType, string.Empty, JVVouNo,
               JVVouDate, JVNarration, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, CreatedDate, CreatedTime, CreatedUserID, ModifiedDate, ModifiedTime, ModifiedUserID);
        }

        //public bool UpdateJVDetailsIntblTrnac()
        //{
        //    DBCashExpenses CBtran = new DBCashExpenses();
        //    return CBtran.UpdateJVDetailsIntblTrnac(TransacID, JVVouSeries, JVVoucherID, JVAccountID, JVDebit, JVCredit, AccAccountID, JVVouDate, string.Empty, JVVouType, string.Empty, JVVouNo,
        //       JVVouDate, JVNarration, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, CreatedDate, CreatedTime, CreatedUserID, ModifiedDate, ModifiedTime, ModifiedUserID);
        //}

        //public bool UpdateDetails()
        //{
        //    //DBArea dbArea = new DBArea();
        //    //return dbArea.UpdateDetails(Id, Name, ModifiedBy, ModifiedDate, ModifiedTime);
        //    DBJournalVoucher DBJNV = new DBJournalVoucher();
        //    return DBJNV.UpdateDetails(ID, JVAccountID, JVVoucherID, JVVouType, JVVouSeries, JVVouNo, JVVouDate, SerialNumber, JVDebit, JVCredit, JVAmountClear, JVAmountBalance,
        //        JVNarration, JVReferenceVoucherID, JVOperatorID, CreatedUserID, CreatedDate, CreatedTime, ModifiedUserID, ModifiedDate, ModifiedTime, ModifiedOperatorID);
        //}

        public bool DeleteDetails()
        {
            DBArea dbArea = new DBArea();
            return dbArea.DeleteDetails(Id);
        }

        public bool DeleteJVDetails()
        {
            DBJournalVoucher DBJNV = new DBJournalVoucher();
            return DBJNV.DeleteJVDetails(JVVoucherID);
        }
        public bool DeleteJVFormtblTrans()
        {
            DBJournalVoucher DBJNV = new DBJournalVoucher();
            return DBJNV.DeleteJVFormtblTrans(JVVoucherID);
        }


        public DataTable GetAreaList()
        {
            DBArea dbData = new DBArea();
            return dbData.GetOverviewData();
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

        public DataTable ReadJVDetailsByID(string ID)
        {
            DBJournalVoucher DBJV = new DBJournalVoucher();
            return DBJV.ReadJVDetailsByID(ID);
        }

        #endregion Public Methods
    }
}

