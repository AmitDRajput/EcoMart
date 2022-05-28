using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EcoMart.BusinessLayer;
using System.Drawing;
using System.Windows.Forms;
using EcoMart.DataLayer;
using EcoMart.InterfaceLayer;
using EcoMart.InterfaceLayer.CommonControls;
using System.Data;
using System.Drawing.Design;

using System.ServiceModel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Collections;
using System.Globalization;

namespace EcoMart.Common
{
    public static class General
    {
        internal static System.IFormatProvider frmt = new System.Globalization.CultureInfo("en-GB", true);
        public static User CurrentUser;
        public static ShopDetails ShopDetail;
        public static Settings CurrentSetting;
        public static EcoMartLicenseLib.Licence EcoMartLicense;
        public static string ApplicationTitle = "EcoMart";
        public static string DeveloperId = "SGC01";
        public static string UserId = "RTTT00054";
        public static string Password = "RTTT00054";
        //        String DeveloperId = "SGC01", UserId = "RTTT00054", Password = "RTTT00054";
        public static string ProdID = ""; 
        public static int SubstituteProductID = 0;
        public static int CloseStockProductID = 0; // [ansuman] [06.11.2016]
        public static int SoldStock = 0; // [ansuman] [06.11.2016]


        public static BackupPath BackupPath;
        private static List<UserRights> UserRightsList;
        //   public static string PharmaSYSVersion = "1.1.8"; // start Date:  14/10/2015  Release Date : 
        //  public static string PharmaSYSVersion = "1.1.9"; // start Date:  10/11/2015  Release Date : 16/11/2015
        //  public static string PharmaSYSVersion = "1.2"; // start Date:  15/12/2015  Release Date : 16/11/2015
        //public static string PharmaSYSVersion = "1.2.1"; // start Date:  07/01/2016  Release Date : 07/01/2016 countersale bug
        //  public static string PharmaSYSVersion = "1.2.2"; // start Date:  12/01/2016  Release Date : 12/01/2016 countersale bug for 
        // database pharmsysretailplus for yearend. //
        // public static string PharmaSYSVersion = "1.2.4"; // start Date:  13/01/2016  Release Date : 13/01/2016 credit debitnote 
        //   public static string PharmaSYSVersion = "1.2.5"; // start Date:  14/01/2016  Release Date : 05/02/2016 Product ledger,stock reprocess 
        //  public static string PharmaSYSVersion = "1.2.6"; // start Date:  22/02/2016  Release Date : 05/02/2016 trial balance pal 
        // adjustment in patientsale  bug removed, number of copies printed//.
        //public static string PharmaSYSVersion = "1.2.7"; // start Date:  07/03/2016 payment receipt tempgrid error removed for delete
        //  public static string PharmaSYSVersion = "1.2.8"; // 08/03/2016 after yearend 25 days.
        //  public static string PharmaSYSVersion = "2.0"; // 25/04/2016
        // public static string PharmaSYSVersion = "2.1"; // 27/04/2016 old cash bank payment receipts
        //  public static string PharmaSYSVersion = "2.2"; // 04/05/2016 Stock In/out with purchase rate print,showbatchwithzero = true for stock in for dr.VIG
        //   public static string PharmaSYSVersion = "2.2.2"; // 04/05/2016 Knowledge Transfer to Scorge.
        //  public static string PharmaSYSVersion = "2.2.3"; // 04/07/2016 Roshan Jain,Pataskar etc.
        // public static string PharmaSYSVersion = "2.2.4"; // 23/08/2016 Purchase Order.
        //  public static string PharmaSYSVersion = "2.2.5"; // 29/08/2016  Next Visit Date in sale  check box in cash bank,import salebill
        //  public static string PharmaSYSVersion = "2.2.6"; // 13/09/2016 
        // public static string PharmaSYSVersion = "2.2.7"; // 25/09/2016  Purchase order in purchase
        // public static string PharmaSYSVersion = "3.0.1"; // 07/10/2016  emilan etc
        // public static string PharmaSYSVersion = "3.0.2"; // 12/10/2016  Sale with Product Discount
        //public static string PharmaSYSVersion = "3.0.3"; // 26/10/2016  bugs and points from 3.0.2
        // public static string PharmaSYSVersion = "3.0.4"; // 04/11/2016  For Kirti Medical
        //  public static string PharmaSYSVersion = "3.0.5"; // 04/11/2016  For Kirti Medical
        // public static string PharmaSYSVersion = "3.0.6"; // 04/11/2016  For Kirti Medical
        // public static string PharmaSYSVersion = "3.0.7"; // 04/11/2016  For Kirti Medical
        // public static string PharmaSYSVersion = "3.1.0";  // 1/12/2016  release Date
        //  public static string PharmaSYSVersion = "3.1.1";  // 12/12/2016  release Date after hemants 25 points
        //public static string PharmaSYSVersion = "3.1.2";  // 15/12/2016  countersale totals patient maintenance
        //public static string PharmaSYSVersion = "3.1.3";  // 19/12/2016  Challan Purchse and bulk Product Working        
        //public static string PharmaSYSVersion = "3.1.4";  // 26/12/2016  Complete point opening stock, similar product, Account,Petient ETC. 
        //public static string PharmaSYSVersion = "3.1.5";  // 30/12/2016  
        // public static string PharmaSYSVersion = "3.1.5.1";  // 05/01/2017 Resolution Set to 1024 and 1366 fix column size and Solve institutional calculation bug  
        //public static string PharmaSYSVersion = "3.1.6";  // 05/01/2017
        //public static string PharmaSYSVersion = "3.1.7";  // 24/01/2017 Realese date 24/01/2017_7.00
        //public static string PharmaSYSVersion = "3.1.7.1"; // 25/01/2017  //Solved bug of opening stock..1) Auto save grid product. 2) Save issues etc. 
        //public static string PharmaSYSVersion = "3.1.7.2"; // 25/01/2017
        //public static string PharmaSYSVersion = "3.1.8"; // 30/01/2017
        //public static string PharmaSYSVersion = "3.1.9"; // 06/02/2017
        //public static string PharmaSYSVersion = "3.1.9.1"; // 13/02/2017 Realese date 16/02/2017
        //public static string PharmaSYSVersion = "3.1.10"; // 20/02/2017 Realese Date 24/02/2017
        //public static string PharmaSYSVersion = "3.1.11"; // 26/02/2017 Realese Date 03/03/2017
        //public static string PharmaSYSVersion = "3.1.12"; // 06/03/2017  Release Date 09/03/2017
        //public static string PharmaSYSVersion = "3.1.13"; // 09/03/2017 Release date: 17/03/2017
        // public static string PharmaSYSVersion = "3.1.14"; // 20/03/2017 Release date: 23/03/2017
        //public static string PharmaSYSVersion = "3.1.14.1"; // 27/03/2017 Release date: 29/03/2017
        //public static string PharmaSYSVersion = "3.1.15"; // 31/03/2017 Year End 
        //public static string PharmaSYSVersion = "3.2.0";   //  Star Working 03/04/2017 
        // public static string PharmaSYSVersion = "3.2.1";   //  Star Working 16/04/2017 Release Date 20/04/2017
        // public static string PharmaSYSVersion = "3.2.2";   //  Star Working 21/04/2017 Release Date 25/04/2017
        //public static string PharmaSYSVersion = "3.2.3";   //  Star Working 07/05/2017 Release Date 12/05/2017
        //public static string PharmaSYSVersion = "3.2.4";   //  Star Working 15/05/2017 Release Date 19/05/2017
        public static string PharmaSYSVersion = "3.2.5";   //  Star Working 29/05/2017 Release Date 02/06/2017
        public static string IfYearEndOverGlobal = "N";
        public static string ifYearEndOverForthePreviousYear = "N";
        public static string TodayString = string.Empty;
        public static DateTime TodayDateTime = DateTime.Now;
        //Focus Color
        public static Color ControlFocusColor = Color.NavajoWhite;

        //Report Variables
        public static int MaxNoOfRowsInPDF = 26;


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

        public static EcoMart.Printing.PrintSettings PrintSettings;
        public static EcoMart.Printing.PrintSettingsForDistributor PrintSettingsForDistributor;

        // private static ServiceReference1.RetailPlusServiceClient _client;
        //  private static DataTable ProductListTable;
        public static Guid ServiceGUID;

        public static MainForm FormMainInstance;

        private static object mLock = new object();
        //   private static BackgroundWorker m_oWorker;
        //   private static BackgroundWorker m_refillWorker;

        public static string ExportPath = "d:\\Reports";
        public static string OnlinePurchasePath = "D:\\OnlinePurchae";
        public static int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;
        public static int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
        //public static int ProductPanel =ScreenHeight- 400;
        //public static int BatchPanel = ScreenHeight - 400;



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

        internal static DateTime ConvertStringToDateyyyyMMddForSystemDateUS(string getStringDate)
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
                    dtTemp = DateTime.ParseExact(strFormattedDate, "dd/MM/yyyy", frmt);
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
            bool isnum = (int.TryParse(exp.Substring(0, 1), out  value));
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
                                    expiry = "00/00";
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
            if (exp.ToString().Trim() == "")
                exp = "00/00";
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

                int newmm = mm + 1;
                int newyy = yy;
                if (newmm > 12)
                {
                    newmm = 1;
                    newyy = newyy + 1;
                }

                bool ifleap = false;
                if (yy % 400 == 0)
                {
                    ifleap = true;
                }
                else if (yy % 100 == 0)
                {
                    ifleap = false;
                }
                else if (yy % 4 == 0)
                {
                    ifleap = true;
                }

                string newcmm = newmm.ToString("00");
                string newcyy2k = Convert.ToString(newyy);

                string newdd = "";
                if (mm == 1 || mm == 3 || mm == 5 || mm == 7 || mm == 8 || mm == 10 || mm == 12)
                    newdd = "31";
                else if (mm != 2)
                    newdd = "30";
                else if (ifleap == true)
                    newdd = "29";
                else
                    newdd = "28";
                //  DateTime dt = DateTime.Now;

                ////////if (CultureInfo.InstalledUICulture.DisplayName == "English (United States)")
                ////////{
                ////////    dt = DateTime.ParseExact(expirydate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                ////////   // dt = ConvertDateToISODateString(dt);
                ////////}
                ////////else
                // dt = DateTime.ParseExact(expirydate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //dt = dt.AddMonths(1);

                // date1.ToString("d MMMM",CultureInfo.CreateSpecificCulture("en-US")));
                //  expirydate = dt.ToString("dd/MM/yyyy");

                //  DateTime expdt = new DateTime(yy, mm, 1).AddMonths(1);
                if (General.CurrentSetting.MsetGeneralExpiryLast == "Y")
                {
                    expirydate = newdd+"/" + cmm + "/" + cyy2k;
                }
                //  string tempDate = System.DateTime.Today.ToString("dd/MM/yyyy"); Nilesh
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

        internal static string GetShortDateInyyyymmddForm(string shortdt)
        {
            string expdt = shortdt;
            if (expdt != "")
            {
                string yy = Convert.ToString(Convert.ToInt32(expdt.Substring(6, 4))); // + 2000
                string mm = expdt.Substring(3, 2);
                string dd = expdt.Substring(0, 2);
                expdt = yy + mm + dd;
            }
            return expdt;
        }
        public static string GetTimeinHHMMDDFormat(string _mtime)
        {
            string mtime = _mtime.ToString();
            if (mtime.Length == 8)
            {
                string mhh = mtime.Substring(0, 2);
                string mmm = mtime.Substring(3, 2);
                string mss = mtime.Substring(6, 2);
                mtime = string.Concat(mhh, mmm, mss);
            }
            return mtime;
        }
        internal static string GetDateInDateFormat(string exp)
        {
            string voudate = "";
            if (exp != "")
            {
                string cmm = exp.ToString().Substring(4, 2);
                string cyy = exp.ToString().Substring(0, 4);
                string cdd = exp.ToString().Substring(6, 2);

                voudate = cdd + "/" + cmm + "/" + cyy;
            }
            return voudate;
        }

        internal static DateTime GetyyyyMMddDateInDateType(string exp)
        {

            string cmm = exp.ToString().Substring(4, 2);
            string cyy = exp.ToString().Substring(0, 4);
            string cdd = exp.ToString().Substring(6, 2);

            string voudate = "";
            voudate = cdd + "/" + cmm + "/" + cyy;
            //  DateTime voud = Convert.ToDateTime(General.ConvertStringToDateyyyyMMdd(voudate));

            // string strFormattedDate = cmm + "/" + cdd + "/" + cyy;            

            DateTime voud = DateTime.ParseExact(voudate, "dd/MM/yyyy", frmt);
            // DateTime voud = DateTime.ParseExact(voudate, "MM/dd/yyyy", frmt);        

            // DateTime voud = Convert.ToDateTime(voudate);
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
        internal static string GetDateInEXPDateFormat(string exp)
        {
            string voudate = "";
            if (exp != string.Empty)
            {
                string cmm = exp.ToString().Substring(4, 2);
                string cyy = exp.ToString().Substring(0, 4);
                string ccyy = cyy.Substring(2, 2);
                string cdd = exp.ToString().Substring(6, 2);


                voudate = cmm + "/" + ccyy;
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

        internal static string GetProductName(int ProductID)
        {
            string productname = "";
            DBProduct dbprod = new DBProduct();
            DataRow dr;
            dr = dbprod.GetProductName(ProductID);
            if (dr["ProdName"] != DBNull.Value)
                productname = dr["ProdName"].ToString();
            return productname;
        }

        internal static string NoofRows(int count)
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
                    mymessage = "Record : " + noofrecords.ToString().Trim() + "   Pages : " + noofpages.ToString();
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
        // sheela 9/11/2016
        internal static string GetOpeningStockTempFile()
        {
            string fileName = "OpeningStockTempFile.xml";
            return AppDomain.CurrentDomain.BaseDirectory.ToString() + fileName;
        }
        // sheela 9/11/2016
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

        public static bool CreatePDF(DataGridView dgv, string PDFFileName)
        {
            bool retValue = false;
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
                //string folderPath = "d:\\";
                //if (!Directory.Exists(folderPath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}
                using (FileStream stream = new FileStream(PDFFileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();
                }
                retValue = true;
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }
            return retValue;
        }


       public static bool CreatePDF(PharmaSYSPlus.CommonLibrary.MReportGridView dgv, string PDFFileName)
        {
            bool retValue = false;

            int RowCountPDF = 0;
            //int PrintPageNumberPDF = 1;

            //Creating iTextSharp Table from the DataTable data
            
            PdfPTable pdfTable = new PdfPTable(dgv.Columns.Count);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 10;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable.DefaultCell.BorderWidth = 1;
            PdfPCell cell;
            //Adding Header row
            try
            {
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    pdfTable.AddCell(cell);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            //Adding DataRow
            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    foreach (DataGridViewCell Ccell in row.Cells)
                    {
                        if (Ccell.Value != null && Ccell.ColumnIndex != 0)
                        {
                            pdfTable.WidthPercentage = 90;
                            pdfTable.AddCell(Ccell.Value.ToString());
                            RowCountPDF++;
                            //pdfTable.FooterHeight
                        }
                        else
                        {
                            pdfTable.WidthPercentage = 10;
                            pdfTable.AddCell("");
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            // Calculating no of Rows
            //int totalrowsPDF = pdfTable.Rows.Count;
            //double ttlpgPDF = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrowsPDF) / MaxNoOfRowsInPDF));
            //int PrintTotalPagesPDF = Convert.ToInt32(Math.Ceiling(ttlpgPDF));

            //Exporting to PDF
            try
            {
                using (FileStream stream = new FileStream(PDFFileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    iTextSharp.text.Rectangle page = pdfDoc.PageSize;
                    pdfTable.TotalWidth = page.Width - 50;
                    float h = pdfTable.TotalHeight;
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();


                    PdfContentByte cb;
                    cb = writer.DirectContent;
                    cb = ExportHeader(cb);
                    cb = ExportFooter(cb);
                    pdfTable.WriteSelectedRows(0, -1, 30, 1500, cb);

                    //pdfDoc.Add(head);
                    //pdfDoc.Add(pdfTable);
                    //pdfDoc.Add(pdfTab);
                    pdfDoc.Close();
                    stream.Close();
                }
                retValue = true;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
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

            double myamount = amount;
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
        public static bool CheckForBlankRowInTheGrid(PSProductViewControlCRDB Grid)
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

        public static bool IsUserRightAllowed(string formName, OperationMode mode)
        {
            bool retValue = true;
            try
            {
                foreach (UserRights right in UserRightsList)
                {
                    if (right.FormName.ToLower() == formName.ToLower())
                    {
                        switch (mode)
                        {
                            case OperationMode.Add:
                                if (General.CurrentUser.Level > right.AddLevel)
                                    retValue = false;
                                break;
                            case OperationMode.Edit:
                                if (General.CurrentUser.Level > right.EditLevel)
                                    retValue = false;
                                break;
                            case OperationMode.Delete:
                                if (General.CurrentUser.Level > right.DeleteLevel)
                                    retValue = false;
                                break;
                            case OperationMode.View:
                                if (General.CurrentUser.Level > right.ViewLevel)
                                    retValue = false;
                                break;
                            case OperationMode.Report:
                                if (General.CurrentUser.Level > right.ViewLevel)
                                    retValue = false;
                                break;
                        }
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
        public static void LoadRights()
        {
            UserRightsList = new List<UserRights>();
            UserRights _UserRights = new UserRights();
            UserRightsList = _UserRights.GetRightList();
        }

        public static Hashtable GetTableListByCode(string IDName, string OrderField, string tableName)
        {
            Hashtable compList = new Hashtable();
            try
            {
                DBGeneral dbc = new DBGeneral();
                DataTable dtable = dbc.GetTableListByCode(IDName, OrderField, tableName);
                int rowcount = 0;
                if (dtable != null && dtable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtable.Rows)
                    {
                        rowcount += 1;
                        compList.Add(rowcount, dr[IDName].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return compList;
        }

        public static Hashtable GetTableListVouchers(string IDName, string vouType, int vouno, string vouSubType, string vouSeries, string tableName)
        {
            Hashtable recordList = new Hashtable();
            try
            {
                DBGeneral dbs = new DBGeneral();
                DataTable dtable = dbs.GetTableListVouchers(IDName, vouType, vouno, vouSubType, vouSeries, tableName);
                int rowcount = 0;
                if (dtable != null && dtable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtable.Rows)
                    {
                        rowcount += 1;
                        recordList.Add(rowcount, dr[IDName].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return recordList;

        }

        public static int GetNumberofLinesInGrid(PSProductViewControl DataGrid)
        {
            int numberoflines = 0;
            foreach (DataGridViewRow dr in DataGrid.Rows)
            {
                string id = "";
                if (dr.Cells["Col_ProductID"].Value != null)
                    id = dr.Cells["Col_ProductID"].Value.ToString();
                if (id != "")
                    numberoflines += 1;
            }
            return numberoflines;
        }

        public static int GetNumberofLinesInGrid(PSProductViewCounter2Control DataGrid)
        {
            int numberoflines = 0;
            foreach (DataGridViewRow dr in DataGrid.Rows)
            {
                string id = "";
                if (dr.Cells["Col_ProductID"].Value != null)
                    id = dr.Cells["Col_ProductID"].Value.ToString();
                if (id != "")
                    numberoflines += 1;
            }
            return numberoflines;
        }
        public static PdfContentByte ExportHeader(PdfContentByte cb)
        {
            BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\Arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            //1
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(30, 1600);  //(xPos, yPos)
            cb.ShowText(ShopDetail.ShopName);
            cb.EndText();
            //2
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(30, 1585);  //(xPos, yPos)
            cb.ShowText(ShopDetail.ShopAddress1);
            cb.EndText();
            //3
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(30, 1570);  //(xPos, yPos)
            cb.ShowText(ShopDetail.ShopAddress2);
            cb.EndText();
            //4
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(30, 1555);  //(xPos, yPos)
            cb.ShowText(ShopDetail.ShopTelephone);
            cb.EndText();
            //5
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(30, 1540);  //(xPos, yPos)
            cb.ShowText("From Date To Date : ----");
            cb.EndText();
            //6
            //string Header2 = "ReportHead" + "\n" + "ReportDate" + "\n" + "ReportTime" + "\n" + "PageNumber" + "\n" + "OperatorName";
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(960, 1600);  //(xPos, yPos)
            cb.ShowText("ReportHead: Name Of Report");    // Need To Pass
            cb.EndText();
            //7
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(960, 1585);  //(xPos, yPos)
            cb.ShowText("ReportDate : " + GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));
            cb.EndText();
            //8
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(960, 1570);  //(xPos, yPos)
            cb.ShowText("ReportTime : " + DateTime.Now.TimeOfDay.ToString().Substring(0, 5));
            cb.EndText();
            //9
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(960, 1555);  //(xPos, yPos)
            cb.ShowText("PageNumber : CurrentPageNumber"); // Need To Pass
            cb.EndText();
            //10
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(960, 1540);  //(xPos, yPos)
            cb.ShowText("OperatorName : ----");   // Need To Pass
            cb.EndText();
        
            return cb;
        }
        public static PdfContentByte ExportFooter(PdfContentByte cb)
        {
            BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\Arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //1
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(30, 200);  //(xPos, yPos)
            cb.ShowText((ShopDetail.ShopJurisdiction).ToUpper());
            cb.EndText();
            //2
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(30, 185);  //(xPos, yPos)
            cb.ShowText("DL Number : " + ShopDetail.ShopDLN);
            cb.EndText();
            //3
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(30, 170);  //(xPos, yPos)
            cb.ShowText("VAT Number : " + ShopDetail.ShopVATTINV);
            cb.EndText();
            //4
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(960, 200);  //(xPos, yPos)
            cb.ShowText("For Shop : " + ShopDetail.ShopName);
            cb.EndText();
            //5
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 12);
            cb.SetTextMatrix(960, 170);  //(xPos, yPos)
            cb.ShowText("PHARMASIST SIGN : ");   // Need To Pass
            cb.EndText();
            return cb;
        }
    }
}