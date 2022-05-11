using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class Prescription : BaseObject
    {
        #region Declaration
        private string _ProductID;
        private int _Quantity;
        private bool _DuplicateProduct;
        #endregion

        #region Constructors, Destructors
        public Prescription()
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
        public bool DuplicateProduct
        {
            get { return _DuplicateProduct; }
            set { _DuplicateProduct = value; }
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
     
        #endregion

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBPrescription dbPrescription = new DBPrescription();
            return dbPrescription.GetOverviewData();
        }

        public DataTable GetAllPrescriptions()
        {
            DBPrescription _dbPresc = new DBPrescription();
            return _dbPresc.GetAllPrescriptions();
        }
       
        # endregion
       
        #region  Validations
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _Quantity = 0;
                _ProductID = "";
                _DuplicateProduct = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }              
        }

        public override void DoValidate()
        {
            try
            {
                if (Name == "")
                    ValidationMessages.Add("Please enter the Prescription Name.");

                if (Name != "")
                {
                    DBPrescription dbPrescription = new DBPrescription();
                    if (IFEdit == "Y")
                    {
                        if (dbPrescription.IsNameUniqueForEdit(Name, Id))
                        {
                            ValidationMessages.Add("Prescription Already Exists.");
                        }
                    }
                    else
                    {
                        if (dbPrescription.IsNameUniqueForAdd(Name, Id))
                        {
                            ValidationMessages.Add("Prescription Already Exists.");
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }
        #endregion

        # region Internal Methods

        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }



        
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBPrescription dbPrescription = new DBPrescription();
                drow = dbPrescription.ReadDetailsByID(Id);

                if (drow != null)
                {
                    if (drow["PrescriptionID"] != DBNull.Value)
                        Id = drow["PrescriptionId"].ToString();
                    if (drow["PrescriptionName"] != DBNull.Value)
                        Name = Convert.ToString(drow["PrescriptionName"]);

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }
        public DataTable ReadProductDetailsById(string Id)
        {
           
            DataTable dt = null;
            try
            {
                DBPrescription _dbPresc = new DBPrescription();
                dt = _dbPresc.ReadProdDetailsById(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;


        }
     
       
        public bool AddDetails()
        {
            DBPrescription dbPrescription = new DBPrescription();
            return dbPrescription.AddDetails(Id, Name,CreatedBy,CreatedDate,CreatedTime);
        }  

        public bool AddProductDetails()
        {
            DBPrescription dbPrescriptionProduct = new DBPrescription();
            return dbPrescriptionProduct.AddProductDetails(Id, DetailId, ProductID, Quantity);
        }

        public bool UpdateDetails()
        {
            DBPrescription dbPrescription = new DBPrescription();
            return dbPrescription.UpdateDetails(Id, Name,ModifiedBy,ModifiedDate,ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBPrescription dbPrescription = new DBPrescription();
            return dbPrescription.DeleteDetails(Id);
        }


        public bool DeleteProductsById()
        {
            DBPrescription dbprescription = new DBPrescription();
            return dbprescription.DeleteProductsByID(Id);
        }
        #endregion



    }
}
