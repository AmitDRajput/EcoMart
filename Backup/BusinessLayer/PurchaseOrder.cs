using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;


namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class PurchaseOrder : BaseObject
    {
        #region Declaration      
        private string _VoucherSeries;
        private string _VoucherType;
        private int _VoucherNumber;
        private string _VoucherDate;  
        private string _AccountID;
        private string _Narration;
        private string _Address1;
        private string _Address2;
        private int _NoofRows;
        private string _IFSAVE;
        private double _Amount;
        private string _ShortListID;

        private double _PurchaseRate;
        private string _ProductID;
        private int _Quantity;
        private bool _DuplicateProduct;

        private string _ShortListDate;
        private string _IfDalilyShortList;
        private string _DSLCreatedBy;
        private string _DSLCreatedDate;
        private string _DSLCreatedTime;

        #endregion
        # region properties

        public string ShortListDate
        {
            get { return _ShortListDate; }
            set { _ShortListDate = value; }
        }
        public string IfDalilyShortList
        {
            get { return _IfDalilyShortList; }
            set { _IfDalilyShortList = value; }
        }
        public string DSLCreatedBy
        {
            get { return _DSLCreatedBy; }
            set { _DSLCreatedBy = value; }   
        }

        public string DSLCreatedDate
        {
            get { return _DSLCreatedDate; }
            set { _DSLCreatedDate = value; }
        }
        public string DSLCreatedTime
        {
            get { return _DSLCreatedTime; }
            set { _DSLCreatedTime = value; }
        }

        public string VoucherSeries
        {
            get { return _VoucherSeries; }
            set { _VoucherSeries = value; }
        }

        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }


        public string VoucherType
        {
            get { return _VoucherType; }
            set { _VoucherType = value; }
        }
        public int VoucherNumber
        {
            get { return _VoucherNumber; }
            set { _VoucherNumber = value; }
        }
        public string VoucherDate
        {
            get { return _VoucherDate; }
            set { _VoucherDate = value; }
        }
      
        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }
        public string Narration
        {
            get { return _Narration; }
            set { _Narration = value; }
        }
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }

        }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public bool DuplicateProduct
        {
            get { return _DuplicateProduct; }
            set { _DuplicateProduct = value; }
        }
        public int NoofRows
        {
            get { return _NoofRows; }
            set { _NoofRows = value; }
        }
        public double PurchaseRate
        {
            get { return _PurchaseRate; }
            set { _PurchaseRate = value; }
        }
        public string IFSAVE
        {
            get { return _IFSAVE; }
            set { _IFSAVE = value; }
        }
        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public string ShortListID
        {
            get { return _ShortListID; }
            set { _ShortListID = value; }
        }
        #endregion

        public override void Initialise()
        {
            base.Initialise();
         
            _VoucherSeries = "";
            _VoucherType = FixAccounts.VoucherTypeForPurchaseOrder;
            _VoucherNumber = 0;
            _VoucherDate = "";
            _AccountID = "";
            _PurchaseRate = 0;
            _Amount = 0;
            _ShortListID = "";
            _Address1 = "";
            _Address2 = "";

            _Quantity = 0;
            _ProductID = "";
            _DuplicateProduct = false;
            _NoofRows = 0;
            _IFSAVE = "Y";
            _Narration = null;
        }
        public override void DoValidate()
        {
            try
            {
                if (AccountID == "")
                    ValidationMessages.Add("Please enter Creditor");

                if (NoofRows == 0)
                    ValidationMessages.Add("Please enter  Product");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }
        public bool AddDetails()
        {
            DBPurchaseOrder dbcrdb = new DBPurchaseOrder();
            return dbcrdb.AddDetails(Id, AccountID, Narration,Amount,VoucherType, VoucherNumber, VoucherDate,General.ShopDetail.ShopVoucherSeries,  CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddProductDetails()
        {
            DBPurchaseOrder dbpo = new DBPurchaseOrder();        
            return dbpo.AddProductDetails(ShortListID, Id, ProductID, Quantity,AccountID,PurchaseRate,VoucherNumber,VoucherDate,IFSAVE,CreatedBy,CreatedDate,CreatedTime);
        }
        public bool AddProductDetailsModified()
        {
            DBPurchaseOrder dbpo = new DBPurchaseOrder();
            return dbpo.AddProductDetailsModified(ShortListID, Id, ProductID, Quantity, AccountID, PurchaseRate, VoucherNumber, VoucherDate, IFSAVE, ShortListDate,IfDalilyShortList,  DSLCreatedBy, DSLCreatedDate, DSLCreatedTime ,ModifiedBy, ModifiedDate,ModifiedTime);
        }
        public bool UpdateDetails()
        {
            DBPurchaseOrder dbcrdb = new DBPurchaseOrder();
            return dbcrdb.UpdateDetails(Id, AccountID, Narration, Amount, VoucherDate,ModifiedBy,ModifiedDate,ModifiedTime );
        }
        public bool DeleteDetails()
        {
            DBPurchaseOrder dbcrdb = new DBPurchaseOrder();
            return dbcrdb.DeleteDetails(Id);
        }
        public bool DeletePreviousProducts()
        {
            DBPurchaseOrder dbpo = new DBPurchaseOrder();
            return dbpo.DeletePreviousProducts(Id);
        }

        public DataTable GetOverviewData()
        {
            DBPurchaseOrder dbStock = new DBPurchaseOrder();
            return dbStock.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBPurchaseOrder dbord = new DBPurchaseOrder();
                drow = dbord.ReadDetailsByID(Id);

                if (drow != null)
                {
                    AccountID = drow["AccountId"].ToString();
                    VoucherDate = drow["VoucherDate"].ToString();
                    VoucherNumber = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    Narration = Convert.ToString(drow["Narration"]);
                    Amount = Convert.ToDouble(drow["Amount"].ToString());
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }

        public bool ReadDetailsByVoucherNumber(int vouno)
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBPurchaseOrder dbord = new DBPurchaseOrder();
                drow = dbord.ReadDetailsByVoucherNumber(vouno);

                if (drow != null)
                {
                    Id = drow["ID"].ToString();
                    AccountID = drow["AccountId"].ToString();
                    VoucherDate = drow["VoucherDate"].ToString();
                    VoucherNumber = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    Narration = Convert.ToString(drow["Narration"]);
                    Amount = Convert.ToDouble(drow["Amount"].ToString());
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public DataTable ReadProductDetailsByID()
        {

            DataTable dt = new DataTable();
            try
            {
                dt = null;
                DBPurchaseOrder dbStock = new DBPurchaseOrder();
                dt = dbStock.ReadProductDetailsByID(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            } 
            return dt;
        }

        public DataTable ReadProductDetailsByAccountID(string AccountID)
        {

            DataTable dt = new DataTable();
            try
            {
                dt = null;
                DBPurchaseOrder dbStock = new DBPurchaseOrder();
                dt = dbStock.ReadProductDetailsByAccountID(AccountID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }
    }
}
