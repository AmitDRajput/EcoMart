using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class FinalAccounts : BaseObject
    {

        #region Declaration

        private double _MPOpeningDebit = 0;
        private double _MPOpeningCredit = 0;
        private double _MPTrDebit = 0;
        private double _MPTrCredit = 0;
        private double _MPClosingDebit = 0;
        private double _MPClosingCredit = 0;
        private double _MPDebit = 0;
        private double _MPCredit = 0;

        #endregion Declaration

        #region Constructors, Destructors
        public FinalAccounts()
        {
            try
            {
                Initialise();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _MPClosingCredit = 0;
                _MPClosingDebit = 0;
                _MPCredit = 0;
                _MPDebit = 0;
                _MPOpeningCredit = 0;
                _MPOpeningDebit = 0;
                _MPTrCredit = 0;
                _MPTrDebit = 0;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        #region Properties
        public double MPOpeningDebit
        {
            get { return _MPOpeningDebit; }
            set { _MPOpeningDebit = value; }
        }
        public double MPOpeningCredit
        {
            get { return _MPOpeningCredit; }
            set { _MPOpeningCredit = value; }
        }
        public double MPTrDebit
        {
            get { return _MPTrDebit; }
            set { _MPTrDebit = value; }
        }
        public double MPTrCredit
        {
            get { return _MPTrCredit; }
            set { _MPTrCredit = value; }
        }
        public double MPClosingDebit
        {
            get { return _MPClosingDebit; }
            set { _MPClosingDebit = value; }
        }
        public double MPClosingCredit
        {
            get { return _MPClosingCredit; }
            set { _MPClosingCredit = value; }
        }
        public double MPDebit
        {
            get { return _MPDebit; }
            set { _MPDebit = value; }
        }
        public double MPCredit
        {
            get { return _MPCredit; }
            set { _MPCredit = value; }
        }

        #endregion Properties

        #region Reports

        public DataTable GetTrialBalanceOPDBCRFromMaster()
        {
            DataTable dt = new DataTable();
            try
            {
                DBAccountDetails ac = new DBAccountDetails();
                dt = ac.GetTrialBalanceOPDBCRFromMaster();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }
        public DataTable GetTrialBalanceOPDBCRFromTransaction(string fromDate)
        {
            DataTable dt = new DataTable();
            try
            {
                DBAccountDetails ac = new DBAccountDetails();
                dt = ac.GetTrialBalanceOPDBCRFromTransaction(fromDate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }
        public DataTable GetTrialBalanceTRDBCRFromTransaction(string fromDate, string toDate)
        {
            DataTable dt = new DataTable();
            try
            {
                DBAccountDetails ac = new DBAccountDetails();
                dt = ac.GetTrialBalanceTRDBCRFromTransaction(fromDate, toDate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }
        public void UpdatebalancesInMasterAccount(DataTable _TransactionOP, DataTable _TransactionSource)
        {
            bool retValue = false;
            string accountID = string.Empty;
            double debitsum = 0;
            double creditsum = 0;
            DBAccountDetails ac = new DBAccountDetails();
            foreach (DataRow dr in _TransactionOP.Rows)
            {
                accountID = "";
                debitsum = 0;
                creditsum = 0;

                if (dr["AccountID"] != DBNull.Value && dr["AccountID"].ToString() != string.Empty)
                    accountID = dr["AccountID"].ToString();
                if (dr["Debit"] != DBNull.Value && dr["Debit"].ToString() != string.Empty)
                    debitsum = Convert.ToDouble(dr["Debit"].ToString());
                if (dr["Credit"] != DBNull.Value && dr["Credit"].ToString() != string.Empty)
                    creditsum = Convert.ToDouble(dr["Credit"].ToString());
                if (accountID != string.Empty)
                    retValue = ac.UpdateTransactionOP(accountID, debitsum, creditsum);
            }

            foreach (DataRow dr in _TransactionSource.Rows)
            {
                accountID = "";
                debitsum = 0;
                creditsum = 0;

                if (dr["AccountID"] != DBNull.Value && dr["AccountID"].ToString() != string.Empty)
                    accountID = dr["AccountID"].ToString();
                if (dr["Debit"] != DBNull.Value && dr["Debit"].ToString() != string.Empty)
                    debitsum = Convert.ToDouble(dr["Debit"].ToString());
                if (dr["Credit"] != DBNull.Value && dr["Credit"].ToString() != string.Empty)
                    creditsum = Convert.ToDouble(dr["Credit"].ToString());
                if (accountID != string.Empty)
                    retValue = ac.UpdateTransactionTR(accountID, debitsum, creditsum);
            }


        }

        public void InitializeDBCRFieldsInMasterAccount()
        {
            bool retValue = false;
            DBAccountDetails ac = new DBAccountDetails();
            retValue = ac.InitializeDBCRFieldsInMasterAccount();
        }

        public void CalculateClosingBalanceInmasterAccount()
        {
            bool retValue = false;
            DBAccountDetails ac = new DBAccountDetails();
            retValue = ac.CalculateClosingBalanceInmasterAccount();
        }

        public DataTable GetDetailsFromMasterAccount()
        {
            DataTable dt = new DataTable();
            try
            {
                DBAccountDetails ac = new DBAccountDetails();
                dt = ac.GetDetailsFromMasterAccount();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public DataTable GetGroupWiseTotals()
        {
            string mgroupID = "";
            string mparentID = "";
            double mopeningdb = 0;
            double mopeningcr = 0;
            double mtransactiondb = 0;
            double mtransactioncr = 0;
            double mclosingdb = 0;
            double mclosingcr = 0;
            DataTable dt = new DataTable();
            DataTable dtt = new DataTable();
            DBAccountDetails ac = new DBAccountDetails();
            try
            {
                //dt = ac.GetTotalsinmasterAccount();
                dt = ac.UpdatebalancesInMasterGroup();
                foreach (DataRow dr in dt.Rows)
                {
                    mgroupID = "";
                    mopeningdb = 0;
                    mopeningcr = 0;
                    mtransactiondb = 0;
                    mtransactioncr = 0;
                    mclosingdb = 0;
                    mclosingcr = 0;
                    if (dr["AccGroupID"] != DBNull.Value && dr["AccGroupID"].ToString() != string.Empty && dr["AccGroupID"].ToString() != " ")
                    {
                        mgroupID = dr["AccGroupID"].ToString();
                        mopeningdb = Convert.ToDouble(dr["opdebit"].ToString()) + Convert.ToDouble(dr["optrdebit"].ToString());
                        mopeningcr = Convert.ToDouble(dr["opcredit"].ToString()) + Convert.ToDouble(dr["optrcredit"].ToString());
                        mtransactiondb = Convert.ToDouble(dr["trdebit"].ToString());
                        mtransactioncr = Convert.ToDouble(dr["trcredit"].ToString());
                        mclosingdb = Convert.ToDouble(dr["cldebit"].ToString());
                        mclosingcr = Convert.ToDouble(dr["clcredit"].ToString());
                        ac.UpdateMasterGroup(mgroupID, mopeningdb, mopeningcr, mtransactiondb, mtransactioncr, mclosingdb, mclosingcr);
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

         //   dtt = ac.Gettotalsinmastergroup();
            dtt = ac.GetTotalsForLevel3();

            try
            {
                foreach (DataRow dr in dtt.Rows)
                {
                    mgroupID = "";
                    mparentID = "";
                    mopeningdb = 0;
                    mopeningcr = 0;
                    mtransactiondb = 0;
                    mtransactioncr = 0;
                    mclosingdb = 0;
                    mclosingcr = 0;


                    if (dr["UnderGroupID"] != DBNull.Value && dr["UnderGroupID"].ToString() != string.Empty && dr["UnderGroupID"].ToString() != " ")
                    {
                        mgroupID = dr["UnderGroupID"].ToString();
                        if (dr["UnderGroupIDParentID"] != DBNull.Value && dr["UnderGroupIDParentID"].ToString() != string.Empty && dr["UnderGroupIDParentID"].ToString() != " ")
                            mparentID = dr["UnderGroupIDParentID"].ToString();
                        if (Convert.ToInt32(mgroupID) > 20 && Convert.ToInt32(mparentID) > 20)
                        {
                            mopeningdb = Convert.ToDouble(dr["opdebit"].ToString());
                            mopeningcr = Convert.ToDouble(dr["opcredit"].ToString());
                            mtransactiondb = Convert.ToDouble(dr["trdebit"].ToString());
                            mtransactioncr = Convert.ToDouble(dr["trcredit"].ToString());
                            mclosingdb = Convert.ToDouble(dr["cldebit"].ToString());
                            mclosingcr = Convert.ToDouble(dr["clcredit"].ToString());
                            ac.UpdateMasterGrouplevel2(mgroupID, mopeningdb, mopeningcr, mtransactiondb, mtransactioncr, mclosingdb, mclosingcr);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }



            dtt = ac.GetTotalsForLevel2();

            try
            {
                foreach (DataRow dr in dtt.Rows)
                {
                    mgroupID = "";
                    mparentID = "";
                    mopeningdb = 0;
                    mopeningcr = 0;
                    mtransactiondb = 0;
                    mtransactioncr = 0;
                    mclosingdb = 0;
                    mclosingcr = 0;
                    if (dr["UnderGroupID"] != DBNull.Value && dr["UnderGroupID"].ToString() != string.Empty && dr["UnderGroupID"].ToString() != " ")
                    {
                        mgroupID = dr["UnderGroupID"].ToString();
                        if (dr["UnderGroupIDParentID"] != DBNull.Value && dr["UnderGroupIDParentID"].ToString() != string.Empty && dr["UnderGroupIDParentID"].ToString() != " ")
                            mparentID = dr["UnderGroupIDParentID"].ToString();
                        if (Convert.ToInt32(mgroupID) > 20 && Convert.ToInt32(mparentID) <= 20)
                        {
                            mopeningdb = Convert.ToDouble(dr["opdebit"].ToString());
                            mopeningcr = Convert.ToDouble(dr["opcredit"].ToString());
                            mtransactiondb = Convert.ToDouble(dr["trdebit"].ToString());
                            mtransactioncr = Convert.ToDouble(dr["trcredit"].ToString());
                            mclosingdb = Convert.ToDouble(dr["cldebit"].ToString());
                            mclosingcr = Convert.ToDouble(dr["clcredit"].ToString());
                            ac.UpdateMasterGrouplevel2(mgroupID, mopeningdb, mopeningcr, mtransactiondb, mtransactioncr, mclosingdb, mclosingcr);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }



            dt = ac.GetOverviewGroupMaster();

            return dt;
        }


        public DataTable GetGroupsUnderLevel2(int GrpID2)
        {
            DataTable dt = new DataTable();
            DBAccountDetails ac = new DBAccountDetails();
            return ac.GetGroupsUnderLevel2(GrpID2);

        }
        public DataTable GetTrnacTotalsByVoucherTypeNo()
        {
            DataTable dt = new DataTable();
            DBAccountDetails ac = new DBAccountDetails();
            return ac.GetTrnacTotalsByVoucherTypeNo();
        }

        #endregion Reports



    }
}
