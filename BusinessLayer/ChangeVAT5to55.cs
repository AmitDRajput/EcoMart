using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class ChangeVAT5to55 : BaseObject
    {
        #region Constructors, Destructors
        public ChangeVAT5to55()
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

        public void UpdateProductMaster()
        {
            bool retValue = false;
            DBChangeVAT5to55 dbc = new DBChangeVAT5to55();
            retValue = dbc.UpdateProductMaster();
        }

        public void UpdatetblStock()
        {
            bool retValue = false;
            DBChangeVAT5to55 dbc = new DBChangeVAT5to55();
            retValue = dbc.UpdatetblStock();
        }

        public void UpdateMasterVATPercent()
        {
            bool retValue = false;
            DBChangeVAT5to55 dbc = new DBChangeVAT5to55();
            retValue = dbc.UpdateMasterVATPercent();
        }

        public void UpdateAccNameInmasterAccount()
        {
            bool retValue = false;
            DBChangeVAT5to55 dbc = new DBChangeVAT5to55();
            retValue = dbc.UpdateAccNameInmasterAccount();
        }
    }
}
