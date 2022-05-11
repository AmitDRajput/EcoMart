using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PharmaSYSRetailPlus.InterfaceLayer;
using PharmaSYSRetailPlus.Common.Classes;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace PharmaSYSRetailPlus
{
    static class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]       
        static void Main()
        
        {        
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "PharmaSYSRetailPlus", out createdNew))
            {
                if (createdNew)
                {
                    RunPharmaSYSRetailPlus();
                }
                else
                {
                    MessageBox.Show("Another instance of the application is already running...!", PharmaSYSRetailPlus.Common.General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {                           
                            SetForegroundWindow(process.MainWindowHandle);
                            break;
                        }
                    }
                }
            }            
        }

        private static void RunPharmaSYSRetailPlus()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                System.Net.ServicePointManager.DefaultConnectionLimit = 10000;

                FormSplash splashScreen = new FormSplash();
                FrmLogin _FrmLogin = new FrmLogin();
                ValidationHelper helper = new ValidationHelper();

                bool proceed = helper.DoValidate();
              
                if (proceed)
                {
                    proceed = _FrmLogin.LoadData();
                    if (proceed)
                    {
                        DialogResult dresult = _FrmLogin.ShowDialog();
                        if (dresult == DialogResult.OK)
                        {
                           
                            splashScreen.Show();
                            splashScreen.Refresh();
                            Application.DoEvents();
                            MainForm _MainForm = new MainForm(splashScreen);
                            splashScreen.Close();
                            Application.Run(_MainForm);
                        }
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                PharmaSYSRetailPlus.Common.Log.WriteException(ex);
            }
        }       
      
    }
}
