using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class SaleList : BaseObject
    {

        #region Declaration  

        #endregion

        #region Constructors, Destructors
        public SaleList()
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

        # region properties


        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();  
        }
     
        #endregion      

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.ReadProductDetailsByProductID();
        }

        public DataTable GetOverviewDataProfitPercentDay(string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataProfitPercentDay(fromdate, todate);
        }
        internal DataTable GetCreditNoteDataProfitPercentDay(string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetCreditNoteDataProfitPercentDay(fromdate, todate);
        }
        public DataTable GetOverviewDataProfitPercentParty(string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataProfitPercentParty(fromdate, todate);
        }

        public DataTable GetDataForVATRegister(string fromdate, string todate)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.ReadSaleDetailsForVATRegister(fromdate, todate);
        }
        public DataTable GetDataForVATRegister(string fromdate, string todate, string accountID)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.ReadSaleDetailsForVATRegister(fromdate, todate, accountID);
        }
        public DataTable GetDataForVATRegisterDATE(string fromdate, string todate)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.ReadSaleDetailsForVATRegisterDATE(fromdate, todate);
        }
        public DataTable GetDataForVATRegisterDATEALL(string fromdate, string todate)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.ReadSaleDetailsForVATRegisterDATEALL(fromdate, todate);
        }
        public DataTable GetDataForVATRegisterMONTH(string fromdate, string todate)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.ReadSaleDetailsForVATRegisterMONTH(fromdate, todate);
        }
        public DataTable GetDataForVATRegisterMONTHALL(string fromdate, string todate)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.ReadSaleDetailsForVATRegisterMONTHALL(fromdate, todate);
        }
        public DataTable GetDataForVATRegisterTIN(string fromdate, string todate)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.ReadSaleDetailsForVATRegisterTIN(fromdate, todate);
        }
        public DataTable GetDataForVATRegisterTINDetail(string fromdate, string todate)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.ReadSaleDetailsForVATRegisterTINDetail(fromdate, todate);
        }
        public DataTable GetOverviewDataForAllPartySummary(string fromdate, string todate)
        {
            DBSaleList dbsl = new DBSaleList();
            return dbsl.GetOverviewDataForAllPartySummary(fromdate, todate);
        }
        public DataTable GetSaleDataForGivenProduct(int ProductID)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForGivenProduct(ProductID);
        }
        public DataTable GetSaleDataForH1(string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForH1(fromdate, todate);
        }
        public DataTable GetSaleDataForGivenProductBatchMrp(int ProductID, string batch, double mrp, string fromDate, string toDate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForGivenProductBatchMrp(ProductID,batch,mrp,fromDate,toDate);
        }
        public DataTable GetSaleDataForScheduledProducts(string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForScheduledProducts(fromdate, todate);
        }

        public DataTable GetSaleDataForDailyProducts(string fromdate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForDailyProducts(fromdate);
        }
        public DataTable GetSaleDataCategory(string mfromdate, string mtodate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return     dbsalelist.GetSaleDataCategory(mfromdate, mtodate);
        }


        public DataTable GetSaleDataForPatient(string patient, string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForPatient(patient, fromdate, todate);
        }
        public DataTable GetSaleDataForPatient(string patient, string fromdate, string todate, int ProductID)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForPatient(patient, fromdate, todate,ProductID);
        }
        public DataTable GetSaleDataForDoctor(string doctorid,  string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForDoctor(doctorid, fromdate, todate);
        }
        public DataTable GetSaleDataForDoctorShowReport(string doctorid, string compID, string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForDoctorShowReport(doctorid, compID, fromdate, todate);
        }
        public DataTable GetSaleDataForDoctorCompanyProduct(string doctorid, string compid, string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForDoctor(doctorid,compid, fromdate, todate);
        }
        public DataTable GetOverviewDataForDailySaleReport(string fromdate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataForDailySaleReport(fromdate);
        }
        public DataTable GetDetailsaleTotalForDayWiseVoucherSaleReport()
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetDetailsaleTotalForDayWiseVoucherSaleReport();
        }
        public DataTable GetOverviewDataForDailySaleReportDistributor(string fromdate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataForDailySaleReportDistributor(fromdate);
        }
        public DataTable GetOverviewDataForPartywiseSaleReport(string accID, string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataForPartywiseSaleReport(accID, fromdate,todate);
        }
        public DataTable GetOverviewDataForPartywiseOutstandingSaleReport(string accID, string fromdate, string todate, string saleSubType)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataForPartywiseOutstandingSaleReport(accID, fromdate, todate, saleSubType);
        }
        public DataTable GetOverviewDataForAreawiseOutstandingSaleReport(string accID, string fromdate, string todate, string saleSubType)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataForAreawiseOutstandingSaleReport(accID, fromdate, todate, saleSubType);
        }
        public DataTable GetOverviewDataForPartywiseOutstandingSaleReport(string fromdate, string todate, string saleSubType)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataForPartywiseOutstandingSaleReport(fromdate, todate, saleSubType);
        }
        public DataTable GetOverviewDataForPartywiseSaleReportforPatient(string accID, string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataForPartywiseSaleReportforPatient(accID, fromdate, todate);
        }
        public DataTable GetOverviewDataForPartywiseOutstandingSaleReportforPatient(string accID, string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataForPartywiseOutstandingSaleReportforPatient(accID, fromdate, todate);
        }
        public DataTable GetOverviewDataForPartyProductListDebtor(string PartyID, int ProductID)
        {
            DBSaleList dbsl = new DBSaleList();
            return dbsl.GetOverviewDataForPartyProductListDebtor(PartyID, ProductID);
        }
        public DataTable GetOverviewDataForPartyProductListPatient(string PartyID, int ProductID)
        {
            DBSaleList dbsl = new DBSaleList();
            return dbsl.GetOverviewDataForPartyProductListPatient(PartyID, ProductID);
        }
        public DataTable GetOverviewDataForRegularPartyProductSale(string PartyID, int ProductID, string fromdate , string todate)
        {
            DBSaleList dbsl = new DBSaleList();
            return dbsl.GetOverviewDataForRegularPartyProductSale(PartyID, ProductID,fromdate,todate);
        }
        public DataTable GetOverviewDataForRegularPartyProductSalePatient(string PartyID, int ProductID, string fromdate , string todate)
        {
            DBSaleList dbsl = new DBSaleList();
            return dbsl.GetOverviewDataForRegularPartyProductSalePatient(PartyID, ProductID, fromdate, todate);
        }
        public DataTable GetSaleDataForRegister(string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.ReadSalesRegisterData(fromdate,todate);
        }
        public DataTable GetSaleDataForRegister(string fromdate, string todate, string mtype)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.ReadSalesRegisterData(fromdate, todate, mtype);
        }
        public DataTable GetRegularSaleOverviewData()
        {
            DBCounterSale dbCountSale = new DBCounterSale();
            return dbCountSale.GetRegularSaleOverviewData();
        }

        //public DataTable GetDebtorSaleOverviewData()
        //{
        //    DBCounterSale dbdebtor = new DBCounterSale();
        //    return dbdebtor.GetDebtorSaleOverviewData();
        //}
        public DataTable GetSaleDataDoctorCompanySummary(string doctorid, string mfromdate, string mtodate)
        {
            DBSaleList dbsl = new DBSaleList();
            return dbsl.GetSaleDataDoctorCompanySummary(doctorid,mfromdate, mtodate);
        }
        public DataTable GetSaleDataForDoctorCompanyDetail(string doctorid, string companyid, string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForDoctorCompanyDetail(doctorid, companyid, fromdate, todate);
        }

        public DataTable GetSaleDataForProductSummary(string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataProductSummary(fromdate, todate);
        }
        public DataTable FillScheduleH1ProductList()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetOverviewDataForScheduleH1();
        }
        public DataTable ReadProductDetailsByID()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = null;
                DBSSSale dbStock = new DBSSSale();
                dt = dbStock.ReadProductDetailsByIDDetailSale(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public int GetAndUpdateSaleNumber(string vtype, string voucherseries)
        {
            int vouno = 0;
            DBGetVouNumbers dbno = new DBGetVouNumbers();
            vouno = dbno.GetSale(vtype,voucherseries);
            return vouno;
        }

        #endregion     

        public bool DeletePreviousRecords()
        {
            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.DeleteDebtorSaleByMasterID(Id);
        }





        public DataTable GetDataForSummary(string _MFromDate, string _MToDate)
        {
            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.GetDataForSummary(_MFromDate, _MToDate);
        }
        public DataTable GetDataForSaleSummary(string _MFromDate, string _MToDate)
        {
            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.GetDataForSaleSummary(_MFromDate, _MToDate);
        }
        public DataTable GetDataForSummaryFromToNumber(string _MFromDate, string _MToDate, string mFromTime, string mToTime)
        {
            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.GetDataForSummaryFromToNumber(_MFromDate, _MToDate,mFromTime, mToTime);
        }

        public DataTable GetSaleDataFromToNumber(int mFromNumber, int mToNumber)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.GetSaleDataFromToNumber(mFromNumber, mToNumber);
        }

        public DataTable GetChangedBillForSelectedID(string selectedID)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.GetSaleDataFromToNumber(selectedID);
        }

        public DataTable GetDataForCashCounter(string toDate)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.GetDataForCashCounter(toDate);
        }

        public bool UpdateCashCounter(string saleID)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.UpdateCashCounter(saleID);
        }

        public DataTable GetNextVisitDays(string _MFromDate, string _MToDate)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.GetNextVisitDays(_MFromDate, _MToDate);
        }

        public DataTable GetStockForNextVisitDays(string _MFromDate, string _MToDate)
        {
            DBSaleList dbsale = new DBSaleList();
            return dbsale.GetStockForNextVisitDays(_MFromDate, _MToDate);
        }
    }
}
