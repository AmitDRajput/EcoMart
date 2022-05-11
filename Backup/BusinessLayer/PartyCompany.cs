using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class PartyCompany : BaseObject
    {
        # region Declaration
        private string _PartyId;
        private string _PartyName;
        private string _CompanyName;
        private string _CompanyId;
        private string _CurrentPartyId;
        # endregion

        #region Constructors, Destructors
        public PartyCompany()
        {
            Initialise();
        }
        #endregion

        #region Properties
        public string PartyId
        {
            get { return _PartyId; }
            set { _PartyId = value; }
        }
        public string PartyName
        {
            get { return _PartyName; }
            set { _PartyName = value; }
        }
        public string ProductName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        public string CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }
        public string CurrentPartyId
        {
            get { return _CurrentPartyId; }
            set { _CurrentPartyId = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _PartyId = "";
                _PartyName = "";
                _CompanyName = "";
                _CompanyId = "";
                _CurrentPartyId = "";
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
                DBPartyCompany dbPartyCompany = new DBPartyCompany();
                if (Id == "" || Id == null)
                    ValidationMessages.Add("Please select the party.");
                int companyCount;
                int.TryParse(_CompanyId, out companyCount);
                if (companyCount == 0)
                    ValidationMessages.Add("Please add atleast one company.");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }
        #endregion

        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBPartyCompany dbPartyCompany = new DBPartyCompany();
            return dbPartyCompany.GetOverviewData();
        }

        public DataTable GetOverviewDataY()
        {
            DBPartyCompany dbPartyCompany = new DBPartyCompany();
            return dbPartyCompany.GetOverviewDataY();
        }

        public DataTable GetParty()
        {
            DBPartyCompany dbPartyCompany = new DBPartyCompany();
            return dbPartyCompany.GetParty();
        }

        public DataTable GetOverviewCompanyData(string AccCode)
        {

            DBPartyCompany dbPartyCompany = new DBPartyCompany();
            return dbPartyCompany.GetOverviewCompanyData(AccCode);
        }

        public DataTable GetOverviewCompanyDataY(string CompCode)
        {

            DBPartyCompany dbPartyCompany = new DBPartyCompany();
            return dbPartyCompany.GetOverviewCompanyDataY(CompCode);
        }
    
        public DataTable IsPartyAlreadyLinked(string Id)
        {
            DBPartyCompany dbpc = new DBPartyCompany();
            return dbpc.IsPartyAlreadyLinked(Id);

        }
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBPartyCompany dbPartyCompany = new DBPartyCompany();
                drow = dbPartyCompany.ReadDetailsByIDParty(Id);

                if (drow != null)
                {
                    Id = drow["AccountId"].ToString();
                    PartyName = Convert.ToString(drow["AccName"]);
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
            DBPartyCompany dbPartyCompany = new DBPartyCompany();
            return dbPartyCompany.AddDetails(Id,DetailId,  CompanyId, CreatedBy, CreatedDate, CreatedTime, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBPartyCompany dbPartyCompany = new DBPartyCompany();
            return dbPartyCompany.DeleteDetails(Id);
        }

        #endregion

    }
}


