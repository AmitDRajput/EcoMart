using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using PharmaSYSRetailPlus.Common;
using System.Data.Odbc;
using MySql.Data.MySqlClient;

namespace PharmaSYSRetailPlus.DataLayer
{
    internal static class DBInterface
    {
        #region Declaration
        private static string _ConnectionString;
        private static bool _IsDbConnected;
        //private static MySqlCommand _Command;
        private static MySqlConnection _Connection;
        //private static DbProviderFactory _DBFactory;
        private static MySqlTransaction _Transaction = null;
        #endregion 

        #region Property(s)
        internal static string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        internal static bool IsDbConnected
        {
            get { return _IsDbConnected; }
        }
        #endregion Property(s)

        #region Internal Method(s)
        internal static void Initialize()
        {
          //  string strProviderString = "";
            try
            {
                _IsDbConnected = false;
                if (ConnectionString.Length > 0)
                {
                    //strProviderString = GetProviderString();
                    //_DBFactory = DbProviderFactories.GetFactory(strProviderString);
                    _Connection = new MySqlConnection(ConnectionString);
                    //_Connection.ConnectionString = ConnectionString;
                    //MySqlCommand _Command = new MySqlCommand();
                    //_Command.Connection = _Connection;
                    //_Command.Transaction = _Transaction;
                    OpenConnection();
                    _IsDbConnected = true;
                    CloseConnection(); 
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                _IsDbConnected = false;
                CloseConnection();
            }
        }

        internal static DataSet SelectDataSet(string strQuery)
        {
            DataSet dataSet = null;
            MySqlDataAdapter adapter;

            try
            {
                if (IsDbConnected)
                {
                    dataSet = new DataSet();
                    adapter = new MySqlDataAdapter();

                    MySqlCommand _Command = new MySqlCommand();
                    _Command.CommandText = strQuery;

                    OpenConnection();

                    _Command.Connection = _Connection;
                    _Command.Transaction = _Transaction;
                    adapter.SelectCommand = _Command;
                    // ss 12/5/2014
                    if (dataSet != null )
     
                    adapter.Fill(dataSet);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                dataSet = null;
                CloseConnection();
            }
            return dataSet;
        }

        internal static DataTable SelectDataTable(string strQuery)
        {
            DataSet dataSet = null;
            DataTable table = null;
            try
            {
                dataSet = SelectDataSet(strQuery);
                // ss 12/5/2014
                if (dataSet != null && dataSet.Tables.Count > 0) 
                    table = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                Log.WriteError("DBInterface.SelectDataTable>> " + ex.Message);
            }
            return table;
        }

        internal static DataRow SelectFirstRow(string strQuery)
        {
            DataSet dataSet = null;
            DataRow dataRow = null;
            try
            {
                dataSet = SelectDataSet(strQuery);
                // ss 12/11/2013 check for null
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                        dataRow = dataSet.Tables[0].Rows[0];
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("DBInterface.SelectFirstRow >> " + ex.Message);
            }
            return dataRow;
        }

        internal static int ExecuteQuery(string strSql)
        {
            int iReturnValue = 0;
            try
            {
                if (IsDbConnected)
                {
                    MySqlCommand _Command = new MySqlCommand();
                    _Command.CommandText = strSql;

                    OpenConnection();

                    _Command.Connection = _Connection;
                    _Command.Transaction = _Transaction;
                    iReturnValue = _Command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                //CloseConnection();
            }
            return iReturnValue;
        }

        //internal static int AppendChunk(string tableName, string where, string fieldName, byte[] chunk)
        //{
        //    string strsql = "";
        //    int retValue = 0;
        //    try
        //    {
        //        if (_IsDbConnected)
        //        {                   
        //            strsql = "UPDATE " + tableName + " SET " + fieldName + "=@param1 WHERE " + where;

        //            if (strsql != "")
        //            {
        //                DbParameter par = _Command.CreateParameter();
        //                par.ParameterName = "@param1";
        //                par.DbType = DbType.Binary;
        //                par.Value = chunk;
        //                par.Size = chunk.Length;
                             
        //                _Command = _DBFactory.CreateCommand();
        //                _Command.Connection = _Connection;
        //                _Command.CommandType = CommandType.Text;
        //                _Command.CommandText = strsql;
        //                _Command.Parameters.Add(par);
        //                _Command.Transaction = _Transaction;
        //                retValue = _Command.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteError("DBInterface.AppendChunk >> " + ex.Message);
        //    }
        //    return retValue;
        //}

        //public static int AddPrescriptionData(string scanPrescriptionId,  byte[] prescriptionData, string fileExtention, string saleBillId)
        //{
        //    int retValue = 0;
        //    System.Data.Odbc.OdbcConnection _objConn = null;  
        //    try
        //    {
        //        _objConn = new OdbcConnection(_ConnectionString);  

        //        string strSql = "SELECT * FROM tblscanprescriptions";
        //        DataSet ds = new DataSet("Image");
        //        System.Data.Odbc.OdbcDataAdapter tempAP = new System.Data.Odbc.OdbcDataAdapter(strSql, _objConn);
        //        OdbcCommandBuilder objCommand = new OdbcCommandBuilder(tempAP);
        //        tempAP.Fill(ds, "Table");

        //        _objConn.Open();
        //        DataRow objNewRow = ds.Tables["Table"].NewRow();

        //        objNewRow["ScanPrescriptionID"] = scanPrescriptionId;               
        //        objNewRow["PrescriptionData"] = prescriptionData;
        //        objNewRow["FileExtension"] = fileExtention;
        //        objNewRow["SaleBillID"] = saleBillId;

        //        ds.Tables["Table"].Rows.Add(objNewRow);
        //        // trying to update the table to add the image
        //        tempAP.Update(ds, "Table");
        //    }
        //    catch (Exception ex) 
        //    {
        //        Log.WriteException(ex);
        //    }
        //    finally 
        //    {
        //        if (_objConn != null)
        //            _objConn.Close();
        //    }
        //    return retValue;
        //}

        //TODEL -> Move to dbSalePrescription
        //public static byte[] ReadPrescriptionData(string saleBillId)
        //{
        //    try
        //    {
        //        DataRow dr = null;
        //        string strSql = "Select * from tblsaleprescriptions WHERE SaleBillID='" + saleBillId + "'";
        //        dr = DBInterface.SelectFirstRow(strSql);
        //        if (dr != null)
        //        {
        //            return (byte[])dr["PrescriptionData"];
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //    return null;
        //}

        internal static void BeginTransaction()
        {
            try
            {
                if (_Connection != null)
                {
                    _Transaction = _Connection.BeginTransaction();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);                
                RollbackTransaction();
            }
        }

        internal static void CommitTransaction()
        {
            try
            {
                if (_Connection != null && _Transaction != null)
                {
                    _Transaction.Commit();
                    _Transaction = null;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);                
                //RollbackTransaction();
            }
        }

        internal static void RollbackTransaction()
        {
            try
            {
                 if (_Connection.State == ConnectionState.Closed)
                _Connection.Open();
                if (_Connection != null && _Transaction != null)
                {
                    _Transaction.Rollback();
                    _Transaction = null;
                }
                
                 
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);                
            }
        }

        public static void Dispose()
        {
            CloseConnection();
            //_Command = null;
            _Connection = null;
        }
        #endregion 

        #region Private Method(s)
        private static void OpenConnection()
        {
            if (_Connection.State == ConnectionState.Closed)
                _Connection.Open();
        }

        private static void CloseConnection()
        {
            if (_Connection.State == ConnectionState.Open)
                _Connection.Close();
        }

        //private static string GetProviderString()
        //{            
        //    string providerString = "System.Data.Odbc";
        //    return providerString;
        //}
        #endregion Private Method(s)
    }
    /*
    internal static class DBInterface
    {
        #region Declaration
        private static string _ConnectionString;
        private static bool _IsDbConnected;
        private static DbCommand _Command;
        private static DbConnection _Connection;
        private static DbProviderFactory _DBFactory;
        private static DbTransaction _Transaction = null;
        #endregion

        #region Property(s)
        internal static string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        internal static bool IsDbConnected
        {
            get { return _IsDbConnected; }
        }
        #endregion Property(s)

        #region Internal Method(s)
        internal static void Initialize()
        {
            string strProviderString = "";
            try
            {
                _IsDbConnected = false;
                if (ConnectionString.Length > 0)
                {
                    strProviderString = GetProviderString();
                    _DBFactory = DbProviderFactories.GetFactory(strProviderString);
                    _Connection = _DBFactory.CreateConnection();
                    _Connection.ConnectionString = ConnectionString;
                    _Command = _DBFactory.CreateCommand();
                    _Command.Connection = _Connection;
                    _Command.Transaction = _Transaction;
                    OpenConnection();
                    _IsDbConnected = true;
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                _IsDbConnected = false;
                CloseConnection();
            }
        }

        internal static DataSet SelectDataSet(string strQuery)
        {
            DataSet dataSet = null;
            DbDataAdapter adapter;

            try
            {
                if (IsDbConnected)
                {
                    dataSet = new DataSet();
                    adapter = _DBFactory.CreateDataAdapter();

                    _Command = _DBFactory.CreateCommand();
                    _Command.CommandText = strQuery;

                    OpenConnection();

                    _Command.Connection = _Connection;
                    _Command.Transaction = _Transaction;
                    adapter.SelectCommand = _Command;
                    // ss 12/5/2014
                    if (dataSet != null)

                        adapter.Fill(dataSet);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                dataSet = null;
                CloseConnection();
            }
            return dataSet;
        }

        internal static DataTable SelectDataTable(string strQuery)
        {
            DataSet dataSet = null;
            DataTable table = null;
            try
            {
                dataSet = SelectDataSet(strQuery);
                // ss 12/5/2014
                if (dataSet != null && dataSet.Tables.Count > 0)
                    table = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                Log.WriteError("DBInterface.SelectDataTable>> " + ex.Message);
            }
            return table;
        }

        internal static DataRow SelectFirstRow(string strQuery)
        {
            DataSet dataSet = null;
            DataRow dataRow = null;
            try
            {
                dataSet = SelectDataSet(strQuery);
                // ss 12/11/2013 check for null
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                        dataRow = dataSet.Tables[0].Rows[0];
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("DBInterface.SelectFirstRow >> " + ex.Message);
            }
            return dataRow;
        }

        internal static int ExecuteQuery(string strSql)
        {
            int iReturnValue = 0;
            try
            {
                if (IsDbConnected)
                {
                    _Command = _DBFactory.CreateCommand();
                    _Command.CommandText = strSql;

                    OpenConnection();

                    _Command.Connection = _Connection;
                    _Command.Transaction = _Transaction;
                    iReturnValue = _Command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                CloseConnection();
            }
            return iReturnValue;
        }

        internal static int AppendChunk(string tableName, string where, string fieldName, byte[] chunk)
        {
            string strsql = "";
            int retValue = 0;
            try
            {
                if (_IsDbConnected)
                {
                    strsql = "UPDATE " + tableName + " SET " + fieldName + "=@param1 WHERE " + where;

                    if (strsql != "")
                    {
                        DbParameter par = _Command.CreateParameter();
                        par.ParameterName = "@param1";
                        par.DbType = DbType.Binary;
                        par.Value = chunk;
                        par.Size = chunk.Length;

                        _Command = _DBFactory.CreateCommand();
                        _Command.Connection = _Connection;
                        _Command.CommandType = CommandType.Text;
                        _Command.CommandText = strsql;
                        _Command.Parameters.Add(par);
                        _Command.Transaction = _Transaction;
                        retValue = _Command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("DBInterface.AppendChunk >> " + ex.Message);
            }
            return retValue;
        }

        public static int AddPrescriptionData(string scanPrescriptionId, byte[] prescriptionData, string fileExtention, string saleBillId)
        {
            int retValue = 0;
            System.Data.Odbc.OdbcConnection _objConn = null;
            try
            {
                _objConn = new OdbcConnection(_ConnectionString);

                string strSql = "SELECT * FROM tblscanprescriptions";
                DataSet ds = new DataSet("Image");
                System.Data.Odbc.OdbcDataAdapter tempAP = new System.Data.Odbc.OdbcDataAdapter(strSql, _objConn);
                OdbcCommandBuilder objCommand = new OdbcCommandBuilder(tempAP);
                tempAP.Fill(ds, "Table");

                _objConn.Open();
                DataRow objNewRow = ds.Tables["Table"].NewRow();

                objNewRow["ScanPrescriptionID"] = scanPrescriptionId;
                objNewRow["PrescriptionData"] = prescriptionData;
                objNewRow["FileExtension"] = fileExtention;
                objNewRow["SaleBillID"] = saleBillId;

                ds.Tables["Table"].Rows.Add(objNewRow);
                // trying to update the table to add the image
                tempAP.Update(ds, "Table");
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            finally
            {
                if (_objConn != null)
                    _objConn.Close();
            }
            return retValue;
        }

        //TODEL -> Move to dbSalePrescription
        //public static byte[] ReadPrescriptionData(string saleBillId)
        //{
        //    try
        //    {
        //        DataRow dr = null;
        //        string strSql = "Select * from tblsaleprescriptions WHERE SaleBillID='" + saleBillId + "'";
        //        dr = DBInterface.SelectFirstRow(strSql);
        //        if (dr != null)
        //        {
        //            return (byte[])dr["PrescriptionData"];
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //    return null;
        //}

        internal static void BeginTransaction()
        {
            try
            {
                if (_Connection != null)
                {
                    _Transaction = _Connection.BeginTransaction();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                RollbackTransaction();
            }
        }

        internal static void CommitTransaction()
        {
            try
            {
                if (_Connection != null && _Transaction != null)
                {
                    _Transaction.Commit();
                    _Transaction = null;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                //RollbackTransaction();
            }
        }

        internal static void RollbackTransaction()
        {
            try
            {
                if (_Connection != null && _Transaction != null)
                {
                    _Transaction.Rollback();
                    _Transaction = null;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public static void Dispose()
        {
            CloseConnection();
            _Command = null;
            _Connection = null;
        }
        #endregion

        #region Private Method(s)
        private static void OpenConnection()
        {
            if (_Connection.State == ConnectionState.Closed)
                _Connection.Open();
        }

        private static void CloseConnection()
        {
            if (_Connection.State == ConnectionState.Open)
                _Connection.Close();
        }

        private static string GetProviderString()
        {
            string providerString = "System.Data.Odbc";
            return providerString;
        }
        #endregion Private Method(s)
    }
     */
}
