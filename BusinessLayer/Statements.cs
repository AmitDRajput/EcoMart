using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;


namespace EcoMart.BusinessLayer
{
    public class Statements : BaseObject
    {
        #region Declaration 

        private string _AccountID;
        private int _StatementNumber;
        private double _StatementAmount;
        private string _FromDate;
        private string _Todate;
        private string _StatementDate;
        private double _Vat5Percent;
        private double _Vat12point5Percent;
        private string _VoucherType;
        private int _LastStatementNumber;
        private int _NumberofBills;
        private double _AmountClear;
        private int _LastStatementNumberinTable;

        #endregion Declaration

        #region Constructors, Destructors
        public Statements()
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

        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }
        public int StatementNumber
        {
            get { return _StatementNumber; }
            set { _StatementNumber = value; }
        }
        public int LastStatementNumber
        {
            get { return _LastStatementNumber; }
            set { _LastStatementNumber = value; }
        }

        public int LastStatementNumberinTable
        {
            get { return _LastStatementNumberinTable; }
            set { _LastStatementNumberinTable = value; }
        }

        public double StatementAmount
        {
            get { return _StatementAmount; }
            set { _StatementAmount = value; }
        }
        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public string ToDate
        {
            get { return _Todate; }
            set { _Todate = value; }
        }
        public string StatementDate
        {
            get { return _StatementDate; }
            set { _StatementDate = value; }
        }
        public double Vat5Percent
        {
            get { return _Vat5Percent; }
            set { _Vat5Percent = value; }
        }
        public double Vat12point5Percent
        {
            get { return _Vat12point5Percent; }
            set { _Vat12point5Percent = value; }
        }
        public string VoucherType
        {
            get { return _VoucherType; }
            set { _VoucherType = value; }
        }
        public int NumberofBills
        {
            get { return _NumberofBills; }
            set { _NumberofBills = value; }
        }
        public double AmountClear
        {
            get { return _AmountClear; }
            set { _AmountClear = value; }
        }
        #endregion Properties

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();

                _AccountID = "";
                _StatementNumber = 0;
                _StatementAmount = 0;
                _FromDate = "";
                _Todate = "";
                _StatementDate = "";
                _Vat5Percent = 0;
                _Vat12point5Percent = 0;
                _VoucherType = "";
                _AmountClear = 0;
                _LastStatementNumberinTable = 0;


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
                if (StatementAmount == 0)
                    ValidationMessages.Add("Amount Zero.");
                if (AccountID == null || AccountID == "")
                    ValidationMessages.Add("Account Not Selected");
               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }
        public override bool CanBeDeleted()
        {
            bool bRetValue = false;

            if (AmountClear == 0)
                bRetValue = true;
            else
                bRetValue = false;

            return bRetValue;
        }

        #endregion Internal Methods

        #region Public Methods

        public bool ReadDetailsByID(string iD)
        {
            bool retValue = false;
          
            try
            {
                DataRow drow = null;
                DBStatements dbStmt = new DBStatements();
                drow = dbStmt.ReadDetailsByID(iD);
                if (drow != null)
                {
                    if (drow["ID"] != DBNull.Value)
                        Id = drow["ID"].ToString();
                    if (drow["VoucherDate"] != DBNull.Value)
                        StatementDate = drow["VoucherDate"].ToString();
                    if (drow["FromDate"] != DBNull.Value)
                        FromDate = drow["FromDate"].ToString();
                    if (drow["ToDate"] != DBNull.Value)
                        ToDate = drow["ToDate"].ToString();
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = drow["AccountID"].ToString();
                    if (drow["AmountClear"] != DBNull.Value)
                        AmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    if (drow["AmountNet"] != DBNull.Value)
                        StatementAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["NumberOfBills"] != DBNull.Value)
                        NumberofBills = Convert.ToInt32(drow["NumberOfBills"].ToString());
                    StatementNumber = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    retValue = true;
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public DataTable GetOverviewDataPurchase15Days(string fromDate, string toDate)
        {
            DBStatements dbstmt = new DBStatements();
            return dbstmt.GetOverviewDataPurchase15Days(fromDate, toDate);
        }
        public DataTable GetOverviewDataSale(string fromDate, string toDate)
        {
            DBStatements dbstmt = new DBStatements();
            return dbstmt.GetOverviewDataSale(fromDate, toDate);
        }
        public DataTable GetOverviewDataPurchase15DaysForView(string fromDate, string toDate)
        {
            DBStatements dbstmt = new DBStatements();
            return dbstmt.GetOverviewDataPurchase15DaysForView(fromDate, toDate);
        }
        public DataTable GetOverviewDataSaleForView(string fromDate, string toDate)
        {
            DBStatements dbstmt = new DBStatements();
            return dbstmt.GetOverviewDataSaleForView(fromDate, toDate);
        }
        public DataTable GetOverviewDataBothStatementForView(string voutype, string fromDate, string toDate)
        {
            DBStatements dbstmt = new DBStatements();
            return dbstmt.GetOverviewDataBothStatementForView(voutype, fromDate, toDate);
        }

        public DataTable GetOverviewDataByType(string vouType)
        {
            DBStatements dbstmt = new DBStatements();
            return dbstmt.GetOverviewDataByType(vouType);
        }

        public DataTable GetOverviewDataSaleStatementForView(string fromDate, string toDate)
        {
            DBStatements dbstmt = new DBStatements();
            return dbstmt.GetOverviewDataSaleStatementForView(fromDate, toDate);
        }
        public bool CheckCanbeDeletedPurchaseStatement(int laststatementnumber)
        {
            bool retValue = true;

            DBStatements dbstmt = new DBStatements();
            retValue = dbstmt.CheckForLastNumberPurchase(laststatementnumber);
            return retValue;
        }
        public bool CheckCanbeDeletedSaleStatement(int laststatementnumber)
        {
            bool retValue = true;

            DBStatements dbstmt = new DBStatements();
            retValue = dbstmt.CheckForLastNumberSale(laststatementnumber);
            return retValue;
        }
        public int GetAndUpdatePurchaseStatementNumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetPurchaseStatement(voucherseries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return vouno;
        }

        public void GetLastVoucherNumberFortblVoucherNumbersPurchase()
        {
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                _LastStatementNumberinTable = dbno.GetLastVoucherNumberFortblVoucherNumbersPurchase(General.ShopDetail.ShopVoucherSeries);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public int GetAndUpdateSaleStatementNumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetSaleStatement(voucherseries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return vouno;
        }

        public int GetPurchaseStatementToView(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetPurchaseStatementToView(voucherseries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return vouno;
        }


        public int GetSaleStatementToView(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetSaleStatementToView(voucherseries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return vouno;
        }

        public void GetDetailsFromMaster(int statementNumber, string voucherType)
        {
            DataRow drow;
            try
            {
                DBStatements dbStmt = new DBStatements();
                drow = dbStmt.GetDetailsFromMaster(statementNumber, voucherType);
                if (drow != null)
                {
                    if (drow["ID"] != DBNull.Value)
                        Id = drow["ID"].ToString();
                    if (drow["VoucherDate"] != DBNull.Value)
                        StatementDate = drow["VoucherDate"].ToString();
                    if (drow["FromDate"] != DBNull.Value)
                        FromDate = drow["FromDate"].ToString();
                    if (drow["ToDate"] != DBNull.Value)
                       ToDate = drow["ToDate"].ToString();
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = drow["AccountID"].ToString();
                    if (drow["AmountClear"] != DBNull.Value)
                        AmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    if (drow["AmountNet"] != DBNull.Value)
                         StatementAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["NumberOfBills"] != DBNull.Value)
                         NumberofBills = Convert.ToInt32(drow["NumberOfBills"].ToString());
                }
                
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void GetDetailsByID(string ID)
        {
            DataRow drow;
            try
            {
                DBStatements dbStmt = new DBStatements();
                drow = dbStmt.GetDetailsByID(ID);
                if (drow != null)
                {
                    StatementNumber = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    if (drow["ID"] != DBNull.Value)
                        Id = drow["ID"].ToString();
                    if (drow["VoucherDate"] != DBNull.Value)
                        StatementDate = drow["VoucherDate"].ToString();
                    if (drow["FromDate"] != DBNull.Value)
                        FromDate = drow["FromDate"].ToString();
                    if (drow["ToDate"] != DBNull.Value)
                        ToDate = drow["ToDate"].ToString();
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = drow["AccountID"].ToString();
                    if (drow["AmountClear"] != DBNull.Value)
                        AmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    if (drow["AmountNet"] != DBNull.Value)
                        StatementAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["NumberOfBills"] != DBNull.Value)
                        NumberofBills = Convert.ToInt32(drow["NumberOfBills"].ToString());
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        public bool AddDetailsPurchase()
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.AddDetails(Id,StatementNumber,AccountID, StatementAmount,NumberofBills,FromDate,ToDate,Vat5Percent,Vat12point5Percent,General.ShopDetail.ShopVoucherSeries,FixAccounts.VoucherTypeForStatementPurchase, CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddDetailsSale()
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.AddDetails(Id, StatementNumber, AccountID, StatementAmount,NumberofBills, FromDate, ToDate, Vat5Percent, Vat12point5Percent, General.ShopDetail.ShopVoucherSeries, FixAccounts.VoucherTypeForStatementSale , CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddStatementNumberInPurchaseVoucher(string purchaseID, int statmentNumber, string statementID)
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.AddStatementNumberInPurchaseVoucher(purchaseID,StatementNumber,statementID);
        }
        public bool AddStatementNumberInSaleVoucher()
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.AddStatementNumberInSaleVoucher(Id, StatementNumber);
        }
        public bool AddStatementNumberInSaleVoucher(string SaleID, int statmentNumber, string statementID)
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.AddStatementNumberInSaleVoucher(SaleID, StatementNumber, statementID);
        }
        public bool UpdateLastStatementNumberInTblVoucherNumbersPurchase()
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.UpdateLastStatementNumberInTblVoucherNumbersPurchase(_LastStatementNumber, General.ShopDetail.ShopVoucherSeries);
        }

        public bool UpdateLastStatementNumberInTblVoucherNumbersSale()
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.UpdateLastStatementNumberInTblVoucherNumbersSale(_LastStatementNumber, General.ShopDetail.ShopVoucherSeries);
        }
        public bool DeleteStatementsPurchase(int fromno, int tono, string voutype, string vouseries)
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.DeleteStatementsPurchase(fromno, tono,voutype,vouseries);
        }

        public bool DeleteStatementsPurchase(string statementID)
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.DeleteStatementsPurchase(statementID);
        }
        public bool DeleteStatementsSale(int fromno, int tono, string voutype, string vouseries)
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.DeleteStatementsSale(fromno, tono, voutype, vouseries);
        }
        public bool DeleteStatementsSale(string statementID)
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.DeleteStatementsSale(statementID);
        }
        #endregion Public Methods

        public bool RemoveStatementNumbersFrommasterPurchase(int fromno, int tono , string vouseries)
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.RemoveStatementNumbersFrommasterPurchase(fromno, tono, vouseries);  
        }

        public bool RemoveStatementNumbersFrommasterPurchase(int statementNumber)
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.RemoveStatementNumbersFrommasterPurchase(statementNumber,General.ShopDetail.ShopVoucherSeries);
        }

        public bool RemoveStatementNumbersFrommasterSale(int fromno, int tono, string vouseries)
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.RemoveStatementNumbersFrommasterSale(fromno, tono, vouseries);
        }
        public bool RemoveStatementNumbersFrommasterSale(int statementNumber)
        {
            DBStatements dbStmt = new DBStatements();
            return dbStmt.RemoveStatementNumbersFrommasterSale(statementNumber, General.ShopDetail.ShopVoucherSeries);
        }
        public bool AddAccountDetailsDebit()
        {
            bool bRetValue = true;
            DBStatements dbs = new DBStatements();
            try
            {
                DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                bRetValue = dbs.AddAccountDetails(Id, DetailId, AccountID  ,StatementAmount ,0,FixAccounts.AccountCreditPurchase.ToString(), VoucherType,0,CreatedDate ," ", CreatedBy, CreatedDate, CreatedTime);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return bRetValue;
        }

        public bool AddAccountDetailsCredit()
        {
            bool bRetValue = true;
            DBStatements dbs = new DBStatements();
            try
            {
                DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                bRetValue = dbs.AddAccountDetails(Id, DetailId, FixAccounts.AccountCreditPurchase.ToString(), 0,StatementAmount , AccountID, VoucherType, 0, CreatedDate, " ", CreatedBy, CreatedDate, CreatedTime);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return bRetValue;
        }



    }
}
