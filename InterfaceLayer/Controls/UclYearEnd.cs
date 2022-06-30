using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.BusinessLayer;
using EcoMart.Common;
using EcoMart.DataLayer;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclYearEnd : BaseControl
    {
        #region Declaration
        //private SsStock _SsStock;
        private YearEnd _YearEnd;

        public string currentdatabase = "EcoMart";
        public string vsleft = "";
        public string vsright = "";
        public string newvoucherseries = "";
        public string oldvoucherseries = "";
        public string _oldyeardatabase = "";
        public string tablename = "";
        public string oldsyear = "";
        public string oldeyear = "";
        public string newsyear = "";
        public string neweyear = "";
        private FinalAccounts _FinalAccounts;
        private string _MFromDate;
        private string _MToDate;

        private string exeName = "EcoMart";       

        #endregion

        public UclYearEnd()
        {
            InitializeComponent();
            _YearEnd = new YearEnd();
           
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                bool retValue =  btnStartClick();
                if (retValue == true)
                {
                    if (ConnectDataNewDataBase(oldvoucherseries))
                    {
                        YearEnd ye = new YearEnd();
                        ye.DeleteFromNewDataBase(_MToDate, oldvoucherseries);
                    }
                    MessageBox.Show("YearEnd Process DONE. It will close the application now...!!!", General.ApplicationTitle, MessageBoxButtons.OK);
                }
                General.DisposeConnection();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            this.Cursor = Cursors.Default;
        }

        private void ShowYearEndProgressInfo(string message)
        {
            try
            {               
                pnlYearEndProgress.Visible = true;
                pnlYearEndProgress.BringToFront();
                lblYearEndMsgLine2.Text = message;
                lblYearEndMsgLine2.Refresh();
                this.Refresh();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override void SetFocus()
        {
            base.SetFocus();
            FormLoad();
            txtpasswd.Focus();

        }
        
        private bool btnStartClick()
        {
            bool retValue = true;
            
            btnStart.Enabled = false;
            _FinalAccounts = new FinalAccounts();           
            if (txtpasswd.Text.ToString() == "DONE")
            {
                if (OldyearNotFound(General.ShopDetail.Shopsy.ToString()))
                {


                    btnStart.Enabled = false;
                    ShowYearEndProgressInfo("Started ... ");

                    string accountingyear = General.ShopDetail.ShopVoucherSeries;
                    oldvoucherseries = General.ShopDetail.ShopVoucherSeries.ToString();
                    vsleft = (Convert.ToInt32(General.ShopDetail.ShopVoucherSeries.Substring(0, 2)) + 1).ToString();
                    vsright = (Convert.ToInt32(General.ShopDetail.ShopVoucherSeries.Substring(2, 2)) + 1).ToString();
                    oldsyear = General.ShopDetail.Shopsy;
                    oldeyear = General.ShopDetail.Shopey;
                    _MFromDate = oldsyear;
                    _MToDate = oldeyear;
                    string lsy = (Convert.ToInt32(General.ShopDetail.Shopsy.Substring(0, 4)) + 1).ToString();
                    string ley = (Convert.ToInt32(General.ShopDetail.Shopey.Substring(0, 4)) + 1).ToString();
                    newsyear = lsy + General.ShopDetail.Shopsy.Substring(4, 4);
                    neweyear = ley + General.ShopDetail.Shopey.Substring(4, 4);
                    newvoucherseries = vsleft + vsright;
                    _oldyeardatabase = "EcoMart" + oldvoucherseries;

                    ShowYearEndProgressInfo("Creating old year database");
                    if (CreateOldYearDataBase())
                    {
                        ShowYearEndProgressInfo("Creating Tables in old year database");

                        CreateTables();

                        //string ImportFilePath = CreateOldDatabase(_oldyeardatabase);
                        
                        ShowYearEndProgressInfo("Checking Connection for old year database");
                        if (ConnectData(oldvoucherseries))
                        {
                            //  RestoreDatabase(ImportFilePath, _oldyeardatabase);

                          //  CreateTables();

                            ShowYearEndProgressInfo("Adding row in accouing year and voucher numbers");

                            try
                            {
                                ChangeAccountingYear();
                                ChangeVoucherNumbers();
                                ShowYearEndProgressInfo("Stock Reprocess..");
                                //   DoStockReprocess();
                                ShowYearEndProgressInfo("Trial Balance..");
                                DoTrialBalanceToGetOpeningBalances();
                                ShowYearEndProgressInfo("Clear Stocks From tblStock..");

                                ClearStocksIntblStock();
                                ShowYearEndProgressInfo("Updating Opening Stock");

                                GetOpeningStock();
                                DeleteUnwantedRows();
                                GetClearedAmountinMasterAccount();
                            }
                            catch (Exception Ex)
                            {
                                Log.WriteException(Ex);
                            }

                        }
                    }
                    else
                        MessageBox.Show("Can Not Connect to OldDataBase");


                //}
                //    else

                //        MessageBox.Show("Database Aleady Exists");

                }
                else
                {
                    MessageBox.Show("Please Do Year End of the Previous Year First");
                    retValue = false;
                }
            }
            else
            {
                MessageBox.Show("Password = DONE");
                btnStart.Enabled = true;
                txtpasswd.Focus();
            }
            return retValue;
        }

        private void RestoreDatabase(string importFilePath, string CurrentDatabase)
        {
            try
            {
                System.Data.Odbc.OdbcConnectionStringBuilder builder = new System.Data.Odbc.OdbcConnectionStringBuilder(EcoMart.DataLayer.DBInterface.ConnectionString);
                if (builder == null)
                    return;
                string ServerName = builder["SERVER"].ToString();
                string DBName = builder["DATABASE"].ToString();
                string UserID = builder["UID"].ToString();
                string UserPassword = builder["PASSWORD"].ToString();

                string constring = string.Format("server={0};user={1};pwd={2};database={3};connection Timeout = 28800;", ServerName,UserID, UserPassword, DBName);


                using (SqlConnection conn = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                       // using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                         //   mb.ImportFromFile(importFilePath);
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError("RestoreDatabase()");
                Log.WriteException(ex);
            }
        }

        private string CreateOldDatabase(string newdatabase)
        {

            try
            {
                string ServerPath = ConfigurationManager.AppSettings["DBServerPath"].ToString();
                string ExeLocation = ServerPath + "\\bin\\mysqldump.exe";
                string tempPath = AppDomain.CurrentDomain.BaseDirectory;
                System.Data.Odbc.OdbcConnectionStringBuilder builder = new System.Data.Odbc.OdbcConnectionStringBuilder(EcoMart.DataLayer.DBInterface.ConnectionString);
                if (builder == null)
                    return string.Empty;

                string ServerName = builder["SERVER"].ToString();
                string DBName = builder["DATABASE"].ToString();
                string UserID = builder["UID"].ToString();
                string UserPassword = builder["PASSWORD"].ToString();

                tempPath = tempPath + General.ConvertCurrentDateToISODateString();
                tempPath = tempPath + "_" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + "_" + General.ShopDetail.ShopVoucherSeries + ".sql";

                System.IO.StreamWriter file = new System.IO.StreamWriter(tempPath);
                ProcessStartInfo proc = new ProcessStartInfo();
                string cmd = string.Format(@"-u{0} -p{1} -h{2} --database {3} ", UserID, UserPassword, ServerName, DBName);
                //string cmd = string.Format(@"-u{0} -p{1} -h{2} {3}", "root", "password", "localhost", "dbfile");
                proc.FileName = ExeLocation;
                proc.RedirectStandardInput = false;
                proc.RedirectStandardOutput = true;
                proc.Arguments = cmd;
                proc.UseShellExecute = false;
                proc.CreateNoWindow = true;
                proc.WindowStyle = ProcessWindowStyle.Hidden;
                Process p = Process.Start(proc);

                //Stopwatch stopwatch = Stopwatch.StartNew();
                //string res;
                //res = p.StandardOutput.ReadToEnd();
                //file.WriteLine(res);
                string line = string.Empty;
                while ((line = p.StandardOutput.ReadLine()) != null)
                {
                    if (line != null && line.ToString() != string.Empty && line.Length > 15 && line.Substring(0, 15).ToUpper() == "CREATE DATABASE")
                        line = line.Replace("EcoMart", newdatabase);
                    else if (line != null && line.ToString() != string.Empty && line.Length > 3 && line.Substring(0, 3).ToUpper() == "USE")
                        line = line.Replace("EcoMart", newdatabase);
                    file.WriteLine(line);
                }
                //stopwatch.Stop();
                //Console.WriteLine(stopwatch.ElapsedMilliseconds);
                p.WaitForExit();
                file.Close();
                return tempPath;
            }
            catch (Exception ex)
            {
                Log.WriteError("Error while backup");
                Log.WriteException(ex);
                return string.Empty;
            }

        }
        private void DoStockReprocess()
        {
            bool retValue = false;
            this.Cursor = Cursors.WaitCursor;
            StockReProcess stk = new StockReProcess();
            retValue = stk.RemoveNegetiveStockFromtblStock();
            retValue = stk.InitializeMasterProduct();

            DataTable dt = stk.GetStockFromtblStock();

            int mprodID = 0;
            int mopstk = 0;
            int mclstk = 0;
            foreach (DataRow dr in dt.Rows)
            {
                mprodID = 0;
                mopstk = 0;
                mclstk = 0;
                if (dr["ProductID"] != DBNull.Value)
                    mprodID = Convert.ToInt32(dr["ProductID"].ToString());
                if (dr["opstk"] != DBNull.Value && dr["opstk"].ToString() != string.Empty)
                    mopstk = Convert.ToInt32(dr["opstk"].ToString());
                if (dr["clstk"] != DBNull.Value && dr["clstk"].ToString() != string.Empty)
                    mclstk = Convert.ToInt32(dr["clstk"].ToString());
                if (mprodID != 0)
                    stk.UpdateStockInMasterProduct(mprodID.ToString(), mopstk, mclstk);
            }

            this.Cursor = Cursors.Default;
            //if (retValue)
            //{
            //    MessageBox.Show("Successfully Processed", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Could Not Process", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}


            // General.NotifyProductListRefill();            

        }

        private void GetOpeningStock()
        {
            try
            {
                GetOpeningStockForCurrentYear();
                GetSaleStockForCurrentYear();
                GetPurchaseStockForCurrentYear();
                GetDebitNoteStockForCurrentYear();
                GetCreditNoteStockForCurrentYear();
                GetCorrectionInRateStockForCurrentYear();
                CalculateOpeningStock(_MToDate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            
        }

        private void CalculateOpeningStock(string toDate)
        {
            _YearEnd.CalculateOpeningStock(toDate);         
        }

        private void GetCorrectionInRateStockForCurrentYear()
        {
            ShowYearEndProgressInfo("Updating CorrectionInRate Stock");
          
            DataTable dtable = new DataTable();           
            dtable = _YearEnd.GetCorrectionInRateForYearEnd(_MToDate);
            string moldstockid = "";
            string mnewstockid = "";
            int mProductID = 0;
            int mnewqty = 0;
            int moldqty = 0;          
            foreach (DataRow dr in dtable.Rows)
            {
                try
                {
                    mnewqty = 0;
                    moldqty = 0;
                    moldstockid = "";
                    mnewstockid = "";
                    mProductID = 0;                   
                    if (dr["OldStockID"] != DBNull.Value)
                        moldstockid = dr["OldStockID"].ToString();
                    if (dr["NewStockID"] != DBNull.Value)
                        mnewstockid = dr["NewStockID"].ToString();
                    if (dr["ProductID"] != DBNull.Value)
                        mProductID = Convert.ToInt32(dr["ProductID"].ToString());

                    if (dr["OldQuantity"] != DBNull.Value && dr["OldQuantity"].ToString() != "")
                        int.TryParse(dr["OldQuantity"].ToString(), out moldqty);
                    if (dr["NewQuantity"] != DBNull.Value && dr["NewQuantity"].ToString() != "")
                        int.TryParse(dr["NewQuantity"].ToString(), out mnewqty);
                    _YearEnd.UpdateCorrectionInRateStock(moldstockid, moldqty, mnewstockid, mnewqty);                   
                    Application.DoEvents();                 
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

            }
        }

        private void GetDebitNoteStockForCurrentYear()
        {
            DataTable dtable = new DataTable();          
            dtable = _YearEnd.GetDebitNoteStockOUTForCurrentYearForYearEnd(_MToDate);
            string mstockid = "";
            int mProductID = 0;
            int mqtyin = 0;
            int mscmqtyin = 0;
            int mloosepack = 1;
            ShowYearEndProgressInfo("Updating DebitNote Stock");        
            foreach (DataRow dr in dtable.Rows)
            {
                try
                {
                    mqtyin = 0;
                    mscmqtyin = 0;
                    mstockid = "";
                    mProductID = 0;
                    mloosepack = 1;
                    if (dr["StockID"] != DBNull.Value)
                        mstockid = dr["StockID"].ToString();
                    if (dr["ProductID"] != DBNull.Value)
                        mProductID = Convert.ToInt32(dr["ProductID"].ToString());
                    if (dr["ProdLoosePack"] != DBNull.Value)
                        mloosepack = Convert.ToInt32(dr["ProdLoosePack"].ToString());
                    if (dr["Quantity"] != DBNull.Value && dr["Quantity"].ToString() != "")
                        int.TryParse(dr["Quantity"].ToString(), out mqtyin);
                    if (dr["SchemeQuantity"] != DBNull.Value && dr["SchemeQuantity"].ToString() != "")
                        int.TryParse(dr["SchemeQuantity"].ToString(), out mscmqtyin);
                    if (General.ShopDetail.ShopDebitNoteWithLooseQuantity == "N")
                    {
                        mqtyin = mqtyin * mloosepack;
                        mscmqtyin = mscmqtyin * mloosepack;
                    }
                        _YearEnd.UpdateDebitNoteStockOUTStock(mstockid, mqtyin, mscmqtyin);
                    
                 
                  
                    Application.DoEvents();
                    //   DateTime start = DateTime.Now;
                    //   retValue = _YearEnd.UpdateClosingStockForYearEnd(mProductID, mstockid, mclosingstock);
                    //  DateTime end = DateTime.Now;
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

            }
        }

        private void GetCreditNoteStockForCurrentYear()
        {
            DataTable dtable = new DataTable();          
            dtable = _YearEnd.GetCreditNoteStockInForCurrentYearForYearEnd(_MToDate);
            string mstockid = "";
            int mProductID = 0;
            int mqtyin = 0;
            int mscmqtyin = 0;
            ShowYearEndProgressInfo("Updating CreditNote Stock");
       
            foreach (DataRow dr in dtable.Rows)
            {
                try
                {
                    mqtyin = 0;
                    mscmqtyin = 0;
                    mstockid = "";
                    mProductID = 0;                    
                    if (dr["StockID"] != DBNull.Value)
                        mstockid = dr["StockID"].ToString();
                    if (dr["ProductID"] != DBNull.Value)
                        mProductID = Convert.ToInt32(dr["ProductID"].ToString());

                    if (dr["Quantity"] != DBNull.Value && dr["Quantity"].ToString() != "")
                        int.TryParse(dr["Quantity"].ToString(), out mqtyin);
                    if (dr["SchemeQuantity"] != DBNull.Value && dr["SchemeQuantity"].ToString() != "")
                        int.TryParse(dr["SchemeQuantity"].ToString(), out mscmqtyin);
                    _YearEnd.UpdateCreditNoteStockINStock(mstockid, mqtyin, mscmqtyin); 
                    Application.DoEvents();                   
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

            }
        }

        private void GetSaleStockForCurrentYear()
        {

            DataTable dtable = new DataTable();          
            dtable = _YearEnd.GetSaleStockForCurrentYearForYearEnd(_MToDate);
            string mstockid = "";
            int mProductID = 0;
            int mqtyin = 0;
            int mscmqtyin = 0;
            ShowYearEndProgressInfo("Updating Sale Stock");
           
            foreach (DataRow dr in dtable.Rows)
            {
                try
                {
                    mqtyin = 0;
                    mscmqtyin = 0;
                    mstockid = "";
                    mProductID = 0;                  
                    if (dr["StockID"] != DBNull.Value)
                        mstockid = dr["StockID"].ToString();
                    if (dr["ProductID"] != DBNull.Value)
                        mProductID = Convert.ToInt32(dr["ProductID"].ToString());

                    if (dr["Quantity"] != DBNull.Value && dr["Quantity"].ToString() != "")
                        int.TryParse(dr["Quantity"].ToString(), out mqtyin);
                    if (dr["SchemeQuantity"] != DBNull.Value && dr["SchemeQuantity"].ToString() != "")
                        int.TryParse(dr["SchemeQuantity"].ToString(), out mscmqtyin);
                    _YearEnd.UpdateSaleStock(mstockid, mqtyin, mscmqtyin);                   
                    Application.DoEvents();                   
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

            }
        }

        private void GetPurchaseStockForCurrentYear()
        {
            ShowYearEndProgressInfo("Updating Purchase Stock");
          
            DataTable dtable = new DataTable();         
            dtable = _YearEnd.GetPurchaseStockForCurrentYearForYearEnd(_MToDate);
            string mstockid = "";
            int mProductID = 0;
            int mqtyin = 0;
            int mscmqtyin = 0;
            int mclosingstock = 0;
            int counter = 0;
            foreach (DataRow dr in dtable.Rows)
            {
                try
                {
                    mqtyin = 0;
                    mscmqtyin = 0;
                    mstockid = "";
                    mProductID = 0;
                    mclosingstock = 0;
                    if (dr["StockID"] != DBNull.Value)
                        mstockid = dr["StockID"].ToString();
                    if (dr["ProductID"] != DBNull.Value)
                        mProductID = Convert.ToInt32(dr["ProductID"].ToString());

                    if (dr["Quantity"] != DBNull.Value && dr["Quantity"].ToString() != "")
                        int.TryParse(dr["Quantity"].ToString(), out mqtyin);
                    if (dr["SchemeQuantity"] != DBNull.Value && dr["SchemeQuantity"].ToString() != "")
                        int.TryParse(dr["SchemeQuantity"].ToString(), out mscmqtyin);
                    _YearEnd.UpdatePurchaseStock(mstockid, mqtyin, mscmqtyin);
                    mclosingstock = mqtyin + mscmqtyin;
                    counter += 1;
                    Application.DoEvents();                   
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

            }
        }

        private void GetOpeningStockForCurrentYear()
        {

            DataTable dtable = new DataTable();          
            dtable = _YearEnd.GetOpeningStockForCurrentYear(_MToDate);          
            string mstockid = "";
            int mProductID = 0;
            int mqtyin = 0;           
            int mscmqtyin = 0;          
            foreach (DataRow dr in dtable.Rows)
            {
                try
                {
                    mqtyin = 0;                   
                    mscmqtyin = 0;                  
                    mstockid = "";
                    mProductID = 0;                  
                    if (dr["StockID"] != DBNull.Value)
                        mstockid = dr["StockID"].ToString();
                    if (dr["ProductID"] != DBNull.Value)
                        mProductID = Convert.ToInt32(dr["ProductID"].ToString());
                    if (dr["Quantity"] != DBNull.Value && dr["Quantity"].ToString() != "")
                        int.TryParse(dr["Quantity"].ToString(), out mqtyin);
                    if (dr["SchemeQuantity"] != DBNull.Value && dr["SchemeQuantity"].ToString() != "")
                        int.TryParse(dr["SchemeQuantity"].ToString(), out mscmqtyin);
                    _YearEnd.UpdateOpeningStock(mstockid, mqtyin, mscmqtyin);
                    Application.DoEvents();                   
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

            }
        }

        private void  ClearStocksIntblStock()
        {
            bool  retValue = _YearEnd.ClearStockIntblStockAndMasterProductForYearEnd();
        }      
        private bool OldyearNotFound(string voucherSeries)
        {
            bool retValue = false;
            retValue = _FinalAccounts.OldyearNotFound(voucherSeries);
                return retValue;
        }
        private void DoTrialBalanceToGetOpeningBalances()
        {
   
            try
            {

                DataTable dtable = new DataTable();
                _FinalAccounts.InitializeDBCRFieldsInMasterAccount();
                dtable = _FinalAccounts.GetTrialBalanceOPDBCRFromTransaction(_MFromDate);
                DataTable _TransactionOP = dtable;
                dtable = _FinalAccounts.GetTrialBalanceTRDBCRFromTransaction(_MFromDate, _MToDate);
                DataTable _TransactionSource = dtable;
                _FinalAccounts.UpdatebalancesInMasterAccount(_TransactionOP, _TransactionSource);
                _FinalAccounts.CalculateClosingBalanceInmasterAccount();
                _FinalAccounts.CopyClosingBalanceAsOpening();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void DeletePreviousYearFromtblAccountingYearandtblvouchernumbers()
        {
            string accountingyear = General.ShopDetail.ShopVoucherSeries;
            YearEnd ye = new YearEnd();
            ye.DeletePreviousYearFromtblAccountingYearandtblvouchernumbers(accountingyear);
        } 
        private void CreateTables()
        {
            YearEnd ye = new YearEnd();
            bool retValue = false;
            try
            {
                //changed:1-16
                tablename = "changeddetailcashbankexpenses";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changeddetailcashbankpayment";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changeddetailcashbankreceipt";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changeddetailcreditdebitnoteamount";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changeddetailcreditdebitnotestock";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changeddetailpurchase";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changeddetailsale";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changedspecialdetailsale";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changedspecialvouchersale";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changedvouchercashbankexpenses";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changedvouchercashbankpayment";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changedvouchercashbankreceipt";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changedvouchercreditdebitnote";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changedvoucherjv";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changedvoucherpurchase";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "changedvouchersale";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            try
            {
                // deleted:1-16 detail 1-9
                tablename = "deleteddetailcashbankexpenses";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deleteddetailcashbankpayment";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deleteddetailcashbankreceipt";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deleteddetailcreditdebitnoteamount";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deleteddetailcreditdebitnotestock";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deleteddetailpurchase";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deleteddetailsale";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deletedspecialdetailsale";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deletedspecialvouchersale";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deletedvouchercashbankexpenses";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deletedvouchercashbankpayment";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deletedvouchercashbankreceipt";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deletedvouchercreditdebitnote";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deletedvoucherjv";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deletedvoucherpurchase";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "deletedvouchersale";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "detailcashbankexpenses";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "detailcashbankpayment";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "detailcashbankreceipt";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "detailchequereturn";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "detailcreditdebitnoteamount";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "detailcreditdebitnotestock";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "detailopstock";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "detailpurchase";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "detailsale";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            try
            {
                //1-6  master:1-23 special - 1-2
            tablename = "inity";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "linkdebtorproduct";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "linkdruggrouping";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "linkpartycompany";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "linkpatientproduct";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "linkprescription";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masteraccount";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "masteraccountnew";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "masterarea";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterbank";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterbranch";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "mastercompany";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "mastercustomer";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterdoctor";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "mastergenericcategory";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "mastergroup";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterhospitalpatient";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterpurchaseorderstockist";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterpack";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterpacktype";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterpatient";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterprescription";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterproduct";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterproductcategory";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "mastersalesman";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterscheduleddrug";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterscheme";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "mastershelf";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "mastervatpercent";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterward";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "specialdetailsale";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "specialvouchersale";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            try
            {
                //tbl:1-30
            tablename = "tblaccountingyear";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "tblbillimportlink";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "detailpurchaseorderstockist";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "masterEmail";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblfavourite";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblfixaccounts";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblformulae";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tbllocktable";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "mastermessage";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "tblolddetailpurchase";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "tblolddetailsale";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "tbloldvoucherpurchase";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "tbloldvouchersale";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "tbloldvoucherstatement";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                
            tablename = "tbloperator";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblEcoMartlic";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblphonebook";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblsaleprescriptions";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblscanprescriptions";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblschedule";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblsettings";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblstock";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tbltemppurchase";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tbltempstock";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tbltrnac";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tbluser";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tbluserlevel";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tbluserrights";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblvat";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblvouchernumbers";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "tblvouchertypes";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            try
            {
                // voucher 1-11
                tablename = "vouchercashbankexpenses";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "vouchercashbankpayment";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "vouchercashbankreceipt";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "voucherchallanpurchase";
                retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
                tablename = "voucherchequereturn";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "vouchercorrectioninrate";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "vouchercreditdebitnote";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "voucherjv";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "voucheropstock";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "voucherpurchase";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "vouchersale";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            tablename = "voucherstatement";
            retValue = ye.CreateTable(currentdatabase, _oldyeardatabase, tablename);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private bool CreateOldYearDataBase()
        {
            bool retValue = false;
            YearEnd ye = new YearEnd();
            retValue = ye.CreateNewBase(currentdatabase, _oldyeardatabase);
            return retValue;
        }

        private bool ConnectData(string VoucherSeries)
        {
            bool retValue = false;
            try
            {
                
                string conectString = "SERVER={0};DATABASE={1};UID={2};PASSWORD={3}" ;
                string connStrCurrentYear = ConfigurationManager.ConnectionStrings["EcoMartConnectionString"].ConnectionString;
                connStrCurrentYear = EcoMartLicenseLib.Common.Decrypt(connStrCurrentYear);

                System.Data.Odbc.OdbcConnectionStringBuilder builder = new System.Data.Odbc.OdbcConnectionStringBuilder(connStrCurrentYear);
                if (builder == null)
                    return false;

                DBInterface.Initialize();              

                string _server = builder["server"].ToString();
                string _databasename = _oldyeardatabase;
                string _username = builder["uid"].ToString();

                string _password = builder["password"].ToString();
                string newconnStr = string.Format(conectString, _server, _databasename, _username, _password);
                               
                if (DBInterface.IsDbConnected)
                {                   
                    CopyCompleteFolder(VoucherSeries,newconnStr);                 
                    retValue = true;
                }
               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private bool ConnectDataNewDataBase(string VoucherSeries)
        {
            bool retValue = false;
            try
            {

                string conectString = "SERVER={0};DATABASE={1};UID={2};PASSWORD={3};default command timeout = 20";
                string connStrOld = ConfigurationManager.ConnectionStrings["EcoMartConnectionString"].ConnectionString;
                connStrOld = EcoMartLicenseLib.Common.Decrypt(connStrOld);

                System.Data.Odbc.OdbcConnectionStringBuilder builder = new System.Data.Odbc.OdbcConnectionStringBuilder(connStrOld);
                if (builder == null)
                    return false;

                string _server = builder["server"].ToString();
                string _databasename = _oldyeardatabase;
                string _username = builder["uid"].ToString();

                // 11/9/2015 instead of pwd  password
                string _password = builder["password"].ToString();
                string connStr = string.Format(conectString, _server, _databasename, _username, _password);
              
                DBInterface.ConnectionString = connStr;
                DBInterface.Initialize();
                if (DBInterface.IsDbConnected)
                {                   
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private void CopyCompleteFolder(string VoucherSeries, string connStr)
        {
            try
            {
                string SourceDirectory = AppDomain.CurrentDomain.BaseDirectory;
                DirectoryInfo dirInfo = new DirectoryInfo(SourceDirectory);
                string DestinationDirectory = dirInfo.Parent.FullName + "\\PharmaSYS Retail Plus_" + VoucherSeries;
                if (System.IO.Directory.Exists(DestinationDirectory) == false)
                    System.IO.Directory.CreateDirectory(DestinationDirectory);

                string[] files = System.IO.Directory.GetFiles(SourceDirectory);
                foreach (string s in files)
                {
                    string fileName = Path.GetFileName(s);
                    string destFile = Path.Combine(DestinationDirectory, fileName);
                    File.Copy(s, destFile);
                }
                //Rename exe & config file
                string exeOldName = DestinationDirectory + "\\" + exeName + ".exe";
                string exeNewName = DestinationDirectory + "\\" + exeName + VoucherSeries + ".exe";

                string configOldName = DestinationDirectory + "\\" + exeName + ".exe.config";
                string configNewName = DestinationDirectory + "\\" + exeName + VoucherSeries + ".exe.config";

                File.Move(exeOldName, exeNewName);
                File.Move(configOldName, configNewName);

                // save connection
                Configuration config = ConfigurationManager.OpenExeConfiguration(exeNewName);
                //Encrypt conenction string
                connStr = EcoMartLicenseLib.Common.Encrypt(connStr);
                ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;
                if (section != null)
                {
                    section.ConnectionStrings["EcoMartConnectionString"].ConnectionString = connStr;
                    config.Save();
                    ConfigurationManager.RefreshSection("connectionStrings");
                }            
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void SaveConnectionString(string connStr)
        {
            ////Encrypt conenction string
            //connStr = EcoMartLicenseLib.Common.Encrypt(connStr);
            //Configuration config =  ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;
            //if (section != null)
            //{
            //    section.ConnectionStrings["EcoMartConnectionString"].ConnectionString = connStr;
            //    config.Save();
            //    ConfigurationManager.RefreshSection("connectionStrings");              
            //}           
        }

        private void ChangeAccountingYear()
        {
            bool retValue = false;
            YearEnd ye = new YearEnd();
            retValue =  ye.RemovePreviousYeartblaccouningYear(oldvoucherseries);
            retValue = ye.AddRowForCurrentAccountingYear(newvoucherseries, newsyear, neweyear);
        }

        private void ChangeVoucherNumbers()
        {
            bool retValue = false;
            YearEnd ye = new YearEnd();
            retValue = ye.RemovePreviousYeartblVoucherNumbers(oldvoucherseries , _oldyeardatabase, newvoucherseries,currentdatabase);
            retValue = ye.AddRowForCurrentAccountingYeartblVoucherNumbers(newvoucherseries, newsyear, neweyear, currentdatabase, oldvoucherseries);
        }

        private void UclYearEnd_Load(object sender, EventArgs e)
        {
            FormLoad();           
        }

        private void FormLoad()
        {
            tsBtnAdd.Visible = false;
            tsBtnCancel.Visible = false;
            tsBtnDelete.Visible = false;
            tsBtnEdit.Visible = false;
            tsBtnFifth.Visible = false;
            tsBtnFirst.Visible = false;
            tsBtnLast.Visible = false;
            tsBtnNext.Visible = false;
            tsBtnPrevious.Visible = false;
            tsBtnPrint.Visible = false;
            tsBtnSearch.Visible = false;
            btnStart.Enabled = true;
        }

        private void txtpasswd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnStart.Focus();
        }

        private void btnStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnStartClick();
        }

        private void DeleteUnwantedRows()
        {
            ShowYearEndProgressInfo("Delete For Vouchers");

            try
            {
                _YearEnd.DeleteFromVouchers(_MToDate);
                ShowYearEndProgressInfo("Delete For Changed and Deleted Details");

                _YearEnd.DeleteFromChangedDeletedDetails();
                ShowYearEndProgressInfo("Copy to OldVoucher Sale");

                _YearEnd.SelectFromSale(_MToDate);
                ShowYearEndProgressInfo("Copy to OldVoucher Purchase");

                _YearEnd.SelectFromPurchase(_MToDate);
                ShowYearEndProgressInfo("Copy to OldVoucher Statement");

                _YearEnd.SelectFromStatement(_MToDate);
                ShowYearEndProgressInfo("Delete From Purchase Sale and statements");

                _YearEnd.DeleteForSalePurchaseAndStatement(_MToDate);
                ShowYearEndProgressInfo("Delete From Other Remaining Tables");

                _YearEnd.DeleteFromDetails(_MToDate);
                ShowYearEndProgressInfo("Delete From Other Details Tables");

                //_YearEnd.DeleteFromtblTrnacdetailpurchaseorder(_MToDate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void GetClearedAmountinMasterAccount()
        {
            ShowYearEndProgressInfo("Get Cleared Amount In Master Account");
           
            _YearEnd.GetClearedAmountinMasterAccount(_MToDate);
          // select a.BillType,a.BillDate, b.AccountID,sum(a.clearAmount) as clearamount,a.DiscountAmount from detailcashbankreceipt a inner join vouchercashbankreceipt b on a.MasterID = b.cbid where b.AccountId = "1829116FC9BB4A91BDE23DD34D4FFD72" && a.BillDate <= "20150331"
        }
       
    }
}
