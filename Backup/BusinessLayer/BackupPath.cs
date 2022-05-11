using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.BusinessLayer;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class BackupPath : BaseObject
    {
        #region Declaration
        public string BackupPath1;
        public string BackupPath2;
        public string BackupPath3;
        #endregion Declaration


        #region Constructors, Destructors
        public BackupPath()
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
        #endregion Constructors, Destructors

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                BackupPath1 = "";
                BackupPath2 = "";
                BackupPath3 = "";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        #region Public Methods

        public void ReadBackupPath()
        {
            try
            {
                DataRow dr;
                DBBackupPath dbp = new DBBackupPath();
                dr = dbp.ReadDetails(General.ShopDetail.ShopVoucherSeries);
                if (dr != null)
                {
                    if (dr["BackupPath1"] != DBNull.Value)
                        BackupPath1 = dr["BackupPath1"].ToString();
                    if (dr["BackupPath2"] != DBNull.Value)
                        BackupPath2 = dr["BackupPath2"].ToString();
                    if (dr["BackupPath3"] != DBNull.Value)
                        BackupPath3 = dr["BackupPath3"].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public bool UpdateDetails()
        {
            DBBackupPath dbp = new DBBackupPath();
            return dbp.UpdateDetails(General.BackupPath.BackupPath1, General.BackupPath.BackupPath2, General.BackupPath.BackupPath3, General.ShopDetail.ShopVoucherSeries);
        }


        #endregion
        
    }
}
