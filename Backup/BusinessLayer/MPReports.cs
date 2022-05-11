using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
     public class MPReports : BaseObject
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
         public MPReports()
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

          # region Reports  

          public DataTable GetGeneralLedger(string fromDate, string toDate)
          {
              DataTable dt = new DataTable();
              try
              {
                  DBAccountDetails ac = new DBAccountDetails();
                  dt = ac.GetGeneralLedger(fromDate,toDate);
              }
              catch (Exception Ex)
              {
                  Log.WriteException(Ex);
              }        
              return dt;
          }


         
          //public DataTable GetTrialBalanceLevel4()
          //{
          //    DataTable dt = new DataTable();
          //    try
          //    {
          //        DBAccountDetails ac = new DBAccountDetails();
          //        dt = ac.GetTrialBalanceLevel4();
          //    }
          //    catch (Exception Ex)
          //    {
          //        Log.WriteException(Ex);
          //    }
          //    return dt; 
          //}
         
          //public DataTable GetUnion(DataTable op, DataTable optr, DataTable trtr)
          //{
          //    DataTable dt = new DataTable();
          //    try
          //    {
          //        DBAccountDetails ac = new DBAccountDetails();
          //        dt = ac.GetUnion(op, optr, trtr);
          //    }
          //    catch (Exception Ex)
          //    {
          //        Log.WriteException(Ex);
          //    }
          //    return dt;
          //}

          //public DataTable GetTrialBalanceTransactions(string fromDate, string toDate)
          //{
          //    DataTable dt = new DataTable();
          //    try
          //    {
          //        DBAccountDetails ac = new DBAccountDetails();
          //        dt = ac.GetTrialBalanceTransactions(fromDate, toDate);
          //    }
          //    catch (Exception Ex)
          //    {
          //        Log.WriteException(Ex);
          //    }
          //    return dt;
          //}

          public DataTable GetDayTotalForDailyClosing(string fromDate, string toDate,string accountID)
          {
              DataTable dt = new DataTable();
              try
              {
                  DBAccountDetails ac = new DBAccountDetails();
                  dt = ac.GetDayTotalForDailyClosing(fromDate, toDate,accountID);
              }
              catch (Exception Ex)
              {
                  Log.WriteException(Ex);
              }
              return dt;
          }
          public DataTable GetGeneralLedgerByClearedDate(string fromDate, string toDate, string accountID)
          {
              DataTable dt = new DataTable();
              try
              {
                  DBAccountDetails ac = new DBAccountDetails();
                  dt = ac.GetGeneralLedgerByClearedDate(fromDate, toDate, accountID);
              }
              catch (Exception Ex)
              {
                  Log.WriteException(Ex);
              }
              return dt;
          }
          public DataTable GetGeneralLedger(string fromDate, string toDate,string accountID)
          {
              DataTable dt = new DataTable();
              try
              {
                  DBAccountDetails ac = new DBAccountDetails();
                  dt = ac.GetGeneralLedger(fromDate, toDate, accountID);
              }
              catch (Exception Ex)
              {
                  Log.WriteException(Ex);
              }
              return dt;
          }
          public DataTable GetSundryDebtors(string todate)
          {
              DataTable dt = new DataTable();
              try
              {
                  DBAccountDetails ac = new DBAccountDetails();
                  dt = ac.GetSundryDebtors(todate);
              }
              catch (Exception Ex)
              {
                  Log.WriteException(Ex);
              }        
              return dt;
          }

          public DataTable GetSundryCreditors(string todate)
          {
              DataTable dt = new DataTable();
              try
              {
                  DBAccountDetails ac = new DBAccountDetails();
                  dt = ac.GetSundryCreditors(todate);
              }
              catch (Exception Ex)
              {
                  Log.WriteException(Ex);
              }        
              return dt;
          }
          #endregion


          //public void GetTransactionData(string AccID)
          //{
          //    DataTable dt = new DataTable();
              
          //    try
          //    {
          //        DBAccountDetails ac = new DBAccountDetails();
          //        dt = ac.GetSundryCreditors(todate);
          //    }
          //    catch (Exception Ex)
          //    {
          //        Log.WriteException(Ex);
          //    }
          //    return dt;  
          //}

       
        
         //public DataTable GetGroupsUnderLevel3(int GrpID3)
         //{
         //    DataTable dt = new DataTable();
         //    DBAccountDetails ac = new DBAccountDetails();
         //    return ac.GetGroupsUnderLevel3(GrpID3);

         //}

        
     }
}
