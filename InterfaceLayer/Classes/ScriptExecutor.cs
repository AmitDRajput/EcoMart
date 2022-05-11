using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Data.Odbc;
using EcoMart.Common;
using System.Data.SqlClient;

namespace EcoMart.InterfaceLayer.Classes
{
    public class ScriptExecutor
    {        
        private string _connectionString;
        private string _database;

        Assembly asm;
        private int _frmVersion = 0;
        private int _uptoVersion = 1; 

        private bool _isSuccess = true;

        public ScriptExecutor(string connString, string db, int versionFrom, int versionTo)
        {
            _connectionString = connString;
            _database = db;
            _frmVersion = versionFrom;
            _uptoVersion = versionTo;
        }

        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; }
        }        

        public ArrayList GetUpgradationFiles()
        {
            int frmVersion;
            int uptoVersion;
            ArrayList fileList = new ArrayList();
            string dbType;
            try
            {
                dbType = "MySQL";
                dbType = dbType.TrimEnd('C', 'o', 'n', 'n', 'e', 'c', 't', 'o', 'r');
                asm = Assembly.GetExecutingAssembly();
                string[] resourceList = asm.GetManifestResourceNames();
                foreach (string resource in resourceList)
                {
                    if (resource.Contains(dbType))
                    {
                        parseFile(resource, out frmVersion, out uptoVersion);
                        if (frmVersion >= _frmVersion && uptoVersion <= _uptoVersion)
                        {
                            fileList.Add(resource);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(" DatabaseUpgrader.GetUpgradationFiles() >> " + ex.Message);
            }
            return fileList;
        }

        public void executeUpgradeScriptFile(string file)
        {
            try
            {
                ArrayList commandList = GetCommand(file);
                foreach (string command in commandList)
                {
                    if (command.Contains("#include"))
                    {
                        string temp = command.Remove(0, 8);  //removing '#include' from command string
                        temp = temp.Trim();
                        temp = temp.TrimEnd(';');
                        executeUpgradeScriptFile(temp);
                    }
                    else
                    {
                        _isSuccess = ExecuteDDLCommand(command);

                        if (_isSuccess == false)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(" UserControlUpgradation.executeUpgradeScriptFile >> " + ex.Message);
            }
        }

        private void parseFile(string fileName, out int frmVersion, out int uptoVersion)
        {
            frmVersion = -1;
            uptoVersion = -1;
            int num = -1;
            char[] cFileName;
            try
            {
                cFileName = fileName.ToCharArray();
                foreach (char c in cFileName)
                {
                    if (c <= 57 && c >= 48)
                    {
                        if (num == -1)
                            num = 0;
                        int temp = Convert.ToInt32(c);
                        temp = temp % 48;
                        num = (num * 10) + temp;
                    }
                    else
                    {
                        if (frmVersion == -1)
                            frmVersion = num;
                        else
                        {
                            uptoVersion = num;
                            break;
                        }
                        num = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(" DatabaseUpgrader.parseFile() >> " + ex.Message);
            }
        }              

        private ArrayList GetCommand(string fileName)
        {
            ArrayList commandList = new ArrayList();
            Assembly asm;
            Stream stream = null;
            StreamReader reader = null;
            string line;
            string command = "";
            try
            {
                asm = Assembly.GetExecutingAssembly();
                stream = asm.GetManifestResourceStream(fileName);
                reader = new StreamReader(stream);
                line = reader.ReadLine();
                while (line != null)
                {
                    if (IsValidLine(line))
                    {
                        if (line.Contains("--") == false)
                        {
                            command = string.Concat(command, line);
                            if (line.EndsWith(";"))
                            {
                                commandList.Add(command);
                                command = "";
                            }
                        }
                    }
                    line = reader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(" DatabaseUpgrader.GetCommand() >> " + ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (reader != null)
                    reader.Close();
            }
            return commandList;
        }

        private bool IsValidLine(string line)
        {
            bool retValue = true;
            try
            {
                if (line == "")
                    retValue = false;
                else if (line.StartsWith("#"))
                    retValue = false;
                else if (line.StartsWith("/*"))
                    retValue = false;
            }
            catch (Exception ex)
            {
                Log.WriteError(" DatabaseUpgrader.IsValidLine() >> " + ex.Message);
            }
            return retValue;
        }

        private bool ExecuteDDLCommand(string command)
        {
            try
            {
                string usedb = string.Format("Use {0};", _database);
                executeOdbc(usedb);
                _isSuccess = executeOdbc(command);
            }
            catch (Exception ex)
            {
                Log.WriteError(" DatabaseUpgrader.ExecuteDDLCommand() >> " + ex.Message);
            }
            return _isSuccess;
        }

        private bool executeOdbc(string cmd)
        {
            SqlConnection _Connection = null;
            SqlCommand _Command = null;
            bool isSuccess = false;

            try
            {
                _Connection = new SqlConnection(_connectionString);
                try
                {
                    _Command = new SqlCommand();
                    _Command.CommandText = cmd;

                    _Connection.Open();

                    _Command.Connection = _Connection;
                    int iReturn = _Command.ExecuteNonQuery();

                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    Log.WriteError(" DatabaseUpgrader.executeOdbc() >> " + ex.Message);
                }
                finally
                {
                    if (_Command != null)
                    {                      
                        _Command.Dispose();
                    }
                    if (_Connection != null)
                    {
                        _Connection.Close();
                        _Connection.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteError(" DatabaseUpgrader.executeOdbc() >> " + ex.Message);
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}
