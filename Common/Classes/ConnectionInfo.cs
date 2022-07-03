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
    
    public class AzureConnectionInfo
    {
        public AzureConnectionInfo()
        {
        }

        public bool IsDBConnected
        {
            get { return AzureDBInterface.IsDbConnected; }
        }

        public void Initialize()
        {
            string connStr;
            try
            {
                //connStr = "DSN=EcoMartConnector";
                connStr = ConfigurationManager.ConnectionStrings["EcoMartAzureConnectionString"].ConnectionString;
                connStr = EcoMartLicenseLib.Common.Decrypt(connStr);
                AzureDBInterface.ConnectionString = connStr;
               //MessageBox.Show("CONNECTION String: " + DBInterface.ConnectionString);
                AzureDBInterface.Initialize();

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }


        private bool ConnectData(string VoucherSeries)
        {
            bool retValue = false;
            try
            {

                string conectString = "SERVER={0};DATABASE={1};UID={2};PASSWORD={3}";
                string connStrCurrentYear = ConfigurationManager.ConnectionStrings["EcoMartConnectionString"].ConnectionString;
                connStrCurrentYear = EcoMartLicenseLib.Common.Decrypt(connStrCurrentYear);

                System.Data.Odbc.OdbcConnectionStringBuilder builder = new System.Data.Odbc.OdbcConnectionStringBuilder(connStrCurrentYear);
                if (builder == null)
                    return false;

                DBInterface.Initialize();

                string _server = builder["server"].ToString();
                //string _databasename = _oldyeardatabase;
                string _username = builder["uid"].ToString();

                string _password = builder["password"].ToString();
                //string newconnStr = string.Format(conectString, _server, _databasename, _username, _password);

                if (DBInterface.IsDbConnected)
                {
                    //CopyCompleteFolder(VoucherSeries, newconnStr);
                    retValue = true;
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private bool ConnectDataNewDataBase(string VoucherSeries)
        {
            bool retValue = false;
            try
            {

                string conectString = "SERVER={0};DATABASE={1};UID={2};PASSWORD={3};default command timeout = 20";
                string connStrOld = ConfigurationManager.ConnectionStrings["EcoMartConnectionString"].ConnectionString;
                connStrOld = EcoMartLicenseLib.Common.Decrypt(connStrOld);

                System.Data.Odbc.OdbcConnectionStringBuilder builder = new System.Data.Odbc.OdbcConnectionStringBuilder(connStrOld);
                if (builder == null)
                    return false;

                string _server = builder["server"].ToString();
                //string _databasename = _oldyeardatabase;
                string _username = builder["uid"].ToString();

                // 11/9/2015 instead of pwd  password
                string _password = builder["password"].ToString();
                //string connStr = string.Format(conectString, _server, _databasename, _username, _password);

                //DBInterface.ConnectionString = connStr;
                DBInterface.Initialize();
                if (DBInterface.IsDbConnected)
                {
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
    }
}
