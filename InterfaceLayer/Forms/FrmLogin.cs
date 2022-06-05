using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.BusinessLayer;
using EcoMart.Common;
using System.IO;

namespace EcoMart.InterfaceLayer
{
    public partial class FrmLogin : Form 
    {
        private User _User;
      
        #region Constructor
        public FrmLogin()
        {
            InitializeComponent(); 
            _User = new User();
            btnlogin.Focus();
            this.Icon = EcoMart.Properties.Resources.Icon;
            this.Text = General.ApplicationTitle+" Ver:"+ General.PharmaSYSVersion;
        }

        #endregion         

        private bool IsValid()
        {
            bool retValue = true;
            if (_User.Name == "")
            {
                MessageBox.Show("Please enter the Valid User Name.");
                retValue = false;
            }
            if (_User.Password == "")
            {
                MessageBox.Show("Please enter the Valid Password.");
                retValue = false;
            }
            if (mcbAccountingYear.SelectedID == null)
            {
                MessageBox.Show("Please select the accounting year.");
                retValue = false;
            }
            return retValue;
        }


        private bool ValidateLogin()
        {
            bool retvalue = false;
            _User.Name = txtusrname.Text.Trim();
            _User.Password = txtpasswd.Text.Trim();
            if (IsValid())
            {
               // AccountingYear accountingyear = _AccountingYear.GetOverviewData();
                User user = _User.GetUserByUserNameAndPassword(_User.Name, _User.Password);
                if (user != null )
                { 
                    General.CurrentUser = new User();                    
                    General.CurrentUser.Id = user.Id;
                    General.CurrentUser.Name = user.Name;
                    General.CurrentUser.IfInUse = user.IfInUse;
                    General.CurrentUser.Password = user.Password;
                    General.CurrentUser.Description = user.Description;
                    General.CurrentUser.Level = user.Level;                    
                    General.ShopDetail = new ShopDetails();                
                    General.ShopDetail.FillShopDetails(General.EcoMartLicense);              
                    General.CurrentSetting = new Settings();
                    General.BackupPath = new BackupPath();

                    General.ShopDetail.ShopVoucherSeries = mcbAccountingYear.SeletedItem.ItemData[1];
                    General.ShopDetail.Shopsy = mcbAccountingYear.SeletedItem.ItemData[2];
                    General.ShopDetail.Shopey = mcbAccountingYear.SeletedItem.ItemData[3];
                    General.ShopDetail.ShopYearEndOver = mcbAccountingYear.SeletedItem.ItemData[4];
                    string _today = DateTime.Now.Date.ToString("yyyyMMdd");
                    if (Convert.ToInt32(_today) < Convert.ToInt32(General.ShopDetail.Shopsy) || Convert.ToInt32(_today) > Convert.ToInt32(General.ShopDetail.Shopey))
                    {
                        General.TodayString = General.ShopDetail.Shopey;
                        General.TodayDateTime = General.ConvertStringToDateyyyyMMdd(General.TodayString);
                    }
                    else
                    {
                        General.TodayString = DateTime.Now.Date.ToString("yyyyMMdd");
                        General.TodayDateTime = DateTime.Now.Date;
                    }
                    string mcurrentyear = "N";
                    if (mcbAccountingYear.SeletedItem.ItemData[5] != null)
                         mcurrentyear = mcbAccountingYear.SeletedItem.ItemData[5].ToString();                  
                        General.CurrentSetting.FillSettings();                     
                        General.PrintSettings = new EcoMart.Printing.PrintSettings();
                        if (General.ShopDetail.ShopDistributorSale == "Y")
                            General.PrintSettingsForDistributor = new EcoMart.Printing.PrintSettingsForDistributor();
                        //General.BackupPath.ReadBackupPath();
                        retvalue = true;
                    //}
                    string dd = (DateTime.Now.Date.ToString("yyyyMMdd"));
                    string shops = General.ShopDetail.Shopsy;
                    string shope = General.ShopDetail.Shopey;
                    if (Convert.ToInt32(dd) < Convert.ToInt32(shops) || Convert.ToInt32(dd) > Convert.ToInt32(shope))
                    {
                        DialogResult result;
                        result = MessageBox.Show("You Are In AccountingYear :" + General.ShopDetail.ShopVoucherSeries, EcoMart.Common.General.ApplicationTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Cancel)
                            retvalue = false;
                    }
                }
                else
                {
                    MessageBox.Show("Login Failed...!");
                }
            }
            return retvalue;
           
        }
        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (ValidateLogin())
            {
                this.DialogResult = DialogResult.OK;                                   
            }
        }
        public string GetCurrentYearData()
        {
            string workingyear = "";
            try
            {
                AccountingYear _AccountingYear = new AccountingYear();
                DataTable dt = _AccountingYear.GetOverviewData();
                string currentYear = GetCurrentYear();
                string ifyearendover = "N";
                //if (dt != null)
                //{
                    foreach (DataRow dr in dt.Rows)
                    {
                        workingyear = dr["VoucherSeries"].ToString();
                        ifyearendover = dr["YearEndOver"].ToString();
                        if (currentYear == workingyear)
                        {
                            break;
                        }
                        else if (dr["CurrentYear"].ToString() == "Y")
                        {
                            break;
                        }
                    }
                    if (dt.Rows.Count == 0)
                        workingyear = currentYear;
                //}
                //    workingyear = currentYear;
                }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return workingyear;
        }
        public bool LoadData()
        {
            bool retValue = false;           
            try
            {
                string reportdir = @"c:\Reports";
                if (!Directory.Exists(reportdir))
                {
                    Directory.CreateDirectory(reportdir);
                }

                AccountingYear _AccountingYear = new AccountingYear();
                DataTable dt = _AccountingYear.GetOverviewData();
                DataRow udr = null;
                udr = _User.GetDefaultUser();
                if (udr != null)
                {
                    if (udr["UserName"] != DBNull.Value)
                        txtusrname.Text = udr["UserName"].ToString().Trim();
                    if (udr["Password"] != DBNull.Value)
                        txtpasswd.Text = udr["Password"].ToString().Trim();
                }
                string currentYear = GetCurrentYear();
                string workingyear = "";
                string ifyearendover = "N";
                //if (dt != null)
                //{
                    foreach (DataRow dr in dt.Rows)
                    {
                        workingyear = dr["VoucherSeries"].ToString();
                        ifyearendover = dr["YearEndOver"].ToString();
                        if (currentYear == workingyear)
                        {
                            retValue = true;
                            break;
                        }
                    }
                //}
                General.IfYearEndOverGlobal = ifyearendover;
                
                if (dt != null &&  dt.Rows.Count == 0)
                    workingyear = currentYear;
                if (retValue == false)
                {
                    if (Convert.ToInt32(currentYear) > Convert.ToInt32(workingyear) && ifyearendover == "N") 
                        retValue = true;
                }
                if (ifyearendover == "Y")
                    retValue = true;
                if (retValue)
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        _AccountingYear.AddCurrentYear(currentYear);
                        dt = _AccountingYear.GetOverviewData();
                    }
                    else if (IsCurrentYearEntryPresent(dt, currentYear) == false && ifyearendover != "Y")
                    {
                        _AccountingYear.UpdateCurrentYearColumn();
                        _AccountingYear.AddCurrentYear(currentYear);
                        dt = _AccountingYear.GetOverviewData();
                        FillAccountingYearCombo(dt);
                    }
                    FillAccountingYearCombo(dt);
                    mcbAccountingYear.SelectedID = workingyear;
                    retValue = true;
                }              
                else
                {
                     MessageBox.Show("Please Check Machine Date", EcoMart.Common.General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

                bool lretValue = false;               
                string lastworkingyear = "N";
                foreach (DataRow dr in dt.Rows)
                {
                    lastworkingyear = dr["VoucherSeries"].ToString();

                    if (currentYear != lastworkingyear)
                    {
                        lretValue = true;
                        break;
                    }
                }
                if (lretValue == true)
                    General.ifYearEndOverForthePreviousYear = "N";
                else
                    General.ifYearEndOverForthePreviousYear = "Y";

                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        private void FillAccountingYearCombo(DataTable dt)
        {
            mcbAccountingYear.SelectedID = null;
            mcbAccountingYear.SourceDataString = new string[6] { "VoucherSeries", "VoucherSeries", "FromDate", "ToDate", "YearEndOver","CurrentYear" };
            mcbAccountingYear.ColumnWidth = new string[6] { "0", "50", "0", "0", "0","0" };
            mcbAccountingYear.FillData(dt);           
        }

        private bool IsCurrentYearEntryPresent(DataTable dt, string currentYear)
        {
            bool retValue = false;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["VoucherSeries"].ToString() == currentYear)
                    {
                        retValue = true;
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

        private string GetCurrentYear()
        {
            string retValue = "";
            try
            {
                int month = DateTime.Now.Month;
                int year = Convert.ToInt32(DateTime.Now.Year.ToString("0000").Substring(2));
                if (month >= 4 && month <= 12)
                {
                    retValue = year.ToString() + (year + 1).ToString();                    
                }
                else
                {
                    retValue = (year - 1).ToString() + year.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    
        private void txtpasswd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (ValidateLogin())
                    {
                        this.DialogResult = DialogResult.OK;                        
                    }
                    break;
            }
        }

        private void txtusrname_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtpasswd.Focus();
                    break;
                case Keys.Down:
                    txtpasswd.Focus();
                    break;
            }
        }

        private void mcbAccountingYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnlogin.Focus();
        }       
    }
}