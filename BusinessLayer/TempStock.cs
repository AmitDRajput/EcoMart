using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class TempStock : BaseObject
    {
        #region Declaration
        private string _TempStockId;
        private int _ProductID;
        private string _StockId;
        private int _SoldQuantity;
        private ModuleNumber _ModuleNumber;
        private string _CompName;
        private OperationMode _Mode;
        private int _CustomerNumber;
        #endregion Declaration

        #region Properties
        public string TempStockId
        {
            get { return _TempStockId; }
            set { _TempStockId = value; }
        }

        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public string StockId
        {
            get { return _StockId; }
            set { _StockId = value; }
        }
        public int SoldQuantity
        {
            get { return _SoldQuantity; }
            set { _SoldQuantity = value; }
        }
        public ModuleNumber ModuleNumber
        {
            get { return _ModuleNumber; }
            set { _ModuleNumber = value; }
        }
        public string CompName
        {
            get { return _CompName; }
            set { _CompName = value; }
        }
        public OperationMode Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        public int CustomerNumber
        {
            get { return _CustomerNumber; }
            set { _CustomerNumber = value; }
        }
        #endregion Properties

          #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _TempStockId = "";
                _ProductID = 0;
                _StockId = "";
                _SoldQuantity = 0;
                _ModuleNumber = ModuleNumber.None;
                _CompName = "";
                _Mode = OperationMode.None;
                _CustomerNumber = -1; //Default = -1

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion Internal Methods

        #region Public Methods
        //public DataTable GetAllTempStockRows()
        //{
        //    DbTempStock dbData = new DbTempStock();
        //    return dbData.GetAllTempStockRows();
        //}

        //public DataTable GetStockByProductID(int prodID, OperationMode mode)
        //{
        //    DbTempStock dbData = new DbTempStock();
        //    return dbData.GetStockByProductID(prodID, (int)mode);
        //}

        //public DataTable GetStockByStockID(string stockID, ModuleNumber moduleNumber, string compName, OperationMode mode)
        //{
        //    DbTempStock dbData = new DbTempStock();
        //    return dbData.GetStockByStockID(stockID, (int)moduleNumber, compName, (int)mode);
        //}

        //public string GetStockByStockIDAndProductID(string stockID, int ProductID, ModuleNumber moduleNumber, string compName, OperationMode mode, int customerNumber)
        //{
        //    DbTempStock dbData = new DbTempStock();
        //    return dbData.GetStockByStockIDAndProductID(StockId, ProductID, (int)ModuleNumber, CompName, (int)Mode, customerNumber);
        //}
               
        //public bool AddDetails()
        //{
        //    DbTempStock dbData = new DbTempStock();
        //    TempStockId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //    return dbData.AddDetails(TempStockId, StockId, ProductID, SoldQuantity, (int)ModuleNumber, CompName, (int)Mode, CustomerNumber);
        //}

        //public bool UpdateDetails()
        //{
        //    DbTempStock dbData = new DbTempStock();
        //    return dbData.UpdateDetails(TempStockId, StockId, ProductID, SoldQuantity, (int)ModuleNumber, CompName, (int)Mode, CustomerNumber);
        //}

        //public bool DeleteDetails()
        //{
        //    DbTempStock dbData = new DbTempStock();
        //    return dbData.DeleteDetails(StockId, ProductID, SoldQuantity, (int)ModuleNumber, CompName, CustomerNumber);
        //}

        //public bool DeleteDetailsByModuleNumber(ModuleNumber moduleNumber)
        //{
        //    DbTempStock dbData = new DbTempStock();
        //    return dbData.DeleteDetailsByModuleNumber((int)moduleNumber, Environment.MachineName);
        //}
        //public bool DeleteDetailsByModuleNumberAndCustomerNumber(ModuleNumber moduleNumber, int customerNumber)
        //{
        //    DbTempStock dbData = new DbTempStock();
        //    return dbData.DeleteDetailsByModuleNumberAndCustomerNumber((int)moduleNumber, Environment.MachineName, customerNumber);
        //}
        //public bool DeleteDetailsByComputerName()
        //{
        //    DbTempStock dbData = new DbTempStock();
        //    return dbData.DeleteDetailsByComputerName(Environment.MachineName);
        //}
        #endregion Public Methods
    }
   
}
