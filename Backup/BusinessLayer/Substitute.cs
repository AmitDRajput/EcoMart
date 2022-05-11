using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class Substitute : BaseObject
    {
        # region Declaration
        private string _ProductId;
        private string _ProductName;
        private string _SubstituteName;
        private string _SubstituteId;
        private string _DrugId;
        # endregion

        #region Constructors, Destructors
        public Substitute()
        {
            Initialise();
        }
        #endregion

        #region Properties
        public string ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
        }
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        public string SubstituteName
        {
            get { return _SubstituteName; }
            set { _SubstituteName = value; }
        }
        public string SubstituteId
        {
            get { return _SubstituteId; }
            set { _SubstituteId = value; }
        }

        public string DrugId
        {
            get { return _DrugId; }
            set { _DrugId = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _ProductId = "";
                _ProductName = "";
                _SubstituteName = "";
                _SubstituteId = "";
                _DrugId = "";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override void DoValidate()
        {
            
        }
        #endregion

       

        #region Public Methods
        public DataTable GetOverviewDataByDrugCode(string drugCode)
        {
            DBSubstitute dbSubstitute = new DBSubstitute();
            return dbSubstitute.GetOverviewDataByDrugCode(drugCode);
        }      

        public DataTable GetProduct()
        {
            DBSubstitute dbSubstitute = new DBSubstitute();
            return dbSubstitute.GetProduct();
        }
      
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBSubstitute dbSubstitute = new DBSubstitute();
                drow = dbSubstitute.ReadDetailsByDrugID(DrugId);

                if (drow != null)
                {
                    Id = drow["AccountId"].ToString();
                    ProductName = Convert.ToString(drow["AccName"]);
                    retValue = true;
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
