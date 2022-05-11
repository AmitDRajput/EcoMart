using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class ScanPrescription : BaseObject
    {

        #region Declaration
        private string _SalePrescriptionID;
        private byte[] _PrescriptionData;
        private string _FileExtension;
        private string _SaleBillID;       
        #endregion Declaration        

        #region Constructors, Destructors
        public ScanPrescription()
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
        #endregion Constructors, Destructors     

        #region Properties
        public string SalePrescriptionID
        {
            get { return _SalePrescriptionID; }
            set { _SalePrescriptionID = value; }
        }
        public byte[] PrescriptionData
        {
            get { return _PrescriptionData; }
            set { _PrescriptionData = value; }
        }
        public string FileExtension
        {
            get { return _FileExtension; }
            set { _FileExtension = value; }
        }
        public string SaleBillID
        {
            get { return _SaleBillID; }
            set { _SaleBillID = value; }
        }
        #endregion Properties


        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            _SalePrescriptionID = string.Empty;
         //   _PrescriptionData = byte[];
            _FileExtension = string.Empty;
            _SaleBillID = string.Empty;
        }

        public override void DoValidate()
        {
           
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
            DBScanPrescription dbscm = new DBScanPrescription();
            return dbscm.GetOverviewData();
        }
        public ScanPrescription ReadDetailsBySaleBillID(string Id)
        {
            ScanPrescription scanPrescription = null;
            DataRow dr = null;
            DBScanPrescription dbscm = new DBScanPrescription();
            dr = dbscm.ReadDetailsBySaleBillID(Id);
            if (dr != null)
            {
                scanPrescription = new ScanPrescription();
                scanPrescription.Id = dr["ScanPrescriptionID"].ToString();
                scanPrescription.PrescriptionData = (byte[])dr["PrescriptionData"];
                scanPrescription.FileExtension = dr["FileExtension"].ToString();
                scanPrescription.SaleBillID = dr["SaleBillID"].ToString();
            }
            return scanPrescription;

        }
        //public bool AddDetails()
        //{
        //    DBScanPrescription dbscm = new DBScanPrescription();
        //    return dbscm.AddDetails(Id,PrescriptionData, FileExtension, SaleBillID);            
        //}
    
        #endregion      
    }
}
