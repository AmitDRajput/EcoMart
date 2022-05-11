using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;
using System.Threading.Tasks;

namespace EcoMart.BusinessLayer
{
    public class Product : BaseObject
    {
        #region Declaration

        private int _ProdCompID;
        private string _ProdCompShortName;
        private int _ProdLoosePack;
        private int _ProdPreLoosePack;
        private string _ProdPack;
        private string _ProdPackType;
        private int _ProdPackID;
        private int _ProdPackTypeID;
        private double _ProdVATPer;
        private double _ProdCST;
        private int _ProdMinLevel;
        private int _ProxMaxLevel;
        private int _ProdOpeningStock;
        private int _ProdClosingStock;
        private int _ProdBoxQty;
        private int _ProdGenericID;
        private int _ProdShelfID;
        private string _ProdShelfCode;
        private int _ProdProductCategoryID;
        private int _ProdCreditor1ID;
        private int _ProdCreditor2ID;
        private string _ProdGrade;
        private string _ProdScheduleDrugCode;
        private string _ProdScheduleDrugName;
        private string _ProdIfShortList;
        private string _ProdIfSaleDisc;
        private string _ProdIfAddOctroi;
        private string _ProdIfPurchaseIncl;
        private string _ProdIfMRPIncl;
        private string _ProdIfScheduledDrug;
        private string _ProdIfBarCodeRequired;
        private string _ScannedBarcode;
        private string _IfNewPack;
        private string _IfNewPackType;
        private int _ProdLinkDrugId;
        private string _ProdRequireColdStorage;
        private string _ProdCanChangeLoosePack;
        private decimal _ProdlastPurchaseRate;
        private double _ProdNumberForBarcode; // [10.02.2017]
        private Int32 _HSNNumber;
        #endregion

        #region Constructors, Destructors
        public Product()
        {

        }
        #endregion

        #region Properties    
        
        public Int32 HSNNumber
        {
            get { return _HSNNumber; }
            set { _HSNNumber = value; }
        }    
        public int ProdCompID
        {
            get { return _ProdCompID; }
            set { _ProdCompID = value; }
        }
        public string ProdCompShortName
        {
            get { return _ProdCompShortName; }
            set { _ProdCompShortName = value; }
        }
        public int ProdLoosePack
        {
            get { return _ProdLoosePack; }
            set { _ProdLoosePack = value; }
        }
        public int ProdPreLoosePack
        {
            get { return _ProdPreLoosePack; }
            set { _ProdPreLoosePack = value; }
        }
        public string ProdPack
        {
            get { return _ProdPack; }
            set { _ProdPack = value; }
        }
        public string ProdPackType
        {
            get { return _ProdPackType; }
            set { _ProdPackType = value; }
        }
        public int ProdPackID
        {
            get { return _ProdPackID; }
            set { _ProdPackID = value; }
        }

        public int ProdPackTypeID
        {
            get { return _ProdPackTypeID; }
            set { _ProdPackTypeID = value; }
        }
        public string IfNewPack
        {
            get { return _IfNewPack; }
            set { _IfNewPack = value; }
        }
        public string IfNewPackType
        {
            get { return _IfNewPackType; }
            set { _IfNewPackType = value; }
        }
        public double ProdVATPer
        {
            get { return _ProdVATPer; }
            set { _ProdVATPer = value; }
        }
        public double ProdCST
        {
            get { return _ProdCST; }
            set { _ProdCST = value; }

        }
        public int ProdMinLevel
        {
            get { return _ProdMinLevel; }
            set { _ProdMinLevel = value; }
        }
        public int ProdMaxLevel
        {
            get { return _ProxMaxLevel; }
            set { _ProxMaxLevel = value; }
        }

        public int ProdOpeningStock
        {
            get { return _ProdOpeningStock; }
            set { _ProdOpeningStock = value; }
        }
        public int ProdClosingStock
        {
            get { return _ProdClosingStock; }
            set { _ProdClosingStock = value; }
        }
        public int ProdBoxQty
        {
            get { return _ProdBoxQty; }
            set { _ProdBoxQty = value; }
        }
        public Int32  ProdGenericID
        {
            get { return _ProdGenericID; }
            set { _ProdGenericID = value; }
        }

        public Int32  ProdShelfID
        {
            get { return _ProdShelfID; }
            set { _ProdShelfID = value; }
        }
        public string ProdShelfCode
        {
            get { return _ProdShelfCode; }
            set { _ProdShelfCode = value; }
        }
        public int ProdProductCategoryID
        {
            get { return _ProdProductCategoryID; }
            set { _ProdProductCategoryID = value; }
        }
        public int ProdCreditor1ID
        {
            get { return _ProdCreditor1ID; }
            set { _ProdCreditor1ID = value; }
        }
        public int ProdCreditor2ID
        {
            get { return _ProdCreditor2ID; }
            set { _ProdCreditor2ID = value; }
        }
        public string ProdGrade
        {
            get { return _ProdGrade; }
            set { _ProdGrade = value; }
        }

        public string ProdScheduleDrugCode
        {
            get { return _ProdScheduleDrugCode; }
            set { _ProdScheduleDrugCode = value; }
        }
        public string ProdScheduleDrugName
        {
            get { return _ProdScheduleDrugName; }
            set { _ProdScheduleDrugName = value; }
        }
        public string ProdIfShortList
        {
            get { return _ProdIfShortList; }
            set { _ProdIfShortList = value; }
        }
        public string ProdIfSaleDisc
        {
            get { return _ProdIfSaleDisc; }
            set { _ProdIfSaleDisc = value; }
        }
        public string ProdIfAddOctroi
        {
            get { return _ProdIfAddOctroi; }
            set { _ProdIfAddOctroi = value; }
        }
        public string ProdIfPurchaseIncl
        {
            get { return _ProdIfPurchaseIncl; }
            set { _ProdIfPurchaseIncl = value; }
        }
        public string ProdIfMRPIncl
        {
            get { return _ProdIfMRPIncl; }
            set { _ProdIfMRPIncl = value; }
        }

        public string ProdIfScheduledDrug
        {
            get { return _ProdIfScheduledDrug; }
            set { _ProdIfScheduledDrug = value; }
        }

        public int ProdLinkDrugId
        {
            get { return _ProdLinkDrugId; }
            set { _ProdLinkDrugId = value; }
        }
        public string ProdRequireColdStorage
        {
            get { return _ProdRequireColdStorage; }
            set { _ProdRequireColdStorage = value; }
        }
        public string ProdIfBarCodeRequired
        {
            get { return _ProdIfBarCodeRequired; }
            set { _ProdIfBarCodeRequired = value; }
        }
        public string ScannedBarcode
        {
            get { return _ScannedBarcode; }
            set { _ScannedBarcode = value; }
        }
        public string ProdCanChangeLoosePack
        {
            get { return _ProdCanChangeLoosePack; }
            set { _ProdCanChangeLoosePack = value; }
        }
        public decimal ProdlastPurchaseRate
        {
            get { return _ProdlastPurchaseRate; }
            set { _ProdlastPurchaseRate = value; }
        }
        public double ProdNumberForBarcode
        {
            get { return _ProdNumberForBarcode; }
            set { _ProdNumberForBarcode = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            _HSNNumber = 3004;
            _ProdCompID = 0;
            _ProdCompShortName = "";
            _ProdLoosePack = 1;
            _ProdPreLoosePack = 0;
            _ProdPack = "";
            _ProdPackTypeID = 0;
            _ProdPackType = "";
            _ProdPackID = 0;
            _IfNewPack = "";
            _IfNewPackType = "";
            _ProdVATPer = 0;
            _ProdCST = 0;
            _ProdMinLevel = 0;
            _ProxMaxLevel = 0;
            _ProdOpeningStock = 0;
            _ProdClosingStock = 0;
            _ProdBoxQty = 0;
            _ProdGenericID = 0;
            _ProdShelfID = 0;
            _ProdShelfCode = "";
            _ProdProductCategoryID = 0;
            _ProdCreditor1ID = 0;
            _ProdCreditor2ID = 0;
            _ProdGrade = "A";
            _ProdScheduleDrugCode = "";
            _ProdScheduleDrugName = "";
            _ProdIfShortList = "Y";
            _ProdIfSaleDisc = "N";
            _ProdIfAddOctroi = "N";
            _ProdIfPurchaseIncl = "N";
            _ProdIfScheduledDrug = "Y";
            _ProdIfMRPIncl = "N";
            _ProdLinkDrugId = 0;
            _ProdRequireColdStorage = "N";
            _ProdIfBarCodeRequired = "N";
            _ScannedBarcode = "";
            _ProdCanChangeLoosePack = "N";
            _ProdlastPurchaseRate = 0;
        }

        public override void DoValidate()
        {
            try
            {
                bool ifvalidvatpercent = GetVatPercent(_ProdVATPer);
                if (!ifvalidvatpercent)
                    ValidationMessages.Add("Please Check VAT %");
                if (Name == "")
                    ValidationMessages.Add("Please enter the Product Name.");
                if (ProdCompShortName == "")
                    ValidationMessages.Add("Please enter Company Short Name.");
                if (ProdCompShortName.Trim().Length < 3)
                    ValidationMessages.Add("Company Name should be 3 characters");
                if (ProdCompID == 0)
                    ValidationMessages.Add("Please Select Company ");
                if (ProdLoosePack == 0)
                    ValidationMessages.Add("Please enter Unit Of Measure");
                if (ProdPack == "")
                    ValidationMessages.Add("Please enter Pack");
                if (ProdPack.Trim().Length > 6)
                    ValidationMessages.Add("Pack Maximum 6 characters");
                if ((ProdMinLevel > ProdMaxLevel) && (ProdMaxLevel != 0))
                    ValidationMessages.Add("Please Check Maximum/Minimum Level");
                if (ProdProductCategoryID == 0)
                    ValidationMessages.Add("Please enter Product Category");
                if (IFEdit == "Y" && ProdLoosePack != ProdPreLoosePack)
                {
                    if (!CanBeDeleted())
                        ValidationMessages.Add("Loose Pack Cannot change Transaction Done");
                }

                DBProduct dbProd = new DBProduct();

                if (IFEdit == "Y")
                {
                    if (dbProd.IsNameUniqueForEdit(Name, ProdLoosePack, ProdPack, ProdCompID, IntID ))
                    {
                        ValidationMessages.Add("Product Already Exists.");
                    }
                }
                else
                {
                    if (dbProd.IsNameUniqueForAdd(Name, ProdLoosePack, ProdPack, ProdCompID, IntID ))
                    {
                        ValidationMessages.Add("Product Already Exists.");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        public override bool CanBeDeleted()
        {
            bool bRetValue = false;
            try
            {
                int _rowcount = 0;
                DBDelete dbdelete = new DBDelete();
                _rowcount = dbdelete.GetOverviewDataSelect("detailsale", "ProductID", Id);
                if (_rowcount == 0)
                {
                    _rowcount = dbdelete.GetOverviewDataSelect("detailpurchase", "ProductID", Id);
                    if (_rowcount == 0)
                    {
                        _rowcount = dbdelete.GetOverviewDataSelect("linkpatientproduct", "ProductID", Id);
                        if (_rowcount == 0)
                        {
                            _rowcount = dbdelete.GetOverviewDataSelect("linkprescription", "ProductID", Id);
                            if (_rowcount == 0)
                            {
                                _rowcount = dbdelete.GetOverviewDataSelect("detailcreditdebitnotestock", "ProductID", Id);
                                if (_rowcount == 0)
                                {
                                    _rowcount = dbdelete.GetOverviewDataSelect("detailopStock", "ProductID", Id);
                                    if (_rowcount == 0)
                                    {
                                        _rowcount = dbdelete.GetOverviewDataSelecttblStock("tblstock", "ProductID", Id);
                                        if (_rowcount == 0)
                                        {
                                            //_rowcount = dbdelete.GetOverviewDataSelect("linkdruggrouping", "ProductID", Id);
                                            //if (_rowcount == 0)
                                            bRetValue = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return bRetValue;
        }

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewData();
        }


        public DataTable GetOverviewData(double vatPercent)
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewData(vatPercent);
        }
        public DataTable GetOverviewDataForCache()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataForCache();
        }
        public DataTable GetOverviewDataForReports()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataForReports();
        }
        public bool GetVatPercent(double vatper)
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetVatPercent(vatper);
        }
        public DataRow GetOverviewDataForProductIDForCache(string productID)
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataForProductIDForCache(productID);
        }

        public DataTable GetOverviewDataForClosingStockNotZero()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataForClosingStockNotZero();
        }
        public DataTable GetOverviewDataForClosingStockNotZero(string productID)
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataForClosingStockNotZero(productID);
        }
        public DataTable GetOverviewDataForList()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataForList();
        }
        public DataTable GetOverviewDataWithOutZeroAllProducts()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataWithOutZeroAllProducts();
        }
        public DataTable GetOverviewDataWithZeroAllProducts()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataWithZeroAllProducts();
        }
        public DataTable GetOverviewDataWithOutZeroAllProductsBatchWise()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataWithOutZeroAllProductsBatchWise();
        }
        public DataTable GetForClosingStockNotZero()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetForClosingStockNotZero();
        }

        public DataTable GetOverviewDataForCompany(string compcd)
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataForCompany(compcd);
        }
        public DataTable GetOverviewDataForShelf(string shelfcode)
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataForShelf(shelfcode);
        }
        public DataTable GetOverviewDataForVATPercent(double vatpercent)
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.GetOverviewDataForVATPercent(vatpercent);
        }
        //public DataTable GetOverviewDataForMultipleCompanies()
        //{
        //    DBProduct dbProd = new DBProduct();
        //    return dbProd.GetOverviewDataForMultipleCompanies();
        //}

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBProduct dbProd = new DBProduct();
                drow = dbProd.ReadDetailsByID(Id);

                if (drow != null)
                {
                    if (drow["ProductId"] != DBNull.Value)
                        Id = drow["ProductId"].ToString();
                    if (drow["ProdName"] != DBNull.Value)
                        Name = Convert.ToString(drow["ProdName"]);
                    if (drow["ProdCompID"] != DBNull.Value)
                        ProdCompID = Convert.ToInt32 ( drow["ProdCompID"]);
                    if (drow["ProdCompShortName"] != DBNull.Value)
                        ProdCompShortName = Convert.ToString(drow["ProdCompShortName"]);
                    if (drow["ProdLoosePack"] != DBNull.Value)
                    {
                        ProdLoosePack = Convert.ToInt32(drow["ProdLoosePack"]);
                        ProdPreLoosePack = ProdLoosePack;
                    }
                    if (drow["ProdPack"] != DBNull.Value)
                        ProdPack = Convert.ToString(drow["ProdPack"]);
                    if (drow["ProdPackType"] != DBNull.Value)
                        ProdPackType = Convert.ToString(drow["ProdPackType"]);
                    if (drow["ProdVATPercent"] != DBNull.Value)
                        ProdVATPer = Convert.ToDouble(drow["ProdVATPercent"]);
                    if (drow["ProdCST"] != DBNull.Value)
                        ProdCST = Convert.ToDouble(drow["ProdCST"]);
                    if (drow["ProdMinLevel"] != DBNull.Value)
                        ProdMinLevel = Convert.ToInt32(drow["ProdMinLevel"]);
                    if (drow["ProdMaxLevel"] != DBNull.Value)
                        ProdMaxLevel = Convert.ToInt32(drow["ProdMaxLevel"]);
                    if (drow["ProdBoxQuantity"] != DBNull.Value)
                        ProdBoxQty = Convert.ToInt32(drow["ProdBoxQuantity"]);
                    if (drow["ProdDrugID"] != DBNull.Value)
                        ProdGenericID = Convert.ToInt32 ( drow["ProdDrugID"]);
                    if (drow["ShelfCode"] != DBNull.Value)
                        ProdShelfID = Convert.ToInt32 (drow["ProdShelfID"]);
                    if (drow["ProdCategoryID"] != DBNull.Value)
                        ProdProductCategoryID = Convert.ToInt32 (drow["ProdCategoryID"]);
                    if (drow["ProdPartyId_1"] != DBNull.Value)
                        ProdCreditor1ID = Convert.ToInt32( drow["ProdPartyId_1"]);
                    if (drow["ProdPartyId_2"] != DBNull.Value)
                        ProdCreditor2ID =  Convert.ToInt32( drow["ProdPartyId_2"]);
                    if (drow["ProdGrade"] != DBNull.Value)
                        ProdGrade = Convert.ToString(drow["ProdGrade"]);
                    if (drow["ProdScheduleDrugCode"] != DBNull.Value)
                        ProdScheduleDrugCode = Convert.ToString(drow["ProdScheduleDrugCode"]);
                    if (drow["ProdIfShortListed"] != DBNull.Value)
                        ProdIfShortList = Convert.ToString(drow["ProdIfShortListed"]);
                    if (drow["ProdIfSaleDisc"] != DBNull.Value)
                        ProdIfSaleDisc = Convert.ToString(drow["ProdIfSaleDisc"]);
                    if (drow["ProdIfOctroi"] != DBNull.Value)
                        ProdIfAddOctroi = Convert.ToString(drow["ProdIfOctroi"]);
                    if (drow["ProdIfPurchaseRateInclusive"] != DBNull.Value)
                        ProdIfPurchaseIncl = Convert.ToString(drow["ProdIfPurchaseRateInclusive"]);
                    if (drow["ProdIfMRPInclusive"] != DBNull.Value)
                        ProdIfMRPIncl = Convert.ToString(drow["ProdIfMRPInclusive"]);
                    if (drow["ProdIfSchedule"] != DBNull.Value)
                        ProdIfScheduledDrug = Convert.ToString(drow["ProdIfSchedule"]);
                    if (drow["ProdRequireColdStorage"] != DBNull.Value)
                        ProdRequireColdStorage = Convert.ToString(drow["ProdRequireColdStorage"]);
                    if (drow["ScannedBarcode"] != DBNull.Value)
                        ScannedBarcode = Convert.ToString(drow["ScannedBarcode"]);
                    if (drow["ProdlastPurchaseRate"] != DBNull.Value)
                        ProdlastPurchaseRate = Convert.ToDecimal(drow["ProdlastPurchaseRate"]);
                    if (drow["HSNNumber"] != DBNull.Value)
                        HSNNumber = Convert.ToInt32(drow["HSNNumber"]);

                    retValue = true;

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public DataRow ReadLastSaleByID(string saleId)
        {
            DataRow dr = null;
            try
            {
                DBProduct dbprod = new DBProduct();
                dr = dbprod.ReadLastSaleByID(saleId);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }
        public DataRow ReadLastPurchaseByID(string purId)
        {
            DataRow dr = null;
            try
            {
                DBProduct dbprod = new DBProduct();
                dr = dbprod.ReadLastPurchaseByID(purId);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }
        public DataTable ReadPatientProdDetailsByID()
        {
            DataTable dt = null;
            try
            {
                DBProduct dbProd = new DBProduct();
                dt = dbProd.ReadPatientProdDetailsByID(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;

        }

        public bool AddDetails()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.AddDetails(IntID , Name, ProdLoosePack, ProdPack, ProdPackType, ProdCompShortName,
                 ProdCompID, ProdVATPer, ProdCST, ProdGrade, ProdMinLevel, ProdMaxLevel, ProdBoxQty,
            ProdGenericID, ProdShelfID, ProdScheduleDrugCode,
             ProdProductCategoryID, ProdCreditor1ID, ProdCreditor2ID,
        ProdIfShortList, ProdIfSaleDisc, ProdIfAddOctroi,
        ProdIfMRPIncl, ProdIfPurchaseIncl, ProdOpeningStock, ProdClosingStock, ProdIfScheduledDrug,
        ProdRequireColdStorage, ProdIfBarCodeRequired, ScannedBarcode, HSNNumber, CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddPack()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.AddPack(ProdPackID, ProdPack);
        }
        public bool AddPackType()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.AddPackType(ProdPackTypeID, ProdPackType);
        }
        public bool UpdateDetails()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.UpdateDetails(Id, Name, ProdLoosePack, ProdPack, ProdPackType, ProdCompShortName,
                 ProdCompID, ProdVATPer, ProdCST, ProdGrade, ProdMinLevel, ProdMaxLevel, ProdBoxQty,
            ProdGenericID, ProdShelfID, ProdScheduleDrugCode,
             ProdProductCategoryID, ProdCreditor1ID, ProdCreditor2ID,
        ProdIfShortList, ProdIfSaleDisc, ProdIfAddOctroi,
        ProdIfMRPIncl, ProdIfPurchaseIncl, ProdIfScheduledDrug, ProdRequireColdStorage, ProdIfBarCodeRequired, ScannedBarcode,HSNNumber,
        ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool UpdateDetailsBulk()  // [ansuman]
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.UpdateDetailsBulk(Id, Name, ProdCompID, ProdCompShortName, ProdLoosePack, ProdPack, ProdPackType,
                 ProdVATPer, ProdCST, ProdMinLevel, ProdMaxLevel, ProdRequireColdStorage, ProdShelfID, ProdGenericID, ProdProductCategoryID,
                 ProdCreditor1ID, ProdCreditor2ID, ProdBoxQty, ProdIfShortList, ProdIfScheduledDrug, ProdScheduleDrugCode, ProdIfSaleDisc,
                 ProdGrade, ProdIfBarCodeRequired, ScannedBarcode,HSNNumber, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool UpdateCreditNoteStockInmasterProduct(string ProductID, int Quantity)
        {
            //DBProduct dbProd = new DBProduct();
            //return dbProd.UpdateCreditNoteStockInmasterProduct(Id, Quantity);

            int Closingstock = GetClosingStock();
            if (Closingstock == 0)
            {
                DBProduct dbprod = new DBProduct();
                return dbprod.UpdateCreditNoteStockInmasterProductForNULLClosingStock(ProductID, Quantity);
            }
            else
            {
                Closingstock += Quantity;
                DBProduct dbprod = new DBProduct();
                return dbprod.UpdateCreditNoteStockInmasterProduct(ProductID, Closingstock);
            }
        }

        public int GetClosingStock()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetClosingStock(Id);
        }

        public bool DeleteDetails()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.DeleteDetails(Id);
        }

        public bool UpdateClosingStock()
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.UpdateClosingStock(Id);
        }
        public bool UpdateProdSelfID()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdateProdSelfID(IntID , ProdShelfID);
        }
        public bool UpdateProductScanCode()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdateProductScanCode(IntID , ScannedBarcode);
        }
        public string SearchForProdPack(string pack)
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.SearchForProdPack(pack);
        }
        public Int32 SearchForProdPackType(string packtype)
        {
            DBProduct dbProd = new DBProduct();
            return dbProd.SearchForProdPackType(packtype);
        }
        public bool SaveProductDrugLink()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.SaveProductDrugLink(ProdLinkDrugId, IntID , ProdGenericID, CreatedBy, CreatedDate, CreatedTime);
        }
        public bool RemoveProductDrugLink()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.RemoveProductDrugLink(IntID , ProdGenericID);
        }
        #endregion

        public bool SetMinMax(int _min, int _max)
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.SetMinMax(_min, _max);
        }
        public DataTable GetForBulkMaintenance()  // [ansuman][for testing] 
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetForBulkMaintenance();
        }
        public DataTable GetNonTransactionProductsBulk()  // [ansuman][for testing] 
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetNonTransactionProductsBulk();
        }
        public DataTable GetTransactionProductsBulk()  // [ansuman][for testing] 
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetTransactionProductsBulk();
        }

        public DataTable GetFilteredProdByProdWise(string Prodname)  // [ansuman][for testing] 
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetFilteredProdByProdWise(Prodname);
        }
        public DataTable GetFilteredProdByCompanyWise(string CompID)  // [ansuman][for testing] 
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetFilteredProdByCompanyWise(CompID);
        }
        public DataTable GetFilteredProdByContentWise(string DrugID)  // [ansuman][for testing] 
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetFilteredProdByContentWise(DrugID);
        }
        public bool RemoveEDEProductLink(string prodID, string partyID)
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.RemoveEDEProductLink(prodID, partyID);
        }
        public DataRow GetDuplicateBarcode(string Barcode) // [09.02.2017]
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetDuplicateBarcode(Barcode);
        }
        //public DataRow GetLastProductNoForBarCode()
        //{
        //    DBProduct dbprod = new DBProduct();
        //    return dbprod.GetLastProductNoForBarCode();
        //}
        public Int32 GetIntID()
        {
            int maxid;
            maxid = 1;
            DBProduct dbprod = new DBProduct();
            DataRow idrow = dbprod.GetMaxID();
            if (idrow != null)
            {
                if (idrow["maxid"] != null && idrow["maxid"].ToString() != string.Empty)
                {
                    maxid = Convert.ToInt32(idrow["maxid"]) + 1;
                }
            }
            return maxid;

        }
    }
}
