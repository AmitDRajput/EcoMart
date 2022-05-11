using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Configuration;

namespace PharmaSYSRetailPlus.Common
{
    public class ConnectionInfo
    {
        public ConnectionInfo()
        {
        }

        public bool IsDBConnected
        {
            get { return DBInterface.IsDbConnected; }
        }

        public void Initialize()
        {
            string connStr;
            try
            {
                //connStr = "DSN=PharmaSysRetailPlusConnector";
                connStr = ConfigurationManager.ConnectionStrings["PharmaSysRetailPlusConnectionString"].ConnectionString;
                connStr = LicenseLib.Common.Decrypt(connStr);
                DBInterface.ConnectionString = connStr;
                DBInterface.Initialize();    
                
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
    }
}
