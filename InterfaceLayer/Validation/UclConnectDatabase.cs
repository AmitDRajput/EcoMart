using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.DataLayer;
using System.Configuration;
using EcoMartLicenseLib;
using EcoMart.Common;
using EcoMart.InterfaceLayer.Classes;

namespace EcoMart.InterfaceLayer.Validation
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclConnectDatabase : UserControl, IValidationControl
    {
        // <!--<add name="PharmaSYSPlusConnectionString" connectionString="DRIVER=MySQL ODBC 3.51 Driver; SERVER=localhost;DATABASE=EcoMart;UID=root;PWD=root" providerName="System.Data.Odbc"/>-->
        //SERVER=localhost;DATABASE=EcoMart1415;UID=root;PASSWORD=root;
        private string conectString = "SERVER={0};DATABASE={1};UID={2};PASSWORD={3};connection Timeout = 28800";
               
        private string _server;
        private string _databasename;
        private string _username;
        private string _password;

        public UclConnectDatabase()
        {
            InitializeComponent();
        }

        #region IValidationControl Members

        public void Initialize()
        {
            this.Location = new Point(10, 10);
            if (OnStateError != null)
            {
            }            
        }

        public event EventHandler OnStateOk;

        public event EventHandler OnStateError;

        #endregion       
        
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            _server = cmbServers.Text;
            _databasename = txtDatabase.Text;
            _username = txtUser.Text;
            _password = txtPassword.Text;
            string connStr = string.Format(conectString, _server, _databasename, _username, _password);
            DBInterface.ConnectionString = connStr;
            DBInterface.Initialize();
            if (DBInterface.IsDbConnected)
            {
                MessageBox.Show("Database connection successful...!");
                //Encrypt conenction string
                connStr = EcoMartLicenseLib.Common.Encrypt(connStr);
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;
                if (section != null)
                {
                    section.ConnectionStrings["EcoMartConnectionString"].ConnectionString = connStr;
                    config.Save();
                    ConfigurationManager.RefreshSection("connectionStrings");
                    //General.RestartService();
                }               
                
                if (OnStateOk != null)
                    OnStateOk(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Failed to connect to Database. Please contact your system administrator...!");
            }
        }
    }
}
