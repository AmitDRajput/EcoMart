using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace EcoMart.InterfaceLayer.Classes
{
    public class RegistryUtility
    {
        private static string connStrRegPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Decos Systems\Decos Audiology Workstation";
        public static string[] GetODBCDrivers()
        {
            RegistryKey localMachine = Registry.LocalMachine;
            RegistryKey software = localMachine.OpenSubKey("Software");
            RegistryKey ODBC = software.OpenSubKey("ODBC");
            RegistryKey ODBCINST = ODBC.OpenSubKey("ODBCINST.INI");
            RegistryKey ODBCDrivers = ODBCINST.OpenSubKey("ODBC Drivers");
            string[] driverNames = ODBCDrivers.GetValueNames();
            return driverNames;
        }

        public static string ReadConnStrFromReg()
        {
            string retVal = (string)Registry.GetValue(connStrRegPath, "dbconstr", string.Empty);
            return retVal;
        }
    }
}
