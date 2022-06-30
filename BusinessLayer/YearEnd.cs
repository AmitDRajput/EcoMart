using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;


namespace EcoMart.BusinessLayer
{
    public class YearEnd : BaseObject
    {

        #region Constructors, Destructors
        public YearEnd()
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
        #endregion Constructors, Destructors

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
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

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public bool CreateNewBase(string currentdatabase, string newdatabase)
        {
            bool retValue = false;

            try
            {
                DBYearEnd dby = new DBYearEnd();
                retValue = dby.CreateNewBase(currentdatabase, newdatabase);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public bool CreateTable(string currentdatabase, string newdatabase, string tablename)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.CreateTable(currentdatabase, newdatabase, tablename);
            return retValue;
        }



        public bool RemovePreviousYeartblaccouningYear(string voucherseries)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.RemovePreviousYeartblaccouningYear(voucherseries);
            return retValue;
        }

        public bool AddRowForCurrentAccountingYear(string newvoucherseries, string newsyear, string neweyear)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.IfCurrentYearExists(newvoucherseries);
            if (!retValue)
                retValue = dby.AddRowForCurrentAccountingYear(newvoucherseries, newsyear, neweyear);
            return retValue;
        }


        public bool RemovePreviousYeartblVoucherNumbers(string voucherseries , string newDataBase , string newVoucherSeries, string currentDataBase)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.RemovePreviousYeartblVoucherNumbers(newVoucherSeries, newDataBase);
            retValue = dby.RemovePreviousYeartblSettings(voucherseries,currentDataBase, newVoucherSeries);
            return retValue;
        }

        public bool AddRowForCurrentAccountingYeartblVoucherNumbers(string newvoucherseries, string newsyear, string neweyear, string currentDataBase, string voucherseries)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            //retValue = dby.IfCurrentYearExists(newvoucherseries);
            retValue = dby.AddRowForCurrentAccountingYeartblVoucherNumbers(newvoucherseries, newsyear, neweyear,currentDataBase,voucherseries);
            return retValue;
        }

        public bool DefinePrimaryKeys(string tablename, string primarykey)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.DefinePrimaryKeys(tablename, primarykey);
            return retValue;
        }

        public bool DefineUniqueKeys(string tablename, string primarykey)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.DefineUniqueKeys(tablename, primarykey);
            return retValue;
        }
        #endregion


        public void DeletePreviousYearFromtblAccountingYearandtblvouchernumbers(string accountingyear)
        {
            DBYearEnd dbye = new DBYearEnd();
            dbye.DeletePreviousYearFromtblAccountingYearandtblvouchernumbers(accountingyear);
        }        
        public bool ClearStockIntblStockAndMasterProductForYearEnd()
        {
            bool retValue = false;
            try
            {
                DBYearEnd dbye = new DBYearEnd();
                retValue = dbye.ClearStockIntblStockAndMasterProductForYearEnd();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public DataTable GetOpeningStockForCurrentYear(string _MToDate)
        {
            DataTable dt = null;
            try
            {
                DBYearEnd dbye = new DBYearEnd();
                dt = dbye.GetOpeningStockForCurrentYear(_MToDate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public void UpdateOpeningStock(string mstockid, int mqtyin, int mscmqtyin)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.UpdateOpeningStock(mstockid, mqtyin, mscmqtyin);
           
        }
        public DataTable GetSaleStockForCurrentYearForYearEnd(string _MToDate)
        {
            DataTable dt = null;
            try
            {
                DBYearEnd dbye = new DBYearEnd();
                dt = dbye.GetSaleStockForCurrentYearForYearEnd(_MToDate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }
        public void UpdateSaleStock(string mstockid, int mqtyin, int mscmqtyin)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.UpdateSaleStock(mstockid, mqtyin, mscmqtyin);

        }
        public DataTable GetPurchaseStockForCurrentYearForYearEnd(string _MToDate)
        {
            DataTable dt = null;
            try
            {
                DBYearEnd dbye = new DBYearEnd();
                dt = dbye.GetPurchaseStockForCurrentYearForYearEnd(_MToDate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public void UpdatePurchaseStock(string mstockid, int mqtyin, int mscmqtyin)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.UpdatePurchaseStock(mstockid, mqtyin, mscmqtyin);

        }
        public DataTable GetCreditNoteStockInForCurrentYearForYearEnd(string _MToDate)
        {
            DataTable dt = null;
            try
            {
                DBYearEnd dbye = new DBYearEnd();
                dt = dbye.GetCreditNoteStockINStockForCurrentYearForYearEnd(_MToDate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public void UpdateCreditNoteStockINStock(string mstockid, int mqtyin, int mscmqtyin)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.UpdateCreditNoteStockINStock(mstockid, mqtyin, mscmqtyin);

        }


        public DataTable GetDebitNoteStockOUTForCurrentYearForYearEnd(string _MToDate)
        {
            DataTable dt = null;
            try
            {
                DBYearEnd dbye = new DBYearEnd();
                dt = dbye.GetDebitNoteStockOUTStockForCurrentYearForYearEnd(_MToDate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public void UpdateDebitNoteStockOUTStock(string mstockid, int mqtyin, int mscmqtyin)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.UpdateDebitNoteStockOUTStock(mstockid, mqtyin, mscmqtyin);

        }

        public DataTable GetCorrectionInRateForYearEnd(string _MToDate)
        {
            DataTable dt = null;
            try
            {
                DBYearEnd dbye = new DBYearEnd();
                dt = dbye.GetCorrectionInRateStockForCurrentYearForYearEnd(_MToDate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public void UpdateCorrectionInRateStock(string oldstockid, int oldqty, string newstockid, int newqty)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.UpdateCorrectionInRateStockStock(oldstockid, oldqty, newstockid,newqty);

        }

        public void CalculateOpeningStock(string _MToDate)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.CalculateOpeningStock(_MToDate); 
        }
        public void DeleteFromVouchers(string _MToDate)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.DeleteFromVouchers(_MToDate); 
        }
        public void DeleteFromChangedDeletedDetails()
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.DeleteFromChangedDeletedDetails(); 
        }

        public void SelectFromSale(string _MToDate)
        {
          
            DBYearEnd dby = new DBYearEnd();
            bool retValue =   dby.SelectFromSale(_MToDate);            
        }       

        //public void DeleteFromtblTrnacdetailpurchaseorder(string _MToDate)
        //{
        //    bool retValue = false;
        //    DBYearEnd dby = new DBYearEnd();
        //    retValue = dby.DeleteFromtblTrnac(_MToDate); 
        //}

        public void DeleteFromDetails(string _MToDate)
        {
            bool retValue = false;
            DBYearEnd dby = new DBYearEnd();
            retValue = dby.DeleteFromDetails(_MToDate); 
        }

        public void SelectFromPurchase(string _MToDate)
        {
           
            DBYearEnd dby = new DBYearEnd();
            bool retValue = dby.SelectFromPurchase(_MToDate);
            
        }

        public void SelectFromStatement(string _MToDate)
        {
            DBYearEnd dby = new DBYearEnd();
            bool retValue =   dby.SelectFromStatement(_MToDate);
        }
        public void DeleteForSalePurchaseAndStatement(string _MToDate)
        {
            DBYearEnd dby = new DBYearEnd();
            bool retValue =  dby.DeleteForSalePurchaseAndStatement(_MToDate);
        }

        public void GetClearedAmountinMasterAccount(string _MToDate)
        {
            DataTable dt = new DataTable();
            DBYearEnd dby = new DBYearEnd();
            dt =  dby.GetClearedAmountinMasterAccount(_MToDate);
            foreach (DataRow dr in dt.Rows)
            {
                string maccountID = dr["AccountID"].ToString();
                double mclearamt = Convert.ToDouble(dr["ClearAmount"].ToString());
                bool retValue =  dby.UpdateMasterClearedAmount(maccountID, mclearamt);
            }
        }

        public void DeleteFromNewDataBase(string _MToDate, string voucherSeries)
        {
            DBYearEnd dby = new DBYearEnd();
            bool retValue = dby.DeleteFromNewDataBase(_MToDate,voucherSeries);
        }
    }
}
