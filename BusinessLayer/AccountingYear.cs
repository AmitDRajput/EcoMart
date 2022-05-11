using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class AccountingYear : BaseObject
    {
        #region Declaration
        private string _VoucherSeries;
        private string _FromDate;
        private string _ToDate;
        private string _YearEndOver;
        #endregion Declaration

        #region Constructor
        public AccountingYear()
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
        #endregion Constructor

        #region Properties
        public string VoucherSeries
        {
            get { return _VoucherSeries; }
            set { _VoucherSeries = value; }
        }
        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        public string YearEndOver
        {
            get { return _YearEndOver; }
            set { _YearEndOver = value; }
        }
        #endregion Properties

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
        #endregion Internal Methods

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBAccountingYear DBAccountingYear = new DBAccountingYear();
            return DBAccountingYear.GetOverviewData();
        }

        public bool ReadDetailsByVoucherSeries()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBAccountingYear DBAccountingYear = new DBAccountingYear();
                drow = DBAccountingYear.ReadDetailsByVoucherSeries(VoucherSeries);

                if (drow != null)
                {
                    VoucherSeries = Convert.ToString(drow["VoucherSeries"]);
                    FromDate = Convert.ToString(drow["FromDate"]);
                    ToDate = Convert.ToString(drow["ToDate"]);
                    YearEndOver = Convert.ToString(drow["YearEndOver"]);
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public void AddCurrentYear(string currentYear)
        {
            VoucherSeries = currentYear;
            FromDate = "20" + currentYear.Substring(0, 2) + "0401";
            ToDate = "20" + currentYear.Substring(2) + "0331";
            YearEndOver = "N";
            AddDetails();
            //AddtblSettings();
            AddRowIntblVoucherNumber();
        }
        public void UpdateCurrentYearColumn()
        {
            DBAccountingYear DBAccountingYear = new DBAccountingYear();
            DBAccountingYear.UpdateCurrentYearColumn();
        }
        public bool AddDetails()
        {
            DBAccountingYear DBAccountingYear = new DBAccountingYear();
            return DBAccountingYear.AddDetails(VoucherSeries, FromDate, ToDate,YearEndOver);
        }
        public bool AddRowIntblVoucherNumber()
        {
            DBAccountingYear DBAccountingYear = new DBAccountingYear();
            return DBAccountingYear.AddRowIntblVoucherNumber(VoucherSeries);
        }
        public bool DeleteDetails()
        {
            DBAccountingYear DBAccountingYear = new DBAccountingYear();
            return DBAccountingYear.DeleteDetails(VoucherSeries);
        }

        public bool GetYearEndStatus()
        {
            DBAccountingYear DBAccountingYear = new DBAccountingYear();
            bool retValue = DBAccountingYear.GetYearEndStatus();
            return retValue;
        }

        #endregion Public Methods
    }
}
