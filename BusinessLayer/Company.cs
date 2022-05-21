using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;
using System.Collections;

namespace EcoMart.BusinessLayer
{
    public class Company : BaseObject
    {

        #region Declaration
        private string _CName;   
        //private string _Telephone;
        //private string _MailID;
        private string _ContactPerson;
        private string _Address;
        private string _ShortName;
        private string _PartyID_1;
        private string _PartyID_2;
        private string _PartyID_3; //Amar
        private string _PartyID_4; //Amar
  
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
        //public string Telephone
        //{
        //    get { return _Telephone; }
        //    set { _Telephone = value; }
        //}
        //public string MailID
        //{
        //    get { return _MailID; }
        //    set { _MailID = value; }
        //}
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

        public string PartyID_3  //Amar
        {
            get { return _PartyID_3; }
            set { _PartyID_3 = value; }
        }
        public string PartyID_4  //Amar
        {
            get { return _PartyID_4; }
            set { _PartyID_4 = value; }
        }       

        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            _CName = "";
            _ShortName = "";
            //_Telephone = "";
            //_MailID = "";
            _ContactPerson = "";
            _Address = "";
            _PartyID_1 = "";
            _PartyID_2 = "";
            _PartyID_3 = "";
            _PartyID_4 = "";
            
            
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
                    if (dbComp.IsNameUniqueForEdit(CName, ShortName))
                    {
                        ValidationMessages.Add("Company Already Exists.");
                    }
                }
                else
                {
                    if (dbComp.IsNameUniqueForAdd(CName, ShortName))
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
            IntID = Convert.ToInt32(Id);
            try
            {
                DBCompany dbComp = new DBCompany();
                drow = dbComp.ReadDetailsByID(IntID);

                if (drow != null)
                {
                    Id = drow["CompId"].ToString();
                    IntID = Convert.ToInt32(drow["CompId"].ToString());
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

                    //Amar
                    if (drow["PartyID_3"] != DBNull.Value && drow["PartyID_3"].ToString() != string.Empty)
                        PartyID_3 = drow["PartyID_3"].ToString();
                    if (drow["PartyID_4"] != DBNull.Value && drow["PartyID_4"].ToString() != string.Empty)
                        PartyID_4 = drow["PartyID_4"].ToString(); //end
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public DataRow ReadDetailsByID(string ID)
        {
            DataRow drow = null;
            DBCompany dbComp = new DBCompany();
            drow = dbComp.ReadDetailsByID(IntID );
            return drow;
        }

        public int AddDetails()
        {
            DBCompany dbComp = new DBCompany();            
            return dbComp.AddDetails( IntID , ShortName, CName, Telephone, MailID, ContactPerson, Address,PartyID_1,PartyID_2, PartyID_3, PartyID_4, CreatedBy, CreatedDate, CreatedTime);
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
        public bool UpdateProductMasterWithPartyID3()  //Amar
        {
            DBCompany dbComp = new DBCompany();
            return dbComp.UpdateProductMasterWithPartyID3(Id, PartyID_3);
        }
        public bool UpdateProductMasterWithPartyID4()  //Amar
        {
            DBCompany dbComp = new DBCompany();
            return dbComp.UpdateProductMasterWithPartyID4(Id, PartyID_4);
        }
        public bool UpdateDetails() // [ansuman][15.11.2016]
        { 
            DBCompany dbComp = new DBCompany();           
            return dbComp.UpdateDetails(Id, ShortName, CName, Telephone, MailID, ContactPerson, Address, PartyID_1, PartyID_2, PartyID_3, PartyID_4, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBCompany dbComp = new DBCompany();
            return dbComp.DeleteDetails(Id);
        }
        public Int32 GetIntID()
        {
            
            DBCompany dbcomp = new DBCompany();
            DataRow idrow = dbcomp.GetMaxID();
            int maxid = Convert.ToInt32 ( idrow["maxcompid"]) + 1;
            return maxid;

        }
        #endregion      

      

        //public Hashtable GetTableListByCode(string IDName)
        //{
        //    Hashtable compList = new Hashtable();
        //    try
        //    {
        //        DBCompany dbc = new DBCompany();
        //        DataTable dtable = dbc.GetTableListByCode(IDName);
        //        int rowcount = 0;
        //        if (dtable != null && dtable.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dtable.Rows)
        //            {
        //                rowcount += 1;
        //                compList.Add(rowcount, dr["CompID"].ToString());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //    return compList;
        //}
    }
}
