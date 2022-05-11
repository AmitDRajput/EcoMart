using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Configuration;
using System.Windows.Forms;

namespace EcoMart.Common
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
                //connStr = "DSN=EcoMartConnector";
                connStr = ConfigurationManager.ConnectionStrings["EcoMartConnectionString"].ConnectionString;
                connStr = EcoMartLicenseLib.Common.Decrypt(connStr);
                DBInterface.ConnectionString = connStr;
                //TOREMOVE
                //Log.WriteInfo("ConnectionString=" + connStr);                
                //TOREMOVE
                MessageBox.Show("CONNECTION String: " + DBInterface.ConnectionString);
                DBInterface.Initialize();                

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
    }
}
