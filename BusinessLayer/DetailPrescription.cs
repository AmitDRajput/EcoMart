using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class DetailPrescription
    {
        string _PrescriptionID;
	    int _ProductID;
	    int _Quantity;
	    string _CreatedDate;
	    string _CreatedUserID;
	    string _ModifyDate;
        string _ModifyUserID;

        #region "Property Declaration"

        public string PrescriptionID
        {
            get { return _PrescriptionID; }
            set { _PrescriptionID = value; }
        }

        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

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

        public bool AddDetail()
        {
            DBDetailPrescription dbPresc = new DBDetailPrescription();
            return dbPresc.AddDetail(PrescriptionID, ProductID, Quantity, CreatedDate, CreatedUserID, ModifyDate, ModifyUserID);
        }

        public bool UpdateDetail()
        {
            DBDetailPrescription dbPresc = new DBDetailPrescription();
            return dbPresc.UpdateDetail(PrescriptionID, ProductID, Quantity, CreatedDate, CreatedUserID, ModifyDate, ModifyUserID);
        }

        public bool DeleteDetail()
        {
            DBDetailPrescription dbPresc = new DBDetailPrescription();
            return dbPresc.DeleteDetail(PrescriptionID);
        }

        public DataTable GetAllPrescriptions()
        {
            DBDetailPrescription dbPrecDtl = new DBDetailPrescription();
            return dbPrecDtl.GetAllPrescriptions();
        }

        public DataTable GetAllPrescriptionsByID()
        {
            DBDetailPrescription dbPrecDtl = new DBDetailPrescription();
            return dbPrecDtl.GetAllPrescriptionsByID(PrescriptionID);
        }
    }
}
