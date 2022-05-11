using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PharmaSYSPlus.CommonLibrary
{
    public abstract class BaseLog<T>
         where T : BaseLog<T>, new()
    {
        /*
         *  How to use
         * ==============         
            internal class DerivedLog : BaseLog<DerivedLog>
            {
                protected override string GetLogFilename()
                {
                    return "DAWCommon.Net";
                }
            }         
         */

        #region Declarations
        private static string _LogFileDirectory = "";
        private static bool _IsInitialized = false;
        private static bool _IsDebugMode = false;
        private static T _DLog = null;
        private static Object staticObject = new Object();
        #endregion

        #region Abstract methods
        protected abstract string GetLogFilename();
        #endregion

        #region Private Methods
        private static void Initialize()
        {
            try
            {
                if (_IsInitialized == false)
                {
                    _IsDebugMode = true;
                    _LogFileDirectory = Application.StartupPath;
                    _DLog = new T();
                    _IsInitialized = true;
                    DebugLog("Log [ctor] >> Log path = [" + _LogFileDirectory + "]");
                }
            }
            catch (Exception exception)
            {
                _LogFileDirectory = "C:";
                WriteError("Log [ctor] >> " + exception.Message + "\n" + exception.StackTrace);
            }
        }
        protected static string LogFile
        {
            get
            {
                if (_LogFileDirectory == null)
                    return _DLog.GetLogFilename() + "_" + DateTime.Today.ToString("yyyyMMdd") + ".log";
                else
                    return _LogFileDirectory + "\\" + _DLog.GetLogFilename() + "_" + DateTime.Today.ToString("yyyyMMdd") + ".log";
            }
        }
        private static void WriteLog(string Text)
        {
            lock (staticObject)
            {
                Text = "[" + TimeToLogFormat(DateTime.Now) + "]" + Text;

                System.IO.StreamWriter sw = new System.IO.StreamWriter(LogFile, true);
                sw.WriteLine(Text);
                sw.Close();
            }
        }
        private static string TimeToLogFormat(DateTime now)
        {
            return now.Hour.ToString("00") + ":" +
                now.Minute.ToString("00") + ":" +
                now.Second.ToString("00") + ":" +
                now.Millisecond.ToString("000");
        }
        protected static bool LogFileExists(string fileName)
        {
            System.IO.FileInfo fil = new System.IO.FileInfo(fileName);
            return fil.Exists;
        }
        #endregion

        #region Public methods
        public static void DebugLog(string Text)
        {
            Initialize();
            if (_IsDebugMode)
            {
                Text = "Debug >> " + Text;
                WriteLog(Text);
            }
        }
        public static void WriteError(string Text)
        {
            Initialize();
            Text = "ERROR >> " + Text;
            WriteLog(Text);
        }
        public static void WriteException(Exception exp)
        {
            StringBuilder strException = new StringBuilder();
            string strText = "";
            if (exp != null)
            {
                strException.Append("<<" + exp.TargetSite.ReflectedType.Name.ToString());
                strException.Append("." + exp.TargetSite.Name.ToString() + ">> - ");
                strException.Append(exp.StackTrace);
                strText = strException.ToString();
                WriteError(strText);
            }
        }
        public static void WriteInfo(string Text)
        {
            Initialize();
            Text = "Info >> " + Text;
            WriteLog(Text);
        }
        public static bool IsIDEMode()
        {
            try
            {
                object obj = new object();
                obj = null;
                Debug.WriteLine(obj.ToString());
                return false;
            }
            catch
            {
                return true;
            }
        }
        #endregion
    }

    internal class Log : BaseLog<Log>
    {

        protected override string GetLogFilename()
        {
            return "PharmaSYSRetailPlusCommonLibraryLog";
        }
    }
}
