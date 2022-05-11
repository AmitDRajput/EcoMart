using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
    
{
    class Level:BaseObject
    {

        #region Declaration

        private int _AddLevel;
        private int _DeleteLevel;
        private int _EditLevel;
        private int _ViewLevel;
        private int _PrintLevel;
       
        #endregion

        #region Constructor Destructor
        public Level()
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
        public int AddLevel
        {
            get { return _AddLevel; }
            set { _AddLevel = value; }
        }
        public int DeleteLevel
        {
            get { return _DeleteLevel; }
            set { _DeleteLevel = value; }
        }
        public int EditLevel
        {
            get { return _EditLevel; }
            set { _EditLevel = value; }
        }
        public int ViewLevel
        {
            get { return _ViewLevel; }
            set { _ViewLevel = value; }
        }
        public int PrintLevel
        {
            get { return _PrintLevel; }
            set { _PrintLevel = value; }
        }

        #endregion
        
        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _AddLevel = 0;
                _EditLevel = 0;
                _DeleteLevel = 0;
                _PrintLevel = 0;

                _ViewLevel = 0;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
           
        }
        public override void DoValidate()
        {
         /*   if (_NewBatchNo == "")
                ValidationMessages.Add("Please enter the New Batch No .");
            if (_NewExpiry == "")
                ValidationMessages.Add("Please enter the New Expiry Date.");
            if (_NewMrp == 0)
                ValidationMessages.Add("Please enter the New MRP  .");
            if (_NewPurchRate == 0)
                ValidationMessages.Add("Please enter the New Purchase Rate .");
            if (_NewSaleRate == 0)
                ValidationMessages.Add("Please enter the New Sale Rate .");
            if (_NewQty == 0)
            {

                DialogResult dResult;
                dResult = MessageBox.Show("Do you want to Cancel?", PharmaSYS+Gold", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dResult == DialogResult.Yes)
                {
                }

            }
            */
        }
        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }

        public bool Cancel()
        {
            bool retValue = true;          
            return retValue;
        }

        #endregion

        #region public Methods

        public DataTable GetLevel(string fname)
        {
            DBLevel dbLevel =new DBLevel();
            return dbLevel.GetLevel( fname); 
        }

        #endregion

    }
}
