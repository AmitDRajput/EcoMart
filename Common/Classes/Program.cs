using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EcoMart.InterfaceLayer;
using EcoMart.Common.Classes;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace EcoMart
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
            //below code give u actual screen size

            // MessageBox.Show("Height:"+height+", width:"+width);  

            RunEcoMart();

            //bool createdNew = true;
            //using (Mutex mutex = new Mutex(true, "EcoMart_" + Instance, out createdNew))
            //{
            //   //using (Mutex mutex = new Mutex(true, "EcoMart", out createdNew))                        
            //    if (createdNew)
            //    {
            //        RunEcoMart();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Another instance of the application is already running...!", EcoMart.Common.General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        Process current = Process.GetCurrentProcess();
            //        foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            //        {
            //            if (process.Id != current.Id)
            //            {                           
            //                SetForegroundWindow(process.MainWindowHandle);
            //                break;
            //            }
            //        }
            //    }
            //}            
        }

        private static void RunEcoMart()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                System.Net.ServicePointManager.DefaultConnectionLimit = 10000;

                ValidationHelper helper = new ValidationHelper();
                FormSplash splashScreen = new FormSplash();
                FrmLogin _FrmLogin = new FrmLogin();

                bool proceed = helper.DoValidate();
              
                if (proceed)
                {
                    string RunAppliction=_FrmLogin.GetCurrentYearData();
                    RunAppliction = "EcoMart" + RunAppliction;
                    bool createdNew = true;
                    using (Mutex mutex = new Mutex(true, RunAppliction, out createdNew))
                    {
                        if (createdNew)
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
                                    //    int x = _MainForm.Size.Height;
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
                            MessageBox.Show("Another instance of the application is already running...!", EcoMart.Common.General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                EcoMart.Common.Log.WriteException(ex);
            }
        }

       
    }
}
