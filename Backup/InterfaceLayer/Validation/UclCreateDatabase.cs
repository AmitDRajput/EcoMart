using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.DataLayer;
using System.Configuration;
using LicenseLib;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.InterfaceLayer.Classes;
using System.Data.Odbc;
using System.Collections;

namespace PharmaSYSRetailPlus.InterfaceLayer.Validation
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclCreateDatabase : UserControl, IValidationControl
    {
        // <!--<add name="PharmaSYSPlusConnectionString" connectionString="DRIVER=MySQL ODBC 3.51 Driver; SERVER=localhost;DATABASE=pharmasysretailplus;UID=root;PWD=root" providerName="System.Data.Odbc"/>-->
        //SERVER=localhost;DATABASE=pharmasysretailplus1415;UID=root;PASSWORD=root;
        private string conectString = "SERVER={1};DATABASE={2};UID={3};PWD={4}";
            
        private string _server;
        private string _databasename;
        private string _username;
        private string _password;

        public UclCreateDatabase()
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

        private bool CreateDatabase()
        {
            bool retValue = false;        
            _server = cmbServers.Text;
            _databasename = txtDatabase.Text;
            _username = txtUser.Text;
            _password = txtPassword.Text;

            string connStr = string.Format(conectString, _server, "", _username, _password);
            OdbcConnection connection = new OdbcConnection(connStr);

            try
            {
                connection.Open();
                OdbcCommand command = new OdbcCommand("create Database " + _databasename, connection);
                command.ExecuteNonQuery();                
                retValue = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on ConnectDatabase...!" + ex.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
            return retValue;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;           
            try
            {
                if (CreateDatabase())
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
                        //Encrypt conenction string
                        connStr = LicenseLib.Common.Encrypt(connStr);
                        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;
                        if (section != null)
                        {
                            section.ConnectionStrings["PharmaSysRetailPlusConnectionString"].ConnectionString = connStr;
                            config.Save();
                            ConfigurationManager.RefreshSection("connectionStrings");
                            General.RestartService();
                        } 
                            
                        connStr = string.Format(conectString, _server, _databasename, _username, _password);
                        bool isScriptRunSuccess = RunDefaultScript(connStr, _databasename);

                        if (isScriptRunSuccess)
                        {
                            MessageBox.Show("Database Created Successful...!", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (OnStateOk != null)
                                OnStateOk(this, new EventArgs());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to connect to Database. Please contact your system administrator...!");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            this.Cursor = Cursors.Default;
        }

        private bool RunDefaultScript(string conString, string database)
        {
            bool retValue = true;
            try
            {
                ScriptExecutor executor = new ScriptExecutor(conString, database, 0, 1);

                ArrayList fileList = executor.GetUpgradationFiles();
                fileList.Sort();
                foreach (string file in fileList)
                {
                    executor.executeUpgradeScriptFile(file);
                    if (executor.IsSuccess == false)
                    {
                        MessageBox.Show("Failed to execute default script. Please contact your system administrator...!");
                        retValue = false;
                        break;
                    }
                }               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
       
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {               
                _server = cmbServers.Text;               
                _username = txtUser.Text;
                _password = txtPassword.Text;

                string connStr = string.Format(conectString, _server, "", _username, _password);
                DBInterface.ConnectionString = connStr;
                DBInterface.Initialize();
                if (DBInterface.IsDbConnected)
                {
                    MessageBox.Show("Connection Test Successful...!", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCreate.Enabled = true;
                    btnTestConnection.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Connection Test Failed...!", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCreate.Enabled = false;
                    btnTestConnection.Enabled = true;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on ConnectDatabase...!" + ex.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
