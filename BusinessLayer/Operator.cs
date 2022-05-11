using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;
using EcoMart.DataLayer;

namespace EcoMart.BusinessLayer
{
    public class Operator : BaseObject
    {
        #region Declaration      
        private string _Password;
        private string _RetypePassword;
        private string _Details;
        private int _IfInUse;
        #endregion

        #region Constructor Destructor
        public Operator()
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
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string RetypePassword
        {
            get { return _RetypePassword; }
            set { _RetypePassword = value; }
        }

        public int IfInUse
        {
            get { return _IfInUse; }
            set { _IfInUse = value; }
        }

        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();              
                _Password = null;
                _RetypePassword = null;
                _Details = null;
                _IfInUse = 0;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        #region  Validations

        public override bool CanBeDeleted()
        {
            bool bRetValue = false;
            try
            {
                int _rowcount = 0;
                DBDelete dbdelete = new DBDelete();
                _rowcount = dbdelete.GetOverviewDataSelect("vouchersale", "OperatorID", Id);
                if (_rowcount == 0)
                {
                    _rowcount = dbdelete.GetOverviewDataSelect("vouchercorrectioninrate", "OperatorID", Id);
                    if (_rowcount == 0)
                    {
                        _rowcount = dbdelete.GetOverviewDataSelect("voucherpurchase", "OperatorID", Id);
                        if (_rowcount == 0)
                        {
                            _rowcount = dbdelete.GetOverviewDataSelect("voucheropstock", "OperatorID", Id);
                            if (_rowcount == 0)
                            {
                                _rowcount = dbdelete.GetOverviewDataSelect("vouchercreditdebitnote", "OperatorID", Id);
                                if (_rowcount == 0)
                                    bRetValue = true;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return bRetValue;
        }



        public override void DoValidate()
        {
            try
            {
                if (Name == "")
                    ValidationMessages.Add("Please enter the  Operator Name.");
                if (_Password == "")
                    ValidationMessages.Add("Please enter the  Password.");               
                if (_Details == "")
                    ValidationMessages.Add("Enter the Details");
                if (Name != "")
                {
                    DBOperator dbAddOP = new DBOperator();
                    if (IFEdit == "Y")
                    {
                        if (dbAddOP.IsNameUniqueForEdit(Name, Id))
                        {
                            ValidationMessages.Add("Operator Already Exists.");
                        }
                    }
                    else
                    {
                        if (dbAddOP.IsNameUniqueForAdd(Name, Id))
                        {
                            ValidationMessages.Add("Operator Already Exists.");
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

        #region Public Method
        public bool AddDetails()
        {
            DBOperator dbAddOP = new DBOperator();
            return dbAddOP.AddDetails(Id, Name, Password, IfInUse, Description, CreatedBy, CreatedDate, CreatedTime);

        }
        public bool UpdateDetails()
        {
            DBOperator dbAddOP = new DBOperator();
            return dbAddOP.UpdateDetails(Id, Name, Password, IfInUse, Description, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public DataTable GetOverviewData()
        {
            DBOperator dbAddOP = new DBOperator();
            return dbAddOP.GetOverviewData();
        }
       
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBOperator dbAddOP = new DBOperator();
                drow = dbAddOP.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["OperatorID"].ToString();
                    Name = Convert.ToString(drow["OperatorName"]);
                    Password = Convert.ToString(drow["Password"]);
                    RetypePassword = Convert.ToString(drow["Password"]);
                    IfInUse = Convert.ToInt16(drow["IfInUse"]);
                    Description = Convert.ToString(drow["OperatorDetails"]);
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public bool DeleteDetails()
        {
            DBOperator dbAddOP = new DBOperator();
            return dbAddOP.DeleteDetails(Id, IfInUse);
        }

        public Operator GetOperatorByOperatorNameAndPassword(string userName, string password)
        {
            Operator retValue = null;
            try
            {
                DBOperator dbAddOP = new DBOperator();
                DataTable dTable = dbAddOP.GetOperatorByOperatorNameAndPassword(userName, password);
                if (dTable != null && dTable.Rows.Count > 0)
                {
                    retValue = new Operator();
                    DataRow drow = dTable.Rows[0];
                    retValue.Id = drow["OperatorID"].ToString();
                    retValue.Name = Convert.ToString(drow["OperatorName"]);
                    retValue.Password = Convert.ToString(drow["Password"]);
                    retValue.IfInUse = Convert.ToInt16(drow["IfInUse"]);
                    retValue.Description = Convert.ToString(drow["OperatorDetails"]);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        #endregion


    }
}
