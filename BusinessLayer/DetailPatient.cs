using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MitraPlus.DataLayer;
using MitraPlus.Common;

namespace MitraPlus.BusinessLayer
{
    class DetailPatient : BaseObject
    {
        #region "Declaration"

        private string _patientID;
        private string _productID;
        private int _quantity;
     //   private string _CreatedDate;
        private string _CreatedUserID;
        private string _ModifyDate;
        private string _ModifyUserID;

        #endregion

        //Constructors
        public DetailPatient()
        {
            InitialiseP();
        }

        #region "Property Declaration"

        public string PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }

        public string ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        //public string CreatedDate
        //{
        //    get { return _CreatedDate; }
        //    set { _CreatedDate = value; }
        //}

        public string CreatedUserID
        {
            get { return _CreatedUserID; }
            set { _CreatedUserID = value; }
        }

        public string ModifyDate
        {
            get { return _ModifyDate; }
            set { _ModifyDate = value; }
        }

        public string ModifyUserID
        {
            get { return _ModifyUserID; }
            set { _ModifyUserID = value; }
        }

        #endregion

        #region "private methods"

        public void InitialiseP()
        {
            base.Initialise();
            _patientID ="";
            _productID = "";
            _quantity = 0;
         //   _CreatedDate = "";
            _CreatedUserID = "";
            _ModifyDate = "";
            _ModifyUserID = "";
        }

        #endregion

        #region "Public methods"

        public bool AddDetails(string PatientID, string ProductID, int Quantity, string CreatedDate, string CreatedUserID, string ModifyDate, string ModifyUserID)
        {
            DBDetailPatient _dbPatientDtl = new DBDetailPatient();
            return _dbPatientDtl.AddDetails(PatientID, ProductID, Quantity, CreatedDate, CreatedUserID, ModifyDate, ModifyUserID);
        }

        public bool UpdateDetails(string PatientID, string ProductID, int Quantity, string CreatedDate, string CreatedUserID, string ModifyDate, string ModifyUserID)
        {
            DBDetailPatient _dbPatientDtl = new DBDetailPatient();
            return _dbPatientDtl.UpdateDetails(PatientID, ProductID, Quantity, CreatedDate, CreatedUserID, ModifyDate, ModifyUserID);
        }

        public bool DeleteDetail(string PatientID, string ProductID)
        {
            DBDetailPatient _dbPatientDtl = new DBDetailPatient();
            return _dbPatientDtl.DeleteDetail(PatientID, ProductID);
        }

        public bool ReadDetailById()
        {
            DBDetailPatient _dbPatientDtl = new DBDetailPatient();
            bool returnVal = false;
            try
            {
                DataRow dRow = _dbPatientDtl.ReadDetailById(PatientID, ProductID);
                if (dRow != null)
                {
                    if (dRow["PatientID"] != DBNull.Value)
                        PatientID = dRow["PatientID"].ToString();
                    if (dRow["ProductID"] != DBNull.Value)
                        ProductID = dRow["ProductID"].ToString();
                    if (dRow["Quantity"] != DBNull.Value)
                        Quantity = Convert.ToInt16(dRow["Quantity"].ToString());
                    if (dRow["CreatedDate"] != DBNull.Value)
                        CreatedDate = dRow["CreatedDate"].ToString();
                    if (dRow["CreatedUserID"] != DBNull.Value)
                        CreatedUserID = dRow["CreatedUserID"].ToString();
                    if (dRow["ModifiedDate"] != DBNull.Value)
                        ModifyDate = dRow["ModifiedDate"].ToString();
                    //if (dRow["ModifyDate"] != DBNull.Value)
                    //    ModifyDate = dRow["ModifyDate"].ToString();
                    if (dRow["ModifiedUserID"] != DBNull.Value)
                        ModifyUserID = dRow["ModifiedUserID"].ToString();
                    returnVal = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }

        public DataTable ReadDetailsByPatientID()
        {
            DBDetailPatient _dbPatientDtl = new DBDetailPatient();
            return _dbPatientDtl.ReadDetailsByPatientID(PatientID);
        }

        #endregion
    }
}
