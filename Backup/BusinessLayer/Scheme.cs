using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class Scheme : BaseObject
    {
        #region Declaration
        private string _ProductID;
        private string _Pack;
        private int _UOM;
        private string _Compshortname;
        private string _StartDate;
        private string _ClosingDate;
        private int _Quantity1;
        private int _Scheme1;
        private int _Quantity2;
        private int _Scheme2;
        private int _Quantity3;
        private int _Scheme3;
        private string _IFOver;
        private string _IfSchemeAlreadyExist;
        #endregion

        #region Constructors, Destructors
        public Scheme()
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
     
        #region Properties

        public string Pack
        {
            get { return _Pack; }
            set { _Pack = value; }
        }
        public int UOM
        {
            get { return _UOM; }
            set { _UOM = value; }
        }

        public string CompanyShortName
        {
            get { return _Compshortname ; }
            set { _Compshortname = value; }
        }


        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public string ClosingDate
        {
            get { return _ClosingDate; }
            set { _ClosingDate = value; }
        }
        public int Quantity1
        {
            get { return _Quantity1; }
            set { _Quantity1 = value; }
        }
        public int Scheme1
        {
            get { return _Scheme1; }
            set { _Scheme1 = value; }
        }
        public int Quantity2
        {
            get { return _Quantity2; }
            set { _Quantity2 = value; }
        }
        public int Scheme2
        {
            get { return _Scheme2; }
            set { _Scheme2 = value; }
        }
        public int Quantity3
        {
            get { return _Quantity3; }
            set { _Quantity3 = value; }
        }
        public int Scheme3
        {
            get { return _Scheme3; }
            set { _Scheme3 = value; }
        }
        public string IFOver
        {
            get { return _IFOver; }
            set { _IFOver = value; }
        }

        public string IfSchemeAlreadyExist
        {
            get { return _IfSchemeAlreadyExist; }
            set { _IfSchemeAlreadyExist = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            _ProductID = "";
            _Compshortname = "";
            _Pack = "";
            _UOM = 0;
            _Quantity1 = 0;
            _Quantity2 = 0;
            _Quantity3 = 0;
            _Scheme1 = 0;
            _Scheme2 = 0;
            _Scheme3 = 0;
            _IfSchemeAlreadyExist = "N";
        }

        public override void DoValidate()
        {
            if ( Convert.ToInt32(ClosingDate) < Convert.ToInt32(StartDate))          
                ValidationMessages.Add("Please Enter Correct Closing Date");          
        }
        public override bool CanBeDeleted()
        {
            bool bRetValue = true;          
            return bRetValue;
        }

        #endregion

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBScheme dbscm = new DBScheme();
            return dbscm.GetOverviewData();
        }

        public DataTable GetOverviewDataForSchemeListAll()
        {
            DBScheme dbscm = new DBScheme();
            return dbscm.GetOverviewDataForSchemeListAll(DateTime.Now.Date.ToString("yyyyMMdd"));
        }

        public DataTable GetSchemeDataForSelectedCompany(string mcompid)
        {
            DBScheme dbscm = new DBScheme();
            return dbscm.GetSchemeDataForSelectedCompany(mcompid);
        }

        public DataTable GetOverviewDataForSelectedCompany(string mcompcode)
        {
            DBScheme dbscm = new DBScheme();
            return dbscm.GetSchemeDataForSelectedCompany(mcompcode);
        }

        public DataTable GetOverviewDataForSelectedProduct(string mproductID)
        {
            DBScheme dbscm = new DBScheme();
            return dbscm.GetOverviewDataForSelectedProduct(mproductID);
        }
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            DBScheme dbscm = new DBScheme();
            try
            {
                drow = dbscm.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["Id"].ToString();
                    ProductID = drow["ProductID"].ToString();
                    UOM = Convert.ToInt32(drow["ProdLoosePack"].ToString());
                    Pack = drow["ProdPack"].ToString();
                    CompanyShortName = drow["ProdCompShortName"].ToString();
                    StartDate = drow["StartingDate"].ToString();
                    ClosingDate = drow["ClosingDate"].ToString();
                    Quantity1 = Convert.ToInt32(drow["ProductQuantity1"].ToString());
                    Quantity2 = Convert.ToInt32(drow["ProductQuantity2"].ToString());
                    Quantity3 = Convert.ToInt32(drow["ProductQuantity3"].ToString());
                    Scheme1 = Convert.ToInt32(drow["SchemeQuantity1"].ToString());
                    Scheme2 = Convert.ToInt32(drow["SchemeQuantity2"].ToString());
                    Scheme3 = Convert.ToInt32(drow["SchemeQuantity3"].ToString());
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }


        public bool ReadDetailsByProductID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBScheme dbscm = new DBScheme();
                drow = dbscm.ReadDetailsByProductID(ProductID);

                if (drow != null)
                {
                    Id = drow["Id"].ToString();
                    ProductID = drow["ProductID"].ToString();
                    StartDate = drow["StartingDate"].ToString();
                    ClosingDate = drow["ClosingDate"].ToString();
                    Quantity1 = Convert.ToInt32(drow["ProductQuantity1"].ToString());
                    Quantity2 = Convert.ToInt32(drow["ProductQuantity2"].ToString());
                    Quantity3 = Convert.ToInt32(drow["ProductQuantity3"].ToString());
                    Scheme1 = Convert.ToInt32(drow["SchemeQuantity1"].ToString());
                    Scheme2 = Convert.ToInt32(drow["SchemeQuantity2"].ToString());
                    Scheme3 = Convert.ToInt32(drow["SchemeQuantity3"].ToString());
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }

        public bool AddDetails()
        {
            DBScheme dbscm = new DBScheme();
            return dbscm.AddDetails(Id,ProductID,StartDate,ClosingDate,Quantity1,Scheme1,Quantity2,Scheme2,Quantity3,Scheme3, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBScheme dbscm = new DBScheme();
            return dbscm.UpdateDetails(Id,ProductID, StartDate, ClosingDate, Quantity1, Scheme1, Quantity2, Scheme2, Quantity3, Scheme3, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBBank dbBank = new DBBank();
            return dbBank.DeleteDetails(Id);
        }

        #endregion      
    }
}
