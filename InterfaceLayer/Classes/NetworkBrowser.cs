using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Security;
using System.Net;
using System.DirectoryServices;
using System.Management;

namespace EcoMart.InterfaceLayer.Classes
{
    public static class NetworkBrowser
    {
        #region Declaration
        //IP Helper Api LIbrary
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        internal static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);

        //declare the Netapi32 : NetServerEnum method import
        [DllImport("Netapi32", CharSet = CharSet.Auto, SetLastError = true),
        SuppressUnmanagedCodeSecurityAttribute]

        private static extern int NetServerEnum
        (
            string ServerNane, // must be null
            int dwLevel,
            ref IntPtr pBuf,
            int dwPrefMaxLen,
            out int dwEntriesRead,
            out int dwTotalEntries,
            int dwServerType,
            string domain, // null for login domain
            out int dwResumeHandle
        );

        //declare the Netapi32 : NetApiBufferFree method import
        [DllImport("Netapi32", SetLastError = true),
        SuppressUnmanagedCodeSecurityAttribute]
        private static extern int NetApiBufferFree(IntPtr pBuf);

        //create a _SERVER_INFO_100 STRUCTURE
        [StructLayout(LayoutKind.Sequential)]
        private struct _SERVER_INFO_100
        {
            internal int sv100_platform_id;
            [MarshalAs(UnmanagedType.LPWStr)]
            internal string sv100_name;
        }
        #endregion //Declaration

        #region Method(s)
        public static ArrayList GetNetworkComputers()
        {
            ArrayList networkComputers = new ArrayList();
            try
            {
                DirectoryEntry DomainEntry = new DirectoryEntry("WinNT://" + Environment.UserDomainName);
                DomainEntry.Children.SchemaFilter.Add("computer");
                foreach (DirectoryEntry machine in DomainEntry.Children)
                {
                    networkComputers.Add(machine.Name);
                }
                DomainEntry.Dispose();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            return networkComputers;
        }

        //public static string GetLocalMacAddress()
        //{
        //    string macAddress = "";
        //    try
        //    {
        //        //macAddress = GetMacAddress(Environment.MachineName);                
        //        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //        ManagementObjectCollection moc = mc.GetInstances();
        //        foreach (ManagementObject mo in moc)
        //        {
        //            bool isIPEnabled = (bool)mo["IPEnabled"];
        //            if (isIPEnabled)
        //            {
        //                macAddress = mo["MacAddress"].ToString();
        //                break;
        //            }
        //        }
        //        macAddress = macAddress.Replace(":", "-").ToUpper();
        //    }
        //    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        //    return macAddress;
        //}

        public static string GetLocalMacAddress()
        {
            string machineName = "";
            try
            {
                machineName = Environment.MachineName;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return machineName;
        }

        public static string GetMacAddress(string pcName)
        {
            return pcName;
        }

        //public static string GetMacAddress(string pcName)
        //{
        //    IPHostEntry ipHostEntry = null;
        //    byte[] macAddress = new byte[6];
        //    int macLength, result;
        //    string macId = "";
        //    try
        //    {
        //        ipHostEntry = Dns.GetHostEntry(pcName);
        //        if (ipHostEntry.AddressList.Length != 0)
        //        {
        //            for (int i = 0; i < ipHostEntry.AddressList.Length; i++)
        //            {
        //                try
        //                {

        //                    macLength = macAddress.Length;
        //                    // This Function Used to Get The Physical Address
        //                    result = SendARP((int)ipHostEntry.AddressList[i].Address, 0, macAddress, ref macLength);
        //                    macId = BitConverter.ToString(macAddress, 0, 6);
        //                    break;
        //                }
        //                catch (Exception) { }
        //            }
        //        }
        //    }
        //    catch (Exception) { }
        //    return macId;
        //}

        //public static string GetComputerName(string macAddress)
        //{
        //    ArrayList pcList;
        //    string computerName = "", macAddr;
        //    try
        //    {
        //        pcList = NetworkBrowser.GetNetworkComputers();
        //        for (int index = 0; index < pcList.Count && computerName.Length == 0; index++)
        //        {
        //            macAddr = GetMacAddress(pcList[index].ToString());
        //            if (Compare(macAddr, macAddress)) computerName = pcList[index].ToString();
        //        }
        //    }
        //    catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        //    return computerName;
        //}

        public static bool Compare(string str1, string str2)
        {
            return str1.Trim().ToLower() == str2.Trim().ToLower();
        }
        #endregion //Method(s)
    }
}
