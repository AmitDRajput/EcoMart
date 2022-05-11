using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class Company : BaseObject
    {

        #region Declaration
        private string _CName;   
        private string _Telephone;
        private string _MailID;
        private string _ContactPerson;
        private string _Address;
        private string _ShortName;
        private string _PartyID_1;
        private string _PartyID_2;
  
        #endregion

        #region Constructors, Destructors
        public Company()
        {
            try
            {
                Initialise();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        #region Properties
        public string CName
        {
            get { return _CName; }
            set { _CName = value; }
        }       
        public string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }
        public string MailID
        {
            get { return _MailID; }
            set { _MailID = value; }
        }
        public string ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        public string PartyID_1
        {
            get { return _PartyID_1; }
            set { _PartyID_1 = value; }
        }
        public string PartyID_2
        {
            get { return _PartyID_2; }
            set { _PartyID_2 = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            _CName = "";
            _ShortName = "";
            _Telephone = "";
            _MailID = "";
            _ContactPerson = "";
            _Address = "";
            _PartyID_1 = "";
            _PartyID_2 = "";
            
            
        }

        public override void DoValidate()
        {
            try
            {
                if (CName == "")
                    ValidationMessages.Add("Please enter Company Name.");
                if (ShortName == "")
                    ValidationMessages.Add("Please enter Company Short Name.");

                DBCompany dbComp = new DBCompany();

                if (IFEdit == "Y")
                {
                    if (dbComp.IsNameUniqueForEdit(CName, Id))
                    {
                        ValidationMessages.Add("Company Already Exists.");
                    }
                }
                else
                {
                    if (dbComp.IsNameUniqueForAdd(CName, Id))
                    {
                        ValidationMessages.Add("Company Already Exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            
        }
        public override bool CanBeDeleted()
        {
            bool bRetValue = false;
            try
            {
                int _rowcount = 0;
                DBDelete dbdelete = new DBDelete();
                _rowcount = dbdelete.GetOverviewDataSelect("masterproduct", "ProdCompID", Id);
                if (_rowcount == 0)
                {
                    _rowcount = dbdelete.GetOverviewDataSelect("linkpartycompany", "CompID", Id);
                    if (_rowcount == 0)
                    {
                        bRetValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return bRetValue;
        }
        #endregion       

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBCompany dbComp = new DBCompany();
            return dbComp.GetOverviewData();
        }
        public DataTable GetOverviewDataForMultiSelection()
        {
            DBCompany dbComp = new DBCompany();
            return dbComp.GetOverviewDataForMultiSelection();
        }
        public DataTable GetCompany()
        {
            DBCompany dbComp = new DBCompany();
            return dbComp.GetCompany();
        }

        public  DataTable CreateTempComp()
        {
            DBCompany dbcomp = new DBCompany();
            return  dbcomp.CreateTempComp();
        }
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBCompany dbComp = new DBCompany();
                drow = dbComp.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["CompId"].ToString();
                    ShortName = Convert.ToString(drow["CompShortName"]);
                    CName = Convert.ToString(drow["CompName"]);
                    Telephone = Convert.ToString(drow["CompTelephone"]);
                    MailID = Convert.ToString(drow["CompMailId"]);
                    ContactPerson = Convert.ToString(drow["CompContactPerson"]);
                    Address = Convert.ToString(drow["CompAddress"]);
                    if (drow["PartyID_1"] != DBNull.Value && drow["PartyID_1"].ToString() != string.Empty)
                        PartyID_1 = drow["PartyID_1"].ToString();
                    if (drow["PartyID_2"] != DBNull.Value && drow["PartyID_2"].ToString() != string.Empty)
                        PartyID_2 = drow["PartyID_2"].ToString();
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public bool AddDetails()
        {
            DBCompany dbComp = new DBCompany();            
            return dbComp.AddDetails(Id, ShortName, CName, Telephone, MailID, ContactPerson, Address,PartyID_1,PartyID_2, CreatedBy, CreatedDate, CreatedTime);
        }
        public bool UpdateProductMasterWithPartyID1()
        {
            DBCompany dbComp = new DBCompany();
            return dbComp.UpdateProductMasterWithPartyID1(Id, PartyID_1);
        }
        public bool UpdateProductMasterWithPartyID2()
        {
            DBCompany dbComp = new DBCompany();
            return dbComp.UpdateProductMasterWithPartyID2(Id, PartyID_2);
        }
        public bool UpdateDetails()
        {
            DBCompany dbComp = new DBCompany();           
            return dbComp.UpdateDetails(Id, ShortName, CName, Telephone, MailID, ContactPerson, Address, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBCompany dbComp = new DBCompany();
            return dbComp.DeleteDetails(Id);
        }

        

        #endregion      


    
        
    }
}
