using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MitraPlus.DataLayer;
using MitraPlus.Common;

namespace MitraPlus.BusinessLayer
{
    public class DetailSale :  BaseObject
    {
        #region "property variables"

        private string _MasterSaleId;
        private string _ProductId;
        private string _BatchNumber;
        private double _PuchaseRate;
        private double _MRP;
        private double _SaleRate;
        private string _Expiry;
        private string _ExpiryDate;
        private int _Quantity;
        private int _SchemeQuantity;
        private string _AccountID;
        private string _PatientID;
        private string _CompanyID;
        private string _DoctorID;
        private double _OctroiPer;
        private double _OctroiAmount;
        private double _CSTPer;
        private double _CSTAmount;
        private double _VATPer;
        private double _VATAmount;
        private string _InwardNumber;
        private char _IPDOPDCode;
        private int _IndentNumber;
        private string _PatientName;
        private string _PatientAddress1;
        private string _PatientAddress2;
        private string _DoctorNameAddress;
        private double _PMTDiscount;
        private double _PMTAmount;
        private char _IfProductDiscount;

        #endregion

        #region "Property Declaration"

        public string MasterSaleId
        {
            get { return _MasterSaleId; }
            set { _MasterSaleId = value; }
        }

        public string ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
        }

        public string BatchNumber
        {
            get { return _BatchNumber; }
            set { _BatchNumber = value; }
        }

        public double PuchaseRate
        {
            get { return _PuchaseRate; }
            set { _PuchaseRate = value; }
        }

        public double MRP
        {
            get { return _MRP; }
            set { _MRP = value; }
        }

        public double SaleRate
        {
            get { return _SaleRate; }
            set { _SaleRate = value; }
        }

        public string Expiry
        {
            get { return _Expiry; }
            set { _Expiry = value; }
        }

        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public int SchemeQuantity
        {
            get { return _SchemeQuantity; }
            set { _SchemeQuantity = value; }
        }

        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        public string PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        public string CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }

        public string DoctorID
        {
            get { return _DoctorID; }
            set { _DoctorID = value; }
        }

        public double OctroiPer
        {
            get { return _OctroiPer; }
            set { _OctroiPer = value; }
        }

        public double OctroiAmount
        {
            get { return _OctroiAmount; }
            set { _OctroiAmount = value; }
        }

        public double CSTPer
        {
            get { return _CSTPer; }
            set { _CSTPer = value; }
        }

        public double CSTAmount
        {
            get { return _CSTAmount; }
            set { _CSTAmount = value; }
        }

        public double VATPer
        {
            get { return _VATPer; }
            set { _VATPer = value; }
        }

        public double VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }

        public string InwardNumber
        {
            get { return _InwardNumber; }
            set { _InwardNumber = value; }
        }

        //public char IPDOPDCode
        //{
        //    get { return _IPDOPDCode; }
        //    set { _IPDOPDCode = value; }
        //}

        public int IndentNumber
        {
            get { return _IndentNumber; }
            set { _IndentNumber = value; }
        }

        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }

        public string PatientAddress1
        {
            get { return _PatientAddress1; }
            set { _PatientAddress1 = value; }
        }
        public string PatientAddress2
        {
            get { return _PatientAddress2; }
            set { _PatientAddress2 = value; }
        }
        public string DoctorNameAddress
        {
            get { return _DoctorNameAddress; }
            set { _DoctorNameAddress = value; }
        }

        public double PMTDiscount
        {
            get { return _PMTDiscount; }
            set { _PMTDiscount = value; }
        }

        public double PMTAmount
        {
            get { return _PMTAmount; }
            set { _PMTAmount = value; }
        }

        public char IfProductDiscount
        {
            get { return _IfProductDiscount; }
            set { _IfProductDiscount = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _MasterSaleId = "";
                _ProductId = "";
                _BatchNumber = "";
                _PuchaseRate = 0;
                _MRP = 0;
                _SaleRate = 0;
                _Expiry = "";
                _ExpiryDate = "";
                _Quantity = 0;
                _SchemeQuantity = 0;
                _AccountID = "";
                _PatientID = "";
                _CompanyID = "";
                _DoctorID = "";
                _OctroiPer = 0;
                _OctroiAmount = 0;
                _CSTPer = 0;
                _CSTAmount = 0;
                _VATPer = 0;
                _VATAmount = 0;
                _InwardNumber = "";
                _IPDOPDCode = '-';
                _IndentNumber = 0;
                _PatientName = "";
                _PatientAddress1 = "";
                _PatientAddress2 = "";
                _DoctorNameAddress = "";
                _PMTDiscount = 0;
                _PMTAmount = 0;
                _IfProductDiscount = '-';
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
                ValidationMessages.Clear();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }

        #endregion

        #region "Public Methods"

        public bool AddDetails()
        {
            DBDetailSale oDetailSale = new DBDetailSale();
            return oDetailSale.Save(MasterSaleId, ProductId, BatchNumber, PuchaseRate, MRP, SaleRate, Expiry, ExpiryDate,
                Quantity, SchemeQuantity, AccountID, PatientID, CompanyID, DoctorID, OctroiPer, OctroiAmount,
                CSTPer, CSTAmount, VATPer, VATAmount, InwardNumber, IPDOPDCode, IndentNumber, PatientName, PatientAddress1,PatientAddress2,
                DoctorNameAddress, PMTDiscount, PMTAmount, IfProductDiscount);
        }

        public bool UpdateDetails()
        {
            DBDetailSale oDetailSale = new DBDetailSale();
            return oDetailSale.Update(MasterSaleId, ProductId, BatchNumber, PuchaseRate, MRP, SaleRate, Expiry, ExpiryDate,
                Quantity, SchemeQuantity, AccountID, PatientID, CompanyID, DoctorID, OctroiPer, OctroiAmount,
                CSTPer, CSTAmount, VATPer, VATAmount, InwardNumber, IPDOPDCode, IndentNumber, PatientName, PatientAddress1,PatientAddress2,
                DoctorNameAddress, PMTDiscount, PMTAmount, IfProductDiscount);
        }

        public bool DeleteDetailById()
        {
            DBDetailSale oDetailSale = new DBDetailSale();
            return oDetailSale.DeleteById(MasterSaleId, ProductId);
        }

        public bool DeleteDetails()
        {
            DBDetailSale oDetailSale = new DBDetailSale();
            return oDetailSale.DeleteAll(MasterSaleId);
        }

        #endregion

        public DataTable ReadDetailById()
        {
            DBDetailSale objDtlSale = new DBDetailSale();
            return objDtlSale.ReadDetailById(MasterSaleId);
        }
    }
}
