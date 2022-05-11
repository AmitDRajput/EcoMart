using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class DailyPurchaseOrder : BaseObject
    {
        #region Declaration
        private string _FromDay;
        private string _EndDay;
        private string _FromDaySaleToday;
        private string _FromDayNextVisit;
        private string _EndDayNextVisit;
        private string _EndDaySaleToday;
        private string _DSLID;
        private string _DSLAccountID;
        private string _DSLAccountName;
        private string _DSLAddress1;
        private string _DSLAddress2;
        private string _DSLAccTelephone;  // [01.06.2017]
        private int _DSLQty;
        private double _DSLPurchaseRate;
        private string _DSLIfSave;
        private string _DSLIfDailyShortList;
        private int _DSLOrderNumber;
        private int _DSLFirstOrderNumber;
        private int _DSLLastOrderNumber;
        private double _DSLAmount;
        private int _CurrentOrderNumber;
        private string _DSLMasterID;
        private string _DSLVoucherType;
        private int _DayofWeek;
        private string _DSLProductID;
        private int _DSLSchemeQuantity;
        #endregion

        #region Constructors, Destructors
        public DailyPurchaseOrder()
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

        #region Properties
        public int DSLSchemeQuantity
        {
            get { return _DSLSchemeQuantity; }
            set { _DSLSchemeQuantity = value; }
        }
        public string DSLVoucherType
        {
            get { return _DSLVoucherType; }
            set { _DSLVoucherType = value; }
        }

        public string DSLMasterID
        {
            get { return _DSLMasterID; }
            set { _DSLMasterID = value; }
        }
        public double DSLPurchaseRate
        {
            get { return _DSLPurchaseRate; }
            set { _DSLPurchaseRate = value; }
        }
        public string FromDay
        {
            get { return _FromDay; }
            set { _FromDay = value; }
        }
        public string EndDay
        {
            get { return _EndDay; }
            set { _EndDay = value; }
        }

        public string FromDaySaleToday
        {
            get { return _FromDaySaleToday; }
            set { _FromDaySaleToday = value; }
        }

        public string EndDaySaleToday
        {
            get { return _EndDaySaleToday; }
            set { _EndDaySaleToday = value; }
        }
        public string FromDayNextVisit
        {
            get { return _FromDayNextVisit; }
            set { _FromDayNextVisit = value; }
        }

        public string EndDayNextVisit
        {
            get { return _EndDayNextVisit; }
            set { _EndDayNextVisit = value; }
        }
             
       
        public string DSLID
        {
            get { return _DSLID; }
            set { _DSLID = value; }
        }
        public string DSLProductID
        {
            get { return _DSLProductID; }
            set { _DSLProductID = value; }
        }
        public string DSLAccountID
        {
            get { return _DSLAccountID; }
            set { _DSLAccountID = value; }
        }
        public string DSLAccountName
        {
            get { return _DSLAccountName; }
            set { _DSLAccountName = value; }
        }
        public string DSLAddress1
        {
            get { return _DSLAddress1; }
            set { _DSLAddress1 = value; }
        }
        public string DSLAddress2
        {
            get { return _DSLAddress2; }
            set { _DSLAddress2 = value; }
        }
        public string DSLAccTelephone   // [01.06.2017]
        {
            get { return _DSLAccTelephone; }
            set { _DSLAccTelephone = value; }
        }
        public int DSLQty
        {
            get { return _DSLQty; }
            set { _DSLQty = value; }
        }
        public string DSLIFSave
        {
            get { return _DSLIfSave; }
            set { _DSLIfSave = value; }
        }
        public string DSLDailyShortList
        {
            get { return _DSLIfDailyShortList; }
            set { _DSLIfDailyShortList = value; }
        }
        public int DSLOrderNumber
        {
            get { return _DSLOrderNumber; }
            set { _DSLOrderNumber = value; }
        }
        public int DSLFirstOrderNumber
        {
            get { return _DSLFirstOrderNumber; }
            set { _DSLFirstOrderNumber = value; }
        }
        public int DSLLastOrderNumber
        {
            get { return _DSLLastOrderNumber; }
            set { _DSLLastOrderNumber = value; }
        }

        public double DSLAmount
        {
            get { return _DSLAmount; }
            set { _DSLAmount = value; }
        }
        public int CurrentOrderNumber
        {
            get { return _CurrentOrderNumber; }
            set { _CurrentOrderNumber = value; }
        }
        public int DayofWeek
        {
            get { return _DayofWeek; }
            set { _DayofWeek = value; }
        }
        #endregion
        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.GetOverviewData();
        }

        public DataTable ReadShortListByDate()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.ReadShotListByDate(FromDay, EndDay);
        }
        public DataTable ReadShortListByDateForToday()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.ReadShotListByDateForToday(FromDay, EndDay);
        }
        public DataTable ReadShortListByDateForTodayByAccountID()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.ReadShotListByDateForTodayByAccountID(FromDay, EndDay,DSLAccountID);
        }
        public DataTable ReadListForTodayALLTypes()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.ReadListForTodayALLTypes(FromDaySaleToday, EndDaySaleToday);
        }
        public DataTable ReadShotListByDateALLTypes()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.ReadShotListByDateALLTypes(FromDay, EndDay);
        }

        public DataTable ReadShotListByDateNextVisit()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.ReadShotListByDateNextVisit(FromDay, EndDay, FromDayNextVisit, EndDayNextVisit);
        }

        public DataTable ReadOrderByNumber()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.ReadOrderByNumber(DSLFirstOrderNumber);
        }

         public DataTable GetSummaryData()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.GetOverviewSummaryData(DSLFirstOrderNumber);
        }
        public DataTable GetDetailData(int ordernumber)
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.GetOverviewDetailData(ordernumber);
        }
        public bool SaveOrder()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.SaveOrder(DSLID, DSLAccountID, DSLQty, DSLIFSave, DSLDailyShortList,DSLSchemeQuantity);
        }
        public bool CreateOrder()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.CreateOrder(DSLID, DSLAccountID, DSLQty, DSLIFSave,DSLOrderNumber,EndDay,DSLDailyShortList,DSLPurchaseRate, DSLMasterID, DSLSchemeQuantity , CreatedBy,CreatedDate,CreatedTime);
          
        }

        public bool CreateOrderForToday()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            // return dpo.CreateOrder(DSLID, DSLAccountID, DSLQty, DSLIFSave,DSLOrderNumber,EndDay,DSLDailyShortList,DSLPurchaseRate, DSLMasterID, CreatedBy,CreatedDate,CreatedTime);
            return dpo.CreateOrderForToday(DSLID, DSLAccountID, DSLQty, DSLIFSave, DSLOrderNumber, EndDay, DSLDailyShortList, DSLPurchaseRate, DSLMasterID, DSLProductID, DSLSchemeQuantity, CreatedBy, CreatedDate, CreatedTime, netrate);
        }
        public bool AddDetails()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.AddDetails(DSLMasterID,General.ShopDetail.ShopVoucherSeries,DSLVoucherType, DSLOrderNumber, EndDay, DSLAccountID,  DSLAmount, CreatedBy, CreatedDate, CreatedTime);
        }
        #endregion Properties

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _FromDay = " ";
                _EndDay = " ";
                _FromDaySaleToday = "";
                _EndDaySaleToday = "";
                _DSLProductID = "";
                _DSLAccountID = null;
                _DSLAccountName = "";
                _DSLAddress1 = "";
                _DSLAddress2 = "";
                _DSLID = null;
                _DSLQty = 0;
                _DSLIfSave = "Y";
                _DSLIfDailyShortList = "Y";
                _DSLOrderNumber = 0;
                _DSLFirstOrderNumber = 0;
                _DSLLastOrderNumber = 0;
                _DSLAmount = 0;
                _CurrentOrderNumber = 0;
                _DSLPurchaseRate = 0;
                _DSLMasterID = "";
                _DSLVoucherType = FixAccounts.VoucherTypeForPurchaseOrder;
                _DayofWeek = 0;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Internal Methods

       
        public DataTable ReadLastOrderAllProducts(string accountID)
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.ReadShotListByDateForTodayByAccountID(accountID);
        }

        public DataTable ReadLastOrderRemainingProducts(string accountID)
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.ReadLastOrderRemainingProducts(accountID);
        }
        public DataTable ReadLastOrderRemainingProductsAllTypes()
        {
            DBDailyPurchaseOrder dpo = new DBDailyPurchaseOrder();
            return dpo.ReadLastOrderRemainingProductsAllTypes();
        }

    }
}
