using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.Printing
{
    public class PrintFactory
    {
        private const double ONELINE = 36;

        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        private static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        private static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }

        private static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }

        public static void SendLineFeed(double NoOfLines)
        {
            try
            {
                // Allow the user to select a printer.
                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new PrinterSettings();
                //MessageBox.Show("pd.PrinterSettings.PrinterName: " + pd.PrinterSettings.PrinterName);
                IntPtr pUnmanagedBytes = new IntPtr(0);
                //int nLength = 3;
                //double lines;
                //double.TryParse(textBox1.Text, out lines);

                //MessageBox.Show("START LineFeed");

                double oLine = NoOfLines * ONELINE;
                while (oLine > 0)
                {
                    Byte[] bytes;
                    if (oLine <= 252)
                    {
                        bytes = new byte[3] { 27, 74, (byte)oLine };
                    }
                    else
                    {
                        bytes = new byte[3] { 27, 74, 252 };
                    }
                    pUnmanagedBytes = Marshal.AllocCoTaskMem(bytes.Length);
                    // Copy the managed byte array into the unmanaged array.
                    Marshal.Copy(bytes, 0, pUnmanagedBytes, bytes.Length);
                    bool retValue = SendBytesToPrinter(pd.PrinterSettings.PrinterName, pUnmanagedBytes, bytes.Length);

                    oLine = oLine - 252;
                }
                //MessageBox.Show("END LineFeed : " + retValue.ToString());
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public static void SendReverseLineFeed(double NoOfLines)
        {
            try
            {
                // Allow the user to select a printer.
                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new PrinterSettings();
                //MessageBox.Show("pd.PrinterSettings.PrinterName: " + pd.PrinterSettings.PrinterName);
                IntPtr pUnmanagedBytes = new IntPtr(0);
                //int nLength = 3;

                //double lines;
                //double.TryParse(textBox1.Text, out lines);

                //MessageBox.Show("START Reverse Feed");

                double oLine = NoOfLines * ONELINE;
                while (oLine > 0)
                {
                    Byte[] bytes;
                    if (oLine <= 252)
                    {
                        bytes = new byte[3] { 27, 106, (byte)oLine };
                    }
                    else
                    {
                        bytes = new byte[3] { 27, 106, 252 };
                    }
                    pUnmanagedBytes = Marshal.AllocCoTaskMem(bytes.Length);
                    // Copy the managed byte array into the unmanaged array.
                    Marshal.Copy(bytes, 0, pUnmanagedBytes, bytes.Length);
                    bool retValue = SendBytesToPrinter(pd.PrinterSettings.PrinterName, pUnmanagedBytes, bytes.Length);

                    oLine = oLine - 252;
                }

                //MessageBox.Show("END Reverse Feed: " + retValue.ToString());
            }
            catch (Exception ex)
            {
              Log.WriteException(ex);
            }
        }
        
    }
}


//public class PrintFactory
//{
//    public const short FILE_ATTRIBUTE_NORMAL = 0x80;
//    public const short INVALID_HANDLE_VALUE = -1;
//    public const uint GENERIC_READ = 0x80000000;
//    public const uint GENERIC_WRITE = 0x40000000;
//    public const uint CREATE_NEW = 1;
//    public const uint CREATE_ALWAYS = 2;
//    public const uint OPEN_EXISTING = 3;
//    private const double ONELINE = 36;

//    [DllImport("kernel32.dll", SetLastError = true)]
//    static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess,
//        uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition,
//        uint dwFlagsAndAttributes, IntPtr hTemplateFile);

//    //[DllImport("Kernel32")]
//    //private extern static Boolean CloseHandle(IntPtr handle);
//    //public static void sendTextToLPT1(String receiptText)
//    //{
//    //    try
//    //    {
//    //        IntPtr ptr = CreateFile("LPT3", GENERIC_WRITE, 0,
//    //                     IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

//    //        /* Is bad handle? INVALID_HANDLE_VALUE */
//    //        if (ptr.ToInt32() == -1)
//    //        {
//    //            /* ask the framework to marshall the win32 error code to an exception */
//    //            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
//    //        }
//    //        else
//    //        {
//    //            FileStream lpt = new FileStream(ptr, FileAccess.ReadWrite);
//    //            Byte[] buffer = new Byte[2048];
//    //            //Check to see if your printer support ASCII encoding or Unicode.
//    //            //If unicode is supported, use the following:
//    //            //buffer = System.Text.Encoding.Unicode.GetBytes(Temp);
//    //            buffer = System.Text.Encoding.ASCII.GetBytes(receiptText);
//    //            lpt.Write(buffer, 0, buffer.Length);
//    //            lpt.Close();
//    //        }
//    //    }
//    //    catch (Exception ex)
//    //    {
//    //        Log.WriteException(ex);
//    //    }
//    //}

//    public static void SendLineFeed(double NoOfLines)
//    {
//        try
//        {
//            string printPort = GetPrintPort();
//            if (printPort == string.Empty)
//                return;

//            IntPtr ptr = CreateFile(printPort, GENERIC_WRITE, 0,
//                         IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

//            /* Is bad handle? INVALID_HANDLE_VALUE */
//            if (ptr.ToInt32() == -1)
//            {
//                /* ask the framework to marshall the win32 error code to an exception */
//                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
//            }
//            else
//            {
//                FileStream lpt = new FileStream(ptr, FileAccess.ReadWrite);
//                Byte[] buffer = new Byte[2048];

//                double oLine = NoOfLines * ONELINE;
//                while (oLine > 0)
//                {
//                    if (oLine <= 252)
//                    {
//                        buffer = new byte[3] { 27, 74, (byte)oLine };
//                        lpt.Write(buffer, 0, buffer.Length);
//                    }
//                    else
//                    {
//                        buffer = new byte[3] { 27, 74, 252 };
//                        lpt.Write(buffer, 0, buffer.Length);
//                    }
//                    oLine = oLine - 252;
//                }

//                lpt.Close();
//            }
//            //CloseHandle(ptr); //ToFIX:Handle issue
//        }
//        catch (Exception ex)
//        {
//            Log.WriteException(ex);
//        }
//    }

//    public static void SendReverseLineFeed(double NoOfLines)
//    {
//        try
//        {
//            string printPort = GetPrintPort();
//            if (printPort == string.Empty)
//                return;

//            IntPtr ptr = CreateFile(printPort, GENERIC_WRITE, 0,
//                         IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

//            /* Is bad handle? INVALID_HANDLE_VALUE */
//            if (ptr.ToInt32() == -1)
//            {
//                /* ask the framework to marshall the win32 error code to an exception */
//                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
//            }
//            else
//            {
//                FileStream lpt = new FileStream(ptr, FileAccess.ReadWrite);
//                Byte[] buffer = new Byte[2048];

//                double oLine = NoOfLines * ONELINE;
//                while (oLine > 0)
//                {
//                    if (oLine <= 252)
//                    {
//                        buffer = new byte[3] { 27, 106, (byte)oLine };
//                        lpt.Write(buffer, 0, buffer.Length);
//                    }
//                    else
//                    {
//                        buffer = new byte[3] { 27, 106, 252 };
//                        lpt.Write(buffer, 0, buffer.Length);
//                    }
//                    oLine = oLine - 252;
//                }

//                lpt.Close();


//            }
//        }
//        catch (Exception ex)
//        {
//            Log.WriteException(ex);
//        }
//    }

//    private static string GetPrintPort()
//    {
//        string port = string.Empty;
//        try
//        {
//            System.Printing.PrintQueue pq = System.Printing.LocalPrintServer.GetDefaultPrintQueue();
//            port = pq.QueuePort.Name.Replace(":", "");
//        }
//        catch (Exception ex)
//        {
//            Log.WriteException(ex);
//        }
//        return port;
//    }
//}
