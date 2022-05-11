using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.BusinessLayer;
using System.Drawing;
using System.Windows.Forms;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.InterfaceLayer;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;
using System.Data;
using System.Drawing.Design;

using System.ServiceModel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;


namespace PharmaSYSRetailPlus.Common
{
    public static class General
    {
        internal static System.IFormatProvider frmt = new System.Globalization.CultureInfo("en-US", true);
        public static User CurrentUser;
        public static ShopDetails ShopDetail;
        public static Settings CurrentSetting;
        public static LicenseLib.Licence PharmaSysRetailPlusLicense;
        public static string ApplicationTitle = "PharmaSYS Retail Plus";

        public static BackupPath BackupPath;

        //Focus Color
        public static Color ControlFocusColor = Color.NavajoWhite;

        //Report Colors
        public static Color ReportTitleColor = Color.LightBlue;
        public static Color ReportSubTotalColor = Color.LightGreen;
        public static Color ReportTotalBackColor = Color.LightPink;
        public static Color ReportTotalForeColor = Color.Black;

        // Font Style
        public static FontStyle fsRegular = FontStyle.Regular;
        public static FontStyle fsBold = FontStyle.Bold;  
      
   

        // Font
        public static System.Drawing.Font FontRegular = new System.Drawing.Font("Arial", 8, fsRegular);
        public static System.Drawing.Font FontRegularBold = new System.Drawing.Font("Arial", 8, fsBold);
        public static System.Drawing.Font FontLarge = new System.Drawing.Font("Arial", 14, fsRegular);
        public static System.Drawing.Font FontLargeBold = new System.Drawing.Font("Arial", 14, fsBold);
        public static System.Drawing.Font FontSmall = new System.Drawing.Font("Arial", 6, fsRegular);
        public static System.Drawing.Font FontSmallBold = new System.Drawing.Font("Arial", 6, fsBold);

        public static System.Drawing.Font FontSylfaenRegular8 = new System.Drawing.Font("Sylfaen", 8, fsRegular);
        public static System.Drawing.Font FontSylfaenRegularBold8 = new System.Drawing.Font("Sylfaen", 8, fsBold);
        public static System.Drawing.Font FontSylfaenRegular10 = new System.Drawing.Font("Sylfaen", 10, fsRegular);
        public static System.Drawing.Font FontSylfaenRegularBold10 = new System.Drawing.Font("Sylfaen", 10, fsBold);
        public static System.Drawing.Font FontSylfaenLarge14 = new System.Drawing.Font("Sylfaen", 14, fsRegular);
        public static System.Drawing.Font FontSylfaenLargeBold14 = new System.Drawing.Font("Sylfaen", 14, fsBold);
        public static System.Drawing.Font FontSylfaenLarge12 = new System.Drawing.Font("Sylfaen", 12, fsRegular);
        public static System.Drawing.Font FontSylfaenLargeBold12 = new System.Drawing.Font("Sylfaen", 12, fsBold);
        public static System.Drawing.Font FontBSylfaenSmall6 = new System.Drawing.Font("Sylfaen", 6, fsRegular);
        public static System.Drawing.Font FontSylfaenSmallBold6 = new System.Drawing.Font("Sylfaen", 6, fsBold);

        public static System.Drawing.Font FontTimesNewRomanRegular8 = new System.Drawing.Font("Times New Roman", 8, fsRegular);
        public static System.Drawing.Font FontTimesNewRomanRegularBold8 = new System.Drawing.Font("Times New Roman", 8, fsBold);
        public static System.Drawing.Font FontTimesNewRomanRegular10 = new System.Drawing.Font("Times New Roman", 10, fsRegular);
        public static System.Drawing.Font FontTimesNewRomanRegularBold10 = new System.Drawing.Font("Times New Roman", 10, fsBold);
        public static System.Drawing.Font FontTimesNewRomanLarge14 = new System.Drawing.Font("Times New Roman", 14, fsRegular);
        public static System.Drawing.Font FontTimesNewRomanLargeBold14 = new System.Drawing.Font("Times New Roman", 14, fsBold);
        public static System.Drawing.Font FontTimesNewRomanLarge12 = new System.Drawing.Font("Times New Roman", 12, fsRegular);
        public static System.Drawing.Font FontTimesNewRomanLargeBold12 = new System.Drawing.Font("Times New Roman", 12, fsBold);
        public static System.Drawing.Font FontTimesNewRomanSmall6 = new System.Drawing.Font("Times New Roman", 6, fsRegular);
        public static System.Drawing.Font FontTimesNewRomanSmallBold6 = new System.Drawing.Font("Times New Roman", 6, fsBold);


        public static System.Drawing.Font FontCambriaRegularBold8 = new System.Drawing.Font("Cambria", 8, fsBold);
        public static System.Drawing.Font FontCambriaRegular10 = new System.Drawing.Font("Cambria", 10, fsRegular);
        public static System.Drawing.Font FontCambriaRegularBold10 = new System.Drawing.Font("Cambria", 10, fsBold);
        public static System.Drawing.Font FontCambriaLarge14 = new System.Drawing.Font("Cambria", 14, fsRegular);
        public static System.Drawing.Font FontCambriaLargeBold14 = new System.Drawing.Font("Cambria", 14, fsBold);
        public static System.Drawing.Font FontCambriaLarge12 = new System.Drawing.Font("Cambria", 12, fsRegular);
        public static System.Drawing.Font FontCambriaLargeBold12 = new System.Drawing.Font("Cambria", 12, fsBold);
        public static System.Drawing.Font FontCambriaSmall6 = new System.Drawing.Font("Cambria", 6, fsRegular);
        public static System.Drawing.Font FontCambriaSmallBold6 = new System.Drawing.Font("Cambria", 6, fsBold);

        public static PharmaSYSRetailPlus.Printing.PrintSettings PrintSettings;

        private static ServiceReference1.RetailPlusServiceClient _client;
        private static DataTable ProductListTable;
        public static Guid ServiceGUID;

        public static MainForm FormMainInstance;
        
        private static object mLock = new object();
        private static BackgroundWorker m_oWorker;
        private static BackgroundWorker m_refillWorker;        

        private static ServiceReference1.RetailPlusServiceClient Client
        {
            get
            {
                InitializeClient();
                return _client;
            }
        }

        private static void InitializeClient()
        {
            try
            {
                lock (mLock)
                {
                    if (_client == null)
                    {

                        ServiceReference1.IRetailPlusServiceCallback callback = new PharmaSYSRetailPlus.Common.Classes.RetailServiceCallback();
                        InstanceContext cntx = new InstanceContext(callback);
                        _client = new PharmaSYSRetailPlus.ServiceReference1.RetailPlusServiceClient(cntx);
                        ((ICommunicationObject)_client).Faulted += new EventHandler(Service_Faulted);
                        ServiceGUID = Guid.NewGuid();
                        _client.ConnectClient(ServiceGUID);                        
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("General.Client :: " + ex.ToString());
            }
        }

        private static void Service_Faulted(object sender, EventArgs e)
        {
            try
            {
                _client = null;
                InitializeClient();                
            }
            catch (Exception ex)
            {
                Log.WriteError("General.Client :: " + ex.ToString());
            }
        }

        public static bool UpdateProductListCacheTest(System.Windows.Forms.DataGridViewRowCollection rowCollectionMain, string columnNameMain, System.Windows.Forms.DataGridViewRowCollection rowCollectionTemp, string columnNameTemp)
        {
            bool returnVal = false;
            try
            {
                string productIDList = "";
                foreach (System.Windows.Forms.DataGridViewRow prodrow in rowCollectionMain)
                {
                    if (prodrow.Cells[columnNameMain].Value != null && prodrow.Cells[columnNameMain].Value.ToString() != string.Empty)
                    {
                        string pID = prodrow.Cells[columnNameMain].Value.ToString();
                        productIDList += string.Format("'{0}',", pID);
                    }
                }
                foreach (System.Windows.Forms.DataGridViewRow prodrow in rowCollectionTemp)
                {
                    if (prodrow.Cells[columnNameTemp].Value != null && prodrow.Cells[columnNameTemp].Value.ToString() != string.Empty)
                    {
                        string pID = prodrow.Cells[columnNameTemp].Value.ToString();
                        productIDList += string.Format("'{0}',", pID);
                    }
                }
                if (productIDList.Length > 0)
                {
                    productIDList = productIDList.Substring(0, productIDList.Length - 1);
                    NotifyProductListRefresh(productIDList);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }
                
        private static void NotifyProductListRefresh(string productIDListToRefresh)
        {           
            try
            {               
                m_oWorker = new BackgroundWorker();               
                m_oWorker.DoWork += new DoWorkEventHandler(m_oWorker_DoWork);                
                m_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_oWorker_RunWorkerCompleted);
                m_oWorker.WorkerReportsProgress = true;
                m_oWorker.WorkerSupportsCancellation = true;
                m_oWorker.RunWorkerAsync(productIDListToRefresh);               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }            
        }
        //Worker to notify product list refresh
        private static void m_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool retValue = false;
            try
            {

                string productIDListToRefresh = (string)e.Result;
                retValue = Client.RefreshProductList(productIDListToRefresh);                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        //Worker to notify product list refresh
        private static void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = e.Argument;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }               
             
        public static DataTable ProductList
        {
            get
            {
                if (ProductListTable == null)
                {
                    FillProducts();
                }
                return ProductListTable;
            }
        }

        //Refill Product list
        public static void NotifyProductListRefill()
        {
            try
            {
                m_refillWorker = new BackgroundWorker();
                m_refillWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_refillWorker_RunWorkerCompleted);
                m_refillWorker.WorkerReportsProgress = true;
                m_refillWorker.WorkerSupportsCancellation = true;
                m_refillWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        //Worker to notify product list refill
        private static void m_refillWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
               Client.RefillProductList();               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }       

       
        //Called from CallBack
        public static void RefreshClientProductListForCallBack(string productIDsToRefresh)
        {
            try
            {
                DataTable smallTable = new DataTable();
                string strsql = string.Format("Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack,floor(a.ProdClosingStock/a.ProdLoosePack) as ProdClosingStockPack,a.ProdLastPurchaseDistributorSaleRatePer ," +
                                "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate,a.ProdIfBarCodeRequired," +
                                "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdClosingStock ,a.ProdClosingStock as ProdClosingStockDatabase, a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage, " +
                                "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,b.ShelfID,b.ShelfCode,c.CompID,c.CompName,d.GenericCategoryName from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID left outer join mastergenericcategory d  on a.ProdDrugID = d.GenericCategoryID inner join mastercompany c on a.ProdCompId = c.CompId " +
                                " where a.ProductID IN({0}) order by a.ProdName", productIDsToRefresh);

                smallTable = DBInterface.SelectDataTable(strsql);
                foreach (DataRow dRow in smallTable.Rows)
                {
                    string productID = dRow["ProductID"].ToString();
                    DataRow[] rows = ProductListTable.Select(string.Format("ProductID='{0}'", productID));
                    foreach (DataRow row in rows)
                    {
                        for (int index = 0; index < ProductListTable.Columns.Count; index++)
                        {
                            row[index] = dRow[index];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        //Called from CallBack
        public static void RefillClientProductListForCallBack()
        {
            try
            {
                FillProducts();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        } 
       

        public static void ConnectClient()
        {
            try
            {
                FillProducts();               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private static void FillProducts()
        {
            lock (mLock)
            {
                try
                {
                    if (DBInterface.IsDbConnected)
                    {
                        ProductListTable = new DataTable();
                        string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack,floor(a.ProdClosingStock/a.ProdLoosePack) as ProdClosingStockPack,a.ProdLastPurchaseDistributorSaleRatePer, " +
                                        "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate,a.ProdIfBarCodeRequired," +
                                        "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdClosingStock ,a.ProdClosingStock as ProdClosingStockDatabase, a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage, " +
                                        "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,b.ShelfID,b.ShelfCode,c.CompID,c.CompName,d.GenericCategoryName from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  left outer join mastergenericcategory d  on a.ProdDrugID = d.GenericCategoryID inner join mastercompany c on a.ProdCompId = c.CompId order by a.ProdName";

                        ProductListTable = DBInterface.SelectDataTable(strsql);
                    }                   
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
        }

        public static void DisconnectClient()
        {
            try
            {
                Client.DisconnectClient(General.ServiceGUID);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        internal static void RestartService()
        {
            try
            {
                string serviceName = "PharmaSYS Retail Plus Service";
                int timeoutMilliseconds = 30000;

                System.ServiceProcess.ServiceController service = System.ServiceProcess.ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == serviceName);
                if (service != null)
                {
                    int millisec1 = Environment.TickCount;
                    TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                    if (service.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                    {
                        service.Stop();
                        service.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped, timeout);
                        // count the rest of the timeout
                        int millisec2 = Environment.TickCount;
                        timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));
                    }
                    else
                    {
                        timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds); 
                    }
                    
                    if (service.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                    {
                        service.Start();
                        service.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running, timeout);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("General.RestartService >> Error starting/stopping the service " + ex.ToString());
            }
        }

        public static void DisposeConnection()
        {
            try
            {
                DBInterface.Dispose();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
                
        internal static string ConvertCurrentDateToISODateString()
        {
            string strRetVal = "";

            string strYear = DateTime.Now.Year.ToString("0000");
            string strMonth = DateTime.Now.Month.ToString("00");
            string strDay = DateTime.Now.Day.ToString("00");

            strRetVal = strYear + strMonth + strDay;

            return strRetVal;
        }

        internal static DateTime ConvertStringToDateyyyyMMdd(string getStringDate)
        {
            DateTime dtTemp = DateTime.Now;
            string strFormattedDate = "";
            string strYear = "";
            string strMonth = "";
            string strDay = "";

            if (getStringDate.Length == 8)
            {
                strYear = getStringDate.Substring(0, 4);
                strMonth = getStringDate.Substring(4, 2);
                strDay = getStringDate.Substring(6, 2);

                strFormattedDate = strMonth + "/" + strDay + "/" + strYear;            
                try
                {
                    dtTemp = DateTime.ParseExact(strFormattedDate, "MM/dd/yyyy", frmt);
                }
                catch (Exception ex) { Log.WriteError(ex.ToString()); }
            }

            return dtTemp;
        }

        internal static DateTime ConvertStringToDateddMMyyyy(string getStringDate)
        {
            DateTime dtTemp = DateTime.Now;
            string strFormattedDate = "";
            string strYear = "";
            string strMonth = "";
            string strDay = "";

            if (getStringDate.Length == 8)
            {
                strYear = getStringDate.Substring(4, 4);
                strMonth = getStringDate.Substring(2, 2);
                strDay = getStringDate.Substring(0, 2);

                strFormattedDate = strMonth + "/" + strDay + "/" + strYear;
                try
                {
                    dtTemp = DateTime.ParseExact(strFormattedDate, "MM/dd/yyyy", frmt);
                }
                catch (Exception ex) { Log.WriteError(ex.ToString()); }
            }

            return dtTemp;
        }

        internal static string FormatDate(string getStringDate)
        {
            string strFormattedDate = string.Empty;
            string strYear = "";
            string strMonth = "";
            string strDay = "";

            if (getStringDate.Length == 8)
            {
                strYear = getStringDate.Substring(0, 4);
                strMonth = getStringDate.Substring(4, 2);
                strDay = getStringDate.Substring(6, 2);

                strFormattedDate = strDay + "/" + strMonth + "/" + strYear;
            }

            return strFormattedDate;
        }
        
        internal static string FormatDateddMMyyyyToStringDate(string getStringDate)
        {
            string strFormattedDate = string.Empty;
            string strYear = "";
            string strMonth = "";
            string strDay = "";

            if (getStringDate.Length == 8)
            {
                strYear = getStringDate.Substring(4, 4);
                strMonth = getStringDate.Substring(2, 2);
                strDay = getStringDate.Substring(0, 2);

                strFormattedDate = strDay + "/" + strMonth + "/" + strYear;
            }

            return strFormattedDate;
        }
        
        internal static string FormatDateyyyyMMddToddMMyyyy(string getStringDate)
        {
            string strFormattedDate = string.Empty;
            string strYear = "";
            string strMonth = "";
            string strDay = "";

            if (getStringDate.Length == 8)
            {
                strYear = getStringDate.Substring(0, 4);
                strMonth = getStringDate.Substring(4, 2);
                strDay = getStringDate.Substring(6, 2);

                strFormattedDate = strDay + strMonth + strYear;
            }

            return strFormattedDate;
        }

        internal static bool CheckBillDateForAccountingYear(string vdate)
        {
            bool retValue = false;
            vdate = General.GetExpiryInyyyymmddForm(vdate);
            if (Convert.ToInt32(vdate) < Convert.ToInt32(General.ShopDetail.Shopsy) || Convert.ToInt32(vdate) > Convert.ToInt32(General.ShopDetail.Shopey))
            {
               
                retValue = false;
            }
            else
                retValue = true;

            return retValue;
        }

        internal static bool CheckForValidStringDateddMMyyyy(string getStringDate)
        {
            bool retValue = false;
            string strFormattedDate = string.Empty;
            string strYear = "";
            string strMonth = "";
            string strDay = "";
            int intYear = 0;
            int intMonth = 0;
            int intDay = 0;
            int intres = 0;

            if (getStringDate.Length == 8)
            {
                strYear = getStringDate.Substring(4, 4);
                strMonth = getStringDate.Substring(2, 2);
                strDay = getStringDate.Substring(0, 2);

                intYear = Convert.ToInt32(strYear);
                intMonth = Convert.ToInt32(strMonth);
                intDay = Convert.ToInt32(strDay);
                if (intMonth == 2)
                {
                    Math.DivRem(intYear, 4, out intres);
                    if (intres == 0)
                    {
                        if (intDay >= 1 && intDay <= 29)
                            retValue = true;
                        else
                            retValue = false;
                    }
                    else
                    {
                        if (intDay >= 1 && intDay <= 28)
                            retValue = true;
                        else
                            retValue = false;
                    }
                }
                else
                {
                    if (intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7 || intMonth == 8 || intMonth == 10 || intMonth == 12)
                    {
                        if (intDay >= 1 && intDay <= 31)
                            retValue = true;
                        else
                            retValue = false;
                    }
                    else
                        if (intDay >= 1 && intDay <= 30)
                            retValue = true;
                        else
                            retValue = false;
                }
            }

            return retValue;
        }
        
        internal static string FormatDateddMMyyyyToyyyyMMdd(string getStringDate)
        {
            string strFormattedDate = string.Empty;
            string strYear = "";
            string strMonth = "";
            string strDay = "";

            if (getStringDate.Length == 8)
            {
                strYear = getStringDate.Substring(4, 4);
                strMonth = getStringDate.Substring(2, 2);
                strDay = getStringDate.Substring(0, 2);

                strFormattedDate = strYear + strMonth + strDay;
            }

            return strFormattedDate;
        }
        
        internal static string ConvertDateToISODateString(DateTime getDate)
        {
            string strISODate = "";
            try
            {
                strISODate = getDate.Year.ToString("0000") + getDate.Month.ToString("00") + getDate.Day.ToString("00");
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }

            return strISODate;
        }

        internal static string GetValidExpiry(string exp)
        {
            string cmm = "";
            string cyy = "";
            string expiry = "";        
            string ifslashpresent = "N";
            int islash = 0;
            int explength = exp.Trim().Length;
            string mdate = "";
            int value = 0;
            bool isnum = (int.TryParse(exp.Substring(0,1), out  value));
            try
            {
                if (isnum == false)
                    expiry = "00/00";
                else
                {
                    if (explength > 0)
                    {
                        for (int i = 0; i < explength; i++)
                        {
                            string expchar = "";
                            expchar = (exp.ToString().Substring(i, 1));
                            if (expchar == "/")
                            {
                                islash = i;
                                ifslashpresent = "Y";
                                break;
                            }
                        }
                        if (ifslashpresent == "Y" && islash != 2)
                        {
                            expiry = "00/00";
                        }
                        else
                        {

                            if (ifslashpresent == "Y")
                            {
                                if (exp != "00/00")
                                {

                                    int mm = Convert.ToInt32(exp.ToString().Substring(0, islash));
                                    int yy = Convert.ToInt32(exp.ToString().Substring(islash + 1, 2));
                                    int myear = yy + 2000;
                                    if (mm > 12)
                                    {
                                        expiry = "00/00";
                                    }
                                    else
                                    {
                                        if (mm > 0 && mm < 10)
                                            cmm = ("0" + Convert.ToString(mm).Trim()).ToString();
                                        else
                                            cmm = Convert.ToString(mm).Trim();
                                        if (yy > 0 && yy < 10)
                                            cyy = ("0" + Convert.ToString(yy).Trim()).ToString();
                                        else
                                            cyy = (Convert.ToString(yy).Trim());
                                        if (cmm != "" && cyy != "")
                                        {
                                            expiry = cmm + "/" + cyy;
                                            mdate = Convert.ToString(myear).Trim() + cmm + "01";
                                        }
                                        else
                                            expiry = "00/00";
                                    }
                                }
                                else
                                {
                                    expiry = "00/00";
                                }
                            }
                            else
                            {
                                if (explength == 4)
                                {

                                    int mm = Convert.ToInt32(exp.ToString().Substring(0, 2));
                                    if (mm > 12)
                                    {
                                        expiry = "";
                                    }
                                    else
                                    {
                                        int yy = Convert.ToInt32(exp.ToString().Substring(2, 2));
                                        int myear = yy + 2000;
                                        if (mm > 0 && mm < 10)
                                            cmm = ("0" + Convert.ToString(mm).Trim()).ToString();
                                        else
                                            cmm = exp.ToString().Substring(0, 2);
                                        cyy = exp.ToString().Substring(2, 2);
                                        if (cmm != "" && cyy != "")
                                        {
                                            expiry = cmm + "/" + cyy;
                                        }
                                        else
                                            expiry = "00/00";
                                    }
                                }
                                else
                                {
                                    expiry = "";
                                }
                            }
                        }

                    }
                    else
                    {
                        expiry = "00/00";
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return expiry;
        }

        internal static string GetValidExpiryDate(string exp)
        {
            string expirydate = "";
            if (exp == "0000")
                exp = "00/00";
            if (exp == "00/00")
            {
                expirydate = "";
            }
            else
            {
                string cmm = exp.ToString().Substring(0, 2);
                string cyy = exp.ToString().Substring(3, 2);
                int n;
                bool isnumeric = int.TryParse(cmm, out n);
               
                int mm = Convert.ToInt32(exp.ToString().Substring(0, 2));
                int yy = Convert.ToInt32(exp.ToString().Substring(3, 2)) + 2000;
                string cyy2k = Convert.ToString(yy);

                expirydate = "01/" + cmm + "/" + cyy2k;
           
                DateTime expdt = new DateTime(yy, mm, 1).AddMonths(1);
                if (General.CurrentSetting.MsetGeneralExpiryLast == "Y")
                {
                    expirydate = expdt.Date.ToString().Substring(0,10);
                }
            }

            return expirydate;
        }

     

        internal static string GetExpiryInyyyymmddForm(string exp)
        {
            string expdt = exp;
            if (expdt != "")
            {
                string yy = expdt.Substring(6, 4);
                string mm = expdt.Substring(3, 2);
                string dd = expdt.Substring(0, 2);
                expdt = yy + mm + dd;
            }
            return expdt;
        }

        internal static string GetDateInDateFormat(string exp)
        {

            string cmm = exp.ToString().Substring(4, 2);
            string cyy = exp.ToString().Substring(0, 4);
            string cdd = exp.ToString().Substring(6, 2);

            string voudate = "";
            voudate = cdd + "/" + cmm + "/" + cyy;
            return voudate;
        }

        internal static DateTime GetyyyyMMddDateInDateType(string exp)
        {

            string cmm = exp.ToString().Substring(4, 2);
            string cyy = exp.ToString().Substring(0, 4);
            string cdd = exp.ToString().Substring(6, 2);

            string voudate = "";
            voudate = cdd + "/" + cmm + "/" + cyy;
            DateTime voud = Convert.ToDateTime(voudate);
            return voud;
        }

        internal static string GetDateInShortDateFormat(string exp)
        {
            string voudate = "";
            if (exp != string.Empty)
            {
                string cmm = exp.ToString().Substring(4, 2);
                string cyy = exp.ToString().Substring(0, 4);
                string ccyy = cyy.Substring(2, 2);
                string cdd = exp.ToString().Substring(6, 2);

                
                voudate = cdd + "/" + cmm + "/" + ccyy;
            }
            return voudate;
        }

        internal static string GetReportHeading()
        {
            string retValue = string.Empty;
            if (ShopDetail != null)
            {
                retValue = ShopDetail.ShopName + Environment.NewLine;
                retValue += ShopDetail.ShopAddress1 + Environment.NewLine;
                retValue += ShopDetail.ShopTelephone + Environment.NewLine;
            }
            return retValue;
        }

        internal static string GetProductName(string productID)
        {
            string productname = "";
            DBProduct dbprod = new DBProduct();
            DataRow dr;
            dr = dbprod.GetProductName(productID);
            if (dr["ProdName"] != DBNull.Value)
                productname = dr["ProdName"].ToString();
            return productname;
        }

        internal static string  NoofRows(int count)
        {
            string mymessage = "";
            try
            {
                int noofrecords = count;
                double totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(noofrecords) / FixAccounts.NumberOfRowsPerReport));
                int noofpages = Convert.ToInt32(Math.Ceiling(totpg));
                if (noofrecords == 0)
                    mymessage = "NO Records ";
                else if (noofrecords == 1)
                    mymessage = "Record : " + noofrecords.ToString().Trim() + "   Pages : " +noofpages.ToString();
                else
                    mymessage = "Records : " + noofrecords.ToString().Trim() + "   Pages : " + noofpages.ToString();
            }
                
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return mymessage;
        }
        
        public static string GetMonthAsString(int intmonth)
        {
            string strmonth = "";
            string[] _Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            if (intmonth >= 1 && intmonth <= 12)
                strmonth = _Months[intmonth];
            return strmonth;
        }

        public static string GetMonthAsStringShort(int intmonth)
        {
            string strmonth = "";
            string[] _Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            if (intmonth >= 0 && intmonth < 12)
                strmonth = _Months[intmonth].ToString().Substring(0, 3);
            return strmonth;
        }

        internal static bool CheckDates(string fromdate, string todate)
        { 
            bool retValue = false;
            int intfromdate = 0;
            int inttotdate = 0;
            int.TryParse(fromdate, out intfromdate);
            int.TryParse(todate, out inttotdate);
            try
            {
                if (intfromdate >= Convert.ToInt32(General.ShopDetail.Shopsy) && intfromdate <= Convert.ToInt32(General.ShopDetail.Shopey))
                {
                    if (inttotdate >= Convert.ToInt32(General.ShopDetail.Shopsy) && inttotdate <= Convert.ToInt32(General.ShopDetail.Shopey) && inttotdate >= intfromdate)
                        retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;

       
        }
        public static string GetDefaultBank()
        {
            DataRow dr = null;
            DBAccount dbdata = new DBAccount();
            string accountID = string.Empty;
            try
            {
                dr = dbdata.GetDefaultBank();
                if (dr != null)
                {
                    accountID = dr["AccountID"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return accountID;

        }
        internal static string GetDebitNoteStockTempFile()
        {
            string fileName = "DebitNoteStockTempFile.xml";
            return AppDomain.CurrentDomain.BaseDirectory.ToString() + fileName;
        }

        internal static string GetPatientSaleTempFile()
        {
            string fileName = "PatientSaleTempFile.xml";
            return AppDomain.CurrentDomain.BaseDirectory.ToString() + fileName;
        }

        internal static string GetDebtorSaleTempFile()
        {
            string fileName = "DebtorSaleTempFile.xml";
            return AppDomain.CurrentDomain.BaseDirectory.ToString() + fileName;
        }

        internal static string GetHospitalSaleTempFile()
        {
            string fileName = "HospitalSaleTempFile.xml";
            return AppDomain.CurrentDomain.BaseDirectory.ToString() + fileName;
        }
        internal static string GetInstitutionalSaleTempFile()
        {
            string fileName = "InstitutionalSaleTempFile.xml";
            return AppDomain.CurrentDomain.BaseDirectory.ToString() + fileName;
        }
        internal static string GetPurchaseTempFile()
        {
            string fileName = "PurchaseTempFile.xml";
            return AppDomain.CurrentDomain.BaseDirectory.ToString() + fileName;
        }

        internal static string GetCounterSaleTempFile()
        {
            string fileName = "CounterSaleTempFile.xml";
            return AppDomain.CurrentDomain.BaseDirectory.ToString() + fileName;
        }

        public static void CreatePDF(DataGridView dgv, string PDFFileName)
        {
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dgv.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
          //  PdfPCell [] cell1 = new PdfPCell();
            PdfPCell cell;
            //Adding Header row
            try
            {
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                   cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    pdfTable.AddCell(cell);
                        // .AddCell(cell);
                }
                
                //PdfPRow row = new PdfPRow(cell1);
                //pdfTable.Rows.Add(row);
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }

            //Adding DataRow
            try
            {
                //foreach (DataGridViewRow row in dgv.Rows)
                //{

                //    foreach (DataGridViewCell cell in row.Cells)
                //    {

                //        pdfTable.AddCell(cell.Value.ToString());
                //    }
                //}
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }
            //Exporting to PDF
            try
            {
            string folderPath = "C:\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + PDFFileName, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }
        }

        //internal static void DeleteTempStockByModuleNumber(ModuleNumber module)
        //{
        //    //TempStock tStock = new TempStock();
        //    //tStock.DeleteDetailsByModuleNumber(module);
        //}
       //  ss 1/10/2013  for countersale
        //internal static void DeleteTempStockByModuleNumberAndCustomerNumber(ModuleNumber module, int customerNumber)
        //{
        //    //TempStock tStock = new TempStock();
        //    //tStock.DeleteDetailsByModuleNumberAndCustomerNumber(module, customerNumber);
        //}

        //internal static void DeleteTempStockByComputerName()
        //{
        //    //TempStock tStock = new TempStock();
        //    //tStock.DeleteDetailsByComputerName();
        //}

        internal static void BeginTransaction()
        {
            DBInterface.BeginTransaction();
        }

        internal static void CommitTransaction()
        {
            DBInterface.CommitTransaction();
        }

        internal static void RollbackTransaction()
        {
            DBInterface.RollbackTransaction();
        }

        internal static string AmountToWord(double amount)
        {
           
            double  myamount = amount;
            string word = "Rs.";
          
            string[] _Ones = { " One", " Two", " Three", " Four", " Five", " Six", " Seven", " Eight", " Nine" };
            string[] _Tens = { " Ten", " Twenty", " Thirty", " Fourty", " Fifty", " Sixty", " Seventy", " Eighty", " Ninety" };
            string[] _Teens = { " Eleven", " Tweleve", " Thirteen", " Forteen", " Fifteen", " Sixteen", " Seventeen", " Eighteen", " Nineteen" };
            string _Hundred = " Hundred";
            string _Thousand = " Thousand";
            string _Lakh = " Lakh";
            string _Crore = " Crore";
            string Paise = "";
            string Rupees = "";
           
            try
            {  
                string StrAmount = myamount.ToString("000000000.00").PadLeft(12);
                Paise = StrAmount.Substring(10, 2);
                Rupees = StrAmount.Substring(0, 9);
                string strcrore = Rupees.Substring(0, 2);
                int crore = Convert.ToInt32(strcrore);
                int strright = Convert.ToInt32(strcrore.Substring(1, 1));
                int strleft = Convert.ToInt32(strcrore.Substring(0, 1));
                if (crore > 0)
                {
                    if (crore < 10)
                        word = word + _Ones[crore - 1] + _Crore;
                    else if (crore >= 11 && crore <= 19)
                        word = word + _Teens[crore - 11] + _Crore;
                    else if (strright == 0)
                        word = word + _Tens[strleft - 1] + _Crore;
                    else
                        word = word + _Tens[strleft - 1] + _Ones[strright - 1] + _Crore;
                }

                strcrore = Rupees.Substring(2, 2);
                crore = Convert.ToInt32(strcrore);
                strright = Convert.ToInt32(strcrore.Substring(1, 1));
                strleft = Convert.ToInt32(strcrore.Substring(0, 1));
                if (crore > 0)
                {
                    if (crore < 10)
                        word = word + _Ones[crore - 1] + _Lakh;
                    else if (crore >= 11 && crore <= 19)
                        word = word + _Teens[crore - 11] + _Lakh;
                    else if (strright == 0)
                        word = word + _Tens[strleft - 1] + _Lakh;
                    else
                        word = word + _Tens[strleft - 1] + _Ones[strright - 1] + _Lakh;
                }

                strcrore = Rupees.Substring(4, 2);
                crore = Convert.ToInt32(strcrore);
                strright = Convert.ToInt32(strcrore.Substring(1, 1));
                strleft = Convert.ToInt32(strcrore.Substring(0, 1));
                if (crore > 0)
                {
                    if (crore < 10)
                        word = word + _Ones[crore - 1] + _Thousand;
                    else if (crore >= 11 && crore <= 19)
                        word = word + _Teens[crore - 11] + _Thousand;
                    else if (strright == 0)
                        word = word + _Tens[strleft - 1] + _Thousand;
                    else
                        word = word + _Tens[strleft - 1] + _Ones[strright - 1] + _Thousand;
                }

                strcrore = Rupees.Substring(6, 1);
                crore = Convert.ToInt32(strcrore);
                strright = Convert.ToInt32(strcrore);
                if (crore > 0)
                {
                    if (crore < 10)
                        word = word + _Ones[crore - 1] + _Hundred;
                }

                strcrore = Rupees.Substring(7, 2);
                crore = Convert.ToInt32(strcrore);
                strright = Convert.ToInt32(strcrore.Substring(1, 1));
                strleft = Convert.ToInt32(strcrore.Substring(0, 1));
                if (crore > 0)
                {
                    if (crore < 10)
                        word = word + _Ones[crore - 1];
                    else if (crore >= 11 && crore <= 19)
                        word = word + _Teens[crore - 11];
                    else if (strright == 0)
                        word = word + _Tens[strleft - 1];
                    else
                        word = word + _Tens[strleft - 1] + _Ones[strright - 1];
                }

                strcrore = Paise;
                crore = Convert.ToInt32(strcrore);
                strright = Convert.ToInt32(strcrore.Substring(1, 1));
                strleft = Convert.ToInt32(strcrore.Substring(0, 1));
                if (crore == 0)
                    word = word + " Only.";
                else
                    word = word + " And ";
                if (crore > 0)
                {
                    if (crore > 0 && crore < 10)
                        word = word + _Ones[crore - 1] + " Paise Only";
                    else if (crore >= 11 && crore <= 19)
                        word = word + _Teens[crore - 11] + " Paise Only";
                    else if (strright == 0)
                        word = word + _Tens[strleft - 1] + " Paise Only";
                    else
                        word = word + _Tens[strleft - 1] + _Ones[strright - 1] + " Paise Only";
                }


            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }



            return word;

        }


        public static bool CheckForBlankRowInTheGrid(DataGridView Grid)
        {
            bool retValue = false;
            foreach (DataGridViewRow dr in Grid.Rows)
            {
                if (dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == "")
                {
                    retValue = true;
                    break;
                }
            }
            return retValue;
        }

        public static bool CheckForBlankRowInTheGrid(PSProductViewControl Grid)
        {
            bool retValue = false;
            foreach (DataGridViewRow dr in Grid.Rows)
            {
                if (dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == "")
                {
                    retValue = true;
                    break;
                }
            }
            return retValue;
        }

        public static DataGridView TrasferReportDataToDataGridView(PharmaSYSPlus.CommonLibrary.MReportGridView dgvReportList, DataGridView ReportDataGridView)
        {

            int cc = dgvReportList.Columns.Count;
            DataGridViewColumn col = new DataGridViewColumn();
            int cindex = 0;
            ReportDataGridView.Columns.Clear();
            try
            {
                foreach (DataGridViewColumn vcol in dgvReportList.Columns)
                {
                    col = new DataGridViewTextBoxColumn();                    
                    
                        col.HeaderText = vcol.HeaderText;
                        col.Width = vcol.Width;
                        col.Name = vcol.Name;
                        col.Visible = vcol.Visible;  
                        // DataGridViewCell cell = new DataGridViewCell(dgvReportList.Columns[i].HeaderText);
                        ReportDataGridView.Columns.Add(col);
                        cindex += 1;
                    
                }
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }

                //Adding DataRow
            DataGridViewRow dr = new DataGridViewRow();
            try
            {
                foreach (DataGridViewRow row in dgvReportList.Rows)
                {

                    for (int i = 0; i < cc; i++)
                    {
                        if (row.Cells[i].Value != null)
                        {
                            dr.Cells[i].Value = row.Cells[i].Value.ToString();
                        }
                    }
                    ReportDataGridView.Rows.Add(dr);
                }
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }
                return ReportDataGridView;
            }
            
        
    }
}
