using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
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
        public DataTable GetOverviewDataForAllPartySummary(string fromdate, string todate)
        {
            DBSaleList dbsl = new DBSaleList();
            return dbsl.GetOverviewDataForAllPartySummary(fromdate, todate);
        }
        public DataTable GetSaleDataForGivenProduct(string productid)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForGivenProduct(productid);
        }
        public DataTable GetSaleDataForH1(string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForH1(fromdate, todate);
        }
        public DataTable GetSaleDataForGivenProductBatchMrp(string productid, string batch, double mrp, string fromDate, string toDate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForGivenProductBatchMrp(productid,batch,mrp,fromDate,toDate);
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

        public DataTable GetSaleDataForDoctor(string doctorid,  string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetSaleDataForDoctor(doctorid, fromdate, todate);
        }

        public DataTable GetOverviewDataForDailySaleReport(string fromdate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataForDailySaleReport(fromdate);
        }
        public DataTable GetOverviewDataForPartywiseSaleReport(string accID, string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataForPartywiseSaleReport(accID, fromdate,todate);
        }
        public DataTable GetOverviewDataForPartywiseSaleReportforPatient(string accID, string fromdate, string todate)
        {
            DBSaleList dbsalelist = new DBSaleList();
            return dbsalelist.GetOverviewDataForPartywiseSaleReportforPatient(accID, fromdate, todate);
        }
        public DataTable GetOverviewDataForPartyProductListDebtor(string PartyID, string ProductID)
        {
            DBSaleList dbsl = new DBSaleList();
            return dbsl.GetOverviewDataForPartyProductListDebtor(PartyID, ProductID);
        }
        public DataTable GetOverviewDataForPartyProductListPatient(string PartyID, string ProductID)
        {
            DBSaleList dbsl = new DBSaleList();
            return dbsl.GetOverviewDataForPartyProductListPatient(PartyID, ProductID);
        }
        public DataTable GetOverviewDataForRegularPartyProductSale(string PartyID, string ProductID, string fromdate , string todate)
        {
            DBSaleList dbsl = new DBSaleList();
            return dbsl.GetOverviewDataForRegularPartyProductSale(PartyID, ProductID,fromdate,todate);
        }
        public DataTable GetOverviewDataForRegularPartyProductSalePatient(string PartyID, string ProductID, string fromdate , string todate)
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

        public DataTable GetDebtorSaleOverviewData()
        {
            DBCounterSale dbdebtor = new DBCounterSale();
            return dbdebtor.GetDebtorSaleOverviewData();
        }
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



       
    }
}
